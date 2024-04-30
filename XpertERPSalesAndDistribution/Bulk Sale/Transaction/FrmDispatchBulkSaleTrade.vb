
'--------Created By Richa 16/09/2014 Against Ticket No BM00000003892
''--updation by richar agarwal against ticket no BM00000004069,BM00000004412,BM00000004808,BM00000005027,BM00000005065
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class FrmDispatchBulkSaleTrade
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
    Dim arrLoc As String = Nothing
    Public Shared aLoc As String = Nothing
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colHSNCode As String = "HSNCode"
    Public Const colUnitCode As String = "colUnitCode"
    Public Const colQty As String = "Qty"
    Public Const colAmount As String = "colAmount"
    Public Const colFatPer As String = "colFatPer"
    Public Const colSNFPer As String = "colSNFPer"
    Public Const colRate As String = "colRate"
    Public Const colFatKG As String = "colFatKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const ColFatAmount As String = "ColFatAmount"
    Public Const ColSNFAmount As String = "ColSNFAmount"
    Public Const colFatRate As String = "colFatRate"
    Public Const colSNFRate As String = "colSNFRate"
    Public Const colFatWeightage As String = "colFatWeightage"
    Public Const colSnfWeightage As String = "colSnfWeightage"
    Public Const colFatRatio As String = "colFatRatio"
    Public Const colSNFRatio As String = "colSNFRatio"
    Public Const colStandardRate As String = "colStandardRate"
    Dim Qry As String
    Public strDocumentCode As String = Nothing
    Dim BulkPriceChartItemPosted As Boolean
    Dim AllowSNFNotManditoryInBulkSale As Boolean = False
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmDispatchBulkSaleTrade_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'FndLocationCode.Value = obj.Default_LocCode
                'LblLocationName.Text = obj.Default_LocName
                Dim LocationName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_LOCATION_MASTER where Location_Type='Virtual' and Location_Code ='" & obj.Default_LocCode & "'"))
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub fndLocationCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        Dim whrcls As String = "Location_Type='Virtual' "
        If clsCommon.myLen(arrLoc) > 0 Then
            whrcls += " and TSPL_LOCATION_MASTER.Location_Code in (" + arrLoc + ") "
        End If
        fndLocationCode.Value = clsLocation.getFinder(whrcls, fndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'"))
        Else
            lblLocationName.Text = ""
        End If
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub FrmDispatchBulkSaleTrade_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        UcAttachment1.Form_ID = MyBase.Form_ID
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N New Transaction")
        If clsCommon.myLen(strDocumentCode) > 0 Then
            LoadData(strDocumentCode, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        BulkPriceChartItemPosted = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.showPostrequiredforBulkSale, clsFixedParameterCode.showPostrequiredforBulkSale, Nothing)) = 1, True, False)
        AllowSNFNotManditoryInBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSNFNotManditoryInBulkSale, clsFixedParameterCode.AllowSNFNotManditoryInBulkSale, Nothing)) = 1, True, False))
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDispatchBulkSaleTrade)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        BtnCreateSrn.Visible = MyBase.isModifyFlag
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
        itemDesc.Width = 320
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
        strUnitCode.HeaderText = "UOM (in KG)"
        strUnitCode.Name = colUnitCode
        strUnitCode.Width = 120
        strUnitCode.ReadOnly = True
        strUnitCode.WrapText = True
        strUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(strUnitCode)

        Dim Qty As New GridViewDecimalColumn
        Qty.FormatString = "{0:n3}"
        Qty.DecimalPlaces = 3
        Qty.HeaderText = "Qty"
        Qty.Name = colQty
        Qty.Width = 120
        Qty.ReadOnly = False
        Qty.WrapText = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Qty)


        'Dim fatper As New GridViewDecimalColumn
        'fatper.FormatString = "{0:n2}"
        'fatper.HeaderText = "FAT %"
        'fatper.Name = colFatPer
        'fatper.Width = 75
        'fatper.ReadOnly = False
        'fatper.WrapText = True
        'fatper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(fatper)



        'Dim snfper As New GridViewDecimalColumn
        'snfper.FormatString = "{0:n2}"
        'snfper.HeaderText = "SNF %"
        'snfper.Name = colSNFPer
        'snfper.Width = 75
        'snfper.ReadOnly = False
        'snfper.WrapText = True
        'snfper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(snfper)

        Dim fatper As New GridViewTextBoxColumn
        fatper.HeaderText = "FAT %"
        fatper.Name = colFatPer
        fatper.Width = 75
        fatper.ReadOnly = False
        fatper.WrapText = True
        fatper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(fatper)



        Dim snfper As New GridViewTextBoxColumn
        snfper.HeaderText = "SNF %"
        snfper.Name = colSNFPer
        snfper.Width = 75
        snfper.ReadOnly = False
        snfper.WrapText = True
        snfper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(snfper)


        Dim FatRate As New GridViewDecimalColumn
        FatRate.FormatString = "{0:n2}"
        FatRate.HeaderText = "FAT Rate"
        FatRate.Name = colFatRate
        FatRate.Width = 75
        FatRate.ReadOnly = True
        FatRate.WrapText = True
        FatRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FatRate)

        Dim SNFRate As New GridViewDecimalColumn
        SNFRate.FormatString = "{0:n2}"
        SNFRate.HeaderText = "SNF Rate"
        SNFRate.Name = colSNFRate
        SNFRate.Width = 75
        SNFRate.ReadOnly = True
        SNFRate.WrapText = True
        SNFRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(SNFRate)

        Dim FatKg As New GridViewDecimalColumn
        FatKg.FormatString = "{0:n3}"
        FatKg.DecimalPlaces = 3
        FatKg.HeaderText = "FAT KG"
        FatKg.Name = colFatKG
        FatKg.Width = 75
        FatKg.ReadOnly = True
        FatKg.WrapText = True
        FatKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FatKg)


        Dim SnfKg As New GridViewDecimalColumn
        SnfKg.FormatString = "{0:n3}"
        SnfKg.DecimalPlaces = 3
        SnfKg.HeaderText = "SNF KG"
        SnfKg.Name = colSNFKG
        SnfKg.Width = 75
        SnfKg.ReadOnly = True
        SnfKg.WrapText = True
        SnfKg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(SnfKg)

        Dim FatAmount As New GridViewDecimalColumn
        FatAmount.FormatString = "{0:n2}"
        FatAmount.HeaderText = "FAT Amount"
        FatAmount.Name = ColFatAmount
        FatAmount.Width = 75
        FatAmount.ReadOnly = True
        FatAmount.WrapText = True
        FatAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FatAmount)

        Dim SNFAmount As New GridViewDecimalColumn
        SNFAmount.FormatString = "{0:n2}"
        SNFAmount.HeaderText = "SNF Amount"
        SNFAmount.Name = ColSNFAmount
        SNFAmount.Width = 75
        SNFAmount.ReadOnly = True
        SNFAmount.WrapText = True
        SNFAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(SNFAmount)

        Dim Rate As New GridViewDecimalColumn
        Rate.FormatString = "{0:n2}"
        Rate.HeaderText = "Rate"
        Rate.Name = colRate
        Rate.Width = 75
        Rate.ReadOnly = True
        Rate.WrapText = True
        Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Rate)

        Dim Amount As New GridViewDecimalColumn
        Amount.FormatString = "{0:n2}"
        Amount.HeaderText = "Amount"
        Amount.Name = colAmount
        Amount.Width = 75
        Amount.ReadOnly = True
        Amount.WrapText = True
        Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Amount)

        '---------------------
        'Dim FatWeightage As New GridViewDecimalColumn
        'FatWeightage.FormatString = "{0:n2}"
        'FatWeightage.HeaderText = "Fat Weightage"
        'FatWeightage.Name = colFatWeightage
        'FatWeightage.Width = 75
        'FatWeightage.ReadOnly = True
        'FatWeightage.WrapText = True
        'FatWeightage.IsVisible = False
        'FatWeightage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(FatWeightage)

        'Dim SnfWeightage As New GridViewDecimalColumn
        'SnfWeightage.FormatString = "{0:n2}"
        'SnfWeightage.HeaderText = "Snf Weightage"
        'SnfWeightage.Name = colSnfWeightage
        'SnfWeightage.Width = 75
        'SnfWeightage.ReadOnly = True
        'SnfWeightage.WrapText = True
        'SnfWeightage.IsVisible = False
        'SnfWeightage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(SnfWeightage)

        'Dim FatRatio As New GridViewDecimalColumn
        'FatRatio.FormatString = "{0:n2}"
        'FatRatio.HeaderText = "Fat Ratio"
        'FatRatio.Name = colFatRatio
        'FatRatio.Width = 75
        'FatRatio.ReadOnly = True
        'FatRatio.WrapText = True
        'FatRatio.IsVisible = False
        'FatRatio.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(FatRatio)

        'Dim SnfRatio As New GridViewDecimalColumn
        'SnfRatio.FormatString = "{0:n2}"
        'SnfRatio.HeaderText = "Snf Ratio"
        'SnfRatio.Name = colSNFRatio
        'SnfRatio.Width = 75
        'SnfRatio.ReadOnly = True
        'SnfRatio.WrapText = True
        'SnfRatio.IsVisible = False
        'SnfRatio.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(SnfRatio)

        'Dim Standardrate As New GridViewDecimalColumn
        'Standardrate.FormatString = "{0:n2}"
        'Standardrate.HeaderText = "Standard rate"
        'Standardrate.Name = colStandardRate
        'Standardrate.Width = 75
        'Standardrate.ReadOnly = False
        'Standardrate.WrapText = True
        'Standardrate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(Standardrate)

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
        itemCode = Nothing
        itemDesc = Nothing
        strUnitCode = Nothing
        fatper = Nothing
        snfper = Nothing

    End Sub
    Sub Reset()
        txtDocNo.Value = ""
        fndLocationCode.Value = ""
        lblLocationName.Text = ""
        FndPriceCode.Value = ""
        fndCustomerNo.Value = ""
        lblCustomerName.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        lblTotRAmt1.Text = ""
        TxtTankerNo.Text = ""
        txtewaybilldate.Value = clsCommon.GETSERVERDATE()
        txtewaybilldate.Checked = False
        TxtEWayBillNo.Text = ""
        ''richa agarwal 18/12/2014
        TxtFatWeightage.Value = 0
        TxtSNFWeightage.Value = 0
        txtfatRatio.Value = 0
        txtSNFRatio.Value = 0
        txtStanadardrate.Value = 0
        TxtToleranceinplus.Value = 0
        txtToleranceinminus.Value = 0
        ''------------------------
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
        BtnCreateSrn.Enabled = False
        chkCreateAoutoInvoice.Checked = False
        UcAttachment1.BlankAllControls()
        loadBlankItemGrid()
        ReStoreGridLayout()
        isNewEntry = True

        gv1.Rows(0).Cells(colItemCode).Value = clsFixedParameter.GetData(clsFixedParameterType.BulkSaleDefaultMilkItem, clsFixedParameterCode.BulkSaleDefaultMilkItem, Nothing)
        If clsCommon.myLen(gv1.Rows(0).Cells(colItemCode).Value) > 0 Then
            gv1.Rows(0).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_Master where Item_code='" & clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value) & "'")
            gv1.Rows(0).Cells(colUnitCode).Value = clsDBFuncationality.getSingleValue("select Unit_Code from tspl_item_Master where Item_code='" & clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value) & "'")
            gv1.Rows(0).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value), Nothing)
        End If
        LOCATIONRIGTHS()
    End Sub

    Private Sub FndPriceCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndPriceCode._MYValidating
        Dim dt As DataTable = Nothing
        Dim whrcls As String = String.Empty
        whrcls = " Convert(date,TSPL_BulkSalePrice_MASTER.Price_Date ,103)<=CONVERT(date,'" & txtDate.Value & "',103) AND (ISNULL(Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill ,103),'')='' OR Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill,103)>=CONVERT(date,'" & txtDate.Value & "',103))"

        ''richa agarwal 12 Sep, 2016
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code= '" & clsFixedParameterCode.showPostrequiredforBulkSale & "' and Type ='" & clsFixedParameterType.showPostrequiredforBulkSale & "'")), "1") = CompairStringResult.Equal Then
            whrcls += " and TSPL_BulkSalePrice_MASTER.Posted='1' "
        End If

        FndPriceCode.Value = ClsBulkSalePriceChart.getFinder(whrcls, FndPriceCode.Value, isButtonClicked)

        If clsCommon.myLen(FndPriceCode.Value) > 0 Then
            dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Ratio,Snf_Ratio,Standard_Rate,TolerancePerPlus,TolerancePerMinus from TSPL_BulkSalePrice_MASTER where Price_Code='" + FndPriceCode.Value + "'")
            If dt.Rows.Count > 0 Then
                'gv1.Rows(0).Cells(colFatWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                'gv1.Rows(0).Cells(colSnfWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                'gv1.Rows(0).Cells(colFatRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                'gv1.Rows(0).Cells(colSNFRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
                'gv1.Rows(0).Cells(colStandardRate).Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                ''richa agarwal 18/12/2014
                TxtFatWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                TxtSNFWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                txtfatRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                txtSNFRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
                txtStanadardrate.Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                TxtToleranceinplus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
                txtToleranceinminus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
                ''------------------------
                UpdateCurrentRow(0)
            End If
        Else
            gv1.DataSource = Nothing
        End If
        dt = Nothing
        whrcls = Nothing
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    ''richa agarwal 18/11/2014
                    If e.Column Is gv1.Columns(colFatPer) Then
                        gv1.Rows(e.RowIndex).Cells(colFatPer).Value = Math.Truncate(Math.Round(clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFatPer).Value) * 100, 2)) / 100
                        Dim fracValue1 As Double = 0
                        If clsCommon.myLen(colFatPer) > 0 Then
                            fracValue1 = clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colFatPer).Value)
                            fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
                            If CInt(fracValue1) Mod 5 <> 0 Then
                                gv1.Rows(e.RowIndex).Cells(colFatPer).Value = 0
                                gv1.CurrentRow = gv1.Rows(e.RowIndex)
                                gv1.CurrentColumn = gv1.Columns(colFatPer)
                                Throw New Exception("FAT% value in Grid, must have its decimal part multiple of 5")
                            End If
                        End If
                    End If
                    If e.Column Is gv1.Columns(colSNFPer) Then
                        gv1.Rows(e.RowIndex).Cells(colSNFPer).Value = Math.Truncate(clsCommon.myCdbl(gv1.Rows(e.RowIndex).Cells(colSNFPer).Value) * 100) / 100
                    End If
                    ''------------------------

                    ' If e.Column Is gv1.Columns(colFatPer) Or e.Column Is gv1.Columns(colSNFPer) Or e.Column Is gv1.Columns(colStandardRate) Or e.Column Is gv1.Columns(colQty) Then
                    If e.Column Is gv1.Columns(colFatPer) Or e.Column Is gv1.Columns(colSNFPer) Or e.Column Is gv1.Columns(colQty) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                        'ElseIf e.Column Is gv1.Columns(colUnitCode) Then
                        '    OpenUOMList(False)
                        'ElseIf e.Column Is gv1.Columns(colItemCode) Then
                        '    OpenItemList(False)
                    End If
                End If
                UpdateAllTotals()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try

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

            'StandardRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colStandardRate).Value)
            'FatWeightage = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatWeightage).Value)
            'SNFWeightage = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSnfWeightage).Value)
            StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)
            FatWeightage = clsCommon.myCdbl(TxtFatWeightage.Value)
            SNFWeightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
            FatPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatPer).Value)
            SNFPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFPer).Value)
            'FatRatio = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatRatio).Value)
            'SNFRatio = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFRatio).Value)
            FatRatio = clsCommon.myCdbl(txtfatRatio.Value)
            SNFRatio = clsCommon.myCdbl(txtSNFRatio.Value)
            'Qty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            Qty = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), 3)

            FatRate = Math.Round(StandardRate * FatWeightage / FatRatio, 2)
            SNFRate = Math.Round(StandardRate * SNFWeightage / SNFRatio, 2)

            'FatRate = Math.Floor(StandardRate * FatWeightage / FatRatio * 100) / 100
            'SNFRate = Math.Floor(StandardRate * SNFWeightage / SNFRatio * 100) / 100

            'FatKG = (Qty * FatPer) / 100
            'SNFKG = (Qty * SNFPer) / 100
            FatKG = Math.Round(((Qty * FatPer) / 100), 3)
            SNFKG = Math.Round(((Qty * SNFPer) / 100), 3)


            FatAmount = Math.Round(FatRate * FatKG, 2)
            SNFAmount = Math.Round(SNFRate * SNFKG, 2)
            'FatAmount = Math.Floor(FatRate * FatKG * 100) / 100
            'SNFAmount = Math.Floor(SNFRate * SNFKG * 100) / 100

            Amount = FatAmount + SNFAmount


            gv1.Rows(IntRowNo).Cells(colFatRate).Value = clsCommon.myCdbl(FatRate)
            gv1.Rows(IntRowNo).Cells(colSNFRate).Value = clsCommon.myCdbl(SNFRate)
            gv1.Rows(IntRowNo).Cells(colFatKG).Value = clsCommon.myCdbl(FatKG)
            gv1.Rows(IntRowNo).Cells(colSNFKG).Value = clsCommon.myCdbl(SNFKG)
            gv1.Rows(IntRowNo).Cells(ColFatAmount).Value = clsCommon.myCdbl(FatAmount)
            gv1.Rows(IntRowNo).Cells(ColSNFAmount).Value = clsCommon.myCdbl(SNFAmount)
            gv1.Rows(IntRowNo).Cells(colAmount).Value = clsCommon.myCdbl(Amount)
            If Qty > 0 Then
                gv1.Rows(IntRowNo).Cells(colRate).Value = Math.Round(clsCommon.myCdbl(Amount / Qty), 2)
                'gv1.Rows(IntRowNo).Cells(colRate).Value = Math.Floor(clsCommon.myCdbl(Amount / Qty) * 100) / 100
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenItemList(ByVal isButtonClick As Boolean)
        Dim qry As String = "Select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER"
        gv1.CurrentRow.Cells(colItemCode).Value = clsCommon.ShowSelectForm("DispatchItemFinder", qry, "Code", "Product_Type ='MI' and Active=1", clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_desc from TSPL_ITEM_Master where Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) + "'")
        gv1.CurrentRow.Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), Nothing)
        qry = Nothing
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnitCode).Value = clsCommon.ShowSelectForm("DispatchUnitFinder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitCode).Value), "Code", isButtonClick)
            qry = Nothing
            whrCls = Nothing
        End If
        strICode = Nothing

    End Sub
    Sub PostData()
        Try
            isFlag = True
            If (myMessages.postConfirm()) Then
               
                SaveData()
                If (ClsDispatchBulkSaleTrade.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully posted")
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
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
    Private Sub UpdateAllTotals()
        Try
            Dim dblTotAmt As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If (clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0) Then
                    dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
                End If
            Next

            lblTotRAmt1.Text = clsCommon.myFormat(dblTotAmt)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        ' Dim desc As String = ""

        ' KUNAL > TICKET : BM00000009609 > Modified Date : 22-09-2016
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Focus()
            txtDate.Select()
            Return False
        End If

        If clsCommon.myLen(fndCustomerNo.Value) <= 0 Then
            fndCustomerNo.Focus()
            Throw New Exception("Customer cannot be left blank")
        End If
        If clsCommon.myLen(fndLocationCode.Value) <= 0 Then
            fndLocationCode.Focus()
            Throw New Exception("Dispatch Location cannot be left blank")
        End If
        If clsCommon.myLen(FndPriceCode.Value) <= 0 Then
            FndPriceCode.Focus()
            Throw New Exception("Price Code cannot be left blank")
        End If
        If clsCommon.myLen(TxtTankerNo.Text) <= 0 Then
            TxtTankerNo.Focus()
            Throw New Exception("Tanker No cannot be left blank")
        End If
        Dim dt As DataTable = Nothing
        dt = clsDBFuncationality.GetDataTable("select TSPL_BulkSalePrice_MASTER.Price_Code as Code from  TSPL_BulkSalePrice_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BulkSalePrice_MASTER .Location_Code where TSPL_BulkSalePrice_MASTER.Price_Code='" & FndPriceCode.Value & "' and Convert(date,TSPL_BulkSalePrice_MASTER.Price_Date ,103)<=CONVERT(date,'" & txtDate.Value & "',103) AND (ISNULL(Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill ,103),'')='' OR Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill,103)>=CONVERT(date,'" & txtDate.Value & "',103))")
        If dt.Rows.Count <= 0 Then
            Throw New Exception("Please check Price Code Date")
        End If
        ''richa 12/12/2014 
        If clsCommon.myCdbl(txtStanadardrate.Value) = 0 Then
            Throw New Exception("Standard Rate cannot be zero")
        End If
        If clsCommon.myCdbl(txtStanadardrate.Value) < 0 Then
            Throw New Exception("Standard Rate cannot be negative")
        End If
        If clsCommon.myLen(txtStanadardrate.Value) > 0 Then
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("Select round((Standard_Rate+((Standard_Rate *TolerancePerPlus )/100)),2) as StandardRatePlus,round((Standard_Rate-((Standard_Rate *TolerancePerMinus  )/100)),2) as StandardRateMinus  from TSPL_BulkSalePrice_MASTER where Price_Code ='" + FndPriceCode.Value + "'")
            If dt1.Rows.Count > 0 Then
                If clsCommon.myCdbl(txtStanadardrate.Value) >= clsCommon.myCdbl(dt1.Rows(0)("StandardRateMinus")) And clsCommon.myCdbl(txtStanadardrate.Value) <= clsCommon.myCdbl(dt1.Rows(0)("StandardRatePlus")) Then
                Else
                    txtStanadardrate.Focus()
                    Throw New Exception("Standard rate should be in range according to price chart")
                End If
            End If
        End If
        ''==============================
        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(i).Cells(colItemCode).Value) <= 0 Then
                Throw New Exception("Item Code cannot be left blank or zero")
            End If
            If clsCommon.myLen(gv1.Rows(i).Cells(colUnitCode).Value) <= 0 Then
                Throw New Exception("UOM cannot be left blank or zero")
            End If
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colUnitCode).Value), "KG") <> CompairStringResult.Equal Then
                Throw New Exception("UOM should be in KG only")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) < 0 Then
                Throw New Exception("Qty cannot be negative")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) = 0 Then
                Throw New Exception("Qty cannot be left blank or zero")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colFatPer).Value) < 0 Then
                Throw New Exception("Fat% cannot be negative")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colFatPer).Value) = 0 Then
                Throw New Exception("Fat% cannot be left blank or zero")
            End If
            If AllowSNFNotManditoryInBulkSale = False Then
                If clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFPer).Value) < 0 Then
                    Throw New Exception("SNF% cannot be negative")
                End If
                If clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFPer).Value) = 0 Then
                    Throw New Exception("SNF% cannot be left blank or zero")
                End If
            End If
            ''richa agarwal 28/02/2016 apply tolerance limit and check stock qty BM00000007217
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select count(Document_No ) from TSPL_Dispatch_BulkSale_Trade where Document_No ='" & txtDocNo.Value & "' and ISNULL(Against_SRN_No,'' )<>''")), "1") = CompairStringResult.Equal Then
                Dim balqty As Double = 0
                Dim dispatchqty As Double = clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)

                balqty = ClsLoadingTanker.getBalance(clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value), fndLocationCode.Value, "", txtDocNo.Value, txtDate.Value, Nothing, "KG")


                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                    If balqty > 0 Then
                        balqty = ClsLoadingTanker.GetTolerane(balqty, dispatchqty)
                    End If
                End If

                If balqty < dispatchqty Then
                    ' Throw New Exception("You cannot save this entry because stock quantity is less than dispatch quantity")
                    Throw New Exception("Available Stock " & balqty & Environment.NewLine & " Dispatch Qty " & dispatchqty)
                End If
            End If
            ' ''richa 12/12/2014 
            'If clsCommon.myLen(gv1.Rows(i).Cells(colStandardRate).Value) = 0 Then
            '    Throw New Exception("Standard Rate cannot be zero")
            'End If
            'If clsCommon.myLen(gv1.Rows(i).Cells(colStandardRate).Value) < 0 Then
            '    Throw New Exception("Standard Rate cannot be negative")
            'End If
            'If clsCommon.myLen(gv1.Rows(i).Cells(colStandardRate).Value) > 0 Then
            '    Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("Select round((Standard_Rate+((Standard_Rate *TolerancePerPlus )/100)),2) as StandardRatePlus,round((Standard_Rate-((Standard_Rate *TolerancePerMinus  )/100)),2) as StandardRateMinus  from TSPL_BulkSalePrice_MASTER where Price_Code ='" + FndPriceCode.Value + "'")
            '    If dt1.Rows.Count > 0 Then
            '        If clsCommon.myCdbl(gv1.Rows(i).Cells(colStandardRate).Value) >= clsCommon.myCdbl(dt1.Rows(0)("StandardRateMinus")) And clsCommon.myCdbl(gv1.Rows(i).Cells(colStandardRate).Value) <= clsCommon.myCdbl(dt1.Rows(0)("StandardRatePlus")) Then
            '        Else
            '            Throw New Exception("Standard rate should be in range according to price chart")
            '        End If
            '    End If
            'End If
            ' ''==============================
        Next
       
        Return True
    End Function

    Sub SaveData()
        Dim BulkMilkSRNNo As String = String.Empty
        Dim obj As New ClsDispatchBulkSaleTrade()
        Dim objTr As New clsDispatchDetailTradeBulkSale
        Try
            If AllowToSave() Then
                If Not isFlag Then

                    BulkMilkSRNNo = clsDBFuncationality.getSingleValue("SELECT SRN_NO  FROM TSPL_Bulk_MILK_SRN WHERE Challan_No='" & txtDocNo.Value & "' AND isPosted=1 and  TSPL_Bulk_MILK_SRN.FormType='Bulk Milk SRN Trade'")
                    If clsCommon.myLen(BulkMilkSRNNo) > 0 Then
                        Throw New Exception("You cannot update this dispatch because its SRN will be created")
                    End If
                End If

                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Location_Code = fndLocationCode.Value
                obj.Customer_Code = fndCustomerNo.Value
                obj.Price_Code = FndPriceCode.Value
                obj.Total_Amt = lblTotRAmt1.Text
                obj.Tanker_No = TxtTankerNo.Text
                If chkCreateAoutoInvoice.Checked = True Then
                    obj.Is_Create_Auto_Invoice = 1
                Else
                    obj.Is_Create_Auto_Invoice = 0
                End If
                obj.arrDispatchDetailTradeBulkSale = New List(Of clsDispatchDetailTradeBulkSale)




                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New clsDispatchDetailTradeBulkSale()
                    objTr.Document_No = clsCommon.myCstr(obj.Document_No)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)

                    objTr.FatPer = clsCommon.myCdbl(grow.Cells(colFatPer).Value)
                    objTr.SNFPer = clsCommon.myCdbl(grow.Cells(colSNFPer).Value)
                    objTr.Fat_KG = clsCommon.myCdbl(grow.Cells(colFatKG).Value)
                    objTr.SNF_KG = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                    objTr.FatAmount = clsCommon.myCdbl(grow.Cells(ColFatAmount).Value)

                    objTr.SNFAmount = clsCommon.myCdbl(grow.Cells(ColSNFAmount).Value)
                    objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                    objTr.FatRate = clsCommon.myCdbl(grow.Cells(colFatRate).Value)
                    objTr.SNFRate = clsCommon.myCdbl(grow.Cells(colSNFRate).Value)
                    'objTr.StandardRate = clsCommon.myCdbl(grow.Cells(colStandardRate).Value)
                    objTr.StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)
                    obj.arrDispatchDetailTradeBulkSale.Add(objTr)
                Next

                If (ClsDispatchBulkSaleTrade.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_No)
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                        LoadData(obj.Document_No, NavigatorType.Current)
                    End If

                End If
                
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
            objTr = Nothing
            BulkMilkSRNNo = Nothing
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsDispatchBulkSaleTrade = Nothing
        Dim dt As DataTable = Nothing
        Try
            obj = ClsDispatchBulkSaleTrade.GetData(strCode, NavTyep)

            isInsideLoadData = True
            If obj IsNot Nothing Then
                isNewEntry = False
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                fndLocationCode.Value = obj.Location_Code
                lblLocationName.Text = obj.Location_Name
                fndCustomerNo.Value = obj.Customer_Code
                lblCustomerName.Text = obj.Customer_Name
                FndPriceCode.Value = obj.Price_Code
                lblTotRAmt1.Text = obj.Total_Amt
                TxtTankerNo.Text = obj.Tanker_No
                If obj.Is_Create_Auto_Invoice = 0 Then
                    chkCreateAoutoInvoice.Checked = False
                Else
                    chkCreateAoutoInvoice.Checked = True
                End If

                TxtEWayBillNo.Text = obj.EWayBillNo
                If obj.EWayBillDate IsNot Nothing Then
                    txtewaybilldate.Value = obj.EWayBillDate
                    txtewaybilldate.Checked = True
                Else
                    txtewaybilldate.Value = clsCommon.GETSERVERDATE()
                    txtewaybilldate.Checked = False
                End If
                If obj.arrDispatchDetailTradeBulkSale IsNot Nothing AndAlso obj.arrDispatchDetailTradeBulkSale.Count > 0 Then
                    For Each objTr As clsDispatchDetailTradeBulkSale In obj.arrDispatchDetailTradeBulkSale
                        ' gv1.Rows.AddNew()
                        gv1.Rows(0).Cells(colSlNo).Value = "1"
                        gv1.Rows(0).Cells(colItemCode).Value = objTr.Item_Code
                        gv1.Rows(0).Cells(colItemDesc).Value = objTr.ItemDesc
                        gv1.Rows(0).Cells(colUnitCode).Value = objTr.Unit_code
                        gv1.Rows(0).Cells(colHSNCode).Value = objTr.HSN_code
                        gv1.Rows(0).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(0).Cells(colFatPer).Value = objTr.FatPer
                        gv1.Rows(0).Cells(colSNFPer).Value = objTr.SNFPer
                        gv1.Rows(0).Cells(colFatKG).Value = objTr.Fat_KG
                        gv1.Rows(0).Cells(colSNFKG).Value = objTr.SNF_KG
                        gv1.Rows(0).Cells(ColFatAmount).Value = objTr.FatAmount
                        gv1.Rows(0).Cells(ColSNFAmount).Value = objTr.SNFAmount
                        gv1.Rows(0).Cells(colRate).Value = objTr.Rate
                        gv1.Rows(0).Cells(colAmount).Value = objTr.Amount
                        gv1.Rows(0).Cells(colFatRate).Value = objTr.FatRate
                        gv1.Rows(0).Cells(colSNFRate).Value = objTr.SNFRate
                        ' gv1.Rows(0).Cells(colStandardRate).Value = objTr.StandardRate
                        txtStanadardrate.Value = objTr.StandardRate
                        If clsCommon.myLen(FndPriceCode.Value) > 0 Then
                            dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Ratio,Snf_Ratio,Standard_Rate,TolerancePerPlus,TolerancePerMinus from TSPL_BulkSalePrice_MASTER where Price_Code='" + FndPriceCode.Value + "'")
                            If dt.Rows.Count > 0 Then
                                'gv1.Rows(0).Cells(colFatWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                                'gv1.Rows(0).Cells(colSnfWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                                'gv1.Rows(0).Cells(colFatRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                                'gv1.Rows(0).Cells(colSNFRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
                                ' gv1.Rows(0).Cells(colStandardRate).Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                                'UpdateCurrentRow(0)
                                ''richa agarwal 18/12/2014
                                TxtFatWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                                TxtSNFWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                                txtfatRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                                txtSNFRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
                                ' txtStanadardrate.Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                                TxtToleranceinplus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
                                txtToleranceinminus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
                                UpdateCurrentRow(0)
                                ''------------------------
                            End If
                        End If
                    Next
                Else
                    gv1.DataSource = Nothing
                End If
                txtDocNo.MyReadOnly = True
                btnsave.Text = "Update"
                BtnCreateSrn.Enabled = True


                If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    'btnPost.Enabled = True
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                'If clsCommon.myLen(obj.Against_SRN_No) > 0 Then
                '    btnPost.Enabled = True
                'Else
                '    btnPost.Enabled = False
                'End If
                Dim BulkMilkSRNNo As String = ""
                BulkMilkSRNNo = clsDBFuncationality.getSingleValue("SELECT SRN_NO  FROM TSPL_Bulk_MILK_SRN WHERE Challan_No='" & txtDocNo.Value & "' AND isPosted=1 and  TSPL_Bulk_MILK_SRN.FormType='Bulk Milk SRN Trade'")
                If clsCommon.myLen(BulkMilkSRNNo) > 0 And clsCommon.CompairString(obj.Posted, "0") = CompairStringResult.Equal Then
                    btnPost.Enabled = True
                Else
                    btnPost.Enabled = False
                End If
                UcAttachment1.LoadData(obj.Document_No)
            Else
                Reset()

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            obj = Nothing
            dt = Nothing
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_Dispatch_BulkSale_Trade where Document_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "Select TSPL_Dispatch_BulkSale_Trade.Document_No as Code,Convert(varchar,TSPL_Dispatch_BulkSale_Trade.Document_Date,103) as [Date],TSPL_Dispatch_BulkSale_Trade.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_Dispatch_BulkSale_Trade.Location_Code as [Dispatch Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Dispatch Location Name],TSPL_Dispatch_BulkSale_Trade.Price_Code as [Price Chart Code],TSPL_Dispatch_BulkSale_Trade.Tanker_No as [Tanker No],TSPL_Dispatch_BulkSale_Trade.Against_SRN_No as [Against SRN No],case when TSPL_Dispatch_BulkSale_Trade.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_Dispatch_BulkSale_Trade Left Outer Join TSPL_LOCATION_MASTER on TSPL_Dispatch_BulkSale_Trade.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Dispatch_BulkSale_Trade.Customer_Code"
        Dim whrcls As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrcls = " TSPL_Dispatch_BulkSale_Trade.Location_Code in (" & arrLoc & ")"
        End If
        txtDocNo.Value = clsCommon.ShowSelectForm("DispatchTradeBulkSale", qry, "Code", whrcls, txtDocNo.Value, "", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub
    Private Sub DeleteData()
        Dim BulkMilkSRNNo As String = String.Empty
        Try
            If (deleteConfirm()) Then

                BulkMilkSRNNo = clsDBFuncationality.getSingleValue("SELECT SRN_NO  FROM TSPL_Bulk_MILK_SRN WHERE Challan_No='" & txtDocNo.Value & "' and  TSPL_Bulk_MILK_SRN.FormType='Bulk Milk SRN Trade'")
                If clsCommon.myLen(BulkMilkSRNNo) > 0 Then
                    Throw New Exception("You cannot delete this dispatch because it is used in Bulk Milk SRN Trade")
                Else
                    If (ClsDispatchBulkSaleTrade.DeleteData(txtDocNo.Value)) Then
                        common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                        Reset()
                    End If

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            BulkMilkSRNNo = Nothing
        End Try
    End Sub

    Private Sub BtnCreateSrn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCreateSrn.Click
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            isInsideLoadData = True
            Dim frm As New FrmBulkTradeSRN()
            'frm.FatWeightage = clsCommon.myCdbl(gv1.Rows(0).Cells(colFatWeightage).Value)
            'frm.SNFWeightage = clsCommon.myCdbl(gv1.Rows(0).Cells(colSnfWeightage).Value)
            'frm.FatRatio = clsCommon.myCdbl(gv1.Rows(0).Cells(colFatRatio).Value)
            'frm.SNFRatio = clsCommon.myCdbl(gv1.Rows(0).Cells(colSNFRatio).Value)
            frm.FatWeightage = clsCommon.myCdbl(TxtFatWeightage.Value)
            frm.SNFWeightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
            frm.FatRatio = clsCommon.myCdbl(txtfatRatio.Value)
            frm.SNFRatio = clsCommon.myCdbl(txtSNFRatio.Value)
            frm.ChallanNo = txtDocNo.Value
            frm.ChallanDate = txtDate.Value
            ' frm.ShowDialog()
            frm.Show()
            LoadData(txtDocNo.Value, NavigatorType.Current)
            isInsideLoadData = False
        Else
            clsCommon.MyMessageBoxShow("Please select challan no first")
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub RdDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RdSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdSaveLayout.Click

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

    Private Sub fndCustomerNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomerNo._MYValidating
        fndCustomerNo.Value = clsCustomerMaster.getFinder("", fndCustomerNo.Value, isButtonClicked)
        If clsCommon.myLen(fndCustomerNo.Value) > 0 Then
            lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + fndCustomerNo.Value + "' ")
        Else
            lblCustomerName.Text = ""
        End If
    End Sub

   

    Private Sub gv1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv1.KeyPress
        Try
            If gv1.CurrentColumn Is gv1.Columns(colFatPer) Or gv1.CurrentColumn Is gv1.Columns(colSNFPer) Then
                If Not IsNumeric(e.KeyChar) And Not e.KeyChar = "." Then
                    e.Handled = True
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtStanadardrate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStanadardrate.TextChanged
        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
    End Sub
    ' Ticket No : TEC/29/10/18-000350 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub
End Class
