
'--------Created By Richa 01/08/2014 Against Ticket No BM00000003248
''-------------updation by richa Against Ticket no BM00000003775 and BM00000003618,ERO/25/02/19-000499 (all work done on report not on code) 27 Feb,2019
'' updation by Richa Agarwal Against Ticket no. BM00000003854,BM00000003770,BM00000005061,BM00000005658,BM00000008256
'' Updation By Richa Agarwal Against Ticket No. BM00000004014 on 15/09/2014,BM00000004772,BM00000005027,BM00000005226,BM00000005218,BM00000005256
'Sanjay Ticket No- ERO/11/07/18-000367  Add Qty in Ltr in grid
Imports System.Data.SqlClient
Imports common
Imports System.IO


Public Class FrmDispatchBulkSale
    Inherits FrmMainTranScreen
#Region "Variables"
    Public AllowModifcationByApprovalUser As Boolean = False
    Dim AmountToCheckCustomerOutstandingForTCSTax As Double = 0
    Dim EnableTCSRateValidityFrom01July2021 As Boolean = False
    Dim AllowFatPerInanynumberofMultipesonBulkQC As Boolean = False
    Dim EnterInsuranceNoandSealNo As Boolean = False
    Dim TCSTaxApplicableOnbulkSale As Boolean = False
    Dim ApplyMultiChamber As Boolean = False
    Dim AllowNLevel As Boolean = False
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Public AllowtoChangeTCSBaseAmount As Boolean = False
    Dim isFlag As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colHSNCode As String = "HSNCode"
    Public Const colChamberDesc As String = "ChamberDesc"
    Public Const colChamberSeal As String = "ChamberSeal"
    Public Const colType As String = "Type"
    Public Const colUnitCode As String = "colUnitCode"
    Public Const colSOUnitCode As String = "colSOUnitCode"
    Public Const colQty As String = "Qty"
    Public Const colSOQty As String = "SOQty"
    Public Const colQtyLtr As String = "colQtyLtr"
    Public Const colAmount As String = "colAmount"
    Public Const colFatPer As String = "colFatPer"
    Public Const colSNFPer As String = "colSNFPer"
    Public Const colCLR As String = "colCLR"
    Public Const colNetMilkRate As String = "colNetMilkRate"
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
    Public arrList As List(Of String) = Nothing
    Public arrListDesc As List(Of String) = Nothing
    Public Shared Alocation As String = Nothing
    Dim ApplyTSPriceAtBulkSale As Boolean = False
    Dim AllowSNFNotManditoryInBulkSale As Boolean = False
    Dim ShowBulkDispatchQtyInLtr As Boolean = False
    Dim UseKGLitreConversionInBulkSaleAsperCLRCalculation As Boolean = False
    Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
    Dim CreateInvoiceAutomaticallyOnbulkDispatch As Boolean = False
    Public DocumentNo As String = ""
    Dim Qry As String
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmDispatchBulkSale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        ElseIf e.Alt AndAlso e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F6 Then
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If btnPost.Enabled = False Then
                    fndCustomerNo.Visible = True
                    fndCustomerNo.Enabled = True
                    btnUpdateCustomer.Visible = True
                    btnUpdateCustomer.Enabled = True
                Else
                    clsCommon.MyMessageBoxShow("Please post the dispatch first")
                End If
            Else
                clsCommon.MyMessageBoxShow("Please select Dispatch No first")
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverseAndUnpost.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub FrmDispatchBulkSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AllowNLevel = clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.FrmDispatchBulkSale)
        EnterInsuranceNoandSealNo = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.InsuranceNoAndSealNoInBulkDispatch, clsFixedParameterCode.InsuranceNoAndSealNoInBulkDispatch, Nothing)) = 1, True, False))
        ApplyMultiChamber = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyMultiChamberInBulkWeighmentEntry, clsFixedParameterCode.ApplyMultiChamberInBulkWeighmentEntry, Nothing)) = 1, True, False))
        ApplyTSPriceAtBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, Nothing)) = 1, True, False))
        AllowSNFNotManditoryInBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowSNFNotManditoryInBulkSale, clsFixedParameterCode.AllowSNFNotManditoryInBulkSale, Nothing)) = 1, True, False))
        ShowBulkDispatchQtyInLtr = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowBulkDispatchQtyInLtr, clsFixedParameterCode.ShowBulkDispatchQtyInLtr, Nothing)) = 1, True, False))
        UseKGLitreConversionInBulkSaleAsperCLRCalculation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.UseKGLitreConversionInBulkSaleAsperCLRCalculation, clsFixedParameterCode.UseKGLitreConversionInBulkSaleAsperCLRCalculation, Nothing)) = 1, True, False))
        AllowtoChangeTCSBaseAmount = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowtoChangeTCSBaseAmount, clsFixedParameterCode.AllowtoChangeTCSBaseAmount, Nothing)) = 0, False, True)
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        CreateInvoiceAutomaticallyOnbulkDispatch = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateInvoiceAutomaticallyOnbulkDispatch, clsFixedParameterCode.CreateInvoiceAutomaticallyOnbulkDispatch, Nothing)) = 1, True, False))
        TCSTaxApplicableOnbulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.TCSTaxApplicableOnbulkSale, clsFixedParameterCode.TCSTaxApplicableOnbulkSale, Nothing)) = 1, True, False))
        AmountToCheckCustomerOutstandingForTCSTax = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AmountToCheckCustomerOutstandingForTCSTax, clsFixedParameterCode.AmountToCheckCustomerOutstandingForTCSTax, Nothing))
        EnableTCSRateValidityFrom01July2021 = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableTCSRateValidityFrom01July2021, clsFixedParameterCode.EnableTCSRateValidityFrom01July2021, Nothing)) = 0, False, True)
        AllowFatPerInanynumberofMultipesonBulkQC = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowFatPerInanynumberofMultipesonBulkQC, clsFixedParameterCode.AllowFatPerInanynumberofMultipesonBulkQC, Nothing)) = 1, True, False))
        If EnterInsuranceNoandSealNo Then
            RadPanel1.Visible = True
        Else
            RadPanel1.Visible = False
        End If
        MyLabel15.Text = "Standard Rate"
        If ApplyTSPriceAtBulkSale = True Then
            MyLabel17.Visible = False
            TxtToleranceinplus.Visible = False
            txtToleranceinminus.Visible = False
            MyLabel4.Visible = False
            TxtFatWeightage.Enabled = True
            TxtSNFWeightage.Enabled = True
            txtfatRatio.Enabled = True
            txtSNFRatio.Enabled = True
            ''richa agarwal ERO/10/01/19-000463 ts rate from price chart of bulk
            MyLabel1.Visible = True
            FndPriceCode.Visible = True
            MyLabel5.Visible = True
            MyLabel15.Text = "TS Rate"
            txtStanadardrate.Visible = True
        End If
        SetUserMgmtNew()
        SetMailRight()
        Reset()
        SetMaxlength()
        UcAttachment1.Form_ID = MyBase.Form_ID
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
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
        btnUpdateCustomer.Visible = False
        EmailSmsSetting.Visibility = ElementVisibility.Collapsed
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDispatchBulkSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        'RadMenu1.Visible = MyBase.isExport
        If MyBase.isReverse Then
            btnReverseAndUnpost.Enabled = True
        Else
            btnReverseAndUnpost.Enabled = False
        End If
    End Sub
    Sub SetMaxlength()
        TxtChallanNo.MaxLength = 30
        txtinsuranceno.MaxLength = 30
        txtsealno.MaxLength = 30
        TxtDipMarking.MaxLength = 30
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

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
        arrList = New List(Of String)
        arrListDesc = New List(Of String)
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        Dim repoComboColumn As GridViewComboBoxColumn = Nothing
        Dim repoTextColumn As GridViewTextBoxColumn = Nothing
        Dim repoDecimalColumn As GridViewDecimalColumn = Nothing
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master where IsBulkSale=1 order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

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

        Dim ChamberDesc As New GridViewTextBoxColumn()
        ChamberDesc.FormatString = ""
        ChamberDesc.HeaderText = "Chamber Desc"
        ChamberDesc.Name = colChamberDesc
        ChamberDesc.Width = 320
        ChamberDesc.ReadOnly = True
        ChamberDesc.WrapText = True
        ChamberDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ChamberDesc)

        Dim type As New GridViewTextBoxColumn()
        type.FormatString = ""
        type.HeaderText = "Type"
        type.Name = colType
        type.Width = 320
        type.ReadOnly = True
        type.WrapText = True
        type.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(type)

        Dim ChamberSeal As New GridViewTextBoxColumn()
        ChamberSeal.FormatString = ""
        ChamberSeal.HeaderText = "Chamber Seal No"
        ChamberSeal.Name = colChamberSeal
        ChamberSeal.Width = 320
        ChamberSeal.ReadOnly = False
        ChamberSeal.WrapText = True
        ChamberSeal.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ChamberSeal)

        If ApplyMultiChamber Then
            gv1.Columns(colChamberDesc).IsVisible = True
            gv1.Columns(colChamberDesc).VisibleInColumnChooser = True
            gv1.Columns(colType).IsVisible = True
            gv1.Columns(colType).VisibleInColumnChooser = True
            gv1.Columns(colChamberSeal).IsVisible = True
            gv1.Columns(colChamberSeal).VisibleInColumnChooser = True
        Else
            gv1.Columns(colChamberDesc).IsVisible = False
            gv1.Columns(colChamberDesc).VisibleInColumnChooser = False
            gv1.Columns(colType).IsVisible = False
            gv1.Columns(colType).VisibleInColumnChooser = False
            gv1.Columns(colChamberSeal).IsVisible = False
            gv1.Columns(colChamberSeal).VisibleInColumnChooser = False
        End If

        Dim strUnitCode As New GridViewTextBoxColumn()
        strUnitCode.FormatString = ""
        strUnitCode.HeaderText = "UOM"
        strUnitCode.Name = colUnitCode
        strUnitCode.Width = 120
        strUnitCode.ReadOnly = True
        strUnitCode.WrapText = True
        strUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(strUnitCode)

        Dim strSOUnitCode As New GridViewTextBoxColumn()
        strSOUnitCode.FormatString = ""
        strSOUnitCode.HeaderText = "SO UOM"
        strSOUnitCode.Name = colSOUnitCode
        strSOUnitCode.Width = 120
        strSOUnitCode.ReadOnly = True
        strSOUnitCode.WrapText = True
        strSOUnitCode.VisibleInColumnChooser = True
        strSOUnitCode.IsVisible = False
        strSOUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(strSOUnitCode)

        Dim Qty As New GridViewDecimalColumn
        Qty.FormatString = "{0:n3}"
        Qty.HeaderText = "Qty"
        Qty.DecimalPlaces = 3
        Qty.Name = colQty
        Qty.Width = 120
        If Not ApplyMultiChamber Then
            Qty.ReadOnly = True
        Else
            Qty.ReadOnly = False
        End If

        Qty.WrapText = True
        Qty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Qty)

        Dim SOQty As New GridViewDecimalColumn
        SOQty.FormatString = "{0:n3}"
        SOQty.HeaderText = "SO Qty"
        SOQty.DecimalPlaces = 3
        SOQty.Name = colSOQty
        SOQty.Width = 120
        SOQty.ReadOnly = True
        SOQty.WrapText = True
        SOQty.VisibleInColumnChooser = True
        SOQty.IsVisible = False
        SOQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(SOQty)

        'Sanjay

        Dim QtyInLtr As New GridViewDecimalColumn
        QtyInLtr.FormatString = "{0:n3}"
        QtyInLtr.HeaderText = "Qty(Ltr)"
        QtyInLtr.DecimalPlaces = 3
        QtyInLtr.Name = colQtyLtr
        QtyInLtr.Width = 120
        QtyInLtr.ReadOnly = True
        QtyInLtr.WrapText = True
        QtyInLtr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(QtyInLtr)

        If ShowBulkDispatchQtyInLtr = True Then
            gv1.Columns(colQtyLtr).IsVisible = True
            gv1.Columns(colQtyLtr).VisibleInColumnChooser = True
        Else
            gv1.Columns(colQtyLtr).IsVisible = False
            gv1.Columns(colQtyLtr).VisibleInColumnChooser = False
        End If
        'Sanjay

        Dim fatper As New GridViewDecimalColumn

        If AllowFatPerInanynumberofMultipesonBulkQC Then
            fatper.FormatString = "{0:n3}"
        Else
            fatper.FormatString = "{0:n2}"
        End If

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

        ''richa Against ticket No BM00000003893 on 11/09/2014
        Dim clr As New GridViewDecimalColumn
        clr.FormatString = "{0:n2}"
        clr.HeaderText = "CLR"
        clr.Name = colCLR
        clr.Width = 75
        clr.ReadOnly = True
        clr.WrapText = True
        clr.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(clr)

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
        If ApplyMultiChamber AndAlso dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            For i As Integer = 0 To dt.Rows.Count() - 1
                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") <> CompairStringResult.Equal And clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") <> CompairStringResult.Equal And clsCommon.CompairString(dt.Rows(i)("Type"), "CLR") <> CompairStringResult.Equal And clsCommon.CompairString(dt.Rows(i)("Type"), "CF") <> CompairStringResult.Equal Then
                    arrList.Add(dt.Rows(i)("Code"))
                    arrListDesc.Add(dt.Rows(i)("Description"))
                    If clsCommon.CompairString(dt.Rows(i)("nature"), "R") = CompairStringResult.Equal Then
                        repoDecimalColumn = New GridViewDecimalColumn()
                        repoDecimalColumn.Name = dt.Rows(i)("Code")
                        repoDecimalColumn.Width = 120
                        repoDecimalColumn.HeaderText = dt.Rows(i)("Description")
                        repoDecimalColumn.Tag = dt.Rows(i)("Type")
                        repoDecimalColumn.ReadOnly = True
                        gv1.Columns.Add(repoDecimalColumn)
                    ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "A") = CompairStringResult.Equal Then
                        repoComboColumn = New GridViewComboBoxColumn()
                        repoComboColumn.Name = dt.Rows(i)("Code")
                        repoComboColumn.Width = 120
                        repoComboColumn.HeaderText = dt.Rows(i)("Description")
                        repoComboColumn.Tag = dt.Rows(i)("Type")
                        repoComboColumn.DataSource = OpenParameterValueList(dt.Rows(i)("Code"))
                        repoComboColumn.DisplayMember = "Value"
                        repoComboColumn.ValueMember = "Value"
                        repoComboColumn.ReadOnly = True
                        gv1.Columns.Add(repoComboColumn)
                    ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "B") = CompairStringResult.Equal Then
                        repoComboColumn = New GridViewComboBoxColumn()
                        repoComboColumn.Name = dt.Rows(i)("Code")
                        repoComboColumn.Width = 120
                        repoComboColumn.HeaderText = dt.Rows(i)("Description")
                        repoComboColumn.Tag = dt.Rows(i)("Type")
                        repoComboColumn.DataSource = FillYesNoValue()
                        repoComboColumn.DisplayMember = "Value"
                        repoComboColumn.ValueMember = "Value"
                        repoComboColumn.ReadOnly = True
                        gv1.Columns.Add(repoComboColumn)
                    Else
                        repoTextColumn = New GridViewTextBoxColumn()
                        repoTextColumn.Name = dt.Rows(i)("Code")
                        repoTextColumn.Width = 120
                        repoTextColumn.HeaderText = dt.Rows(i)("Description")
                        repoTextColumn.Tag = dt.Rows(i)("Type")
                        repoTextColumn.ReadOnly = True
                        gv1.Columns.Add(repoDecimalColumn)
                    End If
                End If
            Next
        End If
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

        ''richa Against ticket No BM00000003893 on 11/09/2014
        Dim netmilkRate As New GridViewDecimalColumn
        netmilkRate.FormatString = "{0:n2}"
        netmilkRate.HeaderText = "Net Milk Rate"
        netmilkRate.Name = colNetMilkRate
        netmilkRate.Width = 75
        netmilkRate.ReadOnly = True
        netmilkRate.WrapText = True
        netmilkRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(netmilkRate)

        Dim Amount As New GridViewDecimalColumn
        Amount.FormatString = "{0:n2}"
        Amount.HeaderText = "Amount"
        Amount.Name = colAmount
        Amount.Width = 75
        Amount.ReadOnly = True
        Amount.WrapText = True
        Amount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Amount)

        If ApplyTSPriceAtBulkSale = True Then
            Dim Standardrate As New GridViewDecimalColumn
            Standardrate.FormatString = "{0:n2}"
            Standardrate.HeaderText = "Standard rate"
            Standardrate.Name = colStandardRate
            Standardrate.Width = 75
            Standardrate.ReadOnly = False
            Standardrate.WrapText = True
            Standardrate.IsVisible = False
            Standardrate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gv1.Columns.Add(Standardrate)
        End If


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




        If Not ApplyMultiChamber Then
            gv1.Rows.AddNew()
            gv1.Rows(0).Cells(colSlNo).Value = "1"
        End If

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnReorder = True
        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        lineNo = Nothing
        itemCode = Nothing
        itemDesc = Nothing
        strUnitCode = Nothing
    End Sub

    Function FillYesNoValue() As DataTable
        Dim dt As DataTable
        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "
        dt = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim dt As DataTable
        Dim qry As String = " select '' as value union all  select  Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "
        dt = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Sub Reset()
        If AllowNLevel Then
            btnPost.Visible = MyBase.isPostFlag
        End If
        txtsealno.Text = ""
        rbtnTaxCalAutomatic.IsChecked = True
        LoadBlankGridTax()
        txtTaxGroup.Value = ""
        lblTaxGrpName.Text = ""
        txtinsuranceno.Text = ""
        txtDocNo.Value = ""
        fndCustomerNo.Value = ""
        lblCustomerCode.Text = ""
        lblCustomerName.Text = ""
        FndTankerCode.Value = ""
        TxtChallanNo.Text = ""
        txtewaybilldate.Value = clsCommon.GETSERVERDATE()
        txtewaybilldate.Checked = False
        TxtEWayBillNo.Text = ""
        fndQcNo.Value = ""
        LblQCCode.Text = ""
        TxtDipMarking.Text = ""
        lblLocationCode.Text = ""
        LblLocationName.Text = ""
        TxtTareWeight.Value = 0
        txtGrossWeight.Value = 0
        txtNetWeight.Value = 0
        FndPriceCode.Value = ""
        txtCreditLimit.Value = 0
        UsLock1.Status = ERPTransactionStatus.Pending
        txtCreditLimit.Enabled = False
        lblTotRAmt1.Text = ""
        ''richa agarwal 18/12/2014
        TxtFatWeightage.Value = 0
        TxtSNFWeightage.Value = 0
        txtfatRatio.Value = 0
        txtSNFRatio.Value = 0
        txtStanadardrate.Value = 0
        TxtToleranceinplus.Value = 0
        txtToleranceinminus.Value = 0
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
        ''richa ERO/18/11/19-001115 18 Nov,2019
        If CreateInvoiceAutomaticallyOnbulkDispatch = True Then
            chkCreateAoutoInvoice.Checked = True
        Else
            chkCreateAoutoInvoice.Checked = False
        End If
        UcAttachment1.BlankAllControls()
        loadBlankItemGrid()
        ReStoreGridLayout()
        isNewEntry = True
        LOCATIONRIGTHS()
        FndTankerCode.Enabled = True
        If TCSTaxApplicableOnbulkSale Then
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
    Private Sub FndTankerCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndTankerCode._MYValidating
        Dim strwhrclause As String = String.Empty
        strwhrclause = " TSPL_Quality_Check_BulkSale.Location_Code in (" + arrLoc + ") and TSPL_Quality_Check_BulkSale.QC_No not in (Select QC_Code from TSPL_Dispatch_BulkSale where Document_No<>'" + txtDocNo.Value + "' and TSPL_Dispatch_BulkSale.Location_Code in (" + arrLoc + ") ) and  TSPL_WEIGHMENT_DETAIL_BULKSALE.Posted=1"
        LblQCCode.Text = ClsQualityCheckBulkSale.getTankerFinder(strwhrclause, FndTankerCode.Value, isButtonClicked)
        Qry = "Select Tanker_No from TSPL_Quality_Check_BulkSale where QC_No='" + LblQCCode.Text + "' "
        FndTankerCode.Value = clsDBFuncationality.getSingleValue(Qry)
        lblLocationCode.Text = clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_Quality_Check_BulkSale where QC_No ='" + LblQCCode.Text + "' ")
        LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")
        ''richa agarwal
        lblCustomerCode.Text = clsDBFuncationality.getSingleValue("Select Customer_Code  from TSPL_GATEENTRY_SALE where Document_No = (Select GateEntry_Document_No  from TSPL_Quality_Check_BulkSale where QC_No = '" + LblQCCode.Text + "')")
        If clsCommon.myLen(lblCustomerCode.Text) > 0 Then
            lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + lblCustomerCode.Text + "' ")
        Else
            lblCustomerName.Text = ""
        End If

        isInsideLoadData = True

        If ApplyMultiChamber = True Then
            gv1.Rows.Clear()
            GetTableValue1()
        Else
            GetTableValue()
        End If
        If TCSTaxApplicableOnbulkSale = True Then
            ''richa UDL/13/07/21-001044
            txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, lblLocationCode.Text, lblCustomerCode.Text, "S", txtDate.Value, "Y")
            lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)
            SetTaxDetails()
        Else
            txtTaxGroup.Value = ""
        End If


        isInsideLoadData = False
        strwhrclause = Nothing
    End Sub

    Private Sub GetTableValue()
        Dim dt As DataTable = Nothing
        Dim strQry As String = ""
        Dim rate As Double = 0
        Qry = "Select Tare_Weight,Gross_Weight,Net_Weight  from TSPL_WEIGHMENT_DETAIL_BULKSALE where GateEntry_Document_No=(Select TSPL_Quality_Check_BulkSale.GateEntry_Document_No as GateEntryNo from TSPL_Quality_Check_BulkSale where  TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "')"
        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            txtNetWeight.Value = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
            TxtTareWeight.Value = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
            txtGrossWeight.Value = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
        End If
        Qry = "Select Item_Code,Qty,Fat ,SNF,CLR,Unit_Code  from TSPL_Quality_Check_BulkSale where QC_No='" + LblQCCode.Text + "'"
        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            gv1.Rows(0).Cells(colSlNo).Value = "1"
            gv1.Rows(0).Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            gv1.Rows(0).Cells(colItemDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + dt.Rows(0)("Item_Code") + "' "))
            gv1.Rows(0).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dt.Rows(0)("Item_Code")), Nothing)
            gv1.Rows(0).Cells(colQty).Value = clsCommon.myCdbl(txtNetWeight.Value)
            gv1.Rows(0).Cells(colSOQty).Value = clsCommon.myCdbl(txtNetWeight.Value)
            gv1.Rows(0).Cells(colUnitCode).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            ''richa Against ticket No BM00000003893 on 11/09/2014

            'Sanjay
            If ShowBulkDispatchQtyInLtr = True Then
                ''richa agarwal 8 Jan,2018
                If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                    ''richa ERO/25/02/19-000499
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQtyLtr).Value = Math.Round(clsCommon.myCdbl(clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(gv1.Rows(0).Cells(colQty).Value), clsCommon.myCdbl(dt.Rows(0)("CLR")))), 0)
                Else
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQtyLtr).Value = QtyInLtr(clsCommon.myCstr(dt.Rows(0)("Item_Code")), clsCommon.myCstr(dt.Rows(0)("Unit_Code")), clsCommon.myCdbl(txtNetWeight.Value))
                End If
            End If
            'Sanjay

            gv1.Rows(0).Cells(colFatPer).Value = clsCommon.myCdbl(dt.Rows(0)("Fat"))
            gv1.Rows(0).Cells(colSNFPer).Value = clsCommon.myCdbl(dt.Rows(0)("SNF"))
            gv1.Rows(0).Cells(colCLR).Value = clsCommon.myCdbl(dt.Rows(0)("CLR"))
            If ApplyTSPriceAtBulkSale = True Then
                strQry = "select TSPL_SALES_ORDER_DETAIL_BULKSALE.rate from TSPL_SALES_ORDER_DETAIL_BULKSALE"
                strQry += " left join TSPL_SALES_ORDER_MASTER_BULKSALE on TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No"
                strQry += " left join TSPL_GATEENTRY_SALE on TSPL_GATEENTRY_SALE.Bulk_SO_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No"
                strQry += " left join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.GateEntry_Document_No=TSPL_GATEENTRY_SALE.Document_No"
                strQry += " where TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "' and TSPL_Quality_Check_BulkSale.Tanker_No='" + FndTankerCode.Value + "' and isnull(TSPL_SALES_ORDER_MASTER_BULKSALE.Price_Code,'')='' and TSPL_SALES_ORDER_DETAIL_BULKSALE.Item_Code='" + clsCommon.myCstr(dt.Rows(0)("Item_Code")) + "'"
                rate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colStandardRate).Value = rate
            End If
            Dim strSOUOM As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_SALES_ORDER_DETAIL_BULKSALE.Unit_code,'') from TSPL_SALES_ORDER_DETAIL_BULKSALE left join TSPL_SALES_ORDER_MASTER_BULKSALE on TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No left join TSPL_GATEENTRY_SALE on TSPL_GATEENTRY_SALE.Bulk_SO_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No left join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.GateEntry_Document_No=TSPL_GATEENTRY_SALE.Document_No where TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "' and TSPL_Quality_Check_BulkSale.Tanker_No='" + FndTankerCode.Value + "' and isnull(TSPL_SALES_ORDER_MASTER_BULKSALE.Price_Code,'')='' and TSPL_SALES_ORDER_DETAIL_BULKSALE.Item_Code='" + clsCommon.myCstr(dt.Rows(0)("Item_Code")) + "'"))
            If clsCommon.myLen(strSOUOM) > 0 Then
                gv1.Rows(0).Cells(colSOUnitCode).Value = clsCommon.myCstr(strSOUOM)
            Else
                gv1.Rows(0).Cells(colSOUnitCode).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            End If
            Dim commsnuom As String = gv1.Rows(0).Cells(colSOUnitCode).Value
            Dim weight_uom As String = gv1.Rows(0).Cells(colUnitCode).Value
            If clsCommon.myLen(gv1.Rows(0).Cells(colSOUnitCode).Value) > 0 Then
                ''richa agarwal TEC/28/03/19-000462 add item structure on setting based
                Qry = "select top 1 CF from (select (case when (Container_UOM='" & commsnuom & "' and Contained_UOM='" & weight_uom & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & weight_uom & "' and Contained_UOM='" & commsnuom & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(clsCommon.myCstr(gv1.Rows(0).Cells(colUnitCode).Value), Nothing) + "') " & IIf(ItemStructureMandatoryOnWeightConversion = True, " and isnull(Structure_Code,'') =(select Structure_Code  from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value) & "')", "") & " )aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                Dim Master_Sku As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
                If Master_Sku = 0 Then
                    Master_Sku = 1
                End If
                gv1.Rows(0).Cells(colQty).Value = clsCommon.myCdbl(gv1.Rows(0).Cells(colQty).Value) * Master_Sku
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
        End If
        dt = Nothing
    End Sub

    Private Sub GetTableValue1()
        Dim dt As DataTable = Nothing
        Dim strpivotcol As String = Nothing
        Dim strQry As String = ""
        Dim rate As Double = 0
        Qry = "select min(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Tare_Weight) as Tare_Weight,max(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Gross_Weight) as Gross_Weight,sum(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Net_Weight)as Net_Weight from TSPL_WEIGHMENT_DETAIL_BULKSALE" &
                " left outer join TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS on TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Weighment_No=TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No=(Select TSPL_Quality_Check_BulkSale.GateEntry_Document_No as GateEntryNo from TSPL_Quality_Check_BulkSale where  TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "')" &
                " group by TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.weighment_No"
        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            txtNetWeight.Value = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
            TxtTareWeight.Value = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
            txtGrossWeight.Value = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
        End If

        Qry = "select stuff((select DISTINCT ',[' + TSPL_QC_Parameter_Detail_BulKSALE.Param_Field_Desc+']'  from TSPL_QC_Parameter_Detail_BulKSALE for xml path('')  ),1,1,'')"
        strpivotcol = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry))
        If clsCommon.myLen(strpivotcol) > 0 Then
            Qry = "select Item_Code,Type,Fat,SNF,CLR,Unit_Code,Chamber_Desc,NetWeight," + strpivotcol + " from (select (TSPL_QC_Parameter_Detail_BulKSALE.Item_Code) as Item_Code,(TSPL_ITEM_MASTER.Product_Type) AS Type,(TSPL_QC_Parameter_Detail_BulKSALE.Fat) as Fat ,(TSPL_QC_Parameter_Detail_BulKSALE.SNF) as SNF,(TSPL_QC_Parameter_Detail_BulKSALE.CLR) as CLR,(TSPL_QC_Parameter_Detail_BulKSALE.Unit_Code) as Unit_Code,TSPL_QC_Parameter_Detail_BulKSALE.Chamber_Desc,(xx.Net_Weight) as NetWeight ,TSPL_QC_Parameter_Detail_BulKSALE.Param_Field_Desc,TSPL_QC_Parameter_Detail_BulKSALE.Param_Field_Value" &
                    " from TSPL_Quality_Check_BulkSale  left outer join TSPL_QC_Parameter_Detail_BulKSALE on TSPL_Quality_Check_BulkSale.QC_No=TSPL_QC_Parameter_Detail_BulKSALE.QC_No left outer join (select (TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Tare_Weight) as Tare_Weight,(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Gross_Weight) as Gross_Weight,(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Net_Weight)as Net_Weight,TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Chamber_Desc from TSPL_WEIGHMENT_DETAIL_BULKSALE  left outer join TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS on TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Weighment_No=TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No=(Select TSPL_Quality_Check_BulkSale.GateEntry_Document_No as GateEntryNo from TSPL_Quality_Check_BulkSale where  TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "')) as xx on xx.Chamber_Desc=TSPL_QC_Parameter_Detail_BulKSALE.Chamber_Desc left outer join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_QC_Parameter_Detail_BulKSALE.Item_Code where TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "' ) as st " &
                    " PIVOT ( MAX(PARAM_FIELD_VALUE) FOR PARAM_FIELD_DESC IN (" + strpivotcol + ")) AS PT ORDER BY Chamber_Desc"
        Else
            Qry = "select max(TSPL_QC_Parameter_Detail_BulKSALE.Item_Code) as Item_Code,MAX(TSPL_ITEM_MASTER.Product_Type) AS Type,max(TSPL_Quality_Check_BulkSale.Qty) as Qty,max(TSPL_QC_Parameter_Detail_BulKSALE.Fat) as Fat ,max(TSPL_QC_Parameter_Detail_BulKSALE.SNF) as SNF,max(TSPL_QC_Parameter_Detail_BulKSALE.CLR) as CLR,max(TSPL_QC_Parameter_Detail_BulKSALE.Unit_Code) as Unit_Code,TSPL_QC_Parameter_Detail_BulKSALE.Chamber_Desc,max(xx.Net_Weight) as NetWeight from TSPL_Quality_Check_BulkSale " &
                    " left outer join TSPL_QC_Parameter_Detail_BulKSALE on TSPL_Quality_Check_BulkSale.QC_No=TSPL_QC_Parameter_Detail_BulKSALE.QC_No left outer join (select (TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Tare_Weight) as Tare_Weight,(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Gross_Weight) as Gross_Weight,(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Net_Weight)as Net_Weight,TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Chamber_Desc from TSPL_WEIGHMENT_DETAIL_BULKSALE " &
                    " left outer join TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS on TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Weighment_No=TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No=(Select TSPL_Quality_Check_BulkSale.GateEntry_Document_No as GateEntryNo from TSPL_Quality_Check_BulkSale where  TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "')) as xx on xx.Chamber_Desc=TSPL_QC_Parameter_Detail_BulKSALE.Chamber_Desc " &
                    " left outer join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_QC_Parameter_Detail_BulKSALE.Item_Code" &
                    " where TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "' group by TSPL_QC_Parameter_Detail_BulKSALE.Chamber_Desc"
        End If

        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            Dim i As Integer = 0
            For Each dr As DataRow In dt.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + dr("Item_Code") + "' "))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dr("Item_Code")), Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("NetWeight"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSOQty).Value = clsCommon.myCdbl(dr("NetWeight"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr("Unit_Code"))
                'Sanjay
                If ShowBulkDispatchQtyInLtr = True Then
                    ''richa agarwal 8 Jan,2018
                    If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                        ''richa ERO/25/02/19-000499
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQtyLtr).Value = Math.Round(clsCommon.myCdbl(clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value), clsCommon.myCdbl(dr("CLR")))), 0)
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQtyLtr).Value = QtyInLtr(clsCommon.myCstr(dr("Item_Code")), clsCommon.myCstr(dr("Unit_Code")), clsCommon.myCdbl(dr("NetWeight")))
                    End If
                End If
                'Sanjay
                gv1.Rows(gv1.Rows.Count - 1).Cells(colFatPer).Value = clsCommon.myCdbl(dr("Fat"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = clsCommon.myCdbl(dr("SNF"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = clsCommon.myCdbl(dr("CLR"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colChamberDesc).Value = clsCommon.myCstr(dr("Chamber_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colType).Value = clsCommon.myCstr(dr("Type"))
                If ApplyTSPriceAtBulkSale = True Then
                    strQry = "select TSPL_SALES_ORDER_DETAIL_BULKSALE.rate from TSPL_SALES_ORDER_DETAIL_BULKSALE"
                    strQry += " left join TSPL_SALES_ORDER_MASTER_BULKSALE on TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No"
                    strQry += " left join TSPL_GATEENTRY_SALE on TSPL_GATEENTRY_SALE.Bulk_SO_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No"
                    strQry += " left join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.GateEntry_Document_No=TSPL_GATEENTRY_SALE.Document_No"
                    strQry += " where TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "' and TSPL_Quality_Check_BulkSale.Tanker_No='" + FndTankerCode.Value + "' and isnull(TSPL_SALES_ORDER_MASTER_BULKSALE.Price_Code,'')='' and TSPL_SALES_ORDER_DETAIL_BULKSALE.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'"
                    rate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colStandardRate).Value = rate
                End If
                Dim strSOUOM As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_SALES_ORDER_DETAIL_BULKSALE.Unit_code,'') from TSPL_SALES_ORDER_DETAIL_BULKSALE left join TSPL_SALES_ORDER_MASTER_BULKSALE on TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No left join TSPL_GATEENTRY_SALE on TSPL_GATEENTRY_SALE.Bulk_SO_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No left join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.GateEntry_Document_No=TSPL_GATEENTRY_SALE.Document_No where TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "' and TSPL_Quality_Check_BulkSale.Tanker_No='" + FndTankerCode.Value + "' and isnull(TSPL_SALES_ORDER_MASTER_BULKSALE.Price_Code,'')='' and TSPL_SALES_ORDER_DETAIL_BULKSALE.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'"))
                If clsCommon.myLen(strSOUOM) > 0 Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSOUnitCode).Value = clsCommon.myCstr(strSOUOM)
                Else
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSOUnitCode).Value = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
                End If
                Dim commsnuom As String = gv1.Rows(gv1.Rows.Count - 1).Cells(colSOUnitCode).Value
                Dim weight_uom As String = gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value
                If clsCommon.myLen(gv1.Rows(gv1.Rows.Count - 1).Cells(colSOUnitCode).Value) > 0 Then
                    ''richa agarwal TEC/28/03/19-000462 add item structure on setting based
                    Qry = "select top 1 CF from (select (case when (Container_UOM='" & commsnuom & "' and Contained_UOM='" & weight_uom & "') then round(Contained_Qty/Container_Qty,4) else case when (Container_UOM='" & weight_uom & "' and Contained_UOM='" & commsnuom & "') then round(Container_Qty/Contained_Qty,4) end end) as CF,product_type from TSPL_WEIGHT_CONVERSION where product_type in ('ALL','" + clsItemMaster.GetItemProductType(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value), Nothing) + "') " & IIf(ItemStructureMandatoryOnWeightConversion = True, " and isnull(Structure_Code,'') =(select Structure_Code  from TSPL_ITEM_MASTER where item_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value) & "')", "") & " )aa where isnull(cast(CF as varchar),'')<>'' order by Product_Type desc"
                    Dim Master_Sku As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))
                    If Master_Sku = 0 Then
                        Master_Sku = 1
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value) * Master_Sku
                End If
                Dim k As Integer = 0
                For k = 0 To arrList.Count - 1
                    gv1.Rows(gv1.Rows.Count - 1).Cells(arrList(k)).Value = clsCommon.myCstr(dr(arrListDesc(k)))
                Next
            Next


            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
            UpdateAllTotals()
        End If
        dt = Nothing
    End Sub

    Private Function QtyInLtr(ByVal ItemCode As String, ByVal UOM As String, ByVal Qty As Double) As Double
        Dim CONVERTED_Qty As Double
        Dim strsql As String = ""
        strsql = "select  ISNULL(round(case when coalesce(CONVERTED_UOM.Conversion_factor,0)=0 then 0 else (" + clsCommon.myCstr(Qty) + " *StockingUnit.Conversion_Factor /coalesce(CONVERTED_UOM.Conversion_factor,1)) end,2),0) as Qty from TSPL_ITEM_MASTER"
        strsql += " LEFT OUTER JOIN tspl_item_uom_detail as StockingUnit on StockingUnit.Item_Code=TSPL_ITEM_MASTER.item_code and StockingUnit.UOM_CODE='" + UOM + "' "
        strsql += " inner join TSPL_ITEM_UOM_DETAIL AS CONVERTED_UOM ON CONVERTED_UOM.Item_Code=TSPL_ITEM_MASTER.item_code and CONVERTED_UOM.UOM_CODE='Ltr' "
        strsql += " where TSPL_ITEM_MASTER.item_code = '" + ItemCode + "'"
        CONVERTED_Qty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strsql))
        Return CONVERTED_Qty
    End Function


    Private Sub FndPriceCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndPriceCode._MYValidating
        Dim dt As DataTable = Nothing
        Dim whrcls As String = String.Empty
        whrcls = " convert(date,isnull(TSPL_BulkSalePrice_MASTER.ValidTill ,'" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'),103)>='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'"

        ''richa agarwal 12 Sep, 2016
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code= '" & clsFixedParameterCode.showPostrequiredforBulkSale & "' and Type ='" & clsFixedParameterType.showPostrequiredforBulkSale & "'")), "1") = CompairStringResult.Equal Then
            whrcls += " and TSPL_BulkSalePrice_MASTER.Posted='1' "
        End If

        Dim Qry As String = "Select Price_Code as Code ,Convert(varchar,Price_Date,103) as Date,Convert(varchar,isnull(TSPL_BulkSalePrice_MASTER.ValidTill,'" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'),103) as [Valid Upto],Location_Code as [Location Code],Fat_Weightage as [Fat Weightage],Snf_Weightage as [SNF Weightage],Fat_Ratio As [Fat Ratio],Snf_Ratio as [SNF Ratio],Standard_Rate as [Standard Rate],Posted,case when  UseInCanSale =1 then 'True' else 'False' end as [Use In Cancel],TSRate as [TS Rate] from TSPL_BulkSalePrice_MASTER "
        FndPriceCode.Value = clsCommon.ShowSelectForm("BulkSalePriceChart", Qry, "Code", whrcls, FndPriceCode.Value, "Code", isButtonClicked)


        'FndPriceCode.Value = ClsBulkSalePriceChart.getFinder(whrcls, FndPriceCode.Value, isButtonClicked)

        If clsCommon.myLen(FndPriceCode.Value) > 0 Then
            dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Ratio,Snf_Ratio,Standard_Rate,TolerancePerPlus,TolerancePerMinus,Posted,TSRate from TSPL_BulkSalePrice_MASTER where Price_Code='" + FndPriceCode.Value + "'")
            If dt.Rows.Count > 0 Then
                ''richa agarwal 18/12/2014
                TxtFatWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                TxtSNFWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                txtfatRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                txtSNFRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
                txtStanadardrate.Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                TxtToleranceinplus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
                txtToleranceinminus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
                ''richa against Ticket no ERO/10/01/19-000463 on 14 Jan,2019
                If ApplyTSPriceAtBulkSale = True Then
                    txtStanadardrate.Value = clsCommon.myCdbl(dt.Rows(0)("TSRate"))
                End If
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
                    If e.Column Is gv1.Columns(colFatPer) Or e.Column Is gv1.Columns(colSNFPer) Or e.Column Is gv1.Columns(colQty) Then
                        'sanjay
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            UpdateCurrentRow(ii)
                        Next
                    End If
                End If
                UpdateAllTotals()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
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
            'For ii As Integer = 0 To gv1.Rows.Count - 1
            '    If (clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0) Then
            '        dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
            '    End If
            'Next



            ''richa 7 Oct,2020

            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim Strii As String = clsCommon.myCstr(ii + 1)
                If (clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0) Then
                    dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
                    'dblAmtAfterDis = dblAmtAfterDis + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmtAfterDis).Value)

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
            Dim dblAmtAfterDis As Double = 0
            If ApplyTSPriceAtBulkSale = True Then
                ''richa agarwal 14 Jan,2019 ERO/10/01/19-000463
                If clsCommon.myLen(FndPriceCode.Value) > 0 Then
                    StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)
                Else
                    StandardRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colStandardRate).Value)
                End If
            Else
                StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)
            End If
            FatWeightage = clsCommon.myCdbl(TxtFatWeightage.Value)
            SNFWeightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
            FatPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatPer).Value)
            SNFPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFPer).Value)
            FatRatio = clsCommon.myCdbl(txtfatRatio.Value)
            SNFRatio = clsCommon.myCdbl(txtSNFRatio.Value)

            ''richa agarwal 8 Jan,2019 ERO/07/01/19-000457
            If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                Qty = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQtyLtr).Value), 0)
            Else
                Qty = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), 3)
            End If

            ''richa agarwal 8 Jan,2019 ERO/07/01/19-000457
            If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                FatRate = Math.Round(StandardRate, 2)
                SNFRate = Math.Round(StandardRate, 2)

                FatKG = ((Qty * FatPer) / 100)
                SNFKG = ((Qty * SNFPer) / 100)
            Else
                FatRate = Math.Round(StandardRate * FatWeightage / FatRatio, 2)
                SNFRate = Math.Round(StandardRate * SNFWeightage / SNFRatio, 2)

                FatKG = Math.Round(((Qty * FatPer) / 100), 3)
                SNFKG = Math.Round(((Qty * SNFPer) / 100), 3)
            End If

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
            ''richa Against ticket No BM00000003893 on 11/09/2014
            If Qty > 0 Then
                If ApplyTSPriceAtBulkSale = True Then
                    gv1.Rows(IntRowNo).Cells(colNetMilkRate).Value = Math.Round((StandardRate * (FatPer + SNFPer)) / 100, 2)
                    If UseKGLitreConversionInBulkSaleAsperCLRCalculation = False Then
                        gv1.Rows(IntRowNo).Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colNetMilkRate).Value) * Qty, 2)
                    End If
                Else
                    gv1.Rows(IntRowNo).Cells(colNetMilkRate).Value = Math.Round(clsCommon.myCdbl(Amount / Qty), 2)
                End If
                'Sanjay
                If ShowBulkDispatchQtyInLtr = True Then
                    ''richa agarwal 8 Jan,2019 ERO/07/01/19-000457
                    If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                        ''richa ERO/25/02/19-000499
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQtyLtr).Value = Math.Round(clsCommon.myCdbl(clsItemMaster.GetQtyInLtrFromKgByCLR(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCLR).Value))), 0)
                    Else
                        gv1.Rows(IntRowNo).Cells(colQtyLtr).Value = QtyInLtr(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colItemCode).Value), clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colUnitCode).Value), Qty)
                    End If
                End If
                'Sanjay
            End If

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
                        dblAmtAfterDis = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmount).Value)
                        Dim dblBaseAmt As Double = 0
                        Dim dblTaxAmt As Double = 0
                        If IsSurTax Then
                            'Dim dblSurTaxAmt As Double = GetCurrentRowSurTaxAmt(IntRowNo, ii, strSurTaxCode)
                            'dblBaseAmt = dblSurTaxAmt
                        Else
                            Dim dblOtherTaxAmt As Double = 0
                            ''richa 15 Sep 2020 changes according to tax
                            If Not IsTaxonBaseAmount Then
                                dblOtherTaxAmt = GetCurrentRowOtherTaxAmt(IntRowNo, Strii, arrTaxableAuth)
                            End If
                            If IsTaxonBaseAmount = False AndAlso clsCommon.myCdbl(txttcstaxbaseamount.Value) > 0 AndAlso AllowtoChangeTCSBaseAmount = True Then
                                Dim dblTotalBasicPrice As Double = 0
                                For n As Integer = 0 To gv1.Rows.Count - 1
                                    If clsCommon.myLen(gv1.Rows(n).Cells(colItemCode).Value) > 0 Then
                                        dblTotalBasicPrice = dblTotalBasicPrice + clsCommon.myCdbl(gv1.Rows(n).Cells(colAmount).Value)
                                    End If
                                Next
                                If dblTotalBasicPrice > 0 Then
                                    dblBaseAmt = (clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colAmount).Value) * clsCommon.myCdbl(txttcstaxbaseamount.Value)) / dblTotalBasicPrice
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
    Private Function AllowToSave() As Boolean
        ' KUNAL > TICKET : BM00000009609 > Modified Date : 22-09-2016
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Focus()
            txtDate.Select()
            Return False
        End If


        If clsCommon.myLen(lblCustomerCode.Text) <= 0 Then
            lblCustomerCode.Focus()
            Throw New Exception("Customer No cannot be left blank")
        End If
        If clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select CONVERT(date, QC_Date,103) from TSPL_Quality_Check_BulkSale where QC_No ='" + LblQCCode.Text + "'")) > clsCommon.myCDate(txtDate.Value) Then
            txtDate.Focus()
            Throw New Exception("Date cannot be less than from QC No Date")
        End If
        If clsCommon.myLen(FndTankerCode.Value) <= 0 Then
            FndTankerCode.Focus()
            Throw New Exception("Tanker No cannot be left blank")
        End If
        If clsCommon.myLen(LblQCCode.Text) <= 0 Then
            LblQCCode.Focus()
            Throw New Exception("Qc No cannot be left blank")
        End If
        If ApplyTSPriceAtBulkSale = False Then
            If clsCommon.myLen(FndPriceCode.Value) <= 0 Then
                FndPriceCode.Focus()
                Throw New Exception("Price Code cannot be left blank")
            End If

            Dim dt1 As DataTable = Nothing
            dt1 = clsDBFuncationality.GetDataTable("select TSPL_BulkSalePrice_MASTER.Price_Code as Code from  TSPL_BulkSalePrice_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BulkSalePrice_MASTER .Location_Code where TSPL_BulkSalePrice_MASTER.Price_Code='" & FndPriceCode.Value & "' and Convert(date,TSPL_BulkSalePrice_MASTER.Price_Date ,103)<=CONVERT(date,'" & txtDate.Value & "',103) AND (ISNULL(Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill ,103),'')='' OR Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill,103)>=CONVERT(date,'" & txtDate.Value & "',103))")
            If dt1.Rows.Count <= 0 Then
                Throw New Exception("Please check Price Code Date")
            End If

            If clsCommon.myCdbl(txtStanadardrate.Value) < 0 Then
                txtStanadardrate.Focus()
                Throw New Exception("Stanadard Rate cannot be in negative")
            End If
            If clsCommon.myCdbl(txtStanadardrate.Value) = 0 Then
                txtStanadardrate.Focus()
                Throw New Exception("Stanadard Rate cannot be zero")
            End If

            If clsCommon.myLen(txtStanadardrate.Value) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select round((Standard_Rate+((Standard_Rate *TolerancePerPlus )/100)),2) as StandardRatePlus,round((Standard_Rate-((Standard_Rate *TolerancePerMinus  )/100)),2) as StandardRateMinus  from TSPL_BulkSalePrice_MASTER where Price_Code ='" + FndPriceCode.Value + "'")
                If dt.Rows.Count > 0 Then

                    If clsCommon.myCdbl(txtStanadardrate.Value) >= clsCommon.myCdbl(dt.Rows(0)("StandardRateMinus")) And clsCommon.myCdbl(txtStanadardrate.Value) <= clsCommon.myCdbl(dt.Rows(0)("StandardRatePlus")) Then
                    Else
                        txtStanadardrate.Focus()
                        Throw New Exception("Standard rate should be in range according to price chart")
                    End If
                End If
            End If
        End If
        If EnterInsuranceNoandSealNo Then
            If clsCommon.myLen(txtinsuranceno.Text) <= 0 Then
                txtinsuranceno.Focus()
                Throw New Exception("Insurance No cannot be blank")
            End If

            If clsCommon.myLen(txtsealno.Text) <= 0 Then
                txtsealno.Focus()
                Throw New Exception("Seal No cannot be blank")
            End If
        End If

        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(i).Cells(colFatPer).Value) <= 0 Then
                Throw New Exception("Fat% cannot be left blank or zero")
            End If
            If AllowSNFNotManditoryInBulkSale = False Then
                If clsCommon.myLen(gv1.Rows(i).Cells(colSNFPer).Value) <= 0 Then
                    Throw New Exception("SNF% cannot be left blank or zero")
                End If
            End If

            ''richa agarwal 28/02/2016 apply tolerance limit and check stock qty 
            Dim balqty As Double = 0
            Dim dispatchqty As Double = clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
            Dim SubLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select IsNull(Silo_No,'')  from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No=(Select LoadingTanker_No  from TSPL_Quality_Check_BulkSale where QC_No='" + LblQCCode.Text + "')"))

            If clsCommon.myLen(SubLocation) > 0 Then
                balqty = ClsLoadingTanker.getBalance(clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value), lblLocationCode.Text, SubLocation, txtDocNo.Value, txtDate.Value, Nothing, "KG")
            Else
                balqty = ClsLoadingTanker.getBalance(clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value), lblLocationCode.Text, "", txtDocNo.Value, txtDate.Value, Nothing, "KG")

            End If
            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                If balqty > 0 Then
                    balqty = ClsLoadingTanker.GetTolerane(balqty, dispatchqty)
                End If
            End If

            If balqty < dispatchqty Then
                Throw New Exception("Available Stock " & balqty & Environment.NewLine & " Dispatch Qty " & dispatchqty)
            End If

        Next

        If ApplyTSPriceAtBulkSale = True Then
            Qry = "select TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No from TSPL_SALES_ORDER_DETAIL_BULKSALE" &
             " left join TSPL_SALES_ORDER_MASTER_BULKSALE on TSPL_SALES_ORDER_MASTER_BULKSALE.Document_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No " &
            " left join TSPL_GATEENTRY_SALE on TSPL_GATEENTRY_SALE.Bulk_SO_No=TSPL_SALES_ORDER_DETAIL_BULKSALE.Document_No " &
             " left join TSPL_Quality_Check_BulkSale on TSPL_Quality_Check_BulkSale.GateEntry_Document_No=TSPL_GATEENTRY_SALE.Document_No " &
             " where TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "' and TSPL_Quality_Check_BulkSale.Tanker_No='" + FndTankerCode.Value + "'   "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
                If clsCommon.myLen(FndPriceCode.Value) <= 0 Then
                    FndPriceCode.Focus()
                    Throw New Exception("Price Code cannot be left blank")
                End If

                Dim dt1 As DataTable = Nothing
                dt1 = clsDBFuncationality.GetDataTable("select TSPL_BulkSalePrice_MASTER.Price_Code as Code from  TSPL_BulkSalePrice_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BulkSalePrice_MASTER .Location_Code where TSPL_BulkSalePrice_MASTER.Price_Code='" & FndPriceCode.Value & "' and Convert(date,TSPL_BulkSalePrice_MASTER.Price_Date ,103)<=CONVERT(date,'" & txtDate.Value & "',103) AND (ISNULL(Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill ,103),'')='' OR Convert(date,TSPL_BulkSalePrice_MASTER.ValidTill,103)>=CONVERT(date,'" & txtDate.Value & "',103))")
                If dt1.Rows.Count <= 0 Then
                    Throw New Exception("Please check Price Code Date")
                End If
                ''richa agarwal ERO/10/01/19-000461 15 Jan,2019
                If clsCommon.myCdbl(txtStanadardrate.Value) <= 0 Then
                    If (common.clsCommon.MyMessageBoxShow("TS Rate is 0,Do you want to continue", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No) Then
                        Throw New Exception("Please check TS Rate.")
                    End If
                End If

            End If
        End If
        If AllowtoChangeTCSBaseAmount Then
            If clsCommon.myCdbl(txttcstaxbaseamount.Value) > clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text) Then
                Throw New Exception("TCS Tax Base amount should not be greater than Actual TCS Tax Base Amount")
            End If
        End If

        Return True
    End Function

    Private Function CheckCustomerOutstandingAmount(ByVal strCustomer As String, ByVal ChekPostBtn As Boolean) As Boolean
        Try
            Dim dblAmt As Double = 0
            dblAmt = ClsDispatchBulkSale.CheckCustomerOutstandingAmount(txtDocNo.Value, strCustomer, lblTotRAmt1.Text)
            ''richa 31/12/2014
            If dblAmt < lblTotRAmt1.Text AndAlso UsLock1.Status = ERPTransactionStatus.Open Then
                Dim dblNewCredtitLimit = dblAmt - lblTotRAmt1.Text
                common.clsCommon.MyMessageBoxShow("Please send for approval for increasing credit limit " + clsCommon.myCstr(dblNewCredtitLimit))
                Return False
            End If
            If dblAmt < lblTotRAmt1.Text And UsLock1.Status = ERPTransactionStatus.Pending Then
                Dim dblNewCredtitLimit = dblAmt - lblTotRAmt1.Text
                common.clsCommon.MyMessageBoxShow("Please increase credit limit " + clsCommon.myCstr(dblNewCredtitLimit) + " for customer " + lblCustomerCode.Text)
                Return False
            End If

            If ChekPostBtn = True Then
                Return True
            End If
            'Return True

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Sub SaveData()

        Dim totalqty As Decimal = 0
        Dim objApproval As New clsApply_Approval()
        Dim obj As New ClsDispatchBulkSale()
        Dim objTr As New clsDispatchDetailBulkSale
        Try
            If AllowNLevel Then
                If Not AllowModifcationByApprovalUser Then
                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(txtDocNo.Value))
                End If
            End If
            If AllowToSave() Then

                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Customer_Code = lblCustomerCode.Text
                obj.QC_Code = LblQCCode.Text
                obj.Tanker_Code = FndTankerCode.Value
                obj.Location_Code = lblLocationCode.Text
                obj.Dip_marking = TxtDipMarking.Text
                obj.Challan_No = TxtChallanNo.Text
                obj.Insurance_No = txtinsuranceno.Text
                obj.Seal_No = txtsealno.Text
                obj.Tare_Weight = TxtTareWeight.Value
                obj.Gross_Weight = txtGrossWeight.Value
                obj.Net_Weight = txtNetWeight.Value
                If ApplyTSPriceAtBulkSale = False Then
                    obj.Price_Code = FndPriceCode.Value
                End If
                obj.Total_Amt = lblTotRAmt1.Text
                obj.CreditLimit = clsCommon.myCdbl(txtCreditLimit.Text)

                If chkCreateAoutoInvoice.Checked = True Then
                    obj.Is_Create_Auto_Invoice = 1
                Else
                    obj.Is_Create_Auto_Invoice = 0
                End If
                obj.ApprovalRequired = "N"
                obj.Status = "Open"
                obj.EWayBillNo = TxtEWayBillNo.Text
                obj.EWayBillDate = txtewaybilldate.Value

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
                obj.Document_Amount = lblDocumentAmount.Text
                obj.Total_Tax_Amt = lblTaxAmt.Text

                obj.ActualTCSBaseAmount = clsCommon.myCdbl(lblActualTCSTaxBaseAmt.Text)
                obj.ChangedTCSBaseAmount = clsCommon.myCdbl(txttcstaxbaseamount.Value)


                ''richa 31/12/2014
                Dim desc As String = ""
                desc = clsDBFuncationality.getSingleValue("Select CheckCreditLimit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + lblCustomerCode.Text + "'")
                If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
                    Dim dblAllowedAmt As Double = 0
                    If CheckCustomerOutstandingAmount(lblCustomerCode.Text, True) = False AndAlso Not AllowNLevel Then
                        obj.ApprovalRequired = "Y"
                        obj.Status = "Pending"
                    End If
                End If
                If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select Approved from TSPL_Dispatch_BulkSale where Document_No='" & txtDocNo.Value & "' "), "Y") = CompairStringResult.Equal Then
                    obj.ApprovalRequired = "Y"
                    obj.Status = "Approved"
                End If
                obj.Fat_Weightage = clsCommon.myCdbl(TxtFatWeightage.Text)
                obj.Snf_Weightage = clsCommon.myCdbl(TxtSNFWeightage.Text)
                obj.Fat_Ratio = clsCommon.myCdbl(txtfatRatio.Text)
                obj.Snf_Ratio = clsCommon.myCdbl(txtSNFRatio.Text)

                obj.arrDispatchDetailBulkSale = New List(Of clsDispatchDetailBulkSale)

                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New clsDispatchDetailBulkSale()
                    objTr.Document_No = clsCommon.myCstr(obj.Document_No)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    objTr.SO_Unit_code = clsCommon.myCstr(grow.Cells(colSOUnitCode).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.SO_Qty = clsCommon.myCdbl(grow.Cells(colSOQty).Value)
                    objTr.FatPer = clsCommon.myCdbl(grow.Cells(colFatPer).Value)
                    objTr.SNFPer = clsCommon.myCdbl(grow.Cells(colSNFPer).Value)
                    objTr.CLR = clsCommon.myCdbl(grow.Cells(colCLR).Value)
                    objTr.Fat_KG = clsCommon.myCdbl(grow.Cells(colFatKG).Value)
                    objTr.SNF_KG = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                    objTr.FatAmount = clsCommon.myCdbl(grow.Cells(ColFatAmount).Value)

                    objTr.SNFAmount = clsCommon.myCdbl(grow.Cells(ColSNFAmount).Value)
                    objTr.NetMilkRate = clsCommon.myCdbl(grow.Cells(colNetMilkRate).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                    objTr.FatRate = clsCommon.myCdbl(grow.Cells(colFatRate).Value)
                    objTr.SNFRate = clsCommon.myCdbl(grow.Cells(colSNFRate).Value)

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


                    If ApplyTSPriceAtBulkSale = True Then
                        ''richa agarwal 14 Jan,2019 ERO/10/01/19-000463
                        If clsCommon.myLen(FndPriceCode.Value) > 0 Then
                            objTr.StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)
                            obj.Price_Code = FndPriceCode.Value
                        Else
                            objTr.StandardRate = clsCommon.myCdbl(grow.Cells(colStandardRate).Value)
                        End If
                    Else
                        objTr.StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)
                    End If
                    objTr.Type = clsCommon.myCstr(grow.Cells(colType).Value)
                    objTr.Seal_No = clsCommon.myCstr(grow.Cells(colChamberSeal).Value)
                    objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                    ''richa agarwal 4 feb, 2019 ERO/25/02/19-000499
                    objTr.Qty_in_Ltr = clsCommon.myCdbl(grow.Cells(colQtyLtr).Value)
                    obj.arrDispatchDetailBulkSale.Add(objTr)
                Next


                If (ClsDispatchBulkSale.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(obj.Document_No)
                    If Not isFlag Then

                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                        txtDocNo.Value = obj.Document_No
                        ''done by stuti approval work 01/12/2016
                        If AllowNLevel Then
                            objApproval = New clsApply_Approval()
                            objApproval.DocCode = txtDocNo.Value
                            objApproval.TotAmt = lblTotRAmt1.Text
                            objApproval.CustCode = lblCustomerCode.Text
                            clsApply_Approval.CheckApprovalRequired(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(obj.Document_No), txtDate.Text, "", "", clsCommon.myCdbl(lblTotRAmt1.Text), clsCommon.myCdbl(totalqty), "", objApproval)
                        End If
                        LoadData(obj.Document_No, NavigatorType.Current)
                    End If

                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
            objTr = Nothing
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim objApproval As New clsApply_Approval()
        Dim dt As DataTable = Nothing
        Dim obj As ClsDispatchBulkSale = Nothing
        Try
            obj = ClsDispatchBulkSale.GetData(strCode, arrLoc, NavTyep)
            If ApplyMultiChamber Then
                gv1.Rows.Clear()
            End If

            isInsideLoadData = True
            If obj IsNot Nothing Then
                isNewEntry = False
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                lblCustomerCode.Text = obj.Customer_Code
                lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code='" + lblCustomerCode.Text + "'")
                FndTankerCode.Value = obj.Tanker_Code

                LblQCCode.Text = obj.QC_Code
                TxtChallanNo.Text = obj.Challan_No
                txtinsuranceno.Text = obj.Insurance_No
                txtsealno.Text = obj.Seal_No
                lblLocationCode.Text = obj.Location_Code
                LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")
                txtGrossWeight.Value = obj.Gross_Weight
                TxtTareWeight.Value = obj.Tare_Weight
                txtNetWeight.Value = obj.Net_Weight
                FndPriceCode.Value = obj.Price_Code

                TxtEWayBillNo.Text = obj.EWayBillNo
                If obj.EWayBillDate IsNot Nothing Then
                    txtewaybilldate.Value = obj.EWayBillDate
                    txtewaybilldate.Checked = True
                Else
                    txtewaybilldate.Value = clsCommon.GETSERVERDATE()
                    txtewaybilldate.Checked = False
                End If
                If obj.Is_Create_Auto_Invoice = 0 Then
                    chkCreateAoutoInvoice.Checked = False
                Else
                    chkCreateAoutoInvoice.Checked = True
                End If
                TxtFatWeightage.Text = obj.Fat_Weightage
                TxtSNFWeightage.Text = obj.Snf_Weightage
                txtfatRatio.Text = obj.Fat_Ratio
                txtSNFRatio.Text = obj.Snf_Ratio

                LoadBlankGridTax()
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


                If obj.arrDispatchDetailBulkSale IsNot Nothing AndAlso obj.arrDispatchDetailBulkSale.Count > 0 Then
                    For Each objTr As clsDispatchDetailBulkSale In obj.arrDispatchDetailBulkSale
                        If ApplyMultiChamber Then
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ")
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = objTr.Unit_code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSOUnitCode).Value = objTr.SO_Unit_code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNCode).Value = objTr.HSN_code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSOQty).Value = objTr.SO_Qty

                            'Sanjay
                            If ShowBulkDispatchQtyInLtr = True Then
                                ''richa agarwal 8 Jan,2018
                                If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                                    ''richa ERO/25/02/19-000499
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQtyLtr).Value = objTr.Qty_in_Ltr
                                Else
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQtyLtr).Value = QtyInLtr(objTr.Item_Code, objTr.Unit_code, objTr.Qty)
                                End If
                            End If
                            'Sanjay

                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFatPer).Value = objTr.FatPer
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNFPer
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = objTr.CLR
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value = objTr.Fat_KG
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = objTr.SNF_KG
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFatAmount).Value = objTr.FatAmount
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFAmount).Value = objTr.SNFAmount
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colNetMilkRate).Value = objTr.NetMilkRate
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
                            If ApplyTSPriceAtBulkSale = True Then
                                ''richa agarwal 14 Jan,2019 ERO/10/01/19-000463
                                If clsCommon.myLen(FndPriceCode.Value) > 0 Then
                                    txtStanadardrate.Value = objTr.StandardRate
                                Else
                                    gv1.Rows(0).Cells(colStandardRate).Value = objTr.StandardRate
                                End If
                            End If
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFatRate).Value = objTr.FatRate
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFRate).Value = objTr.SNFRate
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colChamberSeal).Value = objTr.Seal_No
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colType).Value = objTr.Type
                            txtStanadardrate.Value = objTr.StandardRate

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



                            Dim Qry As String = Nothing
                            Dim strpivotcol As String = Nothing
                            Qry = "select stuff((select DISTINCT ',[' + TSPL_QC_Parameter_Detail_BulKSALE.Param_Field_Desc+']'  from TSPL_QC_Parameter_Detail_BulKSALE for xml path('')  ),1,1,'')"
                            strpivotcol = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry))
                            If clsCommon.myLen(strpivotcol) > 0 Then
                                Qry = "select Item_Code,Type,Fat,SNF,CLR,Unit_Code,Chamber_Desc,NetWeight," + strpivotcol + " from (select (TSPL_QC_Parameter_Detail_BulKSALE.Item_Code) as Item_Code,(TSPL_ITEM_MASTER.Product_Type) AS Type,(TSPL_QC_Parameter_Detail_BulKSALE.Fat) as Fat ,(TSPL_QC_Parameter_Detail_BulKSALE.SNF) as SNF,(TSPL_QC_Parameter_Detail_BulKSALE.CLR) as CLR,(TSPL_QC_Parameter_Detail_BulKSALE.Unit_Code) as Unit_Code,TSPL_QC_Parameter_Detail_BulKSALE.Chamber_Desc,(xx.Net_Weight) as NetWeight ,TSPL_QC_Parameter_Detail_BulKSALE.Param_Field_Desc,TSPL_QC_Parameter_Detail_BulKSALE.Param_Field_Value" &
                                        " from TSPL_Quality_Check_BulkSale  left outer join TSPL_QC_Parameter_Detail_BulKSALE on TSPL_Quality_Check_BulkSale.QC_No=TSPL_QC_Parameter_Detail_BulKSALE.QC_No left outer join (select (TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Tare_Weight) as Tare_Weight,(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Gross_Weight) as Gross_Weight,(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Net_Weight)as Net_Weight,TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Chamber_Desc from TSPL_WEIGHMENT_DETAIL_BULKSALE  left outer join TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS on TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Weighment_No=TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No=(Select TSPL_Quality_Check_BulkSale.GateEntry_Document_No as GateEntryNo from TSPL_Quality_Check_BulkSale where  TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "')) as xx on xx.Chamber_Desc=TSPL_QC_Parameter_Detail_BulKSALE.Chamber_Desc left outer join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_QC_Parameter_Detail_BulKSALE.Item_Code where TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "' and xx.Chamber_Desc='" + clsCommon.myCstr(objTr.Chamber_Desc) + "' ) as st " &
                                        " PIVOT ( MAX(PARAM_FIELD_VALUE) FOR PARAM_FIELD_DESC IN (" + strpivotcol + ")) AS PT ORDER BY Chamber_Desc"
                            End If
                            dt = clsDBFuncationality.GetDataTable(Qry)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                For Each dr As DataRow In dt.Rows
                                    Dim k As Integer = 0
                                    For k = 0 To arrList.Count - 1
                                        gv1.Rows(gv1.Rows.Count - 1).Cells(arrList(k)).Value = clsCommon.myCstr(dr(arrListDesc(k)))
                                    Next
                                Next
                            End If
                        Else
                            gv1.Rows(0).Cells(colSlNo).Value = "1"
                            gv1.Rows(0).Cells(colItemCode).Value = objTr.Item_Code
                            gv1.Rows(0).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ")
                            gv1.Rows(0).Cells(colUnitCode).Value = objTr.Unit_code
                            gv1.Rows(0).Cells(colHSNCode).Value = objTr.HSN_code
                            gv1.Rows(0).Cells(colQty).Value = objTr.Qty
                            If ShowBulkDispatchQtyInLtr = True Then
                                If UseKGLitreConversionInBulkSaleAsperCLRCalculation = True Then
                                    ''richa ERO/25/02/19-000499
                                    gv1.Rows(0).Cells(colQtyLtr).Value = objTr.Qty_in_Ltr
                                Else
                                    gv1.Rows(0).Cells(colQtyLtr).Value = QtyInLtr(objTr.Item_Code, objTr.Unit_code, objTr.Qty)
                                End If
                            End If
                            gv1.Rows(0).Cells(colFatPer).Value = objTr.FatPer
                            gv1.Rows(0).Cells(colSNFPer).Value = objTr.SNFPer
                            gv1.Rows(0).Cells(colCLR).Value = objTr.CLR
                            gv1.Rows(0).Cells(colFatKG).Value = objTr.Fat_KG
                            gv1.Rows(0).Cells(colSNFKG).Value = objTr.SNF_KG
                            gv1.Rows(0).Cells(ColFatAmount).Value = objTr.FatAmount
                            gv1.Rows(0).Cells(ColSNFAmount).Value = objTr.SNFAmount
                            gv1.Rows(0).Cells(colNetMilkRate).Value = objTr.NetMilkRate
                            gv1.Rows(0).Cells(colAmount).Value = objTr.Amount
                            If ApplyTSPriceAtBulkSale = True Then
                                ''richa agarwal 14 Jan,2019 ERO/10/01/19-000463
                                If clsCommon.myLen(FndPriceCode.Value) > 0 Then
                                    txtStanadardrate.Value = objTr.StandardRate
                                Else
                                    gv1.Rows(0).Cells(colStandardRate).Value = objTr.StandardRate
                                End If
                            End If
                            gv1.Rows(0).Cells(colFatRate).Value = objTr.FatRate
                            gv1.Rows(0).Cells(colSNFRate).Value = objTr.SNFRate
                            txtStanadardrate.Value = objTr.StandardRate
                            ''richa agarwal 8 Jan,2018
                            gv1.Rows(0).Cells(colSOUnitCode).Value = objTr.SO_Unit_code
                            gv1.Rows(0).Cells(colSOQty).Value = objTr.SO_Qty
                        End If

                        If clsCommon.myLen(FndPriceCode.Value) > 0 Then
                            dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Ratio,Snf_Ratio,Standard_Rate,TolerancePerPlus,TolerancePerMinus from TSPL_BulkSalePrice_MASTER where Price_Code='" + FndPriceCode.Value + "'")
                            If dt.Rows.Count > 0 Then
                                ''richa agarwal 18/12/2014
                                TxtFatWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                                TxtSNFWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                                txtfatRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                                txtSNFRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
                                TxtToleranceinplus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
                                txtToleranceinminus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
                                UpdateCurrentRow(0)
                            End If
                        End If
                    Next
                Else
                    gv1.DataSource = Nothing
                End If
                lblActualTCSTaxBaseAmt.Text = clsCommon.myFormat(obj.ActualTCSBaseAmount)
                txttcstaxbaseamount.Value = clsCommon.myCdbl(obj.ChangedTCSBaseAmount)

                lblTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amt)
                lblDocumentAmount.Text = clsCommon.myFormat(obj.Document_Amount)
                lblTotRAmt1.Text = obj.Total_Amt
                txtDocNo.MyReadOnly = True
                btnsave.Text = "Update"
                If clsCommon.CompairString(obj.Approved.ToUpper(), "Y".ToUpper()) = CompairStringResult.Equal Then
                    txtCreditLimit.Enabled = True
                    txtCreditLimit.Value = 0
                End If

                If clsCommon.CompairString(obj.Status, "Pending") = CompairStringResult.Equal Then
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                End If

                If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
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
                If clsCommon.CompairString(obj.ReverseFlag, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Posted, "0") = CompairStringResult.Equal Then
                    btndelete.Enabled = False
                End If


                UcAttachment1.LoadData(obj.Document_No)
            Else
                Reset()

            End If
            '=====================if document go for approval then no post button visible or if document contain related setting
            If AllowNLevel Then
                objApproval = New clsApply_Approval()
                objApproval.DocCode = txtDocNo.Value
                objApproval.TotAmt = clsCommon.myCdbl(lblTotRAmt1.Text)
                objApproval.CustCode = lblCustomerCode.Text
                btnPost.Visible = MyBase.isPostFlag

                If Not clsApply_Approval.Visibility_PostButtonForApproval(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(txtDocNo.Value), clsCommon.myCdbl(lblTotRAmt1.Text), 0, "", objApproval) Then
                    btnPost.Visible = False
                    If UsLock1.Status = ERPTransactionStatus.Pending Then
                        UsLock1.Status = clsApply_Approval.ApprovalCondCheck_Doc(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(txtDocNo.Value), Nothing)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
            dt = Nothing
            obj = Nothing
        End Try
    End Sub
    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_Dispatch_BulkSale where Document_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
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
        Dim qry As String = "Select TSPL_Dispatch_BulkSale.Document_No as Code,Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as [Dispatch Date],TSPL_Dispatch_BulkSale.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_Dispatch_BulkSale.Tanker_Code as [Tanker Code],TSPL_Dispatch_BulkSale.QC_Code as [QC Code],TSPL_Dispatch_BulkSale.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Dispatch_BulkSale.Price_Code as [Price Code],TSPL_Dispatch_BulkSale.Dip_marking as [Dip Marking],TSPL_Dispatch_BulkSale.Challan_No as [Challan No],case when TSPL_Dispatch_BulkSale.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_Dispatch_BulkSale left outer Join TSPL_CUSTOMER_MASTER on TSPL_Dispatch_BulkSale.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_Dispatch_BulkSale.Location_Code =TSPL_LOCATION_MASTER.Location_Code"
        txtDocNo.Value = clsCommon.ShowSelectForm("DispatchBulkSale", qry, "Code", " TSPL_Dispatch_BulkSale.Location_Code in (" + arrLoc + ")", txtDocNo.Value, "", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Reset()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                'done by stuti on 12/12/2016 for N-LevelApproval
                If AllowNLevel Then
                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(txtDocNo.Value))
                End If
                If (ClsDispatchBulkSale.DeleteData(txtDocNo.Value)) Then
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
    Sub PostData()
        Dim msg As String = Nothing
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing

        Dim desc As String = Nothing
        Try

            desc = clsDBFuncationality.getSingleValue("Select CheckCreditLimit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + lblCustomerCode.Text + "'")
            If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
                Dim dblAllowedAmt As Double = 0
                If CheckCustomerOutstandingAmount(lblCustomerCode.Text, False) = False AndAlso Not AllowNLevel Then Exit Sub
            End If
            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                If (ClsDispatchBulkSale.PostData(MyBase.Form_ID, arrLoc, txtDocNo.Value)) Then
                    If ApplyMultiChamber Then
                        btnSend.PerformClick()
                    End If
                    msg = "Successfully posted"
                    common.clsCommon.MyMessageBoxShow(msg)
                    'If (clsCommon.MyMessageBoxShow(Me, "Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                    '    funPrintt(txtDocNo.Value)
                    'End If
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


    Private Sub EmailSmsSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EmailSmsSetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.FrmDispatchBulkSale
        frm.ShowDialog()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click

    End Sub

    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Delivery No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)
            Dim lstUsers As New List(Of String)
            lstUsers.Add(lblCustomerCode.Text)
            SendEmail(lstUsers, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub SendEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean)
        Try
            'sanjay
            Dim strotherno As String = Nothing
            strotherno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from TSPL_CUSTOMER_MASTER where cust_code='" + lblCustomerCode.Text + "'"))

            Dim strContactPerson As String = String.Empty
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", Nothing)
            Dim objEmailH As New clsEMailHead()
            objEmailH.arrEMail = New List(Of String)()
            Dim objSMSH As New clsSMSHead()
            objSMSH.arrMobilNo = New List(Of String)()
            objSMSH.arrMobilNo.Add(clsCommon.myCstr(strotherno))
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                    objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.DeliveryNo, txtDocNo.Value)
                    objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.DeliveryDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))

                    objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.DeliveryNo, txtDocNo.Value)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.DeliveryDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerNo, lblCustomerCode.Text)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerName, lblCustomerName.Text)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.LocationCode, lblLocationCode.Text)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.LocationName, LblLocationName.Text)
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt1.Text))
                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)
                End If


                If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                    objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.DeliveryNo, txtDocNo.Value)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.DeliveryDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CustomerNo, lblCustomerCode.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.CustomerName, lblCustomerName.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.LocationCode, lblLocationCode.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.LocationName, LblLocationName.Text)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myCstr(lblTotRAmt1.Text))
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, MyBase.Form_ID)
                End If

                Dim strRptPath As String = String.Empty
                Dim lstReceiptents As List(Of String) = Nothing
                Dim strPath As String = String.Empty
                For Each strUser As String In lstUsers
                    lstReceiptents = New List(Of String)
                    Dim qry As String = String.Empty
                    Dim emailId As String = String.Empty
                    If isSendForApproval Then
                        strContactPerson = strUser
                        qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
                        emailId = clsDBFuncationality.getSingleValue(qry)
                    Else
                        strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
                        emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
                    End If

                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactPerson)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactPerson)
                    lstReceiptents.Add(emailId)
                    objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))

                    objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.UserCode, strUser)
                    objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.UserCode, strUser)
                Next

                objEmailH.SaveData(MyBase.Form_ID, objEmailH, Nothing)
                objEmailH = Nothing

                objSMSH.SaveData(Form_ID, objSMSH, Nothing)
                objSMSH = Nothing
                clsCommon.MyMessageBoxShow("E-Mail/SMS Send Successfully", Me.Text)
            End If
            'sanjay

            'Dim obj As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.FrmDispatchBulkSale)
            'Dim strotherno As String = Nothing
            'If obj Is Nothing Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If
            'If clsCommon.myLen(obj.mailsubjct) <= 0 Then
            '    clsCommon.MyMessageBoxShow("First do email and sms setting", Me.Text)
            '    Return
            'End If

            'strotherno = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Phone1 from TSPL_CUSTOMER_MASTER where cust_code='" + lblCustomerCode.Text + "'"))

            'Dim strContactPerson As String = String.Empty
            'Dim strSubject As String = obj.mailsubjct.Replace(clsEmailSMSConstants.DeliveryNo, txtDocNo.Value)
            'strSubject = strSubject.Replace(clsEmailSMSConstants.DeliveryDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))

            'Dim strbody As String = obj.mailbody.Replace(clsEmailSMSConstants.DeliveryNo, txtDocNo.Value)
            'strbody = strbody.Replace(clsEmailSMSConstants.DeliveryDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
            'strbody = strbody.Replace(clsEmailSMSConstants.CustomerNo, lblCustomerCode.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.CustomerName, lblCustomerName.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.LocationCode, lblLocationCode.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.LocationName, LblLocationName.Text)
            'strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, clsCommon.myCstr(lblTotRAmt1.Text))
            'strbody = strbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

            'Dim strsmsbody As String = obj.smsbody.Replace(clsEmailSMSConstants.DeliveryNo, txtDocNo.Value)
            'strsmsbody = strsmsbody.Replace(clsEmailSMSConstants.DeliveryDate, clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
            'strsmsbody = strsmsbody.Replace(clsEmailSMSConstants.CustomerNo, lblCustomerCode.Text)
            'strsmsbody = strsmsbody.Replace(clsEmailSMSConstants.CustomerName, lblCustomerName.Text)
            'strsmsbody = strsmsbody.Replace(clsEmailSMSConstants.LocationCode, lblLocationCode.Text)
            'strsmsbody = strsmsbody.Replace(clsEmailSMSConstants.LocationName, LblLocationName.Text)
            'strsmsbody = strsmsbody.Replace(clsEmailSMSConstants.TotalAmount, clsCommon.myCstr(lblTotRAmt1.Text))
            'strsmsbody = strsmsbody.Replace(clsEmailSMSConstants.Form_Code, MyBase.Form_ID)

            'Dim strRptPath As String = String.Empty
            'Dim lstReceiptents As List(Of String) = Nothing
            'Dim strPath As String = String.Empty
            'For Each strUser As String In lstUsers
            '    lstReceiptents = New List(Of String)
            '    Dim qry As String = String.Empty
            '    Dim emailId As String = String.Empty
            '    If isSendForApproval Then
            '        strContactPerson = strUser
            '        qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
            '        emailId = clsDBFuncationality.getSingleValue(qry)
            '    Else
            '        strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
            '        emailId = clsDBFuncationality.getSingleValue("select Email from TSPL_customer_MASTER where cust_code ='" & strUser & "' ")
            '    End If

            '    strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
            '    strsmsbody = strsmsbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)
            '    lstReceiptents.Add(emailId)

            '    strbody = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)
            '    strsmsbody = strsmsbody.Replace(clsEmailSMSConstants.UserCode, strUser)
            'Next
            'clsMailViaOutlook.SendEmail(strSubject, strbody, lstReceiptents, Nothing, strPath)
            'clsSMSSend.SendSMS(clsUserMgtCode.FrmDispatchBulkSale, strsmsbody, strotherno)
            'clsCommon.MyMessageBoxShow("E-Mail/SMS Send Successfully", Me.Text)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub btnSendforApproval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendforApproval.Click
        Dim trans As SqlTransaction = Nothing
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Select Document No. First", Me.Text)
                txtDocNo.Focus()
                txtDocNo.Select()
                Return
            End If

            If Not (common.clsCommon.MyMessageBoxShow("Send E-Mail/SMS Of Respective Delivery No. " + txtDocNo.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                Return
            End If
            LoadData(txtDocNo.Value, NavigatorType.Current)

            Dim qry As String = "Select * from TSPL_APPROVAL_LEVEL_SCREEN where 1=1 and Trans_Code='" + MyBase.Form_ID + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim lstUsers As New List(Of String)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    lstUsers.Add(dr("User_Code").ToString())
                Next
            End If

            If lstUsers.Count = 0 Then
                Throw New Exception("No Receiptent Found")
            End If
            SendEmail(lstUsers, True)
            trans = clsDBFuncationality.GetTransactin()
            qry = "update TSPL_Dispatch_BulkSale set ApprovalRequired='Y',Status='Pending' where Document_No='" & txtDocNo.Value & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            LoadData(txtDocNo.Value, NavigatorType.Current)
        Catch ex As Exception
            'trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub SetMailRight()
        If objCommonVar.IsMailSend Then
            btnSend.Enabled = True
        Else
            btnSend.Enabled = False
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        funPrint(txtDocNo.Value)
    End Sub
    Sub funPrint(ByVal StrCode As String, Optional ByVal isPerformaInvoice As Boolean = False)
        Try
            Dim qry As String = ""
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SAHAYOG") = CompairStringResult.Equal Then
                qry = "Select * from ( Select TSPL_COMPANY_MASTER.Comp_Name as Company_Name,TSPL_CUSTOMER_MASTER.Add1 as ConAddress1,TSPL_CUSTOMER_MASTER.Add2 as ConAddress2,TSPL_CUSTOMER_MASTER.Add3 as ConAddress3,TSPL_LOCATION_MASTER.Add1  as Address1,TSPL_LOCATION_MASTER.Add2 as Address2,TSPL_LOCATION_MASTER.Add3 as Address3, " &
                           " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" &
                            "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" &
                            "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" &
                           "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" &
                            "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end " &
                            "  + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end " &
                            "  +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " &
                            "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " &
                            " as Company_Address," &
                           " TSPL_COMPANY_MASTER .Pincode as PinNo,TSPL_COMPANY_MASTER.CINNo as CinNo,TSPL_Dispatch_BulkSale.Document_No as DispatchNo,Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,106) as Dispatchdate, " &
                           " '' as Suppliersref,TSPL_Dispatch_BulkSale.Document_No  AS DespatchDocumentNo, CityMaster.City_Name as Despatchedthrough,TSPL_CUSTOMER_MASTER.Customer_Name as Consignee,TSPL_CUSTOMER_MASTER.Add1 +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end as Consignee_Address,0 as SL_No, " &
                           " TSPL_Dispatch_BulkSale.Tanker_Code  TankerNo,TSPL_Dispatch_Detail_BulkSale.Qty  as MilkQty,TSPL_Dispatch_Detail_BulkSale.FatPer  as Fatper,TSPL_Dispatch_Detail_BulkSale.SNFPer  as Snfper, TSPL_Dispatch_Detail_BulkSale.NetMilkRate  as Rate, " &
                           " TSPL_Dispatch_Detail_BulkSale.Amount  as Amount,TSPL_Dispatch_BulkSale.Created_By as CreatedBy,TSPL_Dispatch_BulkSale.Modified_By as ModifiedBy, TSPL_Dispatch_BulkSale.Total_Amt as DocumentAmount,TSPL_Dispatch_Detail_BulkSale.StandardRate ,TSPL_CUSTOMER_MASTER.Tin_No as ConsigneeTinno,isnull(TSPL_CUSTOMER_MASTER.PIN_Code,'') as ConsigneePin, '1' as CopyType,TSPL_Dispatch_Detail_BulkSale.Chamber_Desc,TSPL_Dispatch_Detail_BulkSale.Seal_No,TSPL_Dispatch_Detail_BulkSale.Type,TSPL_QC_Parameter_Detail_BulkSale.param_field_Desc,TSPL_QC_Parameter_Detail_BulkSale.Param_Field_Value,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_ITEM_MASTER.Item_Desc from TSPL_Dispatch_Detail_BulkSale " &
                           " Left Outer Join TSPL_Dispatch_BulkSale  on TSPL_Dispatch_Detail_BulkSale.Document_No =TSPL_Dispatch_BulkSale.Document_No Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_Dispatch_BulkSale.Comp_Code  " &
                           " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Dispatch_BulkSale.Location_Code " &
                           " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Dispatch_BulkSale.Customer_Code " &
                           " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " &
                           " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
                           " left outer join TSPL_CITY_MASTER as CityMaster on CityMaster.City_Code=TSPL_CUSTOMER_MASTER .City_Code " &
                           " left join TSPL_QC_Parameter_Detail_BulkSale on TSPL_QC_Parameter_Detail_BulkSale.qc_no=TSPL_Dispatch_BulkSale.qc_code left join TSPL_PARAMETER_MASTER on  TSPL_PARAMETER_MASTER.code=TSPL_QC_Parameter_Detail_BulkSale.Param_Field_Code JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_Dispatch_Detail_BulkSale.Item_Code  " &
                           " where 1=1 and TSPL_Dispatch_BulkSale .Document_No='" + StrCode + "' and TSPL_PARAMETER_MASTER.IsBulkSale=1) XXX LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'Customer Copy' as CopyType1 UNION Select '1' as COL1, 2 as COL2,  'Office Copy' as CopyType1 UNION Select '1' as COL1, 3 as COL2,  'Return Copy' as CopyType1) YYY ON YYY.COL1=XXX.CopyType  ORDER BY YYY.COL2"
            Else
                qry = GetPrintQuery()
                qry += " where 1=1 and TSPL_Dispatch_BulkSale .Document_No='" + StrCode + "'"
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            If isPerformaInvoice = False Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDispatchBulkSale", "Milk Sales Dispatch", clsCommon.myCDate(dt.Rows(0)("Dispatch_date")), "rptCompanyAddress.rpt")
            Else
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDispatchBulkSalePerformaInvoice", "Milk Sales Dispatch", "rptCompanyAddress.rpt")
            End If
            frmCRV = Nothing
            qry = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Function GetPrintQuery() As String
        Dim StrQuery As String = "Select Case when dtax1.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX1_Rate when  dtax2.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX2_Rate when dtax3.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX3_Rate  when dtax4.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX4_Rate  when dtax5.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX5_Rate end as TCS_Rate ," &
                                  " Case when dtax1.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX1_Amt when  dtax2.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX2_Amt when dtax3.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX3_Amt when dtax4.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX4_Amt when dtax5.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX5_Amt end  as TCS_Amount," &
                             " Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as Dispatch_date, TSPL_ITEM_MASTER.HSN_Code,STATEMASTER_CUSTOMER.GST_STATE_Code as CustomerGST_STATE_Code," &
                             "STATEMASTER_LOCATION.GST_STATE_Code as CompGST_STATE_Code,TSPL_CUSTOMER_MASTER.GSTNO AS Consignee_GSTIN_NO,TSPL_LOCATION_MASTER.GSTNO AS Location_GSTIN_NO,TSPL_COMPANY_MASTER.Comp_Name as Company_Name,TSPL_CUSTOMER_MASTER.Add1 as ConAddress1,TSPL_CUSTOMER_MASTER.Add2 as ConAddress2,TSPL_CUSTOMER_MASTER.Add3 as ConAddress3,TSPL_LOCATION_MASTER.Add1  as Address1,TSPL_LOCATION_MASTER.Add2 as Address2,TSPL_LOCATION_MASTER.Add3 as Address3, " &
                             " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" &
                              "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" &
                              "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" &
                             "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" &
                              "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end " &
                              "  + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end " &
                              "  +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " &
                              "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " &
                              " as Company_Address," &
                             " TSPL_COMPANY_MASTER .Pincode as PinNo,TSPL_COMPANY_MASTER.CINNo as CinNo,TSPL_Dispatch_BulkSale.Document_No as DispatchNo,Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,106) as Dispatchdate, " &
                             " '' as Suppliersref,TSPL_Dispatch_BulkSale.Document_No  AS DespatchDocumentNo, CityMaster.City_Name as Despatchedthrough,TSPL_CUSTOMER_MASTER.Customer_Name as Consignee,TSPL_CUSTOMER_MASTER.Add1 +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end as Consignee_Address,TSPL_CUSTOMER_MASTER.State as consignee_state,STATEMASTER_CUSTOMER.STATE_NAME as consignee_statename,0 as SL_No, " &
                             " TSPL_Dispatch_BulkSale.Tanker_Code  TankerNo,TSPL_Dispatch_Detail_BulkSale.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_Dispatch_BulkSale.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name, " &
                             " TSPL_COMPANY_MASTER.Phone1    +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as  Comp_Phone, TSPL_COMPANY_MASTER.Email as Comp_Email,TSPL_COMPANY_MASTER.Tcan_No as Comp_Web, " &
                             " cast(TSPL_COMPANY_MASTER.Tin_No as varchar)   as Comp_Tin_No,cast(TSPL_COMPANY_MASTER.Pan_No as varchar) as Pan_No, " &
                             " cast(TSPL_COMPANY_MASTER.Access_Officer as varchar) as Comp_FSSAI_LIC_NO, " &
                             " cast(TSPL_COMPANY_MASTER.CINNo as varchar) as Comp_Corp_Id_No,Format(TSPL_COMPANY_MASTER.TinNo_Issue_Date,'dd.MM.yyyy') as Comp_DT, " &
                             " TSPL_LOCATION_MASTER.Add1 +case when len(TSPL_LOCATION_MASTER.Add2)>0 then ', '+TSPL_LOCATION_MASTER.Add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code    )>3 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code as varchar)  else ' ' end as Comp_AddressHead,TSPL_Dispatch_BulkSale.Challan_No, " &
                             " TSPL_Dispatch_BulkSale.Tanker_Code  TankerNo,TSPL_Dispatch_Detail_BulkSale.Qty  as MilkQty,TSPL_Dispatch_Detail_BulkSale.FatPer  as Fatper,TSPL_Dispatch_Detail_BulkSale.SNFPer  as Snfper, TSPL_Dispatch_Detail_BulkSale.NetMilkRate  as Rate, " &
                             " TSPL_Dispatch_Detail_BulkSale.Amount  as Amount,TSPL_Dispatch_Detail_BulkSale.CLR ,TSPL_Dispatch_BulkSale.Created_By as CreatedBy,TSPL_Dispatch_BulkSale.Modified_By as ModifiedBy, TSPL_Dispatch_BulkSale.Total_Amt as DocumentAmount,TSPL_Dispatch_Detail_BulkSale.StandardRate ,TSPL_CUSTOMER_MASTER.Tin_No as ConsigneeTinno,isnull(TSPL_CUSTOMER_MASTER.PIN_Code,'') as ConsigneePin,TSPL_Dispatch_BulkSale.EWayBillNo,Convert(varchar,TSPL_Dispatch_BulkSale.EWayBillDate,106) as EWayBillDate  " &
                             " from TSPL_Dispatch_Detail_BulkSale " &
                             " Left Outer Join TSPL_Dispatch_BulkSale  on TSPL_Dispatch_Detail_BulkSale.Document_No =TSPL_Dispatch_BulkSale.Document_No Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_Dispatch_BulkSale.Comp_Code  " &
                             " left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_Dispatch_BulkSale.tax1   left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_Dispatch_BulkSale.tax2   left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_Dispatch_BulkSale.TAX3  left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_Dispatch_BulkSale.tax4 " &
                             " left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code= TSPL_Dispatch_BulkSale.tax5" &
                             " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Dispatch_BulkSale.Location_Code " &
                             " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Dispatch_BulkSale.Customer_Code " &
                             " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " &
                             " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
                             " LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_CUSTOMER ON STATEMASTER_CUSTOMER.State_Code=TSPL_CUSTOMER_MASTER.State" &
                             " LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_LOCATION ON STATEMASTER_LOCATION.State_Code=TSPL_LOCATION_MASTER.State " &
                             " left outer join TSPL_CITY_MASTER as CityMaster on CityMaster.City_Code=TSPL_CUSTOMER_MASTER .City_Code " &
                             " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_Dispatch_Detail_BulkSale.item_code "
        Return StrQuery
    End Function

    'Public Sub funPrintt(ByVal StrCode As String)
    '    Try
    '        'atchqry = GetAttachQry(Code)
    '        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)
    '        Dim strqry As String = "Select TSPL_location_master.gstno,TSPL_Dispatch_Detail_BulkSale.Fat_KG,TSPL_Dispatch_Detail_BulkSale.SNF_KG,TSPL_COMPANY_MASTER.Logo_Img,Case when dtax1.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX1_Rate when  dtax2.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX2_Rate when dtax3.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX3_Rate  when dtax4.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX4_Rate  when dtax5.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX5_Rate end as TCS_Rate , Case when dtax1.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX1_Amt when  dtax2.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX2_Amt when dtax3.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX3_Amt when dtax4.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX4_Amt when dtax5.Is_TCS = 'Y' then TSPL_Dispatch_BulkSale.TAX5_Amt end  as TCS_Amount, Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as Dispatch_date, TSPL_ITEM_MASTER.HSN_Code,STATEMASTER_CUSTOMER.GST_STATE_Code as CustomerGST_STATE_Code,STATEMASTER_LOCATION.GST_STATE_Code as CompGST_STATE_Code,TSPL_CUSTOMER_MASTER.GSTNO AS Consignee_GSTIN_NO,TSPL_LOCATION_MASTER.GSTNO AS Location_GSTIN_NO,TSPL_COMPANY_MASTER.Comp_Name as Company_Name,TSPL_CUSTOMER_MASTER.Add1 as ConAddress1,TSPL_CUSTOMER_MASTER.Add2 as ConAddress2,TSPL_CUSTOMER_MASTER.Add3 as ConAddress3,TSPL_LOCATION_MASTER.Add1  as Address1,TSPL_LOCATION_MASTER.Add2 as Address2,TSPL_LOCATION_MASTER.Add3 as Address3,  TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end   + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end   +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address, TSPL_COMPANY_MASTER .Pincode as PinNo,TSPL_COMPANY_MASTER.CINNo as CinNo,TSPL_Dispatch_BulkSale.Document_No as DispatchNo,Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,106) as Dispatchdate,  '' as Suppliersref,TSPL_Dispatch_BulkSale.Document_No  AS DespatchDocumentNo, CityMaster.City_Name as Despatchedthrough,TSPL_CUSTOMER_MASTER.Customer_Name as Consignee,TSPL_CUSTOMER_MASTER.Add1 +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end +case when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end as Consignee_Address,TSPL_CUSTOMER_MASTER.State as consignee_state,STATEMASTER_CUSTOMER.STATE_NAME as consignee_statename,0 as SL_No,  TSPL_Dispatch_BulkSale.Tanker_Code  TankerNo,TSPL_Dispatch_Detail_BulkSale.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_Dispatch_BulkSale.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,  TSPL_COMPANY_MASTER.Phone1    +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as  Comp_Phone, TSPL_COMPANY_MASTER.Email as Comp_Email,TSPL_COMPANY_MASTER.Tcan_No as Comp_Web,  cast(TSPL_COMPANY_MASTER.Tin_No as varchar)   as Comp_Tin_No,cast(TSPL_COMPANY_MASTER.Pan_No as varchar) as Pan_No,  cast(TSPL_COMPANY_MASTER.Access_Officer as varchar) as Comp_FSSAI_LIC_NO,  cast(TSPL_COMPANY_MASTER.CINNo as varchar) as Comp_Corp_Id_No,Format(TSPL_COMPANY_MASTER.TinNo_Issue_Date,'dd.MM.yyyy') as Comp_DT,  TSPL_LOCATION_MASTER.Add1 +case when len(TSPL_LOCATION_MASTER.Add2)>0 then ', '+TSPL_LOCATION_MASTER.Add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end  + case when len(TSPL_LOCATION_MASTER.Pin_Code    )>3 then ', Pin Code - '+ cast(TSPL_LOCATION_MASTER.Pin_Code as varchar)  else ' ' end as Comp_AddressHead,TSPL_Dispatch_BulkSale.Challan_No,  TSPL_Dispatch_BulkSale.Tanker_Code  TankerNo,TSPL_Dispatch_Detail_BulkSale.Qty  as MilkQty,TSPL_Dispatch_Detail_BulkSale.FatPer  as Fatper,TSPL_Dispatch_Detail_BulkSale.SNFPer  as Snfper, TSPL_Dispatch_Detail_BulkSale.NetMilkRate  as Rate,  TSPL_Dispatch_Detail_BulkSale.Amount  as Amount,TSPL_Dispatch_Detail_BulkSale.CLR ,TSPL_Dispatch_BulkSale.Created_By as CreatedBy,TSPL_Dispatch_BulkSale.Modified_By as ModifiedBy, TSPL_Dispatch_BulkSale.Total_Amt as DocumentAmount,TSPL_Dispatch_Detail_BulkSale.StandardRate ,TSPL_CUSTOMER_MASTER.Tin_No as ConsigneeTinno,isnull(TSPL_CUSTOMER_MASTER.PIN_Code,'') as ConsigneePin,TSPL_Dispatch_BulkSale.EWayBillNo,Convert(varchar,TSPL_Dispatch_BulkSale.EWayBillDate,106) as EWayBillDate   from TSPL_Dispatch_Detail_BulkSale  Left Outer Join TSPL_Dispatch_BulkSale  on TSPL_Dispatch_Detail_BulkSale.Document_No =TSPL_Dispatch_BulkSale.Document_No Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_Dispatch_BulkSale.Comp_Code   left outer join TSPL_TAX_MASTER as dtax1 on dtax1.tax_code =TSPL_Dispatch_BulkSale.tax1   left outer join tspl_tax_master as dtax2 on dtax2.tax_code = TSPL_Dispatch_BulkSale.tax2   left outer join tspl_tax_master as dtax3 on dtax3.Tax_Code=TSPL_Dispatch_BulkSale.TAX3  left outer join TSPL_TAX_MASTER as dtax4 on dtax4.Tax_Code= TSPL_Dispatch_BulkSale.tax4  left outer join TSPL_TAX_MASTER as dtax5 on dtax5.Tax_Code= TSPL_Dispatch_BulkSale.tax5 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Dispatch_BulkSale.Location_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Dispatch_BulkSale.Customer_Code  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State  LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_CUSTOMER ON STATEMASTER_CUSTOMER.State_Code=TSPL_CUSTOMER_MASTER.State LEFT OUTER JOIN TSPL_STATE_MASTER STATEMASTER_LOCATION ON STATEMASTER_LOCATION.State_Code=TSPL_LOCATION_MASTER.State  left outer join TSPL_CITY_MASTER as CityMaster on CityMaster.City_Code=TSPL_CUSTOMER_MASTER .City_Code  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_Dispatch_Detail_BulkSale.item_code where TSPL_Dispatch_BulkSale.Document_No ='" + StrCode + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
    '        If dt.Rows.Count > 0 Then
    '            Dim frmCRV As New frmCrystalReportViewer()
    '            frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "Bulkdispath", "Milk Sales Dispatch", "Bulk dispath")
    '            frmCRV = Nothing
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub


    Private Sub txtStanadardrate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStanadardrate.TextChanged
        For ii As Integer = 0 To gv1.Rows.Count - 1
            UpdateCurrentRow(ii)
        Next
    End Sub

    Private Sub btnUpdateCustomer_Click(sender As Object, e As EventArgs) Handles btnUpdateCustomer.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 And btnPost.Enabled = False And clsCommon.myLen(fndCustomerNo.Value) > 0 Then
                If UpdateCustomerAfterPosting() Then
                    clsCommon.MyMessageBoxShow("Customer updated successfully.")
                    fndCustomerNo.Value = ""
                    fndCustomerNo.Visible = False
                    btnUpdateCustomer.Enabled = False
                    btnUpdateCustomer.Visible = False
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            Else
                clsCommon.MyMessageBoxShow("Please Select Customer first.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub fndCustomerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustomerNo._MYValidating
        fndCustomerNo.Value = clsCustomerMaster.getFinder("", fndCustomerNo.Value, isButtonClicked)
        lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + fndCustomerNo.Value + "' ")
    End Sub

    Private Function UpdateCustomerAfterPosting() As Boolean
        Dim strARInvoiceNo As String = clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_Customer_Invoice_Head where Against_Sale_No in (select Document_No  from TSPL_INVOICE_DETAIL_BULKSALE where Dispatch_Code ='" & txtDocNo.Value & "' )")

        Dim strReceiptNo = clsDBFuncationality.getSingleValue("select Receipt_No from TSPL_RECEIPT_DETAIL where Document_No='" & strARInvoiceNo & "'")
        Dim strInvoiceNo As String = clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_INVOICE_DETAIL_BULKSALE where Dispatch_Code ='" & txtDocNo.Value & "' ")

        Dim strSaleReturnNoAgainstdispatch = clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_SALE_RETURN_MASTER_BULKSALE where DispatchNo ='" & txtDocNo.Value & "'")
        Dim strSaleReturnNoAgainstinvoice = clsDBFuncationality.getSingleValue("Select Document_No  from TSPL_SALE_RETURN_MASTER_BULKSALE where InvoiceNo ='" & strInvoiceNo & "'")

        Dim strRemarks = " AR invoice for customer: " + fndCustomerNo.Value + " - " + lblCustomerName.Text + "  For Sale Invoice No " & strInvoiceNo & " "
        Try


            If clsCommon.myLen(strReceiptNo) > 0 Then
                clsCommon.MyMessageBoxShow("Customer cannot be updated because Receipt has been created for this invoice " & strInvoiceNo)
                'Exit Function
            ElseIf clsCommon.myLen(strSaleReturnNoAgainstinvoice) > 0 Then
                clsCommon.MyMessageBoxShow(" Customer cannot be updated because Sale Return has been created for this invoice " & strSaleReturnNoAgainstinvoice)
                'Exit Function
            ElseIf clsCommon.myLen(strSaleReturnNoAgainstdispatch) > 0 Then
                clsCommon.MyMessageBoxShow(" Customer cannot be updated because Sale Return has been created for this dispatch " & strSaleReturnNoAgainstdispatch)
                'Exit Function
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    '' to update journal master ar invoice and invoice bulk sale table againt invoice
                    Dim qry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & fndCustomerNo.Value & "',CustVend_Name='" & lblCustomerName.Text & "',Remarks='" & strRemarks & "'  where Source_Doc_No='" + clsCommon.myCstr(strARInvoiceNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_Customer_Invoice_Head  set Customer_Code= '" & fndCustomerNo.Value & "',Customer_Name='" & lblCustomerName.Text & "'  where Document_No='" + clsCommon.myCstr(strARInvoiceNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_INVOICE_MASTER_BULKSALE  set Customer_Code= '" & fndCustomerNo.Value & "' where Document_No='" + clsCommon.myCstr(strInvoiceNo) + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '' to update journal master ,inventory and dispatch bulk sale table againt dispatch( multiple for one invoice)
                    qry = "update TSPL_JOURNAL_MASTER  set CustVend_Code= '" & fndCustomerNo.Value & "',CustVend_Name='" & lblCustomerName.Text & "'  where Source_Doc_No in ( Select distinct Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No=  (Select Document_No  from TSPL_INVOICE_DETAIL_BULKSALE where Dispatch_Code ='" & txtDocNo.Value & "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_INVENTORY_MOVEMENT_NEW  set Cust_Code= '" & fndCustomerNo.Value & "',Cust_Name='" & lblCustomerName.Text & "'  where Source_Doc_No in ( Select distinct Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No=  (Select Document_No  from TSPL_INVOICE_DETAIL_BULKSALE where Dispatch_Code ='" & txtDocNo.Value & "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_Dispatch_BulkSale  set Customer_Code= '" & fndCustomerNo.Value & "',Is_Update_Customer=1  where Document_No in ( Select distinct Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No=  (Select Document_No  from TSPL_INVOICE_DETAIL_BULKSALE where Dispatch_Code ='" & txtDocNo.Value & "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    '' to update gate entry, qc,gate out  table againt dispatch( multiple for one invoice)

                    qry = "update TSPL_Quality_Check_BulkSale set Customer_Code ='" & fndCustomerNo.Value & "' where QC_No in (Select QC_Code  from TSPL_Dispatch_BulkSale  where Document_No in  (Select distinct Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No=  (Select Document_No  from TSPL_INVOICE_DETAIL_BULKSALE where Dispatch_Code ='" & txtDocNo.Value & "')))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_GATEENTRY_SALE set Customer_Code='" & fndCustomerNo.Value & "' where Document_No in (Select GateEntry_Document_No  from TSPL_Quality_Check_BulkSale where QC_No in (Select QC_Code  from TSPL_Dispatch_BulkSale  where Document_No in  (Select distinct Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No=  (Select Document_No  from TSPL_INVOICE_DETAIL_BULKSALE where Dispatch_Code ='" & txtDocNo.Value & "'))))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "update TSPL_GATEOUT_SALE set Customer_Code ='" & fndCustomerNo.Value & "' where GateEntryNo in  (Select GateEntry_Document_No  from TSPL_Quality_Check_BulkSale where QC_No in (Select QC_Code  from TSPL_Dispatch_BulkSale  where Document_No in  (Select distinct Dispatch_Code  from TSPL_INVOICE_DETAIL_BULKSALE where Document_No=  (Select Document_No  from TSPL_INVOICE_DETAIL_BULKSALE where Dispatch_Code ='" & txtDocNo.Value & "'))))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    trans.Commit()
                End If
                'Return True
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
        Return True
    End Function


    Private Sub btn_printproforma_Click(sender As Object, e As EventArgs) Handles btn_printproforma.Click
        funPrint(txtDocNo.Value, True)
    End Sub

    Private Sub TxtFatWeightage_TextChanged(sender As Object, e As EventArgs) Handles TxtFatWeightage.TextChanged
        If ApplyTSPriceAtBulkSale = True Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
        End If
    End Sub

    Private Sub TxtSNFWeightage_TextChanged(sender As Object, e As EventArgs) Handles TxtSNFWeightage.TextChanged
        If ApplyTSPriceAtBulkSale = True Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
        End If
    End Sub

    Private Sub txtfatRatio_TextChanged(sender As Object, e As EventArgs) Handles txtfatRatio.TextChanged
        If ApplyTSPriceAtBulkSale = True Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
        End If
    End Sub

    Private Sub txtSNFRatio_TextChanged(sender As Object, e As EventArgs) Handles txtSNFRatio.TextChanged
        If ApplyTSPriceAtBulkSale = True Then
            For ii As Integer = 0 To gv1.Rows.Count - 1
                UpdateCurrentRow(ii)
            Next
        End If
    End Sub
    ' Ticket No : TEC/29/10/18-000350 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtDocNo.Value)
    End Sub
    ''richa ERO/11/01/19-000467-------------------14 Jan ,2019
    Private Sub btnReverseAndUnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseAndUnpost.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If ClsDispatchBulkSale.ReverseAndUnpost(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow("Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTaxGroup._MYValidating
        Try
            If TCSTaxApplicableOnbulkSale = True Then
                If clsCommon.myLen(lblLocationCode.Text) > 0 Then
                    Dim strCustomer As String = ""
                    If clsCommon.myLen(strCustomer) <= 0 Then
                        strCustomer = lblCustomerCode.Text
                    End If

                    txtTaxGroup.Value = clsLocationWiseTax.GetExempedDefaultTaxGroup(True, lblLocationCode.Text, strCustomer, "S", txtDate.Value, "Y")
                    lblTaxGrpName.Text = clsTaxGroupMaster.GetNameOfSaleType(txtTaxGroup.Value, Nothing)


                    SetTaxDetails()

                Else
                    Throw New Exception("Please select Tanker First")
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

                                    If EnableTCSRateValidityFrom01July2021 Then
                                        Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & lblCustomerCode.Text & "'")) = 1, True, False)
                                        If Is_ITR_Filled_And_TCSAmountGreater50K = True Then
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                        Else
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                        End If
                                    Else
                                        Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & lblCustomerCode.Text & "'"))
                                        If clsCommon.myLen(panno) > 0 Then
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                        Else
                                            gv2.Rows(gv2.Rows.Count - 1).Cells(colTTaxRate).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                        End If
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

                                            If EnableTCSRateValidityFrom01July2021 Then
                                                Dim Is_ITR_Filled_And_TCSAmountGreater50K As Boolean = IIf(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT CASE WHEN ISNULL(IsTCSGreaterthan50K,0)=1 AND ISNULL(IsITRfilledinLast2Years,0)=1 THEN 1 ELSE 0 END FROM TSPL_CUSTOMER_MASTER WHERE Cust_Code='" & lblCustomerCode.Text & "'")) = 1, True, False)
                                                If Is_ITR_Filled_And_TCSAmountGreater50K = True Then
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                                Else
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                                End If
                                            Else
                                                Dim panno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(pan,'')+isnull(Additional3 ,'') as PanNoAdhar from tspl_customer_master where cust_code='" & lblCustomerCode.Text & "'"))
                                                If clsCommon.myLen(panno) > 0 Then
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithPanNo, clsFixedParameterCode.TCSRateforCustomerWithPanNo, Nothing))
                                                Else
                                                    gv1.Rows(intRowNo).Cells(clsCommon.myCstr("colTaxRate" + strII)).Value = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TCSRateforCustomerWithoutPanNo, clsFixedParameterCode.TCSRateforCustomerWithoutPanNo, Nothing))
                                                End If
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
        repoTaxAmt.ReadOnly = True
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
            If TCSTaxApplicableOnbulkSale Then
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


End Class
