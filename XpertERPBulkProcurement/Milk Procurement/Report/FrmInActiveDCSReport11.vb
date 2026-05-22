Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions
Public Class FrmInActiveDCSReport11
    Inherits FrmMainTranScreen
    Private Sub FrmInActiveDCSReport11_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()

    End Sub

    Private Sub txtUnion__My_Click(sender As Object, e As EventArgs) Handles txtUnion._My_Click
        Try
            Dim dt As DataTable
            Dim qry As String = ""

            If clsCommon.myLen(objCommonVar.CurrentUnionDataBase) > 0 Then
                qry = " Select DataBase_Name as [DataBase Name] from TSPL_USER_MASTER where User_Code = '" + objCommonVar.CurrentUserCode + "' "
                txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("SaleUnionDs", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)

            Else
                dt = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                    Exit Sub
                End If

                qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

                txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTUnionPay", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)

            End If
            'dt = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            'If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            '    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            '    Exit Sub
            'End If

            'qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

            'txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTUnionPay", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        Try
            Dim qry As String = ""
            Dim qryies As String = ""
            Dim Baseqry As String = ""
            Dim Baseqry1 As String = ""
            Dim Baseqry2 As String = ""
            Dim dbNames As String = ""
            Dim portDt As New DataTable
            Dim dtGrandTotal As DataTable
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found", Me.Text)
                Gv1.DataSource = Nothing
                Exit Sub
            End If
            Dim ss As String = clsCommon.GetMulcallString(txtUnion.arrValueMember)

            If txtUnion.arrValueMember Is Nothing Then
                qry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE Apply_PD_Account = 1 order by [TSPL_APP_LOCATION].Location_Name "
            Else
                qry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                        from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE Apply_PD_Account = 1 and [TSPL_APP_LOCATION].DataBase_Name  in (" + ss + ") 
                        order by [TSPL_APP_LOCATION].Location_Name "
            End If
            'dt = clsMilkUnion.UnionDBName()
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        qryies += " UNION ALL "
                    Else
                        qryies += " ( "
                    End If
                    If rbtnDetail.IsChecked Then
                        '                        qryies += " select ROW_NUMBER() OVER (ORDER BY VLC_Code_VLC_Uploader) AS SrNo, '" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name], [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Code],[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS Name],xxx.VLC_CODE,convert(varchar, DOC_DATE,103) as [Last Collection Date] from (
                        'select VLC_CODE,max(DOC_DATE) as DOC_DATE from (
                        'select VLC_CODE,max(DOC_DATE) as DOC_DATE,1 as RI from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD 
                        'where DOC_DATE<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   group by VLC_CODE 
                        'union all
                        'select VLC_CODE,null as DOC_DATE,-1 as RI from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD where 
                        'DOC_DATE>=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' 
                        'and DOC_DATE<=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  group by VLC_CODE 
                        ')xx  group by VLC_CODE having sum(ri)>0
                        ')xxx 
                        'left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_CODE=xxx.VLC_CODE"

                        qryies += " SELECT ROW_NUMBER() OVER (ORDER BY VLC_Code_VLC_Uploader) AS SrNo, 'Jaipur' AS [Union Name], ZZZ.VLC_Code_VLC_Uploader,ZZZ.VLC_CODE,ZZZ.VLC_Name,ZZZ.LastMilkProcDate
FROM

(select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Name,xxx.VLC_CODE,convert(varchar, DOC_DATE,103) as LastMilkProcDate 

from (
select VLC_CODE,max(DOC_DATE) as DOC_DATE from (
select VLC_CODE,max(x.DOC_DATE) as DOC_DATE,1 as RI from (
select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD.VLC_CODE,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD.DOC_DATE  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD where DOC_DATE< '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   
union all
select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_CODE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL 
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code
where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date< '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  
union all
select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.VLC_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date as DOC_DATE  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID 
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.Document_Code
where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date< '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   ) x group by VLC_CODE 
union all
select VLC_CODE,null as DOC_DATE,-1 as RI 

from (
select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD.VLC_CODE,null as DOC_DATE  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD.DOC_DATE>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD.DOC_DATE<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'
union all
select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_CODE,null as DOC_DATE  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL 
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code
where  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'

union all

select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.VLC_CODE,null as DOC_DATE from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID 
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.Document_Code
where  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'
) x group by VLC_CODE 
) xx  group by VLC_CODE having sum(ri)>0
) xxx 
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE=xxx.VLC_CODE
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code
where isnull(TSPL_VLC_MASTER_HEAD.IsSuspense,0)=0  and  ( isnull(TSPL_VENDOR_MASTER.is_Drip_Saver,'')='' or TSPL_VENDOR_MASTER.is_Drip_Saver='N')
) ZZZ   "



                    Else
                        qryies += "    SELECT ROW_NUMBER() OVER (ORDER BY MAX(VLC_Code_VLC_Uploader)) AS SrNo, 'Jaipur' AS [Union Name],COUNT(XYZ.VLC_Code_VLC_Uploader)[DCS Count] FROM  (SELECT ROW_NUMBER() OVER (ORDER BY VLC_Code_VLC_Uploader) AS SrNo, 'Jaipur' AS [Union Name], ZZZ.VLC_Code_VLC_Uploader,ZZZ.VLC_CODE,ZZZ.VLC_Name,ZZZ.LastMilkProcDate
FROM

(select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Name,xxx.VLC_CODE,convert(varchar, DOC_DATE,103) as LastMilkProcDate 

from (
select VLC_CODE,max(DOC_DATE) as DOC_DATE from (
select VLC_CODE,max(x.DOC_DATE) as DOC_DATE,1 as RI from (
select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD.VLC_CODE,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD.DOC_DATE  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD where DOC_DATE< '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   
union all
select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_CODE, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL 
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code
where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date< '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  
union all
select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.VLC_Code,[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date as DOC_DATE  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID 
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.Document_Code
where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date< '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'   ) x group by VLC_CODE 
union all
select VLC_CODE,null as DOC_DATE,-1 as RI 

from (
select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD.VLC_CODE,null as DOC_DATE  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD.DOC_DATE>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MILK_SRN_HEAD.DOC_DATE<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'
union all
select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_CODE,null as DOC_DATE  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL 
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code
where  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'

union all

select [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.VLC_CODE,null as DOC_DATE from  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID 
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD on [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=[" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.Document_Code
where  [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date<= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'
) x group by VLC_CODE 
) xx  group by VLC_CODE having sum(ri)>0
) xxx 
left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE=xxx.VLC_CODE
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code
where isnull(TSPL_VLC_MASTER_HEAD.IsSuspense,0)=0  and  ( isnull(TSPL_VENDOR_MASTER.is_Drip_Saver,'')='' or TSPL_VENDOR_MASTER.is_Drip_Saver='N')
) ZZZ  )XYZ GROUP BY [Union Name] "
                    End If
                Next
                Baseqry = "  " + qryies + "  ) "

            End If

            portDt = clsDBFuncationality.GetDataTable(Baseqry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterView.Refresh()
            Gv1.GroupDescriptors.Clear()
            Gv1.EnableFiltering = True
            Gv1.EnableFiltering = False
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            'If portDt.Rows.Count > 0 Then
            'portDt.Rows.Add("Grand Total", dtGrandTotal.Rows(0)("Month_Year"), dtGrandTotal.Rows(0)("Refence_No"), dtGrandTotal.Rows(0)("No_Of_Record"), dtGrandTotal.Rows(0)("Milk_Qty"), dtGrandTotal.Rows(0)("Amount"))
            Gv1.DataSource = portDt
            ReStoreGridLayout()

            EnableDisableCntrl(False)

            Gv1.BestFitColumns()
            Gv1.MasterTemplate.AutoExpandGroups = True
            RadPageView1.SelectedPage = RadPageViewPage2
            Gv1.BestFitColumns()


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub EnableDisableCntrl(ByVal val As Boolean)
        RadGroupBox1.Enabled = False
    End Sub
    Sub ReStoreGridLayout()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        Gv1.AutoExpandGroups = False
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Gv1.ShowGroupPanel = False
        If rbtnDetail.IsChecked Then
            Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Code"
            Gv1.Columns("VLC_Name").HeaderText = "DCS Name"
            Gv1.Columns("LastMilkProcDate").HeaderText = "Last Collection Date"
            Gv1.Columns("VLC_CODE").IsVisible = False
        End If
        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        RadGroupBox1.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        RadGroupBox1.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
        txtUnion.arrValueMember = Nothing
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class