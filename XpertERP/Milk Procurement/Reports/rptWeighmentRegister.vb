Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class RptWeighmentRegister
    Inherits FrmMainTranScreen

    Dim ds As New DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = " select MCC_Code as Code , MCC_NAME as Name from TSPL_MCC_MASTER "
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("RPTMCC", qry, "Code", "Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptWeighmentRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isQuickExportFlag
    End Sub

    Private Sub RptWeighmentRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub
    Sub Reset()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        txtMCC.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        gv.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub
    Sub LoadData(Optional ByVal Print As Integer = 0)
        Try
            Dim qry As String = ""
            If clsCommon.GetDateWithStartTime(dtpFromdate1.Value) > clsCommon.GetDateWithEndTime(dtpToDate.Value) Then
                dtpFromdate1.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            Dim fromdate As String = ""
            Dim Todate As String = ""
            Dim strMcc As String = ""
            Dim strRoute As String = ""
            Dim whr As String = " and 2=2 "

            fromdate = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Todate = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")

            gv.MasterTemplate.SummaryRowsBottom.Clear()
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                strMcc = clsCommon.GetMulcallString(txtMCC.arrValueMember)
            End If
            If clsCommon.myLen(strMcc) > 0 Then
                whr = whr + " and TSPL_MILK_RECEIPT_HEAD.MCC_CODE  in (" + strMcc + " )"
            End If

            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                strRoute = clsCommon.GetMulcallString(txtRoute.arrValueMember)
            End If
            If clsCommon.myLen(strRoute) > 0 Then
                whr = whr + " and TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE in (" + strRoute + " )"
            End If

            qry = " select TSPL_MILK_RECEIPT_DETAIL.DOC_CODE,FORMAT ( convert (date,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103),'dd/MM/yyyy') as DOC_DATE ,TSPL_MILK_RECEIPT_HEAD.MCC_CODE,max(TSPL_MCC_MASTER.MCC_NAME) as MCC_NAME,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE,max(TSPL_MCC_ROUTE_MASTER.Route_Name) as Route_Name,max(TSPL_MILK_RECEIPT_DETAIL.SHIFT) as SHIFT,sum(TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS) as NO_OF_CANS ,sum(TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT) as MILK_WEIGHT,max(CAST( (convert (time,TSPL_MILK_GATE_ENTRY_IN.Entry_Date,109)) AS TIME(0))) as  Arrival_Time,CAST( min(convert (time,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,109)) AS TIME(0)) 'Challan_Received_At',CAST( min(convert (time,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,109)) AS TIME(0)) 'Dumping_Start_Time',CAST(max(convert (time,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,109)) AS TIME(0)) 'Dumping_Finish_Time', CAST((max(TSPL_MILK_RECEIPT_DETAIL.DOC_DATE)-min(TSPL_MILK_RECEIPT_DETAIL.DOC_DATE)) as time(0)) 'Net_Dumping_Time'   from TSPL_MILK_RECEIPT_DETAIL left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE  " & _
                  " left outer join TSPL_MILK_GATE_ENTRY_IN on convert (varchar,TSPL_MILK_GATE_ENTRY_IN.Shift_Date,103)=convert (varchar,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103) and TSPL_MILK_GATE_ENTRY_IN.Route_Code=TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE  and TSPL_MILK_GATE_ENTRY_IN.Entry_Shift=TSPL_MILK_RECEIPT_DETAIL.SHIFT " & _
                  " where convert (date ,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103) >=convert(date,'" + fromdate + "',103) and convert (date ,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103) <=Convert(Date,'" + Todate + "',103) and TSPL_MILK_RECEIPT_HEAD.Posted = 1 and TSPL_MILK_GATE_ENTRY_IN.Status=1 " + whr + " " & _
                  " group by TSPL_MILK_RECEIPT_DETAIL.DOC_CODE ,TSPL_MILK_RECEIPT_HEAD.MCC_CODE,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE ,convert (date,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103)  order by TSPL_MILK_RECEIPT_HEAD.MCC_CODE "
            Dim dtgv As New DataTable
            If Print = 1 Then
                dtgv = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, clsERPFuncationality.CompanyAddresShowinHeader(), "rptVLCWeighmentRegister", "VLC WEIGHMENT REGISTER", "Address.rpt")
                frmCRV = Nothing
                Exit Sub
            End If
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.BestFitColumns()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
            If dtgv.Rows.Count <= 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try

            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = "Company : " & objCommonVar.CurrentCompanyName ' 
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptWeighmentRegister & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromdate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add("MCC :" + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add("Route :" + clsCommon.GetMulcallStringWithComma(txtRoute.arrValueMember))
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
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Weighment Register Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where 2=2 "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        LoadData(1)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub
End Class
