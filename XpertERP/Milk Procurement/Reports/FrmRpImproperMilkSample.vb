Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class FrmRpImproperMilkSample
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
#End Region

    Private Sub FrmMCCMilkRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadShift()
        Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
    End Sub

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            End If
        End If
        btnExport.Visible = MyBase.isExport
    End Sub

    Sub LoadShift()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        cboFromShift.DataSource = dt.Copy()
        cboFromShift.ValueMember = "Code"
        cboFromShift.DisplayMember = "Name"

        cboToShift.DataSource = dt.Copy()
        cboToShift.ValueMember = "Code"
        cboToShift.DisplayMember = "Name"
    End Sub

    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = "MRIW"
        Return ReportID
    End Function

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = GetReportID()
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub FrmMCCMilkRegister_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = GetReportID()
        TemplateGridview = gv
        LoadData()
    End Sub

    Private Sub LoadData(Optional ByVal BulkExport As Integer = 0)
        Try
            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            Dim FinalQuery As String = Nothing
            Dim qry As String = Nothing

            qry = "select TSPL_MILK_SAMPLE_HEAD.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MILK_SAMPLE_DETAIL.VSP_CODE,TSPL_VENDOR_MASTER.Vendor_Name,x.DocType,x.DOC_CODE,convert(varchar,TSPL_MILK_SAMPLE_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_SAMPLE_HEAD.DOCK_Code,TSPL_MILK_SAMPLE_HEAD.SHIFT,x.SAMPLE_NO,x.FAT,x.SNF,case when DocType='Transaction' then TSPL_MILK_SAMPLE_HEAD.Created_By else x.Created_By end as Created_By from (" + Environment.NewLine + _
            "select 'Transaction' as DocType, TSPL_MILK_SAMPLE_DETAIL.DOC_CODE,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO,TSPL_MILK_SAMPLE_DETAIL.FAT,TSPL_MILK_SAMPLE_DETAIL.SNF,null as Created_By from TSPL_MILK_SAMPLE_DETAIL " + Environment.NewLine + _
            "union  all" + Environment.NewLine + _
            "select 'Log' as DocType,Sample_Code as DOC_CODE,Sample_No,FAT,SNF,TSPL_MILK_SAMPLE_READING_LOG.Created_By  from TSPL_MILK_SAMPLE_READING_LOG   " + Environment.NewLine + _
            "where 2=2 " + Environment.NewLine + _
            "and not exists(select 1 from TSPL_MILK_SAMPLE_DETAIL where TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_READING_LOG.Sample_Code and TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO=TSPL_MILK_SAMPLE_READING_LOG.Sample_No and TSPL_MILK_SAMPLE_DETAIL.FAT=TSPL_MILK_SAMPLE_READING_LOG.FAT and TSPL_MILK_SAMPLE_DETAIL.SNF=TSPL_MILK_SAMPLE_READING_LOG.SNF )" + Environment.NewLine + _
            ")x " + Environment.NewLine + _
            "left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=x.DOC_CODE  " + Environment.NewLine + _
            "left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=x.DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO=x.SAMPLE_NO " + Environment.NewLine + _
            "left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO=x.SAMPLE_NO " + Environment.NewLine + _
            "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_SAMPLE_HEAD.MCC_CODE" + Environment.NewLine + _
            "left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE" + Environment.NewLine + _
            "left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE" + Environment.NewLine + _
            "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MILK_SAMPLE_DETAIL.VSP_CODE" + Environment.NewLine + _
            "where 2=2 "
            qry += " and  TSPL_MILK_SAMPLE_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_SAMPLE_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
            If clsCommon.CompairString(clsCommon.myCstr(cboFromShift.SelectedValue), "E") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_MILK_SAMPLE_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SAMPLE_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_SAMPLE_HEAD.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cboToShift.SelectedValue), "M") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_MILK_SAMPLE_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SAMPLE_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_SAMPLE_HEAD.SHIFT='E' then 3 else 2 end  )"
            End If
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "and TSPL_MILK_SAMPLE_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            End If
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
            End If
            qry += " order by TSPL_MILK_SAMPLE_HEAD.DOC_DATE,x.DOC_CODE,x.SAMPLE_NO,x.DocType desc"
             

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Data Found to Display")
                Exit Sub
                    End If

            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
            EnableDisableControl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        txtMCC.Enabled = val
    End Sub

    Sub FormatGrid()
        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            'gv.Columns(ii).FormatString = "{0:n2}"
        Next

        gv.Columns("MCC_CODE").IsVisible = True
        gv.Columns("MCC_CODE").Width = 80
        gv.Columns("MCC_CODE").HeaderText = "MCC Code"

        gv.Columns("MCC_NAME").IsVisible = True
        gv.Columns("MCC_NAME").Width = 100
        gv.Columns("MCC_NAME").HeaderText = "MCC"

        gv.Columns("ROUTE_CODE").IsVisible = True
        gv.Columns("ROUTE_CODE").Width = 100
        gv.Columns("ROUTE_CODE").HeaderText = "Route Code"

        gv.Columns("Route_Name").IsVisible = True
        gv.Columns("Route_Name").Width = 100
        gv.Columns("Route_Name").HeaderText = "Route"


        gv.Columns("VLC_CODE").IsVisible = True
        gv.Columns("VLC_CODE").Width = 100
        gv.Columns("VLC_CODE").HeaderText = "VLC Code"

        gv.Columns("VLC_Name").IsVisible = True
        gv.Columns("VLC_Name").Width = 100
        gv.Columns("VLC_Name").HeaderText = "VLC"

        gv.Columns("VSP_CODE").IsVisible = True
        gv.Columns("VSP_CODE").Width = 80
        gv.Columns("VSP_CODE").HeaderText = "VSP Code"

        gv.Columns("Vendor_Name").IsVisible = True
        gv.Columns("Vendor_Name").Width = 100
        gv.Columns("Vendor_Name").HeaderText = "VSP"

        gv.Columns("DocType").IsVisible = True
        gv.Columns("DocType").Width = 80
        gv.Columns("DocType").HeaderText = "Document Type"

        gv.Columns("DOC_CODE").IsVisible = True
        gv.Columns("DOC_CODE").Width = 100
        gv.Columns("DOC_CODE").HeaderText = "Document No"

        gv.Columns("DOC_DATE").IsVisible = True
        gv.Columns("DOC_DATE").Width = 80
        gv.Columns("DOC_DATE").HeaderText = "Document Date"

        gv.Columns("DOCK_Code").IsVisible = False
        gv.Columns("DOCK_Code").HeaderText = "DOCK Code"

        gv.Columns("SHIFT").IsVisible = False
        gv.Columns("SHIFT").Width = 50
        gv.Columns("SHIFT").HeaderText = "Shift"

        gv.Columns("SAMPLE_NO").IsVisible = True
        gv.Columns("SAMPLE_NO").Width = 50
        gv.Columns("SAMPLE_NO").HeaderText = "Sample No"

        gv.Columns("FAT").IsVisible = True
        gv.Columns("FAT").Width = 50
        gv.Columns("FAT").HeaderText = " FAT"

        gv.Columns("SNF").IsVisible = True
        gv.Columns("SNF").Width = 50
        gv.Columns("SNF").HeaderText = "SNF"

        gv.Columns("Created_By").IsVisible = True
        gv.Columns("Created_By").Width = 80
        gv.Columns("Created_By").HeaderText = "User"

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0
        'Dim item1 As New GridViewSummaryItem("Min_Weight_Value", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("MILK_WEIGHT", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Dim ReportID As String = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
            arrHeader.Add(CompName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptMilkReceiptImproperWeight & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrValueMember) + " "))
            End If
            'If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            '    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrValueMember) + " "))
            'End If
            'If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            '    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrValueMember) + " "))
            'End If
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
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)

            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, Nothing)
        RefreshRoute()
        RefreshVLC()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where 2=2 "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, Nothing)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1' "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If

            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub RefreshRoute()
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtRoute.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("Route_Code")))
                Next
                txtRoute.arrValueMember = arr
            End If
        End If
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

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
