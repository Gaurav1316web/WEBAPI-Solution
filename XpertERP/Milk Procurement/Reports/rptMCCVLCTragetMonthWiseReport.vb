Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports System.Text
Public Class RptMCCVLCTragetMonthWiseReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMCCVLCTragetMonthWiseReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub LoadMCCRouteVLCTree()
        'Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER"
        'cbtMCCRouteVLCC.ValueMember = "Code"
        'cbtMCCRouteVLCC.DisplayMember = "Name"
        'cbtMCCRouteVLCC.ParentValue = "ParentCode"
        'cbtMCCRouteVLCC.DataSource = clsDBFuncationality.GetDataTable(qry)

        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code as ParentCode,3 as Lvl from TSPL_VLC_MASTER_HEAD where len(isnull(TSPL_VLC_MASTER_HEAD.Route_Code,''))>0 union all   select TSPL_MCC_ROUTE_MASTER.Route_Code as Code,TSPL_MCC_ROUTE_MASTER.Route_Name as Name,TSPL_MCC_ROUTE_MASTER.MCC_Code as ParentCode,2 as Lvl from TSPL_MCC_ROUTE_MASTER where len(isnull(TSPL_MCC_ROUTE_MASTER.MCC_Code,''))>0  union all   select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name,null as ParentCode,1 as Lvl from TSPL_MCC_MASTER   where TSPL_MCC_MASTER.MCC_Code in (" + arrLoc + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbtMCCRouteVLCC.DataSource = dt
            cbtMCCRouteVLCC.ValueMember = "Code"
            cbtMCCRouteVLCC.DisplayMember = "Name"
            cbtMCCRouteVLCC.ParentValue = "ParentCode"
        End If
    End Sub
    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

        RadGroupBox2.Enabled = val
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "   " + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                If chkDetail.Checked = True Then
                    arrHeader.Add("Report Type : " + "Detail")
                ElseIf chkSummaryRouteWise.Checked = True Then
                    arrHeader.Add("Report Type : " + "Route Wise Summary")
                ElseIf chkMCCWiseSummary.Checked = True Then
                    arrHeader.Add("Report Type : " + "MCC Wise Summary")
                End If
                'If rbtnMCCRouteVLCCSelect.IsChecked Then
                Dim arr As List(Of String)
                If cbtMCCRouteVLCC.CheckedText.Count > 0 Then
                    arr = cbtMCCRouteVLCC.CheckedText(1)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 1 Then
                    arr = cbtMCCRouteVLCC.CheckedText(2)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                If cbtMCCRouteVLCC.CheckedText.Count > 2 Then
                    arr = cbtMCCRouteVLCC.CheckedText(3)
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(arr) + " "))
                    End If
                End If
                'End If


                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("VLC Target Report", gv1, arrHeader, Me.Text, True)
                Else
                    clsCommon.MyExportToPDF("VLC Target Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    Private Sub RptMCCVLCTragetMonthWiseReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtFromMonth.Value = clsCommon.GETSERVERDATE()
        LoadMCCRouteVLCTree()
        chkDetail.Checked = True
        LoadShiftFrom()
        LoadShiftTo()

        Reset()
    End Sub
    Public Sub Load_Report()
       Try
            Dim sQuery As String
            Dim dt As New DataTable
            Dim arr As List(Of String) = Nothing

            If clsCommon.CompairString(clsCommon.GetPrintDate(txtFromMonth.Value, "MMM"), clsCommon.GetPrintDate(txtFromDate.Value, "MMM")) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.GetPrintDate(txtFromMonth.Value, "MMM"), clsCommon.GetPrintDate(txtToDate.Value, "MMM")) = CompairStringResult.Equal Then
                If clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") > clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") Then
                    txtFromDate.Focus()
                    Throw New Exception("From date can not be greater then to Date")
                End If
            Else
                Throw New Exception("From date and To date should be within selected month")
            End If

            If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single MCC or select all.")

            End If




            '=============================================pivot variables================================================
            Dim strPivotForInternal As String = Nothing
            Dim strPivotForOuter As String = Nothing
            Dim StrPivotForLastDay As String = Nothing
            Dim StrPivotForLastSumDay As String = Nothing
            Dim strPivotForInternalquery As String = Nothing
            Dim strPivotForOuterquery As String = Nothing
            Dim StrPivotForLastDayquery As String = Nothing
            Dim StrPivotForLastDaySumquery As String = Nothing
            Dim StrDateDiffQuery As String = Nothing
            Dim StrDateDiff As String = Nothing
            Dim StrToatlQuery As String = Nothing
            Dim StrToatl As String = Nothing
            Dim StrToatlSumQuery As String = Nothing
            Dim StrToatlsum As String = Nothing

            Dim dtExtraColumn As DataTable = clsDBFuncationality.GetDataTable("select Datename(DAY ,thedate)+' '+DATENAME(MONTH ,thedate) from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')")


            strPivotForInternalquery = " select STUFF(a.strr,1,1,'') from ("
            strPivotForInternalquery += " select (select +',['+Datename(DAY ,thedate)+' '+DATENAME(MONTH ,thedate)+']' from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "') for xml path ('')) as strr)a "
            strPivotForInternal = clsDBFuncationality.getSingleValue(strPivotForInternalquery)

            strPivotForOuterquery = "select (select +',isnull(['+Datename(DAY ,thedate)+' '+DATENAME(MONTH ,thedate)+'],0) as ['+Datename(DAY ,thedate)+' '+DATENAME(MONTH ,thedate)+']' from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "') for xml path ('')) as strr"
            strPivotForOuter = clsDBFuncationality.getSingleValue(strPivotForOuterquery)

            StrPivotForLastDayquery = "select '['+Datename(DAY ,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')+' '+DATENAME(MONTH ,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')+']'"
            StrPivotForLastDay = clsDBFuncationality.getSingleValue(StrPivotForLastDayquery)

            StrPivotForLastDaySumquery = "select 'sum(['+Datename(DAY ,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')+' '+DATENAME(MONTH ,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')+'])'"
            StrPivotForLastSumDay = clsDBFuncationality.getSingleValue(StrPivotForLastDaySumquery)


            StrDateDiffQuery = "  select DATEDIFF(DAY ,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "')+1"
            StrDateDiff = clsDBFuncationality.getSingleValue(StrDateDiffQuery)

            StrToatlQuery = "select STUFF(a.strr,1,1,'') from ( select (select +'+isnull(['+Datename(DAY ,thedate)+' '+DATENAME(MONTH ,thedate)+'],0)' from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "') for xml path ('')) as strr)a"
            StrToatl = clsDBFuncationality.getSingleValue(StrToatlQuery)

            StrToatlSumQuery = "select STUFF(a.strr,1,1,'') from ( select (select +'+sum(isnull(['+Datename(DAY ,thedate)+' '+DATENAME(MONTH ,thedate)+'],0))' from ExplodeDates('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "','" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "') for xml path ('')) as strr)a"
            StrToatlSum = clsDBFuncationality.getSingleValue(StrToatlSumQuery)
            '================================================================end here=============================================================================

            sQuery = " select max(tspl_mcc_master.State_Code) as State_Code,max(TSPL_STATE_MASTER.STATE_NAME)  as STATE_NAME,max(tspl_mcc_master.EMP_CODE) as RM_Code,max(RM.Emp_Name) as RM_Name , max(tspl_VLC_master_Head.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,max(TSPL_MILK_RECEIPT_DETAIL .UOM_Code) as UOM_Code , max(tspl_mcc_route_master.Supervisor_Name) as Supervisor_Code,max(TSPL_EMPLOYEE_MASTER.Emp_Name) as Emp_Name, max( Datename(month,tspl_milk_receipt_head.doc_date)+' '+DATENAME(YEAR,tspl_milk_receipt_head.doc_date)) as Month_Date, "
            sQuery += "   max( Datename(DAY ,tspl_milk_receipt_head.doc_date)+' '+DATENAME(MONTH ,tspl_milk_receipt_head.doc_date)) as Day_Date, "
            '' richa agarwal changed 22July,2016
            '  sQuery += " tspl_milk_receipt_head.mcc_code, max(tspl_mcc_master.mcc_name) as mcc_name,tspl_milk_receipt_detail.route_code,max(tspl_mcc_route_master.Route_name) as Route_name,"
            ''----------------
            sQuery += " tspl_milk_receipt_head.mcc_code, max(tspl_mcc_master.mcc_name) as mcc_name,max(tspl_VLC_master_Head.Route_Code ) as route_code,max(tspl_mcc_route_master.Route_name) as Route_name,"
            sQuery += "  tspl_milk_receipt_detail.Vlc_code,max(tspl_VLC_master_Head.VLC_Name ) as VLC_Name ,sum(tspl_milk_receipt_detail.milk_weight) as ACC_WEIGHT , MAX(TSPL_Vlc_Target_Detail.Day_Target) as Day_Target"
            sQuery += " from tspl_milk_receipt_detail "
            sQuery += " inner join tspl_milk_receipt_head on tspl_milk_receipt_head.DOC_CODE =tspl_milk_receipt_detail.DOC_CODE"

            If cbtMCCRouteVLCC.CheckedValue.Count > 0 Then
                arr = cbtMCCRouteVLCC.CheckedValue(1)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and tspl_milk_receipt_head.MCC_Code  IN (" + clsCommon.GetMulcallString(arr) + ") "
                Else
                    Throw New Exception("Please select at least one MCC")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 1 Then
                arr = cbtMCCRouteVLCC.CheckedValue(2)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and tspl_milk_receipt_detail.ROUTE_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            If cbtMCCRouteVLCC.CheckedValue.Count > 2 Then
                arr = cbtMCCRouteVLCC.CheckedValue(3)
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    sQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(arr) + ")  "
                Else
                    Throw New Exception("Please select at least one VLC")
                End If
            End If

            'If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            '    sQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.SHIFT='M' then 3 else 2 end  )"
            'End If
            'If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            '    sQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.SHIFT='E' then 3 else 2 end  )"
            'End If

            sQuery += " and  convert(date,tspl_milk_receipt_head.doc_date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,tspl_milk_receipt_head.doc_date,103)<=convert(date,'" + txtToDate.Value + "',103)"
            sQuery += " Inner join tspl_mcc_master on tspl_mcc_master.mcc_code=tspl_milk_receipt_head.mcc_code"
            sQuery += " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =tspl_mcc_master.State_Code "
            sQuery += " left join TSPL_EMPLOYEE_MASTER as RM on RM .EMP_CODE =tspl_mcc_master.EMP_CODE"
            ''richa agarwal changed  22/07/2016
            'sQuery += " inner join tspl_mcc_route_master on tspl_mcc_route_master.route_code=tspl_milk_receipt_detail.route_code"
            ' sQuery += " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =tspl_mcc_route_master.Supervisor_Name"
            ''------------------
            sQuery += " inner join tspl_vendor_master on tspl_vendor_master.vendor_code=tspl_milk_receipt_detail.vsp_code" & _
            " left outer join TSPL_Vlc_Target_Detail on TSPL_Vlc_Target_Detail.mcc_code=tspl_milk_receipt_head.mcc_code " & _
             " and TSPL_Vlc_Target_Detail.vlc_code=tspl_milk_receipt_detail.vlc_code " & _
             " and TSPL_Vlc_Target_Detail.Document_Code=(select max(document_Code) as dd_cc from TSPL_Vlc_Target_Detail where convert(date,frm_date,103)<=convert(date,tspl_milk_receipt_head.doc_date,103) and convert(date,To_Date,103)>=convert(date,tspl_milk_receipt_head.doc_date,103) aND TSPL_Vlc_Target_Detail.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE group by VLC_Code)  and convert(date,tspl_milk_receipt_Head.doc_date,103) between convert(date,frm_date,103) and convert(date,to_date,103) " & _
            " and  TSPL_Vlc_Target_Detail.MP_Code  is  null " & _
            " left join tspl_MP_master on tspl_MP_master.mp_code=TSPL_Vlc_Target_Detail.MP_Code" & _
            " left outer join tspl_VLC_master_Head on tspl_VLC_master_Head.VLC_Code=tspl_milk_receipt_detail.VLC_Code " & _
            " inner join tspl_mcc_route_master on tspl_mcc_route_master.route_code=tspl_VLC_master_Head.Route_Code " & _
            " left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =tspl_mcc_route_master.Supervisor_Name where 2=2 " & _
            " group by  convert(varchar,tspl_milk_receipt_head.doc_date,103),tspl_milk_receipt_head.mcc_code,tspl_milk_receipt_detail.Vlc_code "

            ''richa agarwal changed  22/07/2016
            'sQuery += " left outer join tspl_VLC_master_Head on tspl_VLC_master_Head.VLC_Code=tspl_milk_receipt_detail.VLC_Code  where 2=2"
            'sQuery += " group by  convert(varchar,tspl_milk_receipt_head.doc_date,103),tspl_milk_receipt_head.mcc_code,tspl_milk_receipt_detail.route_code,tspl_milk_receipt_detail.Vlc_code ,TSPL_Vlc_Target_Detail.Day_Target"
            ''---------------

            Dim FinalQuery As String = sQuery

         

            '==========================
            If chkDetail.Checked Then
                FinalQuery = "select  cast(ROW_NUMBER() OVER (PARTITION BY mcc_code1,route_code1 order by mcc_code1,route_code1 ) as varchar)+'.' as SNO, xx.* ,(case when Target_MTD=0 then 0 else(ShortFall_MTD/(Target_MTD))*100 end) as per_ShortFall from (select final.*,isnull(final.ACH_MTD,0)-isnull(final.Target_MTD,0) as ShortFall_MTD from (select Supervisor_Code,Emp_Name,mcc_code as mcc_code1,route_code as route_code1,mcc_code+'  -'+ mcc_name+'  UOM-'+UOM_Code as mcc_code,mcc_name,route_code+ '  -'+route_name as route_code,route_name,VLC_Code_VLC_Uploader,vlc_code,vlc_name,isnull(day_target,0) as day_target"
                FinalQuery += " " + strPivotForOuter + " "
                FinalQuery += " ,isnull(Day_Target,0)-isnull(" + StrPivotForLastDay + ",0) as shortfall," + StrDateDiff + " * isnull(day_target,0) as Target_MTD," + StrToatl + " as ACH_MTD from ("
                FinalQuery += " " + sQuery + " "
                FinalQuery += " )  as pp"
                FinalQuery += " pivot (sum(acc_weight) for day_date in (" + strPivotForInternal + "))t) as Final) xx"


            ElseIf chkSummaryRouteWise.Checked Then
                FinalQuery = " select  cast(ROW_NUMBER() OVER (PARTITION BY mcc_code order by mcc_code ) as varchar)+'.' as SNO,final.*,(case when Target_MTD=0 then 0 else(ShortFall_MTD/(Target_MTD))*100 end) as per_ShortFall from ( "
                FinalQuery += " select aa.*,isnull(aa.ACH_MTD,0)-isnull(aa.Target_MTD,0) as ShortFall_MTD from ( "
                FinalQuery += " select max(xx.State_Code) as State_Code ,max(xx.STATE_NAME) as STATE_NAME,xx.MCC_CODE ,max(xx.mcc_name) as mcc_name ,max(xx.RM_Code) as RM_Code ,max(xx.RM_Name) as RM_Name,xx.ROUTE_CODE ,max(xx.Route_name) as Route_name ,max(xx.Supervisor_Code) as Supervisor_Code ,max(xx.Emp_Name) as Emp_Name ,sum(isnull(xx.Day_Target,0)) as day_target"
                FinalQuery += " ,isnull(" + StrPivotForLastSumDay + ",0) as shortfall," + StrDateDiff + " * sum(isnull(day_target,0)) as Target_MTD," + StrToatlsum + " as ACH_MTD  from ( "
                FinalQuery += " select * from ( "
                FinalQuery += " " + sQuery + " "
                FinalQuery += " )  as pp"
                FinalQuery += " pivot (sum(acc_weight) for day_date in (" + strPivotForInternal + "))t ) as xx group by xx.MCC_CODE,xx.ROUTE_CODE "
                FinalQuery += " ) as aa"
                FinalQuery += " ) as final"


            ElseIf chkMCCWiseSummary.Checked Then
                FinalQuery = " select  cast( ROW_NUMBER() OVER ( PARTITION BY rm_code order by mcc_code )  as varchar)+'.' as SNO,final.*,(case when Target_MTD=0 then 0 else(ShortFall_MTD/(Target_MTD))*100 end) as per_ShortFall from ( "
                FinalQuery += " select aa.*,isnull(aa.ACH_MTD,0)-isnull(aa.Target_MTD,0) as ShortFall_MTD from ( "
                FinalQuery += " select max(xx.State_Code) as State_Code ,max(xx.STATE_NAME) as STATE_NAME,max(xx.RM_Code) as RM_Code ,max(xx.RM_Name) as RM_Name,xx.MCC_CODE ,max(xx.mcc_name) as mcc_name ,max(xx.Supervisor_Code) as Supervisor_Code ,max(xx.Emp_Name) as Emp_Name ,sum(isnull(xx.Day_Target,0)) as day_target"
                FinalQuery += " ,isnull(" + StrPivotForLastSumDay + ",0) as shortfall," + StrDateDiff + " * sum(isnull(day_target,0)) as Target_MTD," + StrToatlsum + " as ACH_MTD  from ( "
                FinalQuery += " select * from ( "
                FinalQuery += " " + sQuery + " "
                FinalQuery += " )  as pp"
                FinalQuery += " pivot (sum(acc_weight) for day_date in (" + strPivotForInternal + "))t ) as xx group by xx.MCC_CODE "
                FinalQuery += " ) as aa"
                FinalQuery += " ) as final"
            End If








            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(FinalQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dtgv
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.BestFitColumns()
                FormatGrid(dtExtraColumn)

            Else
                tmpValLoad = False
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Sub FormatGrid(ByVal dtExtraColumn As DataTable)
        ' Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).Width = 80

        Next
        If chkDetail.Checked = True Then


            gv1.Columns("SNO").IsVisible = True
            gv1.Columns("SNO").Width = 40
            gv1.Columns("SNO").HeaderText = "SNO"
            'gv1.Columns("SNO").FormatString = "{0:0}"

            gv1.Columns("Supervisor_Code").IsVisible = True
            gv1.Columns("Supervisor_Code").Width = 100
            gv1.Columns("Supervisor_Code").HeaderText = "FS Code"

            gv1.Columns("Emp_Name").IsVisible = True
            gv1.Columns("Emp_Name").Width = 100
            gv1.Columns("Emp_Name").HeaderText = "FS Name"

            gv1.Columns("mcc_code").IsVisible = True
            gv1.Columns("mcc_code").Width = 100
            gv1.Columns("mcc_code").HeaderText = "MCC Code"

            gv1.Columns("mcc_code1").IsVisible = False
            gv1.Columns("mcc_code1").Width = 100
            gv1.Columns("mcc_code1").HeaderText = "MCC Code"

            gv1.Columns("route_code1").IsVisible = False
            gv1.Columns("route_code1").Width = 100
            gv1.Columns("route_code1").HeaderText = "route_code1"



            gv1.Columns("mcc_name").IsVisible = False
            gv1.Columns("mcc_name").Width = 100
            gv1.Columns("mcc_name").HeaderText = "MCC Name"


            gv1.Columns("route_code").IsVisible = True
            gv1.Columns("route_code").Width = 100
            gv1.Columns("route_code").HeaderText = "Route Code"

            gv1.Columns("route_name").IsVisible = False
            gv1.Columns("route_name").Width = 100
            gv1.Columns("route_name").HeaderText = "Route Name"

            gv1.Columns("vlc_code").IsVisible = False
            gv1.Columns("vlc_code").Width = 80
            gv1.Columns("vlc_code").HeaderText = "VLC Code"

            gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True
            gv1.Columns("VLC_Code_VLC_Uploader").Width = 80
            gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Uploader Code"

            gv1.Columns("vlc_name").IsVisible = True
            gv1.Columns("vlc_name").Width = 80
            gv1.Columns("vlc_name").HeaderText = "VLC Name"

            gv1.Columns("day_target").IsVisible = True
            gv1.Columns("day_target").Width = 80
            gv1.Columns("day_target").HeaderText = " Daily Target"
            gv1.Columns("day_target").FormatString = "{0:F2}"



            gv1.Columns("shortfall").IsVisible = True
            gv1.Columns("shortfall").Width = 80
            gv1.Columns("shortfall").HeaderText = "Shortfall in Target with last day"
            gv1.Columns("shortfall").FormatString = "{0:F2}"

            gv1.Columns("Target_MTD").IsVisible = True
            gv1.Columns("Target_MTD").Width = 80
            gv1.Columns("Target_MTD").HeaderText = "Target MTD"
            gv1.Columns("Target_MTD").FormatString = "{0:F2}"

            gv1.Columns("ACH_MTD").IsVisible = True
            gv1.Columns("ACH_MTD").Width = 80
            gv1.Columns("ACH_MTD").HeaderText = "ACH. MTD"
            gv1.Columns("ACH_MTD").FormatString = "{0:F2}"

            gv1.Columns("ShortFall_MTD").IsVisible = True
            gv1.Columns("ShortFall_MTD").Width = 80
            gv1.Columns("ShortFall_MTD").HeaderText = "ShortFall MTD"
            gv1.Columns("ShortFall_MTD").FormatString = "{0:F2}"

            gv1.Columns("per_ShortFall").IsVisible = True
            gv1.Columns("per_ShortFall").Width = 80
            gv1.Columns("per_ShortFall").HeaderText = "% ShortFall"
            gv1.Columns("per_ShortFall").FormatString = "{0:F2}"
            gv1.Columns("per_ShortFall").TextAlignment = ContentAlignment.MiddleRight



            Dim summaryRowItem As New GridViewSummaryRowItem()


            Dim intCount As Integer = 0
            If dtExtraColumn IsNot Nothing AndAlso dtExtraColumn.Rows.Count > 0 Then
                For Each dr As DataRow In dtExtraColumn.Rows
                    Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(dr(0)), "{0:F2}", GridAggregateFunction.Sum)

                    summaryRowItem.Add(item1)
                Next
            End If

            'Dim item1 As New GridViewSummaryItem("ACC_WEIGHT", "{0:F2}", GridAggregateFunction.Sum)
            'summaryRowItem.Add(item1)

            'Dim summaryItem2 As New GridViewSummaryItem()

            'summaryItem2.Name = "per_ShortFall"
            'summaryItem2.AggregateExpression = "sum(Target_MTD))"
            'summaryItem2.FormatString = "{0:F2}"
            'summaryRowItem.Add(summaryItem2)

            'If summaryRowItem.Add(summaryItem2) <> 0 Then

            'End If
            Dim summaryItem1 As New GridViewSummaryItem()

            summaryItem1.Name = "per_ShortFall"
            summaryItem1.AggregateExpression = "IIf(sum(Target_MTD)>0,(sum(ShortFall_MTD)/sum(Target_MTD))*100, 0)"
            summaryItem1.FormatString = "{0:F2}"
            ' summaryItem1.FormatString = String.Format(" {0:R #.##,##}")


            summaryRowItem.Add(summaryItem1)

            Dim item2 As New GridViewSummaryItem("day_target", "{0:F2}", GridAggregateFunction.Sum)

            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("shortfall", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Target_MTD", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("ACH_MTD", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("ShortFall_MTD", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("SNO", "Total ", GridAggregateFunction.Var)
            summaryRowItem.Add(item7)




            gv1.GroupDescriptors.Add(New GridGroupByExpression("mcc_code as Item format ""{0}: {1}"" Group By mcc_code"))
            gv1.GroupDescriptors.Add(New GridGroupByExpression("route_code as Item format ""{0}: {1}"" Group By route_code"))

            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True
            gv1.Size = New System.Drawing.Size(456, 311)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            'gv1.MasterTemplate.summaryRowItem[0].PinPosition = PinnedRowPosition.Top



            'gv1.MasterTemplate.Caption = "Total"
            
            gv1.MasterTemplate.ShowTotals = True
        End If
        If chkSummaryRouteWise.Checked Then


            gv1.Columns("SNO").IsVisible = True
            gv1.Columns("SNO").Width = 40
            gv1.Columns("SNO").HeaderText = "SNO"
            'gv1.Columns("SNO").FormatString = "{0:0}"

            gv1.Columns("State_Code").IsVisible = False
            gv1.Columns("State_Code").Width = 100
            gv1.Columns("State_Code").HeaderText = "State Code"

            gv1.Columns("STATE_NAME").IsVisible = True
            gv1.Columns("STATE_NAME").Width = 100
            gv1.Columns("STATE_NAME").HeaderText = "State Name"

            gv1.Columns("MCC_CODE").IsVisible = True
            gv1.Columns("MCC_CODE").Width = 100
            gv1.Columns("MCC_CODE").HeaderText = "MCC Code"

            gv1.Columns("mcc_name").IsVisible = True
            gv1.Columns("mcc_name").Width = 100
            gv1.Columns("mcc_name").HeaderText = "MCC Name"

            gv1.Columns("RM_Code").IsVisible = True
            gv1.Columns("RM_Code").Width = 100
            gv1.Columns("RM_Code").HeaderText = "RM Code"



            gv1.Columns("RM_Name").IsVisible = True
            gv1.Columns("RM_Name").Width = 100
            gv1.Columns("RM_Name").HeaderText = "RM Name"


            gv1.Columns("ROUTE_CODE").IsVisible = False
            gv1.Columns("ROUTE_CODE").Width = 100
            gv1.Columns("ROUTE_CODE").HeaderText = "Route Code"

            gv1.Columns("Route_name").IsVisible = False
            gv1.Columns("Route_name").Width = 100
            gv1.Columns("Route_name").HeaderText = "Route Name"

            gv1.Columns("Supervisor_Code").IsVisible = True
            gv1.Columns("Supervisor_Code").Width = 80
            gv1.Columns("Supervisor_Code").HeaderText = "FS Code"

            gv1.Columns("Emp_Name").IsVisible = True
            gv1.Columns("Emp_Name").Width = 80
            gv1.Columns("Emp_Name").HeaderText = "FS Name"



            gv1.Columns("day_target").IsVisible = True
            gv1.Columns("day_target").Width = 80
            gv1.Columns("day_target").HeaderText = " Daily Target"
            gv1.Columns("day_target").FormatString = "{0:F2}"


            gv1.Columns("shortfall").IsVisible = True
            gv1.Columns("shortfall").Width = 80
            gv1.Columns("shortfall").HeaderText = "ACH Today"

            gv1.Columns("Target_MTD").IsVisible = True
            gv1.Columns("Target_MTD").Width = 80
            gv1.Columns("Target_MTD").HeaderText = "Target MTD"

            gv1.Columns("ACH_MTD").IsVisible = True
            gv1.Columns("ACH_MTD").Width = 80
            gv1.Columns("ACH_MTD").HeaderText = "ACH-MTD"

            gv1.Columns("ShortFall_MTD").IsVisible = True
            gv1.Columns("ShortFall_MTD").Width = 80
            gv1.Columns("ShortFall_MTD").HeaderText = "ShortFall MTD"

            gv1.Columns("per_ShortFall").IsVisible = True
            gv1.Columns("per_ShortFall").Width = 80
            gv1.Columns("per_ShortFall").HeaderText = "% ShortFall"
            gv1.Columns("per_ShortFall").FormatString = "{0:F2}"


            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim summaryItem1 As New GridViewSummaryItem()

            summaryItem1.Name = "per_ShortFall"
            summaryItem1.AggregateExpression = "IIf(sum(Target_MTD)>0,(sum(ShortFall_MTD)/sum(Target_MTD))*100, 0)"
            summaryItem1.FormatString = "{0:F2}"
            summaryRowItem.Add(summaryItem1)

            Dim item2 As New GridViewSummaryItem("day_target", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("shortfall", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Target_MTD", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("ACH_MTD", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("ShortFall_MTD", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("SNO", "Total ", GridAggregateFunction.Var)
            summaryRowItem.Add(item7)




            gv1.GroupDescriptors.Add(New GridGroupByExpression("mcc_code as Item format ""{0}: {1}"" Group By mcc_code"))
            'gv1.GroupDescriptors.Add(New GridGroupByExpression("route_code as Item format ""{0}: {1}"" Group By route_code"))

            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            'gv1.MasterTemplate.Caption = "Total"

            gv1.MasterTemplate.ShowTotals = True
        End If
        If chkMCCWiseSummary.Checked Then


            gv1.Columns("SNO").IsVisible = True
            gv1.Columns("SNO").Width = 40
            gv1.Columns("SNO").HeaderText = "SNO"
            'gv1.Columns("SNO").FormatString = "{0:0}"

            gv1.Columns("State_Code").IsVisible = False
            gv1.Columns("State_Code").Width = 100
            gv1.Columns("State_Code").HeaderText = "State Code"

            gv1.Columns("STATE_NAME").IsVisible = True
            gv1.Columns("STATE_NAME").Width = 100
            gv1.Columns("STATE_NAME").HeaderText = "State Name"

            gv1.Columns("MCC_CODE").IsVisible = True
            gv1.Columns("MCC_CODE").Width = 100
            gv1.Columns("MCC_CODE").HeaderText = "MCC Code"

            gv1.Columns("mcc_name").IsVisible = True
            gv1.Columns("mcc_name").Width = 100
            gv1.Columns("mcc_name").HeaderText = "MCC Name"

            gv1.Columns("RM_Code").IsVisible = True
            gv1.Columns("RM_Code").Width = 100
            gv1.Columns("RM_Code").HeaderText = "RM Code"



            gv1.Columns("RM_Name").IsVisible = True
            gv1.Columns("RM_Name").Width = 100
            gv1.Columns("RM_Name").HeaderText = "RM Name"



            gv1.Columns("Supervisor_Code").IsVisible = False
            gv1.Columns("Supervisor_Code").Width = 80
            gv1.Columns("Supervisor_Code").HeaderText = "FS Code"

            gv1.Columns("Emp_Name").IsVisible = False
            gv1.Columns("Emp_Name").Width = 80
            gv1.Columns("Emp_Name").HeaderText = "FS Name"



            gv1.Columns("day_target").IsVisible = True
            gv1.Columns("day_target").Width = 80
            gv1.Columns("day_target").HeaderText = " Daily/Day"
            gv1.Columns("day_target").FormatString = "{0:F2}"



            gv1.Columns("shortfall").IsVisible = True
            gv1.Columns("shortfall").Width = 80
            gv1.Columns("shortfall").HeaderText = "ACH Today"

            gv1.Columns("Target_MTD").IsVisible = True
            gv1.Columns("Target_MTD").Width = 80
            gv1.Columns("Target_MTD").HeaderText = "Target MTD"

            gv1.Columns("ACH_MTD").IsVisible = True
            gv1.Columns("ACH_MTD").Width = 80
            gv1.Columns("ACH_MTD").HeaderText = "ACH-MTD"

            gv1.Columns("ShortFall_MTD").IsVisible = True
            gv1.Columns("ShortFall_MTD").Width = 80
            gv1.Columns("ShortFall_MTD").HeaderText = "ShortFall MTD"

            gv1.Columns("per_ShortFall").IsVisible = True
            gv1.Columns("per_ShortFall").Width = 80
            gv1.Columns("per_ShortFall").HeaderText = "% ShortFall"
            gv1.Columns("per_ShortFall").FormatString = "{0:F2}"




            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F2}"
            summaryItem1.Name = "per_ShortFall"
            summaryItem1.AggregateExpression = "IIf(sum(Target_MTD)>0,(sum(ShortFall_MTD)/sum(Target_MTD))*100, 0)"
            summaryRowItem.Add(summaryItem1)

            Dim item2 As New GridViewSummaryItem("day_target", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("shortfall", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("Target_MTD", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("ACH_MTD", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("ShortFall_MTD", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
            Dim item7 As New GridViewSummaryItem("SNO", "Total ", GridAggregateFunction.Var)
            summaryRowItem.Add(item7)




            gv1.GroupDescriptors.Add(New GridGroupByExpression("RM_Code as Item format ""{0}: {1}"" Group By RM_Code"))
            'gv1.GroupDescriptors.Add(New GridGroupByExpression("route_code as Item format ""{0}: {1}"" Group By route_code"))

            gv1.ShowGroupPanel = False
            gv1.MasterTemplate.AutoExpandGroups = True

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            'gv1.MasterTemplate.Caption = "Total"

            gv1.MasterTemplate.ShowTotals = True
        End If




    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        Load_Report()
        tmpValLoad = False
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMCCVLCTragetMonthWiseReport & "'"))



            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmSavelayout_Click(sender As Object, e As EventArgs) Handles rmSavelayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

  

    Private Sub gv1_GroupSummaryEvaluate(sender As Object, e As GroupSummaryEvaluationEventArgs) Handles gv1.GroupSummaryEvaluate
        Try
            'If e.SummaryItem.Name = "per_ShortFall" Then
            '    Dim dataview As RadCollectionView(Of GridViewRowInfo) = Nothing
            '    Dim groupRow As GridViewGroupRowInfo = TryCast(e.Parent, GridViewGroupRowInfo)
            '    If groupRow IsNot Nothing Then
            '        dataview = groupRow.ViewTemplate.DataView
            '    Else
            '        Dim template As GridViewTemplate = TryCast(e.Parent, GridViewTemplate)
            '        If template IsNot Nothing Then
            '            dataview = template.DataView
            '        End If
            '    End If

            '    If dataview IsNot Nothing Then
            '        Dim projectedBalSum As Decimal = clsCommon.myCdbl(dataview.Evaluate("Sum(Target_MTD)", 0, dataview.Count))
            '        e.Value = IIf(projectedBalSum > 0, clsCommon.myCdbl(e.Value / projectedBalSum), 0)
            '    End If
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
