'--------Created By Richa 17/08/2015 Against Ticket No BM00000005377
' Ticket No : ERO/30/01/19-000480 By prabhakar for customer name extend to column size 
Imports System.Data.SqlClient
Imports common
Imports System.IO

Public Class FrmSalesOrderBS_Pavitra
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colUnitCode As String = "colUnitCode"
    Public Const colQty As String = "Qty"
    Public Const colPriceCode As String = "colPriceCode"
    Public Const colAmount As String = "colAmount"
    Public Const colFatPer As String = "colFatPer"
    Public Const colRate As String = "colRate"
    Public Const colSNFPer As String = "colSNFPer"
    Public Const colCLR As String = "colCLR"
    Public Const colStandardMilkRate As String = "colNetMilkRate"
    Public Const colActualMilkRate As String = "colActualMilkRate"
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
    Public Const colToleranceperplus As String = "colToleranceperplus"
    Public Const colToleranceperminus As String = "colToleranceperminus"
    Dim arrLoc As String = Nothing
    Public Shared Alocation As String = Nothing
    Dim isallowforclose As Boolean = False
    Public DocumentNo As String = ""
    Dim Qry As String
    Dim ApplyTSPriceAtBulkSale As Boolean = False
    Dim AllowSNFNotManditoryInBulkSale As Boolean = False
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmSalesOrderBS_Pavitra_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
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
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                fndLocation.Value = obj.Default_LocCode
                LblLocationName.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
                Alocation = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Private Sub FrmSalesOrderBS_Pavitra_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ApplyTSPriceAtBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, Nothing)) = 1, True, False))
        AllowSNFNotManditoryInBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSNFNotManditoryInBulkSale, clsFixedParameterCode.AllowSNFNotManditoryInBulkSale, Nothing)) = 1, True, False))
        TxtPONo.MendatroryField = IIf(ApplyTSPriceAtBulkSale = True, False, True)
        GroupBox1.Visible = IIf(ApplyTSPriceAtBulkSale = True, False, True)
        Reset()
        LOCATIONRIGTHS()
        UcAttachment1.Form_ID = MyBase.Form_ID
        'ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        'ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        'ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N New Transaction")
        'If clsCommon.myLen(DocumentNo) > 0 Then
        '    LoadData(DocumentNo, NavigatorType.Current)
        'End If
        'If clsCommon.myLen(Me.Tag) > 0 Then
        '    LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        'End If
    End Sub
    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.FrmSalesOrderBS)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Sub Reset()
        chkSOclose.Enabled = False
        chkSOclose.Checked = False
        isallowforclose = False
        fndSONo.Value = ""
        FndCustomerNo.Value = ""
        lblCustomerName.Text = ""
        fndLocation.Value = ""
        LblLocationName.Text = ""
        fndPaymenttermsNo.Value = ""
        TxtPONo.Text = ""
        FndPriceCode.Value = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        TxtFatWeightage.Value = 0
        TxtSNFWeightage.Value = 0
        txtfatRatio.Value = 0
        txtSNFRatio.Value = 0
        txtStanadardrate.Value = 0
        TxtToleranceinplus.Value = 0
        txtToleranceinminus.Value = 0
        lblTotRAmt1.Text = ""
        txtSOdate.Value = clsCommon.GETSERVERDATE()
        txtPODate.Value = clsCommon.GETSERVERDATE()
        fndSONo.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnPost.Enabled = True
        btnsave.Enabled = True
        UcAttachment1.BlankAllControls()
        loadBlankItemGrid()
        ReStoreGridLayout()
        isNewEntry = True
    End Sub
    Private Sub FndCustomerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndCustomerNo._MYValidating
        FndCustomerNo.Value = clsCustomerMaster.getFinder("isnull(Status,'N')='N'", FndCustomerNo.Value, isButtonClicked)
        lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + FndCustomerNo.Value + "' ")
        fndPaymenttermsNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Terms_Code from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + FndCustomerNo.Value + "' "))
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Dim strwhrcls As String = String.Empty
        strwhrcls = "Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N'"
        If clsCommon.CompairString(objCommonVar.CurrentUser, "ADMIN") <> CompairStringResult.Equal Then
            If clsCommon.CompairString(objCommonVar.strCurrUserLocations, "") <> CompairStringResult.Equal Then
                strwhrcls += "and TSPL_LOCATION_MASTER.Location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        fndLocation.Value = clsLocation.getFinder(strwhrcls, fndLocation.Value, isButtonClicked)
        LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc  from TSPL_LOCATION_MASTER  where Location_Code  ='" + fndLocation.Value + "' ")
        strwhrcls = Nothing
    End Sub

    Private Sub FndPriceCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndPriceCode._MYValidating
        Dim dt As DataTable = Nothing
        Dim whrcls As String = String.Empty
        whrcls = " TSPL_BulkSalePrice_MASTER.Location_Code='" + fndLocation.Value + "' and Convert(date,TSPL_BulkSalePrice_MASTER.Price_Date ,103)<=CONVERT(date,'" & txtSOdate.Value & "',103) AND (ISNULL(Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill ,103),'')='' OR Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill,103)>=CONVERT(date,'" & txtSOdate.Value & "',103))"

        FndPriceCode.Value = ClsBulkSalePriceChart.getFinder(whrcls, FndPriceCode.Value, isButtonClicked)

        If clsCommon.myLen(FndPriceCode.Value) > 0 Then
            dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Ratio,Snf_Ratio,Standard_Rate,TolerancePerPlus,TolerancePerMinus from TSPL_BulkSalePrice_MASTER where Price_Code='" + FndPriceCode.Value + "'")
            If dt.Rows.Count > 0 Then
                TxtFatWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                TxtSNFWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                txtfatRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                txtSNFRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
                txtStanadardrate.Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                TxtToleranceinplus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
                txtToleranceinminus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
                ''------------------------

                'UpdateCurrentRow(0)
            End If
        Else
            gv1.DataSource = Nothing
        End If
        dt = Nothing
        whrcls = Nothing
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
        itemCode.ReadOnly = False
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

        Dim strUnitCode As New GridViewTextBoxColumn()
        strUnitCode.FormatString = ""
        strUnitCode.HeaderText = "UOM"
        strUnitCode.Name = colUnitCode
        strUnitCode.Width = 120
        strUnitCode.ReadOnly = False
        strUnitCode.WrapText = True
        strUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(strUnitCode)

        Dim strPriceCode As New GridViewTextBoxColumn()
        strPriceCode.FormatString = ""
        strPriceCode.HeaderText = "Price Chart Code"
        strPriceCode.Name = colPriceCode
        strPriceCode.Width = 120
        strPriceCode.ReadOnly = False
        strPriceCode.WrapText = True
        strPriceCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(strPriceCode)

        Dim FatWeightage As New GridViewDecimalColumn
        FatWeightage.FormatString = "{0:n2}"
        FatWeightage.HeaderText = "Fat Weightage"
        FatWeightage.Name = colFatWeightage
        FatWeightage.Width = 75
        FatWeightage.ReadOnly = True
        FatWeightage.WrapText = True
        FatWeightage.IsVisible = True
        FatWeightage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FatWeightage)

        Dim SnfWeightage As New GridViewDecimalColumn
        SnfWeightage.FormatString = "{0:n2}"
        SnfWeightage.HeaderText = "Snf Weightage"
        SnfWeightage.Name = colSnfWeightage
        SnfWeightage.Width = 75
        SnfWeightage.ReadOnly = True
        SnfWeightage.WrapText = True
        SnfWeightage.IsVisible = True
        SnfWeightage.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(SnfWeightage)

        Dim FatRatio As New GridViewDecimalColumn
        FatRatio.FormatString = "{0:n2}"
        FatRatio.HeaderText = "Fat Ratio"
        FatRatio.Name = colFatRatio
        FatRatio.Width = 75
        FatRatio.ReadOnly = True
        FatRatio.WrapText = True
        FatRatio.IsVisible = True
        FatRatio.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FatRatio)

        Dim SnfRatio As New GridViewDecimalColumn
        SnfRatio.FormatString = "{0:n2}"
        SnfRatio.HeaderText = "Snf Ratio"
        SnfRatio.Name = colSNFRatio
        SnfRatio.Width = 75
        SnfRatio.ReadOnly = True
        SnfRatio.WrapText = True
        SnfRatio.IsVisible = True
        SnfRatio.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(SnfRatio)

        Dim Standardrate As New GridViewDecimalColumn
        Standardrate.FormatString = "{0:n2}"
        Standardrate.HeaderText = "Standard rate"
        Standardrate.Name = colStandardRate
        Standardrate.Width = 75
        Standardrate.ReadOnly = False
        Standardrate.WrapText = True
        Standardrate.IsVisible = True
        Standardrate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Standardrate)

        Dim toleranceplus As New GridViewDecimalColumn
        toleranceplus.FormatString = "{0:n2}"
        toleranceplus.HeaderText = "Tolerance % in plus"
        toleranceplus.Name = colToleranceperplus
        toleranceplus.Width = 75
        toleranceplus.ReadOnly = False
        toleranceplus.WrapText = True
        toleranceplus.IsVisible = True
        toleranceplus.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(toleranceplus)

        Dim toleranceminus As New GridViewDecimalColumn
        toleranceminus.FormatString = "{0:n2}"
        toleranceminus.HeaderText = "Tolerance % in minus"
        toleranceminus.Name = colToleranceperminus
        toleranceminus.Width = 75
        toleranceminus.ReadOnly = False
        toleranceminus.WrapText = True
        toleranceminus.IsVisible = True
        toleranceminus.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(toleranceminus)

        Dim Qty As New GridViewDecimalColumn
        Qty.FormatString = "{0:n3}"
        Qty.HeaderText = "Qty"
        Qty.DecimalPlaces = 3
        Qty.Name = colQty
        Qty.Width = 120
        Qty.ReadOnly = False
        Qty.WrapText = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Qty)

        Dim Rate As New GridViewDecimalColumn
        Rate.FormatString = "{0:n3}"
        Rate.HeaderText = "Rate"
        Rate.DecimalPlaces = 3
        Rate.Name = colRate
        Rate.Width = 120
        Rate.ReadOnly = False
        Rate.WrapText = True
        Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Rate)

        Dim fatper As New GridViewDecimalColumn
        fatper.FormatString = "{0:n2}"
        fatper.HeaderText = "FAT %"
        fatper.Name = colFatPer
        fatper.Width = 75
        fatper.ReadOnly = False
        fatper.WrapText = True
        fatper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(fatper)



        Dim snfper As New GridViewDecimalColumn
        snfper.FormatString = "{0:n2}"
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


        Dim netmilkRate As New GridViewDecimalColumn
        netmilkRate.FormatString = "{0:n2}"
        netmilkRate.HeaderText = "Standard Milk Rate"
        netmilkRate.Name = colStandardMilkRate
        netmilkRate.Width = 75
        netmilkRate.ReadOnly = True
        netmilkRate.WrapText = True
        netmilkRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(netmilkRate)

        Dim actualmilkRate As New GridViewDecimalColumn
        actualmilkRate.FormatString = "{0:n2}"
        actualmilkRate.HeaderText = "Actual Milk Rate"
        actualmilkRate.Name = colActualMilkRate
        actualmilkRate.Width = 75
        actualmilkRate.ReadOnly = True
        actualmilkRate.WrapText = True
        actualmilkRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(actualmilkRate)

        ''=================================================
        Dim Amount As New GridViewDecimalColumn
        Amount.FormatString = "{0:n2}"
        Amount.HeaderText = "Amount"
        Amount.Name = colAmount
        Amount.Width = 75
        Amount.ReadOnly = True
        Amount.WrapText = True
        Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Amount)

        gv1.Rows.AddNew()
        gv1.Rows(0).Cells(colSlNo).Value = "1"
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AllowColumnReorder = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        ReStoreGridLayout()
        lineNo = Nothing
        itemCode = Nothing
        itemDesc = Nothing
        strUnitCode = Nothing
        If ApplyTSPriceAtBulkSale = True Then
            gv1.Columns(colPriceCode).IsVisible = False
            gv1.Columns(colFatPer).IsVisible = False
            gv1.Columns(colSNFPer).IsVisible = False
            'gv1.Columns(colCLR).IsVisible = False
            gv1.Columns(colStandardMilkRate).IsVisible = False
            gv1.Columns(colActualMilkRate).IsVisible = False
            gv1.Columns(colFatKG).IsVisible = False
            gv1.Columns(colSNFKG).IsVisible = False
            gv1.Columns(ColFatAmount).IsVisible = False
            gv1.Columns(ColSNFAmount).IsVisible = False
            gv1.Columns(colFatRate).IsVisible = False
            gv1.Columns(colSNFRate).IsVisible = False
            gv1.Columns(colFatWeightage).IsVisible = False
            gv1.Columns(colSnfWeightage).IsVisible = False
            gv1.Columns(colFatRatio).IsVisible = False
            gv1.Columns(colSNFRatio).IsVisible = False
            gv1.Columns(colToleranceperplus).IsVisible = False
            gv1.Columns(colToleranceperminus).IsVisible = False
            gv1.Columns(colStandardRate).IsVisible = False
            gv1.Columns(colAmount).IsVisible = False
        End If
        gv1.AllowAddNewRow = False
    End Sub

    Private Sub fndPaymenttermsNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPaymenttermsNo._MYValidating
        fndPaymenttermsNo.Value = clsPaymentTerms.getFinder("", fndPaymenttermsNo.Value, isButtonClicked)
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True



                    If e.Column Is gv1.Columns(colUnitCode) OrElse e.Column Is gv1.Columns(colItemCode) OrElse e.Column Is gv1.Columns(colPriceCode) Then
                        If e.Column Is gv1.Columns(colItemCode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colUnitCode) Then
                            OpenUOMList(False)
                        ElseIf e.Column Is gv1.Columns(colPriceCode) Then
                            OpenPriceCodeList(False)
                        End If
                        'UpdateCurrentRow(gv1.CurrentRow.Index) ''-1 is for current row
                        'UpdateAllTotals()
                        'End If
                        ''setGridFocus()
                    End If
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
                    If e.Column Is gv1.Columns(colFatPer) Or e.Column Is gv1.Columns(colQty) Or e.Column Is gv1.Columns(colSNFPer) Or e.Column Is gv1.Columns(colRate) Or e.Column Is gv1.Columns(colPriceCode) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                    End If
                End If
                UpdateAllTotals()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
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

            If clsCommon.myLen(gv1.Rows(IntRowNo).Cells(colPriceCode).Value) <= 0 Then
                ' StandardRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
                StandardRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
                FatWeightage = clsCommon.myCdbl(TxtFatWeightage.Value)
                SNFWeightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
                FatRatio = clsCommon.myCdbl(txtfatRatio.Value)
                SNFRatio = clsCommon.myCdbl(txtSNFRatio.Value)
            Else
                StandardRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colRate).Value)
                FatWeightage = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatWeightage).Value)
                SNFWeightage = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSnfWeightage).Value)
                FatRatio = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatRatio).Value)
                SNFRatio = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFRatio).Value)
            End If


            FatPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatPer).Value)
            SNFPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFPer).Value)

            Qty = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), 3)

            FatRate = Math.Round(StandardRate * FatWeightage / FatRatio, 2)
            SNFRate = Math.Round(StandardRate * SNFWeightage / SNFRatio, 2)

            gv1.Rows(IntRowNo).Cells(colFatRate).Value = clsCommon.myCdbl(FatRate)
            gv1.Rows(IntRowNo).Cells(colSNFRate).Value = clsCommon.myCdbl(SNFRate)

            FatKG = Math.Round(((Qty * FatPer) / 100), 3)
            SNFKG = Math.Round(((Qty * SNFPer) / 100), 3)

            gv1.Rows(IntRowNo).Cells(colFatKG).Value = clsCommon.myCdbl(FatKG)
            gv1.Rows(IntRowNo).Cells(colSNFKG).Value = clsCommon.myCdbl(SNFKG)

            FatAmount = Math.Round(FatRate * FatKG, 2)
            SNFAmount = Math.Round(SNFRate * SNFKG, 2)

            gv1.Rows(IntRowNo).Cells(ColFatAmount).Value = clsCommon.myCdbl(FatAmount)
            gv1.Rows(IntRowNo).Cells(ColSNFAmount).Value = clsCommon.myCdbl(SNFAmount)

            Amount = FatAmount + SNFAmount

            gv1.Rows(IntRowNo).Cells(colAmount).Value = clsCommon.myCdbl(Amount)

            gv1.Rows(IntRowNo).Cells(colFatKG).Value = clsCommon.myCdbl(FatKG)
            gv1.Rows(IntRowNo).Cells(colSNFKG).Value = clsCommon.myCdbl(SNFKG)
            If Qty > 0 Then
                gv1.Rows(IntRowNo).Cells(colActualMilkRate).Value = Math.Round(clsCommon.myCdbl(Amount / Qty), 2)
            End If

            ''------------to calculate standard milk rate using standard rate of price chart
            'StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)
            'FatWeightage = clsCommon.myCdbl(TxtFatWeightage.Value)
            'SNFWeightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
            'FatRatio = clsCommon.myCdbl(txtfatRatio.Value)
            'SNFRatio = clsCommon.myCdbl(txtSNFRatio.Value)


            If clsCommon.myLen(gv1.Rows(IntRowNo).Cells(colPriceCode).Value) <= 0 Then
                StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)
                FatWeightage = clsCommon.myCdbl(TxtFatWeightage.Value)
                SNFWeightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
                FatRatio = clsCommon.myCdbl(txtfatRatio.Value)
                SNFRatio = clsCommon.myCdbl(txtSNFRatio.Value)
            Else
                StandardRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colStandardRate).Value)
                FatWeightage = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatWeightage).Value)
                SNFWeightage = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSnfWeightage).Value)
                FatRatio = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatRatio).Value)
                SNFRatio = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFRatio).Value)
            End If



            FatPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatPer).Value)
            SNFPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFPer).Value)

            Qty = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), 3)

            FatRate = Math.Round(StandardRate * FatWeightage / FatRatio, 2)
            SNFRate = Math.Round(StandardRate * SNFWeightage / SNFRatio, 2)

            FatKG = Math.Round(((Qty * FatPer) / 100), 3)
            SNFKG = Math.Round(((Qty * SNFPer) / 100), 3)

            FatAmount = Math.Round(FatRate * FatKG, 2)
            SNFAmount = Math.Round(SNFRate * SNFKG, 2)

            Amount = FatAmount + SNFAmount

            If Qty > 0 Then
                gv1.Rows(IntRowNo).Cells(colStandardMilkRate).Value = Math.Round(clsCommon.myCdbl(Amount / Qty), 2)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "Select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER"
        gv1.CurrentRow.Cells(colItemCode).Value = clsCommon.ShowSelectForm("SalesOrderItemFinder", qry, "Code", "Product_Type ='MI' and Active=1", clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_desc from TSPL_ITEM_Master where Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) + "'")
        qry = Nothing

        If clsCommon.myLen(FndPriceCode.Value) > 0 Then
            gv1.CurrentRow.Cells(colPriceCode).Value = clsCommon.myCstr(FndPriceCode.Value)
            gv1.CurrentRow.Cells(colFatWeightage).Value = clsCommon.myCdbl(TxtFatWeightage.Value)
            gv1.CurrentRow.Cells(colSnfWeightage).Value = clsCommon.myCdbl(TxtSNFWeightage.Value)
            gv1.CurrentRow.Cells(colFatRatio).Value = clsCommon.myCdbl(txtfatRatio.Value)
            gv1.CurrentRow.Cells(colSNFRatio).Value = clsCommon.myCdbl(txtSNFRatio.Value)
            gv1.CurrentRow.Cells(colStandardRate).Value = clsCommon.myCdbl(txtStanadardrate.Value)
            gv1.CurrentRow.Cells(colToleranceperplus).Value = clsCommon.myCdbl(TxtToleranceinplus.Value)
            gv1.CurrentRow.Cells(colToleranceperminus).Value = clsCommon.myCdbl(txtToleranceinminus.Value)
        End If
    End Sub

    Sub OpenPriceCodeList(ByVal isButtonClick As Boolean)

        Dim dt As DataTable = Nothing
        Dim whrcls As String = String.Empty
        whrcls = " TSPL_BulkSalePrice_MASTER.Location_Code='" + fndLocation.Value + "' and Convert(date,TSPL_BulkSalePrice_MASTER.Price_Date ,103)<=CONVERT(date,'" & txtSOdate.Value & "',103) AND (ISNULL(Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill ,103),'')='' OR Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill,103)>=CONVERT(date,'" & txtSOdate.Value & "',103))"

        gv1.CurrentRow.Cells(colPriceCode).Value = ClsBulkSalePriceChart.getFinder(whrcls, gv1.CurrentRow.Cells(colPriceCode).Value, isButtonClick)

        If clsCommon.myLen(gv1.CurrentRow.Cells(colPriceCode).Value) > 0 Then
            dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Ratio,Snf_Ratio,Standard_Rate,TolerancePerPlus,TolerancePerMinus from TSPL_BulkSalePrice_MASTER where Price_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colPriceCode).Value) + "'")
            If dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colFatWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                gv1.CurrentRow.Cells(colSnfWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                gv1.CurrentRow.Cells(colFatRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                gv1.CurrentRow.Cells(colSNFRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
                gv1.CurrentRow.Cells(colStandardRate).Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                gv1.CurrentRow.Cells(colToleranceperplus).Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
                gv1.CurrentRow.Cells(colToleranceperminus).Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
            End If
        Else
            gv1.CurrentRow.Cells(colFatWeightage).Value = 0
            gv1.CurrentRow.Cells(colSnfWeightage).Value = 0
            gv1.CurrentRow.Cells(colFatRatio).Value = 0
            gv1.CurrentRow.Cells(colSNFRatio).Value = 0
            gv1.CurrentRow.Cells(colStandardRate).Value = 0
            gv1.CurrentRow.Cells(colToleranceperplus).Value = 0
            gv1.CurrentRow.Cells(colToleranceperminus).Value = 0

        End If
        dt = Nothing
        whrcls = Nothing

    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gv1.CurrentRow.Cells(colUnitCode).Value = clsCommon.ShowSelectForm("SalesorderItemfndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitCode).Value), "Code", isButtonClick)
        End If
    End Sub
    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colSlNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        '===============Preeti==================================
        If AllowFutureDateTransaction(txtSOdate.Value, Nothing) = False Then
            txtSOdate.Select()
            Return False
        End If
        '=======================================================
        If clsCommon.myLen(FndCustomerNo.Value) <= 0 Then
            FndCustomerNo.Focus()
            Throw New Exception("Customer cannot be left blank")
        End If
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            fndLocation.Focus()
            Throw New Exception("Location cannot be left blank")
        End If

        If clsCommon.myLen(fndPaymenttermsNo.Value) <= 0 Then
            fndPaymenttermsNo.Focus()
            Throw New Exception("Payment Terms cannot be left blank")
        End If
        If ApplyTSPriceAtBulkSale = False Then
            If clsCommon.myLen(TxtPONo.Text) <= 0 Then
                TxtPONo.Focus()
                Throw New Exception("PO No cannot be left blank")
            End If

            If clsCommon.myLen(FndPriceCode.Value) <= 0 Then
                FndPriceCode.Focus()
                Throw New Exception("Price Chart cannot be left blank")
            End If
        End If


        Dim dt As DataTable = Nothing
        Dim strcount As Integer = 0

        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(i).Cells(colItemCode).Value) > 0 Then
                If clsCommon.myLen(gv1.Rows(i).Cells(colUnitCode).Value) <= 0 Then
                    Throw New Exception("UOM cannot be left blank")
                End If
                If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) = 0 Then
                    Throw New Exception("Qty cannot be zero")
                End If
                If clsCommon.myCdbl(gv1.Rows(i).Cells(colRate).Value) = 0 Then
                    Throw New Exception("Rate cannot be zero")
                End If
                If ApplyTSPriceAtBulkSale = False Then
                    If clsCommon.myCdbl(gv1.Rows(i).Cells(colFatPer).Value) = 0 Then
                        Throw New Exception("Fat% cannot be zero")
                    End If
                    If AllowSNFNotManditoryInBulkSale = False Then
                        If clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFPer).Value) = 0 Then
                            Throw New Exception("SNF% cannot be zero")
                        End If
                    End If
                    If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) < 0 Then
                        Throw New Exception("Qty cannot be in negative")
                    End If
                    If clsCommon.myCdbl(gv1.Rows(i).Cells(colRate).Value) < 0 Then
                        Throw New Exception("Rate cannot be in negative")
                    End If
                    If clsCommon.myCdbl(gv1.Rows(i).Cells(colFatPer).Value) < 0 Then
                        Throw New Exception("Fat% cannot be in negative")
                    End If
                    If clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFPer).Value) < 0 Then
                        Throw New Exception("SNF% cannot be in negative")
                    End If
                    If clsCommon.myLen(gv1.Rows(i).Cells(colPriceCode).Value) > 0 Then
                        dt = clsDBFuncationality.GetDataTable("Select round((Standard_Rate+((Standard_Rate *TolerancePerPlus )/100)),2) as StandardRatePlus,round((Standard_Rate-((Standard_Rate *TolerancePerMinus  )/100)),2) as StandardRateMinus  from TSPL_BulkSalePrice_MASTER where Price_Code ='" + clsCommon.myCstr(gv1.Rows(i).Cells(colPriceCode).Value) + "'")
                        If dt.Rows.Count > 0 Then
                            If clsCommon.myCdbl(gv1.Rows(i).Cells(colRate).Value) >= clsCommon.myCdbl(dt.Rows(0)("StandardRateMinus")) And clsCommon.myCdbl(gv1.Rows(i).Cells(colRate).Value) <= clsCommon.myCdbl(dt.Rows(0)("StandardRatePlus")) Then
                            Else
                                Throw New Exception("Rate should be in range according to price chart")
                            End If
                        End If
                    Else
                        dt = clsDBFuncationality.GetDataTable("Select round((Standard_Rate+((Standard_Rate *TolerancePerPlus )/100)),2) as StandardRatePlus,round((Standard_Rate-((Standard_Rate *TolerancePerMinus  )/100)),2) as StandardRateMinus  from TSPL_BulkSalePrice_MASTER where Price_Code ='" + FndPriceCode.Value + "'")
                        If dt.Rows.Count > 0 Then

                            If clsCommon.myCdbl(gv1.Rows(i).Cells(colRate).Value) >= clsCommon.myCdbl(dt.Rows(0)("StandardRateMinus")) And clsCommon.myCdbl(gv1.Rows(i).Cells(colRate).Value) <= clsCommon.myCdbl(dt.Rows(0)("StandardRatePlus")) Then
                            Else
                                Throw New Exception("Rate should be in range according to price chart")
                            End If
                        End If
                    End If
                End If
                strcount = strcount + 1
            Else
                If clsCommon.myLen(gv1.Rows(i).Cells(colItemCode).Value) > 0 Then
                    strcount = strcount + 1
                End If
            End If

        Next


        If strcount = 0 Then
            Throw New Exception("Please fill atleast one item into grid")
        End If

        Return True
    End Function
    Sub SaveData()
        Dim obj As New ClsSalesOrderBulkSale_Pavitra()
        Dim objTr As New clsSalesOrderDetailBulkSale_Pavitra
        Try
            If AllowToSave() Then

                obj.Document_No = clsCommon.myCstr(fndSONo.Value)
                obj.Document_Date = clsCommon.myCDate(txtSOdate.Value)
                obj.Customer_Code = clsCommon.myCstr(FndCustomerNo.Value)
                obj.Customer_Name = clsCommon.myCstr(lblCustomerName.Text)
                obj.Location_Code = clsCommon.myCstr(fndLocation.Value)
                obj.Location_Name = clsCommon.myCstr(LblLocationName.Text)
                obj.TERMS_Code = clsCommon.myCstr(fndPaymenttermsNo.Value)
                obj.PO_NO = clsCommon.myCstr(TxtPONo.Text)
                obj.PO_Date = clsCommon.myCDate(txtPODate.Value)
                obj.Price_Code = clsCommon.myCstr(FndPriceCode.Value)
                obj.Fat_Weightage = clsCommon.myCdbl(TxtFatWeightage.Value)
                obj.Snf_Weightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
                obj.Fat_Ratio = clsCommon.myCdbl(txtfatRatio.Value)
                obj.Snf_Ratio = clsCommon.myCdbl(txtSNFRatio.Value)
                obj.Standard_Rate = clsCommon.myCdbl(txtStanadardrate.Value)
                obj.TolerancePerPlus = clsCommon.myCdbl(TxtToleranceinplus.Value)
                obj.TolerancePerMinus = clsCommon.myCdbl(txtToleranceinminus.Value)
                obj.DocumentAmount = clsCommon.myCdbl(lblTotRAmt1.Text)
                If chkSOclose.Checked = True Then
                    obj.close_yn = "Y"
                ElseIf chkSOclose.Checked = False Then
                    obj.close_yn = "N"
                End If
                obj.arrSalesOrderDetailBulkSale = New List(Of clsSalesOrderDetailBulkSale_Pavitra)

                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colItemCode).Value)) > 0 Then
                        objTr = New clsSalesOrderDetailBulkSale_Pavitra()
                        objTr.Document_No = clsCommon.myCstr(obj.Document_No)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.Item_Name = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                        objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colPriceCode).Value)) > 0 Then
                            objTr.Price_Code = clsCommon.myCstr(grow.Cells(colPriceCode).Value)
                            objTr.Fat_Weightage = clsCommon.myCdbl(grow.Cells(colFatWeightage).Value)
                            objTr.Snf_Weightage = clsCommon.myCdbl(grow.Cells(colSnfWeightage).Value)
                            objTr.Fat_Ratio = clsCommon.myCdbl(grow.Cells(colFatRatio).Value)
                            objTr.Snf_Ratio = clsCommon.myCdbl(grow.Cells(colSNFRatio).Value)
                            objTr.Standard_Rate = clsCommon.myCdbl(grow.Cells(colStandardRate).Value)
                            objTr.TolerancePerPlus = clsCommon.myCdbl(grow.Cells(colToleranceperplus).Value)
                            objTr.TolerancePerMinus = clsCommon.myCdbl(grow.Cells(colToleranceperminus).Value)
                        Else
                            objTr.Price_Code = clsCommon.myCstr(FndPriceCode.Value)
                            objTr.Fat_Weightage = clsCommon.myCdbl(TxtFatWeightage.Value)
                            objTr.Snf_Weightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
                            objTr.Fat_Ratio = clsCommon.myCdbl(txtfatRatio.Value)
                            objTr.Snf_Ratio = clsCommon.myCdbl(txtSNFRatio.Value)
                            objTr.Standard_Rate = clsCommon.myCdbl(txtStanadardrate.Value)
                            objTr.TolerancePerPlus = clsCommon.myCdbl(TxtToleranceinplus.Value)
                            objTr.TolerancePerMinus = clsCommon.myCdbl(txtToleranceinminus.Value)
                        End If

                        objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                        objTr.FatPer = clsCommon.myCdbl(grow.Cells(colFatPer).Value)
                        objTr.SNFPer = clsCommon.myCdbl(grow.Cells(colSNFPer).Value)
                        objTr.FatRate = clsCommon.myCdbl(grow.Cells(colFatRate).Value)
                        objTr.SNFRate = clsCommon.myCdbl(grow.Cells(colSNFRate).Value)
                        objTr.Fat_KG = clsCommon.myCdbl(grow.Cells(colFatKG).Value)
                        objTr.SNF_KG = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                        objTr.FatAmount = clsCommon.myCdbl(grow.Cells(ColFatAmount).Value)
                        objTr.SNFAmount = clsCommon.myCdbl(grow.Cells(ColSNFAmount).Value)
                        objTr.StandardMilkRate = clsCommon.myCdbl(grow.Cells(colStandardMilkRate).Value)
                        objTr.ActualMilkRate = clsCommon.myCdbl(grow.Cells(colActualMilkRate).Value)
                        objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                        obj.arrSalesOrderDetailBulkSale.Add(objTr)
                    End If

                Next


                If (XpertERPEngine.ClsSalesOrderBulkSale_Pavitra.SaveData(obj, isNewEntry)) Then
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim dt As DataTable = Nothing
        Dim obj As ClsSalesOrderBulkSale_Pavitra = Nothing
        Try
            obj = ClsSalesOrderBulkSale_Pavitra.GetData(strCode, arrLoc, NavTyep)

            isInsideLoadData = True
            If obj IsNot Nothing Then
                isNewEntry = False
                fndSONo.Value = obj.Document_No
                txtSOdate.Value = obj.Document_Date
                FndCustomerNo.Value = obj.Customer_Code
                lblCustomerName.Text = obj.Customer_Name
                fndLocation.Value = obj.Location_Code
                LblLocationName.Text = obj.Location_Name
                TxtPONo.Text = obj.PO_NO
                txtPODate.Value = obj.PO_Date
                fndPaymenttermsNo.Value = obj.TERMS_Code
                FndPriceCode.Value = obj.Price_Code
                TxtFatWeightage.Value = obj.Fat_Weightage
                TxtSNFWeightage.Value = obj.Snf_Weightage
                txtfatRatio.Value = obj.Fat_Ratio
                txtSNFRatio.Value = obj.Snf_Ratio
                TxtToleranceinplus.Value = obj.TolerancePerPlus
                txtToleranceinminus.Value = obj.TolerancePerPlus
                txtStanadardrate.Value = obj.Standard_Rate
                lblTotRAmt1.Text = obj.DocumentAmount
               
                loadBlankItemGrid()
                If obj.arrSalesOrderDetailBulkSale IsNot Nothing AndAlso obj.arrSalesOrderDetailBulkSale.Count > 0 Then
                    For Each objTr As clsSalesOrderDetailBulkSale_Pavitra In obj.arrSalesOrderDetailBulkSale
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = objTr.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = objTr.Unit_code

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceCode).Value = objTr.Price_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatWeightage).Value = objTr.Fat_Weightage
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSnfWeightage).Value = objTr.Snf_Weightage
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatRatio).Value = objTr.Fat_Ratio
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFRatio).Value = objTr.Snf_Ratio
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colStandardRate).Value = objTr.Standard_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colToleranceperplus).Value = objTr.TolerancePerPlus
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colToleranceperminus).Value = objTr.TolerancePerMinus

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatPer).Value = objTr.FatPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNFPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatRate).Value = objTr.FatRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFRate).Value = objTr.SNFRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value = objTr.Fat_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = objTr.SNF_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFatAmount).Value = objTr.FatAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFAmount).Value = objTr.SNFAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colStandardMilkRate).Value = objTr.StandardMilkRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colActualMilkRate).Value = objTr.ActualMilkRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
                        gv1.Rows.AddNew()
                    Next
                    gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                Else
                    gv1.DataSource = Nothing
                End If
                fndSONo.MyReadOnly = True
                btnsave.Text = "Update"


                If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                    btnPost.Enabled = False
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    chkSOclose.Enabled = True
                Else
                    btnPost.Enabled = True
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    chkSOclose.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                ''richa 9 AMy,2019
                If obj.close_yn = "Y" Then
                    chkSOclose.Checked = True
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                    isallowforclose = False
                ElseIf obj.close_yn = "N" Then
                    chkSOclose.Checked = False
                    isallowforclose = True
                End If

                UcAttachment1.LoadData(obj.Document_No)
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

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsSalesOrderBulkSale_Pavitra.DeleteData(fndSONo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub PostData()
        Try
            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                If (ClsSalesOrderBulkSale_Pavitra.PostData(MyBase.Form_ID, arrLoc, fndSONo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully posted")
                    LoadData(fndSONo.Value, NavigatorType.Current)
                End If
            End If
            isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
        End Try
    End Sub

    Private Sub fndSONo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndSONo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_SALES_ORDER_MASTER_BULKSALE where Document_No='" + fndSONo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndSONo.MyReadOnly = True
            ElseIf check <= 0 Then
                fndSONo.MyReadOnly = False
            End If

            LoadData(fndSONo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub fndSONo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSONo._MYValidating
        Dim qry As String = "Select TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No as Code,Convert(varchar,TSPL_SALES_ORDER_MASTER_BULKSALE.Document_Date,103) as [Dispatch Date],TSPL_SALES_ORDER_MASTER_BULKSALE.Customer_Code as [Customer Code],TSPL_SALES_ORDER_MASTER_BULKSALE.Customer_Name as [Customer Name],TSPL_SALES_ORDER_MASTER_BULKSALE.Location_Code as [Location Code],TSPL_SALES_ORDER_MASTER_BULKSALE.Location_Name [Location Name],TSPL_SALES_ORDER_MASTER_BULKSALE.PO_NO as [PO NO],Convert(varchar,TSPL_SALES_ORDER_MASTER_BULKSALE.PO_Date,103) as [PO Date],TSPL_SALES_ORDER_MASTER_BULKSALE.Price_Code as [Price Code],TSPL_SALES_ORDER_MASTER_BULKSALE.TERMS_Code as [Payment Terms],case when TSPL_SALES_ORDER_MASTER_BULKSALE.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_SALES_ORDER_MASTER_BULKSALE"
        fndSONo.Value = clsCommon.ShowSelectForm("SalesOrderBulkSale", qry, "Code", " TSPL_SALES_ORDER_MASTER_BULKSALE.Location_Code in (" + arrLoc + ")", fndSONo.Value, "", isButtonClicked)
        LoadData(fndSONo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub RMSaveLayout_Click(sender As Object, e As EventArgs) Handles RMSaveLayout.Click
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
            obj = Nothing
        End If
    End Sub

    Private Sub RDDeleteLayout_Click(sender As Object, e As EventArgs) Handles RDDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
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
    ''richa ERO/08/05/19-000594
    Private Sub chkSOclose_CheckedChanged(sender As Object, e As EventArgs) Handles chkSOclose.CheckedChanged
        Try
            If chkSOclose.Checked = True AndAlso isallowforclose = True Then
                If common.clsCommon.MyMessageBoxShow("Close Sale Order for the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    Dim strgateentry As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select document_no  from TSPL_GATEENTRY_SALE where Bulk_SO_No='" & fndSONo.Value & "' "))
                    If clsCommon.myLen(strgateentry) <= 0 Then
                        '' REASON FOR Close
                        Dim Reason As String = ""
                        Dim frm As New FrmFreeTxtBox1
                        frm.Text = "Remarks for Close"
                        frm.ShowDialog()
                        If clsCommon.myLen(frm.strRmks) <= 0 Then
                            Exit Sub
                        Else
                            Reason = frm.strRmks
                        End If
                        If (ClsSalesOrderBulkSale_Pavitra.closepodata(fndSONo.Value, chkSOclose.Checked, arrLoc, Reason)) Then
                            common.clsCommon.MyMessageBoxShow("Sale Order closed Successfully.", Me.Text)
                            LoadData(fndSONo.Value, NavigatorType.Current)
                            isallowforclose = False
                        End If
                    Else
                        Throw New Exception("Sale Order should not be closed becuase it is used in Gate Entry Document " & strgateentry & "")
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

End Class