'--------Created By Richa 29/01/2015 Against Ticket No BM00000005036
'' changes by richa agarwal against ticket no BM00000006236 on 16/04/2015,BM00000006385
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmDocumentAcceptance
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim curRow As Integer = -1
    Dim curCol As Integer = -1
    Dim Qry As String
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isFlag As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colType As String = "colType"
    Public Const colUnitCode As String = "colUnitCode"
    Public Const colParticularsofGoods As String = "colParticularsofGoods"
    Public Const colQty As String = "colQty"
    Public Const colLCQty As String = "colLCQty"
    Public Const colItemCode As String = "colItemCode"
    Public Const colItemDesc As String = "colItemDesc"
    Public Const colPONo As String = "colPONo"
    Public Const colLCRequestNo As String = "colLCRequestNo"
    Public Const colCurrency As String = "colCurrency"
    Public Const colRate As String = "colRate"
    Public Const colAmount As String = "colAmount"
    Dim isLoadInsideData As Boolean = False
    Dim isCellValueChangedOpen As Boolean

#End Region

#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmDocumentAcceptance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub FrmDocumentAcceptance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtdocumentAcceptancedate.Value, Nothing) = False Then
            txtdocumentAcceptancedate.Select()
            Return False
        End If
        Dim LCCreditValue As Double = 0
        RadPageView1.SelectedPage = RadPageViewPage1
        If clsCommon.myLen(fndLCCreationNo.Value) <= 0 Then
            fndLCCreationNo.Focus()
            Throw New Exception("LC No cannot be left blank")
        End If
        If clsCommon.myCDate(clsCommon.GetPrintDate(txtdocumentAcceptancedate.Value, "dd/MM/yyyy")) > clsCommon.myCDate(TxtDueDate.Value) Then
            TxtDueDate.Focus()
            Throw New Exception("Due Date cannot be less than from Document Acceptance Date")
        End If
        If Not ChkAcceptanceLetter.Checked Then
            ChkAcceptanceLetter.Focus()
            Throw New Exception("Acceptance letter should be checked")
        End If
        If Not ChkA2.Checked Then
            ChkA2.Focus()
            Throw New Exception("F2 should be checked")
        End If
        If Not chkTrustReceipt.Checked Then
            chkTrustReceipt.Focus()
            Throw New Exception("Trust receipt should be checked")
        End If
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_DOCUMENT_ACCEPTANCE_MT where AcceptanceReferenceNo='" & TxtAcceptanceReferenceNo.Text & "' and DocumentAcceptanceNo <> '" & fndDocumentAcceptance.Value & "' "))
        If count <> 0 Then
            RadPageView1.SelectedPage = RadPageViewPage3
            TxtAcceptanceReferenceNo.Focus()
            Throw New Exception("Acceptance Ref. No exist in another transaction please enter another reference no")
        End If
        'If clsCommon.myLen(FndPurchaseOrderNo.Value) <= 0 Then
        '    FndPurchaseOrderNo.Focus()
        '    Throw New Exception("Purchase Order No cannot be Zero or blank")
        'End If

        'If clsCommon.myCdbl(TxtLCAmount.Value) < 0 Then
        '    TxtLCAmount.Focus()
        '    Throw New Exception("LC Amount cannot be negative")
        'End If

        'If clsCommon.myCdbl(TxtLCAmount.Value) = 0 Then
        '    TxtLCAmount.Focus()
        '    Throw New Exception("LC Amount cannot be zero")
        'End If

        'LCCreditValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select LCCreditLimit from TSPL_BANK_master where BANK_CODE ='" & fndBankCode.Value & "'"))
        'If clsCommon.myCdbl(TxtLCAmount.Value) > LCCreditValue Then
        '    TxtLCAmount.Focus()
        '    Throw New Exception("LC Amount cannot be greater than " & LCCreditValue & "")
        'End If
        'If clsCommon.myCdbl(TxtFDPer.Value) < 0 Then
        '    TxtFDPer.Focus()
        '    Throw New Exception("FD % cannot be negative")
        'End If
        'If clsCommon.myCdbl(TxtLCPeriod.Value) < 0 Then
        '    TxtLCPeriod.Focus()
        '    Throw New Exception("LC Period cannot be negative")
        'End If
        'If clsCommon.myCdbl(TxtLCPeriod.Value) = 0 Then
        '    TxtLCPeriod.Focus()
        '    Throw New Exception("LC Period cannot be zero")
        'End If
        'If clsCommon.myCdbl(TxtFDPeriod.Value) < 0 Then
        '    TxtFDPeriod.Focus()
        '    Throw New Exception("FD Period cannot be negative")
        'End If
        'If clsCommon.myCdbl(TxtFDPer.Value) = 0 Then
        '    TxtFDPer.Focus()
        '    Throw New Exception("FD % cannot be zero")
        'End If
        For i As Integer = 0 To gv1.Rows.Count - 1

            If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) < 0 Then
                Throw New Exception("Qty cannot be negative")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) = 0 Then
                Throw New Exception("Qty cannot be left blank")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) > clsCommon.myCdbl(gv1.Rows(i).Cells(colLCQty).Value) Then
                Throw New Exception("Qty cannot be greater than LC qty")
            End If
        Next
        'If clsCommon.myLen(txtCurrencyCode.Value) <= 0 Then
        '    txtCurrencyCode.Focus()
        '    Throw New Exception("Currency Code cannot be Zero or blank")
        'End If
        'If clsCommon.myCdbl(txtConversionRate.Value) = 0 Then
        '    txtConversionRate.Focus()
        '    Throw New Exception("Conversion Rate cannot be zero")
        'End If
        'If clsCommon.myCdbl(txtConversionRate.Value) < 0 Then
        '    txtConversionRate.Focus()
        '    Throw New Exception("Conversion Rate cannot be negative")
        'End If
        Return True
    End Function
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsDocumentAcceptance.DeleteData(fndDocumentAcceptance.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data deleted successfully")
                    Reset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        fndDocumentAcceptance.Value = ""
        fndLCCreationNo.Value = ""
        ' LblBankName.Text = ""
        FndVendor.Value = ""
        lblvendor.Text = ""
        TxtLCAmount.Value = 0
        LoadShipmentType()
        cboShipment.SelectedValue = "P"
        TxtAcceptanceReferenceNo.Text = ""
        TxtLocationCode.Text = ""
        lblLocationDesc.Text = ""
        lblTotRAmt.Text = ""
        LblPurchaseOrder.Text = ""
        TxtPurchaseInvoiceNo.Text = ""
        ChkAcceptanceLetter.Checked = True
        ChkA2.Checked = True
        chkTrustReceipt.Checked = True
        RadPageView1.SelectedPage = RadPageViewPage1
        UcAttachment1.BlankAllControls()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtdocumentAcceptancedate.Value = clsCommon.GETSERVERDATE()
        TxtDueDate.Value = clsCommon.GETSERVERDATE()
        fndDocumentAcceptance.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnsave.Enabled = True
        isNewEntry = True
        loadBlankItemGrid()
        ReStoreGridLayout()
        isLoadInsideData = True
    End Sub
    Sub LoadShipmentType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("P", "Partial")
        dt.Rows.Add("F", "Full")

        cboShipment.DataSource = dt
        cboShipment.ValueMember = "Code"
        cboShipment.DisplayMember = "Name"
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDocumentAcceptance)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnprint.Visible = MyBase.isPrintFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New ClsDocumentAcceptance()
                obj.DocumentAcceptanceNo = fndDocumentAcceptance.Value
                obj.DocumentAcceptance_Date = txtdocumentAcceptancedate.Value
                obj.LCCreationNo = clsCommon.myCstr(fndLCCreationNo.Value)
                obj.VendorCode = clsCommon.myCstr(FndVendor.Value)
                obj.VendorName = clsCommon.myCstr(lblvendor.Text)
                obj.Location_Code = clsCommon.myCstr(TxtLocationCode.Text)
                obj.Location_Desc = clsCommon.myCstr(lblLocationDesc.Text)
                obj.LCAmount = clsCommon.myCdbl(TxtLCAmount.Value)
                obj.DueDate = clsCommon.myCDate(TxtDueDate.Value)
                obj.ReferenceNo = clsCommon.myCstr(TXTReferenceNo.Text)
                obj.ShipmentType = clsCommon.myCstr(cboShipment.SelectedValue)
                obj.AcceptanceReferenceNo = clsCommon.myCstr(TxtAcceptanceReferenceNo.Text)
                If clsCommon.myLen(LblPurchaseOrder.Text) > 0 Then
                    obj.PurchaseOrder_No = clsCommon.myCstr(LblPurchaseOrder.Text)
                    If clsCommon.myLen(TxtPurchaseInvoiceNo.Text) > 0 Then
                        obj.PurchaseInvoice_No = clsCommon.myCstr(TxtPurchaseInvoiceNo.Text)
                    End If
                Else
                    obj.PurchaseInvoice_No = clsCommon.myCstr(TxtPurchaseInvoiceNo.Text)
                End If

                obj.DocumentAmount = clsCommon.myCdbl(lblTotRAmt.Text)
                If ChkAcceptanceLetter.Checked Then
                    obj.AcceptanceLetter = 1
                Else
                    obj.AcceptanceLetter = 0
                End If
                If ChkA2.Checked Then
                    obj.F2Letter = 1
                Else
                    obj.F2Letter = 0
                End If
                If chkTrustReceipt.Checked Then
                    obj.TrustReceiptLetter = 1
                Else
                    obj.TrustReceiptLetter = 0
                End If

                ''richa agarwal 11/10/2014
                obj.Is_Create_Auto_GRN = 0
                obj.Is_Create_Auto_MRN = 0
                obj.Is_Create_Auto_SRN = 1
                Dim settingonsale As Integer = clsFixedParameter.GetData(clsFixedParameterType.AllowAutoMRNGRNonDocumentAcceptance, clsFixedParameterCode.AllowAutoMRNGRNonDocumentAcceptance, Nothing)
                If settingonsale = 1 Then
                    '' richa agarwal against ticket no.  BM00000006551
                    'If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ShowMRN, clsFixedParameterCode.ShowMRN, Nothing), 1) = CompairStringResult.Equal And clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing), 1) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.SkipMRNGRNinCaseofMT, clsFixedParameterCode.SkipMRNGRNinCaseofMT, Nothing), 0) = CompairStringResult.Equal And clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ShowMRN, clsFixedParameterCode.ShowMRN, Nothing), 1) = CompairStringResult.Equal And clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.ShowGRN, clsFixedParameterCode.ShowGRN, Nothing), 1) = CompairStringResult.Equal Then
                        obj.Is_Create_Auto_GRN = 1
                        obj.Is_Create_Auto_MRN = 1
                    End If
                End If
                ''=====================
                Dim objTr As New ClsDocumentAcceptanceDetail
                obj.arrDocumentAcceptanceDetail = New List(Of ClsDocumentAcceptanceDetail)


                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New ClsDocumentAcceptanceDetail()
                    objTr.DocumentAcceptanceNo = clsCommon.myCstr(obj.DocumentAcceptanceNo)
                    objTr.LCRequestNo = clsCommon.myCstr(grow.Cells(colLCRequestNo).Value)
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                    If clsCommon.myLen(clsCommon.myCstr(LblPurchaseOrder.Text)) > 0 Then
                        objTr.PurchaseOrder_No = clsCommon.myCstr(grow.Cells(colPONo).Value)
                    Else
                        objTr.PurchaseInvoice_No = clsCommon.myCstr(grow.Cells(colPONo).Value)
                    End If
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                    objTr.CURRENCY_CODE = clsCommon.myCstr(grow.Cells(colCurrency).Value)
                    objTr.ParticularsofGoodsLCNo = clsCommon.myCstr(grow.Cells(colParticularsofGoods).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                    obj.arrDocumentAcceptanceDetail.Add(objTr)
                Next



                If (ClsDocumentAcceptance.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                    End If
                    LoadData(obj.DocumentAcceptanceNo, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsDocumentAcceptance = ClsDocumentAcceptance.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            loadBlankItemGrid()
            fndDocumentAcceptance.Value = obj.DocumentAcceptanceNo
            txtdocumentAcceptancedate.Value = obj.DocumentAcceptance_Date
            fndLCCreationNo.Value = obj.LCCreationNo

            FndVendor.Value = obj.VendorCode
            lblvendor.Text = obj.VendorName
            TxtLocationCode.Text = obj.Location_Code
            lblLocationDesc.Text = obj.Location_Desc
            TxtLCAmount.Value = obj.LCAmount
            TxtDueDate.Value = obj.DueDate
            TXTReferenceNo.Text = obj.ReferenceNo
            TxtAcceptanceReferenceNo.Text = obj.AcceptanceReferenceNo
            LblPurchaseOrder.Text = obj.PurchaseOrder_No
            TxtPurchaseInvoiceNo.Text = obj.PurchaseInvoice_No
            lblTotRAmt.Text = obj.DocumentAmount
            If clsCommon.CompairString(obj.TrustReceiptLetter, 1) = CompairStringResult.Equal Then
                chkTrustReceipt.Checked = True
            Else
                chkTrustReceipt.Checked = False
            End If
            If clsCommon.CompairString(obj.F2Letter, 1) = CompairStringResult.Equal Then
                ChkA2.Checked = True
            Else
                ChkA2.Checked = False
            End If
            If clsCommon.CompairString(obj.AcceptanceLetter, 1) = CompairStringResult.Equal Then
                ChkAcceptanceLetter.Checked = True
            Else
                ChkAcceptanceLetter.Checked = False
            End If
            If clsCommon.CompairString(obj.ShipmentType, "P") = CompairStringResult.Equal Then
                cboShipment.SelectedValue = "P"
            Else
                cboShipment.SelectedValue = "F"

            End If
            fndDocumentAcceptance.MyReadOnly = True

            If obj.arrDocumentAcceptanceDetail IsNot Nothing AndAlso obj.arrDocumentAcceptanceDetail.Count > 0 Then
                For Each objTr As ClsDocumentAcceptanceDetail In obj.arrDocumentAcceptanceDetail
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLCRequestNo).Value = objTr.LCRequestNo
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colParticularsofGoods).Value = objTr.ParticularsofGoodsLCNo
                    If clsCommon.myLen(clsCommon.myCstr(objTr.PurchaseOrder_No)) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objTr.PurchaseOrder_No
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objTr.PurchaseInvoice_No
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = objTr.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = objTr.Unit_code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrency).Value = objTr.CURRENCY_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
                    gv1.Rows.AddNew()
                Next
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                GetBalanceQty()
            Else
                gv1.DataSource = Nothing
            End If



            btnsave.Text = "Update"

            If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnPost.Enabled = True
                btnsave.Enabled = True
                btndelete.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        Else
            Reset()
        End If
    End Sub
    Sub PostData()
        Try
            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                If (ClsDocumentAcceptance.PostData(MyBase.Form_ID, fndDocumentAcceptance.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully posted")
                    LoadData(fndDocumentAcceptance.Value, NavigatorType.Current)
                End If
            End If
            isFlag = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub
    Sub loadBlankItemGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "SL. No."
        lineNo.Name = colSlNo
        lineNo.Width = 60
        lineNo.ReadOnly = True
        lineNo.WrapText = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(lineNo)

        Dim LCRequestNo As New GridViewTextBoxColumn()
        LCRequestNo.FormatString = ""
        LCRequestNo.HeaderText = "LC Request No"
        LCRequestNo.Name = colLCRequestNo
        LCRequestNo.Width = 100
        LCRequestNo.ReadOnly = True
        LCRequestNo.WrapText = True
        LCRequestNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(LCRequestNo)

        Dim PurchaseOrderNo As New GridViewTextBoxColumn()
        PurchaseOrderNo.FormatString = ""
        PurchaseOrderNo.HeaderText = "PO No"
        PurchaseOrderNo.Name = colPONo
        PurchaseOrderNo.Width = 100
        PurchaseOrderNo.ReadOnly = True
        PurchaseOrderNo.WrapText = True
        PurchaseOrderNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(PurchaseOrderNo)

        Dim ParticularsofGoods As New GridViewTextBoxColumn()
        ParticularsofGoods.FormatString = ""
        ParticularsofGoods.HeaderText = "Particulars of Goods or Documents of Title to Goods"
        ParticularsofGoods.Name = colParticularsofGoods
        ParticularsofGoods.Width = 250
        ParticularsofGoods.ReadOnly = True
        ParticularsofGoods.WrapText = True
        ParticularsofGoods.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ParticularsofGoods)

        Dim itemCode As New GridViewTextBoxColumn()
        itemCode.FormatString = ""
        itemCode.HeaderText = "Item Code"
        itemCode.Name = colItemCode
        itemCode.Width = 100
        itemCode.ReadOnly = True
        itemCode.WrapText = True
        itemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(itemCode)


        Dim itemDesc As New GridViewTextBoxColumn()
        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Description of goods"
        itemDesc.Name = colItemDesc
        itemDesc.Width = 320
        itemDesc.ReadOnly = True
        itemDesc.WrapText = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(itemDesc)

        Dim strUnitCode As New GridViewTextBoxColumn()
        strUnitCode.FormatString = ""
        strUnitCode.HeaderText = "UOM"
        strUnitCode.Name = colUnitCode
        strUnitCode.Width = 120
        strUnitCode.ReadOnly = True
        strUnitCode.WrapText = True
        strUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(strUnitCode)

        Dim POQty As New GridViewDecimalColumn
        POQty.FormatString = "{0:n3}"
        POQty.HeaderText = "LCQty"
        POQty.DecimalPlaces = 3
        POQty.Name = colLCQty
        POQty.Width = 120
        POQty.ReadOnly = True
        POQty.WrapText = True
        POQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(POQty)

        Dim Qty As New GridViewDecimalColumn
        Qty.FormatString = "{0:n3}"
        Qty.HeaderText = "Qty"
        Qty.DecimalPlaces = 3
        Qty.Name = colQty
        Qty.Width = 120
        Qty.ReadOnly = False
        Qty.WrapText = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Qty)

        Dim Currency As New GridViewTextBoxColumn
        Currency.FormatString = ""
        Currency.HeaderText = "Currency"
        ' Currency.DecimalPlaces = 3
        Currency.Name = colCurrency
        Currency.Width = 120
        Currency.ReadOnly = True
        Currency.WrapText = True
        Currency.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Currency)

        Dim Rate As New GridViewDecimalColumn
        Rate.FormatString = "{0:n3}"
        Rate.HeaderText = "Rate"
        Rate.DecimalPlaces = 3
        Rate.Name = colRate
        Rate.Width = 120
        Rate.ReadOnly = True
        Rate.WrapText = True
        Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Rate)

        Dim Amount As New GridViewDecimalColumn
        Amount.FormatString = "{0:n2}"
        Amount.HeaderText = "Amount"
        Amount.DecimalPlaces = 2
        Amount.Name = colAmount
        Amount.Width = 120
        Amount.ReadOnly = True
        Amount.WrapText = True
        Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Amount)

        gv1.Rows.AddNew()
        gv1.Rows(0).Cells(colSlNo).Value = "1"
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnReorder = True
        ' gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        ' gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

    End Sub



    Private Sub fndLCCreationNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndLCCreationNo._MYValidating

        Dim qry As String = " Select Distinct BB.LCCreationNo,BB.LCRequestNo,BB.PurchaseOrder_No,BB.PurchaseInvoice_No as ProformaInvoiceNo,BB.BenefecieryCode as [Vendor Code],BB.BenefecieryName as [Vendor Name] , BB.Location_Code as [Location Code],BB.Location_Desc as [Location Desc] from (Select MAx(LCCreationNo)as LCCreationNo,Max(BenefecieryCode ) as BenefecieryCode,max(BenefecieryName) as BenefecieryName,MAX(Location_Code) as Location_Code ,MAX(Location_Desc) as Location_Desc,SUM(LCAmount) as LCAmount,MAX(LCRequestNo) as LCRequestNo,MAX(Item_Code) as [Item Code],MAX(Item_Desc) as [Item Desc],MAX(Unit_code) as [Unit Code],SUM(Qty* case when RI=1 then 1 else 0 end) as LCQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as AcceptanceQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty,MAX(PurchaseOrder_No) as PurchaseOrder_No,MAX(PurchaseInvoice_No) as PurchaseInvoice_No,SUM(Rate) as Rate,MAX(LCNo) as LCNo from ( "
        qry += " Select TSPL_LC_CREATION_MT.LCCreationNo,TSPL_LC_CREATION_MT.BenefecieryCode,TSPL_LC_CREATION_MT.BenefecieryName,TSPL_LC_CREATION_MT.Location_Code ,TSPL_LC_CREATION_MT.Location_Desc ,TSPL_LC_CREATION_MT.LCAmount ,TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,TSPL_LC_REQUEST_DETAIL_MT.Item_Code ,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc ,TSPL_LC_REQUEST_DETAIL_MT.Unit_code ,TSPL_LC_REQUEST_DETAIL_MT.Qty,0 as Unapproved,1 as RI,isnull(TSPL_LC_REQUEST_MT.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'') as  PurchaseInvoice_No,TSPL_LC_REQUEST_DETAIL_MT.Rate,TSPL_LC_CREATION_MT.LCNo   from TSPL_LC_CREATION_MT left outer Join TSPL_LC_REQUEST_MT on TSPL_LC_CREATION_MT.LCRequestNo=TSPL_LC_REQUEST_MT.LCRequestNo Left Outer Join TSPL_LC_REQUEST_DETAIL_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo where  TSPL_LC_CREATION_MT.Posted =1 "
        qry += " Union All "
        qry += " Select TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo  as LCCreationNo,'' as BenefecieryCode,'' as BenefecieryName,'' as Location_Code ,'' as Location_Desc,0 as LCAmount,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.LCRequestNo ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Desc ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code ,0 as Qty,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Unapproved ,-1 as RI ,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseInvoice_No,'') as  PurchaseInvoice_No,0 as Rate,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.ParticularsofGoodsLCNo as LCNo from TSPL_DOCUMENT_ACCEPTANCE_MT left outer join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo=TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  where TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=1 and len(isnull(TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo ,''))>0 "
        qry += " Union All"
        qry += " Select TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo  as LCCreationNo,'' as BenefecieryCode,'' as BenefecieryName,'' as Location_Code ,'' as Location_Desc,0 as LCAmount,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.LCRequestNo ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Desc ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code ,0 as Qty,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Unapproved,-1 as RI ,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseInvoice_No,'') as  PurchaseInvoice_No,0 as Rate,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.ParticularsofGoodsLCNo as LCNo from TSPL_DOCUMENT_ACCEPTANCE_MT left outer join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo=TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  where TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=0 and len(isnull(TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo ,''))>0 and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo not in('" & fndDocumentAcceptance.Value & "') "
        qry += " ) Final group by LCRequestNo  having (SUM(Qty *RI)-SUM(Unapproved)) >0)BB"

        fndLCCreationNo.Value = clsCommon.ShowSelectForm("formLCCreationNo", qry, "LCCreationNo", "", fndLCCreationNo.Value, "", isButtonClicked)
        If clsCommon.myLen(fndLCCreationNo.Value) > 0 Then
            FillLCCreationDetailintoGrid()
        Else
            FndVendor.Value = ""
            lblvendor.Text = ""
            TxtLocationCode.Text = ""
            lblLocationDesc.Text = ""
            LblPurchaseOrder.Text = ""
            TxtLCAmount.Value = 0
            loadBlankItemGrid()
        End If
    End Sub
    Sub FillLCCreationDetailintoGrid()
        If clsCommon.myLen(fndLCCreationNo.Value) > 0 Then
            loadBlankItemGrid()

            Dim qry As String = " Select BB.LCAmount,BB.CURRENCY_CODE,BB.LCNo,BB.[Unit Code] ,BB.LCCreationNo,BB.LCRequestNo,BB.PurchaseOrder_No,BB.PurchaseInvoice_No,BB.BenefecieryCode as [Vendor Code],BB.BenefecieryName as [Vendor Name] , BB.Location_Code as [Location Code],BB.Location_Desc as [Location Desc],BB.Line_No,BB.Line_No,BB.[Item Code] ,BB.[Item Desc] ,BB.Rate ,BB.PendingQty " & _
             " from (Select Max(CURRENCY_CODE) as CURRENCY_CODE,MAx(LCCreationNo)as LCCreationNo,Max(Line_No) as Line_No,Max(BenefecieryCode ) as BenefecieryCode,max(BenefecieryName) as BenefecieryName,MAX(Location_Code) as Location_Code ,MAX(Location_Desc) as Location_Desc,SUM(LCAmount) as LCAmount,MAX(LCRequestNo) as LCRequestNo,MAX(Item_Code) as [Item Code],MAX(Item_Desc) as [Item Desc],MAX(Unit_code) as [Unit Code],SUM(Qty* case when RI=1 then 1 else 0 end) as LCQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as AcceptanceQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty,MAX(PurchaseOrder_No) as PurchaseOrder_No,MAX(PurchaseInvoice_No) as PurchaseInvoice_No,SUM(Rate) as Rate,MAX(LCNo) as LCNo from (" & _
             " Select TSPL_LC_CREATION_MT.CURRENCY_CODE,TSPL_LC_CREATION_MT.LCCreationNo,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,TSPL_LC_CREATION_MT.BenefecieryCode,TSPL_LC_CREATION_MT.BenefecieryName,TSPL_LC_CREATION_MT.Location_Code ,TSPL_LC_CREATION_MT.Location_Desc ,TSPL_LC_CREATION_MT.LCAmount ,TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,TSPL_LC_REQUEST_DETAIL_MT.Item_Code ,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc ,TSPL_LC_REQUEST_DETAIL_MT.Unit_code ,TSPL_LC_REQUEST_DETAIL_MT.Qty,0 as Unapproved,1 as RI,isnull(TSPL_LC_REQUEST_MT.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'') as  PurchaseInvoice_No,TSPL_LC_REQUEST_DETAIL_MT.Rate,TSPL_LC_CREATION_MT.LCNo   from TSPL_LC_CREATION_MT left outer Join TSPL_LC_REQUEST_MT on TSPL_LC_CREATION_MT.LCRequestNo=TSPL_LC_REQUEST_MT.LCRequestNo Left Outer Join TSPL_LC_REQUEST_DETAIL_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo where  TSPL_LC_CREATION_MT.Posted =1 " & _
             " Union All " & _
             " Select '' as CURRENCY_CODE,TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo as LCCreationNo,0 as Line_No,'' as BenefecieryCode,'' as BenefecieryName,'' as Location_Code ,'' as Location_Desc,0 as LCAmount,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.LCRequestNo ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Desc ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code ,0 as Qty,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Unapproved,-1 as RI ,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseInvoice_No,'') as  PurchaseInvoice_No,0 as Rate,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.ParticularsofGoodsLCNo as LCNo from TSPL_DOCUMENT_ACCEPTANCE_MT left outer join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo=TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  where TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=1 and len(isnull(TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo ,''))>0 " & _
             " Union All " & _
            " Select '' as CURRENCY_CODE,TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo as LCCreationNo,0 as Line_No,'' as BenefecieryCode,'' as BenefecieryName,'' as Location_Code ,'' as Location_Desc,0 as LCAmount,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.LCRequestNo ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Desc ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code ,0 as Qty,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Unapproved,-1 as RI ,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseInvoice_No,'') as  PurchaseInvoice_No,0 as Rate,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.ParticularsofGoodsLCNo as LCNo from TSPL_DOCUMENT_ACCEPTANCE_MT left outer join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo=TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  where TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=0 and len(isnull(TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo ,''))>0 and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo not in('" + fndDocumentAcceptance.Value + "')" & _
             " ) Final Where Final.LCCreationNo ='" & fndLCCreationNo.Value & "'  group by LCRequestNo,Item_Code   having (SUM(Qty *RI)-SUM(Unapproved)) >0)BB"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FndVendor.Value = clsCommon.myCstr(dt.Rows(0)("Vendor Code"))
                lblvendor.Text = clsCommon.myCstr(dt.Rows(0)("Vendor Name"))
                TxtLocationCode.Text = clsCommon.myCstr(dt.Rows(0)("Location Code"))
                lblLocationDesc.Text = clsCommon.myCstr(dt.Rows(0)("Location Desc"))
                TxtLCAmount.Value = clsCommon.myCdbl(dt.Rows(0)("LCAmount"))
                LblPurchaseOrder.Text = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No"))
                TxtPurchaseInvoiceNo.Text = clsCommon.myCstr(dt.Rows(0)("PurchaseInvoice_No"))
                For Each dr As DataRow In dt.Rows
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = clsCommon.myCstr(dr("Line_No"))
                    If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No"))) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = clsCommon.myCstr(dr("PurchaseOrder_No"))
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = clsCommon.myCstr(dr("PurchaseInvoice_No"))
                        gv1.Columns(colPONo).HeaderText = "PI No"
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item Desc"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLCRequestNo).Value = clsCommon.myCstr(dr("LCRequestNo"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colParticularsofGoods).Value = clsCommon.myCstr(dr("LCNo"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr("Unit Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrency).Value = clsCommon.myCstr(dr("CURRENCY_CODE"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLCQty).Value = clsCommon.myCdbl(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = clsCommon.myCdbl(dr("PendingQty")) * clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows.AddNew()
                Next
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If
        End If
    End Sub
    Sub GetBalanceQty()
        'Dim qry As String = " Select BB.CURRENCY_CODE,BB.LCNo,BB.[Unit Code] ,BB.LCCreationNo,BB.LCRequestNo,BB.PurchaseOrder_No,BB.BenefecieryCode as [Vendor Code],BB.BenefecieryName as [Vendor Name] , BB.Location_Code as [Location Code],BB.Location_Desc as [Location Desc],BB.Line_No,BB.Line_No,BB.[Item Code] ,BB.[Item Desc] ,BB.Rate ,BB.PendingQty "
        'qry += " from (Select Max(CURRENCY_CODE) as CURRENCY_CODE,MAx(LCCreationNo)as LCCreationNo,Max(Line_No) as Line_No,Max(BenefecieryCode ) as BenefecieryCode,max(BenefecieryName) as BenefecieryName,MAX(Location_Code) as Location_Code ,MAX(Location_Desc) as Location_Desc,SUM(LCAmount) as LCAmount,MAX(LCRequestNo) as LCRequestNo,MAX(Item_Code) as [Item Code],MAX(Item_Desc) as [Item Desc],MAX(Unit_code) as [Unit Code],SUM(Qty* case when RI=1 then 1 else 0 end) as LCQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as AcceptanceQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty,MAX(PurchaseOrder_No) as PurchaseOrder_No,SUM(Rate) as Rate,MAX(LCNo) as LCNo from ("
        'qry += " Select TSPL_LC_CREATION_MT.CURRENCY_CODE,TSPL_LC_CREATION_MT.LCCreationNo,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,TSPL_LC_CREATION_MT.BenefecieryCode,TSPL_LC_CREATION_MT.BenefecieryName,TSPL_LC_CREATION_MT.Location_Code ,TSPL_LC_CREATION_MT.Location_Desc ,TSPL_LC_CREATION_MT.LCAmount ,TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,TSPL_LC_REQUEST_DETAIL_MT.Item_Code ,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc ,TSPL_LC_REQUEST_DETAIL_MT.Unit_code ,TSPL_LC_REQUEST_DETAIL_MT.Qty,0 as Unapproved,1 as RI,TSPL_LC_REQUEST_DETAIL_MT.PurchaseOrder_No ,TSPL_LC_REQUEST_DETAIL_MT.Rate,TSPL_LC_CREATION_MT.LCNo   from TSPL_LC_CREATION_MT left outer Join TSPL_LC_REQUEST_MT on TSPL_LC_CREATION_MT.LCRequestNo=TSPL_LC_REQUEST_MT.LCRequestNo Left Outer Join TSPL_LC_REQUEST_DETAIL_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo where  TSPL_LC_CREATION_MT.Posted =1 "
        'qry += " Union All "
        'qry += " Select '' as CURRENCY_CODE,TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo as LCCreationNo,0 as Line_No,'' as BenefecieryCode,'' as BenefecieryName,'' as Location_Code ,'' as Location_Desc,0 as LCAmount,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.LCRequestNo ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Desc ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code ,0 as Qty,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Unapproved,-1 as RI ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.PurchaseOrder_No,0 as Rate,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.ParticularsofGoodsLCNo as LCNo from TSPL_DOCUMENT_ACCEPTANCE_MT left outer join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo=TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  where TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=1 and len(isnull(TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo ,''))>0 "
        'qry += " Union All "
        'qry += " Select '' as CURRENCY_CODE,TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo as LCCreationNo,0 as Line_No,'' as BenefecieryCode,'' as BenefecieryName,'' as Location_Code ,'' as Location_Desc,0 as LCAmount,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.LCRequestNo ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Desc ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code ,0 as Qty,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Unapproved,-1 as RI ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.PurchaseOrder_No,0 as Rate,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.ParticularsofGoodsLCNo as LCNo from TSPL_DOCUMENT_ACCEPTANCE_MT left outer join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo=TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  where TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=0 and len(isnull(TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo ,''))>0 and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo not in('" + fndDocumentAcceptance.Value + "')"
        'qry += " ) Final Where Final.LCCreationNo ='" & fndLCCreationNo.Value & "'  group by LCRequestNo,Item_Code   having (SUM(Qty *RI)-SUM(Unapproved)) >0)BB"


        Dim qry As String = " Select BB.LCAmount,BB.CURRENCY_CODE,BB.LCNo,BB.[Unit Code] ,BB.LCCreationNo,BB.LCRequestNo,BB.PurchaseOrder_No,BB.PurchaseInvoice_No,BB.BenefecieryCode as [Vendor Code],BB.BenefecieryName as [Vendor Name] , BB.Location_Code as [Location Code],BB.Location_Desc as [Location Desc],BB.Line_No,BB.Line_No,BB.[Item Code] ,BB.[Item Desc] ,BB.Rate ,BB.PendingQty " & _
           " from (Select Max(CURRENCY_CODE) as CURRENCY_CODE,MAx(LCCreationNo)as LCCreationNo,Max(Line_No) as Line_No,Max(BenefecieryCode ) as BenefecieryCode,max(BenefecieryName) as BenefecieryName,MAX(Location_Code) as Location_Code ,MAX(Location_Desc) as Location_Desc,SUM(LCAmount) as LCAmount,MAX(LCRequestNo) as LCRequestNo,MAX(Item_Code) as [Item Code],MAX(Item_Desc) as [Item Desc],MAX(Unit_code) as [Unit Code],SUM(Qty* case when RI=1 then 1 else 0 end) as LCQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as AcceptanceQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty,MAX(PurchaseOrder_No) as PurchaseOrder_No,MAX(PurchaseInvoice_No) as PurchaseInvoice_No,SUM(Rate) as Rate,MAX(LCNo) as LCNo from (" & _
           " Select TSPL_LC_CREATION_MT.CURRENCY_CODE,TSPL_LC_CREATION_MT.LCCreationNo,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,TSPL_LC_CREATION_MT.BenefecieryCode,TSPL_LC_CREATION_MT.BenefecieryName,TSPL_LC_CREATION_MT.Location_Code ,TSPL_LC_CREATION_MT.Location_Desc ,TSPL_LC_CREATION_MT.LCAmount ,TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,TSPL_LC_REQUEST_DETAIL_MT.Item_Code ,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc ,TSPL_LC_REQUEST_DETAIL_MT.Unit_code ,TSPL_LC_REQUEST_DETAIL_MT.Qty,0 as Unapproved,1 as RI,isnull(TSPL_LC_REQUEST_MT.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'') as  PurchaseInvoice_No,TSPL_LC_REQUEST_DETAIL_MT.Rate,TSPL_LC_CREATION_MT.LCNo   from TSPL_LC_CREATION_MT left outer Join TSPL_LC_REQUEST_MT on TSPL_LC_CREATION_MT.LCRequestNo=TSPL_LC_REQUEST_MT.LCRequestNo Left Outer Join TSPL_LC_REQUEST_DETAIL_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo where  TSPL_LC_CREATION_MT.Posted =1 " & _
           " Union All " & _
           " Select '' as CURRENCY_CODE,TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo as LCCreationNo,0 as Line_No,'' as BenefecieryCode,'' as BenefecieryName,'' as Location_Code ,'' as Location_Desc,0 as LCAmount,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.LCRequestNo ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Desc ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code ,0 as Qty,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Unapproved,-1 as RI ,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseInvoice_No,'') as  PurchaseInvoice_No,0 as Rate,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.ParticularsofGoodsLCNo as LCNo from TSPL_DOCUMENT_ACCEPTANCE_MT left outer join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo=TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  where TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=1 and len(isnull(TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo ,''))>0 " & _
           " Union All " & _
          " Select '' as CURRENCY_CODE,TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo as LCCreationNo,0 as Line_No,'' as BenefecieryCode,'' as BenefecieryName,'' as Location_Code ,'' as Location_Desc,0 as LCAmount,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.LCRequestNo ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Code ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Desc ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code ,0 as Qty,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Unapproved,-1 as RI ,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseOrder_No,'') as PurchaseOrder_No,isnull(TSPL_DOCUMENT_ACCEPTANCE_MT.PurchaseInvoice_No,'') as  PurchaseInvoice_No,0 as Rate,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.ParticularsofGoodsLCNo as LCNo from TSPL_DOCUMENT_ACCEPTANCE_MT left outer join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo=TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo  where TSPL_DOCUMENT_ACCEPTANCE_MT.Posted=0 and len(isnull(TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo ,''))>0 and TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo not in('" + fndDocumentAcceptance.Value + "')" & _
         " ) Final Where Final.LCCreationNo ='" & fndLCCreationNo.Value & "'  group by LCRequestNo,Item_Code  )BB"
        ' " ) Final Where Final.LCCreationNo ='" & fndLCCreationNo.Value & "'  group by LCRequestNo,Item_Code   having (SUM(Qty *RI)-SUM(Unapproved)) >0)BB"


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To gv1.Rows.Count - 1
                gv1.Rows(i).Cells(colLCQty).Value = clsCommon.myCdbl(dt.Rows(i)("PendingQty"))
            Next
        End If
    End Sub
    Private Sub fndDocumentAcceptance__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocumentAcceptance._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DOCUMENT_ACCEPTANCE_MT where DocumentAcceptanceNo='" + fndDocumentAcceptance.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndDocumentAcceptance.MyReadOnly = True
            ElseIf check <= 0 Then
                fndDocumentAcceptance.MyReadOnly = False
            End If
            LoadData(fndDocumentAcceptance.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDocumentAcceptance__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocumentAcceptance._MYValidating
        fndDocumentAcceptance.Value = ClsDocumentAcceptance.getFinder("", fndDocumentAcceptance.Value, isButtonClicked)
        LoadData(fndDocumentAcceptance.Value, NavigatorType.Current)
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isLoadInsideData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True

                    If e.Column Is gv1.Columns(colQty) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                    End If
                End If
                UpdateAllTotals()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateAllTotals()
        Try
            Dim dblTotAmt As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If (clsCommon.myLen(gv1.Rows(ii).Cells(colAmount).Value) > 0) Then
                    dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
                End If
            Next

            lblTotRAmt.Text = clsCommon.myFormat(dblTotAmt)
           
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try

            Dim Qty As Double = 0
            Dim rate As Double = 0
            Dim Amount As Double = 0

            Qty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            rate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)

            Amount = Qty * rate
            gv1.Rows(IntRowNo).Cells(colAmount).Value = Math.Round(Amount, 2)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RDAcceptanceLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RDAcceptanceLetter.Click
        Try
            If clsCommon.myLen(fndDocumentAcceptance.Value) > 0 Then
                funAcceptanceLetterPrint()
            Else
                Throw New Exception("Please Select Acceptance No first")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funAcceptanceLetterPrint()
        Try

            Dim qry As String = "Select TSPL_COMPANY_MASTER.IECode,TSPL_COMPANY_MASTER.Comp_Name,TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo  as AccReferenceNo ,convert(varchar,TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptance_Date ,103) as ReferenceDate,TSPL_DOCUMENT_ACCEPTANCE_MT.ReferenceNo,TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo as LCNo ,TSPL_DOCUMENT_ACCEPTANCE_MT.LCAmount ,convert(varchar,TSPL_DOCUMENT_ACCEPTANCE_MT.DueDate,103)  as duedate," & _
            " case when TSPL_COMPANY_MASTER.Add1<>'' then TSPL_COMPANY_MASTER.Add1 else '' end + case when TSPL_COMPANY_MASTER.Add2<>'' " & _
            " then ', '+TSPL_COMPANY_MASTER.Add2 else '' end+ case when TSPL_COMPANY_MASTER.Add3<>'' then ',Address - '+TSPL_COMPANY_MASTER.Add3 " & _
            " else '' end+ case when TSPL_COMPANY_MASTER.City_Code <>'' then ', '+TSPL_COMPANY_MASTER.City_Code  else '' end  as address," & _
            " TSPL_REPORT_FORMAT_DECLARATION_MT.Acceptance_Letter_Context  as ReferenceContext, " & _
            " TSPL_BANK_MASTER.DESCRIPTION as BankName,'' as BankSubject,TSPL_BANK_MASTER.ADD1 ,TSPL_BANK_MASTER.ADD2,TSPL_BANK_MASTER.ADD3 ,TSPL_BANK_MASTER.CITY ," & _
            " TSPL_BANK_MASTER.POSTAL,case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' then TSPL_BANK_MASTER.ADD3 else '' END + CASE when isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' Then ', ' Else '' End) +TSPL_BANK_MASTER.CITY Else '' END + CASE when isnull(TSPL_BANK_MASTER.POSTAL,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')+isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then ' - ' Else '' End)+ TSPL_BANK_MASTER.POSTAL  Else '' END as [bankAddress] " & _
            " from TSPL_DOCUMENT_ACCEPTANCE_MT " & _
            " Left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_DOCUMENT_ACCEPTANCE_MT.Comp_Code" & _
            " Left Outer Join TSPL_REPORT_FORMAT_DECLARATION_MT on TSPL_REPORT_FORMAT_DECLARATION_MT.Comp_Code =TSPL_DOCUMENT_ACCEPTANCE_MT .Comp_Code  " & _
            " Left Outer Join TSPL_LC_CREATION_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo =TSPL_LC_CREATION_MT .LCCreationNo " & _
            " Left Outer Join TSPL_LC_REQUEST_MT  on TSPL_LC_CREATION_MT.LCRequestNo  =TSPL_LC_REQUEST_MT .LCRequestNo  " & _
            " Left outer Join TSPL_BANK_MASTER on TSPL_LC_REQUEST_MT.Bank_Code=TSPL_BANK_MASTER.BANK_CODE " & _
            " where 1=1 and  TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo  ='" + fndDocumentAcceptance.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptAcceptanceLetterMT", "Acceptance Letter")
            frmCRV = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RDTrustReceipt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RDTrustReceipt.Click
        Try
            If clsCommon.myLen(fndDocumentAcceptance.Value) > 0 Then
                funTrustReceiptPrint()
            Else
                Throw New Exception("Please Select Acceptance No first")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funTrustReceiptPrint()
        Try

            Dim qry As String = " Select TSPL_COMPANY_MASTER.IECode,TSPL_COMPANY_MASTER.Comp_Name," & _
                                " TSPL_COMPANY_MASTER.CINNo as CinNo ,TSPL_COMPANY_MASTER.Add1 as CompAdd1 ,TSPL_COMPANY_MASTER.Add2 as CompAdd2 ,TSPL_COMPANY_MASTER.Add3 as CompAdd3 ," & _
                                " TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo  as AccReferenceNo ,convert(varchar,TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptance_Date ,103) as ReferenceDate,TSPL_DOCUMENT_ACCEPTANCE_MT.ReferenceNo,TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo as LCNo ,TSPL_DOCUMENT_ACCEPTANCE_MT.LCAmount ,convert(varchar,TSPL_DOCUMENT_ACCEPTANCE_MT.DueDate,103)  as duedate, " & _
                                " TSPL_BANK_MASTER.DESCRIPTION as BankName,TSPL_BANK_MASTER.ADD1 ,TSPL_BANK_MASTER.ADD2,TSPL_BANK_MASTER.ADD3 ,TSPL_BANK_MASTER.CITY ,TSPL_BANK_MASTER.POSTAL,case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' then TSPL_BANK_MASTER.ADD3 else '' END + CASE when isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' Then ', ' Else '' End) +TSPL_BANK_MASTER.CITY Else '' END + CASE when isnull(TSPL_BANK_MASTER.POSTAL,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')+isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then ' - ' Else '' End)+ TSPL_BANK_MASTER.POSTAL  Else '' END as [bankAddress]," & _
                                " TSPL_REPORT_FORMAT_DECLARATION_MT.Trust_Receipt_Context   as ReferenceContext , TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Unit_code,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.CURRENCY_CODE ," & _
                                " TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.ParticularsofGoodsLCNo as PartiularsofGoods ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Item_Desc as DescriptionofGoods,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Qty as Quantity ,TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.Amount as Value  from TSPL_DOCUMENT_ACCEPTANCE_MT  Left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_DOCUMENT_ACCEPTANCE_MT.Comp_Code Left Outer Join TSPL_REPORT_FORMAT_DECLARATION_MT on TSPL_REPORT_FORMAT_DECLARATION_MT.Comp_Code =TSPL_DOCUMENT_ACCEPTANCE_MT .Comp_Code " & _
                                " Left Outer Join TSPL_LC_CREATION_MT on TSPL_DOCUMENT_ACCEPTANCE_MT.LCCreationNo =TSPL_LC_CREATION_MT .LCCreationNo " & _
                                " Left Outer Join TSPL_LC_REQUEST_MT  on TSPL_LC_CREATION_MT.LCRequestNo  =TSPL_LC_REQUEST_MT .LCRequestNo  " & _
                                " Left outer Join TSPL_BANK_MASTER on TSPL_LC_REQUEST_MT.Bank_Code=TSPL_BANK_MASTER.BANK_CODE" & _
                                " Left Outer Join TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT on TSPL_DOCUMENT_ACCEPTANCE_DETAIL_MT.DocumentAcceptanceNo =TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo " & _
                                " where 1=1 and  TSPL_DOCUMENT_ACCEPTANCE_MT.DocumentAcceptanceNo ='" + fndDocumentAcceptance.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptTrustReceiptMT", "Trust Receipt")
            frmCRV = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub RMDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RMSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RMSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & "gv1"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                gv1.MasterTemplate.FilterDescriptors.Clear()

                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
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
                obj = CType(obj.GetData(Form_ID & "gv1", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If

        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RDA2Letter_Click(sender As Object, e As EventArgs) Handles RDA2Letter.Click
        Try
            If clsCommon.myLen(fndDocumentAcceptance.Value) > 0 Then
                funFormA2Print()
            Else
                Throw New Exception("Please Select Acceptance No first")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funFormA2Print()
        Try

            'Dim qry As String = "Select TSPL_Vendor_Bank_MASTER.Bank_Name +case when TSPL_Vendor_Bank_MASTER.Add1<>'' then ', '+TSPL_Vendor_Bank_MASTER.Add1 else '' end+case when TSPL_Vendor_Bank_MASTER.Add2<>'' then ', '+TSPL_Vendor_Bank_MASTER.Add2 else '' end+case when TSPL_Vendor_Bank_MASTER.Add3<>'' then ', '+TSPL_Vendor_Bank_MASTER.Add3 else '' end as BenefeciaryBankInfo,TSPL_VENDOR_MASTER.Account_No as BenefieceryAccountNo," & _
            '" TSPL_LC_REQUEST_MT.CURRENCY_CODE ,TSPL_LC_REQUEST_MT.ConvRate,TSPL_LC_REQUEST_MT.LCAmount,TSPL_COMPANY_MASTER.Comp_Name+ case when TSPL_COMPANY_MASTER.CINNo <>'' then ' {CIN No.- '+TSPL_COMPANY_MASTER.CINNo+'}' else '' end   as CompanyFooterAddress,TSPL_BANK_MASTER.BANKACCNUMBER ,TSPL_COMPANY_MASTER.Comp_Name+ case when TSPL_COMPANY_MASTER.CINNo <>'' then ' {CIN No.- '+TSPL_COMPANY_MASTER.CINNo+'}' else '' end + case when TSPL_COMPANY_MASTER.Add1<>'' then ',Address - '+TSPL_COMPANY_MASTER.Add1 else '' end + case when TSPL_COMPANY_MASTER.Add2<>'' then ', '+TSPL_COMPANY_MASTER.Add2 else '' end+ case when TSPL_COMPANY_MASTER.Add3<>'' then ',Address - '+TSPL_COMPANY_MASTER.Add3 else '' end+ case when TSPL_COMPANY_MASTER.City_Code <>'' then ', '+TSPL_COMPANY_MASTER.City_Code  else '' end  as CompanyAddress,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_MASTER.Vendor_Name as BenefecieryName,TSPL_LC_REQUEST_MT.AD_Code_No ,TSPL_LC_REQUEST_MT.Form_No ,TSPL_LC_REQUEST_MT.Serial_No ,TSPL_LC_REQUEST_MT.Purpose_Code ,TSPL_LC_REQUEST_MT.Purpose_Group_Name,Convert(varchar,TSPL_LC_REQUEST_MT.LCRequest_Date,103) as ReferenceDate," & _
            '" TSPL_BANK_MASTER.DESCRIPTION as BankName,'' as BankSubject,TSPL_BANK_MASTER.ADD1 ,TSPL_BANK_MASTER.ADD2,TSPL_BANK_MASTER.ADD3 ,TSPL_BANK_MASTER.CITY ,TSPL_BANK_MASTER.POSTAL from TSPL_LC_REQUEST_MT Left outer Join TSPL_BANK_MASTER on TSPL_LC_REQUEST_MT.Bank_Code=TSPL_BANK_MASTER.BANK_CODE " & _
            '" Left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_LC_REQUEST_MT.Comp_Code " & _
            '" left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_LC_REQUEST_MT.PurchaseOrder_No" & _
            '" left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PURCHASE_ORDER_HEAD.MT_Beneficiary_Code " & _
            '" Left Outer Join TSPL_Vendor_Bank_MASTER on TSPL_Vendor_Bank_MASTER.Bank_Code=TSPL_VENDOR_MASTER.Bank_Code  " & _
            '" where 1=1 and  TSPL_LC_REQUEST_MT.LCRequestNo ='" + fndLCRequestcode.Value + "'"

            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            'KwalitySalesReportViewer.funreport(dt, "rptFormA2MT", "Form A2")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
