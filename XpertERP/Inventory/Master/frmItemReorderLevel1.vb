'-Updation By-[Pankaj Kumar Chaudhary]--Against Ticket--[BM00000002102]
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

Public Class frmItemReorderLevel1
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

#Region "Variables"
    Dim dtCategory As DataTable
    Dim dt As DataTable
    Dim isInsideLoadData As Boolean = False
    Dim qry As String = Nothing
    Dim whrCls As String = Nothing
    Dim Categorytype As ArrayList
    Dim Categoryvalues As ArrayList
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ItemReorderLevel)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "please select location", Me.Text)
            txtLocation.Focus()
            Return False
        End If

        If gv1.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No data found to save", Me.Text)
            gv1.Focus()
            Return False
        End If

        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.ItemReorderLevel, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim Arr As New List(Of clsItemReorderLevel)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells("Apply").Value) Then
                        Dim obj As New clsItemReorderLevel()
                        obj.Apply = IIf(grow.Cells("Apply").Value = True, "Y", "N")
                        obj.Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                        obj.Item_Description = clsCommon.myCstr(grow.Cells("Item Description").Value)
                        obj.Min_Level = clsCommon.myCdbl(grow.Cells("Min Level").Value)
                        obj.Min_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Min Level Tolerance").Value)
                        obj.Max_Level = clsCommon.myCdbl(grow.Cells("Max Level").Value)
                        obj.Max_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Max Level Tolerance").Value)
                        obj.Reorder_Level = clsCommon.myCdbl(grow.Cells("Reorder Level").Value)
                        obj.Reorder_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Reorder Level Tolerance").Value)
                        obj.Reorder_Qty = clsCommon.myCdbl(grow.Cells("Reorder Qty").Value)
                        obj.Unit_Code = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                        'obj.Location_Code = clsCommon.myCstr(grow.Cells("Location_Code").Value)
                        If obj.Reorder_Qty < obj.Max_Level AndAlso obj.Reorder_Qty > obj.Reorder_Level Then
                        Else
                            Throw New Exception("Re-order qty must be in between Max level and Re-order Level at row no. " + clsCommon.myCstr(grow.Index + 1))
                        End If
                        If clsCommon.myLen(txtLocation.Value) > 0 Then
                            obj.Location_Code = txtLocation.Value
                        Else
                            obj.Location_Code = ""
                        End If
                        Arr.Add(obj)
                    Else
                        Dim obj As New clsItemReorderLevel()
                        obj.Apply = IIf(grow.Cells("Apply").Value = True, "Y", "N")
                        obj.Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                        obj.Item_Description = clsCommon.myCstr(grow.Cells("Item Description").Value)
                        obj.Min_Level = clsCommon.myCdbl(grow.Cells("Min Level").Value)
                        obj.Min_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Min Level Tolerance").Value)
                        obj.Max_Level = clsCommon.myCdbl(grow.Cells("Max Level").Value)
                        obj.Max_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Max Level Tolerance").Value)
                        obj.Reorder_Level = clsCommon.myCdbl(grow.Cells("Reorder Level").Value)
                        obj.Reorder_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Reorder Level Tolerance").Value)
                        obj.Reorder_Qty = clsCommon.myCdbl(grow.Cells("Reorder Qty").Value)
                        obj.Unit_Code = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                        If clsCommon.myLen(txtLocation.Value) > 0 Then
                            obj.Location_Code = txtLocation.Value
                        Else
                            obj.Location_Code = ""
                        End If
                        Arr.Add(obj)
                    End If
                Next
                If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                    If (clsItemReorderLevel.SaveData(Arr)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        LoadDataAll()
                        'LoadData(txtItemType.arrValueMember)
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "No data found to save", Me.Text)
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal Itemtype As String)
        Try
            isInsideLoadData = True
            dt = clsItemReorderLevel.GetData(Itemtype)
            gv1.DataSource = dt
            If gv1.Rows.Count > 0 Then
                FormatGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub LoadDataLocation(ByVal Itemtype As String, ByVal strlocationCode As String)
        Try
            dt = clsItemReorderLevel.GetDataByLocation(Itemtype, strlocationCode)
            gv1.DataSource = dt
            If gv1.Rows.Count > 0 Then
                FormatGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGrid()
        For Each col As GridViewColumn In gv1.Columns
            col.Width = 100
            col.ReadOnly = True
        Next
        gv1.Columns("Apply").Width = 50
        gv1.Columns("Apply").ReadOnly = False
        gv1.Columns("Unit Code").ReadOnly = False
        gv1.Columns("Min Level").ReadOnly = False
        gv1.Columns("Min Level Tolerance").ReadOnly = False
        gv1.Columns("Max Level").ReadOnly = False
        gv1.Columns("Max Level Tolerance").ReadOnly = False
        gv1.Columns("Reorder Level").ReadOnly = False
        gv1.Columns("Reorder Level Tolerance").ReadOnly = False
        gv1.Columns("Reorder Qty").ReadOnly = False
        gv1.Columns("Location Code").IsVisible = False
        gv1.Columns("Location Code").VisibleInColumnChooser = False

    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Sub Reset()
        txtLocation.Value = ""
        txtlocdesc.Text = ""
        UnCheckedAll(gvCategory)
        gv1.DataSource = Nothing
    End Sub

    Private Sub frmItemReorderLevel1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub frmItemReorderLevel1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        LoadCategory()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
        gvCategory.Enabled = False
        chk_applyall.Checked = False
        rbtnCategoryAll.IsChecked = True
    End Sub

    Private Sub rmiExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExport.Click
        Try
            qry = "select TSPL_ITEM_REORDER_LEVEL_NEW.Item_Code AS [Item Code],TSPL_ITEM_REORDER_LEVEL_NEW.Uom_Code as [Unit Code],TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level AS [Min Level] , TSPL_ITEM_REORDER_LEVEL_NEW.Min_Level_Tollerence AS [Min Level Tolerance] , TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level AS [Max Level], TSPL_ITEM_REORDER_LEVEL_NEW.Max_Level_Tollerence AS [Max Level Tolerance], TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level AS [Reorder Level], TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Level_Tollerence AS [Reorder Level Tolerance], TSPL_ITEM_REORDER_LEVEL_NEW.Reorder_Qty AS [Reorder Qty],TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code AS [Location Code] from TSPL_ITEM_REORDER_LEVEL_NEW "
            dt = clsDBFuncationality.GetDataTable(qry)
            ListImpExpColumnsMandatory = New List(Of String)({"Item Code", "Min Level", "Max Level", "Reorder Level"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Item Code"})
            If dt.Rows.Count <= 0 Then
                qry = "  Select '' As [Item Code], '' AS [Unit Code] , '' AS [Min Level], 0 as [Min Level Tolerance], 0 as [Max Level], 0 as [Max Level Tolerance], 0 as [Reorder Level], 0 as [Reorder Level Tolerance],0 as [Reorder Qty], '' as [Location Code]"
                transportSql.ExporttoExcel(qry, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
            Else
                transportSql.ExporttoExcel(qry, " and TSPL_ITEM_REORDER_LEVEL_NEW.Apply='Y' and isnull(TSPL_ITEM_REORDER_LEVEL_NEW.Location_Code,'')<>''", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImport.Click
        Try
            ImportItems()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ImportItems()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Item Code", "Unit Code", "Min Level", "Min Level Tolerance", "Max Level", "Max Level Tolerance", "Reorder Level", "Reorder Level Tolerance", "Reorder Qty", "Location Code") Then
            clsCommon.ProgressBarShow()
            Try
                Dim Item_Code As String
                Dim Arr As New List(Of clsItemReorderLevel)
                Dim LineNo As String
                Dim locationCode As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    LineNo = clsCommon.myCstr(grow.Index + 1)
                    Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If (grow.Cells("Location Code").Value IsNot Nothing AndAlso clsCommon.myLen(grow.Cells("Location Code").Value) > 0) Then
                        locationCode = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    Else
                        locationCode = Nothing
                    End If

                    Dim obj As New clsItemReorderLevel()
                    obj.Item_Code = clsDBFuncationality.getSingleValue("Select Item_Code from TSPL_ITEM_MASTER WHERE Item_Code='" + Item_Code + "'")
                    ' Add by : Prabhakar Ticket Ref : BM00000009269
                    obj.Location_Code = clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + locationCode + "'")

                    If clsCommon.CompairString(obj.Item_Code, Item_Code) <> CompairStringResult.Equal Then
                        Throw New Exception("Item Code at line '" + LineNo + "' does not exist.")
                    End If
                    obj.Apply = "Y"
                    obj.Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    obj.Min_Level = clsCommon.myCdbl(grow.Cells("Min Level").Value)
                    obj.Min_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Min Level Tolerance").Value)
                    obj.Max_Level = clsCommon.myCdbl(grow.Cells("Max Level").Value)
                    obj.Max_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Max Level Tolerance").Value)
                    obj.Reorder_Level = clsCommon.myCdbl(grow.Cells("Reorder Level").Value)
                    obj.Reorder_Level_Tollerence = clsCommon.myCdbl(grow.Cells("Reorder Level Tolerance").Value)
                    obj.Item_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_item_master.item_desc from tspl_item_master where tspl_item_master.Item_Code='" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "'"))
                    obj.Location_Code = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    obj.Reorder_Qty = clsCommon.myCdbl(grow.Cells("Reorder Qty").Value)
                    obj.Unit_Code = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                    'obj.Location_Code = clsCommon.myCstr(grow.Cells("Location_Code").Value)
                    If obj.Min_Level <= 0 Then
                        Throw New Exception("Min Level must be greater than zero at row no. " + clsCommon.myCstr(grow.Index + 1))
                    End If
                    If obj.Max_Level <= 0 Then
                        Throw New Exception("Max Level must be greater than zero at row no. " + clsCommon.myCstr(grow.Index + 1))
                    End If
                    If obj.Reorder_Level <= 0 Then
                        Throw New Exception("Re-order Level must be greater than zero at row no. " + clsCommon.myCstr(grow.Index + 1))
                    End If
                    If obj.Reorder_Level < obj.Max_Level AndAlso obj.Reorder_Level > obj.Min_Level Then
                    Else
                        Throw New Exception("Re-order Level must be in between Max level and Re-order Level at row no. " + clsCommon.myCstr(grow.Index + 1))
                    End If

                    If obj.Reorder_Qty < obj.Max_Level AndAlso obj.Reorder_Qty > obj.Reorder_Level Then
                    Else
                        Throw New Exception("Re-order qty must be in between Max level and Re-order Level at row no. " + clsCommon.myCstr(grow.Index + 1))
                    End If

                    Dim qry As String = "select UOM_Code as Code from TSPL_ITEM_UOM_DETAIL where item_code='" + obj.Item_Code + "' and uom_code='" + obj.Unit_Code + "'"
                    Dim unitvalue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                    If clsCommon.myLen(unitvalue) <= 0 Then
                        Throw New Exception("Unit Code doesn't exists for item '" + obj.Item_Code + "' at row no. " + clsCommon.myCstr(grow.Index + 1))
                    End If
                    ' Add by : Prabhakar Ticket Ref : BM00000009269
                    'obj.Item_Category_Code = clsCommon.myCstr(grow.Cells("Item_Category_Code").Value)
                    'obj.Unit_Code = clsCommon.myCstr(grow.Cells("Unit_Code").Value)
                    Arr.Add(obj)
                Next
                If (clsItemReorderLevel.SaveData(Arr)) Then
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                End If
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                Throw New Exception(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        qry = "select TSPL_LOCATION_MASTER.Location_Code as Code ,TSPL_LOCATION_MASTER.Location_Desc as Description from TSPL_LOCATION_MASTER"
        txtLocation.Value = clsCommon.ShowSelectForm("CATREORDERLVLLoc", qry, "Code", "", txtLocation.Value, "Code", isButtonClicked)
        txtlocdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code ='" + txtLocation.Value + "'"))

        'LoadDataLocation(txtItemType.arrValueMember, clsCommon.myCstr(txtLocation.Value))

    End Sub

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        Try
            txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestoreco", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

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

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select location.", Me.Text)
                Exit Sub
            End If
            LoadDataAll()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
      
    End Sub

    Private Sub LoadDataAll()
        gv1.DataSource = Nothing
        GVcategorydata()
        Try
            dt = clsItemReorderLevel.GetDataAll(clsCommon.GetMulcallStringWithComma(txtItemType.arrValueMember).Replace(",", "','"), txtLocation.Value, Categorytype, Categoryvalues)
            gv1.DataSource = dt
            If gv1.Rows.Count > 0 Then
                FormatGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        If (Not isInsideLoadData) Then
            If e.Column Is gv1.Columns("Unit Code") Then
                OpenUOMList(False)
            End If
        End If
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Item Code").Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells("Unit Code").Value = clsCommon.ShowSelectForm("SRNItendnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells("Unit Code").Value), "Code", isButtonClick)
            
        End If
    End Sub

    Private Sub chk_applyall_CheckedChanged(sender As Object, e As EventArgs) Handles chk_applyall.CheckedChanged
        If chk_applyall.Checked = True Then
            For ii As Integer = 0 To gv1.RowCount - 1
                gv1.Rows(ii).Cells("Apply").Value = True
            Next
        Else
            For ii As Integer = 0 To gv1.RowCount - 1
                gv1.Rows(ii).Cells("Apply").Value = False
            Next
        End If
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
End Class
