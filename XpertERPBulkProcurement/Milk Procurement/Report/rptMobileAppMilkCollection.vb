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
        chkDifference.Checked = False
        ControlEnableDisable(True)
    End Sub
    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        fromDate.Enabled = isEnable
        dtpToDate.Enabled = isEnable
        txtBMC.Enabled = isEnable
        txtRoute.Enabled = isEnable
        txtDCS.Enabled = isEnable
        RadGroupBox1.Enabled = isEnable
        chkDifference.Enabled = isEnable
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
        GetReportID()
        LoadData()
        'ControlEnableDisable(False)
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
            Dim qry As String = "select MCC_Code,Mcc_Code_VLC_Uploader as [MCC Uploader Code],MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where tspl_mcc_master.mcc_Code in (" & StrPermission & ")"
            txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtBMC.arrValueMember, txtBMC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadData()
        Try
            Dim dt As New DataTable
            Dim strQry As String = ""
            Dim FromDateAvg As New DateTime(fromDate.Value.Year, fromDate.Value.Month, 1)
            FromDateAvg = FromDateAvg.AddMonths(-1)
            Dim ToDateAvg As DateTime = FromDateAvg.AddMonths(1).AddDays(-1)

            If chkDifference.Checked Then
                strQry = LoadDiffData()
            Else
                strQry = ReturnQry()
                If rbtnBMC.Checked Then
                    strQry = "select *,isnull(Average_Qty,0)Avg_Qty from ( " + strQry + " Left join ( select MCC_Code as MCC,isnull(convert(decimal(18,2),sum(qty)/count(Days)),0) as Average_Qty from ( select TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,sum(isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,0)) as  Qty,1 as Days
           from TSPL_MILK_COLLECTION_MCC_DETAIL left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No where convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) >='" + clsCommon.GetPrintDate(FromDateAvg, "dd/MMM/yyyy") + "' and convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103)<='" + clsCommon.GetPrintDate(ToDateAvg, "dd/MMM/yyyy") + "' group by MCC_Code,Document_Date ) xxxx group by MCC_Code ) xxx on xxx.MCC= xx.MCC_Code"
                ElseIf rbtnDCS.Checked Then
                    strQry = "select  *,isnull(Average_Qty,0)Avg_Qty from ( " + strQry + " left join (
					 select VLC_CODE as VLC,isnull(convert(decimal(18,2),sum(qty)/count(Days)),0) as Average_Qty from ( select sum(isnull(TSPL_MILK_SRN_DETAIL.Qty,0)) as  Qty,1 as Days,VLC_CODE
                from TSPL_MILK_SRN_DETAIL left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE where convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) >='" + clsCommon.GetPrintDate(FromDateAvg, "dd/MMM/yyyy") + "'  and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)<= '" + clsCommon.GetPrintDate(ToDateAvg, "dd/MMM/yyyy") + "' group by VLC_CODE,DOC_DATE ) xxxx group by VLC_CODE )xxx on xxx.VLC = xx.VLC_Code"
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
            Dim viewBlank As New TableViewDefinition()
            Gv1.ViewDefinition = viewBlank
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                Gv1.ShowGroupPanel = False
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                If chkDifference.Checked Then
                    FormatDiffDataGrid()
                    View()
                Else
                    FormatGrid()
                End If
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If

            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub View()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            If rbtnBMC.Checked = True Then
                view.ColumnGroups.Add(New GridViewColumnGroup("Mobile App BMC Data"))
            ElseIf rbtnDCS.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("Mobile App DCS Data"))
            End If
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Mcc_Code_VLC_Uploader").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("MCC_NAME").Name)
            If rbtnBMC.Checked = True Then
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Gaze_Reading").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Sample_No").Name)
            ElseIf rbtnDCS.Checked Then
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("VLC_Code_VLC_Uploader").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("VLC_Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("VLC_Name").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Shift").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Own BMC").Name)
            End If
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Route_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Vehicle_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Trip_No").Name)


            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("FAT").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("SNF").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("FATKG").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("SNFKG").Name)

            If rbtnBMC.Checked = True Then
                view.ColumnGroups.Add(New GridViewColumnGroup("ERP BMC Data"))
            ElseIf rbtnDCS.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("ERP DCS Data"))
            End If
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())

            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_Date").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_Mcc_Code_VLC_Uploader").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_MCC_Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_MCC_NAME").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_FAT").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_SNF").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_FATKG").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_SNFKG").Name)

            If rbtnBMC.Checked = True Then
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_Gaze_Reading").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_Gaze_Qty").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_Sample_No").Name)
            ElseIf rbtnDCS.Checked Then
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_VLC_Code_VLC_Uploader").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_VLC_Code").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_VLC_Name").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_Shift").Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_OwnBMC").Name)
            End If
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_Route_Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_Vehicle_No").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Erp_Trip_No").Name)

            If rbtnBMC.Checked = True Then
                view.ColumnGroups.Add(New GridViewColumnGroup("Mobile VS ERP BMC Data"))
            ElseIf rbtnDCS.Checked Then
                view.ColumnGroups.Add(New GridViewColumnGroup("Mobile VS ERP DCS Data"))
            End If
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            If rbtnBMC.Checked = True Then
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_Gaze_Reading").Name)
            End If
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FAT").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNF").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_FATKG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("Diff_SNFKG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())

            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("Status").Name)

            Gv1.ViewDefinition = view
        End If
    End Sub
    Function LoadDiffData() As String
        Dim qry As String = ReturnQry()
        If rbtnBMC.Checked Then
            qry = " select max(case when source = 'Mobile' then Date else '' end) as Date,max(case when source = 'Mobile' then Mcc_Code_VLC_Uploader else '' end) as Mcc_Code_VLC_Uploader,max(case when source = 'Mobile' then MCC_Code else '' end) as MCC_Code,max(case when source = 'Mobile' then MCC_NAME else '' end) as MCC_NAME,max(case when source = 'Mobile' then Gaze_Reading else 0 end) as Gaze_Reading,max(case when source = 'Mobile' then Sample_No else null end) as Sample_No, max(case when source = 'Mobile' then Route_Code else '' end) as Route_Code,max(case when source = 'Mobile' then Vehicle_No else '' end) as Vehicle_No,max(case when source = 'Mobile' then Trip_No else null end) as Trip_No,sum(case when source = 'Mobile' then Gaze_Qty else 0 end) as Gaze_Qty,sum(case when source = 'Mobile' then qty else 0 end) as Qty,Convert(decimal(18,2),sum(case when source = 'Mobile' then (FATKG/ qty)*100 else 0 end)) as FAT,
		 Convert(decimal(18,2), sum(case when source = 'Mobile' then (SNFKG/qty)*100 else 0 end)) as SNF,sum(case when source = 'Mobile' then FATKG else 0 end) as FATKG,sum(case when source = 'Mobile' then SNFKG else 0 end) as SNFKG,max(case when source = 'Erp' then Date else '' end) as Erp_Date,max(case when source = 'Erp' then Mcc_Code_VLC_Uploader else '' end) as Erp_Mcc_Code_VLC_Uploader,max(case when source = 'Erp' then MCC_Code else '' end) as Erp_MCC_Code,max(case when source = 'Erp' then MCC_NAME else '' end) as Erp_MCC_NAME,max(case when source = 'Erp' then Gaze_Reading else 0 end) as Erp_Gaze_Reading,max(case when source = 'Erp' then Sample_No else null end) as Erp_Sample_No, max(case when source = 'Erp' then Route_Code else '' end) as Erp_Route_Code,max(case when source = 'Erp' then Vehicle_No else '' end) as Erp_Vehicle_No, max(case when source = 'Erp' then Trip_No else null end) as Erp_Trip_No,
		 sum(case when source = 'Erp' then Gaze_Qty else 0 end) as Erp_Gaze_Qty,sum(case when source = 'Erp' then qty else 0 end) as Erp_Qty,Convert(decimal(18,2),sum(case when source = 'Erp' then (FATKG/ qty)*100 else 0 end)) as Erp_FAT,Convert(decimal(18,2), sum(case when source = 'Erp' then (SNFKG/qty)*100 else 0 end)) as Erp_SNF,sum(case when source = 'Erp' then FATKG else 0 end) as Erp_FATKG,sum(case when source = 'Erp' then SNFKG else 0 end) as Erp_SNFKG, max(case when source = 'Mobile' then Gaze_Reading else 0 end) - max(case when source = 'Erp' then Gaze_Reading else 0 end)  as Diff_Gaze_Reading,sum(case when source = 'Mobile' then Qty else 0 end) - sum(case when source = 'Erp' then Qty else 0 end)  as Diff_Qty,Convert(decimal(18,2),sum(case when source = 'Mobile' then (FATKG/QTY)*100 else 0 end) - sum(case when source = 'Erp' then (FATKG/QTY)*100 else 0 end))  as Diff_FAT,Convert(decimal(18,2),sum(case when source = 'Mobile' then (SNFKG/QTY)*100 else 0 end) - sum(case when source = 'Erp' then (SNFKG/QTY)*100 else 0 end))  as Diff_SNF,sum(case when source = 'Mobile' then FATKG else 0 end) - sum(case when source = 'Erp' then FATKG else 0 end)  as Diff_FATKG,
	   	 sum(case when source = 'Mobile' then SNFKG else 0 end) - sum(case when source = 'Erp' then SNFKG else 0 end)  as Diff_SNFKG	,max(Status)Status from ( select convert(varchar,IDate,103)Date,Mcc_Code_VLC_Uploader,MCC_Code, (MCC_NAME)MCC_NAME, Gaze_Reading,Sample_No,Route_Code,Vehicle_No, Trip_No, 0 as Gaze_Qty,Qty, FAT, SNF,FATKG,SNFKG,'' as Status,'Mobile' as Source from  ( " + qry + "  " + Environment.NewLine + " union all " + Environment.NewLine + " select convert(varchar,Document_Date,103)Date,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader ,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,Gaze_Reading,Sample_No,TSPL_MILK_COLLECTION_MCC.Route_Code ,TSPL_MILK_COLLECTION_MCC.Vehicle_No ,TSPL_MILK_COLLECTION_MCC.Trip_No  ,isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.Gaze_Qty,0) as  Gaze_Qty,isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,0) as  Qty,TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as  FAT,TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as  SNF,TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as  FATKG,TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG ,case when TSPL_MILK_COLLECTION_MCC.Status = 1 then 'Approved' when TSPL_MILK_COLLECTION_MCC.Status = 0 then 'Pending' else '' end as Status,'Erp' as Source from TSPL_MILK_COLLECTION_MCC_DETAIL left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No  Left Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code where convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date ,103) >='" + clsCommon.GetPrintDate(fromDate.Value) + "'  and convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date ,103) <= '" + clsCommon.GetPrintDate(dtpToDate.Value) + "'  "

            If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_COLLECTION_MCC.Route_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
            End If
            qry += " ) xxx group by MCC_Code,Date,Trip_No,Sample_No "
        ElseIf rbtnDCS.Checked Then
            qry = "select max(case when source = 'Mobile' then Date else '' end) as Date,max(case when source = 'Mobile' then Mcc_Code_VLC_Uploader else '' end) as Mcc_Code_VLC_Uploader,max(case when source = 'Mobile' then MCC_Code else '' end) as MCC_Code,max(case when source = 'Mobile' then MCC_NAME else '' end) as MCC_NAME,max(case when source = 'Mobile' then VLC_Code_VLC_Uploader else '' end) as VLC_Code_VLC_Uploader,max(case when source = 'Mobile' then VLC_Code else '' end) as VLC_Code,max(case when source = 'Mobile' then VLC_Name else '' end) as VLC_Name,max(case when source = 'Mobile' then Shift else '' end) as Shift, max(case when source = 'Mobile' then Route_Code else '' end) as Route_Code,max(case when source = 'Mobile' then Vehicle_No else '' end) as Vehicle_No,max(case when source = 'Mobile' then Trip_No else null end) as Trip_No,sum(case when source = 'Mobile' then qty else 0 end) as Qty,Convert(decimal(18,2),sum(case when source = 'Mobile' then (FATKG/ qty)*100 else 0 end)) as FAT,
		Convert(decimal(18,2), sum(case when source = 'Mobile' then (SNFKG/qty)*100 else 0 end)) as SNF,sum(case when source = 'Mobile' then FATKG else 0 end) as FATKG,sum(case when source = 'Mobile' then SNFKG else 0 end) as SNFKG,max(case when source = 'Mobile' then [Own BMC] else '' end) as [Own BMC], max(case when source = 'Erp' then Date else '' end) as Erp_Date,max(case when source = 'Erp' then Mcc_Code_VLC_Uploader else '' end) as Erp_Mcc_Code_VLC_Uploader,max(case when source = 'Erp' then MCC_Code else '' end) as Erp_MCC_Code,max(case when source = 'Erp' then MCC_NAME else '' end) as Erp_MCC_NAME,max(case when source = 'Erp' then VLC_Code_VLC_Uploader else '' end) as Erp_VLC_Code_VLC_Uploader, max(case when source = 'Erp' then VLC_Code else '' end) as Erp_VLC_Code,max(case when source = 'Erp' then VLC_Name else '' end) as Erp_VLC_Name,max(case when source = 'Erp' then Shift else '' end) as Erp_Shift, max(case when source = 'Erp' then Route_Code else '' end) as Erp_Route_Code,max(case when source = 'Erp' then Vehicle_No else '' end) as Erp_Vehicle_No, max(case when source = 'Erp' then Trip_No else null end) as Erp_Trip_No,sum(case when source = 'Erp' then qty else 0 end) as Erp_Qty,
        Convert(decimal(18,2),sum(case when source = 'Erp' then (FATKG/ qty)*100 else 0 end)) as Erp_FAT,Convert(decimal(18,2), sum(case when source = 'Erp' then (SNFKG/qty)*100 else 0 end)) as Erp_SNF,sum(case when source = 'Erp' then FATKG else 0 end) as Erp_FATKG,sum(case when source = 'Erp' then SNFKG else 0 end) as Erp_SNFKG,max(case when source = 'Erp' then [Own BMC] else '' end) as Erp_OwnBMC,sum(case when source = 'Mobile' then Qty else 0 end) - sum(case when source = 'Erp' then Qty else 0 end)  as Diff_Qty,Convert(decimal(18,2),sum(case when source = 'Mobile' then (FATKG/QTY)*100 else 0 end) - sum(case when source = 'Erp' then (FATKG/QTY)*100 else 0 end))  as Diff_FAT,Convert(decimal(18,2),sum(case when source = 'Mobile' then (SNFKG/QTY)*100 else 0 end) - sum(case when source = 'Erp' then (SNFKG/QTY)*100 else 0 end))  as Diff_SNF,sum(case when source = 'Mobile' then FATKG else 0 end) - sum(case when source = 'Erp' then FATKG else 0 end)  as Diff_FATKG,sum(case when source = 'Mobile' then SNFKG else 0 end) - sum(case when source = 'Erp' then SNFKG else 0 end)  as Diff_SNFKG, max(Status)Status from (   select convert(varchar,IDate,103)Date,Mcc_Code_VLC_Uploader,MCC_Code, (MCC_NAME)MCC_NAME, Trip_No,VLC_Code_VLC_Uploader,VLC_Code,VLC_Name,IShift AS Shift, (Qty)Qty, FAT, SNF, (FATKG)FATKG, (SNFKG)SNFKG,(Route_Code)Route_Code, (Vehicle_No)Vehicle_No,[Own BMC],''AS Status,[Mobile User],'Mobile' as Source from ( " + qry + "  " + Environment.NewLine + " union all " + Environment.NewLine + "  select convert(varchar,TSPL_MILK_COLLECTION_DCS.Document_Date,103) Date,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader ,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code ,TSPL_MCC_MASTER.MCC_NAME ,TSPL_MILK_COLLECTION_MCC.Trip_No as  Trip_No,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MILK_COLLECTION_DCS_DETAIL.Shift  ,isnull(TSPL_MILK_COLLECTION_DCS_DETAIL.Qty,0) as  Qty,TSPL_MILK_COLLECTION_DCS_DETAIL.FAT,TSPL_MILK_COLLECTION_DCS_DETAIL.SNF,
            TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG ,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG ,TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC.Vehicle_No ,case when Mcc_Code_VLC_Uploader = VLC_Code_VLC_Uploader then 'Yes' else 'No' end as  [Own BMC],case when TSPL_MILK_COLLECTION_DCS.Status = 1 then 'Approved' when TSPL_MILK_COLLECTION_DCS.Status = 0 then 'Pending' else '' end as Status,'' as [Mobile User],'Erp' as Source from TSPL_MILK_COLLECTION_DCS_DETAIL left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No left join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No
            left join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id = TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No Left Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL.VLC_Code  where convert(date,TSPL_MILK_COLLECTION_DCS.Document_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and convert(date,TSPL_MILK_COLLECTION_DCS.Document_Date,103) <= '" + clsCommon.GetPrintDate(dtpToDate.Value) + "' "
            If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_COLLECTION_MCC.Route_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
            End If
            qry += " ) xxx group by VLC_Code,Date,VLC_Code,Shift ORDER BY SHIFT DESC"
        End If
        Return qry
    End Function
    Private Sub txtDCS__My_Click(sender As Object, e As EventArgs) Handles txtDCS._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as [DCS Code],TSPL_VLC_MASTER_HEAD.VLC_Name AS [DCS Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.Route_Code AS[Route Code] ,TSPL_MCC_ROUTE_MASTER.Route_Name AS [Route Name] from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1' "
            'If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            'End If

            txtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "DCS Code", "DCS Name", txtDCS.arrValueMember, Nothing)
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
                Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "BMC Uploader"
                Gv1.Columns("Sample_No").HeaderText = "Sample No"
                Gv1.Columns("Route_Code").HeaderText = "Route Code"
                Gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"
                Gv1.Columns("Milk_Not_Picked").HeaderText = "Milk Not Picked"
                Gv1.Columns("MCC").IsVisible = False
            End If
            If rbtnDCS.Checked Then
                Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "MCC Uploader"
                Gv1.Columns("VLC_Code").HeaderText = "DCS Code"
                Gv1.Columns("VLC_Name").HeaderText = "DCS Name"
                Gv1.Columns("IShift").HeaderText = "Shift"
                Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Uploader"
                Gv1.Columns("Route_code").IsVisible = False
                Gv1.Columns("Vehicle_No").IsVisible = False
                Gv1.Columns("VLC").IsVisible = False
            End If
            Gv1.Columns("Avg_Qty").HeaderText = "Average Qty"
            Gv1.Columns("Average_Qty").IsVisible = False
            Gv1.Columns("Avg_Qty").FormatString = "{0:n2}"
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


        Dim Avg_Qty As New GridViewSummaryItem("Avg_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Avg_Qty)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Sub FormatDiffDataGrid()
        Dim summaryItem As New GridViewSummaryItem()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns("MCC_Code").HeaderText = "BMC Code"
            Gv1.Columns("Erp_MCC_Code").HeaderText = "BMC Code"
            Gv1.Columns("MCC_NAME").HeaderText = "BMC NAME"
            Gv1.Columns("Erp_MCC_NAME").HeaderText = "BMC NAME"
            Gv1.Columns("Trip_No").HeaderText = "Trip No"
            Gv1.Columns("Erp_Trip_No").HeaderText = "Trip No"
            Gv1.Columns("Trip_No").FormatString = "{0}"
            Gv1.Columns("Erp_Trip_No").FormatString = "{0}"
            Gv1.Columns("Erp_Date").HeaderText = "Date"
            Gv1.Columns("Route_Code").HeaderText = "Route Code"
            Gv1.Columns("Erp_Route_Code").HeaderText = "Route Code"
            Gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"
            Gv1.Columns("Erp_Vehicle_No").HeaderText = "Vehicle No"

            If rbtnBMC.Checked Then
                Gv1.Columns("Sample_No").HeaderText = "Sample No"
                Gv1.Columns("Erp_Sample_No").HeaderText = "Sample No"
                Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "BMC Uploader"
                Gv1.Columns("Erp_Mcc_Code_VLC_Uploader").HeaderText = "BMC Uploader"
                Gv1.Columns("Gaze_Reading").HeaderText = "Gaze Reading"
                Gv1.Columns("Gaze_Reading").FormatString = "{0}"
                Gv1.Columns("Erp_Gaze_Reading").HeaderText = "Gaze Reading"
                Gv1.Columns("Erp_Gaze_Reading").FormatString = "{0}"
                Gv1.Columns("Erp_Gaze_Qty").HeaderText = "Qty"
                Gv1.Columns("Erp_Gaze_Qty").FormatString = "{0:n2}"
                Gv1.Columns("Diff_Gaze_Reading").HeaderText = "Gaze Reading"
                Gv1.Columns("Diff_Gaze_Reading").FormatString = "{0:n2}"
                Gv1.Columns("Erp_Qty").HeaderText = "Qty(KG)"
            End If
            If rbtnDCS.Checked Then
                Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "MCC Uploader"
                Gv1.Columns("VLC_Code").HeaderText = "DCS Code"
                Gv1.Columns("VLC_Name").HeaderText = "DCS Name"
                Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Uploader"
                Gv1.Columns("Erp_Mcc_Code_VLC_Uploader").HeaderText = "MCC Uploader"
                Gv1.Columns("Erp_VLC_Code").HeaderText = "DCS Code"
                Gv1.Columns("Erp_VLC_Name").HeaderText = "DCS Name"
                Gv1.Columns("Erp_Shift").HeaderText = "Shift"
                Gv1.Columns("Erp_VLC_Code_VLC_Uploader").HeaderText = "DCS Uploader"
                Gv1.Columns("Erp_OwnBMC").HeaderText = "Own BMC"
                Gv1.Columns("Erp_Qty").HeaderText = "Qty"
            End If

            Gv1.Columns("Qty").HeaderText = "Qty"
            Gv1.Columns("Qty").FormatString = "{0:n2}"
            Gv1.Columns("Erp_Qty").FormatString = "{0:n2}"
            Gv1.Columns("FAT").HeaderText = "FAT%"
            Gv1.Columns("FAT").FormatString = "{0:n2}"
            Gv1.Columns("Erp_FAT").HeaderText = "FAT%"
            Gv1.Columns("Erp_FAT").FormatString = "{0:n2}"
            Gv1.Columns("SNF").HeaderText = "SNF%"
            Gv1.Columns("SNF").FormatString = "{0:n2}"
            Gv1.Columns("Erp_SNF").HeaderText = "SNF%"
            Gv1.Columns("Erp_SNF").FormatString = "{0:n2}"
            Gv1.Columns("FATKG").HeaderText = "FAT KG"
            Gv1.Columns("FATKG").FormatString = "{0:n2}"
            Gv1.Columns("SNFKG").HeaderText = "SNF KG"
            Gv1.Columns("SNFKG").FormatString = "{0:n2}"
            Gv1.Columns("Erp_FATKG").HeaderText = "FAT KG"
            Gv1.Columns("Erp_FATKG").FormatString = "{0:n2}"
            Gv1.Columns("Erp_SNFKG").HeaderText = "SNF KG"
            Gv1.Columns("Erp_SNFKG").FormatString = "{0:n2}"
            Gv1.Columns("Diff_Qty").HeaderText = "Qty"
            Gv1.Columns("Diff_Qty").FormatString = "{0:n2}"


            Gv1.Columns("Diff_FATKG").HeaderText = "FAT KG"
            Gv1.Columns("Diff_FATKG").FormatString = "{0:n2}"
            Gv1.Columns("Diff_SNFKG").HeaderText = "SNF KG"
            Gv1.Columns("Diff_SNFKG").FormatString = "{0:n2}"

            Gv1.Columns("Diff_FAT").HeaderText = "FAT%"
            Gv1.Columns("Diff_FAT").FormatString = "{0:n2}"
            Gv1.Columns("Diff_SNF").HeaderText = "SNF%"
            Gv1.Columns("Diff_SNF").FormatString = "{0:n2}"
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim Qty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Qty)
        Dim Erp_Qty As New GridViewSummaryItem("Erp_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Erp_Qty)

        Dim Diff_Qty As New GridViewSummaryItem("Diff_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Diff_Qty)

        Dim Erp_Gaze_Qty As New GridViewSummaryItem("Erp_Gaze_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Erp_Gaze_Qty)

        If rbtnDCS.Checked Then
            Dim Trip As New GridViewSummaryItem("Trip_No", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Trip)
        End If
        Dim FAT_PER As New GridViewSummaryItem()
        FAT_PER.FormatString = "{0:F2}"
        FAT_PER.Name = "FAT"
        FAT_PER.AggregateExpression = "sum(FATKG)*100/sum(Qty)"
        summaryRowItem.Add(FAT_PER)

        FAT_PER = New GridViewSummaryItem()
        FAT_PER.FormatString = "{0:F2}"
        FAT_PER.Name = "Erp_FAT"
        FAT_PER.AggregateExpression = "sum(Erp_FATKG)*100/sum(Erp_Qty)"
        summaryRowItem.Add(FAT_PER)

        Dim SNF_PER As New GridViewSummaryItem()
        SNF_PER.FormatString = "{0:F2}"
        SNF_PER.Name = "SNF"
        SNF_PER.AggregateExpression = "sum(SNFKG)*100/sum(Qty)"
        summaryRowItem.Add(SNF_PER)


        SNF_PER = New GridViewSummaryItem()
        SNF_PER.FormatString = "{0:F2}"
        SNF_PER.Name = "Erp_SNF"
        SNF_PER.AggregateExpression = "sum(Erp_SNFKG)*100/sum(Erp_Qty)"
        summaryRowItem.Add(SNF_PER)

        Dim FATKG As New GridViewSummaryItem("FATKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(FATKG)
        Dim SNFKG As New GridViewSummaryItem("SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SNFKG)

        Dim Erp_FATKG As New GridViewSummaryItem("Erp_FATKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Erp_FATKG)
        Dim Erp_SNFKG As New GridViewSummaryItem("Erp_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Erp_SNFKG)

        Dim Diff_FATKG As New GridViewSummaryItem("Diff_FATKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Diff_FATKG)
        Dim Diff_SNFKG As New GridViewSummaryItem("Diff_SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Diff_SNFKG)

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    '    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
    '        Try


    '            Dim strQry As String = "select TSPL_MILK_COLLECTION_BMCDCS.PK_ID,TSPL_MILK_COLLECTION_BMCDCS.IDate,TSPL_MILK_COLLECTION_BMCDCS.Truck_Sheet_SNo,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No,TSPL_MILK_COLLECTION_BMCDCS.Arrive_Time,TSPL_MILK_COLLECTION_BMCDCS.Dispatch_Time,TSPL_MILK_COLLECTION_BMCDCS.Last_BMC_Seal_No,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Silo_Capacity,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Gaze_Reading,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Qty,TSPL_MILK_COLLECTION_BMCDCS_TRIP.FAT,TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNF,TSPL_MILK_COLLECTION_BMCDCS_TRIP.FATKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNFKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Temp
    'from TSPL_MILK_COLLECTION_BMCDCS
    'left join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
    'left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code where TSPL_MILK_COLLECTION_BMCDCS.PK_ID='" + clsCommon.myCstr(Gv1.CurrentRow.Cells(1).Value) + "'"
    '            Dim strQryDCS As String = "select TSPL_MILK_COLLECTION_BMCDCS.PK_ID,TSPL_MILK_COLLECTION_BMCDCS.IDate,TSPL_MILK_COLLECTION_BMCDCS.Truck_Sheet_SNo,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No,
    'TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MILK_COLLECTION_BMCDCS_DCS.IShift,TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty,TSPL_MILK_COLLECTION_BMCDCS_DCS.FAT,TSPL_MILK_COLLECTION_BMCDCS_DCS.SNF,TSPL_MILK_COLLECTION_BMCDCS_DCS.FATKG,TSPL_MILK_COLLECTION_BMCDCS_DCS.SNFKG
    'from TSPL_MILK_COLLECTION_BMCDCS
    'left join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
    'left join TSPL_MILK_COLLECTION_BMCDCS_DCS on TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
    'left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code
    'left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code where TSPL_MILK_COLLECTION_BMCDCS.PK_ID='" + clsCommon.myCstr(Gv1.CurrentRow.Cells(1).Value) + "'"
    '            Dim dtdcs As DataTable = clsDBFuncationality.GetDataTable(strQry)

    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)

    '            If dt IsNot Nothing And dt.Rows.Count > 0 Then
    '                Dim frmCRV As New frmCrystalReportViewer()
    '                frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, dtdcs, "crptMobileAppMilkCollectionReport", "", "crptDCSMilkcollectionReport.rpt")
    '                frmCRV = Nothing
    '            Else
    '                clsCommon.MyMessageBoxShow(Me, "No Record Found!", Me.Text)

    '            End If


    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") <> clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") Then
                Throw New Exception("Only single Date allowed.")
                Exit Sub
            End If

            Dim qry As String = ReturnQry()
            Dim dt As DataTable
            Dim dtSubBMCTankerEntryDetails As DataTable = Nothing
            Dim dtSubOwnBMCMilk As DataTable = Nothing
            Dim dtSubBMCLocalSale As DataTable = Nothing
            If rbtnBMC.Checked Then
                qry = " select  xx.*,TSPL_COMPANY_MASTER.Comp_Name,convert(varchar,IDate,103) as Date,'" + objCommonVar.CurrentUser + "' as UserName  from ( " + qry + "  )xx 	left join TSPL_COMPANY_MASTER on 1 = 1"
                dt = clsDBFuncationality.GetDataTable(qry)
            ElseIf rbtnDCS.Checked Then
                qry = " select  xx.*,TSPL_COMPANY_MASTER.Comp_Name,convert(varchar,IDate,103) as Date,'" + objCommonVar.CurrentUser + "' as UserName from ( " + qry + "  )xx 	left join TSPL_COMPANY_MASTER on 1 = 1"
                dt = clsDBFuncationality.GetDataTable(qry)
                qry = "select Row_Number() Over (Order By (SELECT 1) Asc) as [S No],(TSPL_MILK_COLLECTION_BMCDCS.PK_ID)PK_ID, (TSPL_MILK_COLLECTION_BMCDCS.IDate)IDate,(TSPL_MILK_COLLECTION_BMCDCS.Truck_Sheet_SNo)Truck_Sheet_SNo,
                        TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,
                        (TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code)Route_Code,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No)Trip_No,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Silo_Capacity)Silo_Capacity,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Gaze_Reading)Gaze_Reading,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Qty)Qty,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.FAT)FAT,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNF)SNF,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.FATKG)FATKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Sample_No,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNFKG)SNFKG,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Temp)Temp, case when isnull(Milk_Not_Picked,0) = 1 then 'Yes' else 'No' end as Milk_Not_Picked
                        From TSPL_MILK_COLLECTION_BMCDCS
                        left Join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
                        Left Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code   
                        where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'"

                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    qry += " and TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                End If
                If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
                    qry += " and TSPL_MILK_COLLECTION_BMCDCS.MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
                End If
                dtSubBMCTankerEntryDetails = clsDBFuncationality.GetDataTable(qry)
                qry = "select isnull(Own_BMC_Qty,0) as Qty,Own_BMC_FAT AS FAT,Own_BMC_SNF as SNF,'Evening' as Shift,MCC_Code  from TSPL_MILK_COLLECTION_BMCDCS  where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
         union all
        select isnull(Own_BMC_Qty,0) as Qty,Own_BMC_FAT_E as FAT,Own_BMC_SNF_E as SNF,'Morning' as Shift,MCC_Code from TSPL_MILK_COLLECTION_BMCDCS  where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'"

                dtSubOwnBMCMilk = clsDBFuncationality.GetDataTable(qry)
                qry = "select isnull(Own_BMC_Loose_Sale_Qty,0) as Qty,'Evening' as Shift,MCC_Code  from TSPL_MILK_COLLECTION_BMCDCS  where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
         union all
        select isnull(Own_BMC_Loose_Sale_Qty_E,0) as Qty,'Morning' as Shift,MCC_Code from TSPL_MILK_COLLECTION_BMCDCS  where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'"
                dtSubBMCLocalSale = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If rbtnBMC.Checked Then
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBMCTruckSheet", "")
                ElseIf rbtnDCS.Checked Then
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtSubBMCTankerEntryDetails, "rptDCSTruckSheet", "", "rptSubBMCTankerEnterDetails", "rptSubOwnBMCMilkCollectionDetails", dtSubOwnBMCMilk, "rptSubBMCLocalSaleDetails", dtSubBMCLocalSale)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function ReturnQry() As String
        Dim strQry As String = ""
        If rbtnBMC.Checked Then
            strQry = "  select Row_Number() Over (Order By (SELECT 1) Asc) as [S No],(TSPL_MILK_COLLECTION_BMCDCS.PK_ID)PK_ID, (TSPL_MILK_COLLECTION_BMCDCS.IDate)IDate,(TSPL_MILK_COLLECTION_BMCDCS.Truck_Sheet_SNo)Truck_Sheet_SNo,
                        TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,
                        (TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code)Route_Code,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No)Trip_No,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Silo_Capacity)Silo_Capacity,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Gaze_Reading)Gaze_Reading,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Qty)Qty,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.FAT)FAT,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNF)SNF,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.FATKG)FATKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Sample_No,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNFKG)SNFKG,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Temp)Temp, case when isnull(Milk_Not_Picked,0) = 1 then 'Yes' else 'No' end as Milk_Not_Picked,TSPL_MILK_COLLECTION_BMCDCS.Created_By as [Mobile User]
                        From TSPL_MILK_COLLECTION_BMCDCS
                        left Join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
                        Left Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code   
                        where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'"
        End If
        If rbtnDCS.Checked Then
            strQry = " select Row_Number() Over (Order By (SELECT 1) Asc) as [S No],(TSPL_MILK_COLLECTION_BMCDCS.PK_ID)PK_ID,(TSPL_MILK_COLLECTION_BMCDCS.IDate)IDate,(TSPL_MILK_COLLECTION_BMCDCS.Truck_Sheet_SNo)Truck_Sheet_SNo,(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,Trip_No=1
                        ,(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader)VLC_Code_VLC_Uploader,
                        (TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code)VLC_Code,(TSPL_VLC_MASTER_HEAD.VLC_Name)VLC_Name,(TSPL_MILK_COLLECTION_BMCDCS_DCS.IShift)IShift,(TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty)Qty,(TSPL_MILK_COLLECTION_BMCDCS_DCS.FAT)FAT,(TSPL_MILK_COLLECTION_BMCDCS_DCS.SNF)SNF,(TSPL_MILK_COLLECTION_BMCDCS_DCS.FATKG)FATKG,(TSPL_MILK_COLLECTION_BMCDCS_DCS.SNFKG)SNFKG
                   , ( SELECT DISTINCT  STRING_AGG( Route_Code, '/') AS Route_Code from ( select distinct Route_Code,MCC_Code   from ( select TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,TabInnerTrip.Vehicle_No,TabInnerTrip.Route_Code
                        from TSPL_MILK_COLLECTION_BMCDCS as TabInnerBMCDCS left Join TSPL_MILK_COLLECTION_BMCDCS_TRIP as TabInnerTrip on TabInnerTrip.REF_PK_ID=TabInnerBMCDCS.PK_ID
                where TabInnerBMCDCS.IDate  >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TabInnerBMCDCS.IDate <= '" + clsCommon.GetPrintDate(dtpToDate.Value) + "' and TabInnerBMCDCS.MCC_Code = TSPL_MILK_COLLECTION_BMCDCS.MCC_Code ) xx )xxx  ) as Route_code,
					  	( SELECT DISTINCT  STRING_AGG( Vehicle_No, '/') AS Vehicle_No from ( select distinct Vehicle_No,MCC_Code   from ( select TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,TabInnerTrip.Vehicle_No,TabInnerTrip.Route_Code
                        from TSPL_MILK_COLLECTION_BMCDCS as TabInnerBMCDCS
                       left Join TSPL_MILK_COLLECTION_BMCDCS_TRIP as TabInnerTrip on TabInnerTrip.REF_PK_ID=TabInnerBMCDCS.PK_ID
                where TabInnerBMCDCS.IDate  >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TabInnerBMCDCS.IDate <= '" + clsCommon.GetPrintDate(dtpToDate.Value) + "' and TabInnerBMCDCS.MCC_Code = TSPL_MILK_COLLECTION_BMCDCS.MCC_Code
                      ) xx )xxx  ) as Vehicle_No,case when Mcc_Code_VLC_Uploader = VLC_Code_VLC_Uploader then 'Yes' else 'No' end as [Own BMC],TSPL_MILK_COLLECTION_BMCDCS.Created_By as [Mobile User]
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
        strQry += " )xx "
        Return strQry
    End Function

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            Dim PK_ID As Integer = clsCommon.myCstr(Gv1.CurrentRow.Cells("PK_ID").Value)
            Dim dt As DataTable = LoadHistoryData(PK_ID)
                If dt.Rows.Count > 0 And dt IsNot Nothing Then
                    Dim ff As New FrmFreeGrid
                    ff.ReportID = MyBase.Form_ID
                    ff.Text = Me.Text
                    ff.dt = dt
                    ff.ShowDialog()
                Else
                    Throw New Exception("No history found.")
                End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function LoadHistoryData(ByVal PK_ID As Integer) As DataTable
        Dim qry As String = ""

        If rbtnBMC.Checked Then
            qry = "WITH VersionsData AS ( SELECT TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.SNo,TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.Hist_Version as [Hist Version],TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.Hist_By as [Hist By],TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.Hist_On as [Hist On],TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.Document_No as [Document No],convert(varchar, TSPL_MILK_COLLECTION_MCC_Hist_Data.Document_date,103)[Document Date],TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.MCC_Code as [BMC Code],Mcc_Code_VLC_Uploader as [MCC Uploader code],MCC_NAME as [BMC Name],TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.PK_Id,TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.Sample_No as [Sample No],TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.Qty,TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.FAT,TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.SNF,TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.FATKG,TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.SNFKG,CASE WHEN REF_PK_ID_BMCDCS_TRIP IS NULL THEN 'ERP' ELSE 'Mobile' END AS [Source Type],TSPL_MILK_COLLECTION_MCC_Hist_Data.operation_type AS Operation,ROW_NUMBER() OVER (PARTITION BY TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.Document_No, TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.SNo ORDER BY TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.Hist_Version asc) AS rownum   FROM TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data
           Left Join TSPL_MILK_COLLECTION_MCC_Hist_Data ON TSPL_MILK_COLLECTION_MCC_Hist_Data.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.Document_No And TSPL_MILK_COLLECTION_MCC_Hist_Data.Hist_Version = TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.Hist_Version Left Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.MCC_Code LEFT JOIN TSPL_MILK_COLLECTION_BMCDCS_TRIP ON TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID = TSPL_MILK_COLLECTION_MCC_DETAIL_Hist_Data.REF_PK_ID_BMCDCS_TRIP   WHERE TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID =" + clsCommon.myCstr(PK_ID) + " ),
      ChangedData AS ( SELECT ChangedData.SNo,ChangedData.[Hist Version],ChangedData.[Hist By],ChangedData.[Hist On],ChangedData.[Document No],ChangedData.[Document Date],ChangedData.[MCC Uploader code],ChangedData.[BMC Code],ChangedData.[BMC Name],ChangedData.PK_Id,ChangedData.[Sample No],ChangedData.Qty,ChangedData.FAT,ChangedData.SNF,ChangedData.FATKG,ChangedData.SNFKG,ChangedData.[Source Type],ChangedData.Operation,ChangedData.rownum,CASE WHEN ChangedData.Qty != NextVersionDa.Qty OR ChangedData.FAT != NextVersionDa.FAT OR ChangedData.SNF != NextVersionDa.SNF THEN 1 ELSE 0 END AS DataChanged FROM VersionsData ChangedData LEFT JOIN VersionsData NextVersionDa ON ChangedData.SNo = NextVersionDa.SNo AND ChangedData.[Document No] =NextVersionDa.[Document No] AND ChangedData.[Hist Version] = NextVersionDa.[Hist Version] + 1 ), FinalResults AS ( SELECT SNo,[Hist Version],[Hist By],[Hist On],[Document No],[Document Date],[MCC Uploader code],[BMC Code],[BMC Name],[Sample No],Qty,FAT,SNF,FATKG,SNFKG,CASE WHEN rownum = 1 THEN 'Mobile' WHEN DataChanged = 1 THEN 'Erp' ELSE 'Mobile' END AS [Source Type],Operation,rownum,DataChanged  FROM ChangedData WHERE rownum = 1  OR DataChanged = 1 ) SELECT SNo,[Hist Version],[Hist By],[Hist On], [Document No],[Document Date],[MCC Uploader code],[BMC Code],[BMC Name],[Sample No],Qty,FAT,SNF,FATKG,SNFKG,CASE WHEN rownum = 1 THEN 'Mobile' WHEN DataChanged = 1 THEN 'Erp' ELSE 'Mobile' END AS [Source Type],Operation FROM FinalResults ORDER BY SNo, [Hist Version] asc "
        ElseIf rbtnDCS.Checked Then
            qry = " WITH VersionsData AS ( SELECT TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.SNo,TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.Hist_Version as [Hist Version],TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.Hist_By as [Hist By],TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.Hist_On as [Hist On],TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.Document_No as [Document No],convert(varchar, TSPL_MILK_COLLECTION_DCS_Hist_Data.Document_date,103)[Document Date],TSPL_MILK_COLLECTION_DCS_detail_Hist_Data.Shift,TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code as [BMC Code],Mcc_Code_VLC_Uploader as [BMC Uploader code],MCC_NAME as [BMC Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader AS [DCS Uploader Code],TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.VLC_Code as [DCS Code],VLC_Name as [DCS Name],TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.PK_Id,TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.Qty,TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.FAT,TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.SNF,TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.FATKG,TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.SNFKG,CASE WHEN REF_PK_ID_BMCDCS_TRIP IS NULL THEN 'ERP' ELSE 'Mobile' END AS [Source Type],TSPL_MILK_COLLECTION_DCS_Hist_Data.operation_type AS Operation,ROW_NUMBER() OVER (PARTITION BY TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.Document_No, TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.SNo ORDER BY TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.Hist_Version asc) AS rownum 
            from TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data left outer join TSPL_MILK_COLLECTION_DCS_Hist_Data on TSPL_MILK_COLLECTION_DCS_Hist_Data.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.Document_No and TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.Hist_Version = TSPL_MILK_COLLECTION_DCS_Hist_Data.Hist_Version left join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.Document_No left join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id = TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No  Left Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_DCS_DETAIL_Hist_Data.VLC_Code 
			LEFT JOIN TSPL_MILK_COLLECTION_BMCDCS_TRIP ON TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID = TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP  WHERE TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID =" + clsCommon.myCstr(PK_ID) + " ),
            ChangedData AS  ( SELECT ChangedData.SNo,ChangedData.[Hist Version],ChangedData.[Hist By],ChangedData.[Hist On],ChangedData.[Document No],ChangedData.[Document Date],ChangedData.Shift,ChangedData.[MCC Uploader code],ChangedData.[BMC Code],ChangedData.[BMC Name],ChangedData.[DCS Uploader Code],ChangedData.[DCS Code],ChangedData.[DCS Name],ChangedData.PK_Id,ChangedData.Qty,ChangedData.FAT,ChangedData.SNF,ChangedData.FATKG,ChangedData.SNFKG,ChangedData.[Source Type],ChangedData.Operation,ChangedData.rownum,CASE WHEN ChangedData.Qty != NextVersionDa.Qty OR ChangedData.FAT != NextVersionDa.FAT OR ChangedData.SNF != NextVersionDa.SNF THEN 1 ELSE 0 END AS DataChanged FROM VersionsData ChangedData LEFT JOIN VersionsData NextVersionDa ON ChangedData.SNo = NextVersionDa.SNo AND ChangedData.[Document No] =NextVersionDa.[Document No] AND ChangedData.[Hist Version] = NextVersionDa.[Hist Version] + 1 ), FinalResults AS ( SELECT SNo,[Hist Version],[Hist By],[Hist On],[Document No],[Document Date],Shift,[MCC Uploader code],[BMC Code],[BMC Name],[DCS Uploader Code],[DCS Code],[DCS Name],Qty,FAT,SNF,FATKG,SNFKG,CASE WHEN rownum = 1 THEN 'Mobile' WHEN DataChanged = 1 THEN 'Erp' ELSE 'Mobile' END AS [Source Type],Operation,rownum,DataChanged  FROM ChangedData WHERE rownum = 1  OR DataChanged = 1 ) SELECT SNo,[Hist Version],[Hist By],[Hist On], [Document No],[Document Date],Shift,[MCC Uploader code],[BMC Code],[BMC Name],[DCS Uploader Code],[DCS Code],[DCS Name],Qty,FAT,SNF,FATKG,SNFKG,CASE WHEN rownum = 1 THEN 'Mobile' WHEN DataChanged = 1 THEN 'Erp' ELSE 'Mobile' END AS [Source Type],Operation FROM FinalResults ORDER BY SNo, [Hist Version] asc "
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
End Class
