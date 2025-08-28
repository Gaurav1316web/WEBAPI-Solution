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
            If rdbValid.Checked Then
                DBT_NEFT = "TSPL_DBT_NEFT_DETAIL"
                'Dim TSPL_MP_INCENTIVE_ENTRY_detail As String = "TSPL_MP_INCENTIVE_ENTRY_detail"
            ElseIf rdbInValid.Checked Then
                DBT_NEFT = "TSPL_DBT_NEFT_DETAIL_invalid"
            ElseIf rblHold.Checked Then
                DBT_NEFT = "TSPL_DBT_NEFT_DETAIL_HOLD"

            End If

            If rdbAll.Checked Then
                qry = "select Document_Date,TSPL_MP_INCENTIVE_ENTRY_detail.MP_Code AS MPCODEERP, TSPL_VLC_MASTER_HEAD.Vsp_code as DCSCODE,TSPL_VLC_MASTER_HEAD.vlc_name as DcsName,TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code AS Mpcode ,TSPL_DBT_NEFT_DETAIL.MP_Name,TSPL_DBT_NEFT_DETAIL.MP_Bank
            ,TSPL_DBT_NEFT_DETAIL.MP_Account_No,TSPL_DBT_NEFT_DETAIL.MP_IFSC_No,TSPL_MP_INCENTIVE_ENTRY_detail.qty,TSPL_MP_INCENTIVE_ENTRY_detail.UOM
            from TSPL_DBT_NEFT_DETAIL
            left outer join TSPL_DBT_NEFT on TSPL_DBT_NEFT_DETAIL.Document_Code=TSPL_DBT_NEFT.Document_Code
			left outer join TSPL_MP_INCENTIVE_ENTRY_detail on TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
            left outer join TSPL_VLC_MASTER_HEAD on TSPL_MP_INCENTIVE_ENTRY_detail.vlc_code= TSPL_VLC_MASTER_HEAD.VLC_Code 
            where 2 = 2 and convert(date,Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
            ElseIf rblCappingHold.Checked Then
                qry = "select TSPL_DBT_CAPING.Document_Code,TSPL_VLC_MASTER_HEAD.VSP_Code as Dcscode,TSPL_VLC_MASTER_HEAD.vlc_name as DcsName,TSPL_DBT_CAPING_DETAIL.MP_Code AS MpCode,TSPL_DBT_NEFT_DETAIL.MP_Name AS MpName,TSPL_DBT_NEFT_DETAIL.MP_Bank as MpBank,TSPL_DBT_NEFT_DETAIL.MP_Account_No as MpAccountNo,TSPL_DBT_NEFT_DETAIL.MP_IFSC_No As MPIFSCNO,
TSPL_DBT_CAPING_DETAIL.qty As Quantity
from TSPL_DBT_CAPING_DETAIL 
left outer join TSPL_DBT_CAPING on TSPL_DBT_CAPING.Document_Code=TSPL_DBT_CAPING_DETAIL.Document_Code
left outer join TSPL_VLC_MASTER_HEAD on TSPL_DBT_CAPING_DETAIL.DCS_Code= TSPL_VLC_MASTER_HEAD.VLC_Code 
left outer join tspl_mp_master on tspl_mp_master.MP_Code=TSPL_DBT_CAPING_DETAIL.MP_Code
left outer join TSPL_MP_INCENTIVE_ENTRY_detail on TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=TSPL_DBT_CAPING_DETAIL.PK_Id
left outer join TSPL_DBT_NEFT_DETAIL on TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR=TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id
WHERE Capping_Status='0' and convert(date,Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "

            Else
                qry = "SELECT 
    TSPL_MP_INCENTIVE_ENTRY_detail.MP_Code AS MPCODEERP,
    TSPL_VLC_MASTER_HEAD.Vsp_code AS DCSCODE,
	TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Date,
    TSPL_VLC_MASTER_HEAD.vlc_name AS DcsName,
    dnd.MP_Uploader_Code AS Mpcode,
    dnd.MP_Name as MpName,
    dnd.MP_Bank AS BankName,
    dnd.MP_Account_No as bankAccount,
    dnd.MP_IFSC_No as IFSC,
    TSPL_MP_INCENTIVE_ENTRY_detail.qty,
    TSPL_MP_INCENTIVE_ENTRY_detail.UOM
FROM TSPL_MP_INCENTIVE_ENTRY_detail 
LEFT JOIN TSPL_MP_INCENTIVE_ENTRY_HEAD 
    ON TSPL_MP_INCENTIVE_ENTRY_detail.Document_Code = TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code
LEFT JOIN TSPL_VLC_MASTER_HEAD  
    ON TSPL_MP_INCENTIVE_ENTRY_detail.vlc_code = TSPL_VLC_MASTER_HEAD.VLC_Code
CROSS APPLY (
    SELECT TOP 1 TSPL_DBT_NEFT_DETAIL.*
    FROM " + DBT_NEFT + " 
    INNER JOIN TSPL_DBT_NEFT  
        ON " + DBT_NEFT + ".Document_Code = TSPL_DBT_NEFT.Document_Code
    WHERE " + DBT_NEFT + ".Against_MP_Incentive_TR = TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id
    ORDER BY TSPL_DBT_NEFT.Document_Date DESC, " + DBT_NEFT + ".PK_Id DESC
) dnd  where 2 = 2 and convert(date,Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) 
"
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
                'SetGridFormat1()
                '  SetGridFormationOFGV1Collection()
                ' View()
                gv1.BestFitColumns()

                'Else
                '    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                '    Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub YearlyDBTSummaryReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
    End Sub
End Class