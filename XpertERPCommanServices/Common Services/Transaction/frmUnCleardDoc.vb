Imports common
Imports System.IO

Public Class frmUnCleardDoc
    Inherits FrmMainTranScreen
    ''Against ticket BM00000007856
#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False


    Const colSelect As String = "colSelect"
    Const colLineNo As String = "colLineNo"
    Const colClearDocNo As String = "colClearDocNo"
    Const colClearDocDate As String = "colClearDocDate"
    Const colDocAmt As String = "colDocAmt"
    Const colApplyAmt As String = "colApplyAmt"
    Const colOutstandingAmt As String = "colOutstandingAmt"
    Const colDateToBe As String = "colDateToBe"
    Dim ButtonToolTip As ToolTip = New ToolTip()
#End Region

    'Private Sub SetUserMgmtNew()
    '    'MyBase.SetUserMgmt(clsUserMgtCode.UnCleardDoc)
    '    If Not (MyBase.isReadFlag) Then
    '        Throw New Exception("Permission Denied")
    '    End If
    '    btnSave.Visible = MyBase.isModifyFlag
    '    btnPost.Visible = MyBase.isPostFlag
    '    btnDelete.Visible = MyBase.isDeleteFlag
    'End Sub

    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        LoadType()
        AddNew()
        SetLength()
    End Sub

    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Receipt"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Payment"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub

    Sub SetLength()
        txtCode.MyMaxLength = 30
        txtDesc.MaxLength = 200
    End Sub

    Sub BlankAllControls()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtFromDate.Value = txtDate.Value.AddMonths(-1)
        txtToDate.Value = txtDate.Value
        cboType.SelectedValue = "P"
        txtVendor.Value = ""
        lblVendorName.Text = ""
        txtDesc.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtUnclearedDoc.Value = ""
        lblAppliedAmt.Text = ""
        lblDocAmt.Text = ""
        lblOSAmt.Text = ""
        LoadBlankGrid()
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 30
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoOrderNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrderNo.FormatString = ""
        repoOrderNo.HeaderText = "Document No"
        repoOrderNo.Name = colClearDocNo
        repoOrderNo.Width = 300
        repoOrderNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoOrderNo)

        Dim repoExpiry As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Document Date"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colClearDocDate
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = True
        repoExpiry.Width = 80
        gv1.MasterTemplate.Columns.Add(repoExpiry)

        repoExpiry = New GridViewDateTimeColumn()
        repoExpiry.Format = DateTimePickerFormat.Custom
        repoExpiry.CustomFormat = "dd-MM-yyyy"
        repoExpiry.HeaderText = "Date To Be"
        repoExpiry.FormatString = "{0:d}"
        repoExpiry.Name = colDateToBe
        repoExpiry.WrapText = True
        repoExpiry.ReadOnly = False
        repoExpiry.Width = 80
        gv1.MasterTemplate.Columns.Add(repoExpiry)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Document Amount"
        repoAmt.Name = colDocAmt
        repoAmt.Width = 100
        repoAmt.ReadOnly = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Apply Amount"
        repoRate.Name = colApplyAmt
        repoRate.Width = 100
        repoRate.Minimum = 0
        repoRate.ReadOnly = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Outstanding Amount"
        repoQty.Name = colOutstandingAmt
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.ReadOnly = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)



        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
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
   

    Private Sub UpdateCurrentRow(ByVal SelNewVal As Boolean)
        If SelNewVal Then
            If clsCommon.myCdbl(lblOSAmt.Text) > clsCommon.myCdbl(gv1.CurrentRow.Cells(colDocAmt).Value) Then
                gv1.CurrentRow.Cells(colApplyAmt).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDocAmt).Value)
            Else
                gv1.CurrentRow.Cells(colApplyAmt).Value = clsCommon.myCdbl(lblOSAmt.Text)
            End If
        Else
            gv1.CurrentRow.Cells(colApplyAmt).Value = 0
        End If
    End Sub

   

    Private Sub UpdateAllTotals()
        Dim dblApplyAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(ii).Cells(colOutstandingAmt).Value = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDocAmt).Value) - clsCommon.myCdbl(gv1.Rows(ii).Cells(colApplyAmt).Value)
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colSelect).Value)) Then
                dblApplyAmt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colApplyAmt).Value)
            End If
        Next
        lblOSAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(lblDocAmt.Text) - dblApplyAmt)
        lblAppliedAmt.Text = clsCommon.myFormat(dblApplyAmt)
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
       
        txtDate.Focus()


        

    End Sub

    Function AllowToSave() As Boolean
        Try
            btnSave.Focus()

            gv1.CurrentColumn = gv1.Columns(colClearDocNo)


            UpdateAllTotals()
            gv1.CurrentColumn = gv1.Columns(colDateToBe)
            If clsCommon.myLen(txtVendor.Value) <= 0 Then
                txtVendor.Focus()
                Throw New Exception("Plese enter " + MyLabel3.Text)
            End If
            If clsCommon.myLen(txtUnclearedDoc.Value) <= 0 Then
                txtUnclearedDoc.Focus()
                Throw New Exception("Plese select Document ")
            End If
            Dim qry As String = ""
            If clsCommon.CompairString("R", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
                qry = "select Receipt_Date from TSPL_RECEIPT_HEADER where Receipt_No='" + txtUnclearedDoc.Value + "'"
            Else
                qry = "select Payment_Date from TSPL_PAYMENT_HEADER where Payment_No='" + txtUnclearedDoc.Value + "'"
            End If

            Dim dtUnclededDocdate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry))
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells(colSelect).Value) Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colDateToBe).Value) <= 0 Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        gv1.CurrentColumn = gv1.Columns(colDateToBe)
                        Throw New Exception("Please enter To Be Date at row no" + clsCommon.myCstr(ii + 1))
                    End If
                    Dim dtToBe As Date = clsCommon.myCDate(gv1.Rows(ii).Cells(colDateToBe).Value)
                    If dtToBe.Month <> dtUnclededDocdate.Month Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        gv1.CurrentColumn = gv1.Columns(colDateToBe)
                        Throw New Exception("Month should be same as uncleard document i.e. " + clsCommon.myCstr(dtUnclededDocdate.Month) + " Please enter correct To Be Date at row no" + clsCommon.myCstr(ii + 1))
                    End If
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

    Sub SaveData(ByVal ChekBtnPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New clsUnclearedDocumentHead()
                obj.DOC_No = txtCode.Value
                obj.DOC_Date = txtDate.Value
                obj.Doc_Type = clsCommon.myCstr(cboType.SelectedValue)
                obj.Business_Partner_Code = txtVendor.Value
                obj.Remarks = txtDesc.Text
                obj.Cleared_Doc_No = txtUnclearedDoc.Value
                obj.Doc_Amount = clsCommon.myCdbl(lblDocAmt.Text)
                obj.Apply_Amount = clsCommon.myCdbl(lblAppliedAmt.Text)
                obj.Outstand_Amount = clsCommon.myCdbl(lblOSAmt.Text)

                obj.Arr = New List(Of clsUnclearedDocumentDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells(colSelect).Value) AndAlso clsCommon.myCdbl(grow.Cells(colApplyAmt).Value) > 0 Then
                        Dim objTr As New clsUnclearedDocumentDetail()
                        objTr.Cleared_Doc_No = clsCommon.myCstr(grow.Cells(colClearDocNo).Value)
                        objTr.Date_To_Be = clsCommon.myCstr(grow.Cells(colDateToBe).Value)
                        objTr.Doc_Amount = clsCommon.myCdbl(grow.Cells(colDocAmt).Value)
                        objTr.Apply_Amount = clsCommon.myCdbl(grow.Cells(colApplyAmt).Value)
                        objTr.Outstand_Amount = clsCommon.myCdbl(grow.Cells(colOutstandingAmt).Value)
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Exit Sub
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    If ChekBtnPost = False Then
                        common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.DOC_No, NavigatorType.Current)
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


            BlankAllControls()
            LoadBlankGrid()
            Dim obj As clsUnclearedDocumentHead = clsUnclearedDocumentHead.GetData(strDocumentNo, navType, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DOC_No) > 0) Then
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If

                txtCode.Value = obj.DOC_No
                txtDate.Value = obj.DOC_Date
                cboType.SelectedValue = obj.Doc_Type
                txtVendor.Value = obj.Business_Partner_Code
                lblVendorName.Text = obj.Business_Partner_Name
                txtUnclearedDoc.Value = obj.Cleared_Doc_No
                txtDesc.Text = obj.Remarks
                lblDocAmt.Text = clsCommon.myFormat(obj.Doc_Amount)
                lblAppliedAmt.Text = clsCommon.myFormat(obj.Apply_Amount)
                lblOSAmt.Text = clsCommon.myFormat(obj.Outstand_Amount)
                UsLock1.Status = obj.Status
                For Each objTr As clsUnclearedDocumentDetail In obj.Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colClearDocNo).Value = objTr.Cleared_Doc_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colClearDocDate).Value = objTr.Cleared_Doc_Date
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocAmt).Value = objTr.Doc_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApplyAmt).Value = objTr.Apply_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOutstandingAmt).Value = objTr.Outstand_Amount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDateToBe).Value = objTr.Date_To_Be
                Next
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
                If (clsUnclearedDocumentHead.PostData(txtCode.Value)) Then
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
                If (clsUnclearedDocumentHead.DeleteData(txtCode.Value)) Then
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
            Dim qst As String = "select count(*) from TSPL_Uncleared_Doc_Head where Doc_No='" + txtCode.Value + "'"
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
        Dim qry As String = " select Doc_No as Code,Doc_Date as Date,Case when Doc_type='R' then 'Receipt' else 'Payment' end as DocType,case when status=0 then 'Pending' else 'Posted' end as Status,Doc_Amount as DocumentAmount,Apply_Amount as ApplyAmount,Outstand_Amount as OutstandAmount from TSPL_Uncleared_Doc_Head"
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("unCldDocFnd", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
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
        End If
    End Sub

    

    

   

     

    Private Sub txtVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendor._MYValidating
        
        Dim qry As String = ""
        If clsCommon.CompairString("R", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
            qry = "select xxx.Code,TSPL_CUSTOMER_MASTER.Customer_Name as Name  from (select TSPL_RECEIPT_HEADER.Cust_Code as Code from TSPL_RECEIPT_HEADER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code where TSPL_BANK_MASTER.Bank_type='S' and TSPL_RECEIPT_HEADER.Posted='Y' group by TSPL_RECEIPT_HEADER.Cust_Code )xxx left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=xxx.Code"
        Else
            qry = "select Code, TSPL_VENDOR_MASTER.Vendor_Name as Name from (select TSPL_PAYMENT_HEADER.Vendor_Code as Code from TSPL_PAYMENT_HEADER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code where TSPL_BANK_MASTER.Bank_type='S' and TSPL_PAYMENT_HEADER.Posted='1' group by TSPL_PAYMENT_HEADER.Vendor_Code)xx  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=Code"
        End If
        txtVendor.Value = clsCommon.ShowSelectForm("BPUNCL", qry, "Code", "", txtVendor.Value, "Code", isButtonClicked)
        If clsCommon.CompairString("R", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
            qry = "select  Customer_Name as Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtVendor.Value + "'"
        Else
            qry = "select Vendor_Name as Name from TSPL_VENDOR_MASTER where Vendor_Code ='" + txtVendor.Value + "'"
        End If
        lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

    End Sub

     

    Private Sub DeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)

        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub saveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------

        End If
    End Sub

    Private Sub cboType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboType.SelectedValueChanged, cboType.SelectedIndexChanged
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "R") = CompairStringResult.Equal Then
            MyLabel3.Text = "Customer"
        Else
            MyLabel3.Text = "Vendor"
        End If
        txtVendor.Value = ""
        lblVendorName.Text = ""
        txtUnclearedDoc.Value = ""
        LoadBlankGrid()
    End Sub

    Private Sub txtUnclearedDoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUnclearedDoc._MYValidating
        If clsCommon.myLen(txtVendor.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please first select " + MyLabel3.Text, Me.Text)
            txtVendor.Focus()
            Exit Sub
        End If

        Dim qry As String = ""
        Dim Whr As String = ""
        If clsCommon.CompairString("R", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
            qry = "select Receipt_No as Code,Receipt_Date as Date,Receipt_Amount as Amount from TSPL_RECEIPT_HEADER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code "
            Whr = "TSPL_BANK_MASTER.Bank_type='S' and TSPL_RECEIPT_HEADER.Cust_Code='" + txtVendor.Value + "' and TSPL_RECEIPT_HEADER.Posted='Y' and not exists (select 1 from TSPL_UNCLEARED_DOC_HEAD where TSPL_RECEIPT_HEADER.Receipt_No=TSPL_UNCLEARED_DOC_HEAD.Cleared_Doc_No and DOC_No<>'" + txtCode.Value + "' and status=0) "
        Else
            qry = "select Payment_No as Code,Payment_Date as Date,Payment_Amount as Amount from TSPL_PAYMENT_HEADER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code "
            Whr = "TSPL_BANK_MASTER.Bank_type='S' and TSPL_PAYMENT_HEADER.Vendor_Code='" + txtVendor.Value + "' and TSPL_PAYMENT_HEADER.Posted='1' and not exists (select 1 from TSPL_UNCLEARED_DOC_HEAD where TSPL_PAYMENT_HEADER.Payment_No=TSPL_UNCLEARED_DOC_HEAD.Cleared_Doc_No and DOC_No<>'" + txtCode.Value + "' and status=0)"
        End If
        txtUnclearedDoc.Value = clsCommon.ShowSelectForm("UncleDocno", qry, "Code", Whr, txtUnclearedDoc.Value, "Code", isButtonClicked)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from (" + qry + ")xxx where code='" + txtUnclearedDoc.Value + "' ")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblDocAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("Amount")))
        Else
            lblDocAmt.Text = "0"
        End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(txtVendor.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please first select " + MyLabel3.Text, Me.Text)
                txtVendor.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtUnclearedDoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please first select uncleared document", Me.Text)
                txtUnclearedDoc.Focus()
                Exit Sub
            End If
            LoadBlankGrid()
            Dim qry As String = ""
            If clsCommon.CompairString("R", clsCommon.myCstr(cboType.SelectedValue)) = CompairStringResult.Equal Then
                qry = "select Receipt_No as Code,Receipt_Date as Date,Receipt_Amount as Amount,1 as RI,1 as Chk  from TSPL_RECEIPT_HEADER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_RECEIPT_HEADER.Bank_Code  where TSPL_RECEIPT_HEADER.Posted='Y' and TSPL_RECEIPT_HEADER.Cust_Code='" + txtVendor.Value + "' and  Receipt_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Receipt_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_BANK_MASTER.Bank_type<>'S'"
            Else
                qry = "select Payment_No as Code,Payment_Date as Date,Payment_Amount as Amount,1 as RI,1 as Chk  from TSPL_PAYMENT_HEADER left outer join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_HEADER.Bank_Code where TSPL_PAYMENT_HEADER.Posted='1' and TSPL_PAYMENT_HEADER.Vendor_Code='" + txtVendor.Value + "' and  Payment_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Payment_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_BANK_MASTER.Bank_type<>'S'"
            End If
            Dim finalQry As String = "select Code,max(xxx.Date) as Date,sum(Amount * case when ri=1 then 1 else -1 end ) as Amount   from (" + qry + "union all select TSPL_Uncleared_Doc_Detail.Cleared_Doc_No as Code,null as Date,case when TSPL_UNCLEARED_DOC_HEAD.Status=0 then TSPL_Uncleared_Doc_Detail.Doc_Amount else TSPL_Uncleared_Doc_Detail.Apply_Amount end as Amount,2 as RI,0 as Chk from TSPL_Uncleared_Doc_Detail left outer join TSPL_UNCLEARED_DOC_HEAD on TSPL_UNCLEARED_DOC_HEAD.DOC_No= TSPL_Uncleared_Doc_Detail.DOC_No where TSPL_UNCLEARED_DOC_HEAD.DOC_No<>'" + txtCode.Value + "' )xxx group by Code  having sum(Chk)>0  and sum(Amount * case when ri=1 then 1 else -1 end )<>0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(finalQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                isInsideLoadData = True
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colClearDocNo).Value = clsCommon.myCstr(dr("Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colClearDocDate).Value = clsCommon.myCDate(dr("Date"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDocAmt).Value = clsCommon.myCdbl(dr("Amount"))
                Next
                UpdateAllTotals()
                isInsideLoadData = False
                End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gv1.ValueChanging
        Try
            If Not isInsideLoadData AndAlso gv1.CurrentRow.Index >= 0 Then
                isInsideLoadData = True
                If gv1.CurrentColumn Is gv1.Columns(colSelect) Then
                    UpdateCurrentRow(e.NewValue)
                    gv1.CurrentRow.Cells(colSelect).Value = e.NewValue
                    UpdateAllTotals()
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
