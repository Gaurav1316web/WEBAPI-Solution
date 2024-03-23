Imports common
Imports System.IO

Public Class frmVendorQuotation
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colLineNo As String = "LNO"
    Const ColRowType As String = "ORDERNO"
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
        If MyBase.isExport = True Then
            btnexport.Enabled = True
            btnimport.Enabled = True
        Else
            btnexport.Enabled = False
            btnimport.Enabled = False
        End If
    End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

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
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment
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

        txtRefNo.Text = ""
        txtRmks.Text = ""

        btnAmendment.Visible = True
        btnAmendment.Enabled = False

        txtComment.Text = ""

        chkOnHold.Checked = False
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")

        lblTotRAmt.Text = ""
        txtVendor.Value = ""
        lblVendorName.Text = ""
        txtRFQNo.Value = ""

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

        Dim repoOrderNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrderNo = New GridViewTextBoxColumn()
        repoOrderNo.FormatString = ""
        repoOrderNo.HeaderText = "Row Type"
        repoOrderNo.Name = ColRowType
        repoOrderNo.Width = 100
        repoOrderNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoOrderNo)

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
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) Then
                        If (e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate)) Then
                            UpdateCurrentRow()
                            UpdateAllTotals()
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
        txtRFQNo.Value = ""
        lblRequistionNo.Text = ""
        txtVendorQuotationNo.Text = ""
        txtDate.Focus()
        gv1.Rows.AddNew()

        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Function AllowToSave() As Boolean
        Try
            UpdateAllTotals()
            ' = KUNAL > TICKET :  BM00000009580 ==============================
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If clsCommon.myLen(txtRFQNo.Value) <= 0 Then
                txtRFQNo.Focus()
                Throw New Exception("Plese enter RFQ No")
            End If
            If clsCommon.myLen(txtVendor.Value) <= 0 Then
                txtVendor.Focus()
                Throw New Exception("Plese enter Vendor")
            End If
            If clsCommon.myLen(txtVendorQuotationNo.Text) <= 0 Then
                txtVendorQuotationNo.Focus()
                Throw New Exception("Plese enter vendor Quotation No")
            End If

          

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
                Dim obj As New ClsVendorQuotationHead()
                obj.Code = txtCode.Value
                obj.VQDate = txtDate.Value
                obj.RFQ_NO = txtRFQNo.Value
                obj.Requisition_Id = lblRequistionNo.Text
                obj.Vendor_Code = txtVendor.Value
                obj.Vendor_Name = lblVendorName.Text
                obj.Quotation_No = txtVendorQuotationNo.Text
                obj.Quotation_Date = txtVendorQuotationDate.Value
                obj.Description = txtDesc.Text
                obj.Ref_No = txtRefNo.Text
                obj.Remarks = txtRmks.Text
                obj.Comments = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Total_Amt = lblTotRAmt.Text
                obj.ArrTr = New List(Of ClsVendorQuotationDeatil)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New ClsVendorQuotationDeatil()
                    objTr.Line_No = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                    objTr.Row_Type = clsCommon.myCstr(grow.Cells(ColRowType).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Specification = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
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

                If (obj.SaveData(obj, isNewEntry, False, True)) Then
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
            Dim obj As New ClsVendorQuotationHead()
            obj = ClsVendorQuotationHead.GetData(strDocumentNo, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Requisition_Id) > 0) Then
                btnAmendment.Visible = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnAmendment.Enabled = True
                    btnAmendment.Visible = True
                End If

                txtCode.Value = obj.Code
                txtDate.Value = obj.VQDate
                txtRFQNo.Value = obj.RFQ_NO
                lblRequistionNo.Text = obj.Requisition_Id
                txtVendor.Value = obj.Vendor_Code
                lblVendorName.Text = obj.Vendor_Name
                txtVendorQuotationNo.Text = obj.Quotation_No
                txtVendorQuotationDate.Value = obj.Quotation_Date
                txtDesc.Text = obj.Description
                txtRefNo.Text = obj.Ref_No
                txtRmks.Text = obj.Remarks
                txtComment.Text = obj.Comments
                UsLock1.Status = obj.Status
                chkOnHold.Checked = obj.On_Hold
                lblTotRAmt.Text = clsCommon.myFormat(obj.Total_Amt)
                If obj.ArrTr IsNot Nothing Then
                    For Each objTr As ClsVendorQuotationDeatil In obj.ArrTr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColRowType).Value = objTr.Row_Type
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
                If (ClsVendorQuotationHead.PostData(txtCode.Value)) Then
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
                If (ClsVendorQuotationHead.DeleteData(txtCode.Value)) Then
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
            Dim qst As String = "select count(*) from TSPL_VENDOR_QUOTATION_HEAD where Code='" + txtCode.Value + "'"
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
        Dim qry As String = " select  Code,VQDate as Date,Description,ISNULL(TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code,'') As [Vendor Code],ISNULL(TSPL_VENDOR_MASTER.Vendor_Name,'') As [Vendor Name],ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name], case when TSPL_VENDOR_QUOTATION_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status] from TSPL_VENDOR_QUOTATION_HEAD"
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        qry += " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code "
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("VQReqfndNo", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
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
            'Add Tool tip Task No- TEC/22/05/18-000245
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                         "TSPL_VENDOR_QUOTATION_HEAD " + Environment.NewLine + _
                                         "TSPL_Vendor_Quotation_DETAIL ")
            'Add Tool tip Task No- TEC/22/05/18-000245
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, "Requisition Number", Me.Text)

        Else
            funPrint()
        End If

    End Sub

    Private Sub funPrint()

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
        '                If ClsVendorQuotationDeatil.CompleteRequition(txtReqNo.Value, strICode, intSNo) Then
        '                    common.clsCommon.MyMessageBoxShow("Successfully Completed")
        '                    LoadData(txtReqNo.Value, NavigatorType.Current)
        '                End If
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
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
            'If (e.RowIndex = gv1.CurrentRow.Index AndAlso e.ColumnIndex = gv1.CurrentColumn.Index) Then
            '    Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
            '    cell.GradientStyle = GradientStyles.Solid
            '    cell.BackColor = Color.FromArgb(243, 181, 51)
            'End If

            ''Dim columnIndex As Integer = e.CellElement.ColumnIndex
            ''Dim rowIndex As Integer = e.CellElement.RowIndex
            ''Dim cell As GridCellElement = e.CellElement
            ''If rowIndex = rowBeginIndex - 1 Then
            ''    If columnIndex = totalColumnIndex OrElse columnIndex = comissionColumnIndex OrElse columnIndex = feeColumnIndex OrElse columnIndex = clientColumnIndex Then

            ''        If cell.IsSelected Then
            ''            cell.BackColor = Color.FromArgb(162, 207, 223)
            ''        End If
            ''    End If
            ''ElseIf rowIndex > rowBeginIndex - 1 AndAlso rowIndex < rowBeginIndex + 8 Then
            ''    If columnIndex = totalColumnIndex OrElse columnIndex = comissionColumnIndex OrElse columnIndex = feeColumnIndex OrElse columnIndex = clientColumnIndex Then
            ''        If cell.IsSelected Then
            ''            cell.BackColor = Color.FromArgb(228, 227, 216)
            ''        End If
            ''    End If
            ''End If
            ''If IsNumber(e.CellElement.Value) Then
            ''    e.CellElement.Alignment = ContentAlignment.TopRight
            ''Else
            ''    e.CellElement.Alignment = ContentAlignment.TopLeft
            ''End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub

    Private Sub txtVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendor._MYValidating
        If clsCommon.myLen(lblRequistionNo.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Requisition no is Required for selecting vendor", Me.Text)
            Exit Sub
        End If
        '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
        Dim qry As String = "select TSPL_RFQ_DETAIL.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name] from TSPL_RFQ_DETAIL left outer join TSPL_RFQ_HEAD on TSPL_RFQ_HEAD.RFQ_NO=TSPL_RFQ_DETAIL.RFQ_NO left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_RFQ_DETAIL.Vendor_Code "
        Dim whrcls As String = "TSPL_RFQ_DETAIL.RFQ_NO='" + txtRFQNo.Value + "' and not exists(select 1 from TSPL_VENDOR_QUOTATION_HEAD where TSPL_VENDOR_QUOTATION_HEAD.Requisition_Id=TSPL_RFQ_HEAD.Requisition_Id and TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code=TSPL_RFQ_DETAIL.Vendor_Code and TSPL_VENDOR_QUOTATION_HEAD.Code not in ('" + txtCode.Value + "')) and TSPL_VENDOR_MASTER.Status='N' "
        txtVendor.Value = clsCommon.ShowSelectForm("VQVendofnd", qry, "Code", whrcls, txtVendor.Value, "Code", isButtonClicked)
        lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendor.Value + "'"))
        LoadItems()
    End Sub

    Sub LoadItems()
        LoadBlankGrid()
        If clsCommon.myLen(txtRFQNo.Value) > 0 Then
            lblRequistionNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Requisition_Id  from TSPL_RFQ_HEAD where RFQ_NO='" + txtRFQNo.Value + "'"))
            Dim obj As clsRequistionHead = clsRequistionHead.GetData(lblRequistionNo.Text, NavigatorType.Current, "")
            Dim arr As ArrayList = clsRFQDetailsItems.GetData(txtRFQNo.Value, txtVendor.Value, Nothing)

            If obj IsNot Nothing AndAlso obj.ArrTr.Count > 0 Then
                For Each objTr As clsRequistionDetail In obj.ArrTr
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        If Not arr.Contains(objTr.Item_Code) Then
                            Continue For
                        End If
                    End If

                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColRowType).Value = IIf(clsCommon.myLen(objTr.Row_Type) <= 0, "Item", objTr.Row_Type)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Requisition_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Item_Net_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = ""
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = ""
                Next
            End If

        End If
    End Sub
    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRFQNo._MYValidating
        Dim qry As String = "select RFQ_NO as Code,RFQ_Date as Date,Requisition_Id as [Requisition ID] from TSPL_RFQ_HEAD"
        Dim whrclas As String = " TSPL_RFQ_HEAD.Is_Post not exists(select 1 from TSPL_VENDOR_QUOTATION_HEAD where TSPL_VENDOR_QUOTATION_HEAD.Requisition_Id= TSPL_RFQ_HEAD.Requisition_Id and TSPL_VENDOR_QUOTATION_HEAD.Code not in('" + txtCode.Value + "'))"
        txtRFQNo.Value = clsCommon.ShowSelectForm("VQVendofnd", qry, "Code", "", txtRFQNo.Value, "Code", isButtonClicked)
        txtVendor.Value = ""
        LoadItems()
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


    Private Sub btnimporthead_Click(sender As Object, e As EventArgs) Handles btnimporthead.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Date", "RFQ No", "Requisition No", "Vendor Code", "Vendor Quotation No", "Vendor Quotation Date", "Description", "Reference No", "Remarks", "Comments") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsVendorQuotationHead()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    obj.Code = strCode

                    Dim docdate As String = clsCommon.myCstr(grow.Cells("Date").Value)
                    If clsCommon.myLen(docdate) <= 0 AndAlso IsDate(docdate) Then
                        Throw New Exception("Fill document date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    Else
                        obj.VQDate = clsCommon.GetPrintDate(docdate, "dd/MMM/yyyy hh:mm tt")
                    End If

                    Dim strRFQ As String = clsCommon.myCstr(grow.Cells("RFQ No").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strRFQ), "") <> CompairStringResult.Equal Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_RFQ_HEAD where RFQ_NO='" + clsCommon.myCstr(strRFQ) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("RFQ No not exists in RFQ.")
                        Else
                            obj.RFQ_NO = strRFQ
                        End If
                    End If

                    Dim strREQNo As String = clsCommon.myCstr(grow.Cells("Requisition No").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strREQNo), "") <> CompairStringResult.Equal Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_REQUISITION_HEAD where Requisition_Id='" + clsCommon.myCstr(strREQNo) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Requisition No not exists in Requisition.")
                        Else
                            obj.Requisition_Id = strREQNo
                        End If
                    End If

                    Dim strVenCode As String = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strVenCode), "") <> CompairStringResult.Equal Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_VENDOR_MASTER where Vendor_Code='" + clsCommon.myCstr(strVenCode) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Vendor Code not exists in vendor master.")
                        Else
                            obj.Vendor_Code = strVenCode
                        End If
                    End If

                    Dim strVenQtNo As String = clsCommon.myCstr(grow.Cells("Vendor Quotation No").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strVenQtNo), "") <> CompairStringResult.Equal Then
                        obj.Quotation_No = strVenQtNo
                    Else
                        Throw New Exception("Fill quotation no at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    Dim quotationdate As String = clsCommon.myCstr(grow.Cells("Vendor Quotation Date").Value)
                    If clsCommon.myLen(quotationdate) <= 0 AndAlso IsDate(quotationdate) Then
                        Throw New Exception("Fill quotation date at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    Else
                        obj.Quotation_Date = clsCommon.GetPrintDate(quotationdate, "dd/MMM/yyyy hh:mm tt")
                    End If

                    Dim strDescription As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    obj.Description = strDescription

                    Dim strRefNo As String = clsCommon.myCstr(grow.Cells("Reference No").Value)
                    obj.Ref_No = strRefNo

                    Dim strRemarks As String = clsCommon.myCstr(grow.Cells("Remarks").Value)
                    obj.Remarks = strRemarks

                    Dim strComments As String = clsCommon.myCstr(grow.Cells("Comments").Value)
                    obj.Comments = strComments

                    obj.SaveData(obj, ClsVendorQuotationHead.CheckNewEntry(obj.Code), True)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnimportdetail_Click(sender As Object, e As EventArgs) Handles btnimportdetail.Click
        Dim gv As New RadGridView()

        Dim obj As New ClsVendorQuotationHead()
        obj.ArrTr = New List(Of ClsVendorQuotationDeatil)
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Item Code", "Quantity", "Unit Code", "Unit Cost", "Specification", "Remarks") Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim objtr As New ClsVendorQuotationDeatil()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim dt1 As DataTable
                    dt1 = clsDBFuncationality.GetDataTable("select * from TSPL_VENDOR_QUOTATION_HEAD where Code='" + clsCommon.myCstr(strCode) + "'")
                    If dt1.Rows.Count <= 0 Then
                        Throw New Exception("Code not exists in vendor quotation.")
                    End If
                    objtr.Code = strCode
                    obj.Code = strCode

                    Dim strItemCode As String = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strItemCode), "") <> CompairStringResult.Equal Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(strItemCode) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Item code not exists in item master.")
                        Else
                            objtr.Item_Code = strItemCode
                            objtr.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                        End If
                    End If

                    Dim strQty As Double = clsCommon.myCdbl(grow.Cells("Quantity").Value)
                    If strQty <= 0 Then
                        Throw New Exception("Fill quantity at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    Else
                        objtr.Qty = strQty
                    End If

                    Dim strUom As String = clsCommon.myCstr(grow.Cells("Unit Code").Value)
                    If clsCommon.CompairString(clsCommon.myCstr(strUom), "") <> CompairStringResult.Equal Then
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable("select * from TSPL_ITEM_UOM_DETAIL where Item_Code='" + clsCommon.myCstr(strItemCode) + "' and UOM_Code='" + clsCommon.myCstr(strUom) + "'")
                        If dt.Rows.Count <= 0 Then
                            Throw New Exception("Unit code not exists in UOM.")
                        Else
                            objtr.Unit_Code = strUom
                        End If
                    End If

                    Dim strUnitCost As Double = clsCommon.myCdbl(grow.Cells("Unit Cost").Value)
                    If strUnitCost <= 0 Then
                        Throw New Exception("Fill unit cost at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    Else
                        objtr.Item_Cost = strUnitCost
                    End If

                    Dim strAmount As Double = clsCommon.myCdbl(strUnitCost * strQty)
                    objtr.Item_Net_Amt = strAmount

                    Dim strSpecification As String = clsCommon.myCstr(grow.Cells("Specification").Value)
                    objtr.Specification = strSpecification

                    Dim strRemarks As String = clsCommon.myCstr(grow.Cells("Remarks").Value)
                    objtr.Remarks = strRemarks

                    obj.ArrTr.Add(objtr)
                    'obj.a

                    Dim qry As String = "delete from TSPL_Vendor_Quotation_DETAIL where Code='" + obj.Code + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)

                Next

                ClsVendorQuotationDeatil.SaveData("", obj.ArrTr, Nothing, True)

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnexporthead_Click(sender As Object, e As EventArgs) Handles btnexporthead.Click
        Dim str As String
        str = "select TSPL_VENDOR_QUOTATION_HEAD.Code as [Code],TSPL_VENDOR_QUOTATION_HEAD.VQDate as [Date]," & _
                " TSPL_VENDOR_QUOTATION_HEAD.RFQ_NO as [RFQ NO],TSPL_VENDOR_QUOTATION_HEAD.Requisition_Id as [Requisition No]," & _
                " TSPL_VENDOR_QUOTATION_HEAD.Vendor_Code as [Vendor Code],TSPL_VENDOR_QUOTATION_HEAD.Quotation_No as [Vendor Quotation No],TSPL_VENDOR_QUOTATION_HEAD.Quotation_Date as [Vendor Quotation Date]," & _
                " TSPL_VENDOR_QUOTATION_HEAD.Description as [Description],TSPL_VENDOR_QUOTATION_HEAD.Ref_No as [Reference No],TSPL_VENDOR_QUOTATION_HEAD.Remarks as [Remarks]," & _
                " TSPL_VENDOR_QUOTATION_HEAD.Comments as [Comments] from TSPL_VENDOR_QUOTATION_HEAD"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub btnexportdetail_Click(sender As Object, e As EventArgs) Handles btnexportdetail.Click
        Dim str As String
        str = "select TSPL_VENDOR_QUOTATION_DETAIL.Code as [Code],TSPL_VENDOR_QUOTATION_DETAIL.Item_Code as [Item Code],TSPL_VENDOR_QUOTATION_DETAIL.Qty as [Quantity]," & _
                " TSPL_VENDOR_QUOTATION_DETAIL.Unit_Code as [Unit Code],TSPL_VENDOR_QUOTATION_DETAIL.Item_Cost as [Unit Cost]," & _
                " TSPL_VENDOR_QUOTATION_DETAIL.Specification AS [Specification],TSPL_VENDOR_QUOTATION_DETAIL.Remarks as [Remarks]" & _
                " from TSPL_VENDOR_QUOTATION_DETAIL"
        transportSql.ExporttoExcel(str, Me)
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
    
End Class
