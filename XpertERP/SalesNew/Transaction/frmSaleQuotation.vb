''18/06/2012---Updation by --[Pankaj kumar]-- Commented Grid Formating Event Code So that Grid Cell's Color Could Not be change on clicking 
'' Added By Abhishek as on 30 Nov 2012 4:16 Pm For Location Lock
'by vipin for some feild hidden on 07/03/addnew

Imports common
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Net
Imports System.IO


Public Class frmSaleQuotation
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim attachqry As String = ""
    Private isCellValueChangedOpen As Boolean = False
    Dim iStxtTaxGroup_TxtChangedComplete As Boolean = True
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim vaddnew As String
    Const colLineNo As String = "LNO"
    Const colComplete As String = "COMPLETE"
    Const colICode As String = "ICODE"
    Const colIName As String = "INAME"
    Const colBalanceQty As String = "BALANCEQTY"
    Const colQty As String = "QTY"
    Const colUnit As String = "COLTAX3"
    Const colRate As String = "RATE"
    Const colAmt As String = "AMT"
    Const colDiscountPer As String = "Discount%"
    Const colDiscAmt As String = "Discount Amount"
    Const colAmtAfterDisc As String = "AmtAfterDiscount"
    ''Const colLocationName As String = "LOCATIONNAME"
    Const colVendorItemNo As String = "VENDORITEMNO"
    Const colOrderNo As String = "ORDERNO"

    Const colSpecification As String = "SPECIFICATION"
    Const colRemarks As String = "REMARKS"
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim closeyn As String
    Dim repoBalQty As GridViewDecimalColumn
    Dim repoComplete As GridViewTextBoxColumn
    Public DocCode As String
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSaleQuotation)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub fndProject__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProject._MYValidating
        Dim qry As String = "select PROJECT_CODE as Code,SPECIFICATION,PROJECT_STATUS as Status from TSPL_PJC_PROJECT"
        fndProject.Value = clsCommon.ShowSelectForm("Project Code", qry, "Code", "", fndProject.Value, "", isButtonClicked)
        lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")
    End Sub
    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        SetMailRight()
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
        gv1.Rows.AddNew()
        LoadModeOfTrasport()
        LoadItemType()
        AddNew()
        SetLength()
        btnsetting.Visible = False
        chkRateDefaultSetting.ToggleState = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SalesRateEditable, clsFixedParameterCode.SalesRateEditable, Nothing)) = 1, ToggleState.On, ToggleState.Off)

        If clsCommon.myLen(DocCode) > 0 Then
            LoadData(DocCode, NavigatorType.Current)
        End If
        isNewEntry = True

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''End of For Custom Fields

        '' MultiCurrency
        SetMultiCurrencyVisibility()
        '' End of MultiCurrency

        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment

    End Sub
    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
            lblCurrency.Visible = True
            txtCurrencyCode.Visible = True
            lblConvRate.Visible = True
            txtConversionRate.Visible = True
            lblEffectiveFrom.Visible = True
            txtApplicableFrom.Visible = True
            If clsCommon.myLen(Me.txtVendorNo.Value) > 0 Then
                strq = "select currency_code from TSPL_CUSTOMER_MASTER where cust_code='" & clsCommon.myCstr(Me.txtVendorNo.Value) & "'"
                Me.txtCurrencyCode.Value = clsDBFuncationality.getSingleValue(strq).ToString
                ShowCurrencyDetail()
            End If
            
        Else
            lblCurrency.Visible = False
            txtCurrencyCode.Visible = False
            lblConvRate.Visible = False
            txtConversionRate.Visible = False
            lblEffectiveFrom.Visible = False
            txtApplicableFrom.Visible = False
        End If

    End Sub
    Sub ShowCurrencyDetail()
        'Dim strq As String
        Dim dt As DataTable
        If clsCommon.myLen(clsCommon.myCstr(Me.txtVendorNo.Value)) = 0 Then
            Exit Sub
        End If

        If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
            dt = clsModuleCurrencyMapping.GetLatestCurConvRateDT(Me.txtDate.Value, txtCurrencyCode.Value)
            If dt.Rows.Count = 0 Then
                If Me.txtCurrencyCode.Value = objCommonVar.BaseCurrencyCode Then
                    Me.txtConversionRate.Text = 1
                    Me.txtApplicableFrom.Text = ""
                Else
                    clsCommon.MyMessageBoxShow("Conversion rate not entered for currency '" & Me.txtCurrencyCode.Value & "'")
                    Exit Sub
                End If
               
            Else
                Me.txtConversionRate.Text = dt.Rows(0).Item("Rate")
                Me.txtApplicableFrom.Text = clsCommon.GetPrintDate(dt.Rows(0).Item("FROM_DATE"), "dd/MMM/yyyy")
            End If
        Else
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
        End If

    End Sub
    Sub SetLength()
        txtReqNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtCustOrderNo.MaxLength = 50
        txtRefNo.MaxLength = 50
        txtRmks.MaxLength = 200
        txtComment.MaxLength = 200
        cboModeOfTransport.MaxLength = 12
        txtRequestBy.MaxLength = 100
    End Sub

    Sub LoadItemType()
        cboItemType.DataSource = clsItemMaster.GetItemType()
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
        cboItemType.SelectedIndex = 2
        cboItemType.Visible = False
        RadLabel10.Visible = False
    End Sub

    Sub LoadModeOfTrasport()
        cboModeOfTransport.Items.Add("By Road")
        cboModeOfTransport.Items.Add("By Air")
        cboModeOfTransport.Items.Add("By Sea")
    End Sub

    Sub BlankAllControls()
        txtReqNo.Value = ""
        txtDesc.Text = ""
        txtCustOrderNo.Text = ""
        txtRefNo.Text = ""
        txtRmks.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtComment.Text = ""
        cboModeOfTransport.Text = ""
        chkOnHold.Checked = False
        txtDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtExpireDate.Value = txtDate.Value
        txtRequiredDate.Value = txtDate.Value
        lblTotRAmt.Text = ""
        txtRequestBy.Text = ""
        fndProject.Value = ""
        lblProject.Text = ""
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

        repoComplete = New GridViewTextBoxColumn()
        repoComplete.FormatString = ""
        repoComplete.HeaderText = "Complete"
        repoComplete.Width = 70
        repoComplete.Name = colComplete
        repoComplete.ReadOnly = True
        repoComplete.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoComplete)


        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
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

        repoBalQty = New GridViewDecimalColumn()
        repoBalQty.FormatString = ""
        repoBalQty.WrapText = True
        repoBalQty.HeaderText = "Balance Quantity"
        repoBalQty.Name = colBalanceQty
        repoBalQty.Width = 80
        repoBalQty.Minimum = 0
        repoBalQty.IsVisible = False
        repoBalQty.ReadOnly = True
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoBalQty)

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
        repoUnit.ReadOnly = False
        repoUnit.Width = 80
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Unit Price"
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

        Dim DiscPercent As GridViewDecimalColumn = New GridViewDecimalColumn()
        DiscPercent = New GridViewDecimalColumn()
        DiscPercent.FormatString = ""
        DiscPercent.HeaderText = "Discount%"
        DiscPercent.Name = colDiscountPer
        DiscPercent.Width = 70
        DiscPercent.ReadOnly = False
        DiscPercent.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(DiscPercent)

        Dim DiscountAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        DiscountAmt = New GridViewDecimalColumn()
        DiscountAmt.FormatString = ""
        DiscountAmt.HeaderText = "Discount Amount"
        DiscountAmt.Name = colDiscAmt
        DiscountAmt.Width = 80
        DiscountAmt.ReadOnly = True
        DiscPercent.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(DiscountAmt)

        Dim AmtAfterDisc As GridViewDecimalColumn = New GridViewDecimalColumn()
        AmtAfterDisc = New GridViewDecimalColumn()
        AmtAfterDisc.FormatString = ""
        AmtAfterDisc.HeaderText = "Net Discount"
        AmtAfterDisc.Name = colAmtAfterDisc
        AmtAfterDisc.Width = 80
        AmtAfterDisc.ReadOnly = True
        AmtAfterDisc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(AmtAfterDisc)

        Dim repoVendroItemNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendroItemNo = New GridViewTextBoxColumn()
        repoVendroItemNo.FormatString = ""
        repoVendroItemNo.HeaderText = "Customer Item No"
        repoVendroItemNo.Name = colVendorItemNo
        repoVendroItemNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoVendroItemNo)

        Dim repoOrderNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOrderNo = New GridViewTextBoxColumn()
        repoOrderNo.FormatString = ""
        repoOrderNo.HeaderText = "Order No"
        repoOrderNo.Name = colOrderNo
        repoOrderNo.Width = 100
        repoOrderNo.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoOrderNo.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoOrderNo)

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
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colVendorItemNo) OrElse e.Column Is gv1.Columns(colOrderNo) OrElse e.Column Is gv1.Columns(colSpecification) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colDiscountPer) OrElse e.Column Is gv1.Columns(colUnit) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colOrderNo) Then
                            OpenOrderList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        ElseIf (e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate)) OrElse e.Column Is gv1.Columns(colDiscountPer) Then
                            UpdateCurrentRow()
                            UpdateAllTotals()

                        End If
                        setGridFocus()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isCellValueChangedOpen = False
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
        If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Item Type")
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            cboItemType.Focus()
            Exit Sub
        End If

        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), True, isButtonClick, "", txtVendorNo.Value)

        'clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(cboItemType.SelectedValue), isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
            Dim objCustItem As clsCustomeritemDetails = clsCustomeritemDetails.GetItemRateAndDiscount(txtVendorNo.Value, obj.Item_Code, obj.Unit_Code, txtDate.Value)
            If objCustItem IsNot Nothing Then
                gv1.CurrentRow.Cells(colRate).Value = objCustItem.Approval_Item_Rate
                gv1.CurrentRow.Cells(colDiscountPer).Value = clsCommon.myCdbl(objCustItem.Discount_Per)

            Else
                gv1.CurrentRow.Cells(colRate).Value = 0
            End If
        Else
            gv1.CurrentRow.Cells(colICode).Value = ""
            gv1.CurrentRow.Cells(colIName).Value = ""
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colRate).Value = 0
            gv1.CurrentRow.Cells(colDiscountPer).Value = 0
            gv1.CurrentRow.Cells(colDiscAmt).Value = 0
            gv1.CurrentRow.Cells(colAmtAfterDisc).Value = 0
        End If
        UpdateCurrentRow()
        UpdateAllTotals()
        setBalance()
    End Sub

    Sub OpenOrderList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Order_No as Code,CONVERT(varchar(10),Order_Date,103) as Date from TSPL_SALES_ORDER_HEAD"
        gv1.CurrentRow.Cells(colOrderNo).Value = clsCommon.ShowSelectForm("SQOrdfnd", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colOrderNo).Value), "Code", isButtonClick)
    End Sub

    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        'gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        'If intCurrRow = gv1.Rows.Count - 1 Then
        '    gv1.Rows.AddNew()
        '    gv1.CurrentRow = gv1.Rows(intCurrRow)
        'End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colRate)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colRate) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colVendorItemNo)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colVendorItemNo) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colOrderNo)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colOrderNo) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colSpecification)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colSpecification) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colRemarks)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colRemarks) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
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

    Private Sub UpdateCurrentRow()
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            Dim dblQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRate).Value)
            Dim dblAmt As Double = (dblQty * dblRate)
            Dim dblDisAmt As Double = dblAmt * clsCommon.myCdbl(gv1.CurrentRow.Cells(colDiscountPer).Value) / 100
            gv1.CurrentRow.Cells(colAmt).Value = dblAmt

            gv1.CurrentRow.Cells(colDiscAmt).Value = dblDisAmt
            gv1.CurrentRow.Cells(colAmtAfterDisc).Value = dblAmt - dblDisAmt
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
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDisc).Value)
            End If
        Next
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
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
        cboModeOfTransport.Text = "By Road"
        txtDate.Focus()
        cboItemType.SelectedIndex = 2

        cboItemType.Enabled = True
        txtLocation.Enabled = True
        gv1.Rows.AddNew()

        txtSalesman.Value = ""
        lblSalesman.Text = ""
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        vaddnew = "Y"
        chkqclose.Checked = False

        txtDate.Enabled = True
        txtVendorNo.Enabled = True
        ''For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.SetDefaultValues()
        End If
        ''End of For Custom Fields
        UcAttachment1.BlankAllControls()

    End Sub

    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Description", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Function AllowToSave() As Boolean
        Try
            UpdateAllTotals()
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Location")
                txtLocation.Focus()
                Return False
            End If

            If clsCommon.myLen(cboItemType.SelectedValue) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Item Type")
                cboItemType.Focus()
                Return False
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Customer")
                txtVendorNo.Focus()
                Return False
            End If
            'If clsCommon.CompairString("R", cboItemType.SelectedValue) = CompairStringResult.Equal AndAlso Not (clsLocation.isLocatinExcisable(txtLocation.Value)) Then
            '    common.clsCommon.MyMessageBoxShow("Location should be Excisable for Raw Material")
            '    txtLocation.Focus()
            '    Return False
            'End If
            Dim arrICode As New List(Of String)()
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)

                Dim objCustItem As clsCustomeritemDetails = clsCustomeritemDetails.GetItemRateAndDiscount(txtVendorNo.Value, strICode, clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value), txtDate.Value)
                If objCustItem IsNot Nothing Then
                    If objCustItem.Min_Rate > clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value) Then
                        common.clsCommon.MyMessageBoxShow("Minimum Rate Can't be Less Then " + clsCommon.myCstr(objCustItem.Min_Rate) + " of Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)))
                        Return False
                    End If
                End If

                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If (ii = jj) Then
                        Continue For
                    End If
                    If (clsCommon.CompairString(strICode, clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)) = CompairStringResult.Equal) And clsCommon.myLen(strICode) > 0 Then
                        common.clsCommon.MyMessageBoxShow("Already selected Item " + strICode.Trim() + "( " + strIName.Trim() + " ) At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and  " + clsCommon.myCstr(clsCommon.myCdbl(jj + 1)) + "")
                        Return False
                    End If
                Next

                If Not arrICode.Contains(strICode) Then
                    arrICode.Add(strICode)
                End If
            Next
            clsItemMaster.isItemOfSameType(clsCommon.myCstr(cboItemType.SelectedValue), cboItemType.Text, arrICode)
            UcCustomFields1.AllowToSave()
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                Dim obj As New clsSalesQuotationsHead()
                obj.Document_Code = txtReqNo.Value

                obj.Customer_Code = txtVendorNo.Value
                obj.Customer_Name = lblVendorName.Text
                obj.Salesman_Code = txtSalesman.Value
                obj.Salesman_Name = lblSalesman.Text

                obj.Document_Date = txtDate.Value
                obj.Cust_OrderNo = txtCustOrderNo.Text
                If txtExpireDate.Checked Then
                    obj.Expire_Date = txtExpireDate.Value
                End If
                If txtRequiredDate.Checked Then
                    obj.Require_Date = txtRequiredDate.Value
                End If
                obj.Ref_No = txtRefNo.Text
                obj.Description = txtDesc.Text
                obj.Remarks = txtRmks.Text
                obj.On_Hold = IIf(chkOnHold.Checked, 1, 0)
                obj.Location = txtLocation.Value
                obj.Detail_Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)
                obj.Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)
                obj.Mode_Of_Transport = cboModeOfTransport.Text
                obj.Comments = txtComment.Text

                obj.Item_Type = clsCommon.myCstr(cboItemType.SelectedValue)

                obj.Request_By = txtRequestBy.Text
                obj.PROJECT_ID = fndProject.Value

                obj.ArrTr = New List(Of clsSalesQuotationsDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsSalesQuotationsDetail()
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colIName).Value)

                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Location = txtLocation.Value 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    objTr.Vendor_ItemNo = clsCommon.myCstr(grow.Cells(colVendorItemNo).Value)
                    objTr.Order_No = clsCommon.myCstr(grow.Cells(colOrderNo).Value)
                    objTr.Status = "N"

                    objTr.Specification = clsCommon.myCstr(grow.Cells(colSpecification).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    objTr.Discount_Per = clsCommon.myCdbl(grow.Cells(colDiscountPer).Value)
                    'objTr.Order_No = clsCommon.myCdbl(grow.Cells(colorderno).Value)
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.ArrTr.Add(objTr)
                    End If
                Next


                If (obj.ArrTr Is Nothing OrElse obj.ArrTr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
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

                '' CurrencConversion
                If txtCurrencyCode.Visible = True Then
                    obj.CURRENCY_CODE = Me.txtCurrencyCode.Value
                Else
                    obj.CURRENCY_CODE = Nothing
                End If
                If txtConversionRate.Visible = True Then
                    obj.ConvRate = clsCommon.myCdbl(Me.txtConversionRate.Text)
                Else
                    obj.ConvRate = 1
                End If
                If txtApplicableFrom.Visible = True Then
                    If clsCommon.myLen(txtApplicableFrom.Text) > 0 Then
                        obj.ApplicableFrom = Me.txtApplicableFrom.Text
                    Else
                        obj.ApplicableFrom = Nothing
                    End If

                Else
                    obj.ApplicableFrom = Nothing
                End If

                '' end CurrencyConversion

                If chkqclose.Checked = True Then
                    closeyn = "Y"
                ElseIf chkqclose.Checked = False Then
                    closeyn = "N"
                End If
                obj.close_yn = closeyn

                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_Code)
                    If ChekBtnPost = False Then
                        common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    End If
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub LoadData(ByVal strDocumentNo As String, ByVal navType As common.NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            UsLock1.Status = ERPTransactionStatus.Pending
            cboItemType.Enabled = False
            txtLocation.Enabled = False
            BlankAllControls()
            LoadBlankGrid()
            Dim obj As New clsSalesQuotationsHead()
            obj = clsSalesQuotationsHead.GetData(strDocumentNo, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then

                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False

                    repoComplete.IsVisible = True
                    repoBalQty.IsVisible = True

                End If
                txtReqNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date

                txtVendorNo.Value = obj.Customer_Code
                chkRateUserCustomer.ToggleState = ClsUserCustomerSettings.GetUserCustomerRateSetting(txtVendorNo.Value)
                lblVendorName.Text = obj.Customer_Name
                txtSalesman.Value = obj.Salesman_Code
                lblSalesman.Text = obj.Salesman_Name

                txtCustOrderNo.Text = obj.Cust_OrderNo
                UsLock1.Status = obj.Status
                txtExpireDate.Checked = IIf(obj.Expire_Date Is Nothing, False, True)
                If txtExpireDate.Checked Then
                    txtExpireDate.Value = obj.Expire_Date
                End If
                txtRequiredDate.Checked = IIf(obj.Require_Date Is Nothing, False, True)
                If txtRequiredDate.Checked Then
                    txtRequiredDate.Value = obj.Require_Date
                End If
                txtRefNo.Text = obj.Ref_No
                txtDesc.Text = obj.Description
                txtRmks.Text = obj.Remarks
                chkOnHold.Checked = IIf(obj.On_Hold, 1, 0)
                txtLocation.Value = obj.Location
                lblLocation.Text = obj.LocationName
                lblTotRAmt.Text = clsCommon.myFormat(obj.Detail_Total_Amt)
                cboModeOfTransport.Text = obj.Mode_Of_Transport
                txtComment.Text = obj.Comments
                cboItemType.SelectedValue = obj.Item_Type
                txtRequestBy.Text = obj.Request_By
                fndProject.Value = obj.PROJECT_ID

                lblProject.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" + fndProject.Value + "'")


                For Each objTr As clsSalesQuotationsDetail In obj.ArrTr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colComplete).Value = objTr.Status
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceQty).Value = objTr.Balance_Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objTr.Location
                    ''gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objTr.LocationName
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Cost
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmt).Value = objTr.Item_Net_Amt

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiscountPer).Value = objTr.Discount_Per
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDiscAmt).Value = objTr.Discount_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDisc).Value = objTr.Amount_After_Discount

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorItemNo).Value = objTr.Vendor_ItemNo
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colOrderNo).Value = objTr.Order_No

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSpecification).Value = objTr.Specification
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                Next
                If obj.Status = ERPTransactionStatus.Pending Then
                    gv1.Rows.AddNew()
                End If

                ''For Custom Fields
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.LoadData(obj.Document_Code)
                End If
                clsCustomFieldGrid.FillDataInGrid(obj.Document_Code, MyBase.Form_ID, gv1)
                ''End of For Custom Fields

                '' currencyconversion
                Me.txtCurrencyCode.Value = obj.CURRENCY_CODE
                Me.txtConversionRate.Text = obj.ConvRate
                Me.txtApplicableFrom.Text = obj.ApplicableFrom.ToString
                '' end  currencyconversion
                UcAttachment1.LoadData(obj.Document_Code)
            End If

            If obj.close_yn = "Y" Then
                chkqclose.Checked = True
            ElseIf obj.close_yn = "N" Then
                chkqclose.Checked = False
                vaddnew = "N"
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                '-----------------------------------------------
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Level1_Approval_Status, Level2_Approval_Status, Level3_Approval_Status from TSPL_SD_QUOTATION_HEAD WHERE Document_Code ='" + txtReqNo.Value + "'")
                If dt.Rows.Count > 0 Then
                    If clsCommon.myCdbl(dt.Rows(0)("Level1_Approval_Status")) = 0 And clsCommon.myCdbl(dt.Rows(0)("Level2_Approval_Status")) = 0 And clsCommon.myCdbl(dt.Rows(0)("Level3_Approval_Status")) = 0 Then
                        SaveData(True)
                    End If
                End If
                '-----------------------------------------------
                If (clsSalesQuotationsHead.PostData(txtReqNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Posted Successfully ")
                    LoadData(txtReqNo.Value, NavigatorType.Current)
                    If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                        funPrint()
                    End If

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
                If (clsSalesQuotationsHead.DeleteData(txtReqNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtReqNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function



    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        btnSave.Visible = True
    '        btnDelete.Visible = True
    '        btnPost.Visible = True

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "PO-REQ"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Visible = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Visible = False
    '        End If
    '        If strTemp(3) = "0" Then 'Grant Authorize access
    '            btnPost.Visible = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtReqNo._MYNavigator
        Try
            vaddnew = "Y"
            Dim qst As String = "select count(*) from TSPL_SD_QUOTATION_HEAD where Document_Code='" + txtReqNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtReqNo.MyReadOnly = False
            Else
                txtReqNo.MyReadOnly = True
            End If
            LoadData(txtReqNo.Value, NavType)
        Catch ex As Exception
            vaddnew = "N"
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        vaddnew = "Y"
        Dim UserLevel As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ApprovalLevel from TSPL_USER_MASTER WHERE User_Code='" + objCommonVar.CurrentUserCode + "'"))
        Dim qry As String = "select Document_Code as Code, Document_Date as Date, Description, case when Status='0' then 'Pending' else 'Approved' end as [Status] from TSPL_SD_QUOTATION_HEAD "
        Dim whrClas As String = " 1=1 "
        If UserLevel = 2 Then
            whrClas += " AND Level1_Approval_Status=1"
        ElseIf UserLevel = 3 Then
            whrClas += " AND Level2_Approval_Status=1"
        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += "  AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("SQReqfndNo", qry, "Code", whrClas, txtReqNo.Value, "Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            ''Dim qry As String = "select  Location_Code as Code,Location_Desc as Name  from TSPL_LOCATION_MASTER"
            ''Dim whrcls As String = "" ''"Loc_Segment_Code in (" + clsCommon.GetMulcallString(clsERPFuncationality.UserAvailableLocationData()) + ")"
            ''txtLocation.Value = clsCommon.ShowSelectForm("PRLocation", qry, "Code", whrcls, txtLocation.Value, "Code", isButtonClicked)
            ''lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            'Dim obj As clsLocation = clsLocation.FinderForPhysicalLoaction(txtLocation.Value, isButtonClicked)
            'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
            '    txtLocation.Value = obj.Code
            '    lblLocation.Text = obj.Name
            'Else
            '    txtLocation.Value = ""
            '    lblLocation.Text = ""
            'End If


            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical'  "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            txtLocation.Value = clsCommon.ShowSelectForm("CustLocFND", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))



        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmPurchaseRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colIName)
                OpenICodeList(True)
                gv1.CurrentColumn = gv1.Columns(colICode)

            ElseIf gv1.CurrentColumn Is gv1.Columns(colOrderNo) Then
                gv1.CurrentColumn = gv1.Columns(colSpecification)
                OpenOrderList(True)
                gv1.CurrentColumn = gv1.Columns(colOrderNo)
            End If
            setGridFocus()
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso MyBase.isModifyFlag Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.T Then
            chkRateDefaultSetting.Visible = Not chkRateDefaultSetting.Visible
            chkRateUserCustomer.Visible = Not chkRateUserCustomer.Visible
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtReqNo.Value = "" Then
            myMessages.blankValue("Quotation Number")
        Else
            funPrint()
        End If

    End Sub

    Private Sub funPrint()

        Try
            attachqry = GetAttachQry()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(attachqry)

            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.NewSalesReports, dt, "crptSalesQuotation", "Sales Quoation")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Function GetAttachQry()
        Dim qry As String = " select '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy") + "' as RunDate, TSPL_SD_QUOTATION_HEAD.Salesman_Name ,"
        qry += " TSPL_SD_QUOTATION_HEAD.Document_Code ,Convert(Varchar,TSPL_SD_QUOTATION_HEAD.Document_Date,103) as Document_Date  ,TSPL_SD_QUOTATION_HEAD.Expire_Date , "
        qry += " TSPL_SD_QUOTATION_HEAD.Require_Date ,TSPL_SD_QUOTATION_HEAD.Ref_No ,TSPL_SD_QUOTATION_HEAD.Description,TSPL_SD_QUOTATION_HEAD.Remarks,"
        qry += " TSPL_SD_QUOTATION_HEAD.Request_By ,TSPL_SD_QUOTATION_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc ,TSPL_SD_QUOTATION_DETAIL.Unit_Code ,"
        qry += " TSPL_SD_QUOTATION_DETAIL.Qty ,"
        qry += " (select SUM(Item_Qty)from TSPL_ITEM_LOCATION_DETAILS where Item_Code=TSPL_SD_QUOTATION_DETAIL.Item_Code)as AvaiQty  ,"
        qry += " TSPL_CUSTOMER_MASTER.Cust_Code , TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as Customer_Add1,TSPL_CUSTOMER_MASTER.Add2  as Customer_Add2,TSPL_CUSTOMER_MASTER.Add3 as Customer_Add3,TSPL_CUSTOMER_MASTER.City_Code, TSPL_CITY_MASTER.City_Name,TSPL_CITY_MASTER .STATE_CODE ,PIN_Code , TSPL_SD_QUOTATION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,"
        qry += " TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2,user1.User_Name as CreatedBy,user2.User_Name as AuthorizeBy ,"
        qry += " TSPL_SD_QUOTATION_HEAD.Request_By,TSPL_SD_QUOTATION_HEAD.Require_Date,TSPL_SD_QUOTATION_HEAD.Dept_Desc,TSPL_SD_QUOTATION_HEAD.Location ,"
        qry += " TSPL_COMPANY_MASTER.Add1 + Case When ISNULL(TSPL_COMPANY_MASTER.Add2,'')<>'' Then ', '+TSPL_COMPANY_MASTER.Add2 Else '' + Case When TSPL_COMPANY_MASTER.Add3<>'' Then ', '+TSPL_COMPANY_MASTER.Add3 Else ''end end as [Address], "
        qry += " ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+ Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as Phone, ISNULL(TSPL_COMPANY_MASTER.Fax,'' ) as Fax  from TSPL_SD_QUOTATION_HEAD "
        qry += " Left Outer join TSPL_SD_QUOTATION_DETAIL on TSPL_SD_QUOTATION_HEAD.Document_Code =TSPL_SD_QUOTATION_DETAIL.Document_Code "
        qry += " left outer join TSPL_COMPANY_MASTER on  TSPL_SD_QUOTATION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code "
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_QUOTATION_HEAD.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code"
        qry += " LEFT OUTER JOIN TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code  "
        qry += " Left Outer Join TSPL_ITEM_MASTER on  TSPL_SD_QUOTATION_DETAIL.Item_Code= TSPL_ITEM_MASTER.Item_Code "
        qry += " left outer join TSPL_USER_MASTER as user1 on TSPL_SD_QUOTATION_HEAD.Created_By=user1.User_Code "
        qry += " left outer join TSPL_USER_MASTER as user2 on TSPL_SD_QUOTATION_HEAD.Modify_By=user2.User_Code   where (2 = 2)"

        If txtReqNo.Value <> "" Then
            qry += "  and  TSPL_SD_QUOTATION_HEAD.Document_Code = '" + txtReqNo.Value + "'"
        End If

        Return qry


    End Function


    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            ''If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentCell.ColumnInfo.Name), colComplete) = CompairStringResult.Equal AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
            If gv1.Columns(colIName) Is gv1.CurrentColumn AndAlso UsLock1.Status = ERPTransactionStatus.Approved Then
                Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                Dim intSNo As Integer = Convert.ToInt32((clsCommon.myCdbl(gv1.CurrentRow.Cells(colLineNo).Value)))
                Dim strStatus As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colComplete).Value)
                If clsCommon.myLen(txtReqNo.Value) > 0 AndAlso clsCommon.myLen(strICode) > 0 AndAlso intSNo > 0 AndAlso clsCommon.CompairString(strStatus, "N") = CompairStringResult.Equal Then
                    If common.clsCommon.MyMessageBoxShow("Do you want to complete the item " + clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value), Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        If clsSalesQuotationsDetail.CompleteRequition(txtReqNo.Value, strICode, intSNo) Then
                            common.clsCommon.MyMessageBoxShow("Successfully Completed")
                            LoadData(txtReqNo.Value, NavigatorType.Current)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
            If e.Column Is gv1.Columns(colRate) Then
                If chkRateUserCustomer.ToggleState = ToggleState.Indeterminate Then
                    gv1.CurrentRow.Cells(colRate).ReadOnly = Not chkRateDefaultSetting.Checked
                Else
                    gv1.CurrentRow.Cells(colRate).ReadOnly = Not chkRateUserCustomer.Checked
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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


    Private Sub RadPageViewPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub

    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name,TSPL_CUSTOMER_MASTER.add1 +case when len(TSPL_CUSTOMER_MASTER.add2)>0 then ', '+TSPL_CUSTOMER_MASTER.add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER.City_Name)>0 then ', '+TSPL_CITY_MASTER.City_Name else ' ' end + case when len(TSPL_CUSTOMER_MASTER.State )>0 then TSPL_CUSTOMER_MASTER.State else '' end  as Address ,Salesman_Code as [Salesman Code],Salesman_Desc as Salesman "
        qry += " from TSPL_CUSTOMER_MASTER "
        qry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code"
        qry += " left outer join TSPL_TDS_STATE_MASTER on TSPL_TDS_STATE_MASTER.State_Code=TSPL_CUSTOMER_MASTER.State"
        txtVendorNo.Value = clsCommon.ShowSelectForm("SNSOClientFndr", qry, "Code", "", txtVendorNo.Value, "Code", isButtonClicked)
        qry += " where 2=2 and TSPL_CUSTOMER_MASTER.Cust_Code ='" + txtVendorNo.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
            txtSalesman.Value = clsCommon.myCstr(dt.Rows(0)("Salesman Code"))
            lblSalesman.Text = clsCommon.myCstr(dt.Rows(0)("Salesman"))
            txtDate.Enabled = False
            txtVendorNo.Enabled = False
            chkRateUserCustomer.ToggleState = ClsUserCustomerSettings.GetUserCustomerRateSetting(txtVendorNo.Value)
            SetMultiCurrencyVisibility()
        Else
            lblVendorName.Text = ""
            txtSalesman.Value = ""
            lblSalesman.Text = ""
            Me.txtCurrencyCode.Value = ""
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
        End If
    End Sub

    
    Private Sub txtSalesman__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesman._MYValidating
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
        Dim whrcls As String = "Emp_type='Salesman'"
        txtSalesman.Value = clsCommon.ShowSelectForm("SNOSaleman", qry, "Code", whrcls, txtSalesman.Value, "Code", isButtonClicked)
        lblSalesman.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name as Name from TSPL_EMPLOYEE_MASTER where EMP_CODE ='" + txtSalesman.Value + "' and Emp_type='Salesman'"))

    End Sub

    Private Sub fndBaseCurrency__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        ShowCurrencyDetail()
    End Sub

    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        UcItemBalance1.ItemMRP = -1 ''clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
        UcItemBalance1.LocationCode = txtLocation.Value
        UcItemBalance1.LocationName = lblLocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtReqNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowSOQty = True
        UcItemBalance1.RefreshData()
    End Sub

    Private Sub gv1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
        End If
    End Sub
    Sub SQCLOSEDATA()
        Try
            If (clsSalesQuotationsHead.SQDATACLOSE(txtReqNo.Value, closeyn)) Then
                If closeyn = "Y" Then
                    common.clsCommon.MyMessageBoxShow("Data Closed Successfully ")
                ElseIf closeyn = "N" Then
                    common.clsCommon.MyMessageBoxShow("Data Opened Successfully ")
                End If
                LoadData(txtReqNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
   
    Private Sub chkqclose_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkqclose.ToggleStateChanged
        If chkqclose.Checked = True And vaddnew = "N" Then
            Dim response = MsgBox("Are you sure want to close the Sale Quotation?", MsgBoxStyle.YesNo, "")
            If response = MsgBoxResult.Yes Then
                closeyn = "Y"
                SQCLOSEDATA()
            Else
                closeyn = "N"
                chkqclose.Checked = False
            End If
        ElseIf chkqclose.Checked = False And vaddnew = "N" Then
            closeyn = "N"
            SQCLOSEDATA()
        End If
        vaddnew = "N"
    End Sub

 
    Public Sub SetMailRight()
        Dim obj As clsCheckMailSetting = clsCheckMailSetting.CheckMailRight()
        If obj.IsSendMail = "YES" Then
            btnsetting.Enabled = True
        Else
            btnsetting.Enabled = False
        End If

    End Sub
   
    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Try
            If clsCommon.myLen(txtReqNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtReqNo.Focus()
                txtReqNo.Select()
                Return
            End If

            attachqry = GetAttachQry()
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachqry)

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                System.Diagnostics.Process.Start(frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptSalesQuotation", "Sales Quotation"))
                frmCRV = Nothing
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)

        End Try
        
    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try
            If clsCommon.myLen(txtReqNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtReqNo.Focus()
                txtReqNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Sale Order No. " + txtReqNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtReqNo.Value, NavigatorType.Current)
            'SendSMSandEmail()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdbEmailSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbEmailSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmSaleQuotation
        frm.ShowDialog()
    End Sub

    'Private Sub SendSMSandEmail()
    '    Dim strEmail, strphone, strMes As String
    '    Dim strCustomer, strContactperson As String
    '    '  Dim decAmt As Decimal


    '    Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmSaleQuotation)

    '    If obj Is Nothing Then
    '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '        Return
    '    End If
    '    If clsCommon.myLen(obj.mailsubjct) <= 0 Then
    '        clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
    '        Return
    '    End If

    '    Try


    '        If Process.GetProcessesByName("OutLook").Length < 1 Then
    '            'restarts the Process
    '            Process.Start("OutLook.exe")
    '        End If
    '        Dim oApp As New Outlook.Application()
    '        Dim oMsg As Outlook.MailItem

    '        strContactperson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")
    '        strCustomer = lblVendorName.Text





    '        'oMsg.Body = "Dear " & strContactperson & " (" & strCustomer & ")" & Environment.NewLine & "   your order No " & txtDocNo.Value & "  dated  " & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "  has been booked with amount  " & lblTotRAmt.Text
    '        'oMsg.Subject = "Your order no  " & txtDocNo.Value & "  has been booked"

    '        oMsg = DirectCast(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
    '        strEmail = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")

    '        oMsg.Body = obj.mailbody

    '        oMsg.Body = oMsg.Body.Replace("'", " ").Replace("`", "/")

    '        If oMsg.Body.Contains(clsEmailSMSConstants.SaleOrderNo) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.SaleOrderNo, txtReqNo.Value)
    '        End If
    '        If oMsg.Body.Contains(clsEmailSMSConstants.SaleOrderDate) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
    '        End If
    '        If oMsg.Body.Contains(clsEmailSMSConstants.VendorNo) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
    '        End If
    '        If oMsg.Body.Contains(clsEmailSMSConstants.VendorName) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
    '        End If
    '        If oMsg.Body.Contains(clsEmailSMSConstants.ContactPerson) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.ContactPerson, strContactperson)
    '        End If
    '        If oMsg.Body.Contains(clsEmailSMSConstants.TotalAmount) Then
    '            oMsg.Body = oMsg.Body.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
    '        End If



    '        oMsg.Subject = obj.mailsubjct

    '        oMsg.Subject = oMsg.Subject.Replace("'", " ").Replace("`", "/")

    '        If oMsg.Subject.Contains(clsEmailSMSConstants.SaleOrderNo) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.SaleOrderNo, txtReqNo.Value)
    '        End If
    '        If oMsg.Subject.Contains(clsEmailSMSConstants.SaleOrderDate) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
    '        End If
    '        If oMsg.Subject.Contains(clsEmailSMSConstants.VendorNo) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
    '        End If
    '        If oMsg.Subject.Contains(clsEmailSMSConstants.VendorName) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
    '        End If
    '        If oMsg.Subject.Contains(clsEmailSMSConstants.ContactPerson) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.ContactPerson, strContactperson)
    '        End If
    '        If oMsg.Subject.Contains(clsEmailSMSConstants.TotalAmount) Then
    '            oMsg.Subject = oMsg.Subject.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
    '        End If

    '        '------------------------code for attchament-------------------------------------
    '        If obj.atchmnt = "Y" Then
    '            Dim sDisplayname As [String] = "MyAttachment"
    '            If oMsg.Body Is Nothing Then
    '                oMsg.Body = " "
    '            End If
    '            Dim iPosition As Integer = CInt(oMsg.Body.Length) + 1
    '            Dim iAtchmentType As Integer = CInt(Outlook.OlAttachmentType.olByValue)

    '            attachqry = GetAttachQry()
    '            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(attachqry)
    '            Dim strRptPath As String = ""

    '            Dim oAttachment As Outlook.Attachment = Nothing
    '            If dt1.Rows.Count > 0 Then
    '                'SetItemWiseTax(dt1, txtDocNo.Value)
    '                Dim frmCRV As New frmCrystalReportViewer()
    '                strRptPath = frmCRV.EmailAttachment(CrystalReportFolder.NewSalesReports, dt1, "crptSalesOrderReport", "Sales Order")
    '                frmCRV = Nothing
    '                oAttachment = oMsg.Attachments.Add(strRptPath, iAtchmentType, iPosition, sDisplayname)
    '            End If
    '        End If
    '        '---------------------------------------------------------------------------


    '        oMsg.Recipients.Add(strEmail)
    '        oMsg.CC = "ranjana.sinha@tecxpert.in;rakesh.sharma@tecxpert.in"
    '        oMsg.Send()
    '        oMsg = Nothing
    '        oApp = Nothing

    '        clsCommon.MyMessageBoxShow("E-Mail Send Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try

    '    Try
    '        Dim client As New System.Net.WebClient()

    '        If clsCommon.myLen(obj.smsbody) <= 0 Then
    '            Return
    '        End If

    '        'strMes = "Dear  " & strCustomer & "  your order No " & txtDocNo.Value & "  dated  " & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "has been booked with amount" & lblTotRAmt.Text

    '        strMes = obj.smsbody
    '        strMes = strMes.Replace("'", " ").Replace("`", "/")

    '        If strMes.Contains(clsEmailSMSConstants.SaleOrderNo) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderNo, txtReqNo.Value)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.SaleOrderDate) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.SaleOrderDate, clsCommon.GetPrintDate(txtDate.Text, "dd/MMM/yyyy"))
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.VendorNo) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.VendorNo, txtVendorNo.Value)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.VendorName) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.VendorName, lblVendorName.Text)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.ContactPerson) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.ContactPerson, strContactperson)
    '        End If
    '        If strMes.Contains(clsEmailSMSConstants.TotalAmount) Then
    '            strMes = strMes.Replace(clsEmailSMSConstants.TotalAmount, lblTotRAmt.Text)
    '        End If


    '        strphone = clsDBFuncationality.getSingleValue("select Phone1 from TSPL_customer_MASTER where cust_code ='" & txtVendorNo.Value & "' ")

    '        Dim baseurl As String = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=tecxpert&password=1818948263&sendername=vipin&mobileno=91" + strphone + "&message=" + strMes
    '        Dim data As Stream = client.OpenRead(baseurl)
    '        Dim reader As StreamReader = New StreamReader(data)
    '        Dim s As String = reader.ReadToEnd()
    '        data.Close()
    '        reader.Close()

    '        clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub
End Class
