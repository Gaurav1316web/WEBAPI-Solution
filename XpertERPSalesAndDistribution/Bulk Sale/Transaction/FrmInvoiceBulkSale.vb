
'--------Created By Richa 05/08/2014 Against Ticket No BM00000003249
''-------------updation by richa Against Ticket no BM00000003774 and BM00000003113 and BM00000003853,BM00000003854,BM00000004068,BM00000005028,BM00000005027,BM00000005676
'' Updation By Richa Agarwal Against Ticket No. BM00000004013 on 15/09/2014,Against Ticket No BM00000004059 on 25/09/2014,BM00000004274 on 15/10/2014,BM00000004416,BM00000006412,BM00000006533,BM00000006952
'Sanjay Ticket No- ERO/11/07/18-000367  Add Qty in Ltr in grid
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class FrmInvoiceBulkSale
    Inherits FrmMainTranScreen
#Region "Variables"
    Public AllowtoChangeTCSBaseAmount As Boolean = False
    Dim AmountToCheckCustomerOutstandingForTCSTax As Double = 0
    Dim EnableTCSRateValidityFrom01July2021 As Boolean = False
    Dim AllowFatPerInanynumberofMultipesonBulkQC As Boolean = False
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean
    Dim TCSTaxApplicableOnbulkSale As Boolean = False
    Public isInsideLoadData As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colDispatchNo As String = "colDispatchNo"
    Public Const colDispatchDate As String = "colDispatchDate"
    Public Const colTankerNo As String = "colTankerNo"
    Public Const colItemCode As String = "colItemCode"
    Public Const colItemDesc As String = "colItemDesc"
    Public Const colHSNCode As String = "HSNCode"
    Public Const colUnitCode As String = "colUnitCode"
    Public Const colDispatchQty As String = "colDispatchQty"
    Public Const colDispatchQtyLtr As String = "colDispatchQtyLtr"
    Public Const colDispatchFatPer As String = "colDispatchFatPer"
    Public Const colDispatchSnfPer As String = "colDispatchSnfPer"
    Public Const colDispatchRate As String = "colDispatchRate"
    Public Const colCLR As String = "colCLR"
    Public Const colDispatchAmount As String = "colDispatchAmount"
    Public Const ColInvoiceQty As String = "ColInvoiceQty"
    Public Const ColInvoiceQtyLtr As String = "ColInvoiceQtyLtr"
    Public Const ColInvoiceFatPer As String = "ColInvoiceFatPer"
    Public Const colInvoiceSnfPer As String = "colInvoiceSnfPer"
    Public Const colInvoiceRate As String = "colInvoiceRate"
    Public Const colInvoiceAmount As String = "colInvoiceAmount"
    Public Const colInvoiceSNFKg As String = "colInvoiceSNFKg"
    Public Const colInvoiceFatKg As String = "colInvoiceFatKg"
    Public strSaleInvoice As String = Nothing
    Dim isFlag As Boolean = False
    Dim Qry As String

    Const colLineNo As String = "COLLNO"
    Const colRowType As String = "COLTYPE"
    'Const colDispatchNo As String = "colDispatchNo"
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


    Dim arrLoc As String = Nothing
    Public Shared aLoc As String = Nothing
    Dim AllowSNFNotManditoryInBulkSale As Boolean = False
    Dim ApplyTSPriceAtBulkSale As Boolean = False
    Dim ShowBulkDispatchQtyInLtr As Boolean = False
    Dim UseKGLitreConversionInBulkSaleAsperCLRCalculation As Boolean = False
    Dim Allow0FatPerOnBulkSaleQualityCheckScreen As Boolean = False
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmInvoiceBulkSale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'If e.Alt And e.KeyCode = Keys.N Then
        '    Reset()
        'End If
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
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "BulkInvoiceDeleteType"
            frm.strCode = "BulkInvoiceDelete"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnDeleteInvoiceafterPost.Visible = True
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F10 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterCode.Importbulkdatafromexcelsheet
            frm.strCode = clsFixedParameterType.Importbulkdatafromexcelsheet
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnimportdocument.Visible = True
            End If
        End If
    End Sub
    Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub FrmInvoiceBulkSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'SetMailRight()
        AllowSNFNotManditoryInBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSNFNotManditoryInBulkSale, clsFixedParameterCode.AllowSNFNotManditoryInBulkSale, Nothing)) = 1, True, False))
        ApplyTSPriceAtBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, Nothing)) = 1, True, False))
        ShowBulkDispatchQtyInLtr = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowBulkDispatchQtyInLtr, clsFixedParameterCode.ShowBulkDispatchQtyInLtr, Nothing)) = 1, True, False))
        UseKGLitreConversionInBulkSaleAsperCLRCalculation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UseKGLitreConversionInBulkSaleAsperCLRCalculation, clsFixedParameterCode.UseKGLitreConversionInBulkSaleAsperCLRCalculation, Nothing)) = 1, True, False))
        Allow0FatPerOnBulkSaleQualityCheckScreen = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow0FatPerOnBulkSaleQualityCheckScreen, clsFixedParameterCode.Allow0FatPerOnBulkSaleQualityCheckScreen, Nothing)) = 1, True, False))
        TCSTaxApplicableOnbulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.TCSTaxApplicableOnbulkSale, clsFixedParameterCode.TCSTaxApplicableOnbulkSale, Nothing)) = 1, True, False))
        AllowtoChangeTCSBaseAmount = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoChangeTCSBaseAmount, clsFixedParameterCode.AllowtoChangeTCSBaseAmount, Nothing)) = 0, False, True)
        AmountToCheckCustomerOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckCustomerOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckCustomerOutstandingForTCSTax, Nothing))
        EnableTCSRateValidityFrom01July2021 = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableTCSRateValidityFrom01July2021, clsFixedParameterCode.EnableTCSRateValidityFrom01July2021, Nothing)) = 0, False, True)
        AllowFatPerInanynumberofMultipesonBulkQC = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowFatPerInanynumberofMultipesonBulkQC, clsFixedParameterCode.AllowFatPerInanynumberofMultipesonBulkQC, Nothing)) = 1, True, False))
        Reset()
        UcAttachment1.Form_ID = MyBase.Form_ID
        'SetMaxlength()
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
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmInvoiceBulkSale)
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

    Private Sub fndCustomerNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomerNo._MYValidating
        fndCustomerNo.Value = clsCustomerMaster.getFinder("", fndCustomerNo.Value, isButtonClicked)
        lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + fndCustomerNo.Value + "' ")

    End Sub

    Private Sub FndLocationCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndLocationCode._MYValidating
        If rdbAgainstDispatch.IsChecked Then
            FndLocationCode.Value = clsLocation.getFinder(" Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and TSPL_LOCATION_MASTER.Location_Code in (" + arrLoc + ") ", FndLocationCode.Value, isButtonClicked)
            LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_LOCATION_MASTER  where Location_Code  ='" + FndLocationCode.Value + "' ")
        Else
            FndLocationCode.Value = clsLocation.getFinder(" Location_Type='Virtual' ", FndLocationCode.Value, isButtonClicked)
            LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_LOCATION_MASTER  where Location_Code  ='" + FndLocationCode.Value + "' ")

        End If

    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'FndLocationCode.Value = obj.Default_LocCode
                'LblLocationName.Text = obj.Default_LocName
                Dim LocationName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_LOCATION_MASTER where Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and Location_Code ='" & obj.Default_LocCode & "'"))
                If clsCommon.myLen(LocationName) > 0 Then
                    FndLocationCode.Value = obj.Default_LocCode
                    LblLocationName.Text = obj.Default_LocName

                Else
                    FndLocationCode.Value = ""
                    LblLocationName.Text = ""
                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
                aLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub
    'Sub LoadDispatchNo()
    '    Dim qry As String = "select item_code as Document_No,Item_Desc as Document_Date from tspl_item_master" '' "Select Document_No,Document_Date  from TSPL_Dispatch_BulkSale " ''where Posted='Y' Document_No not in (Select Invoice_Code From TSPL_INVOICE_MASTER_BULKSALE where Document_code <>'" + txtDocNo.Value + "')  "
    '    cbgDispatchNo.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgDispatchNo.ValueMember = "Document_No"
    '    cbgDispatchNo.DisplayMember = "Document_Date"
    'End Sub
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

        Dim HSNCode As New GridViewTextBoxColumn()
        HSNCode.FormatString = ""
        HSNCode.HeaderText = "HSN Code"
        HSNCode.Name = colHSNCode
        HSNCode.Width = 100
        HSNCode.ReadOnly = True
        HSNCode.WrapText = True
        HSNCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(HSNCode)

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
        dispatchQty.DecimalPlaces = 3
        dispatchQty.Name = colDispatchQty
        dispatchQty.Width = 100
        dispatchQty.ReadOnly = True
        dispatchQty.WrapText = True
        dispatchQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(dispatchQty)

        'Sanjay
        Dim QtydispatchInLtr As New GridViewDecimalColumn
        QtydispatchInLtr.FormatString = "{0:n3}"
        QtydispatchInLtr.HeaderText = "Dispatch Qty(Ltr)"
        QtydispatchInLtr.DecimalPlaces = 3
        QtydispatchInLtr.Name = colDispatchQtyLtr
        QtydispatchInLtr.Width = 120
        QtydispatchInLtr.ReadOnly = True
        QtydispatchInLtr.WrapText = True
        QtydispatchInLtr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(QtydispatchInLtr)

        If ShowBulkDispatchQtyInLtr = True Then
            gv1.Columns(colDispatchQtyLtr).IsVisible = True
            gv1.Columns(colDispatchQtyLtr).VisibleInColumnChooser = True
        Else
            gv1.Columns(colDispatchQtyLtr).IsVisible = False
            gv1.Columns(colDispatchQtyLtr).VisibleInColumnChooser = False
        End If
        'Sanjay

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

        ''richa Against 8 Jan 2019
        Dim clr As New GridViewDecimalColumn
        clr.FormatString = "{0:n2}"
        clr.HeaderText = "CLR"
        clr.Name = colCLR
        clr.Width = 75
        clr.ReadOnly = True
        clr.WrapText = True
        clr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(clr)

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
        InvoiceQty.DecimalPlaces = 3
        InvoiceQty.Name = ColInvoiceQty
        InvoiceQty.Width = 75
        InvoiceQty.ReadOnly = False
        InvoiceQty.WrapText = True
        InvoiceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceQty)

        'Sanjay
        Dim QtyInvoiceInLtr As New GridViewDecimalColumn
        QtyInvoiceInLtr.FormatString = "{0:n3}"
        QtyInvoiceInLtr.HeaderText = "Invoice Qty(Ltr)"
        QtyInvoiceInLtr.DecimalPlaces = 3
        QtyInvoiceInLtr.Name = ColInvoiceQtyLtr
        QtyInvoiceInLtr.Width = 120
        QtyInvoiceInLtr.ReadOnly = True
        QtyInvoiceInLtr.WrapText = True
        QtyInvoiceInLtr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(QtyInvoiceInLtr)

        If ShowBulkDispatchQtyInLtr = True Then
            gv1.Columns(ColInvoiceQtyLtr).IsVisible = True
            gv1.Columns(ColInvoiceQtyLtr).VisibleInColumnChooser = True
        Else
            gv1.Columns(ColInvoiceQtyLtr).IsVisible = False
            gv1.Columns(ColInvoiceQtyLtr).VisibleInColumnChooser = False
        End If
        'Sanjay

        'Dim InvoiceFatPer As New GridViewDecimalColumn
        'InvoiceFatPer.FormatString = ""
        'InvoiceFatPer.HeaderText = "Invoice Fat %"
        'InvoiceFatPer.Name = ColInvoiceFatPer
        'InvoiceFatPer.Width = 75
        'InvoiceFatPer.FormatString = "{0:n2}"
        'InvoiceFatPer.ReadOnly = False
        'InvoiceFatPer.WrapText = True
        ''InvoiceFatPer.IsVisible = rdbAgainstDispatchTrade.IsChecked
        'InvoiceFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(InvoiceFatPer)

        'Dim InvoiceSnfPer As New GridViewDecimalColumn
        'InvoiceSnfPer.FormatString = ""
        'InvoiceSnfPer.HeaderText = "Invoice SNF %"
        'InvoiceSnfPer.Name = colInvoiceSnfPer
        'InvoiceSnfPer.Width = 75
        'InvoiceSnfPer.FormatString = "{0:n2}"
        'InvoiceSnfPer.ReadOnly = False
        'InvoiceSnfPer.WrapText = True
        ''InvoiceSnfPer.IsVisible = rdbAgainstDispatchTrade.IsChecked
        'InvoiceSnfPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(InvoiceSnfPer)

        Dim InvoiceFatPer As New GridViewTextBoxColumn
        InvoiceFatPer.FormatString = ""
        InvoiceFatPer.HeaderText = "Invoice Fat %"
        InvoiceFatPer.Name = ColInvoiceFatPer
        InvoiceFatPer.Width = 75
        InvoiceFatPer.ReadOnly = False
        InvoiceFatPer.WrapText = True
        'InvoiceFatPer.IsVisible = rdbAgainstDispatchTrade.IsChecked
        InvoiceFatPer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceFatPer)

        Dim InvoiceSnfPer As New GridViewTextBoxColumn
        InvoiceSnfPer.FormatString = ""
        InvoiceSnfPer.HeaderText = "Invoice SNF %"
        InvoiceSnfPer.Name = colInvoiceSnfPer
        InvoiceSnfPer.Width = 75
        InvoiceSnfPer.ReadOnly = False
        InvoiceSnfPer.WrapText = True
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
        'InvoiceAmount.IsVisible = rdbAgainstDispatchTrade.IsChecked
        InvoiceAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceAmount)

        ''richa 12/01/2014

        Dim InvoiceFatKg As New GridViewDecimalColumn
        InvoiceFatKg.FormatString = ""
        InvoiceFatKg.DecimalPlaces = 3
        InvoiceFatKg.HeaderText = "Invoice FAT KG"
        InvoiceFatKg.Name = colInvoiceFatKg
        InvoiceFatKg.Width = 75
        InvoiceFatKg.ReadOnly = True
        InvoiceFatKg.WrapText = True
        InvoiceFatKg.IsVisible = False
        InvoiceFatKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceFatKg)


        Dim InvoiceSNFKg As New GridViewDecimalColumn
        InvoiceSNFKg.FormatString = ""
        InvoiceSNFKg.DecimalPlaces = 3
        InvoiceSNFKg.HeaderText = "Invoice SNF KG"
        InvoiceSNFKg.Name = colInvoiceSNFKg
        InvoiceSNFKg.Width = 75
        InvoiceSNFKg.ReadOnly = True
        InvoiceSNFKg.WrapText = True
        InvoiceSNFKg.IsVisible = False
        InvoiceSNFKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(InvoiceSNFKg)

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


        ''----------------

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
        txtDocNo.Value = ""
        fndCustomerNo.Value = ""
        lblCustomerName.Text = ""
        FndLocationCode.Value = ""
        LblLocationName.Text = ""
        txtDispatchNo.Value = ""
        txtComment.Text = ""
        rbtnTaxCalAutomatic.IsChecked = True
        txtDate.Value = clsCommon.GETSERVERDATE()
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        'Dim DateTime As String = clsDBFuncationality.getSingleValue("Select Description From TSPL_FIXED_PARAMETER where Code ='" & clsFixedParameterCode.AllowToSaveTimeWithDocumentDate & "'")
        If DateTime = "1" Then
            txtDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            txtDate.CustomFormat = "dd/MM/yyyy"
        End If

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtDocNo.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        UcAttachment1.BlankAllControls()
        isNewEntry = True
        rdbAgainstDispatch.IsChecked = True
        RadGroupBox1.Enabled = True
        txtewaybilldate.Value = clsCommon.GETSERVERDATE()
        txtewaybilldate.Checked = False
        TxtEWayBillNo.Text = ""
        txtElectronicRefNo.Text = ""
        loadBlankItemGrid()
        'LoadDispatchNo()
        ReStoreGridLayout()
        isNewEntry = True
        lblInvoiceStatus.Visible = False
        LOCATIONRIGTHS()
        lblTotRAmt1.Text = ""
        TxtRoundoff.Text = ""
        ''richa agarwal 10/10/2014
        fndCustomerNo.Enabled = True
        FndLocationCode.Enabled = True
        ''===================
        btnDeleteInvoiceafterPost.Visible = False
        btnimportdocument.Visible = False
        LoadBlankGridTax()
        If TCSTaxApplicableOnbulkSale Then
            txtTaxGroup.Enabled = True
        Else
            txtTaxGroup.Enabled = False
        End If
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        lblTaxAmt.Text = "0"
        lblDocumentAmount.Text = "0"
        If AllowtoChangeTCSBaseAmount = True Then
            txttcstaxbaseamount.Enabled = True
        Else
            txttcstaxbaseamount.Enabled = False
        End If
        txttcstaxbaseamount.Value = 0
        lblActualTCSTaxBaseAmt.Text = "0"
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

    Private Sub EmailSMSSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailSMSSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.FrmInvoiceBulkSale
        frm.ShowDialog()
    End Sub

    Private Sub RMDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RMDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    'Private Sub cbgDispatchNo__MyCheckChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    clsCommon.MyMessageBoxShow("New event fired")
    'End Sub
    Function IsItemExistInGridTrade(ByVal obj As clsDispatchDetailTradeBulkSale)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colDispatchNo).Value)
            Dim strItemDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc  from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "))
            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.Document_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow("Requition No : " + obj.Document_No + "  Item : " + strItemDesc + Environment.NewLine + "Already exist at row no:" + clsCommon.myCstr(ii + 1))
                Return True
            End If
            strICode = Nothing
            strItemDesc = Nothing
            strReqCode = Nothing
        Next
        Return False
    End Function
    Sub SelectDispatchTradeItems()
        isInsideLoadData = True
        Dim frm As New frmPendingDispatchTrade()
        frm.customerCode = fndCustomerNo.Value
        frm.customerName = lblCustomerName.Text
        frm.Locationcode = FndLocationCode.Value
        frm.LocationName = LblLocationName.Text
        frm.InvoiceAgainst = rdbAgainstDispatchTrade.Text
        frm.strCurrCode = txtDocNo.Value
        frm.ShowDialog()
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            Dim objDispatch As ClsDispatchBulkSaleTrade = ClsDispatchBulkSaleTrade.GetData(frm.ArrReturn(0).Document_No, NavigatorType.Current)
            If objDispatch IsNot Nothing AndAlso clsCommon.myLen(objDispatch.Document_No) > 0 Then
                If (clsCommon.myLen(fndCustomerNo.Value) <= 0) Then
                    fndCustomerNo.Value = frm.customerCode
                    lblCustomerName.Text = frm.customerName
                End If
                If (clsCommon.myLen(FndLocationCode.Value) <= 0) Then
                    FndLocationCode.Value = objDispatch.Location_Code
                    LblLocationName.Text = objDispatch.Location_Name
                End If

            End If
            If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value) <= 0 Then
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            End If
            For Each obj As clsDispatchDetailTradeBulkSale In frm.ArrReturn
                If Not IsItemExistInGridTrade(obj) Then
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = clsCommon.myCdbl(gv1.Rows.Count)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchNo).Value = obj.Document_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchDate).Value = clsDBFuncationality.getSingleValue("Select convert(varchar,Document_Date,103) from TSPL_Dispatch_BulkSale_Trade where Document_No ='" + obj.Document_No + "'")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = obj.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc  from TSPL_ITEM_MASTER where Item_Code  ='" + obj.Item_Code + "'")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerNo).Value = clsDBFuncationality.getSingleValue("Select Tanker_No from TSPL_Dispatch_BulkSale_Trade where Document_No ='" + obj.Document_No + "'")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = obj.Unit_code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQty).Value = obj.Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchFatPer).Value = obj.FatPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchSnfPer).Value = obj.SNFPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchRate).Value = obj.Rate
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = obj.Amount
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = obj.Qty * obj.Rate
                    ''richa 25/09/2014
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQty).Value = obj.Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceFatPer).Value = obj.FatPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSnfPer).Value = obj.SNFPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceRate).Value = obj.Rate
                    ''richa agarwal 12/01/2014
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceFatKg).Value = obj.Fat_KG
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSNFKg).Value = obj.SNF_KG
                    ''-----------------------
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value = obj.Qty * obj.Rate
                    ''==========================

                    'Sanjay 14/09/2018
                    If ShowBulkDispatchQtyInLtr = True Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQtyLtr).Value = QtyInLtr(obj.Item_Code, obj.Unit_code, obj.Qty)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQtyLtr).Value = QtyInLtr(obj.Item_Code, obj.Unit_code, obj.Qty)
                    End If
                    'Sanjay

                    UpdateCurrentRow(gv1.Rows.Count - 1)
                    ''richa agarwal 11/12/2014
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value)
                    ''-------------
                End If
            Next
            gv1.Columns(ColInvoiceQty).ReadOnly = True
            ''richa agarwal 10/10/2014
            fndCustomerNo.Enabled = False
            FndLocationCode.Enabled = False
            ''===================
        Else
            txtDispatchNo.Value = ""
            loadBlankItemGrid()
        End If
        isInsideLoadData = False
        UpdateAllTotals()
        RefreshDispatchNo()
    End Sub
    Sub SelectDispatchItems()
        Try
            Dim dblActualTCSBaseAmount As Double = 0
            Dim dblChangedTCSBaseAmount As Double = 0
            If txtFromDate.Value > txtToDate.Value Then
                Throw New Exception("From date cannot be greater than to Date")
            End If

            isInsideLoadData = True
            Dim frm As New frmPendingDispatch()
            frm.customerCode = fndCustomerNo.Value
            frm.customerName = lblCustomerName.Text
            frm.Locationcode = FndLocationCode.Value
            frm.LocationName = LblLocationName.Text
            frm.InvoiceAgainst = rdbAgainstDispatch.Text
            frm.strCurrCode = txtDocNo.Value
            frm.fromdate = txtFromDate.Value
            frm.todate = txtToDate.Value

            frm.ShowDialog()
            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                Dim objDispatch As ClsDispatchBulkSale = ClsDispatchBulkSale.GetData(frm.ArrReturn(0).Document_No, arrLoc, NavigatorType.Current)
                If objDispatch IsNot Nothing AndAlso clsCommon.myLen(objDispatch.Document_No) > 0 Then
                    If (clsCommon.myLen(fndCustomerNo.Value) <= 0) Then
                        fndCustomerNo.Value = frm.customerCode
                        lblCustomerName.Text = frm.customerName
                    End If
                    If (clsCommon.myLen(FndLocationCode.Value) <= 0) Then
                        FndLocationCode.Value = objDispatch.Location_Code
                        LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_LOCATION_MASTER  where Location_Code  ='" + FndLocationCode.Value + "' ")
                    End If

                End If
                dblActualTCSBaseAmount = objDispatch.ActualTCSBaseAmount
                dblChangedTCSBaseAmount = objDispatch.ChangedTCSBaseAmount
                rbtnTaxCalAutomatic.IsChecked = True

                If gv1.Rows.Count > 0 AndAlso clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value) <= 0 Then
                    gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                End If
                For Each obj As clsDispatchDetailBulkSale In frm.ArrReturn
                    If Not IsItemExistInGrid(obj) Then
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = clsCommon.myCdbl(gv1.Rows.Count)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchNo).Value = obj.Document_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchDate).Value = clsDBFuncationality.getSingleValue("Select convert(varchar,Document_Date,103) from TSPL_Dispatch_BulkSale where Document_No ='" + obj.Document_No + "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = obj.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc  from TSPL_ITEM_MASTER where Item_Code  ='" + obj.Item_Code + "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerNo).Value = clsDBFuncationality.getSingleValue("Select Tanker_Code  from TSPL_Dispatch_BulkSale where Document_No ='" + obj.Document_No + "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = obj.Unit_code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQty).Value = obj.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchFatPer).Value = obj.FatPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchSnfPer).Value = obj.SNFPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchRate).Value = obj.Rate
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = obj.Qty * obj.Rate
                        ''richa 25/09/2014
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQty).Value = obj.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceFatPer).Value = obj.FatPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSnfPer).Value = obj.SNFPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceRate).Value = obj.Rate
                        ''richa agarwal 12/01/2014
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceFatKg).Value = obj.Fat_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSNFKg).Value = obj.SNF_KG

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = obj.CLR
                        'Sanjay 14/09/2018
                        If ShowBulkDispatchQtyInLtr = True Then
                            ''richa agarwal 8 Jan,2018 ERO/07/01/19-000457
                            If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                                ''richa ERO/25/02/19-000499
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQtyLtr).Value = Math.Round(clsCommon.myCdbl(clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(obj.Qty), clsCommon.myCdbl(obj.CLR))), 0)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQtyLtr).Value = Math.Round(clsCommon.myCdbl(clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(obj.Qty), clsCommon.myCdbl(obj.CLR))), 0)

                            Else
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQtyLtr).Value = QtyInLtr(obj.Item_Code, obj.Unit_code, obj.Qty)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQtyLtr).Value = QtyInLtr(obj.Item_Code, obj.Unit_code, obj.Qty)
                            End If
                        End If

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(1)).Value = obj.TAX1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(1)).Value = obj.TAX1_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(1)).Value = obj.TAX1_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(1)).Value = obj.TAX1_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(2)).Value = obj.TAX2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(2)).Value = obj.TAX2_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(2)).Value = obj.TAX2_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(2)).Value = obj.TAX2_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(3)).Value = obj.TAX3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(3)).Value = obj.TAX3_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(3)).Value = obj.TAX3_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(3)).Value = obj.TAX3_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(4)).Value = obj.TAX4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(4)).Value = obj.TAX4_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(4)).Value = obj.TAX4_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(4)).Value = obj.TAX4_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax + clsCommon.myCstr(5)).Value = obj.TAX5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt + clsCommon.myCstr(5)).Value = obj.TAX5_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate + clsCommon.myCstr(5)).Value = obj.TAX5_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt + clsCommon.myCstr(5)).Value = obj.TAX5_Amt
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = obj.Total_Tax_Amt
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = obj.Item_Net_Amt



                        'Sanjay
                        ''-----------------------
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value = obj.Qty * obj.Rate
                        ''==========================
                        UpdateCurrentRow(gv1.Rows.Count - 1)
                        ''richa agarwal 11/12/2014
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value)
                        ''-------------
                    End If
                Next
                gv1.Columns(ColInvoiceQty).ReadOnly = False
                fndCustomerNo.Enabled = False
                FndLocationCode.Enabled = False
                If TCSTaxApplicableOnbulkSale = True Then
                    txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, FndLocationCode.Value, fndCustomerNo.Value, "S", txtDate.Value, "Y")
                    lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)

                    SetTaxDetails()
                    txttcstaxbaseamount.Value = dblChangedTCSBaseAmount
                    lblActualTCSTaxBaseAmt.Text = dblActualTCSBaseAmount
                End If
            Else
                txtDispatchNo.Value = ""
                loadBlankItemGrid()
            End If
            isInsideLoadData = False
            UpdateAllTotals()
            RefreshDispatchNo()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Function IsItemExistInGrid(ByVal obj As clsDispatchDetailBulkSale)
        For ii As Integer = 0 To gv1.RowCount - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
            Dim strReqCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colDispatchNo).Value)
            Dim strItemDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc  from TSPL_ITEM_MASTER where Item_Code='" + strICode + "' "))
            If clsCommon.myLen(strReqCode) > 0 AndAlso clsCommon.CompairString(strReqCode, obj.Document_No) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, obj.Item_Code) = CompairStringResult.Equal Then
                common.clsCommon.MyMessageBoxShow("Requition No : " + obj.Document_No + "  Item : " + strItemDesc + Environment.NewLine + "Already exist at row no:" + clsCommon.myCstr(ii + 1))
                Return True
            End If
            strICode = Nothing
            strReqCode = Nothing
            strItemDesc = Nothing
        Next
        Return False
    End Function
    Sub RefreshDispatchNo()
        txtDispatchNo.Value = ""
        If gv1.Rows.Count > 0 Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strDispatchNo As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colDispatchNo).Value)
                If clsCommon.myLen(strDispatchNo) > 0 Then
                    txtDispatchNo.Value = strDispatchNo
                    Exit Sub
                End If
            Next
        End If
    End Sub
    Private Sub txtDispatchNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDispatchNo._MYValidating
        If rdbAgainstDispatch.IsChecked Then
            SelectDispatchItems()
        Else
            SelectDispatchTradeItems()
        End If

    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    'If e.Column Is gv1.Columns(ColInvoiceQty) Or e.Column Is gv1.Columns(colInvoiceRate) Then
                    '    For ii As Integer = 0 To gv1.Rows.Count - 1
                    '        UpdateCurrentRow(ii)
                    '    Next
                    'Else
                    'If e.Column Is gv1.Columns(colUnitCode) Then
                    '    OpenUOMList(False)
                    'End If
                    ''richa agarwal 18/11/2014
                    If AllowFatPerInanynumberofMultipesonBulkQC = False Then
                        If e.Column Is gv1.Columns(ColInvoiceFatPer) Then
                            gv1.Rows(e.RowIndex).Cells(ColInvoiceFatPer).Value = Math.Truncate(Math.Round(clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(ColInvoiceFatPer).Value) * 100, 2)) / 100
                            Dim fracValue1 As Double = 0
                            If clsCommon.myLen(ColInvoiceFatPer) > 0 Then
                                fracValue1 = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(ColInvoiceFatPer).Value)
                                fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
                                If CInt(fracValue1) Mod 5 <> 0 Then
                                    gv1.Rows(e.RowIndex).Cells(ColInvoiceFatPer).Value = 0
                                    gv1.CurrentRow = gv1.Rows(e.RowIndex)
                                    gv1.CurrentColumn = gv1.Columns(ColInvoiceFatPer)
                                    Throw New Exception("Invoice FAT% value in Grid, must have its decimal part multiple of 5")
                                End If
                            End If
                        End If
                    End If

                    If e.Column Is gv1.Columns(colInvoiceSnfPer) Then
                        ' gv1.Rows(e.RowIndex).Cells(colInvoiceSnfPer).Value = Math.Truncate(clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colInvoiceSnfPer).Value) * 100) / 100
                        gv1.Rows(e.RowIndex).Cells(colInvoiceSnfPer).Value = Math.Truncate(Math.Round(clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colInvoiceSnfPer).Value) * 100, 2)) / 100
                    End If
                    ''------------------------
                    If e.Column Is gv1.Columns(ColInvoiceFatPer) Or e.Column Is gv1.Columns(ColInvoiceQty) Or e.Column Is gv1.Columns(colInvoiceSnfPer) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                    End If
                End If
                UpdateAllTotals()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnitCode).Value = clsCommon.ShowSelectForm("InvoiceUnitFinder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitCode).Value), "Code", isButtonClick)
            qry = Nothing
            whrCls = Nothing
        End If
        strICode = Nothing
    End Sub
    'Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)

    '    'Dim InvoiceQty As Double = 0
    '    'Dim InvoiceAmount As Double = 0
    '    'Dim InvoiceRate As Double = 0

    '    'InvoiceQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColInvoiceQty).Value)
    '    'InvoiceRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInvoiceRate).Value)
    '    'InvoiceAmount = InvoiceQty * InvoiceRate

    '    'gv1.Rows(IntRowNo).Cells(colInvoiceAmount).Value = clsCommon.myCdbl(InvoiceAmount)


    '    Dim DispatchQty As Double = 0
    '    Dim InvoiceAmount As Double = 0
    '    Dim InvoiceRate As Double = 0

    '    DispatchQty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDispatchQty).Value)
    '    InvoiceRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInvoiceRate).Value)
    '    InvoiceAmount = DispatchQty * InvoiceRate

    '    gv1.Rows(IntRowNo).Cells(colInvoiceAmount).Value = clsCommon.myCdbl(InvoiceAmount)

    'End Sub
    ''richa 25/09/2014
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

            Dim dt As DataTable
            If rdbAgainstDispatch.IsChecked Then
                If ApplyTSPriceAtBulkSale = True Then
                    dt = clsDBFuncationality.GetDataTable("Select Fat_Weightage,Fat_Ratio,Snf_Weightage ,Snf_Ratio ,0 as Standard_Rate from TSPL_Dispatch_BulkSale  where Document_No='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colDispatchNo).Value) & "'")
                Else
                    dt = clsDBFuncationality.GetDataTable("Select Fat_Weightage,Fat_Ratio,Snf_Weightage ,Snf_Ratio ,Standard_Rate from TSPL_BulkSalePrice_MASTER where Price_Code =(Select Price_Code  from TSPL_Dispatch_BulkSale where Document_No='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colDispatchNo).Value) & "') ")
                End If
            Else
                If ApplyTSPriceAtBulkSale = True Then
                    dt = clsDBFuncationality.GetDataTable("Select Fat_Weightage,Fat_Ratio,Snf_Weightage ,Snf_Ratio ,0 as Standard_Rate from TSPL_Dispatch_BulkSale  where Document_No='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colDispatchNo).Value) & "'")
                Else
                    dt = clsDBFuncationality.GetDataTable("Select Fat_Weightage,Fat_Ratio,Snf_Weightage ,Snf_Ratio ,Standard_Rate from TSPL_BulkSalePrice_MASTER where Price_Code =(Select Price_Code  from TSPL_Dispatch_BulkSale_Trade where Document_No='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colDispatchNo).Value) & "') ")
                End If
            End If
            If dt.Rows.Count > 0 Then
                If rdbAgainstDispatch.IsChecked Then
                    StandardRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select StandardRate  from TSPL_Dispatch_Detail_BulkSale  where Document_No ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colDispatchNo).Value) & "' "))
                Else
                    StandardRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select StandardRate  from TSPL_Dispatch_Detail_BulkSale_Trade  where Document_No ='" & clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colDispatchNo).Value) & "' "))
                End If


                ' StandardRate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                FatWeightage = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                SNFWeightage = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                FatRatio = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                SNFRatio = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
            End If

            FatPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColInvoiceFatPer).Value)
            SNFPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInvoiceSnfPer).Value)

            'Qty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColInvoiceQty).Value)
            ''richa agarwal 8 Jan,2019
            If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                ''richa ERO/25/02/19-000499
                Qty = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColInvoiceQtyLtr).Value), 0)
            Else
                Qty = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColInvoiceQty).Value), 3)
            End If

            ''richa agarwal 8 Jan,2019 ERO/25/02/19-000499
            If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                FatRate = Math.Round(StandardRate, 2)
                SNFRate = Math.Round(StandardRate, 2)

                FatKG = ((Qty * FatPer) / 100)
                SNFKG = ((Qty * SNFPer) / 100)
            Else
                FatRate = Math.Round(StandardRate * FatWeightage / FatRatio, 2)
                SNFRate = Math.Round(StandardRate * SNFWeightage / SNFRatio, 2)

                FatKG = Math.Round(((Qty * FatPer) / 100), 3)
                SNFKG = Math.Round(((Qty * SNFPer) / 100), 3)
            End If


            FatAmount = Math.Round(FatRate * FatKG, 2)
            SNFAmount = Math.Round(SNFRate * SNFKG, 2)

            'FatAmount = Math.Floor(FatRate * FatKG * 100) / 100
            'SNFAmount = Math.Floor(SNFRate * SNFKG * 100) / 100

            Amount = FatAmount + SNFAmount

            gv1.Rows(IntRowNo).Cells(colInvoiceAmount).Value = clsCommon.myCdbl(Amount)
            ''richa agarwal 12/01/2014
            gv1.Rows(IntRowNo).Cells(colInvoiceFatKg).Value = clsCommon.myCdbl(FatKG)
            gv1.Rows(IntRowNo).Cells(colInvoiceSNFKg).Value = clsCommon.myCdbl(SNFKG)
            ''-----------------------
            'If Qty > 0 Then
            '    gv1.Rows(IntRowNo).Cells(colInvoiceRate).Value = Math.Round(clsCommon.myCdbl(Amount / Qty), 2)
            '    'gv1.Rows(IntRowNo).Cells(colInvoiceRate).Value = Math.Floor(clsCommon.myCdbl(Amount / Qty) * 100) / 100
            'End If

            If Qty > 0 Then
                If ApplyTSPriceAtBulkSale = True Then
                    gv1.Rows(IntRowNo).Cells(colInvoiceRate).Value = Math.Round(clsCommon.myCdbl((StandardRate * (FatPer + SNFPer)) / 100), 2)
                    If UseKGLitreConversionInBulkSaleAsperCLRCalculation = False Then
                        gv1.Rows(IntRowNo).Cells(colInvoiceAmount).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInvoiceRate).Value) * Qty, 2)
                    End If
                Else
                    gv1.Rows(IntRowNo).Cells(colInvoiceRate).Value = Math.Round(clsCommon.myCdbl(Amount / Qty), 2)
                End If
                'Sanjay
                If ShowBulkDispatchQtyInLtr = True Then
                    ''richa agarwal 8 Jan,2018 ERO/07/01/19-000457
                    If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                        ''richa ERO/25/02/19-000499
                        gv1.Rows(IntRowNo).Cells(ColInvoiceQtyLtr).Value = Math.Round(clsCommon.myCdbl(clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColInvoiceQty).Value), clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCLR).Value))), 0)
                    Else
                        gv1.Rows(IntRowNo).Cells(ColInvoiceQtyLtr).Value = QtyInLtr(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colItemCode).Value), clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnitCode).Value), Qty)
                    End If
                End If
                'Sanjay
            End If

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
                        dblAmtAfterDis = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colInvoiceAmount).Value)
                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0
                        If IsSurTax Then
                            'Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                            'dblBaseAmt = dblSurTaxAmt
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            ''richa 15 Sep 2020 changes according to tax
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
                            ''
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
    Private Sub txttcstaxbaseamount_TextChanged(sender As Object, e As EventArgs) Handles txttcstaxbaseamount.TextChanged
        Try
            If AllowtoChangeTCSBaseAmount Then
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    UpdateCurrentRow(ii)
                Next
                UpdateAllTotals()

            Else
                txttcstaxbaseamount.Value = 0
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    ''==================='
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

            'For ii As Integer = 0 To gv1.Rows.Count - 1
            '    If (clsCommon.myLen(gv1.Rows(ii).Cells(colDispatchNo).Value) > 0) Then
            '        'If rdbAgainstDispatch.IsChecked = True Then
            '        '    dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDispatchAmount).Value)
            '        'Else
            '        dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colInvoiceAmount).Value)
            '        'End If
            '        'dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colInvoiceAmount).Value)
            '        'dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDispatchAmount).Value)
            '    End If
            'Next

            ''richa 9 Oct,2020

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim Strii As String = clsCommon.myCstr(ii + 1)
                If (clsCommon.myLen(gv1.Rows(ii).Cells(colDispatchNo).Value) > 0) Then
                    dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colInvoiceAmount).Value)
                    'dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)

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



            'done by priti KDI/07/05/18-000298
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
    Sub SaveData()
        'Dim trans As SqlTransaction
        Dim obj As New ClsInvoiceBulkSale()
        Dim objTr As New ClsInvoiceDetailBulkSale
        Try
            If AllowToSave() Then
                'trans = clsDBFuncationality.GetTransactin()
                Dim DocuAmount As Double = 0
                Dim AmountLimitInvoiceBulkSale As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'"))


                obj.Document_Date = txtDate.Value
                obj.Document_No = txtDocNo.Value


                obj.Customer_Code = fndCustomerNo.Value
                obj.Location_Code = FndLocationCode.Value
                If rdbAgainstDispatch.IsChecked Then
                    obj.InvoiceAgainst = rdbAgainstDispatch.Text
                    obj.fromdate = txtFromDate.Value
                    obj.todate = txtToDate.Value
                Else
                    obj.InvoiceAgainst = rdbAgainstDispatchTrade.Text
                End If
                obj.Comments = txtComment.Text
                'obj.Total_Amt = lblTotRAmt1.Text
                'obj.RoundOffAmount = TxtRoundoff.Text

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

                obj.arrInvoiceDetailBulkSale = New List(Of ClsInvoiceDetailBulkSale)


                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New ClsInvoiceDetailBulkSale()
                    objTr.Document_No = clsCommon.myCstr(obj.Document_No)
                    objTr.Dispatch_Code = clsCommon.myCstr(grow.Cells(colDispatchNo).Value)
                    objTr.Dispatch_Date = clsCommon.myCDate(grow.Cells(colDispatchDate).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    If rdbAgainstDispatch.IsChecked Then
                        objTr.Tanker_Code = clsCommon.myCstr(grow.Cells(colTankerNo).Value)
                    Else
                        objTr.TradeTanker_No = clsCommon.myCstr(grow.Cells(colTankerNo).Value)
                    End If
                    objTr.DispatchQty = clsCommon.myCdbl(grow.Cells(colDispatchQty).Value)
                    objTr.DispatchFatPer = clsCommon.myCdbl(grow.Cells(colDispatchFatPer).Value)
                    objTr.DispatchSNFPer = clsCommon.myCdbl(grow.Cells(colDispatchSnfPer).Value)
                    objTr.DispatchRate = clsCommon.myCdbl(grow.Cells(colDispatchRate).Value)
                    objTr.DispatchAmount = clsCommon.myCdbl(grow.Cells(colDispatchAmount).Value)


                    'If rdbAgainstDispatchTrade.IsChecked Then
                    '    objTr.InvoiceQty = 0
                    '    objTr.InvoiceFatPer = clsCommon.myCdbl(grow.Cells(ColInvoiceFatPer).Value)
                    '    objTr.InvoiceSNFPer = clsCommon.myCdbl(grow.Cells(colInvoiceSnfPer).Value)
                    '    objTr.InvoiceRate = clsCommon.myCdbl(grow.Cells(colInvoiceRate).Value)
                    '    objTr.InvoiceAmount = clsCommon.myCdbl(grow.Cells(colInvoiceAmount).Value)
                    'Else
                    '    objTr.InvoiceQty = 0
                    '    objTr.InvoiceFatPer = 0
                    '    objTr.InvoiceSNFPer = 0
                    '    objTr.InvoiceRate = 0
                    '    objTr.InvoiceAmount = 0
                    'End If
                    objTr.CLR = clsCommon.myCdbl(grow.Cells(colCLR).Value)
                    objTr.InvoiceQty = clsCommon.myCdbl(grow.Cells(ColInvoiceQty).Value)
                    objTr.InvoiceFatPer = clsCommon.myCdbl(grow.Cells(ColInvoiceFatPer).Value)
                    objTr.InvoiceSNFPer = clsCommon.myCdbl(grow.Cells(colInvoiceSnfPer).Value)
                    objTr.InvoiceFatKG = clsCommon.myCdbl(grow.Cells(colInvoiceFatKg).Value)
                    objTr.InvoiceSNFKG = clsCommon.myCdbl(grow.Cells(colInvoiceSNFKg).Value)
                    objTr.InvoiceRate = clsCommon.myCdbl(grow.Cells(colInvoiceRate).Value)
                    objTr.InvoiceAmount = clsCommon.myCdbl(grow.Cells(colInvoiceAmount).Value)
                    ''richa agarwal 4 feb, 2019
                    objTr.DispatchQty_in_Ltr = Math.Round(clsCommon.myCdbl(grow.Cells(colDispatchQtyLtr).Value), 3)
                    objTr.InvoiceQty_in_Ltr = Math.Round(clsCommon.myCdbl(grow.Cells(ColInvoiceQtyLtr).Value), 3)
                    'objTr.InvoiceQty = 0
                    'objTr.InvoiceFatPer = 0
                    'objTr.InvoiceSNFPer = 0
                    'objTr.InvoiceRate = 0
                    'objTr.InvoiceAmount = 0

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
                    objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterTax).Value)


                    If AmountLimitInvoiceBulkSale > 0 Then
                        'If rdbAgainstDispatch.IsChecked Then
                        '    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.DispatchAmount, 2)
                        '    If Math.Round(DocuAmount, 2) < AmountLimitInvoiceBulkSale Then
                        '        obj.arrInvoiceDetailBulkSale.Add(objTr)
                        '    Else
                        '        DocuAmount = Math.Round(DocuAmount, 2) - Math.Round(objTr.DispatchAmount, 2)
                        '    End If
                        'Else
                        DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.Item_Net_Amt, 2)
                        If Math.Round(DocuAmount, 2) < AmountLimitInvoiceBulkSale Then
                            obj.arrInvoiceDetailBulkSale.Add(objTr)
                        Else
                            DocuAmount = Math.Round(DocuAmount, 2) - Math.Round(objTr.Item_Net_Amt, 2)
                        End If

                        'DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.InvoiceAmount, 2)
                        'If Math.Round(DocuAmount, 2) < AmountLimitInvoiceBulkSale Then
                        '    obj.arrInvoiceDetailBulkSale.Add(objTr)
                        'Else
                        '    DocuAmount = Math.Round(DocuAmount, 2) - Math.Round(objTr.InvoiceAmount, 2)
                        'End If

                        ' End If

                    Else
                        obj.arrInvoiceDetailBulkSale.Add(objTr)
                    End If

                Next
                ''richa 15/09/2014
                If AmountLimitInvoiceBulkSale > 0 Then
                    If DocuAmount <= 0 Then
                        Throw New Exception("You cannot save this Invoice because amount limit is less than Document amount")
                    End If
                    If Math.Round(clsCommon.myCdbl(DocuAmount), 0) > clsCommon.myCdbl(DocuAmount) Then
                        'obj.RoundOffAmount = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(clsCommon.myCdbl(DocuAmount)) - Math.Round(clsCommon.myCdbl(DocuAmount), 0)), 2)
                        obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount), 0) - clsCommon.myCdbl(DocuAmount), 2)
                        obj.Total_Amt = Math.Round(clsCommon.myCdbl(DocuAmount), 0)
                    Else
                        'obj.RoundOffAmount = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(DocuAmount) - Math.Round(clsCommon.myCdbl(DocuAmount))), 2)
                        obj.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(DocuAmount)) - clsCommon.myCdbl(DocuAmount), 2)
                        obj.Total_Amt = Math.Round(clsCommon.myCdbl(DocuAmount), 0)

                    End If
                Else
                    obj.Total_Amt = lblTotRAmt1.Text
                    obj.RoundOffAmount = TxtRoundoff.Text
                End If
                ''15/09/2014
                If (ClsInvoiceBulkSale.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_No)
                    If Not isFlag Then

                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                        LoadData(obj.Document_No, NavigatorType.Current)

                    End If

                End If
            End If
        Catch ex As Exception
            ' trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            obj = Nothing
            objTr = Nothing
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        Dim desc As String = ""
        Dim InvoiceQty As Double = 0
        Dim DispatchQty As Double = 0

        ' KUNAL > TICKET : BM00000009609 > Modified Date : 22-09-2016
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If

        If clsCommon.myLen(fndCustomerNo.Value) <= 0 Then
            fndCustomerNo.Focus()
            Throw New Exception("Customer No cannot be left blank")
        End If
        If clsCommon.myLen(FndLocationCode.Value) <= 0 Then
            FndLocationCode.Focus()
            Throw New Exception("Location cannot be left blank")
        End If

        If clsCommon.myLen(txtDispatchNo.Value) <= 0 Then
            txtDispatchNo.Focus()
            Throw New Exception("Dispatch No cannot be left blank")
        End If

        'If rdbAgainstDispatchTrade.IsChecked Then
        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myCdbl(gv1.Rows(i).Cells(ColInvoiceQty).Value) < 0 Then
                Throw New Exception("Invoice Qty cannot be negative")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(ColInvoiceQty).Value) = 0 Then
                Throw New Exception("Invoice Qty cannot be left blank")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(ColInvoiceFatPer).Value) < 0 Then
                Throw New Exception("Invoice Fat% cannot be negative")
            End If
           
            ''richa ERO/06/09/19-001021
            If Allow0FatPerOnBulkSaleQualityCheckScreen = False Then
                If clsCommon.myCdbl(gv1.Rows(i).Cells(ColInvoiceFatPer).Value) = 0 Then
                    Throw New Exception("Invoice Fat% cannot be left blank")
                End If
            End If

            If AllowSNFNotManditoryInBulkSale = False Then
                If clsCommon.myCdbl(gv1.Rows(i).Cells(colInvoiceSnfPer).Value) < 0 Then
                    Throw New Exception("Invoice SNF% cannot be negative")
                End If
                If clsCommon.myCdbl(gv1.Rows(i).Cells(colInvoiceSnfPer).Value) = 0 Then
                    Throw New Exception("Invoice SNF% cannot be left blank")
                End If
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colInvoiceRate).Value) < 0 Then
                Throw New Exception("Invoice Rate cannot be negative")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colInvoiceRate).Value) = 0 Then
                Throw New Exception("Invoice Rate cannot be left blank")
            End If
            InvoiceQty = clsCommon.myCdbl(gv1.Rows(i).Cells(ColInvoiceQty).Value)
            DispatchQty = clsCommon.myCdbl(gv1.Rows(i).Cells(colDispatchQty).Value)
            If InvoiceQty = 0 Then
                Throw New Exception("Invoice qty cannot be zero.")
            End If
            If InvoiceQty > DispatchQty Then
                Throw New Exception("Invoice qty cannot be greater than dispatch qty.")
            End If

        Next
        ''richa agarwal 11/10/2014
        If rdbAgainstDispatchTrade.IsChecked Then
            If clsCommon.myLen(gv1.Rows(0).Cells(colDispatchDate).Value) > 0 Then
                If clsCommon.myCDate(gv1.Rows(0).Cells(colDispatchDate).Value, "dd/MM/yyyy") <> clsCommon.myCDate(txtDate.Value, "dd/MM/yyyy") Then
                    txtDate.Focus()
                    Throw New Exception("Invoice Date should be same as dispatch date")
                End If
            End If
        End If
        '=====================
        'End If


        'For i As Integer = 1 To gv1.Rows.Count - 1
        '    If clsCommon.myCdbl(gv1.Rows(0).Cells(colDispatchFatPer).Value) <> clsCommon.myCdbl(gv1.Rows(i).Cells(colDispatchFatPer).Value) Then
        '        Throw New Exception("Fat % should be same for all dispatch")
        '    End If
        '    If clsCommon.myCdbl(gv1.Rows(0).Cells(colDispatchSnfPer).Value) <> clsCommon.myCdbl(gv1.Rows(i).Cells(colDispatchSnfPer).Value) Then
        '        Throw New Exception("SNF % should be same for all dispatch")
        '    End If
        'Next

        Dim AmountLimitInvoiceBulkSale As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'"))
        If AmountLimitInvoiceBulkSale > 0 Then
            If gv1.Rows.Count = 1 Then
                Dim DocumentAmount As Double = clsCommon.myCdbl(lblTotRAmt1.Text)
                If DocumentAmount > AmountLimitInvoiceBulkSale Then
                    Throw New Exception("You cannot save this Invoice because amount limit is less than Document amount")
                End If
            End If
        End If
        If AllowtoChangeTCSBaseAmount Then
            If clsCommon.myCdbl(txttcstaxbaseamount.Value) > clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text) Then
                Throw New Exception("TCS Tax Base amount should not be greater than Actual TCS Tax Base Amount")
            End If
        End If
        desc = Nothing
        Return True
    End Function
    Sub PostData()
        Try

            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                ''richa agarwal 14/10/2014
                Dim strfLocation As String = ""
                Dim strvirlocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select distinct ','+''+Location_Code  from TSPL_INVOICE_MASTER_BULKSALE for xml path ('')), 1,1,'') as Test from TSPL_INVOICE_MASTER_BULKSALE where InvoiceAgainst ='Against Dispatch Trade' group by InvoiceAgainst"))
                If clsCommon.myLen(strvirlocation) > 0 Then
                    strvirlocation = strvirlocation.Replace(",", "','")
                    If clsCommon.myLen(arrLoc) > 0 Then
                        strfLocation = arrLoc + ",'" + strvirlocation + "'"
                    Else
                        strfLocation = "'" + strvirlocation + "'"
                    End If
                End If
                ''===================================
                If (ClsInvoiceBulkSale.PostData(MyBase.Form_ID, strfLocation, txtDocNo.Value)) Then

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

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        ''richa agarwal 14/10/2014
        Dim strfLocation As String = String.Empty
        ReStoreGridLayout()
        strfLocation = arrLoc
        ''===================================
        Dim obj As ClsInvoiceBulkSale = ClsInvoiceBulkSale.GetData(strCode, strfLocation, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtDocNo.Value = obj.Document_No
            txtDate.Value = obj.Document_Date
            'FndLocationCode.Value = obj.Location_Code
            'LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + FndLocationCode.Value + "' ")
            fndCustomerNo.Value = obj.Customer_Code
            lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCustomerNo.Value + "'")
            lblTotRAmt1.Text = obj.Total_Amt
            TxtRoundoff.Text = obj.RoundOffAmount
            txtComment.Text = obj.Comments
            TxtEWayBillNo.Text = obj.EWayBillNo
            If obj.EWayBillDate IsNot Nothing Then
                txtewaybilldate.Value = obj.EWayBillDate
                txtewaybilldate.Checked = True
            Else
                txtewaybilldate.Value = clsCommon.GETSERVERDATE()
                txtewaybilldate.Checked = False
            End If
            txtElectronicRefNo.Text = obj.Electronic_Ref_No
            'loadBlankItemGrid()
            If clsCommon.CompairString(obj.InvoiceAgainst, rdbAgainstDispatch.Text) = CompairStringResult.Equal Then
                rdbAgainstDispatch.IsChecked = True
                txtFromDate.Value = obj.fromdate
                txtToDate.Value = obj.todate
            Else
                rdbAgainstDispatchTrade.IsChecked = True
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



            If obj.arrInvoiceDetailBulkSale IsNot Nothing AndAlso obj.arrInvoiceDetailBulkSale.Count > 0 Then
                loadBlankItemGrid()
                For Each objTr As ClsInvoiceDetailBulkSale In obj.arrInvoiceDetailBulkSale
                    'gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchNo).Value = objTr.Dispatch_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchDate).Value = clsCommon.GetPrintDate(objTr.Dispatch_Date, "dd/MM/yyyy")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = objTr.Unit_code
                    If clsCommon.CompairString(obj.InvoiceAgainst, rdbAgainstDispatch.Text) = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerNo).Value = objTr.Tanker_Code
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTankerNo).Value = objTr.TradeTanker_No
                    End If

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNCode).Value = objTr.HSN_code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQty).Value = objTr.DispatchQty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchFatPer).Value = objTr.DispatchFatPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchSnfPer).Value = objTr.DispatchSNFPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchRate).Value = objTr.DispatchRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchAmount).Value = objTr.DispatchAmount

                    'gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceFatPer).Value = objTr.InvoiceFatPer
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSnfPer).Value = objTr.InvoiceSNFPer
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceRate).Value = objTr.InvoiceRate
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value = objTr.InvoiceAmount

                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQty).Value = objTr.InvoiceQty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = objTr.CLR
                    'Sanjay 14/09/2018
                    If ShowBulkDispatchQtyInLtr = True Then
                        ''richa agarwal 8 Jan,2018 ERO/07/01/19-000457
                        If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                            ''richa ERO/25/02/19-000499
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQtyLtr).Value = clsCommon.myCdbl(clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(objTr.DispatchQty), clsCommon.myCdbl(objTr.CLR)))
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQtyLtr).Value = clsCommon.myCdbl(clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(objTr.InvoiceQty), clsCommon.myCdbl(objTr.CLR)))

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQtyLtr).Value = objTr.DispatchQty_in_Ltr
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQtyLtr).Value = objTr.InvoiceQty_in_Ltr
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDispatchQtyLtr).Value = QtyInLtr(objTr.Item_Code, objTr.Unit_code, objTr.DispatchQty)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceQtyLtr).Value = QtyInLtr(objTr.Item_Code, objTr.Unit_code, objTr.InvoiceQty)
                        End If
                    End If
                    'Sanjay
                    gv1.Rows(gv1.Rows.Count - 1).Cells(ColInvoiceFatPer).Value = objTr.InvoiceFatPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSnfPer).Value = objTr.InvoiceSNFPer
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceRate).Value = objTr.InvoiceRate
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceAmount).Value = objTr.InvoiceAmount
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceFatKg).Value = objTr.InvoiceFatKG
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInvoiceSNFKg).Value = objTr.InvoiceSNFKG

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


                    If clsCommon.myCdbl(gv1.Rows.Count) = 1 Then
                        txtDispatchNo.Value = objTr.Dispatch_Code
                    End If
                    gv1.Rows.AddNew()
                Next
                If rdbAgainstDispatch.IsChecked Then
                    gv1.Columns(ColInvoiceQty).ReadOnly = False
                Else
                    gv1.Columns(ColInvoiceQty).ReadOnly = True
                End If
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
            Else
                gv1.DataSource = Nothing
            End If

            ' txtDispatchNo.Value = clsDBFuncationality.getSingleValue("SELECT TOP 1 Dispatch_Code FROM TSPL_INVOICE_DETAIL_BULKSALE WHERE Document_No='" + txtDocNo.Value + "' ")
            FndLocationCode.Value = obj.Location_Code
            LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + FndLocationCode.Value + "' ")

            txtDocNo.MyReadOnly = True
            btnsave.Text = "Update"
            btnDeleteInvoiceafterPost.Visible = False
            btnimportdocument.Visible = False
            ' btndelete.Enabled = True
            If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
                lblInvoiceStatus.Visible = True
                lblInvoiceStatus.Text = clsDBFuncationality.getSingleValue("Select 'Approved By '+ Modified_By  from TSPL_INVOICE_MASTER_BULKSALE where Document_No='" + obj.Document_No + "' ")
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
    End Sub

    Private Function QtyInLtr(ByVal ItemCode As String, ByVal UOM As String, ByVal Qty As Double) As Double
        Dim CONVERTED_Qty As Double
        Dim strsql As String = ""
        strsql = "select  ISNULL(round(case when coalesce(CONVERTED_UOM.Conversion_factor,0)=0 then 0 else (" + clsCommon.myCstr(Qty) + " *StockingUnit.Conversion_Factor /coalesce(CONVERTED_UOM.Conversion_factor,1)) end,2),0) as Qty from TSPL_ITEM_MASTER"
        strsql += " LEFT OUTER JOIN tspl_item_uom_detail as StockingUnit on StockingUnit.Item_Code=TSPL_ITEM_MASTER.item_code and StockingUnit.UOM_CODE='" + UOM + "' "
        strsql += " inner join TSPL_ITEM_UOM_DETAIL AS CONVERTED_UOM ON CONVERTED_UOM.Item_Code=TSPL_ITEM_MASTER.item_code and CONVERTED_UOM.UOM_CODE='Ltr' "
        strsql += " where TSPL_ITEM_MASTER.item_code = '" + ItemCode + "'"
        CONVERTED_Qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strsql))
        Return CONVERTED_Qty
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsInvoiceBulkSale.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_INVOICE_MASTER_BULKSALE where Document_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
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

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        ' Dim qry As String = "Select Document_No as Code,Document_Date from TSPL_INVOICE_MASTER_BULKSALE "
        ''richa agarwal 14/10/2014
        'Dim strfLocation As String = String.Empty
        'Dim strvirlocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select distinct ','+''+Location_Code  from TSPL_INVOICE_MASTER_BULKSALE where InvoiceAgainst ='Against Dispatch Trade' for xml path ('')), 1,1,'') as Test from TSPL_INVOICE_MASTER_BULKSALE where InvoiceAgainst ='Against Dispatch Trade' group by InvoiceAgainst"))
        'If clsCommon.myLen(strvirlocation) > 0 Then
        '    strvirlocation = strvirlocation.Replace(",", "','")
        '    If clsCommon.myLen(arrLoc) > 0 Then
        '        strfLocation = arrLoc + ",'" + strvirlocation + "'"
        '    Else
        '        strfLocation = "'" + strvirlocation + "'"
        '    End If
        'End If
        ''===================================
        Dim whrclas As String = String.Empty
        'If clsCommon.myLen(strfLocation) Then
        '    whrclas += " TSPL_INVOICE_MASTER_BULKSALE.Location_Code in (" + strfLocation + ") "
        'Else
        If clsCommon.myLen(arrLoc) Then
            whrclas += " TSPL_INVOICE_MASTER_BULKSALE.Location_Code in (" + arrLoc + ") "
        End If


        ''richa agarwal against ticket no.BM00000007184 30/06/2015
        ' Dim qry As String = "Select TSPL_INVOICE_MASTER_BULKSALE.Document_No as Code,Convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) as [Invoice Date],TSPL_INVOICE_MASTER_BULKSALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_INVOICE_MASTER_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],case when TSPL_INVOICE_MASTER_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status,TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst as [Invoice Against] from TSPL_INVOICE_MASTER_BULKSALE left outer Join TSPL_CUSTOMER_MASTER on TSPL_INVOICE_MASTER_BULKSALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_INVOICE_MASTER_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code"
        Dim qry As String = "Select TSPL_INVOICE_MASTER_BULKSALE.Document_No as Code,Convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) as [Invoice Date],TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code,isnull(TSPL_Dispatch_BulkSale.QC_Code,'') as [FAT/SNF check No] ,Isnull(TSPL_Quality_Check_BulkSale.LoadingTanker_No,'') as [Loading No] ,Isnull(TSPL_Quality_Check_BulkSale.Weighment_No,'') as [Weighment No] ,Isnull(TSPL_Quality_Check_BulkSale.GateEntry_Document_No,'') as [Gate Entry No] ,TSPL_INVOICE_MASTER_BULKSALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_INVOICE_MASTER_BULKSALE.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],case when TSPL_INVOICE_MASTER_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status,TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst as [Invoice Against] from TSPL_INVOICE_MASTER_BULKSALE left outer Join TSPL_CUSTOMER_MASTER on TSPL_INVOICE_MASTER_BULKSALE.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_INVOICE_MASTER_BULKSALE.Location_Code =TSPL_LOCATION_MASTER.Location_Code" & _
        " left outer join TSPL_INVOICE_DETAIL_BULKSALE on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No " & _
        " Left Outer Join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code " & _
        " Left outer join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.QC_No =TSPL_Dispatch_BulkSale.QC_Code "
        If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyMultiChamberInBulkWeighmentEntry, clsFixedParameterCode.ApplyMultiChamberInBulkWeighmentEntry, Nothing)) = 1, True, False)) Then
            qry = "select * from (" + qry + " group by TSPL_INVOICE_MASTER_BULKSALE.Document_No ,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code,TSPL_Dispatch_BulkSale.QC_Code,TSPL_Quality_Check_BulkSale.LoadingTanker_No,TSPL_Quality_Check_BulkSale.Weighment_No,TSPL_Quality_Check_BulkSale.GateEntry_Document_No,TSPL_INVOICE_MASTER_BULKSALE.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Alies_Name,TSPL_INVOICE_MASTER_BULKSALE.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_INVOICE_MASTER_BULKSALE.Posted,TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst)x "
            If clsCommon.myLen(arrLoc) Then
                whrclas = " x.[Location Code] in (" + arrLoc + ") "
            End If
        End If
        txtDocNo.Value = clsCommon.ShowSelectForm("InvoiceBulkSale", qry, "Code", whrclas, txtDocNo.Value, "TSPL_INVOICE_MASTER_BULKSALE.Document_Date", isButtonClicked, "TSPL_INVOICE_MASTER_BULKSALE.Document_Date")

        LoadData(txtDocNo.Value, NavigatorType.Current)
        ' strfLocation = Nothing
        ' strvirlocation = Nothing
        whrclas = Nothing
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If rdbAgainstDispatch.IsChecked Then
                    funPrint(txtDocNo.Value, rdbAgainstDispatch.Text)
                End If
                'Modify By Prabhakar Ticket Ref : MSI-002/15-16/006120'
                If rdbAgainstDispatchTrade.IsChecked Then
                    funPrint(txtDocNo.Value, rdbAgainstDispatchTrade.Text)
                End If
            Else
                Throw New Exception("Please Select Document No first")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub funPrint(ByVal StrCode As String, ByVal AgainstDis As String)

        '" ISNULL(round(case when coalesce(CONVERTED_UOM.Conversion_factor,0)=0 then 0 else (TSPL_Dispatch_Detail_BulkSale.Qty*StockingUnit.Conversion_Factor /coalesce(CONVERTED_UOM.Conversion_factor,1)) end,3),0) as QtyInLtr " & _
        'Qry += " LEFT OUTER JOIN tspl_item_uom_detail as StockingUnit on StockingUnit.Item_Code=TSPL_Dispatch_Detail_BulkSale.item_code and StockingUnit.UOM_CODE=TSPL_Dispatch_Detail_BulkSale.Unit_Code " & _
        '       " LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL AS CONVERTED_UOM ON CONVERTED_UOM.Item_Code=TSPL_Dispatch_Detail_BulkSale.item_code and CONVERTED_UOM.UOM_CODE='Ltr' "


        Dim PrintTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToPrintTimeWithDocumentDate, clsFixedParameterCode.AllowToPrintTimeWithDocumentDate, Nothing)
        '==============update by preeti against ticket no[ERO/31/05/18-000333,ERO/11/07/18-000370,ERO/12/07/18-000372]
        Try
            'Dim qry As String = "Select TSPL_COMPANY_MASTER.Comp_Name as Company_Name,TSPL_COMPANY_MASTER.Add1 as Address1,TSPL_COMPANY_MASTER.Add2 as Address2,TSPL_COMPANY_MASTER.Add3 as Address3," & _
            '" TSPL_COMPANY_MASTER .Pincode as PinNo,TSPL_COMPANY_MASTER.CINNo as CinNo,TSPL_INVOICE_MASTER_BULKSALE.Document_No as InvoiceNo,Convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,106) as Invoicedate,'' as Suppliersref," & _
            '" (SELECT STUFF((SELECT ',' + Dispatch_Code FROM TSPL_INVOICE_DETAIL_BULKSALE WHERE Document_No ='" + txtDocNo.Value + "' ORDER BY Dispatch_Code FOR XML PATH('')), 1, 1, '')) AS DespatchDocumentNo," & _
            '" TSPL_INVOICE_MASTER_BULKSALE.Location_Code as Despatchedthrough,TSPL_CUSTOMER_MASTER.Customer_Name as Consignee,TSPL_CUSTOMER_MASTER.Add1 +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end as Consignee_Address,0 as SL_No,Convert(varchar,TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Date,103) as despatchDate," & _
            '" TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code as TankerNo,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as MilkQty,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatPer as Fatper,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFPer as Snfper," & _
            '" TSPL_INVOICE_DETAIL_BULKSALE.InvoiceRate as Rate,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amount,TSPL_INVOICE_MASTER_BULKSALE.RoundOffAmount,TSPL_INVOICE_MASTER_BULKSALE.Created_By as CreatedBy,TSPL_INVOICE_MASTER_BULKSALE.Modified_By as ModifiedBy," & _
            '" TSPL_INVOICE_MASTER_BULKSALE.Total_Amt as DocumentAmount,TSPL_ITEM_MASTER.Item_Desc as itemDesc,TSPL_BulkSalePrice_MASTER.Fat_Weightage as fatweightage ,TSPL_BulkSalePrice_MASTER.Snf_Weightage as snfweightage,TSPL_BulkSalePrice_MASTER.Fat_Ratio as fatratio,TSPL_BulkSalePrice_MASTER.Snf_Ratio as snfratio,TSPL_INVOICE_MASTER_BULKSALE.Posted " & _
            '" from TSPL_INVOICE_DETAIL_BULKSALE Left Outer Join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No" & _
            '" Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_INVOICE_MASTER_BULKSALE.Comp_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_INVOICE_MASTER_BULKSALE.Customer_Code" & _
            '" left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVOICE_DETAIL_BULKSALE .Item_Code Left outer Join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code " & _
            '" left Outer Join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code=TSPL_Dispatch_BulkSale.Price_Code  " & _
            '" where 1=1 and TSPL_INVOICE_MASTER_BULKSALE .Document_No='" + txtDocNo.Value + "' and TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='" + rdbAgainstDispatch.Text + "'"

            Dim qry As String = ""
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SAHAYOG") = CompairStringResult.Equal Then
                qry += "Select * from ("
            End If
            qry += "Select " &
                " TSPL_Dispatch_BulkSale.document_no,TSPL_Dispatch_BulkSale.document_date,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_location_master.gstno,TSPL_Dispatch_Detail_BulkSale.Fat_KG,TSPL_Dispatch_Detail_BulkSale.SNF_KG,TSPL_ITEM_MASTER.HSN_Code,TSPL_ITEM_MASTER.Item_Desc,Case when dtax1.Is_TCS = 'Y' then TSPL_INVOICE_MASTER_BULKSALE.TAX1_Rate when  dtax2.Is_TCS = 'Y' then TSPL_INVOICE_MASTER_BULKSALE.TAX2_Rate when dtax3.Is_TCS = 'Y' then TSPL_INVOICE_MASTER_BULKSALE.TAX3_Rate " &
                " when dtax4.Is_TCS = 'Y' then TSPL_INVOICE_MASTER_BULKSALE.TAX4_Rate  when dtax5.Is_TCS = 'Y' then TSPL_INVOICE_MASTER_BULKSALE.TAX5_Rate end as TCS_Rate " &
                " ,Case when dtax1.Is_TCS = 'Y' then TSPL_INVOICE_MASTER_BULKSALE.TAX1_Amt when  dtax2.Is_TCS = 'Y' then TSPL_INVOICE_MASTER_BULKSALE.TAX2_Amt when dtax3.Is_TCS = 'Y' then TSPL_INVOICE_MASTER_BULKSALE.TAX3_Amt when dtax4.Is_TCS = 'Y' then TSPL_INVOICE_MASTER_BULKSALE.TAX4_Amt  when dtax5.Is_TCS = 'Y' then TSPL_INVOICE_MASTER_BULKSALE.TAX5_Amt end  as TCS_Amount" &
                " ,TSPL_INVOICE_DETAIL_BULKSALE.unit_code, case when ISNULL(tspl_company_master.Phone1,'')='(+__)__________' then '' " &
                " else tspl_company_master.Phone1 end +  Case When ISNULL(tspl_company_master.Phone2,'')<>'(+__)__________' Then ', '+  tspl_company_master.Phone2 Else'' End as CompPhone, tspl_route_master.route_no,TSPL_LOCATION_MASTER.GSTNO as LOC_GSTIN,tspl_company_master.CINNo as Comp_CINNo,tspl_company_master.Pan_No  as Com_PAN_No,tspl_company_master.Access_Officer as Comp_FSSAI,TSPL_CUSTOMER_MASTER.pin_no as Cust_Pin_no,TSPL_CUSTOMER_MASTER.FSSAI_NO as Cust_FSSAI,TSPL_VEHICLE_MASTER.employee_id as Driver_code,tspl_employee_master.emp_Name as Driver_Name,tspl_vehicle_master.vehicle_id,tspl_vehicle_master.Description as Vehicle_Name," &
                    " (select top 1 tspl_employee_master.emp_name from tspl_salesman_detail" &
                    " left join tspl_employee_master on tspl_employee_master.emp_code=tspl_salesman_detail.salesman_code" &
                    " where route_code=tspl_route_master.route_no) as  Sales_Name,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then '' " &
                    " else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone,TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code, tspl_company_master.cst_lst as Comp_CSt_LST,TSPL_LOCATION_MASTER.Tin_No as Comp_tin, TSPL_COMPANY_MASTER.Comp_Name as Company_Name,TSPL_CUSTOMER_MASTER.Add1 as ConAddress1,TSPL_CUSTOMER_MASTER.Add2 as ConAddress2,TSPL_CUSTOMER_MASTER.Add3  as ConAddress3, STATEMASTER_CUSTOMER.STATE_NAME as State_Customer,TSPL_LOCATION_MASTER.Add1  as Address1,TSPL_LOCATION_MASTER.Add2 as Address2, TSPL_LOCATION_MASTER.Add3 as Address3, STATEMASTER_LOCATION.State_Name as State_Location, STATEMASTER_COMPANY.STATE_NAME as State_Company," &
            " TSPL_COMPANY_MASTER .Pincode as PinNo,TSPL_COMPANY_MASTER.CINNo as CinNo,TSPL_INVOICE_MASTER_BULKSALE.Document_No as InvoiceNo," &
            " TLM.GSTNO as GSTIN_Comp,STATEMASTER_LOCATION.GST_STATE_Code as State_Code,STATEMASTER_CUSTOMER.GST_STATE_Code  as State_Code_receiver,isnull(TSPL_CUStomer_Master.GSTNO,'') as GSTIN_Receiver," &
            " TSPL_INVOICE_MASTER_BULKSALE.EWayBillNo as Ewaybillno,Convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.EWayBillDate,106) as EwaybillDate,TSPL_ITEM_MASTER.HSN_Code as HSN"

            If PrintTime = "1" Then
                qry += " ,TSPL_INVOICE_MASTER_BULKSALE.Document_Date as Invoicedate"
            Else
                qry += " ,Convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,106) as Invoicedate"

            End If

            qry += ",'' as Suppliersref,TSPL_Dispatch_Detail_BulkSale.StandardRate," &
        " (SELECT STUFF((SELECT ',' + Dispatch_Code FROM TSPL_INVOICE_DETAIL_BULKSALE WHERE Document_No ='" + txtDocNo.Value + "' ORDER BY Dispatch_Code FOR XML PATH('')), 1, 1, '')) AS DespatchDocumentNo," &
        " CityMaster.City_Name as Despatchedthrough,TSPL_CUSTOMER_MASTER.Customer_Name as Consignee,TSPL_CUSTOMER_MASTER.Add1 +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end as Consignee_Address,TSPL_CUSTOMER_MASTER.PAN as Customer_Pan,0 as SL_No,Convert(varchar,TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Date,103) as despatchDate,"

            If AgainstDis = "Against Dispatch Trade" Then
                qry += " TSPL_INVOICE_DETAIL_BULKSALE.TradeTanker_No as TankerNo"
            Else
                qry += " TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code as TankerNo"
            End If


            '    qry += ",TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as MilkQty,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatPer as Fatper,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFPer as Snfper," & _
            '" TSPL_INVOICE_DETAIL_BULKSALE.InvoiceRate as Rate,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amount,TSPL_INVOICE_MASTER_BULKSALE.RoundOffAmount,TSPL_INVOICE_MASTER_BULKSALE.Created_By as CreatedBy,TSPL_INVOICE_MASTER_BULKSALE.Modified_By as ModifiedBy," & _
            '" TSPL_INVOICE_MASTER_BULKSALE.Total_Amt as DocumentAmount,TSPL_ITEM_MASTER.Item_Desc as itemDesc,TSPL_BulkSalePrice_MASTER.Fat_Weightage as fatweightage ,TSPL_BulkSalePrice_MASTER.Snf_Weightage as snfweightage,TSPL_BulkSalePrice_MASTER.Fat_Ratio as fatratio,TSPL_BulkSalePrice_MASTER.Snf_Ratio as snfratio,TSPL_INVOICE_MASTER_BULKSALE.Posted,TSPL_CUSTOMER_MASTER.Tin_No as ConsigneeTinno,isnull(TSPL_CUSTOMER_MASTER.PIN_Code,'') as ConsigneePin,TSPL_INVOICE_MASTER_BULKSAlE.Comments, '1' as CopyType,TSPL_Dispatch_Detail_BulkSale.Chamber_Desc,TSPL_Dispatch_Detail_BulkSale.Seal_No,TSPL_Dispatch_Detail_BulkSale.Type,tspl_company_master.Logo_Img,tspl_company_master.Logo_Img2 , TSPL_INVOICE_MASTER_BULKSALE.Electronic_Ref_No " & _
            '" from TSPL_INVOICE_DETAIL_BULKSALE Left Outer Join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No" & _
            '" Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_INVOICE_MASTER_BULKSALE.Comp_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_INVOICE_MASTER_BULKSALE.Customer_Code" & _
            '" left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVOICE_DETAIL_BULKSALE .Item_Code Left outer Join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code " & _
            '" left Outer Join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code=TSPL_Dispatch_BulkSale.Price_Code  " & _
            '" left Outer Join TSPL_Dispatch_Detail_BulkSale  on TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code  =TSPL_Dispatch_Detail_BulkSale.Document_No  " & _
            '" left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVOICE_MASTER_BULKSALE.Location_Code " & _
            '" left outer join TSPL_LOCATION_MASTER as TLM ON TLM.Location_Code=TSPL_INVOICE_MASTER_BULKSALE.Location_Code " & _
            '" left outer join TSPL_CITY_MASTER as CityMaster on CityMaster.City_Code=TSPL_CUSTOMER_MASTER .City_Code " & _
            '" LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_COMPANY ON STATEMASTER_COMPANY.State_Code=TSPL_COMPANY_MASTER.State" & _
            '" LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_CUSTOMER ON STATEMASTER_CUSTOMER.State_Code=TSPL_CUSTOMER_MASTER.State" & _
            '" LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_LOCATION ON STATEMASTER_LOCATION.State_Code=TSPL_LOCATION_MASTER.State"


            qry += ",TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as MilkQty,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatPer as Fatper,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFPer as Snfper," &
        " TSPL_INVOICE_DETAIL_BULKSALE.InvoiceRate as Rate,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amount,TSPL_INVOICE_MASTER_BULKSALE.RoundOffAmount,TSPL_INVOICE_MASTER_BULKSALE.Created_By as CreatedBy,TSPL_INVOICE_MASTER_BULKSALE.Modified_By as ModifiedBy," &
        " TSPL_INVOICE_MASTER_BULKSALE.Total_Amt as DocumentAmount,TSPL_ITEM_MASTER.Item_Desc as itemDesc,TSPL_BulkSalePrice_MASTER.Fat_Weightage as fatweightage ,TSPL_BulkSalePrice_MASTER.Snf_Weightage as snfweightage,TSPL_BulkSalePrice_MASTER.Fat_Ratio as fatratio,TSPL_BulkSalePrice_MASTER.Snf_Ratio as snfratio,TSPL_INVOICE_MASTER_BULKSALE.Posted,TSPL_CUSTOMER_MASTER.Tin_No as ConsigneeTinno,isnull(TSPL_CUSTOMER_MASTER.PIN_Code,'') as ConsigneePin,TSPL_INVOICE_MASTER_BULKSAlE.Comments, '1' as CopyType,"
            If AgainstDis <> "Against Dispatch Trade" Then
                qry += " TSPL_Dispatch_Detail_BulkSale.Chamber_Desc,TSPL_Dispatch_Detail_BulkSale.Seal_No,TSPL_Dispatch_Detail_BulkSale.Type,"
            End If

            qry += " tspl_company_master.Logo_Img,tspl_company_master.Logo_Img2 , TSPL_INVOICE_MASTER_BULKSALE.Electronic_Ref_No,ISNULL(TSPL_INVOICE_DETAIL_BULKSALE.CLR,0) AS CLR,TSPL_COMPANY_MASTER.Bank_Name as Comp_Bank_Name , TSPL_COMPANY_MASTER.BankAccountNo as Comp_BankAccountNo , TSPL_COMPANY_MASTER.BankIFSCCode as Comp_BankIFSCCode , TSPL_COMPANY_MASTER.BankBranchAddress as Comp_BankBranchAddress " &
                " from TSPL_INVOICE_DETAIL_BULKSALE Left Outer Join TSPL_INVOICE_MASTER_BULKSALE  on TSPL_INVOICE_DETAIL_BULKSALE.Document_No =TSPL_INVOICE_MASTER_BULKSALE.Document_No" &
        " Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_INVOICE_MASTER_BULKSALE.Comp_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_INVOICE_MASTER_BULKSALE.Customer_Code" &
        " left Outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVOICE_DETAIL_BULKSALE .Item_Code "

            If AgainstDis = "Against Dispatch Trade" Then
                qry += " Left outer Join TSPL_Dispatch_BulkSale_Trade as TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code " &
        " left Outer Join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code=TSPL_Dispatch_BulkSale.Price_Code  " &
        " left Outer Join TSPL_Dispatch_Detail_BulkSale_Trade as TSPL_Dispatch_Detail_BulkSale  on TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code  =TSPL_Dispatch_Detail_BulkSale.Document_No  "
            Else
                qry += " Left outer Join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVOICE_DETAIL_BULKSALE .Dispatch_Code " &
        " left Outer Join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code=TSPL_Dispatch_BulkSale.Price_Code  " &
        " left Outer Join TSPL_Dispatch_Detail_BulkSale  on TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code  =TSPL_Dispatch_Detail_BulkSale.Document_No  "
            End If

            qry += " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_INVOICE_DETAIL_BULKSALE .tax1 " &
                " left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_INVOICE_DETAIL_BULKSALE.tax2 " &
                " left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_INVOICE_DETAIL_BULKSALE .TAX3 " &
                " left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_INVOICE_DETAIL_BULKSALE .tax4 " &
                " left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code=TSPL_INVOICE_DETAIL_BULKSALE .tax5 " &
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INVOICE_MASTER_BULKSALE.Location_Code " &
       " left outer join TSPL_LOCATION_MASTER as TLM ON TLM.Location_Code=TSPL_INVOICE_MASTER_BULKSALE.Location_Code " &
       " left outer join TSPL_CITY_MASTER as CityMaster on CityMaster.City_Code=TSPL_CUSTOMER_MASTER .City_Code " &
       " LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_COMPANY ON STATEMASTER_COMPANY.State_Code=TSPL_COMPANY_MASTER.State" &
       " LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_CUSTOMER ON STATEMASTER_CUSTOMER.State_Code=TSPL_CUSTOMER_MASTER.State" &
       " LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_LOCATION ON STATEMASTER_LOCATION.State_Code=TSPL_LOCATION_MASTER.State" &
" left join tspl_route_master on tspl_route_master.route_no= tspl_customer_master.route_no " &
       " left join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=tspl_route_master.vehicle_code" &
       " left join tspl_employee_master on tspl_employee_master.emp_code=TSPL_VEHICLE_MASTER.employee_id"


            ' " where 1=1 and TSPL_INVOICE_MASTER_BULKSALE .Document_No='" + txtDocNo.Value + "' and TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='" + rdbAgainstDispatch.Text + "'"
            qry += " where 1=1 and TSPL_INVOICE_MASTER_BULKSALE .Document_No='" + StrCode + "' and TSPL_INVOICE_MASTER_BULKSALE.InvoiceAgainst='" + AgainstDis + "'"

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SAHAYOG") = CompairStringResult.Equal Then
                qry += ") XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'Customer Copy' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'Office Copy' as CopyType1 ) YYY ON YYY.COL1=XXX.CopyType  ORDER BY YYY.COL2"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            'KwalitySalesReportViewer.funreport(dt, "rptInvoiceBulkSale", "Milk Sales Invoice")
            'frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptInvoiceBulkSale", "Milk Sales Invoice", "rptCompanyAddress.rpt")
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "Bulkdispath", "Milk Sales Dispatch", "Bulk dispath")
            'frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptInvoiceBulkSale", "Milk Sales Invoice", txtDate.Value, "rptCompanyAddress.rpt")
            frmCRV = Nothing
            qry = Nothing
            dt = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub rdbAgainstDispatchTrade_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbAgainstDispatchTrade.ToggleStateChanged
        'gv1.Columns(ColInvoiceFatPer).IsVisible = rdbAgainstDispatchTrade.IsChecked
        'gv1.Columns(colInvoiceSnfPer).IsVisible = rdbAgainstDispatchTrade.IsChecked
        'gv1.Columns(colInvoiceRate).IsVisible = rdbAgainstDispatchTrade.IsChecked
        'gv1.Columns(colInvoiceAmount).IsVisible = rdbAgainstDispatchTrade.IsChecked
        FndLocationCode.Value = ""
        LblLocationName.Text = ""
        RadGroupBox1.Enabled = False
    End Sub

    Private Sub gv1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv1.KeyPress
        Try
            If gv1.CurrentColumn Is gv1.Columns(ColInvoiceFatPer) Or gv1.CurrentColumn Is gv1.Columns(colInvoiceSnfPer) Then
                If Not IsNumeric(e.KeyChar) And Not e.KeyChar = "." Then
                    e.Handled = True
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub gv1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyUp
    '    Try

    '        'If Not IsNumeric(e.KeyChar) And Not e.KeyChar = "." Then
    '        '    e.Handled = True
    '        'End If
    '    Catch ex As Exception

    '    End Try
    'End Sub



    Private Sub rdbAgainstDispatch_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbAgainstDispatch.ToggleStateChanged
        RadGroupBox1.Enabled = True
    End Sub


    Private Sub btnDeleteInvoiceafterPost_Click(sender As Object, e As EventArgs) Handles btnDeleteInvoiceafterPost.Click
        DeleteDataAndMaintainedHistory()
    End Sub
    ''richa agarwal ERO/11/01/19-000467
    Private Sub DeleteDataAndMaintainedHistory()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (deleteConfirm()) Then
                If clsDBFuncationality.getSingleValue("Select count(*) from TSPL_SALE_RETURN_MASTER_BULKSALE where InvoiceNo ='" & txtDocNo.Value & "' ", trans) < 1 AndAlso rdbAgainstDispatch.IsChecked = True Then
                    If (ClsInvoiceBulkSale.SaveDataForHistory(txtDocNo.Value, trans)) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                        trans.Commit()
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                Else
                    Throw New Exception("Document " & txtDocNo.Value & " can't be deleted,its Sale return is created.")
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnimportdocument_Click(sender As Object, e As EventArgs) Handles btnimportdocument.Click
        ImportDatafromExcelSheetAndCreateAllBulkDocuments()
    End Sub

    Sub ImportDatafromExcelSheetAndCreateAllBulkDocuments()
        Dim gv As New RadGridView()
        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)
        '' declare object of classes
        Dim objGateEntry As New clsGateEntrySale()
        Dim objWeighmentEntry As New ClsWeighmentEntry()
        Dim objLoadingEntry As New ClsLoadingTanker()
        Dim objQCEntry As New ClsQualityCheckBulkSale()
        Dim objDispatchMasterEntry As New ClsDispatchBulkSale()
        Dim objDispatchDetailEntry As New clsDispatchDetailBulkSale()
        Dim objGateOutEntry As New ClsTankerOut()
        Try
            trans = clsDBFuncationality.GetTransactin()
            connectSql.OpenConnection()
            If transportSql.importExcel(gv, "Customer code", "Location Code", "Gate_entry_Date", "Sale_TankerNo.", "Item Code", "GROSS WEIGHT", "TARE WEIGHT", "Sale_MILK QTY.", "SILO No", "Sale_Fat %", "Sale_SNF %", "Sale_Rate", "Sale_Amount", "FAT Qty.", "SNF Qty", "Fat Per Kg", "Snf Per Kg", "Fat Value", "SNF Value", "Rate/100Kg.", "Invoice", "Sale Price Code", "FAT%", "SNF%", "FAT", "SNF", "Sale_CLR") Then
                Dim linno As Integer = 1


                For Each grow As GridViewRowInfo In gv.Rows
                    Dim isSaved As Boolean = True

                    If clsCommon.myLen(grow.Cells("Gate_entry_Date").Value) > 0 Then
                        ''----fetch data from sheet 
                        Dim strCustomerCode As String = clsCommon.myCstr(grow.Cells("Customer code").Value)
                        If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Customer code").Value))) Then
                            Throw New Exception("Customer code should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim strLocationCode As String = clsCommon.myCstr(grow.Cells("Location Code").Value)
                        If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Location Code").Value))) Then
                            Throw New Exception("Location Code should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        Dim strGate_entry_Date As String = clsCommon.myCDate(grow.Cells("Gate_entry_Date").Value)
                        Dim strTankerNo As String = clsCommon.myCstr(grow.Cells("Sale_TankerNo.").Value)

                        Dim strItemCode As String = clsCommon.myCstr(grow.Cells("Item Code").Value)
                        Dim strSilo As String = clsCommon.myCstr(grow.Cells("SILO No").Value)

                        Dim DblGrossWeight As Double = clsCommon.myCdbl(grow.Cells("GROSS WEIGHT").Value)
                        Dim DblTareWeight As Double = clsCommon.myCdbl(grow.Cells("TARE WEIGHT").Value)
                        Dim DblNetWeight As Double = clsCommon.myCdbl(grow.Cells("Sale_MILK QTY.").Value)

                        Dim DblSnfPer As Double = clsCommon.myCdbl(grow.Cells("Sale_SNF %").Value)
                        Dim DblFatPer As Double = clsCommon.myCdbl(grow.Cells("Sale_Fat %").Value)
                        Dim dblSaleAmount As Double = clsCommon.myCdbl(grow.Cells("Sale_Amount").Value)

                        Dim DblClr As Double = clsCommon.myCdbl(grow.Cells("Sale_CLR").Value)

                        Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Sale Price Code").Value)

                        Dim dblFatKg As Double = clsCommon.myCdbl(grow.Cells("FAT Qty.").Value)
                        Dim DblSNFKG As Double = clsCommon.myCdbl(grow.Cells("SNF Qty").Value)

                        Dim DblStandardrate As Double = clsCommon.myCdbl(grow.Cells("Sale_Rate").Value)


                        Dim DblFatRate As Double = clsCommon.myCdbl(grow.Cells("Fat Per Kg").Value)
                        Dim DblSNFRate As Double = clsCommon.myCdbl(grow.Cells("Snf Per Kg").Value)

                        Dim DblFatAmount As Double = clsCommon.myCdbl(grow.Cells("Fat Value").Value)
                        Dim DblSNFAmount As Double = clsCommon.myCdbl(grow.Cells("SNF Value").Value)

                        Dim DblNetMilkRate As Double = clsCommon.myCdbl(grow.Cells("Rate/100Kg.").Value)


                        Dim DblFATweightage As Double = clsCommon.myCdbl(grow.Cells("FAT%").Value)
                        Dim DblSNFWeightage As Double = clsCommon.myCdbl(grow.Cells("SNF%").Value)
                        Dim DblFATRatio As Double = clsCommon.myCdbl(grow.Cells("FAT").Value)
                        Dim DblSNFRatio As Double = clsCommon.myCdbl(grow.Cells("SNF").Value)


                        linno += 1

                        ''check data after fetching
                        If (String.IsNullOrEmpty(strCustomerCode)) Or clsCommon.myLen(strCustomerCode) > 12 Then
                            Throw New Exception("Length of Customer Code should be max. 12 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        Dim qry As String = "select COUNT(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" + strCustomerCode + "'"
                        Dim i As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If i <= 0 Then
                            Throw New Exception("Customer Code '" + strCustomerCode + "' does not exist At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If (String.IsNullOrEmpty(strLocationCode)) Or clsCommon.myLen(strLocationCode) > 12 Then
                            Throw New Exception("Length of Location Code should be max. 12 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        qry = "select COUNT(*) from TSPL_LOCATION_MASTER where Location_Code='" + strLocationCode + "'"
                        i = clsDBFuncationality.getSingleValue(qry, trans)
                        If i <= 0 Then
                            Throw New Exception("Location Code '" + strLocationCode + "' does not exist At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If


                        If (String.IsNullOrEmpty(strGate_entry_Date)) Or clsCommon.myLen(strGate_entry_Date) < 0 Then
                            Throw New Exception("Gate Entry Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If (String.IsNullOrEmpty(strTankerNo)) Or clsCommon.myLen(strTankerNo) > 20 Then
                            Throw New Exception("Length of Sale Tanker No. should be max. 20 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If (String.IsNullOrEmpty(strItemCode)) Or clsCommon.myLen(strItemCode) > 50 Then
                            Throw New Exception("Length of Item Code should be max. 50 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblGrossWeight) <= 0 Then
                            Throw New Exception("Gross Weight should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblTareWeight) <= 0 Then
                            Throw New Exception("Tare Weight should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblNetWeight) <= 0 Then
                            Throw New Exception("Sale MILK QTY. should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If (String.IsNullOrEmpty(strSilo)) Or clsCommon.myLen(strSilo) > 12 Then
                            Throw New Exception("Length of Silo No. should be max. 12 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        qry = "select COUNT(*) from TSPL_LOCATION_MASTER where Location_Code='" + strSilo + "' and is_sub_location='Y' and Loc_Segment_Code in (Select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" + strLocationCode + "')"
                        i = clsDBFuncationality.getSingleValue(qry, trans)
                        If i <= 0 Then
                            Throw New Exception("Silo No. '" + strSilo + "' does not exist At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblFatPer) <= 0 Then
                            Throw New Exception("Sale_Fat % should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblSnfPer) <= 0 Then
                            Throw New Exception("Sale_SNF % should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblStandardrate) <= 0 Then
                            Throw New Exception("Sale_Rate should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(dblSaleAmount) <= 0 Then
                            Throw New Exception(" Sale_Amount should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(dblFatKg) <= 0 Then
                            Throw New Exception(" FAT Qty should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblSNFKG) <= 0 Then
                            Throw New Exception("SNF Qty should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblFatRate) <= 0 Then
                            Throw New Exception(" Fat Per Kg should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblSNFRate) <= 0 Then
                            Throw New Exception("Snf Per Kg should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblFatAmount) <= 0 Then
                            Throw New Exception(" Fat Value should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblSNFAmount) <= 0 Then
                            Throw New Exception("SNF Value should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If clsCommon.myCdbl(DblNetMilkRate) <= 0 Then
                            Throw New Exception("Rate/100Kg should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If (String.IsNullOrEmpty(strPriceCode)) Or clsCommon.myLen(strPriceCode) > 30 Then
                            Throw New Exception("Length of Sale Price Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        qry = "select COUNT(*) from TSPL_BulkSalePrice_MASTER where Price_Code='" + strPriceCode + "'"
                        i = clsDBFuncationality.getSingleValue(qry, trans)
                        If i <= 0 Then
                            Throw New Exception("Sale Price Code '" + strPriceCode + "' does not exist At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblFATweightage) <= 0 Then
                            Throw New Exception("Fat% should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If clsCommon.myCdbl(DblSNFWeightage) <= 0 Then
                            Throw New Exception("SNF% should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblFATRatio) <= 0 Then
                            Throw New Exception("FAT should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        If clsCommon.myCdbl(DblSNFRatio) <= 0 Then
                            Throw New Exception("SNF should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        If clsCommon.myCdbl(DblClr) <= 0 Then
                            Throw New Exception("Sale_CLR should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If

                        objGateEntry = New clsGateEntrySale()
                        objWeighmentEntry = New ClsWeighmentEntry()
                        objLoadingEntry = New ClsLoadingTanker()
                        objQCEntry = New ClsQualityCheckBulkSale()
                        objDispatchMasterEntry = New ClsDispatchBulkSale()
                        objDispatchDetailEntry = New clsDispatchDetailBulkSale()
                        objGateOutEntry = New ClsTankerOut()



                        '' insert data into gate entry objects
                        objGateEntry.Document_Date = strGate_entry_Date
                        objGateEntry.Tanker_No = strTankerNo
                        objGateEntry.Location_Code = strLocationCode
                        objGateEntry.Customer_Code = strCustomerCode
                        objGateEntry.IsSaleReturn = "N"

                        ''---save and post data of gate entry class
                        clsGateEntrySale.SaveData(objGateEntry, True, trans)
                        clsGateEntrySale.PostData("", "", objGateEntry.Document_No, trans)

                        ''-----------------------------------------


                        '' insert data into Weighment entry objects
                        objWeighmentEntry.Weighment_Date = strGate_entry_Date
                        objWeighmentEntry.GateEntry_Document_No = objGateEntry.Document_No
                        objWeighmentEntry.Tanker_No = strTankerNo
                        objWeighmentEntry.Location_Code = strLocationCode
                        objWeighmentEntry.Gross_Weight = DblGrossWeight
                        objWeighmentEntry.Tare_Weight = DblTareWeight
                        objWeighmentEntry.Net_Weight = DblNetWeight

                        '' save and post data of weighment entry class
                        ClsWeighmentEntry.SaveData(objWeighmentEntry, True, trans)
                        ClsWeighmentEntry.PostData("", "", objWeighmentEntry.Weighment_No, trans)


                        '' insert data into Loading Tanker entry objects
                        objLoadingEntry.LoadingTanker_Date = strGate_entry_Date
                        objLoadingEntry.Weighment_No = objWeighmentEntry.Weighment_No
                        objLoadingEntry.Tanker_No = strTankerNo
                        objLoadingEntry.Location_Code = strLocationCode
                        objLoadingEntry.Item_Code = strItemCode
                        objLoadingEntry.Silo_No = strSilo
                        objLoadingEntry.Quantity = 0

                        '' save and post data of Loading Tanker entry class
                        ClsLoadingTanker.SaveData(objLoadingEntry, True, trans)
                        ClsLoadingTanker.PostData("", "", objLoadingEntry.LoadingTanker_No, trans)

                        '' insert data into Quality Check entry objects
                        objQCEntry.QC_Date = strGate_entry_Date
                        objQCEntry.Weighment_No = objWeighmentEntry.Weighment_No
                        objQCEntry.LoadingTanker_No = objLoadingEntry.LoadingTanker_No
                        objQCEntry.GateEntry_Document_No = objGateEntry.Document_No
                        objQCEntry.Tanker_No = strTankerNo
                        objQCEntry.Location_Code = strLocationCode
                        objQCEntry.Silo_No = strSilo
                        objQCEntry.Correction_Factor = 0.14
                        objQCEntry.Item_Code = strItemCode
                        objQCEntry.Unit_code = "KG"
                        objQCEntry.Qty = 0
                        objQCEntry.Fat = DblFatPer
                        objQCEntry.CLR = DblClr
                        objQCEntry.SNF = DblSnfPer
                        objQCEntry.Remarks = ""
                        objQCEntry.Customer_Code = strCustomerCode


                        '' save and post data of Quality Check entry class
                        ClsQualityCheckBulkSale.SaveData(objQCEntry, True, trans)
                        ClsQualityCheckBulkSale.PostData("", "", objQCEntry.QC_No, trans)



                        '' insert data into Dispatch Master entry objects
                        objDispatchMasterEntry.Document_Date = strGate_entry_Date
                        objDispatchMasterEntry.Customer_Code = strCustomerCode
                        objDispatchMasterEntry.QC_Code = objQCEntry.QC_No
                        objDispatchMasterEntry.Tanker_Code = strTankerNo
                        objDispatchMasterEntry.Location_Code = strLocationCode
                        objDispatchMasterEntry.Dip_marking = ""
                        objDispatchMasterEntry.Challan_No = ""
                        objDispatchMasterEntry.Gross_Weight = DblGrossWeight
                        objDispatchMasterEntry.Tare_Weight = DblTareWeight
                        objDispatchMasterEntry.Net_Weight = DblNetWeight
                        objDispatchMasterEntry.Price_Code = strPriceCode
                        objDispatchMasterEntry.Total_Amt = dblSaleAmount
                        objDispatchMasterEntry.ApprovalRequired = "N"
                        objDispatchMasterEntry.Approved = "N"
                        objDispatchMasterEntry.Status = "Open"
                        objDispatchMasterEntry.Is_Create_Auto_Invoice = 1
                        objDispatchMasterEntry.ReverseFlag = "N"

                        ''insert data into dispatch Detail bulk sale
                        ' objDispatchDetailEntry.Document_No = clsCommon.myCstr(obj.Document_No)
                        objDispatchMasterEntry.arrDispatchDetailBulkSale = New List(Of clsDispatchDetailBulkSale)

                        objDispatchDetailEntry.Item_Code = strItemCode
                        objDispatchDetailEntry.Unit_code = "KG"
                        objDispatchDetailEntry.Qty = DblNetWeight

                        objDispatchDetailEntry.FatPer = DblFatPer
                        objDispatchDetailEntry.SNFPer = DblSnfPer
                        objDispatchDetailEntry.CLR = DblClr
                        objDispatchDetailEntry.Fat_KG = dblFatKg
                        objDispatchDetailEntry.SNF_KG = DblSNFKG
                        objDispatchDetailEntry.FatAmount = DblFatAmount

                        objDispatchDetailEntry.SNFAmount = DblSNFAmount
                        objDispatchDetailEntry.NetMilkRate = DblNetMilkRate
                        objDispatchDetailEntry.Amount = dblSaleAmount
                        objDispatchDetailEntry.FatRate = DblFatRate
                        objDispatchDetailEntry.SNFRate = DblSNFRate
                        objDispatchDetailEntry.StandardRate = DblStandardrate

                        objDispatchMasterEntry.arrDispatchDetailBulkSale.Add(objDispatchDetailEntry)

                        '' save and post data of Dispatch entry class and also create auto invoice
                        ClsDispatchBulkSale.SaveData(objDispatchMasterEntry, True, trans)
                        ClsDispatchBulkSale.PostData("", "", objDispatchMasterEntry.Document_No, trans)


                        ''insert data into gate out entry objects
                        objGateOutEntry.Document_Date = strGate_entry_Date
                        objGateOutEntry.GateEntryNo = objGateEntry.Document_No
                        objGateOutEntry.Tanker_No = strTankerNo
                        objGateOutEntry.Location_Code = strLocationCode
                        objGateOutEntry.Customer_Code = strCustomerCode
                        objGateOutEntry.IsGateOut = 1

                        '' save and post data of gate out entry
                        ClsTankerOut.SaveData(objGateOutEntry, True, trans)
                    End If
                Next

                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            End If
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        Finally
            '' assign nothing to object of classes
            objGateEntry = Nothing
            objWeighmentEntry = Nothing
            objLoadingEntry = Nothing
            objQCEntry = Nothing
            objDispatchMasterEntry = Nothing
            objDispatchDetailEntry = Nothing
        End Try

        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnEwaybillupdate_Click(sender As Object, e As EventArgs) Handles btnEwaybillupdate.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim obj As New ClsInvoiceBulkSale
                obj.Document_No = clsCommon.myCstr(txtDocNo.Value)
                obj.EWayBillNo = txtEWayBillNo.Text

                If txtEWayBillDate.Checked Then
                    obj.EWayBillDate = clsCommon.GetPrintDate(txtewaybilldate.Value, "dd/MMM/yyyy")
                Else
                    obj.EWayBillDate = Nothing
                End If
                obj.Electronic_Ref_No = txtElectronicRefNo.Text
                If rdbAgainstDispatch.IsChecked Then
                    obj.InvoiceAgainst = rdbAgainstDispatch.Text
                Else
                    obj.InvoiceAgainst = rdbAgainstDispatchTrade.Text
                End If

                If ClsInvoiceBulkSale.UpdateAfterPosting(obj, Nothing) Then
                    clsCommon.MyMessageBoxShow("Information updated successfully.")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btnInvoiceJE_Click(sender As Object, e As EventArgs) Handles btnInvoiceJE.Click
        clsOpenJEAgainstInvoice.ShowInvoiceJE(txtDocNo.Value)
    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Try
            If TCSTaxApplicableOnbulkSale = True Then
                If clsCommon.myLen(FndLocationCode.Value) > 0 Then
                    Dim strCustomer As String = ""
                    If clsCommon.myLen(strCustomer) <= 0 Then
                        strCustomer = fndCustomerNo.Value
                    End If

                    txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, FndLocationCode.Value, strCustomer, "S", txtDate.Value, "Y")
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

    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", fndCustomerNo.Value, FndLocationCode.Value, True)
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
                    ''            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
                    ''richa 26 oct,2020
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & fndCustomerNo.Value & "'")), "0") = CompairStringResult.Equal Then
                            If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
                                Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsCustomerMaster.GetCustomerOutstandingForTCSTaxApplicableOnFY(fndCustomerNo.Value, txtDate.Value))
                                If dblOutstandingAmount < AmountToCheckCustomerOutstandingForTCSTax Then
                                    dblOutstandingAmount = dblOutstandingAmount + clsCommon.myCdbl(clsCommon.myFormat(lblDocumentAmount.Text))
                                    If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                                        If clsCommon.myCdbl(clsCommon.myFormat(lblDocumentAmount.Text)) > 0 Then
                                            txttcstaxbaseamount.Value = clsCommon.myCdbl(dblOutstandingAmount - AmountToCheckCustomerOutstandingForTCSTax)
                                        End If
                                    End If
                                End If

                                If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then

                                    If EnableTCSRateValidityFrom01July2021 Then
                                        Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & fndCustomerNo.Value & "'")) = 1, True, False)
                                        If Is_ITR_Filled_And_TCSAmountGreater50K = True Then
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                        Else
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                        End If
                                    Else
                                        Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & fndCustomerNo.Value & "'"))
                                        If clsCommon.myLen(panno) > 0 Then
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                        Else
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                        End If
                                    End If
                                Else
                                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = 0
                                End If
                            Else
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
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
    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", fndCustomerNo.Value, FndLocationCode.Value, True)
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
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & fndCustomerNo.Value & "'")), "0") = CompairStringResult.Equal Then
                                    If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
                                        Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsCustomerMaster.GetCustomerOutstandingForTCSTaxApplicableOnFY(fndCustomerNo.Value, txtDate.Value))
                                        If dblOutstandingAmount < AmountToCheckCustomerOutstandingForTCSTax Then
                                            dblOutstandingAmount = dblOutstandingAmount + clsCommon.myCdbl(clsCommon.myFormat(lblDocumentAmount.Text))
                                            If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                                                If clsCommon.myCdbl(clsCommon.myFormat(lblDocumentAmount.Text)) > 0 Then
                                                    txttcstaxbaseamount.Value = clsCommon.myCdbl(dblOutstandingAmount - AmountToCheckCustomerOutstandingForTCSTax)
                                                End If
                                            End If
                                        End If
                                        If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then

                                            If EnableTCSRateValidityFrom01July2021 Then
                                                Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & fndCustomerNo.Value & "'")) = 1, True, False)
                                                If Is_ITR_Filled_And_TCSAmountGreater50K = True Then
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                                Else
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                                End If
                                            Else
                                                Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & fndCustomerNo.Value & "'"))
                                                If clsCommon.myLen(panno) > 0 Then
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                                Else
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                                End If
                                            End If
                                        Else
                                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                                        End If
                                    Else
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = dr("TaxRate")
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
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(txtTaxGroup.Value)
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
                    ' gv1.Rows(intRowNo).Cells(colItemwiseTaxCode).Value = Nothing
                End If
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing

            End If
        Next
    End Sub

    Private Sub lblDocumentAmount_TextChanged(sender As Object, e As EventArgs) Handles lblDocumentAmount.TextChanged
        Try
            If TCSTaxApplicableOnbulkSale Then
                If clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                    lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(lblDocumentAmount.Text)
                    SetTaxDetails()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class
