Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.IO

'Create By Prabhakar , Ticket Ref : BM00000009269'
Public Class FrmItemReloadReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Categorytype As ArrayList
    Dim Categoryvalues As ArrayList
    Dim dtCategory As DataTable
    Dim qry As String
#End Region

    Private Sub frmItemReorderLevel1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'SetUserMgmtNew()
            LoadCategory()
            
            gvCategory.Enabled = False
            rbtnCategoryAll.IsChecked = True
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                btnprint.Visible = False
            ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                btnprint.Visible = True
            End If

            'ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestoreco", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub

    Sub LoadCategory()
        rbtnCategoryAll.IsChecked = True
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

    Private Sub GVcategorydata()
        Categorytype = New ArrayList()
        Categoryvalues = New ArrayList()
        Try
            If rbtnCategorySelect.IsChecked Then
                For ii As Integer = 0 To gvCategory.RowCount - 1
                    If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                        Categorytype.Add(gvCategory.Rows(ii).Cells("CODE").Value)
                        Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            For Each strInn As String In arr.Keys
                                Categoryvalues.Add(strInn)
                            Next
                        Else
                            Dim qry As String = " select TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE from TSPL_ITEM_CATEGORY_LEVEL_VALUES where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "'"
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                For Each dr As DataRow In dt1.Rows
                                    Categoryvalues.Add(clsCommon.myCstr(dr("CODE").ToString()))
                                Next
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Categorytype = Nothing
            Categoryvalues = Nothing
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Public Sub Load_Report()
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        gv.ShowGroupPanel = False
        gv.EnableFiltering = True
        GVcategorydata()
        Try
            Dim dt As New DataTable()
            Dim isapply As Integer = 2
            If rdbApplyTypeYes.IsChecked Then
                isapply = 1
            ElseIf rdbApplyTypeNo.IsChecked Then
                isapply = 0
            Else
                isapply = 2
            End If
            dt = clsItemReorderLevel.GetDataAll(clsCommon.GetMulcallStringWithComma(txtItemType.arrValueMember).Replace(",", "','"), Nothing, Categorytype, Categoryvalues, isapply, clsCommon.GetMulcallStringWithComma(tst_multiitemcode.arrValueMember).Replace(",", "','"), clsCommon.GetMulcallStringWithComma(tst_multilocation.arrValueMember).Replace(",", "','"))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt
                If gv.Rows.Count > 0 Then
                    FormatGrid()
                    ReStoreGridLayout()
                End If
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub
    Sub FormatGrid()
        gv.ReadOnly = True
        For Each col As GridViewColumn In gv.Columns
            col.Width = 100
        Next
        gv.Columns("Apply").Width = 50

        RadPageView1.SelectedPage = RadPageViewPage2
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Try
            RadGroupBox2.Enabled = True
            txtItemType.arrValueMember = Nothing
            UnCheckedAll(gvCategory)
            gv.DataSource = Nothing
            RadPageView1.SelectedPage = RadPageViewPage1
            rdbApplyTypeBoth.IsChecked = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If gv.RowCount > 0 Then

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                PrintData(objCommonVar.CurrentCompanyCode)
            Else
                PrintData() ' // Keep old function as it is.
            End If
        Else
            common.clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
            Exit Sub
        End If
    End Sub
    Private Sub PrintData(ByVal companyCode As String)
        GVcategorydata()
        Try
            Dim dt As New DataTable()
            Dim isapply As Integer = 2
            If rdbApplyTypeYes.IsChecked Then
                isapply = 1
            ElseIf rdbApplyTypeNo.IsChecked Then
                isapply = 0
            Else
                isapply = 2
            End If

            dt = clsItemReorderLevel.GetDataAll("UDL", clsCommon.GetMulcallStringWithComma(txtItemType.arrValueMember).Replace(",", "','"), Nothing, Categorytype, Categoryvalues, isapply, clsCommon.GetMulcallStringWithComma(tst_multiitemcode.arrValueMember).Replace(",", "','"), clsCommon.GetMulcallStringWithComma(tst_multilocation.arrValueMember).Replace(",", "','"))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptItemReorderLevel1", "crptItemReorderLevel1")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub PrintData()
        GVcategorydata()
        Try
            Dim dt As New DataTable()
            Dim isapply As Integer = 2
            If rdbApplyTypeYes.IsChecked Then
                isapply = 1
            ElseIf rdbApplyTypeNo.IsChecked Then
                isapply = 0
            Else
                isapply = 2
            End If

            dt = clsItemReorderLevel.GetDataAll(clsCommon.GetMulcallStringWithComma(txtItemType.arrValueMember).Replace(",", "','"), Nothing, Categorytype, Categoryvalues, isapply, clsCommon.GetMulcallStringWithComma(tst_multiitemcode.arrValueMember).Replace(",", "','"), clsCommon.GetMulcallStringWithComma(tst_multilocation.arrValueMember).Replace(",", "','"))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptItemReorderLevel1", "crptItemReorderLevel1")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow("No data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmItemReloadReport & "'"))
            If txtItemType.arrDispalyMember IsNot Nothing AndAlso txtItemType.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Type : " + clsCommon.GetMulcallStringWithComma(txtItemType.arrDispalyMember))
            End If
            If tst_multiitemcode.arrDispalyMember IsNot Nothing AndAlso tst_multiitemcode.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(tst_multiitemcode.arrDispalyMember))
            End If
            If tst_multilocation.arrDispalyMember IsNot Nothing AndAlso tst_multilocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(tst_multilocation.arrDispalyMember))
            End If


            If rbtnCategorySelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvCategory.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        If clsCommon.CompairString(strLoca, "") = CompairStringResult.Equal Then
                            strLoca += clsCommon.myCstr(grow.Cells("NAME").Value)
                        Else
                            strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                        End If
                    End If
                Next
                arrHeader.Add("Category : " + strLoca)
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
                clsCommon.MyExportToPDF("Item Reorder Level", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        If rbtnCategoryAll.IsChecked = True Then
            CheckedAll(gvCategory)
            gvCategory.Enabled = False
        Else
            UnCheckedAll(gvCategory)
            gvCategory.Enabled = True
        End If
    End Sub

    Private Sub tst_multiitemcode__My_Click(sender As Object, e As EventArgs) Handles tst_multiitemcode._My_Click
        Dim qry As String = Nothing
        qry = "select item_code as code ,item_desc as name from tspl_item_master"
        tst_multiitemcode.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestreco", qry, "Code", "Name", tst_multiitemcode.arrValueMember, tst_multiitemcode.arrDispalyMember)
    End Sub

    Private Sub tst_multilocation__My_Click(sender As Object, e As EventArgs) Handles tst_multilocation._My_Click
        Dim qry As String = Nothing
        qry = "select TSPL_LOCATION_MASTER.Location_Code as Code ,TSPL_LOCATION_MASTER.Location_Desc as Name from TSPL_LOCATION_MASTER"
        tst_multilocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTyreco", qry, "Code", "Name", tst_multilocation.arrValueMember, tst_multilocation.arrDispalyMember)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
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
End Class
