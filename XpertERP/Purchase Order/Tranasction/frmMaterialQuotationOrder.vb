'Sanjay Ticket No  BHA/26/06/18-000083 Dated  26/Jun/2018 of Client  Bharat Dairy 
Imports common
Imports System.IO

Public Class frmMaterialQuotationOrder
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim AllowAssetItem As Boolean = False
    Const colLineNo As String = "LNO"
    Const colICode As String = "ICODE"
    Const colIName As String = "INAME"
    Const colQty As String = "QTY"
    Const colUnit As String = "COLTAX3"
    Const colRate As String = "RATE"
    Const colAmt As String = "AMT"
    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"

    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.VendorQuotation)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmMaterialQuotationOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        LoadBlankGrid()
        AddNew()
        SetLength()

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 200

        txtRefNo.MaxLength = 50
        txtRmks.MaxLength = 200
        txtComment.MaxLength = 200

    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtMaterialQuotation.Value = ""
        txtCustomer.Value = ""
        txtcustdesc.Text = ""
        txtRefNo.Text = ""
        txtRmks.Text = ""
        fndLocation.Value = ""
        txtlocation.Text = ""
        btnAmendment.Visible = True
        btnAmendment.Enabled = False

        txtComment.Text = ""

        chkOnHold.Checked = False
        chkTaxable.Checked = False
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")

        lblTotRAmt.Text = ""
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

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.ReadOnly = True
        repoUnit.Width = 80
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Cost"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.ReadOnly = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpecification = New GridViewTextBoxColumn()
        repoSpecification.FormatString = ""
        repoSpecification.HeaderText = "Specification"
        repoSpecification.Name = colSpecification
        repoSpecification.Width = 100
        gv1.MasterTemplate.Columns.Add(repoSpecification)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRemarks)

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
                    'If e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) Then
                    If (e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate)) Then
                        UpdateCurrentRow()
                        UpdateAllTotals()
                        'ElseIf gv1.CurrentColumn Is gv1.Columns(colICode) Then
                        '    OpenICodeList(False)
                        'ElseIf gv1.CurrentColumn Is gv1.Columns(colUnit) Then
                        '    OpenUOMList(False)
                    End If
                    'End If
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
            Dim dblQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value)
            Dim dblAmt As Double = (dblQty * dblRate)
            gv1.CurrentRow.Cells(colAmt).Value = dblAmt
        End If
    End Sub

    Private Sub BlankTaxDetailsCurrentRowWihtRowNo(ByVal intRowNo As Integer)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
        Next
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
            End If
        Next
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()

    End Sub

    Sub AddNew()
    
        BlankAllControls()
        btnAmendment.Visible = False
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDate.Focus()
        gv1.Rows.AddNew()
    End Sub

    Function AllowToSave() As Boolean
        Try
            UpdateAllTotals()
            ' = KUNAL > TICKET :  BM00000009580 ==============================
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If clsCommon.myLen(txtMaterialQuotation.Value) <= 0 Then
                txtMaterialQuotation.Focus()
                Throw New Exception("Plese Select Material Quotation")
            End If

            If clsCommon.myLen(txtCustomer.Value) <= 0 Then
                txtCustomer.Focus()
                Throw New Exception("Plese Select Customer")
            End If

            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                fndLocation.Focus()
                Throw New Exception("Please select Location")
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) <= 0 Then
                    gv1.CurrentRow = gv1.Rows(ii)
                    clsCommon.MyMessageBoxShow("Quantity should not be 0(zero) at row no. " + clsCommon.myCstr(ii + 1) + ".")
                    Return False
                End If
                '=====================added by Preeti Gupta [31/01/2017]
                If clsCommon.myLen(gv1.Rows(ii).Cells(colUnit).Value) <= 0 Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                        common.clsCommon.MyMessageBoxShow("Please enter UOM for Item : " + strIName + " . At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        Return False
                    End If
                End If
                '=======================================================

                If clsCommon.myLen(strICode) > 0 Then
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If (ii = jj) Then
                            Continue For
                        End If
                        If (clsCommon.CompairString(strICode, clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)) = CompairStringResult.Equal) Then
                            common.clsCommon.MyMessageBoxShow("Duplicate Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                            Return False
                        End If
                    Next

                End If
            Next

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
                Dim obj As New ClsMaterialQuotationOrderHead()
                obj.Code = txtCode.Value
                obj.QODate = txtDate.Value
                obj.Location_Code = fndLocation.Value
                obj.ScrapQuotation_Code = txtMaterialQuotation.Value
                obj.Description = txtDesc.Text
                obj.Ref_No = txtRefNo.Text
                obj.Remarks = txtRmks.Text
                obj.Comments = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Is_Taxable = chkTaxable.Checked
                obj.Total_Amt = lblTotRAmt.Text
                obj.Customer_Code = txtCustomer.value
                obj.ArrTr = New List(Of ClsMaterialQuotationOrderDeatil)


                For Each grow As GridViewRowInfo In gv1.Rows
                    If (clsCommon.myLen(grow.Cells(colICode).Value) > 0) Then
                        'obj.ArrTr.Add(objTr)
                        Dim objTr As New ClsMaterialQuotationOrderDeatil()
                        objTr.Line_No = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                        objTr.Specification = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        'If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.ArrTr.Add(objTr)
                        'End If
                    End If
                Next

                If (obj.ArrTr Is Nothing OrElse obj.ArrTr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If

                If (obj.SaveData(obj, isNewEntry, False, True)) Then
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
            Dim obj As New ClsMaterialQuotationOrderHead()
            obj = ClsMaterialQuotationOrderHead.GetData(strDocumentNo, navType)
            If (obj IsNot Nothing) Then
                btnAmendment.Visible = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnAmendment.Enabled = True
                    btnAmendment.Visible = True
                End If

                txtCode.Value = obj.Code
                txtDate.Value = obj.QODate
                txtMaterialQuotation.Value = obj.ScrapQuotation_Code
                fndLocation.Value = obj.Location_Code
                txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
                txtCustomer.Value = obj.Customer_Code
                txtcustdesc.Text = clsDBFuncationality.getSingleValue("select isnull(TSPL_customer_MASTER.Customer_Name,'') from TSPL_customer_MASTER where TSPL_customer_MASTER.cust_code='" + txtCustomer.Value + "'")
                txtDesc.Text = obj.Description
                txtRefNo.Text = obj.Ref_No
                txtRmks.Text = obj.Remarks
                txtComment.Text = obj.Comments
                UsLock1.Status = obj.Status
                chkOnHold.Checked = obj.On_Hold
                chkTaxable.Checked = obj.Is_Taxable
                lblTotRAmt.Text = clsCommon.myFormat(obj.Total_Amt)
                If obj.ArrTr IsNot Nothing Then
                    For Each objTr As ClsMaterialQuotationOrderDeatil In obj.ArrTr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Item_Net_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                    Next
                End If

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
        Try
            If chkOnHold.Checked = True Then
                chkOnHold.Focus()
                clsCommon.MyMessageBoxShow("Please Unhold the record first. ", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData(True)
                If (ClsMaterialQuotationOrderHead.PostData(txtCode.Value)) Then
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
                If (ClsMaterialQuotationOrderHead.DeleteData(txtCode.Value)) Then
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
            Dim qst As String = "select count(*) from TSPL_SCRAP_QUOTATION_ORDER_HEAD where Code='" + txtCode.Value + "'"
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
        Dim qry As String = " select  Code,QODate as Date,Description, case when TSPL_SCRAP_QUOTATION_ORDER_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status] from TSPL_SCRAP_QUOTATION_ORDER_HEAD"
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        'qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code "
        Dim whrClas As String = " 1=1 "
        LoadData(clsCommon.ShowSelectForm("QReqfndNo", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
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
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                         "TSPL_SCRAP_QUOTATION_ORDER_HEAD " + Environment.NewLine + _
                                         "TSPL_SCRAP_QUOTATION_ORDER_DETAIL ")
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtCode.Value = "" Then
            myMessages.blankValue("Requisition Number")

        Else
            funPrint()
        End If

    End Sub

    Private Sub funPrint()

    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
       
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
          
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub


    Private Sub DeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub


   
    Private Sub btnAmendment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAmendment.Click
        Try
            Dim isDoAbandomentNo As Boolean = False
            Dim Reason As String = ""
            If UsLock1.Status = ERPTransactionStatus.Approved Then
                If common.clsCommon.MyMessageBoxShow("Do you want to make Amendment", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    isDoAbandomentNo = True
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Amendment"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
            End If

            Dim IsSavedData As Boolean
            SaveData(False, True)
            saveCancelLog(Reason, "Amendment", Nothing)

            If IsSavedData Then
                common.clsCommon.MyMessageBoxShow(Me, "Successfully Amendmented", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub gv1_CellValueNeeded(sender As Object, e As GridViewCellValueEventArgs) Handles gv1.CellValueNeeded

    End Sub


    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        ' ''RICHA AGARWAL 23/03/2015 CHANGE ITEM TYPE FROM "F" TO "A"
        ' ''=======================30/03/2017  Monika===================================
        Dim strItemType As String = "A"
        If AllowAssetItem Then
            strItemType = ""
        End If
        ' ''============================================================================

        Dim obj As ClsScrapSaleDetail = ClsScrapSaleDetail.FinderItemGST(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), strItemType, isButtonClick, txtDate.Value, False, False)


        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            'gv1.CurrentRow.Cells(colHSNNo).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
            gv1.CurrentRow.Cells(colRate).Value = obj.price
            'SetitemWiseTaxSetting(True, True)
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            'gv1.CurrentRow.Cells(colHSNNo).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colRate).Value = 0
        End If



    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("scrapsItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
        End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
       
    End Sub

    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomer._MYValidating
        Try

            txtCustomer.Value = ""
            txtcustdesc.Text = ""

            If clsCommon.myLen(txtMaterialQuotation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Quotation First.", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                txtMaterialQuotation.Focus()
                Exit Sub
            End If

            Dim qry1 As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
            qry1 += " RIGHT join TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL "
            qry1 += " ON TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL.Customer_Code = tspl_customer_master.Cust_Code"
            qry1 += " where TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL.Code='" & txtMaterialQuotation.Value & "' and TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL.Customer_Code not in (select Customer_Code from TSPL_SCRAP_QUOTATION_ORDER_HEAD where ScrapQuotation_Code='" & txtMaterialQuotation.Value & "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
            If (dt Is Nothing OrElse dt.Rows.Count = 0) Then
                clsCommon.MyMessageBoxShow("Quotation Order already created against this Quotation for all customer.", Me.Text, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If


            Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
            qry += " RIGHT join TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL "
            qry += " ON TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL.Customer_Code = tspl_customer_master.Cust_Code"
            Dim WhrCls As String = " TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL.Code='" & txtMaterialQuotation.Value & "' and TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL.Customer_Code not in (select Customer_Code from TSPL_SCRAP_QUOTATION_ORDER_HEAD where ScrapQuotation_Code='" & txtMaterialQuotation.Value & "')"
            txtCustomer.Value = clsCommon.ShowSelectForm("CustMasterID", qry, "Code", WhrCls, txtCustomer.Value, "Code", isButtonClicked)

            txtcustdesc.Text = clsDBFuncationality.getSingleValue("select isnull(TSPL_customer_MASTER.Customer_Name,'') from TSPL_customer_MASTER where TSPL_customer_MASTER.cust_code='" + txtCustomer.Value + "'")

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadQuotation(ByVal MaterialQuotation As String)
        Try

            Dim obj As New ClsMaterialQuotationHead()
            obj = ClsMaterialQuotationHead.GetData(txtMaterialQuotation.Value, NavigatorType.Current)
            If (obj IsNot Nothing) Then


                'txtCode.Value = obj.Code
                txtDate.Value = obj.QDate
                'txtMaterialQuotation.Value = obj.ScrapQuotation_Code
                txtDesc.Text = obj.Description
                txtRefNo.Text = obj.Ref_No
                txtRmks.Text = obj.Remarks
                txtComment.Text = obj.Comments
                fndLocation.Value = obj.Location_Code
                txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
                chkTaxable.Checked = obj.Is_Taxable
                'UsLock1.Status = obj.Status
                'chkOnHold.Checked = obj.On_Hold
                lblTotRAmt.Text = clsCommon.myFormat(obj.Total_Amt)
                If obj.ArrTr IsNot Nothing Then
                    For Each objTr As ClsMaterialQuotationDeatil In obj.ArrTr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Item_Net_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                    Next
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadLabel1_Click(sender As Object, e As EventArgs) Handles lblQuotationOrder.Click

    End Sub

    Private Sub txtMaterialQuotation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMaterialQuotation._MYValidating
        BlankAllControls()
        LoadBlankGrid()
        Dim qry As String = " select  Code,QDate as Date,Description, case when TSPL_SCRAP_QUOTATION_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status] from TSPL_SCRAP_QUOTATION_HEAD"
        Dim whrClas As String = " TSPL_SCRAP_QUOTATION_HEAD.Status=1"
        txtMaterialQuotation.Value = clsCommon.ShowSelectForm("QfndNo", qry, "Code", whrClas, txtMaterialQuotation.Value, "Code", isButtonClicked)
        LoadQuotation(txtMaterialQuotation.Value)
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
        Dim WhrCls As String = " Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        fndLocation.Value = clsCommon.ShowSelectForm("LocTnMstFND", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
        txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
    End Sub
End Class
