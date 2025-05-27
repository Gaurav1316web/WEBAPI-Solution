Imports System.Data.SqlClient
Imports System.IO
Imports common
Public Class frmLeakedSaleReturn
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim GSTStatus As Boolean = False
    Dim Price_code As String = ""
    Public strExcise As Boolean
    Dim CalculateTaxRatefromItemwsieTaxOnSale As Integer = 0
    '' For Detail
    Const colLineNo As String = "colLineNo"
    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colQty As String = "colQty"
    Const colUOM As String = "colUOM"
    Const colpriceitem As String = "colpriceitem"
    Const colPriceItemName As String = "colPriceItemName"
    Const colPriceItemUOM As String = "colPriceItemUOM"
    Const colFat As String = "colFat"
    Const colSNF As String = "colSNF"
    Const colFatKG As String = "colFatKG"
    Const colSNFKG As String = "colSNFKG"
    Const colRate As String = "colRate"
    Const colAmount As String = "colAmount"
    Const colDisPer As String = "COLDISPER"
    Const colDisAmt As String = "COLDISAMT"
    Const colAmtAfterDis As String = "COLAMTAFTERDIS"
    Const colTAXGroup As String = "colTAXGroup"
    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxOnBaseAmount As String = "colIsTaxOnBaseAmount"
    Const colTotTaxAmt As String = "TAXAMT"
    Const colLocation As String = "colLocation"
    Const colItem_Net_Amt As String = "colItem_Net_Amt"
    Const colPriceCode As String = "colPriceCode"
    '' For taxes 
    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"
    '' For Main item
    Const colAPItem As String = "colAPItem"
    Const colAPitemName As String = "colAPitemName"
    Const colAPQty As String = "colAPQty"
    Const colAPUOM As String = "colAPUOM"
#End Region
    Public Sub SetUserMgmtNew()
        ''Richa Check in 19/06/2020
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnreverse.Visible = MyBase.isReverse
    End Sub
    Private Sub frmLeakedSaleReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        CalculateTaxRatefromItemwsieTaxOnSale = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, Nothing))
        CreateTable()
        AddNew()
        setGridFocus()
    End Sub
    Private Sub frmLeakedSaleReturn_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()

        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIR
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnreverse.Visible = True
                End If

            End If
        End If
    End Sub
    Sub AddNew()
        isNewEntry = True
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtCustomer.Value = ""
        txtLocation.Value = ""
        lblLocationDesc.Text = ""
        txtRouteNo.Value = ""
        txtRemarks.Text = ""
        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        End If
        rbtnTaxCalAutomatic.IsChecked = True
        lblAmtAfterDiscount.Text = ""
        lblAmtWithDiscount.Text = ""
        lblInvoiceDiscAmt.Text = ""
        lblDiscountAmt.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        TxtRoundoff.Text = ""
        lblTaxGrpName.Text = ""
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True

        LoadBlankGrid()
        LoadBlankGridMainItem()
        LoadBlankGridTax()
        isInsideLoadData = False
        btnreverse.Visible = False
    End Sub
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer, ByVal isBlankRate As Boolean)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxBaseAmt" + strII)).Value = Nothing
                If isBlankRate Then
                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                End If
            Else
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxBaseAmt" + strII)).Value = Nothing
                If isBlankRate Then
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                End If
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
            End If
        Next
    End Sub
    Private Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "varchar(30) NOT NULL Primary Key")
        coll.Add("Document_Date", "DateTime not NULL")
        coll.Add("Customer_Code", "varchar(12) NOT NULL")
        coll.Add("Route_No", "varchar(12) null references tspl_route_master(Route_No)")
        coll.Add("Remarks", "varchar(500) NULL")
        coll.Add("Tax_Group", "varchar(12) NOT NULL")
        coll.Add("Location_Code", "varchar(12) NULL")
        coll.Add("TAX1", "varchar(12) NULL")
        coll.Add("TAX1_Rate", "decimal(18, 4) NULL")
        coll.Add("TAX1_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX1_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX2", "varchar(12) NULL")
        coll.Add("TAX2_Rate", "decimal(18, 4) NULL")
        coll.Add("TAX2_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX2_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX3", "varchar(12) NULL")
        coll.Add("TAX3_Rate", "decimal(18, 4) NULL")
        coll.Add("TAX3_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX3_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX4", "varchar(12) NULL")
        coll.Add("TAX4_Rate", "decimal(18, 4) NULL")
        coll.Add("TAX4_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX4_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX5", "varchar(12) NULL")
        coll.Add("TAX5_Rate", "decimal(18, 4) NULL")
        coll.Add("TAX5_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX5_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX6", "varchar(12) NULL")
        coll.Add("TAX6_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX6_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX6_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX7", "varchar(12) NULL")
        coll.Add("TAX7_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX7_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX7_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX8", "varchar(12) NULL")
        coll.Add("TAX8_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX8_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX8_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX9", "varchar(12) NULL")
        coll.Add("TAX9_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX9_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX9_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX10", "varchar(12) NULL")
        coll.Add("TAX10_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX10_Amt", "decimal(18, 2) NULL")
        coll.Add("TAX10_Base_Amt", "decimal(18, 2) NULL")
        coll.Add("Discount_Base", "decimal(18, 2) NULL")
        coll.Add("Discount_Amt", "decimal(18, 2) NULL")
        coll.Add("Amount_Less_Discount", "decimal(18, 2) NULL")
        coll.Add("Total_Tax_Amt", "decimal(18, 2) NULL")
        coll.Add("Total_Amt", "decimal(18, 2) NULL")
        coll.Add("Status", "integer not null default 0")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modify_By", "varchar(12) NOT NULL")
        coll.Add("Modify_Date", "Datetime NOT NULL")
        coll.Add("Posting_Date", "Datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_LEAKED_SALE_RETURN_HEAD", coll, Nothing, True, True, "", "Document_Code", "Document_Date", True)
        coll = New Dictionary(Of String, String)()
        coll.Add("DOCUMENT_CODE", "Varchar(30) not null References TSPL_LEAKED_SALE_RETURN_HEAD(DOCUMENT_CODE)")
        coll.Add("Line_No", "integer not null default 0")
        coll.Add("Item_Code", "Varchar(50) NOT NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Unit_code", "varchar(12) NULL")
        coll.Add("Qty", "decimal(18, 2) NULL")
        coll.Add("Location", "varchar(12) NOT NULL")
        coll.Add("Price_Item", "Varchar(50) NOT NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Price_Item_UOM", "varchar(12) NULL")
        coll.Add("Item_Rate", "decimal(18, 2) NULL")
        coll.Add("FAt_Per", "decimal(18, 2) NULL")
        coll.Add("SNF_Per", "decimal(18, 2) NULL")
        coll.Add("FAT_KG", "decimal(18, 2) NULL")
        coll.Add("SNF_KG", "decimal(18, 2) NULL")
        coll.Add("TAXGroup", "varchar(12) NULL")
        coll.Add("TAX1", "varchar(12) NULL")
        coll.Add("TAX1_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX1_Rate", "decimal(18, 4) NULL")
        coll.Add("TAX1_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX2", "varchar(12) NULL")
        coll.Add("TAX2_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX2_Rate", "decimal(18, 4) NULL")
        coll.Add("TAX2_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX3", "varchar(12) NULL")
        coll.Add("TAX3_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX3_Rate", "decimal(18, 4) NULL")
        coll.Add("TAX3_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX4", "varchar(12) NULL")
        coll.Add("TAX4_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX4_Rate", "decimal(18, 4) NULL")
        coll.Add("TAX4_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX5", "varchar(12) NULL")
        coll.Add("TAX5_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX5_Rate", "decimal(18, 4) NULL")
        coll.Add("TAX5_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX6", "varchar(12) NULL")
        coll.Add("TAX6_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX6_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX6_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX7", "varchar(12) NULL")
        coll.Add("TAX7_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX7_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX7_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX8", "varchar(12) NULL")
        coll.Add("TAX8_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX8_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX8_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX9", "varchar(12) NULL")
        coll.Add("TAX9_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX9_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX9_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX10", "varchar(12) NULL")
        coll.Add("TAX10_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX10_Rate", "decimal(18, 2) NULL")
        coll.Add("TAX10_Amt", "decimal(18, 6) NULL")
        coll.Add("Amount", "decimal(18, 6) NULL")
        coll.Add("Disc_Amt", "decimal(18, 2) NULL")
        coll.Add("Amt_Less_Discount", "decimal(18, 6) NULL")
        coll.Add("Total_Tax_Amt", "decimal(18, 6) NULL")
        coll.Add("Item_Net_Amt", "decimal(18, 6) NULL")
        coll.Add("Price_Code", "varchar(30) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_LEAKED_SALE_RETURN_DETAIL", coll, Nothing, True, True, "TSPL_LEAKED_SALE_RETURN_HEAD", "DOCUMENT_CODE", "", True)
        coll = New Dictionary(Of String, String)()
        coll.Add("DOCUMENT_CODE", "Varchar(30) not null References TSPL_LEAKED_SALE_RETURN_HEAD(DOCUMENT_CODE)")
        coll.Add("Item_Code", "Varchar(50) Not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Unit_code", "varchar(12) Not NULL")
        coll.Add("Qty", "decimal(18, 2) NOt NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_LEAKED_SALE_RETURN_Main_Item", coll, Nothing, True, True, "TSPL_LEAKED_SALE_RETURN_HEAD", "DOCUMENT_CODE", "", True)
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.Rows.AddNew()
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
        repoICode.Name = colItemCode
        repoICode.HeaderImage = My.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)
        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colItemName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)
        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location"
        repoLocation.Name = colLocation
        repoLocation.Width = 150
        repoLocation.ReadOnly = True
        repoLocation.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoLocation)
        Dim repopriceCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repopriceCode.FormatString = ""
        repopriceCode.HeaderText = "Price Code"
        repopriceCode.Name = colPriceCode
        repopriceCode.Width = 150
        repopriceCode.ReadOnly = True
        repopriceCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repopriceCode)
        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUOM
        repoUOM.Width = 150
        repoUOM.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUOM)
        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.IsVisible = True
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoQty.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoQty)
        Dim repopriceitem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repopriceitem.FormatString = ""
        repopriceitem.HeaderText = "Price Item"
        repopriceitem.Name = colpriceitem
        repopriceitem.HeaderImage = My.Resources.search4
        repopriceitem.TextImageRelation = TextImageRelation.TextBeforeImage
        repopriceitem.Width = 150
        repopriceitem.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repopriceitem)
        Dim repoPriceItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceItemName.FormatString = ""
        repoPriceItemName.HeaderText = "Price Item Desc"
        repoPriceItemName.Name = colPriceItemName
        repoPriceItemName.Width = 150
        repoPriceItemName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoPriceItemName)
        Dim repoPriceItemUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPriceItemUOM.FormatString = ""
        repoPriceItemUOM.HeaderText = "Price Item UOM"
        repoPriceItemUOM.Name = colPriceItemUOM
        repoPriceItemUOM.Width = 100
        repoPriceItemUOM.ReadOnly = True
        repoPriceItemUOM.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPriceItemUOM)
        Dim repoFAt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFAt = New GridViewDecimalColumn()
        repoFAt.FormatString = ""
        repoFAt.HeaderText = "Fat%"
        repoFAt.Name = colFat
        repoFAt.IsVisible = True
        repoFAt.Width = 80
        repoFAt.Minimum = 0
        repoFAt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFAt.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoFAt)
        Dim repoSNF As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNF = New GridViewDecimalColumn()
        repoSNF.FormatString = ""
        repoSNF.HeaderText = "SNF%"
        repoSNF.Name = colSNF
        repoSNF.IsVisible = True
        repoSNF.Width = 80
        repoSNF.Minimum = 0
        repoSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSNF.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoSNF)
        Dim repoFatKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatKG = New GridViewDecimalColumn()
        repoFatKG.FormatString = ""
        repoFatKG.HeaderText = "FAT KG"
        repoFatKG.Name = colFatKG
        repoFatKG.IsVisible = True
        repoFatKG.Width = 80
        repoFatKG.Minimum = 0
        repoFatKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFatKG.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFatKG)
        Dim repoSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFKG = New GridViewDecimalColumn()
        repoSNFKG.FormatString = ""
        repoSNFKG.HeaderText = "SNFKG"
        repoSNFKG.Name = colSNFKG
        repoSNFKG.IsVisible = True
        repoSNFKG.Width = 80
        repoSNFKG.Minimum = 0
        repoSNFKG.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSNFKG.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSNFKG)
        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colRate
        repoRate.IsVisible = True
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRate)
        Dim repoTaxGroup As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxGroup.FormatString = ""
        repoTaxGroup.HeaderText = "Tax Group"
        repoTaxGroup.Name = colTAXGroup
        repoTaxGroup.ReadOnly = True
        repoTaxGroup.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxGroup)
        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax 1"
        repoTax1.Name = colTax1
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax1)
        Dim repoTaxBaseAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt1.FormatString = ""
        repoTaxBaseAmt1.HeaderText = "Tax Base Amount 1"
        repoTaxBaseAmt1.Name = colTaxBaseAmt1
        repoTaxBaseAmt1.ReadOnly = True
        repoTaxBaseAmt1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt1)
        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1)
        Dim repoTaxAmt1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt1 = New GridViewDecimalColumn()
        repoTaxAmt1.FormatString = ""
        repoTaxAmt1.HeaderText = "Tax Amt 1"
        repoTaxAmt1.Name = colTaxAmt1
        repoTaxAmt1.IsVisible = False
        repoTaxAmt1.ReadOnly = True
        repoTaxAmt1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt1)
        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax 2"
        repoTax2.Name = colTax2
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax2)
        Dim repoTaxBaseAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt2.FormatString = ""
        repoTaxBaseAmt2.HeaderText = "Tax Base Amount 2"
        repoTaxBaseAmt2.Name = colTaxBaseAmt2
        repoTaxBaseAmt2.ReadOnly = True
        repoTaxBaseAmt2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt2)
        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2)
        Dim repoTaxAmt2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt2 = New GridViewDecimalColumn()
        repoTaxAmt2.FormatString = ""
        repoTaxAmt2.HeaderText = "Tax Amt 2"
        repoTaxAmt2.Name = colTaxAmt2
        repoTaxAmt2.IsVisible = False
        repoTaxAmt2.ReadOnly = True
        repoTaxAmt2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt2)
        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax 3"
        repoTax3.Name = colTax3
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax3)
        Dim repoTaxBaseAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt3.FormatString = ""
        repoTaxBaseAmt3.HeaderText = "Tax Base Amount 3"
        repoTaxBaseAmt3.Name = colTaxBaseAmt3
        repoTaxBaseAmt3.ReadOnly = True
        repoTaxBaseAmt3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt3)
        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3)
        Dim repoTaxAmt3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt3 = New GridViewDecimalColumn()
        repoTaxAmt3.FormatString = ""
        repoTaxAmt3.HeaderText = "Tax Amt 3"
        repoTaxAmt3.Name = colTaxAmt3
        repoTaxAmt3.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt3)
        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax 4"
        repoTax4.Name = colTax4
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax4)
        Dim repoTaxBaseAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt4.FormatString = ""
        repoTaxBaseAmt4.HeaderText = "Tax Base Amount 4"
        repoTaxBaseAmt4.Name = colTaxBaseAmt4
        repoTaxBaseAmt4.ReadOnly = True
        repoTaxBaseAmt4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt4)
        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4)
        Dim repoTaxAmt4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt4 = New GridViewDecimalColumn()
        repoTaxAmt4.FormatString = ""
        repoTaxAmt4.HeaderText = "Tax Amt 4"
        repoTaxAmt4.Name = colTaxAmt4
        repoTaxAmt4.IsVisible = False
        repoTaxAmt4.ReadOnly = True
        repoTaxAmt4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt4)
        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax 5"
        repoTax5.Name = colTax5
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax5)
        Dim repoTaxBaseAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt5.FormatString = ""
        repoTaxBaseAmt5.HeaderText = "Tax Base Amount 5"
        repoTaxBaseAmt5.Name = colTaxBaseAmt5
        repoTaxBaseAmt5.ReadOnly = True
        repoTaxBaseAmt5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt5)
        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5)
        Dim repoTaxAmt5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt5 = New GridViewDecimalColumn()
        repoTaxAmt5.FormatString = ""
        repoTaxAmt5.HeaderText = "Tax Amt 5"
        repoTaxAmt5.Name = colTaxAmt5
        repoTaxAmt5.IsVisible = False
        repoTaxAmt3.ReadOnly = True
        repoTaxAmt5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt5)
        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax 6"
        repoTax6.Name = colTax6
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax6)
        Dim repoTaxBaseAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt6.FormatString = ""
        repoTaxBaseAmt6.HeaderText = "Tax Base Amount 6"
        repoTaxBaseAmt6.Name = colTaxBaseAmt6
        repoTaxBaseAmt6.ReadOnly = True
        repoTaxBaseAmt6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt6)
        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)
        Dim repoTaxAmt6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt6 = New GridViewDecimalColumn()
        repoTaxAmt6.FormatString = ""
        repoTaxAmt6.HeaderText = "Tax Amt 6"
        repoTaxAmt6.Name = colTaxAmt6
        repoTaxAmt6.IsVisible = False
        repoTaxAmt6.ReadOnly = True
        repoTaxAmt6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt6)
        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax 7"
        repoTax7.Name = colTax7
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax7)
        Dim repoTaxBaseAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt7.FormatString = ""
        repoTaxBaseAmt7.HeaderText = "Tax Base Amount 7"
        repoTaxBaseAmt7.Name = colTaxBaseAmt7
        repoTaxBaseAmt7.ReadOnly = True
        repoTaxBaseAmt7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt7)
        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)
        Dim repoTaxAmt7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt7 = New GridViewDecimalColumn()
        repoTaxAmt7.FormatString = ""
        repoTaxAmt7.HeaderText = "Tax Amt 7"
        repoTaxAmt7.Name = colTaxAmt7
        repoTaxAmt7.IsVisible = False
        repoTaxAmt7.ReadOnly = True
        repoTaxAmt7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt7)
        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax 8"
        repoTax8.Name = colTax8
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax8)
        Dim repoTaxBaseAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt8.FormatString = ""
        repoTaxBaseAmt8.HeaderText = "Tax Base Amount 8"
        repoTaxBaseAmt8.Name = colTaxBaseAmt8
        repoTaxBaseAmt8.ReadOnly = True
        repoTaxBaseAmt8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt8)
        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)
        Dim repoTaxAmt8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt8 = New GridViewDecimalColumn()
        repoTaxAmt8.FormatString = ""
        repoTaxAmt8.HeaderText = "Tax Amt 8"
        repoTaxAmt8.Name = colTaxAmt8
        repoTaxAmt8.IsVisible = False
        repoTaxAmt8.ReadOnly = True
        repoTaxAmt8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt8)
        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax 9"
        repoTax9.Name = colTax9
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax9)
        Dim repoTaxBaseAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt9.FormatString = ""
        repoTaxBaseAmt9.HeaderText = "Tax Base Amount 9"
        repoTaxBaseAmt9.Name = colTaxBaseAmt9
        repoTaxBaseAmt9.ReadOnly = True
        repoTaxBaseAmt9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt9)
        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)
        Dim repoTaxAmt9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt9 = New GridViewDecimalColumn()
        repoTaxAmt9.FormatString = ""
        repoTaxAmt9.HeaderText = "Tax Amt 9"
        repoTaxAmt9.Name = colTaxAmt9
        repoTaxAmt9.IsVisible = False
        repoTaxAmt9.ReadOnly = True
        repoTaxAmt9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt9)
        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax 10"
        repoTax10.Name = colTax10
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax10)
        Dim repoTaxBaseAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxBaseAmt10.FormatString = ""
        repoTaxBaseAmt10.HeaderText = "Tax Base Amount 10"
        repoTaxBaseAmt10.Name = colTaxBaseAmt10
        repoTaxBaseAmt10.ReadOnly = True
        repoTaxBaseAmt10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTaxBaseAmt10)
        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)
        Dim repoTaxAmt10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAmt10 = New GridViewDecimalColumn()
        repoTaxAmt10.FormatString = ""
        repoTaxAmt10.HeaderText = "Tax Amt 10"
        repoTaxAmt10.Name = colTaxAmt10
        repoTaxAmt10.IsVisible = False
        repoTaxAmt10.ReadOnly = True
        repoTaxAmt10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxAmt10)
        For ii As Integer = 1 To 10
            Dim repoCheckBox As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            repoCheckBox.HeaderText = "Is Tax On Base Amount " + clsCommon.myCstr(ii)
            repoCheckBox.Name = colIsTaxOnBaseAmount + clsCommon.myCstr(ii)
            repoCheckBox.ReadOnly = True
            repoCheckBox.IsVisible = False
            repoCheckBox.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
            repoCheckBox.WrapText = True
            gv1.MasterTemplate.Columns.Add(repoCheckBox)
        Next
        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.IsVisible = False
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)
        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmount
        repoAmount.IsVisible = True
        repoAmount.Width = 100
        repoAmount.Minimum = 0
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmount.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmount)
        Dim repoDisAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDisAmt = New GridViewDecimalColumn()
        repoDisAmt.FormatString = ""
        repoDisAmt.HeaderText = "Discount Amount"
        repoDisAmt.WrapText = True
        repoDisAmt.Name = colDisAmt
        repoDisAmt.Width = 80
        repoDisAmt.IsVisible = False
        repoDisAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoDisAmt.VisibleInColumnChooser = False
        repoDisAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDisAmt)
        Dim repoAmtAfterDis As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterDis = New GridViewDecimalColumn()
        repoAmtAfterDis.FormatString = ""
        repoAmtAfterDis.HeaderText = "Amount After Discount"
        repoAmtAfterDis.Name = colAmtAfterDis
        repoAmtAfterDis.WrapText = True
        repoAmtAfterDis.Width = 80
        repoAmtAfterDis.IsVisible = False
        repoAmtAfterDis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterDis.VisibleInColumnChooser = False
        repoAmtAfterDis.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterDis)
        Dim repoItemNetAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemNetAmt = New GridViewDecimalColumn()
        repoItemNetAmt.FormatString = ""
        repoItemNetAmt.HeaderText = "Item Net Amount"
        repoItemNetAmt.Name = colItem_Net_Amt
        repoItemNetAmt.WrapText = True
        repoItemNetAmt.Width = 80
        repoItemNetAmt.IsVisible = False
        repoItemNetAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoItemNetAmt.VisibleInColumnChooser = False
        repoItemNetAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemNetAmt)
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Sub LoadBlankGridMainItem()
        gvMainItem.Rows.Clear()
        gvMainItem.Columns.Clear()
        gvMainItem.DataSource = Nothing
        gvMainItem.Rows.AddNew()
        Dim repoapICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoapICode.FormatString = ""
        repoapICode.HeaderText = "Item Code"
        repoapICode.Name = colAPItem
        repoapICode.HeaderImage = My.Resources.search4
        repoapICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoapICode.Width = 100
        gvMainItem.MasterTemplate.Columns.Add(repoapICode)
        Dim repoapIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoapIName.FormatString = ""
        repoapIName.HeaderText = "Item Description"
        repoapIName.Name = colAPitemName
        repoapIName.Width = 150
        repoapIName.ReadOnly = True
        gvMainItem.MasterTemplate.Columns.Add(repoapIName)
        Dim repomainUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repomainUOM = New GridViewTextBoxColumn()
        repomainUOM.FormatString = ""
        repomainUOM.HeaderText = "UOM"
        repomainUOM.Name = colAPUOM
        repomainUOM.Width = 150
        repomainUOM.ReadOnly = False
        gvMainItem.MasterTemplate.Columns.Add(repomainUOM)
        Dim repoapQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoapQty = New GridViewDecimalColumn()
        repoapQty.FormatString = ""
        repoapQty.HeaderText = "Quantity"
        repoapQty.Name = colAPQty
        repoapQty.IsVisible = True
        repoapQty.Minimum = 0
        repoapQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoapQty.ReadOnly = False
        gvMainItem.MasterTemplate.Columns.Add(repoapQty)
        gvMainItem.AllowDeleteRow = True
        gvMainItem.AllowAddNewRow = False
        gvMainItem.ShowGroupPanel = False
        gvMainItem.AllowColumnReorder = False
        gvMainItem.AllowRowReorder = False
        gvMainItem.EnableSorting = False
        gvMainItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvMainItem.MasterTemplate.ShowRowHeaderColumn = False
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
        repoTaxAmt.ReadOnly = False
        repoTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAmt)
        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Public Function AllowtoSave() As Boolean
        Try
            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                Throw New Exception("Please Select Route No.")
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please Select Location")
            End If
            If clsCommon.myLen(txtCustomer.Value) <= 0 Then
                Throw New Exception("Please Select Customer Code.")
            End If
            Dim dblQuantity As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If (clsCommon.CompairString(gv1.Rows(ii).Cells(colItemCode).Value, Nothing) = CompairStringResult.Equal) Then
                    If gv1.Rows.Count > 1 Then
                        Continue For
                    Else
                        Throw New Exception("Please Fill atleast one Item")
                    End If
                    Continue For
                End If
                Dim taxGroup As String = txtTaxGroup.Value
                If clsCommon.myLen(txtTaxGroup) <= 0 Then
                    Throw New Exception("TaxGoup not Found!")
                End If
                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)) <= 0 Then
                    Throw New Exception("Please enter Item At Line No" + clsCommon.myCstr(ii + 1))
                End If
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
                Dim strPriceitemCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colpriceitem).Value)
                Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemName).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim dblrate As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUOM).Value)
                dblQuantity = dblQuantity + dblQty
                If (clsCommon.myLen(strICode) > 0) Then
                    If clsCommon.myLen(strUOM) <= 0 Then
                        common.clsCommon.MyMessageBoxShow(Me, "Please enter UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                        Return False
                    End If
                End If
                If dblQty <= 0 Then
                    Throw New Exception("Please enter Qty At Line No" + clsCommon.myCstr(ii + 1))
                End If
                If clsCommon.myLen(strPriceitemCode) <= 0 Then
                    Throw New Exception("Please Select price Item At Line No" + clsCommon.myCstr(ii + 1))
                End If
                If dblrate <= 0 Then
                    Throw New Exception("Item rate not found! At Line No" + clsCommon.myCstr(ii + 1))
                End If
                For jj As Integer = ii + 1 To gv1.Rows.Count - 1
                    Dim innerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colItemCode).Value)
                    Dim innerPriceitemCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colpriceitem).Value)
                    Dim innerrIName As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colItemName).Value)
                    If clsCommon.CompairString(strICode, innerICode) = CompairStringResult.Equal Then
                        Throw New Exception("Same Item found At Line No" + clsCommon.myCstr(jj + 1))
                    End If
                Next
            Next
            For introw As Integer = 0 To gvMainItem.Rows.Count - 1
                If (clsCommon.CompairString(gvMainItem.Rows(introw).Cells(colAPItem).Value, Nothing) = CompairStringResult.Equal) Then
                    If gvMainItem.Rows.Count > 1 Then
                        Continue For
                    Else
                        Throw New Exception("Please Fill atleast one main item")
                    End If
                End If
                If clsCommon.myLen(clsCommon.myCstr(gvMainItem.Rows(introw).Cells(colAPItem).Value)) <= 0 Then
                    Throw New Exception("Please enter Item At Line No" + clsCommon.myCstr(introw + 1))
                End If
                Dim MianitemCode As String = clsCommon.myCstr(gvMainItem.Rows(introw).Cells(colAPItem).Value)
                Dim MianitemName As String = clsCommon.myCstr(gvMainItem.Rows(introw).Cells(colAPitemName).Value)
                Dim MianitemUOM As String = clsCommon.myCstr(gvMainItem.Rows(introw).Cells(colAPUOM).Value)
                Dim MianitemQty As Double = clsCommon.myCdbl(gvMainItem.Rows(introw).Cells(colAPQty).Value)
                If (clsCommon.myLen(MianitemCode) > 0) Then
                    If clsCommon.myLen(MianitemUOM) <= 0 Then
                        Throw New Exception("Please enter Main Item UOM for " + MianitemName + ". At Line No" + clsCommon.myCstr(introw + 1))
                    End If
                End If
                If MianitemQty <= 0 Then
                    Throw New Exception("Please enter Main item Qty At Line No" + clsCommon.myCstr(introw + 1))
                End If
                If clsCommon.myLen(MianitemCode) <= 0 Then
                    Throw New Exception("Please Select Main Item At Line No" + clsCommon.myCstr(introw + 1))
                End If
                For jj As Integer = introw + 1 To gv1.Rows.Count - 1
                    Dim innerMainICode As String = clsCommon.myCstr(gvMainItem.Rows(jj).Cells(colAPItem).Value)
                    Dim innerMainIName As String = clsCommon.myCstr(gvMainItem.Rows(jj).Cells(colAPitemName).Value)
                    Dim indblQty As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colQty).Value)
                    If clsCommon.CompairString(MianitemCode, innerMainICode) = CompairStringResult.Equal Then
                        Throw New Exception("Same Main Item found At Line No" + clsCommon.myCstr(jj + 1))
                    End If
                Next
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If AllowtoSave() Then
                SaveData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SaveData()
        Try
            Dim obj As New clsLeakedSaleReturnHead()
            If Not isNewEntry Then
                obj.Document_Code = txtDocNo.Value
            End If
            obj.Document_Date = txtDate.Value
            obj.Route_No = txtRouteNo.Value
            obj.Location_Code = txtLocation.Value
            obj.Customer_Code = txtCustomer.Value
            obj.Remarks = txtRemarks.Text
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
            obj.Discount_Base = clsCommon.myCdbl(lblAmtWithDiscount.Text)
            obj.Discount_Amt = clsCommon.myCdbl(lblDiscountAmt.Text)
            obj.Amount_Less_Discount = clsCommon.myCdbl(lblAmtAfterDiscount.Text)
            obj.Total_Amt = clsCommon.myCdbl(lblTotRAmt.Text)
            Dim LineNo As Decimal = 1
            obj.Arr = New List(Of clsLeakedSaleReturnDetail)
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsLeakedSaleReturnDetail()
                objTr.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                objTr.Price_Item = clsCommon.myCstr(grow.Cells(colpriceitem).Value)
                objTr.Price_Item_UOM = clsCommon.myCstr(grow.Cells(colPriceItemUOM).Value)
                objTr.FAt_Per = clsCommon.myCdbl(grow.Cells(colFat).Value)
                objTr.SNF_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                objTr.FAT_KG = clsCommon.myCdbl(grow.Cells(colFatKG).Value)
                objTr.SNF_KG = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                objTr.Location = clsCommon.myCstr(grow.Cells(colLocation).Value)
                objTr.TaxGroup = clsCommon.myCstr(grow.Cells(colTAXGroup).Value)
                objTr.Item_Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                objTr.TAX1 = clsCommon.myCstr(grow.Cells(colTax1).Value)
                objTr.TAX1_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt1).Value)
                objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                objTr.TAX1_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt1).Value)
                objTr.TAX2 = clsCommon.myCstr(grow.Cells(colTax2).Value)
                objTr.TAX2_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt2).Value)
                objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                objTr.TAX2_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt2).Value)
                objTr.TAX3 = clsCommon.myCstr(grow.Cells(colTax3).Value)
                objTr.TAX3_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt3).Value)
                objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                objTr.TAX3_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt3).Value)
                objTr.TAX4 = clsCommon.myCstr(grow.Cells(colTax4).Value)
                objTr.TAX4_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt4).Value)
                objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                objTr.TAX4_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt4).Value)
                objTr.TAX5 = clsCommon.myCstr(grow.Cells(colTax5).Value)
                objTr.TAX5_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt5).Value)
                objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                objTr.TAX5_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt5).Value)
                objTr.TAX6 = clsCommon.myCstr(grow.Cells(colTax6).Value)
                objTr.TAX6_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt6).Value)
                objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                objTr.TAX6_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt6).Value)
                objTr.TAX7 = clsCommon.myCstr(grow.Cells(colTax7).Value)
                objTr.TAX7_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt7).Value)
                objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                objTr.TAX7_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt7).Value)
                objTr.TAX8 = clsCommon.myCstr(grow.Cells(colTax8).Value)
                objTr.TAX8_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt8).Value)
                objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                objTr.TAX8_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt8).Value)
                objTr.TAX9 = clsCommon.myCstr(grow.Cells(colTax9).Value)
                objTr.TAX9_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt9).Value)
                objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                objTr.TAX9_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt9).Value)
                objTr.TAX10 = clsCommon.myCstr(grow.Cells(colTax10).Value)
                objTr.TAX10_Base_Amt = clsCommon.myCdbl(grow.Cells(colTaxBaseAmt10).Value)
                objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                objTr.TAX10_Amt = clsCommon.myCdbl(grow.Cells(colTaxAmt10).Value)
                objTr.Total_Tax_Amt = clsCommon.myCdbl(grow.Cells(colTotTaxAmt).Value)
                objTr.Amt_Less_Discount = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)
                objTr.Item_Net_Amt = clsCommon.myCdbl(grow.Cells(colAmtAfterDis).Value)
                objTr.Price_Code = clsCommon.myCstr(grow.Cells(colPriceCode).Value)
                If clsCommon.myLen(objTr.Item_Code) > 0 Then
                    obj.Arr.Add(objTr)
                End If
            Next
            obj.ArrMainItem = New List(Of clsLeakedSaleReturnMainItem)
            For Each grow As GridViewRowInfo In gvMainItem.Rows
                Dim objMTr As New clsLeakedSaleReturnMainItem()
                objMTr.Item_Code = clsCommon.myCstr(grow.Cells(colAPItem).Value)
                objMTr.Unit_Code = clsCommon.myCstr(grow.Cells(colAPUOM).Value)
                objMTr.Qty = clsCommon.myCdbl(grow.Cells(colAPQty).Value)
                If clsCommon.myLen(objMTr.Item_Code) > 0 Then
                    obj.ArrMainItem.Add(objMTr)
                End If
            Next
            If clsLeakedSaleReturnHead.SaveData(obj, isNewEntry) Then
                clsCommon.MyMessageBoxShow(Me, "Saved Successfully")
                LoadData(obj.Document_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub LoadData(ByVal strDoc As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As New clsLeakedSaleReturnHead()
            obj = clsLeakedSaleReturnHead.GetData(strDoc, NavType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                AddNew()
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isInsideLoadData = True
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If
                txtDocNo.Value = obj.Document_Code
                txtRouteNo.Value = obj.Route_No
                lblRouteDesc.Text = obj.Route_Desc
                txtLocation.Value = obj.Location_Code
                txtCustomer.Value = obj.Customer_Code
                lblCustomerName.Text = obj.Customer_Name
                txtRemarks.Text = obj.Remarks
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
                                Exit For
                            End If
                        Next
                    End If
                End If

                lblAmtAfterDiscount.Text = obj.Amount_Less_Discount
                lblAmtWithDiscount.Text = obj.Discount_Base
                lblInvoiceDiscAmt.Text = ""
                lblDiscountAmt.Text = obj.Discount_Amt
                lblTaxAmt.Text = obj.Total_Tax_Amt
                lblTotRAmt.Text = obj.Total_Amt
                TxtRoundoff.Text = ""

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsLeakedSaleReturnDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colpriceitem).Value = objTr.Price_Item
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceItemName).Value = objTr.Price_Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceItemUOM).Value = objTr.Price_Item_UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFat).Value = objTr.FAt_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = objTr.SNF_Per
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value = objTr.FAT_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = objTr.SNF_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterDis).Value = objTr.Amt_Less_Discount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDisAmt).Value = objTr.Disc_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objTr.Total_Tax_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Net_Amt).Value = objTr.Item_Net_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLocation).Value = objTr.Location
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAXGroup).Value = objTr.TaxGroup
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = objTr.TAX1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt1).Value = objTr.TAX1_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = objTr.TAX1_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = objTr.TAX1_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = objTr.TAX2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt2).Value = objTr.TAX2_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = objTr.TAX2_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = objTr.TAX2_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = objTr.TAX3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt3).Value = objTr.TAX3_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = objTr.TAX3_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = objTr.TAX3_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = objTr.TAX4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt4).Value = objTr.TAX4_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = objTr.TAX4_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = objTr.TAX4_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = objTr.TAX5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt5).Value = objTr.TAX5_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = objTr.TAX5_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = objTr.TAX5_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = objTr.TAX6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt6).Value = objTr.TAX6_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = objTr.TAX6_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = objTr.TAX6_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = objTr.TAX7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt7).Value = objTr.TAX7_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = objTr.TAX7_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = objTr.TAX7_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = objTr.TAX8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt8).Value = objTr.TAX8_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = objTr.TAX8_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = objTr.TAX8_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = objTr.TAX9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt9).Value = objTr.TAX9_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = objTr.TAX9_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = objTr.TAX9_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = objTr.TAX10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt10).Value = objTr.TAX10_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = objTr.TAX10_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = objTr.TAX10_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = objTr.Price_Code
                        gv1.Rows.AddNew()
                    Next

                End If
                If obj.ArrMainItem IsNot Nothing AndAlso obj.ArrMainItem.Count > 0 Then
                    For Each objMTr As clsLeakedSaleReturnMainItem In obj.ArrMainItem
                        gvMainItem.Rows(gvMainItem.Rows.Count - 1).Cells(colAPItem).Value = objMTr.Item_Code
                        gvMainItem.Rows(gvMainItem.Rows.Count - 1).Cells(colAPitemName).Value = objMTr.Item_Desc
                        gvMainItem.Rows(gvMainItem.Rows.Count - 1).Cells(colAPUOM).Value = objMTr.Unit_Code
                        gvMainItem.Rows(gvMainItem.Rows.Count - 1).Cells(colAPQty).Value = objMTr.Qty
                        gvMainItem.Rows.AddNew()
                    Next
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If (myMessages.postConfirm()) Then
                PostData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub PostData()
        Try
            If (clsLeakedSaleReturnHead.PostData(txtDocNo.Value)) Then
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted ", Me.Text)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If (myMessages.deleteConfirm()) Then
                    DeleteData()
                End If
            Else
                Throw New Exception("Please Select Document")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
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
            If (clsLeakedSaleReturnHead.DeleteData(txtDocNo.Value)) Then
                saveCancelLog(Reason, "Delete", Nothing)
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
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
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim strQry As String = ""
            strQry = "select count(*) from TSPL_LEAKED_SALE_RETURN_HEAD where Document_Code='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim strwherecls As String = ""
            strwherecls = Xtra.CustomerPermission()
            Dim strDONo As String = Nothing
            Dim qry As String = "select TSPL_LEAKED_SALE_RETURN_HEAD.Document_Code as Code,TSPL_LEAKED_SALE_RETURN_HEAD.Location_code,TSPL_LEAKED_SALE_RETURN_HEAD.Document_Date,TSPL_LEAKED_SALE_RETURN_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_name,TSPL_LEAKED_SALE_RETURN_HEAD.Route_No from TSPL_LEAKED_SALE_RETURN_HEAD
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_LEAKED_SALE_RETURN_HEAD.Customer_Code "
            Dim whrClas As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
                whrClas = " TSPL_LEAKED_SALE_RETURN_HEAD.Location_code in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SHIPMENT_HEAD.Customer_Code in (" + strwherecls + ") "
            ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas = " TSPL_LEAKED_SALE_RETURN_HEAD.Location_code in (" + objCommonVar.strCurrUserLocations + ") "
            ElseIf clsCommon.myLen(strwherecls) > 0 Then
                whrClas = " TSPL_LEAKED_SALE_RETURN_HEAD.Customer_Code in (" + strwherecls + ") "
            End If
            LoadData(clsCommon.ShowSelectForm("LeakedSaleReturnDoc", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked, "TSPL_LEAKED_SALE_RETURN_HEAD.Document_Date"), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Try
            Dim qry As String = "Select Route_No as Code,Route_Desc as Description,Type,Employee_Code as 'Employee Code',Off_Day as 'Off Day' from TSPL_ROUTE_MASTER"
            txtRouteNo.Value = clsCommon.ShowSelectForm("LSRSc", qry, "Code", "", txtRouteNo.Value, "", isButtonClicked)
            lblRouteDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + txtRouteNo.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
            Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomer._MYValidating
        Try
            Dim Whrcls As String = "  TSPL_CUSTOMER_MASTER.Status='N' "
            Dim strQry As String = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER.Alies_Name as [Short Name],TSPL_CUSTOMER_MASTER.Route_No,TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],isnull(TSPL_CUSTOMER_MASTER.Customer_Category,'') as [Customer Category],tspl_customer_master.Cust_Group_Code as [Customer Group Code] 
from TSPL_CUSTOMER_MASTER "
            txtCustomer.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", strQry, "Code", Whrcls, txtCustomer.Value, "Code", isButtonClicked)
            lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomer.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetTax(ByVal Item_Code As String, ByVal intRow As Integer)
        GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
        If GSTStatus = False OrElse (rbtnTaxable.IsChecked AndAlso GSTStatus) Then
            If CalculateTaxRatefromItemwsieTaxOnSale Then
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    Dim strTaxType As String = clsLocationWiseTax.TaxType(txtLocation.Value, txtCustomer.Value, "S", txtDate.Value, Nothing)
                    If GSTStatus = True AndAlso clsCommon.CompairString(strTaxType, "L") = CompairStringResult.Equal Then
                        txtTaxGroup.Value = clsItemWiseTaxAuthority.GetTaxGroupItemWise("L", "S", txtDate.Value, Item_Code)
                    Else
                        txtTaxGroup.Value = clsItemWiseTaxAuthority.GetTaxGroupItemWise("I", "S", txtDate.Value, Item_Code)
                    End If
                Else
                    txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtLocation.Value, txtCustomer.Value, "S", txtDate.Value)
                End If
            Else
                txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtLocation.Value, txtCustomer.Value, "S", txtDate.Value)
                lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
            End If
        Else
            txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, txtLocation.Value, txtCustomer.Value, "S", txtDate.Value)
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
        End If
        'SetTaxDetails(Item_Code, intRow)
    End Sub
    Sub SetTaxDetails(ByVal ICode As String, ByVal intRow As Integer)
        LoadBlankGridTax()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtCustomer.Value, txtLocation.Value)
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
                If rbtnTaxCalAutomatic.IsChecked Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                    Else
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
                    End If
                End If
            Next
            SetitemWiseTaxSetting(True, ICode, intRow)
        Else
            lblTaxGrpName.Text = ""
            For ii As Integer = 0 To gv1.Rows.Count - 1
                BlankTaxDetails(ii, False)
            Next
        End If
        UpdateCurrentRow(intRow)
        UpdateAllTotals()
    End Sub
    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal ICode As String, ByVal intRowNo As Integer)
        Dim strCustomer As String = ""
        Try
            strCustomer = clsCommon.myCstr(txtCustomer.Value)
        Catch ex As Exception
        End Try
        If clsCommon.myLen(strCustomer) <= 0 Then
            strCustomer = txtCustomer.Value
        End If
        Dim IsTaxable As Integer = 0
        Dim dt As DataTable = clsTaxCalculation.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtCustomer.Value, txtLocation.Value, ICode, clsCommon.GetPrintDate(txtDate.Value))
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'For intRowNo As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(intRowNo).Cells(colpriceitem).Value) > 0) Then
                BlankTaxDetails(intRowNo, isChangeRate)
                IsTaxable = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colpriceitem).Value) & "'"))
                If ((clsCommon.myLen(gv1.Rows(intRowNo).Cells(colpriceitem).Value) > 0 And IsTaxable = 1) OrElse (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code IN (select Tax_Code  from TSPL_TAX_GROUP_DETAILS WHERE TAX_GROUP_CODE='" & txtTaxGroup.Value & "') AND Is_TCS ='Y'")) > 0)) Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & txtCustomer.Value & "'")), "0") = CompairStringResult.Equal Then
                            Else
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = 0
                            End If
                        End If
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr(colIsTaxOnBaseAmount + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Tax_On_Base_Amount")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            End If
            ' Next
        End If
    End Sub
    Public Sub ItemPrice(ByVal strItem As String, ByVal intQty As Decimal, ByVal introw As Integer, ByVal Fat_per As Double, ByVal Snf_Per As Double)
        Dim dt As New DataTable()
        Dim dblRate As Double = 0
        Dim dblTotal As Double = 0
        Dim dblItemBasicPrice As Double = 0
        Dim whrcls As String = ""
        Dim qry As String = ""
        'Price_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select price_CodeNon from tspl_customer_master where cust_code='" & txtCustomer.Value & "'"))
        'whrcls = " and TSPL_ITEM_PRICE_MASTER.Price_Code='" & Price_code & "' and TSPL_ITEM_PRICE_MASTER.Is_For_Price=0"
        'qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
        '" XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
        '"  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
        '" XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
        '" XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
        '" XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
        '" XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
        '"Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
        '"Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
        '"Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
        '"TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
        '" TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
        '" TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
        '" TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
        '" TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
        '"TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
        '"TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  " & whrcls & "  " &
        '" and UOM='" & strUnit & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & strItem & "'  AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
        '") XXXE WHERE RowNo=1  "
        Dim strPriceCode As String = ""
        dblRate = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(intQty, strPriceCode, Fat_per, Snf_Per, "", "", "M", txtDate.Value, Nothing, "M")
        'dt = clsDBFuncationality.GetDataTable(qry)
        'If dt.Rows.Count > 0 Then
        '    dblRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
        '    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0).Item("Is_With_Tax")), "N") = CompairStringResult.Equal Then
        '        dblItemBasicPrice = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price")) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX1_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX2_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX3_Amt")), 2) + Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("TAX4_Amt")), 2), 2)
        '    Else
        '        dblItemBasicPrice = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
        '    End If
        '    If dblRate = 0 Then
        '        Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(gv1.CurrentRow.Cells(colpriceitem).Value) & ".")
        '        Exit Sub
        '    End If
        'Else
        '    Throw New Exception("Please create Price chart for customer " & txtCustomer.Value & " for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(gv1.CurrentRow.Cells(colPriceItemName).Value) & ".")
        'End If
        Dim tax As Double = 0
        Dim tax_on_amt As Decimal = 0
        'dt = clsDBFuncationality.GetDataTable(qry)
        If dblRate > 0 Then
            gv1.Rows(introw).Cells(colRate).Value = dblRate 'clsCommon.myCdbl(dt.Rows(0).Item("Item_Selling_Price"))
            gv1.Rows(introw).Cells(colAmount).Value = dblRate * intQty
            gv1.Rows(introw).Cells(colTAXGroup).Value = txtTaxGroup.Value
            gv1.Rows(introw).Cells(colPriceCode).Value = strPriceCode
            UpdateCurrentRow(introw)
            UpdateAllTotals()
        Else
            Throw New Exception("Please create Price chart for customer " & clsCommon.myCstr(txtCustomer.Value) & " for Location " & clsCommon.myCstr(txtLocation.Value) & "  for item " & gv1.Rows(introw).Cells(colpriceitem).Value & ".")
        End If
        'Return dblRate
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            Dim arrTaxableAuth As New List(Of String)
            Dim arrTaxableAuth1 As New List(Of String)
            Dim dblFAmt As Double = 0
            GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            If GSTStatus = False Then
                strExcise = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Excisable from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'")) = "T", True, False)
            End If
            Dim dblAlterQty As Double = 0
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colpriceitem).Value)
            Dim strUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colPriceItemUOM).Value)
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
            Dim dblBasicRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
            Dim wt_unit As String = 0
            Dim wt_qty As Double = 0
            Dim Item_Weight As Double = 0
            Dim TotalItem_Weight As Double = 0
            Dim TotalItem_WeightMetric As Double = 0
            Dim strOrgUnit As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colPriceItemUOM).Value)
            Dim dblBasicAmt As Double = dblQty * dblRate
            Dim dblAmt As Double = (dblQty * dblRate)
            dblAmt = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmount).Value)
            ''''' to calculate customer disc
            Dim dt As New DataTable
            Dim dblOrderQty As Double = 0
            Dim dblCustDiscQty As Double = 0
            Dim dblCustDiscAmt As Double = 0
            Dim dblCustDiscPercentage As Double = 0
            Dim dblApplyCustDisc As Double = 0
            Dim dblTotCustDisc As Double = 0
            Dim dblTotalDCAmt As Double = 0
            Dim dblTotalTCAmt As Double = 0
            Dim dblTotTaxRate As Double = GetCurrentRowTotalTaxRate(IntRowNo)
            'Dim dblDisPer As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colDisPer).Value)
            Dim dblDisAmt As Double = 0
            Dim dblTotDiscAmt As Double = 0
            Dim dblAmtAfterDis As Double = 0
            dblTotDiscAmt = dblDisAmt
            dblAmtAfterDis = dblAmt - dblDisAmt
            For ii As Integer = 1 To 10
                Dim Strii As String = clsCommon.myCstr(ii)
                If rbtnTaxCalAutomatic.IsChecked Then
                    Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                        Dim IsTaxonBaseAmount As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(colIsTaxOnBaseAmount + clsCommon.myCstr(ii)).Value)
                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0
                        Dim dblOtherTaxAmt As Double = 0
                        If Not IsTaxonBaseAmount AndAlso clsCommon.CompairString(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value, "TCS") <> CompairStringResult.Equal Then
                            dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                        ElseIf Not IsTaxonBaseAmount AndAlso clsCommon.CompairString(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value, "TCS") = CompairStringResult.Equal Then
                            dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth1)
                        End If
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 3)
                        dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, 6)
                        If (Not arrTaxableAuth.Contains(strTaxCode.ToUpper())) AndAlso (clsCommon.CompairString(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value, "CGST") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value, "SGST") <> CompairStringResult.Equal) Then
                            arrTaxableAuth.Add(strTaxCode.ToUpper())
                        End If
                        If (Not arrTaxableAuth1.Contains(strTaxCode.ToUpper())) Then
                            arrTaxableAuth1.Add(strTaxCode.ToUpper())
                        End If
                    Else
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + Strii)).Value = Nothing
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Nothing
                    End If
                End If
            Next
            Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
            Dim dblAmtAfterTax As Double = 0
            dblAmtAfterTax = dblAmtAfterDis + dblTotTaxAmt
            gv1.Rows(IntRowNo).Cells(colDisAmt).Value = Math.Round(dblDisAmt, 2)
            gv1.Rows(IntRowNo).Cells(colAmtAfterDis).Value = Math.Round(dblAmtAfterDis, 2)
            gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = Math.Round(dblTotTaxAmt, 2)
            gv1.Rows(IntRowNo).Cells(colRate).Value = dblRate
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub UpdateAllTotals()
        Try
            Dim dblCashDisAmt As Double = 0
            Dim dblVolumeSlabCashDisAmt As Double = 0
            Dim dblHeadDisPerAmt As Double = 0
            Dim dblCommAmt As Double = 0
            Dim dblTotalTcsAmt As Decimal = 0
            Dim dblTCAmt As Double = 0
            Dim dblSCAmt As Double = 0
            Dim dblBoothSCAmt As Double = 0
            Dim dblTotAmt As Double = 0
            Dim dblTotDisAmt As Double = 0
            Dim dblAmtAfterDis As Double = 0
            Dim dblTotLandedCost As Double = 0
            Dim dblHeadDisAmt As Double = 0
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
            Dim dblTotalWtMetric As Double = 0
            Dim dblTotalWt As Double = 0
            Dim dblTotalDCAmt As Double = 0
            Dim dblTaxTotAmt As Double = 0
            Dim dblNetAmt As Double = 0
            Dim dblCrateQty As Double = 0
            Dim dblCanQty As Double = 0
            Dim dblTotalQtyinKG As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If (clsCommon.myLen(gv1.Rows(ii).Cells(colpriceitem).Value) > 0) Then
                    dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)
                    dblTotDisAmt = dblTotDisAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colDisAmt).Value)
                    dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
                    dblTaxAmt1 = dblTaxAmt1 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt1).Value), 2)
                    dblTaxAmt2 = dblTaxAmt2 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt2).Value), 2)
                    dblTaxAmt3 = dblTaxAmt3 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt3).Value), 2)
                    dblTaxAmt4 = dblTaxAmt4 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt4).Value), 2)
                    dblTaxAmt5 = dblTaxAmt5 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt5).Value), 2)
                    dblTaxAmt6 = dblTaxAmt6 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt6).Value), 2)
                    dblTaxAmt7 = dblTaxAmt7 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt7).Value), 2)
                    dblTaxAmt8 = dblTaxAmt8 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt8).Value), 2)
                    dblTaxAmt9 = dblTaxAmt9 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt9).Value), 2)
                    dblTaxAmt10 = dblTaxAmt10 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt10).Value), 2)
                    dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt1).Value)
                    dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt2).Value)
                    dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt3).Value)
                    dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt4).Value)
                    dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt5).Value)
                    dblTaxBaseAmt6 = dblTaxBaseAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt6).Value)
                    dblTaxBaseAmt7 = dblTaxBaseAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt7).Value)
                    dblTaxBaseAmt8 = dblTaxBaseAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt8).Value)
                    dblTaxBaseAmt9 = dblTaxBaseAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt9).Value)
                    dblTaxBaseAmt10 = dblTaxBaseAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt10).Value)
                    dblTaxTotAmt = dblTaxTotAmt + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value), 2)
                    dblNetAmt = dblAmtAfterDis + dblTaxTotAmt
                End If
            Next
            If rbtnTaxCalAutomatic.IsChecked Then
                For ii As Integer = 1 To gv2.Rows.Count
                    Select Case (ii)
                        Case 1
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt1, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt1, 2)
                            If dblTaxBaseAmt1 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt1 * 100) / dblTaxBaseAmt1, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 2
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                            If dblTaxBaseAmt2 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 3
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                            If dblTaxBaseAmt3 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 4
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                            If dblTaxBaseAmt4 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 5
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                            If dblTaxBaseAmt5 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 6
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt6, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt6, 2)
                            If dblTaxBaseAmt6 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 7
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                            If dblTaxBaseAmt7 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 8
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                            If dblTaxBaseAmt8 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 9
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                            If dblTaxBaseAmt9 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                        Case 10
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                            If dblTaxBaseAmt10 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 3)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                    End Select
                Next
            End If
            Dim dblACAmount As Double = 0
            lblAmtWithDiscount.Text = clsCommon.myFormat(dblTotAmt)
            lblDiscountAmt.Text = clsCommon.myFormat(dblTotDisAmt + dblCashDisAmt + dblVolumeSlabCashDisAmt)
            lblAmtAfterDiscount.Text = clsCommon.myFormat(dblAmtAfterDis)
            lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)
            'lblAddCharges1.Text = clsCommon.myFormat(dblACAmount)
            dblNetAmt = dblNetAmt + dblACAmount
            lblInvoiceDiscAmt.Text = dblHeadDisAmt + dblHeadDisPerAmt
            lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
            'If ApplyRoundOffZero Then
            '    If Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0) > clsCommon.myCdbl(lblTotRAmt.Text) Then
            '        TxtRoundoff.Text = Math.Round(Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0) - clsCommon.myCdbl(lblTotRAmt.Text), 2)
            '        lblTotRAmt.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0)
            '    Else
            '        TxtRoundoff.Text = Math.Round(Math.Round(clsCommon.myCdbl(lblTotRAmt.Text)) - clsCommon.myCdbl(lblTotRAmt.Text), 2)
            '        lblTotRAmt.Text = Math.Round(clsCommon.myCdbl(lblTotRAmt.Text), 0)
            '    End If
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If Not clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value), "TCS") = CompairStringResult.Equal Then
                If IntRowNo < 0 Then
                    dblTotTax = dblTotTax + Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value), 2)
                Else
                    dblTotTax = dblTotTax + Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXAMT" + strii)).Value), 2)
                End If
            End If
        Next
        Return dblTotTax
    End Function
    Private Function GetCurrentRowTotalTaxRate(ByVal IntRowNo As Integer) As Double
        Dim dblTotRate As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If Not clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value), "TCS") = CompairStringResult.Equal Then
                If IntRowNo < 0 Then
                    dblTotRate = dblTotRate + clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strii)).Value)
                Else
                    dblTotRate = dblTotRate + clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxRate" + strii)).Value)
                End If
            End If
        Next
        Return dblTotRate
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
    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colItemCode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentColumn Is gv1.Columns(colQty) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.CurrentColumn = gv1.Columns(colpriceitem)
            End If
        End If
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
    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
        'RefreshReqNo()
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub gv1_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colItemCode) OrElse e.Column Is gv1.Columns(colUOM) OrElse e.Column Is gv1.Columns(colpriceitem) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) OrElse e.Column Is gv1.Columns(colSNF) OrElse e.Column Is gv1.Columns(colFat) Then
                        If e.Column Is gv1.Columns(colItemCode) Then
                            If clsCommon.myLen(txtCustomer.Value) <= 0 Then
                                gv1.CurrentRow.Cells(colItemCode).Value = ""
                                Throw New Exception("Please select Customer First")
                            End If
                            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                                gv1.CurrentRow.Cells(colItemCode).Value = ""
                                Throw New Exception("Please select Location First")
                            End If
                            OpenRawItemList(False)
                            'Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
                            'Dim strIUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUOM).Value)
                            'ItemPrice(strICode, strIUOM, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index, False)
                            'SetTax(strICode, gv1.CurrentRow.Index)
                        End If
                        If e.Column Is gv1.Columns(colUOM) Then
                            Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
                            If clsCommon.myLen(strICode) > 0 Then
                                OpenRawitemUOMList(False)
                            Else
                                gv1.CurrentRow.Cells(colUOM).Value = ""
                                Throw New Exception("Please fill item first.")
                            End If
                        End If
                        If e.Column Is gv1.Columns(colpriceitem) Then
                            Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
                            Dim strUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUOM).Value)
                            Dim strQty As Double = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
                            If clsCommon.myLen(strICode) > 0 AndAlso clsCommon.myLen(strUOM) > 0 AndAlso strQty > 0 Then
                                OpenItemList(False)
                                Dim strPICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colpriceitem).Value)
                                Dim strPIUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colPriceItemUOM).Value)
                                ItemPrice(strPICode, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index, clsCommon.myCdbl(gv1.CurrentRow.Cells(colFat).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNF).Value))
                                SetTax(strPICode, gv1.CurrentRow.Index)
                                SetTaxDetails(strPICode, gv1.CurrentRow.Index)
                            Else
                                gv1.CurrentRow.Cells(colpriceitem).Value = ""
                                Throw New Exception("Please fill item first.")
                                Exit Sub
                            End If
                        End If
                        If e.Column Is gv1.Columns(colQty) Then
                            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colpriceitem).Value)) > 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colFat).Value) > 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNF).Value) > 0 Then
                                Dim strPICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colpriceitem).Value)
                                Dim strPIUOM As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colPriceItemUOM).Value)
                                ItemPrice(strPICode, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index, clsCommon.myCdbl(gv1.CurrentRow.Cells(colFat).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNF).Value))

                                SetTax(strPICode, gv1.CurrentRow.Index)
                                SetTaxDetails(strPICode, gv1.CurrentRow.Index)

                            End If
                        End If
                        If e.Column Is gv1.Columns(colFat) Then
                            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colpriceitem).Value)) > 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colUOM).Value)) > 0 Then
                                Dim CNFUOM As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + gv1.CurrentRow.Cells(colpriceitem).Value + "' and UOM_Code='" + gv1.CurrentRow.Cells(colUOM).Value + "'"))
                                Dim CNFKG As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + gv1.CurrentRow.Cells(colpriceitem).Value + "' and UOM_Code='KG'"))

                                Dim Fatkg As Decimal = (((CNFUOM * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)) / CNFKG) * (clsCommon.myCdbl(gv1.CurrentRow.Cells(colFat).Value))) / 100
                                Dim SNFkg As Decimal = (((CNFUOM * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)) / CNFKG) * (clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNF).Value))) / 100
                                gv1.CurrentRow.Cells(colFatKG).Value = Fatkg
                                gv1.CurrentRow.Cells(colSNFKG).Value = SNFkg
                                ItemPrice(gv1.CurrentRow.Cells(colpriceitem).Value, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index, clsCommon.myCdbl(gv1.CurrentRow.Cells(colFat).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNF).Value))
                            Else
                                Throw New Exception("Please fill price item and Qty first.")
                            End If
                        End If
                        If e.Column Is gv1.Columns(colSNF) Then
                            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colpriceitem).Value)) > 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > 0 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colUOM).Value)) > 0 Then
                                Dim CNFUOM As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + gv1.CurrentRow.Cells(colpriceitem).Value + "' and UOM_Code='" + gv1.CurrentRow.Cells(colUOM).Value + "'"))
                                Dim CNFKG As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + gv1.CurrentRow.Cells(colpriceitem).Value + "' and UOM_Code='KG'"))

                                Dim Fatkg As Decimal = (((CNFUOM * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)) / CNFKG) * (clsCommon.myCdbl(gv1.CurrentRow.Cells(colFat).Value))) / 100
                                Dim SNFkg As Decimal = (((CNFUOM * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)) / CNFKG) * (clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNF).Value))) / 100
                                gv1.CurrentRow.Cells(colFatKG).Value = Fatkg
                                gv1.CurrentRow.Cells(colSNFKG).Value = SNFkg
                                ItemPrice(gv1.CurrentRow.Cells(colpriceitem).Value, clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value), gv1.CurrentRow.Index, clsCommon.myCdbl(gv1.CurrentRow.Cells(colFat).Value), clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNF).Value))
                            Else
                                Throw New Exception("Please fill price item and Qty first.")

                            End If

                        End If
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenRawItemList(ByVal isButtonClick As Boolean)
        Try
            gv1.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getRawItemFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), False)
            gv1.CurrentRow.Cells(colItemName).Value = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colItemCode).Value & "' ")
            gv1.CurrentRow.Cells(colUOM).Value = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Default_UOM=1 and Item_Code='" & gv1.CurrentRow.Cells(colItemCode).Value & "' ")
            gv1.CurrentRow.Cells(colLocation).Value = txtLocation.Value
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub RefreshSerialNo()
        Dim intSerialNo As Integer
        For intCount As Integer = 0 To gv1.Rows.Count - 1
            intSerialNo += 1
            gv1.Rows(intCount).Cells(colLineNo).Value = intSerialNo
        Next
    End Sub
    Sub OpenRawitemUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("PS-BOUOMFndr", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
        End If
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvMainItem.CurrentRow.Cells(colAPItem).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gvMainItem.CurrentRow.Cells(colAPUOM).Value = clsCommon.ShowSelectForm("PS-BOUOMFndr", qry, "Code", whrCls, clsCommon.myCstr(gvMainItem.CurrentRow.Cells(colAPUOM).Value), "Code", isButtonClick)
        End If
    End Sub
    Sub OpenItemList(ByVal isButtonClick As Boolean)
        Try
            Dim strTax As String = Nothing
            GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            Dim whrCls As String = ""
            If clsCommon.myLen(whrCls) > 0 Then
                whrCls += "  and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 "
            Else
                whrCls += "  isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 "
            End If
            whrCls += " and isnull(TSPL_ITEM_MASTER.item_type,'')='F' "
            whrCls += " and tspl_item_master.Active=1 "
            gv1.CurrentRow.Cells(colpriceitem).Value = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colpriceitem).Value), False)
            If GSTStatus = False Then
                If CheckItemtaxType() = False Then
                    Exit Sub
                End If
            End If
            gv1.CurrentRow.Cells(colPriceItemName).Value = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colpriceitem).Value & "' ")
            gv1.CurrentRow.Cells(colPriceItemUOM).Value = gv1.CurrentRow.Cells(colUOM).Value
            Dim CNFUOM As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + gv1.CurrentRow.Cells(colpriceitem).Value + "' and UOM_Code='" + gv1.CurrentRow.Cells(colUOM).Value + "'"))
            Dim CNFKG As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" + gv1.CurrentRow.Cells(colpriceitem).Value + "' and UOM_Code='KG'"))
            gv1.CurrentRow.Cells(colFat).Value = clsDBFuncationality.getSingleValue("select Actual_Range from TSPL_ITEM_QC_PARAMETER_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colpriceitem).Value & "' and code='FAT' ")
            gv1.CurrentRow.Cells(colSNF).Value = clsDBFuncationality.getSingleValue("select Actual_Range from TSPL_ITEM_QC_PARAMETER_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colpriceitem).Value & "' and code='SNF' ")
            Dim Fatkg As Decimal = (((CNFUOM * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)) / CNFKG) * (clsCommon.myCdbl(gv1.CurrentRow.Cells(colFat).Value))) / 100
            Dim SNFkg As Decimal = (((CNFUOM * clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)) / CNFKG) * (clsCommon.myCdbl(gv1.CurrentRow.Cells(colSNF).Value))) / 100
            gv1.CurrentRow.Cells(colFatKG).Value = Fatkg
            gv1.CurrentRow.Cells(colSNFKG).Value = SNFkg
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub OpenMainItemList(ByVal isButtonClick As Boolean)
        Try
            Dim strTax As String = Nothing
            GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            Dim whrCls As String = ""
            If clsCommon.myLen(whrCls) > 0 Then
                whrCls += "  and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 "
            Else
                whrCls += "  isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0 "
            End If
            whrCls += " and isnull(TSPL_ITEM_MASTER.item_type,'')='F' "
            whrCls += " and tspl_item_master.Active=1 "
            gvMainItem.CurrentRow.Cells(colAPItem).Value = clsItemMaster.getFinder(whrCls, clsCommon.myCstr(gvMainItem.CurrentRow.Cells(colAPItem).Value), False)
            If GSTStatus = False Then
                If CheckItemtaxType() = False Then
                    Exit Sub
                End If
            End If
            gvMainItem.CurrentRow.Cells(colAPitemName).Value = clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" & gvMainItem.CurrentRow.Cells(colAPItem).Value & "' ")
            gvMainItem.CurrentRow.Cells(colAPUOM).Value = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Default_UOM=1 and Item_Code='" & gvMainItem.CurrentRow.Cells(colAPItem).Value & "' ")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Function CheckItemtaxType() As Boolean
        Throw New NotImplementedException
    End Function
    Private Sub gvMainItem_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvMainItem.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvMainItem.Columns(colAPItem) OrElse e.Column Is gvMainItem.Columns(colAPUOM) OrElse e.Column Is gvMainItem.Columns(colAPQty) Then
                        If e.Column Is gvMainItem.Columns(colAPItem) Then
                            OpenMainItemList(False)
                        End If
                        If e.Column Is gvMainItem.Columns(colAPUOM) Then
                            OpenUOMList(False)
                        End If
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvMainItem_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvMainItem.CurrentColumnChanged
        If gvMainItem.RowCount > 0 Then
            Dim intCurrRow As Integer = gvMainItem.CurrentRow.Index
            'gvMainItem.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvMainItem.Rows.Count - 1 Then
                gvMainItem.Rows.AddNew()
                gvMainItem.CurrentRow = gvMainItem.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnreverse_Click(sender As Object, e As EventArgs) Handles btnreverse.Click
        Try
            If clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsLeakedSaleReturnHead.ReverseAndUnpost(txtDocNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class