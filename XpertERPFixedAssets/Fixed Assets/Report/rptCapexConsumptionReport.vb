'' added by Richa Agarwal 30 Jan,2019  Against Ticket No. UDL/30/01/19-000264 (Capex Consumption Report)
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

Public Class rptCapexConsumptionReport

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
        Dim Itemarr As ArrayList = txtItem.arrValueMember

       
        'Dim qry As String = " SELECT ROW_NUMBER () over(order by FINALQUERY.capex_code) as [SL. NO.],FINALQUERY.[CAPEX NAME],FINALQUERY.Capex_Code AS [CAPEX CODE],FINALQUERY.CAPEX_CURRENT_BUDGET AS [CAPEX BUDGET],FINALQUERY.[SUB CAPEX NAME],FINALQUERY.[SUB CAPEX CODE],FINALQUERY.SUB_CAPEX_CURRENT_BUDGET AS [SUB CAPEX BUDGET],FINALQUERY.Item_Code [Item Code],FINALQUERY.ITEM_dESC AS [ITEM DESC]," & Environment.NewLine & _
        '" FINALQUERY.[ACQUISITION ENTRY NO.],FINALQUERY.[ACQUISITION ENTRY DATE],FINALQUERY.[ACQUISITION AMOUNT],FINALQUERY.[ASSETS STORE REQUESITION],FINALQUERY.[ASSETS STORE REQUESITION DATE],FINALQUERY.[STORE ISSUE DOCT. NO.],FINALQUERY.[STORE ISSUE DOCT. DATE], [STORE ISSUE DOC AMT]   " & Environment.NewLine & _
        '" FROM (  " & Environment.NewLine & _
        '" select TSPL_CAPEX_MASTER.DESCRIPTION as [CAPEX NAME ], TSPL_CAPEX_BUDGET_MASTER.Capex_Code ,ISNULL(TSPL_CAPEX_MASTER.CURRENT_BUDGET,0) CAPEX_CURRENT_BUDGET,TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION as [SUB CAPEX NAME],TSPL_CAPEX_BUDGET_MASTER.CODE AS [SUB CAPEX CODE],ISNULL(TSPL_CAPEX_BUDGET_MASTER.CURRENT_BUDGET,0) SUB_CAPEX_CURRENT_BUDGET,TSPL_IssueItemToAssembledAsset_DETAIL.Item_Code, TSPL_ITEM_MASTER.ITEM_dESC, " & Environment.NewLine & _
        '" TSPL_ACQUISITION_HEAD.Acquisition_Code as [ACQUISITION ENTRY NO.],convert(varchar,TSPL_ACQUISITION_HEAD.Acquisition_Date,103) as [ACQUISITION ENTRY DATE], " & Environment.NewLine & _
        '" ISNULL(TSPL_ACQUISITION_HEAD.Net_Amt ,0) AS [ACQUISITION AMOUNT],TSPL_IssueItemToAssembledAsset_DETAIL.Req_IssueNo  AS [ASSETS STORE REQUESITION],case when isnull(TSPL_IssueItemToAssembledAsset_DETAIL.Req_IssueNo,'')<>'' then convert(varchar,TSPL_ASSET_STORE_REQUISITION.Requisition_Date,103) end  AS [ASSETS STORE REQUESITION DATE] " & Environment.NewLine & _
        '" ,TSPL_IssueItemToAssembledAsset_HEAD.DOC_NO AS [STORE ISSUE DOCT. NO.],convert(varchar,TSPL_IssueItemToAssembledAsset_HEAD.Doc_Date,103) AS [STORE ISSUE DOCT. DATE],TSPL_IssueItemToAssembledAsset_HEAD.Doc_Amt as [STORE ISSUE DOC AMT]   " & Environment.NewLine & _
        '" from TSPL_CAPEX_BUDGET_MASTER  " & Environment.NewLine & _
        '" left outer join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER.CODE = TSPL_CAPEX_BUDGET_MASTER.Capex_Code  " & Environment.NewLine & _
        '" left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL .Capex_SubCode =TSPL_CAPEX_BUDGET_MASTER.CODE   " & Environment.NewLine & _
        '" left outer join TSPL_ACQUISITION_HEAD  on TSPL_ACQUISITION_DETAIL .Acquisition_Code =TSPL_ACQUISITION_HEAD.Acquisition_Code  " & Environment.NewLine & _
        '" LEFT OUTER JOIN TSPL_REQUISITION_HEAD AS TSPL_ASSET_STORE_REQUISITION ON TSPL_ASSET_STORE_REQUISITION .Capex_SubCode =TSPL_CAPEX_BUDGET_MASTER .CODE AND  TSPL_ASSET_STORE_REQUISITION.Is_Internal='Y' and TSPL_ASSET_STORE_REQUISITION.Requisition_Type<>'' AND TSPL_ASSET_STORE_REQUISITION.STATUS=1  and isnull(TSPL_ASSET_STORE_REQUISITION.Requisition_Id,'')<>''" & Environment.NewLine & _
        '" LEFT OUTER JOIN TSPL_REQUISITION_detail AS TSPL_ASSET_STORE_REQUISITION_DETAIL ON  TSPL_ASSET_STORE_REQUISITION_DETAIL.Requisition_Id   =TSPL_ASSET_STORE_REQUISITION.Requisition_Id" & Environment.NewLine & _
        '" LEFT OUTER JOIN TSPL_IssueItemToAssembledAsset_DETAIL ON TSPL_IssueItemToAssembledAsset_DETAIL.Capex_SubCode= TSPL_ASSET_STORE_REQUISITION .Capex_SubCode   " & Environment.NewLine & _
        '" and (TSPL_IssueItemToAssembledAsset_DETAIL.Req_IssueNo =TSPL_ASSET_STORE_REQUISITION.Requisition_Id  or isnull(TSPL_IssueItemToAssembledAsset_DETAIL.Req_IssueNo,'')='' ) " & Environment.NewLine & _
        '" and TSPL_ASSET_STORE_REQUISITION_DETAIL.Item_Code =TSPL_IssueItemToAssembledAsset_DETAIL.Item_Code " & Environment.NewLine & _
        '" LEFT OUTER JOIN TSPL_IssueItemToAssembledAsset_Head ON TSPL_IssueItemToAssembledAsset_DETAIL.Doc_No =TSPL_IssueItemToAssembledAsset_Head.Doc_No AND TSPL_IssueItemToAssembledAsset_HEAD.STATUS=1  " & Environment.NewLine & _
        '" LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_ASSET_STORE_REQUISITION_DETAIL.Item_Code  " & Environment.NewLine & _
        '" ) FINALQUERY " & Environment.NewLine & _
        '" where 1=1  "
        ''richa show (ISNULL(TSPL_CAPEX_MASTER.Current_Budget,0) +ISNULL(TSPL_CAPEX_MASTER.Inc_Budget ,0) ) as instead of Current budget of capex 16 Apr,2019 UDL/16/04/19-000289
        Dim qry As String = " SELECT ROW_NUMBER () over(order by FINALQUERY.capex_code) as [SL. NO.],FINALQUERY.[CAPEX NAME],FINALQUERY.Capex_Code AS [CAPEX CODE],FINALQUERY.CAPEX_CURRENT_BUDGET AS [CAPEX BUDGET],FINALQUERY.[SUB CAPEX NAME],FINALQUERY.[SUB CAPEX CODE],FINALQUERY.SUB_CAPEX_CURRENT_BUDGET AS [SUB CAPEX BUDGET],FINALQUERY.Item_Code [Item Code],FINALQUERY.ITEM_dESC AS [ITEM DESC], FINALQUERY.[ACQUISITION ENTRY NO.],FINALQUERY.[ACQUISITION ENTRY DATE],FINALQUERY.[ACQUISITION AMOUNT],FINALQUERY.[ASSETS STORE REQUESITION],FINALQUERY.[ASSETS STORE REQUESITION DATE],FINALQUERY.[STORE ISSUE DOCT. NO.],FINALQUERY.[STORE ISSUE DOCT. DATE], [STORE ISSUE DOC AMT]   " & Environment.NewLine & _
        "  FROM (  " & Environment.NewLine & _
        " select TSPL_CAPEX_MASTER.DESCRIPTION as [CAPEX NAME ], TSPL_CAPEX_BUDGET_MASTER.Capex_Code ,(ISNULL(TSPL_CAPEX_MASTER.Current_Budget,0) +ISNULL(TSPL_CAPEX_MASTER.Inc_Budget ,0) ) as CAPEX_CURRENT_BUDGET,TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION as [SUB CAPEX NAME],TSPL_CAPEX_BUDGET_MASTER.CODE AS [SUB CAPEX CODE],(ISNULL(TSPL_CAPEX_BUDGET_MASTER.Current_Budget,0) +ISNULL(TSPL_CAPEX_BUDGET_MASTER.Inc_Budget ,0) ) as SUB_CAPEX_CURRENT_BUDGET,TSPL_ASSET_STORE_REQUISITION_DETAIL.Item_Code, TSPL_ITEM_MASTER.ITEM_dESC, " & Environment.NewLine & _
        " TSPL_ACQUISITION_HEAD.Acquisition_Code as [ACQUISITION ENTRY NO.],convert(varchar,TSPL_ACQUISITION_HEAD.Acquisition_Date,103) as [ACQUISITION ENTRY DATE], " & Environment.NewLine & _
        " ISNULL(TSPL_ACQUISITION_HEAD.Net_Amt ,0) AS [ACQUISITION AMOUNT],TSPL_ASSET_STORE_REQUISITION.Requisition_Id  AS [ASSETS STORE REQUESITION]," & Environment.NewLine & _
        " convert(varchar,TSPL_ASSET_STORE_REQUISITION.Requisition_Date,103) AS [ASSETS STORE REQUESITION DATE] " & Environment.NewLine & _
        " ,TSPL_IssueItemToAssembledAsset_HEAD.DOC_NO AS [STORE ISSUE DOCT. NO.],convert(varchar,TSPL_IssueItemToAssembledAsset_HEAD.Doc_Date,103) AS [STORE ISSUE DOCT. DATE],TSPL_IssueItemToAssembledAsset_HEAD.Doc_Amt as [STORE ISSUE DOC AMT]   " & Environment.NewLine & _
        " from TSPL_CAPEX_BUDGET_MASTER  " & Environment.NewLine & _
        " left outer join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER.CODE = TSPL_CAPEX_BUDGET_MASTER.Capex_Code  " & Environment.NewLine & _
        " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL .Capex_SubCode =TSPL_CAPEX_BUDGET_MASTER.CODE   " & Environment.NewLine & _
        " left outer join TSPL_ACQUISITION_HEAD  on TSPL_ACQUISITION_DETAIL .Acquisition_Code =TSPL_ACQUISITION_HEAD.Acquisition_Code  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_REQUISITION_HEAD AS TSPL_ASSET_STORE_REQUISITION ON TSPL_ASSET_STORE_REQUISITION .Capex_SubCode =TSPL_CAPEX_BUDGET_MASTER .CODE AND  TSPL_ASSET_STORE_REQUISITION.Is_Internal='Y' and TSPL_ASSET_STORE_REQUISITION.Requisition_Type<>'' AND TSPL_ASSET_STORE_REQUISITION.STATUS=1  and isnull(TSPL_ASSET_STORE_REQUISITION.Requisition_Id,'')<>''" & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_REQUISITION_detail AS TSPL_ASSET_STORE_REQUISITION_DETAIL ON  TSPL_ASSET_STORE_REQUISITION_DETAIL.Requisition_Id   =TSPL_ASSET_STORE_REQUISITION.Requisition_Id" & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_IssueItemToAssembledAsset_DETAIL ON TSPL_IssueItemToAssembledAsset_DETAIL.Capex_SubCode= TSPL_ASSET_STORE_REQUISITION .Capex_SubCode " & Environment.NewLine & _
        " and TSPL_IssueItemToAssembledAsset_DETAIL.Req_IssueNo =TSPL_ASSET_STORE_REQUISITION.Requisition_Id " & Environment.NewLine & _
        " and TSPL_ASSET_STORE_REQUISITION_DETAIL.Item_Code =TSPL_IssueItemToAssembledAsset_DETAIL.Item_Code " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_IssueItemToAssembledAsset_Head ON TSPL_IssueItemToAssembledAsset_DETAIL.Doc_No =TSPL_IssueItemToAssembledAsset_Head.Doc_No AND TSPL_IssueItemToAssembledAsset_HEAD.STATUS=1  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_ASSET_STORE_REQUISITION_DETAIL.Item_Code " & Environment.NewLine & _
        " -------------------------- only for item issue which is created seperate-------------------- " & Environment.NewLine & _
        " Union All " & Environment.NewLine & _
        " select TSPL_CAPEX_MASTER.DESCRIPTION as [CAPEX NAME ], TSPL_CAPEX_BUDGET_MASTER.Capex_Code ,(ISNULL(TSPL_CAPEX_MASTER.Current_Budget,0) +ISNULL(TSPL_CAPEX_MASTER.Inc_Budget ,0) ) as CAPEX_CURRENT_BUDGET,TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION as [SUB CAPEX NAME],TSPL_CAPEX_BUDGET_MASTER.CODE AS [SUB CAPEX CODE],(ISNULL(TSPL_CAPEX_BUDGET_MASTER.Current_Budget,0) +ISNULL(TSPL_CAPEX_BUDGET_MASTER.Inc_Budget ,0) ) as SUB_CAPEX_CURRENT_BUDGET,TSPL_IssueItemToAssembledAsset_DETAIL.Item_Code, TSPL_ITEM_MASTER.ITEM_dESC, " & Environment.NewLine & _
        " TSPL_ACQUISITION_HEAD.Acquisition_Code as [ACQUISITION ENTRY NO.],convert(varchar,TSPL_ACQUISITION_HEAD.Acquisition_Date,103) as [ACQUISITION ENTRY DATE],  " & Environment.NewLine & _
        " ISNULL(TSPL_ACQUISITION_HEAD.Net_Amt ,0) AS [ACQUISITION AMOUNT],''  AS [ASSETS STORE REQUESITION],null AS [ASSETS STORE REQUESITION DATE], " & Environment.NewLine & _
        " TSPL_IssueItemToAssembledAsset_HEAD.DOC_NO AS [STORE ISSUE DOCT. NO.],convert(varchar,TSPL_IssueItemToAssembledAsset_HEAD.Doc_Date,103) AS [STORE ISSUE DOCT. DATE],TSPL_IssueItemToAssembledAsset_HEAD.Doc_Amt as [STORE ISSUE DOC AMT]   " & Environment.NewLine & _
        " from TSPL_CAPEX_BUDGET_MASTER" & Environment.NewLine & _
        " left outer join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER.CODE = TSPL_CAPEX_BUDGET_MASTER.Capex_Code  " & Environment.NewLine & _
        " left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL .Capex_SubCode =TSPL_CAPEX_BUDGET_MASTER.CODE   " & Environment.NewLine & _
        " left outer join TSPL_ACQUISITION_HEAD  on TSPL_ACQUISITION_DETAIL .Acquisition_Code =TSPL_ACQUISITION_HEAD.Acquisition_Code  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_IssueItemToAssembledAsset_DETAIL ON TSPL_IssueItemToAssembledAsset_DETAIL.Capex_SubCode= TSPL_CAPEX_BUDGET_MASTER .CODE " & Environment.NewLine & _
        " and isnull(TSPL_IssueItemToAssembledAsset_DETAIL.Req_IssueNo,'')=''  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_IssueItemToAssembledAsset_Head ON TSPL_IssueItemToAssembledAsset_DETAIL.Doc_No =TSPL_IssueItemToAssembledAsset_Head.Doc_No AND TSPL_IssueItemToAssembledAsset_HEAD.STATUS=1  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_IssueItemToAssembledAsset_DETAIL.Item_Code  where isnull(TSPL_IssueItemToAssembledAsset_DETAIL.Req_IssueNo,'')=''  and isnull(TSPL_IssueItemToAssembledAsset_DETAIL.Capex_SubCode,'')<>'' " & Environment.NewLine & _
        " ) FINALQUERY " & Environment.NewLine & _
        " where 1=1  "

        If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
            qry += " AND FINALQUERY.Capex_Code IN (" + clsCommon.GetMulcallString(capexarr) + ")"
            End If
        If subcapexarr IsNot Nothing AndAlso subcapexarr.Count > 0 Then
            qry += " AND FINALQUERY.[SUB CAPEX CODE] IN (" + clsCommon.GetMulcallString(subcapexarr) + ")"
            End If
        If Itemarr IsNot Nothing AndAlso Itemarr.Count > 0 Then
            qry += " AND FINALQUERY.Item_Code IN (" + clsCommon.GetMulcallString(Itemarr) + ")"
        End If
            'qry += " AND convert(date,FINALQUERY.PurchaseOrder_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103) AND convert(date,FINALQUERY.PurchaseOrder_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103) " & _
            '    " ORDER BY  FINALQUERY.PurchaseOrder_Date"

        Return qry

    End Function

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If Not IsNothing(txtItem.arrValueMember) Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            clsCommon.ProgressBarShow()

            txtcapex.Enabled = False
            txtsubcapex.Enabled = False
            txtItem.Enabled = False

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            strRunQuery = getquery()

            If BulkExport = 1 Then
                transportSql.BulkExport("Capex Consumption Report", strRunQuery, "", "csv")
            ElseIf BulkExport = 2 Then
                transportSql.BulkExport("Capex Consumption Report", strRunQuery, "", "xls")
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
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)

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
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtcapex.arrValueMember = Nothing
        txtsubcapex.arrValueMember = Nothing
        txtItem.arrValueMember = Nothing
        txtcapex.Enabled = True
        txtsubcapex.Enabled = True
        txtItem.Enabled = True
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCapexConsumptionRpt & "'"))
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")


            If Not IsNothing(txtItem.arrValueMember) Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Export(Exporter.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Export(Exporter.PDF)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub

End Class


