Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'===============update by preeti gupta against ticket no[BM00000006682]
Public Class RptDispatchofmilkTransfer2
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptDispatchOfMilkTransfer)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton4.Visible = MyBase.isExport
    End Sub
    
    Sub LoadMCC()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then

            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If
        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"

    End Sub

   

    Sub Reset()
        LOCATIONRIGTHS()
        txtDate.Value = clsCommon.GETSERVERDATE()
        LoadMCC()
        chkMCCAll.CheckState = CheckState.Checked

        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        ChkAll.IsChecked = True
        gvReport.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub chkMCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub RptDispatchofmilkTransfer2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gvReport
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gvReport.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptDispatchOfMilkTransfer & "'"))
                arrHeader.Add("Date : " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") + "")

                If chkMCCSelect.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgMCC.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgMCC.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add((" MCC Name: " + strMCCName + " "))
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
                    transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvReport, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gvReport, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gvReport, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Dispatch Milk Transfer", gvReport, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub butnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvReport.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvReport.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvReport.ColumnCount
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
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Public Sub Load_Report()
        try
        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Location or select all.", Me.Text)
            Exit Sub
        End If
        Dim sQuery As String = " ;with wholedata as (select case when sum(Dis_qty_Inter) >0 then xx.isIntermittent else 0 end  as isIntermittent,'' as SNO, coalesce(yy.State_Code,xx.state_code) as state_code ,Max(coalesce(yy.STATE_NAME,xx.STATE_NAME)) as STATE_NAME ,coalesce(yy.MCC_CODE,xx.MCC_CODE) as MCC_CODE,max(coalesce(yy.MCC_NAME,xx.MCC_NAME)) as MCC_NAME,max(convert(varchar,coalesce(yy.DOC_DATE,xx.Dispatch_Date),103))  as DOC_DATE,isnull(yy.Opening,0) as  Opening ,isnull(yy.closing ,0) as  closing , isnull(yy.milk_pro,0 ) as milk_proc,isnull(yy.milk_pro_Inter ,0 )as milk_pro_Inter,sum(xx.Dis_qty) as Dis_qty,sum(xx.Dis_qty_Inter ) as Dis_qty_Inter, Max(xx.Tanker_Dispatch_To) as Tanker_Dispatch_To,Max (xx.Tanker_Dispatch_Time) as Tanker_Dispatch_Time,(Tanker_No) as Tanker_No ,(convert(varchar,Reached_Date_time,103)) as Reached_Date_time  from "
        sQuery += " (select 0 as isIntermittent,  TSPL_MCC_MASTER.State_Code ,Max(TSPL_STATE_MASTER.STATE_NAME) as STATE_NAME ,TSPL_MILK_Shift_End_HEAD.MCC_CODE,max(TSPL_MCC_MASTER.MCC_NAME)"
        sQuery += "  as MCC_NAME  ,max(TSPL_MILK_Shift_End_HEAD.DOC_DATE)  as DOC_DATE,max(opening.manual_stock) as  Opening,max(closing.manual_stock) as  closing  ,0 as milk_pro_Inter,sum(tspl_milk_shift_end_route_detail.Actual_Weight) "
        sQuery += " as milk_pro from TSPL_MILK_Shift_End_HEAD  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER .MCC_Code =TSPL_MILK_Shift_End_HEAD.MCC_CODE "
        sQuery += " left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_MCC_MASTER.State_Code "
        sQuery += " left join TSPL_OPEN_MCC_SHIFT on convert(date,TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_DATE,103)=convert(date,'" + txtDate.Value + "',103) and  TSPL_OPEN_MCC_SHIFT.SHIFT='M' and TSPL_OPEN_MCC_SHIFT.MCC_CODE=TSPL_MILK_Shift_End_HEAD.MCC_CODE left outer join tspl_milk_shift_end_route_detail on tspl_milk_shift_end_route_detail.DOC_CODE =TSPL_MILK_Shift_End_HEAD.DOC_CODE  "
        'sQuery += " Left join (select location_code,round(sum(Qty),2) as Qty,round(SUM(FAT),2) as FAT,round(SUM(SNF),2) as SNF,Item_COde  from (select TSPL_INVENTORY_MOVEMENT_NEW.location_code,case when InOut='O'  then case when UOM='ltr' then -Qty*(Contained_Qty)  else -qty end else case when UOM='ltr' then  Qty*(Contained_Qty)  else qty end  end as Qty,case when InOut='O' then -FAT_KG else FAT_Kg end as FAT,case when InOut='O' then -SNF_KG  else SNF_KG end as SNF,Item_Code from TSPL_INVENTORY_MOVEMENT_NEW left join (select * from TSPL_WEIGHT_CONVERSION where  Container_UOM='ltr' and Contained_UOM='kg') conv on conv.Container_UOM=UOM and Contained_UOM='kg' where Item_Code=(select  Description from TSPL_FIXED_PARAMETER where Type='MCCDefaultMilkItem')  and convert(date,Source_Doc_Date,103)<=convert(date,'" + txtDate.Value + "',103) )    tt group by Item_Code,location_code ) Opening on Opening.Location_Code=TSPL_OPEN_MCC_SHIFT.MCC_CODE"
        sQuery += " left join (select mcc_code,mcc_shift_date,sum(manual_stock) as manual_stock from TSPL_OPEN_MCC_SHIFT where 2=2 and shift='M' and  convert(date,mcc_shift_date,103)=convert(date,'" + txtDate.Value + "',103)  group by mcc_code,mcc_shift_date)Opening on Opening.mcc_code=TSPL_OPEN_MCC_SHIFT.MCC_CODE "
        sQuery += "left join (select mcc_code,DOC_DATE ,sum(manual_stock) as manual_stock from TSPL_MILK_Shift_End_HEAD where 2=2 and shift='E' and  convert(date,DOC_DATE,103)=convert(date,'" + txtDate.Value + "',103) group by mcc_code,DOC_DATE)closing on closing.mcc_code=TSPL_OPEN_MCC_SHIFT.MCC_CODE "
        sQuery += " where 2=2 and  convert(date,TSPL_MILK_Shift_End_HEAD.Doc_date,103)=convert(date,'" + txtDate.Value + "',103)"

        If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
            sQuery += "and TSPL_MILK_Shift_End_HEAD.Mcc_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If

        sQuery += " group by TSPL_MCC_MASTER.State_Code ,TSPL_MILK_Shift_End_HEAD.MCC_CODE)"

        sQuery += " yy  full join  (select TSPL_MCC_Dispatch_Challan.isIntermittent,TSPL_MCC_MASTER.State_Code,max(TSPL_STATE_MASTER.STATE_NAME) as STATE_NAME, TSPL_MCC_Dispatch_Challan.MCC_Code ,max(TSPL_MCC_MASTER.MCC_NAME)as MCC_NAME ,max(TSPL_MCC_Dispatch_Challan.Dispatch_Date)  as Dispatch_Date,'' as shift,0 as Opening,0 as closing,0 as Milk_proc,case when max(TSPL_MCC_Dispatch_Challan.isIntermittent)=1 then sum(TSPL_MCC_Dispatch_Challan.Net_Qty)else 0 end as Dis_qty_Inter,case when max(TSPL_MCC_Dispatch_Challan.isIntermittent)=0 then sum(TSPL_MCC_Dispatch_Challan.Net_Qty)else 0 end as Dis_qty,max(TSPL_LOCATION_MASTER.location_desc) as Tanker_Dispatch_To , cast(max(convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,108) )  as varchar) as Tanker_Dispatch_Time ,TSPL_MCC_Dispatch_Challan.Tanker_No ,max(Tspl_Gate_Entry_Details.Date_And_Time ) as Reached_Date_time from TSPL_MCC_Dispatch_Challan left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER .MCC_Code =TSPL_MCC_Dispatch_Challan.MCC_CODE left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_MCC_MASTER.State_Code  left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Challan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code  where convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103)=convert(date,'" + txtDate.Value + "',103) group by isIntermittent,TSPL_MCC_MASTER.State_Code,TSPL_MCC_Dispatch_Challan.MCC_Code,TSPL_MCC_Dispatch_Challan.Tanker_No)as xx on convert(date,xx.Dispatch_Date,103) =convert(date,yy.doc_date,103) and xx.MCC_Code=yy.mcc_code"
        sQuery += " where 2=2 "

        'sQuery += " and  convert(date,yy.Doc_date,103)=convert(date,'" + txtDate.Value + "',103) "

        If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
            sQuery += "and yy.Mcc_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        'If ChkInterMCCSelect.IsChecked And cbgInterMCC.CheckedValue.Count > 0 Then '
        '    sQuery += "and  xx.isIntermittent  IN (" + clsCommon.GetMulcallString(cbgInterMCC.CheckedValue) + ") "
        'End If
        If ChkIntermittent.IsChecked Then
            sQuery += "and  xx.isIntermittent=1 "
        End If
        If ChkNormal.IsChecked Then
            sQuery += "and  xx.isIntermittent=0 "
        End If
        sQuery += " group by yy.isIntermittent,xx.isIntermittent, yy.State_Code ,yy.MCC_CODE,yy.Opening,yy.closing,yy.milk_pro,yy.milk_pro_Inter  ,Tanker_No,Reached_Date_time,xx.MCC_CODE,xx.state_code)"
        sQuery += "  ,FinalWholeData as (select ROW_NUMBER () over (partition by mcc_code order by doc_date) as SLNO,* from wholedata)"
        sQuery += " select case when slno=1 then State_Code else '' end as State_Code,case when slno=1 then STATE_NAME  else '' end as STATE_NAME,case when slno=1 then MCC_CODE   else '' end as MCC_CODE,case when slno=1 then MCC_NAME    else '' end as MCC_NAME,Tanker_No ,case when slno=1 then DOC_DATE     else '' end as DOC_DATE, (case when slno=1 then Opening      else null end) as Opening  ,case when slno=1 then milk_proc       else null end as milk_proc,case when slno=1 then milk_pro_Inter       else null end as milk_pro_Inter"
        sQuery += " ,Dis_qty,Dis_qty_Inter ,Tanker_Dispatch_To,Tanker_Dispatch_Time ,(case when slno=1 then closing       else null end) as closing ,Reached_Date_time ,isIntermittent from FinalWholeData "


        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gvReport.DataSource = Nothing
            gvReport.Rows.Clear()
            gvReport.Columns.Clear()
            gvReport.DataSource = dtgv
            'For i As Integer = 0 To gvReport.Rows.Count - 1
            '    gvReport.Rows(i).Cells(0).Value = i + 1
            'Next
            'gvReport.GroupDescriptors.Clear()
            gvReport.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            gvReport.BestFitColumns()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
            ReStoreGridLayout()
        Catch err As Exception
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()
        
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next

        'gvReport.Columns("SNO").IsVisible = True
        'gvReport.Columns("SNO").Width = 30
        'gvReport.Columns("SNO").ReadOnly = False
        'gvReport.Columns("SNO").HeaderText = "SNO"

        gvReport.Columns("State_Code").IsVisible = False
        gvReport.Columns("State_Code").Width = 100
        gvReport.Columns("State_Code").HeaderText = "State_Code"


        gvReport.Columns("STATE_NAME").IsVisible = True
        gvReport.Columns("STATE_NAME").Width = 100
        gvReport.Columns("STATE_NAME").HeaderText = " State Code"

        gvReport.Columns("MCC_CODE").IsVisible = False
        gvReport.Columns("MCC_CODE").Width = 100
        gvReport.Columns("MCC_CODE").HeaderText = " Mcc Code"


        gvReport.Columns("MCC_NAME").IsVisible = True
        gvReport.Columns("MCC_NAME").Width = 100
        gvReport.Columns("MCC_NAME").HeaderText = "Mcc Name"

        gvReport.Columns("Opening").IsVisible = True
        gvReport.Columns("Opening").Width = 100
        gvReport.Columns("Opening").HeaderText = "Op. Balance"

        gvReport.Columns("Closing").IsVisible = True
        gvReport.Columns("Closing").Width = 100
        gvReport.Columns("Closing").HeaderText = "Clo. Balance"

        gvReport.Columns("milk_proc").IsVisible = True
        gvReport.Columns("milk_proc").Width = 80
        gvReport.Columns("milk_proc").HeaderText = "milk proc."

        gvReport.Columns("milk_pro_Inter").IsVisible = True
        gvReport.Columns("milk_pro_Inter").Width = 80
        gvReport.Columns("milk_pro_Inter").HeaderText = "milk proc Intermittent"

        gvReport.Columns("Dis_qty").IsVisible = True
        gvReport.Columns("Dis_qty").Width = 80
        gvReport.Columns("Dis_qty").HeaderText = "Dispatch Qty"

        gvReport.Columns("Dis_qty_Inter").IsVisible = True
        gvReport.Columns("Dis_qty_Inter").Width = 80
        gvReport.Columns("Dis_qty_Inter").HeaderText = "Dispatch Qty Intermittent"

        gvReport.Columns("Tanker_Dispatch_To").IsVisible = True
        gvReport.Columns("Tanker_Dispatch_To").Width = 80
        gvReport.Columns("Tanker_Dispatch_To").HeaderText = "Dispatch To"

        gvReport.Columns("Tanker_Dispatch_Time").IsVisible = True
        gvReport.Columns("Tanker_Dispatch_Time").Width = 80
        gvReport.Columns("Tanker_Dispatch_Time").HeaderText = "Dispatch Time"

        '

        gvReport.Columns("Tanker_No").IsVisible = True
        gvReport.Columns("Tanker_No").Width = 80
        gvReport.Columns("Tanker_No").HeaderText = "Tanker No"

        gvReport.Columns("Reached_Date_time").IsVisible = True
        gvReport.Columns("Reached_Date_time").Width = 80
        gvReport.Columns("Reached_Date_time").HeaderText = "Reached Date"


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Opening", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("milk_proc", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("Dis_qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        'gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_CODE as Item format ""{0}: {1}"" Group By VLC_CODE"))

        gvReport.ShowGroupPanel = False
        gvReport.MasterTemplate.AutoExpandGroups = True

        gvReport.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvReport.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvReport.Columns.Count - 1 Step ii + 1
                        gvReport.Columns(ii).IsVisible = False
                        gvReport.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvReport.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RptDispatchofmilkTransfer2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")

        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtDate.Value = clsCommon.GETSERVERDATE()

        Reset()

    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
