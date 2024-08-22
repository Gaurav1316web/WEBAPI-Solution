Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine

'Sanjay, Ticket No  TEC/06/09/19-001003 
Public Class rptDeleteHistoryReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strModuleCode As String = Nothing


    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try

            If clsCommon.myLen(fndScreen.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Screen ....", Me.Text)
                Exit Sub
            End If
            If txtToDate.Value < txtFromDate.Value Then
                clsCommon.MyMessageBoxShow(Me, "To Date cant be less than from date", Me.Text)
                Exit Sub
            End If
            
            Dim DateRangeColumn As String = ""
            Dim StrMasterTable As String = ""
            Dim StrWhere As String = ""
            Dim DetailTable1 As String = ""
            Dim DetailTable2 As String = ""
            Dim DetailTable3 As String = ""
            Dim DetailTable4 As String = ""
            Dim DetailTable5 As String = ""
            Dim DetailTable6 As String = ""
            Dim DetailTable7 As String = ""
            Dim DetailTable8 As String = ""
            Dim DetailTable9 As String = ""

            Dim qry As String = Nothing

            If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.ReceiptEntry) = CompairStringResult.Equal Then
                '--=================clsUserMgtCode.ReceiptEntry, "Receipt Entry"
                StrMasterTable = "TSPL_RECEIPT_HEADER"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Receipt_Date"
                End If
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnARInvoiceEntry) = CompairStringResult.Equal Then
                '--=================clsUserMgtCode.mbtnARInvoiceEntry, "AR Invoice Entry
                StrMasterTable = "TSPL_customer_Invoice_Head"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Document_Date"
                End If
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PaymentEntryNew) = CompairStringResult.Equal Then
                '--clsUserMgtCode.PaymentEntryNew, "Payment Entry"
                StrMasterTable = "TSPL_PAYMENT_HEADER"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Payment_Date"
                End If
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnAPInvoiceEntry) = CompairStringResult.Equal Then
                '--clsUserMgtCode.mbtnAPInvoiceEntry, "AP Invoice Entry"
                StrMasterTable = "TSPL_vendor_invoice_head"
                StrWhere = " is_For_TDS=0 and Invoice_Type in ('AP','VC') "
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Invoice_Entry_Date"
                End If
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.FrmVendorService) = CompairStringResult.Equal Then
                '--clsUserMgtCode.FrmVendorService, "Vendor Service Charge"
                StrMasterTable = "TSPL_vendor_invoice_head"
                StrWhere = " is_For_TDS=0  and Invoice_Type in ('VS') "
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Invoice_Entry_Date"
                End If
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.mbtnAPInvoiceEntryTDS) = CompairStringResult.Equal Then
                '--clsUserMgtCode.mbtnAPInvoiceEntryTDS, "AP Invoice TDS"
                StrMasterTable = "TSPL_vendor_invoice_head"
                StrWhere = " is_For_TDS=1 "
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Invoice_Entry_Date"
                End If
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.reverseTransaction) = CompairStringResult.Equal Then
                '--=================clsUserMgtCode.reverseTransaction, "Bank Reverse"
                StrMasterTable = "TSPL_BANK_REVERSE"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Reversal_Date"
                End If
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.bankTransfer) = CompairStringResult.Equal Then
                '--=================clsUserMgtCode.bankTransfer, "Bank Transfer"
                StrMasterTable = "TSPL_BANK_TRANSFER"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Transfer_Date"
                End If
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProductionEntry) = CompairStringResult.Equal Then
                StrMasterTable = "TSPL_PP_PRODUCTION_ENTRY"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "PROD_DATE"
                End If
                DetailTable1 = "Issue Detail"
                DetailTable2 = "Stage Detail"
                DetailTable3 = "Batch Production Detail"
                DetailTable4 = "Quality Check Detail"
                DetailTable5 = "Wreckage & Flushing Detail"
                DetailTable6 = "Scrap Detail"
                DetailTable7 = "Section Stock"
                DetailTable8 = "Section Stock History Detail"
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProductionPlanningDairy) = CompairStringResult.Equal Then
                StrMasterTable = "TSPL_PP_PRODUCTION_PLAN_HEAD"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Plan_Date"
                End If
                DetailTable1 = "Plan Detail"
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBatchOrderDairy) = CompairStringResult.Equal Then
                StrMasterTable = "TSPL_PP_BATCH_ORDER_HEAD"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Batch_Date"
                End If
                DetailTable1 = "BOM Detail"
                DetailTable2 = "ITEM Detail"
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProcessProductionIssueEntry) = CompairStringResult.Equal Then
                StrMasterTable = "TSPL_PP_ISSUE_HEAD"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Issue_Date"
                End If
                DetailTable1 = "ITEM Detail"
                DetailTable2 = "QC Detail"

            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProcessProductionStandardization) = CompairStringResult.Equal Then
                StrMasterTable = "TSPL_PP_STANDARDIZATION_HEAD"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Standardization_Date"
                End If
                DetailTable1 = "QC DETAIL"
                DetailTable2 = "BATCH ITEM DETAIL"
                DetailTable3 = "ISSUE ITEM DETAIL"
                DetailTable4 = "ADD REMOVE ITEM DETAIL"
                DetailTable5 = "QC LOG SHEET"
                DetailTable5 = "STAGE DETAIL"
                DetailTable6 = "CONSUMPTION DETAIL"

            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PricePlan) = CompairStringResult.Equal Then
                StrMasterTable = "TSPL_ITEM_PRICE_PLAN_HEADER"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Plan_Date"
                End If
            ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PriceMaster) = CompairStringResult.Equal Then
                StrMasterTable = "TSPL_ITEM_Price_MASTER"
                If rbtn_Document.Checked = True Then
                    DateRangeColumn = "Created_Date"
                End If
            End If

            qry = clsERPFuncationality.ShowDeletedData(txtFromDate.Value, txtToDate.Value, StrWhere, DateRangeColumn, StrMasterTable, DetailTable1, DetailTable2, DetailTable3, DetailTable4, DetailTable5, DetailTable6, DetailTable7, DetailTable8, DetailTable9)

            If clsCommon.myLen(qry) > 0 Then

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    gv1.DataSource = Nothing
                    gv1.Columns.Clear()
                    gv1.Rows.Clear()
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    gv1.ShowGroupPanel = True
                    gv1.EnableFiltering = True
                    RadPageView1.SelectedPage = RadPageViewPage2

                    fndScreen.Enabled = False
                    'RadGroupBox2.Enabled = False
                    Panel1.Enabled = False

                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                End If

                gv1.DataSource = dt
                SetGridFormationOFGV1()
                gv1.BestFitColumns()

                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Related to this screen.", Me.Text)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).BestFit()
        Next

    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        fndScreen.Value = ""
        lblScreen.Text = ""
        fndScreen.Enabled = True
        'RadGroupBox2.Enabled = True
        Panel1.Enabled = True
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        GetReportID()
        TemplateGridview = gv1
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        Print(Exporter.Refresh)
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If clsCommon.myLen(fndScreen.Value) > 0 Then
            VarID += "_" + clsCommon.myCstr(fndScreen.Value)
            gv1.VarID = VarID
        End If

    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rptTankerStatusReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub rptTankerStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
        'strModuleCode = MyBase.Module_Code
        'fndModule.Value = MyBase.Module_Code
        'lblModule.Text = clsDBFuncationality.getSingleValue(" select Program_Name  from TSPL_PROGRAM_MASTER where Program_Code = '" + strModuleCode + "' ")
        'fndModule.Enabled = False
        txtFromDate.Checked = True
        txtToDate.Checked = True
    End Sub




    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Delete History Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If clsCommon.myLen(fndScreen.Value) > 0 Then
                    arrHeader.Add("Screen Name : " + clsCommon.myCstr(lblScreen.Text))
                End If

                'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Delete History Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                If clsCommon.myLen(fndScreen.Value) > 0 Then
                    arrHeader.Add("Screen Name : " + clsCommon.myCstr(lblScreen.Text))
                End If
            
                'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Delete History Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndScreen__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndScreen._MYValidating
        Dim qry As String = Nothing
        Dim wher As String = " 2= 2"

        qry = "  select TSPL_PROGRAM_MASTER.Program_Code as Code,case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as Name , TBL_MODULE.Program_Name as [Module Name] from TSPL_PROGRAM_MASTER " & _
              "  left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('SM')) as TBL_SMODULE on TBL_SMODULE.Program_Code = TSPL_PROGRAM_MASTER.Parent_Code " & _
              "  left outer join (select Program_Code, Program_Name,Parent_Code from TSPL_PROGRAM_MASTER where Type in ('M')) as TBL_MODULE on TBL_MODULE.Program_Code = TBL_SMODULE.Parent_Code  "
        wher = " TBL_MODULE.Program_Code in (select  distinct Module_Name from TSPL_MODULE_PERMISSION ) and  not TSPL_PROGRAM_MASTER.Type in ('M','SM') and  TSPL_PROGRAM_MASTER.Program_Code in ('" &
                  "" + clsUserMgtCode.ReceiptEntry + "','" + clsUserMgtCode.mbtnARInvoiceEntry + "','" + clsUserMgtCode.PaymentEntryNew + "','" + clsUserMgtCode.mbtnAPInvoiceEntry + "','" + clsUserMgtCode.FrmVendorService + "','" + clsUserMgtCode.mbtnAPInvoiceEntryTDS + "" &
                     "','" + clsUserMgtCode.bankTransfer + "','" + clsUserMgtCode.reverseTransaction + "', '" + clsUserMgtCode.frmProductionEntry + "', '" + clsUserMgtCode.frmProductionPlanningDairy + "', '" + clsUserMgtCode.frmBatchOrderDairy + "', '" + clsUserMgtCode.frmProcessProductionIssueEntry + "', '" + clsUserMgtCode.frmProcessProductionStandardization + "" &
                     "', '" + clsUserMgtCode.PricePlan + "','" + clsUserMgtCode.PriceMaster + "' " &
                    " )  "
        fndScreen.Value = clsCommon.ShowSelectForm("LocationSegGP", qry, "Code", wher, fndScreen.Value, "TSPL_PROGRAM_MASTER.sno", isButtonClicked)
        lblScreen.Text = clsDBFuncationality.getSingleValue("select  case when len (isnull(TSPL_PROGRAM_MASTER.Re_Name,'')) > 0 then TSPL_PROGRAM_MASTER.Re_Name else  TSPL_PROGRAM_MASTER.Program_Name end  as Name  from TSPL_PROGRAM_MASTER where Program_Code='" & fndScreen.Value & "'")
        'If clsCommon.myLen(fndScreen.Value) > 0 Then
        '    RadGroupBox2.Enabled = False
        'Else
        '    RadGroupBox2.Enabled = True
        'End If
    End Sub

   
    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If gv1.CurrentRow.Index >= 0 Then
                If clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProductionEntry) = CompairStringResult.Equal Then
                    'DetailTable1 = "Issue Detail"
                    'DetailTable2 = "Stage Detail"
                    'DetailTable3 = "Batch Production Detail"
                    'DetailTable4 = "Quality Check Detail"
                    'DetailTable5 = "Wreckage & Flushing Detail"
                    'DetailTable6 = "Scrap Detail"
                    'DetailTable7 = "Section Stock"
                    'DetailTable8 = "Section Stock History Detail"
                    If e.Column Is gv1.Columns("Issue Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("PROD ENTRY CODE").Value), "PROD_ENTRY_CODE", "TSPL_PP_PE_ISSUE_ITEM_DETAIL")
                        ' clsERPFuncationalityold.ShowTransHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("PROD ENTRY CODE").Value), "PROD_ENTRY_CODE", "TSPL_PP_PRODUCTION_ENTRY", "TSPL_PP_PE_ISSUE_ITEM_DETAIL")
                    ElseIf e.Column Is gv1.Columns("Stage Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("PROD ENTRY CODE").Value), "PROD_ENTRY_CODE", "TSPL_PP_PE_STAGE_DETAIL")
                    ElseIf e.Column Is gv1.Columns("Batch Production Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("PROD ENTRY CODE").Value), "PROD_ENTRY_CODE", "TSPL_PP_PRODUCTION_ENTRY_DETAIL")
                    ElseIf e.Column Is gv1.Columns("Quality Check Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("PROD ENTRY CODE").Value), "PROD_ENTRY_CODE", "TSPL_PP_PE_QC_DETAIL")
                        'clsERPFuncationalityold.ShowTransHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("PROD ENTRY CODE").Value), "PROD_ENTRY_CODE", "TSPL_PP_PRODUCTION_ENTRY", "TSPL_PP_PE_QC_DETAIL")
                    ElseIf e.Column Is gv1.Columns("Wreckage & Flushing Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("PROD ENTRY CODE").Value), "PROD_ENTRY_CODE", "TSPL_PP_PE_WRECKAGE_FLASHING")
                    ElseIf e.Column Is gv1.Columns("Scrap Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("PROD ENTRY CODE").Value), "PROD_ENTRY_CODE", "TSPL_PP_PE_SCRAP_DETAIL")
                    End If

                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProductionPlanningDairy) = CompairStringResult.Equal Then
                    If e.Column Is gv1.Columns("Plan Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("PLAN CODE").Value), "PLAN_CODE", "TSPL_PP_PRODUCTION_PLAN_DETAIL")
                    End If
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmBatchOrderDairy) = CompairStringResult.Equal Then
                    If e.Column Is gv1.Columns("BOM Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("BATCH CODE").Value), "batch_code", "TSPL_PP_BATCH_ORDER_BOM_DETAIL")
                    ElseIf e.Column Is gv1.Columns("ITEM Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("BATCH CODE").Value), "batch_code", "TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL")
                    End If
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProcessProductionIssueEntry) = CompairStringResult.Equal Then
                    If e.Column Is gv1.Columns("ITEM Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("issue code").Value), "issue_code", "TSPL_PP_ISSUE_ITEM_DETAIL")
                    ElseIf e.Column Is gv1.Columns("QC Detail") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("issue code").Value), "issue_code", "TSPL_PP_ISSUE_QC_DETAIL")
                    End If
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.frmProcessProductionStandardization) = CompairStringResult.Equal Then
                    If e.Column Is gv1.Columns("QC DETAIL") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("Standardization Code").Value), "Standardization_Code", "TSPL_PP_STD_QC_DETAIL")
                    ElseIf e.Column Is gv1.Columns("BATCH ITEM DETAIL") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("Standardization Code").Value), "Standardization_Code", "TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL")
                    ElseIf e.Column Is gv1.Columns("ISSUE ITEM DETAIL") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("Standardization Code").Value), "Standardization_Code", "TSPL_PP_STD_ISSUE_ITEM_DETAIL")
                    ElseIf e.Column Is gv1.Columns("ADD REMOVE ITEM DETAIL") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("Standardization Code").Value), "Standardization_Code", "TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL")
                    ElseIf e.Column Is gv1.Columns("QC LOG SHEET") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("Standardization Code").Value), "Standardization_Code", "TSPL_PP_STD_QC_LOG_SHEET")
                    ElseIf e.Column Is gv1.Columns("STAGE DETAIL") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("Standardization Code").Value), "Standardization_Code", "TSPL_PP_STD_STAGE_DETAIL")
                    ElseIf e.Column Is gv1.Columns("CONSUMPTION DETAIL") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("Standardization Code").Value), "Standardization_Code", "TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL")
                    End If

                    'StrMasterTable = "TSPL_PP_STANDARDIZATION_HEAD"
                    'If rbtn_Document.Checked = True Then
                    '    DateRangeColumn = "Standardization_Date"
                    'End If
                    'DetailTable1 = "QC DETAIL"
                    'DetailTable2 = "BATCH ITEM DETAIL"
                    'DetailTable3 = "ISSUE ITEM DETAIL"
                    'DetailTable4 = "ADD REMOVE ITEM DETAIL"
                    'DetailTable5 = "QC LOG SHEET"
                    'DetailTable5 = "STAGE DETAIL"
                    'DetailTable6 = "CONSUMPTION DETAIL"
                ElseIf clsCommon.CompairString(fndScreen.Value, clsUserMgtCode.PricePlan) = CompairStringResult.Equal Then
                    If e.Column Is gv1.Columns("PLAN CODE") Then
                        clsERPFuncationalityOLD.ShowHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("PLAN CODE").Value), "PLAN_CODE", "TSPL_ITEM_PRICE_PLAN_DETAIL")
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
