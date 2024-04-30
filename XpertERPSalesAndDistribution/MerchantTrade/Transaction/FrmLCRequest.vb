'--------Created By Richa 19/09/2014 Against Ticket No BM00000003638,BM00000005029,BM00000005558,BM00000005840,BM00000006066,BM00000006349,BM00000007552
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmLCRequest
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim curRow As Integer = -1
    Dim curCol As Integer = -1
    Dim Qry As String
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isFlag As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colType As String = "colType"
    Public Const colUnitCode As String = "colUnitCode"
    Public Const colDetails As String = "colDetails"
    Public Const colFieldName As String = "colFieldName"
    Public Const colTag As String = "colTag"
    Public Const colQty As String = "colQty"
    Public Const colPOQty As String = "colPOQty"
    Public Const colItemCode As String = "colItemCode"
    Public Const colItemDesc As String = "colItemDesc"
    Public Const colPONo As String = "colPONo"
    Public Const colRate As String = "colRate"
    Dim isLoadInsideData As Boolean = False
    Dim isLoadData As Boolean = False
    Private arrEditableRow As List(Of Integer) = Nothing
    Dim strDescofGoodsforprint As String = String.Empty
#End Region


#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmLCRequest_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FrmLCRequest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        FillLCIssuing()
        arrEditableRow = New List(Of Integer)
        arrEditableRow.Add(1)
        arrEditableRow.Add(2)
        arrEditableRow.Add(3)
        arrEditableRow.Add(5)
        arrEditableRow.Add(6)
        arrEditableRow.Add(11)
        arrEditableRow.Add(12)
        arrEditableRow.Add(13)
        arrEditableRow.Add(14)
        arrEditableRow.Add(16)
        arrEditableRow.Add(21)
        arrEditableRow.Add(22)
        arrEditableRow.Add(29)
        arrEditableRow.Add(35)
        RadPageView1.Pages("RDFormA2").Item.Visibility = ElementVisibility.Collapsed
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
        FillLCIssuing()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub fndBankCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBankCode._MYValidating
        Dim dt As DataTable = Nothing
        fndBankCode.Value = clsBankMaster.getFinder("", fndBankCode.Value, isButtonClicked)
        LblBankName.Text = clsDBFuncationality.getSingleValue("Select DESCRIPTION  from TSPL_BANK_MASTER where BANK_CODE  ='" + fndBankCode.Value + "' ")
        TxtFDPer.Value = clsDBFuncationality.getSingleValue("Select FDPercentage  from TSPL_BANK_master where BANK_CODE ='" & fndBankCode.Value & "'")
        FndDraweeBankCode.Value = fndBankCode.Value
        TxtDraweeBank.Text = LblBankName.Text
        gvLCIssuing.Rows(7).Cells(colDetails).Value = LblBankName.Text
        dt = clsDBFuncationality.GetDataTable("Select DESCRIPTION ,ADD1 ,ADD2,ADD3 ,CITY ,POSTAL from TSPL_BANK_MASTER  where BANK_CODE ='" & fndBankCode.Value & "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvLCIssuing.Rows(34).Cells(colDetails).Value = "Negotiated documents to be sent to us in one lot." & Environment.NewLine & "Our mailing address is:" & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("DESCRIPTION")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("ADD1")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("ADD2")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("ADD3")) & clsCommon.myCstr(dt.Rows(0)("CITY")) & " - " & clsCommon.myCstr(dt.Rows(0)("POSTAL")) & Environment.NewLine & "Upon receipt of credit conforming documents, we undertake to remit funds as per instrustions of the beneficiary bank."
        Else
            gvLCIssuing.Rows(34).Cells(colDetails).Value = ""
        End If
        gvLCIssuing.Rows(16).Cells(colDetails).Value = TxtDraweeBank.Text
        dt = clsDBFuncationality.GetDataTable("  Select DESCRIPTION ,'Address: '+ADD1+', '+ADD2 as Address,'A/C No.:- '+BANKACCNUMBER as AccountNo,'IBAN No.:- '+IBAN_No as IBANNo,'Swift Code.:- '+Swift_Code as SwiftCode from TSPL_BANK_MASTER  where BANK_CODE ='" & FndDraweeBankCode.Value & "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvLCIssuing.Rows(35).Cells(colDetails).Value = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("Address")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("AccountNo")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("IBANNo")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("SwiftCode"))
        Else
            gvLCIssuing.Rows(35).Cells(colDetails).Value = ""
        End If
        dt = Nothing
    End Sub

    Private Sub fndLCRequestcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndLCRequestcode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_LC_REQUEST_MT where LCRequestNo='" + fndLCRequestcode.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndLCRequestcode.MyReadOnly = True
            ElseIf check <= 0 Then
                fndLCRequestcode.MyReadOnly = False
            End If
            LoadData(fndLCRequestcode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndLCRequestcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLCRequestcode._MYValidating
        fndLCRequestcode.Value = ClsLCRequest.getFinder("", fndLCRequestcode.Value, isButtonClicked)
        LoadData(fndLCRequestcode.Value, NavigatorType.Current)
    End Sub
    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtLCRequestdate.Value, Nothing) = False Then
            txtLCRequestdate.Select()
            Return False
        End If
        Dim LCCreditValue As Double = 0
        RadPageView1.SelectedPage = RadPageViewPage1
        If clsCommon.myLen(fndBankCode.Value) <= 0 Then
            fndBankCode.Focus()
            Throw New Exception("Bank Code cannot be left blank")
        End If
        If rdbAgainstPO.IsChecked Then
            If clsCommon.myLen(FndPurchaseOrderNo.Value) <= 0 Then
                FndPurchaseOrderNo.Focus()
                Throw New Exception("Purchase Order No cannot be Zero or blank")
            End If
        Else
            If clsCommon.myLen(FndPurchaseInvoiceNo.Value) <= 0 Then
                FndPurchaseInvoiceNo.Focus()
                Throw New Exception("Purchase Invoice No cannot be Zero or blank")
            End If
            If clsCommon.myLen(FndVendor.Value) <= 0 Then
                FndVendor.Focus()
                Throw New Exception("Vendor Code cannot be Zero or blank")
            End If
        End If


        If clsCommon.myCdbl(TxtLCAmount.Value) < 0 Then
            TxtLCAmount.Focus()
            Throw New Exception("LC Amount cannot be negative")
        End If

        If clsCommon.myCdbl(TxtLCAmount.Value) = 0 Then
            TxtLCAmount.Focus()
            Throw New Exception("LC Amount cannot be zero")
        End If
        Dim count As Integer = Nothing
        count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_LC_REQUEST_MT where SGS_Waiver_Ref_no='" & TxtSGSWaiverRefNo.Text & "' and LCRequestNo<>'" & fndLCRequestcode.Value & "' "))
        If count <> 0 Then
            RadPageView1.SelectedPage = RDSGSWaiver
            TxtSGSWaiverRefNo.Focus()
            Throw New Exception("SGS Waiver Ref. No exist in another transaction please enter another reference no")
        End If
        count = Nothing
        count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_LC_REQUEST_MT where Merchant_Dec_Ref_no='" & TxtMerchantDecrefNo.Text & "' and LCRequestNo<>'" & fndLCRequestcode.Value & "' "))
        If count <> 0 Then
            RadPageView1.SelectedPage = RDMerchantdeclaration
            TxtMerchantDecrefNo.Focus()
            Throw New Exception("Merchant Declaration Ref. No exist in another transaction please enter another reference no")
        End If
        'LCCreditValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select LCCreditLimit from TSPL_BANK_master where BANK_CODE ='" & fndBankCode.Value & "'"))
        'If clsCommon.myCdbl(TxtLCAmount.Value) > LCCreditValue Then
        '    TxtLCAmount.Focus()
        '    Throw New Exception("LC Amount cannot be greater than " & LCCreditValue & "")
        'End If
        If clsCommon.myCdbl(TxtFDPer.Value) < 0 Then
            TxtFDPer.Focus()
            Throw New Exception("FD % cannot be negative")
        End If
        If clsCommon.myCdbl(TxtFDPer.Value) > 100 Then
            TxtFDPer.Focus()
            Throw New Exception(" FD % cannot be more than 100 ")
        End If
        If clsCommon.CompairString(cmbLCType.SelectedValue, "U") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(TxtLCPeriod.Value) < 0 Then
                TxtLCPeriod.Focus()
                Throw New Exception("LC Period cannot be negative")
            End If
            If clsCommon.myCdbl(TxtLCPeriod.Value) = 0 Then
                TxtLCPeriod.Focus()
                Throw New Exception("LC Period cannot be zero")
            End If
            If clsCommon.CompairString(cboLCPeriod.SelectedValue, "") = CompairStringResult.Equal Then
                cboLCPeriod.Focus()
                Throw New Exception("Please select LC Period")
            End If
        End If

        If clsCommon.myCdbl(TxtFDPeriod.Value) < 0 Then
            TxtFDPeriod.Focus()
            Throw New Exception("FD Period cannot be negative")
        End If
        If clsCommon.myCdbl(TxtFDPeriod.Value) > 0 Then
            If clsCommon.CompairString(cboFDPeriod.SelectedValue, "") = CompairStringResult.Equal Then
                cboFDPeriod.Focus()
                Throw New Exception("Please select FD Period")
            End If
        End If
        If clsCommon.myCdbl(TxtLCExtend.Value) < 0 Then
            TxtLCExtend.Focus()
            Throw New Exception("LC Extend cannot be negative")
        End If
        If clsCommon.myCdbl(TxtLCExtend.Value) > 0 Then
            If clsCommon.CompairString(cboLCExtend.SelectedValue, "") = CompairStringResult.Equal Then
                cboLCExtend.Focus()
                Throw New Exception("Please select LC Extend")
            End If
        End If
        'If clsCommon.myCdbl(TxtFDPer.Value) = 0 Then
        '    TxtFDPer.Focus()
        '    Throw New Exception("FD % cannot be zero")
        'End If
        For i As Integer = 0 To gv1.Rows.Count - 1

            If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) < 0 Then
                Throw New Exception("Qty cannot be negative")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) = 0 Then
                Throw New Exception("Qty cannot be left blank")
            End If
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) > clsCommon.myCdbl(gv1.Rows(i).Cells(colPOQty).Value) Then
                Throw New Exception("Qty cannot be greater than PO qty")
            End If
        Next
        If clsCommon.myLen(txtCurrencyCode.Value) <= 0 Then
            txtCurrencyCode.Focus()
            Throw New Exception("Currency Code cannot be Zero or blank")
        End If
        If clsCommon.myCdbl(txtConversionRate.Value) = 0 Then
            txtConversionRate.Focus()
            Throw New Exception("Conversion Rate cannot be zero")
        End If
        If clsCommon.myCdbl(txtConversionRate.Value) < 0 Then
            txtConversionRate.Focus()
            Throw New Exception("Conversion Rate cannot be negative")
        End If
        Return True
    End Function
    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsLCRequest.DeleteData(fndLCRequestcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data deleted successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()

        fndLCRequestcode.Value = ""
        fndBankCode.Value = ""
        LblBankName.Text = ""
        FndDraweeBankCode.Value = ""
        TxtDraweeBank.Text = ""
        TxtLCAmount.Value = 0
        TxtFDPer.Value = 0
        TxtLCPeriod.Value = 0
        TxtFDPeriod.Value = 0
        TxtLCExtend.Value = 0
        FndPurchaseOrderNo.Value = ""
        FndVendor.Value = ""
        lblvendor.Text = ""
        LoadLCPeriod()
        LoadFDPeriod()
        LoadLCExtendPeriod()
        LoadMixedPD()
        LoadDeferredPD()
        LoadPartialShipment()
        LoadTransShipment()
        LoadAvailableBy()
        LoadDraftsatTenorFrom()
        LoadLCType()
        TxtRemarks.Text = ""
        TxtNoofDays.Value = 0
        cboLCPeriod.SelectedValue = ""
        cboFDPeriod.SelectedValue = ""
        cboLCExtend.SelectedValue = ""
        cmbLCType.SelectedValue = "S"
        txtCurrencyCode.Value = ""
        txtConversionRate.Value = 1
        TxtLocationCode.Text = ""
        lblLocationDesc.Text = ""
        TxtSGSWaiverRefNo.Text = ""
        txtSGSWaiverContext.Text = ""
        TxtMerchantDecrefNo.Text = ""
        TxtMerchantDecContext.Text = ""
        TxtAdCodeNo.Text = ""
        TxtFormNo.Text = ""
        TxtSerailNo.Text = ""
        TxtPurposeCode.Text = ""
        TxtPurposeGroupName.Text = ""
        TxtPlace.Text = ""
        strDescofGoodsforprint = ""
        ''richa agarwal 13/04/2015
        rdbAgainstPO.IsChecked = True
        FndPurchaseInvoiceNo.Value = ""
        FndPurchaseOrderNo.Enabled = True
        FndPurchaseInvoiceNo.Enabled = False
        ''--------------
        RadPageView1.SelectedPage = RadPageViewPage1
        UcAttachment1.BlankAllControls()
        FndVendor.Enabled = False
        UsLock1.Status = ERPTransactionStatus.Pending
        txtLCRequestdate.Value = clsCommon.GETSERVERDATE()
        fndLCRequestcode.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnsave.Enabled = True
        isNewEntry = True
        TxtNoofDays.Enabled = True
        TxtRemarks.Enabled = False
        loadBlankItemGrid()
        loadBlankLCIssuingGrid()
        ReStoreGridLayout()
        isLoadInsideData = True
        FillLCIssuing()

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmLCRequest)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnsetting.Visible = MyBase.isPrintFlag
    End Sub
    Sub GetBalanceQty()
        Dim qry As String = String.Empty
        If rdbAgainstPO.IsChecked Then
            qry = " Select AA.Line_No, AA.Location ,AA.LocationDesc ,AA.PurchaseOrderno,AA.[Vendor Code],AA.[Vendor Name] ,AA.[Unit Code] ,AA.[Payment Terms Group Code],AA.PendingQty,AA.ItemCode ,AA.ItemName from ( Select MAX(Line_No) as Line_No,MAX(Code) as PurchaseOrderno,MAX([Vendor Code])as [Vendor Code],MAX([Vendor Name]) as [Vendor Name],MAX(ICode) as ItemCode,MAX(IName) as ItemName,MAX(Unit_code) as [Unit Code],MAX(Bill_To_Location) as Location,MAX(Location_Desc) as LocationDesc,MAX(MT_Payment_Terms_Group_Code) as [Payment Terms Group Code] ,SUM(Qty* case when RI=1 then 1 else 0 end) as POQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as LCQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty from ( " & _
            " Select TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_PURCHASE_ORDER_DETAIL.Line_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as Code,CONVERT(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,103) as Date,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as [Vendor Code],TSPL_PURCHASE_ORDER_HEAD.Vendor_Name as [Vendor Name],TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code," & _
            " TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.Unit_code ," & _
 " TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,1 as RI  from TSPL_PURCHASE_ORDER_HEAD Left Outer Join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location  where TSPL_PURCHASE_ORDER_HEAD.Status=1  AND isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code,'') IN (Select TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT LEFT OUTER JOIN TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT ON TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code=TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code LEFT OUTER JOIN TSPL_PAYMENT_TERMS_MASTER_MT ON TSPL_PAYMENT_TERMS_MASTER_MT.Code=TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code WHERE TSPL_PAYMENT_TERMS_MASTER_MT.TermsType='L')" & _
            "  Union All" & _
            " Select '' as Bill_To_Location,'' as Location_Desc,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,isnull(TSPL_LC_REQUEST_MT.PurchaseOrder_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code ,TSPL_LC_REQUEST_DETAIL_MT.Qty as Qty,0 as Unapproved,-1 as RI  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=1 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0" & _
            " Union All" & _
            "  Select '' as Bill_To_Location,'' as Location_Desc,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,isnull(TSPL_LC_REQUEST_MT.PurchaseOrder_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code,0 as Qty,TSPL_LC_REQUEST_DETAIL_MT.Qty as Unapproved,-1 as RI  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=0 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0 and TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo not in ('" + fndLCRequestcode.Value + "')" & _
            "  )Final where Final.Code ='" & FndPurchaseOrderNo.Value & "' group by Code,ICode) AA "
            '"  )Final where Final.Code ='" & FndPurchaseOrderNo.Value & "' group by Code,Icode having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA "
        Else
            qry = " Select AA.Line_No, AA.Location ,AA.LocationDesc ,AA.PurchaseInvoiceno,AA.[Unit Code] ,AA.[Payment Terms Group Code],AA.PendingQty,AA.ItemCode ,AA.ItemName,AA.ConvRate,AA.CURRENCY_CODE,AA.Rate from ( Select Max(CURRENCY_CODE) as CURRENCY_CODE ,Max(ConvRate) as ConvRate, MAX(Line_No) as Line_No,MAX(Code) as PurchaseInvoiceno,MAX([Vendor Code])as [Vendor Code],MAX([Vendor Name]) as [Vendor Name],MAX(ICode) as ItemCode,MAX(IName) as ItemName,MAX(Unit_code) as [Unit Code],MAX(Bill_To_Location) as Location,MAX(Location_Desc) as LocationDesc,MAX(MT_Payment_Terms_Group_Code) as [Payment Terms Group Code] ,SUM(Qty* case when RI=1 then 1 else 0 end) as POQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as LCQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty,Sum(Rate) as Rate  from ( " & _
                " Select TSPL_EX_PI_HEAD.CURRENCY_CODE ,TSPL_EX_PI_HEAD.ConvRate,TSPL_EX_PI_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_EX_PI_DETAIL.Line_No ,TSPL_EX_PI_HEAD.Document_Code as Code,CONVERT(varchar,TSPL_EX_PI_HEAD.Document_Date ,103) as Date,'' as [Vendor Code],'' as [Vendor Name],TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code, TSPL_EX_PI_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_EX_PI_DETAIL.Unit_code , TSPL_EX_PI_DETAIL.Qty as Qty,0 as Unapproved,1 as RI,TSPL_EX_PI_DETAIL.Item_Cost as Rate  from TSPL_EX_PI_HEAD Left Outer Join TSPL_EX_PI_DETAIL on TSPL_EX_PI_DETAIL.DOCUMENT_CODE =TSPL_EX_PI_HEAD.Document_Code left outer join TSPL_PAYMENT_TERMS_GROUP_MASTER_MT on TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code = TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code left outer join TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT on TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code =TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code left outer join TSPL_PAYMENT_TERMS_MASTER_MT on TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code = TSPL_PAYMENT_TERMS_MASTER_MT.Code " & _
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_EX_PI_DETAIL.Item_Code " & _
                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EX_PI_HEAD.Bill_To_Location" & _
                " where  len(isnull(TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code,''))>0  and len(isnull(TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code,''))>0 and TSPL_PAYMENT_TERMS_MASTER_MT.TermsType ='L' and  TSPL_EX_PI_HEAD.Status=1  and TSPL_EX_PI_HEAD.Document_Code not in(Select MT_PI_No from TSPL_PURCHASE_ORDER_HEAD where isnull(MT_PI_No,'')<>'')" & _
                " Union All " & _
                " Select '' as CURRENCY_CODE ,0 as ConvRate,'' as Bill_To_Location,'' as Location_Desc,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,ISNULL(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code ,TSPL_LC_REQUEST_DETAIL_MT.Qty as Qty,0 as Unapproved ,-1 as RI,0 as Rate  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=1 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0 " & _
                " Union All  " & _
                " Select '' as CURRENCY_CODE ,0 as ConvRate,'' as Bill_To_Location,'' as Location_Desc,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,ISNULL(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code,0 as Qty,TSPL_LC_REQUEST_DETAIL_MT.Qty as Unapproved,-1 as RI,0 as Rate  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=0 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0 and TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo not in ('" + fndLCRequestcode.Value + "') " & _
           " )Final where Final.Code ='" & FndPurchaseInvoiceNo.Value & "' group by Code,ICode ) AA "
            '" )Final where Final.Code ='" & FndPurchaseInvoiceNo.Value & "' group by Code having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA "
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To gv1.Rows.Count - 1
                ' If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value), clsCommon.myCstr(dt.Rows(i)("ItemCode"))) = CompairStringResult.Equal Then
                gv1.Rows(i).Cells(colPOQty).Value = clsCommon.myCdbl(dt.Rows(i)("PendingQty"))
                'End If
            Next
        End If
    End Sub
    Sub FillPurchaseOrderDetailintoGrid()
        If clsCommon.myLen(FndPurchaseOrderNo.Value) > 0 Then
            loadBlankItemGrid()
            Dim qry As String = " Select AA.Line_No, AA.Location ,AA.LocationDesc ,AA.PurchaseOrderno,AA.[Vendor Code],AA.[Vendor Name] ,AA.[Unit Code] ,AA.[Payment Terms Group Code],AA.PendingQty,AA.ItemCode ,AA.ItemName,AA.ConvRate,AA.CURRENCY_CODE,AA.Rate from ( Select Max(CURRENCY_CODE) as CURRENCY_CODE ,Max(ConvRate) as ConvRate, MAX(Line_No) as Line_No,MAX(Code) as PurchaseOrderno,MAX([Vendor Code])as [Vendor Code],MAX([Vendor Name]) as [Vendor Name],MAX(ICode) as ItemCode,MAX(IName) as ItemName,MAX(Unit_code) as [Unit Code],MAX(Bill_To_Location) as Location,MAX(Location_Desc) as LocationDesc,MAX(MT_Payment_Terms_Group_Code) as [Payment Terms Group Code] ,SUM(Qty* case when RI=1 then 1 else 0 end) as POQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as LCQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty,Sum(Rate) as Rate  from ( "
            qry += " Select TSPL_PURCHASE_ORDER_HEAD.CURRENCY_CODE ,TSPL_PURCHASE_ORDER_HEAD.ConvRate, TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_PURCHASE_ORDER_DETAIL.Line_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as Code,CONVERT(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,103) as Date,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as [Vendor Code],TSPL_PURCHASE_ORDER_HEAD.Vendor_Name as [Vendor Name],TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code,"
            qry += " TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.Unit_code ,"
            ' qry += " TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate   from TSPL_PURCHASE_ORDER_HEAD Left Outer Join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location  left outer join TSPL_PAYMENT_TERMS_GROUP_MASTER_MT on TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code = TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code left outer join TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT on TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code =TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code left outer join TSPL_PAYMENT_TERMS_MASTER_MT on TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code = TSPL_PAYMENT_TERMS_MASTER_MT.Code where   len(isnull(TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code,''))>0  and len(isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code,''))>0 and TSPL_PAYMENT_TERMS_MASTER_MT.TermsType ='L' and  TSPL_PURCHASE_ORDER_HEAD.Status=1 "
            qry += " TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,1 as RI,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost as Rate  from TSPL_PURCHASE_ORDER_HEAD Left Outer Join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location where TSPL_PURCHASE_ORDER_HEAD.Status=1  AND isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code,'') IN (Select TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT LEFT OUTER JOIN TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT ON TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code=TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code LEFT OUTER JOIN TSPL_PAYMENT_TERMS_MASTER_MT ON TSPL_PAYMENT_TERMS_MASTER_MT.Code=TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code WHERE TSPL_PAYMENT_TERMS_MASTER_MT.TermsType='L')"
            qry += "  Union All"
            qry += " Select '' as CURRENCY_CODE ,0 as ConvRate,'' as Bill_To_Location,'' as Location_Desc,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,ISNULL(TSPL_LC_REQUEST_MT.PurchaseOrder_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code ,TSPL_LC_REQUEST_DETAIL_MT.Qty as Qty,0 as Unapproved ,-1 as RI,0 as Rate  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=1 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0"
            qry += " Union All"
            qry += "  Select '' as CURRENCY_CODE ,0 as ConvRate,'' as Bill_To_Location,'' as Location_Desc,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,ISNULL(TSPL_LC_REQUEST_MT.PurchaseOrder_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code,0 as Qty,TSPL_LC_REQUEST_DETAIL_MT.Qty as Unapproved,-1 as RI,0 as Rate  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=0 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0 and TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo not in ('" + fndLCRequestcode.Value + "')"
            qry += "  )Final where Final.Code ='" & FndPurchaseOrderNo.Value & "' group by Code,ICode having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FndVendor.Value = clsCommon.myCstr(dt.Rows(0)("Vendor Code"))
                lblvendor.Text = clsCommon.myCstr(dt.Rows(0)("Vendor Name"))
                TxtLCAmount.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select case when MT_Is_AmountinRs=1 then MT_LC+MT_CAD+MT_RETAINED+MT_Balance_Payment +MT_On_Account +MT_Advance+MT_CIF else (PO_Total_Amt * (MT_LC+MT_CAD+MT_RETAINED+MT_Balance_Payment +MT_On_Account +MT_Advance+MT_CIF)/100)  end as LCAMount from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" & FndPurchaseOrderNo.Value & "'"))
                TxtLocationCode.Text = clsCommon.myCstr(dt.Rows(0)("Location"))
                lblLocationDesc.Text = clsCommon.myCstr(dt.Rows(0)("LocationDesc"))
                txtCurrencyCode.Value = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
                txtConversionRate.Value = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))

                For Each dr As DataRow In dt.Rows
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = clsCommon.myCstr(dr("Line_No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = clsCommon.myCstr(dr("PurchaseOrderno"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("ItemCode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("ItemName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr("Unit Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPOQty).Value = clsCommon.myCdbl(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows.AddNew()
                Next
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                funtofillDescriptionofGoods()
            End If
        End If
    End Sub
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New ClsLCRequest()
                obj.LCRequestNo = fndLCRequestcode.Value
                obj.LCRequest_Date = txtLCRequestdate.Value
                obj.Bank_Code = clsCommon.myCstr(fndBankCode.Value)
                obj.Bank_Name = clsCommon.myCstr(LblBankName.Text)

                If rdbAgainstPO.IsChecked Then
                    obj.Against = "Against PO"
                    obj.PurchaseOrder_No = clsCommon.myCstr(FndPurchaseOrderNo.Value)
                    If clsCommon.myLen(FndPurchaseInvoiceNo.Value) > 0 Then
                        obj.PurchaseInvoice_No = clsCommon.myCstr(FndPurchaseInvoiceNo.Value)
                    End If
                Else
                    obj.Against = "Against PI"
                    obj.PurchaseInvoice_No = clsCommon.myCstr(FndPurchaseInvoiceNo.Value)
                End If

                obj.VendorCode = clsCommon.myCstr(FndVendor.Value)
                obj.LCAmount = clsCommon.myCdbl(TxtLCAmount.Value)
                obj.FDPer = clsCommon.myCdbl(TxtFDPer.Value)
                obj.Drawee_Bank_Code = clsCommon.myCstr(FndDraweeBankCode.Value)
                obj.Drawee_Bank_Name = clsCommon.myCstr(TxtDraweeBank.Text)
                obj.LCPeriod = clsCommon.myCdbl(TxtLCPeriod.Value)
                obj.LCPeriodType = clsCommon.myCstr(cboLCPeriod.SelectedValue)
                obj.LCType = clsCommon.myCstr(cmbLCType.SelectedValue)
                obj.FDPeriod = clsCommon.myCdbl(TxtFDPeriod.Value)
                obj.FDPeriodType = clsCommon.myCstr(cboFDPeriod.SelectedValue)
                obj.LCExtendPeriod = clsCommon.myCdbl(TxtLCExtend.Value)
                obj.LCExtendPeriodType = clsCommon.myCstr(cboLCExtend.SelectedValue)
                ' obj.LCExpiryDate = lblLCExpiryDate.Text
                obj.Location_Code = clsCommon.myCstr(TxtLocationCode.Text)
                obj.Location_Desc = clsCommon.myCstr(lblLocationDesc.Text)
                obj.CURRENCY_CODE = clsCommon.myCstr(txtCurrencyCode.Value)
                obj.ConvRate = clsCommon.myCdbl(txtConversionRate.Value)
                ''SGS Waiver------------------
                obj.SGS_Waiver_Ref_no = clsCommon.myCstr(TxtSGSWaiverRefNo.Text)
                obj.SGS_Waiver_Context = clsCommon.myCstr(txtSGSWaiverContext.Text)
                ''Merchant Declaration'--------
                obj.Merchant_Dec_Ref_no = clsCommon.myCstr(TxtMerchantDecrefNo.Text)
                obj.Merchant_Dec_Context = clsCommon.myCstr(TxtMerchantDecContext.Text)
                ''Form A2------------------
                obj.AD_Code_No = clsCommon.myCstr(TxtAdCodeNo.Text)
                obj.Form_No = clsCommon.myCstr(TxtFormNo.Text)
                obj.Serial_No = clsCommon.myCstr(TxtSerailNo.Text)
                obj.Purpose_Group_Name = clsCommon.myCstr(TxtPurposeGroupName.Text)
                obj.Purpose_Code = clsCommon.myCstr(TxtPurposeCode.Text)
                ''LC Issuing Application
                obj.MixedPaymentDetails = clsCommon.myCstr(cboMixedPD.SelectedValue)
                obj.DeferredPaymentDetails = clsCommon.myCstr(cboDeferredPD.SelectedValue)
                obj.PartialShipment = clsCommon.myCstr(cboPartialShipment.SelectedValue)
                obj.TransShipment = clsCommon.myCstr(cboTransshipment.SelectedValue)
                obj.AvailableBy = clsCommon.myCstr(cboAvailableBy.SelectedValue)
                obj.Place = clsCommon.myCstr(TxtPlace.Text)
                obj.DraftsFrom = clsCommon.myCstr(cboFrom.SelectedValue)
                obj.DraftsNoofDays = clsCommon.myCdbl(TxtNoofDays.Value)
                obj.DraftsRemarks = clsCommon.myCstr(TxtRemarks.Text)
                obj.DescriptionofGoods = clsCommon.myCstr(strDescofGoodsforprint)
                Dim objTr As New ClsLCRequestDetail
                obj.arrLCRequestDetail = New List(Of ClsLCRequestDetail)


                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New ClsLCRequestDetail()
                    objTr.LCRequestNo = clsCommon.myCstr(obj.LCRequestNo)
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                    If rdbAgainstPO.IsChecked Then
                        objTr.PurchaseOrder_No = clsCommon.myCstr(grow.Cells(colPONo).Value)
                    Else
                        objTr.PurchaseInvoice_No = clsCommon.myCstr(grow.Cells(colPONo).Value)
                    End If
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                    objTr.Qty = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Rate = clsCommon.myCdbl(grow.Cells(colRate).Value)
                    obj.arrLCRequestDetail.Add(objTr)
                Next

                Dim objTra As New ClsLCRequestIssueApplicationDetail
                obj.arrLCRequestISSUEDetail = New List(Of ClsLCRequestIssueApplicationDetail)

                Dim K As Integer = 1
                For Each grow As GridViewRowInfo In gvLCIssuing.Rows
                    objTra = New ClsLCRequestIssueApplicationDetail()
                    objTra.SNo = K
                    objTra.LCRequestNo = clsCommon.myCstr(obj.LCRequestNo)
                    objTra.IssueTag = clsCommon.myCstr(grow.Cells(colTag).Value)
                    objTra.IssueType = clsCommon.myCstr(grow.Cells(colType).Value)
                    objTra.IssueFieldname = clsCommon.myCstr(grow.Cells(colFieldName).Value)
                    objTra.IssueDetails = clsCommon.myCstr(grow.Cells(colDetails).Value)
                    obj.arrLCRequestISSUEDetail.Add(objTra)
                    K = K + 1
                Next
                K = 0
                If (ClsLCRequest.SaveData(obj, isNewEntry)) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                    End If
                    LoadData(obj.LCRequestNo, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsLCRequest = ClsLCRequest.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isLoadData = True
            isNewEntry = False
            loadBlankItemGrid()
            loadBlankLCIssuingGrid()
            fndLCRequestcode.Value = obj.LCRequestNo
            txtLCRequestdate.Value = obj.LCRequest_Date
            fndBankCode.Value = obj.Bank_Code
            LblBankName.Text = obj.Bank_Name
            FndPurchaseOrderNo.Value = obj.PurchaseOrder_No
            If clsCommon.CompairString(obj.Against, "Against PO") = CompairStringResult.Equal Then
                rdbAgainstPO.IsChecked = True
            Else
                rdbAgainstPI.IsChecked = True
            End If
            FndPurchaseOrderNo.Value = obj.PurchaseOrder_No
            FndPurchaseInvoiceNo.Value = obj.PurchaseInvoice_No
            FndVendor.Value = obj.VendorCode
            lblvendor.Text = obj.VendorName
            TxtLCAmount.Value = obj.LCAmount
            TxtFDPer.Value = obj.FDPer
            FndDraweeBankCode.Value = obj.Drawee_Bank_Code
            TxtDraweeBank.Text = obj.Drawee_Bank_Name
            TxtLCPeriod.Value = obj.LCPeriod
            cboLCPeriod.SelectedValue = obj.LCPeriodType
            cmbLCType.SelectedValue = obj.LCType
            TxtFDPeriod.Value = obj.FDPeriod
            cboFDPeriod.SelectedValue = obj.FDPeriodType
            TxtLCExtend.Value = obj.LCExtendPeriod
            cboLCExtend.SelectedValue = obj.LCExtendPeriodType
            ' lblLCExpiryDate.Text = obj.LCExpiryDate
            TxtLocationCode.Text = obj.Location_Code
            lblLocationDesc.Text = obj.Location_Desc
            txtCurrencyCode.Value = obj.CURRENCY_CODE
            txtConversionRate.Value = obj.ConvRate
            ''SGS Waiver------------------
            TxtSGSWaiverRefNo.Text = obj.SGS_Waiver_Ref_no
            txtSGSWaiverContext.Text = obj.SGS_Waiver_Context
            ''Merchant Declaration'--------
            TxtMerchantDecrefNo.Text = obj.Merchant_Dec_Ref_no
            TxtMerchantDecContext.Text = obj.Merchant_Dec_Context
            ''Form A2------------------
            TxtAdCodeNo.Text = obj.AD_Code_No
            TxtFormNo.Text = obj.Form_No
            TxtSerailNo.Text = obj.Serial_No
            TxtPurposeCode.Text = obj.Purpose_Code
            TxtPurposeGroupName.Text = obj.Purpose_Group_Name
            ''LC Issuing Application
            cboMixedPD.SelectedValue = obj.MixedPaymentDetails
            cboDeferredPD.SelectedValue = obj.DeferredPaymentDetails
            cboPartialShipment.SelectedValue = obj.PartialShipment
            cboTransshipment.SelectedValue = obj.TransShipment
            cboAvailableBy.SelectedValue = obj.AvailableBy
            TxtPlace.Text = obj.Place
            cboFrom.SelectedValue = obj.DraftsFrom
            TxtNoofDays.Value = obj.DraftsNoofDays
            TxtRemarks.Text = obj.DraftsRemarks
            strDescofGoodsforprint = obj.DescriptionofGoods
            fndLCRequestcode.MyReadOnly = True

            If obj.arrLCRequestDetail IsNot Nothing AndAlso obj.arrLCRequestDetail.Count > 0 Then
                For Each objTr As ClsLCRequestDetail In obj.arrLCRequestDetail
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                    If clsCommon.CompairString(obj.Against, "Against PO") = CompairStringResult.Equal Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objTr.PurchaseOrder_No
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = objTr.PurchaseInvoice_No
                    End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = objTr.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = objTr.Unit_code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
                    gv1.Rows.AddNew()
                Next
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                GetBalanceQty()
            Else
                gv1.DataSource = Nothing
            End If

            If obj.arrLCRequestISSUEDetail IsNot Nothing AndAlso obj.arrLCRequestISSUEDetail.Count > 0 Then
                For Each objTra As ClsLCRequestIssueApplicationDetail In obj.arrLCRequestISSUEDetail
                    If gvLCIssuing.Rows.Count = 0 Then
                        gvLCIssuing.Rows.AddNew()
                    End If
                    gvLCIssuing.Rows(gvLCIssuing.Rows.Count - 1).Cells(colType).Value = objTra.IssueType
                    gvLCIssuing.Rows(gvLCIssuing.Rows.Count - 1).Cells(colTag).Value = objTra.IssueTag
                    gvLCIssuing.Rows(gvLCIssuing.Rows.Count - 1).Cells(colFieldName).Value = objTra.IssueFieldname
                    gvLCIssuing.Rows(gvLCIssuing.Rows.Count - 1).Cells(colDetails).Value = objTra.IssueDetails
                    gvLCIssuing.Rows.AddNew()
                Next
                gvLCIssuing.Rows.RemoveAt(gvLCIssuing.Rows.Count - 1)
            Else
                gvLCIssuing.DataSource = Nothing
            End If
            'EnableDisableLCPeriod()
            btnsave.Text = "Update"
            'gvLCIssuing.Columns(colDetails).ReadOnly = False
            'gvLCIssuing.Columns(colFieldName).ReadOnly = True
            'gvLCIssuing.Columns(colTag).ReadOnly = True
            'gvLCIssuing.Columns(colType).ReadOnly = True
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
        Else
            Reset()
        End If
        isLoadData = False
    End Sub
    Sub PostData()
        Try
            isFlag = True
            If (myMessages.postConfirm()) Then
                SaveData()
                If (ClsLCRequest.PostData(MyBase.Form_ID, fndLCRequestcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Successfully posted")
                    LoadData(fndLCRequestcode.Value, NavigatorType.Current)
                End If
            End If
            isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
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

        Dim PurchaseOrderNo As New GridViewTextBoxColumn()
        PurchaseOrderNo.FormatString = ""
        PurchaseOrderNo.HeaderText = "Purchase Order No"
        PurchaseOrderNo.Name = colPONo
        PurchaseOrderNo.Width = 100
        PurchaseOrderNo.ReadOnly = True
        PurchaseOrderNo.WrapText = True
        PurchaseOrderNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(PurchaseOrderNo)

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

        Dim strUnitCode As New GridViewTextBoxColumn()
        strUnitCode.FormatString = ""
        strUnitCode.HeaderText = "UOM"
        strUnitCode.Name = colUnitCode
        strUnitCode.Width = 120
        strUnitCode.ReadOnly = True
        strUnitCode.WrapText = True
        strUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(strUnitCode)

        Dim POQty As New GridViewDecimalColumn
        POQty.FormatString = "{0:n3}"
        POQty.HeaderText = "POQty"
        POQty.DecimalPlaces = 3
        POQty.Name = colPOQty
        POQty.Width = 120
        POQty.ReadOnly = True
        POQty.WrapText = True
        POQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(POQty)

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
        Rate.ReadOnly = True
        Rate.WrapText = True
        Rate.IsVisible = False
        Rate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(Rate)

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

    End Sub
    Private Sub FndPurchaseOrderNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndPurchaseOrderNo._MYValidating
        If rdbAgainstPO.IsChecked Then
            Dim qry As String = " Select Distinct AA.PurchaseOrderno,AA.[Vendor Code],AA.[Vendor Name] ,AA.[Unit Code] ,AA.[Payment Terms Group Code] from ( Select MAX(Code) as PurchaseOrderno,MAX([Vendor Code])as [Vendor Code],MAX([Vendor Name]) as [Vendor Name],MAX(Unit_code) as [Unit Code],MAX(MT_Payment_Terms_Group_Code) as [Payment Terms Group Code] ,SUM(Qty* case when RI=1 then 1 else 0 end) as POQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as LCQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty from ( "
            qry += " Select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as Code,CONVERT(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date ,103) as Date,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code as [Vendor Code],TSPL_PURCHASE_ORDER_HEAD.Vendor_Name as [Vendor Name],TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code,"
            qry += " TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.Item_Desc as IName,TSPL_PURCHASE_ORDER_DETAIL.Unit_code ,"
            ' qry += " TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,1 as RI  from TSPL_PURCHASE_ORDER_HEAD Left Outer Join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No   left outer join TSPL_PAYMENT_TERMS_GROUP_MASTER_MT on TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code = TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code left outer join TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT on TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code =TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code left outer join TSPL_PAYMENT_TERMS_MASTER_MT on TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code = TSPL_PAYMENT_TERMS_MASTER_MT.Code where   len(isnull(TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code,''))>0  and len(isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code,''))>0 and TSPL_PAYMENT_TERMS_MASTER_MT.TermsType ='L' and  TSPL_PURCHASE_ORDER_HEAD.Status=1 "
            qry += " TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,0 as Unapproved,1 as RI  from TSPL_PURCHASE_ORDER_HEAD Left Outer Join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No  where TSPL_PURCHASE_ORDER_HEAD.Status=1 And TSPL_PURCHASE_ORDER_HEAD.close_yn ='N' and TSPL_PURCHASE_ORDER_HEAD.IsCancel  =0 AND isnull(TSPL_PURCHASE_ORDER_HEAD.MT_Payment_Terms_Group_Code,'') IN (Select TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code from TSPL_PAYMENT_TERMS_GROUP_MASTER_MT LEFT OUTER JOIN TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT ON TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code=TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code LEFT OUTER JOIN TSPL_PAYMENT_TERMS_MASTER_MT ON TSPL_PAYMENT_TERMS_MASTER_MT.Code=TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code WHERE TSPL_PAYMENT_TERMS_MASTER_MT.TermsType='L')"
            qry += "  Union All"
            qry += " Select ISNULL(TSPL_LC_REQUEST_MT.PurchaseOrder_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code ,TSPL_LC_REQUEST_DETAIL_MT.Qty as Qty,0 as Unapproved ,-1 as RI  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=1 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0"
            qry += " Union All"
            qry += "  Select ISNULL(TSPL_LC_REQUEST_MT.PurchaseOrder_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code, 0 as Qty,TSPL_LC_REQUEST_DETAIL_MT.Qty as Unapproved ,-1 as RI  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=0 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0 and TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo not in ('" + fndLCRequestcode.Value + "')"
            qry += "  )Final group by Code having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA "

            FndPurchaseOrderNo.Value = clsCommon.ShowSelectForm("formPurchaseOrderNo", qry, "PurchaseOrderno", "", FndPurchaseOrderNo.Value, "", isButtonClicked)
            If clsCommon.myLen(FndPurchaseOrderNo.Value) > 0 Then
                FillPurchaseOrderDetailintoGrid()
            Else
                FndVendor.Value = ""
                lblvendor.Text = ""
                TxtLocationCode.Text = ""
                lblLocationDesc.Text = ""
                TxtLCAmount.Value = 0
                loadBlankItemGrid()
            End If
            Dim dt As DataTable = Nothing
            If clsCommon.myLen(FndVendor.Value) > 0 Then
                dt = clsDBFuncationality.GetDataTable("Select Vendor_Name ,Add1+', '+Add2 as Address from TSPL_VENDOR_MASTER where Vendor_Code ='" & FndVendor.Value & "'")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvLCIssuing.Rows(9).Cells(colDetails).Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Name")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("Address"))
                End If
            End If
            Dim dbldecimalpartofamount As Double = 0
            Dim strdecimalpartstring As String = String.Empty
            If TxtLCAmount.Value.ToString.Contains(".") Then
                dbldecimalpartofamount = TxtLCAmount.Value.ToString.Substring(TxtLCAmount.Value.ToString.IndexOf(".") + 1, TxtLCAmount.Value.ToString.Length - TxtLCAmount.Value.ToString.IndexOf(".") - 1)
                strdecimalpartstring = clsCommon.changeNumericToWords(dbldecimalpartofamount)
                gvLCIssuing.Rows(10).Cells(colDetails).Value = clsCommon.myCstr(txtCurrencyCode.Value + " " + clsCommon.myCstr(TxtLCAmount.Value) + " (USD " + clsCommon.myCstr(NumberToUSDString(TxtLCAmount.Value)) + " and " + strdecimalpartstring + "cents only)")
                'txtCurrencyCode.Value + "; " +
            Else
                gvLCIssuing.Rows(10).Cells(colDetails).Value = clsCommon.myCstr(txtCurrencyCode.Value + " " + clsCommon.myCstr(TxtLCAmount.Value) + " (USD " + clsCommon.myCstr(NumberToUSDString(TxtLCAmount.Value)) + " only)")
                'txtCurrencyCode.Value + "; " +
            End If
            'gvLCIssuing.Rows(10).Cells(colDetails).Value = clsCommon.myCstr(txtCurrencyCode.Value + "; " + txtCurrencyCode.Value + " " + clsCommon.myCstr(TxtLCAmount.Value) + " (USD " + clsCommon.myCstr(NumberToUSDString(TxtLCAmount.Value)) + ")")
            ' dt = clsDBFuncationality.GetDataTable("SELECT Discharge_Port ,Final_Destination  FROM TSPL_EX_PI_HEAD where TSPL_EX_PI_HEAD.Document_Code= ( Select MT_PI_No  from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" & FndPurchaseOrderNo.Value & "')")
            dt = clsDBFuncationality.GetDataTable("Select MT_Final_Destination,MT_Discharge_Port,isnull(MT_PI_No,'') as MT_PI_No from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" & FndPurchaseOrderNo.Value & "'")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvLCIssuing.Rows(23).Cells(colDetails).Value = clsCommon.myCstr(dt.Rows(0)("MT_Discharge_Port"))
                gvLCIssuing.Rows(24).Cells(colDetails).Value = clsCommon.myCstr(dt.Rows(0)("MT_Final_Destination"))
                If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("MT_PI_No"))) > 0 Then
                    FndPurchaseInvoiceNo.Value = clsCommon.myCstr(dt.Rows(0)("MT_PI_No"))
                End If
            End If
            dt = Nothing

        End If
    End Sub


    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub FndDraweeBankCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndDraweeBankCode._MYValidating
        FndDraweeBankCode.Value = clsBankMaster.getFinder("", FndDraweeBankCode.Value, isButtonClicked)
        TxtDraweeBank.Text = clsDBFuncationality.getSingleValue("Select DESCRIPTION  from TSPL_BANK_MASTER where BANK_CODE  ='" + FndDraweeBankCode.Value + "' ")

        gvLCIssuing.Rows(16).Cells(colDetails).Value = FndDraweeBankCode.Value
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("  Select DESCRIPTION ,'Address: '+ADD1+', '+ADD2 as Address,'A/C No.:- '+BANKACCNUMBER as AccountNo,'IBAN No.:- '+IBAN_No as IBANNo,'Swift Code.:- '+Swift_Code as SwiftCode from TSPL_BANK_MASTER  where BANK_CODE ='" & FndDraweeBankCode.Value & "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gvLCIssuing.Rows(35).Cells(colDetails).Value = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("Address")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("AccountNo")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("IBANNo")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("SwiftCode"))
        Else
            gvLCIssuing.Rows(35).Cells(colDetails).Value = ""
        End If
        FillAvailableBy()
    End Sub
    Sub LoadLCPeriod()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", " ")
        dt.Rows.Add("D", "Days")
        dt.Rows.Add("M", "Month")
        dt.Rows.Add("Y", "Year")

        cboLCPeriod.DataSource = dt
        cboLCPeriod.ValueMember = "Code"
        cboLCPeriod.DisplayMember = "Name"
    End Sub
    Sub LoadLCType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("S", "Sight")
        dt.Rows.Add("U", "Usance")

        cmbLCType.DataSource = dt
        cmbLCType.ValueMember = "Code"
        cmbLCType.DisplayMember = "Name"
    End Sub
    Sub LoadFDPeriod()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", " ")
        dt.Rows.Add("D", "Days")
        dt.Rows.Add("M", "Month")
        dt.Rows.Add("Y", "Year")

        cboFDPeriod.DataSource = dt
        cboFDPeriod.ValueMember = "Code"
        cboFDPeriod.DisplayMember = "Name"
    End Sub
    Sub LoadLCExtendPeriod()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", " ")
        dt.Rows.Add("D", "Days")
        dt.Rows.Add("M", "Month")
        dt.Rows.Add("Y", "Year")

        cboLCExtend.DataSource = dt
        cboLCExtend.ValueMember = "Code"
        cboLCExtend.DisplayMember = "Name"
    End Sub

    Sub FunLCExpiryDate()
        If isLoadInsideData Then
            Dim strLCExpiryDate As String = ""
            If clsCommon.myLen(TxtLCPeriod.Value) <= 0 Then
                TxtLCPeriod.Value = 0
            End If
            If clsCommon.CompairString(cboLCPeriod.SelectedValue, "D") = CompairStringResult.Equal Then
                strLCExpiryDate = txtLCRequestdate.Value.AddDays(TxtLCPeriod.Value).ToShortDateString()
            ElseIf clsCommon.CompairString(cboLCPeriod.SelectedValue, "M") = CompairStringResult.Equal Then
                strLCExpiryDate = txtLCRequestdate.Value.AddMonths(TxtLCPeriod.Value).ToShortDateString()
            ElseIf clsCommon.CompairString(cboLCPeriod.SelectedValue, "Y") = CompairStringResult.Equal Then
                strLCExpiryDate = txtLCRequestdate.Value.AddYears(TxtLCPeriod.Value).ToShortDateString()
            End If
            'If clsCommon.myLen(strLCExpiryDate) > 0 Then
            '    lblLCExpiryDate.Text = strLCExpiryDate
            'End If

            If gvLCIssuing.Rows.Count > 0 Then
                ' gvLCIssuing.Rows(6).Cells(colDetails).Value = clsCommon.myCstr(lblLCExpiryDate.Text)
                gvLCIssuing.Rows(6).Cells(colDetails).Value = strLCExpiryDate
            End If

        End If
    End Sub
    Private Sub TxtLCPeriod_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtLCPeriod.TextChanged
        FunLCExpiryDate()
    End Sub

    Private Sub cboLCPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboLCPeriod.SelectedIndexChanged
        FunLCExpiryDate()
    End Sub

    Private Sub RDPSGSWaiverLetter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDPSGSWaiverLetter.Click
        Try
            If clsCommon.myLen(fndLCRequestcode.Value) > 0 Then
                funSGSWaiverLetterPrint()
            Else
                Throw New Exception("Please Select LC Request No first")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funSGSWaiverLetterPrint()
        Try

            Dim qry As String = "Select TSPL_LC_REQUEST_MT.LCAmount,TSPL_LC_REQUEST_MT.CURRENCY_CODE ,TSPL_LC_REQUEST_MT.ConvRate ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_MASTER.Vendor_Name as BenefecieryName,TSPL_LC_REQUEST_MT.SGS_Waiver_Ref_no as ReferenceNo,Convert(varchar,TSPL_LC_REQUEST_MT.LCRequest_Date,103) as ReferenceDate,TSPL_REPORT_FORMAT_DECLARATION_MT.SGS_Waiver_Context as ReferenceContext," & _
            " TSPL_BANK_MASTER.DESCRIPTION as BankName,'' as BankSubject,TSPL_BANK_MASTER.ADD1 ,TSPL_BANK_MASTER.ADD2,TSPL_BANK_MASTER.ADD3 ,TSPL_BANK_MASTER.CITY ,TSPL_BANK_MASTER.POSTAL,case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' then TSPL_BANK_MASTER.ADD3 else '' END + CASE when isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' Then ', ' Else '' End) +TSPL_BANK_MASTER.CITY Else '' END + CASE when isnull(TSPL_BANK_MASTER.POSTAL,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')+isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then ' - ' Else '' End)+ TSPL_BANK_MASTER.POSTAL  Else '' END as [bankAddress] from TSPL_LC_REQUEST_MT Left outer Join TSPL_BANK_MASTER on TSPL_LC_REQUEST_MT.Bank_Code=TSPL_BANK_MASTER.BANK_CODE " & _
            " Left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_LC_REQUEST_MT.Comp_Code " & _
            " left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_LC_REQUEST_MT.PurchaseOrder_No" & _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PURCHASE_ORDER_HEAD.MT_Beneficiary_Code " & _
            " Left Outer Join TSPL_REPORT_FORMAT_DECLARATION_MT on TSPL_REPORT_FORMAT_DECLARATION_MT.Comp_Code =TSPL_LC_REQUEST_MT .Comp_Code " & _
            " where 1=1 and  TSPL_LC_REQUEST_MT.LCRequestNo ='" + fndLCRequestcode.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptSGSWaiverLetterMT", "SGS Waiver Letter")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub RDPMerchantDeclaration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDPMerchantDeclaration.Click

    End Sub
    Sub funMerchantDeclarationPrint()
        Try

            Dim qry As String = "Select TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_MASTER.Vendor_Name as BenefecieryName,TSPL_LC_REQUEST_MT.Merchant_Dec_Ref_no as ReferenceNo,Convert(varchar,TSPL_LC_REQUEST_MT.LCRequest_Date,103) as ReferenceDate,TSPL_REPORT_FORMAT_DECLARATION_MT.Merchant_Dec_Context as ReferenceContext," & _
            " TSPL_BANK_MASTER.DESCRIPTION as BankName,'' as BankSubject,TSPL_BANK_MASTER.ADD1 ,TSPL_BANK_MASTER.ADD2,TSPL_BANK_MASTER.ADD3 ,TSPL_BANK_MASTER.CITY ,TSPL_BANK_MASTER.POSTAL,case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' then TSPL_BANK_MASTER.ADD3 else '' END + CASE when isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' Then ', ' Else '' End) +TSPL_BANK_MASTER.CITY Else '' END + CASE when isnull(TSPL_BANK_MASTER.POSTAL,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')+isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then ' - ' Else '' End)+ TSPL_BANK_MASTER.POSTAL  Else '' END as [bankAddress] from TSPL_LC_REQUEST_MT Left outer Join TSPL_BANK_MASTER on TSPL_LC_REQUEST_MT.Bank_Code=TSPL_BANK_MASTER.BANK_CODE " & _
            " Left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_LC_REQUEST_MT.Comp_Code " & _
            " left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_LC_REQUEST_MT.PurchaseOrder_No" & _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PURCHASE_ORDER_HEAD.MT_Beneficiary_Code " & _
            " Left Outer Join TSPL_REPORT_FORMAT_DECLARATION_MT on TSPL_REPORT_FORMAT_DECLARATION_MT.Comp_Code =TSPL_LC_REQUEST_MT .Comp_Code " & _
            " where 1=1 and  TSPL_LC_REQUEST_MT.LCRequestNo ='" + fndLCRequestcode.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptMerchantDeclarationMT", "Merchant Declaration")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub RDPFormA2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDPFormA2.Click
        Try
            If clsCommon.myLen(fndLCRequestcode.Value) > 0 Then
                funFormA2Print()
            Else
                Throw New Exception("Please Select LC Request No first")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funFormA2Print()
        Try

            Dim qry As String = "Select TSPL_Vendor_Bank_MASTER.Bank_Name +case when TSPL_Vendor_Bank_MASTER.Add1<>'' then ', '+TSPL_Vendor_Bank_MASTER.Add1 else '' end+case when TSPL_Vendor_Bank_MASTER.Add2<>'' then ', '+TSPL_Vendor_Bank_MASTER.Add2 else '' end+case when TSPL_Vendor_Bank_MASTER.Add3<>'' then ', '+TSPL_Vendor_Bank_MASTER.Add3 else '' end as BenefeciaryBankInfo,TSPL_VENDOR_MASTER.Account_No as BenefieceryAccountNo," & _
            " TSPL_LC_REQUEST_MT.CURRENCY_CODE ,TSPL_LC_REQUEST_MT.ConvRate,TSPL_LC_REQUEST_MT.LCAmount,TSPL_COMPANY_MASTER.Comp_Name+ case when TSPL_COMPANY_MASTER.CINNo <>'' then ' {CIN No.- '+TSPL_COMPANY_MASTER.CINNo+'}' else '' end   as CompanyFooterAddress,TSPL_BANK_MASTER.BANKACCNUMBER ,TSPL_COMPANY_MASTER.Comp_Name+ case when TSPL_COMPANY_MASTER.CINNo <>'' then ' {CIN No.- '+TSPL_COMPANY_MASTER.CINNo+'}' else '' end + case when TSPL_COMPANY_MASTER.Add1<>'' then ',Address - '+TSPL_COMPANY_MASTER.Add1 else '' end + case when TSPL_COMPANY_MASTER.Add2<>'' then ', '+TSPL_COMPANY_MASTER.Add2 else '' end+ case when TSPL_COMPANY_MASTER.Add3<>'' then ',Address - '+TSPL_COMPANY_MASTER.Add3 else '' end+ case when TSPL_COMPANY_MASTER.City_Code <>'' then ', '+TSPL_COMPANY_MASTER.City_Code  else '' end  as CompanyAddress,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_MASTER.Vendor_Name as BenefecieryName,TSPL_LC_REQUEST_MT.AD_Code_No ,TSPL_LC_REQUEST_MT.Form_No ,TSPL_LC_REQUEST_MT.Serial_No ,TSPL_LC_REQUEST_MT.Purpose_Code ,TSPL_LC_REQUEST_MT.Purpose_Group_Name,Convert(varchar,TSPL_LC_REQUEST_MT.LCRequest_Date,103) as ReferenceDate," & _
            " TSPL_BANK_MASTER.DESCRIPTION as BankName,'' as BankSubject,TSPL_BANK_MASTER.ADD1 ,TSPL_BANK_MASTER.ADD2,TSPL_BANK_MASTER.ADD3 ,TSPL_BANK_MASTER.CITY ,TSPL_BANK_MASTER.POSTAL from TSPL_LC_REQUEST_MT Left outer Join TSPL_BANK_MASTER on TSPL_LC_REQUEST_MT.Bank_Code=TSPL_BANK_MASTER.BANK_CODE " & _
            " Left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_LC_REQUEST_MT.Comp_Code " & _
            " left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_LC_REQUEST_MT.PurchaseOrder_No" & _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PURCHASE_ORDER_HEAD.MT_Beneficiary_Code " & _
            " Left Outer Join TSPL_Vendor_Bank_MASTER on TSPL_Vendor_Bank_MASTER.Bank_Code=TSPL_VENDOR_MASTER.Bank_Code  " & _
            " where 1=1 and  TSPL_LC_REQUEST_MT.LCRequestNo ='" + fndLCRequestcode.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptFormA2MT", "Form A2")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function NumberToUSDString(ByVal num As Double) As String
        ' Remove any fractional part.
        num = Int(num)

        ' If the number is 0, return zero.
        If num = 0 Then Return "zero"

        Static groups() As String = {"", "thousand", "million", _
            "billion", "trillion", "quadrillion", "?", "??", _
            "???", "????"}
        Dim result As String = ""

        ' Process the groups, smallest first.
        Dim quotient As Double
        Dim remainder As Integer
        Dim group_num As Integer = 0
        Do While num > 0
            ' Get the next group of three digits.
            quotient = Int(num / 1000)
            remainder = CInt(num - quotient * 1000)
            num = quotient

            ' Convert the group into words.
            result = GroupToWords(remainder) & _
                " " & groups(group_num) & ", " & _
                result

            ' Get ready for the next group.
            group_num += 1
        Loop

        '' Remove the trailing ", ".
        'If result.EndsWith(", ") Then
        '    result = result.Substring(0, result.Length - 2)
        'End If
        result = result.Replace(",", "")
        result = StrConv(result, VbStrConv.ProperCase)
        Return result.Trim()
    End Function
    Private Function GroupToWords(ByVal num As Integer) As String

        Static one_to_nineteen() As String = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eightteen", "nineteen"}

        Static multiples_of_ten() As String = {"twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"}

        ' If the number is 0, return an empty string.

        If num = 0 Then Return ""

        ' Handle the hundreds digit.

        Dim digit As Integer

        Dim result As String = ""

        If num > 99 Then

            digit = num \ 100

            num = num Mod 100

            result = one_to_nineteen(digit) & " hundred"

        End If



        ' If num = 0, we have hundreds only.

        If num = 0 Then Return result.Trim()



        ' See if the rest is less than 20.

        If num < 20 Then

            ' Look up the correct name.

            result &= " " & one_to_nineteen(num)

        Else

            ' Handle the tens digit.

            digit = num \ 10

            num = num Mod 10

            result &= " " & multiples_of_ten(digit - 2)



            ' Handle the final digit.

            If num > 0 Then

                result &= " " & one_to_nineteen(num)

            End If

        End If
        Return result.Trim()

    End Function

    Private Sub btnCopyofLC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyofLC.Click

        fndLCRequestcode.Value = ""
        TxtLCAmount.Value = 0
        FndPurchaseOrderNo.Value = ""
        TxtSGSWaiverRefNo.Text = ""
        TxtMerchantDecrefNo.Text = ""
        FndVendor.Value = ""
        lblvendor.Text = ""
        TxtLocationCode.Text = ""
        lblLocationDesc.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtLCRequestdate.Value = clsCommon.GETSERVERDATE()
        fndLCRequestcode.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnsave.Enabled = True
        isNewEntry = True
        loadBlankItemGrid()
        isLoadInsideData = True
    End Sub
    Sub loadBlankLCIssuingGrid()

        'gvLCIssuing.Rows.Clear()
        'gvLCIssuing.Columns.Clear()
        gvLCIssuing.DataSource = Nothing
        gvLCIssuing.Rows.Clear()
        gvLCIssuing.Columns.Clear()

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Type"
        lineNo.Name = colType
        lineNo.Width = 50
        lineNo.ReadOnly = False
        lineNo.WrapText = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvLCIssuing.Columns.Add(lineNo)

        Dim strTag As New GridViewTextBoxColumn()
        strTag.FormatString = ""
        strTag.HeaderText = "Tag"
        strTag.Name = colTag
        strTag.Width = 60
        strTag.ReadOnly = False
        strTag.WrapText = True
        strTag.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvLCIssuing.Columns.Add(strTag)

        Dim strFieldName As New GridViewTextBoxColumn()
        strFieldName.FormatString = ""
        strFieldName.HeaderText = "Field Name"
        strFieldName.Name = colFieldName
        strFieldName.Width = 250
        strFieldName.ReadOnly = False
        strFieldName.WrapText = True
        strFieldName.AcceptsTab = False
        strFieldName.AcceptsReturn = True
        strFieldName.Multiline = True
        strFieldName.AcceptsReturn = True
        strFieldName.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gvLCIssuing.Columns.Add(strFieldName)


        Dim strDetails As New GridViewTextBoxColumn()
        strDetails.FormatString = ""
        strDetails.HeaderText = "Details"
        strDetails.Name = colDetails
        strDetails.Width = 700
        strDetails.ReadOnly = False
        strDetails.WrapText = True
        strDetails.AcceptsTab = False
        strDetails.AcceptsReturn = True
        strDetails.Multiline = True
        strDetails.AcceptsReturn = True
        strDetails.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        gvLCIssuing.Columns.Add(strDetails)



        ' gvLCIssuing.Rows(0).Cells(colSlNo).Value = "1"
        gvLCIssuing.AllowDeleteRow = False
        gvLCIssuing.AllowRowReorder = False
        gvLCIssuing.ShowGroupPanel = False
        gvLCIssuing.EnableFiltering = False
        gvLCIssuing.EnableSorting = False
        gvLCIssuing.EnableGrouping = False
        gvLCIssuing.AllowColumnReorder = True
        gvLCIssuing.AllowAddNewRow = False
        gvLCIssuing.ShowGroupPanel = False
        ' gvLCIssuing.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLCIssuing.MasterTemplate.ShowRowHeaderColumn = False
        gvLCIssuing.TableElement.TableHeaderHeight = 40


    End Sub

    Sub FillLCIssuing()
        Dim stradddocRequired As String = "1. All documents should be in English. " & Environment.NewLine & "2. All documents should mention our L/C number and date. " & Environment.NewLine & "3. Documents produced by reprographic process/ computerised carbon copies " & Environment.NewLine & " are not acceptable unless marked original and signed." & Environment.NewLine & _
           "4. All documents should mention applicants Importer Exporter Code no 0298023059 and HS Code No. 04021010" & Environment.NewLine & "5. All documents except invoice not showing full description of goods acceptable. " & Environment.NewLine & "6. All documents (including B/Ls issued prior to L/C issuance date are not acceptable)." & Environment.NewLine & "7. Insurance Policy/Certificate issued from web portal of the Insurance Company with Digital Signature acceptable."
        Dim strDocumentsRequired As String = "1. Beneficiary's Signed Invoice 1 original and 3 copies. " & Environment.NewLine & "2. Full set of 3/3 plus 3 non-negotiable copies clean on Board Bills of Lading made out " & Environment.NewLine & " to the order and blank endorsed marked freight prepaid and notify applicant. " & Environment.NewLine & "3. Packing List in 1 original 3 copies.  " & Environment.NewLine & "4. Marine/aviation insurance policy or certificate issued by insurance company, made to  " & Environment.NewLine & " order and blank endorsed in original and copy in negotiable form in the currency of  " & Environment.NewLine & " credit covering 110 percent of the invoice value, covering institute cargo clauses (a), with  " & Environment.NewLine & " extended cover for transshipment risks,if applicable, theft, pilferage, breakage and non-delivery,  " & Environment.NewLine & " institute war clauses (cargo) and institute strikes clauses (cargo), Voyage number and name, institute  " & Environment.NewLine & " transit clauses for warehouse to warehouse cover with claims payable in India irrespective of percentage.  "
        Dim strcompname As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select 'M/s '+Comp_Name as compname  from TSPL_COMPANY_MASTER "))
        Dim strcompaddress As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Add1 +', '+Add2  as address  from TSPL_COMPANY_MASTER "))
        '+', '+City_Code +' - '+Pincode

        Dim dt As DataTable = New DataTable
        dt.Columns.Add("colType", GetType(String))
        dt.Columns.Add("colTag", GetType(String))
        dt.Columns.Add("colFieldName", GetType(String))
        dt.Columns.Add("colDetails", GetType(String))

        dt.Rows.Add("M", "27", "Sequence of Total", "1")
        dt.Rows.Add("M", "40A", "Form of Documentary Credit", "")
        dt.Rows.Add("M", "20", "Documentary Credit Number", "")
        dt.Rows.Add("O", "23", "Reference to Pre-advice", "")
        dt.Rows.Add("O", "31C", "Date of Issue", clsCommon.myCstr(clsCommon.GetPrintDate(txtLCRequestdate.Value, "yy-MM-dd")))
        dt.Rows.Add("M", "40E", "Applicable Rules", "")
        dt.Rows.Add("M", "31D", "Date of Expiry/Place of Expiry", "")
        dt.Rows.Add("O", "51A", "Applicant Bank", "")
        dt.Rows.Add("M", "50", "Applicant", "" & strcompname & " " & Environment.NewLine & " " & strcompaddress & "")
        dt.Rows.Add("M", "59", "Beneficiary", "")
        dt.Rows.Add("M", "32B", "Currency Code, Amount", "")
        dt.Rows.Add("O", "39A", "Percentage Credit Amount Tolerance", "0")
        dt.Rows.Add("O", "39B", "Maximum Credit Amount", "")
        dt.Rows.Add("O", "39C", "Additional Amounts Covered", "")
        dt.Rows.Add("M", "41A", "Available With " & Environment.NewLine & " Available By", "")
        dt.Rows.Add("O", "42C", "Drafts at (Tenor)", "")
        dt.Rows.Add("O", "42A", "Drawee", "")
        dt.Rows.Add("O", "42M", "Mixed Payment Details", "")
        dt.Rows.Add("O", "42P", "Deferred Payment Details", "")
        dt.Rows.Add("O", "43P", "Partial Shipment", "")
        dt.Rows.Add("O", "43T", "Transshipment", "")
        dt.Rows.Add("O", "44A", "Place of taking in Charge/of Receipt", "")
        dt.Rows.Add("O", "44E", "Port of Loading/Airport of Departure", "")
        dt.Rows.Add("O", "44F", "Port of Discharge/Airport of Destination", "")
        dt.Rows.Add("O", "44B", "Place of Final Destination/of Delivery", "")
        dt.Rows.Add("O", "44C", "Latest Date for loading on board/dispatch/taking in charge", clsCommon.myCstr(clsCommon.GetPrintDate(txtLCRequestdate.Value.AddDays(45), "yy-MM-dd")))
        dt.Rows.Add("O", "44D", "Shipment Period", "")
        dt.Rows.Add("O", "45A", "Description of Goods and Quantity", "")
        dt.Rows.Add("O", "46A", "Documents Required", "" & strDocumentsRequired & "")
        dt.Rows.Add("O", "47A", "Additional Documents Required", "" & stradddocRequired & "")
        dt.Rows.Add("O", "71B", "Charges", "All bank charges inside India are for applicant account and all bank charges " & Environment.NewLine & "outside India are to the account of beneficiary.")
        dt.Rows.Add("O", "48", "Period of Presentation", "Document must be presented within 21 days after the date of delivery " & Environment.NewLine & "but within the validity of Credit.")
        dt.Rows.Add("M", "49", "Confirmation Instructions", "Without")
        dt.Rows.Add("O", "53A", "Reimbursing Bank", "")
        dt.Rows.Add("O", "78", "Instruction to the Negotiating/Accepting/Paying Bank", "")
        dt.Rows.Add("O", "57A", "Advice Through Bank", "")
        dt.Rows.Add("O", "72", "Sender to Receiver additional Information", "")
        gvLCIssuing.AutoGenerateColumns = False
        gvLCIssuing.DataSource = dt
        gvLCIssuing.Columns(colType).FieldName = "colType"
        gvLCIssuing.Columns(colTag).FieldName = "colTag"
        gvLCIssuing.Columns(colFieldName).FieldName = "colFieldName"
        gvLCIssuing.Columns(colDetails).FieldName = "colDetails"
        gvLCIssuing.AutoSizeRows = True
        'gvLCIssuing.Columns(colDetails).ReadOnly = False
        'gvLCIssuing.Columns(colFieldName).ReadOnly = True
        'gvLCIssuing.Columns(colTag).ReadOnly = True
        'gvLCIssuing.Columns(colType).ReadOnly = True
    End Sub

    Private Sub RDPLCIssuingApp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDPLCIssuingApp.Click

        Try
            If clsCommon.myLen(fndLCRequestcode.Value) > 0 Then
                funLCIssuingApplicationPrint()
            Else
                Throw New Exception("Please Select LC Request No first")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funLCIssuingApplicationPrint()
        Try

            Dim qry As String = "Select TSPL_LC_REQUEST_MT.DescriptionofGoods ,TSPL_PURCHASE_ORDER_HEAD.MT_HS_Classification_No ,TSPL_PURCHASE_ORDER_HEAD.MT_INCOTERMS ,TSPL_COMPANY_MASTER.IECode, TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Phone1 ,TSPL_COMPANY_MASTER.Fax ,TSPL_COMPANY_MASTER.Email ," & _
           " case when TSPL_COMPANY_MASTER.Add1<>'' then 'Address - '+TSPL_COMPANY_MASTER.Add1 else '' end + case when TSPL_COMPANY_MASTER.Add2<>'' then ', '+TSPL_COMPANY_MASTER.Add2 else '' end+ case when TSPL_COMPANY_MASTER.Add3<>'' then ',Address - '+TSPL_COMPANY_MASTER.Add3 else '' end+ case when TSPL_COMPANY_MASTER.City_Code <>'' then ', '+TSPL_COMPANY_MASTER.City_Code  else '' end  as CompanyAddress, " & _
           " TSPL_VENDOR_MASTER.Vendor_Name as BenefecieryName,TSPL_LC_REQUEST_MT.Merchant_Dec_Ref_no as ReferenceNo,Convert(varchar,TSPL_LC_REQUEST_MT.LCRequest_Date,103) as ReferenceDate,TSPL_REPORT_FORMAT_DECLARATION_MT.LC_Issuing_Application_Context  as ReferenceContext, TSPL_BANK_MASTER.DESCRIPTION as BankName,'' as BankSubject,TSPL_BANK_MASTER.ADD1 ,TSPL_BANK_MASTER.ADD2,TSPL_BANK_MASTER.ADD3 ,TSPL_BANK_MASTER.CITY ,TSPL_BANK_MASTER.POSTAL,case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' then TSPL_BANK_MASTER.ADD3 else '' END + CASE when isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' Then ', ' Else '' End) +TSPL_BANK_MASTER.CITY Else '' END + CASE when isnull(TSPL_BANK_MASTER.POSTAL,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')+isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then ' - ' Else '' End)+ TSPL_BANK_MASTER.POSTAL  Else '' END as [bankAddress], " & _
           " TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT.IssueType as TagType,TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT.IssueTag as Tag,TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT.IssueFieldname as FieldName,TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT.IssueDetails as Details, " & _
           " case when TSPL_VENDOR_MASTER.Add1<> '' then 'Address -'+ TSPL_VENDOR_MASTER.Add1 else '' end+ case when TSPL_VENDOR_MASTER.Add2<>'' then ', '+TSPL_VENDOR_MASTER.Add2 else '' end +case when TSPL_VENDOR_MASTER.City_Code_Desc<> '' then  TSPL_VENDOR_MASTER.City_Code_Desc else '' end as benefieceryAddress " & _
           " from TSPL_LC_REQUEST_MT Left outer Join TSPL_BANK_MASTER on TSPL_LC_REQUEST_MT.Bank_Code=TSPL_BANK_MASTER.BANK_CODE " & _
           " Left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_LC_REQUEST_MT.Comp_Code " & _
           " left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_LC_REQUEST_MT.PurchaseOrder_No " & _
           " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PURCHASE_ORDER_HEAD.MT_Beneficiary_Code " & _
           " Left Outer Join TSPL_REPORT_FORMAT_DECLARATION_MT on TSPL_REPORT_FORMAT_DECLARATION_MT.Comp_Code =TSPL_LC_REQUEST_MT .Comp_Code " & _
           " Left Outer Join TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT on TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT.LCRequestNo =TSPL_LC_REQUEST_MT.LCRequestNo  " & _
           " where 1=1 and  TSPL_LC_REQUEST_MT.LCRequestNo ='" + fndLCRequestcode.Value + "' order by TSPL_LC_ISSUINGAPPLICATION_DETAIL_MT.SNo "


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptLCIssuingApplicationMT", "LC Issuing Application")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnMDFormat1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMDFormat1.Click
        Try
            If clsCommon.myLen(fndLCRequestcode.Value) > 0 Then
                funMerchantDeclarationPrint()
            Else
                Throw New Exception("Please Select LC Request No first")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnMDFormat2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMDFormat2.Click
        Try
            If clsCommon.myLen(fndLCRequestcode.Value) > 0 Then
                funMerchantDeclarationFormat2Print()
            Else
                Throw New Exception("Please Select LC Request No first")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funMerchantDeclarationFormat2Print()
        Try

            Dim qry As String = "Select TSPL_COMPANY_MASTER.IECode,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_MASTER.Vendor_Name as BenefecieryName,TSPL_LC_REQUEST_MT.Merchant_Dec_Ref_no as ReferenceNo,Convert(varchar,TSPL_LC_REQUEST_MT.LCRequest_Date,103) as ReferenceDate,TSPL_REPORT_FORMAT_DECLARATION_MT.Merchant_Dec_Context_Format2 as ReferenceContext," & _
            " case when TSPL_COMPANY_MASTER.Add1<>'' then TSPL_COMPANY_MASTER.Add1 else '' end + case when TSPL_COMPANY_MASTER.Add2<>'' then ', '+TSPL_COMPANY_MASTER.Add2 else '' end+ case when TSPL_COMPANY_MASTER.Add3<>'' then ',Address - '+TSPL_COMPANY_MASTER.Add3 else '' end+ case when TSPL_COMPANY_MASTER.City_Code <>'' then ', '+TSPL_COMPANY_MASTER.City_Code  else '' end  as address," & _
            " TSPL_BANK_MASTER.DESCRIPTION as BankName,'' as BankSubject,TSPL_BANK_MASTER.ADD1 ,TSPL_BANK_MASTER.ADD2,TSPL_BANK_MASTER.ADD3 ,TSPL_BANK_MASTER.CITY ,TSPL_BANK_MASTER.POSTAL,case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' then TSPL_BANK_MASTER.ADD3 else '' END + CASE when isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')<>'' Then ', ' Else '' End) +TSPL_BANK_MASTER.CITY Else '' END + CASE when isnull(TSPL_BANK_MASTER.POSTAL,'')<>'' Then (case when  isnull(TSPL_BANK_MASTER.ADD3,'')+isnull(TSPL_BANK_MASTER.CITY,'')<>'' Then ' - ' Else '' End)+ TSPL_BANK_MASTER.POSTAL  Else '' END as [bankAddress] from TSPL_LC_REQUEST_MT Left outer Join TSPL_BANK_MASTER on TSPL_LC_REQUEST_MT.Bank_Code=TSPL_BANK_MASTER.BANK_CODE " & _
            " Left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_LC_REQUEST_MT.Comp_Code " & _
            " left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_LC_REQUEST_MT.PurchaseOrder_No" & _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PURCHASE_ORDER_HEAD.MT_Beneficiary_Code " & _
            " Left Outer Join TSPL_REPORT_FORMAT_DECLARATION_MT on TSPL_REPORT_FORMAT_DECLARATION_MT.Comp_Code =TSPL_LC_REQUEST_MT .Comp_Code " & _
            " where 1=1 and  TSPL_LC_REQUEST_MT.LCRequestNo ='" + fndLCRequestcode.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptMerchantDeclarationMTFormat2", "Merchant Declaration")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtFDPer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFDPer.TextChanged
        Try
            If clsCommon.myCdbl(TxtFDPer.Value) > 100 Then
                TxtFDPer.Value = 0
                Throw New Exception(" FD % cannot be more than 100 ")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RMDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gv1", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RMSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RMSaveLayout.Click
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
    Sub LoadMixedPD()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", " ")
        dt.Rows.Add("A", "Acceptance")
        dt.Rows.Add("N", "Negotiation")

        cboMixedPD.DataSource = dt
        cboMixedPD.ValueMember = "Code"
        cboMixedPD.DisplayMember = "Name"
    End Sub
    Sub LoadDeferredPD()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", " ")
        dt.Rows.Add("A", "Acceptance")
        dt.Rows.Add("N", "Negotiation")

        cboDeferredPD.DataSource = dt
        cboDeferredPD.ValueMember = "Code"
        cboDeferredPD.DisplayMember = "Name"
    End Sub
    Sub LoadAvailableBy()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", " ")
        dt.Rows.Add("A", "Acceptance")
        dt.Rows.Add("N", "Negotiation")

        cboAvailableBy.DataSource = dt
        cboAvailableBy.ValueMember = "Code"
        cboAvailableBy.DisplayMember = "Name"
    End Sub
    Sub LoadPartialShipment()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", " ")
        dt.Rows.Add("A", "Allowed")
        dt.Rows.Add("NA", "Not Allowed")

        cboPartialShipment.DataSource = dt
        cboPartialShipment.ValueMember = "Code"
        cboPartialShipment.DisplayMember = "Name"
    End Sub
    Sub LoadTransShipment()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", " ")
        dt.Rows.Add("A", "Allowed")
        dt.Rows.Add("NA", "Not Allowed")

        cboTransshipment.DataSource = dt
        cboTransshipment.ValueMember = "Code"
        cboTransshipment.DisplayMember = "Name"
    End Sub

    Sub LoadDraftsatTenorFrom()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("", " ")
        dt.Rows.Add("S", "Sight")
        dt.Rows.Add("SH", "Shipment")
        dt.Rows.Add("B", "BL")
        dt.Rows.Add("A", "Acceptance")
        dt.Rows.Add("N", "Negotiation")
        dt.Rows.Add("O", "Others")

        cboFrom.DataSource = dt
        cboFrom.ValueMember = "Code"
        cboFrom.DisplayMember = "Name"
    End Sub

    Private Sub txtLCRequestdate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtLCRequestdate.Validating
        If gvLCIssuing.Rows.Count > 0 Then
            gvLCIssuing.Rows(4).Cells(colDetails).Value = clsCommon.myCstr(clsCommon.GetPrintDate(txtLCRequestdate.Value, "yy-MM-dd"))
            If clsCommon.myLen(txtLCRequestdate.Value) > 0 Then
                gvLCIssuing.Rows(25).Cells(colDetails).Value = clsCommon.myCstr(clsCommon.GetPrintDate(txtLCRequestdate.Value.AddDays(21), "yy-MM-dd"))
            End If
        End If
    End Sub

    Private Sub cboMixedPD_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboMixedPD.SelectedValueChanged
        If gvLCIssuing.Rows.Count > 0 Then
            If clsCommon.CompairString(cboMixedPD.SelectedValue, "A") = CompairStringResult.Equal Then
                gvLCIssuing.Rows(17).Cells(colDetails).Value = "Acceptance"
            ElseIf clsCommon.CompairString(cboMixedPD.SelectedValue, "N") = CompairStringResult.Equal Then
                gvLCIssuing.Rows(17).Cells(colDetails).Value = "Negotiation"
            Else
                gvLCIssuing.Rows(17).Cells(colDetails).Value = ""
            End If
        End If
    End Sub

    Private Sub cboDeferredPD_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboDeferredPD.SelectedValueChanged
        If gvLCIssuing.Rows.Count > 0 Then
            If clsCommon.CompairString(cboDeferredPD.SelectedValue, "A") = CompairStringResult.Equal Then
                gvLCIssuing.Rows(18).Cells(colDetails).Value = "Acceptance"
            ElseIf clsCommon.CompairString(cboDeferredPD.SelectedValue, "N") = CompairStringResult.Equal Then
                gvLCIssuing.Rows(18).Cells(colDetails).Value = "Negotiation"
            Else
                gvLCIssuing.Rows(18).Cells(colDetails).Value = ""
            End If
        End If
    End Sub

    Private Sub cboPartialShipment_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboPartialShipment.SelectedValueChanged
        If gvLCIssuing.Rows.Count > 0 Then
            If clsCommon.CompairString(cboPartialShipment.SelectedValue, "A") = CompairStringResult.Equal Then
                gvLCIssuing.Rows(19).Cells(colDetails).Value = "Allowed"
            ElseIf clsCommon.CompairString(cboPartialShipment.SelectedValue, "NA") = CompairStringResult.Equal Then
                gvLCIssuing.Rows(19).Cells(colDetails).Value = "Not Allowed"
            Else
                gvLCIssuing.Rows(19).Cells(colDetails).Value = ""
            End If
        End If
    End Sub

    Private Sub cboTransshipment_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboTransshipment.SelectedValueChanged
        If gvLCIssuing.Rows.Count > 0 Then
            If clsCommon.CompairString(cboTransshipment.SelectedValue, "A") = CompairStringResult.Equal Then
                gvLCIssuing.Rows(20).Cells(colDetails).Value = "Allowed"
            ElseIf clsCommon.CompairString(cboTransshipment.SelectedValue, "NA") = CompairStringResult.Equal Then
                gvLCIssuing.Rows(20).Cells(colDetails).Value = "Not Allowed"
            Else
                gvLCIssuing.Rows(20).Cells(colDetails).Value = ""
            End If
        End If
    End Sub



    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        If e.Column Is gv1.Columns(colQty) Then
            funtofillDescriptionofGoods()
        End If
    End Sub
    Sub funtofillDescriptionofGoods()
        Dim strDescriptionofGoods As String = String.Empty
        strDescofGoodsforprint = ""
        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myCdbl(gv1.Rows(i).Cells(colQty).Value) > 0 Then
                If i > 0 Then
                    strDescriptionofGoods = strDescriptionofGoods + ", "
                    strDescofGoodsforprint = strDescofGoodsforprint + ", "
                End If
                strDescriptionofGoods = strDescriptionofGoods + clsCommon.myCstr(gv1.Rows(i).Cells(colItemDesc).Value) + " " + clsCommon.myCstr(gv1.Rows(i).Cells(colQty).Value) + clsCommon.myCstr(gv1.Rows(i).Cells(colUnitCode).Value) + " at " + clsCommon.myCstr(txtCurrencyCode.Value) + " " + clsCommon.myCstr(gv1.Rows(i).Cells(colRate).Value) + "/" + clsCommon.myCstr(gv1.Rows(i).Cells(colUnitCode).Value)
                strDescofGoodsforprint = strDescofGoodsforprint + clsCommon.myCstr(gv1.Rows(i).Cells(colItemDesc).Value) + " - " + clsCommon.myCstr(gv1.Rows(i).Cells(colQty).Value) + clsCommon.myCstr(gv1.Rows(i).Cells(colUnitCode).Value)
            End If
        Next
        If gvLCIssuing.Rows.Count > 0 Then
            gvLCIssuing.Rows(27).Cells(colDetails).Value = strDescriptionofGoods
            'If clsCommon.myLen(FndPurchaseOrderNo.Value) > 0 Then
            '    'strDescriptionofGoods = strDescriptionofGoods + " As per PI No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MT_PI_No  from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" & FndPurchaseOrderNo.Value & "'")) + " dated " + clsCommon.GetPrintDate(txtLCRequestdate.Value, "MMM/dd/yyyy")
            'End If
            If rdbAgainstPO.IsChecked Then
                strDescriptionofGoods = strDescriptionofGoods + " As per PO No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MT_Buyer_PO_No from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" & FndPurchaseOrderNo.Value & "'")) + " dated " + clsCommon.GetPrintDate(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(MT_Buyer_PO_Date,getdate())  from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" & FndPurchaseOrderNo.Value & "'")), "yy-MM-dd")
            Else
                strDescriptionofGoods = strDescriptionofGoods + " As per PI No " + clsCommon.myCstr(FndPurchaseInvoiceNo.Value) + " dated " + clsCommon.GetPrintDate(txtLCRequestdate.Value, "yy-MM-dd")
            End If

            gvLCIssuing.Rows(27).Cells(colDetails).Value = strDescriptionofGoods
        End If
    End Sub

    Private Sub cboAvailableBy_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboAvailableBy.SelectedValueChanged
        FillAvailableBy()
    End Sub
    Sub FillAvailableBy()
        If gvLCIssuing.Rows.Count > 0 Then
            Dim stravailableBy As String = String.Empty
            If clsCommon.CompairString(cboAvailableBy.SelectedValue, "A") = CompairStringResult.Equal Then
                stravailableBy = "Acceptance"
            ElseIf clsCommon.CompairString(cboAvailableBy.SelectedValue, "N") = CompairStringResult.Equal Then
                stravailableBy = "Negotiation"
            Else
                gvLCIssuing.Rows(20).Cells(colDetails).Value = ""
            End If
            stravailableBy = stravailableBy + "; " + clsCommon.myCstr(TxtDraweeBank.Text) + " (In  " + clsCommon.myCstr(TxtPlace.Text) + ")"
            gvLCIssuing.Rows(14).Cells(colDetails).Value = clsCommon.myCstr(TxtDraweeBank.Text) + " (In  " + clsCommon.myCstr(TxtPlace.Text) + ")" + Environment.NewLine + stravailableBy
        End If
    End Sub

    Private Sub TxtPlace_TextChanged(sender As Object, e As EventArgs) Handles TxtPlace.TextChanged
        FillAvailableBy()
    End Sub
    Sub CalculateDraftsatTenor()
        If gvLCIssuing.Rows.Count > 0 Then
            Dim strdraftsfrom As String = String.Empty
            If clsCommon.CompairString(cboFrom.SelectedValue, "S") = CompairStringResult.Equal Then
                strdraftsfrom = "Sight"
            ElseIf clsCommon.CompairString(cboFrom.SelectedValue, "SH") = CompairStringResult.Equal Then
                strdraftsfrom = "Shipment"
            ElseIf clsCommon.CompairString(cboFrom.SelectedValue, "B") = CompairStringResult.Equal Then
                strdraftsfrom = "BL"
            ElseIf clsCommon.CompairString(cboFrom.SelectedValue, "A") = CompairStringResult.Equal Then
                strdraftsfrom = "Acceptance"
            ElseIf clsCommon.CompairString(cboFrom.SelectedValue, "N") = CompairStringResult.Equal Then
                strdraftsfrom = "Negotiation"
            ElseIf clsCommon.CompairString(cboFrom.SelectedValue, "O") = CompairStringResult.Equal Then
                strdraftsfrom = "Others"
            Else
                strdraftsfrom = ""
            End If
            gvLCIssuing.Rows(15).Cells(colDetails).Value = clsCommon.myCstr(TxtNoofDays.Value) + " Days from the Date of " & strdraftsfrom & "" '+ Environment.NewLine + "2.From: " + strdraftsfrom + Environment.NewLine + clsCommon.myCstr(TxtRemarks.Text)
        End If
    End Sub


    Private Sub cboFrom_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboFrom.SelectedValueChanged
        If gvLCIssuing.Rows.Count > 0 Then
            If clsCommon.CompairString(cboFrom.SelectedValue, "S") = CompairStringResult.Equal Then
                TxtNoofDays.Value = 0
                TxtRemarks.Text = ""
                TxtNoofDays.Enabled = False
                TxtRemarks.Enabled = True
            Else
                TxtNoofDays.Value = 0
                TxtRemarks.Text = ""
                TxtNoofDays.Enabled = True
                TxtRemarks.Enabled = False
            End If
            CalculateDraftsatTenor()
        End If
    End Sub

    Private Sub TxtNoofDays_TextChanged(sender As Object, e As EventArgs) Handles TxtNoofDays.TextChanged
        CalculateDraftsatTenor()
    End Sub

    Private Sub TxtRemarks_TextChanged(sender As Object, e As EventArgs) Handles TxtRemarks.TextChanged
        CalculateDraftsatTenor()
    End Sub

    Private Sub gvLCIssuing_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gvLCIssuing.CellFormatting
        Try

            If e.Column Is gvLCIssuing.Columns(colDetails) Then
                If arrEditableRow.Contains(e.RowIndex) Then
                    gvLCIssuing.Rows(e.RowIndex).Cells(e.Column.Index).ReadOnly = False
                Else
                    gvLCIssuing.Rows(e.RowIndex).Cells(e.Column.Index).ReadOnly = True
                End If
            Else
                gvLCIssuing.Rows(e.RowIndex).Cells(e.Column.Index).ReadOnly = True
            End If
        Catch ex As Exception

        End Try
    End Sub
    Dim IsCellValueChanged As Boolean = False

    Private Sub gvLCIssuing_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvLCIssuing.CellValueChanged
        'If Not isLoadData Then
        '    If gvLCIssuing.Rows.Count > 0 Then
        '        If e.Column Is gvLCIssuing.Columns(colDetails) Then
        '            If gvLCIssuing.CurrentRow.Index = 11 Then

        '                If IsNumeric(gvLCIssuing.Rows(11).Cells(colDetails).Value) Then
        '                    gvLCIssuing.Rows(12).Cells(colDetails).Value = clsCommon.myCstr(txtCurrencyCode.Value) + "; " + clsCommon.myCstr(clsCommon.myCdbl(TxtLCAmount.Value) + clsCommon.myCdbl(gvLCIssuing.Rows(11).Cells(colDetails).Value))
        '                Else
        '                    gvLCIssuing.Rows(11).Cells(colDetails).Value = ""
        '                    clsCommon.MyMessageBoxShow("Please enter numbers only")
        '                End If
        '            End If
        '        End If
        '    End If
        'End If
        Try
            If Not isLoadData And IsCellValueChanged = False Then
                Dim strexpirydate As Date? = Nothing
                If gvLCIssuing.Rows.Count > 0 Then
                    If e.Column Is gvLCIssuing.Columns(colDetails) Then
                        If gvLCIssuing.CurrentRow.Index = 6 Then
                            If IsDate(gvLCIssuing.Rows(6).Cells(colDetails).Value) Then
                                IsCellValueChanged = True
                                gvLCIssuing.Rows(6).Cells(colDetails).Value = clsCommon.GetPrintDate(gvLCIssuing.Rows(6).Cells(colDetails).Value, "yy-MM-dd")
                                ' strexpirydate = clsCommon.GetPrintDate(gvLCIssuing.Rows(6).Cells(colDetails).Value, "yy-MM-dd")
                                '  gvLCIssuing.Rows(6).Cells(colDetails).Value = clsCommon.GetPrintDate(gvLCIssuing.Rows(6).Cells(colDetails).Value, "yy-MM-dd")
                                Dim ii As Integer = 0
                            Else
                                gvLCIssuing.Rows(6).Cells(colDetails).Value = ""
                                clsCommon.MyMessageBoxShow("Please enter valid date")
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            IsCellValueChanged = False
        End Try
    End Sub

    Private Sub cmbLCType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbLCType.SelectedValueChanged
        EnableDisableLCPeriod()
    End Sub
    Sub EnableDisableLCPeriod()
        If isLoadInsideData Then
            If clsCommon.CompairString(cmbLCType.SelectedValue, "U") = CompairStringResult.Equal Then
                cboLCPeriod.Enabled = True
                TxtLCPeriod.Enabled = True
                'cboLCPeriod.SelectedValue = ""
                'TxtLCPeriod.Value = 0
            Else
                cboLCPeriod.Enabled = False
                TxtLCPeriod.Enabled = False
                cboLCPeriod.SelectedValue = ""
                TxtLCPeriod.Value = 0
            End If
        End If
    End Sub

    ''richa agarwal 13/04/2015
    Private Sub rdbAgainstPO_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rdbAgainstPO.ToggleStateChanged
        If rdbAgainstPO.IsChecked Then
            FndPurchaseOrderNo.Enabled = True
            FndPurchaseInvoiceNo.Enabled = False
        Else
            FndPurchaseOrderNo.Enabled = False
            FndPurchaseInvoiceNo.Enabled = True

        End If
        FndPurchaseInvoiceNo.Value = ""
        FndPurchaseOrderNo.Value = ""
    End Sub

    Private Sub FndPurchaseInvoiceNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndPurchaseInvoiceNo._MYValidating
        If rdbAgainstPI.IsChecked Then
            Dim qry As String = "  Select Distinct AA.PurchaseInvoiceno,AA.[Unit Code] ,AA.[Payment Terms Group Code] from ( Select MAX(Code) as PurchaseInvoiceno,MAX([Vendor Code])as [Vendor Code],MAX([Vendor Name]) as [Vendor Name],MAX(Unit_code) as [Unit Code],MAX(MT_Payment_Terms_Group_Code) as [Payment Terms Group Code] ,SUM(Qty* case when RI=1 then 1 else 0 end) as POQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as LCQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty from (" & _
                                " Select TSPL_EX_PI_HEAD.Document_Code as Code,CONVERT(varchar,TSPL_EX_PI_HEAD.Document_Date ,103) as Date,'' as [Vendor Code],'' as [Vendor Name],TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code, TSPL_EX_PI_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_EX_PI_DETAIL.Unit_code , TSPL_EX_PI_DETAIL.Qty as Qty,0 as Unapproved,1 as RI  from TSPL_EX_PI_HEAD Left Outer Join TSPL_EX_PI_DETAIL on TSPL_EX_PI_DETAIL.DOCUMENT_CODE =TSPL_EX_PI_HEAD.Document_Code left outer join TSPL_PAYMENT_TERMS_GROUP_MASTER_MT on TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code = TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code left outer join TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT on TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code =TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code left outer join TSPL_PAYMENT_TERMS_MASTER_MT on TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code = TSPL_PAYMENT_TERMS_MASTER_MT.Code" & _
                                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_EX_PI_DETAIL.Item_Code " & _
                                " where len(isnull(TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code,''))>0  and len(isnull(TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code,''))>0 and TSPL_PAYMENT_TERMS_MASTER_MT.TermsType ='L' and  TSPL_EX_PI_HEAD.Status=1  and TSPL_EX_PI_HEAD.Document_Code not in(Select MT_PI_No from TSPL_PURCHASE_ORDER_HEAD where isnull(MT_PI_No,'')<>'')" & _
                                " Union All " & _
                                " Select ISNULL(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'')  as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code ,TSPL_LC_REQUEST_DETAIL_MT.Qty as Qty,0 as Unapproved ,-1 as RI  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=1 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0 " & _
                                " Union All" & _
                                " Select ISNULL(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code, 0 as Qty,TSPL_LC_REQUEST_DETAIL_MT.Qty as Unapproved ,-1 as RI  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=0 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0 and TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo not in ('" + fndLCRequestcode.Value + "') " & _
                                " )Final group by Code having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA "

            FndPurchaseInvoiceNo.Value = clsCommon.ShowSelectForm("formPurchaseInvoiceNo", qry, "PurchaseInvoiceno", "", FndPurchaseInvoiceNo.Value, "", isButtonClicked)
            If clsCommon.myLen(FndPurchaseInvoiceNo.Value) > 0 Then
                FndVendor.Enabled = True
                FillPurchaseInvoiceDetailintoGrid()
            Else
                FndVendor.Enabled = False
                TxtLocationCode.Text = ""
                lblLocationDesc.Text = ""
                TxtLCAmount.Value = 0
                loadBlankItemGrid()
            End If
            Dim dt As DataTable = Nothing
            'If clsCommon.myLen(FndVendor.Value) > 0 Then
            '    dt = clsDBFuncationality.GetDataTable("Select Vendor_Name ,Add1+', '+Add2 as Address from TSPL_VENDOR_MASTER where Vendor_Code ='" & FndVendor.Value & "'")
            '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '        gvLCIssuing.Rows(9).Cells(colDetails).Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Name")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("Address"))
            '    End If
            'End If
            'gvLCIssuing.Rows(10).Cells(colDetails).Value = clsCommon.myCstr(txtCurrencyCode.Value + "; " + clsCommon.myCstr(NumberToUSDString(TxtLCAmount.Value)))
            ' gvLCIssuing.Rows(10).Cells(colDetails).Value = clsCommon.myCstr(txtCurrencyCode.Value + "; " + txtCurrencyCode.Value + " " + clsCommon.myCstr(TxtLCAmount.Value) + " (USD " + clsCommon.myCstr(NumberToUSDString(TxtLCAmount.Value)) + ")")
            Dim dbldecimalpartofamount As Double = 0
            Dim strdecimalpartstring As String = String.Empty
            If TxtLCAmount.Value.ToString.Contains(".") Then
                dbldecimalpartofamount = TxtLCAmount.Value.ToString.Substring(TxtLCAmount.Value.ToString.IndexOf(".") + 1, TxtLCAmount.Value.ToString.Length - TxtLCAmount.Value.ToString.IndexOf(".") - 1)
                strdecimalpartstring = clsCommon.changeNumericToWords(dbldecimalpartofamount)
                gvLCIssuing.Rows(10).Cells(colDetails).Value = clsCommon.myCstr(txtCurrencyCode.Value + " " + clsCommon.myCstr(TxtLCAmount.Value) + " (USD " + clsCommon.myCstr(NumberToUSDString(TxtLCAmount.Value)) + " and " + strdecimalpartstring + "cents only)")
                'txtCurrencyCode.Value + "; " +
            Else
                gvLCIssuing.Rows(10).Cells(colDetails).Value = clsCommon.myCstr(txtCurrencyCode.Value + " " + clsCommon.myCstr(TxtLCAmount.Value) + " (USD " + clsCommon.myCstr(NumberToUSDString(TxtLCAmount.Value)) + " only)")
                'txtCurrencyCode.Value + "; " +
            End If
            'gvLCIssuing.Rows(10).Cells(colDetails).Value = clsCommon.myCstr(txtCurrencyCode.Value + "; " + txtCurrencyCode.Value + " " + clsCommon.myCstr(TxtLCAmount.Value) + " (USD " + clsCommon.myCstr(NumberToUSDString(TxtLCAmount.Value)) + " and " + strdecimalpartstring + " cents only)")
            ' dt = clsDBFuncationality.GetDataTable("SELECT Discharge_Port ,Final_Destination  FROM TSPL_EX_PI_HEAD where TSPL_EX_PI_HEAD.Document_Code= ( Select MT_PI_No  from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" & FndPurchaseOrderNo.Value & "')")
            dt = clsDBFuncationality.GetDataTable("Select Final_Destination,Discharge_Port from TSPL_EX_PI_HEAD where Document_Code =('" & FndPurchaseInvoiceNo.Value & "')")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvLCIssuing.Rows(23).Cells(colDetails).Value = clsCommon.myCstr(dt.Rows(0)("Discharge_Port"))
                gvLCIssuing.Rows(24).Cells(colDetails).Value = clsCommon.myCstr(dt.Rows(0)("Final_Destination"))
            End If
            dt = Nothing

        End If
    End Sub
    Sub FillPurchaseInvoiceDetailintoGrid()
        If clsCommon.myLen(FndPurchaseInvoiceNo.Value) > 0 Then
            loadBlankItemGrid()

            Dim qry As String = "  Select AA.Line_No, AA.Location ,AA.LocationDesc ,AA.PurchaseInvoiceno,AA.[Unit Code] ,AA.[Payment Terms Group Code],AA.PendingQty,AA.ItemCode ,AA.ItemName,AA.ConvRate,AA.CURRENCY_CODE,AA.Rate from ( Select Max(CURRENCY_CODE) as CURRENCY_CODE ,Max(ConvRate) as ConvRate, MAX(Line_No) as Line_No,MAX(Code) as PurchaseInvoiceno,MAX([Vendor Code])as [Vendor Code],MAX([Vendor Name]) as [Vendor Name],MAX(ICode) as ItemCode,MAX(IName) as ItemName,MAX(Unit_code) as [Unit Code],MAX(Bill_To_Location) as Location,MAX(Location_Desc) as LocationDesc,MAX(MT_Payment_Terms_Group_Code) as [Payment Terms Group Code] ,SUM(Qty* case when RI=1 then 1 else 0 end) as POQty,SUM(Qty* case when RI=-1 then 1 else 0 end) as LCQty,SUM((Qty *RI)- Unapproved) as PendingQty ,SUM(Unapproved) as UnapprovedQty,Sum(Rate) as Rate  from ( " & _
                                " Select TSPL_EX_PI_HEAD.CURRENCY_CODE ,TSPL_EX_PI_HEAD.ConvRate,TSPL_EX_PI_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_EX_PI_DETAIL.Line_No ,TSPL_EX_PI_HEAD.Document_Code as Code,CONVERT(varchar,TSPL_EX_PI_HEAD.Document_Date ,103) as Date,'' as [Vendor Code],'' as [Vendor Name],TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code, TSPL_EX_PI_DETAIL.Item_Code as ICode,TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_EX_PI_DETAIL.Unit_code , TSPL_EX_PI_DETAIL.Qty as Qty,0 as Unapproved,1 as RI,TSPL_EX_PI_DETAIL.Item_Cost as Rate  from TSPL_EX_PI_HEAD Left Outer Join TSPL_EX_PI_DETAIL on TSPL_EX_PI_DETAIL.DOCUMENT_CODE =TSPL_EX_PI_HEAD.Document_Code left outer join TSPL_PAYMENT_TERMS_GROUP_MASTER_MT on TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code = TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code left outer join TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT on TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code =TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Group_Code left outer join TSPL_PAYMENT_TERMS_MASTER_MT on TSPL_PAYMENT_TERMS_GROUP_DETAIL_MT.Terms_Code = TSPL_PAYMENT_TERMS_MASTER_MT.Code " & _
                                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_EX_PI_DETAIL.Item_Code " & _
                                " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EX_PI_HEAD.Bill_To_Location" & _
                                " where  len(isnull(TSPL_PAYMENT_TERMS_GROUP_MASTER_MT.Group_Code,''))>0  and len(isnull(TSPL_EX_PI_HEAD.MT_Payment_Terms_Group_Code,''))>0 and TSPL_PAYMENT_TERMS_MASTER_MT.TermsType ='L' and  TSPL_EX_PI_HEAD.Status=1  and TSPL_EX_PI_HEAD.Document_Code not in(Select MT_PI_No from TSPL_PURCHASE_ORDER_HEAD where isnull(MT_PI_No,'')<>'')" & _
                                " Union All " & _
                                " Select '' as CURRENCY_CODE ,0 as ConvRate,'' as Bill_To_Location,'' as Location_Desc,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,ISNULL(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code ,TSPL_LC_REQUEST_DETAIL_MT.Qty as Qty,0 as Unapproved ,-1 as RI,0 as Rate  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=1 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0 " & _
                                " Union All  " & _
                                " Select '' as CURRENCY_CODE ,0 as ConvRate,'' as Bill_To_Location,'' as Location_Desc,TSPL_LC_REQUEST_DETAIL_MT.Line_No ,ISNULL(TSPL_LC_REQUEST_MT.PurchaseInvoice_No,'') as Code,'' as Date,TSPL_LC_REQUEST_MT.VendorCode [Vendor Code] ,TSPL_VENDOR_MASTER.Vendor_Name  as [Vendor Name],'' as MT_Payment_Terms_Group_Code,TSPL_LC_REQUEST_DETAIL_MT.Item_Code as ICode,TSPL_LC_REQUEST_DETAIL_MT.Item_Desc as IName,TSPL_LC_REQUEST_DETAIL_MT.Unit_code,0 as Qty,TSPL_LC_REQUEST_DETAIL_MT.Qty as Unapproved,-1 as RI,0 as Rate  from TSPL_LC_REQUEST_DETAIL_MT left outer join TSPL_LC_REQUEST_MT on TSPL_LC_REQUEST_MT.LCRequestNo=TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo Left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_LC_REQUEST_MT.VendorCode where TSPL_LC_REQUEST_MT.Posted=0 and len(isnull(TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo ,''))>0 and TSPL_LC_REQUEST_DETAIL_MT.LCRequestNo not in ('" + fndLCRequestcode.Value + "') " & _
                                " )Final where Final.Code ='" & FndPurchaseInvoiceNo.Value & "' group by Code,ICode having (SUM(Qty *RI)-SUM(Unapproved)) >0) AA "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.Columns(colPONo).HeaderText = "Purchase Invoice No"
                gv1.Columns(colPOQty).HeaderText = "PI Qty"
                TxtLCAmount.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select case when MT_Is_AmountinRs=1 then MT_LC+MT_CAD+MT_RETAINED+MT_Balance_Payment +MT_On_Account +MT_Advance+MT_CIF else (Total_Amt * (MT_LC+MT_CAD+MT_RETAINED+MT_Balance_Payment +MT_On_Account +MT_Advance+MT_CIF)/100)  end as LCAMount from TSPL_EX_PI_HEAD where Document_Code ='" & FndPurchaseInvoiceNo.Value & "'"))
                TxtLocationCode.Text = clsCommon.myCstr(dt.Rows(0)("Location"))
                lblLocationDesc.Text = clsCommon.myCstr(dt.Rows(0)("LocationDesc"))
                txtCurrencyCode.Value = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
                txtConversionRate.Value = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))

                For Each dr As DataRow In dt.Rows
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSlNo).Value = clsCommon.myCstr(dr("Line_No"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPONo).Value = clsCommon.myCstr(dr("PurchaseInvoiceno"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("ItemCode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("ItemName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = clsCommon.myCstr(dr("Unit Code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPOQty).Value = clsCommon.myCdbl(dr("PendingQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows.AddNew()
                Next
                gv1.Rows.RemoveAt(gv1.Rows.Count - 1)
                funtofillDescriptionofGoods()
            End If
        End If
    End Sub

    Private Sub FndVendor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndVendor._MYValidating
        If rdbAgainstPI.IsChecked Then
            Dim qry As String = ""
            Dim whrCls As String = ""
            whrCls = " TSPL_VENDOR_MASTER.Status='N' and  TSPL_VENDOR_MASTER.CURRENCY_CODE<>(select isnull(BaseCurrencyCode,'')  from TSPL_COMPANY_MASTER where Comp_Code ='" & objCommonVar.CurrentCompanyCode & "' )"
            qry = "select Vendor_Code as Code,Vendor_Name as Name,TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as Address,Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER "
            FndVendor.Value = clsCommon.ShowSelectForm("POBenefieceryFndr", qry, "Code", whrCls, FndVendor.Value, "Code", isButtonClicked)
            qry = "select  Vendor_Code,Vendor_Name,Terms_Code,Terms_Code_Desc ,Vendor_Account ,Tax_Group,Tax_Group_Desc from TSPL_VENDOR_MASTER where Vendor_Code ='" + FndVendor.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                lblvendor.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            Else
                lblvendor.Text = ""
            End If
            If clsCommon.myLen(FndVendor.Value) > 0 Then
                dt = clsDBFuncationality.GetDataTable("Select Vendor_Name ,Add1+', '+Add2 as Address from TSPL_VENDOR_MASTER where Vendor_Code ='" & FndVendor.Value & "'")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvLCIssuing.Rows(9).Cells(colDetails).Value = clsCommon.myCstr(dt.Rows(0)("Vendor_Name")) & Environment.NewLine & clsCommon.myCstr(dt.Rows(0)("Address"))
                End If
            End If

            qry = Nothing
            whrCls = Nothing
        End If

    End Sub
End Class
