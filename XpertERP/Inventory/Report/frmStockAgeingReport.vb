Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
'Ticket No  TEC/18/06/19-000547 ,Shelf Life wise report,Provide Shelf Life checkbox
Public Class frmStockAgeingReport
    Inherits FrmMainTranScreen
#Region "variables"
    Dim dtCategory As DataTable
    Dim isDataLoad As Boolean = False
    Public asofdt As Date
    Public cuttoff As Date
    Public strType As String
    Public arrLocation As ArrayList
    Public arrItem As ArrayList
    Public arrCategory As Array
    Dim IsDrillDown As Boolean = False
    Dim BackProcess As Boolean = False
    Dim stritemcode As String = String.Empty
#End Region
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmStockAgeingReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnBulkExp.Visible = MyBase.isQuickExportFlag
    End Sub
    Private Sub frmStockAgeingReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        'txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        SetFiscalYear()
        'txtFromDate.Text = clsCommon.GETSERVERDATE()
        'txtToDate.Text = clsCommon.GETSERVERDATE()
        cboType.Text = "Detail"
        LoadUnit()
        LoadLocation()
        RadGroupBox5.Visible = False
        'LoadCategory()
        LoadTransactionType()
        'LoadBucketType()
        LoadFatSnf()
        LoadAgeingColumns()
        LoadItemStatus()
        'cbFatSnf.SelectedIndex = 0
        'cbType.SelectedIndex = 0
        rbtnLocationAll.IsChecked = True
        rbtnCategoryAll.IsChecked = True
        radbtnBulkExp.Visible = False
        btnBack.Enabled = False
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing

        ''===============when WIP then only section/sublocation seen in location finders=====24/03/2017==========================
        Dim whrCls As String = " and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') )"

        ''====================================================================================================================
        ''richa agarwal UDL/02/11/18-000238
        If chkGITLocation.Checked = False Then
            whrCls += "  and isnull(TSPL_LOCATION_MASTER.GIT_Type,'') <>'Y'"
        End If
        ''----------------------
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""

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

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        'txtFromDate.Text = clsCommon.GETSERVERDATE()
        'txtToDate.Text = clsCommon.GETSERVERDATE()

        'LoadTransactionType()
        'LoadBucketType()
        'cboType.Text = "Detail"
        'cbFatSnf.SelectedIndex = 0
        'cbType.SelectedIndex = 0
        'txtLocation.arrValueMember = Nothing
        'txtLocation.arrDispalyMember = Nothing

        'txtItem.arrValueMember = Nothing
        'txtItem.arrDispalyMember = Nothing
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadGroupBox3.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage3
        chkGITLocation.Checked = False
        chkShelfLife.Checked = False
        IsDrillDown = False
        BackProcess = False
        btnBack.Enabled = False
    End Sub
    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where PROGRAM_CODE='" & clsUserMgtCode.frmInventoryAgeingReport & "'"))
            If txtItemType.arrDispalyMember IsNot Nothing AndAlso txtItemType.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Type : " + clsCommon.GetMulcallStringWithComma(txtItemType.arrDispalyMember))
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
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        GetData()
    End Sub
    Public Sub GetData()
        Try
            If txtFromDate.Value > txtToDate.Value Then
                clsCommon.MyMessageBoxShow("Cuttoff Date can't be greater than As on date")
                Exit Sub
            End If
            If DateDiff(DateInterval.Day, txtFromDate.Value, txtToDate.Value) < clsCommon.myCdbl(txtOver.Text) Then
                clsCommon.MyMessageBoxShow("Minimum difference between Cutoff Date and As on Date should be " & clsCommon.myCdbl(txtOver.Text) & "")
                Exit Sub
            End If
            clsCommon.ProgressBarShow()
            Dim dtLoc As New DataTable()
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")
            Dim Location As String = String.Empty
            Dim objFilter As New clsStockAgeingFilters
            objFilter.UOM_Code = cmbUnit.SelectedValue
            objFilter.arrLocation = New List(Of clsCode)
            objFilter.arrLoc = New ArrayList
            objFilter.CutOffDate = clsCommon.GetPrintDate(txtFromDate.Value, "dd-MMM-yyyy")
            objFilter.AsOnDate = clsCommon.GetPrintDate(txtToDate.Value, "dd-MMM-yyyy")
            'objFilter.arrTransaction = txtTransaction.arrValueMember
            'objFilter.arrItemGroup = txtItemGroup.arrValueMember
            objFilter.arrItem = txtItem.arrValueMember
            objFilter.ReportType = cboType.SelectedValue
            objFilter.arrItemType = txtItemType.arrValueMember
            objFilter.SelectLocation = rbtnLocationSelect.IsChecked
            objFilter.AgeingColumns = cboAgeingColumns.SelectedValue
            Dim arrBucket As New ArrayList
            arrBucket.Add(clsCommon.myCdbl(txt1.Text))
            arrBucket.Add(clsCommon.myCdbl(txt2.Text))
            arrBucket.Add(clsCommon.myCdbl(txt3.Text))
            arrBucket.Add(clsCommon.myCdbl(txt4th.Text))
            objFilter.arrAgeingBucket = arrBucket
            objFilter.InventoryType = cboInventoryType.SelectedValue
            objFilter.Item_Status = cboItemStatus.SelectedValue
            ''richa agarwal 14 Nov,2018
            If IsDrillDown = True AndAlso clsCommon.myLen(stritemcode) > 0 And clsCommon.CompairString(cboType.SelectedValue, "Detail") = CompairStringResult.Equal Then
                Dim arritem1 As New ArrayList
                arritem1.Add(stritemcode)
                objFilter.arrItem = arritem1
            End If
            '=============================================================

            For intLoc As Integer = 0 To gvLocation.RowCount - 1
                Dim loc As New clsCode
                '====================add by Monika26/03/2017==================
                If Not rbtnLocationSelect.IsChecked Then
                    loc.Sel = True
                Else
                    loc.Sel = gvLocation.Rows(intLoc).Cells("Sel").Value
                End If

                '============================================================
                loc.Code = gvLocation.Rows(intLoc).Cells("Code").Value
                loc.Desc = gvLocation.Rows(intLoc).Cells("Name").Value
                loc.arrOut = gvLocation.Rows(intLoc).Tag
                objFilter.arrLocation.Add(loc)
                If loc.Sel Then
                    objFilter.arrLoc.Add(loc.Code)
                End If
            Next
            objFilter.arrCategory = New List(Of clsCode)
            objFilter.SelectCategory = rbtnCategorySelect.IsChecked
            For intLoc As Integer = 0 To gvCategory.RowCount - 1
                Dim loc As New clsCode
                loc.Sel = gvCategory.Rows(intLoc).Cells("Sel").Value
                loc.Code = gvCategory.Rows(intLoc).Cells("Code").Value
                loc.Desc = gvCategory.Rows(intLoc).Cells("Name").Value
                objFilter.arrCategory.Add(loc)
            Next
            Dim qry As String = clsInventoryMovement.GetQryStockAgeing(objFilter, chkGITLocation.Checked, chkShelfLife.Checked)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                'gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dt
                For Each col As GridViewColumn In gv1.Columns
                    col.Width = 100
                Next
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                gv1.EnableFiltering = True
                gv1.ShowGroupedColumns = False
                gv1.ReadOnly = True
                gv1.ShowGroupPanel = False

                'Dim Bkt1 = ("0-" + clsCommon.myCstr(txt1.Text))
                'Dim Bkt2 = (clsCommon.myCstr(Integer.Parse(txt1.Text) + 1) + "-" + clsCommon.myCstr(txt2.Text))
                'Dim Bkt3 = (clsCommon.myCstr(Integer.Parse(txt2.Text) + 1) + "-" + clsCommon.myCstr(txt3.Text))
                'Dim Bkt4 = ("Over-" + clsCommon.myCstr(Integer.Parse(txt3.Text) + 1))                
                'gv1.Columns("BKT1").HeaderText = Bkt1
                'gv1.Columns("BKT2").HeaderText = Bkt2
                'gv1.Columns("BKT3").HeaderText = Bkt3
                'gv1.Columns("BKT4").HeaderText = Bkt4
                'gv1.Columns("Item Desc").Width = 200
                SetGridFormationOFGV1()
                RadPageView1.SelectedPage = RadPageViewPage2
                RadGroupBox3.Enabled = False
            Else
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                'clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Data Not Found")
            End If
            gv1.Tag = cboType.SelectedValue & cboAgeingColumns.SelectedValue
            FindAndRestoreGridLayout(Me, gv1)
            ''richa agarwal 14 Nov
            BackProcess = False
            IsDrillDown = False
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
        End Try
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

    'Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs)
    '    BulkExport("csv")
    'End Sub
    'Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs)
    '    BulkExport("xls")
    'End Sub

    Sub BulkExport(ByVal FormatType As String)
        Try
            clsCommon.ProgressBarPercentShow()
            clsCommon.ProgressBarPercentUpdate(0, "Generating query for the report..")
            Dim qry As String = ""

            clsCommon.ProgressBarPercentUpdate(10, "Query generated..starting Inventory Ageing export..")
            If cboType.SelectedValue = "Summary" Then
                transportSql.BulkExport("Summary", qry, "", FormatType)
            Else
                transportSql.BulkExport("Detail", qry, "", FormatType)

            End If

            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Data exported successfully")
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            'gv1.Columns(ii).IsVisible = False
            gv1.Columns(ii).Width = 125
        Next
        If clsCommon.CompairString(cboType.SelectedValue, "Summary") = CompairStringResult.Equal Then
            gv1.Columns("Item_Type").IsVisible = True
            gv1.Columns("Item_Type").Width = 100
            gv1.Columns("Item_Type").HeaderText = "Item Type"

            gv1.Columns("shelf_life").IsVisible = True
            gv1.Columns("shelf_life").Width = 50
            gv1.Columns("shelf_life").HeaderText = "Shelf Life"

            'gv1.Columns("Document_Date").IsVisible = True
            'gv1.Columns("Document_Date").Width = 100
            'gv1.Columns("Document_Date").HeaderText = "Document Date"

            gv1.Columns("Stock_Uom").IsVisible = True
            gv1.Columns("Stock_Uom").Width = 100
            gv1.Columns("Stock_Uom").HeaderText = "UOM"

            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 100
            gv1.Columns("Location_Code").HeaderText = "Location Code"

            gv1.Columns("Current_Qty").IsVisible = True
            gv1.Columns("Current_Qty").Width = 100
            gv1.Columns("Current_Qty").HeaderText = "Current Qty"

            gv1.Columns("Current_Value").IsVisible = True
            gv1.Columns("Current_Value").Width = 100
            gv1.Columns("Current_Value").HeaderText = "Current Value"

        ElseIf clsCommon.CompairString(cboType.SelectedValue, "Detail") = CompairStringResult.Equal Then
            If gv1.Columns.Contains("Item_Desc") = True Then
                gv1.Columns("Item_Desc").IsVisible = True
                gv1.Columns("Item_Desc").Width = 100
                gv1.Columns("Item_Desc").HeaderText = "Item Desc"
            End If

            gv1.Columns("Punching_Date").IsVisible = False
            gv1.Columns("Punching_Date").Width = 100
            gv1.Columns("Punching_Date").HeaderText = "Punching Date"

            gv1.Columns("Item_Type").IsVisible = True
            gv1.Columns("Item_Type").Width = 100
            gv1.Columns("Item_Type").HeaderText = "Item Type"

            gv1.Columns("shelf_life").IsVisible = True
            gv1.Columns("shelf_life").Width = 50
            gv1.Columns("shelf_life").HeaderText = "Shelf Life"

            If gv1.Columns.Contains("Item_Code") = True Then
                gv1.Columns("Item_Code").IsVisible = True
                gv1.Columns("Item_Code").Width = 100
                gv1.Columns("Item_Code").HeaderText = "Item Code"
            End If

            gv1.Columns("Trans_Type").IsVisible = False
            gv1.Columns("Trans_Type").Width = 100
            gv1.Columns("Trans_Type").HeaderText = "Transaction Type"

            gv1.Columns("Trans_Name").IsVisible = True
            gv1.Columns("Trans_Name").Width = 100
            gv1.Columns("Trans_Name").HeaderText = "Transaction Type"

            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 100
            gv1.Columns("Location_Code").HeaderText = "Location Code"

            gv1.Columns("Document_No").IsVisible = True
            gv1.Columns("Document_No").Width = 100
            gv1.Columns("Document_No").HeaderText = "Document No"

            gv1.Columns("Document_Date").IsVisible = True
            gv1.Columns("Document_Date").Width = 100
            gv1.Columns("Document_Date").HeaderText = "Document Date"

            gv1.Columns("Stock_Uom").IsVisible = True
            gv1.Columns("Stock_Uom").Width = 100
            gv1.Columns("Stock_Uom").HeaderText = "UOM"

            gv1.Columns("Current_Qty").IsVisible = True
            gv1.Columns("Current_Qty").Width = 100
            gv1.Columns("Current_Qty").HeaderText = "Current Qty"

            gv1.Columns("Current_Value").IsVisible = True
            gv1.Columns("Current_Value").Width = 100
            gv1.Columns("Current_Value").HeaderText = "Current Value"


            ElseIf clsCommon.CompairString(cboType.SelectedValue, "Actual Ageing") = CompairStringResult.Equal Then
                gv1.Columns("Punching_Date").IsVisible = False
                gv1.Columns("Punching_Date").Width = 100
                gv1.Columns("Punching_Date").HeaderText = "Punching Date"

                gv1.Columns("Item_Type").IsVisible = True
                gv1.Columns("Item_Type").Width = 100
                gv1.Columns("Item_Type").HeaderText = "Item Type"

                gv1.Columns("Location_Code").IsVisible = True
                gv1.Columns("Location_Code").Width = 100
                gv1.Columns("Location_Code").HeaderText = "Location Code"

                gv1.Columns("Item_Code").IsVisible = True
                gv1.Columns("Item_Code").Width = 100
                gv1.Columns("Item_Code").HeaderText = "Item Code"

                gv1.Columns("Item_Desc").IsVisible = True
                gv1.Columns("Item_Desc").Width = 100
                gv1.Columns("Item_Desc").HeaderText = "Item Desc"

                gv1.Columns("Document_Date").IsVisible = True
                gv1.Columns("Document_Date").Width = 100
                gv1.Columns("Document_Date").HeaderText = "Document Date"

                gv1.Columns("Stock_Uom").IsVisible = True
                gv1.Columns("Stock_Uom").Width = 100
                gv1.Columns("Stock_Uom").HeaderText = "UOM"

                gv1.Columns("Current_Qty").IsVisible = True
                gv1.Columns("Current_Qty").Width = 100
                gv1.Columns("Current_Qty").HeaderText = "Current Qty"

                gv1.Columns("Current_Value").IsVisible = True
                gv1.Columns("Current_Value").Width = 100
                gv1.Columns("Current_Value").HeaderText = "Current Value"

            End If



            If clsCommon.CompairString(cboAgeingColumns.SelectedValue, "Fat-SNG") = CompairStringResult.Equal Then
                gv1.Columns("Current_Fat_KG").IsVisible = True
                gv1.Columns("Current_Fat_KG").Width = 100
                gv1.Columns("Current_Fat_KG").HeaderText = "Current Fat KG"

                gv1.Columns("Current_Fat_Value").IsVisible = True
                gv1.Columns("Current_Fat_Value").Width = 100
                gv1.Columns("Current_Fat_Value").HeaderText = "Current Fat Value"

                gv1.Columns("Current_SNF_KG").IsVisible = True
                gv1.Columns("Current_SNF_KG").Width = 100
                gv1.Columns("Current_SNF_KG").HeaderText = "Current SNF KG"

                gv1.Columns("Current_SNF_Value").IsVisible = True
                gv1.Columns("Current_SNF_Value").Width = 100
                gv1.Columns("Current_SNF_Value").HeaderText = "Current Fat Value"

            End If

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            For Each dc As GridViewColumn In gv1.Columns
                If dc.Name.Contains("Qty") = True OrElse dc.Name.Contains("Value") = True OrElse dc.Name.Contains("Fat") = True OrElse dc.Name.Contains("SNF") = True Then
                    Dim item1 As New GridViewSummaryItem(dc.Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                End If
                If clsCommon.CompairString(cboAgeingColumns.SelectedValue, "Qty") = CompairStringResult.Equal Then
                    If dc.Name.Contains("Value") = True Then
                        dc.IsVisible = False
                    End If
                ElseIf clsCommon.CompairString(cboAgeingColumns.SelectedValue, "Value") = CompairStringResult.Equal Then
                    If dc.Name.Contains("Qty") = True Then
                        dc.IsVisible = False
                    End If
                End If
                If clsCommon.CompairString(cboType.SelectedValue, "Summary") = CompairStringResult.Equal Then
                    If dc.Name.Contains("Qty") = True Then
                        'dc.IsVisible = False
                    End If
                End If

            Next

            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            RadPageView1.SelectedPage = RadPageViewPage2
    End Sub


    Sub LoadTransactionType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))

        'Dim dr As DataRow = dt.NewRow()
        'dr("Code") = "Actual Ageing"
        'dt.Rows.Add(dr)

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Detail"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Summary"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"
        cboType.SelectedValue = "Detail"
    End Sub
    'Sub LoadBucketType()
    '    Dim dt As DataTable = New DataTable()
    '    dt.Columns.Add("Code", GetType(String))
    '    dt.Rows.Add("Outstanding")
    '    dt.Rows.Add("Out")
    '    cbType.DataSource = dt
    '    cbType.ValueMember = "Code"
    '    cbType.DisplayMember = "Code"
    'End Sub

    Sub LoadFatSnf()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("MI", "Milk")
        dt.Rows.Add("MP", "Milk Product")
        dt.Rows.Add("Other", "Other")
        dt.Rows.Add("All", "All")
        cboInventoryType.DataSource = dt
        cboInventoryType.ValueMember = "Code"
        cboInventoryType.DisplayMember = "Name"
        cboInventoryType.SelectedValue = "All"
    End Sub
    Sub LoadAgeingColumns()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("Qty", "Quantity")
        dt.Rows.Add("Value", "Value")
        dt.Rows.Add("Qty+Value", "Quantity and Value")
        'dt.Rows.Add("FAT-SNF", "FAT-SNF")
        'dt.Rows.Add("All", "All")
        cboAgeingColumns.DataSource = dt
        cboAgeingColumns.ValueMember = "Code"
        cboAgeingColumns.DisplayMember = "Name"
        cboAgeingColumns.SelectedValue = "Qty+Value"
    End Sub
    Sub LoadItemStatus()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("Active", "Active")
        dt.Rows.Add("Inactive", "Inactive")
        dt.Rows.Add("All", "All")
        cboItemStatus.DataSource = dt
        cboItemStatus.ValueMember = "Code"
        cboItemStatus.DisplayMember = "Name"
        cboItemStatus.SelectedValue = "Active"
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

    Private Sub txt1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt1.Focus()
            txt1.Select()
            Return
        End If
    End Sub

    Private Sub txt2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt2.Focus()
            txt2.Select()
            Return
        End If
    End Sub

    Private Sub txt3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (IsNumeric(e.KeyChar) = False And e.KeyChar <> CChar(ChrW(Keys.Back))) Then
            MsgBox("Only Numeric Values")
            e.KeyChar = ""
            txt3.Focus()
            txt3.Select()
            Return
        End If
    End Sub

    Private Sub txt3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txt4th.Text = Me.txt3.Text
    End Sub




    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs)
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Description from tspl_location_master where location_type='physical' order by location_code"
            'txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("Location", qry, "Code", "Description", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Try
            Dim qry As String = "select Item_Code as Code,Item_Desc as Description from tspl_item_master order by item_code"
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Item", qry, "Code", "Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtCategory__My_Click(sender As Object, e As EventArgs)
        Try
            Dim qry As String = "select Code ,Name as Description,Parent from ("
            qry += " select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE"
            qry += " union all"
            qry += " select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL"
            qry += " left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE"
            qry += " Union all"
            qry += " select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES"
            qry += " )xxx order by Sno"
            txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Category", qry, "Code", "Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txt4th_TextChanged_1(sender As Object, e As EventArgs) Handles txt4th.TextChanged
        txtOver.Text = txt4th.Text

    End Sub



    Private Sub cbType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs)
        txtItem.arrValueMember = Nothing
    End Sub

    Private Sub MyComboBox1_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs)

    End Sub

    Private Sub RadGroupBox3_Click(sender As Object, e As EventArgs) Handles RadGroupBox3.Click

    End Sub

    Private Sub rbtnLocationSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
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
    Private Sub txtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
        txtFiscalYear.Value = clsCommon.ShowSelectForm("rptACMGTlBal", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
        SetFiscalYear()
    End Sub
    Sub SetFiscalYear()
        txtFromDate.MinDate = New Date(2001, 4, 1)
        txtFromDate.MaxDate = New Date(3000, 12, 1)
        txtToDate.MinDate = txtFromDate.MinDate
        txtToDate.MaxDate = txtFromDate.MaxDate
        If clsCommon.myLen(txtFiscalYear.Value) > 0 Then
            Dim qry As String = " select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                txtFromDate.MinDate = clsCommon.myCDate(dt.Rows(0)("Start_Date"))
                txtFromDate.MaxDate = clsCommon.myCDate(dt.Rows(0)("End_Date"))
                txtToDate.MinDate = txtFromDate.MinDate
                txtToDate.MaxDate = txtFromDate.MaxDate

                txtFromDate.Value = txtFromDate.MinDate
                txtToDate.Value = txtFromDate.MaxDate
            End If
        Else
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddDays(-121)
            'If txtToDate.Value.Month >= 1 AndAlso txtToDate.Value.Month <= 3 Then
            '    txtFromDate.Value = New Date(txtToDate.Value.Year - 1, 4, 1)
            'Else
            '    txtFromDate.Value = New Date(txtToDate.Value.Year, 4, 1)
            'End If
        End If
    End Sub

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        Try
            Dim qry As String = "select ITEM_TYPE_CODE as Code,ITEM_TYPE_NAME as Description from TSPL_ITEM_TYPE_MASTER order by Id"
            txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemType", qry, "Code", "Description", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
        Catch ex As Exception
        End Try
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
    Private Sub rbtnCategoryAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
        RadButton6.Enabled = rbtnCategorySelect.IsChecked
        RadButton7.Enabled = rbtnCategorySelect.IsChecked
    End Sub
    Sub LoadUnit()
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("Code") = ""
        dr("Description") = "Select"
        dt.Rows.InsertAt(dr, 0)
        cmbUnit.DataSource = Nothing
        cmbUnit.DataSource = dt
        cmbUnit.DisplayMember = "Description"
        cmbUnit.ValueMember = "Code"
    End Sub

    Private Sub cboType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboType.SelectedValueChanged
        Try
            If clsCommon.CompairString(cboType.SelectedValue, "Actual Ageing") = CompairStringResult.Equal Then
                cboAgeingColumns.SelectedValue = "Qty+Value"
                'cboAgeingColumns.Enabled = False
                txtFiscalYear.Value = Nothing
                SetFiscalYear()
            Else
                cboAgeingColumns.Enabled = True
            End If
        Catch ex As Exception

        End Try
       
    End Sub

    Private Sub cboType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboType.SelectedIndexChanged

    End Sub

    Private Sub chkGITLocation_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkGITLocation.ToggleStateChanged
        Try
            LoadLocation()
        Catch ex As Exception

        End Try
    End Sub
    ''richa agarwal 14 Nov,2018 UDL/02/11/18-000238
    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If (e.Column Is gv1.Columns("Item Code") OrElse e.Column Is gv1.Columns("Item Desc")) And clsCommon.CompairString(cboType.SelectedValue, "Summary") = CompairStringResult.Equal Then
                cboType.SelectedValue = "Detail"
                stritemcode = clsCommon.myCstr(gv1.CurrentRow.Cells("Item Code").Value)
                btnBack.Enabled = True
                IsDrillDown = True
                GetData()
                BackProcess = False
                stritemcode = Nothing
            ElseIf (e.Column Is gv1.Columns("Item_Code") OrElse e.Column Is gv1.Columns("Item_Desc")) And clsCommon.CompairString(cboType.SelectedValue, "Detail") = CompairStringResult.Equal Then
                Dim itemcode As String = ""
                itemcode = clsCommon.myCstr(gv1.CurrentRow.Cells("Item_Code").Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmItemMasterRMOther, itemcode)
            ElseIf e.Column Is gv1.Columns("Document_No") And clsCommon.CompairString(cboType.SelectedValue, "Detail") = CompairStringResult.Equal Then
                DrillDown()
                btnBack.Enabled = False
                IsDrillDown = False
                BackProcess = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub DrillDown()
        Try
            If Gv1.CurrentRow.Index >= 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "BulkSRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "CRATE-REC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCrateReceviedDairySale, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "Disassembly") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAssembDis, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "DispChallan") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "FS-SH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "PS-SH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MCC-MSALE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "FS-SR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "PS-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturndairy, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "IC-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "ISSTRAN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MCC-MSRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MilkTransferIn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "NRGP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "RGP") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "PP_ISSUE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "PP_STD-FQC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ProcessProductionStandardizationFinalQC, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "PROD_ENTRY") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "SRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "SRN-RET") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.SRNReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "Transfer") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "Purchase Return") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "PRD_STG_PROC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MCC-MSR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "BulkSRNRet") = CompairStringResult.Equal Then
                    'No separate screen for display record
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "DisCanSale") = CompairStringResult.Equal Then
                    Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CanSale_Doc_No from TSPL_CANSALE_DISPATCH_HEAD where Document_No='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value) + "'"))
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmCanSale, clsCommon.myCstr(strDocNo))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "DispatchBS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "ScrapIn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "BulkSRNTrade") = CompairStringResult.Equal Then
                    'No separate screen for display record
                    'Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(tspl_bulk_milk_srn.Challan_No,'') AS Challan_No From tspl_bulk_milk_srn where tspl_bulk_milk_srn.srn_no='" + clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_No").Value) + "'"))
                    'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strDocNo)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "CSA-SALE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "CSA-SALEPATTI-RETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASalePattiReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "DispatchBSTrade") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm("", clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                    ''''''''''''''''''''''''''''''''''''
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "DispChallanRet") = CompairStringResult.Equal Then
                    'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCTankerDispatchReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                    'ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "DispChallan-RET") = CompairStringResult.Equal Then
                    '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.MCCDispatchReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "EX_SALE_IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "JWO-SRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MCC-AISSUE") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MCC-ARETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MCC-IISSUE") = CompairStringResult.Equal Then
                    'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MilkTransferInReturn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferInReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MilkTransferJobWork") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransfer, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MilkTransJWOReturn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MJ-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkSRN, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "M-PURRETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkPurchaseReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MS-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSaleRetrun, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "MT_SALE_IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "PP_STDN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "PROD_WR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "Prod-Scrap") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmWreckageBooking, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "Sale Return") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "SaleReturnBS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "SD-CSATRANS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "SD-CSATRANS-RETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells("Trans_Type").Value), "TRN-RET") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TransferReturn, clsCommon.myCstr(gv1.CurrentRow.Cells("Document_No").Value))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            cboType.SelectedValue = "Summary"
            IsDrillDown = False
            BackProcess = False
            GetData()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub

   
    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub BulkExcel_Click(sender As Object, e As EventArgs) Handles BulkExcel.Click
        BulkExport("xls")
    End Sub


    Private Sub BulkCSV_Click(sender As Object, e As EventArgs) Handles BulkCSV.Click
        BulkExport("csv")
    End Sub
End Class
