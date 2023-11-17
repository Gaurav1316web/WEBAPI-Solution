Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports Microsoft.Office.Interop


Public Class FrmMeterialStockReco
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrLoc As Dictionary(Of String, Object) = Nothing
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
    Private Sub FrmMeterialStockReco_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE
        '================================================
        arrBack = New List(Of String)

        SetUserMgmtNew()
        LoadCategory()
        LoadLocation()


        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnCategoryAll.IsChecked = True
        rbtnLocationAll.IsChecked = True
  

       
        If isDataLoad Then
            txtFromDate.Value = dtFrom
            txtToDate.Value = dtTo
       
          
            txtItemGroup.arrValueMember = arrItemGroup
            If arrLoc IsNot Nothing AndAlso arrLoc.Count > 0 Then
                rbtnLocationSelect.IsChecked = True
                For Each str As String In arrLoc.Keys
                    For ii As Integer = 0 To gvLocation.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvLocation.Rows(ii).Cells("SEL").Value = True
                            gvLocation.Rows(ii).Tag = arrLoc(str)
                        End If
                    Next
                Next
            End If
            If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
                rbtnCategorySelect.IsChecked = True
                For Each str As String In arrCat.Keys
                    For ii As Integer = 0 To gvCategory.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvCategory.Rows(ii).Cells("SEL").Value = True
                            gvCategory.Rows(ii).Tag = arrCat(str)
                        End If
                    Next
                Next
            End If
          

        End If
      
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') ) "
     
        qry += " order by Location_Code"
        gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvLocation.Columns("SEL").ReadOnly = False
        gvLocation.Columns("SEL").Width = 30
        gvLocation.Columns("SEL").HeaderText = " "

        gvLocation.Columns("CODE").ReadOnly = True
        gvLocation.Columns("CODE").Width = 100
        gvLocation.Columns("CODE").HeaderText = "Code"

        gvLocation.Columns("NAME").ReadOnly = True
        gvLocation.Columns("NAME").Width = 200
        gvLocation.Columns("NAME").HeaderText = "Description"

        gvLocation.ShowGroupPanel = False
        gvLocation.AllowAddNewRow = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.ShowFilteringRow = True
        gvLocation.EnableFiltering = True
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    End Sub

  


    

    Sub LoadCategory()
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub

   

    Private Sub SetUserMgmtNew()

        MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        btnExport.Visible = MyBase.isExport
        btnQuickExport.Visible = MyBase.isExport
     
    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

   
    


    'Private Sub ReStoreGridLayout()
    '    Try
    '        ' Dim ReportID As String = GetReportID()
    '        If clsCommon.myLen(ReportID) > 0 Then
    '            Dim obj As clsGridLayout = New clsGridLayout()
    '            obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
    '            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
    '                Dim ii As Integer
    '                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
    '                    gv1.Columns(ii).IsVisible = False
    '                    gv1.Columns(ii).VisibleInColumnChooser = True
    '                Next
    '                gv1.LoadLayout(obj.GridLayout)
    '                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '            End If
    '        End If
    '    Catch err As Exception
    '        common.clsCommon.MyMessageBoxShow(err.Message)
    '    End Try
    'End Sub


    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)


            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If


            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Location : " + strLoca)
            End If




            If rbtnCategorySelect.IsChecked Then
                'Dim strLoca As String = ""
                'For Each Str As RadTreeNode In tvCategory.CheckedNodes
                '    If clsCommon.myLen(strLoca) > 0 Then
                '        strLoca += ", "
                '    End If
                '    strLoca += Str.Text
                'Next
                'arrHeader.Add("Category : " + strLoca)
            End If
       
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try

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
        RadPageView1.SelectedPage = RadPageViewPage1
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = txtToDate.Value
        'ChkMRPWise.Checked = False
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        arrBack = New List(Of String)
        RadPageViewPage2.Text = "Report"

    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        RadGroupBox3.Enabled = val
        txtItem.Enabled = val
        RadGroupBox2.Enabled = val
        txtTransaction.Enabled = val
     
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged, rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub


    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
        RadButton6.Enabled = rbtnCategorySelect.IsChecked
        RadButton7.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    

    'Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSett1.Click
    '    Dim ReportID As String = GetReportID()
    '    If clsCommon.myLen(ReportID) > 0 Then
    '        gv1.MasterTemplate.FilterDescriptors.Clear()
    '        Dim obj As New clsGridLayout()
    '        obj.ReportID = ReportID
    '        obj.UserID = objCommonVar.CurrentUserCode
    '        obj.GridLayout = New MemoryStream()
    '        gv1.SaveLayout(obj.GridLayout)
    '        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '        obj.GridColumns = gv1.ColumnCount
    '        If obj.SaveData() Then
    '            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
    '        End If
    '    End If
    'End Sub

    Private Sub TreeView_NodeCheckedChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.TreeNodeCheckedEventArgs)
        TreeCheckBoxes(e.Node, e.Node.Checked)
    End Sub

    Public Sub TreeCheckBoxes(ByVal CurrentNode As RadTreeNode, ByVal val As Boolean)
        For Each Ctr As RadTreeNode In CurrentNode.Nodes
            Ctr.Checked = val
            TreeCheckBoxes(Ctr, val)
        Next
    End Sub



    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Private Sub gvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick
        If clsCommon.myCBool(gvLocation.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 3
            frm.strCode = clsCommon.myCstr(gvLocation.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvLocation.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvLocation.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

   


    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub txtItemGroup__My_Click(sender As Object, e As EventArgs) Handles txtItemGroup._My_Click
        Dim qry As String = " select SNo,Value,Description as Name,Custom_Field_Code as [Code] from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "' "
        txtItemGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemGroupMulSel", qry, "Value", "Name", txtItemGroup.arrValueMember, txtItemGroup.arrDispalyMember)
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
    End Sub

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

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub

    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        CheckedAll(gvCategory)
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        UnCheckedAll(gvCategory)
    End Sub

    

    'Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
    '    Try
    '        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal Then
    '            If Not arrBack.Contains("Item Type Wise Summary") Then
    '                arrBack.Add("Item Type Wise Summary")
    '            End If
    '            cboType.SelectedValue = "Item Group Wise Summary"
    '            arrItemType = New ArrayList()
    '            arrItemType = txtItemType.arrValueMember()

    '            Dim tmp As New ArrayList()
    '            tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Type").Value))
    '            txtItemType.arrValueMember = tmp
    '            LoadData(0)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal Then
    '            If Not arrBack.Contains("Item Group Wise Summary") Then
    '                arrBack.Add("Item Group Wise Summary")
    '            End If
    '            cboType.SelectedValue = "Category Wise Summary"
    '            arrItemGroup = New ArrayList()
    '            arrItemGroup = txtItemGroup.arrValueMember
    '            Dim tmp As New ArrayList()
    '            tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Group").Value))
    '            txtItemGroup.arrValueMember = tmp
    '            LoadData(0)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Category Wise Summary") = CompairStringResult.Equal Then
    '            If Not arrBack.Contains("Category Wise Summary") Then
    '                arrBack.Add("Category Wise Summary")
    '            End If
    '            cboType.SelectedValue = "Item Wise Summary"
    '            arrCat = Nothing
    '            If rbtnCategorySelect.IsChecked Then
    '                arrCat = New Dictionary(Of String, Object)
    '                For ii As Integer = 0 To gvCategory.RowCount - 1
    '                    If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
    '                        arrCat.Add(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), gvCategory.Rows(ii).Tag)
    '                    End If
    '                Next
    '            End If
    '            UnCheckedAll(gvCategory)
    '            Dim arrCatTemp As New Dictionary(Of String, Object)
    '            For Each dr As DataRow In dtCategory.Rows
    '                If clsCommon.myLen(gv1.CurrentRow.Cells(clsCommon.myCstr(dr("CodeColumn"))).Value) > 0 Then
    '                    Dim arrIn As New Dictionary(Of String, Object)
    '                    arrIn.Add(gv1.CurrentRow.Cells(clsCommon.myCstr(dr("CodeColumn"))).Value, Nothing)
    '                    arrCatTemp.Add(clsCommon.myCstr(dr("CodeColumn")), arrIn)
    '                End If
    '            Next
    '            If arrCatTemp IsNot Nothing AndAlso arrCatTemp.Count > 0 Then
    '                rbtnCategorySelect.IsChecked = True
    '                For Each str As String In arrCatTemp.Keys
    '                    For ii As Integer = 0 To gvCategory.RowCount - 1
    '                        If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
    '                            gvCategory.Rows(ii).Cells("SEL").Value = True
    '                            gvCategory.Rows(ii).Tag = arrCatTemp(str)
    '                        End If
    '                    Next
    '                Next
    '            End If
    '            LoadData(0)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Wise Summary") = CompairStringResult.Equal Then
    '            If Not arrBack.Contains("Item Wise Summary") Then
    '                arrBack.Add("Item Wise Summary")
    '            End If
    '            cboType.SelectedValue = "Item And Location Wise Summary"
    '            arrItem = New ArrayList()
    '            arrItem = txtItem.arrValueMember
    '            Dim tmp As New ArrayList()
    '            tmp.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Code").Value))
    '            txtItem.arrValueMember = tmp
    '            LoadData(0)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Location Wise Summary") = CompairStringResult.Equal Then
    '            If Not arrBack.Contains("Item And Location Wise Summary") Then
    '                arrBack.Add("Item And Location Wise Summary")
    '            End If
    '            cboType.SelectedValue = "Document Wise Detail"
    '            arrLoc = Nothing
    '            If rbtnLocationSelect.IsChecked Then
    '                arrLoc = New Dictionary(Of String, Object)
    '                For ii As Integer = 0 To gvLocation.RowCount - 1
    '                    If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
    '                        arrLoc.Add(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), gvLocation.Rows(ii).Tag)
    '                    End If
    '                Next
    '            End If
    '            UnCheckedAll(gvLocation)
    '            rbtnLocationSelect.IsChecked = True
    '            Dim arrInn As New Dictionary(Of String, Object)
    '            arrInn.Add(clsCommon.myCstr(gv1.CurrentRow.Cells("Location_Code").Value), Nothing)
    '            For ii As Integer = 0 To gvLocation.RowCount - 1
    '                If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Main_Location_Code").Value)) = CompairStringResult.Equal Then
    '                    gvLocation.Rows(ii).Cells("SEL").Value = True
    '                    gvLocation.Rows(ii).Tag = IIf(clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Location_Code").Value), clsCommon.myCstr(gv1.CurrentRow.Cells("Main_Location_Code").Value)) = CompairStringResult.Equal, Nothing, arrInn)
    '                End If
    '            Next
    '            LoadData(0)
    '        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail Ledger") = CompairStringResult.Equal Then
    '            Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value)
    '            Dim strTransCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Source_Doc_No").Value)
    '            If clsCommon.myLen(strTransType) > 0 AndAlso clsCommon.myLen(strTransCode) > 0 Then
    '                Select Case strTransType
    '                    Case "IC-AD"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
    '                    Case "ISSTRAN"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, strTransCode)
    '                    Case "SRN"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, strTransCode)
    '                    Case "SD-IN"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
    '                    Case "Sale Return"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strTransCode)
    '                    Case "SD-CSATRANS"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, strTransCode)
    '                    Case "SD-SH"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
    '                    Case "CSA-SALE"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, strTransCode)
    '                    Case "RICE-MIX"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceMixingEntry, strTransCode)
    '                    Case "RICE-PROC"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmRiceProcessingEntry, strTransCode)
    '                    Case "PP_ISSUE"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, strTransCode)
    '                    Case "PP_STDN"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, strTransCode)
    '                    Case "BulkSRNTrade"
    '                        clsOpenTransactionForm.OpenTransacionForm(EnumTransType.BulkSRNTrade, strTransCode)
    '                    Case "DispatchBSTrade"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strTransCode)
    '                    Case "DispChallan"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, strTransCode)
    '                    Case "MilkTransferIn"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, strTransCode)
    '                    Case "IC-AD"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, strTransCode)
    '                    Case "DispatchBS"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, strTransCode)
    '                    Case "PRD_STG_PROC"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, strTransCode)
    '                    Case "RGP"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
    '                    Case "NRGP"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, strTransCode)
    '                    Case "PROD_ENTRY"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, strTransCode)
    '                    Case "MCC-IISSUE"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, strTransCode)
    '                    Case "FS-SH"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmDispatchMultipleFreshSale, strTransCode)
    '                    Case "PS-SH"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, strTransCode)
    '                    Case "Transfer"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, strTransCode)
    '                    Case "BulkSRN"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, strTransCode)
    '                    Case "MCC-MSRN"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, strTransCode)
    '                    Case "SD-CSATRANS-RETURN"
    '                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, strTransCode)
    '                End Select

    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub chkIncludeGIT_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        LoadLocation()
    End Sub


   




   
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

    Private Sub cboType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs)
        'SetBulkExport()
    End Sub

    Private Sub cboType_SelectedValueChanged(sender As Object, e As EventArgs)
        'SetBulkExport()
    End Sub

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

  
End Class
