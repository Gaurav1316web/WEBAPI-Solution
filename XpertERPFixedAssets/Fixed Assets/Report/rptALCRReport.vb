'' added by Richa Agarwal 18 Jan,2019  Against Ticket No. UDL/14/01/19-000254 (Capex Purchase Life Cycle Report)
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

Public Class rptALCRReport

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

        ''richa show Revised budget instead of Current budget of capex 16 Apr,2019
        Dim qry As String = "SELECT ROW_NUMBER () over(order by FINALQUERY.capex_code) as [SL. NO.],FINALQUERY.[CAPEX NAME],FINALQUERY.Capex_Code AS [CAPEX CODE],FINALQUERY.CAPEX_CURRENT_BUDGET AS [CAPEX BUDGET]," & Environment.NewLine & _
        " FINALQUERY.[SUB CAPEX NAME],FINALQUERY.[SUB CAPEX CODE],FINALQUERY.SUB_CAPEX_CURRENT_BUDGET AS [SUB CAPEX BUDGET],FINALQUERY.Item_Code [Item Code],FINALQUERY.ITEM_dESC AS [ITEM DESC],FINALQUERY.Requisition_Id AS [INDENT NO.]," & Environment.NewLine & _
        " convert(varchar,FINALQUERY.Requisition_Date,103) AS [INDENT DATE],FINALQUERY.PurchaseOrder_No AS [PO NO],convert(varchar,FINALQUERY.PurchaseOrder_Date,103) AS [PO DATE],FINALQUERY.Amount AS [PO AMOUNT],FINALQUERY.GRN_NO AS [GRN NO]," & Environment.NewLine & _
        " convert(varchar,FINALQUERY.GRN_Date,103) AS [GRN DATE],FINALQUERY.GRN_Qty AS [GRN QTY],FINALQUERY.MRN_NO AS [MRN NO],convert(varchar,FINALQUERY.MRN_Date,103) AS [MRN DATE],FINALQUERY.MRN_Qty AS [MRN QTY],FINALQUERY.SRN_NO AS [SRN NO]," & Environment.NewLine & _
        " convert(varchar,FINALQUERY.SRN_Date,103) AS [SRN DATE],FINALQUERY.SRN_Qty AS [SRN QTY],FINALQUERY.PI_No AS [PI NO],convert(varchar,FINALQUERY.PI_Date,103) AS [PI DATE],FINALQUERY.PI_BASIC_AMOUNT AS [PI BASIC AMT]," & Environment.NewLine & _
        " FINALQUERY.PI_FREIGHT AS [PI FREIGHT],FINALQUERY.PI_Total_Tax_Amt AS [PI TAX AMT],FINALQUERY.PI_TOTAL_AMOUNT AS [PI TOTAL AMT],FINALQUERY.PR_No AS [PR NO],convert(varchar,FINALQUERY.PR_Date,103) AS [PR DATE],FINALQUERY.PR_BASIC_AMOUNT AS [PR BASIC AMT]," & Environment.NewLine & _
        " FINALQUERY.PR_FREIGHT AS [PR FREIGHT],FINALQUERY.PR_Total_Tax_Amt AS [PR TAX AMT],FINALQUERY.PR_TOTAL_AMOUNT AS [PR TOTAL AMT] " & Environment.NewLine & _
        " FROM (select TSPL_CAPEX_MASTER.DESCRIPTION as [CAPEX NAME ], TSPL_CAPEX_BUDGET_MASTER.Capex_Code ,(ISNULL(TSPL_CAPEX_MASTER.Current_Budget,0) +ISNULL(TSPL_CAPEX_MASTER.Inc_Budget ,0) ) as CAPEX_CURRENT_BUDGET,TSPL_CAPEX_BUDGET_MASTER.DESCRIPTION as [SUB CAPEX NAME],TSPL_CAPEX_BUDGET_MASTER.CODE AS [SUB CAPEX CODE],(ISNULL(TSPL_CAPEX_BUDGET_MASTER.Current_Budget,0) +ISNULL(TSPL_CAPEX_BUDGET_MASTER.Inc_Budget ,0) ) SUB_CAPEX_CURRENT_BUDGET,TSPL_PURCHASE_ORDER_DETAIL.Item_Code, " & Environment.NewLine & _
        " TSPL_ITEM_MASTER.ITEM_dESC,TSPL_REQUISITION_HEAD.Requisition_Id,TSPL_REQUISITION_HEAD.Requisition_Date, " & Environment.NewLine & _
        " TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,TSPL_PURCHASE_ORDER_DETAIL.Item_Net_Amt as Amount , " & Environment.NewLine & _
        " TSPL_GRN_HEAD.GRN_NO,TSPL_GRN_HEAD.GRN_Date ,TSPL_GRN_DETAIL .GRN_Qty , " & Environment.NewLine & _
        " TSPL_MRN_HEAD.MRN_NO,TSPL_MRN_HEAD.MRN_Date ,TSPL_MRN_DETAIL .MRN_Qty , " & Environment.NewLine & _
        " TSPL_SRN_HEAD.SRN_No,TSPL_SRN_HEAD.SRN_Date ,TSPL_SRN_DETAIL .SRN_Qty , " & Environment.NewLine & _
        " TSPL_PI_HEAD.PI_No,TSPL_PI_HEAD.PI_Date , CASE WHEN TSPL_PI_DETAIL.Row_Type ='Item' THEN TSPL_PI_DETAIL.Amt_Less_Discount ELSE 0 END AS PI_BASIC_AMOUNT " & Environment.NewLine & _
        " ,CASE WHEN TSPL_PI_DETAIL.Row_Type ='Misc' THEN TSPL_PI_DETAIL.Amt_Less_Discount ELSE 0 END AS PI_FREIGHT,ISNULL(TSPL_PI_DETAIL.Total_Tax_Amt,0) AS PI_Total_Tax_Amt, " & Environment.NewLine & _
        " ISNULL( TSPL_PI_DETAIL.Amt_Less_Discount,0)+ISNULL(TSPL_PI_DETAIL.Total_Tax_Amt,0) AS PI_TOTAL_AMOUNT, " & Environment.NewLine & _
        " TSPL_PR_HEAD.PR_No,TSPL_PR_HEAD.PR_Date , CASE WHEN TSPL_PR_DETAIL.Row_Type ='Item' THEN TSPL_PR_DETAIL.Amt_Less_Discount ELSE 0 END AS PR_BASIC_AMOUNT " & Environment.NewLine & _
        " ,CASE WHEN TSPL_PR_DETAIL.Row_Type ='Misc' THEN TSPL_PR_DETAIL.Amt_Less_Discount ELSE 0 END AS PR_FREIGHT,ISNULL(TSPL_PR_DETAIL.Total_Tax_Amt,0)  AS PR_Total_Tax_Amt, " & Environment.NewLine & _
        " ISNULL( TSPL_PR_DETAIL.Amt_Less_Discount,0)+ISNULL(TSPL_PR_DETAIL.Total_Tax_Amt,0) AS PR_TOTAL_AMOUNT " & Environment.NewLine & _
        " from TSPL_CAPEX_BUDGET_MASTER " & Environment.NewLine & _
        " left outer join TSPL_CAPEX_MASTER on TSPL_CAPEX_MASTER.CODE = TSPL_CAPEX_BUDGET_MASTER.Capex_Code " & Environment.NewLine & _
        " left outer join TSPL_PURCHASE_ORDER_HEAD  ON TSPL_CAPEX_BUDGET_MASTER.CODE =TSPL_PURCHASE_ORDER_HEAD .Capex_SubCode    and TSPL_PURCHASE_ORDER_HEAD.Status =1  " & Environment.NewLine & _
        " left outer join  TSPL_PURCHASE_ORDER_DETAIL  on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.ITEM_CODE=TSPL_PURCHASE_ORDER_DETAIL.Item_Code " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_REQUISITION_detail ON  TSPL_REQUISITION_detail.Requisition_Id   =TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id    " & Environment.NewLine & _
        " AND TSPL_REQUISITION_detail.Item_Code =TSPL_PURCHASE_ORDER_DETAIL.Item_Code  " & Environment.NewLine & _
        " LEFT OUTER JOIN  TSPL_REQUISITION_HEAD ON TSPL_REQUISITION_detail.Requisition_Id =TSPL_REQUISITION_HEAD.Requisition_Id AND TSPL_REQUISITION_HEAD.STATUS=1 AND TSPL_REQUISITION_HEAD.Is_Internal='N' " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_GRN_DETAIL ON TSPL_GRN_DETAIL.PO_Id =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No AND TSPL_PURCHASE_ORDER_DETAIL.Item_Code =TSPL_GRN_DETAIL.Item_Code  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_GRN_HEAD ON TSPL_GRN_HEAD.GRN_NO  =TSPL_GRN_DETAIL.GRN_NO  AND TSPL_GRN_HEAD.STATUS=1 " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_MRN_DETAIL  ON TSPL_MRN_DETAIL.GRN_Id  =TSPL_GRN_HEAD.GRN_NO  AND TSPL_MRN_DETAIL.Item_Code =TSPL_GRN_DETAIL.Item_Code AND TSPL_MRN_DETAIL.PO_ID  =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No  " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_MRN_HEAD ON TSPL_MRN_HEAD.MRN_No   =TSPL_MRN_DETAIL.MRN_NO AND TSPL_MRN_HEAD.STATUS=1 " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_SRN_DETAIL  ON TSPL_SRN_DETAIL.MRN_Id   =TSPL_MRN_HEAD.MRN_No  AND TSPL_SRN_DETAIL.Item_Code =TSPL_MRN_DETAIL.Item_Code AND TSPL_SRN_DETAIL.PO_ID  =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No   =TSPL_SRN_DETAIL.SRN_NO AND TSPL_SRN_HEAD.STATUS=1 " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_PI_DETAIL ON TSPL_PI_DETAIL .SRN_Id =TSPL_SRN_HEAD .SRN_No AND TSPL_PI_DETAIL.Item_Code =TSPL_SRN_DETAIL.Item_Code  AND TSPL_PI_DETAIL.PO_ID  =TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No " & Environment.NewLine & _
        " LEFT OUTER JOIN TSPL_PI_HEAD  ON TSPL_PI_HEAD.PI_No  =TSPL_PI_DETAIL.PI_No AND TSPL_PI_HEAD.STATUS=1 " & Environment.NewLine & _
        " left outer join TSPL_PR_HEAD ON TSPL_PR_HEAD.Against_PI =TSPL_PI_HEAD .PI_No AND TSPL_PR_HEAD.STATUS=1 " & Environment.NewLine & _
        " left outer join TSPL_PR_DETAIL ON TSPL_PR_DETAIL.PI_Id  =TSPL_PI_HEAD .PI_No AND TSPL_PR_DETAIL.Item_Code =TSPL_PI_DETAIL.Item_Code " & Environment.NewLine & _
        " ) FINALQUERY " & Environment.NewLine & _
        " where 1=1 "

        If capexarr IsNot Nothing AndAlso capexarr.Count > 0 Then
            qry += " AND FINALQUERY.Capex_Code IN (" + clsCommon.GetMulcallString(capexarr) + ")"
        End If
        If subcapexarr IsNot Nothing AndAlso subcapexarr.Count > 0 Then
            qry += " AND FINALQUERY.[SUB CAPEX CODE] IN (" + clsCommon.GetMulcallString(subcapexarr) + ")"
        End If
        If Itemarr IsNot Nothing AndAlso Itemarr.Count > 0 Then
            qry += " AND FINALQUERY.Item_Code IN (" + clsCommon.GetMulcallString(Itemarr) + ")"
        End If
        qry += " AND convert(date,FINALQUERY.PurchaseOrder_Date,103)<=convert(date,'" + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + "',103) AND convert(date,FINALQUERY.PurchaseOrder_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "',103) " & _
            " ORDER BY  FINALQUERY.PurchaseOrder_Date"

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

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim str As String = ""
            Dim dt As DataTable = Nothing
            Dim strRunQuery As String = ""
            strRunQuery = getquery()

            If BulkExport = 1 Then
                transportSql.BulkExport("Capex Purchase Life Cycle Report", strRunQuery, "", "csv")
            ElseIf BulkExport = 2 Then
                transportSql.BulkExport("Capex Purchase Life Cycle Report", strRunQuery, "", "xls")
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptALCRReport & "'"))
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

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub

End Class


