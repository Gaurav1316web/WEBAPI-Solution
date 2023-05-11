'--20/12/2012--Updation By --Pankaj Kumar--- Applied Validations
'-03/04/2013:5:41PM--Updation By--Pankaj Kumar--Transaction Problem while importing-------------------Ashok
'--preeti gupta-ticket no-[BM00000003128],BHA/21/06/18-000077 add Penalty charges account  richa 
'Ticket No-TEC/11/07/19-000941 Sanjay Change Account 1 -> Refrigerator Security ,Account 2 -> Other Security 
'Ticket No-TEC/12/07/19-000942 sanjay
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports XpertERPEngine
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel
Imports common


'Start date =18/5/2011
'End date =20/5/2011
'Last modify date = 30/5/2011
'Database =TSPLERP
' Tables=Tspl_customer_account_set
'This Cunstructer is used to send usercode and compcode data in table.
Public Class frmCustomerAccountSet
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim EnableDistributorSubsidy As Boolean = False
    Dim dr As SqlDataReader
    Dim ds As DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub CustomerAccountSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetDataBaseGrid()
        SetUserMgmtNew()
        funreset()

        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnnew, "Press Alt+N Adding New Trasnaction")
        If fndaccountsetcode.Value = "" Then
            rdtxtdescription.Enabled = True
            fndrecisvablecontrol.Enabled = True
            fndadvance.Enabled = True
            fndrecieptdiscount.Enabled = True
            fndwriteoffs.Enabled = True
            rdbtndelete.Enabled = False
            rdbtnsave.Enabled = True
            rdbtnclose.Enabled = True
            fndrecisvablecontrol.BackColor = Color.White
            ValidateLength()
            ApplyReadOption()
            'SetTabIndex()
        End If
        '' check for multi currency
        If CheckMultiCurrency() = True Then
            Me.fndBaseCurrency.Enabled = True
            Me.fndExchangeLoss.Enabled = True
            Me.fndExchangeGain.Enabled = True

        Else
            Me.fndBaseCurrency.Enabled = False
            Me.fndExchangeLoss.Enabled = False
            Me.fndExchangeGain.Enabled = False
        End If
        EnableDistributorSubsidy = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.EnableDistributorSubsidy, clsFixedParameterCode.EnableDistributorSubsidy, Nothing)) = "1", True, False))
        If EnableDistributorSubsidy = True Then
            txtSubsidy.Visible = True
            txtSubsidyAccount.Visible = True
            lblSubsidyAccount.Visible = True
        End If
    End Sub
    Function CheckMultiCurrency() As Boolean
        Dim strq As String
        strq = "select * from tspl_module_currency_mapping where comp_code='" + objCommonVar.CurrentCompanyCode + "' and module_code='" & Me.Module_Code & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Apply") = True Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Private Sub ValidateLength()
        fndaccountsetcode.MyMaxLength = 12
        rdtxtdescription.MaxLength = 50
    End Sub
    Private Sub ApplyReadOption()
        txtcontainer.ReadOnly = True
        gvDB.AllowDeleteRow = False
        rdtxtadvance.ReadOnly = True
        rdtxtrecieptdicount.ReadOnly = True
        rdtxtrecievablecontrol.ReadOnly = True
        rdtxtwriteoff.ReadOnly = True
    End Sub
    Private Sub SetTabIndex()
        fndaccountsetcode.TabIndex = 0
        rdtxtdescription.TabIndex = 1
        fndrecisvablecontrol.TabIndex = 2
        fndrecieptdiscount.TabIndex = 3
        fndadvance.TabIndex = 4
        fndwriteoffs.TabIndex = 5
        fndcontainer.TabIndex = 6
        rdbtnsave.TabIndex = 7
        rdbtndelete.TabIndex = 8
        rdbtnclose.TabIndex = 9
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomerAccountSet)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If rdbtnsave.Visible = True Then
            rdmenuimport.Enabled = True
            rdmenuexport.Enabled = True
        Else
            rdmenuimport.Enabled = False
            rdmenuexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub Container_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtcontainer.Text = connectSql.RunScalar("select Description  from TSPL_GL_ACCOUNTS where Account_Code = '" + fndcontainer.Value + "' ")
    End Sub
    'This function is used for Insert data.
    Public Sub funinsert()
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False)) Then
                Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CUSTOMER_ACCOUNT_SET where Cust_account='" & fndaccountsetcode.Value & "'")
                If ChkNewEntry = 0 Then
                    fndaccountsetcode.Value = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"), clsDocType.CustomerAccountSet, "", "")
                    If clsCommon.myLen(fndaccountsetcode.Value) <= 0 Then
                        Throw New Exception("Error in Code Generation")
                    End If
                End If
            End If
            clsDBFuncationality.UpdateInSelectedDatabase(GetReplecateCompaniesDataBase(), "sp_customeraccountset_insert", New SqlParameter("@custacc", fndaccountsetcode.Value), New SqlParameter("@custdesc", rdtxtdescription.Text.ToString()), New SqlParameter("@containerdeposit", fndcontainer.Value), New SqlParameter("@receivablecontrolacc", fndrecisvablecontrol.Value()), New SqlParameter("@receiptdiscacc ", fndrecieptdiscount.Value), New SqlParameter("@advanceacc", fndadvance.Value), New SqlParameter("@writeoff", fndwriteoffs.Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modofieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            myMessages.insert()
            '' multicurrency
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", IIf(objCommonVar.IsMultiCurrencyCompany = False, objCommonVar.BaseCurrencyCode, Me.fndBaseCurrency.Value), True)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_LOSS_ACCOUNT", Me.fndExchangeLoss.Value, True)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_GAIN_ACCOUNT", Me.fndExchangeGain.Value, True)
            'richa  Ticket No. BM00000003087 on 08/07/2014
            clsCommon.AddColumnsForChange(coll, "SECURITY_ACCOUNT", Me.FndSecurity.Value, True)
            clsCommon.AddColumnsForChange(coll, "CREATE_SECURITY_ACCOUNT", Me.fndCreateSecurity.Value, True)
            clsCommon.AddColumnsForChange(coll, "BANK_GUARANTEE", Me.fndBankGuarantee.Value, True)
            clsCommon.AddColumnsForChange(coll, "ACCOUNT1", Me.fndAccount1.Value, True)
            clsCommon.AddColumnsForChange(coll, "ACCOUNT2", Me.fndAccount2.Value, True)
            clsCommon.AddColumnsForChange(coll, "Foreign_Bank_Charges_Account", Me.TxtForeignBankCharges.Value, True)
            clsCommon.AddColumnsForChange(coll, "Bank_Charges_Other_Account", Me.TxtBankChargesOther.Value, True)
            clsCommon.AddColumnsForChange(coll, "Penalty_Charges_Account", Me.FndPenaltyCharges.Value, True)
            '---------richa Code Ends------------------------------------

            '=================BM00000003604===================================
            clsCommon.AddColumnsForChange(coll, "GSOC_Acct", Me.txtgsoc.Value, True)
            clsCommon.AddColumnsForChange(coll, "Consignment_Acct", Me.txtconsignmnt.Value, True)
            clsCommon.AddColumnsForChange(coll, "Gain_Acct", Me.txtgain.Value, True)
            clsCommon.AddColumnsForChange(coll, "Loss_Acct", Me.txtloss.Value, True)
            '========BM00000003604======end here=============================
            clsCommon.AddColumnsForChange(coll, "SubSidy_Account", Me.txtSubsidyAccount.Value, True)

            '' agaisnt ticket no. SWA/13/08/18-000041 by Parteek on 13/08/2018
            clsCommon.AddColumnsForChange(coll, "Leakage_Deduction", Me.fndLeakageDeduction.Value, True)
            ' Ticket No : TEC/02/11/18-000359 By Prabhakar
            clsCommon.AddColumnsForChange(coll, "Customer_Opening_Clearing_AC", Me.fndCustomerOpeningClearingAC.Value, True)
            clsCommon.AddColumnsForChange(coll, "Customer_Security_Opening_Clearing_AC", Me.fndCustomerSecurityOpeningClearingAC.Value, True)

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ACCOUNT_SET", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_ACCOUNT_SET.Cust_account='" + fndaccountsetcode.Value + "'", Nothing)
            '' end multicurrency
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text())
        End Try
    End Sub
    'This function is used to update Data.
    Public Sub funupdate()
        Try
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndaccountsetcode.Value, "TSPL_CUSTOMER_ACCOUNT_SET", "Cust_account", Nothing)
            clsDBFuncationality.UpdateInSelectedDatabase(GetReplecateCompaniesDataBase(), "sp_customeraccountset_update", New SqlParameter("custacct", fndaccountsetcode.Value), New SqlParameter("@custdesc", rdtxtdescription.Text.ToString()), New SqlParameter("@containerdeposit", fndcontainer.Value), New SqlParameter("@receivablecontrolacc", fndrecisvablecontrol.Value()), New SqlParameter("@receiptdiscacc", fndrecieptdiscount.Value), New SqlParameter("@advanceacc", fndadvance.Value), New SqlParameter("@writeoff", fndwriteoffs.Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modofieddate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            myMessages.update()
            '' multicurrency
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", IIf(objCommonVar.IsMultiCurrencyCompany = False, objCommonVar.BaseCurrencyCode, Me.fndBaseCurrency.Value), True)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_LOSS_ACCOUNT", Me.fndExchangeLoss.Value, True)
            clsCommon.AddColumnsForChange(coll, "EXCHANGE_GAIN_ACCOUNT", Me.fndExchangeGain.Value, True)
            'richa  Ticket No. BM00000003087 on 08/07/2014
            clsCommon.AddColumnsForChange(coll, "SECURITY_ACCOUNT", Me.FndSecurity.Value, True)
            clsCommon.AddColumnsForChange(coll, "CREATE_SECURITY_ACCOUNT", Me.fndCreateSecurity.Value, True)
            clsCommon.AddColumnsForChange(coll, "BANK_GUARANTEE", Me.fndBankGuarantee.Value, True)
            clsCommon.AddColumnsForChange(coll, "ACCOUNT1", Me.fndAccount1.Value, True)
            clsCommon.AddColumnsForChange(coll, "ACCOUNT2", Me.fndAccount2.Value, True)
            clsCommon.AddColumnsForChange(coll, "Foreign_Bank_Charges_Account", Me.TxtForeignBankCharges.Value, True)
            clsCommon.AddColumnsForChange(coll, "Bank_Charges_Other_Account", Me.TxtBankChargesOther.Value, True)
            clsCommon.AddColumnsForChange(coll, "Penalty_Charges_Account", Me.FndPenaltyCharges.Value, True)
            '---------richa Code Ends------------------------------------

            '=================BM00000003604===================================
            clsCommon.AddColumnsForChange(coll, "GSOC_Acct", Me.txtgsoc.Value, True)
            clsCommon.AddColumnsForChange(coll, "Consignment_Acct", Me.txtconsignmnt.Value, True)
            clsCommon.AddColumnsForChange(coll, "Gain_Acct", Me.txtgain.Value, True)
            clsCommon.AddColumnsForChange(coll, "Loss_Acct", Me.txtloss.Value, True)
            '========BM00000003604======end here=============================
            clsCommon.AddColumnsForChange(coll, "SubSidy_Account", Me.txtSubsidyAccount.Value, True)

            clsCommon.AddColumnsForChange(coll, "Leakage_Deduction", Me.fndLeakageDeduction.Value, True)

            clsCommon.AddColumnsForChange(coll, "Customer_Opening_Clearing_AC", Me.fndCustomerOpeningClearingAC.Value, True)
            clsCommon.AddColumnsForChange(coll, "Customer_Security_Opening_Clearing_AC", Me.fndCustomerSecurityOpeningClearingAC.Value, True)

            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ACCOUNT_SET", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_ACCOUNT_SET.Cust_account='" + fndaccountsetcode.Value + "'", Nothing)

            '' UPDATE CURRENCY CODE IN CUSTOMER MASTER TABLE
            Dim STRQ As String
            STRQ = "UPDATE TSPL_CUSTOMER_MASTER SET CURRENCY_CODE=TSPL_CUSTOMER_ACCOUNT_SET.CURRENCY_CODE FROM " & _
                " TSPL_CUSTOMER_ACCOUNT_SET WHERE TSPL_CUSTOMER_MASTER.Cust_Account=TSPL_CUSTOMER_ACCOUNT_SET.CUST_ACCOUNT AND TSPL_CUSTOMER_ACCOUNT_SET.CUST_ACCOUNT='" & fndaccountsetcode.Value & "' "
            clsDBFuncationality.ExecuteNonQuery(STRQ)

            '' end multicurrency
        Catch ex As Exception

        End Try
    End Sub
    'This function is used to delete Data.
    'richa  Ticket No. BM00000003087 on 08/07/2014


    Public Function fundelete()
        Try
            clsDBFuncationality.UpdateInSelectedDatabase(GetReplecateCompaniesDataBase(), "sp_customeraccountset_delete", New SqlParameter("@custacc", fndaccountsetcode.Value))
            Return True
        Catch ex As Exception
            myMessages.myExceptions(ex)
            Return False
        End Try
    End Function
    'Public Sub fundelete()
    '    Try
    '        clsDBFuncationality.UpdateInSelectedDatabase(GetReplecateCompaniesDataBase(), "sp_customeraccountset_delete", New SqlParameter("@custacc", fndaccountsetcode.Value))
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '        Exit Sub
    '    End Try
    'End Sub

    '---------richa Code Ends------------------------------------



    Private Sub rdbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso fndaccountsetcode.Value = "" Then
            myMessages.blankValue("Account Set Code")
            fndaccountsetcode.Focus()
        ElseIf (fndrecisvablecontrol.Value = "") Then
            myMessages.blankValue("Debtor Control")
            fndrecisvablecontrol.Focus()
        ElseIf (fndrecieptdiscount.Value = "") Then
            myMessages.blankValue("Reciept Discount")
            fndrecieptdiscount.Focus()
        ElseIf (fndwriteoffs.Value = "") Then
            myMessages.blankValue("Write Offs")
            fndwriteoffs.Focus()
            'richa  Ticket No. BM00000003087 on 08/07/2014
        ElseIf (FndSecurity.Value = "") Then
            myMessages.blankValue("Security")
            FndSecurity.Focus()
        ElseIf (fndCreateSecurity.Value = "") Then
            myMessages.blankValue("Create Security")
            fndCreateSecurity.Focus()
        ElseIf (fndBankGuarantee.Value = "") Then
            myMessages.blankValue("Bank Guarantee")
            fndBankGuarantee.Focus()

        ElseIf (fndAccount1.Value = "") Then
            myMessages.blankValue("Refrigerator Security")
            fndAccount1.Focus()
        ElseIf (fndAccount2.Value = "") Then
            myMessages.blankValue("Other Security")
            fndAccount2.Focus()
            '---------richa Code Ends------------------------------------
            'Check Control Account 
        ElseIf CheckControlAccount("Debtor Control", fndrecisvablecontrol.Value) = False Then
            fndrecisvablecontrol.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Reciept Discounts", fndrecieptdiscount.Value) = False Then
            fndrecieptdiscount.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Advance", fndadvance.Value) = False Then
            fndadvance.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Write - Offs", fndwriteoffs.Value) = False Then
            fndwriteoffs.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Container Deposit", fndcontainer.Value) = False Then
            fndcontainer.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Exchange Loss", fndExchangeLoss.Value) = False Then
            fndExchangeLoss.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Exchange Gain", fndExchangeGain.Value) = False Then
            fndExchangeGain.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Security", FndSecurity.Value) = False Then
            FndSecurity.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Create Security", fndCreateSecurity.Value) = False Then
            fndCreateSecurity.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Bank Guarantee", fndBankGuarantee.Value) = False Then
            fndBankGuarantee.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Account1", fndAccount1.Value) = False Then 'fndAccount2
            fndAccount1.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Account2", fndAccount2.Value) = False Then '
            fndAccount2.Focus()
            Exit Sub
        ElseIf clsCommon.myLen(TxtForeignBankCharges.Value) > 0 AndAlso CheckControlAccount("Foreign Bank Charges", TxtForeignBankCharges.Value) = False Then '
            TxtForeignBankCharges.Focus()
            Exit Sub
        ElseIf clsCommon.myLen(TxtBankChargesOther.Value) > 0 AndAlso CheckControlAccount("Bank Charges Other", TxtBankChargesOther.Value) = False Then '
            TxtBankChargesOther.Focus()
            Exit Sub
        ElseIf CheckControlAccount("Penalty Charges", FndPenaltyCharges.Value) = False Then '
            FndPenaltyCharges.Focus()
            Exit Sub
        Else
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.CustomerAccountSet, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If

            If rdbtnsave.Text = "Save" Then
                funinsert()
                funfill()
            ElseIf (rdbtnsave.Text = "Update") Then
                funupdate()
            End If
        End If
    End Sub

    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        If clsCommon.myLen(fndaccountsetcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        If myMessages.deleteConfirm() Then
            'richa  Ticket No. BM00000003087 on 08/07/2014:
            If fundelete() Then
                myMessages.delete()
                rdbtnsave.Text = "Save"
                rdbtndelete.Enabled = False
                funreset()
            Else
                common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            End If
            'fundelete()
            'myMessages.delete()
            'rdbtnsave.Text = "Save"
            'rdbtndelete.Enabled = False
            '---------richa Code Ends------------------------------------
        End If
    End Sub


    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        funreset()
    End Sub

    Private Sub fndaccountsetcode_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndaccountsetcode.Value <> "" Then
            fndadvance.Enabled = True
            rdtxtdescription.Enabled = True
            fndrecieptdiscount.Enabled = True
            fndrecisvablecontrol.Enabled = True
            fndwriteoffs.Enabled = True
            fndaccountsetcode.Enabled = True
            rdbtnsave.Enabled = True
            rdbtndelete.Enabled = True
            rdbtnclose.Enabled = True
        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    Private Sub fndaccountsetcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndaccountsetcode.Query = "Select cust_account as [Customer Account],cust_acct_Desc as [Account Description]from Tspl_Customer_account_Set"
        'fndaccountsetcode.ConnectionString = connectSql.SqlCon()
        'fndaccountsetcode.Caption = "Customer Account Set"
        'fndaccountsetcode.ValueToSelect = "Customer Account"
        'fndaccountsetcode.ValueToSelect1 = "Account Description"
    End Sub
    'This function is used to funfill data in all control from table.
    Public Sub funfill()
        Try

            Dim query As String = "select cust_account,cust_acct_desc,receivable_control_acct,receipts_discount_acct,advance_acct,write_offs, " & _
            " Container_Deposit,tspl_customer_account_set.CURRENCY_CODE,tspl_customer_account_set.EXCHANGE_LOSS_ACCOUNT, " & _
            " TSPL_CUSTOMER_ACCOUNT_SET.SECURITY_ACCOUNT,gl3.description as SECURITY_ACCOUNT_NAME ,TSPL_CUSTOMER_ACCOUNT_SET.CREATE_SECURITY_ACCOUNT,gl4.description as CREATE_SECURITY_ACCOUNT_NAME ,TSPL_CUSTOMER_ACCOUNT_SET.BANK_GUARANTEE, gl5.description as BANK_GUARANTEE_NAME ,TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT1, gl6.description as ACCOUNT1_NAME ,TSPL_CUSTOMER_ACCOUNT_SET.ACCOUNT2, gl7.description as ACCOUNT2_NAME,TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account, g23.description as Bank_Charges_Other_Account_NAME,TSPL_CUSTOMER_ACCOUNT_SET.Foreign_Bank_Charges_Account, g22.description as Foreign_Bank_Charges_Account_NAME,TSPL_CUSTOMER_ACCOUNT_SET.SubSidy_Account, g24.description as SubSidy_Account_Desc , " & _
            " TSPL_CURRENCY_MASTER.currency_name,gl1.description as EXCHANGE_LOSS_ACCOUNT_Name,gl2.description as EXCHANGE_GAIN_ACCOUNT_Name,tspl_customer_account_set.EXCHANGE_GAIN_ACCOUNT,tspl_customer_account_set.GSOC_Acct,gl8.description as gsoc_name,tspl_customer_account_set.Consignment_Acct,gl9.description as consgnmnt_name,tspl_customer_account_set.Gain_Acct,g20.description as gain_name,tspl_customer_account_set.Loss_Acct,g21.description as loss_name,TSPL_CUSTOMER_ACCOUNT_SET.Penalty_Charges_Account, g25.description as Penalty_Charges_Account_NAME,TSPL_CUSTOMER_ACCOUNT_SET.Leakage_Deduction,g26.Description as Leakage_Deduction_Desc,tspl_customer_account_set.Customer_Opening_Clearing_AC, g27.Description as Customer_Opening_Clearing_AC_Desc ,tspl_customer_account_set.Customer_Security_Opening_Clearing_AC , g28.Description as Customer_Security_Opening_Clearing_AC_Desc   from tspl_customer_account_set " & _
            " left join TSPL_CURRENCY_MASTER on tspl_customer_account_set.currency_code=TSPL_CURRENCY_MASTER.currency_code " & _
            " left join TSPL_GL_ACCOUNTS gl1 on tspl_customer_account_set.EXCHANGE_LOSS_ACCOUNT=gl1.account_code " & _
            " left join TSPL_GL_ACCOUNTS gl2 on tspl_customer_account_set.EXCHANGE_GAIN_ACCOUNT=gl2.account_code " & _
            " left join TSPL_GL_ACCOUNTS gl3 on tspl_customer_account_set.SECURITY_ACCOUNT=gl3.account_code " & _
            " left join TSPL_GL_ACCOUNTS gl4 on tspl_customer_account_set.CREATE_SECURITY_ACCOUNT=gl4.account_code " & _
            " left join TSPL_GL_ACCOUNTS gl5 on tspl_customer_account_set.BANK_GUARANTEE=gl5.account_code " & _
            " left join TSPL_GL_ACCOUNTS gl6 on tspl_customer_account_set.ACCOUNT1=gl6.account_code " & _
            "left join TSPL_GL_ACCOUNTS gl7 on tspl_customer_account_set.ACCOUNT2=gl7.account_code " & _
            "left join TSPL_GL_ACCOUNTS gl8 on tspl_customer_account_set.GSOC_Acct=gl8.account_code " & _
            "left join TSPL_GL_ACCOUNTS gl9 on tspl_customer_account_set.Consignment_Acct=gl9.account_code " & _
            "left join TSPL_GL_ACCOUNTS g20 on tspl_customer_account_set.Gain_Acct=g20.account_code " & _
            "left join TSPL_GL_ACCOUNTS g21 on tspl_customer_account_set.Loss_Acct=g21.account_code " & _
            "left join TSPL_GL_ACCOUNTS g22 on tspl_customer_account_set.Foreign_Bank_Charges_Account=g22.account_code " & _
            "left join TSPL_GL_ACCOUNTS g23 on tspl_customer_account_set.Bank_Charges_Other_Account=g23.account_code " & _
            "left join TSPL_GL_ACCOUNTS g24 on tspl_customer_account_set.SubSidy_Account=g24.account_code " & _
            "left join TSPL_GL_ACCOUNTS g25 on tspl_customer_account_set.Penalty_Charges_Account=g25.account_code " & _
            "left join TSPL_GL_ACCOUNTS g26 on tspl_customer_account_set.Leakage_Deduction=g26.account_code " & _
            " left join TSPL_GL_ACCOUNTS g27 on tspl_customer_account_set.Customer_Opening_Clearing_AC=g27.account_code " & _
            " left join TSPL_GL_ACCOUNTS g28 on tspl_customer_account_set.Customer_Security_Opening_Clearing_AC=g28.account_code " & _
                        " where cust_account='" + fndaccountsetcode.Value + "'"

            Dim adp As New SqlDataAdapter(query, connectSql.SqlCon)
            Dim dt As New DataTable()
            adp.Fill(dt)
            Dim dr As DataRow = dt.Rows(0)
            fndaccountsetcode.Value = dr(0).ToString().Trim()
            rdtxtdescription.Text = dr(1).ToString()
            fndrecisvablecontrol.Value = dr(2).ToString().Trim()
            fndrecieptdiscount.Value = dr(3).ToString()
            fndadvance.Value = dr(4).ToString().Trim()
            fndwriteoffs.Value = dr(5).ToString().Trim()
            fndcontainer.Value = Convert.ToString(dr("Container_Deposit"))
            '' multicurrency
            fndBaseCurrency.Value = Convert.ToString(dr("CURRENCY_CODE"))
            fndExchangeLoss.Value = Convert.ToString(dr("EXCHANGE_LOSS_ACCOUNT"))
            fndExchangeGain.Value = Convert.ToString(dr("EXCHANGE_GAIN_ACCOUNT"))
            lblCurrencyName.Text = Convert.ToString(dr("currency_name"))
            lblExchangeLossName.Text = Convert.ToString(dr("EXCHANGE_LOSS_ACCOUNT_Name"))
            lblExchangeGainName.Text = Convert.ToString(dr("EXCHANGE_GAIN_ACCOUNT_Name"))
            'richa  Ticket No. BM00000003087 on 08/07/2014
            FndSecurity.Value = Convert.ToString(dr("SECURITY_ACCOUNT"))
            fndBankGuarantee.Value = Convert.ToString(dr("BANK_GUARANTEE"))
            fndAccount1.Value = Convert.ToString(dr("ACCOUNT1"))
            fndAccount2.Value = Convert.ToString(dr("ACCOUNT2"))
            TxtForeignBankCharges.Value = Convert.ToString(dr("Foreign_Bank_Charges_Account"))
            TxtBankChargesOther.Value = Convert.ToString(dr("Bank_Charges_Other_Account"))
            lblForeignBankCharges.Text = Convert.ToString(dr("Foreign_Bank_Charges_Account_Name"))
            lblBankChargesOther.Text = Convert.ToString(dr("Bank_Charges_Other_Account_Name"))
            fndCreateSecurity.Value = Convert.ToString(dr("CREATE_SECURITY_ACCOUNT"))
            lblSecurityName.Text = Convert.ToString(dr("SECURITY_ACCOUNT_NAME"))
            lblBankGuaranteeName.Text = Convert.ToString(dr("BANK_GUARANTEE_NAME"))
            lblAccount1Name.Text = Convert.ToString(dr("ACCOUNT1_NAME"))
            lblAccount2Name.Text = Convert.ToString(dr("ACCOUNT2_NAME"))
            lblCreateSecurityName.Text = Convert.ToString(dr("CREATE_SECURITY_ACCOUNT_NAME"))
            FndPenaltyCharges.Value = Convert.ToString(dr("Penalty_Charges_Account"))
            lblPenaltyCharges.Text = Convert.ToString(dr("Penalty_Charges_Account_Name"))
            '---------richa Code Ends------------------------------------

            txtgsoc.Value = clsCommon.myCstr(dr("GSOC_Acct"))
            txtgsoc_name.Text = clsCommon.myCstr(dr("gsoc_name"))
            txtconsignmnt.Value = clsCommon.myCstr(dr("Consignment_Acct"))
            txtcongnmnt_name.Text = clsCommon.myCstr(dr("consgnmnt_name"))
            txtgain.Value = clsCommon.myCstr(dr("Gain_Acct"))
            txtgian_name.Text = clsCommon.myCstr(dr("gain_name"))
            txtloss.Value = clsCommon.myCstr(dr("Loss_Acct"))
            txtloss_name.Text = clsCommon.myCstr(dr("loss_name"))
            '' end multicurrency
            txtSubsidyAccount.Value = clsCommon.myCstr(dr("SubSidy_Account"))
            txtSubsidy.Text = clsCommon.myCstr(dr("SubSidy_Account_Desc"))

            fndLeakageDeduction.Value = clsCommon.myCstr(dr("Leakage_Deduction"))
            txtLeakageDed.Text = clsCommon.myCstr(dr("Leakage_Deduction_Desc"))
            fndCustomerOpeningClearingAC.Value = clsCommon.myCstr(dr("Customer_Opening_Clearing_AC"))
            lblCustomerOpeningClearingAC.Text = clsCommon.myCstr(dr("Customer_Opening_Clearing_AC_Desc"))
            fndCustomerSecurityOpeningClearingAC.Value = clsCommon.myCstr(dr("Customer_Security_Opening_Clearing_AC"))
            lblCustomerSecurityOpeningClearingAC.Text = clsCommon.myCstr(dr("Customer_Security_Opening_Clearing_AC_Desc"))

            rdbtnsave.Text = "Update"
            rdbtndelete.Enabled = True

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub accountsetcode_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadData()
    End Sub
    Sub LoadData()
        Dim str As String
        Dim query As String = "select Cust_Account from tspl_customer_account_set where cust_account='" + fndaccountsetcode.Value + "'"
        'dr = connectSql.RunSqlReturnDR(query)
        'Dim str As String
        'While dr.Read()
        '    str = dr(0).ToString()
        'End While
        str = clsDBFuncationality.getSingleValue(query)
        If str <> "" Then
            funfill()
            fndaccountsetcode.Enabled = True
            rdbtnsave.Text = "Update"
            rdbtndelete.Enabled = True
            rdbtnsave.Enabled = True
            rdtxtdescription.Enabled = True
            fndadvance.Enabled = True
            fndrecieptdiscount.Enabled = True
            fndrecisvablecontrol.Enabled = True
            fndwriteoffs.Enabled = True
        Else
            rdbtnsave.Text = "Save"
            rdtxtdescription.Text = ""
            fndadvance.Value = ""
            fndrecieptdiscount.Value = ""
            fndrecisvablecontrol.Value = ""
            fndwriteoffs.Value = ""
            rdtxtrecievablecontrol.Text = ""
            rdtxtrecieptdicount.Text = ""
            rdtxtadvance.Text = ""
            rdtxtwriteoff.Text = ""
            rdbtndelete.Enabled = False
            rdbtnsave.Enabled = True
            rdtxtdescription.Enabled = True
            fndadvance.Enabled = True
            fndrecieptdiscount.Enabled = True
            fndrecisvablecontrol.Enabled = True
            fndwriteoffs.Enabled = True
            fndcontainer.Value = String.Empty

            'richa  Ticket No. BM00000003087 on 08/07/2014

            FndSecurity.Value = ""
            lblSecurityName.Text = ""
            fndCreateSecurity.Value = ""
            lblCreateSecurityName.Text = ""
            fndAccount1.Value = ""
            lblAccount1Name.Text = ""
            fndAccount2.Value = ""
            lblAccount2Name.Text = ""
            fndBankGuarantee.Value = ""
            lblBankGuaranteeName.Text = ""
            ''richa agarwal
            TxtForeignBankCharges.Value = ""
            lblForeignBankCharges.Text = ""
            TxtBankChargesOther.Value = ""
            lblBankChargesOther.Text = ""
            FndPenaltyCharges.Value = ""
            lblPenaltyCharges.Text = ""
            '---------richa Code Ends------------------------------------


        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If


    End Sub
    'This function is used for reset all Controls.
    Public Sub funreset()
        fndaccountsetcode.Value = ""
        fndaccountsetcode.Enabled = True
        rdtxtdescription.Text = ""
        fndadvance.Value = ""
        fndrecieptdiscount.Value = ""
        fndrecisvablecontrol.Value = ""
        fndwriteoffs.Value = ""
        rdtxtrecievablecontrol.Text = ""
        rdtxtrecieptdicount.Text = ""
        rdtxtadvance.Text = ""
        rdtxtwriteoff.Text = ""
        txtcontainer.Text = ""
        rdtxtdescription.Enabled = True
        rdtxtadvance.ReadOnly = True
        rdtxtrecieptdicount.ReadOnly = True
        rdtxtrecievablecontrol.ReadOnly = True
        fndadvance.Enabled = True
        fndrecieptdiscount.Enabled = True
        fndwriteoffs.Enabled = True
        fndrecisvablecontrol.Enabled = True
        rdbtnsave.Enabled = True
        rdbtnsave.Text = "Save"
        rdbtndelete.Enabled = False
        rdbtnclose.Enabled = True
        fndcontainer.Value = String.Empty
        fndaccountsetcode.MyReadOnly = False
        fndLeakageDeduction.Value = ""
        txtLeakageDed.Text = ""
        txtSubsidyAccount.Value = ""
        txtSubsidy.Text = ""
        '' multicurrency
        fndBaseCurrency.Value = Nothing
        lblCurrencyName.Text = ""
        fndExchangeLoss.Value = Nothing
        lblExchangeLossName.Text = ""
        fndExchangeGain.Value = Nothing
        lblExchangeGainName.Text = ""
        'richa  Ticket No. BM00000003087 on 08/07/2014
        FndSecurity.Value = Nothing
        lblSecurityName.Text = ""
        fndCreateSecurity.Value = Nothing
        lblCreateSecurityName.Text = ""
        fndAccount1.Value = Nothing
        lblAccount1Name.Text = ""
        fndAccount2.Value = Nothing
        lblAccount2Name.Text = ""
        fndBankGuarantee.Value = Nothing
        lblBankGuaranteeName.Text = ""
        ''richa agarwal
        TxtForeignBankCharges.Value = Nothing
        lblForeignBankCharges.Text = ""
        TxtBankChargesOther.Value = Nothing
        lblBankChargesOther.Text = ""
        FndPenaltyCharges.Value = Nothing
        lblPenaltyCharges.Text = ""
        '---------richa Code Ends------------------------------------

        '' end multicurrency

        SetDataBaseGrid()

        txtcongnmnt_name.Text = ""
        txtconsignmnt.Value = ""
        txtgsoc.Value = ""
        txtgsoc_name.Text = ""
        txtgain.Value = ""
        txtgian_name.Text = ""
        txtloss.Value = ""
        txtloss_name.Text = ""
        fndCustomerOpeningClearingAC.Value = Nothing
        lblCustomerOpeningClearingAC.Text = ""
        fndCustomerSecurityOpeningClearingAC.Value = Nothing
        lblCustomerSecurityOpeningClearingAC.Text = ""

    End Sub

    Private Sub fndaccountsetcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndrecisvablecontrol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndrecieptdiscount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndadvance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndwriteoffs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub rdmenuexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexport.Click
        'richa  Ticket No. BM00000003087 on 08/07/2014
        'Dim query As String = "select cust_account as 'Customer Account',cust_acct_desc as 'Customer Account Description',receivable_control_acct as 'Receivable Control Account',receipts_discount_acct as 'Receipts Discount_Account',Advance_acct as 'Advance Account',Write_offs as 'Write Off' , Container_Deposit as 'Container Deposite' from tspl_customer_account_set"
        Dim query As String = "select cust_account as 'Customer Account',cust_acct_desc as 'Customer Account Description',receivable_control_acct as 'Debtor Control',receipts_discount_acct as 'Receipt Discount',Advance_acct as 'Advance Account',Write_offs as 'Write Offs' , Container_Deposit as 'Container Deposite',SECURITY_ACCOUNT as 'Security Account',CREATE_SECURITY_ACCOUNT as 'Create Security Account',BANK_GUARANTEE as 'Bank Guarantee' ,ACCOUNT1 as 'Refrigerator Security',ACCOUNT2 as 'Other Security',GSOC_Acct,Consignment_Acct,Gain_Acct,Loss_Acct,Foreign_Bank_Charges_Account as [Foreign Bank Charges],Bank_Charges_Other_Account as [Bank Charges Other], Customer_Opening_Clearing_AC as [Customer Opening Clearing AC],Customer_Security_Opening_Clearing_AC as [Customer Security Opening Clearing AC]  from tspl_customer_account_set"

        '---------richa Code Ends------------------------------------
        ListImpExpColumnsMandatory = New List(Of String)({"Customer Account", "Debtor Control", "Receipt Discount", "Write Offs", "Security Account", "Create Security Account", "Bank Guarantee", "Refrigerator Security", "Other Security"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Customer Account"})
        transportSql.ExporttoExcel(query, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub rdmenuexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexit.Click
        Me.Close()
    End Sub
    Dim iscommited As Boolean = False
    Private Sub rdmenuimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuimport.Click
        Dim dgv As New RadGridView
        Dim NullVAlue As String = Nothing
        Me.Controls.Add(dgv)
        'richa  Ticket No. BM00000003087 on 08/07/2014
        'If transportSql.importExcel(dgv, "Customer Account", "Customer Account Description", "Receivable Control Account", "Receipts Discount_Account", "Advance Account", "Write Off", "Container Deposite") Then
        If transportSql.importExcel(dgv, "Customer Account", "Customer Account Description", "Debtor Control", "Receipt Discount", "Advance Account", "Write Offs", "Container Deposite", "Security Account", "Create Security Account", "Bank Guarantee", "Refrigerator Security", "Other Security", "GSOC_Acct", "Consignment_Acct", "Gain_Acct", "Loss_Acct", "Foreign Bank Charges", "Bank Charges Other", "Customer Opening Clearing AC", "Customer Security Opening Clearing AC") Then
            Dim linno As Integer = 0
            '---------richa Code Ends------------------------------------
            Dim trans As SqlTransaction = Nothing
            Try
                'connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()

                For Each dgrv As GridViewRowInfo In dgv.Rows
                    linno += 1
                    Dim strcustmaster As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                    Dim strcustaccountdisc As String = clsCommon.myCstr(dgrv.Cells(1).Value)
                    Dim strreceivablecontrol As String = clsCommon.myCstr(dgrv.Cells(2).Value)
                    '' Anubhooti 14-Nov-2014
                    If clsCommon.myLen(strreceivablecontrol) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strreceivablecontrol + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled debtor control (" & strreceivablecontrol & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strreceivablecontrol + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled debtor control (" & strreceivablecontrol & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim strrecieptdiscount As String = clsCommon.myCstr(dgrv.Cells(3).Value)
                    '' Anubhooti 14-Nov-2014
                    If clsCommon.myLen(strrecieptdiscount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strrecieptdiscount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled receipt discount (" & strrecieptdiscount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strrecieptdiscount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled receipt discount (" & strrecieptdiscount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim stradvanceaccount As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                    '' Anubhooti 14-Nov-2014
                    If clsCommon.myLen(stradvanceaccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + stradvanceaccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled advance account (" & stradvanceaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + stradvanceaccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled advance account (" & stradvanceaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''

                    Dim strwriteoff As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                    '' Anubhooti 14-Nov-2014
                    If clsCommon.myLen(strwriteoff) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strwriteoff + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled write offs (" & strwriteoff & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strwriteoff + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled write offs (" & strwriteoff & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''

                    Dim ContainerDeposite As String = clsCommon.myCstr(dgrv.Cells(6).Value)
                    '' Anubhooti 14-Nov-2014
                    If clsCommon.myLen(ContainerDeposite) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + ContainerDeposite + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled container deposite (" & ContainerDeposite & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + ContainerDeposite + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled container deposite (" & ContainerDeposite & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    ''
                    Dim Datee As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                    'richa  Ticket No. BM00000003087 on 08/07/2014
                    Dim Debtor As String = clsCommon.myCstr(dgrv.Cells("Debtor Control").Value)
                    Dim ReceiptDiscount As String = clsCommon.myCstr(dgrv.Cells("Receipt Discount").Value)
                    Dim write As String = clsCommon.myCstr(dgrv.Cells("Write Offs").Value)

                    Dim Securityaccount As String = clsCommon.myCstr(dgrv.Cells("Security Account").Value)
                    Dim CreateSecurityaccount As String = clsCommon.myCstr(dgrv.Cells("Create Security Account").Value)
                    Dim BankGuaranteeaccount As String = clsCommon.myCstr(dgrv.Cells("Bank Guarantee").Value)
                    Dim account1 As String = clsCommon.myCstr(dgrv.Cells("Refrigerator Security").Value)
                    Dim account2 As String = clsCommon.myCstr(dgrv.Cells("Other Security").Value)
                    Dim ForeignBankCharges As String = clsCommon.myCstr(dgrv.Cells("Foreign Bank Charges").Value)
                    Dim BankChargesOther As String = clsCommon.myCstr(dgrv.Cells("Bank Charges Other").Value)

                    ''==========Parteek Ticket No : BM00000010386
                    Dim GSOC_Acct As String = clsCommon.myCstr(dgrv.Cells("GSOC_Acct").Value)
                    Dim Consignment_Acct As String = clsCommon.myCstr(dgrv.Cells("Consignment_Acct").Value)
                    Dim Gain_Acct As String = clsCommon.myCstr(dgrv.Cells("Gain_Acct").Value)
                    Dim Loss_Acct As String = clsCommon.myCstr(dgrv.Cells("Loss_Acct").Value)


                    If String.IsNullOrEmpty(Debtor) Or Debtor.Length > 50 Then
                        Throw New Exception("Debtor Control Length cannot be greater than 50 or blank")
                        trans.Rollback()
                    End If

                    If String.IsNullOrEmpty(ReceiptDiscount) Or ReceiptDiscount.Length > 50 Then
                        Throw New Exception("Receipt Discount Length cannot be greater than 50 or blank")
                        trans.Rollback()
                    End If

                    If String.IsNullOrEmpty(write) Or write.Length > 50 Then
                        Throw New Exception("Write Offs Length cannot be greater than 50 or blank")
                        trans.Rollback()
                    End If

                    If String.IsNullOrEmpty(Securityaccount) Or Securityaccount.Length > 50 Then
                        Throw New Exception("Security Account Length cannot be greater than 50 or blank")
                        trans.Rollback()
                    End If

                    If String.IsNullOrEmpty(CreateSecurityaccount) Or CreateSecurityaccount.Length > 50 Then
                        Throw New Exception("Create Security Account Length cannot be greater than 50  or blank")
                        trans.Rollback()
                    End If
                    If String.IsNullOrEmpty(BankGuaranteeaccount) Or BankGuaranteeaccount.Length > 50 Then
                        Throw New Exception("Bank Guarantee Account Length cannot be greater than 50  or blank")
                        trans.Rollback()
                    End If
                    If String.IsNullOrEmpty(account1) Or account1.Length > 50 Then
                        Throw New Exception("Refrigerator Security Length cannot be greater than 50  or blank")
                        trans.Rollback()
                    End If
                    If String.IsNullOrEmpty(account2) Or account2.Length > 50 Then
                        Throw New Exception("Other Security Length cannot be greater than 50  or blank")
                        trans.Rollback()
                    End If
                    If ForeignBankCharges.Length > 50 Then
                        Throw New Exception("Foreign Bank Charges Length cannot be greater than 50")
                        trans.Rollback()
                    End If
                    If BankChargesOther.Length > 50 Then
                        Throw New Exception("Bank Charges Other Length cannot be greater than 50")
                        trans.Rollback()
                    End If
                    ''==========Added by Parteek Ticket No : BM00000010386
                    'If clsCommon.myLen(GSOC_Acct) <= 0 Then
                    '    Throw New Exception("GSOC_Acct cannot be blank")
                    '    trans.Rollback()
                    'End If
                    'If clsCommon.myLen(Consignment_Acct) <= 0 Then
                    '    Throw New Exception("Consignment_Acct cannot be blank")
                    '    trans.Rollback()
                    'End If
                    'If clsCommon.myLen(Gain_Acct) <= 0 Then
                    '    Throw New Exception("Gain_Acct cannot be blank")
                    '    trans.Rollback()
                    'End If
                    'If clsCommon.myLen(Loss_Acct) <= 0 Then
                    '    Throw New Exception("Loss_Acct cannot be blank")
                    '    trans.Rollback()
                    'End If

                    If clsCommon.myLen(Debtor) > 0 Then
                        If CheckAccountCode(Debtor, trans) = False Then
                            Exit Sub
                        End If
                    End If
                    ''============End
                    If clsCommon.myLen(ReceiptDiscount) > 0 Then
                        If CheckAccountCode(ReceiptDiscount, trans) = False Then
                            Exit Sub
                        End If

                    End If
                    If clsCommon.myLen(stradvanceaccount) > 0 Then
                        If CheckAccountCode(stradvanceaccount, trans) = False Then
                            Exit Sub
                        End If
                    End If
                    If clsCommon.myLen(write) > 0 Then
                        If CheckAccountCode(write, trans) = False Then
                            Exit Sub
                        End If

                    End If
                    If clsCommon.myLen(ContainerDeposite) > 0 Then
                        If CheckAccountCode(ContainerDeposite, trans) = False Then
                            Exit Sub
                        End If
                    End If
                    If clsCommon.myLen(Securityaccount) > 0 Then
                        If CheckAccountCode(Securityaccount, trans) = False Then
                            Exit Sub
                        End If
                        '' Anubhooti 14-Nov-2014
                        'If clsCommon.myLen(Securityaccount) > 0 Then
                        'Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Securityaccount + "'"
                        'Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        'If check <= 0 Then
                        '    Throw New Exception("Filled security account (" & Securityaccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        'End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Securityaccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled security account (" & Securityaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        'End If
                        ''
                    End If
                    If clsCommon.myLen(CreateSecurityaccount) > 0 Then
                        If CheckAccountCode(CreateSecurityaccount, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + CreateSecurityaccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled create security account (" & CreateSecurityaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If clsCommon.myLen(BankGuaranteeaccount) > 0 Then
                        If CheckAccountCode(BankGuaranteeaccount, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + BankGuaranteeaccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled bank guarantee account (" & BankGuaranteeaccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If clsCommon.myLen(account1) > 0 Then
                        If CheckAccountCode(account1, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + account1 + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Refrigerator Security (" & account1 & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    If clsCommon.myLen(account2) > 0 Then
                        If CheckAccountCode(account2, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + account2 + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Other Security (" & account2 & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(ForeignBankCharges) > 0 Then
                        If CheckAccountCode(ForeignBankCharges, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + ForeignBankCharges + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Foreign Bank Charges (" & ForeignBankCharges & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If clsCommon.myLen(BankChargesOther) > 0 Then
                        If CheckAccountCode(BankChargesOther, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + BankChargesOther + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Bank Charges Other (" & BankChargesOther & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    '---------richa Code Ends------------------------------------

                    Dim gsoc As String = clsCommon.myCstr(dgrv.Cells("GSOC_Acct").Value)
                    If clsCommon.myLen(gsoc) > 0 Then
                        If CheckAccountCode(gsoc, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + gsoc + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled GSOC_Acct (" & gsoc & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If

                    Dim consignment As String = clsCommon.myCstr(dgrv.Cells("Consignment_Acct").Value)
                    If clsCommon.myLen(consignment) > 0 Then
                        If CheckAccountCode(consignment, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry2 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + consignment + "' AND ControlAccount ='Y'"
                        Dim check2 As Integer = clsDBFuncationality.getSingleValue(qry2, trans)
                        If check2 <= 0 Then
                            Throw New Exception("Filled Consignment_Acct (" & consignment & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If

                    Dim gain As String = clsCommon.myCstr(dgrv.Cells("Gain_Acct").Value)
                    If clsCommon.myLen(gain) > 0 Then
                        If CheckAccountCode(gain, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry3 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + gain + "' AND ControlAccount ='Y'"
                        Dim check3 As Integer = clsDBFuncationality.getSingleValue(qry3, trans)
                        If check3 <= 0 Then
                            Throw New Exception("Filled Gain_Acct (" & gain & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If

                    Dim loss As String = clsCommon.myCstr(dgrv.Cells("Loss_Acct").Value)
                    If clsCommon.myLen(loss) > 0 Then
                        If CheckAccountCode(loss, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry4 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + loss + "' AND ControlAccount ='Y'"
                        Dim check4 As Integer = clsDBFuncationality.getSingleValue(qry4, trans)
                        If check4 <= 0 Then
                            Throw New Exception("Filled Loss_Acct (" & loss & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If
                    Dim CustomerOpeningclearingAC As String = clsCommon.myCstr(dgrv.Cells("Customer Opening Clearing AC").Value)
                    If clsCommon.myLen(CustomerOpeningclearingAC) > 0 Then
                        If CheckAccountCode(CustomerOpeningclearingAC, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry5 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + CustomerOpeningclearingAC + "' AND ControlAccount ='Y'"
                        Dim check5 As Integer = clsDBFuncationality.getSingleValue(qry5, trans)
                        If check5 <= 0 Then
                            Throw New Exception("Filled Customer Opening clearing AC (" & CustomerOpeningclearingAC & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    Dim CustomerSecurityOpeningClearingAC As String = clsCommon.myCstr(dgrv.Cells("Customer Security Opening Clearing AC").Value)
                    If clsCommon.myLen(CustomerSecurityOpeningClearingAC) > 0 Then
                        If CheckAccountCode(CustomerSecurityOpeningClearingAC, trans) = False Then
                            Exit Sub
                        End If
                        Dim qry6 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + CustomerSecurityOpeningClearingAC + "' AND ControlAccount ='Y'"
                        Dim check6 As Integer = clsDBFuncationality.getSingleValue(qry6, trans)
                        If check6 <= 0 Then
                            Throw New Exception("Filled Customer Security Opening Clearing AC (" & CustomerSecurityOpeningClearingAC & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    '"Customer Opening Clearing AC", "Customer Security Opening Clearing AC"
                    'Ticket No-  TEC/12/07/19-000942 
                    Dim sql1 As String = "select count(*) from tspl_customer_account_set  where cust_account='" + strcustmaster + "'"
                    Dim i As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql1, trans))
                    If (i = 0) Then
                        'richa  Ticket No. BM00000003087 on 08/07/2014
                        'Dim query As String = "insert into tspl_customer_account_set values('" + strcustmaster + "','" + strcustaccountdisc + "','" + strreceivablecontrol + "','" + strrecieptdiscount + "','" + stradvanceaccount + "','" + strwriteoff + "','" + userCode + "','" + Datee + "','" + userCode + "','" + Datee + "','" + companyCode + "', '" + ContainerDeposite + "')"
                        'Dim query As String = "insert into tspl_customer_account_set(Cust_Account , Cust_Acct_Desc , Receivable_Control_acct , Receipts_Discount_acct, Advance_acct , Write_Offs , Created_By , Created_Date , Modify_By , Modify_Date , Comp_Code , Container_Deposit , CURRENCY_CODE , EXCHANGE_LOSS_ACCOUNT , EXCHANGE_GAIN_ACCOUNT , SECURITY_ACCOUNT ,CREATE_SECURITY_ACCOUNT,BANK_GUARANTEE , ACCOUNT1 , ACCOUNT2, GSOC_Acct ,Consignment_Acct, Gain_Acct, Loss_Acct , Foreign_Bank_Charges_Account , Bank_Charges_Other_Account) values('" + strcustmaster + "','" + strcustaccountdisc + "','" + strreceivablecontrol + "','" + strrecieptdiscount + "','" + stradvanceaccount + "','" + strwriteoff + "','" + userCode + "','" + Datee + "','" + userCode + "','" + Datee + "','" + companyCode + "', '" + ContainerDeposite + "',Null,Null,Null,'" + Securityaccount + "','" + CreateSecurityaccount + "','" + BankGuaranteeaccount + "','" + account1 + "','" + account2 + "', " + IIf(clsCommon.myLen(gsoc) > 0, "'" + gsoc + "'", "null") + " ," + IIf(clsCommon.myLen(consignment) > 0, "'" + consignment + "'", "null") + " ," + IIf(clsCommon.myLen(gain) > 0, "'" + gain + "'", "null") + " ," + IIf(clsCommon.myLen(loss) > 0, "'" + loss + "'", "null") + "  ,'" + ForeignBankCharges + "','" + BankChargesOther + "')"
                        'Dim query As String = "insert into tspl_customer_account_set(Cust_Account , Cust_Acct_Desc , Receivable_Control_acct , Receipts_Discount_acct, Advance_acct , Write_Offs , Created_By , Created_Date , Modify_By , Modify_Date , Comp_Code , Container_Deposit , CURRENCY_CODE , EXCHANGE_LOSS_ACCOUNT , EXCHANGE_GAIN_ACCOUNT , SECURITY_ACCOUNT ,CREATE_SECURITY_ACCOUNT,BANK_GUARANTEE , ACCOUNT1 , ACCOUNT2, GSOC_Acct ,Consignment_Acct, Gain_Acct, Loss_Acct , Foreign_Bank_Charges_Account , Bank_Charges_Other_Account) values('" + strcustmaster + "','" + strcustaccountdisc + "','" + strreceivablecontrol + "','" + strrecieptdiscount + "','" + stradvanceaccount + "','" + strwriteoff + "','" + userCode + "','" + Datee + "','" + userCode + "','" + Datee + "','" + companyCode + "', '" + ContainerDeposite + "',Null,Null,Null,'" + Securityaccount + "','" + CreateSecurityaccount + "','" + BankGuaranteeaccount + "','" + account1 + "','" + account2 + "', " + IIf(clsCommon.myLen(gsoc) > 0, "'" + gsoc + "'", "null") + " ," + IIf(clsCommon.myLen(consignment) > 0, "'" + consignment + "'", "null") + " ," + IIf(clsCommon.myLen(gain) > 0, "'" + gain + "'", "null") + " ," + IIf(clsCommon.myLen(loss) > 0, "'" + loss + "'", "null") + "," + IIf(clsCommon.myLen(ForeignBankCharges) > 0, "'" + ForeignBankCharges + "'", "null") + "," + IIf(clsCommon.myLen(BankChargesOther) > 0, "'" + BankChargesOther + "'", "null") + ")"
                        Dim query As String = "insert into tspl_customer_account_set(Cust_Account , Cust_Acct_Desc , Receivable_Control_acct , Receipts_Discount_acct, Advance_acct , Write_Offs , Created_By , Created_Date , Modify_By , Modify_Date , Comp_Code , Container_Deposit , CURRENCY_CODE , EXCHANGE_LOSS_ACCOUNT , EXCHANGE_GAIN_ACCOUNT , SECURITY_ACCOUNT ,CREATE_SECURITY_ACCOUNT,BANK_GUARANTEE , ACCOUNT1 , ACCOUNT2, GSOC_Acct ,Consignment_Acct, Gain_Acct, Loss_Acct , Foreign_Bank_Charges_Account , Bank_Charges_Other_Account) values('" + strcustmaster + "','" + strcustaccountdisc + "','" + strreceivablecontrol + "','" + strrecieptdiscount + "','" + stradvanceaccount + "','" + strwriteoff + "','" + userCode + "','" + Datee + "','" + userCode + "','" + Datee + "','" + companyCode + "', '" + ContainerDeposite + "'," + IIf(objCommonVar.IsMultiCurrencyCompany = False, "'" + objCommonVar.BaseCurrencyCode + "'", "null") + ",Null,Null,'" + Securityaccount + "','" + CreateSecurityaccount + "','" + BankGuaranteeaccount + "','" + account1 + "','" + account2 + "', " + IIf(clsCommon.myLen(gsoc) > 0, "'" + gsoc + "'", "null") + " ," + IIf(clsCommon.myLen(consignment) > 0, "'" + consignment + "'", "null") + " ," + IIf(clsCommon.myLen(gain) > 0, "'" + gain + "'", "null") + " ," + IIf(clsCommon.myLen(loss) > 0, "'" + loss + "'", "null") + "," + IIf(clsCommon.myLen(ForeignBankCharges) > 0, "'" + ForeignBankCharges + "'", "null") + "," + IIf(clsCommon.myLen(BankChargesOther) > 0, "'" + BankChargesOther + "'", "null") + ")"
                        '---------richa Code Ends------------------------------------
                        clsDBFuncationality.ExecuteNonQuery(query, trans)
                    Else

                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strcustmaster, "TSPL_CUSTOMER_ACCOUNT_SET", "cust_account", trans)
                        'richa  Ticket No. BM00000003087 on 08/07/2014
                        'Dim query1 As String = "update tspl_customer_account_set set cust_acct_desc='" + strcustaccountdisc + "',receivable_control_acct='" + strreceivablecontrol + "',receipts_discount_acct='" + strrecieptdiscount + "',Advance_acct='" + stradvanceaccount + "',Write_offs='" + strwriteoff + "', Container_Deposit='" + ContainerDeposite + "' where cust_account='" + strcustmaster + "'"
                        'Dim query1 As String = "update tspl_customer_account_set set cust_acct_desc='" + strcustaccountdisc + "',receivable_control_acct='" + strreceivablecontrol + "',receipts_discount_acct='" + strrecieptdiscount + "',Advance_acct='" + stradvanceaccount + "',Write_offs='" + strwriteoff + "', Container_Deposit='" + ContainerDeposite + "',SECURITY_ACCOUNT='" + Securityaccount + "',CREATE_SECURITY_ACCOUNT='" + CreateSecurityaccount + "',BANK_GUARANTEE='" + BankGuaranteeaccount + "',ACCOUNT1='" + account1 + "',ACCOUNT2='" + account2 + "',GSOC_Acct= ( CASE WHEN " & clsCommon.myLen(gsoc) & ">0 THEN '" + gsoc + "' ELSE NULL END),Consignment_Acct= ( CASE WHEN " & clsCommon.myLen(consignment) & ">0 THEN '" + consignment + "' ELSE NULL END),Gain_Acct= ( CASE WHEN " & clsCommon.myLen(gain) & ">0 THEN '" + gain + "' ELSE NULL END),Loss_Acct=( CASE WHEN " & clsCommon.myLen(loss) & ">0 THEN '" + loss + "' ELSE NULL END),Foreign_Bank_Charges_Account='" + ForeignBankCharges + "',Bank_Charges_Other_Account='" + BankChargesOther + "' where cust_account='" + strcustmaster + "'"
                        Dim query1 As String = "update tspl_customer_account_set set cust_acct_desc='" + strcustaccountdisc + "',receivable_control_acct='" + strreceivablecontrol + "',receipts_discount_acct='" + strrecieptdiscount + "',Advance_acct='" + stradvanceaccount + "',Write_offs='" + strwriteoff + "', Container_Deposit='" + ContainerDeposite + "',SECURITY_ACCOUNT='" + Securityaccount + "',CREATE_SECURITY_ACCOUNT='" + CreateSecurityaccount + "',BANK_GUARANTEE='" + BankGuaranteeaccount + "',ACCOUNT1='" + account1 + "',ACCOUNT2='" + account2 + "',GSOC_Acct= ( CASE WHEN " & clsCommon.myLen(gsoc) & ">0 THEN '" + gsoc + "' ELSE NULL END),Consignment_Acct= ( CASE WHEN " & clsCommon.myLen(consignment) & ">0 THEN '" + consignment + "' ELSE NULL END),Gain_Acct= ( CASE WHEN " & clsCommon.myLen(gain) & ">0 THEN '" + gain + "' ELSE NULL END),Loss_Acct=( CASE WHEN " & clsCommon.myLen(loss) & ">0 THEN '" + loss + "' ELSE NULL END),Foreign_Bank_Charges_Account= ( CASE WHEN " & clsCommon.myLen(ForeignBankCharges) & ">0 THEN '" + ForeignBankCharges + "' ELSE NULL END) ,Bank_Charges_Other_Account= ( CASE WHEN " & clsCommon.myLen(BankChargesOther) & ">0 THEN '" + BankChargesOther + "' ELSE NULL END) where cust_account='" + strcustmaster + "'"
                        '---------richa Code Ends------------------------------------
                        clsDBFuncationality.ExecuteNonQuery(query1, trans)

                    End If
                    ' Ticket No : TEC/02/11/18-000359 By Prabhakar 
                    If clsCommon.myLen(CustomerOpeningclearingAC) > 0 Then
                        Dim qryClearing As String = " update tspl_customer_account_set set Customer_Opening_Clearing_AC = '" + CustomerOpeningclearingAC + "' where cust_account='" + strcustmaster + "' "
                        clsDBFuncationality.ExecuteNonQuery(qryClearing, trans)
                    End If
                    If clsCommon.myLen(CustomerSecurityOpeningClearingAC) > 0 Then
                        Dim qrySecurity As String = " update tspl_customer_account_set set Customer_Security_Opening_Clearing_AC = '" + CustomerSecurityOpeningClearingAC + "' where cust_account='" + strcustmaster + "' "
                        clsDBFuncationality.ExecuteNonQuery(qrySecurity, trans)
                    End If
                Next
                Try
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)

                Catch ex As Exception

                End Try

            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub fndrecieptdiscount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''fndrecieptdiscount.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
        'fndrecieptdiscount.Query = clsERPFuncationality.glaccountquery
        'fndrecieptdiscount.ConnectionString = connectSql.SqlCon()
        'fndrecieptdiscount.Caption = "Reciept Discount Account"
        'fndrecieptdiscount.ValueToSelect = "Account_Code"
        'fndrecieptdiscount.ValueToSelect1 = "Description"

    End Sub

    Private Sub fndadvance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ''fndadvance.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
        'fndadvance.Query = clsERPFuncationality.glaccountquery
        'fndadvance.ConnectionString = connectSql.SqlCon()
        'fndadvance.Caption = "Advance Account"
        'fndadvance.ValueToSelect = "Account_Code"
        'fndadvance.ValueToSelect1 = "Description"
    End Sub

    Private Sub fndwriteoffs_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndwriteoffs.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts"
        'fndwriteoffs.Query = clsERPFuncationality.glaccountquery
        'fndwriteoffs.ConnectionString = connectSql.SqlCon()
        'fndwriteoffs.Caption = "Write Offs Account"
        'fndwriteoffs.ValueToSelect = "Account_Code"
        'fndwriteoffs.ValueToSelect1 = "Description"
    End Sub
    Public Sub receivable_txtchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadRec()
    End Sub
    Sub LoadRec()
        Try
            'Dim query As String = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts where account_code='" + fndrecisvablecontrol.Value + "' "
            'dr = connectSql.RunSqlReturnDR(query)
            'Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            Dim query As String = "select account_code as [Account Code] from tspl_gl_accounts where account_code='" + fndrecisvablecontrol.Value + "' "
            Dim str As String = clsDBFuncationality.getSingleValue(query)

            If str <> "" Then
                recievablecontrol_funfill()
            Else
                rdtxtrecievablecontrol.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub receiptdiscount_txtchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        LoadRecDis()
    End Sub
    Sub LoadRecDis()
        Try
            Dim query1 As String = "select account_code as [Account Code] from tspl_gl_accounts where account_code='" + fndrecieptdiscount.Value + "' "
            'dr = connectSql.RunSqlReturnDR(query1)
            Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            str = clsDBFuncationality.getSingleValue(query1)
            If str <> "" Then
                receiptdiscount_funfill()
            Else
                rdtxtrecieptdicount.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub advance_txtchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim query2 As String = "select account_code as [Account Code] from tspl_gl_accounts where account_code='" + fndadvance.Value + "' "
            'dr = connectSql.RunSqlReturnDR(query2)
            Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            str = clsDBFuncationality.getSingleValue(query2)

            If str <> "" Then
                advance_funfill()
            Else
                rdtxtadvance.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try



    End Sub
    Public Sub writeoff_txtchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadWrite()
    End Sub
    Sub LoadWrite()
        Try
            Dim query3 As String = "select account_code as [Account Code] from tspl_gl_accounts where account_code='" + fndwriteoffs.Value + "' "
            'dr = connectSql.RunSqlReturnDR(query3)
            Dim str As String
            'While dr.Read()
            '    str = dr(0).ToString()
            'End While
            str = clsDBFuncationality.getSingleValue(query3)
            If str <> "" Then
                writeoff_funfill()
            Else
                rdtxtwriteoff.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try


    End Sub
    'This function is used retrive data in recievable finder.
    Public Sub recievablecontrol_funfill()
        Dim query As String = "Select description from tspl_gl_accounts where account_code ='" + fndrecisvablecontrol.Value + "'"
        'dr = connectSql.RunSqlReturnDR(query)
        'While dr.Read()
        '    rdtxtrecievablecontrol.Text = dr(1).ToString()
        'End While
        rdtxtrecievablecontrol.Text = clsDBFuncationality.getSingleValue(query)
    End Sub
    'This function is used retrive data in Reciept discount finder.
    Public Sub receiptdiscount_funfill()

        'Dim query1 As String = "Select account_code,description from tspl_gl_accounts where account_code ='" + fndrecieptdiscount.Value + "'"
        'dr = connectSql.RunSqlReturnDR(query1)
        'While dr.Read()
        '    rdtxtrecieptdicount.Text = dr(1).ToString()
        'End While

        Dim query1 As String = "Select description from tspl_gl_accounts where account_code ='" + fndrecieptdiscount.Value + "'"
        rdtxtrecieptdicount.Text = clsDBFuncationality.getSingleValue(query1)
    End Sub
    'This function is used retrive data in advance finder.
    Public Sub advance_funfill()

        'Dim query2 As String = "Select account_code,description from tspl_gl_accounts where account_code ='" + fndadvance.Value + "'"
        'dr = connectSql.RunSqlReturnDR(query2)
        'While dr.Read()
        '    rdtxtadvance.Text = dr(1).ToString()
        'End While

        Dim query2 As String = "Select description from tspl_gl_accounts where account_code ='" + fndadvance.Value + "'"
        rdtxtadvance.Text = clsDBFuncationality.getSingleValue(query2)

    End Sub
    'This function is used retrive data in write off finder.
    Public Sub writeoff_funfill()
        'Dim query3 As String = "Select account_code,description from tspl_gl_accounts where account_code ='" + fndwriteoffs.Value + "'"
        'dr = connectSql.RunSqlReturnDR(query3)
        'While dr.Read()
        '    rdtxtwriteoff.Text = dr(1).ToString()
        'End While
        Dim query3 As String = "Select description from tspl_gl_accounts where account_code ='" + fndwriteoffs.Value + "'"

        rdtxtwriteoff.Text = clsDBFuncationality.getSingleValue(query3)


    End Sub
    Private Sub fndrecisvablecontrol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndrecisvablecontrol.Query = "select account_code as [Account Code],description as [Description]from tspl_gl_accounts "
        'fndrecisvablecontrol.Query = clsERPFuncationality.glaccountquery
        'fndrecisvablecontrol.ConnectionString = connectSql.SqlCon()
        'fndrecisvablecontrol.Caption = "Recisvable Control Account"
        'fndrecisvablecontrol.ValueToSelect = "Account_Code"
        'fndrecisvablecontrol.ValueToSelect1 = "Description"
    End Sub
    Public Sub recievable_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndrecisvablecontrol.Value = "" Then

        Else
            Try
                Dim query As String = "select account_code  from Tspl_gl_Accounts where account_code='" + fndrecisvablecontrol.Value + "'"
                Dim strvalue As String

                'dr = connectSql.RunSqlReturnDR(query)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While

                strvalue = clsDBFuncationality.getSingleValue(query)

                If strvalue <> "" Then

                Else : query = ""

                    rdtxtrecievablecontrol.Text = ""
                    common.clsCommon.MyMessageBoxShow("This Recievable Control Account does not exist")
                    fndrecisvablecontrol.Value = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub recieptdiscount_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndrecieptdiscount.Value = "" Then

        Else
            Try
                Dim query As String = "select account_code from Tspl_gl_Accounts where account_code='" + fndrecieptdiscount.Value + "'"
                Dim strvalue As String

                'dr = connectSql.RunSqlReturnDR(query)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While

                strvalue = clsDBFuncationality.getSingleValue(query)

                If strvalue <> "" Then

                Else : query = ""

                    rdtxtrecieptdicount.Text = ""
                    common.clsCommon.MyMessageBoxShow("This Reciept Discount Account does not exist")
                    fndrecieptdiscount.Value = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub advance_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndadvance.Value = "" Then

        Else
            Try
                Dim query As String = "select account_code  from Tspl_gl_Accounts where account_code='" + fndadvance.Value + "'"
                Dim strvalue As String

                'dr = connectSql.RunSqlReturnDR(query)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While

                strvalue = clsDBFuncationality.getSingleValue(query)

                If strvalue <> "" Then

                Else : query = ""

                    rdtxtadvance.Text = ""
                    common.clsCommon.MyMessageBoxShow("This Advance Account does not exist")
                    fndadvance.Value = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    Public Sub writeoffs_leave()
        If fndwriteoffs.Value = "" Then

        Else
            Try
                Dim query As String = "select account_code  from Tspl_gl_Accounts where account_code='" + fndwriteoffs.Value + "'"
                Dim strvalue As String

                'dr = connectSql.RunSqlReturnDR(query)
                'While dr.Read()
                '    strvalue = dr(0).ToString()
                'End While

                strvalue = clsDBFuncationality.getSingleValue(query)

                If strvalue <> "" Then

                Else : query = ""

                    rdtxtwriteoff.Text = ""
                    common.clsCommon.MyMessageBoxShow("This Write Offs Account does not exist")
                    fndwriteoffs.Value = ""


                End If


            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    'priti added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CUST-ACC-SET"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            rdbtnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            rdbtndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Function GetReplecateCompaniesDataBase() As List(Of String)
        Dim arrDBName As New List(Of String)
        arrDBName.Add(objCommonVar.CurrDatabase)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 and Comp_Code not in ('" + objCommonVar.CurrentCompanyCode + "')"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub


    Private Sub fndcontainer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndcontainer.Query = clsERPFuncationality.glaccountquery
        'fndcontainer.ConnectionString = connectSql.SqlCon()
        'fndcontainer.Caption = "Recisvable Control Account"
        'fndcontainer.ValueToSelect = "Account_Code"
        'fndcontainer.ValueToSelect1 = "Description"

    End Sub

    Private Sub frmCustomerAccountSet_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            fundelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()

        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()

        End If
    End Sub


    Private Sub fndrecisvablecontrol__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndrecisvablecontrol._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndrecisvablecontrol.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", " ControlAccount ='Y' ", fndrecisvablecontrol.Value, "", isButtonClicked)
        rdtxtrecievablecontrol.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndrecisvablecontrol.Value + "' ")
        LoadRec()
    End Sub

    Private Sub fndrecieptdiscount__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndrecieptdiscount._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts"
        fndrecieptdiscount.Value = clsCommon.ShowSelectForm("ACC_CODfnd", qry, "AccountCode", " ControlAccount ='Y' ", fndrecieptdiscount.Value, "", isButtonClicked)
        rdtxtrecieptdicount.Text = clsDBFuncationality.getSingleValue("select description as [Description] from tspl_gl_accounts where  account_code='" + fndrecieptdiscount.Value + "'")
        LoadRecDis()
    End Sub

    Private Sub fndadvance__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndadvance._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts"
        fndadvance.Value = clsCommon.ShowSelectForm("GL_ACCCTfnd", qry, "AccountCode", " ControlAccount ='Y' ", fndadvance.Value, "", isButtonClicked)
        rdtxtadvance.Text = clsDBFuncationality.getSingleValue("select  description from tspl_gl_accounts where account_code='" + fndadvance.Value + "'")
    End Sub

    Private Sub fndwriteoffs__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndwriteoffs._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts"
        fndwriteoffs.Value = clsCommon.ShowSelectForm("GL_ACTid", qry, "AccountCode", " ControlAccount ='Y' ", fndadvance.Value, "", isButtonClicked)
        rdtxtwriteoff.Text = clsDBFuncationality.getSingleValue("select  description from tspl_gl_accounts where account_code='" + fndwriteoffs.Value + "'")
        LoadWrite()
    End Sub

    Private Sub fndcontainer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcontainer._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts"
        fndcontainer.Value = clsCommon.ShowSelectForm("GL_ACCUNTIDN", qry, "AccountCode", " ControlAccount ='Y' ", fndcontainer.Value, "", isButtonClicked)
        txtcontainer.Text = clsDBFuncationality.getSingleValue("select  description from tspl_gl_accounts where account_code='" + fndcontainer.Value + "'")
    End Sub

    Private Sub fndaccountsetcode__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndaccountsetcode._MYValidating
        Dim str As String = "select count(*) from Tspl_Customer_account_Set   where  cust_account ='" + fndaccountsetcode.Value + "' "

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 Then
            fndaccountsetcode.MyReadOnly = False
        Else
            fndaccountsetcode.MyReadOnly = True
        End If
        If fndaccountsetcode.MyReadOnly OrElse isButtonClicked Then

            'Dim QRY As String = "Select cust_account as [CustomerAccount],cust_acct_Desc as [Account Description]from Tspl_Customer_account_Set"
            'fndaccountsetcode.Value = clsCommon.ShowSelectForm("CUST_ACCNTIDFRM", QRY, "CustomerAccount", "", fndaccountsetcode.Value, "", isButtonClicked)
            fndaccountsetcode.Value = clsCustomerAccountSet.getFinder("", fndaccountsetcode.Value, isButtonClicked)
            rdtxtdescription.Text = clsDBFuncationality.getSingleValue("Select  cust_acct_Desc  from Tspl_Customer_account_Set where cust_account='" + fndaccountsetcode.Value + "'")
            LoadData()
            LoadRec()
            LoadRecDis()
            LoadWrite()
            txtcontainer.Text = clsDBFuncationality.getSingleValue("select  description from tspl_gl_accounts where account_code='" + fndcontainer.Value + "'")
            rdtxtadvance.Text = clsDBFuncationality.getSingleValue("select  description from tspl_gl_accounts where account_code='" + fndadvance.Value + "'")
        End If
    End Sub

    Private Sub fndaccountsetcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccountsetcode._MYNavigator
        Dim qst As String = "Select cust_account as [Customer Account],cust_acct_Desc as [Account Description]from Tspl_Customer_account_Set where 2=2"
        Select Case NavType
            Case NavigatorType.Current
                qst += " and Tspl_Customer_account_Set .cust_account in ('" + fndaccountsetcode.Value + "')"
            Case NavigatorType.Next
                qst += " and Tspl_Customer_account_Set .cust_account in (select min(cust_account ) from Tspl_Customer_account_Set where cust_account  >'" + fndaccountsetcode.Value + "')"
            Case NavigatorType.First
                qst += " and Tspl_Customer_account_Set .cust_account in (select MIN(cust_account ) from Tspl_Customer_account_Set)"

            Case NavigatorType.Last
                qst += " and Tspl_Customer_account_Set .cust_account in (select Max(cust_account ) from Tspl_Customer_account_Set)"
            Case NavigatorType.Previous
                qst += " and Tspl_Customer_account_Set .cust_account in (select Max(cust_account ) from Tspl_Customer_account_Set where cust_account  <'" + fndaccountsetcode.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndaccountsetcode.Value = clsCommon.myCstr(dt.Rows(0)("Customer Account"))
            rdtxtdescription.Text = clsCommon.myCstr(dt.Rows(0)("Account Description"))
        End If
        LoadData()
        LoadRec()
        LoadRecDis()
        LoadWrite()
        txtcontainer.Text = clsDBFuncationality.getSingleValue("select  description from tspl_gl_accounts where account_code='" + fndcontainer.Value + "'")
        rdtxtadvance.Text = clsDBFuncationality.getSingleValue("select  description from tspl_gl_accounts where account_code='" + fndadvance.Value + "'")
    End Sub

    Private Sub fndBaseCurrency__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBaseCurrency._MYValidating
        Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        fndBaseCurrency.Value = clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", fndBaseCurrency.Value, "CURRENCY_CODE", isButtonClicked)
        lblCurrencyName.Text = clsDBFuncationality.getSingleValue("select currency_name from TSPL_CURRENCY_MASTER where currency_code='" + fndBaseCurrency.Value + "' ")
    End Sub

    Private Sub fndWIP__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndExchangeLoss._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndExchangeLoss.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", " ControlAccount ='Y' ", fndExchangeLoss.Value, "", isButtonClicked)
        lblExchangeLossName.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndExchangeLoss.Value + "' ")
    End Sub

    Private Sub fndExchangeGain__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndExchangeGain._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndExchangeGain.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", " ControlAccount ='Y' ", fndExchangeGain.Value, "", isButtonClicked)
        lblExchangeGainName.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndExchangeGain.Value + "' ")
    End Sub

    'richa  Ticket No. BM00000003087 on 08/07/2014

    Private Sub FndSecurity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndSecurity._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        FndSecurity.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", " ControlAccount ='Y' ", FndSecurity.Value, "", isButtonClicked)
        lblSecurityName.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + FndSecurity.Value + "' ")
    End Sub

    Private Sub fndCreateSecurity__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCreateSecurity._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndCreateSecurity.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", " ControlAccount ='Y' ", fndCreateSecurity.Value, "", isButtonClicked)
        lblCreateSecurityName.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndCreateSecurity.Value + "' ")
    End Sub

    Private Sub fndBankGuarantee__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndBankGuarantee._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndBankGuarantee.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", " ControlAccount ='Y' ", fndBankGuarantee.Value, "", isButtonClicked)
        lblBankGuaranteeName.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndBankGuarantee.Value + "' ")
    End Sub

    Private Sub fndAccount1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndAccount1._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndAccount1.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", " ControlAccount ='Y' ", fndAccount1.Value, "", isButtonClicked)
        lblAccount1Name.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndAccount1.Value + "' ")
    End Sub

    Private Sub fndAccount2__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndAccount2._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndAccount2.Value = clsCommon.ShowSelectForm("REC_CONfnd", qry, "AccountCode", " ControlAccount ='Y' ", fndAccount2.Value, "", isButtonClicked)
        lblAccount2Name.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndAccount2.Value + "' ")
    End Sub

    Private Function CheckAccountCode(ByVal AccountCode As String, ByVal trans As SqlTransaction) As Boolean

        Try
            If clsCommon.myLen(AccountCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from tspl_gl_accounts where account_code='" + AccountCode + "' ", trans) > 0 Then
                Return True
            Else
                Throw New Exception("Account Code is Invalid")
                Return False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    '---------richa Code Ends------------------------------------

    Private Function CheckControlAccount(ByVal AccountType As String, ByVal AccountCode As String) As Boolean
        Try
            If clsCommon.myLen(AccountCode) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + AccountCode + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled " + AccountType + " (" & AccountCode & ") must be control account.", Me.Text)
                    'Throw New Exception("Filled " + AccountType + " (" & AccountCode & ") must be control account.")
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

#Region "New a/c Add By Monika"
    Private Sub txtgsoc__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtgsoc._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        txtgsoc.Value = clsCommon.ShowSelectForm("GSOCfnd", qry, "AccountCode", " ControlAccount ='Y' ", txtgsoc.Value, "", isButtonClicked)
        txtgsoc_name.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtgsoc.Value + "' ")
    End Sub

    Private Sub txtconsignmnt__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtconsignmnt._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        txtconsignmnt.Value = clsCommon.ShowSelectForm("CNSfnd", qry, "AccountCode", " ControlAccount ='Y' ", txtconsignmnt.Value, "", isButtonClicked)
        txtcongnmnt_name.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtconsignmnt.Value + "' ")
    End Sub

    Private Sub txtgain__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtgain._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        txtgain.Value = clsCommon.ShowSelectForm("GAINfnd", qry, "AccountCode", " ControlAccount ='Y'", txtgain.Value, "", isButtonClicked)
        txtgian_name.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtgain.Value + "' ")
    End Sub

    Private Sub txtloss__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtloss._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        txtloss.Value = clsCommon.ShowSelectForm("LOSSfnd", qry, "AccountCode", " ControlAccount ='Y' ", txtloss.Value, "", isButtonClicked)
        txtloss_name.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtloss.Value + "' ")
    End Sub
#End Region

    Private Sub TxtForeignBankCharges__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtForeignBankCharges._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        TxtForeignBankCharges.Value = clsCommon.ShowSelectForm("For_bc", qry, "AccountCode", " ControlAccount ='Y' ", TxtForeignBankCharges.Value, "", isButtonClicked)
        lblForeignBankCharges.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + TxtForeignBankCharges.Value + "' ")
    End Sub

    Private Sub TxtBankChargesOther__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtBankChargesOther._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        TxtBankChargesOther.Value = clsCommon.ShowSelectForm("BankCO", qry, "AccountCode", " ControlAccount ='Y' ", TxtBankChargesOther.Value, "", isButtonClicked)
        lblBankChargesOther.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + TxtBankChargesOther.Value + "' ")
    End Sub
    Private Sub txtSubsidyAccount__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubsidyAccount._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        txtSubsidyAccount.Value = clsCommon.ShowSelectForm("SubSidyCO", qry, "AccountCode", " ControlAccount ='Y' ", txtSubsidyAccount.Value, "", isButtonClicked)
        txtSubsidy.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + txtSubsidyAccount.Value + "' ")
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndaccountsetcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Account Set", Me.Text)
                Exit Sub
            End If
            clsERPFuncationality.ShowHistoryData(fndaccountsetcode.Value, "Cust_Account", "TSPL_CUSTOMER_ACCOUNT_SET")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub FndPenaltyCharges__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndPenaltyCharges._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        FndPenaltyCharges.Value = clsCommon.ShowSelectForm("BankCO", qry, "AccountCode", " ControlAccount ='Y' ", FndPenaltyCharges.Value, "", isButtonClicked)
        lblPenaltyCharges.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + FndPenaltyCharges.Value + "' ")

    End Sub
    Private Sub fndLeakageDeduction__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLeakageDeduction._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndLeakageDeduction.Value = clsCommon.ShowSelectForm("Leakage", qry, "AccountCode", " ControlAccount ='Y' ", fndLeakageDeduction.Value, "", isButtonClicked)
        txtLeakageDed.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndLeakageDeduction.Value + "' ")

    End Sub
  
    Private Sub fndCustomerOpeningClearingAC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustomerOpeningClearingAC._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndCustomerOpeningClearingAC.Value = clsCommon.ShowSelectForm("Clearing", qry, "AccountCode", " ControlAccount ='Y' ", fndCustomerOpeningClearingAC.Value, "", isButtonClicked)
        lblCustomerOpeningClearingAC.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndCustomerOpeningClearingAC.Value + "' ")
    End Sub

    Private Sub fndCustomerSecurityOpeningClearingAC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCustomerSecurityOpeningClearingAC._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndCustomerSecurityOpeningClearingAC.Value = clsCommon.ShowSelectForm("Security", qry, "AccountCode", " ControlAccount ='Y' ", fndCustomerSecurityOpeningClearingAC.Value, "", isButtonClicked)
        lblCustomerSecurityOpeningClearingAC.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndCustomerSecurityOpeningClearingAC.Value + "' ")
    End Sub
End Class
