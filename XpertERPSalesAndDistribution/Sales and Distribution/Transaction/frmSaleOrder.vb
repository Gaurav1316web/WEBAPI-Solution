Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports XpertERPEngine
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


Public Class frmSaleOrder
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






    Dim l1User, l2User, l3User, l4User, l5User As String
    Private ttlItemShpQtyForCheck As Decimal = 0
    Dim btntooltip As ToolTip = New ToolTip()


    Dim dtStartTime As DateTime = DateTime.Now
    Dim span As TimeSpan
    Dim dtEndTime As DateTime
    Dim strMsg As String = ""


    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False

    Const ReportID As String = "SalesOrderGrid"
    Public strLoadOutNo As String
    Dim conversionnumber As Decimal
    Dim emptyvalue123 As Decimal = 0




    Dim repoActualBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim customdatarow As DataRow = Nothing




    Const ColICode As String = "itemCode"
    Const ColItemName As String = "itemName"
    Const colPriceDateColumn As String = "pricedate"
    Const colShippedQty As String = "shippedQty"
    Const colBalanceQty As String = "balanceQty"
    Const colUnitCode As String = "unitCode"
    Const colSchemeApplicable As String = "schemeApplicable"
    Const colMainItem As String = "mainitem"
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

    Const colTonnagePerUnit As String = "COLTONNAGEPERUNIT"
    Const colTonnage As String = "COLTONNAGE"

    ''Tax Grid Columns
    Const colTAssessibleAmount As String = "assessibleAmount"
    Const colTTaxAmount As String = "taxAmount"
    Const colTTaxRate As String = "taxRate"
    Const colTBasicAmount As String = "basicAmount"

#End Region

    Public Sub New()
        InitializeComponent()
      
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.saleOrders)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnAdd.Visible = MyBase.isModifyFlag

        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmShipment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtshellqty.Value = 0


        handlers()
        resetForm()
        cboModeOfTransport.Text = "By Road"
        pageLoadOut.IsContentVisible = True
        'globalFunc.mandatoryText(fndTaxGroup.Value)
        globalFunc.mandatoryDropdown(cboModeOfTransport, cboPriceDate)
        Dim lds As DataTable = clsDBFuncationality.GetDataTable(clsERPFuncationality.UserAvailableLocationQuery)
        If lds.Rows.Count = 1 Then
            txtLocation.Value = lds.Rows(0)("Code").ToString()
        End If

        btntooltip.SetToolTip(btnAdd, "Press Alt+S for Save/Update Trasnaction")
        btntooltip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        btntooltip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        btntooltip.SetToolTip(btnClose, "Press Esc Close the Window")
        btntooltip.SetToolTip(btnReset, "Press Alt+N Adding New Transaction")
        btntooltip.SetToolTip(btnPrint, "Press Alt+R For Print")



        gvTax.AllowColumnReorder = False
        gvTax.AllowRowReorder = False
        gvTax.EnableSorting = False

        LoadBlankGrid()
        txtShipmentTotal.Text = 0

        pvLoadOut.SelectedPage = pageLoadOut

        resetdata()
        
        If clsCommon.myLen(strLoadOutNo) > 0 Then
            txtDocNo.Value = strLoadOutNo
            LoadData(strLoadOutNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            txtDocNo.Value = clsCommon.myCstr(Me.Tag)
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoComplete As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoComplete.FormatString = ""
        repoComplete.HeaderText = "Complete"
        repoComplete.Name = "complete"
        repoComplete.ReadOnly = True
        repoComplete.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoComplete)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = ColICode
        repoICode.Width = 60
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.AllowSort = False
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Name"
        repoIName.Name = ColItemName
        repoIName.ReadOnly = True
        repoIName.Width = 64
        gv1.MasterTemplate.Columns.Add(repoIName)

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

       

        Dim repoShipedQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoShipedQty.FormatString = ""
        repoShipedQty.HeaderText = "Order Quantity"
        repoShipedQty.Name = colShippedQty
        repoShipedQty.Width = 80
        repoShipedQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoShipedQty.ReadOnly = False
        repoShipedQty.Minimum = 0
        repoShipedQty.ShowUpDownButtons = False
        gv1.MasterTemplate.Columns.Add(repoShipedQty)

        Dim repoBalQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBalQty.AllowSort = False
        repoBalQty.FormatString = ""
        repoBalQty.HeaderText = "Balance Quantity"
        repoBalQty.Name = colBalanceQty
        repoBalQty.ReadOnly = True
        repoBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBalQty.Width = 94
        repoBalQty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoBalQty)

        Dim repoUnitCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnitCode.FormatString = ""
        repoUnitCode.HeaderText = "Unit Code"
        repoUnitCode.Name = colUnitCode
        repoUnitCode.Width = 59
        repoUnitCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnitCode)

        Dim repoSchemeApp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeApp.AllowSort = False
        repoSchemeApp.FormatString = ""
        repoSchemeApp.HeaderText = "App. Qty Dis."
        repoSchemeApp.Name = colSchemeApplicable
        repoSchemeApp.ReadOnly = True
        repoSchemeApp.Width = 75
        gv1.MasterTemplate.Columns.Add(repoSchemeApp)

        Dim repoMainItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMainItem.FormatString = ""
        repoMainItem.HeaderText = "Main Item"
        repoMainItem.Name = colMainItem
        repoMainItem.Width = 58
        gv1.MasterTemplate.Columns.Add(repoMainItem)

        
        Dim repoPriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceCode.AllowSort = False
        repoPriceCode.HeaderText = "Price Code"
        repoPriceCode.IsVisible = False
        repoPriceCode.Name = colPriceCode
        repoPriceCode.ReadOnly = True
        repoPriceCode.Width = 62
        gv1.MasterTemplate.Columns.Add(repoPriceCode)


        Dim repoSampleItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSampleItem.AllowSort = False
        repoSampleItem.FormatString = ""
        repoSampleItem.HeaderText = "Sample Item"
        repoSampleItem.Name = colSampleItem
        repoSampleItem.ReadOnly = True
        repoSampleItem.Width = 72
        gv1.MasterTemplate.Columns.Add(repoSampleItem)


        Dim repoEmptyValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEmptyValue.AllowSort = False
        repoEmptyValue.FormatString = ""
        repoEmptyValue.HeaderText = "Empty Value"
        repoEmptyValue.Name = colEmptyValue
        repoEmptyValue.ReadOnly = True
        repoEmptyValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoEmptyValue.Width = 72
        gv1.MasterTemplate.Columns.Add(repoEmptyValue)


        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.AllowSort = False
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.ReadOnly = True
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.Width = 33
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoMRPInBottle As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRPInBottle.AllowSort = False
        repoMRPInBottle.FormatString = ""
        repoMRPInBottle.HeaderText = "Bottle MRP"
        repoMRPInBottle.Name = colMRPInBottle
        repoMRPInBottle.ReadOnly = True
        repoMRPInBottle.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRPInBottle.Width = 70
        gv1.MasterTemplate.Columns.Add(repoMRPInBottle)

        Dim repoBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBasicAmt.AllowSort = False
        repoBasicAmt.HeaderText = "Basic Price"
        repoBasicAmt.Name = colBasicAmount
        repoBasicAmt.ReadOnly = True
        repoBasicAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBasicAmt.Width = 65
        gv1.MasterTemplate.Columns.Add(repoBasicAmt)

        Dim repoDiscountAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiscountAmt.AllowSort = False
        repoDiscountAmt.HeaderText = "Discount"
        repoDiscountAmt.Name = colDiscountAmount
        repoDiscountAmt.ReadOnly = True
        repoDiscountAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDiscountAmt.Width = 52
        gv1.MasterTemplate.Columns.Add(repoDiscountAmt)


        Dim repoCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDiscount.HeaderText = "Cust Dis."
        repoCustDiscount.MinWidth = 4
        repoCustDiscount.Name = colcustDiscount
        repoCustDiscount.ReadOnly = True
        repoCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDiscount.Width = 54
        gv1.MasterTemplate.Columns.Add(repoCustDiscount)

        Dim repoItemNetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemNetAmt.AllowSort = False
        repoItemNetAmt.HeaderText = "Net Price"
        repoItemNetAmt.Name = colitemNetAmount
        repoItemNetAmt.ReadOnly = True
        repoItemNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoItemNetAmt.Width = 55
        gv1.MasterTemplate.Columns.Add(repoItemNetAmt)


        Dim repoTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt.AllowSort = False
        repoTaxAmt.HeaderText = "Tax"
        repoTaxAmt.Name = colTaxamount
        repoTaxAmt.ReadOnly = True
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTaxAmt.Width = 27
        gv1.MasterTemplate.Columns.Add(repoTaxAmt)

        Dim repoTPT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTPT.AllowSort = False
        repoTPT.HeaderText = "TPT"
        repoTPT.Name = colTPT
        repoTPT.ReadOnly = True
        repoTPT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTPT.Width = 30
        gv1.MasterTemplate.Columns.Add(repoTPT)

        Dim repoTotalMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalMRP.AllowSort = False
        repoTotalMRP.HeaderText = "Total MRP"
        repoTotalMRP.Name = colTotalMRP
        repoTotalMRP.ReadOnly = True
        repoTotalMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalMRP.Width = 62
        gv1.MasterTemplate.Columns.Add(repoTotalMRP)

        Dim repoTotalBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalBasicAmt.AllowSort = False
        repoTotalBasicAmt.HeaderText = "Total Basic Amount"
        repoTotalBasicAmt.Name = colTotalBasicAmount
        repoTotalBasicAmt.ReadOnly = True
        repoTotalBasicAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalBasicAmt.Width = 106
        gv1.MasterTemplate.Columns.Add(repoTotalBasicAmt)


        Dim repoTotalDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalDiscount.AllowSort = False
        repoTotalDiscount.HeaderText = "Total Discount"
        repoTotalDiscount.Name = colTotalDiscountAmount
        repoTotalDiscount.ReadOnly = True
        repoTotalDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalDiscount.Width = 81
        gv1.MasterTemplate.Columns.Add(repoTotalDiscount)

        Dim repoTotalCustDiscount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalCustDiscount.HeaderText = "Total Cust Dis"
        repoTotalCustDiscount.Name = colTotalCustDiscount
        repoTotalCustDiscount.ReadOnly = True
        repoTotalCustDiscount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalCustDiscount.Width = 79
        gv1.MasterTemplate.Columns.Add(repoTotalCustDiscount)

        Dim repoTotalNetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalNetAmt.AllowSort = False
        repoTotalNetAmt.HeaderText = "Total Net Amount"
        repoTotalNetAmt.Name = colTotalNetAmount
        repoTotalNetAmt.ReadOnly = True
        repoTotalNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalNetAmt.Width = 96
        gv1.MasterTemplate.Columns.Add(repoTotalNetAmt)

        Dim repoTotalTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalTaxAmt.AllowSort = False
        repoTotalTaxAmt.HeaderText = "Total Tax"
        repoTotalTaxAmt.Name = colTotalTaxAmount
        repoTotalTaxAmt.ReadOnly = True
        repoTotalTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalTaxAmt.Width = 55
        gv1.MasterTemplate.Columns.Add(repoTotalTaxAmt)


        Dim repoTotalTPT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalTPT.AllowSort = False
        repoTotalTPT.HeaderText = "Total TPT"
        repoTotalTPT.Name = colTotalTPT
        repoTotalTPT.ReadOnly = True
        repoTotalTPT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalTPT.Width = 58
        gv1.MasterTemplate.Columns.Add(repoTotalTPT)


        Dim repoTotalItemAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalItemAmt.AllowSort = False
        repoTotalItemAmt.HeaderText = "Total Item Amount"
        repoTotalItemAmt.Name = colTotalItemAmount
        repoTotalItemAmt.ReadOnly = True
        repoTotalItemAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotalItemAmt.Width = 101
        gv1.MasterTemplate.Columns.Add(repoTotalItemAmt)

        Dim repoPromoSchemeApp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPromoSchemeApp.AllowSort = False
        repoPromoSchemeApp.HeaderText = "Promo Scheme App."
        repoPromoSchemeApp.Name = colPromoSchemeApplicable
        repoPromoSchemeApp.ReadOnly = True
        repoPromoSchemeApp.Width = 113
        gv1.MasterTemplate.Columns.Add(repoPromoSchemeApp)

        Dim repoCashSchemApp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCashSchemApp.AllowSort = False
        repoCashSchemApp.HeaderText = "Cash Scheme App."
        repoCashSchemApp.Name = colSchemeDiscountApplicable
        repoCashSchemApp.ReadOnly = True
        repoCashSchemApp.Width = 106
        gv1.MasterTemplate.Columns.Add(repoCashSchemApp)

        Dim repoQtySchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQtySchemeCode.AllowSort = False
        repoQtySchemeCode.HeaderText = "Qty. Scheme Code"
        repoQtySchemeCode.Name = colSchemeCodeItem
        repoQtySchemeCode.ReadOnly = True
        repoQtySchemeCode.Width = 104
        gv1.MasterTemplate.Columns.Add(repoQtySchemeCode)

        Dim repoPromoSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPromoSchemeCode.AllowSort = False
        repoPromoSchemeCode.HeaderText = "Promo Code"
        repoPromoSchemeCode.Name = colPromoSchemeCode
        repoPromoSchemeCode.ReadOnly = True
        repoPromoSchemeCode.Width = 72
        gv1.MasterTemplate.Columns.Add(repoPromoSchemeCode)

        Dim repoSchemeCodeDiscount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSchemeCodeDiscount.AllowSort = False
        repoSchemeCodeDiscount.HeaderText = "Cash Scheme Code"
        repoSchemeCodeDiscount.Name = colSchemeCodeDiscount
        repoSchemeCodeDiscount.ReadOnly = True
        repoSchemeCodeDiscount.Width = 110
        gv1.MasterTemplate.Columns.Add(repoSchemeCodeDiscount)

        Dim repoQtySchemeItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQtySchemeItem.AllowSort = False
        repoQtySchemeItem.HeaderText = "Qty Scheme Item"
        repoQtySchemeItem.Name = colSchemeItem
        repoQtySchemeItem.ReadOnly = True
        repoQtySchemeItem.Width = 96
        gv1.MasterTemplate.Columns.Add(repoQtySchemeItem)

        Dim repoPromoSchemeItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPromoSchemeItem.AllowSort = False
        repoPromoSchemeItem.HeaderText = "Promo Item"
        repoPromoSchemeItem.Name = colPromoSchemeItem
        repoPromoSchemeItem.ReadOnly = True
        repoPromoSchemeItem.Width = 67
        gv1.MasterTemplate.Columns.Add(repoPromoSchemeItem)

        Dim repoFromSchemeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromSchemeCode.HeaderText = "Scheme Code"
        repoFromSchemeCode.Name = colFromSchemeCode
        repoFromSchemeCode.Width = 80
        repoFromSchemeCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFromSchemeCode)




        Dim repoEmptyValueShell As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEmptyValueShell.HeaderText = "Empty Value Shell"
        repoEmptyValueShell.IsVisible = False
        repoEmptyValueShell.Name = colEmptyValueShell
        repoEmptyValueShell.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEmptyValueShell)


        Dim repoEmptyValueBottle As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEmptyValueBottle.HeaderText = "Empty Value Bottle"
        repoEmptyValueBottle.IsVisible = False
        repoEmptyValueBottle.Name = colEmptyValueBottle
        repoEmptyValueBottle.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEmptyValueBottle)

        Dim repoTransferBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTransferBasicAmt.HeaderText = "Tansfer Basic Amount"
        repoTransferBasicAmt.IsVisible = False
        repoTransferBasicAmt.Name = colTransferBasicAmount
        repoTransferBasicAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTransferBasicAmt)


        Dim repoTax1Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax1Rate.HeaderText = "Tax1 Rate"
        repoTax1Rate.IsVisible = False
        repoTax1Rate.Name = colTax1Rate
        repoTax1Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax1Rate)

        Dim repoTax2Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax2Rate.HeaderText = "Tax2 Rate"
        repoTax2Rate.IsVisible = False
        repoTax2Rate.Name = colTax2Rate
        repoTax2Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax2Rate)

        Dim repoTax3Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax3Rate.HeaderText = "Tax3 Rate"
        repoTax3Rate.IsVisible = False
        repoTax3Rate.Name = colTax3Rate
        repoTax3Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax3Rate)

        Dim repoTax4Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax4Rate.HeaderText = "Tax4 Rate"
        repoTax4Rate.IsVisible = False
        repoTax4Rate.Name = colTax4Rate
        repoTax4Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax4Rate)

        Dim repoTax5Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax5Rate.HeaderText = "Tax5 Rate"
        repoTax5Rate.IsVisible = False
        repoTax5Rate.Name = colTax5Rate
        repoTax5Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax5Rate)

        Dim repoTax6Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax6Rate.HeaderText = "Tax6 Rate"
        repoTax6Rate.IsVisible = False
        repoTax6Rate.Name = colTax6Rate
        repoTax6Rate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax6Rate)

        Dim repoTax1Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax1Assess.HeaderText = "Tax1 Assess"
        repoTax1Assess.IsVisible = False
        repoTax1Assess.Name = colAssess1
        repoTax1Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax1Assess)

        Dim repoTax2Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax2Assess.HeaderText = "Tax2 Assess"
        repoTax2Assess.IsVisible = False
        repoTax2Assess.Name = colAssess2
        repoTax2Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax2Assess)

        Dim repoTax3Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax3Assess.HeaderText = "Tax3 Assess"
        repoTax3Assess.IsVisible = False
        repoTax3Assess.Name = colAssess3
        repoTax3Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax3Assess)

        Dim repoTax4Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax4Assess.HeaderText = "Tax4 Assess"
        repoTax4Assess.IsVisible = False
        repoTax4Assess.Name = colAssess4
        repoTax4Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax4Assess)

        Dim repoTax5Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax5Assess.HeaderText = "Tax5 Assess"
        repoTax5Assess.IsVisible = False
        repoTax5Assess.Name = colAssess5
        repoTax5Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax5Assess)

        Dim repoTax6Assess As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax6Assess.HeaderText = "Tax6 Assess"
        repoTax6Assess.IsVisible = False
        repoTax6Assess.Name = colAssess6
        repoTax6Assess.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax6Assess)

        Dim repotax1Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax1Amt.ReadOnly = True
        repotax1Amt.HeaderText = "Tax1 Amt"
        repotax1Amt.IsVisible = False
        repotax1Amt.Name = colTax1Amt
        repotax1Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax1Amt)

        Dim repotax2Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax2Amt.ReadOnly = True
        repotax2Amt.HeaderText = "Tax2 Amt"
        repotax2Amt.IsVisible = False
        repotax2Amt.Name = colTax2Amt
        repotax2Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax2Amt)

        Dim repotax3Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax3Amt.ReadOnly = True
        repotax3Amt.HeaderText = "Tax3 Amt"
        repotax3Amt.IsVisible = False
        repotax3Amt.Name = colTax3Amt
        repotax3Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax3Amt)

        Dim repotax4Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax4Amt.ReadOnly = True
        repotax4Amt.HeaderText = "Tax4 Amt"
        repotax4Amt.IsVisible = False
        repotax4Amt.Name = colTax4Amt
        repotax4Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax4Amt)

        Dim repotax5Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax5Amt.ReadOnly = True
        repotax5Amt.HeaderText = "Tax5 Amt"
        repotax5Amt.IsVisible = False
        repotax5Amt.Name = colTax5Amt
        repotax5Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax5Amt)

        Dim repotax6Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repotax6Amt.ReadOnly = True
        repotax6Amt.HeaderText = "Tax6 Amt"
        repotax6Amt.IsVisible = False
        repotax6Amt.Name = colTax6Amt
        repotax6Amt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repotax6Amt)

        Dim repoCheckValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCheckValue.ReadOnly = True
        repoCheckValue.HeaderText = "Check Value"
        repoCheckValue.IsVisible = False
        repoCheckValue.Name = colCheckvalue
        gv1.MasterTemplate.Columns.Add(repoCheckValue)


        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colBatchNumber
        repoBatchNo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoBatchNo.IsVisible = False
        repoBatchNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBatchNo)

        Dim repoDiscCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDiscCode.FormatString = ""
        repoDiscCode.HeaderText = "Discount Code"
        repoDiscCode.Name = colDiscountCode
        repoDiscCode.HeaderImage = Global.XpertERPSalesAndDistribution.My.Resources.Resources.search4
        repoDiscCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDiscCode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoDiscCode)

        Dim repoCustDisNoTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCustDisNoTax.FormatString = ""
        repoCustDisNoTax.HeaderText = "Cust Discount Without Tax"
        repoCustDisNoTax.Name = ColCustDisNoTax
        repoCustDisNoTax.Width = 80
        repoCustDisNoTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCustDisNoTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustDisNoTax)

        Dim repoTargetDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTargetDisAmt.FormatString = ""
        repoTargetDisAmt.HeaderText = "Target Discount Amount"
        repoTargetDisAmt.Name = ColTargetDisAmt
        repoTargetDisAmt.Width = 80
        repoTargetDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTargetDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTargetDisAmt)


        repoActualBalQty.FormatString = ""
        repoActualBalQty.HeaderText = "Actual Balance"
        repoActualBalQty.Name = ColActualBalQty
        repoActualBalQty.Width = 80
        repoActualBalQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoActualBalQty.ReadOnly = True
        repoActualBalQty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoActualBalQty)

        Dim repoPriceToShow As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPriceToShow.FormatString = ""
        repoPriceToShow.HeaderText = "Price"
        repoPriceToShow.Name = ColPriceToShow
        repoPriceToShow.Width = 80
        repoPriceToShow.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPriceToShow.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPriceToShow)

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

        Dim repoAbatementRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAbatementRate.FormatString = ""
        repoAbatementRate.HeaderText = "Abatement Rate"
        repoAbatementRate.Name = colAbatementRate
        repoAbatementRate.Width = 80
        repoAbatementRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAbatementRate.ReadOnly = True
        repoAbatementRate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAbatementRate)

        Dim repoTonnagePerUnit As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTonnagePerUnit.FormatString = ""
        repoTonnagePerUnit.HeaderText = "Tonnage Per Unit"
        repoTonnagePerUnit.Name = colTonnagePerUnit
        repoTonnagePerUnit.Width = 80
        repoTonnagePerUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTonnagePerUnit.ReadOnly = True
        repoTonnagePerUnit.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTonnagePerUnit)

        Dim repoTonnage As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTonnage.FormatString = ""
        repoTonnage.HeaderText = "Tonnage"
        repoTonnage.Name = colTonnage
        repoTonnage.Width = 80
        repoTonnage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTonnage.ReadOnly = True
        repoTonnage.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTonnage)


        gv1.AllowAddNewRow = False
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

        AddHandler fndTaxGroup.TextChanged, AddressOf fndTaxGroup_TextChanged
    End Sub

    'Private Sub fndTaxGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndTaxGroup.ConnectionString = connectSql.SqlCon()
    '    fndTaxGroup.Query = clsERPFuncationality.UserAvailableTaxGroup + " AND M.Tax_Group_Type='S'"
    '    fndTaxGroup.ValueToSelect = "Code"
    '    fndTaxGroup.ValueToSelect1 = "Description"
    '    fndTaxGroup.Caption = "Tax Groups"
    'End Sub

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

    Private Sub fndTaxGroup_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SetTaxGroup()
    End Sub

    Private Sub SetTaxGroup()
        sql = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code " & _
           " WHERE G.Tax_Group_Code = '" + fndTaxGroup.Value + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
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

                Dim tpt As String = ""
                Dim tptcheck As String = "N"
                Dim tptdr As DataTable = clsDBFuncationality.GetDataTable("SELECT  Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10   FROM TSPL_ITEM_PRICE_MASTER Where Price_Code='" + txtPriceCode.Text + "' and Item_Code = '" + Convert.ToString(grow.Cells(ColICode).Value) + "' AND Item_Basic_Net ='" + Convert.ToString(grow.Cells(colMRP).Value * convertFact) + "' AND Tax_group = '" + fndTaxGroup.Value + "' and UOM = 'FC'")
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

    Private Sub gvLoadOut1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(ColICode) OrElse e.Column Is gv1.Columns(colSchemeApplicable) OrElse e.Column Is gv1.Columns(colSchemeDiscountApplicable) OrElse e.Column Is gv1.Columns(colitemNetAmount) OrElse e.Column Is gv1.Columns(colShippedQty) OrElse e.Column Is gv1.Columns(colMainItem) OrElse e.Column Is gv1.Columns(colDiscountCode) OrElse e.Column Is gv1.Columns(colTotalTPT) Then

                        If e.Column Is gv1.Columns(ColICode) AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(ColICode).Value) > 0 Then
                            Dim baseQry As String = GetShipmentViewQty()
                            Dim qry As String = "SELECT Item_Code,Item_Desc,Start_Date,Item_Basic_Net as [MRP], UOM FROM (" + baseQry + ")xxx  Where Show='N' AND UOM in ('FB','FC') AND  Price_Code='" + txtPriceCode.Text + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.Value + "' "

                            qry += " AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0 and MRP=xxx.Item_Basic_Net*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=xxx.Item_Code and UOM_Code=xxx.UOM))"



                            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ShipmentItemSelect", qry, "Item_Code", clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value))
                            If dr IsNot Nothing Then
                                Dim itemcode As String = Convert.ToString(dr("Item_Code"))
                                Dim startdate As String = CDate(dr("start_date")).ToString("dd/MM/yyyy")
                                Dim mrp As Decimal = clsCommon.myCdbl(dr("MRP"))
                                Dim strUOM As String = clsCommon.myCstr(dr("UOM"))

                                currentmanualscheme(itemcode, startdate, mrp, strUOM)
                                gv1.CurrentRow.Cells(colTonnagePerUnit).Value = clsItemMaster.GetWeitht(itemcode, strUOM, Nothing)
                                gv1.CurrentRow.Cells(colTonnage).Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colTonnagePerUnit).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colShippedQty).Value), 2, MidpointRounding.ToEven)
                                isCellValueChangedOpen = False
                                Exit Sub
                            Else
                                gv1.CurrentRow.Cells(colTonnagePerUnit).Value = 0
                                gv1.CurrentRow.Cells(colTonnage).Value = 0
                                gv1.CurrentRow.Cells(ColICode).Value = String.Empty
                                isCellValueChangedOpen = False
                                Exit Sub
                            End If

                        End If
                        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
                        If grow.Cells(colSchemeItem).Value = "No" And grow.Cells(colSampleItem).Value = "No" And grow.Cells(colPromoSchemeItem).Value = "No" Then
                            grow.Cells("shippedQty").ReadOnly = False
                            If e.Column Is gv1.Columns(colSchemeApplicable) Then
                                findQtyandPromoSchemeCode(grow, "Q")
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
                                    If Not checkItemonLocation(grow.Cells(ColICode).Value, grow.Cells(colShippedQty).Value, txtLocation.Value, grow.Cells(colUnitCode).Value, clsCommon.myCdbl(grow.Cells(colMRP).Value), False, grow.Cells(colBatchNumber).Value) Then
                                        grow.Cells(colShippedQty).Value = 0
                                    End If

                                    loadoutCellValueChange(grow)
                                    calculateItemTaxAgainstItemRate(grow)
                                    CalculateTaxByLocation(grow)
                                End If
                            End If
                        Else
                            grow.Cells(colShippedQty).ReadOnly = False

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
                                gr.Cells(colTTaxRate).Value = Math.Round(taxAmt * 100 / assess, 0)
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

                        End If
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colFromSchemeCode).Value) > 0 AndAlso gv1.CurrentRow.Cells(colFromSchemeCode).Value.ToString().Contains("MS") Then
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

                        txtShipmentAmt.Text = 0
                        For Each gr As GridViewRowInfo In gv1.Rows
                            If Not clsCommon.myCdbl(gr.Cells(colShippedQty).Value) = 0 Then
                                If gr.Cells(colSchemeItem).Value = "No" And gr.Cells(colPromoSchemeItem).Value = "No" Then
                                    txtShipmentAmt.Text = clsCommon.myCdbl(txtShipmentAmt.Text) + gr.Cells(colTotalItemAmount).Value
                                End If
                            End If
                        Next

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

                                Dim baseQry As String = GetShipmentViewQty()
                                qry += "SELECT Item_Code,Item_Desc,Start_Date,Item_Basic_Net as [MRP], UOM FROM (" + baseQry + ")xxx Where Show='N' AND UOM='FC' AND  Price_Code='" + txtPriceCode.Text + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.Value + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0 and MRP=xxx.Item_Basic_Net*(select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code=xxx.Item_Code and UOM_Code=xxx.UOM)) "

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
                            gv1.CurrentRow.Cells(colDiscountCode).Value = clsCommon.ShowSelectForm("SODisCodFND", sql, "Code", whrclas, clsCommon.myCstr(gv1.CurrentRow.Cells(colDiscountCode).Value))
                            gv1.CurrentRow.Cells(colMainItem).Value = ""
                        End If
                    End If
                    If e.Column Is gv1.Columns(colShippedQty) Then
                        gv1.CurrentRow.Cells(colTonnage).Value = Math.Round(clsCommon.myCdbl(gv1.CurrentRow.Cells(colTonnagePerUnit).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colShippedQty).Value), 2, MidpointRounding.AwayFromZero)
                    End If
                    UpdateAllTotal()
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateAllTotal()
        Dim dblTotalTonnage As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            dblTotalTonnage += clsCommon.myCdbl(gv1.Rows(ii).Cells(colTonnage).Value)
        Next
        lblTotalTonnage.Text = clsCommon.myFormat(dblTotalTonnage)
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
         If e.Column.Name = "schemeApplicable" And grow.Cells(colPromoSchemeItem).Value = "No" And grow.Cells(colSchemeItem).Value = "No" And grow.Cells(colSampleItem).Value = "No" Then
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
                frm.taxGroupCode = fndTaxGroup.Value

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

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    Private Function funvalidatevehicle(ByVal trans As SqlTransaction) As Boolean

        Dim count As Decimal = 0
        Dim segno As String = String.Empty
        Dim strvehiclenum As String = txtVehicleCode.Value
        sql = "select Description from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(txtVehicleCode.Value) + "' or Description = '" + Convert.ToString(txtVehicleCode.Value) + "'"
        If Not String.IsNullOrEmpty(clsDBFuncationality.getSingleValue(sql, trans)) Then
            Return True
        Else
            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            Dim strVehicalNo As String = txtVehicleCode.Value
            strmessage += "Do you want to continue "
            If common.clsCommon.MyMessageBoxShow(strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                count = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_GL_SEGMENT_CODE where Segment_name = 'Vehicles'", trans)

                txtVehicleCode.Value = Convert.ToString(count + 1) + ""
                sql = "select seg_no from tspl_gl_segment where seg_name='Vehicles'"
                segno = CStr(clsDBFuncationality.getSingleValue(sql, trans))
                clsDBFuncationality.SaveAStorePorcedure(trans, "sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", segno), New SqlParameter("@segmentname", "Vehicles"), New SqlParameter("@segmentcode", txtVehicleCode.Value), New SqlParameter("@desc", strVehicalNo), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUser), New SqlParameter("@createddate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")), New SqlParameter("@modifiedby", objCommonVar.CurrentUser), New SqlParameter("@modifieddate", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", txtVehicleCode.Value), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", ""), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUser), New SqlParameter("@Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")), New SqlParameter("@Modified_By", objCommonVar.CurrentUser), New SqlParameter("@Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
                lblVhicleNo.Text = strvehiclenum
                Return True
            Else
                txtVehicleCode.Value = String.Empty
                Return False
                trans.Rollback()
            End If
        End If

        Return True
    End Function

    Private Function abatement(ByVal grow As GridViewRowInfo, ByVal trans As SqlTransaction) As Decimal
        Dim abat As Decimal
        sql = "Select Abatement_Rate from TSPL_ITEM_PRICE_MASTER WHERE Item_Code='" + clsCommon.myCstr(grow.Cells(ColICode).Value) + "' AND Item_Basic_Net='" + clsCommon.myCstr(grow.Cells(colMRP).Value) + "' AND Start_Date='" + clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells(colPriceDateColumn).Value), "dd/MMM/yyyy") + "'"
        abat = clsDBFuncationality.getSingleValue(sql, trans)
        Return abat
    End Function

    Private Function abatement1(ByVal grow As GridViewRowInfo, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim abat As Decimal
        sql = "Select Abatement from TSPL_SALES_ORDER_DETAIL WHERE SNo='" + grow.Cells("lineNo").Value + "' AND Order_No='" + txtDocNo.Value + "'"
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
                        common.clsCommon.MyMessageBoxShow("This quantity is not available at the location.")
                        Return False
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
        Return True
    End Function

    Private Sub resetFNDCustomer()
        txtCustomer.Value = String.Empty
        txtCustomerName.Text = String.Empty
        txtRouteNo.Value = String.Empty
        lblRouteDesc.Text = String.Empty
        txtPriceCode.Text = String.Empty
        fndTaxGroup.Value = String.Empty
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
        LoadBlankGrid()
        txtshellqty.Value = 0
        lblfc.Text = 0
        lblfb.Text = 0
        cboPriceDate.Text = clsCommon.GETSERVERDATE()
        txtCustomer.Enabled = True

        txtDate.Enabled = True
        rbFB.Enabled = True
        rbFC.Enabled = True


        rbAll.Enabled = True
        rbFB.IsChecked = False
        txtEmployeeCode.Value = ""
        rbFC.IsChecked = False
        rbAll.IsChecked = False

        txtCustomer.Enabled = True
        txtLocation.Enabled = True

        txtLocation.Enabled = True


        Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")

        txtDate.Value = clsCommon.GETSERVERDATE()
        txtExpectedShipDate.Value = clsCommon.GETSERVERDATE()


        btnAdd.Text = "Save"
        btnAdd.Enabled = True

        btnDelete.Enabled = False
        btnPost.Enabled = False

        txtRef.Text = String.Empty
        txtCustomerPONO.Text = String.Empty

        txtRemarks.Text = String.Empty
        txtShipTo.Value = ""
        lblShipTo.Text = ""


        txtDesc.Text = String.Empty
        txtCustDisc.Text = 0.0

        txtRouteNo.Value = String.Empty
        txtShipmentAmt.Text = 0.0
        txtShipmentTotal.Text = 0.0
        ''txtStatus.Text = "Open"
        UsLock1.Status = ERPTransactionStatus.Pending


        txtVehicleCode.Value = String.Empty
        txtPaymentTerm.Value = String.Empty
        txtLocation.Value = String.Empty
        cboModeOfTransport.Text = "Select"

        txtDocNo.Value = ""
        cboPriceDate.DropDownStyle = RadDropDownStyle.DropDownList

        cboModeOfTransport.DropDownStyle = RadDropDownStyle.DropDownList
        pvLoadOut.SelectedPage.Name = "pageLoadOut"
        gv1.AllowAddNewRow = False
        txtCustomer.Value = ""

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



        lblEmpName.Text = ""
        lblVhicleNo.Text = ""
        lblSalesMan1.Text = ""
        txtPaymentDate.Checked = False
        txtPaymentDate.Value = txtExpectedShipDate.Value
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
                    gr.Cells("tax" + (grow.Index + 1).ToString() + "Amt").Value = Math.Round((clsCommon.myCdbl(gr.Cells("assess" + (grow.Index + 1).ToString()).Value) * clsCommon.myCdbl(gr.Cells("tax" + (grow.Index + 1).ToString() + "Rate").Value) / 100), 2).ToString()
                    ttlItemtax = ttlItemtax + clsCommon.myCdbl(gr.Cells("tax" + (grow.Index + 1).ToString() + "Amt").Value)
                Next
                gr.Cells(colTaxamount).Value = Math.Round(ttlItemtax, 4, MidpointRounding.ToEven)
            End If
        Else
        End If
    End Sub

    Private Sub findQtyandPromoSchemeCode(ByVal grow As GridViewRowInfo, ByVal schemeType As String)
        Dim dr1 As DataTable
        Dim schemeAppCol As String = "schemeApplicable"
        Dim schemeCodeCol As String = "schemeCodeItem"
        Dim schemeItemCol As String = "schemeItem"
        If schemeType = "P" Then
            schemeAppCol = "promoSchemeApplicable"
            schemeCodeCol = "promoSchemeCode"
            schemeItemCol = "promoSchemeItem"
        End If
        Try

            sql = "SELECT S.Scheme_Code,S.Main_Item_Qty FROM TSPL_SCHEME_MASTER AS S INNER JOIN TSPL_SCHEME_DETAILS AS D ON S.Scheme_Code = D.Scheme_Code " & _
                  " WHERE  (S.Main_Item_Code = '" + grow.Cells(ColICode).Value + "') AND S.Start_Date <='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "MM/dd/yyyy") + "' " & _
                  " AND (S.End_Date >='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "MM/dd/yyyy") + "' OR S.End_Date is NULL ) AND S.Main_Item_Qty <= '" + clsCommon.myCstr(grow.Cells(colShippedQty).Value) + "' " & _
                  " AND S.Main_item_UOM='" + grow.Cells(colUnitCode).Value + "' AND S.MRP='" + clsCommon.myCstr(grow.Cells(colMRP).Value) + "' AND S.Cust_Cate =  (SELECT Cust_Category_Code  FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code = '" + txtCustomer.Value + "')"

            If grow.Cells(schemeAppCol).Value = "No" Then
                For schemeRow As Integer = gv1.Rows.Count - 1 To 0 Step -1
                    If Not grow.Cells(schemeCodeCol).Value.ToString().Trim() = String.Empty Then
                        If gv1.Rows(schemeRow).Cells(colFromSchemeCode).Value = grow.Cells(schemeCodeCol).Value Then
                            gv1.Rows.RemoveAt(schemeRow)

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

                dataRowInfo.Cells(ColICode).Value = tdr(0).ToString()
                Dim shpQty As Decimal = tdr(2).ToString()
                dataRowInfo.Cells(ColItemName).Value = tdr(1).ToString()
                If clsCommon.myLen(strStartDate) <= 0 Then
                    Throw New Exception("Scheme can't be Applied")
                Else
                    dataRowInfo.Cells(colPriceDateColumn).Value = strStartDate
                End If

                dataRowInfo.Cells(colPriceCode).Value = txtPriceCode.Text

                dataRowInfo.Cells(colShippedQty).ReadOnly = True

                dataRowInfo.Cells(colBalanceQty).Value = 0
                dataRowInfo.Cells(colUnitCode).Value = tdr(3).ToString()
                Dim emptyshell1 As Decimal = clsDBFuncationality.getSingleValue("select Empty_Value_Shell   from TSPL_ITEM_PRICE_MASTER where Item_Code =  '" + dataRowInfo.Cells(ColICode).Value + "' AND UOM = '" + dataRowInfo.Cells(colUnitCode).Value + "'")
                Dim emptybottle1 As Decimal = clsDBFuncationality.getSingleValue("select Empty_Value_Bottle  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + dataRowInfo.Cells(ColICode).Value + "' AND UOM = '" + dataRowInfo.Cells(colUnitCode).Value + "'")
                Dim conversion As Decimal = clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + dataRowInfo.Cells(ColICode).Value + "' AND UOM_Code  = '" + dataRowInfo.Cells(colUnitCode).Value + "'")


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
                    Dim dr As DataRow = clsTaxMaster.GetExcisableTaxRates(Convert.ToString(dataRowInfo.Cells(ColICode).Value), clsCommon.myCdbl(dataRowInfo.Cells(colMRP).Value), CDate(strStartDate).ToString("yyyy-MM-dd"), clsCommon.myCdbl(dataRowInfo.Cells(colBasicAmount).Value), Convert.ToString(dataRowInfo.Cells(colUnitCode).Value), fndTaxGroup.Value, txtPriceCode.Text)
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
                location = txtLocation.Value
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

        ElseIf txtSalesman.Value = String.Empty Then
            txtSalesman.Focus()
            Throw New Exception("Salesman can not be left blank.")
        ElseIf txtLocation.Value = String.Empty Then
            txtLocation.Focus()
            Throw New Exception("Location can not be left blank.")
            'ElseIf txtVehicleCode.Value = String.Empty Then
            '    txtVehicleCode.Focus()
            '    Throw New Exception("Vehicle can not be left blank.")
        ElseIf txtPaymentTerm.Value = String.Empty Then
            txtPaymentTerm.Focus()
            Throw New Exception("Payment Terms can not be left blank.")
        ElseIf fndTaxGroup.Value = String.Empty Then
            fndTaxGroup.Focus()
            Throw New Exception("Tax Group can not be left blank.")
        ElseIf cboModeOfTransport.Text = "Select" Then
            cboModeOfTransport.Focus()
            Throw New Exception("Please choose a mode of transport from the list.")
        ElseIf cboPriceDate.Text = "Select" Then
            cboModeOfTransport.Focus()
            Throw New Exception("Please choose a price date from the list.")

        ElseIf shipQty = 0 Then
            Throw New Exception("There is no item to ship.")
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


            Dim obj As New clsSalesOrder()

            obj.Order_No = txtDocNo.Value
            obj.Order_Date = txtDate.Value
            obj.Expected_Ship_Date = txtExpectedShipDate.Value
            obj.Cust_Code = txtCustomer.Value
            obj.Cust_Name = txtCustomerName.Text
            obj.Cust_PONo = txtCustomerPONO.Text
            obj.On_Hold = chkOnHold.Checked

            obj.Ref_No = txtRef.Text
            obj.Description = txtDesc.Text
            obj.Remarks = txtRemarks.Text
            obj.Price_Code = txtPriceCode.Text
            obj.Tax_Group = fndTaxGroup.Value
            obj.Location = txtLocation.Value
            obj.Cust_Account = strCustAccount
            obj.Payment_Amount = txtPaymentAmt.Value
            obj.Total_Tonnage = clsCommon.myCdbl(lblTotalTonnage.Text)
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
            obj.Order_Disc_Percent = shipmentDiscPer
            obj.Order_Discount_Amt = shipmentDiscAmt
            obj.Order_Detail_Disc_Amt = detailDiscount
            obj.Order_Detail_Total_Amt = clsCommon.myCdbl(txtNetShipAmt.Text)
            obj.Order_Tax_Amt = shipmentTaxAmt
            obj.Freight_Amt = freightCharges
            obj.Other_Charges = OtherCharges
            obj.Add_Charges = additionalCharges
            obj.Total_Order_Amt = txtShipmentAmt.Text
            obj.Salesman_Code = txtSalesman.Value
            obj.Mode_Of_Transport = cboModeOfTransport.Text
            obj.Vehicle_Code = txtVehicleCode.Value
            obj.Vehicle_No = lblVhicleNo.Text

            obj.Route_No = txtRouteNo.Value
            obj.Route_Desc = lblRouteDesc.Text

            obj.Price_Date = priceDate
            obj.Terms_Code = txtPaymentTerm.Value
            obj.Comments = txtRemarks.Text
            obj.Discount_On = IIf(chkDiscountOnRate.IsChecked, 0, 1)
            obj.Level1_User_code = l1User
            obj.Level2_User_code = l2User
            obj.Level3_User_code = l3User
            obj.Level4_User_code = l4User
            obj.Level5_User_code = l5User



            obj.Total_TPT = txtTotalTPT.Text

            'obj.Level1_User_Commission=
            'obj.Level2_User_Commission=
            'obj.Level3_User_Commission=
            'obj.Level4_User_Commission=
            'obj.Level5_User_Commission=
            obj.Empty_Value = emptyvalue
            'obj.Is_Complete=
            'obj.Shell_Qty=


            'obj.Employee_Code=

            obj.Total_Disc_Percent = dblTotDisPer
            obj.Tax_Recoverable_Amt = dblTaxRecAmt
            obj.Tax_Recoverable_Amt2 = dblTaxRecAmt2
            obj.Tax_Recoverable_Amt3 = dblTaxRecAmt3
            obj.Tot_Customer_Dis_Amt = clsCommon.myCdbl(txtCustDisc.Text)


            'obj.Verify_By=
            obj.Ship_To = txtShipTo.Value
            obj.Ship_To_Desc = lblShipTo.Text

            sql = "select [" + clsCommon.myCstr(Weekday(txtDate.Value)) + "] as IsScheduled from("
            sql += "select Sunday as [1] ,Monday as [2] ,Tuesday as [3],Wednesday as [4],Thursday as [5] ,Friday as [6] ,Saturday as [7]  from TSPL_CUSTOMER_MASTER left outer join TSPL_ROUTE_GROUP_MASTER on TSPL_ROUTE_GROUP_MASTER.Group_Id=TSPL_CUSTOMER_MASTER.Route_Group where TSPL_CUSTOMER_MASTER.Cust_Code='" + txtCustomer.Value + "' )xxx"
            obj.Is_Scheduled = IIf(clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql, trans)), "T") = CompairStringResult.Equal, 1, 0)



            sql = "select Type from TSPL_ROUTE_MASTER where Route_No='" + txtRouteNo.Value + "'"
            obj.Route_Type_Id = clsDBFuncationality.getSingleValue(sql, trans)

            obj.TAX_GROUP_TYPE = "S"
            If txtPaymentDate.Checked Then
                obj.Payment_Date = txtPaymentDate.Value
            End If

            obj.Payment_Date = txtPaymentDate.Value
            obj.Arr = New List(Of clsSalesOrderDetail)
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(grow.Cells(colShippedQty).Value) > 0 Then
                    Dim objtr As New clsSalesOrderDetail()

                    objtr.Item_Code = clsCommon.myCstr(grow.Cells(ColICode).Value)
                    objtr.Item_Desc = clsCommon.myCstr(grow.Cells(ColItemName).Value)
                    objtr.Price_Date = clsCommon.myCDate(grow.Cells(colPriceDateColumn).Value)

                    objtr.Order_Qty = clsCommon.myCdbl(grow.Cells(colShippedQty).Value)
                    objtr.Balance_Qty = clsCommon.myCdbl(grow.Cells(colShippedQty).Value)
                    objtr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)

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

                    objtr.Tonnage_Per_Unit = clsCommon.myCdbl(grow.Cells(colTonnagePerUnit).Value)
                    objtr.Tonnage = clsCommon.myCdbl(grow.Cells(colTonnage).Value)

                    obj.Arr.Add(objtr)
                End If
            Next

            obj.SaveData(obj, isNewEntry, trans)
            txtDocNo.Value = obj.Order_No




            trans.Commit()
            isNewEntry = False
            btnAdd.Text = "Update"
            btnAdd.Enabled = True

            btnDelete.Enabled = True
            btnPost.Enabled = True


        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub totalAmounts()
        Dim otherCharges As Decimal
        Dim additionalCharges As Decimal
        Dim freight As Decimal
        Dim discountAmt As Decimal
        Dim totalTaxAmt As Decimal
        Dim shipmentTotal As Decimal

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
        txtShipmentAmt.Text = Math.Round(shipmentTotal + totalTaxAmt + otherCharges + additionalCharges + freight + totalTransport(), 4)
    End Sub

    Private Sub totalAmounts(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim otherCharges As Decimal
        Dim additionalCharges As Decimal
        Dim freight As Decimal
        Dim discountAmt As Decimal
        Dim totalTaxAmt As Decimal
        Dim shipmentTotal As Decimal

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
                sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle, TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate , TAX1_Amt ,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt,Abatement_Rate,Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10,PriceToShow  FROM (" + baseQty + ")xxx Where Show='N' AND UOM='FB' and  Price_Code='" + txtPriceCode.Text.Trim() + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.Value + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0) Order By Sku_Seq,Start_Date,UOM desc"
            ElseIf items = "FC" Or rbFC.ToggleState = ToggleState.On Then
                sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle, TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate , TAX1_Amt ,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt,Abatement_Rate,Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10,PriceToShow  FROM (" + baseQty + ")xxx Where Show='N' AND UOM='FC' AND  Price_Code='" + txtPriceCode.Text.Trim() + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.Value + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0)  Order By Sku_Seq,Start_Date,UOM desc"
            ElseIf items = "All" Then
                sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle, TAX1_Rate,TAX2_Rate,TAX3_Rate,TAX4_Rate,TAX5_Rate,TAX6_Rate,TAX7_Rate,TAX8_Rate,TAX9_Rate,TAX10_Rate , TAX1_Amt ,TAX2_Amt,TAX3_Amt,TAX4_Amt,TAX5_Amt,TAX6_Amt,TAX7_Amt,TAX8_Amt,TAX9_Amt,TAX10_Amt,Abatement_Rate,Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10,PriceToShow  FROM (" + baseQty + ")xxx Where  UOM in ('FC','FB') AND Price_Code='" + txtPriceCode.Text.Trim() + "' AND  ITEM_TYPE = 'F' and Tax_group = '" + fndTaxGroup.Value + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0) Order By Sku_Seq,Start_Date,UOM desc"
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

                        datarowinfo.Cells(ColICode).Value = tdr(0).ToString()
                        'datarowinfo.Cells(ColICode).ReadOnly = True
                        datarowinfo.Cells(ColItemName).Value = tdr(1).ToString()
                        datarowinfo.Cells(colPriceDateColumn).Value = tdr(2).ToString()
                        datarowinfo.Cells(colPriceDateColumn).ReadOnly = True

                        datarowinfo.Cells(colShippedQty).Value = 0
                        datarowinfo.Cells(colUnitCode).Value = tdr(3).ToString()

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
                        datarowinfo.Cells(colTonnagePerUnit).Value = clsItemMaster.GetWeitht(clsCommon.myCstr(tdr("Item_Code")), clsCommon.myCstr(tdr("UOM")), Nothing)
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


                gv1.AllowAddNewRow = False
                AddHandler txtCustDisc.TextChanged, AddressOf totalAmounts

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
        Dim dtUOM As DataTable = clsDBFuncationality.GetDataTable("select distinct Unit_code from TSPL_SALES_ORDER_DETAIL where TSPL_SALES_ORDER_DETAIL.Order_No= '" + txtDocNo.Value + "'")
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
        sql += " and not Exists(select 1 from TSPL_SALES_ORDER_DETAIL where TSPL_SALES_ORDER_DETAIL.Order_No= '" + txtDocNo.Value + "' and TSPL_SALES_ORDER_DETAIL.Item_Code=xxx.Item_Code and TSPL_SALES_ORDER_DETAIL.MRP_Amt=xxx.Item_Basic_Net) "
        sql += " and Tax_group = '" + taxgroup + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0) Order By Sku_Seq"

        Dim dr5 As DataTable = clsDBFuncationality.GetDataTable(sql)
        Dim i As Integer = 0
        If dr5 IsNot Nothing AndAlso dr5.Rows.Count > 0 Then
            For Each tdr As DataRow In dr5.Rows
                sql = "SELECT isnull(SUM(isnull(Item_Qty,0)),0) FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + tdr(0).ToString() + "' AND Location_Code='" + txtLocation.Value + "' and MRP='" + tdr(5).ToString() + "'"
                Dim dblBalanceQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql))
                If dblBalanceQty > 0 Then
                    Dim datarowinfo As GridViewRowInfo = gv1.Rows.AddNew()

                    datarowinfo.Cells(ColICode).Value = tdr(0).ToString()

                    datarowinfo.Cells(ColItemName).Value = tdr(1).ToString()
                    datarowinfo.Cells(colPriceDateColumn).Value = tdr(2).ToString()

                    datarowinfo.Cells(colShippedQty).Value = 0
                    datarowinfo.Cells(colUnitCode).Value = tdr(3).ToString()

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

                    Dim convFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), Nothing)
                    If convFac = 1 Then
                        datarowinfo.Cells(ColActualBalQty).Value = clsItemLocationDetails.getBalanceWithUnapprove(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), txtLocation.Value, clsCommon.myCstr(datarowinfo.Cells(colMRP).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), txtDocNo.Value, txtDate.Value)
                    End If
                    datarowinfo.Cells(colTonnagePerUnit).Value = clsItemMaster.GetWeitht(clsCommon.myCstr(datarowinfo.Cells(ColICode).Value), clsCommon.myCstr(datarowinfo.Cells(colUnitCode).Value), Nothing)
                End If
                gv1.Refresh()
            Next
            gv1.AllowAddNewRow = False
            AddHandler txtCustDisc.TextChanged, AddressOf totalAmounts

        End If
        gv1.AllowAddNewRow = True
    End Sub

    Private Sub currentmanualscheme(ByVal itemcode As String, ByVal startdate As String, ByVal mrp As Decimal, ByVal strUOM As String)
        Dim tptcheck As String = "N"
        Dim baseQry As String = GetShipmentViewQty()
        sql = "SELECT Item_Code,Item_Desc,Start_Date,UOM,Price_Code,Item_Basic_Net,Item_Basic_Price,Empty_Value_Shell,Empty_Value_Bottle,UOM,Abatement_Rate FROM (" + baseQry + ")xxx Where Show='N' AND ITEM_CODE = '" + itemcode + "' AND UOM='" + strUOM + "' and  Price_Code='" + txtPriceCode.Text.Trim() + "' and Start_Date = '" + startdate + "' and Item_Basic_Net = '" + CStr(mrp) + "' AND ITEM_TYPE = 'F'  and Tax_group = '" + fndTaxGroup.Value + "' AND Item_Code IN (SELECT Item_Code FROM TSPL_ITEM_LOCATION_DETAILS WHERE Location_Code = '" + txtLocation.Value + "' AND Item_Qty <> 0)  Order By Sku_Seq"
        Dim dr5 As DataTable = clsDBFuncationality.GetDataTable(sql)
        If dr5 IsNot Nothing AndAlso dr5.Rows.Count > 0 Then
            For Each tdr As DataRow In dr5.Rows

                gv1.CurrentRow.Cells(ColICode).Value = itemcode
                gv1.CurrentRow.Cells(ColItemName).Value = Convert.ToString(tdr(1))
                gv1.CurrentRow.Cells(colPriceDateColumn).Value = startdate
                gv1.CurrentRow.Cells(colPriceDateColumn).ReadOnly = True

                gv1.CurrentRow.Cells(colShippedQty).Value = 1
                gv1.CurrentRow.Cells(colUnitCode).Value = clsCommon.myCstr(tdr("UOM"))  ''"FB"
                gv1.CurrentRow.Cells("unitCode").ReadOnly = True

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
                    Dim dr As DataRow = clsTaxMaster.GetExcisableTaxRates(itemcode, clsCommon.myCdbl(mrp), clsCommon.GetPrintDate(startdate, "yyyy-MM-dd"), clsCommon.myCdbl(gv1.CurrentRow.Cells(colBasicAmount).Value), Convert.ToString(gv1.CurrentRow.Cells(colUnitCode).Value), fndTaxGroup.Value, txtPriceCode.Text)
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

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As common.NavigatorType)
        Dim dtStartTime As DateTime = DateTime.Now
        Dim span As TimeSpan
        Dim dtEndTime As DateTime
        Dim strMsg As String = ""
        isInsideLoadData = True
        LoadBlankGrid()

        gvTax.DataSource = Nothing
        gvTax.Rows.Clear()

        Dim shipmentamt As Decimal = 0
        Dim taxamt1 As Decimal = 0
        Dim pricecode As String = ""
        Dim TAXGROUP As String = ""
        Dim basicamt1 As Decimal = 0

        isNewEntry = False
        Try
            Dim strLocation As String = ""
            Dim obj As clsSalesOrder = clsSalesOrder.GetData(strCode, NavType, Nothing)

            dtEndTime = DateTime.Now
            span = dtEndTime.Subtract(dtStartTime)
            strMsg += "query Execute:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
            dtStartTime = DateTime.Now

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Order_No) > 0 Then

                txtDocNo.Value = obj.Order_No
                lblTotalTonnage.Text = clsCommon.myFormat(obj.Total_Tonnage)

                gvTax.DataSource = Nothing
                gvTax.Rows.Clear()
                gvTax.AllowAddNewRow = True
                txtPriceCode.Text = obj.Price_Code

                RemoveHandler fndTaxGroup.TextChanged, AddressOf fndTaxGroup_TextChanged



                RemoveHandler txtCustDisc.TextChanged, AddressOf totalAmounts
                RemoveHandler txtDiscPer.TextChanged, AddressOf MyTextBox1_TextChanged


                taxamt1 = obj.Order_Tax_Amt
                shipmentamt = obj.Total_Order_Amt
                'txtDate.Enabled = True
                btnPost.Visible = True AndAlso MyBase.isPostFlag



                txtshellqty.Value = obj.Shell_Qty
                txtDate.Value = obj.Order_Date


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

                UsLock1.Status = obj.Is_Post




                If obj.On_Hold Then
                    chkOnHold.Checked = True
                Else
                    chkOnHold.Checked = False
                End If


                txtPaymentAmt.Value = obj.Payment_Amount
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

                If obj.Payment_Date IsNot Nothing Then
                    txtPaymentDate.Checked = True
                    txtPaymentDate.Value = obj.Payment_Date
                Else
                    txtPaymentDate.Checked = False
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

                btnAdd.Text = "Update"

                txtCustomer.Enabled = False
                'txtDate.Enabled = False
                txtLocation.Enabled = False

                If obj.Is_Post = ERPTransactionStatus.Approved Then
                    btnAdd.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                Else
                    btnAdd.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True

                End If
                Dim MRP As Decimal = totalMRP()
                Dim basicAmt As Decimal = totalBasicAmt()
                Dim netAmt As Decimal = totalNetAmount()


                dtEndTime = DateTime.Now
                span = dtEndTime.Subtract(dtStartTime)
                strMsg += "Data Load From Transaction:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
                dtStartTime = DateTime.Now

                funfilldetail(obj)

                If obj.Is_Post = ERPTransactionStatus.Pending Then
                    priceDateSelection1(TAXGROUP)
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

                RemoveHandler fndTaxGroup.TextChanged, AddressOf fndTaxGroup_TextChanged
                fndTaxGroup.Value = ""
                fndTaxGroup.Value = TAXGROUP
                lblTaxDesc.Text = clsTaxGroupMaster.GetNameOfSaleType(fndTaxGroup.Value, Nothing)
                sql = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code " & _
           " WHERE G.Tax_Group_Code = '" + TAXGROUP + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
           " ORDER BY G.Trans_Code"
                ds = clsDBFuncationality.GetDataTable(sql)
                If ds.Rows.Count > 0 Then
                    gvTax.DataSource = ds
                End If
                AddHandler fndTaxGroup.TextChanged, AddressOf fndTaxGroup_TextChanged


                AddHandler txtCustDisc.TextChanged, AddressOf totalAmounts

                AddHandler txtDiscPer.TextChanged, AddressOf MyTextBox1_TextChanged

                gvTax.AllowAddNewRow = False

                Dim i1 As Integer
                gvTax.DataSource = Nothing
                gvTax.Rows.Clear()
                For i1 = 1 To 10
                    sql = "Select (case When Tax" + i1.ToString() + " is NULL THEN '' else Tax" + i1.ToString() + " end),Tax" + i1.ToString() + "_Rate,Tax" + i1.ToString() + "_Assessable_Amt,Tax" + i1.ToString() + "_Amt from TSPL_SALES_ORDER_HEAD WHERE Order_No='" + txtDocNo.Value + "'"
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
                Dim SQLTAX As String = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code as [taxcode]  FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code  WHERE G.Tax_Group_Code = '" + fndTaxGroup.Value + "' AND G.Tax_Group_Type = 'S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code  ORDER BY G.Trans_Code"
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

        dtEndTime = DateTime.Now
        span = dtEndTime.Subtract(dtStartTime)
        strMsg += "Tax fill:" + clsCommon.myCstr(span.Hours) + ":" + clsCommon.myCstr(span.Minutes) + ":" + clsCommon.myCstr(span.Seconds) + ":" + clsCommon.myCstr(span.Milliseconds) + Environment.NewLine
        dtStartTime = DateTime.Now
    End Sub

    Private Sub funfilldetail(ByVal obj As clsSalesOrder)
        LoadBlankGrid()
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
                datarowinfo.Cells(colShippedQty).Value = objtr.Order_Qty
                datarowinfo.Cells(colUnitCode).Value = objtr.Unit_code

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

                datarowinfo.Cells(colTonnagePerUnit).Value = objtr.Tonnage_Per_Unit
                datarowinfo.Cells(colTonnage).Value = objtr.Tonnage

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

        If btnAdd.Text = "Update" Then
            Dim strchk As String = "select Is_Post from TSPL_SALES_ORDER_HEAD where Order_No='" + txtDocNo.Value + "'"
            Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
            If chkpost = "Y" Then
                clsCommon.MyMessageBoxShow("Transection already posted")
                Return False
            End If
        End If


        Dim isCalculateShall As Boolean = IIf(clsCommon.myCdbl(txtshellqty.Text) > 0, False, True)
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
        'gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)
        CheckTransactionDiscount()
        CalculateDiscountAmount()





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
                If Not (checkItemonLocation(gv1.Rows(ii).Cells(ColICode).Value, gv1.Rows(ii).Cells(colShippedQty).Value, txtLocation.Value, gv1.Rows(ii).Cells(colUnitCode).Value, clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value), False, gv1.Rows(ii).Cells(colBatchNumber).Value)) Then
                    Return False
                End If

                ' ''Check for Balance With Unapproved Qty
                ''If clsCommon.CompairString(cboLoadOutType.Text, "Sale") = CompairStringResult.Equal Then
                'Dim strCurrMRP As Double = clsCommon.myCdbl(strMRP)
                'Dim dblOuterConvFac As Double = 0
                'If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal Then
                '    dblOuterConvFac = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                '    strCurrMRP = clsCommon.myCdbl(strMRP) * dblOuterConvFac
                'ElseIf clsCommon.CompairString(strUOM, "FC") = CompairStringResult.Equal Then
                '    dblOuterConvFac = clsItemMaster.GetConvertionFactor(strICode, strUOM, Nothing)
                '    strCurrMRP = clsCommon.myCdbl(strMRP) * dblOuterConvFac
                'End If
                'Dim dblBalQty As Double = clsItemLocationDetails.getBalanceWithUnapprove(strICode, txtLocation.Value, clsCommon.myCstr(strCurrMRP), strUOM, txtDocNo.Value)
                'Dim dblEnteredQty As Double = dblQty
                'For jj As Integer = 0 To gv1.Rows.Count - 1
                '    If ii = jj Then
                '        Continue For
                '    End If
                '    Dim strICodeInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(ColICode).Value)
                '    Dim strUOMInner As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnitCode).Value)
                '    Dim dblMRPInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colMRP).Value)
                '    Dim dblQtyInner As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colShippedQty).Value)

                '    Dim dblInnerConvFac As Double = 0
                '    If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal Then
                '        dblInnerConvFac = clsItemMaster.GetConvertionFactor(strICodeInner, strUOM, Nothing)
                '        dblMRPInner = dblMRPInner / dblInnerConvFac
                '    ElseIf clsCommon.CompairString(strUOM, "FC") = CompairStringResult.Equal Then
                '        dblInnerConvFac = clsItemMaster.GetConvertionFactor(strICodeInner, strUOMInner, Nothing)
                '        dblMRPInner = dblMRPInner * dblInnerConvFac
                '    End If

                '    If dblQtyInner > 0 AndAlso dblMRPInner = clsCommon.myCdbl(strMRP) AndAlso clsCommon.CompairString(strICodeInner, strICode) = CompairStringResult.Equal Then
                '        If clsCommon.CompairString(strUOM, strUOMInner) = CompairStringResult.Equal Then
                '            dblEnteredQty += dblQtyInner
                '        Else
                '            If clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal Then
                '                dblEnteredQty += (dblQtyInner * dblInnerConvFac)
                '            ElseIf clsCommon.CompairString(strUOM, "FC") = CompairStringResult.Equal Then
                '                dblEnteredQty += (dblQtyInner / dblInnerConvFac)
                '            End If
                '        End If
                '    End If
                'Next
                'dblEnteredQty = Math.Round(dblEnteredQty, 2, MidpointRounding.ToEven)
                'If dblEnteredQty > dblBalQty Then
                '    Throw New Exception("Item - " + strICode + " , MRP - " + strMRP + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                'End If

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

        If isCalculateShall AndAlso arrShall.Count > 0 Then
            Dim dblTotShall As Double
            For Each Keys As Integer In arrShall.Keys
                dblTotShall += Math.Ceiling(arrShall(Keys) / Keys)
            Next
            txtshellqty.Value = clsCommon.myCstr(dblTotShall)
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
        If clsCommon.myCdbl(txtDiscPer.Text) >= 100 Then
            Throw New Exception("Discount Percentage can't be " + clsCommon.myCstr(txtDiscPer.Text))
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

        If index > 0 Then
            gv1.CurrentRow = gv1.Rows(index)
        End If

        Return True
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
                    'Exit Function
                    Return False
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
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        resetdata()
        txtDate.Focus()
    End Sub

    Public Sub resetdata()
        resetFNDCustomer()
        resetFNDTaxGroup()
        resetForm()


        lblLocation.Text = ""
        isCellValueChangedOpen = False
        txtDiscPer.ReadOnly = False
        txtDate.Enabled = True
        btnPost.Visible = True
        txtPaymentAmt.Value = 0



        chkDiscountOnAmt.IsChecked = True
        chkDiscountOnRate.IsChecked = True

        isNewEntry = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeform()
    End Sub

    Public Sub closeform()
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Deletedata()
    End Sub

    Sub Deletedata()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Order No not found to Delete")
            End If

            
            Dim Reason As String = ""
            If clsCommon.MyMessageBoxShow("Do you want to delete the current Order" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                '' REASON FOR DELETE 
                If clsCancelLog.CheckForReasonOnDelete() Then
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                
                If clsSalesOrder.DeleteData(txtDocNo.Value) Then
                    saveCancelLog(Reason, Nothing)
                End If
                clsCommon.MyMessageBoxShow("Transaction Deleted Successfully", Me.Text)
                resetdata()
            End If
            

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = "Delete"
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
        If myMessages.postConfirm() Then
            PostDataFun()
        End If
    End Sub

    Private Sub PostDataFun()
        Try
            If clsSalesOrder.PostData(txtDocNo.Value) Then
                myMessages.post()
                btnPost.Enabled = False
                btnAdd.Enabled = False
                btnDelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                Throw New Exception("Error in posting.")
            End If
        Catch ex As Exception
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

    Private Sub rbFB_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbFB.ToggleStateChanged
        If rbFB.ToggleState = ToggleState.On Then

            priceDateSelection("FB")

        End If
        lblfc.Text = 0
        lblfb.Text = 0
        funSetFirstRow()
    End Sub

    Private Sub rbFC_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbFC.ToggleStateChanged
        If rbFC.ToggleState = ToggleState.On Then

            priceDateSelection("FC")


        End If
        lblfc.Text = 0
        lblfb.Text = 0
        funSetFirstRow()
    End Sub

    Private Sub rbAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbAll.ToggleStateChanged

        If rbAll.ToggleState = ToggleState.On Then

            priceDateSelection()

        End If
        lblfc.Text = 0
        lblfb.Text = 0
        funSetFirstRow()
    End Sub

    Private Sub FrmShipment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag Then
            resetdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnAdd.Enabled Then
            SaveMainClick()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            Deletedata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            print()


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

        End If
    End Sub



    Private Sub txtshellqty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(45)) Then
        Else
            e.Handled = True
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

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub fndCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustomer._MYValidating
        txtCustomerName.Focus()
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            Exit Sub
        End If
        Dim qry As String = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State,TSPL_LOCATION_MASTER.Sales_Tax_GroupIS,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Sales_Tax_GroupISName  FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' WHERE TSPL_LOCATION_MASTER.Location_Code = '" + Convert.ToString(txtLocation.Value) + "'"
        Dim dtLocation As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim loc As String = clsCommon.myCstr(dtLocation.Rows(0)("Excisable"))
        Dim strLocState As String = clsCommon.myCstr(dtLocation.Rows(0)("State"))
        Dim WhrCls As String = " 2=2"
        qry = "SELECT M.Cust_Code AS [Code], m.Customer_Name as [Name], m.Route_No as [Route No], m.Price_Code as [Excisable Price Code], m.price_CodeNon as [Non-Excisable Price Code], (case when m.Credit_Customer = 'Y' THEN 'YES' ELSE 'NO' END) AS [Credit Customer], M.Cust_Category_Code AS [Customer Category Code],M.City_Code as [City Code],TSPL_CITY_MASTER.City_Name as City  FROM TSPL_CUSTOMER_MASTER M JOIN TSPL_CUSTOMER_ACCOUNT_SET A ON M.Cust_Account =A.Cust_Account left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=M.City_Code"
        WhrCls += " and M.status='N' AND M.OnHold='N'"
        txtCustomer.Value = clsCommon.ShowSelectForm("SOCustFinder", qry, "Code", WhrCls, txtCustomer.Value, "Code", isButtonClicked)



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
                RemoveHandler fndTaxGroup.TextChanged, AddressOf fndTaxGroup_TextChanged
                SetTaxGroup(clsCommon.myCstr(dtLocation.Rows(0)("Sales_Tax_GroupIS")))
                fndTaxGroup.Value = clsCommon.myCstr(dtLocation.Rows(0)("Sales_Tax_GroupIS"))
                lblTaxDesc.Text = clsCommon.myCstr(dtLocation.Rows(0)("Sales_Tax_GroupISName"))
                AddHandler fndTaxGroup.TextChanged, AddressOf fndTaxGroup_TextChanged
            End If
        End If
        If clsCommon.myLen(txtCustomer.Value) > 0 Then
            Try
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
                    rbFC.IsChecked = True
                    txtSalesman.Value = dt.Rows(0)("Salesman_Code").ToString()
                    strCustAccount = dt.Rows(0)("Cust_Account").ToString()
                    txtPaymentTerm.Value = dt.Rows(0)("Terms_Code").ToString()

                End If
            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.ToString())
                Exit Sub
            End Try
        End If


        funSetFirstRow()
        If clsCommon.myLen(txtCustomer.Value) > 0 Then
            txtCustomer.Enabled = False
        End If



        txtLocation.Enabled = False

        'txtDate.Enabled = False
        Dim dtEndTime As DateTime = DateTime.Now
        Dim span As TimeSpan = dtEndTime.Subtract(dtStartTime)
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
            txtLocation.Value = clsCommon.ShowSelectForm("SoLoFND", qry, "Code", whrClus, txtLocation.Value, "Code", isButtonClicked)
            lblLocation.Text = clsLocation.GetName(txtLocation.Value, Nothing)

            sql = "Select Excisable,Sales_Tax_Group from TSPL_LOCATION_MASTER WHERE Location_Code='" + txtLocation.Value + "' "
            Dim lDr As DataTable = clsDBFuncationality.GetDataTable(sql)
            If lDr IsNot Nothing AndAlso lDr.Rows.Count > 0 Then
                If lDr.Rows(0)(0).ToString() = "F" Or lDr.Rows(0)(0).ToString() = "N" Then
                    If gvTax.Rows.Count > 0 Then
                        For Each grow As GridViewRowInfo In gvTax.Rows
                            sql = "Select Excisable from TSPL_TAX_MASTER Where Tax_Code='" + grow.Cells("taxAuthority").Value + "'"
                            If clsDBFuncationality.getSingleValue(sql) = "Y" Then
                                common.clsCommon.MyMessageBoxShow("Tax group with excisable tax authority is not applicable for given location")
                                fndTaxGroup.Value = ""
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
                                    fndTaxGroup.Value = ""
                                    Exit For
                                End If
                            Else
                                Exit For
                            End If
                        Next
                    End If
                End If

            End If

            fndTaxGroup.Value = Convert.ToString(clsDBFuncationality.getSingleValue("SELECT Sales_Tax_Group  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + txtLocation.Value + "'"))
            lblTaxDesc.Text = clsTaxGroupMaster.GetNameOfSaleType(fndTaxGroup.Value, Nothing)
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
        Dim qry As String = "Select Order_No as Code,TSPL_SALES_ORDER_HEAD.Cust_Code as 'Customer Code',Cust_Name as 'Customer Name',CONVERT(varchar(15), Order_Date , 103) as [Shipment Date] ,Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as Location, case when Is_Post=0 then 'Unposted' else 'Posted' end as [Status], Route_No as [Route Code],Route_Desc as [Route] "
        qry += " FROM TSPL_SALES_ORDER_HEAD "
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SALES_ORDER_HEAD.Location"
        Dim whrClas As String = " 2=2"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and TSPL_SALES_ORDER_HEAD.Location  in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        LoadData(clsCommon.ShowSelectForm("SoFND", qry, "Code", whrClas, txtDocNo.Value, "", isButtonClicked), NavigatorType.Current)
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
                    gv1.CurrentRow.Cells(colMainItem).ReadOnly = clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeItem).Value), "No") = CompairStringResult.Equal
                ElseIf (e.Column Is gv1.Columns(colDiscountCode)) Then
                    gv1.CurrentRow.Cells(colDiscountCode).ReadOnly = clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colSchemeItem).Value), "No") = CompairStringResult.Equal
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtShipTo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtShipTo._MYValidating
        If clsCommon.myLen(txtCustomer.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Select Customer", Me.Text)
            Exit Sub
        End If

        Dim qry As String = "select Ship_To_Code as Code,Ship_To_Desc as Description,Add1,Add2,Add3,Add4,City_Code as City,State,Country,Telphone,Email,Pin_Code, Tin_No, CST_No from TSPL_SHIP_TO_LOCATION"
        Dim whrClas As String = "Ship_To_Type_Code='" + txtCustomer.Value + "'"
        txtShipTo.Value = clsCommon.ShowSelectForm("SOLOCONSIGNEE", qry, "Code", whrClas, txtShipTo.Value, "", isButtonClicked)
        lblShipTo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Ship_To_Desc from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + txtShipTo.Value + "'"))
    End Sub

    Private Sub RadMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub fndVehicleCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVehicleCode._MYValidating
        Dim qry As String = "Select Vehicle_Id as Code,Number,Description,Model from TSPL_VEHICLE_MASTER"
        txtVehicleCode.Value = clsCommon.ShowSelectForm("SOVehicaleFinder", qry, "Code", "", txtVehicleCode.Value, "", isButtonClicked)

        sql = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicleCode.Value + "'"
        lblVhicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
    End Sub

    Private Sub fndemployeecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtEmployeeCode._MYValidating
        Dim qry As String = "select EMP_CODE as Code, Emp_Name as [Employee Name] from TSPL_EMPLOYEE_MASTER"
        txtEmployeeCode.Value = clsCommon.ShowSelectForm("SOEmpFinder", qry, "Code", "", txtEmployeeCode.Value, "", isButtonClicked)

        qry = "select  Emp_Name  from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + txtEmployeeCode.Value + "'"
        lblEmpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub

    Private Sub fndRouteNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRouteNo._MYValidating

        Dim qry As String = "Select Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
        txtRouteNo.Value = clsCommon.ShowSelectForm("SORouteFinder", qry, "Code", "", txtRouteNo.Value, "", isButtonClicked)
        fndRouteNo_TextChanged()
    End Sub

    Private Sub fndSalesman__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesman._MYValidating
        Dim qry As String = "SELECT Emp_Code as Code,Emp_Name as 'Name' from TSPL_EMPLOYEE_MASTER   "
        txtSalesman.Value = clsCommon.ShowSelectForm("SOsalesmanFinder", qry, "Code", "Emp_Type='SalesMan'", txtSalesman.Value, "", isButtonClicked)

    End Sub

    Private Sub fndPaymentTerms__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPaymentTerm._MYValidating
        Dim qry As String = "SELECT Terms_Code as Code, Terms_Desc as Description, No_Days as 'No of Days' FROM TSPL_TERMS_MASTER"
        txtPaymentTerm.Value = clsCommon.ShowSelectForm("SOPaymentFinder", qry, "Code", "", txtPaymentTerm.Value, "", isButtonClicked)
    End Sub

    Private Sub MyTextBox1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscPer.Leave, txtDiscAmt.Leave
        CalculateDiscountAmount()
    End Sub

    Private Sub CalculateDiscountAmount()
        Dim discountrate As Decimal = Decimal.Parse(txtDiscPer.Text)

        If chkDiscountOnAmt.IsChecked Then
            discountrate = clsCommon.myCstr(Math.Round(((txtDiscAmt.Value * 100) / clsCommon.myCdbl(txtShipmentTotal.Text)), 5, MidpointRounding.ToEven))
            txtDiscPer.Value = discountrate
        End If


        Dim disamt As Decimal = 0
        Dim dblCustDiscountNoTax As Double = 0
        If String.IsNullOrEmpty(txtShipmentTotal.Text) Then
            txtShipmentTotal.Text = 0
        End If
        Dim pricedate As String = String.Empty
        For Each gro As GridViewRowInfo In gv1.Rows
            If clsCommon.myCdbl(gro.Cells(colShippedQty).Value) <> 0 Then

                pricedate = CDate(gro.Cells(colPriceDateColumn).Value).ToString("yyyy-MM-dd")
                If (clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value)) = CompairStringResult.Equal OrElse clsCommon.CompairString("Yes", clsCommon.myCstr(gro.Cells(colSampleItem).Value)) = CompairStringResult.Equal) Then
                    disamt = 0
                Else
                    Dim itemnameamt As Decimal = clsDBFuncationality.getSingleValue("select Item_Basic_Price  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + Convert.ToString(gro.Cells(ColICode).Value) + "' and Item_Basic_Net = '" + clsCommon.myCstr(gro.Cells(colMRP).Value) + "' and Price_Code = '" + Convert.ToString(txtPriceCode.Text) + "' and Start_Date = '" + pricedate + "'")
                    disamt = Math.Round(clsCommon.myCdbl(itemnameamt) * discountrate / 100, 2)
                End If
                Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gro.Cells(ColICode).Value), clsCommon.myCstr(clsCommon.myCstr(gro.Cells(colUnitCode).Value)), Nothing)
                dblCustDiscountNoTax = clsCommon.myCdbl(gro.Cells(ColCustDisNoTax).Value)
                gro.Cells(colDiscountAmount).Value = disamt
                gro.Cells(colTotalDiscountAmount).Value = Math.Round(((disamt + dblCustDiscountNoTax) * clsCommon.myCdbl(gro.Cells(colShippedQty).Value)) / dblConvFac, 2)
                If ((clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSchemeItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colSampleItem).Value), "Yes") = CompairStringResult.Equal) OrElse (clsCommon.CompairString(clsCommon.myCstr(gro.Cells(colPromoSchemeItem).Value), "Yes") = CompairStringResult.Equal)) Then
                    gro.Cells(colitemNetAmount).Value = 0
                Else
                    gro.Cells(colitemNetAmount).Value = Math.Round(gro.Cells(colBasicAmount).Value - disamt - (dblCustDiscountNoTax) / dblConvFac, 5)
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
        If discountrate = 0 Then
            txtDiscAmt.Text = 0
        ElseIf discountrate = 100 Then
            Dim txtvalue As Decimal = 0
            For Each g As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(g.Cells(colShippedQty).Value) > 0 Then
                    txtShipmentTotal.Text = txtShipmentTotal.Text + clsCommon.myCdbl(g.Cells(colBasicAmount).Value) * clsCommon.myCdbl(g.Cells(colShippedQty).Value)
                    txtvalue += clsCommon.myCdbl(g.Cells(colTotalItemAmount).Value)
                End If
            Next
            txtShipmentAmt.Text = txtTotalTaxAmount.Text
            txtDiscAmt.Text = txtShipmentTotal.Text
            txtNetShipAmt.Text = 0
        Else
            txtDiscAmt.Text = Math.Round(clsCommon.myCdbl(txtShipmentTotal.Text) * discountrate / 100, 2)
        End If
        Dim totaltpt As Decimal = 0
        For Each tptcount As GridViewRowInfo In gv1.Rows
            totaltpt = totaltpt + clsCommon.myCdbl(tptcount.Cells(colTotalTPT).Value)
        Next
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
            Else
                If discountrate = 100 Then
                    txtShipmentTotal.Text = 0.0
                End If
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintOrder(txtLocation.Value)
    End Sub

    Sub PrintOrder(ByVal Location As String)
        If txtDocNo.Value = "" Then
            common.clsCommon.MyMessageBoxShow("Please Select Order No.")
        Else
            Dim whereclause As String = " where  TSPL_SALES_ORDER_DETAIL.Order_Qty>0 and  TSPL_SALES_ORDER_HEAD.Order_No =  '" + txtDocNo.Value + "'"
            Dim strQuery As String = ""
            strQuery = "select Payment_Amount,(case when len(isnull(TSPL_CUSTOMER_MASTER.Add1,''))>0 then  isnull(TSPL_CUSTOMER_MASTER.Add1,'')+ (case when LEN(ISNULL(TSPL_CUSTOMER_MASTER.Add2,''))>0 then ','else '' end ) +isnull(TSPL_CUSTOMER_MASTER.Add2,'')+(case when LEN(ISNULL(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ','else '' end ) +isnull(TSPL_CUSTOMER_MASTER.Add3,'')+(case when LEN(ISNULL(TSPL_CUSTOMER_MASTER.City_Code,''))>0 then ','else '' end ) +isnull(TSPL_CUSTOMER_MASTER.City_Code,'')+(case when LEN(ISNULL(TSPL_CUSTOMER_MASTER.State,''))>0 then ','else '' end ) + isnull(TSPL_CUSTOMER_MASTER.State,'')  else TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 end) AS Address,"
            strQuery += "  TSPL_SALES_ORDER_HEAD.Cust_Name , TSPL_SALES_ORDER_HEAD.Description,TSPL_SALES_ORDER_HEAD.Remarks, TSPL_SALES_ORDER_HEAD.Order_No, Scheme_Item, ISNULL(Discount_Code,'') as Discount_Code, "
            strQuery += " TSPL_SALES_ORDER_HEAD.Order_Date ,TSPL_SALES_ORDER_HEAD.Total_Tonnage , TSPL_SALES_ORDER_DETAIL.Item_Code, TSPL_SALES_ORDER_DETAIL.Item_Desc + case When (Scheme_Item='Y' AND ISNULL(Discount_Code,'')='') Then '-(T)' When (Scheme_Item='Y' AND ISNULL(Discount_Code,'')<>'') Then '-(S)' Else '' End as Item_Desc ,TSPL_SALES_ORDER_DETAIL.Unit_code, TSPL_SALES_ORDER_DETAIL.TAX1_Amt*Order_Qty  as TAX1_AmtDetail ,"
            strQuery += " (Select Conversion_Factor from TSPL_ITEM_UOM_DETAIL WHERE TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALES_ORDER_DETAIL.Item_Code  AND TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALES_ORDER_DETAIL.Unit_code) as [orgConversion], Case When TSPL_SALES_ORDER_DETAIL.Unit_code ='FC' Then (Select Conversion_Factor from TSPL_ITEM_UOM_DETAIL Where TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALES_ORDER_DETAIL.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code='FB') Else 1 END as [Conversion], "
            strQuery += " TSPL_SALES_ORDER_DETAIL.Order_Qty ,TSPL_SALES_ORDER_DETAIL.Order_Qty*TSPL_SALES_ORDER_DETAIL.Basic_Rate  as Amount,"
            strQuery += " (select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code=TSPL_SALES_ORDER_HEAD.TAX1) as TAX1,TSPL_SALES_ORDER_HEAD.TAX1_Rate,TSPL_SALES_ORDER_HEAD.TAX1_Assessable_Amt,TSPL_SALES_ORDER_HEAD.TAX1_Amt,(select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code = TSPL_SALES_ORDER_HEAD.TAX2) as TAX2 ,TSPL_SALES_ORDER_HEAD.TAX2_Rate,TSPL_SALES_ORDER_HEAD.TAX2_Assessable_Amt,case when  TSPL_SALES_ORDER_HEAD.Order_Disc_Percent=100 then 0 else  TSPL_SALES_ORDER_HEAD.TAX2_Amt end as TAX2_Amt,(select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code =TSPL_SALES_ORDER_HEAD.TAX3) as TAX3,TSPL_SALES_ORDER_HEAD.TAX3_Rate,TSPL_SALES_ORDER_HEAD.TAX3_Assessable_Amt,case when  TSPL_SALES_ORDER_HEAD.Order_Disc_Percent =100 then 0 else  TSPL_SALES_ORDER_HEAD.TAX3_Amt end as TAX3_Amt,(select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code =TSPL_SALES_ORDER_HEAD.TAX4) as TAX4,TSPL_SALES_ORDER_HEAD.TAX4_Rate,TSPL_SALES_ORDER_HEAD.TAX4_Assessable_Amt,case when  TSPL_SALES_ORDER_HEAD.Order_Disc_Percent=100 then 0 else  TSPL_SALES_ORDER_HEAD.TAX4_Amt end as TAX4_Amt ,isnull((select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code = TSPL_SALES_ORDER_HEAD.TAX5),'') as TAX5,TSPL_SALES_ORDER_DETAIL.Total_TPT,TSPL_SALES_ORDER_DETAIL.Empty_Value,TSPL_SALES_ORDER_HEAD.TAX5_Rate,TSPL_SALES_ORDER_HEAD.TAX5_Assessable_Amt,case when  TSPL_SALES_ORDER_HEAD.Order_Disc_Percent=100 then 0 else  TSPL_SALES_ORDER_HEAD.TAX5_Amt end as TAX5_Amt ,TSPL_SALES_ORDER_HEAD.Total_Order_Amt,TSPL_SALES_ORDER_DETAIL.MRP_Amt,case when Order_Disc_Percent=100 then 0 else TSPL_SALES_ORDER_HEAD.Order_Discount_Amt end as Order_Discount_Amt,case when Order_Disc_Percent =100 then 0 else TSPL_SALES_ORDER_DETAIL.Basic_Rate end as Basic_Rate ,TSPL_SALES_ORDER_DETAIL.Item_Assessable_Rate,TSPL_SALES_ORDER_DETAIL.Item_Net_Amt,TSPL_CUSTOMER_MASTER.Tin_No,TSPL_SALES_ORDER_HEAD.Cust_PONo ,(select [USER_NAME] from TSPL_USER_MASTER where TSPL_USER_MASTER.User_Code=TSPL_SALES_ORDER_HEAD.Created_By) as CreateByName,(case when TSPL_SALES_ORDER_HEAD.Is_Post='1' then (select [USER_NAME] from TSPL_USER_MASTER where User_Code=TSPL_SALES_ORDER_HEAD.Modify_By) else '' end) as PostByName,TSPL_SALES_ORDER_HEAD.Salesman_Code,TSPL_EMPLOYEE_MASTER.Emp_Name as SalesmanName,TSPL_SALES_ORDER_HEAD.Route_No,TSPL_SALES_ORDER_HEAD.Route_Desc,  TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 from TSPL_SALES_ORDER_HEAD left outer join  TSPL_SALES_ORDER_DETAIL on TSPL_SALES_ORDER_DETAIL.Order_No =TSPL_SALES_ORDER_HEAD.Order_No left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SALES_ORDER_HEAD.Cust_Code left outer join TSPL_COMPANY_MASTER on TSPL_SALES_ORDER_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_SALES_ORDER_HEAD.Salesman_Code "
            strQuery = strQuery & whereclause

            Try
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                If dt.Rows.Count > 0 Then
                    Dim Excisable As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Excisable from TSPL_LOCATION_MASTER  WHERE Location_Code='" + Location + "'"))
                    If clsCommon.CompairString(Excisable, "T") = CompairStringResult.Equal Then
                        Dim frmcrystal As New frmCrystalReportViewer()
                        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptOrderWithExcise", "Sales Order")
                    Else
                        Dim frmcrystal As New frmCrystalReportViewer()
                        frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptOrderWithNonExcise", "Sales Order")
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow("No Data Found", "Sales Order Report", MessageBoxButtons.OK)
                End If

            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.Message, "Sales Order Report", MessageBoxButtons.OK)
            End Try
        End If
    End Sub

    Private Sub btnUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnpost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim qry As String = "select 1 from TSPL_SALES_ORDER_HEAD where Order_No='" + txtDocNo.Value + "' and Is_Post=1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Transaction status should be posted.")
                End If
                qry = "select Shipment_No from TSPL_SHIPMENT_MASTER where Order_No='" + txtDocNo.Value + "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = "Sales order used in following shipment"
                    For Each dr As DataRow In dt.Rows
                        qry += Environment.NewLine + clsCommon.myCstr(dr("Shipment_No"))
                    Next
                    qry += Environment.NewLine + "Can't unpost it"
                    Throw New Exception(qry)
                End If
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    qry = "update TSPL_SALES_ORDER_HEAD set Is_Post=0,Post_Date=null where Order_No='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    clsCommon.MyMessageBoxShow("Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnCheckSlip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckSlip.Click
        CheckSlip(txtDocNo.Value)
    End Sub
    Sub CheckSlip(ByVal OrderNo As String)
        If txtDocNo.Value = "" Then
            common.clsCommon.MyMessageBoxShow("Please Select Order No.")
        Else
            Dim whereclause As String = " where TSPL_SALES_ORDER_HEAD.Order_No =  '" + OrderNo + "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='FB'"
            Dim strQuery As String = ""
            strQuery = " select Flavour,Size,max(Case1) as [Case],max(TSPL_INV_CLASS_DETAILS.Inv_Class_Desc) as [Flavour Description],max(Vehicle_No) as Vehicle_No,max(Order_No) as [Indent No] "
            strQuery += " ,max(Order_Date) as [Date],max(Cust_Name) as [Party Name],max(Route_No) as [Route],max(abc.Item_Code) as ItemCode ,sum(ordered ) as [ordered],'' as[Loaded] "
            strQuery += " ,max(MRPBottle) as   MRPBottle ,max(Comp_Name) as Comp_Name,max(Address) as Address,MAX(TPTName ) as TPTName, max(Route_Desc) as Route_Desc  from( "
            strQuery += " SELECT  LEFT(TSPL_SALES_ORDER_DETAIL.Item_Code ,2) AS [Flavour], cast(SUBSTRING (TSPL_SALES_ORDER_DETAIL.Item_Code,3,LEN(TSPL_SALES_ORDER_DETAIL.Item_Code)-5)as DECIMAL(9,2) ) as [Size] , "

            strQuery += " TSPL_SALES_ORDER_DETAIL.Item_Code,SUBSTRING(TSPL_SALES_ORDER_DETAIL.Item_Code,len(TSPL_SALES_ORDER_DETAIL.Item_Code )-2,1)as [Case1],RIGHT(TSPL_SALES_ORDER_DETAIL.Item_Code,2) as bottlecase "
            strQuery += " ,TSPL_SALES_ORDER_HEAD.Order_No,TSPL_SALES_ORDER_HEAD.Order_Date ,TSPL_SALES_ORDER_HEAD.Vehicle_No ,TSPL_SALES_ORDER_HEAD.Cust_Name,TSPL_SALES_ORDER_HEAD.Route_No "
            strQuery += " ,Comp_Name,case when LEN(Add2 )>0 then Add1+', '+ Add2 else case when LEN(Add3 )>0 then Add1+', '+ Add2+', '+Add3 else Add1 end end as [Address], TSPL_SALES_ORDER_HEAD.Route_Desc,TSPL_SALES_ORDER_DETAIL.MRP_Amt,TSPL_SALES_ORDER_DETAIL.Unit_code  "
            strQuery += " ,(case when TSPL_SALES_ORDER_DETAIL.Unit_code='FC' then MRP_Amt/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor else MRP_Amt end )as MRPBottle,(case when TSPL_SALES_ORDER_DETAIL.Unit_code ='FB' then  Order_Qty/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor else Order_Qty end) as [ordered], "
            strQuery += " (select max(vendor_name) from TSPL_VENDOR_MASTER LEFT outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.transport_id =TSPL_VENDOR_MASTER.vendor_code left outer join TSPL_SALES_ORDER_HEAD on TSPL_SALES_ORDER_HEAD.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id where TSPL_SALES_ORDER_HEAD.Order_No=TSPL_SALES_ORDER_DETAIL.Order_No  ) as [TPTName],"
            strQuery += " TSPL_SALES_ORDER_DETAIL.Order_Qty   FROM TSPL_SALES_ORDER_DETAIL  left outer join  TSPL_SALES_ORDER_HEAD ON TSPL_SALES_ORDER_DETAIL.Order_No=TSPL_SALES_ORDER_HEAD.Order_No "
            strQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_SALES_ORDER_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code "
            strQuery += " left outer join  TSPL_ITEM_UOM_DETAIL on  TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_SALES_ORDER_DETAIL.Item_Code"
            strQuery += " " + whereclause + " "
            strQuery += " )abc left outer join TSPL_INV_CLASS_DETAILS on   Flavour =TSPL_INV_CLASS_DETAILS.Inv_Class_Code "
            ' strQuery += " left outer join  TSPL_ITEM_UOM_DETAIL on  TSPL_ITEM_UOM_DETAIL.Item_Code= abc.Item_Code "
            'strQuery += " where TSPL_ITEM_UOM_DETAIL.UOM_Code=abc .Unit_code  "
            strQuery += "  group by Size ,Flavour order by CASE  WHEN Size  = '200' THEN 1  WHEN Size = '250' THEN 2 WHEN Size = '300' THEN 3 WHEN Size = '500' THEN 4 WHEN Size = '600' THEN 5 WHEN Size = '1.0' THEN 6 WHEN Size = '1.2' THEN 7 WHEN Size = '1.5' THEN 8 ELSE 9 END "
            strQuery = strQuery

            Try
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                If dt.Rows.Count > 0 Then
                    Dim frmcrystal As New frmCrystalReportViewer()
                    frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(strQuery), "crptCheckSlipReport", "Check Slip")
                Else
                    common.clsCommon.MyMessageBoxShow("No Data Found", "Sales Order Report", MessageBoxButtons.OK)
                End If

            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(ex.Message, "Sales Order Report", MessageBoxButtons.OK)
            End Try
        End If
    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 AndAlso Not isInsideLoadData Then
            setBalance()
        End If
    End Sub

    Sub setBalance()
        lblSalesmanQty.Text = getQty(txtSalesman.Value, txtDate.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value), True)
        lblCustomerQty.Text = getQty(txtCustomer.Value, txtDate.Value, clsCommon.myCstr(gv1.CurrentRow.Cells(ColICode).Value), False)
    End Sub

    Public Function getQty(ByVal strSalesmanOrCustomer As String, ByVal IDate As Date, ByVal strICode As String, ByVal isForSalesman As Boolean) As String
        Dim qry As String = "select SUM( TSPL_SALES_ORDER_DETAIL.Order_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as OrderQty from TSPL_SALES_ORDER_DETAIL"
        qry += " left outer join TSPL_SALES_ORDER_HEAD on TSPL_SALES_ORDER_HEAD.Order_No=TSPL_SALES_ORDER_DETAIL.Order_No"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALES_ORDER_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALES_ORDER_DETAIL.Unit_code"
        qry += " where TSPL_SALES_ORDER_HEAD.Order_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(IDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_SALES_ORDER_HEAD.Order_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(IDate), "dd/MMM/yyyy hh:mm tt") + "'  "

        If isForSalesman Then
            qry += " and Salesman_Code='" + strSalesmanOrCustomer + "'"
        Else
            qry += " and Cust_Code='" + strSalesmanOrCustomer + "'"
        End If
        Return clsCommon.myFormat(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)))

    End Function

   
    
    Private Sub fndTaxGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndTaxGroup._MYValidating
        Dim qry As String = clsERPFuncationality.UserAvailableTaxGroup + " AND M.Tax_Group_Type='S'"
        fndTaxGroup.Value = clsCommon.ShowSelectForm("fndTaxGroup@", qry, "Code", "", fndTaxGroup.Value, "", isButtonClicked)
        lblTaxDesc.Text = clsTaxGroupMaster.GetNameOfSaleType(fndTaxGroup.Value, Nothing)
    End Sub
End Class
