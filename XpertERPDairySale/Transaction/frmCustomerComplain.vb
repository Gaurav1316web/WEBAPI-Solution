Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class frmCustomerComplain
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strQuery As String
    Dim strQueryCANCRate As String
    Dim dt As DataTable
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim blnLoad As Boolean = False

    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"
    Const colItemShortDesc As String = "colItemShortDesc"
    Const colUnit As String = "colUnit"
    Const colCustomerComplain As String = "colCustomerComplain"
    Const colQty As String = "colQty"
    Const colLineNo As String = "colLineNo"
    Const colHSNCode As String = "colHSNCode"
    Const colDamageQty As String = "colDamageQty"
    Const colDamageUOM As String = "colDamageUOM"
#End Region
    ''Checked in 20200617 by richa.
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverseAndUnpost.Enabled = True
        Else
            btnReverseAndUnpost.Enabled = False
        End If
    End Sub

    Private Sub LoadBlankGrid()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim ItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemCode)

        Dim ItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Item Desc"
        ItemDesc.Name = colItemDesc
        ItemDesc.Width = 100
        ItemDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemDesc)

        Dim ItemShortDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemShortDesc.FormatString = ""
        ItemShortDesc.HeaderText = "Item Short Desc"
        ItemShortDesc.Name = colItemShortDesc
        ItemShortDesc.Width = 100
        ItemShortDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemShortDesc)

        Dim HSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        HSN.FormatString = ""
        HSN.HeaderText = "HSN Code"
        HSN.Name = colHSNCode
        HSN.Width = 100
        HSN.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(HSN)

        Dim ItemCustComplain As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        ItemCustComplain.FormatString = ""
        ItemCustComplain.HeaderText = "Complain Code"
        ItemCustComplain.Name = colCustomerComplain
        ItemCustComplain.Width = 100
        ItemCustComplain.ReadOnly = False
        ItemCustComplain.DataSource = funComplainCode()
        ItemCustComplain.ValueMember = "Code"
        ItemCustComplain.DisplayMember = "Code"
        Gv1.MasterTemplate.Columns.Add(ItemCustComplain)


        Dim Unit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Unit.FormatString = ""
        Unit.HeaderText = "Unit"
        Unit.Name = colUnit
        Unit.Width = 100
        Unit.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(Unit)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ReadOnly = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoDamageQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDamageQty = New GridViewDecimalColumn()
        repoDamageQty.FormatString = ""
        repoDamageQty.HeaderText = "Damage Quantity"
        repoDamageQty.Name = colDamageQty
        repoDamageQty.Width = 80
        repoDamageQty.Minimum = 0
        repoDamageQty.ReadOnly = False
        repoDamageQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoDamageQty)

        Dim DamageUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DamageUnit.FormatString = ""
        DamageUnit.HeaderText = "Damage Unit"
        DamageUnit.Name = colDamageUOM
        DamageUnit.Width = 100
        DamageUnit.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(DamageUnit)

        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
    End Sub

    Private Sub funFillGrid(ByVal strInvoiceNo As String)
        Try
            LoadBlankGrid()
            'strQuery = " Select  TSPL_SD_SALE_INVOICE_DETAIL.Line_No,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_Code , TSPL_SD_SALE_INVOICE_DETAIL.Qty from TSPL_SD_SALE_INVOICE_DETAIL  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
            '           " where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code = '" + strInvoiceNo + "' and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N'  order By  TSPL_SD_SALE_INVOICE_DETAIL.Line_No asc "
            strQuery = " Select TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_Code , sum(TSPL_SD_SALE_INVOICE_DETAIL.Qty) as Qty from TSPL_SD_SALE_INVOICE_DETAIL  Left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code= TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                       " where TSPL_SD_SALE_INVOICE_DETAIL.Document_Code = '" + strInvoiceNo + "' and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item = 'N' group by TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Short_Description,TSPL_ITEM_MASTER.HSN_Code,TSPL_SD_SALE_INVOICE_DETAIL.Unit_Code "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQuery)
            Dim intLineNo As Integer = 0
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    intLineNo += 1
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(intLineNo) 'clsCommon.myCstr(dr("Line_No"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item_Desc"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemShortDesc).Value = clsCommon.myCstr(dr("Short_Description"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsCommon.myCstr(dr("HSN_Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCstr(dr("Qty"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDamageQty).Value = clsCommon.myCstr(dr("Qty"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDamageUOM).Value = clsCommon.myCstr(dr("Unit_Code"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDamageQty).Value = 0
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Customer Complain", MessageBoxButtons.OK)
        End Try
    End Sub

    Function AllowToSave() As Boolean

        'If clsCommon.myLen(txtRemarks.Text) <= 0 Then
        '    txtRemarks.Focus()
        '    Throw New Exception("Remarks can't be blank.")
        'End If
        'If clsCommon.myLen(fndComplainCode.Value) <= 0 Then
        '    fndComplainCode.Focus()
        '    ' Throw New Exception("Plese select Complain Code")
        'End If
        If clsCommon.CompairString("Select", cboType.SelectedValue) = CompairStringResult.Equal Then
            fndInvoice.Focus()
            Throw New Exception("Plese select Type")
        End If
        If clsCommon.CompairString(cboType.SelectedValue, "Quailty") = CompairStringResult.Equal Then
            If clsCommon.myLen(fndInvoice.Value) <= 0 Then
                Throw New Exception("Plese select Invoice")
            End If
        End If
        Dim totalDamageQty As Double = 0
        For ii As Integer = 0 To Gv1.Rows.Count - 1
            Dim qty As Double = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colQty).Value)
            Dim Uom As String = clsCommon.myCstr(Gv1.Rows(ii).Cells(colUnit).Value)
            Dim DamageQty As Double = clsCommon.myCdbl(Gv1.Rows(ii).Cells(colDamageQty).Value)
            Dim DamageUom As String = clsCommon.myCstr(Gv1.Rows(ii).Cells(colDamageUOM).Value)
            Dim ItemCode As String = clsCommon.myCstr(Gv1.Rows(ii).Cells(colItemCode).Value)
            If DamageQty > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(Gv1.Rows(ii).Cells(colCustomerComplain).Value)) <= 0 Then
                Throw New Exception("Please Select Complain Code For Item Code '" + ItemCode + "'")
            End If

            If DamageQty > 0 AndAlso clsCommon.myLen(DamageUom) <= 0 Then
                Throw New Exception("Please Select Damage UOM For Item Code '" + ItemCode + "'")
            End If
            If DamageQty > 0 Then
                Dim dblValidMaxQty As Double = GetItemConvQty(ItemCode, Uom, DamageUom, qty)
                If DamageQty > dblValidMaxQty Then
                    Throw New Exception("Invalid Damage Qty for " + ItemCode + " (with Damage Uom " + DamageUom + " maiximum qty " + clsCommon.myCstr(dblValidMaxQty) + " Allowed).")
                End If
            End If
            totalDamageQty = totalDamageQty + DamageQty
        Next
        If clsCommon.CompairString(cboType.SelectedValue, "Quailty") = CompairStringResult.Equal Then
            If totalDamageQty <= 0 Then
                Throw New Exception("Please enter Damage Qty atleast for one item")
            End If
        End If
        Return True
    End Function

    

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsCustomerComplainHead()
                obj.Complaint_No = txtDocNo.Value
                obj.Complaint_Date = clsCommon.myCDate(txtDocDate.Value)
                obj.Invoice_No = fndInvoice.Value
                If clsCommon.myLen(obj.Invoice_No) > 0 Then
                    obj.Invoice_Date = clsCommon.myCDate(lblInvocieDate.Text)
                
                End If
                obj.Type = cboType.SelectedValue
                obj.Complaint_Code = fndComplainCode.Value
                obj.Remarks = txtRemarks.Text
                obj.Cust_Code = fndCustom.Value
                obj.Arr = New List(Of clsCustomerComplainDetail)
                For Each grow As GridViewRowInfo In Gv1.Rows
                    Dim objTr As New clsCustomerComplainDetail()
                    objTr.SNo = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.HSN_Code = clsCommon.myCstr(grow.Cells(colHSNCode).Value)
                    objTr.Damage_Uom = clsCommon.myCstr(grow.Cells(colDamageUOM).Value)
                    objTr.Damage_Qty = clsCommon.myCdbl(grow.Cells(colDamageQty).Value)
                    objTr.Complaint_Code = clsCommon.myCstr(grow.Cells(colCustomerComplain).Value)
                    obj.Arr.Add(objTr)
                    'If clsCommon.myLen(objTr.Item_Code) > 0 AndAlso clsCommon.myCdbl(objTr.Damage_Qty) > 0 Then
                    '    obj.Arr.Add(objTr)
                    'End If
                Next
                'If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                '    common.clsCommon.MyMessageBoxShow("Please Fill at list one Document")
                '    Return
                'End If
                If (obj.SaveData(obj, isNewEntry, Nothing)) Then
                    common.clsCommon.MyMessageBoxShow(Gv1, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Complaint_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        txtDocNo.Value = ""
        txtDocDate.Value = clsCommon.GETSERVERDATE
        fndCustom.Value = ""
        lblCustomer.Text = ""
        fndInvoice.Value = ""
        lblInvocieDate.Text = ""
        fndComplainCode.Value = ""
        lblComplainCode.Text = ""
        txtRemarks.Text = ""
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnSave.Text = "Save"
        LoadBlankGrid()
        isNewEntry = True
        isInsideLoadData = False
        btnDelete.Enabled = True
        cboType.ReadOnly = False
        LoadType()
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click


        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Document No not found to Post")
                Exit Sub
            End If

            If clsCommon.myLen(fndInvoice.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Invoice No not found, please post it from Bulk Posting ")
                Exit Sub
            End If


            Dim isPost As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count (*) from TSPL_CUSTOMER_COMPLAINT_HEAD where Complaint_No = '" + txtDocNo.Value + "' and IsPosted = 'Y'"))
            If isPost = True Then
                common.clsCommon.MyMessageBoxShow("Record Already posted.")
                Exit Sub
            End If
            If myMessages.postConfirm() Then
                ' If (clsCustomerComplainHead.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                '' auto creation of Dairy dispatch as replacement by richa 
                Dim frm As New frmShipmentDairy
                    frm.SetUserMgmt(clsUserMgtCode.frmSaleDispatchDairy)
                    frm.Show()
                    frm.chkReplacement.Checked = True
                    frm.RadLabel24.Text = "Invoice No"
                    frm.txtReqNo.Visible = False
                    frm.TxtInvoiceNoForReplacement.Visible = True
                    frm.txtReqNo.Value = ""
                    frm.txtCustomerComplaintNo.Text = ""
                    frm.txtCustomerComplaintNo.Text = txtDocNo.Value
                    frm.txtVendorNo.Value = fndCustom.Value
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_Taxable  from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'")), "0") = CompairStringResult.Equal Then
                        frm.cmbDisItemType.SelectedValue = "NT"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_Taxable  from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'")), "1") = CompairStringResult.Equal Then
                        frm.cmbDisItemType.SelectedValue = "T"
                    End If
                    frm.txtDate.Value = txtDocDate.Value
                    frm.txtBillToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bill_To_Location  from TSPL_SD_SALE_INVOICE_HEAD  where document_code='" & fndInvoice.Value & "'"))
                    frm.txtSubLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select sub_location_code  from TSPL_SD_SALE_INVOICE_HEAD  where document_code='" & fndInvoice.Value & "'"))
                    frm.SelectInvoiceNo()
                frm.showSavedMessage = False

            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                frm.SaveData(False, trans)
                Dim strShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_SD_SHIPMENT_HEAD where isnull(Customer_Complaint_No,'')='" & txtDocNo.Value & "'", trans))
                If clsCommon.myLen(strShipmentNo) > 0 Then
                    If clsPSShipmentHead.PostData(clsUserMgtCode.frmSaleDispatchDairy, strShipmentNo, trans, Nothing, True) Then

                    End If
                    clsCustomerComplainHead.PostData(MyBase.Form_ID, txtDocNo.Value, trans)

                    trans.Commit()
                    frm.Close()
                    common.clsCommon.MyMessageBoxShow(Gv1, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                End If
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(ex, Me.Text)
            End Try
        End If


    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        'Try
        '    Dim strItem = Gv1.Rows(e.RowIndex).Cells(1).Value
        '    strQuery = LoadDoubleClickQuery(strItem)
        '    Dim frmStock As New FrmStockDetail
        '    frmStock.LoadDispatchData(strQuery)
        '    frmStock.Show()
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsCustomerComplainHead.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    btnAddNew.PerformClick()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
    '            If clsDairyGatePassEntry.ReverseAndUnpost(txtCode.Value) Then
    '                common.clsCommon.MyMessageBoxShow("Successfully Reversed", Me.Text)
    '                LoadData(txtCode.Value, NavigatorType.Current)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try
    'End Sub
    '--============================================================================================================================================================
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = " Select Complaint_No as Code, Convert (varchar,Complaint_Date,103) as Date, Invoice_No as [Invoice No], convert (varchar,Invoice_Date,103) as [Invoice Date],Cust_Code as [Customer Code], Type,case when isnull(TSPL_CUSTOMER_COMPLAINT_HEAD.IsPosted,'')='Y' then 'Approved' else 'Pending' end as Status from TSPL_CUSTOMER_COMPLAINT_HEAD "
        LoadData(clsCommon.ShowSelectForm("CustomerComplain@Fnd", qry, "Code", "", txtDocNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_CUSTOMER_COMPLAINT_HEAD where Complaint_No='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomerComplain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        LoadBlankGrid()
        txtDocDate.Value = clsCommon.GETSERVERDATE()
        LoadType()
        txtRemarks.MaxLength = 500
        btnPost.Visible = True
        btnPost.Enabled = False
        'funFillGrid()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        Gv1.AllowDeleteRow = False
        Gv1.AllowAddNewRow = False
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            LoadBlankGrid()
            If clsCommon.myLen(strCode) <= 0 Then
                Exit Sub
            End If
            Dim obj As New clsCustomerComplainHead()
            obj = clsCustomerComplainHead.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Complaint_No)) > 0) Then
                isNewEntry = False
                btnSave.Text = "Update"
                If obj.IsPosted = "Y" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnDelete.Enabled = True
                End If
                txtDocNo.Value = obj.Complaint_No
                txtDocDate.Value = obj.Complaint_Date
                fndCustom.Value = obj.Cust_Code
                lblCustomer.Text = obj.Cust_Name
                fndInvoice.Value = obj.Invoice_No
                If clsCommon.myLen(obj.Invoice_No) > 0 Then
                    lblInvocieDate.Text = obj.Invoice_Date
                End If

                cboType.SelectedValue = obj.Type
                fndComplainCode.Value = obj.Complaint_Code
                lblComplainCode.Text = ""
                txtRemarks.Text = obj.Remarks

                isInsideLoadData = True
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsCustomerComplainDetail In obj.Arr
                        Gv1.Rows.AddNew()
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.SNo
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = objTr.Item_Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemShortDesc).Value = clsItemMaster.GetItemShortDescription(objTr.Item_Code, Nothing)
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSNCode).Value = objTr.HSN_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDamageUOM).Value = objTr.Damage_Uom
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDamageQty).Value = objTr.Damage_Qty
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCustomerComplain).Value = objTr.Complaint_Code

                    Next
                End If

                If clsCommon.myLen(obj.Invoice_No) <= 0 Then
                    lblInvocieDate.Text = ""
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally

        End Try
    End Sub

    Private Sub frmCustomerComplain_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnSave.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseAndUnpost.Visible = True
            End If
        End If
    End Sub
    Sub LoadType()

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Quailty"
        dr("Name") = "Quailty"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Service"
        dr("Name") = "Service "
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
        cboType.SelectedIndex = 0
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub

    Private Sub fndInvoice__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndInvoice._MYValidating
        strQuery = " Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo, Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [InvoiceDate] , TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] , TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as [Location Code], TSPL_Location_Master.Location_Desc as [Location Name] from TSPL_SD_SALE_INVOICE_HEAD " &
                   " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " &
                   " Left Outer Join TSPL_Location_Master on TSPL_Location_Master.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location "
        Dim whr As String = " TSPL_SD_SALE_INVOICE_HEAD.Status =1 and Trans_Type in ('PS','FS')  and TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in ( select Invoice_No from TSPL_CUSTOMER_COMPLAINT_HEAD union select Sale_Invoice_No from TSPL_SD_SHIPMENT_HEAD where IsReplacement=1) "
        If clsCommon.myLen(fndCustom.Value) > 0 Then
            whr += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code = '" + fndCustom.Value + "'  "
        End If
        whr += " and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) = Convert(date,'" + txtDocDate.Value.AddDays(-1) + "',103)"
        fndInvoice.Value = clsCommon.ShowSelectForm("Invoice@Customer@Complain", strQuery, "InvoiceNo", whr, fndInvoice.Value, "InvoiceNo", isButtonClicked)
        lblInvocieDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'"))
        fndCustom.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Customer_Code from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'"))
        lblCustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code = '" + fndCustom.Value + "' "))
        If clsCommon.myLen(fndInvoice.Value) > 0 Then
            funFillGrid(fndInvoice.Value)
        End If
    End Sub

    Private Sub fndCustom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustom._MYValidating
        strQuery = " select Cust_Code as Code , Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        fndCustom.Value = clsCommon.ShowSelectForm("CustomerMaster@Complain", strQuery, "Code", "", fndCustom.Value, "Code", isButtonClicked)
        lblCustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code = '" + fndCustom.Value + "' "))
        LoadPreviousinvoice(isButtonClicked)
    End Sub

    Sub LoadPreviousinvoice(ByVal isButtonClick As Boolean)
        strQuery = " Select count (distinct TSPL_SD_SALE_INVOICE_HEAD.Document_Code) as InvoiceNo from TSPL_SD_SALE_INVOICE_HEAD " &
                   " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " &
                   " Left Outer Join TSPL_Location_Master on TSPL_Location_Master.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location "
        Dim whr As String = " TSPL_SD_SALE_INVOICE_HEAD.Status =1 and Trans_Type in ('PS','FS')  and TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in ( select Invoice_No from TSPL_CUSTOMER_COMPLAINT_HEAD union select Sale_Invoice_No from TSPL_SD_SHIPMENT_HEAD where IsReplacement=1) "
        If clsCommon.myLen(fndCustom.Value) > 0 Then
            whr += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code = '" + fndCustom.Value + "'  "
        End If
        whr += " and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) = Convert(date,'" + txtDocDate.Value.AddDays(-1) + "',103)"
        Dim dblcount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("" & strQuery & " where " & whr & " "))
        Dim strfndQuery As String = " Select TSPL_SD_SALE_INVOICE_HEAD.Document_Code as InvoiceNo, Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [InvoiceDate] , TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] , TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as [Location Code], TSPL_Location_Master.Location_Desc as [Location Name] from TSPL_SD_SALE_INVOICE_HEAD " &
                   " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " &
                   " Left Outer Join TSPL_Location_Master on TSPL_Location_Master.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location "
        Dim whrcls As String = " TSPL_SD_SALE_INVOICE_HEAD.Status =1 and Trans_Type in ('PS','FS')  and TSPL_SD_SALE_INVOICE_HEAD.Document_Code not in ( select Invoice_No from TSPL_CUSTOMER_COMPLAINT_HEAD union select Sale_Invoice_No from TSPL_SD_SHIPMENT_HEAD where IsReplacement=1) "
        If clsCommon.myLen(fndCustom.Value) > 0 Then
            whrcls += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code = '" + fndCustom.Value + "'  "
        End If
        whrcls += " and Convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) = Convert(date,'" + txtDocDate.Value.AddDays(-1) + "',103)"

        If dblcount > 1 Then
            fndInvoice.Value = clsCommon.ShowSelectForm("Invoice@Customer@Complain", strfndQuery, "InvoiceNo", whrcls, fndInvoice.Value, "InvoiceNo", isButtonClick)
        ElseIf dblcount = 1 Then
            fndInvoice.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select InvoiceNo from (" & strfndQuery & " where " & whrcls & ")z"))
        Else
            fndInvoice.Value = ""
        End If

        lblInvocieDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'"))
        fndCustom.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select   Customer_Code from TSPL_SD_SALE_INVOICE_HEAD where Document_Code ='" & fndInvoice.Value & "'"))
        lblCustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code = '" + fndCustom.Value + "' "))
        If clsCommon.myLen(fndInvoice.Value) > 0 Then
            funFillGrid(fndInvoice.Value)
        End If
    End Sub
    Private Function funComplainCode() As DataTable
        Dim dt As DataTable = Nothing
        strQuery = "select Code from TSPL_CUSTOMER_COMPLAINT_MASTER "
        Dim whr As String = ""
        If clsCommon.CompairString(cboType.SelectedValue, "") <> CompairStringResult.Equal Then
            whr = " Type = '" + cboType.SelectedValue + "' "
        End If
        dt = clsDBFuncationality.GetDataTable(strQuery)
        Return dt
    End Function
    Private Sub fndComplainCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndComplainCode._MYValidating
        strQuery = "select Code ,Description,Type  from TSPL_CUSTOMER_COMPLAINT_MASTER "
        Dim whr As String = ""
        If clsCommon.CompairString(cboType.SelectedValue, "") <> CompairStringResult.Equal Then
            whr = " Type = '" + cboType.SelectedValue + "' "
        End If
        fndComplainCode.Value = clsCommon.ShowSelectForm("ComplainMaster@Complain", strQuery, "Code", whr, fndComplainCode.Value, "Code", isButtonClicked)
        lblComplainCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Description from TSPL_CUSTOMER_COMPLAINT_MASTER where Code = '" + fndComplainCode.Value + "' "))
        If clsCommon.myLen(fndComplainCode.Value) > 0 Then
            cboType.SelectedValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Type from TSPL_CUSTOMER_COMPLAINT_MASTER where Code = '" + fndComplainCode.Value + "' "))
            cboType.ReadOnly = True
        End If
    End Sub

    Private Sub Gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellValueChanged
        If e.Column Is Gv1.Columns(colDamageUOM) AndAlso isInsideLoadData = False Then
            If clsCommon.myCdbl(Gv1.CurrentRow.Cells(colDamageQty).Value) > 0 Then
                OpenUOMList(False)
            Else
                Gv1.CurrentRow.Cells(colDamageUOM).Value = ""
            End If
        End If
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            Gv1.CurrentRow.Cells(colDamageUOM).Value = clsCommon.ShowSelectForm("DamageUomfndnder", qry, "Code", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colDamageUOM).Value), "Code", isButtonClick)
        End If
    End Sub

    Public Shared Function GetItemConvQty(ByVal strItem As String, ByVal strCurrentUnit As String, ByVal strConvertedUnit As String, ByVal dblQty As Double) As Double
        Dim dblCurrentConvF As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strConvertedUnit & "'"))
        Dim dblConvQty As Double = 0
        If clsCommon.myLen(strConvertedUnit) > 0 Then
            Dim dblOrgConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from tspl_item_uom_detail where Item_Code='" & strItem & "' and UOM_Code='" & strCurrentUnit & "'"))
            If dblCurrentConvF > 0 Then
                dblConvQty = Math.Round(Math.Round((dblOrgConvF / dblCurrentConvF), 2) * dblQty, 6)
            End If
        End If
        Return dblConvQty
    End Function

    Private Sub btnReverseAndUnpost_Click(sender As Object, e As EventArgs) Handles btnReverseAndUnpost.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsCustomerComplainHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
