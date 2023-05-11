
Imports System.Data.SqlClient
Imports common
Imports System.IO


Public Class FrmCanSale
    Inherits FrmMainTranScreen
#Region "Variables"
    Public AllowtoChangeTCSBaseAmount As Boolean = False
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
     Public Const colSlNo As String = "SL"
    Public Const colDocDate As String = "Date"
    Public Const colCustCode As String = "Distributor"
    Public Const colIsValidated As String = "colIsValidated"
    Public Const colErrorStatus As String = "colErrorStatus"
    Public Const colNoOfCans As String = "No of cans"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemName As String = "colItemName"
    Public Const colUOM As String = "colUOM"
    Public Const colQty As String = "Qty"
    Public Const colMilkAmount As String = "Milk Amt"
    Public Const colFatPer As String = "Fat %"
    Public Const colSNFPer As String = "SNF %"
    Public Const colMilkRate As String = "Milk Rate"
    Public Const colFatKG As String = "Fat KG"
    Public Const colSNFKG As String = "SNF KG"

    Public Const colPriceRate As String = "D M Rate"
    Public Const ColFatAmount As String = "Fat Amt"
    Public Const ColSNFAmount As String = "SNF Amt"
    Public Const colFatRate As String = "Fat Rate"
    Public Const colSNFRate As String = "SNF Rate"
    Public Const colTotalAmount As String = "Total Amt"
    Public Const colDifference As String = "Diff"
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
 
    Public Shared Alocation As String = Nothing
    Public DocumentNo As String = ""
    Dim Qry As String
    Dim AmountToCheckCustomerOutstandingForTCSTax As Double = 0
    Dim TCSTaxApplicableOnCanSale As Boolean = False

#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmDispatchBulkSale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
       
        End If
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub FrmDispatchBulkSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TCSTaxApplicableOnCanSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.TCSTaxApplicableOnCanSale, clsFixedParameterCode.TCSTaxApplicableOnCanSale, Nothing)) = 1, True, False))
        AmountToCheckCustomerOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckCustomerOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckCustomerOutstandingForTCSTax, Nothing))
        AllowtoChangeTCSBaseAmount = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoChangeTCSBaseAmount, clsFixedParameterCode.AllowtoChangeTCSBaseAmount, Nothing)) = 0, False, True)
        SetUserMgmtNew()
        Reset()
        LOCATIONRIGTHS()
        UcAttachment1.Form_ID = MyBase.Form_ID
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N New Transaction")
        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag

    End Sub
  
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'lblLocationCode.Value = obj.Default_LocCode
                'LblLocationName.Text = obj.Default_LocName
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
    Sub loadBlankItemGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "SL"
        lineNo.Name = colSlNo
        lineNo.Width = 60
        lineNo.ReadOnly = True
        lineNo.WrapText = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(lineNo)

        Dim ItemCode As New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = True
        ItemCode.WrapText = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ItemCode)

        Dim ItemName As New GridViewTextBoxColumn()
        ItemName.FormatString = ""
        ItemName.HeaderText = "Item Name"
        ItemName.Name = colItemName
        ItemName.Width = 100
        ItemName.ReadOnly = True
        ItemName.WrapText = True
        ItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ItemName)

        Dim UOM As New GridViewTextBoxColumn()
        UOM.FormatString = ""
        UOM.HeaderText = "UOM"
        UOM.Name = colUOM
        UOM.Width = 100
        UOM.ReadOnly = True
        UOM.WrapText = True
        UOM.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(UOM)

        Dim NoOfCans As New GridViewDecimalColumn
        NoOfCans.FormatString = "{0:n3}"
        NoOfCans.HeaderText = "No of Cans"
        NoOfCans.DecimalPlaces = 0
        NoOfCans.Name = colNoOfCans
        NoOfCans.Width = 120
        NoOfCans.ReadOnly = True
        NoOfCans.WrapText = True
        NoOfCans.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(NoOfCans)

        Dim Qty As New GridViewDecimalColumn
        Qty.FormatString = "{0:n3}"
        Qty.HeaderText = "Qty"
        Qty.DecimalPlaces = 3
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

        Dim netmilkRate As New GridViewDecimalColumn
        netmilkRate.FormatString = "{0:n2}"
        netmilkRate.HeaderText = "Milk Rate"
        netmilkRate.Name = colMilkRate
        netmilkRate.Width = 75
        netmilkRate.ReadOnly = True
        netmilkRate.WrapText = True
        netmilkRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(netmilkRate)

        ''=================================================
        Dim Amount As New GridViewDecimalColumn
        Amount.FormatString = "{0:n2}"
        Amount.HeaderText = "Milk Amt"
        Amount.Name = colMilkAmount
        Amount.Width = 75
        Amount.ReadOnly = True
        Amount.WrapText = True
        Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Amount)

        Dim DMRate As New GridViewDecimalColumn
        DMRate.FormatString = "{0:n2}"
        DMRate.HeaderText = "D M Rate"
        DMRate.Name = colPriceRate
        DMRate.Width = 75
        DMRate.ReadOnly = True
        DMRate.WrapText = True
        DMRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(DMRate)



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



        Dim FatAmount As New GridViewDecimalColumn
        FatAmount.FormatString = "{0:n2}"
        FatAmount.HeaderText = "FAT Amt"
        FatAmount.Name = ColFatAmount
        FatAmount.Width = 75
        FatAmount.ReadOnly = True
        FatAmount.WrapText = True
        FatAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FatAmount)

        Dim SNFAmount As New GridViewDecimalColumn
        SNFAmount.FormatString = "{0:n2}"
        SNFAmount.HeaderText = "SNF Amt"
        SNFAmount.Name = ColSNFAmount
        SNFAmount.Width = 75
        SNFAmount.ReadOnly = True
        SNFAmount.WrapText = True
        SNFAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(SNFAmount)


        Dim TotalAmount As New GridViewDecimalColumn
        TotalAmount.FormatString = "{0:n2}"
        TotalAmount.HeaderText = "Total Amt"
        TotalAmount.Name = colTotalAmount
        TotalAmount.Width = 75
        TotalAmount.ReadOnly = True
        TotalAmount.WrapText = True
        TotalAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(TotalAmount)

        Dim Diff As New GridViewDecimalColumn
        Diff.FormatString = "{0:n2}"
        Diff.HeaderText = "Diff"
        Diff.Name = colDifference
        Diff.Width = 75
        Diff.ReadOnly = True
        Diff.WrapText = True
        Diff.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Diff)

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
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.Rows.AddNew()
        gv1.Rows(0).Cells(colSlNo).Value = "1"
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        lineNo = Nothing


    End Sub

  

    

    

    

    
    'Private Sub FndPriceCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndPriceCode._MYValidating
    '    Dim dt As DataTable = Nothing
    '    Dim whrcls As String = String.Empty

    '    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code= '" & clsFixedParameterCode.showPostrequiredforBulkSale & "' and Type ='" & clsFixedParameterType.showPostrequiredforBulkSale & "'")), "1") = CompairStringResult.Equal Then
    '        whrcls = " TSPL_BulkSalePrice_MASTER.Posted='1' "
    '    End If

    '    FndPriceCode.Value = ClsBulkSalePriceChart.getFinder(whrcls, FndPriceCode.Value, isButtonClicked)

    '    If clsCommon.myLen(FndPriceCode.Value) > 0 Then
    '        dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Ratio,Snf_Ratio,Standard_Rate,TolerancePerPlus,TolerancePerMinus,Posted from TSPL_BulkSalePrice_MASTER where Price_Code='" + FndPriceCode.Value + "'")
    '        If dt.Rows.Count > 0 Then
    '            TxtFatWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
    '            TxtSNFWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
    '            txtfatRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
    '            txtSNFRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
    '            txtStanadardrate.Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
    '            TxtToleranceinplus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
    '            txtToleranceinminus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
    '            UpdateCurrentRow(0)
    '        End If
    '    Else
    '        gv1.DataSource = Nothing
    '    End If
    '    dt = Nothing
    '    whrcls = Nothing
    'End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colFatPer) Or e.Column Is gv1.Columns(colSNFPer) Then
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            'UpdateCurrentRow(ii)
                        Next
                    End If
                End If
                'UpdateAllTotals()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
 
 
    
    

    

    'Sub SaveData()

    '    Dim totalqty As Decimal = 0
    '    Dim objApproval As New clsApply_Approval()
    '    Dim obj As New ClsDispatchBulkSale()
    '    Dim objTr As New clsDispatchDetailBulkSale
    '    Try
    '        If AllowNLevel Then
    '            If Not AllowModifcationByApprovalUser Then
    '                clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(txtDocNo.Value))
    '            End If
    '        End If
    '        If AllowToSave() Then

    '            obj.Document_No = txtDocNo.Value
    '            obj.Document_Date = txtDate.Value
    '            obj.Customer_Code = lblCustomerCode.Text
    '            obj.QC_Code = LblQCCode.Text
    '            obj.Tanker_Code = FndTankerCode.Value
    '            obj.Location_Code = lblLocationCode.Text
    '            obj.Dip_marking = TxtDipMarking.Text
    '            obj.Challan_No = TxtChallanNo.Text
    '            obj.Insurance_No = txtinsuranceno.Text
    '            obj.Seal_No = txtsealno.Text
    '            obj.Tare_Weight = TxtTareWeight.Value
    '            obj.Gross_Weight = txtGrossWeight.Value
    '            obj.Net_Weight = txtNetWeight.Value
    '            If ApplyTSPriceAtBulkSale = False Then
    '                obj.Price_Code = FndPriceCode.Value
    '            End If
    '            obj.Total_Amt = lblTotRAmt1.Text
    '            obj.CreditLimit = clsCommon.myCdbl(txtCreditLimit.Text)

    '            If chkCreateAoutoInvoice.Checked = True Then
    '                obj.Is_Create_Auto_Invoice = 1
    '            Else
    '                obj.Is_Create_Auto_Invoice = 0
    '            End If
    '            obj.ApprovalRequired = "N"
    '            obj.Status = "Open"
    '            obj.EWayBillNo = TxtEWayBillNo.Text
    '            obj.EWayBillDate = txtewaybilldate.Value
    '            ''richa 31/12/2014
    '            Dim desc As String = ""
    '            desc = clsDBFuncationality.getSingleValue("Select CheckCreditLimit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + lblCustomerCode.Text + "'")
    '            If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
    '                Dim dblAllowedAmt As Double = 0
    '                If CheckCustomerOutstandingAmount(lblCustomerCode.Text, True) = False AndAlso Not AllowNLevel Then
    '                    obj.ApprovalRequired = "Y"
    '                    obj.Status = "Pending"
    '                End If
    '            End If
    '            If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select Approved from TSPL_Dispatch_BulkSale where Document_No='" & txtDocNo.Value & "' "), "Y") = CompairStringResult.Equal Then
    '                obj.ApprovalRequired = "Y"
    '                obj.Status = "Approved"
    '            End If
    '            obj.Fat_Weightage = clsCommon.myCdbl(TxtFatWeightage.Text)
    '            obj.Snf_Weightage = clsCommon.myCdbl(TxtSNFWeightage.Text)
    '            obj.Fat_Ratio = clsCommon.myCdbl(txtfatRatio.Text)
    '            obj.Snf_Ratio = clsCommon.myCdbl(txtSNFRatio.Text)
    '            ''-------------------------

    '            obj.arrDispatchDetailBulkSale = New List(Of clsDispatchDetailBulkSale)

    '            For Each grow As GridViewRowInfo In gv1.Rows
    '                objTr = New clsDispatchDetailBulkSale()
    '                objTr.Document_No = clsCommon.myCstr(obj.Document_No)
    '                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
    '                objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
    '                objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)

    '                objTr.FatPer = clsCommon.myCdbl(grow.Cells(colFatPer).Value)
    '                objTr.SNFPer = clsCommon.myCdbl(grow.Cells(colSNFPer).Value)
    '                objTr.CLR = clsCommon.myCdbl(grow.Cells(colCLR).Value)
    '                objTr.Fat_KG = clsCommon.myCdbl(grow.Cells(colFatKG).Value)
    '                objTr.SNF_KG = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
    '                objTr.FatAmount = clsCommon.myCdbl(grow.Cells(ColFatAmount).Value)

    '                objTr.SNFAmount = clsCommon.myCdbl(grow.Cells(ColSNFAmount).Value)
    '                objTr.NetMilkRate = clsCommon.myCdbl(grow.Cells(colNetMilkRate).Value)
    '                objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
    '                objTr.FatRate = clsCommon.myCdbl(grow.Cells(colFatRate).Value)
    '                objTr.SNFRate = clsCommon.myCdbl(grow.Cells(colSNFRate).Value)
    '                If ApplyTSPriceAtBulkSale = True Then
    '                    objTr.StandardRate = clsCommon.myCdbl(grow.Cells(colStandardRate).Value)
    '                Else
    '                    objTr.StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)
    '                End If
    '                objTr.Type = clsCommon.myCstr(grow.Cells(colType).Value)
    '                objTr.Seal_No = clsCommon.myCstr(grow.Cells(colChamberSeal).Value)
    '                objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
    '                'objTr.s
    '                obj.arrDispatchDetailBulkSale.Add(objTr)
    '            Next


    '            If (ClsDispatchBulkSale.SaveData(obj, isNewEntry)) Then
    '                UcAttachment1.SaveData(obj.Document_No)
    '                'If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select Approved from TSPL_Dispatch_BulkSale where Document_No='" + txtDocNo.Value + "'"), "Y") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(txtCreditLimit.Value) > 0 Then
    '                '    Dim strQuery As String = ""
    '                '    trans = clsDBFuncationality.GetTransactin()
    '                '    strQuery = "update TSPL_CUSTOMER_MASTER set Credit_Limit=Credit_Limit+" + txtCreditLimit.Text + " where Cust_Code='" + fndCustomerNo.Value + "'"
    '                '    clsDBFuncationality.ExecuteNonQuery(strQuery, trans)
    '                '    trans.Commit()
    '                'End If
    '                If Not isFlag Then

    '                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
    '                    txtDocNo.Value = obj.Document_No
    '                    ''done by stuti approval work 01/12/2016
    '                    If AllowNLevel Then
    '                        objApproval = New clsApply_Approval()
    '                        objApproval.DocCode = txtDocNo.Value
    '                        objApproval.TotAmt = lblTotRAmt1.Text
    '                        objApproval.CustCode = lblCustomerCode.Text
    '                        clsApply_Approval.CheckApprovalRequired(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(obj.Document_No), txtDate.Text, "", "", clsCommon.myCdbl(lblTotRAmt1.Text), clsCommon.myCdbl(totalqty), "", objApproval)
    '                    End If

    '                    LoadData(obj.Document_No, NavigatorType.Current)

    '                End If

    '            End If
    '        End If
    '    Catch ex As Exception
    '        '            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '        Throw New Exception(ex.Message)
    '    Finally
    '        obj = Nothing
    '        objTr = Nothing
    '    End Try
    'End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim dt As DataTable = Nothing
        Dim obj As ClsCanSale = Nothing
        Try
            obj = ClsCanSale.GetData(strCode, arrLoc, NavTyep)
            isInsideLoadData = True
            If obj IsNot Nothing Then
                isNewEntry = False
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                lblCustomerCode.Text = obj.Customer_Code
                lblCustomerName.Text = obj.Customer_Name
                lblLocationCode.Text = obj.Location_Code
                LblLocationName.Text = obj.Location_Name
                FndPriceCode.Value = obj.Price_Code
                txtStanadardrate.Value = obj.Standard_Rate
                TxtToleranceinplus.Value = obj.TolerancePerPlus
                txtToleranceinminus.Value = obj.TolerancePerMinus
                TxtFatWeightage.Text = obj.Fat_Weightage
                TxtSNFWeightage.Text = obj.Snf_Weightage
                txtfatRatio.Text = obj.Fat_Ratio
                txtSNFRatio.Text = obj.Snf_Ratio
                lblDispatchNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select CanSale_Dispatch_No from TSPL_CANSALE_INVOICE_HEAD   where CanSale_Doc_No  ='" & txtDocNo.Value & "'"))
                lblInvoiceNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_No from TSPL_CANSALE_INVOICE_HEAD   where CanSale_Doc_No  ='" & txtDocNo.Value & "'"))
                ChkCanInventoryType.Checked = IIf(obj.CanInventoryType = 1, True, False)
                lblTotNoofCans.Text = obj.TotalNoofCans

                txtTaxGroup.Value = obj.Tax_Group
                Dim objTaxGrpMaster As New clsTaxGroupMaster()
                objTaxGrpMaster = objTaxGrpMaster.GetDataForSale(obj.Tax_Group)
                If (objTaxGrpMaster IsNot Nothing) Then
                    lblTaxGrpName.Text = objTaxGrpMaster.Tax_Group_Desc
                End If
                LoadBlankGridTax()

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

                If obj.arrCanSaleDetail IsNot Nothing AndAlso obj.arrCanSaleDetail.Count > 0 Then
                    loadBlankItemGrid()
                    For Each objTr As ClsCanSaleDetail In obj.arrCanSaleDetail
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.ItemCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = objTr.ItemName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfCans).Value = objTr.NoOfCans
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatPer).Value = objTr.FatPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNFPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value = objTr.Fat_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = objTr.SNF_KG
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkRate).Value = objTr.MilkRate

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkAmount).Value = objTr.MilkAmt
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPriceRate).Value = objTr.PriceRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatRate).Value = objTr.FatRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFRate).Value = objTr.SNFRate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFatAmount).Value = objTr.FatAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFAmount).Value = objTr.SNFAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTotalAmount).Value = objTr.TotalAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDifference).Value = objTr.Diff

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

                        gv1.Rows.AddNew()
                    Next
                    gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                Else
                    gv1.DataSource = Nothing
                End If

                txtDocNo.MyReadOnly = True
                lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(obj.ActualTCSBaseAmount)
                txttcstaxbaseamount.Value = clsCommon.myCdbl(obj.ChangedTCSBaseAmount)
                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblDocumentAmount.Text = clsCommon.myFormat(obj.DocumentAmount)
                lblTotRAmt1.Text = obj.Total_Amt

                If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                    btnPost.Enabled = False
                    btndelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    btnPost.Enabled = True
                    btndelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending

                    If TCSTaxApplicableOnCanSale = True Then
                        If clsCommon.myLen(txtTaxGroup.Value) <= 0 Then
                            If clsCommon.myLen(lblLocationCode.Text) > 0 Then
                                txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, lblLocationCode.Text, lblCustomerCode.Text, "S", txtDate.Value)
                                lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
                                SetTaxDetails()
                            Else
                                Throw New Exception("Please select Document First")
                            End If
                        End If
                    Else
                        txtTaxGroup.Value = ""
                    End If

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


    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_CAN_SALE_HEAD where Document_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
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
        Dim qry As String = "Select TSPL_CAN_SALE_HEAD.Document_No as Code,Convert(varchar,TSPL_CAN_SALE_HEAD.Document_Date,103) as [Dispatch Date],TSPL_CAN_SALE_HEAD.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CAN_SALE_HEAD.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_CAN_SALE_HEAD.Price_Code as [Price Code],case when TSPL_CAN_SALE_HEAD.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_CAN_SALE_HEAD left outer Join TSPL_CUSTOMER_MASTER on TSPL_CAN_SALE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_CAN_SALE_HEAD.Location_Code =TSPL_LOCATION_MASTER.Location_Code"
        txtDocNo.Value = clsCommon.ShowSelectForm("CanSale", qry, "Code", " TSPL_CAN_SALE_HEAD.Location_Code in (" + arrLoc + ")", txtDocNo.Value, "", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub
    Sub Reset()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        lblLocationCode.Text = ""
        LblLocationName.Text = ""
        lblCustomerCode.Text = ""
        lblCustomerName.Text = ""
        FndPriceCode.Value = ""
        lblDispatchNo.Text = ""
        lblInvoiceNo.Text = ""
        TxtFatWeightage.Value = 0
        TxtSNFWeightage.Value = 0
        txtfatRatio.Value = 0
        txtSNFRatio.Value = 0
        txtStanadardrate.Value = 0
        TxtToleranceinplus.Value = 0
        txtToleranceinminus.Value = 0
        lblTotRAmt1.Text = ""
        lblTotNoofCans.Text = ""
        ChkCanInventoryType.Checked = False
        btnPost.Enabled = True
        UcAttachment1.BlankAllControls()
        loadBlankItemGrid()
        ReStoreGridLayout()
        LOCATIONRIGTHS()
        LoadBlankGridTax()
        If TCSTaxApplicableOnCanSale Then
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
    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then

                If (ClsCanSale.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                txtDate.Select()
                Return False
            End If

            Dim dispatchqty As Double = 0
            For i As Integer = 0 To gv1.Rows.Count - 1
                dispatchqty += clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
            Next
            Dim CurBal As Double = 0
            Dim CurBalOFVL As Double = 0
            Dim CurBalOFML As Double = 0
            ''Dim dispatchqty As Double = clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
            '' Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(lblLocationCode.Text) & "' and Location_Type='Physical'"))
            ''Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Location_Code  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code   WHERE  (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Loc_Segment_Code='" & clsCommon.myCstr(lblLocationCode.Text) & "' and Location_Type='Physical'"))
            'Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Location_Code  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code   WHERE  (isnull(is_sub_location,'N')='Y' or isnull(Is_Section,'N')='Y') and Loc_Segment_Code='" & clsCommon.myCstr(lblLocationCode.Text) & "' "))
            'If clsCommon.myLen(strsublocation) > 0 Then
            '    CurBalOFVL = clsCommon.myCdbl(ClsLoadingTanker.getBalance(clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value), lblLocationCode.Text, strsublocation, txtDocNo.Value, txtDate.Value, Nothing, "KG"))
            'End If
            'CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value), lblLocationCode.Text, "", txtDocNo.Value, txtDate.Value, Nothing, "KG"))
            'CurBal = CurBalOFVL + CurBalOFML


            'If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
            '    If CurBal > 0 Then
            '        CurBal = ClsLoadingTanker.GetTolerane(CurBal, clsCommon.myCdbl(dispatchqty))
            '    End If
            'End If

            'If CurBal < clsCommon.myCdbl(dispatchqty) Then
            '    Throw New Exception("Available Qty is :     " & CurBal & Environment.NewLine & "Required Qty :     " & dispatchqty & " ")
            'End If

            ''--------------------------  31 jULY,
            ''richa agarwal 02/01/2018
            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowtoNegativeStockInventoryAtTankerDispatch, clsFixedParameterCode.AllowtoNegativeStockInventoryAtTankerDispatch, Nothing), "0") = CompairStringResult.Equal Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(lblLocationCode.Text) & "' order by TSPL_Location_MASTER.Location_Code ")
                Dim strsublocation As String = String.Empty
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        strsublocation = clsCommon.myCstr(dt.Rows(i)("Code"))
                        If clsCommon.myLen(strsublocation) > 0 Then
                            CurBalOFVL = CurBalOFVL + clsCommon.myCdbl(ClsLoadingTanker.getBalance(clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value), lblLocationCode.Text, strsublocation, txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.Rows(0).Cells(colUOM).Value)))
                        End If
                    Next
                End If


                CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value), lblLocationCode.Text, "", txtDocNo.Value, txtDate.Value, Nothing, clsCommon.myCstr(gv1.Rows(0).Cells(colUOM).Value)))
                CurBal = CurBalOFVL + CurBalOFML


                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                    If CurBal > 0 Then
                        CurBal = ClsLoadingTanker.GetTolerane(CurBal, clsCommon.myCdbl(dispatchqty))
                    End If
                End If

                If CurBal < clsCommon.myCdbl(dispatchqty) Then
                    Throw New Exception("Available Qty is :     " & CurBal & Environment.NewLine & "Required Qty :     " & dispatchqty & " ")
                End If
            End If
            ''------------------------
            If AllowtoChangeTCSBaseAmount Then
                If clsCommon.myCdbl(txttcstaxbaseamount.Value) > clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text) Then
                    Throw New Exception("TCS Tax Base amount should not be greater than Actual TCS Tax Base Amount")
                End If
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function


    Sub PostData()
        Dim msg As String = Nothing
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing

        Dim desc As String = Nothing
        Try
            If AllowToSave() Then
                isFlag = True
                If (myMessages.postConfirm()) Then
                    If TCSTaxApplicableOnCanSale = True Then
                        If clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                            SaveData()
                        End If
                    End If
                    If (ClsCanSale.PostData(MyBase.Form_ID, arrLoc, txtDocNo.Value)) Then
                        msg = "Successfully posted"
                        common.clsCommon.MyMessageBoxShow(msg)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If

                    End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isFlag = False
            msg = Nothing
            qry = Nothing
            dt = Nothing
            desc = Nothing
        End Try
    End Sub
    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
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

    Private Sub BtnPrintInvoice_Click(sender As Object, e As EventArgs) Handles BtnPrintInvoice.Click
        Dim Qry As String = LoadPrintQuery(txtDocNo.Value)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptCANSaleInvoice", "Fresh Invoice Statement", txtDate.Value, "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
            frmCRV = Nothing
        End If
    End Sub
    Public Function LoadPrintQuery(ByVal strinvoiceNo) As String
        Dim Qry As String = Nothing
        Dim Bank_Name As String = ""
        Dim IFSC_Code As String = ""
        Dim BANKACCNUMBER As String = ""

        Qry = "select isnull(DESCRIPTION,'') as Bank_Name,isnull(IFSC_Code,'') as IFSC_Code,isnull(BANKACCNUMBER,'') as BANKACCNUMBER from TSPL_BANK_MASTER" & _
        " left outer join TSPL_BANK_BRANCH_MASTER on TSPL_BANK_MASTER.BANK_CODE=TSPL_BANK_BRANCH_MASTER.Bank_CODE " & _
        " where Default_Bank = 1 "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        If dt.Rows.Count > 0 Then
            Bank_Name = clsCommon.myCstr(dt.Rows(0).Item("Bank_Name"))
            IFSC_Code = clsCommon.myCstr(dt.Rows(0).Item("IFSC_Code"))
            BANKACCNUMBER = clsCommon.myCstr(dt.Rows(0).Item("BANKACCNUMBER"))
        End If
        '=================Added by preeti Against Ticket No[ERO/31/05/18-000331,ERO/18/06/18-000348]====
        Qry = "select TSPL_CANSALE_invoice_HEAD.Total_Amt ,TSPL_CANSALE_invoice_HEAD.TAX1,TSPL_CANSALE_invoice_HEAD.Tax1_Rate,TSPL_CANSALE_invoice_HEAD.Tax1_Amt, TSPL_CANSALE_invoice_HEAD.TAX2,TSPL_CANSALE_invoice_HEAD.Tax2_Rate,TSPL_CANSALE_invoice_HEAD.Tax2_Amt,TSPL_CANSALE_invoice_HEAD.TAX3,TSPL_CANSALE_invoice_HEAD.Tax3_Rate,TSPL_CANSALE_invoice_HEAD.Tax3_Amt, TSPL_CANSALE_invoice_HEAD.TAX4,TSPL_CANSALE_invoice_HEAD.Tax4_Rate,TSPL_CANSALE_invoice_HEAD.Tax4_Amt , TSPL_CANSALE_invoice_HEAD.TAX5,TSPL_CANSALE_invoice_HEAD.Tax5_Rate,TSPL_CANSALE_invoice_HEAD.Tax5_Amt, '" + Bank_Name + "' as Bank_Name,'" + IFSC_Code + "' as IFSC_Code,'" + BANKACCNUMBER + "' as BANKACCNUMBER" + ", tspl_company_master.CINNo as Comp_CINNo,tspl_company_master.Pan_No  as Com_PAN_No,TSPL_CANSALE_invoice_HEAD.document_no, TSPL_CANSALE_invoice_HEAD.document_date, TSPL_CANSALE_invoice_HEAD.CanSale_Doc_No" &
                " ,TSPL_CANSALE_invoice_HEAD.Customer_Code,TSPL_CANSALE_invoice_HEAD.Location_Code,TSPL_CANSALE_invoice_HEAD.price_Code " &
                " ,TSPL_CANSALE_invoice_DETAIL.Itemcode,TSPL_CANSALE_invoice_DETAIL.NoOfCans,TSPL_CANSALE_invoice_DETAIL.Qty,TSPL_CANSALE_invoice_DETAIL.uom,TSPL_CANSALE_invoice_DETAIL.milkrate,TSPL_CANSALE_invoice_DETAIL.TotalAmount,TSPL_CANSALE_invoice_HEAD.DocumentAmount" &
                " ,TSPL_LOCATION_MASTER.Add1  as Loc_ADd1,TSPL_LOCATION_MASTER.Add2  as LOC_ADD2,TSPL_LOCATION_MASTER.Add3 as LOC_ADD3, Loc_State.State_Name as LocationState, case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as LOCPhone,TSPL_LOCATION_MASTER.TIN_No as Loc_TIN_NO,Loc_State.gst_state_code,tspl_location_master.gstno as LocGstNo,tspl_company_master.Access_Officer as FSSAI" &
                " ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Add1 as cust_add1 ,TSPL_CUSTOMER_MASTER. Add2 as cust_add2 ,TSPL_CUSTOMER_MASTER.Add3 cust_add3,isnull(TSPL_CUSTOMER_MASTER.pin_Code,'') as Cust_Pin,Tspl_customer_master.gstno as CustGSTNo,CUSTOMER_STATE_MASTER.GST_STATE_Code AS Cust_Gst_StateCode,TSPL_CUSTOMER_MASTER.pan as Customer_Pan,isnull(TSPL_CUSTOMER_MASTER.FSSAI_NO,'') as Cust_FSSAI_LIC_NO" &
                 " ,case when ISNULL(TSPL_CUSTOMER_MASTER.Phone1,'')='(+__)__________' then ''  " &
                " else TSPL_CUSTOMER_MASTER.Phone1 end +  Case When ISNULL(TSPL_CUSTOMER_MASTER.Phone2,'')<>'(+__)__________' Then ', '+  TSPL_CUSTOMER_MASTER.Phone2 Else'' End as CustPhone " &
                "  ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Add1 as comp_add1 ,TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone , TSPL_COMPANY_MASTER.Tin_No as comp_tinNo,TSPL_COMPANY_MASTER.Logo_Img,tspl_item_Master.item_desc,tspl_item_Master.HSN_Code" &
                   " ,customer_city_master.city_Name as Cust_City_Name,CUSTOMER_STATE_MASTER.state_Name as Cust_State_Name from TSPL_CANSALE_invoice_HEAD" &
                    " left join TSPL_CANSALE_invoice_DETAIL on TSPL_CANSALE_invoice_DETAIL.document_no=TSPL_CANSALE_invoice_HEAD.document_no" &
                  " left join tspl_location_master on tspl_location_master.location_code= TSPL_CANSALE_invoice_HEAD.location_code" &
                 " LEFT OUTER JOIN TSPL_STATE_MASTER as Loc_State On Loc_State.State_Code=tspl_location_master.State " &
                " left join tspl_customer_master on tspl_customer_master.cust_code=TSPL_CANSALE_invoice_HEAD.Customer_Code" &
                " left outer join TSPL_CITY_MASTER as customer_city_master on TSPL_CUSTOMER_MASTER.city_code=customer_city_master.City_Code  " &
                " left join TSPL_STATE_MASTER as CUSTOMER_STATE_MASTER on TSPL_CUSTOMER_MASTER.State= CUSTOMER_STATE_MASTER.STATE_CODE " &
                " left join tspl_company_master on tspl_company_master.comp_code=TSPL_CANSALE_invoice_HEAD.comp_code" &
                " left join tspl_item_master on tspl_item_master.item_Code=TSPL_CANSALE_invoice_DETAIL.itemCode" &
               " where 2=2 and TSPL_CANSALE_invoice_HEAD.CanSale_Doc_No='" + strinvoiceNo + "'"
        Return Qry
    End Function

    ' Ticket No : BHA/12/11/18-000674 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(lblDispatchNo.Text)
    End Sub
    'Ticket No  TEC/10/09/19-001005 
    Private Sub btnInvoiceJE_Click(sender As Object, e As EventArgs) Handles btnInvoiceJE.Click
        If clsCommon.myLen(lblInvoiceNo.Text) > 0 Then
            clsOpenJEAgainstInvoice.ShowInvoiceJE(lblInvoiceNo.Text)
        End If
    End Sub
    '' TCS tax impact

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Try
            If TCSTaxApplicableOnCanSale = True Then
                If clsCommon.myLen(lblLocationCode.Text) > 0 Then
                    Dim strCustomer As String = ""
                    If clsCommon.myLen(strCustomer) <= 0 Then
                        strCustomer = lblCustomerCode.Text
                    End If

                    txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, lblLocationCode.Text, strCustomer, "S", txtDate.Value)
                    lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)


                    SetTaxDetails()

                Else
                    Throw New Exception("Please select Document First")
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
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", lblCustomerCode.Text, lblLocationCode.Text, True)
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
                    '' gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = dr("TaxRate")
                    ''richa 26 oct,2020
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & lblCustomerCode.Text & "'")), "0") = CompairStringResult.Equal Then
                            If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
                                Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsCustomerMaster.GetCustomerOutstandingForTCSTaxApplicableOnFY(lblCustomerCode.Text, txtDate.Value))
                                If dblOutstandingAmount < AmountToCheckCustomerOutstandingForTCSTax Then
                                    dblOutstandingAmount = dblOutstandingAmount + clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text))
                                    If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                                        If clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text)) > 0 Then
                                            txttcstaxbaseamount.Value = clsCommon.myCdbl(dblOutstandingAmount - AmountToCheckCustomerOutstandingForTCSTax)
                                        End If
                                    End If
                                End If

                                If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                                    Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & lblCustomerCode.Text & "'"))
                                    If clsCommon.myLen(panno) > 0 Then
                                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                    Else
                                        gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
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
        Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsByLocation(txtTaxGroup.Value, "S", lblCustomerCode.Text, lblLocationCode.Text, True)
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
                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = dr("TaxRate")
                            End If
                            ''tcs tax rate
                            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Is_TCS  from tspl_tax_master where tax_code ='" & clsCommon.myCstr(dr("Tax_Code")) & "' ")), "Y") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(IsTCSnotApplicable ,0) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & lblCustomerCode.Text & "'")), "0") = CompairStringResult.Equal Then
                                    If AmountToCheckCustomerOutstandingForTCSTax > 0 Then
                                        Dim dblOutstandingAmount As Double = clsCommon.myCdbl(clsCustomerMaster.GetCustomerOutstandingForTCSTaxApplicableOnFY(lblCustomerCode.Text, txtDate.Value))
                                        If dblOutstandingAmount < AmountToCheckCustomerOutstandingForTCSTax Then
                                            dblOutstandingAmount = dblOutstandingAmount + clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text))
                                            If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                                                If clsCommon.myCdbl(clsCommon.myFormat(lblActualTCSTaxBaseAmt.Text)) > 0 Then
                                                    txttcstaxbaseamount.Value = clsCommon.myCdbl(dblOutstandingAmount - AmountToCheckCustomerOutstandingForTCSTax)
                                                End If
                                            End If
                                        End If
                                        If dblOutstandingAmount > AmountToCheckCustomerOutstandingForTCSTax Then
                                            Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & lblCustomerCode.Text & "'"))
                                            If clsCommon.myLen(panno) > 0 Then
                                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                            Else
                                                gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
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
                            'If clsCommon.myCdbl(txttcstaxbaseamount.Value) <= 0 Then
                            '    txttcstaxbaseamount.Value = 1
                            '    txttcstaxbaseamount.Value = 0
                            'End If

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


    Private Sub lblDocumentAmount_TextChanged(sender As Object, e As EventArgs) Handles lblDocumentAmount.TextChanged
        Try
            If TCSTaxApplicableOnCanSale Then
                If clsCommon.myLen(txtTaxGroup.Value) > 0 Then
                    lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(lblDocumentAmount.Text)
                    SetTaxDetails()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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

    Sub SaveData()

        Dim totalqty As Decimal = 0
        Dim obj As New ClsCanSale()
        Dim objTr As New ClsCanSaleDetail()
        Try
            ''If AllowToSave() Then
            obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Customer_Code = lblCustomerCode.Text
                obj.Customer_Name = lblCustomerName.Text
                obj.Location_Code = lblLocationCode.Text
                obj.Location_Name = LblLocationName.Text
                obj.Price_Code = clsCommon.myCstr(FndPriceCode.Value)
                obj.Fat_Weightage = clsCommon.myCdbl(TxtFatWeightage.Value)
                obj.Snf_Weightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
                obj.Fat_Ratio = clsCommon.myCdbl(txtfatRatio.Value)
                obj.Snf_Ratio = clsCommon.myCdbl(txtSNFRatio.Value)
                obj.Standard_Rate = clsCommon.myCdbl(txtStanadardrate.Value)
                obj.TolerancePerPlus = clsCommon.myCdbl(TxtToleranceinplus.Value)
                obj.TolerancePerMinus = clsCommon.myCdbl(txtToleranceinminus.Value)
                obj.CanInventoryType = IIf(ChkCanInventoryType.Checked = True, "1", "0")
                If ChkCanInventoryType.Checked = True Then
                    obj.CanItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code from tspl_item_master where ISNULL(CAN,'0')<>0"))
                    obj.CanItemRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCanRate, clsFixedParameterCode.ItemCanRate, Nothing))
                    obj.CanItemUOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_Code from tspl_item_master where ISNULL(CAN,'0')<>0"))
                End If
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
                obj.DocumentAmount = lblDocumentAmount.Text
                obj.Total_Tax_Amt = lblTaxAmt.Text
                obj.Total_Amt = lblTotRAmt1.Text
                obj.ActualTCSBaseAmount = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                obj.ChangedTCSBaseAmount = clsCommon.myCdbl(txttcstaxbaseamount.Value)
                obj.TotalNoofCans = clsCommon.myCdbl(lblTotNoofCans.Text)
                obj.arrCanSaleDetail = New List(Of ClsCanSaleDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New ClsCanSaleDetail()
                    objTr.Document_No = clsCommon.myCstr(obj.Document_No)

                    objTr.NoOfCans = clsCommon.myCdbl(grow.Cells(colNoOfCans).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.ItemCode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.ItemName = clsCommon.myCstr(grow.Cells(colItemName).Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                    objTr.FatPer = clsCommon.myCdbl(grow.Cells(colFatPer).Value)
                    objTr.SNFPer = clsCommon.myCdbl(grow.Cells(colSNFPer).Value)
                    objTr.Fat_KG = clsCommon.myCdbl(grow.Cells(colFatKG).Value)
                    objTr.SNF_KG = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                    objTr.MilkRate = clsCommon.myCdbl(grow.Cells(colMilkRate).Value)
                    objTr.MilkAmt = clsCommon.myCdbl(grow.Cells(colMilkAmount).Value)
                    objTr.PriceRate = clsCommon.myCdbl(grow.Cells(colPriceRate).Value)
                    objTr.FatRate = clsCommon.myCdbl(grow.Cells(colFatRate).Value)
                    objTr.SNFRate = clsCommon.myCdbl(grow.Cells(colSNFRate).Value)
                    objTr.FatAmount = clsCommon.myCdbl(grow.Cells(ColFatAmount).Value)
                    objTr.SNFAmount = clsCommon.myCdbl(grow.Cells(ColSNFAmount).Value)
                    objTr.TotalAmount = clsCommon.myCdbl(grow.Cells(colTotalAmount).Value)
                    objTr.Diff = clsCommon.myCdbl(grow.Cells(colDifference).Value)

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
                    obj.arrCanSaleDetail.Add(objTr)
                Next

                If (ClsCanSale.SaveData(obj, False)) Then

            End If
            ''End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
            objTr = Nothing
        End Try
    End Sub

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

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim Strii As String = clsCommon.myCstr(ii + 1)
                If (clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0) Then
                    dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colTotalAmount).Value)

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
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Try
            Dim arrTaxableAuth As New List(Of String)
            Dim Amount As Double = 0
            Dim dblAmtAfterDis As Double = 0

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
                        dblAmtAfterDis = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMilkAmount).Value)
                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0
                        If IsSurTax Then
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            If Not IsTaxonBaseAmount Then
                                dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                            End If
                            If Not IsTaxonBaseAmount AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                                Dim dblTotalBasicPrice As Double = 0
                                For n As Integer = 0 To gv1.Rows.Count - 1
                                    If clsCommon.myLen(gv1.Rows(n).Cells(colItemCode).Value) > 0 Then
                                        dblTotalBasicPrice = dblTotalBasicPrice + clsCommon.myCdbl(gv1.Rows(n).Cells(colMilkAmount).Value)
                                    End If
                                Next
                                If dblTotalBasicPrice > 0 Then
                                    dblBaseAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMilkAmount).Value) * clsCommon.myCdbl(txttcstaxbaseamount.Value)) / dblTotalBasicPrice
                                Else
                                    dblBaseAmt = 0
                                End If
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
                        Dim dblCurrRowAmt As Double = clsCommon.myCdbl(gv1.Rows(clsCommon.myCdbl(IntRowNo)).Cells(colMilkAmount).Value)
                        Dim dblTotAmt As Double = 0
                        For jj As Integer = 0 To gv1.Rows.Count - 1
                            dblTotAmt += clsCommon.myCdbl(gv1.Rows(jj).Cells(colMilkAmount).Value)
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
End Class
