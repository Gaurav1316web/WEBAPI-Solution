Imports common
Imports System.Data.SqlClient
Public Class rptUnionWiseMilkTankerCollectionDetail
    Inherits FrmMainTranScreen
    Private Sub rptUnionWiseMilkTankerCollectionDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        LoadWEIGHMENTdata()
        LoadQCdata()
        If objCommonVar.RCDFCFP Then
            txtUnion.Visible = True
            lblUnion.Visible = True
            'txtRoute.Visible = True
            '    TxtMultiTanker.Visible = False
            '    lblLocation.Visible = False
            '    lblRoute.Visible = False
            '    lblTanker.Visible = False
        Else
            txtUnion.Visible = False
            lblUnion.Visible = False
            txtRoute.Visible = True
            TxtMultiTanker.Visible = True
            lblRoute.Visible = True
            lblTanker.Visible = True
        End If

        If clsCommon.myLen(objCommonVar.CurrentUnionDataBase) > 0 Then
            Dim Union As ArrayList = Nothing
            Dim qry As String = " Select DataBase_Name from TSPL_USER_MASTER where User_Code = '" + objCommonVar.CurrentUserCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Union = New ArrayList()
                For Each drZone As DataRow In dt.Rows
                    Union.Add(clsCommon.myCstr(drZone("DataBase_Name")))
                Next
            End If
            txtUnion.arrValueMember = Union
        End If
    End Sub
    Sub Reset()
        Try
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()
            RadPageView1.SelectedPage = RadPageViewPage1
            txtUnion.arrValueMember = Nothing
            txtRoute.arrValueMember = Nothing
            TxtMultiTanker.arrValueMember = Nothing


            txtUnion.arrValueMember = Nothing
            txtFromDate.Enabled = True
            txtToDate.Enabled = True
            txtFromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
            txtToDate.Value = clsCommon.GETSERVERDATE()
            'EnableDisableFields(True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function chkDataBase() As Boolean
        Try
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Database [TSPL_MASTER] not found !", Me.Text)
                Reset()
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
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
                If objCommonVar.RCDFCFP Then
                    qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

                Else
                    '  qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 AND [TSPL_APP_LOCATION].DataBase_Name='" & objCommonVar.CurrComp_Code1 & "' ORDER BY [TSPL_APP_LOCATION].Location_Name"
                    qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 AND [TSPL_APP_LOCATION].DataBase_Name IN ('JPR','JDH') ORDER BY [TSPL_APP_LOCATION].Location_Name"

                End If
                'qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

                txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTUnionPay", qry, "DataBase Name", "Location", txtUnion.arrValueMember, Nothing)

            End If
            dt = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            If objCommonVar.RCDFCFP Then
                qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

            Else
                '  qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 AND [TSPL_APP_LOCATION].DataBase_Name='" & objCommonVar.CurrComp_Code1 & "' ORDER BY [TSPL_APP_LOCATION].Location_Name"
                qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 AND [TSPL_APP_LOCATION].DataBase_Name IN ('JPR','JDH') ORDER BY [TSPL_APP_LOCATION].Location_Name"

            End If
            'qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

            txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTUnionPay", qry, "DataBase Name", "Location", txtUnion.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Griddata(ByVal print As Boolean)
        Try

            If chkSummary.Checked Then
                txtRoute.Enabled = False
                TxtMultiTanker.Enabled = False
                txtUnion.Enabled = False
                RadGroupBox3.Enabled = False
            Else
                txtRoute.Enabled = False
                TxtMultiTanker.Enabled = False
                txtUnion.Enabled = False
                RadGroupBox3.Enabled = False

            End If
            Dim baseqry As String = Nothing
            Dim qry1 As String = Nothing
            Dim FromDate As String = clsCommon.myCstr(txtFromDate.Text)
            Dim TODate As String = clsCommon.myCstr(txtToDate.Text)
            Dim dtunion As New DataTable
            Dim uQry As String = ""
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found", Me.Text)
                gv1.DataSource = Nothing
                Exit Sub
            End If
            Dim ss As String = clsCommon.GetMulcallString(txtUnion.arrValueMember)

            If txtUnion.arrValueMember Is Nothing Then
                If objCommonVar.RCDFCFP Then
                    uQry = " select [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE 2=2 and  Union_Report=1 order by [TSPL_APP_LOCATION].Location_Name "
                Else
                    uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE 2=2 AND [TSPL_APP_LOCATION].DataBase_Name='" & objCommonVar.CurrDatabase & "' order by [TSPL_APP_LOCATION].Location_Name "
                End If

            Else
                uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                        from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE [TSPL_APP_LOCATION].DataBase_Name  in (" + ss + ") 
                        order by [TSPL_APP_LOCATION].Location_Name "
            End If
            dtunion = clsDBFuncationality.GetDataTable(uQry)

            For ii As Integer = 0 To dtunion.Rows.Count - 1
                If ii > 0 Then
                    baseqry += "union all"
                End If

                baseqry += "  select '" + objCommonVar.CurrentUserCode + "' as UserName, '" + FromDate + "' AS FromDate, '" + TODate + " ' as ToDate,'" + clsCommon.myCstr(dtunion.Rows(ii).Item("Location_Name")) + "' AS [UnionName] ,
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS.location_Code, convert(varchar(12),[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Document_Date,103) as Weighment_Date, 
([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Document_Date) as Weighment_Date_Ordring, 
 [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS.ROUTE_NO,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS.Tanker_No,
 ROW_NUMBER () OVER(ORDER BY  [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS.Gate_Entry_No  ) as SNo,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.Comp_Name,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.add1,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER.add2,
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Document_No as Weighment_No,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QUALITY_CHECK.QC_No,convert(varchar(12),[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QUALITY_CHECK.QC_Out_Date_Time,103) as QC_Date,
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS.Gate_Entry_No,convert(varchar(12),[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS.Date_And_Time,103) as GATE_ENTRY_Date ,
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Gross_Weight, 
case when isnull([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Manual_Gross_Weight,0)=1 then 'M' else 'A' end as Manual_Gross_Weight, 
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Tare_Weight ,case when isnull([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Manual_Tare_Weight,0)= 1 then 'M' else'A' end as Manual_Tare_Weight, 
CASE  WHEN cast( isnull(QCFAT.Param_Field_Value,0) as decimal) > 0 THEN CASE  WHEN ISNULL([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QUALITY_CHECK.Manual_Entry,0) = 1 THEN 'M' WHEN [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QUALITY_CHECK.Manual_Entry = 0 THEN 'A' ELSE NULL END ELSE '' END AS Manual_Entry_QC,
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Net_Weight,
isnull(QCFAT.Param_Field_Value,0) AS Fat_Per,ISNULL(cast((TSPL_PLANT_WEIGHMENT.Net_Weight * QCFAT.Param_Field_Value)/100 as decimal(18,3)),0) as Fat_Kg,ISNULL(QCSNF.Param_Field_Value,0) as SNF_Per,
isnull(cast((TSPL_PLANT_WEIGHMENT.Net_Weight * QCSNF.Param_Field_Value)/100 as decimal(18,3)),0) as SNF_Kg ,
CASE WHEN [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QUALITY_CHECK.is_Param_Accepted = 1  THEN 'Accept' WHEN [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QUALITY_CHECK.SNF_Per IS NOT NULL  AND [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QUALITY_CHECK.Fat_Per IS NOT NULL THEN 'Pending' ELSE '' END AS QcStatus,
isnull(QCCLR.Param_Field_Value,0) AS [CLR],CASE WHEN  [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS.Doc_Type ='MccProc' THEN 'Route'
else 'Purchase' end as Source,'" & objCommonVar.CurrComp_Code1 & "' AS CompCode 
from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT
left join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS.Gate_Entry_No = [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Gate_Entry_No  
left join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QUALITY_CHECK on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QUALITY_CHECK.Gate_Entry_No  =[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo]. TSPL_GATE_ENTRY_DETAILS.Gate_Entry_No 
left join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QC_Parameter_Detail AS QCFAT on QCFAT.QC_No  = TSPL_QUALITY_CHECK.QC_No AND QCFAT.Param_Field_Code = 'FAT' 
left join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QC_Parameter_Detail AS QCSNF on QCSNF.QC_No  = TSPL_QUALITY_CHECK.QC_No AND  QCSNF.Param_Field_Code = 'SNF'
left join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_QC_Parameter_Detail AS QCCLR on QCCLR.QC_No  = TSPL_QUALITY_CHECK.QC_No AND  QCCLR.Param_Field_Code = 'CLR'
left join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_COMPANY_MASTER on 1= 1 
where 2=2    and convert(date,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Document_Date,103) >= convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103)
and convert(date,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PLANT_WEIGHMENT.Document_Date,103) <= convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103) "
                If chkSummary.Checked Then
                Else
                    If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                        baseqry += " and [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS.ROUTE_NO in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
                    End If
                    If TxtMultiTanker.arrValueMember IsNot Nothing AndAlso TxtMultiTanker.arrValueMember.Count > 0 Then
                        baseqry += " and [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_GATE_ENTRY_DETAILS.Tanker_No in (" + clsCommon.GetMulcallString(TxtMultiTanker.arrValueMember) + ") "
                    End If
                End If
            Next
            Dim qryall As String = ""
            qryall = " SELECT ROW_NUMBER() OVER (ORDER BY xx.UnionName,Weighment_Date) AS SNo,Weighment_Date_Ordring,UserName,FromDate,ToDate,UnionName,location_Code,Weighment_Date,ROUTE_NO,Tanker_No,SNo,Weighment_No,QC_No,QC_Date,	Gate_Entry_No,	GATE_ENTRY_Date	,Gross_Weight,	Manual_Gross_Weight,Tare_Weight,Manual_Tare_Weight	,Manual_Entry_QC,Net_Weight,Fat_Per,Fat_Kg,	SNF_Per	,SNF_Kg	,QcStatus,CLR,Source,CompCode "
            If objCommonVar.RCDFCFP Then
                qryall += ",TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Comp_Name "
            Else
                qryall += ",Comp_Name, add1, add2 "
            End If
            If objCommonVar.RCDFCFP Then
                qryall += " FROM(  " & baseqry & ") xx  LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_code1='RCDFCF'  order by xx.UnionName,xx.Weighment_Date_Ordring  "
            Else
                qryall += " FROM(  " & baseqry & ") xx WHERE "

                If cboQC.SelectedValue = "M" Then
                    qryall += " Manual_Entry_QC='M' "
                ElseIf cboQC.SelectedValue = "A" Then
                    qryall += "  Manual_Entry_QC='A'"
                ElseIf cboQC.SelectedValue = "L" Then
                    qryall += "Manual_Entry_QC IN ('M','A')  "
                End If
                If cboQC.SelectedValue = "M" Then
                    qryall += "AND  Manual_Tare_Weight='M' "
                ElseIf cboQC.SelectedValue = "A" Then
                    qryall += "AND  Manual_Tare_Weight='A'"
                ElseIf cboQC.SelectedValue = "L" Then
                    qryall += " AND Manual_Tare_Weight IN ('M','A')  "
                End If

                qryall += " order by xx.UnionName,xx.Weighment_Date_Ordring "
                End If

                Dim SummaryQry As String = ""
            If chkSummary.Checked Then
                SummaryQry += "SELECT ROW_NUMBER() OVER (ORDER BY XX.UnionName) AS SNo,'" + objCommonVar.CurrentUserCode + "' as UserName,
max(xx.FromDate)FromDate,max(xx.ToDate)ToDate, xx.UnionName, max(xx.Weighment_Date)Weighment_Date,count(XX.Tanker_No) AS Tanker_No,max(XX.ROUTE_NO) AS ROUTE_NO,
count(XX.Gate_Entry_No) AS Gate_Entry_No,SUM(XX.Gross_Weight) AS Gross_Weight, SUM(XX.tare_weight) AS Tare_Weight,MAX(XX.manual_Tare_Weight) AS Manual_Tare_Weight, MAX(XX.Manual_Entry_Qc) AS Manual_Entry_QC,
SUM(XX.Net_Weight) AS Net_Weight,CAST(sum(isnull(XX.Fat_Kg,0)) AS DECIMAL(18,3)) AS Fat_Kg,CAST(sum(isnull(XX.SNF_Kg,0)) AS DECIMAL(18,3))  AS SNF_Kg,
SUM(CASE WHEN XX.QcStatus = 'Accept' THEN 1 ELSE 0 END) AS AcceptQC,SUM(CASE WHEN XX.QcStatus = 'Reject' THEN 1 ELSE 0 END) AS RejectQC,max(CompCode)CompCode, "
                If objCommonVar.RCDFCFP Then
                    SummaryQry += " max(TSPL_COMPANY_MASTER.Comp_Name)Comp_Name,max(TSPL_COMPANY_MASTER.add1)add1,max(TSPL_COMPANY_MASTER.add2)add2 "
                Else
                    SummaryQry += " max(xx.Comp_Name)Comp_Name,max(xx.add1)add1,max(xx.add2)add2,MAX(CompCode)CompCode "
                End If
                SummaryQry += ",max(Weighment_Date_Ordring)Weighment_Date_Ordring From ( " & baseqry & ") XX LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_code1='RCDFCF' "
                SummaryQry += " GROUP BY XX.UnionName,XX.Weighment_Date ORDER BY xx.UnionName,Weighment_Date_Ordring;"
            End If
            Dim dt2 As DataTable
            If chkSummary.Checked Then
                dt2 = clsDBFuncationality.GetDataTable(SummaryQry)
            Else
                dt2 = clsDBFuncationality.GetDataTable(qryall)
            End If
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = False
                If chkSummary.Checked Then
                    SetGridFormatSummary()
                Else
                    SetGridFormat()
                End If
                gv1.BestFitColumns()
                If print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    If chkSummary.Checked Then
                        frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "rptUnionWiseMilkTankerCollectionSummary", "") ''report for both (RCDF And RCDFCF)
                    Else
                        frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "rptUnionWiseMilkTankerCollection", "") ''report for both (RCDF And RCDFCF)
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        Griddata(False)
    End Sub
    Sub SetGridFormatSummary()
        gv1.ShowGroupPanel = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns("CompCode").IsVisible = False
            gv1.Columns("Weighment_Date_Ordring").IsVisible = False

            gv1.Columns("UserName").IsVisible = False
            gv1.Columns("ROUTE_NO").IsVisible = False
            gv1.Columns("Weighment_Date").IsVisible = True
            gv1.Columns("Weighment_Date").HeaderText = "Document Date"

            gv1.Columns("SNo").HeaderText = "Document Date"

            gv1.Columns("SNo").IsVisible = True
            gv1.Columns("SNo").HeaderText = "SNo."
            gv1.Columns("UnionName").IsVisible = True
            gv1.Columns("UnionName").HeaderText = "Union Name"
            gv1.Columns("Gate_Entry_No").IsVisible = True
            gv1.Columns("Gate_Entry_No").HeaderText = "Gate Entry Count"
            gv1.Columns("Tanker_No").IsVisible = True
            gv1.Columns("Tanker_No").HeaderText = "No. of Tanker"
            gv1.Columns("Gross_Weight").IsVisible = True
            gv1.Columns("Gross_Weight").HeaderText = "Gross Weight"
            gv1.Columns("Tare_Weight").IsVisible = True
            gv1.Columns("Tare_Weight").HeaderText = "Tare Weight"
            gv1.Columns("Manual_Tare_Weight").IsVisible = True
            gv1.Columns("Manual_Tare_Weight").HeaderText = "Manual Tare Weight"
            gv1.Columns("Manual_Entry_QC").IsVisible = True
            gv1.Columns("Manual_Entry_QC").HeaderText = "Manual Entry QC"
            gv1.Columns("Net_Weight").IsVisible = False
            gv1.Columns("Net_Weight").HeaderText = "Net Weight"
            gv1.Columns("Fat_Kg").IsVisible = True
            gv1.Columns("Fat_Kg").HeaderText = "FAT Kg"
            gv1.Columns("SNF_Kg").IsVisible = True
            gv1.Columns("SNF_Kg").HeaderText = "SNF Kg"
            gv1.Columns("AcceptQC").IsVisible = True
            gv1.Columns("AcceptQC").HeaderText = "AcceptQC"
            gv1.Columns("RejectQC").IsVisible = True
            gv1.Columns("RejectQC").HeaderText = "RejectQC"
            gv1.Columns("Comp_Name").IsVisible = False
            gv1.Columns("add1").IsVisible = False
            gv1.Columns("add2").IsVisible = False

        Next
        'Dim summaryRowItemB As New GridViewSummaryRowItem()
        'Dim Gross_Weight As New GridViewSummaryItem("Gross_Weight", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(Gross_Weight)
        'Dim Tare_Weight As New GridViewSummaryItem("Tare_Weight", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(Tare_Weight)
        'Dim Net_Weight As New GridViewSummaryItem("Net_Weight", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(Net_Weight)


        'Dim SNF_Kg As New GridViewSummaryItem("SNF_Kg", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(SNF_Kg)

        'Dim Fat_Kg As New GridViewSummaryItem("Fat_Kg", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(Fat_Kg)
        'Dim intCount As Integer = 0
        'gv1.ShowGroupPanel = True
        'gv1.MasterTemplate.AutoExpandGroups = True
        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Dim summaryRowItemB As New GridViewSummaryRowItem()

        'Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n0}", GridAggregateFunction.Sum)

        Dim SNF_Kg As New GridViewSummaryItem("SNF_Kg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(SNF_Kg)
        Dim Fat_Kg As New GridViewSummaryItem("Fat_Kg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Fat_Kg)
        Dim Net_Weight As New GridViewSummaryItem("Net_Weight", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Net_Weight)
        Dim Tare_Weight As New GridViewSummaryItem("Tare_Weight", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Tare_Weight)
        Dim Gross_Weight As New GridViewSummaryItem("Gross_Weight", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Gross_Weight)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.AutoSizeRows = True
        gv1.BestFitColumns()
        gv1.MasterTemplate.AutoExpandGroups = True

    End Sub
    Sub SetGridFormat()
        gv1.ShowGroupPanel = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True

            'For ii As Integer = 0 To gv1.Columns.Count - 1
            'gv1.Columns(ii).ReadOnly = True
            ' gv1.Columns(ii).BestFit()

            gv1.Columns("CompCode").IsVisible = False

            gv1.Columns("ToDate").IsVisible = False
            gv1.Columns("FromDate").IsVisible = False
            gv1.Columns("UserName").IsVisible = False
            gv1.Columns("CLR").IsVisible = True
            gv1.Columns("Weighment_Date_Ordring").IsVisible = False

            gv1.Columns("SNo").IsVisible = True
            gv1.Columns("SNo").HeaderText = "SNo."
            gv1.Columns("UnionName").IsVisible = True
            gv1.Columns("UnionName").HeaderText = "Union Name"
            gv1.Columns("Weighment_Date").IsVisible = True
            gv1.Columns("Weighment_Date").HeaderText = "Date"
            gv1.Columns("ROUTE_NO").IsVisible = True
            gv1.Columns("ROUTE_NO").HeaderText = "Route NO"
            gv1.Columns("Tanker_No").IsVisible = True
            gv1.Columns("Tanker_No").HeaderText = "Tanker No"

            gv1.Columns("location_Code").IsVisible = False
            gv1.Columns("location_Code").HeaderText = "location_Code"
            gv1.Columns("Comp_Name").IsVisible = False
            gv1.Columns("Comp_Name").HeaderText = "Comp Name"
            gv1.Columns("add1").IsVisible = False
            gv1.Columns("add1").HeaderText = "add1"
            gv1.Columns("add2").IsVisible = False
            gv1.Columns("add2").HeaderText = "add2"
            gv1.Columns("Weighment_No").IsVisible = False
            gv1.Columns("Weighment_No").HeaderText = "Weighment No"

            gv1.Columns("QC_No").IsVisible = True
            gv1.Columns("QC_No").HeaderText = "QC No"
            gv1.Columns("QC_Date").IsVisible = True
            gv1.Columns("QC_Date").HeaderText = "QC Date"
            gv1.Columns("Gate_Entry_No").IsVisible = True
            gv1.Columns("Gate_Entry_No").HeaderText = "Gate_Entry_No"
            gv1.Columns("GATE_ENTRY_Date").IsVisible = True
            gv1.Columns("GATE_ENTRY_Date").HeaderText = "GATE ENTRY Date"

            gv1.Columns("Manual_Gross_Weight").IsVisible = True
            gv1.Columns("Manual_Gross_Weight").HeaderText = "Type"
            gv1.Columns("Gross_Weight").IsVisible = True
            gv1.Columns("Gross_Weight").HeaderText = "Gross Weight"

            gv1.Columns("Manual_Tare_Weight").IsVisible = True
            gv1.Columns("Manual_Tare_Weight").HeaderText = "Type"
            gv1.Columns("Tare_Weight").IsVisible = True
            gv1.Columns("Tare_Weight").HeaderText = "Tare Weight"

            gv1.Columns("Net_Weight").IsVisible = True
            gv1.Columns("Net_Weight").HeaderText = "Net Weight"

            gv1.Columns("QcStatus").IsVisible = True
            gv1.Columns("QcStatus").HeaderText = "QC Status"
            gv1.Columns("Manual_Entry_QC").IsVisible = True
            gv1.Columns("Manual_Entry_QC").HeaderText = "Type"



            gv1.Columns("Fat_Per").IsVisible = True
            gv1.Columns("Fat_Per").HeaderText = "Fat %"
            gv1.Columns("SNF_Per").IsVisible = True
            gv1.Columns("SNF_Per").HeaderText = "SNF %"
            gv1.Columns("Fat_Kg").IsVisible = True
            gv1.Columns("Fat_Kg").HeaderText = "Fat Kg"
            gv1.Columns("SNF_Kg").IsVisible = True
            gv1.Columns("SNF_Kg").HeaderText = "SNF Kg"
        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim SNF_Kg As New GridViewSummaryItem("SNF_Kg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(SNF_Kg)
        Dim Gross_Weight As New GridViewSummaryItem("Gross_Weight", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Gross_Weight)

        Dim Tare_Weight As New GridViewSummaryItem("Tare_Weight", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Tare_Weight)

        Dim Net_Weight As New GridViewSummaryItem("Net_Weight", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Net_Weight)

        'Dim SNF_Kg As New GridViewSummaryItem("SNF_Kg", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(SNF_Kg)

        Dim Fat_Kg As New GridViewSummaryItem("Fat_Kg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Fat_Kg)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.AutoSizeRows = True
        gv1.BestFitColumns()
        gv1.MasterTemplate.AutoExpandGroups = True
        'Dim summaryRowItemB As New GridViewSummaryRowItem()
        'Dim Gross_Weight As New GridViewSummaryItem("Gross_Weight", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(Gross_Weight)

        'Dim Tare_Weight As New GridViewSummaryItem("Tare_Weight", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(Tare_Weight)

        'Dim Net_Weight As New GridViewSummaryItem("Net_Weight", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(Net_Weight)

        'Dim SNF_Kg As New GridViewSummaryItem("SNF_Kg", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(SNF_Kg)

        'Dim Fat_Kg As New GridViewSummaryItem("Fat_Kg", "{0:n2}", GridAggregateFunction.Sum)
        'summaryRowItemB.Add(Fat_Kg)
        'Dim intCount As Integer = 0
        'gv1.ShowGroupPanel = True
        'gv1.MasterTemplate.AutoExpandGroups = True
        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        'gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        View()
    End Sub

    Sub View()

        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("SNo").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("UnionName").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("location_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Weighment_Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("ROUTE_NO").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Tanker_No").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Weighment"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Manual_Gross_Weight").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Gross_Weight").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Manual_Tare_Weight").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Tare_Weight").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Net_Weight").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Quality Check"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("QcStatus").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Manual_Entry_QC").Name)

            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Fat_Per").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SNF_Per").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("CLR").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Fat_Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SNF_Kg").Name)

            gv1.ViewDefinition = view
        End If
    End Sub
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select ROUTE_NO as [Route Code],ROUTE_NAME as Name  from TSPL_BULK_ROUTE_MASTER where 2=2 "
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultiTanker__My_Click(sender As Object, e As EventArgs) Handles TxtMultiTanker._My_Click
        Try
            Dim qry As String = " select Tanker_No as Code,Tanker_Name as Name from TSPL_TANKER_MASTER "
            TxtMultiTanker.arrValueMember = clsCommon.ShowMultipleSelectForm("MultiTanker", qry, "Code", "Name", TxtMultiTanker.arrValueMember, TxtMultiTanker.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(True)
    End Sub

    Private Sub chkSummary_Click(sender As Object, e As EventArgs) Handles chkSummary.Click
        If chkSummary.Checked Then


            RadGroupBox3.Enabled = True
            txtUnion.Enabled = True
            txtRoute.Enabled = True
            TxtMultiTanker.Enabled = True
        Else
            RadGroupBox3.Enabled = True
            txtUnion.Enabled = True
            txtRoute.Enabled = False
            TxtMultiTanker.Enabled = False
        End If



    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtRoute.Enabled = True
        TxtMultiTanker.Enabled = True
        txtUnion.Enabled = True
        RadGroupBox3.Enabled = True
        chkSummary.Checked = False
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterView.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub



    Sub LoadQCdata()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "All"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Manual"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Auto"
        dt.Rows.Add(dr)


        cboQC.DataSource = dt
        cboQC.ValueMember = "Code"
        cboQC.DisplayMember = "Name"


    End Sub



    Sub LoadWEIGHMENTdata()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "All"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Manual"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Auto"
        dt.Rows.Add(dr)


        cboWEIGHMENT.DataSource = dt
        cboWEIGHMENT.ValueMember = "Code"
        cboWEIGHMENT.DisplayMember = "Name"
    End Sub

End Class