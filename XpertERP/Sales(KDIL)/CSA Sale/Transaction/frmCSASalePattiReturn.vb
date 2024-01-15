Imports common
Imports Telerik
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.IO

Public Class FrmCSASalePattiReturn
    Inherits FrmMainTranScreen

#Region "variables"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim ButtonToolTip As New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim arrLoc As String = ""
    Dim Errcntl As New clsErrorControl()

    Public StrDocumentCode As String = Nothing

    Const colLineno As String = "Lineno"
    Const colItemCode As String = "Icode"
    Const colItemName As String = "Iname"
    Const colItemCSAType As String = "ItemCSAType"
    Const colUOM As String = "UOM"
    Const colQty As String = "Qty"
    Const colRate As String = "Rate"
    Const colAmount As String = "Amount"
    Const colItemFOC As String = "itemFOC"
    Const colRemarks As String = "Remarks"
    Const colIsBatchItem As String = "colIsBatchItem"
    Dim showReturnType As Boolean = False
    Dim showBatchSkipAtCSAReturn As Boolean = False

#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSASalePattiReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnpost.Visible = MyBase.isPostFlag
    End Sub

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim check As Integer = 0
                check = clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where type='Plant' and location_code='" + obj.Default_LocCode + "'")
                If check > 0 Then
                    fndLocation.Value = obj.Default_LocCode
                    txt_loc_name.Text = obj.Default_LocName
                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub FunReset()
        chkCncelPSR.Enabled = True
        chkCncelPSR.Checked = False
        fndCode.Value = ""
        dtpdate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtDesc.Text = ""
        txtDesc.Text = clsCSASalePattiReturnHead.GetLatestDescription(Nothing)
        fndCustomer.Value = ""
        txtcustName.Text = ""
        fndLocation.Value = ""
        txt_loc_name.Text = ""
        fndCsaLocationCode.Value = ""
        txtCSAloca_name.Text = ""
        lblTotRAmt1.Text = ""
        UsLock1.Status = ERPTransactionStatus.Open

        cmbType.SelectedValue = ""

        gv.Rows.Clear()
        gv.Rows.AddNew()

        LOCATIONRIGTHS()

        fndCode.MyReadOnly = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnpost.Enabled = False
        isNewEntry = True

        RadPageView1.SelectedPage = RadPageViewPage1
        fndCode.Focus()
        fndCode.Select()

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
    End Sub

    Private Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repochk As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineno
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repochk = New GridViewCheckBoxColumn()
        repochk.FormatString = ""
        repochk.HeaderText = "FOC Item"
        repochk.Name = colItemFOC
        repochk.Width = 50
        repochk.ThreeState = False
        repochk.WrapText = True
        gv.MasterTemplate.Columns.Add(repochk)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Code"
        repoLineNo.Name = colItemCode
        repoLineNo.Width = 110
        repoLineNo.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoLineNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Name"
        repoLineNo.Name = colItemName
        repoLineNo.Width = 300
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "CSA Type"
        repoLineNo.Name = colItemCSAType
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Unit"
        repoLineNo.Name = colUOM
        repoLineNo.Width = 110
        repoLineNo.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoLineNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.DecimalPlaces = 2
        repoQty.WrapText = True
        gv.MasterTemplate.Columns.Add(repoQty)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Unit Rate"
        repoQty.Name = colRate
        repoQty.Width = 80
        repoQty.DecimalPlaces = 2
        repoQty.ReadOnly = False
        repoQty.WrapText = True
        gv.MasterTemplate.Columns.Add(repoQty)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Amount"
        repoQty.Name = colAmount
        repoQty.Width = 80
        repoQty.DecimalPlaces = 2
        repoQty.ReadOnly = True
        repoQty.WrapText = True
        gv.MasterTemplate.Columns.Add(repoQty)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Remarks"
        repoLineNo.Name = colRemarks
        repoLineNo.Width = 120
        repoLineNo.MaxLength = 200
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoIsBatchItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsBatchItem.HeaderText = "Is Batch Item"
        repoIsBatchItem.Name = colIsBatchItem
        repoIsBatchItem.ReadOnly = True
        repoIsBatchItem.IsVisible = False
        repoIsBatchItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsBatchItem)


        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
        gv.AllowDeleteRow = True

        repoLineNo = Nothing
        repoQty = Nothing
    End Sub

    Sub OpenBatchItem()
        If clsCommon.myCBool(gv.CurrentRow.Cells(colIsBatchItem).Value) Then
            Dim frm As frmBatchItemIn = New frmBatchItemIn()
            frm.strItemCode = clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value)
            frm.strItemName = clsCommon.myCstr(gv.CurrentRow.Cells(colItemName).Value)
            frm.strUOM = clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value)
            frm.dblMRP = 0 ' clsCommon.myCdbl(gv.CurrentRow.Cells(colMRP).Value)
            frm.dblqty = clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value)


            frm.arr = TryCast(gv.CurrentRow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv.CurrentRow.Cells(colItemCode).Tag = frm.arr
            End If
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
            If AllowFutureDateTransaction(dtpdate.Value, Nothing) = False Then
                dtpdate.Select()
                Return False
            End If

            RadPageView1.SelectedPage = RadPageViewPage1

            UpdateTotal()

            If clsCommon.myLen(fndCustomer.Value) <= 0 Then
                fndCustomer.Focus()
                fndCustomer.Select()
                Errcntl.SetError(txtcustName, "Select CSA Name first.")
                Throw New Exception("Please select CSA Name first.")
            Else
                Errcntl.ResetError(txtcustName)
            End If

            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                fndLocation.Focus()
                fndLocation.Select()
                Errcntl.SetError(txt_loc_name, "Select location first.")
                Throw New Exception("Please select location first.")
            Else
                Errcntl.ResetError(txt_loc_name)
            End If

            If showReturnType AndAlso clsCommon.CompairString(clsCommon.myCstr(cmbType.SelectedValue), "") = CompairStringResult.Equal Then
                cmbType.Focus()
                cmbType.Select()
                Errcntl.SetError(cmbType, "Select type for return.")
                Throw New Exception("Please select Type for return document.")
            Else
                Errcntl.ResetError(cmbType)
            End If

            Dim arrIcode As New ArrayList()
            Dim Icode As String = ""
            Dim UOM As String = ""
            Dim qty As Decimal = 0
            Dim rate As Decimal = 0
            Dim Iname As String = ""
            Dim NewIcode As String = ""
            Dim NewUOM As String = ""
            Dim Newrate As Decimal = 0
            lblTotRAmt1.Text = 0

            gv.Focus()
            gv.Select()
            For Each grow As GridViewRowInfo In gv.Rows
                Icode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                Iname = clsCommon.myCstr(grow.Cells(colItemName).Value)
                UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                rate = clsCommon.myCdbl(grow.Cells(colRate).Value)

                If clsCommon.myLen(Icode) > 0 Then
                    If Not arrIcode.Contains(Icode) Then
                        arrIcode.Add(Icode)
                    End If

                    If clsCommon.myLen(UOM) <= 0 Then
                        gv.CurrentRow = gv.Rows(grow.Index)
                        gv.CurrentColumn = gv.Columns(colUOM)
                        Throw New Exception("Select unit of item [" + Iname + "] at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If qty <= 0 Then
                        gv.CurrentRow = gv.Rows(grow.Index)
                        gv.CurrentColumn = gv.Columns(colQty)
                        Throw New Exception("Fill quantity of item [" + Iname + "] at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If rate <= 0 AndAlso clsCommon.myCBool(grow.Cells(colItemFOC).Value) = False Then
                        gv.CurrentRow = gv.Rows(grow.Index)
                        gv.CurrentColumn = gv.Columns(colRate)
                        Throw New Exception("Fill unit rate of item [" + Iname + "] at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    grow.Cells(colAmount).Value = clsCommon.myCdbl(qty * rate)
                    lblTotRAmt1.Text = clsCommon.myCdbl(lblTotRAmt1.Text) + clsCommon.myCdbl(qty * rate)


                    ''==============batch item condition
                    If Not showBatchSkipAtCSAReturn Then '=======[If Batch skip then Batch dosen't work]
                        If clsCommon.myCdbl(grow.Cells(colQty).Value) > 0 AndAlso clsCommon.myCBool(grow.Cells(colIsBatchItem).Value) Then
                            Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(grow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
                            If arrBatchNo Is Nothing Then
                                Throw New Exception("Please provide Batch no for item : " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + " . At Line No" + clsCommon.myCstr(grow.Index + 1))
                            Else
                                Dim tQty As Decimal = 0
                                For Each objBatch As clsBatchInventory In arrBatchNo
                                    tQty += objBatch.Qty
                                Next
                                If tQty <> clsCommon.myCdbl(grow.Cells(colQty).Value) > 0 Then
                                    Throw New Exception("Item : " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + " Entered Qty " + clsCommon.myCstr(clsCommon.myCdbl(grow.Cells(colQty).Value)) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(grow.Index + 1))
                                End If
                            End If
                        End If
                    End If
                    
                    ''===========================================

                    For ii As Integer = grow.Index + 1 To gv.Rows.Count - 1
                        NewIcode = clsCommon.myCstr(gv.Rows(ii).Cells(colItemCode).Value)
                        NewUOM = clsCommon.myCstr(gv.Rows(ii).Cells(colUOM).Value)
                        Newrate = clsCommon.myCdbl(gv.Rows(ii).Cells(colRate).Value)

                        If clsCommon.CompairString(Icode, NewIcode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(UOM, NewUOM) = CompairStringResult.Equal AndAlso clsCommon.CompairString(rate, Newrate) = CompairStringResult.Equal Then
                            gv.CurrentRow = gv.Rows(ii)
                            gv.CurrentColumn = gv.Columns(colItemCode)
                            Throw New Exception("Duplicate entry found at row no. " + clsCommon.myCstr(grow.Index + 1) + " and " + clsCommon.myCstr(ii + 1) + ".")
                        End If
                    Next

                End If ''end icode cond 
            Next

            If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
                Throw New Exception("Fill atleast one row in grid.")
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub SaveData(ByVal FromPost As Boolean)
        Dim obj As New clsCSASalePattiReturnHead()
        Dim objtr As New clsCSASalePattiReturnDetail()
        Try
            obj.Is_Cancelled = IIf(chkCncelPSR.Checked, 1, 0)
            obj.Document_Code = clsCommon.myCstr(fndCode.Value)
            obj.Documnet_Date = clsCommon.myCDate(dtpdate.Text)
            obj.Description = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")
            obj.Cust_Code = clsCommon.myCstr(fndCustomer.Value)
            obj.isNewEntry = isNewEntry
            obj.CSA_Location_Code = clsCommon.myCstr(fndCsaLocationCode.Value)
            obj.Location_Code = clsCommon.myCstr(fndLocation.Value)
            obj.Document_Amount = clsCommon.myCdbl(lblTotRAmt1.Text)
            obj.Tax_Group = clsCommon.myCstr(clsLocationWiseTax.GetDefaultTaxGroup(obj.Location_Code, obj.Cust_Code, "S"))
            obj.Arr = New List(Of clsCSASalePattiReturnDetail)

            obj.Return_Type = clsCommon.myCstr(cmbType.SelectedValue)

            For Each grow As GridViewRowInfo In gv.Rows
                objtr = New clsCSASalePattiReturnDetail()
                objtr.arrBatchItem = New List(Of clsBatchInventory)

                If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 Then
                    objtr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                    objtr.Is_FOC = clsCommon.myCBool(grow.Cells(colItemFOC).Value)
                    objtr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objtr.Line_No = clsCommon.myCdbl(grow.Cells(colLineno).Value)
                    objtr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objtr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    objtr.Unit_Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objtr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)

                    objtr.arrBatchItem = TryCast(grow.Cells(colItemCode).Tag, List(Of clsBatchInventory))

                    obj.Arr.Add(objtr)
                End If
            Next

            If obj.Arr Is Nothing OrElse obj.Arr.Count <= 0 Then
                Throw New Exception("Fill atleast one row in grid.")
            End If

            If clsCSASalePattiReturnHead.SaveData(obj) Then
                If Not FromPost Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                End If

                UcAttachment1.funDelete(obj.Document_Code, False)
                UcAttachment1.SaveData(obj.Document_Code)

                LoadData(obj.Document_Code, arrLoc, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            objtr = Nothing
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal strArrLoc As String, ByVal NavType As NavigatorType)
        Dim obj As New clsCSASalePattiReturnHead()
        Try
            FunReset()

            isInsideLoadData = True
            gv.Rows.Clear()
            gv.Rows.AddNew()

            obj = clsCSASalePattiReturnHead.GetData(strCode, strArrLoc, NavType, Nothing)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                isNewEntry = False
                chkCncelPSR.Enabled = False
                chkCncelPSR.Checked = IIf(obj.Is_Cancelled = 1, True, False)
                fndCode.Value = obj.Document_Code
                dtpdate.Text = obj.Documnet_Date
                txtDesc.Text = obj.Description
                fndCustomer.Value = obj.Cust_Code
                txtcustName.Text = obj.Cust_Name
                fndCsaLocationCode.Value = obj.CSA_Location_Code
                txtCSAloca_name.Text = obj.CSA_location_desc
                fndLocation.Value = obj.Location_Code
                txt_loc_name.Text = obj.Location_Code
                lblTotRAmt1.Text = obj.Document_Amount

                cmbType.SelectedValue = obj.Return_Type

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsCSASalePattiReturnDetail In obj.Arr
                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = objtr.Line_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = objtr.Item_Name
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCSAType).Value = objtr.CSA_Type
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemFOC).Value = objtr.Is_FOC
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOM).Value = objtr.UOM
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr.Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colRate).Value = objtr.Unit_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colAmount).Value = objtr.Amount
                        gv.Rows(gv.Rows.Count - 1).Cells(colRemarks).Value = objtr.Remarks

                        gv.Rows(gv.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Item_Code)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Tag = objtr.arrBatchItem

                        If objtr.Is_FOC Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colRate).ReadOnly = True
                        Else
                            gv.Rows(gv.Rows.Count - 1).Cells(colRate).ReadOnly = False
                        End If

                        gv.Rows.AddNew()
                    Next
                End If

                ReStoreGridLayout()

                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnpost.Enabled = True
                fndCode.MyReadOnly = True
                UsLock1.Status = ERPTransactionStatus.Pending

                If obj.Status = 1 Then
                    btnsave.Text = "Update"
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnpost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If

                UcAttachment1.LoadData(obj.Document_Code)
            Else
                FunReset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)  ''save layout
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs)  ''delete layout
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
        End If
    End Sub

    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        LoadData(clsCommon.myCstr(fndCode.Value), arrLoc, NavType)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub fndCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustomer._MYValidating
        fndCustomer.Value = clsCommon.myCstr(clsCustomerMaster.getFinder(" isnull(tspl_customer_master.csa_type,'N')='Y' ", fndCustomer.Value, isButtonClicked))
        txtcustName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + fndCustomer.Value + "'"))
        fndCsaLocationCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from tspl_location_master where cust_code='" + fndCustomer.Value + "'"))
        txtCSAloca_name.Text = clsLocation.GetName(fndCsaLocationCode.Value, Nothing)
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Dim WhrCls As String = " coalesce(ltrim(Rejected_Type),'') in ('N','') and coalesce(ltrim(GIT_Type),'') in ('N','') and Is_Section='N' and Is_Sub_Location='N'  "
        If clsCommon.myLen(arrLoc) > 0 Then
            WhrCls += "  and  Location_Code in (" + arrLoc + ")"
        End If

        fndLocation.Value = clsLocation.getFinder(whrCls, fndLocation.Value, isButtonClicked)
        txt_loc_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + fndLocation.Value + "'"))
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If AllowToSave() Then
                SaveData(False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                fndCode.Focus()
                fndCode.Select()
                Errcntl.SetError(fndCode, "Select Document First.")
                Throw New Exception("Please select document for deletion.")
            Else
                Errcntl.ResetError(fndCode)
            End If

            If myMessages.deleteConfirm() Then
                If clsCSASalePattiReturnHead.DeleteData(clsCommon.myCstr(fndCode.Value)) Then
                    myMessages.delete()

                    UcAttachment1.funDelete(fndCode.Value, False)

                    FunReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnpost_Click(sender As Object, e As EventArgs) Handles btnpost.Click
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                fndCode.Focus()
                fndCode.Select()
                Errcntl.SetError(fndCode, "Select Document First.")
                Throw New Exception("Please select document for deletion.")
            Else
                Errcntl.ResetError(fndCode)
            End If

            If AllowToSave() Then
                SaveData(True)

                If myMessages.postConfirm() Then
                    If clsCSASalePattiReturnHead.PostData(clsCommon.myCstr(fndCode.Value)) Then
                        myMessages.post()

                        LoadData(fndCode.Value, arrLoc, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Try
            Dim qry As String = "select count(document_code) from tspl_sd_sale_return_head where comp_code='" + objCommonVar.CurrentCompanyCode + "' and document_code='" + clsCommon.myCstr(fndCode.Value) + "'"
            fndCode.MyReadOnly = False
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                fndCode.MyReadOnly = True
            End If

            If fndCode.MyReadOnly OrElse isButtonClicked Then
                fndCode.Value = clsCSASalePattiReturnHead.GetFinder(fndCode.Value, " tspl_sd_sale_return_head.comp_code='" + objCommonVar.CurrentCompanyCode + "' and tspl_sd_sale_return_head.bill_to_location in (" + arrLoc + ") and tspl_sd_sale_return_head.trans_type='CPR' ", isButtonClicked)

                If clsCommon.myLen(fndCode.Value) > 0 Then
                    LoadData(fndCode.Value, arrLoc, NavigatorType.Current)
                Else
                    FunReset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv.Columns(colItemCode) Then
                        isCellValueChanged = True
                        gv.CurrentRow.Cells(colItemCode).Tag = Nothing
                        OpenIcodeList(False)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colUOM) Then
                        isCellValueChanged = True
                        gv.CurrentRow.Cells(colItemCode).Tag = Nothing
                        OpenItemUnit(False)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colQty) OrElse e.Column Is gv.Columns(colRate) Then
                        isCellValueChanged = True
                        If Not showBatchSkipAtCSAReturn Then '=======[If Batch skip then Batch dosen't work]
                            If e.Column Is gv.Columns(colQty) Then
                                OpenBatchItem()
                            End If
                        End If
                        UpdateTotal()
                        isCellValueChanged = False
                    End If

                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub UpdateTotal()
        Try
            lblTotRAmt1.Text = 0
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(colAmount).Value = clsCommon.myCdbl(grow.Cells(colQty).Value) * clsCommon.myCdbl(grow.Cells(colRate).Value)

                lblTotRAmt1.Text = clsCommon.myCdbl(lblTotRAmt1.Text) + clsCommon.myCdbl(grow.Cells(colAmount).Value)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub OpenIcodeList(ByVal isButtonClicked As Boolean)
        Dim whrcls As String = " tspl_item_master.Active=1 and isnull(Is_FreshItem,0)<>1 "
        'If clsCommon.myCBool(gv.CurrentRow.Cells(colItemFOC).Value) = True Then
        '    whrcls += " and (tspl_item_master.item_code in (select item_code from TSPL_CSA_TRANSFER_DETAIL where FOC='Y' and doc_code in (select doc_code from TSPL_CSA_TRANSFER_HEAD where isnull(status,0)=1 and cust_code='" + fndCustomer.Value + "' ))) "
        'Else
        '    whrcls += " and (tspl_item_master.item_code in (select item_code from TSPL_CSA_TRANSFER_DETAIL where FOC<>'Y' and doc_code in (select doc_code from TSPL_CSA_TRANSFER_HEAD where isnull(status,0)=1 and cust_code='" + fndCustomer.Value + "' ) union all select item_code from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_DETAIL.adjustment_no=TSPL_ADJUSTMENT_HEADER.adjustment_no where TSPL_ADJUSTMENT_HEADER.posted='Y' and (TSPL_ADJUSTMENT_HEADER.mainlocationcode='" + csa_loc + "' or TSPL_ADJUSTMENT_HEADER.loc_code='" + csa_loc + "') )) "
        'End If



        gv.CurrentRow.Cells(colItemCode).Value = clsCommon.myCstr(clsItemMaster.getFinder(whrcls, clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value), isButtonClicked))
        Dim icode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value)
        gv.CurrentRow.Cells(colItemName).Value = clsItemMaster.GetItemName(icode, Nothing)
        gv.CurrentRow.Cells(colItemCSAType).Value = clsItemMaster.GetItemCSAType(icode, Nothing)
        gv.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(icode, Nothing)
        gv.CurrentRow.Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(icode)
    End Sub

    Private Sub OpenItemUnit(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.uom_code as Code,TSPL_ITEM_UOM_DETAIL.uom_description as Description,TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL "
        gv.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("CSASALERETUOMFND", qry, "Code", " item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value) + "'", clsCommon.myCstr(gv.CurrentRow.Cells(colUOM).Value), "Code", isButtonClicked))
    End Sub

    Private Sub FrmCSASalePattiReturn_KeyDown1(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N Then
                btnNew.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                btndelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnpost.Enabled Then
                btnpost.PerformClick()
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadTypeCombobox()
        cmbType.DataSource = Nothing
        Dim qry As String = "select '' as Code,'Select' as Name union all select 'I' as Code,'Return Goods' as Name union all select 'D' as Code,'Damage Good' as Name union all select 'S' as Code,'Shortage Goods' as Name"
        cmbType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cmbType.ValueMember = "Code"
        cmbType.DisplayMember = "Name"
    End Sub

    Private Sub FrmCSASalePattiReturn_Load1(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        LoadTypeCombobox()
        FunReset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+P for post record.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+N for reset window.")

        showReturnType = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCSAReturnTypeOnScreen, clsFixedParameterCode.ShowCSAReturnTypeOnScreen, Nothing)) = "1", True, False))
        Panel1.Visible = showReturnType
        showBatchSkipAtCSAReturn = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.BatchSkipCSAReturn, clsFixedParameterCode.BatchSkipCSAReturn, Nothing)) = "1", True, False))

        If clsCommon.myLen(StrDocumentCode) > 0 Then
            LoadData(StrDocumentCode, arrLoc, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndCode.Value = clsCommon.myCstr(Me.Tag)
            LoadData(fndCode.Value, arrLoc, NavigatorType.Current)
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        Try
            If gv.Rows.Count > 0 Then
                Dim intIndex As Integer = gv.CurrentRow.Index
                gv.CurrentRow.Cells(colLineno).Value = clsCommon.myCstr(intIndex + 1)
                If gv.Rows.Count - 1 = intIndex Then
                    gv.Rows.AddNew()
                    gv.CurrentRow = gv.Rows(intIndex)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_KeyDown(sender As Object, e As KeyEventArgs) Handles gv.KeyDown
        If e.KeyCode = Keys.F5 Then
            OpenBatchItem()
        End If
    End Sub

    Private Sub gv_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gv.ValueChanging
        Try
            If Not isInsideLoadData Then
                If gv.CurrentColumn Is gv.Columns(colItemFOC) Then
                    MakeReadonlyRate(e.NewValue)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MakeReadonlyRate(ByVal NewVal As Boolean)
        isInsideLoadData = True
        If NewVal Then
            gv.CurrentRow.Cells(colRate).ReadOnly = True
            gv.CurrentRow.Cells(colRate).Value = 0
        Else
            gv.CurrentRow.Cells(colRate).ReadOnly = False
        End If
        isInsideLoadData = False
    End Sub
End Class
