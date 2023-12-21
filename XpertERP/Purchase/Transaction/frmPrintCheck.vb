Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
'''' <summary>
'''' ''''''''''''''''''''''''TicketNo='BM00000002089''''''''''''''''''''''''''''''''''''''''
'''' </summary>
'''' <remarks></remarks>
'' updation by richa agarwal BM00000006836 a/c payee checkbox working ,BM00000006857
'update by preeti gupta Against ticket no[BHA/28/02/19-000825]

Public Class frmPrintCheck
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Public Shared BankCode As String
    Public Shared CheckCode As String
    Public Shared DocumentType As String
    Public Shared DocumentCode As String
    Public Shared Manual_Print As Integer = 0
    Public Shared Manual_Check_No As String = ""
    '' Anubhooti 22-July-2014
    Public Shared Account_Payee As Integer
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPaymentEntry)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'btnPrint.Visible = MyBase.isModifyFlag

    End Sub
    Sub LoadData(ByVal BankCode As String, ByVal CheckCode As String, ByVal DocumentType As String, ByVal DocumentCode As String, Optional ByVal trans As SqlTransaction = Nothing)
        Dim obj As clsPrintCheck = clsPrintCheck.GetData(BankCode, CheckCode, DocumentType, DocumentCode, trans)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtPrintingId.Text = obj.PRINTING_ID
            txtDocType.Text = obj.DOCUMENT_TYPE
            txtDocCode.Text = obj.DOCUMENT_NO
            dtpPrintDate.Value = clsCommon.GETSERVERDATE
            txtPrintedBy.Text = obj.PINTED_BY
            txtPrintStatus.Text = obj.PRINT_STATUS
            txtBankCode.Text = obj.BANK_CODE
            Manual_Print = IIf(clsCommon.myLen(obj.CHECK_CODE) > 0, obj.Manual_Print, Manual_Print)
            '' Anubhooti
            'If obj.Account_Payee = 1 Then
            '    chkAccPayee.Checked = True
            'Else
            '    chkAccPayee.Checked = False
            'End If
            'If obj.Not_For_High_Value = 1 Then
            '    chkNotForHighValue.Checked = True
            'Else
            '    chkNotForHighValue.Checked = False
            'End If
            If obj.PRINTING_ID <= 0 And Manual_Print = 0 Then
                fndCheckCode.Value = Nothing
                fndCheckCode.Enabled = True
            Else
                fndCheckCode.Value = obj.CHECK_CODE
                fndCheckCode.Enabled = False
            End If
            Me.txtNextCheckNumber.Text = IIf(clsCommon.myLen(obj.CHECK_CODE) > 0, obj.CHECK_NUMBER, Manual_Check_No) 'obj.CHECK_NUMBER
            'If Manual_Check_No = 1 Then
            '    fndCheckCode.Enabled = False
            'Else
            '    fndCheckCode.Enabled = True
            'End If
        End If
    End Sub
    Sub SaveData(ByVal showPrint As Boolean)
        Dim trans As SqlTransaction = Nothing
        Try
            If AllowToSave() Then
                trans = clsDBFuncationality.GetTransactin()
                Dim obj As New clsPrintCheck()
                obj.PRINTING_ID = txtPrintingId.Text
                obj.PRINT_DATE = Me.dtpPrintDate.Value
                obj.DOCUMENT_TYPE = Me.txtDocType.Text
                obj.DOCUMENT_NO = Me.txtDocCode.Text
                obj.DOCUMENT_Date = dtpPrintDate.Value
                obj.PINTED_BY = Me.txtPrintedBy.Text
                obj.PRINT_STATUS = Me.txtPrintStatus.Text
                obj.BANK_CODE = Me.txtBankCode.Text
                obj.CHECK_CODE = clsCommon.myCstr(Me.fndCheckCode.Value)
                obj.CHECK_NUMBER = Me.txtNextCheckNumber.Text
                obj.Manual_Print = Manual_Print
                '' Anubhooti 22-July-2014 BM00000003161
                If chkAccPayee.Checked Then
                    obj.Account_Payee = 1
                Else
                    obj.Account_Payee = 0
                End If
                If chkNotForHighValue.Checked Then
                    obj.Not_For_High_Value = 1
                Else
                    obj.Not_For_High_Value = 0
                End If
                If (clsPrintCheck.SaveData(obj, trans)) Then
                    trans.Commit()
                    'clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.BANK_CODE, obj.CHECK_CODE, obj.DOCUMENT_TYPE, obj.DOCUMENT_NO)
                    If showPrint Then
                        funPrint()
                    End If


                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function funPrint() As Boolean
        Try
            Dim Qry As String = ""
            If clsCommon.CompairString(DocumentType, "Payment Entry") = CompairStringResult.Equal Then
                '(case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV') then TSPL_VENDOR_MASTER.Vendor_Name  else TSPL_PAYMENT_HEADER.Entry_Desc  end) as Pay_To
                '(TSPL_VENDOR_MASTER.vsp_payee_name + '  A/c ' + coalesce(TSPL_BANK_MASTER.Bank_Name,'') + ' - ' + coalesce(TSPL_VENDOR_MASTER.Account_No,''))
                Qry = "select TSPL_PAYMENT_HEADER.Bank_Code,TSPL_BANK_MASTER.Bank_Name as bank_name,Cheque_No as check_no, " & _
                                " coalesce(Cheque_In_Favour_Of,Remit_To) as Pay_To," & _
                                " (case when memorandum_amt>0 and Payment_Amount>0 then Payment_Amount when memorandum_amt>0 and  Payment_Amount=0 then memorandum_amt else Payment_Amount end ) as Check_Amount,'' as Amount_In_Words," & _
                                " TSPL_VENDOR_MASTER.Account_No as Account_No,TSPL_BANK_MASTER.ADD1 as Address1,TSPL_PAYMENT_HEADER.Comp_Code, " & _
                                " TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, convert(varchar,Cheque_Date,105) as check_Date ,(case when memorandum_amt>0 then '(not exceeding for Rs.' + cast(cast(memorandum_amt as decimal) as varchar) + '/-)' else '' end) as Memorandum_Lim_Desc,memorandum_amt,(case when memorandum_amt>0 then Entry_Desc else '' end) as Memorandum_Entry_Desc " & _
                                " , (case when TSPL_PAYMENT_HEADER.Account_Payee = 1 then 'A/C Payee' else '' end) as Account_Payee,TSPL_PAYMENT_HEADER.Bank_Charges " & _
                                " from TSPL_PAYMENT_HEADER " & _
                                " left join TSPL_COMPANY_MASTER on TSPL_PAYMENT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
                                " left join TSPL_VENDOR_MASTER on TSPL_PAYMENT_HEADER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
                                " left join TSPL_VENDOR_BANK_MASTER as TSPL_BANK_MASTER  on TSPL_VENDOR_MASTER.Bank_Code=TSPL_BANK_MASTER.Bank_Code " & _
                                " where TSPL_PAYMENT_HEADER.Payment_No='" & Me.txtDocCode.Text & "' "

                ' " coalesce((CASE WHEN TSPL_VENDOR_MASTER.Form_Type in ('VSP','PTM','TTM') then coalesce(Cheque_In_Favour_Of,'') else (TSPL_VENDOR_MASTER.Vendor_Name) end),Remit_To) as Pay_To," & _
            ElseIf clsCommon.CompairString(DocumentType, "Receipt Entry") = CompairStringResult.Equal Then
                Qry = " select TSPL_RECEIPT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as bank_name,Cheque_No as check_no,  " & _
                    " (case when TSPL_RECEIPT_HEADER.Receipt_Type='PY' then TSPL_CUSTOMER_MASTER.Customer_Name else TSPL_RECEIPT_HEADER.Entry_Desc end) as Pay_To, " & _
                    " Receipt_Amount as Check_Amount,'' as Amount_In_Words, TSPL_BANK_MASTER.BANKACCNUMBER as Account_No,TSPL_BANK_MASTER.ADD1 as Address1, " & _
                    " TSPL_RECEIPT_HEADER.Comp_Code,  TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, convert(varchar,Cheque_Date,105) as check_Date,TSPL_RECEIPT_HEADER.Bank_charges_amt as Bank_Charges from TSPL_RECEIPT_HEADER " & _
                    " left join TSPL_BANK_MASTER on TSPL_RECEIPT_HEADER.Bank_Code=TSPL_BANK_MASTER.Bank_Code  " & _
                    " left join TSPL_COMPANY_MASTER on TSPL_RECEIPT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  " & _
                    " left join TSPL_CUSTOMER_MASTER on TSPL_RECEIPT_HEADER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                    " where TSPL_RECEIPT_HEADER.Receipt_No='" & Me.txtDocCode.Text & "' "
            ElseIf clsCommon.CompairString(DocumentType, "Bank Transfer") = CompairStringResult.Equal Then
                Qry = "Select TSPL_BANK_TRANSFER.From_Bank_Code AS [Bank_Code],TSPL_BANK_TRANSFER.From_Bank_Name AS [Bank_Name],TSPL_BANK_TRANSFER.Cheque_No AS [Check_No],ISNULL(TSPL_BANK_TRANSFER.Remitt_To,'') AS Pay_To,TSPL_BANK_TRANSFER.Transfer_Amount AS [Check_Amount],'' as Amount_In_Words,TSPL_BANK_TRANSFER.From_Bank_Acc_No AS Account_No,TSPL_BANK_MASTER.ADD1 as Address1,TSPL_BANK_TRANSFER.Comp_Code , TSPL_COMPANY_MASTER.Comp_Name as Comp_Name,convert(varchar,TSPL_BANK_TRANSFER.Cheque_Date,105) as check_Date,'' as Memorandum_Lim_Desc,0 AS memorandum_amt,'' as Memorandum_Entry_Desc " & _
                    " , '' as Account_Payee,TSPL_BANK_TRANSFER.Bank_Charges_Ac as Bank_Charges From TSPL_BANK_TRANSFER " & _
                    " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_TRANSFER.From_Bank_Code =TSPL_BANK_MASTER.BANK_CODE " & _
                    " LEFT OUTER JOIN TSPL_COMPANY_MASTER on TSPL_BANK_TRANSFER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
                    " WHERE TSPL_BANK_TRANSFER.Transfer_No ='" & Me.txtDocCode.Text & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim objrpt As New clsrptChequePrint()
                objrpt.PrintCheque(dt, chkAccPayee.Checked, chkNotForHighValue.Checked)
            End If

            
             
            Return False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Function funPrintOld() As Boolean
        Try
            Dim Qry As String = ""
            If clsCommon.CompairString(DocumentType, "Payment Entry") = CompairStringResult.Equal Then
                Qry = "select TSPL_PAYMENT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as bank_name,Cheque_No as check_no, " & _
                                " (case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV') then TSPL_VENDOR_MASTER.Vendor_Name + '  x x x x' else TSPL_PAYMENT_HEADER.Entry_Desc + '  x x x x' end) as Pay_To,(case when memorandum_amt>0 and Payment_Amount>0 then Payment_Amount when memorandum_amt>0 and  Payment_Amount=0 then null else Payment_Amount end ) as Check_Amount,'' as Amount_In_Words," & _
                                " TSPL_BANK_MASTER.BANKACCNUMBER as Account_No,TSPL_BANK_MASTER.ADD1 as Address1,TSPL_PAYMENT_HEADER.Comp_Code, " & _
                                " TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, convert(varchar,Cheque_Date,105) as check_Date,'' as datePart1,'' as datePart2,'' as datePart3,'' as datePart4," & _
                                " '' as datePart5,'' as datePart6,'' as datePart7,'' as datePart8 ,(case when memorandum_amt>0 then '(not exceeding for Rs.' + cast(cast(memorandum_amt as decimal) as varchar) + '/-)' else '' end) as Memorandum_Lim_Desc,memorandum_amt,(case when memorandum_amt>0 then Entry_Desc else '' end) as Memorandum_Entry_Desc " & _
                                " , (case when TSPL_PAYMENT_HEADER.Account_Payee = 1 then 'A/C Payee' else '' end) as Account_Payee " & _
                                " from TSPL_PAYMENT_HEADER " & _
                                " left join TSPL_BANK_MASTER on TSPL_PAYMENT_HEADER.Bank_Code=TSPL_BANK_MASTER.Bank_Code " & _
                                " left join TSPL_COMPANY_MASTER on TSPL_PAYMENT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
                                " left join TSPL_VENDOR_MASTER on TSPL_PAYMENT_HEADER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
                                " where TSPL_PAYMENT_HEADER.Payment_No='" & Me.txtDocCode.Text & "' "
            ElseIf clsCommon.CompairString(DocumentType, "Receipt Entry") = CompairStringResult.Equal Then
                Qry = " select TSPL_RECEIPT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as bank_name,Cheque_No as check_no,  (case when TSPL_RECEIPT_HEADER.Receipt_Type='PY' then TSPL_CUSTOMER_MASTER.Customer_Name else TSPL_RECEIPT_HEADER.Entry_Desc end) as Pay_To,Receipt_Amount as Check_Amount,'' as Amount_In_Words, TSPL_BANK_MASTER.BANKACCNUMBER as Account_No,TSPL_BANK_MASTER.ADD1 as Address1,TSPL_RECEIPT_HEADER.Comp_Code,  TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, convert(varchar,Cheque_Date,105) as check_Date from TSPL_RECEIPT_HEADER  left join TSPL_BANK_MASTER on TSPL_RECEIPT_HEADER.Bank_Code=TSPL_BANK_MASTER.Bank_Code  left join TSPL_COMPANY_MASTER on TSPL_RECEIPT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  left join TSPL_CUSTOMER_MASTER on TSPL_RECEIPT_HEADER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where TSPL_RECEIPT_HEADER.Receipt_No='" & Me.txtDocCode.Text & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            For Each dr As DataRow In dt.Rows
                If IsDBNull(dt.Rows(dt.Rows.IndexOf(dr)).Item("Check_Amount")) Then
                    dt.Rows(dt.Rows.IndexOf(dr)).Item("Amount_In_Words") = ""
                Else
                    dt.Rows(dt.Rows.IndexOf(dr)).Item("Amount_In_Words") = clsCommon.changeNumericToWords(dt.Rows(dt.Rows.IndexOf(dr)).Item("Check_Amount")) + "only x x x x"
                End If

                Dim arr As String()
                arr = dt.Rows(dt.Rows.IndexOf(dr)).Item("check_Date").ToString.Split("-")
                If arr.Length = 3 Then
                    dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart1") = arr(0).Substring(0, 1)
                    dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart2") = arr(0).Substring(1, 1)
                    dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart3") = arr(1).Substring(0, 1)
                    dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart4") = arr(1).Substring(1, 1)
                    dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart5") = arr(2).Substring(0, 1)
                    dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart6") = arr(2).Substring(1, 1)
                    dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart7") = arr(2).Substring(2, 1)
                    dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart8") = arr(2).Substring(3, 1)
                End If
            Next
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                '' changed by Panch Raj as per discussion with Amit Sir.
                'NewSalesReportViewer.funreport(dt, Me.txtBankCode.Text, "Print Check")
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.NewSalesReports, dt, "CheckPrintDemo", "Print Check")
                frmCRV = Nothing
            End If
            'If MsgBox("Have you done Check Printing ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '    Me.txtPrintStatus.Text = "Printed"
            '    Return True
            'Else
            '    Return False
            'End If
            Return False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(clsCommon.myCstr(txtBankCode.Text)) <= 0 Then
            txtBankCode.Focus()
            clsCommon.MyMessageBoxShow(Me, "Bank Code not found", Me.Text)
            Return False
        End If
        If Manual_Print = False Then
            If clsCommon.myLen(clsCommon.myCstr(fndCheckCode.Value)) <= 0 Then
                fndCheckCode.Focus()
                clsCommon.MyMessageBoxShow(Me, "Please select Check Code", Me.Text)
                Return False
            End If
        End If
       
        If clsCommon.myLen(clsCommon.myCdbl(txtNextCheckNumber.Text)) <= 0 Then
            txtNextCheckNumber.Focus()
            clsCommon.MyMessageBoxShow(Me, "Check No is blank", Me.Text)
            Return False
        End If


        Return True
    End Function
    
#End Region
#Region "Events"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        SaveData(True)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If clsCommon.MyMessageBoxShow("Have you done Check Printing ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.txtPrintStatus.Text = "Printed"
            SaveData(False)
        End If
        Me.Close()
    End Sub
    Private Sub frmPrintCheck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If Manual_Print Then
            fndCheckCode.Enabled = False
            fndCheckCode.MyReadOnly = True
        End If
        LoadData(BankCode, CheckCode, DocumentType, DocumentCode)
    End Sub

    Private Sub frmPrintCheck_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isModifyFlag Then
            SaveData(True)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
#End Region

    Private Sub fndCheckCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCheckCode._MYValidating
        Dim strWhrclas As String = ""
        Dim qry As String = "select CHECK_CODE as Code,BANK_CODE as [Bank Code],DESCRIPTION as Description,NEXT_CHECK_NUMBER as [Start Check Number],LAST_CHECK_NUMBER as [Last Check Number] from TSPL_BANK_CHECK_PRINTING"
        strWhrclas = " BANK_CODE='" & txtBankCode.Text & "'"
        fndCheckCode.Value = clsCommon.ShowSelectForm("Check", qry, "Code", strWhrclas, fndCheckCode.Value, "Code", isButtonClicked)
        lblCheckDesc.Text = connectSql.RunScalar("select description from TSPL_BANK_CHECK_PRINTING where CHECK_CODE = '" + fndCheckCode.Value + "'")
        Me.txtNextCheckNumber.Text = clsPrintCheck.GetNextCheckNumber(txtBankCode.Text, fndCheckCode.Value)
    End Sub
End Class
