Imports System
Imports common
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Data.SqlClient

Public Class frmTankerCleaningItem
    Inherits FrmMainTranScreen
#Region "Variables"
    Const colLineNo As String = "LNO"
    Const colICode As String = "ICODE"
    Const colIName As String = "INAME"
    Const colQty As String = "QTY"
    Const colUnit As String = "colUnit"

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsFormLoad As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        chkprclose.Enabled = MyBase.isPostFlag
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        SetLength()
        LoadBlankGrid()
        AddNew()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtReqNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
    End Sub

    Sub BlankAllControls()
        btnPost.Visible = MyBase.isPostFlag
        txtDesc.Enabled = True
        txtReqNo.Value = ""
        txtDesc.Text = ""
        chkprclose.Checked = False
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDate.Focus()
        txtLocation.Enabled = True
        gv1.Rows.AddNew()
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 30
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = My.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Req. Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.ReadOnly = False
        repoUnit.Width = 80
        gv1.MasterTemplate.Columns.Add(repoUnit)



        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
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

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colUnit) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        End If
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", True, isButtonClick, "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()
        BlankAllControls()
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("First select Location")
            End If
            If clsCommon.myLen(txtSubLocation.Value) <= 0 Then
                txtSubLocation.Focus()
                Throw New Exception("First select sub Location")
            End If
            Dim arrICode As New List(Of String)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                    If arrICode.Contains(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)) Then
                        Throw New Exception("Item can't be Repeated. [" + clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value) + "]")
                    Else
                        arrICode.Add(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value))
                    End If
                End If
            Next
            arrICode = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal ChekBtnPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsTankerCleaningItemHead()
                obj.Code = txtReqNo.Value
                obj.Apply_Date = txtDate.Value
                obj.Description = txtDesc.Text
                obj.Location = txtLocation.Value
                obj.Sub_Location = txtSubLocation.Value
                obj.Arr = New List(Of clsTankerCleaningItemDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsTankerCleaningItemDetail()
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, ByVal navType As common.NavigatorType)
        Try
            BlankAllControls()
            LoadBlankGrid()
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            UsLock1.Status = ERPTransactionStatus.Pending
            txtLocation.Enabled = False
            Dim obj As New clsTankerCleaningItemHead()
            obj = clsTankerCleaningItemHead.GetData(strDocumentNo, navType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                If chkprclose.Checked Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                txtReqNo.Value = obj.Code
                txtDate.Value = obj.Apply_Date
                UsLock1.Status = obj.Status
                txtDesc.Text = obj.Description
                txtLocation.Value = obj.Location
                lblLocation.Text = obj.LocationName
                txtSubLocation.Value = obj.Sub_Location
                lblSubLocation.Text = obj.Sub_LocationName
                chkprclose.Checked = obj.Is_Closed
                gv1.Rows.Clear()
                For Each objTr As clsTankerCleaningItemDetail In obj.Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.SNo
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                Next
                If obj.Status = ERPTransactionStatus.Pending Then
                    gv1.Rows.AddNew()
                End If
            Else
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsTankerCleaningItemHead.PostData(txtReqNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully ", Me.Text)
                    LoadData(txtReqNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsTankerCleaningItemHead.DeleteData(txtReqNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtReqNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_TANKER_CLEANING_ITEM_HEAD"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtReqNo.MyReadOnly = False
            Else
                txtReqNo.MyReadOnly = True
            End If
            LoadData(txtReqNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        Dim qry As String = "select Code as Code,convert(varchar,Apply_Date,103) as Date,Description, case when Status='0' then 'Pending' else 'Approved' end as [Status],TSPL_USER_MASTER.User_Name as [User Name] from TSPL_TANKER_CLEANING_ITEM_HEAD left join tspl_user_master on TSPL_USER_MASTER .User_Code =TSPL_TANKER_CLEANING_ITEM_HEAD.Created_By"
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("TnCleIt@MF", qry, "Code", whrClas, txtReqNo.Value, "Apply_Date desc", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            txtLocation.Value = clsCommon.ShowSelectForm("TnCleIt@LF", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            txtSubLocation.Value = ""
            lblSubLocation.Text = ""
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmPurchaseRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
                isCellValueChangedOpen = True
                If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                    gv1.CurrentColumn = gv1.Columns(colIName)
                    OpenICodeList(True)
                    gv1.CurrentColumn = gv1.Columns(colICode)
                ElseIf gv1.CurrentColumn Is gv1.Columns(colUnit) Then
                    gv1.CurrentColumn = gv1.Columns(colIName)
                    OpenUOMList(True)
                    gv1.CurrentColumn = gv1.Columns(colUnit)
                End If
                isCellValueChangedOpen = False
            ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
                AddNew()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
                SaveData(False)
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                PostData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
                DeleteData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
                CloseForm()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                             "TSPL_TANKER_CLEANING_ITEM_HEAD " + Environment.NewLine + _
                                             "TSPL_REQUISITION_DETAIL ")
                'Dim frm As New FrmPWD(Nothing)
                'frm.strType = "SIRC"
                'frm.strCode = "SIReversAndCreate"
                'frm.ShowDialog()
                'If frm.isPasswordCorrect Then
                '    btnUnpost.Visible = True
                'End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then

            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub chkprclose_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkprclose.CheckedChanged
        Try
            If Not isInsideLoadData Then
                If chkprclose.Checked = True Then
                    If clsCommon.MyMessageBoxShow(Me, "Are you sure want to close?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If (clsTankerCleaningItemHead.CloseprData(txtReqNo.Value)) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Data Closed Successfully ", Me.Text)
                            LoadData(txtReqNo.Value, NavigatorType.Current)
                        End If
                    Else
                        chkprclose.Checked = False
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub DeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        Try
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                txtLocation.Focus()
                Throw New Exception("First select Location")
            End If

            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical' and Main_Location_Code='" + txtLocation.Value + "' "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            txtSubLocation.Value = clsCommon.ShowSelectForm("TnCleIt@LF", qry, "Code", WhrCls, txtSubLocation.Value, "Code", isButtonClicked)
            lblSubLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtSubLocation.Value + "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class


