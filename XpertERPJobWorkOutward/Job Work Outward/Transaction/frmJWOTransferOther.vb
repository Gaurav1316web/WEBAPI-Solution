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
Imports common
Imports XpertERPEngine

Public Class frmJWOTransferOther
    Inherits FrmMainTranScreen
#Region "Variables"
    Private IsFormLoad As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colIHSN As String = "colIHSN"
    Const colIsBatchItem As String = "colIsBatchItem"
    Const colUnit As String = "COLUNIT"
    Const colQty As String = "COLOutQty"
    Const colRate As String = "colRate"
    Const colAmount As String = "colAmount"

    Const colTax1 As String = "COLTAX1"
    Const colTaxBaseAmt1 As String = "COLTAXBASEAMT1"
    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxAmt1 As String = "COLTAXAMT1"
    Const colIsTaxable1 As String = "ISTAXABLE1"
    Const colIsSurTax1 As String = "ISSURTAX1"
    Const colSurTaxCode1 As String = "SURTAXCODE1"
    Const colIsExcisable1 As String = "ISEXCISABLE1"

    Const colTax2 As String = "COLTAX2"
    Const colTaxBaseAmt2 As String = "COLTAXBASEAMT2"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxAmt2 As String = "COLTAXAMT2"
    Const colIsTaxable2 As String = "ISTAXABLE2"
    Const colIsSurTax2 As String = "ISSURTAX2"
    Const colSurTaxCode2 As String = "SURTAXCODE2"
    Const colIsExcisable2 As String = "ISEXCISABLE2"

    Const colTax3 As String = "COLTAX3"
    Const colTaxBaseAmt3 As String = "COLTAXBASEAMT3"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxAmt3 As String = "COLTAXAMT3"
    Const colIsTaxable3 As String = "ISTAXABLE3"
    Const colIsSurTax3 As String = "ISSURTAX3"
    Const colSurTaxCode3 As String = "SURTAXCODE3"
    Const colIsExcisable3 As String = "ISEXCISABLE3"

    Const colTax4 As String = "COLTAX4"
    Const colTaxBaseAmt4 As String = "COLTAXBASEAMT4"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxAmt4 As String = "COLTAXAMT4"
    Const colIsTaxable4 As String = "ISTAXABLE4"
    Const colIsSurTax4 As String = "ISSURTAX4"
    Const colSurTaxCode4 As String = "SURTAXCODE4"
    Const colIsExcisable4 As String = "ISEXCISABLE4"

    Const colTax5 As String = "COLTAX5"
    Const colTaxBaseAmt5 As String = "COLTAXBASEAMT5"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxAmt5 As String = "COLTAXAMT5"
    Const colIsTaxable5 As String = "ISTAXABLE5"
    Const colIsSurTax5 As String = "ISSURTAX5"
    Const colSurTaxCode5 As String = "SURTAXCODE5"
    Const colIsExcisable5 As String = "ISEXCISABLE5"

    Const colTax6 As String = "COLTAX6"
    Const colTaxBaseAmt6 As String = "COLTAXBASEAMT6"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxAmt6 As String = "COLTAXAMT6"
    Const colIsTaxable6 As String = "ISTAXABLE6"
    Const colIsSurTax6 As String = "ISSURTAX6"
    Const colSurTaxCode6 As String = "SURTAXCODE6"
    Const colIsExcisable6 As String = "ISEXCISABLE6"

    Const colTax7 As String = "COLTAX7"
    Const colTaxBaseAmt7 As String = "COLTAXBASEAMT7"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxAmt7 As String = "COLTAXAMT7"
    Const colIsTaxable7 As String = "ISTAXABLE7"
    Const colIsSurTax7 As String = "ISSURTAX7"
    Const colSurTaxCode7 As String = "SURTAXCODE7"
    Const colIsExcisable7 As String = "ISEXCISABLE7"

    Const colTax8 As String = "COLTAX8"
    Const colTaxBaseAmt8 As String = "COLTAXBASEAMT8"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxAmt8 As String = "COLTAXAMT8"
    Const colIsTaxable8 As String = "ISTAXABLE8"
    Const colIsSurTax8 As String = "ISSURTAX8"
    Const colSurTaxCode8 As String = "SURTAXCODE8"
    Const colIsExcisable8 As String = "ISEXCISABLE8"

    Const colTax9 As String = "COLTAX9"
    Const colTaxBaseAmt9 As String = "COLTAXBASEAMT9"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxAmt9 As String = "COLTAXAMT9"
    Const colIsTaxable9 As String = "ISTAXABLE9"
    Const colIsSurTax9 As String = "ISSURTAX9"
    Const colSurTaxCode9 As String = "SURTAXCODE9"
    Const colIsExcisable9 As String = "ISEXCISABLE9"

    Const colTax10 As String = "COLTAX10"
    Const colTaxBaseAmt10 As String = "COLTAXBASEAMT10"
    Const colTaxRate10 As String = "COLTAXRATE10"
    Const colTaxAmt10 As String = "COLTAXAMT10"
    Const colIsTaxable10 As String = "ISTAXABLE10"
    Const colIsSurTax10 As String = "ISSURTAX10"
    Const colSurTaxCode10 As String = "SURTAXCODE10"
    Const colIsExcisable10 As String = "ISEXCISABLE10"

    Const colTotTaxAmt As String = "TAXAMT"
    Const colAmtAfterTax As String = "AMTAFTERTAX"
    Const colItemwiseTaxCode As String = "colItemwiseTaxCode"
    '-----Tax Grid
    Const colTTaxAutCode As String = "TAXAUTCODE"
    Const colTTaxAutName As String = "TAXAUTNAME"
    Const colTTaxRate As String = "TAXRATE"
    Const colTBaseAmt As String = "TAXBASEAMT"
    Const colTTaxAmt As String = "TAXAMT"
    Const colTTaxAssessableAmt As String = "COLTTAXASSESSABLEAMT"
    '-----end of Tax Grid
    Dim CalculateTaxRatefromItemwsieTaxOnSale As Integer = 0
    Dim GstStatus As Boolean
    Dim strTaxType As String
    Dim RunBatchFifowise As Boolean = False
    Dim ApplyFEFO As Boolean = False
    Dim allowMilkJobworkOutowordWithAvgFatSNFRate As Boolean = False
#End Region
    Private Sub frmJWOTransferOther_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim coll As New Dictionary(Of String, String)

        'coll = New Dictionary(Of String, String)()
        'coll.Add("QC_Status", "integer null")
        'coll.Add("QC_By", "varchar(12) NULL references tspl_user_master(User_Code)")
        'coll.Add("QC_Date", "Datetime null")
        'coll.Add("QC_Post_By", "varchar(12) NULL references tspl_user_master(User_Code)")
        'coll.Add("QC_Post_Date", "Datetime null")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_JWO_GATE_ENTRY", coll, Nothing, False, False)

        'coll = New Dictionary(Of String, String)()
        'coll.Add("QC_Status", "integer null")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_JWO_GATE_ENTRY_DETAIL", coll, Nothing, False, False)

        CalculateTaxRatefromItemwsieTaxOnSale = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateTaxRatefromItemwsieTaxOnSale, clsFixedParameterCode.CalculateTaxRatefromItemwsieTaxOnSale, Nothing))
        allowMilkJobworkOutowordWithAvgFatSNFRate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.allowMilkJWOutowordWithAvgFatSNFRate, clsFixedParameterCode.allowMilkJWOutowordWithAvgFatSNFRate, Nothing)) = 1, True, False)
        RunBatchFifowise = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing)) = 1)
        ApplyFEFO = (clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyFEFO, clsFixedParameterCode.ApplyFEFO, Nothing)) = 1)
        UcAttachment1.Form_ID = MyBase.Form_ID
        RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        AddNew()
        gv1.Rows.AddNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        gv1.Enabled = True

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If

    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub AddNew()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtFromLocation.Value = ""
        lblFromLocation.Text = ""
        txtToLocation.Value = ""
        lblToLocation.Text = ""
        lblVendorCode.Text = ""
        lblVendorName.Text = ""
        txtRemarks.Text = ""
        txtVehicleCode.Value = ""
        lblVehicleNo.Text = ""
        txtvehicle_mannual_no.Text = ""
        txtLoadingAdviceNo.Text = ""
        txtEntryBillNo.Text = ""
        rbtnTaxCalAutomatic.IsChecked = True
        lblAmtWithoutTax.Text = ""
        lblTaxAmt.Text = ""
        lblTotRAmt.Text = ""
        txtLoadingAdviceNo.Text = ""
        txtEntryBillNo.Text = ""
        btnDelete.Enabled = False
        txtDocNo.MyReadOnly = False
        btnPost.Enabled = False
        btnSave.Enabled = True
        LoadBlankGrid()
        LoadBlankGridTax()
        isNewEntry = True
        btnSave.Text = "Save"
        lblPending.Status = ERPTransactionStatus.Pending
        UcAttachment1.BlankAllControls()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Sub ADDNewRows()
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
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


        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.Name = colICode
        'repoItemCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoItemCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoItemCode.Width = 150
        gv1.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemDesc.FormatString = ""
        repoItemDesc.HeaderText = "Item Description"
        repoItemDesc.Name = colIName
        repoItemDesc.Width = 250
        repoItemDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemDesc)

        Dim repoHSNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNCode.FormatString = ""
        repoHSNCode.HeaderText = "HSN Code"
        repoHSNCode.Name = colIHSN
        repoHSNCode.Width = 150
        repoHSNCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSNCode)

        Dim repoIsBatchItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsBatchItem.HeaderText = "Is Batch Item"
        repoIsBatchItem.Name = colIsBatchItem
        repoIsBatchItem.ReadOnly = True
        repoIsBatchItem.IsVisible = False
        repoIsBatchItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsBatchItem)

        Dim repoItemTaxCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemTaxCode = New GridViewTextBoxColumn()
        repoItemTaxCode.FormatString = ""
        repoItemTaxCode.HeaderText = "Item Tax Code"
        repoItemTaxCode.Name = colItemwiseTaxCode
        repoItemTaxCode.Width = 100
        repoItemTaxCode.IsVisible = False
        repoItemTaxCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemTaxCode)

        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = colUnit
        'repoUOM.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoUOM.TextImageRelation = TextImageRelation.TextBeforeImage
        repoUOM.Width = 100
        repoUOM.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUOM)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Qty"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.IsVisible = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colRate
        repoRate.Width = 80
        repoRate.Minimum = 0
        repoRate.ShowUpDownButtons = False
        repoRate.ReadOnly = allowMilkJobworkOutowordWithAvgFatSNFRate
        repoRate.Step = 0
        repoRate.IsVisible = True
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmount
        repoAmount.Width = 80
        repoAmount.Minimum = 0
        repoAmount.ShowUpDownButtons = False
        repoAmount.Step = 0
        repoAmount.IsVisible = True
        repoAmount.ReadOnly = True
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmount)


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

        Dim repoIsSurTax1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax1.HeaderText = "Is Surtax 1"
        repoIsSurTax1.Name = colIsSurTax1
        repoIsSurTax1.ReadOnly = True
        repoIsSurTax1.IsVisible = False
        repoIsSurTax1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax1)

        Dim repoSurTaxCode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode1.FormatString = ""
        repoSurTaxCode1.HeaderText = "Surtax 1"
        repoSurTaxCode1.Name = colSurTaxCode1
        repoSurTaxCode1.ReadOnly = True
        repoSurTaxCode1.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode1)

        Dim repoIsTaxable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable1.HeaderText = "Is Taxable 1"
        repoIsTaxable1.Name = colIsTaxable1
        repoIsTaxable1.ReadOnly = True
        repoIsTaxable1.IsVisible = False
        repoIsTaxable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable1)


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

        Dim repoIsSurTax2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax2.HeaderText = "Is Surtax 2"
        repoIsSurTax2.Name = colIsSurTax2
        repoIsSurTax2.ReadOnly = True
        repoIsSurTax2.IsVisible = False
        repoIsSurTax2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax2)

        Dim repoSurTaxCode2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode2.FormatString = ""
        repoSurTaxCode2.HeaderText = "Surtax 2"
        repoSurTaxCode2.Name = colSurTaxCode2
        repoSurTaxCode2.ReadOnly = True
        repoSurTaxCode2.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode2)

        Dim repoIsTaxable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable2.HeaderText = "Is Taxable 2"
        repoIsTaxable2.Name = colIsTaxable2
        repoIsTaxable2.ReadOnly = True
        repoIsTaxable2.IsVisible = False
        repoIsTaxable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable2)

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

        Dim repoIsSurTax3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax3.HeaderText = "Is Surtax 3"
        repoIsSurTax3.Name = colIsSurTax3
        repoIsSurTax3.ReadOnly = True
        repoIsSurTax3.IsVisible = False
        repoIsSurTax3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax3)

        Dim repoSurTaxCode3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode3.FormatString = ""
        repoSurTaxCode3.HeaderText = "Surtax 3"
        repoSurTaxCode3.Name = colSurTaxCode3
        repoSurTaxCode3.ReadOnly = True
        repoSurTaxCode3.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode3)

        Dim repoIsTaxable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable3.HeaderText = "Is Taxable 3"
        repoIsTaxable3.Name = colIsTaxable3
        repoIsTaxable3.ReadOnly = True
        repoIsTaxable3.IsVisible = False
        repoIsTaxable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable3)

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

        Dim repoIsSurTax4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax4.HeaderText = "Is Surtax 4"
        repoIsSurTax4.Name = colIsSurTax4
        repoIsSurTax4.ReadOnly = True
        repoIsSurTax4.IsVisible = False
        repoIsSurTax4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax4)

        Dim repoSurTaxCode4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode4.FormatString = ""
        repoSurTaxCode4.HeaderText = "Surtax 4"
        repoSurTaxCode4.Name = colSurTaxCode4
        repoSurTaxCode4.ReadOnly = True
        repoSurTaxCode4.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode4)

        Dim repoIsTaxable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable4.HeaderText = "Is Taxable 4"
        repoIsTaxable4.Name = colIsTaxable4
        repoIsTaxable4.ReadOnly = True
        repoIsTaxable4.IsVisible = False
        repoIsTaxable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable4)

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

        Dim repoIsSurTax5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax5.HeaderText = "Is Surtax 5"
        repoIsSurTax5.Name = colIsSurTax5
        repoIsSurTax5.ReadOnly = True
        repoIsSurTax5.IsVisible = False
        repoIsSurTax5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax5)

        Dim repoSurTaxCode5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode5.FormatString = ""
        repoSurTaxCode5.HeaderText = "Surtax 5"
        repoSurTaxCode5.Name = colSurTaxCode5
        repoSurTaxCode5.ReadOnly = True
        repoSurTaxCode5.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode5)

        Dim repoIsTaxable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable5.HeaderText = "Is Taxable 5"
        repoIsTaxable5.Name = colIsTaxable5
        repoIsTaxable5.ReadOnly = True
        repoIsTaxable5.IsVisible = False
        repoIsTaxable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable5)

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

        Dim repoIsSurTax6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax6.HeaderText = "Is Surtax 6"
        repoIsSurTax6.Name = colIsSurTax6
        repoIsSurTax6.ReadOnly = True
        repoIsSurTax6.IsVisible = False
        repoIsSurTax6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax6)

        Dim repoSurTaxCode6 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode6.FormatString = ""
        repoSurTaxCode6.HeaderText = "Surtax 6"
        repoSurTaxCode6.Name = colSurTaxCode6
        repoSurTaxCode6.ReadOnly = True
        repoSurTaxCode6.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode6)

        Dim repoIsTaxable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable6.HeaderText = "Is Taxable 6"
        repoIsTaxable6.Name = colIsTaxable6
        repoIsTaxable6.ReadOnly = True
        repoIsTaxable6.IsVisible = False
        repoIsTaxable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable6)

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

        Dim repoIsSurTax7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax7.HeaderText = "Is Surtax 7"
        repoIsSurTax7.Name = colIsSurTax7
        repoIsSurTax7.ReadOnly = True
        repoIsSurTax7.IsVisible = False
        repoIsSurTax7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax7)

        Dim repoSurTaxCode7 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode7.FormatString = ""
        repoSurTaxCode7.HeaderText = "Surtax 7"
        repoSurTaxCode7.Name = colSurTaxCode7
        repoSurTaxCode7.ReadOnly = True
        repoSurTaxCode7.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode7)

        Dim repoIsTaxable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable7.HeaderText = "Is Taxable 7"
        repoIsTaxable7.Name = colIsTaxable7
        repoIsTaxable7.ReadOnly = True
        repoIsTaxable7.IsVisible = False
        repoIsTaxable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable7)

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

        Dim repoIsSurTax8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax8.HeaderText = "Is Surtax 8"
        repoIsSurTax8.Name = colIsSurTax8
        repoIsSurTax8.ReadOnly = True
        repoIsSurTax8.IsVisible = False
        repoIsSurTax8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax8)

        Dim repoSurTaxCode8 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode8.FormatString = ""
        repoSurTaxCode8.HeaderText = "Surtax 8"
        repoSurTaxCode8.Name = colSurTaxCode8
        repoSurTaxCode8.ReadOnly = True
        repoSurTaxCode8.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode8)

        Dim repoIsTaxable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable8.HeaderText = "Is Taxable 8"
        repoIsTaxable8.Name = colIsTaxable8
        repoIsTaxable8.ReadOnly = True
        repoIsTaxable8.IsVisible = False
        repoIsTaxable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable8)

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

        Dim repoIsSurTax9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax9.HeaderText = "Is Surtax 9"
        repoIsSurTax9.Name = colIsSurTax9
        repoIsSurTax9.ReadOnly = True
        repoIsSurTax9.IsVisible = False
        repoIsSurTax9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax9)

        Dim repoSurTaxCode9 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode9.FormatString = ""
        repoSurTaxCode9.HeaderText = "Surtax 9"
        repoSurTaxCode9.Name = colSurTaxCode9
        repoSurTaxCode9.ReadOnly = True
        repoSurTaxCode9.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode9)

        Dim repoIsTaxable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable9.HeaderText = "Is Taxable 9"
        repoIsTaxable9.Name = colIsTaxable9
        repoIsTaxable9.ReadOnly = True
        repoIsTaxable9.IsVisible = False
        repoIsTaxable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable9)


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

        Dim repoIsSurTax10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSurTax10.HeaderText = "Is Surtax 10"
        repoIsSurTax10.Name = colIsSurTax10
        repoIsSurTax10.ReadOnly = True
        repoIsSurTax10.IsVisible = False
        repoIsSurTax10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsSurTax10)

        Dim repoSurTaxCode10 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSurTaxCode10.FormatString = ""
        repoSurTaxCode10.HeaderText = "Surtax 10"
        repoSurTaxCode10.Name = colSurTaxCode10
        repoSurTaxCode10.ReadOnly = True
        repoSurTaxCode10.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoSurTaxCode10)

        Dim repoIsTaxable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsTaxable10.HeaderText = "Is Taxable 10"
        repoIsTaxable10.Name = colIsTaxable10
        repoIsTaxable10.ReadOnly = True
        repoIsTaxable10.IsVisible = False
        repoIsTaxable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsTaxable10)

        Dim repoIsExcisable1 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable1.HeaderText = "Is Excisable 1"
        repoIsExcisable1.Name = colIsExcisable1
        repoIsExcisable1.ReadOnly = True
        repoIsExcisable1.IsVisible = False
        repoIsExcisable1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable1)

        Dim repoIsExcisable2 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable2.HeaderText = "Is Excisable 2"
        repoIsExcisable2.Name = colIsExcisable2
        repoIsExcisable2.ReadOnly = True
        repoIsExcisable2.IsVisible = False
        repoIsExcisable2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable2)

        Dim repoIsExcisable3 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable3.HeaderText = "Is Excisable 3"
        repoIsExcisable3.Name = colIsExcisable3
        repoIsExcisable3.ReadOnly = True
        repoIsExcisable3.IsVisible = False
        repoIsExcisable3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable3)

        Dim repoIsExcisable4 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable4.HeaderText = "Is Excisable 4"
        repoIsExcisable4.Name = colIsExcisable4
        repoIsExcisable4.ReadOnly = True
        repoIsExcisable4.IsVisible = False
        repoIsExcisable4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable4)

        Dim repoIsExcisable5 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable5.HeaderText = "Is Excisable 5"
        repoIsExcisable5.Name = colIsExcisable5
        repoIsExcisable5.ReadOnly = True
        repoIsExcisable5.IsVisible = False
        repoIsExcisable5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable5)

        Dim repoIsExcisable6 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable6.HeaderText = "Is Excisable 6"
        repoIsExcisable6.Name = colIsExcisable6
        repoIsExcisable6.ReadOnly = True
        repoIsExcisable6.IsVisible = False
        repoIsExcisable6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable6)

        Dim repoIsExcisable7 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable7.HeaderText = "Is Excisable 7"
        repoIsExcisable7.Name = colIsExcisable7
        repoIsExcisable7.ReadOnly = True
        repoIsExcisable7.IsVisible = False
        repoIsExcisable7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable7)

        Dim repoIsExcisable8 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable8.HeaderText = "Is Excisable 8"
        repoIsExcisable8.Name = colIsExcisable8
        repoIsExcisable8.ReadOnly = True
        repoIsExcisable8.IsVisible = False
        repoIsExcisable8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable8)

        Dim repoIsExcisable9 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable9.HeaderText = "Is Excisable 9"
        repoIsExcisable9.Name = colIsExcisable9
        repoIsExcisable9.ReadOnly = True
        repoIsExcisable9.IsVisible = False
        repoIsExcisable9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable9)

        Dim repoIsExcisable10 As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsExcisable10.HeaderText = "Is Excisable 10"
        repoIsExcisable10.Name = colIsExcisable10
        repoIsExcisable10.ReadOnly = True
        repoIsExcisable10.IsVisible = False
        repoIsExcisable10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoIsExcisable10)

        Dim repoTotTaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotTaxAmt = New GridViewDecimalColumn()
        repoTotTaxAmt.FormatString = ""
        repoTotTaxAmt.HeaderText = "Tax Amount"
        repoTotTaxAmt.Name = colTotTaxAmt
        repoTotTaxAmt.Width = 80
        repoTotTaxAmt.ReadOnly = True
        repoTotTaxAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTotTaxAmt)

        Dim repoAmtAfterTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmtAfterTax = New GridViewDecimalColumn()
        repoAmtAfterTax.FormatString = ""
        repoAmtAfterTax.HeaderText = "Included Tax Amount"
        repoAmtAfterTax.Name = colAmtAfterTax
        repoAmtAfterTax.WrapText = True
        repoAmtAfterTax.Width = 80
        repoAmtAfterTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmtAfterTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmtAfterTax)


        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AutoSizeRows = False
        gv1.AllowRowReorder = True
        ReStoreGridLayout()
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

        Dim repoTaxAssessableAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxAssessableAmt.FormatString = ""
        repoTaxAssessableAmt.HeaderText = "Assessable Amount"
        repoTaxAssessableAmt.Name = colTTaxAssessableAmt
        repoTaxAssessableAmt.Width = 100
        repoTaxAssessableAmt.ReadOnly = True
        repoTaxAssessableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(repoTaxAssessableAmt)

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
        repoTaxRate.ReadOnly = False
        repoTaxRate.IsVisible = True
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

        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = False
        gv2.AllowRowReorder = False
        gv2.EnableSorting = False
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False

    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("First Select From Location.")
            gv1.CurrentRow.Cells(colICode).Value = ""
            Return
        End If
        If clsCommon.myLen(txtToLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("First Select To Location.")
            gv1.CurrentRow.Cells(colICode).Value = ""
            Return
        End If

        Dim whr As String = " tspl_item_master.Item_Code in (select TSPL_LOCATION_MASTER_Jobwork_Item.jobwork_item from TSPL_LOCATION_MASTER_Jobwork_Item where TSPL_LOCATION_MASTER_Jobwork_Item.Main_Location_Code = '" + txtToLocation.Value + "')"
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", False, isButtonClick, "", "", "", "")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            ADDNewRows()
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(obj.Item_Code, Nothing)
            gv1.CurrentRow.Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(obj.Item_Code)
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
            SetRate(gv1.CurrentRow.Index)
        Else
            SetBlankOfItemColumns()
        End If
        SetitemWiseTaxSetting(True, True)
        setBalance()
    End Sub

    Sub SetRate(ByVal Rowindex As Integer)
        If allowMilkJobworkOutowordWithAvgFatSNFRate Then
            gv1.Rows(Rowindex).Cells(colRate).Value = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, clsCommon.myCstr(gv1.Rows(Rowindex).Cells(colICode).Value), txtFromLocation.Value, 1, txtDate.Value, txtDate.Value, False, Nothing, "TSPL_INVENTORY_MOVEMENT", clsCommon.myCstr(gv1.Rows(Rowindex).Cells(colUnit).Value))
        End If
    End Sub
    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colIHSN).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colQty).Value = 0
    End Sub
    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        'UcItemBalance1.ItemMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
        UcItemBalance1.LocationCode = txtFromLocation.Value
        UcItemBalance1.LocationName = lblFromLocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowPOQty = True
        UcItemBalance1.RefreshData()
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "TSPL_ITEM_UOM_DETAIL.Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("TRNSKLUOMFND", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
            SetRate(gv1.CurrentRow.Index)
        End If
    End Sub
    Sub PostData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Code not found to post")
            End If
            If (myMessages.postConfirm()) Then
                If SaveData() Then
                    If clsJWOTransferOtherHead.PostData(txtDocNo.Value) Then
                        clsCommon.MyMessageBoxShow("Successfully Posted", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
        End Try

        'Try

        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
        'End Try
    End Sub
    Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Delete the current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, txtDocNo.Value)
                clsJWOTransferOtherHead.DeleteData(txtDocNo.Value)
                UcAttachment1.funDelete(txtDocNo.Value)
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
        End Try

    End Sub
    Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub gv1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub gv1_CurrentCellChanged(sender As Object, e As CurrentCellChangedEventArgs)
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gv1_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs)

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Private Sub txtFromLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFromLocation._MYValidating
        txtToLocation.Value = ""
        lblToLocation.Text = ""
        lblVendorCode.Text = ""
        lblVendorName.Text = ""
        Dim strWhrclas As String = " isNull(Is_Jobwork,0) = 0 "
        Dim qry As String = "select Location_Code as  Code, Location_Desc as Description  from TSPL_LOCATION_MASTER"
        txtFromLocation.Value = clsCommon.ShowSelectForm("FrmLocCode", qry, "Code", strWhrclas, txtFromLocation.Value, "Code", isButtonClicked)
        lblFromLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtFromLocation.Value & "'")
        SetTax()
    End Sub
    Private Sub txtToLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtToLocation._MYValidating
        LoadBlankGrid()
        LoadBlankGridTax()
        gv1.Rows.AddNew()
        Dim strWhrclas As String = " isNull(Is_Jobwork,0) = 1 "
        Dim qry As String = " select Location_Code as  Code, Location_Desc as Description  from TSPL_LOCATION_MASTER "
        txtToLocation.Value = clsCommon.ShowSelectForm("ToLocCode", qry, "Code", strWhrclas, txtToLocation.Value, "Code", isButtonClicked)
        lblToLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtToLocation.Value & "'")
        lblVendorCode.Text = clsDBFuncationality.getSingleValue("select JobWork_Vendor from TSPL_LOCATION_MASTER where Location_Code='" & txtToLocation.Value & "'")
        If clsCommon.myLen(lblVendorCode.Text) > 0 Then
            lblVendorName.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code= '" & lblVendorCode.Text & "'")
        End If
        SetTax()
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
        gv1.Rows.AddNew()
    End Sub
    Private Sub gv1_CellValueChanged_1(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try

            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) Then
                        OpenICodeList(False)
                    ElseIf e.Column Is gv1.Columns(colUnit) Then
                        OpenUOMList(False)
                    ElseIf e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colRate) Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                        UpdateAllTotals()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Private Function SavingData(ByVal ChekBtnPost As Boolean) As Boolean
        If (SaveData()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                btnSave.Text = "Update"
            End If
            Return True
        Else
            Return False
        End If
    End Function
    Private Function SaveData() As Boolean
        Try
            If AllowToSave() Then

                Dim obj As New clsJWOTransferOtherHead()
                obj.TRANSFER_NO = txtDocNo.Value
                obj.TRANSFER_DATE = txtDate.Value
                obj.From_Locaction = txtFromLocation.Value
                obj.To_Locaction = txtToLocation.Value
                obj.Vendor_Code = lblVendorCode.Text
                obj.Remarks = txtRemarks.Text
                obj.Vehicle_Code = txtVehicleCode.Value
                obj.Vehicle_No = txtvehicle_mannual_no.Text


                obj.Tax_Group = txtTaxGroup.Value
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
                obj.Total_Amount = clsCommon.myCdbl(lblAmtWithoutTax.Text)
                obj.Total_Tax_Amount = clsCommon.myCdbl(lblTaxAmt.Text)
                obj.Total_Net_Amount = clsCommon.myCdbl(lblTotRAmt.Text)
                obj.Loading_Advice_No = txtLoadingAdviceNo.Text
                obj.Entry_Bill_No = txtEntryBillNo.Text


                obj.Arr = New List(Of clsJWOTransferOtherDetail)
                For ii As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                        Dim objtr As New clsJWOTransferOtherDetail
                        objtr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                        objtr.UOM = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                        objtr.Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                        objtr.Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                        objtr.Amount = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
                        objtr.TAX1 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax1).Value)
                        objtr.TAX1_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt1).Value)
                        objtr.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate1).Value)
                        objtr.TAX1_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt1).Value)
                        objtr.TAX2 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax2).Value)
                        objtr.TAX2_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt2).Value)
                        objtr.TAX2_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate2).Value)
                        objtr.TAX2_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt2).Value)
                        objtr.TAX3 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax3).Value)
                        objtr.TAX3_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt3).Value)
                        objtr.TAX3_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate3).Value)
                        objtr.TAX3_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt3).Value)
                        objtr.TAX4 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax4).Value)
                        objtr.TAX4_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt4).Value)
                        objtr.TAX4_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate4).Value)
                        objtr.TAX4_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt4).Value)
                        objtr.TAX5 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax5).Value)
                        objtr.TAX5_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt5).Value)
                        objtr.TAX5_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate5).Value)
                        objtr.TAX5_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt5).Value)
                        objtr.TAX6 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax6).Value)
                        objtr.TAX6_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt6).Value)
                        objtr.TAX6_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate6).Value)
                        objtr.TAX6_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt6).Value)
                        objtr.TAX7 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax7).Value)
                        objtr.TAX7_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt7).Value)
                        objtr.TAX7_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate7).Value)
                        objtr.TAX7_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt7).Value)
                        objtr.TAX8 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax8).Value)
                        objtr.TAX8_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt8).Value)
                        objtr.TAX8_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate8).Value)
                        objtr.TAX8_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt8).Value)
                        objtr.TAX9 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax9).Value)
                        objtr.TAX9_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt9).Value)
                        objtr.TAX9_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate9).Value)
                        objtr.TAX9_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt9).Value)
                        objtr.TAX10 = clsCommon.myCstr(gv1.Rows(ii).Cells(colTax10).Value)
                        objtr.TAX10_Base_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxBaseAmt10).Value)
                        objtr.TAX10_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxRate10).Value)
                        objtr.TAX10_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt10).Value)
                        objtr.Tax_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                        objtr.Net_Amt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterTax).Value)
                        objtr.ItemwiseTaxCode = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemwiseTaxCode).Value)
                        objtr.arrBatchItem = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                        obj.Arr.Add(objtr)
                    End If
                Next
                If obj.SaveData(obj, isNewEntry) Then
                    UcAttachment1.SaveData(obj.TRANSFER_NO)

                    Dim xNewDesc As String = "Description : " + obj.Remarks
                    clsApply_Approval.CheckApprovalRequired(MyBase.Form_ID, txtDocNo.Value, txtDate.Value, xNewDesc, txtRemarks.Text, 0, 0, "")

                    LoadData(obj.TRANSFER_NO, NavigatorType.Current)
                End If
                Return True
            Else
                txtDocNo.MyReadOnly = False
                btnDelete.Enabled = False
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
            Return False
        End Try
        Return False
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            isInsideLoadData = True
            AddNew()
            Dim obj As clsJWOTransferOtherHead = clsJWOTransferOtherHead.GetData(strCode, NavType, Nothing)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.TRANSFER_NO) > 0 Then
                isNewEntry = False

                btnDelete.Enabled = True

                btnSave.Enabled = True
                btnPost.Enabled = True

                txtDocNo.Value = obj.TRANSFER_NO
                txtDate.Value = obj.TRANSFER_DATE
                txtFromLocation.Value = obj.From_Locaction
                lblToLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtToLocation.Value & "'")
                txtToLocation.Value = obj.To_Locaction
                lblVendorCode.Text = obj.Vendor_Code
                lblToLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & txtToLocation.Value & "'")
                If clsCommon.myLen(lblVendorCode.Text) > 0 Then
                    lblVendorName.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code= '" & lblVendorCode.Text & "'")
                End If
                txtRemarks.Text = obj.Remarks
                txtVehicleCode.Value = obj.Vehicle_Code
                If clsCommon.myLen(txtVehicleCode.Value) > 0 Then
                    lblVehicleNo.Text = clsDBFuncationality.getSingleValue("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleCode.Value) + "'")
                End If
                txtvehicle_mannual_no.Text = obj.Vehicle_No

                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = clsTransferDCC.GetTaxGroupData(obj.Tax_Group, "T")
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If
                txtTaxGroup.Value = obj.Tax_Group
                If obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic Then
                    rbtnTaxCalAutomatic.IsChecked = True
                ElseIf obj.Tax_Calculation_Type = EnumTaxCalucationType.Mannual Then
                    rbtnTaxCalManual.IsChecked = True
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX2_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX3_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX4_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX5_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX6_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX7_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX8_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX9_Base_Amt
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxAssessableAmt).Value = obj.TAX10_Base_Amt
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
                lblAmtWithoutTax.Text = clsCommon.myFormat(obj.Total_Amount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amount)
                lblTotRAmt.Text = clsCommon.myFormat(obj.Total_Net_Amount)
                txtLoadingAdviceNo.Text = obj.Loading_Advice_No
                txtEntryBillNo.Text = obj.Entry_Bill_No

                LoadDetailData(obj.Arr, False)
                gv1.Rows.AddNew()
                txtDocNo.MyReadOnly = True
                btnSave.Text = "Update"
                lblPending.Status = obj.Status
                If obj.Status = 1 Then
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                Else
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    btnSave.Enabled = True
                End If
                UcAttachment1.LoadData(obj.TRANSFER_NO)

                If Not clsApply_Approval.Visibility_PostButtonForApproval(MyBase.Form_ID, txtDocNo.Value, 0, 0, "") Then
                    btnPost.Visible = False
                    If lblPending.Status = ERPTransactionStatus.Pending Then
                        lblPending.Status = clsApply_Approval.ApprovalCondCheck_Doc(MyBase.Form_ID, txtDocNo.Value, Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub LoadDetailData(ByVal Arr As List(Of clsJWOTransferOtherDetail), ByVal isAddMasterCode As Boolean)
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            For Each objtr As clsJWOTransferOtherDetail In Arr
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Tag = objtr.arrBatchItem
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objtr.line_No
                gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objtr.Item_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = clsItemMaster.GetItemName(objtr.Item_Code, Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIHSN).Value = clsItemMaster.GetItemHSNCode(objtr.Item_Code, Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.Item_Code)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objtr.UOM
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objtr.Qty
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objtr.Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objtr.Amount


                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemwiseTaxCode).Value = objtr.ItemwiseTaxCode
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax1).Value = objtr.TAX1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt1).Value = objtr.TAX1_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate1).Value = objtr.TAX1_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt1).Value = objtr.TAX1_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax2).Value = objtr.TAX2
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt2).Value = objtr.TAX2_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate2).Value = objtr.TAX2_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt2).Value = objtr.TAX2_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax3).Value = objtr.TAX3
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt3).Value = objtr.TAX3_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate3).Value = objtr.TAX3_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt3).Value = objtr.TAX3_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax4).Value = objtr.TAX4
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt4).Value = objtr.TAX4_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate4).Value = objtr.TAX4_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt4).Value = objtr.TAX4_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax5).Value = objtr.TAX5
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt5).Value = objtr.TAX5_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate5).Value = objtr.TAX5_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt5).Value = objtr.TAX5_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax6).Value = objtr.TAX6
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt6).Value = objtr.TAX6_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate6).Value = objtr.TAX6_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt6).Value = objtr.TAX6_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax7).Value = objtr.TAX7
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt7).Value = objtr.TAX7_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate7).Value = objtr.TAX7_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt7).Value = objtr.TAX7_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax8).Value = objtr.TAX8
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt8).Value = objtr.TAX8_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate8).Value = objtr.TAX8_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt8).Value = objtr.TAX8_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax9).Value = objtr.TAX9
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt9).Value = objtr.TAX9_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate9).Value = objtr.TAX9_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt9).Value = objtr.TAX9_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTax10).Value = objtr.TAX10
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxBaseAmt10).Value = objtr.TAX10_Base_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxRate10).Value = objtr.TAX10_Rate
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTaxAmt10).Value = objtr.TAX10_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTotTaxAmt).Value = objtr.Tax_Amt
                gv1.Rows(gv1.Rows.Count - 1).Cells(colAmtAfterTax).Value = objtr.Net_Amt
            Next
        End If
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        LoadData(txtDocNo.Value, NavType)
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "  select TRANSFER_NO as Code, CONVERT(varchar, TRANSFER_DATE,103) as TRANSFER_DATE, From_Locaction , To_Locaction,Vendor_Code,Remarks,[Status] from TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD "
        Dim whrcls As String = ""
        txtDocNo.Value = clsCommon.ShowSelectForm("FNDDoc", qry, "Code", whrcls, txtDocNo.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            LoadData(txtDocNo.Value, NavigatorType.Current)
        Else
            AddNew()
        End If
    End Sub
    Function AllowToSave() As Boolean
        Try
            clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, txtDocNo.Value)
            If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("From Location can not be blank.")
                Return False
            End If
            If clsCommon.myLen(txtToLocation.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("To Location can not be blank.")
                Return False
            End If
            If clsCommon.myLen(lblVendorCode.Text) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Vendor can not be blank.")
                Return False
            End If
            If clsCommon.myLen(txtvehicle_mannual_no) > 50 Then
                common.clsCommon.MyMessageBoxShow("Vehicle No can not grater then 50 .")
                Return False
            End If
            'If ApplyFEFO = True Then
            '    If clsCommon.myLen(txtLoadingAdviceNo.Text) <= 0 Then
            '        common.clsCommon.MyMessageBoxShow("Please Enter Loading Advice No ")
            '        txtLoadingAdviceNo.Focus()
            '        Return False
            '    End If
            'End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                If clsCommon.myLen(strICode) <= 0 Then
                    Continue For
                End If

                If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(txtDate.Value)) Then
                    Dim HSNCode As String = clsItemMaster.GetItemHSNCode(gv1.Rows(ii).Cells(colICode).Value, Nothing)
                    gv1.Rows(ii).Cells(colIHSN).Value = HSNCode
                    If clsCommon.myLen(HSNCode) <= 0 Then
                        Throw New Exception("HSN Code is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    End If
                End If

                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
                Dim strSno As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colLineNo).Value)
                Dim dblQty As Decimal = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                If clsCommon.myLen(strUOM) <= 0 And clsCommon.myLen(strSno) > 0 Then
                    Dim Msg As String = "UOM Code Can not be blank at Row No " + clsCommon.myCstr(ii + 1)
                    common.clsCommon.MyMessageBoxShow(Msg)
                    Return False
                End If
                Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strICode, txtFromLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, strUOM, 0)

                If clsCommon.myCdbl(dblQty) <= 0 Then
                    Dim Msg As String = "Qty Can not be blank or 0 at Row No " + clsCommon.myCstr(ii + 1)
                    common.clsCommon.MyMessageBoxShow(Msg)
                    Return False
                End If
                If clsCommon.myCdbl(dblQty) > dblBalQty Then
                    common.clsCommon.MyMessageBoxShow("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty), Me.Text)
                    Return False
                End If
                '====For Inner Check===================
                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If jj = ii Then
                        Continue For
                    End If
                    Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                    If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal Then
                        Dim Msg As String = "Same Item Code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If

                Next
                '=============================================
                If dblQty > 0 AndAlso clsCommon.myCBool(gv1.Rows(ii).Cells(colIsBatchItem).Value) Then
                    If RunBatchFifowise OrElse ApplyFEFO Then
                        gv1.CurrentRow = gv1.Rows(ii)
                        OpenBatchItem()
                    End If
                    Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv1.Rows(ii).Cells(colICode).Tag, List(Of clsBatchInventory))
                    If arrBatchNo Is Nothing Then
                        Throw New Exception("Please provide Batch no for item : " + strICode + " . At Line No" + clsCommon.myCstr(ii + 1))
                    Else
                        Dim tQty As Decimal = 0
                        For Each objBatch As clsBatchInventory In arrBatchNo
                            tQty += objBatch.Qty
                        Next
                        If tQty <> dblQty Then
                            Throw New Exception("Item : " + strICode + " Entered Qty " + clsCommon.myCstr(dblQty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                End If
                SetRate(ii)
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Sub OpenBatchItem()
        If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsBatchItem).Value) Then
            Dim frm As frmBatchItemOut = New frmBatchItemOut()
            frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
            frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
            frm.strLocationCode = txtFromLocation.Value
            frm.strCurrDocNo = txtDocNo.Value
            frm.strCurrDocType = "JW-TO"
            frm.strUOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
            frm.dblMRP = clsCommon.myCdbl(0)
            frm.dblqty = clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value)
            frm.arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
            If ApplyFEFO = True Then
                frm.OpenSerialList(0, "", "", True)
                gv1.CurrentRow.Cells(colICode).Tag = frm.arr
            ElseIf Not RunBatchFifowise Then
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    gv1.CurrentRow.Cells(colICode).Tag = frm.arr
                End If
            Else
                frm.OpenSerialList(0, "")
                gv1.CurrentRow.Cells(colICode).Tag = frm.arr
            End If

        End If
    End Sub
    Sub AvilableQtyForItem()

    End Sub
    Sub getBaseQryForOther()
        If clsCommon.myLen(txtFromLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("First Select From Location.")
            gv1.CurrentRow.Cells(colQty).Value = 0
            Return
        End If
        If clsCommon.myLen(txtToLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("First Select To Location.")
            gv1.CurrentRow.Cells(colQty).Value = 0
            Return
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("First Select Item Code.")
            gv1.CurrentRow.Cells(colQty).Value = 0
            Return
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colUnit).Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("First Select UOM.")
            gv1.CurrentRow.Cells(colQty).Value = 0
            Return
        End If
        Dim ActualBalanceQty As Decimal = 0
        Dim qry As String = clsItemLocationDetails.getBaseQryForItemBalanceDuringTransaction(gv1.CurrentRow.Cells(colICode).Value, gv1.CurrentRow.Cells(colUnit).Value, txtFromLocation.Value, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"), txtDocNo.Value, False, gv1.CurrentRow.Cells(colRate).Value, Nothing)
        qry = "select ICode,SUM(Qty * case when TransType='' then 1 else 0 end)as BalanceQty,SUM(Qty * case when TransType='' then 0 else 1 end)as CommitQty,SUM(Qty *RI )as ActualBalanceQty from (" + qry + ")FinalQry group by ICode"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            ActualBalanceQty = clsCommon.myFormat(dt.Rows(0)("ActualBalanceQty"))
        End If
        If clsCommon.myCdbl(gv1.CurrentRow.Cells(colQty).Value) > ActualBalanceQty Then
            common.clsCommon.MyMessageBoxShow("Qty is grether then avilable qty.Re Enter Qty..")
            gv1.CurrentRow.Cells(colQty).Value = 0
        End If
    End Sub
    Private Sub btnPrintNew_Click(sender As Object, e As EventArgs) Handles btnPrintNew.Click
        Dim frmCrystalReportViewer As New frmCrystalReportViewer()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(" Transfer No Not Available for Print .", Me.Text)
                Return
            End If
            'Dim qry As String = "  Select * from ( " &
            '                    "  select   TSPL_VENDOR_MASTER.GSTFinalNo as Ven_GSTIN,TSPL_STATE_MASTER_Vendor.GST_STATE_Code as Ven_State_GST_Code,TSPL_STATE_MASTER_Vendor.STATE_NAME as Ven_State_Name,tspl_city_master_Vendor.City_Name as Ven_City_Name, '1' as CopyType, TSPL_ITEM_MASTER.Item_Desc ,TSPL_ITEM_MASTER.HSN_Code,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vehicle_Code,TSPL_VEHICLE_MASTER.Description as Vehicle_Name , " &
            '                    "  convert (varchar,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_DATE,103) as TRANSFER_DATE, case when len(TSPL_CITY_MASTER_From_location.City_Name) > 0   " &
            '                    "  then TSPL_CITY_MASTER_From_location.City_Name  else TSPL_LOCATION_MASTER_From_loc.City_Code end as From_Location_City_Name , case when len  " &
            '                    "  (TSPL_CITY_MASTER_To_Location.City_Code) > 0 then TSPL_CITY_MASTER_To_Location.City_Name else TSPL_LOCATION_MASTER_To_loc.City_Code end as To_Location_City_Name,  " &
            '                    "  TSPL_STATE_MASTER_To_loc.STATE_CODE as To_Location_State_Code,TSPL_STATE_MASTER_To_loc.STATE_NAME as To_Location_State_Name,   " &
            '                    "  TSPL_STATE_MASTER_From_loc.STATE_CODE as From_Location_State_Code,TSPL_STATE_MASTER_From_loc.STATE_NAME as From_Location_State_Name,  " &
            '                    "  TSPL_LOCATION_MASTER_From_loc.GSTNO as From_location_GSTIN,TSPL_STATE_MASTER_From_loc.GST_STATE_Code as From_Loc_GST_STATE_Code,  " &
            '                    "  TSPL_LOCATION_MASTER_To_loc.GSTNO as To_Location_GSTIN,TSPL_STATE_MASTER_To_loc.GST_STATE_Code as   " &
            '                    "  To_Loc_GST_State_Code,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code,  " &
            '                    "  TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.UOM,   " &
            '                    "  TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Qty,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.line_No,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Rate,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Amount,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction,TSPL_LOCATION_MASTER_From_loc.Location_Desc as   " &
            '                    "  From_Location_Desc,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.To_Locaction,TSPL_LOCATION_MASTER_To_loc.Location_Desc as To_Location_Desc,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name,  " &
            '                    "  TSPL_LOCATION_MASTER_From_loc.Add1 + case when len(TSPL_LOCATION_MASTER_From_loc. Add2) > 0 then +','+ TSPL_LOCATION_MASTER_From_loc. Add2 else ' '   " &
            '                    "  end + Case when len( TSPL_LOCATION_MASTER_From_loc. Add3) >0 then ','+TSPL_LOCATION_MASTER_From_loc. Add3 else '' end + case when len   " &
            '                    "  (TSPL_LOCATION_MASTER_From_loc.Add4) >0 then ','+TSPL_LOCATION_MASTER_From_loc.Add4 else '' end + case when len   " &
            '                    "  (TSPL_LOCATION_MASTER_From_loc.City_Code)>0 then ','+ TSPL_LOCATION_MASTER_From_loc.City_Code else '' end + case when len  " &
            '                    "  (TSPL_LOCATION_MASTER_From_loc.state) > 0 then ','+ TSPL_STATE_MASTER_From_loc.STATE_NAME else ''  end + Case when len   " &
            '                    "  (TSPL_LOCATION_MASTER_From_loc.Pin_Code ) >0 then ' - '+ convert(varchar, TSPL_LOCATION_MASTER_From_loc.Pin_Code,103) else '' end as From_Location_Address , " &
            '                    "  TSPL_LOCATION_MASTER_To_loc.Add1 + case when len(TSPL_LOCATION_MASTER_To_loc. Add2) > 0 then +','+ TSPL_LOCATION_MASTER_To_loc. Add2 else ' ' end + Case when len( TSPL_LOCATION_MASTER_To_loc. Add3) >0 then ','+TSPL_LOCATION_MASTER_To_loc. Add3 else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.Add4) >0 then ','+TSPL_LOCATION_MASTER_To_loc.Add4 else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.City_Code)>0 then ','+ TSPL_LOCATION_MASTER_To_loc.City_Code else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.state) > 0 then ','+ TSPL_STATE_MASTER_To_loc.STATE_NAME else ''  end + Case when len(TSPL_LOCATION_MASTER_To_loc.Pin_Code ) >0 then ' - '+ convert(varchar, TSPL_LOCATION_MASTER_To_loc.Pin_Code,103) else '' end  as To_Location_Address  " &
            '                    "  , tspl_company_master.Comp_Name,tspl_company_master.Add1 + case when len( tspl_company_master.Add2) >0 then ',' + tspl_company_master.Add2 else '' end + case when len(tspl_company_master.Add3) >0  then ','+tspl_company_master.Add3 else '' end + case when len(tspl_company_master.Fax) >0 then ', FAX :'+ tspl_company_master.Fax else '' end + case when len(tspl_company_master.Email) >0 then ', Email : '+tspl_company_master.Email else '' end + case when len( tspl_company_master.CINNo) >0 then  'CIN NO : '+tspl_company_master.CINNo else '' end + case when len(tspl_company_master.Pan_No) > 0 then ', PAN No : '+ tspl_company_master.Pan_No else '' end as Company_Address " &
            '                    "  , TSPL_VENDOR_MASTER.Add1+ case when len( TSPL_VENDOR_MASTER.Add2) >0 then ',' + TSPL_VENDOR_MASTER.Add2 else '' end + case when len(TSPL_VENDOR_MASTER.Add3) >0  then ','+TSPL_VENDOR_MASTER.Add3 else '' end + case when len( TSPL_VENDOR_MASTER.City_Code) >0 then ',' +tspl_city_master_Vendor.City_Name else '' end + case when len(TSPL_VENDOR_MASTER.State_Code) >0 then ',' +TSPL_STATE_MASTER_Vendor.STATE_NAME end  as Vendor_Address " &
            '                    "  from TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS left outer join TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD on TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO =TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO  " &
            '                    "  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vendor_Code " &
            '                    "  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_From_loc on TSPL_LOCATION_MASTER_From_loc.Location_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction " &
            '                    "  left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_To_loc on TSPL_LOCATION_MASTER_To_loc.Location_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.To_Locaction  " &
            '                    "  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_From_loc on  TSPL_STATE_MASTER_From_loc.STATE_CODE = TSPL_LOCATION_MASTER_From_loc.State  " &
            '                    "  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_To_loc on  TSPL_STATE_MASTER_To_loc.STATE_CODE = TSPL_LOCATION_MASTER_From_loc.State " &
            '                    "  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_From_location on TSPL_CITY_MASTER_From_location.City_Code = TSPL_LOCATION_MASTER_From_loc.City_Code  " &
            '                    "  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_To_Location on TSPL_CITY_MASTER_To_Location.City_Code =TSPL_LOCATION_MASTER_To_loc.City_Code " &
            '                    "  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code " &
            '                    "  left outer join tspl_company_master on tspl_company_master.comp_code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.comp_code          " &
            '                    "  left outer join tspl_city_master as tspl_city_master_Vendor  on  tspl_city_master_Vendor.City_Code =TSPL_VENDOR_MASTER.City_Code          " &
            '                    "  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Vendor on TSPL_STATE_MASTER_Vendor.STATE_CODE = TSPL_VENDOR_MASTER.State_Code  left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vehicle_Code       " &
            '                    "  where TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO='" + txtDocNo.Value + "'  " &
            '                    "  ) XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINAL COPY' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'DUPLICATE COPY' as   " &
            '                    "  CopyType1 UNION Select '1' as COL1, 3 as COL2,  'TRIPLICATE COPY' as CopyType1  UNION Select '1' as COL1, 4 as COL2,  'QUADRUPLICATE COPY' as  " &
            '                    "  CopyType1  ) YYY ON YYY.COL1=XXX.CopyType  ORDER BY YYY.COL2,Line_No  "
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            'If dt.Rows.Count > 0 Then
            '    Dim dtDocdate As Date?
            '    dtDocdate = Nothing
            '    dtDocdate = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
            '    frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.ServiceReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "RptTransfer_Local", " JOB Work Transfer Other", dtDocdate, "rptCompanyAddress.rpt")

            'End If
            Dim qry As String = "  select  LMFrom.GSTNO fromGSTNo,SMFrom.gst_state_code as FromStateCode,TSPL_COMPANY_MASTER.GSTINNo AS COMP_GSTIN_NO,SMCompany.GST_STATE_Code AS Comp_GST_StateCode,To_GP_Location.GSTNO as To_Gp_GSTIN_NO,To_GP_Location_State.GST_STATE_Code AS To_GP_GST_StateCode,TSPL_ITEM_MASTER.HSN_Code  " &
            "  , To_GP_Location_State.STATE_CODE As To_Gp_stateCode,SMCompany.STATE_CODE As Comp_StateCode,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Status " &
            "  , TSPL_COMPANY_MASTER.Circle_No,To_GP_Location.Ecc_Number as To_Gp_EccNo,Case when len(ISNULL(tspl_company_master.Phone1,''))>0 and tspl_company_master.Phone1='(+__)__________' then '' else tspl_company_master.Phone1 end +','+ Case When   ISNULL(tspl_company_master.Phone2,'')<>'(+__)__________' Then '  '+ tspl_company_master.Phone2 Else'' end as comp_Phn ,tspl_company_master.CINNo,convert(varchar,tspl_company_master.TinNo_Issue_Date,103) as TinNo_Issue_Date,convert(varchar,tspl_company_master.PanNo_Issue_Date,103) as PanNo_Issue_Date, case when TSPL_ITEM_MASTER.is_scheme_item=1 then 'Y' else 'N' end as is_scheme_item ,To_GP_Location.CST_No as to_GP_CST_no " &
            "  ,From_GP_Location.add1 +case when len(From_GP_Location.add2)>0 then ', '+From_GP_Location.add2 else '' end +case when LEN(isnull(From_GP_Location.Add3,''))>0 then ', '+isnull(From_GP_Location.Add3,'') else ' ' end  + case when len(From_GP_Location_State.STATE_NAME   )>0 then ', '+ From_GP_Location_State.STATE_NAME  else ' ' end    as From_Location_Address_GP " &
            "  , From_GP_Location.Location_Code as From_Location_GP_Code,From_GP_Location.Location_Desc as From_Location_GP_Name,From_GP_Location.Add1 as From_GP_Add1,From_GP_Location.Add2 as From_GP_Add2,From_GP_Location.Add3 as From_GP_Add3,From_GP_Location.Add4 as From_GP_Add4,From_GP_Location_State.STATE_NAME as From_GP_State_Name, From_GP_Location.TIN_No as From_GP_TINNO,To_GP_Location.Location_Code as To_location_GP_Code,To_GP_Location.Location_Desc as To_Location_GP_Name,To_GP_Location.Add1 as To_GP_Add1,To_GP_Location.Add2 as To_GP_Add2,To_GP_Location.Add3 as To_GP_Add3,To_GP_Location.Add4 as To_GP_Add4,To_GP_Location_State.STATE_NAME as TO_GP_State_Name,To_GP_Location.TIN_No  as To_GP_TINNO,LMFrom.TIN_No as Loc_Tin_No, To_GP_Location.add1 +case when len(To_GP_Location.add2)>0 then ', '+To_GP_Location.add2 else '' end +case when LEN(isnull(To_GP_Location.Add3,''))>0 then ', '+isnull(To_GP_Location.Add3,'') else ' ' end  + case when len(To_GP_Location_State.STATE_NAME   )>0 then ', '+ To_GP_Location_State.STATE_NAME  else ' ' end     as Location_Address_GP,  From_GP_Location.Loc_Short_Name as From_GP_Short_Name,To_GP_Location.Loc_Short_Name as To_Gp_Loc_Short_Name,LMFrom.Loc_Short_Name as LmFrom_Short_Name,  TSPL_COMPANY_MASTER.logo_Img,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ' , '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ' , '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.tin_no)>0 then ' , TIN No '+TSPL_COMPANY_MASTER.tin_no else ' ' end as Comp_Add_GP,TSPL_COMPANY_MASTER.CE_Division as GP_Division,TSPL_COMPANY_MASTER.ServiceTax_Reg_No +case when len(TSPL_COMPANY_MASTER.Ecc_No)>0 then ''+TSPL_COMPANY_MASTER.Ecc_No else '' end as GP_ECC_No,TSPL_COMPANY_MASTER.CE_Range as GP_CE_Range, To_GP_Location.city_code as City_Name ,TO_LOCATION_CITY.City_Name AS Destination_City,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Remarks as Description ,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Line_No,SMFrom.STATE_NAME as Loc_State_Name, SMFrom.state_code as frm_State_code, LMFrom.HOAdd1, LMFrom.HOAdd2, tspl_company_master.cst_lst as Comp_CSt_LST " &
            "  , TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vehicle_No, 0 as Alter_UnitQty " &
            "  , 0 as HeadDisc_PerAmt, TSPL_ITEM_MASTER.Cheapter_Heads, TSPL_ITEM_master.ITF_CODE as Chap_Desc, LMFrom.Registration_Number " &
            "  , TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Modify_By, TSPL_COMPANY_MASTER.Pan_No as Comp_PANNO,  LMFrom.add1 +case when len(LMFrom.add2)>0 then ', '+LMFrom.add2 else '' end +case when LEN(isnull(LMFrom.Add3,''))>0 then ', '+isnull(LMFrom.Add3,'') else ' ' end + case when LEN(CMFrom.City_Name)>0 then ', '+CMFrom .City_Name else ' ' end + case when len(SMFrom.STATE_NAME  )>0 then ', '+ SMFrom.STATE_NAME else ' ' end  + case when len(LMFrom.Pin_Code   )>0 then ', Pin Code - '+ cast(LMFrom.Pin_Code  as varchar)  else ' ' end  + case when len(LMFrom.Tin_No     )>0 then ', Tin No - '+ cast(LMFrom.Tin_No as varchar)  else ' ' end  + Case when len(ISNULL(LMFrom.Phone1,''))>0 and LMFrom.Phone1='(+__)__________' then '' else ', Phone'+LMFrom.Phone1 end +  Case When   ISNULL(LMFrom.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(LMFrom.Email    )>0 then ', Email - '+ LMFrom.Email else '' end  as Location_Address, LMFrom.CST_No as Loc_CSTNo, LMFrom.Excisable as loc_Excisable,LMFrom.Range_Address as Loc_range_Add,LMFrom.Division_Address  as loc_Division_Address,LMFrom.Commissionerate  as Loc_Commissionerate, '' as Challan_No, '' as Challan_Date, '' as Removal_Date " &
            "  , '' as WayBill_No, TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(CM_Company.City_Name)>0 then ', '+CM_Company.City_Name else ' ' end + case when len(SMCompany.STATE_NAME  )>0 then ', '+ SMCompany.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No)>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end + case when len(TSPL_COMPANY_MASTER.Pan_No)>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end+ Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ',Phone'+TSPL_COMPANY_MASTER.Phone1 end +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ',  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Comp_Address, LMFrom .Add1 as Loc_Add1, LMFrom.Add2 as Loc_ADd2, LMFrom.Add3 as Loc_Add3, LMFrom.Pin_Code as Loc_Pin_Code, LMFrom.TIN_No as Loc_TinNo, Case when ISNULL(LMFrom.Phone1,'')='(+__)__________' then '' else LMFrom.Phone1 end +  Case When   ISNULL(LMFrom.Phone2,'')<>'(+__)__________' Then ', '+ LMFrom.Phone2 Else'' End as  Loc_Phn, LMFrom.Email as Loc_Email " &
            "  , 0 as Scheme_Qty, '' as Scheme_Item_UOM " &
            "  ,  TSPL_COMPANY_MASTER .State as Comp_State, '' as Buyer_order_no, '' as Buyer_order_date, '' as Terms_of_delivery " &
            "   , TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO as InvoiceNo,'' as ShipmentNo, 0 as Alt_Qty, '' as ShipmentDate, '' as DeliveryOrderNo " &
            "  , LMFrom.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName, ISNULL(TSPL_COMPANY_MASTER.Phone1,'')+  Case When ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone, TSPL_COMPANY_MASTER.Fax as CompFax,TSPL_COMPANY_MASTER.Email as ComMail,TSPL_COMPANY_MASTER.CINNo as CompCinNo,TSPL_COMPANY_MASTER.Pan_No as ComPanNo,  TSPL_COMPANY_MASTER.CST_LST as CompCSTLST,TSPL_COMPANY_MASTER.Pincode as ComPINCode, TSPL_COMPANY_MASTER .Tin_No as ComTinNO,ISNULL(tspl_company_Master.ADD1,'') as Compaddress1,ISNULL(tspl_company_Master.ADD2,'')   as Compaddress2,ISNULL(tspl_company_Master.ADD3,'') as Compaddress3, '' as P_Add1, '' as P_Add2, '' as P_Add3, '' as P_PinNo, '' as P_CstNo, '' as P_TinNo, '' as P_Email, '' as P_Fax, '' as P_LstNo, '' as P_CustCode, '' as P_Cust_Name, '' as P_City_Name, '' as P_State_Name, '' as P_Cust_Phn, LMTo.Location_Code as Cust_Code, LMTo.Location_Desc as Customer_Name, LMTo.Add1 as Cust_Add1, LMTo.Add2 as Cust_add2, LMTo.Add3 as cust_add3, case when ISNULL(LMTo.Phone1,'')='(+__)__________' then '' else LMTo.Phone1 end +  Case When   ISNULL(LMTo.Phone2,'')<>'(+__)__________' Then ', '+ LMTo.Phone2 Else'' End as Cust_Phn,LMTo.Tin_No  as Cust_TinNo, LMTo.CST_No as Cust_CSTNo, '' Cust_LSTNo, LMTo.Email as Cust_Email, LMTo.PIN_Code as Cust_PinCode, CMTo.City_Name as Cust_City_Name, '' as Cust_Fax, SMTo.STATE_NAME as Cust_State_Name, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.item_code, TSPL_ITEM_MASTER.Item_Desc as itemdesc " &
            "  , TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.qty,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Qty  AS Item_Qty,   COALESCE(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.amount,0)   AS Item_Amount, ''  AS Item_B_uom,'' AS Item_BatchNo " &
            "  ,IT.Is_Tax_Exempted,TSPL_ITEM_MASTER.Is_Batch_Item,  0 as mrp, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.amount as amount, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.UOM as uom, '' as RATE_UOM ,TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Rate as itemcost " &
            "  , tax1.Tax_Code_Desc as tax1name,isnull (TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax1_amt,0) as txt1amt,   tax2.Tax_Code_Desc as tax2name,isnull (TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax2_amt,0) as txt2amt,   tax3.Tax_Code_Desc as tax3name,   isnull (TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax3_amt,0) as txt3amt,   tax4.Tax_Code_Desc as tax4name,   isnull (TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax4_amt,0) as txt4amt,   tax5.Tax_Code_Desc as tax5name,  isnull (TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax5_amt,0) as txt5amt,   tax6.Tax_Code_Desc as tax6name,  isnull (TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax6_amt,0) as txt6amt,   tax7.Tax_Code_Desc as tax7name, isnull (TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax7_amt,0) as txt7amt,   tax8.Tax_Code_Desc as tax8name, isnull (TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,  isnull (TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax9_amt,0) as txt9amt,   tax10.Tax_Code_Desc as tax10name,  isnull (TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax10_amt,0) as txt10amt, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX1_Rate, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX2_Rate, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX3_Rate, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX4_Rate, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX5_Rate, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX6_Rate, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX7_Rate, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX8_Rate, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX9_Rate, TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX10_Rate,ISNULL(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX1_Amt,0) AS DTax1_Amt,  ISNULL(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX2_Amt,0) AS DTax2_Amt, ISNULL(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX3_Amt,0) AS DTax3_Amt,  ISNULL(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX4_Amt,0) AS DTax4_Amt,  ISNULL(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX5_Amt,0) AS DTax5_Amt,  ISNULL(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX6_Amt,0) AS DTax6_Amt,  ISNULL(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX7_Amt,0) AS DTax7_Amt,  ISNULL(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX8_Amt,0) AS DTax8_Amt,  ISNULL(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX9_Amt,0) AS DTax9_Amt,  ISNULL(TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TAX10_Amt,0) AS DTax10_Amt " &
            "  ,  isnull(TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD .Total_Tax_Amount,0) as Total_Tax_Amt, TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Total_Net_Amount as Total_Amt, '1' as CopyType, '' as Add_Charge_Name1, 0 as Add_Charge_Amt1, '' as Add_Charge_Name2, 0 as Add_Charge_Amt2, '' as Add_Charge_Name3, 0 as Add_Charge_Amt3, '' as Add_Charge_Name4, 0 as Add_Charge_Amt4, '' as Add_Charge_Name5, 0 as Add_Charge_Amt5, '' as Add_Charge_Name6, 0 as Add_Charge_Amt6, '' as Add_Charge_Name7, 0 as Add_Charge_Amt7, '' as Add_Charge_Name8, 0 as Add_Charge_Amt8, '' as Add_Charge_Name9, 0 as Add_Charge_Amt9, '' as Add_Charge_Name10, 0 as Add_Charge_Amt10 , Dtax1.Type as Taxtype1, Dtax2.Type as Taxtype2, Dtax3.Type as Taxtype3, Dtax4.Type as Taxtype4, Dtax5.Type as Taxtype5, Dtax6.Type as Taxtype6, Dtax7.Type as Taxtype7, Dtax8.Type as Taxtype8, Dtax9.Type as Taxtype9, Dtax10.Type as Taxtype10,  TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Modify_Date,'' AS Time_of_Prepration,'' AS Time_of_Removal,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Vehicle_Code, TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.Email,TSPL_COMPANY_MASTER.Tcan_No AS WebSite,TSPL_COMPANY_MASTER.Ecc_No,TSPL_COMPANY_MASTER.tin_no,TSPL_COMPANY_MASTER.ServiceTax_Reg_No,AM.Abatement_Percent " &
            "  ,(TSPL_COMPANY_MASTER.Add1+' '+TSPL_COMPANY_MASTER.Add2+' '+TSPL_COMPANY_MASTER.Add3+' '+TSPL_COMPANY_MASTER.State) as COMP_ADDRESS2,TSPL_COMPANY_MASTER.CE_Commissionerate,'' as Tariff,TSPL_COMPANY_MASTER.Access_Officer as FSSAI,LMFROM.Location_Desc AS LOCFROM_Customer_Name,'' as Customer_EccNo,From_GP_Location.Loc_Short_Name,case when len(From_GP_Location.Pin_Code   )>0 then From_GP_Location.Pin_Code ELSE Null END AS From_Location_Pin,case when len(From_GP_Location.Phone1   )>0 then From_GP_Location.Phone1 when len(From_GP_Location.Phone2)>0 then From_GP_Location.Phone2 ELSE Null END AS From_Location_phone,TSPL_COMPANY_MASTER.Insurance_No,TSPL_COMPANY_MASTER.Insurance_Comp_Name,CONVERT(VARCHAR(15),TSPL_COMPANY_MASTER.Insurance_Valid_Date,103) AS Insurance_Valid_Date,TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Remarks " &
            "  ,'' AS Item_MRP,TAX1=REPLACE((CASE WHEN TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX1!='' THEN(Tax1.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX1_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX1_Amt)) ELSE '' END)+CHAR(13)+(CASE WHEN TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX2!='' THEN(TAX2.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX2_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX2_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX3!=''  THEN(TAX3.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX3_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX3_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX4!='' THEN(TAX4.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX4_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX4_Amt))ELSE '' END) +CHAR(13)+(CASE WHEN TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX5!='' THEN(TAX5.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX5_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX5_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX6!='' THEN(TAX6.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX6_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX6_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX7!='' THEN(TAX7.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX7_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX7_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX8!='' THEN(TAX8.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX8_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX8_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX9!='' THEN(TAX9.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX9_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX9_Amt))ELSE '' END) +CHAR(13)+ (CASE WHEN TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX10!=''  THEN(TAX10.Tax_Code_Desc+' ( '+CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX10_Rate)+' % ) '+ CONVERT(NVARCHAR(10),TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TAX10_Amt))ELSE '' END),CHAR(13),'') " &
            "  From TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS " &
            "   join TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD  on TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TRANSFER_NO=TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.TRANSFER_NO " &
            "   Left OUTER JOIN TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.ITEM_CODE = TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.iTEM_CODE " &
            "   left outer join TSPL_LOCATION_MASTER LMFrom on LMFrom.Location_Code=  TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction " &
            "   Left outer join TSPL_LOCATION_MASTER LMTo on LMTo.GIT_Location =  TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.To_Locaction " &
            "   Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.Comp_Code " &
            "   Left OUTER JOIN TSPL_CITY_MASTER  AS CM_Company ON CM_Company.City_Code =TSPL_COMPANY_MASTER.City_Code " &
            "   LEFT OUTER JOIN TSPL_CITY_MASTER  AS CMFrom ON CMFrom.City_Code =LMFrom.City_Code " &
            "   Left OUTER JOIN TSPL_CITY_MASTER  AS CMTo ON CMTo.City_Code =LMTo.City_Code " &
            "   LEFT OUTER JOIN TSPL_STATE_MASTER AS SMCompany  ON SMCompany.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
            "   Left OUTER JOIN TSPL_STATE_MASTER SMFrom on SMFrom.STATE_CODE=LMFrom.State " &
            "   LEFT OUTER JOIN TSPL_STATE_MASTER SMTo ON SMTo.State_Code=LMTo.State " &
            "  Left outer join tspl_item_uom_detail on tspl_item_uom_detail.item_code=TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.item_code And tspl_item_uom_detail.uom_code=TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.UOM  " &
            "   left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code =TSPL_ITEM_MASTER.Cheapter_Heads " &
            "   Left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax1 " &
            "   left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.tax2 " &
            "   Left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD .TAX3 " &
            "   Left outer join TSPL_TAX_MASTER As tax4 On tax4.Tax_Code= TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD .tax4 " &
            "   Left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD .tax5 " &
            "   left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD .TAX6 " &
            "   Left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD .TAX7 " &
            "   left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD .TAX8 " &
            "   Left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD .TAX9 " &
            "   left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD .TAX10 " &
            "   Left outer join TSPL_TAX_MASTER as Dtax1 on Dtax1.tax_code =TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.tax1  left outer join tspl_tax_master as Dtax2 on Dtax2.tax_code = TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.tax2 left outer join tspl_tax_master as Dtax3 on Dtax3.Tax_Code=TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS .TAX3  left outer join TSPL_TAX_MASTER as Dtax4 on Dtax4.Tax_Code= TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS .tax4  left outer join TSPL_TAX_MASTER as Dtax5 on Dtax5.Tax_Code=TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS .tax5  left outer join TSPL_TAX_MASTER as Dtax6 on Dtax6.Tax_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS .TAX6  left outer join TSPL_TAX_MASTER as Dtax7 on Dtax7.Tax_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS .TAX7  left outer join TSPL_TAX_MASTER as Dtax8 on Dtax8.Tax_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS .TAX8  left outer join TSPL_TAX_MASTER as Dtax9 on Dtax9.Tax_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS .TAX9  left outer join TSPL_TAX_MASTER as Dtax10 on Dtax10.Tax_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS .TAX10  " &
            "     left join tspl_location_master as From_GP_Location on From_GP_Location.Location_Code =TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.From_Locaction " &
            "     Left Join TSPL_STATE_MASTER as From_GP_Location_State on From_GP_Location_State.STATE_CODE =From_GP_Location.State   " &
            "     left outer join TSPL_LOCATION_MASTER AS To_GP_Location on To_GP_Location.Location_Code  " &
            "     =  TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD.TO_Locaction   " &
            "     left join TSPL_STATE_MASTER as To_GP_Location_State on To_GP_Location_State.STATE_CODE =To_GP_Location.State " &
            "      Left OUTER JOIN TSPL_CITY_MASTER  TO_LOCATION_CITY ON TO_LOCATION_CITY.City_Code= To_GP_Location.City_Code    " &
            "  	Left JOIN TSPL_ITEM_MASTER IT ON TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS.Item_Code=IT.Item_Code  " &
            "      Left Join TSPL_ABATEMENT_MASTER AM ON AM.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code   " &
            "  	where 2=2 and  TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD. TRANSFER_NO = '" + txtDocNo.Value + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim dtDocdate As Date?
                dtDocdate = Nothing
                dtDocdate = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "RptJWOTransferOther_Local", "JOB WORK CHALLAN", dtDocdate)
                frmCRV = Nothing
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtVehicleCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVehicleCode._MYValidating
        Try
            Dim qry As String = "Select distinct  vehicle_id ,Description from TSPL_VEHICLE_MASTER"
            If isButtonClicked Then
                txtVehicleCode.Value = clsCommon.ShowSelectForm("Vehicle No", qry, "vehicle_id", "", txtVehicleCode.Value, "vehicle_id", isButtonClicked)
            End If
            lblVehicleNo.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleCode.Value) + "'")
            txtvehicle_mannual_no.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select number from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleCode.Value) + "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub SetTax()
        If clsCommon.myLen(clsCommon.myCstr(txtToLocation.Value)) <= 0 Then
            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroupforTransfer(txtFromLocation.Value, txtToLocation.Value, "T", txtDate.Value)
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
            SetTaxDetails()
        Else
            txtTaxGroup.Value = clsLocationWiseTax.GetDefaultTaxGroupforTransfer(txtFromLocation.Value, txtToLocation.Value, "T", txtDate.Value)
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfPurchaseType(txtTaxGroup.Value, Nothing)
            SetTaxDetails()
        End If
    End Sub
    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Try
            GstStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
            If clsCommon.myLen(txtFromLocation.Value) > 0 AndAlso clsCommon.myLen(txtToLocation.Value) > 0 Then
                strTaxType = clsLocationWiseTax.TaxType(txtFromLocation.Value, txtToLocation.Value, "T", txtDate.Value, Nothing)
                If GstStatus = True AndAlso clsCommon.CompairString(strTaxType, "L") = CompairStringResult.Equal Then
                    txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroupForTransfer(txtFromLocation.Value, txtToLocation.Value, "T", txtTaxGroup.Value, isButtonClicked)
                Else
                    txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroupForTransfer(txtFromLocation.Value, txtToLocation.Value, "T", txtTaxGroup.Value, isButtonClicked)
                End If
            Else
                txtTaxGroup.Value = clsLocationWiseTax.FinderForTaxGroupForTransfer(txtFromLocation.Value, txtToLocation.Value, "T", txtTaxGroup.Value, isButtonClicked)

            End If
            txtTaxGroup.Enabled = True
            SetTaxDetails()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Sub SetTaxDetails()
        LoadBlankGridTax()
        Dim dt As New DataTable
        If clsERPFuncationality.GetGSTStatus(txtDate.Value) = True Then
            dt = clsTaxGroupMaster.GetTaxDetailsByLocationForTransfer(txtTaxGroup.Value, "T", txtToLocation.Value, txtFromLocation.Value)
        Else
            dt = clsTaxGroupMaster.GetTaxDetailsByLocationForTransfer(txtTaxGroup.Value, "T", txtToLocation.Value, txtFromLocation.Value)
        End If
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
                    gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(dr("TaxRate"))
                End If
            Next
            If CalculateTaxRatefromItemwsieTaxOnSale = 1 Then
                SetitemWiseTaxSetting(True, False)
            End If
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
    Private Sub UpdateAllTotals()
        Dim dblTotAmt As Double = 0
        Dim dblTotDisAmt As Double = 0
        Dim dblAmtAfterDis As Double = 0

        Dim dblTaxAssessableAmt As Double = 0

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



        Dim dblTaxTotAmt As Double = 0
        Dim dblNetAmt As Double = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            If (clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0) Then
                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
                dblTaxAmt1 = dblTaxAmt1 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt1).Value)
                dblTaxAmt2 = dblTaxAmt2 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt2).Value)
                dblTaxAmt3 = dblTaxAmt3 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt3).Value)
                dblTaxAmt4 = dblTaxAmt4 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt4).Value)
                dblTaxAmt5 = dblTaxAmt5 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt5).Value)
                dblTaxAmt6 = dblTaxAmt6 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt6).Value)
                dblTaxAmt7 = dblTaxAmt7 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt7).Value)
                dblTaxAmt8 = dblTaxAmt8 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt8).Value)
                dblTaxAmt9 = dblTaxAmt9 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt9).Value)
                dblTaxAmt10 = dblTaxAmt10 + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTaxAmt10).Value)

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

                dblTaxTotAmt = dblTaxTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotTaxAmt).Value)
                dblNetAmt = dblNetAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterTax).Value)
            End If
        Next

        If clsCommon.myLen(txtTaxGroup.Value) > 0 Then
            If rbtnTaxCalAutomatic.IsChecked Then
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
                            gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = Math.Round(dblTaxAssessableAmt, 2)
                        Case 2
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt2, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt2, 2)
                            If dblTaxBaseAmt2 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt2 * 100) / dblTaxBaseAmt2, 2)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                        Case 3
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt3, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt3, 2)
                            If dblTaxBaseAmt3 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt3 * 100) / dblTaxBaseAmt3, 2)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                        Case 4
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt4, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt4, 2)
                            If dblTaxBaseAmt4 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt4 * 100) / dblTaxBaseAmt4, 2)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                        Case 5
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt5, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt5, 2)
                            If dblTaxBaseAmt5 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt5 * 100) / dblTaxBaseAmt5, 2)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                        Case 6
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt6, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt6, 2)
                            If dblTaxBaseAmt6 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt6 * 100) / dblTaxBaseAmt6, 2)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                        Case 7
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt7, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt7, 2)
                            If dblTaxBaseAmt7 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt7 * 100) / dblTaxBaseAmt7, 2)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                        Case 8
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt8, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt8, 2)
                            If dblTaxBaseAmt8 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt8 * 100) / dblTaxBaseAmt8, 2)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                        Case 9
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt9, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt9, 2)
                            If dblTaxBaseAmt9 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt9 * 100) / dblTaxBaseAmt9, 2)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                        Case 10
                            gv2.Rows(ii - 1).Cells(colTTaxAmt).Value = Math.Round(dblTaxAmt10, 2)
                            gv2.Rows(ii - 1).Cells(colTBaseAmt).Value = Math.Round(dblTaxBaseAmt10, 2)
                            If dblTaxBaseAmt10 <> 0 Then
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = Math.Round((dblTaxAmt10 * 100) / dblTaxBaseAmt10, 2)
                            Else
                                gv2.Rows(ii - 1).Cells(colTTaxRate).Value = 0
                            End If
                            gv2.Rows(ii - 1).Cells(colTTaxAssessableAmt).Value = gv2.Rows(ii - 1).Cells(colTBaseAmt).Value
                    End Select
                Next
            End If

        Else
            dblNetAmt = clsCommon.myFormat(dblTotAmt)
        End If
        lblAmtWithoutTax.Text = clsCommon.myFormat(dblTotAmt)
        lblTaxAmt.Text = clsCommon.myFormat(dblTaxTotAmt)


        dblNetAmt = dblNetAmt
        lblTotRAmt.Text = clsCommon.myFormat(dblNetAmt)
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        GstStatus = clsERPFuncationality.GetGSTStatus(txtDate.Value)
        Dim arrTaxableAuth As New List(Of String)
        Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
        Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
        Dim dblAmt As Double = Math.Round(dblQty * dblRate, 2)
        gv1.Rows(IntRowNo).Cells(colAmount).Value = dblAmt
        Dim strItemCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        'Dim strItemtype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select case when CSA_TYPE='BULK-DESI GHEE' then 1 when CSA_TYPE='CPD-DESI GHEE' then 1 else 0 end   from TSPL_ITEM_MASTER where Item_Code ='" & strItemCode & "' "))
        strTaxType = clsLocationWiseTax.TaxType(txtFromLocation.Value, txtToLocation.Value, "T", txtDate.Value, Nothing)
        If clsCommon.myLen(txtTaxGroup.Value) > 0 Then
            Dim dblAmtAfterDis As Double = dblAmt
            'If clsCommon.CompairString(strItemtype, "1") = CompairStringResult.Equal Then
            For ii As Integer = 1 To 10
                Dim Strii As String = clsCommon.myCstr(ii)
                If rbtnTaxCalAutomatic.IsChecked Then
                    Dim strTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAX" + Strii)).Value)
                    If clsCommon.myLen(strTaxCode) > 0 Then
                        Dim dblTaxRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value)
                        Dim IsSurTax As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISSURTAX" + Strii)).Value)
                        Dim strSurTaxCode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + Strii)).Value)
                        Dim IsTaxable As Boolean = clsCommon.myCBool(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + Strii)).Value)
                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0
                        If IsSurTax Then
                            Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                            dblBaseAmt = dblSurTaxAmt
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)

                            If clsERPFuncationality.GetGSTStatus(txtDate.Value) = True Then
                                dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                            Else
                                dblBaseAmt = (dblAmtAfterDis + dblOtherTaxAmt)
                            End If
                        End If
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXBASEAMT" + Strii)).Value = Math.Round(dblBaseAmt, 2)
                        dblTaxAmt = (dblBaseAmt * dblTaxRate) / 100
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = Math.Round(dblTaxAmt, IIf(objCommonVar.IsRoundOffTaxToZeroDecimal, 0, 2))
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
                    End If
                ElseIf rbtnTaxCalManual.IsChecked Then
                    If gv2.Rows.Count >= ii Then
                        Dim dblTaxAmt As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxAmt).Value)
                        Dim dblTaxRate As Double = clsCommon.myCdbl(gv2.Rows(ii - 1).Cells(colTTaxRate).Value)
                        Dim dblCurrRowAmt As Double = clsCommon.myCdbl(gv1.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colAmount).Value)
                        Dim dblTotAmt As Double = 0
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colAmount).Value)
                        Next
                        Dim dblCurrCalTax As Double = 0
                        If dblTotAmt <> 0 Then
                            dblCurrCalTax = Math.Round(clsCommon.myCdbl(dblTaxAmt * dblCurrRowAmt / dblTotAmt), 2, MidpointRounding.ToEven)
                        End If
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + Strii)).Value = dblCurrCalTax
                        gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("COLTAXRATE" + Strii)).Value = dblTaxRate
                    End If
                End If
            Next
            Dim dblTotTaxAmt As Double = GetCurrentRowTotalTaxAmt(IntRowNo)
            Dim dblAmtAfterTax As Double = dblAmtAfterDis + dblTotTaxAmt
            gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = dblTotTaxAmt
            gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = dblAmtAfterTax
            'Else
            '    gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = 0
            '    gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = dblAmt
            'End If

        Else
            gv1.Rows(IntRowNo).Cells(colTotTaxAmt).Value = 0
            gv1.Rows(IntRowNo).Cells(colAmtAfterTax).Value = 0
        End If

    End Sub
    Private Function GetCurrentRowTotalTaxAmt(ByVal IntRowNo As Integer) As Double
        Dim dblTotTax As Double = 0
        For ii As Integer = 1 To 10
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
    Private Function GetCurrentRowSurTaxAmt(ByVal IntRowNo As Integer, ByVal intEndCol As Integer, ByVal strSurTaxCode As String) As Double
        For ii As Integer = 1 To intEndCol
            Dim strii As String = clsCommon.myCstr(ii)
            If IntRowNo < 0 Then
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            Else
                If clsCommon.CompairString(strSurTaxCode, clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTax" + strii)).Value)) = CompairStringResult.Equal Then
                    Return clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strii)).Value)
                End If
            End If
        Next
        Return 0
    End Function
    Sub SetitemWiseTaxSetting(ByVal isChangeRate As Boolean, ByVal isForCurrentRow As Boolean)
        Dim dt As New DataTable
        If clsERPFuncationality.GetGSTStatus(txtDate.Value) = True Then
            dt = clsTaxGroupMaster.GetTaxDetailsByLocationForTransfer(txtTaxGroup.Value, "T", txtToLocation.Value, txtFromLocation.Value)

        Else
            dt = clsTaxGroupMaster.GetTaxDetailsByLocationForTransfer(txtTaxGroup.Value, "T", txtToLocation.Value, txtFromLocation.Value)
        End If
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            If isForCurrentRow Then
                If clsERPFuncationality.GetGSTStatus(txtDate.Value) = True Then
                    BlankTaxDetails(gv1.CurrentRow.Index, isChangeRate)
                Else
                    BlankTaxDetails(gv1.CurrentRow.Index)
                End If

                If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                        If (CalculateTaxRatefromItemwsieTaxOnSale = 0) Then
                            If isChangeRate Then
                                gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                            End If
                        Else
                            If isChangeRate Then
                                Dim objTM As clsItemWiseTaxAuthority
                                objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "T")
                                If objTM IsNot Nothing Then
                                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTM.TAX_Rate
                                    gv1.CurrentRow.Cells(colItemwiseTaxCode).Value = objTM.HCODE
                                End If
                            End If
                        End If
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.CurrentRow.Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.CurrentRow.Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Else
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    If clsERPFuncationality.GetGSTStatus(txtDate.Value) = True Then
                        BlankTaxDetails(intRowNo, isChangeRate)
                    Else
                        BlankTaxDetails(intRowNo)
                    End If
                    If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode).Value) > 0 Then
                        Dim ii As Integer = 1
                        For Each dr As DataRow In dt.Rows
                            Dim strII As String = clsCommon.myCstr(ii)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value = clsCommon.myCstr(dr("Tax_Code"))
                            If (CalculateTaxRatefromItemwsieTaxOnSale = 0) Then
                                If isChangeRate Then
                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(dr("TaxRate"))
                                End If
                            Else
                                If isChangeRate Then
                                    Dim objTM As clsItemWiseTaxAuthority
                                    objTM = clsItemWiseTaxAuthority.GetAutoItemwiseTaxRate(clsCommon.myCstr(gv1.Rows(intRowNo).Cells(colICode).Value), clsCommon.myCstr(txtTaxGroup.Value), clsCommon.myCstr(gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTax" + strII)).Value), txtDate.Value, "T")
                                    If objTM IsNot Nothing Then
                                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = objTM.TAX_Rate
                                        gv1.Rows(intRowNo).Cells(colItemwiseTaxCode).Value = objTM.HCODE
                                    End If
                                End If
                            End If
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                            gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                            ii = ii + 1
                        Next
                    End If
                Next
            End If
        End If
    End Sub
    Sub SetitemWiseTaxOnlySetting()
        ' Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetails(txtTaxGroup.Value)
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocationForTransfer(txtTaxGroup.Value, "T", txtToLocation.Value, txtFromLocation.Value)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(intRowNo).Cells(colICode)) > 0 Then
                    Dim ii As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Taxable")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Surtax")), "Y") = CompairStringResult.Equal)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = clsCommon.myCstr(dr("Surtax_Tax_Code"))
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISEXCISABLE" + strII)).Value = clsCommon.myCBool(clsCommon.CompairString(clsCommon.myCstr(dr("Excisable")), "Y") = CompairStringResult.Equal)
                        ii = ii + 1
                    Next
                End If
            Next
        End If
    End Sub
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer)
        BlankTaxDetails(intRowNo, True)
    End Sub
    Private Sub BlankTaxDetails(ByVal intRowNo As Integer, ByVal isBlankRate As Boolean)
        For ii As Integer = 1 To 10
            Dim strII As String = clsCommon.myCstr(ii)
            If intRowNo < 0 Then
                gv1.CurrentRow.Cells(clsCommon.myCstr("colTax" + strII)).Value = Nothing
                gv1.CurrentRow.Cells(clsCommon.myCstr("COLTAXBASEAMT" + strII)).Value = Nothing
                If isBlankRate Then
                    gv1.CurrentRow.Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                    gv1.CurrentRow.Cells(colItemwiseTaxCode).Value = Nothing
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
                    gv1.Rows(intRowNo).Cells(colItemwiseTaxCode).Value = Nothing
                End If
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISTAXABLE" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("ISSURTAX" + strII)).Value = Nothing
                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("SURTAXCODE" + strII)).Value = Nothing
            End If
        Next
    End Sub
    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick
        Try
            If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                If gv1.CurrentColumn Is gv1.Columns(colTotTaxAmt) AndAlso rbtnTaxCalAutomatic.IsChecked Then
                    Dim frm As New FrmPOItemTaxDetails()
                    frm.strLineNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLineNo).Value)
                    frm.strItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
                    frm.strItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
                    frm.dblTotTax = clsCommon.myCdbl(gv1.CurrentRow.Cells(colTotTaxAmt).Value)
                    frm.dblAmtAfterDis = clsCommon.myCdbl(gv1.CurrentRow.Cells(colAmount).Value)
                    frm.strTaxType = "T"
                    frm.strTransLocation = clsCommon.myCstr(txtFromLocation.Value)
                    frm.strVendorCustomerCode = clsCommon.myCstr(txtToLocation.Value)
                    frm.strTaxGroup = clsCommon.myCstr(txtTaxGroup.Value)
                    If clsCommon.myLen(frm.strItemCode) > 0 Then
                        frm.ArrIn = New List(Of clsTempItemTaxDetails)
                        For ii As Integer = 1 To 10
                            Dim strii As String = clsCommon.myCstr(ii)
                            Dim obj As New clsTempItemTaxDetails()
                            obj.AuthorityCode = clsCommon.myCstr(gv1.CurrentRow.Cells("COLTAX" + strii).Value)
                            If clsCommon.myLen(obj.AuthorityCode) > 0 Then
                                obj.AuthorityName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Code_Desc from TSPL_TAX_MASTER WHERE Tax_Code='" + obj.AuthorityCode + "'"))
                                obj.Rate = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value)
                                obj.BaseAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value)
                                obj.TaxAmt = clsCommon.myCdbl(gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value)
                                obj.isSurTax = clsCommon.myCBool(gv1.CurrentRow.Cells("ISSURTAX" + strii).Value)
                                obj.SurTax = clsCommon.myCstr(gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value)
                                obj.IsTaxable = clsCommon.myCBool(gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value)
                                frm.ArrIn.Add(obj)
                            End If
                        Next

                        frm.ShowDialog()
                        If frm.ArrOut IsNot Nothing AndAlso frm.ArrOut.Count > 0 Then
                            BlankTaxDetails(gv1.CurrentRow.Index)
                            For ii As Integer = 0 To frm.ArrOut.Count - 1
                                Dim strii As String = clsCommon.myCstr(ii + 1)
                                gv1.CurrentRow.Cells("COLTAX" + strii).Value = frm.ArrOut(ii).AuthorityCode
                                gv1.CurrentRow.Cells("COLTAXRATE" + strii).Value = frm.ArrOut(ii).Rate
                                gv1.CurrentRow.Cells("COLTAXBASEAMT" + strii).Value = frm.ArrOut(ii).BaseAmt
                                gv1.CurrentRow.Cells("COLTAXAMT" + strii).Value = frm.ArrOut(ii).TaxAmt
                                gv1.CurrentRow.Cells("ISSURTAX" + strii).Value = frm.ArrOut(ii).isSurTax
                                gv1.CurrentRow.Cells("SURTAXCODE" + strii).Value = frm.ArrOut(ii).SurTax
                                gv1.CurrentRow.Cells("ISTAXABLE" + strii).Value = frm.ArrOut(ii).IsTaxable
                            Next
                            gv1.CurrentRow.Cells(colTotTaxAmt).Value = frm.dblTotTax
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                            UpdateAllTotals()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub gv2_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv2.DoubleClick
        Try
            If rbtnTaxCalAutomatic.IsChecked Then
                If CalculateTaxRatefromItemwsieTaxOnSale = 0 Then
                    Dim dblNewRate As Double = clsLocationWiseTax.FinderForTaxRateForTransfer(txtFromLocation.Value, txtTaxGroup.Value, clsCommon.myCstr(gv2.CurrentRow.Cells(colTTaxAutCode).Value), txtToLocation.Value, "T")
                    Dim intRowNo As Integer = gv2.CurrentRow.Index
                    If gv1.RowCount > 0 AndAlso intRowNo >= 0 Then
                        Dim strII As String = clsCommon.myCstr(intRowNo + 1)
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            gv1.Rows(ii).Cells("COLTAXRATE" + strII).Value = dblNewRate
                        Next
                    End If
                    For ii As Integer = 0 To gv1.Rows.Count - 1
                        UpdateCurrentRow(ii)
                    Next
                    UpdateAllTotals()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub gv2_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        Try
            If e.Column.Index >= 0 Then
                If (e.Column Is gv2.Columns(colTTaxAmt)) Then
                    gv2.CurrentRow.Cells(colTTaxAmt).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                ElseIf (e.Column Is gv2.Columns(colTTaxRate)) Then
                    gv2.CurrentRow.Cells(colTTaxRate).ReadOnly = rbtnTaxCalAutomatic.IsChecked
                End If
            End If

            'Dim cell As GridDataCellElement = TryCast(e.CellElement, GridDataCellElement)
            'cell.GradientStyle = GradientStyles.Solid
            'cell.BackColor = Color.FromArgb(243, 181, 51)
            'End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub rbtnTaxCalAutomatic_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnTaxCalAutomatic.ToggleStateChanged, rbtnTaxCalManual.ToggleStateChanged
        If Not isInsideLoadData Then
            If rbtnTaxCalAutomatic.IsChecked Then
                SetTaxDetails()
            ElseIf rbtnTaxCalManual.IsChecked Then
                For intRowNo As Integer = 0 To gv2.Rows.Count - 1
                    gv2.Rows(intRowNo).Cells(colTTaxRate).Value = Nothing
                    gv2.Rows(intRowNo).Cells(colTTaxAmt).Value = Nothing
                    gv2.Rows(intRowNo).Cells(colTBaseAmt).Value = Nothing
                Next
                For intRowNo As Integer = 0 To gv1.Rows.Count - 1
                    For ii As Integer = 1 To 10
                        Dim strII As String = clsCommon.myCstr(ii)
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = Nothing
                        gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxAmt" + strII)).Value = Nothing
                    Next
                Next
            End If
        End If
    End Sub
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub
    Private Sub btnReverseAndUnpost_Click(sender As Object, e As EventArgs) Handles btnReverseAndUnpost.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsJWOTransferOtherHead.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub frmJWOTransferOther_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                OpenICodeList(True)
            End If
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                  "TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD " + Environment.NewLine +
                                                  "TSPL_JOURNAL_MASTER (Journal Voucher Entry - After Posting )  " + Environment.NewLine +
                                                  "TSPL_JOURNAL_DETAILS  (After Posting) " + Environment.NewLine +
                                                  "TSPL_INVENTORY_MOVEMENT  - After Posting ")
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseAndUnpost.Visible = True
            End If
        End If
    End Sub
    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.F5 Then
            If ApplyFEFO = True Then
                OpenBatchItemIfFIFIOSettingON()
            ElseIf Not RunBatchFifowise Then
                OpenBatchItem()
            Else
                OpenBatchItemIfFIFIOSettingON()
            End If
        End If
    End Sub
    Public Sub OpenBatchItemIfFIFIOSettingON()
        Dim arr As List(Of clsBatchInventory) = Nothing
        Dim strBatchunion As String = ""
        If clsCommon.myLen(gv1.CurrentRow.Cells(colICode).Value) > 0 Then
            arr = TryCast(gv1.CurrentRow.Cells(colICode).Tag, List(Of clsBatchInventory))
        End If
        If Not arr Is Nothing Then
            If arr.Count > 0 Then
                For Each obj As clsBatchInventory In arr
                    strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                Next
                clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
            End If
        End If
    End Sub

    Private Sub BtnHistory_Click(sender As Object, e As EventArgs) Handles BtnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Code not found to show history")
                txtDocNo.Focus()
            End If
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                clsERPFuncationalityold.ShowTransHistoryData(clsCommon.myCstr(txtDocNo.Value), "TRANSFER_NO", "TSPL_JOB_WORK_OUTWARD_TRANSFER_HEAD", "TSPL_JOB_WORK_OUTWARD_TRANSFER_DETAILS")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class