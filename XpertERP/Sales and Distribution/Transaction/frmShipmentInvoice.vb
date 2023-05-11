Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common
Imports System.IO
''BM00000000497
''BM00000000455
''BM00000000639
''BM00000000821
'--Updation By [Pankaj Kumar Chaudhary]-Against-Ticket No-[BM00000000643, BM00000001626]
Public Class frmShipmentInvoice
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variables"
    Dim isNewEntry As Boolean
    Dim iiDeadlockErrors As Integer
    Dim isRakeshSharmaClicked As Boolean = False
    Dim strCustAccount As String
    Dim sql As String
    Dim dr As SqlDataReader
    Dim ds As New DataTable

    Dim tableName As String = "TSPL_SHIPMENT_MASTER"
    Dim tableCode As String = "Shipment_No"
    Dim codePrefix As String = "SHPNO"
    Dim noOfZero As Integer = 3
    Dim userCode, companyCode As String
    Dim l1User, l2User, l3User, l4User, l5User As String
    Private ttlItemShpQtyForCheck As Decimal = 0
    Dim btntooltip As ToolTip = New ToolTip()


    Dim dtStartTime As DateTime = DateTime.Now
    Dim span As TimeSpan
    Dim dtEndTime As DateTime
    Dim strMsg As String = ""


    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False

    Const ReportID As String = "ShipmentInvoiceGrid"
    Public strLoadOutNo As String
    Dim conversionnumber As Decimal
    Dim emptyvalue123 As Decimal = 0
    Dim checkpost As String = "N"
    Dim Invoiceno As String = String.Empty
    Dim TaxGroupvalue As String = String.Empty

    Dim repoActualBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim customdatarow As DataRow = Nothing
    Dim startdate As String
    Dim mrp As String

    Const ColComplete As String = "complete"
    Const ColICode As String = "itemCode"
    Const ColItemName As String = "itemName"
    Const colPriceDateColumn As String = "pricedate"
    Const colOrderedQty As String = "orderedQty"
    Const colShippedQty As String = "shippedQty"
    Const colBalanceQty As String = "balanceQty"
    Const colUnitCode As String = "unitCode"
    Const colSchemeApplicable As String = "schemeApplicable"
    Const colMainItem As String = "mainitem"
    Const collocation As String = "location"
    Const colPriceCode As String = "priceCode"
    Const colSampleItem As String = "sampleItem"
    Const colEmptyValue As String = "emptyValue"
    Const colMRP As String = "mrp"
    Const colMRPInBottle As String = "MRPINBOTTLE"
    Const colBasicAmount As String = "basicAmount"
    Const colDiscountAmount As String = "discountAmount"
    Const colcustDiscount As String = "custDiscount"
    Const colitemNetAmount As String = "itemNetAmount"
    Const colTaxamount As String = "taxamount"
    Const colTPT As String = "tpt"
    Const colTotalMRP As String = "totalMRP"
    Const colTotalBasicAmount As String = "totalBasicAmount"
    Const colTotalDiscountAmount As String = "totalDiscountAmount"
    Const colTotalCustDiscount As String = "totalCustDiscount"
    Const colTotalTaxAmount As String = "totaltaxamount"
    Const colTotalTPT As String = "totalTPT"
    Const colTotalItemAmount As String = "totalItemAmount"
    Const colPromoSchemeApplicable As String = "promoSchemeApplicable"
    Const colSchemeDiscountApplicable As String = "schemeDiscountApplicable"
    Const colSchemeCodeItem As String = "schemeCodeItem"
    Const colPromoSchemeCode As String = "promoSchemeCode"
    Const colSchemeCodeDiscount As String = "schemeCodeDiscount"
    Const colSchemeItem As String = "schemeItem"
    Const colPromoSchemeItem As String = "promoSchemeItem"
    Const colFromSchemeCode As String = "fromSchemeCode"
    Const colEmptyValueShell As String = "emptyValueShell"
    Const colEmptyValueBottle As String = "emptyValueBottle"
    Const colTransferBasicAmount As String = "transferBasicAmount"
    Const colTax1Rate As String = "tax1Rate"
    Const colTax2Rate As String = "tax2Rate"
    Const colTax3Rate As String = "tax3Rate"
    Const colTax4Rate As String = "tax4Rate"
    Const colTax5Rate As String = "tax5Rate"
    Const colTax6Rate As String = "tax6Rate"
    Const colAssess1 As String = "assess1"
    Const colAssess2 As String = "assess2"
    Const colAssess3 As String = "assess3"
    Const colAssess4 As String = "assess4"
    Const colAssess5 As String = "assess5"
    Const colAssess6 As String = "assess6"
    Const colTax1Amt As String = "tax1Amt"
    Const colTax2Amt As String = "tax2Amt"
    Const colTax3Amt As String = "tax3Amt"
    Const colTax4Amt As String = "tax4Amt"
    Const colTax5Amt As String = "tax5Amt"
    Const colTax6Amt As String = "tax6Amt"
    Const colCheckvalue As String = "checkvalue"

    Const colBatchNumber As String = "batchnumber"
    Const colTotalNetAmount As String = "totalNetAmount"
    Const colDiscountCode As String = "COLDISCOUNTCODE"
    Const ColCustDisNoTax As String = "COLCUSTDISNOTAX"
    Const ColTargetDisAmt As String = "COLTARGETDISAMT"
    Const ColActualBalQty As String = "COLACTUALBALANCE"
    Const ColPriceToShow As String = "PRICETOSHOW"
    Const colPriceDateActual As String = "PriceDateActual"
    Const colAbatementRate As String = "colAbatementRate"

    ''Tax Grid Columns
    Const colTAssessibleAmount As String = "assessibleAmount"
    Const colTTaxAmount As String = "taxAmount"
    Const colTTaxRate As String = "taxRate"
    Const colTBasicAmount As String = "basicAmount"
    Dim dtOldData As New DataTable()
#End Region

    Public Sub New()
        InitializeComponent()
        userCode = objCommonVar.CurrentUserCode
        companyCode = objCommonVar.CurrentCompanyCode
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("LOAD-OUT")
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnAdd.Visible = MyBase.isModifyFlag
        btnSaveAndPrint.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag

    End Sub

    Private Sub FrmShipment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        txtshellqty.Text = 0


        handlers()
        resetForm()
        cboModeOfTransport.Text = "By Road"
        pageLoadOut.IsContentVisible = True
        globalFunc.mandatoryText(txtTripNo, fndTaxGroup.txtValue)
        globalFunc.mandatoryDropdown(cboLoadOutType, cboModeOfTransport, cboPriceDate)
        Dim lds As DataTable = clsDBFuncationality.GetDataTable(clsERPFuncationality.UserAvailableLocationQuery)
        If lds.Rows.Count = 1 Then
            txtLocation.Value = lds.Rows(0)("Code").ToString()
        End If
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AskForDate, clsFixedParameterCode.AskForDate, Nothing)) = 1 Then
            txtDate.ShowCheckBox = True
        End If

        btntooltip.SetToolTip(btnAdd, "Press Alt+S for Save/Update Trasnaction")
        btntooltip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        btntooltip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        btntooltip.SetToolTip(btnClose, "Press Esc Close the Window")
        btntooltip.SetToolTip(btnReset, "Press Alt+N Adding New Transaction")
        btntooltip.SetToolTip(btnPrint, "Press Alt+R For Print")
        btntooltip.SetToolTip(btnSaveAndPrint, "Press Ctrl+S For Save and Print Print")


        gvTax.AllowColumnReorder = False
        gvTax.AllowRowReorder = False
        gvTax.EnableSorting = False

        LoadBlankGrid()
        txtShipmentTotal.Text = 0

        pvLoadOut.SelectedPage = pageLoadOut
        LoadTransactionType()
        resetdata()
        lblManualAmt.Visible = False
        txtMannualAmt.Visible = False
        lblManualQty.Visible = False
        txtMannualQty.Visible = False
        chkCreateEmpty.Visible = False
        cboLoadOutType.Text = "Transfer"
        loadOutTypeChanged(cboLoadOutType.Text)
        If clsCommon.myLen(strLoadOutNo) > 0 Then
            txtDocNo.Value = strLoadOutNo
            LoadData(strLoadOutNo, NavigatorType.Current)
        ElseIf clsCommon.myLen(Me.Tag) > 0 Then
            txtDocNo.Value = clsCommon.myCstr(Me.Tag)
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        Else
            resetdata()
        End If
      
        txtTransferNo.Focus()
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        dtOldData = New DataTable()

        Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoComplete.FormatString = ""
        repoComplete.HeaderText = "Complete"
        repoComplete.Name = ColComplete
        repoComplete.ReadOnly = True
        repoComplete.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoComplete)
        dtOldData.Columns.Add(ColComplete, GetType(String))

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = ColICode
        repoICode.Width = 60
        gv1.MasterTemplate.Columns.Add(repoICode)
        dtOldData.Columns.Add(ColICode, GetType(String))

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.AllowSort = False
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Name"
        repoIName.Name = ColItemName
        repoIName.ReadOnly = True
        repoIName.Width = 64
        gv1.MasterTemplate.Columns.Add(repoIName)
        dtOldData.Columns.Add(ColItemName, GetType(String))

        Dim repoPriceDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoPriceDate.Format = DateTimePickerFormat.Custom
        repoPriceDate.CustomFormat = "dd-MM-yyyy"
        repoPriceDate.HeaderText = "Price Date"
        repoPriceDate.WrapText = True
        repoPriceDate.FormatString = "{0:d}"
        repoPriceDate.Name = colPriceDateColumn
        repoPriceDate.ReadOnly = True
        repoPriceDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoPriceDate)
        dtOldData.Columns.Add(colPriceDateColumn, GetType(Date))

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Ordered Quantity"
        repoOrderQty.Name = colOrderedQty
        repoOrderQty.Width = 80
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOrderQty.ReadOnly = True
        repoOrderQty.Minimum = 0
        repoOrderQty.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoOrderQty)
        dtOldData.Columns.Add(colOrderedQty, GetType(Decimal))

        Dim repoShipedQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShipedQty.FormatString = ""
        repoShipedQty.HeaderText = "Shipped Quantity"
        repoShipedQty.Name = colShippedQty
        repoShipedQty.Width = 80
        repoShipedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoShipedQty.ReadOnly = False
        repoShipedQty.Minimum = 0
        repoShipedQty.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoShipedQty)
        dtOldData.Columns.Add(colShippedQty, GetType(Decimal))

        repoActualBalQty.FormatString = ""
        repoActualBalQty.HeaderText = "Actual Balance"
        repoActualBalQty.Name = ColActualBalQty
        repoActualBalQty.Width = 80
        repoActualBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoActualBalQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoActualBalQty)
        dtOldData.Columns.Add(ColActualBalQty, GetType(Decimal))

        Dim repoBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBalQty.AllowSort = False
        repoBalQty.FormatString = ""
        repoBalQty.HeaderText = "Balance Quantity"
        repoBalQty.Name = colBalanceQty
        repoBalQty.ReadOnly = True
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBalQty.Width = 94
        gv1.MasterTemplate.Columns.Add(repoBalQty)
        dtOldData.Columns.Add(colBalanceQty, GetType(Decimal))

        Dim repoUnitCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnitCode.FormatString = ""
        repoUnitCode.HeaderText = "Unit Code"
        repoUnitCode.Name = colUnitCode
        repoUnitCode.Width = 59
        repoUnitCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnitCode)
        dtOldData.Columns.Add(colUnitCode, GetType(String))

        Dim repoSchemeApp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeApp.AllowSort = False
        repoSchemeApp.FormatString = ""
        repoSchemeApp.HeaderText = "App. Qty Dis."
        repoSchemeApp.Name = colSchemeApplicable
        repoSchemeApp.ReadOnly = True
        repoSchemeApp.Width = 75
        gv1.MasterTemplate.Columns.Add(repoSchemeApp)
        dtOldData.Columns.Add(colSchemeApplicable, GetType(String))

        Dim repoMainItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMainItem.FormatString = ""
        repoMainItem.HeaderText = "Main Item"
        repoMainItem.Name = colMainItem
        repoMainItem.Width = 58
        gv1.MasterTemplate.Columns.Add(repoMainItem)
        dtOldData.Columns.Add(colMainItem, GetType(String))

        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.AllowSort = False
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location"
        repoLocation.Name = collocation
        repoLocation.ReadOnly = True
        repoLocation.Width = 51
        gv1.MasterTemplate.Columns.Add(repoLocation)
        dtOldData.Columns.Add(collocation, GetType(String))

        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.AllowSort = False
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.IsVisible = False
        repoPriceCode.Name = colPriceCode
        repoPriceCode.ReadOnly = True
        repoPriceCode.Width = 62
        gv1.MasterTemplate.Columns.Add(repoPriceCode)
        dtOldData.Columns.Add(colPriceCode, GetType(String))

        Dim repoSampleItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSampleItem.AllowSort = False
        repoSampleItem.FormatString = ""
        repoSampleItem.HeaderText = "Sample Item"
        repoSampleItem.Name = colSampleItem
        repoSampleItem.ReadOnly = True
        repoSampleItem.Width = 72
        gv1.MasterTemplate.Columns.Add(repoSampleItem)
        dtOldData.Columns.Add(colSampleItem, GetType(String))

        Dim repoEmptyValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEmptyValue.AllowSort = False
        repoEmptyValue.FormatString = ""
        repoEmptyValue.HeaderText = "Empty Value"
        repoEmptyValue.Name = colEmptyValue
        repoEmptyValue.ReadOnly = True
        repoEmptyValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoEmptyValue.Width = 72
        gv1.MasterTemplate.Columns.Add(repoEmptyValue)
        dtOldData.Columns.Add(colEmptyValue, GetType(Decimal))

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.AllowSort = False
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.ReadOnly = True
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.Width = 33
        gv1.MasterTemplate.Columns.Add(repoMRP)
        dtOldData.Columns.Add(colMRP, GetType(Decimal))

        Dim repoMRPInBottle As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRPInBottle.AllowSort = False
        repoMRPInBottle.FormatString = ""
        repoMRPInBottle.HeaderText = "Bottle MRP"
        repoMRPInBottle.Name = colMRPInBottle
        repoMRPInBottle.ReadOnly = True
        repoMRPInBottle.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRPInBottle.Width = 70
        gv1.MasterTemplate.Columns.Add(repoMRPInBottle)
        dtOldData.Columns.Add(colMRPInBottle, GetType(Decimal))

        Dim repoBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBasicAmt.AllowSort = False
        repoBasicAmt.HeaderText = "Basic Price"
        repoBasicAmt.Name = colBasicAmount
        repoBasicAmt.ReadOnly = True
        repoBasicAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBasicAmt.Width = 65
        gv1.MasterTemplate.Columns.Add(repoBasicAmt)
        dtOldData.Columns.Add(colBasicAmount, GetType(Decimal))

        Dim repoDiscountAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscountAmt.AllowSort = False
        repoDiscountAmt.HeaderText = "Discount"
        repoDiscountAmt.Name = colDiscountAmount
        repoDiscountAmt.ReadOnly = True
        repoDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDiscountAmt.Width = 52
        gv1.MasterTemplate.Columns.Add(repoDiscountAmt)
        dtOldData.Columns.Add(colDiscountAmount, GetType(Decimal))

        Dim repoCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscount.HeaderText = "Cust Dis."
        repoCustDiscount.MinWidth = 4
        repoCustDiscount.Name = colcustDiscount
        repoCustDiscount.ReadOnly = True
        repoCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDiscount.Width = 54
        gv1.MasterTemplate.Columns.Add(repoCustDiscount)
        dtOldData.Columns.Add(colcustDiscount, GetType(Decimal))

        Dim repoItemNetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemNetAmt.AllowSort = False
        repoItemNetAmt.HeaderText = "Net Price"
        repoItemNetAmt.Name = colitemNetAmount
        repoItemNetAmt.ReadOnly = True
        repoItemNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoItemNetAmt.Width = 55
        gv1.MasterTemplate.Columns.Add(repoItemNetAmt)
        dtOldData.Columns.Add(colitemNetAmount, GetType(Decimal))

        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.AllowSort = False
        repoTaxAmt.HeaderText = "Tax"
        repoTaxAmt.Name = colTaxamount
        repoTaxAmt.ReadOnly = True
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTaxAmt.Width = 27
        gv1.MasterTemplate.Columns.Add(repoTaxAmt)
        dtOldData.Columns.Add(colTaxamount, GetType(Decimal))

        Dim repoTPT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTPT.AllowSort = False
        repoTPT.HeaderText = "TPT"
        repoTPT.Name = colTPT
        repoTPT.ReadOnly = True
        repoTPT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTPT.Width = 30
        gv1.MasterTemplate.Columns.Add(repoTPT)
        dtOldData.Columns.Add(colTPT, GetType(Decimal))

        Dim repoTotalMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalMRP.AllowSort = False
        repoTotalMRP.HeaderText = "Total MRP"
        repoTotalMRP.Name = colTotalMRP
        repoTotalMRP.ReadOnly = True
        repoTotalMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalMRP.Width = 62
        gv1.MasterTemplate.Columns.Add(repoTotalMRP)
        dtOldData.Columns.Add(colTotalMRP, GetType(Decimal))

        Dim repoTotalBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalBasicAmt.AllowSort = False
        repoTotalBasicAmt.HeaderText = "Total Basic Amount"
        repoTotalBasicAmt.Name = colTotalBasicAmount
        repoTotalBasicAmt.ReadOnly = True
        repoTotalBasicAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalBasicAmt.Width = 106
        gv1.MasterTemplate.Columns.Add(repoTotalBasicAmt)
        dtOldData.Columns.Add(colTotalBasicAmount, GetType(Decimal))

        Dim repoTotalDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalDiscount.AllowSort = False
        repoTotalDiscount.HeaderText = "Total Discount"
        repoTotalDiscount.Name = colTotalDiscountAmount
        repoTotalDiscount.ReadOnly = True
        repoTotalDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalDiscount.Width = 81
        gv1.MasterTemplate.Columns.Add(repoTotalDiscount)
        dtOldData.Columns.Add(colTotalDiscountAmount, GetType(Decimal))

        Dim repoTotalCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalCustDiscount.HeaderText = "Total Cust Dis"
        repoTotalCustDiscount.Name = colTotalCustDiscount
        repoTotalCustDiscount.ReadOnly = True
        repoTotalCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalCustDiscount.Width = 79
        gv1.MasterTemplate.Columns.Add(repoTotalCustDiscount)
        dtOldData.Columns.Add(colTotalCustDiscount, GetType(Decimal))

        Dim repoTotalNetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalNetAmt.AllowSort = False
        repoTotalNetAmt.HeaderText = "Total Net Amount"
        repoTotalNetAmt.Name = colTotalNetAmount
        repoTotalNetAmt.ReadOnly = True
        repoTotalNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalNetAmt.Width = 96
        gv1.MasterTemplate.Columns.Add(repoTotalNetAmt)
        dtOldData.Columns.Add(colTotalNetAmount, GetType(Decimal))

        Dim repoTotalTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalTaxAmt.AllowSort = False
        repoTotalTaxAmt.HeaderText = "Total Tax"
        repoTotalTaxAmt.Name = colTotalTaxAmount
        repoTotalTaxAmt.ReadOnly = True
        repoTotalTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalTaxAmt.Width = 55
        gv1.MasterTemplate.Columns.Add(repoTotalTaxAmt)
        dtOldData.Columns.Add(colTotalTaxAmount, GetType(Decimal))

        Dim repoTotalTPT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalTPT.AllowSort = False
        repoTotalTPT.HeaderText = "Total TPT"
        repoTotalTPT.Name = colTotalTPT
        repoTotalTPT.ReadOnly = True
        repoTotalTPT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalTPT.Width = 58
        gv1.MasterTemplate.Columns.Add(repoTotalTPT)
        dtOldData.Columns.Add(colTotalTPT, GetType(Decimal))

        Dim repoTotalItemAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalItemAmt.AllowSort = False
        repoTotalItemAmt.HeaderText = "Total Item Amount"
        repoTotalItemAmt.Name = colTotalItemAmount
        repoTotalItemAmt.ReadOnly = True
        repoTotalItemAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalItemAmt.Width = 101
        gv1.MasterTemplate.Columns.Add(repoTotalItemAmt)
        dtOldData.Columns.Add(colTotalItemAmount, GetType(Decimal))

        Dim repoPromoSchemeApp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPromoSchemeApp.AllowSort = False
        repoPromoSchemeApp.HeaderText = "Promo Scheme App."
        repoPromoSchemeApp.Name = colPromoSchemeApplicable
        repoPromoSchemeApp.ReadOnly = True
        repoPromoSchemeApp.Width = 113
        gv1.MasterTemplate.Columns.Add(repoPromoSchemeApp)
        dtOldData.Columns.Add(colPromoSchemeApplicable, GetType(String))

        Dim repoCashSchemApp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCashSchemApp.AllowSort = False
        repoCashSchemApp.HeaderText = "Cash Scheme App."
        repoCashSchemApp.Name = colSchemeDiscountApplicable
        repoCashSchemApp.ReadOnly = True
        repoCashSchemApp.Width = 106
        gv1.MasterTemplate.Columns.Add(repoCashSchemApp)
        dtOldData.Columns.Add(colSchemeDiscountApplicable, GetType(String))

        Dim repoQtySchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQtySchemeCode.AllowSort = False
        repoQtySchemeCode.HeaderText = "Qty. Scheme Code"
        repoQtySchemeCode.Name = colSchemeCodeItem
        repoQtySchemeCode.ReadOnly = True
        repoQtySchemeCode.Width = 104
        gv1.MasterTemplate.Columns.Add(repoQtySchemeCode)
        dtOldData.Columns.Add(colSchemeCodeItem, GetType(String))

        Dim repoPromoSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPromoSchemeCode.AllowSort = False
        repoPromoSchemeCode.HeaderText = "Promo Code"
        repoPromoSchemeCode.Name = colPromoSchemeCode
        repoPromoSchemeCode.ReadOnly = True
        repoPromoSchemeCode.Width = 72
        gv1.MasterTemplate.Columns.Add(repoPromoSchemeCode)
        dtOldData.Columns.Add(colPromoSchemeCode, GetType(String))

        Dim repoSchemeCodeDiscount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeCodeDiscount.AllowSort = False
        repoSchemeCodeDiscount.HeaderText = "Cash Scheme Code"
        repoSchemeCodeDiscount.Name = colSchemeCodeDiscount
        repoSchemeCodeDiscount.ReadOnly = True
        repoSchemeCodeDiscount.Width = 110
        gv1.MasterTemplate.Columns.Add(repoSchemeCodeDiscount)
        dtOldData.Columns.Add(colSchemeCodeDiscount, GetType(String))

        Dim repoQtySchemeItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQtySchemeItem.AllowSort = False
        repoQtySchemeItem.HeaderText = "Qty Scheme Item"
        repoQtySchemeItem.Name = colSchemeItem
        repoQtySchemeItem.ReadOnly = True
        repoQtySchemeItem.Width = 96
        gv1.MasterTemplate.Columns.Add(repoQtySchemeItem)
        dtOldData.Columns.Add(colSchemeItem, GetType(String))

        Dim repoPromoSchemeItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPromoSchemeItem.AllowSort = False
        repoPromoSchemeItem.HeaderText = "Promo Item"
        repoPromoSchemeItem.Name = colPromoSchemeItem
        repoPromoSchemeItem.ReadOnly = True
        repoPromoSchemeItem.Width = 67
        gv1.MasterTemplate.Columns.Add(repoPromoSchemeItem)
        dtOldData.Columns.Add(colPromoSchemeItem, GetType(String))

        Dim repoFromSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromSchemeCode.HeaderText = "Scheme Code"
        repoFromSchemeCode.Name = colFromSchemeCode
        repoFromSchemeCode.Width = 80
        repoFromSchemeCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFromSchemeCode)
        dtOldData.Columns.Add(colFromSchemeCode, GetType(String))



        Dim repoEmptyValueShell As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEmptyValueShell.HeaderText = "Empty Value Shell"
        repoEmptyValueShell.IsVisible = False
        repoEmptyValueShell.Name = colEmptyValueShell
        repoEmptyValueShell.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEmptyValueShell)
        dtOldData.Columns.Add(colEmptyValueShell, GetType(Decimal))

        Dim repoEmptyValueBottle As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEmptyValueBottle.HeaderText = "Empty Value Bottle"
        repoEmptyValueBottle.IsVisible = False
        repoEmptyValueBottle.Name = colEmptyValueBottle
        repoEmptyValueBottle.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEmptyValueBottle)
        dtOldData.Columns.Add(colEmptyValueBottle, GetType(Decimal))

        Dim repoTransferBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTransferBasicAmt.HeaderText = "Tansfer Basic Amount"
        repoTransferBasicAmt.IsVisible = False
        repoTransferBasicAmt.Name = colTransferBasicAmount
        repoTransferBasicAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTransferBasicAmt)
        dtOldData.Columns.Add(colTransferBasicAmount, GetType(Decimal))

        Dim repoTax1Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax1Rate.HeaderText = "Tax1 Rate"
        repoTax1Rate.IsVisible = False
        repoTax1Rate.Name = colTax1Rate
        repoTax1Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax1Rate)
        dtOldData.Columns.Add(colTax1Rate, GetType(Decimal))

        Dim repoTax2Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax2Rate.HeaderText = "Tax2 Rate"
        repoTax2Rate.IsVisible = False
        repoTax2Rate.Name = colTax2Rate
        repoTax2Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax2Rate)
        dtOldData.Columns.Add(colTax2Rate, GetType(Decimal))

        Dim repoTax3Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax3Rate.HeaderText = "Tax3 Rate"
        repoTax3Rate.IsVisible = False
        repoTax3Rate.Name = colTax3Rate
        repoTax3Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax3Rate)
        dtOldData.Columns.Add(colTax3Rate, GetType(Decimal))

        Dim repoTax4Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax4Rate.HeaderText = "Tax4 Rate"
        repoTax4Rate.IsVisible = False
        repoTax4Rate.Name = colTax4Rate
        repoTax4Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax4Rate)
        dtOldData.Columns.Add(colTax4Rate, GetType(Decimal))

        Dim repoTax5Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax5Rate.HeaderText = "Tax5 Rate"
        repoTax5Rate.IsVisible = False
        repoTax5Rate.Name = colTax5Rate
        repoTax5Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax5Rate)
        dtOldData.Columns.Add(colTax5Rate, GetType(Decimal))

        Dim repoTax6Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax6Rate.HeaderText = "Tax6 Rate"
        repoTax6Rate.IsVisible = False
        repoTax6Rate.Name = colTax6Rate
        repoTax6Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax6Rate)
        dtOldData.Columns.Add(colTax6Rate, GetType(Decimal))

        Dim repoTax1Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax1Assess.HeaderText = "Tax1 Assess"
        repoTax1Assess.IsVisible = False
        repoTax1Assess.Name = colAssess1
        repoTax1Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax1Assess)
        dtOldData.Columns.Add(colAssess1, GetType(Decimal))

        Dim repoTax2Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax2Assess.HeaderText = "Tax2 Assess"
        repoTax2Assess.IsVisible = False
        repoTax2Assess.Name = colAssess2
        repoTax2Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax2Assess)
        dtOldData.Columns.Add(colAssess2, GetType(Decimal))

        Dim repoTax3Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax3Assess.HeaderText = "Tax3 Assess"
        repoTax3Assess.IsVisible = False
        repoTax3Assess.Name = colAssess3
        repoTax3Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax3Assess)
        dtOldData.Columns.Add(colAssess3, GetType(Decimal))

        Dim repoTax4Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax4Assess.HeaderText = "Tax4 Assess"
        repoTax4Assess.IsVisible = False
        repoTax4Assess.Name = colAssess4
        repoTax4Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax4Assess)
        dtOldData.Columns.Add(colAssess4, GetType(Decimal))

        Dim repoTax5Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax5Assess.HeaderText = "Tax5 Assess"
        repoTax5Assess.IsVisible = False
        repoTax5Assess.Name = colAssess5
        repoTax5Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax5Assess)
        dtOldData.Columns.Add(colAssess5, GetType(Decimal))

        Dim repoTax6Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax6Assess.HeaderText = "Tax6 Assess"
        repoTax6Assess.IsVisible = False
        repoTax6Assess.Name = colAssess6
        repoTax6Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax6Assess)
        dtOldData.Columns.Add(colAssess6, GetType(Decimal))

        Dim repotax1Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax1Amt.ReadOnly = True
        repotax1Amt.HeaderText = "Tax1 Amt"
        repotax1Amt.IsVisible = False
        repotax1Amt.Name = colTax1Amt
        repotax1Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax1Amt)
        dtOldData.Columns.Add(colTax1Amt, GetType(Decimal))

        Dim repotax2Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax2Amt.ReadOnly = True
        repotax2Amt.HeaderText = "Tax2 Amt"
        repotax2Amt.IsVisible = False
        repotax2Amt.Name = colTax2Amt
        repotax2Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax2Amt)
        dtOldData.Columns.Add(colTax2Amt, GetType(Decimal))

        Dim repotax3Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax3Amt.ReadOnly = True
        repotax3Amt.HeaderText = "Tax3 Amt"
        repotax3Amt.IsVisible = False
        repotax3Amt.Name = colTax3Amt
        repotax3Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax3Amt)
        dtOldData.Columns.Add(colTax3Amt, GetType(Decimal))

        Dim repotax4Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax4Amt.ReadOnly = True
        repotax4Amt.HeaderText = "Tax4 Amt"
        repotax4Amt.IsVisible = False
        repotax4Amt.Name = colTax4Amt
        repotax4Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax4Amt)
        dtOldData.Columns.Add(colTax4Amt, GetType(Decimal))

        Dim repotax5Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax5Amt.ReadOnly = True
        repotax5Amt.HeaderText = "Tax5 Amt"
        repotax5Amt.IsVisible = False
        repotax5Amt.Name = colTax5Amt
        repotax5Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax5Amt)
        dtOldData.Columns.Add(colTax5Amt, GetType(Decimal))

        Dim repotax6Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax6Amt.ReadOnly = True
        repotax6Amt.HeaderText = "Tax6 Amt"
        repotax6Amt.IsVisible = False
        repotax6Amt.Name = colTax6Amt
        repotax6Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax6Amt)
        dtOldData.Columns.Add(colTax6Amt, GetType(Decimal))

        Dim repoCheckValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCheckValue.ReadOnly = True
        repoCheckValue.HeaderText = "Check Value"
        repoCheckValue.IsVisible = False
        repoCheckValue.Name = colCheckvalue
        gv1.MasterTemplate.Columns.Add(repoCheckValue)
        dtOldData.Columns.Add(colCheckvalue, GetType(String))

        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colBatchNumber
        repoBatchNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBatchNo.IsVisible = False
        repoBatchNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBatchNo)
        dtOldData.Columns.Add(colBatchNumber, GetType(String))

        Dim repoDiscCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDiscCode.FormatString = ""
        repoDiscCode.HeaderText = "Discount Code"
        repoDiscCode.Name = colDiscountCode
        repoDiscCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoDiscCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDiscCode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoDiscCode)
        dtOldData.Columns.Add(colDiscountCode, GetType(String))

        Dim repoCustDisNoTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDisNoTax.FormatString = ""
        repoCustDisNoTax.HeaderText = "Cust Discount Without Tax"
        repoCustDisNoTax.Name = ColCustDisNoTax
        repoCustDisNoTax.Width = 80
        repoCustDisNoTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDisNoTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustDisNoTax)
        dtOldData.Columns.Add(ColCustDisNoTax, GetType(Decimal))

        Dim repoTargetDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTargetDisAmt.FormatString = ""
        repoTargetDisAmt.HeaderText = "Target Discount Amount"
        repoTargetDisAmt.Name = ColTargetDisAmt
        repoTargetDisAmt.Width = 80
        repoTargetDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTargetDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTargetDisAmt)
        dtOldData.Columns.Add(ColTargetDisAmt, GetType(Decimal))

        
        Dim repoPriceToShow As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPriceToShow.FormatString = ""
        repoPriceToShow.HeaderText = "Price"
        repoPriceToShow.Name = ColPriceToShow
        repoPriceToShow.Width = 80
        repoPriceToShow.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPriceToShow.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPriceToShow)
        dtOldData.Columns.Add(ColPriceToShow, GetType(Decimal))

        Dim repoPriceDateActual As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoPriceDateActual.Format = DateTimePickerFormat.Custom
        repoPriceDateActual.CustomFormat = "dd-MM-yyyy"
        repoPriceDateActual.HeaderText = "Price Date Actual"
        repoPriceDateActual.FormatString = "{0:d}"
        repoPriceDateActual.Name = colPriceDateActual
        repoPriceDateActual.WrapText = True
        repoPriceDateActual.ReadOnly = True
        repoPriceDateActual.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPriceDateActual)
        dtOldData.Columns.Add(colPriceDateActual, GetType(Date))

        Dim repoAbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatementRate.FormatString = ""
        repoAbatementRate.HeaderText = "Abatement Rate"
        repoAbatementRate.Name = colAbatementRate
        repoAbatementRate.Width = 80
        repoAbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAbatementRate.ReadOnly = True
        repoAbatementRate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAbatementRate)
        dtOldData.Columns.Add(colAbatementRate, GetType(Decimal))

        gv1.AllowAddNewRow = True ''Change false to ture
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.EnableSorting = False
        ReStoreGridLayout()
    End Sub

    Private Sub LoadTransactionType()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Retail"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "T"
        dr("Name") = "Tax"
        dt.Rows.Add(dr)

        CmbTransaction.DataSource = dt
        CmbTransaction.ValueMember = "Code"
        CmbTransaction.DisplayMember = "Name"
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub handlers()

        AddHandler txtSchemeSample.txtValue.TextChanged, AddressOf fndSchemeSample_TextChanged
        AddHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
    End Sub

     

    Private Sub fndTaxGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndTaxGroup.Load
        fndTaxGroup.ConnectionString = connectSql.SqlCon()
        fndTaxGroup.Query = clsERPFuncationality.UserAvailableTaxGroup + " AND M.Tax_Group_Type='S'"
        fndTaxGroup.ValueToSelect = "Code"
        fndTaxGroup.ValueToSelect1 = "Description"
        fndTaxGroup.Caption = "Tax Groups"
    End Sub

    Private Sub fndSchemeSample_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSchemeSample.Load
        sql = "SELECT Scheme_Code as 'Scheme Code', Scheme_Desc as Description, Start_Date as 'Start Date' FROM TSPL_SCHEME_MASTER WHERE Scheme_Type = 'S' ORDER BY Scheme_Code"
        txtSchemeSample.ConnectionString = connectSql.SqlCon()
        txtSchemeSample.Query = sql
        txtSchemeSample.ValueToSelect = "Scheme Code"
        txtSchemeSample.ValueToSelect1 = "Description"
        txtSchemeSample.Caption = "Sample Schemes"
    End Sub

    Private Sub fndSalesman_TextChanged()
        sql = "Select Emp_Name from TSPL_EMPLOYEE_MASTER WHERE Emp_Code='" + txtSalesman.Value + "'"
        lblSalesMan1.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
    End Sub

    Private Sub fndRouteNo_TextChanged()
        sql = "Select Route_Desc,Employee_Code from TSPL_ROUTE_MASTER where Route_No='" + txtRouteNo.Value + "'"
        Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then

            lblRouteDesc.Text = dr1.Rows(0)(0).ToString()
            txtSalesman.Value = dr1.Rows(0)(1).ToString()

        Else
            lblRouteDesc.Text = String.Empty
        End If
    End Sub

    Private Sub fndSchemeSample_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If chkSample.ToggleState = ToggleState.Off Then
            Try
                sql = "SELECT SD.Scheme_Item_Code, SD.Scheme_Item_Desc, SD.Qty, SD.UOM,SD.Price_Date,SD.MRP,SD.Item_Basic_Price FROM TSPL_SCHEME_MASTER AS SM INNER JOIN " & _
                      " TSPL_SCHEME_DETAILS AS SD ON SM.Scheme_Code = SD.Scheme_Code INNER JOIN TSPL_ITEM_MASTER AS IM ON SD.Scheme_Item_Code = " & _
                      " IM.Item_Code WHERE (SM.Scheme_Code = '" + txtSchemeSample.txtValue.Text + "')"
                Dim dr4 As DataTable = clsDBFuncationality.GetDataTable(sql)
                Dim i As Integer = gv1.Rows.Count
                ' Dim conversionfactor As Decimal
                If dr4 IsNot Nothing AndAlso dr4.Rows.Count > 0 Then

                    If txtCustomer.Value = String.Empty Then
                        txtSchemeSample.txtValue.Text = ""
                        common.clsCommon.MyMessageBoxShow("Please choose customer first.")
                        txtCustomer.Focus()
                        Exit Sub
                    ElseIf txtLocation.Value.Trim() = String.Empty Then
                        txtSchemeSample.txtValue.Text = String.Empty
                        common.clsCommon.MyMessageBoxShow("Please choose location first.")
                        txtLocation.Focus()
                        Exit Sub
                    End If


                    If Not checkItemonLocation(dr4.Rows(0)(0).ToString(), dr4.Rows(0)(2).ToString(), txtLocation.Value, dr4.Rows(0)(3).ToString(), clsCommon.myCdbl(dr4.Rows(0)(5).ToString()), True, " ") Then
                        For i = gv1.Rows.Count - 1 To 1 Step -1
                            If gv1.Rows(i).Cells(colSampleItem).Value = "Yes" Then
                                gv1.Rows.RemoveAt(i)
                            End If
                        Next
                    End If
                    Dim viewInfo As New GridViewInfo(gv1.MasterTemplate)
                    Dim dataRowInfo As New GridViewDataRowInfo(viewInfo)
                    sql = "SELECT Empty_Value_Bottle , Empty_Value_Shell   FROM TSPL_ITEM_PRICE_MASTER WHERE Item_Code = '" + Convert.ToString(dr4.Rows(0)("Scheme_Item_Code")) + "' AND " & _
                "UOM = '" + Convert.ToString(dr4.Rows(0)("UOM")) + "' AND Item_Basic_Net = '" + Convert.ToString(dr4.Rows(0)("MRP")) + "' AND " & _
                " Item_Basic_Price='" + Convert.ToString(dr4.Rows(0)("Item_Basic_Price")) + "'"
                    Dim emptydr As DataTable = clsDBFuncationality.GetDataTable(sql)
                    If emptydr IsNot Nothing AndAlso emptydr.Rows.Count > 0 Then

                        dataRowInfo.Cells(colEmptyValueBottle).Value = emptydr.Rows(0)("Empty_Value_Bottle")
                        dataRowInfo.Cells(colEmptyValueShell).Value = emptydr.Rows(0)("Empty_Value_Shell")
                    End If

                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + Convert.ToString(dr4.Rows(0)("Scheme_Item_Code")) + "' AND " & _
                        " Uom_Code='" + Convert.ToString(dr4.Rows(0)("UOM")) + "'"
                    Dim convertFact As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                    If Not convertFact = 0 Then
                        Dim shells As Integer
                        Dim bottles As Decimal = clsCommon.myCdbl(dr4.Rows(0)("Qty")) Mod convertFact
                        If bottles = 0 Then
                            shells = (clsCommon.myCdbl(dr4.Rows(0)("Qty")) - bottles) / convertFact
                            dataRowInfo.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueBottle).Value) * (shells) * convertFact + clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueBottle).Value) * bottles, 2)
                        Else
                            shells = (clsCommon.myCdbl(dataRowInfo.Cells(colShippedQty).Value) - bottles) / convertFact + 1
                            dataRowInfo.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueBottle).Value) * (shells - 1) * convertFact + clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueBottle).Value) * bottles, 2)
                        End If
                        If bottles = 0 And shells = 0 Then
                            dataRowInfo.Cells(colEmptyValue).Value = clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueBottle).Value) + clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueShell).Value)
                        End If

                    End If
                    dataRowInfo.Cells(ColComplete).Value = "Yes"
                    dataRowInfo.Cells(ColICode).Value = dr4.Rows(0)("Scheme_Item_Code").ToString()
                    dataRowInfo.Cells(ColItemName).Value = dr4.Rows(0)("Scheme_Item_Desc").ToString()
                    dataRowInfo.Cells(colPriceDateColumn).Value = Format(CDate(dr4.Rows(0)("Price_Date").ToString()), "dd/MM/yyyy")
                    dataRowInfo.Cells(colPriceCode).Value = txtPriceCode.Text
                    dataRowInfo.Cells(colOrderedQty).Value = "0"
                    dataRowInfo.Cells("shippedQty").ReadOnly = True
                    dataRowInfo.Cells(colBalanceQty).Value = "0"
                    dataRowInfo.Cells(colUnitCode).Value = dr4.Rows(0)("UOM").ToString()
                    dataRowInfo.Cells(collocation).Value = txtLocation.Value
                    dataRowInfo.Cells(colSchemeCodeItem).Value = ""
                    dataRowInfo.Cells(colSchemeItem).Value = "No"
                    dataRowInfo.Cells(colSchemeApplicable).Value = "No"
                    dataRowInfo.Cells(colPromoSchemeApplicable).Value = "No"
                    dataRowInfo.Cells(colPromoSchemeItem).Value = "No"
                    dataRowInfo.Cells(colPromoSchemeCode).Value = ""
                    dataRowInfo.Cells(colSchemeDiscountApplicable).Value = "No"
                    dataRowInfo.Cells(colSchemeCodeDiscount).Value = ""
                    dataRowInfo.Cells(colSampleItem).Value = "Yes"
                    dataRowInfo.Cells(colcustDiscount).Value = 0
                    dataRowInfo.Cells(colTotalCustDiscount).Value = 0
                    dataRowInfo.Cells(colTransferBasicAmount).Value = dr4.Rows(0)("Item_Basic_Price").ToString()
                    Dim itemMrp As Decimal = clsCommon.myCdbl(dr4.Rows(0)("MRP"))
                    dataRowInfo.Cells(colMRP).Value = itemMrp

                    dataRowInfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(dataRowInfo.Cells(ColICode).Value), clsCommon.myCstr(dataRowInfo.Cells(colUnitCode).Value), clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value))
                    Dim assessibleamt As Decimal = Math.Round(itemMrp * abatement(dataRowInfo, Nothing) / 100, 2)
                    sql = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"
                    Dim strexcisable As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
                    If strexcisable = "T" Then
                        dataRowInfo.Cells(colMRP).Value = dr4.Rows(0)("mrp").ToString()
                        dataRowInfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(dataRowInfo.Cells(ColICode).Value), clsCommon.myCstr(dataRowInfo.Cells(colUnitCode).Value), clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value))
                        dataRowInfo.Cells(colTaxamount).Value = 0
                        SnDUtility.calculateTax(assessibleamt, clsCommon.myCdbl(dr4.Rows(0)("Item_Basic_Price").ToString()), txtLocation.Value, gv1, gvTax)
                        For Each gr As GridViewRowInfo In gvTax.Rows
                            dataRowInfo.Cells("Tax" + (gr.Index + 1).ToString() + "Rate").Value = gr.Cells(2).Value
                            dataRowInfo.Cells("assess" + (gr.Index + 1).ToString()).Value = gr.Cells(4).Value
                            dataRowInfo.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value = gr.Cells(5).Value
                            sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + gr.Cells(0).Value + "'"
                            If clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql)) = "N" Then
                                dataRowInfo.Cells("Tax" + (gr.Index + 1).ToString() + "Rate").Value = 0
                                dataRowInfo.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value = 0
                            End If
                            dataRowInfo.Cells(colTaxamount).Value = clsCommon.myCdbl(dataRowInfo.Cells(colTaxamount).Value) + clsCommon.myCdbl(gr.Cells(5).Value)
                        Next
                    Else
                        dataRowInfo.Cells(colTaxamount).Value = 0
                        dataRowInfo.Cells(colMRP).Value = clsCommon.myCdbl(dr4.Rows(0)("mrp"))
                        dataRowInfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(dataRowInfo.Cells(ColICode).Value), clsCommon.myCstr(dataRowInfo.Cells(colUnitCode).Value), clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value))
                        For Each gr As GridViewRowInfo In gvTax.Rows
                            dataRowInfo.Cells("Tax" + (gr.Index + 1).ToString() + "Rate").Value = 0
                            dataRowInfo.Cells("assess" + (gr.Index + 1).ToString()).Value = 0
                            dataRowInfo.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value = 0
                        Next
                    End If


                    dataRowInfo.Cells(colBasicAmount).Value = dr4.Rows(0)("Item_Basic_Price").ToString()

                    dataRowInfo.Cells(colDiscountAmount).Value = "0"
                    dataRowInfo.Cells(colitemNetAmount).Value = dr4.Rows(0)("Item_Basic_Price").ToString()
                    dataRowInfo.Cells(colTotalBasicAmount).Value = "0"
                    dataRowInfo.Cells(colTotalDiscountAmount).Value = "0"
                    dataRowInfo.Cells(colTotalNetAmount).Value = "0"
                    dataRowInfo.Cells(colTPT).Value = "0"
                    dataRowInfo.Cells(colTotalTPT).Value = "0"
                    dataRowInfo.Cells(colFromSchemeCode).Value = txtSchemeSample.txtValue.Text
                    dataRowInfo.Cells(colShippedQty).Value = clsCommon.myCdbl(dr4.Rows(0)(2))
                    dataRowInfo.Cells(colTotalTaxAmount).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colShippedQty).Value) * clsCommon.myCdbl(dataRowInfo.Cells(colTaxamount).Value), 2)
                    dataRowInfo.Cells(colTotalMRP).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colShippedQty).Value) * clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value), 2)
                    dataRowInfo.Cells(colTotalItemAmount).Value = dataRowInfo.Cells(colTotalTaxAmount).Value
                    gv1.Rows.Insert(i, dataRowInfo)
                    i = i + 1

                    fndTaxGroup_TextChanged(Me, New EventArgs())

                    For Each gr As GridViewRowInfo In gvTax.Rows
                        Dim assess As Decimal = 0
                        Dim taxAmt As Decimal = 0
                        For Each gr1 As GridViewRowInfo In gv1.Rows
                            If Not clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) = 0 Then
                                If gr1.Cells(colSchemeItem).Value = "No" And gr1.Cells(colSampleItem).Value = "No" And gr1.Cells(colPromoSchemeItem).Value = "No" Then
                                    assess = assess + clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) * clsCommon.myCdbl(gr1.Cells("assess" + (gr.Index + 1).ToString()).Value)
                                    taxAmt = taxAmt + clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) * clsCommon.myCdbl(gr1.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value)
                                End If
                            End If
                        Next
                        If assess = 0 AndAlso taxAmt = 0 Then
                            gr.Cells("assessibleAmount").Value = assess
                            gr.Cells(colTaxamount).Value = taxAmt
                        End If
                        If assess <> 0 Then
                            gr.Cells("taxRate").Value = Math.Round(taxAmt * 100 / assess, 2)
                            gr.Cells("assessibleAmount").Value = assess
                            gr.Cells(colTaxamount).Value = taxAmt
                        End If
                    Next

                    Dim netAmount As Decimal = 0.0
                    Dim totalTPT As Decimal = 0.0
                    Dim ttlCustDiscount As Decimal = 0
                    For Each gro As GridViewRowInfo In gv1.Rows
                        netAmount = netAmount + clsCommon.myCdbl(gro.Cells(colTotalNetAmount).Value)
                        totalTPT = totalTPT + clsCommon.myCdbl(gro.Cells(colTotalTPT).Value)
                        ttlCustDiscount = ttlCustDiscount + clsCommon.myCdbl(gro.Cells(colTotalCustDiscount).Value)
                    Next
                    txtShipmentTotal.Text = netAmount
                    txtCustDisc.Text = ttlCustDiscount


                    Dim totalTax As Decimal = 0.0
                    For Each gr As GridViewRowInfo In gvTax.Rows
                        totalTax = totalTax + clsCommon.myCdbl(gr.Cells(5).Value)
                    Next
                    txtTotalTaxAmount.Text = totalTax
                    txtShipmentAmt.Text = netAmount + totalTax + totalTPT
                Else
                    For i = gv1.Rows.Count - 1 To 1 Step -1
                        If gv1.Rows(i).Cells(colSampleItem).Value = "Yes" Then
                            gv1.Rows.RemoveAt(i)
                        End If
                    Next
                End If

            Catch ex As Exception
                myMessages.myExceptions(ex)
                txtSchemeSample.txtValue.Text = ""
            End Try
        End If

    End Sub

    Private Sub fndTaxGroup_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SetTaxGroup()
    End Sub

    Private Sub SetTaxGroup()
        sql = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code " & _
           " WHERE G.Tax_Group_Code = '" + fndTaxGroup.txtValue.Text + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " ORDER BY G.Trans_Code"
        ds = clsDBFuncationality.GetDataTable(sql)
        If ds.Rows.Count > 0 Then
            gvTax.DataSource = ds

        Else
            resetFNDTaxGroup()
        End If
        Dim mrp As Decimal = 0.0
        Dim basic As Decimal = 0.0
        Dim netAmount As Decimal = 0.0
        Dim totalAssessibleAmt As Decimal = 0
        For Each grow As GridViewRowInfo In gvTax.Rows
            For Each dataRowInfo As GridViewRowInfo In gv1.Rows

                SnDUtility.calculateTax(clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value) * abatement(dataRowInfo, Nothing) / 100, clsCommon.myCdbl(dataRowInfo.Cells(colitemNetAmount).Value), txtLocation.Value, gv1, gvTax)
                If dataRowInfo.Cells(colSchemeItem).Value = "Yes" Or dataRowInfo.Cells(colSampleItem).Value = "Yes" Or dataRowInfo.Cells(colPromoSchemeItem).Value = "Yes" Then
                    For Each gr As GridViewRowInfo In gvTax.Rows
                        sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + gr.Cells(0).Value + "'"
                        If clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql)) = "N" Then
                            gr.Cells(2).Value = 0
                            gr.Cells(5).Value = 0
                        End If
                    Next
                End If
                dataRowInfo.Cells("Tax" + (grow.Index + 1).ToString() + "Rate").Value = grow.Cells("taxRate").Value
                dataRowInfo.Cells("assess" + (grow.Index + 1).ToString()).Value = grow.Cells("assessibleAmount").Value
                dataRowInfo.Cells("Tax" + (grow.Index + 1).ToString() + "Amt").Value = grow.Cells(colTaxamount).Value

                dataRowInfo.Cells(colTaxamount).Value = Math.Round(SnDUtility.calculateItemTax(clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value) * abatement(dataRowInfo, Nothing) / 100, clsCommon.myCdbl(dataRowInfo.Cells(colitemNetAmount).Value), txtLocation.Value, gv1, gvTax), 4, MidpointRounding.ToEven)
                dataRowInfo.Cells(colTotalTaxAmount).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colShippedQty).Value) * clsCommon.myCdbl(dataRowInfo.Cells(colTaxamount).Value), 0)
                dataRowInfo.Cells(colTotalItemAmount).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colTotalNetAmount).Value) + clsCommon.myCdbl(dataRowInfo.Cells(colTotalTaxAmount).Value) + clsCommon.myCdbl(dataRowInfo.Cells(colTotalTPT).Value), 0)


            Next
        Next
        Dim taxAmt As Decimal = 0
        For Each gr As GridViewRowInfo In gv1.Rows

            netAmount = netAmount + clsCommon.myCdbl(gr.Cells(colTotalNetAmount).Value)
        Next
        For Each gr As GridViewRowInfo In gvTax.Rows
            totalAssessibleAmt = 0
            taxAmt = 0
            For Each gr1 As GridViewRowInfo In gv1.Rows
                If gr1.Cells(colSchemeItem).Value = "No" And gr1.Cells(colSampleItem).Value = "No" And gr1.Cells(colPromoSchemeItem).Value = "No" Then
                    totalAssessibleAmt = totalAssessibleAmt + clsCommon.myCdbl(gr1.Cells("assess" + (gr.Index + 1).ToString()).Value) * clsCommon.myCdbl(gr1.Cells(colShippedQty).Value)
                    taxAmt = taxAmt + clsCommon.myCdbl(gr1.Cells("tax" + (gr.Index + 1).ToString() + "Amt").Value) * clsCommon.myCdbl(gr1.Cells(colShippedQty).Value)
                End If
            Next
            If totalAssessibleAmt = 0 AndAlso taxAmt = 0 Then
                gr.Cells("assessibleAmount").Value = totalAssessibleAmt
                gr.Cells(colTaxamount).Value = taxAmt
            End If
            If totalAssessibleAmt <> 0 Then
                gr.Cells("taxRate").Value = Math.Round(taxAmt * 100 / totalAssessibleAmt, 0)
                gr.Cells("assessibleAmount").Value = totalAssessibleAmt
                gr.Cells(colTaxamount).Value = taxAmt
            End If
        Next
        txtShipmentTotal.Text = netAmount
        Dim totalTax As Decimal = 0.0
        For Each gr As GridViewRowInfo In gvTax.Rows
            totalTax = totalTax + Math.Round(clsCommon.myCdbl(gr.Cells(5).Value), 2)
        Next
        txtTotalTaxAmount.Text = totalTax
        totalAmounts()
    End Sub

    Private Sub SetTaxGroup(ByVal taxgroup As String)
        sql = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code  and R.Tax_Type='S'         " & _
           " WHERE G.Tax_Group_Code = '" + taxgroup + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " ORDER BY G.Trans_Code"
        ds = clsDBFuncationality.GetDataTable(sql)
        If ds.Rows.Count > 0 Then
            gvTax.DataSource = ds

        Else
            resetFNDTaxGroup()
        End If
        Dim mrp As Decimal = 0.0
        Dim basic As Decimal = 0.0
        Dim netAmount As Decimal = 0.0
        Dim totalAssessibleAmt As Decimal = 0
        For Each grow As GridViewRowInfo In gvTax.Rows
            For Each dataRowInfo As GridViewRowInfo In gv1.Rows

                SnDUtility.calculateTax(clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value) * abatement(dataRowInfo, Nothing) / 100, clsCommon.myCdbl(dataRowInfo.Cells(colitemNetAmount).Value), txtLocation.Value, gv1, gvTax)
                If dataRowInfo.Cells(colSchemeItem).Value = "Yes" Or dataRowInfo.Cells(colSampleItem).Value = "Yes" Or dataRowInfo.Cells(colPromoSchemeItem).Value = "Yes" Then
                    For Each gr As GridViewRowInfo In gvTax.Rows
                        sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + gr.Cells(0).Value + "'"
                        If clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql)) = "N" Then
                            gr.Cells(2).Value = 0
                            gr.Cells(5).Value = 0
                        End If
                    Next
                End If
                dataRowInfo.Cells("Tax" + (grow.Index + 1).ToString() + "Rate").Value = grow.Cells("taxRate").Value
                dataRowInfo.Cells("assess" + (grow.Index + 1).ToString()).Value = grow.Cells("assessibleAmount").Value
                dataRowInfo.Cells("Tax" + (grow.Index + 1).ToString() + "Amt").Value = grow.Cells(colTaxamount).Value

                dataRowInfo.Cells(colTaxamount).Value = Math.Round(SnDUtility.calculateItemTax(clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value) * abatement(dataRowInfo, Nothing) / 100, clsCommon.myCdbl(dataRowInfo.Cells(colitemNetAmount).Value), txtLocation.Value, gv1, gvTax), 4, MidpointRounding.ToEven)
                dataRowInfo.Cells(colTotalTaxAmount).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colShippedQty).Value) * clsCommon.myCdbl(dataRowInfo.Cells(colTaxamount).Value), 0)
                dataRowInfo.Cells(colTotalItemAmount).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colTotalNetAmount).Value) + clsCommon.myCdbl(dataRowInfo.Cells(colTotalTaxAmount).Value) + clsCommon.myCdbl(dataRowInfo.Cells(colTotalTPT).Value), 0)


            Next
        Next
        Dim taxAmt As Decimal = 0
        For Each gr As GridViewRowInfo In gv1.Rows

            netAmount = netAmount + clsCommon.myCdbl(gr.Cells(colTotalNetAmount).Value)
        Next
        For Each gr As GridViewRowInfo In gvTax.Rows
            totalAssessibleAmt = 0
            taxAmt = 0
            For Each gr1 As GridViewRowInfo In gv1.Rows
                If gr1.Cells(colSchemeItem).Value = "No" And gr1.Cells(colSampleItem).Value = "No" And gr1.Cells(colPromoSchemeItem).Value = "No" Then
                    totalAssessibleAmt = totalAssessibleAmt + clsCommon.myCdbl(gr1.Cells("assess" + (gr.Index + 1).ToString()).Value) * clsCommon.myCdbl(gr1.Cells(colShippedQty).Value)
                    taxAmt = taxAmt + clsCommon.myCdbl(gr1.Cells("tax" + (gr.Index + 1).ToString() + "Amt").Value) * clsCommon.myCdbl(gr1.Cells(colShippedQty).Value)
                End If
            Next
            If totalAssessibleAmt = 0 AndAlso taxAmt = 0 Then
                gr.Cells("assessibleAmount").Value = totalAssessibleAmt
                gr.Cells(colTaxamount).Value = taxAmt
            End If
            If totalAssessibleAmt <> 0 Then
                gr.Cells("taxRate").Value = Math.Round(taxAmt * 100 / totalAssessibleAmt, 0)
                gr.Cells("assessibleAmount").Value = totalAssessibleAmt
                gr.Cells(colTaxamount).Value = taxAmt
            End If
        Next
        txtShipmentTotal.Text = netAmount
        Dim totalTax As Decimal = 0.0
        For Each gr As GridViewRowInfo In gvTax.Rows
            totalTax = totalTax + Math.Round(clsCommon.myCdbl(gr.Cells(5).Value), 2)
        Next
        txtTotalTaxAmount.Text = totalTax
        totalAmounts()
    End Sub

    Private Sub funfilltransfer(ByVal isFillForUpdate As Boolean, Optional ByVal item As String = "ALL")
        Try
            isInsideLoadData = True

            dtStartTime = DateTime.Now
            strMsg = ""

            If Not isFillForUpdate Then
                gv1.Rows.Clear()
            End If
            sql = "select DISTINCT td.Item_Code, td.Item_Desc ,  td.Item_Qty ,td.Pending_Qty, PM.Uom ,PM.Item_Basic_Price as basic_price  ,pm.Item_Basic_Net AS mrp, td.Price_Date, pm.Empty_Value_Shell , pm.Empty_Value_Bottle,TSPL_ITEM_MASTER.Sku_Seq,(pm.NetLTPT+pm.Price_Amount10) as PriceToShow,case when pm.Start_Date<>td.Price_Date then pm.Start_Date else null end as PriceDateActual,Abatement_Rate from TSPL_TRANSFER_DETAIL td left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=td.Item_Code join TSPL_ITEM_PRICE_MASTER pm on td.Item_Code = pm.Item_Code and td.MRP = pm.Item_Basic_Net  * (SELECT UD.Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code where UD.Item_Code = td.Item_Code  and UD.UOM_Code = pm.UOM  and UM.Create_Price = 'Y' ) and  pm.Start_Date = (select MAX(Start_Date) from TSPL_ITEM_PRICE_MASTER where Price_Code = '" + Convert.ToString(txtPriceCode.Text) + "'  AND Item_Code = td.Item_Code AND  Item_Basic_Net=td.MRP) and td.Transfer_No = '" + txtTransferNo.Value + "' and pm.Price_Code = '" + txtPriceCode.Text + "' and td.Pending_Qty<>0  "
            If item = "ALL" Then
            ElseIf item = "FB" Then
                sql += " AND PM.Uom = 'FB' "
            ElseIf item = "FC" Then
                sql += " AND PM.Uom = 'FC' "
            End If

            Dim qry As String = "select xxx.*,(SELECT top 1 case when PC1.TPT_Type='Y' then ISNULL(Price_Amount1,0) else 0 end"
            qry += " + case when PC2.TPT_Type='Y' then ISNULL(Price_Amount2,0) else 0 end "
            qry += " + case when PC3.TPT_Type='Y' then ISNULL(Price_Amount3,0) else 0 end "
            qry += " + case when PC4.TPT_Type='Y' then ISNULL(Price_Amount4,0) else 0 end "
            qry += " + case when PC5.TPT_Type='Y' then ISNULL(Price_Amount5,0) else 0 end "
            qry += " + case when PC6.TPT_Type='Y' then ISNULL(Price_Amount6,0) else 0 end "
            qry += " + case when PC7.TPT_Type='Y' then ISNULL(Price_Amount7,0) else 0 end "
            qry += " + case when PC8.TPT_Type='Y' then ISNULL(Price_Amount8,0) else 0 end "
            qry += " + case when PC9.TPT_Type='Y' then ISNULL(Price_Amount9,0) else 0 end "
            qry += " + case when PC10.TPT_Type='Y' then ISNULL(Price_Amount10,0) else 0 end "
            qry += " FROM TSPL_ITEM_PRICE_MASTER "
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC1 on PC1.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp1"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC2 on PC2.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp2"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC3 on PC3.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp3"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC4 on PC4.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp4"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC5 on PC5.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp5"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC6 on PC6.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp6"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC7 on PC7.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp7"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC8 on PC8.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp8"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC9 on PC9.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp9"
            qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC10 on PC10.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp10"
            qry += " Where Price_Code='" + txtPriceCode.Text + "'  and Item_Code = xxx.Item_Code AND Item_Basic_Net =xxx.mrp AND Item_Basic_Price =xxx.basic_price ) as TPT "
            qry += " from ( " + sql + " )xxx order by xxx.Sku_Seq"


            Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(qry)

            dtEndTime = DateTime.Now
            span = dtEndTime.Subtract(dtStartTime)
            strMsg += "query Execute:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
            dtStartTime = DateTime.Now

            Dim i As Integer = 0
            If dt5 IsNot Nothing AndAlso dt5.Rows.Count > 0 Then
                If Not isFillForUpdate Then
                    LoadBlankGrid()
                    fndTaxGroup.txtValue.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sales_Tax_Group  from TSPL_LOCATION_MASTER where Location_Code = '" + txtLocation.Value + "'"))
                    lblTaxDesc.Text = clsTaxGroupMaster.GetNameOfSaleType(fndTaxGroup.txtValue.Text, Nothing)
                End If
                For Each datarow5 As DataRow In dt5.Rows
                    If isFillForUpdate Then
                        Dim strICode As String = clsCommon.myCstr(datarow5("Item_Code"))
                        Dim dblMRP As Double = clsCommon.myCdbl(datarow5("mrp"))
                        Dim strUOM As String = clsCommon.myCstr(datarow5("uom"))
                        Dim isFound As Boolean = False
                        For ii As Integer = 0 To gv1.RowCount - 1
                            If clsCommon.CompairString(strUOM, clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitCode).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strICode, clsCommon.myCstr(gv1.Rows(ii).Cells(ColICode).Value)) = CompairStringResult.Equal AndAlso clsCommon.myCdbl(clsCommon.myCstr(gv1.Rows(ii).Cells(colMRP).Value)) = dblMRP Then
                                isFound = True
                                Exit For
                            End If
                        Next
                        If isFound Then
                            Continue For
                        End If
                    End If
                    Dim dataRowInfo As GridViewRowInfo = gv1.Rows.AddNew()
                    dataRowInfo.Cells(ColComplete).Value = "No"
                    dataRowInfo.Cells(ColICode).Value = datarow5("Item_Code").ToString()
                    dataRowInfo.Cells(ColItemName).Value = datarow5("Item_Desc").ToString()
                    dataRowInfo.Cells(colPriceDateColumn).Value = CDate(datarow5("Price_Date").ToString()).ToString("dd/MM/yyyy")
                    dataRowInfo.Cells(ColPriceToShow).Value = clsCommon.myCstr(datarow5("PriceToShow"))

                    Dim startdate As String = Convert.ToString(datarow5("Price_Date"))
                    If datarow5("PriceDateActual") IsNot DBNull.Value Then
                        startdate = Convert.ToString(datarow5("PriceDateActual"))
                        dataRowInfo.Cells(colPriceDateActual).Value = startdate
                    End If
                    dataRowInfo.Cells(colAbatementRate).Value = clsCommon.myCstr(datarow5("Abatement_Rate"))
                    startdate = Format$(startdate, "yyyy-MM-dd")

                    dataRowInfo.Cells(colTPT).Value = clsCommon.myCstr(datarow5("TPT"))
                    dataRowInfo.Cells(colTotalTPT).Value = "0"
                    dataRowInfo.Cells(colUnitCode).Value = datarow5("uom").ToString()
                    dataRowInfo.Cells(collocation).Value = txtLocation.Value
                    dataRowInfo.Cells(colPriceCode).Value = txtPriceCode.Text
                    dataRowInfo.Cells(colSchemeApplicable).Value = "No"
                    dataRowInfo.Cells(colPromoSchemeApplicable).Value = "No"
                    dataRowInfo.Cells(colPromoSchemeItem).Value = "No"
                    dataRowInfo.Cells(colPromoSchemeCode).Value = ""
                    dataRowInfo.Cells(colSchemeCodeItem).Value = ""
                    dataRowInfo.Cells(colSchemeItem).Value = "No"
                    dataRowInfo.Cells(colSchemeDiscountApplicable).Value = "No"
                    dataRowInfo.Cells(colSchemeCodeDiscount).Value = ""
                    dataRowInfo.Cells(colSampleItem).Value = "No"
                    dataRowInfo.Cells(colEmptyValue).Value = Convert.ToDecimal(datarow5("Empty_Value_Shell").ToString()) + Convert.ToDecimal(datarow5("Empty_Value_Bottle").ToString())
                    dataRowInfo.Cells(colEmptyValueShell).Value = datarow5("Empty_Value_Shell").ToString()
                    dataRowInfo.Cells(colEmptyValueBottle).Value = datarow5("Empty_Value_Bottle").ToString()
                    dataRowInfo.Cells(colMRP).Value = clsCommon.myCdbl(datarow5("MRP"))
                    dataRowInfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(dataRowInfo.Cells(ColICode).Value), clsCommon.myCstr(dataRowInfo.Cells(colUnitCode).Value), clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value))

                    dataRowInfo.Cells(colTransferBasicAmount).Value = datarow5("Basic_Price").ToString()
                    Dim assessibleamt As Decimal = Math.Round(clsCommon.myCdbl(datarow5("MRP").ToString()) * abatement(dataRowInfo, Nothing) / 100, 2)
                    dataRowInfo.Cells(colTaxamount).Value = 0.0
                    dataRowInfo.Cells(colDiscountAmount).Value = "0"
                    dataRowInfo.Cells(colitemNetAmount).Value = dataRowInfo.Cells(colBasicAmount).Value
                    dataRowInfo.Cells(colTotalMRP).Value = "0"
                    dataRowInfo.Cells(colTotalBasicAmount).Value = "0"
                    dataRowInfo.Cells(colTotalDiscountAmount).Value = "0"
                    dataRowInfo.Cells(colTotalNetAmount).Value = "0"
                    dataRowInfo.Cells(colTotalTaxAmount).Value = "0"
                    dataRowInfo.Cells(colOrderedQty).Value = 0
                    dataRowInfo.Cells(colFromSchemeCode).Value = ""
                    Dim dblConvFac As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(datarow5("Item_Code")), clsCommon.myCstr(datarow5("uom")), Nothing)
                    If dblConvFac = 1 Then
                        dataRowInfo.Cells(colBalanceQty).Value = Convert.ToString(datarow5("Pending_Qty"))
                        dataRowInfo.Cells(colOrderedQty).Value = Convert.ToString(datarow5("Item_Qty"))
                    Else
                        dataRowInfo.Cells(colBalanceQty).Value = "0.00"
                        dataRowInfo.Cells(colOrderedQty).Value = "0.00"
                    End If
                    dataRowInfo.Cells(colBasicAmount).Value = Convert.ToString(datarow5("Basic_price"))
                    Dim pricedate As Date = CDate(datarow5("Price_Date"))
                    startdate = pricedate.ToString("yyyy-MM-dd")
                    Dim taxgroup As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sales_Tax_Group  from TSPL_LOCATION_MASTER where Location_Code = '" + txtLocation.Value + "'"))

                    Dim taxratequery As String = "select TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + Convert.ToString(dataRowInfo.Cells(ColICode).Value) + "' and Start_Date = '" + startdate + "' and Item_Basic_Net = '" + Convert.ToString(clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value) * clsCommon.myCdbl(dblConvFac)) + "'  and Tax_group = '" + taxgroup + "'"
                    Dim dtTaxRate As DataTable = clsDBFuncationality.GetDataTable(taxratequery)
                    If dtTaxRate IsNot Nothing AndAlso dtTaxRate.Rows.Count > 0 Then
                        For counttax As Integer = 1 To 6
                            Dim taxrate As String = "Tax" + counttax.ToString() + "_Rate"
                            Dim taxr As String = "tax" + counttax.ToString() + "rate"
                            dataRowInfo.Cells(taxr).Value = clsCommon.myCdbl(dtTaxRate.Rows(0)(taxrate))
                        Next
                    End If
                    gv1.Refresh()
                Next

                dtEndTime = DateTime.Now
                span = dtEndTime.Subtract(dtStartTime)
                strMsg += "Filling Item :" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
                dtStartTime = DateTime.Now

                gv1.AllowAddNewRow = True
                LoadPendingBalanceAgainstTransfer()

                dtEndTime = DateTime.Now
                span = dtEndTime.Subtract(dtStartTime)
                strMsg += "Loading Pending:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
                dtStartTime = DateTime.Now
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

  

    Private Sub addCashDiscountScheme(ByVal grow As GridViewRowInfo)

        If grow.Cells(colSchemeDiscountApplicable).Value = "No" Then
            grow.Cells(colitemNetAmount).Value = clsCommon.myCdbl(grow.Cells(colBasicAmount).Value) - (clsCommon.myCdbl(grow.Cells(ColCustDisNoTax).Value))
            grow.Cells(colTotalNetAmount).Value = 0
            grow.Cells(colDiscountAmount).Value = "0"
            grow.Cells(colTotalDiscountAmount).Value = "0"
            grow.Cells(colSchemeCodeDiscount).Value = String.Empty
        ElseIf grow.Cells(colSchemeDiscountApplicable).Value = "Yes" Then
            sql = "SELECT Scheme_Code,Amount,Main_Item_Qty FROM TSPL_SCHEME_MASTER " & _
                  " WHERE  (Scheme_Type = 'C') AND (Main_Item_Code = '" + grow.Cells(ColICode).Value + "') AND (Start_Date <='" + Format(clsCommon.GETSERVERDATE(), "MM/dd/yyyy") + "') AND (End_Date >='" + Format(clsCommon.GETSERVERDATE(), "MM/dd/yyyy") + "' OR End_Date is NULL ) AND (Main_Item_Qty <= '" + clsCommon.myCstr(grow.Cells(colShippedQty).Value) + "') AND (Main_Item_Uom = '" + grow.Cells(colUnitCode).Value + "' AND (MRP = '" + clsCommon.myCstr(grow.Cells(colMRP).Value) + "') AND (Item_Basic_Price = '" + grow.Cells(colTransferBasicAmount).Value + "') AND Cust_Cate =  (SELECT Cust_Category_Code  FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code = '" + txtCustomer.Value + "'))"
            Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(sql)
            If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then

                Dim mode As Decimal = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) Mod clsCommon.myCdbl(dr1.Rows(0)(2).ToString())
                Dim disRatio As Integer = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - mode) / clsCommon.myCdbl(dr1.Rows(0)(2).ToString())
                grow.Cells(colSchemeCodeDiscount).Value = dr1.Rows(0)(0).ToString
                If clsCommon.myCdbl(dr1.Rows(0)(1).ToString()) < 0 Then
                    grow.Cells(colDiscountAmount).Value = (clsCommon.myCdbl(grow.Cells(colBasicAmount).Value) * Math.Abs(clsCommon.myCdbl(dr1.Rows(0)(1).ToString())) / 100)
                Else
                    grow.Cells(colDiscountAmount).Value = clsCommon.myCdbl(dr1.Rows(0)(1).ToString())
                End If
                grow.Cells(colTotalDiscountAmount).Value = disRatio * clsCommon.myCdbl(grow.Cells(colDiscountAmount).Value)
                grow.Cells(colitemNetAmount).Value = clsCommon.myCdbl(grow.Cells(colBasicAmount).Value) - clsCommon.myCdbl(grow.Cells(colDiscountAmount).Value) - clsCommon.myCdbl(grow.Cells(ColCustDisNoTax).Value)

            Else
                common.clsCommon.MyMessageBoxShow("No scheme applicable.")
                grow.Cells(colSchemeDiscountApplicable).Value = "No"
            End If
        End If
        ' AddHandler gvLoadOut.CellValueChanged, AddressOf gvLoadOut1_CellValueChanged
        loadoutCellValueChange(grow)
    End Sub

    Private Sub loadoutCellValueChange(ByVal grow As GridViewRowInfo)
        Dim IntRoundOff As Integer = 4
        sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + grow.Cells(ColICode).Value + "' AND " & _
                    " Uom_Code='" + grow.Cells(colUnitCode).Value + "'"
        Dim convertFact As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
        If Not convertFact = 0 Then
            Dim shells As Integer
            Dim bottles As Decimal = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) Mod convertFact
            If bottles = 0 Then
                shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact
                grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
            Else
                shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact + 1
                grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells - 1) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
            End If
            If bottles = 0 And shells = 0 Then
                grow.Cells(colEmptyValue).Value = clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value)
            End If
        End If
        If convertFact = 1 Then
            IntRoundOff = 2
        End If
        grow.Cells(colTotalCustDiscount).Value = Math.Round((clsCommon.myCdbl(grow.Cells(ColCustDisNoTax).Value) * clsCommon.myCdbl(grow.Cells(colShippedQty).Value) / convertFact), 2)
        grow.Cells(colTaxamount).Value = SnDUtility.calculateItemTax(itemAssessibleAmt(grow, Nothing), clsCommon.myCdbl(grow.Cells(colitemNetAmount).Value), txtLocation.Value, gv1, gvTax, clsCommon.myCdbl(txtDiscPer.Text))
        grow.Cells(colTaxamount).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colTax1Amt).Value) + clsCommon.myCdbl(grow.Cells(colTax2Amt).Value) + clsCommon.myCdbl(grow.Cells(colTax3Amt).Value) + clsCommon.myCdbl(grow.Cells(colTax4Amt).Value) + clsCommon.myCdbl(grow.Cells(colTax5Amt).Value) + clsCommon.myCdbl(grow.Cells(colTax6Amt).Value), 4, MidpointRounding.ToEven)
        grow.Cells(colTotalMRP).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colMRP).Value) * clsCommon.myCdbl(grow.Cells(colShippedQty).Value), 2)
         
        If ((clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal)) Then
            grow.Cells(colTotalBasicAmount).Value = 0

        Else
            grow.Cells(colTotalBasicAmount).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colBasicAmount).Value) * clsCommon.myCdbl(grow.Cells(colShippedQty).Value), 2)

        End If
        grow.Cells(colTotalTaxAmount).Value = clsCommon.myCdbl(grow.Cells(colTaxamount).Value) * clsCommon.myCdbl(grow.Cells(colShippedQty).Value)
        grow.Cells(colTotalNetAmount).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colitemNetAmount).Value) * clsCommon.myCdbl(grow.Cells(colShippedQty).Value), 2)
        If ((clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal)) Then
            grow.Cells(colTotalTPT).Value = 0
        Else
            If convertFact = 1 Then
                grow.Cells(colTotalTPT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colTPT).Value) * clsCommon.myCdbl(grow.Cells(colShippedQty).Value), 2)
            Else
                Dim fullbottlcase As Decimal = Math.Ceiling(clsCommon.myCdbl(grow.Cells(colShippedQty).Value) / convertFact)

                Dim tpt As String = Nothing
                Dim tptcheck As String = "N"
                Dim tptdr As DataTable = clsDBFuncationality.GetDataTable("SELECT  Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10   FROM TSPL_ITEM_PRICE_MASTER Where Price_Code='" + txtPriceCode.Text + "' and Item_Code = '" + Convert.ToString(grow.Cells(ColICode).Value) + "' AND Item_Basic_Net ='" + Convert.ToString(grow.Cells(colMRP).Value * convertFact) + "' AND Tax_group = '" + fndTaxGroup.txtValue.Text + "' and UOM = 'FC'")
                If tptdr IsNot Nothing AndAlso tptdr.Rows.Count > 0 Then
                    For j As Integer = 1 To 10
                        Dim Price_Amount As String = "Price_Amount" + j.ToString()
                        Dim Price_Comp As String = "Price_Comp" + j.ToString()
                        tptcheck = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code = '" + Convert.ToString(tptdr.Rows(0)(Price_Comp)) + "'"))
                        If tptcheck = "Y" Then
                            tpt = Convert.ToString(tptdr.Rows(0)(Price_Amount))
                            Exit For
                        End If
                    Next

                End If
                Dim tptval As Decimal = Math.Round(clsCommon.myCdbl(grow.Cells(colTPT).Value) * convertFact, 0)
                grow.Cells(colTotalTPT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colShippedQty).Value) / convertFact * clsCommon.myCdbl(tpt), 2)
            End If
        End If
        grow.Cells(colTotalItemAmount).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colTotalNetAmount).Value) + clsCommon.myCdbl(grow.Cells(colTotalTaxAmount).Value) + clsCommon.myCdbl(grow.Cells(colTotalTPT).Value), 2)
        Dim netAmount As Decimal = totalNetAmount()
        txtShipmentTotal.Text = clsCommon.myFormat(totalBasicAmt())
        SnDUtility.calculateTax(totalAssessibleAmt(), netAmount, txtLocation.Value, gv1, gvTax)
        Dim totalTax As Decimal = 0.0
        For Each gr As GridViewRowInfo In gvTax.Rows
            totalTax = totalTax + clsCommon.myCdbl(gr.Cells(5).Value)
        Next
        Dim ttlCustDiscount As Decimal = 0
        For Each gro As GridViewRowInfo In gv1.Rows
            ttlCustDiscount = ttlCustDiscount + clsCommon.myCdbl(gro.Cells(colTotalCustDiscount).Value)
        Next
        txtCustDisc.Text = ttlCustDiscount
        txtTotalTaxAmount.Text = totalTax
        txtShipmentAmt.Text = netAmount + totalTax + totalTransport()
    End Sub

    Private Sub foctransferitem(ByVal itemcode As String, ByVal startdate As Date, ByVal strUOM As String, ByVal dblMRP As Double)
        Dim pricedt As String
        gv1.CurrentRow.Cells(ColComplete).Value = "Yes"
        gv1.CurrentRow.Cells(ColICode).Value = itemcode
        pricedt = startdate.ToString("yyyy-MM-dd")
        gv1.CurrentRow.Cells(colPriceDateColumn).ReadOnly = True
        gv1.CurrentRow.Cells(ColItemName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Item_Desc   from TSPL_TRANSFER_DETAIL where Transfer_No = '" + txtTransferNo.Value + "' and Item_Code = '" + gv1.CurrentRow.Cells(ColICode).Value.ToString() + "'"))
        gv1.CurrentRow.Cells(colPriceCode).Value = txtPriceCode.Text
        gv1.CurrentRow.Cells("orderedQty").ReadOnly = True
        gv1.CurrentRow.Cells(colOrderedQty).Value = 0
        gv1.CurrentRow.Cells(colShippedQty).Value = 1
        gv1.CurrentRow.Cells("balanceQty").ReadOnly = True
        gv1.CurrentRow.Cells(colBalanceQty).Value = 0
        gv1.CurrentRow.Cells(colUnitCode).Value = strUOM
        gv1.CurrentRow.Cells(colPriceDateColumn).Value = startdate.ToString("dd/MM/yyyy")
        'Dim qry As String = "select Start_Date from TSPL_ITEM_PRICE_MASTER where Item_Code='" + itemcode + "' and Start_Date='" + pricedt + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + clsCommon.myCstr(dblMRP) + "' and  Price_Code='" + txtPriceCode.Text + "' "
        'If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
        '    qry = "select max(Start_Date) from TSPL_ITEM_PRICE_MASTER where Item_Code='" + itemcode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + clsCommon.myCstr(dblMRP) + "' and  Price_Code='" + txtPriceCode.Text + "' and Start_Date<='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' "
        '    If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))) <= 0 Then
        '        common.clsCommon.MyMessageBoxShow("Error " + Environment.NewLine + "Price Date does not exist for Item_Code='" + itemcode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + clsCommon.myCstr(dblMRP) + "' and  Price_Code='" + txtPriceCode.Text + "'", Me.Text)
        '    Else
        '        pricedt = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry)), "yyyy/MM/dd")
        '        gv1.CurrentRow.Cells(colPriceDateActual).Value = pricedt
        '    End If
        'End If
        Dim qry As String = "select max(Start_Date) from TSPL_ITEM_PRICE_MASTER where Item_Code='" + itemcode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + clsCommon.myCstr(dblMRP) + "' and  Price_Code='" + txtPriceCode.Text + "' and Start_Date<='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' "
        pricedt = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry)), "yyyy/MM/dd")
        If clsCommon.myLen(pricedt) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Error " + Environment.NewLine + "Price Date does not exist for Item_Code='" + itemcode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + clsCommon.myCstr(dblMRP) + "' and  Price_Code='" + txtPriceCode.Text + "'", Me.Text)
        End If
        gv1.CurrentRow.Cells("unitCode").ReadOnly = True
        gv1.CurrentRow.Cells(colSchemeItem).Value = "Yes"
        gv1.CurrentRow.Cells(colPromoSchemeCode).Value = ""
        gv1.CurrentRow.Cells(colSchemeCodeItem).Value = ""
        gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
        gv1.CurrentRow.Cells(colPromoSchemeApplicable).Value = "No"
        gv1.CurrentRow.Cells(colSchemeDiscountApplicable).Value = "No"
        gv1.CurrentRow.Cells(colSchemeCodeDiscount).Value = ""
        gv1.CurrentRow.Cells(colSampleItem).Value = "No"
        gv1.CurrentRow.Cells(colTPT).Value = "0.00"
        Dim count As String = "MS1"
        For Each grow As GridViewRowInfo In gv1.Rows
            If grow.Cells(colShippedQty).Value > 0 Then
                Dim strTemp As String = clsCommon.myCstr(grow.Cells(colFromSchemeCode).Value)
                If strTemp.Contains("MS") Then
                    count = clsCommon.incval(strTemp)
                End If
            End If
        Next
        gv1.CurrentRow.Cells(colMRP).Value = dblMRP
        gv1.CurrentRow.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitCode).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value))
        qry = "SELECT Item_Basic_Price,Empty_Value_Bottle, Empty_Value_Shell,Empty_Value_Bottle,(TSPL_ITEM_PRICE_MASTER.NetLTPT+TSPL_ITEM_PRICE_MASTER.Price_Amount10) as PriceToShow,TSPL_ITEM_PRICE_MASTER.Abatement_Rate FROM TSPL_ITEM_PRICE_MASTER WHERE Item_Code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value) + "' AND UOM = '" + strUOM + "' AND Start_Date = '" + pricedt + "' AND Price_Code = '" + txtPriceCode.Text + "' and item_basic_net = '" + Convert.ToString(gv1.CurrentRow.Cells(colMRP).Value) + "' "
        Dim dtPM As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtPM Is Nothing OrElse dtPM.Rows.Count <= 0 Then
            Throw New Exception("Price not found for item = " + clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value) + " AND UOM = " + strUOM + " AND Start_Date = " + pricedt + " AND Price Code =  " + txtPriceCode.Text + "  and MRP =  " + clsCommon.myCstr(gv1.CurrentRow.Cells(colMRP).Value))
        End If
        gv1.CurrentRow.Cells(colAbatementRate).Value = clsCommon.myCdbl(dtPM.Rows(0)("Abatement_Rate"))
        gv1.CurrentRow.Cells(colBasicAmount).Value = clsCommon.myCdbl(dtPM.Rows(0)("Item_Basic_Price"))
        gv1.CurrentRow.Cells(colEmptyValue).Value = clsCommon.myCdbl(dtPM.Rows(0)("Empty_Value_Bottle"))
        gv1.CurrentRow.Cells(colEmptyValueBottle).Value = clsCommon.myCdbl(dtPM.Rows(0)("Empty_Value_Bottle"))
        gv1.CurrentRow.Cells(colEmptyValueShell).Value = clsCommon.myCdbl(dtPM.Rows(0)("Empty_Value_Shell"))
        gv1.CurrentRow.Cells(ColPriceToShow).Value = clsCommon.myCdbl(dtPM.Rows(0)("PriceToShow"))

        gv1.CurrentRow.Cells(colTransferBasicAmount).Value = "0.00"
        gv1.CurrentRow.Cells(colDiscountAmount).Value = "0.00"
        gv1.CurrentRow.Cells(colitemNetAmount).Value = "0.00"
        gv1.CurrentRow.Cells(colcustDiscount).Value = "0.00"
        gv1.CurrentRow.Cells(colTotalCustDiscount).Value = "0.00"
        gv1.CurrentRow.Cells(collocation).Value = txtLocation.Value
        gv1.CurrentRow.Cells(colcustDiscount).Value = "0.00"
        gv1.CurrentRow.Cells(colitemNetAmount).Value = "0.00"
        gv1.CurrentRow.Cells(colTaxamount).Value = "0.00"
        gv1.CurrentRow.Cells(colTotalNetAmount).Value = "0.00"
        gv1.CurrentRow.Cells(colTPT).Value = "0.00"
        gv1.CurrentRow.Cells(colTotalMRP).Value = "0.00"
        gv1.CurrentRow.Cells(colTotalBasicAmount).Value = "0.00"
        gv1.CurrentRow.Cells(colTotalNetAmount).Value = "0.00"
        gv1.CurrentRow.Cells(colTotalTaxAmount).Value = "0.00"
        gv1.CurrentRow.Cells(colTotalTPT).Value = "0.00"
        gv1.CurrentRow.Cells(colTotalDiscountAmount).Value = "0.00"
        gv1.CurrentRow.Cells(colTotalItemAmount).Value = "0.00"
        gv1.CurrentRow.Cells(colPromoSchemeItem).Value = "No"
        gv1.CurrentRow.Cells(colSampleItem).Value = "No"
        gv1.CurrentRow.Cells(colSchemeItem).Value = "Yes"
        gv1.CurrentRow.Cells(colFromSchemeCode).Value = count
    End Sub

    Private Sub gvLoadOut1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(ColICode) OrElse e.Column Is gv1.Columns(colSchemeApplicable) OrElse e.Column Is gv1.Columns(collocation) OrElse e.Column Is gv1.Columns(colSchemeDiscountApplicable) OrElse e.Column Is gv1.Columns(colitemNetAmount) OrElse e.Column Is gv1.Columns(colShippedQty) OrElse e.Column Is gv1.Columns(colMainItem) OrElse e.Column Is gv1.Columns(colDiscountCode) OrElse e.Column Is gv1.Columns(colTotalTPT) Then
                        If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                             If e.Column Is gv1.Columns(ColICode) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(ColICode).Value) > 0 Then
                                Dim qry As String = "select TSPL_TRANSFER_DETAIL.Item_Code as [Item Code], TSPL_TRANSFER_DETAIL.item_Desc as [Item Description] , TSPL_ITEM_UOM_DETAIL.UOM_Code  as Uom ,CONVERT(decimal(18,2), ROUND( TSPL_TRANSFER_DETAIL.MRP/TSPL_ITEM_UOM_DETAIL.Conversion_Factor,2)) as MRP, convert(varchar(10),TSPL_TRANSFER_DETAIL.price_date,103) as [price date]  "
                                qry += " from TSPL_TRANSFER_DETAIL"
                                qry += " inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code"
                                qry += " where TSPL_TRANSFER_DETAIL.Transfer_No ='" + txtTransferNo.Value + "' and TSPL_ITEM_UOM_DETAIL.UOM_Code in ('FB','FC')"
                                Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ShipmentItemSelect", qry, "Item Code", clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value))
                                If dr IsNot Nothing Then
                                    Dim itemcode As String = Convert.ToString(dr("Item Code"))
                                    Dim startdate As Date = CDate(dr("price date")).ToString("dd/MM/yyyy")
                                    Dim strUOM As String = clsCommon.myCstr(dr("Uom"))
                                    foctransferitem(itemcode, startdate, strUOM, clsCommon.myCdbl(dr("MRP")))
                                    isCellValueChangedOpen = False
                                    Exit Sub
                                Else
                                    gv1.CurrentRow.Cells(ColICode).Value = String.Empty
                                    isCellValueChangedOpen = False
                                    Exit Sub
                                End If
                            End If
                        Else
                             If e.Column Is gv1.Columns(ColICode) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(ColICode).Value) > 0 Then
                                Dim baseQry As String = GetShipmentViewQty()
                                Dim qry As String = "SELECT Item_Code,Item_Desc,Start_Date,Item_Basic_Net as [MRP], UOM FROM (" + baseQry + ")xxx  Where Show='N' AND UOM in ('FB','FC') AND  Price_Code='" + txtPriceCode.Text + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.txtValue.Text + "' "
                                If clsCommon.myLen(txtOrderNo.Value) > 0 Then
                                    'qry += " AND Item_Code IN ( select Item_Code from TSPL_SALES_ORDER_DETAILS where TSPL_SALES_ORDER_DETAILS.Order_No='" + txtOrderNo.Value + "')"
                                Else
                                    qry += " AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0 and MRP=xxx.Item_Basic_Net*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=xxx.Item_Code and UOM_Code=xxx.UOM))"
                                End If

                                Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ShipmentItemSelect", qry, "Item_Code", clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value))
                                If dr IsNot Nothing Then
                                    Dim itemcode As String = Convert.ToString(dr("Item_Code"))
                                    Dim startdate As String = CDate(dr("start_date")).ToString("dd/MM/yyyy")
                                    Dim mrp As Decimal = clsCommon.myCdbl(dr("MRP"))
                                    Dim strUOM As String = clsCommon.myCstr(dr("UOM"))
                                    currentmanualscheme(itemcode, startdate, mrp, strUOM)
                                    isCellValueChangedOpen = False
                                    Exit Sub
                                Else
                                    gv1.CurrentRow.Cells(ColICode).Value = String.Empty
                                    isCellValueChangedOpen = False
                                    Exit Sub
                                End If
                            End If
                        End If
                        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
                        If grow.Cells(colSchemeItem).Value = "No" And grow.Cells(colSampleItem).Value = "No" And grow.Cells(colPromoSchemeItem).Value = "No" Then
                            grow.Cells("shippedQty").ReadOnly = False
                            If e.Column Is gv1.Columns(collocation) Then
                                If clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal AndAlso txtOrderNo.Value.Trim() = "" Then
                                    sql = "SELECT LD.Item_Qty * UD.Conversion_Factor AS Expr1 FROM TSPL_ITEM_LOCATION_DETAILS AS LD INNER JOIN " & _
                                          " TSPL_ITEM_UOM_DETAIL AS UD ON LD.Item_Code = UD.Item_Code WHERE LD.Item_Code='" + grow.Cells(ColICode).Value + "' AND " & _
                                          " LD.Location_Code='" + grow.Cells(collocation).Value + "' and LD.MRP='" + clsCommon.myCstr(grow.Cells(colMRP).Value) + "' AND UD.UOM_Code='" + grow.Cells(colUnitCode).Value + "'"
                                    grow.Cells(colBalanceQty).Value = Math.Round(Convert.ToDecimal(clsDBFuncationality.getSingleValue(sql)), 2)
                                End If
                                If Not checkItemonLocation(grow.Cells(ColICode).Value, grow.Cells(colShippedQty).Value, grow.Cells(collocation).Value, grow.Cells(colUnitCode).Value, clsCommon.myCdbl(grow.Cells(colMRP).Value), False, "") Then
                                    grow.Cells(colShippedQty).Value = ttlItemShpQtyForCheck
                                End If
                            ElseIf e.Column Is gv1.Columns(colSchemeApplicable) Then
                                findQtyandPromoSchemeCode(grow, "Q")

                            ElseIf e.Column Is gv1.Columns(collocation) Then
                                findQtyandPromoSchemeCode(grow, "P")

                            ElseIf e.Column Is gv1.Columns(colSchemeDiscountApplicable) Then
                                addCashDiscountScheme(grow)
                            ElseIf e.Column Is gv1.Columns(colitemNetAmount) Then
                                calculateItemTaxAgainstItemRate(grow)
                                CalculateTaxByLocation(grow)
                            ElseIf e.Column Is gv1.Columns(colShippedQty) Then
                                If clsCommon.myLen(txtCustomer.Value) <= 0 Then
                                    common.clsCommon.MyMessageBoxShow("Please select customer.")
                                    grow.Cells(colShippedQty).Value = 0
                                    txtCustomer.Focus()
                                    isCellValueChangedOpen = False
                                    Exit Sub
                                Else
                                    For Each gro As GridViewRowInfo In gv1.Rows
                                        If Not gro.Index = grow.Index Then
                                            If gro.Cells(ColICode).Value = grow.Cells(ColICode).Value AndAlso clsCommon.myCdbl(gro.Cells(colShippedQty).Value) > 0 AndAlso clsCommon.myCdbl(grow.Cells(colShippedQty).Value) > 0 AndAlso grow.Cells(colUnitCode).Value = gro.Cells(colUnitCode).Value AndAlso clsCommon.myCdbl(gro.Cells(colMRP).Value) <> clsCommon.myCdbl(grow.Cells(colMRP).Value) Then
                                                If gro.Cells(colSchemeItem).Value = "No" AndAlso gro.Cells(colPromoSchemeItem).Value = "No" AndAlso gro.Cells(colSampleItem).Value = "No" Then
                                                    common.clsCommon.MyMessageBoxShow("Same item for different MRP can not be shipped.")
                                                    grow.Cells(colShippedQty).Value = 0
                                                    isCellValueChangedOpen = False
                                                    Exit For
                                                End If
                                            End If
                                        End If
                                    Next
                                    addCashDiscountScheme(grow)
                                    findQtyandPromoSchemeCode(grow, "P")
                                    findQtyandPromoSchemeCode(grow, "Q")
                                    If Not checkItemonLocation(grow.Cells(ColICode).Value, grow.Cells(colShippedQty).Value, grow.Cells(collocation).Value, grow.Cells(colUnitCode).Value, clsCommon.myCdbl(grow.Cells(colMRP).Value), False, grow.Cells(colBatchNumber).Value) Then
                                        grow.Cells(colShippedQty).Value = 0
                                    End If
                                    If grow.Cells(colBalanceQty).Value = 0 Then
                                        grow.Cells(ColComplete).Value = "Yes"
                                    Else
                                        grow.Cells(ColComplete).Value = "No"
                                    End If
                                    loadoutCellValueChange(grow)
                                    calculateItemTaxAgainstItemRate(grow)
                                    CalculateTaxByLocation(grow)
                                End If
                            End If
                        Else
                            grow.Cells(colShippedQty).ReadOnly = False
                            If gv1.CurrentRow.Cells(colFromSchemeCode).Value = "ManualScheme" Then
                                If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gv1.CurrentRow.Cells(ColICode).Value + "' AND " & _
                                         " Uom_Code='" + gv1.CurrentRow.Cells(colUnitCode).Value + "'"
                                    Dim convertFact As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                                    If Not convertFact = 0 Then
                                        Dim shells As Integer
                                        Dim bottles As Decimal = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) Mod convertFact
                                        If bottles = 0 Then
                                            shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact
                                            grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
                                        Else
                                            shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact + 1
                                            grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells - 1) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
                                        End If
                                        If bottles = 0 And shells = 0 Then
                                            grow.Cells(colEmptyValue).Value = clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value)
                                        End If
                                        grow.Cells(19).Value = grow.Cells(colShippedQty).Value * grow.Cells(colMRP).Value
                                    End If
                                End If
                            End If
                        End If
                        Dim dblBasicAmt As Double = 0.0
                        Dim netAmount As Decimal = 0.0
                        Dim totalTPT As Decimal = 0.0
                        Dim ttlCustDiscount As Decimal = 0
                        For Each gro As GridViewRowInfo In gv1.Rows
                            dblBasicAmt = dblBasicAmt + clsCommon.myCdbl(gro.Cells(colTotalBasicAmount).Value)
                            netAmount = netAmount + clsCommon.myCdbl(gro.Cells(colTotalNetAmount).Value)
                            totalTPT = totalTPT + clsCommon.myCdbl(gro.Cells(colTotalTPT).Value)
                            ttlCustDiscount = ttlCustDiscount + clsCommon.myCdbl(gro.Cells(colTotalCustDiscount).Value)
                        Next
                        txtShipmentTotal.Text = clsCommon.myFormat(dblBasicAmt)
                        txtNetShipAmt.Text = clsCommon.myFormat(netAmount)
                        txtCustDisc.Text = ttlCustDiscount
                        For Each gr As GridViewRowInfo In gvTax.Rows
                            Dim assess As Decimal = 0
                            Dim taxAmt As Decimal = 0
                            Dim basicprice As Decimal = 0
                            For Each gr1 As GridViewRowInfo In gv1.Rows
                                If Not clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) = 0 Then
                                    If gr1.Cells(colSchemeItem).Value = "No" And gr1.Cells(colSampleItem).Value = "No" And gr1.Cells(colPromoSchemeItem).Value = "No" Then
                                        assess = assess + clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) * clsCommon.myCdbl(gr1.Cells("assess" + (gr.Index + 1).ToString()).Value)
                                        taxAmt = taxAmt + clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) * clsCommon.myCdbl(gr1.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value)
                                        Try
                                            Dim x As Double = clsCommon.myCdbl(gr1.Cells(colShippedQty).Value)
                                        Catch ex As Exception
                                            common.clsCommon.MyMessageBoxShow(ex.Message)
                                        End Try

                                        basicprice = basicprice + clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) * clsCommon.myCdbl(gr1.Cells(colBasicAmount).Value)
                                    End If
                                End If
                            Next
                            If assess = 0 AndAlso taxAmt = 0 Then
                                gr.Cells(colTAssessibleAmount).Value = assess
                                gr.Cells(colTTaxAmount).Value = taxAmt
                            End If
                            If assess <> 0 Then
                                gr.Cells(colTTaxRate).Value = Math.Round(taxAmt * 100 / assess, 2)
                                gr.Cells(colTAssessibleAmount).Value = Math.Round(assess, 2)
                                gr.Cells(colTTaxAmount).Value = Math.Round(taxAmt, 2)
                                gr.Cells(colTBasicAmount).Value = basicprice
                            End If
                        Next



                        Dim totalTax As Decimal = 0.0
                        Dim totaltaxamt As Decimal = 0
                        For Each g As GridViewRowInfo In gvTax.Rows
                            totaltaxamt = totaltaxamt + clsCommon.myCdbl(g.Cells(colTTaxAmount).Value)
                        Next
                        txtTotalTaxAmount.Text = totaltaxamt
                        txtShipmentAmt.Text = 0
                        For Each t As GridViewRowInfo In gv1.Rows
                            txtShipmentAmt.Text = clsCommon.myCdbl(txtShipmentAmt.Text) + t.Cells(colTotalItemAmount).Value
                        Next
                        txtTotalTPT.Text = totalTPT

                        If e.Column Is gv1.Columns(colUnitCode) Then
                            Dim pricedate As Date
                            Dim pricedt As String = ""
                            If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                                pricedt = pricedate.ToString("yyyy-MM-dd")
                                grow.Cells(colPriceDateColumn).Value = pricedate.ToString("dd/MM/yyyy")
                                grow.Cells(colPriceDateColumn).ReadOnly = True
                                grow.Cells(colEmptyValueBottle).Value = clsDBFuncationality.getSingleValue("SELECT  Empty_Value_Bottle    FROM TSPL_ITEM_PRICE_MASTER WHERE Item_Code = '" + gv1.CurrentRow.Cells(1).Value + "' AND UOM = '" + gv1.CurrentRow.Cells(colUnitCode).Value + "' AND Start_Date = '" + pricedt + "' AND Price_Code = '" + txtPriceCode.Text + "'")
                                grow.Cells(colEmptyValueShell).Value = clsDBFuncationality.getSingleValue("SELECT  Empty_Value_Shell    FROM TSPL_ITEM_PRICE_MASTER WHERE Item_Code = '" + gv1.CurrentRow.Cells(1).Value + "' AND UOM = '" + gv1.CurrentRow.Cells(colUnitCode).Value + "' AND Start_Date = '" + pricedt + "' AND Price_Code = '" + txtPriceCode.Text + "'")
                                pricedate = clsDBFuncationality.getSingleValue("select distinct Price_Date   from TSPL_TRANSFER_DETAIL where Transfer_No = '" + txtTransferNo.Value + "' and Item_Code = '" + gv1.CurrentRow.Cells(1).Value.ToString() + "'")
                            End If
                            If gv1.CurrentRow.Cells(35).Value = "MS" Then
                                If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gv1.CurrentRow.Cells(ColICode).Value + "' AND " & _
                   " Uom_Code='" + gv1.CurrentRow.Cells(colUnitCode).Value + "'"
                                    Dim convertFact As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                                    If Not convertFact = 0 Then
                                        Dim shells As Integer
                                        Dim bottles As Decimal = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) Mod convertFact
                                        If bottles = 0 Then
                                            shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact
                                            grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
                                        Else
                                            grow.Cells(colEmptyValueShell).Value = "0.00"
                                            shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact + 1
                                            grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells - 1) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
                                        End If
                                        If bottles = 0 And shells = 0 Then
                                            grow.Cells(colEmptyValue).Value = clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value)
                                        End If
                                        gv1.CurrentRow.Cells(colMRP).Value = clsDBFuncationality.getSingleValue("select mrp  from TSPL_TRANSFER_DETAIL where Transfer_No = '" + txtTransferNo.Value + "' and Item_Code =  '" + gv1.CurrentRow.Cells(ColICode).Value + "' and Price_Date ='" + Convert.ToString(pricedt) + "' and Uom =  '" + gv1.CurrentRow.Cells(colUnitCode).Value + "'")

                                        gv1.CurrentRow.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitCode).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value))
                                        grow.Cells(colTotalMRP).Value = grow.Cells(colShippedQty).Value * grow.Cells(colMRP).Value
                                        gv1.CurrentRow.Cells(colBasicAmount).Value = clsDBFuncationality.getSingleValue("SELECT  Item_Basic_Price    FROM TSPL_ITEM_PRICE_MASTER WHERE Item_Code = '" + gv1.CurrentRow.Cells(ColICode).Value + "' AND UOM = '" + grow.Cells(colUnitCode).Value + "'  AND Item_Basic_net = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colMRP).Value) + "' and Start_Date = '" + pricedt + "' AND Price_Code = '" + txtPriceCode.Text + "'")
                                    End If
                                Else

                                End If
                            End If
                        End If
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colFromSchemeCode).Value) > 0 AndAlso gv1.CurrentRow.Cells(colFromSchemeCode).Value.ToString().Contains("MS") Then
                            If clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal Then
                                If e.Column Is gv1.Columns(colShippedQty) Then
                                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gv1.CurrentRow.Cells(ColICode).Value + "' AND " & _
              " Uom_Code='" + gv1.CurrentRow.Cells(colUnitCode).Value + "'"

                                    Dim convertFact As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                                    If Not convertFact = 0 Then
                                        Dim shells As Integer
                                        Dim bottles As Decimal = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) Mod convertFact
                                        If bottles = 0 Then
                                            shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact
                                            grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
                                        Else
                                            grow.Cells(colEmptyValueShell).Value = "0.00"
                                            shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact + 1
                                            grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells - 1) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
                                        End If
                                        If bottles = 0 And shells = 0 Then
                                            grow.Cells(colEmptyValue).Value = clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value)
                                        End If
                                    End If
                                    grow.Cells(colTotalMRP).Value = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) * clsCommon.myCdbl(grow.Cells(colMRP).Value)
                                    grow.Cells(colTotalTaxAmount).Value = grow.Cells(colTaxamount).Value * grow.Cells(colShippedQty).Value
                                End If
                            Else
                                If e.Column Is gv1.Columns(colShippedQty) Then
                                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gv1.CurrentRow.Cells(ColICode).Value + "' AND " & _
              " Uom_Code='" + gv1.CurrentRow.Cells(colUnitCode).Value + "'"
                                    Dim convertFact As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                                    If Not convertFact = 0 Then
                                        Dim shells As Integer
                                        Dim bottles As Decimal = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) Mod convertFact
                                        If bottles = 0 Then
                                            shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact
                                            grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
                                        Else
                                            grow.Cells(colEmptyValueShell).Value = "0.00"
                                            shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact + 1
                                            grow.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * (shells - 1) * convertFact + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) * bottles, 2)
                                        End If
                                        If bottles = 0 And shells = 0 Then
                                            grow.Cells(colEmptyValue).Value = clsCommon.myCdbl(grow.Cells(colEmptyValueBottle).Value) + clsCommon.myCdbl(grow.Cells(colEmptyValueShell).Value)
                                        End If
                                    End If
                                    grow.Cells(colTotalMRP).Value = grow.Cells(colShippedQty).Value * grow.Cells(colMRP).Value
                                    grow.Cells(colTotalTaxAmount).Value = grow.Cells(colTaxamount).Value * grow.Cells(colShippedQty).Value
                                End If
                            End If
                        End If
                        Dim containerdeposit As Decimal = 0
                        For Each g As GridViewRowInfo In gv1.Rows
                            If clsCommon.myCdbl(g.Cells(colShippedQty).Value) > 0 Then
                                containerdeposit = containerdeposit + clsCommon.myCdbl(g.Cells(colEmptyValue).Value)
                            End If
                        Next
                        containerdeposit = containerdeposit + clsCommon.myCdbl(txtshellqty.Text) * 100
                        txtContainerDeposit.Text = containerdeposit
                        txtTotalShipmentAmt.Text = containerdeposit + clsCommon.myCdbl(txtShipmentAmt.Text)
                        funtotalfcfb()
                        If Not clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                            txtShipmentAmt.Text = 0
                            For Each gr As GridViewRowInfo In gv1.Rows
                                If Not clsCommon.myCdbl(gr.Cells(colShippedQty).Value) = 0 Then
                                    If gr.Cells(colSchemeItem).Value = "No" And gr.Cells(colPromoSchemeItem).Value = "No" Then
                                        txtShipmentAmt.Text = clsCommon.myCdbl(txtShipmentAmt.Text) + gr.Cells(colTotalItemAmount).Value
                                    End If
                                End If
                            Next
                        End If
                        txtShipmentAmt.Text = clsCommon.myCdbl(txtNetShipAmt.Text) + clsCommon.myCdbl(txtTotalTaxAmount.Text) + clsCommon.myCdbl(txtTotalTPT.Text)
                        txtTotalShipmentAmt.Text = clsCommon.myCdbl(txtShipmentAmt.Text) + clsCommon.myCdbl(txtContainerDeposit.Text)


                        If e.Column.Name = "shippedqty" Then
                            If clsCommon.myCdbl(e.Row.Cells(colShippedQty).Value) = 0 And e.Row.Cells(colSchemeCodeItem).Value <> "" Then
                                For Each gr As GridViewRowInfo In gv1.Rows
                                    If gr.Cells(colMainItem).Value = e.Row.Cells(colSchemeCodeItem).Value Then
                                        gv1.Rows.RemoveAt(gr.Index)
                                    End If
                                Next
                            End If
                        End If

                        If gv1.CurrentColumn Is gv1.Columns(colMainItem) Then
                            If clsCommon.myLen(e.Value) > 0 Then
                                Dim grow1 As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
                                Dim qry As String = ""
                                If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                                    qry = "select distinct Item_Code , item_Desc as [Item Description] , Uom , MRP, convert(varchar(10),price_date,103) as Start_Date from TSPL_TRANSFER_DETAIL where Transfer_No ='" + txtTransferNo.Value + "'"
                                Else
                                    Dim baseQry As String = GetShipmentViewQty()
                                    qry += "SELECT Item_Code,Item_Desc,Start_Date,Item_Basic_Net as [MRP], UOM FROM (" + baseQry + ")xxx Where Show='N' AND UOM='FC' AND  Price_Code='" + txtPriceCode.Text + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.txtValue.Text + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0 and MRP=xxx.Item_Basic_Net*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=xxx.Item_Code and UOM_Code=xxx.UOM)) "
                                End If
                                Dim itemcode As String = String.Empty
                                Dim startdate As String = String.Empty
                                Dim mrpvalue As Decimal = 0
                                If grow1.Cells(colMainItem).Value <> String.Empty Or grow1.Cells(colCheckvalue).Value <> String.Empty Then
                                    customdatarow = clsCommon.ShowSelectFormForRow("ShipmentItemMain", qry, "Item_Code", clsCommon.myCstr(grow1.Cells(colMainItem).Value))
                                    If customdatarow IsNot Nothing Then
                                        itemcode = Convert.ToString(customdatarow("Item_Code"))
                                        startdate = CDate(customdatarow("start_date")).ToString("dd/MM/yyyy")
                                        mrpvalue = clsCommon.myCdbl(customdatarow("MRP"))
                                        Dim strcheck As String = "N"
                                        Dim ConvFactor As Decimal = 0
                                        For Each gr As GridViewRowInfo In gv1.Rows
                                            If gr.Cells(ColICode).Value = itemcode AndAlso gr.Cells(colPriceDateColumn).Value = startdate AndAlso clsCommon.CompairString(clsCommon.myCstr(gr.Cells(colSchemeItem).Value), "No") = CompairStringResult.Equal Then
                                                ConvFactor = clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + Convert.ToString(gr.Cells(ColICode).Value) + "' and UOM_Code = '" + Convert.ToString(gr.Cells(colUnitCode).Value) + "'")
                                                If gr.Cells(colMRP).Value = mrpvalue / ConvFactor Then
                                                    If gr.Cells(colShippedQty).Value > 0 Then
                                                        strcheck = "Y"
                                                        If String.IsNullOrEmpty(Convert.ToString(gr.Cells(colSchemeCodeItem).Value)) Then
                                                            gr.Cells(colSchemeCodeItem).Value = gv1.CurrentRow.Cells(colFromSchemeCode).Value
                                                        Else
                                                            gv1.CurrentRow.Cells(colFromSchemeCode).Value = gr.Cells(colSchemeCodeItem).Value
                                                        End If
                                                        Exit For
                                                    End If
                                                End If
                                            End If
                                        Next
                                        If strcheck = "N" Then
                                            common.clsCommon.MyMessageBoxShow("Main Item didn't shipped any qty")
                                            gv1.CurrentRow.Cells(colMainItem).Value = ""
                                            gv1.CurrentRow.Cells(colCheckvalue).Value = ""
                                        Else
                                            gv1.CurrentRow.Cells(colMainItem).Value = itemcode
                                            gv1.CurrentRow.Cells(colCheckvalue).Value = "some"
                                        End If
                                    Else
                                        gv1.CurrentRow.Cells(colMainItem).Value = String.Empty
                                        gv1.CurrentRow.Cells(colCheckvalue).Value = String.Empty
                                    End If
                                End If
                            End If
                        ElseIf gv1.CurrentColumn Is gv1.Columns(colDiscountCode) Then
                            sql = "select Code,Description from TSPL_Discount_Master "
                            Dim whrclas As String = " skuwise='Y' "
                            gv1.CurrentRow.Cells(colDiscountCode).Value = clsCommon.ShowSelectForm("ShipmtDisCodFND", sql, "Code", whrclas, clsCommon.myCstr(gv1.CurrentRow.Cells(colDiscountCode).Value))
                            gv1.CurrentRow.Cells(colMainItem).Value = ""
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub CalculateTaxByLocation(ByVal grow As GridViewRowInfo)
        Dim dblTotalTax As Double = 0
        For ii As Integer = 1 To 6
            dblTotalTax += clsCommon.myCdbl(grow.Cells("tax" + clsCommon.myCstr(ii) + "Amt").Value)
        Next
        grow.Cells(colTotalTaxAmount).Value = dblTotalTax * clsCommon.myCdbl(grow.Cells(colShippedQty).Value)

    End Sub

    Private Sub funtotalfcfb()
        Dim totalfc As Decimal = 0
        Dim totalfb As Decimal = 0
        For Each g As GridViewRowInfo In gv1.Rows
            If clsCommon.myCdbl(g.Cells(colShippedQty).Value) <> 0 Then
                If g.Cells(colUnitCode).Value = "FC" Then
                    totalfc = totalfc + clsCommon.myCdbl(g.Cells(colShippedQty).Value)
                End If
                If g.Cells(colUnitCode).Value = "FB" Then
                    totalfb = totalfb + clsCommon.myCdbl(g.Cells(colShippedQty).Value)
                End If
            End If
        Next
        If totalfc = 0 Then
            lblfb.Text = CStr(totalfb)
            lblfc.Text = 0
        ElseIf totalfb = 0 Then
            lblfc.Text = CStr(totalfc)
            lblfb.Text = 0
        ElseIf totalfb <> 0 And totalfc <> 0 Then
            lblfb.Text = CStr(totalfb)
            lblfc.Text = CStr(totalfc)
        ElseIf totalfb = 0 And totalfc = 0 Then
            lblfc.Text = 0
            lblfb.Text = 0
        End If
    End Sub

    Private Sub gvLoadOut1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellEditorInitialized
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor(editor.DisplayMember, FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Private Sub gvLoadOut1_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles gv1.CellBeginEdit
        If TypeOf Me.gv1.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gv1.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
        End If
        If TypeOf Me.gv1.CurrentColumn Is GridViewTextBoxColumn Then
            Dim editor As RadTextBoxEditor = DirectCast(Me.gv1.ActiveEditor, RadTextBoxEditor)
            Dim editorElement As RadTextBoxElement = DirectCast(editor.EditorElement, RadTextBoxElement)
            If e.Column.Name = "shippedQty" Then
                Dim balanceqty As Decimal = 0
                Dim convertFact As Decimal = 0
                If gv1.CurrentRow.Cells(colShippedQty).Value > 0 Then
                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + e.Row.Cells(ColICode).Value + "' AND " & _
                  " Uom_Code='" + e.Row.Cells(colUnitCode).Value + "'"
                    convertFact = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                    If convertFact = 0 Then
                        common.clsCommon.MyMessageBoxShow("Conversion factor is not available.")
                        Exit Sub
                    End If
                End If
                If Not clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal Then
                    Dim whereClause As String = " Item_Code='" + e.Row.Cells(ColICode).Value + "' AND " & _
                                                " Price_Date='" + Format(CDate(e.Row.Cells(colPriceDateColumn).Value), "MM/dd/yyyy") + "' AND " & _
                                                " MRP='" + clsCommon.myCstr(clsCommon.myCdbl(e.Row.Cells(colMRP).Value) * convertFact) + "'"

                    sql = "SELECT Pending_Qty from TSPL_TRANSFER_DETAIL WHERE Transfer_No='" + txtTransferNo.Value + "' AND " + whereClause
                    balanceqty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                End If

                Dim shippingQty As Decimal = 0
                For Each gro As GridViewRowInfo In gv1.Rows
                    If Not gro.Index = e.Row.Index AndAlso clsCommon.myCdbl(gro.Cells(colShippedQty).Value) > 0 Then
                        If gro.Cells(ColICode).Value = e.Row.Cells(ColICode).Value Then
                            sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gro.Cells(ColICode).Value + "' AND " & _
                                      " Uom_Code='" + gro.Cells(colUnitCode).Value + "'"
                            Dim Fact As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                            If Fact = 0 Then
                                common.clsCommon.MyMessageBoxShow("Conversion factor is not available.")
                                Exit Sub
                            End If
                            shippingQty = shippingQty + Math.Round(clsCommon.myCdbl(gro.Cells(colShippedQty).Value) / Fact, 2)
                        End If
                    End If
                Next
                balanceqty = Math.Round(balanceqty - shippingQty, 2)


                ttlItemShpQtyForCheck = balanceqty * convertFact

            ElseIf e.ColumnIndex = 1 Then
            End If
        End If
    End Sub

    Private Sub gvLoadOut1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        If e.Column.Name = "complete" Then
            If grow.Cells(ColComplete).Value = "No" Then
                grow.Cells(ColComplete).Value = "Yes"
            ElseIf grow.Cells(ColComplete).Value = "Yes" Then
                grow.Cells(ColComplete).Value = "No"
            End If
        ElseIf e.Column.Name = "schemeApplicable" And grow.Cells(colPromoSchemeItem).Value = "No" And grow.Cells(colSchemeItem).Value = "No" And grow.Cells(colSampleItem).Value = "No" Then
            If grow.Cells(colSchemeApplicable).Value = "Yes" Then
                grow.Cells(colSchemeApplicable).Value = "No"

            ElseIf grow.Cells(colSchemeApplicable).Value = "No" Then
                grow.Cells(colSchemeApplicable).Value = "Yes"
            End If
        ElseIf e.Column.Name = "promoSchemeApplicable" And grow.Cells(colPromoSchemeItem).Value = "No" And grow.Cells(colSchemeItem).Value = "No" And grow.Cells(colSampleItem).Value = "No" Then
            If grow.Cells(colPromoSchemeApplicable).Value = "Yes" Then
                grow.Cells(colPromoSchemeApplicable).Value = "No"
            ElseIf grow.Cells(colPromoSchemeApplicable).Value = "No" Then
                grow.Cells(colPromoSchemeApplicable).Value = "Yes"
            End If
        ElseIf e.Column.Name = "schemeDiscountApplicable" And grow.Cells(colPromoSchemeItem).Value = "No" And grow.Cells(colSchemeItem).Value = "No" And grow.Cells(colSampleItem).Value = "No" Then
            If grow.Cells(colSchemeDiscountApplicable).Value = "Yes" Then
                grow.Cells(colSchemeDiscountApplicable).Value = "No"
            ElseIf grow.Cells(colSchemeDiscountApplicable).Value = "No" Then
                grow.Cells(colSchemeDiscountApplicable).Value = "Yes"
            End If
        ElseIf e.Column.Name = "taxAmount" Then
            If e.Row.Cells(colShippedQty).Value <> 0 Then
                Dim frm As New FrmTaxDetails()
                frm.locationCode = txtLocation.Value
                frm.taxGroupCode = fndTaxGroup.txtValue.Text

                frm.assessibleAmount = Math.Round(Math.Round(clsCommon.myCdbl(e.Row.Cells(colMRP).Value) * abatement(e.Row, Nothing) / 100, 2), 2).ToString()
                frm.gridRow = e.Row
                frm.ShowDialog()
            End If
        End If
    End Sub

    Private Sub gvLoadOut1_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles gv1.EditorRequired
        Dim col As GridViewMultiComboBoxColumn = TryCast(gv1.Columns("unitCode"), GridViewMultiComboBoxColumn)
        Dim col1 As GridViewComboBoxColumn = TryCast(gv1.Columns(colPriceDateColumn), GridViewComboBoxColumn)
        If gv1.CurrentColumn.Name = "unitCode" Then
            sql = "SELECT DISTINCT U.Unit_Code, U.Unit_Desc, U.Conv_Factor FROM TSPL_UNIT_MASTER AS U INNER JOIN TSPL_ITEM_PRICE_MASTER AS P " & _
   " ON U.Unit_Code=P.UOM WHERE P.Item_Code='" + gv1.CurrentRow.Cells(ColICode).Value + "' AND P.Price_Code='" + txtPriceCode.Text + "' ORDER BY Unit_Code"
            ds = clsDBFuncationality.GetDataTable(sql)
            col.ValueMember = "Unit_Code"
            col.DataSource = ds
        ElseIf gv1.CurrentColumn.Name = colPriceDateColumn Then
            sql = "select distinct CONVERT(varchar(10), Start_Date, 103) as Start_Date FROM TSPL_ITEM_PRICE_MASTER Where Price_Code='" + txtPriceCode.Text + "' and Item_Code='" + gv1.CurrentRow.Cells(ColICode).Value + "' ORDER BY Start_Date"
            ds = clsDBFuncationality.GetDataTable(sql)
            col1.ValueMember = "Start_Date"
            col1.DataSource = ds

        End If

        If gv1.CurrentColumn.Index = 1 Then
            If Not String.IsNullOrEmpty(gv1.CurrentRow.Cells(ColICode).Value) Then
                gv1.CurrentRow.Cells(ColItemName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct Item_Desc   from TSPL_TRANSFER_DETAIL where Transfer_No = '" + txtTransferNo.Value + "' and Item_Code = '" + gv1.CurrentRow.Cells(1).Value.ToString() + "'"))

            End If

        End If
    End Sub

    Private Sub gvTaxDetails_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles gvTax.EditorRequired
        If TypeOf gvTax.CurrentColumn Is GridViewComboBoxColumn Then
            Dim coltaxrate As GridViewComboBoxColumn = TryCast(gvTax.Columns("taxRate"), GridViewComboBoxColumn)
            sql = "select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Code='" + gvTax.CurrentRow.Cells("taxAuthority").Value + "' AND Tax_Type='S'"
            ds = clsDBFuncationality.GetDataTable(sql)
            coltaxrate.ValueMember = "Tax_Rate"
            coltaxrate.DataSource = ds
        End If
    End Sub

    Private Sub gvTaxDetails_ContextMenuOpening(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ContextMenuOpeningEventArgs) Handles gvTax.ContextMenuOpening
        e.ContextMenu.Enabled = False
    End Sub

    Private Function funvalidatevehicle() As Boolean
        Dim count As Decimal = 0
        Dim segno As String = String.Empty
        Dim strvehiclenum As String = lblVhicleNo.Text
        Dim sql As String = "select segment_code from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(txtVehicleCode.Value) + "' "
        If Not String.IsNullOrEmpty(connectSql.RunScalar(sql)) Then
            sql = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicleCode.Value + "'"
            lblVhicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
            Return True
        Else
            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            strmessage += "Do you want to continue "

            

            If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                If clsCommon.myLen(lblVhicleNo.Text) <= 0 Then
                    lblVhicleNo.Focus()
                    Throw New Exception("Please Enter Vehicle No")
                End If


                txtVehicleCode.Value = clsCommon.incval(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Max(Segment_code) from TSPL_GL_SEGMENT_CODE where Segment_name = 'Vehicles'")))
                'strvehiclenum = txtVehicleCode.Text
                sql = "select seg_no from tspl_gl_segment where seg_name='Vehicles'"
                segno = CStr(connectSql.RunScalar(sql))
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    connectSql.RunSpTransaction(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", segno), New SqlParameter("@segmentname", "Vehicles"), New SqlParameter("@segmentcode", txtVehicleCode.Value), New SqlParameter("@desc", strvehiclenum), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                    connectSql.RunSpTransaction(trans, "SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", txtVehicleCode.Value), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", "0"), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

                    trans.Commit()
                Catch ex As Exception
                    txtVehicleCode.Value = ""
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try

                'lblVhicleNo.Text = txtVehicleCode.Text + "-Hired"
                txtVehicleCode.Text = txtVehicleCode.Value
                Return True
            Else
                txtVehicleCode.Value = String.Empty
                txtVehicleCode.Text = txtVehicleCode.Value
                Return False
            End If
        End If
    End Function

    Private Function abatement(ByVal grow As GridViewRowInfo, ByVal trans As SqlTransaction) As Decimal
        Dim abat As Decimal
        sql = "Select Abatement_Rate from TSPL_ITEM_PRICE_MASTER WHERE Item_Code='" + clsCommon.myCstr(grow.Cells(ColICode).Value) + "' AND Item_Basic_Net='" + clsCommon.myCstr(grow.Cells(colMRP).Value) + "' AND Start_Date='" + clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells(colPriceDateColumn).Value), "dd/MMM/yyyy") + "'"
        abat = clsDBFuncationality.getSingleValue(sql, trans)
        Return abat
    End Function

    Private Function abatement1(ByVal grow As GridViewRowInfo, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim abat As Decimal
        sql = "Select Abatement from TSPL_SHIPMENT_DETAILS WHERE Shipment_Id='" + grow.Cells("lineNo").Value + "' AND Shipment_No='" + txtDocNo.Value + "'"
        If trans Is Nothing Then
            abat = clsDBFuncationality.getSingleValue(sql)
        Else
            abat = clsDBFuncationality.getSingleValue(sql, trans)
        End If
        Return abat
    End Function

    Private Function itemAssessibleAmt(ByVal grow As GridViewRowInfo, ByVal trans As SqlTransaction) As Decimal
        Dim TAX As Decimal = Math.Round(clsCommon.myCdbl(grow.Cells(colMRP).Value) * abatement(grow, trans) / 100, 2)

        Return Math.Round(clsCommon.myCdbl(grow.Cells(colMRP).Value) * abatement(grow, trans) / 100, 2)
    End Function

    Private Function totalItemAssessibleAmt(ByVal grow As GridViewRowInfo, ByVal trans As SqlTransaction) As Decimal
        Dim amt As Decimal = Math.Round(clsCommon.myCdbl(grow.Cells(colTotalMRP).Value) * abatement(grow, trans) / 100, 2)
        Return amt
    End Function

    Private Function totalAssessibleAmt() As Decimal
        Dim total As Decimal = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If Not clsCommon.myCdbl(grow.Cells(colShippedQty).Value) = 0 Then
                total = total + Math.Round(clsCommon.myCdbl(grow.Cells(colTotalMRP).Value) * abatement(grow, Nothing) / 100, 2)
            End If
        Next
        Return total
    End Function

    Private Function totalMRP() As Decimal
        Dim total As Decimal = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If grow.Cells(colSchemeItem).Value = "No" AndAlso grow.Cells(colPromoSchemeItem).Value = "No" AndAlso grow.Cells(colSampleItem).Value = "No" Then
                total = total + clsCommon.myCdbl(grow.Cells(colTotalMRP).Value)
            End If
        Next
        Return total
    End Function

    Private Function totalBasicAmt() As Decimal
        Dim total As Decimal = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            total = total + clsCommon.myCdbl(grow.Cells(colTotalBasicAmount).Value)
        Next
        Return total
    End Function

    Private Function totalNetAmount() As Decimal
        Dim total As Decimal = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If grow.Cells(colSchemeItem).Value = "No" AndAlso grow.Cells(colPromoSchemeItem).Value = "No" AndAlso grow.Cells(colSampleItem).Value = "No" Then
                total = total + clsCommon.myCdbl(grow.Cells(colTotalNetAmount).Value)
            End If
        Next
        Return total
    End Function

    Private Function totalDiscount() As Decimal
        Dim total As Decimal = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            total = total + clsCommon.myCdbl(grow.Cells(colTotalDiscountAmount).Value)
        Next
        Return total
    End Function

    Private Function totalTransport() As Decimal
        Dim total As Decimal = 0
        If clsCommon.myCdbl(txtDiscPer.Text) > 0 Then
            total = txtTotalTPT.Text
        Else
            For Each grow As GridViewRowInfo In gv1.Rows
                total = total + clsCommon.myCdbl(grow.Cells(colTotalTPT).Value)
            Next
        End If

        Return total
    End Function

    Private Function promoSchemeApplicable(ByVal grow As GridViewRowInfo) As String
        If grow.Cells(colPromoSchemeApplicable).Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function schemeItemApplicable(ByVal grow As GridViewRowInfo) As String
        If grow.Cells(colSchemeApplicable).Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function schemeDiscApplicable(ByVal grow As GridViewRowInfo) As String
        If grow.Cells(colSchemeDiscountApplicable).Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function schemeItem(ByVal grow As GridViewRowInfo) As String
        If grow.Cells(colSchemeItem).Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function promoSchemeItem(ByVal grow As GridViewRowInfo) As String
        If grow.Cells(colPromoSchemeItem).Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function sampleItem(ByVal grow As GridViewRowInfo) As String
        If grow.Cells(colSampleItem).Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function complete(ByVal grow As GridViewRowInfo) As String
        If grow.Cells(ColComplete).Value = "Yes" Then
            Return "Y"
        End If
        Return "N"
    End Function

    Private Function checkItemonLocation(ByVal itemcode As String, ByVal shippedqty As Decimal, ByVal location As String, ByVal uom As String, ByVal mrp As Decimal, ByVal scheme As Boolean, ByVal batchnumber As String) As Boolean
        Try
            Dim stockQty As Decimal = 0
            If shippedqty > 0 Then
                sql = "SELECT Allow_Negative_Inv FROM TSPL_INV_PARAMETERS"
                If clsDBFuncationality.getSingleValue(sql) = "N" Then
                    Dim orderedQty As Decimal = 0
                    Dim totalShippedQty As Decimal = 0
                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + itemcode + "' AND Uom_Code='" + uom + "'"
                    Dim convertFact As Decimal = clsItemMaster.GetConvertionFactor(itemcode, uom, Nothing)
                    conversionnumber = convertFact
                    If convertFact = 0 Then
                        common.clsCommon.MyMessageBoxShow("Conversion factor is not defined.")
                        Return False
                    End If
                    Dim standardMRP As Decimal = Math.Round(mrp * convertFact, 2)
                    If Not clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then

                        Dim conversion As Decimal = clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + itemcode + "' AND UOM_Code  = '" + uom + "' ")
                        sql = "SELECT ISNULL(sum(isnull(Item_Qty, 0)),0) FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + itemcode + "' " & _
                              " AND location_code='" + location + "' and MRP='" + standardMRP.ToString() + "' "
                        stockQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql)) * conversion
                        For Each gr As GridViewRowInfo In gv1.Rows
                            sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gv1.CurrentRow.Cells(ColICode).Value + "' AND " & _
                               " Uom_Code='" + gr.Cells(colUnitCode).Value + "'"
                            Dim fact As Decimal = clsDBFuncationality.getSingleValue(sql)
                            If clsCommon.myCdbl(gr.Cells(colShippedQty).Value) > 0 AndAlso gr.Cells(ColICode).Value = itemcode AndAlso Math.Round(clsCommon.myCdbl(gr.Cells(colMRP).Value) * fact, 2) = standardMRP AndAlso gr.Cells(colBatchNumber).Value = batchnumber Then
                                totalShippedQty = totalShippedQty + clsCommon.myCdbl(gr.Cells(colShippedQty).Value) * convertFact / fact
                            End If
                        Next
                        If conversion = 0 Then
                            Throw New Exception("Conversion factor can't be zero for item" + itemcode)
                        End If
                        totalShippedQty = Math.Round(totalShippedQty, 2)
                        If totalShippedQty < stockQty Then
                        ElseIf totalShippedQty = stockQty Then
                        Else
                            common.clsCommon.MyMessageBoxShow("Item " + itemcode + " quantity is not available at the location.")
                            Return False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Function checkItemonLocation12(ByVal itemcode As String, ByVal shippedqty As Decimal, ByVal location As String, ByVal uom As String, ByVal mrp As Decimal, ByVal scheme As Boolean, ByVal batchnumber As String) As Boolean
        Dim stockQty As Decimal = 0
        If shippedqty > 0 Then
            sql = "SELECT Allow_Negative_Inv FROM TSPL_INV_PARAMETERS"
            If clsDBFuncationality.getSingleValue(sql) = "N" Then
                Dim orderedQty As Decimal = 0
                Dim totalShippedQty As Decimal = 0
                sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + itemcode + "' AND " & _
                       " Uom_Code='" + uom + "'"
                Dim convertFact As Decimal = clsDBFuncationality.getSingleValue(sql)
                conversionnumber = convertFact
                If convertFact = 0 Then
                    common.clsCommon.MyMessageBoxShow("Conversion factor is not defined.")
                    Return False
                End If
                Dim standardMRP As Decimal = Math.Round(mrp * convertFact, 2)
                If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                    sql = "Select Pending_Qty from TSPL_TRANSFER_DETAIL WHERE Transfer_No='" + txtTransferNo.Value.ToString() + "' AND " & _
                    " Item_Code='" + itemcode + "' AND MRP='" + standardMRP.ToString() + "'"
                    Dim availableQty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                    For Each gr As GridViewRowInfo In gv1.Rows
                        sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gr.Cells(ColICode).Value + "' AND " & _
                          " Uom_Code='" + gr.Cells(colUnitCode).Value + "'"
                        Dim fact As Decimal = clsDBFuncationality.getSingleValue(sql)
                        If fact = 0 Then
                            common.clsCommon.MyMessageBoxShow("Conversion factor is not defined.")
                            Return False
                        End If

                        If gr.Cells(ColICode).Value = itemcode AndAlso Math.Round(clsCommon.myCdbl(gr.Cells(colMRP).Value) * fact, 2) = standardMRP Then
                            sql = "Select Pending_Qty from TSPL_TRANSFER_DETAIL WHERE Transfer_No='" + txtTransferNo.Value.ToString() + "' AND " & _
                     " Item_Code='" + itemcode + "' AND MRP='" + standardMRP.ToString() + "'"
                            availableQty = availableQty + clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql)) * convertFact / fact
                            totalShippedQty = totalShippedQty + clsCommon.myCdbl(gr.Cells(colShippedQty).Value) * convertFact / fact
                        End If
                    Next
                    If totalShippedQty > availableQty Or availableQty = 0 Then
                        common.clsCommon.MyMessageBoxShow("This quantity is not available at the location. Of Item " + itemcode.ToString())
                        Return False
                    End If
                Else
                    Dim JKL As Integer = gv1.CurrentRow.Index
                    Dim conversion As Decimal = clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gv1.CurrentRow.Cells(ColICode).Value + "' AND UOM_Code  = '" + gv1.CurrentRow.Cells(colUnitCode).Value + "' ")
                    sql = "SELECT isnull(sum(Item_Qty),0) as [Item_Qty] FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + itemcode + "'  AND location_code='" + location + "' and MRP='" + standardMRP.ToString() + "' "
                    stockQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                    stockQty = stockQty * conversion

                    For Each gr As GridViewRowInfo In gv1.Rows
                        sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + gv1.CurrentRow.Cells(ColICode).Value + "' AND " & _
                           " Uom_Code='" + gr.Cells(colUnitCode).Value + "'"
                        Dim fact As Decimal = clsDBFuncationality.getSingleValue(sql)
                        If clsCommon.myCdbl(gr.Cells(colShippedQty).Value) > 0 AndAlso gr.Cells(ColICode).Value = itemcode AndAlso Math.Round(clsCommon.myCdbl(gr.Cells(colMRP).Value) * fact, 2) = standardMRP AndAlso gr.Cells(colBatchNumber).Value = batchnumber Then
                            totalShippedQty = totalShippedQty + clsCommon.myCdbl(gr.Cells(colShippedQty).Value) * conversion / fact
                        End If
                    Next
                    If totalShippedQty < stockQty Then
                    ElseIf totalShippedQty = stockQty Then
                    Else
                        common.clsCommon.MyMessageBoxShow("This quantity is not available at the location.")
                        Return False
                    End If
                End If
            End If
        End If
        Return True
    End Function

    Private Sub resetFNDCustomer()
        txtCustomer.Value = String.Empty
        txtCustomerName.Text = String.Empty
        txtRouteNo.Value = String.Empty
        lblRouteDesc.Text = String.Empty
        txtPriceCode.Text = String.Empty
        fndTaxGroup.txtValue.Text = String.Empty
        lblTaxDesc.Text = ""
        txtSalesman.Value = String.Empty
        cboPriceDate.Text = "Select"
        cboPriceDate.DataSource = Nothing
        cboPriceDate.Items.Clear()
        cboPriceDate.Text = "Select"
        gv1.Rows.Clear()
    End Sub

    Private Sub resetFNDTaxGroup()
        gvTax.DataSource = Nothing
        gvTax.Rows.Clear()
    End Sub

    Private Sub resetForm()
        Dim dtTransaction As DateTime = clsCommon.GETSERVERDATE()

        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickMachineDateForTran, clsFixedParameterCode.PickMachineDateForTran, Nothing)) = 1 Then
            dtTransaction = DateTime.Now
        End If

        cboPriceDate.Text = dtTransaction
        txtTransferDate.Value = dtTransaction
        txtDate.Value = dtTransaction
        txtRemovalTime.Value = dtTransaction
        txtExpectedShipDate.Value = dtTransaction
        txtOrderDate.Value = dtTransaction


        txtcustomerinvoiceno.Text = ""
        txtshellqty.Text = 0
        lblfc.Text = 0
        lblfb.Text = 0

        txtCustomer.Enabled = True
        txtTransferNo.Enabled = True
        txtDate.Enabled = True
        rbFB.Enabled = True
        rbFC.Enabled = True

        rbAll.Enabled = True
        rbFB.IsChecked = False
        txtEmployeeCode.Value = ""
        rbFC.IsChecked = False
        rbAll.IsChecked = False
        cboLoadOutType.Enabled = True
        txtCustomer.Enabled = True
        txtLocation.Enabled = True

        txtLocation.Enabled = True
        txtOrderNo.Value = ""
        gv1.Columns("orderedQty").HeaderText = "Ordered Qty"
        Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        lblTransferDate.Visible = False
        lblTransferNo.Visible = False
        txtTransferNo.Visible = False
        txtTransferDate.Visible = False
        chkInvoice.Checked = True
        lblOrderDate.Visible = True
        lblOrderNo.Visible = True
        txtOrderNo.Visible = True
        txtOrderDate.Visible = True

        txtTransferNo.Value = ""
        chkAgainstCForm.Checked = False
        txtSchemeSample.txtValue.Text = ""


        btnAdd.Text = "Save"
        btnAdd.Enabled = True
        btnSaveAndPrint.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnSettlement.Enabled = False
        txtRef.Text = String.Empty
        txtCustomerPONO.Text = String.Empty
        txtOtherCharges.Text = 0.0
        txtRemarks.Text = String.Empty
        txtShipTo.Value = ""
        lblShipTo.Text = ""

        txtAdditionalCharges.Text = 0.0
        txtDesc.Text = String.Empty
        txtCustDisc.Text = 0.0
        txtFreight.Text = 0.0
        txtKMReading.Text = 0
        txtRouteNo.Value = String.Empty
        txtShipmentAmt.Text = 0.0
        txtShipmentTotal.Text = 0.0
        ''txtStatus.Text = "Open"
        UsLock1.Status = ERPTransactionStatus.Pending
        txtTotalTaxAmount.Text = 0.0
        txtTripNo.Text = 0
        txtVehicleCode.Value = String.Empty
        txtPaymentTerm.Value = String.Empty
        txtLocation.Value = String.Empty
        cboModeOfTransport.Text = "Select"
        cboLoadOutType.Text = "Sale"
        txtDocNo.Value = ""
        cboPriceDate.DropDownStyle = RadDropDownStyle.DropDownList
        cboLoadOutType.DropDownStyle = RadDropDownStyle.DropDownList
        cboModeOfTransport.DropDownStyle = RadDropDownStyle.DropDownList
        pvLoadOut.SelectedPage.Name = "pageLoadOut"
        gv1.AllowAddNewRow = True ''Change false to ture
        txtCustomer.Value = ""
        gv1.Columns("promoSchemeApplicable").DataSourceNullValue = "No"
        gv1.Columns("promoSchemeItem").DataSourceNullValue = "No"
        gv1.Columns("promoSchemeCode").DataSourceNullValue = ""
        gv1.Columns("SchemeItem").DataSourceNullValue = "No"
        gv1.Columns("schemeApplicable").DataSourceNullValue = "No"
        gv1.Columns("sampleItem").DataSourceNullValue = "No"
        gv1.Columns("schemeCodeItem").DataSourceNullValue = ""
        gv1.Columns("schemeCodeDiscount").DataSourceNullValue = ""
        gv1.Columns("emptyValue").DataSourceNullValue = 0
        gv1.Columns("priceCode").DataSourceNullValue = ""
        gv1.Columns("location").DataSourceNullValue = ""
        gv1.Columns("unitCode").DataSourceNullValue = ""
        If gv1.Columns.Contains("transferId") Then
            gv1.Columns.Remove("transferId")
        End If
        cboModeOfTransport.Text = "By Road"
        rbFB.Enabled = True
        rbFC.Enabled = True
        rbAll.Enabled = True
        txtTotalTPT.Text = 0
        txtDiscPer.Text = 0
        txtDiscAmt.Text = 0
        txtNetShipAmt.Text = 0
        txtContainerDeposit.Text = 0
        txtTotalShipmentAmt.Text = 0
        chlcreditinvoice.Checked = False
        lblSaleInvoiceNo.Text = ""

        lblTransaction.Visible = False
        lblEmpName.Text = ""
        lblVhicleNo.Text = ""
        lblSalesMan1.Text = ""
        ReStoreGridLayout()
    End Sub

    Private Sub calculateItemTaxAgainstItemRate(ByVal gr As GridViewRowInfo)
        Dim basic As Decimal = gr.Cells(colitemNetAmount).Value
        Dim assessible As Decimal = itemAssessibleAmt(gr, Nothing)
        Dim ttlItemtax As Decimal = 0
        sql = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"
        Dim strexcisable As String = clsDBFuncationality.getSingleValue(sql)
        If gvTax.RowCount <> 0 Then
            If strexcisable = "T" Or strexcisable = "Y" Then
                For Each grow As GridViewRowInfo In gvTax.Rows
                    If grow.Index = 0 Then
                        gr.Cells("assess" + (grow.Index + 1).ToString()).Value = Math.Round(assessible, 2)
                    ElseIf grow.Cells("surtax").Value = "N" Then
                        Dim taxabletaxTotal As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTax.Rows
                            If gro.Index < grow.Index Then
                                If clsCommon.CompairString(clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = CompairStringResult.Equal Then
                                    taxabletaxTotal = taxabletaxTotal + clsCommon.myCdbl(gr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value)
                                End If
                            End If
                        Next
                        If basic = 0 Then
                            gr.Cells("assess" + (grow.Index + 1).ToString()).Value = basic
                        Else
                            gr.Cells("assess" + (grow.Index + 1).ToString()).Value = basic + taxabletaxTotal
                        End If
                    ElseIf grow.Cells("surtax").Value = "Y" Then
                        Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                        Dim assess As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTax.Rows
                            If gro.Cells(0).Value = strSurtaxCode Then
                                assess = gr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value
                                Exit For
                            End If
                        Next
                        gr.Cells("assess" + (grow.Index + 1).ToString()).Value = Math.Round(assess, 2)
                    End If
                    gr.Cells("tax" + (grow.Index + 1).ToString() + "Amt").Value = Math.Round((clsCommon.myCdbl(gr.Cells("assess" + (grow.Index + 1).ToString()).Value) * clsCommon.myCdbl(gr.Cells("tax" + (grow.Index + 1).ToString() + "Rate").Value) / 100), 4).ToString()
                    ttlItemtax = ttlItemtax + clsCommon.myCdbl(gr.Cells("tax" + (grow.Index + 1).ToString() + "Amt").Value)
                Next
            Else
                For Each grow As GridViewRowInfo In gvTax.Rows
                    If grow.Index = 0 Then
                        gr.Cells("assess" + (grow.Index + 1).ToString()).Value = basic
                    ElseIf grow.Cells("surtax").Value = "N" Then
                        Dim taxabletaxTotal As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTax.Rows
                            If gro.Index < grow.Index Then
                                If clsCommon.CompairString(clsCommon.myCstr(gro.Cells("taxable").Value), "Y") = CompairStringResult.Equal Then
                                    taxabletaxTotal = taxabletaxTotal + clsCommon.myCdbl(gr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value)
                                End If
                            End If
                        Next
                        gr.Cells("assess" + (grow.Index + 1).ToString()).Value = basic + taxabletaxTotal
                    ElseIf grow.Cells("surtax").Value = "Y" Then
                        Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                        Dim assess As Decimal = 0
                        For Each gro As GridViewRowInfo In gvTax.Rows
                            If gro.Cells(0).Value = strSurtaxCode Then
                                assess = gr.Cells("tax" + (gro.Index + 1).ToString() + "Amt").Value
                                Exit For
                            End If
                        Next
                        gr.Cells("assess" + (grow.Index + 1).ToString()).Value = Math.Round(assess, 2)
                    End If
                    gr.Cells("tax" + (grow.Index + 1).ToString() + "Amt").Value = Math.Round((clsCommon.myCdbl(gr.Cells("assess" + (grow.Index + 1).ToString()).Value) * clsCommon.myCdbl(gr.Cells("tax" + (grow.Index + 1).ToString() + "Rate").Value) / 100), 4).ToString()
                    ttlItemtax = ttlItemtax + clsCommon.myCdbl(gr.Cells("tax" + (grow.Index + 1).ToString() + "Amt").Value)
                Next
                gr.Cells(colTaxamount).Value = Math.Round(ttlItemtax, 4, MidpointRounding.ToEven)
            End If
        Else
        End If
    End Sub

    Private Sub findQtyandPromoSchemeCode(ByVal grow As GridViewRowInfo, ByVal schemeType As String)
        Dim dr1 As DataTable
        Dim schemeAppCol As String = ""
        Dim schemeCodeCol As String = ""
        Dim schemeItemCol As String = ""
        If schemeType = "P" Then
            schemeAppCol = "promoSchemeApplicable"
            schemeCodeCol = "promoSchemeCode"
            schemeItemCol = "promoSchemeItem"
        ElseIf schemeType = "Q" Then
            schemeAppCol = "schemeApplicable"
            schemeCodeCol = "schemeCodeItem"
            schemeItemCol = "schemeItem"
        End If
        Try

            sql = "SELECT S.Scheme_Code,S.Main_Item_Qty FROM TSPL_SCHEME_MASTER AS S INNER JOIN TSPL_SCHEME_DETAILS AS D ON S.Scheme_Code = D.Scheme_Code " & _
                  " WHERE  (S.Main_Item_Code = '" + grow.Cells(ColICode).Value + "') AND S.Start_Date <='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "MM/dd/yyyy") + "' " & _
                  " AND (S.End_Date >='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "MM/dd/yyyy") + "' OR S.End_Date is NULL ) AND S.Main_Item_Qty <= '" + clsCommon.myCstr(grow.Cells(colShippedQty).Value) + "' " & _
                  " AND S.Main_item_UOM='" + grow.Cells(colUnitCode).Value + "' AND S.MRP='" + clsCommon.myCstr(grow.Cells(colMRP).Value) + "' AND S.Cust_Cate =  (SELECT Cust_Category_Code  FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code = '" + txtCustomer.Value + "')"

            If grow.Cells(schemeAppCol).Value = "No" Then
                For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    If clsCommon.myLen(grow.Cells(schemeCodeCol).Value) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(schemeRow).Cells(colFromSchemeCode).Value), clsCommon.myCstr(grow.Cells(schemeCodeCol).Value)) = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(grow.Cells(colShippedQty).Value) <= 0 Then
                                gv1.Rows.RemoveAt(schemeRow)
                            End If
                        End If
                    End If
                Next
                grow.Cells(schemeCodeCol).Value = String.Empty
            ElseIf grow.Cells(schemeAppCol).Value = "Yes" Then
                sql = "SELECT S.Scheme_Code,S.Main_Item_Qty FROM TSPL_SCHEME_MASTER AS S INNER JOIN TSPL_SCHEME_DETAILS AS D ON S.Scheme_Code = D.Scheme_Code " & _
                      " WHERE  (S.Scheme_Type = '" + schemeType + "') AND (S.Main_Item_Code = '" + grow.Cells(ColICode).Value + "') AND " & _
                      " S.Start_Date <='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "MM/dd/yyyy") + "' AND (S.End_Date >='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "MM/dd/yyyy") + "' OR S.End_Date is NULL ) " & _
                      " AND S.Main_Item_Qty <= '" + clsCommon.myCstr(grow.Cells(colShippedQty).Value) + "' " & _
                      " AND S.Main_item_UOM='" + grow.Cells(colUnitCode).Value + "' AND S.MRP='" + clsCommon.myCstr(grow.Cells(colMRP).Value) + "' AND S.Cust_Cate =  (SELECT Cust_Category_Code  FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code = '" + txtCustomer.Value + "')"

                dr1 = clsDBFuncationality.GetDataTable(sql)
                Dim discountRatio As Integer = 0
                If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then

                    grow.Cells(schemeCodeCol).Value = dr1.Rows(0)(0).ToString
                    Dim mainItemQty As Decimal = clsCommon.myCdbl(dr1.Rows(0)(1).ToString())
                    Dim mainItemCode As String = grow.Cells(ColICode).Value
                    Dim mode As Decimal = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) Mod clsCommon.myCdbl(dr1.Rows(0)(1).ToString())
                    discountRatio = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - mode) / clsCommon.myCdbl(dr1.Rows(0)(1).ToString())

                    If discountRatio > 0 Then

                        For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                            If Not grow.Cells(schemeCodeCol).Value.ToString().Trim() = String.Empty Then
                                If gv1.Rows(schemeRow).Cells(colFromSchemeCode).Value = grow.Cells(schemeCodeCol).Value Then
                                    gv1.Rows.RemoveAt(schemeRow)
                                End If
                            End If
                        Next
                        sql = "SELECT SD.Scheme_Item_Code, SD.Qty,SD.UOM,SD.MRP FROM TSPL_SCHEME_MASTER AS SM INNER JOIN TSPL_SCHEME_DETAILS " & _
            "  AS SD ON SM.Scheme_Code = SD.Scheme_Code WHERE (SM.Scheme_Code = '" + grow.Cells(schemeCodeCol).Value + "')"
                        dr1 = clsDBFuncationality.GetDataTable(sql)
                        If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then
                            For Each tdr As DataRow In dr1.Rows
                                Dim usedItemQty As Decimal = clsCommon.myCdbl(tdr(1).ToString()) * discountRatio
                                If Not checkItemonLocation12(tdr(0).ToString(), usedItemQty, txtLocation.Value, tdr(2).ToString(), clsCommon.myCdbl(tdr(3).ToString()), True, grow.Cells(colBatchNumber).Value) Then
                                    grow.Cells(schemeCodeCol).Value = String.Empty
                                    grow.Cells(schemeAppCol).Value = "No"
                                    Exit Sub
                                End If
                            Next

                        End If
                    End If
                    addDiscountItemRow(discountRatio, grow, schemeType, schemeCodeCol, clsCommon.myCstr(grow.Cells(ColICode).Value))
                Else
                    common.clsCommon.MyMessageBoxShow("No scheme applicable.")
                    grow.Cells(schemeAppCol).Value = "No"
                    grow.Cells(schemeCodeCol).Value = String.Empty
                End If
            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
            grow.Cells(schemeAppCol).Value = "No"
            grow.Cells(schemeCodeCol).Value = String.Empty
            Exit Sub
        End Try

    End Sub

    Private Sub addDiscountItemRow(ByVal disRatio As Integer, ByVal grow As GridViewRowInfo, ByVal schemeType As String, ByVal schemeCodeCol As String, ByVal strICode As String)
        sql = "SELECT Scheme_Item_Code, Scheme_Item_Desc, Qty, UOM,MRP  FROM TSPL_SCHEME_DETAILS WHERE (Scheme_Code = '" + grow.Cells(schemeCodeCol).Value + "')"
        Dim schemeDR As DataTable = clsDBFuncationality.GetDataTable(sql)
        If schemeDR IsNot Nothing AndAlso schemeDR.Rows.Count > 0 Then
            Dim i As Integer = grow.Index + 1
            For Each tdr As DataRow In schemeDR.Rows
                Dim viewInfo As New GridViewInfo(gv1.MasterTemplate)
                Dim dataRowInfo As New GridViewDataRowInfo(viewInfo)
                sql = "SELECT  top 1 Empty_Value_Bottle , Empty_Value_Shell,Item_Basic_Price,Start_Date,(TSPL_ITEM_PRICE_MASTER.NetLTPT+TSPL_ITEM_PRICE_MASTER.Price_Amount10) as PriceToShow,TSPL_ITEM_PRICE_MASTER.Abatement_Rate FROM TSPL_ITEM_PRICE_MASTER WHERE Item_Code = '" + Convert.ToString(tdr("Scheme_Item_Code")) + "' AND " & _
                "UOM = '" + Convert.ToString(tdr("UOM")) + "' AND Item_Basic_Net = '" + Convert.ToString(tdr("MRP")) + "' and Price_Code='" + txtPriceCode.Text + "'  order by Start_Date desc"
                Dim emptydr As DataTable = clsDBFuncationality.GetDataTable(sql)
                'Dim emptybottle, emptyshell As Decimal
                Dim dblBasicPrice As Double = 0
                Dim strStartDate As String = ""
                If emptydr IsNot Nothing AndAlso emptydr.Rows.Count > 0 Then
                    dataRowInfo.Cells(colEmptyValueBottle).Value = emptydr.Rows(0)("Empty_Value_Bottle")
                    dataRowInfo.Cells(colEmptyValueShell).Value = emptydr.Rows(0)("Empty_Value_Shell")
                    dblBasicPrice = clsCommon.myCdbl(emptydr.Rows(0)("Item_Basic_Price"))
                    strStartDate = clsCommon.GetPrintDate(emptydr.Rows(0)("Start_Date"), "dd/MM/yyyy")
                    dataRowInfo.Cells(ColPriceToShow).Value = emptydr.Rows(0)("PriceToShow")

                    dataRowInfo.Cells(colAbatementRate).Value = clsCommon.myCdbl(emptydr.Rows(0)("Abatement_Rate"))
                End If

                sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + Convert.ToString(tdr("Scheme_Item_Code")) + "' AND " & _
                    " Uom_Code='" + Convert.ToString(tdr("UOM")) + "'"
                Dim convertFact As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                If Not convertFact = 0 Then
                    Dim shells As Integer
                    Dim bottles As Decimal = clsCommon.myCdbl(tdr("Qty")) * disRatio Mod convertFact
                    If bottles = 0 Then
                        shells = (clsCommon.myCdbl(tdr("Qty")) - bottles) / convertFact
                        dataRowInfo.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueBottle).Value) * (shells) * convertFact + clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueBottle).Value) * bottles, 2)
                    Else
                        shells = (clsCommon.myCdbl(grow.Cells(colShippedQty).Value) - bottles) / convertFact + 1
                        dataRowInfo.Cells(colEmptyValue).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueBottle).Value) * (shells - 1) * convertFact + clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueShell).Value) * shells + clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueBottle).Value) * bottles, 2)
                    End If
                    If bottles = 0 And shells = 0 Then
                        dataRowInfo.Cells(colEmptyValue).Value = clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueBottle).Value) + clsCommon.myCdbl(dataRowInfo.Cells(colEmptyValueShell).Value)
                    End If

                End If
                dataRowInfo.Cells(colFromSchemeCode).Value = grow.Cells(schemeCodeCol).Value
                dataRowInfo.Cells(ColComplete).Value = "Yes"
                dataRowInfo.Cells(ColICode).Value = tdr(0).ToString()
                Dim shpQty As Decimal = tdr(2).ToString()
                dataRowInfo.Cells(ColItemName).Value = tdr(1).ToString()
                If clsCommon.myLen(strStartDate) <= 0 Then
                    Throw New Exception("Scheme can't be Applied")
                Else
                    dataRowInfo.Cells(colPriceDateColumn).Value = strStartDate
                End If

                dataRowInfo.Cells(colPriceCode).Value = txtPriceCode.Text
                dataRowInfo.Cells("orderedQty").ReadOnly = True
                dataRowInfo.Cells(colOrderedQty).Value = 0
                dataRowInfo.Cells("shippedQty").ReadOnly = True
                dataRowInfo.Cells("balanceQty").ReadOnly = True
                dataRowInfo.Cells(colBalanceQty).Value = 0
                dataRowInfo.Cells(colUnitCode).Value = tdr(3).ToString()
                Dim emptyshell1 As Decimal = clsDBFuncationality.getSingleValue("select Empty_Value_Shell   from TSPL_ITEM_PRICE_MASTER where Item_Code =  '" + dataRowInfo.Cells(ColICode).Value + "' AND UOM = '" + dataRowInfo.Cells(colUnitCode).Value + "'")
                Dim emptybottle1 As Decimal = clsDBFuncationality.getSingleValue("select Empty_Value_Bottle  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + dataRowInfo.Cells(ColICode).Value + "' AND UOM = '" + dataRowInfo.Cells(colUnitCode).Value + "'")
                Dim conversion As Decimal = clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + dataRowInfo.Cells(ColICode).Value + "' AND UOM_Code  = '" + dataRowInfo.Cells(colUnitCode).Value + "'")
                dataRowInfo.Cells(collocation).Value = txtLocation.Value

                If schemeType = "Q" Then
                    dataRowInfo.Cells(colSchemeItem).Value = "Yes"
                    dataRowInfo.Cells(colPromoSchemeItem).Value = "No"
                Else
                    dataRowInfo.Cells(colSchemeItem).Value = "No"
                    dataRowInfo.Cells(colPromoSchemeItem).Value = "Yes"
                End If
                dataRowInfo.Cells(colPromoSchemeCode).Value = ""
                dataRowInfo.Cells(colSchemeCodeItem).Value = ""
                dataRowInfo.Cells(colSchemeApplicable).Value = "No"
                dataRowInfo.Cells(colPromoSchemeApplicable).Value = "No"
                dataRowInfo.Cells(colSchemeDiscountApplicable).Value = "No"
                dataRowInfo.Cells(colSchemeCodeDiscount).Value = ""
                dataRowInfo.Cells(colSampleItem).Value = "No"
                dataRowInfo.Cells(colTPT).Value = "0"
                dataRowInfo.Cells(colBasicAmount).Value = dblBasicPrice
                dataRowInfo.Cells(colTransferBasicAmount).Value = dblBasicPrice
                dataRowInfo.Cells(colDiscountAmount).Value = "0"
                dataRowInfo.Cells(colitemNetAmount).Value = "0"
                dataRowInfo.Cells(colcustDiscount).Value = 0
                dataRowInfo.Cells(colTotalCustDiscount).Value = 0
                dataRowInfo.Cells(colMRP).Value = clsCommon.myCdbl(tdr(4))
                dataRowInfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(dataRowInfo.Cells(ColICode).Value), clsCommon.myCstr(dataRowInfo.Cells(colUnitCode).Value), clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value))
                Dim assessibleamt As Decimal = Math.Round(clsCommon.myCdbl(tdr(4).ToString()) * abatement(dataRowInfo, Nothing) / 100, 2)
                sql = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"
                Dim strexcisable As String = clsDBFuncationality.getSingleValue(sql)
                If strexcisable = "T" Then
                    dataRowInfo.Cells(colMRP).Value = clsCommon.myCdbl(tdr(4))
                    dataRowInfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(dataRowInfo.Cells(ColICode).Value), clsCommon.myCstr(dataRowInfo.Cells(colUnitCode).Value), clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value))

                    dataRowInfo.Cells(colTaxamount).Value = 0
                    Dim dr As DataRow = clsTaxMaster.GetExcisableTaxRates(Convert.ToString(dataRowInfo.Cells(ColICode).Value), clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value), CDate(strStartDate).ToString("yyyy-MM-dd"), clsCommon.myCdbl(dataRowInfo.Cells(colBasicAmount).Value), Convert.ToString(dataRowInfo.Cells(colUnitCode).Value), fndTaxGroup.txtValue.Text, txtPriceCode.Text)
                    If dr IsNot Nothing AndAlso dr.ItemArray.Length > 0 Then
                        dataRowInfo.Cells(colTax1Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax1Rate")), 2)
                        dataRowInfo.Cells(colAssess1).Value = Math.Round(clsCommon.myCdbl(dr("Tax1BaseAmt")), 2)
                        dataRowInfo.Cells(colTax1Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax1Amt")), 4, MidpointRounding.ToEven)

                        dataRowInfo.Cells(colTax2Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax2Rate")), 2)
                        dataRowInfo.Cells(colAssess2).Value = Math.Round(clsCommon.myCdbl(dr("Tax2BaseAmt")), 2)
                        dataRowInfo.Cells(colTax2Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax2Amt")), 4, MidpointRounding.ToEven)

                        dataRowInfo.Cells(colTax3Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax3Rate")), 2)
                        dataRowInfo.Cells(colAssess3).Value = Math.Round(clsCommon.myCdbl(dr("Tax3BaseAmt")), 2)
                        dataRowInfo.Cells(colTax3Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax3Amt")), 4, MidpointRounding.ToEven)

                        dataRowInfo.Cells(colTax4Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax4Rate")), 2)
                        dataRowInfo.Cells(colAssess4).Value = Math.Round(clsCommon.myCdbl(dr("Tax4BaseAmt")), 2)
                        dataRowInfo.Cells(colTax4Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax4Amt")), 4, MidpointRounding.ToEven)

                        dataRowInfo.Cells(colTax5Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax5Rate")), 2)
                        dataRowInfo.Cells(colAssess5).Value = Math.Round(clsCommon.myCdbl(dr("Tax5BaseAmt")), 2)
                        dataRowInfo.Cells(colTax5Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax5Amt")), 4, MidpointRounding.ToEven)

                        dataRowInfo.Cells(colTax6Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax6Rate")), 2)
                        dataRowInfo.Cells(colAssess6).Value = Math.Round(clsCommon.myCdbl(dr("Tax6BaseAmt")), 2)
                        dataRowInfo.Cells(colTax6Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax6Amt")), 4, MidpointRounding.ToEven)
                    Else
                        dataRowInfo.Cells(colTax1Rate).Value = 0
                        dataRowInfo.Cells(colAssess1).Value = 0
                        dataRowInfo.Cells(colTax1Amt).Value = 0

                        dataRowInfo.Cells(colTax2Rate).Value = 0
                        dataRowInfo.Cells(colAssess2).Value = 0
                        dataRowInfo.Cells(colTax2Amt).Value = 0

                        dataRowInfo.Cells(colTax3Rate).Value = 0
                        dataRowInfo.Cells(colAssess3).Value = 0
                        dataRowInfo.Cells(colTax3Amt).Value = 0

                        dataRowInfo.Cells(colTax4Rate).Value = 0
                        dataRowInfo.Cells(colAssess4).Value = 0
                        dataRowInfo.Cells(colTax4Amt).Value = 0

                        dataRowInfo.Cells(colTax5Rate).Value = 0
                        dataRowInfo.Cells(colAssess5).Value = 0
                        dataRowInfo.Cells(colTax5Amt).Value = 0

                        dataRowInfo.Cells(colTax6Rate).Value = 0
                        dataRowInfo.Cells(colAssess6).Value = 0
                        dataRowInfo.Cells(colTax6Amt).Value = 0
                    End If

                Else
                    dataRowInfo.Cells(colTaxamount).Value = 0
                    dataRowInfo.Cells(colMRP).Value = clsCommon.myCdbl(tdr(4))
                    dataRowInfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(dataRowInfo.Cells(ColICode).Value), clsCommon.myCstr(dataRowInfo.Cells(colUnitCode).Value), clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value))

                    For Each gr As GridViewRowInfo In gvTax.Rows
                        dataRowInfo.Cells("Tax" + (gr.Index + 1).ToString() + "Rate").Value = 0
                        dataRowInfo.Cells("assess" + (gr.Index + 1).ToString()).Value = 0
                        dataRowInfo.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value = 0
                    Next
                End If

                dataRowInfo.Cells(colTotalBasicAmount).Value = "0"
                dataRowInfo.Cells(colTotalDiscountAmount).Value = "0"
                dataRowInfo.Cells(colTotalNetAmount).Value = "0"
                dataRowInfo.Cells(colTotalTPT).Value = "0"
                dataRowInfo.Cells("shippedQty").ReadOnly = True
                dataRowInfo.Cells(colShippedQty).Value = shpQty * disRatio
                dataRowInfo.Cells(colEmptyValue).Value = dataRowInfo.Cells(colEmptyValueBottle).Value * dataRowInfo.Cells(colShippedQty).Value

                Dim dblConvFact As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(dataRowInfo.Cells(ColICode).Value), clsCommon.myCstr(dataRowInfo.Cells(colUnitCode).Value), Nothing)
                Dim dblmrp As Double = clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value)

                If dblConvFact <> 1 Then
                    dblmrp = dblmrp * dblConvFact
                End If


                dataRowInfo.Cells(colBatchNumber).Value = clsDBFuncationality.getSingleValue("select batch_no from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + dataRowInfo.Cells(ColICode).Value + "'  AND location_code='" + txtLocation.Value + "' AND MRP = '" + clsCommon.myCstr(dblmrp) + "'")




                dataRowInfo.Cells(colTotalTaxAmount).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colShippedQty).Value) * clsCommon.myCdbl(dataRowInfo.Cells(colTaxamount).Value), 2)
                dataRowInfo.Cells(colTotalMRP).Value = Math.Round(clsCommon.myCdbl(dataRowInfo.Cells(colShippedQty).Value) * clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value), 2)
                dataRowInfo.Cells(colTotalItemAmount).Value = dataRowInfo.Cells(colTotalTaxAmount).Value
                dataRowInfo.Cells(schemeCodeCol).Value = grow.Cells(schemeCodeCol).Value
                dataRowInfo.Cells(colMainItem).Value = strICode
                gv1.Rows.Insert(i, dataRowInfo)

                i = i + 1
            Next

            For Each gr As GridViewRowInfo In gvTax.Rows
                Dim assess As Decimal = 0
                Dim taxAmt As Decimal = 0
                For Each gr1 As GridViewRowInfo In gv1.Rows
                    If Not clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) = 0 Then
                        If gr1.Cells(colSchemeItem).Value = "No" And gr1.Cells(colSampleItem).Value = "No" And gr1.Cells(colPromoSchemeItem).Value = "No" Then
                            assess = assess + clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) * clsCommon.myCdbl(gr1.Cells("assess" + (gr.Index + 1).ToString()).Value)
                            taxAmt = taxAmt + clsCommon.myCdbl(gr1.Cells(colShippedQty).Value) * clsCommon.myCdbl(gr1.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value)
                        End If
                    End If
                Next
                If assess = 0 AndAlso taxAmt = 0 Then
                    gr.Cells("assessibleAmount").Value = assess
                    gr.Cells(colTaxamount).Value = taxAmt
                End If
                If assess <> 0 Then
                    gr.Cells("taxRate").Value = Math.Round(taxAmt * 100 / assess, 2)
                    gr.Cells("assessibleAmount").Value = assess
                    gr.Cells(colTaxamount).Value = taxAmt
                End If
            Next

            Dim netAmount As Decimal = 0.0
            Dim totalTPT As Decimal = 0.0
            Dim ttlCustDiscount As Decimal = 0
            Dim dblBasicAmt As Double = 0
            For Each gro As GridViewRowInfo In gv1.Rows
                If gro.Cells(colShippedQty).Value <> 0 Then
                    If gro.Cells(colSchemeItem).Value = "No" And gro.Cells(colPromoSchemeItem).Value = "No" Then
                        dblBasicAmt = dblBasicAmt + clsCommon.myCdbl(gro.Cells(colTotalBasicAmount).Value)
                        netAmount = netAmount + clsCommon.myCdbl(gro.Cells(colTotalNetAmount).Value)
                        totalTPT = totalTPT + clsCommon.myCdbl(gro.Cells(colTotalTPT).Value)
                        ttlCustDiscount = ttlCustDiscount + clsCommon.myCdbl(gro.Cells(colTotalCustDiscount).Value)
                    End If
                End If
            Next
            txtShipmentTotal.Text = clsCommon.myFormat(dblBasicAmt)
            txtCustDisc.Text = ttlCustDiscount

            Dim totalTax As Decimal = 0.0
            For Each gr As GridViewRowInfo In gvTax.Rows
                totalTax = totalTax + clsCommon.myCdbl(gr.Cells(5).Value)
            Next
            txtTotalTaxAmount.Text = totalTax
            txtShipmentAmt.Text = netAmount + totalTax + totalTPT

        Else

        End If
        Dim totalfc As Decimal = 0
        Dim totalfb As Decimal = 0
        For Each g As GridViewRowInfo In gv1.Rows
            If clsCommon.myCdbl(g.Cells(colShippedQty).Value) <> 0 Then
                If g.Cells(colUnitCode).Value = "FC" Then
                    totalfc = totalfc + clsCommon.myCdbl(g.Cells(colShippedQty).Value)
                End If
                If g.Cells(colUnitCode).Value = "FB" Then
                    totalfb = totalfb + clsCommon.myCdbl(g.Cells(colShippedQty).Value)
                End If
            End If
        Next
        If totalfc = 0 Then
            lblfb.Text = CStr(totalfb)
        ElseIf totalfb = 0 Then
            lblfc.Text = CStr(totalfc)
        ElseIf totalfb <> 0 And totalfc <> 0 Then
            lblfb.Text = CStr(totalfb)
            lblfc.Text = CStr(totalfc)
        ElseIf totalfb = 0 And totalfc = 0 Then
            lblfc.Text = 0
            lblfb.Text = 0
        End If

    End Sub

    Private Function validateData() As Boolean

        Dim shipQty As Integer = 0
        Dim location As String = ""
        Dim unitCode As String = ""
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myCdbl(grow.Cells(colShippedQty).Value) > 0 Then
                shipQty = shipQty + CInt(grow.Cells(colShippedQty).Value)
                unitCode = grow.Cells(colUnitCode).Value
                location = grow.Cells(collocation).Value
                If location = "" Then
                    Throw New Exception("Location for item " + grow.Cells(ColICode).Value + " can not be left blank.")
                ElseIf unitCode = "" Then
                    Throw New Exception("Unit Code for item " + grow.Cells(ColICode).Value + " can not be left blank.")
                End If

            End If
        Next
        If txtCustomer.Value = String.Empty Then
            txtCustomer.Focus()
            Throw New Exception("Customer  can not be left blank.")
        ElseIf txtTripNo.Text = String.Empty Then
            txtTripNo.Focus()
            Throw New Exception("Trip No can not be left blank.")
        ElseIf txtSalesman.Value = String.Empty Then
            txtSalesman.Focus()
            Throw New Exception("Salesman can not be left blank.")
        ElseIf txtLocation.Value = String.Empty Then
            txtLocation.Focus()
            Throw New Exception("Location can not be left blank.")
        ElseIf txtVehicleCode.Value = String.Empty Then
            txtVehicleCode.Focus()
            Throw New Exception("Vehicle can not be left blank.")
        ElseIf txtPaymentTerm.Value = String.Empty Then
            txtPaymentTerm.Focus()
            Throw New Exception("Payment Terms can not be left blank.")
        ElseIf fndTaxGroup.txtValue.Text = String.Empty Then
            fndTaxGroup.txtValue.Focus()
            Throw New Exception("Tax Group can not be left blank.")
        ElseIf cboModeOfTransport.Text = "Select" Then
            cboModeOfTransport.Focus()
            Throw New Exception("Please choose a mode of transport from the list.")
        ElseIf cboPriceDate.Text = "Select" Then
            cboModeOfTransport.Focus()
            Throw New Exception("Please choose a price date from the list.")
        ElseIf cboLoadOutType.Text = "Select" Then
            cboLoadOutType.Focus()
            Throw New Exception("Please choose a loadout type from the list.")
        ElseIf shipQty = 0 Then
            Throw New Exception("There is no item to ship.")
        End If

        If chkSample.Checked = True Then
            If txtSchemeSample.txtValue.Text = String.Empty Then
                Throw New Exception("Sample Scheme can not be left blank.")
            End If
        End If
        If cboLoadOutType.SelectedIndex = 0 Then
            If txtKMReading.Text = String.Empty Then
                txtKMReading.Focus()
                Throw New Exception("KM Reading can not be left blank.")
            End If
        Else
            Return True
        End If
        Return True
    End Function

    Private Function isAllTargetItem() As Boolean
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myCdbl(gv1.Rows(ii).Cells(colShippedQty).Value) > 0 Then
                If clsCommon.myLen(gv1.Rows(ii).Cells(colDiscountCode).Value) <= 0 Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Private Function GetRecoverableAmt(ByRef dblTaxRecAmt As Double, ByRef dblTaxRecAmt2 As Double, ByRef dblTaxRecAmt3 As Double) As Double
        Dim dblTotDisPer As Double = 0

        If isAllTargetItem() Then
            dblTotDisPer = 100
        Else
            If clsCommon.myCdbl(txtShipmentTotal.Text) = 0 Then
                dblTotDisPer = 0
            Else
                dblTotDisPer = (clsCommon.myCdbl(txtCustDisc.Text) + clsCommon.myCdbl(txtDiscAmt.Text)) * 100 / clsCommon.myCdbl(txtShipmentTotal.Text)
            End If
        End If

        dblTotDisPer = Math.Round(dblTotDisPer, 2, MidpointRounding.ToEven)
        Dim dblTax1Amt As Double = 0
        Dim dblTax2Amt As Double = 0
        Dim dblTax3Amt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblTax1Amt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1Amt).Value) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colShippedQty).Value)
            dblTax2Amt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax2Amt).Value) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colShippedQty).Value)
            dblTax3Amt += clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax3Amt).Value) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colShippedQty).Value)
        Next
        dblTaxRecAmt = dblTax1Amt - ((dblTotDisPer * dblTax1Amt) / 100)
        dblTaxRecAmt = Math.Round(dblTaxRecAmt, 2, MidpointRounding.ToEven)

        dblTaxRecAmt2 = dblTax2Amt - ((dblTotDisPer * dblTax2Amt) / 100)
        dblTaxRecAmt2 = Math.Round(dblTaxRecAmt2, 2, MidpointRounding.ToEven)

        dblTaxRecAmt3 = dblTax3Amt - ((dblTotDisPer * dblTax3Amt) / 100)
        dblTaxRecAmt3 = Math.Round(dblTaxRecAmt3, 2, MidpointRounding.ToEven)

        Return dblTotDisPer
    End Function

    Private Function IsRouteJumped(ByVal trans As SqlTransaction) As Integer
        Dim RouteJumbed As Integer = 0
        Dim qry As String = ""
        If clsCommon.myLen(txtTransferNo.Value) > 0 Then
            qry = "select Route_No  from TSPL_TRANSFER_HEAD where Transfer_No='" + txtTransferNo.Value + "'"
            Dim strOrgRoute As String = clsDBFuncationality.getSingleValue(qry, trans)
            If Not clsCommon.CompairString(strOrgRoute, txtRouteNo.Value) = CompairStringResult.Equal Then
                RouteJumbed = 1
            End If
        End If
        Return RouteJumbed
    End Function

    Private Sub insertData(ByVal mrp As Decimal, ByVal basic As Decimal, ByVal assessible As Decimal, ByVal detailDiscount As Decimal, ByVal shipmentDiscAmt As Decimal, ByVal shipmentDiscPer As Decimal, ByVal shipmentTaxAmt As Decimal, ByVal netAmount As Decimal, ByVal additionalCharges As Decimal, ByVal OtherCharges As Decimal, ByVal freightCharges As Decimal, ByVal totalTPT As Decimal, ByVal onHold As String)
        Dim isShowLastMessage = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim emptyvalue As Decimal = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                If Not clsCommon.myCdbl(grow.Cells(colShippedQty).Value) = 0 Then
                    emptyvalue = emptyvalue + grow.Cells(colEmptyValue).Value
                End If
            Next
            Dim totaltaxamt As Decimal = 0
            For Each g As GridViewRowInfo In gvTax.Rows
                totaltaxamt = totaltaxamt + clsCommon.myCdbl(g.Cells(colTaxamount).Value)
            Next
            txtTotalTaxAmount.Text = totaltaxamt
            emptyvalue = clsCommon.myCdbl(txtshellqty.Text) * 100 + clsCommon.myCdbl(emptyvalue)
            emptyvalue123 = emptyvalue


            Dim priceDate As Date = CDate(cboPriceDate.Text).ToString("dd/MMM/yyyy")

            Dim dblTaxRecAmt As Double = 0
            Dim dblTaxRecAmt2 As Double = 0
            Dim dblTaxRecAmt3 As Double = 0
            Dim dblTotDisPer As Double = GetRecoverableAmt(dblTaxRecAmt, dblTaxRecAmt2, dblTaxRecAmt3)

            If isNewEntry AndAlso clsCommon.myLen(txtMannaulInvoiceNo.Text) > 0 Then
                lblSaleInvoiceNo.Text = txtMannaulInvoiceNo.Text
            End If


            Dim obj As New clsShipmentMaster()
            obj.Order_No = txtOrderNo.Value
            obj.Order_Date = txtOrderDate.Value
            obj.Shipment_No = txtDocNo.Value
            txtDate.Value = New DateTime(txtDate.Value.Year, txtDate.Value.Month, txtDate.Value.Day, txtRemovalTime.Value.Hour, txtRemovalTime.Value.Minute, txtRemovalTime.Value.Second)
            obj.Shipment_Date = txtDate.Value '' New DateTime(txtDate.Value.Year, txtDate.Value.Month, txtDate.Value.Day, txtRemovalTime.Value.Hour, txtRemovalTime.Value.Minute, txtRemovalTime.Value.Second)
            obj.Date_Time_Removal = txtDate.Value
            obj.Cust_Code = txtCustomer.Value
            obj.Cust_Name = txtCustomerName.Text
            obj.Cust_PONo = txtCustomerPONO.Text
            obj.Expected_Ship_Date = txtExpectedShipDate.Value
            obj.Status = "Open"
            obj.On_Hold = onHold
            obj.Multiple_Orders = "N"
            obj.Ref_No = txtRef.Text
            obj.Description = txtDesc.Text
            obj.Remarks = txtRemarks.Text
            obj.Price_Code = txtPriceCode.Text
            obj.Tax_Group = fndTaxGroup.txtValue.Text
            obj.Location = txtLocation.Value
            obj.Cust_Account = strCustAccount

            obj.Is_Create_Empty = chkCreateEmpty.Checked
            obj.Shell_Qty = clsCommon.myCdbl(txtshellqty.Text)
            If (gvTax.Rows.Count > 0) Then
                obj.TAX1 = clsCommon.myCstr(gvTax.Rows(0).Cells("taxAuthority").Value)
                obj.TAX1_Rate = clsCommon.myCdbl(gvTax.Rows(0).Cells("taxRate").Value)
                obj.TAX1_Assessable_Amt = clsCommon.myCdbl(gvTax.Rows(0).Cells("assessibleAmount").Value)
                obj.TAX1_Amt = clsCommon.myCdbl(gvTax.Rows(0).Cells(colTaxamount).Value)
            End If
            If (gvTax.Rows.Count > 1) Then
                obj.TAX2 = clsCommon.myCstr(gvTax.Rows(1).Cells("taxAuthority").Value)
                obj.TAX2_Rate = clsCommon.myCdbl(gvTax.Rows(1).Cells("taxRate").Value)
                obj.TAX2_Assessable_Amt = clsCommon.myCdbl(gvTax.Rows(1).Cells("assessibleAmount").Value)
                obj.TAX2_Amt = clsCommon.myCdbl(gvTax.Rows(1).Cells(colTaxamount).Value)
            End If
            If (gvTax.Rows.Count > 2) Then
                obj.TAX3 = clsCommon.myCstr(gvTax.Rows(2).Cells("taxAuthority").Value)
                obj.TAX3_Rate = clsCommon.myCdbl(gvTax.Rows(2).Cells("taxRate").Value)
                obj.TAX3_Assessable_Amt = clsCommon.myCdbl(gvTax.Rows(2).Cells("assessibleAmount").Value)
                obj.TAX3_Amt = clsCommon.myCdbl(gvTax.Rows(2).Cells(colTaxamount).Value)
            End If
            If (gvTax.Rows.Count > 3) Then
                obj.TAX4 = clsCommon.myCstr(gvTax.Rows(3).Cells("taxAuthority").Value)
                obj.TAX4_Rate = clsCommon.myCdbl(gvTax.Rows(3).Cells("taxRate").Value)
                obj.TAX4_Assessable_Amt = clsCommon.myCdbl(gvTax.Rows(3).Cells("assessibleAmount").Value)
                obj.TAX4_Amt = clsCommon.myCdbl(gvTax.Rows(3).Cells(colTaxamount).Value)
            End If
            If (gvTax.Rows.Count > 4) Then
                obj.TAX5 = clsCommon.myCstr(gvTax.Rows(4).Cells("taxAuthority").Value)
                obj.TAX5_Rate = clsCommon.myCdbl(gvTax.Rows(4).Cells("taxRate").Value)
                obj.TAX5_Assessable_Amt = clsCommon.myCdbl(gvTax.Rows(4).Cells("assessibleAmount").Value)
                obj.TAX5_Amt = clsCommon.myCdbl(gvTax.Rows(4).Cells(colTaxamount).Value)
            End If
            If (gvTax.Rows.Count > 5) Then
                obj.TAX6 = clsCommon.myCstr(gvTax.Rows(5).Cells("taxAuthority").Value)
                obj.TAX6_Rate = clsCommon.myCdbl(gvTax.Rows(5).Cells("taxRate").Value)
                obj.TAX6_Assessable_Amt = clsCommon.myCdbl(gvTax.Rows(5).Cells("assessibleAmount").Value)
                obj.TAX6_Amt = clsCommon.myCdbl(gvTax.Rows(5).Cells(colTaxamount).Value)
            End If
            If (gvTax.Rows.Count > 6) Then
                obj.TAX7 = clsCommon.myCstr(gvTax.Rows(6).Cells("taxAuthority").Value)
                obj.TAX7_Rate = clsCommon.myCdbl(gvTax.Rows(6).Cells("taxRate").Value)
                obj.TAX7_Assessable_Amt = clsCommon.myCdbl(gvTax.Rows(6).Cells("assessibleAmount").Value)
                obj.TAX7_Amt = clsCommon.myCdbl(gvTax.Rows(6).Cells(colTaxamount).Value)
            End If
            If (gvTax.Rows.Count > 7) Then
                obj.TAX8 = clsCommon.myCstr(gvTax.Rows(7).Cells("taxAuthority").Value)
                obj.TAX8_Rate = clsCommon.myCdbl(gvTax.Rows(7).Cells("taxRate").Value)
                obj.TAX8_Assessable_Amt = clsCommon.myCdbl(gvTax.Rows(7).Cells("assessibleAmount").Value)
                obj.TAX8_Amt = clsCommon.myCdbl(gvTax.Rows(7).Cells(colTaxamount).Value)
            End If
            If (gvTax.Rows.Count > 8) Then
                obj.TAX9 = clsCommon.myCstr(gvTax.Rows(8).Cells("taxAuthority").Value)
                obj.TAX9_Rate = clsCommon.myCdbl(gvTax.Rows(8).Cells("taxRate").Value)
                obj.TAX9_Assessable_Amt = clsCommon.myCdbl(gvTax.Rows(8).Cells("assessibleAmount").Value)
                obj.TAX9_Amt = clsCommon.myCdbl(gvTax.Rows(8).Cells(colTaxamount).Value)
            End If
            If (gvTax.Rows.Count > 9) Then
                obj.TAX10 = clsCommon.myCstr(gvTax.Rows(9).Cells("taxAuthority").Value)
                obj.TAX10_Rate = clsCommon.myCdbl(gvTax.Rows(9).Cells("taxRate").Value)
                obj.TAX10_Assessable_Amt = clsCommon.myCdbl(gvTax.Rows(9).Cells("assessibleAmount").Value)
                obj.TAX10_Amt = clsCommon.myCdbl(gvTax.Rows(9).Cells(colTaxamount).Value)
            End If
            If gvTax.Rows.Count > 10 Then
                Throw New Exception("System not support more than 10 tax")
            End If

            obj.Total_Assessable_Amount = assessible
            obj.Shipment_Disc_Percent = shipmentDiscPer
            obj.Shipment_Discount_Amt = shipmentDiscAmt
            obj.Shipment_Detail_Disc_Amt = detailDiscount
            obj.Shipment_Detail_Total_Amt = clsCommon.myCdbl(txtNetShipAmt.Text)
            obj.Shipment_Tax_Amt = shipmentTaxAmt
            obj.Freight_Amt = freightCharges
            obj.Other_Charges = OtherCharges
            obj.Add_Charges = additionalCharges
            obj.Total_Order_Amt = txtShipmentAmt.Text
            obj.Salesman_Code = txtSalesman.Value
            obj.Mode_Of_Transport = cboModeOfTransport.Text
            obj.Vehicle_Code = txtVehicleCode.Value
            obj.Vehicle_No = lblVhicleNo.Text
            obj.KM_Reading = txtKMReading.Text
            obj.Route_No = txtRouteNo.Value
            obj.Route_Desc = lblRouteDesc.Text
            obj.Trip_No = txtTripNo.Text
            obj.Scheme_Sample_Code = txtSchemeSample.txtValue.Text
            obj.Price_Date = priceDate
            obj.Terms_Code = txtPaymentTerm.Value
            obj.Comments = txtRemarks.Text
            obj.Discount_On = IIf(chkDiscountOnRate.IsChecked, 0, 1)
            obj.Level1_User_code = l1User
            obj.Level2_User_code = l2User
            obj.Level3_User_code = l3User
            obj.Level4_User_code = l4User
            obj.Level5_User_code = l5User

            obj.Shipment_Type = cboLoadOutType.Text
            obj.Is_Post = "N"
            obj.Total_TPT = txtTotalTPT.Text
            obj.Transfer_No = txtTransferNo.Value
            obj.Transfer_Date = txtTransferDate.Value
            'obj.Level1_User_Commission=
            'obj.Level2_User_Commission=
            'obj.Level3_User_Commission=
            'obj.Level4_User_Commission=
            'obj.Level5_User_Commission=
            obj.Empty_Value = emptyvalue
            'obj.Is_Complete=
            'obj.Shell_Qty=

            obj.Customer_Invoice_No = txtcustomerinvoiceno.Text
            'obj.Employee_Code=
            obj.is_Sample = IIf(chkSample.Checked, 1, 0)
            obj.Total_Disc_Percent = dblTotDisPer
            obj.Tax_Recoverable_Amt = dblTaxRecAmt
            obj.Tax_Recoverable_Amt2 = dblTaxRecAmt2
            obj.Tax_Recoverable_Amt3 = dblTaxRecAmt3
            obj.Tot_Customer_Dis_Amt = clsCommon.myCdbl(txtCustDisc.Text)
            obj.is_Route_Jumped = IsRouteJumped(trans)
            obj.Is_Printed = 0
            'obj.Verify_By=
            obj.Ship_To = txtShipTo.Value
            obj.Ship_To_Desc = lblShipTo.Text

            sql = "select [" + clsCommon.myCstr(Weekday(txtDate.Value)) + "] as IsScheduled from("
            sql += "select Sunday as [1] ,Monday as [2] ,Tuesday as [3],Wednesday as [4],Thursday as [5] ,Friday as [6] ,Saturday as [7]  from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_GROUP_MASTER on TSPL_ROUTE_GROUP_MASTER.Group_Id=TSPL_CUSTOMER_MASTER.Route_Group where TSPL_CUSTOMER_MASTER.Cust_Code='" + txtCustomer.Value + "' )xxx"
            obj.Is_Scheduled = IIf(clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql, trans)), "T") = CompairStringResult.Equal, 1, 0)
            obj.Against_C_Form = chkAgainstCForm.Checked
            obj.Transaction_Type = clsCommon.myCstr(CmbTransaction.SelectedValue)
            obj.Mannual_Invoice_Amt = txtMannualAmt.Value
            sql = "select Type from TSPL_ROUTE_MASTER where Route_No='" + txtRouteNo.Value + "'"
            obj.Route_Type_Id = clsDBFuncationality.getSingleValue(sql, trans)
            obj.Mannual_Invoice_Qty = txtMannualQty.Value
            obj.TAX_GROUP_TYPE = "S"
            obj.Invoice_No = lblSaleInvoiceNo.Text

            obj.Arr = New List(Of clsShipmentDetail)
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(grow.Cells(colShippedQty).Value) > 0 OrElse clsCommon.myLen(txtOrderNo.Value) > 0 Then
                    Dim objtr As New clsShipmentDetail()
                    objtr.Complete = complete(grow)
                    objtr.Item_Code = clsCommon.myCstr(grow.Cells(ColICode).Value)
                    objtr.Item_Desc = clsCommon.myCstr(grow.Cells(ColItemName).Value)
                    objtr.Price_Date = clsCommon.myCDate(grow.Cells(colPriceDateColumn).Value)
                    objtr.Order_Qty = clsCommon.myCdbl(grow.Cells(colOrderedQty).Value)
                    objtr.Shipped_Qty = clsCommon.myCdbl(grow.Cells(colShippedQty).Value)
                    objtr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colShippedQty).Value)
                    objtr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    objtr.Location = obj.Location
                    objtr.Price_code = clsCommon.myCstr(grow.Cells(colPriceCode).Value)
                    objtr.Scheme_Applicable = schemeItemApplicable(grow)
                    objtr.Scheme_Code_Qty = clsCommon.myCstr(grow.Cells(colSchemeCodeItem).Value)
                    objtr.Scheme_Item = schemeItem(grow)
                    objtr.Promo_Scheme_Applicable = promoSchemeApplicable(grow)
                    objtr.Promo_Scheme_Code = clsCommon.myCstr(grow.Cells(colPromoSchemeCode).Value)
                    objtr.Promo_Scheme_Item = promoSchemeItem(grow)
                    objtr.Scheme_Disc_Applicable = schemeDiscApplicable(grow)
                    objtr.Scheme_Code_Cash = clsCommon.myCstr(grow.Cells(colSchemeCodeDiscount).Value)
                    objtr.Sampling_Item = sampleItem(grow)
                    objtr.Empty_Value = clsCommon.myCdbl(grow.Cells(colEmptyValue).Value)
                    objtr.MRP_Amt = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                    objtr.Basic_Rate = clsCommon.myCdbl(grow.Cells(colBasicAmount).Value)
                    objtr.Item_Assessable_Rate = itemAssessibleAmt(grow, trans)
                    objtr.Disc_Amt = clsCommon.myCdbl(grow.Cells(colDiscountAmount).Value)
                    objtr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colitemNetAmount).Value)

                    'If Not (clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal Or clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) Then
                    If (gvTax.Rows.Count > 0) Then
                        objtr.TAX1 = Convert.ToString(gvTax.Rows(0).Cells("taxAuthority").Value)
                        objtr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTax1Rate).Value)
                        objtr.TAX1_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colAssess1).Value)
                        objtr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTax1Amt).Value)
                    End If
                    If (gvTax.Rows.Count > 1) Then
                        objtr.TAX2 = Convert.ToString(gvTax.Rows(1).Cells("taxAuthority").Value)
                        objtr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTax2Rate).Value)
                        objtr.TAX2_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colAssess2).Value)
                        objtr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTax2Amt).Value)
                    End If
                    If (gvTax.Rows.Count > 2) Then
                        objtr.TAX3 = Convert.ToString(gvTax.Rows(2).Cells("taxAuthority").Value)
                        objtr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTax3Rate).Value)
                        objtr.TAX3_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colAssess3).Value)
                        objtr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTax3Amt).Value)
                    End If
                    If (gvTax.Rows.Count > 3) Then
                        objtr.TAX4 = Convert.ToString(gvTax.Rows(3).Cells("taxAuthority").Value)
                        objtr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTax4Rate).Value)
                        objtr.TAX4_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colAssess4).Value)
                        objtr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTax4Amt).Value)
                    End If
                    If (gvTax.Rows.Count > 4) Then
                        objtr.TAX5 = Convert.ToString(gvTax.Rows(4).Cells("taxAuthority").Value)
                        objtr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTax5Rate).Value)
                        objtr.TAX5_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colAssess5).Value)
                        objtr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTax5Amt).Value)
                    End If
                    If (gvTax.Rows.Count > 5) Then
                        objtr.TAX6 = Convert.ToString(gvTax.Rows(5).Cells("taxAuthority").Value)
                        objtr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTax6Rate).Value)
                        objtr.TAX6_Assessable_Amt = clsCommon.myCdbl(grow.Cells(colAssess6).Value)
                        objtr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTax6Amt).Value)
                    End If
                    'End If
                    objtr.Item_Tax = clsCommon.myCdbl(grow.Cells(colTaxamount).Value)
                    objtr.Total_Assessable_Amt = totalItemAssessibleAmt(grow, trans)
                    objtr.Total_MRP_Amt = clsCommon.myCdbl(grow.Cells(colTotalMRP).Value)
                    objtr.Total_Basic_Amt = clsCommon.myCdbl(grow.Cells(colTotalBasicAmount).Value)
                    objtr.Total_Disc_Amt = clsCommon.myCdbl(grow.Cells(colTotalDiscountAmount).Value)
                    objtr.Total_net_Amt = clsCommon.myCdbl(grow.Cells(colTotalNetAmount).Value)
                    objtr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotalTaxAmount).Value)
                    objtr.TPT = clsCommon.myCdbl(grow.Cells(colTPT).Value)
                    objtr.Total_Item_Amt = clsCommon.myCdbl(grow.Cells(colTotalItemAmount).Value)
                    objtr.Total_TPT = clsCommon.myCdbl(grow.Cells(colTotalTPT).Value)
                    'objtr.Unit_COGS=
                    objtr.From_Scheme_Code = clsCommon.myCstr(grow.Cells(colFromSchemeCode).Value)
                    objtr.Abatement = abatement(grow, trans)
                    objtr.Empty_Value_Shell = grow.Cells(colEmptyValueShell).Value
                    objtr.Empty_Value_Bottle = grow.Cells(colEmptyValueBottle).Value
                    objtr.Transfer_Basic_Amount = grow.Cells(colTransferBasicAmount).Value
                    objtr.Cust_Discount = grow.Cells(colcustDiscount).Value
                    objtr.Total_Cust_Discount = clsCommon.myCdbl(grow.Cells(colTotalCustDiscount).Value)
                    objtr.Level1_User_Code = l1User
                    objtr.Level2_User_Code = l2User
                    objtr.Level3_User_Code = l3User
                    objtr.Level4_User_Code = l4User
                    objtr.Level5_User_Code = l5User
                    'objtr.Level1_User_Commission=
                    'objtr.Level2_User_Commission=
                    'objtr.Level3_User_Commission=
                    'objtr.Level4_User_Commission=
                    'objtr.Level5_User_Commission=
                    'objtr.Level1_User_Comm_Amount=
                    'objtr.Level2_User_Comm_Amount=
                    'objtr.Level3_User_Comm_Amount=
                    'objtr.Level4_User_Comm_Amount=
                    'objtr.Level5_User_Comm_Amount=
                    objtr.Batch_No = clsCommon.myCstr(grow.Cells(colBatchNumber).Value)
                    objtr.Sample_Scheme_Code = clsCommon.myCstr(txtSchemeSample.txtValue.Text)
                    objtr.Main_Item = clsCommon.myCstr(grow.Cells(colMainItem).Value)
                    objtr.Discount_Code = clsCommon.myCstr(grow.Cells(colDiscountCode).Value)
                    objtr.Cust_Item_Discount_NoTax = clsCommon.myCdbl(grow.Cells(ColCustDisNoTax).Value)
                    objtr.Target_Discount_Amt = clsCommon.myCdbl(grow.Cells(ColTargetDisAmt).Value)
                    objtr.Price_To_Show = clsCommon.myCdbl(grow.Cells(ColPriceToShow).Value)
                    If clsCommon.myLen(grow.Cells(colPriceDateActual).Value) > 0 Then
                        objtr.Price_Date_Actual = clsCommon.myCDate(grow.Cells(colPriceDateActual).Value)
                    End If
                    Dim objConvFac As clsTempUOMForConversion = clsTempUOMForConversion.GetConvertsionFactors(clsCommon.myCstr(grow.Cells(ColICode).Value), trans)
                    If objConvFac IsNot Nothing Then
                        If clsCommon.CompairString("FB", clsCommon.myCstr(grow.Cells(colUnitCode).Value)) = CompairStringResult.Equal Then
                            objtr.RAW_Qty = clsCommon.myCdbl(grow.Cells(colShippedQty).Value) / (objConvFac.Raw)
                        Else
                            objtr.RAW_Qty = clsCommon.myCdbl(grow.Cells(colShippedQty).Value)
                        End If
                        objtr.RAW_Qty = Math.Round(objtr.RAW_Qty, 2, MidpointRounding.ToEven)
                        If objConvFac.Converted <> 0 Then
                            objtr.Converted_Qty = objtr.RAW_Qty * (objConvFac.Raw) / objConvFac.Converted
                            objtr.Converted_Qty = Math.Round(objtr.Converted_Qty, 2, MidpointRounding.ToEven)
                        End If
                        objtr.OZ_Qty = objtr.Converted_Qty * objConvFac.OZ
                        objtr.OZ_Qty = Math.Round(objtr.OZ_Qty, 2, MidpointRounding.ToEven)
                    End If
                    objtr.Abatement_rate = clsCommon.myCdbl(grow.Cells(colAbatementRate).Value)
                    obj.Arr.Add(objtr)
                End If
            Next

            obj.SaveData(obj, isNewEntry, trans)
            txtDocNo.Value = obj.Shipment_No
            UpdateUnitCogs(obj, trans)
            lblSaleInvoiceNo.Text = clsSaleHead.createInvoice(lblSaleInvoiceNo.Text, txtDocNo.Value, trans)
            CreateEmpties(trans)
            PostOfTransferWhenManualAmtEqualFieldAmt(trans)
            'Throw New Exception("Exception")
            trans.Commit()
            isNewEntry = False
            btnAdd.Text = "Update"
            btnAdd.Enabled = True
            btnSaveAndPrint.Enabled = True
            btnDelete.Enabled = True
            btnPost.Enabled = True
            btnSettlement.Enabled = True
            txtMannaulInvoiceNo.Text = ""
            pnlMannualInvoiceNo.Visible = False

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub UpdateUnitCogs(ByVal obj As clsShipmentMaster, ByVal trans As SqlTransaction)
        If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            For Each objtr As clsShipmentDetail In obj.Arr
                clsShipmentMaster.updateUnitCogs(trans, obj, objtr, obj.Shipment_No)
            Next
        End If
    End Sub

    Public Sub CreateAndPostInvoice(ByVal trans As SqlTransaction)
        Try
            clsSaleHead.Postdata(lblSaleInvoiceNo.Text, trans)
        Catch ex As Exception
            Throw New Exception("Error in created Invoice" + Environment.NewLine + ex.Message)
        End Try
    End Sub

    Sub OpenSettlementEntry(ByVal strInvoiceNo As String, ByVal trans As SqlTransaction)
        If clsCommon.myCdbl(txtShipmentAmt.Text) > 0 AndAlso clsCommon.myLen(strInvoiceNo) > 0 Then
            Dim frm As frmAdj = New frmAdj()
            frm.isFromLoadout = True
            frm.strCustomerNo = txtCustomer.Value
            frm.strCustomerName = txtCustomerName.Text
            frm.strDocumnentNo = strInvoiceNo
            frm.dblDocumnentAmt = clsCommon.myCdbl(txtShipmentAmt.Text)
            frm.dtLoadOut = txtDate.Value
            frm.ShowDialog()
        End If
    End Sub

    Private Function CreateAnotherTransaction() As Boolean
        If (common.clsCommon.MyMessageBoxShow("Do you want to create another Transaction Against Transfer/Loadout no:" + txtTransferNo.Value, Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
            Dim strTransferNo As String = txtTransferNo.Value
            resetdata()
            cboLoadOutType.Text = "Transfer"
            loadOutTypeChanged(cboLoadOutType.Text)
            txtTransferNo.Value = strTransferNo
            LoadTransferData()
            fndCustomer__MYValidating(Nothing, Nothing, True)
            txtcustomerinvoiceno.Focus()
            Return True
        End If
        Return False
    End Function

    Private Sub totalAmounts()
        Dim otherCharges As Decimal
        Dim additionalCharges As Decimal
        Dim freight As Decimal
        Dim discountAmt As Decimal
        Dim totalTaxAmt As Decimal
        Dim shipmentTotal As Decimal
        If txtOtherCharges.Text = String.Empty Then
            otherCharges = 0.0
        Else
            otherCharges = clsCommon.myCdbl(txtOtherCharges.Text)
        End If
        If txtAdditionalCharges.Text = String.Empty Then
            additionalCharges = 0.0
        Else
            additionalCharges = clsCommon.myCdbl(txtAdditionalCharges.Text)
        End If
        If txtFreight.Text = String.Empty Then
            freight = 0.0
        Else
            freight = clsCommon.myCdbl(txtFreight.Text)
        End If
        If txtCustDisc.Text = String.Empty Then
            discountAmt = 0.0
        Else
            discountAmt = clsCommon.myCdbl(txtCustDisc.Text)
        End If
        If txtTotalTaxAmount.Text = String.Empty Then
            totalTaxAmt = 0
        Else
            totalTaxAmt = txtTotalTaxAmount.Text
        End If
        If txtShipmentTotal.Text = String.Empty Then
            shipmentTotal = 0
        Else
            shipmentTotal = txtShipmentTotal.Text
        End If
        'Dim shipmentDiscPer As Decimal
        'Dim shipmentDiscAmt As Decimal

        txtShipmentAmt.Text = Math.Round(shipmentTotal + totalTaxAmt + otherCharges + additionalCharges + freight + totalTransport(), 4)


    End Sub
    Private Sub totalAmounts(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim otherCharges As Decimal
        Dim additionalCharges As Decimal
        Dim freight As Decimal
        Dim discountAmt As Decimal
        Dim totalTaxAmt As Decimal
        Dim shipmentTotal As Decimal
        If txtOtherCharges.Text = String.Empty Then
            otherCharges = 0.0
        Else
            otherCharges = clsCommon.myCdbl(txtOtherCharges.Text)
        End If
        If txtAdditionalCharges.Text = String.Empty Then
            additionalCharges = 0.0
        Else
            additionalCharges = clsCommon.myCdbl(txtAdditionalCharges.Text)
        End If
        If txtFreight.Text = String.Empty Then
            freight = 0.0
        Else
            freight = clsCommon.myCdbl(txtFreight.Text)
        End If
        If txtCustDisc.Text = String.Empty Then
            discountAmt = 0.0
        Else
            discountAmt = clsCommon.myCdbl(txtCustDisc.Text)
        End If
        If txtTotalTaxAmount.Text = String.Empty Then
            totalTaxAmt = 0
        Else
            totalTaxAmt = txtTotalTaxAmount.Text
        End If
        If txtShipmentTotal.Text = String.Empty Then
            shipmentTotal = 0
        Else
            shipmentTotal = txtShipmentTotal.Text
        End If
        'Dim shipmentDiscPer As Decimal
        'Dim shipmentDiscAmt As Decimal

        txtShipmentAmt.Text = Math.Round(shipmentTotal + totalTaxAmt + otherCharges + additionalCharges + freight + totalTransport(), 4)


    End Sub

    Private Function GetShipmentViewQty() As String
        Dim baseQty As String = "SELECT TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc, CONVERT(varchar(10), TSPL_ITEM_PRICE_MASTER.Start_Date, 103) AS Start_Date, TSPL_ITEM_PRICE_MASTER.UOM, TSPL_ITEM_PRICE_MASTER.Price_Code, TSPL_ITEM_PRICE_MASTER.Item_Basic_Net,TSPL_ITEM_MASTER.Batch_No , TSPL_ITEM_PRICE_MASTER.Tax_group  , TSPL_ITEM_PRICE_MASTER.Item_Basic_Price, TSPL_ITEM_PRICE_MASTER.Empty_Value_Shell, TSPL_ITEM_PRICE_MASTER.Empty_Value_Bottle, TSPL_ITEM_MASTER.Item_Type, TSPL_ITEM_MASTER.show, TSPL_ITEM_MASTER.Sku_Seq, TSPL_ITEM_PRICE_MASTER.TAX1_Rate,TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,TSPL_ITEM_PRICE_MASTER.TAX6_Rate,TSPL_ITEM_PRICE_MASTER.TAX7_Rate,TSPL_ITEM_PRICE_MASTER.TAX8_Rate,TSPL_ITEM_PRICE_MASTER.TAX9_Rate,TSPL_ITEM_PRICE_MASTER.TAX10_Rate , TSPL_ITEM_PRICE_MASTER.TAX1_Amt ,TSPL_ITEM_PRICE_MASTER.TAX2_Amt,TSPL_ITEM_PRICE_MASTER.TAX3_Amt,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.TAX5_Amt,TSPL_ITEM_PRICE_MASTER.TAX6_Amt,TSPL_ITEM_PRICE_MASTER.TAX7_Amt,TSPL_ITEM_PRICE_MASTER.TAX8_Amt,TSPL_ITEM_PRICE_MASTER.TAX9_Amt,TSPL_ITEM_PRICE_MASTER.TAX10_Amt"
        baseQty += " ,TSPL_ITEM_PRICE_MASTER.Abatement_Rate, "
        baseQty += "TSPL_ITEM_PRICE_MASTER.Price_Comp1 , TSPL_ITEM_PRICE_MASTER.Price_Amount1,TSPL_ITEM_PRICE_MASTER.Price_Comp2 ,TSPL_ITEM_PRICE_MASTER.Price_Amount2,TSPL_ITEM_PRICE_MASTER.Price_Comp3 ,TSPL_ITEM_PRICE_MASTER.Price_Amount3,TSPL_ITEM_PRICE_MASTER.Price_Comp4 ,TSPL_ITEM_PRICE_MASTER.Price_Amount4,TSPL_ITEM_PRICE_MASTER.Price_Comp5 ,TSPL_ITEM_PRICE_MASTER.Price_Amount5,TSPL_ITEM_PRICE_MASTER.Price_Comp6 ,TSPL_ITEM_PRICE_MASTER.Price_Amount6,TSPL_ITEM_PRICE_MASTER.Price_Comp7 ,TSPL_ITEM_PRICE_MASTER.Price_Amount7,TSPL_ITEM_PRICE_MASTER.Price_Comp8 ,TSPL_ITEM_PRICE_MASTER.Price_Amount8,TSPL_ITEM_PRICE_MASTER.Price_Comp9 ,TSPL_ITEM_PRICE_MASTER.Price_Amount9,TSPL_ITEM_PRICE_MASTER.Price_Comp10 ,TSPL_ITEM_PRICE_MASTER.Price_Amount10,(TSPL_ITEM_PRICE_MASTER.NetLTPT+TSPL_ITEM_PRICE_MASTER.Price_Amount10) as PriceToShow "
        baseQty += " FROM TSPL_ITEM_PRICE_MASTER "
        baseQty += " INNER Join "
        baseQty += " (SELECT     Item_Code, UOM, MAX(Start_Date) AS MaxDateTime, Item_Basic_Net,  Price_Code, Tax_group  FROM  TSPL_ITEM_PRICE_MASTER "
        baseQty += " where Start_Date<='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "' GROUP BY Item_Code, UOM, Item_Basic_Net,  Price_Code, Tax_group    ) "
        baseQty += " AS groupedP "
        baseQty += " ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group   "
        baseQty += " INNER JOIN TSPL_ITEM_MASTER AS TSPL_ITEM_MASTER ON TSPL_ITEM_PRICE_MASTER.Item_Code = TSPL_ITEM_MASTER.Item_Code "
        Return baseQty
    End Function

    Private Sub priceDateSelection(Optional ByVal items As String = "All")
        Try
            isInsideLoadData = True
            dtStartTime = DateTime.Now
            strMsg = ""

            gv1.Rows.Clear()
            Dim tptcheck As String = "N"
            LoadBlankGrid()
            Dim baseQty As String = GetShipmentViewQty()
            If items = "FB" Or rbFB.ToggleState = ToggleState.On Then
                sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle, TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate , TAX1_Amt ,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt,Abatement_Rate,Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10,PriceToShow  FROM (" + baseQty + ")xxx Where Show='N' AND UOM='FB' and  Price_Code='" + txtPriceCode.Text.Trim() + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.txtValue.Text + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0) Order By Sku_Seq,Start_Date,UOM desc"
            ElseIf items = "FC" Or rbFC.ToggleState = ToggleState.On Then
                sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle, TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate , TAX1_Amt ,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt,Abatement_Rate,Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10,PriceToShow  FROM (" + baseQty + ")xxx Where Show='N' AND UOM='FC' AND  Price_Code='" + txtPriceCode.Text.Trim() + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.txtValue.Text + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0)  Order By Sku_Seq,Start_Date,UOM desc"
            ElseIf items = "All" Then
                sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle, TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate , TAX1_Amt ,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt,Abatement_Rate,Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10,PriceToShow  FROM (" + baseQty + ")xxx Where  UOM in ('FC','FB') AND Price_Code='" + txtPriceCode.Text.Trim() + "' AND  ITEM_TYPE = 'F' and Tax_group = '" + fndTaxGroup.txtValue.Text + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0) Order By Sku_Seq,Start_Date,UOM desc"
            End If

            Dim dr5 As DataTable = clsDBFuncationality.GetDataTable(sql)

            dtEndTime = DateTime.Now
            span = dtEndTime.Subtract(dtStartTime)
            strMsg += "query Execute:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
            dtStartTime = DateTime.Now


            Dim i As Integer = 0
            If dr5 IsNot Nothing AndAlso dr5.Rows.Count > 0 Then
                For Each tdr As DataRow In dr5.Rows
                    Dim isAddNewRow As Boolean = False

                    Dim convFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(tdr("Item_Code")), clsCommon.myCstr(tdr("UOM")), Nothing)
                    Dim sql2 As String = "SELECT isnull(SUM(isnull(Item_Qty,0)),0) FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + tdr(0).ToString() + "' AND Location_Code='" + txtLocation.Value + "' and MRP='" + clsCommon.myCstr(clsCommon.myCdbl(tdr(5)) * convFac) + "'"
                    Dim balanceamt As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql2))

                    If balanceamt > 0 Then
                        isAddNewRow = True
                    End If

                    If convFac <> 1 Then
                        balanceamt = 0
                    End If

                    If isAddNewRow Then
                        Dim datarowinfo As GridViewRowInfo = gv1.Rows.AddNew()
                        datarowinfo.Cells(colBalanceQty).Value = Math.Round(balanceamt, 2)
                        datarowinfo.Cells(ColComplete).Value = "No"
                        datarowinfo.Cells(ColICode).Value = tdr(0).ToString()
                        'datarowinfo.Cells(ColICode).ReadOnly = True
                        datarowinfo.Cells(ColItemName).Value = tdr(1).ToString()
                        datarowinfo.Cells(colPriceDateColumn).Value = tdr(2).ToString()
                        datarowinfo.Cells(colPriceDateColumn).ReadOnly = True
                        datarowinfo.Cells(colOrderedQty).Value = "0"
                        datarowinfo.Cells(colShippedQty).Value = 0
                        datarowinfo.Cells(colUnitCode).Value = tdr(3).ToString()
                        datarowinfo.Cells(collocation).Value = txtLocation.Value
                        If datarowinfo.Cells(ColICode).Value.ToString().Trim() = "7n600pfc" Then
                            common.clsCommon.MyMessageBoxShow("itemcaught")
                        End If

                        datarowinfo.Cells(colPriceCode).Value = tdr(4).ToString()
                        datarowinfo.Cells(colSchemeApplicable).Value = "No"
                        datarowinfo.Cells(colPromoSchemeApplicable).Value = "No"
                        datarowinfo.Cells(colPromoSchemeItem).Value = "No"
                        datarowinfo.Cells(colPromoSchemeCode).Value = ""
                        datarowinfo.Cells(colSchemeCodeItem).Value = ""
                        datarowinfo.Cells(colSchemeItem).Value = "No"
                        datarowinfo.Cells(colSchemeDiscountApplicable).Value = "No"
                        datarowinfo.Cells(colSchemeCodeDiscount).Value = ""
                        datarowinfo.Cells(colSampleItem).Value = "No"
                        datarowinfo.Cells(colEmptyValue).Value = Convert.ToDecimal(tdr(7).ToString()) + Convert.ToDecimal(tdr(8).ToString())
                        datarowinfo.Cells(colEmptyValueShell).Value = Convert.ToDecimal(tdr(7).ToString())
                        datarowinfo.Cells(colEmptyValueBottle).Value = Convert.ToDecimal(tdr(8).ToString())
                        datarowinfo.Cells(colMRP).Value = clsCommon.myCdbl(tdr(5))
                        datarowinfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), clsCommon.myCdbl(datarowinfo.Cells(colMRP).Value))

                        sql = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"
                        Dim strexcisable As String = clsDBFuncationality.getSingleValue(sql)
                        If strexcisable Is Nothing Then
                            strexcisable = "T"
                        End If
                        Dim assessibleamt As Decimal = 0
                        datarowinfo.Cells(colBasicAmount).Value = tdr(6).ToString()
                        datarowinfo.Cells(colTransferBasicAmount).Value = tdr(6).ToString()
                        assessibleamt = Math.Round(clsCommon.myCdbl(tdr(5).ToString()) * abatement(datarowinfo, Nothing) / 100, 2)
                        If strexcisable = "T" Or strexcisable = "F" Then
                            datarowinfo.Cells(colBasicAmount).Value = tdr(6).ToString()
                        Else
                            datarowinfo.Cells(colBasicAmount).Value = Math.Round(clsCommon.myCdbl(tdr(6).ToString()) + assessibleamt * 10.3 / 100, 2)
                        End If
                        datarowinfo.Cells(colAbatementRate).Value = tdr("Abatement_Rate").ToString()
                        Dim discAmount As String = 0
                        Dim dblFirstTaxRate As Double = clsCommon.myCdbl(tdr("TAX1_Rate"))
                        Dim dblCustDisWithoutTax As Double = Math.Round(discAmount * 100 / (100 + dblFirstTaxRate), 2, MidpointRounding.ToEven)


                        datarowinfo.Cells(ColCustDisNoTax).Value = dblCustDisWithoutTax
                        datarowinfo.Cells(colitemNetAmount).Value = clsCommon.myCdbl(datarowinfo.Cells(colBasicAmount).Value) - dblCustDisWithoutTax
                        datarowinfo.Cells(colcustDiscount).Value = discAmount
                        datarowinfo.Cells(colTotalCustDiscount).Value = 0
                        datarowinfo.Cells(colDiscountAmount).Value = "0"
                        For counttax As Integer = 1 To 6
                            Dim taxrate As String = "Tax" + counttax.ToString() + "_Rate"
                            Dim taxr As String = "tax" + counttax.ToString() + "rate"
                            datarowinfo.Cells(taxr).Value = clsCommon.myCdbl(tdr(taxrate))
                        Next
                        datarowinfo.Cells(colTaxamount).Value = "0"

                        SnDUtility.calculateTax(0, 0, txtLocation.Value, gv1, gvTax)
                        datarowinfo.Cells(colTotalMRP).Value = "0"
                        datarowinfo.Cells(colTotalBasicAmount).Value = "0"
                        datarowinfo.Cells(colTotalDiscountAmount).Value = "0"
                        datarowinfo.Cells(colTotalNetAmount).Value = "0"
                        datarowinfo.Cells(colTotalTaxAmount).Value = "0"
                        sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + datarowinfo.Cells(ColICode).Value + "' AND " & _
                          " Uom_Code='" + datarowinfo.Cells(colUnitCode).Value + "'"
                        Dim convertFact As Decimal = clsDBFuncationality.getSingleValue(sql)

                        Dim tpt As String = ""
                        datarowinfo.Cells(ColPriceToShow).Value = clsCommon.myCdbl(tdr("PriceToShow"))
                        For j As Integer = 1 To 10
                            Dim Price_Amount As String = "Price_Amount" + j.ToString()
                            Dim Price_Comp As String = "Price_Comp" + j.ToString()
                            tptcheck = clsDBFuncationality.getSingleValue("select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code = '" + Convert.ToString(tdr(Price_Comp)) + "'")
                            If tptcheck = "Y" Then
                                tpt = Convert.ToString(tdr(Price_Amount))
                                Exit For
                            End If
                        Next
                        datarowinfo.Cells(colTPT).Value = tpt
                        datarowinfo.Cells(colTotalItemAmount).Value = "0"
                        datarowinfo.Cells(colTotalTPT).Value = "0"
                        datarowinfo.Cells(colFromSchemeCode).Value = ""
                        datarowinfo.Cells(colBatchNumber).Value = clsDBFuncationality.getSingleValue("select batch_no from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + datarowinfo.Cells(ColICode).Value + "' AND Location_Code = '" + txtLocation.Value + "'")
                        If convFac = 1 Then
                            datarowinfo.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), txtLocation.Value, clsCommon.myCstr(datarowinfo.Cells(colMRP).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), txtDocNo.Value, txtDate.Value)
                        End If

                    End If
                    gv1.Refresh()
                Next

                dtEndTime = DateTime.Now
                span = dtEndTime.Subtract(dtStartTime)
                strMsg += "Item fill:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
                dtStartTime = DateTime.Now


                gv1.AllowAddNewRow = True ''Change false to ture
                AddHandler txtCustDisc.TextChanged, AddressOf totalAmounts
                AddHandler txtAdditionalCharges.TextChanged, AddressOf totalAmounts
                AddHandler txtOtherCharges.TextChanged, AddressOf totalAmounts
                AddHandler txtFreight.TextChanged, AddressOf totalAmounts
            End If
            gv1.AllowAddNewRow = True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try


    End Sub

    Private Sub priceDateSelection1(Optional ByVal taxgroup As String = "")
        Dim tptcheck As String = "N"
        Dim strUOM As String = ""
        Dim dtUOM As DataTable = clsDBFuncationality.GetDataTable("select distinct Unit_code from TSPL_SHIPMENT_DETAILS where TSPL_SHIPMENT_DETAILS.Shipment_No = '" + txtDocNo.Value + "'")
        If dtUOM IsNot Nothing AndAlso dtUOM.Rows.Count > 0 Then
            If dtUOM.Rows.Count = 1 Then
                strUOM = clsCommon.myCstr(dtUOM.Rows(0)(0))
            End If
        End If

        Dim baseQty As String = GetShipmentViewQty()
        sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle, TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate , TAX1_Amt ,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt,abatement_rate,Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10,PriceToShow FROM (" + baseQty + ")xxx  Where 2=2 "
        If clsCommon.myLen(strUOM) > 0 Then
            sql += " and UOM='" + strUOM + "'"
        End If
        sql += " and Price_Code='" + txtPriceCode.Text.Trim() + "' AND ITEM_TYPE = 'F'  "
        sql += " and not Exists(select 1 from TSPL_SHIPMENT_DETAILS where TSPL_SHIPMENT_DETAILS.Shipment_No = '" + txtDocNo.Value + "' and TSPL_SHIPMENT_DETAILS.item_code=xxx.Item_Code and TSPL_SHIPMENT_DETAILS.MRP_Amt=xxx.Item_Basic_Net) "
        sql += " and Tax_group = '" + taxgroup + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0) Order By Sku_Seq"

        Dim dr5 As DataTable = clsDBFuncationality.GetDataTable(sql)
        Dim i As Integer = 0
        If dr5 IsNot Nothing AndAlso dr5.Rows.Count > 0 Then
            For Each tdr As DataRow In dr5.Rows
                sql = "SELECT isnull(SUM(isnull(Item_Qty,0)),0) FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + tdr(0).ToString() + "' AND Location_Code='" + txtLocation.Value + "' and MRP='" + tdr(5).ToString() + "'"
                Dim dblBalanceQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                If dblBalanceQty > 0 Then
                    Dim datarowinfo As GridViewRowInfo = gv1.Rows.AddNew()
                    datarowinfo.Cells(ColComplete).Value = "No"
                    datarowinfo.Cells(ColICode).Value = tdr(0).ToString()
                    'datarowinfo.Cells(ColICode).ReadOnly = True
                    datarowinfo.Cells(ColItemName).Value = tdr(1).ToString()
                    datarowinfo.Cells(colPriceDateColumn).Value = tdr(2).ToString()
                    datarowinfo.Cells(colOrderedQty).Value = "0"
                    datarowinfo.Cells(colShippedQty).Value = 0
                    datarowinfo.Cells(colUnitCode).Value = tdr(3).ToString()
                    datarowinfo.Cells(collocation).Value = txtLocation.Value
                    datarowinfo.Cells(colBalanceQty).Value = Math.Round(dblBalanceQty, 2, MidpointRounding.ToEven)
                    datarowinfo.Cells(colPriceCode).Value = tdr(4).ToString()
                    datarowinfo.Cells(colSchemeApplicable).Value = "No"
                    datarowinfo.Cells(colPromoSchemeApplicable).Value = "No"
                    datarowinfo.Cells(colPromoSchemeItem).Value = "No"
                    datarowinfo.Cells(colPromoSchemeCode).Value = ""
                    datarowinfo.Cells(colSchemeCodeItem).Value = ""
                    datarowinfo.Cells(colSchemeItem).Value = "No"
                    datarowinfo.Cells(colSchemeDiscountApplicable).Value = "No"
                    datarowinfo.Cells(colSchemeCodeDiscount).Value = ""
                    datarowinfo.Cells(colSampleItem).Value = "No"
                    datarowinfo.Cells(colEmptyValue).Value = Convert.ToDecimal(tdr(7).ToString()) + Convert.ToDecimal(tdr(8).ToString())
                    datarowinfo.Cells(colEmptyValueShell).Value = Convert.ToDecimal(tdr(7).ToString())
                    datarowinfo.Cells(colEmptyValueBottle).Value = Convert.ToDecimal(tdr(8).ToString())
                    datarowinfo.Cells(colMRP).Value = clsCommon.myCdbl(tdr(5))
                    datarowinfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), clsCommon.myCdbl(datarowinfo.Cells(colMRP).Value))

                    sql = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"
                    Dim strexcisable As String = clsDBFuncationality.getSingleValue(sql)
                    If strexcisable Is Nothing Then
                        strexcisable = "T"
                    End If
                    Dim assessibleamt As Decimal = 0
                    datarowinfo.Cells(colBasicAmount).Value = tdr(6).ToString()
                    datarowinfo.Cells(colTransferBasicAmount).Value = tdr(6).ToString()
                    assessibleamt = Math.Round(clsCommon.myCdbl(tdr(5).ToString()) * abatement(datarowinfo, Nothing) / 100, 2)
                    datarowinfo.Cells(colBasicAmount).Value = tdr(6).ToString()
                    Dim discAmount As String = 0
                    Dim dblFirstTaxRate As Double = clsCommon.myCdbl(tdr("TAX1_Rate"))
                    Dim dblCustDisWithoutTax As Double = Math.Round(discAmount * 100 / (100 + dblFirstTaxRate), 2, MidpointRounding.ToEven)

                    For counttax As Integer = 1 To 6
                        Dim taxrate As String = "Tax" + counttax.ToString() + "_Rate"
                        Dim taxr As String = "tax" + counttax.ToString() + "rate"
                        datarowinfo.Cells(taxr).Value = clsCommon.myCdbl(tdr(taxrate))
                    Next
                    datarowinfo.Cells(colitemNetAmount).Value = clsCommon.myCdbl(datarowinfo.Cells(colBasicAmount).Value) - dblCustDisWithoutTax
                    datarowinfo.Cells(colAbatementRate).Value = clsCommon.myCdbl(tdr("abatement_rate"))
                    datarowinfo.Cells(colcustDiscount).Value = discAmount
                    datarowinfo.Cells(colTotalCustDiscount).Value = 0
                    datarowinfo.Cells(colDiscountAmount).Value = "0"
                    SnDUtility.calculateTax(0, 0, txtLocation.Value, gv1, gvTax)
                    datarowinfo.Cells(colTotalMRP).Value = "0"
                    datarowinfo.Cells(colTotalBasicAmount).Value = "0"
                    datarowinfo.Cells(colTotalDiscountAmount).Value = "0"
                    datarowinfo.Cells(colTotalNetAmount).Value = "0"
                    datarowinfo.Cells(colTaxamount).Value = "0"
                    datarowinfo.Cells(colTotalTaxAmount).Value = "0"
                    sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + datarowinfo.Cells(ColICode).Value + "' AND " & _
                      " Uom_Code='" + datarowinfo.Cells(colUnitCode).Value + "'"
                    Dim convertFact As Decimal = clsDBFuncationality.getSingleValue(sql)

                    Dim tpt As String = ""
                    datarowinfo.Cells(ColPriceToShow).Value = clsCommon.myCdbl(tdr("PriceToShow"))
                    For j As Integer = 10 To 1 Step -1
                        Dim Price_Amount As String = "Price_Amount" + j.ToString()
                        Dim Price_Comp As String = "Price_Comp" + j.ToString()
                        tptcheck = clsDBFuncationality.getSingleValue("select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code = '" + Convert.ToString(tdr(Price_Comp)) + "'")
                        If tptcheck = "Y" Then
                            tpt = Convert.ToString(tdr(Price_Amount))
                            Exit For
                        End If
                    Next
                    datarowinfo.Cells(colTPT).Value = tpt
                    datarowinfo.Cells(colTotalItemAmount).Value = "0"
                    datarowinfo.Cells(colTotalTPT).Value = "0"
                    datarowinfo.Cells(colFromSchemeCode).Value = ""
                    datarowinfo.Cells(colBatchNumber).Value = clsDBFuncationality.getSingleValue("select batch_no from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + datarowinfo.Cells(ColICode).Value + "' AND Location_Code = '" + txtLocation.Value + "'")
                    If clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal Then
                        Dim convFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), Nothing)
                        If convFac = 1 Then
                            datarowinfo.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), txtLocation.Value, clsCommon.myCstr(datarowinfo.Cells(colMRP).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), txtDocNo.Value, txtDate.Value)
                        End If
                    End If
                End If
                gv1.Refresh()
            Next
            gv1.AllowAddNewRow = True ''Change false to ture
            AddHandler txtCustDisc.TextChanged, AddressOf totalAmounts
            AddHandler txtAdditionalCharges.TextChanged, AddressOf totalAmounts
            AddHandler txtOtherCharges.TextChanged, AddressOf totalAmounts
            AddHandler txtFreight.TextChanged, AddressOf totalAmounts
        End If
        gv1.AllowAddNewRow = True
    End Sub

    Private Sub currentmanualscheme(ByVal itemcode As String, ByVal startdate As String, ByVal mrp As Decimal, ByVal strUOM As String)
        Dim tptcheck As String = "N"
        Dim baseQry As String = GetShipmentViewQty()
        sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle,UOM,Abatement_Rate FROM (" + baseQry + ")xxx Where Show='N' AND ITEM_CODE = '" + itemcode + "' AND UOM='" + strUOM + "' and  Price_Code='" + txtPriceCode.Text.Trim() + "' and Start_Date = '" + startdate + "' and Item_Basic_Net = '" + CStr(mrp) + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.txtValue.Text + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0)  Order By Sku_Seq"
        Dim dr5 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dr5 IsNot Nothing AndAlso dr5.Rows.Count > 0 Then
            For Each tdr As DataRow In dr5.Rows
                gv1.CurrentRow.Cells(ColComplete).Value = "Yes"
                gv1.CurrentRow.Cells(ColICode).Value = itemcode
                gv1.CurrentRow.Cells(ColItemName).Value = Convert.ToString(tdr(1))
                gv1.CurrentRow.Cells(colPriceDateColumn).Value = startdate
                gv1.CurrentRow.Cells(colPriceDateColumn).ReadOnly = True
                gv1.CurrentRow.Cells(colOrderedQty).Value = "0.00"
                gv1.CurrentRow.Cells(colShippedQty).Value = 1
                gv1.CurrentRow.Cells(colUnitCode).Value = clsCommon.myCstr(tdr("UOM"))  ''"FB"
                gv1.CurrentRow.Cells("unitCode").ReadOnly = True
                gv1.CurrentRow.Cells(collocation).Value = txtLocation.Value
                gv1.CurrentRow.Cells(colBalanceQty).Value = "0.00"
                gv1.CurrentRow.Cells(colPriceCode).Value = tdr(4).ToString()
                gv1.CurrentRow.Cells(colSchemeApplicable).Value = "No"
                gv1.CurrentRow.Cells(colPromoSchemeApplicable).Value = "No"
                gv1.CurrentRow.Cells(colPromoSchemeItem).Value = "No"
                gv1.CurrentRow.Cells(colAbatementRate).Value = clsCommon.myCdbl(tdr("Abatement_Rate"))
                gv1.CurrentRow.Cells(colPromoSchemeCode).Value = ""
                gv1.CurrentRow.Cells(colSchemeCodeItem).Value = ""
                gv1.CurrentRow.Cells(colSchemeItem).Value = "Yes"
                gv1.CurrentRow.Cells(colSchemeDiscountApplicable).Value = "No"
                gv1.CurrentRow.Cells(colSchemeCodeDiscount).Value = ""
                gv1.CurrentRow.Cells(colSampleItem).Value = "No"
                gv1.CurrentRow.Cells(colEmptyValue).Value = Convert.ToString(clsCommon.myCdbl(tdr("Empty_Value_Bottle") + tdr("Empty_Value_Shell")))
                gv1.CurrentRow.Cells(colEmptyValueShell).Value = clsCommon.myCstr(clsCommon.myCdbl(tdr("Empty_Value_Shell")))
                gv1.CurrentRow.Cells(colEmptyValueBottle).Value = Convert.ToString(tdr("Empty_Value_Bottle"))
                gv1.CurrentRow.Cells(colMRP).Value = clsCommon.myCdbl(tdr(5))
                gv1.CurrentRow.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitCode).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value))
                gv1.CurrentRow.Cells(colBasicAmount).Value = tdr(6).ToString()
                gv1.CurrentRow.Cells(colTransferBasicAmount).Value = tdr(6).ToString()
                gv1.CurrentRow.Cells(colitemNetAmount).Value = "0.00"
                gv1.CurrentRow.Cells(colcustDiscount).Value = "0.00"
                gv1.CurrentRow.Cells(colTotalCustDiscount).Value = "0.00"
                gv1.CurrentRow.Cells(colDiscountAmount).Value = "0.00"
                sql = "select excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"
                Dim strexcisable As String = clsDBFuncationality.getSingleValue(sql)
                If strexcisable = "T" Then
                    Dim strDate As String = ""
                    Dim dr As DataRow = clsTaxMaster.GetExcisableTaxRates(itemcode, clsCommon.myCdbl(mrp), clsCommon.GetPrintDate(startdate, "yyyy-MM-dd"), clsCommon.myCdbl(gv1.CurrentRow.Cells(colBasicAmount).Value), Convert.ToString(gv1.CurrentRow.Cells(colUnitCode).Value), fndTaxGroup.txtValue.Text, txtPriceCode.Text)
                    If dr IsNot Nothing AndAlso dr.ItemArray.Length > 0 Then
                        gv1.CurrentRow.Cells(colTax1Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax1Rate")), 2)
                        gv1.CurrentRow.Cells(colAssess1).Value = Math.Round(clsCommon.myCdbl(dr("Tax1BaseAmt")), 2)
                        gv1.CurrentRow.Cells(colTax1Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax1Amt")), 4, MidpointRounding.ToEven)

                        gv1.CurrentRow.Cells(colTax2Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax2Rate")), 2)
                        gv1.CurrentRow.Cells(colAssess2).Value = Math.Round(clsCommon.myCdbl(dr("Tax2BaseAmt")), 2)
                        gv1.CurrentRow.Cells(colTax2Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax2Amt")), 4, MidpointRounding.ToEven)

                        gv1.CurrentRow.Cells(colTax3Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax3Rate")), 2)
                        gv1.CurrentRow.Cells(colAssess3).Value = Math.Round(clsCommon.myCdbl(dr("Tax3BaseAmt")), 2)
                        gv1.CurrentRow.Cells(colTax3Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax3Amt")), 4, MidpointRounding.ToEven)

                        gv1.CurrentRow.Cells(colTax4Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax4Rate")), 2)
                        gv1.CurrentRow.Cells(colAssess4).Value = Math.Round(clsCommon.myCdbl(dr("Tax4BaseAmt")), 2)
                        gv1.CurrentRow.Cells(colTax4Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax4Amt")), 4, MidpointRounding.ToEven)

                        gv1.CurrentRow.Cells(colTax5Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax5Rate")), 2)
                        gv1.CurrentRow.Cells(colAssess5).Value = Math.Round(clsCommon.myCdbl(dr("Tax5BaseAmt")), 2)
                        gv1.CurrentRow.Cells(colTax5Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax5Amt")), 4, MidpointRounding.ToEven)

                        gv1.CurrentRow.Cells(colTax6Rate).Value = Math.Round(clsCommon.myCdbl(dr("Tax6Rate")), 2)
                        gv1.CurrentRow.Cells(colAssess6).Value = Math.Round(clsCommon.myCdbl(dr("Tax6BaseAmt")), 2)
                        gv1.CurrentRow.Cells(colTax6Amt).Value = Math.Round(clsCommon.myCdbl(dr("Tax6Amt")), 4, MidpointRounding.ToEven)

                        gv1.CurrentRow.Cells(colTaxamount).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTax1Amt).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTax2Amt).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTax3Amt).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTax4Amt).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTax5Amt).Value) + clsCommon.myCdbl(gv1.CurrentRow.Cells(colTax6Amt).Value)
                    Else
                        gv1.CurrentRow.Cells(colTax1Rate).Value = 0
                        gv1.CurrentRow.Cells(colAssess1).Value = 0
                        gv1.CurrentRow.Cells(colTax1Amt).Value = 0

                        gv1.CurrentRow.Cells(colTax2Rate).Value = 0
                        gv1.CurrentRow.Cells(colAssess2).Value = 0
                        gv1.CurrentRow.Cells(colTax2Amt).Value = 0

                        gv1.CurrentRow.Cells(colTax3Rate).Value = 0
                        gv1.CurrentRow.Cells(colAssess3).Value = 0
                        gv1.CurrentRow.Cells(colTax3Amt).Value = 0

                        gv1.CurrentRow.Cells(colTax4Rate).Value = 0
                        gv1.CurrentRow.Cells(colAssess4).Value = 0
                        gv1.CurrentRow.Cells(colTax4Amt).Value = 0

                        gv1.CurrentRow.Cells(colTax5Rate).Value = 0
                        gv1.CurrentRow.Cells(colAssess5).Value = 0
                        gv1.CurrentRow.Cells(colTax5Amt).Value = 0

                        gv1.CurrentRow.Cells(colTax6Rate).Value = 0
                        gv1.CurrentRow.Cells(colAssess6).Value = 0
                        gv1.CurrentRow.Cells(colTax6Amt).Value = 0
                        gv1.CurrentRow.Cells(colTaxamount).Value = 0.0

                    End If
                Else
                    gv1.CurrentRow.Cells(colTaxamount).Value = 0

                    For Each gr As GridViewRowInfo In gvTax.Rows
                        gv1.CurrentRow.Cells("Tax" + (gr.Index + 1).ToString() + "Rate").Value = 0 ' tds.Tables(0).Rows(0)(0).ToString()
                        gv1.CurrentRow.Cells("assess" + (gr.Index + 1).ToString()).Value = 0 'tds.Tables(0).Rows(0)(1).ToString()
                        gv1.CurrentRow.Cells("Tax" + (gr.Index + 1).ToString() + "Amt").Value = 0 'tds.Tables(0).Rows(0)(2).ToString()
                    Next
                End If
                gv1.CurrentRow.Cells(colTotalMRP).Value = Convert.ToString(tdr(5))
                gv1.CurrentRow.Cells(colTotalBasicAmount).Value = "0.00"
                gv1.CurrentRow.Cells(colTotalDiscountAmount).Value = "0.00"
                gv1.CurrentRow.Cells(colTotalNetAmount).Value = "0.00"
                gv1.CurrentRow.Cells(colTotalTaxAmount).Value = "0.00"
                gv1.CurrentRow.Cells(colTPT).Value = "0.00"
                gv1.CurrentRow.Cells(colTotalItemAmount).Value = "0"
                gv1.CurrentRow.Cells(colTotalTPT).Value = "0.00"
                Dim count As String = "MS1"
                For Each grow As GridViewRowInfo In gv1.Rows
                    If grow.Cells(colShippedQty).Value > 0 Then
                        Dim strTemp As String = clsCommon.myCstr(grow.Cells(colFromSchemeCode).Value)
                        If strTemp.Contains("MS") Then
                            count = clsCommon.incval(strTemp)
                        End If
                    End If
                Next
                gv1.CurrentRow.Cells(colFromSchemeCode).Value = count
                gv1.CurrentRow.Cells(colBatchNumber).Value = clsDBFuncationality.getSingleValue("select batch_no from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + gv1.CurrentRow.Cells(ColICode).Value + "' AND Location_Code = '" + txtLocation.Value + "'")

            Next


        End If

    End Sub

    Private Function LoadData(ByVal strCode As String, ByVal NavType As common.NavigatorType)
        Dim dtStartTime As DateTime = DateTime.Now
        Dim span As TimeSpan
        Dim dtEndTime As DateTime
        Dim strMsg As String = ""
        isInsideLoadData = True
        LoadBlankGrid()

        gvTax.DataSource = Nothing
        gvTax.Rows.Clear()
        pnlMannualInvoiceNo.Visible = False
        txtMannaulInvoiceNo.Text = ""
        Dim shipmentamt As Decimal = 0
        Dim taxamt1 As Decimal = 0
        Dim pricecode As String = ""
        Dim TAXGROUP As String = ""
        Dim basicamt1 As Decimal = 0
        btnUpdateWithCustomerNewPrice.Visible = False

        Try
            Dim strLocation As String = ""
            Dim obj As clsShipmentMaster = clsShipmentMaster.GetData(strCode, NavType, Nothing)
            If txtDate.ShowCheckBox Then
                txtDate.Checked = True
            End If
            dtEndTime = DateTime.Now
            span = dtEndTime.Subtract(dtStartTime)
            strMsg += "query Execute:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
            dtStartTime = DateTime.Now

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Shipment_No) > 0 Then
                isNewEntry = False
                txtDocNo.Value = obj.Shipment_No
                lblSaleInvoiceNo.Text = obj.Invoice_No

                gvTax.DataSource = Nothing
                gvTax.Rows.Clear()
                gvTax.AllowAddNewRow = True
                txtPriceCode.Text = obj.Price_Code

                RemoveHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged

                RemoveHandler txtSchemeSample.txtValue.TextChanged, AddressOf fndSchemeSample_TextChanged
                RemoveHandler txtAdditionalCharges.TextChanged, AddressOf totalAmounts
                RemoveHandler txtOtherCharges.TextChanged, AddressOf totalAmounts
                RemoveHandler txtFreight.TextChanged, AddressOf totalAmounts
                RemoveHandler txtCustDisc.TextChanged, AddressOf totalAmounts
                RemoveHandler txtDiscPer.TextChanged, AddressOf MyTextBox1_TextChanged

                cboLoadOutType.Text = obj.Shipment_Type
                taxamt1 = obj.Shipment_Tax_Amt
                shipmentamt = obj.Total_Order_Amt
                If clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal Then
                    lblTransferDate.Visible = False
                    lblTransferNo.Visible = False
                    txtTransferNo.Visible = False
                    txtTransferDate.Visible = False
                    lblOrderDate.Visible = True
                    lblOrderNo.Visible = True
                    txtOrderNo.Visible = True
                    txtOrderDate.Visible = True
                     
                    btnPost.Visible = True AndAlso MyBase.isPostFlag
                    btnSettlement.Visible = False

                    lblTransaction.Visible = False
                    lblManualAmt.Visible = False
                    txtMannualAmt.Visible = False

                    txtcustomerinvoiceno.Visible = False
                    lblcustomerinvoiceno.Visible = False

                    lblManualQty.Visible = False
                    txtMannualQty.Visible = False
                    chkCreateEmpty.Visible = False
                Else

                    lblTransferDate.Visible = True
                    lblTransferNo.Visible = True
                    txtTransferNo.Visible = True
                    txtTransferDate.Visible = True

                    lblOrderDate.Visible = False
                    lblOrderNo.Visible = False
                    txtOrderNo.Visible = False
                    txtOrderDate.Visible = False

                    gv1.Columns("orderedQty").HeaderText = "Transfer Qty"
                    btnPost.Visible = False
                    btnSettlement.Visible = True

                    lblTransaction.Visible = False

                    lblManualAmt.Visible = True
                    txtMannualAmt.Visible = True

                    txtcustomerinvoiceno.Visible = True
                    lblcustomerinvoiceno.Visible = True

                    lblManualQty.Visible = True
                    txtMannualQty.Visible = True
                    chkCreateEmpty.Visible = True
                End If
                txtshellqty.Text = obj.Shell_Qty
                txtDate.Value = obj.Date_Time_Removal
                txtRemovalTime.Value = obj.Date_Time_Removal
                chkAgainstCForm.Checked = IIf(obj.Against_C_Form = 1, True, False)

                txtCustDisc.Text = clsCommon.myFormat(obj.Tot_Customer_Dis_Amt)
                txtDiscPer.Text = clsCommon.myFormat(obj.Shipment_Disc_Percent)
                txtDiscAmt.Text = clsCommon.myFormat(obj.Shipment_Discount_Amt - obj.Tot_Customer_Dis_Amt)
                txtNetShipAmt.Text = clsCommon.myFormat(obj.Shipment_Detail_Total_Amt)
                txtContainerDeposit.Text = clsCommon.myFormat(obj.Empty_Value)

                txtCustomer.Value = obj.Cust_Code
                txtcustomerinvoiceno.Text = obj.Customer_Invoice_No
                sql = "SELECT Cust_Account FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code = '" + txtCustomer.Value + "'"
                strCustAccount = clsDBFuncationality.getSingleValue(sql)
                txtCustomerName.Text = obj.Cust_Name
                txtCustomerPONO.Text = obj.Cust_PONo
                txtExpectedShipDate.Value = obj.Expected_Ship_Date

                UsLock1.Status = IIf(clsCommon.CompairString("In Progress", obj.Status) = CompairStringResult.Equal, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

                If UsLock1.Status = ERPTransactionStatus.Pending Then
                    btnUpdateWithCustomerNewPrice.Visible = True
                End If



                If clsCommon.CompairString("Y", obj.On_Hold) = CompairStringResult.Equal Then
                    chkOnHold.Checked = True
                Else
                    chkOnHold.Checked = False
                End If
                If clsCommon.CompairString("Y", obj.Multiple_Orders) = CompairStringResult.Equal Then
                    chkMultipleOrder.Checked = True
                Else
                    chkMultipleOrder.Checked = False
                End If

                If obj.is_Sample > 0 Then
                    chkSample.Checked = True
                    txtDiscPer.ReadOnly = True
                Else
                    chkSample.Checked = False
                    txtDiscPer.ReadOnly = False
                End If

                txtRef.Text = obj.Ref_No

                txtDesc.Text = obj.Description
                txtRemarks.Text = obj.Remarks
                txtMannualAmt.Text = clsCommon.myFormat(obj.Mannual_Invoice_Amt)
                txtMannualQty.Text = clsCommon.myFormat(clsCommon.myCdbl(obj.Mannual_Invoice_Qty))
                chkCreateEmpty.Checked = obj.Is_Create_Empty
                txtShipTo.Value = obj.Ship_To
                lblShipTo.Text = obj.Ship_To_Desc

                TAXGROUP = obj.Tax_Group
                txtLocation.Value = obj.Location
                basicamt1 = obj.Shipment_Detail_Total_Amt
                txtTotalTaxAmount.Text = clsCommon.myFormat(obj.Shipment_Tax_Amt)
                txtFreight.Text = clsCommon.myFormat(obj.Freight_Amt)
                txtOtherCharges.Text = clsCommon.myFormat(obj.Other_Charges)
                txtAdditionalCharges.Text = clsCommon.myFormat(obj.Add_Charges)
                txtShipmentAmt.Text = clsCommon.myFormat(obj.Total_Order_Amt)
                lblSaleInvoiceNo.Text = obj.Invoice_No
                If clsCommon.CompairString(obj.Shipment_Type, "Sale") = CompairStringResult.Equal Then
                    txtOrderDate.Value = obj.Order_Date
                    txtOrderNo.Value = obj.Order_No
                Else
                    txtTransferDate.Value = obj.Transfer_Date
                    txtTransferNo.Value = obj.Transfer_No
                End If
                txtEmployeeCode.Value = obj.Employee_Code
                sql = "SELECT  Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_SALESMAN_MAPPING WHERE Salesman_Code='" + obj.Route_No + "'"
                Dim dtable As DataTable = clsDBFuncationality.GetDataTable(sql)
                If dtable IsNot Nothing AndAlso dtable.Rows.Count > 0 Then
                    l1User = dtable.Rows(0)(0).ToString()
                    l2User = dtable.Rows(0)(1).ToString()
                    l3User = dtable.Rows(0)(2).ToString()
                    l4User = dtable.Rows(0)(3).ToString()
                    l5User = dtable.Rows(0)(4).ToString()
                Else
                    common.clsCommon.MyMessageBoxShow("Salesman does not exist.")
                    txtSalesman.Focus()
                    Return False
                    Exit Function
                End If
                txtTotalTPT.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(SUM(ISNULL(Total_TPT,0)),0)  FROM TSPL_SHIPMENT_DETAILS  WHERE Shipment_No = '" + txtDocNo.Value + "'")
                cboModeOfTransport.Text = obj.Mode_Of_Transport
                txtVehicleCode.Value = obj.Vehicle_Code
                lblVhicleNo.Text = obj.Vehicle_No
                txtKMReading.Text = obj.KM_Reading
                txtRouteNo.Value = obj.Route_No
                lblRouteDesc.Text = obj.Route_Desc
                txtSalesman.Value = obj.Salesman_Code
                fndSalesman_TextChanged()
                txtTripNo.Text = obj.Trip_No
                txtSchemeSample.txtValue.Text = obj.Scheme_Sample_Code
                cboPriceDate.Text = Format(CDate(obj.Price_Date), "dd/MM/yyyy")
                txtPaymentTerm.Value = obj.Terms_Code

                chkDiscountOnRate.IsChecked = IIf(obj.Discount_On = 0, True, False)
                chkDiscountOnAmt.IsChecked = IIf(obj.Discount_On = 1, True, False)

                btnAdd.Text = "Update"
                cboLoadOutType.Enabled = False
                txtCustomer.Enabled = False

                txtDate.Enabled = False
                If clsCommon.myLen(obj.Order_No) > 0 Then
                    txtDate.Enabled = True
                End If

                txtLocation.Enabled = False
                txtTransferNo.Enabled = False
                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    btnAdd.Enabled = False
                    btnSaveAndPrint.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btnSettlement.Enabled = False
                Else
                    btnAdd.Enabled = True
                    btnSaveAndPrint.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                    btnSettlement.Enabled = True
                End If
                Dim MRP As Decimal = totalMRP()
                Dim basicAmt As Decimal = totalBasicAmt()
                Dim netAmt As Decimal = totalNetAmount()


                dtEndTime = DateTime.Now
                span = dtEndTime.Subtract(dtStartTime)
                strMsg += "Data Load From Transaction:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
                dtStartTime = DateTime.Now

                funfilldetail(obj)
                If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then ''Against Transfer
                    dtEndTime = DateTime.Now
                    span = dtEndTime.Subtract(dtStartTime)
                    strMsg += "Fill SAved Data:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
                    dtStartTime = DateTime.Now
                    If UsLock1.Status = ERPTransactionStatus.Pending Then
                        Dim dtNoOfUnits As DataTable = clsDBFuncationality.GetDataTable("select distinct Unit_code from TSPL_SHIPMENT_DETAILS where Shipment_No='" + txtDocNo.Value + "'")
                        Dim strUOMType As String = "FC"
                        If dtNoOfUnits.Rows.Count > 1 Then
                            strUOMType = "ALL"
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dtNoOfUnits.Rows(0)("Unit_code")), "FC") = CompairStringResult.Equal Then
                            strUOMType = "FC"
                        Else
                            strUOMType = "FB"
                        End If
                        funfilltransfer(True, strUOMType)
                        dtEndTime = DateTime.Now
                        span = dtEndTime.Subtract(dtStartTime)
                        strMsg += "Fill Unsaved Data:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
                        dtStartTime = DateTime.Now
                    End If
                    LoadPendingBalanceAgainstTransfer()
                Else ''Direct Sale Invoice  
                    If UsLock1.Status = ERPTransactionStatus.Pending AndAlso clsCommon.myLen(obj.Order_No) <= 0 Then
                        priceDateSelection1(TAXGROUP)
                    End If
                End If

                If clsCommon.myCdbl(txtDiscPer.Text) < 100 Then
                    txtShipmentTotal.Text = clsCommon.myCdbl(txtDiscAmt.Text) + clsCommon.myCdbl(txtNetShipAmt.Text)
                ElseIf clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
                    txtShipmentTotal.Text = clsCommon.myCdbl(txtDiscAmt.Text)
                    txtNetShipAmt.Text = 0
                End If

                funtotalfcfb()
                txtShipmentAmt.Text = shipmentamt
                txtTotalShipmentAmt.Text = clsCommon.myCdbl(txtContainerDeposit.Text) + clsCommon.myCdbl(txtShipmentAmt.Text)
                txtTotalTaxAmount.Text = taxamt1

                RemoveHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
                fndTaxGroup.txtValue.Text = ""
                fndTaxGroup.txtValue.Text = TAXGROUP
                lblTaxDesc.Text = clsTaxGroupMaster.GetNameOfSaleType(fndTaxGroup.txtValue.Text, Nothing)
                sql = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code " & _
           " WHERE G.Tax_Group_Code = '" + TAXGROUP + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " ORDER BY G.Trans_Code"
                ds = clsDBFuncationality.GetDataTable(sql)
                If ds.Rows.Count > 0 Then
                    gvTax.DataSource = ds
                End If
                AddHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
                AddHandler txtSchemeSample.txtValue.TextChanged, AddressOf fndSchemeSample_TextChanged
                AddHandler txtAdditionalCharges.TextChanged, AddressOf totalAmounts
                AddHandler txtOtherCharges.TextChanged, AddressOf totalAmounts
                AddHandler txtFreight.TextChanged, AddressOf totalAmounts
                AddHandler txtCustDisc.TextChanged, AddressOf totalAmounts
                AddHandler cboLoadOutType.SelectedIndexChanged, AddressOf ddlLoadOutType_SelectedIndexChanged
                AddHandler txtDiscPer.TextChanged, AddressOf MyTextBox1_TextChanged

                gvTax.AllowAddNewRow = False
                Dim i1 As Integer
                gvTax.DataSource = Nothing
                gvTax.Rows.Clear()
                For i1 = 1 To 10
                    sql = "Select (case When Tax" + i1.ToString() + " is NULL THEN '' else Tax" + i1.ToString() + " end),Tax" + i1.ToString() + "_Rate,Tax" + i1.ToString() + "_Assessable_Amt,Tax" + i1.ToString() + "_Amt from TSPL_SHIPMENT_MASTER WHERE Shipment_No='" + txtDocNo.Value + "'"
                    ds = clsDBFuncationality.GetDataTable(sql)
                    If Not ds.Rows(0)(0).ToString() = String.Empty Then
                        Dim taxCode As String = ds.Rows(0)(0).ToString()
                        Dim taxRate As Decimal = clsCommon.myCdbl(ds.Rows(0)(1))
                        Dim assAmt As Decimal = clsCommon.myCdbl(ds.Rows(0)(2))
                        Dim taxAmt As Decimal = clsCommon.myCdbl(ds.Rows(0)(3))
                        If Not ds.Rows(0)(0).ToString() = "" Then
                            sql = "Select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code='" + ds.Rows(0)(0).ToString() + "'"
                            Dim taxCodeDesc As String = clsDBFuncationality.getSingleValue(sql)
                            Dim grow As GridViewRowInfo = gvTax.Rows.AddNew()
                            grow.Cells("taxAuthority").Value = taxCode
                            grow.Cells("description").Value = taxCodeDesc
                            grow.Cells("taxRate").Value = taxRate
                            grow.Cells(colBasicAmount).Value = basicamt1
                            grow.Cells("assessibleAmount").Value = assAmt
                            grow.Cells(colTaxamount).Value = taxAmt

                        End If
                    End If
                Next
                Dim SQLTAX As String = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code as [taxcode]  FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code  WHERE G.Tax_Group_Code = '" + fndTaxGroup.txtValue.Text + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code  ORDER BY G.Trans_Code"
                ds = clsDBFuncationality.GetDataTable(SQLTAX)

                For k As Integer = 0 To ds.Rows.Count - 1
                    gvTax.Rows(k).Cells("taxable").Value = ds.Rows(k)("Taxable")
                    gvTax.Rows(k).Cells("surtax").Value = ds.Rows(k)("surtax")
                    gvTax.Rows(k).Cells(8).Value = ds.Rows(k)("taxcode")
                Next

            Else
                btnAdd.Text = "Save"
                btnDelete.Enabled = False
                btnPost.Enabled = False
                btnSettlement.Enabled = False
                resetFNDCustomer()
                resetFNDTaxGroup()
                resetForm()
            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
        Finally
            isInsideLoadData = False
        End Try
        funSetFirstRow()

        dtEndTime = DateTime.Now
        span = dtEndTime.Subtract(dtStartTime)
        strMsg += "Tax fill:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
        dtStartTime = DateTime.Now
        Return Nothing
       
    End Function

    Private Sub LoadPendingBalanceAgainstTransfer()
        If clsCommon.myLen(txtTransferNo.Value) > 0 Then


            Dim qry As String = "select Transfer_No,Item_Code,sum(Item_Qty * case when RI in (1,5) then 1 else case when RI in (2,3,4) then -1 else 0 end end) as BalanceQty,MRP from (" + Environment.NewLine
            qry += " select Transfer_No,Item_Code,Price_Date,MRP,Item_Qty,1 as RI from TSPL_TRANSFER_DETAIL where Transfer_No='" + txtTransferNo.Value + "'" + Environment.NewLine
            qry += " union all " + Environment.NewLine
            qry += " select TSPL_SHIPMENT_MASTER.Transfer_No as Transfer_No,TSPL_SHIPMENT_DETAILS.Item_Code,TSPL_SHIPMENT_DETAILS.Price_Date,MRP_Amt*Conversion_Factor as MRP,TSPL_SHIPMENT_DETAILS.Shipped_Qty /Conversion_Factor as Item_Qty ,2 as RI" + Environment.NewLine
            qry += " from TSPL_SHIPMENT_DETAILS " + Environment.NewLine
            qry += "left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No" + Environment.NewLine
            qry += "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SHIPMENT_DETAILS.Unit_code" + Environment.NewLine
            qry += "where TSPL_SHIPMENT_MASTER.Is_Post='Y' and  TSPL_SHIPMENT_MASTER.Transfer_No='" + txtTransferNo.Value + "' " + Environment.NewLine
            qry += "union all " + Environment.NewLine
            qry += "select TSPL_SHIPMENT_MASTER.Transfer_No as Transfer_No,TSPL_SHIPMENT_DETAILS.Item_Code,TSPL_SHIPMENT_DETAILS.Price_Date,MRP_Amt*Conversion_Factor as MRP,TSPL_SHIPMENT_DETAILS.Shipped_Qty /Conversion_Factor   as Item_Qty ,3 as RI" + Environment.NewLine
            qry += " from TSPL_SHIPMENT_DETAILS" + Environment.NewLine
            qry += "left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No" + Environment.NewLine
            qry += "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SHIPMENT_DETAILS.Unit_code" + Environment.NewLine
            qry += "where TSPL_SHIPMENT_MASTER.Is_Post='N' and  TSPL_SHIPMENT_MASTER.Transfer_No='" + txtTransferNo.Value + "'  and TSPL_SHIPMENT_MASTER.Shipment_No not in('" + txtDocNo.Value + "') " + Environment.NewLine
            qry += " union all " + Environment.NewLine
            qry += " select TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Price_Date,MRP*Conversion_Factor as MRP,(ISNULL( TSPL_TRANSFER_DETAIL.Burst,0)+isnull(TSPL_TRANSFER_DETAIL.Leak,0)+isnull(TSPL_TRANSFER_DETAIL.Shortage,0)+TSPL_TRANSFER_DETAIL.LoadIn_Qty) /Conversion_Factor  as Item_Qty  ,4 as RI" + Environment.NewLine
            qry += " from TSPL_TRANSFER_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_DETAIL.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No" + Environment.NewLine
            qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_TRANSFER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TRANSFER_DETAIL.Uom" + Environment.NewLine
            qry += "where TSPL_TRANSFER_HEAD.Load_Out_No='" + txtTransferNo.Value + "' and Transfer_Type='LI'  "

            qry += " union all "
            qry += " select TSPL_SHIPMENT_MASTER.Transfer_No as Transfer_No,TSPL_SALE_RETURN_DETAIL.Item_Code,TSPL_SALE_RETURN_DETAIL.Price_Date,MRP_Amt*Conversion_Factor as MRP,  TSPL_SALE_RETURN_DETAIL.Return_Qty/Conversion_Factor  as Item_Qty  ,5 as RI"
            qry += " from TSPL_SALE_RETURN_DETAIL"
            qry += " left outer join TSPL_SALE_RETURN_HEAD on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No"
            qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_RETURN_HEAD.Invoice_No"
            qry += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No"
            qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_RETURN_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_RETURN_DETAIL.Unit_code"
            qry += " where "
            qry += " TSPL_SHIPMENT_MASTER.Transfer_No='" + txtTransferNo.Value + "' and "
            qry += " TSPL_SHIPMENT_MASTER.Shipment_Type='Transfer'  "
            qry += " and LEN(ISNULL(TSPL_SHIPMENT_MASTER.Transfer_No,''))>0"


            qry += ") xxx group by Transfer_No,Item_Code,MRP"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            dtEndTime = DateTime.Now
            span = dtEndTime.Subtract(dtStartTime)
            strMsg += "Pending query :" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
            dtStartTime = DateTime.Now






            dtEndTime = DateTime.Now
            span = dtEndTime.Subtract(dtStartTime)
            strMsg += "Loop:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
            dtStartTime = DateTime.Now


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim dv As DataView = dt.DefaultView
                For ii As Integer = 0 To gv1.RowCount - 1
                    Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(ColICode).Value)
                    Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitCode).Value)
                    Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim dblMRP As Double = clsCommon.myCstr(clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value) * dblConvFac)

                    dv.RowFilter = " Item_Code='" + strICode + "' and  MRP='" + clsCommon.myCstr(dblMRP) + "'"
                    Dim dtTemp As DataTable = dv.ToTable()
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal Then
                            gv1.Rows(ii).Cells(ColActualBalQty).Value = Math.Round(clsCommon.myCdbl(dtTemp.Rows(0)("BalanceQty")) * dblConvFac, 0, MidpointRounding.ToEven)
                        Else
                            gv1.Rows(ii).Cells(ColActualBalQty).Value = Math.Round(clsCommon.myCdbl(dtTemp.Rows(0)("BalanceQty")), 2, MidpointRounding.ToEven)
                        End If
                    End If
                Next
            End If


            dtEndTime = DateTime.Now
            span = dtEndTime.Subtract(dtStartTime)
            strMsg += "row filter:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
            dtStartTime = DateTime.Now


        End If
    End Sub

    Private Sub funfilldetail(ByVal obj As clsShipmentMaster)
        LoadBlankGrid()
        'Dim i As Integer
        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            For Each objtr As clsShipmentDetail In obj.Arr
                Dim datarowinfo As GridViewRowInfo = gv1.Rows.AddNew()
                datarowinfo.Cells(ColComplete).Value = IIf(clsCommon.CompairString("N", objtr.Complete) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colPriceDateColumn).Value = objtr.Price_Date
                datarowinfo.Cells(colOrderedQty).Value = objtr.Order_Qty
                sql = "SELECT isnull(SUM(isnull(Item_Qty,0)),0) FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objtr.Item_Code + "' AND Location_Code='" + objtr.Location + "' and MRP='" + clsCommon.myCstr(objtr.MRP_Amt) + "'"
                Try
                    If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                        datarowinfo.Cells(colBalanceQty).Value = objtr.Balance_Qty
                    Else
                        datarowinfo.Cells(colBalanceQty).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql)), 2)
                    End If
                Catch ex As Exception
                    datarowinfo.Cells(colBalanceQty).Value = 0
                End Try
                datarowinfo.Cells(colShippedQty).Value = objtr.Shipped_Qty
                datarowinfo.Cells(colUnitCode).Value = objtr.Unit_code
                datarowinfo.Cells(collocation).Value = objtr.Location
                datarowinfo.Cells(colPriceCode).Value = obj.Price_Code
                datarowinfo.Cells(colSchemeApplicable).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Applicable) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeCodeItem).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Item) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeItem).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Disc_Applicable) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeApplicable).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Applicable) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeCodeItem).Value = objtr.Scheme_Code_Qty
                datarowinfo.Cells(colSchemeItem).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Item) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeDiscountApplicable).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Disc_Applicable) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeCodeDiscount).Value = objtr.Scheme_Code_Cash
                datarowinfo.Cells(colSampleItem).Value = IIf(clsCommon.CompairString("N", objtr.Sampling_Item) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colEmptyValue).Value = objtr.Empty_Value
                datarowinfo.Cells(ColICode).Value = objtr.Item_Code
                datarowinfo.Cells(colMRP).Value = objtr.MRP_Amt
                datarowinfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), clsCommon.myCdbl(datarowinfo.Cells(colMRP).Value))
                datarowinfo.Cells(colBasicAmount).Value = objtr.Basic_Rate
                datarowinfo.Cells(colDiscountAmount).Value = objtr.Disc_Amt
                datarowinfo.Cells(colitemNetAmount).Value = objtr.Item_Net_Amt
                datarowinfo.Cells(colTaxamount).Value = objtr.Item_Net_Amt
                datarowinfo.Cells(colTotalMRP).Value = objtr.Total_MRP_Amt
                datarowinfo.Cells(colTotalBasicAmount).Value = objtr.Total_Basic_Amt
                datarowinfo.Cells(colTotalDiscountAmount).Value = objtr.Total_Disc_Amt
                datarowinfo.Cells(colTotalNetAmount).Value = objtr.Total_net_Amt
                datarowinfo.Cells(colTotalTaxAmount).Value = objtr.Total_Tax_Amt
                datarowinfo.Cells(colTPT).Value = objtr.TPT
                datarowinfo.Cells(colTotalTPT).Value = Math.Round(objtr.TPT * objtr.Shipped_Qty, 4)
                datarowinfo.Cells(colTotalItemAmount).Value = objtr.Total_Item_Amt
                datarowinfo.Cells(colPromoSchemeApplicable).Value = IIf(clsCommon.CompairString("N", objtr.Promo_Scheme_Applicable) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colPromoSchemeCode).Value = objtr.Promo_Scheme_Code
                datarowinfo.Cells(colPromoSchemeItem).Value = IIf(clsCommon.CompairString("N", objtr.Promo_Scheme_Item) = CompairStringResult.Equal, "No", "Yes")

                datarowinfo.Cells(colFromSchemeCode).Value = objtr.From_Scheme_Code
                datarowinfo.Cells(colEmptyValueShell).Value = objtr.Empty_Value_Shell
                datarowinfo.Cells(colEmptyValueBottle).Value = objtr.Empty_Value_Bottle
                datarowinfo.Cells(colTransferBasicAmount).Value = objtr.Transfer_Basic_Amount
                datarowinfo.Cells(ColItemName).Value = objtr.Item_Desc
                datarowinfo.Cells(colBatchNumber).Value = objtr.Batch_No
                datarowinfo.Cells(colcustDiscount).Value = objtr.Cust_Discount
                datarowinfo.Cells(colTotalCustDiscount).Value = objtr.Total_Cust_Discount
                datarowinfo.Cells(ColCustDisNoTax).Value = objtr.Cust_Item_Discount_NoTax
                datarowinfo.Cells(colDiscountCode).Value = objtr.Discount_Code
                datarowinfo.Cells(ColTargetDisAmt).Value = objtr.Target_Discount_Amt
                datarowinfo.Cells(ColPriceToShow).Value = objtr.Price_To_Show
                If objtr.Price_Date_Actual.HasValue Then
                    datarowinfo.Cells(colPriceDateActual).Value = objtr.Price_Date_Actual
                End If
                datarowinfo.Cells(colAbatementRate).Value = objtr.Abatement_rate
                datarowinfo.Cells(colMainItem).Value = objtr.Main_Item
                If clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal Then
                    If clsCommon.myLen(obj.Order_No) > 0 Then
                        datarowinfo.Cells(ColActualBalQty).Value = clsSalesOrderDetail.GetBalanceQty(obj.Order_No, objtr.Item_Code, objtr.Unit_code, obj.Shipment_No, objtr.From_Scheme_Code)
                        Dim convFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), Nothing)
                        If convFac = 1 Then
                            datarowinfo.Cells(colBalanceQty).Value = clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), txtLocation.Value, clsCommon.myCstr(datarowinfo.Cells(colMRP).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), txtDocNo.Value, txtDate.Value)
                        End If
                    Else
                        Dim convFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), Nothing)
                        If convFac = 1 Then
                            datarowinfo.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), txtLocation.Value, clsCommon.myCstr(datarowinfo.Cells(colMRP).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), txtDocNo.Value, txtDate.Value)
                        End If
                    End If
                End If
                datarowinfo.Cells(colTax1Rate).Value = objtr.TAX1_Rate
                datarowinfo.Cells(colAssess1).Value = objtr.TAX1_Assessable_Amt
                datarowinfo.Cells(colTax1Amt).Value = objtr.TAX1_Amt
                datarowinfo.Cells(colTax2Rate).Value = objtr.TAX2_Rate
                datarowinfo.Cells(colAssess2).Value = objtr.TAX2_Assessable_Amt
                datarowinfo.Cells(colTax2Amt).Value = objtr.TAX2_Amt
                datarowinfo.Cells(colTax3Rate).Value = objtr.TAX3_Rate
                datarowinfo.Cells(colAssess3).Value = objtr.TAX3_Assessable_Amt
                datarowinfo.Cells(colTax3Amt).Value = objtr.TAX3_Amt
                datarowinfo.Cells(colTax4Rate).Value = objtr.TAX4_Rate
                datarowinfo.Cells(colAssess4).Value = objtr.TAX4_Assessable_Amt
                datarowinfo.Cells(colTax4Amt).Value = objtr.TAX4_Amt
                datarowinfo.Cells(colTax5Rate).Value = objtr.TAX5_Rate
                datarowinfo.Cells(colAssess5).Value = objtr.TAX5_Assessable_Amt
                datarowinfo.Cells(colTax5Amt).Value = objtr.TAX5_Amt
                datarowinfo.Cells(colTax6Rate).Value = objtr.TAX6_Rate
                datarowinfo.Cells(colAssess6).Value = objtr.TAX6_Assessable_Amt
                datarowinfo.Cells(colTax6Amt).Value = objtr.TAX6_Amt
                gv1.Refresh()
            Next
        End If
        Dim check As Integer = gvTax.Rows.Count
    End Sub

    Private Function AllowToSave() As Boolean
        gv1.CurrentColumn = gv1.Columns(ColItemName)
        If gv1.CurrentRow Is Nothing Then
            gv1.CurrentRow = gv1.Rows(0)
        End If
        Dim index As Integer = gv1.CurrentRow.Index
        Dim arrTax1Rate As New List(Of Decimal)
      

        If txtDate.ShowCheckBox AndAlso Not txtDate.Checked Then
            txtDate.Focus()
            Throw New Exception("Please select date")
        End If
        Dim isCalculateShall As Boolean = IIf(clsCommon.myCdbl(txtshellqty.Text) > 0, False, True)
        If clsLocation.isLocatinExcisable(txtLocation.Value, Nothing) Then
            isCalculateShall = True
        End If

        If txtDiscAmt.Value > clsCommon.myCdbl(txtShipmentTotal.Text) Then
            Throw New Exception("Discount Amount can't be more than shipment amount")
        End If


        If gv1.CurrentRow.Index < 0 Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value)) > 0 Then
                Throw New Exception("Last Row is not inserted Properly")
            End If
        End If

        'gv1.CurrentRow = gv1.Rows(0)
        'gv1.CurrentColumn = gv1.Columns(0)

        Dim item1, unitcode As String
        Dim discamt As Double = 0
        Dim TotDiscAmt As Double = 0
        If ((Not clsLocation.isLocatinExcisable(txtLocation.Value, Nothing)) AndAlso (Not clsCommon.myCdbl(txtDiscPer.Text) = 100)) Then
            Dim dtCustItemDis As DataTable = clsDBFuncationality.GetDataTable("SELECT Item_Code , Unit_Code , Disc_Amt  FROM TSPL_CUSTOMER_ITEM_DISCOUNT_DETAILS WHERE Cust_Code = '" + txtCustomer.Value + "' and  '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "'>=Start_DATE and ( Valid_Upto is null or '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "'<=Valid_Upto) and Unit_Code='FC'")
            If dtCustItemDis IsNot Nothing AndAlso dtCustItemDis.Rows.Count > 0 Then
                For Each dritemdiscount As DataRow In dtCustItemDis.Rows
                    item1 = clsCommon.myCstr(dritemdiscount("Item_Code"))
                    unitcode = clsCommon.myCstr(dritemdiscount("Unit_Code"))
                    discamt = clsCommon.myCdbl(dritemdiscount("Disc_Amt"))
                    For Each GROW As GridViewRowInfo In gv1.Rows
                        If (clsCommon.CompairString("Yes", clsCommon.myCstr(GROW.Cells(colSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(GROW.Cells(colPromoSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(GROW.Cells(colSampleItem).Value)) = CompairStringResult.Equal) Then
                            GROW.Cells(ColCustDisNoTax).Value = 0
                            GROW.Cells(colcustDiscount).Value = 0
                        Else
                            If clsCommon.CompairString(clsCommon.myCstr(GROW.Cells(ColICode).Value), item1) = CompairStringResult.Equal Then
                                Dim dblFirstTaxRate As Double = clsCommon.myCdbl(GROW.Cells(colTax1Rate).Value)
                                Dim dblCustDisWithoutTax As Double = Math.Round(discamt * 100 / (100 + dblFirstTaxRate), 2, MidpointRounding.ToEven)
                                GROW.Cells(ColCustDisNoTax).Value = dblCustDisWithoutTax
                                GROW.Cells(colcustDiscount).Value = clsCommon.myCstr(discamt)
                            End If
                        End If
                    Next
                Next
            End If
        Else
            For Each GROW As GridViewRowInfo In gv1.Rows
                GROW.Cells(ColCustDisNoTax).Value = 0
                GROW.Cells(colcustDiscount).Value = 0
            Next
        End If


        gv1.CurrentRow = gv1.MasterView.TableAddNewRow
        CheckTransactionDiscount()
        CalculateDiscountAmount()

        If isRakeshSharmaClicked Then
            Return True
        End If

        If btnAdd.Text = "Update" Then
            Dim strchk As String = "select Is_Post from TSPL_SHIPMENT_MASTER where Shipment_No='" + txtDocNo.Value + "'"
            Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
            If chkpost = "Y" Then
                Throw New Exception("Transection already posted")
            End If
        End If

        Dim arrDiscountCode As New Dictionary(Of String, Integer)
        Dim dtCust As DataTable = clsDBFuncationality.GetDataTable("select Cust_Type_Code,Tin_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomer.Value + "'")
        Dim strCustType As String = clsCommon.myCstr(dtCust.Rows(0)("Cust_Type_Code"))
        Dim arrShall As New Dictionary(Of Integer, Double)
        For ii As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(ii).Cells(ColTargetDisAmt).Value = 0
            Dim strSchemeCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colFromSchemeCode).Value)
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(ColICode).Value)
            Dim dblMRP As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            Dim strPriceDate As String = clsCommon.GetPrintDate(clsCommon.myCDate(gv1.Rows(ii).Cells(colPriceDateColumn).Value), "yyyy-MM-dd")
            If clsCommon.myLen(gv1.Rows(ii).Cells(colPriceDateActual).Value) > 0 Then
                strPriceDate = clsCommon.GetPrintDate(clsCommon.myCDate(gv1.Rows(ii).Cells(colPriceDateActual).Value), "yyyy-MM-dd")
            End If
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitCode).Value)
            Dim strMRP As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colMRP).Value)
            gv1.Rows(ii).Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(gv1.Rows(ii).Cells(ColICode).Value), clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitCode).Value), clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value))
            Dim strPriceCode As String = txtPriceCode.Text
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colShippedQty).Value)
            Dim qry As String = ""
            If clsCommon.myLen(strSchemeCode) >= 2 Then
                Dim strTwoCharacher As String = strSchemeCode.Substring(0, 2)
                If clsCommon.CompairString(strTwoCharacher, "MS") = CompairStringResult.Equal Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colMainItem).Value) <= 0 AndAlso clsCommon.myLen(gv1.Rows(ii).Cells(colDiscountCode).Value) <= 0 Then
                        Throw New Exception("Please fill the Main Item/Discount Code at Row No" + clsCommon.myCstr(ii + 1))
                    End If
                    Dim strDisCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colDiscountCode).Value).Trim()
                    If clsCommon.myLen(strDisCode) > 0 Then
                        If Not arrDiscountCode.ContainsKey(strDisCode) Then
                            arrDiscountCode.Add(strDisCode, 0)
                        End If

                        qry = "select Price_Amount1,Price_Amount4,Price_Amount5,Price_Amount6,Price_Amount7,NetLTPT,Price_Amount10 from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' and Start_Date='" + strPriceDate + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "' "
                        Dim dtPC As DataTable = clsDBFuncationality.GetDataTable(qry)
                        If dtPC Is Nothing OrElse dtPC.Rows.Count <= 0 Then
                            Throw New Exception("Price Component not found for item " + strICode + " at Row No " + (clsCommon.myCstr(ii + 1)))
                        End If

                        Dim dblActMRP As Double = clsCommon.myCdbl(dtPC.Rows(0)("NetLTPT")) + clsCommon.myCdbl(dtPC.Rows(0)("Price_Amount10"))
                        Dim dblTargetDiscountAmt As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colShippedQty).Value) * dblActMRP
                        gv1.Rows(ii).Cells(ColTargetDisAmt).Value = dblTargetDiscountAmt
                        arrDiscountCode(strDisCode) += dblTargetDiscountAmt
                    End If
                End If
            End If

            If dblQty > 0 Then
                If isCalculateShall AndAlso clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colEmptyValue).Value) > 0 Then
                    Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    If Not arrShall.ContainsKey(dblConvFac) Then
                        arrShall.Add(dblConvFac, 0)
                    End If
                    arrShall(dblConvFac) += dblQty
                End If
                If Not (checkItemonLocation(gv1.Rows(ii).Cells(ColICode).Value, gv1.Rows(ii).Cells(colShippedQty).Value, gv1.Rows(ii).Cells(collocation).Value, gv1.Rows(ii).Cells(colUnitCode).Value, clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value), False, gv1.Rows(ii).Cells(colBatchNumber).Value)) Then
                    Return False
                End If

                If clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1Rate).Value) > 0 Then
                    If Not arrTax1Rate.Contains(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1Rate).Value)) Then
                        arrTax1Rate.Add(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1Rate).Value))
                    End If
                End If

                

                ''Check for Balance With Unapproved Qty
                If clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal Then
                    Dim strCurrMRP As Double = clsCommon.myCdbl(strMRP)
                    Dim dblOuterConvFac As Double = 0
                    If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal Then
                        dblOuterConvFac = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                        strCurrMRP = clsCommon.myCdbl(strMRP) * dblOuterConvFac
                    ElseIf clsCommon.CompairString(strUOM, "FC") = CompairStringResult.Equal Then
                        dblOuterConvFac = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                        strCurrMRP = clsCommon.myCdbl(strMRP) * dblOuterConvFac
                    End If
                    Dim dblBalQty As Double = clsItemLocationDetails.getBalanceWithUnapprove(strICode, txtLocation.Value, clsCommon.myCstr(strCurrMRP), strUOM, txtDocNo.Value, txtDate.Value)
                    Dim dblEnteredQty As Double = dblQty
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(ColICode).Value)
                        Dim strUOMInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnitCode).Value)
                        Dim dblMRPInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colMRP).Value)
                        Dim dblQtyInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colShippedQty).Value)

                        Dim dblInnerConvFac As Double = 0
                        If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal Then
                            dblInnerConvFac = clsItemMaster.GetConvertionFactor(strICodeInner, strUOM, Nothing)
                            dblMRPInner = dblMRPInner / dblInnerConvFac
                        ElseIf clsCommon.CompairString(strUOM, "FC") = CompairStringResult.Equal Then
                            dblInnerConvFac = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                            dblMRPInner = dblMRPInner * dblInnerConvFac
                        End If

                        If dblQtyInner > 0 AndAlso dblMRPInner = clsCommon.myCdbl(strMRP) AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal Then
                            If clsCommon.CompairString(strUOM, strUOMInner) = CompairStringResult.Equal Then
                                dblEnteredQty += dblQtyInner
                            Else
                                If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal Then
                                    dblEnteredQty += (dblQtyInner * dblInnerConvFac)
                                ElseIf clsCommon.CompairString(strUOM, "FC") = CompairStringResult.Equal Then
                                    dblEnteredQty += (dblQtyInner / dblInnerConvFac)
                                End If
                            End If
                        End If
                    Next
                    dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                    If dblEnteredQty > dblBalQty Then
                        Throw New Exception("Item - " + strICode + " , MRP - " + strMRP + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                    End If
                    If txtOrderNo.Visible AndAlso clsCommon.myLen(txtOrderNo.Value) > 0 AndAlso Not (clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) Then
                        Dim OrderBalanceQty As Double = clsSalesOrderDetail.GetBalanceQty(txtOrderNo.Value, strICode, strUOM, txtDocNo.Value, clsCommon.myCstr(gv1.Rows(ii).Cells(colFromSchemeCode).Value))
                        If dblQty > OrderBalanceQty Then
                            Throw New Exception("Item - " + strICode + " , UOM - " + strUOM + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblQty) + " and Order Balance Quantity - " + clsCommon.myCstr(OrderBalanceQty))
                        End If
                    End If
                Else

                    Dim dblOuterConvFac As Double = 0
                    dblOuterConvFac = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                    Dim dblBalQty As Double = clsTransferMaster.getBalanceWithUnapproveTranferInFC(txtTransferNo.Value, strICode, txtDocNo.Value, dblMRP, strUOM)
                    dblBalQty = dblBalQty * dblOuterConvFac
                    If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal Then
                        dblBalQty = Math.Round(dblBalQty, 0, MidpointRounding.ToEven)
                    Else
                        dblBalQty = Math.Round(dblBalQty, 2, MidpointRounding.ToEven)
                    End If

                    Dim dblEnteredQty As Double = dblQty
                    For jj As Integer = 0 To gv1.Rows.Count - 1
                        If ii = jj Then
                            Continue For
                        End If
                        Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(ColICode).Value)
                        Dim strUOMInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnitCode).Value)
                        Dim dblMRPInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colMRP).Value)
                        Dim dblQtyInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colShippedQty).Value)
                        Dim dblInnerConvFac As Double = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)

                        If dblQtyInner > 0 AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal Then
                            If clsCommon.CompairString(strUOM, strUOMInner) = CompairStringResult.Equal Then
                                dblEnteredQty += dblQtyInner
                            Else
                                If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal Then
                                    dblEnteredQty += (dblQtyInner * dblOuterConvFac)
                                ElseIf clsCommon.CompairString(strUOM, "FC") = CompairStringResult.Equal Then
                                    dblEnteredQty += (dblQtyInner / dblInnerConvFac)
                                End If
                            End If
                        End If
                    Next
                    dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                    If dblEnteredQty > dblBalQty Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        gv1.CurrentColumn = gv1.Columns(colShippedQty)
                        Throw New Exception("Item - " + strICode + " Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                    End If

                End If
                ''End of Check for Balance With Unapproved Qty


                qry = "select Start_Date from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' and Start_Date='" + strPriceDate + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "' "
                If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                    qry = "select max(Start_Date) from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "' and Start_Date<='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "'"
                    If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))) <= 0 Then
                        Throw New Exception("Error " + Environment.NewLine + "Price Date does not exist for Item_Code='" + strICode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "'")
                        Return False
                    End If
                    strPriceDate = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry)), "")
                End If
            End If
        Next

        If isCalculateShall AndAlso clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal AndAlso arrShall.Count > 0 Then
            Dim dblTotShall As Double
            For Each Keys As Integer In arrShall.Keys
                dblTotShall += Math.Ceiling(arrShall(Keys) / Keys)
            Next
            txtshellqty.Text = clsCommon.myCstr(dblTotShall)
        End If

        If arrDiscountCode IsNot Nothing AndAlso arrDiscountCode.Count > 0 Then
            Dim dt As DataTable = clsTargetMaster.GetBalance(txtDocNo.Value, txtCustomer.Value, txtDate.Value)
            For Each strDisCode As String In arrDiscountCode.Keys
                Dim isFound As Boolean = False
                For Each dr As DataRow In dt.Rows
                    If clsCommon.CompairString(strDisCode, clsCommon.myCstr(dr("Discount_Type"))) = CompairStringResult.Equal Then
                        isFound = True
                        If arrDiscountCode(strDisCode) > clsCommon.myCdbl(dr("Amount")) Then
                            Throw New Exception("Available Target Balance :" + clsCommon.myCstr(clsCommon.myCdbl(dr("Amount"))) + Environment.NewLine + "Total Enterd Target Amount : " + clsCommon.myCstr(arrDiscountCode(strDisCode)) + Environment.NewLine + "For Discount Code :" + strDisCode)
                        End If
                    End If
                Next
                If Not isFound Then
                    Throw New Exception("Available Target Balance : 0" + Environment.NewLine + "Total Enterd Target Amount : " + clsCommon.myCstr(arrDiscountCode(strDisCode)) + Environment.NewLine + "For Discount Code :" + strDisCode)
                End If
            Next
        End If
        'If Not chkSample.Checked AndAlso clsCommon.myCdbl(txtDiscPer.Text) >= 100 Then
        '    Throw New Exception("Discount Percentage can't be " + clsCommon.myCstr(txtDiscPer.Text))
        'End If

        If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
            If Not clsCommon.myCdbl(txtShipmentAmt.Text) = 0 AndAlso txtMannualAmt.Value <= 0 Then
                txtMannualAmt.Focus()
                Throw New Exception("Please Enter Mannual Invoice Amount")
            End If
            If clsCommon.myLen(txtcustomerinvoiceno.Text) > 0 Then
                Dim qry As String = "select Shipment_No from TSPL_SHIPMENT_MASTER where Transfer_No='" + txtTransferNo.Value + "' and Customer_Invoice_No='" + txtcustomerinvoiceno.Text + "' and Shipment_No not in ('" + txtDocNo.Value + "')"
                Dim strShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                If clsCommon.myLen(strShipmentNo) > 0 Then
                    txtcustomerinvoiceno.Focus()
                    Throw New Exception("Customer Invoice no already used in shipment no " + strShipmentNo)
                End If
            Else
                txtcustomerinvoiceno.Focus()
                Throw New Exception("Please Enter Customer Invoice No  ")
            End If

            If clsCommon.myCDate(txtDate.Value, "dd/MMM/yyyy") < clsCommon.myCDate(txtTransferDate.Value, "dd/MMM/yyyy") Then
                Throw New Exception("Transfer Date Can't be Greate than Transaction Date")
            End If


        End If
        If clsCommon.myLen(txtShipTo.Value) > 0 Then
            Dim qry As String = "select Ship_To_Type_Code from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipTo.Value + "' and Ship_To_Type_Code='" + txtCustomer.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Ship to location is not for current customer")
            End If
        End If

        If clsLocation.isLocatinExcisable(txtLocation.Value, Nothing) Then
            Dim ArrFraction As New Dictionary(Of Integer, Double)()
            For Each g As GridViewRowInfo In gv1.Rows
                Dim dblQty As Double = clsCommon.myCdbl(g.Cells(colShippedQty).Value)
                If dblQty > 0 Then
                    Dim dblConvFac As Integer = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(g.Cells(ColICode).Value), clsCommon.myCstr(g.Cells(colUnitCode).Value), Nothing)
                    If Not dblConvFac = 1 Then
                        If ArrFraction.ContainsKey(dblConvFac) Then
                            ArrFraction(dblConvFac) = ArrFraction(dblConvFac) + dblQty
                        Else
                            ArrFraction.Add(dblConvFac, dblQty)
                        End If
                    End If
                End If
            Next
            For Each Keys As Integer In ArrFraction.Keys
                Dim remendor As Double = (ArrFraction(Keys) Mod Keys)
                If remendor <> 0 Then
                    Throw New Exception("Fraction Qty is not allowed in Excise Sale Invoice.")
                End If
            Next
        End If

        If chkDiscountOnAmt.IsChecked AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CashDiscountFromClaimMaster, clsFixedParameterCode.CashDiscountFromClaimMaster, Nothing)) = 1 Then
            Dim dblBalAmt As Double = clsClaimDetails.GetBalanceOfClaim(txtCustomer.Value, txtDate.Value, lblSaleInvoiceNo.Text, Nothing)
            If txtDiscAmt.Value > dblBalAmt Then
                Throw New Exception("Approved balance of Discount Claim is : " + clsCommon.myFormat(dblBalAmt) + " And Cash discount is : " + clsCommon.myFormat(txtDiscAmt.Value))
            End If
        End If

        If txtDiscAmt.Value > 0 AndAlso arrTax1Rate.Count > 1 Then
            Throw New Exception("Claim amount is not applicable for more than one flavour")
        End If

        If index > 0 Then
            gv1.CurrentRow = gv1.Rows(index)
        End If
        Return funvalidatevehicle()

    End Function

    Private Function saveDataClicked() As Boolean
        Try
            Return saveDataClickedNew()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try

    End Function

    Private Function saveDataClickedNew() As Boolean
        Try
            If AllowToSave() Then
                savedata()
            Else
                Return False
            End If
        Catch ex As Exception
            If ex.Message.Contains("deadlocked") Then
                iiDeadlockErrors += 1
                If iiDeadlockErrors >= 15 Then
                    Me.Close()
                    Exit Function
                End If
                System.Threading.Thread.Sleep(3000)
                saveDataClicked()
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
        Return True
    End Function

    Public Sub savedata()
        Try
            Dim shipmentTaxAmt As Decimal = 0.0
            Dim netAmount As Decimal = totalNetAmount()
            Dim shipmentDiscPer As Decimal = 0.0
            Dim shipmentDiscAmt As Decimal = 0.0
            Dim additionalCharges As Decimal = 0.0
            Dim OtherCharges As Decimal = 0.0
            Dim freightCharges As Decimal = 0.0
            Dim onHold As String = "N"

            If validateData() Then
                sql = "SELECT  Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_SALESMAN_MAPPING WHERE Salesman_Code='" + txtRouteNo.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    l1User = Convert.ToString(dt.Rows(0)(0))
                    l2User = Convert.ToString(dt.Rows(0)(1))
                    l3User = Convert.ToString(dt.Rows(0)(2))
                    l4User = Convert.ToString(dt.Rows(0)(3))
                    l5User = Convert.ToString(dt.Rows(0)(4))

                Else
                    Throw New Exception("Salesman does not exist.")
                    txtSalesman.Focus()
                    Exit Sub
                End If

                If chkOnHold.Checked = True Then
                    onHold = "Y"
                End If

                If Not txtAdditionalCharges.Text = String.Empty Then
                    additionalCharges = txtAdditionalCharges.Text
                End If
                If Not txtOtherCharges.Text = String.Empty Then
                    OtherCharges = txtOtherCharges.Text
                End If
                If Not txtFreight.Text = String.Empty Then
                    freightCharges = txtFreight.Text
                End If

                For Each grow As GridViewRowInfo In gvTax.Rows
                    shipmentTaxAmt = shipmentTaxAmt + clsCommon.myCdbl(grow.Cells(5).Value)
                Next

                shipmentDiscPer = clsCommon.myCdbl(txtDiscPer.Text)
                shipmentDiscAmt = clsCommon.myCdbl(txtDiscAmt.Text) + clsCommon.myCdbl(txtCustDisc.Text)



                insertData(totalMRP, totalBasicAmt, totalAssessibleAmt, totalDiscount, shipmentDiscAmt, shipmentDiscPer, shipmentTaxAmt, netAmount, additionalCharges, OtherCharges, freightCharges, totalTransport, onHold)

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        SaveMainClick()
    End Sub

    Private Sub SaveMainClick()
        Try
            iiDeadlockErrors = 1
            If saveDataClicked() AndAlso iiDeadlockErrors < 15 Then
                If Not isRakeshSharmaClicked Then
                    common.clsCommon.MyMessageBoxShow(GetMessageForTransfer() + "Data Saved Successfully", Me.Text)
                End If
                If Not isRakeshSharmaClicked AndAlso clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                    CreateAnotherTransaction()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function GetMessageForTransfer() As String
        Dim strMessage As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(cboLoadOutType.Text), "Transfer") = CompairStringResult.Equal Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select upper( Invoice_Type) as Invoice_Type,Sale_Invoice_No,Total_Invoice_Amt,Mannual_Invoice_Amt,Mannual_Invoice_Qty,(select CONVERT(decimal(18,2), SUM(Invoice_Qty/Conversion_Factor)) as Qty from TSPL_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code where TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) as InvoiceQty from TSPL_SALE_INVOICE_HEAD where Shipment_No='" + txtDocNo.Value + "'")
            Dim DiffAmt As Double = 1
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                DiffAmt = Math.Abs(clsCommon.myCdbl(dt.Rows(0)("Mannual_Invoice_Amt")) - clsCommon.myCdbl(dt.Rows(0)("Total_Invoice_Amt")))
                strMessage += clsCommon.myCstr(dt.Rows(0)("Invoice_Type")) + " Invoice No - " + clsCommon.myCstr(dt.Rows(0)("Sale_Invoice_No")) + " Generated." + Environment.NewLine + "System Generated Amount - " + clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("Total_Invoice_Amt"))) + Environment.NewLine + "Mannual Generated Amount - " + clsCommon.myFormat(clsCommon.myCdbl(dt.Rows(0)("Mannual_Invoice_Amt"))) + Environment.NewLine + "Difference Amount - " + clsCommon.myFormat(DiffAmt) + "" + Environment.NewLine
            End If
            Dim isCorrect As Integer = IIf(DiffAmt <= 15, 1, 0)
            If isCorrect = 1 Then
                btnPost.Enabled = False
                btnSettlement.Enabled = False
                btnAdd.Enabled = False
                btnSaveAndPrint.Enabled = False
                btnDelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            End If
        End If
        Return strMessage
    End Function

    Private Function PostOfTransferWhenManualAmtEqualFieldAmt(ByVal tra As SqlTransaction) As Boolean
        If Not isRakeshSharmaClicked AndAlso clsCommon.CompairString(clsCommon.myCstr(cboLoadOutType.Text), "Transfer") = CompairStringResult.Equal Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select upper( Invoice_Type) as Invoice_Type,Sale_Invoice_No,Total_Invoice_Amt,Mannual_Invoice_Amt,Mannual_Invoice_Qty,(select CONVERT(decimal(18,2), SUM(Invoice_Qty/Conversion_Factor)) as Qty from TSPL_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code where TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) as InvoiceQty from TSPL_SALE_INVOICE_HEAD where Shipment_No='" + txtDocNo.Value + "'", tra)
            Dim DiffAmt As Double = 1
            'Dim DiffQty As Double = 1
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                DiffAmt = Math.Abs(clsCommon.myCdbl(dt.Rows(0)("Mannual_Invoice_Amt")) - clsCommon.myCdbl(dt.Rows(0)("Total_Invoice_Amt")))
            End If

            Dim isCorrect As Integer = IIf(DiffAmt <= 15, 1, 0)
            Dim qry As String = " update TSPL_SHIPMENT_MASTER set Is_Printed='" + clsCommon.myCstr(isCorrect) + "',Verify_By='" + IIf(isCorrect = 1, objCommonVar.CurrentUserCode, "") + "' where Shipment_No='" + txtDocNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tra)

            qry = " update TSPL_SALE_INVOICE_HEAD set Is_Printed='" + clsCommon.myCstr(isCorrect) + "',Verify_By='" + IIf(isCorrect = 1, objCommonVar.CurrentUserCode, "") + "' where Shipment_No='" + txtDocNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tra)

            If isCorrect = 1 Then
                Try
                    Dim strBankCode As String = clsFixedParameter.GetData(clsFixedParameterType.LOReceiptDefaultBankForSettlement, clsFixedParameterCode.LOReceiptDefaultBankForSettlement, tra)
                    If clsCommon.myLen(strBankCode) <= 0 Then
                        Throw New Exception("Default Bank code not found")
                    End If
                    Dim strPaymentCode As String = clsFixedParameter.GetData(clsFixedParameterType.LOReceiptPaymentTypeForSettlement, clsFixedParameterCode.LOReceiptPaymentTypeForSettlement, tra)
                    If clsCommon.myLen(strPaymentCode) <= 0 Then
                        Throw New Exception("Default Payemnt code not found")
                    End If

                    clsShipmentMaster.postShipment(txtDocNo.Value, tra)
                    Dim InvNo As String = clsCommon.myCstr(dt.Rows(0)("Sale_Invoice_No"))
                    clsSaleHead.createInvoice(InvNo, txtDocNo.Value, tra)
                    clsSaleHead.Postdata(InvNo, tra)
                    qry = "select Adjustment_No  from TSPL_Receipt_Adjustment_Header  where Doc_No='" + InvNo + "' and (Is_Post is null or Is_Post <> 'Y')"
                    Dim dtReceiptAdjustment As DataTable = clsDBFuncationality.GetDataTable(qry, tra)
                    If dtReceiptAdjustment IsNot Nothing AndAlso dtReceiptAdjustment.Rows.Count > 0 Then
                        For Each dr As DataRow In dtReceiptAdjustment.Rows
                            clsAdjustmentEntryReceivables.FunPost(clsCommon.myCstr(dr("Adjustment_No")), tra)
                        Next
                    End If
                    clsReceiptHeader.ReciepEntryWithPostOfInvoice(InvNo, strBankCode, strPaymentCode, tra)
                Catch ex As Exception
                    Throw New Exception("Error in Posting " + Environment.NewLine + ex.Message)
                End Try
            End If
        End If
        Return True
    End Function

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        resetdata()
        txtDate.Focus()
    End Sub

    Public Sub resetdata()
        resetFNDCustomer()
        resetFNDTaxGroup()
        resetForm()
        chkSample.Checked = False
        txtSchemeSample.Enabled = False
        chkCreateEmpty.Checked = True
        isCellValueChangedOpen = False
        txtDiscPer.ReadOnly = False
        txtDate.Enabled = True
        btnPost.Visible = True
        btnSettlement.Visible = False
        txtMannaulInvoiceNo.Text = ""
        pnlMannualInvoiceNo.Visible = False
        lblManualAmt.Visible = False
        txtMannualAmt.Visible = False
        txtMannualAmt.Value = 0

        lblManualQty.Visible = False
        txtMannualQty.Visible = False
        chkCreateEmpty.Visible = False
        txtMannualQty.Value = 0
        chkDiscountOnAmt.IsChecked = True
        chkDiscountOnRate.IsChecked = True
        cboLoadOutType.Text = "Sale"
        loadOutTypeChanged(cboLoadOutType.Text)
        btnUpdateWithCustomerNewPrice.Visible = False
        txtTransferNo.Focus()
        isNewEntry = True

        If txtDate.ShowCheckBox Then
            txtDate.Checked = False
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeform()
    End Sub

    Public Sub closeform()
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deletedata()
    End Sub

    Public Sub deletedata()

        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("No shipment no found to Cancel")
            Exit Sub
        End If
        sql = "select Adjustment_No,Is_Post from TSPL_Receipt_Adjustment_Header  where Doc_No='" + lblSaleInvoiceNo.Text + "' and Is_Post='Y'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            sql = "Can't Cancel the current Transaction because following Posted Adjustment Found : "
            For Each dr As DataRow In dt.Rows
                sql += Environment.NewLine + clsCommon.myCstr(dr("Adjustment_No"))
            Next
            common.clsCommon.MyMessageBoxShow(sql, Me.Text)
            Exit Sub
        End If
        sql = "select Adjustment_No  from TSPL_Receipt_Adjustment_Header  where Doc_No='" + lblSaleInvoiceNo.Text + "' and (Is_Post is null or Is_Post <> 'Y')"
        Dim dtReceiptAdjustment As DataTable = clsDBFuncationality.GetDataTable(sql)
        sql = "SELECT Is_Post from TSPL_SHIPMENT_MASTER WHERE Shipment_No='" + txtDocNo.Value + "'"
        If clsDBFuncationality.getSingleValue(sql) = "Y" Then
            common.clsCommon.MyMessageBoxShow("Record is already posted.")
            Exit Sub
        Else
            If common.clsCommon.MyMessageBoxShow("Cancel the Current Transaction." + Environment.NewLine + "Are you sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Cancel"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                End If

                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    '------------Saving Data in cancel tables
                    Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_SALE_INVOICE_HEAD", trans)

                    sql = "INSERT INTO TSPL_SHIPMENT_MASTER_CANCEL SELECT * FROM TSPL_SHIPMENT_MASTER WHERE Shipment_No='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(sql, trans)
                    sql = "INSERT INTO TSPL_SHIPMENT_DETAILS_CANCEL SELECT * FROM TSPL_SHIPMENT_DETAILS WHERE Shipment_No='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(sql, trans)

                    sql = "INSERT INTO TSPL_SALE_INVOICE_HEAD_CANCEL(" + strInvColumns + ",Cancel_By,Cancel_Date,Cancel_Remarks) SELECT " + strInvColumns + ",'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','" + frm.strRmks + "' FROM TSPL_SALE_INVOICE_HEAD WHERE Sale_Invoice_No='" + lblSaleInvoiceNo.Text + "'"
                    clsDBFuncationality.ExecuteNonQuery(sql, trans)
                    sql = "INSERT INTO TSPL_SALE_INVOICE_DETAIL_CANCEL SELECT * FROM TSPL_SALE_INVOICE_DETAIL WHERE Sale_Invoice_No='" + lblSaleInvoiceNo.Text + "'"
                    clsDBFuncationality.ExecuteNonQuery(sql, trans)
                    '------------End of Saving Data in cancel tables

                    sql = "Select Item_Code,Shipped_Qty,Unit_Code,Location,isnull(Unit_COGS,0) as Unit_COGS,Price_Date,MRP_Amt,Basic_Rate from TSPL_SHIPMENT_DETAILS WHERE Shipment_No='" + txtDocNo.Value + "'"
                    Dim shpDS As DataTable = clsDBFuncationality.GetDataTable(sql, trans)
                    For Each drow As DataRow In shpDS.Rows
                        sql = "SELECT Conversion_factor FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code='" + drow(0).ToString() + "' AND " & _
                         " Uom_Code='" + drow(2).ToString() + "'"
                        Dim convertFact As Decimal = clsDBFuncationality.getSingleValue(sql, trans)
                        If clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal Then
                            Dim itemQty As Decimal = 0
                            Dim cogs As Decimal = 0
                            Dim unitCogs As Decimal = drow(4)
                            '--------------------------------update Item location details---------------------------------
                            sql = "SELECT Item_Qty, Amount FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + drow(0).ToString() + "' " & _
                            " AND location_code='" + drow(3).ToString() + "' and MRP='" + drow(6).ToString() + "'"
                            ds = clsDBFuncationality.GetDataTable(sql, trans)
                            If ds.Rows.Count > 0 Then
                                itemQty = clsCommon.myCdbl(ds.Rows(0)(0).ToString())
                                cogs = clsCommon.myCdbl(ds.Rows(0)(1).ToString())
                            End If
                        Else
                            Dim whereClause As String = " Item_Code='" + drow(0).ToString() + "' AND Uom='" + drow(2).ToString() + "' AND " & _
                            " Price_Date='" + Format(CDate(drow(5).ToString()), "MM/dd/yyyy") + "' AND MRP='" + drow(6).ToString() + "'"
                            sql = "SELECT pending_Qty from TSPL_TRANSFER_DETAIL WHERE Transfer_No='" + txtTransferNo.Value + "' AND " + whereClause
                            Dim balanceqty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql, trans))
                        End If
                    Next

                    Dim qry As String = Nothing
                    If dtReceiptAdjustment IsNot Nothing AndAlso dtReceiptAdjustment.Rows.Count > 0 Then
                        For Each dr As DataRow In dtReceiptAdjustment.Rows
                            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_Receipt_Adjustment_Header_Delete", New SqlParameter("@Adjustment_No", clsCommon.myCstr(dr("Adjustment_No"))))
                            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_Receipt_Adjustment_Detail_Delete", New SqlParameter("@Adjustment_No", clsCommon.myCstr(dr("Adjustment_No"))))
                        Next
                    End If

                    Dim strAdjNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Adjustment_No from TSPL_ADJUSTMENT_HEADER where ItemType='E' and Reference_Document='Sale Invoice' and Document_No='" + lblSaleInvoiceNo.Text + "'", trans))
                    qry = "Delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No='" + strAdjNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "Delete from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" + strAdjNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)


                    qry = "delete from TSPL_SALE_INVOICE_DETAIL where  Sale_Invoice_No='" + lblSaleInvoiceNo.Text + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + lblSaleInvoiceNo.Text + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)


                    clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_SHIPMENT_DETAILS_DELETE", New SqlParameter("@Shipment_No", txtDocNo.Value))
                    clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_SHIPMENT_MASTER_DELETE", New SqlParameter("@Shipment_No", txtDocNo.Value))

                    If saveCancelLog(frm.strRmks, trans) = True Then
                        trans.Commit()
                    End If

                    common.clsCommon.MyMessageBoxShow("Transaction cancelled successfully", Me.Text)
                    btnAdd.Text = "Save"
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    btnSettlement.Enabled = False

                    resetdata()
                Catch ex As Exception
                    trans.Rollback()
                    myMessages.myExceptions(ex)
                End Try
            End If
        End If
    End Sub
    Function saveCancelLog(ByVal Reason As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = "Cancel"
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function



    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        iiDeadlockErrors = 1
        postdata()
    End Sub

    Public Sub postdata()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Post Record")
            Exit Sub
        End If
        If clsCommon.CompairString("Y", clsFixedParameter.GetData(clsFixedParameterType.PrintVerify, clsFixedParameterCode.SalesInvoice, Nothing)) = CompairStringResult.Equal Then
            Dim qry As String = "select Is_Printed from TSPL_SHIPMENT_MASTER where Shipment_No='" + txtDocNo.Value + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 0 Then
                common.clsCommon.MyMessageBoxShow("Before posting, Please Check and Verify Sale Invoice on print preview. ", Me.Text)
                Exit Sub
            End If
        End If

        If myMessages.postConfirm() Then
            Dim i As Integer = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                i += 1
                If Not checkItemonLocation(grow.Cells(ColICode).Value, grow.Cells(colShippedQty).Value, grow.Cells(collocation).Value, grow.Cells(colUnitCode).Value, clsCommon.myCdbl(grow.Cells(colMRP).Value), False, grow.Cells(colBatchNumber).Value) Then
                    common.clsCommon.MyMessageBoxShow("Load out not posted successfully.")
                    Exit Sub
                End If
            Next
            PostDataFun()
        End If
    End Sub

    Private Sub PostDataFun()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsShipmentMaster.postShipment(txtDocNo.Value, trans) Then
                CreateAndPostInvoice(trans)
                'Throw New Exception("xxx.")
                Dim isCreateAnotherTrans As Boolean = False
                trans.Commit()
                If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                    isCreateAnotherTrans = CreateAnotherTransaction()
                End If
                If Not isCreateAnotherTrans Then
                    myMessages.post()
                    btnPost.Enabled = False
                    btnSettlement.Enabled = False
                    btnAdd.Enabled = False
                    btnSaveAndPrint.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
            Else
                Throw New Exception("Loadout not posted successfully.")
            End If

        Catch ex As Exception
            trans.Rollback()
            If ex.Message.Contains("deadlocked") Then
                iiDeadlockErrors += 1
                If iiDeadlockErrors >= 15 Then
                    Me.Close()
                    Exit Sub
                End If
                System.Threading.Thread.Sleep(3000)
                PostDataFun()
            Else
                myMessages.myExceptions(ex)
            End If
        End Try
    End Sub

    Private Sub rdbPer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbPer.ToggleStateChanged
        totalAmounts()
    End Sub

    Private Sub ddlLoadOutType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboLoadOutType.SelectedIndexChanged
        loadOutTypeChanged(cboLoadOutType.Text)
    End Sub

    Private Sub loadOutTypeChanged(ByVal loadoutType As String)
        If loadoutType = "Sale" Then
            resetFNDCustomer()
            resetFNDTaxGroup()
            resetForm()
            txtcustomerinvoiceno.MendatroryField = False
            repoActualBalQty.IsVisible = True
            txtDate.Enabled = True
            btnPost.Visible = True AndAlso MyBase.isPostFlag
            btnSettlement.Visible = False
            lblTransaction.Visible = False
            lblManualAmt.Visible = False
            txtMannualAmt.Visible = False
            txtcustomerinvoiceno.Visible = False
            lblcustomerinvoiceno.Visible = False
            lblManualQty.Visible = False
            txtMannualQty.Visible = False
            chkCreateEmpty.Visible = False
        Else
            lblTransferDate.Visible = True
            lblTransferNo.Visible = True
            txtTransferNo.Visible = True
            txtTransferDate.Visible = True
            lblOrderDate.Visible = False
            lblOrderNo.Visible = False
            txtOrderNo.Visible = False
            txtOrderDate.Visible = False
            gv1.Columns("orderedQty").HeaderText = "Transfer Qty"
            txtcustomerinvoiceno.MendatroryField = True
            repoActualBalQty.IsVisible = False
            txtDate.Enabled = True
            btnPost.Visible = False
            btnSettlement.Visible = True
            lblTransaction.Visible = False
            lblManualAmt.Visible = True
            txtMannualAmt.Visible = True
            txtcustomerinvoiceno.Visible = True
            lblcustomerinvoiceno.Visible = True
            lblManualQty.Visible = True
            txtMannualQty.Visible = True
            chkCreateEmpty.Visible = True
        End If
    End Sub

    Private Sub rbFB_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbFB.ToggleStateChanged
        If clsCommon.myLen(txtOrderNo.Value) > 0 Then
            Exit Sub
        End If
        If rbFB.ToggleState = ToggleState.On Then
            If txtTransferNo.Value <> "" Then
                funfilltransfer(False, "FB")
            Else
                priceDateSelection("FB")
            End If
        End If
        lblfc.Text = 0
        lblfb.Text = 0
        funSetFirstRow()
    End Sub

    Private Sub rbFC_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbFC.ToggleStateChanged
        If clsCommon.myLen(txtOrderNo.Value) > 0 Then
            Exit Sub
        End If

        If rbFC.ToggleState = ToggleState.On Then
            If txtTransferNo.Value <> "" Then
                funfilltransfer(False, "FC")
            Else
                priceDateSelection("FC")
            End If
        End If
        lblfc.Text = 0
        lblfb.Text = 0
        funSetFirstRow()
    End Sub

    Private Sub rbAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbAll.ToggleStateChanged
        If clsCommon.myLen(txtOrderNo.Value) > 0 Then
            Exit Sub
        End If
        If rbAll.ToggleState = ToggleState.On Then
            If txtTransferNo.Value <> "" Then
                funfilltransfer(False)
            Else
                priceDateSelection()
            End If
        End If
        lblfc.Text = 0
        lblfb.Text = 0
        funSetFirstRow()
    End Sub

    Private Sub FrmShipment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag Then
            resetdata()
        ElseIf e.KeyCode = Keys.Enter Then
            If gv1.Rows.Count > 0 Then
                If gv1.CurrentRow.Index > 0 Then
                    Dim i As Integer = gv1.CurrentRow.Index + 1
                    If gv1.Rows.Count > i Then
                        gv1.CurrentRow = gv1.Rows(i)
                    ElseIf gv1.Rows.Count = i Then
                        ''gvLoadOut.CurrentRow = gvLoadOut.Rows(0)
                    End If
                End If
            End If
        End If

        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveMainClick()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveAndPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            deletedata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            print()
        ElseIf isNewEntry AndAlso e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F11 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                pnlMannualInvoiceNo.Visible = True
            End If
        ElseIf e.Control And e.KeyCode = Keys.F11 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIR"
            frm.strCode = "SIRevers"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseTransaction.Visible = True
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then


            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
                btnDeleteRS.Visible = True
                btnCreateRS.Visible = True
                btnCreateRSSaleType.Visible = True
                btnDeleteRSSaleType.Visible = True
                btnRecalTransAndReCreateJE.Visible = True
            End If



        End If

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        print()

    End Sub

    Sub print()
        If txtDocNo.Value = "" Then
            common.clsCommon.MyMessageBoxShow("Please Select Load Out.")
        Else
            Dim strFrm As String = txtDate.Value
            Dim strTo As String = txtDate.Value
            Dim strshipFrm As String = txtDocNo.Value
            Dim strshipTo As String = txtDocNo.Value
            sql = "Select Excisable from TSPL_LOCATION_MASTER WHERE Location_Code='" + txtLocation.Value + "' "
            Dim LType As String = clsDBFuncationality.getSingleValue(sql)
            funPrintReport(strFrm, strTo, strshipFrm, strshipTo, LType)
        End If
    End Sub

    Public Shared Sub funPrintReport(ByVal strFrmDt As String, ByVal strToDt As String, ByVal strShipFrm As String, ByVal strShipTo As String, ByVal locationType As String)
        Dim whereclause As String = " where TSPL_SHIPMENT_MASTER.Shipment_No = '" + strShipFrm + "'"
        Dim str As String = ",'' as totalqty"
        Dim qry As String = "select  Unit_code as Unit,sum(shipped_qty) as Qty,(select shell_qty from TSPL_SHIPMENT_MASTER where Shipment_No='" + strShipFrm + "')as SH from TSPL_SHIPMENT_DETAILS where Shipment_No = '" + strShipFrm + "' group by Unit_code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            str = ",'"
            For Each dr As DataRow In dt.Rows
                str = str + clsCommon.myCstr(dr("Unit")) + " - " + clsCommon.myCstr(dr("Qty")) + " "
            Next
            str = str + " SH - " + clsCommon.myCstr(dt.Rows(0)("SH"))
            str = str + "'" + " as totalqty"
        End If
        Dim qryForGettingTax As String = "select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code"
        Dim strQuery As String = ""
        If clsCommon.CompairString(locationType, "T") = CompairStringResult.Equal Then
            strQuery = "  SELECT case when Len(TSPL_CUSTOMER_MASTER.Add1)>0 then  + ', '  else '' + case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then  + ', ' else '' +   TSPL_CUSTOMER_MASTER.Add3" & _
            " end end AS Address,   TSPL_SHIPMENT_MASTER.Shipment_No,convert (varchar(10), TSPL_SHIPMENT_MASTER.Shipment_Date,103)as Shipment_Date ," & _
            " TSPL_SHIPMENT_MASTER.Cust_Name,(" + qryForGettingTax + "= TSPL_SHIPMENT_MASTER.TAX1) as TAX1, TSPL_SHIPMENT_MASTER.TAX1_Rate, TSPL_SHIPMENT_DETAILS.TAX1_Assessable_Amt," & _
            " TSPL_SHIPMENT_MASTER.TAX1_Amt, (" + qryForGettingTax + "= TSPL_SHIPMENT_MASTER.TAX2) as TAX2, TSPL_SHIPMENT_MASTER.TAX2_Rate,  TSPL_SHIPMENT_MASTER.TAX2_Assessable_Amt," & _
            " TSPL_SHIPMENT_MASTER.TAX2_Amt, (" + qryForGettingTax + "= TSPL_SHIPMENT_MASTER.TAX3) as TAX3,  TSPL_SHIPMENT_MASTER.TAX3_Rate, TSPL_SHIPMENT_MASTER.TAX3_Assessable_Amt," & _
            " TSPL_SHIPMENT_MASTER.TAX3_Amt,  (" + qryForGettingTax + "= TSPL_SHIPMENT_MASTER.TAX4) as TAX4, TSPL_SHIPMENT_MASTER.TAX4_Rate, TSPL_SHIPMENT_MASTER.TAX4_Assessable_Amt," & _
            " TSPL_SHIPMENT_MASTER.TAX4_Amt,( " + qryForGettingTax + "= TSPL_SHIPMENT_MASTER.TAX5) as TAX5, TSPL_SHIPMENT_MASTER.TAX5_Rate,  TSPL_SHIPMENT_MASTER.TAX5_Assessable_Amt," & _
            " TSPL_SHIPMENT_MASTER.TAX5_Amt, TSPL_SHIPMENT_DETAILS.Shipped_Qty ,  TSPL_SHIPMENT_DETAILS.Item_Code, TSPL_SHIPMENT_DETAILS.Item_Desc  + ' (' + TSPL_SHIPMENT_DETAILS.Unit_Code + ')' as Item_Desc, " & _
            " TSPL_SHIPMENT_DETAILS.MRP_Amt,  TSPL_SHIPMENT_DETAILS.Basic_Rate, TSPL_SHIPMENT_DETAILS.Item_Assessable_Rate, " & _
            " TSPL_SHIPMENT_DETAILS.Item_Net_Amt,  TSPL_SHIPMENT_DETAILS.TAX1 AS 'DTax1', TSPL_SHIPMENT_DETAILS.TAX1_Rate AS 'DTax1Rate', " & _
            " TSPL_SHIPMENT_DETAILS.TAX1_Assessable_Amt AS 'DTax1Ass', (TSPL_SHIPMENT_DETAILS.TAX1_Amt ) AS 'Dtax1Amt'," & _
            " TSPL_SHIPMENT_DETAILS.Total_Assessable_Amt, TSPL_SHIPMENT_DETAILS.Total_MRP_Amt, TSPL_SHIPMENT_DETAILS.Total_Basic_Amt, " & _
            " TSPL_SHIPMENT_DETAILS.Total_net_Amt, TSPL_SHIPMENT_DETAILS.Total_Tax_Amt, TSPL_SHIPMENT_DETAILS.Total_Item_Amt," & _
            " TSPL_SHIPMENT_MASTER.Empty_Value,TSPL_SHIPMENT_DETAILS.Total_TPT,TSPL_SHIPMENT_DETAILS.TPT as 'ttlTPT' ,TSPL_COMPANY_MASTER.Comp_Name ," & _
            " TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 " & _
            " " + str + ",TSPL_CUSTOMER_MASTER.Tin_No, TSPL_SHIPMENT_MASTER.Remarks ,TSPL_SHIPMENT_MASTER.Description ," & _
            " TSPL_ITEM_MASTER .Cheapter_Heads,TSPL_CHAPTER_HEAD.Description as ChapterName ,(select max(Class_Code) from TSPL_ITEM_DETAILS where Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code and Class_Name ='Size') as Class_Code," & _
            " TSPL_EMPLOYEE_MASTER.emp_name ,TSPL_SHIPMENT_MASTER.Vehicle_No,TSPL_SHIPMENT_MASTER.Shipment_Discount_Amt,(CASE WHEN TSPL_SHIPMENT_DETAILS.Scheme_Item='Y' or TSPL_SHIPMENT_DETAILS.Promo_Scheme_Item='Y' or TSPL_SHIPMENT_DETAILS.Sampling_Item='Y' THEN 'Y' ELSE 'N' end) as IsFOCItem " & _
            " FROM  TSPL_CUSTOMER_MASTER INNER JOIN  TSPL_SHIPMENT_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SHIPMENT_MASTER.Cust_Code" & _
            " INNER JOIN  TSPL_SHIPMENT_DETAILS ON TSPL_SHIPMENT_MASTER.Shipment_No = TSPL_SHIPMENT_DETAILS.Shipment_No " & _
            " left outer join TSPL_COMPANY_MASTER on TSPL_SHIPMENT_MASTER.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  " & _
            " left outer join TSPL_ITEM_MASTER on TSPL_SHIPMENT_DETAILS.Item_Code =TSPL_ITEM_MASTER.Item_Code  " & _
            " left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads " & _
            " join TSPL_EMPLOYEE_MASTER on TSPL_SHIPMENT_MASTER.Salesman_Code =TSPL_EMPLOYEE_MASTER.EMP_CODE "
        Else
            strQuery = "  SELECT case when Len(TSPL_CUSTOMER_MASTER.Add1)>0 then  + ', '  else '' + case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then  + ', ' else '' +   TSPL_CUSTOMER_MASTER.Add3 end end AS Address,   TSPL_SHIPMENT_MASTER.Shipment_No, convert (varchar(10), TSPL_SHIPMENT_MASTER.Shipment_Date,103)as Shipment_Date , TSPL_SHIPMENT_MASTER.Cust_Name,(" + qryForGettingTax + " = TSPL_SHIPMENT_MASTER.TAX1) as TAX1, TSPL_SHIPMENT_MASTER.TAX1_Rate, TSPL_SHIPMENT_DETAILS.TAX1_Assessable_Amt,  TSPL_SHIPMENT_MASTER.TAX1_Amt,(" + qryForGettingTax + " = TSPL_SHIPMENT_MASTER.TAX2) as TAX2, TSPL_SHIPMENT_MASTER.TAX2_Rate,  TSPL_SHIPMENT_MASTER.TAX2_Assessable_Amt, TSPL_SHIPMENT_MASTER.TAX2_Amt, (" + qryForGettingTax + " = TSPL_SHIPMENT_MASTER.TAX3) as TAX3,TSPL_SHIPMENT_MASTER.TAX3_Rate, TSPL_SHIPMENT_MASTER.TAX3_Assessable_Amt, TSPL_SHIPMENT_MASTER.TAX3_Amt, (" + qryForGettingTax + " =  TSPL_SHIPMENT_MASTER.TAX4) as TAX4, TSPL_SHIPMENT_MASTER.TAX4_Rate, TSPL_SHIPMENT_MASTER.TAX4_Assessable_Amt,  TSPL_SHIPMENT_MASTER.TAX4_Amt,(" + qryForGettingTax + " =  TSPL_SHIPMENT_MASTER.TAX5) as TAX5, TSPL_SHIPMENT_MASTER.TAX5_Rate,  TSPL_SHIPMENT_MASTER.TAX5_Assessable_Amt, TSPL_SHIPMENT_MASTER.TAX5_Amt, TSPL_SHIPMENT_DETAILS.Shipped_Qty ,  TSPL_SHIPMENT_DETAILS.Item_Code, TSPL_SHIPMENT_DETAILS.Item_Desc  + ' (' + TSPL_SHIPMENT_DETAILS.Unit_Code + ')' as Item_Desc, TSPL_SHIPMENT_DETAILS.MRP_Amt,  TSPL_SHIPMENT_DETAILS.Basic_Rate, TSPL_SHIPMENT_DETAILS.Item_Assessable_Rate, TSPL_SHIPMENT_DETAILS.Item_Net_Amt,  TSPL_SHIPMENT_DETAILS.TAX1 AS 'DTax1', TSPL_SHIPMENT_DETAILS.TAX1_Rate AS 'DTax1Rate',  TSPL_SHIPMENT_DETAILS.TAX1_Assessable_Amt AS 'DTax1Ass','0.00' AS 'Dtax1Amt',  TSPL_SHIPMENT_DETAILS.Total_Assessable_Amt, TSPL_SHIPMENT_DETAILS.Total_MRP_Amt, TSPL_SHIPMENT_DETAILS.Total_Basic_Amt,  TSPL_SHIPMENT_DETAILS.Total_net_Amt, TSPL_SHIPMENT_DETAILS.Total_Tax_Amt, TSPL_SHIPMENT_DETAILS.Total_Item_Amt,  TSPL_SHIPMENT_MASTER.Empty_Value,TSPL_SHIPMENT_DETAILS.Total_TPT,TSPL_SHIPMENT_DETAILS.TPT as 'ttlTPT' ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2  " & _
            " " + str + ",TSPL_CUSTOMER_MASTER.Tin_No,TSPL_SHIPMENT_MASTER.Remarks ,TSPL_SHIPMENT_MASTER.Description ,TSPL_ITEM_MASTER .Cheapter_Heads ,(select max(Class_Code) from TSPL_ITEM_DETAILS where Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code and Class_Name ='Size') as Class_Code, TSPL_EMPLOYEE_MASTER.emp_name ,TSPL_SHIPMENT_MASTER.Vehicle_No,TSPL_SHIPMENT_MASTER.Shipment_Discount_Amt,(CASE WHEN TSPL_SHIPMENT_DETAILS.Scheme_Item='Y' or TSPL_SHIPMENT_DETAILS.Promo_Scheme_Item='Y' or TSPL_SHIPMENT_DETAILS.Sampling_Item='Y' THEN 'Y' ELSE 'N' end) as IsFOCItem FROM  TSPL_CUSTOMER_MASTER INNER JOIN  TSPL_SHIPMENT_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SHIPMENT_MASTER.Cust_Code INNER JOIN  TSPL_SHIPMENT_DETAILS ON TSPL_SHIPMENT_MASTER.Shipment_No = TSPL_SHIPMENT_DETAILS.Shipment_No left outer join TSPL_COMPANY_MASTER on TSPL_SHIPMENT_MASTER.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code  left outer join TSPL_ITEM_MASTER on TSPL_SHIPMENT_DETAILS.Item_Code =TSPL_ITEM_MASTER.Item_Code  join TSPL_EMPLOYEE_MASTER on TSPL_SHIPMENT_MASTER.Salesman_Code =TSPL_EMPLOYEE_MASTER.EMP_CODE "
        End If

        strQuery = strQuery & whereclause

        Try
            Dim Ds As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If Ds.Rows.Count > 0 Then
                If locationType = "T" Then
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptCustomLoadOutWithExcisewithStandardformat", "Loadout")
                Else
                    frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptCustomLoadOutWithOutExcisewithStandardformat", "Loadout")
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found", "LoadOut Report", MessageBoxButtons.OK)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Load Out Report", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Dim salesmancode As String = txtSalesman.Value
        Dim custcode As String = txtCustomer.Value
        Dim value As String = String.Empty
        value = clsDBFuncationality.getSingleValue("select Sale_Invoice_No  from TSPL_SALE_INVOICE_HEAD  where Shipment_No = '" + txtDocNo.Value + "'")
        If value <> String.Empty Or custcode <> String.Empty Or salesmancode <> String.Empty Then
            If value <> String.Empty Then
                value = "Sale Invoice" + "," + value

            End If
            Dim frm As New frmAdjustmentEmpty()
            frm.strLoadoutNo = lblSaleInvoiceNo.Text
            frm.isSaleInvoice = True
            frm.strCustomer = custcode
            frm.strSalesman = salesmancode
            frm.strLocation = txtLocation.Value
            frm.dtTransDate = txtDate.Value
            frm.Show()
        Else
            Dim frm1 As New frmAdjustmentEmpty()
            frm1.Show()
        End If
    End Sub

    Public Sub printdata(ByVal ispreprint As Boolean)
        Dim Invoiceno As String = ""
        Invoiceno = clsDBFuncationality.getSingleValue("select Sale_Invoice_No  from TSPL_SALE_INVOICE_HEAD  where Shipment_No = '" + txtDocNo.Value + "'")
        sql = "Select Excisable from TSPL_LOCATION_MASTER WHERE Location_Code='" + txtLocation.Value + "' "
        Dim LType As String = clsDBFuncationality.getSingleValue(sql)
        FrmLoadOutRpt.funPrintReport(Invoiceno, LType, ispreprint, txtDocNo.Value)
    End Sub

    Private Sub txtshellqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtshellqty.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
        End If
    End Sub

    Private Function GetJounalEntryException(ByVal VoucherNo As String, ByVal trans As SqlTransaction) As String
        Dim sql As String = "Select Account_code,Account_Desc,case when Amount>0 then Amount end as DrAmt,case when Amount<0 then -1*Amount end as CrAmt from TSPL_JOURNAL_DETAILS WHERE Voucher_No='" + VoucherNo + "'"
        Dim dtError As DataTable = clsDBFuncationality.GetDataTable(sql, trans)
        Dim msg As String = "Please Check Journal Entry" + Environment.NewLine
        Dim counter As Integer = 1
        Dim TotDrAmt As Double = 0
        Dim TotCrAmt As Double = 0
        For Each dr As DataRow In dtError.Rows
            msg += clsCommon.myCstr(counter) + "   "
            msg += clsCommon.myCstr(dr("Account_code")) + "             "
            msg += clsCommon.myCstr(dr("DrAmt")) + "                  "
            msg += clsCommon.myCstr(dr("CrAmt")) + Environment.NewLine
            TotDrAmt += clsCommon.myCdbl(dr("DrAmt"))
            TotCrAmt += clsCommon.myCdbl(dr("CrAmt"))
            counter += 1
        Next
        msg += "-------------------------------------------------------------------------" + Environment.NewLine
        msg += clsCommon.myCstr("Total") + "             "
        msg += clsCommon.myCstr(TotDrAmt) + "                  "
        msg += clsCommon.myCstr(TotCrAmt) + Environment.NewLine
        msg += "-------------------------------------------------------------------------"

        Return msg
    End Function

    Private Sub RadGroupBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox2.Click

    End Sub

    Private Sub MyTextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar >= Chr(47) And e.KeyChar <= Chr(58) Then
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(8) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else


        End If
        If e.KeyChar = Chr(46) Then
            e.Handled = False
        End If
    End Sub



    Private Sub txtShipmentTotal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtNetShipAmt.Text = txtShipmentTotal.Text
    End Sub


    Private Sub txtShipmentAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShipmentAmt.TextChanged
        txtTotalShipmentAmt.Text = clsCommon.myCdbl(txtShipmentAmt.Text) + clsCommon.myCdbl(txtContainerDeposit.Text)
    End Sub

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        printdata(True)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        printdata(False)

    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Sub SetOldDataDatatable()
        dtOldData.Rows.Clear()
        For index As Integer = 0 To gv1.Rows.Count - 1
            Dim drOldData As DataRow = dtOldData.NewRow()
            drOldData(ColComplete) = gv1.Rows(index).Cells(ColComplete).Value
            drOldData(ColICode) = gv1.Rows(index).Cells(ColICode).Value
            drOldData(ColItemName) = gv1.Rows(index).Cells(ColItemName).Value
            drOldData(colPriceDateColumn) = gv1.Rows(index).Cells(colPriceDateColumn).Value
            drOldData(colOrderedQty) = gv1.Rows(index).Cells(colOrderedQty).Value
            drOldData(colShippedQty) = gv1.Rows(index).Cells(colShippedQty).Value
            drOldData(colBalanceQty) = gv1.Rows(index).Cells(colBalanceQty).Value
            drOldData(colUnitCode) = gv1.Rows(index).Cells(colUnitCode).Value
            drOldData(colSchemeApplicable) = gv1.Rows(index).Cells(colSchemeApplicable).Value
            drOldData(colMainItem) = gv1.Rows(index).Cells(colMainItem).Value
            drOldData(collocation) = gv1.Rows(index).Cells(collocation).Value
            drOldData(colPriceCode) = gv1.Rows(index).Cells(colPriceCode).Value
            drOldData(colSampleItem) = gv1.Rows(index).Cells(colSampleItem).Value
            drOldData(colEmptyValue) = gv1.Rows(index).Cells(colEmptyValue).Value
            drOldData(colMRP) = gv1.Rows(index).Cells(colMRP).Value
            drOldData(colMRPInBottle) = gv1.Rows(index).Cells(colMRPInBottle).Value
            drOldData(colBasicAmount) = gv1.Rows(index).Cells(colBasicAmount).Value
            drOldData(colDiscountAmount) = gv1.Rows(index).Cells(colDiscountAmount).Value
            drOldData(colcustDiscount) = gv1.Rows(index).Cells(colcustDiscount).Value
            drOldData(colitemNetAmount) = gv1.Rows(index).Cells(colitemNetAmount).Value
            drOldData(colTaxamount) = gv1.Rows(index).Cells(colTaxamount).Value
            drOldData(colTPT) = gv1.Rows(index).Cells(colTPT).Value
            drOldData(colTotalMRP) = gv1.Rows(index).Cells(colTotalMRP).Value
            drOldData(colTotalBasicAmount) = gv1.Rows(index).Cells(colTotalBasicAmount).Value
            drOldData(colTotalDiscountAmount) = gv1.Rows(index).Cells(colTotalDiscountAmount).Value
            drOldData(colTotalCustDiscount) = gv1.Rows(index).Cells(colTotalCustDiscount).Value
            drOldData(colTotalTaxAmount) = gv1.Rows(index).Cells(colTotalTaxAmount).Value
            drOldData(colTotalItemAmount) = gv1.Rows(index).Cells(colTotalItemAmount).Value
            drOldData(colPromoSchemeApplicable) = gv1.Rows(index).Cells(colPromoSchemeApplicable).Value
            drOldData(colSchemeDiscountApplicable) = gv1.Rows(index).Cells(colSchemeDiscountApplicable).Value
            drOldData(colPromoSchemeCode) = gv1.Rows(index).Cells(colPromoSchemeCode).Value
            drOldData(colSchemeCodeDiscount) = gv1.Rows(index).Cells(colSchemeCodeDiscount).Value
            drOldData(colSchemeItem) = gv1.Rows(index).Cells(colSchemeItem).Value
            drOldData(colPromoSchemeItem) = gv1.Rows(index).Cells(colPromoSchemeItem).Value
            drOldData(colFromSchemeCode) = gv1.Rows(index).Cells(colFromSchemeCode).Value
            drOldData(colEmptyValueShell) = gv1.Rows(index).Cells(colEmptyValueShell).Value
            drOldData(colEmptyValueBottle) = gv1.Rows(index).Cells(colEmptyValueBottle).Value
            drOldData(colTransferBasicAmount) = gv1.Rows(index).Cells(colTransferBasicAmount).Value
            drOldData(colTax1Rate) = gv1.Rows(index).Cells(colTax1Rate).Value
            drOldData(colTax2Rate) = gv1.Rows(index).Cells(colTax2Rate).Value
            drOldData(colTax3Rate) = gv1.Rows(index).Cells(colTax3Rate).Value
            drOldData(colTax4Rate) = gv1.Rows(index).Cells(colTax4Rate).Value
            drOldData(colTax5Rate) = gv1.Rows(index).Cells(colTax5Rate).Value
            drOldData(colTax6Rate) = gv1.Rows(index).Cells(colTax6Rate).Value
            drOldData(colAssess1) = gv1.Rows(index).Cells(colAssess1).Value
            drOldData(colAssess2) = gv1.Rows(index).Cells(colAssess2).Value
            drOldData(colAssess3) = gv1.Rows(index).Cells(colAssess3).Value
            drOldData(colAssess4) = gv1.Rows(index).Cells(colAssess4).Value
            drOldData(colAssess5) = gv1.Rows(index).Cells(colAssess5).Value
            drOldData(colAssess6) = gv1.Rows(index).Cells(colAssess6).Value
            drOldData(colTax1Amt) = gv1.Rows(index).Cells(colTax1Amt).Value
            drOldData(colTax2Amt) = gv1.Rows(index).Cells(colTax2Amt).Value
            drOldData(colTax3Amt) = gv1.Rows(index).Cells(colTax3Amt).Value
            drOldData(colTax4Amt) = gv1.Rows(index).Cells(colTax4Amt).Value
            drOldData(colTax5Amt) = gv1.Rows(index).Cells(colTax5Amt).Value
            drOldData(colTax6Amt) = gv1.Rows(index).Cells(colTax6Amt).Value
            drOldData(colCheckvalue) = gv1.Rows(index).Cells(colCheckvalue).Value
            drOldData(colBatchNumber) = gv1.Rows(index).Cells(colBatchNumber).Value
            drOldData(colTotalNetAmount) = gv1.Rows(index).Cells(colTotalNetAmount).Value
            drOldData(colDiscountCode) = gv1.Rows(index).Cells(colDiscountCode).Value
            drOldData(ColCustDisNoTax) = gv1.Rows(index).Cells(ColCustDisNoTax).Value
            drOldData(ColTargetDisAmt) = gv1.Rows(index).Cells(ColTargetDisAmt).Value
            drOldData(ColActualBalQty) = gv1.Rows(index).Cells(ColActualBalQty).Value
            drOldData(ColPriceToShow) = gv1.Rows(index).Cells(ColPriceToShow).Value
            drOldData(colPriceDateActual) = gv1.Rows(index).Cells(colPriceDateActual).Value
            drOldData(colAbatementRate) = gv1.Rows(index).Cells(colAbatementRate).Value
            dtOldData.Rows.Add(drOldData)
        Next
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustomer._MYValidating
        If txtDate.ShowCheckBox AndAlso Not txtDate.Checked Then
            clsCommon.MyMessageBoxShow("Please select date", Me.Text)
            txtCustomer.Value = ""
            txtDate.Focus()
            Exit Sub
        End If
        txtCustomerName.Focus()
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            Exit Sub
        End If
        'SetOldDataDatatable()
        Dim qry As String = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State,TSPL_LOCATION_MASTER.Sales_Tax_GroupIS,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Sales_Tax_GroupISName  FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' WHERE TSPL_LOCATION_MASTER.Location_Code = '" + Convert.ToString(txtLocation.Value) + "'"
        Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim loc As String = clsCommon.myCstr(dtLocation.Rows(0)("Excisable"))
        Dim strLocState As String = clsCommon.myCstr(dtLocation.Rows(0)("State"))
        Dim WhrCls As String = " 2=2"
        qry = "SELECT M.Cust_Code AS [Code], m.Customer_Name as [Name], m.Route_No as [Route No], m.Price_Code as [Excisable Price Code], m.price_CodeNon as [Non-Excisable Price Code], (case when m.Credit_Customer = 'Y' THEN 'YES' ELSE 'NO' END) AS [Credit Customer], M.Cust_Category_Code AS [Customer Category Code],M.City_Code as [City Code],TSPL_CITY_MASTER.City_Name as City  FROM TSPL_CUSTOMER_MASTER M JOIN TSPL_CUSTOMER_ACCOUNT_SET A ON M.Cust_Account =A.Cust_Account left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=M.City_Code"
        WhrCls += " and M.status='N' AND M.OnHold='N'"
        txtCustomer.Value = clsCommon.ShowSelectForm("SCustFinder", qry, "Code", WhrCls, txtCustomer.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtTransferNo.Value) > 0 Then
            qry = "select Route_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomer.Value + "'"
            Dim strCustRoute As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

            If Not clsCommon.CompairString(txtRouteNo.Value, strCustRoute) = CompairStringResult.Equal AndAlso clsCommon.myLen(txtCustomer.Value) > 0 Then
                If common.clsCommon.MyMessageBoxShow("Change the Transaction Route." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                    txtCustomer.Value = ""
                    Exit Sub
                End If
                Dim frm As New FrmPWD(Nothing)
                frm.strCode = "RJ"
                frm.strType = "Route"
                frm.ShowDialog()
                If Not frm.isPasswordCorrect Then
                    txtCustomer.Value = ""
                    Exit Sub
                End If
            End If
        End If

        Dim dtStartTime As DateTime = DateTime.Now
        If clsCommon.myLen(txtCustomer.Value) > 0 Then
            qry = "select Customer_Name,Price_Code,price_CodeNon,State from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomer.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtCustomerName.Text = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            If loc = "T" Or loc = "Y" Then
                txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            Else
                txtPriceCode.Text = clsCommon.myCstr(dt.Rows(0)("price_CodeNon"))
            End If

            If clsCommon.myLen(strLocState) > 0 AndAlso clsCommon.myLen(dt.Rows(0)("State")) > 0 AndAlso Not clsCommon.CompairString(strLocState, clsCommon.myCstr(dt.Rows(0)("State"))) = CompairStringResult.Equal Then
                RemoveHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
                SetTaxGroup(clsCommon.myCstr(dtLocation.Rows(0)("Sales_Tax_GroupIS")))
                fndTaxGroup.txtValue.Text = clsCommon.myCstr(dtLocation.Rows(0)("Sales_Tax_GroupIS"))
                lblTaxDesc.Text = clsCommon.myCstr(dtLocation.Rows(0)("Sales_Tax_GroupISName"))
                AddHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
            End If
        End If
        If clsCommon.myLen(txtCustomer.Value) > 0 Then
            If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                Dim checkpricecode As String = clsDBFuncationality.getSingleValue("SELECT price_CodeNon  FROM TSPL_CUSTOMER_MASTER  where Cust_Code= '" + Convert.ToString(txtCustomer.Value) + "'")
                If loc = "T" Or loc = "Y" Then
                Else
                    If txtPriceCode.Text = checkpricecode Then
                    Else
                        common.clsCommon.MyMessageBoxShow("Price Code does not Match with transfer")
                        txtCustomer.Value = String.Empty
                        txtCustomerName.Text = String.Empty
                        Exit Sub
                    End If
                End If
            End If
            Try
                If clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal Then
                    Dim checklocation As String = String.Empty
                    sql = "select isnull(rm.vehicle_code,'') as [vehicle_code] , rm.Route_No , cm.Salesman_Code, cm.Customer_Name ,rm.Route_Desc ,cm.Price_Code , cm.Tax_Group , cm.Cust_Account , cm.Terms_Code, cm.price_codenon , cm.Credit_Customer    "
                    sql += " from TSPL_CUSTOMER_MASTER cm  "
                    sql += " left outer join TSPL_ROUTE_MASTER rm on rm.Route_No= cm.Route_No"
                    sql += " where cm.Cust_Code = '" + txtCustomer.Value + "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        txtCustomerName.Text = dt.Rows(0)("Customer_Name").ToString()
                        txtRouteNo.Value = dt.Rows(0)("Route_No").ToString()
                        lblRouteDesc.Text = dt.Rows(0)("Route_Desc").ToString()
                        checklocation = clsDBFuncationality.getSingleValue("SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtLocation.Value) + "'")
                        If checklocation = "T" Or checklocation = "Y" Then
                            txtPriceCode.Text = dt.Rows(0)("Price_Code").ToString()
                        Else
                            txtPriceCode.Text = dt.Rows(0)("price_codenon").ToString()
                        End If
                        txtVehicleCode.Value = dt.Rows(0)("vehicle_code")
                        rbFC.IsChecked = False
                        rbFC.IsChecked = True
                        txtSalesman.Value = dt.Rows(0)("Salesman_Code").ToString()
                        strCustAccount = dt.Rows(0)("Cust_Account").ToString()
                        txtPaymentTerm.Value = dt.Rows(0)("Terms_Code").ToString()
                        If dt.Rows(0)("Credit_Customer") = "Y" Then
                            chlcreditinvoice.Checked = True
                        Else
                            chlcreditinvoice.Checked = False
                        End If
                    End If
                Else
                    Dim checklocation As String = String.Empty
                    sql = "select  isnull(TSPL_ROUTE_MASTER.vehicle_code,'') as [vehicle_code] , TSPL_ROUTE_MASTER.Route_No , TSPL_CUSTOMER_MASTER.Salesman_Code, TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_ROUTE_MASTER.Route_Desc ,TSPL_CUSTOMER_MASTER.Price_Code , TSPL_CUSTOMER_MASTER.Tax_Group , TSPL_CUSTOMER_MASTER.Cust_Account , TSPL_CUSTOMER_MASTER.Terms_Code, TSPL_CUSTOMER_MASTER.price_CodeNon , TSPL_CUSTOMER_MASTER.Credit_Customer  "
                    sql += " from TSPL_CUSTOMER_MASTER "
                    sql += " left outer join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_CUSTOMER_MASTER.Route_No"
                    sql += " where TSPL_CUSTOMER_MASTER.Cust_Code = '" + txtCustomer.Value + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If dt.Rows(0)("Credit_Customer") = "Y" Then
                            chlcreditinvoice.Checked = True
                        Else
                            chlcreditinvoice.Checked = False
                        End If
                        txtCustomerName.Text = dt.Rows(0)("Customer_Name").ToString()
                        checklocation = clsDBFuncationality.getSingleValue("SELECT Excisable  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtLocation.Value) + "'")
                        If checklocation = "T" Or checklocation = "Y" Then
                            txtPriceCode.Text = dt.Rows(0)("Price_Code").ToString()
                        Else
                            txtPriceCode.Text = dt.Rows(0)("price_codenon").ToString()
                        End If

                        strCustAccount = dt.Rows(0)("Cust_Account").ToString()
                        txtPaymentTerm.Value = dt.Rows(0)("Terms_Code").ToString()
                        txtRouteNo.Value = dt.Rows(0)("Route_No").ToString()
                        lblRouteDesc.Text = dt.Rows(0)("Route_Desc").ToString()
                    End If
                End If

            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.ToString())
                Exit Sub
            End Try
        End If

        If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
            For ii As Integer = 0 To gv1.RowCount - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(ColICode).Value)
                Dim strPriceDate As String = clsCommon.GetPrintDate(clsCommon.myCDate(gv1.Rows(ii).Cells(colPriceDateColumn).Value), "yyyy-MM-dd")
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitCode).Value)
                Dim strMRP As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colMRP).Value)
                Dim strPriceCode As String = txtPriceCode.Text

                'qry = "select Start_Date from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' and Start_Date='" + strPriceDate + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "' "
                'If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                '    qry = "select max(Start_Date) from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "' and Start_Date<='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "'"
                '    If clsCommon.myLen(clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))) <= 0 Then
                '        common.clsCommon.MyMessageBoxShow("Error " + Environment.NewLine + "Price Date does not exist for Item_Code='" + strICode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "'", Me.Text)
                '        Continue For
                '    End If
                '    strPriceDate = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry)), "")
                'End If

                qry = "select max(Start_Date) from TSPL_ITEM_PRICE_MASTER where Item_Code='" + strICode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "' and Start_Date<='" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "'"
                If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry)) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Error " + Environment.NewLine + "Price Date does not exist for Item_Code='" + strICode + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "'", Me.Text)
                    Continue For
                End If
                strPriceDate = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry)), "dd/MMM/yyyy")

                qry = "select (Empty_Value_Bottle+Empty_Value_Shell) as EmptyValue,Item_Basic_Price ,(isnull(Price_Amount1,0) * case when (select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code= Price_Comp1)='Y' then 1 else 0 end+isnull(Price_Amount2,0) * case when (select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code= Price_Comp2)='Y' then 1 else 0 end+ isnull(Price_Amount3,0) * case when (select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code= Price_Comp3)='Y' then 1 else 0 end + "
                qry += " isnull(Price_Amount4,0) * case when (select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code= Price_Comp4)='Y' then 1 else 0 end+ "
                qry += " isnull(Price_Amount5,0) * case when (select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code= Price_Comp5)='Y' then 1 else 0 end+ "
                qry += " isnull(Price_Amount6,0) * case when (select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code= Price_Comp6)='Y' then 1 else 0 end+ "
                qry += " isnull(Price_Amount7,0) * case when (select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code= Price_Comp7)='Y' then 1 else 0 end+ "
                qry += " isnull(Price_Amount8,0) * case when (select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code= Price_Comp8)='Y' then 1 else 0 end+ "
                qry += " isnull(Price_Amount9,0) * case when (select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code= Price_Comp9)='Y' then 1 else 0 end+ "
                qry += " isnull(Price_Amount10,0) * case when (select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code= Price_Comp10)='Y' then 1 else 0 end )as TPT from TSPL_ITEM_PRICE_MASTER "
                qry += " where Item_Code='" + strICode + "' and Start_Date='" + strPriceDate + "' and UOM='" + strUOM + "' and Item_Basic_Net='" + strMRP + "' and  Price_Code='" + strPriceCode + "' "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.Rows(ii).Cells(colEmptyValue).Value = clsCommon.myCdbl(dt.Rows(0)("EmptyValue"))
                    gv1.Rows(ii).Cells(colBasicAmount).Value = clsCommon.myCdbl(dt.Rows(0)("Item_Basic_Price"))
                    gv1.Rows(ii).Cells(colTPT).Value = clsCommon.myCdbl(dt.Rows(0)("TPT"))
                    gv1.Rows(ii).Cells(colPriceDateActual).Value = strPriceDate
                End If
            Next
        End If
        funSetFirstRow()
        If clsCommon.myLen(txtCustomer.Value) > 0 Then
            txtCustomer.Enabled = False
        End If


        cboLoadOutType.Enabled = False
        txtLocation.Enabled = False
        txtTransferNo.Enabled = False
        txtDate.Enabled = False
        Dim dtEndTime As DateTime = DateTime.Now
        Dim span As TimeSpan = dtEndTime.Subtract(dtStartTime)
    End Sub

    Private Sub chkSample_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSample.ToggleStateChanged
        If Not isInsideLoadData Then
            If chkSample.Checked Then
                If common.clsCommon.MyMessageBoxShow("Make the current Transacion as Sample." + Environment.NewLine + "Are you sure.", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    txtDiscPer.Text = 100
                    txtDiscPer.ReadOnly = True
                    sql = "select sampling_code as [Sampling Code], description , account_code as [Account Code], account_description as [Account Description] from TSPL_Sampling_Master"
                    txtSchemeSample.ConnectionString = connectSql.SqlCon()
                    txtSchemeSample.Query = sql
                    txtSchemeSample.ValueToSelect = "Sampling Code"
                    txtSchemeSample.ValueToSelect1 = "description"
                    txtSchemeSample.Caption = "Sample Schemes"

                Else
                    chkSample.Checked = False
                End If
            Else
                txtDiscPer.Text = 0
                txtDiscPer.ReadOnly = False
                sql = "SELECT Scheme_Code as 'Scheme Code', Scheme_Desc as Description, Start_Date as 'Start Date' FROM TSPL_SCHEME_MASTER WHERE Scheme_Type = 'S' ORDER BY Scheme_Code"
                txtSchemeSample.ConnectionString = connectSql.SqlCon()
                txtSchemeSample.Query = sql
                txtSchemeSample.ValueToSelect = "Scheme Code"
                txtSchemeSample.ValueToSelect1 = "Description"
                txtSchemeSample.Caption = "Sample Schemes"
                txtSchemeSample.txtValue.Text = ""
            End If
            txtSchemeSample.Enabled = chkSample.Checked

            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                gv1.CurrentColumn = gv1.Columns(0)
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(ColICode).Value)) > 0 Then
                        If chkSample.Checked Then
                            gv1.Rows(ii).Cells(colSampleItem).Value = "Yes"
                        Else
                            gv1.Rows(ii).Cells(colSampleItem).Value = "No"
                        End If
                    End If
                Next
            End If
            CheckTransactionDiscount()
            CalculateDiscountAmount()
        End If
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub fndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "Select  LM.Location_Code as Code,LM.Location_Desc as Description,Location_type as 'Location Type',(case LM.Excisable when 'T'then 'Excisable'else 'Non-Excisable'end) as 'Excisable'  from TSPL_LOCATION_MASTER as LM "
            Dim whrClus As String = "  Location_Type = 'Physical' "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClus += " and LM.Location_Code  in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("ShipmLoFND", qry, "Code", whrClus, txtLocation.Value, "Code", isButtonClicked)


            sql = "Select Excisable,Sales_Tax_Group from TSPL_LOCATION_MASTER WHERE Location_Code='" + txtLocation.Value + "' "
            Dim lDr As DataTable = clsDBFuncationality.GetDataTable(sql)
            If lDr IsNot Nothing AndAlso lDr.Rows.Count > 0 Then

                If lDr.Rows(0)(0).ToString() = "F" Or lDr.Rows(0)(0).ToString() = "N" Then
                    If gvTax.Rows.Count > 0 Then
                        For Each grow As GridViewRowInfo In gvTax.Rows
                            sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + grow.Cells("taxAuthority").Value + "'"
                            If clsDBFuncationality.getSingleValue(sql) = "Y" Then
                                common.clsCommon.MyMessageBoxShow("Tax group with excisable tax authority is not applicable for given location")
                                fndTaxGroup.txtValue.Text = ""
                                Exit For
                            End If
                        Next
                    End If
                Else
                    If gvTax.Rows.Count > 0 Then
                        For Each grow As GridViewRowInfo In gvTax.Rows
                            sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + grow.Cells("taxAuthority").Value + "'"
                            If clsDBFuncationality.getSingleValue(sql) = "N" Then
                                If grow.Index = gvTax.RowCount - 1 Then
                                    common.clsCommon.MyMessageBoxShow("There should be atleast one tax authority should be excisable.")
                                    fndTaxGroup.txtValue.Text = ""
                                    Exit For
                                End If
                            Else
                                Exit For
                            End If
                        Next
                    End If
                End If

            End If

            fndTaxGroup.txtValue.Text = Convert.ToString(clsDBFuncationality.getSingleValue("SELECT Sales_Tax_Group  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + txtLocation.Value + "'"))
            lblTaxDesc.Text = clsTaxGroupMaster.GetNameOfSaleType(fndTaxGroup.txtValue.Text, Nothing)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndLoadOut__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            LoadData(txtDocNo.Value, NavType)
            funSetFirstRow()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub funSetFirstRow()
        If gv1.Rows.Count > 0 Then
            gv1.CurrentRow = gv1.Rows(0)
        End If
    End Sub

    Private Sub fndLoadOut__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "Select Shipment_No ,Cust_Code as 'Customer Code',Cust_Name as 'Customer Name',CONVERT(varchar(15), Shipment_Date , 103) as [Shipment Date], Transfer_No as [Transfer No],Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as Location,( TSPL_SHIPMENT_MASTER.Invoice_No) as [Sale Invoice No],case when [status]='Open' then 'Unposted' else 'Posted' end as [Status],(select top 1 Invoice_Type from TSPL_SALE_INVOICE_HEAD where Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No) as [Invoice Type],Route_No as [Route Code],Route_Desc as [Route],Vehicle_Code as [Vehicle Code],Vehicle_No as [Vehicle No] FROM TSPL_SHIPMENT_MASTER left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIPMENT_MASTER.Location"
        Dim whrClas As String = " 2=2"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and TSPL_SHIPMENT_MASTER.Location  in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("ShipFND", qry, "Shipment_No", whrClas, txtDocNo.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub fndTransferNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTransferNo._MYValidating
        Dim qry As String = "SELECT DISTINCT h.Transfer_No AS Code, Convert(Varchar(10),h.Transfer_Date,103) AS 'Transfer Date', h.Route_No AS 'Route', h.From_Location AS 'From', h.To_Location AS 'To',h.Reference  ,(select InTable.Transfer_No from TSPL_TRANSFER_HEAD  as InTable where InTable.Load_Out_No=h.Transfer_No) as [Loadin No],(select case when Post='Y' then 'Posted' else 'Pending' end as Status from TSPL_TRANSFER_HEAD  as  InTable where InTable.Load_Out_No=h.Transfer_No) as [Loadin Status], h.Reference_Doc_No as [Reference Document] " & _
              " FROM tspl_transfer_head AS h  INNER JOIN TSPL_TRANSFER_DETAIL AS D ON h.Transfer_No=D.Transfer_No "
        Dim WhrCls As String = "h.Post = 'Y' AND D.Pending_Qty > '0' AND h.To_Location IN (SELECT     Location_Code FROM TSPL_LOCATION_MASTER WHERE  Location_Type = 'Logical')"

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += " and h.From_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        Dim strCurrCode As String = txtTransferNo.Value
        txtTransferNo.Value = clsCommon.ShowSelectForm("ShipTransferFiND", qry, "Code", WhrCls, txtTransferNo.Value, "", isButtonClicked)

        qry = "select TSPL_TRANSFER_HEAD.Transfer_No,(select InTable.Transfer_No from TSPL_TRANSFER_HEAD  as InTable where InTable.Load_Out_No=TSPL_TRANSFER_HEAD.Transfer_No) as LoadInNo,(select Post from TSPL_TRANSFER_HEAD  as InTable where InTable.Load_Out_No=TSPL_TRANSFER_HEAD.Transfer_No) as IsPost"
        qry += " from TSPL_TRANSFER_HEAD where TSPL_TRANSFER_HEAD.Transfer_No='" + txtTransferNo.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            txtTransferNo.Value = ""
            Exit Sub
        ElseIf clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("LoadInNo"))) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please First Create and Post Loadin against Transfer No : " + clsCommon.myCstr(dt.Rows(0)("Transfer_No")) + "")
            txtTransferNo.Value = ""
            Exit Sub
        ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("IsPost")), "N") = CompairStringResult.Equal Then
            common.clsCommon.MyMessageBoxShow("Please First Post Loadin No : " + clsCommon.myCstr(dt.Rows(0)("LoadInNo")) + "")
            txtTransferNo.Value = ""
            Exit Sub
        End If

        Dim dtStartTime As DateTime = DateTime.Now
        LoadTransferData()

        funSetFirstRow()
        Dim dtEndTime As DateTime = DateTime.Now
        Dim span As TimeSpan = dtEndTime.Subtract(dtStartTime)
    End Sub

    Private Sub LoadTransferData()
        Dim lastbatchnumber As String = ""
        Dim balanceqty As Decimal = 0
        Dim orderqty As Decimal = 0
        Dim batch As String = ""
        Dim conversionfactor As Decimal = 0
        isInsideLoadData = True
        Try
            sql = "SELECT  Transfer_Date,  Price_Date, Price_Code, From_Location, Route_No, Salesmancode, Vehicle_Code, Vehicle_No,  " & _
            "Mode_Of_Transport, Km_Reading,Reference, description,to_location, (select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code = to_location) as [SaleMan] FROM tspl_transfer_head " & _
           " WHERE [Transfer_No]='" + txtTransferNo.Value + "'"
            Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(sql)
            If dr1 IsNot Nothing AndAlso dr1.Rows.Count > 0 Then
                RemoveHandler rbFC.ToggleStateChanged, AddressOf rbFC_ToggleStateChanged
                rbFC.IsChecked = True
                AddHandler rbFC.ToggleStateChanged, AddressOf rbFC_ToggleStateChanged
                txtTransferDate.Value = CDate(dr1.Rows(0)(0).ToString())
                txtDate.Value = CDate(dr1.Rows(0)(0).ToString())

                cboPriceDate.Text = CDate(dr1.Rows(0)(1).ToString()).ToString("dd/MM/yyyy")
                txtPriceCode.Text = Convert.ToString(dr1.Rows(0)(2))
                txtLocation.Value = Convert.ToString(dr1.Rows(0)(3))
                txtRemarks.Text = clsCommon.myCstr(dr1.Rows(0)("Reference"))
                txtRouteNo.Value = Convert.ToString(dr1.Rows(0)(4))
                txtSalesman.Value = Convert.ToString(dr1.Rows(0)("to_location"))
                lblSalesMan1.Text = Convert.ToString(dr1.Rows(0)("SaleMan"))
                txtVehicleCode.Value = Convert.ToString(dr1.Rows(0)(6))
                lblVhicleNo.Text = Convert.ToString(dr1.Rows(0)(7))
                cboModeOfTransport.Text = Convert.ToString(dr1.Rows(0)(8))
                txtKMReading.Text = Convert.ToString(clsCommon.myCdbl(dr1.Rows(0)(9)))
                txtRef.Text = Convert.ToString(dr1.Rows(0)(10))
                txtDesc.Text = Convert.ToString(dr1.Rows(0)(11))
                txtLocation.Enabled = False

                funfilltransfer(False, "ALL")

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
            resetForm()
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub gvLoadOut_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If e.Column Is gv1.Columns(colUnitCode) Then
                    gv1.CurrentRow.Cells(colUnitCode).ReadOnly = True

                ElseIf e.Column Is gv1.Columns(ColICode) Then
                    If gv1.CurrentRow.Index < 0 Then
                        gv1.CurrentRow.Cells(ColICode).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(ColICode).ReadOnly = True
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colFromSchemeCode).Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colFromSchemeCode).Value).Substring(0, 2), "MS") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(ColICode).ReadOnly = False
                            End If
                        End If
                    End If

                ElseIf (e.Column Is gv1.Columns(colPriceDateActual)) Then
                    gv1.Columns(colPriceDateActual).FormatString = "{0:d}"
                ElseIf (e.Column Is gv1.Columns(colMainItem)) Then
                    'gv1.CurrentRow.Cells(colMainItem).ReadOnly = clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeItem).Value), "No") = CompairStringResult.Equal
                    If gv1.CurrentRow.Index < 0 Then
                        gv1.CurrentRow.Cells(colMainItem).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colMainItem).ReadOnly = True
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colFromSchemeCode).Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colFromSchemeCode).Value).Substring(0, 2), "MS") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colMainItem).ReadOnly = False
                            End If
                        End If
                    End If

                ElseIf (e.Column Is gv1.Columns(colDiscountCode)) Then
                    'gv1.CurrentRow.Cells(colDiscountCode).ReadOnly = clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeItem).Value), "No") = CompairStringResult.Equal
                    If gv1.CurrentRow.Index < 0 Then
                        gv1.CurrentRow.Cells(colDiscountCode).ReadOnly = False
                    Else
                        gv1.CurrentRow.Cells(colDiscountCode).ReadOnly = True
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colFromSchemeCode).Value) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colFromSchemeCode).Value).Substring(0, 2), "MS") = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colDiscountCode).ReadOnly = False
                            End If
                        End If
                    End If

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSaveAndPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAndPrint.Click
        SaveAndPrint()
    End Sub

    Private Sub SaveAndPrint()
        Try
            If (saveDataClicked()) Then
                If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                    common.clsCommon.MyMessageBoxShow(GetMessageForTransfer() + "Data Saved Successfully", Me.Text)
                End If
                printdata(False)
                If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal Then
                    Dim qry As String = "select Is_Printed from TSPL_SHIPMENT_MASTER where Shipment_No='" + txtDocNo.Value + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1 Then
                        CreateAnotherTransaction()
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtShipTo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipTo._MYValidating
        If clsCommon.myLen(txtCustomer.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Customer", Me.Text)
            Exit Sub
        End If

        Dim qry As String = "select Ship_To_Code as Code,Ship_To_Desc as Description,Add1,Add2,Add3,Add4,City_Code as City,State,Country,Telphone,Email,Pin_Code from TSPL_SHIP_TO_LOCATION"
        Dim whrClas As String = "Ship_To_Type_Code='" + txtCustomer.Value + "'"
        txtShipTo.Value = clsCommon.ShowSelectForm("LOCONSIGNEE", qry, "Code", whrClas, txtShipTo.Value, "", isButtonClicked)
        lblShipTo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipTo.Value + "'"))
    End Sub

    Sub CreateEmpties(ByVal trans As SqlTransaction)
        If clsCommon.CompairString(cboLoadOutType.Text, "Transfer") = CompairStringResult.Equal AndAlso chkCreateEmpty.Checked Then
            Dim strAdjNo As String = ClsAdjustments.GetEmptyAdjustmentCode(lblSaleInvoiceNo.Text, trans)
            Dim qry As String = "Delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No='" + strAdjNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" + strAdjNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim isFirstTime As Boolean = True
            Dim obj As ClsAdjustments = Nothing
            Dim LineNo As Integer = 1
            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(ColICode).Value) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colShippedQty).Value) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(ii).Cells(colEmptyValue).Value) > 0 Then
                    If isFirstTime Then
                        isFirstTime = False

                        obj = New ClsAdjustments()
                        obj.Adjustment_Date = txtTransferDate.Value
                        obj.Posting_Date = txtTransferDate.Value
                        obj.Reference = txtRef.Text
                        obj.Description = txtDesc.Text
                        obj.Reference_Document = "Sale Invoice"
                        obj.Document_No = lblSaleInvoiceNo.Text
                        obj.Unit_Code = "ALL"
                        obj.ItemType = "E"
                        obj.isBySaleInvoice = True
                        obj.EMP_CODE = txtSalesman.Value
                        obj.EMP_NAME = lblSalesMan1.Text
                        obj.Customer_CODE = txtCustomer.Value
                        obj.Customer_NAME = txtCustomerName.Text
                        obj.Created_time = clsCommon.GetPrintDate(txtDate.Value, "hh:mm tt")
                        obj.Modified_Time = clsCommon.GetPrintDate(txtDate.Value, "hh:mm tt")
                        obj.Vehicle_Code = txtVehicleCode.Value
                        obj.Vehicle_No = lblVhicleNo.Text
                        obj.Trans_Type = "In"
                        obj.Loc_Code = txtLocation.Value
                        obj.Loc_Desc = clsLocation.GetName(txtLocation.Value, trans)
                        obj.Arr = New List(Of ClsAdjustmentsDetails)
                    End If
                    Dim objtr As New ClsAdjustmentsDetails()
                    objtr.Adjustment_Line_No = LineNo
                    objtr.Item_Code = clsItemMaster.GetFatherCode(clsCommon.myCstr(gv1.Rows(ii).Cells(ColICode).Value), trans)
                    If clsCommon.myLen(objtr.Item_Code) <= 0 Then
                        Throw New Exception("Father code not found of item:" + clsCommon.myCstr(gv1.Rows(ii).Cells(ColICode).Value))
                    End If
                    objtr.Item_Description = clsItemMaster.GetItemName(objtr.Item_Code, trans)
                    objtr.Adjustment_Type = "BI"
                    objtr.Location_Code = obj.Loc_Code
                    objtr.Item_Quantity = clsCommon.myCdbl(gv1.Rows(ii).Cells(colShippedQty).Value)
                    objtr.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colEmptyValue).Value)
                    Dim strUOM As String = "EC"
                    If clsCommon.CompairString("FB", clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitCode).Value)) = CompairStringResult.Equal Then
                        strUOM = "EB"
                    End If

                    objtr.Unit_Code = strUOM
                    qry = "select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')"
                    objtr.Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.myLen(objtr.Account_Code) <= 0 Then
                        Throw New Exception("Please set Adjustment Account of purchase Account set of Item" + objtr.Item_Code)
                    End If
                    objtr.Account_Description = clsGLAccount.GetName(objtr.Account_Code, trans)
                    objtr.mrp = clsCommon.myCdbl(gv1.Rows(ii).Cells(colEmptyValue).Value) / clsCommon.myCdbl(gv1.Rows(ii).Cells(colShippedQty).Value)
                    objtr.ItemType = obj.ItemType
                    LineNo = LineNo + 1
                    obj.Arr.Add(objtr)
                End If
            Next
            If obj IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                obj.SaveData(obj, True, strAdjNo, trans)
            End If
        End If
    End Sub

    Private Sub btnSettlement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettlement.Click
        OpenSettlementEntry(lblSaleInvoiceNo.Text, Nothing)
    End Sub

    Private Sub btnSkipExciseInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSkipExciseInvoice.Click
        Dim frm As FrmSkipExciseInvoice = New FrmSkipExciseInvoice()
        frm.strDocType = clsDocType.SaleInvoice
        frm.strDocTransType = clsDocTransactionType.SaleInvoiceExcise
        frm.ShowDialog()
    End Sub

    Private Sub RadMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub fndVehicleCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVehicleCode._MYValidating
        Dim qry As String = "Select Vehicle_Id as Code,Number,Description,Model from TSPL_VEHICLE_MASTER"
        txtVehicleCode.Value = clsCommon.ShowSelectForm("ShipVehicaleFinder", qry, "Code", "", txtVehicleCode.Value, "", isButtonClicked)

        sql = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicleCode.Value + "'"
        lblVhicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
    End Sub

    Private Sub fndemployeecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtEmployeeCode._MYValidating
        Dim qry As String = "select EMP_CODE as Code, Emp_Name as [Employee Name] from TSPL_EMPLOYEE_MASTER"
        txtEmployeeCode.Value = clsCommon.ShowSelectForm("ShipEmpFinder", qry, "Code", "", txtEmployeeCode.Value, "", isButtonClicked)

        qry = "select  Emp_Name  from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + txtEmployeeCode.Value + "'"
        lblEmpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub

    Private Sub fndRouteNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRouteNo._MYValidating

        Dim qry As String = "Select Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
        txtRouteNo.Value = clsCommon.ShowSelectForm("ShipRouteFinder", qry, "Code", "", txtRouteNo.Value, "", isButtonClicked)
        fndRouteNo_TextChanged()
    End Sub

    Private Sub fndSalesman__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesman._MYValidating
        Dim qry As String = "SELECT Emp_Code as Code,Emp_Name as 'Name' from TSPL_EMPLOYEE_MASTER   "
        txtSalesman.Value = clsCommon.ShowSelectForm("ShipsalesmanFinder", qry, "Code", "Emp_Type='SalesMan'", txtSalesman.Value, "", isButtonClicked)
        fndSalesman_TextChanged()
    End Sub

    Private Sub fndPaymentTerms__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPaymentTerm._MYValidating
        Dim qry As String = "SELECT Terms_Code as Code, Terms_Desc as Description, No_Days as 'No of Days' FROM TSPL_TERMS_MASTER"
        txtPaymentTerm.Value = clsCommon.ShowSelectForm("ShipPaymentFinder", qry, "Code", "", txtPaymentTerm.Value, "", isButtonClicked)
    End Sub

    Private Sub txtMannualQty_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMannualQty.Validating
        If txtMannualAmt.Value <= 0 Then
            funSetFirstRow()
            gv1.CurrentColumn = gv1.Columns(colShippedQty)
        End If
    End Sub

    Private Sub btnUpdateWithCustomerNewPrice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateWithCustomerNewPrice.Click
        Try
            If Not UsLock1.Status = ERPTransactionStatus.Pending Then
                Throw New Exception("Transacion's status should be Pending.")
            End If

            If clsCommon.MyMessageBoxShow("Do you want to change customer's New Price", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                clsSaleHead.UpdateWithCustomerNewPrice(False, txtDocNo.Value, txtLocation.Value, txtDate.Value, True, False)
                clsCommon.MyMessageBoxShow("Customer's New Price updated successfully")
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReverse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            Dim strInvNo As String = lblSaleInvoiceNo.Text
            clsSaleHead.UpdateWithCustomerNewPrice(True, txtDocNo.Value, txtLocation.Value, txtDate.Value, True, True)
            LoadData(txtDocNo.Value, NavigatorType.Current)
            lblSaleInvoiceNo.Text = strInvNo
            SaveMainClick()

            LoadData(txtDocNo.Value, NavigatorType.Current)
            'btnReverse.Visible = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteRS.Click
        TecxpertForDelete(True)
    End Sub

    Private Sub TecxpertForDelete(ByVal isForTransfer As Boolean)
        Try
            Dim qry As String = " select ShipmentNo,InvoiceNo,TSPL_SHIPMENT_MASTER.Location,TSPL_SHIPMENT_MASTER.Shipment_Date from(select ShipmentNo,InvoiceNo  from ("
            qry += " select  TSPL_SALE_INVOICE_HEAD.Shipment_No as  ShipmentNo,Source_Doc_No as InvoiceNo,1 as RI"
            qry += " from TSPL_JOURNAL_MASTER   left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= TSPL_JOURNAL_MASTER.Source_Doc_No"
            qry += " where Source_Code='SD-IN' and ABS( isnull(Total_Credit_Amt,0) - isnull(Total_Debit_Amt,0))>1 and Authorized='N' "
            qry += " union all "
            qry += " select ShipmentNo,InvoiceNo,-1 as RI from TEMP_Delete"
            qry += " )xxx group by ShipmentNo,InvoiceNo having sum(RI)>0 "
            qry += "  )xxxx left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=xxxx.ShipmentNo where 2=2 "

            If isForTransfer Then
                qry += " and TSPL_SHIPMENT_MASTER.Shipment_Type='Transfer'"
            Else
                qry += " and TSPL_SHIPMENT_MASTER.Shipment_Type='Sale'"
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim strCode As String = clsCommon.myCstr(dr("ShipmentNo"))
                    clsSaleHead.UpdateWithCustomerNewPrice(True, strCode, clsCommon.myCstr(dr("Location")), clsCommon.myCDate(dr("Shipment_Date")), False, isForTransfer)
                Next
            End If
            common.clsCommon.MyMessageBoxShow("Delete Task Completed", Me.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnCreateRS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateRS.Click
        TecxpertForCreate(True)
    End Sub

    Private Sub TecxpertForCreate(ByVal isForTransfer As Boolean)
        Try
            isRakeshSharmaClicked = True
            Dim qry As String = " select ShipmentNo,InvoiceNo,SUM(1) from ("
            If isForTransfer Then
                qry += " select ShipmentNo,InvoiceNo,1 as RI from TEMP_Delete"
                qry += " union all "
                qry += " select ShipmentNo,InvoiceNo,-1 as RI from TEMP_Created"
            Else
                qry += " select ShipmentNo,InvoiceNo,1 as RI from TEMP_Delete_Sale"
                qry += " union all "
                qry += " select ShipmentNo,InvoiceNo,-1 as RI from TEMP_Created_Sale"
            End If
            qry += " )xxx group by ShipmentNo,InvoiceNo having sum(RI)>0 "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow("Proceed Pending " + clsCommon.myCstr(dt.Rows.Count) + " Task " + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    For Each dr As DataRow In dt.Rows
                        txtDocNo.Value = clsCommon.myCstr(dr("ShipmentNo"))
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                        lblSaleInvoiceNo.Text = clsCommon.myCstr(dr("InvoiceNo"))
                        SaveMainClick()

                    Next
                    common.clsCommon.MyMessageBoxShow("Create Task Completed", Me.Text)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Task Remaining", Me.Text)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        Finally
            isRakeshSharmaClicked = False
        End Try
    End Sub

    Private Sub RadButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteRSSaleType.Click
        TecxpertForDelete(False)
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateRSSaleType.Click
        TecxpertForCreate(False)
    End Sub

    Private Sub btnReverseTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseTransaction.Click
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            If common.clsCommon.MyMessageBoxShow("Do you want to Reverse the current Transaction." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_SALE_INVOICE_HEAD", trans)

            sql = "INSERT INTO TSPL_SHIPMENT_MASTER_CANCEL SELECT * FROM TSPL_SHIPMENT_MASTER WHERE Shipment_No='" + txtDocNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(sql, trans)
            sql = "INSERT INTO TSPL_SHIPMENT_DETAILS_CANCEL SELECT * FROM TSPL_SHIPMENT_DETAILS WHERE Shipment_No='" + txtDocNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(sql, trans)

            sql = "INSERT INTO TSPL_SALE_INVOICE_HEAD_CANCEL(" + strInvColumns + ",Cancel_By,Cancel_Date,Cancel_Remarks) SELECT " + strInvColumns + ",'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "','' FROM TSPL_SALE_INVOICE_HEAD WHERE Sale_Invoice_No='" + lblSaleInvoiceNo.Text + "'"
            clsDBFuncationality.ExecuteNonQuery(sql, trans)
            sql = "INSERT INTO TSPL_SALE_INVOICE_DETAIL_CANCEL SELECT * FROM TSPL_SALE_INVOICE_DETAIL WHERE Sale_Invoice_No='" + lblSaleInvoiceNo.Text + "'"
            clsDBFuncationality.ExecuteNonQuery(sql, trans)

             If clsSaleHead.UpdateWithCustomerNewPrice(True, txtDocNo.Value, txtLocation.Value, txtDate.Value, False, True, True, trans) Then
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Transaction Reversed successfully", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Last)
                btnReverseTransaction.Visible = False
            End If

        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SplitContainer1_Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitContainer1.Panel2.Paint

    End Sub

    Private Sub RadMenuItem1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        printdata(True)
    End Sub

    Private Sub RadMenuItem2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        printdata(False)
    End Sub

    Private Sub RadButton2_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        'Dim frm As frmShipmentNew = New frmShipmentNew()
        'frm.Show()
    End Sub

    Private Sub MyTextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscPer.Leave, txtDiscAmt.Leave
        CalculateDiscountAmount()
    End Sub

    'Private Sub CalculateDiscountAmount()
    '    If chkDiscountOnRate.IsChecked Then
    '        Dim discountrate As Decimal = Decimal.Parse(txtDiscPer.Text)
    '        Dim disamt As Decimal = 0
    '        Dim dblCustDiscountNoTax As Double = 0
    '        If String.IsNullOrEmpty(txtShipmentTotal.Text) Then
    '            txtShipmentTotal.Text = 0
    '        End If
    '        Dim pricedate As String = String.Empty
    '        For Each gro As GridViewRowInfo In gv1.Rows
    '            If clsCommon.myCdbl(gro.Cells(colShippedQty).Value) <> 0 Then
    '                pricedate = CDate(gro.Cells(colPriceDateColumn).Value).ToString("yyyy-MM-dd")
    '                If (clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colSampleItem).Value)) = CompairStringResult.Equal) Then
    '                    disamt = 0
    '                Else
    '                    Dim itemnameamt As Decimal = clsDBFuncationality.getSingleValue("select Item_Basic_Price  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + Convert.ToString(gro.Cells(ColICode).Value) + "' and Item_Basic_Net = '" + clsCommon.myCstr(gro.Cells(colMRP).Value) + "' and Price_Code = '" + Convert.ToString(txtPriceCode.Text) + "' and Start_Date = '" + pricedate + "'")
    '                    disamt = Math.Round(clsCommon.myCdbl(itemnameamt) * discountrate / 100, 2)
    '                End If
    '                Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gro.Cells(ColICode).Value), clsCommon.myCstr(clsCommon.myCstr(gro.Cells(colUnitCode).Value)), Nothing)
    '                dblCustDiscountNoTax = clsCommon.myCdbl(gro.Cells(ColCustDisNoTax).Value)
    '                gro.Cells(colDiscountAmount).Value = disamt
    '                gro.Cells(colTotalDiscountAmount).Value = Math.Round(((disamt + dblCustDiscountNoTax) * clsCommon.myCdbl(gro.Cells(colShippedQty).Value)) / dblConvFac, 2)
    '                If ((clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal)) Then
    '                    gro.Cells(colitemNetAmount).Value = 0
    '                Else
    '                    gro.Cells(colitemNetAmount).Value = Math.Round(gro.Cells(colBasicAmount).Value - disamt - (dblCustDiscountNoTax) / dblConvFac, 5)
    '                End If
    '                gro.Cells(colTotalNetAmount).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTotalBasicAmount).Value - gro.Cells(colTotalCustDiscount).Value - gro.Cells(colTotalDiscountAmount).Value), 2)
    '                Dim disctpt As Decimal = Math.Round(clsCommon.myCdbl(gro.Cells(colTPT).Value * gro.Cells(colShippedQty).Value) * discountrate / 100, 2)
    '                loadoutCellValueChange(gro)
    '                If ((clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal)) Then
    '                    gro.Cells(colTotalTPT).Value = 0
    '                Else
    '                    gro.Cells(colTotalTPT).Value = clsCommon.myCdbl(gro.Cells(colTotalTPT).Value) - disctpt
    '                End If
    '                gro.Cells(colTotalItemAmount).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTotalNetAmount).Value) + clsCommon.myCdbl(gro.Cells(colTotalTaxAmount).Value) + clsCommon.myCdbl(gro.Cells(colTotalTPT).Value), 2)
    '            End If
    '        Next
    '        If discountrate = 0 Then
    '            txtDiscAmt.Text = 0
    '        ElseIf discountrate = 100 Then
    '            Dim txtvalue As Decimal = 0
    '            For Each g As GridViewRowInfo In gv1.Rows
    '                If clsCommon.myCdbl(g.Cells(colShippedQty).Value) > 0 Then
    '                    txtShipmentTotal.Text = txtShipmentTotal.Text + clsCommon.myCdbl(g.Cells(colBasicAmount).Value) * clsCommon.myCdbl(g.Cells(colShippedQty).Value)
    '                    txtvalue += clsCommon.myCdbl(g.Cells(colTotalItemAmount).Value)
    '                End If
    '            Next
    '            txtShipmentAmt.Text = txtTotalTaxAmount.Text
    '            txtDiscAmt.Text = txtShipmentTotal.Text
    '            txtNetShipAmt.Text = 0
    '        Else
    '            txtDiscAmt.Text = Math.Round(clsCommon.myCdbl(txtShipmentTotal.Text) * discountrate / 100, 2)
    '        End If
    '        Dim totaltpt As Decimal = 0
    '        For Each tptcount As GridViewRowInfo In gv1.Rows
    '            totaltpt = totaltpt + clsCommon.myCdbl(tptcount.Cells(colTotalTPT).Value)
    '        Next
    '    ElseIf chkDiscountOnAmt.IsChecked Then
    '        txtDiscPer.Text = clsCommon.myCstr(Math.Round(((txtDiscAmt.Value * 100) / clsCommon.myCdbl(txtShipmentTotal.Text)), 5, MidpointRounding.ToEven))
    '        For Each gro As GridViewRowInfo In gv1.Rows
    '            If clsCommon.myCdbl(gro.Cells(colTotalBasicAmount).Value) > 0 Then
    '                Dim disamt As Double = txtDiscAmt.Value * (clsCommon.myCdbl(gro.Cells(colTotalBasicAmount).Value) / clsCommon.myCdbl(txtShipmentTotal.Text))
    '                gro.Cells(colDiscountAmount).Value = disamt / clsCommon.myCdbl(gro.Cells(colShippedQty).Value)
    '                gro.Cells(colTotalDiscountAmount).Value = Math.Round((disamt + clsCommon.myCdbl(gro.Cells(ColCustDisNoTax).Value)), 2)
    '                gro.Cells(colitemNetAmount).Value = Math.Round(gro.Cells(colBasicAmount).Value - clsCommon.myCdbl(gro.Cells(colTotalDiscountAmount).Value), 5)
    '                gro.Cells(colTotalTPT).Value = (clsCommon.myCdbl(gro.Cells(colTPT).Value) * clsCommon.myCdbl(gro.Cells(colShippedQty).Value)) - clsCommon.myCdbl(gro.Cells(colDiscountAmount).Value)
    '                loadoutCellValueChange(gro)
    '                gro.Cells(colTotalItemAmount).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTotalNetAmount).Value) + clsCommon.myCdbl(gro.Cells(colTotalTaxAmount).Value) + clsCommon.myCdbl(gro.Cells(colTotalTPT).Value), 2)
    '            End If
    '        Next

    '    End If
    'End Sub



    Private Sub CalculateDiscountAmount()
        Dim discountrate As Decimal = Decimal.Parse(txtDiscPer.Text)
        If chkDiscountOnAmt.IsChecked AndAlso clsCommon.myCdbl(txtShipmentTotal.Text) > 0 Then
            discountrate = clsCommon.myCstr(Math.Round(((txtDiscAmt.Value * 100) / clsCommon.myCdbl(txtShipmentTotal.Text)), 5, MidpointRounding.ToEven))
            txtDiscPer.Value = discountrate
        End If
        Dim dblDiscountAmtPerUnit As Decimal = 0
        Dim dblDiscountAmt As Decimal = 0
        Dim dblCustDiscountNoTax As Double = 0
        If String.IsNullOrEmpty(txtShipmentTotal.Text) Then
            txtShipmentTotal.Text = 0
        End If

        If chkDiscountOnAmt.IsChecked Then

            For Each gro As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(gro.Cells(colShippedQty).Value) <> 0 Then
                    If (clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colSampleItem).Value)) = CompairStringResult.Equal) Then
                        dblDiscountAmt = 0
                        dblDiscountAmtPerUnit = 0
                    Else
                        dblDiscountAmt = Math.Round((clsCommon.myCdbl(gro.Cells(colTotalBasicAmount).Value) * txtDiscAmt.Value) / clsCommon.myCdbl(txtShipmentTotal.Text), 2)
                        dblDiscountAmtPerUnit = Math.Round(dblDiscountAmt / clsCommon.myCdbl(gro.Cells(colShippedQty).Value), 5)
                    End If

                    Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gro.Cells(ColICode).Value), clsCommon.myCstr(clsCommon.myCstr(gro.Cells(colUnitCode).Value)), Nothing)
                    dblCustDiscountNoTax = clsCommon.myCdbl(gro.Cells(ColCustDisNoTax).Value)

                    gro.Cells(colTotalDiscountAmount).Value = Math.Round(dblDiscountAmt + (((dblCustDiscountNoTax) * clsCommon.myCdbl(gro.Cells(colShippedQty).Value)) / dblConvFac), 2)
                    gro.Cells(colDiscountAmount).Value = dblDiscountAmtPerUnit

                    If ((clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal)) Then
                        gro.Cells(colitemNetAmount).Value = 0
                    Else
                        gro.Cells(colitemNetAmount).Value = Math.Round(gro.Cells(colBasicAmount).Value - dblDiscountAmtPerUnit - (dblCustDiscountNoTax) / dblConvFac, 5)
                    End If
                    gro.Cells(colTotalNetAmount).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTotalBasicAmount).Value - gro.Cells(colTotalCustDiscount).Value - gro.Cells(colTotalDiscountAmount).Value), 2)
                    Dim disctpt As Decimal = Math.Round(clsCommon.myCdbl(gro.Cells(colTPT).Value * gro.Cells(colShippedQty).Value) * discountrate / 100, 2)
                    loadoutCellValueChange(gro)
                    If ((clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal)) Then
                        gro.Cells(colTotalTPT).Value = 0
                    Else
                        gro.Cells(colTotalTPT).Value = clsCommon.myCdbl(gro.Cells(colTotalTPT).Value) - disctpt
                    End If
                    gro.Cells(colTotalItemAmount).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTotalNetAmount).Value) + clsCommon.myCdbl(gro.Cells(colTotalTaxAmount).Value) + clsCommon.myCdbl(gro.Cells(colTotalTPT).Value), 2)
                End If
            Next
        Else
            For Each gro As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(gro.Cells(colShippedQty).Value) <> 0 Then
                    If (clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colSampleItem).Value)) = CompairStringResult.Equal) Then
                        dblDiscountAmtPerUnit = 0
                    Else
                        Dim itemnameamt As Decimal = clsCommon.myCdbl(gro.Cells(colBasicAmount).Value) ''clsDBFuncationality.getSingleValue("select Item_Basic_Price  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + Convert.ToString(gro.Cells(ColICode).Value) + "' and Item_Basic_Net = '" + clsCommon.myCstr(gro.Cells(colMRP).Value) + "' and Price_Code = '" + Convert.ToString(txtPriceCode.Text) + "' and Start_Date = '" + pricedate + "'")
                        dblDiscountAmtPerUnit = Math.Round(clsCommon.myCdbl(itemnameamt) * discountrate / 100, 5)
                    End If
                    Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gro.Cells(ColICode).Value), clsCommon.myCstr(clsCommon.myCstr(gro.Cells(colUnitCode).Value)), Nothing)
                    dblCustDiscountNoTax = clsCommon.myCdbl(gro.Cells(ColCustDisNoTax).Value)
                    gro.Cells(colDiscountAmount).Value = dblDiscountAmtPerUnit
                    gro.Cells(colTotalDiscountAmount).Value = Math.Round(((dblDiscountAmtPerUnit + dblCustDiscountNoTax) * clsCommon.myCdbl(gro.Cells(colShippedQty).Value)) / dblConvFac, 2)
                    If ((clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal)) Then
                        gro.Cells(colitemNetAmount).Value = 0
                    Else
                        gro.Cells(colitemNetAmount).Value = Math.Round(gro.Cells(colBasicAmount).Value - dblDiscountAmtPerUnit - (dblCustDiscountNoTax) / dblConvFac, 5)
                    End If
                    gro.Cells(colTotalNetAmount).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTotalBasicAmount).Value - gro.Cells(colTotalCustDiscount).Value - gro.Cells(colTotalDiscountAmount).Value), 2)
                    Dim disctpt As Decimal = Math.Round(clsCommon.myCdbl(gro.Cells(colTPT).Value * gro.Cells(colShippedQty).Value) * discountrate / 100, 2)
                    loadoutCellValueChange(gro)
                    If ((clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal)) Then
                        gro.Cells(colTotalTPT).Value = 0
                    Else
                        gro.Cells(colTotalTPT).Value = clsCommon.myCdbl(gro.Cells(colTotalTPT).Value) - disctpt
                    End If
                    gro.Cells(colTotalItemAmount).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTotalNetAmount).Value) + clsCommon.myCdbl(gro.Cells(colTotalTaxAmount).Value) + clsCommon.myCdbl(gro.Cells(colTotalTPT).Value), 2)
                End If
            Next
        End If
        If discountrate = 0 Then
            txtDiscAmt.Text = 0
        Else
            txtDiscAmt.Text = Math.Round(clsCommon.myCdbl(txtShipmentTotal.Text) * discountrate / 100, 2)
        End If
    End Sub



    Private Sub MyTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscPer.TextChanged
        CheckTransactionDiscount()
    End Sub

    Private Sub CheckTransactionDiscount()
        If Not String.IsNullOrEmpty(txtDiscPer.Text) Then
            Dim totaltptamt As Decimal = 0
            For Each gr As GridViewRowInfo In gv1.Rows
                If Not clsCommon.myCdbl(gr.Cells(colShippedQty).Value) = 0 Then
                    totaltptamt += clsCommon.myCdbl(gr.Cells(colTotalTPT).Value)
                End If

            Next
            Dim discountrate As Decimal = Decimal.Parse(txtDiscPer.Text)
            If discountrate > 100 Then
                common.clsCommon.MyMessageBoxShow("Not more than 100")
                txtDiscPer.Text = 0
                'Else
                '    If discountrate = 100 Then
                '        txtShipmentTotal.Text = 0.0
                '    End If
            End If
        End If
    End Sub

    Private Sub chkDiscountOnAmt_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDiscountOnAmt.ToggleStateChanged, chkDiscountOnRate.ToggleStateChanged
        If chkDiscountOnAmt.IsChecked Then
            txtDiscAmt.Enabled = True
            txtDiscPer.Enabled = False
        Else
            txtDiscAmt.Enabled = False
            txtDiscPer.Enabled = True
        End If
    End Sub

    Private Sub txtOrderNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtOrderNo._MYValidating
        Dim qry As String = "Select Order_No as Code,Cust_Code as 'Customer Code',Cust_Name as 'Customer Name',CONVERT(varchar(15), Order_Date , 103) as [Shipment Date] ,Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as Location, case when Is_Post=0 then 'Unposted' else 'Posted' end as [Status], Route_No as [Route Code],Route_Desc as [Route] "
        qry += " FROM TSPL_SALES_ORDER_HEAD "
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALES_ORDER_HEAD.Location"
        Dim whrClas As String = " 2=2 and Is_Post=1"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and TSPL_SALES_ORDER_HEAD.Location  in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtOrderNo.Value = clsCommon.ShowSelectForm("SoFNDSHipemnt", qry, "Code", whrClas, txtOrderNo.Value, "", isButtonClicked)
        LoadBlankGrid()

        isInsideLoadData = True


        gvTax.DataSource = Nothing
        gvTax.Rows.Clear()

        Dim shipmentamt As Decimal = 0
        Dim taxamt1 As Decimal = 0
        Dim TAXGROUP As String = ""
        Dim basicamt1 As Decimal = 0

        Try
            Dim strLocation As String = ""
            Dim obj As clsSalesOrder = clsSalesOrder.GetData(txtOrderNo.Value, NavigatorType.Current, Nothing)

            dtEndTime = DateTime.Now
            span = dtEndTime.Subtract(dtStartTime)
            strMsg += "query Execute:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
            dtStartTime = DateTime.Now

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Order_No) > 0 Then
                'txtDocNo.Value = obj.Order_No
                txtOrderDate.Value = obj.Order_Date
                gvTax.DataSource = Nothing
                gvTax.Rows.Clear()
                gvTax.AllowAddNewRow = True
                txtPriceCode.Text = obj.Price_Code

                RemoveHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
                RemoveHandler txtCustDisc.TextChanged, AddressOf totalAmounts
                RemoveHandler txtDiscPer.TextChanged, AddressOf MyTextBox1_TextChanged

                taxamt1 = obj.Order_Tax_Amt
                shipmentamt = obj.Total_Order_Amt
                'txtDate.Enabled = True

                txtshellqty.Text = obj.Shell_Qty

                txtCustDisc.Text = clsCommon.myFormat(obj.Tot_Customer_Dis_Amt)
                txtDiscPer.Text = clsCommon.myFormat(obj.Order_Disc_Percent)
                txtDiscAmt.Text = clsCommon.myFormat(obj.Order_Discount_Amt - obj.Tot_Customer_Dis_Amt)
                txtNetShipAmt.Text = clsCommon.myFormat(obj.Order_Detail_Total_Amt)
                txtContainerDeposit.Text = clsCommon.myFormat(obj.Empty_Value)
                txtCustomer.Value = obj.Cust_Code
                sql = "SELECT Cust_Account FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code = '" + txtCustomer.Value + "'"
                strCustAccount = clsDBFuncationality.getSingleValue(sql)
                txtCustomerName.Text = obj.Cust_Name
                txtCustomerPONO.Text = obj.Cust_PONo
                txtExpectedShipDate.Value = obj.Expected_Ship_Date



                txtRef.Text = obj.Ref_No
                txtDesc.Text = obj.Description
                txtRemarks.Text = obj.Remarks
                txtShipTo.Value = obj.Ship_To
                lblShipTo.Text = obj.Ship_To_Desc

                TAXGROUP = obj.Tax_Group
                txtLocation.Value = obj.Location
                basicamt1 = obj.Order_Detail_Total_Amt
                txtTotalTaxAmount.Text = clsCommon.myFormat(obj.Order_Tax_Amt)
                txtShipmentAmt.Text = clsCommon.myFormat(obj.Total_Order_Amt)
                txtEmployeeCode.Value = obj.Employee_Code
                sql = "SELECT  Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code FROM TSPL_SALESMAN_MAPPING WHERE Salesman_Code='" + obj.Route_No + "'"
                Dim dtable As DataTable = clsDBFuncationality.GetDataTable(sql)
                If dtable IsNot Nothing AndAlso dtable.Rows.Count > 0 Then
                    l1User = dtable.Rows(0)(0).ToString()
                    l2User = dtable.Rows(0)(1).ToString()
                    l3User = dtable.Rows(0)(2).ToString()
                    l4User = dtable.Rows(0)(3).ToString()
                    l5User = dtable.Rows(0)(4).ToString()
                Else
                    common.clsCommon.MyMessageBoxShow("Salesman does not exist.")
                    txtSalesman.Focus()
                    Exit Sub
                End If
                txtTotalTPT.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(SUM(ISNULL(Total_TPT,0)),0)  FROM TSPL_SALES_ORDER_DETAIL  WHERE Order_No = '" + txtDocNo.Value + "'")
                cboModeOfTransport.Text = obj.Mode_Of_Transport
                txtVehicleCode.Value = obj.Vehicle_Code
                lblVhicleNo.Text = obj.Vehicle_No
                txtRouteNo.Value = obj.Route_No
                lblRouteDesc.Text = obj.Route_Desc
                txtSalesman.Value = obj.Salesman_Code
                cboPriceDate.Text = Format(CDate(obj.Price_Date), "dd/MM/yyyy")
                txtPaymentTerm.Value = obj.Terms_Code
                chkDiscountOnRate.IsChecked = IIf(obj.Discount_On = 0, True, False)
                chkDiscountOnAmt.IsChecked = IIf(obj.Discount_On = 1, True, False)

                txtCustomer.Enabled = False
                'txtDate.Enabled = False
                txtLocation.Enabled = False

                Dim MRP As Decimal = totalMRP()
                Dim basicAmt As Decimal = totalBasicAmt()
                Dim netAmt As Decimal = totalNetAmount()


                dtEndTime = DateTime.Now
                span = dtEndTime.Subtract(dtStartTime)
                strMsg += "Data Load From Transaction:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
                dtStartTime = DateTime.Now

                funfilldetailSalesOrder(obj)


                If clsCommon.myCdbl(txtDiscPer.Text) < 100 Then
                    txtShipmentTotal.Text = clsCommon.myCdbl(txtDiscAmt.Text) + clsCommon.myCdbl(txtNetShipAmt.Text)
                ElseIf clsCommon.myCdbl(txtDiscPer.Text) = 100 Then
                    txtShipmentTotal.Text = clsCommon.myCdbl(txtDiscAmt.Text)
                    txtNetShipAmt.Text = 0
                End If

                funtotalfcfb()
                txtShipmentAmt.Text = shipmentamt
                txtTotalShipmentAmt.Text = clsCommon.myCdbl(txtContainerDeposit.Text) + clsCommon.myCdbl(txtShipmentAmt.Text)
                txtTotalTaxAmount.Text = taxamt1

                RemoveHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
                fndTaxGroup.txtValue.Text = ""
                fndTaxGroup.txtValue.Text = TAXGROUP
                lblTaxDesc.Text = clsTaxGroupMaster.GetNameOfSaleType(fndTaxGroup.txtValue.Text, Nothing)
                sql = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code " & _
           " WHERE G.Tax_Group_Code = '" + TAXGROUP + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " ORDER BY G.Trans_Code"
                ds = clsDBFuncationality.GetDataTable(sql)
                If ds.Rows.Count > 0 Then
                    gvTax.DataSource = ds
                End If
                AddHandler fndTaxGroup.txtValue.TextChanged, AddressOf fndTaxGroup_TextChanged
                AddHandler txtCustDisc.TextChanged, AddressOf totalAmounts
                AddHandler txtDiscPer.TextChanged, AddressOf MyTextBox1_TextChanged

                gvTax.AllowAddNewRow = False
                Dim i1 As Integer
                gvTax.DataSource = Nothing
                gvTax.Rows.Clear()
                For i1 = 1 To 10
                    sql = "Select (case When Tax" + i1.ToString() + " is NULL THEN '' else Tax" + i1.ToString() + " end),Tax" + i1.ToString() + "_Rate,Tax" + i1.ToString() + "_Assessable_Amt,Tax" + i1.ToString() + "_Amt from TSPL_SALES_ORDER_HEAD WHERE Order_No='" + obj.Order_No + "'"
                    ds = clsDBFuncationality.GetDataTable(sql)
                    If ds IsNot Nothing AndAlso ds.Rows.Count > 0 Then
                        Dim taxCode As String = ds.Rows(0)(0).ToString()
                        Dim taxRate As Decimal = clsCommon.myCdbl(ds.Rows(0)(1))
                        Dim assAmt As Decimal = clsCommon.myCdbl(ds.Rows(0)(2))
                        Dim taxAmt As Decimal = clsCommon.myCdbl(ds.Rows(0)(3))
                        If Not ds.Rows(0)(0).ToString() = "" Then
                            sql = "Select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code='" + ds.Rows(0)(0).ToString() + "'"
                            Dim taxCodeDesc As String = clsDBFuncationality.getSingleValue(sql)
                            Dim grow As GridViewRowInfo = gvTax.Rows.AddNew()
                            grow.Cells("taxAuthority").Value = taxCode
                            grow.Cells("description").Value = taxCodeDesc
                            grow.Cells("taxRate").Value = taxRate
                            grow.Cells(colBasicAmount).Value = basicamt1
                            grow.Cells("assessibleAmount").Value = assAmt
                            grow.Cells(colTaxamount).Value = taxAmt

                        End If
                    End If
                Next
                Dim SQLTAX As String = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code as [taxcode]  FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code  WHERE G.Tax_Group_Code = '" + fndTaxGroup.txtValue.Text + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code  ORDER BY G.Trans_Code"
                ds = clsDBFuncationality.GetDataTable(SQLTAX)
                For k As Integer = 0 To ds.Rows.Count - 1
                    gvTax.Rows(k).Cells("taxable").Value = ds.Rows(k)("Taxable")
                    gvTax.Rows(k).Cells("surtax").Value = ds.Rows(k)("surtax")
                    gvTax.Rows(k).Cells(8).Value = ds.Rows(k)("taxcode")
                Next


            Else
                btnAdd.Text = "Save"
                btnDelete.Enabled = False
                btnPost.Enabled = False
                resetFNDCustomer()
                resetFNDTaxGroup()
                resetForm()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        Finally
            isInsideLoadData = False
        End Try

        funSetFirstRow()
    End Sub



    Private Sub funfilldetailSalesOrder(ByVal obj As clsSalesOrder)
        LoadBlankGrid()
        'Dim i As Integer
        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            For Each objtr As clsSalesOrderDetail In obj.Arr
                Dim datarowinfo As GridViewRowInfo = gv1.Rows.AddNew()
                datarowinfo.Cells(colPriceDateColumn).Value = objtr.Price_Date
                sql = "SELECT isnull(SUM(isnull(Item_Qty,0)),0) FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objtr.Item_Code + "' AND Location_Code='" + txtLocation.Value + "' and MRP='" + clsCommon.myCstr(objtr.MRP_Amt) + "'"
                Try
                    If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                        datarowinfo.Cells(colBalanceQty).Value = objtr.Balance_Qty
                    Else
                        datarowinfo.Cells(colBalanceQty).Value = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql)), 2)
                    End If
                Catch ex As Exception
                    datarowinfo.Cells(colBalanceQty).Value = 0
                End Try
                Dim OrderBalanceQty As Double = clsSalesOrderDetail.GetBalanceQty(obj.Order_No, objtr.Item_Code, objtr.Unit_code, "", objtr.From_Scheme_Code)

                datarowinfo.Cells(colOrderedQty).Value = objtr.Order_Qty
                datarowinfo.Cells(colShippedQty).Value = OrderBalanceQty
                datarowinfo.Cells(ColActualBalQty).Value = OrderBalanceQty
                datarowinfo.Cells(colUnitCode).Value = objtr.Unit_code
                datarowinfo.Cells(collocation).Value = obj.Location
                datarowinfo.Cells(ColICode).Value = objtr.Item_Code
                datarowinfo.Cells(colPriceCode).Value = obj.Price_Code
                datarowinfo.Cells(colSchemeApplicable).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Applicable) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeCodeItem).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Item) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeItem).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Disc_Applicable) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeApplicable).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Applicable) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeCodeItem).Value = objtr.Scheme_Code_Qty
                datarowinfo.Cells(colSchemeItem).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Item) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeDiscountApplicable).Value = IIf(clsCommon.CompairString("N", objtr.Scheme_Disc_Applicable) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colSchemeCodeDiscount).Value = objtr.Scheme_Code_Cash
                datarowinfo.Cells(colSampleItem).Value = IIf(clsCommon.CompairString("N", objtr.Sampling_Item) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colEmptyValue).Value = objtr.Empty_Value
                datarowinfo.Cells(colMRP).Value = objtr.MRP_Amt
                datarowinfo.Cells(colMRPInBottle).Value = clsItemMaster.GetMRPInBottle(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), clsCommon.myCdbl(datarowinfo.Cells(colMRP).Value))
                datarowinfo.Cells(colBasicAmount).Value = objtr.Basic_Rate
                datarowinfo.Cells(colDiscountAmount).Value = objtr.Disc_Amt
                datarowinfo.Cells(colitemNetAmount).Value = objtr.Item_Net_Amt
                datarowinfo.Cells(colTaxamount).Value = objtr.Item_Net_Amt
                datarowinfo.Cells(colTotalMRP).Value = objtr.Total_MRP_Amt
                datarowinfo.Cells(colTotalBasicAmount).Value = objtr.Total_Basic_Amt
                datarowinfo.Cells(colTotalDiscountAmount).Value = objtr.Total_Disc_Amt
                datarowinfo.Cells(colTotalNetAmount).Value = objtr.Total_net_Amt
                datarowinfo.Cells(colTotalTaxAmount).Value = objtr.Total_Tax_Amt
                datarowinfo.Cells(colTPT).Value = objtr.TPT
                datarowinfo.Cells(colTotalTPT).Value = Math.Round(objtr.TPT * objtr.Order_Qty, 4)
                datarowinfo.Cells(colTotalItemAmount).Value = objtr.Total_Item_Amt
                datarowinfo.Cells(colPromoSchemeApplicable).Value = IIf(clsCommon.CompairString("N", objtr.Promo_Scheme_Applicable) = CompairStringResult.Equal, "No", "Yes")
                datarowinfo.Cells(colPromoSchemeCode).Value = objtr.Promo_Scheme_Code
                datarowinfo.Cells(colPromoSchemeItem).Value = IIf(clsCommon.CompairString("N", objtr.Promo_Scheme_Item) = CompairStringResult.Equal, "No", "Yes")

                datarowinfo.Cells(colFromSchemeCode).Value = objtr.From_Scheme_Code
                datarowinfo.Cells(colEmptyValueShell).Value = objtr.Empty_Value_Shell
                datarowinfo.Cells(colEmptyValueBottle).Value = objtr.Empty_Value_Bottle
                datarowinfo.Cells(colTransferBasicAmount).Value = objtr.Transfer_Basic_Amount
                datarowinfo.Cells(ColItemName).Value = objtr.Item_Desc
                datarowinfo.Cells(colBatchNumber).Value = objtr.Batch_No
                datarowinfo.Cells(colcustDiscount).Value = objtr.Cust_Discount
                datarowinfo.Cells(colTotalCustDiscount).Value = objtr.Total_Cust_Discount
                datarowinfo.Cells(ColCustDisNoTax).Value = objtr.Cust_Item_Discount_NoTax
                datarowinfo.Cells(colDiscountCode).Value = objtr.Discount_Code
                datarowinfo.Cells(ColTargetDisAmt).Value = objtr.Target_Discount_Amt
                datarowinfo.Cells(ColPriceToShow).Value = objtr.Price_To_Show
                If objtr.Price_Date_Actual.HasValue Then
                    datarowinfo.Cells(colPriceDateActual).Value = objtr.Price_Date_Actual
                End If
                datarowinfo.Cells(colAbatementRate).Value = objtr.Abatement_rate
                datarowinfo.Cells(colMainItem).Value = objtr.Main_Item

                Dim convFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), Nothing)
                If convFac = 1 Then
                    datarowinfo.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), txtLocation.Value, clsCommon.myCstr(datarowinfo.Cells(colMRP).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), txtDocNo.Value, txtDate.Value)
                End If

                datarowinfo.Cells(colTax1Rate).Value = objtr.TAX1_Rate
                datarowinfo.Cells(colAssess1).Value = objtr.TAX1_Assessable_Amt
                datarowinfo.Cells(colTax1Amt).Value = objtr.TAX1_Amt
                datarowinfo.Cells(colTax2Rate).Value = objtr.TAX2_Rate
                datarowinfo.Cells(colAssess2).Value = objtr.TAX2_Assessable_Amt
                datarowinfo.Cells(colTax2Amt).Value = objtr.TAX2_Amt
                datarowinfo.Cells(colTax3Rate).Value = objtr.TAX3_Rate
                datarowinfo.Cells(colAssess3).Value = objtr.TAX3_Assessable_Amt
                datarowinfo.Cells(colTax3Amt).Value = objtr.TAX3_Amt
                datarowinfo.Cells(colTax4Rate).Value = objtr.TAX4_Rate
                datarowinfo.Cells(colAssess4).Value = objtr.TAX4_Assessable_Amt
                datarowinfo.Cells(colTax4Amt).Value = objtr.TAX4_Amt
                datarowinfo.Cells(colTax5Rate).Value = objtr.TAX5_Rate
                datarowinfo.Cells(colAssess5).Value = objtr.TAX5_Assessable_Amt
                datarowinfo.Cells(colTax5Amt).Value = objtr.TAX5_Amt
                datarowinfo.Cells(colTax6Rate).Value = objtr.TAX6_Rate
                datarowinfo.Cells(colAssess6).Value = objtr.TAX6_Assessable_Amt
                datarowinfo.Cells(colTax6Amt).Value = objtr.TAX6_Amt

                loadoutCellValueChange(datarowinfo)
                calculateItemTaxAgainstItemRate(datarowinfo)
                CalculateTaxByLocation(datarowinfo)

                gv1.Refresh()
            Next
        End If

    End Sub


    Private Sub txtMannaulInvoiceNo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtMannaulInvoiceNo.Validating
        Try
            Dim qry As String = "select 1 from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + txtMannaulInvoiceNo.Text + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsCommon.MyMessageBoxShow("Mannual Invoice No " + txtMannaulInvoiceNo.Text + " is Already exist")
                txtMannaulInvoiceNo.Text = ""
                txtMannaulInvoiceNo.Focus()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnRecalTransAndReCreateJE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecalTransAndReCreateJE.Click
        Try
            isRakeshSharmaClicked = True
            Dim qry As String = " select ShipmentNo,InvoiceNo,SUM(1) from ("
            qry += " select ShipmentNo,InvoiceNo,1 as RI from TEMP_Delete"
            qry += " union all "
            qry += " select ShipmentNo,InvoiceNo,-1 as RI from TEMP_Created"
            qry += " )xxx group by ShipmentNo,InvoiceNo having sum(RI)>0 "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If common.clsCommon.MyMessageBoxShow("Proceed Pending " + clsCommon.myCstr(dt.Rows.Count) + " Task " + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    For Each dr As DataRow In dt.Rows
                        txtDocNo.Value = clsCommon.myCstr(dr("ShipmentNo"))
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                        lblSaleInvoiceNo.Text = clsCommon.myCstr(dr("InvoiceNo"))
                        CalculateDiscountAmount()
                        SaveMainClick()

                        qry = "update TSPL_SHIPMENT_MASTER set Is_Post='Y',Status='In Progress' where Shipment_No='" + txtDocNo.Value + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)

                        qry = "update TSPL_SALE_INVOICE_HEAD set Is_Post='Y' where Sale_Invoice_No='" + lblSaleInvoiceNo.Text + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry)

                        qry = "insert into TEMP_Created values( '" + txtDocNo.Value + "','" + lblSaleInvoiceNo.Text + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry)
                    Next
                    common.clsCommon.MyMessageBoxShow("Create Task Completed", Me.Text)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Task Remaining", Me.Text)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Me.Text)
        Finally
            isRakeshSharmaClicked = False
        End Try
    End Sub

   
    Private Sub txtRouteNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRouteNo.Load

    End Sub
End Class

