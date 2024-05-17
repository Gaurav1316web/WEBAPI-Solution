Imports common
Imports System.ComponentModel
Imports System.IO

Public Class frmDBTRecoVsIncentiveReport
    Inherits FrmMainTranScreen

    Dim dt As DataTable = Nothing
    Dim arr As New Dictionary(Of Integer, DataRow)
    Dim strColumnForTotal As String = Nothing
    Dim SettMPIncentiveEntryApplyMonthly As Boolean = False
    Dim SettMPIncentiveEntryCycleWiseButNEFTMonthly As Boolean = False
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Private Sub rptMilkBillProcurementSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        SettMPIncentiveEntryApplyMonthly = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryApplyMonthly, clsFixedParameterCode.MPIncentiveEntryApplyMonthly, Nothing))
        SettMPIncentiveEntryCycleWiseButNEFTMonthly = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MPIncentiveEntryCycleWiseButNEFTMonthly, clsFixedParameterCode.MPIncentiveEntryCycleWiseButNEFTMonthly, Nothing))
        ReportType()
        Reset()
    End Sub

    Private Sub ReportType()
        Dim dt As DataTable
        Dim dr As DataRow
        dt = New DataTable()
        dt.Columns.Add(New DataColumn("Code", System.Type.GetType("System.String")))

        'dr = dt.NewRow()
        'dr("Code") = "All"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Mineral Mixture"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Pashu Aahar"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Zone Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "DCS Wise"
        dt.Rows.Add(dr)

        ddlType.DataSource = dt
        ddlType.ValueMember = "Code"
        ddlType.DisplayMember = "Code"
        ddlType.SelectedIndex = 0
    End Sub

    Sub Reset()
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
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean)
        Try
            If SetToDate() Then
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()

                PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID) + clsCommon.myCstr(ddlType.SelectedValue)
                TemplateGridview = Gv1
                Dim whre As String = ""
                If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                    whre += " and  TSPL_VENDOR_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
                End If
                If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                    whre += " and  TSPL_VENDOR_MASTER.Vendor_Code in (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ") "
                End If
                'If ApplyZoneWiseVSP = True Then
                '    If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
                '        whre += " and TSPL_VENDOR_MASTER.Zone_Code in  (" + objCommonVar.strCurrUserZones + ")"
                '    End If
                'End If
                If clsCommon.myLen(objCommonVar.strCurrUserZones) > 0 Then
                    whre += " and TSPL_VENDOR_MASTER.Zone_Code in  (" + objCommonVar.strCurrUserZones + ")"
                End If

                Dim qryStatus As String = Nothing
                If rbtnMatch.IsChecked Then
                    qryStatus = " where Status='Match' "
                ElseIf rbtnNotMatch.IsChecked Then
                    qryStatus = " where Status='Not Match' "
                Else
                    qryStatus = Nothing
                End If


                Dim QryINCENTIVE_RECO As String = "select yy.Cycle_Year,yy.Cycle_Month
                ,isnull(sum(yy.Qty),0) as RecoQty
                ,yy.Zone_Code as [Zone Code], max(yy.Zone_Name) as [Zone Name]
                ,yy.Vendor_Code,max(yy.Vendor_Name ) as [Vendor Name]
                ,yy.VLC_Code_VLC_Uploader
                ,0 as MPCount
				,0 as Qty
                 from (select Cycle_Year,Cycle_Month,TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_Code,
                TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Qty
                ,isnull(TSPL_VENDOR_MASTER.Zone_Code,'') as Zone_Code, TSPL_ZONE_MASTER.Description as Zone_Name
                ,TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name
                ,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader
                 from (select * from TSPL_DCS_MP_INCENTIVE_RECO_DETAIL union all select * from TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID)TSPL_DCS_MP_INCENTIVE_RECO_DETAIL
                LEFT OUTER JOIN TSPL_DCS_MP_INCENTIVE_RECO_HEAD ON TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code
                left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_Code
                left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =  TSPL_VLC_MASTER_HEAD.VSP_Code
                left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_VENDOR_MASTER.Zone_Code
                where CONVERT(date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date_To,103)='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' " + whre + "  
                )yy group by yy.Cycle_Year,yy.Cycle_Month
                ,yy.Zone_Code
                ,yy.Vendor_Code,yy.VLC_Code_VLC_Uploader "

                'Dim QryINCENTIVE As String = "select xx.Cycle_Year,xx.Cycle_Month
                '    ,xx.MCC_Code
                '    ,sum(xx.Qty) as Qty
                '    ,xx.Zone_Code as [Zone Code], max(xx.Zone_Name) as [Zone Name]
                '    ,xx.Vendor_Code,max(xx.Vendor_Name ) as [Vendor Name] 
                '    ,xx.MP_Code
                '    from (select TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_Year,TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_Month
                '    ,TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code
                '    ,TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
                '    ,TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty
                '    ,TSPL_VENDOR_MASTER.Zone_Code, TSPL_ZONE_MASTER.Description as Zone_Name
                '    ,TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name
                '    ,TSPL_MP_MASTER.MP_Code
                '    from TSPL_MP_INCENTIVE_ENTRY_DETAIL 
                '    left outer join TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
                '    left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code
                '    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
                '    left outer join tspl_MCC_Master on tspl_MCC_Master.MCC_Code=TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code
                '    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =  TSPL_VLC_MASTER_HEAD.VSP_Code
                '    left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_VENDOR_MASTER.Zone_Code
                '    where CONVERT(date,TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,TSPL_MP_INCENTIVE_ENTRY_HEAD.To_Date,103)='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' " + whre + "  
                '    )xx group by xx.Cycle_Year,xx.Cycle_Month
                '    ,xx.MCC_Code
                '    ,xx.Zone_Code
                '    ,xx.Vendor_Code
                '    ,xx.MP_Code"
                Dim QryINCENTIVE As String = "select xx.Cycle_Year,xx.Cycle_Month
                    ,0 as RecoQty
                    ,xx.Zone_Code as [Zone Code], max(xx.Zone_Name) as [Zone Name]
                    ,xx.Vendor_Code,max(xx.Vendor_Name ) as [Vendor Name] 
                    ,xx.VLC_Code_VLC_Uploader
                    ,isnull(count(distinct xx.MP_Code),0) as MPCount
                    ,isnull(sum(xx.Qty),0) as Qty
                    from (select TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_Year,TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_Month
                    ,TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
                    ,TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty
                    ,isnull(TSPL_VENDOR_MASTER.Zone_Code,'') as Zone_Code, TSPL_ZONE_MASTER.Description as Zone_Name
                    ,TSPL_VENDOR_MASTER.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name
                    ,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader
                    ,TSPL_MP_MASTER.MP_Code
                    from TSPL_MP_INCENTIVE_ENTRY_DETAIL 
                    left outer join TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
                    left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code
                    left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =  TSPL_VLC_MASTER_HEAD.VSP_Code
                    left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_VENDOR_MASTER.Zone_Code
                    where CONVERT(date,TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date,103)='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and CONVERT(date,TSPL_MP_INCENTIVE_ENTRY_HEAD.To_Date,103)='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' " + whre + "  
                    )xx group by xx.Cycle_Year,xx.Cycle_Month
                    ,xx.Zone_Code
                    ,xx.Vendor_Code,xx.VLC_Code_VLC_Uploader"
                Dim Qry As String = ""
                'If clsCommon.CompairString(ddlType.SelectedValue, "Zone Wise") = CompairStringResult.Equal Then
                '    Qry = "select INCENTIVE_RECO.[Zone Code],max(INCENTIVE_RECO.[Zone Name]) as [Zone Name],
                '        isnull(sum(INCENTIVE_RECO.Qty),0) as [Reco Qty]
                '        , isnull(sum(INCENTIVE.MPCount),0) as [MP Count]
                '        ,isnull(sum(INCENTIVE.Qty),0) as [Incentive Qty]
                '        ,(case when isnull(sum(INCENTIVE_RECO.Qty),0)=isnull(sum(INCENTIVE.Qty),0) then 'Match' else 'Not Match' end) as [Status]
                '         from (" + QryINCENTIVE_RECO +
                '        " )INCENTIVE_RECO
                '        left outer join
                '        (" + QryINCENTIVE + " )INCENTIVE on  INCENTIVE.Cycle_Month= INCENTIVE_RECO.Cycle_Month
                '        and INCENTIVE.Cycle_Year= INCENTIVE_RECO.Cycle_Year
                '        and INCENTIVE.Vendor_Code= INCENTIVE_RECO.Vendor_Code
                '        and INCENTIVE.VLC_Code_VLC_Uploader= INCENTIVE_RECO.VLC_Code_VLC_Uploader
                '        and INCENTIVE.[Zone Code]=INCENTIVE_RECO.[Zone Code]
                '        group by INCENTIVE_RECO.[Zone Code]"
                'ElseIf clsCommon.CompairString(ddlType.SelectedValue, "DCS Wise") = CompairStringResult.Equal Then
                '    Qry = "select INCENTIVE_RECO.VLC_Code_VLC_Uploader as [DCS Code],max(INCENTIVE_RECO.[Vendor Name]) AS [DCS Name],INCENTIVE_RECO.Vendor_Code as [Code],
                '    isnull(sum(INCENTIVE_RECO.Qty),0) as [Reco Qty]
                '    , isnull(sum(INCENTIVE.MPCount),0) as [MP Count]
                '    ,isnull(sum(INCENTIVE.Qty),0) as [Incentive Qty]
                '    ,(case when isnull(sum(INCENTIVE_RECO.Qty),0)=isnull(sum(INCENTIVE.Qty),0) then 'Match' else 'Not Match' end) as [Status]
                '     from (" + QryINCENTIVE_RECO +
                '    " )INCENTIVE_RECO
                '    left outer join
                '    (" + QryINCENTIVE + " )INCENTIVE on  INCENTIVE.Cycle_Month= INCENTIVE_RECO.Cycle_Month
                '    and INCENTIVE.Cycle_Year= INCENTIVE_RECO.Cycle_Year
                '    and INCENTIVE.Vendor_Code= INCENTIVE_RECO.Vendor_Code
                '    and INCENTIVE.VLC_Code_VLC_Uploader= INCENTIVE_RECO.VLC_Code_VLC_Uploader
                '    and INCENTIVE.[Zone Code]=INCENTIVE_RECO.[Zone Code]
                '    group by INCENTIVE_RECO.Vendor_Code,INCENTIVE_RECO.VLC_Code_VLC_Uploader"
                'End If
                'If rbtnMatch.IsChecked = True Then
                '    Qry += " having isnull(sum(INCENTIVE_RECO.Qty),0)=isnull(sum(INCENTIVE.Qty),0)"
                'ElseIf rbtnNotMatch.IsChecked = True Then
                '    Qry += " having isnull(sum(INCENTIVE_RECO.Qty),0)<>isnull(sum(INCENTIVE.Qty),0)"
                'End If

                If clsCommon.CompairString(ddlType.SelectedValue, "Zone Wise") = CompairStringResult.Equal Then
                    Qry = "Select * from (select INCENTIVE.[Zone Code],max(INCENTIVE.[Zone Name]) AS [Zone Name],
                    isnull(sum(INCENTIVE.RecoQty),0) as [Reco Qty]
                    , isnull(sum(INCENTIVE.MPCount),0) as [MP Count]
                    ,isnull(sum(INCENTIVE.Qty),0) as [Incentive Qty]"
                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                        Qry += " ,(case when abs(isnull(sum(INCENTIVE.RecoQty),0)-isnull(sum(INCENTIVE.Qty),0))=0 then 'Match' else 'Not Match' end) as [Status] "
                    Else
                        Qry += " ,(case when abs(isnull(sum(INCENTIVE.RecoQty),0)-isnull(sum(INCENTIVE.Qty),0))<1 then 'Match' else 'Not Match' end) as [Status]  "
                    End If
                    Qry += "                  
                     from (" + QryINCENTIVE_RECO +
                    " union all " + QryINCENTIVE + " )INCENTIVE 
                    group by INCENTIVE.Cycle_Year,INCENTIVE.Cycle_Month,INCENTIVE.[Zone Code]"
                ElseIf clsCommon.CompairString(ddlType.SelectedValue, "DCS Wise") = CompairStringResult.Equal Then
                    Qry = "Select * from (select INCENTIVE.VLC_Code_VLC_Uploader as [DCS Code],max(INCENTIVE.[Vendor Name]) AS [DCS Name],INCENTIVE.Vendor_Code as [Code],
                    isnull(sum(INCENTIVE.RecoQty),0) as [Reco Qty]
                    , isnull(sum(INCENTIVE.MPCount),0) as [MP Count]
                    ,isnull(sum(INCENTIVE.Qty),0) as [Incentive Qty]"

                    If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "GNG") = CompairStringResult.Equal Then
                        Qry += " ,(case when abs(isnull(sum(INCENTIVE.RecoQty),0)-isnull(sum(INCENTIVE.Qty),0))=0 then 'Match' else 'Not Match' end) as [Status] "
                    Else
                        Qry += " ,(case when abs(isnull(sum(INCENTIVE.RecoQty),0)-isnull(sum(INCENTIVE.Qty),0))<1 then 'Match' else 'Not Match' end) as [Status]  "
                    End If
                    Qry += "               
                     from (" + QryINCENTIVE_RECO +
                    " union all " + QryINCENTIVE + " )INCENTIVE 
                    group by INCENTIVE.Cycle_Year,INCENTIVE.Cycle_Month,INCENTIVE.Vendor_Code,INCENTIVE.VLC_Code_VLC_Uploader"
                End If
                If rbtnMatch.IsChecked = True Then
                    Qry += " having abs(isnull(sum(INCENTIVE.RecoQty),0)-isnull(sum(INCENTIVE.Qty),0))<1"
                ElseIf rbtnNotMatch.IsChecked = True Then
                    Qry += " having abs(isnull(sum(INCENTIVE.RecoQty),0)-isnull(sum(INCENTIVE.Qty),0))>=1"
                End If
                Qry += ") abc" + qryStatus
                dt = Nothing
                dt = clsDBFuncationality.GetDataTable(Qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("No Data Found to Display")
                End If


                Gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                SetGridFormat(Gv1)
                EnableDisaableControls(False)
                ReStoreGridLayout()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            '  If rbtnNEFT.IsChecked Then
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub SetGridFormat(ByRef Gv1 As RadGridView)
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next

        Gv1.Columns("Reco Qty").FormatString = "{0:n2}"
        Gv1.Columns("Incentive Qty").FormatString = "{0:n2}"
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim FreightCost As New GridViewSummaryItem("Reco Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(FreightCost)
        Dim SalesInLTR As New GridViewSummaryItem("Incentive Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SalesInLTR)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Gv1.AutoSizeRows = False
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisaableControls(True)
    End Sub
    Sub EnableDisaableControls(ByVal flag As Boolean)
        txtFromDate.Enabled = flag
        txtToDate.Enabled = flag
        ddlType.Enabled = flag
        txtDCS.Enabled = flag
        txtZone.Enabled = flag
        GroupBox1.Enabled = flag
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
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmDBTRecoVsIncentiveReport & "'"))

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Type : " + clsCommon.myCstr(ddlType.SelectedValue))
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtZone.arrValueMember))
            End If
            If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                arrHeader.Add("DCS : " + clsCommon.GetMulcallStringWithComma(txtDCS.arrValueMember))
            End If

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text,, arrHeader)
                transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, True)
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

    Private Sub txtFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub
    Private Sub txtFromDate_Leave(sender As Object, e As EventArgs) Handles txtFromDate.Leave
        SetToDate()
    End Sub
    Function SetToDate() As Boolean
        Try
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select Payment_Cycle,PC_TYPE,PC_VALUE from ( select TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE as Payment_Cycle,
 TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_PAYMENT_CYCLE_MASTER  where 
 TSPL_PAYMENT_CYCLE_MASTER.IsDefault=1 ) xx group by Payment_Cycle,PC_TYPE,PC_VALUE")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Payment Cycle found on current MCC/Location")
            End If
            If dt.Rows.Count > 1 Then
                Throw New Exception("Selected MCC's Payment Cycle Should be Same")
            End If
            If SettMPIncentiveEntryApplyMonthly OrElse SettMPIncentiveEntryCycleWiseButNEFTMonthly Then
                txtFromDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
                txtToDate.Value = txtFromDate.Value.AddMonths(1).AddDays(-1)
            Else
                PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
                PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
                Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
                If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                    If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                        txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                        txtToDate.Value = txtFromDate.Value
                        Throw New Exception("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    End If
                    txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)

                    If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                        txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                    Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                    If txtFromDate.Value.Month <> dtNxtPay.Month Then
                        txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then

                        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        Throw New Exception("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    End If
                    txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
                ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                        Throw New Exception("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                    End If
                    txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
                ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                    Dim today As Date = txtFromDate.Value
                    Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                    txtFromDate.Value = today.AddDays(-dayDiff)
                    txtToDate.Value = txtFromDate.Value.AddDays(6)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Gv1.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = PageSetupReport_ID
                obj.UserID = objCommonVar.CurrentUserCode
                obj.GridLayout = New MemoryStream()
                Gv1.SaveLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                obj.GridColumns = Gv1.ColumnCount
                If obj.SaveData() Then
                    common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
                End If
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Try
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Dim qry As String = " select TSPL_ZONE_MASTER.Zone_Code as Code , TSPL_ZONE_MASTER.Description as Name from TSPL_ZONE_MASTER where 2=2 "
        If clsCommon.myLen(objCommonVar.strCurrUserZones) > 0 Then
            qry += "  and TSPL_ZONE_MASTER.Zone_Code in (" + objCommonVar.strCurrUserZones + ") "
        End If
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSelZone@MPIncentiveEntryRPT", qry, "Code", "Code", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub

    Private Sub txtDCS__My_Click(sender As Object, e As EventArgs) Handles txtDCS._My_Click
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER  where Form_Type ='VSP' And Status='N' "
        txtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTDCS1", qry, "Code", "Name", txtDCS.arrValueMember, txtDCS.arrDispalyMember)
    End Sub
End Class
