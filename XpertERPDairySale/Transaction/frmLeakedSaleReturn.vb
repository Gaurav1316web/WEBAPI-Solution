Imports System.Data.SqlClient
Imports System.IO
Imports common
Public Class frmLeakedSaleReturn
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    '' For Detail
    Const colLineNo As String = "colLineNo"
    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colQty As String = "colQty"
    Const colUOM As String = "colUOM"
    Const colpriceitem As String = "colpriceitem"
    Const colPriceItemName As String = "colPriceItemName"
    Const colFat As String = "colFat"
    Const colSNF As String = "colSNF"
    Const colFatKG As String = "colFatKG"
    Const colSNFKG As String = "colSNFKG"
    Const colRate As String = "colRate"
    Const colAmount As String = "colAmount"
    Const colDisPer As String = "COLDISPER"
    Const colDisAmt As String = "COLDISAMT"
    Const colAmtAfterDis As String = "COLAMTAFTERDIS"
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
    Const colTotTaxAmt As String = "TAXAMT"

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
    Const colAPUOM As String = "colAPQty"
#End Region
    Public Sub SetUserMgmtNew()
        ''Richa Check in 19/06/2020
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmLeakedSaleReturn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        LoadBlankGridMainItem()
        LoadBlankGridTax()
        AddNew()

    End Sub
    Sub AddNew()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtCustomer.Value = ""
        txtRouteNo.Value = ""
        txtRemarks.Text = ""
        LoadBlankGrid()
        LoadBlankGridMainItem()
        LoadBlankGridTax()
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
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
        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.IsVisible = True
        repoQty.Minimum = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoQty.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoQty)
        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUOM
        repoUOM.Width = 150
        repoUOM.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUOM)
        Dim repopriceitem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repopriceitem.FormatString = ""
        repopriceitem.HeaderText = "Price Item"
        repopriceitem.Name = colpriceitem
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
        Dim repoFAt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFAt = New GridViewDecimalColumn()
        repoFAt.FormatString = ""
        repoFAt.HeaderText = "Fat%"
        repoFAt.Name = colFat
        repoFAt.IsVisible = True
        repoFAt.Minimum = 0
        repoFAt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFAt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFAt)
        Dim repoSNF As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNF = New GridViewDecimalColumn()
        repoSNF.FormatString = ""
        repoSNF.HeaderText = "SNF%"
        repoSNF.Name = colSNF
        repoSNF.IsVisible = True
        repoSNF.Minimum = 0
        repoSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSNF.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoSNF)
        Dim repoFatKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatKG = New GridViewDecimalColumn()
        repoFatKG.FormatString = ""
        repoFatKG.HeaderText = "FAT KG"
        repoFatKG.Name = colFatKG
        repoFatKG.IsVisible = True
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
        repoRate.Minimum = 0
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRate)
        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmount
        repoAmount.IsVisible = True
        repoAmount.Minimum = 0
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmount.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmount)

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
        Dim repoapQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoapQty = New GridViewDecimalColumn()
        repoapQty.FormatString = ""
        repoapQty.HeaderText = "Quantity"
        repoapQty.Name = colAPQty
        repoapQty.IsVisible = True
        repoapQty.Minimum = 0
        repoapQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoapQty.ReadOnly = False
        gvMainItem.MasterTemplate.Columns.Add(repoapQty)
        Dim repomainUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomainUOM.FormatString = ""
        repomainUOM.HeaderText = "UOM"
        repomainUOM.Name = colAPUOM
        repomainUOM.Width = 150
        repomainUOM.ReadOnly = False
        gvMainItem.MasterTemplate.Columns.Add(repomainUOM)

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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator

    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating

    End Sub
End Class