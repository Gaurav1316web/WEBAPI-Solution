'Developed By -  Ajit Singh
'Start Date - 28/4/2011
'Last Modify Date - 03/06/2011
'Table Used - TSPL_TAX_MASTER
'BM00000004018

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExportToExcelML
Imports System.Text
Imports System.Windows.Forms
Imports common

Public Class frmTaxAuthority
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Dim AllowGSTApplicable As Boolean = False
    Dim AllowGSTApplicableDate As Boolean = False
    Dim ActiveTaxes_Rates_GroupsforGST As Boolean = False
    Dim arrlist As New ArrayList()
    Dim whrcls As String = ""




    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Public Sub SetLength()
        findTaxAuthority.MyMaxLength = 12
        txtDesc.MaxLength = 50
        txtTaxRelAccDesc.MaxLength = 50

    End Sub


    Private Sub SetUserMgmtNew()
        '' Anubhooti 30-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.taxAuthority)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnAdd.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnAdd.Visible = True Then
            menuImport.Enabled = True
            menuExport.Enabled = True
        Else
            menuImport.Enabled = False
            menuExport.Enabled = False
        End If
        '--------------------------------------------------
        '         btnclose.Visible = MyBase.isDeleteFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub



    Private Sub frmTaxAuthority_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        'globalFunc.mandatoryText(findTaxAuthority.Value, txtDesc, findTaxRelAcc.Value, txtTaxRelAccDesc)
        findTaxAuthority.MyMaxLength = 12

        ButtonToolTip.SetToolTip(btnAdd, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        'fndnetpay.MyMaxLength = 12
        'findTaxRelAcc.MyMaxLength = 50
        AddHandler findTaxAuthority.TextChanged, AddressOf findTaxAuthority_TextChanged
        AddHandler findTaxAuthority.KeyPress, AddressOf keyPress1
        AddHandler findTaxRelAcc.TextChanged, AddressOf findTaxRelAcc_TextChanged
        AddHandler findTaxRelAcc.KeyPress, AddressOf keyPress1
        'AddHandler findRecoverTaxAcc.TextChanged, AddressOf findRecoverTaxAcc_TextChanged
        AddHandler findRecoverTaxAcc.KeyPress, AddressOf keyPress1
        AddHandler fndnetpay.TextChanged, AddressOf fndnetpay_textchange
        AddHandler fndnetpay.Leave, AddressOf fndnetpay_leave
        AddHandler fndnetpay.KeyPress, AddressOf fndnetpay_press
        AddHandler txtDesc.KeyPress, AddressOf keyPress1
        AddHandler txtRecoverRate.KeyPress, AddressOf NumericKeyPress

        btnAdd.Enabled = False
        btnDelete.Enabled = False
        findRecoverTaxAcc.Enabled = False
        txtRecoverRate.Enabled = False
        fndRecovTaxAcc2.Enabled = False
        txtRecovRate2.Enabled = False
        fndRecovTaxAcc3.Enabled = False
        txtRecovRate3.Enabled = False
        fndRecovTaxAcc4.Enabled = False
        txtRecovRate4.Enabled = False
        fndRecovTaxAcc5.Enabled = False
        txtRecovRate5.Enabled = False
        txtRecoverTaxAccDesc.Enabled = False
        txtRecovTaxAccDesc2.Enabled = False
        txtRecovTaxAccDesc3.Enabled = False
        txtRecovTaxAccDesc4.Enabled = False
        txtRecovTaxAccDesc5.Enabled = False
        chkmanditax.Checked = False
        RadPanel1.Visible = False
        LoadCSAType()
        chkExcisable.Enabled = False
        '' MultiCurrency
        SetMultiCurrencyVisibility()

        Me.txtBaseCurrency.Value = objCommonVar.BaseCurrencyCode
        '' End of MultiCurrency
        reset()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        AllowGSTApplicable = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GSTApplicable, clsFixedParameterCode.GSTApplicable, Nothing)) = 1, True, False)

        ActiveTaxes_Rates_GroupsforGST = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GSTActiveTaxesRatesGroup, clsFixedParameterCode.GSTActiveTaxesRatesGroup, Nothing)) = 1, True, False)
        LoadType()
        If AllowGSTApplicable Then
            RadGroupBox2.Enabled = True
            ChkGSTActive.Enabled = True
            fndpayable.Enabled = True
            txtDepositControl.Enabled = True
        End If
    End Sub
    
    Private Sub LoadCSAType()
        Dim qry As String = "select 'CPD-DESI GHEE' as Name union all select 'BULK-DESI GHEE' as Name union all select 'CPD-OTHER' as Name union all select 'BULK-OTHER' as Name"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgCSA.DataSource = dt
        cbgCSA.DisplayMember = "Name"
        cbgCSA.ValueMember = "Name"

        cbgCSA.UnCheckedAll()
    End Sub

    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
            pnlCurrConv.Visible = True

            'If clsCommon.myLen(Me.fndCustomer.Value) > 0 Then
            '    strq = "select currency_code from TSPL_CUSTOMER_MASTER where CUST_CODE='" & clsCommon.myCstr(Me.fndCustomer.Value) & "'"
            '    Me.txtCurrencyCode.Value = clsDBFuncationality.getSingleValue(strq).ToString
            '    ShowCurrencyDetail()
            'End If
            ShowCurrencyDetail()
        Else
            RadGroupBox1.Height = RadGroupBox1.Height - pnlCurrConv.Height
            pnlCurrConv.Height = 0
            pnlCurrConv.Visible = False
        End If

    End Sub
    Sub ShowCurrencyDetail()

        Dim dt As DataTable
        If clsCommon.myLen(clsCommon.myCstr(Me.txtCurrencyCode.Value)) = 0 Then
            Me.txtCurrencyCode.Value = objCommonVar.BaseCurrencyCode
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
            Exit Sub
        End If

        If clsCommon.myLen(txtCurrencyCode.Value) > 0 Then
            dt = clsModuleCurrencyMapping.GetLatestCurConvRateDT(clsCommon.GETSERVERDATE, txtCurrencyCode.Value)
            If dt.Rows.Count = 0 Then
                If Me.txtCurrencyCode.Value = objCommonVar.BaseCurrencyCode Then
                    Me.txtConversionRate.Text = 1
                    Me.txtApplicableFrom.Text = ""
                Else
                    clsCommon.MyMessageBoxShow("Conversion rate not entered for currency '" & Me.txtCurrencyCode.Value & "'")
                    Exit Sub
                End If
            Else
                Me.txtConversionRate.Text = dt.Rows(0).Item("Rate")
                Me.txtApplicableFrom.Text = clsCommon.GetPrintDate(dt.Rows(0).Item("FROM_DATE"), "dd/MMM/yyyy")
            End If
        Else
            'Me.txtCurrencyCode.Value = objCommonVar.BaseCurrencyCode.ToString
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""
        End If

    End Sub
    Public Sub fndnetpay_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    Public Sub fndnetpay_textchange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndnetpay.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                strvalue = dr.Rows(0)(0).ToString()
            End If
            If strvalue <> "" Then

                funfillnetpay()
            Else
                txtnetpay.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub funfillnetpay()
        Try

            Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndnetpay.Value + "'"
            Dim dr As DataTable
            Dim strvalue As String = ""
            dr = clsDBFuncationality.GetDataTable(strquery)
            If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

                txtnetpay.Text = dr.Rows(0)(1).ToString()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub fndnetpay_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndnetpay.Value = "" Then
        Else
            Try
                Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndnetpay.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then

                    strvalue = dr.Rows(0)(0).ToString()
                End If
                If strvalue <> "" Then
                Else : strquery = ""
                    txtnetpay.Text = ""
                    common.clsCommon.MyMessageBoxShow("This Account does not exist in Master Table")
                    fndnetpay.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If

    End Sub

#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As DataTable

#End Region

#Region "events"

    Private Sub keyPress1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        SaveData()
    End Sub
    Public Sub SaveData()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.taxAuthority, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim trans As SqlTransaction
        Try


            btnAdd.Focus()

            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
                If clsCommon.myLen(findTaxAuthority.Value) <= 0 Then
                    findTaxAuthority.Focus()
                    Throw New Exception("Tax Authority Can not be left Blank")
                End If
            End If

            'If clsCommon.myLen(findTaxAuthority.Value) <= 0 Then
            '    findTaxAuthority.Focus()
            '    Throw New Exception("Tax Authority Can not be left Blank")
            'End If

            If chkmanditax.Checked AndAlso cbgCSA.CheckedValue.Count <= 0 Then
                chkmanditax.Focus()
                chkmanditax.Select()
                Throw New Exception("Select CSA Item Type For Mandi Tax")
            End If


            If validateRecords() Then
                trans = clsDBFuncationality.GetTransactin()
                Dim taxRecoverable As String = "N"
                Dim excisable As String = "N"
                Dim taxtype As String
                If chkTaxRecover.Checked Then
                    taxRecoverable = "Y"
                End If

                taxtype = drpTaxtype.Text
                'Dim type As String
                If taxtype = "VAT" Then
                    taxtype = "V"
                ElseIf taxtype = "CST" Then
                    taxtype = "C"
                ElseIf taxtype = "EXCISE" Then
                    taxtype = "E"
                    chkExcisable.Checked = True
                    excisable = "Y"
                ElseIf taxtype = "ADDTAX" Then
                    taxtype = "A"
                ElseIf taxtype = "OTHER" Then
                    taxtype = "O"
                ElseIf taxtype = "SERVICE" Then
                    taxtype = "S"
                ElseIf taxtype = "MANDI TAX" Then
                    taxtype = "M"
                ElseIf taxtype = "WCT" Then
                    taxtype = "W"
                ElseIf taxtype = "SGST" Then
                    taxtype = "SGST"
                ElseIf taxtype = "IGST" Then
                    taxtype = "IGST"
                ElseIf taxtype = "CGST" Then
                    taxtype = "CGST"
                ElseIf taxtype = "UGST" Then
                    taxtype = "UGST"
                End If
                If chkExcisable.Checked Then
                    excisable = "Y"
                End If
                If btnAdd.Text = "Save" Then
                    Try
                        If chkTCS.Checked = True Then
                            Dim tcscount As Integer = clsDBFuncationality.getSingleValue("SELECT count(Is_TCS) from tspl_tax_master where Tax_Code<>'" & findTaxAuthority.Value & "' and Is_TCS='Y'", trans)
                            If tcscount > 0 Then
                                Throw New Exception("Tax of TCS type already exist")
                            End If
                        End If

                        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                            Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_TAX_MASTER where Tax_Code='" & findTaxAuthority.Value & "'", trans)
                            If ChkNewEntry = 0 Then
                                findTaxAuthority.Value = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.TaxAuthority, "", "")
                                If clsCommon.myLen(findTaxAuthority.Value) <= 0 Then
                                    Throw New Exception("Error in Tax Code Generation")
                                End If
                            End If
                        End If
                        connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_MASTER_INSERT", New SqlParameter("@Tax_Code", findTaxAuthority.Value), New SqlParameter("@Tax_Code_Desc", txtDesc.Text), New SqlParameter("@Tax_Liability_Account", findTaxRelAcc.Value), New SqlParameter("@Tax_Recoverable", taxRecoverable), New SqlParameter("@Tax_Recover_Account", findRecoverTaxAcc.Value), New SqlParameter("@Tax_Recover_Rate", clsCommon.myCdbl(txtRecoverRate.Value)), New SqlParameter("@Tax_Net_Payable", fndnetpay.Value), New SqlParameter("@Excisable", excisable), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Type", taxtype), New SqlParameter("@taxRecoverableAcc2", fndRecovTaxAcc2.Value), New SqlParameter("@TaxRecoverRate2", clsCommon.myCdbl(txtRecovRate2.Value)), New SqlParameter("@taxRecoverableAcc3", fndRecovTaxAcc3.Value), New SqlParameter("@TaxRecoverRate3", clsCommon.myCdbl(txtRecovRate3.Value)), New SqlParameter("@taxRecoverableAcc4", fndRecovTaxAcc4.Value), New SqlParameter("@TaxRecoverRate4", clsCommon.myCdbl(txtRecovRate4.Value)), New SqlParameter("@taxRecoverableAcc5", fndRecovTaxAcc5.Value), New SqlParameter("@TaxRecoverRate5", clsCommon.myCdbl(txtRecovRate5.Value)))

                        ''---------GST Related Work done
                        If AllowGSTApplicable = True Then
                            Dim gstactive As Integer = 0
                            If ChkGSTActive.Checked = True Then
                                gstactive = 1
                            Else
                                gstactive = 0
                            End If
                            clsDBFuncationality.ExecuteNonQuery(" UPDATE TSPL_TAX_MASTER SET GSTActive = '" & gstactive & "',PayableControl = '" & (fndpayable.Value) & "' , DepositControl = '" & (txtDepositControl.Value) & "' where Tax_code='" & findTaxAuthority.Value & "'", trans)
                            'clsDBFuncationality.ExecuteNonQuery(" UPDATE TSPL_TAX_MASTER SET PayableControl = '" & clsCommon.myCBool(fndpayable.Value) & "' DepositControl = '" & clsCommon.myCBool(txtDepositControl.Value) & "' where Tax_code='" & findTaxAuthority.Value & "'", trans)
                        End If
                        ''-----------End

                        myMessages.insert()

                        '' currencyconversion
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", IIf(objCommonVar.IsMultiCurrencyCompany = False, objCommonVar.BaseCurrencyCode, Me.txtCurrencyCode.Value), True)
                        clsCommon.AddColumnsForChange(coll, "ConvRate", clsCommon.myCdbl(txtConversionRate.Text))
                        clsCommon.AddColumnsForChange(coll, "Is_Mandi_Tax", clsCommon.myCstr(IIf(chkmanditax.Checked = True, "Y", "N")))
                        clsCommon.AddColumnsForChange(coll, "Is_TCS", clsCommon.myCstr(IIf(chkTCS.Checked = True, "Y", "N")))
                        clsCommon.AddColumnsForChange(coll, "Is_MandiTaxCess", clsCommon.myCstr(IIf(chk_Mandi_Tax_Cess.Checked = True, "Y", "N")))
                        clsCommon.AddColumnsForChange(coll, "Is_Change_Rate", IIf(chkAllowToChangeRate.Checked, 1, 0))
                        If clsCommon.myLen(txtApplicableFrom.Text) > 0 Then
                            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(txtApplicableFrom.Text, "dd/MMM/yyyy"), True)
                        End If
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TAX_MASTER", OMInsertOrUpdate.Update, "Tax_Code='" + clsCommon.myCstr(findTaxAuthority.Value) + "'", trans)
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(findTaxAuthority.Value), "TSPL_TAX_MASTER", "Tax_Code", trans)
                        ' reset()
                        btnAdd.Text = "Update"
                        btnDelete.Enabled = True

                        '===============Save CSA Detail==BM00000004018================
                        Dim qry As String = "delete from TSPL_TAX_CSA_DETAIL where Tax_Code='" + clsCommon.myCstr(findTaxAuthority.Value) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        If chkmanditax.Checked Then
                            For Each strv As String In cbgCSA.CheckedValue
                                coll = New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Tax_Code", clsCommon.myCstr(findTaxAuthority.Value))
                                clsCommon.AddColumnsForChange(coll, "CSA_Type", clsCommon.myCstr(strv))

                                If clsCommon.myLen(strv) > 0 Then
                                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TAX_CSA_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                                End If
                            Next
                        End If
                        '=============================================

                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                ElseIf btnAdd.Text = "Update" Then
                    Try
                        If chkTCS.Checked = True Then
                            Dim tcscount As Integer = clsDBFuncationality.getSingleValue("SELECT count(Is_TCS) from tspl_tax_master where Tax_Code<>'" & findTaxAuthority.Value & "' and Is_TCS='Y'", trans)
                            If tcscount > 0 Then
                                Throw New Exception("Tax of TCS type already exist")
                            End If
                        End If

                        connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_MASTER_UPDATE", New SqlParameter("@Tax_Code", findTaxAuthority.Value), New SqlParameter("@Tax_Code_Desc", txtDesc.Text), New SqlParameter("@Tax_Liability_Account", findTaxRelAcc.Value), New SqlParameter("@Tax_Recoverable", taxRecoverable), New SqlParameter("@Tax_Recover_Account", findRecoverTaxAcc.Value), New SqlParameter("@Tax_Recover_Rate", clsCommon.myCdbl(txtRecoverRate.Value)), New SqlParameter("@Tax_Net_Payable", fndnetpay.Value), New SqlParameter("@Excisable", excisable), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Type", taxtype), New SqlParameter("@taxRecoverableAcc2", fndRecovTaxAcc2.Value), New SqlParameter("@TaxRecoverRate2", clsCommon.myCdbl(txtRecovRate2.Value)), New SqlParameter("@taxRecoverableAcc3", fndRecovTaxAcc3.Value), New SqlParameter("@TaxRecoverRate3", clsCommon.myCdbl(txtRecovRate3.Value)), New SqlParameter("@taxRecoverableAcc4", fndRecovTaxAcc4.Value), New SqlParameter("@TaxRecoverRate4", clsCommon.myCdbl(txtRecovRate4.Value)), New SqlParameter("@taxRecoverableAcc5", fndRecovTaxAcc5.Value), New SqlParameter("@TaxRecoverRate5", clsCommon.myCdbl(txtRecovRate5.Value)))
                        myMessages.update()
                        '' currencyconversion
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", IIf(objCommonVar.IsMultiCurrencyCompany = False, objCommonVar.BaseCurrencyCode, Me.txtCurrencyCode.Value), True)
                        clsCommon.AddColumnsForChange(coll, "ConvRate", clsCommon.myCdbl(txtConversionRate.Text))
                        clsCommon.AddColumnsForChange(coll, "Is_Mandi_Tax", clsCommon.myCstr(IIf(chkmanditax.Checked = True, "Y", "N")))
                        clsCommon.AddColumnsForChange(coll, "Is_TCS", clsCommon.myCstr(IIf(chkTCS.Checked = True, "Y", "N")))
                        clsCommon.AddColumnsForChange(coll, "Is_MandiTaxCess", clsCommon.myCstr(IIf(chk_Mandi_Tax_Cess.Checked = True, "Y", "N")))
                        If clsCommon.myLen(txtApplicableFrom.Text) > 0 Then
                            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(txtApplicableFrom.Text, "dd/MMM/yyyy"), True)
                        End If
                        clsCommon.AddColumnsForChange(coll, "Is_Change_Rate", IIf(chkAllowToChangeRate.Checked, 1, 0))
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TAX_MASTER", OMInsertOrUpdate.Update, "Tax_Code='" + clsCommon.myCstr(findTaxAuthority.Value) + "'", trans)
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(findTaxAuthority.Value), "TSPL_TAX_MASTER", "Tax_Code", trans)
                        '===============Save CSA Detail==BM00000004018================
                        Dim qry As String = "delete from TSPL_TAX_CSA_DETAIL where Tax_Code='" + clsCommon.myCstr(findTaxAuthority.Value) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        If chkmanditax.Checked Then
                            For Each strv As String In cbgCSA.CheckedValue
                                coll = New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Tax_Code", clsCommon.myCstr(findTaxAuthority.Value))
                                clsCommon.AddColumnsForChange(coll, "CSA_Type", clsCommon.myCstr(strv))

                                If clsCommon.myLen(strv) > 0 Then
                                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TAX_CSA_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                                End If
                            Next
                        End If
                        '=============================================
                        ' reset()
                        ''---------GST Related Work done
                        If AllowGSTApplicable = True Then
                            Dim gstactive As Integer = 0
                            If ChkGSTActive.Checked = True Then
                                gstactive = 1
                            Else
                                gstactive = 0
                            End If
                            clsDBFuncationality.ExecuteNonQuery(" UPDATE TSPL_TAX_MASTER SET GSTActive = '" & gstactive & "' where Tax_code='" & findTaxAuthority.Value & "'", trans)
                            clsDBFuncationality.ExecuteNonQuery(" UPDATE TSPL_TAX_MASTER SET PayableControl = '" & (fndpayable.Value) & "', DepositControl = '" & (txtDepositControl.Value) & "' where Tax_code='" & findTaxAuthority.Value & "'", trans)
                            clsDBFuncationality.ExecuteNonQuery(" UPDATE TSPL_TAX_MASTER SET Type = '" & taxtype & "' where Tax_code='" & findTaxAuthority.Value & "'", trans)
                        End If
                        ''-----------End
                        btnAdd.Text = "Update"
                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                End If

            End If

        Catch exx As Exception
            clsCommon.MyMessageBoxShow(exx.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
        ' reset()
    End Sub
    Public Sub DeleteData()
        If clsCommon.myLen(findTaxAuthority.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        If myMessages.deleteConfirm() Then
            Try
                connectSql.RunSp("sp_TSPL_TAX_MASTER_DELETE", New SqlParameter("@taxcode", findTaxAuthority.Value))
                clsDBFuncationality.ExecuteNonQuery("delete from TSPL_TAX_CSA_DETAIL where tax_code='" + findTaxAuthority.Value + "'")
                myMessages.delete()
                btnAdd.Text = "Save"
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeform()
    End Sub
    Public Sub closeform()
        Me.Close()

    End Sub

    'Private Sub txtRecoverRate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    e.Handled = TrapKey(Asc(e.KeyChar))
    'End Sub

    Private Sub chkTaxRecover_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTaxRecover.ToggleStateChanged
        If chkTaxRecover.Checked = True Then
            findRecoverTaxAcc.Enabled = True

            txtRecoverTaxAccDesc.Enabled = True
            fndRecovTaxAcc2.Enabled = True
            fndRecovTaxAcc3.Enabled = True
            fndRecovTaxAcc4.Enabled = True
            fndRecovTaxAcc5.Enabled = True
        Else
            findRecoverTaxAcc.Enabled = False
            findRecoverTaxAcc.Value = ""
            txtRecoverTaxAccDesc.Enabled = False
            txtRecoverTaxAccDesc.Text = ""
            txtRecoverRate.Enabled = False
            txtRecoverRate.Value = 0.0
            fndRecovTaxAcc2.Enabled = False
            fndRecovTaxAcc2.Value = ""
            txtRecovTaxAccDesc2.Text = ""
            txtRecovRate2.Enabled = False
            txtRecovRate2.Value = 0.0
            fndRecovTaxAcc3.Enabled = False
            fndRecovTaxAcc3.Value = ""
            txtRecovTaxAccDesc3.Text = ""
            txtRecovRate3.Enabled = False
            txtRecovRate3.Value = 0.0
            fndRecovTaxAcc4.Enabled = False
            fndRecovTaxAcc4.Value = ""
            txtRecovTaxAccDesc4.Text = ""
            txtRecovRate4.Enabled = False
            txtRecovRate4.Value = 0.0
            fndRecovTaxAcc5.Enabled = False
            fndRecovTaxAcc5.Value = ""
            txtRecovTaxAccDesc5.Text = ""
            txtRecovRate5.Enabled = False
            txtRecovRate5.Value = 0.0
        End If
    End Sub
#End Region

#Region "Finders"
    'Private Sub findTaxAuthority_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    findTaxAuthority.ConnectionString = connectSql.SqlCon()
    '    findTaxAuthority.Query = "select TAX_code as 'Tax Authority',Tax_Code_Desc as 'Description' from TSPL_TAX_MASTER"
    '    ' findTaxAuthority.Query = clsERPFuncationality.UserAvailableTaxGroup
    '    findTaxAuthority.ValueToSelect = "Tax Authority"
    '    findTaxAuthority.ValueToSelect1 = "Description"
    '    findTaxAuthority.Caption = "Tax Authority"
    'End Sub
    'Private Sub findTaxRelAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        findTaxRelAcc.ConnectionString = connectSql.SqlCon()
    '        findTaxRelAcc.Query = "select Account_code as 'Account No.',Description,(case when status='Y' then 'Active' else 'In Active' end) as Status,Account_Balance as Balance,Account_Type as 'Account Type'  from TSPL_GL_Accounts Order by Account_Code"
    '        '  findTaxRelAcc.Query = clsERPFuncationality.UserAvailableTaxGroup
    '        findTaxRelAcc.ValueToSelect = "Account No."
    '        findTaxRelAcc.ValueToSelect1 = "Description"
    '        findTaxRelAcc.Caption = "Tax Liability Accounts"
    '        findTaxRelAcc.Value.MaxLength = 50
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub

    'Private Sub findRecoverTaxAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    findRecoverTaxAcc.ConnectionString = connectSql.SqlCon()
    '    findRecoverTaxAcc.Query = "select Account_code as 'Account No.',Description,(case when status='Y' then 'Active' else 'In Active' end) as Status,Account_Balance as Balance,Account_Type as 'Account Type'  from TSPL_GL_Accounts Order by Account_Code"
    '    ' findRecoverTaxAcc.Query = clsERPFuncationality.UserAvailableTaxGroup
    '    findRecoverTaxAcc.ValueToSelect = "Account No."
    '    findRecoverTaxAcc.ValueToSelect1 = "Description"
    '    findRecoverTaxAcc.Caption = "Tax Recoverable Accounts"
    'End Sub

    Private Sub findTaxRelAcc_TextChanged()
        txtTaxRelAccDesc.Text = findTaxRelAcc.Tag
        sql = "select Description  from TSPL_GL_Accounts Where Account_code='" + findTaxRelAcc.Value + "'"
        txtTaxRelAccDesc.Text = connectSql.RunScalar(sql)
    End Sub

    'Private Sub findRecoverTaxAcc_TextChanged()
    '    txtRecoverTaxAccDesc.Text = findRecoverTaxAcc.txtValue.Tag
    '    sql = "select Description from TSPL_GL_Accounts WHERE Account_code='" + findRecoverTaxAcc.txtValue.Text + "'"
    '    txtRecoverTaxAccDesc.Text = connectSql.RunScalar(sql)
    '    If findRecoverTaxAcc.txtValue.Text <> String.Empty Then
    '        txtRecoverRate.Enabled = True
    '    ElseIf findRecoverTaxAcc.txtValue.Text = String.Empty Then
    '        txtRecoverRate.Enabled = False
    '    End If

    'End Sub

    Private Sub findTaxAuthority_TextChanged()
        sql = "select Tax_Liability_Account ,(case when Tax_Recoverable='Y' then 'True' else 'False' end) ,Tax_Recoverable_Account ,Tax_Recover_Rate,Tax_Net_Payable,(case when Excisable='Y' then 'True' else 'False' end),type,Tax_Recoverable_Account2 ,Tax_Recover_Rate2 ,Tax_Recoverable_Account3 ,Tax_Recover_Rate3 ,Tax_Recoverable_Account4 ,Tax_Recover_Rate4 ,Tax_Recoverable_Account5 ,Tax_Recover_Rate5  from TSPL_TAX_MASTER where Tax_Code='" + findTaxAuthority.Value + "'"
        dr = clsDBFuncationality.GetDataTable(sql)
        If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
            ' txtDesc.Text = findTaxAuthority.Tag
            'dr.Read()
            findTaxRelAcc.Value = dr.Rows(0)(0).ToString()
            chkTaxRecover.Checked = dr.Rows(0)(1).ToString()
            findRecoverTaxAcc.Value = dr.Rows(0)(2).ToString()
            Dim qry5 As String = "select Description from tspl_gl_accounts where Account_Code ='" + findRecoverTaxAcc.Value + "'"
            Dim RecovAcc1Desc As String = clsDBFuncationality.getSingleValue(qry5)
            txtRecoverTaxAccDesc.Text = RecovAcc1Desc
            If findRecoverTaxAcc.Value <> String.Empty Then
                txtRecoverRate.Enabled = True
            ElseIf findRecoverTaxAcc.Value = String.Empty Then
                txtRecoverRate.Enabled = False
                txtRecoverRate.Value = 0.0
            End If
            txtRecoverRate.Value = clsCommon.myFormat(dr.Rows(0)(3).ToString())
            fndnetpay.Value = dr.Rows(0)(4).ToString()
            chkExcisable.Checked = dr.Rows(0)(5).ToString()
            Dim type As String = dr.Rows(0)("type").ToString()
            If type = "V" Then
                drpTaxtype.Text = "VAT"
            ElseIf type = "C" Then
                drpTaxtype.Text = "CST"
            ElseIf type = "E" Then
                drpTaxtype.Text = "EXCISE"
            ElseIf type = "A" Then
                drpTaxtype.Text = "ADDTAX"
            ElseIf type = "O" Then
                drpTaxtype.Text = "OTHER"
            ElseIf type = "W" Then
                drpTaxtype.Text = "WCT"
            Else
                drpTaxtype.Text = "Select"
            End If
            
            fndRecovTaxAcc2.Value = dr.Rows(0)("Tax_Recoverable_Account2").ToString()
            Dim qry As String = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc2.Value + "'"
            Dim RecovAcc2Desc As String = clsDBFuncationality.getSingleValue(qry)
            txtRecovTaxAccDesc2.Text = RecovAcc2Desc

            If fndRecovTaxAcc2.Value <> String.Empty Then
                txtRecovRate2.Enabled = True
            ElseIf fndRecovTaxAcc2.Value = String.Empty Then
                txtRecovRate2.Enabled = False
                txtRecovRate2.Value = 0.0
            End If
            txtRecovRate2.Value = clsCommon.myFormat(dr.Rows(0)("Tax_Recover_Rate2").ToString())
            fndRecovTaxAcc3.Value = dr.Rows(0)("Tax_Recoverable_Account3").ToString()
            Dim qry1 As String = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc3.Value + "'"
            Dim RecovAcc3Desc As String = clsDBFuncationality.getSingleValue(qry1)
            txtRecovTaxAccDesc3.Text = RecovAcc3Desc
            If fndRecovTaxAcc3.Value <> String.Empty Then
                txtRecovRate3.Enabled = True
            ElseIf fndRecovTaxAcc3.Value = String.Empty Then
                txtRecovRate3.Enabled = False
                txtRecovRate3.Value = 0.0
            End If
            txtRecovRate3.Value = clsCommon.myFormat(dr.Rows(0)("Tax_Recover_Rate3").ToString())
            fndRecovTaxAcc4.Value = dr.Rows(0)("Tax_Recoverable_Account4").ToString()
            Dim qry2 As String = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc4.Value + "'"
            Dim RecovAcc4Desc As String = clsDBFuncationality.getSingleValue(qry2)
            txtRecovTaxAccDesc4.Text = RecovAcc4Desc
            If fndRecovTaxAcc4.Value <> String.Empty Then
                txtRecovRate4.Enabled = True
            ElseIf fndRecovTaxAcc4.Value = String.Empty Then
                txtRecovRate4.Enabled = False
                txtRecovRate4.Value = 0.0
            End If
            txtRecovRate4.Value = clsCommon.myFormat(dr.Rows(0)("Tax_Recover_Rate4").ToString())
            fndRecovTaxAcc5.Value = dr.Rows(0)("Tax_Recoverable_Account5").ToString()
            Dim qry3 As String = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc5.Value + "'"
            Dim RecovAcc5Desc As String = clsDBFuncationality.getSingleValue(qry3)
            txtRecovTaxAccDesc5.Text = RecovAcc5Desc
            If fndRecovTaxAcc5.Value <> String.Empty Then
                txtRecovRate5.Enabled = True
            ElseIf fndRecovTaxAcc5.Value = String.Empty Then
                txtRecovRate5.Enabled = False
                txtRecovRate5.Value = 0.0
            End If
            txtRecovRate5.Value = clsCommon.myFormat(dr.Rows(0)("Tax_Recover_Rate5").ToString())
            btnAdd.Text = "Update"
            btnDelete.Enabled = True
            btnAdd.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Else
            txtDesc.Text = String.Empty
            findTaxRelAcc.Value = String.Empty
            chkTaxRecover.Checked = False
            findRecoverTaxAcc.Value = String.Empty
            txtRecoverRate.Value = 0.0
            fndnetpay.Value = ""
            fndRecovTaxAcc2.Value = ""
            txtRecovRate2.Value = 0.0
            txtRecovTaxAccDesc2.Text = ""
            fndRecovTaxAcc3.Value = ""
            txtRecovRate3.Value = 0.0
            txtRecovTaxAccDesc3.Text = ""
            fndRecovTaxAcc4.Value = ""
            txtRecovRate4.Value = 0.0
            txtRecovTaxAccDesc4.Text = ""
            fndRecovTaxAcc5.Value = ""
            txtRecovRate5.Value = 0.0
            txtRecovTaxAccDesc5.Text = ""
            drpTaxtype.Text = "Select"
            btnAdd.Text = "Save"
            btnDelete.Enabled = False
            btnAdd.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        End If
        If findTaxAuthority.Value = String.Empty Then
            btnAdd.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

#End Region

#Region "Methods"

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "TAX-AUTH-M"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text, MessageBoxButtons.OK)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnAdd.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function
    Private Sub NumericKeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = globalFunc.TrapKey(Asc(e.KeyChar))
    End Sub

    Private Sub reset()
        chkExcisable.Enabled = False
        txtDesc.Text = String.Empty
        findTaxAuthority.Value = String.Empty
        findTaxAuthority.MyReadOnly = False
        findTaxRelAcc.Value = String.Empty
        chkTaxRecover.Checked = False
        chkmanditax.Checked = False
        cbgCSA.UnCheckedAll()
        chkTCS.Checked = False
        findRecoverTaxAcc.Value = String.Empty
        txtRecoverTaxAccDesc.Text = ""
        txtRecoverRate.Value = 0.0
        fndnetpay.Value = String.Empty
        chkExcisable.Checked = False
        fndRecovTaxAcc2.Value = ""
        txtRecovTaxAccDesc2.Text = ""
        txtRecovRate2.Value = 0.0
        txtRecovRate3.Value = 0.0
        txtRecovRate4.Value = 0.0
        txtRecovRate5.Value = 0.0
        txtRecovTaxAccDesc3.Text = ""
        txtRecovTaxAccDesc4.Text = ""
        txtRecovTaxAccDesc5.Text = ""
        fndRecovTaxAcc3.Value = ""
        fndRecovTaxAcc4.Value = ""
        fndRecovTaxAcc5.Value = ""
        drpTaxtype.Text = "Select"
        btnAdd.Text = "Save"
        btnAdd.Enabled = True
        txtTaxRelAccDesc.Text = ""
        txtnetpay.Text = ""
        ChkGSTActive.Checked = False
        txtDepositControl.Value = ""
        fndpayable.Value = ""
        lblDepositControl.Text = ""
        rdtxtpayablescontrol.Text = ""
        chk_Mandi_Tax_Cess.Checked = False
    End Sub
    Private Function validateRecords() As Boolean
        'txtTaxRelAccDesc.Text = findTaxRelAcc.Tag
        If (findTaxRelAcc.Value <> String.Empty) Then
            sql = "select Description  from TSPL_GL_Accounts Where Account_code='" + findTaxRelAcc.Value + "'"
            If connectSql.RunScalar(sql) Is Nothing Then
                common.clsCommon.MyMessageBoxShow("Tax Liability Account not found.")
                findTaxRelAcc.Focus()
                Return False
            End If
        End If
        If clsCommon.myCstr(drpTaxtype.Text) = "Select" Then
            myMessages.blankValue("Tax Type")
            drpTaxtype.Focus()
            Return False
        End If
        'If chkTaxRecover.Checked = True And findTaxRelAcc.Value.Text <> String.Empty Then
        '    sql = "select Description from TSPL_GL_Accounts WHERE Account_code='" + findRecoverTaxAcc.Value + "'"
        '    If connectSql.RunScalar(sql) Is Nothing Then
        '        common.clsCommon.MyMessageBoxShow("Recover Tax Account not found.")
        '        findRecoverTaxAcc.Focus()
        '        Return False
        '    End If
        'End If
        If (txtDesc.Text = String.Empty) Then
            myMessages.blankValue("Description")
            txtDesc.Focus()
            Return False
        ElseIf (findTaxRelAcc.Value = String.Empty) Then
            myMessages.blankValue("Tax Liability Account")
            findTaxRelAcc.Focus()
            Return False

        ElseIf chkTaxRecover.Checked = True Then
            If (findRecoverTaxAcc.Value = String.Empty) Then
                myMessages.blankValue("Tax Recoverable Account1")
                findRecoverTaxAcc.Focus()
                Return False
            ElseIf (findRecoverTaxAcc.Value = String.Empty) AndAlso (fndRecovTaxAcc2.Value <> String.Empty) Then
                myMessages.blankValue("Tax Recoverable Account1")
                findRecoverTaxAcc.Focus()
                Return False
            ElseIf ((findRecoverTaxAcc.Value = String.Empty) OrElse (fndRecovTaxAcc2.Value = String.Empty)) AndAlso (fndRecovTaxAcc3.Value <> String.Empty) Then
                myMessages.blankValue("Other Recoverable Account")
                ' findRecoverTaxAcc.Focus()
                Return False
            ElseIf ((findRecoverTaxAcc.Value = String.Empty) OrElse (fndRecovTaxAcc2.Value = String.Empty) OrElse (fndRecovTaxAcc3.Value = String.Empty)) AndAlso (fndRecovTaxAcc4.Value <> String.Empty) Then
                myMessages.blankValue("Other Recoverable Account")
                'findRecoverTaxAcc.Focus()
                Return False
            ElseIf ((findRecoverTaxAcc.Value = String.Empty) OrElse (fndRecovTaxAcc2.Value = String.Empty) OrElse (fndRecovTaxAcc3.Value = String.Empty) OrElse (fndRecovTaxAcc4.Value = String.Empty)) AndAlso (fndRecovTaxAcc5.Value <> String.Empty) Then
                myMessages.blankValue("Other Recoverable Account")
                ' findRecoverTaxAcc.Focus()
                Return False
            Else
                If (findRecoverTaxAcc.Value <> String.Empty) AndAlso (txtRecoverRate.Value = 0.0) Then
                    myMessages.blankValue("Tax Recoverable Rate1")
                    txtRecoverRate.Focus()
                    Return False
                ElseIf (fndRecovTaxAcc2.Value <> String.Empty) AndAlso (txtRecovRate2.Value = 0.0) Then
                    myMessages.blankValue("Tax Recoverable Rate2")
                    txtRecovRate2.Focus()
                    Return False
                ElseIf (fndRecovTaxAcc3.Value <> String.Empty) AndAlso (txtRecovRate3.Value = 0.0) Then
                    myMessages.blankValue("Tax Recoverable Rate3")
                    txtRecovRate3.Focus()
                    Return False
                ElseIf (fndRecovTaxAcc4.Value <> String.Empty) AndAlso (txtRecovRate4.Value = 0.0) Then
                    myMessages.blankValue("Tax Recoverable Rate4")
                    txtRecovRate4.Focus()
                    Return False
                ElseIf (fndRecovTaxAcc5.Value <> String.Empty) AndAlso (txtRecovRate5.Value = 0.0) Then
                    myMessages.blankValue("Tax Recoverable Rate5")
                    txtRecovRate5.Focus()
                    Return False
                Else
                    Dim Amount As Double
                    Amount = clsCommon.myCdbl(txtRecoverRate.Value) + clsCommon.myCdbl(txtRecovRate2.Value) + clsCommon.myCdbl(txtRecovRate3.Value) + clsCommon.myCdbl(txtRecovRate4.Value) + clsCommon.myCdbl(txtRecovRate5.Value)
                    If Amount <> Convert.ToDouble(100.0) Then
                        common.clsCommon.MyMessageBoxShow("Sum of Recoverable Rate should be equal to 100")
                        Return False
                    Else

                        Dim txtRate As List(Of RadTextBox) = New List(Of RadTextBox)
                        txtRate.Add(txtRecoverRate)
                        txtRate.Add(txtRecovRate2)
                        txtRate.Add(txtRecovRate3)
                        txtRate.Add(txtRecovRate4)
                        txtRate.Add(txtRecovRate5)
                        Dim txtrate1 As RadTextBox
                        For Each txtrate1 In txtRate
                            If txtrate1.Enabled = True And ((If(txtrate1.Text = "", 0.0, Convert.ToDouble(txtrate1.Text))) = 0.0) Then
                                common.clsCommon.MyMessageBoxShow("Your Recoverable Rate Equal to 100 so you can not select any other Account and also fill Recoverable Rate")
                                Return False
                            End If
                        Next

                    End If
                End If
            End If
        End If

        If Not chkTaxRecover.Checked Then
            Dim qry As String = "select GL_Account_Code from TSPL_TAX_RATES where Tax_Code='" + findTaxAuthority.Value + "' and Tax_Type='P' and len(ISNULL( GL_Account_Code,''))>0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                common.clsCommon.MyMessageBoxShow("Please first remove all GL Account from Tax rate of " + findTaxAuthority.Value + " Authority", Me.Text)
                Return False
            End If
        End If

        Return True
    End Function
#End Region

#Region "Menu Events"

#Region "Import/Export"


    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        sql = "Select Tax_Code as 'Tax Authority',Tax_Code_Desc as 'Description'," & _
        "Tax_Liability_Account as 'Tax Liability Account',(case when Tax_Recoverable='Y' then 'Yes' else 'No' END) as 'Tax Recoverable',Tax_Recoverable_Account as 'Tax Recoverable Account',Tax_Recover_Rate as 'Tax Recover Rate',Tax_Net_Payable as 'Tax Net Payable',(Case type when 'V'then 'VAT' when 'E'then 'EXCISE' when 'C'then 'CST' when 'A' then 'ADDTAX' when 'W' then 'WCT' when 'O' then  'OTHER'  when 'S' then 'SERVICE' when 'M' then 'MANDI TAX' when 'IGST' then 'IGST' when 'SGST' then 'SGST' when 'UGST' then 'UGST' when 'CGST' then 'CGST' else ''end)as 'Type',Tax_Recoverable_Account2 as 'Tax Recoverable Account2',Tax_Recover_Rate2 as 'Tax Recover Rate2',Tax_Recoverable_Account3 as 'Tax Recoverable Account3',Tax_Recover_Rate3 as 'Tax Recover Rate3',Tax_Recoverable_Account4 as 'Tax Recoverable Account4',Tax_Recover_Rate4 as 'Tax Recover Rate4',Tax_Recoverable_Account5 as 'Tax Recoverable Account5',Tax_Recover_Rate5 as 'Tax Recover Rate5',Is_MandiTaxCess AS [Is Mandi Tax Cess]  "
        ''---- GST Applicable 
        If AllowGSTApplicable = True Then
            sql += " ,PayableControl as [Payable Control],DepositControl as [Deposit Control],GSTActive as [GST Active] "
        End If
        ''-------END
        sql += " from TSPL_TAX_MASTER"

        ListImpExpColumnsMandatory = New List(Of String)({"Tax Authority", "Description"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Tax Authority"})
        transportSql.ExporttoExcel(sql, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub menuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        Dim inputs() As String = {}

        If AllowGSTApplicable Then
            inputs = {"Tax Authority", "Description", "Tax Liability Account", "Tax Recoverable", "Tax Recoverable Account", "Tax Recover Rate", "Tax Net Payable", "Type", "Tax Recoverable Account2", "Tax Recover Rate2", "Tax Recoverable Account3", "Tax Recover Rate3", "Tax Recoverable Account4", "Tax Recover Rate4", "Tax Recoverable Account5", "Tax Recover Rate5", "Is Mandi Tax Cess", "Allow To Change Rate[Y/N]", "GST Active", "Payable Control", "Deposit Control"}
        Else
            inputs = {"Tax Authority", "Description", "Tax Liability Account", "Tax Recoverable", "Tax Recoverable Account", "Tax Recover Rate", "Tax Net Payable", "Type", "Tax Recoverable Account2", "Tax Recover Rate2", "Tax Recoverable Account3", "Tax Recover Rate3", "Tax Recoverable Account4", "Tax Recover Rate4", "Tax Recoverable Account5", "Tax Recover Rate5", "Is Mandi Tax Cess", "Allow To Change Rate[Y/N]"}
        End If

        Dim Strs As List(Of String) = New List(Of String)(inputs)

        If transportSql.importExcel(gv, Strs.ToArray()) Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strTaxCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strDesc As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strLiabAcc As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim strRecoverable As String = clsCommon.myCstr(grow.Cells(3).Value)
                    Dim strRecoverAcc As String = clsCommon.myCstr(grow.Cells(4).Value)
                    Dim strRecoverRate As String = clsCommon.myCstr(grow.Cells(5).Value)
                    Dim strnetpay As String = clsCommon.myCstr(grow.Cells(6).Value)
                    Dim Type As String = grow.Cells(7).Value.ToString.ToUpper
                    Dim strRecoverAcc2 As String = clsCommon.myCstr(grow.Cells(8).Value)
                    Dim strrecoverRate2 As String = clsCommon.myCstr(grow.Cells(9).Value)
                    Dim strRecoverAcc3 As String = clsCommon.myCstr(grow.Cells(10).Value)
                    Dim strrecoverRate3 As String = clsCommon.myCstr(grow.Cells(11).Value)
                    Dim strRecoverAcc4 As String = clsCommon.myCstr(grow.Cells(12).Value)
                    Dim strrecoverRate4 As String = clsCommon.myCstr(grow.Cells(13).Value)
                    Dim strRecoverAcc5 As String = clsCommon.myCstr(grow.Cells(14).Value)
                    Dim strrecoverRate5 As String = clsCommon.myCstr(grow.Cells(15).Value)
                    Dim strMandiTaxCess As String = clsCommon.myCstr(grow.Cells(16).Value)
                    Dim IsChangeRate As Integer = IIf(clsCommon.CompairString(clsCommon.myCstr(grow.Cells(17).Value), "Y") = CompairStringResult.Equal, 1, 0)
                    Dim GSTActive As Integer =Nothing
                    Dim Payable As String = Nothing
                    Dim Deposit As String = Nothing
                    If AllowGSTApplicable Then
                        GSTActive = clsCommon.myCstr(grow.Cells(18).Value)
                        Payable = clsCommon.myCstr(grow.Cells(19).Value)
                        Deposit = clsCommon.myCstr(grow.Cells(20).Value)
                    End If
                 


                    If Type = "VAT" Then
                        Type = "V"
                    ElseIf Type = "CST" Then
                        Type = "C"
                    ElseIf Type = "EXCISE" Then
                        Type = "E"
                    ElseIf Type = "ADDTAX" Then
                        Type = "A"
                    ElseIf Type = "WCT" Then
                        Type = "W"
                    ElseIf Type = "OTHER" Then
                        Type = "O"
                    ElseIf Type = "SGST" Then
                        Type = "SGST"
                    ElseIf Type = "IGST" Then
                        Type = "IGST"
                    ElseIf Type = "CGST" Then
                        Type = "CGST"
                    ElseIf Type = "UGST" Then
                        Type = "UGST"
                    ElseIf Type = "MANDI TAX" Then
                        Type = "M"
                    Else
                        Type = ""
                    End If
                    If String.IsNullOrEmpty(strTaxCode) Or strTaxCode.Length > 12 Then
                        Throw New Exception("Tax Authority has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strDesc) Or strDesc.Length > 50 Then
                        Throw New Exception("Description has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strLiabAcc.Length > 50 Then
                        Throw New Exception("Tax Liability Account has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If (strRecoverable = "Yes" Or strRecoverable = "True") Then
                        strRecoverable = "Y"
                    ElseIf (strRecoverable = "No" Or strRecoverable = "False") Then
                        strRecoverable = "N"
                    Else
                        Throw New Exception("Tax Recoverable has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If

                    If strRecoverAcc.Length > 50 Then
                        Throw New Exception("Tax Recoverable Account has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strnetpay.Length > 12 Then
                        Throw New Exception("Net Payble  Account has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If

                    Dim re As Regex = New Regex("(^[0-9]{1,3}(\.[0-9]{0,2})?$)")
                    If strRecoverRate.Length > 50 Or Not re.IsMatch(strRecoverRate) Then
                        Throw New Exception("Tax Recover Rate has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strRecoverAcc2.Length > 50 Then
                        Throw New Exception("Tax Recoverable Account2 has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    Dim re1 As Regex = New Regex("^[0-9]{1,3}(\.[0-9]{0,2})?$")
                    If strrecoverRate2.Length > 50 Or Not re1.IsMatch(strrecoverRate2) Then
                        Throw New Exception("Tax Recover Rate2 has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strRecoverAcc3.Length > 50 Then
                        Throw New Exception("Tax Recoverable Account3 has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strrecoverRate3.Length > 50 Or Not re1.IsMatch(strrecoverRate3) Then
                        Throw New Exception("Tax Recover Rate3 has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strRecoverAcc4.Length > 50 Then
                        Throw New Exception("Tax Recoverable Account4 has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strrecoverRate4.Length > 50 Or Not re1.IsMatch(strrecoverRate4) Then
                        Throw New Exception("Tax Recover Rate4 has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strRecoverAcc5.Length > 50 Then
                        Throw New Exception("Tax Recoverable Account5 has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strrecoverRate5.Length > 50 Or Not re1.IsMatch(strrecoverRate5) Then
                        Throw New Exception("Tax Recover Rate5 has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If clsCommon.CompairString(Type, "M") = CompairStringResult.Equal Then
                        If (strMandiTaxCess = "Yes" Or strMandiTaxCess = "Y") Then
                            strMandiTaxCess = "Y"
                        ElseIf (strMandiTaxCess = "No" Or strMandiTaxCess = "N") Then
                            strMandiTaxCess = "N"
                        Else
                            Throw New Exception("Mandi Tax Cess should be Y/N values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                    Else
                        If (strMandiTaxCess = "Yes" Or strMandiTaxCess = "Y") Then
                            strMandiTaxCess = "Y"
                        ElseIf (strMandiTaxCess = "No" Or strMandiTaxCess = "N") Then
                            strMandiTaxCess = "N"
                        Else
                            Throw New Exception("Mandi Tax Cess should be Y/N values")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If clsCommon.CompairString(strMandiTaxCess, "Y") = CompairStringResult.Equal And clsCommon.CompairString(Type, "M") <> CompairStringResult.Equal Then
                            Throw New Exception("Mandi Tax Cess should be Y only when Type is Mandi Tax.")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                    End If

                    If AllowGSTApplicable = True Then
                        'If clsCommon.myCdbl(GSTActive) <= 0 Then
                        '    Throw New Exception("Fill GST is Active/De-Active ")
                        '    trans.Rollback()
                        '    Me.Controls.Remove(gv)
                        '    Exit Sub
                        'End If
                        If Payable.Length > 50 Then
                            Throw New Exception("Fill Payable Account ")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                        If Deposit.Length > 50 Then
                            Throw New Exception("Fill Deposit Account ")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                    End If


                    Try
                        Dim sql1 As String = "select count(*) from TSPL_TAX_MASTER  where  Tax_Code='" + strTaxCode + "'"
                        Dim i2 As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i2 = 0) Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_MASTER_INSERT", New SqlParameter("@Tax_Code", strTaxCode), New SqlParameter("@Tax_Code_Desc", strDesc), New SqlParameter("@Tax_Liability_Account", strLiabAcc), New SqlParameter("@Tax_Recoverable", strRecoverable), New SqlParameter("@Tax_Recover_Account", strRecoverAcc), New SqlParameter("@Tax_Recover_Rate", strRecoverRate), New SqlParameter("@Tax_Net_Payable", strnetpay), New SqlParameter("@Excisable", ""), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode), New SqlParameter("@Type", Type), New SqlParameter("@taxRecoverableAcc2", strRecoverAcc2), New SqlParameter("@TaxRecoverRate2", strrecoverRate2), New SqlParameter("@taxRecoverableAcc3", strRecoverAcc3), New SqlParameter("@TaxRecoverRate3", strrecoverRate3), New SqlParameter("@taxRecoverableAcc4", strRecoverAcc4), New SqlParameter("@TaxRecoverRate4", strrecoverRate4), New SqlParameter("@taxRecoverableAcc5", strRecoverAcc5), New SqlParameter("@TaxRecoverRate5", strrecoverRate5))
                        Else
                            connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_MASTER_UPDATE", New SqlParameter("@Tax_Code", strTaxCode), New SqlParameter("@Tax_Code_Desc", strDesc), New SqlParameter("@Tax_Liability_Account", strLiabAcc), New SqlParameter("@Tax_Recoverable", strRecoverable), New SqlParameter("@Tax_Recover_Account", strRecoverAcc), New SqlParameter("@Tax_Recover_Rate", strRecoverRate), New SqlParameter("@Tax_Net_Payable", strnetpay), New SqlParameter("@Excisable", ""), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Type", Type), New SqlParameter("@taxRecoverableAcc2", strRecoverAcc2), New SqlParameter("@TaxRecoverRate2", strrecoverRate2), New SqlParameter("@taxRecoverableAcc3", strRecoverAcc3), New SqlParameter("@TaxRecoverRate3", strrecoverRate3), New SqlParameter("@taxRecoverableAcc4", strRecoverAcc4), New SqlParameter("@TaxRecoverRate4", strrecoverRate4), New SqlParameter("@taxRecoverableAcc5", strRecoverAcc5), New SqlParameter("@TaxRecoverRate5", strrecoverRate5))
                        End If
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Type", Type)
                        clsCommon.AddColumnsForChange(coll, "Is_MandiTaxCess", strMandiTaxCess)
                        clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", objCommonVar.BaseCurrencyCode)

                        clsCommon.AddColumnsForChange(coll, "Is_Change_Rate", IsChangeRate)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TAX_MASTER", OMInsertOrUpdate.Update, "Tax_Code='" + strTaxCode + "'", trans)

                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strTaxCode, "TSPL_TAX_MASTER", "Tax_Code", trans)


                        ''Update Import file on setting based

                        If AllowGSTApplicable = True Then
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_TAX_MASTER set GSTActive='" & GSTActive & "' ,PayableControl='" & Payable & "' , DepositControl='" & Deposit & "' where tax_code='" & strTaxCode & "'", trans)

                        End If
                        ''End

                    Catch ex As Exception

                        If ex.Message.Contains("Violation of PRIMARY KEY") Then
                            connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_MASTER_UPDATE", New SqlParameter("@Tax_Code", strTaxCode), New SqlParameter("@Tax_Code_Desc", strDesc), New SqlParameter("@Tax_Liability_Account", strLiabAcc), New SqlParameter("@Tax_Recoverable", strRecoverable), New SqlParameter("@Tax_Recover_Account", strRecoverAcc), New SqlParameter("@Tax_Recover_Rate", strRecoverRate), New SqlParameter("@Tax_Net_Payable", strnetpay), New SqlParameter("@Excisable", ""), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Type", Type), New SqlParameter("@taxRecoverableAcc2", strRecoverAcc2), New SqlParameter("@TaxRecoverRate2", strrecoverRate2), New SqlParameter("@taxRecoverableAcc3", strRecoverAcc3), New SqlParameter("@TaxRecoverRate3", strrecoverRate3), New SqlParameter("@taxRecoverableAcc4", strRecoverAcc4), New SqlParameter("@TaxRecoverRate4", strrecoverRate4), New SqlParameter("@taxRecoverableAcc5", strRecoverAcc5), New SqlParameter("@TaxRecoverRate5", strrecoverRate5))
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strTaxCode), "TSPL_TAX_MASTER", "Tax_Code", trans)
                        End If
                    End Try
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception

                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub


#End Region

    Private Sub menuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExit.Click
        Me.Close()
    End Sub

#End Region

    'Private Sub fndnetpay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndnetpay.ConnectionString = connectSql.SqlCon()
    '    fndnetpay.Query = "select account_code as [Account Code],description as [Description] from Tspl_gl_Accounts"
    '    ''   fndnetpay.Query = clsERPFuncationality.UserAvailableTaxGroup
    '    fndnetpay.ValueToSelect = "Account Code"
    '    fndnetpay.Caption = "Account"
    '    fndnetpay.ValueToSelect1 = "Description"
    'End Sub
    'Added By Abhishek kuamr 21/04/2012

    Private Sub fndRecovTaxAcc2__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRecovTaxAcc2._MYValidating
        Dim qry As String = "select Account_code as 'Account',Description,(case when status='Y' then 'Active' else 'In Active' end) as Status,Account_Balance as Balance,Account_Type as 'Account Type'  from TSPL_GL_Accounts "
        'fndRecovTaxAcc2.Value = clsCommon.ShowSelectForm("frmTaxRecTaxAcc2", qry, "Account", "", fndRecovTaxAcc2.Value, "Account_Code", isButtonClicked)
        fndRecovTaxAcc2.Value = clsGLAccount.getFinder("", fndRecovTaxAcc2.Value, isButtonClicked)
        If fndRecovTaxAcc2.Value <> String.Empty Then
            txtRecovRate2.Enabled = True
        ElseIf fndRecovTaxAcc2.Value = String.Empty Then
            txtRecovRate2.Enabled = False
            txtRecovRate2.Value = 0.0
        End If
        qry = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc2.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtRecovTaxAccDesc2.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtRecovTaxAccDesc2.Text = ""
        End If
    End Sub

    Private Sub fndRecovTaxAcc3__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRecovTaxAcc3._MYValidating
        Dim qry As String = "select Account_code as 'Account',Description,(case when status='Y' then 'Active' else 'In Active' end) as Status,Account_Balance as Balance,Account_Type as 'Account Type'  from TSPL_GL_Accounts "
        'fndRecovTaxAcc3.Value = clsCommon.ShowSelectForm("frmTaxRecTaxAcc3", qry, "Account", "", fndRecovTaxAcc3.Value, "Account_Code", isButtonClicked)
        fndRecovTaxAcc3.Value = clsGLAccount.getFinder("", fndRecovTaxAcc3.Value, isButtonClicked)
        If fndRecovTaxAcc3.Value <> String.Empty Then
            txtRecovRate3.Enabled = True
        ElseIf fndRecovTaxAcc3.Value = String.Empty Then
            txtRecovRate3.Enabled = False
            txtRecovRate3.Value = 0.0
        End If
        qry = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc3.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtRecovTaxAccDesc3.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtRecovTaxAccDesc3.Text = ""
        End If

    End Sub

    Private Sub fndRecovTaxAcc4__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRecovTaxAcc4._MYValidating
        Dim qry As String = "select Account_code as 'Account',Description,(case when status='Y' then 'Active' else 'In Active' end) as Status,Account_Balance as Balance,Account_Type as 'Account Type'  from TSPL_GL_Accounts "
        'fndRecovTaxAcc4.Value = clsCommon.ShowSelectForm("frmTaxRecTaxAcc4", qry, "Account", "", fndRecovTaxAcc4.Value, "Account_Code", isButtonClicked)
        fndRecovTaxAcc4.Value = clsGLAccount.getFinder("", fndRecovTaxAcc4.Value, isButtonClicked)
        If fndRecovTaxAcc4.Value <> String.Empty Then
            txtRecovRate4.Enabled = True
        ElseIf fndRecovTaxAcc4.Value = String.Empty Then
            txtRecovRate4.Enabled = False
            txtRecovRate4.Value = 0.0
        End If
        qry = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc4.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtRecovTaxAccDesc4.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtRecovTaxAccDesc4.Text = ""
        End If

    End Sub

    Private Sub fndRecovTaxAcc5__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndRecovTaxAcc5._MYValidating
        Dim qry As String = "select Account_code as 'Account',Description,(case when status='Y' then 'Active' else 'In Active' end) as Status,Account_Balance as Balance,Account_Type as 'Account Type'  from TSPL_GL_Accounts "
        'fndRecovTaxAcc5.Value = clsCommon.ShowSelectForm("frmTaxRecTaxAcc5", qry, "Account", "", fndRecovTaxAcc5.Value, "Account_Code", isButtonClicked)
        fndRecovTaxAcc5.Value = clsGLAccount.getFinder("", fndRecovTaxAcc5.Value, isButtonClicked)
        If fndRecovTaxAcc5.Value <> String.Empty Then
            txtRecovRate5.Enabled = True
        ElseIf fndRecovTaxAcc5.Value = String.Empty Then
            txtRecovRate5.Enabled = False
            txtRecovRate5.Value = 0.0
        End If

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc5.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtRecovTaxAccDesc5.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtRecovTaxAccDesc5.Text = ""
        End If

    End Sub




    Private Sub txtRecoverRate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRecoverRate.Validating
        If txtRecoverRate.Value > 100 Then
            common.clsCommon.MyMessageBoxShow("Value should be less then 100")
            txtRecoverRate.Value = 0.0
        End If
    End Sub

    Private Sub txtRecovRate2_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRecovRate2.Validating
        If txtRecovRate2.Value > 100 Then
            common.clsCommon.MyMessageBoxShow("Value should be less then 100")
            txtRecovRate2.Value = 0.0
        End If
    End Sub

    Private Sub txtRecovRate3_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRecovRate3.Validating
        If txtRecovRate3.Value > 100 Then
            common.clsCommon.MyMessageBoxShow("Value should be less then 100")
            txtRecovRate3.Value = 0.0
        End If
    End Sub

    Private Sub txtRecovRate4_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRecovRate4.Validating
        If txtRecovRate4.Value > 100 Then
            common.clsCommon.MyMessageBoxShow("Value should be less then 100")
            txtRecovRate4.Value = 0.0
        End If
    End Sub

    Private Sub txtRecovRate5_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRecovRate5.Validating
        If txtRecovRate5.Value > 100 Then
            common.clsCommon.MyMessageBoxShow("Value should be less then 100")
            txtRecovRate5.Value = 0.0
        End If
    End Sub

    Private Sub findRecoverTaxAcc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles findRecoverTaxAcc._MYValidating
        Dim qry As String = "select Account_code as 'Account',Description,(case when status='Y' then 'Active' else 'In Active' end) as Status,Account_Balance as Balance,Account_Type as 'Account Type'  from TSPL_GL_Accounts "
        'findRecoverTaxAcc.Value = clsCommon.ShowSelectForm("frmRecTaxAcc", qry, "Account", "", findRecoverTaxAcc.Value, "Account_Code", isButtonClicked)
        findRecoverTaxAcc.Value = clsGLAccount.getFinder("", findRecoverTaxAcc.Value, isButtonClicked)
        If findRecoverTaxAcc.Value <> String.Empty Then
            txtRecoverRate.Enabled = True
        ElseIf findRecoverTaxAcc.Value = String.Empty Then
            txtRecoverRate.Enabled = False
            txtRecoverRate.Value = 0.0
        End If

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + findRecoverTaxAcc.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtRecoverTaxAccDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtRecoverTaxAccDesc.Text = ""
        End If
    End Sub

    Private Sub frmTaxAuthority_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        'If e.Alt AndAlso e.Control AndAlso e.KeyCode = Keys.T Then
        '    Dim frm As frmTaxAuthority = New frmTaxAuthority()
        '    frm.ShowDialog()
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnAdd.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub

   

    Private Sub findTaxAuthority__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles findTaxAuthority._MYValidating
        Dim qst As String = "select count(*) from TSPL_TAX_MASTER where TAX_code='" + findTaxAuthority.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            findTaxAuthority.MyReadOnly = False
        Else
            findTaxAuthority.MyReadOnly = True
        End If
        If findTaxAuthority.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select TAX_code as 'TaxAuthority',Tax_Code_Desc as 'Description' from TSPL_TAX_MASTER"
            'findTaxAuthority.Value = clsCommon.ShowSelectForm("Taxauty", qry, "TaxAuthority", "", findTaxAuthority.Value, "TAX_code", isButtonClicked)
            findTaxAuthority.Value = clsTaxMaster.getFinder("", findTaxAuthority.Value, isButtonClicked)
            If findTaxAuthority.Value <> String.Empty Then
                txtDesc.Enabled = True
            ElseIf findTaxAuthority.Value = String.Empty Then
                txtDesc.Enabled = False
            End If
            qry = "select Tax_Code_Desc  from TSPL_TAX_MASTER where TAX_code ='" + findTaxAuthority.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                txtDesc.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Code_Desc"))
            Else
                txtDesc.Text = ""
            End If
            LoadData()
        End If
                            End Sub

    
    Private Sub findTaxAuthority__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles findTaxAuthority._MYNavigator
        Dim qst As String = " select TAX_code as 'Tax Authority',Tax_Code_Desc as 'Description' from TSPL_TAX_MASTER where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_TAX_MASTER .TAX_code in ('" + findTaxAuthority.Value + "')"
            Case NavigatorType.Next
                qst += " and TSPL_TAX_MASTER .TAX_code in (select min(TAX_code) from TSPL_TAX_MASTER where TAX_code  >'" + findTaxAuthority.Value + "')"
            Case NavigatorType.First
                qst += " and TSPL_TAX_MASTER .TAX_code in (select MIN(TAX_code) from TSPL_TAX_MASTER)"

            Case NavigatorType.Last
                qst += " and TSPL_TAX_MASTER .TAX_code in (select Max(TAX_code) from TSPL_TAX_MASTER)"
            Case NavigatorType.Previous
                qst += " and TSPL_TAX_MASTER .TAX_code in (select Max(TAX_code) from TSPL_TAX_MASTER where TAX_code  <'" + findTaxAuthority.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            findTaxAuthority.Value = clsCommon.myCstr(dt.Rows(0)("Tax Authority"))
            txtDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadData()
    End Sub

    Private Sub findTaxRelAcc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles findTaxRelAcc._MYValidating
        Dim qry As String = "select Account_code as Code,Description,(case when status='Y' then 'Active' else 'In Active' end) as Status,Account_Balance as Balance,Account_Type as 'Account Type'  from TSPL_GL_Accounts" 'Order by Account_Code"
        'findTaxRelAcc.Value = clsCommon.ShowSelectForm("frmTaxRelAcc", qry, "Code", "", findTaxRelAcc.Value, "Account_code", isButtonClicked)
        findTaxRelAcc.Value = clsGLAccount.getFinder("", findTaxRelAcc.Value, isButtonClicked)
        qry = "select Description  from TSPL_GL_Accounts where Account_Code ='" + findTaxRelAcc.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtTaxRelAccDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        Else
            txtTaxRelAccDesc.Text = ""
        End If
    End Sub

    Private Sub fndnetpay__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndnetpay._MYValidating
        Dim qry As String = "select account_code as Code,description as [Description] from Tspl_gl_Accounts"
        'fndnetpay.Value = clsCommon.ShowSelectForm("frmTaxnet", qry, "Code", "", fndnetpay.Value, "account_code", isButtonClicked)
        fndnetpay.Value = clsGLAccount.getFinder("", fndnetpay.Value, isButtonClicked)
        qry = "select description  from Tspl_gl_Accounts where account_code ='" + fndnetpay.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtnetpay.Text = clsCommon.myCstr(dt.Rows(0)("description"))
        Else
            txtnetpay.Text = ""
        End If
    End Sub
    Public Sub LoadData()
        cbgCSA.Enabled = False
        cbgCSA.UnCheckedAll()

        sql = "select Tax_Liability_Account ,(case when Tax_Recoverable='Y' then 'True' else 'False' end) ,Tax_Recoverable_Account ,Tax_Recover_Rate,Tax_Net_Payable,(case when Excisable='Y' then 'True' else 'False' end),type,Tax_Recoverable_Account2 ,Tax_Recover_Rate2 ,Tax_Recoverable_Account3 ,Tax_Recover_Rate3 ,Tax_Recoverable_Account4 ,Tax_Recover_Rate4 ,Tax_Recoverable_Account5 ,Tax_Recover_Rate5,CURRENCY_CODE,ConvRate,ApplicableFrom,Is_Mandi_Tax,Is_TCS,GSTActive,PayableControl,DepositControl,Is_MandiTaxCess  from TSPL_TAX_MASTER where Tax_Code='" + findTaxAuthority.Value + "'"
        dr = clsDBFuncationality.GetDataTable(sql)
        If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
            ' txtDesc.Text = findTaxAuthority.Tag

            findTaxRelAcc.Value = dr.Rows(0)(0).ToString()
            chkTaxRecover.Checked = dr.Rows(0)(1).ToString()
            findRecoverTaxAcc.Value = dr.Rows(0)(2).ToString()

            Dim qst As String = "select Description  from TSPL_GL_Accounts Where Account_code='" + findTaxRelAcc.Value + "'"
            Dim TaxRelAccDesc As String = clsDBFuncationality.getSingleValue(qst)
            txtTaxRelAccDesc.Text = TaxRelAccDesc

            Dim qry5 As String = "select Description from tspl_gl_accounts where Account_Code ='" + findRecoverTaxAcc.Value + "'"
            Dim RecovAcc1Desc As String = clsDBFuncationality.getSingleValue(qry5)
            txtRecoverTaxAccDesc.Text = RecovAcc1Desc
            If findRecoverTaxAcc.Value <> String.Empty Then
                txtRecoverRate.Enabled = True
            ElseIf findRecoverTaxAcc.Value = String.Empty Then
                txtRecoverRate.Enabled = False
                txtRecoverRate.Value = 0.0
            End If
            txtRecoverRate.Value = clsCommon.myFormat(dr.Rows(0)(3).ToString())
            fndnetpay.Value = dr.Rows(0)(4).ToString()
            chkExcisable.Checked = dr.Rows(0)(5).ToString()
            Dim type As String = dr.Rows(0)("type").ToString()
            If type = "V" Then
                drpTaxtype.Text = "VAT"
            ElseIf type = "C" Then
                drpTaxtype.Text = "CST"
            ElseIf type = "E" Then
                drpTaxtype.Text = "EXCISE"
            ElseIf type = "A" Then
                drpTaxtype.Text = "ADDTAX"
            ElseIf type = "O" Then
                drpTaxtype.Text = "OTHER"
            ElseIf type = "S" Then
                drpTaxtype.Text = "SERVICE"
            ElseIf type = "M" Then
                drpTaxtype.Text = "MANDI TAX"
            ElseIf type = "W" Then
                drpTaxtype.Text = "WCT"
            ElseIf type = "SGST" Then
                drpTaxtype.Text = "SGST"
            ElseIf type = "IGST" Then
                drpTaxtype.Text = "IGST"
            ElseIf type = "CGST" Then
                drpTaxtype.Text = "CGST"
            ElseIf type = "UGST" Then
                drpTaxtype.Text = "UGST"
            Else
                drpTaxtype.Text = "Select"
            End If
           


            fndRecovTaxAcc2.Value = dr.Rows(0)("Tax_Recoverable_Account2").ToString()
            Dim qry As String = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc2.Value + "'"
            Dim RecovAcc2Desc As String = clsDBFuncationality.getSingleValue(qry)
            txtRecovTaxAccDesc2.Text = RecovAcc2Desc

            If fndRecovTaxAcc2.Value <> String.Empty Then
                txtRecovRate2.Enabled = True
            ElseIf fndRecovTaxAcc2.Value = String.Empty Then
                txtRecovRate2.Enabled = False
                txtRecovRate2.Value = 0.0
            End If
            txtRecovRate2.Value = clsCommon.myFormat(dr.Rows(0)("Tax_Recover_Rate2").ToString())
            fndRecovTaxAcc3.Value = dr.Rows(0)("Tax_Recoverable_Account3").ToString()
            Dim qry1 As String = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc3.Value + "'"
            Dim RecovAcc3Desc As String = clsDBFuncationality.getSingleValue(qry1)
            txtRecovTaxAccDesc3.Text = RecovAcc3Desc
            If fndRecovTaxAcc3.Value <> String.Empty Then
                txtRecovRate3.Enabled = True
            ElseIf fndRecovTaxAcc3.Value = String.Empty Then
                txtRecovRate3.Enabled = False
                txtRecovRate3.Value = 0.0
            End If
            txtRecovRate3.Value = clsCommon.myFormat(dr.Rows(0)("Tax_Recover_Rate3").ToString())
            fndRecovTaxAcc4.Value = dr.Rows(0)("Tax_Recoverable_Account4").ToString()
            Dim qry2 As String = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc4.Value + "'"
            Dim RecovAcc4Desc As String = clsDBFuncationality.getSingleValue(qry2)
            txtRecovTaxAccDesc4.Text = RecovAcc4Desc
            If fndRecovTaxAcc4.Value <> String.Empty Then
                txtRecovRate4.Enabled = True
            ElseIf fndRecovTaxAcc4.Value = String.Empty Then
                txtRecovRate4.Enabled = False
                txtRecovRate4.Value = 0.0
            End If
            txtRecovRate4.Value = clsCommon.myFormat(dr.Rows(0)("Tax_Recover_Rate4").ToString())
            fndRecovTaxAcc5.Value = dr.Rows(0)("Tax_Recoverable_Account5").ToString()
            Dim qry3 As String = "select Description from tspl_gl_accounts where Account_Code ='" + fndRecovTaxAcc5.Value + "'"
            Dim RecovAcc5Desc As String = clsDBFuncationality.getSingleValue(qry3)
            txtRecovTaxAccDesc5.Text = RecovAcc5Desc
            If fndRecovTaxAcc5.Value <> String.Empty Then
                txtRecovRate5.Enabled = True
            ElseIf fndRecovTaxAcc5.Value = String.Empty Then
                txtRecovRate5.Enabled = False
                txtRecovRate5.Value = 0.0
            End If
            txtRecovRate5.Value = clsCommon.myFormat(dr.Rows(0)("Tax_Recover_Rate5").ToString())
            btnAdd.Text = "Update"
            btnDelete.Enabled = True
            btnAdd.Enabled = True
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If


            Try
                Dim strquery As String = "select account_code,description  from Tspl_gl_Accounts where account_code='" + fndnetpay.Value + "'"
                Dim dr As DataTable
                Dim strvalue As String = ""
                dr = clsDBFuncationality.GetDataTable(strquery)
                If dr.Rows IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                    strvalue = dr.Rows(0)(0).ToString()
                End If
                If strvalue <> "" Then

                    funfillnetpay()
                Else
                    txtnetpay.Text = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try

            '' MULTICURRENCY
            Me.txtCurrencyCode.Value = dr.Rows(0).Item("CURRENCY_CODE").ToString ''obj.CURRENCY_CODE
            Me.txtConversionRate.Text = dr.Rows(0).Item("ConvRate") ''obj.ConvRate
            If IsDBNull(dr.Rows(0).Item("ApplicableFrom")) Then
                Me.txtApplicableFrom.Text = ""
            Else
                Me.txtApplicableFrom.Text = clsCommon.GetPrintDate(dr.Rows(0).Item("ApplicableFrom"), "dd/MMM/yyyy")
            End If

            cbgCSA.UnCheckedAll()
            chkmanditax.Checked = clsCommon.myCBool(IIf(dr.Rows(0)("Is_Mandi_Tax") = "Y", True, False))
            chkTCS.Checked = clsCommon.myCBool(IIf(dr.Rows(0)("Is_TCS") = "Y", True, False))

            chk_Mandi_Tax_Cess.Checked = clsCommon.myCBool(IIf(dr.Rows(0)("Is_MandiTaxCess") = "Y", True, False))

            fndpayable.Value = dr.Rows(0)("PayableControl").ToString()
            Dim fndpayableqry As String = "select Description from tspl_gl_accounts where Account_Code ='" + fndpayable.Value + "'"
            Dim Payable1 As String = clsDBFuncationality.getSingleValue(fndpayableqry)
            rdtxtpayablescontrol.Text = Payable1

            txtDepositControl.Value = dr.Rows(0)("DepositControl").ToString()
            Dim txtDepositControlqry As String = "select Description from tspl_gl_accounts where Account_Code ='" + txtDepositControl.Value + "'"
            Dim txtDepositControlqry1 As String = clsDBFuncationality.getSingleValue(txtDepositControlqry)
            lblDepositControl.Text = txtDepositControlqry1

            ChkGSTActive.Checked = dr.Rows(0)("GSTActive").ToString()

            '' end  MULTICURRENCY
        Else
            txtDesc.Text = String.Empty
            findTaxRelAcc.Value = String.Empty
            chkTaxRecover.Checked = False
            findRecoverTaxAcc.Value = String.Empty
            txtRecoverRate.Value = 0.0
            fndnetpay.Value = ""
            fndRecovTaxAcc2.Value = ""
            txtRecovRate2.Value = 0.0
            txtRecovTaxAccDesc2.Text = ""
            fndRecovTaxAcc3.Value = ""
            txtRecovRate3.Value = 0.0
            txtRecovTaxAccDesc3.Text = ""
            fndRecovTaxAcc4.Value = ""
            txtRecovRate4.Value = 0.0
            txtRecovTaxAccDesc4.Text = ""
            fndRecovTaxAcc5.Value = ""
            txtRecovRate5.Value = 0.0
            txtRecovTaxAccDesc5.Text = ""
            drpTaxtype.Text = "Select"
            btnAdd.Text = "Save"
            btnDelete.Enabled = False
            btnAdd.Enabled = True

            Me.txtCurrencyCode.Value = ""
            Me.txtConversionRate.Text = 1
            Me.txtApplicableFrom.Text = ""

            chkmanditax.Checked = False
            cbgCSA.Enabled = False
            RadPanel1.Visible = False
            cbgCSA.UnCheckedAll()
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        End If

        '===================
        RadPanel1.Visible = False
        If chkmanditax.Checked Then
            sql = "select * from TSPL_TAX_CSA_DETAIL where Tax_Code='" + clsCommon.myCstr(findTaxAuthority.Value) + "'"
            dr = New DataTable()
            dr = clsDBFuncationality.GetDataTable(sql)

            Dim arr As New ArrayList()
            If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                For Each dr1 As DataRow In dr.Rows
                    arr.Add(clsCommon.myCstr(dr1("CSA_Type")))
                Next

                cbgCSA.CheckedValue = arr
                cbgCSA.Enabled = True
                RadPanel1.Visible = True
            End If
        End If
        '============================

        If findTaxAuthority.Value = String.Empty Then
            btnAdd.Enabled = False
            btnDelete.Enabled = False
        End If
    End Sub

    Public Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Select"
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "V"
        dr("Name") = "VAT"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "C"
        dr("Name") = "CST"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "EXCISE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "ADDTAX"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "O"
        dr("Name") = "OTHER"
        dt.Rows.Add(dr)


        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "SERVICE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "MANDI TAX"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "W"
        dr("Name") = "WCT"
        dt.Rows.Add(dr)

        If clsCommon.myCBool(AllowGSTApplicable) = True Then
            dr = dt.NewRow()
            dr("Code") = "SGST"
            dr("Name") = "SGST"
            dt.Rows.Add(dr)


            dr = dt.NewRow()
            dr("Code") = "IGST"
            dr("Name") = "IGST"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "CGST"
            dr("Name") = "CGST"
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Code") = "UGST"
            dr("Name") = "UGST"
            dt.Rows.Add(dr)

        End If

        drpTaxtype.DataSource = dt
        drpTaxtype.ValueMember = "Code"
        drpTaxtype.DisplayMember = "Name"

    End Sub
    
    Private Sub txtCurrencyCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCurrencyCode._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        txtCurrencyCode.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        ShowCurrencyDetail()
    End Sub

    Private Sub chkmanditax_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkmanditax.ToggleStateChanged
        If chkmanditax.Checked Then
            cbgCSA.Enabled = True
            RadPanel1.Visible = True
        Else
            cbgCSA.Enabled = False
            RadPanel1.Visible = False
        End If
    End Sub

    Private Sub drpTaxtype_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpTaxtype.TextChanged
        If clsCommon.myLen(drpTaxtype.Text) > 0 AndAlso clsCommon.CompairString(drpTaxtype.Text, "MANDI TAX") = CompairStringResult.Equal Then
            chkmanditax.Checked = True
            chk_Mandi_Tax_Cess.Visible = True
            chk_Mandi_Tax_Cess.Checked = False
        Else
            chkmanditax.Checked = False
            chk_Mandi_Tax_Cess.Visible = False
            chk_Mandi_Tax_Cess.Checked = False
        End If
    End Sub

    Private Sub fndpayable__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndpayable._MYValidating
        Dim qry As String
        arrlist = clsERPFuncationality.glaccountqueryForControlAcc(objCommonVar.CurrentUserCode)
        qry = arrlist.Item(0)
        whrcls = arrlist.Item(1)
        'If whrcls <> "" Then
        '    whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='Y' "
        'Else
        '    whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='Y' "
        'End If

        fndpayable.Value = clsCommon.ShowSelectForm("ACCODE", qry, "Account_Code", "", fndpayable.Value, "Account_Code", isButtonClicked)
        rdtxtpayablescontrol.Text = clsDBFuncationality.getSingleValue("select  Description  from TSPL_GL_ACCOUNTS where Account_Code='" + fndpayable.Value + "'")
    End Sub

    Private Sub txtDepositControl__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepositControl._MYValidating
        Dim qry As String
        arrlist = clsERPFuncationality.glaccountqueryForControlAcc(objCommonVar.CurrentUserCode)
        qry = arrlist.Item(0)
        whrcls = arrlist.Item(1)
        'If whrcls <> "" Then
        '    whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='Y' "
        'Else
        '    whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='Y' "
        'End If

        txtDepositControl.Value = clsCommon.ShowSelectForm("ACCODE", qry, "Account_Code", "", txtDepositControl.Value, "Account_Code", isButtonClicked)
        lblDepositControl.Text = clsDBFuncationality.getSingleValue("select  Description  from TSPL_GL_ACCOUNTS where Account_Code='" + txtDepositControl.Value + "'")
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(findTaxAuthority.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Tax Authority Code", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(findTaxAuthority.Value, "Tax_Code", "TSPL_TAX_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
