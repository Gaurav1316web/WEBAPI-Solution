Imports common
Imports System.ComponentModel
Imports System.IO

Public Class frmVLCProgressReportReport
    Inherits FrmMainTranScreen

    Dim dt As DataTable = Nothing
    Dim arrRange As New Dictionary(Of String, clsDateRange)
    Dim arrItem As New List(Of String)
    Dim Key As String
    Dim obj As clsDateRange

    Dim arr As New Dictionary(Of Integer, DataRow)
    Dim strColumnForTotal As String = Nothing
    'Dim SettTrendDiffValueForColor As Integer

    Const colSelect As String = "colSelect"
    Const colFromDate As String = "colFromDate"
    Const colToDate As String = "colToDate"

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Private Sub rptMilkBillProcurementSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        'SettTrendDiffValueForColor = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TrendDiffValueForColor, clsFixedParameterCode.TrendDiffValueForColor, Nothing))
        LoadType()
        LoadCycles()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        arr = New Dictionary(Of Integer, DataRow)
        strColumnForTotal = Nothing
        txtMCC.Value = ""
        RadPageView1.SelectedPage = RadPageViewPage1

        SetControls()
    End Sub

    Private Sub LoadCycles()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow
        For ii As Integer = 1 To 20
            dr = dt.NewRow()
            dr("Code") = clsCommon.myCstr(ii)
            dr("Name") = clsCommon.myCstr(ii)
            dt.Rows.Add(dr)
        Next
        cboCycles.DataSource = dt
        cboCycles.ValueMember = "Code"
        cboCycles.DisplayMember = "Name"

        cboCycles.SelectedValue = "4"
    End Sub

    Private Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "V"
        dr("Name") = "VLC Progress Report"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "D"
        dr("Name") = "Day Wise Trend Report"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Sale Progress Report"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub

    Private Sub cboType_Validating(sender As Object, e As CancelEventArgs) Handles cboType.Validating
        SetControls()
    End Sub

    Sub SetControls()
        chkShowColor.Visible = False
        cboCycles.Visible = False
        MyLabel2.Visible = False
        txtToDate.ReadOnly = False
        SplitContainer2.Panel1Collapsed = False
        SplitContainer2.Panel2Collapsed = True
        cbkShowFATSNF.Visible = False
        lblColorChangeQty.Visible = False
        txtColorChangeQty.Visible = False
        If clsCommon.CompairString(cboType.SelectedValue, "V") = CompairStringResult.Equal Then
            cboCycles.Visible = True
            MyLabel2.Visible = True
            SplitContainer2.Panel1Collapsed = True
            SplitContainer2.Panel2Collapsed = False
            cboCycles.SelectedValue = 5
            LoadGridDate()
        ElseIf clsCommon.CompairString(cboType.SelectedValue, "D") = CompairStringResult.Equal Then
            chkShowColor.Visible = True
            cbkShowFATSNF.Visible = True
            lblColorChangeQty.Visible = True
            txtColorChangeQty.Visible = True
        ElseIf clsCommon.CompairString(cboType.SelectedValue, "S") = CompairStringResult.Equal Then
            cboCycles.Visible = True
            MyLabel2.Visible = True
            txtToDate.ReadOnly = True
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print()
    End Sub
    Sub Print()
        Try

            Try
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ConditionalFormattingObjectList.Clear()
                Next
            Catch ex As Exception
                Dim x As Integer = 0
            End Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From Date Can't be more than To Date")
            End If
            If clsCommon.CompairString(cboType.SelectedValue, "V") = CompairStringResult.Equal Then
#Region "VLC Progress Report"
                arrRange = New Dictionary(Of String, clsDateRange)
                For ii As Integer = 0 To gvDate.Rows.Count - 1
                    If clsCommon.myCBool(gvDate.Rows(ii).Cells(colSelect).Value) Then
                        If clsCommon.myLen(gvDate.Rows(ii).Cells(colFromDate).Value) <= 0 Then
                            Throw New Exception("Please select from Date at " + clsCommon.myCstr(ii + 1))
                        End If
                        If clsCommon.myLen(gvDate.Rows(ii).Cells(colFromDate).Value) <= 0 Then
                            Throw New Exception("Please select to Date at " + clsCommon.myCstr(ii + 1))
                        End If
                        obj = New clsDateRange
                        obj.FromDate = clsCommon.myCDate(gvDate.Rows(ii).Cells(colFromDate).Value)
                        obj.ToDate = clsCommon.myCDate(gvDate.Rows(ii).Cells(colToDate).Value)
                        Dim ts As TimeSpan = clsCommon.GetDateWithStartTime(obj.ToDate).Subtract(clsCommon.GetDateWithStartTime(obj.FromDate))
                        obj.Days = ts.Days + 1
                        If Not arrRange.ContainsKey(clsCommon.GetPrintDate(obj.FromDate, "dd.MM.yyyy") + "-" + clsCommon.GetPrintDate(obj.ToDate, "dd.MM.yyyy")) Then
                            arrRange.Add(clsCommon.GetPrintDate(obj.FromDate, "dd.MM.yyyy") + "-" + clsCommon.GetPrintDate(obj.ToDate, "dd.MM.yyyy"), obj)
                        End If
                        If txtFromDate.Value > obj.FromDate Then
                            txtFromDate.Value = obj.FromDate
                        End If
                        If txtToDate.Value < obj.ToDate Then
                            txtToDate.Value = obj.ToDate
                        End If
                    End If
                Next
                If arrRange.Count <= 0 Then
                    Throw New Exception("Please select Date Range")
                End If
                Dim Qry As String = "select ROW_NUMBER() over (Partition by xxx.ROUTE_CODE order by xxx.ROUTE_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ) as SNO ,xxx.ROUTE_CODE,TSPL_MCC_ROUTE_MASTER.Route_Name+' '+isnull(TSPL_EMPLOYEE_MASTER.Emp_Name,'') as RouteName,xxx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader+' '+TSPL_VLC_MASTER_HEAD.VLC_Name as VLCName,TabStart.DOS"
                For ii As Integer = 0 To arrRange.Count - 1
                    Key = arrRange.Keys(ii)
                    obj = arrRange(Key)

                    Qry += ",cast([" + Key + " Qty] as decimal(18,2)) as [" + Key + " Qty]" + Environment.NewLine
                    Qry += ",cast((case when isnull([" + Key + " Days],0)=0 then 0 else ([" + Key + " Qty]/[" + Key + " Days]) end) as decimal(18,0)) as [" + Key + " Avg]" + Environment.NewLine
                    Qry += ",cast(isnull(TabMP.[" + Key + " MP] ,0) as decimal(18,0)) as  [" + Key + " MP] " + Environment.NewLine
                    If ii > 0 Then
                        Dim objPrevious As clsDateRange = arrRange(arrRange.Keys(ii - 1))
                        Qry += ", cast(case when ([" + arrRange.Keys(ii - 1) + " Qty]/" + clsCommon.myCstr(objPrevious.Days) + ")=0 then 0 else ((([" + Key + " Qty]/" + clsCommon.myCstr(obj.Days) + ")-([" + arrRange.Keys(ii - 1) + " Qty]/" + clsCommon.myCstr(objPrevious.Days) + "))*100 )/([" + arrRange.Keys(ii - 1) + " Qty]/" + clsCommon.myCstr(objPrevious.Days) + ") end as decimal(18,0)) as [" + Key + " INCDEC]" + Environment.NewLine
                    End If
                Next
                Qry += ",case when TSPL_VLC_MASTER_HEAD.Active=0 then 'Closed' else '' end as Remarks from (
select ROUTE_CODE,VLC_CODE"
                For ii As Integer = 0 To arrRange.Count - 1
                    Key = arrRange.Keys(ii)
                    obj = arrRange(Key)
                    Qry += ",SUM(Qty * (Case When xx.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(obj.FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and xx.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end)) as [" + Key + " Qty] "
                    Qry += ",Count(distinct (Case When xx.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(obj.FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and xx.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then xx.DOC_DATE_Calculate else null end)) as [" + Key + " Days] "
                Next
                Qry += "  from (

select TSPL_MILK_SRN_HEAD.ROUTE_CODE,TSPL_MILK_SRN_HEAD.VLC_CODE,TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,CONVERT(DATE,TSPL_MILK_SRN_HEAD.DOC_DATE,103) AS DOC_DATE_Calculate,TSPL_MILK_SRN_DETAIL.Qty from TSPL_MILK_SRN_DETAIL
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE
where TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                and TSPL_MILK_SRN_HEAD.MCC_CODE='" + txtMCC.Value + "'"
                If txtSupervisor.arrValueMember IsNot Nothing AndAlso txtSupervisor.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_MCC_ROUTE_MASTER.Supervisor_Name in (" + clsCommon.GetMulcallString(txtSupervisor.arrValueMember) + ")"
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_MILK_SRN_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_MILK_SRN_HEAD.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")"
                End If



                Qry += ")xx group by ROUTE_CODE,VLC_CODE
)xxx

left outer join ( select VLC_CODE,SUBSTRING( replace(convert(varchar, min(DOC_DATE),106),' ','/'),4,8) as DOS from TSPL_MILK_SRN_HEAD group by VLC_CODE)  as TabStart on TabStart.VLC_CODE=xxx.VLC_CODE

left outer join ( select VLC_Code "
                For ii As Integer = 0 To arrRange.Count - 1
                    Key = arrRange.Keys(ii)
                    obj = arrRange(Key)

                    Qry += ",COUNT(distinct (case when xx.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(obj.FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and xx.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then MP_Code else null end)) as [" + Key + " MP] "
                Next

                Qry += " from (
select TSPL_VLC_DATA_UPLOADER.Doc_No,TSPL_VLC_DATA_UPLOADER.Doc_Date,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_MP_MASTER.MP_Code from TSPL_VLC_DATA_UPLOADER 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE and TSPL_VLC_MASTER_HEAD.MCC=TSPL_VLC_DATA_UPLOADER.MCC_Code
left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.MP_CODE and TSPL_MP_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code
where TSPL_VLC_DATA_UPLOADER.MCC_Code='" + txtMCC.Value + "' and TSPL_VLC_DATA_UPLOADER.Doc_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_VLC_DATA_UPLOADER.Doc_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
union all
select TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code as Doc_No,convert(date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as Doc_Date,TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code,TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code as MP_Code  
from TSPL_VLC_DATA_UPLOADER_DETAIL 
left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code=TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code
where TSPL_VLC_MASTER_HEAD.MCC='" + txtMCC.Value + "' and TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
 ) xx group by VLC_Code ) as TabMP on TabMP.VLC_Code=xxx.VLC_CODE

left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=xxx.ROUTE_CODE
left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_MCC_ROUTE_MASTER.Supervisor_Name
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xxx.VLC_CODE
order by TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader"


                dt = Nothing
                dt = clsDBFuncationality.GetDataTable(Qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("No Data Found to Display")
                End If
                AddTotalRows()
                Gv1.DataSource = dt
#End Region
            ElseIf clsCommon.CompairString(cboType.SelectedValue, "D") = CompairStringResult.Equal Then
#Region "Day Wise Trend"
                Dim FromDate As Date = txtFromDate.Value
                Dim ToDate As Date = txtToDate.Value
                arrRange = New Dictionary(Of String, clsDateRange)
                While FromDate <= ToDate
                    obj = New clsDateRange
                    obj.FromDate = FromDate
                    obj.ToDate = FromDate

                    arrRange.Add(clsCommon.GetPrintDate(FromDate, "dd.MM.yyyy"), obj)
                    FromDate = FromDate.AddDays(1)
                End While
                Dim KeyPrevious As String = ""
                Dim Qry As String = "select ROW_NUMBER() over (Partition by xxx.ROUTE_CODE order by xxx.ROUTE_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ) as SNO ,xxx.ROUTE_CODE,TSPL_MCC_ROUTE_MASTER.Route_Name+' '+isnull(TSPL_EMPLOYEE_MASTER.Emp_Name,'') as RouteName,xxx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader+' '+TSPL_VLC_MASTER_HEAD.VLC_Name as VLCName,TabStart.DOS"
                For ii As Integer = 0 To arrRange.Count - 1
                    Key = arrRange.Keys(ii)
                    obj = arrRange(Key)

                    Qry += ",cast([" + Key + " Qty] as decimal(18,2)) as [" + Key + " Qty]" + Environment.NewLine

                    If chkShowColor.Checked Then
                        If ii > 0 Then
                            KeyPrevious = arrRange.Keys(ii - 1)
                            Qry += ",cast(([" + KeyPrevious + " Qty]-[" + Key + " Qty]) as decimal(18,2)) as [" + Key + " Diff]" + Environment.NewLine
                        End If
                    End If

                    If cbkShowFATSNF.Checked Then
                        Qry += ",cast([" + Key + " FAT] as decimal(18,2)) as [" + Key + " FAT]" + Environment.NewLine
                        Qry += ",cast([" + Key + " SNF] as decimal(18,2)) as [" + Key + " SNF]" + Environment.NewLine
                    End If
                Next
                Qry += ",case when TSPL_VLC_MASTER_HEAD.Active=0 then 'Closed' else '' end as Remarks from (
select max(ROUTE_CODE) as ROUTE_CODE,VLC_CODE"

                For ii As Integer = 0 To arrRange.Count - 1
                    Key = arrRange.Keys(ii)
                    obj = arrRange(Key)

                    Qry += ",SUM(Qty * (Case When xx.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(obj.FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and xx.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end)) as [" + Key + " Qty] "
                    Qry += ",SUM(FAT_KG * (Case When xx.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(obj.FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and xx.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end)) as [" + Key + " FAT] "
                    Qry += ",SUM(SNF_KG * (Case When xx.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(obj.FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and xx.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' then 1 else 0 end)) as [" + Key + " SNF] "
                Next
                Qry += "  from (

select TSPL_MILK_SRN_HEAD.ROUTE_CODE,TSPL_MILK_SRN_HEAD.VLC_CODE,TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.FAT_KG,TSPL_MILK_SRN_DETAIL.SNF_KG from TSPL_MILK_SRN_DETAIL
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE
where TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
and TSPL_MILK_SRN_HEAD.MCC_CODE='" + txtMCC.Value + "'"
                If txtSupervisor.arrValueMember IsNot Nothing AndAlso txtSupervisor.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_MCC_ROUTE_MASTER.Supervisor_Name in (" + clsCommon.GetMulcallString(txtSupervisor.arrValueMember) + ")"
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_MILK_SRN_HEAD.ROUTE_CODE in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_MILK_SRN_HEAD.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")"
                End If

                Qry += ")xx group by VLC_CODE
)xxx

left outer join ( select VLC_CODE,SUBSTRING( replace(convert(varchar, min(DOC_DATE),106),' ','/'),4,8) as DOS from TSPL_MILK_SRN_HEAD group by VLC_CODE)  as TabStart on TabStart.VLC_CODE=xxx.VLC_CODE
left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=xxx.ROUTE_CODE
left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_MCC_ROUTE_MASTER.Supervisor_Name
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xxx.VLC_CODE
order by TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader"
                dt = Nothing
                dt = clsDBFuncationality.GetDataTable(Qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("No Data Found to Display")
                End If
                AddTotalRowsDayWiseTrend()
                Gv1.DataSource = dt
#End Region
            ElseIf clsCommon.CompairString(cboType.SelectedValue, "S") = CompairStringResult.Equal Then
#Region "MCC Sales Report"
                If True Then
                    Dim FromDate As Date = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
                    Dim ToDate As Date = FromDate
                    txtFromDate.Value = FromDate
                    arrRange = New Dictionary(Of String, clsDateRange)
                    Dim tmpFromDate As DateTime = txtFromDate.Value
                    For ii As Integer = 1 To clsCommon.myCdbl(cboCycles.SelectedValue)
                        ToDate = clsMccMaster.GetPaymentCycleToDate(txtMCC.Value, FromDate)

                        obj = New clsDateRange
                        obj.FromDate = FromDate
                        obj.ToDate = ToDate

                        arrRange.Add("Cycle-" + clsCommon.myCstr(ii) + " (" + clsCommon.myCstr(FromDate.Day) + "-" + clsCommon.GetPrintDate(ToDate, "dd.MM.yyyy") + ")", obj)
                        FromDate = ToDate.AddDays(1)
                    Next
                    txtToDate.Value = ToDate

                    obj = New clsDateRange
                    obj.FromDate = txtFromDate.Value
                    obj.ToDate = txtToDate.Value

                    arrRange.Add("Total- (" + clsCommon.GetPrintDate(obj.FromDate, "dd.MM.yyyy") + "-" + clsCommon.GetPrintDate(obj.ToDate, "dd.MM.yyyy") + ")", obj)



                    Dim BaseQry As String = "select TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE,TSPL_SD_SHIPMENT_HEAD.Document_Date,TSPL_SD_SHIPMENT_HEAD.Customer_Code,TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code as VLC_CODE ,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,(TSPL_ITEM_MASTER.Alies_Name +' In '+TSPL_SD_SHIPMENT_DETAIL.Unit_code) as ItemName,TSPL_SD_SHIPMENT_DETAIL.Qty, TSPL_SD_SHIPMENT_DETAIL.Unit_code
from TSPL_SD_SHIPMENT_DETAIL 
left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.DOCUMENT_CODE=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_DETAIL.Item_Code
left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code
where TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' and TSPL_SD_SHIPMENT_HEAD.Status=1 and Row_Type='Item' and TSPL_SD_SHIPMENT_HEAD.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_SD_SHIPMENT_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
and TSPL_SD_SHIPMENT_HEAD.Bill_To_Location='" + txtMCC.Value + "' "


                    Dim Qry As String = "select distinct ItemName from (" + BaseQry + ")xx"
                    dt = clsDBFuncationality.GetDataTable(Qry)
                    dt = Nothing
                    dt = clsDBFuncationality.GetDataTable(Qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("No Data Found to Display")
                    End If
                    arrItem = New List(Of String)
                    For Each dr As DataRow In dt.Rows
                        arrItem.Add(clsCommon.myCstr(dr("ItemName")))
                    Next

                    Qry = "Select ROW_NUMBER() over (Partition by TSPL_VLC_MASTER_HEAD.ROUTE_CODE order by TSPL_VLC_MASTER_HEAD.ROUTE_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader ) As SNO ,TSPL_VLC_MASTER_HEAD.ROUTE_CODE,TSPL_MCC_ROUTE_MASTER.Route_Name+' '+isnull(TSPL_EMPLOYEE_MASTER.Emp_Name,'') as RouteName,xxx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader+' '+TSPL_VLC_MASTER_HEAD.VLC_Name as VLCName "
                    For ii As Integer = 0 To arrRange.Count - 1
                        Key = arrRange.Keys(ii)
                        obj = arrRange(Key)
                        For Each strItem As String In arrItem
                            Qry += ",cast([" + Key + " " + strItem + "] as decimal(18,0)) as [" + Key + " " + strItem + "]" + Environment.NewLine
                        Next
                    Next
                    Qry += ",Case When TSPL_VLC_MASTER_HEAD.Active=0 Then 'Closed' else '' end as Remarks from (
Select VLC_CODE"

                    For ii As Integer = 0 To arrRange.Count - 1
                        Key = arrRange.Keys(ii)
                        obj = arrRange(Key)
                        For Each strItem As String In arrItem
                            Qry += ",SUM(Qty * (Case When xx.Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(obj.FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and xx.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(obj.ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and xx.ItemName='" + strItem + "'  then 1 else 0 end)) as [" + Key + " " + strItem + "] "
                        Next

                    Next

                    Qry += "  from (" + BaseQry + ")xx group by VLC_CODE
)xxx
inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=xxx.VLC_CODE
left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code
left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_MCC_ROUTE_MASTER.Supervisor_Name
Where 2=2 "
                    If txtSupervisor.arrValueMember IsNot Nothing AndAlso txtSupervisor.arrValueMember.Count > 0 Then
                        Qry += " and TSPL_MCC_ROUTE_MASTER.Supervisor_Name in (" + clsCommon.GetMulcallString(txtSupervisor.arrValueMember) + ")"
                    End If
                    If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                        Qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                    End If
                    If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                        Qry += " and TSPL_VLC_MASTER_HEAD.VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")"
                    End If
                    Qry += " order by TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader"


                    dt = Nothing
                    dt = clsDBFuncationality.GetDataTable(Qry)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("No Data Found to Display")
                    End If
                    AddTotalRowsSale()
                    Gv1.DataSource = dt
                End If

#End Region
            End If
            SetGridFormat(Gv1)
            EnableDisaableControls(False)

            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddTotalRowsSale()
        arr = New Dictionary(Of Integer, DataRow)
        Dim Total As Decimal = 0
        Dim TotalQty As Decimal = 0
        Dim strPrevious As String = clsCommon.myCstr(dt.Rows(0)("ROUTE_CODE"))
        Dim drGrandTotal As DataRow = dt.NewRow()
        Dim drTotal As DataRow = dt.NewRow()
        For jj As Integer = 0 To arrRange.Count - 1
            For Each strItem As String In arrItem
                drTotal(arrRange.Keys(jj) + " " + strItem + "") = 0
                drGrandTotal(arrRange.Keys(jj) + " " + strItem + "") = 0
            Next
        Next


        For ii As Integer = 0 To dt.Rows.Count - 1
            Dim flag As Boolean = False
            If clsCommon.CompairString(strPrevious, clsCommon.myCstr(dt.Rows(ii)("ROUTE_CODE"))) = CompairStringResult.Equal Then
                For jj As Integer = 0 To arrRange.Count - 1
                    For Each strItem As String In arrItem
                        drTotal(arrRange.Keys(jj) + " " + strItem + "") += dt.Rows(ii)(arrRange.Keys(jj) + " " + strItem + "")
                    Next
                Next
            Else
                drTotal("ROUTE_CODE") = strPrevious
                drTotal("VLCName") = "Total"

                For jj As Integer = 0 To arrRange.Count - 1
                    For Each strItem As String In arrItem
                        drGrandTotal(arrRange.Keys(jj) + " " + strItem + "") += drTotal(arrRange.Keys(jj) + " " + strItem + "")
                    Next
                Next
                arr.Add(ii, drTotal)

                drTotal = dt.NewRow()
                For jj As Integer = 0 To arrRange.Count - 1
                    For Each strItem As String In arrItem
                        drTotal(arrRange.Keys(jj) + " " + strItem + "") = 0
                    Next
                Next

                For jj As Integer = 0 To arrRange.Count - 1
                    For Each strItem As String In arrItem
                        drTotal(arrRange.Keys(jj) + " " + strItem + "") += dt.Rows(ii)(arrRange.Keys(jj) + " " + strItem + "")
                    Next
                Next
            End If
            strPrevious = clsCommon.myCstr(dt.Rows(ii)("ROUTE_CODE"))
            If dt.Rows.Count - 1 = ii Then
                drTotal("ROUTE_CODE") = strPrevious
                drTotal("VLCName") = "Total"

                drGrandTotal("ROUTE_CODE") = ""
                drGrandTotal("VLCName") = "Grand Total"
                For jj As Integer = 0 To arrRange.Count - 1
                    For Each strItem As String In arrItem
                        drGrandTotal(arrRange.Keys(jj) + " " + strItem + "") += drTotal(arrRange.Keys(jj) + " " + strItem + "")
                    Next
                Next
                arr.Add(ii + 1, drTotal)
            End If
        Next

        For ii As Integer = arr.Count - 1 To 0 Step -1
            Dim Key As Integer = arr.Keys(ii)
            dt.Rows.InsertAt(arr(Key), Key)
            If ii < arr.Count - 1 Then
                drTotal = dt.NewRow()
                drTotal("VLCName") = dt.Rows(Key + 1)("RouteName")

                dt.Rows.InsertAt(drTotal, Key + 1)

                If ii = 0 Then
                    drTotal = dt.NewRow()
                    drTotal("VLCName") = dt.Rows(0)("RouteName")

                    dt.Rows.InsertAt(drTotal, 0)
                End If
            End If
        Next
        dt.Rows.Add(drGrandTotal)
    End Sub

    Private Sub AddTotalRowsDayWiseTrend()
        arr = New Dictionary(Of Integer, DataRow)
        Dim Total As Decimal = 0
        Dim TotalQty As Decimal = 0
        Dim strPrevious As String = clsCommon.myCstr(dt.Rows(0)("ROUTE_CODE"))
        Dim drGrandTotal As DataRow = dt.NewRow()
        Dim drTotal As DataRow = dt.NewRow()
        For jj As Integer = 0 To arrRange.Count - 1
            drTotal(arrRange.Keys(jj) + " Qty") = 0
            drGrandTotal(arrRange.Keys(jj) + " Qty") = 0
            If cbkShowFATSNF.Checked Then
                drTotal(arrRange.Keys(jj) + " FAT") = 0
                drTotal(arrRange.Keys(jj) + " SNF") = 0
                drGrandTotal(arrRange.Keys(jj) + " FAT") = 0
                drGrandTotal(arrRange.Keys(jj) + " SNF") = 0
            End If
        Next


        For ii As Integer = 0 To dt.Rows.Count - 1
            Dim flag As Boolean = False
            If clsCommon.CompairString(strPrevious, clsCommon.myCstr(dt.Rows(ii)("ROUTE_CODE"))) = CompairStringResult.Equal Then
                For jj As Integer = 0 To arrRange.Count - 1
                    drTotal(arrRange.Keys(jj) + " Qty") += dt.Rows(ii)(arrRange.Keys(jj) + " Qty")
                    If cbkShowFATSNF.Checked Then
                        drTotal(arrRange.Keys(jj) + " FAT") += dt.Rows(ii)(arrRange.Keys(jj) + " FAT")
                        drTotal(arrRange.Keys(jj) + " SNF") += dt.Rows(ii)(arrRange.Keys(jj) + " SNF")
                    End If
                Next
            Else
                drTotal("ROUTE_CODE") = strPrevious
                drTotal("VLCName") = "Total"


                For jj As Integer = 0 To arrRange.Count - 1
                    drGrandTotal(arrRange.Keys(jj) + " Qty") += drTotal(arrRange.Keys(jj) + " Qty")
                    If cbkShowFATSNF.Checked Then
                        drGrandTotal(arrRange.Keys(jj) + " FAT") += drTotal(arrRange.Keys(jj) + " FAT")
                        drGrandTotal(arrRange.Keys(jj) + " SNF") += drTotal(arrRange.Keys(jj) + " SNF")
                    End If
                Next
                arr.Add(ii, drTotal)

                drTotal = dt.NewRow()
                For jj As Integer = 0 To arrRange.Count - 1
                    drTotal(arrRange.Keys(jj) + " Qty") = 0
                    If cbkShowFATSNF.Checked Then
                        drTotal(arrRange.Keys(jj) + " FAT") = 0
                        drTotal(arrRange.Keys(jj) + " SNF") = 0
                    End If
                Next

                For jj As Integer = 0 To arrRange.Count - 1
                    drTotal(arrRange.Keys(jj) + " Qty") += dt.Rows(ii)(arrRange.Keys(jj) + " Qty")
                    If cbkShowFATSNF.Checked Then
                        drTotal(arrRange.Keys(jj) + " FAT") += dt.Rows(ii)(arrRange.Keys(jj) + " FAT")
                        drTotal(arrRange.Keys(jj) + " SNF") += dt.Rows(ii)(arrRange.Keys(jj) + " SNF")
                    End If
                Next
            End If
            strPrevious = clsCommon.myCstr(dt.Rows(ii)("ROUTE_CODE"))
            If dt.Rows.Count - 1 = ii Then
                drTotal("ROUTE_CODE") = strPrevious
                drTotal("VLCName") = "Total"


                drGrandTotal("ROUTE_CODE") = ""
                drGrandTotal("VLCName") = "Grand Total"
                For jj As Integer = 0 To arrRange.Count - 1
                    drGrandTotal(arrRange.Keys(jj) + " Qty") += drTotal(arrRange.Keys(jj) + " Qty")
                    If cbkShowFATSNF.Checked Then
                        drGrandTotal(arrRange.Keys(jj) + " FAT") += drTotal(arrRange.Keys(jj) + " FAT")
                        drGrandTotal(arrRange.Keys(jj) + " SNF") += drTotal(arrRange.Keys(jj) + " SNF")
                    End If
                Next
                arr.Add(ii + 1, drTotal)
            End If
        Next

        For ii As Integer = arr.Count - 1 To 0 Step -1
            Dim Key As Integer = arr.Keys(ii)
            dt.Rows.InsertAt(arr(Key), Key)
            If ii < arr.Count - 1 Then
                drTotal = dt.NewRow()
                drTotal("VLCName") = dt.Rows(Key + 1)("RouteName")

                dt.Rows.InsertAt(drTotal, Key + 1)

                If ii = 0 Then
                    drTotal = dt.NewRow()
                    drTotal("VLCName") = dt.Rows(0)("RouteName")

                    dt.Rows.InsertAt(drTotal, 0)
                End If
            End If
        Next
        dt.Rows.Add(drGrandTotal)

        Dim drGrowth As DataRow = dt.NewRow()
        For jj As Integer = 1 To arrRange.Count - 1
            drGrowth(arrRange.Keys(jj) + " Qty") = Math.Round(clsCommon.myCDivide((clsCommon.myCdbl(drGrandTotal(arrRange.Keys(jj) + " Qty")) - clsCommon.myCdbl(drGrandTotal(arrRange.Keys(jj - 1) + " Qty"))), clsCommon.myCdbl(drGrandTotal(arrRange.Keys(jj - 1) + " Qty"))) * 100, 2, MidpointRounding.AwayFromZero)
        Next
        drGrowth("ROUTE_CODE") = ""
        drGrowth("VLCName") = "Growth"
        dt.Rows.Add(drGrowth)
    End Sub

    Sub AddTotalRows()
        arr = New Dictionary(Of Integer, DataRow)
        Dim Total As Decimal = 0
        Dim TotalQty As Decimal = 0
        Dim strPrevious As String = clsCommon.myCstr(dt.Rows(0)("ROUTE_CODE"))
        Dim drGrandTotal As DataRow = dt.NewRow()
        Dim drTotal As DataRow = dt.NewRow()
        For jj As Integer = 0 To arrRange.Count - 1
            drTotal(arrRange.Keys(jj) + " Qty") = 0
            drTotal(arrRange.Keys(jj) + " Avg") = 0
            drTotal(arrRange.Keys(jj) + " MP") = 0

            drGrandTotal(arrRange.Keys(jj) + " Qty") = 0
            drGrandTotal(arrRange.Keys(jj) + " Avg") = 0
            drGrandTotal(arrRange.Keys(jj) + " MP") = 0
        Next


        For ii As Integer = 0 To dt.Rows.Count - 1
            Dim flag As Boolean = False
            If clsCommon.CompairString(strPrevious, clsCommon.myCstr(dt.Rows(ii)("ROUTE_CODE"))) = CompairStringResult.Equal Then
                For jj As Integer = 0 To arrRange.Count - 1
                    Key = arrRange.Keys(jj)
                    obj = arrRange(Key)

                    drTotal(arrRange.Keys(jj) + " Qty") += dt.Rows(ii)(arrRange.Keys(jj) + " Qty")
                    drTotal(arrRange.Keys(jj) + " Avg") = Math.Round(clsCommon.myCDivide(drTotal(arrRange.Keys(jj) + " Qty"), obj.Days), 2)
                    drTotal(arrRange.Keys(jj) + " MP") += dt.Rows(ii)(arrRange.Keys(jj) + " MP")
                Next
            Else
                drTotal("ROUTE_CODE") = strPrevious
                drTotal("VLCName") = "Total"
                For jj As Integer = 1 To arrRange.Count - 1
                    drTotal(arrRange.Keys(jj) + " INCDEC") = Math.Round(clsCommon.myCDivide((drTotal(arrRange.Keys(jj) + " Avg") - drTotal(arrRange.Keys(jj - 1) + " Avg")) * 100, drTotal(arrRange.Keys(jj - 1) + " Avg")), 0, MidpointRounding.ToEven)
                Next

                For jj As Integer = 0 To arrRange.Count - 1
                    Key = arrRange.Keys(jj)
                    obj = arrRange(Key)
                    drGrandTotal(arrRange.Keys(jj) + " Qty") += drTotal(arrRange.Keys(jj) + " Qty")
                    drGrandTotal(arrRange.Keys(jj) + " Avg") = Math.Round(clsCommon.myCDivide(drGrandTotal(arrRange.Keys(jj) + " Qty"), obj.Days), 2)
                    drGrandTotal(arrRange.Keys(jj) + " MP") += drTotal(arrRange.Keys(jj) + " MP")
                Next
                arr.Add(ii, drTotal)

                drTotal = dt.NewRow()
                For jj As Integer = 0 To arrRange.Count - 1
                    drTotal(arrRange.Keys(jj) + " Qty") = 0
                    drTotal(arrRange.Keys(jj) + " Avg") = 0
                    drTotal(arrRange.Keys(jj) + " MP") = 0
                Next

                For jj As Integer = 0 To arrRange.Count - 1
                    Key = arrRange.Keys(jj)
                    obj = arrRange(Key)
                    drTotal(arrRange.Keys(jj) + " Qty") += dt.Rows(ii)(arrRange.Keys(jj) + " Qty")
                    drTotal(arrRange.Keys(jj) + " Avg") = Math.Round(clsCommon.myCDivide(drTotal(arrRange.Keys(jj) + " Qty"), obj.Days), 2)
                    drTotal(arrRange.Keys(jj) + " MP") += dt.Rows(ii)(arrRange.Keys(jj) + " MP")
                Next
            End If
            strPrevious = clsCommon.myCstr(dt.Rows(ii)("ROUTE_CODE"))
            If dt.Rows.Count - 1 = ii Then
                drTotal("ROUTE_CODE") = strPrevious
                drTotal("VLCName") = "Total"
                For jj As Integer = 1 To arrRange.Count - 1
                    drTotal(arrRange.Keys(jj) + " INCDEC") = Math.Round(clsCommon.myCDivide((drTotal(arrRange.Keys(jj) + " Avg") - drTotal(arrRange.Keys(jj - 1) + " Avg")) * 100, drTotal(arrRange.Keys(jj - 1) + " Avg")), 0, MidpointRounding.ToEven)
                Next

                drGrandTotal("ROUTE_CODE") = ""
                drGrandTotal("VLCName") = "Grand Total"
                For jj As Integer = 0 To arrRange.Count - 1
                    Key = arrRange.Keys(jj)
                    obj = arrRange(Key)

                    drGrandTotal(arrRange.Keys(jj) + " Qty") += drTotal(arrRange.Keys(jj) + " Qty")
                    drGrandTotal(arrRange.Keys(jj) + " Avg") = Math.Round(clsCommon.myCDivide(drGrandTotal(arrRange.Keys(jj) + " Qty"), obj.Days), 2)
                    drGrandTotal(arrRange.Keys(jj) + " MP") += drTotal(arrRange.Keys(jj) + " MP")
                    If jj > 0 Then
                        drGrandTotal(arrRange.Keys(jj) + " INCDEC") = Math.Round(clsCommon.myCDivide((drGrandTotal(arrRange.Keys(jj) + " Avg") - drGrandTotal(arrRange.Keys(jj - 1) + " Avg")) * 100, drGrandTotal(arrRange.Keys(jj - 1) + " Avg")), 0, MidpointRounding.ToEven)
                    End If
                Next
                arr.Add(ii + 1, drTotal)
            End If
        Next

        For ii As Integer = arr.Count - 1 To 0 Step -1
            Dim Key As Integer = arr.Keys(ii)
            dt.Rows.InsertAt(arr(Key), Key)
            If ii < arr.Count - 1 Then
                drTotal = dt.NewRow()
                drTotal("VLCName") = dt.Rows(Key + 1)("RouteName")

                dt.Rows.InsertAt(drTotal, Key + 1)

                If ii = 0 Then
                    drTotal = dt.NewRow()
                    drTotal("VLCName") = dt.Rows(0)("RouteName")

                    dt.Rows.InsertAt(drTotal, 0)
                End If
            End If
        Next
        dt.Rows.Add(drGrandTotal)
    End Sub
    Sub SetGridFormat(ByRef Gv1 As RadGridView)
        Gv1.ViewDefinition = New TableViewDefinition()
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = False
        Gv1.ShowFilteringRow = False
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).IsVisible = False
            Gv1.Columns(ii).ReadOnly = True
            'Gv1.Columns(ii).BestFit()
        Next
        If clsCommon.CompairString(cboType.SelectedValue, "V") = CompairStringResult.Equal Then
            Gv1.Columns("SNO").HeaderText = "SNo"
            Gv1.Columns("SNO").IsVisible = True
            Gv1.Columns("SNO").BestFit()

            Gv1.Columns("RouteName").HeaderText = "Route"
            Gv1.Columns("RouteName").IsVisible = False
            Gv1.Columns("RouteName").BestFit()

            Gv1.Columns("VLCName").HeaderText = "VLC Name"
            Gv1.Columns("VLCName").IsVisible = True
            Gv1.Columns("VLCName").BestFit()

            Gv1.Columns("DOS").HeaderText = "Start Date"
            Gv1.Columns("DOS").IsVisible = False
            Gv1.Columns("DOS").BestFit()

            For jj As Integer = 0 To arrRange.Count - 1
                Gv1.Columns(arrRange.Keys(jj) + " Qty").HeaderText = "Qty"
                Gv1.Columns(arrRange.Keys(jj) + " Qty").IsVisible = True
                Gv1.Columns(arrRange.Keys(jj) + " Qty").BestFit()

                Gv1.Columns(arrRange.Keys(jj) + " Avg").HeaderText = "Avg"
                Gv1.Columns(arrRange.Keys(jj) + " Avg").IsVisible = True
                Gv1.Columns(arrRange.Keys(jj) + " Avg").BestFit()

                Gv1.Columns(arrRange.Keys(jj) + " MP").HeaderText = "MP"
                Gv1.Columns(arrRange.Keys(jj) + " MP").IsVisible = True
                Gv1.Columns(arrRange.Keys(jj) + " MP").BestFit()

                If jj > 0 Then
                    Gv1.Columns(arrRange.Keys(jj) + " INCDEC").HeaderText = "Inc/Dec"
                    Gv1.Columns(arrRange.Keys(jj) + " INCDEC").IsVisible = True
                    Gv1.Columns(arrRange.Keys(jj) + " INCDEC").BestFit()
                End If
            Next

            Gv1.Columns("Remarks").HeaderText = "Remarks"
            Gv1.Columns("Remarks").IsVisible = True
            Gv1.Columns("Remarks").BestFit()


            Gv1.AutoSizeRows = False
            Gv1.BestFitColumns()
            Gv1.MasterTemplate.AutoExpandGroups = True




            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("SNO").Name)
            'view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("RouteName"))
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("VLCName").Name)
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("DOS").Name)

            For jj As Integer = 0 To arrRange.Count - 1
                view.ColumnGroups.Add(New GridViewColumnGroup(arrRange.Keys(jj).Replace(" ", "\")))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns(arrRange.Keys(jj) + " Qty").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns(arrRange.Keys(jj) + " Avg").Name)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns(arrRange.Keys(jj) + " MP").Name)
                If jj > 0 Then
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns(arrRange.Keys(jj) + " INCDEC").Name)
                End If
            Next

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("Remarks").Name)

            Gv1.ViewDefinition = view
        ElseIf clsCommon.CompairString(cboType.SelectedValue, "D") = CompairStringResult.Equal Then
            Gv1.Columns("SNO").HeaderText = "SNo"
            Gv1.Columns("SNO").IsVisible = True
            Gv1.Columns("SNO").BestFit()

            Gv1.Columns("RouteName").HeaderText = "Route"
            Gv1.Columns("RouteName").IsVisible = False
            Gv1.Columns("RouteName").BestFit()

            Gv1.Columns("VLCName").HeaderText = "DCS Name"
            Gv1.Columns("VLCName").IsVisible = True
            Gv1.Columns("VLCName").BestFit()

            Gv1.Columns("DOS").HeaderText = "Start Date"
            Gv1.Columns("DOS").IsVisible = False
            Gv1.Columns("DOS").BestFit()

            For jj As Integer = 0 To arrRange.Count - 1
                Gv1.Columns(arrRange.Keys(jj) + " Qty").HeaderText = "Qty"
                Gv1.Columns(arrRange.Keys(jj) + " Qty").IsVisible = True
                If cbkShowFATSNF.Checked Then
                    Gv1.Columns(arrRange.Keys(jj) + " Qty").BestFit()

                    Gv1.Columns(arrRange.Keys(jj) + " FAT").HeaderText = "FAT"
                    Gv1.Columns(arrRange.Keys(jj) + " FAT").IsVisible = True
                    Gv1.Columns(arrRange.Keys(jj) + " FAT").BestFit()

                    Gv1.Columns(arrRange.Keys(jj) + " SNF").HeaderText = "SNF"
                    Gv1.Columns(arrRange.Keys(jj) + " SNF").IsVisible = True
                    Gv1.Columns(arrRange.Keys(jj) + " SNF").BestFit()
                Else
                    Gv1.Columns(arrRange.Keys(jj) + " Qty").Width = 80
                End If


            Next

            Gv1.Columns("Remarks").HeaderText = "Remarks"
            Gv1.Columns("Remarks").IsVisible = True
            Gv1.Columns("Remarks").BestFit()


            Gv1.AutoSizeRows = False
            'Gv1.BestFitColumns()
            Gv1.MasterTemplate.AutoExpandGroups = True


            If chkShowColor.Checked Then
                For ii As Integer = 1 To arrRange.Count - 1
                    Dim KeyPrevious As String = arrRange.Keys(ii - 1)
                    Key = arrRange.Keys(ii)

                    Dim obj2 As New ExpressionFormattingObject("MyCondition2", "[" + Key + " Diff]>" + clsCommon.myCstr(txtColorChangeQty.Value) + "", False)
                    obj2.CellBackColor = Color.LightPink
                    Gv1.Columns(Key + " Qty").ConditionalFormattingObjectList.Add(obj2)
                    If cbkShowFATSNF.Checked Then
                        Gv1.Columns(Key + " FAT").ConditionalFormattingObjectList.Add(obj2)
                        Gv1.Columns(Key + " SNF").ConditionalFormattingObjectList.Add(obj2)
                    End If

                    Dim obj3 As New ExpressionFormattingObject("MyCondition3", "[" + Key + " Diff]<-" + clsCommon.myCstr(txtColorChangeQty.Value) + "", False)
                    obj3.CellBackColor = Color.PaleGreen
                    Gv1.Columns(Key + " Qty").ConditionalFormattingObjectList.Add(obj3)
                    If cbkShowFATSNF.Checked Then
                        Gv1.Columns(Key + " FAT").ConditionalFormattingObjectList.Add(obj3)
                        Gv1.Columns(Key + " SNF").ConditionalFormattingObjectList.Add(obj3)
                    End If

                    Dim obj1 As New ConditionalFormattingObject("MyCondition1", ConditionTypes.Equal, "0", "", False)
                    obj1.CellBackColor = Color.OrangeRed
                    Gv1.Columns(Key + " Qty").ConditionalFormattingObjectList.Add(obj1)
                    If cbkShowFATSNF.Checked Then
                        Gv1.Columns(Key + " FAT").ConditionalFormattingObjectList.Add(obj1)
                        Gv1.Columns(Key + " SNF").ConditionalFormattingObjectList.Add(obj1)
                    End If


                    Gv1.Columns(Key + " Diff").IsVisible = False

                Next
            End If


            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("SNO").Name)
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("VLCName").Name)

            For jj As Integer = 0 To arrRange.Count - 1
                view.ColumnGroups.Add(New GridViewColumnGroup(arrRange.Keys(jj).Replace(" ", "\")))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns(arrRange.Keys(jj) + " Qty").Name)
                If cbkShowFATSNF.Checked Then
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns(arrRange.Keys(jj) + " FAT").Name)
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns(arrRange.Keys(jj) + " SNF").Name)
                End If
            Next

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("Remarks").Name)

            Gv1.ViewDefinition = view
        ElseIf clsCommon.CompairString(cboType.SelectedValue, "S") = CompairStringResult.Equal Then
            Gv1.Columns("SNO").HeaderText = "SNo"
            Gv1.Columns("SNO").IsVisible = True
            Gv1.Columns("SNO").BestFit()

            Gv1.Columns("RouteName").HeaderText = "Route"
            Gv1.Columns("RouteName").IsVisible = False
            Gv1.Columns("RouteName").BestFit()

            Gv1.Columns("VLCName").HeaderText = "DCS Name"
            Gv1.Columns("VLCName").IsVisible = True
            Gv1.Columns("VLCName").BestFit()


            For jj As Integer = 0 To arrRange.Count - 1
                For Each strItem As String In arrItem
                    Gv1.Columns(arrRange.Keys(jj) + " " + strItem + "").HeaderText = strItem
                    Gv1.Columns(arrRange.Keys(jj) + " " + strItem + "").IsVisible = True
                    Gv1.Columns(arrRange.Keys(jj) + " " + strItem + "").BestFit()
                Next
            Next

            Gv1.Columns("Remarks").HeaderText = "Remarks"
            Gv1.Columns("Remarks").IsVisible = True
            Gv1.Columns("Remarks").BestFit()


            Gv1.AutoSizeRows = False
            Gv1.BestFitColumns()
            Gv1.MasterTemplate.AutoExpandGroups = True




            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("SNO").Name)
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("VLCName").Name)

            For jj As Integer = 0 To arrRange.Count - 1
                view.ColumnGroups.Add(New GridViewColumnGroup(arrRange.Keys(jj)))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
                For Each strItem As String In arrItem
                    view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns(arrRange.Keys(jj) + " " + strItem + "").Name)
                Next
            Next

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(Gv1.Columns("Remarks").Name)

            Gv1.ViewDefinition = view
        End If
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisaableControls(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub EnableDisaableControls(ByVal flag As Boolean)
        txtMCC.Enabled = flag
        txtSupervisor.Enabled = flag
        txtRoute.Enabled = flag
        txtVLC.Enabled = flag
        txtFromDate.Enabled = flag
        txtToDate.Enabled = flag
        cboType.Enabled = flag
        chkShowColor.Enabled = flag
        cbkShowFATSNF.Enabled = flag
        cboCycles.Enabled = flag
        txtColorChangeQty.Enabled = flag
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = cboType.SelectedText

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Mcc : " + clsCommon.myCstr(txtMCC.Tag))
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            If exporter = EnumExportTo.Excel Then
                transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Private Sub RmiExcelGrid_Click(sender As Object, e As EventArgs) Handles rmiExcelGrid.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If

            Dim strHeading As String = clsCommon.myCstr("MP Incetive Entry Register")

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Mcc : " + txtMCC.Value)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "MM/yyyy"))


            clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text, True)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Try
            Dim qry As String = "select MCC_Code as Code,MCC_NAME as Name,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
            txtMCC.Value = clsCommon.ShowSelectForm("vbaMccm", qry, "Code", "", txtMCC.Value, "Code", isButtonClicked)
            txtMCC.Tag = clsMccMaster.GetName(txtMCC.Value, Nothing)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub cboCycles_Validated(sender As Object, e As EventArgs) Handles cboCycles.Validated
        If clsCommon.CompairString(cboType.SelectedValue, "V") = CompairStringResult.Equal Then
            LoadGridDate()
        ElseIf clsCommon.CompairString(cboType.SelectedValue, "D") = CompairStringResult.Equal Then
            SetToDate()
        ElseIf clsCommon.CompairString(cboType.SelectedValue, "S") = CompairStringResult.Equal Then
            SetToDate()
        End If
    End Sub

    Private Sub LoadGridDate()
        gvDate.DataSource = Nothing
        gvDate.Rows.Clear()
        gvDate.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvDate.MasterTemplate.Columns.Add(repoSelect)

        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "From Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colFromDate
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = False
        repoExpiry.Width = 120
        gvDate.MasterTemplate.Columns.Add(repoExpiry)

        repoExpiry = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "To Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colToDate
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = False
        repoExpiry.Width = 120
        gvDate.MasterTemplate.Columns.Add(repoExpiry)



        gvDate.ShowFilteringRow = False
        gvDate.EnableFiltering = False
        gvDate.AllowDeleteRow = False
        gvDate.AllowAddNewRow = False
        gvDate.ShowGroupPanel = False
        gvDate.AllowColumnReorder = False
        gvDate.AllowRowReorder = False
        gvDate.EnableSorting = False
        gvDate.EnableAlternatingRowColor = True
        gvDate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvDate.MasterTemplate.ShowRowHeaderColumn = False
        'gvDate.MasterTemplate.ShowColumnHeaders = False

        For ii As Integer = 1 To clsCommon.myCdbl(cboCycles.SelectedValue)
            gvDate.Rows.AddNew()
        Next
    End Sub

    Private Sub txtFromDate_Validated(sender As Object, e As EventArgs) Handles txtFromDate.Validated
        SetToDate()
    End Sub

    Private Sub SetToDate()
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "S") = CompairStringResult.Equal Then
                Dim tmpFromDate As DateTime = txtFromDate.Value
                For ii As Integer = 1 To clsCommon.myCdbl(cboCycles.SelectedValue)
                    txtToDate.Value = clsMccMaster.GetPaymentCycleToDate(txtMCC.Value, tmpFromDate)
                    tmpFromDate = txtToDate.Value.AddDays(1)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtSupervisor__My_Click(sender As Object, e As EventArgs) Handles txtSupervisor._My_Click
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            Dim qry As String = "select xxx.SupervisorCode,TSPL_EMPLOYEE_MASTER.Emp_Name as SupervisorName
from (
select Supervisor_Name  as SupervisorCode
from TSPL_MCC_ROUTE_MASTER
where len(isnull(Supervisor_Name,''))>0 group by Supervisor_Name
)xxx
left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=xxx.SupervisorCode "
            txtSupervisor.arrValueMember = clsCommon.ShowMultipleSelectForm("VLCPRS", qry, "SupervisorCode", "SupervisorName", txtSupervisor.arrValueMember, txtSupervisor.arrDispalyMember)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where 2=2 "
            qry += "  and MCC_Code = '" + txtMCC.Value + "'"
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("VLCPRR", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [Uploader Code],TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1' "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If
            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("VLCPRV", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshVLC()
        If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  and Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVLC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                Next
                txtVLC.arrValueMember = arr
            End If
        End If
    End Sub

    Private Sub gvDate_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvDate.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gvDate.Columns(colFromDate) Then
                    gvDate.CurrentRow.Cells(colFromDate).ReadOnly = Not clsCommon.myCBool(gvDate.CurrentRow.Cells(colSelect).Value)
                ElseIf e.Column Is gvDate.Columns(colToDate) Then
                    gvDate.CurrentRow.Cells(colToDate).ReadOnly = Not clsCommon.myCBool(gvDate.CurrentRow.Cells(colSelect).Value)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class


