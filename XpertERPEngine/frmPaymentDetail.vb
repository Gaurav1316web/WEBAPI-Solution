Imports common
Imports System.Windows.Forms

Public Class FrmPaymentDetail
    Public bankCode As String = ""
    Public bankDesc As String = ""
    Public MainbankCode As String = ""
    Public MainbankDesc As String = ""
    Public paymentMode As String = ""
    Public desc As String = ""
    Public btnOkClicked As Boolean = False
    Public IsFarmerPayment As Boolean = False
    Public PaymentDate As Date? = Nothing


    Private Sub FrmPaymentDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOkPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub
    Sub btnCancelPressed()
        btnOkClicked = False
        Me.Close()
    End Sub
    Sub btnOkPressed()
        Try
            If clsCommon.myLen(txtDescription.Text) <= 0 Then
                Throw New Exception("Please Enter Description")
            End If
            If clsCommon.myLen(txtBankCode.Value) <= 0 Then
                Throw New Exception("Please Enter Bank Code")
            End If
            If clsCommon.myLen(txtPaymentMode.Value) <= 0 Then
                Throw New Exception("Please Enter Payment Mode")
            End If
            bankCode = txtBankCode.Value
            MainbankCode = clsBankMaster.GetMainBank(bankCode) ''frm.bankCode
            desc = txtDescription.Text
            bankDesc = lblBankDesc.Text
            MainbankDesc = clsBankMaster.GetName(MainbankCode)
            paymentMode = txtPaymentMode.Value
            If PaymentDate IsNot Nothing Then
                PaymentDate = txtDate.Value
            End If
            btnOkClicked = True
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub reset()
        txtDescription.Text = desc
        txtBankCode.Value = ""
        lblBankDesc.Text = ""
        txtPaymentMode.Value = ""
        If PaymentDate IsNot Nothing Then
            lblDocDate.Visible = True
            txtDate.Visible = True
            txtDate.MinDate = PaymentDate
        End If

    End Sub

    Private Sub FrmPaymentDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        reset()
    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOkPressed()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub txtBankCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtBankCode._MYValidating
        Dim strWhrclas As String = " 2=2 "
        Dim Qry As String = clsERPFuncationality.glbankqueryNew(strWhrclas)
        If IsFarmerPayment Then
            strWhrclas = strWhrclas & " and Is_Clearance_Bank='Y'"
        End If
        txtBankCode.Value = clsCommon.ShowSelectForm("BankSlctr@Payment", Qry, "Code", strWhrclas, txtBankCode.Value, "Code", isButtonClicked)
        lblBankDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_MASTER where bank_code = '" + txtBankCode.Value + "'")
        txtPaymentMode.Value = connectSql.RunScalar("select TSPL_PAYMENT_CODE.Payment_Code   from TSPL_PAYMENT_CODE Where TSPL_PAYMENT_CODE.Payment_Code=  (select DISTINCT (case when Bank_type = 'C' THEN 'CASH' WHEN BANK_TYPE = 'B' THEN 'CHEQUE' WHEN BANK_TYPE = 'O' THEN 'OTHER' WHEN Bank_type = 'P' THEN 'PETTYCASH' END ) AS [Paymet Type] from TSPL_BANK_MASTER Where BANK_CODE='" + txtBankCode.Value + "' )")
    End Sub

    Private Sub txtPaymentMode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtPaymentMode._MYValidating
        Dim strbankcode As String
        If Not String.IsNullOrEmpty(connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtBankCode.Value + "'")) Then
            strbankcode = connectSql.RunScalar("select bank_type from tspl_bank_master where bank_code = '" + txtBankCode.Value + "'")
            If strbankcode.Trim() = "C" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector1", Qry1, "PaymentMode", "PAYMENT_TYPE = 'CASH'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode.Trim() = "P" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector2", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Petty Cash'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            ElseIf strbankcode = "B" Then
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector3", Qry1, "PaymentMode", "PAYMENT_TYPE IN ('Cheque', 'Other','NEFT','RTGS')", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            Else
                Dim Qry1 As String = "select Payment_Code as [PaymentMode], Payment_Desc as [Description], Payment_Type  as [PaymentType]  from TSPL_PAYMENT_CODE "
                txtPaymentMode.Value = clsCommon.ShowSelectForm("PaymentCode Selector4", Qry1, "PaymentMode", "PAYMENT_TYPE = 'Other'", txtPaymentMode.Value, "PaymentMode", isButtonClicked)
            End If
        End If
    End Sub
End Class
