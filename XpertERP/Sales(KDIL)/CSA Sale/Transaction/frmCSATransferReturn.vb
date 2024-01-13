''Created By Monika 24/03/2015
Imports common
Imports System.Data.SqlClient

Public Class FrmCSATransferReturn
    Inherits FrmMainTranScreen

#Region "variables"
    Dim AllowGateReturn As Integer = 0
    Dim showReturnType As Boolean = False
    Dim TransferManual_KnockOFF As Boolean = False
    Dim arrLoc As String = Nothing
    Public strDocumentNo As String = Nothing
    Dim ButtonToolTip As New ToolTip()
    Dim Errorcontrol As New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Dim isCellValueChnaged As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim Rate_Changed_On As Boolean = False

    Dim ShowDocumentCancel As Boolean = False

    Const colItemLineno As String = "ItemLineno"
    Const colItemItemCode As String = "ItemItemCode"
    Const colItemItemName As String = "ItemItemName"
    Const colItemItemCSAType As String = "ItemCSAType"
    Const colItemUnit As String = "ItemUnit"
    Const colItemQty As String = "ItemQty"
    Const colItemRate As String = "ItemRate"
    Const colItemAmount As String = "ItemAmount"
    Const colItemremarks As String = "Itemremarks"
    Const colItemFOC As String = "itemFOC"
    Const colItemPickTransferDetail As String = "PickTransferDetail"
    Const colIsBatchItem As String = "colIsBatchItem"

    Const colLineno As String = "Lineno"
    Const colItemCode As String = "ItemCode"
    Const colItemName As String = "ItemName"
    Const colItemCSAType As String = "CSAType"
    Const colUnit As String = "Unit"
    Const colQty As String = "Qty"
    Const colRate As String = "Rate"
    Const colTransferNo As String = "TransferNo"
    Const colTransferDesc As String = "Transdesc"
    Const colTransOrgUnit As String = "OrgTransUnit"
    Const colTransOrgQty As String = "OrgTransQty"
    Const colTransAltUnit As String = "TransUnit"
    Const colTransAltQty As String = "TransQty"
    Const colAmount As String = "Amount"
    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colIsExcisable1 As String = "ISEXCISABLE1"
    Const colIsExcisable2 As String = "ISEXCISABLE2"
    Const colIsExcisable3 As String = "ISEXCISABLE3"
    Const colIsExcisable4 As String = "ISEXCISABLE4"
    Const colIsExcisable5 As String = "ISEXCISABLE5"
    Const colIsExcisable6 As String = "ISEXCISABLE6"
    Const colIsExcisable7 As String = "ISEXCISABLE7"
    Const colIsExcisable8 As String = "ISEXCISABLE8"
    Const colIsExcisable9 As String = "ISEXCISABLE9"
    Const colIsExcisable10 As String = "ISEXCISABLE10"
    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"


    Const colremarks As String = "remarks"
    Const colFOC As String = "FOC"
    Const colAdjustmentNo As String = "Adjustment No"
    Const colSalePattiReturnNo As String = "colSalePattiReturnNo"
    Const colBalanceQty As String = "BalanceQty"
    Dim OpenALLTaxes As Boolean = False

    



    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"
#End Region

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim check As Integer = 0
                check = clsDBFuncationality.getSingleValue("select count(*) from tspl_location_master where type='Plant' and location_code='" + obj.Default_LocCode + "'")
                If check > 0 Then
                    txt_loc_code.Value = obj.Default_LocCode
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

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCSATransferReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FunReset()
        fndGateEntryNo.Value = ""
        fndGateEntryNo.Enabled = True
        fndGateReturnNo.Value = ""
        btnRev_Unpost.Visible = False
        txtCode.Value = ""
        dtpdate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtDesc.Text = ""
        txtDesc.Text = clsCSATransferReturnHead.GetDescription(Nothing)
        txtcustcode.Value = ""
        txtcustName.Text = ""
        txtCSA_Loc_Code.Value = ""
        txt_loc_code.Value = ""
        txt_loc_name.Text = ""
        lblTotRAmt1.Text = ""
        lblNetAmt.Text = ""
        lblTaxAmt.Text = ""
        cmbType.SelectedValue = ""

        gv_Item.Rows.Clear()
        gv_Item.Rows.AddNew()
        gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemPickTransferDetail).Value = "Double Click"

        gv.Rows.Clear()

        txtcustcode.Enabled = True
        txt_loc_code.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCode.MyReadOnly = False

        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.BlankAllControls()

        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        isInsideLoadData = False
        isNewEntry = True

        RadPageView1.SelectedPage = RadPageViewPage1
        txtDesc.Focus()
        txtDesc.Select()
        LoadBlankGridTax()
    End Sub

    Private Sub FrmCSATransferReturn_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N Then
                FunReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                clsERPFuncationality.closeForm(Me)
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
                btnSave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
                btnDelete.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                btnPost.PerformClick()
            End If

            If e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 AndAlso MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnRev_Unpost.Visible = True
                End If
            End If


            If e.KeyCode = Keys.F2 AndAlso gv_Item.CurrentColumn IsNot Nothing AndAlso gv_Item.CurrentColumn Is gv_Item.Columns(colItemItemCode) Then
                isCellValueChnaged = True
                OpenItem(True)

                ''if transfer knock-off is manually then below function is not run.
                If Not TransferManual_KnockOFF Then
                    FillTransferDetail(gv_Item.CurrentRow.Index, False, False)
                ElseIf TransferManual_KnockOFF Then
                    RemoveTransferDetail(clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCode).Value), clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemUnit).Value))
                End If
                ''============================
                isCellValueChnaged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv_Item.CurrentColumn IsNot Nothing AndAlso gv_Item.CurrentColumn Is gv_Item.Columns(colItemUnit) Then
                isCellValueChnaged = True
                OpenUnit(True)

                If Not TransferManual_KnockOFF Then
                    FillTransferDetail(gv_Item.CurrentRow.Index, False, False)
                ElseIf TransferManual_KnockOFF Then
                    RemoveTransferDetail(clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCode).Value), clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemUnit).Value))
                End If
                isCellValueChnaged = False
            End If

        Catch ex As Exception
            isCellValueChnaged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadTypeCombobox()
        cmbType.DataSource = Nothing
        Dim qry As String = "select '' as Code,'Select' as Name union all select 'I' as Code,'Return Goods' as Name union all select 'D' as Code,'Damage Good' as Name union all select 'S' as Code,'Shortage Goods' as Name"
        If ShowDocumentCancel = True Then
            qry += " union all select 'C' as Code,'Cancel Type' as Name "
        End If

        cmbType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cmbType.ValueMember = "Code"
        cmbType.DisplayMember = "Name"
    End Sub

    Private Sub FrmCSATransferReturn_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        AllowGateReturn = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowGateReturn, clsFixedParameterCode.AllowGateReturn, Nothing)) = 1, 1, 0)

        Rate_Changed_On = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ChangeRateAT_CSA_Return, clsFixedParameterCode.ChangeRateAT_CSA_Return, Nothing)) = "1", True, False))
        TransferManual_KnockOFF = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PickManual_CSATransfer_OnTRansferReturn, clsFixedParameterCode.PickManual_CSATransfer_OnTRansferReturn, Nothing)) = "1", True, False))

        showReturnType = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowCSAReturnTypeOnScreen, clsFixedParameterCode.ShowCSAReturnTypeOnScreen, Nothing)) = "1", True, False))
        ShowDocumentCancel = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CSADocumentCancel, clsFixedParameterCode.CSADocumentCancel, Nothing)) = 1, True, False)
        Panel1.Visible = showReturnType

        If AllowGateReturn = 0 Then
            pnlGateReturn.Visible = False
        Else
            pnlGateReturn.Visible = True
        End If

        LoadBlankGrid()
        LoadTransBlankGrid()
        FunReset()

        LoadTypeCombobox()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for post record.")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for refresh window.")
        OpenALLTaxes = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CSATransfer_SalePatti_All_Tax_Open, clsFixedParameterCode.CSATransfer_SalePatti_All_Tax_Open, Nothing)) = "1", True, False))
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub LoadTransBlankGrid()
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
        repoLineNo.PinPosition = PinnedColumnPosition.Left
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repochk = New GridViewCheckBoxColumn()
        repochk.FormatString = ""
        repochk.HeaderText = "FOC Item"
        repochk.Name = colFOC
        repochk.Width = 50
        repochk.WrapText = True
        repochk.ReadOnly = True
        repochk.PinPosition = PinnedColumnPosition.Left
        gv.MasterTemplate.Columns.Add(repochk)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Code"
        repoLineNo.Name = colItemCode
        repoLineNo.Width = 110
        repoLineNo.ReadOnly = True
        repoLineNo.PinPosition = PinnedColumnPosition.Left
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Name"
        repoLineNo.Name = colItemName
        repoLineNo.Width = 200
        repoLineNo.ReadOnly = True
        repoLineNo.PinPosition = PinnedColumnPosition.Left
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "CSA Type"
        repoLineNo.Name = colItemCSAType
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.PinPosition = PinnedColumnPosition.Left
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Unit"
        repoLineNo.Name = colUnit
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.WrapText = True
        repoQty.DecimalPlaces = 2

        If TransferManual_KnockOFF Then
            repoQty.ReadOnly = False
        Else
            repoQty.ReadOnly = True
        End If
        gv.MasterTemplate.Columns.Add(repoQty)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Unit Rate"
        repoQty.Name = colRate
        repoQty.Width = 80
        repoQty.DecimalPlaces = 2
        repoQty.ReadOnly = True
        repoQty.WrapText = True
        gv.MasterTemplate.Columns.Add(repoQty)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Transfer No"
        repoLineNo.Name = colTransferNo
        repoLineNo.Width = 110
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Adjustment No"
        repoLineNo.Name = colAdjustmentNo
        repoLineNo.Width = 110
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SalePatti Return No"
        repoLineNo.Name = colSalePattiReturnNo
        repoLineNo.Width = 110
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Description"
        repoLineNo.Name = colTransferDesc
        repoLineNo.Width = 150
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Transfer/Adj. Unit"
        repoLineNo.Name = colTransOrgUnit
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Transfer/Adj. Qty"
        repoLineNo.Name = colTransOrgQty
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Transfer/Adj. Alt Unit"
        repoLineNo.Name = colTransAltUnit
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Transfer/Adj. Alt Qty"
        repoLineNo.Name = colTransAltQty
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Amount"
        repoQty.Name = colAmount
        repoQty.Width = 80
        repoQty.DecimalPlaces = 2
        repoQty.ReadOnly = True
        repoQty.WrapText = True
        gv.MasterTemplate.Columns.Add(repoQty)


        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax1)

        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = colTaxBaseAmt1
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt1)

        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt1)

        Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax1)

        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode1)

        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable1)

        Dim repoIsExcisable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable1.HeaderText = "Is Excisable 1"
        repoIsExcisable1.Name = colIsExcisable1
        repoIsExcisable1.ReadOnly = True
        repoIsExcisable1.IsVisible = False
        repoIsExcisable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable1)

        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax2)

        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = colTaxBaseAmt2
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt2)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt2)

        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax2)

        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode2)

        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable2)

        Dim repoIsExcisable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable2.HeaderText = "Is Excisable 2"
        repoIsExcisable2.Name = colIsExcisable2
        repoIsExcisable2.ReadOnly = True
        repoIsExcisable2.IsVisible = False
        repoIsExcisable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable2)

        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = colTax3
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax3)

        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = colTaxBaseAmt3
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt3)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt3)

        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax3)

        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode3)

        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable3)

        Dim repoIsExcisable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable3.HeaderText = "Is Excisable 3"
        repoIsExcisable3.Name = colIsExcisable3
        repoIsExcisable3.ReadOnly = True
        repoIsExcisable3.IsVisible = False
        repoIsExcisable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable3)


        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax4)

        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = colTaxBaseAmt4
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt4)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt4)

        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax4)

        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode4)

        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable4)

        Dim repoIsExcisable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable4.HeaderText = "Is Excisable 4"
        repoIsExcisable4.Name = colIsExcisable4
        repoIsExcisable4.ReadOnly = True
        repoIsExcisable4.IsVisible = False
        repoIsExcisable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable4)

        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = colTax5
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax5)

        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = colTaxBaseAmt5
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt5)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt5)

        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax5)

        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode5)

        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable5)

        Dim repoIsExcisable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable5.HeaderText = "Is Excisable 5"
        repoIsExcisable5.Name = colIsExcisable5
        repoIsExcisable5.ReadOnly = True
        repoIsExcisable5.IsVisible = False
        repoIsExcisable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable5)

        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = colTax6
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax6)

        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = colTaxBaseAmt6
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt6)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colTaxAmt6
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt6)

        Dim repoIsSurTax6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax6.HeaderText = "Is Surtax 6"
        repoIsSurTax6.Name = colIsSurTax6
        repoIsSurTax6.ReadOnly = True
        repoIsSurTax6.IsVisible = False
        repoIsSurTax6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax6)

        Dim repoSurTaxCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode6.FormatString = ""
        repoSurTaxCode6.HeaderText = "Surtax 6"
        repoSurTaxCode6.Name = colSurTaxCode6
        repoSurTaxCode6.ReadOnly = True
        repoSurTaxCode6.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode6)

        Dim repoIsTaxable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable6.HeaderText = "Is Taxable 6"
        repoIsTaxable6.Name = colIsTaxable6
        repoIsTaxable6.ReadOnly = True
        repoIsTaxable6.IsVisible = False
        repoIsTaxable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable6)

        Dim repoIsExcisable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable6.HeaderText = "Is Excisable 6"
        repoIsExcisable6.Name = colIsExcisable6
        repoIsExcisable6.ReadOnly = True
        repoIsExcisable6.IsVisible = False
        repoIsExcisable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable6)

        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = colTax7
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax7)

        Dim repoTaxBaseAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt7.FormatString = ""
        repoTaxBaseAmt7.HeaderText = "Tax Base Amount 7"
        repoTaxBaseAmt7.Name = colTaxBaseAmt7
        repoTaxBaseAmt7.ReadOnly = True
        repoTaxBaseAmt7.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt7)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate7)

        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colTaxAmt7
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt7)

        Dim repoIsSurTax7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax7.HeaderText = "Is Surtax 7"
        repoIsSurTax7.Name = colIsSurTax7
        repoIsSurTax7.ReadOnly = True
        repoIsSurTax7.IsVisible = False
        repoIsSurTax7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax7)

        Dim repoSurTaxCode7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode7.FormatString = ""
        repoSurTaxCode7.HeaderText = "Surtax 7"
        repoSurTaxCode7.Name = colSurTaxCode7
        repoSurTaxCode7.ReadOnly = True
        repoSurTaxCode7.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode7)

        Dim repoIsTaxable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable7.HeaderText = "Is Taxable 7"
        repoIsTaxable7.Name = colIsTaxable7
        repoIsTaxable7.ReadOnly = True
        repoIsTaxable7.IsVisible = False
        repoIsTaxable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable7)

        Dim repoIsExcisable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable7.HeaderText = "Is Excisable 7"
        repoIsExcisable7.Name = colIsExcisable7
        repoIsExcisable7.ReadOnly = True
        repoIsExcisable7.IsVisible = False
        repoIsExcisable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable7)



        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = colTax8
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax8)

        Dim repoTaxBaseAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt8.FormatString = ""
        repoTaxBaseAmt8.HeaderText = "Tax Base Amount 8"
        repoTaxBaseAmt8.Name = colTaxBaseAmt8
        repoTaxBaseAmt8.ReadOnly = True
        repoTaxBaseAmt8.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt8)

        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate8)

        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colTaxAmt8
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt8)

        Dim repoIsSurTax8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax8.HeaderText = "Is Surtax 8"
        repoIsSurTax8.Name = colIsSurTax8
        repoIsSurTax8.ReadOnly = True
        repoIsSurTax8.IsVisible = False
        repoIsSurTax8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax8)

        Dim repoSurTaxCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode8.FormatString = ""
        repoSurTaxCode8.HeaderText = "Surtax 8"
        repoSurTaxCode8.Name = colSurTaxCode8
        repoSurTaxCode8.ReadOnly = True
        repoSurTaxCode8.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode8)

        Dim repoIsTaxable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable8.HeaderText = "Is Taxable 8"
        repoIsTaxable8.Name = colIsTaxable8
        repoIsTaxable8.ReadOnly = True
        repoIsTaxable8.IsVisible = False
        repoIsTaxable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable8)

        Dim repoIsExcisable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable8.HeaderText = "Is Excisable 8"
        repoIsExcisable8.Name = colIsExcisable8
        repoIsExcisable8.ReadOnly = True
        repoIsExcisable8.IsVisible = False
        repoIsExcisable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable8)

        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = colTax9
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax9)

        Dim repoTaxBaseAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt9.FormatString = ""
        repoTaxBaseAmt9.HeaderText = "Tax Base Amount 9"
        repoTaxBaseAmt9.Name = colTaxBaseAmt9
        repoTaxBaseAmt9.ReadOnly = True
        repoTaxBaseAmt9.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt9)

        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colTaxAmt9
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt9)

        Dim repoIsSurTax9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax9.HeaderText = "Is Surtax 9"
        repoIsSurTax9.Name = colIsSurTax9
        repoIsSurTax9.ReadOnly = True
        repoIsSurTax9.IsVisible = False
        repoIsSurTax9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax9)

        Dim repoSurTaxCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode9.FormatString = ""
        repoSurTaxCode9.HeaderText = "Surtax 9"
        repoSurTaxCode9.Name = colSurTaxCode9
        repoSurTaxCode9.ReadOnly = True
        repoSurTaxCode9.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode9)

        Dim repoIsTaxable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable9.HeaderText = "Is Taxable 9"
        repoIsTaxable9.Name = colIsTaxable9
        repoIsTaxable9.ReadOnly = True
        repoIsTaxable9.IsVisible = False
        repoIsTaxable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable9)

        Dim repoIsExcisable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable9.HeaderText = "Is Excisable 9"
        repoIsExcisable9.Name = colIsExcisable9
        repoIsExcisable9.ReadOnly = True
        repoIsExcisable9.IsVisible = False
        repoIsExcisable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable9)

        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = colTax10
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTax10)

        Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt10.FormatString = ""
        repoTaxBaseAmt10.HeaderText = "Tax Base Amount 10"
        repoTaxBaseAmt10.Name = colTaxBaseAmt10
        repoTaxBaseAmt10.ReadOnly = True
        repoTaxBaseAmt10.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoTaxBaseAmt10)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxRate10)

        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colTaxAmt10
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTaxAmt10)

        Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax10.HeaderText = "Is Surtax 10"
        repoIsSurTax10.Name = colIsSurTax10
        repoIsSurTax10.ReadOnly = True
        repoIsSurTax10.IsVisible = False
        repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSurTax10)

        Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode10.FormatString = ""
        repoSurTaxCode10.HeaderText = "Surtax 10"
        repoSurTaxCode10.Name = colSurTaxCode10
        repoSurTaxCode10.ReadOnly = True
        repoSurTaxCode10.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoSurTaxCode10)

        Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable10.HeaderText = "Is Taxable 10"
        repoIsTaxable10.Name = colIsTaxable10
        repoIsTaxable10.ReadOnly = True
        repoIsTaxable10.IsVisible = False
        repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsTaxable10)

        Dim repoIsExcisable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable10.HeaderText = "Is Excisable 10"
        repoIsExcisable10.Name = colIsExcisable10
        repoIsExcisable10.ReadOnly = True
        repoIsExcisable10.IsVisible = False
        repoIsExcisable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsExcisable10)

        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Net Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoAmtAfterTax)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Remarks"
        repoLineNo.Name = colremarks
        repoLineNo.Width = 100
        repoLineNo.MaxLength = 200
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv.MasterTemplate.Columns.Add(repoLineNo)

        ''--------------------below column is user when transfer knock-off manully
        ''---------in case of auto knock-off qty is not editable,but in manual case it s editable,so have to check balance.
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Balance Quantity"
        repoQty.Name = colBalanceQty
        repoQty.Width = 80
        repoQty.DecimalPlaces = 2
        repoQty.ReadOnly = True
        repoQty.VisibleInColumnChooser = False
        repoQty.IsVisible = False
        repoQty.WrapText = True
        gv.MasterTemplate.Columns.Add(repoQty)
        ''-------------end here=======================================

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
        repochk = Nothing
    End Sub

    Private Sub LoadBlankGrid()
        gv_Item.Rows.Clear()
        gv_Item.Columns.Clear()

        Dim repoLineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        Dim repochk As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colItemLineno
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repoLineNo)

        repochk = New GridViewCheckBoxColumn()
        repochk.FormatString = ""
        repochk.HeaderText = "FOC Item"
        repochk.Name = colItemFOC
        repochk.Width = 50
        repochk.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repochk)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Code"
        repoLineNo.Name = colItemItemCode
        repoLineNo.Width = 110
        repoLineNo.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoLineNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLineNo.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Item Name"
        repoLineNo.Name = colItemItemName
        repoLineNo.Width = 300
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "CSA Type"
        repoLineNo.Name = colItemItemCSAType
        repoLineNo.Width = 80
        repoLineNo.ReadOnly = True
        repoLineNo.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repoLineNo)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Unit"
        repoLineNo.Name = colItemUnit
        repoLineNo.Width = 110
        repoLineNo.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoLineNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLineNo.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repoLineNo)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colItemQty
        repoQty.Width = 80
        repoQty.DecimalPlaces = 2
        repoQty.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repoQty)

        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Transfer Detail"
        repoLineNo.Name = colItemPickTransferDetail
        repoLineNo.Width = 80
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        If TransferManual_KnockOFF Then
            repoLineNo.VisibleInColumnChooser = True
            repoLineNo.IsVisible = True
        ElseIf Not TransferManual_KnockOFF Then
            repoLineNo.VisibleInColumnChooser = False
            repoLineNo.IsVisible = False
        End If
        repoLineNo.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repoLineNo)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Unit Rate"
        repoQty.Name = colItemRate
        repoQty.Width = 80
        repoQty.DecimalPlaces = 2
        repoQty.ReadOnly = False
        repoQty.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repoQty)

        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Amount"
        repoQty.Name = colItemAmount
        repoQty.Width = 80
        repoQty.DecimalPlaces = 2
        repoQty.ReadOnly = True
        repoQty.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repoQty)


         

        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Total Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv_Item.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Net Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gv_Item.MasterTemplate.Columns.Add(repoAmtAfterTax)


        repoLineNo = New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Remarks"
        repoLineNo.Name = colItemremarks
        repoLineNo.Width = 120
        repoLineNo.MaxLength = 200
        repoLineNo.WrapText = True
        gv_Item.MasterTemplate.Columns.Add(repoLineNo)

        gv_Item.AllowAddNewRow = False
        gv_Item.ShowGroupPanel = False
        gv_Item.AllowColumnReorder = True
        gv_Item.AllowRowReorder = False
        gv_Item.EnableSorting = False
        gv_Item.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_Item.MasterTemplate.ShowRowHeaderColumn = False
        gv_Item.TableElement.TableHeaderHeight = 40
        gv_Item.AllowDeleteRow = True

        repoLineNo = Nothing
        repoQty = Nothing
    End Sub

    Sub OpenBatchItem()
        If clsCommon.myCBool(gv.CurrentRow.Cells(colIsBatchItem).Value) Then
            Dim frm As frmBatchItemOut = New frmBatchItemOut()
            frm.strItemCode = clsCommon.myCstr(gv.CurrentRow.Cells(colItemCode).Value)
            frm.strItemName = clsCommon.myCstr(gv.CurrentRow.Cells(colItemName).Value)
            frm.strLocationCode = clsCommon.myCstr(txt_loc_code.Value)
            frm.strCurrDocNo = clsCommon.myCstr(txtCode.Value)
            frm.strSplTransaction = "CSATransfer"
            frm.strCurrDocType = "SD-CSATRANS-RETURN"
            Dim Arrlst As New ArrayList()
            Arrlst.Add(clsCommon.myCstr(gv.CurrentRow.Tag))

            frm.ArrTransferNo = TryCast(Arrlst, ArrayList)
            frm.strUOM = clsCommon.myCstr(gv.CurrentRow.Cells(colUnit).Value)
            frm.dblMRP = 0 ' clsCommon.myCdbl(gv_Item.CurrentRow.Cells(colMRP).Value)
            frm.dblqty = clsCommon.myCdbl(gv.CurrentRow.Cells(colQty).Value)


            frm.arr = TryCast(gv.CurrentRow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
            frm.ShowDialog()
            If Not frm.isCencelButtonClicked Then
                gv.CurrentRow.Cells(colItemCode).Tag = frm.arr
            End If
        End If
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsCSATransferReturnHead()
        Try
            FunReset()
            obj = clsCSATransferReturnHead.GetData(strCode, arrLoc, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0 Then
                isInsideLoadData = True
                isNewEntry = False
                fndGateReturnNo.Value = obj.Gate_ReturnNo
                fndGateEntryNo.Value = obj.Gate_Entry_No
                txtCode.Value = obj.Document_Code
                dtpdate.Text = obj.Document_Date
                txtDesc.Text = obj.Description
                txtcustcode.Value = obj.Customer_Code
                txtcustName.Text = obj.Customer_Name
                txtCSA_Loc_Code.Value = obj.CSA_Loc_Code
                txt_loc_code.Value = obj.Bill_To_Location
                txt_loc_name.Text = obj.Location_Desc
                lblTotRAmt1.Text = clsCommon.myFormat(obj.Amount_Less_Discount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblNetAmt.Text = clsCommon.myFormat(obj.Total_Amt)


                cmbType.SelectedValue = obj.Return_Type

                txtTaxGroup.Value = obj.Tax_Group
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If

                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX1
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX1_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX1_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX1_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX1) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX2
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX2_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX2_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX2_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX2) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX3
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX3_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX3_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX3_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX3) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX4
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX4_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX4_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX4_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX4) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX5
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX5_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX5_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX5_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX5) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX6
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX6_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX6_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX6_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX6) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX7
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX7_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX7_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX7_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX7) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX8
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX8_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX8_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX8_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX8) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX9
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX9_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX9_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX9_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX9) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    gv2.Rows.AddNew()
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = obj.TAX10
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = obj.TAX10_Rate
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTBaseAmt).Value = obj.TAX10_Base_Amt
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAmt).Value = obj.TAX10_Amt
                    If (objTaxGrpMaster IsNot Nothing AndAlso objTaxGrpMaster.Arr IsNot Nothing AndAlso objTaxGrpMaster.Arr.Count > 0) Then
                        For Each objTaxGrpTr As clsTaxGroupDetail In objTaxGrpMaster.Arr
                            If (clsCommon.CompairString(objTaxGrpTr.Tax_Code, obj.TAX10) = CompairStringResult.Equal) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsSurTax).Value = objTaxGrpTr.Surtax
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTIsTaxable).Value = objTaxGrpTr.Taxable
                                ''gv2.Rows(gv2.Rows.Count - 1).Cells(colTSurTaxCode).Value = objTaxGrpTr.Surtax_Tax_Code
                                Exit For
                            End If
                        Next
                    End If
                End If


                gv_Item.Rows.Clear()
                gv.Rows.Clear()
                Dim oldicode As String = Nothing
                Dim arrTransferReturnNo As New ArrayList()
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsCSATransferReturnDetail In obj.Arr
                        If clsCommon.CompairString(oldicode, objtr.Item_Code) <> CompairStringResult.Equal Then '' want single row for repeated items
                            gv_Item.Rows.AddNew()
                            arrTransferReturnNo = New ArrayList()
                            gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemLineno).Value = objtr.Line_No
                            gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemFOC).Value = clsCommon.myCBool(IIf(objtr.FOC_Item = "1", True, False))
                            gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemItemCode).Value = objtr.Item_Code
                            gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemItemName).Value = objtr.Item_Desc
                            gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemItemCSAType).Value = objtr.CSA_Type
                            gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemUnit).Value = objtr.Unit_code
                            gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemRate).Value = objtr.Item_Cost
                            gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemremarks).Value = objtr.Remarks
                            gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemPickTransferDetail).Value = "Double Click"
                        End If
                        gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemQty).Value = clsCommon.myCdbl(gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemQty).Value) + objtr.Qty
                        gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemAmount).Value = clsCommon.myCdbl(gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemAmount).Value) + objtr.Amount

                        gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colTotTaxAmt).Value = clsCommon.myCdbl(gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colTotTaxAmt).Value) + objtr.Total_Tax_Amt
                        gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colAmtAfterTax).Value = clsCommon.myCdbl(gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colAmtAfterTax).Value) + objtr.Item_Net_Amt

                        oldicode = objtr.Item_Code
                        '=====================fill trans detail grid
                        gv.Rows.AddNew()

                        ''===============batchwise==========================================
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Item_Code)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Tag = objtr.arrBatchItem
                        If clsCommon.myLen(objtr.Transfer_No) > 0 Then
                            gv.Rows(gv.Rows.Count - 1).Tag = objtr.Transfer_No
                        ElseIf clsCommon.myLen(objtr.Adjustment_No) > 0 Then
                            gv.Rows(gv.Rows.Count - 1).Tag = objtr.Adjustment_No
                        ElseIf clsCommon.myLen(objtr.CSA_SalePatti_Return_No) > 0 Then
                            gv.Rows(gv.Rows.Count - 1).Tag = objtr.CSA_SalePatti_Return_No
                        End If
                        ''===============================================================================

                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = objtr.Line_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colFOC).Value = clsCommon.myCBool(IIf(objtr.FOC_Item = "1", True, False))
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = objtr.Item_Code
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = objtr.Item_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCSAType).Value = objtr.CSA_Type
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value = objtr.Unit_code
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = objtr.Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colRate).Value = objtr.Item_Cost
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransferNo).Value = objtr.Transfer_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransferDesc).Value = objtr.Transfer_Desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransOrgUnit).Value = objtr.Org_Transfer_UOM
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransOrgQty).Value = objtr.Org_Transfer_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransAltQty).Value = objtr.Alt_Transfer_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransAltUnit).Value = objtr.Unit_code
                        gv.Rows(gv.Rows.Count - 1).Cells(colAmount).Value = objtr.Amount
                        gv.Rows(gv.Rows.Count - 1).Cells(colremarks).Value = objtr.Remarks
                        gv.Rows(gv.Rows.Count - 1).Cells(colAdjustmentNo).Value = objtr.Adjustment_No
                        gv.Rows(gv.Rows.Count - 1).Cells(colSalePattiReturnNo).Value = objtr.CSA_SalePatti_Return_No

                        gv.Rows(gv.Rows.Count - 1).Cells(colBalanceQty).Value = FindBalanceQty(objtr.Item_Code, objtr.Transfer_No, objtr.Adjustment_No, objtr.CSA_SalePatti_Return_No, IIf(objtr.FOC_Item = 1, True, False))


                        gv.Rows(gv.Rows.Count - 1).Cells(colTax1).Value = objtr.TAX1
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt1).Value = objtr.TAX1_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate1).Value = objtr.TAX1_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt1).Value = objtr.TAX1_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax2).Value = objtr.TAX2
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt2).Value = objtr.TAX2_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate2).Value = objtr.TAX2_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt2).Value = objtr.TAX2_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax3).Value = objtr.TAX3
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt3).Value = objtr.TAX3_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate3).Value = objtr.TAX3_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt3).Value = objtr.TAX3_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax4).Value = objtr.TAX4
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt4).Value = objtr.TAX4_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate4).Value = objtr.TAX4_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt4).Value = objtr.TAX4_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax5).Value = objtr.TAX5
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt5).Value = objtr.TAX5_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate5).Value = objtr.TAX5_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt5).Value = objtr.TAX5_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax6).Value = objtr.TAX6
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt6).Value = objtr.TAX6_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate6).Value = objtr.TAX6_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt6).Value = objtr.TAX6_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax7).Value = objtr.TAX7
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt7).Value = objtr.TAX7_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate7).Value = objtr.TAX7_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt7).Value = objtr.TAX7_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax8).Value = objtr.TAX8
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt8).Value = objtr.TAX8_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate8).Value = objtr.TAX8_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt8).Value = objtr.TAX8_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax9).Value = objtr.TAX9
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt9).Value = objtr.TAX9_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate9).Value = objtr.TAX9_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt9).Value = objtr.TAX9_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTax10).Value = objtr.TAX10
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxBaseAmt10).Value = objtr.TAX10_Base_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxRate10).Value = objtr.TAX10_Rate
                        gv.Rows(gv.Rows.Count - 1).Cells(colTaxAmt10).Value = objtr.TAX10_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colTotTaxAmt).Value = objtr.Total_Tax_Amt
                        gv.Rows(gv.Rows.Count - 1).Cells(colAmtAfterTax).Value = objtr.Item_Net_Amt
                        SetitemWiseTaxOnlySetting(gv.Rows.Count - 1)
                    Next
                End If
                txtCode.MyReadOnly = True
                txtcustcode.Enabled = False
                txt_loc_code.Enabled = False
                btnSave.Text = "Update"
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending

                If obj.Status = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
            Else
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New clsCSATransferReturnHead()
        Dim objtr As New clsCSATransferReturnDetail()
        Try
            If AllowToSave() Then

                obj = New clsCSATransferReturnHead()
                obj.Gate_Entry_No = fndGateEntryNo.Value
                obj.Gate_ReturnNo = clsCommon.myCstr(fndGateReturnNo.Value)
                obj.Document_Code = clsCommon.myCstr(txtCode.Value)
                obj.Document_Date = clsCommon.myCDate(dtpdate.Text)
                obj.Description = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")
                obj.Customer_Code = clsCommon.myCstr(txtcustcode.Value)
                obj.CSA_Loc_Code = clsCommon.myCstr(txtCSA_Loc_Code.Value)
                obj.Bill_To_Location = clsCommon.myCstr(txt_loc_code.Value)
                obj.Amount_Less_Discount = clsCommon.myCdbl(lblTotRAmt1.Text)
                obj.Total_Tax_Amt = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Total_Amt = clsCommon.myCdbl(lblNetAmt.Text)

                'obj.Tax_Group = clsCommon.myCstr(clsLocationWiseTax.GetDefaultTaxGroup(obj.Bill_To_Location, obj.Customer_Code, "S"))
                obj.Tax_Group = txtTaxGroup.Value

                If (gv2.Rows.Count > 0) Then
                    obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells(colTTaxAutCode).Value)
                    obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxRate).Value)
                    obj.TAX1_Base_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTBaseAmt).Value)
                    obj.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 1) Then
                    obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells(colTTaxAutCode).Value)
                    obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxRate).Value)
                    obj.TAX2_Base_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTBaseAmt).Value)
                    obj.TAX2_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 2) Then
                    obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells(colTTaxAutCode).Value)
                    obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxRate).Value)
                    obj.TAX3_Base_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTBaseAmt).Value)
                    obj.TAX3_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 3) Then
                    obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells(colTTaxAutCode).Value)
                    obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxRate).Value)
                    obj.TAX4_Base_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTBaseAmt).Value)
                    obj.TAX4_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 4) Then
                    obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells(colTTaxAutCode).Value)
                    obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxRate).Value)
                    obj.TAX5_Base_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTBaseAmt).Value)
                    obj.TAX5_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 5) Then
                    obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells(colTTaxAutCode).Value)
                    obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxRate).Value)
                    obj.TAX6_Base_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTBaseAmt).Value)
                    obj.TAX6_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 6) Then
                    obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells(colTTaxAutCode).Value)
                    obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxRate).Value)
                    obj.TAX7_Base_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTBaseAmt).Value)
                    obj.TAX7_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 7) Then
                    obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells(colTTaxAutCode).Value)
                    obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxRate).Value)
                    obj.TAX8_Base_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTBaseAmt).Value)
                    obj.TAX8_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 8) Then
                    obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells(colTTaxAutCode).Value)
                    obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxRate).Value)
                    obj.TAX9_Base_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTBaseAmt).Value)
                    obj.TAX9_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells(colTTaxAmt).Value)
                End If
                If (gv2.Rows.Count > 9) Then
                    obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells(colTTaxAutCode).Value)
                    obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxRate).Value)
                    obj.TAX10_Base_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTBaseAmt).Value)
                    obj.TAX10_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells(colTTaxAmt).Value)
                End If



                obj.Return_Type = clsCommon.myCstr(cmbType.SelectedValue)
                If clsCommon.myLen(obj.Return_Type) <= 0 Then
                    obj.Return_Type = "I"
                End If

                obj.Arr = New List(Of clsCSATransferReturnDetail)

                Dim ItemArr As New ArrayList()

                For Each grow As GridViewRowInfo In gv.Rows
                    objtr = New clsCSATransferReturnDetail()
                    objtr.arrBatchItem = New List(Of clsBatchInventory)

                    objtr.Line_No = CInt(clsCommon.myCdbl(grow.Cells(colLineno).Value))
                    objtr.FOC_Item = CInt(IIf(clsCommon.myCBool(grow.Cells(colFOC).Value) = True, "1", "0"))
                    objtr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objtr.Unit_code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objtr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objtr.Item_Cost = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objtr.Transfer_No = clsCommon.myCstr(grow.Cells(colTransferNo).Value)
                    objtr.Org_Transfer_UOM = clsCommon.myCstr(grow.Cells(colTransOrgUnit).Value)
                    objtr.Org_Transfer_Qty = clsCommon.myCdbl(grow.Cells(colTransOrgQty).Value)
                    objtr.Alt_Transfer_Qty = clsCommon.myCdbl(grow.Cells(colTransAltQty).Value)
                    objtr.Unit_code = clsCommon.myCstr(grow.Cells(colTransAltUnit).Value)
                    objtr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                    objtr.Remarks = clsCommon.myCstr(grow.Cells(colremarks).Value)
                    objtr.Adjustment_No = clsCommon.myCstr(grow.Cells(colAdjustmentNo).Value)
                    objtr.CSA_SalePatti_Return_No = clsCommon.myCstr(grow.Cells(colSalePattiReturnNo).Value)



                    objtr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                    objtr.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt1).Value)
                    objtr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                    objtr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                    objtr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                    objtr.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt2).Value)
                    objtr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                    objtr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                    objtr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                    objtr.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt3).Value)
                    objtr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                    objtr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                    objtr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                    objtr.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt4).Value)
                    objtr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                    objtr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                    objtr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                    objtr.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt5).Value)
                    objtr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                    objtr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                    objtr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                    objtr.TAX6_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt6).Value)
                    objtr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                    objtr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                    objtr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                    objtr.TAX7_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt7).Value)
                    objtr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                    objtr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                    objtr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                    objtr.TAX8_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt8).Value)
                    objtr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                    objtr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                    objtr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                    objtr.TAX9_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt9).Value)
                    objtr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                    objtr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                    objtr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                    objtr.TAX10_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt10).Value)
                    objtr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                    objtr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)
                    objtr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    objtr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)

                    ''===============batch item from main grid
                    objtr.arrBatchItem = TryCast(grow.Cells(colItemCode).Tag, List(Of clsBatchInventory))
                    ''======================================================


                    If clsCommon.myLen(objtr.Item_Code) > 0 Then
                        obj.Arr.Add(objtr)
                    End If
                Next


                If clsCSATransferReturnHead.SaveData(obj, isNewEntry) Then
                    If Not isPost Then
                        clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                    End If

                    txtCode.Value = obj.Document_Code

                    UcAttachment1.SaveData(txtCode.Value)

                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            objtr = Nothing
        End Try
    End Sub

    Private Sub UpdateTotalAmount()
        lblTotRAmt1.Text = 0
        For Each grow As GridViewRowInfo In gv_Item.Rows
            grow.Cells(colItemLineno).Value = grow.Index + 1
            grow.Cells(colItemAmount).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colItemQty).Value) * clsCommon.myCdbl(grow.Cells(colItemRate).Value), 2, MidpointRounding.ToEven)
        Next

        lblTotRAmt1.Text = 0
        lblTaxAmt.Text = 0
        lblNetAmt.Text = 0


        For Each grow As GridViewRowInfo In gv.Rows
            grow.Cells(colLineno).Value = grow.Index + 1
            grow.Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colQty).Value) * clsCommon.myCdbl(grow.Cells(colRate).Value), 2, MidpointRounding.ToEven)
            lblTotRAmt1.Text = clsCommon.myCdbl(lblTotRAmt1.Text) + clsCommon.myCdbl(grow.Cells(colAmount).Value)
            lblTaxAmt.Text = clsCommon.myCdbl(lblTaxAmt.Text) + clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
            lblNetAmt.Text = clsCommon.myCdbl(lblNetAmt.Text) + clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
        Next
        lblTotRAmt1.Text = clsCommon.myFormat(clsCommon.myCdbl(lblTotRAmt1.Text))
        lblTaxAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(lblTaxAmt.Text))
        lblNetAmt.Text = clsCommon.myFormat(clsCommon.myCdbl(lblNetAmt.Text))
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            'KUNAL > TICKET : BM00000009580 > DATE :  18 - OCTOBER - 2016
            If AllowFutureDateTransaction(dtpdate.Value, Nothing) = False Then
                dtpdate.Select()
                Return False
            End If

            If clsCommon.myLen(dtpdate.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                dtpdate.Focus()
                dtpdate.Select()
                clsCommon.MyMessageBoxShow(Me, "Fill document date.", Me.Text)
                Errorcontrol.SetError(dtpdate, "Select document date.")
                Return False
            Else
                Errorcontrol.ResetError(dtpdate)
            End If

            If clsCommon.myLen(txtcustcode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtcustcode.Focus()
                txtcustcode.Select()
                clsCommon.MyMessageBoxShow(Me, "Select CSA", Me.Text)
                Errorcontrol.SetError(txtcustName, "Select CSA")
                Return False
            Else
                Errorcontrol.ResetError(txtcustName)
            End If

            If clsCommon.myLen(txt_loc_code.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txt_loc_code.Focus()
                txt_loc_code.Select()
                clsCommon.MyMessageBoxShow(Me, "Select location detail", Me.Text)
                Errorcontrol.SetError(txt_loc_name, "Select location detail")
                Return False
            Else
                Errorcontrol.ResetError(txt_loc_name)
            End If

            If showReturnType AndAlso clsCommon.CompairString(cmbType.SelectedValue, "") = CompairStringResult.Equal Then
                cmbType.Focus()
                cmbType.Select()
                Errorcontrol.SetError(cmbType, "Select return type.")
                clsCommon.MyMessageBoxShow(Me, "Select return type.", Me.Text)
                Return False
            Else
                Errorcontrol.ResetError(cmbType)
            End If

            Dim arrIcode As List(Of String) = Nothing
            Dim icode As String = ""
            Dim oldicode As String = ""
            Dim unit As String = ""
            Dim oldunit As String = ""
            Dim qty As Decimal = Nothing
            Dim rate As Decimal = Nothing
            Dim altqty As Decimal = Nothing
            arrIcode = New List(Of String)

            '===================Added by Richa Agarwal=================
            If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
                Dim strCustomerOfGateEntry As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Customer_Code from  TSPL_Sale_Return_Gate_Entry_Head where Gate_Entry_No =  '" + fndGateEntryNo.Value + "'"))
                If clsCommon.CompairString(strCustomerOfGateEntry, txtcustcode.Value) <> CompairStringResult.Equal Then
                    fndGateEntryNo.Focus()
                    Throw New Exception("Document Customer Not match to Customer of [Gate Entry No].")
                End If
                Dim isGateEntryNoUsed As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_SD_SALE_RETURN_HEAD where Gate_Entry_No = '" + fndGateEntryNo.Value + "' "))
                If isGateEntryNoUsed > 0 AndAlso isNewEntry Then
                    fndGateEntryNo.Focus()
                    Throw New Exception("[Gate Entry No] already used another Document.")
                End If

                If isGateEntryLocAndSaleRetrunLocSame(txt_loc_code.Value, fndGateEntryNo.Value) = False Then
                    common.clsCommon.MyMessageBoxShow(Me, "[Gate Entry No] location and Sale Return Location Should be Same.", Me.Text)
                    fndGateEntryNo.Focus()
                    Return False
                End If

                If isCancelGateEntry(fndGateEntryNo.Value) = True Then
                    common.clsCommon.MyMessageBoxShow(Me, "[Gate Entry No] canceled.Select another gate entry no.", Me.Text)
                    fndGateEntryNo.Focus()
                    Return False
                End If

            End If
            '=============================================================

            For Each grow As GridViewRowInfo In gv_Item.Rows
                icode = clsCommon.myCstr(grow.Cells(colItemItemCode).Value)
                unit = clsCommon.myCstr(grow.Cells(colItemUnit).Value)
                qty = clsCommon.myCdbl(grow.Cells(colItemQty).Value)
                rate = clsCommon.myCdbl(grow.Cells(colItemRate).Value)

                If clsCommon.myLen(icode) > 0 Then
                    If Not arrIcode.Contains(icode) Then
                        arrIcode.Add(icode)
                    End If

                    If qty <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv_Item.CurrentRow = gv_Item.Rows(grow.Index + 1)
                        Throw New Exception("Fill quantity at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    If rate <= 0 AndAlso (Not Rate_Changed_On) AndAlso clsCommon.myCBool(grow.Cells(colItemFOC).Value) = False Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv_Item.CurrentRow = gv_Item.Rows(grow.Index + 1)
                        Throw New Exception("Fill rate at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    grow.Cells(colItemAmount).Value = qty * rate


                    For ii As Integer = grow.Index + 1 To gv_Item.Rows.Count - 1
                        oldicode = clsCommon.myCstr(gv_Item.Rows(ii).Cells(colItemItemCode).Value)
                        oldunit = clsCommon.myCstr(gv_Item.Rows(ii).Cells(colItemUnit).Value)

                        If clsCommon.CompairString(icode, oldicode) = CompairStringResult.Equal Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv_Item.CurrentRow = gv_Item.Rows(ii)
                            Throw New Exception("Duplicate value at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If

                    Next

                    altqty = 0
                    For Each gr As GridViewRowInfo In gv.Rows
                        If clsCommon.CompairString(icode, clsCommon.myCstr(gr.Cells(colItemCode).Value)) = CompairStringResult.Equal Then
                            altqty += clsCommon.myCdbl(gr.Cells(colTransAltQty).Value)
                        End If
                    Next
                    If altqty <> qty Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv_Item.CurrentRow = gv_Item.Rows(grow.Index)
                        Throw New Exception("Filled quantity not match with transfer quantity i.e " + clsCommon.myCstr(altqty) + " at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                End If

            Next

            If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Fill atleast one item in grid.")
            End If
            Dim strTransferNoMultiple As String = String.Empty
            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(grow.Cells(colItemCode).Value) > 0 Then
                    ''=================batch condition===============================
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
                    ''===================================================================
                End If
                For ii As Integer = 1 To 10
                    Dim strHTax As String = ""
                    Try
                        strHTax = clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value)
                    Catch ex As Exception
                    End Try
                    If Not clsCommon.CompairString(clsCommon.myCstr(grow.Cells("COLTAX" + clsCommon.myCstr(ii)).Value), strHTax) = CompairStringResult.Equal Then
                        Throw New Exception("Wrong tax authority :" + clsCommon.myCstr(ii) + ".At Header : " + clsCommon.myCstr(grow.Cells("COLTAX" + clsCommon.myCstr(ii)).Value) + " At Detail : " + strHTax)
                    End If
                Next
                UpdateCurrentRow(grow.Index)
                If clsCommon.myLen(grow.Cells(colTransferNo).Value) > 0 Then
                    strTransferNoMultiple += "'" & clsCommon.myCstr(grow.Cells(colTransferNo).Value) & "'" & ","
                End If
            Next
            If clsCommon.myLen(strTransferNoMultiple) > 0 Then
                strTransferNoMultiple = strTransferNoMultiple.Substring(0, strTransferNoMultiple.Length - 1)
            End If
            Dim strcount As Integer = clsDBFuncationality.getSingleValue(" select count(TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Gate_Entry_No) from TSPL_Sale_Return_Gate_Entry_Invoice_Wise left join TSPL_Sale_Return_Gate_Entry_Head on TSPL_Sale_Return_Gate_Entry_Head.Gate_Entry_No=TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Gate_Entry_No where Invoice_No in (" & strTransferNoMultiple & ") and  POSTED = 1")
            If strcount > 1 Then
                Throw New Exception("ransfer No tagged with multiple gate entry no , Please select Transfer No. of single Gate entry no.")
            End If
            UpdateTotalAmount()
            Return True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Focus()
                txtCode.Select()
                clsCommon.MyMessageBoxShow(Me, "Select document for deletion.", Me.Text)
                Errorcontrol.SetError(txtCode, "Select document for deletion.")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If Not myMessages.deleteConfirm() Then
                Exit Sub
            End If

            If clsCSATransferReturnHead.DeleteData(txtCode.Value) Then
                myMessages.delete()
                FunReset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Focus()
                txtCode.Select()
                clsCommon.MyMessageBoxShow(Me, "Select document for posting.", Me.Text)
                Errorcontrol.SetError(txtCode, "Select document for posting.")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If Not myMessages.postConfirm() Then
                Exit Sub
            End If

            If AllowToSave() Then
                SaveData(True)

                If clsCSATransferReturnHead.PostData(MyBase.Form_ID, txtCode.Value, arrLoc) Then
                    myMessages.post()
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_SD_SALE_RETURN_HEAD where document_code='" + txtCode.Value + "' and trans_type='CSA'"
        Dim count As Integer = clsDBFuncationality.getSingleValue(qry)

        If count > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsCSATransferReturnHead.GetFinder(" tspl_sd_sale_return_head.trans_type='CSA' ", txtCode.Value, isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        End If
    End Sub

    Private Sub txtcustcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcustcode._MYValidating
        txtcustcode.Value = clsCustomerMaster.getFinder(" isnull(tspl_customer_master.csa_type,'N')='Y' ", txtcustcode.Value, isButtonClicked)

        txtcustName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + txtcustcode.Value + "'"))
        txtCSA_Loc_Code.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from tspl_location_master where cust_code='" + txtcustcode.Value + "'"))
    End Sub

    Private Sub txt_loc_code__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txt_loc_code._MYValidating
        Dim whrCls As String = ""

        If clsCommon.myLen(arrLoc) <= 0 Then
            whrCls = " coalesce(ltrim(Rejected_Type),'') in ('N','') and coalesce(ltrim(GIT_Type),'') in ('N','') and Is_Section='N' and Is_Sub_Location='N' "
        Else
            whrCls = " coalesce(ltrim(Rejected_Type),'') in ('N','') and coalesce(ltrim(GIT_Type),'') in ('N','') and Is_Section='N' and Is_Sub_Location='N' and tspl_location_master.location_code in (" + arrLoc + ")"
        End If

        txt_loc_code.Value = clsLocation.getFinder(whrCls, txt_loc_code.Value, isButtonClicked)

        txt_loc_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from tspl_location_master where location_code='" + txt_loc_code.Value + "'"))

    End Sub

    Private Sub gv_Item_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv_Item.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If TransferManual_KnockOFF Then
                    gv_Item.CurrentRow.Cells(colItemRate).ReadOnly = True
                ElseIf Not TransferManual_KnockOFF Then
                    If clsCommon.myCBool(gv_Item.CurrentRow.Cells(colItemFOC).Value) = True OrElse Rate_Changed_On = False Then
                        gv_Item.CurrentRow.Cells(colItemRate).ReadOnly = True
                    ElseIf clsCommon.myCBool(gv_Item.CurrentRow.Cells(colItemFOC).Value) = False AndAlso Rate_Changed_On = True Then
                        gv_Item.CurrentRow.Cells(colItemRate).ReadOnly = False
                    End If
                End If
            End If

        Catch ex As Exception
            isCellValueChnaged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv_Item.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChnaged Then
                    If e.Column Is gv_Item.Columns(colItemItemCode) Then
                        isCellValueChnaged = True
                        OpenItem(False)
                        ''if transfer knock-off is manually then below function is not run.
                        If Not TransferManual_KnockOFF Then
                            FillTransferDetail(gv_Item.CurrentRow.Index, False, False)
                        ElseIf TransferManual_KnockOFF Then
                            gv_Item.CurrentRow.Cells(colItemRate).Value = Nothing
                            gv_Item.CurrentRow.Cells(colItemAmount).Value = Nothing
                            RemoveTransferDetail(clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCode).Value), clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemUnit).Value))
                        End If
                        ''============================
                        isCellValueChnaged = False
                    End If

                    If e.Column Is gv_Item.Columns(colItemUnit) Then
                        isCellValueChnaged = True
                        OpenUnit(False)

                        If Not TransferManual_KnockOFF Then
                            FillTransferDetail(gv_Item.CurrentRow.Index, False, False)
                        ElseIf TransferManual_KnockOFF Then
                            gv_Item.CurrentRow.Cells(colItemRate).Value = Nothing
                            gv_Item.CurrentRow.Cells(colItemAmount).Value = Nothing
                            RemoveTransferDetail(clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCode).Value), clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemUnit).Value))
                        End If
                        isCellValueChnaged = False
                    End If

                    If Not TransferManual_KnockOFF AndAlso (e.Column Is gv_Item.Columns(colItemQty) OrElse e.Column Is gv_Item.Columns(colItemRate)) Then
                        isCellValueChnaged = True
                        FillTransferDetail(gv_Item.CurrentRow.Index, False, False)
                        gv_Item.CurrentRow.Cells(colItemAmount).Value = clsCommon.myCdbl(gv_Item.CurrentRow.Cells(colItemQty).Value) * clsCommon.myCdbl(gv_Item.CurrentRow.Cells(colItemRate).Value)
                        UpdateTotalAmount()
                        isCellValueChnaged = False
                    End If

                    If TransferManual_KnockOFF AndAlso e.Column Is gv_Item.Columns(colItemQty) Then
                        isCellValueChnaged = True
                        gv_Item.CurrentRow.Cells(colItemRate).Value = Nothing
                        gv_Item.CurrentRow.Cells(colItemAmount).Value = Nothing
                        RemoveTransferDetail(clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCode).Value), clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemUnit).Value))
                        isCellValueChnaged = False
                    End If

                End If
            End If
        Catch ex As Exception
            isCellValueChnaged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenItem(ByVal isButtonClicked As Boolean)
        Dim whrcls As String = " tspl_item_master.Active=1 and isnull(Is_FreshItem,0)<>1 "
        If clsCommon.myCBool(gv_Item.CurrentRow.Cells(colItemFOC).Value) = True Then
            whrcls += " and (tspl_item_master.item_code in (select TSPL_CSA_TRANSFER_DETAIL.item_code from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code where TSPL_CSA_TRANSFER_DETAIL.FOC='Y' and isnull(TSPL_CSA_TRANSFER_HEAD.status,0)=1 and TSPL_CSA_TRANSFER_HEAD.cust_code='" + txtcustcode.Value + "' " & _
                " union all select TSPL_SD_SALE_RETURN_DETAIL.item_code from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_SD_SALE_RETURN_HEAD.document_code where TSPL_SD_SALE_RETURN_HEAD.trans_type='CPR' and TSPL_SD_SALE_RETURN_DETAIL.foc_item=1 and TSPL_SD_SALE_RETURN_HEAD.status=1 and TSPL_SD_SALE_RETURN_HEAD.customer_code='" + txtcustcode.Value + "')) "
        Else
            whrcls += " and (tspl_item_master.item_code in (select TSPL_CSA_TRANSFER_DETAIL.item_code from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code where TSPL_CSA_TRANSFER_DETAIL.FOC<>'Y' and isnull(TSPL_CSA_TRANSFER_HEAD.status,0)=1 and TSPL_CSA_TRANSFER_HEAD.cust_code='" + txtcustcode.Value + "' union all select item_code from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_DETAIL.adjustment_no=TSPL_ADJUSTMENT_HEADER.adjustment_no where TSPL_ADJUSTMENT_HEADER.posted='Y' and (TSPL_ADJUSTMENT_HEADER.mainlocationcode='" + txtCSA_Loc_Code.Value + "' or TSPL_ADJUSTMENT_HEADER.loc_code='" + txtCSA_Loc_Code.Value + "') " & _
                " union all select TSPL_SD_SALE_RETURN_DETAIL.item_code from TSPL_SD_SALE_RETURN_DETAIL left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_DETAIL.document_code=TSPL_SD_SALE_RETURN_HEAD.document_code where TSPL_SD_SALE_RETURN_HEAD.trans_type='CPR' and isnull(TSPL_SD_SALE_RETURN_DETAIL.foc_item,0)=0 and TSPL_SD_SALE_RETURN_HEAD.status=1 and TSPL_SD_SALE_RETURN_HEAD.customer_code='" + txtcustcode.Value + "')) "
        End If



        gv_Item.CurrentRow.Cells(colItemItemCode).Value = clsCommon.myCstr(clsItemMaster.getFinder(whrcls, clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCode).Value), isButtonClicked))
        Dim icode As String = clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCode).Value)
        gv_Item.CurrentRow.Cells(colItemItemName).Value = clsItemMaster.GetItemName(icode, Nothing)
        gv_Item.CurrentRow.Cells(colItemItemCSAType).Value = clsItemMaster.GetItemCSAType(icode, Nothing)
        gv_Item.CurrentRow.Cells(colItemUnit).Value = clsItemMaster.GetStockUnit(icode, Nothing)
    End Sub

    Private Sub OpenUnit(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select TSPL_ITEM_UOM_DETAIL.uom_code as Code,TSPL_ITEM_UOM_DETAIL.uom_description as Description,TSPL_ITEM_UOM_DETAIL.conversion_factor from TSPL_ITEM_UOM_DETAIL "
        gv_Item.CurrentRow.Cells(colItemUnit).Value = clsCommon.myCstr(clsCommon.ShowSelectForm("CSARETUOMFND", qry, "Code", " item_code='" + clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCode).Value) + "'", clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemUnit).Value), "Code", isButtonClicked))
    End Sub

    Private Sub FillTransferDetail(ByVal intRow As Integer, ByVal ValueChanged As Boolean, ByVal NewValue As Boolean)
        Dim dt As New DataTable()
        Dim ArrTransferNo_For_Batch As New ArrayList()
        Try
            If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                txtTaxGroup.Focus()
                Throw New Exception("Please select tax group")
            End If

            isInsideLoadData = True
            Dim icode As String = clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCode).Value)
            Dim unit_code As String = clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemUnit).Value)
            Dim qty As Decimal = clsCommon.myCdbl(gv_Item.CurrentRow.Cells(colItemQty).Value)
            Dim isFOC As Boolean = IIf(clsCommon.myCBool(gv_Item.CurrentRow.Cells(colItemFOC).Value) = True, True, False)

            If ValueChanged Then
                isFOC = NewValue
            End If

            Dim totalAmount As Decimal = 0
            Dim transferNo As String = ""
            Dim SalePattiRetNo As String = ""
            Dim adjustment_no As String = ""
            Dim TransferDesc As String = ""
            Dim Org_Unit As String = ""
            Dim rate As Decimal = Nothing
            Dim org_qty As Decimal = Nothing
            Dim alt_Qty As Decimal = Nothing
            Dim counter As Integer = 0
            Dim total_rate As Decimal = Nothing

            RemoveTransferDetail(icode, unit_code)

            '======BM00000008575 (replace to_locatio_code,bill_to_location condition with from_location and csa_plant_location in below query.
            '======now only see stock of transfer location and customer ,not customer location receive stock. And no change in case of Adjustment data

            Dim strReturnCond As String = ""
            Dim strReturnCond1 As String = ""
            If showReturnType Then
                strReturnCond = " and TSPL_SD_SALE_RETURN_HEAD.Return_Type not in ('D','S') "
                strReturnCond1 = " and csa_salepatti_return.Return_Type not in ('D','S') "
            End If

            Dim qry As String = ""
            If Not isFOC Then
                qry = "select a.adjustment_no,a.doc_code,a.SalePatti_Return_No,(case when len(isnull(a.doc_code,''))>0 then csa_trans_head.description when len(isnull(a.SalePatti_Return_No,''))>0 then csa_salepatti_return.description else adj_head.description end) as description,a.item_code,sum(isnull(a.qty,0)) as qty,a.unit_code,(case when len(isnull(a.doc_code,''))>0 then sum(csa_trans.transfer_rate)/count(a.item_code) when len(isnull(a.salepatti_return_no,''))>0 then sum(csa_salepatti_return_head.unit_cost)/count(a.item_code) else sum(adj_det.unit_cost)/count(a.item_code) end) as transfer_rate from ( " & _
                      " select sub.adjustment_no,sub.DOC_CODE,sub.SalePatti_Return_No,sub.Item_Code,sum(isnull(sub.qty,0)) as qty,sub.Unit_code from ( " & _
                " select '' as adjustment_no,tspl_csa_transfer_head.doc_code,'' as SalePatti_Return_No,TSPL_CSA_TRANSFER_DETAIL.item_code,TSPL_CSA_TRANSFER_DETAIL.qty,TSPL_CSA_TRANSFER_DETAIL.unit_code from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_head on tspl_csa_transfer_head.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code where tspl_csa_transfer_head.Status=1 and TSPL_CSA_TRANSFER_DETAIL.foc<>'Y' and tspl_csa_transfer_head.cust_code='" + txtcustcode.Value + "' and tspl_csa_transfer_head.From_location_code='" + txt_loc_code.Value + "' "

                ''======= Parteek added on 01-02-20117
                If ShowDocumentCancel = True Then
                    If clsCommon.CompairString(cmbType.SelectedValue, "C") = CompairStringResult.Equal Then
                        qry += " and tspl_csa_transfer_head.CancelFlag is not null "
                    Else
                        qry += " and tspl_csa_transfer_head.CancelFlag is null "
                    End If

                End If
                ''============End

                qry += " union all " & _
                " select TSPL_ADJUSTMENT_HEADER.adjustment_no,'' as doc_code,'' as SalePatti_Return_No,TSPL_ADJUSTMENT_DETAIL.item_code,TSPL_ADJUSTMENT_DETAIL.item_quantity as qty,TSPL_ADJUSTMENT_DETAIL.unit_code from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.adjustment_no=TSPL_ADJUSTMENT_DETAIL.adjustment_no where TSPL_ADJUSTMENT_HEADER.posted='Y' and (TSPL_ADJUSTMENT_HEADER.mainlocationcode='" + txtCSA_Loc_Code.Value + "' or TSPL_ADJUSTMENT_HEADER.loc_code='" + txtCSA_Loc_Code.Value + "') " & _
                " union all " & _
                " select '' as adjustment_no,'' as doc_code,tspl_sd_sale_return_detail.document_code as SalePatti_Return_No,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.qty as qty,tspl_sd_sale_return_detail.unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_HEAD on tspl_sd_sale_return_HEAD.document_code=tspl_sd_sale_return_detail.document_code where tspl_sd_sale_return_head.trans_type='CPR' and tspl_sd_sale_return_HEAD.status=1 and tspl_sd_sale_return_head.csa_loc_code='" + txtCSA_Loc_Code.Value + "' and isnull(tspl_sd_sale_return_detail.foc_item,0)=0 " + strReturnCond + " " & _
                " union all " & _
                " select isnull(tspl_sd_sale_return_detail.adjustment_no,'') as adjustment_no,isnull(tspl_sd_sale_return_detail.transfer_no,'') as doc_code,isnull(tspl_sd_sale_return_detail.CSA_SalePatti_Return_No,'') as SalePatti_Return_No,tspl_sd_sale_return_detail.item_code,(0-isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as qty,tspl_sd_sale_return_detail.org_transfer_uom as unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code where tspl_sd_sale_return_head.trans_type='CSA' and isnull(tspl_sd_sale_return_detail.adjustment_no,'')<>'' and tspl_sd_sale_return_detail.document_code<>'" + txtCode.Value + "' and isnull(tspl_sd_sale_return_detail.foc_item,0)=0 and tspl_sd_sale_return_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_return_head.csa_loc_code='" + txtCSA_Loc_Code.Value + "'  " & _
                " union all " & _
                " select isnull(tspl_sd_sale_return_detail.adjustment_no,'') as adjustment_no,isnull(tspl_sd_sale_return_detail.transfer_no,'') as doc_code,isnull(tspl_sd_sale_return_detail.CSA_SalePatti_Return_No,'') as SalePatti_Return_No,tspl_sd_sale_return_detail.item_code,(0-isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as qty,tspl_sd_sale_return_detail.org_transfer_uom as unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code where tspl_sd_sale_return_head.trans_type='CSA' and isnull(tspl_sd_sale_return_detail.CSA_SalePatti_Return_No,'')<>'' and tspl_sd_sale_return_detail.document_code<>'" + txtCode.Value + "' and isnull(tspl_sd_sale_return_detail.foc_item,0)=0 and tspl_sd_sale_return_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_return_head.csa_loc_code='" + txtCSA_Loc_Code.Value + "'  " & _
                " union all " & _
                " select isnull(tspl_sd_sale_return_detail.adjustment_no,'') as adjustment_no,isnull(tspl_sd_sale_return_detail.transfer_no,'') as doc_code,isnull(tspl_sd_sale_return_detail.CSA_SalePatti_Return_No,'') as SalePatti_Return_No,tspl_sd_sale_return_detail.item_code,(0-isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as qty,tspl_sd_sale_return_detail.org_transfer_uom as unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code where tspl_sd_sale_return_head.trans_type='CSA' and isnull(tspl_sd_sale_return_detail.transfer_no,'')<>'' and tspl_sd_sale_return_detail.document_code<>'" + txtCode.Value + "' and isnull(tspl_sd_sale_return_detail.foc_item,0)=0 and tspl_sd_sale_return_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_return_head.bill_to_location='" + txt_loc_code.Value + "'  " & _
                " union all " & _
                " select '' as adjustment_no,isnull(TSPL_CSA_TRANSFER_HEAD.DOC_CODE,'') as against_transfer_code,'' as SalePatti_Return_No,tspl_csa_sale_transfer_detail.item_code,sum(0-isnull(tspl_csa_sale_transfer_detail.alt_qty,0)) as qty,tspl_csa_sale_transfer_detail.transfer_uom from tspl_csa_sale_transfer_detail left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=tspl_csa_sale_transfer_detail.against_transfer_code where isnull(TSPL_CSA_TRANSFER_HEAD.DOC_CODE,'')<>'' and tspl_csa_sale_transfer_detail.foc<>'Y' and tspl_csa_sale_transfer_detail.document_code in (select document_code from tspl_sd_sale_invoice_head where tspl_sd_sale_invoice_head.trans_type='CSA' and tspl_sd_sale_invoice_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_invoice_head.CSA_PLANT_LOCATION='" + txt_loc_code.Value + "')  " & _
                " group by TSPL_CSA_TRANSFER_HEAD.DOC_CODE,tspl_csa_sale_transfer_detail.item_code,tspl_csa_sale_transfer_detail.Transfer_UOM " & _
                " union all " & _
                " select isnull(tspl_adjustment_header.adjustment_no,'') as adjustment_no,'' as against_transfer_code,'' as SalePatti_Return_No,tspl_csa_sale_transfer_detail.item_code,sum(0-isnull(tspl_csa_sale_transfer_detail.alt_qty,0)) as qty,tspl_csa_sale_transfer_detail.transfer_uom from tspl_csa_sale_transfer_detail left outer join tspl_adjustment_header on tspl_adjustment_header.adjustment_no=tspl_csa_sale_transfer_detail.against_transfer_code where isnull(tspl_adjustment_header.adjustment_no,'')<>'' and tspl_csa_sale_transfer_detail.foc<>'Y' and tspl_csa_sale_transfer_detail.document_code in (select document_code from tspl_sd_sale_invoice_head where tspl_sd_sale_invoice_head.trans_type='CSA' and tspl_sd_sale_invoice_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_invoice_head.bill_to_location='" + txtCSA_Loc_Code.Value + "')  " & _
                " group by tspl_adjustment_header.adjustment_no,tspl_csa_sale_transfer_detail.item_code,tspl_csa_sale_transfer_detail.Transfer_UOM " & _
                " union all " & _
                " select '' as adjustment_no,'' as against_transfer_code,isnull(tspl_sd_sale_return_head.document_code,'') as SalePatti_Return_No,tspl_csa_sale_transfer_detail.item_code,sum(0-isnull(tspl_csa_sale_transfer_detail.alt_qty,0)) as qty,tspl_csa_sale_transfer_detail.transfer_uom from tspl_csa_sale_transfer_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_head.document_code=tspl_csa_sale_transfer_detail.Against_Transfer_Code and tspl_sd_sale_return_head.trans_type='CPR' where tspl_sd_sale_return_head.trans_type='CPR' " + strReturnCond + " and isnull(tspl_sd_sale_return_head.document_code,'')<>'' and tspl_csa_sale_transfer_detail.foc<>'Y' and tspl_csa_sale_transfer_detail.document_code in (select document_code from tspl_sd_sale_invoice_head where tspl_sd_sale_invoice_head.trans_type='CSA' and tspl_sd_sale_invoice_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_invoice_head.bill_to_location='" + txtCSA_Loc_Code.Value + "')  " & _
                " group by tspl_sd_sale_return_head.document_code,tspl_csa_sale_transfer_detail.item_code,tspl_csa_sale_transfer_detail.Transfer_UOM " & _
                " )sub group by sub.adjustment_no,sub.SalePatti_Return_No,sub.DOC_CODE,sub.Item_Code,sub.Unit_code " & _
                " )a left outer join (select doc_code,item_code,sum(isnull(qty,0)) as qty,unit_code,(sum(isnull(transfer_rate,0))/count(item_code)) as transfer_rate from TSPL_CSA_TRANSFER_DETAIL group by doc_code,item_code,Unit_code) csa_trans on csa_trans.doc_code=a.doc_code and csa_trans.item_code=a.item_code and csa_trans.unit_code=a.unit_code left outer join tspl_csa_transfer_head csa_trans_head on csa_trans_head.doc_code=a.doc_code " & _
                " left outer join (select adjustment_no,item_code,sum(isnull(Item_Quantity,0)) as qty,unit_code,(sum(isnull(unit_cost,0))/count(item_code)) as unit_cost from tspl_adjustment_detail group by adjustment_no,item_code,unit_code) adj_det on adj_det.adjustment_no=a.adjustment_no and adj_det.item_code=a.item_code and adj_det.unit_code=a.unit_code left outer join tspl_adjustment_header adj_head on adj_head.adjustment_no=a.adjustment_no " & _
                " left outer join (select document_code,item_code,sum(isnull(qty,0)) as qty,unit_code,(sum(isnull(item_cost,0))/count(item_code)) as unit_cost from tspl_sd_sale_return_detail group by document_code,item_code,unit_code) csa_salepatti_return_head on csa_salepatti_return_head.document_code=a.salepatti_return_no and csa_salepatti_return_head.item_code=a.item_code and csa_salepatti_return_head.unit_code=a.unit_code left outer join tspl_sd_sale_return_head csa_salepatti_return on csa_salepatti_return.document_code=a.salepatti_return_no and csa_salepatti_return_head.document_code=csa_salepatti_return.document_code and csa_salepatti_return.trans_type='CPR' " + strReturnCond1 + " " & _
                " where a.item_code='" + icode + "' and csa_trans_head.Tax_Group='" + txtTaxGroup.Value + "' group by a.adjustment_no,a.doc_code,a.salepatti_return_no,csa_trans_head.Description,adj_head.description,csa_salepatti_return.description ,a.item_code,a.unit_code " & _
                " having sum(a.qty)>0 "
            Else
                qry = "select '' as adjustment_no,a.doc_code,a.SalePatti_Return_No,case when len(isnull(a.doc_code,0))>0 then csa_trans_head.description else csa_salepatti_return.description end as description,a.item_code,sum(isnull(a.qty,0)) as qty,a.unit_code,case when len(isnull(a.doc_code,''))>0 then csa_trans.transfer_rate else csa_salepatti_return_detail.item_cost end as transfer_rate from ( " & _
                    " select sub.adjustment_no,sub.DOC_CODE,sub.SalePatti_Return_No,sub.Item_Code,sum(isnull(sub.qty,0)) as qty,sub.Unit_code from ( " & _
                "select '' as adjustment_no,tspl_csa_transfer_head.doc_code,'' as SalePatti_Return_No,TSPL_CSA_TRANSFER_DETAIL.item_code,TSPL_CSA_TRANSFER_DETAIL.qty,TSPL_CSA_TRANSFER_DETAIL.unit_code from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_head on tspl_csa_transfer_head.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code where tspl_csa_transfer_head.Status=1 and TSPL_CSA_TRANSFER_DETAIL.foc='Y' and tspl_csa_transfer_head.cust_code='" + txtcustcode.Value + "' and tspl_csa_transfer_head.From_location_code='" + txt_loc_code.Value + "' "

                ''======= Parteek added on 01-02-20117
                If ShowDocumentCancel = True Then
                    If clsCommon.CompairString(cmbType.SelectedValue, "C") = CompairStringResult.Equal Then
                        qry += " and tspl_csa_transfer_head.CancelFlag is not null "
                    Else
                        qry += " and tspl_csa_transfer_head.CancelFlag is null "
                    End If

                End If
                ''============End

                qry += " union all " & _
                "select '' as adjustment_no,'' as doc_code,tspl_sd_sale_return_detail.document_code as SalePatti_Return_No,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.qty,tspl_sd_sale_return_detail.unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_HEAD on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code where tspl_sd_sale_return_head.trans_type='CPR' and tspl_sd_sale_return_head.Status=1 and tspl_sd_sale_return_detail.foc_item=1 and tspl_sd_sale_return_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_return_head.csa_loc_code='" + txtCSA_Loc_Code.Value + "' " + strReturnCond + " " & _
                "union all " & _
                "select isnull(tspl_sd_sale_return_detail.adjustment_no,'') as adjustment_no,isnull(tspl_sd_sale_return_detail.transfer_no,'') as doc_code,isnull(tspl_sd_sale_return_detail.csa_salepatti_return_no,'') as SalePatti_Return_No,tspl_sd_sale_return_detail.item_code,(0-isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as qty,tspl_sd_sale_return_detail.org_transfer_uom as unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code where tspl_sd_sale_return_head.trans_type='CSA' and tspl_sd_sale_return_detail.document_code<>'" + txtCode.Value + "' and tspl_sd_sale_return_detail.foc_item=1 and tspl_sd_sale_return_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_return_head.bill_to_location='" + txt_loc_code.Value + "'  " & _
                "union all " & _
                "select TSPL_ADJUSTMENT_HEADER.adjustment_no,'' as doc_code,'' as SalePatti_Return_No,TSPL_ADJUSTMENT_DETAIL.item_code,TSPL_ADJUSTMENT_DETAIL.item_quantity as qty,TSPL_ADJUSTMENT_DETAIL.unit_code from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.adjustment_no=TSPL_ADJUSTMENT_DETAIL.adjustment_no where TSPL_ADJUSTMENT_HEADER.posted='Y' and (TSPL_ADJUSTMENT_HEADER.mainlocationcode='" + txtCSA_Loc_Code.Value + "' or TSPL_ADJUSTMENT_HEADER.loc_code='" + txtCSA_Loc_Code.Value + "') and isnull(TSPL_ADJUSTMENT_DETAIL.unit_cost,0)<=0 " & _
                "union all " & _
                "select isnull((select adjustment_no from tspl_adjustment_header where tspl_adjustment_header.adjustment_no=tspl_csa_sale_transfer_detail.against_transfer_code),'') as adjustment_no,isnull((select DOC_CODE from TSPL_CSA_TRANSFER_HEAD where TSPL_CSA_TRANSFER_HEAD.DOC_CODE=tspl_csa_sale_transfer_detail.against_transfer_code),'') as against_transfer_code,isnull((select DOCument_CODE from tspl_sd_sale_return_head where tspl_sd_sale_return_head.trans_type='CPR' " + strReturnCond + " and tspl_sd_sale_return_head.DOCument_CODE=tspl_csa_sale_transfer_detail.against_transfer_code),'') as SalePatti_Return_No,tspl_csa_sale_transfer_detail.item_code,sum(0-isnull(tspl_csa_sale_transfer_detail.alt_qty,0)) as qty,tspl_csa_sale_transfer_detail.transfer_uom from tspl_csa_sale_transfer_detail left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_CSA_SALE_TRANSFER_DETAIL.DOCUMENT_CODE and TSPL_SD_SALE_INVOICE_DETAIL.Line_No=TSPL_CSA_SALE_TRANSFER_DETAIL.Line_No and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_CSA_SALE_TRANSFER_DETAIL.Item_Code where tspl_csa_sale_transfer_detail.foc='Y' and tspl_csa_sale_transfer_detail.document_code in (select document_code from tspl_sd_sale_invoice_head where tspl_sd_sale_invoice_head.trans_type='CSA' and tspl_sd_sale_invoice_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_invoice_head.CSA_PLANT_LOCATION='" + txt_loc_code.Value + "') " & _
                " group by tspl_csa_sale_transfer_detail.against_transfer_code,tspl_csa_sale_transfer_detail.item_code,tspl_csa_sale_transfer_detail.Transfer_UOM)sub group by sub.adjustment_no,sub.DOC_CODE,sub.Item_Code,sub.Unit_code,sub.SalePatti_Return_No " & _
                " )a left outer join (select doc_code,item_code,sum(qty) as qty,unit_code,(sum(transfer_rate)/count(item_code)) as transfer_rate from TSPL_CSA_TRANSFER_DETAIL where isnull(FOC,'N')='Y' group by doc_code,item_code,Unit_code) csa_trans on csa_trans.doc_code=a.doc_code and csa_trans.item_code=a.item_code left outer join tspl_csa_transfer_head csa_trans_head on csa_trans_head.doc_code=a.doc_code " & _
                " left outer join (select document_code,item_code,sum(qty) as qty,unit_code,(sum(item_cost)/count(item_code)) as item_cost from tspl_sd_sale_return_detail where isnull(FOC_item,0)=1 group by document_code,item_code,Unit_code) csa_salepatti_return_detail on csa_salepatti_return_detail.document_code=a.salepatti_return_no and csa_salepatti_return_detail.item_code=a.item_code left outer join tspl_sd_sale_return_head csa_salepatti_return on csa_salepatti_return.document_code=a.salepatti_return_no and csa_salepatti_return_detail.document_code=csa_salepatti_return.document_code and csa_salepatti_return.trans_type='CPR' " + strReturnCond1 + " " & _
                " where a.item_code='" + icode + "' and csa_trans_head.Tax_Group='" + txtTaxGroup.Value + "' group by a.doc_code,csa_trans_head.Description,csa_salepatti_return.description,a.SalePatti_Return_No,a.item_code,a.unit_code,csa_trans.transfer_rate,csa_salepatti_return_detail.item_cost " & _
                " having sum(a.qty)>0 "
            End If
            dt = New DataTable()
            If Not TransferManual_KnockOFF Then
                dt = clsDBFuncationality.GetDataTable(qry)
            ElseIf TransferManual_KnockOFF Then
                dt = clsCommon.ShowMultipleSelectForm_ForDatatable("MULTCOMNBALCSA", qry)
            End If
            Dim cnvrsn_fctr As Decimal = Nothing
            Dim final_Cnvrsn As Decimal = Nothing
            Dim actual_Qty As Decimal = Nothing
            Dim tempQty As Decimal = 0
            Dim strtransfernoMultiple As String = String.Empty
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If qty <= 0 AndAlso Not TransferManual_KnockOFF Then
                        Exit For
                    End If
                    adjustment_no = clsCommon.myCstr(dr("adjustment_no"))
                    transferNo = clsCommon.myCstr(dr("doc_code"))
                    SalePattiRetNo = clsCommon.myCstr(dr("salepatti_return_no"))
                    TransferDesc = clsCommon.myCstr(dr("description"))
                    Org_Unit = clsCommon.myCstr(dr("unit_code"))
                    org_qty = clsCommon.myCdbl(dr("qty"))
                    rate = clsCommon.myCdbl(dr("transfer_rate"))

                    cnvrsn_fctr = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(icode, Org_Unit, Nothing)) ''transfer unit
                    final_Cnvrsn = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(icode, unit_code, Nothing)) ''return in unit

                    ''Balwinder 
                    If clsCommon.myLen(transferNo) > 0 Then
                        strtransfernoMultiple += "'" & transferNo & "'" & ","
                    End If
                    Try
                        Dim tRate As Decimal = System.Math.Round((rate / cnvrsn_fctr) * final_Cnvrsn, 4)
                        rate = tRate
                    Catch ex As Exception
                    End Try
                    ''end by Balwinder 



                    If final_Cnvrsn > 0 Then
                        actual_Qty = System.Math.Round((org_qty * cnvrsn_fctr) / final_Cnvrsn, 2)
                    Else
                        actual_Qty = 0
                    End If

                    ''==============================================================
                    tempQty = 0
                    If TransferManual_KnockOFF AndAlso qty <= 0 Then
                        qty = 1
                        tempQty = 1
                    End If
                    ''==============================================================

                    Dim fill_qty As Decimal = 0
                    While (actual_Qty > 0 AndAlso qty > 0)
                        ''==============================================================
                        If TransferManual_KnockOFF AndAlso tempQty > 0 Then
                            qty = 0
                            tempQty = 0
                        End If
                        ''==============================================================

                        counter += 1
                        If qty < actual_Qty Then
                            fill_qty = qty
                            If cnvrsn_fctr > 0 Then
                                alt_Qty = System.Math.Round((qty * final_Cnvrsn) / cnvrsn_fctr, 2)
                            Else
                                alt_Qty = 0
                            End If

                            qty = 0
                            actual_Qty = actual_Qty - qty
                        ElseIf qty > actual_Qty Then
                            fill_qty = actual_Qty
                            If cnvrsn_fctr > 0 Then
                                alt_Qty = System.Math.Round((actual_Qty * final_Cnvrsn) / cnvrsn_fctr, 2)
                            Else
                                alt_Qty = 0
                            End If

                            qty = qty - actual_Qty
                            actual_Qty = 0
                        ElseIf qty = actual_Qty Then
                            fill_qty = qty
                            If final_Cnvrsn > 0 Then
                                alt_Qty = System.Math.Round((qty * final_Cnvrsn) / cnvrsn_fctr, 2)
                            Else
                                alt_Qty = 0
                            End If

                            qty = 0
                            actual_Qty = 0
                        End If

                        gv.Rows.AddNew()

                        gv.Rows(gv.Rows.Count - 1).Cells(colFOC).Value = isFOC
                        gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = gv.Rows.Count
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = icode
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemName).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCSAType).Value = clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCSAType).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value = unit_code
                        If clsCommon.myCdbl(gv_Item.CurrentRow.Cells(colItemRate).Value) > 0 Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(gv_Item.CurrentRow.Cells(colItemRate).Value)
                        Else
                            gv.Rows(gv.Rows.Count - 1).Cells(colRate).Value = rate
                        End If

                        If TransferManual_KnockOFF Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colRate).Value = rate
                        End If

                        ''************************batchwise*********************
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(icode)
                        If clsCommon.myLen(transferNo) > 0 Then
                            gv.Rows(gv.Rows.Count - 1).Tag = transferNo
                        ElseIf clsCommon.myLen(SalePattiRetNo) > 0 Then
                            gv.Rows(gv.Rows.Count - 1).Tag = SalePattiRetNo
                        ElseIf clsCommon.myLen(adjustment_no) > 0 Then
                            gv.Rows(gv.Rows.Count - 1).Tag = adjustment_no
                        End If

                        ''88888**********************************************************

                        gv.Rows(gv.Rows.Count - 1).Cells(colTransferNo).Value = transferNo
                        gv.Rows(gv.Rows.Count - 1).Cells(colSalePattiReturnNo).Value = SalePattiRetNo
                        gv.Rows(gv.Rows.Count - 1).Cells(colAdjustmentNo).Value = adjustment_no
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransferDesc).Value = TransferDesc
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransOrgUnit).Value = Org_Unit
                        gv.Rows(gv.Rows.Count - 1).Cells(colBalanceQty).Value = org_qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransAltUnit).Value = unit_code
                        gv.Rows(gv.Rows.Count - 1).Cells(colremarks).Value = clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemremarks).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = fill_qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransOrgQty).Value = alt_Qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colTransAltQty).Value = fill_qty

                        Dim dclAmt As Decimal = Math.Round(clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value) * clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colRate).Value), 2, MidpointRounding.ToEven)
                        gv.Rows(gv.Rows.Count - 1).Cells(colAmount).Value = dclAmt
                        qry = "select TAX1,TAX1_Rate,tax2,TAX2_Rate,tax3,TAX3_Rate,tax4,TAX4_Rate,tax5,TAX5_Rate,tax6,TAX6_Rate,tax7,TAX7_Rate,tax8,TAX8_Rate,tax9,TAX9_Rate,tax10,TAX10_Rate from TSPL_CSA_TRANSFER_DETAIL where DOC_CODE='" + transferNo + "' and Item_Code='" + icode + "'  "
                        If isFOC Then
                            qry += "and FOC='Y' "
                        Else
                            qry += "and FOC<>'Y' "
                        End If
                        qry += " and Unit_code='" + Org_Unit + "'"
                        Dim dtTax As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dtTax IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            SetitemWiseTaxOnlySetting(gv.Rows.Count - 1)
                            For ii As Integer = 1 To 10
                                gv.Rows(gv.Rows.Count - 1).Cells("colTax" + clsCommon.myCstr(ii)).Value = clsCommon.myCstr(dtTax.Rows(0)("TAX" + clsCommon.myCstr(ii)))
                                gv.Rows(gv.Rows.Count - 1).Cells("colTaxRate" + clsCommon.myCstr(ii)).Value = clsCommon.myCdbl(dtTax.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Rate"))
                            Next
                            UpdateCurrentRow(gv.Rows.Count - 1)
                        End If
                        total_rate += rate
                        totalAmount += dclAmt
                        If Not ArrTransferNo_For_Batch.Contains(transferNo) Then
                            ArrTransferNo_For_Batch.Add(transferNo)
                        End If
                    End While
                Next
                If clsCommon.myLen(strtransfernoMultiple) > 0 Then
                    strtransfernoMultiple = strtransfernoMultiple.Substring(0, strtransfernoMultiple.Length - 1)
                End If
                FillGateEntryNo(strtransfernoMultiple)
            End If

            If (Not Rate_Changed_On OrElse clsCommon.myCdbl(gv_Item.CurrentRow.Cells(colItemRate).Value) <= 0) AndAlso counter > 0 Then
                gv_Item.CurrentRow.Cells(colItemRate).Value = System.Math.Round(clsCommon.myCdbl(total_rate / counter), 2)
                If TransferManual_KnockOFF Then
                    gv_Item.CurrentRow.Cells(colItemAmount).Value = System.Math.Round(totalAmount, 2)
                End If
            End If
            If TransferManual_KnockOFF Then
                UpdatewhenTransfer_ManualKnockOff()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            dt = Nothing
            isInsideLoadData = False
        End Try
    End Sub



    Private Sub RemoveTransferDetail(ByVal Icode As String, ByVal IUnit As String)
        Dim Item_Code As String = ""
        Dim Item_Unit As String = ""
        For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
            Item_Code = clsCommon.myCstr(gv.Rows(ii).Cells(colItemCode).Value)
            Item_Unit = clsCommon.myCstr(gv.Rows(ii).Cells(colUnit).Value)

            If clsCommon.myLen(Item_Code) > 0 AndAlso clsCommon.CompairString(Item_Code, Icode) = CompairStringResult.Equal Then
                gv.Rows.RemoveAt(ii)
            End If
        Next
    End Sub

    Private Sub gv_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv_Item.CurrentColumnChanged
        If gv_Item.RowCount > 0 Then
            Dim intCurrRow As Integer = gv_Item.CurrentRow.Index
            gv_Item.CurrentRow.Cells(colItemLineno).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
            If intCurrRow = gv_Item.Rows.Count - 1 Then
                gv_Item.Rows.AddNew()
                gv_Item.CurrentRow = gv_Item.Rows(intCurrRow)
                gv_Item.Rows(gv_Item.Rows.Count - 1).Cells(colItemPickTransferDetail).Value = "Double Click"
            End If
        End If
    End Sub

    Private Sub gv_Item_DoubleClick(sender As Object, e As EventArgs) Handles gv_Item.DoubleClick
        Try
            If TransferManual_KnockOFF AndAlso gv_Item.CurrentColumn Is gv_Item.Columns(colItemPickTransferDetail) Then
                Dim ItemQty As Decimal = clsCommon.myCdbl(gv_Item.CurrentRow.Cells(colItemQty).Value)

                If ItemQty <= 0 Then
                    gv_Item.Focus()
                    gv_Item.Select()
                    gv_Item.CurrentRow = gv_Item.Rows(gv_Item.CurrentRow.Index)
                    gv_Item.CurrentColumn = gv_Item.Columns(colItemQty)
                    Throw New Exception("Please enter quantity first.")
                End If

                FillTransferDetail(gv_Item.CurrentRow.Index, False, False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_Item_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv_Item.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        Else
            RemoveTransferDetail(clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemItemCode).Value), clsCommon.myCstr(gv_Item.CurrentRow.Cells(colItemUnit).Value))
        End If
    End Sub

    Private Sub gv_Item_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gv_Item.ValueChanging
        If Not TransferManual_KnockOFF Then
            ''when transfer knock-off is auto only then below function run otherwise, working done manually.
            If Not isCellValueChnaged AndAlso gv_Item.CurrentColumn Is gv_Item.Columns(colItemFOC) Then
                isCellValueChnaged = True
                FillTransferDetail(gv_Item.CurrentRow.Index, True, e.NewValue)
                isCellValueChnaged = False
            End If
        End If
    End Sub

    Public Sub funPrint(ByVal StrCode As String)

        Try
            Dim Qry As String = " select DocNo,max(Description) as Description ,max(Document_Date) as Document_Date,Customer_Code,max(Customer_Name) as Customer_Name,Item_Code ,max(Item_Desc) as Item_Desc,sum(item_cost)/count(*) as item_cost,sum(Qty) as Qty,uom,sum(amount) as amount ,max(Loc_ADd1) as Loc_ADd1,max(LOC_ADD2) as LOC_ADD2 ,max(LOC_ADD3) as LOC_ADD3,max(LocationState) as LocationState,max(RunDate) as RunDate,max(Terms_Code) as Terms_Code" & _
                                " ,max(VehicleNo) as  VehicleNo, max(termscode) as termscode , max(comments) as comments , sum(dis_amt) as dis_amt,sum( dis_amt1)as dis_amt1, sum( aftrdiscount) as aftrdiscount , Total_amount, sum(bfrdisc_amount) as bfrdisc_amount,max(tax1name) as tax1name ,sum(txt1amt) as txt1amt,  max( tax2name) as tax2name, sum(txt2amt) as txt2amt ,  max(  tax3name) as tax3name,sum( txt3amt) as txt3amt,   max( tax4name) as tax4name ,sum(txt4amt) as txt4amt,  max(  tax5name) as tax5name,sum( txt5amt) as txt5amt,max( tax6name) as tax6name,sum( txt6amt) as txt6amt,   max( tax7name) as tax7name, sum(txt7amt) as txt7amt, max( tax8name) as tax8name, sum(txt8amt) as txt8amt, max(  tax9name) as tax9name,sum( txt9amt) as txt9amt, max(tax10name) as tax10name, sum(txt10amt) as txt10amt, max(TAX1_Rate) as TAX1_Rate , max(TAX2_Rate) as TAX2_Rate ,max( TAX3_Rate) as TAX3_Rate ,max( TAX4_Rate) as  TAX4_Rate,max(TAX5_Rate) as TAX5_Rate ,max( TAX6_Rate) as TAX6_Rate ,max(TAX7_Rate) as TAX7_Rate ,max(TAX8_Rate) as TAX8_Rate ,max(TAX9_Rate) as TAX9_Rate ,max(TAX10_Rate) as TAX10_Rate ,   max(total_tax_amt) as total_tax_amt, max( DocAmt) as DocAmt ,  max( compname) as compname,max(address1) as address1,max(TAX1) as TAX1,max(TAX2) as TAX2,max(TAX3) as TAX3,max(TAX4) as TAX4 ,max(TAX5) as TAX5,max(Total_Add_Charge) as  Total_Add_Charge, max( CustomerInvoiceNo) as CustomerInvoiceNo, max( CustomerInvoiceDate) as CustomerInvoiceDate,  max(CompAddress) as  CompAddress , max(Against_Invoice_No) as Against_Invoice_No,max(SaleInvoiceAmt) as SaleInvoiceAmt,max(Remarks) as Remarks" & _
                                " from  (select TSPL_SD_SALE_RETURN_HEAD.DOCUMENT_CODE as DocNo,TSPL_SD_SALE_RETURN_HEAD.Description ,convert(varchar,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SALE_RETURN_HEAD.Customer_Code, TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SALE_RETURN_DETAIL.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SALE_RETURN_DETAIL.item_cost, TSPL_SD_SALE_RETURN_DETAIL.Qty ,TSPL_SD_SALE_RETURN_DETAIL.unit_code as uom ,TSPL_SD_SALE_RETURN_DETAIL.amount,TSPL_LOCATION_MASTER.Add1  as Loc_ADd1,TSPL_LOCATION_MASTER.Add2  as LOC_ADD2,TSPL_LOCATION_MASTER.Add3 as LOC_ADD3, TSPL_STATE_MASTER.State_Name as LocationState,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy") + "' as RunDate,TSPL_SD_SALE_RETURN_HEAD.Terms_Code,TSPL_SD_SALE_RETURN_HEAD.VehicleNo ,TSPL_SD_SALE_RETURN_HEAD .Terms_Code as termscode , TSPL_SD_SALE_RETURN_HEAD .Comments as comments ,  TSPL_SD_SALE_RETURN_HEAD .Discount_Amt as dis_amt,TSPL_SD_SALE_RETURN_DETAIL .Disc_Amt  as dis_amt1, TSPL_SD_SALE_RETURN_HEAD.Amount_Less_Discount  as aftrdiscount ,TSPL_SD_SALE_RETURN_HEAD .Total_Amt as Total_amount, TSPL_SD_SALE_RETURN_HEAD.Discount_Base as bfrdisc_amount,   tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_SALE_RETURN_HEAD.tax1_amt,0) as txt1amt,   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_SD_SALE_RETURN_HEAD.tax2_amt,0) as txt2amt,   tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SD_SALE_RETURN_HEAD.tax3_amt,0) as txt3amt,   tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SD_SALE_RETURN_HEAD.tax4_amt,0) as txt4amt,   tax5.Tax_Code_Desc as tax5name,isnull (TSPL_SD_SALE_RETURN_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SD_SALE_RETURN_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SD_SALE_RETURN_HEAD.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name,isnull (TSPL_SD_SALE_RETURN_HEAD.tax8_amt,0) as txt8amt,    tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SD_SALE_RETURN_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SD_SALE_RETURN_HEAD.tax10_amt,0) as txt10amt, TSPL_SD_SALE_RETURN_HEAD. TAX1_Rate ,TSPL_SD_SALE_RETURN_HEAD.TAX2_Rate ,TSPL_SD_SALE_RETURN_HEAD.TAX3_Rate ,TSPL_SD_SALE_RETURN_HEAD.TAX4_Rate ,TSPL_SD_SALE_RETURN_HEAD.TAX5_Rate ,TSPL_SD_SALE_RETURN_HEAD.TAX6_Rate ,TSPL_SD_SALE_RETURN_HEAD.TAX7_Rate ,TSPL_SD_SALE_RETURN_HEAD.TAX8_Rate ,TSPL_SD_SALE_RETURN_HEAD.TAX9_Rate ,TSPL_SD_SALE_RETURN_HEAD.TAX10_Rate,  isnull(TSPL_SD_SALE_RETURN_HEAD .Total_Tax_Amt,0) as total_tax_amt, TSPL_SD_SALE_RETURN_HEAD.Total_Amt as DocAmt,  TSPL_COMPANY_MASTER.Comp_Name as compname,ISNULL(tspl_company_Master.ADD1,'') as address1,TSPL_SD_SALE_RETURN_HEAD.TAX1,TSPL_SD_SALE_RETURN_HEAD.TAX2,TSPL_SD_SALE_RETURN_HEAD.TAX3,TSPL_SD_SALE_RETURN_HEAD.TAX4,TSPL_SD_SALE_RETURN_HEAD.TAX5,TSPL_SD_SALE_RETURN_HEAD.Total_Add_Charge , TSPL_Customer_Invoice_Head.Document_No as CustomerInvoiceNo,  convert(varchar(15),TSPL_Customer_Invoice_Head.Document_Date,106) as CustomerInvoiceDate, (case when ISNULL(TSPL_CUSTOMER_MASTER.ADD1,'')<> '' then TSPL_CUSTOMER_MASTER.ADD1 else '' end + case when ISNULL(TSPL_CUSTOMER_MASTER.ADD2,'')<> '' then ', ' +TSPL_CUSTOMER_MASTER.ADD2 else '' end + case when ISNULL(TSPL_CUSTOMER_MASTER.ADD3,'')<> '' then ', ' +TSPL_CUSTOMER_MASTER.ADD3 else '' end ) as CustAddress,	 TSPL_COMPANY_MASTER.Tin_No as TinNo, TSPL_COMPANY_MASTER.CST_LST as CstNo, (case when ISNULL(TSPL_COMPANY_MASTER.ADD1,'')<> '' then TSPL_COMPANY_MASTER.ADD1 else '' end + case when ISNULL(TSPL_COMPANY_MASTER.ADD2,'')<> '' then ', ' +TSPL_COMPANY_MASTER.ADD2 else '' end +  case when ISNULL(TSPL_COMPANY_MASTER.ADD3,'')<> '' then ', ' +TSPL_COMPANY_MASTER.ADD3 else '' end ) as CompAddress " & _
                                " ,stuff((select ',' + isnull(detail.Transfer_No  ,'') from TSPL_SD_SALE_RETURN_DETAIL as detail left join TSPL_CSA_TRANSFER_DETAIL on  detail.Transfer_No = TSPL_CSA_TRANSFER_DETAIL.DOC_CODE  and  detail.Item_Code=TSPL_CSA_TRANSFER_DETAIL.Item_Code " & _
                                " left join TSPL_SD_SALE_RETURN_HEAD as HEAD on HEAD.Document_Code =detail.DOCUMENT_CODE where HEAD.Trans_Type ='CSA' " & _
                                " and HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE " & _
                                " and detail.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE and detail.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code" & _
                                " for xml path ('')),1,1,'' )as Against_Invoice_No,stuff((select ',' + isnull(cast(cast(TSPL_CSA_TRANSFER_head.document_Amount as decimal) as varchar),'') from TSPL_SD_SALE_RETURN_DETAIL as detail " & _
                                " left join TSPL_CSA_TRANSFER_DETAIL on  detail.Transfer_No = TSPL_CSA_TRANSFER_DETAIL.DOC_CODE  and  detail.Item_Code=TSPL_CSA_TRANSFER_DETAIL.Item_Code " & _
                                " left join TSPL_CSA_TRANSFER_head on TSPL_CSA_TRANSFER_head.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code " & _
                                " left join TSPL_SD_SALE_RETURN_HEAD as HEAD on HEAD.Document_Code = detail.DOCUMENT_CODE where HEAD.Trans_Type ='CSA' and HEAD.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE " & _
                                " and detail.Document_Code=TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE and detail.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code" & _
                                " for xml path ('')),1,1,'' )as SaleInvoiceAmt,TSPL_SD_SALE_RETURN_HEAD.Remarks from TSPL_SD_SALE_RETURN_DETAIL" & _
                                " LEFT outer join TSPL_SD_SALE_RETURN_HEAD  on TSPL_SD_SALE_RETURN_HEAD.Document_Code  =TSPL_SD_SALE_RETURN_DETAIL.Document_Code" & _
                                " LEFT outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_SALE_RETURN_HEAD.tax1 " & _
                                " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_SALE_RETURN_HEAD.tax2  " & _
                                " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_SALE_RETURN_HEAD .TAX3  " & _
                                " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_SALE_RETURN_HEAD .tax4  " & _
                                " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_SALE_RETURN_HEAD .tax5  " & _
                                " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_SALE_RETURN_HEAD .TAX6  " & _
                                " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_SALE_RETURN_HEAD .TAX7 " & _
                                " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_SALE_RETURN_HEAD .TAX8 " & _
                                " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_SALE_RETURN_HEAD .TAX9 " & _
                                " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_SALE_RETURN_HEAD .TAX10   " & _
                                " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SD_SALE_RETURN_HEAD.comp_code  " & _
                                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_RETURN_HEAD.Customer_Code " & _
                                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location " & _
                                " LEFT OUTER JOIN TSPL_STATE_MASTER On TSPL_STATE_MASTER.State_Code=TSPL_LOCATION_MASTER.State" & _
                                " Left Outer Join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code" & _
                                " LEFT OUTER JOIN TSPL_CSA_TRANSFER_HEAD  on TSPL_CSA_TRANSFER_HEAD.DOC_CODE =TSPL_SD_SALE_RETURN_DETAIL.Transfer_No " & _
                                " LEFT OUTER JOIN TSPL_CSA_TRANSFER_DETAIL on TSPL_CSA_TRANSFER_DETAIL.DOC_CODE = TSPL_CSA_TRANSFER_HEAD.DOC_CODE and TSPL_CSA_TRANSFER_DETAIL.Item_Code= TSPL_SD_SALE_RETURN_DETAIL.Item_Code " & _
                                " LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Against_Sale_Return_No = TSPL_SD_SALE_RETURN_HEAD.Document_Code   " & _
                                " LEFT OUTER JOIN TSPL_ITEM_BARCODE ON TSPL_ITEM_BARCODE.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code AND  TSPL_ITEM_BARCODE.Item_MRP = TSPL_SD_SALE_RETURN_DETAIL.MRP where TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='CSA' and TSPL_SD_SALE_RETURN_HEAD.Document_Code in ('" + StrCode + "'))as mm group by DocNo,mm.Customer_Code ,mm.Item_Code ,mm.uom,mm.Total_amount"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.NewSalesReports, dt, "crptCSASaleReturn", "Sale Return")
                frmCRV = Nothing
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Document not found to Print")
        Else
            funPrint(txtCode.Value)
        End If
    End Sub

    Private Sub btnRev_Unpost_Click(sender As Object, e As EventArgs) Handles btnRev_Unpost.Click
        Try
            ''BM00000009170
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Focus()
                txtCode.Select()
                clsCommon.MyMessageBoxShow(Me, "Select document for unposting.", Me.Text)
                Errorcontrol.SetError(txtCode, "Select document for unposting.")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If Not clsCommon.MyMessageBoxShow("Reverse and Unpost the current document?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                btnRev_Unpost.Visible = False
                Exit Sub
            End If

            If clsCSATransferReturnHead.UnPostData(MyBase.Form_ID, txtCode.Value, arrLoc) Then
                clsCommon.MyMessageBoxShow(Me, "Record UnPosted Successfully", Me.Text)

                LoadData(txtCode.Value, NavigatorType.Current)
                btnRev_Unpost.Visible = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

#Region "RunWhenTransferManualKnockOFF"
    Private Sub gv_CellValueChanged1(sender As Object, e As GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If TransferManual_KnockOFF Then
                If Not isInsideLoadData Then
                    If Not isCellValueChnaged Then
                        If e.Column Is gv.Columns(colQty) Then
                            isCellValueChnaged = True
                            CheckQtyWithBalanceQty(gv.CurrentRow.Index)
                            UpdatewhenTransfer_ManualKnockOff()

                            If gv.CurrentRow.Tag IsNot Nothing AndAlso clsCommon.myLen(gv.CurrentRow.Tag) > 0 Then
                                OpenBatchItem()
                            End If
                            isCellValueChnaged = False
                        End If
                    End If
                End If
            End If ''end setting cond.
        Catch ex As Exception
            isCellValueChnaged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub CheckQtyWithBalanceQty(ByVal IntRow As Integer)
        Try
            Dim icode As String = clsCommon.myCstr(gv.Rows(IntRow).Cells(colItemCode).Value)

            If clsCommon.myLen(icode) <= 0 Then
                Exit Sub
            End If

            Dim Org_Unit As String = clsCommon.myCstr(gv.Rows(IntRow).Cells(colTransOrgUnit).Value)
            Dim org_qty As Decimal = clsCommon.myCdbl(gv.Rows(IntRow).Cells(colBalanceQty).Value)
            Dim Rate As Decimal = clsCommon.myCdbl(gv.Rows(IntRow).Cells(colRate).Value)

            Dim CurrentQty As Decimal = clsCommon.myCdbl(gv.Rows(IntRow).Cells(colQty).Value)
            Dim CurrentUnit As String = clsCommon.myCstr(gv.Rows(IntRow).Cells(colUnit).Value)

            Dim cnvrsn_fctr As Decimal = 0
            Dim final_Cnvrsn As Decimal = 0
            Dim actual_Qty As Decimal = 0
            Dim fill_qty As Decimal = 0
            Dim alt_Qty As Decimal = 0

            cnvrsn_fctr = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(icode, Org_Unit, Nothing)) ''transfer unit
            final_Cnvrsn = clsCommon.myCdbl(clsItemMaster.GetConvertionFactor(icode, CurrentUnit, Nothing)) ''return in unit

            If final_Cnvrsn > 0 Then
                actual_Qty = System.Math.Round((org_qty * cnvrsn_fctr) / final_Cnvrsn, 2)
            Else
                actual_Qty = 0
            End If


            If CurrentQty < actual_Qty Then
                fill_qty = CurrentQty
                If cnvrsn_fctr > 0 Then
                    alt_Qty = System.Math.Round((CurrentQty * final_Cnvrsn) / cnvrsn_fctr, 2)
                Else
                    alt_Qty = 0
                End If

                CurrentQty = 0
                actual_Qty = actual_Qty - CurrentQty
            ElseIf CurrentQty > actual_Qty Then
                'fill_qty = actual_Qty
                'If cnvrsn_fctr > 0 Then
                '    alt_Qty = System.Math.Round((actual_Qty * final_Cnvrsn) / cnvrsn_fctr, 2)
                'Else
                '    alt_Qty = 0
                'End If

                'CurrentQty = CurrentQty - actual_Qty
                'actual_Qty = 0
                gv.Rows(IntRow).Cells(colQty).Value = Nothing
                gv.Rows(IntRow).Cells(colTransOrgQty).Value = Nothing
                gv.Rows(IntRow).Cells(colTransAltQty).Value = Nothing
                gv.Rows(IntRow).Cells(colAmount).Value = Nothing
                Throw New Exception("Balance transfer qty " + clsCommon.myCstr(actual_Qty) + " " + clsCommon.myCstr(CurrentUnit) + " is not matched with filled qty " + clsCommon.myCstr(CurrentQty) + " " + clsCommon.myCstr(CurrentUnit) + " at row no. " + clsCommon.myCstr(IntRow + 1) + ".")
            ElseIf CurrentQty = actual_Qty Then
                fill_qty = CurrentQty
                If final_Cnvrsn > 0 Then
                    alt_Qty = System.Math.Round((CurrentQty * cnvrsn_fctr) / final_Cnvrsn, 2)
                Else
                    alt_Qty = 0
                End If

                CurrentQty = 0
                actual_Qty = 0
            End If

            gv.Rows(IntRow).Cells(colQty).Value = fill_qty
            gv.Rows(IntRow).Cells(colTransOrgQty).Value = alt_Qty
            gv.Rows(IntRow).Cells(colTransAltQty).Value = fill_qty

            gv.Rows(IntRow).Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(gv.Rows(IntRow).Cells(colQty).Value) * clsCommon.myCdbl(gv.Rows(IntRow).Cells(colRate).Value), 2)
            UpdateCurrentRow(IntRow)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub UpdatewhenTransfer_ManualKnockOff()
        Try
            Dim Rate As Decimal = 0
            Dim Amount As Decimal = 0
            Dim Qty As Decimal = 0
            Dim TaxAmt As Decimal = 0
            Dim NetAmt As Decimal = 0
            For Each grow As GridViewRowInfo In gv_Item.Rows
                If clsCommon.myLen(grow.Cells(colItemItemCode).Value) <= 0 Then
                    Continue For
                End If

                If clsCommon.myCBool(grow.Cells(colItemFOC).Value) Then
                    grow.Cells(colItemAmount).Value = 0
                    grow.Cells(colTotTaxAmt).Value = 0
                    grow.Cells(colAmtAfterTax).Value = 0
                Else
                    Qty = 0
                    Rate = 0
                    Amount = 0
                    grow.Cells(colItemRate).Value = 0
                    grow.Cells(colItemAmount).Value = 0

                    Dim Counter As Decimal = 0

                    For Each gr As GridViewRowInfo In gv.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colItemItemCode).Value), clsCommon.myCstr(gr.Cells(colItemCode).Value)) = CompairStringResult.Equal Then
                            Qty += clsCommon.myCdbl(gr.Cells(colQty).Value)
                            Rate += clsCommon.myCdbl(gr.Cells(colRate).Value)
                            Amount += clsCommon.myCdbl(gr.Cells(colQty).Value) * clsCommon.myCdbl(gr.Cells(colRate).Value)

                            TaxAmt += clsCommon.myCdbl(gr.Cells(colTotTaxAmt).Value)
                            NetAmt += clsCommon.myCdbl(gr.Cells(colAmtAfterTax).Value)

                            Counter += 1
                        End If
                    Next

                    If Qty > clsCommon.myCdbl(grow.Cells(colItemQty).Value) Then
                        Throw New Exception("Entered sum of transfer qty(" + clsCommon.myCstr(Qty) + ") is more then return qty(" + clsCommon.myCstr(grow.Cells(colItemQty).Value) + ")" + Environment.NewLine + "for item [" + clsCommon.myCstr(grow.Cells(colItemItemCode).Value) + "] : " + clsCommon.myCstr(grow.Cells(colItemItemName).Value) + "")
                    End If

                    If Qty > 0 Then
                        grow.Cells(colItemRate).Value = System.Math.Round(Amount / Qty, 2)
                        grow.Cells(colItemAmount).Value = Amount

                        grow.Cells(colTotTaxAmt).Value = TaxAmt
                        grow.Cells(colAmtAfterTax).Value = NetAmt
                    End If
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function FindBalanceQty(ByVal iCode As String, ByVal TransferNo As String, ByVal AdjustmentNo As String, ByVal CSA_SalePatti_Return_No As String, ByVal isFOC As Boolean) As Decimal
        Dim qry As String = ""
        Dim strReturnCond As String = ""
        Dim strReturnCond1 As String = ""
        Dim strReturnCond2 As String = ""
        Try
            If showReturnType Then
                strReturnCond = " and tspl_sd_sale_return_head.return_type not in ('D','S') "
                strReturnCond1 = " and salepatti_return.return_type not in ('D','S') "
                strReturnCond2 = " and csa_salepatti_return_head.return_type not in ('D','S') "
            End If

            If Not isFOC Then
                qry = "select sum(a.qty) as qty from ( " & _
                      " select sub.adjustment_no,sub.DOC_CODE,sub.salepatti_return_no,sub.Item_Code,sum(isnull(sub.qty,0)) as qty,sub.Unit_code from ( " & _
                "select '' as adjustment_no,tspl_csa_transfer_head.doc_code,'' as salepatti_return_no,TSPL_CSA_TRANSFER_DETAIL.item_code,TSPL_CSA_TRANSFER_DETAIL.qty,TSPL_CSA_TRANSFER_DETAIL.unit_code from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_head on tspl_csa_transfer_head.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code where tspl_csa_transfer_head.Status=1 and TSPL_CSA_TRANSFER_DETAIL.foc<>'Y' and tspl_csa_transfer_head.cust_code='" + txtcustcode.Value + "' and tspl_csa_transfer_head.From_location_code='" + txt_loc_code.Value + "' "

                ''======= Parteek added on 01-02-20117
                If ShowDocumentCancel = True Then
                    If clsCommon.CompairString(cmbType.SelectedValue, "C") = CompairStringResult.Equal Then
                        qry += " and tspl_csa_transfer_head.CancelFlag is not null "
                    Else
                        qry += " and tspl_csa_transfer_head.CancelFlag is null "
                    End If

                End If
                ''============End
                qry += "union all " & _
                "select TSPL_ADJUSTMENT_HEADER.adjustment_no,'' as doc_code,'' as salepatti_return_no,TSPL_ADJUSTMENT_DETAIL.item_code,TSPL_ADJUSTMENT_DETAIL.item_quantity as qty,TSPL_ADJUSTMENT_DETAIL.unit_code from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.adjustment_no=TSPL_ADJUSTMENT_DETAIL.adjustment_no where TSPL_ADJUSTMENT_HEADER.posted='Y' and (TSPL_ADJUSTMENT_HEADER.mainlocationcode='" + txtCSA_Loc_Code.Value + "' or TSPL_ADJUSTMENT_HEADER.loc_code='" + txtCSA_Loc_Code.Value + "') " & _
                "union all " & _
                "select '' as adjustment_no,'' as doc_code,tspl_sd_sale_return_head.document_code as salepatti_return_no,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.qty as qty,tspl_sd_sale_return_detail.unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_head.document_code=tspl_sd_sale_return_detail.document_code where tspl_sd_sale_return_head.status=1 and tspl_sd_sale_return_head.trans_type='CPR' and tspl_sd_sale_return_head.csa_loc_code='" + txtCSA_Loc_Code.Value + "' " + strReturnCond + " " & _
                "union all " & _
                "select isnull(tspl_sd_sale_return_detail.adjustment_no,'') as adjustment_no,isnull(tspl_sd_sale_return_detail.transfer_no,'') as doc_code,isnull(tspl_sd_sale_return_detail.csa_salepatti_return_no,'') as salepatti_return_no,tspl_sd_sale_return_detail.item_code,(0-isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as qty,tspl_sd_sale_return_detail.org_transfer_uom as unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code where tspl_sd_sale_return_head.trans_type='CSA' and isnull(tspl_sd_sale_return_detail.adjustment_no,'')<>'' and tspl_sd_sale_return_detail.document_code<>'" + txtCode.Value + "' and tspl_sd_sale_return_detail.foc_item <> 1 and tspl_sd_sale_return_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_return_head.csa_loc_code='" + txtCSA_Loc_Code.Value + "'  " & _
                "union all " & _
                "select isnull(tspl_sd_sale_return_detail.adjustment_no,'') as adjustment_no,isnull(tspl_sd_sale_return_detail.transfer_no,'') as doc_code,isnull(tspl_sd_sale_return_detail.csa_salepatti_return_no,'') as salepatti_return_no,tspl_sd_sale_return_detail.item_code,(0-isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as qty,tspl_sd_sale_return_detail.org_transfer_uom as unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code where tspl_sd_sale_return_head.trans_type='CSA'  and isnull(tspl_sd_sale_return_detail.csa_salepatti_return_no,'')<>'' and tspl_sd_sale_return_detail.document_code<>'" + txtCode.Value + "' and tspl_sd_sale_return_detail.foc_item <> 1 and tspl_sd_sale_return_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_return_head.csa_loc_code='" + txtCSA_Loc_Code.Value + "'  " & _
                " union all " & _
                "select isnull(tspl_sd_sale_return_detail.adjustment_no,'') as adjustment_no,isnull(tspl_sd_sale_return_detail.transfer_no,'') as doc_code,isnull(tspl_sd_sale_return_detail.csa_salepatti_return_no,'') as salepatti_return_no,tspl_sd_sale_return_detail.item_code,(0-isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as qty,tspl_sd_sale_return_detail.org_transfer_uom as unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code where tspl_sd_sale_return_head.trans_type='CSA'  and isnull(tspl_sd_sale_return_detail.transfer_no,'')<>'' and tspl_sd_sale_return_detail.document_code<>'" + txtCode.Value + "' and tspl_sd_sale_return_detail.foc_item <> 1 and tspl_sd_sale_return_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_return_head.bill_to_location='" + txt_loc_code.Value + "'  " & _
                "union all " & _
                "select '' as adjustment_no,isnull(TSPL_CSA_TRANSFER_HEAD.DOC_CODE,'') as against_transfer_code,'' as salepatti_return_no,tspl_csa_sale_transfer_detail.item_code,sum(0-isnull(tspl_csa_sale_transfer_detail.alt_qty,0)) as qty,tspl_csa_sale_transfer_detail.transfer_uom from tspl_csa_sale_transfer_detail left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE=tspl_csa_sale_transfer_detail.against_transfer_code where tspl_csa_sale_transfer_detail.foc<>'Y' and tspl_csa_sale_transfer_detail.document_code in (select document_code from tspl_sd_sale_invoice_head where tspl_sd_sale_invoice_head.trans_type='CSA' and tspl_sd_sale_invoice_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_invoice_head.CSA_PLANT_LOCATION='" + txt_loc_code.Value + "')  " & _
                " group by TSPL_CSA_TRANSFER_HEAD.DOC_CODE,tspl_csa_sale_transfer_detail.item_code,tspl_csa_sale_transfer_detail.Transfer_UOM " & _
                "union all " & _
                "select '' as adjustment_no,'' as against_transfer_code,isnull(tspl_sd_sale_return_head.document_code,'') as salepatti_return_no,tspl_csa_sale_transfer_detail.item_code,sum(0-isnull(tspl_csa_sale_transfer_detail.alt_qty,0)) as qty,tspl_csa_sale_transfer_detail.transfer_uom from tspl_csa_sale_transfer_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_head.document_code=tspl_csa_sale_transfer_detail.against_transfer_code where tspl_sd_sale_return_head.trans_type='CPR' " + strReturnCond + " and tspl_csa_sale_transfer_detail.foc<>'Y' and tspl_csa_sale_transfer_detail.document_code in (select document_code from tspl_sd_sale_invoice_head where tspl_sd_sale_invoice_head.trans_type='CSA' and tspl_sd_sale_invoice_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_invoice_head.bill_to_location='" + txtCSA_Loc_Code.Value + "')  " & _
                " group by tspl_sd_sale_return_head.document_code,tspl_csa_sale_transfer_detail.item_code,tspl_csa_sale_transfer_detail.Transfer_UOM " & _
                "union all " & _
                "select isnull(tspl_adjustment_header.adjustment_no,'') as adjustment_no,'' as against_transfer_code,'' as salepatti_return_no,tspl_csa_sale_transfer_detail.item_code,sum(0-isnull(tspl_csa_sale_transfer_detail.alt_qty,0)) as qty,tspl_csa_sale_transfer_detail.transfer_uom from tspl_csa_sale_transfer_detail left outer join tspl_adjustment_header on tspl_adjustment_header.adjustment_no=tspl_csa_sale_transfer_detail.against_transfer_code where tspl_csa_sale_transfer_detail.foc<>'Y' and tspl_csa_sale_transfer_detail.document_code in (select document_code from tspl_sd_sale_invoice_head where tspl_sd_sale_invoice_head.trans_type='CSA' and tspl_sd_sale_invoice_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_invoice_head.bill_to_location='" + txtCSA_Loc_Code.Value + "')  " & _
                " group by tspl_adjustment_header.adjustment_no,tspl_csa_sale_transfer_detail.item_code,tspl_csa_sale_transfer_detail.Transfer_UOM)sub group by sub.adjustment_no,sub.DOC_CODE,sub.Item_Code,sub.Unit_code,SUB.SALEPATTI_RETURN_NO " & _
                ")a left outer join (select doc_code,item_code,sum(qty) as qty,unit_code,(sum(transfer_rate)/count(item_code)) as transfer_rate from TSPL_CSA_TRANSFER_DETAIL group by doc_code,item_code,Unit_code) csa_trans on csa_trans.doc_code=a.doc_code and csa_trans.item_code=a.item_code and csa_trans.unit_code=a.unit_code left outer join tspl_csa_transfer_head csa_trans_head on csa_trans_head.doc_code=a.doc_code " & _
                " left outer join (select adjustment_no,item_code,sum(Item_Quantity) as qty,unit_code,(sum(unit_cost)/count(item_code)) as unit_cost from tspl_adjustment_detail group by adjustment_no,item_code,unit_code) adj_det on adj_det.adjustment_no=a.adjustment_no and adj_det.item_code=a.item_code and adj_det.unit_code=a.unit_code left outer join tspl_adjustment_header adj_head on adj_head.adjustment_no=a.adjustment_no " & _
                " left outer join (select document_code,item_code,sum(qty) as qty,unit_code,(sum(item_cost)/count(item_code)) as unit_cost from tspl_sd_sale_return_detail group by document_code,item_code,unit_code) salepatti_return_det on salepatti_return_det.document_code=a.salepatti_return_no and salepatti_return_det.item_code=a.item_code and salepatti_return_det.unit_code=a.unit_code left outer join tspl_sd_sale_return_head salepatti_return on salepatti_return.document_code=a.salepatti_return_no and salepatti_return_det.document_code=salepatti_return.document_code and salepatti_return.trans_type='CPR' " + strReturnCond1 + " " & _
                " where a.item_code='" + iCode + "' "
                If clsCommon.myLen(TransferNo) > 0 Then
                    qry += " and a.doc_code= '" + TransferNo + "' "
                End If
                If clsCommon.myLen(AdjustmentNo) > 0 Then
                    qry += " and a.adjustment_no='" + AdjustmentNo + "' "
                End If
                If clsCommon.myLen(CSA_SalePatti_Return_No) > 0 Then
                    qry += " and a.salepatti_return_no='" + CSA_SalePatti_Return_No + "' "
                End If
                qry += " group by a.adjustment_no,a.doc_code,a.salepatti_return_no,csa_trans_head.Description,adj_head.description ,a.item_code,a.unit_code "
            Else
                qry = "select sum(a.qty) as qty from ( " & _
                    " select sub.adjustment_no,sub.DOC_CODE,sub.salepatti_return_no,sub.Item_Code,sum(isnull(sub.qty,0)) as qty,sub.Unit_code from ( " & _
                "select '' as adjustment_no,tspl_csa_transfer_head.doc_code,'' as salepatti_return_no,TSPL_CSA_TRANSFER_DETAIL.item_code,TSPL_CSA_TRANSFER_DETAIL.qty,TSPL_CSA_TRANSFER_DETAIL.unit_code from TSPL_CSA_TRANSFER_DETAIL left outer join TSPL_CSA_TRANSFER_head on tspl_csa_transfer_head.doc_code=TSPL_CSA_TRANSFER_DETAIL.doc_code where tspl_csa_transfer_head.Status=1 and TSPL_CSA_TRANSFER_DETAIL.foc='Y' and tspl_csa_transfer_head.cust_code='" + txtcustcode.Value + "' and tspl_csa_transfer_head.From_location_code='" + txt_loc_code.Value + "' "

                ''======= Parteek added on 01-02-20117
                If ShowDocumentCancel = True Then
                    If clsCommon.CompairString(cmbType.SelectedValue, "C") = CompairStringResult.Equal Then
                        qry += " and tspl_csa_transfer_head.CancelFlag is not null "
                    Else
                        qry += " and tspl_csa_transfer_head.CancelFlag is null "
                    End If

                End If
                ''============End

                qry += " union all " & _
                "select '' as adjustment_no,'' as doc_code,tspl_sd_sale_return_head.document_code as salepatti_return_no,tspl_sd_sale_return_detail.item_code,tspl_sd_sale_return_detail.qty,tspl_sd_sale_return_detail.unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code where tspl_sd_sale_return_head.trans_type='CPR' and tspl_sd_sale_return_head.Status=1 and tspl_sd_sale_return_detail.foc_item=1 and tspl_sd_sale_return_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_return_head.csa_loc_code='" + txtCSA_Loc_Code.Value + "' " + strReturnCond + " " & _
                "union all " & _
                "select isnull(tspl_sd_sale_return_detail.adjustment_no,'') as adjustment_no,isnull(tspl_sd_sale_return_detail.transfer_no,'') as doc_code,isnull(tspl_sd_sale_return_detail.csa_salepatti_return_no,'') as salepatti_return_no,tspl_sd_sale_return_detail.item_code,(0-isnull(tspl_sd_sale_return_detail.org_transfer_qty,0)) as qty,tspl_sd_sale_return_detail.org_transfer_uom as unit_code from tspl_sd_sale_return_detail left outer join tspl_sd_sale_return_head on tspl_sd_sale_return_detail.document_code=tspl_sd_sale_return_head.document_code where tspl_sd_sale_return_head.trans_type='CSA' and tspl_sd_sale_return_detail.document_code<>'" + txtCode.Value + "' and tspl_sd_sale_return_detail.foc_item=1 and tspl_sd_sale_return_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_return_head.bill_to_location='" + txt_loc_code.Value + "' " & _
                "union all " & _
                "select TSPL_ADJUSTMENT_HEADER.adjustment_no,'' as doc_code,'' as salepatti_return_no,TSPL_ADJUSTMENT_DETAIL.item_code,TSPL_ADJUSTMENT_DETAIL.item_quantity as qty,TSPL_ADJUSTMENT_DETAIL.unit_code from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.adjustment_no=TSPL_ADJUSTMENT_DETAIL.adjustment_no where TSPL_ADJUSTMENT_HEADER.posted='Y' and (TSPL_ADJUSTMENT_HEADER.mainlocationcode='" + txtCSA_Loc_Code.Value + "' or TSPL_ADJUSTMENT_HEADER.loc_code='" + txtCSA_Loc_Code.Value + "') and isnull(TSPL_ADJUSTMENT_DETAIL.unit_cost,0)<=0 " & _
                "union all " & _
                "select isnull((select adjustment_no from tspl_adjustment_header where tspl_adjustment_header.adjustment_no=tspl_csa_sale_transfer_detail.against_transfer_code),'') as adjustment_no,isnull((select DOC_CODE from TSPL_CSA_TRANSFER_HEAD where TSPL_CSA_TRANSFER_HEAD.DOC_CODE=tspl_csa_sale_transfer_detail.against_transfer_code),'') as against_transfer_code,isnull((select DOCument_CODE from tspl_sd_sale_return_head where tspl_sd_sale_return_head.trans_type='CPR' " + strReturnCond + " and tspl_sd_sale_return_head.DOCument_CODE=tspl_csa_sale_transfer_detail.against_transfer_code),'') as salepatti_return_no,tspl_csa_sale_transfer_detail.item_code,sum(0-isnull(tspl_csa_sale_transfer_detail.alt_qty,0)) as qty,tspl_csa_sale_transfer_detail.transfer_uom from tspl_csa_sale_transfer_detail left outer join TSPL_SD_SALE_INVOICE_DETAIL on TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_CSA_SALE_TRANSFER_DETAIL.DOCUMENT_CODE and TSPL_SD_SALE_INVOICE_DETAIL.Line_No=TSPL_CSA_SALE_TRANSFER_DETAIL.Line_No and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code=TSPL_CSA_SALE_TRANSFER_DETAIL.Item_Code where tspl_csa_sale_transfer_detail.foc='Y' and tspl_csa_sale_transfer_detail.document_code in (select document_code from tspl_sd_sale_invoice_head where tspl_sd_sale_invoice_head.trans_type='CSA' and tspl_sd_sale_invoice_head.customer_code='" + txtcustcode.Value + "' and tspl_sd_sale_invoice_head.CSA_PLANT_LOCATION='" + txt_loc_code.Value + "') " & _
                " group by tspl_csa_sale_transfer_detail.against_transfer_code,tspl_csa_sale_transfer_detail.item_code,tspl_csa_sale_transfer_detail.Transfer_UOM)sub group by sub.adjustment_no,sub.DOC_CODE,sub.salepatti_return_no,sub.Item_Code,sub.Unit_code " & _
                ")a left outer join (select doc_code,item_code,sum(qty) as qty,unit_code,(sum(transfer_rate)/count(item_code)) as transfer_rate from TSPL_CSA_TRANSFER_DETAIL where isnull(FOC,'N')='Y' group by doc_code,item_code,Unit_code) csa_trans on csa_trans.doc_code=a.doc_code and csa_trans.item_code=a.item_code left outer join tspl_csa_transfer_head csa_trans_head on csa_trans_head.doc_code=a.doc_code " & _
                " left outer join (select document_code,item_code,sum(qty) as qty,unit_code,(sum(item_cost)/count(item_code)) as transfer_rate from tspl_sd_sale_return_detail where isnull(FOC_item,0)=1 group by document_code,item_code,Unit_code) csa_salepatti_return on csa_salepatti_return.document_code=a.salepatti_return_no and csa_salepatti_return.item_code=a.item_code left outer join tspl_sd_sale_return_head csa_salepatti_return_head on csa_salepatti_return_head.document_code=a.salepatti_return_no and csa_salepatti_return.document_code=csa_salepatti_return_head.document_code and csa_salepatti_return_head.trans_type='CPR' " + strReturnCond2 + " " & _
                " where a.item_code='" + iCode + "' "
                If clsCommon.myLen(TransferNo) > 0 Then
                    qry += " and a.doc_code= '" + TransferNo + "' "
                End If
                If clsCommon.myLen(CSA_SalePatti_Return_No) > 0 Then
                    qry += " and a.salepatti_return_no='" + CSA_SalePatti_Return_No + "' "
                End If
                qry += " group by a.doc_code,a.salepatti_return_no,csa_trans_head.Description,a.item_code,a.unit_code,csa_trans.transfer_rate "
            End If

            Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
#End Region

    Private Sub gv_KeyDown(sender As Object, e As KeyEventArgs) Handles gv.KeyDown
        If e.KeyCode = Keys.F5 Then
            If gv.CurrentRow.Tag Is Nothing OrElse clsCommon.myLen(gv.CurrentRow.Tag) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select transfer detail first.", Me.Text)
                Exit Sub
            End If
            OpenBatchItem()
        End If
    End Sub

    Private Sub fndGateReturnNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndGateReturnNo._MYValidating
        fndGateReturnNo.Value = clsGateEntryReturnCSA.getFinder("Posted=1", fndGateReturnNo.Value, isButtonClicked)
    End Sub

    Private Sub txtTaxGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTaxGroup._MYValidating
        Try
            If clsCommon.myLen(txt_loc_code.Value) <= 0 Then
                txt_loc_code.Focus()
                Throw New Exception("Please select location ")
            End If
            txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txt_loc_code.Value, txtcustcode.Value, "S", txtTaxGroup.Value, isButtonClicked, OpenALLTaxes)
            SetTaxDetails()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtcustcode.Value, txt_loc_code.Value, OpenALLTaxes)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 10) Then
                MessageBox.Show("Can't Handle More than 10 Tax Types in a Group")
                Return
            End If
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            For Each dr As DataRow In dt.Rows
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
            Next
        Else
            lblTaxGrpName.Text = ""
        End If
    End Sub

    Sub LoadBlankGridTax()
        gv2.Rows.Clear()
        gv2.Columns.Clear()

        Dim repoTaxAuthCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthCode.FormatString = ""
        repoTaxAuthCode.HeaderText = "Tax Authority Code"
        repoTaxAuthCode.Name = colTTaxAutCode
        repoTaxAuthCode.Width = 150
        repoTaxAuthCode.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthCode)

        Dim repoTaxAuthName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxAuthName.FormatString = ""
        repoTaxAuthName.HeaderText = "Tax Authority"
        repoTaxAuthName.Name = colTTaxAutName
        repoTaxAuthName.Width = 200
        repoTaxAuthName.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(repoTaxAuthName)

        Dim repoTaxBaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt.FormatString = ""
        repoTaxBaseAmt.HeaderText = "Base Amount"
        repoTaxBaseAmt.Name = colTBaseAmt
        repoTaxBaseAmt.Width = 100
        repoTaxBaseAmt.ReadOnly = True
        repoTaxBaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxBaseAmt)

        Dim repoTaxRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate.FormatString = ""
        repoTaxRate.HeaderText = "Tax Rate"
        repoTaxRate.Name = colTTaxRate
        repoTaxRate.Width = 100
        repoTaxRate.IsVisible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTaxRateColumnOnTransaction, clsFixedParameterCode.ShowTaxRateColumnOnTransaction, Nothing)) = 1, True, False)
        repoTaxRate.ReadOnly = True
        repoTaxRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxRate)

        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.FormatString = ""
        repoTaxAmt.HeaderText = "Tax Amount"
        repoTaxAmt.Name = colTTaxAmt
        repoTaxAmt.Width = 100
        repoTaxAmt.ReadOnly = True
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAmt)

        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub SetitemWiseTaxOnlySetting(ByVal intRowNo As Integer)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtcustcode.Value, txt_loc_code.Value, OpenALLTaxes)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim ii As Integer = 1
            For Each dr As DataRow In dt.Rows
                Dim strII As String = clsCommon.myCstr(ii)
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                ii = ii + 1
            Next
        End If
    End Sub

    Private Function GetCurrentRowSurTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Else
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            End If
        Next
        Return 0
    End Function

    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function

    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv.CurrentRow.Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            Else
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            End If
        Next
        Return dblTotTax
    End Function

    Sub UpdateCurrentRow(ByVal intRowNo As Integer)
        Dim dclAmt As Decimal = clsCommon.myCdbl(gv.Rows(intRowNo).Cells(colAmount).Value)
        Dim dblTaxAmt As Decimal
        Dim arrTaxableAuth As New List(Of String)
        For ii As Integer = 1 To 10
            Dim Strii As String = clsCommon.myCstr(ii)
            Dim strTaxCode As String = clsCommon.myCstr(gv.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
            If clsCommon.myLen(strTaxCode) > 0 Then
                Dim dblTaxRate As Double = clsCommon.myCdbl(gv.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                Dim IsSurTax As Boolean = clsCommon.myCBool(gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                Dim strSurTaxCode As String = clsCommon.myCstr(gv.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                Dim IsTaxable As Boolean = clsCommon.myCBool(gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                Dim IsExcisable As Boolean = clsCommon.myCBool(gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                Dim dblBaseAmt As Double = 0
                If IsSurTax Then
                    Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(intRowNo, ii, strSurTaxCode)
                    dblBaseAmt = dblSurTaxAmt
                Else
                    Dim dblOtherTaxAmt As Double = 0
                    dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(intRowNo, Strii, arrTaxableAuth)
                    dblBaseAmt = dclAmt + dblOtherTaxAmt
                End If
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 2)
                If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                    arrTaxableAuth.Add(strTaxCode.ToUpper())
                End If
            Else
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
                gv.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
            End If
        Next

        Dim dblTotalTaxAmt As Decimal = GetCurrentRowTotalTaxAmt(intRowNo)
        Dim dblAmtAfterTax As Decimal = dblTotalTaxAmt + dclAmt

        gv.Rows(intRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotalTaxAmt, 2)
        gv.Rows(intRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)

        UpdateAllTotals()
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0

        Dim dblTaxBaseAmt1 As Double = 0
        Dim dblTaxBaseAmt2 As Double = 0
        Dim dblTaxBaseAmt3 As Double = 0
        Dim dblTaxBaseAmt4 As Double = 0
        Dim dblTaxBaseAmt5 As Double = 0
        Dim dblTaxBaseAmt6 As Double = 0
        Dim dblTaxBaseAmt7 As Double = 0
        Dim dblTaxBaseAmt8 As Double = 0
        Dim dblTaxBaseAmt9 As Double = 0
        Dim dblTaxBaseAmt10 As Double = 0
        Dim dblTotalWt As Double = 0

        Dim dblTaxAmt1 As Double = 0
        Dim dblTaxAmt2 As Double = 0
        Dim dblTaxAmt3 As Double = 0
        Dim dblTaxAmt4 As Double = 0
        Dim dblTaxAmt5 As Double = 0
        Dim dblTaxAmt6 As Double = 0
        Dim dblTaxAmt7 As Double = 0
        Dim dblTaxAmt8 As Double = 0
        Dim dblTaxAmt9 As Double = 0
        Dim dblTaxAmt10 As Double = 0

        Dim TotalCommitionCharges As Double = 0
        Dim TotalOtherCharges As Double = 0
        Dim TotalTransferCharges As Double = 0

        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv.Rows.Count - 1
            If (clsCommon.myLen(gv.Rows(ii).Cells(colQty).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv.Rows(ii).Cells(colAmount).Value)

                dblTaxAmt1 = dblTaxAmt1 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt1).Value)
                dblTaxAmt2 = dblTaxAmt2 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt2).Value)
                dblTaxAmt3 = dblTaxAmt3 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt3).Value)
                dblTaxAmt4 = dblTaxAmt4 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt4).Value)
                dblTaxAmt5 = dblTaxAmt5 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt5).Value)
                dblTaxAmt6 = dblTaxAmt6 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt6).Value)
                dblTaxAmt7 = dblTaxAmt7 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt7).Value)
                dblTaxAmt8 = dblTaxAmt8 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt8).Value)
                dblTaxAmt9 = dblTaxAmt9 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt9).Value)
                dblTaxAmt10 = dblTaxAmt10 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxAmt10).Value)

                dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt1).Value)
                dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt2).Value)
                dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt3).Value)
                dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt4).Value)
                dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt5).Value)
                dblTaxBaseAmt6 = dblTaxBaseAmt6 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt6).Value)
                dblTaxBaseAmt7 = dblTaxBaseAmt7 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt7).Value)
                dblTaxBaseAmt8 = dblTaxBaseAmt8 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt8).Value)
                dblTaxBaseAmt9 = dblTaxBaseAmt9 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt9).Value)
                dblTaxBaseAmt10 = dblTaxBaseAmt10 + clsCommon.myCdbl(gv.Rows(ii).Cells(colTaxBaseAmt10).Value)

                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv.Rows(ii).Cells(colTotTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv.Rows(ii).Cells(colAmtAfterTax).Value)
            End If
        Next


        For ii As Integer = 1 To gv2.Rows.Count
            Select Case (ii)
                Case 1
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                    If dblTaxBaseAmt1 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 2
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                    If dblTaxBaseAmt2 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 3
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                    If dblTaxBaseAmt3 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 4
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                    If dblTaxBaseAmt4 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 5
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                    If dblTaxBaseAmt5 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 6
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt6, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt6, 2)
                    If dblTaxBaseAmt6 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 7
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                    If dblTaxBaseAmt7 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 8
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                    If dblTaxBaseAmt8 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 9
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                    If dblTaxBaseAmt9 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
                Case 10
                    gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                    gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                    If dblTaxBaseAmt10 <> 0 Then
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 2)
                    Else
                        gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                    End If
            End Select
        Next
    End Sub
    Private Sub fndGateEntryNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndGateEntryNo._MYValidating
        Dim qry As String = Nothing
        Dim WhrCls As String = Nothing
        WhrCls = " Doc_Type = 'CSATRAN' and POSTED = 1  and Gate_Entry_No not in ( select case when TSPL_SD_SALE_RETURN_HEAD.Gate_Entry_No is Null then '' else TSPL_SD_SALE_RETURN_HEAD.Gate_Entry_No end  from TSPL_SD_SALE_RETURN_HEAD ) "  ' where  isnull(TSPL_SD_SALE_RETURN_HEAD.Gate_Entry_No,'')<>''  union all select Gate_Entry_No from TSPL_Sale_Return_Gate_Entry_Invoice_Wise) 
        If clsCommon.myLen(txtcustcode.Value) > 0 Then
            WhrCls = WhrCls + " and TSPL_Sale_Return_Gate_Entry_Head.Customer_Code = '" + txtcustcode.Value + "'  "
        End If
        If clsCommon.myLen(txt_loc_code.Value) > 0 Then
            WhrCls = WhrCls + " and TSPL_Sale_Return_Gate_Entry_Head.Location_Code= '" + txt_loc_code.Value + "' "
        End If
        WhrCls = WhrCls + " and  TSPL_Sale_Return_Gate_Entry_Head.isCancel=0 "
        qry = "select Gate_Entry_No as [Gate Entry No] , Gate_Entry_Date as [Gate Entry Date]  from TSPL_Sale_Return_Gate_Entry_Head  "
        fndGateEntryNo.Value = clsCommon.ShowSelectForm("GATE@ENTRYNO", qry, "Gate Entry No", WhrCls, fndGateEntryNo.Value, "", isButtonClicked)
    End Sub
    Public Sub FillGateEntryNo(ByVal strMultipleTransferNo As String)
        Dim qry As String = Nothing
        If clsCommon.myLen(strMultipleTransferNo) > 0 Then
            ' Ticket No : KDI/13/06/18-000361
            qry = " select TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Gate_Entry_No from TSPL_Sale_Return_Gate_Entry_Invoice_Wise left join TSPL_Sale_Return_Gate_Entry_Head on TSPL_Sale_Return_Gate_Entry_Head.Gate_Entry_No=TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Gate_Entry_No where Invoice_No in (" & strMultipleTransferNo & ") and  POSTED = 1 and  TSPL_Sale_Return_Gate_Entry_Head.isCancel=0 "
            fndGateEntryNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
                fndGateEntryNo.Enabled = False
            Else
                fndGateEntryNo.Enabled = True
            End If
        End If


    End Sub


    Public Function isGateEntryLocAndSaleRetrunLocSame(ByVal strLocation As String, ByVal strGateEntryNo As String) As Boolean
        Dim isSameLoc As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Sale_Return_Gate_Entry_Head where TSPL_Sale_Return_Gate_Entry_Head.Location_Code= '" + strLocation + "'and TSPL_Sale_Return_Gate_Entry_Head.Gate_entry_no = '" + strGateEntryNo + "'"))
        Return isSameLoc
    End Function
    ' Ticket No  KDI/02/05/18-000286  By Prabhakar
    Public Function isCancelGateEntry(ByVal strGateEntryNo As String) As Boolean
        Dim isCancel As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select count(*) from TSPL_Sale_Return_Gate_Entry_Head where TSPL_Sale_Return_Gate_Entry_Head.isCancel=1  and TSPL_Sale_Return_Gate_Entry_Head.Gate_entry_no = '" + strGateEntryNo + "' "))
        Return isCancel
    End Function
End Class