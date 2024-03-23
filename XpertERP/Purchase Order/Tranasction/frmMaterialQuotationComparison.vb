'Sanjay Ticket No-BHA/26/06/18-000084 Dated  27/Jun/2018 of Client  Bharat Dairy
Imports common
Imports System.IO

Public Class frmMaterialQuotationComparison
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim AllowRoundOff_onInvoice As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim AllowAssetItem As Boolean = False
    Const colSelect As String = "SELECT"
    Const colLineNo As String = "LNO"
    Const colICode As String = "ICODE"
    Const colIName As String = "INAME"
    Const colQty As String = "QTY"
    Const colUnit As String = "COLTAX3"
    Const colRate As String = "RATE"
    Const colAmt As String = "AMT"
    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"
    Const colOrderNo As String = "ORDERNO"
    Const colCCode As String = "CCODE"
    Const colCName As String = "CNAME"

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
    Private Sub frmMaterialQuotationComparison_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        AllowRoundOff_onInvoice = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowRoundOff_OnCSASalePatti, clsFixedParameterCode.AllowRoundOff_OnCSASalePatti, Nothing)) = "1", True, False))
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

    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn
        repoSelect = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = ""
        repoSelect.Name = colSelect
        repoSelect.Width = 30
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSelect)


        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 30
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoOrder As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrder.FormatString = ""
        repoOrder.HeaderText = "Order No"
        repoOrder.Name = colOrderNo
        repoOrder.Width = 150
        repoOrder.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoOrder)

        Dim repoCCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCCode.FormatString = ""
        repoCCode.HeaderText = "Customer Code"
        repoCCode.Name = colCCode
        repoCCode.Width = 150
        repoCCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCCode)

        Dim repoCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCName.FormatString = ""
        repoCName.HeaderText = "Customer Name"
        repoCName.Name = colCName
        repoCName.Width = 150
        repoCName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCName)

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
        repoRate.ReadOnly = True
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

        'clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = False
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

    Function AllowToSave(ByVal ChekBtnPost As Boolean) As Boolean
        Try
            'UpdateAllTotals()
            ' = KUNAL > TICKET :  BM00000009580 ==============================
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If clsCommon.myLen(txtMaterialQuotation.Value) <= 0 Then
                txtMaterialQuotation.Focus()
                Throw New Exception("Plese Select Material Quotation")
            End If

            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                fndLocation.Focus()
                Throw New Exception("Please select Location")
            End If

            ' ''Check Stock----
            'If ChekBtnPost = True Then
            '    Dim dblBalQty As Double = 0
            '    Dim dblEnteredQty As Double = 0
            '    Dim strICode As String = ""
            '    Dim strUOM As String = ""
            '    For jj As Integer = 0 To gv1.Rows.Count - 1
            '        If (clsCommon.myCBool(gv1.Rows(jj).Cells(colSelect).Value) = True) Then
            '            strICode = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
            '            strUOM = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
            '            dblEnteredQty = clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value)
            '            dblBalQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, fndLocation.Value, "", txtDate.Value, Nothing, strUOM)
            '            dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
            '            If dblBalQty < dblEnteredQty Then
            '                common.clsCommon.MyMessageBoxShow("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty), Me.Text)
            '                Return False
            '            End If
            '        End If
            '    Next
            'End If
            ' ''Check Stock----

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
            If (AllowToSave(ChekBtnPost)) Then
                Dim obj As New ClsMaterialQuotationComparisonHead()
                obj.Code = txtCode.Value
                obj.QCDate = txtDate.Value
                obj.ScrapQuotation_Code = txtMaterialQuotation.Value
                obj.Location_Code = fndLocation.Value
                obj.Description = txtDesc.Text
                obj.Ref_No = txtRefNo.Text
                obj.Remarks = txtRmks.Text
                obj.Comments = txtComment.Text
                obj.On_Hold = chkOnHold.Checked
                obj.Is_Taxable = chkTaxable.Checked
                obj.ArrTr = New List(Of ClsMaterialQuotationComparisonDeatil)


                For Each grow As GridViewRowInfo In gv1.Rows
                    If (grow.Cells(colSelect).Value = True) Then
                        'obj.ArrTr.Add(objTr)
                        Dim objTr As New ClsMaterialQuotationComparisonDeatil()
                        objTr.Line_No = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                        objTr.QuotationOrder_Code = clsCommon.myCstr(grow.Cells(colOrderNo).Value)
                        objTr.Customer_Code = clsCommon.myCstr(grow.Cells(colCCode).Value)
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

            Dim obj As New ClsMaterialQuotationComparisonHead()
            obj = ClsMaterialQuotationComparisonHead.GetData(strDocumentNo, navType)
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
                txtDate.Value = obj.QCDate
                txtMaterialQuotation.Value = obj.ScrapQuotation_Code
                'txtCustomer.Value = obj.Customer_Code
                'txtcustdesc.Text = clsDBFuncationality.getSingleValue("select isnull(TSPL_customer_MASTER.Customer_Name,'') from TSPL_customer_MASTER where TSPL_customer_MASTER.cust_code='" + txtCustomer.Value + "'")
                fndLocation.Value = obj.Location_Code
                txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
                txtDesc.Text = obj.Description
                txtRefNo.Text = obj.Ref_No
                txtRmks.Text = obj.Remarks
                txtComment.Text = obj.Comments
                UsLock1.Status = obj.Status
                chkOnHold.Checked = obj.On_Hold
                chkTaxable.Checked = obj.Is_Taxable
                LoadQuotationOrder(obj.ScrapQuotation_Code)

                If obj.ArrTr IsNot Nothing Then
                    For Each objTr As ClsMaterialQuotationComparisonDeatil In obj.ArrTr
                      
                        For Each grow As GridViewRowInfo In gv1.Rows

                            If clsCommon.myCstr(grow.Cells(colOrderNo).Value) = objTr.QuotationOrder_Code AndAlso clsCommon.myCstr(grow.Cells(colCCode).Value) = objTr.Customer_Code AndAlso clsCommon.myCstr(grow.Cells(colICode).Value) = objTr.Item_Code AndAlso clsCommon.myCstr(grow.Cells(colUnit).Value) = objTr.Unit_Code Then
                                grow.Cells(colSelect).Value = True
                            End If
                        Next
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
            Dim isSaved As Boolean = True
            Dim TempCustCode As String = ""
            ' Dim TempTotalAmt As Decimal = 0
            Dim arrCustmer As New List(Of String)
            If (myMessages.postConfirm()) Then


                ' ''Check Stock----
                'If ChekBtnPost = True Then
                Dim dblBalQty As Double = 0
                Dim dblEnteredQty As Double = 0
                Dim strICode As String = ""
                Dim strUOM As String = ""
                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If (clsCommon.myCBool(gv1.Rows(jj).Cells(colSelect).Value) = True) Then
                        strICode = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                        strUOM = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                        dblEnteredQty = clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value)
                        dblBalQty = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(strICode, fndLocation.Value, "", txtDate.Value, Nothing, strUOM)
                        dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                        If dblBalQty < dblEnteredQty Then
                            'common.clsCommon.MyMessageBoxShow("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty), Me.Text)
                            'Return False
                            Throw New Exception("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                        End If
                    End If
                Next
                'End If
                ' ''Check Stock----

                SaveData(True)


                For Each grow As GridViewRowInfo In gv1.Rows
                    If (grow.Cells(colSelect).Value = True AndAlso (Not arrCustmer.Contains(grow.Cells(colCCode).Value))) Then
                        arrCustmer.Add(grow.Cells(colCCode).Value)
                    End If
                Next

                For i As Int16 = 0 To arrCustmer.Count - 1
                    Dim objSSH As New ClsScrapSaleHead()
                    objSSH = CreateScrapShipment(clsCommon.myCstr(arrCustmer(i)))
                    isSaved = isSaved AndAlso objSSH.SaveData(objSSH, "", True)
                Next


                isSaved = isSaved AndAlso ClsMaterialQuotationComparisonHead.PostData(txtCode.Value)
                If (isSaved) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Posted and Material Shipment Create Successfully ", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Function CreateScrapShipment(ByVal CustCode As String) As ClsScrapSaleHead
        Dim LineNo As Int16 = 1
        Dim TotalAmt As Decimal = 0
        Dim obj As New ClsScrapSaleHead()

        For Each grow As GridViewRowInfo In gv1.Rows
            If (grow.Cells(colSelect).Value = True AndAlso grow.Cells(colCCode).Value = CustCode) Then
                TotalAmt = TotalAmt + clsCommon.myCdbl(grow.Cells(colAmt).Value)
            End If
        Next


        obj.shipment_No = ""
        obj.strInvoiceNo = ""
        'If chkOnHold.Checked = True Then
        '    obj.Status = 1
        'Else
        obj.Status = 0
        'End If
        obj.Po_No = ""
        obj.NRG_No = ""
        obj.cust_Code = CustCode
        obj.cust_Name = clsDBFuncationality.getSingleValue("select isnull(TSPL_customer_MASTER.Customer_Name,'') from TSPL_customer_MASTER where TSPL_customer_MASTER.cust_code='" + CustCode + "'")
        obj.shipment_Date = txtDate.Value
        obj.posting_Date = txtDate.Value
        obj.expship_Date = txtDate.Value
        obj.Loc_Code = fndLocation.Value
        obj.Vehicle_Id = ""
        obj.Loc_Name = txtlocation.Text
        obj.Transporter_code = ""
        obj.Transporter_Name = ""
        obj.Vehicle_code = ""
        'obj.ToLoc_Code = ""
        obj.Is_Taxable = chkTaxable.Checked
        'If chkinvoice.Checked = True Then
        obj.CreateInvoice = 1
        'Else
        'obj.CreateInvoice = 0
        'End If
        'If chkExcisable.Checked = True Then
        '    obj.Excisable = "Y"
        'Else
        '    obj.Excisable = "N"
        'End If
        ''richa agarwal 19/03/2015
        obj.Excisable = "N"
        obj.Invoice_Type = IIf(chkTaxable.Checked = True, "T", "N")
        'If clsCommon.CompairString(obj.Invoice_Type, "E") = CompairStringResult.Equal Then
        '    obj.Excisable = "Y"
        'End If
        ''--------------
        obj.Inter_Branch = False
        obj.Description = txtDesc.Text
        obj.reff = txtMaterialQuotation.Value 'txtRefNo.Text
        obj.Tax_Group = ""
        obj.Tax_Desc = ""
        obj.Add_Amt = 0
        obj.Before_Add_Amt = clsCommon.myCdbl(TotalAmt) 'SKG TOTAL
        obj.Discount_Base = clsCommon.myCdbl(TotalAmt) 'SKG TOTAL
        obj.Discount_Amt = 0
        obj.Amount_Less_Discount = clsCommon.myCdbl(TotalAmt) 'SKG TOTAL
        obj.Total_Tax_Amt = 0
        obj.ship_Total_Amt = clsCommon.myCdbl(TotalAmt) 'SKG TOTAL
        'obj.doc_Amt = 0 'SKG TOTAL

        If AllowRoundOff_onInvoice Then
            Dim lstDecml As New List(Of Decimal)
            lstDecml = ClsScrapSaleHead.Calculate_RoundOffAmt(clsCommon.myCdbl(TotalAmt), Nothing)
            Dim AmtAfterRoundOff As Decimal = 0
            If lstDecml IsNot Nothing AndAlso lstDecml.Count > 0 Then
                AmtAfterRoundOff = clsCommon.myCdbl(lstDecml(0))
                obj.RoundOffAmount = clsCommon.myCdbl(lstDecml(1))
                obj.doc_Amt = clsCommon.myCdbl(AmtAfterRoundOff)
            End If
        Else
            obj.RoundOffAmount = 0
        End If

        'obj.RoundOffAmount = clsCommon.myCdbl(txtRoundOff.Text)

        obj.Is_CashSale = "N"
        'obj.On_Hold = chkOnHold.Checked
        'If chkScrapSale.Checked = True Then
        obj.Is_Scrap = "N"
        'Else
        'obj.Is_Scrap = "N"
        'End If
        obj.Doc_Type = "S"

        obj.TAX1 = ""
        obj.TAX1_Rate = 0
        obj.TAX1_Base_Amt = 0 'TotalAmt SKG
        obj.TAX1_Amt = 0
        obj.TAX2 = ""
        obj.TAX2_Rate = 0
        obj.TAX2_Base_Amt = 0
        obj.TAX2_Amt = 0
        obj.TAX3 = ""
        obj.TAX3_Rate = 0
        obj.TAX3_Base_Amt = 0
        obj.TAX3_Amt = 0
        obj.TAX4 = ""
        obj.TAX4_Rate = 0
        obj.TAX4_Base_Amt = 0
        obj.TAX4_Amt = 0
        obj.TAX5 = ""
        obj.TAX5_Rate = 0
        obj.TAX5_Base_Amt = 0
        obj.TAX5_Amt = 0
        obj.TAX6 = ""
        obj.TAX6_Rate = 0
        obj.TAX6_Base_Amt = 0
        obj.TAX6_Amt = 0
        obj.TAX7 = ""
        obj.TAX7_Rate = 0
        obj.TAX7_Base_Amt = 0
        obj.TAX7_Amt = 0
        obj.TAX8 = ""
        obj.TAX8_Rate = 0
        obj.TAX8_Base_Amt = 0
        obj.TAX8_Amt = 0
        obj.TAX9 = ""
        obj.TAX9_Rate = 0
        obj.TAX9_Base_Amt = 0
        obj.TAX9_Amt = 0
        obj.TAX10 = ""
        obj.TAX10_Rate = 0
        obj.TAX10_Base_Amt = 0
        obj.TAX10_Amt = 0
        obj.AddCode1 = ""
        obj.AddDesc1 = ""
        obj.AddAmt1 = 0
        obj.AddCode2 = ""
        obj.AddDesc2 = ""
        obj.AddAmt2 = 0
        obj.AddCode3 = ""
        obj.AddDesc3 = ""
        obj.AddAmt3 = 0
        obj.AddCode4 = ""
        obj.AddDesc4 = ""
        obj.AddAmt4 = 0
        obj.AddCode5 = ""
        obj.AddDesc5 = ""
        obj.AddAmt5 = 0
        obj.AddCode6 = ""
        obj.AddDesc6 = ""
        obj.AddAmt6 = 0
        obj.AddCode7 = ""
        obj.AddDesc7 = ""
        obj.AddAmt7 = 0
        obj.AddCode8 = ""
        obj.AddDesc8 = ""
        obj.AddAmt8 = 0
        obj.AddCode9 = ""
        obj.AddDesc9 = ""
        obj.AddAmt9 = 0
        obj.AddCode10 = ""
        obj.AddDesc10 = ""
        obj.AddAmt10 = 0

        obj.Terms_Code = "ADVANCE"
        obj.Due_Date = txtDate.Value
        'If rbtnTaxCalAutomatic.IsChecked Then
        obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
        'ElseIf rbtnTaxCalManual.IsChecked Then
        'obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
        'End If

        obj.Total_Gross_Weight = 0
        obj.Total_Net_Weight = 0
        obj.Arr = New List(Of ClsScrapSaleDetail)
        LineNo = 1
        For Each grow As GridViewRowInfo In gv1.Rows
            If (grow.Cells(colSelect).Value = True AndAlso grow.Cells(colCCode).Value = CustCode) Then
                Dim objTr As New ClsScrapSaleDetail()
                objTr.Line_No = LineNo 'SKG
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)
                objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                objTr.shipped_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objTr.price = clsCommon.myCdbl(grow.Cells(colRate).Value)
                objTr.DiscountPer = 0
                objTr.DiscountAmt = 0
                'objTr.Tax = clsCommon.myCdbl(grow.Cells(coltax).Value)
                objTr.ItemAmt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                objTr.TotalTaxAmt = 0
                objTr.NetPriceAmt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                objTr.TotalDiscountAmt = 0
                objTr.ItemNetAmt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                objTr.TotalAmt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                objTr.TAX1 = ""
                objTr.TAX1_Base_Amt = 0 'clsCommon.myCdbl(grow.Cells(colAmt).Value) 'SKG
                objTr.TAX1_Rate = 0
                objTr.TAX1_Amt = 0
                objTr.TAX2 = ""
                objTr.TAX2_Base_Amt = 0
                objTr.TAX2_Rate = 0
                objTr.TAX2_Amt = 0
                objTr.TAX3 = ""
                objTr.TAX3_Base_Amt = 0
                objTr.TAX3_Rate = 0
                objTr.TAX3_Amt = 0
                objTr.TAX4 = ""
                objTr.TAX4_Base_Amt = 0
                objTr.TAX4_Rate = 0
                objTr.TAX4_Amt = 0
                objTr.TAX5 = ""
                objTr.TAX5_Base_Amt = 0
                objTr.TAX5_Rate = 0
                objTr.TAX5_Amt = 0
                objTr.TAX6 = ""
                objTr.TAX6_Base_Amt = 0
                objTr.TAX6_Rate = 0
                objTr.TAX6_Amt = 0
                objTr.TAX7 = ""
                objTr.TAX7_Base_Amt = 0
                objTr.TAX7_Rate = 0
                objTr.TAX7_Amt = 0
                objTr.TAX8 = ""
                objTr.TAX8_Base_Amt = 0
                objTr.TAX8_Rate = 0
                objTr.TAX8_Amt = 0
                objTr.TAX9 = ""
                objTr.TAX9_Base_Amt = 0
                objTr.TAX9_Rate = 0
                objTr.TAX9_Amt = 0
                objTr.TAX10 = ""
                objTr.TAX10_Base_Amt = 0
                objTr.TAX10_Rate = 0
                objTr.TAX10_Amt = 0
                objTr.pending_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objTr.Specification = ""
                objTr.ItemwiseTaxCode = ""
                'objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))

                obj.Arr.Add(objTr)
                LineNo = LineNo + 1
            End If
        Next

        'If (obj.SaveData(obj, "", isNewEntry)) Then
        '    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
        '    LoadData(obj.shipment_No, NavigatorType.Current)
        'End If
        Return obj
    End Function

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
                If (ClsMaterialQuotationComparisonHead.DeleteData(txtCode.Value)) Then
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
            Dim qst As String = "select count(*) from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD where Code='" + txtCode.Value + "'"
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
        Dim qry As String = " select  Code,QCDate as Date,Description, case when TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.Status='0' then 'Pending' else 'Approved' end as [Status] from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD"
        Dim whrClas As String = " 1=1 "
        LoadData(clsCommon.ShowSelectForm("QCfndNo", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
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
                                         "TSPL_SCRAP_QUOTATION_COMPARISON_HEAD " + Environment.NewLine + _
                                         "TSPL_SCRAP_QUOTATION_COMPARISON_DETAIL ")
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
      
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        'UpdateAllTotals()
        'For ii As Integer = 1 To gv1.Rows.Count
        '    gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        'Next
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


    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
       
    End Sub


    Private Sub LoadQuotationOrder(ByVal MaterialQuotation As String)
        Try
            Dim LineNo As Int16 = 1
            Dim obj As New ClsGettingMaterialQuotationOrderData()
            obj = ClsGettingMaterialQuotationOrderData.GetOrderData(txtMaterialQuotation.Value)
            If (obj IsNot Nothing) Then
                If obj.ArrTr IsNot Nothing Then
                    fndLocation.Value = obj.ArrTr(0).Location_Code
                    chkTaxable.Checked = obj.ArrTr(0).Is_Taxable
                    txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'"))
                    For Each objTr As ClsGettingMaterialQuotationOrderData In obj.ArrTr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = LineNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = objTr.Order_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCCode).Value = objTr.Customer_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCName).Value = clsDBFuncationality.getSingleValue("select isnull(TSPL_customer_MASTER.Customer_Name,'') from TSPL_customer_MASTER where TSPL_customer_MASTER.cust_code='" + objTr.Customer_Code + "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Item_Net_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        LineNo = LineNo + 1
                    Next
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadLabel1_Click(sender As Object, e As EventArgs) Handles lblQuotationComparison.Click

    End Sub

    Private Sub txtMaterialQuotation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMaterialQuotation._MYValidating
        BlankAllControls()
        LoadBlankGrid()
        Dim qry As String = " select  Code,QDate as Date,Description, 'Approved' as [Status] from TSPL_SCRAP_QUOTATION_HEAD"
        Dim whrClas As String = " TSPL_SCRAP_QUOTATION_HEAD.Status=1 and TSPL_SCRAP_QUOTATION_HEAD.code not in (select TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.ScrapQuotation_Code from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD)"
        txtMaterialQuotation.Value = clsCommon.ShowSelectForm("QfndNo", qry, "Code", whrClas, txtMaterialQuotation.Value, "Code", isButtonClicked)
        LoadQuotationOrder(txtMaterialQuotation.Value)
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
