Imports common
Public Class FrmSalesOrder
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim GSTStatus As Boolean = False
    Dim CalculateTaxRatefromItemwsieTaxOnSale As Integer = 0
    Const colLineNo As String = "colLineNo"
    Const colRowType As String = "colRowType"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colUOM As String = "colUOM"
    Const colQty As String = "colQty"
    Const colTenderRate As String = "colTenderRate"
    Const colRate As String = "colRate"
    Const colItemAmt As String = "colItemAmt"
    Const colTax1 As String = "colTax1"
    Const colTax1_BaseAmt As String = "colTax1_BaseAmt"
    Const colTax1_Rate As String = "colTax1_Rate"
    Const colTax1_Amt As String = "colTax1_Amt"
    Const colTax2 As String = "colTax2"
    Const colTax2_BaseAmt As String = "colTax2_BaseAmt"
    Const colTax2_Rate As String = "colTax2_Rate"
    Const colTax2_Amt As String = "colTax2_Amt"
    Const colTax3 As String = "colTax3"
    Const colTax3_BaseAmt As String = "colTax3_BaseAmt"
    Const colTax3_Rate As String = "colTax3_Rate"
    Const colTax3_Amt As String = "colTax3_Amt"
    Const colTax4 As String = "colTax4"
    Const colTax4_BaseAmt As String = "colTax4_BaseAmt"
    Const colTax4_Rate As String = "colTax4_Rate"
    Const colTax4_Amt As String = "colTax4_Amt"
    Const colTax5 As String = "colTax5"
    Const colTax5_BaseAmt As String = "colTax5_BaseAmt"
    Const colTax5_Rate As String = "colTax5_Rate"
    Const colTax5_Amt As String = "colTax5_Amt"
    Const colTax6 As String = "colTax6"
    Const colTax6_BaseAmt As String = "colTax6_BaseAmt"
    Const colTax6_Rate As String = "colTax6_Rate"
    Const colTax6_Amt As String = "colTax6_Amt"
    Const colTax7 As String = "colTax7"
    Const colTax7_BaseAmt As String = "colTax7_BaseAmt"
    Const colTax7_Rate As String = "colTax7_Rate"
    Const colTax7_Amt As String = "colTax7_Amt"
    Const colTax8 As String = "colTax8"
    Const colTax8_BaseAmt As String = "colTax8_BaseAmt"
    Const colTax8_Rate As String = "colTax8_Rate"
    Const colTax8_Amt As String = "colTax8_Amt"
    Const colTax9 As String = "colTax9"
    Const colTax9_BaseAmt As String = "colTax9_BaseAmt"
    Const colTax9_Rate As String = "colTax9_Rate"
    Const colTax9_Amt As String = "colTax9_Amt"
    Const colTax10 As String = "colTax10"
    Const colTax10_BaseAmt As String = "colTax10_BaseAmt"
    Const colTax10_Rate As String = "colTax10_Rate"
    Const colTax10_Amt As String = "colTax10_Amt"
    Const colTotalTaxAmt As String = "colTotalTaxAmt"
    Const colTotalAmt As String = "colTotalAmt"
    Const colInclusiveTax As String = "colInclusiveTax"
    Const colInclusiveTPT As String = "colInclusiveTPT"

    Const colTTaxAutCode As String = "colTTaxAutCode"
    Const colTTaxAutName As String = "colTTaxAutName"
    Const colTTaxRate As String = "colTTaxRate"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"
#End Region
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        If MyBase.isReverse Then
            btnReverseAndUnPost.Enabled = True
        End If
    End Sub
    Private Sub FrmSalesOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'CreateTable()
        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.MandatoryPDFFileAny = False
        CalculateTaxRatefromItemwsieTaxOnSale = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, Nothing))

        AddNew()
    End Sub
    Private Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = clsItemRowType.RowTypeItem
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsItemRowType.RowTypeMisc
        dt.Rows.Add(dr)

        Return dt
    End Function
    Private Sub LoadBlankGrid()
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
        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Row Type"
        repoRowType.Name = colRowType
        repoRowType.Width = 100
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetItemType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Code"
        gv1.MasterTemplate.Columns.Add(repoRowType)
        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.Width = 100
        repoICode.ReadOnly = False
        repoICode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoICode)
        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Desc"
        repoIName.Name = colIName
        repoIName.Width = 100
        repoIName.ReadOnly = True
        repoIName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = "{0:n2}"
        repoQty.HeaderText = "Qty"
        repoQty.Name = colQty
        repoQty.Width = 100
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)
        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "Unit Code"
        repoUOM.Name = colUOM
        repoUOM.Width = 50
        repoUOM.ReadOnly = True
        repoUOM.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoUOM)
        Dim repoTenderRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTenderRate.FormatString = "{0:n2}"
        repoTenderRate.HeaderText = "Tender Rate"
        repoTenderRate.Name = colTenderRate
        repoTenderRate.Width = 50
        repoTenderRate.Minimum = 0
        repoTenderRate.ReadOnly = True
        repoTenderRate.IsVisible = False
        repoTenderRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTenderRate)
        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = "{0:n2}"
        repoRate.HeaderText = "Item Rate"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ReadOnly = True
        repoRate.IsVisible = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)
        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = "{0:n2}"
        repoAmt.HeaderText = "Item Amt"
        repoAmt.Name = colItemAmt
        repoAmt.Width = 100
        repoAmt.Minimum = 0
        repoAmt.ShowUpDownButtons = False
        repoAmt.ReadOnly = False
        repoAmt.IsVisible = True
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)
        Dim repoTax1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1.FormatString = ""
        repoTax1.HeaderText = "Tax1"
        repoTax1.Name = colTax1
        repoTax1.Width = 100
        repoTax1.ReadOnly = True
        repoTax1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax1)
        Dim repoTax1BaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax1BaseAmt.FormatString = "{0:n2}"
        repoTax1BaseAmt.HeaderText = "Tax1 Base Amt"
        repoTax1BaseAmt.Name = colTax1_BaseAmt
        repoTax1BaseAmt.Width = 50
        repoTax1BaseAmt.Minimum = 0
        repoTax1BaseAmt.ReadOnly = True
        repoTax1BaseAmt.IsVisible = False
        repoTax1BaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax1BaseAmt)
        Dim repoTax1Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax1Rate.FormatString = "{0:n2}"
        repoTax1Rate.HeaderText = "Tax1 Rate"
        repoTax1Rate.Name = colTax1_Rate
        repoTax1Rate.Width = 50
        repoTax1Rate.Minimum = 0
        repoTax1Rate.ReadOnly = True
        repoTax1Rate.IsVisible = False
        repoTax1Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax1Rate)
        Dim repoTax1Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax1Amt.FormatString = "{0:n2}"
        repoTax1Amt.HeaderText = "Tax1 Amt"
        repoTax1Amt.Name = colTax1_Amt
        repoTax1Amt.Width = 50
        repoTax1Amt.Minimum = 0
        repoTax1Amt.ReadOnly = True
        repoTax1Amt.IsVisible = False
        repoTax1Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax1Amt)
        Dim repoTax2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2.FormatString = ""
        repoTax2.HeaderText = "Tax2"
        repoTax2.Name = colTax2
        repoTax2.Width = 100
        repoTax2.ReadOnly = True
        repoTax2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax2)
        Dim repoTax2BaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax2BaseAmt.FormatString = "{0:n2}"
        repoTax2BaseAmt.HeaderText = "Tax2 Base Amt"
        repoTax2BaseAmt.Name = colTax2_BaseAmt
        repoTax2BaseAmt.Width = 50
        repoTax2BaseAmt.Minimum = 0
        repoTax2BaseAmt.ReadOnly = True
        repoTax2BaseAmt.IsVisible = False
        repoTax2BaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax2BaseAmt)
        Dim repoTax2Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax2Rate.FormatString = "{0:n2}"
        repoTax2Rate.HeaderText = "Tax2 Rate"
        repoTax2Rate.Name = colTax2_Rate
        repoTax2Rate.Width = 50
        repoTax2Rate.Minimum = 0
        repoTax2Rate.ReadOnly = True
        repoTax2Rate.IsVisible = False
        repoTax2Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax2Rate)
        Dim repoTax2Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax2Amt.FormatString = "{0:n2}"
        repoTax2Amt.HeaderText = "Tax2 Amt"
        repoTax2Amt.Name = colTax2_Amt
        repoTax2Amt.Width = 50
        repoTax2Amt.Minimum = 0
        repoTax2Amt.ReadOnly = True
        repoTax2Amt.IsVisible = False
        repoTax2Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax2Amt)
        Dim repoTax3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3.FormatString = ""
        repoTax3.HeaderText = "Tax3"
        repoTax3.Name = colTax3
        repoTax3.Width = 100
        repoTax3.ReadOnly = True
        repoTax3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax3)
        Dim repoTax3BaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax3BaseAmt.FormatString = "{0:n2}"
        repoTax3BaseAmt.HeaderText = "Tax3 Base Amt"
        repoTax3BaseAmt.Name = colTax3_BaseAmt
        repoTax3BaseAmt.Width = 50
        repoTax3BaseAmt.Minimum = 0
        repoTax3BaseAmt.ReadOnly = True
        repoTax3BaseAmt.IsVisible = False
        repoTax3BaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax3BaseAmt)
        Dim repoTax3Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax3Rate.FormatString = "{0:n2}"
        repoTax3Rate.HeaderText = "Tax3 Rate"
        repoTax3Rate.Name = colTax3_Rate
        repoTax3Rate.Width = 50
        repoTax3Rate.Minimum = 0
        repoTax3Rate.ReadOnly = True
        repoTax3Rate.IsVisible = False
        repoTax3Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax3Rate)
        Dim repoTax3Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax3Amt.FormatString = "{0:n2}"
        repoTax3Amt.HeaderText = "Tax3 Amt"
        repoTax3Amt.Name = colTax3_Amt
        repoTax3Amt.Width = 50
        repoTax3Amt.Minimum = 0
        repoTax3Amt.ReadOnly = True
        repoTax3Amt.IsVisible = False
        repoTax3Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax3Amt)
        Dim repoTax4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4.FormatString = ""
        repoTax4.HeaderText = "Tax4"
        repoTax4.Name = colTax4
        repoTax4.Width = 100
        repoTax4.ReadOnly = True
        repoTax4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax4)
        Dim repoTax4BaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax4BaseAmt.FormatString = "{0:n2}"
        repoTax4BaseAmt.HeaderText = "Tax4 Base Amt"
        repoTax4BaseAmt.Name = colTax4_BaseAmt
        repoTax4BaseAmt.Width = 50
        repoTax4BaseAmt.Minimum = 0
        repoTax4BaseAmt.ReadOnly = True
        repoTax4BaseAmt.IsVisible = False
        repoTax4BaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax4BaseAmt)
        Dim repoTax4Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax4Rate.FormatString = "{0:n2}"
        repoTax4Rate.HeaderText = "Tax4 Rate"
        repoTax4Rate.Name = colTax4_Rate
        repoTax4Rate.Width = 50
        repoTax4Rate.Minimum = 0
        repoTax4Rate.ReadOnly = True
        repoTax4Rate.IsVisible = False
        repoTax4Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax4Rate)
        Dim repoTax4Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax4Amt.FormatString = "{0:n2}"
        repoTax4Amt.HeaderText = "Tax4 Amt"
        repoTax4Amt.Name = colTax4_Amt
        repoTax4Amt.Width = 50
        repoTax4Amt.Minimum = 0
        repoTax4Amt.ReadOnly = True
        repoTax4Amt.IsVisible = False
        repoTax4Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax4Amt)
        Dim repoTax5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5.FormatString = ""
        repoTax5.HeaderText = "Tax5"
        repoTax5.Name = colTax5
        repoTax5.Width = 100
        repoTax5.ReadOnly = True
        repoTax5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax5)
        Dim repoTax5BaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax5BaseAmt.FormatString = "{0:n2}"
        repoTax5BaseAmt.HeaderText = "Tax5 Base Amt"
        repoTax5BaseAmt.Name = colTax5_BaseAmt
        repoTax5BaseAmt.Width = 50
        repoTax5BaseAmt.Minimum = 0
        repoTax5BaseAmt.ReadOnly = True
        repoTax5BaseAmt.IsVisible = False
        repoTax5BaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax5BaseAmt)
        Dim repoTax5Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax5Rate.FormatString = "{0:n2}"
        repoTax5Rate.HeaderText = "Tax5 Rate"
        repoTax5Rate.Name = colTax5_Rate
        repoTax5Rate.Width = 50
        repoTax5Rate.Minimum = 0
        repoTax5Rate.ReadOnly = True
        repoTax5Rate.IsVisible = False
        repoTax5Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax5Rate)
        Dim repoTax5Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax5Amt.FormatString = "{0:n2}"
        repoTax5Amt.HeaderText = "Tax5 Amt"
        repoTax5Amt.Name = colTax5_Amt
        repoTax5Amt.Width = 50
        repoTax5Amt.Minimum = 0
        repoTax5Amt.ReadOnly = True
        repoTax5Amt.IsVisible = False
        repoTax5Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax5Amt)
        Dim repoTax6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax6.FormatString = ""
        repoTax6.HeaderText = "Tax6"
        repoTax6.Name = colTax6
        repoTax6.Width = 100
        repoTax6.ReadOnly = True
        repoTax6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax6)
        Dim repoTax6BaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax6BaseAmt.FormatString = "{0:n2}"
        repoTax6BaseAmt.HeaderText = "Tax6 Base Amt"
        repoTax6BaseAmt.Name = colTax6_BaseAmt
        repoTax6BaseAmt.Width = 50
        repoTax6BaseAmt.Minimum = 0
        repoTax6BaseAmt.ReadOnly = True
        repoTax6BaseAmt.IsVisible = False
        repoTax6BaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax6BaseAmt)
        Dim repoTax6Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax6Rate.FormatString = "{0:n2}"
        repoTax6Rate.HeaderText = "Tax6 Rate"
        repoTax6Rate.Name = colTax6_Rate
        repoTax6Rate.Width = 50
        repoTax6Rate.Minimum = 0
        repoTax6Rate.ReadOnly = True
        repoTax6Rate.IsVisible = False
        repoTax6Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax6Rate)
        Dim repoTax6Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax6Amt.FormatString = "{0:n2}"
        repoTax6Amt.HeaderText = "Tax6 Amt"
        repoTax6Amt.Name = colTax6_Amt
        repoTax6Amt.Width = 50
        repoTax6Amt.Minimum = 0
        repoTax6Amt.ReadOnly = True
        repoTax6Amt.IsVisible = False
        repoTax6Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax6Amt)
        Dim repoTax7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax7.FormatString = ""
        repoTax7.HeaderText = "Tax7"
        repoTax7.Name = colTax7
        repoTax7.Width = 100
        repoTax7.ReadOnly = True
        repoTax7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax7)
        Dim repoTax7BaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax7BaseAmt.FormatString = "{0:n2}"
        repoTax7BaseAmt.HeaderText = "Tax7 Base Amt"
        repoTax7BaseAmt.Name = colTax7_BaseAmt
        repoTax7BaseAmt.Width = 50
        repoTax7BaseAmt.Minimum = 0
        repoTax7BaseAmt.ReadOnly = True
        repoTax7BaseAmt.IsVisible = False
        repoTax7BaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax7BaseAmt)
        Dim repoTax7Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax7Rate.FormatString = "{0:n2}"
        repoTax7Rate.HeaderText = "Tax7 Rate"
        repoTax7Rate.Name = colTax7_Rate
        repoTax7Rate.Width = 50
        repoTax7Rate.Minimum = 0
        repoTax7Rate.ReadOnly = True
        repoTax7Rate.IsVisible = False
        repoTax7Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax7Rate)
        Dim repoTax7Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax7Amt.FormatString = "{0:n2}"
        repoTax7Amt.HeaderText = "Tax7 Amt"
        repoTax7Amt.Name = colTax7_Amt
        repoTax7Amt.Width = 50
        repoTax7Amt.Minimum = 0
        repoTax7Amt.ReadOnly = True
        repoTax7Amt.IsVisible = False
        repoTax7Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax7Amt)
        Dim repoTax8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax8.FormatString = ""
        repoTax8.HeaderText = "Tax8"
        repoTax8.Name = colTax8
        repoTax8.Width = 100
        repoTax8.ReadOnly = True
        repoTax8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax8)
        Dim repoTax8BaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax8BaseAmt.FormatString = "{0:n2}"
        repoTax8BaseAmt.HeaderText = "Tax8 Base Amt"
        repoTax8BaseAmt.Name = colTax8_BaseAmt
        repoTax8BaseAmt.Width = 50
        repoTax8BaseAmt.Minimum = 0
        repoTax8BaseAmt.ReadOnly = True
        repoTax8BaseAmt.IsVisible = False
        repoTax8BaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax8BaseAmt)
        Dim repoTax8Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax8Rate.FormatString = "{0:n2}"
        repoTax8Rate.HeaderText = "Tax8 Rate"
        repoTax8Rate.Name = colTax8_Rate
        repoTax8Rate.Width = 50
        repoTax8Rate.Minimum = 0
        repoTax8Rate.ReadOnly = True
        repoTax8Rate.IsVisible = False
        repoTax8Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax8Rate)
        Dim repoTax8Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax8Amt.FormatString = "{0:n2}"
        repoTax8Amt.HeaderText = "Tax8 Amt"
        repoTax8Amt.Name = colTax8_Amt
        repoTax8Amt.Width = 50
        repoTax8Amt.Minimum = 0
        repoTax8Amt.ReadOnly = True
        repoTax8Amt.IsVisible = False
        repoTax8Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax8Amt)
        Dim repoTax9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax9.FormatString = ""
        repoTax9.HeaderText = "Tax9"
        repoTax9.Name = colTax9
        repoTax9.Width = 100
        repoTax9.ReadOnly = True
        repoTax9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax9)
        Dim repoTax9BaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax9BaseAmt.FormatString = "{0:n2}"
        repoTax9BaseAmt.HeaderText = "Tax9 Base Amt"
        repoTax9BaseAmt.Name = colTax9_BaseAmt
        repoTax9BaseAmt.Width = 50
        repoTax9BaseAmt.Minimum = 0
        repoTax9BaseAmt.ReadOnly = True
        repoTax9BaseAmt.IsVisible = False
        repoTax9BaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax9BaseAmt)
        Dim repoTax9Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax9Rate.FormatString = "{0:n2}"
        repoTax9Rate.HeaderText = "Tax9 Rate"
        repoTax9Rate.Name = colTax9_Rate
        repoTax9Rate.Width = 50
        repoTax9Rate.Minimum = 0
        repoTax9Rate.ReadOnly = True
        repoTax9Rate.IsVisible = False
        repoTax9Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax9Rate)
        Dim repoTax9Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax9Amt.FormatString = "{0:n2}"
        repoTax9Amt.HeaderText = "Tax9 Amt"
        repoTax9Amt.Name = colTax9_Amt
        repoTax9Amt.Width = 50
        repoTax9Amt.Minimum = 0
        repoTax9Amt.ReadOnly = True
        repoTax9Amt.IsVisible = False
        repoTax9Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax9Amt)
        Dim repoTax10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax10.FormatString = ""
        repoTax10.HeaderText = "Tax10"
        repoTax10.Name = colTax10
        repoTax10.Width = 100
        repoTax10.ReadOnly = True
        repoTax10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTax10)
        Dim repoTax10BaseAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax10BaseAmt.FormatString = "{0:n2}"
        repoTax10BaseAmt.HeaderText = "Tax10 Base Amt"
        repoTax10BaseAmt.Name = colTax10_BaseAmt
        repoTax10BaseAmt.Width = 50
        repoTax10BaseAmt.Minimum = 0
        repoTax10BaseAmt.ReadOnly = True
        repoTax10BaseAmt.IsVisible = False
        repoTax10BaseAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax10BaseAmt)
        Dim repoTax10Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax10Rate.FormatString = "{0:n2}"
        repoTax10Rate.HeaderText = "Tax10 Rate"
        repoTax10Rate.Name = colTax10_Rate
        repoTax10Rate.Width = 50
        repoTax10Rate.Minimum = 0
        repoTax10Rate.ReadOnly = True
        repoTax10Rate.IsVisible = False
        repoTax10Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax10Rate)
        Dim repoTax10Amt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax10Amt.FormatString = "{0:n2}"
        repoTax10Amt.HeaderText = "Tax10 Amt"
        repoTax10Amt.Name = colTax10_Amt
        repoTax10Amt.Width = 50
        repoTax10Amt.Minimum = 0
        repoTax10Amt.ReadOnly = True
        repoTax10Amt.IsVisible = False
        repoTax10Amt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTax10Amt)
        Dim repoTotalTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalTaxAmt.FormatString = "{0:n2}"
        repoTotalTaxAmt.HeaderText = "Total Tax Amt"
        repoTotalTaxAmt.Name = colTotalTaxAmt
        repoTotalTaxAmt.Width = 150
        repoTotalTaxAmt.Minimum = 0
        repoTotalTaxAmt.ReadOnly = True
        repoTotalTaxAmt.IsVisible = True
        repoTotalTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotalTaxAmt)

        Dim repoTotalAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalAmt.FormatString = "{0:n2}"
        repoTotalAmt.HeaderText = "Total Amt"
        repoTotalAmt.Name = colTotalAmt
        repoTotalAmt.Width = 150
        repoTotalAmt.Minimum = 0
        repoTotalAmt.ReadOnly = True
        repoTotalAmt.IsVisible = True
        repoTotalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotalAmt)
        Dim repoTotalInclusiveTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalInclusiveTax.FormatString = "{0:n2}"
        repoTotalInclusiveTax.HeaderText = "Inclusive Tax"
        repoTotalInclusiveTax.Name = colInclusiveTax
        repoTotalInclusiveTax.Width = 50
        repoTotalInclusiveTax.Minimum = 0
        repoTotalInclusiveTax.ReadOnly = True
        repoTotalInclusiveTax.IsVisible = False
        repoTotalInclusiveTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotalInclusiveTax)
        Dim repoTotalInclusiveTPT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalInclusiveTPT.FormatString = "{0:n2}"
        repoTotalInclusiveTPT.HeaderText = "Inclusive TPT"
        repoTotalInclusiveTPT.Name = colInclusiveTPT
        repoTotalInclusiveTPT.Width = 50
        repoTotalInclusiveTPT.Minimum = 0
        repoTotalInclusiveTPT.ReadOnly = True
        repoTotalInclusiveTPT.IsVisible = False
        repoTotalInclusiveTPT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotalInclusiveTPT)
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

    End Sub
    Private Sub AddNew()
        isNewEntry = True
        LoadBlankGrid()
        LoadBlankGridTax()
        UcAttachment1.BlankAllControls()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtRALNo.Value = ""
        lblRalNoDesc.Text = ""
        txtCustomerCode.Value = ""
        lblCustomerName.Text = ""
        txtLocation.Value = ""
        lblLocationDesc.Text = ""
        txtSubLocation.Value = ""
        lblSubLocationDesc.Text = ""
        txtRefNo.Text = ""
        txtRefDate.Value = txtDate.Value
        txtRemark.Text = ""
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtDocAmtWithoutTax.Text = ""
        txtTaxAmt.Text = ""
        txtDocAmt.Text = ""
        txtTenderQty.Text = 0
        txtBalQty.Text = 0
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
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
        gv1.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
    End Sub
    Sub ItemDescRaadonly(ByVal isButtonClick As Boolean)
        If clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value) <> "Item" Then
            gv1.CurrentRow.Cells(colIName).ReadOnly = False
        Else
            gv1.CurrentRow.Cells(colIName).ReadOnly = True
        End If
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Private Sub setGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).ReadOnly = True
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemAmt).ReadOnly = False
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub txtDocCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocCode._MYNavigator
        Try
            Dim strwherecls As String = ""
            Dim strQry As String = ""
            strwherecls = Xtra.CustomerPermission()
            strQry = "select count(*) from TSPL_CUSTOMER_TENDER_ORDER where Document_Code='" & txtDocCode.Value & "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
            If count = 0 Then
                txtDocCode.MyReadOnly = False
            Else
                txtDocCode.MyReadOnly = True
            End If
            LoadData(txtDocCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocCode._MYValidating
        Try
            Dim strwherecls As String = ""
            strwherecls = Xtra.CustomerPermission()
            Dim qry As String = "select Document_Code as Code,Document_Date,RAL_No,Cust_Code,Location_Code,case when Status=1 then 'Approved' else 'Pending' end as Status
from TSPL_CUSTOMER_TENDER_ORDER "
            Dim whrClas As String = ""
            LoadData(clsCommon.ShowSelectForm("Cust_ordDocfnd", qry, "Code", whrClas, txtDocCode.Value, "Code", isButtonClicked, "TSPL_CUSTOMER_TENDER_ORDER.Document_Date"), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtRALNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRALNo._MYValidating
        Try
            Dim qry As String = "select Document_Code as Code,From_Date as [From Date],To_Date as [To Date],Location_Code as [Location] from TSPL_CUSTOMER_TENDER"
            Dim Whrcls As String = " status=1"
            txtRALNo.Value = clsCommon.ShowSelectForm("Sale-Ordralfnd", qry, "Code", Whrcls, txtRALNo.Value, "Code", isButtonClicked)
            lblRalNoDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Remarks from TSPL_CUSTOMER_TENDER where Document_Code='" & txtRALNo.Value & "'"))
            GetTenderQty(txtRALNo.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub GetTenderQty(ByVal strCode As String)
        Try
            Dim qry As String = "select Total_Qty from TSPL_CUSTOMER_TENDER where Document_Code='" & strCode & "'"
            txtTenderQty.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            qry = "select sum(Qty) as Qty from TSPL_CUSTOMER_TENDER_ORDER
left join TSPL_CUSTOMER_TENDER_ORDER_DETAIL on TSPL_CUSTOMER_TENDER_ORDER_DETAIL.Document_Code=TSPL_CUSTOMER_TENDER_ORDER.Document_Code
where TSPL_CUSTOMER_TENDER_ORDER.RAL_No='" & strCode & "' and TSPL_CUSTOMER_TENDER_ORDER.Status=1"
            txtBalQty.Text = clsCommon.myCdbl(txtTenderQty.Text) - clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtCustomerCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomerCode._MYValidating
        Try
            If clsCommon.myLen(txtRALNo.Value) > 0 AndAlso clsCommon.myLen(txtLocation.Value) > 0 Then
                Dim qry As String = "select TSPL_CUSTOMER_TENDER_DETAIL.Cust_Code as Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_TENDER_DETAIL.Item_Rate,TSPL_CUSTOMER_TENDER.Inclusive_Tax,TSPL_CUSTOMER_TENDER.Inclusive_TPT from
TSPL_CUSTOMER_TENDER left join TSPL_CUSTOMER_TENDER_DETAIL on TSPL_CUSTOMER_TENDER_DETAIL.Document_Code=TSPL_CUSTOMER_TENDER.Document_Code
left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_TENDER_DETAIL.Cust_Code"
                Dim Whrcls As String = "  TSPL_CUSTOMER_TENDER.Document_Code='" & txtRALNo.Value & "' "
                txtCustomerCode.Value = clsCommon.ShowSelectForm("Sale-Ordcustfnd", qry, "Code", Whrcls, txtCustomerCode.Value, "Code", isButtonClicked)
                lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & txtCustomerCode.Value & "'"))
                LoadGrid(txtRALNo.Value, txtCustomerCode.Value)
            Else
                Throw New Exception("Please Select Location or RAL No.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name,Loc_Short_Name as [Short Name] from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Category not in('MCC')"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("CUST-TENDLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtLocation.Value & "'"))
            If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                    txtSubLocation.Enabled = True
                Else
                    txtSubLocation.Enabled = False
                End If
                txtSubLocation.Value = ""
                lblSubLocationDesc.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) > 0 Then
                txtSubLocation.Value = clsLocation.getFinder(" (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Main_Location_Code='" & txtLocation.Value & "'", txtSubLocation.Value, isButtonClicked)
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocationDesc.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                Else
                    lblSubLocationDesc.Text = ""
                End If
            Else
                txtLocation.Value = ""
                lblSubLocationDesc.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "varchar(30) NOT NULL Primary key")
        coll.Add("Document_Date", "datetime not NULL")
        coll.Add("RAL_No", "varchar(30) NOT NULL REFERENCES TSPL_CUSTOMER_TENDER(Document_Code)")
        coll.Add("Cust_Code", "Varchar(12) not null references TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Location_Code", "Varchar(12) NOt NULL  References TSPL_LOCATION_MASTER(Location_Code)")
        coll.Add("Sub_Location", "Varchar(12) NULL  References TSPL_LOCATION_MASTER(Location_Code)")
        coll.Add("Ref_No", "Varchar(50) Null")
        coll.Add("Ref_Date", "datetime NULL")
        coll.Add("Remarks", "Varchar(100) NULL")
        coll.Add("Tax_Group", "varchar(12) NULL")
        coll.Add("TaxGroupName", "varchar(30) NULL")
        coll.Add("TAX1", "varchar(12) NULL")
        coll.Add("TAX1_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX1_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX1_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX2", "varchar(12) NULL")
        coll.Add("TAX2_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX2_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX2_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX3", "varchar(12) NULL")
        coll.Add("TAX3_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX3_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX3_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX4", "varchar(12) NULL")
        coll.Add("TAX4_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX4_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX4_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX5", "varchar(12) NULL")
        coll.Add("TAX5_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX5_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX5_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX6", "varchar(12) NULL")
        coll.Add("TAX6_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX6_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX6_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX7", "varchar(12) NULL")
        coll.Add("TAX7_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX7_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX7_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX8", "varchar(12) NULL")
        coll.Add("TAX8_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX8_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX8_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX9", "varchar(12) NULL")
        coll.Add("TAX9_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX9_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX9_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX10", "varchar(12) NULL")
        coll.Add("TAX10_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX10_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX10_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("Doc_Amt_Without_Tax", "decimal(18, 6) NULL")
        coll.Add("Tax_Amt", "decimal(18, 6) NULL")
        coll.Add("Document_Amt", "decimal(18, 6) Not null")
        coll.Add("Status", "integer not null default 0")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "Datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_TENDER_ORDER", coll, "", True, False, "", "Document_Code", "Document_Date", True)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_Code", "varchar(30) NOT NULL REFERENCES TSPL_CUSTOMER_TENDER_ORDER(Document_Code)")
        coll.Add("RowType", "Varchar(50) not null")
        coll.Add("Item_Code", "Varchar(50) not null")
        coll.Add("Unit_Code", "Varchar(50) not null")
        coll.Add("Qty", "decimal(18, 6) null")
        coll.Add("Tender_Rate", "decimal(18, 6) null")
        coll.Add("Item_Rate", "decimal(18, 6) null")
        coll.Add("Item_Amt", "decimal(18, 6) null")
        coll.Add("Item_Type", "varchar(12) NULL")
        coll.Add("TAX1", "varchar(12) NULL")
        coll.Add("TAX1_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX1_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX1_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX2", "varchar(12) NULL")
        coll.Add("TAX2_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX2_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX2_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX3", "varchar(12) NULL")
        coll.Add("TAX3_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX3_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX3_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX4", "varchar(12) NULL")
        coll.Add("TAX4_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX4_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX4_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX5", "varchar(12) NULL")
        coll.Add("TAX5_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX5_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX5_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX6", "varchar(12) NULL")
        coll.Add("TAX6_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX6_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX6_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX7", "varchar(12) NULL")
        coll.Add("TAX7_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX7_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX7_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX8", "varchar(12) NULL")
        coll.Add("TAX8_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX8_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX8_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX9", "varchar(12) NULL")
        coll.Add("TAX9_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX9_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX9_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX10", "varchar(12) NULL")
        coll.Add("TAX10_Base_Amt", "decimal(18, 6) NULL")
        coll.Add("TAX10_Rate", "decimal(18, 6) NULL")
        coll.Add("TAX10_Amt", "decimal(18, 6) NULL")
        coll.Add("Total_Tax_Amt", "decimal(18, 6) NULL")
        coll.Add("Total_Amt", "decimal(18, 6) null")
        coll.Add("Inclusive_Tax", "decimal(18, 6) null")
        coll.Add("Inclusive_TPT", "decimal(18, 6) null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_CUSTOMER_TENDER_ORDER_DETAIL", coll, "", True, False, "TSPL_CUSTOMER_TENDER_ORDER", "Document_Code", "", True)
    End Sub
    Private Sub LoadGrid(ByVal strCode As String, ByVal strCustCode As String)
        Try
            Dim strqry As String = "select TSPL_CUSTOMER_TENDER.Item_Code,TSPL_ITEM_MASTER.Short_Description,TSPL_CUSTOMER_TENDER_DETAIL.Item_Rate,TSPL_CUSTOMER_TENDER.Inclusive_Tax,TSPL_CUSTOMER_TENDER.Inclusive_TPT from TSPL_CUSTOMER_TENDER
left join TSPL_CUSTOMER_TENDER_DETAIL on TSPL_CUSTOMER_TENDER_DETAIL.Document_Code=TSPL_CUSTOMER_TENDER.Document_Code
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_TENDER.Item_Code
where TSPL_CUSTOMER_TENDER.Document_Code='" & strCode & "' and TSPL_CUSTOMER_TENDER_DETAIL.Cust_Code='" & strCustCode & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                isInsideLoadData = True
                LoadBlankGrid()
                LoadBlankGridTax()
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = clsItemRowType.RowTypeItem
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).ReadOnly = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Short_Description"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = "LTR"
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTenderRate).Value = clsCommon.myCdbl(dr("Item_Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemAmt).ReadOnly = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInclusiveTax).Value = clsCommon.myCdbl(dr("Inclusive_Tax"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colInclusiveTPT).Value = clsCommon.myCdbl(dr("Inclusive_TPT"))
                    SetTax(clsCommon.myCstr(dr("Item_Code")), gv1.Rows.Count - 1)

                Next
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetTax(ByVal Item_Code As String, ByVal introw As Integer)
        Dim isTaxable As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where Item_Code='" & Item_Code & "'")) = 1, True, False)
        GSTStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
        If Not GSTStatus OrElse (isTaxable AndAlso GSTStatus) Then
            If CalculateTaxRatefromItemwsieTaxOnSale Then
                If clsCommon.myLen(txtLocation.Value) > 0 Then
                    Dim strTaxType As String = clsLocationWiseTax.TaxType(txtLocation.Value, txtCustomerCode.Value, "S", txtDate.Value, Nothing)
                    If GSTStatus AndAlso clsCommon.CompairString(strTaxType, "L") = CompairStringResult.Equal Then
                        txtTaxGroup.Value = clsItemWiseTaxAuthority.GetTaxGroupItemWise("L", "S", txtDate.Value, Item_Code)
                    Else
                        txtTaxGroup.Value = clsItemWiseTaxAuthority.GetTaxGroupItemWise("I", "S", txtDate.Value, Item_Code)
                    End If
                Else
                    txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtLocation.Value, txtCustomerCode.Value, "S", txtDate.Value)
                End If
            Else
                txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroup(txtLocation.Value, txtCustomerCode.Value, "S", txtDate.Value)
                lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
            End If
        Else
            If Not isTaxable Then
                txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, txtLocation.Value, txtCustomerCode.Value, "S", txtDate.Value)
                lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
            End If
        End If
        SetTaxDetails(Item_Code, introw)
    End Sub
    Sub SetTaxDetails(ByVal ICode As String, ByVal intRow As Integer)
        LoadBlankGridTax()
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtCustomerCode.Value, txtLocation.Value)
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
                End If
            Next
            SetitemWiseTaxSetting(True, ICode, intRow)
        Else
            lblTaxGrpName.Text = ""
            For ii As Integer = 0 To gv1.Rows.Count - 1
                BlankTaxDetails(ii)
            Next
        End If
        ' For ii As Integer = 0 To gv1.Rows.Count - 1
        'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "BKN") = CompairStringResult.Equal Then
        '    UpdateCurrentRow1(intRow)
        'Else
        '    UpdateCurrentRow(intRow)
        'End If
        '' Next
        'UpdateAllTotals()
    End Sub
    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal ICode As String, ByVal intRowNo As Integer)
        Dim strCustomer As String = ""
        Try
            strCustomer = clsCommon.myCstr(txtCustomerCode.Value)
        Catch ex As Exception
        End Try
        If clsCommon.myLen(strCustomer) <= 0 Then
            strCustomer = txtCustomerCode.Value
        End If
        Dim IsTaxable As Integer = 0
        Dim dt As DataTable = clsTaxCalculation.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", txtCustomerCode.Value, txtLocation.Value, ICode, clsCommon.GetPrintDate(txtDate.Value))
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'For intRowNo As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode).Value) > 0) Then
                BlankTaxDetails(intRowNo, isChangeRate)
                IsTaxable = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsTaxable from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colICode).Value) & "'"))
                If ((clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode).Value) > 0 AndAlso IsTaxable = 1) OrElse (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TAX_MASTER WHERE Tax_Code IN (select Tax_Code  from TSPL_TAX_GROUP_DETAILS WHERE TAX_GROUP_CODE='" & txtTaxGroup.Value & "') AND Is_TCS ='Y'")) > 0)) Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" & strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" & strII & "_Rate")).Value = clsCommon.myCdbl(dr("TaxRate"))
                        ii = ii + 1
                    Next
                End If
            End If
            ' Next
        End If
    End Sub
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        BlankTaxDetails(intRowNo, True)
    End Sub
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer, ByVal isBlankRate As Boolean)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" & strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" & strII & "_BaseAmt")).Value = Nothing
                If isBlankRate Then
                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" & strII & "_Rate")).Value = Nothing
                End If
            Else
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" & strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" & strII & "_BaseAmt")).Value = Nothing
                If isBlankRate Then
                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" & strII & "_Rate")).Value = Nothing
                End If
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" & strII & "_Amt")).Value = Nothing
            End If
        Next
    End Sub
    Function AllowToSave()
        Try
            If clsCommon.myLen(txtRALNo.Value) <= 0 Then
                Throw New Exception("Please select RAL No.")
            End If
            If clsCommon.myLen(txtCustomerCode.Value) <= 0 Then
                Throw New Exception("Please select Customer")
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select location")
            End If
            If clsCommon.myLen(txtLocation.Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtSubLocation.Value) <= 0 Then
                Throw New Exception("Please select sub location")
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)) > 0 Then
                    Throw New Exception("Enter Qty at line no -" & clsCommon.myCstr(ii + 1))
                End If
            Next
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsCustomerTenderOrder()
                obj.Document_Code = txtDocCode.Value
                obj.Document_Date = txtDate.Value
                obj.RAL_No = txtRALNo.Value
                obj.Cust_Code = txtCustomerCode.Value
                obj.Location_Code = txtLocation.Value
                obj.Sub_Location = txtSubLocation.Value
                obj.Ref_No = txtRefNo.Text
                obj.Ref_Date = txtRefDate.Value
                obj.Document_Amt = 0
                obj.Remarks = txtRemark.Text
                obj.Tax_Group = txtTaxGroup.Value
                obj.TaxGroupName = lblTaxGrpName.Text
                If rbtnTaxCalAutomatic.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ElseIf rbtnTaxCalManual.IsChecked Then
                    obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual
                End If
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
                obj.Doc_Amt_Without_Tax = clsCommon.myCdbl(txtDocAmtWithoutTax.Text)
                obj.Tax_Amt = clsCommon.myCdbl(txtTaxAmt.Text)
                obj.Document_Amt = clsCommon.myCdbl(txtDocAmt.Text)
                obj.Arr = GetTRData()
                obj.SaveData(obj, isNewEntry)
                UcAttachment1.SaveData(obj.Document_Code)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function GetTRData() As List(Of clsCustomerTenderOrderDetail)
        Dim Arr As New List(Of clsCustomerTenderOrderDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                Dim objTr As New clsCustomerTenderOrderDetail()
                objTr.RowType = clsCommon.myCstr(gv1.Rows(ii).Cells(colRowType).Value)
                objTr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                objTr.Unit_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colUOM).Value)
                objTr.Tender_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTenderRate).Value)
                objTr.Item_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                objTr.Item_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemAmt).Value)
                objTr.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                objTr.TAX1 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax1).Value)
                objTr.TAX1_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1_BaseAmt).Value)
                objTr.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1_Rate).Value)
                objTr.TAX1_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1_Amt).Value)
                objTr.Tax2 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax2).Value)
                objTr.Tax2_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax2_BaseAmt).Value)
                objTr.Tax2_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax2_Rate).Value)
                objTr.Tax2_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax2_Amt).Value)
                objTr.Tax3 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax3).Value)
                objTr.Tax3_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax3_BaseAmt).Value)
                objTr.Tax3_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax3_Rate).Value)
                objTr.Tax3_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax3_Amt).Value)
                objTr.Tax4 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax4).Value)
                objTr.Tax4_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax4_BaseAmt).Value)
                objTr.Tax4_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax4_Rate).Value)
                objTr.Tax4_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax4_Amt).Value)
                objTr.Tax5 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax5).Value)
                objTr.Tax5_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax5_BaseAmt).Value)
                objTr.Tax5_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax5_Rate).Value)
                objTr.Tax5_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax5_Amt).Value)
                objTr.Tax6 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax6).Value)
                objTr.Tax6_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax6_BaseAmt).Value)
                objTr.Tax6_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax6_Rate).Value)
                objTr.Tax6_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax6_Amt).Value)
                objTr.Tax7 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax7).Value)
                objTr.Tax7_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax7_BaseAmt).Value)
                objTr.Tax7_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax7_Rate).Value)
                objTr.Tax7_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax7_Amt).Value)
                objTr.TAX1 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax1).Value)
                objTr.TAX1_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1_BaseAmt).Value)
                objTr.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1_Rate).Value)
                objTr.TAX1_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1_Amt).Value)
                objTr.Tax9 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax9).Value)
                objTr.Tax9_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax9_BaseAmt).Value)
                objTr.Tax9_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax9_Rate).Value)
                objTr.Tax9_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax9_Amt).Value)
                objTr.Tax10 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax10).Value)
                objTr.Tax10_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax10_BaseAmt).Value)
                objTr.Tax10_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax10_Rate).Value)
                objTr.Tax10_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax10_Amt).Value)
                objTr.Total_Tax_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalTaxAmt).Value)
                objTr.Total_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalAmt).Value)
                objTr.Inclusive_Tax = clsCommon.myCdbl(gv1.Rows(ii).Cells(colInclusiveTax).Value)
                objTr.Inclusive_TPT = clsCommon.myCdbl(gv1.Rows(ii).Cells(colInclusiveTPT).Value)
                If clsCommon.myLen(objTr.Item_Code) > 0 Then
                    Arr.Add(objTr)
                End If
            End If
        Next
        Return Arr
    End Function
    Private Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            Dim obj As New clsCustomerTenderOrder()
            obj = clsCustomerTenderOrder.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                LoadBlankGrid()
                AddNew()
                isNewEntry = False

                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                txtDocCode.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtRALNo.Value = obj.RAL_No
                lblRalNoDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Remarks from TSPL_CUSTOMER_TENDER where Document_Code='" & txtRALNo.Value & "'"))
                txtCustomerCode.Value = obj.Cust_Code
                lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" & txtCustomerCode.Value & "'"))
                txtLocation.Value = obj.Location_Code
                lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtLocation.Value & "'"))
                If clsCommon.myLen(clsCommon.myCstr(txtLocation.Value)) > 0 Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsSubLocationWise,'N') as  IsSubLocationWise from tspl_location_master where location_code='" & clsCommon.myCstr(txtLocation.Value) & "'")), "Y") = CompairStringResult.Equal Then
                        txtSubLocation.Value = obj.Sub_Location
                    Else
                        txtSubLocation.Value = ""
                    End If
                End If
                GetTenderQty(txtRALNo.Value)
                txtRefNo.Text = obj.Ref_No
                txtRefDate.Value = obj.Ref_Date
                LoadBlankGridTax()
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    txtTaxGroup.Value = obj.Tax_Group
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
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAutName).Value = objTaxGrpTr.Tax_Code_Desc
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
                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
                End If
                txtDocAmtWithoutTax.Text = obj.Doc_Amt_Without_Tax
                txtTaxAmt.Text = obj.Tax_Amt
                txtDocAmt.Text = obj.Document_Amt
                txtRemark.Text = obj.Remarks

                Dim sl As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsCustomerTenderOrderDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = sl
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRowType).Value = objTr.RowType
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" & clsCommon.myCstr(objTr.Item_Code) & "' ")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTenderRate).Value = objTr.Tender_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Item_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemAmt).Value = objTr.Item_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = objTr.TAX1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1_BaseAmt).Value = objTr.TAX1_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1_Rate).Value = objTr.TAX1_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1_Amt).Value = objTr.TAX1_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = objTr.Tax2
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2_BaseAmt).Value = objTr.Tax2_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2_Rate).Value = objTr.Tax2_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2_Amt).Value = objTr.Tax2_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = objTr.Tax3
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3_BaseAmt).Value = objTr.Tax3_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3_Rate).Value = objTr.Tax3_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3_Amt).Value = objTr.Tax3_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = objTr.Tax4
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4_BaseAmt).Value = objTr.Tax4_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4_Rate).Value = objTr.Tax4_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4_Amt).Value = objTr.Tax4_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = objTr.Tax5
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5_BaseAmt).Value = objTr.Tax5_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5_Rate).Value = objTr.Tax5_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5_Amt).Value = objTr.Tax5_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = objTr.Tax6
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6_BaseAmt).Value = objTr.Tax6_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6_Rate).Value = objTr.Tax6_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6_Amt).Value = objTr.Tax6_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = objTr.Tax7
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7_BaseAmt).Value = objTr.Tax7_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7_Rate).Value = objTr.Tax7_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7_Amt).Value = objTr.Tax7_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = objTr.Tax8
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8_BaseAmt).Value = objTr.Tax8_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8_Rate).Value = objTr.Tax8_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8_Amt).Value = objTr.Tax8_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = objTr.Tax9
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9_BaseAmt).Value = objTr.Tax9_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9_Rate).Value = objTr.Tax9_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9_Amt).Value = objTr.Tax9_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = objTr.Tax10
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10_BaseAmt).Value = objTr.Tax10_Base_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10_Rate).Value = objTr.Tax10_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10_Amt).Value = objTr.Tax10_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalTaxAmt).Value = objTr.Total_Tax_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAmt).Value = objTr.Total_Amt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInclusiveTax).Value = objTr.Inclusive_Tax
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colInclusiveTPT).Value = objTr.Inclusive_TPT
                        sl += 1
                    Next
                End If
                UcAttachment1.LoadData(obj.Document_Code)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colItemAmt) Then
                        If clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > 0 Then
                            If clsCommon.myCdbl(txtBalQty.Text) >= clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) Then
                                UpdateCurrentRow(gv1.CurrentRow.Index)

                            Else
                                gv1.CurrentRow.Cells(colQty).Value = 0
                                Throw New Exception("Item [" & clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value) & "] out of stock! Current available balance is [" & clsCommon.myCstr(txtBalQty.Text) & "]")
                            End If
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                            ICodeList()
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal AndAlso clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colItemAmt).Value) > 0 Then
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            'UpdateAllTotals()

                        End If
                        setGridFocus()
                    End If
                End If
                UpdateAllTotals()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ICodeList()
        Try
            Dim obj As clsAdditionalCharge = clsAdditionalCharge.GetFinder(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), False)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                gv1.CurrentRow.Cells(colICode).Value = obj.Code
                gv1.CurrentRow.Cells(colIName).Value = obj.desc
                gv1.CurrentRow.Cells(colUOM).Value = Nothing
                gv1.CurrentRow.Cells(colQty).Value = Nothing
                gv1.CurrentRow.Cells(colRate).Value = Nothing
                gv1.CurrentRow.Cells(colTax1).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax1).Value
                gv1.CurrentRow.Cells(colTax1_Rate).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax1_Rate).Value
                gv1.CurrentRow.Cells(colTax2).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax2).Value
                gv1.CurrentRow.Cells(colTax2_Rate).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax2_Rate).Value
                gv1.CurrentRow.Cells(colTax3).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax3).Value
                gv1.CurrentRow.Cells(colTax3_Rate).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax3_Rate).Value
                gv1.CurrentRow.Cells(colTax4).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax4).Value
                gv1.CurrentRow.Cells(colTax4_Rate).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax4_Rate).Value
                gv1.CurrentRow.Cells(colTax5).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax5).Value
                gv1.CurrentRow.Cells(colTax5_Rate).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax5_Rate).Value
                gv1.CurrentRow.Cells(colTax6).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax6).Value
                gv1.CurrentRow.Cells(colTax6_Rate).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax6_Rate).Value
                gv1.CurrentRow.Cells(colTax7).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax7).Value
                gv1.CurrentRow.Cells(colTax7_Rate).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax7_Rate).Value
                gv1.CurrentRow.Cells(colTax8).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax8).Value
                gv1.CurrentRow.Cells(colTax8_Rate).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax8_Rate).Value
                gv1.CurrentRow.Cells(colTax9).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax9).Value
                gv1.CurrentRow.Cells(colTax9_Rate).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax9_Rate).Value
                gv1.CurrentRow.Cells(colTax10).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax10).Value
                gv1.CurrentRow.Cells(colTax10_Rate).Value = gv1.Rows(gv1.Rows.Count - 2).Cells(colTax10_Rate).Value
            Else
                gv1.CurrentRow.Cells(colICode).Value = ""
                gv1.CurrentRow.Cells(colIName).Value = ""
                gv1.CurrentRow.Cells(colUOM).Value = ""
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colTotalTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked AndAlso clsCommon.CompairString(gv1.CurrentRow.Cells(colRowType).Value, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                Dim frm As New FrmPOItemTaxDetails()
                frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotalTaxAmt).Value)
                ''New Column for location wise
                frm.strTaxGroup = txtTaxGroup.Value
                frm.strTransLocation = txtLocation.Value
                frm.strTaxType = "S"
                frm.strVendorCustomerCode = txtCustomerCode.Value

                frm.PurchaseModulePickFixTaxRate = True
                frm.IsTaxableItem = clsCommon.myCBool(gv1.CurrentRow.Cells(colInclusiveTPT).Value)

                If clsCommon.myLen(frm.strItemCode) > 0 Then
                    frm.ArrIn = New List(Of clsTempItemTaxDetails)
                    For ii As Integer = 1 To 10
                        Dim strii As String = clsCommon.myCstr(ii)
                        Dim obj As New clsTempItemTaxDetails()
                        obj.AuthorityCode = clsCommon.myCstr(gv1.CurrentRow.Cells("COLTAX" & strii).Value)
                        If clsCommon.myLen(obj.AuthorityCode) > 0 Then
                            obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" & obj.AuthorityCode & "'"))
                            obj.Rate = clsCommon.myCdbl(gv1.CurrentRow.Cells("colTax" & strii & "_Rate").Value)
                            obj.BaseAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells(colItemAmt).Value)
                            obj.TaxAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("colTax" & strii & "_Amt"))
                            'obj.isSurTax = clsCommon.myCBool(gv1.CurrentRow.Cells("ISSURTAX" + strii).Value)
                            'obj.SurTax = clsCommon.myCstr(gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value)
                            'obj.IsTaxable = clsCommon.myCBool(gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value)
                            'obj.TaxOnBaseAmount = clsCommon.myCBool(gv1.CurrentRow.Cells("colTaxOnBaseAmt" + strii).Value)
                            frm.ArrIn.Add(obj)
                        End If
                    Next
                    frm.ShowDialog()
                    If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
                        BlankTaxDetails(gv1.CurrentRow.Index)
                        For ii As Integer = 0 To frm.ArrOut.Count - 1
                            Dim strii As String = clsCommon.myCstr(ii + 1)
                            gv1.CurrentRow.Cells("COLTAX" & strii).Value = frm.ArrOut(ii).AuthorityCode
                            gv1.CurrentRow.Cells("colTax" & strii & "_Rate").Value = frm.ArrOut(ii).Rate
                            gv1.CurrentRow.Cells("colTax" & strii & "_BaseAmt").Value = frm.ArrOut(ii).BaseAmt
                            gv1.CurrentRow.Cells("colTax" & strii & "_Amt").Value = frm.ArrOut(ii).TaxAmt
                            'gv1.CurrentRow.Cells("ISSURTAX" + strii).Value = frm.ArrOut(ii).isSurTax
                            'gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value = frm.ArrOut(ii).SurTax
                            'gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value = frm.ArrOut(ii).IsTaxable
                            'gv1.CurrentRow.Cells("colTaxOnBaseAmt" + strii).Value = frm.ArrOut(ii).TaxOnBaseAmount
                        Next
                        gv1.CurrentRow.Cells(colTotalTaxAmt).Value = frm.dblTotTax
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                    End If
                End If
                'End If



            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function TruncateToDecimalPlaces(value As Double, decimalPlaces As Integer) As Double
        Try
            Dim factor As Double = Math.Pow(10, decimalPlaces)
            Return Math.Truncate(value * factor) / factor
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
            Dim strii As String = clsCommon.myCstr(ii)
            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" & strii)).Value), "TCS") <> CompairStringResult.Equal Then
                If IntRowNo < 0 Then
                    dblTotTax = dblTotTax + Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" & strii & "_AMT")).Value), 2)
                Else
                    dblTotTax = dblTotTax + Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" & strii & "_AMT")).Value), 2)
                End If
            End If

        Next
        Return dblTotTax
    End Function
    Private Sub UpdateCurrentRow(ByVal intRow As Integer)
        Try
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRow).Cells(colRowType).Value), clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Dim dblBasicAmt As Decimal = 0
                Dim dblKKFTaxRate As Decimal = 0
                Dim dblMNDTaxRate As Decimal = 0
                Dim dblGSTTaxRate As New List(Of Decimal)
                Dim dblGSTTaxValue1 As Decimal = 0
                Dim dblGSTTaxValue2 As Decimal = 0
                Dim dblKKFTaxValue As Decimal = 0
                Dim dblMNDTaxValue As Decimal = 0
                Dim dblTotalTaxValue As Decimal = 0
                Dim dblKKFMNDBaseAmt As Decimal = 0
                Dim dblTaxableValue As Decimal = 0
                Dim dblProductValue As Decimal = 0
                Dim dblQty As Decimal = clsCommon.myCDecimal(gv1.Rows(intRow).Cells(colQty).Value)
                Dim dblRate As Decimal = clsCommon.myCDecimal(gv1.Rows(intRow).Cells(colTenderRate).Value)

                'gv1.Rows(intRow).Cells(colItemAmt).Value = clsCommon.myCDecimal(gv1.Rows(intRow).Cells(colQty).Value) * clsCommon.myCDecimal(gv1.Rows(intRow).Cells(colRate).Value)

                If clsCommon.myCdbl(gv1.Rows(intRow).Cells(colInclusiveTax).Value) = 1 Then
                    dblBasicAmt = dblQty * dblRate
                    For ii As Integer = 1 To 10
                        Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & clsCommon.myCstr(ii))).Value)

                        If clsCommon.myLen(strTaxCode) > 0 Then
                            If clsCommon.CompairString(clsTaxCalculation.GetTaxType(strTaxCode, Nothing), "K") = CompairStringResult.Equal Then
                                dblKKFTaxRate = clsCommon.myCDecimal(gv1.Rows(intRow).Cells("COLTAX" & clsCommon.myCstr(ii) & "_RATE").Value)
                            ElseIf clsCommon.CompairString(clsTaxCalculation.GetTaxType(strTaxCode, Nothing), "M") = CompairStringResult.Equal Then
                                dblMNDTaxRate = clsCommon.myCDecimal(gv1.Rows(intRow).Cells("COLTAX" & clsCommon.myCstr(ii) & "_RATE").Value)
                            ElseIf clsCommon.CompairString(clsTaxCalculation.GetTaxType(strTaxCode, Nothing), "GST") = CompairStringResult.Equal Then
                                dblGSTTaxRate.Add(clsCommon.myCDecimal(gv1.Rows(intRow).Cells("COLTAX" & clsCommon.myCstr(ii) & "_RATE").Value))
                            End If
                        End If
                    Next
                    If dblGSTTaxRate.Count = 1 Then
                        dblGSTTaxValue1 = clsCommon.myRoundOFF(TruncateToDecimalPlaces(dblBasicAmt / (100 + dblGSTTaxRate(0)) * dblGSTTaxRate(0), 3), 2, 4)
                    ElseIf dblGSTTaxRate.Count = 2 Then
                        dblGSTTaxValue1 = clsCommon.myRoundOFF(TruncateToDecimalPlaces(dblBasicAmt / (100 + dblGSTTaxRate(0) + dblGSTTaxRate(1)) * dblGSTTaxRate(0), 3), 2, 4)
                        dblGSTTaxValue2 = clsCommon.myRoundOFF(TruncateToDecimalPlaces(dblBasicAmt / (100 + dblGSTTaxRate(0) + dblGSTTaxRate(1)) * dblGSTTaxRate(1), 3), 2, 4)
                    End If
                    dblKKFMNDBaseAmt = clsCommon.myRoundOFF(dblBasicAmt - (dblGSTTaxValue1 + dblGSTTaxValue2), 2, 4)
                    dblKKFTaxValue = clsCommon.myRoundOFF(TruncateToDecimalPlaces(dblKKFMNDBaseAmt / (100 + dblKKFTaxRate + dblMNDTaxRate) * dblKKFTaxRate, 3), 2, 4)
                    dblMNDTaxValue = clsCommon.myRoundOFF(TruncateToDecimalPlaces(dblKKFMNDBaseAmt / (100 + dblKKFTaxRate + dblMNDTaxRate) * dblMNDTaxRate, 3), 2, 4)
                    dblTotalTaxValue = dblGSTTaxValue1 + dblGSTTaxValue2 + dblKKFTaxValue + dblMNDTaxValue
                    dblTaxableValue = dblBasicAmt - (dblGSTTaxValue1 + dblGSTTaxValue2)
                    dblProductValue = dblTaxableValue - (dblKKFTaxValue + dblMNDTaxValue)
                    gv1.Rows(intRow).Cells(colItemAmt).Value = dblBasicAmt - dblTotalTaxValue
                    gv1.Rows(intRow).Cells(colRate).Value = (dblBasicAmt - dblTotalTaxValue) / dblQty
                    For ii As Integer = 1 To 10
                        Dim Strii As String = clsCommon.myCstr(ii)
                        If rbtnTaxCalAutomatic.IsChecked Then
                            Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii)).Value)
                            If clsCommon.myLen(strTaxCode) > 0 Then
                                If clsCommon.CompairString(clsTaxCalculation.GetTaxType(strTaxCode, Nothing), "K") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dblKKFTaxValue
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblProductValue, 2, 4)
                                ElseIf clsCommon.CompairString(clsTaxCalculation.GetTaxType(strTaxCode, Nothing), "M") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dblMNDTaxValue
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblProductValue, 2, 4)
                                ElseIf clsCommon.CompairString(strTaxCode, "CGST") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dblGSTTaxValue1
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblTaxableValue, 2, 4)
                                ElseIf clsCommon.CompairString(strTaxCode, "SGST") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dblGSTTaxValue2
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblTaxableValue, 2, 4)
                                ElseIf clsCommon.CompairString(strTaxCode, "IGST") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dblGSTTaxValue1
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblTaxableValue, 2, 4)
                                ElseIf clsCommon.CompairString(strTaxCode, "EXEMPTED") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = 0
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblTaxableValue, 3, 4)

                                End If
                            Else
                                gv1.Rows(intRow).Cells(clsCommon.myCstr("colTax" & Strii)).Value = Nothing
                                gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = Nothing
                                gv1.Rows(intRow).Cells(clsCommon.myCstr("colTax" & Strii & "_Rate")).Value = Nothing
                                gv1.Rows(intRow).Cells(clsCommon.myCstr("colTax" & Strii & "_Amt")).Value = Nothing
                            End If
                        ElseIf rbtnTaxCalManual.IsChecked Then
                            If gv2.Rows.Count >= ii Then
                                Dim dblTaxAmt As Decimal = clsCommon.myCDecimal(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                                Dim dblCurrRowAmt As Decimal = clsCommon.myCDecimal(gv1.Rows(clsCommon.myCDecimal(intRow)).Cells(colItemAmt).Value)
                                Dim dblTotAmt As Decimal = 0
                                For jj As Integer = 0 To gv1.Rows.Count - 1
                                    dblTotAmt += clsCommon.myCDecimal(gv1.Rows(jj).Cells(colItemAmt).Value)
                                Next
                                Dim dblCurrCalTax As Decimal = 0
                                If dblTotAmt <> 0 Then
                                    dblCurrCalTax = clsCommon.myRoundOFF(clsCommon.myCDecimal(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, 4)
                                End If
                                gv1.Rows(intRow).Cells(clsCommon.myCstr("colTax" & Strii & "_Amt")).Value = dblCurrCalTax
                            End If
                        End If

                    Next
                    Dim TotalTaxAmt As Decimal = GetCurrentRowTotalTaxAmt(intRow)
                    Dim Baseamt As Decimal = clsCommon.myCDecimal(gv1.Rows(intRow).Cells(colItemAmt).Value)
                    gv1.Rows(intRow).Cells(colTotalTaxAmt).Value = TotalTaxAmt
                    gv1.Rows(intRow).Cells(colTotalAmt).Value = Baseamt + TotalTaxAmt
                Else
                    dblBasicAmt = dblQty * dblRate
                    For ii As Integer = 1 To 10
                        Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & clsCommon.myCstr(ii))).Value)

                        If clsCommon.myLen(strTaxCode) > 0 Then
                            If clsCommon.CompairString(clsTaxCalculation.GetTaxType(strTaxCode, Nothing), "K") = CompairStringResult.Equal Then
                                dblKKFTaxRate = clsCommon.myCDecimal(gv1.Rows(intRow).Cells("COLTAX" & clsCommon.myCstr(ii) & "_RATE").Value)
                            ElseIf clsCommon.CompairString(clsTaxCalculation.GetTaxType(strTaxCode, Nothing), "M") = CompairStringResult.Equal Then
                                dblMNDTaxRate = clsCommon.myCDecimal(gv1.Rows(intRow).Cells("COLTAX" & clsCommon.myCstr(ii) & "_RATE").Value)
                            ElseIf clsCommon.CompairString(clsTaxCalculation.GetTaxType(strTaxCode, Nothing), "GST") = CompairStringResult.Equal Then
                                dblGSTTaxRate.Add(clsCommon.myCDecimal(gv1.Rows(intRow).Cells("COLTAX" & clsCommon.myCstr(ii) & "_RATE").Value))
                            End If
                        End If
                    Next
                    dblKKFMNDBaseAmt = clsCommon.myRoundOFF(dblBasicAmt, 2, 4)
                    dblKKFTaxValue = clsCommon.myRoundOFF(TruncateToDecimalPlaces(dblKKFMNDBaseAmt * (dblKKFTaxRate / 100), 3), 2, 4)
                    dblMNDTaxValue = clsCommon.myRoundOFF(TruncateToDecimalPlaces(dblKKFMNDBaseAmt * (dblMNDTaxRate / 100), 3), 2, 4)
                    If dblGSTTaxRate.Count = 1 Then
                        dblGSTTaxValue1 = clsCommon.myRoundOFF(TruncateToDecimalPlaces((dblKKFTaxValue + dblMNDTaxValue + dblBasicAmt) * (dblGSTTaxRate(0) / 100), 3), 2, 4)
                    ElseIf dblGSTTaxRate.Count = 2 Then
                        dblGSTTaxValue1 = clsCommon.myRoundOFF(TruncateToDecimalPlaces((dblKKFTaxValue + dblMNDTaxValue + dblBasicAmt) * (dblGSTTaxRate(0) / 100), 3), 2, 4)
                        dblGSTTaxValue2 = clsCommon.myRoundOFF(TruncateToDecimalPlaces((dblKKFTaxValue + dblMNDTaxValue + dblBasicAmt) * (dblGSTTaxRate(1) / 100), 3), 2, 4)
                    End If
                    dblTotalTaxValue = dblGSTTaxValue1 + dblGSTTaxValue2 + dblKKFTaxValue + dblMNDTaxValue
                    dblTaxableValue = dblBasicAmt + (dblKKFTaxValue + dblMNDTaxValue)
                    dblProductValue = dblBasicAmt
                    gv1.Rows(intRow).Cells(colItemAmt).Value = dblProductValue
                    gv1.Rows(intRow).Cells(colRate).Value = clsCommon.myRoundOFF(TruncateToDecimalPlaces(dblProductValue / dblQty, 3), 2, 5)

                    For ii As Integer = 1 To 10
                        Dim Strii As String = clsCommon.myCstr(ii)
                        If rbtnTaxCalAutomatic.IsChecked Then
                            Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                            If clsCommon.myLen(strTaxCode) > 0 Then
                                If clsCommon.CompairString(clsTaxCalculation.GetTaxType(strTaxCode, Nothing), "K") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dblKKFTaxValue
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblProductValue, 2, 4)
                                ElseIf clsCommon.CompairString(clsTaxCalculation.GetTaxType(strTaxCode, Nothing), "M") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dblMNDTaxValue
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblProductValue, 2, 4)
                                ElseIf clsCommon.CompairString(strTaxCode, "CGST") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dblGSTTaxValue1
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblTaxableValue, 2, 4)
                                ElseIf clsCommon.CompairString(strTaxCode, "SGST") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dblGSTTaxValue2
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblTaxableValue, 2, 4)
                                ElseIf clsCommon.CompairString(strTaxCode, "IGST") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dblGSTTaxValue1
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "BASEAMT")).Value = clsCommon.myRoundOFF(dblTaxableValue, 2, 4)
                                ElseIf clsCommon.CompairString(strTaxCode, "EXEMPTED") = CompairStringResult.Equal Then
                                    gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = 0
                                    gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = clsCommon.myRoundOFF(dblTaxableValue, 3, 4)

                                End If
                            Else
                                gv1.Rows(intRow).Cells(clsCommon.myCstr("colTax" & Strii)).Value = Nothing
                                gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" & Strii & "_BASEAMT")).Value = Nothing
                                gv1.Rows(intRow).Cells(clsCommon.myCstr("colTax" & Strii & "_Rate")).Value = Nothing
                                gv1.Rows(intRow).Cells(clsCommon.myCstr("colTax" & Strii & "_Amt")).Value = Nothing
                            End If
                        ElseIf rbtnTaxCalManual.IsChecked Then
                            If gv2.Rows.Count >= ii Then
                                Dim dblTaxAmt As Decimal = clsCommon.myCDecimal(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                                Dim dblCurrRowAmt As Decimal = clsCommon.myCDecimal(gv1.Rows(clsCommon.myCDecimal(intRow)).Cells(colItemAmt).Value)
                                Dim dblTotAmt As Decimal = 0
                                For jj As Integer = 0 To gv1.Rows.Count - 1
                                    dblTotAmt += clsCommon.myCDecimal(gv1.Rows(jj).Cells(colItemAmt).Value)
                                Next
                                Dim dblCurrCalTax As Decimal = 0
                                If dblTotAmt <> 0 Then
                                    dblCurrCalTax = clsCommon.myRoundOFF(clsCommon.myCDecimal(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, 4)
                                End If
                                gv1.Rows(intRow).Cells(clsCommon.myCstr("colTax" & Strii & "_Amt")).Value = dblCurrCalTax
                            End If
                        End If

                    Next
                    Dim TotalTaxAmt As Decimal = GetCurrentRowTotalTaxAmt(intRow)
                    Dim Baseamt As Decimal = clsCommon.myCDecimal(gv1.Rows(intRow).Cells(colItemAmt).Value)
                    gv1.Rows(intRow).Cells(colTotalTaxAmt).Value = TotalTaxAmt
                    gv1.Rows(intRow).Cells(colTotalAmt).Value = Baseamt + TotalTaxAmt
                End If

            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(intRow).Cells(colRowType).Value), clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                For ii As Integer = 1 To 10

                    Dim Strii As String = clsCommon.myCstr(ii)
                    Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(intRow).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)

                    If clsCommon.myLen(strTaxCode) > 0 Then
                        Dim dbltaxRate As Decimal = clsCommon.myCDecimal(gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Rate").Value)
                        Dim dbltaxBaseAmt As Decimal = clsCommon.myCDecimal(gv1.Rows(intRow).Cells(colItemAmt).Value)
                        gv1.Rows(intRow).Cells("colTax" & clsCommon.myCstr(ii) & "_Amt").Value = dbltaxBaseAmt * dbltaxRate / 100
                    End If
                Next
                Dim TotalTaxAmt As Decimal = GetCurrentRowTotalTaxAmt(intRow)
                Dim Baseamt As Decimal = clsCommon.myCDecimal(gv1.Rows(intRow).Cells(colItemAmt).Value)
                gv1.Rows(intRow).Cells(colTotalTaxAmt).Value = TotalTaxAmt
                gv1.Rows(intRow).Cells(colTotalAmt).Value = Baseamt + TotalTaxAmt
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub UpdateAllTotals()
        Try
            Dim dblTotAmt As Decimal = 0
            Dim dblTotalTaxAmt As Decimal = 0
            Dim dblAmtwithTax As Decimal = 0
            Dim dblTaxBaseAmt1 As Decimal = 0
            Dim dblTaxBaseAmt2 As Decimal = 0
            Dim dblTaxBaseAmt3 As Decimal = 0
            Dim dblTaxBaseAmt4 As Decimal = 0
            Dim dblTaxBaseAmt5 As Decimal = 0
            Dim dblTaxBaseAmt6 As Decimal = 0
            Dim dblTaxBaseAmt7 As Decimal = 0
            Dim dblTaxBaseAmt8 As Decimal = 0
            Dim dblTaxBaseAmt9 As Decimal = 0
            Dim dblTaxBaseAmt10 As Decimal = 0
            Dim dblTaxAmt1 As Decimal = 0
            Dim dblTaxAmt2 As Decimal = 0
            Dim dblTaxAmt3 As Decimal = 0
            Dim dblTaxAmt4 As Decimal = 0
            Dim dblTaxAmt5 As Decimal = 0
            Dim dblTaxAmt6 As Decimal = 0
            Dim dblTaxAmt7 As Decimal = 0
            Dim dblTaxAmt8 As Decimal = 0
            Dim dblTaxAmt9 As Decimal = 0
            Dim dblTaxAmt10 As Decimal = 0

            For ii As Integer = 0 To gv1.Rows.Count - 1
                dblTaxAmt1 = dblTaxAmt1 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1_Amt).Value), 2)
                dblTaxAmt2 = dblTaxAmt2 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax2_Amt).Value), 2)
                dblTaxAmt3 = dblTaxAmt3 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax3_Amt).Value), 2)
                dblTaxAmt4 = dblTaxAmt4 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax4_Amt).Value), 2)
                dblTaxAmt5 = dblTaxAmt5 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax5_Amt).Value), 2)
                dblTaxAmt6 = dblTaxAmt6 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax6_Amt).Value), 2)
                dblTaxAmt7 = dblTaxAmt7 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax7_Amt).Value), 2)
                dblTaxAmt8 = dblTaxAmt8 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax8_Amt).Value), 2)
                dblTaxAmt9 = dblTaxAmt9 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax9_Amt).Value), 2)
                dblTaxAmt10 = dblTaxAmt10 + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax10_Amt).Value), 2)
                dblTaxBaseAmt1 = dblTaxBaseAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax1_BaseAmt).Value)
                dblTaxBaseAmt2 = dblTaxBaseAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax2_BaseAmt).Value)
                dblTaxBaseAmt3 = dblTaxBaseAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax3_BaseAmt).Value)
                dblTaxBaseAmt4 = dblTaxBaseAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax4_BaseAmt).Value)
                dblTaxBaseAmt5 = dblTaxBaseAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax5_BaseAmt).Value)
                dblTaxBaseAmt6 = dblTaxBaseAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax6_BaseAmt).Value)
                dblTaxBaseAmt7 = dblTaxBaseAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax7_BaseAmt).Value)
                dblTaxBaseAmt8 = dblTaxBaseAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax8_BaseAmt).Value)
                dblTaxBaseAmt9 = dblTaxBaseAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax9_BaseAmt).Value)
                dblTaxBaseAmt10 = dblTaxBaseAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTax10_BaseAmt).Value)
                dblTotalTaxAmt = dblTotalTaxAmt + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalTaxAmt).Value), 2)
                dblAmtwithTax = dblAmtwithTax + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colItemAmt).Value), 2)
                dblTotAmt = dblTotAmt + Math.Round(clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalAmt).Value), 2)
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
            txtDocAmtWithoutTax.Text = dblAmtwithTax
            txtTaxAmt.Text = dblTotalTaxAmt
            txtDocAmt.Text = dblTotAmt


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Public Sub PostData()
        Try
            If clsCommon.myLen(txtDocCode.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" & txtDocCode.Value & "]" & Environment.NewLine & "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsCustomerTenderOrder.PostData(clsCommon.myCstr(txtDocCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtDocCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                clsCustomerTenderOrder.DeleteData(txtDocCode.Value)
                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub txtTaxGroup__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTaxGroup._MYValidating
        Try
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                Dim strCustomer As String = ""
                Try
                    strCustomer = txtCustomerCode.Value
                Catch ex As Exception
                End Try
                If clsCommon.myLen(strCustomer) <= 0 Then
                    strCustomer = txtCustomerCode.Value
                End If
                Dim qry As String = "select Tax_Group_Code as Code,Tax_Group_Desc as Description from TSPL_TAX_GROUP_MASTER "
                txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroup(txtLocation.Value, strCustomer, "S", txtTaxGroup.Value, isButtonClicked)
                ' SetTaxDetails()
            Else
                Throw New Exception("Please select Location First")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmSalesOrder_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
                isCellValueChangedOpen = True
                setGridFocus()
                isCellValueChangedOpen = False
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control AndAlso e.KeyCode = Keys.F12 Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverseAndUnPost.Visible = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnReverseAndUnPost_Click(sender As Object, e As EventArgs) Handles btnReverseAndUnPost.Click
        Try
            If clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" & Environment.NewLine & "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsCustomerTenderOrder.ReverseAndUnpost(txtDocCode.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class