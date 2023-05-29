Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports System.IO
Imports System.Xml
Public Class frmJWIItemPriceMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Const colRMCode As String = "colRMCode"
    Const colRMName As String = "colRMName"
    Const colFGCode As String = "colFGCode"
    Const colFGName As String = "colFGName"
    Const colFGUOM As String = "colFGUOM"
    Const colFGCost As String = "colFGCost"

    Const colSNo As String = "colSNo"
    Const colVendorCode As String = "colVendorCode"
    Const ColVendorName As String = "ColVendorName"

    Private isNewEntry As Boolean = True
#End Region
    ''Richa 20200616
    Private Sub frmJWIItemPriceMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New ")
        AddNew()
        gv1.Enabled = True
        btnShowHistory.Visible = False
    End Sub

    Private Sub frmVendorItemChargeMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
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

    Sub AddNew()
        txtDate.Value = clsCommon.GETSERVERDATE()
        dtpEndDate.Value = txtDate.Value
        dtStartDate.Value = txtDate.Value
        txtCode.Value = ""
        txtDesc.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        dtpEndDate.Checked = False
        LoadBlankGrid(gv1)
        LoadBlankGrid(gv2)
        LoadBlankGridVendor()
        gv2.ReadOnly = True
        gv1.Rows.AddNew()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
        chkInactive.Enabled = False
        chkInactive.Checked = False
        btnVendorDetails.Visible = False
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Sub LoadBlankGrid(ByVal gv As common.UserControls.MyRadGridView)
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoText As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "RM Item Code"
        repoText.Name = colRMCode
        repoText.HeaderImage = Global.XpertERPJobWorkOutward.My.Resources.Resources.search4
        repoText.TextImageRelation = TextImageRelation.TextBeforeImage
        repoText.Width = 100
        gv.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "RM Item Description"
        repoText.Name = colRMName
        repoText.Width = 150
        repoText.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "FG Item Code"
        repoText.Name = colFGCode
        repoText.HeaderImage = Global.XpertERPJobWorkOutward.My.Resources.Resources.search4
        repoText.TextImageRelation = TextImageRelation.TextBeforeImage
        repoText.Width = 100
        gv.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "FG Item Description"
        repoText.Name = colFGName
        repoText.Width = 150
        repoText.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Item UOM"
        repoText.Name = colFGUOM
        repoText.Width = 100
        repoText.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoText)

        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "FG Cost"
        repoNumBox.Name = colFGCost
        repoNumBox.Minimum = 0
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoNumBox)


        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
        gv.AutoSizeRows = False
        gv.AllowRowReorder = True
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

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If e.Column Is gv1.Columns(colRMCode) Then
                    OpenRMCodeList(False)
                ElseIf e.Column Is gv1.Columns(colFGCode) Then
                    OpenFGCodeList(False)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        'Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colRMCode).Value)
        'If clsCommon.myLen(strICode) > 0 Then
        '    Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
        '    Dim whrCls As String = "Item_Code='" + strICode + "'"
        '    gv1.CurrentRow.Cells(colIUOM).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colIUOM).Value), "Code", isButtonClick)
        'End If
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

    Sub OpenFGCodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colFGCode).Value), "F", True, isButtonClick, "", "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colFGCode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colFGName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colFGUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Top 1 UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code ='" + obj.Item_Code + "'  and Stocking_Unit='Y'"))
        Else
            SetBlankOfItemColumns()
        End If
    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colRMCode).Value = ""
        gv1.CurrentRow.Cells(colRMName).Value = ""
        gv1.CurrentRow.Cells(colFGCode).Value = ""
        gv1.CurrentRow.Cells(colFGName).Value = ""
        gv1.CurrentRow.Cells(colFGUOM).Value = ""
        gv1.CurrentRow.Cells(colFGCost).Value = 0
    End Sub

    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        AddNew()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Public Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsJWIItemPriceMaster()
                obj.Price_Code = txtCode.Value
                obj.Price_Date = txtDate.Value
                obj.Start_Date = dtStartDate.Value
                If dtpEndDate.Checked Then
                    obj.End_Date = dtpEndDate.Value
                End If
                obj.Description = txtDesc.Text
                obj.Arr = New List(Of clsJWIItemPriceDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsJWIItemPriceDetail()
                    objTr.RM_Item_Code = clsCommon.myCstr(grow.Cells(colRMCode).Value)
                    objTr.FG_Item_Code = clsCommon.myCstr(grow.Cells(colFGCode).Value)
                    objTr.FG_Item_UOM = clsCommon.myCstr(grow.Cells(colFGUOM).Value)
                    objTr.FG_Item_Cost = clsCommon.myCdbl(grow.Cells(colFGCost).Value)
                    If (clsCommon.myLen(objTr.RM_Item_Code) > 0 AndAlso clsCommon.myLen(objTr.FG_Item_UOM) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at least one Item")
                    Exit Sub
                End If

                obj.ArrVendor = New List(Of clsJWIVendorDetail)
                For Each grow As GridViewRowInfo In gvVendor.Rows
                    Dim objTr As New clsJWIVendorDetail()
                    objTr.Vendor_Code = clsCommon.myCstr(grow.Cells(colVendorCode).Value)
                    objTr.Vendor_Name = clsCommon.myCstr(grow.Cells(ColVendorName).Value)
                    If (clsCommon.myLen(objTr.Vendor_Code) > 0) Then
                        obj.ArrVendor.Add(objTr)
                    End If
                Next

                If (obj.ArrVendor Is Nothing OrElse obj.ArrVendor.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at least one Vendor")
                    Exit Sub
                End If

                If obj.SaveData(obj, isNewEntry) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Price_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try

            Dim arrString As New List(Of String)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strRMCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRMCode).Value)
                Dim strFGCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colFGCode).Value)
                If clsCommon.myLen(strRMCode) > 0 AndAlso clsCommon.myLen(strFGCode) > 0 Then
                    Dim strRMFG As String = strRMCode + "#$#" + strFGCode
                    If arrString.Contains(strRMFG) Then
                        Throw New Exception("RM Item [" + strRMCode + "] and FG Item [" + strFGCode + "] is Repeating")
                    Else
                        arrString.Add(strRMFG)
                    End If
                    If clsCommon.myCdbl(gv1.Rows(ii).Cells(colFGCost).Value) <= 0 Then
                        Throw New Exception("Cost is not defined for RM Item [" + strRMCode + "] and FG Item [" + strFGCode + "]")
                    End If
                End If
            Next
            'For ii As Integer = 0 To gv1.Rows.Count - 1
            '    Dim strRMICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colRMCode).Value)
            '    Dim strFGICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colFGCode).Value)
            '    Dim strFGUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colFGUOM).Value)
            '    Dim intCountCustomer As Integer = 0
            '    For kk As Integer = 0 To gvVendor.Rows.Count - 1
            '        Dim strCustomer As String = clsCommon.myCstr(gvVendor.Rows(kk).Cells(colVendorCode).Value)
            '        If clsCommon.myLen(strCustomer) > 0 Then
            '            Dim strSchemeExist As String = ("SELECT count(*) from TSPL_JWI_PRICE_HEAD inner join TSPL_JWI_PRICE_DETAIL_ALL_UOM on TSPL_JWI_PRICE_DETAIL_ALL_UOM.Price_Code=TSPL_JWI_PRICE_HEAD.Price_Code inner join TSPL_JWI_PRICE_VENDOR_DETAIL on TSPL_JWI_PRICE_HEAD.Price_Code=" & _
            '                                          " TSPL_JWI_PRICE_VENDOR_DETAIL.Price_Code WHERE TSPL_JWI_PRICE_HEAD.Price_Code<>'" + clsCommon.myCstr(txtCode.Value) + "' and TSPL_JWI_PRICE_HEAD.InActive=0 " & _
            '                                          " and TSPL_JWI_PRICE_HEAD.Start_Date < = '" & clsCommon.GetPrintDate(dtStartDate.Value, "dd/MMM/yyyy") & "' and isnull(TSPL_JWI_PRICE_HEAD.End_Date,getdate()) >= '" & clsCommon.GetPrintDate(dtpEndDate.Value, "dd/MMM/yyyy") & "' and TSPL_JWI_PRICE_VENDOR_DETAIL.Vendor_Code='" & strCustomer & "' and TSPL_JWI_PRICE_DETAIL_ALL_UOM.RM_Item_Code='" & strRMICode & "' and TSPL_JWI_PRICE_DETAIL_ALL_UOM.FG_Item_Code='" & strFGICode & "' and TSPL_JWI_PRICE_DETAIL_ALL_UOM.FG_Item_UOM='" & strFGUOM & "'")
            '            Dim SchemeExist As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strSchemeExist))
            '            If clsCommon.CompairString(clsCommon.myCstr(SchemeExist), "0") <> CompairStringResult.Equal AndAlso clsCommon.myLen(SchemeExist) > 0 Then
            '                Throw New Exception("Price already created for From Date: " & clsCommon.GetPrintDate(dtStartDate.Value, "dd/MMM/yyyy") & " To Date: " & clsCommon.GetPrintDate(dtpEndDate.Value, "dd/MMM/yyyy") & " and RM Item:" & strRMICode & "  and FG Item: " & strFGICode & " and Vendor : " & strCustomer & ". First In-Active then create new Price.")
            '            End If

            '            intCountCustomer += 1
            '            Dim strSchemeCode As String = clsDBFuncationality.getSingleValue("select top 1 TSPL_JWI_PRICE_HEAD.Price_Code  from TSPL_JWI_PRICE_HEAD  " & _
            '             "left outer join TSPL_JWI_PRICE_DETAIL_ALL_UOM on TSPL_JWI_PRICE_HEAD.Price_Code=TSPL_JWI_PRICE_DETAIL_ALL_UOM.Price_Code   left outer join " & _
            '             "TSPL_JWI_PRICE_VENDOR_DETAIL on TSPL_JWI_PRICE_HEAD.Price_Code=TSPL_JWI_PRICE_VENDOR_DETAIL.Price_Code " & _
            '             "where TSPL_JWI_PRICE_DETAIL_ALL_UOM.RM_Item_Code='" & strRMICode & "'  and TSPL_JWI_PRICE_DETAIL_ALL_UOM.FG_Item_Code='" & strFGICode & "' and TSPL_JWI_PRICE_DETAIL_ALL_UOM.FG_Item_UOM='" & strFGUOM & "'  " & _
            '             "and TSPL_JWI_PRICE_HEAD.Start_Date='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "' " & _
            '             " and TSPL_JWI_PRICE_VENDOR_DETAIL.Vendor_Code='" & strCustomer & "'  and  " & _
            '             " TSPL_JWI_PRICE_HEAD.Price_Code not in ('" & txtCode.Value & "')")
            '            If clsCommon.myLen(strSchemeCode) > 0 Then
            '                Throw New Exception("" & strSchemeCode & " already created for Date: " & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & " and RM Item : " & strRMICode & " and FG Item: " & strFGICode & "  and Vendor : " & strCustomer & ".")
            '            End If

            '        End If
            '    Next
            'Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            AddNew()
            Dim obj As New clsJWIItemPriceMaster()
            obj = clsJWIItemPriceMaster.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Price_Code) > 0) Then
                btnSave.Enabled = True
                btnPost.Enabled = True
                isNewEntry = False
                txtCode.Value = obj.Price_Code
                txtDate.Value = obj.Price_Date
                txtDesc.Text = obj.Description
                dtStartDate.Value = obj.Start_Date
                If obj.End_Date IsNot Nothing Then
                    dtpEndDate.Checked = True
                    dtpEndDate.Value = obj.End_Date
                Else
                    dtpEndDate.Checked = False
                End If
                UsLock1.Status = obj.Posted
                chkInactive.Checked = obj.Inactive
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    chkInactive.Enabled = Not obj.Inactive
                    If chkInactive.Enabled Then
                        chkInactive.Enabled = MyBase.isPostFlag
                    End If
                    btnVendorDetails.Visible = True
                End If
                gv1.Rows.Clear()
                btnSave.Text = "Update"

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsJWIItemPriceDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRMCode).Value = objTr.RM_Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRMName).Value = objTr.RM_Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFGCode).Value = objTr.FG_Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFGName).Value = objTr.FG_Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFGUOM).Value = objTr.FG_Item_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFGCost).Value = objTr.FG_Item_Cost
                    Next
                End If

                If obj.Arr IsNot Nothing AndAlso obj.ArrUom.Count > 0 Then
                    For Each objTr As clsJWIItemPriceDetail In obj.ArrUom
                        gv2.Rows.AddNew()
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colRMCode).Value = objTr.RM_Item_Code
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colRMName).Value = objTr.RM_Item_Name
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colFGCode).Value = objTr.FG_Item_Code
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colFGName).Value = objTr.FG_Item_Name
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colFGUOM).Value = objTr.FG_Item_UOM
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colFGCost).Value = objTr.FG_Item_Cost
                    Next
                End If


                If obj.ArrVendor IsNot Nothing AndAlso obj.ArrVendor.Count > 0 Then
                    For Each objTr As clsJWIVendorDetail In obj.ArrVendor
                        gvVendor.Rows(gvVendor.Rows.Count - 1).Cells(colSNo).Value = gvVendor.Rows.Count
                        gvVendor.Rows(gvVendor.Rows.Count - 1).Cells(colVendorCode).Value = objTr.Vendor_Code
                        gvVendor.Rows(gvVendor.Rows.Count - 1).Cells(ColVendorName).Value = objTr.Vendor_Name
                        gvVendor.Rows.AddNew()
                    Next
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_JWI_PRICE_DETAIL where Price_Code='" + txtCode.Value + "'"

            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))

            LoadData(txtCode.Value, NavType)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select Price_Code as PriceCode,convert(varchar,Price_Date,103) as PriceDate,Description,Start_Date as StartDate,End_Date as EndDate,case when Posted=1 then 'Approved' else 'Pending' end as PostStatus,case when Inactive=0 then 'Active' else 'Inactive' end as ActiveStatus  from TSPL_JWI_PRICE_HEAD"
        LoadData(clsCommon.ShowSelectForm("JWPC@JWI", qry, "PriceCode", "", txtCode.Value, "PriceCode", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Please select document no to post")
            End If
            If (myMessages.postConfirm()) Then
                If (clsJWIItemPriceMaster.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
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

    Private Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        Try
            Dim qry As String
            If clsCommon.myLen(txtCode.Value) > 0 Then
                qry = "select * from (select RM_Item_Code,FG_Item_Code,FG_Item_UOM,FG_Item_Cost from TSPL_JWI_PRICE_DETAIL where Price_Code='" + txtCode.Value + "')xxx "
                transportSql.ExporttoExcel(qry, Me)
            Else
                qry = "select '' as RM_Item_Code,'' as FG_Item_Code,'' as FG_Item_UOM,0 as FG_Item_Cost  "
                transportSql.ExporttoExcel(qry, Me)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub Import_Click(sender As Object, e As EventArgs) Handles Import.Click
        Try
            Dim obj As New clsJWIItemPriceDetail()
            Dim arr As New List(Of clsJWIItemPriceDetail)
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim gvbool As Boolean = False
            Dim ii As Integer = 0
            If transportSql.importExcel(gv, "RM_Item_Code", "FG_Item_Code", "FG_Item_UOM", "FG_Item_Cost") Then
                Try
                    clsCommon.ProgressBarShow()
                    For ii = 0 To gv.RowCount - 1
                        Dim objTr As New clsJWIItemPriceDetail()
                        objTr.RM_Item_Code = clsCommon.myCstr(gv.Rows(ii).Cells("RM_Item_Code").Value)
                        objTr.FG_Item_Code = clsCommon.myCstr(gv.Rows(ii).Cells("FG_Item_Code").Value)
                        If clsCommon.myLen(objTr.RM_Item_Code) > 0 AndAlso clsCommon.myLen(objTr.FG_Item_Code) > 0 Then
                            If clsCommon.myLen(objTr.RM_Item_Code) > 0 Then
                                objTr.RM_Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from TSPL_ITEM_MASTER where item_code='" + objTr.RM_Item_Code + "'"))
                                If clsCommon.myLen(objTr.RM_Item_Code) <= 0 Then
                                    Throw New Exception("Invalid item code [" + clsCommon.myCstr(gv.Rows(ii).Cells("RM_Item_Code").Value) + "]")
                                End If
                            End If

                            If clsCommon.myLen(objTr.FG_Item_Code) > 0 Then
                                objTr.FG_Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_code from TSPL_ITEM_MASTER where item_code='" + objTr.FG_Item_Code + "'"))
                                If clsCommon.myLen(objTr.FG_Item_Code) <= 0 Then
                                    Throw New Exception("Invalid item code [" + clsCommon.myCstr(gv.Rows(ii).Cells("FG_Item_Code").Value) + "]")
                                End If
                            End If
                            objTr.RM_Item_Name = clsItemMaster.GetItemName(objTr.RM_Item_Code, Nothing)
                            objTr.FG_Item_Name = clsItemMaster.GetItemName(objTr.FG_Item_Code, Nothing)
                            objTr.FG_Item_UOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Top 1 UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code ='" + objTr.FG_Item_Code + "'  and Stocking_Unit='Y'"))
                            objTr.FG_Item_Cost = clsCommon.myCstr(gv.Rows(ii).Cells("FG_Item_Cost").Value)

                            If clsCommon.myLen(objTr.FG_Item_Cost) <= 0 Then
                                Throw New Exception("Fill Item Charges " + clsCommon.myCstr(gv.Rows(ii).Cells("Charges").Value))
                            End If
                            arr.Add(objTr)
                        End If
                    Next
                    isInsideLoadData = True
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        LoadBlankGrid(gv1)
                        For Each objTr As clsJWIItemPriceDetail In arr
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRMCode).Value = objTr.RM_Item_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colRMName).Value = objTr.RM_Item_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFGCode).Value = objTr.FG_Item_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFGName).Value = objTr.FG_Item_Name
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFGUOM).Value = objTr.FG_Item_UOM
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFGCost).Value = objTr.FG_Item_Cost
                        Next
                    End If
                Catch ex As Exception
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

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Document found to Delete")
            Exit Sub
        End If

        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsJWIItemPriceMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnShowHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowHistory.Click
        Try
            clsERPFuncationalityOLD.ShowTransHistoryData(txtCode.Value, "Price_Code", "TSPL_JOB_OUTWARD_PRICE_HEAD", "TSPL_JOB_OUTWARD_PRICE_DETAIL")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkInactive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInactive.ToggleStateChanged ''ERO/29/03/19-000526 by balwinder on 01/04/2019
        Try
            If Not isInsideLoadData Then
                If chkInactive.Checked Then
                    If clsCommon.myLen(txtCode.Value) > 0 Then
                        If clsCommon.MyMessageBoxShow("Current Price code [" + txtCode.Value + "] will be inactive" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            If (clsJWIItemPriceMaster.InactiveData(txtCode.Value)) Then
                                clsCommon.MyMessageBoxShow("Successfully Inactivated")
                            End If
                        End If
                    End If
                End If
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGridVendor()
        gvVendor.Rows.Clear()
        gvVendor.Columns.Clear()

        Dim repoText As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "SNo"
        repoText.Name = ColSNo
        repoText.Width = 100
        repoText.ReadOnly = True
        repoText.IsVisible = True
        gvVendor.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Vendor Code"
        repoText.Name = colVendorCode
        repoText.Width = 100
        gvVendor.MasterTemplate.Columns.Add(repoText)

        repoText = New GridViewTextBoxColumn()
        repoText.FormatString = ""
        repoText.HeaderText = "Vendor Name"
        repoText.Name = ColVendorName
        repoText.Width = 150
        repoText.ReadOnly = True
        gvVendor.MasterTemplate.Columns.Add(repoText)


        gvVendor.AllowDeleteRow = True
        gvVendor.AllowAddNewRow = False
        gvVendor.ShowGroupPanel = False
        gvVendor.AllowColumnReorder = False
        gvVendor.AllowRowReorder = False
        gvVendor.EnableSorting = False
        gvVendor.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvVendor.MasterTemplate.ShowRowHeaderColumn = False
        gvVendor.TableElement.TableHeaderHeight = 40
        gvVendor.AutoSizeRows = False
        gvVendor.AllowRowReorder = True
        gvVendor.Rows.AddNew()
        '   ReStoreGridLayout()
    End Sub


    Private Sub gvVendor_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvVendor.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If e.Column Is gvVendor.Columns(colVendorCode) Then
                    OpenVendorCode(False)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenVendorCode(ByVal isButtonClick As Boolean)
        Dim Qry As String = " select Vendor_Code AS [Code], Vendor_Name as [Name],ISNULL(alies_name,'') As [Alies Name] from TSPL_VENDOR_MASTER  "

        gvVendor.CurrentRow.Cells(colVendorCode).Value = clsCommon.ShowSelectForm("VendSelectfnd", Qry, "Code", " Status='N'", clsCommon.myCstr(gvVendor.CurrentRow.Cells(colVendorCode).Value), "Code", isButtonClick)

        If clsCommon.myLen(clsCommon.myCstr(gvVendor.CurrentRow.Cells(colVendorCode).Value)) > 0 Then
            gvVendor.CurrentRow.Cells(ColVendorName).Value = clsVendorMaster.GetName(gvVendor.CurrentRow.Cells(colVendorCode).Value, Nothing)
            gvVendor.Rows.AddNew()
        Else
            gvVendor.CurrentRow.Cells(ColVendorName).Value = ""
        End If
    End Sub

    Private Sub gvVendor_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvVendor.CurrentColumnChanged
        If gvVendor.RowCount > 0 And gvVendor.CurrentRow.Index <> -1 Then
            Dim intCurrRow As Integer = gvVendor.CurrentRow.Index
            gvVendor.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        End If
    End Sub

    Private Sub btnVendorDetails_Click(sender As Object, e As EventArgs) Handles btnVendorDetails.Click
        Dim trans As SqlTransaction = Nothing
        Try
            Dim obj As New clsJWIItemPriceMaster()
            obj = clsJWIItemPriceMaster.GetData(txtCode.Value, NavigatorType.Current)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Price_Code) > 0) Then
                If ERPTransactionStatus.Approved Then
                    If (AllowToSave()) Then
                        obj.ArrVendor = New List(Of clsJWIVendorDetail)
                        For Each grow As GridViewRowInfo In gvVendor.Rows
                            Dim objTr As New clsJWIVendorDetail()
                            objTr.Vendor_Code = clsCommon.myCstr(grow.Cells(colVendorCode).Value)
                            objTr.Vendor_Name = clsCommon.myCstr(grow.Cells(ColVendorName).Value)
                            If (clsCommon.myLen(objTr.Vendor_Code) > 0) Then
                                obj.ArrVendor.Add(objTr)
                            End If
                        Next
                        trans = clsDBFuncationality.GetTransactin()
                        Qry = "Delete from TSPL_JWI_PRICE_VENDOR_DETAIL where Price_Code='" + obj.Price_Code + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                        If clsJWIVendorDetail.SaveData(txtCode.Value, obj.ArrVendor, trans) Then
                            clsCommon.MyMessageBoxShow("Successfully Updated", Me.Text)
                            trans.Commit()
                        Else
                            trans.Rollback()
                        End If

                    End If
                End If
            End If

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class

