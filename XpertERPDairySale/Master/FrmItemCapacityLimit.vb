Imports common

Public Class FrmItemCapacityLimit

#Region "Variable"
    Dim isNewEntry As Boolean = False
    Const ColSNo As String = "ColSNo"
    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colUOM As String = "colUOM"
    Const colMaxQty As String = "colMaxQty"

    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private ischkinactive As Boolean = False
#End Region
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBookingProductSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnReverse.Visible = False
        If MyBase.isExport = True Then
            btnImport.Enabled = True
            btnExport.Enabled = True
        Else
            btnImport.Enabled = False
            btnExport.Enabled = False
        End If
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
        If btnSave.Visible = True Then
            btnImport.Enabled = True
        Else
            btnImport.Enabled = False
        End If
    End Sub
    Private Sub FrmItemCapacityLimit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        AddNew()
        CreateTable()
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Public Sub AddNew()
        isNewEntry = True
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtStartDate.Value = txtDate.Value
        txtEndDate.Value = txtDate.Value
        chkInactive.Checked = False
        chkInactive.Enabled = False
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = False
        btnShowHistory.Enabled = False
        LoadBlankGrid()
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoitemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitemCode.FormatString = ""
        repoitemCode.HeaderText = "Item Code"
        repoitemCode.Name = colItemCode
        repoitemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoitemCode.Width = 180
        repoitemCode.HeaderImage = My.Resources.search4
        repoitemCode.IsVisible = True
        repoitemCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoitemCode)

        Dim repoItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemName.FormatString = ""
        repoItemName.HeaderText = "Item Name"
        repoItemName.Name = colItemName
        repoItemName.Width = 200
        repoItemName.IsVisible = True
        repoItemName.ReadOnly = True
        repoItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoItemName)
        Dim repoitemUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitemUOM.FormatString = ""
        repoitemUOM.HeaderText = "Item UOM"
        repoitemUOM.Name = colUOM
        repoitemUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoitemUOM.Width = 120
        repoitemUOM.HeaderImage = My.Resources.search4
        repoitemUOM.IsVisible = True
        repoitemUOM.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoitemUOM)
        Dim repoTextBox = New GridViewDecimalColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Max Qty"
        repoTextBox.Name = colMaxQty
        repoTextBox.Width = 120
        repoTextBox.Minimum = 0
        repoNumBox.ShowUpDownButtons = False
        repoNumBox.Step = 0
        repoTextBox.IsVisible = True
        repoitemUOM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight

        'repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)


        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = True
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnChooser = True
        gv1.AllowColumnReorder = True
        gv1.Rows.AddNew()
    End Sub
    Private Sub FrmItemCapacityLimit_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colUOM) Then
            isCellValueChangedOpen = True
            gv1.CurrentColumn = gv1.Columns(colItemCode)
            OpenUOMList(True)
            gv1.CurrentColumn = gv1.Columns(colUOM)

            setGridFocus()
            isCellValueChangedOpen = False
        ElseIf gv1.CurrentColumn Is gv1.Columns(colMaxQty) AndAlso e.KeyCode = Keys.Enter Then
            gv1.CurrentColumn = gv1.Columns(colMaxQty)
            setGridFocus()

        End If
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colItemCode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colUOM)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colUOM) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colMaxQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colMaxQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colItemCode)

            End If
        End If
    End Sub

    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub
    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        Try
            RefeshSNO()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCDecimal(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colItemCode) Then
                        OpenItemList(False)
                        'Dim strItemCode As String = clsItemCapacityLimitHead.getFinder(" Item_Type='F' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), False)
                        'gv1.CurrentRow.Cells(colItemCode).Value = strItemCode
                        'gv1.CurrentRow.Cells(colItemCode).Value = clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" + strItemCode + "' ")
                        'isCellValueChangedOpen = False
                        'Exit Sub
                    ElseIf e.Column Is gv1.Columns(colUOM) Then
                        OpenUOMList(False)
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenItemList(ByVal isButtonClick As Boolean)

        Dim whrCls As String = " Item_Type='F' and tspl_item_master.Active=1 "

        gv1.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), False)

        gv1.CurrentRow.Cells(colItemName).Value = clsDBFuncationality.getSingleValue("select distinct Short_Description from tspl_item_master where item_code='" + gv1.CurrentRow.Cells(colItemCode).Value + "' ")
        gv1.CurrentRow.Cells(colUOM).Value = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Default_UOM=1 and Item_Code='" & gv1.CurrentRow.Cells(colItemCode).Value & "' ")

    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("PS-BOUOMFndr", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)

        End If
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            If txtDocNo.MyReadOnly OrElse isButtonClicked Then
                Dim whrClas As String = "2=2"
                Dim qry As String = "select Document_No as Code,Document_Date,From_Date,To_Date,Created_By,Created_Date,Modified_By,Modified_Date,Posted,Posted_Date from TSPL_ITEM_CAPACITY_LIMIT_HEAD"
                LoadData(clsCommon.myCstr(clsCommon.ShowSelectForm("ICLBDMST", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked, "Document_Date")), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_ITEM_CAPACITY_LIMIT_HEAD where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtDocNo.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            For i As Integer = 0 To gv1.Rows.Count - 1
                If (clsCommon.CompairString(gv1.Rows(i).Cells(colItemCode).Value, Nothing) = CompairStringResult.Equal) Then
                    Continue For
                End If
                Dim ItemCode As String = clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value)
                For j As Integer = 0 To gv1.Rows.Count - 1
                    Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(j).Cells(colItemCode).Value)
                    If j = i Then
                        Continue For
                    End If
                    If clsCommon.CompairString(ItemCode, strInnerICode) = CompairStringResult.Equal Then
                        Throw New Exception("Same Item Exist at Row No " + clsCommon.myCstr(i + 1) + " And " + clsCommon.myCstr(j + 1))
                    End If
                Next
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If (AllowToSave()) Then
                SaveData()

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SaveData()
        Try
            Dim obj As New clsItemCapacityLimitHead()
            obj.Document_No = txtDocNo.Value
            obj.Document_Date = txtDate.Value
            obj.From_Date = txtStartDate.Value
            If txtEndDate.Checked Then
                obj.To_Date = txtEndDate.Value
            End If
            If chkInactive.Checked Then
                obj.IN_Active = True
            End If
            obj.Arr = GetTRData()
            obj.SaveData(obj, isNewEntry)
            clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
            LoadData(obj.Document_No, NavigatorType.Current)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function GetTRData() As List(Of clsItemCapacityLimitDetail)
        Dim Arr As New List(Of clsItemCapacityLimitDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0 Then
                Dim objTr As New clsItemCapacityLimitDetail()
                objTr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
                objTr.UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colUOM).Value)
                objTr.Qty = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colMaxQty).Value)
                Arr.Add(objTr)
            End If
        Next
        Return Arr
    End Function
    Public Sub LoadData(ByVal strDocNo As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            Dim obj As New clsItemCapacityLimitHead()
            obj = clsItemCapacityLimitHead.GetData(strDocNo, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                isNewEntry = False
                LoadBlankGrid()
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    chkInactive.Enabled = True
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                    chkInactive.Enabled = False
                End If
                btnShowHistory.Enabled = True
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtStartDate.Value = obj.From_Date
                If obj.To_Date IsNot Nothing Then
                    txtEndDate.Value = obj.To_Date

                End If
                If obj.IN_Active Then
                    ischkinactive = True
                    chkInactive.Checked = True
                    chkInactive.Enabled = False
                    ischkinactive = False
                End If
                Dim sl As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsItemCapacityLimitDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = sl
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = clsDBFuncationality.getSingleValue("select distinct Short_Description from tspl_item_master where item_code='" + clsCommon.myCstr(objTr.Item_Code) + "' ")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMaxQty).Value = objTr.Qty

                        sl += 1
                        gv1.Rows.AddNew()
                    Next
                    'GV1.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) >= 0 Then
                DeleteData()
            Else

                Throw New Exception("Document not found!")

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsItemCapacityLimitHead.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) >= 0 Then
                PostData()
            Else

                Throw New Exception("Document not found!")

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsItemCapacityLimitHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully ", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) >= 0 Then
                ReverseData()
            Else

                Throw New Exception("Document not found!")

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ReverseData()
        Try

        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
    End Sub
    Private Sub CreateTable()
        Dim coll As Dictionary(Of String, String)

        coll = New Dictionary(Of String, String)()
        coll.Add("Document_No", "Varchar(30) Not null Primary key")
        coll.Add("Document_Date", "date Not null")
        coll.Add("From_Date", "date not null")
        coll.Add("To_Date", "date null")
        coll.Add("IN_Active", "integer null")
        coll.Add("Posted", "integer NULL")
        coll.Add("Created_By", "varchar(12)  Not NULL")
        coll.Add("Created_Date", "datetime  Not NULL")
        coll.Add("Modified_By", "varchar(12)  Not NULL")
        coll.Add("Modified_Date", "datetime  Not NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "datetime NULL")
        coll.Add("InActive_By", "varchar(12) NULL")
        coll.Add("InActive_Date", "datetime null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", coll, "", True, False, "", "Document_No", "Document_Date")
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_No", "Varchar(30) Not null references TSPL_ITEM_CAPACITY_LIMIT_HEAD(Document_No)")
        coll.Add("Item_Code", "VARCHAR(50) Not null REFERENCES TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("UOM", "varchar(12) Not NULL REFERENCES TSPL_UNIT_MASTER(UNIT_CODE)")
        coll.Add("Qty", "Decimal(18,6) Not null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ITEM_CAPACITY_LIMIT_DETAIL", coll, "", True, False, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", "Document_No", "")
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("REF_PK_ID", "integer NOT NULL references TSPL_ITEM_CAPACITY_LIMIT_DETAIL(PK_ID)")
        coll.Add("Document_No", "Varchar(30) Not null references TSPL_ITEM_CAPACITY_LIMIT_HEAD(Document_No)")
        coll.Add("Item_Code", "VARCHAR(50) Not null REFERENCES TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("UOM", "varchar(12) Not NULL REFERENCES TSPL_UNIT_MASTER(UNIT_CODE)")
        coll.Add("Qty", "Decimal(18,6) Not null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_ITEM_CAPACITY_LIMIT_DETAIL_ALL_UOM", coll, "", True, False, "TSPL_ITEM_CAPACITY_LIMIT_HEAD", "Document_No", "")

    End Sub

    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick
        Try
            If clscommon.myLen(txtDocNo.value) > 0 Then


                If gv1.CurrentColumn Is gv1.Columns(ColSNo) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
                    Dim frm As New FrmFreeGrid
                    Dim qry As String = "select Item_Code,UOM,Qty from TSPL_ITEM_CAPACITY_LIMIT_DETAIL_ALL_UOM where Document_No='" + txtDocNo.Value + "' and Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) + "'"
                    frm.dt = clsDBFuncationality.GetDataTable(qry)
                    If frm.dt Is Nothing OrElse frm.dt.Rows.Count <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "No data Found", Me.Text)
                        Exit Sub
                    End If
                    frm.strFormName = "All UOM Item Capacity Limit For : " + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
                    'frm.WindowState = FormWindowState.Maximized
                    frm.ReportID = "ALLUOMLIMIT"
                    frm.Show()
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkInactive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInactive.ToggleStateChanged
        Try
            If Not ischkinactive Then
                If chkInactive.Checked Then
                    If clsCommon.myLen(txtDocNo.Value) > 0 Then
                        If common.clsCommon.MyMessageBoxShow("Do you want to Inactive this record. ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                            Dim qry As String = "update TSPL_ITEM_CAPACITY_LIMIT_HEAD set IN_Active=1 where Document_No='" + txtDocNo.Value + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry)
                            LoadData(txtDocNo.Value, NavigatorType.Current)
                        Else
                            chkInactive.Checked = False
                        End If
                    Else
                        Throw New Exception("Please select Document")

                    End If

                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnShowHistory_Click(sender As Object, e As EventArgs) Handles btnShowHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtDocNo.Value, "Document_No", "TSPL_ITEM_CAPACITY_LIMIT_HEAD")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Export()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Import()
    End Sub
    Public Sub Import()
        Try
            AddNew()
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim obj As New List(Of clsItemCapacityLimitDetail)
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Item Code", "UOM", "Qty") Then
                Dim linno As Integer = 0
                Dim TempNewRecord As Boolean = False
                Try
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim Arr As New clsItemCapacityLimitDetail()
                        linno += 1
                        If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Item Code").Value))) Then
                            Continue For
                        Else
                            Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code from TSPL_Item_Master where Item_Code='" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "'"))
                            If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Item Code").Value)) = CompairStringResult.Equal Then
                                Arr.Item_Code = clsCommon.myCstr(grow.Cells("Item Code").Value)
                            Else
                                Continue For
                            End If
                        End If
                        If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("UOM").Value))) Then
                            Continue For
                        Else
                            Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" + clsCommon.myCstr(grow.Cells("Item Code").Value) + "' and UOM_Code='" + clsCommon.myCstr(grow.Cells("UOM").Value) + "'"))
                            If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("UOM").Value)) = CompairStringResult.Equal Then
                                Arr.UOM = clsCommon.myCstr(grow.Cells("UOM").Value)
                            Else
                                Continue For
                            End If
                        End If
                        If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Qty").Value)) AndAlso clsCommon.myCdbl(grow.Cells("Qty").Value) <= 0) Then
                            Continue For
                        Else
                            Arr.Qty = clsCommon.myCDecimal(grow.Cells("Qty").Value)
                        End If

                        obj.Add(Arr)
                    Next
                    clsCommon.ProgressBarHide()
                    If clsCommon.MyMessageBoxShow(Me, "Total Correct Document [" + clsCommon.myCstr(obj.Count) + "] out of [" + clsCommon.myCstr(linno) + "] Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        Dim sl As Integer = 1
                        'AddNew()
                        LoadBlankGrid()
                        If obj IsNot Nothing AndAlso obj.Count > 0 Then
                            isInsideLoadData = True
                            For Each objTr As clsItemCapacityLimitDetail In obj
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = sl
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(objTr.Item_Code) + "' ")
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colMaxQty).Value = objTr.Qty

                                sl += 1
                                gv1.Rows.AddNew()
                            Next
                            isInsideLoadData = False
                        End If
                        common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Failed", Me.Text, MessageBoxButtons.OK)
                    End If
                    clsCommon.ProgressBarHide()
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            Else
                clsCommon.MyMessageBoxShow(Me, "Excel Sheet is not in expected format", Me.Text)
            End If

            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Export()
        Try
            Dim str As String = "select Item_Code as [Item Code],UOM as [UOM],Qty from TSPL_ITEM_CAPACITY_LIMIT_DETAIL"
            Dim whrCls As String = ""
            ListImpExpColumnsMandatory = New List(Of String)({"Item Code", "UOM", "Qty"})
            transportSql.ExporttoExcel(str, whrCls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class