Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports System.Text
'==============Created by Preeti Gupta update by Preeti gupta against ticket no [BM00000008412]===================
Public Class RptVLVCTragetMasterReport
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptVLCTragetMasterReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
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
    '===================================================================

    Sub LoadVSP()
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER  where Form_Type ='VSP' "
        cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVSP.ValueMember = "Code"
        cbgVSP.DisplayMember = "Name"

    End Sub
    Sub LoadMP()
        Dim qry As String = "select MP_Code as [Code] ,MP_Name as [Name]  from tspl_Mp_Master   "
        cbgMP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMP.ValueMember = "Code"
        cbgMP.DisplayMember = "Name"

    End Sub
    Public Sub Load_Report()
        Try
            Dim sQuery As String
            Dim dt As New DataTable
            Dim arr As List(Of String) = Nothing
            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            If chkVSPSelect.IsChecked AndAlso cbgVSP.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single VSP or select all.")
            End If

            If cbtMCCRouteVLCC.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single MCC or select all.")

            End If

          


            '=============================================pivot variables================================================
            Dim strMonthlyQry As String = ""
            Dim MonthlyCollection As String = String.Empty
            Dim MonthlyCollectionTarget As String = String.Empty
            Dim outerQry As String = String.Empty
            Dim MonthlyCollectionTargetTotal As String = String.Empty
            Dim outerQryTotal As String = String.Empty
            If rbMonthly.IsChecked Then
                strMonthlyQry = "declare @start DATE = '" & clsCommon.GetPrintDate(txtFromDate.Value, "MM/dd/yyyy") & "'" + Environment.NewLine
                strMonthlyQry += "  declare @end DATE ='" & clsCommon.GetPrintDate(txtToDate.Value, "MM/dd/yyyy") & "'" + Environment.NewLine
                strMonthlyQry += "   ;with months (date)"
                strMonthlyQry += "  AS(SELECT @start UNION ALL SELECT DATEADD(month,1,date) from months where DATEADD(month,1,date)<=@end)" + Environment.NewLine
                strMonthlyQry += "   select substring(fin.months,2,len(fin.months)-1) as month from (select (select ',['+Datename(month,date)+' '+DATENAME(YEAR,date)+' ]' from months for xml path(''))months)fin "
                strMonthlyQry += "            union all"
                strMonthlyQry += "	select substring(fin.months,2,len(fin.months)-1) as month from (select (select ',['+Datename(month,date)+' '+DATENAME(YEAR,date)+' Target]' from months for xml path(''))months)fin "
                strMonthlyQry += "            union all"
                strMonthlyQry += "	select (select ',sum(isnull(['+Datename(month,date)+' '+DATENAME(YEAR,date)+' Target],0)) as ['+Datename(month,date)+' '+DATENAME(YEAR,date)+' Target]'"
                strMonthlyQry += "+','+"
                strMonthlyQry += "  'sum(isnull(['+Datename(month,date)+' '+DATENAME(YEAR,date)+' ],0)) as ['+Datename(month,date)+' '+DATENAME(YEAR,date)+' ]' from months for xml path(''))months "


                dt = clsDBFuncationality.GetDataTable(strMonthlyQry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso dt.Rows.Count = 3 Then
                    MonthlyCollection = clsCommon.myCstr(dt.Rows(0)("month"))
                    MonthlyCollectionTarget = clsCommon.myCstr(dt.Rows(1)("month"))
                    outerQry = clsCommon.myCstr(dt.Rows(2)("month"))
                End If


            ElseIf chkWeekely.IsChecked Then

                strMonthlyQry = "declare @weekofmonth1 table(monthname1 varchar(max))"
                strMonthlyQry += " declare @startdate datetime,@enddate datetime,@weekofmonth varchar(max)"
                strMonthlyQry += " set @startdate='" & clsCommon.GetPrintDate(txtFromDate.Value, "MM/dd/yyyy") & "' "
                strMonthlyQry += " set @enddate='" & clsCommon.GetPrintDate(txtToDate.Value, "MM/dd/yyyy") & "'"
                strMonthlyQry += " if @startdate=@enddate "
                strMonthlyQry += " while (@startdate<=@enddate)"
                strMonthlyQry += "begin"
                strMonthlyQry += " select @weekofmonth=cast(DATEPART(WEEK, @startdate)  - DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,@startdate), 0))+ 1 as varchar)"
                strMonthlyQry += " set @startdate=DATEADD(DD,1,@startdate) "
                strMonthlyQry += "	insert into @weekofmonth1 (monthname1) values (cast(@weekofmonth as varchar)+' Week '+left(datename(MM,@startdate),3)+' '+right(datename(Year,@startdate),2))     End"
                strMonthlyQry += "     Else"
                strMonthlyQry += " while (@startdate<@enddate)"

                strMonthlyQry += " begin"
                strMonthlyQry += " select @weekofmonth=cast(DATEPART(WEEK, @startdate)  -"
                strMonthlyQry += " DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,@startdate), 0))+ 1 as varchar)"
                strMonthlyQry += " set @startdate=DATEADD(DD,1,@startdate)"
                strMonthlyQry += " insert into @weekofmonth1 (monthname1) values (cast(@weekofmonth as varchar)+' Week '+left(datename(MM,@startdate),3)+' '+right(datename(Year,@startdate),2))"
                strMonthlyQry += "     End"
                strMonthlyQry += " select substring(fin.months,2,len(fin.months)-1) as month from (select (select distinct ',['+monthname1+']' from @weekofmonth1 for xml path(''))months)fin"
                strMonthlyQry += "    union all"
                strMonthlyQry += " select substring(fin.months,2,len(fin.months)-1) as month from (select (select distinct ',['+monthname1+' Target]' from @weekofmonth1 for xml path(''))months)fin"
                strMonthlyQry += " union all"
                strMonthlyQry += " select (select distinct ',sum(isnull(['+monthname1+' Target],0)) as ['+monthname1+' Target]'+',' + 'sum(isnull(['+monthname1+'],0))as ['+monthname1+' ]'   from @weekofmonth1 for xml path(''))months"
                strMonthlyQry += "  union all"
                strMonthlyQry += " select substring(fin.months,2,len(fin.months)-1) as month from ( select (select distinct '+sum(isnull(['+monthname1+' Target],0))' from @weekofmonth1 for xml path(''))months)fin"
                strMonthlyQry += " union all"
                strMonthlyQry += "  select substring(fin.months,2,len(fin.months)-1) as month from ( select (select distinct   '+sum(isnull(['+monthname1+'],0))'   from @weekofmonth1 for xml path(''))months)fin"

                MonthlyCollection = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strMonthlyQry))
                dt = clsDBFuncationality.GetDataTable(strMonthlyQry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso dt.Rows.Count = 5 Then
                    MonthlyCollection = clsCommon.myCstr(dt.Rows(0)("month"))
                    MonthlyCollectionTarget = clsCommon.myCstr(dt.Rows(1)("month"))
                    outerQry = clsCommon.myCstr(dt.Rows(2)("month"))
                    MonthlyCollectionTargetTotal = "," + clsCommon.myCstr(dt.Rows(3)("month"))
                    outerQryTotal = "," + clsCommon.myCstr(dt.Rows(4)("month"))
                End If


            End If
            '================================================================end here=============================================================================

            sQuery = "select DATEPART(WEEK, tspl_milk_receipt_head.doc_date)-DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,tspl_milk_receipt_head.doc_date), 0))+ 1 as Week, convert(varchar,dbo.getWeekNo(tspl_milk_receipt_head.doc_date ),103)+' Week '+left(DATENAME(month,(tspl_milk_receipt_head.doc_date) ),3)+ ' '+right(DATENAME(year,tspl_milk_receipt_head.doc_date ),2) as WeekCollection  , convert(varchar,dbo.getWeekNo(tspl_milk_receipt_head.doc_date ),103)+' Week '+left(DATENAME(month,(tspl_milk_receipt_head.doc_date) ),3)+ ' '+right(DATENAME(year,tspl_milk_receipt_head.doc_date ),2)+' Target' as WeekCollectionTarget, Datename(month,tspl_milk_receipt_head.doc_date)+' '+DATENAME(YEAR,tspl_milk_receipt_head.doc_date) as Month_Date,Datename(month,tspl_milk_receipt_head.doc_date)+' '+DATENAME(YEAR,tspl_milk_receipt_head.doc_date)+' Target' as Month_Target,convert(varchar,tspl_milk_receipt_head.doc_date,103) as doc_date,tspl_milk_receipt_head.doc_date as Date,TSPL_Vlc_Target_Detail.frm_date,TSPL_Vlc_Target_Detail.To_Date,tspl_milk_receipt_head.mcc_code,"
            sQuery += " tspl_mcc_master.mcc_name,tspl_milk_receipt_detail.route_code,tspl_mcc_route_master.Route_name,tspl_milk_receipt_detail.Vlc_code,tspl_VLC_master_Head.VLC_Name,tspl_milk_receipt_detail.vsp_code,"
            sQuery += " tspl_vendor_master.vendor_name,TSPL_Vlc_Target_Detail.MP_Code,tspl_mp_master.mp_name  ,tspl_milk_receipt_detail.SHIFT ,SUm(tspl_milk_receipt_detail.milk_weight) as ACC_WEIGHT ,"
            sQuery += " TSPL_Vlc_Target_Detail.Day_Target,"
            sQuery += "  TSPL_Vlc_Target_Detail.Morning_Target, "
            sQuery += " TSPL_Vlc_Target_Detail.Evening_Target"


            If chkTraget.Checked Then
                sQuery += ",case when tspl_milk_receipt_detail.shift='M' then TSPL_Vlc_Target_Detail.Morning_Target else TSPL_Vlc_Target_Detail.Evening_Target end as Target "
            Else
                sQuery += " ,0 as Target"
            End If

            If chkTraget.Checked Then
                sQuery += ",isnull(SUm(tspl_milk_receipt_detail.milk_weight),0)-isnull(case when tspl_milk_receipt_detail.shift='M' then TSPL_Vlc_Target_Detail.Morning_Target else TSPL_Vlc_Target_Detail.Evening_Target end,0) as Difference "
            Else
                sQuery += ",isnull(SUm(tspl_milk_receipt_detail.milk_weight),0) as Difference "
            End If


            sQuery += " from tspl_milk_receipt_detail "
            sQuery += " inner join tspl_milk_receipt_head on tspl_milk_receipt_head.DOC_CODE =tspl_milk_receipt_detail.DOC_CODE"
            'and tspl_milk_receipt_head.mcc_code='MCUP00005' and tspl_milk_receipt_detail.route_COde='MCUP00005/MR015' 
            'If rbtnMCCRouteVLCCSelect.IsChecked Then
            ' Dim arr As List(Of String) = Nothing
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
                    Throw New Exception("Please select at least one Route")
                End If
            End If
            'End If


            If chkVSPSelect.IsChecked And cbgVSP.CheckedValue.Count > 0 Then
                sQuery += " and tspl_milk_receipt_detail.vsp_code   in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
            End If



            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                sQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                sQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.SHIFT='E' then 3 else 2 end  )"
            End If

            sQuery += " and  convert(date,tspl_milk_receipt_head.doc_date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,tspl_milk_receipt_head.doc_date,103)<=convert(date,'" + txtToDate.Value + "',103)"
            sQuery += " Inner join tspl_mcc_master on tspl_mcc_master.mcc_code=tspl_milk_receipt_head.mcc_code"
            sQuery += " inner join tspl_mcc_route_master on tspl_mcc_route_master.route_code=tspl_milk_receipt_detail.route_code"
            sQuery += " inner join tspl_vendor_master on tspl_vendor_master.vendor_code=tspl_milk_receipt_detail.vsp_code"
            'sQuery += "  left join (select max(document_Code) as dd_cc,VLC_Code as v_c from TSPL_Vlc_Target_Detail group by VLC_Code) v_t_d on v_t_d.v_c=tspl_milk_receipt_detail.vlc_code "
            sQuery += " left outer join TSPL_Vlc_Target_Detail on TSPL_Vlc_Target_Detail.mcc_code=tspl_milk_receipt_head.mcc_code "

            sQuery += " and TSPL_Vlc_Target_Detail.route_code=tspl_milk_receipt_detail.route_code and TSPL_Vlc_Target_Detail.vlc_code=tspl_milk_receipt_detail.vlc_code        and TSPL_Vlc_Target_Detail.Document_Code=(select max(document_Code) as dd_cc from TSPL_Vlc_Target_Detail where convert(date,frm_date,103)<=convert(date,tspl_milk_receipt_head.doc_date,103) and convert(date,To_Date,103)>=convert(date,tspl_milk_receipt_head.doc_date,103) aND TSPL_Vlc_Target_Detail.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE group by VLC_Code)  and convert(date,tspl_milk_receipt_Head.doc_date,103) between convert(date,frm_date,103) and convert(date,to_date,103) "
            If chkMP.Checked Then
                sQuery += " and  TSPL_Vlc_Target_Detail.MP_Code  is not null "
            Else
                sQuery += " and  TSPL_Vlc_Target_Detail.MP_Code  is  null "
            End If
            sQuery += " left join tspl_MP_master on tspl_MP_master.mp_code=TSPL_Vlc_Target_Detail.MP_Code"
            sQuery += " left outer join tspl_VLC_master_Head on tspl_VLC_master_Head.VLC_Code=tspl_milk_receipt_detail.VLC_Code  where 2=2"


            If chkMP.Checked Then
                If chkSelectMP.IsChecked And cbgMP.CheckedValue.Count > 0 Then
                    sQuery += " and tspl_MP_master.Mp_Code    in (" + clsCommon.GetMulcallString(cbgMP.CheckedValue) + ")"
                End If
            End If
            sQuery += " group by tspl_VLC_master_Head.VLC_Name,TSPL_Vlc_Target_Detail.Day_Target,TSPL_Vlc_Target_Detail.Morning_Target, TSPL_Vlc_Target_Detail.Evening_Target,tspl_milk_receipt_detail.shift,TSPL_Vlc_Target_Detail.Morning_Target, TSPL_Vlc_Target_Detail.Evening_Target, tspl_milk_receipt_Head.doc_date,convert(varchar,tspl_milk_receipt_head.doc_date,103),tspl_milk_receipt_head.doc_date,TSPL_Vlc_Target_Detail.frm_date,TSPL_Vlc_Target_Detail.To_Date,tspl_milk_receipt_head.mcc_code,tspl_mcc_master.mcc_name,tspl_milk_receipt_detail.route_code,tspl_mcc_route_master.Route_name,tspl_milk_receipt_detail.Vlc_code, tspl_milk_receipt_detail.vsp_code, tspl_vendor_master.vendor_name ,tspl_milk_receipt_detail.SHIFT ,TSPL_Vlc_Target_Detail.MP_Code,tspl_mp_master.mp_name"
            'If rbMonthly.IsChecked = False AndAlso chkWeekely.IsChecked = False Then
            '    sQuery += "   order by Date"
            'End If

            Dim ss As String = sQuery


            '==========================



            If rbMonthly.IsChecked Then
                sQuery = "  select [MCC Code],[MCC Name],[Route Code],[Route Name],[VLC Code],[VLC Name],[VSP Code],[VSP Name],[MP Code],[MP Name] " & outerQry & " from"
                sQuery += " (select * from ( select  Month_Date,Month_Target ,MCC_CODE as [MCC Code] ,max(MCC_NAME) as [MCC Name],(ROUTE_CODE ) as [Route Code],max(Route_Name) as [Route Name],VLC_CODE as [VLC Code],max(VLC_Name) as [VLC Name],VSP_CODE as [VSP Code],max(Vendor_Name  ) as [VSP Name],mp_code as [MP Code],max(mp_Name) as [MP Name] ,sum(isnull(ACC_WEIGHT,0) ) as TCollection,sum(isnull(Target,0) ) as [Traget]  from ( "
                sQuery += " " & ss & ""
                sQuery += " )as Month group by Month_Date ,Month_Target,MCC_CODE ,ROUTE_CODE ,VLC_CODE ,VSP_CODE,mp_code) as final"
                sQuery += " pivot (sum(traget) FOR month_target IN (" & MonthlyCollectionTarget & ")) AS pvt1"
                sQuery += " pivot (sum(Tcollection) FOR month_date IN (" & MonthlyCollection & ")) AS pvt"
                sQuery += " )fin group by [MCC Code],[MCC Name],[Route Code],[Route Name],[VLC Code],[VLC Name],[VSP Code],[VSP Name],[MP Code],[MP Name]"
            ElseIf chkWeekely.IsChecked Then


                sQuery = "  select [MCC Code],[MCC Name],[Route Code],[Route Name],[VLC Code],[VLC Name],[VSP Code],[VSP Name],[MP Code],[MP Name] " & outerQry & " " & MonthlyCollectionTargetTotal & " as [Total Target] " & outerQryTotal & " as [Total Collection] from"
                sQuery += " (select * from ( select WeekCollection,WeekCollectionTarget, Month_Date,Month_Target ,MCC_CODE as [MCC Code] ,max(MCC_NAME) as [MCC Name],(ROUTE_CODE ) as [Route Code],max(Route_Name) as [Route Name],VLC_CODE as [VLC Code],max(VLC_Name) as [VLC Name],VSP_CODE as [VSP Code],max(Vendor_Name  ) as [VSP Name],mp_code as [MP Code],max(mp_Name) as [MP Name] ,sum(isnull(ACC_WEIGHT,0) ) as TCollection,sum(isnull(Target,0) ) as [Traget]  from ( "
                sQuery += " " & ss & ""
                sQuery += " )as Month group by WeekCollection,WeekCollectionTarget,Month_Date ,Month_Target,MCC_CODE ,ROUTE_CODE ,VLC_CODE ,VSP_CODE,mp_code) as final"
                sQuery += " pivot (sum(traget) FOR WeekCollectionTarget IN (" & MonthlyCollectionTarget & ")) AS pvt1"
                sQuery += " pivot (sum(Tcollection) FOR WeekCollection IN (" & MonthlyCollection & ")) AS pvt"
                sQuery += " )fin group by [MCC Code],[MCC Name],[Route Code],[Route Name],[VLC Code],[VLC Name],[VSP Code],[VSP Name],[MP Code],[MP Name]"
            End If


            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dtgv
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()

                If rbMonthly.IsChecked = False andalso chkWeekely .IsChecked = False Then
                    FormatGrid()
                    'ElseIf chkWeekely.IsChecked = False Then
                    '    FormatGrid()

                Else
                    If chkMP.Checked = False Then
                        gv1.Columns("MP Code").IsVisible = False
                        gv1.Columns("MP Name").IsVisible = False
                        gv1.BestFitColumns()
                    End If
                    gv1.BestFitColumns()
                End If



                If btnReferesh = False Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, clsERPFuncationality.CompanyAddresShowinHeader(), "MCCShiftReport(RouteWise)RateAndAmount", "Milk Shift Report (Route Wise)", "Address.rpt")
                    frmCRV = Nothing
                End If

                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableControl(False)
            Else
                tmpValLoad = False
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGrid()
        ' Dim strItemCode, head2 As String

        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False

        Next


        gv1.Columns("doc_date").IsVisible = True
        gv1.Columns("doc_date").Width = 100
        gv1.Columns("doc_date").HeaderText = "Doc Date"
        gv1.Columns("doc_date").FormatString = "{0:d}"


        gv1.Columns("frm_date").IsVisible = False
        gv1.Columns("frm_date").Width = 100
        gv1.Columns("frm_date").HeaderText = " From Date"
        gv1.Columns("frm_date").FormatString = "{0:d}"

        gv1.Columns("To_Date").IsVisible = False
        gv1.Columns("To_Date").Width = 100
        gv1.Columns("To_Date").HeaderText = "To Date"
        gv1.Columns("To_Date").FormatString = "{0:d}"

        'gv1.Columns("Sample_no").IsVisible = True
        'gv1.Columns("Sample_no").Width = 100
        'gv1.Columns("Sample_no").HeaderText = "Sample No"

        gv1.Columns("mcc_code").IsVisible = False
        gv1.Columns("mcc_code").Width = 100
        gv1.Columns("mcc_code").HeaderText = "MCC Code"

        gv1.Columns("mcc_name").IsVisible = True
        gv1.Columns("mcc_name").Width = 80
        gv1.Columns("mcc_name").HeaderText = "MCC Name"

        gv1.Columns("route_code").IsVisible = False
        gv1.Columns("route_code").Width = 80
        gv1.Columns("route_code").HeaderText = "Cans"

        gv1.Columns("Route_name").IsVisible = True
        gv1.Columns("Route_name").Width = 80
        gv1.Columns("Route_name").HeaderText = "Route Name"

        gv1.Columns("Vlc_code").IsVisible = False
        gv1.Columns("Vlc_code").Width = 50
        gv1.Columns("Vlc_code").HeaderText = "VLC Code"

        gv1.Columns("Vlc_Name").IsVisible = True
        gv1.Columns("Vlc_Name").Width = 100
        gv1.Columns("Vlc_Name").HeaderText = " VLC Name"

        gv1.Columns("vsp_code").IsVisible = False
        gv1.Columns("vsp_code").Width = 100
        gv1.Columns("vsp_code").HeaderText = "VSP Code"

        gv1.Columns("vendor_name").IsVisible = True
        gv1.Columns("vendor_name").Width = 100
        gv1.Columns("vendor_name").HeaderText = "VSP Name"

        gv1.Columns("SHIFT").IsVisible = True
        gv1.Columns("SHIFT").Width = 100
        gv1.Columns("SHIFT").HeaderText = "Shift"

        gv1.Columns("ACC_WEIGHT").IsVisible = True
        gv1.Columns("ACC_WEIGHT").Width = 100
        gv1.Columns("ACC_WEIGHT").HeaderText = "Total Collection"

        gv1.Columns("Target").IsVisible = True
        gv1.Columns("Target").Width = 100
        gv1.Columns("Target").HeaderText = "Target"

        gv1.Columns("Difference").IsVisible = True
        gv1.Columns("Difference").Width = 100
        gv1.Columns("Difference").HeaderText = "Difference"

        If chkMP.Checked Then
            gv1.Columns("Mp_Code").IsVisible = True
            gv1.Columns("Mp_Code").Width = 100
            gv1.Columns("Mp_Code").HeaderText = "MP Code"

            gv1.Columns("MP_Name").IsVisible = True
            gv1.Columns("MP_Name").Width = 100
            gv1.Columns("MP_Name").HeaderText = "MP Name"
        End If


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("ACC_WEIGHT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Target", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)




       
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        chkTraget.Checked = True
        rbMonthly.IsChecked = False
        chkWeekely.IsChecked = False
        chkMP.Checked = False
        RadGroupBox3.Visible = False
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        RadGroupBox5.Enabled = val
        RadGroupBox2.Enabled = val
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try

            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " " + clsCommon.myCstr(txtFromShift.SelectedValue) + "  " + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " " + clsCommon.myCstr(txtToShift.SelectedValue))
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
                If chkVSPSelect.IsChecked Then
                    Dim stVSPName As String = ""
                    For Each StrName As String In cbgVSP.CheckedDisplayMember
                        If clsCommon.myLen(stVSPName) > 0 Then
                            stVSPName += ", "
                        End If
                        stVSPName += StrName
                    Next
                    Dim strVSPCode As String = ""
                    For Each StrCode As String In cbgVSP.CheckedValue
                        If clsCommon.myLen(strVSPCode) > 0 Then
                            strVSPCode += ", "
                        End If
                        strVSPCode += StrCode
                    Next
                    arrHeader.Add(("VSP Name: " + stVSPName + " "))
                End If

                If exporter = EnumExportTo.Excel Then
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
                Else
                    clsCommon.MyExportToPDF("VLC Traget Master Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    Private Sub rbtnMCCRouteVLCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
    End Sub

    Private Sub chkVSPAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVSPAll.ToggleStateChanged
        cbgVSP.Enabled = Not chkVSPAll.IsChecked
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

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptVLVCTragetMasterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        'rbtnMCCRouteVLCCAll.IsChecked = True
        chkVSPAll.IsChecked = True
        chkAllMP.IsChecked = True
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Pres%s Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        LoadMCCRouteVLCTree()
        LoadVSP()
        LoadShiftFrom()
        LoadShiftTo()
        LoadMP()
        Reset()
    End Sub

    Private Sub RptVLVCTragetMasterReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            Load_Report()
        End If
    End Sub

    Private Sub chkMP_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMP.ToggleStateChanged
        If chkMP.Checked Then
            RadGroupBox3.Visible = True
        Else
            RadGroupBox3.Visible = False
        End If
    End Sub

    Private Sub chkAllMP_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllMP.ToggleStateChanged
        cbgMP.Enabled = Not chkAllMP.IsChecked
    End Sub
End Class
