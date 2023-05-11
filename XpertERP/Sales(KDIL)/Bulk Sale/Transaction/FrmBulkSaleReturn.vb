'--------Created By Richa 24/11/2014 Against Ticket No BM00000004744,BM00000004865,BM00000005059,(add Invoice fat kg and snf kg in table and also work on form regarding this BM00000005562)
''RICHA AGARWAL BM00000006030 01/04/2015
Imports System.Data.SqlClient
Imports common
Imports System.IO
Public Class FrmBulkSaleReturn
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim TCSTaxApplicableOnbulkSale As Boolean = False
    Dim AmountToCheckCustomerOutstandingForTCSTax As Double = 0
    Dim SaleInvoiceDate As DateTime
    Public AllowtoChangeTCSBaseAmount As Boolean = False
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean
    Public isInsideLoadData As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colDispatchNo As String = "colDispatchNo"
    Public Const colDispatchDate As String = "colDispatchDate"
    Public Const colTankerNo As String = "colTankerNo"
    Public Const colItemCode As String = "colItemCode"
    Public Const colItemDesc As String = "colItemDesc"
    Public Const colUnitCode As String = "colUnitCode"
    Public Const colDispatchQty As String = "colDispatchQty"
    Public Const colDispatchFatPer As String = "colDispatchFatPer"
    Public Const colDispatchSnfPer As String = "colDispatchSnfPer"
    Public Const colDispatchRate As String = "colDispatchRate"
    Public Const colDispatchAmount As String = "colDispatchAmount"
    Public Const ColInvoiceQty As String = "ColInvoiceQty"
    Public Const ColInvoiceFatPer As String = "ColInvoiceFatPer"
    Public Const colInvoiceSnfPer As String = "colInvoiceSnfPer"
    Public Const colInvoiceRate As String = "colInvoiceRate"
    Public Const colInvoiceAmount As String = "colInvoiceAmount"
    Public Const colInvoiceSNFKG As String = "colInvoiceSNFKG"
    Public Const colInvoiceFatKG As String = "colInvoiceFatKG"
    Public Const colDispatchQtyLtr As String = "colDispatchQtyLtr"
    Public Const colInvoiceQtyLtr As String = "colInvoiceQtyLtr"
    Dim isFlag As Boolean = False
    Dim Qry As String
    Public strSaleInvoice As String = Nothing
    Const colLineNo As String = "COLLNO"
    Const colRowType As String = "COLTYPE"
    Const colRGPNo As String = "COLRGPNo"
    Const colComplete As String = "COMPLETE"
    Const colBalanceQty As String = "BALANCEQTY"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colPendingQty As String = "COLPENDINGQTY"
    Const colOrgRequitionQty As String = "COLORIGPEQQTY"
    Const colQty As String = "COLQTY"
    Const colUnit As String = "COLUNIT"
    Const colRate As String = "COLRATE"
    Const colAmt As String = "COLAMT"
    Const colDisPer As String = "COLDISPER"
    Const colDisAmt As String = "COLDISAMT"
    Const colAmtAfterDis As String = "COLAMTAFTERDIS"
    Public strFormId As String
    Const ReportID As String = "POItemGrid"
    Dim arrLoc As String = Nothing
    Public Shared aLoc As String = Nothing
    Dim ShowBulkDispatchQtyInLtr As Boolean = False

    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"

    Const colIsTaxOnBaseAmount As String = "colIsTaxOnBaseAmount"
    Const colTax As String = "COLTAX"
    Const colTaxBaseAmt As String = "COLTAXBASEAMT"
    Const colTaxRate As String = "COLTAXRATE"
    Const colTaxAmt As String = "COLTAXAMT"
    Const colIsTaxable As String = "ISTAXABLE"
    Const colIsSurTax As String = "ISSURTAX"
    Const colSurTaxCode As String = "SURTAXCODE"
    Const colTaxRecoverable As String = "RECOVERTABLETAX"
    Const colTotTaxAmt As String = "colTotTaxAmt"
    Const colAmtAfterTax As String = "colAmtAfterTax"
    Const colIsExcisable As String = "IsExcisable"


#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmBulkSaleReturn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
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


    Private Sub FrmBulkSaleReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ShowBulkDispatchQtyInLtr = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowBulkDispatchQtyInLtr, clsFixedParameterCode.ShowBulkDispatchQtyInLtr, Nothing)) = 1, True, False))
        TCSTaxApplicableOnbulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.TCSTaxApplicableOnbulkSale, clsFixedParameterCode.TCSTaxApplicableOnbulkSale, Nothing)) = 1, True, False))
        AmountToCheckCustomerOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckCustomerOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckCustomerOutstandingForTCSTax, Nothing))
        AllowtoChangeTCSBaseAmount = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoChangeTCSBaseAmount, clsFixedParameterCode.AllowtoChangeTCSBaseAmount, Nothing)) = 0, False, True)
        SetUserMgmtNew()
        Reset()
        UcAttachment1.Form_ID = MyBase.Form_ID
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N New Transaction")
        If clsCommon.myLen(strSaleInvoice) > 0 Then
            LoadData(strSaleInvoice, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBulkSaleReturn)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
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

        Dim DispatchNo As New GridViewTextBoxColumn()
        DispatchNo.FormatString = ""
        DispatchNo.HeaderText = "Dispatch No"
        DispatchNo.Name = colDispatchNo
        DispatchNo.Width = 100
        DispatchNo.ReadOnly = True
        DispatchNo.WrapText = True
        DispatchNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(DispatchNo)

        Dim DispatchDate As New GridViewTextBoxColumn()
        DispatchDate.FormatString = ""
        DispatchDate.HeaderText = "Dispatch Date"
        DispatchDate.Name = colDispatchDate
        DispatchDate.Width = 100
        DispatchDate.ReadOnly = True
        DispatchDate.WrapText = True
        DispatchDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(DispatchDate)

        Dim TankerCode As New GridViewTextBoxColumn()
        TankerCode.FormatString = ""
        TankerCode.HeaderText = "Tanker Code"
        TankerCode.Name = colTankerNo
        TankerCode.Width = 100
        TankerCode.ReadOnly = True
        TankerCode.WrapText = True
        TankerCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(TankerCode)

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
        itemDesc.HeaderText = "Item Desc"
        itemDesc.Name = colItemDesc
        itemDesc.Width = 250
        itemDesc.ReadOnly = True
        itemDesc.WrapText = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(itemDesc)

        Dim strUnitCode As New GridViewTextBoxColumn()
        strUnitCode.FormatString = ""
        strUnitCode.HeaderText = "UOM"
        strUnitCode.Name = colUnitCode
        strUnitCode.Width = 70
        strUnitCode.ReadOnly = True
        strUnitCode.WrapText = True
        strUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(strUnitCode)

        Dim dispatchQty As New GridViewDecimalColumn
        dispatchQty.FormatString = "{0:n3}"
        'dispatchQty.HeaderText = "Dispatch Qty"
        dispatchQty.HeaderText = "Qty"
        dispatchQty.Name = colDispatchQty
        dispatchQty.Width = 100
        dispatchQty.ReadOnly = True
        dispatchQty.WrapText = True
        dispatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(dispatchQty)

        'richa 4 Feb,2019 ERO/07/01/19-000459
        Dim QtyInLtr As New GridViewDecimalColumn
        QtyInLtr.FormatString = "{0:n3}"
        QtyInLtr.HeaderText = "Dispatch Qty(Ltr)"
        QtyInLtr.DecimalPlaces = 3
        QtyInLtr.Name = colDispatchQtyLtr
        QtyInLtr.Width = 120
        QtyInLtr.ReadOnly = True
        QtyInLtr.WrapText = True
        QtyInLtr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(QtyInLtr)

        If ShowBulkDispatchQtyInLtr = True Then
            gv1.Columns(colDispatchQtyLtr).IsVisible = True
            gv1.Columns(colDispatchQtyLtr).VisibleInColumnChooser = True
        Else
            gv1.Columns(colDispatchQtyLtr).IsVisible = False
            gv1.Columns(colDispatchQtyLtr).VisibleInColumnChooser = False
        End If
        'richa 

        Dim dispatchfatper As New GridViewDecimalColumn
        dispatchfatper.FormatString = ""
        'dispatchfatper.HeaderText = "Dispatch FAT %"
        dispatchfatper.HeaderText = "FAT %"
        dispatchfatper.Name = colDispatchFatPer
        dispatchfatper.Width = 75
        dispatchfatper.ReadOnly = True
        dispatchfatper.WrapText = True
        dispatchfatper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(dispatchfatper)



        Dim dispatchsnfper As New GridViewDecimalColumn
        dispatchsnfper.FormatString = ""
        'dispatchsnfper.HeaderText = "Dispatch SNF %"
        dispatchsnfper.HeaderText = "SNF %"
        dispatchsnfper.Name = colDispatchSnfPer
        dispatchsnfper.Width = 75
        dispatchsnfper.ReadOnly = True
        dispatchsnfper.WrapText = True
        dispatchsnfper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(dispatchsnfper)

        Dim DispatchFatRate As New GridViewDecimalColumn
        DispatchFatRate.FormatString = ""
        'DispatchFatRate.HeaderText = "Dispatch Rate"
        DispatchFatRate.HeaderText = "Rate"
        DispatchFatRate.Name = colDispatchRate
        DispatchFatRate.Width = 75
        DispatchFatRate.ReadOnly = True
        DispatchFatRate.WrapText = True
        DispatchFatRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(DispatchFatRate)

        Dim DispatchAmount As New GridViewDecimalColumn
        DispatchAmount.FormatString = ""
        'DispatchAmount.HeaderText = "Dispatch Amount"
        DispatchAmount.HeaderText = "Amount"
        DispatchAmount.Name = colDispatchAmount
        DispatchAmount.Width = 75
        DispatchAmount.ReadOnly = True
        DispatchAmount.WrapText = True
        DispatchAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(DispatchAmount)

        Dim InvoiceQty As New GridViewDecimalColumn
        InvoiceQty.FormatString = "{0:n3}"
        InvoiceQty.HeaderText = "Invoice Qty"
        InvoiceQty.Name = ColInvoiceQty
        InvoiceQty.Width = 75
        InvoiceQty.ReadOnly = True
        InvoiceQty.WrapText = True
        InvoiceQty.IsVisible = Not rdbAgainstDispatch.IsChecked
        InvoiceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceQty)

        'richa 4 Feb,2019 ERO/07/01/19-000459
        Dim QtyInvoiceInLtr As New GridViewDecimalColumn
        QtyInvoiceInLtr.FormatString = "{0:n3}"
        QtyInvoiceInLtr.HeaderText = "Invoice Qty(Ltr)"
        QtyInvoiceInLtr.DecimalPlaces = 3
        QtyInvoiceInLtr.Name = colInvoiceQtyLtr
        QtyInvoiceInLtr.Width = 120
        QtyInvoiceInLtr.ReadOnly = True
        QtyInvoiceInLtr.WrapText = True
        QtyInvoiceInLtr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(QtyInvoiceInLtr)

        If ShowBulkDispatchQtyInLtr = True Then
            gv1.Columns(colInvoiceQtyLtr).IsVisible = True
            gv1.Columns(colInvoiceQtyLtr).VisibleInColumnChooser = True
        Else
            gv1.Columns(colInvoiceQtyLtr).IsVisible = False
            gv1.Columns(colInvoiceQtyLtr).VisibleInColumnChooser = False
        End If
        QtyInvoiceInLtr.IsVisible = Not rdbAgainstDispatch.IsChecked

        Dim InvoiceFatPer As New GridViewTextBoxColumn
        InvoiceFatPer.FormatString = ""
        InvoiceFatPer.HeaderText = "Invoice Fat %"
        InvoiceFatPer.Name = ColInvoiceFatPer
        InvoiceFatPer.Width = 75
        InvoiceFatPer.ReadOnly = True
        InvoiceFatPer.WrapText = True
        InvoiceFatPer.IsVisible = Not rdbAgainstDispatch.IsChecked
        'InvoiceFatPer.IsVisible = rdbAgainstDispatchTrade.IsChecked
        InvoiceFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceFatPer)

        Dim InvoiceSnfPer As New GridViewTextBoxColumn
        InvoiceSnfPer.FormatString = ""
        InvoiceSnfPer.HeaderText = "Invoice SNF %"
        InvoiceSnfPer.Name = colInvoiceSnfPer
        InvoiceSnfPer.Width = 75
        InvoiceSnfPer.ReadOnly = True
        InvoiceSnfPer.WrapText = True
        InvoiceSnfPer.IsVisible = Not rdbAgainstDispatch.IsChecked
        'InvoiceSnfPer.IsVisible = rdbAgainstDispatchTrade.IsChecked
        InvoiceSnfPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceSnfPer)


        Dim InvoiceRate As New GridViewDecimalColumn
        InvoiceRate.FormatString = ""
        InvoiceRate.HeaderText = "Invoice Rate"
        InvoiceRate.Name = colInvoiceRate
        InvoiceRate.Width = 75
        InvoiceRate.ReadOnly = True
        InvoiceRate.WrapText = True
        InvoiceRate.IsVisible = Not rdbAgainstDispatch.IsChecked
        'InvoiceRate.IsVisible = rdbAgainstDispatchTrade.IsChecked
        InvoiceRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceRate)

        Dim InvoiceAmount As New GridViewDecimalColumn
        InvoiceAmount.FormatString = ""
        InvoiceAmount.HeaderText = "Invoice Amount"
        InvoiceAmount.Name = colInvoiceAmount
        InvoiceAmount.Width = 75
        InvoiceAmount.ReadOnly = True
        InvoiceAmount.WrapText = True
        InvoiceAmount.IsVisible = Not rdbAgainstDispatch.IsChecked
        'InvoiceAmount.IsVisible = rdbAgainstDispatchTrade.IsChecked
        InvoiceAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceAmount)

        Dim InvoiceFatKG As New GridViewDecimalColumn
        InvoiceFatKG.FormatString = ""
        InvoiceFatKG.HeaderText = "Invoice Fat KG"
        InvoiceFatKG.Name = colInvoiceFatKG
        InvoiceFatKG.Width = 75
        InvoiceFatKG.ReadOnly = True
        InvoiceFatKG.WrapText = True
        InvoiceFatKG.IsVisible = False
        InvoiceFatKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceFatKG)

        Dim InvoiceSNFKG As New GridViewDecimalColumn
        InvoiceSNFKG.FormatString = ""
        InvoiceSNFKG.HeaderText = "Invoice SNF KG"
        InvoiceSNFKG.Name = colInvoiceSNFKG
        InvoiceSNFKG.Width = 75
        InvoiceSNFKG.ReadOnly = True
        InvoiceSNFKG.WrapText = True
        InvoiceSNFKG.IsVisible = False
        InvoiceSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceSNFKG)

        For ii As Integer = 1 To 5
            Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax10.FormatString = ""
            repoTax10.HeaderText = "Tax " + clsCommon.myCstr(ii)
            repoTax10.Name = colTax + clsCommon.myCstr(ii)
            repoTax10.ReadOnly = True
            repoTax10.IsVisible = False
            gv1.MasterTemplate.Columns.Add(repoTax10)

            Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoTaxBaseAmt10.FormatString = ""
            repoTaxBaseAmt10.HeaderText = "Tax Base Amount "
            repoTaxBaseAmt10.Name = colTaxBaseAmt + clsCommon.myCstr(ii)
            repoTaxBaseAmt10.ReadOnly = True
            repoTaxBaseAmt10.IsVisible = False
            gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt10)

            Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoTaxRate10 = New GridViewDecimalColumn()
            repoTaxRate10.FormatString = ""
            repoTaxRate10.HeaderText = "Tax Rate " + clsCommon.myCstr(ii)
            repoTaxRate10.Name = colTaxRate + clsCommon.myCstr(ii)
            repoTaxRate10.IsVisible = False
            repoTaxRate10.ReadOnly = True
            repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gv1.MasterTemplate.Columns.Add(repoTaxRate10)

            Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoTaxAmt10 = New GridViewDecimalColumn()
            repoTaxAmt10.FormatString = ""
            repoTaxAmt10.HeaderText = "Tax Amt " + clsCommon.myCstr(ii)
            repoTaxAmt10.Name = colTaxAmt + clsCommon.myCstr(ii)
            repoTaxAmt10.IsVisible = False
            repoTaxAmt10.ReadOnly = True
            repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gv1.MasterTemplate.Columns.Add(repoTaxAmt10)

            Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoIsSurTax10.HeaderText = "Is Surtax " + clsCommon.myCstr(ii)
            repoIsSurTax10.Name = colIsSurTax + clsCommon.myCstr(ii)
            repoIsSurTax10.ReadOnly = True
            repoIsSurTax10.IsVisible = False
            repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(repoIsSurTax10)

            Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoSurTaxCode10.FormatString = ""
            repoSurTaxCode10.HeaderText = "Surtax " + clsCommon.myCstr(ii)
            repoSurTaxCode10.Name = colSurTaxCode + clsCommon.myCstr(ii)
            repoSurTaxCode10.ReadOnly = True
            repoSurTaxCode10.IsVisible = False
            gv1.MasterTemplate.Columns.Add(repoSurTaxCode10)

            Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoIsTaxable10.HeaderText = "Is Taxable " + clsCommon.myCstr(ii)
            repoIsTaxable10.Name = colIsTaxable + clsCommon.myCstr(ii)
            repoIsTaxable10.ReadOnly = True
            repoIsTaxable10.IsVisible = False
            repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(repoIsTaxable10)


            Dim repoCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoCheckBox.HeaderText = "Is Tax On Base Amount " + clsCommon.myCstr(ii)
            repoCheckBox.Name = colIsTaxOnBaseAmount + clsCommon.myCstr(ii)
            repoCheckBox.ReadOnly = True
            repoCheckBox.IsVisible = False
            repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            repoCheckBox.WrapText = True
            gv1.MasterTemplate.Columns.Add(repoCheckBox)

            Dim repoTaxRecoverable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoTaxRecoverable10.HeaderText = "Recoverable Tax " + clsCommon.myCstr(ii)
            repoTaxRecoverable10.Name = colTaxRecoverable + clsCommon.myCstr(ii)
            repoTaxRecoverable10.ReadOnly = True
            repoTaxRecoverable10.IsVisible = False
            repoTaxRecoverable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(repoTaxRecoverable10)

            Dim repoIsExcisable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoIsExcisable10.HeaderText = "Is Excisable " + clsCommon.myCstr(ii)
            repoIsExcisable10.Name = colIsExcisable + clsCommon.myCstr(ii)
            repoIsExcisable10.ReadOnly = True
            repoIsExcisable10.IsVisible = False
            repoIsExcisable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            gv1.MasterTemplate.Columns.Add(repoIsExcisable10)

        Next

        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax)



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

        lineNo = Nothing
        DispatchNo = Nothing
        DispatchDate = Nothing
        TankerCode = Nothing
        itemCode = Nothing
        itemDesc = Nothing
        strUnitCode = Nothing
        InvoiceFatPer = Nothing
        InvoiceSnfPer = Nothing
    End Sub
    Sub Reset()
        btnInvoiceJE.Visible = False
        chkCncelPSR.Enabled = True
        chkCncelPSR.Checked = False
        txtDocNo.Value = ""
        fndCustomerNo.Value = ""
        lblCustomerCode.Text = ""
        lblCustomerName.Text = ""
        FndLocationCode.Value = ""
        lblLocationCode.Text = ""
        LblLocationName.Text = ""
        FndSiloNo.Value = ""
        LblSiloName.Text = ""
        txtInvoiceNo.Value = ""
        fndGateEntryNo.Value = ""
        lblGateEntryNo.Text = ""
        FndTanker.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtDate.CustomFormat = "dd/MM/yyyy"
        End If
        txtDocNo.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        UcAttachment1.BlankAllControls()
        isNewEntry = True
        loadBlankItemGrid()
        ReStoreGridLayout()
        isNewEntry = True
        lblTotRAmt1.Text = ""
        TxtRoundoff.Text = ""

        chkRejected.Checked = False
        rdbAgainstDispatch.IsChecked = False
        rdbAgainstInvoice.IsChecked = True
        chkRejected.Enabled = False
        MyLabel1.Text = "Invoice No"
        FndSiloNo.Enabled = True
        FndDispatchLocation.Enabled = False
        txtInvoiceNo.Enabled = True
        FndDispatchLocation.Value = ""
        lblDispatchLocation.Text = ""
        SaleInvoiceDate = Nothing
        txtTaxGroup.Enabled = False
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        lblTaxAmt.Text = "0"
        lblDocumentAmount.Text = "0"
        LoadBlankGridTax()
        If AllowtoChangeTCSBaseAmount = True Then
            txttcstaxbaseamount.Enabled = True
        Else
            txttcstaxbaseamount.Enabled = False
        End If
        txttcstaxbaseamount.Value = 0
        lblActualTCSTaxBaseAmt.Text = "0"
        rbtnTaxCalAutomatic.IsChecked = True

    End Sub
    Private Sub ReStoreGridLayout()
        Dim obj As clsGridLayout = New clsGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
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
            End If

        Catch err As Exception
            MessageBox.Show(err.Message)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub RMSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMSaveLayout.Click
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
    Private Sub RMDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RMDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub txtInvoiceNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtInvoiceNo._MYValidating
        ' Dim qry As String = "Select TSPL_INVOICE_MASTER_BULKSALE.Document_No as Code,CONVERT(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) as [Invoice Date],TSPL_INVOICE_MASTER_BULKSALE.Customer_Code as [Customer Code],TSPL_INVOICE_MASTER_BULKSALE.Location_Code as [Location Code],TSPL_Quality_Check_BulkSale.GateEntry_Document_No from TSPL_INVOICE_MASTER_BULKSALE left outer join TSPL_Customer_Invoice_Head on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_Customer_Invoice_Head.Against_Sale_No inner join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE.Document_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No inner join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code inner join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.QC_No=TSPL_Dispatch_BulkSale.QC_Code inner join TSPL_GATEOUT_SALE on TSPL_GATEOUT_SALE.GateEntryNo=TSPL_Quality_Check_BulkSale.GateEntry_Document_No "
        'Dim whrcls As String = "  TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='Against Dispatch' and TSPL_INVOICE_MASTER_BULKSALE.Posted=1  and not exists (Select 1 from TSPL_RECEIPT_DETAIL where TSPL_RECEIPT_DETAIL.Document_No=TSPL_Customer_Invoice_Head.Against_Sale_No ) and  TSPL_INVOICE_MASTER_BULKSALE.Document_No not in (Select TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo from TSPL_SALE_RETURN_MASTER_BULKSALE where TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo<>'" & txtInvoiceNo.Value & "') "
        If rdbAgainstInvoice.IsChecked Then
            Dim qry As String = "Select TSPL_INVOICE_MASTER_BULKSALE.Document_No as Code,TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code ,CONVERT(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) as [Invoice Date],TSPL_INVOICE_MASTER_BULKSALE.Customer_Code as [Customer Code],TSPL_INVOICE_MASTER_BULKSALE.Location_Code as [Location Code] from TSPL_INVOICE_MASTER_BULKSALE left outer join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE.Document_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No left outer  join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code"
            Dim whrcls As String = "TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='Against Dispatch' and TSPL_INVOICE_MASTER_BULKSALE.Posted=1 and TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code=(Select Dispatch_No from TSPL_GATEENTRY_SALE where Document_No ='" & lblGateEntryNo.Text & "')  and TSPL_INVOICE_MASTER_BULKSALE.Document_No not in (Select TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo from TSPL_SALE_RETURN_MASTER_BULKSALE where TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo<>'" & txtInvoiceNo.Value & "') "
            txtInvoiceNo.Value = clsCommon.ShowSelectForm("InvoiceBulkSaleNo", qry, "Code", whrcls, txtInvoiceNo.Value, "", isButtonClicked)
            LoadInvoiceData(txtInvoiceNo.Value, NavigatorType.Current)
            qry = Nothing
            whrcls = Nothing
        Else
            Dim qry As String = "Select TSPL_Dispatch_BulkSale.Tanker_Code as Tanker,TSPL_Dispatch_BulkSale.Location_Code AS Location,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Dispatch_BulkSale.Document_No as [Dispatch No],TSPL_Quality_Check_BulkSale.GateEntry_Document_No as [Gate Entry No]  from TSPL_Dispatch_BulkSale Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_Dispatch_BulkSale.Location_Code " & _
                              " Left Outer Join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale .QC_No =TSPL_Dispatch_BulkSale.QC_Code " & _
                              " Left Outer Join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_Dispatch_BulkSale.Document_No =ISNULL(TSPL_SALE_RETURN_MASTER_BULKSALE.DispatchNo ,'')" & _
                               " where TSPL_Dispatch_BulkSale.Posted =1 and TSPL_Dispatch_BulkSale.Document_No not in  (Select TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE Left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_INVOICE_DETAIL_BULKSALE .Document_No where TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst = 'Against Dispatch' )" & _
                               " and TSPL_Dispatch_BulkSale.Document_No not in (Select DispatchNo from TSPL_SALE_RETURN_MASTER_BULKSALE where Against ='Bulk Dispatch' and Document_No <>'" & txtDocNo.Value & "' )  and TSPL_Dispatch_BulkSale.Tanker_Code='" & FndTanker.Value & "' and TSPL_Quality_Check_BulkSale.GateEntry_Document_No='" & lblGateEntryNo.Text & "' "
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("DispatchNo", qry)
            If Not dr Is Nothing Then
                txtInvoiceNo.Value = clsCommon.myCstr(dr("Dispatch No"))
                LoadDispatchData(txtInvoiceNo.Value, NavigatorType.Current)
            Else
                txtInvoiceNo.Value = ""
            End If
        End If
    End Sub

    Sub LoadInvoiceData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim dt As DataTable = Nothing
        Dim dblActualTCSBaseAmount As Double = 0
        Dim dblChangedTCSBaseAmount As Double = 0

        Dim qry As String = "Select TSPL_INVOICE_MASTER_BULKSALE.Document_Date,TSPL_INVOICE_MASTER_BULKSALE.Total_Amt,TSPL_INVOICE_MASTER_BULKSALE.ChangedTCSBaseAmount,TSPL_INVOICE_MASTER_BULKSALE.ActualTCSBaseAmount,TSPL_INVOICE_MASTER_BULKSALE.Tax_Group,
TSPL_INVOICE_MASTER_BULKSALE.Tax_Calculation_Type,TSPL_INVOICE_MASTER_BULKSALE.Total_Tax_Amt,TSPL_INVOICE_MASTER_BULKSALE.Document_No AS [InvoiceNo],TSPL_INVOICE_MASTER_BULKSALE.Customer_Code ,TSPL_CUSTOMER_MASTER .Customer_Name ,TSPL_INVOICE_MASTER_BULKSALE.Location_Code ,TSPL_LOCATION_MASTER .Location_Desc ,TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code ,TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Date,TSPL_INVOICE_DETAIL_BULKSALE.Item_Code ,TSPL_ITEM_MASTER.Item_Desc,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code ,TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ," &
        " TSPL_INVOICE_DETAIL_BULKSALE.DispatchQty ,TSPL_INVOICE_DETAIL_BULKSALE.DispatchQty_in_Ltr,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty_in_Ltr,TSPL_INVOICE_DETAIL_BULKSALE.DispatchFatPer ,TSPL_INVOICE_DETAIL_BULKSALE.DispatchSNFPer ,TSPL_INVOICE_DETAIL_BULKSALE.DispatchRate ,TSPL_INVOICE_DETAIL_BULKSALE.DispatchAmount ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatPer ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFPer ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceRate ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatKG ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFKG, " &
        " TSPL_INVOICE_DETAIL_BULKSALE.TAX1,TSPL_INVOICE_DETAIL_BULKSALE.TAX1_Base_Amt,TSPL_INVOICE_DETAIL_BULKSALE.TAX1_Rate,TSPL_INVOICE_DETAIL_BULKSALE.TAX1_Amt,TSPL_INVOICE_DETAIL_BULKSALE.TAX2,TSPL_INVOICE_DETAIL_BULKSALE.TAX2_Base_Amt,TSPL_INVOICE_DETAIL_BULKSALE.TAX2_Rate,TSPL_INVOICE_DETAIL_BULKSALE.TAX2_Amt,TSPL_INVOICE_DETAIL_BULKSALE.TAX3,TSPL_INVOICE_DETAIL_BULKSALE.TAX3_Base_Amt,TSPL_INVOICE_DETAIL_BULKSALE.TAX3_Rate,TSPL_INVOICE_DETAIL_BULKSALE.TAX3_Amt,TSPL_INVOICE_DETAIL_BULKSALE.Total_Tax_Amt,TSPL_INVOICE_DETAIL_BULKSALE.Item_Net_Amt " &
        " from TSPL_INVOICE_MASTER_BULKSALE left outer join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No=TSPL_INVOICE_DETAIL_BULKSALE .Document_No " &
        " Left outer join TSPL_CUSTOMER_MASTER on TSPL_INVOICE_MASTER_BULKSALE.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code " &
        " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_INVOICE_MASTER_BULKSALE.Location_Code =TSPL_LOCATION_MASTER .Location_Code  " &
        " Left outer jOin TSPL_ITEM_MASTER on TSPL_INVOICE_DETAIL_BULKSALE.Item_Code =TSPL_ITEM_MASTER .Item_Code" &
        " where TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='Against Dispatch' and  TSPL_INVOICE_MASTER_BULKSALE.Document_No ='" & txtInvoiceNo.Value & "' and TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code =(Select Dispatch_No from TSPL_GATEENTRY_SALE where Document_No ='" & lblGateEntryNo.Text & "')"
        dt = clsDBFuncationality.GetDataTable(qry)
        loadBlankItemGrid()
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                txtInvoiceNo.Value = clsCommon.myCstr(dr("InvoiceNo"))
                lblCustomerCode.Text = clsCommon.myCstr(dr("Customer_Code"))
                lblCustomerName.Text = clsCommon.myCstr(dr("Customer_Name"))
                lblLocationCode.Text = clsCommon.myCstr(dr("Location_Code"))
                LblLocationName.Text = clsCommon.myCstr(dr("Location_Desc"))
                SaleInvoiceDate = clsCommon.myCDate(dr("Document_Date"))
                lblDocumentAmount.Text = clsCommon.myCstr(clsCommon.myCdbl(dr("InvoiceAmount")))
                lblTaxAmt.Text = clsCommon.myCstr(clsCommon.myCdbl(dr("Total_Tax_Amt")))
                Dim DocuAmount As Double = clsCommon.myCdbl(dr("Item_Net_Amt"))
                If Math.Round(clsCommon.myCdbl(DocuAmount), 0) > clsCommon.myCdbl(DocuAmount) Then
                    TxtRoundoff.Text = clsCommon.myCstr(Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount), 0) - clsCommon.myCdbl(DocuAmount), 2))
                    lblTotRAmt1.Text = clsCommon.myCstr(Math.Round(clsCommon.myCdbl(DocuAmount), 0))
                Else
                    TxtRoundoff.Text = clsCommon.myCstr(Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount)) - clsCommon.myCdbl(DocuAmount), 2))
                    lblTotRAmt1.Text = clsCommon.myCstr(Math.Round(clsCommon.myCdbl(DocuAmount), 0))

                End If

                gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchNo).Value = clsCommon.myCstr(dr("Dispatch_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchDate).Value = clsCommon.GetPrintDate(clsCommon.myCDate(dr("Dispatch_Date")), "dd/MM/yyyy")
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr("Unit_code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerNo).Value = clsCommon.myCstr(dr("Tanker_Code"))

                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQty).Value = clsCommon.myCdbl(dr("DispatchQty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQtyLtr).Value = clsCommon.myCdbl(dr("DispatchQty_in_Ltr"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchFatPer).Value = clsCommon.myCdbl(dr("DispatchFatPer"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchSnfPer).Value = clsCommon.myCdbl(dr("DispatchSNFPer"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchRate).Value = clsCommon.myCdbl(dr("DispatchRate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = clsCommon.myCdbl(dr("DispatchAmount"))


                gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQty).Value = clsCommon.myCdbl(dr("InvoiceQty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceQtyLtr).Value = clsCommon.myCdbl(dr("InvoiceQty_in_Ltr"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceFatPer).Value = clsCommon.myCdbl(dr("InvoiceFatPer"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSnfPer).Value = clsCommon.myCdbl(dr("InvoiceSNFPer"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceRate).Value = clsCommon.myCdbl(dr("InvoiceRate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value = clsCommon.myCdbl(dr("InvoiceAmount"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceFatKG).Value = clsCommon.myCdbl(dr("InvoiceFatKG"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSNFKG).Value = clsCommon.myCdbl(dr("InvoiceSNFKG"))

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(1)).Value = clsCommon.myCstr(dr("TAX1"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(1)).Value = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(1)).Value = clsCommon.myCdbl(dr("TAX1_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(1)).Value = clsCommon.myCdbl(dr("TAX1_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(2)).Value = clsCommon.myCstr(dr("TAX2"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(2)).Value = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(2)).Value = clsCommon.myCdbl(dr("TAX2_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(2)).Value = clsCommon.myCdbl(dr("TAX2_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(3)).Value = clsCommon.myCstr(dr("TAX3"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(3)).Value = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(3)).Value = clsCommon.myCdbl(dr("TAX3_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(3)).Value = clsCommon.myCdbl(dr("TAX3_Amt"))
                UpdateCurrentRow(gv1.Rows.Count - 1)
                txtTaxGroup.Enabled = False
                rbtnTaxCalAutomatic.IsChecked = True
                If TCSTaxApplicableOnbulkSale = True Then
                    txtTaxGroup.Value = clsCommon.myCstr(dr("Tax_Group"))
                    lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)

                    SetTaxDetails()
                    txttcstaxbaseamount.Value = dblChangedTCSBaseAmount
                    lblActualTCSTaxBaseAmt.Text = dblActualTCSBaseAmount
                End If
            Next


        Else
            txtInvoiceNo.Value = ""
            lblCustomerCode.Text = ""
            lblCustomerName.Text = ""
            lblLocationCode.Text = ""
            LblLocationName.Text = ""
            TxtRoundoff.Text = ""
            lblTotRAmt1.Text = ""
        End If

        dt = Nothing
        qry = Nothing
    End Sub
    Sub LoadDispatchData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim dt As DataTable = Nothing
        Dim dblActualTCSBaseAmount As Double = 0
        Dim dblChangedTCSBaseAmount As Double = 0
        Dim qry As String = "Select TSPL_Dispatch_BulkSale.ChangedTCSBaseAmount,TSPL_Dispatch_BulkSale.ActualTCSBaseAmount,TSPL_Dispatch_BulkSale.Tax_Group,TSPL_Dispatch_BulkSale.Document_No,TSPL_Dispatch_BulkSale.Document_Date ,TSPL_Dispatch_BulkSale.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name , TSPL_LOCATION_MASTER.Location_Desc ,TSPL_Dispatch_BulkSale.Location_Code ,TSPL_Dispatch_BulkSale.Document_Date , TSPL_Dispatch_Detail_BulkSale.Qty_in_Ltr,TSPL_Dispatch_Detail_BulkSale.Item_Code,TSPL_ITEM_MASTER .Item_Desc  ,TSPL_Dispatch_Detail_BulkSale.Unit_code,TSPL_Dispatch_BulkSale.Tanker_Code ,TSPL_Dispatch_Detail_BulkSale.Qty ,TSPL_Dispatch_Detail_BulkSale.FatPer ,TSPL_Dispatch_Detail_BulkSale.SNFPer ,TSPL_Dispatch_Detail_BulkSale.NetMilkRate   ,TSPL_Dispatch_Detail_BulkSale.Amount, " &
"TSPL_DISPATCH_DETAIL_BULKSALE.TAX1,TSPL_DISPATCH_DETAIL_BULKSALE.TAX1_Base_Amt,TSPL_DISPATCH_DETAIL_BULKSALE.TAX1_Rate,TSPL_DISPATCH_DETAIL_BULKSALE.TAX1_Amt,TSPL_DISPATCH_DETAIL_BULKSALE.TAX2,TSPL_DISPATCH_DETAIL_BULKSALE.TAX2_Base_Amt,TSPL_DISPATCH_DETAIL_BULKSALE.TAX2_Rate,TSPL_DISPATCH_DETAIL_BULKSALE.TAX2_Amt,TSPL_DISPATCH_DETAIL_BULKSALE.TAX3,TSPL_DISPATCH_DETAIL_BULKSALE.TAX3_Base_Amt,TSPL_DISPATCH_DETAIL_BULKSALE.TAX3_Rate,TSPL_DISPATCH_DETAIL_BULKSALE.TAX3_Amt,TSPL_DISPATCH_DETAIL_BULKSALE.Total_Tax_Amt,TSPL_DISPATCH_DETAIL_BULKSALE.Item_Net_Amt " &
"from TSPL_Dispatch_BulkSale left Outer Join TSPL_Dispatch_Detail_BulkSale On TSPL_Dispatch_BulkSale .Document_No =TSPL_Dispatch_Detail_BulkSale.Document_No  " &
                            " Left outer join TSPL_CUSTOMER_MASTER On TSPL_Dispatch_BulkSale.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code  LEFT OUTER JOIN TSPL_LOCATION_MASTER On TSPL_Dispatch_BulkSale.Location_Code =TSPL_LOCATION_MASTER .Location_Code   Left outer jOin TSPL_ITEM_MASTER On TSPL_Dispatch_Detail_BulkSale.Item_Code =TSPL_ITEM_MASTER .Item_Code where TSPL_Dispatch_BulkSale .Document_No ='" & txtInvoiceNo.Value & "'"
        dt = clsDBFuncationality.GetDataTable(qry)
        loadBlankItemGrid()
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                txtInvoiceNo.Value = clsCommon.myCstr(dr("Document_No"))
                lblCustomerCode.Text = clsCommon.myCstr(dr("Customer_Code"))
                lblCustomerName.Text = clsCommon.myCstr(dr("Customer_Name"))
                lblLocationCode.Text = clsCommon.myCstr(dr("Location_Code"))
                LblLocationName.Text = clsCommon.myCstr(dr("Location_Desc"))
                dblActualTCSBaseAmount = clsCommon.myCdbl(dr("ActualTCSBaseAmount"))
                dblChangedTCSBaseAmount = clsCommon.myCdbl(dr("ChangedTCSBaseAmount"))
                rbtnTaxCalAutomatic.IsChecked = True
                SaleInvoiceDate = clsCommon.myCDate(dr("Document_Date"))
                lblDocumentAmount.Text = clsCommon.myCdbl(dr("Amount"))

                lblTaxAmt.Text = clsCommon.myCstr(clsCommon.myCdbl(dr("Total_Tax_Amt")))
                Dim DocuAmount As Double = clsCommon.myCdbl(dr("Item_Net_Amt"))

                If Math.Round(clsCommon.myCdbl(DocuAmount), 0) > clsCommon.myCdbl(DocuAmount) Then
                    TxtRoundoff.Text = clsCommon.myCstr(Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount), 0) - clsCommon.myCdbl(DocuAmount), 2))
                    lblTotRAmt1.Text = clsCommon.myCstr(Math.Round(clsCommon.myCdbl(DocuAmount), 0))
                Else
                    TxtRoundoff.Text = clsCommon.myCstr(Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount)) - clsCommon.myCdbl(DocuAmount), 2))
                    lblTotRAmt1.Text = clsCommon.myCstr(Math.Round(clsCommon.myCdbl(DocuAmount), 0))
                End If

                gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchNo).Value = clsCommon.myCstr(dr("Document_No"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchDate).Value = clsCommon.GetPrintDate(clsCommon.myCDate(dr("Document_Date")), "dd/MM/yyyy")
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr("Unit_code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerNo).Value = clsCommon.myCstr(dr("Tanker_Code"))

                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQty).Value = clsCommon.myCdbl(dr("Qty"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQtyLtr).Value = clsCommon.myCdbl(dr("Qty_in_Ltr"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchFatPer).Value = clsCommon.myCdbl(dr("FatPer"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchSnfPer).Value = clsCommon.myCdbl(dr("SNFPer"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchRate).Value = clsCommon.myCdbl(dr("NetMilkRate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = clsCommon.myCdbl(dr("Amount"))

                gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQty).Value = 0
                gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceFatPer).Value = 0
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSnfPer).Value = 0
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceRate).Value = 0
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value = 0
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceFatKG).Value = 0
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSNFKG).Value = 0

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(1)).Value = clsCommon.myCstr(dr("TAX1"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(1)).Value = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(1)).Value = clsCommon.myCdbl(dr("TAX1_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(1)).Value = clsCommon.myCdbl(dr("TAX1_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(2)).Value = clsCommon.myCstr(dr("TAX2"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(2)).Value = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(2)).Value = clsCommon.myCdbl(dr("TAX2_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(2)).Value = clsCommon.myCdbl(dr("TAX2_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(3)).Value = clsCommon.myCstr(dr("TAX3"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(3)).Value = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(3)).Value = clsCommon.myCdbl(dr("TAX3_Rate"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(3)).Value = clsCommon.myCdbl(dr("TAX3_Amt"))
                UpdateCurrentRow(gv1.Rows.Count - 1)
                txtTaxGroup.Enabled = False
                rbtnTaxCalAutomatic.IsChecked = True
                If TCSTaxApplicableOnbulkSale = True Then
                    txtTaxGroup.Value = clsCommon.myCstr(dr("Tax_Group"))
                    lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)

                    SetTaxDetails()
                    txttcstaxbaseamount.Value = dblChangedTCSBaseAmount
                    lblActualTCSTaxBaseAmt.Text = dblActualTCSBaseAmount
                End If
            Next
        Else
            txtInvoiceNo.Value = ""
            lblCustomerCode.Text = ""
            lblCustomerName.Text = ""
            lblLocationCode.Text = ""
            LblLocationName.Text = ""
            TxtRoundoff.Text = ""
            lblTotRAmt1.Text = ""
        End If

        dt = Nothing
        qry = Nothing
    End Sub
    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try

            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()

                If (ClsBulkSaleReturn.PostData(MyBase.Form_ID, "", txtDocNo.Value)) Then

                    common.clsCommon.MyMessageBoxShow("Successfully posted")
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim obj As ClsBulkSaleReturn = ClsBulkSaleReturn.GetData(strCode, "", NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtDocNo.Value = obj.Document_No
            txtDate.Value = obj.Document_Date
            chkCncelPSR.Enabled = False
            chkCncelPSR.Checked = IIf(obj.Is_Cancelled = 1, True, False)
            lblLocationCode.Text = obj.Location_Code
            LblLocationName.Text = obj.Location_Name
            lblCustomerCode.Text = obj.Customer_Code
            lblCustomerName.Text = obj.Customer_Name
            FndTanker.Value = obj.Tanker_No
            lblGateEntryNo.Text = obj.GateEntryNo
            lblTotRAmt1.Text = obj.Total_Amt
            TxtRoundoff.Text = obj.RoundOffAmount

            FndSiloNo.Value = obj.Silo_No
            LblSiloName.Text = obj.Silo_Name
            FndDispatchLocation.Value = obj.DispatchLocation
            lblDispatchLocation.Text = obj.DispatchLocationName
            If clsCommon.CompairString(obj.Against, "Bulk Invoice") = CompairStringResult.Equal Then
                rdbAgainstInvoice.IsChecked = True
                txtInvoiceNo.Value = obj.InvoiceNo
            Else
                rdbAgainstDispatch.IsChecked = True
                txtInvoiceNo.Value = obj.DispatchNo
            End If
            If obj.IsRejected = 1 Then
                chkRejected.Checked = True
            End If
            LoadBlankGridTax()
            txtTaxGroup.Value = obj.Tax_Group
            Dim objTaxGrpMaster As New clsTaxGroupMaster()
            objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
            If (objTaxGrpMaster IsNot Nothing) Then
                lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
            End If
            lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
            lblDocumentAmount.Text = clsCommon.myFormat(obj.Document_Amount)
            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(obj.ActualTCSBaseAmount)
            txttcstaxbaseamount.Value = clsCommon.myCdbl(obj.ChangedTCSBaseAmount)

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
                            Exit For
                        End If
                    Next
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX1) & "' ")), "Y") = CompairStringResult.Equal Then
                    txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
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
                            Exit For
                        End If
                    Next
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX2) & "' ")), "Y") = CompairStringResult.Equal Then
                    txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
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
                            Exit For
                        End If
                    Next
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX3) & "' ")), "Y") = CompairStringResult.Equal Then
                    txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
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
                            Exit For
                        End If
                    Next
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX4) & "' ")), "Y") = CompairStringResult.Equal Then
                    txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
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
                            Exit For
                        End If
                    Next
                End If
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(obj.TAX5) & "' ")), "Y") = CompairStringResult.Equal Then
                    txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
                End If
            End If
            If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                rbtnTaxCalAutomatic.IsChecked = True
            ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                rbtnTaxCalManual.IsChecked = True
            End If



            If obj.arrSaleReturnDetailBulkSale IsNot Nothing AndAlso obj.arrSaleReturnDetailBulkSale.Count > 0 Then
                loadBlankItemGrid()
                For Each objTr As ClsSaleReturnDetailBulkSale In obj.arrSaleReturnDetailBulkSale
                    'gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchNo).Value = objTr.Dispatch_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchDate).Value = clsCommon.GetPrintDate(objTr.Dispatch_Date, "dd/MM/yyyy")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = objTr.Unit_code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerNo).Value = objTr.Tanker_Code

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQty).Value = objTr.DispatchQty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQtyLtr).Value = objTr.DispatchQty_in_Ltr
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchFatPer).Value = objTr.DispatchFatPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchSnfPer).Value = objTr.DispatchSNFPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchRate).Value = objTr.DispatchRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = objTr.DispatchAmount


                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQty).Value = objTr.InvoiceQty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceQtyLtr).Value = objTr.InvoiceQty_in_Ltr
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceFatPer).Value = objTr.InvoiceFatPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSnfPer).Value = objTr.InvoiceSNFPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceRate).Value = objTr.InvoiceRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value = objTr.InvoiceAmount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceFatKG).Value = objTr.InvoiceFatKG
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSNFKG).Value = objTr.InvoiceSNFKG

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(1)).Value = objTr.TAX1
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(1)).Value = objTr.TAX1_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(1)).Value = objTr.TAX1_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(1)).Value = objTr.TAX1_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(2)).Value = objTr.TAX2
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(2)).Value = objTr.TAX2_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(2)).Value = objTr.TAX2_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(2)).Value = objTr.TAX2_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(3)).Value = objTr.TAX3
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(3)).Value = objTr.TAX3_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(3)).Value = objTr.TAX3_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(3)).Value = objTr.TAX3_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(4)).Value = objTr.TAX4
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(4)).Value = objTr.TAX4_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(4)).Value = objTr.TAX4_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(4)).Value = objTr.TAX4_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(5)).Value = objTr.TAX5
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(5)).Value = objTr.TAX5_Base_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(5)).Value = objTr.TAX5_Rate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(5)).Value = objTr.TAX5_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objTr.Total_Tax_Amt
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = objTr.Item_Net_Amt


                    gv1.Rows.AddNew()
                Next
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            Else
                gv1.DataSource = Nothing
            End If
            againstSub()

            txtDocNo.MyReadOnly = True
            btnsave.Text = "Update"
            ' btndelete.Enabled = True
            If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
                btnInvoiceJE.Visible = True
            Else
                btnPost.Enabled = True
                btnsave.Enabled = True
                btndelete.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
                btnInvoiceJE.Visible = False
            End If
            UcAttachment1.LoadData(obj.Document_No)
        Else
            Reset()

        End If
        obj = Nothing
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsBulkSaleReturn.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SaveData()
        Dim obj As New ClsBulkSaleReturn()
        Dim objTr As New ClsSaleReturnDetailBulkSale
        Try
            If AllowToSave() Then
                Dim DocuAmount As Double = 0
                obj.Is_Cancelled = IIf(chkCncelPSR.Checked, 1, 0)
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Customer_Code = lblCustomerCode.Text
                obj.Tanker_No = FndTanker.Value
                obj.GateEntryNo = lblGateEntryNo.Text
                obj.Location_Code = lblLocationCode.Text

                obj.Total_Amt = lblTotRAmt1.Text
                obj.RoundOffAmount = TxtRoundoff.Text
                obj.Silo_No = FndSiloNo.Value
                obj.DispatchLocation = FndDispatchLocation.Value
                obj.DispatchLocationName = lblDispatchLocation.Text
                obj.arrSaleReturnDetailBulkSale = New List(Of ClsSaleReturnDetailBulkSale)
                If chkRejected.Checked Then
                    obj.IsRejected = 1
                Else
                    obj.IsRejected = 0
                End If
                If rdbAgainstInvoice.IsChecked Then
                    obj.Against = "Bulk Invoice"
                    obj.InvoiceNo = txtInvoiceNo.Value
                Else
                    obj.Against = "Bulk Dispatch"
                    obj.DispatchNo = txtInvoiceNo.Value
                End If

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


                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If
                obj.Document_Amount = lblDocumentAmount.Text
                obj.Total_Tax_Amt = lblTaxAmt.Text
                obj.ActualTCSBaseAmount = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                obj.ChangedTCSBaseAmount = clsCommon.myCdbl(txttcstaxbaseamount.Value)



                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New ClsSaleReturnDetailBulkSale()
                    objTr.Document_No = clsCommon.myCstr(obj.Document_No)
                    objTr.Dispatch_Code = clsCommon.myCstr(grow.Cells(colDispatchNo).Value)
                    objTr.Dispatch_Date = clsCommon.myCDate(grow.Cells(colDispatchDate).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    objTr.Tanker_Code = clsCommon.myCstr(grow.Cells(colTankerNo).Value)
                    objTr.DispatchQty = clsCommon.myCdbl(grow.Cells(colDispatchQty).Value)
                    objTr.DispatchFatPer = clsCommon.myCdbl(grow.Cells(colDispatchFatPer).Value)
                    objTr.DispatchSNFPer = clsCommon.myCdbl(grow.Cells(colDispatchSnfPer).Value)
                    objTr.DispatchRate = clsCommon.myCdbl(grow.Cells(colDispatchRate).Value)
                    objTr.DispatchAmount = clsCommon.myCdbl(grow.Cells(colDispatchAmount).Value)
                    objTr.DispatchQty_in_Ltr = Math.Round(clsCommon.myCdbl(grow.Cells(colDispatchQtyLtr).Value), 0)
                    objTr.InvoiceQty_in_Ltr = Math.Round(clsCommon.myCdbl(grow.Cells(colInvoiceQtyLtr).Value), 0)

                    objTr.InvoiceQty = clsCommon.myCdbl(grow.Cells(ColInvoiceQty).Value)
                    objTr.InvoiceFatPer = clsCommon.myCdbl(grow.Cells(ColInvoiceFatPer).Value)
                    objTr.InvoiceSNFPer = clsCommon.myCdbl(grow.Cells(colInvoiceSnfPer).Value)
                    objTr.InvoiceRate = clsCommon.myCdbl(grow.Cells(colInvoiceRate).Value)
                    objTr.InvoiceAmount = clsCommon.myCdbl(grow.Cells(colInvoiceAmount).Value)
                    objTr.InvoiceFatKG = clsCommon.myCdbl(grow.Cells(colInvoiceFatKG).Value)
                    objTr.InvoiceSNFKG = clsCommon.myCdbl(grow.Cells(colInvoiceSNFKG).Value)

                    objTr.TAX1 = clsCommon.myCstr(grow.Cells(colTax + clsCommon.myCstr(1)).Value)
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt + clsCommon.myCstr(1)).Value)
                    objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate + clsCommon.myCstr(1)).Value)
                    objTr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt + clsCommon.myCstr(1)).Value)
                    objTr.TAX2 = clsCommon.myCstr(grow.Cells(colTax + clsCommon.myCstr(2)).Value)
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt + clsCommon.myCstr(2)).Value)
                    objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate + clsCommon.myCstr(2)).Value)
                    objTr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt + clsCommon.myCstr(2)).Value)
                    objTr.TAX3 = clsCommon.myCstr(grow.Cells(colTax + clsCommon.myCstr(3)).Value)
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt + clsCommon.myCstr(3)).Value)
                    objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate + clsCommon.myCstr(3)).Value)
                    objTr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt + clsCommon.myCstr(3)).Value)
                    objTr.TAX4 = clsCommon.myCstr(grow.Cells(colTax + clsCommon.myCstr(4)).Value)
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt + clsCommon.myCstr(4)).Value)
                    objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate + clsCommon.myCstr(4)).Value)
                    objTr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt + clsCommon.myCstr(4)).Value)
                    objTr.TAX5 = clsCommon.myCstr(grow.Cells(colTax + clsCommon.myCstr(5)).Value)
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt + clsCommon.myCstr(5)).Value)
                    objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate + clsCommon.myCstr(5)).Value)
                    objTr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt + clsCommon.myCstr(5)).Value)
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                    If objTr.Total_Tax_Amt > 0 Then
                        objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)
                    Else
                        objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colDispatchAmount).Value)
                    End If

                    obj.arrSaleReturnDetailBulkSale.Add(objTr)

                Next


                If (ClsBulkSaleReturn.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_No)
                    If Not isFlag Then

                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                        LoadData(obj.Document_No, NavigatorType.Current)

                    End If

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            obj = Nothing
            objTr = Nothing
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Focus()
            txtDate.Select()
            Return False
        End If
        If clsCommon.myLen(FndTanker.Value) <= 0 Then
            FndTanker.Focus()
            Throw New Exception("Tanker No cannot be left blank")
        End If
        If clsCommon.myLen(txtInvoiceNo.Value) <= 0 Then
            txtInvoiceNo.Focus()

            If rdbAgainstInvoice.IsChecked Then
                Throw New Exception("Invoice No cannot be left blank")
            Else
                Throw New Exception("Dispatch No cannot be left blank")
            End If

        End If
        If rdbAgainstDispatch.IsChecked Then
            If chkRejected.Checked = False Then
                Throw New Exception("Please check Rejected check box")
            End If
            If clsCommon.myLen(FndDispatchLocation.Value) <= 0 Then
                FndDispatchLocation.Focus()
                Throw New Exception("Dispatch Location cannot be left blank")
            End If
        End If
        If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date,Document_Date,103) from TSPL_INVOICE_MASTER_BULKSALE where Document_No ='" + txtInvoiceNo.Value + "'")) > clsCommon.myCDate(txtDate.Value) Then
            txtDate.Focus()
            Throw New Exception("Date cannot be less than from Invoice Date")
        End If
        Return True
    End Function

    Private Sub FndSiloNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndSiloNo._MYValidating
        FndSiloNo.Value = clsLocation.getFinder("is_sub_location='Y' and Loc_Segment_Code in (Select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + lblLocationCode.Text + "')", FndSiloNo.Value, isButtonClicked)
        If clsCommon.myLen(FndSiloNo.Value) > 0 Then
            LblSiloName.Text = clsLocation.GetName(FndSiloNo.Value, Nothing)
        Else
            LblSiloName.Text = ""
        End If
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_SALE_RETURN_MASTER_BULKSALE where Document_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "Select TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No as Code,Convert(varchar,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) as [Sale Return Date],TSPL_SALE_RETURN_MASTER_BULKSALE.GateEntryNo as [Gate Entry No],TSPL_SALE_RETURN_MASTER_BULKSALE.Tanker_No as [Tanker No],TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo as [Invoice No],TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_SALE_RETURN_MASTER_BULKSALE.Silo_No as [Silo No],SubLocation.Location_Desc as [Silo Name],case when TSPL_SALE_RETURN_MASTER_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_SALE_RETURN_MASTER_BULKSALE left outer Join TSPL_CUSTOMER_MASTER on TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_LOCATION_MASTER as SubLocation on TSPL_SALE_RETURN_MASTER_BULKSALE.Silo_No =SubLocation.Location_Code"
        txtDocNo.Value = clsCommon.ShowSelectForm("ReturnBulkSale", qry, "Code", "", txtDocNo.Value, "", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub


    Private Sub FndTanker__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndTanker._MYValidating
        'Dim qry As String = "Select TSPL_GATEENTRY_SALE.Tanker_No as [TankerNo],TSPL_GATEENTRY_SALE.Document_No as [Gate Entry No],TSPL_GATEENTRY_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_GATEENTRY_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] from TSPL_GATEENTRY_SALE Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEENTRY_SALE.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEENTRY_SALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code where TSPL_GATEENTRY_SALE.IsSaleReturn ='Y' and ISNULL(TSPL_GATEENTRY_SALE.SaleReturnAgaintGEN,'') <>'' and TSPL_GATEENTRY_SALE.Document_No not in (Select GateEntryNo from TSPL_SALE_RETURN_MASTER_BULKSALE where Document_No <>'" & txtDocNo.Value & "') "
        ' Dim qry As String = "Select TSPL_GATEENTRY_SALE.Document_No,TSPL_GATEENTRY_SALE.Tanker_No as [TankerNo],TSPL_GATEENTRY_SALE.Document_No as [Gate Entry No],TSPL_GATEENTRY_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_GATEENTRY_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] from TSPL_GATEENTRY_SALE Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEENTRY_SALE.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEENTRY_SALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left outer join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code =TSPL_GATEENTRY_SALE.Dispatch_No where TSPL_GATEENTRY_SALE.IsSaleReturn ='Y' and TSPL_GATEENTRY_SALE.Posted=1 and ISNULL(TSPL_GATEENTRY_SALE.SaleReturnAgaintGEN,'') <>'' and not EXISTS (Select 1 from TSPL_SALE_RETURN_MASTER_BULKSALE where Document_No <>'" & txtDocNo.Value & "' AND TSPL_SALE_RETURN_MASTER_BULKSALE.GateEntryNo =TSPL_GATEENTRY_SALE.Document_No )"
        If rdbAgainstInvoice.IsChecked Then
            Dim qry As String = "Select TSPL_GATEENTRY_SALE.Dispatch_No as [Dispatch No],TSPL_GATEENTRY_SALE.Tanker_No as [TankerNo],TSPL_GATEENTRY_SALE.Document_No as [Gate Entry No],TSPL_GATEENTRY_SALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_GATEENTRY_SALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name] from TSPL_GATEENTRY_SALE Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_GATEENTRY_SALE.Customer_Code =TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_GATEENTRY_SALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code where TSPL_GATEENTRY_SALE.IsSaleReturn ='Y' and TSPL_GATEENTRY_SALE.Posted=1 and ISNULL(TSPL_GATEENTRY_SALE.SaleReturnAgaintGEN,'') <>'' and not EXISTS (Select 1 from TSPL_SALE_RETURN_MASTER_BULKSALE where Document_No <>'" & txtDocNo.Value & "' AND TSPL_SALE_RETURN_MASTER_BULKSALE.GateEntryNo =TSPL_GATEENTRY_SALE.Document_No and TSPL_SALE_RETURN_MASTER_BULKSALE.Against ='Bulk Invoice' )"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("SaleReturnTnkrNo", qry)
            If Not dr Is Nothing Then
                FndTanker.Value = clsCommon.myCstr(dr("TankerNo"))
                lblGateEntryNo.Text = clsCommon.myCstr(dr("Gate Entry No"))
                txtInvoiceNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_No from TSPL_INVOICE_DETAIL_BULKSALE where Dispatch_Code ='" & clsCommon.myCstr(dr("Dispatch No")) & "'"))
                LoadInvoiceData(txtInvoiceNo.Value, NavigatorType.Current)
            Else
                FndTanker.Value = ""
                lblGateEntryNo.Text = ""
                txtInvoiceNo.Value = ""
            End If
            qry = Nothing
            dr = Nothing
        Else
            Dim qry As String = "Select TSPL_Dispatch_BulkSale.Tanker_Code as Tanker,TSPL_Dispatch_BulkSale.Location_Code AS Location,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Dispatch_BulkSale.Document_No as [Dispatch No],TSPL_Quality_Check_BulkSale.GateEntry_Document_No as [Gate Entry No]  from TSPL_Dispatch_BulkSale Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_Dispatch_BulkSale.Location_Code " & _
                                " Left Outer Join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale .QC_No =TSPL_Dispatch_BulkSale.QC_Code " & _
                                " Left Outer Join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_Dispatch_BulkSale.Document_No =ISNULL(TSPL_SALE_RETURN_MASTER_BULKSALE.DispatchNo ,'')" & _
                                 " where TSPL_Dispatch_BulkSale.Posted =1 and TSPL_Dispatch_BulkSale.Document_No not in  (Select TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE Left outer join TSPL_INVOICE_MASTER_BULKSALE on TSPL_INVOICE_MASTER_BULKSALE.Document_No =TSPL_INVOICE_DETAIL_BULKSALE .Document_No where TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst = 'Against Dispatch' )" & _
                                 " and TSPL_Dispatch_BulkSale.Document_No not in (Select DispatchNo from TSPL_SALE_RETURN_MASTER_BULKSALE where Against ='Bulk Dispatch' and Document_No <>'" & txtDocNo.Value & "' )"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("SaleTnkrNo", qry)
            If Not dr Is Nothing Then
                FndTanker.Value = clsCommon.myCstr(dr("Tanker"))
                lblGateEntryNo.Text = clsCommon.myCstr(dr("Gate Entry No"))
                txtInvoiceNo.Value = clsCommon.myCstr(dr("Dispatch No"))
                LoadDispatchData(txtInvoiceNo.Value, NavigatorType.Current)
            Else
                FndTanker.Value = ""
                lblGateEntryNo.Text = ""
                txtInvoiceNo.Value = ""
            End If
            qry = Nothing
            dr = Nothing
        End If

    End Sub



    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            PrintData(txtDocNo.Value, rdbAgainstInvoice.IsChecked)
        Else
            clsCommon.MyMessageBoxShow("Document no. not found")
        End If

    End Sub
    Sub PrintData(ByVal StrCode As String, ByVal AgainstInv As Boolean)
        '==========Added by Shivani Tyagi
        Dim Qry As String = String.Empty
        Dim dt As DataTable = Nothing
        Dim PrintTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToPrintTimeWithDocumentDate, clsFixedParameterCode.AllowToPrintTimeWithDocumentDate, Nothing)
        If clsCommon.myLen(txtDocNo.Value) > 0 OrElse clsCommon.myLen(StrCode) > 0 Then
            Dim strfield As String = String.Empty
            ''richa agarwal against ticket no BM00000006084 06/04/2015
            If rdbAgainstInvoice.IsChecked Then
                strfield = " InvoiceNo ,InvoiceQty,InvoiceRate ,InvoiceAmount ,"
            Else
                strfield = "DispatchNo as InvoiceNo ,DispatchQty as InvoiceQty,DispatchRate as InvoiceRate ,DispatchAmount as InvoiceAmount ,"
            End If
            ''-------------------------
            Qry = "select TSPL_SALE_RETURN_MASTER_BULKSALE. Document_No"
            If PrintTime = "1" Then
                Qry += " ,Document_Date"
            Else
                Qry += " ,convert(varchar,Document_Date,103)as Document_Date"

            End If
            Qry += " ,Customer_Name ,Item_Desc,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code ,Total_Amt ," & strfield & "  TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end " & _
            " + case when len(TSPL_LOCATION_MASTER.Pin_Code   )>0 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code as varchar)  else ' ' end" & _
            " + case when len(TSPL_LOCATION_MASTER.Email    )>0 then TSPL_LOCATION_MASTER.Email else '' end +Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_LOCATION_MASTER.Phone1 end " & _
            " +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_LOCATION_MASTER.Phone2 Else'' End  " & _
            " as lOC_Address,Comp_Name ,RoundOffAmount    from TSPL_SALE_RETURN_DETAIL_BULKSALE left join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No =TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALE_RETURN_MASTER_BULKSALE .customer_code left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code    left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_SALE_RETURN_MASTER_BULKSALE.comp_code" & _
             " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " & _
             " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State" & _
             " where 2=2  and  TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No = '" + StrCode + "'"
            dt = clsDBFuncationality.GetDataTable(Qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptBulkSaleReturn", "Sales Return", "rptCompanyAddress.rpt")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow("Please select an invoice to print")
        End If
        Qry = Nothing
        dt = Nothing
    End Sub

    Private Sub rdbAgainstDispatch_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbAgainstDispatch.ToggleStateChanged
        againstSub()
    End Sub
    Sub againstSub()
        If rdbAgainstDispatch.IsChecked Then
            chkRejected.Enabled = True
            MyLabel1.Text = "Dispatch No"
            FndSiloNo.Enabled = False
            FndDispatchLocation.Enabled = True
            txtInvoiceNo.Enabled = True
        Else
            chkRejected.Enabled = False
            MyLabel1.Text = "Invoice No"
            FndSiloNo.Enabled = True
            FndDispatchLocation.Enabled = False
            txtInvoiceNo.Enabled = False
        End If
    End Sub
    Private Sub rdbAgainstInvoice_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbAgainstInvoice.ToggleStateChanged
        againstSub()
    End Sub

    Private Sub FndDispatchLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndDispatchLocation._MYValidating
        ''richa agarwal against ticket no. BM00000006083 06/04/2015
        Dim strwhrcls As String = String.Empty
        strwhrcls = " Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' "
        If clsCommon.myLen(lblLocationCode.Text) > 0 Then
            strwhrcls += " and Location_Code<>'" & lblLocationCode.Text & "'"
        End If
        FndDispatchLocation.Value = clsLocation.getFinder(strwhrcls, FndDispatchLocation.Value, isButtonClicked)

        If clsCommon.myLen(FndDispatchLocation.Value) > 0 Then
            lblDispatchLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndDispatchLocation.Value & "'"))
        Else
            lblDispatchLocation.Text = ""
        End If
    End Sub

 
    Private Sub btnInvoiceJE_Click(sender As Object, e As EventArgs) Handles btnInvoiceJE.Click
        clsOpenJEAgainstInvoice.ShowInvoiceJEForReturn(txtDocNo.Value)
    End Sub
    ' Ticket No : TEC/29/10/18-000350 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Try
            If TCSTaxApplicableOnbulkSale = True Then
                If clsCommon.myLen(lblLocationCode.Text) > 0 Then
                    Dim strCustomer As String = ""
                    If clsCommon.myLen(strCustomer) <= 0 Then
                        strCustomer = lblCustomerCode.Text
                    End If

                    txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, lblLocationCode.Text, strCustomer, "S", txtDate.Value)
                    lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)


                    SetTaxDetails()

                Else
                    Throw New Exception("Please select Tanker First")
                End If
            Else
                txtTaxGroup.Value = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Function taxDetailsInvoice() As String
        Dim qry As String = "select final.Tax_Group_Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc,final.Tax_Code,TSPL_TAX_MASTER.Tax_Code_Desc,final.TaxRate,Surtax,Surtax_Tax_Code,TSPL_TAX_GROUP_DETAILS.Tax_On_Base_Amount,TSPL_TAX_GROUP_DETAILS.Taxable, TSPL_TAX_MASTER.Excisable , TSPL_TAX_MASTER.Tax_Recoverable,TSPL_TAX_MASTER.Type,TSPL_TAX_MASTER.IS_TCS from 
(select Tax_Group as Tax_Group_Code,tax1 as Tax_Code,tax1_rate as TaxRate from " & IIf(rdbAgainstInvoice.IsChecked = True, "tspl_invoice_master_bulkSale", "tspl_Dispatch_bulkSale") & " where document_no ='" & txtInvoiceNo.Value & "'
union all
select Tax_Group as Tax_Group_Code,tax2 as Tax_Code,tax2_rate as TaxRate from " & IIf(rdbAgainstInvoice.IsChecked = True, "tspl_invoice_master_bulkSale", "tspl_Dispatch_bulkSale") & " where document_no ='" & txtInvoiceNo.Value & "'
union all
select Tax_Group as Tax_Group_Code, tax3 as Tax_Code,tax3_rate as TaxRate from " & IIf(rdbAgainstInvoice.IsChecked = True, "tspl_invoice_master_bulkSale", "tspl_Dispatch_bulkSale") & " where document_no ='" & txtInvoiceNo.Value & "')Final
left outer join TSPL_TAX_MASTER on TSPL_TAX_MASTER.Tax_Code=Final.Tax_Code
left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=Final.Tax_Group_Code 
        left outer join TSPL_TAX_GROUP_DETAILS on TSPL_TAX_GROUP_DETAILS.Tax_Group_Code=TSPL_TAX_GROUP_MASTER.Tax_Group_Code where ISNULL(final.Tax_Group_Code,'')<>'' AND ISNULL(final.Tax_Code,'')<>''"
        Return qry
    End Function
    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(taxDetailsInvoice())

        'dt = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", lblCustomerCode.Text, lblLocationCode.Text, True)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If (dt.Rows.Count > 5) Then
                MessageBox.Show("Can't Handle More than 5 Tax Types in a Group")
                Return
            End If
            lblTaxGrpName.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            For Each dr As DataRow In dt.Rows
                gv2.Rows.AddNew()
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutCode).Value = clsCommon.myCstr(dr("Tax_Code"))
                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = clsCommon.myCstr(dr("Tax_Code_Desc"))
                If rbtnTaxCalAutomatic.IsChecked Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & lblCustomerCode.Text & "'")), "0") = CompairStringResult.Equal Then
                            If (clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Is_TCS from tspl_tax_master where Tax_Code='" & clsCommon.myCstr(dr("Tax_Code")) & "'")), "Y") = CompairStringResult.Equal) AndAlso Not (SaleInvoiceDate.Month() = txtDate.Value.Month() And SaleInvoiceDate.Year() = txtDate.Value.Year()) Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                            Else
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                        Else
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                        End If
                        txtTCSTaxRate.Value = clsCommon.myCdbl(gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value)
                    Else
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
                    End If

                End If
            Next
            SetitemWiseTaxSetting(True, False)
        Else
            lblTaxGrpName.Text = ""
            For ii As Integer = 0 To gv1.Rows.Count - 1
                BlankTaxDetails(ii)
            Next
        End If

        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
        UpdateAllTotals()
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            Dim dblAmtAfterDis As Double = 0
            Dim arrTaxableAuth As New List(Of String)
            Dim FatRate As Double = 0
            Dim SNFRate As Double = 0
            Dim StandardRate As Double = 0
            Dim FatWeightage As Double = 0
            Dim SNFWeightage As Double = 0
            Dim FatPer As Double = 0
            Dim SNFPer As Double = 0
            Dim FatRatio As Double = 0
            Dim SNFRatio As Double = 0
            Dim FatKG As Double = 0
            Dim SNFKG As Double = 0
            Dim Qty As Double = 0
            Dim FatAmount As Double = 0
            Dim SNFAmount As Double = 0
            Dim Amount As Double = 0

            For ii As Integer = 1 To 5
                Dim Strii As String = clsCommon.myCstr(ii)
                If rbtnTaxCalAutomatic.IsChecked Then
                    Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                        Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                        Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                        Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                        Dim IsExcisable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value)
                        Dim IsTaxonBaseAmount As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(colIsTaxOnBaseAmount + clsCommon.myCstr(ii)).Value)
                        If rdbAgainstInvoice.IsChecked Then
                            dblAmtAfterDis = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInvoiceAmount).Value)
                        Else
                            dblAmtAfterDis = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDispatchAmount).Value)
                        End If

                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0
                        If IsSurTax Then
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            If Not IsTaxonBaseAmount Then
                                dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                            End If
                            If IsTaxonBaseAmount AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                                Dim dblTotalBasicPrice As Double = 0
                                For n As Integer = 0 To gv1.Rows.Count - 1
                                    If clsCommon.myLen(gv1.Rows(n).Cells(colItemCode).Value) > 0 Then
                                        dblTotalBasicPrice = dblTotalBasicPrice + clsCommon.myCdbl(gv1.Rows(n).Cells(colDispatchAmount).Value)
                                    End If
                                Next

                                dblBaseAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDispatchAmount).Value) * clsCommon.myCdbl(txttcstaxbaseamount.Value)) / dblTotalBasicPrice
                            Else
                                dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                            End If
                        End If
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                        dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 6)
                        If IsTaxable AndAlso Not arrTaxableAuth.Contains(strTaxCode.ToUpper()) Then
                            arrTaxableAuth.Add(strTaxCode.ToUpper())
                        End If

                    Else
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + Strii)).Value = Nothing
                    End If
                ElseIf rbtnTaxCalManual.IsChecked Then
                    If gv2.Rows.Count >= ii Then
                        Dim dblTaxAmt As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                        Dim dblCurrRowAmt As Double = clsCommon.myCdbl(gv1.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colInvoiceAmount).Value)
                        Dim dblTotAmt As Double = 0
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colInvoiceAmount).Value)
                        Next
                        Dim dblCurrCalTax As Double = 0
                        If dblTotAmt <> 0 Then
                            dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                        End If
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = dblCurrCalTax
                    End If
                End If
            Next
            Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
            Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotTaxAmt
            gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
            gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = Math.Round(dblAmtAfterTax, 2)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub UpdateAllTotals()
        Try
            Dim dblTotAmt As Double = 0

            Dim dblTotDisAmt As Double = 0
            Dim dblAmtAfterDis As Double = 0
            Dim dblTaxBaseAmt1 As Double = 0
            Dim dblTaxBaseAmt2 As Double = 0
            Dim dblTaxBaseAmt3 As Double = 0
            Dim dblTaxBaseAmt4 As Double = 0
            Dim dblTaxBaseAmt5 As Double = 0
            Dim dblTaxAmt1 As Double = 0
            Dim dblTaxAmt2 As Double = 0
            Dim dblTaxAmt3 As Double = 0
            Dim dblTaxAmt4 As Double = 0
            Dim dblTaxAmt5 As Double = 0
            Dim dblTaxTotAmt As Double = 0
            Dim dblNetAmt As Double = 0


            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim Strii As String = clsCommon.myCstr(ii + 1)
                If (clsCommon.myLen(gv1.Rows(ii).Cells(colDispatchNo).Value) > 0) Then
                    If rdbAgainstInvoice.IsChecked Then
                        dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colInvoiceAmount).Value)
                    Else
                        dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDispatchAmount).Value)
                    End If


                    dblTaxAmt1 = dblTaxAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + clsCommon.myCstr(1)).Value)
                    dblTaxAmt2 = dblTaxAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + clsCommon.myCstr(2)).Value)
                    dblTaxAmt3 = dblTaxAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + clsCommon.myCstr(3)).Value)
                    dblTaxAmt4 = dblTaxAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + clsCommon.myCstr(4)).Value)
                    dblTaxAmt5 = dblTaxAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt + clsCommon.myCstr(5)).Value)

                    dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + clsCommon.myCstr(1)).Value)
                    dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + clsCommon.myCstr(2)).Value)
                    dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + clsCommon.myCstr(3)).Value)
                    dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + clsCommon.myCstr(4)).Value)
                    dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt + clsCommon.myCstr(5)).Value)

                    dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                    dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterTax).Value)

                End If


            Next

            If rbtnTaxCalAutomatic.IsChecked Then

                For ii As Integer = 1 To gv2.Rows.Count
                    Select Case (ii)
                        Case 1
                            If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                                lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1)
                                dblTaxBaseAmt1 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                                dblTaxAmt1 = (dblTaxBaseAmt1 * txtTCSTaxRate.Value) / 100
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                            If dblTaxBaseAmt1 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 2
                            If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                                lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1)
                                dblTaxBaseAmt2 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                                dblTaxAmt2 = (dblTaxBaseAmt2 * txtTCSTaxRate.Value) / 100
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                            If dblTaxBaseAmt2 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 3
                            If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                                lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1 + dblTaxAmt2)
                                dblTaxBaseAmt3 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                                dblTaxAmt3 = (dblTaxBaseAmt3 * txtTCSTaxRate.Value) / 100
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                            If dblTaxBaseAmt3 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 4
                            If (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code='" & clsCommon.myCstr(gv2.Rows(ii - 1).Cells(colTTaxAutCode).Value) & "' AND Is_TCS ='Y'")) > 0) AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                                lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTaxBaseAmt1 + dblTaxAmt1 + dblTaxAmt2 + dblTaxAmt3)
                                dblTaxBaseAmt4 = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                                dblTaxAmt4 = (dblTaxBaseAmt4 * txtTCSTaxRate.Value) / 100
                            End If
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
                    End Select
                Next
            End If
            lblDocumentAmount.Text = clsCommon.myFormat(dblTotAmt)
            lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
            lblTotRAmt1.Text = clsCommon.myFormat(clsCommon.myCdbl(dblTaxTotAmt) + clsCommon.myCdbl(dblTotAmt))
            lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(dblTotAmt)



            If Math.Round(clsCommon.myCdbl(lblTotRAmt1.Text), 0, MidpointRounding.AwayFromZero) > clsCommon.myCdbl(lblTotRAmt1.Text) Then
                TxtRoundoff.Text = Math.Round(Math.Round(clsCommon.myCdbl(lblTotRAmt1.Text), 0, MidpointRounding.AwayFromZero) - clsCommon.myCdbl(lblTotRAmt1.Text), 2)
                lblTotRAmt1.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt1.Text), 0, MidpointRounding.AwayFromZero)
            Else
                TxtRoundoff.Text = Math.Round(Math.Round(clsCommon.myCdbl(lblTotRAmt1.Text)) - clsCommon.myCdbl(lblTotRAmt1.Text), 2)
                lblTotRAmt1.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt1.Text), 0)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        ' Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", lblCustomerCode.Text, lblLocationCode.Text, True)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(taxDetailsInvoice())
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
                If clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If isChangeRate Then
                            gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = dr("TaxRate")
                        End If

                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    BlankTaxDetails(intRowNo, isChangeRate)
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colItemCode).Value) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If isChangeRate Then
                                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = dr("TaxRate")
                            End If
                            ''tcs tax rate
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & lblCustomerCode.Text & "'")), "0") = CompairStringResult.Equal Then
                                    If (clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Is_TCS from tspl_tax_master where Tax_Code='" & clsCommon.myCstr(dr("Tax_Code")) & "'")), "Y") = CompairStringResult.Equal) AndAlso Not (SaleInvoiceDate.Month() = txtDate.Value.Month() And SaleInvoiceDate.Year() = txtDate.Value.Year()) Then
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                    Else
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                    End If
                                Else
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                End If

                            End If
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("RECOVERTABLETAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_Recoverable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
    End Sub

    Sub SetitemWiseTaxOnlySetting()
        'Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(taxDetailsInvoice())
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colItemCode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Next
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
        repoTaxRate.ReadOnly = True
        repoTaxRate.IsVisible = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowTaxRateColumnOnTransaction, clsFixedParameterCode.ShowTaxRateColumnOnTransaction, Nothing)) = 1, True, False)
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

        gv1.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False

    End Sub
    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 5
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            Else
                dblTotTax = dblTotTax + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value)
            End If
        Next
        Return dblTotTax
    End Function
    Private Function GetCurrentRowOtherTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal arrTaxableAuth As List(Of String)) As Double
        Dim dblRet As Double = 0
        For Each strTaxAuth As String In arrTaxableAuth
            For ii As Integer = 1 To intEndCol
                Dim strii As String = clsCommon.myCstr(ii)
                If IntRowNo < 0 Then
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                Else
                    If clsCommon.CompairString(strTaxAuth, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                        dblRet = dblRet + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                    End If
                End If
            Next
        Next
        Return dblRet
    End Function
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        BlankTaxDetails(intRowNo, True)
    End Sub

    Private Sub BlankTaxDetails(ByVal intRowNo As Integer, ByVal isBlankRate As Boolean)
        For ii As Integer = 1 To 5
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                If isBlankRate Then
                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                    ' gv1.CurrentRow.Cells(colItemwiseTaxCode).Value = Nothing
                End If
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing

            Else
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                If isBlankRate Then
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                End If
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing

            End If
        Next
    End Sub


End Class
