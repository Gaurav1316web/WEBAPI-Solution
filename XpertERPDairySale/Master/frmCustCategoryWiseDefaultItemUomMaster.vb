Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports System.IO
Imports System.Xml
Public Class frmCustCategoryWiseDefaultItemUomMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colIShortDescription As String = "COLIShortDescription"
    Const colIAliesName As String = "COLIAliesName"
    Const colIHSNCode As String = "COLIHSNCode"
    Const colIUOM As String = "colIUOM"
    Private isNewEntry As Boolean = True
#End Region
    Private Sub frmVendorItemChargeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim coll As Dictionary(Of String, String)
        'coll = New Dictionary(Of String, String)()
        '' Customer_Category,Description,Created_By ,Created_Date,Modified_By, Modified_Date
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD", coll)

        'coll = New Dictionary(Of String, String)()
        ''Customer_Category,Item_code , Default_UOM 
        'clsCommonFunctionality.CreateOrAlterTable("TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS", coll)

        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        LoadBlankGrid()
        gv1.Enabled = True
    End Sub
    Private Sub frmVendorItemChargeMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SavingData(False)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        'repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode) '3

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Name"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoIShortDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIShortDescription.FormatString = ""
        repoIShortDescription.HeaderText = "Item Short Name"
        repoIShortDescription.Name = colIShortDescription
        repoIShortDescription.Width = 150
        repoIShortDescription.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIShortDescription)

        Dim repoIAliesName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIAliesName.FormatString = ""
        repoIAliesName.HeaderText = "Item Alies Name"
        repoIAliesName.Name = colIAliesName
        repoIAliesName.Width = 150
        repoIAliesName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIAliesName)

        Dim repoIHSNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIHSNCode.FormatString = ""
        repoIHSNCode.HeaderText = "Item HSN Code"
        repoIHSNCode.Name = colIHSNCode
        repoIHSNCode.Width = 150
        repoIHSNCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIHSNCode)

        Dim repoIUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIUOM.FormatString = ""
        repoIUOM.HeaderText = "Default UOM"
        repoIUOM.Name = colIUOM
        repoIUOM.Width = 150
        gv1.MasterTemplate.Columns.Add(repoIUOM)

        gv1.AllowDeleteRow = False
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Sub funReset()
        cmbCustomerCategory.Enabled = True
        cmbCustomerCategory.Text = "Select"
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        'gv1.Rows.AddNew()

    End Sub



    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                'If e.Column Is gv1.Columns(colICode) Then
                '    OpenICodeList(False)
                If e.Column Is gv1.Columns(colIUOM) Then
                    OpenUOMList(False)
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colIUOM).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colIUOM).Value), "Code", isButtonClick)
        End If
    End Sub
    'Sub OpenICodeList(ByVal isButtonClick As Boolean)
    '    If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("Please select Item Type")
    '        SetBlankOfItemColumns()
    '        cboItemType.Focus()
    '        Exit Sub
    '    End If
    '    Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), True, isButtonClick, "", "", "")
    '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
    '        gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
    '        gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
    '        gv1.CurrentRow.Cells(colIUOM).Value = obj.Unit_Code

    '    Else
    '        SetBlankOfItemColumns()
    '    End If
    'End Sub
    'Private Sub SetBlankOfItemColumns()
    '    gv1.CurrentRow.Cells(colICode).Value = ""
    '    gv1.CurrentRow.Cells(colIName).Value = ""
    '    gv1.CurrentRow.Cells(colIUOM).Value = ""
    '    gv1.CurrentRow.Cells(colCharges).Value = 0
    'End Sub

    'Private Sub rdbtnreset_Click(sender As Object, e As EventArgs)
    '    funReset()
    'End Sub
    'Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    SavingData(False)
    'End Sub
    Private Function SavingData(ByVal ChekBtnPost As Boolean) As Boolean
        If (SaveData(False)) Then
            'If ChekBtnPost = False Then
            common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            'End If
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SaveData(ByVal isDoAbandomentNo As Boolean, Optional ByVal isPOCancel As Boolean = False) As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsCustCategoryWiseDefaultItemUomMaster()

                obj.Customer_Category = cmbCustomerCategory.Text
                obj.Description = ""
                obj.Arr = New List(Of clsCustCategoryWiseDefaultItemUomDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsCustCategoryWiseDefaultItemUomDetail()
                    objTr.Customer_Category = cmbCustomerCategory.Text
                    objTr.Item_code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Default_UOM = clsCommon.myCstr(grow.Cells(colIUOM).Value)
                    If (clsCommon.myLen(objTr.Item_code) > 0 AndAlso clsCommon.myLen(objTr.Default_UOM) > 0) Then
                        obj.Arr.Add(objTr)
                    End If

                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill atlist one Item's Default UOM ", Me.Text)
                    Return False
                End If

                Dim isSaved As Boolean = True
                Dim isCategoryCodeExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("Select count (*) from TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD where Customer_Category = '" + cmbCustomerCategory.Text + "'"))
                If isCategoryCodeExist = False Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                isSaved = isSaved AndAlso obj.SaveData(obj, isNewEntry)
                LoadData(cmbCustomerCategory.Text, NavigatorType.Current)
                Return isSaved
            Else
                Return False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return False
    End Function
    Function AllowToSave() As Boolean
        Try
            If clsCommon.CompairString(cmbCustomerCategory.Text, "Select") = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Customer Category Code", Me.Text)
                cmbCustomerCategory.Focus()
                Return False
            End If
            'Dim count As Integer = 0
            'For ii As Integer = 0 To gv1.Rows.Count - 1
            '    Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            '    Dim strDefaultUOM As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colIUOM).Value)
            '    If clsCommon.myLen(strDefaultUOM) > 0 Then
            '        count += 1
            '    End If
            'Next
            'If count = 0 Then
            '    common.clsCommon.MyMessageBoxShow("Please select atlest one Item's Default Uom.", Me.Text)
            '    Return False
            'End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True

            Dim obj As New clsCustCategoryWiseDefaultItemUomMaster()
            obj = clsCustCategoryWiseDefaultItemUomMaster.GetData(strCode, NavTyep, "")

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Customer_Category) > 0) Then
                cmbCustomerCategory.Enabled = False
                btnSave.Enabled = True
                isNewEntry = False
                cmbCustomerCategory.Text = obj.Customer_Category
                gv1.Rows.Clear()
                Dim isUpdate As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD where Customer_Category = '" + obj.Customer_Category + "'"))
                If isUpdate Then
                    btnSave.Text = "Update"
                Else
                    btnSave.Text = "Save"
                End If

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        For Each objTr As clsCustCategoryWiseDefaultItemUomDetail In obj.Arr
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIShortDescription).Value = objTr.Short_Description
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIAliesName).Value = objTr.Alies_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSNCode).Value = objTr.HSN_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colIUOM).Value = objTr.Default_UOM


                        Next
                    End If

                End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    'Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType)
    '    Try
    '        Dim qst As String = "select count(*) from TSPL_JOB_OUTWARD_PRICE_head where Price_Code='" + txtCode.Value + "'"

    '        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))

    '        LoadData(txtCode.Value, NavType)
    '    Catch ex As Exception

    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)

    '    Dim qry As String = "select Price_Code,convert(varchar,Price_Date,103) as [Price Date],Item_type,Vendor_Price_Code,StartDate,EndDate from TSPL_JOB_OUTWARD_PRICE_head"

    '    LoadData(clsCommon.ShowSelectForm("JWPC", qry, "Price_Code", "", txtCode.Value, "Price_Code", isButtonClicked), NavigatorType.Current)

    'End Sub
    'Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    PostData()
    'End Sub
    'Sub PostData()
    '    Try
    '        Dim qry As String = ""
    '        Dim msg As String = ""
    '        Dim dt As DataTable = Nothing
    '        Dim obj As New clsVendorItemChargeMaster()
    '        If (myMessages.postConfirm()) Then
    '            If SavingData(True) Then

    '                obj.UomArr = New List(Of clsVendorItemChargeDetail)
    '                For Each grow As GridViewRowInfo In gv1.Rows
    '                    Dim objTr As New clsVendorItemChargeDetail()
    '                    objTr.ItemCode = clsCommon.myCstr(grow.Cells(colICode).Value)
    '                    objTr.ItemCharge = clsCommon.myCstr(grow.Cells(colCharges).Value)
    '                    objTr.ItemUom = clsCommon.myCstr(grow.Cells(colIUOM).Value)
    '                    obj.Code = txtCode.Value
    '                    If (clsCommon.myLen(objTr.ItemCode) > 0) Then
    '                        obj.UomArr.Add(objTr)
    '                    End If
    '                    qry = " select Item_Code,UOM_Code,UOM_Description from TSPL_ITEM_UOM_DETAIL where Item_Code='" & objTr.ItemCode & "'"
    '                    dt = clsDBFuncationality.GetDataTable(qry)
    '                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '                        obj.UomArr = New List(Of clsVendorItemChargeDetail)
    '                        Dim objTr1 As clsVendorItemChargeDetail
    '                        For Each dr As DataRow In dt.Rows
    '                            objTr1 = New clsVendorItemChargeDetail
    '                            objTr1.ItemCode = clsCommon.myCstr(dr("Item_Code"))
    '                            objTr1.ItemUom = clsCommon.myCstr(dr("UOM_Code"))
    '                            objTr1.UomDesc = clsCommon.myCstr(dr("UOM_Description"))

    '                            Dim coll As New Hashtable()
    '                            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Code)
    '                            clsCommon.AddColumnsForChange(coll, "ItemCode", objTr.ItemCode)
    '                            clsCommon.AddColumnsForChange(coll, "ItemUom", objTr1.ItemUom)
    '                            clsCommon.AddColumnsForChange(coll, "UomDesc", objTr1.UomDesc)
    '                            clsCommon.AddColumnsForChange(coll, "Charges", clsVendorItemChargeMaster.GetConvertionFactorValue(objTr.ItemCode, objTr1.ItemUom, objTr.ItemCharge, objTr.ItemUom, Nothing))

    '                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOB_OUTWARD_PRICE_DETAIL_All_UOM", OMInsertOrUpdate.Insert, "")
    '                        Next
    '                    End If

    '                Next


    '                If (obj.PostData(obj, isNewEntry, True)) Then
    '                    msg = "Successfully Posted"
    '                End If

    '                If clsCommon.myLen(msg) > 0 Then
    '                    common.clsCommon.MyMessageBoxShow(msg)
    '                End If
    '                txtCode.Value = obj.Code
    '                LoadData(txtCode.Value, NavigatorType.Current)



    '            End If
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    'Private Sub gvTS_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
    '    If gv1.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gv1.CurrentRow.Index
    '        If intCurrRow = gv1.Rows.Count - 1 Then
    '            gv1.Rows.AddNew()
    '            gv1.CurrentRow = gv1.Rows(intCurrRow)
    '        End If
    '    End If
    'End Sub
    'Private Sub gvTS_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow

    '    If Not myMessages.deleteConfirm() Then
    '        e.Cancel = True
    '    End If
    'End Sub


    Private Sub Import_Click(sender As Object, e As EventArgs) Handles Import.Click
        Dim gv As New RadGridView()

        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)

        If transportSql.importExcel(gv, "Customer_Category", "Item_Code", "Item_Desc", "Short_Description", "Alies_Name", "HSN_Code", "Default_UOM") Then
            Dim linno As Integer = 1
            Try
                trans = clsDBFuncationality.GetTransactin()
                connectSql.OpenConnection()

                Dim strCustomerCategory As String = String.Empty
                Dim strItemCode As String = String.Empty
                Dim strDefaultUOM As String = String.Empty


                For Each grow As GridViewRowInfo In gv.Rows

                    strCustomerCategory = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    strItemCode = clsCommon.myCstr(grow.Cells("Location Name").Value)
                    strDefaultUOM = clsCommon.myCstr(grow.Cells("Item Code").Value)


                    linno += 1

                    If (String.IsNullOrEmpty(strCustomerCategory)) Then
                        Throw New Exception("Customer Category can't be blank. At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If (String.IsNullOrEmpty(strItemCode)) Then
                        Throw New Exception("Item code can't be blank. 50 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim isValidItemcode As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_MASTER  where Item_Code = '" + strItemCode + "' ", trans))
                        If isValidItemcode = False Then
                            Throw New Exception("Invalid Item Code. At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If clsCommon.myLen(strDefaultUOM) > 0 Then
                        Dim isValidUOM As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_UOM_DETAIL  where Item_Code = '" + strItemCode + "' and UOM_Code = '" + strDefaultUOM + "' ", trans))
                        If isValidUOM = False Then
                            Throw New Exception("Invalid Default UOM, UOM Code not exist with this item. At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If clsCommon.CompairString(strCustomerCategory, "Others") = CompairStringResult.Equal Then
                    ElseIf clsCommon.CompairString(strCustomerCategory, "Vendor") = CompairStringResult.Equal Then
                    ElseIf clsCommon.CompairString(strCustomerCategory, "Institution CR") = CompairStringResult.Equal Then
                    ElseIf clsCommon.CompairString(strCustomerCategory, "Institution SO") = CompairStringResult.Equal Then
                    ElseIf clsCommon.CompairString(strCustomerCategory, "Distributor") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Invalid Customer Category. At Line No. " + clsCommon.myCstr(linno) + ".")

                    End If


                    If clsCommon.myLen(strDefaultUOM) > 0 Then

                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD where Customer_Category ='" & strCustomerCategory & "' ", trans)) > 0 Then
                        Else
                            clsDBFuncationality.ExecuteNonQuery(" insert into TSPL_CUST_CATEGORY_DEFAULT_UOM_HEAD (Customer_Category,Description,Created_By,Created_Date,Modified_By,Modified_Date) values ('" + strCustomerCategory + "','','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "')", trans)

                        End If

                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS where Customer_Category ='" & strCustomerCategory & "' and Item_code = '" + strItemCode + "' ", trans)) > 0 Then
                            
                            clsDBFuncationality.ExecuteNonQuery(" update TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS set Default_UOM = '" + strDefaultUOM + "' where  Customer_Category = '" + strCustomerCategory + "' and Item_code = '" + strItemCode + "' ", trans)
                        Else
                            clsDBFuncationality.ExecuteNonQuery(" insert into TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS (Customer_Category,Item_code,Default_UOM) values ('" + strCustomerCategory + "','" + strItemCode + "','" + strDefaultUOM + "')", trans)
                        End If

                    End If

                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub


    'Private Sub cboItemType_SelectedIndexChanged(sender As Object, e As Data.PositionChangedEventArgs)
    '    Try
    '        If clsCommon.myCstr(cboItemType.SelectedValue) = "F" Then
    '            Dim itemType As String = clsDBFuncationality.getSingleValue("select Item_Type from tspl_item_master where item_code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "'")
    '            If clsCommon.myCstr(itemType) = "F" Then
    '            Else
    '                gv1.Rows.Clear()
    '                gv1.Rows.AddNew()
    '            End If
    '        Else
    '            Dim itemType As String = clsDBFuncationality.getSingleValue("select Item_Type from tspl_item_master where item_code='" & gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value & "'")
    '            If clsCommon.myCstr(itemType) = "S" Then
    '            Else
    '                gv1.Rows.Clear()
    '                gv1.Rows.AddNew()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    DeleteData()
    'End Sub
    'Sub DeleteData()
    '    If clsCommon.myLen(txtCode.Value) <= 0 Then
    '        common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
    '        Exit Sub
    '    End If
    '    ' Code Ends 
    '    funDelete()
    'End Sub

    'Sub funDelete()
    '    Try
    '        If (myMessages.deleteConfirm()) Then
    '            If (clsVendorItemChargeMaster.DeleteData(txtCode.Value)) Then
    '                common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
    '                funReset()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try

    'End Sub
    'Private Sub btnShowHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        clsERPFuncationalityold.ShowTransHistoryData(txtCode.Value, "Price_Code", "TSPL_JOB_OUTWARD_PRICE_HEAD", "TSPL_JOB_OUTWARD_PRICE_DETAIL")
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    'Sub ShowUOMDetail(ByVal strPrice_Code As String)
    '    gv.DataSource = Nothing
    '    Dim qry As String = ""
    '    Dim ItemSelect As String = clsDBFuncationality.getSingleValue(" Select STUFF((Select ','''+ItemCode+'''' from (Select Distinct TSPL_JOB_OUTWARD_PRICE_DETAIL.ItemCode from TSPL_JOB_OUTWARD_PRICE_DETAIL WHERE Price_Code='" & strPrice_Code & "'  ) XXX For XML Path('')),1,1,'')")

    '    qry = "   select TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemCode,TSPL_ITEM_MASTER.Item_Desc,TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemUom as [Unit Code],TSPL_UNIT_MASTER.Unit_Desc as [Unit Desc],TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit,TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.Charges "
    '    qry += " from TSPL_ITEM_UOM_DETAIL "
    '    qry += "  left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code "
    '    qry += "  left outer join TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM on TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemUom=TSPL_ITEM_UOM_DETAIL.UOM_Code and TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemCode left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.ItemCode where 2=2"
    '    qry += " and  TSPL_JOB_OUTWARD_PRICE_DETAIL_ALL_UOM.Price_Code='" & strPrice_Code & "' and TSPL_ITEM_UOM_DETAIL.Item_Code in (" & ItemSelect & ")"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
    '        gv.MasterTemplate.SummaryRowsBottom.Clear()
    '        gv.DataSource = dt
    '        gv.EnableFiltering = True
    '        formatGrid()
    '    End If
    'End Sub
    'Sub formatGrid()
    '    gv.GroupDescriptors.Clear()
    '    gv.TableElement.TableHeaderHeight = 40
    '    gv.MasterTemplate.ShowRowHeaderColumn = False
    '    For ii As Integer = 0 To gv.Columns.Count - 1
    '        gv.Columns(ii).ReadOnly = True
    '        gv.Columns(ii).IsVisible = False
    '    Next

    '    gv.Columns("ItemCode").IsVisible = True
    '    gv.Columns("ItemCode").Width = 100
    '    gv.Columns("ItemCode").HeaderText = "Item Code"

    '    gv.Columns("Item_Desc").IsVisible = True
    '    gv.Columns("Item_Desc").Width = 200
    '    gv.Columns("Item_Desc").HeaderText = "Item Description"

    '    gv.Columns("Unit Code").IsVisible = True
    '    gv.Columns("Unit Code").Width = 100
    '    gv.Columns("Unit Code").HeaderText = "Unit Code"

    '    gv.Columns("Unit Desc").IsVisible = True
    '    gv.Columns("Unit Desc").Width = 100
    '    gv.Columns("Unit Desc").HeaderText = "Unit Description"

    '    gv.Columns("Conversion_Factor").IsVisible = True
    '    gv.Columns("Conversion_Factor").Width = 100
    '    gv.Columns("Conversion_Factor").HeaderText = "Conversion Factor"

    '    gv.Columns("Stocking_Unit").IsVisible = False
    '    gv.Columns("Stocking_Unit").Width = 100
    '    gv.Columns("Stocking_Unit").HeaderText = "Stocking Unit"

    '    gv.Columns("Charges").IsVisible = True
    '    gv.Columns("Charges").Width = 100
    '    gv.Columns("Charges").HeaderText = "Charges"


    'End Sub

    Private Sub cmbCustomerCategory_SelectedIndexChanged(sender As Object, e As Data.PositionChangedEventArgs) Handles cmbCustomerCategory.SelectedIndexChanged
        'If clsCommon.CompairString(cmbCustomerCategory.Text, "Select") <> CompairStringResult.Equal AndAlso clsCommon.myLen(cmbCustomerCategory.Text) > 0 Then
        '    LoadData(cmbCustomerCategory.Text, NavigatorType.Current)
        'End If

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If clsCommon.CompairString(cmbCustomerCategory.Text, "Select") <> CompairStringResult.Equal AndAlso clsCommon.myLen(cmbCustomerCategory.Text) > 0 Then
            LoadData(cmbCustomerCategory.Text, NavigatorType.Current)
        Else
            clsCommon.MyMessageBoxShow(Me, "Please Select Customer Category", Me.Text)
        End If
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        funReset()
    End Sub

    Private Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        Try
            Dim str As String = String.Empty

            str = " Select XXXFinal.* from ( " &
                    "  select 'Others'as Customer_Category,  TSPL_ITEM_MASTER.item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Alies_Name, TSPL_ITEM_MASTER.HSN_Code, TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM from TSPL_ITEM_MASTER left outer Join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = TSPL_ITEM_MASTER.item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = 'Others' where 2=2  and isnull(tspl_item_master.Item_Type,'')='F' and isnull(tspl_item_master.TypeOfItm,'')='A' and tspl_item_master.Active=1 " &
                    "  union all " &
                    "  select 'Vendor'as Customer_Category, TSPL_ITEM_MASTER.item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Alies_Name, TSPL_ITEM_MASTER.HSN_Code, TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM from TSPL_ITEM_MASTER left outer Join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = TSPL_ITEM_MASTER.item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = 'Vendor' where 2=2  and isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1  " &
                    "    union all " &
                    "  select 'Institution CR'as Customer_Category, TSPL_ITEM_MASTER.item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Alies_Name, TSPL_ITEM_MASTER.HSN_Code, TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM from TSPL_ITEM_MASTER left outer Join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = TSPL_ITEM_MASTER.item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = 'Institution CR' where 2=2  and isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1 " &
                    "   union all " &
                    " select 'Institution SO'as Customer_Category, TSPL_ITEM_MASTER.item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Alies_Name, TSPL_ITEM_MASTER.HSN_Code, TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM from TSPL_ITEM_MASTER left outer Join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = TSPL_ITEM_MASTER.item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = 'Institution SO' where 2=2  and isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1  " &
                    "   union all " &
                    "  select 'Distributor'as Customer_Category, TSPL_ITEM_MASTER.item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_ITEM_MASTER.Short_Description, TSPL_ITEM_MASTER.Alies_Name, TSPL_ITEM_MASTER.HSN_Code, TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Default_UOM from TSPL_ITEM_MASTER left outer Join TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS on TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Item_Code = TSPL_ITEM_MASTER.item_Code and TSPL_CUST_CATEGORY_DEFAULT_UOM_DETAILS.Customer_Category = 'Distributor' where 2=2  and isnull(tspl_item_master.Chilled_Freezen,0)=1 and isnull(TSPL_ITEM_MASTER.item_type,'')='F'  and tspl_item_master.Active=1 " &
                    "  ) XXXFinal order by XXXFinal.Customer_Category , XXXFinal.Item_Code "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                transportSql.ExporttoExcel(str, Me)
                'Else
                '    transportSql.ExporttoExcel("Select '' as [Location Code] ,''  as [Location Name],0 as [Sequence No] ,'' as [Customer Code] ,'' as [Customer Name]", Me)
            End If
            str = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
