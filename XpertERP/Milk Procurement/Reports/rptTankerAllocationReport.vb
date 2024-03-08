Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class rptTankerAllocationReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        Reset()
    End Sub
    Sub Reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        txtTanker.arrValueMember = Nothing
        txtTransporter.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        TemplateGridview = Gv1
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            Dim dt1 As New DataTable
            Dim qry As String = ""
            qry = "select convert (varchar,ROW_NUMBER() OVER(ORDER BY Convert(datetime,TSPL_MCC_TANKER_GATE_OUT.gate_out_date,113), gate_out_no ASC))as SNo,  gate_out_no as [Document Code],Convert(varchar,TSPL_MCC_TANKER_GATE_OUT.gate_out_date,113) as [Document Date],TSPL_MCC_TANKER_GATE_OUT.tanker_no as [Tanker No] " &
                " ,TSPL_MCC_TANKER_GATE_OUT.mcc_code + case when isnull(TSPL_MCC_TANKER_GATE_OUT.mcc_code2,'')<>'' then ','+isnull(TSPL_MCC_TANKER_GATE_OUT.mcc_code2,'') else '' end + case when isnull(TSPL_MCC_TANKER_GATE_OUT.mcc_code3,'')<>'' then ','+isnull(TSPL_MCC_TANKER_GATE_OUT.mcc_code3,'') else '' end as [Allocated To Code] " &
                " ,coalesce(tspl_mcc_master.mcc_name,tspl_location_master.location_desc) + case when coalesce(TMM2.mcc_name,TLM2.location_desc)<>'' then ','+coalesce(TMM2.mcc_name,TLM2.location_desc) else '' end + case when coalesce(TMM3.mcc_name,TLM3.location_desc)<>'' then ','+coalesce(TMM3.mcc_name,TLM3.location_desc) else '' end as [Allocated To Name]" &
                " ,case when is_posted=1 then 'Yes' else 'No' end [Is Posted],case when IsCancel =1 then 'Cancel' else '' end [Cancel Status],TSPL_MCC_TANKER_GATE_OUT.Storage_Capacity as [Tanker Capacity],tspl_tanker_master.Tanker_Transporter_Code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],(CASE WHEN ISNULL(tspl_mcc_dispatch_challan.Chalan_NO,'') ='' THEN 'No' else 'Yes' end) as [Used In Tanker Dispatch] " &
                " ,convert(varchar,tspl_mcc_dispatch_challan.Dispatch_Date,113) as [Tanker Dispatch from Unit],convert(varchar,tspl_gate_entry_details.Date_And_Time,113) as [Tanker Received at MPF] " &
                " from TSPL_MCC_TANKER_GATE_OUT " &
                " Left Join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_MCC_TANKER_GATE_OUT.mcc_code " &
                " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_TANKER_GATE_OUT.mcc_code " &
                " Left Join tspl_mcc_master TMM2 on TMM2.mcc_code=TSPL_MCC_TANKER_GATE_OUT.MCC_CODE2 left join tspl_location_master TLM2 on TLM2.location_code=TSPL_MCC_TANKER_GATE_OUT.MCC_CODE2 " &
                " Left Join tspl_mcc_master TMM3 on TMM3.mcc_code=TSPL_MCC_TANKER_GATE_OUT.MCC_CODE3 left join tspl_location_master TLM3 on TLM3.location_code=TSPL_MCC_TANKER_GATE_OUT.MCC_CODE3 " &
                " left outer join  tspl_tanker_master on tspl_tanker_master.Tanker_No=TSPL_MCC_TANKER_GATE_OUT.tanker_no left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=tspl_tanker_master.Tanker_Transporter_Code and tspl_vendor_master.form_type='TTM' " &
                " left outer join tspl_mcc_dispatch_challan on tspl_mcc_dispatch_challan.Against_Gate_Out=TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO " &
                " left outer join tspl_gate_entry_details on tspl_gate_entry_details.challan_no= tspl_mcc_dispatch_challan.chalan_no " &
                " where  CONVERT(date,TSPL_MCC_TANKER_GATE_OUT.gate_out_date,103) >= convert(date,'" + fromDate.Value + "',103) AND  " &
                " CONVERT(date,TSPL_MCC_TANKER_GATE_OUT.gate_out_date,103) <= convert(date,'" + ToDate.Value + "',103) "

            If txtTanker.arrValueMember IsNot Nothing AndAlso txtTanker.arrValueMember.Count > 0 Then
                qry += " and TSPL_MCC_TANKER_GATE_OUT.tanker_no in(" + clsCommon.GetMulcallString(txtTanker.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                qry += " and tspl_tanker_master.Tanker_Transporter_Code in(" + clsCommon.GetMulcallString(txtTransporter.arrValueMember) + ")" + Environment.NewLine
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

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                btnGo.Enabled = False
                SetGridFormat()
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.AutoSizeRows = True
        Gv1.EnableFiltering = True

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.BestFitColumns()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
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



    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsCommon.myCstr("Tanker Allocation Report"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            If txtTanker.arrDispalyMember IsNot Nothing AndAlso txtTanker.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Tanker : " + clsCommon.GetMulcallStringWithComma(txtTanker.arrDispalyMember))
            End If
            If txtTransporter.arrDispalyMember IsNot Nothing AndAlso txtTransporter.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Transporter : " + clsCommon.GetMulcallStringWithComma(txtTransporter.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Tanker Allocation Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Tanker Allocation Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub TxtTanker__My_Click(sender As Object, e As EventArgs) Handles txtTanker._My_Click
        Dim qry As String = " select TSPL_TANKER_MASTER.Tanker_No as [Tanker No] from TSPL_TANKER_MASTER "
        txtTanker.arrValueMember = clsCommon.ShowMultipleSelectForm("TankerMulSel1", qry, "Tanker No", "Tanker No", txtTanker.arrValueMember, txtTanker.arrDispalyMember)
    End Sub

    Private Sub TxtTransporter__My_Click(sender As Object, e As EventArgs) Handles txtTransporter._My_Click
        Dim qry As String = " select tspl_vendor_master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name] from tspl_vendor_master where TSPL_VENDOR_MASTER.Form_Type='TTM' "
        txtTransporter.arrValueMember = clsCommon.ShowMultipleSelectForm("TansporterSel1", qry, "Transporter Code", "Transporter Name", txtTransporter.arrValueMember, txtTransporter.arrDispalyMember)
    End Sub
End Class
