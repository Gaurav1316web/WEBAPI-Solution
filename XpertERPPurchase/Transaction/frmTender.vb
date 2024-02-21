Imports System.Data.SqlClient
Imports common

Public Class frmTender
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim itemtype As String
#Region "Variables"
    Dim blnPageLoad As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isPageLoadData As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colLCode As String = "COLLCODE"
    Const colLName As String = "COLLNAME"
    Const colVCode As String = "COLVCODE"
    Const colVName As String = "COLVNAME"
    Const colQty As String = "COLQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colAmt As String = "COLAMT"
    Const colRemarks As String = "colRemarks"
    Const colComments As String = "colComments"
    Dim closeyn As String
    Dim vaddnew As String
    Dim strRemarks As String
    Const colDiscount As String = "colDiscount"

    Const colScheduleSNo As String = "colScheduleSNo"
    Const colScheduleParentSNo As String = "colScheduleParentSNo"
    Const colScheduleNo As String = "colScheduleNo"
    Const colScheduleFromDate As String = "colScheduleFromDate"
    Const colScheduleToDate As String = "colScheduleToDate"
    Const colScheduleVCode As String = "colScheduleVCode"
    Const colScheduleVName As String = "colScheduleVName"
    Const colScheduleLCode As String = "colScheduleLCode"
    Const colScheduleLName As String = "colScheduleLName"
    Const colScheduleICode As String = "colScheduleICode"
    Const colScheduleIName As String = "colScheduleIName"
    Const colScheduleQtyPer As String = "colScheduleQtyPer"
    Const colScheduleQty As String = "colScheduleQty"
    Const colScheduleShortPer As String = "colScheduleShortPer"
    Const colScheduleShort As String = "colScheduleShort"
    Const colScheduleLateDays As String = "colScheduleLateDays"
    Const colScheduleExtensionDays As String = "colScheduleExtensionDays"
    Dim ButtonToolTip As ToolTip = New ToolTip()

#End Region
    Public Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnreverse.Enabled = True
        Else
            btnreverse.Enabled = False
        End If
    End Sub
    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isPageLoadData = True
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")

        RadPageView1.SelectedPage = RadPageViewPage1
        LoadItemType()
        LoadTenderType()
        LoadMode()
        LoadBlankGrid1()
        LoadBlankGrid2()
        AddNew()
        isPageLoadData = False

    End Sub
    Sub LoadMode()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "Offline"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Online"
        dt.Rows.Add(dr)

        cboMode.DataSource = dt
        cboMode.ValueMember = "Code"
        cboMode.DisplayMember = "Name"
    End Sub
    Sub LoadTenderType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        dr = dt.NewRow()
        dr("Code") = "0"
        dr("Name") = "RM Tender"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "Risk Purchase"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "2"
        dr("Name") = "Techical Spare Part"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "3"
        dr("Name") = "Local Purchase"
        dt.Rows.Add(dr)

        cboTenderType.DataSource = dt
        cboTenderType.ValueMember = "Code"
        cboTenderType.DisplayMember = "Name"
    End Sub

    Sub LoadItemType()
        'cboItemType.DataSource = clsItemMaster.GetItemType()
        Dim Whr = " AND IS_NON_INVENTORY=0   AND ITEM_TYPE_CODE NOT IN('J') "
        cboItemType.DataSource = clsItemMaster.getItemTypeQuery(Whr)
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
    End Sub

    Sub BlankAllControls()
        txtDocNo.Value = ""
        vaddnew = "Y"
        chkRalclose.Enabled = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtScheduleStartDate.Value = txtDate.Value
        UsLock1.Status = ERPTransactionStatus.Pending
        lblTotalDocAmt.Text = ""
        txtFieldValue1.Text = ""
        txtFieldValue2.Text = ""
        txtFieldValue3.Text = ""
        txtFieldValue4.Text = ""
        txtFieldValue5.Text = ""
        txtFieldValue6.Text = ""
        txtOtherInfo1.Text = ""
        txtOtherInfo2.Text = ""
        txtOtherInfo3.Text = ""
        txtOtherInfo4.Text = ""
        txtOtherInfo5.Text = ""
        txtOtherInfo6.Text = ""
        txtOtherInfo7.Text = ""
        txtOtherInfo8.Text = ""
        txtOtherInfo9.Text = ""
        txtOtherInfo10.Text = ""
        chkRalclose.Checked = False
        cboTenderType.SelectedValue = "0"
        cboMode.SelectedValue = "1"
    End Sub

    Sub LoadBlankGrid1()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim TAX_PAID As New GridViewComboBoxColumn
        Dim BOOK_RATE_UOM As New GridViewTextBoxColumn

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        'repoICode.HeaderImage = My.Resources.search4
        'repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        repoICode.IsVisible = True
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        repoIName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLCode.FormatString = ""
        repoLCode.HeaderText = "Location Code"
        repoLCode.Name = colLCode
        'repoICode.HeaderImage = My.Resources.search4
        'repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLCode.ReadOnly = True
        repoLCode.Width = 100
        repoLCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoLCode)

        Dim repoLName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLName.FormatString = ""
        repoLName.HeaderText = "Location Name"
        repoLName.Name = colLName
        repoLName.Width = 150
        repoLName.ReadOnly = True
        repoLName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoLName)

        Dim repoVCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVCode.FormatString = ""
        repoVCode.HeaderText = "Vendor Code"
        repoVCode.Name = colVCode
        repoVCode.Width = 100
        repoVCode.IsVisible = True
        repoVCode.HeaderImage = My.Resources.search4
        repoVCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoVCode)

        Dim repoVName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVName.FormatString = ""
        repoVName.HeaderText = "Vendor Name"
        repoVName.Name = colVName
        repoVName.Width = 150
        repoVName.ReadOnly = True
        repoVName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoVName)


        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 80
        repoUnit.HeaderImage = My.Resources.search4
        repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoUnit)



        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = False
        repoRate.IsVisible = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscount = New GridViewDecimalColumn()
        repoDiscount.FormatString = ""
        repoDiscount.HeaderText = "Discount %Age"
        repoDiscount.Name = colDiscount
        repoDiscount.Width = 80
        repoDiscount.Minimum = 0
        repoDiscount.ReadOnly = False
        repoDiscount.IsVisible = True
        repoDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDiscount)


        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.IsVisible = True
        repoAmt.VisibleInColumnChooser = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)



        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 200
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        Dim repoComments As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoComments.FormatString = ""
        repoComments.HeaderText = "Comments"
        repoComments.Name = colComments
        repoComments.Width = 200
        gv1.MasterTemplate.Columns.Add(repoComments)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

    End Sub

    Sub LoadBlankGrid2()
        gv2.Rows.Clear()
        gv2.Columns.Clear()


        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        'repoICode.HeaderImage = My.Resources.search4
        'repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.ReadOnly = True
        repoICode.Width = 100
        repoICode.IsVisible = True
        gv2.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        repoIName.IsVisible = True
        gv2.MasterTemplate.Columns.Add(repoIName)

        Dim repoLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLCode.FormatString = ""
        repoLCode.HeaderText = "Location Code"
        repoLCode.Name = colLCode
        repoLCode.ReadOnly = True
        repoLCode.Width = 100
        repoLCode.IsVisible = True
        gv2.MasterTemplate.Columns.Add(repoLCode)

        Dim repoLName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLName.FormatString = ""
        repoLName.HeaderText = "Location Name"
        repoLName.Name = colLName
        repoLName.Width = 150
        repoLName.ReadOnly = True
        repoLName.IsVisible = True
        gv2.MasterTemplate.Columns.Add(repoLName)

        Dim repoVCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVCode.FormatString = ""
        repoVCode.HeaderText = "Vendor Code"
        repoVCode.Name = colVCode
        repoVCode.HeaderImage = My.Resources.search4
        repoVCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoVCode.ReadOnly = False
        repoVCode.Width = 100
        repoVCode.IsVisible = True
        gv2.MasterTemplate.Columns.Add(repoVCode)

        Dim repoVName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVName.FormatString = ""
        repoVName.HeaderText = "Vendor Name"
        repoVName.Name = colVName
        repoVName.Width = 150
        repoVName.ReadOnly = True
        repoVName.IsVisible = True
        gv2.MasterTemplate.Columns.Add(repoVName)


        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.HeaderImage = My.Resources.search4
        repoUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        repoUnit.Width = 80
        repoUnit.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoUnit)



        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.ReadOnly = False
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoQty)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = False
        repoRate.IsVisible = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoRate)

        Dim repoDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscount = New GridViewDecimalColumn()
        repoDiscount.FormatString = ""
        repoDiscount.HeaderText = "Discount % Age"
        repoDiscount.Name = colDiscount
        repoDiscount.Width = 80
        repoDiscount.Minimum = 0
        repoDiscount.ReadOnly = False
        repoDiscount.IsVisible = True
        repoDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoDiscount)


        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = True
        repoAmt.IsVisible = True
        repoAmt.VisibleInColumnChooser = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoAmt)


        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 200
        repoRemarks.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoRemarks)

        Dim repoComments As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoComments.FormatString = ""
        repoComments.HeaderText = "Comments"
        repoComments.Name = colComments
        repoComments.Width = 200
        repoComments.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(repoComments)

        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        gv2.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try

            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colVCode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) Then
                        If e.Column Is gv1.Columns(colVCode) Then
                            'If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                            '    common.clsCommon.MyMessageBoxShow("Please select Customer First")
                            '    isCellValueChangedOpen = False
                            '    Exit Sub
                            'End If
                            'If clsCommon.myLen(txtLocation.Value) <= 0 Then
                            '    common.clsCommon.MyMessageBoxShow("Please select Location First")
                            '    isCellValueChangedOpen = False
                            '    Exit Sub
                            'End If
                            'OpenItemList(False)
                            'Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                            'Dim strIUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
                            'ItemPrice(strICode, strIUOM, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index)
                            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER "
                            Dim whrCls As String = " TSPL_VENDOR_MASTER.Status='N'"
                            gv1.CurrentRow.Cells(colVCode).Value = clsCommon.ShowSelectForm("TenVendorFndr", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colVCode).Value), "Code", False)
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colVCode).Value) > 0 Then
                                gv1.CurrentRow.Cells(colVName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where vendor_code='" + gv1.CurrentRow.Cells(colVCode).Value + "'"))
                            End If

                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)

                            If clsCommon.myLen(strICode) > 0 Then
                                'OpenUOMList(False)
                                If clsCommon.myLen(strICode) > 0 Then
                                    Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
                                    Dim whrCls As String = "Item_Code='" + strICode + "'"
                                    gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("ITEM-TenFndr1", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", False)

                                End If

                            Else
                                Throw New Exception("Please fill item first.")
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If

                        ElseIf e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) Then

                            gv1.CurrentRow.Cells(colAmt).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                        End If
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub RefreshSerialNo()
        Dim intSerialNo As Integer
        For intCount As Integer = 0 To gv2.Rows.Count - 1
            intSerialNo += 1
            gv2.Rows(intCount).Cells(colLineNo).Value = intSerialNo
        Next


    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        gv1.Rows(IntRowNo).Cells(colAmt).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value), 2)
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtScheduleStartDate.Value = txtDate.Value
        BlankAllControls()
        LoadBlankGrid1()
        LoadBlankGrid2()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
        txtDate.Enabled = True
        txtDocNo.MyReadOnly = False
        lblTenderSeqNo.Text = ""
        cboItemType.SelectedValue = ""
    End Sub

    Function AllowToSaveRAL_Type(ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qst As String = "SELECT TSPL_LOCATION_MASTER.IsMainPlant FROM TSPL_USER_MASTER
                     INNER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.LOCATION_CODE=TSPL_USER_MASTER.Default_Location
                     WHERE USER_CODE='" + objCommonVar.CurrentUserCode + "'"
            Dim IsMainPlant As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst, trans))
            If IsMainPlant = 0 Then
                If clsCommon.myCDecimal(cboTenderType.SelectedValue) = 0 Then
                    cboTenderType.Focus()
                    Throw New Exception("You are not authorized to Save/Post " + cboTenderType.SelectedText + " type RAL")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
        Try

            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Enter Tender No", Me.Text)
                txtDocNo.Focus()
                Return False
            End If

            If AllowToSaveRAL_Type(trans) = False Then
                Return False
            End If

            Dim dblTotalAmount As Double = 0
            For ii As Integer = 0 To gv2.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv2.Rows(ii).Cells(colICode).Value)
                Dim strVCode As String = clsCommon.myCstr(gv2.Rows(ii).Cells(colVCode).Value)
                Dim strLCode As String = clsCommon.myCstr(gv2.Rows(ii).Cells(colLCode).Value)

                Dim strIName As String = clsCommon.myCstr(gv2.Rows(ii).Cells(colIName).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv2.Rows(ii).Cells(colQty).Value)
                Dim dblrate As Double = clsCommon.myCdbl(gv2.Rows(ii).Cells(colRate).Value)
                Dim strUOM As String = clsCommon.myCstr(gv2.Rows(ii).Cells(colUnit).Value)
                Dim dblAmount As Double = clsCommon.myCdbl(gv2.Rows(ii).Cells(colAmt).Value)
                Dim dblDiscount As Double = clsCommon.myCdbl(gv2.Rows(ii).Cells(colDiscount).Value)


                dblTotalAmount = dblTotalAmount + dblAmount

                If clsCommon.myLen(strICode) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter Item At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If clsCommon.myLen(strLCode) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter Location for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If clsCommon.myLen(strVCode) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter Vendor for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If clsCommon.myLen(strUOM) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter Quantity UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If dblQty <= 0 AndAlso clsCommon.myCDecimal(cboTenderType.SelectedValue) <> 2 AndAlso clsCommon.myCDecimal(cboTenderType.SelectedValue) <> 3 Then
                    'clsCommon.myCDecimal(dblQty) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter Booked Quantity for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(strLCode) > 0 AndAlso clsCommon.myLen(strVCode) > 0 AndAlso clsCommon.myCdbl(dblrate) <= 0 AndAlso clsCommon.myCdbl(dblDiscount) <= 0 AndAlso clsCommon.myCDecimal(cboTenderType.SelectedValue) = 2 Then
                    clsCommon.MyMessageBoxShow(Me, "Please enter Rate or Discount for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If clsCommon.myCdbl(dblrate) <= 0 AndAlso clsCommon.myCDecimal(cboTenderType.SelectedValue) <> 2 Then
                    clsCommon.MyMessageBoxShow(Me, "Please enter Booked Rate for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If (clsCommon.myLen(strICode) > 0) Then

                    For jj As Integer = 0 To gv2.Rows.Count - 1
                        If jj = ii Then
                            Continue For
                        End If
                        Dim strInnerICode As String = clsCommon.myCstr(gv2.Rows(jj).Cells(colICode).Value)
                        Dim strInnerLCode As String = clsCommon.myCstr(gv2.Rows(jj).Cells(colLCode).Value)
                        Dim strInnerVCode As String = clsCommon.myCstr(gv2.Rows(jj).Cells(colVCode).Value)
                        Dim strInnerUOM As String = clsCommon.myCstr(gv2.Rows(jj).Cells(colUnit).Value)

                        If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strLCode, strInnerLCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strVCode, strInnerVCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strInnerUOM) = CompairStringResult.Equal Then
                            Dim Msg As String = "Same Detail Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If

                    Next
                End If
            Next
            RefreshSerialNo()
            lblTotalDocAmt.Text = Math.Round(clsCommon.myCdbl(dblTotalAmount), 2)

            'UpdateAllTotals()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (SaveData()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                btnAddNew.Focus()
            End If

        End If
    End Sub

    Private Function SaveData() As Boolean
        Try
            Dim qry As String = ""
            If (AllowToSave(Nothing)) Then

                Dim obj As New clsTenderHead()
                obj.DocumentCode = txtDocNo.Value
                obj.DocumentDate = txtDate.Value
                obj.DocumentAmount = Math.Round(clsCommon.myCdbl(lblTotalDocAmt.Text), 2)
                obj.FieldValue1 = txtFieldValue1.Text
                obj.FieldValue2 = txtFieldValue2.Text
                obj.FieldValue3 = txtFieldValue3.Text
                obj.FieldValue4 = txtFieldValue4.Text
                obj.FieldValue5 = txtFieldValue5.Text
                obj.FieldValue6 = txtFieldValue6.Text
                If chkRalclose.Checked = True Then
                    obj.close_yn = "Y"
                ElseIf chkRalclose.Checked = False Then
                    obj.close_yn = "N"
                End If

                obj.OtherInfo1 = txtOtherInfo1.Text
                obj.OtherInfo2 = txtOtherInfo2.Text
                obj.OtherInfo3 = txtOtherInfo3.Text
                obj.OtherInfo4 = txtOtherInfo4.Text
                obj.OtherInfo5 = txtOtherInfo5.Text
                obj.OtherInfo6 = txtOtherInfo6.Text
                obj.OtherInfo7 = txtOtherInfo7.Text
                obj.OtherInfo8 = txtOtherInfo8.Text
                obj.OtherInfo9 = txtOtherInfo9.Text
                obj.OtherInfo10 = txtOtherInfo10.Text
                obj.Tender_Type = clsCommon.myCDecimal(cboTenderType.SelectedValue)
                obj.Mode = clsCommon.myCDecimal(cboMode.SelectedValue)
                obj.Arr = New List(Of clsTenderDetail)

                Dim intLine As Integer = 0
                For Each grow As GridViewRowInfo In gv2.Rows
                    Dim objTr As New clsTenderDetail()
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Discount = clsCommon.myCdbl(grow.Cells(colDiscount).Value)
                    'If clsCommon.myCdbl(grow.Cells(colQty).Value) > 0 Then
                    If ((objTr.Qty) > 0 OrElse clsCommon.myCDecimal(cboTenderType.SelectedValue) = 2 OrElse clsCommon.myCDecimal(cboTenderType.SelectedValue) = 3) AndAlso clsCommon.myCdbl(objTr.Rate) OrElse clsCommon.myCdbl(grow.Cells(colDiscount).Value) > 0 Then
                        intLine += 1
                        objTr.Line_No = grow.Cells(colLineNo).Value
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        objTr.Vendor_Code = clsCommon.myCstr(grow.Cells(colVCode).Value)
                        objTr.Location = clsCommon.myCstr(grow.Cells(colLCode).Value)

                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                        objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        objTr.Discount = clsCommon.myCdbl(grow.Cells(colDiscount).Value)
                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objTr.Comments = clsCommon.myCstr(grow.Cells(colComments).Value)
                    End If

                    If (clsCommon.myLen(objTr.Location) > 0) AndAlso (clsCommon.myLen(objTr.Vendor_Code) > 0) AndAlso (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                        'End If
                    End If
                Next


                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return False
                End If
                obj.ArrSchedule = New List(Of clsTenderSchedule)
                For Each grow As GridViewRowInfo In gvSchedule.Rows
                    Dim objTr As New clsTenderSchedule()
                    objTr.SNo = clsCommon.myCDecimal(grow.Cells(colScheduleSNo).Value)
                    objTr.PSNo = clsCommon.myCDecimal(grow.Cells(colScheduleParentSNo).Value)
                    objTr.Schedule_No = clsCommon.myCDecimal(grow.Cells(colScheduleNo).Value)
                    objTr.From_Date = clsCommon.myCDate(grow.Cells(colScheduleFromDate).Value)
                    objTr.To_Date = clsCommon.myCDate(grow.Cells(colScheduleToDate).Value)
                    objTr.Vendor_Code = clsCommon.myCstr(grow.Cells(colScheduleVCode).Value)
                    objTr.Location_Code = clsCommon.myCstr(grow.Cells(colScheduleLCode).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colScheduleICode).Value)
                    objTr.Schedule_Qty_Per = clsCommon.myCDecimal(grow.Cells(colScheduleQtyPer).Value)
                    objTr.Schedule_Qty = clsCommon.myCDecimal(grow.Cells(colScheduleQty).Value)
                    objTr.Schedule_Short_Per = clsCommon.myCDecimal(grow.Cells(colScheduleShortPer).Value)
                    objTr.Schedule_Short = clsCommon.myCDecimal(grow.Cells(colScheduleShort).Value)
                    objTr.Late_Days = clsCommon.myCDecimal(grow.Cells(colScheduleLateDays).Value)
                    objTr.Extension_Days = clsCommon.myCDecimal(grow.Cells(colScheduleExtensionDays).Value)
                    objTr.Arr = TryCast(grow.Cells(colScheduleLateDays).Tag, List(Of clsTenderSchedulePenelty))
                    obj.ArrSchedule.Add(objTr)
                Next

                If (obj.SaveData(obj, isNewEntry)) = True Then
                    LoadData(obj.DocumentCode, NavigatorType.Current, False)
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return False
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType, ByVal isLoadCopy As Boolean)
        Try
            Dim dblTotalDocAmt As Decimal = 0
            Dim qry As String = ""
            Dim obj As New clsTenderHead()
            'Dim intRow As Integer
            obj = clsTenderHead.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.DocumentCode) > 0) Then

                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Text = "Update"
                BlankAllControls()
                LoadBlankGrid1()
                LoadBlankGrid2()
                LoadBlankGridSchedule()
                txtDocNo.MyReadOnly = True
                txtDocNo.Value = obj.DocumentCode
                txtDate.Value = obj.DocumentDate
                If obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                End If
                UsLock1.Status = obj.Status
                lblTenderSeqNo.Text = clsCommon.myCstr(obj.TendorSeqNo)
                lblTotalDocAmt.Text = clsCommon.myCstr(obj.DocumentAmount)

                txtFieldValue1.Text = obj.FieldValue1
                txtFieldValue2.Text = obj.FieldValue2
                txtFieldValue3.Text = obj.FieldValue3
                txtFieldValue4.Text = obj.FieldValue4
                txtFieldValue5.Text = obj.FieldValue5
                txtFieldValue6.Text = obj.FieldValue6
                If obj.close_yn = "Y" Then
                    vaddnew = "Y"
                    chkRalclose.Checked = True
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    vaddnew = "N"
                ElseIf obj.close_yn = "N" Then
                    chkRalclose.Checked = False
                    vaddnew = "N"
                End If


                txtOtherInfo1.Text = obj.OtherInfo1
                txtOtherInfo2.Text = obj.OtherInfo2
                txtOtherInfo3.Text = obj.OtherInfo3
                txtOtherInfo4.Text = obj.OtherInfo4
                txtOtherInfo5.Text = obj.OtherInfo5
                txtOtherInfo6.Text = obj.OtherInfo6
                txtOtherInfo7.Text = obj.OtherInfo7
                txtOtherInfo8.Text = obj.OtherInfo8
                txtOtherInfo9.Text = obj.OtherInfo9
                txtOtherInfo10.Text = obj.OtherInfo10
                cboTenderType.SelectedValue = clsCommon.myCstr(obj.Tender_Type)
                cboMode.SelectedValue = clsCommon.myCstr(obj.Mode)
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    cboItemType.SelectedValue = clsItemMaster.GetItemType(obj.Arr(0).Item_Code, Nothing)
                    For Each objTr As clsTenderDetail In obj.Arr
                        gv2.Rows.AddNew()

                        gv2.Rows(gv2.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Name
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colLCode).Value = objTr.Location
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colLName).Value = objTr.Location_Name
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colVCode).Value = objTr.Vendor_Code
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colVName).Value = objTr.Vendor_Name
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_code

                        gv2.Rows(gv2.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colDiscount).Value = objTr.Discount
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colAmt).Value = objTr.Item_Cost
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colComments).Value = objTr.Comments
                    Next
                End If

                If isLoadCopy = False Then
                    If obj.ArrSchedule IsNot Nothing AndAlso obj.ArrSchedule.Count > 0 Then
                        txtScheduleStartDate.Value = obj.ArrSchedule(0).From_Date
                        For Each objTr As clsTenderSchedule In obj.ArrSchedule
                            gvSchedule.Rows.AddNew()
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleSNo).Value = objTr.SNo
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleParentSNo).Value = objTr.PSNo
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleNo).Value = objTr.Schedule_No
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleFromDate).Value = objTr.From_Date
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleToDate).Value = objTr.To_Date
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleVCode).Value = objTr.Vendor_Code
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleVName).Value = clsVendorMaster.GetName(objTr.Vendor_Code, Nothing)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLCode).Value = objTr.Location_Code
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLName).Value = clsLocation.GetName(objTr.Location_Code, Nothing)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleICode).Value = objTr.Item_Code
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleIName).Value = clsItemMaster.GetItemName(objTr.Item_Code, Nothing)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQtyPer).Value = objTr.Schedule_Qty_Per
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQty).Value = objTr.Schedule_Qty
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShortPer).Value = objTr.Schedule_Short_Per
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShort).Value = objTr.Schedule_Short
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Value = objTr.Late_Days
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleExtensionDays).Value = objTr.Extension_Days
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Tag = objTr.Arr
                            If txtScheduleStartDate.Value > objTr.From_Date Then
                                txtScheduleStartDate.Value = objTr.From_Date
                            End If
                        Next
                    End If
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
        PostData()
    End Sub

    Sub PostData()
        Try

            If AllowToSaveRAL_Type(Nothing) = False Then
                Exit Sub
            End If
            If gvSchedule.Rows.Count() <= 0 Then
                If common.clsCommon.MyMessageBoxShow(Me, "Post Document without Schedule." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            If (myMessages.postConfirm()) Then
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    clsTenderHead.PostData(txtDocNo.Value)
                    Dim msg = "Successfully Posted"
                    common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current, False)
                Else
                    Throw New Exception("No Data found to Post")
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
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
            End If
            If (clsTenderHead.DeleteData(txtDocNo.Value)) Then
                saveCancelLog(Reason, "Delete", Nothing)
                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    'Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
    '    If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
    '        Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
    '        editor.AutoSizeDropDownToBestFit = True
    '        editor.EditorControl.MasterTemplate.BestFitColumns()
    '        editor.DropDownStyle = RadDropDownStyle.DropDown
    '        editor.AutoFilter = True
    '        If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
    '            Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
    '            autoFilter.IsFilterEditor = True
    '            editor.EditorControl.FilterDescriptors.Add(autoFilter)
    '        End If
    '    End If
    'End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try

            LoadData(txtDocNo.Value, NavType, False)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub



    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qst As String = "select count(*) from tspl_tender_header where DocumentCode='" + txtDocNo.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            txtDocNo.MyReadOnly = False
        Else
            txtDocNo.MyReadOnly = True
        End If
        If txtDocNo.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select tspl_tender_header.DocumentCode as DocumentNo,convert(varchar(12),tspl_tender_header.Documentdate,103) as Document_date,case when tspl_tender_header.Posted=1 then 'posted' else 'Unposted' end as Posted from tspl_tender_header"
            txtDocNo.Value = clsCommon.ShowSelectForm("TenderNoFndd", qry, "DocumentNo", "", txtDocNo.Value, "DocumentNo", isButtonClicked)
            LoadData(txtDocNo.Value, NavigatorType.Current, False)
        End If

    End Sub

    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colUnit) Then
        '    isCellValueChangedOpen = True
        '    'If chkItemwise.Checked Then
        '    gv1.CurrentColumn = gv1.Columns(colIName)
        '    OpenItemUOMList(True)
        '    gv1.CurrentColumn = gv1.Columns(colUnit)
        '    'Else
        '    '    gv1.CurrentColumn = gv1.Columns(colIName)
        '    '    OpenItemGrpUOMList(True)
        '    '    gv1.CurrentColumn = gv1.Columns(colUnit)
        '    'End If
        '    setGridFocus()
        '    isCellValueChangedOpen = False
        'End If

        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Control AndAlso e.KeyCode = Keys.F7 Then
            'SelectRequistionItems()

        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnreverse.Visible = True
            End If
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                              "TSPL_TENDER_HEADER " + Environment.NewLine +
                              "TSPL_TENDER_DETAIL ")
        End If
    End Sub



    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        'UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
        'RefreshReqNo()
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then

            e.Cancel = True
            'ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colItemUsedINGRN).Value) Then
            '    common.clsCommon.MyMessageBoxShow("Can't Delete The Current Row.This Item is Used In GRN")
            '    e.Cancel = True
        End If
    End Sub


    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                'gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = RowTypeItem
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub


    Private Sub btnreverse_Click(sender As Object, e As EventArgs) Handles btnreverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                '' REASON FOR DELETE 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If clsTenderHead.ReverseAndUnpost(txtDocNo.Value) Then
                    saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current, False)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If ValidateSubmitData() = False Then
            Exit Sub
        End If

        txtItem.Value = ""
        'Dim dt As New DataTable()
        'Dim dtTemp As New DataTable()

        'If gv2 IsNot Nothing AndAlso gv2.Rows.Count > 0 Then
        '    dt = CType(gv2.DataSource, DataTable)
        'End If
        If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
            'dtTemp = CType(gv1.DataSource, DataTable)

            'For Each dr As DataRow In dtTemp.Rows
            '    If clsCommon.myLen(dr.Item("Item Code")) > 0 AndAlso clsCommon.myLen(dr.Item("Location Code")) > 0 AndAlso clsCommon.myLen(dr.Item("Vendor Code")) > 0 AndAlso clsCommon.myLen(dr.Item("UOM")) > 0 AndAlso clsCommon.myCdbl(dr.Item("Amount")) > 0 Then
            '        dt.Rows.Add(dr)
            '    End If
            'Next


            'dt.Merge(dtTemp, True, MissingSchemaAction.Ignore)
            'gv2.Rows.Add(grow)
            'gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intSerialNo
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colICode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colLCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colVCode).Value) > 0 AndAlso ((clsCommon.myCdbl(grow.Cells(colAmt).Value) > 0) OrElse (clsCommon.myCdbl(grow.Cells(colRate).Value) > 0 AndAlso (clsCommon.myCDecimal(cboTenderType.SelectedValue) = 2 OrElse clsCommon.myCDecimal(cboTenderType.SelectedValue) = 3) OrElse (clsCommon.myCDecimal(cboTenderType.SelectedValue) = 2 AndAlso (clsCommon.myCdbl(grow.Cells(colRate).Value) > 0 OrElse clsCommon.myCdbl(grow.Cells(colDiscount).Value) > 0)))) Then

                    gv2.Rows.AddNew()

                    gv2.Rows(gv2.Rows.Count - 1).Cells(colLineNo).Value = 0
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(grow.Cells(colICode).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(grow.Cells(colIName).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colLCode).Value = clsCommon.myCstr(grow.Cells(colLCode).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colLName).Value = clsCommon.myCstr(grow.Cells(colLName).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colVCode).Value = clsCommon.myCstr(grow.Cells(colVCode).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colVName).Value = clsCommon.myCstr(grow.Cells(colVName).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(grow.Cells(colUnit).Value)


                    gv2.Rows(gv2.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colDiscount).Value = clsCommon.myCdbl(grow.Cells(colDiscount).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colAmt).Value = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colRemarks).Value = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colComments).Value = clsCommon.myCstr(grow.Cells(colComments).Value)

                End If
            Next

        End If

        Dim dblTotalAmount As Double = 0
        For Each grow As GridViewRowInfo In gv2.Rows
            dblTotalAmount = dblTotalAmount + clsCommon.myCdbl(grow.Cells(colAmt).Value)
        Next
        lblTotalDocAmt.Text = Math.Round(clsCommon.myCdbl(dblTotalAmount), 2)
        'gv2.Refresh()
        'gv2.DataSource = dt
        LoadBlankGrid1()
        RefreshSerialNo()
    End Sub

    Private Function ValidateSubmitData() As Boolean
        Try

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strVCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colVCode).Value)
                Dim strLCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colLCode).Value)

                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim dblrate As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim dblAmount As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)
                Dim dblDiscount As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDiscount).Value)

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(strLCode) > 0 AndAlso clsCommon.myLen(strVCode) > 0 AndAlso clsCommon.myLen(strUOM) <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter Quantity UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(strLCode) > 0 AndAlso clsCommon.myLen(strVCode) > 0 AndAlso clsCommon.myCdbl(dblQty) <= 0 AndAlso clsCommon.myCDecimal(cboTenderType.SelectedValue) <> 2 AndAlso clsCommon.myCDecimal(cboTenderType.SelectedValue) <> 3 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please enter Booked Quantity for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(strLCode) > 0 AndAlso clsCommon.myLen(strVCode) > 0 AndAlso clsCommon.myCdbl(dblrate) <= 0 AndAlso clsCommon.myCdbl(dblDiscount) <= 0 AndAlso clsCommon.myCDecimal(cboTenderType.SelectedValue) = 2 Then
                    clsCommon.MyMessageBoxShow(Me, "Please enter Rate or Discount for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(strLCode) > 0 AndAlso clsCommon.myLen(strVCode) > 0 AndAlso clsCommon.myCdbl(dblrate) <= 0 AndAlso clsCommon.myCDecimal(cboTenderType.SelectedValue) <> 2 Then
                    clsCommon.MyMessageBoxShow(Me, "Please enter Rate for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
                    Return False
                End If

                If (clsCommon.myLen(strVCode) > 0) Then

                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If jj = ii Then
                            Continue For
                        End If
                        Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                        Dim strInnerLCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colLCode).Value)
                        Dim strInnerVCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colVCode).Value)
                        Dim strInnerUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)

                        If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strLCode, strInnerLCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strVCode, strInnerVCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strInnerUOM) = CompairStringResult.Equal Then
                            Dim Msg As String = "Same Detail Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If

                    Next
                End If
            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function




    Private Sub txtItem__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtItem._MYValidating
        Try
            If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                cboItemType.Focus()
                Throw New Exception("Please select Item Type")
            End If
            Dim whr As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(cboTenderType.SelectedValue), "2") = CompairStringResult.Equal Then
                whr += " TSPL_ITEM_MASTER.TypeOfItm='T' "
            End If
            Dim obj As clsItemMaster
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(txtItem.Value), clsCommon.myCstr(cboItemType.SelectedValue), isButtonClicked, "", whr)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
                txtItem.Value = obj.Item_Code
                LoadBlankGrid1()
                LoadItemGrid1()
            Else
                txtItem.Value = ""
                LoadBlankGrid1()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadItemGrid1()
        Try
            Dim qry As String = "select tspl_location_master.Location_Code,tspl_location_master.Location_Desc,tspl_item_master.item_code,tspl_item_master.Item_Desc,UOM.UOM_Code from 
                                (select 1 as a,Location_Code,Location_Desc from tspl_location_master where 2=2 "
            If objCommonVar.RCDFCFP Then
                qry += "and isnull(IsMainPlant,0)=0 "
            Else
                qry += "and Location_Category<>'MCC' and Is_Sub_Location='N' "
            End If
            qry += ")tspl_location_master  left join (select 1 as a,item_code,Item_Desc from tspl_item_master)tspl_item_master
                                on tspl_item_master.a=tspl_location_master.a
                                left join 
                                (Select top 1 isnull(UOM_Code,'') AS UOM_Code,Item_Code from TSPL_ITEM_UOM_DETAIL where Item_Code ='" + txtItem.Value + "' and Default_UOM=1)UOM
                                ON UOM.Item_Code=tspl_item_master.Item_Code
                                where tspl_item_master.Item_Code='" + txtItem.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim intSerialNo As Integer
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    intSerialNo += 1
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = intSerialNo
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("item_code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLCode).Value = clsCommon.myCstr(dr("Location_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLName).Value = clsCommon.myCstr(dr("Location_Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = clsCommon.myCstr(dr("UOM_Code"))
                Next

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printData()
    End Sub
    Sub printData()
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim strQuery As String = "select TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name 
                    ,TSPL_COMPANY_MASTER.Add1 as comp_add1 , TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone , cast(TSPL_COMPANY_MASTER.logo_img2 as image) as logo_img,tspl_company_master.Pincode,tspl_company_master.Tcan_No
                    ,tspl_tender_header.DocumentCode,tspl_tender_header.DocumentDate,tspl_tender_detail.item_code,TSPL_ITEM_MASTER.item_desc
                    ,TSPL_TENDER_DETAIL.LOCATION,TSPL_LOCATION_MASTER.Loc_Short_Name as LOCATION_DESC
                    ,TSPL_TENDER_DETAIL.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,tspl_tender_detail.Qty,tspl_tender_detail.Rate
                    ,tspl_tender_header.FieldValue1,tspl_tender_header.FieldValue2,tspl_tender_header.FieldValue3
                    ,tspl_tender_header.FieldValue4,tspl_tender_header.FieldValue5,tspl_tender_header.FieldValue6
                    ,tspl_tender_header.OtherInfo1,tspl_tender_header.OtherInfo2,tspl_tender_header.OtherInfo3
                    ,tspl_tender_header.OtherInfo4,tspl_tender_header.OtherInfo5,tspl_tender_header.OtherInfo6
                    ,tspl_tender_header.OtherInfo7,tspl_tender_header.OtherInfo8,tspl_tender_header.OtherInfo9
                    ,tspl_tender_header.OtherInfo10
                    from tspl_tender_detail left join tspl_tender_header
                    on tspl_tender_header.documentcode=tspl_tender_detail.documentcode
                    left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.VENDOR_Code=TSPL_TENDER_DETAIL.Vendor_Code
                    left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.LOCATION_CODE=TSPL_TENDER_DETAIL.LOCATION
                    left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_TENDER_DETAIL.Item_Code
                    left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code =tspl_tender_header.Comp_Code
                    where tspl_tender_detail.documentcode='" + txtDocNo.Value + "'
                    order by tspl_tender_detail.line_no"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptTender", "Tender", clsCommon.myCDate(dt.Rows(0)("DocumentDate")))
                frmCRV = Nothing
            Else
                Throw New Exception("Please select document to print")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gv2_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try

            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv2.Columns(colUnit) OrElse e.Column Is gv2.Columns(colVCode) OrElse e.Column Is gv2.Columns(colQty) OrElse e.Column Is gv2.Columns(colRate) Then
                        If e.Column Is gv2.Columns(colVCode) Then
                            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER "
                            Dim whrCls As String = " TSPL_VENDOR_MASTER.Status='N'"
                            gv2.CurrentRow.Cells(colVCode).Value = clsCommon.ShowSelectForm("TenVendorFndr", qry, "Code", whrCls, clsCommon.myCstr(gv2.CurrentRow.Cells(colVCode).Value), "Code", False)
                            If clsCommon.myLen(gv2.CurrentRow.Cells(colVCode).Value) > 0 Then
                                gv2.CurrentRow.Cells(colVName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where vendor_code='" + gv2.CurrentRow.Cells(colVCode).Value + "'"))
                            End If

                        ElseIf e.Column Is gv2.Columns(colUnit) Then
                            Dim strICode As String = clsCommon.myCstr(gv2.CurrentRow.Cells(colICode).Value)

                            If clsCommon.myLen(strICode) > 0 Then
                                'OpenUOMList(False)
                                If clsCommon.myLen(strICode) > 0 Then
                                    Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
                                    Dim whrCls As String = "Item_Code='" + strICode + "'"
                                    gv2.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("ITEM-TenFndr2", qry, "Code", whrCls, clsCommon.myCstr(gv2.CurrentRow.Cells(colUnit).Value), "Code", False)

                                End If

                            Else
                                Throw New Exception("Please fill item first.")
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If
                        ElseIf e.Column Is gv2.Columns(colQty) OrElse e.Column Is gv2.Columns(colRate) Then
                            gv2.CurrentRow.Cells(colAmt).Value = clsCommon.myCdbl(gv2.CurrentRow.Cells(colRate).Value) * clsCommon.myCdbl(gv2.CurrentRow.Cells(colQty).Value)
                            For index = 0 To gvSchedule.Rows.Count - 1
                                If clsCommon.myCDecimal(gv2.CurrentRow.Cells(colLineNo).Value) = clsCommon.myCDecimal(gvSchedule.Rows(index).Cells(colScheduleParentSNo).Value) Then
                                    gvSchedule.Rows(index).Cells(colScheduleQty).Value = ((clsCommon.myCDecimal(gv2.CurrentRow.Cells(colQty).Value) * clsCommon.myCDecimal(gvSchedule.Rows(index).Cells(colScheduleQtyPer).Value)) / 100)
                                End If
                            Next
                        End If
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        Try
            'isLoadCopy = True
            Dim qry As String = "select tspl_tender_header.DocumentCode as DocumentNo,convert(varchar(12),tspl_tender_header.Documentdate,103) as Document_date,case when tspl_tender_header.Posted=1 then 'posted' else 'Unposted' end as Posted from tspl_tender_header"
            Dim strTender As String = clsCommon.ShowSelectForm("TenderNoFndd1", qry, "DocumentNo", "", "", "DocumentNo", True)
            If clsCommon.myLen(strTender) > 0 Then
                LoadData(strTender, NavigatorType.Current, True)
                txtDocNo.Value = ""
                txtDocNo.MyReadOnly = False
                isNewEntry = True
                btnSave.Text = "Save"
                lblTenderSeqNo.Text = ""
                btnSave.Enabled = True
                btnDelete.Enabled = False
                btnPost.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Pending
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGridSchedule()
        gvSchedule.Rows.Clear()
        gvSchedule.Columns.Clear()


        Dim repoNum As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "SNo"
        repoNum.Name = colScheduleSNo
        repoNum.Width = 50
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Schedule No"
        repoNum.Name = colScheduleNo
        repoNum.Width = 100
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "From Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colScheduleFromDate
        repoDate.WrapText = True
        repoDate.ReadOnly = False
        repoDate.Width = 80
        gvSchedule.MasterTemplate.Columns.Add(repoDate)

        repoDate = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "To Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colScheduleToDate
        repoDate.WrapText = True
        repoDate.ReadOnly = False
        repoDate.Width = 80
        gvSchedule.MasterTemplate.Columns.Add(repoDate)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Parent SNo"
        repoNum.Name = colScheduleParentSNo
        repoNum.Width = 50
        repoNum.ReadOnly = True
        repoNum.IsVisible = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        Dim repotxt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repotxt.FormatString = ""
        repotxt.HeaderText = "Vendor Code"
        repotxt.Name = colScheduleVCode
        'repotxt.HeaderImage = My.Resources.search4
        'repotxt.TextImageRelation = TextImageRelation.TextBeforeImage
        repotxt.ReadOnly = True
        repotxt.Width = 100
        repotxt.IsVisible = True
        gvSchedule.MasterTemplate.Columns.Add(repotxt)

        repotxt = New GridViewTextBoxColumn()
        repotxt.FormatString = ""
        repotxt.HeaderText = "Vendor Name"
        repotxt.Name = colScheduleVName
        repotxt.Width = 150
        repotxt.ReadOnly = True
        repotxt.IsVisible = True
        gvSchedule.MasterTemplate.Columns.Add(repotxt)

        repotxt = New GridViewTextBoxColumn()
        repotxt.FormatString = ""
        repotxt.HeaderText = "Location Code"
        repotxt.Name = colScheduleLCode
        repotxt.ReadOnly = True
        repotxt.Width = 100
        repotxt.IsVisible = True
        gvSchedule.MasterTemplate.Columns.Add(repotxt)

        repotxt = New GridViewTextBoxColumn()
        repotxt.FormatString = ""
        repotxt.HeaderText = "Location Name"
        repotxt.Name = colScheduleLName
        repotxt.Width = 150
        repotxt.ReadOnly = True
        repotxt.IsVisible = True
        gvSchedule.MasterTemplate.Columns.Add(repotxt)


        repotxt = New GridViewTextBoxColumn()
        repotxt.FormatString = ""
        repotxt.HeaderText = "Item Code"
        repotxt.Name = colScheduleICode
        repotxt.ReadOnly = True
        repotxt.Width = 100
        repotxt.IsVisible = True
        gvSchedule.MasterTemplate.Columns.Add(repotxt)

        repotxt = New GridViewTextBoxColumn()
        repotxt.FormatString = ""
        repotxt.HeaderText = "Item Description"
        repotxt.Name = colScheduleIName
        repotxt.Width = 150
        repotxt.ReadOnly = True
        repotxt.IsVisible = True
        gvSchedule.MasterTemplate.Columns.Add(repotxt)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Quantity %"
        repoNum.Name = colScheduleQtyPer
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Quantity"
        repoNum.Name = colScheduleQty
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Short %"
        repoNum.Name = colScheduleShortPer
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Short Quantity"
        repoNum.Name = colScheduleShort
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Late Days"
        repoNum.Name = colScheduleLateDays
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Extension Days"
        repoNum.Name = colScheduleExtensionDays
        repoNum.ReadOnly = False
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        gvSchedule.AllowDeleteRow = True
        gvSchedule.AllowAddNewRow = False
        gvSchedule.ShowGroupPanel = False
        gvSchedule.AllowColumnReorder = False
        gvSchedule.AllowRowReorder = False
        gvSchedule.EnableSorting = False
        gvSchedule.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvSchedule.MasterTemplate.ShowRowHeaderColumn = False
        gvSchedule.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        SetSchedule()
    End Sub

    Sub SetSchedule()
        Try
            isInsideLoadData = True
            LoadBlankGridSchedule()
            For ii As Integer = 0 To gv2.Rows.Count - 1
                If clsCommon.myLen(gv2.Rows(ii).Cells(colVCode).Value) > 0 AndAlso clsCommon.myLen(gv2.Rows(ii).Cells(colLCode).Value) > 0 AndAlso clsCommon.myLen(gv2.Rows(ii).Cells(colICode).Value) > 0 AndAlso clsCommon.myCDecimal(gv2.Rows(ii).Cells(colQty).Value) > 0 Then
                    Dim dtRunningDate As DateTime = txtScheduleStartDate.Value
                    Dim ArrSch As List(Of clsItemSchedule) = clsItemSchedule.GetData(clsCommon.myCstr(gv2.Rows(ii).Cells(colICode).Value), Nothing)
                    If ArrSch IsNot Nothing AndAlso ArrSch.Count > 0 Then
                        For Each obj As clsItemSchedule In ArrSch
                            gvSchedule.Rows.AddNew()
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleSNo).Value = gvSchedule.Rows.Count
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleNo).Value = obj.SNo
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleFromDate).Value = dtRunningDate
                            dtRunningDate = dtRunningDate.AddDays(obj.Days - 1)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleToDate).Value = dtRunningDate
                            dtRunningDate = dtRunningDate.AddDays(1)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleParentSNo).Value = clsCommon.myCDecimal(gv2.Rows(ii).Cells(colLineNo).Value)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleVCode).Value = clsCommon.myCstr(gv2.Rows(ii).Cells(colVCode).Value)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleVName).Value = clsCommon.myCstr(gv2.Rows(ii).Cells(colVName).Value)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLCode).Value = clsCommon.myCstr(gv2.Rows(ii).Cells(colLCode).Value)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLName).Value = clsCommon.myCstr(gv2.Rows(ii).Cells(colLName).Value)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleICode).Value = clsCommon.myCstr(gv2.Rows(ii).Cells(colICode).Value)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleIName).Value = clsCommon.myCstr(gv2.Rows(ii).Cells(colIName).Value)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQtyPer).Value = obj.Qty_Per
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQty).Value = ((clsCommon.myCDecimal(gv2.Rows(ii).Cells(colQty).Value) * obj.Qty_Per) / 100)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShortPer).Value = obj.Short_Per
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShort).Value = ((clsCommon.myCDecimal(gv2.Rows(ii).Cells(colQty).Value) * obj.Short_Per) / 100)
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Value = obj.Late_Days
                            gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Tag = SetSchedulePenalty(obj.Arr, dtRunningDate)
                        Next
                    End If
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Function SetSchedulePenalty(ByVal Arr As List(Of clsItemSchedulePenalty), ByVal dtRunningDate As DateTime) As List(Of clsTenderSchedulePenelty)
        Dim ArrTemp As List(Of clsTenderSchedulePenelty) = Nothing
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            ArrTemp = New List(Of clsTenderSchedulePenelty)
            For Each objtr As clsItemSchedulePenalty In Arr
                Dim objTemp As New clsTenderSchedulePenelty
                objTemp.Penalty_Date = dtRunningDate.AddDays(objtr.Penalty_Days - 1)
                objTemp.Penalty = objtr.Penalty
                ArrTemp.Add(objTemp)
            Next
        End If
        Return ArrTemp
    End Function

    Private Sub ShowPenalty()
        Try
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add("Penalty Date", GetType(String))
            dt.Columns.Add("Penalty", GetType(Decimal))

            Dim arr As List(Of clsTenderSchedulePenelty) = TryCast(gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag, List(Of clsTenderSchedulePenelty))
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For ii As Integer = 0 To arr.Count - 1
                    Dim dr As DataRow = dt.NewRow
                    dr("Penalty Date") = clsCommon.GetPrintDate(arr(ii).Penalty_Date, "dd/MM/yyyy")
                    dr("Penalty") = arr(ii).Penalty
                    dt.Rows.Add(dr)
                Next
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frm As New FrmFreeGrid
                frm.dt = dt
                'frm.arrEditableColumn = New List(Of String)
                'frm.arrEditableColumn.Add("Penalty")
                frm.strFormName = "Show Penalty"
                frm.ReportID = "SchPenaltyD"
                frm.WindowState = FormWindowState.Maximized
                frm.ShowDialog()
                'If frm.dt IsNot Nothing AndAlso frm.dt.Rows.Count > 0 Then
                '    Dim ArrTemp As New List(Of clsItemSchedulePenalty)
                '    Dim obj As clsItemSchedulePenalty = Nothing
                '    For Each dr As DataRow In frm.dt.Rows
                '        obj = New clsItemSchedulePenalty()
                '        obj.Penalty_Days = clsCommon.myCDecimal(dr("Days"))
                '        obj.Penalty = clsCommon.myCDecimal(dr("Penalty"))
                '        ArrTemp.Add(obj)
                '    Next
                '    gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag = ArrTemp
                'Else
                '    gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag = Nothing
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CellFormatting(sender As Object, e As UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column Is gv1.Columns(colQty) Then
                gv1.CurrentRow.Cells(colQty).ReadOnly = (clsCommon.myCDecimal(cboTenderType.SelectedValue) = 2)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gv2_CellFormatting(sender As Object, e As UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column Is gv2.Columns(colQty) Then
                gv2.CurrentRow.Cells(colQty).ReadOnly = (clsCommon.myCDecimal(cboTenderType.SelectedValue) = 2)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gvSchedule_KeyDown(sender As Object, e As KeyEventArgs) Handles gvSchedule.KeyDown
        If e.KeyCode = Keys.F5 Then
            ShowPenalty()
        End If
    End Sub

    Dim isCellValueChangedOpenSchedule As Boolean = False
    Private Sub gvSchedule_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSchedule.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpenSchedule Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvSchedule.Columns(colScheduleToDate) Then
                        Dim PKID As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PK_Id from (select ROW_NUMBER() over (Partition by Item_Code order by PK_Id) as SNO, * from TSPL_ITEM_SCHEDULE where Item_Code= '" + clsCommon.myCstr(gvSchedule.CurrentRow.Cells(colScheduleICode).Value) + "' )xx where SNO=" + clsCommon.myCstr(gvSchedule.CurrentRow.Cells(colScheduleSNo).Value) + ""))
                        If clsCommon.myLen(PKID) > 0 Then
                            Dim Arr As List(Of clsItemSchedulePenalty) = clsItemSchedulePenalty.GetData(PKID, Nothing)
                            gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag = SetSchedulePenalty(Arr, clsCommon.myCDate(gvSchedule.CurrentRow.Cells(colScheduleToDate).Value).AddDays(1))
                        End If
                    End If
                End If
                isCellValueChangedOpenSchedule = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpenSchedule = False
        End Try
    End Sub

    Private Sub btnApplyAll_Click(sender As Object, e As EventArgs) Handles btnApplyAll.Click
        Try
            For ii As Integer = 0 To gvSchedule.Rows.Count - 1
                gvSchedule.Rows(ii).Cells(colScheduleExtensionDays).Value = txtExtensionDays.Value
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExtensionUpdate_Click(sender As Object, e As EventArgs) Handles btnExtensionUpdate.Click
        Try
            Dim Reason As String = ""
            If (myMessages.updateConfirm) Then
                'If clsCancelLog.CheckForReasonOnUpdateAfterPost() Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "sirc"
                frm.strCode = "sireversandcreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    '' REASON FOR DELETE 
                    Dim frm1 As New FrmFreeTxtBox1
                    frm1.Text = "Remarks for Update"
                    frm1.ShowDialog()
                    If clsCommon.myLen(frm1.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm1.strRmks
                    End If

                    For ii As Integer = 0 To gvSchedule.Rows.Count - 1
                        If CInt(gvSchedule.Rows(ii).Cells(colScheduleExtensionDays).Value) >= 0 Then
                            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_TENDER_SCHEDULE SET Extension_Days='" + clsCommon.myCstr(gvSchedule.Rows(ii).Cells(colScheduleExtensionDays).Value) + "' where DocumentCode='" + txtDocNo.Value + "' and PSNo='" + clsCommon.myCstr(gvSchedule.Rows(ii).Cells(colScheduleParentSNo).Value) + "' and Schedule_No='" + clsCommon.myCstr(gvSchedule.Rows(ii).Cells(colScheduleNo).Value) + "' and Vendor_Code='" + clsCommon.myCstr(gvSchedule.Rows(ii).Cells(colScheduleVCode).Value) + "' and Location_Code='" + clsCommon.myCstr(gvSchedule.Rows(ii).Cells(colScheduleLCode).Value) + "' and Item_Code='" + clsCommon.myCstr(gvSchedule.Rows(ii).Cells(colScheduleICode).Value) + "'")
                        End If
                    Next

                    saveCancelLog(Reason, "Update", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Update Successfully ", Me.Text)
                End If
                'End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub Import_Click(sender As Object, e As EventArgs)
        CloseForm()
    End Sub

    Private Sub ItemClose_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub RadMenuItem8_Click(sender As Object, e As EventArgs) Handles RadMenuItem8.Click
        funImport()
    End Sub

    Private Sub RadMenuItem9_Click(sender As Object, e As EventArgs) Handles RadMenuItem9.Click
        Try
            Dim str As String = "select tspl_tender_detail.Item_Code,TSPL_ITEM_MASTER.Item_Desc,tspl_tender_detail.Location,tspl_tender_detail.Vendor_Code, TSPL_VENDOR_MASTER.vendor_name,tspl_tender_detail.Unit_code,tspl_tender_detail.Qty,tspl_tender_detail.Rate, tspl_tender_detail.Item_cost as Amount,tspl_tender_detail.Discount , tspl_tender_detail.Remarks, tspl_tender_detail.Comments from  tspl_tender_detail 
					left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE=TSPL_TENDER_DETAIL.Item_Code
                    left outer join tspl_tender_header on tspl_tender_header.DocumentCode=tspl_tender_detail.DocumentCode
					 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.VENDOR_Code=TSPL_TENDER_DETAIL.Vendor_Code where  tspl_tender_header.DocumentCode='" + txtDocNo.Value + "' "
            ListImpExpColumnsMandatory = New List(Of String)({"Item_Code", "Item_Desc", "Vendor_Code", "vendor_name", "Location"})
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funImport()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        isInsideLoadData = True
        Try
            If clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells("colICode").Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If
        Catch ex As Exception
        End Try

        If transportSql.importExcel(gv, "Item_Code", "Item_Desc", "Location", "Vendor_Code", "vendor_name", "Unit_code", "Qty", "Rate", "Amount", "Discount", "Remarks", "Comments") Then
            Dim linno As Integer = 0
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    linno += 1
                    gv1.Rows.AddNew()
                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Item_Code").Value))) Then
                        Throw New Exception("Item Code Cannot be empty" + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code  from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(grow.Cells("Item_Code").Value) + "'"))
                        If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Item_Code").Value)) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells("colICode").Value = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                        Else
                            '   Throw New Exception("Item Code Not Exists at line no ." + clsCommon.myCstr(linno) + ".")
                            gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                        End If
                    End If
                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Item_Desc").Value))) Then
                        Throw New Exception("Item Name Cannot be empty" + clsCommon.myCstr(linno) + ".")

                    Else
                        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc  from TSPL_ITEM_MASTER where Item_Desc='" + clsCommon.myCstr(grow.Cells("Item_Desc").Value) + "'"))
                        If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Item_Desc").Value)) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells("colIName").Value = clsCommon.myCstr(grow.Cells("Item_Desc").Value)
                        Else
                            ' Throw New Exception("Item Name Not Exists  at line no." + clsCommon.myCstr(linno) + ".")
                            gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                        End If
                    End If
                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Location").Value))) Then
                        Throw New Exception("Location Cannot be empty" + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from TSPL_LOCATION_MASTER where location_code='" + clsCommon.myCstr(grow.Cells("Location").Value) + "'"))
                        If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Location").Value)) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells("colLCode").Value = clsCommon.myCstr(grow.Cells("Location").Value)
                        Else
                            ' Throw New Exception("Location Not Exists  at line no." + clsCommon.myCstr(linno) + ".")
                            gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                        End If
                    End If
                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Vendor_Code").Value))) Then
                        Throw New Exception("Vendor Code Cannot be empty" + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER where Vendor_Code='" + clsCommon.myCstr(grow.Cells("Vendor_Code").Value) + "'"))
                        If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Vendor_Code").Value)) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells("colVCode").Value = clsCommon.myCstr(grow.Cells("Vendor_Code").Value)
                        Else
                            '  Throw New Exception("Vendor Code Not Exists  at line no." + clsCommon.myCstr(linno) + ".")
                            gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                        End If
                    End If
                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("vendor_name").Value))) Then
                        Throw New Exception("Vendor Name Cannot be empty" + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where vendor_name='" + clsCommon.myCstr(grow.Cells("vendor_name").Value) + "'"))
                        If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("vendor_name").Value)) = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells("colVName").Value = clsCommon.myCstr(grow.Cells("vendor_name").Value)
                        Else
                            ' Throw New Exception("Vendor Code Not Exists at line no." + clsCommon.myCstr(linno) + ".")
                            gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                        End If
                    End If
                    If grow.Cells("Unit_code").Value IsNot Nothing Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells("colUnit").Value = clsCommon.myCdbl(grow.Cells("Unit_code").Value)
                    End If
                    If grow.Cells("Qty").Value IsNot Nothing Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells("colQty").Value = clsCommon.myCdbl(grow.Cells("Qty").Value)
                    End If
                    If grow.Cells("Rate").Value IsNot Nothing Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells("colRate").Value = clsCommon.myCdbl(grow.Cells("Rate").Value)
                    End If
                    If grow.Cells("Amount").Value IsNot Nothing Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells("colAmt").Value = clsCommon.myCdbl(grow.Cells("Amount").Value)
                    End If
                    If grow.Cells("Discount").Value IsNot Nothing Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells("colDiscount").Value = clsCommon.myCdbl(grow.Cells("Discount").Value)
                    End If
                    If grow.Cells("Remarks").Value IsNot Nothing Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells("colRemarks").Value = clsCommon.myCstr(grow.Cells("Remarks").Value)
                    End If
                    If grow.Cells("Comments").Value IsNot Nothing Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells("colComments").Value = clsCommon.myCstr(grow.Cells("Comments").Value)
                    End If
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                ' gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End Try
        End If
    End Sub
    Private Sub chkRalclose_CheckedChanged(sender As Object, e As EventArgs) Handles chkRalclose.CheckedChanged
        If chkRalclose.Checked = True And vaddnew = "N" Then
            Dim response = MsgBox("Are you sure want to close the RAL", MsgBoxStyle.YesNo, "Attention")
            If response = MsgBoxResult.Yes Then
                closeyn = "Y"
                closeRal()
            ElseIf response = MsgBoxResult.No Then
                chkRalclose.Checked = False
            End If
        ElseIf chkRalclose.Checked = False And vaddnew = "N" Then
            closeyn = "N"
            closeRal()
        End If
        vaddnew = "N"
        If chkRalclose.Checked Then
            Dim makereadonly As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MakeClosingofPOreadonlyforuser, clsFixedParameterCode.MakeClosingofPOreadonlyforuser, Nothing)) = "1", True, False))
            If makereadonly Then
                chkRalclose.Enabled = False
            Else
                chkRalclose.Enabled = True
            End If
        End If
    End Sub

    Private Sub gv2_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv2.UserDeletedRow
        For ii As Integer = 0 To gv2.Rows.Count - 1
            Dim SNo As Integer = gv2.Rows(ii).Cells(colLineNo).Value
            gv2.Rows(ii).Cells(colLineNo).Value = ii + 1
            For index = gvSchedule.Rows.Count - 1 To 0 Step -1
                If SNo = clsCommon.myCDecimal(gvSchedule.Rows(index).Cells(colScheduleParentSNo).Value) Then
                    gvSchedule.Rows(index).Cells(colScheduleParentSNo).Value = ii + 1
                End If
            Next
        Next
    End Sub

    Private Sub gv2_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv2.UserDeletingRow
        If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        Else
            Dim SNo As Integer = clsCommon.myCDecimal(gv2.CurrentRow.Cells(colLineNo).Value)
            For index = gvSchedule.Rows.Count - 1 To 0 Step -1
                If SNo = clsCommon.myCDecimal(gvSchedule.Rows(index).Cells(colScheduleParentSNo).Value) Then
                    gvSchedule.Rows.RemoveAt(index)
                End If
            Next
        End If

    End Sub

    Sub closeRal()
        Try
            If (clsTenderHead.closeRaldata(txtDocNo.Value, True, closeyn, strRemarks)) Then
                If closeyn = "Y" Then
                    Dim Reason As String = ""
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Reason for Close RAL "
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                        strRemarks = Reason
                    End If
                    Dim obj As New clsTenderHead()
                    If (clsTenderHead.closeRaldata(txtDocNo.Value, True, closeyn, strRemarks)) Then
                        clsCommon.MyMessageBoxShow(Me, "Successfully Closed")
                    End If

                ElseIf closeyn = "N" Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Opened")
                End If
                LoadData(txtDocNo.Value, NavigatorType.Current, False)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

End Class
