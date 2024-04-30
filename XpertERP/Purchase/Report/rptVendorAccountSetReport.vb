'Sanjay Ticket- BHA/04/10/18-000596 ,Date  04/Oct/2018  Client- Bharat Dairy
'Ticket No- BHA/09/10/18-000607 Grid column and crystal report column caption change as per Vendor Account set screen.
'Ticket No- BHA/12/10/18-000621 Import,Update,Drill Down
Imports common
Imports System.IO
Public Class rptVendorAccountSetReport
    Inherits FrmMainTranScreen
    Dim inputs As String() = {}
    Dim ButtonToolTip As New ToolTip()
    Dim strQry As String = ""
    Dim dt As DataTable
    Dim blnRefresh As Boolean = False
    Dim qry As String = ""
    Dim arrlist As New ArrayList()
    Dim whrcls As String = ""
    Public isInsideLoadData As Boolean = False
    Const colVendorCode As String = "colVendorCode"
    Const colVendorName As String = "colVendorName"
    Const colVendorGroupCode As String = "colVendorGroupCode"
    Const colVendorGroupName As String = "colVendorGroupName"
    Const colAccountSetCode As String = "colAccountSetCode"
    Const colAccountSetName As String = "colAccountSetName"
    Const colPayableAccount As String = "colPayableAccount"
    Const colPayableAccountName As String = "colPayableAccountName"
    Const colDiscountAccount As String = "colDiscountAccount"
    Const colDiscountAccountName As String = "colDiscountAccountName"
    Const colAdvanceAccount As String = "colAdvanceAccount"
    Const colAdvanceAccountName As String = "colAdvanceAccountName"
    Const colExchangeLossAccount As String = "colExchangeLossAccount"
    Const colExchangeLossAccountName As String = "colExchangeLossAccountName"
    Const colExchangeGainAccount As String = "colExchangeGainAccount"
    Const colExchangeGainAccountName As String = "colExchangeGainAccountName"
    Const colCommissionAccount As String = "colCommissionAccount"
    Const colCommissionAccountName As String = "colCommissionAccountName"
    Const colIncentiveAccount As String = "colIncentiveAccount"
    Const colIncentiveAccountName As String = "colIncentiveAccountName"
    Const colSecurityAccount As String = "colSecurityAccount"
    Const colSecurityAccountName As String = "colSecurityAccountName"
    Const colHeadLoadAccount As String = "colHeadLoadAccount"
    Const colHeadLoadAccountName As String = "colHeadLoadAccountName"
    Const colOwnAssetAccount As String = "colOwnAssetAccount"
    Const colOwnAssetAccountName As String = "colOwnAssetAccountName"
    Const colDeductionAccount As String = "colDeductionAccount"
    Const colDeductionAccountName As String = "colDeductionAccountName"
    Const colAdvanceAgainstSalary As String = "colAdvanceAgainstSalary"
    Const colAdvanceAgainstSalaryName As String = "colAdvanceAgainstSalaryName"
    Const colEmployeeSalary As String = "colEmployeeSalary"
    Const colEmployeeSalaryName As String = "colEmployeeSalaryName"
    Const colAdvanceAgainstTravelling As String = "colAdvanceAgainstTravelling"
    Const colAdvanceAgainstTravellingName As String = "colAdvanceAgainstTravellingName"
    Const colAdvanceAgainstImprest As String = "colAdvanceAgainstImprest"
    Const colAdvanceAgainstImprestName As String = "colAdvanceAgainstImprestName"
    Const colFreightProvisionAccount As String = "colFreightProvisionAccount"
    Const colFreightProvisionAccountName As String = "colFreightProvisionAccountName"
    Const colHandlingChargeAccount As String = "colHandlingChargeAccount"
    Const colHandlingChargeAccountName As String = "colHandlingChargeAccountName"
    Const colRoundoffAccount As String = "colRoundoffAccount"
    Const colRoundoffAccountName As String = "colRoundoffAccountName"
    Const colShortExcessAccount As String = "colShortExcessAccount"
    Const colShortExcessAccountName As String = "colShortExcessAccountName"

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        blnRefresh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        Print()
    End Sub


    Private Sub Print()

        Try
            isInsideLoadData = False
            Dim dtPrint As New DataTable
            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            strWhrClause = " 1=1 "
            If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                strWhrClause += " and TSPL_VENDOR_GROUP.Ven_Group_Code in (" + clsCommon.GetMulcallString(txtVendorGroup.arrValueMember) + ")  "
            End If

            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                strWhrClause += " and TSPL_VENDOR_MASTER.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")  "
            End If

            If txtAccountSet.arrValueMember IsNot Nothing AndAlso txtAccountSet.arrValueMember.Count > 0 Then
                strWhrClause += " and TSPL_VENDOR_ACCOUNT_SET.acct_set_code in (" + clsCommon.GetMulcallString(txtAccountSet.arrValueMember) + ")  "
            End If

            Dim query As String = "SELECT isnull(xx.AccountSetCode,'Account Set Code') as AccountSetCode " & _
            ",isnull(xx.AccountSetName,'Account Set Name') as AccountSetName " & _
            ",isnull(xx.PayableAccountName,'Payable Account Name') as PayableAccountName " & _
            ",isnull(xx.DiscountAccountName,'Discount Account Name') as DiscountAccountName " & _
            ",isnull(xx.AdvanceAccountName,'Advance Account Name') as AdvanceAccountName " & _
            ",isnull(xx.ExchangeLossAccountName,'Exchange Loss Account Name') as ExchangeLossAccountName " & _
            ",isnull(xx.ExchangeGainAccountName,'Exchange Gain Account Name') as ExchangeGainAccountName " & _
            ",isnull(xx.CommissionAccountName,'Commission Account Name') as CommissionAccountName " & _
            ",isnull(xx.IncentiveAccountName,'Incentive Account Name') as IncentiveAccountName " & _
            ",isnull(xx.SecurityAccountName,'Security Account Name') as SecurityAccountName " & _
            ",isnull(xx.HeadLoadAccountName,'Head Load Account Name') as HeadLoadAccountName " & _
            ",isnull(xx.OwnAssetAccountName,'Own Asset Account Name') as OwnAssetAccountName " & _
            ",isnull(xx.DeductionAccountName,'Deduction Account Name') as DeductionAccountName " & _
            ",isnull(xx.AdvanceAgainstSalaryName,'Advance Against Salary Name') as AdvanceAgainstSalaryName " & _
            ",isnull(xx.EmployeeSalaryName,'Employee Salary Name') as EmployeeSalaryName " & _
            ",isnull(xx.AdvanceAgainstTravellingName,'Advance Against Travelling Name') as AdvanceAgainstTravellingName " & _
            ",isnull(xx.AdvanceAgainstImprestName,'Advance Against Imprest Name') as AdvanceAgainstImprestName " & _
            ",isnull(xx.FreightProvisionAccountName,'Freight Provision Account Name') as FreightProvisionAccountName " & _
            ",isnull(xx.HandlingChargeAccountName,'Handling Charge Account Name') as HandlingChargeAccountName " & _
            ",isnull(xx.RoundoffAccountName,'Round off Account Name') as RoundoffAccountName " & _
            ",isnull(xx.ShortExcessAccountName,'Short Excess Account Name') as ShortExcessAccountName " & _
            ",xx.[Vendor Code],xx.[Vendor Name],xx.[Vendor Group Code],xx.[Vendor Group Name],xx.[Account Set Code],xx.[Account Set Name],xx.[Payable Account] " & _
            ",xx.[Payable Account Name],xx.[Discount Account],xx.[Discount Account Name], " & _
            "xx.[Advance Account]  ,xx.[Advance Account Name]  ,xx.[Exchange Loss Account],xx.[Exchange Loss Account Name],xx.[Exchange Gain Account],xx.[Exchange Gain Account Name] ,xx.[Commission Account],xx.[Commission Account Name],xx.[Incentive Account] ,xx.[Incentive Account Name],xx.[Security Account],xx.[Security Account Name] ,xx.[Head Load Account],xx.[Head Load Account Name] ,xx.[Own Asset Account],xx.[Own Asset Account Name],xx.[Deduction Account],xx.[Deduction Account Name] ,xx.[Advance Against Salary],xx.[Advance Against Salary Name],xx.[Employee Salary],xx.[Employee Salary Name],xx.[Advance Against Travelling],xx.[Advance Against Travelling Name],xx.[Advance Against Imprest],xx.[Advance Against Imprest Name],xx.[Freight Provision Account],xx.[Freight Provision Account Name],xx.[Handling Charge Account],xx.[Handling Charge Account Name],xx.[Round off Account],xx.[Round off Account Name],xx.[ShortExcess Account],xx.[ShortExcess Account Name] " & _
            " FROM (Select (select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='rdlblaccountsetcode') as AccountSetCode , " & _
            "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='rdlbldescription') as AccountSetName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='lkPayablesControl') as PayableAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='rdlblpurchasediscount') as DiscountAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='RadLabel5') as AdvanceAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='lblExchangeLoss') as ExchangeLossAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='lblExchangeGain') as ExchangeGainAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='LblCommission') as CommissionAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='LblIncentive') as IncentiveAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='MyLabel2') as SecurityAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='MyLabel7') as HeadLoadAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='MyLabel6') as OwnAssetAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='MyLabel3') as DeductionAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='lblAdvlanceAgainstSalary') as AdvanceAgainstSalaryName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='MyLabel1') as EmployeeSalaryName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='MyLabel4') as AdvanceAgainstTravellingName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='MyLabel5') as AdvanceAgainstImprestName, " & _
              "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='lkFreightProvision') as FreightProvisionAccountName, " & _
              "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='lkHandlingCharge') as HandlingChargeAccountName, " & _
              "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='lkRoundOff') as RoundoffAccountName, " & _
             "(select TSPL_CLIENT_FORM_LABEL_SETTING.NEW_LABEL_NAME as Account_Set_Code from   " & _
             "TSPL_CLIENT_FORM_LABEL_SETTING where FORM_NAME='VEN-ACCT-SET' and TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID='MyLabel8') as ShortExcessAccountName, " & _
            " TSPL_VENDOR_MASTER.Vendor_Code as [Vendor Code],TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name] " & _
            ",TSPL_VENDOR_MASTER.Vendor_Group_Code as [Vendor Group Code],TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc as [Vendor Group Name] , TSPL_VENDOR_ACCOUNT_SET.acct_set_code as [Account Set Code],TSPL_VENDOR_ACCOUNT_SET.acct_set_desc as [Account Set Name] " & _
            " ,payable_account as [Payable Account],Payable_ac.description as [Payable Account Name] " & _
            " ,discount_account as [Discount Account],Discount_ac.description as [Discount Account Name],advance_account as [Advance Account] " & _
            " ,Advance_ac.description as [Advance Account Name] " & _
            " ,TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT as [Exchange Loss Account],gl1.description as [Exchange Loss Account Name],TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT as [Exchange Gain Account],gl2.description as [Exchange Gain Account Name] ,TSPL_VENDOR_ACCOUNT_SET.Commission_ACCOUNT as [Commission Account],gl3.description as [Commission Account Name],TSPL_VENDOR_ACCOUNT_SET.Incentive_ACCOUNT as [Incentive Account] ,gl4.description as [Incentive Account Name],Security_Account as [Security Account],gl5.description as [Security Account Name] ,Head_Load_Account as [Head Load Account],gl7.description as [Head Load Account Name] ,Own_Asset_Account as [Own Asset Account],gl6.description as [Own Asset Account Name],Deduction_Account as [Deduction Account],gl8.description as [Deduction Account Name] ,tspl_vendor_account_set.Advance_Against_Salary as [Advance Against Salary],gl9.description as [Advance Against Salary Name],tspl_vendor_account_set.Employee_Salary as [Employee Salary],gl10.description as [Employee Salary Name],tspl_vendor_account_set.Advance_Against_Travelling as [Advance Against Travelling],gl11.description as [Advance Against Travelling Name],tspl_vendor_account_set.Advance_Against_Imprest as [Advance Against Imprest],gl12.description as [Advance Against Imprest Name],TSPL_VENDOR_ACCOUNT_SET.Freight_Provision as [Freight Provision Account],glFreight_account.description as [Freight Provision Account Name],TSPL_VENDOR_ACCOUNT_SET.Handling_Charges as [Handling Charge Account],TabGLHandlingCharge.description as [Handling Charge Account Name],TSPL_VENDOR_ACCOUNT_SET.Round_Off as [Round off Account],TabRoundOff.description as [Round off Account Name],TSPL_VENDOR_ACCOUNT_SET.Short_Excess as [ShortExcess Account],TabShortExcess.description as [ShortExcess Account Name]   from TSPL_VENDOR_MASTER " & _
            " left join TSPL_VENDOR_ACCOUNT_SET on TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code= TSPL_VENDOR_MASTER.Vendor_Account " & _
            " left join TSPL_GL_ACCOUNTS gl1 on TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT=gl1.account_code  left join TSPL_GL_ACCOUNTS gl2 on TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT=gl2.account_code  left join TSPL_GL_ACCOUNTS gl3 on TSPL_VENDOR_ACCOUNT_SET.Commission_ACCOUNT=gl3.account_code  left join TSPL_GL_ACCOUNTS gl4 on TSPL_VENDOR_ACCOUNT_SET.Incentive_ACCOUNT=gl4.account_code  left join TSPL_GL_ACCOUNTS gl5 on TSPL_VENDOR_ACCOUNT_SET.Security_ACCOUNT=gl5.account_code  left join TSPL_GL_ACCOUNTS gl6 on TSPL_VENDOR_ACCOUNT_SET.Own_Asset_ACCOUNT=gl6.account_code  left join TSPL_GL_ACCOUNTS gl7 on TSPL_VENDOR_ACCOUNT_SET.Head_Load_ACCOUNT=gl7.account_code  left join TSPL_GL_ACCOUNTS gl8 on TSPL_VENDOR_ACCOUNT_SET.Deduction_ACCOUNT=gl8.account_code  left join TSPL_GL_ACCOUNTS gl9 on TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Salary=gl9.account_code left join TSPL_GL_ACCOUNTS gl10 on TSPL_VENDOR_ACCOUNT_SET.Employee_Salary=gl10.account_code left join TSPL_GL_ACCOUNTS gl11 on TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Travelling=gl11.account_code left join TSPL_GL_ACCOUNTS gl12 on TSPL_VENDOR_ACCOUNT_SET.Advance_Against_Imprest=gl12.account_code left join TSPL_GL_ACCOUNTS glFreight_account on TSPL_VENDOR_ACCOUNT_SET.Freight_Provision=glFreight_account.account_code  left join TSPL_GL_ACCOUNTS TabGLHandlingCharge on TSPL_VENDOR_ACCOUNT_SET.Handling_Charges=TabGLHandlingCharge.account_code  left join TSPL_GL_ACCOUNTS TabRoundOff on TSPL_VENDOR_ACCOUNT_SET.Round_Off=TabRoundOff.account_code  left join TSPL_GL_ACCOUNTS TabShortExcess on TSPL_VENDOR_ACCOUNT_SET.Short_Excess=TabShortExcess.account_code " & _
            " left join TSPL_GL_ACCOUNTS Payable_ac on TSPL_VENDOR_ACCOUNT_SET.payable_account=Payable_ac.account_code " & _
            " left join TSPL_GL_ACCOUNTS Discount_ac on TSPL_VENDOR_ACCOUNT_SET.discount_account=Discount_ac.account_code " & _
            " left join TSPL_GL_ACCOUNTS Advance_ac on TSPL_VENDOR_ACCOUNT_SET.advance_account=Advance_ac.account_code " & _
            " where "
            query = query + strWhrClause
            query = query + " )xx order by xx.[Account Set Code]"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            Gv1.DataSource = Nothing

            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            dtPrint = dtgv.Copy

            dtgv.Columns.Remove("AccountSetCode")
            dtgv.Columns.Remove("AccountSetName")
            dtgv.Columns.Remove("PayableAccountName")
            dtgv.Columns.Remove("DiscountAccountName")
            dtgv.Columns.Remove("AdvanceAccountName")
            dtgv.Columns.Remove("ExchangeLossAccountName")
            dtgv.Columns.Remove("ExchangeGainAccountName")
            dtgv.Columns.Remove("CommissionAccountName")
            dtgv.Columns.Remove("IncentiveAccountName")
            dtgv.Columns.Remove("SecurityAccountName")
            dtgv.Columns.Remove("HeadLoadAccountName")
            dtgv.Columns.Remove("OwnAssetAccountName")
            dtgv.Columns.Remove("DeductionAccountName")
            dtgv.Columns.Remove("AdvanceAgainstSalaryName")
            dtgv.Columns.Remove("EmployeeSalaryName")
            dtgv.Columns.Remove("AdvanceAgainstTravellingName")
            dtgv.Columns.Remove("AdvanceAgainstImprestName")
            dtgv.Columns.Remove("FreightProvisionAccountName")
            dtgv.Columns.Remove("HandlingChargeAccountName")
            dtgv.Columns.Remove("RoundoffAccountName")
            dtgv.Columns.Remove("ShortExcessAccountName")

            FormatGrid()

            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            Else

                For Each row As DataRow In dtgv.Rows
                    Gv1.Rows.AddNew()
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVendorCode).Value = clsCommon.myCstr(row("Vendor Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVendorName).Value = clsCommon.myCstr(row("Vendor Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVendorGroupCode).Value = clsCommon.myCstr(row("Vendor Group Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colVendorGroupName).Value = clsCommon.myCstr(row("Vendor Group Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAccountSetCode).Value = clsCommon.myCstr(row("Account Set Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAccountSetName).Value = clsCommon.myCstr(row("Account Set Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPayableAccount).Value = clsCommon.myCstr(row("Payable Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPayableAccountName).Value = clsCommon.myCstr(row("Payable Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDiscountAccount).Value = clsCommon.myCstr(row("Discount Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDiscountAccountName).Value = clsCommon.myCstr(row("Discount Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAdvanceAccount).Value = clsCommon.myCstr(row("Advance Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAdvanceAccountName).Value = clsCommon.myCstr(row("Advance Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colExchangeLossAccount).Value = clsCommon.myCstr(row("Exchange Loss Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colExchangeLossAccountName).Value = clsCommon.myCstr(row("Exchange Loss Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colExchangeGainAccount).Value = clsCommon.myCstr(row("Exchange Gain Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colExchangeGainAccountName).Value = clsCommon.myCstr(row("Exchange Gain Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCommissionAccount).Value = clsCommon.myCstr(row("Commission Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCommissionAccountName).Value = clsCommon.myCstr(row("Commission Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colIncentiveAccount).Value = clsCommon.myCstr(row("Incentive Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colIncentiveAccountName).Value = clsCommon.myCstr(row("Incentive Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSecurityAccount).Value = clsCommon.myCstr(row("Security Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSecurityAccountName).Value = clsCommon.myCstr(row("Security Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHeadLoadAccount).Value = clsCommon.myCstr(row("Head Load Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHeadLoadAccountName).Value = clsCommon.myCstr(row("Head Load Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colOwnAssetAccount).Value = clsCommon.myCstr(row("Own Asset Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colOwnAssetAccountName).Value = clsCommon.myCstr(row("Own Asset Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDeductionAccount).Value = clsCommon.myCstr(row("Deduction Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDeductionAccountName).Value = clsCommon.myCstr(row("Deduction Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAdvanceAgainstSalary).Value = clsCommon.myCstr(row("Advance Against Salary").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAdvanceAgainstSalaryName).Value = clsCommon.myCstr(row("Advance Against Salary Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colEmployeeSalary).Value = clsCommon.myCstr(row("Employee Salary").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colEmployeeSalaryName).Value = clsCommon.myCstr(row("Employee Salary Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAdvanceAgainstTravelling).Value = clsCommon.myCstr(row("Advance Against Travelling").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAdvanceAgainstTravellingName).Value = clsCommon.myCstr(row("Advance Against Travelling Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAdvanceAgainstImprest).Value = clsCommon.myCstr(row("Advance Against Imprest").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colAdvanceAgainstImprestName).Value = clsCommon.myCstr(row("Advance Against Imprest Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colFreightProvisionAccount).Value = clsCommon.myCstr(row("Freight Provision Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colFreightProvisionAccountName).Value = clsCommon.myCstr(row("Freight Provision Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHandlingChargeAccount).Value = clsCommon.myCstr(row("Handling Charge Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHandlingChargeAccountName).Value = clsCommon.myCstr(row("Handling Charge Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colRoundoffAccount).Value = clsCommon.myCstr(row("Round off Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colRoundoffAccountName).Value = clsCommon.myCstr(row("Round off Account Name").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colShortExcessAccount).Value = clsCommon.myCstr(row("ShortExcess Account").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colShortExcessAccountName).Value = clsCommon.myCstr(row("ShortExcess Account Name").ToString())
                Next
                isInsideLoadData = True
            End If

            Gv1.BestFitColumns()
            ChangeColumnCaption()
            ReStoreGridLayout()

            If blnRefresh = False Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.Purchase, dtPrint, "rptVendorAccountSet", "Vendor Account Set Report", clsCommon.GETSERVERDATE())
                frmCRV = Nothing
            End If



            RadPageView1.SelectedPage = RadPageViewPage2


            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
            Next
            If chkOnlyview.Checked = True Then
                btnUpdate.Enabled = False
            Else
                btnUpdate.Enabled = True
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub ChangeColumnCaption()
        Try

        Dim StrSql As String = ""
        StrSql = "select TSPL_CLIENT_FORM_LABEL_SETTING.* from  TSPL_CLIENT_FORM_LABEL_SETTING" & _
        " Left Join " & _
        " ( select 'rdlblaccountsetcode' as lname " & _
        " union select 'rdlbldescription' as lname " & _
        " union select 'lkPayablesControl' as lname " & _
        " union select 'rdlblpurchasediscount' as lname " & _
        " union select 'RadLabel5' as lname " & _
        " union select 'lblExchangeLoss' as lname " & _
        " union select 'lblExchangeGain' as lname " & _
        " union select 'LblCommission' as lname " & _
        " union select 'LblIncentive' as lname " & _
        " union select 'MyLabel2' as lname " & _
        " union select 'MyLabel7' as lname " & _
        " union select 'MyLabel6' as lname " & _
        " union select 'MyLabel3' as lname " & _
        " union select 'lblAdvlanceAgainstSalary' as lname " & _
        " union select 'MyLabel1' as lname " & _
        " union select 'MyLabel4' as lname " & _
        " union select 'MyLabel5' as lname " & _
        " union select 'lkFreightProvision' as lname " & _
        " union select 'lkHandlingCharge' as lname " & _
        " union select 'lkRoundOff' as lname " & _
        " union select 'MyLabel8' as lname " & _
        " )tt on tt.lname=TSPL_CLIENT_FORM_LABEL_SETTING.LABEL_ID " & _
        " where FORM_NAME='VEN-ACCT-SET' "
        Dim dt As New DataTable
        dt = clsDBFuncationality.GetDataTable(StrSql)
        For Each row As DataRow In dt.Rows
            If clsCommon.myCstr(row.Item("LABEL_ID")) = "rdlblaccountsetcode" Then
                Gv1.Columns(colAccountSetCode).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "rdlbldescription" Then
                Gv1.Columns(colAccountSetName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "lkPayablesControl" Then
                Gv1.Columns(colPayableAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colPayableAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "rdlblpurchasediscount" Then
                Gv1.Columns(colDiscountAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colDiscountAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "RadLabel5" Then
                Gv1.Columns(colAdvanceAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colAdvanceAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "lblExchangeLoss" Then
                Gv1.Columns(colExchangeLossAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colExchangeLossAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "lblExchangeGain" Then
                Gv1.Columns(colExchangeGainAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colExchangeGainAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "LblCommission" Then
                Gv1.Columns(colCommissionAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colCommissionAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "LblIncentive" Then
                Gv1.Columns(colIncentiveAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colIncentiveAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "MyLabel2" Then
                Gv1.Columns(colSecurityAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colSecurityAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "MyLabel7" Then
                Gv1.Columns(colHeadLoadAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colHeadLoadAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "MyLabel6" Then
                Gv1.Columns(colOwnAssetAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colOwnAssetAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "MyLabel3" Then
                Gv1.Columns(colDeductionAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colDeductionAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "lblAdvlanceAgainstSalary" Then
                Gv1.Columns(colAdvanceAgainstSalary).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colAdvanceAgainstSalaryName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "MyLabel1" Then
                Gv1.Columns(colEmployeeSalary).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colEmployeeSalaryName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "MyLabel4" Then
                Gv1.Columns(colAdvanceAgainstTravelling).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colAdvanceAgainstTravellingName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "MyLabel5" Then
                Gv1.Columns(colAdvanceAgainstImprest).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colAdvanceAgainstImprestName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "lkFreightProvision" Then
                Gv1.Columns(colFreightProvisionAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colFreightProvisionAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "lkHandlingCharge" Then
                Gv1.Columns(colHandlingChargeAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colHandlingChargeAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "lkRoundOff" Then
                Gv1.Columns(colRoundoffAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colRoundoffAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            ElseIf clsCommon.myCstr(row.Item("LABEL_ID")) = "MyLabel8" Then
                Gv1.Columns(colShortExcessAccount).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME"))
                Gv1.Columns(colShortExcessAccountName).HeaderText = clsCommon.myCstr(row.Item("NEW_LABEL_NAME")) + " Name"
            End If
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rptVendorAccountSetReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.R Then
                FunReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
                btnPrint.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rptVendorAccountSetReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R for reset window")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for view report")
        FormatGrid()
        ChangeColumnCaption()
    End Sub

    Private Sub FormatGrid()
        Try

            Dim repoString As New GridViewTextBoxColumn()

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Vendor Code"
        repoString.Name = colVendorCode
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Vendor Name"
        repoString.Name = colVendorName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Vendor Group Code"
        repoString.Name = colVendorGroupCode
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Vendor Group Name"
        repoString.Name = colVendorGroupName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Account Set Code"
        repoString.Name = colAccountSetCode
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Account Set Name"
        repoString.Name = colAccountSetName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Payable Account"
        repoString.Name = colPayableAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Payable Account Name"
        repoString.Name = colPayableAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Discount Account"
        repoString.Name = colDiscountAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Discount Account Name"
        repoString.Name = colDiscountAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Advance Account"
        repoString.Name = colAdvanceAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Advance Account Name"
        repoString.Name = colAdvanceAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Exchange Loss Account"
        repoString.Name = colExchangeLossAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Exchange Loss Account Name"
        repoString.Name = colExchangeLossAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Exchange Gain Account"
        repoString.Name = colExchangeGainAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Exchange Gain Account Name"
        repoString.Name = colExchangeGainAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Commission Account"
        repoString.Name = colCommissionAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Commission Account Name"
        repoString.Name = colCommissionAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Incentive Account"
        repoString.Name = colIncentiveAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Incentive Account Name"
        repoString.Name = colIncentiveAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Security Account"
        repoString.Name = colSecurityAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Security Account Name"
        repoString.Name = colSecurityAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Head Load Account"
        repoString.Name = colHeadLoadAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Head Load Account Name"
        repoString.Name = colHeadLoadAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Own Asset Account"
        repoString.Name = colOwnAssetAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Own Asset Account Name"
        repoString.Name = colOwnAssetAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Deduction Account"
        repoString.Name = colDeductionAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Deduction Account Name"
        repoString.Name = colDeductionAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Advance Against Salary"
        repoString.Name = colAdvanceAgainstSalary
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Advance Against Salary Name"
        repoString.Name = colAdvanceAgainstSalaryName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Employee Salary"
        repoString.Name = colEmployeeSalary
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Employee Salary Name"
        repoString.Name = colEmployeeSalaryName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Advance Against Travelling"
        repoString.Name = colAdvanceAgainstTravelling
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Advance Against Travelling Name"
        repoString.Name = colAdvanceAgainstTravellingName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Advance Against Imprest"
        repoString.Name = colAdvanceAgainstImprest
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Advance Against Imprest Name"
        repoString.Name = colAdvanceAgainstImprestName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Freight Provision Account"
        repoString.Name = colFreightProvisionAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Freight Provision Account Name"
        repoString.Name = colFreightProvisionAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Handling Charge Account"
        repoString.Name = colHandlingChargeAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Handling Charge Account Name"
        repoString.Name = colHandlingChargeAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Round off Account"
        repoString.Name = colRoundoffAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Round off Account Name"
        repoString.Name = colRoundoffAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "ShortExcess Account"
        repoString.Name = colShortExcessAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        Gv1.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "ShortExcess Account Name"
        repoString.Name = colShortExcessAccountName
        repoString.Width = 100
        repoString.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoString)
        Gv1.MasterTemplate.AllowAddNewRow = False
            'Gv1.MasterTemplate.AllowDragToGroup = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGridImport()
        Try

            Gv1.Columns(0).Name = colVendorCode
        Gv1.Columns(0).ReadOnly = True
        Gv1.Columns(1).Name = colVendorName
        Gv1.Columns(1).ReadOnly = True
        Gv1.Columns(2).Name = colVendorGroupCode
        Gv1.Columns(2).ReadOnly = True
        Gv1.Columns(3).Name = colVendorGroupName
        Gv1.Columns(3).ReadOnly = True
        Gv1.Columns(4).Name = colAccountSetCode
        Gv1.Columns(4).ReadOnly = True
        Gv1.Columns(5).Name = colAccountSetName
        Gv1.Columns(5).ReadOnly = True
        Gv1.Columns(6).Name = colPayableAccount
        Gv1.Columns(6).ReadOnly = False
        Gv1.Columns(7).Name = colPayableAccountName
        Gv1.Columns(7).ReadOnly = True
        Gv1.Columns(8).Name = colDiscountAccount
        Gv1.Columns(8).ReadOnly = False
        Gv1.Columns(9).Name = colDiscountAccountName
        Gv1.Columns(9).ReadOnly = True
        Gv1.Columns(10).Name = colAdvanceAccount
        Gv1.Columns(10).ReadOnly = False
        Gv1.Columns(11).Name = colAdvanceAccountName
        Gv1.Columns(11).ReadOnly = True
        Gv1.Columns(12).Name = colExchangeLossAccount
        Gv1.Columns(12).ReadOnly = False
        Gv1.Columns(13).Name = colExchangeLossAccountName
        Gv1.Columns(13).ReadOnly = True
        Gv1.Columns(14).Name = colExchangeGainAccount
        Gv1.Columns(14).ReadOnly = False
        Gv1.Columns(15).Name = colExchangeGainAccountName
        Gv1.Columns(15).ReadOnly = True
        Gv1.Columns(16).Name = colCommissionAccount
        Gv1.Columns(16).ReadOnly = False
        Gv1.Columns(17).Name = colCommissionAccountName
        Gv1.Columns(17).ReadOnly = True
        Gv1.Columns(18).Name = colIncentiveAccount
        Gv1.Columns(18).ReadOnly = False
        Gv1.Columns(19).Name = colIncentiveAccountName
        Gv1.Columns(19).ReadOnly = True
        Gv1.Columns(20).Name = colSecurityAccount
        Gv1.Columns(20).ReadOnly = False
        Gv1.Columns(21).Name = colSecurityAccountName
        Gv1.Columns(21).ReadOnly = True
        Gv1.Columns(22).Name = colHeadLoadAccount
        Gv1.Columns(22).ReadOnly = False
        Gv1.Columns(23).Name = colHeadLoadAccountName
        Gv1.Columns(23).ReadOnly = True
        Gv1.Columns(24).Name = colOwnAssetAccount
        Gv1.Columns(24).ReadOnly = False
        Gv1.Columns(25).Name = colOwnAssetAccountName
        Gv1.Columns(25).ReadOnly = True
        Gv1.Columns(26).Name = colDeductionAccount
        Gv1.Columns(26).ReadOnly = False
        Gv1.Columns(27).Name = colDeductionAccountName
        Gv1.Columns(27).ReadOnly = True
        Gv1.Columns(28).Name = colAdvanceAgainstSalary
        Gv1.Columns(28).ReadOnly = False
        Gv1.Columns(29).Name = colAdvanceAgainstSalaryName
        Gv1.Columns(29).ReadOnly = True
        Gv1.Columns(30).Name = colEmployeeSalary
        Gv1.Columns(30).ReadOnly = False
        Gv1.Columns(31).Name = colEmployeeSalaryName
        Gv1.Columns(31).ReadOnly = True
        Gv1.Columns(32).Name = colAdvanceAgainstTravelling
        Gv1.Columns(32).ReadOnly = False
        Gv1.Columns(33).Name = colAdvanceAgainstTravellingName
        Gv1.Columns(33).ReadOnly = True
        Gv1.Columns(34).Name = colAdvanceAgainstImprest
        Gv1.Columns(34).ReadOnly = False
        Gv1.Columns(35).Name = colAdvanceAgainstImprestName
        Gv1.Columns(35).ReadOnly = True
        Gv1.Columns(36).Name = colFreightProvisionAccount
        Gv1.Columns(36).ReadOnly = False
        Gv1.Columns(37).Name = colFreightProvisionAccountName
        Gv1.Columns(37).ReadOnly = True
        Gv1.Columns(38).Name = colHandlingChargeAccount
        Gv1.Columns(38).ReadOnly = False
        Gv1.Columns(39).Name = colHandlingChargeAccountName
        Gv1.Columns(39).ReadOnly = True
        Gv1.Columns(40).Name = colRoundoffAccount
        Gv1.Columns(40).ReadOnly = False
        Gv1.Columns(41).Name = colRoundoffAccountName
        Gv1.Columns(41).ReadOnly = True
        Gv1.Columns(42).Name = colShortExcessAccount
        Gv1.Columns(42).ReadOnly = False
        Gv1.Columns(43).Name = colShortExcessAccountName
        Gv1.Columns(43).ReadOnly = True

        For i As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(i).Width = 100
            Gv1.Columns(i).BestFit()
        Next
        Gv1.MasterTemplate.AllowAddNewRow = False
            'Gv1.MasterTemplate.AllowDragToGroup = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        FunReset()
    End Sub

    Private Sub FunReset()
        Try
            Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        'Gv1.Columns.Clear()
        txtVendorGroup.arrValueMember = Nothing
        txtVendor.arrValueMember = Nothing
        txtAccountSet.arrValueMember = Nothing
        inputs = Nothing
        isInsideLoadData = False
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Gv1.EnableFiltering = False
        chkOnlyview.Checked = False
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptVendorAccountSetReport & "'"))

            If txtVendorGroup.arrValueMember IsNot Nothing AndAlso txtVendorGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtVendorGroup.arrDispalyMember))
            End If
            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
            End If
            If txtAccountSet.arrValueMember IsNot Nothing AndAlso txtAccountSet.arrValueMember.Count > 0 Then
                arrHeader.Add("Account Set : " + clsCommon.GetMulcallStringWithComma(txtAccountSet.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vendor Account Set Report", Gv1, arrHeader, "Vendor Account Set Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        'Try

        '    If Gv1.Rows.Count > 0 Then
        '        Dim arrHeader As List(Of String) = New List(Of String)()
        '        arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        '        arrHeader.Add("Vendor Account Set Report")

        '        clsCommon.MyExportToExcelGrid("Vendor Account Set Report", Gv1, arrHeader, "Vendor Account Set Report")
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
        Export(EnumExportTo.Excel)
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RbtnSaveLayout_Click(sender As Object, e As EventArgs) Handles RbtnSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RbtnDeleteLayout_Click(sender As Object, e As EventArgs) Handles RbtnDeleteLayout.Click
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        strQry = "select TSPL_VENDOR_MASTER.Vendor_Code as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name  from TSPL_VENDOR_MASTER where 1=1  order by TSPL_VENDOR_MASTER.Vendor_Code"
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorSelector@VendorLedger", strQry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtVendorGroup__My_Click(sender As Object, e As EventArgs) Handles txtVendorGroup._My_Click
        strQry = "select Ven_Group_Code as Code, Group_Desc as Name from TSPL_VENDOR_GROUP order by Ven_Group_Code "
        txtVendorGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorGrpSelector@VendorLedger", strQry, "Code", "Name", txtVendorGroup.arrValueMember, txtVendorGroup.arrDispalyMember)
    End Sub

    Private Sub txtAccountSet__My_Click(sender As Object, e As EventArgs) Handles txtAccountSet._My_Click
        strQry = "select Acct_Set_Code as Code, Acct_Set_Desc as Description from TSPL_VENDOR_ACCOUNT_SET"
        txtAccountSet.arrValueMember = clsCommon.ShowMultipleSelectForm("AcSetSelector@VendorLedger", strQry, "Code", "Description", txtAccountSet.arrValueMember, txtAccountSet.arrDispalyMember)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        blnRefresh = False
        Print()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            'Gv1.Columns.Clear()

            'inputs = ImportInputString()
            ' Dim Strs As List(Of String) = New List(Of String)(inputs)
            Dim myList As New List(Of String)()
            For i As Integer = 0 To Gv1.Columns.Count - 1
                myList.Add(clsCommon.myCstr(Gv1.Columns(i).HeaderText))
            Next



            'inputs = {"Item Code", "Item Description", "Seq No", "Short Description", "Structure Code", "Rack No", "Purchase A/c Set", "Sale A/c Set", "Category", "Sub Category", "UOM", "Item Type", "Type", "Cost", "Tolerence", "Shelf Life", "Standard Purchase Price", "Morning", "Chapter Code", "Category Structure", "Weight_UOM", "Weight_Value", "Is_MRP", "ITF_CODE", "Active", "Is_FreshItem", "Is_Ambient", "Product Type", "Is Purchaseable", "Is Allow QC on Purchase", "Is_CrateType", "Is_CAN_Type", "Used As", "Part_No", "Drawing_No", "Item_Cagetory_Values", "Is_Serial_Item", "Serial_Counter", "Is_Pick_Auto_SrNo", "Tax Exempted", "GL Account", "Account Description", "Warranty Code", "Warranty Applied From", "Alies Name", "UOM1", "UOM2", "UOM3", "UOM4", "UOM5", "UOM6", "UOM7", "UOM8", "UOM9", "UOM10", "Conversion Factor1", "Conversion Factor2", "Conversion Factor3", "Conversion Factor4", "Conversion Factor5", "Conversion Factor6", "Conversion Factor7", "Conversion Factor8", "Conversion Factor9", "Conversion Factor10", "Default UOM1", "Default UOM2", "Default UOM3", "Default UOM4", "Default UOM5", "Default UOM6", "Default UOM7", "Default UOM8", "Default UOM9", "Default UOM10", "Stocking Unit1", "Stocking Unit2", "Stocking Unit3", "Stocking Unit4", "Stocking Unit5", "Stocking Unit6", "Stocking Unit7", "Stocking Unit8", "Stocking Unit9", "Stocking Unit10", "Weight1", "Weight2", "Weight3", "Weight4", "Weight5", "Weight6", "Weight7", "Weight8", "Weight9", "Weight10", "Gross_Weight1", "Gross_Weight2", "Gross_Weight3", "Gross_Weight4", "Gross_Weight5", "Gross_Weight6", "Gross_Weight7", "Gross_Weight8", "Gross_Weight9", "Gross_Weight10", "Item Category Code1", "Item Category Code Description1", "Item Category Code2", "Item Category Code Description2", "Item Category Code3", "Item Category Code Description3", "Item Category Code4", "Item Category Code Description4", "Item Category Code5", "Item Category Code Description5", "Item Category Code6", "Item Category Code Description6", "Item Category Code7", "Item Category Code Description7", "Item Category Code8", "Item Category Code Description8", "Item Category Code9", "Item Category Code Description9", "Item Category Code10", "Item Category Code Description10", "Item Category Code11", "Item Category Code Description11", "Item Category Code12", "Item Category Code Description12", "Item Category Code13", "Item Category Code Description13", "Item Category Code14", "Item Category Code Description14", "Item Category Code15", "Item Category Code Description15", "Item Category Level1", "Item Category Level Desp1", "Item Category Level2", "Item Category Level Desp2", "Item Category Level3", "Item Category Level Desp3", "Item Category Level4", "Item Category Level Desp4", "Item Category Level5", "Item Category Level Desp5", "Item Category Level6", "Item Category Level Desp6", "Item Category Level7", "Item Category Level Desp7", "Item Category Level8", "Item Category Level Desp8", "Item Category Level9", "Item Category Level Desp9", "Item Category Level10", "Item Category Level Desp10", "Item Category Level11", "Item Category Level Desp11", "Item Category Level12", "Item Category Level Desp12", "Item Category Level13", "Item Category Level Desp13", "Item Category Level14", "Item Category Level Desp14", "Item Category Level15", "Item Category Level Desp15", "Is_Rate_Change_OnDairyDispatch", "Is_Scheme_Item", "Distributor_Commission", "CNF_Commission", "CSA_Type", "IsTaxable", "is_Batch_Item", "AllowSRNWithoutShortReject", "HSN Code", "Chilled Freezen"}

            If transportSql.importExcel(Gv1, myList.ToArray()) Then '"Vendor Code", "Vendor Name", "Vendor Group Code", "Vendor Group Name", "Weight UOM", "Weight Value", "Stocking UOM", "Stocking Conversion", "Default UOM", "Default Conversion", "Weight UOM1", "Weight Conversion1", "Weight UOM2", "Weight Conversion2", "Other UOM1", "Other Conversion1", "Other UOM2", "Other Conversion2", "Item Type", "Purchase Account Set", "Sale Account Set", "Batch Wise", "Fresh/Ambient", "Taxable", "MRP Wise", "Fat Rate", "SNF Rate", "Item Cost") Then

                clsCommon.ProgressBarPercentShow()

                ''do sorting of records for easy saving purpose.
                Dim dt As New DataTable()
                dt = Gv1.DataSource()
                dt.DefaultView.Sort = "Account Set Code"
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.DataSource = dt.DefaultView.ToTable()
                ''======================end here========================

                FormatGridImport()
                Gv1.BestFitColumns()
                'For i As Integer = 0 To Gv1.Columns.Count - 1
                '    Gv1.Columns(i).BestFit()
                'Next

                RadPageView1.SelectedPage = RadPageViewPage2
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfered Successfully.", Me.Text)

            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            Dim LineNo As String
            Dim AccountFromGrid As String
            Dim AccountCode As String = ""
            For Each grow As GridViewRowInfo In Gv1.Rows
                AccountFromGrid = ""
                AccountCode = ""
                LineNo = clsCommon.myCstr(grow.Index + 1)

                'Vendor Account Set
                Dim VendorAccountSet_Code As String = ""
                Dim VendorAccountSet As String = clsCommon.myCstr(grow.Cells(colAccountSetCode).Value)
                If VendorAccountSet = "" Then
                    Throw New Exception("Vendor Account Set Code at line '" + LineNo + "' is Blank.")
                Else
                    VendorAccountSet_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code from TSPL_VENDOR_ACCOUNT_SET WHERE TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code='" + VendorAccountSet + "'"))
                    If Not clsCommon.CompairString(VendorAccountSet_Code, VendorAccountSet) = CompairStringResult.Equal Then
                        Throw New Exception("Vendor Account Set Code at line '" + LineNo + "' does not exist.")
                    End If
                End If

                'Payable Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colPayableAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    arrlist = clsERPFuncationality.glaccountqueryForControlAcc(objCommonVar.CurrentUserCode)
                    qry = arrlist.Item(0)
                    whrcls = arrlist.Item(1)
                    If whrcls <> "" Then
                        whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    Else
                        whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    End If
                    whrcls = whrcls + " and Account_Code = '" + AccountFromGrid + "'"

                    qry = qry + " where " + whrcls

                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Payable Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Discount Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colDiscountAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    arrlist = clsERPFuncationality.glaccountqueryForControlAcc(objCommonVar.CurrentUserCode)
                    qry = arrlist.Item(0)
                    whrcls = arrlist.Item(1)
                    If whrcls <> "" Then
                        whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    Else
                        whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    End If
                    whrcls = whrcls + " and Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " where " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))


                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Discount Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Advance Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colAdvanceAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    arrlist = clsERPFuncationality.glaccountqueryForControlAcc(objCommonVar.CurrentUserCode)
                    qry = arrlist.Item(0)
                    whrcls = arrlist.Item(1)
                    If whrcls <> "" Then
                        whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    Else
                        whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    End If
                    whrcls = whrcls + " and Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " where " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))


                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Advance Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'ExchangeLoss Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colExchangeLossAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("ExchangeLoss Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'ExchangeGain Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colExchangeGainAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("ExchangeGain Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Commission Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colCommissionAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Commission Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Incentive Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colIncentiveAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Incentive Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Security Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colSecurityAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Security Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'HeadLoad Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colHeadLoadAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("HeadLoad Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'OwnAsset Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colOwnAssetAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Own Asset Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Deduction Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colDeductionAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Deduction Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Advance Against Salary Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colAdvanceAgainstSalary).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Advance Against Salary Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Employee Salary Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colEmployeeSalary).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Employee Salary Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Advance Against Travelling Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colAdvanceAgainstTravelling).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Advance Against Travelling Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Advance Against Imprest Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colAdvanceAgainstImprest).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Advance Against Imprest Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Freight Provision Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colFreightProvisionAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Freight Provision Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Handling Charge Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colHandlingChargeAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Handling Charge Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Round off Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colRoundoffAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Round off Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If

                'Short Excess Account
                AccountFromGrid = clsCommon.myCstr(grow.Cells(colShortExcessAccount).Value)
                qry = ""
                whrcls = ""
                If AccountFromGrid <> "" Then
                    qry = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where ControlAccount ='Y'"
                    whrcls = whrcls + " Account_Code = '" + AccountFromGrid + "'"
                    qry = qry + " and " + whrcls
                    AccountCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If Not clsCommon.CompairString(AccountCode, AccountFromGrid) = CompairStringResult.Equal Then
                        Throw New Exception("Short Excess Account Code at line '" + LineNo + "' is not valid.")
                    End If
                End If
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try

            If (AllowToSave()) Then

                'Dim qry As String = ""
                ''Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                'Dim obj As New clsItemUpdateMaster()
                'obj.ArrTr = New List(Of clsItemUpdateDetail)

                For Each grow As GridViewRowInfo In Gv1.Rows
                    If (clsCommon.myLen(grow.Cells(colAccountSetCode).Value) > 0) Then

                        'Dim objTr As New clsItemUpdateDetail()
                        'objTr.Item_Code = clsCommon.myCstr(grow.Cells(colAccountSetCode).Value)

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Payable_Account", clsCommon.myCstr(grow.Cells(colPayableAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Discount_Account", clsCommon.myCstr(grow.Cells(colDiscountAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Advance_Account", clsCommon.myCstr(grow.Cells(colAdvanceAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "EXCHANGE_LOSS_ACCOUNT", clsCommon.myCstr(grow.Cells(colExchangeLossAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "EXCHANGE_GAIN_ACCOUNT", clsCommon.myCstr(grow.Cells(colExchangeGainAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Commission_ACCOUNT", clsCommon.myCstr(grow.Cells(colCommissionAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Incentive_ACCOUNT", clsCommon.myCstr(grow.Cells(colIncentiveAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "SECURITY_ACCOUNT", clsCommon.myCstr(grow.Cells(colSecurityAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Head_Load_ACCOUNT", clsCommon.myCstr(grow.Cells(colHeadLoadAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Own_Asset_ACCOUNT", clsCommon.myCstr(grow.Cells(colOwnAssetAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Deduction_ACCOUNT", clsCommon.myCstr(grow.Cells(colDeductionAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Advance_Against_Salary", clsCommon.myCstr(grow.Cells(colAdvanceAgainstSalary).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Employee_Salary", clsCommon.myCstr(grow.Cells(colEmployeeSalary).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Advance_Against_Travelling", clsCommon.myCstr(grow.Cells(colAdvanceAgainstTravelling).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Advance_Against_Imprest", clsCommon.myCstr(grow.Cells(colAdvanceAgainstImprest).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Freight_Provision", clsCommon.myCstr(grow.Cells(colFreightProvisionAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Handling_Charges", clsCommon.myCstr(grow.Cells(colHandlingChargeAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Round_Off", clsCommon.myCstr(grow.Cells(colRoundoffAccount).Value), True)
                        clsCommon.AddColumnsForChange(coll, "Short_Excess", clsCommon.myCstr(grow.Cells(colShortExcessAccount).Value), True)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_ACCOUNT_SET", OMInsertOrUpdate.Update, "TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code='" + clsCommon.myCstr(grow.Cells(colAccountSetCode).Value) + "'", Nothing)

                    End If
                Next

                common.clsCommon.MyMessageBoxShow(Me, "Data Update Successfully", Me.Text)
                FunReset()
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            If e.Column Is Gv1.Columns(colVendorCode) OrElse e.Column Is Gv1.Columns(colVendorName) Then
                Dim vendorcode As String = ""
                vendorcode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colVendorCode).Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.vendormaster, vendorcode)
            ElseIf e.Column Is Gv1.Columns(colVendorGroupCode) OrElse e.Column Is Gv1.Columns(colVendorGroupName) Then
                Dim vendorgroupcode As String = ""
                vendorgroupcode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colVendorGroupCode).Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.vendorgroup, vendorgroupcode)
            ElseIf e.Column Is Gv1.Columns(colAccountSetCode) OrElse e.Column Is Gv1.Columns(colAccountSetName) Then
                Dim vendoraccountsetcode As String = ""
                vendoraccountsetcode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colAccountSetCode).Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.vendoraccountset, vendoraccountsetcode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellValueChanged
        Try
            If (isInsideLoadData) Then
                If e.Column Is Gv1.Columns(colPayableAccount) Then
                    Dim Qry As String = ""
                    whrcls = ""
                    arrlist = clsERPFuncationality.glaccountqueryForControlAcc(objCommonVar.CurrentUserCode)
                    Qry = arrlist.Item(0)
                    whrcls = arrlist.Item(1)
                    If whrcls <> "" Then
                        whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    Else
                        whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    End If
                    'Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colPayableAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", whrcls, Gv1.CurrentRow.Cells(colPayableAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colPayableAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colPayableAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colDiscountAccount) Then
                    Dim Qry As String = ""
                    whrcls = ""
                    arrlist = clsERPFuncationality.glaccountqueryForControlAcc(objCommonVar.CurrentUserCode)
                    Qry = arrlist.Item(0)
                    whrcls = arrlist.Item(1)
                    If whrcls <> "" Then
                        whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    Else
                        whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    End If
                    'Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colDiscountAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", whrcls, Gv1.CurrentRow.Cells(colDiscountAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colDiscountAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colDiscountAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colAdvanceAccount) Then
                    Dim Qry As String = ""
                    whrcls = ""
                    arrlist = clsERPFuncationality.glaccountqueryForControlAcc(objCommonVar.CurrentUserCode)
                    Qry = arrlist.Item(0)
                    whrcls = arrlist.Item(1)
                    If whrcls <> "" Then
                        whrcls = "" + whrcls + " and account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    Else
                        whrcls = "  account_type <>'Retained Earnings'and ControlAccount  ='Y' "
                    End If
                    'Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colAdvanceAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", whrcls, Gv1.CurrentRow.Cells(colAdvanceAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colAdvanceAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colAdvanceAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colExchangeLossAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colExchangeLossAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colExchangeLossAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colExchangeLossAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colExchangeLossAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colExchangeGainAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colExchangeGainAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colExchangeGainAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colExchangeGainAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colExchangeGainAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colCommissionAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colCommissionAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colCommissionAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colCommissionAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colCommissionAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colIncentiveAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colIncentiveAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colIncentiveAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colIncentiveAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colIncentiveAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colSecurityAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colSecurityAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colSecurityAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colSecurityAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colSecurityAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colHeadLoadAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colHeadLoadAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colHeadLoadAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colHeadLoadAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colHeadLoadAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colOwnAssetAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colOwnAssetAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colOwnAssetAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colOwnAssetAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colOwnAssetAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colDeductionAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colDeductionAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colDeductionAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colDeductionAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colDeductionAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colAdvanceAgainstSalary) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colAdvanceAgainstSalary).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colAdvanceAgainstSalary).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colAdvanceAgainstSalaryName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colAdvanceAgainstSalary).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colEmployeeSalary) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colEmployeeSalary).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colEmployeeSalary).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colEmployeeSalaryName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colEmployeeSalary).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colAdvanceAgainstTravelling) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colAdvanceAgainstTravelling).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colAdvanceAgainstTravelling).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colAdvanceAgainstTravellingName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colAdvanceAgainstTravelling).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colAdvanceAgainstImprest) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colAdvanceAgainstImprest).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colAdvanceAgainstImprest).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colAdvanceAgainstImprestName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colAdvanceAgainstImprest).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colFreightProvisionAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colFreightProvisionAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colFreightProvisionAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colFreightProvisionAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colFreightProvisionAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colHandlingChargeAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colHandlingChargeAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colHandlingChargeAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colHandlingChargeAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colHandlingChargeAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colRoundoffAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colRoundoffAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colRoundoffAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colRoundoffAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colRoundoffAccount).Value + "' ")
                ElseIf e.Column Is Gv1.Columns(colShortExcessAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    Gv1.CurrentRow.Cells(colShortExcessAccount).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", Gv1.CurrentRow.Cells(colShortExcessAccount).Value, "Account_Code", False)
                    Gv1.CurrentRow.Cells(colShortExcessAccountName).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + Gv1.CurrentRow.Cells(colShortExcessAccount).Value + "' ")
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Export(EnumExportTo.PDF)
    End Sub
    ' Ticket No : TEC/02/05/19-000470 by prabhakar
    Private Sub chkOnlyview_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyview.CheckedChanged
        If chkOnlyview.Checked = True Then
            Gv1.EnableFiltering = True
        Else
            Gv1.EnableFiltering = False
        End If
    End Sub
End Class
