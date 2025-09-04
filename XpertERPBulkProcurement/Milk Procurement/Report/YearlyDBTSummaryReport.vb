Imports common
Imports System.ComponentModel
Imports System.IO
Public Class YearlyDBTSummaryReport
    Inherits FrmMainTranScreen

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        LoadData()
    End Sub
    Private Sub LoadData()
        Try
            Dim qry As String = ""
            Dim Baseqry As String = ""
            Dim DBT_NEFT As String = ""
            txtFromDate.Enabled = False
            txtToDate.Enabled = False

            If rdbValid.Checked Then
                qry = "select  TSPL_MP_INCENTIVE_ENTRY_detail.mp_code AS [ERP Mp Code],TSPL_DBT_NEFT.Document_Date,TSPL_VLC_MASTER_HEAD.Vsp_code as [DCS CODE],
TSPL_VLC_MASTER_HEAD.vlc_name as [Dcs Name],TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code AS [Mp Code] ,TSPL_DBT_NEFT_DETAIL.MP_Name AS [MP Name],TSPL_DBT_NEFT_DETAIL.MP_Bank AS [MP Bank]
,TSPL_DBT_NEFT_DETAIL.MP_Account_No AS [Bank Account],TSPL_DBT_NEFT_DETAIL.MP_IFSC_No as[IFSC],
    SUM(TSPL_MP_INCENTIVE_ENTRY_detail.qty) AS [Quantity]
 from TSPL_DBT_NEFT_DETAIl 
left outer join TSPL_DBT_NEFT on TSPL_DBT_NEFT_DETAIL.Document_Code=TSPL_DBT_NEFT.Document_Code
left outer join TSPL_MP_INCENTIVE_ENTRY_detail on TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
left outer join TSPL_MP_INCENTIVE_ENTRY_head on TSPL_MP_INCENTIVE_ENTRY_head.Document_Code=TSPL_MP_INCENTIVE_ENTRY_detail.Document_Code
 left outer join TSPL_VLC_MASTER_HEAD on TSPL_MP_INCENTIVE_ENTRY_detail.vlc_code= TSPL_VLC_MASTER_HEAD.VLC_Code 
where convert(date,TSPL_MP_INCENTIVE_ENTRY_head.From_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)
GROUP BY  
TSPL_DBT_NEFT.Document_Date,
TSPL_MP_INCENTIVE_ENTRY_head.From_Date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,
TSPL_DBT_NEFT.Document_Date,
    TSPL_MP_INCENTIVE_ENTRY_detail.mp_code,
    TSPL_VLC_MASTER_HEAD.Vsp_code,
    TSPL_VLC_MASTER_HEAD.vlc_name,
    TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code,
    TSPL_DBT_NEFT_DETAIL.MP_Name,
    TSPL_DBT_NEFT_DETAIL.MP_Bank,
    TSPL_DBT_NEFT_DETAIL.MP_Account_No,
    TSPL_DBT_NEFT_DETAIL.MP_IFSC_No

"
            ElseIf rdbInValid.Checked Then
                qry = "select TSPL_MP_INCENTIVE_ENTRY_detail.mp_code AS [ERP Mp Code],TSPL_DBT_NEFT.Document_Date,  TSPL_VLC_MASTER_HEAD.Vsp_code as [DCS CODE],TSPL_VLC_MASTER_HEAD.vlc_name as [Dcs Name],TSPL_DBT_NEFT_DETAIL_invalid.MP_Uploader_Code AS [Mp Code],TSPL_DBT_NEFT_DETAIL_invalid.MP_Name AS [MP Name],TSPL_MP_INCENTIVE_ENTRY_detail.MP_Bank AS [MP Bank],
TSPL_DBT_NEFT_DETAIL_invalid.MP_Account_No AS [Bank Account],TSPL_DBT_NEFT_DETAIL_invalid.MP_IFSC_No as[IFSC],
    SUM(TSPL_MP_INCENTIVE_ENTRY_detail.qty) AS [Quantity]
from TSPL_DBT_NEFT_DETAIL_invalid
left outer join TSPL_DBT_NEFT on TSPL_DBT_NEFT_DETAIL_invalid.Document_Code=TSPL_DBT_NEFT.Document_Code
left outer join TSPL_MP_INCENTIVE_ENTRY_detail on TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=TSPL_DBT_NEFT_DETAIL_invalid.Against_MP_Incentive_TR
left outer join TSPL_MP_INCENTIVE_ENTRY_head on TSPL_MP_INCENTIVE_ENTRY_head.Document_Code=TSPL_MP_INCENTIVE_ENTRY_detail.Document_Code

 left outer join TSPL_VLC_MASTER_HEAD on TSPL_MP_INCENTIVE_ENTRY_detail.vlc_code= TSPL_VLC_MASTER_HEAD.VLC_Code
where convert(date,TSPL_MP_INCENTIVE_ENTRY_head.From_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)
GROUP BY  
TSPL_DBT_NEFT.Document_Date,
TSPL_MP_INCENTIVE_ENTRY_head.From_Date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,
TSPL_DBT_NEFT.Document_Date,
    TSPL_MP_INCENTIVE_ENTRY_detail.mp_code,
    TSPL_VLC_MASTER_HEAD.Vsp_code,
    TSPL_VLC_MASTER_HEAD.vlc_name,
   
    TSPL_DBT_NEFT_DETAIL_invalid.MP_Uploader_Code,
    TSPL_DBT_NEFT_DETAIL_invalid.MP_Name,
    TSPL_MP_INCENTIVE_ENTRY_detail.MP_Bank,

    TSPL_DBT_NEFT_DETAIL_invalid.MP_Account_No,
    TSPL_DBT_NEFT_DETAIL_invalid.MP_IFSC_No"

            ElseIf rblHold.Checked Then
                qry = "select TSPL_MP_INCENTIVE_ENTRY_detail.mp_code AS [ERP Mp Code],TSPL_DBT_NEFT.Document_Date,TSPL_VLC_MASTER_HEAD.Vsp_code as [DCS CODE],
TSPL_VLC_MASTER_HEAD.vlc_name as [Dcs Name],tspl_dbt_neft_detail_hold.MP_Uploader_Code AS [Mp Code],tspl_dbt_neft_detail_hold.MP_Name AS [MP Name],TSPL_MP_INCENTIVE_ENTRY_detail.MP_Bank AS [MP Bank],
tspl_dbt_neft_detail_hold.MP_Account_No AS [Bank Account],tspl_dbt_neft_detail_hold.MP_IFSC_No as[IFSC],
    SUM(TSPL_MP_INCENTIVE_ENTRY_detail.qty) AS [ Quantity]
from tspl_dbt_neft_detail_hold 
left outer join TSPL_DBT_NEFT on tspl_dbt_neft_detail_hold.Document_Code=tspl_dbt_neft_detail_hold.Document_Code
left outer join TSPL_MP_INCENTIVE_ENTRY_detail on TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=tspl_dbt_neft_detail_hold.Against_MP_Incentive_TR
left outer join TSPL_MP_INCENTIVE_ENTRY_head on TSPL_MP_INCENTIVE_ENTRY_head.Document_Code=TSPL_MP_INCENTIVE_ENTRY_detail.Document_Code

 left outer join TSPL_VLC_MASTER_HEAD on TSPL_MP_INCENTIVE_ENTRY_detail.vlc_code= TSPL_VLC_MASTER_HEAD.VLC_Code
where convert(date,TSPL_MP_INCENTIVE_ENTRY_head.From_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)
GROUP BY  
    TSPL_MP_INCENTIVE_ENTRY_head.From_Date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,
TSPL_DBT_NEFT.Document_Date,
TSPL_MP_INCENTIVE_ENTRY_head.From_Date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,
    TSPL_MP_INCENTIVE_ENTRY_detail.mp_code,
    TSPL_VLC_MASTER_HEAD.Vsp_code,
    TSPL_VLC_MASTER_HEAD.vlc_name,
    tspl_dbt_neft_detail_hold.MP_Uploader_Code,
    tspl_dbt_neft_detail_hold.MP_Name,
    TSPL_MP_INCENTIVE_ENTRY_detail.MP_Bank,
    tspl_dbt_neft_detail_hold.MP_Account_No,
    tspl_dbt_neft_detail_hold.MP_IFSC_No"

            ElseIf rdbAll.Checked Then
                qry = "select  XXX.[ERP Mp Code],
    MAX(xxx.Document_Date) AS Document_Date, 
    MAX(xxx.[DCS Code]) AS [DCS Code],
    MAX(xxx.[Dcs Name]) AS [Dcs Name],
    MAX(xxx.[Mp Code]) AS [Mp Code],
    MAX(xxx.[MP Name]) AS [MP Name],
    MAX(xxx.[MP Bank]) AS [MP Bank],
    MAX(xxx.[Bank Account]) AS [Bank Account],
    MAX(xxx.[IFSC]) AS [IFSC],
    SUM(xxx.Quantity) AS Quantity,            
    MIN(xxx.From_Date) AS From_Date,           
    MAX(xxx.To_Date) AS To_Date
from
(select TSPL_MP_INCENTIVE_ENTRY_head.From_Date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,TSPL_MP_INCENTIVE_ENTRY_detail.mp_code AS [ERP Mp Code],TSPL_DBT_NEFT.Document_Date,TSPL_VLC_MASTER_HEAD.Vsp_code as [DCS CODE],TSPL_VLC_MASTER_HEAD.vlc_name as [Dcs Name],tspl_dbt_neft_detail_hold.MP_Uploader_Code AS [Mp Code],
tspl_dbt_neft_detail_hold.MP_Name AS [MP Name],TSPL_MP_INCENTIVE_ENTRY_detail.MP_Bank AS [MP Bank],tspl_dbt_neft_detail_hold.MP_Account_No AS [Bank Account],
tspl_dbt_neft_detail_hold.MP_IFSC_No as[IFSC],    SUM(TSPL_MP_INCENTIVE_ENTRY_detail.qty) AS [Quantity]
from tspl_dbt_neft_detail_hold 
left outer join TSPL_DBT_NEFT on tspl_dbt_neft_detail_hold.Document_Code=TSPL_DBT_NEFT.Document_Code
left outer join TSPL_MP_INCENTIVE_ENTRY_detail on TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=tspl_dbt_neft_detail_hold.Against_MP_Incentive_TR
left outer join TSPL_MP_INCENTIVE_ENTRY_head on TSPL_MP_INCENTIVE_ENTRY_head.Document_Code=TSPL_MP_INCENTIVE_ENTRY_detail.Document_Code

 left outer join TSPL_VLC_MASTER_HEAD on TSPL_MP_INCENTIVE_ENTRY_detail.vlc_code= TSPL_VLC_MASTER_HEAD.VLC_Code 
GROUP BY  
TSPL_DBT_NEFT.Document_Date,
TSPL_MP_INCENTIVE_ENTRY_head.From_Date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,
    TSPL_MP_INCENTIVE_ENTRY_detail.mp_code,
    TSPL_VLC_MASTER_HEAD.Vsp_code,
    TSPL_VLC_MASTER_HEAD.vlc_name,
    tspl_dbt_neft_detail_hold.MP_Uploader_Code,
    tspl_dbt_neft_detail_hold.MP_Name,
    TSPL_MP_INCENTIVE_ENTRY_detail.MP_Bank,
    tspl_dbt_neft_detail_hold.MP_Account_No,
    tspl_dbt_neft_detail_hold.MP_IFSC_No
  union all
select TSPL_MP_INCENTIVE_ENTRY_head.From_Date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date, TSPL_MP_INCENTIVE_ENTRY_detail.mp_code AS [ERP Mp Code],TSPL_DBT_NEFT.Document_Date, TSPL_VLC_MASTER_HEAD.Vsp_code as [DCS CODE],TSPL_VLC_MASTER_HEAD.vlc_name as [Dcs Name],TSPL_DBT_NEFT_DETAIL_invalid.MP_Uploader_Code AS [Mp Code],TSPL_DBT_NEFT_DETAIL_invalid.MP_Name AS [MP Name],TSPL_MP_INCENTIVE_ENTRY_detail.MP_Bank AS [MP Bank],
TSPL_DBT_NEFT_DETAIL_invalid.MP_Account_No AS [Bank Account],TSPL_DBT_NEFT_DETAIL_invalid.MP_IFSC_No as[IFSC],
SUM(TSPL_MP_INCENTIVE_ENTRY_detail.qty) AS [Quantity]

from TSPL_DBT_NEFT_DETAIL_invalid
left outer join TSPL_DBT_NEFT on TSPL_DBT_NEFT_DETAIL_invalid.Document_Code=TSPL_DBT_NEFT.Document_Code
left outer join TSPL_MP_INCENTIVE_ENTRY_detail on TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=TSPL_DBT_NEFT_DETAIL_invalid.Against_MP_Incentive_TR
left outer join TSPL_MP_INCENTIVE_ENTRY_head on TSPL_MP_INCENTIVE_ENTRY_head.Document_Code=TSPL_MP_INCENTIVE_ENTRY_detail.Document_Code

 left outer join TSPL_VLC_MASTER_HEAD on TSPL_MP_INCENTIVE_ENTRY_detail.vlc_code= TSPL_VLC_MASTER_HEAD.VLC_Code 
GROUP BY  
TSPL_DBT_NEFT.Document_Date,
TSPL_MP_INCENTIVE_ENTRY_head.From_Date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,
    TSPL_MP_INCENTIVE_ENTRY_detail.mp_code,
    TSPL_VLC_MASTER_HEAD.Vsp_code,
    TSPL_VLC_MASTER_HEAD.vlc_name,
    TSPL_DBT_NEFT_DETAIL_invalid.MP_Uploader_Code,
    TSPL_DBT_NEFT_DETAIL_invalid.MP_Name,
    TSPL_MP_INCENTIVE_ENTRY_detail.MP_Bank,
    TSPL_DBT_NEFT_DETAIL_invalid.MP_Account_No,
    TSPL_DBT_NEFT_DETAIL_invalid.MP_IFSC_No
    union all
select TSPL_MP_INCENTIVE_ENTRY_head.From_Date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date, TSPL_MP_INCENTIVE_ENTRY_detail.mp_code AS [ERP Mp Code],TSPL_DBT_NEFT.Document_Date,TSPL_VLC_MASTER_HEAD.Vsp_code as [DCS CODE],
TSPL_VLC_MASTER_HEAD.vlc_name as [Dcs Name],TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code AS [Mp Code] ,TSPL_DBT_NEFT_DETAIL.MP_Name AS [MP Name],TSPL_DBT_NEFT_DETAIL.MP_Bank AS [MP Bank]
,TSPL_DBT_NEFT_DETAIL.MP_Account_No AS [Bank Account],TSPL_DBT_NEFT_DETAIL.MP_IFSC_No as[IFSC],
    SUM(TSPL_MP_INCENTIVE_ENTRY_detail.qty) AS [Quantity]

 from TSPL_DBT_NEFT_DETAIl 
left outer join TSPL_DBT_NEFT on TSPL_DBT_NEFT_DETAIL.Document_Code=TSPL_DBT_NEFT.Document_Code
left outer join TSPL_MP_INCENTIVE_ENTRY_detail on TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
left outer join TSPL_MP_INCENTIVE_ENTRY_head on TSPL_MP_INCENTIVE_ENTRY_head.Document_Code=TSPL_MP_INCENTIVE_ENTRY_detail.Document_Code

 left outer join TSPL_VLC_MASTER_HEAD on TSPL_MP_INCENTIVE_ENTRY_detail.vlc_code= TSPL_VLC_MASTER_HEAD.VLC_Code 
GROUP BY  
TSPL_DBT_NEFT.Document_Date,
TSPL_MP_INCENTIVE_ENTRY_head.From_Date,TSPL_MP_INCENTIVE_ENTRY_head.To_Date,
    TSPL_MP_INCENTIVE_ENTRY_detail.mp_code,
    TSPL_VLC_MASTER_HEAD.Vsp_code,
    TSPL_VLC_MASTER_HEAD.vlc_name,
    TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code,
    TSPL_DBT_NEFT_DETAIL.MP_Name,
    TSPL_DBT_NEFT_DETAIL.MP_Bank,
    TSPL_DBT_NEFT_DETAIL.MP_Account_No,
    TSPL_DBT_NEFT_DETAIL.MP_IFSC_No

    )xxx
where convert(date,xxx.From_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,xxx.To_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)
GROUP BY xxx.[ERP Mp Code];"
            ElseIf rblCappingHold.Checked Then
                qry = "select  TSPL_MP_INCENTIVE_ENTRY_detail.mp_code AS [ERP Mp Code] , TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Date,TSPL_VLC_MASTER_HEAD.Vsp_code as [DCS CODE],
TSPL_VLC_MASTER_HEAD.vlc_name as [Dcs Name],
TSPL_DBT_CAPING_DETAIL.mp_code AS [Mp Code] ,
--TSPL_DBT_NEFT_DETAIL.MP_Name AS [MP Name],
TSPL_MP_INCENTIVE_ENTRY_detail.MP_Bank AS [MP Bank]
,TSPL_MP_INCENTIVE_ENTRY_detail.MP_Account_No AS [Bank Account],TSPL_MP_INCENTIVE_ENTRY_detail.MP_IFSC_No as[IFSC],
TSPL_DBT_CAPING_DETAIL.qty AS  [Quantity]
from TSPL_DBT_CAPING_DETAIL
left outer join TSPL_DBT_CAPING on TSPL_DBT_CAPING.Document_Code=TSPL_DBT_CAPING_DETAIL.Document_Code
left outer join TSPL_DCS_MP_INCENTIVE_RECO_HEAD on TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=TSPL_DBT_CAPING.reco_code
left outer join TSPL_MP_INCENTIVE_ENTRY_detail on TSPL_MP_INCENTIVE_ENTRY_detail.pk_id=TSPL_DBT_CAPING_DETAIL.pk_id
 left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code= TSPL_MP_INCENTIVE_ENTRY_detail.VLC_Code 
where TSPL_DBT_CAPING_DETAIL.Capping_Status='0' AND  convert(date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "


            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
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
                SetGridFormat1()
                '  SetGridFormationOFGV1Collection()
                ' View()
                gv1.BestFitColumns()

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns("Document_Date").IsVisible = False
            gv1.Columns("From_Date").IsVisible = False

            gv1.Columns("To_Date").IsVisible = False

            'gv1.Columns("UOM").IsVisible = False
        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim Quantity As New GridViewSummaryItem("Quantity", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Quantity)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.AutoSizeRows = True
        gv1.BestFitColumns()
        gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub YearlyDBTSummaryReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.YearlyDBTSummaryReport & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)

            Else
                Throw New Exception("No data found to export.")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class