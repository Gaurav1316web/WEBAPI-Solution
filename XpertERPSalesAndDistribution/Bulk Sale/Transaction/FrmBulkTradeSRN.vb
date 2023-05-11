
'--------Created By Richa 17/09/2014 Against Ticket No BM00000003892,BM00000005027,BM00000004864

Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class FrmBulkTradeSRN
    Inherits FrmMainTranScreen
#Region "Variables"
    'Public PriceCode As String = Nothing
    'Public Locationcode As String = Nothing
    'Public LocationName As String = Nothing
    Public FatWeightage As Double = 0
    Public SNFWeightage As Double = 0
    Public FatRatio As Double = 0
    Public SNFRatio As Double = 0
    Private IsFormLoad As Boolean = True
    Public ChallanNo As String = Nothing
    Public ChallanDate As DateTime = Nothing
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
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
    Public strDocumentCode As String = ""
#End Region

    Private Sub FrmBulkTradeSRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub FrmBulkTradeSRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        IsFormLoad = True
        Reset()
        LoadGridData()
        UcAttachment1.Form_ID = MyBase.Form_ID
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        IsFormLoad = False
        If clsCommon.myLen(strDocumentCode) > 0 Then
            LoadData(strDocumentCode, NavigatorType.Current)
        End If

    End Sub
    Sub Reset()
        txtDocNo.Value = ""
        fndLocationCode.Value = ""
        lblLocationName.Text = ""
        FndPriceCode.Value = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        lblTotRAmt1.Text = ""
        FndVendor.Value = ""
        lblVendor.Text = ""
        ''richa agarwal 18/12/2014
        TxtFatWeightage.Value = 0
        TxtSNFWeightage.Value = 0
        txtfatRatio.Value = 0
        txtSNFRatio.Value = 0
        txtStanadardrate.Value = 0
        TxtTolerance.Value = 0
        ''------------------------
        txtTanker.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtDocNo.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        UcAttachment1.BlankAllControls()
        loadBlankItemGrid()
        ReStoreGridLayout()
        isNewEntry = True
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
        Qty.ReadOnly = True
        Qty.WrapText = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Qty)


        Dim fatper As New GridViewDecimalColumn
        fatper.FormatString = "{0:n2}"
        fatper.HeaderText = "FAT %"
        fatper.Name = colFatPer
        fatper.Width = 75
        fatper.ReadOnly = True
        fatper.WrapText = True
        fatper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(fatper)



        Dim snfper As New GridViewDecimalColumn
        snfper.FormatString = "{0:n2}"
        snfper.HeaderText = "SNF %"
        snfper.Name = colSNFPer
        snfper.Width = 75
        snfper.ReadOnly = True
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

    Private Sub FndPriceCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndPriceCode._MYValidating
        Dim dt As DataTable = Nothing
        Dim whrcls As String = " Fat_Weightage = " & FatWeightage & " and Snf_Weightage=" & SNFWeightage & " and Fat_Percentage=" & FatRatio & " and Snf_Percentage=" & SNFRatio & ""
        'FndPriceCode.Value = clsPriceChartBulkProc.getFinder(whrcls, FndPriceCode.Value, isButtonClicked)
        FndPriceCode.Value = clsPriceChartBulkProc.getFinder(" Convert(date,TSPL_Bulk_Price_MASTER.Price_Date ,103)<=CONVERT(date,'" & txtDate.Value & "',103)", FndPriceCode.Value, isButtonClicked)

        'If clsCommon.myLen(FndPriceCode.Value) > 0 Then
        dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Percentage,Snf_Percentage,Standard_Rate,Tolerance from TSPL_Bulk_Price_MASTER where Price_Code='" + FndPriceCode.Value + "'")
        If dt.Rows.Count > 0 Then
            'gv1.Rows(0).Cells(colFatWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
            'gv1.Rows(0).Cells(colSnfWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
            'gv1.Rows(0).Cells(colFatRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
            'gv1.Rows(0).Cells(colSNFRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
            'gv1.Rows(0).Cells(colStandardRate).Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
            'UpdateCurrentRow(0)
            ''richa agarwal 18/12/2014
            TxtFatWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
            TxtSNFWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
            txtfatRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
            txtSNFRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
            txtStanadardrate.Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
            TxtTolerance.Value = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
            UpdateCurrentRow(0)
            ''------------------------
            ''richa agarwal 16/10/2014
            lblTotRAmt1.Text = clsCommon.myCstr(gv1.Rows(0).Cells(colAmount).Value)
        Else
            'gv1.Rows(0).Cells(colFatWeightage).Value = 0
            'gv1.Rows(0).Cells(colSnfWeightage).Value = 0
            'gv1.Rows(0).Cells(colFatRatio).Value = 0
            'gv1.Rows(0).Cells(colSNFRatio).Value = 0
            'gv1.Rows(0).Cells(colStandardRate).Value = 0
            TxtFatWeightage.Value = 0
            TxtSNFWeightage.Value = 0
            txtfatRatio.Value = 0
            txtSNFRatio.Value = 0
            txtStanadardrate.Value = 0
            TxtTolerance.Value = 0
            UpdateCurrentRow(0)
            lblTotRAmt1.Text = 0
        End If
        'Else
        'gv1.DataSource = Nothing
        'End If
        dt = Nothing
        whrcls = Nothing
    End Sub
    Private Sub LoadGridData()
        Dim qry As String = "select SRN_NO from TSPL_Bulk_MILK_SRN where  Challan_No='" + ChallanNo + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "' and FormType='Bulk Milk SRN Trade'"
        Dim SRNNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        If clsCommon.myLen(SRNNo) > 0 Then
            LoadData(SRNNo, NavigatorType.Current)
        Else
            Dim obj As ClsDispatchBulkSaleTrade = ClsDispatchBulkSaleTrade.GetData(ChallanNo, NavigatorType.Current)
            '  Dim dt As DataTable
            isInsideLoadData = True
            If obj IsNot Nothing Then
                fndLocationCode.Value = obj.Location_Code
                lblLocationName.Text = obj.Location_Name

                ''richa agarwal 08/03/2016
                txtTanker.Text = obj.Tanker_No
                ''-------------------
                ' lblTotRAmt1.Text = obj.Total_Amt

                If obj.arrDispatchDetailTradeBulkSale IsNot Nothing AndAlso obj.arrDispatchDetailTradeBulkSale.Count > 0 Then
                    For Each objTr As clsDispatchDetailTradeBulkSale In obj.arrDispatchDetailTradeBulkSale
                        gv1.Rows(0).Cells(colSlNo).Value = "1"
                        gv1.Rows(0).Cells(colItemCode).Value = objTr.Item_Code
                        gv1.Rows(0).Cells(colItemDesc).Value = objTr.ItemDesc
                        gv1.Rows(0).Cells(colUnitCode).Value = objTr.Unit_code
                        gv1.Rows(0).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
                        gv1.Rows(0).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(0).Cells(colFatPer).Value = objTr.FatPer
                        gv1.Rows(0).Cells(colSNFPer).Value = objTr.SNFPer
                        'gv1.Rows(0).Cells(colFatKG).Value = objTr.Fat_KG
                        'gv1.Rows(0).Cells(colSNFKG).Value = objTr.SNF_KG
                        'gv1.Rows(0).Cells(ColFatAmount).Value = objTr.FatAmount
                        'gv1.Rows(0).Cells(ColSNFAmount).Value = objTr.SNFAmount
                        'gv1.Rows(0).Cells(colRate).Value = objTr.Rate
                        'gv1.Rows(0).Cells(colAmount).Value = objTr.Amount
                        'gv1.Rows(0).Cells(colFatRate).Value = objTr.FatRate
                        'gv1.Rows(0).Cells(colSNFRate).Value = objTr.SNFRate
                        'If clsCommon.myLen(FndPriceCode.Value) > 0 Then
                        '    dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Ratio,Snf_Ratio,Standard_Rate from TSPL_BulkSalePrice_MASTER where Price_Code='" + FndPriceCode.Value + "'")
                        '    If dt.Rows.Count > 0 Then
                        '        gv1.Rows(0).Cells(colFatWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                        '        gv1.Rows(0).Cells(colSnfWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                        '        gv1.Rows(0).Cells(colFatRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                        '        gv1.Rows(0).Cells(colSNFRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
                        '        gv1.Rows(0).Cells(colStandardRate).Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                        '        UpdateCurrentRow(0)
                        '    End If
                        'End If
                    Next
                Else
                    gv1.DataSource = Nothing
                End If
                obj = Nothing
            Else
                Reset()

            End If
        End If
        qry = Nothing
        SRNNo = Nothing
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

            'FatKG = (Qty * FatPer) / 100
            'SNFKG = (Qty * SNFPer) / 100
            FatKG = Math.Round(((Qty * FatPer) / 100), 3)
            SNFKG = Math.Round(((Qty * SNFPer) / 100), 3)



            FatAmount = Math.Round(FatRate * FatKG, 2)
            SNFAmount = Math.Round(SNFRate * SNFKG, 2)

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
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FndVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndVendor._MYValidating
        FndVendor.Value = clsVendorMaster.getFinder("", FndVendor.Value, isButtonClicked)
        lblVendor.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + FndVendor.Value + "'")
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        Dim desc As String = String.Empty

        If clsCommon.myLen(FndVendor.Value) <= 0 Then
            FndVendor.Focus()
            Throw New Exception("Vendor cannot be left blank")
        End If
        If clsCommon.myLen(FndPriceCode.Value) <= 0 Then
            FndPriceCode.Focus()
            Throw New Exception("Price Code cannot be left blank")
        End If
        If clsCommon.myCDate(ChallanDate, "dd/MM/yyyy") <> clsCommon.myCDate(txtDate.Value, "dd/MM/yyyy") Then
            txtDate.Focus()
            Throw New Exception("SRN Date should be same as challan date")
        End If
        Dim dt As DataTable = Nothing
        dt = clsDBFuncationality.GetDataTable("select TSPL_Bulk_Price_MASTER.Price_Code as Code from  TSPL_Bulk_Price_MASTER where TSPL_Bulk_Price_MASTER.Price_Code='" & FndPriceCode.Value & "' and Convert(date,TSPL_Bulk_Price_MASTER.Price_Date ,103)<=CONVERT(date,'" & txtDate.Value & "',103)")
        If dt.Rows.Count <= 0 Then
            Throw New Exception("Please check Price Code Date")
        End If
        If clsCommon.myCdbl(txtStanadardrate.Value) = 0 Then
            Throw New Exception("Standard Rate cannot be zero")
        End If
        If clsCommon.myCdbl(txtStanadardrate.Value) < 0 Then
            Throw New Exception("Standard Rate cannot be negative")
        End If
        If clsCommon.myLen(txtStanadardrate.Value) > 0 Then
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("Select round((Standard_Rate+((Standard_Rate *Tolerance)/100)),2) as StandardRatePlus,round((Standard_Rate-((Standard_Rate *Tolerance)/100)),2) as StandardRateMinus  from TSPL_Bulk_Price_MASTER where Price_Code ='" + FndPriceCode.Value + "'")
            If dt1.Rows.Count > 0 Then
                If clsCommon.myCdbl(txtStanadardrate.Value) >= clsCommon.myCdbl(dt1.Rows(0)("StandardRateMinus")) And clsCommon.myCdbl(txtStanadardrate.Value) <= clsCommon.myCdbl(dt1.Rows(0)("StandardRatePlus")) Then
                Else
                    txtStanadardrate.Focus()
                    Throw New Exception("Standard rate should be in range according to price chart")
                End If
            End If
        End If
        ''richa 12/12/2014 
        'For i As Integer = 0 To gv1.Rows.Count - 1

        '    If clsCommon.myLen(gv1.Rows(i).Cells(colStandardRate).Value) = 0 Then
        '        Throw New Exception("Standard Rate cannot be zero")
        '    End If
        '    If clsCommon.myLen(gv1.Rows(i).Cells(colStandardRate).Value) < 0 Then
        '        Throw New Exception("Standard Rate cannot be negative")
        '    End If
        '    If clsCommon.myLen(gv1.Rows(i).Cells(colStandardRate).Value) > 0 Then
        '        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("Select round((Standard_Rate+((Standard_Rate *Tolerance)/100)),2) as StandardRatePlus,round((Standard_Rate-((Standard_Rate *Tolerance)/100)),2) as StandardRateMinus  from TSPL_Bulk_Price_MASTER where Price_Code ='" + FndPriceCode.Value + "'")
        '        If dt1.Rows.Count > 0 Then
        '            If clsCommon.myCdbl(gv1.Rows(i).Cells(colStandardRate).Value) >= clsCommon.myCdbl(dt1.Rows(0)("StandardRateMinus")) And clsCommon.myCdbl(gv1.Rows(i).Cells(colStandardRate).Value) <= clsCommon.myCdbl(dt1.Rows(0)("StandardRatePlus")) Then
        '            Else
        '                Throw New Exception("Standard rate should be in range according to price chart")
        '            End If
        '        End If
        '    End If
        '    ''==============================
        'Next
        desc = Nothing
        Return True
    End Function
    Sub SaveData()
        Dim obj As New clsBulkMilkSRNTrade()
        Try
            If AllowToSave() Then

                obj.SRN_NO = clsCommon.myCstr(txtDocNo.Value)
                obj.SRN_Date = txtDate.Value
                obj.Vendor_Code = FndVendor.Value
                obj.Loc_Code = clsLocation.GetSegmentCode(fndLocationCode.Value, Nothing)
                obj.sub_location = fndLocationCode.Value
                obj.Challan_No = ChallanNo
                obj.Challan_Date = ChallanDate
                obj.Price_Code = FndPriceCode.Value
                obj.Item_Code = gv1.Rows(0).Cells(colItemCode).Value
                obj.Item_Desc = gv1.Rows(0).Cells(colItemDesc).Value
                obj.UOM = gv1.Rows(0).Cells(colUnitCode).Value
                obj.Net_Weight = gv1.Rows(0).Cells(colQty).Value
                obj.snf_Per = gv1.Rows(0).Cells(colSNFPer).Value
                obj.fat_per = gv1.Rows(0).Cells(colFatPer).Value
                obj.fat_KG = gv1.Rows(0).Cells(colFatKG).Value
                obj.SNF_KG = gv1.Rows(0).Cells(colSNFKG).Value
                obj.fat_Rate = gv1.Rows(0).Cells(colFatRate).Value
                obj.SNF_Rate = gv1.Rows(0).Cells(colSNFRate).Value
                obj.Amount = gv1.Rows(0).Cells(colAmount).Value
                obj.Actual_Amount = gv1.Rows(0).Cells(colAmount).Value
                ''RICHA AGARWAL 06/07/2015
                obj.fat_amount = gv1.Rows(0).Cells(ColFatAmount).Value
                obj.SNF_Amount = gv1.Rows(0).Cells(ColSNFAmount).Value
                ''-----------------
                ''richa agarwal 08/03/2016
                obj.Tanker_No = txtTanker.Text
                ''----------
                'obj.StandardRate = gv1.Rows(0).Cells(colStandardRate).Value
                obj.StandardRate = txtStanadardrate.Value
                If (clsBulkMilkSRNTrade.saveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.SRN_NO)
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                        LoadData(obj.SRN_NO, NavigatorType.Current)
                    End If

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (clsBulkMilkSRNTrade.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim dt As DataTable = Nothing
        Dim obj As clsBulkMilkSRNTrade = Nothing
        Try
            obj = clsBulkMilkSRNTrade.getData(strCode, NavTyep)

            isInsideLoadData = True
            If obj IsNot Nothing Then
                isNewEntry = False
                txtDocNo.Value = obj.SRN_NO
                txtDate.Value = obj.SRN_Date
                fndLocationCode.Value = obj.sub_location
                lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'"))
                FndVendor.Value = obj.Vendor_Code
                lblVendor.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + FndVendor.Value + "'")
                FndPriceCode.Value = obj.Price_Code
                lblTotRAmt1.Text = obj.Actual_Amount
                ''richa agarwal 08/03/2016
                txtTanker.Text = obj.Tanker_No
                ''-----------------
                gv1.Rows(0).Cells(colSlNo).Value = "1"
                gv1.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                gv1.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                gv1.Rows(0).Cells(colHSNCode).Value = obj.HSN_code
                gv1.Rows(0).Cells(colUnitCode).Value = obj.UOM
                gv1.Rows(0).Cells(colQty).Value = obj.Net_Weight
                gv1.Rows(0).Cells(colFatPer).Value = obj.fat_per
                gv1.Rows(0).Cells(colSNFPer).Value = obj.snf_Per
                gv1.Rows(0).Cells(colFatKG).Value = obj.fat_KG
                gv1.Rows(0).Cells(colSNFKG).Value = obj.SNF_KG
                gv1.Rows(0).Cells(colAmount).Value = obj.Amount
                gv1.Rows(0).Cells(colFatRate).Value = obj.fat_Rate
                gv1.Rows(0).Cells(colSNFRate).Value = obj.SNF_Rate
                'gv1.Rows(0).Cells(colStandardRate).Value = obj.StandardRate
                txtStanadardrate.Value = obj.StandardRate
                If clsCommon.myLen(FndPriceCode.Value) > 0 Then
                    dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Percentage,Snf_Percentage,Standard_Rate,Tolerance from TSPL_Bulk_Price_MASTER where Price_Code='" + FndPriceCode.Value + "'")
                    If dt.Rows.Count > 0 Then
                        'gv1.Rows(0).Cells(colFatWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                        'gv1.Rows(0).Cells(colSnfWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                        'gv1.Rows(0).Cells(colFatRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                        'gv1.Rows(0).Cells(colSNFRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                        'gv1.Rows(0).Cells(colStandardRate).Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                        'UpdateCurrentRow(0)
                        ''richa agarwal 18/12/2014
                        TxtFatWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                        TxtSNFWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                        txtfatRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                        txtSNFRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                        'txtStanadardrate.Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                        TxtTolerance.Value = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
                        UpdateCurrentRow(0)
                        ''------------------------
                    End If
                End If


                txtDocNo.MyReadOnly = True
                btnsave.Text = "Update"


                If clsCommon.CompairString(obj.isPosted, "1") = CompairStringResult.Equal Then
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

                UcAttachment1.LoadData(obj.SRN_NO)
            Else
                Reset()

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
            dt = Nothing
            obj = Nothing
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_Bulk_MILK_SRN where SRN_NO='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "' and FormType='Bulk Milk SRN Trade'"
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
        Dim qry As String = "Select TSPL_Bulk_MILK_SRN.SRN_NO as Code,CONVERT(varchar,TSPL_Bulk_MILK_SRN.SRN_Date ,103) as Date,TSPL_Bulk_MILK_SRN.Loc_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Bulk_MILK_SRN.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name],TSPL_Bulk_MILK_SRN.Price_Code as [Price Code],TSPL_Bulk_MILK_SRN.Challan_No as [Challan No],CONVERT(varchar,TSPL_Bulk_MILK_SRN.Challan_Date ,103) as [Challan Date],case when TSPL_Bulk_MILK_SRN.isPosted=0 then 'Pending' else 'Approved' end as Status from TSPL_Bulk_MILK_SRN Left Outer Join TSPL_VENDOR_MASTER on TSPL_Bulk_MILK_SRN.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_Bulk_MILK_SRN.Loc_Code  =TSPL_LOCATION_MASTER.Location_Code"
        txtDocNo.Value = clsCommon.ShowSelectForm("SRNBULKTRADE", qry, "Code", " TSPL_Bulk_MILK_SRN.FormType='Bulk Milk SRN Trade' ", txtDocNo.Value, "", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub
    Sub PostData()
        Dim msg As String = String.Empty
        Dim qry As String = String.Empty
        Dim dt As DataTable = Nothing

        Try
            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                If (clsBulkMilkSRNTrade.postData(txtDocNo.Value, MyBase.Form_ID)) Then
                    msg = "Successfully posted"
                    common.clsCommon.MyMessageBoxShow(msg)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
            msg = Nothing
            qry = Nothing
            dt = Nothing
        End Try
    End Sub


    Private Sub RDSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDSaveLayout.Click

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

    Private Sub RDDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub txtStanadardrate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStanadardrate.TextChanged
        If Not IsFormLoad Then
            UpdateCurrentRow(0)
        End If
    End Sub
End Class
