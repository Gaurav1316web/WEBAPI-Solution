'' added by Richa Agarwal 16 Jan,2019  Against Ticket No. UDL/14/01/19-000254 
Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class rptCwipReport

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrCapex As ArrayList
    Public arrSubCapex As ArrayList
    Dim arrBack As List(Of String)
   
#End Region

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnBulkExp.Visible = MyBase.isExport
    End Sub

   
    Public Function getquery() As String
        Dim capexarr As ArrayList = txtcapex.arrValueMember
        Dim subcapexarr As ArrayList = txtsubcapex.arrValueMember
      

       
        Dim qry As String
        qry = "select ROW_NUMBER () over(order by TSPL_CAPEX_BUDGET_MASTER.capex_code) as [SL. NO.],TSPL_CAPEX_MASTER.DESCRIPTION as [CAPEX NAME ],TSPL_CAPEX_BUDGET_MASTER.Capex_Code as [CAPEX CODE] ," & Environment.NewLine & _
        " TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION as [SUB CAPEX NAME],TSPL_CAPEX_BUDGET_MASTER.CODE as [SUB CAPEX CODE],TSPL_ACQUISITION_DETAIL.Acquisition_Code as [ACQUISITION ENTRY NO.],convert(varchar,TSPL_ACQUISITION_HEAD.Acquisition_Date,103) as [ACQUISITION ENTRY DATE],isnull(finalQuery.DocumentAmountPI,0) as [AMOUNT RECD. FROM PURCHASE] , " & Environment.NewLine & _
        " isnull(finalQuery.DocumentAmountSIR,0) as [AMOUNT RECD. FROM STORE] ,isnull(finalQuery.DocumentAmountAW,0) as [AMOUNT RECD. FROM DIRECT BILLS] ,isnull(finalQuery.Total ,0) as [TOTAL CWIP AMOUNT]  " & Environment.NewLine & _
        " from TSPL_CAPEX_BUDGET_MASTER left outer join " & Environment.NewLine & _
        " (select  max(CapexQuery.Capex_Code) as Capex_Code,CapexQuery.Capex_SubCode,sum(CapexQuery.DocumentAmountPI) as DocumentAmountPI,sum(CapexQuery.DocumentAmountSIR) as  DocumentAmountSIR,sum(CapexQuery.DocumentAmountAW) as  DocumentAmountAW ,(sum(CapexQuery.DocumentAmountPI) +sum(CapexQuery.DocumentAmountSIR) + sum(CapexQuery.DocumentAmountAW)) as Total from (" & Environment.NewLine & _
        " -------------------- purchase invoice---------------------" & Environment.NewLine & _
        " select 'PI' as TransType, max(TSPL_PI_DETAIL.Capex_Code) as Capex_Code,TSPL_PI_DETAIL.Capex_SubCode,sum(TSPL_PI_DETAIL.Item_Net_Amt) as DocumentAmountPI,0 as  DocumentAmountSIR,0 as  DocumentAmountAW from TSPL_PI_DETAIL" & Environment.NewLine & _
        " left outer join TSPL_PI_HEAD on TSPL_PI_DETAIL.PI_No =TSPL_PI_HEAD.PI_No " & Environment.NewLine & _
        " where isnull(TSPL_PI_DETAIL.Capex_SubCode,'')<>'' and TSPL_PI_HEAD.Status =1 " & Environment.NewLine & _
        " group by TSPL_PI_DETAIL.Capex_SubCode" & Environment.NewLine & _
        " Union all " & Environment.NewLine & _
        " -------------- Store issue/ return --- transfer to Capex------------" & Environment.NewLine & _
        " select 'SIR' as TransType, max(TSPL_IssueReturn_HEAD.Capex_Code) as Capex_Code,TSPL_IssueReturn_HEAD.Capex_SubCode,0  as DocumentAmountPI,sum (TSPL_IssueReturn_HEAD.Doc_Amt )as DocumentAmountSIR,0 as  DocumentAmountAW " & Environment.NewLine & _
        "  from TSPL_IssueReturn_HEAD where Doc_Type ='TransferCX' and Status =1 and isnull(Capex_SubCode,'')<>'' group by Capex_SubCode" & Environment.NewLine & _
        " ----------------- Assets Expense Entry-------------" & Environment.NewLine & _
        " Union all " & Environment.NewLine & _
        " select 'AW' as TransType, max(TSPL_ASSET_WORK_HEAD.Capex_Code) as Capex_Code,TSPL_ASSET_WORK_HEAD.Capex_SubCode,0  as DocumentAmountPI,0 as DocumentAmountSIR," & Environment.NewLine & _
        " sum (TSPL_ASSET_WORK_HEAD.Net_amt )as DocumentAmountAW from TSPL_ASSET_WORK_HEAD where  Status =1 and isnull(Capex_SubCode,'')<>'' group by Capex_SubCode " & Environment.NewLine & _
        " )CapexQuery " & Environment.NewLine & _
        " group  by CapexQuery.Capex_SubCode) finalQuery" & Environment.NewLine & _
        " on TSPL_CAPEX_BUDGET_MASTER.CODE =finalQuery.Capex_SubCode " & Environment.NewLine & _
        " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL .Capex_SubCode =TSPL_CAPEX_BUDGET_MASTER.CODE " & Environment.NewLine & _
        " left outer join TSPL_ACQUISITION_HEAD  on TSPL_ACQUISITION_DETAIL .Acquisition_Code =TSPL_ACQUISITION_HEAD.Acquisition_Code " & Environment.NewLine & _
        " left outer join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER.CODE = TSPL_CAPEX_BUDGET_MASTER.Capex_Code " & Environment.NewLine & _
        " where 1=1 "

        If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
            qry += " AND TSPL_CAPEX_BUDGET_MASTER.Capex_Code IN (" + clsCommon.GetMulcallString(capexarr) + ")"
        End If
        If subcapexarr IsNot Nothing AndAlso subcapexarr.Count > 0 Then
            qry += " AND TSPL_CAPEX_BUDGET_MASTER.CODE IN (" + clsCommon.GetMulcallString(subcapexarr) + ")"
        End If

        Return qry

    End Function

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""

            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
          

            clsCommon.ProgressBarShow()

            txtcapex.Enabled = False
            txtsubcapex.Enabled = False
          
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            strRunQuery = getquery()

            If BulkExport = 1 Then
                transportSql.BulkExport("CWIP Report", strRunQuery, "", "csv")
            ElseIf BulkExport = 2 Then
                transportSql.BulkExport("CWIP Report", strRunQuery, "", "xls")
            End If

            RadPageViewPage2.Text = "Report"
            dt = clsDBFuncationality.GetDataTable(strRunQuery)
            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)

                Exit Sub
            Else
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If
            'FindAndRestoreGridLayout(Me)
            Gv1.MasterTemplate.AllowAddNewRow = False
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2


        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
        Next
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub

    Sub Reset()
        txtcapex.arrValueMember = Nothing
        txtsubcapex.arrValueMember = Nothing
        txtcapex.Enabled = True
        txtsubcapex.Enabled = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadPageViewPage2.Text = "Report"
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

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = ReportId()
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Function ReportId()
        Dim Report_Id As String = ""
        Report_Id = MyBase.Form_ID
        Return Report_Id
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptPurchaseRegisterReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub RptPurchaseRegisterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False
        arrBack = New List(Of String)
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        Gv1.EnableGrouping = True
        Gv1.ShowGroupPanel = True
        
    End Sub

    

    Private Sub txtCapex_My_Click(sender As Object, e As EventArgs) Handles txtcapex._My_Click
        Dim qry As String = "select code as code,description as name from tspl_capex_master"
        txtcapex.arrValueMember = clsCommon.ShowMultipleSelectForm("capexPur", qry, "code", "Name", txtcapex.arrValueMember, txtcapex.arrDispalyMember)
        txtsubcapex.arrValueMember = Nothing
    End Sub

    Private Sub txtsubCapex_My_Click(sender As Object, e As EventArgs) Handles txtsubcapex._My_Click
        Dim qry As String = "select code as code,description as name from TSPL_CAPEX_BUDGET_MASTER "
        If txtcapex.arrValueMember IsNot Nothing AndAlso txtcapex.arrValueMember.Count > 0 Then
            qry += " where Capex_code IN (" + clsCommon.GetMulcallString(txtcapex.arrValueMember) + ")"
        End If
        txtsubcapex.arrValueMember = clsCommon.ShowMultipleSelectForm("subcapexPur", qry, "code", "Name", txtsubcapex.arrValueMember, txtsubcapex.arrDispalyMember)
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles Gv1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
        End If
    End Sub

   
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCWIPReport & "'"))
           

            If exporter = EnumExportTo.Excel Then

                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 2007 (*.xlsx) |*.xlsx;|Excel 97-2003 (*.xls)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        Print(Exporter.Refresh, 1)
    End Sub

    Private Sub BulkExportXls_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        Print(Exporter.Refresh, 2)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Export(Exporter.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Export(Exporter.PDF)
    End Sub

End Class


