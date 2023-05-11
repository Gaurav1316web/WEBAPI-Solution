'' Developped by Panch Raj on date:15-02-2018
Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO

Imports Microsoft.Office.Interop



Public Class frmMCCMilkLossGain
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    'Public arrLoc As ArrayList
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
    Dim isInsideLoadData As Boolean = False
    Dim FORMTYPE As String = Nothing
    Public arrLoc As Dictionary(Of String, Object) = Nothing
    Dim isLoad As Boolean = True
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
        'txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        'txtToDate.Value = clsCommon.GETSERVERDATE
        '================================================
        isLoad = True
        arrBack = New List(Of String)
        LoadType()

        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        reset()

        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        'rbtnLocationAll.IsChecked = True
        'gvLocation.Enabled = rbtnLocationSelect.IsChecked
        'RadButton4.Enabled = rbtnLocationSelect.IsChecked
        'RadButton5.Enabled = rbtnLocationSelect.IsChecked
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
            'txtLocation.arrValueMember = arrLoc
            'If arrLoc IsNot Nothing AndAlso arrLoc.Count > 0 Then
            '    rbtnLocationSelect.IsChecked = True
            '    For Each str As String In arrLoc.Keys
            '        For ii As Integer = 0 To gvLocation.RowCount - 1
            '            If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
            '                gvLocation.Rows(ii).Cells("SEL").Value = True
            '                gvLocation.Rows(ii).Tag = arrLoc(str)
            '            End If
            '        Next
            '    Next
            'End If

            LoadData(False)

        End If
        isLoad = False
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMCCMilkStockReport)
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
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv1
            LoadData(0)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData(ByVal isPrintCrystalReport As Integer)
        Try
            clsCommon.ProgressBarShow()
            'If txtLocation.arrValueMember Is Nothing OrElse txtLocation.arrValueMember.Count <= 0 Then
            '    Throw New Exception("Please select Location.")
            'End If

            'If txtItem.arrValueMember Is Nothing OrElse txtItem.arrValueMember.Count <= 0 Then
            '    Throw New Exception("Please select Item.")
            'End If
            Dim objFilter As clsStockRecoFilters = New clsStockRecoFilters
            objFilter.arrLocation = New List(Of clsCode)
            objFilter.arrCategory = New List(Of clsCode)
            objFilter.arrLoc = New ArrayList

            objFilter.arrItem = txtItem.arrValueMember
            objFilter.From_Date = txtFromDate.Value
            objFilter.To_Date = txtToDate.Value
            objFilter.ReportType = cboType.SelectedValue
            objFilter.SelectLocation = True
            EnableDisableCtrl(False)
            'For intLoc As Integer = 0 To gvLocation.RowCount - 1
            Dim loc As New clsCode
            '====================add by Monika26/03/2017==================
            loc.Sel = True

            '============================================================
            loc.Code = fndLoc.Value
            loc.Desc = txtLocName.Text
            loc.arrOut = Nothing
            objFilter.arrLocation.Add(loc)
            If loc.Sel Then
                objFilter.arrLoc.Add(loc.Code)
            End If
            'Next

            btnGo.Enabled = False
            Dim dt As DataTable = clsInventoryMovement.GetQtyFatSNFStockGKDTCrystel(objFilter, True)
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
            clsCommon.MyMessageBoxShow(ex.Message)
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
            If (col.Name.Contains("FAT") = True OrElse col.Name.Contains("Fat") = True OrElse col.Name.Contains("SNF") = True OrElse col.Name.Contains("Total") = True OrElse col.Name.Contains("Quantity") = True OrElse col.Name.Contains("Qty") = True) Then
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
            Dim ReportID As String = PageSetupReport_ID
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
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub


    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arrHeader As List(Of String) = New List(Of String)()
        'Dim strTemp As String = ""
        'arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + " ")

        'arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
        ''If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
        ''    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
        ''End If
        ''If rbtnLocationSelect.IsChecked Then
        ''    Dim strLoca As String = ""
        ''    For Each grow As GridViewRowInfo In gvLocation.Rows
        ''        If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
        ''            strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
        ''        End If
        ''    Next
        ''    arrHeader.Add("Location : " + strLoca)
        ''End If
        'arrHeader.Add("Location : " + fndLoc.Value)
        'clsCommon.MyExportToExcelGrid(" Fat-SNF Stock Report:", gv1, arrHeader, Me.Text)
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    'Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
    '    EnableDisableCtrl(True)
    '    gv1.DataSource = Nothing
    '    gv1.Columns.Clear()
    '    gv1.Rows.Clear()
    '    gv1.GroupDescriptors.Clear()

    '    gv2.DataSource = Nothing
    '    gv2.Columns.Clear()
    '    gv2.Rows.Clear()
    '    gv2.GroupDescriptors.Clear()

    '    RadPageView1.SelectedPage = RadPageViewPage1
    '    'txtToDate.Value = clsCommon.GETSERVERDATE()
    '    'txtFromDate.Value = txtToDate.Value
    '    'ChkMRPWise.Checked = False
    '    'txtLocation.arrValueMember = Nothing
    '    gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '    arrBack = New List(Of String)
    '    RadPageViewPage2.Text = "Report"

    'End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        cboType.Enabled = val
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
        'RadGroupBox2.Enabled = val
        'chkIncludeGIT.Enabled = val
        'txtItemType.Enabled = val
    End Sub

    'Private Function GetReportID() As String
    '    Dim ReportID As String = "FATSNFStockReport"
    '    Return ReportID
    'End Function

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSett1.Click
        Dim ReportID As String = PageSetupReport_ID
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
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
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name from TSPL_ITEM_MASTER where Product_Type='MI'"
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)

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
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Summary") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Summary") Then
                    arrBack.Add("Summary")
                End If
                cboType.SelectedValue = "Monthly"
                'arrItemType = New ArrayList()
                'arrItemType = txtItemType.arrValueMember()

                'Dim tmp As New ArrayList()
                'tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Type").Value))
                'txtItemType.arrValueMember = tmp
                LoadData(0)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Monthly") = CompairStringResult.Equal Then
                If Not arrBack.Contains("Monthly") Then
                    arrBack.Add("Monthly")
                End If
                cboType.SelectedValue = "Daily"
                arrItem = New ArrayList()
                arrItem = txtItem.arrValueMember
                Dim tmp As New ArrayList()
                tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Item Code").Value))
                txtItem.arrValueMember = tmp
                'rbtnLocationSelect.IsChecked = True
                arrLoc = Nothing
                arrLoc = New Dictionary(Of String, Object)
                arrLoc.Add(fndLoc.Value, txtLocName.Text)

                'UnCheckedAll(gvLocation)
                'rbtnLocationSelect.IsChecked = True
                Dim arrInn As New Dictionary(Of String, Object)
                arrInn.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Location Code").Value), Nothing)
                'For ii As Integer = 0 To gvLocation.RowCount - 1
                '    If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Location Code").Value)) = CompairStringResult.Equal Then
                '        gvLocation.Rows(ii).Cells("SEL").Value = True
                '        gvLocation.Rows(ii).Tag = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Location Code").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Location Code").Value)) = CompairStringResult.Equal, Nothing, arrInn)
                '    End If
                'Next
                LoadData(0)


            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Daily") = CompairStringResult.Equal Then
                'Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value)
                'Dim strTransCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Source_Doc_No").Value)
                'If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
                '    Select Case strTransType
                '        Case "IC-AD"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
                '        Case "ISSTRAN"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strTransCode)
                '        Case "SRN"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strTransCode)
                '        Case "SD-IN"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                '        Case "Sale Return"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strTransCode)
                '        Case "SD-CSATRANS"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strTransCode)
                '        Case "SD-SH"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                '        Case "CSA-SALE"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, strTransCode)
                '        Case "RICE-MIX"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceMixingEntry, strTransCode)
                '        Case "RICE-PROC"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceProcessingEntry, strTransCode)
                '        Case "PP_ISSUE"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, strTransCode)
                '        Case "PP_STDN"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strTransCode)
                '        Case "BulkSRNTrade"
                '            clsOpenTransactionForm.OpenTransacionForm(EnumTransType.BulkSRNTrade, strTransCode)
                '        Case "DispatchBSTrade"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strTransCode)
                '        Case "DispChallan"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strTransCode)
                '        Case "MilkTransferIn"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, strTransCode)
                '        Case "IC-AD"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
                '        Case "DispatchBS"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, strTransCode)
                '        Case "PRD_STG_PROC"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, strTransCode)
                '        Case "RGP"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
                '        Case "NRGP"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
                '        Case "PROD_ENTRY"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, strTransCode)
                '        Case "MCC-IISSUE"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, strTransCode)
                '        Case "FS-SH"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDispatchMultipleFreshSale, strTransCode)
                '        Case "PS-SH"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
                '        Case "Transfer"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                '        Case "ITransfer"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
                '        Case "BulkSRN"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, strTransCode)
                '        Case "MCC-MSRN"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, strTransCode)
                '        Case "SD-CSATRANS-RETURN"
                '            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
                '    End Select

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmMCCMilkLossGain & "'"))
            If clsCommon.myLen(fndLoc.Value) > 0 Then
                arrHeader.Add("Location : " + txtLocName.Text)
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
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
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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

    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs)
    '    ''Is_Section='N' and Is_Sub_Location='N' and
    '    Dim qry As String = " select Location_Code as CODE,Location_Desc as NAME,Main_Location_Code as [Main Location] from TSPL_LOCATION_MASTER where (( Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') ) "
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "CODE", "NAME", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    'End Sub

    Private Sub gv1_Click(sender As Object, e As EventArgs) Handles gv1.Click

    End Sub

    'Private Sub gv2_CellDoubleClick(sender As Object, e As GridViewCellEventArgs)
    '    Dim strTransType As String = clsCommon.myCstr(gv2.CurrentRow.Cells("Trans_Type").Value)
    '    Dim strTransCode As String = clsCommon.myCstr(gv2.CurrentRow.Cells("Source_Doc_No").Value)
    '    If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
    '        Select Case strTransType
    '            Case "IC-AD"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
    '            Case "ISSTRAN"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strTransCode)
    '            Case "SRN"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strTransCode)
    '            Case "SD-IN"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
    '            Case "Sale Return"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strTransCode)
    '            Case "SD-CSATRANS"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strTransCode)
    '            Case "SD-SH"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
    '            Case "CSA-SALE"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, strTransCode)
    '            Case "RICE-MIX"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceMixingEntry, strTransCode)
    '            Case "RICE-PROC"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceProcessingEntry, strTransCode)
    '            Case "PP_ISSUE"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, strTransCode)
    '            Case "PP_STDN"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strTransCode)
    '            Case "BulkSRNTrade"
    '                clsOpenTransactionForm.OpenTransacionForm(EnumTransType.BulkSRNTrade, strTransCode)
    '            Case "DispatchBSTrade"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strTransCode)
    '            Case "DispChallan"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strTransCode)
    '            Case "MilkTransferIn"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, strTransCode)
    '            Case "IC-AD"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
    '            Case "DispatchBS"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, strTransCode)
    '            Case "PRD_STG_PROC"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, strTransCode)
    '            Case "RGP"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
    '            Case "NRGP"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
    '            Case "PROD_ENTRY"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, strTransCode)
    '            Case "MCC-IISSUE"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, strTransCode)
    '            Case "FS-SH"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDispatchMultipleFreshSale, strTransCode)
    '            Case "PS-SH"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
    '            Case "Transfer"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
    '            Case "BulkSRN"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, strTransCode)
    '            Case "MCC-MSRN"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, strTransCode)
    '            Case "SD-CSATRANS-RETURN"
    '                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
    '        End Select

    '    End If
    'End Sub
    Sub LoadType()
        isInsideLoadData = True
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        'clsUserMgtCode.stockRecoNewJR()

        dr("Code") = "Summary"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Monthly"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Daily"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"
        cboType.SelectedValue = "Daily"
        isInsideLoadData = False
    End Sub


    'Sub LoadLocation()
    '    gvLocation.DataSource = Nothing

    '    ''===============when WIP then only section/sublocation seen in location finders=====24/03/2017==========================
    '    Dim whrCls As String = " and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') )"
    '    'If chkProd_WIP.Checked Then
    '    '    whrCls = " and location_code in (select Main_Location_Code from TSPL_LOCATION_MASTER where coalesce(Main_Location_Code,'')<>'') " ''" and (Is_Section='Y' or Is_Sub_Location='Y') "
    '    'End If
    '    ''====================================================================================================================

    '    Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""
    '    'If Not chkIncludeGIT.Checked Then
    '    '    qry += " and  TSPL_LOCATION_MASTER.GIT_Type<>'Y' "
    '    'End If
    '    If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.stockRecoNewJR) = CompairStringResult.Equal Then
    '        'qry = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME,Jobwork_Vendor,Is_Sub_Location from TSPL_LOCATION_MASTER where len(coalesce(Jobwork_Vendor,''))>0 and Is_Sub_Location='Y'"
    '        qry += " and TSPL_LOCATION_MASTER.Location_Code in (select distinct coalesce(Main_Location_Code,'') as Main_Location from tspl_location_master where len(coalesce(Main_Location_Code,''))>0 and len(coalesce(Jobwork_Vendor,''))>0) "
    '    End If
    '    qry += " order by Location_Code"
    '    gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

    '    gvLocation.Columns("SEL").ReadOnly = False
    '    gvLocation.Columns("SEL").Width = 30
    '    gvLocation.Columns("SEL").HeaderText = " "

    '    gvLocation.Columns("CODE").ReadOnly = True
    '    gvLocation.Columns("CODE").Width = 100
    '    gvLocation.Columns("CODE").HeaderText = "Code"

    '    gvLocation.Columns("NAME").ReadOnly = True
    '    gvLocation.Columns("NAME").Width = 200
    '    gvLocation.Columns("NAME").HeaderText = "Description"

    '    gvLocation.ShowGroupPanel = False
    '    gvLocation.AllowAddNewRow = False
    '    gvLocation.AllowColumnReorder = False
    '    gvLocation.AllowRowReorder = False
    '    gvLocation.EnableSorting = False
    '    gvLocation.ShowFilteringRow = True
    '    gvLocation.EnableFiltering = True
    '    gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '    gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    'End Sub

    'Private Sub gvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs)
    '    If clsCommon.myCBool(gvLocation.CurrentRow.Cells("SEL").Value) Then
    '        Dim frm As New FrmCategorySelect()
    '        frm.lvl = 3
    '        frm.strCode = clsCommon.myCstr(gvLocation.CurrentRow.Cells("CODE").Value)
    '        frm.arrIn = gvLocation.CurrentRow.Tag
    '        frm.ShowDialog()
    '        If Not frm.isCancel Then
    '            gvLocation.CurrentRow.Tag = frm.arrOut
    '        End If
    '    End If
    'End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        reset()
    End Sub
    Sub reset()
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing        
        'isLoad = True
        'txtFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        'txtToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        'isLoad = False
        txtToDate.ReadOnly = True
    End Sub
    Private Sub dtpFromDate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub

    Private Sub dtpFromDate_Leave(sender As Object, e As EventArgs) Handles txtFromDate.Leave
        SetToDate()
    End Sub
    Sub SetToDate()
        If Not isLoad Then
            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select the Location first")
                Exit Sub
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code =(select Location_Code  from TSPL_LOCATION_MASTER where Loc_Segment_Code='" & fndLoc.Value & "' and Location_Category='MCC' and Rejected_Type='N') ")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Payment Cycle found on current MCC/Location")
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    txtFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                    txtToDate.Value = txtFromDate.Value
                    Exit Sub
                End If
                txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)

                If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
                Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                If txtFromDate.Value.Month <> dtNxtPay.Month Then
                    txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, txtFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(txtFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                    txtFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    txtToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                    Exit Sub
                End If
                txtToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, txtFromDate.Value)
            End If
            ' End If
        End If
    End Sub

    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        fndLoc.Value = clsLocation.getFinder("Location_Category='MCC'", fndLoc.Value, isButtonClicked)
        txtLocName.Text = clsLocation.GetName(fndLoc.Value, Nothing)
        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
    End Sub
    Public Sub funPrint()
        Try

            Dim dt As DataTable = gv1.DataSource
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "RptLossGainReport", "MCC Milk Loss Gain", clsCommon.myCDate(txtFromDate.Value))
                frmCRV = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub
End Class
