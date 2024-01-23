Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class rptSRNReco
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()

#End Region

    Private Sub rptSRNReco_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.rptSRNReco)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal BulkExport As Integer = 0)
        Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()

            Dim strQry As String = ""
            Dim dt As DataTable = Nothing

            strQry = "select Convert(VARCHAR,SRN.DOC_DATE,103)  AS [SRN Date],SRN.DOC_CODE AS [SRN No]
                    ,Convert(VARCHAR,INV.Entry_Date,103)  AS [Inv Entry Date] 
                    ,INV.Source_Doc_No  AS [Inv Source Doc No]
                    ,Convert(VARCHAR,INV.Source_Doc_Date,103)  AS [Inv Source Doc Date]
                    ,INV.Fat_Amt as [Fat Amt]
                    ,INV.SNF_Amt as [SNF Amt]
                    ,INV.Avg_Cost as [Avg Cost]
                    ,CAST(abs(ROUND(INV.Fat_Amt+INV.SNF_Amt,2)-INV.Avg_Cost) as decimal(18,2)) as [Difference Avg V/s Fat+Snf]
                    ,JV.VOUCHER_NO AS [Voucher No],JV.VOUCHER_Date AS [Voucher Date]
                    ,JV.Source_Doc_No  AS [JV Source Doc No]
                    ,Convert(VARCHAR,JV.Source_Doc_Date,103)  AS [JV Source Doc Date]
                    ,JV.Total_Debit_Amt AS [Debit Amt],JV.Total_Credit_Amt AS [Credit Amt]
                    from 
                    (select * from TSPL_MILK_SRN_head where POSTED=1 AND Convert(Date, TSPL_MILK_SRN_head.DOC_DATE,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' and convert(date, TSPL_MILK_SRN_head.DOC_DATE,103)<='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "') SRN
                    full outer join
                    (select * from TSPL_inventory_movement_new where trans_type='MCC-MSRN' AND Convert(Date, TSPL_inventory_movement_new.Source_Doc_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' and convert(date, TSPL_inventory_movement_new.Source_Doc_Date,103)<='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "') INV 
                    ON INV.Source_Doc_No=SRN.DOC_CODE
                    full outer join
                    (select * from TSPL_JOURNAL_MASTER where source_code='MI-SR' AND Convert(Date, TSPL_JOURNAL_MASTER.Source_Doc_Date,103) >='" + clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy") + "' and convert(date, TSPL_JOURNAL_MASTER.Source_Doc_Date,103)<='" + clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy") + "') JV
                    ON (JV.Source_Doc_No=SRN.DOC_CODE or INV.Source_Doc_No=JV.Source_Doc_No)"


            dt = clsDBFuncationality.GetDataTable(strQry)

            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                EnableDisableAllControl(False)
                Gv1.DataSource = dt
                SetGridFormationOFGV1()
            End If

            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub EnableDisableAllControl(ByVal val As Boolean)
        fromDate.Enabled = val
        ToDate.Enabled = val
    End Sub

    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns(ii).BestFit()
        Next

        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = True
        Gv1.BestFitColumns()
    End Sub

    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Columns.Clear()
        Gv1.Rows.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        EnableDisableAllControl(True)
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

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
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Milk SRN Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rptSRNReco_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : Milk SRN Reco")
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Milk SRN Reco", Gv1, arrHeader, "Milk SRN Reco", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class


