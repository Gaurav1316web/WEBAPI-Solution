Imports common
Imports System.Data.SqlClient

Public Class frmBankOpeningReco
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
#End Region
    Private Sub fndaccgp__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndaccgp__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        txtCode.Value = clsBankOpeningReco.getFinder("", txtCode.Value, isButtonClicked)
        If txtCode.Value <> "" Then
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            funReset()
        End If

    End Sub

    Private Sub FrmAccountMainGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub FrmAccountMainGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        funReset()
        LoadType()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N for New Transaction")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "Withdrawal"
        dr("Name") = "Withdrawal"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Deposit"
        dr("Name") = "Deposit"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.InvetorySourceCode)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        If btnsave.Visible = True Then
            rmExport.Enabled = True
            rmImport.Enabled = True
        Else
            rmExport.Enabled = False
            rmImport.Enabled = False
        End If
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Function AllowToSave() As Boolean
        Dim strcode As String = txtCode.Value.ToString()
        If txtAmount.Value <= 0 Then
            txtAmount.Focus()
            Throw New Exception("Amount can't be zero")
        End If
        If txtAmount.Value <= 0 Then
            txtAmount.Focus()
            Throw New Exception("Amount can't be zero")
        End If
        If clsCommon.myLen(txtVendorCustomer.Value) <= 0 Then
            txtVendorCustomer.Focus()
            Throw New Exception("Vendor/Customer can not be blank")
        End If
        If clsCommon.myLen(txtBankCode.Value) <= 0 Then
            txtBankCode.Focus()
            Throw New Exception("Bank can not be blank")
        End If
        Return True
    End Function
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (clsBankOpeningReco.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub PostData()
        Try
            If (postConfirm()) Then
                If (clsBankOpeningReco.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data posted Successfully ")
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtDate.Focus()
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtChequeDate.Value = txtDate.Value
        txtDesc.Text = ""
        cboType.SelectedValue = "Withdrawal"
        txtAmount.Value = 0
        txtVendorCustomer.Value = ""
        lblVendorCustomer.Text = ""
        txtChequeNo.Text = ""
        txtBankCode.Value = ""
        lblBankCode.Text = ""
        btnsave.Enabled = True
        btnPost.Enabled = True
        btndelete.Enabled = True
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsBankOpeningReco()
        obj = clsBankOpeningReco.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            txtCode.MyReadOnly = True
            isNewEntry = False
            If obj.Status = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
            End If
            UsLock1.Status = obj.Status
            txtCode.Value = obj.Code
            txtDate.Value = obj.Reco_Date
            txtDesc.Text = obj.Description
            cboType.SelectedValue = obj.Type
            txtAmount.Value = obj.Amt
            If clsCommon.CompairString(obj.Type, "Withdrawal") = CompairStringResult.Equal Then
                txtVendorCustomer.Value = obj.Vendor_Code
                lblVendorCustomer.Text = obj.Vendor_Name
            Else
                txtVendorCustomer.Value = obj.Cust_Code
                lblVendorCustomer.Text = obj.Cust_Name
            End If
            txtChequeNo.Text = obj.Cheque_No
            txtChequeDate.Value = obj.Cheque_Date
            txtBankCode.Value = obj.Bank_Code
            lblBankCode.Text = obj.Bank_Name
        End If
    End Sub

    Public Sub SaveData()
        Try

            If AllowToSave() Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.InvetorySourceCode, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsBankOpeningReco()
                obj.Code = txtCode.Value
                obj.Reco_Date = txtDate.Value
                obj.Description = txtDesc.Text
                obj.Type = clsCommon.myCstr(cboType.SelectedValue)
                obj.Amt = txtAmount.Value
                If clsCommon.CompairString(obj.Type, "Withdrawal") = CompairStringResult.Equal Then
                    obj.Vendor_Code = txtVendorCustomer.Value
                Else
                    obj.Cust_Code = txtVendorCustomer.Value
                End If
                obj.Cheque_No = txtChequeNo.Text
                obj.Cheque_Date = txtChequeDate.Value
                obj.Bank_Code = txtBankCode.Value
                If (clsBankOpeningReco.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub funClose()
        Me.Close()
        GC.Collect()
    End Sub
#End Region

    Private Sub rmImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim linno As Integer = 0
        Dim dtTemp As DataTable
        If transportSql.importExcel(gv, "RecoDate(dd/MMM/yyyy)", "VendorCustomerCode", "WithdrawalAmount", "DepositAmount", "BankCode", "ChequeNo", "ChequeDate(dd/MMM/yyyy)", "Description") Then
            Dim trans As SqlTransaction = Nothing
            Try

                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarPercentShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    linno += 1
                    clsCommon.ProgressBarPercentUpdate((linno * 100) / (gv.RowCount), "Importing Data.Please Wait...")
                    Dim strVendorCustomer As String = clsCommon.myCstr(grow.Cells("VendorCustomerCode").Value)
                    If clsCommon.myLen(strVendorCustomer) > 0 Then
                        Dim obj As New clsBankOpeningReco()
                        If clsCommon.myCdbl(grow.Cells("WithdrawalAmount").Value) = 0 AndAlso clsCommon.myCdbl(grow.Cells("DepositAmount").Value) = 0 Then
                            Throw New Exception("Please enter withdrwal amount or Deposit amount")
                        End If
                        If clsCommon.myCdbl(grow.Cells("WithdrawalAmount").Value) > 0 AndAlso clsCommon.myCdbl(grow.Cells("DepositAmount").Value) > 0 Then
                            Throw New Exception("Please enter only one withdrwal amount or Deposit amount")
                        End If
                        If clsCommon.myCdbl(grow.Cells("WithdrawalAmount").Value) > 0 Then
                            obj.Vendor_Code = clsCommon.myCstr(grow.Cells("VendorCustomerCode").Value)
                            If clsCommon.myLen(obj.Vendor_Code) <= 0 Then
                                Throw New Exception("Vendor Code can not be blank")
                            End If

                            dtTemp = clsDBFuncationality.GetDataTable("select Vendor_Code from TSPL_VENDOR_MASTER where Vendor_Code='" + obj.Vendor_Code + "'", trans)
                            If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                Throw New Exception("Not a Valid vendor " + obj.Vendor_Code)
                            Else
                                obj.Vendor_Code = clsCommon.myCstr(dtTemp.Rows(0)("Vendor_Code"))
                            End If
                            obj.Type = "Withdrawal"
                            obj.Amt = clsCommon.myCdbl(grow.Cells("WithdrawalAmount").Value)
                        Else
                            obj.Cust_Code = clsCommon.myCstr(grow.Cells("VendorCustomerCode").Value)
                            If clsCommon.myLen(obj.Cust_Code) <= 0 Then
                                Throw New Exception("Customer Code can not be blank")
                            End If
                            dtTemp = clsDBFuncationality.GetDataTable("select Cust_Code from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Cust_Code + "'", trans)
                            If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                Throw New Exception("Not a Valid Customer " + obj.Cust_Code)
                            Else
                                obj.Cust_Code = clsCommon.myCstr(dtTemp.Rows(0)("Cust_Code"))
                            End If
                            obj.Type = "Deposit"
                            obj.Amt = clsCommon.myCdbl(grow.Cells("DepositAmount").Value)
                        End If
                        If clsCommon.myLen(grow.Cells("BankCode").Value) <= 0 Then
                            Throw New Exception("Bank Code can not be blank")
                        End If

                        obj.Bank_Code = clsCommon.myCstr(grow.Cells("BankCode").Value)
                        dtTemp = clsDBFuncationality.GetDataTable("select BANK_CODE from TSPL_BANK_MASTER where BANK_CODE='" + obj.Bank_Code + "'", trans)
                        If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                            Throw New Exception("Not a Valid Bank " + obj.Bank_Code)
                        Else
                            obj.Bank_Code = clsCommon.myCstr(dtTemp.Rows(0)("BANK_CODE"))
                        End If

                        obj.Reco_Date = clsCommon.myCDate(grow.Cells("RecoDate(dd/MMM/yyyy)").Value)
                        obj.Cheque_No = clsCommon.myCstr(grow.Cells("ChequeNo").Value)
                        obj.Cheque_Date = clsCommon.myCDate(grow.Cells("ChequeDate(dd/MMM/yyyy)").Value)
                        obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        clsBankOpeningReco.SaveData(obj, True, trans)
                        clsBankOpeningReco.PostData(obj.Code, trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(ex.Message + Environment.NewLine + "At line no " + clsCommon.myCstr(linno))
            Finally
                Me.Controls.Remove(gv)
            End Try
        End If
    End Sub

    Private Sub rmExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select '' as [RecoDate(dd/MMM/yyyy)],'' as VendorCustomerCode,0 as WithdrawalAmount,0 as DepositAmount,'' as BankCode, '' as ChequeNo, null as [ChequeDate(dd/MMM/yyyy)],'' as Description"
        transportSql.ExporttoExcel(str, Me)

    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub txtVendorCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVendorCustomer._MYValidating
        Dim qry As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Withdrawal") = CompairStringResult.Equal Then
            qry = " select Vendor_Code as Code,Vendor_Name as Description from TSPL_VENDOR_MASTER "
            txtVendorCustomer.Value = clsCommon.ShowSelectForm("VendFndBOPR", qry, "Code", "", txtVendorCustomer.Value, "Code", isButtonClicked)
            lblVendorCustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name as Description from TSPL_VENDOR_MASTER where Vendor_Code  ='" + txtVendorCustomer.Value + "'"))
        Else
            qry = "  select Cust_Code as Code,Customer_Name as Description  from TSPL_CUSTOMER_MASTER "
            txtVendorCustomer.Value = clsCommon.ShowSelectForm("CustFndBOPR", qry, "Code", "", txtVendorCustomer.Value, "Code", isButtonClicked)
            lblVendorCustomer.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name as Description  from TSPL_CUSTOMER_MASTER where Cust_Code ='" + txtVendorCustomer.Value + "'"))
        End If
    End Sub

    Private Sub cboType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboType.SelectedIndexChanged
        txtVendorCustomer.Value = ""
        lblVendorCustomer.Text = ""
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Withdrawal") = CompairStringResult.Equal Then
            RadLabel2.Text = "Vendor"
        Else
            RadLabel2.Text = "Customer"
        End If
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub txtBankCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBankCode._MYValidating
        Dim StrQuery As String = " select bank_code As Code,description  as [Description],BANKACCNUMBER as [BankAccNo]  from TSPL_Bank_MASTER "
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(Nothing)
        Dim strWhrclas As String = "1=1"
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strWhrclas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(Bank_Code) > 0 Then
                strWhrclas += " AND TSPL_BANK_TRANSFER.Bank_Code in ( " + Bank_Code + " )"
            End If
        End If
        txtBankCode.Value = clsCommon.ShowSelectForm("Bank Master", StrQuery, "Code", strWhrclas, txtBankCode.Value, "bank_code", isButtonClicked)
        lblBankCode.Text = clsCommon.myCstr("select description from TSPL_Bank_MASTER where bank_code='" + txtBankCode.Value + "'")
    End Sub
End Class
