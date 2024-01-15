'' Developped by Panch Raj on date:14-01-2017
Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO

Imports Microsoft.Office.Interop



Public Class FrmFatSNFStock
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrLoc As ArrayList
    Public arrItemGroup As ArrayList
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public MRP_Wise As Boolean = False
    Public ShowFatAndSNF As Boolean = False
    Dim dtCategory As DataTable
    Dim MIS_Item_Group As String
    Public InOutType As String = Nothing
    Public SkipCheckFatAndSNF As Boolean = False
    Public arrItemType As ArrayList
    Dim arrBack As List(Of String)

    Dim FORMTYPE As String = Nothing
#End Region
#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE
        '================================================
        arrBack = New List(Of String)

        SetUserMgmtNew()       
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        
        'btnPrint.Visible = False

        'If clsCommon.myLen(MIS_Item_Group) <= 0 Then
        '    clsCommon.MyMessageBoxShow("MIS Item Group Custom field is not create in Item Structure.")
        'End If
        If isDataLoad Then
            txtFromDate.Value = dtFrom
            txtToDate.Value = dtTo
           
            txtItem.arrValueMember = arrItem
            txtTransaction.arrValueMember = arrTransaction
            txtItemGroup.arrValueMember = arrItemGroup
            txtLocation.arrValueMember = arrLoc
            
            LoadData(False)

        End If
       
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmFatSnfStockReport)
        'MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
        ' RadButton8.Visible = MyBase.isExport
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        'LoadData(3)
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            gv1.EnableFiltering = True
            PageSetupReport_ID = GetReportID()
            TemplateGridview = gv1
            LoadData(0)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData(ByVal isPrintCrystalReport As Integer)
         Try
            clsCommon.ProgressBarShow()
            If txtLocation.arrValueMember Is Nothing OrElse txtLocation.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select Location.")
            End If

            btnGo.Enabled = False
            Dim dt As DataTable = clsInventoryMovement.GetFatSNFStockDT(txtLocation.arrValueMember, txtFromDate.Value, txtToDate.Value, chkIncludeSubLocation.Checked)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.DataSource = dt
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.BestFitColumns()
            gv1.EnableFiltering = True
            RadPageView1.SelectedPage = RadPageViewPage2
           
            gv1.ReadOnly = True
            SetGridFormationOFGV1()
            btnGo.Enabled = True
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message)
            btnGo.Enabled = True
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        'gv1.GroupDescriptors.Clear()
        'gv1.TableElement.TableHeaderHeight = 40
        'gv1.MasterTemplate.ShowRowHeaderColumn = False
        'For ii As Integer = 0 To gv1.Columns.Count - 1
        '    gv1.Columns(ii).ReadOnly = True
        '    gv1.Columns(ii).IsVisible = False
        'Next

        Dim summaryRowItem As New GridViewSummaryRowItem()
        For Each col As GridViewColumn In gv1.Columns           
            If (col.Name.Contains("FAT") = True OrElse col.Name.Contains("Fat") = True OrElse col.Name.Contains("SNF") = True OrElse col.Name.Contains("Total") = True) Then
                If (col.Name.Contains("Opening") = True OrElse col.Name.Contains("Closing") = True) Then
                    Continue For
                End If
                Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item)
                'ElseIf col.Name.Contains("Rate") = True Or col.Name.Contains("%") = True Then
                '    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Avg)
                '    summaryRowItem.Add(item)
            End If
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.BestFitColumns()
        ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = GetReportID()
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()

        gv2.DataSource = Nothing
        gv2.Columns.Clear()
        gv2.Rows.Clear()
        gv2.GroupDescriptors.Clear()

        RadPageView1.SelectedPage = RadPageViewPage1
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = txtToDate.Value
        'ChkMRPWise.Checked = False
        txtLocation.arrValueMember = Nothing
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        arrBack = New List(Of String)
        RadPageViewPage2.Text = "Report"

    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        'RadGroupBox3.Enabled = val
        txtItem.Enabled = val
        'RadGroupBox2.Enabled = val
        'cmbUnit.Enabled = val
        'cboType.Enabled = val
        'ChkMRPWise.Enabled = val
        'chkFATAndSNF.Enabled = val
        txtTransaction.Enabled = val
        'cboInOutType.Enabled = val
        txtItemGroup.Enabled = val
        'chkIncludeGIT.Enabled = val
        'txtItemType.Enabled = val
    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = "FATSNFStockReport"        
        Return ReportID
    End Function

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData("FATSNFStockReport", objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSett1.Click
        Dim ReportID As String = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub TreeView_NodeCheckedChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.TreeNodeCheckedEventArgs)
        TreeCheckBoxes(e.Node, e.Node.Checked)
    End Sub

    Public Sub TreeCheckBoxes(ByVal CurrentNode As RadTreeNode, ByVal val As Boolean)
        For Each Ctr As RadTreeNode In CurrentNode.Nodes
            Ctr.Checked = val
            TreeCheckBoxes(Ctr, val)
        Next
    End Sub

    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        'Dim qry As String
        'If txtItemType.arrValueMember Is Nothing OrElse clsCommon.GetMulcallString(txtItemType.arrValueMember) = "All" Then
        '    qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
        'Else
        '    qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") order by Item_Code "

        'End If

        'txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " select Code,Name,InOutType as [In/Out Type],Type from TSPL_INVENTORY_SOURCE_CODE where 2=2 "
        'If Not clsCommon.CompairString(clsCommon.myCstr(cboInOutType.SelectedValue), "All") = CompairStringResult.Equal Then
        '    qry += " and InOutType='" + clsCommon.myCstr(cboInOutType.SelectedValue) + "'"
        'End If
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSe", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub

    Private Sub txtItemGroup__My_Click(sender As Object, e As EventArgs) Handles txtItemGroup._My_Click
        Dim qry As String = " select SNo,Value,Description as Name,Custom_Field_Code as [Code] from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "' "
        txtItemGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemGroupMulSel", qry, "Value", "Name", txtItemGroup.arrValueMember, txtItemGroup.arrDispalyMember)
    End Sub

    'Private Sub RadButton4_Click(sender As Object, e As EventArgs)
    '    CheckedAll(gvLocation)
    'End Sub

    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            Dim qry As String = ""
            If clsCommon.myCdbl(e.Value) = 0 Then
                Exit Sub
            End If

            Dim Loc_Code As String = gv1.Rows(e.RowIndex).Cells("Location Code").Value
            arrLoc = New ArrayList
            arrLoc.Add(Loc_Code)
            Dim Trans_Date As Date = gv1.Rows(e.RowIndex).Cells("Trans Date").Value
            Dim Report_Type As String = gv1.Columns(e.ColumnIndex).Name.ToString.Replace(" FAT", "").Replace(" SNF", "")
            If Report_Type.Contains("Opening") = True OrElse Report_Type.Contains("Closing") = True Then
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            qry = clsInventoryMovement.GetDetailQry(arrLoc, Trans_Date, Trans_Date, Report_Type, chkIncludeSubLocation.Checked)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv2.DataSource = Nothing
            gv2.DataSource = dt
            gv2.ReadOnly = True
            gv2.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()
            For Each col As GridViewColumn In gv2.Columns
                If (col.Name.Contains("FAT") = True OrElse col.Name.Contains("Fat") = True OrElse col.Name.Contains("SNF") = True OrElse col.Name.Contains("Cost") = True OrElse col.Name.Contains("Qty") = True OrElse col.Name.Contains("Amount") = True) Then
                    If col.Name.Contains("_Per") = True Then
                        Continue For
                    End If
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                End If
            Next

            gv2.SummaryRowsBottom.Add(summaryRowItem)
            gv2.BestFitColumns()            
            gv2.EnableFiltering = True
            RadPageView1.SelectedPage = RadPageViewPage3
            clsCommon.ProgressBarHide()
            
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

   
    Private Sub print(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmFatSnfStockReport & "'"))
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            'If txtItemGroup.arrDispalyMember IsNot Nothing AndAlso txtItemGroup.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Item Group : " + clsCommon.GetMulcallStringWithComma(txtItemGroup.arrDispalyMember))
            'End If
            'If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            'End If
            'If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
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
                If gv2.DataSource Is Nothing Then
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                Else
                    If clsCommon.MyMessageBoxShow(Me, "Want to export Detail part of the report ?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        'transportSql.exportdataChilRows(gv2, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                        transportSql.QuickExportToExcel(gv2, "", Me.Text, , arrHeader)
                    Else
                        transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                        transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                        'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    End If
                End If

                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                If gv2.DataSource Is Nothing Then
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Fat-SNF Stock Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                Else
                    If clsCommon.MyMessageBoxShow(Me,"Want to export Detail part of the report ?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        clsCommon.MyExportToPDF("Fat-SNF Stock Report", gv2, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                    Else
                        transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                        clsCommon.MyExportToPDF("Fat-SNF Stock Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    'Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
    '    '=====================Added by Preeti Gupta====
    '    Try
    '        LoadData(1)
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub


    Private Shared Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = [String].Empty
        Dim [mod] As Integer = 0
        While div > 0
            [mod] = (div - 1) Mod 26
            colLetter = (Convert.ToChar(65 + [mod])).ToString & colLetter
            div = CInt((div - [mod]) / 26)
        End While
        Return colLetter
    End Function

    Private Sub LoadDataInGridViaDataReader(ByVal qry As String)
        'clsCommon.ProgressBarPercentShow()
        'clsCommon.ProgressBarUpdate("Fatching data...")
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            If reader Is Nothing OrElse Not reader.HasRows Then
                Throw New Exception("No Data found")
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim dtTest As New DataTable
            dtTest.Load(reader)
            gv1.DataSource = dtTest
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        LoadData(4)
    End Sub

    Private Sub BulkExportXls_Click(sender As Object, e As EventArgs) Handles BulkExportXls.Click
        LoadData(5)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        ''Is_Section='N' and Is_Sub_Location='N' and
        Dim qry As String = " select Location_Code as CODE,Location_Desc as NAME,Main_Location_Code as [Main Location] from TSPL_LOCATION_MASTER where (( Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') ) "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "CODE", "NAME", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub gv1_Click(sender As Object, e As EventArgs) Handles gv1.Click

    End Sub

    Private Sub gv2_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv2.CellDoubleClick
        Dim strTransType As String = clsCommon.myCstr(gv2.CurrentRow.Cells("Trans_Type").Value)
        Dim strTransCode As String = clsCommon.myCstr(gv2.CurrentRow.Cells("Source_Doc_No").Value)
        If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
            Select Case strTransType
                Case "IC-AD"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
                Case "ISSTRAN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strTransCode)
                Case "SRN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strTransCode)
                Case "SD-IN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                Case "Sale Return"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strTransCode)
                Case "SD-CSATRANS"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strTransCode)
                Case "SD-SH"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                Case "CSA-SALE"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, strTransCode)
                Case "RICE-MIX"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceMixingEntry, strTransCode)
                Case "RICE-PROC"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceProcessingEntry, strTransCode)
                Case "PP_ISSUE"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, strTransCode)
                Case "PP_STDN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strTransCode)
                    'Case "BulkSRNTrade"
                    '    clsOpenTransactionForm.OpenTransacionForm(EnumTransType.BulkSRNTrade, strTransCode)
                'Case "DispatchBSTrade"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strTransCode)
                Case "DispChallan"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strTransCode)
                Case "MilkTransferIn"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, strTransCode)
                Case "IC-AD"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
                Case "DispatchBS"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, strTransCode)
                Case "PRD_STG_PROC"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, strTransCode)
                Case "RGP"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
                Case "NRGP"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
                Case "PROD_ENTRY"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, strTransCode)
                'Case "MCC-IISSUE"
                '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, strTransCode)
                Case "FS-SH"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDispatchMultipleFreshSale, strTransCode)
                Case "PS-SH"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                Case "Transfer"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                Case "BulkSRN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, strTransCode)
                Case "MCC-MSRN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, strTransCode)
                Case "SD-CSATRANS-RETURN"
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
            End Select

        End If
    End Sub
End Class
