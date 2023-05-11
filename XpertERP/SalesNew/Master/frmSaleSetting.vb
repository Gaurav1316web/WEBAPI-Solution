'---------------------BM00000003305
Imports common
Imports System.Data.SqlClient
Imports common.Controls
Public Class FrmSaleSetting
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim formtype As String = Nothing
    Dim strSaleSettUserCode As String = ""

    ''richa 
#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
    ''==================
    Private Sub FrmSaleSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        SetControlsTag()
        LoadData()
        LoadItemTypeForDairyBooking()
    End Sub
    Sub LoadItemTypeForDairyBooking()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "B"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "Fresh"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Ambient"
        dt.Rows.Add(dr)

        cboItemType.DataSource = dt
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
    End Sub



    Sub SetControlsTag()
        chkBackLog.Tag1 = clsFixedParameterCode.AllowAutoNoForBackLogEntry
        chkBackLog.Tag = clsFixedParameterType.AllowAutoNoForBackLogEntry

        ChkFranchiseeDetails.Tag1 = clsFixedParameterCode.DisplayFranchiseeinCustomer
        ChkFranchiseeDetails.Tag = clsFixedParameterType.DisplayFranchiseeinCustomer

        chkdefaultvolumescheme.Tag1 = clsFixedParameterCode.IsVolumeSchemeBydefault
        chkdefaultvolumescheme.Tag = clsFixedParameterType.IsVolumeSchemeBydefault

        chkDispatchOutstandingBS.Tag1 = clsFixedParameterCode.AllowDispatchOutstandingBS
        chkDispatchOutstandingBS.Tag = clsFixedParameterType.AllowDispatchOutstandingBS

        chkDispatchOutstandingFS.Tag1 = clsFixedParameterCode.AllowDispatchOutstandingFS
        chkDispatchOutstandingFS.Tag = clsFixedParameterType.AllowDispatchOutstandingFS

        chkDispatchOutstandingPS.Tag1 = clsFixedParameterCode.AllowDispatchOutstandingPS
        chkDispatchOutstandingPS.Tag = clsFixedParameterType.AllowDispatchOutstandingPS

        'Richa Agarwal 19/08/2014 Against Ticket No BM00000003110
        chkCreateDeliveryorderincaseamountincrease.Tag1 = clsFixedParameterCode.AllowDeliveryOrderIncaseAmountIncreases
        chkCreateDeliveryorderincaseamountincrease.Tag = clsFixedParameterType.AllowDeliveryOrderIncaseAmountIncreases
        '-----------------------------------
        ''richa agarwal 09/02/2015
        ChkCreateAutoMRNGRNonDA.Tag1 = clsFixedParameterCode.AllowAutoMRNGRNonDocumentAcceptance
        ChkCreateAutoMRNGRNonDA.Tag = clsFixedParameterType.AllowAutoMRNGRNonDocumentAcceptance
        '================Preeti Gupta=============
        ChkCommitedDefaultQty.Tag1 = clsFixedParameterCode.CommitedDefaultQty
        ChkCommitedDefaultQty.Tag = clsFixedParameterType.CommitedDefaultQty


        ''----------------
        '' Anubhooti 12-Sep-2014 BM00000003890
        ChkAllowItemMRP.Tag1 = clsFixedParameterCode.AllowToEnterMRPManually
        ChkAllowItemMRP.Tag = clsFixedParameterType.AllowToEnterMRPManually

        chkPostShipment.Tag1 = clsFixedParameterCode.PostShipmentonAutoSTN
        chkPostShipment.Tag = clsFixedParameterType.PostShipmentonAutoSTN

        chkIsRemarksMandatoryoncloseofSaleOrder.Tag1 = clsFixedParameterCode.IsRemarksMandatoryOnCloseSaleOrder
        chkIsRemarksMandatoryoncloseofSaleOrder.Tag = clsFixedParameterType.IsRemarksMandatoryOnCloseSaleOrder

        chkCreateInvoice.Tag1 = clsFixedParameterCode.CreateInvoicewithShipmentonAutoSTN
        chkCreateInvoice.Tag = clsFixedParameterType.CreateInvoicewithShipmentonAutoSTN

        chkSIngleOrderSingleInvoice.Tag1 = clsFixedParameterCode.AllowSingleInvoiceAgainstSingleOrder
        chkSIngleOrderSingleInvoice.Tag = clsFixedParameterType.AllowSingleInvoiceAgainstSingleOrder

        chkItemRateditableontransfer.Tag1 = clsFixedParameterCode.IsItemRateEditableOnTransfer
        chkItemRateditableontransfer.Tag = clsFixedParameterType.IsItemRateEditableOnTransfer

        chkAutoscheme.Tag1 = clsFixedParameterCode.AutoSchemeOn
        chkAutoscheme.Tag = clsFixedParameterType.AutoSchemeOn

        chkTransferQtyEditonSTN.Tag1 = clsFixedParameterCode.IsTransferQtyEditableOnAutoSTN
        chkTransferQtyEditonSTN.Tag = clsFixedParameterType.IsTransferQtyEditableOnAutoSTN

        chkItemMRPEditableonSales.Tag1 = clsFixedParameterCode.IsItemMRPEditableOnSales
        chkItemMRPEditableonSales.Tag = clsFixedParameterType.IsItemMRPEditableOnSales

        chkItemRateEditableOnSales.Tag1 = clsFixedParameterCode.IsItemRateEditableOnSales
        chkItemRateEditableOnSales.Tag = clsFixedParameterType.IsItemRateEditableOnSales

        '-----------------richa 26/06/2014 Ticket No .BM00000002982---------
        ChkInvoiceManualNoWithPrefix.Tag1 = clsFixedParameterCode.InvoiceManualNoWithPrefix
        ChkInvoiceManualNoWithPrefix.Tag = clsFixedParameterType.InvoiceManualNoWithPrefix
        '-------------------------------------------------------------------

        '-------Anubhooti 28-Aug-2014 (Demo Setting For Sales Module)---------
        ChkShowStatusSale.Tag1 = clsFixedParameterCode.ShowStatusForSales
        ChkShowStatusSale.Tag = clsFixedParameterType.ShowStatusForSales

        '-------Anubhooti 28-Aug-2014 (Demo Setting For Sales Module)---------
        ChkSerialNo.Tag1 = clsFixedParameterCode.ShowSerialNoForSales
        ChkSerialNo.Tag = clsFixedParameterType.ShowSerialNoForSales
        '---------------------------------------------------------------------

        '-----------------richa 28/08/2014 Against Ticket No .BM00000003667---------
        ChkAdvanceAgainstSO.Tag1 = clsFixedParameterCode.AdvanceAgainstSO
        ChkAdvanceAgainstSO.Tag = clsFixedParameterType.AdvanceAgainstSO
        '-------------------------------------------------------------------
        chkNlevel_Customer.Tag1 = clsFixedParameterCode.NLevelAtCustomer
        chkNlevel_Customer.Tag = clsFixedParameterType.NLevelAtCustomer

        chksmsatpost.Tag1 = clsFixedParameterCode.Sale_SMSATPOST
        chksmsatpost.Tag = clsFixedParameterType.Sale_SMSATPOST

        chkAllowDeliveryQtyMorethanBooking.Tag1 = clsFixedParameterCode.AllowDeliveryQtygreaterthanBookingQtyPS
        chkAllowDeliveryQtyMorethanBooking.Tag = clsFixedParameterType.AllowDeliveryQtygreaterthanBookingQtyPS

        chkIsPickServerDateForMultipleDispatchInvoice.Tag1 = clsFixedParameterCode.IsPickServerDateForMultipleDispatchInvoice
        chkIsPickServerDateForMultipleDispatchInvoice.Tag = clsFixedParameterType.IsPickServerDateForMultipleDispatchInvoice

        chkDifferentseriesonPS.Tag1 = clsFixedParameterCode.AllowDiffentSeriesExemptedItemONPS
        chkDifferentseriesonPS.Tag = clsFixedParameterType.AllowDiffentSeriesExemptedItemONPS

        chkDairyDispatchfromDelivery.Tag1 = clsFixedParameterCode.SHowOptionOnLocationForDairyDispatchfromDOorGatepass
        chkDairyDispatchfromDelivery.Tag = clsFixedParameterType.SHowOptionOnLocationForDairyDispatchfromDOorGatepass

        chkPostForbulkSale.Tag1 = clsFixedParameterCode.showPostrequiredforBulkSale
        chkPostForbulkSale.Tag = clsFixedParameterType.showPostrequiredforBulkSale
    End Sub


#Region "Functions"

    Private Sub SetUserMgmtNew()
        ''----------------
        ''richa agarwal against ticket no BM00000004367
        If formtype = clsUserMgtCode.FrmSaleSetting Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleSetting)
            strSaleSettUserCode = clsUserMgtCode.FrmSaleSetting
        ElseIf formtype = clsUserMgtCode.FrmSaleSettingFresh Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleSettingFresh)
            strSaleSettUserCode = clsUserMgtCode.FrmSaleSettingFresh
        ElseIf formtype = clsUserMgtCode.FrmSaleSettingBulk Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleSettingBulk)
            strSaleSettUserCode = clsUserMgtCode.FrmSaleSettingBulk
        ElseIf formtype = clsUserMgtCode.FrmSaleSettingMerchant Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleSettingMerchant)
            strSaleSettUserCode = clsUserMgtCode.FrmSaleSettingMerchant
        ElseIf formtype = clsUserMgtCode.FrmSaleSettingExport Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleSettingExport)
            strSaleSettUserCode = clsUserMgtCode.FrmSaleSettingExport
        ElseIf formtype = clsUserMgtCode.FrmSaleSettingCSA Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleSettingCSA)
            strSaleSettUserCode = clsUserMgtCode.FrmSaleSettingCSA
        ElseIf formtype = clsUserMgtCode.FrmSaleSettingProduct Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleSettingProduct)
            strSaleSettUserCode = clsUserMgtCode.FrmSaleSettingProduct
        ElseIf formtype = clsUserMgtCode.FrmSaleSettingFreshDS Then
            'MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleSettingFreshDS)
            strSaleSettUserCode = clsUserMgtCode.FrmSaleSettingFreshDS
        End If


        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmSaleSetting)

        ''----------------------------------------------
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag

    End Sub

    Sub LoadData()
        Try

            For Each ctrl As Control In RadGroupBox1.Controls
                If ctrl.GetType Is GetType(MyCheckBox) Then
                    Dim chkBox As MyCheckBox = TryCast(ctrl, MyCheckBox)
                    If clsCommon.myLen(chkBox.Tag) >= 0 AndAlso clsCommon.myLen(chkBox.Tag1) >= 0 Then
                        chkBox.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(chkBox.Tag, chkBox.Tag1, Nothing)) = 1, True, False)
                    End If
                End If
            Next
            TxtAmountLimitForInvoiceBulkSale.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + "' and code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'"))
            '-------Updated By Preeti Gupta---
            txtCrateValue.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.CrateValue + "' and code='" + clsFixedParameterCode.CrateValue + "'"))
            '-----------End-------------------


            TxtCorrectionFactor.Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.DefaultCorrectionFactorForBulkSale + "' and code='" + clsFixedParameterCode.DefaultCorrectionFactorForBulkSale + "'"))
            FndItemCode.Value = clsFixedParameter.GetData(clsFixedParameterType.BulkSaleDefaultMilkItem, clsFixedParameterCode.BulkSaleDefaultMilkItem, Nothing)
            lblItemDesc.Text = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_Master where Item_code='" & FndItemCode.Value & "'")

            FndItemCodeforBulk.Value = clsFixedParameter.GetData(clsFixedParameterType.BSDefaultMilkItem, clsFixedParameterCode.BSDefaultMilkItem, Nothing)
            lblItemDescBulk.Text = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_Master where Item_code='" & FndItemCodeforBulk.Value & "'")

            fndDefaultRoundoffAccount.Value = clsFixedParameter.GetData(clsFixedParameterType.DefaultRoundOffGLAccount, clsFixedParameterCode.DefaultRoundOffGLAccount, Nothing)
            LblDefaultRoundoffglaccount.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS where Account_Code  ='" & fndDefaultRoundoffAccount.Value & "'")

            fndDiscCodeARAdj.Value = clsFixedParameter.GetData(clsFixedParameterType.DiscountCodeForArAdj, clsFixedParameterCode.DiscountCodeForArAdj, Nothing)
            lblDiscDescARAdj.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Description from TSPL_Discount_Master WHERE Code='" + fndDiscCodeARAdj.Value + "'"))

            FndItemCode.Value = clsFixedParameter.GetData(clsFixedParameterType.ItemTypeForDairyBooking, clsFixedParameterCode.ItemTypeForDairyBooking, Nothing)

            ''=============Parteek added 16/01/2017
            txtCashCustomerAc.Value = clsFixedParameter.GetData(clsFixedParameterType.AutoRecieptBankCode, clsFixedParameterCode.AutoRecieptBankCode, Nothing)
            lblBankCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  BANK_CODE from TSPL_BANK_MASTER WHERE BANK_CODE='" + txtCashCustomerAc.Value + "'"))

            txtpaymentMode.Value = clsFixedParameter.GetData(clsFixedParameterType.AutoRecieptPaymentMode, clsFixedParameterCode.AutoRecieptPaymentMode, Nothing)
            'done by stuti on 17/01/2017
            fnditemuom.Value = clsFixedParameter.GetData(clsFixedParameterType.DefaultItemUOMForBulkSale, clsFixedParameterCode.DefaultItemUOMForBulkSale, Nothing)
            MyLabel7.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Unit_Desc from tspl_unit_master WHERE unit_code='" + fnditemuom.Value + "'"))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub FndItemCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndItemCode._MYValidating
        Try
            Dim Qry As String = ""
            Qry = "Select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER "
            FndItemCode.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", " Product_Type ='MI' and Active=1", FndItemCode.Value, "", isButtonClicked)
            lblItemDesc.Text = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + FndItemCode.Value + "' ")

        Catch ex As Exception
        End Try
    End Sub
    Private Sub FndItemCodeforBulk__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles FndItemCodeforBulk._MYValidating
        Try
            Dim Qry As String = ""
            Qry = "Select Item_Code as Code,Item_Desc as Description from TSPL_ITEM_MASTER "
            FndItemCodeforBulk.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", " Product_Type ='MI' and Active=1", FndItemCodeforBulk.Value, "", isButtonClicked)
            lblItemDescBulk.Text = clsDBFuncationality.getSingleValue("Select Item_Desc from TSPL_ITEM_MASTER where Item_Code ='" + FndItemCodeforBulk.Value + "' ")

        Catch ex As Exception
        End Try
    End Sub
    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(strSaleSettUserCode, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            For Each ctrl As Control In RadGroupBox1.Controls
                If ctrl.GetType Is GetType(MyCheckBox) Then
                    Dim chkBox As MyCheckBox = TryCast(ctrl, MyCheckBox)
                    If clsCommon.myLen(chkBox.Tag) >= 0 AndAlso clsCommon.myLen(chkBox.Tag1) >= 0 Then
                        clsFixedParameter.UpdateData(chkBox.Tag, chkBox.Tag1, IIf(chkBox.Checked, "1", "0"), trans)
                    End If
                End If
            Next

            If chkNlevel_Customer.Checked Then
                MDI.IsCustomer_NLevel = "YES"
            ElseIf Not chkNlevel_Customer.Checked Then
                MDI.IsCustomer_NLevel = "NO"
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + clsCommon.myCstr(TxtAmountLimitForInvoiceBulkSale.Value) + "' where TYPE='" + clsFixedParameterType.AmountLimitForInvoiceBulkSale + " ' and Code='" + clsFixedParameterCode.AmountLimitForInvoiceBulkSale + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + clsCommon.myCstr(TxtCorrectionFactor.Value) + "' where TYPE='" + clsFixedParameterType.DefaultCorrectionFactorForBulkSale + " ' and Code='" + clsFixedParameterCode.DefaultCorrectionFactorForBulkSale + "'", trans)
            'If fndItemCode.Value <> "" Then
            clsDBFuncationality.ExecuteNonQuery("update TSPL_FIXED_PARAMETER set Description='" & FndItemCode.Value & "' where Type='" & clsFixedParameterType.BulkSaleDefaultMilkItem & " ' and Code='" & clsFixedParameterCode.BulkSaleDefaultMilkItem & "'", trans)
            'nd If
            clsDBFuncationality.ExecuteNonQuery("update TSPL_FIXED_PARAMETER set Description='" & FndItemCodeforBulk.Value & "' where Type='" & clsFixedParameterType.BSDefaultMilkItem & " ' and Code='" & clsFixedParameterCode.BSDefaultMilkItem & "'", trans)

            clsDBFuncationality.ExecuteNonQuery("update TSPL_FIXED_PARAMETER set Description='" & fndDefaultRoundoffAccount.Value & "' where Type='" & clsFixedParameterType.DefaultRoundOffGLAccount & " ' and Code='" & clsFixedParameterCode.DefaultRoundOffGLAccount & "'", trans)
            clsDBFuncationality.ExecuteNonQuery("update TSPL_FIXED_PARAMETER set Description='" & fndDiscCodeARAdj.Value & "' where Type='" & clsFixedParameterType.DiscountCodeForArAdj & " ' and Code='" & clsFixedParameterCode.DiscountCodeForArAdj & "'", trans)

            '---Updated by Preeti gupta---------
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + clsCommon.myCstr(txtCrateValue.Value) + "' where TYPE='" + clsFixedParameterType.CrateValue + " ' and Code='" + clsFixedParameterCode.CrateValue + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Update TSPL_FIXED_PARAMETER Set description='" + clsCommon.myCstr(cboItemType.SelectedValue) + "' where TYPE='" + clsFixedParameterType.ItemTypeForDairyBooking + " ' and Code='" + clsFixedParameterCode.ItemTypeForDairyBooking + "'", trans)

            '-------------End-------------------
            ''======= added by parteek 16-01-2017
            clsDBFuncationality.ExecuteNonQuery("update TSPL_FIXED_PARAMETER set Description='" & txtCashCustomerAc.Value & "' where Type='" & clsFixedParameterType.AutoRecieptBankCode & " ' and Code='" & clsFixedParameterCode.AutoRecieptBankCode & "'", trans)
            clsDBFuncationality.ExecuteNonQuery("update TSPL_FIXED_PARAMETER set Description='" & txtpaymentMode.Value & "' where Type='" & clsFixedParameterType.AutoRecieptPaymentMode & " ' and Code='" & clsFixedParameterCode.AutoRecieptPaymentMode & "'", trans)

            ''========End
            ''added by stuti on 17/01/2017
            clsDBFuncationality.ExecuteNonQuery("update TSPL_FIXED_PARAMETER set Description='" & fnditemuom.Value & "' where Type='" & clsFixedParameterType.DefaultItemUOMForBulkSale & "' and Code='" & clsFixedParameterCode.DefaultItemUOMForBulkSale & "'", trans)
            ''======end here===============
            trans.Commit()
            clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

#End Region

#Region "EVENTS"
    Private Sub frmPurchaseSettings_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
#End Region

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub

    Private Sub fndDefaultRoundofAccount__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDefaultRoundoffAccount._MYValidating
        Try
            Dim Qry As String = ""
            Qry = "Select Account_Code as Code ,Description from TSPL_GL_ACCOUNTS  "
            fndDefaultRoundoffAccount.Value = clsCommon.ShowSelectForm("GLAccount", Qry, "Code", " ControlAccount ='Y'", fndDefaultRoundoffAccount.Value, "", isButtonClicked)
            LblDefaultRoundoffglaccount.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS where Account_Code ='" + fndDefaultRoundoffAccount.Value + "' ")

        Catch ex As Exception
        End Try
    End Sub

    Private Sub fndDiscCodeARAdj__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDiscCodeARAdj._MYValidating
        Dim qry As String = "Select Code, Description, Account_Code, Account_Description from TSPL_Discount_Master "
        Dim Whr As String = ""
        fndDiscCodeARAdj.Value = clsCommon.ShowSelectForm("frmAdjDSCTFND", qry, "Code", Whr, fndDiscCodeARAdj.Value, "Code", isButtonClicked)
        lblDiscDescARAdj.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  Description from TSPL_Discount_Master WHERE Code='" + fndDiscCodeARAdj.Value + "'"))
    End Sub
    Private Sub txtCashCustomerAc__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCashCustomerAc._MYValidating
        Dim qry As String = "select BANK_CODE as [Code], DESCRIPTION,BANKACCNUMBER as [Bank Account No]  from TSPL_BANK_MASTER "
        Dim Whr As String = ""
        txtCashCustomerAc.Value = clsCommon.ShowSelectForm("frmbankCode", qry, "Code", Whr, txtCashCustomerAc.Value, "Code", isButtonClicked)
        lblBankCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select  BANK_CODE from TSPL_BANK_MASTER WHERE BANK_CODE='" + txtCashCustomerAc.Value + "'"))
        txtpaymentMode.Value = connectSql.RunScalar("select TSPL_PAYMENT_CODE.Payment_Code   from TSPL_PAYMENT_CODE Where TSPL_PAYMENT_CODE.Payment_Code=  (select DISTINCT (case when Bank_type = 'C' THEN 'CASH' WHEN BANK_TYPE = 'B' THEN 'CHEQUE' WHEN BANK_TYPE = 'O' THEN 'OTHER' WHEN Bank_type = 'P' THEN 'PETTYCASH' else 'CASH' END ) AS [Paymet Type] from TSPL_BANK_MASTER Where BANK_CODE='" + txtCashCustomerAc.Value + "' )")
    End Sub
    Private Sub txtpaymentMode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtpaymentMode._MYValidating
        Dim strbankcode As String
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtCashCustomerAc.Value + "'")) Then
            strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtCashCustomerAc.Value + "'")
            If strbankcode.Trim() = "C" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtpaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode_Selector1", Qry1, "PaymentMode", "PAYMENT_TYPE = 'CASH'", txtpaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode.Trim() = "P" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtpaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode_Selector2", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Petty Cash'", txtpaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode = "B" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtpaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode_Selector3", Qry1, "PaymentMode", "PAYMENT_TYPE IN ('Cheque', 'Other','NEFT','RTGS')", txtpaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode = "S" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtpaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode_Selector4", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Cheque' or PAYMENT_TYPE = 'Cash'", txtpaymentMode.Value, "PaymentMode", isButtonClicked)
            Else
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtpaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode_Selector5", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Other'", txtpaymentMode.Value, "PaymentMode", isButtonClicked)
            End If
        End If
    End Sub

    Private Sub fnditemuom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fnditemuom._MYValidating
        Dim Qry1 As String = "select Unit_Code as [Code],Unit_Desc as [Description] from tspl_unit_master"
        fnditemuom.Value = clsCommon.ShowSelectForm("fnduom", Qry1, "Code", "", fnditemuom.Value, "Code", isButtonClicked)
        MyLabel7.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Unit_Desc from tspl_unit_master WHERE unit_code='" + fnditemuom.Value + "'"))
    End Sub
End Class
