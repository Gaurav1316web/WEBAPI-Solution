Imports common
Imports System.IO

Public Class frmSetPOSchedule
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isNewEntry As Boolean = False

    Const colLineNo As String = "colLineNo"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colPOQty As String = "colPOQty"
    Const colBalanceQty As String = "colBalanceQty"
    Const colQty As String = "colQty"
    Const colUnit As String = "colUnit"


    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        RadPageView1.SelectedPage = RadPageViewPage1

        LoadBlankGrid()
        AddNew()
        SetLength()

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields

        ''For Attachment
        UcAttachment1.Form_ID = MyBase.Form_ID
        RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        ''End of For Attachment
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtRmks.MaxLength = 200
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtScheduleDate.Value = txtDate.Value
        txtVendor.Value = ""
        lblVendorName.Text = ""
        txtPONo.Value = ""
        txtRmks.Text = ""
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
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
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "PO Qty"
        repoRate.Name = colPOQty
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)


        repoRate = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Balance Qty"
        repoRate.Name = colBalanceQty
        repoRate.ReadOnly = True
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Schedule Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.ReadOnly = True
        repoUnit.Width = 80
        gv1.MasterTemplate.Columns.Add(repoUnit)



        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

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
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If e.Column Is gv1.Columns(colQty) Then
                        If e.Column Is gv1.Columns(colQty) Then
                            'UpdateCurrentRow()
                            'UpdateAllTotals()
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

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub UpdateCurrentRow()
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            'Dim dblQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            'Dim dblRate As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colPOQty).Value)
            'Dim dblAmt As Double = (dblQty * dblRate)
            'gv1.CurrentRow.Cells(colAmt).Value = dblAmt
        End If
    End Sub



    Private Sub UpdateAllTotals()
        'Dim dblNetAmt As Double = 0
        'For ii As Integer = 0 To gv1.Rows.Count - 1
        '    If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
        '        dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
        '    End If
        'Next
        'lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        txtPONo.Value = ""
        txtDate.Focus()
        gv1.Rows.AddNew()
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()
        UcAttachment1.isDeleteTheAttachment = True
    End Sub

    Function AllowToSave() As Boolean
        Try
            UpdateAllTotals()
            ' = KUNAL > TICKET :  BM00000009580 ==============================
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If clsCommon.myLen(txtPONo.Value) <= 0 Then
                txtPONo.Focus()
                Throw New Exception("Plese enter RFQ No")
            End If
            If clsCommon.myLen(txtVendor.Value) <= 0 Then
                txtVendor.Focus()
                Throw New Exception("Plese enter Vendor")
            End If
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myCDecimal(grow.Cells(colQty).Value) > clsCommon.myCDecimal(grow.Cells(colBalanceQty).Value) Then
                    Throw New Exception("Item [" + clsCommon.myCstr(grow.Cells(colICode).Value) + "]  [" + clsCommon.myCstr(grow.Cells(colIName).Value) + "] Qty [" + clsCommon.myCstr(grow.Cells(colQty).Value) + "] cant be more than balance qty [" + clsCommon.myCstr(grow.Cells(colBalanceQty).Value) + "]")
                End If
            Next
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Sub SaveData(ByVal ChekBtnPost As Boolean, Optional ByVal isDoAbandomentNo As Boolean = False)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsSetPOSchedule()
                obj.Code = txtCode.Value
                obj.DDate = txtDate.Value
                obj.Schedule_Date = txtScheduleDate.Value
                obj.PO_No = txtPONo.Value
                obj.Vendor_Code = txtVendor.Value
                obj.Remarks = txtRmks.Text
                obj.ArrTr = New List(Of clsSetPOScheduleDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsSetPOScheduleDetail()
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.ArrTr.Add(objTr)
                    End If
                Next
                If (obj.ArrTr Is Nothing OrElse obj.ArrTr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colICode)
                End If
                ''End of For Custom Fields
                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Code)
                    If ChekBtnPost = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strDocumentNo As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            UsLock1.Status = ERPTransactionStatus.Pending

            BlankAllControls()
            LoadBlankGrid()
            Dim obj As New clsSetPOSchedule()
            obj = clsSetPOSchedule.GetData(strDocumentNo, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UcAttachment1.isDeleteTheAttachment = False
                End If

                txtCode.Value = obj.Code
                txtDate.Value = obj.DDate
                txtScheduleDate.Value = obj.Schedule_Date
                txtPONo.Value = obj.PO_No
                txtVendor.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtRmks.Text = obj.Remarks
                UsLock1.Status = obj.Status
                If obj.ArrTr IsNot Nothing Then
                    For Each objTr As clsSetPOScheduleDetail In obj.ArrTr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.UOM
                    Next
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(clsSetPOSchedule.GetPendingBaseQry(txtPONo.Value, txtCode.Value, txtVendor.Value))
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(dr("Item_Code")), clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)) = CompairStringResult.Equal Then
                                gv1.Rows(ii).Cells(colPOQty).Value = clsCommon.myCDecimal(dr("POQty"))
                                gv1.Rows(ii).Cells(colBalanceQty).Value = clsCommon.myCDecimal(dr("BalanceQty"))
                            End If
                        Next
                    Next
                End If


                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Code)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Code, MyBase.Form_ID, gv1)
                ''End of For Custom Fields
                UcAttachment1.LoadData(obj.Code)
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
                SaveData(True)
                If (clsSetPOSchedule.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully ", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
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
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsSetPOSchedule.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Name", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_SET_PO_SCHEDULE where Code='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = " select  Code,DDate as Date,Remarks,ISNULL(TSPL_SET_PO_SCHEDULE.Vendor_Code,'') As [Vendor Code],ISNULL(TSPL_VENDOR_MASTER.Vendor_Name,'') As [Vendor Name],ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name], case when TSPL_SET_PO_SCHEDULE.Status=0 then 'Pending' else 'Approved' end as [Status] 
from TSPL_SET_PO_SCHEDULE  
LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_SET_PO_SCHEDULE.Vendor_Code "
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("Doc@SetPOS", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked, "TSPL_SET_PO_SCHEDULE.DDate"), NavigatorType.Current)
    End Sub

    Private Sub frmPurchaseRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                         "TSPL_SET_PO_SCHEDULE " + Environment.NewLine +
                                         "TSPL_SET_PO_SCHEDULE_DETAIL ")
        End If
    End Sub



    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        'Try
        '    ''If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colComplete) = CompairStringResult.Equal AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
        '    If gv1.Columns(colIName) Is gv1.CurrentColumn AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
        '        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        '        Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
        '        Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
        '        If clsCommon.myLen(txtReqNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "N") = CompairStringResult.Equal Then
        '            If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
        '                If clsSetPOScheduleDetail.CompleteRequition(txtReqNo.Value, strICode, intSNo) Then
        '                    common.clsCommon.MyMessageBoxShow("Successfully Completed")
        '                    LoadData(txtReqNo.Value, NavigatorType.Current)
        '                End If
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Sub LoadItems()
        LoadBlankGrid()
        If clsCommon.myLen(txtPONo.Value) > 0 Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(clsSetPOSchedule.GetPendingBaseQry(txtPONo.Value, txtCode.Value, txtVendor.Value))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr("Item_Code")), Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPOQty).Value = clsCommon.myCDecimal(dr("POQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = clsCommon.myCDecimal(dr("BalanceQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCDecimal(dr("BalanceQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_code"))
                Next
            End If
        End If
    End Sub


    Private Sub DeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub

    Private Sub saveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saveLayoutbtn.Click
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub txtVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendor._MYValidating
        Try
            txtVendor.Value = clsVendorMaster.getFinder("", txtVendor.Value, isButtonClicked)
            lblVendorName.Text = clsVendorMaster.GetName(txtVendor.Value, Nothing)
            txtPONo.Value = ""
            LoadBlankGrid()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPONo._MYValidating
        Try
            If clsCommon.myLen(txtVendor.Value) <= 0 Then
                txtVendor.Focus()
                Throw New Exception("Please select vendor")
            End If
            Dim qry As String = "select * from (select PurchaseOrder_No as Code ,max(PurchaseOrder_Date) as PO_Date from (" + clsSetPOSchedule.GetPendingBaseQry("", txtCode.Value, txtVendor.Value) + ")xxx group by PurchaseOrder_No )xxxx "
            Dim whrclas As String = " TSPL_RFQ_HEAD.Is_Post not exists(select 1 from TSPL_SET_PO_SCHEDULE where TSPL_SET_PO_SCHEDULE.Requisition_Id= TSPL_RFQ_HEAD.Requisition_Id and TSPL_SET_PO_SCHEDULE.Code not in('" + txtCode.Value + "'))"
            txtPONo.Value = clsCommon.ShowSelectForm("POFind@Sch", qry, "Code", "", txtPONo.Value, "PO_Date", isButtonClicked)
            LoadItems()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
