Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports XpertERPEngine

Public Class FrmspareStockReport2


    Private Sub FrmspareStockReport2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadItemMaster()
        LoadCategory()
        LoadData()
    End Sub
    Sub LoadItemMaster()
        Dim qry As String = "select Item_Code ,Item_Desc  from tspl_item_master "
        cbgItemMaster.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItemMaster.ValueMember = "Item_Code"
        cbgItemMaster.DisplayMember = "Item_Desc"
    End Sub
    Sub LoadData()


        Dim qry As String
        Try
            If chkItemSelect.IsChecked And cbgItemMaster.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast One Item or Check All.")
            End If
            qry = " Select ROW_NUMBER() over(order by [Item_Code ]) as [SL NO],a.* from  (select TSPL_ITEM_MASTER.Item_Code, MAX(TSPL_ITEM_MASTER.Item_Desc) as SparePartsName,max(TSPL_ITEM_MASTER.Unit_Code )as Unit_Code, MAX(DESCRIPTION) as Model, SUM(Case When TSPL_INVENTORY_MOVEMENT.InOut='I' Then TSPL_INVENTORY_MOVEMENT.Qty Else TSPL_INVENTORY_MOVEMENT.Qty*-1 End) as Qty,max(Stock_Qty) as Req from (Select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE, TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION from TSPL_ITEM_CATEGORY_STRUCT_DETAIL LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE WHERE TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE='Model') XXX RIGHT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Category_Struct_Code=XXX.ITEM_CATEGORY_CODE LEFT OUTER JOIN TSPL_INVENTORY_MOVEMENT ON TSPL_INVENTORY_MOVEMENT.Item_Code=TSPL_ITEM_MASTER.Item_Code WHERE TSPL_ITEM_MASTER.Item_Type='R' Group By TSPL_ITEM_MASTER.Item_Code)a"

            If chkItemSelect.IsChecked And cbgItemMaster.CheckedValue.Count > 0 Then
                qry += " and  tspl_item_master.Item_Code in ( " + clsCommon.GetMulcallString(cbgItemMaster.CheckedValue) + ") "
            End If

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            End If
            gv1.DataSource = dt
            RadPageView1.Visible = True
            RadPageView1.SelectedPage = RadPageViewPage1
            chkItemAll.IsChecked = True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        SetGridFormationOFgv()

    End Sub
    Sub SetGridFormationOFgv()
        Try

            LoadData()
            ' Dim strItemCode, head2 As String
            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next
            gv1.Columns("SL NO").IsVisible = True
            gv1.Columns("SL NO").Width = 50
            gv1.Columns("SL NO").HeaderText = "S.NO."

            gv1.Columns("SparePartsName").IsVisible = True
            gv1.Columns("SparePartsName").Width = 300
            gv1.Columns("SparePartsName").HeaderText = "Spare Parts Name"

            gv1.Columns("Model").IsVisible = True
            gv1.Columns("Model").Width = 100
            gv1.Columns("Model").HeaderText = "Model"

            gv1.Columns("Unit_Code").IsVisible = True
            gv1.Columns("Unit_Code").Width = 70
            gv1.Columns("Unit_Code").HeaderText = "Units"

            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").Width = 80
            gv1.Columns("Qty").HeaderText = "Stock"

            gv1.Columns("Req").IsVisible = True
            gv1.Columns("Req").Width = 100
            gv1.Columns("Req").HeaderText = "Req"

            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
    End Sub
    Sub LoadCategory()
        Dim qry As String = " select Code,Name,Parent from ( select ITEM_CATEGORY_STRUCT_CODE as Code,DESCRIPTION as Name, null as Parent,0 as Sno from TSPL_ITEM_CATEGORY_STRUCTURE "
        qry += "union all select TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE as Code,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as Name,ITEM_CATEGORY_STRUCT_CODE as Parent,TSPL_ITEM_CATEGORY_STRUCT_DETAIL.CATEGORY_LEVEL as SNo from TSPL_ITEM_CATEGORY_STRUCT_DETAIL left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE=TSPL_ITEM_CATEGORY_STRUCT_DETAIL.ITEM_CATEGORY_CODE "
        qry += "Union all select CODE,DESCRIPTION as Name,ITEM_CATEGORY_CODE as Parent,100 as SNo from TSPL_ITEM_CATEGORY_LEVEL_VALUES "
        qry += ")xxx WHERE xxx.Parent ='Model' order by Sno "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        tvCategory.DataSource = Nothing
        tvCategory.TreeViewElement.AutoSizeItems = True
        tvCategory.ShowLines = True
        tvCategory.ShowRootLines = True
        tvCategory.TreeViewElement.ViewElement.Margin = New Padding(4)
        tvCategory.ShowExpandCollapse = True
        tvCategory.TreeIndent = 15
        tvCategory.FullRowSelect = False
        tvCategory.ShowLines = True
        tvCategory.LineStyle = TreeLineStyle.Dot
        tvCategory.LineColor = Color.FromArgb(110, 153, 210)
        tvCategory.ExpandAnimation = ExpandAnimation.Opacity
        tvCategory.AllowEdit = False
        tvCategory.ShowRootLines = False
        tvCategory.TreeViewElement.AllowAlternatingRowColor = True
        tvCategory.TreeViewElement.AlternatingRowColor = Color.AliceBlue

        tvCategory.TreeViewElement.DrawBorder = True
        tvCategory.ValueMember = "Code"
        tvCategory.DisplayMember = "Name"
        tvCategory.ChildMember = "Code"
        tvCategory.ParentMember = "Parent"
        tvCategory.DataSource = dt
        tvCategory.CheckBoxes = True

        tvCategory.ExpandAll()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = ""
            arrHeader.Add(strtemp)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Spare Stock Report", gv1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Spare Stock Report", gv1, arrHeader, "Spare Stock Report", True)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click

    End Sub

    Private Sub RadMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem6.Click
        'If clsCommon.myLen(GetReportID()) > 0 Then
        '    gv1.MasterTemplate.FilterDescriptors.Clear()
        '    Dim obj As New clsGridLayout()
        '    obj.ReportID = GetReportID()
        '    obj.UserID = objCommonVar.CurrentUserCode
        '    obj.GridLayout = New MemoryStream()
        '    gv1.SaveLayout(obj.GridLayout)
        '    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        '    obj.GridColumns = gv1.ColumnCount
        '    If obj.SaveData() Then
        '        common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
        '    End If
        'End If
    End Sub
End Class
