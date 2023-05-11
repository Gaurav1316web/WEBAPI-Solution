Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports System.IO
Imports System.Xml
Public Class frmVendorItemChargeMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Const colRMCode As String = "colRMCode"
    Const colRMName As String = "colRMName"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colIUOM As String = "colIUOM"
    Const colCharges As String = "COLCHARGES"
    Private isNewEntry As Boolean = True
#End Region
    Private Sub frmVendorItemChargeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New ")
        funReset()
        gv1.Enabled = True
        LoadCategory()
        btnShowHistory.Visible = False

    End Sub
    Private Sub frmVendorItemChargeMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub
    Private Sub txtPriceCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPriceCode._MYValidating
        Try
            Dim qry As String = "SELECT Code,[Description] as [Description] FROM TSPL_JOB_OUTWARD_PRICE_MASTER "
            txtPriceCode.Value = clsCommon.ShowSelectForm("JWPrice", qry, "Code", "", txtPriceCode.Value, "", isButtonClicked)
            lblPriceCode.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_JOB_OUTWARD_PRICE_MASTER where code='" & txtPriceCode.Value & "'")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoText As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "RM Item Code"
        repoText.Name = colRMCode
        repoText.HeaderImage = Global.XpertERPJobWorkOutward.My.Resources.Resources.search4
        repoText.TextImageRelation = TextImageRelation.TextBeforeImage
        repoText.Width = 100
        gv1.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "RM Item Description"
        repoText.Name = colRMName
        repoText.Width = 150
        repoText.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoText)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "FG Item Code"
        repoICode.Name = colICode
        'repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode) '3

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "FG Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoIUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIUOM.FormatString = ""
        repoIUOM.HeaderText = "Item UOM"
        repoIUOM.Name = colIUOM
        repoIUOM.Width = 150
        gv1.MasterTemplate.Columns.Add(repoIUOM)

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Charges"
        repoPendingQty.Name = colCharges
        repoPendingQty.Minimum = 0
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AutoSizeRows = False
        gv1.AllowRowReorder = True
        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Sub funReset()
        txtDate.Value = clsCommon.GETSERVERDATE()
        dtpEndDate.Value = clsCommon.GETSERVERDATE()
        dtStartDate.Value = clsCommon.GETSERVERDATE()
        txtCode.Value = ""
        cboItemType.SelectedValue = ""
        txtPriceCode.Value = ""
        lblPriceCode.Text = ""
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        UsLock1.Status = ERPTransactionStatus.Pending
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
        gv1.Rows.AddNew()
        cboItemType.SelectedValue = "F"
        dtpEndDate.Checked = False
        gv.DataSource = Nothing

    End Sub


    Sub LoadCategory()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "Finished Goods"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Semi-Finished"
        dt.Rows.Add(dr)

        cboItemType.DataSource = dt
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"

    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If e.Column Is gv1.Columns(colRMCode) Then
                    OpenRMCodeList(False)
                ElseIf e.Column Is gv1.Columns(colICode) Then
                    OpenICodeList(False)
                ElseIf e.Column Is gv1.Columns(colIUOM) Then
                    OpenUOMList(False)
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenRMCodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colRMCode).Value), "", True, isButtonClick, "", "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colRMCode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colRMName).Value = obj.Item_Desc
        Else
            SetBlankOfItemColumns()
        End If
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colIUOM).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colIUOM).Value), "Code", isButtonClick)
        End If
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Item Type")
            SetBlankOfItemColumns()
            cboItemType.Focus()
            Exit Sub
        End If
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), True, isButtonClick, "", "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colIUOM).Value = obj.Unit_Code

        Else
            SetBlankOfItemColumns()
        End If
    End Sub
    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colRMCode).Value = ""
        gv1.CurrentRow.Cells(colRMName).Value = ""
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colIUOM).Value = ""
        gv1.CurrentRow.Cells(colCharges).Value = 0
    End Sub

    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        funReset()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub
    Private Function SavingData(ByVal ChekBtnPost As Boolean) As Boolean
        If (SaveData(False)) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SaveData(ByVal isDoAbandomentNo As Boolean, Optional ByVal isPOCancel As Boolean = False) As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsVendorItemChargeMaster()
                obj.StartDate = dtStartDate.Value
                If dtpEndDate.Checked Then
                    obj.EndDate = dtpEndDate.Value
                End If
                obj.ItemType = cboItemType.SelectedValue
                obj.Vendor_Price_Code = txtPriceCode.Value
                obj.Code = txtCode.Value
                obj.CreatedDate = txtDate.Value
                obj.Arr = New List(Of clsVendorItemChargeDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsVendorItemChargeDetail()
                    objTr.RM_Item_Code = clsCommon.myCstr(grow.Cells(colRMCode).Value)
                    objTr.ItemCode = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.ItemDesc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.ItemUom = clsCommon.myCstr(grow.Cells(colIUOM).Value)
                    objTr.ItemCharge = clsCommon.myCdbl(grow.Cells(colCharges).Value)
                    If (clsCommon.myLen(objTr.ItemCode) > 0 AndAlso clsCommon.myLen(objTr.RM_Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    Return False
                End If

                Dim isSaved As Boolean = True
                isSaved = isSaved AndAlso obj.SaveData(obj, isNewEntry, isDoAbandomentNo)
                txtCode.Value = obj.Code
                LoadData(txtCode.Value, NavigatorType.Current)
                Return isSaved
            Else
                Return False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return False
    End Function
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtPriceCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Price Code")
                txtPriceCode.Focus()
                Return False
            End If

            Dim arrString As New List(Of String)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strRMCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRMCode).Value)
                Dim strFGCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                If clsCommon.myLen(strRMCode) > 0 AndAlso clsCommon.myLen(strFGCode) > 0 Then
                    Dim strRMFG As String = strRMCode + "#$#" + strFGCode
                    If arrString.Contains(strRMFG) Then
                        Throw New Exception("RM Item [" + strRMCode + "] and FG Item [" + strFGCode + "] is Repeating")
                    Else
                        arrString.Add(strRMFG)
                    End If
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colCharges).Value) <= 0 Then
                        Throw New Exception("Cost is not defined for RM Item [" + strRMCode + "] and FG Item [" + strFGCode + "]")
                    End If
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True

            Dim obj As New clsVendorItemChargeMaster()
            obj = clsVendorItemChargeMaster.GetData(strCode, NavTyep, "")

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then

                btnSave.Enabled = True
                btnPost.Enabled = True
                isNewEntry = False

                txtPriceCode.Value = obj.Vendor_Price_Code
                lblPriceCode.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_JOB_OUTWARD_PRICE_MASTER where code='" & txtPriceCode.Value & "'")
                dtStartDate.Value = obj.StartDate
                If obj.EndDate IsNot Nothing Then
                    dtpEndDate.Checked = True
                    dtpEndDate.Value = obj.EndDate
                Else
                    dtpEndDate.Checked = False
                End If

                cboItemType.SelectedValue = obj.ItemType
                txtDate.Value = clsCommon.myCDate(obj.CreatedDate)
                txtCode.Value = clsCommon.myCstr(obj.Code)

                If clsCommon.myCstr(obj.Status) = "1" Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                gv1.Rows.Clear()
                btnSave.Text = "Update"
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsVendorItemChargeDetail In obj.Arr
                        gv1.Rows.AddNew()

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRMCode).Value = objTr.RM_Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRMName).Value = objTr.RM_Item_Name

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.ItemCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.ItemDesc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIUOM).Value = objTr.ItemUom
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCharges).Value = objTr.ItemCharge

                    Next
                End If
                ShowUOMDetail(txtCode.Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_JOB_OUTWARD_PRICE_head where Price_Code='" + txtCode.Value + "'"

            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
          
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim qry As String = "select Price_Code,convert(varchar,Price_Date,103) as [Price Date],Item_type,Vendor_Price_Code,StartDate,EndDate from TSPL_JOB_OUTWARD_PRICE_head"

        LoadData(clsCommon.ShowSelectForm("JWPC", qry, "Price_Code", "", txtCode.Value, "Price_Code", isButtonClicked), NavigatorType.Current)

    End Sub
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            Dim obj As New clsVendorItemChargeMaster()
            If (myMessages.postConfirm()) Then
                If SavingData(True) Then

                    obj.UomArr = New List(Of clsVendorItemChargeDetail)
                    For Each grow As GridViewRowInfo In gv1.Rows
                        Dim objTr As New clsVendorItemChargeDetail()
                        objTr.ItemCode = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.ItemCharge = clsCommon.myCstr(grow.Cells(colCharges).Value)
                        objTr.ItemUom = clsCommon.myCstr(grow.Cells(colIUOM).Value)
                        objTr.RM_Item_Code = clsCommon.myCstr(grow.Cells(colRMCode).Value)
                        obj.Code = txtCode.Value
                        If (clsCommon.myLen(objTr.ItemCode) > 0) Then
                            obj.UomArr.Add(objTr)
                        End If
                        qry = " select Item_Code,UOM_Code,UOM_Description from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objTr.ItemCode & "'"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            obj.UomArr = New List(Of clsVendorItemChargeDetail)
                            Dim objTr1 As clsVendorItemChargeDetail
                            For Each dr As DataRow In dt.Rows
                                objTr1 = New clsVendorItemChargeDetail
                                objTr1.ItemCode = clsCommon.myCstr(dr("Item_Code"))
                                objTr1.ItemUom = clsCommon.myCstr(dr("UOM_Code"))
                                objTr1.UomDesc = clsCommon.myCstr(dr("UOM_Description"))

                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "RM_Item_Code", objTr.RM_Item_Code)
                                clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Code)
                                clsCommon.AddColumnsForChange(coll, "ItemCode", objTr.ItemCode)
                                clsCommon.AddColumnsForChange(coll, "ItemUom", objTr1.ItemUom)
                                clsCommon.AddColumnsForChange(coll, "UomDesc", objTr1.UomDesc)
                                clsCommon.AddColumnsForChange(coll, "Charges", clsVendorItemChargeMaster.GetConvertionFactorValue(objTr.ItemCode, objTr1.ItemUom, objTr.ItemCharge, objTr.ItemUom, Nothing))
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_OUTWARD_PRICE_DETAIL_All_UOM", OMInsertOrUpdate.Insert, "")
                            Next
                        End If
                    Next

                    If (obj.PostData(obj, isNewEntry, True)) Then
                        msg = "Successfully Posted"
                    End If

                    If clsCommon.myLen(msg) > 0 Then
                        common.clsCommon.MyMessageBoxShow(msg)
                    End If
                    txtCode.Value = obj.Code
                    LoadData(txtCode.Value, NavigatorType.Current)



                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub gvTS_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gvTS_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow

        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub


    Private Sub Import_Click(sender As Object, e As EventArgs) Handles Import.Click
        Try
            If clsCommon.myLen(txtPriceCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Price Code")
                txtPriceCode.Focus()
                Exit Sub
            End If

            Dim obj As New clsVendorItemChargeDetail()
        Dim arr As New List(Of clsVendorItemChargeDetail)
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim gvbool As Boolean = False
            gvbool = transportSql.importExcel(gv, "ItemCode", "ItemDesc", "ItemUOM", "Charges")

        If gvbool Then

                Dim ii As Integer = 0
            Try
                    clsCommon.ProgressBarShow()
                    For ii = 0 To gv.RowCount - 1
                        If clsCommon.myLen(gv.Rows(ii).Cells("ItemCode").Value) > 0 Then
                            Dim objTr As New clsVendorItemChargeDetail()
                            objTr.ItemCode = clsCommon.myCstr(gv.Rows(ii).Cells("ItemCode").Value)
                            If clsCommon.myLen(objTr.ItemCode) > 0 Then
                                If cboItemType.SelectedValue = "F" Then
                                    Dim ItemType As String = clsDBFuncationality.getSingleValue("select Item_type from tspl_item_master where item_code='" & objTr.ItemCode & "' and Item_Type='F'", trans)
                                    If clsCommon.myLen(ItemType) <= 0 Then
                                        Throw New Exception("Item is not finished Goods " + clsCommon.myCstr(gv.Rows(ii).Cells("ItemCode").Value))
                                    End If
                                Else
                                    Dim ItemType As String = clsDBFuncationality.getSingleValue("select Item_type from tspl_item_master where item_code='" & objTr.ItemCode & "' and Item_Type='S'", trans)
                                    If clsCommon.myLen(ItemType) <= 0 Then
                                        Throw New Exception("Item is not Semi-finished Goods " + clsCommon.myCstr(gv.Rows(ii).Cells("ItemCode").Value))
                                    End If
                                End If
                            Else

                                Throw New Exception("Fill Item code " + clsCommon.myCstr(gv.Rows(ii).Cells("ItemCode").Value))
                            End If
                            objTr.ItemDesc = clsCommon.myCstr(gv.Rows(ii).Cells("ItemDesc").Value)
                            If clsCommon.myLen(objTr.ItemDesc) <= 0 Then
                                Throw New Exception("Fill Item Description " + clsCommon.myCstr(gv.Rows(ii).Cells("ItemDesc").Value))
                            End If
                            objTr.ItemUom = clsCommon.myCstr(gv.Rows(ii).Cells("ItemUOM").Value)
                            objTr.ItemCharge = clsCommon.myCstr(gv.Rows(ii).Cells("Charges").Value)
                            If clsCommon.myLen(objTr.ItemCharge) <= 0 Then
                                Throw New Exception("Fill Item Charges " + clsCommon.myCstr(gv.Rows(ii).Cells("Charges").Value))
                            End If
                            arr.Add(objTr)
                        End If
                    Next

                isInsideLoadData = True
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        gv1.Rows.Clear()
                    For Each objTr As clsVendorItemChargeDetail In arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.ItemCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.ItemDesc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIUOM).Value = objTr.ItemUom
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCharges).Value = objTr.ItemCharge

                    Next
                End If
                    trans.Commit()
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()

                    Throw New Exception("Error at Row No" + clsCommon.myCstr(ii + 1) + Environment.NewLine + ex.Message)
            Finally
                clsCommon.ProgressBarHide()
                isInsideLoadData = False
            End Try
            Me.Controls.Remove(gv)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        Try
            Dim qry As String = " select ItemCode,ItemDesc,ItemUOM,Charges from TSPL_JOB_OUTWARD_PRICE_DETAIL "
            transportSql.ExporttoExcel(qry, Me)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

 
    Private Sub cboItemType_SelectedIndexChanged(sender As Object, e As Data.PositionChangedEventArgs) Handles cboItemType.SelectedIndexChanged
        Try
            If clsCommon.myCstr(cboItemType.SelectedValue) = "F" Then
                Dim itemType As String = clsDBFuncationality.getSingleValue("select Item_Type from tspl_item_master where item_code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "'")
                If clsCommon.myCstr(itemType) = "F" Then
                Else
                    gv1.Rows.Clear()
                    gv1.Rows.AddNew()
                End If
            Else
                Dim itemType As String = clsDBFuncationality.getSingleValue("select Item_Type from tspl_item_master where item_code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "'")
                If clsCommon.myCstr(itemType) = "S" Then
                Else
                    gv1.Rows.Clear()
                    gv1.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        ' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsVendorItemChargeMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub btnShowHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowHistory.Click
        Try
            clsERPFuncationality.ShowTransHistoryData(txtCode.Value, "Price_Code", "TSPL_JOB_OUTWARD_PRICE_HEAD", "TSPL_JOB_OUTWARD_PRICE_DETAIL")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub ShowUOMDetail(ByVal strPrice_Code As String)
        gv.DataSource = Nothing
        Dim qry As String = ""
        Dim ItemSelect As String = clsDBFuncationality.getSingleValue(" Select STUFF((Select ','''+ItemCode+'''' from (Select Distinct TSPL_JOB_OUTWARD_PRICE_DETAIL.ItemCode from TSPL_JOB_OUTWARD_PRICE_DETAIL WHERE Price_Code='" & strPrice_Code & "'  ) XXX For XML Path('')),1,1,'')")

        qry = "select  TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.RM_Item_Code,TSPL_ITEM_MASTER.Item_Desc as RM_Item_Name, TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemCode,TSPL_ITEM_MASTER.Item_Desc,TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemUom as [Unit Code],TSPL_UNIT_MASTER.Unit_Desc as [Unit Desc],TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit,TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.Charges 
        From TSPL_ITEM_UOM_DETAIL  
        Left outer Join TSPL_UNIT_MASTER On TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code  
         left outer Join TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM On TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemUom=TSPL_ITEM_UOM_DETAIL.UOM_Code And TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemCode 
        Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemCode  where 2=2
         And  TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.Price_Code='" & strPrice_Code & "' and TSPL_ITEM_UOM_DETAIL.Item_Code in (" & ItemSelect & ")"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            gv.DataSource = dt
            gv.EnableFiltering = True
            formatGrid()
        End If
    End Sub
    Sub formatGrid()
        gv.GroupDescriptors.Clear()
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("RM_Item_Code").IsVisible = True
        gv.Columns("RM_Item_Code").Width = 100
        gv.Columns("RM_Item_Code").HeaderText = "RM Item Code"

        gv.Columns("RM_Item_Name").IsVisible = True
        gv.Columns("RM_Item_Name").Width = 200
        gv.Columns("RM_Item_Name").HeaderText = "RM Item Description"

        gv.Columns("ItemCode").IsVisible = True
        gv.Columns("ItemCode").Width = 100
        gv.Columns("ItemCode").HeaderText = "Item Code"

        gv.Columns("Item_Desc").IsVisible = True
        gv.Columns("Item_Desc").Width = 200
        gv.Columns("Item_Desc").HeaderText = "Item Description"

        gv.Columns("Unit Code").IsVisible = True
        gv.Columns("Unit Code").Width = 100
        gv.Columns("Unit Code").HeaderText = "Unit Code"

        gv.Columns("Unit Desc").IsVisible = True
        gv.Columns("Unit Desc").Width = 100
        gv.Columns("Unit Desc").HeaderText = "Unit Description"

        gv.Columns("Conversion_Factor").IsVisible = True
        gv.Columns("Conversion_Factor").Width = 100
        gv.Columns("Conversion_Factor").HeaderText = "Conversion Factor"

        gv.Columns("Stocking_Unit").IsVisible = False
        gv.Columns("Stocking_Unit").Width = 100
        gv.Columns("Stocking_Unit").HeaderText = "Stocking Unit"

        gv.Columns("Charges").IsVisible = True
        gv.Columns("Charges").Width = 100
        gv.Columns("Charges").HeaderText = "Charges"


    End Sub


End Class
