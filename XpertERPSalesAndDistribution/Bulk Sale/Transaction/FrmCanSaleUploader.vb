'--------Created By Richa 15/09/2017 ,ERO/15/05/18-000310

Imports System.Data.SqlClient
Imports common
Imports System.IO


Public Class FrmCanSaleUploader
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
    Public TextCol As GridViewTextBoxColumn = Nothing
    Public ChkBoxColumn As GridViewCheckBoxColumn = Nothing
    Public ValidatedCount As Integer = 0
    Dim dtmain As DataTable = Nothing
    Public Const colSlNo As String = "SL"
    Public Const colDocDate As String = "Date"
    Public Const colCustCode As String = "Distributor"
    Public Const colItemCode As String = "ItemCode"
    Public Const colIsValidated As String = "colIsValidated"
    Public Const colErrorStatus As String = "colErrorStatus"
    Public Const colNoOfCans As String = "No of cans"
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
    Dim arrLoc As String = Nothing
    Public arrList As List(Of String) = Nothing
    Public arrListDesc As List(Of String) = Nothing
    Public Shared Alocation As String = Nothing
    Public DocumentNo As String = ""
    Dim SettAvgFATSNFPerForCanSale As Boolean = False
    Dim CreateSeparateInvoiceforeachrowinCansale As Boolean = False
    Dim Qry As String
#End Region
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmDispatchBulkSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SettAvgFATSNFPerForCanSale = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CanSaleAvgFATSNFPer, clsFixedParameterCode.CanSaleAvgFATSNFPer, Nothing)) = 1)
        CreateSeparateInvoiceforeachrowinCansale = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateSeparateInvoiceforeachrowinCansale, clsFixedParameterCode.CreateSeparateInvoiceforeachrowinCansale, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        'SetMailRight()
        Reset()
        'SetMaxlength()
        rdbEnterDataManual.IsChecked = False
        rdbEnterDataManual.Visible = True
        rdbImportDataFromSheet.IsChecked = True
        UcAttachment1.Form_ID = MyBase.Form_ID
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N New Transaction")

    End Sub

    Private Sub FrmDispatchBulkSale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            ' SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            ' PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()

        End If
    End Sub
    Private Sub CloseForm()
        clsERPFuncationality.closeForm(Me)
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                Dim LocationName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_LOCATION_MASTER where Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and Location_Code ='" & obj.Default_LocCode & "'"))
                If clsCommon.myLen(LocationName) > 0 Then
                    fndLocation.Value = obj.Default_LocCode
                    LblLocationName.Text = obj.Default_LocName

                Else
                    fndLocation.Value = ""
                    LblLocationName.Text = ""
                End If
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
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

        Dim ChkBoxColumn As New GridViewCheckBoxColumn()
        ChkBoxColumn.Name = colIsValidated
        ChkBoxColumn.HeaderText = "Validated"
        ChkBoxColumn.ReadOnly = True
        gv1.Columns.Add(ChkBoxColumn)

        Dim TextCol As New GridViewTextBoxColumn()
        TextCol.Name = colErrorStatus
        TextCol.HeaderText = "Error Status"
        TextCol.ReadOnly = True
        gv1.Columns.Add(TextCol)


        Dim DocDate As New GridViewDateTimeColumn()
        DocDate.FormatString = ""
        DocDate.HeaderText = "Date"
        DocDate.Name = colDocDate
        DocDate.Width = 100
        DocDate.ReadOnly = False
        DocDate.WrapText = True
        DocDate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(DocDate)

        Dim CustCode As New GridViewTextBoxColumn()
        CustCode.FormatString = ""
        CustCode.HeaderText = "Distributor"
        CustCode.Name = colCustCode
        CustCode.Width = 100
        CustCode.ReadOnly = False
        CustCode.WrapText = True
        CustCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(CustCode)

        Dim ItemCode As New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 100
        ItemCode.ReadOnly = False
        ItemCode.WrapText = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ItemCode)

        Dim NoOfCans As New GridViewDecimalColumn
        NoOfCans.FormatString = "{0:n3}"
        NoOfCans.HeaderText = "No of Cans"
        NoOfCans.DecimalPlaces = 0
        NoOfCans.Name = colNoOfCans
        NoOfCans.Width = 120
        NoOfCans.ReadOnly = False
        NoOfCans.WrapText = True
        NoOfCans.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(NoOfCans)

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


        Dim fatper As New GridViewDecimalColumn
        fatper.FormatString = "{0:n2}"
        fatper.HeaderText = "FAT %"
        fatper.Name = colFatPer
        fatper.Width = 75
        fatper.ReadOnly = SettAvgFATSNFPerForCanSale
        fatper.WrapText = True
        fatper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(fatper)



        Dim snfper As New GridViewDecimalColumn
        snfper.FormatString = "{0:n2}"
        snfper.HeaderText = "SNF %"
        snfper.Name = colSNFPer
        snfper.Width = 75
        snfper.ReadOnly = SettAvgFATSNFPerForCanSale
        snfper.WrapText = True
        snfper.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(snfper)

        Dim netmilkRate As New GridViewDecimalColumn
        netmilkRate.FormatString = "{0:n2}"
        netmilkRate.HeaderText = "Milk Rate"
        netmilkRate.Name = colMilkRate
        netmilkRate.Width = 75
        netmilkRate.ReadOnly = False
        netmilkRate.WrapText = True
        netmilkRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(netmilkRate)

        ''=================================================

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


        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnReorder = True
        gv1.Enabled = True
        ' gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.Rows.AddNew()
        gv1.Rows(0).Cells(colSlNo).Value = "1"
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ReadOnly = False
        lineNo = Nothing
        CustCode = Nothing

    End Sub


    Sub Reset()
        fndLocation.Value = ""
        LblLocationName.Text = ""
        FndPriceCode.Value = ""
        TxtFatWeightage.Value = 0
        TxtSNFWeightage.Value = 0
        txtfatRatio.Value = 0
        txtSNFRatio.Value = 0
        txtStanadardrate.Value = 0
        TxtToleranceinplus.Value = 0
        txtToleranceinminus.Value = 0
        TxtUOM.Value = ""
        rdbImportDataFromSheet.IsChecked = True
        ChkCanInventoryType.Checked = False
        btnsave.Text = "Save"
        btnPost.Enabled = True
        btnsave.Enabled = True
        UcAttachment1.BlankAllControls()
        'loadBlankItemGrid()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        'ReStoreGridLayout()
        isNewEntry = True
        LOCATIONRIGTHS()
    End Sub

    'Private Sub fndCustomerNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCustomerNo._MYValidating
    '    fndCustomerNo.Value = clsCustomerMaster.getFinder("", "", isButtonClicked)
    '    If clsCommon.myLen(fndCustomerNo.Value) > 0 Then
    '        lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + fndCustomerNo.Value + "' ")
    '    Else
    '        lblCustomerName.Text = ""
    '    End If
    'End Sub


    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If rdbEnterDataManual.IsChecked Then
                If Not isInsideLoadData Then
                    If Not isCellValueChangedOpen Then
                        isCellValueChangedOpen = True
                        'If e.Column Is gv1.Columns(colFatPer) Or e.Column Is gv1.Columns(colSNFPer) Or e.Column Is gv1.Columns(colStandardRate) Then
                        If e.Column Is gv1.Columns(colFatPer) Or e.Column Is gv1.Columns(colSNFPer) Or e.Column Is gv1.Columns(colNoOfCans) Or e.Column Is gv1.Columns(colQty) Or e.Column Is gv1.Columns(colMilkRate) Then
                            'For ii As Integer = 0 To gv1.Rows.Count - 1
                            '    UpdateCurrentRow(ii)
                            'Next
                        ElseIf e.Column Is gv1.Columns(colCustCode) Then
                            OpenCustomerList(False)
                            'ElseIf e.Column Is gv1.Columns(colItemCode) Then
                            '    OpenCustomerList(False)
                        ElseIf e.Column Is gv1.Columns(colItemCode) Then
                            OpenItemList(False)
                        End If
                    End If
                    'UpdateAllTotals()
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenCustomerList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value)
        If clsCommon.myLen(strICode) > 0 Then

            gv1.CurrentRow.Cells(colCustCode).Value = clsCustomerMaster.getFinder(" isnull(Manual_Customer,'')='Y' and isnull(status,'')='N' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCustCode).Value), isButtonClick)
        End If
        strICode = Nothing
    End Sub
    Sub OpenItemList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            gv1.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder(" Product_Type ='MI' and Active=1 ", clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), isButtonClick)
        End If
        strICode = Nothing
    End Sub
    'Private Sub UpdateAllTotals()
    '    Try
    '        Dim dblTotAmt As Double = 0
    '        For ii As Integer = 0 To gv1.Rows.Count - 1
    '            If (clsCommon.myLen(gv1.Rows(ii).Cells(colItemCode).Value) > 0) Then
    '                dblTotAmt = dblTotAmt + clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmount).Value)
    '            End If
    '        Next

    '        lblTotRAmt1.Text = clsCommon.myFormat(dblTotAmt)
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    'Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
    '    Try

    '        Dim FatRate As Double = 0
    '        Dim SNFRate As Double = 0
    '        Dim StandardRate As Double = 0
    '        Dim FatWeightage As Double = 0
    '        Dim SNFWeightage As Double = 0
    '        Dim FatPer As Double = 0
    '        Dim SNFPer As Double = 0
    '        Dim FatRatio As Double = 0
    '        Dim SNFRatio As Double = 0
    '        Dim FatKG As Double = 0
    '        Dim SNFKG As Double = 0
    '        Dim Qty As Double = 0
    '        Dim FatAmount As Double = 0
    '        Dim SNFAmount As Double = 0
    '        Dim Amount As Double = 0
    '        If ApplyTSPriceAtBulkSale = True Then
    '            StandardRate = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colStandardRate).Value)
    '        Else
    '            StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)
    '        End If
    '        FatWeightage = clsCommon.myCdbl(TxtFatWeightage.Value)
    '        SNFWeightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
    '        'FatWeightage = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatWeightage).Value)
    '        'SNFWeightage = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSnfWeightage).Value)
    '        FatPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatPer).Value)
    '        SNFPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFPer).Value)
    '        FatRatio = clsCommon.myCdbl(txtfatRatio.Value)
    '        SNFRatio = clsCommon.myCdbl(txtSNFRatio.Value)

    '        'FatRatio = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatRatio).Value)
    '        'SNFRatio = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFRatio).Value)
    '        'Qty = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
    '        Qty = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), 3)

    '        FatRate = Math.Round(StandardRate * FatWeightage / FatRatio, 2)
    '        SNFRate = Math.Round(StandardRate * SNFWeightage / SNFRatio, 2)
    '        'FatRate = Math.Floor(StandardRate * FatWeightage / FatRatio * 100) / 100
    '        'SNFRate = Math.Floor(StandardRate * SNFWeightage / SNFRatio * 100) / 100

    '        'FatKG = (Qty * FatPer) / 100
    '        'SNFKG = (Qty * SNFPer) / 100

    '        FatKG = Math.Round(((Qty * FatPer) / 100), 3)
    '        SNFKG = Math.Round(((Qty * SNFPer) / 100), 3)

    '        FatAmount = Math.Round(FatRate * FatKG, 2)
    '        SNFAmount = Math.Round(SNFRate * SNFKG, 2)
    '        'FatAmount = Math.Floor(FatRate * FatKG * 100) / 100
    '        'SNFAmount = Math.Floor(SNFRate * SNFKG * 100) / 100

    '        Amount = FatAmount + SNFAmount

    '        gv1.Rows(IntRowNo).Cells(colFatRate).Value = clsCommon.myCdbl(FatRate)
    '        gv1.Rows(IntRowNo).Cells(colSNFRate).Value = clsCommon.myCdbl(SNFRate)
    '        gv1.Rows(IntRowNo).Cells(colFatKG).Value = clsCommon.myCdbl(FatKG)
    '        gv1.Rows(IntRowNo).Cells(colSNFKG).Value = clsCommon.myCdbl(SNFKG)
    '        gv1.Rows(IntRowNo).Cells(ColFatAmount).Value = clsCommon.myCdbl(FatAmount)
    '        gv1.Rows(IntRowNo).Cells(ColSNFAmount).Value = clsCommon.myCdbl(SNFAmount)
    '        gv1.Rows(IntRowNo).Cells(colAmount).Value = clsCommon.myCdbl(Amount)
    '        ''richa Against ticket No BM00000003893 on 11/09/2014
    '        If Qty > 0 Then
    '            If ApplyTSPriceAtBulkSale = True Then
    '                gv1.Rows(IntRowNo).Cells(colNetMilkRate).Value = (StandardRate * (FatPer + SNFPer)) / 100
    '                gv1.Rows(IntRowNo).Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colNetMilkRate).Value) * Qty, 2)
    '            Else
    '                gv1.Rows(IntRowNo).Cells(colNetMilkRate).Value = Math.Round(clsCommon.myCdbl(Amount / Qty), 2)
    '                'gv1.Rows(IntRowNo).Cells(colNetMilkRate).Value = Math.Floor(clsCommon.myCdbl(Amount / Qty) * 100) / 100
    '            End If
    '        End If
    '        ''==============================================
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub




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
    'Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
    '    Dim objApproval As New clsApply_Approval()
    '    Dim dt As DataTable = Nothing
    '    Dim obj As ClsDispatchBulkSale = Nothing
    '    Try
    '        obj = ClsDispatchBulkSale.GetData(strCode, arrLoc, NavTyep)
    '        If ApplyMultiChamber Then
    '            gv1.Rows.Clear()
    '        End If

    '        isInsideLoadData = True
    '        If obj IsNot Nothing Then
    '            isNewEntry = False
    '            txtDocNo.Value = obj.Document_No
    '            txtDate.Value = obj.Document_Date
    '            lblCustomerCode.Text = obj.Customer_Code
    '            lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code='" + lblCustomerCode.Text + "'")
    '            FndTankerCode.Value = obj.Tanker_Code

    '            'LblTankerName.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_TANKER_MASTER where Tanker_No  ='" + FndTankerCode.Value + "' ")
    '            LblQCCode.Text = obj.QC_Code
    '            TxtChallanNo.Text = obj.Challan_No
    '            txtinsuranceno.Text = obj.Insurance_No
    '            txtsealno.Text = obj.Seal_No
    '            lblLocationCode.Text = obj.Location_Code
    '            LblLocationName.Text = clsDBFuncationality.getSingleValue("Select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" + lblLocationCode.Text + "' ")
    '            txtGrossWeight.Value = obj.Gross_Weight
    '            TxtTareWeight.Value = obj.Tare_Weight
    '            txtNetWeight.Value = obj.Net_Weight
    '            FndPriceCode.Value = obj.Price_Code
    '            lblTotRAmt1.Text = obj.Total_Amt
    '            TxtEWayBillNo.Text = obj.EWayBillNo
    '            If obj.EWayBillDate IsNot Nothing Then
    '                txtewaybilldate.Value = obj.EWayBillDate
    '                txtewaybilldate.Checked = True
    '            Else
    '                txtewaybilldate.Value = clsCommon.GETSERVERDATE()
    '                txtewaybilldate.Checked = False
    '            End If
    '            If obj.Is_Create_Auto_Invoice = 0 Then
    '                chkCreateAoutoInvoice.Checked = False
    '            Else
    '                chkCreateAoutoInvoice.Checked = True
    '            End If
    '            'txtCreditLimit.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code='" & obj.Customer_Code & "'"))
    '            TxtFatWeightage.Text = obj.Fat_Weightage
    '            TxtSNFWeightage.Text = obj.Snf_Weightage
    '            txtfatRatio.Text = obj.Fat_Ratio
    '            txtSNFRatio.Text = obj.Snf_Ratio
    '            If obj.arrDispatchDetailBulkSale IsNot Nothing AndAlso obj.arrDispatchDetailBulkSale.Count > 0 Then
    '                For Each objTr As clsDispatchDetailBulkSale In obj.arrDispatchDetailBulkSale
    '                    ' gv1.Rows.AddNew()
    '                    If ApplyMultiChamber Then
    '                        gv1.Rows.AddNew()
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = gv1.Rows.Count
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ")
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = objTr.Unit_code
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSNCode).Value = objTr.HSN_code
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatPer).Value = objTr.FatPer
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFPer).Value = objTr.SNFPer
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = objTr.CLR
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatKG).Value = objTr.Fat_KG
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFKG).Value = objTr.SNF_KG
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColFatAmount).Value = objTr.FatAmount
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNFAmount).Value = objTr.SNFAmount
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNetMilkRate).Value = objTr.NetMilkRate
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
    '                        If ApplyTSPriceAtBulkSale = True Then
    '                            gv1.Rows(0).Cells(colStandardRate).Value = objTr.StandardRate
    '                        End If
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFatRate).Value = objTr.FatRate
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFRate).Value = objTr.SNFRate
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colChamberSeal).Value = objTr.Seal_No
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colType).Value = objTr.Type
    '                        txtStanadardrate.Value = objTr.StandardRate
    '                        Dim Qry As String = Nothing
    '                        Dim strpivotcol As String = Nothing
    '                        Qry = "select stuff((select DISTINCT ',[' + TSPL_QC_Parameter_Detail_BulKSALE.Param_Field_Desc+']'  from TSPL_QC_Parameter_Detail_BulKSALE for xml path('')  ),1,1,'')"
    '                        strpivotcol = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry))
    '                        If clsCommon.myLen(strpivotcol) > 0 Then
    '                            Qry = "select Item_Code,Type,Fat,SNF,CLR,Unit_Code,Chamber_Desc,NetWeight," + strpivotcol + " from (select (TSPL_QC_Parameter_Detail_BulKSALE.Item_Code) as Item_Code,(TSPL_ITEM_MASTER.Product_Type) AS Type,(TSPL_QC_Parameter_Detail_BulKSALE.Fat) as Fat ,(TSPL_QC_Parameter_Detail_BulKSALE.SNF) as SNF,(TSPL_QC_Parameter_Detail_BulKSALE.CLR) as CLR,(TSPL_QC_Parameter_Detail_BulKSALE.Unit_Code) as Unit_Code,TSPL_QC_Parameter_Detail_BulKSALE.Chamber_Desc,(xx.Net_Weight) as NetWeight ,TSPL_QC_Parameter_Detail_BulKSALE.Param_Field_Desc,TSPL_QC_Parameter_Detail_BulKSALE.Param_Field_Value" & _
    '                                    " from TSPL_Quality_Check_BulkSale  left outer join TSPL_QC_Parameter_Detail_BulKSALE on TSPL_Quality_Check_BulkSale.QC_No=TSPL_QC_Parameter_Detail_BulKSALE.QC_No left outer join (select (TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Tare_Weight) as Tare_Weight,(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Gross_Weight) as Gross_Weight,(TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Net_Weight)as Net_Weight,TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Chamber_Desc from TSPL_WEIGHMENT_DETAIL_BULKSALE  left outer join TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS on TSPL_WEIGHMENTBULKSALE_CHEMBER_DETAILS.Weighment_No=TSPL_WEIGHMENT_DETAIL_BULKSALE.Weighment_No where TSPL_WEIGHMENT_DETAIL_BULKSALE.GateEntry_Document_No=(Select TSPL_Quality_Check_BulkSale.GateEntry_Document_No as GateEntryNo from TSPL_Quality_Check_BulkSale where  TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "')) as xx on xx.Chamber_Desc=TSPL_QC_Parameter_Detail_BulKSALE.Chamber_Desc left outer join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_QC_Parameter_Detail_BulKSALE.Item_Code where TSPL_Quality_Check_BulkSale.QC_No='" + LblQCCode.Text + "' and xx.Chamber_Desc='" + clsCommon.myCstr(objTr.Chamber_Desc) + "' ) as st " & _
    '                                    " PIVOT ( MAX(PARAM_FIELD_VALUE) FOR PARAM_FIELD_DESC IN (" + strpivotcol + ")) AS PT ORDER BY Chamber_Desc"
    '                        End If
    '                        dt = clsDBFuncationality.GetDataTable(Qry)
    '                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                            For Each dr As DataRow In dt.Rows
    '                                Dim k As Integer = 0
    '                                For k = 0 To arrList.Count - 1
    '                                    gv1.Rows(gv1.Rows.Count - 1).Cells(arrList(k)).Value = clsCommon.myCstr(dr(arrListDesc(k)))
    '                                Next
    '                            Next
    '                        End If
    '                    Else
    '                        gv1.Rows(0).Cells(colSlNo).Value = "1"
    '                        gv1.Rows(0).Cells(colItemCode).Value = objTr.Item_Code
    '                        gv1.Rows(0).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "' ")
    '                        gv1.Rows(0).Cells(colUnitCode).Value = objTr.Unit_code
    '                        gv1.Rows(0).Cells(colHSNCode).Value = objTr.HSN_code
    '                        gv1.Rows(0).Cells(colQty).Value = objTr.Qty
    '                        gv1.Rows(0).Cells(colFatPer).Value = objTr.FatPer
    '                        gv1.Rows(0).Cells(colSNFPer).Value = objTr.SNFPer
    '                        gv1.Rows(0).Cells(colCLR).Value = objTr.CLR
    '                        gv1.Rows(0).Cells(colFatKG).Value = objTr.Fat_KG
    '                        gv1.Rows(0).Cells(colSNFKG).Value = objTr.SNF_KG
    '                        gv1.Rows(0).Cells(ColFatAmount).Value = objTr.FatAmount
    '                        gv1.Rows(0).Cells(ColSNFAmount).Value = objTr.SNFAmount
    '                        gv1.Rows(0).Cells(colNetMilkRate).Value = objTr.NetMilkRate
    '                        gv1.Rows(0).Cells(colAmount).Value = objTr.Amount
    '                        If ApplyTSPriceAtBulkSale = True Then
    '                            gv1.Rows(0).Cells(colStandardRate).Value = objTr.StandardRate
    '                        End If
    '                        gv1.Rows(0).Cells(colFatRate).Value = objTr.FatRate
    '                        gv1.Rows(0).Cells(colSNFRate).Value = objTr.SNFRate
    '                        txtStanadardrate.Value = objTr.StandardRate
    '                    End If

    '                    If clsCommon.myLen(FndPriceCode.Value) > 0 Then
    '                        dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Ratio,Snf_Ratio,Standard_Rate,TolerancePerPlus,TolerancePerMinus from TSPL_BulkSalePrice_MASTER where Price_Code='" + FndPriceCode.Value + "'")
    '                        If dt.Rows.Count > 0 Then
    '                            'gv1.Rows(0).Cells(colFatWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
    '                            'gv1.Rows(0).Cells(colSnfWeightage).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
    '                            'gv1.Rows(0).Cells(colFatRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
    '                            'gv1.Rows(0).Cells(colSNFRatio).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
    '                            'gv1.Rows(0).Cells(colStandardRate).Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
    '                            'UpdateCurrentRow(0)
    '                            ''richa agarwal 18/12/2014
    '                            TxtFatWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
    '                            TxtSNFWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
    '                            txtfatRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
    '                            txtSNFRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
    '                            'txtStanadardrate.Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
    '                            TxtToleranceinplus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
    '                            txtToleranceinminus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
    '                            UpdateCurrentRow(0)
    '                            ''------------------------
    '                        End If
    '                    End If
    '                Next
    '            Else
    '                gv1.DataSource = Nothing
    '            End If
    '            txtDocNo.MyReadOnly = True
    '            btnsave.Text = "Update"
    '            ' btndelete.Enabled = True
    '            If clsCommon.CompairString(obj.Approved.ToUpper(), "Y".ToUpper()) = CompairStringResult.Equal Then
    '                txtCreditLimit.Enabled = True
    '                txtCreditLimit.Value = 0
    '            End If
    '            'If clsCommon.CompairString(obj.Status, "Posted") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
    '            '    btnsave.Enabled = False
    '            '    btndelete.Enabled = False
    '            '    btnPost.Enabled = False
    '            'Else
    '            If clsCommon.CompairString(obj.Status, "Pending") = CompairStringResult.Equal Then
    '                btndelete.Enabled = False
    '                btnPost.Enabled = False
    '                'Else
    '                '    btnsave.Enabled = True
    '                '    btndelete.Enabled = True
    '                '    btnPost.Enabled = True
    '            End If

    '            If clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
    '                btnPost.Enabled = False
    '                btnsave.Enabled = False
    '                btndelete.Enabled = False
    '                UsLock1.Status = ERPTransactionStatus.Approved
    '            Else
    '                btnPost.Enabled = True
    '                btnsave.Enabled = True
    '                btndelete.Enabled = True
    '                UsLock1.Status = ERPTransactionStatus.Pending
    '            End If
    '            If clsCommon.CompairString(obj.ReverseFlag, "Y") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Posted, "0") = CompairStringResult.Equal Then
    '                btndelete.Enabled = False
    '            End If


    '            UcAttachment1.LoadData(obj.Document_No)
    '        Else
    '            Reset()

    '        End If
    '        '=====================if document go for approval then no post button visible or if document contain related setting
    '        If AllowNLevel Then
    '            objApproval = New clsApply_Approval()
    '            objApproval.DocCode = txtDocNo.Value
    '            objApproval.TotAmt = clsCommon.myCdbl(lblTotRAmt1.Text)
    '            objApproval.CustCode = lblCustomerCode.Text
    '            btnPost.Visible = MyBase.isPostFlag

    '            If Not clsApply_Approval.Visibility_PostButtonForApproval(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(txtDocNo.Value), clsCommon.myCdbl(lblTotRAmt1.Text), 0, "", objApproval) Then
    '                btnPost.Visible = False
    '                If UsLock1.Status = ERPTransactionStatus.Pending Then
    '                    UsLock1.Status = clsApply_Approval.ApprovalCondCheck_Doc(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(txtDocNo.Value), Nothing)
    '                End If
    '            End If
    '        End If
    '        '============================================
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    Finally
    '        isInsideLoadData = False
    '        dt = Nothing
    '        obj = Nothing
    '    End Try
    'End Sub
    'Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
    '    Try
    '        SaveData()
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
    '    End Try

    'End Sub

    'Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
    '    Dim qry As String = String.Empty
    '    Try
    '        qry = "select count(*) from TSPL_Dispatch_BulkSale where Document_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
    '        Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    '        If check > 0 Then
    '            txtDocNo.MyReadOnly = True
    '        ElseIf check <= 0 Then
    '            txtDocNo.MyReadOnly = False
    '        End If

    '        LoadData(txtDocNo.Value, NavType)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    Finally
    '        qry = Nothing
    '    End Try
    'End Sub

    'Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
    '    'Dim qry As String = "Select Document_No as Code,Document_Date from TSPL_Dispatch_BulkSale "
    '    'Dim qry As String = "Select TSPL_Dispatch_BulkSale.Document_No as Code,Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as [Dispatch Date],TSPL_Dispatch_BulkSale.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_Dispatch_BulkSale.Tanker_Code as [Tanker Code],TSPL_TANKER_MASTER.Tanker_Name as [Tanker Name],TSPL_Dispatch_BulkSale.QC_Code as [QC Code],TSPL_Dispatch_BulkSale.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Dispatch_BulkSale.Price_Code as [Price Code],TSPL_Dispatch_BulkSale.Dip_marking as [Dip Marking],TSPL_Dispatch_BulkSale.Challan_No as [Challan No],case when TSPL_Dispatch_BulkSale.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_Dispatch_BulkSale left outer Join TSPL_CUSTOMER_MASTER on TSPL_Dispatch_BulkSale.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_Dispatch_BulkSale.Location_Code =TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_TANKER_MASTER on TSPL_Dispatch_BulkSale.Tanker_Code=TSPL_TANKER_MASTER.Tanker_No"
    '    Dim qry As String = "Select TSPL_Dispatch_BulkSale.Document_No as Code,Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,103) as [Dispatch Date],TSPL_Dispatch_BulkSale.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_Dispatch_BulkSale.Tanker_Code as [Tanker Code],TSPL_Dispatch_BulkSale.QC_Code as [QC Code],TSPL_Dispatch_BulkSale.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Dispatch_BulkSale.Price_Code as [Price Code],TSPL_Dispatch_BulkSale.Dip_marking as [Dip Marking],TSPL_Dispatch_BulkSale.Challan_No as [Challan No],case when TSPL_Dispatch_BulkSale.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_Dispatch_BulkSale left outer Join TSPL_CUSTOMER_MASTER on TSPL_Dispatch_BulkSale.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_Dispatch_BulkSale.Location_Code =TSPL_LOCATION_MASTER.Location_Code"
    '    txtDocNo.Value = clsCommon.ShowSelectForm("DispatchBulkSale", qry, "Code", " TSPL_Dispatch_BulkSale.Location_Code in (" + arrLoc + ")", txtDocNo.Value, "", isButtonClicked)
    '    LoadData(txtDocNo.Value, NavigatorType.Current)
    '    qry = Nothing
    'End Sub


    'Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
    '    PostData()
    'End Sub
    'Sub PostData()
    '    Dim msg As String = Nothing
    '    Dim qry As String = Nothing
    '    Dim dt As DataTable = Nothing

    '    Dim desc As String = Nothing
    '    Try

    '        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '        'desc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDispatchOutstandingBS, clsFixedParameterCode.AllowDispatchOutstandingBS, Nothing))
    '        desc = clsDBFuncationality.getSingleValue("Select CheckCreditLimit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + lblCustomerCode.Text + "'")
    '        If clsCommon.CompairString(desc, "1") = CompairStringResult.Equal Then
    '            Dim dblAllowedAmt As Double = 0
    '            If CheckCustomerOutstandingAmount(lblCustomerCode.Text, False) = False AndAlso Not AllowNLevel Then Exit Sub
    '            'dblAllowedAmt = CheckCustomerOutstandingAmount(lblCustomerCode.Text, False)
    '            'If dblAllowedAmt < lblTotRAmt1.Text Then
    '            '    Dim dblNewCredtitLimit = clsCommon.myCdbl(lblTotRAmt1.Text) - dblAllowedAmt
    '            '    If clsCommon.CompairString(clsDBFuncationality.getSingleValue("Select Approved from TSPL_Dispatch_BulkSale where Document_No='" + txtDocNo.Value + "'"), "Y") = CompairStringResult.Equal Then
    '            '        ' If txtCreditLimit.Enabled = True Then
    '            '        Throw New Exception("Please increase credit limit ")
    '            '    Else
    '            '        Throw New Exception("Please send for approval for increasing credit limit ")
    '            '    End If

    '            'End If
    '        End If
    '        isFlag = True
    '        If (myMessages.postConfirm()) Then
    '            SaveData()
    '            If (ClsDispatchBulkSale.PostData(MyBase.Form_ID, arrLoc, txtDocNo.Value)) Then
    '                If ApplyMultiChamber Then
    '                    btnSend.PerformClick()
    '                End If
    '                msg = "Successfully posted"
    '                common.clsCommon.MyMessageBoxShow(msg)
    '                LoadData(txtDocNo.Value, NavigatorType.Current)
    '            End If
    '        End If

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    Finally
    '        isFlag = False
    '        msg = Nothing
    '        qry = Nothing
    '        dt = Nothing
    '        desc = Nothing
    '    End Try
    'End Sub
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
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub RDDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub








    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsLocation.getFinder(" Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N' and tspl_location_master.location_code in (" + arrLoc + ")", fndLocation.Value, isButtonClicked)

        If clsCommon.myLen(fndLocation.Value) > 0 Then
            LblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
        Else
            LblLocationName.Text = ""
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub FndPriceCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndPriceCode._MYValidating
        Dim dt As DataTable = Nothing
        Dim whrcls As String = String.Empty

        whrcls = " isnull(TSPL_BulkSalePrice_MASTER.UseInCanSale,0)=1 "
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code= '" & clsFixedParameterCode.showPostrequiredforBulkSale & "' and Type ='" & clsFixedParameterType.showPostrequiredforBulkSale & "'")), "1") = CompairStringResult.Equal Then
            whrcls += " and TSPL_BulkSalePrice_MASTER.Posted='1' "
        End If

        FndPriceCode.Value = ClsBulkSalePriceChart.getFinder(whrcls, FndPriceCode.Value, isButtonClicked)

        If clsCommon.myLen(FndPriceCode.Value) > 0 Then
            dt = clsDBFuncationality.GetDataTable("Select Price_Code ,Fat_Weightage,Snf_Weightage,Fat_Ratio,Snf_Ratio,Standard_Rate,TolerancePerPlus,TolerancePerMinus,Posted,UOM from TSPL_BulkSalePrice_MASTER where Price_Code='" + FndPriceCode.Value + "'")
            If dt.Rows.Count > 0 Then
                TxtFatWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                TxtSNFWeightage.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                txtfatRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Ratio"))
                txtSNFRatio.Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Ratio"))
                txtStanadardrate.Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                TxtToleranceinplus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerPlus"))
                txtToleranceinminus.Value = clsCommon.myCdbl(dt.Rows(0)("TolerancePerMinus"))
                TxtUOM.Value = clsCommon.myCstr(dt.Rows(0)("UOM"))
                UpdateCurrentRow()
            End If
        Else
        End If
        dt = Nothing
        whrcls = Nothing
    End Sub
    Private Sub UpdateCurrentRow()
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
            StandardRate = clsCommon.myCdbl(txtStanadardrate.Value)

            If clsCommon.myCdbl(StandardRate) > 0 Then
                FatWeightage = clsCommon.myCdbl(TxtFatWeightage.Value)
                SNFWeightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
                FatRatio = clsCommon.myCdbl(txtfatRatio.Value)
                SNFRatio = clsCommon.myCdbl(txtSNFRatio.Value)
                For IntRowNo As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colCustCode).Value)) > 0 Or clsCommon.myLen(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colItemCode).Value)) > 0 Then
                        Dim TotalAmount As Double = 0
                        Qty = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value), 3)
                        FatPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatPer).Value)
                        SNFPer = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFPer).Value)
                        FatKG = ((Qty * FatPer) / 100)
                        SNFKG = ((Qty * SNFPer) / 100)
                        TotalAmount = Qty * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMilkRate).Value)
                        SNFRate = TotalAmount / ((((FatWeightage / FatRatio) * (SNFRatio / SNFWeightage)) * FatKG) + SNFKG)
                        FatRate = (TotalAmount - (SNFRate * SNFKG)) / FatKG

                        FatAmount = FatRate * FatKG
                        SNFAmount = SNFRate * SNFKG

                        gv1.Rows(IntRowNo).Cells(colMilkAmount).Value = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMilkRate).Value) * Qty
                        gv1.Rows(IntRowNo).Cells(colPriceRate).Value = StandardRate
                        gv1.Rows(IntRowNo).Cells(colFatRate).Value = clsCommon.myCdbl(FatRate)
                        gv1.Rows(IntRowNo).Cells(colSNFRate).Value = clsCommon.myCdbl(SNFRate)
                        gv1.Rows(IntRowNo).Cells(colFatKG).Value = clsCommon.myCdbl(FatKG)
                        gv1.Rows(IntRowNo).Cells(colSNFKG).Value = clsCommon.myCdbl(SNFKG)
                        gv1.Rows(IntRowNo).Cells(ColFatAmount).Value = clsCommon.myCdbl(FatAmount)
                        gv1.Rows(IntRowNo).Cells(ColSNFAmount).Value = clsCommon.myCdbl(SNFAmount)
                        gv1.Rows(IntRowNo).Cells(colTotalAmount).Value = TotalAmount
                        gv1.Rows(IntRowNo).Cells(colDifference).Value = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colMilkAmount).Value) - clsCommon.myCdbl(TotalAmount)
                    End If
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 And rdbEnterDataManual.IsChecked Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colSlNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnSelectSheet_Click(sender As Object, e As EventArgs) Handles btnSelectSheet.Click
        If rdbImportDataFromSheet.IsChecked Then
            gv1.Columns.Clear()
            gv1.DataSource = Nothing
            ' If transportSql.importExcel(gv1, "SL", "Date", "Distributor", "ItemCode", "No of cans", "Qty", "FAT %", "SNF %", "FAT Kg", "SNF Kg", "Milk Rate", "Milk Amt", "D M Rate", "Fat Rate", "SNF Rate", "FAT Amt", "SNF Amt", "Total Amt", "Diff") Then
            ' If transportSql.importExcel(gv1, "SL", "Date", "Distributor", "ItemCode", "No of cans", "Qty", "FAT %", "SNF %", "FAT Kg", "SNF Kg", "Milk Rate", "Milk Amt") Then
            If transportSql.importExcel(gv1, "SL", "Date", "Distributor", "ItemCode", "No of cans", "Qty", "FAT %", "SNF %", "Milk Rate") Then
                If gv1.Columns.Count > 0 Then

                    ChkBoxColumn = New GridViewCheckBoxColumn()
                    ChkBoxColumn.Name = colIsValidated
                    ChkBoxColumn.HeaderText = "Validated"
                    ChkBoxColumn.ReadOnly = True
                    gv1.MasterTemplate.Columns.Insert(1, ChkBoxColumn)

                    TextCol = New GridViewTextBoxColumn()
                    TextCol.Name = colErrorStatus
                    TextCol.HeaderText = "Error Status"
                    TextCol.ReadOnly = True
                    gv1.MasterTemplate.Columns.Insert(2, TextCol)

                    For i As Integer = 0 To gv1.Rows.Count - 1
                        'gv1.Rows(i).Cells(colSlNo).Value = (i + 1)
                        gv1.Rows(i).Cells(colIsValidated).Value = False
                        ValidatedCount = 0
                        gv1.Rows(i).Cells(colErrorStatus).Value = ""
                        gv1.Rows(i).Cells("Date").Value = clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDocDate).Value, "dd/MM/yyyy")
                    Next

                    Dim fatkg As New GridViewDecimalColumn
                    fatkg.FormatString = "{0:n2}"
                    fatkg.HeaderText = "FAT Kg"
                    fatkg.Name = colFatKG
                    fatkg.Width = 75
                    fatkg.ReadOnly = True
                    fatkg.WrapText = True
                    fatkg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                    gv1.Columns.Add(fatkg)



                    Dim snfkg As New GridViewDecimalColumn
                    snfkg.FormatString = "{0:n2}"
                    snfkg.HeaderText = "SNF Kg"
                    snfkg.Name = colSNFKG
                    snfkg.Width = 75
                    snfkg.ReadOnly = True
                    snfkg.WrapText = True
                    snfkg.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                    gv1.Columns.Add(snfkg)

                    Dim milkamt As New GridViewDecimalColumn
                    milkamt.FormatString = "{0:n2}"
                    milkamt.HeaderText = "Milk Amt"
                    milkamt.Name = colMilkAmount
                    milkamt.Width = 75
                    milkamt.ReadOnly = True
                    milkamt.WrapText = True
                    milkamt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                    gv1.Columns.Add(milkamt)


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


                    For i As Integer = 0 To gv1.Columns.Count - 1
                        gv1.Columns(i).ReadOnly = True
                    Next
                    gv1.AllowAddNewRow = False
                    gv1.AllowDeleteRow = True
                    gv1.EnableFiltering = True
                    gv1.EnableSorting = False
                    gv1.EnableGrouping = False
                    gv1.AllowColumnChooser = False
                    gv1.AllowColumnReorder = True
                    gv1.BestFitColumns()
                    gv1.AutoSizeRows = True
                    gv1.ReadOnly = True
                    gv1.TableElement.TableHeaderHeight = 30
                End If
                UpdateCurrentRow()
            End If
        End If
    End Sub

    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        CheckAndValidate()
    End Sub

    Sub CheckAndValidate()
        Dim ValidateStatus As String = String.Empty
        If Gv1.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("There are no row is grid please select a sheet to import")
        End If
        If ValidatedCount = Gv1.Rows.Count Then
            clsCommon.MyMessageBoxShow("All Rows are already validated")
            Exit Sub
        End If
        ValidatedCount = 0
        Dim strCellValue


        If SettAvgFATSNFPerForCanSale Then
            Dim strUOM As String = "KG"
            If clsCommon.myLen(clsCommon.myCstr(TxtUOM.Value)) > 0 Then
                strUOM = clsCommon.myCstr(TxtUOM.Value)
            End If
            For IntRowNo As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colCustCode).Value)) > 0 Or clsCommon.myLen(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colItemCode).Value)) > 0 Then
                    Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", "MI", clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colItemCode).Value), fndLocation.Value, 1, strUOM, 1, 1, clsCommon.myCDate(gv1.Rows(IntRowNo).Cells(colDocDate).Value), clsCommon.myCDate(gv1.Rows(IntRowNo).Cells(colDocDate).Value), False, Nothing, "")
                    gv1.Rows(IntRowNo).Cells(colFatPer).Value = Math.Round(objMCT.FAT_Per, 2, MidpointRounding.ToEven)
                    gv1.Rows(IntRowNo).Cells(colSNFPer).Value = Math.Round(objMCT.SNF_Per, 2, MidpointRounding.ToEven)
                    gv1.Rows(IntRowNo).Cells(colFatKG).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colFatPer).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) / 100, 3, MidpointRounding.ToEven)
                    gv1.Rows(IntRowNo).Cells(colSNFKG).Value = Math.Round(clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colSNFPer).Value) * clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value) / 100, 3, MidpointRounding.ToEven)

                End If
            Next
        End If

        If rdbEnterDataManual.IsChecked = True Then
            UpdateCurrentRow()
        End If

        If rdbImportDataFromSheet.IsChecked Then
            For i As Integer = 0 To gv1.Rows.Count - 1

                If i = 0 Then
                    clsCommon.ProgressBarPercentShow()
                End If
                clsCommon.ProgressBarPercentUpdate((i + 1) / gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & gv1.Rows.Count)
                ValidateStatus = ""
                strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colCustCode).Value)

                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Customer Code Must not be Blank" & Environment.NewLine
                End If
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCellValue & "' and isnull(Manual_Customer,'')='Y' ")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Distributor not found in master" & Environment.NewLine
                End If

                strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value)
                If clsCommon.myLen(strCellValue) <= 0 Then
                    ValidateStatus = ValidateStatus & "Item Code Must not be Blank" & Environment.NewLine
                End If

                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where iTEM_Code='" & strCellValue & "' AND Product_Type ='MI' and Active=1")) <= 0 Then
                    ValidateStatus = ValidateStatus & "Item not found in master" & Environment.NewLine
                End If

                If clsCommon.myLen(ValidateStatus) <= 0 Then
                    gv1.Rows(i).Cells(colIsValidated).Value = True
                    ValidatedCount = ValidatedCount + 1
                    gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                    gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                    gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.White
                Else
                    gv1.Rows(i).Cells(colIsValidated).Value = False
                    gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                    gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                    gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                    gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.Red
                End If
            Next
        Else
            For i As Integer = 0 To gv1.Rows.Count - 1

                If i = 0 Then
                    clsCommon.ProgressBarPercentShow()
                End If
                clsCommon.ProgressBarPercentUpdate((i + 1) / gv1.Rows.Count * 100, "Validating  Record(s) " & (i + 1) & "   of  Total " & gv1.Rows.Count)
                ValidateStatus = ""

                If rdbEnterDataManual.IsChecked = True AndAlso (clsCommon.myLen(clsCommon.myCstr(gv1.Rows(i).Cells(colCustCode).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value)) > 0) Then
                    strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colCustCode).Value)

                    If clsCommon.myLen(strCellValue) <= 0 Then
                        ValidateStatus = ValidateStatus & "Customer Code Must not be Blank" & Environment.NewLine
                    End If
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCellValue & "' and isnull(Manual_Customer,'')='Y' ")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Distributor not found in master" & Environment.NewLine
                    End If

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_CUSTOMER_MASTER where Cust_Code='" & strCellValue & "' and isnull(status,'')='N' ")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Distributor should be active." & Environment.NewLine
                    End If

                    strCellValue = clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value)
                    If clsCommon.myLen(strCellValue) <= 0 Then
                        ValidateStatus = ValidateStatus & "Item Code Must not be Blank" & Environment.NewLine
                    End If

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_ITEM_MASTER where iTEM_Code='" & strCellValue & "' AND Product_Type ='MI' and Active=1")) <= 0 Then
                        ValidateStatus = ValidateStatus & "Item not found in master" & Environment.NewLine
                    End If

                    strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colNoOfCans).Value)
                    If strCellValue <= 0 Then
                        ValidateStatus = ValidateStatus & "No. of Cans Must not be Zero or Negative" & Environment.NewLine
                    End If

                    strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value)
                    If strCellValue <= 0 Then
                        ValidateStatus = ValidateStatus & "Qty Must not be Zero or Negative" & Environment.NewLine
                    End If

                    strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colFatPer).Value)
                    If strCellValue > clsCommon.myCdbl(gv1.Rows(i).Cells(colFatPer).Value) Then
                        ValidateStatus = ValidateStatus & "Fat% Must not be Zero or Negative" & Environment.NewLine
                    End If

                    strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colSNFPer).Value)
                    If strCellValue <= 0 Then
                        ValidateStatus = ValidateStatus & "SNF% Must not be Zero or Negative" & Environment.NewLine
                    End If

                    strCellValue = clsCommon.myCdbl(gv1.Rows(i).Cells(colMilkRate).Value)
                    If strCellValue > clsCommon.myCdbl(gv1.Rows(i).Cells(colMilkRate).Value) Then
                        ValidateStatus = ValidateStatus & "Milk Rate Must not be Zero or Negative" & Environment.NewLine
                    End If

                    If clsCommon.myLen(ValidateStatus) <= 0 Then
                        gv1.Rows(i).Cells(colIsValidated).Value = True
                        ValidatedCount = ValidatedCount + 1
                        gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                        gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                        gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.White
                    Else
                        gv1.Rows(i).Cells(colIsValidated).Value = False
                        gv1.Rows(i).Cells(colErrorStatus).Value = ValidateStatus
                        gv1.Rows(i).Cells(colErrorStatus).Style.DrawFill = True
                        gv1.Rows(i).Cells(colErrorStatus).Style.CustomizeFill = True
                        gv1.Rows(i).Cells(colErrorStatus).Style.BackColor = Color.Red
                    End If
                Else
                    If rdbEnterDataManual.IsChecked = True AndAlso (clsCommon.myLen(clsCommon.myCstr(gv1.Rows(i).Cells(colCustCode).Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value)) <= 0) Then
                        gv1.Rows.RemoveAt(i)
                        i = i - 1
                        If gv1.Rows.Count = 1 Then
                            Exit For
                        End If
                        ' If 
                    End If
                End If
            Next
        End If
        gv1.BestFitColumns()
        gv1.AutoSizeRows = True
        gv1.Columns(colSlNo).PinPosition = PinnedColumnPosition.Left
        gv1.Columns(colIsValidated).PinPosition = PinnedColumnPosition.Left
        gv1.Columns(colErrorStatus).PinPosition = PinnedColumnPosition.Left
        clsCommon.ProgressBarPercentHide()
    End Sub

    Private Sub CreateAutoCanSale()
        Dim LocationCode As String = String.Empty
        Dim CustomerCode As String = String.Empty
        Dim SalePriceCode As String = String.Empty
        Dim strdocdate As Date? = Nothing
        Try
            Dim DocAmount As Double = 0

            Dim CustomerCount As Integer = 0
            Dim count As Integer = 1
            Dim dt1 As DataTable = Nothing
            dt1 = clsDBFuncationality.GetDataTable("select '' as DocDate, '' as Distributor,'' as ItemCode,'' as Noofcans, '' as Qty,'' as FATper, '' as SNFper,'' as FATKg, '' as SNFKg, '' as MilkRate,'' as MilkAmt, '' as DMRate, '' as FatRate,'' as SNFRate,'' as FATAmt,'' as SNFAmt,'' as TotalAmt,'' as Diff")

            dt1.Rows.RemoveAt(0)
            If ValidatedCount > 0 Then
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells(colIsValidated).Value) Then
                        dt1.Rows.Add("" + clsCommon.myCstr(grow.Cells(colDocDate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colCustCode).Value) + "", "" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "", "" + clsCommon.myCstr(grow.Cells(colNoOfCans).Value) + "", "" + clsCommon.myCstr(grow.Cells(colQty).Value) + "", "" + clsCommon.myCstr(grow.Cells(colFatPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colSNFPer).Value) + "", "" + clsCommon.myCstr(grow.Cells(colFatKG).Value) + "", "" + clsCommon.myCstr(grow.Cells(colSNFKG).Value) + "", " " + clsCommon.myCstr(grow.Cells(colMilkRate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colMilkAmount).Value) + "", "" + clsCommon.myCstr(grow.Cells(colPriceRate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colFatRate).Value) + "", "" + clsCommon.myCstr(grow.Cells(colSNFRate).Value) + "", "" + clsCommon.myCstr(grow.Cells(ColFatAmount).Value) + "", "" + clsCommon.myCstr(grow.Cells(ColSNFAmount).Value) + "", "" + clsCommon.myCstr(grow.Cells(colTotalAmount).Value) + "", "" + clsCommon.myCstr(grow.Cells(colDifference).Value) + "")
                    End If
                Next
            End If

            Dim dtout As DataTable = Nothing
            dt1.DefaultView.Sort = "DocDate,Distributor"
            dtout = dt1.DefaultView.ToTable()

            dtmain = clsDBFuncationality.GetDataTable("Select '' as CanSaleSrNo,'' as DocDate, '' as Distributor,'' as ItemCode,'' as Noofcans, '' as Qty,'' as FATper, '' as SNFper,'' as FATKg, '' as SNFKg, '' as MilkRate,'' as MilkAmt, '' as DMRate, '' as FatRate,'' as SNFRate,'' as FATAmt,'' as SNFAmt,'' as TotalAmt,'' as Diff")
            dtmain.Rows.RemoveAt(0)


            If ValidatedCount > 0 Then
                For Each dr As DataRow In dtout.Rows
                    If CreateSeparateInvoiceforeachrowinCansale = True Then
                        CustomerCount = CustomerCount + 1
                        DocAmount = 0
                        DocAmount = clsCommon.myCdbl(dr("TotalAmt"))
                    Else
                        If clsCommon.CompairString(clsCommon.myCDate(strdocdate), clsCommon.myCDate(dr("DocDate"))) = CompairStringResult.Equal And clsCommon.CompairString(CustomerCode, clsCommon.myCstr(dr("Distributor"))) = CompairStringResult.Equal Then
                            DocAmount = DocAmount + clsCommon.myCdbl(dr("TotalAmt"))
                        Else
                            CustomerCount = CustomerCount + 1
                            DocAmount = 0
                            DocAmount = clsCommon.myCdbl(dr("TotalAmt"))
                        End If
                    End If

                    CustomerCode = clsCommon.myCstr(dr("Distributor"))
                    'LocationCode = clsCommon.myCstr(dr("Location"))
                    'SalePriceCode = clsCommon.myCstr(dr("SalePriceCode"))
                    strdocdate = clsCommon.myCDate(dr("DocDate"))
                    dtmain.Rows.Add("" + clsCommon.myCstr(CustomerCount) + "", "" + clsCommon.myCstr(dr("DocDate")) + "", "" + clsCommon.myCstr(dr("Distributor")) + "", "" + clsCommon.myCstr(dr("ItemCode")) + "", "" + clsCommon.myCstr(dr("Noofcans")) + "", "" + clsCommon.myCstr(dr("Qty")) + "", "" + clsCommon.myCstr(dr("FATper")) + "", "" + clsCommon.myCstr(dr("SNFper")) + "", "" + clsCommon.myCstr(dr("FATKg")) + "", "" + clsCommon.myCstr(dr("SNFKg")) + "", "" + clsCommon.myCstr(dr("MilkRate")) + "", " " + clsCommon.myCstr(dr("MilkAmt")) + "", "" + clsCommon.myCstr(dr("DMRate")) + "", "" + clsCommon.myCstr(dr("FatRate")) + "", "" + clsCommon.myCstr(dr("SNFRate")) + "", " " + clsCommon.myCstr(dr("FATAmt")) + "", "" + clsCommon.myCstr(dr("SNFAmt")) + "", "" + clsCommon.myCstr(dr("TotalAmt")) + "", "" + clsCommon.myCstr(dr("Diff")) + "")

                Next
                CanSaleSaveData()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Sub CanSaleSaveData()

        Dim strcountno As String = ""
        Dim objTr As ClsCanSaleDetail = Nothing
        Dim obj As ClsCanSale = Nothing
        Try
            Dim dblSLNo As Double = 0
            Dim DocuAmount As Double = 0
            Dim intCounter As Integer = 0
            Dim j As Integer = 0
            Dim strCanItemCode As String = String.Empty
            Dim dblCanItemRate As Double = 0
            Dim strCanUOM As String = String.Empty
            Dim TotalNoOfCans As Double = 0

            If ChkCanInventoryType.Checked = True Then
                strCanItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code from tspl_item_master where ISNULL(CAN,'0')<>0"))
                dblCanItemRate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCanRate, clsFixedParameterCode.ItemCanRate, Nothing))
                strCanUOM = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Unit_Code from tspl_item_master where ISNULL(CAN,'0')<>0"))
            End If

            For Each dr As DataRow In dtmain.Rows
                j += 1
                clsCommon.ProgressBarPercentUpdate(j * 100 / dtmain.Rows.Count, " Creating Can Sale Records " & j & " of Total " & dtmain.Rows.Count)
                Dim intCurrInvNo As Integer = clsCommon.myCdbl(dr("CanSaleSrNo"))

                If clsCommon.CompairString(strcountno, clsCommon.myCstr(dr("CanSaleSrNo"))) <> CompairStringResult.Equal Then
                    obj = New ClsCanSale()
                    obj.Document_Date = clsCommon.myCstr(dr("DocDate"))
                    obj.Customer_Code = clsCommon.myCstr(dr("Distributor"))
                    obj.Customer_Name = clsCommon.myCstr(clsCustomerMaster.GetName(clsCommon.myCstr(dr("Distributor")), Nothing))
                    obj.Location_Code = clsCommon.myCstr(fndLocation.Value)
                    obj.Location_Name = clsCommon.myCstr(LblLocationName.Text)
                    obj.Price_Code = clsCommon.myCstr(FndPriceCode.Value)
                    obj.Fat_Weightage = clsCommon.myCdbl(TxtFatWeightage.Value)
                    obj.Snf_Weightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
                    obj.Fat_Ratio = clsCommon.myCdbl(txtfatRatio.Value)
                    obj.Snf_Ratio = clsCommon.myCdbl(txtSNFRatio.Value)
                    obj.Standard_Rate = clsCommon.myCdbl(txtStanadardrate.Value)
                    obj.TolerancePerPlus = clsCommon.myCdbl(TxtToleranceinplus.Value)
                    obj.TolerancePerMinus = clsCommon.myCdbl(txtToleranceinminus.Value)
                    obj.CanInventoryType = IIf(ChkCanInventoryType.Checked = True, "1", "0")
                    ''BHA/09/05/18-000021
                    If ChkCanInventoryType.Checked = True Then
                        obj.CanItemCode = strCanItemCode
                        obj.CanItemRate = dblCanItemRate
                        obj.CanItemUOM = strCanUOM
                    End If

                    obj.arrCanSaleDetail = New List(Of ClsCanSaleDetail)
                    objTr = New ClsCanSaleDetail()
                    dblSLNo = 1
                    objTr.SL_No = dblSLNo
                    objTr.NoOfCans = clsCommon.myCdbl(dr("NoOfCans"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.ItemCode = clsCommon.myCstr(dr("ItemCode"))
                    objTr.ItemName = clsCommon.myCstr(clsItemMaster.GetItemName(dr("ItemCode"), Nothing))
                    If clsCommon.myLen(clsCommon.myCstr(TxtUOM.Value)) > 0 Then
                        objTr.UOM = clsCommon.myCstr(TxtUOM.Value)
                    Else
                        objTr.UOM = "KG"
                    End If

                    objTr.FatPer = clsCommon.myCdbl(dr("FatPer"))
                    objTr.SNFPer = clsCommon.myCdbl(dr("SNFPer"))
                    objTr.Fat_KG = clsCommon.myCdbl(dr("FatKG"))
                    objTr.SNF_KG = clsCommon.myCdbl(dr("SNFKG"))
                    objTr.MilkRate = clsCommon.myCdbl(dr("MilkRate"))
                    objTr.MilkAmt = clsCommon.myCdbl(dr("MilkAmt"))
                    objTr.PriceRate = clsCommon.myCdbl(dr("DMRate"))
                    objTr.FatRate = clsCommon.myCdbl(dr("FatRate"))
                    objTr.SNFRate = clsCommon.myCdbl(dr("SNFRate"))
                    objTr.FatAmount = clsCommon.myCdbl(dr("FatAmt"))
                    objTr.SNFAmount = clsCommon.myCdbl(dr("SNFAmt"))
                    objTr.TotalAmount = clsCommon.myCdbl(dr("TotalAmt"))
                    objTr.Diff = clsCommon.myCdbl(dr("Diff"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("TotalAmt"))
                    DocuAmount = 0
                    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.MilkAmt, 2)
                    TotalNoOfCans = 0
                    TotalNoOfCans = TotalNoOfCans + objTr.NoOfCans
                    obj.arrCanSaleDetail.Add(objTr)
                    dblSLNo = dblSLNo + 1
                Else
                    objTr = New ClsCanSaleDetail()
                    objTr.SL_No = dblSLNo
                    objTr.NoOfCans = clsCommon.myCdbl(dr("NoOfCans"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.ItemCode = clsCommon.myCstr(dr("ItemCode"))
                    objTr.ItemName = clsCommon.myCstr(clsItemMaster.GetItemName(dr("ItemCode"), Nothing))
                    If clsCommon.myLen(clsCommon.myCstr(TxtUOM.Value)) > 0 Then
                        objTr.UOM = clsCommon.myCstr(TxtUOM.Value)
                    Else
                        objTr.UOM = "KG"
                    End If
                    objTr.FatPer = clsCommon.myCdbl(dr("FatPer"))
                    objTr.SNFPer = clsCommon.myCdbl(dr("SNFPer"))
                    objTr.Fat_KG = clsCommon.myCdbl(dr("FatKG"))
                    objTr.SNF_KG = clsCommon.myCdbl(dr("SNFKG"))
                    objTr.MilkRate = clsCommon.myCdbl(dr("MilkRate"))
                    objTr.MilkAmt = clsCommon.myCdbl(dr("MilkAmt"))
                    objTr.PriceRate = clsCommon.myCdbl(dr("DMRate"))
                    objTr.FatRate = clsCommon.myCdbl(dr("FatRate"))
                    objTr.SNFRate = clsCommon.myCdbl(dr("SNFRate"))
                    objTr.FatAmount = clsCommon.myCdbl(dr("FatAmt"))
                    objTr.SNFAmount = clsCommon.myCdbl(dr("SNFAmt"))
                    objTr.TotalAmount = clsCommon.myCdbl(dr("TotalAmt"))
                    objTr.Diff = clsCommon.myCdbl(dr("Diff"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("TotalAmt"))
                    DocuAmount = Math.Round(DocuAmount, 2) + Math.Round(objTr.MilkAmt, 2)
                    TotalNoOfCans = TotalNoOfCans + objTr.NoOfCans
                    obj.arrCanSaleDetail.Add(objTr)
                    dblSLNo = dblSLNo + 1
                End If
                strcountno = intCurrInvNo
                Dim intNextInvNo As Integer = -1

                If intCounter + 1 < dtmain.Rows.Count Then
                    intNextInvNo = clsCommon.myCdbl(dtmain.Rows(intCounter + 1)("CanSaleSrNo"))
                End If

                If Not (intCurrInvNo = intNextInvNo) Then
                    obj.DocumentAmount = clsCommon.myCdbl(DocuAmount)
                    obj.Total_Amt = clsCommon.myCdbl(DocuAmount)
                    obj.TotalNoofCans = clsCommon.myCdbl(TotalNoOfCans)
                    ClsCanSale.SaveData(obj, True)
                End If
                intCounter += 1
            Next

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            strcountno = Nothing
            obj = Nothing
            objTr = Nothing
        End Try

    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try

            If ValidatedCount > 0 AndAlso AllowToSave() Then
                CreateAutoCanSale()
                clsCommon.MyMessageBoxShow("Saved Successfully")
                 Reset()
            Else
                Throw New Exception("No Validated Rows found to save")
            End If
        Catch ex As Exception
            Try
                'clsCommon.ProgressBarPercentHide()
            Catch ex1 As Exception
            End Try
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean

        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            fndLocation.Focus()
            Throw New Exception("Location cannot be left blank")
        End If

        If clsCommon.myLen(FndPriceCode.Value) <= 0 Then
            FndPriceCode.Focus()
            Throw New Exception("Price Code cannot be left blank")
        End If
        If ChkCanInventoryType.Checked Then
            If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from tspl_item_master where ISNULL(CAN,'0')<>0")), "0") = CompairStringResult.Equal Then
                Throw New Exception("Please create item of Can Type in Item Master.")
            End If
            Dim dblCanItemRate As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCanRate, clsFixedParameterCode.ItemCanRate, Nothing))
            If dblCanItemRate <= 0 Then
                Throw New Exception("Please fill rate for Can in utility.")
            End If

        End If

        Dim stritemcode As String = clsCommon.myCstr(gv1.Rows(0).Cells(colItemCode).Value)
        For i As Integer = 0 To gv1.Rows.Count - 1
            Dim strinneritemcode As String = clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value)
            If clsCommon.CompairString(stritemcode, strinneritemcode) <> CompairStringResult.Equal Then
                Throw New Exception("Item Code must be same for all")
            End If
        Next
        Return True
    End Function

    Private Sub btnExportFormat_Click(sender As Object, e As EventArgs) Handles btnExportFormat.Click
        Dim qry As String = String.Empty
        qry = "select '' As [SL],'' as [Date],'' as [Distributor],'' as [ItemCode],'' as [No of cans],'' as [Qty],'' as [FAT %],'' as [SNF %],'' as [Milk Rate]"
            transportSql.ExporttoExcel(qry, Me)
        qry = Nothing
    End Sub

    Private Sub rdbImportDataFromSheet_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbImportDataFromSheet.ToggleStateChanged
        If rdbEnterDataManual.IsChecked Then
            loadBlankItemGrid()
        Else
            gv1.Columns.Clear()
            gv1.DataSource = Nothing
        End If
    End Sub
End Class
