Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class rptMilkAnalysis
    Inherits FrmMainTranScreen

    Dim MultipleFinderFillAuto As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    'Dim StrPermission As String
    'Dim dtREJECT As DataTable
    'Const colLineNo As String = "colLineNo"
    Public Const colRange As String = "colRange"
    'Public Const colRate As String = "colRate"
    Public Const colTo As String = "colTo"
    Private Sub rptMilkAnalysis_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Try
        '    clsDBFuncationality.ExecuteNonQuery(" IF ( NOT EXISTS (SELECT * 
        '            FROM INFORMATION_SCHEMA.TABLES 
        '            WHERE TABLE_SCHEMA = 'dbo' AND
        '            TABLE_NAME = 'TSPL_Milk_Quantity_Slab_FOR_DAYS'))
        '            BEGIN
        '                CREATE TABLE TSPL_Milk_Quantity_Slab_FOR_DAYS (id INT Identity, Min_Range decimal(18,2), Max_Range decimal(18,2)) 
        '             truncate table TSPL_Milk_Quantity_Slab_FOR_DAYS
        '            insert into TSPL_Milk_Quantity_Slab_FOR_DAYS (Min_Range,Max_Range) values (1,5)
        '            insert into TSPL_Milk_Quantity_Slab_FOR_DAYS (Min_Range,Max_Range) values (6,10)
        '            insert into TSPL_Milk_Quantity_Slab_FOR_DAYS (Min_Range,Max_Range) values (11,15)
        '            insert into TSPL_Milk_Quantity_Slab_FOR_DAYS (Min_Range,Max_Range) values (16,20)
        '            insert into TSPL_Milk_Quantity_Slab_FOR_DAYS (Min_Range,Max_Range) values (21,31)
        '            END
        '            ")
        'Catch ex As Exception
        'End Try



        'StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        MultipleFinderFillAuto = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, Nothing)) = 1)
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        Reset()
        RadGroupBox3.Visible = MultipleFinderFillAuto
        txtPaymentCycleFrom.Enabled = Not MultipleFinderFillAuto
        txtPaymentCycleTo.Enabled = Not MultipleFinderFillAuto
        'GroupBox1.Visible = Not MultipleFinderFillAuto
        btnPrint.Visible = MultipleFinderFillAuto
        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)()
        'coll.Add("Line_No", "integer not null default 0")
        'coll.Add("Min_Range", "decimal(18,1) null")
        'coll.Add("Max_Range", "decimal(18,1) null")
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_Milk_Quantity_Slab", coll)
        GroupBox76.Visible = True
        loadBlankGridRange()
        LoadRange()
        If gvTS.Rows.Count = 0 Then
            gvTS.Rows.AddNew()
        End If
        GroupBox76.Visible = False
        btn_RangeClose.Visible = False
    End Sub
    Sub Reset()
        'fromDate.Value = clsCommon.GETSERVERDATE()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        'dtREJECT = Nothing
        'txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        'txtPaymentCycleFrom.Value = ""
        'txtPaymentCycleTo.Value = ""
        'btnGo.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        'txtMCC.arrValueMember = Nothing
        EnableDisableCtrl(True)
        RadPageView1.SelectedPage = RadPageViewPage1
        If rbtn_Detail.Checked = True Then
            GroupBox76.Visible = False
        End If
        If rbtn_Summary.Checked = True Then
            GroupBox76.Visible = True
        End If
    End Sub
    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFiscalYear.Enabled = val
        txtPaymentCycleFrom.Enabled = val
        txtPaymentCycleTo.Enabled = val
        txtMCC.Enabled = val
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try

            Dim dt1 As New DataTable
            Dim qry As String = Nothing
            'Dim FinalQuery As String = Nothing
            'Dim strRejection As String = Nothing
            'Dim strSRNQuery As String = Nothing
            'Dim strRejectionQuery As String = Nothing
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            If Not MultipleFinderFillAuto Then
                If clsCommon.myLen(txtPaymentCycleFrom.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Plz Select Payment Cycle From First.", Me.Text)
                    txtPaymentCycleFrom.Focus()
                    Exit Sub
                End If
                If clsCommon.myLen(txtPaymentCycleTo.Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Plz Select Payment Cycle To First.", Me.Text)
                    txtPaymentCycleTo.Focus()
                    Exit Sub
                End If

                If clsCommon.myCdbl(txtPaymentCycleFrom.Value) > clsCommon.myCdbl(txtPaymentCycleTo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "From Payment Cycle can not be greater then to Payment Cycle", Me.Text)
                    txtPaymentCycleFrom.Focus()
                    Exit Sub
                End If

                Patment_Cycle_changed()
            End If
            'strRejection = ",'' as RejectType,'' as RejectReason,'' as Defaulter"
            'Dim ShowVLCUploaderData As Boolean = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVLCUploaderData, clsFixedParameterCode.ShowVLCUploaderData, Nothing)) = 1
            'strSRNQuery = clsMilkRejectHead.GetMCCRegisterWithRejectionColumnQuery(fromDate.Value, ToDate.Value, "M", "E", "", StrPermission, txtMCC.arrValueMember, Nothing, Nothing, "", strRejection, ShowVLCUploaderData)

            'strRejectionQuery = clsMilkRejectHead.GetMCCRegisterRejectionQuery(fromDate.Value, ToDate.Value, "M", "E", StrPermission, txtMCC.arrValueMember, Nothing, Nothing, "")

            'qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
            '    " final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
            '    " final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)]," &
            '    " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
            '    " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
            '    " final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
            '    " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,RejectType,RejectReason,Defaulter, " &
            '    " final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle  From ( " & strSRNQuery & " Union All " & strRejectionQuery & ") As final where 2=2 "

            'Dim qry1 As String = "select TSPL_MILK_REJECT_TYPE.code as Reject_Type,TSPL_MILK_REJECT_TYPE.Description as Description,TSPL_MILK_REJECT_TYPE.SNo as SNo from TSPL_MILK_REJECT_TYPE where 1=1 order by SNo"
            'dtREJECT = clsDBFuncationality.GetDataTable(qry1)
            'Dim strRejectQty As String = ""
            'Dim strRejectPer1 As String = ""
            'Dim strRejectsum As String = ""
            'Dim strRejectPer2 As String = ",'' as [FAT - Per],'' as [SNF - Per]"
            'If dtREJECT IsNot Nothing AndAlso dtREJECT.Rows.Count > 0 Then
            '    For Each dr As DataRow In dtREJECT.Rows
            '        strRejectQty += ",case When isnull(RejectType,'')='" + clsCommon.myCstr(dr("Description")) + "' then [Milk Weight] else 0 end as [" + clsCommon.myCstr(dr("Description")) + "]"
            '        strRejectPer1 += ",case When sum([Milk Weight])>0 then cast((sum([" + clsCommon.myCstr(dr("Description")) + "])/sum([Milk Weight]))*100 as decimal(18,2)) else 0 end as [" + clsCommon.myCstr(dr("Description")) + " %]"
            '        strRejectsum += ",sum([" + clsCommon.myCstr(dr("Description")) + "]) as [" + clsCommon.myCstr(dr("Description")) + "]"
            '        strRejectPer2 += ",'' as [" + clsCommon.myCstr(dr("Description")) + " - Per]"
            '    Next
            'End If

            'FinalQuery = "select [MCC Code],[MCC Name],[Route Code],[Route Name]
            ' ,ROW_NUMBER() OVER(Partition by [MCC Name],[Route Name] ORDER BY [MCC Name],[Route Name]) AS SNo
            ',[VSP Code] as [SOCIETY CODE],[VSP Name] as [SOCIETY NAME],[Milk Type]
            ',sum([Milk Weight]) as [Milk Weight],sum([FAT]) as [FAT],sum([SNF]) as [SNF]" + strRejectsum + " ,case when sum([Milk Weight] )=0 then 0 else (sum([FAT] )/sum([Milk Weight] ))*100 end as [FAT(%)]
            ',case when sum([Milk Weight] )=0 then 0 else (sum([SNF] )/sum([Milk Weight] ))*100 end as [SNF(%)] " + strRejectPer1 + " " + strRejectPer2 + " from(select [MCC Code],[MCC Name],[Route Code],[Route Name]
            ',[VSP Code],[VSP Name],[Milk Type]
            ',[Milk Weight],[FAT], [SNF]" + strRejectQty + "
            'from (select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name]
            ',pp.[Route Code],max(pp.[Route Name]) as [Route Name]
            ',pp.[VSP Code],max(pp.[VSP Name]) as [VSP Name]
            ',pp.[Milk Type]
            ',sum([Milk Weight(KG)] ) as [Milk Weight]
            ',sum([FAT(KG)] ) as [FAT] ,sum([SNF(KG)] ) as [SNF]
            ',RejectType
            'from (" + Environment.NewLine + qry + Environment.NewLine + " ) as  pp group by pp.[MCC Code],pp.[Route Code],pp.[VSP Code],pp.[Milk Type],pp.RejectType 
            '  ) as aa )a where 1=1 group by [MCC Code],[MCC Name],[Route Code],[Route Name]
            ',[VSP Code],[VSP Name],[Milk Type] order by [MCC Name],[Route Name],[VSP Code]"

            If rbtn_Detail.Checked = True Then
                Dim strRoutJoin As String = ""
                Dim strRouteName As String = ""
                Dim strQty As String = ""
                Dim strMaxRoute As String = ""
                Dim strMCCCodeName As String = ""
                Dim strMCCCodeNameMax As String = ""
                Dim strGroupByWithOrderby As String = ""
                Dim strFromDate As String = ""
                Dim strToDate As String = ""

                If MultipleFinderFillAuto Then
                    strRoutJoin = " Left Outer Join TSPL_BULK_ROUTE_MASTER On TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_SRN_HEAD.ROUTE_CODE "
                    strRouteName = " , TSPL_MILK_SRN_HEAD.ROUTE_CODE ,TSPL_BULK_ROUTE_MASTER.Route_Name as [ROUTE Name] "
                    strQty = " , SUM(Quantity) as [Qty MILK] "
                    strMaxRoute = " ,MAX(ROUTE_CODE) as [ROUTE CODE],MAX(final.[ROUTE Name]) AS [ROUTE NAME] , "
                    'strMCCCodeName = " ,TSPL_MILK_SRN_HEAD.MCC_CODE ,TSPL_MCC_MASTER.MCC_NAME "
                    'strMCCCodeNameMax = " (MCC_CODE) as [MCC CODE], max(MCC_NAME) as [MCC NAME] , "
                    strGroupByWithOrderby = " GROUP BY ROUTE_CODE ,[SOCIETY CODE] order by ROUTE_CODE,[SOCIETY CODE] " '" GROUP BY MCC_CODE  ,[SOCIETY CODE] order by MCC_CODE,[SOCIETY CODE] "
                    strFromDate = clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy"))
                    strToDate = clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy"))
                    strFromDate = "'" + strFromDate + "'" + " as FromDate " + ",'" + strToDate + "'" + " as ToDate ,"

                Else
                    strRoutJoin = " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE "
                    strRouteName = " ,TSPL_MCC_ROUTE_MASTER.Route_Name as [ROUTE] "
                    strMaxRoute = " , MAX(final.[ROUTE]) AS [ROUTE] , "
                    strGroupByWithOrderby = " GROUP BY [SOCIETY CODE] "
                End If

                qry = "select 
                    " + strFromDate + " final.[SOCIETY CODE],MAX(final.[SOCIETY NAME]) AS [SOCIETY NAME] " + strMaxRoute + " MAX(final.[VLC Created Date]) AS [VLC Created Date]
                    ,CAST(SUM(Quantity)/COUNT(shift) as decimal(18,2)) AS [AVG MILK] " + strQty + " from(SELECT TSPL_MILK_SRN_HEAD.shift
                    ,TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader as [SOCIETY CODE]
                    , TSPL_VENDOR_MASTER.Vendor_Name As [SOCIETY NAME]
                    " + strRouteName + "
                    ,TSPL_VLC_MASTER_HEAD.created_date as [VLC Created Date]
                    ,cast(TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) AS Quantity " + strMCCCodeName + " from TSPL_MILK_SRN_DETAIL left outer join  TSPL_MILK_SRN_HEAD On  TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_SRN_HEAD.MCC_CODE left outer join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_code 
                    Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                    " + strRoutJoin + "
                    Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                     where CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103)>=convert(date,'" + fromDate.Value + "',103) and CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103)<=convert(date,'" + ToDate.Value + "',103)"

                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    qry += " and TSPL_MILK_SRN_HEAD.MCC_CODE  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                End If

                qry += " )final where 1=1  " + strGroupByWithOrderby + " "
            ElseIf rbtn_Summary.Checked = True Then
                Dim strFromDateToDateCompName As String = " "
                If MultipleFinderFillAuto = True Then
                    Dim strFromDate As String = clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy"))
                    Dim strToDate As String = clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy"))
                    Dim strCompanyName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'"))
                    strFromDateToDateCompName = "'" + strFromDate + "'" + " as FromDate " + ",'" + strToDate + "'" + " as ToDate , '" + strCompanyName + "' as CompName ,"

                End If

                qry = "select  " + IIf(MultipleFinderFillAuto = True, " " + strFromDateToDateCompName + " ", " ") + "  (CONVERT(VARCHAR(10),max(TSPL_Milk_Quantity_Slab.max_range))+ ' - ' + CONVERT(VARCHAR(10),CONVERT(INT,max(TSPL_Milk_Quantity_Slab.min_range)))) as [RANGE]
                        ,count(yy.line_no) as [NO OF SOCIETIES] " + IIf(MultipleFinderFillAuto = True, " ,sum([AVG MILK]) as [AVG MILK], sum (Quantity) as Quantity ", " ") + "  from
                        (select case when xx.[AVG MILK]>=sl.Min_Range and xx.[AVG MILK]<=sl.Max_Range then sl.line_no else 0 end as LINE_NO,[AVG MILK] , Quantity
                        from (select 1 as SNO, CAST(SUM(Quantity)/COUNT(shift) as decimal(18,2)) AS [AVG MILK],SUM(Quantity) as Quantity,
                        final.[SOCIETY CODE],MAX(final.[SOCIETY NAME]) AS [SOCIETY NAME],MAX(final.[ROUTE]) AS [ROUTE],MAX(final.[VLC Created Date]) AS [VLC Created Date]
                        ,mcc_code from(SELECT TSPL_MCC_MASTER.mcc_code,TSPL_MILK_SRN_HEAD.shift
                        ,TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader as [SOCIETY CODE], TSPL_VENDOR_MASTER.Vendor_Name As [SOCIETY NAME]
                        ,TSPL_MCC_ROUTE_MASTER.Route_Name as [ROUTE],TSPL_VLC_MASTER_HEAD.created_date as [VLC Created Date]
                         ,cast(TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) AS Quantity from TSPL_MILK_SRN_DETAIL left outer join  TSPL_MILK_SRN_HEAD On  TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_SRN_HEAD.MCC_CODE left outer join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_code 
                        Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                        Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
                        Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                        where CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103)>=convert(date,'" + fromDate.Value + "',103) and CONVERT(date,TSPL_MILK_SRN_HEAD.Doc_Date,103)<=convert(date,'" + ToDate.Value + "',103)"

                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    qry += " and TSPL_MILK_SRN_HEAD.MCC_CODE  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                End If

                qry += ")final where 1=1 GROUP BY [SOCIETY CODE],mcc_code)xx
                        left join ( select 1 as SNO, TSPL_Milk_Quantity_Slab.min_range,TSPL_Milk_Quantity_Slab.max_range,TSPL_Milk_Quantity_Slab.line_no from  TSPL_Milk_Quantity_Slab)sl on xx.SNO = sl.SNO
                        )  yy 
                        left join TSPL_Milk_Quantity_Slab on TSPL_Milk_Quantity_Slab.line_no=yy.Line_no
                        where yy.line_no>0
                        group by TSPL_Milk_Quantity_Slab.line_no
                        " + IIf(MultipleFinderFillAuto = True, " order by TSPL_Milk_Quantity_Slab.line_no desc ", " order by TSPL_Milk_Quantity_Slab.line_no asc ") + " "
            End If

            dt1 = Nothing
            dt1 = clsDBFuncationality.GetDataTable(qry)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()
            If MultipleFinderFillAuto Then
                'Gv1.Columns("FromDate").IsVisible = False
                'Gv1.Columns("ToDate").IsVisible = False
            End If

            If isPrerint = True Then
                If rbtn_Detail.Checked Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt1, "rptMilkAnalysis", "Milk Analysis")
                    frmCRV = Nothing
                ElseIf rbtn_Summary.Checked Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt1, "rptDCSSummarByRange", "Milk Analysis Summary")
                    frmCRV = Nothing
                End If
            End If

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                'btnGo.Enabled = False
                EnableDisableCtrl(False)
                SetGridFormat()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat()
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).Width = 150
        Next

        If Gv1.Rows.Count > 0 Then
            If rbtn_Detail.Checked = True Then
                If MultipleFinderFillAuto Then
                    'Gv1.GroupDescriptors.Add(New GridGroupByExpression("[ROUTE CODE] as [ROUTE CODE] format ""{0}: {1}"" Group By [ROUTE CODE]"))
                Else
                    Gv1.GroupDescriptors.Add(New GridGroupByExpression("[AVG MILK] as [AVG MILK] format ""{0}: {1}"" Group By [AVG MILK]"))
                End If

            ElseIf rbtn_Summary.Checked = True Then
                Dim summaryRowItem As New GridViewSummaryRowItem()

                Dim ItemRANGE As New GridViewSummaryItem("RANGE", "TOTAL", GridAggregateFunction.Max)
                summaryRowItem.Add(ItemRANGE)

                Dim item1 As New GridViewSummaryItem("NO OF SOCIETIES", "{0:n0}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)

                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                End If
            End If

            'If Gv1.Rows.Count > 0 Then
            '    Dim summaryRowItemB As New GridViewSummaryRowItem()
            '    Dim summaryRowItemC As New GridViewSummaryRowItem()


            '    Dim MilkTypeB As New GridViewSummaryItem("Milk Type", "B", GridAggregateFunction.Max)
            '    summaryRowItemB.Add(MilkTypeB)
            '    Dim MilkTypeC As New GridViewSummaryItem("Milk Type", "C", GridAggregateFunction.Max)
            '    summaryRowItemC.Add(MilkTypeC)

            '    For i As Integer = 8 To 8 + 2 + dtREJECT.Rows.Count
            '        Dim aa = Gv1.Columns(i).HeaderText()

            '        Dim summaryItemB As New GridViewSummaryItem()
            '        summaryItemB.FormatString = "{0:n2}"
            '        summaryItemB.Name = aa
            '        summaryItemB.AggregateExpression = "sum(IIF([Milk Type]='B',[" + aa + "],0))"
            '        summaryRowItemB.Add(summaryItemB)

            '        Dim summaryItemC As New GridViewSummaryItem()
            '        summaryItemC.FormatString = "{0:n2}"
            '        summaryItemC.Name = aa
            '        summaryItemC.AggregateExpression = "sum(IIF([Milk Type]='C',[" + aa + "],0))"
            '        summaryRowItemC.Add(summaryItemC)

            '    Next

            '    For i As Integer = 9 To 9 + 2 + dtREJECT.Rows.Count - 1
            '        Dim aa = Gv1.Columns(i).HeaderText()

            '        Dim summaryItemB As New GridViewSummaryItem()
            '        summaryItemB.FormatString = "{0:n2}"
            '        summaryItemB.Name = aa + "(%)"
            '        summaryItemB.AggregateExpression = "sum(IIF([Milk Type]='B',[" + aa + "],0))*100/sum(IIF([Milk Type]='B',[Milk Weight],0))"
            '        summaryRowItemB.Add(summaryItemB)

            '        Dim summaryItemC As New GridViewSummaryItem()
            '        summaryItemC.FormatString = "{0:n2}"
            '        summaryItemC.Name = aa + "(%)"
            '        summaryItemC.AggregateExpression = "sum(IIF([Milk Type]='C',[" + aa + "],0))*100/sum(IIF([Milk Type]='C',[Milk Weight],0))"
            '        summaryRowItemC.Add(summaryItemC)

            '    Next


            'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
            'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemC)
            'End If

            Gv1.AutoSizeRows = True
        'Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
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
            Dim strHeading As String = clsCommon.myCstr("Milk Analysis For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Name : " & "Milk Bill Procurement Summary For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value)
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            End If
            'arrHeader.Add("MCC : " + txtmccode.Value)
            'arrHeader.Add("Fiscal Year : " + txtFiscalYear.Value)
            'arrHeader.Add("Payment Cycle : " + txtPaymentCycleFrom.Value)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text)
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


    Private Sub TxtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MBPSMCC1", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    'Private Sub Txtmccode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim whrcls As String = ""

    '    txtmccode.Value = clsMccMaster.getFinder(whrcls, txtmccode.Value, isButtonClicked)
    '    If clsCommon.myLen(txtmccode.Value) > 0 Then
    '        lblmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + txtmccode.Value + "'"))
    '    Else
    '        txtmccode.Value = ""
    '        lblmccname.Text = ""
    '    End If
    'End Sub

    Private Sub TxtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
            txtFiscalYear.Value = clsCommon.ShowSelectForm("LRFY", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
            'fromDate.Value = clsCommon.GETSERVERDATE()
            'ToDate.Value = clsCommon.GETSERVERDATE()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycleFrom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleFrom._MYValidating
        Try
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim whrcls As String = " Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim qry As String = "SELECT distinct convert(int,name) as Code,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date] FROM TSPL_PAYMENT_CYCLE_GENERATED"
            txtPaymentCycleFrom.Value = clsCommon.ShowSelectForm("LRPCF", qry, "Code", whrcls, txtPaymentCycleFrom.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtPaymentCycleFrom.Value) > 0 Then
                txtPaymentCycleTo.Value = txtPaymentCycleFrom.Value
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try

    End Sub

    Private Sub Patment_Cycle_changed()
        Try
            Dim dt As DataTable
            Dim qry As String = "SELECT Name ,From_Date,To_Date FROM TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleFrom.Value + "' "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found for Selected Fiscal Year", Me.Text)
                Exit Sub
            End If

            fromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select From_Date from TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleFrom.Value + "' "))
            ToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select To_Date from TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleTo.Value + "' "))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycleTo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleTo._MYValidating
        Try
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim whrcls As String = " Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim qry As String = "SELECT distinct convert(int,name) as Code,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date] FROM TSPL_PAYMENT_CYCLE_GENERATED"
            txtPaymentCycleTo.Value = clsCommon.ShowSelectForm("LRPCT", qry, "Code", whrcls, txtPaymentCycleTo.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub RmiExcelGrid_Click(sender As Object, e As EventArgs) Handles rmiExcelGrid.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr("Milk Analysis For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            Dim arrHeader As List(Of String) = New List(Of String)()

            If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            End If

            clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text, True)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub BtnSaveUpdate_Click(sender As Object, e As EventArgs) Handles btnSaveUpdate.Click
        Dim obj As New clsMilkQuantitySlab()
        Dim arrSLAB As List(Of clsMilkQuantitySlab)
        arrSLAB = New List(Of clsMilkQuantitySlab)
        Dim IntCounter As Integer = 0
        For ii As Integer = 0 To gvTS.RowCount - 1
            Dim objTS1 As New clsMilkQuantitySlab
            objTS1.Min_Range = clsCommon.myCdbl(gvTS.Rows(ii).Cells(colRange).Value)
            objTS1.Max_Range = clsCommon.myCdbl(gvTS.Rows(ii).Cells(colTo).Value)
            If objTS1.Max_Range > 0 Then
                IntCounter = IntCounter + 1
                objTS1.Line_No = IntCounter
                arrSLAB.Add(objTS1)
            End If
        Next
        If arrSLAB.Count <= 0 Then
            Throw New Exception("Please define at least one row for slab.")
        End If
        Dim isSaved As Boolean = False
        isSaved = obj.SaveData(arrSLAB)
        If isSaved Then
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully.", Me.Text)
        End If
    End Sub

    Private Sub LoadRange()
        Try
            Dim arrSLAB As List(Of clsMilkQuantitySlab)
            arrSLAB = clsMilkQuantitySlab.GetData()
            For Each objQts As clsMilkQuantitySlab In arrSLAB
                gvTS.Rows.AddNew()
                gvTS.Rows(gvTS.Rows.Count - 1).Cells(colRange).Value = objQts.Min_Range
                gvTS.Rows(gvTS.Rows.Count - 1).Cells(colTo).Value = objQts.Max_Range
                'If gvTS.Rows.Count > 1 Then
                '    gvTS.Rows(gvTS.Rows.Count - 2).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(objQts.Min_Range) - 1)
                'End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub GvTS_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvTS.UserDeletedRow
        Try
            If gvTS.CurrentRow.Index > 0 Then
                gvTS.Rows(gvTS.CurrentRow.Index - 1).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(gvTS.Rows(gvTS.CurrentRow.Index).Cells(colRange).Value) - 0.01)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub GvTS_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvTS.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvTS_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvTS.CurrentColumnChanged
        If gvTS.RowCount > 0 Then
            Dim intCurrRow As Integer = gvTS.CurrentRow.Index
            If intCurrRow = gvTS.Rows.Count - 1 Then
                gvTS.Rows.AddNew()
                gvTS.CurrentRow = gvTS.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvTS_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvTS.CellValueChanged
        Try
            If e.Column.Name = colRange And clsCommon.myCdbl(e.Value) > 0 And gvTS.CurrentRow.Index > 0 Then
                gvTS.Rows(gvTS.CurrentRow.Index - 1).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(e.Value) - 0.01)
                If (gvTS.CurrentRow.Index + 1 = gvTS.Rows.Count) OrElse (clsCommon.myCdbl(gvTS.Rows(gvTS.CurrentRow.Index + 1).Cells(colTo).Value) = 0) Then
                    gvTS.Rows(gvTS.CurrentRow.Index).Cells(colTo).Value = 5000
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub loadBlankGridRange()
        Try
            gvTS.Rows.Clear()
            gvTS.Columns.Clear()

            'Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
            'repoLineNo = New GridViewDecimalColumn()
            'repoLineNo.FormatString = ""
            'repoLineNo.HeaderText = "Line No"
            'repoLineNo.Name = colLineNo
            'repoLineNo.Width = 50
            'repoLineNo.ReadOnly = True
            'repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            'gvTS.MasterTemplate.Columns.Add(repoLineNo)

            Dim repoDeciCol As GridViewDecimalColumn
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colRange
            repoDeciCol.Width = 120
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 5000
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            'repoDeciCol.HeaderText = "Slab Upto"
            repoDeciCol.HeaderText = "From"
            gvTS.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colTo
            repoDeciCol.Width = 120
            repoDeciCol.FormatString = "{0:n2}"
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 5000
            repoDeciCol.ReadOnly = True
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.Step = 0
            repoDeciCol.HeaderText = "To"
            gvTS.MasterTemplate.Columns.Add(repoDeciCol)

            'repoDeciCol = New GridViewDecimalColumn()
            'repoDeciCol.Name = colRate
            'repoDeciCol.Width = 200
            'repoDeciCol.DecimalPlaces = 2
            'repoDeciCol.Minimum = 0
            'repoDeciCol.Step = 0
            'repoDeciCol.ShowUpDownButtons = False
            'repoDeciCol.HeaderText = "Qty"
            'gvTS.MasterTemplate.Columns.Add(repoDeciCol)

            gvTS.AllowDeleteRow = True
            gvTS.AllowAddNewRow = False
            gvTS.ShowGroupPanel = False
            gvTS.AllowColumnReorder = False
            gvTS.AllowRowReorder = False
            gvTS.EnableSorting = False
            gvTS.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvTS.MasterTemplate.ShowRowHeaderColumn = False
            gvTS.TableElement.TableHeaderHeight = 40
            gvTS.AutoSizeRows = False
            gvTS.AllowRowReorder = True
            'gvTS.Rows.AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rptMilkAnalysis_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'If e.Control AndAlso e.Alt AndAlso e.KeyCode = Keys.D Then
        '    Dim pwd As New FrmPWD(Nothing)
        '    pwd.strCode = clsFixedParameterCode.AllowToSaveAndUpdatePasswordBased
        '    pwd.strType = clsFixedParameterType.AllowToSaveAndUpdatePasswordBased
        '    pwd.ShowDialog()
        '    If pwd.isPasswordCorrect Then
        '        GroupBox76.Visible = True
        '        loadBlankGridRange()
        '        LoadRange()
        '        If gvTS.Rows.Count = 0 Then
        '            gvTS.Rows.AddNew()
        '        End If
        '    Else
        '        GroupBox76.Visible = False
        '        loadBlankGridRange()
        '        gvTS.Rows.AddNew()
        '    End If
        'End If

    End Sub


    Private Sub Btn_RangeClose_Click(sender As Object, e As EventArgs) Handles btn_RangeClose.Click
        'GroupBox76.Visible = False
    End Sub
    Sub SetToDate()
        If MultipleFinderFillAuto Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0

            'If clsCommon.myLen(fndLoc.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
            '    clsCommon.MyMessageBoxShow("Please select the Location first")
            '    Exit Sub
            'End If
            'If MultipleFinderFillAuto = True Then
            '    If mfndMcc.arrValueMember Is Nothing OrElse mfndMcc.arrValueMember.Count <= 0 Then
            '        clsCommon.MyMessageBoxShow("Please select the Location first")
            '        Exit Sub
            '    End If
            'End If
            Dim strMCCcode = ""
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                strMCCcode = " location_Code in ( " + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")  and "
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where " + strMCCcode + "  Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If fromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ", Me.Text)
                    fromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    ToDate.Value = fromDate.Value
                    Exit Sub
                End If
                ToDate.Value = fromDate.Value.AddDays(PaymentCycleValue - 1)

                If fromDate.Value.Month <> ToDate.Value.Month Then
                    ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = ToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If fromDate.Value.Month <> dtNxtPay.Month Then
                    ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                    fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                ToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, fromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                    fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                ToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, fromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = fromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                fromDate.Value = today.AddDays(-dayDiff)
                ToDate.Value = fromDate.Value.AddDays(6)
            End If
            ' End If
            'If clsCommon.myLen(txtMCC.Text) > 0 Then
            '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Text, dtpToDate.Value)
            '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Text, dtpToDate.Value)
            'Else
            '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(fndLoc.Value, dtpToDate.Value)
            '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(fndLoc.Value, dtpToDate.Value)
            'End If
        End If
    End Sub

    Private Sub fromDate_Leave(sender As Object, e As EventArgs) Handles fromDate.Leave
        If MultipleFinderFillAuto = True Then
            SetToDate()
        End If
    End Sub

    Private Sub fromDate_Validating(sender As Object, e As CancelEventArgs) Handles fromDate.Validating
        If MultipleFinderFillAuto = True Then
            SetToDate()
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Print(False, True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub rbtn_Detail_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_Detail.CheckedChanged
        Try
            If rbtn_Detail.Checked = True Then
                GroupBox76.Visible = False
            ElseIf rbtn_Summary.Checked = True Then
                GroupBox76.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub rbtn_Summary_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_Summary.CheckedChanged
        Try
            If rbtn_Detail.Checked = True Then
                GroupBox76.Visible = False
            ElseIf rbtn_Summary.Checked = True Then
                GroupBox76.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
