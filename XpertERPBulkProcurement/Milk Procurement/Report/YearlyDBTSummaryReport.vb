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
            qry = "select TSPL_MP_INCENTIVE_ENTRY_detail.MP_Code AS MPCODEERP, TSPL_VLC_MASTER_HEAD.Vlc_code as DCSCODE,vlc_name as DcsName,TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code AS Mpcode ,TSPL_DBT_NEFT_DETAIL.MP_Name,TSPL_DBT_NEFT_DETAIL.MP_Bank
            ,TSPL_DBT_NEFT_DETAIL.MP_Account_No,TSPL_DBT_NEFT_DETAIL.MP_IFSC_No,TSPL_MP_INCENTIVE_ENTRY_detail.qty,TSPL_MP_INCENTIVE_ENTRY_detail.UOM
            from TSPL_DBT_NEFT
            left outer join TSPL_DBT_NEFT_DETAIL on TSPL_DBT_NEFT_DETAIL.Document_Code=TSPL_DBT_NEFT.Document_Code
            left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader= TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader
            left outer join TSPL_MP_INCENTIVE_ENTRY_detail on TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=TSPL_DBT_NEFT_DETAIL.PK_Id 
            where 2 = 2 and convert(date,Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
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
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class