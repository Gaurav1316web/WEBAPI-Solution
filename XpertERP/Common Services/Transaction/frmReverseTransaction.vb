''Updated By Abhishek kumar  as on 18 Oct 2012 3:04 Pm
'Updated By Priti  as on 18 Oct 2012 5:26 Pm
''''30/10/12----Updation by--[Shipra Jain]----add Print Button On Screen and Print On crystal Report ----Forwarded By:Ranjana Ma'am----
''22/11/12-10:59AM---Updation by--[Pankaj Kumar]----Cheque/paymentNo finder Will select only posted Documents, Added New Columns in main Finder----fwd By : Ranjana Mam
''09/01/2013-03:00PM---Updation by--[Pankaj Kumar]----Cheque/paymentNo finder Will select only posted Documents
'by vipin on 04/02/2013 for posting check on update 
'--Preeti Gupta--Ticket No.-BM00000003015-1/7/2014-- BM00000003244,[BM00000003204]

''Parteek Code generate based on  Prefix generation Ticket No BM00000009273
''MPD Ticket No - BM00000009933 Parteek

Imports Telerik.WinControls.UI
Imports Telerik.Collections
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports Common
Public Class frmReverseTransaction
    Inherits FrmMainTranScreen
    ''check In sanjay 19/06/2020
    Public strBankRvrse As String = Nothing
    Dim userCode, companyCode As String
    Dim dr As SqlDataReader
    Dim isFlag As Boolean = False
    Dim isTDSReverse As Boolean = False
    Dim isTaxReverse As Boolean = False
    Dim ds As DataSet
    Dim btntooltip As ToolTip = New ToolTip()
    Public clicked As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.reverseTransaction)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btn_save.Visible = MyBase.isModifyFlag
        btn_post.Visible = MyBase.isPostFlag
        btn_delete.Visible = MyBase.isDeleteFlag
        btnprint.Visible = MyBase.isPrintFlag
        'If MyBase.isReverse Then
        '    btnReverseTransaction.Enabled = True
        'Else
        '    btnReverseTransaction.Enabled = False
        'End If
        btnReverseTransaction.Visible = False

    End Sub
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Public Sub SetLength()
        txt_BankaccountNo.MaxLength = 30
        txt_Bankcode.MaxLength = 12
        txt_paymentAmount.MaxLength = 9
        txt_receiptAmount.MaxLength = 9
        txt_checkpaymentno.MaxLength = 30
        txt_checkreceiptno.MaxLength = 30
        txt_CustomerNo.MaxLength = 12
        txt_reason.MaxLength = 100
        txt_ReceiptReversedocument.MaxLength = 10
        txt_VendorNo.MaxLength = 10
    End Sub
    Private Sub frmReverseTransaction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isTDSReverse = False
        globalFunc.mandatoryText(txt_ReceiptReversedocument, txt_PaymentReverseDocument, txt_paymentAmount, txt_receiptAmount)
        txt_receiptAmount.Text = "0"
        fndreversecode.MyMaxLength = 30
        dtp_PayRecDate.Enabled = False
        dtp_reversaldate.Value = clsCommon.GETSERVERDATE()
        dtp_PayRecDate.Value = dtp_reversaldate.Value
        txt_Bankcode.Enabled = False
        txt_BankaccountNo.Enabled = False
        txt_PaymentReverseDocument.Enabled = False
        txt_ReceiptReversedocument.Enabled = False
        txt_checkreceiptno.Enabled = False
        txt_checkpaymentno.Enabled = False
        txt_VendorNo.Enabled = False
        txt_CustomerNo.Enabled = False
        txt_paymentAmount.Enabled = False
        txt_receiptAmount.Enabled = False
        btn_delete.Enabled = False
        btn_post.Enabled = False
        dtp_PayRecDate.CustomFormat = "dd/MM/yyyy"
        lbl_checkpaymentno.Visible = True
        ' Edited by Abhishek
        btntooltip.SetToolTip(btn_save, "Press Alt+S for Save/Update Trasnaction")
        btntooltip.SetToolTip(btn_post, "Press Alt+P Post Trasnaction")
        btntooltip.SetToolTip(btn_delete, "Press Alt+D Delete Trasnaction")
        btntooltip.SetToolTip(btn_close, "Press Esc Close the Window")
        btntooltip.SetToolTip(btn_reset, "Press Alt+N Adding New Transaction")
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        If clsCommon.myLen(strBankRvrse) > 0 Then
            fndreversecode.Value = strBankRvrse
            funFill4()
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndreversecode.Value = clsCommon.myCstr(Me.Tag)
            funFill4()
        End If
    End Sub

    Private Sub funFill1()
        Try
            Dim dt As DataTable
            If fndbankcode.Value <> "" Then
                dt = clsDBFuncationality.GetDataTable("select Bank_code,description,BANKACCNUMBER from TSPL_BANK_MASTER where Bank_Code='" + fndbankcode.Value + "'")
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                    For Each row As DataRow In dt.Rows


                        txt_Bankcode.Text = row(1).ToString()
                        txt_BankaccountNo.Text = row(2).ToString()
                    Next
                End If

                'While dr.Read()
                '    txt_Bankcode.Text = dr(1).ToString()
                '    txt_BankaccountNo.Text = dr(2).ToString()
                'End While
                'btn_save.Enabled = True
            Else
                'btn_save.Enabled = True
                'btn_save.Text = "&Save"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub funFill2()
        Try
            If fndVendorNo.Value <> "" Then
                Dim dt As DataTable
                dt = clsDBFuncationality.GetDataTable("select Vendor_Code,Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + fndVendorNo.Value + "'")
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then


                    For Each row As DataRow In dt.Rows
                        txt_VendorNo.Text = row(1).ToString()
                    Next
                End If
                'btn_save.Enabled = True
            Else
                'btn_save.Enabled = True
                ' btn_save.Text = "&Save"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub funFill3()
        Try
            If fndcustomerNo.Value <> "" Then
                Dim dt As DataTable
                dt = clsDBFuncationality.GetDataTable("select Cust_Code,Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndcustomerNo.Value + "'")
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                    For Each row As DataRow In dt.Rows
                        txt_CustomerNo.Text = row(1).ToString()
                    Next
                End If

                'btn_save.Enabled = True
            Else
                'btn_save.Enabled = True
                'btn_save.Text = "&Save"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub funFill5()
        Try
            If fndCheckPaymentNo.Value <> "" Then
                Dim dt As DataTable
                dt = clsDBFuncationality.GetDataTable("select Cheque_No,Payment_Amount,Payment_Date,Vendor_Code from TSPL_PAYMENT_HEADER where Payment_No='" + fndCheckPaymentNo.Value + "'")
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then


                    For Each row As DataRow In dt.Rows
                        txt_checkpaymentno.Text = row(0).ToString()
                        txt_paymentAmount.Text = row(1).ToString()
                        dtp_PayRecDate.Value = clsCommon.GetPrintDate(row(2).ToString(), "dd/MM/yyyy")
                        '' Anubhooti 21-Nov-2014 BM00000004671
                        fndVendorNo.Value = clsCommon.myCstr(row(3).ToString())
                        If clsCommon.myLen(fndVendorNo.Value) > 0 Then
                            txt_VendorNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code='" + fndVendorNo.Value + "'"))
                        Else
                            txt_VendorNo.Text = ""
                        End If
                        ''
                    Next
                End If
                'btn_save.Enabled = True
            Else
                'btn_save.Enabled = True
                'btn_save.Text = "&Save"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub funFill6()
        Try
            If fndcheckReceiptNo.Value <> "" Then
                Dim dt As DataTable
                'dr = connectSql.RunSqlReturnDR("select Receipt_No,Cheque_No,Receipt_Amount,Receipt_Date from TSPL_RECEIPT_HEADER where Receipt_No='" + fndcheckReceiptNo.Value + "'")
                dt = clsDBFuncationality.GetDataTable("select Cheque_No, sum(isnull(Receipt_Amount,0) + isnull(UnApplied_Balance,0)) as Receipt_Amount ,Receipt_Date,MAX(Cust_Code) As Cust_Code from TSPL_RECEIPT_HEADER where Receipt_No='" + fndcheckReceiptNo.Value + "'   group by Cheque_No,Receipt_Date")
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

                    For Each row As DataRow In dt.Rows
                        txt_checkreceiptno.Text = row(0).ToString()
                        txt_receiptAmount.Text = row(1).ToString()
                        dtp_PayRecDate.Value = clsCommon.myCDate(row(2))
                        '' Anubhooti 21-Nov-2014 BM00000004671
                        fndcustomerNo.Value = clsCommon.myCstr(row(3))
                        If clsCommon.myLen(fndcustomerNo.Value) > 0 Then
                            txt_CustomerNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndcustomerNo.Value + "'"))
                        Else
                            txt_CustomerNo.Text = ""
                        End If
                        ''
                    Next
                End If
                'btn_save.Enabled = True
            Else
                'btn_save.Enabled = True
                'btn_save.Text = "&Save"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub



    Private Sub ddl_SourceApplication_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddl_SourceApplication.SelectedIndexChanged
        If ddl_SourceApplication.Text = "Account Payable" Then
            txt_ReceiptReversedocument.Visible = False
            lbl_CustomerNumber.Visible = False
            fndcustomerNo.Visible = False
            txt_CustomerNo.Visible = False
            lbl_CheckReceiptNo.Visible = False
            fndcheckReceiptNo.Visible = False
            txt_checkreceiptno.Visible = True
            lbl_ReceiptAmount.Visible = False
            txt_receiptAmount.Visible = False
            lbl_Receiptdate.Visible = False
            txt_PaymentReverseDocument.Visible = True
            lbl_Vendornumber.Visible = True
            fndVendorNo.Visible = True
            txt_VendorNo.Visible = True
            lbl_checkpaymentno.Visible = True
            fndCheckPaymentNo.Visible = True
            'txt_checkpaymentno.Visible = True
            lbl_Paymentamount.Visible = True
            txt_paymentAmount.Visible = True
            lbl_Paymentdate.Visible = True
            txt_receiptAmount.Visible = False
        Else
            txt_ReceiptReversedocument.Visible = True
            lbl_CustomerNumber.Visible = True
            fndcustomerNo.Visible = True
            txt_CustomerNo.Visible = True
            lbl_CheckReceiptNo.Visible = True
            fndcheckReceiptNo.Visible = True
            txt_checkreceiptno.Visible = True
            lbl_ReceiptAmount.Visible = True
            txt_receiptAmount.Visible = True
            lbl_Receiptdate.Visible = True
            txt_PaymentReverseDocument.Visible = False
            lbl_Vendornumber.Visible = False
            fndVendorNo.Visible = False
            txt_VendorNo.Visible = False
            lbl_checkpaymentno.Visible = False
            fndCheckPaymentNo.Visible = False
            ' txt_checkpaymentno.Visible = False
            lbl_Paymentamount.Visible = False
            txt_paymentAmount.Visible = False
            lbl_Paymentdate.Visible = False
        End If
    End Sub



    Private Sub btn_close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_close.Click
        closeform()
    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub

    '=================Added by preeti Gupta [01/02/2017]
    Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(dtp_reversaldate.Value, Nothing) = False Then
                dtp_reversaldate.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click, RadButton7.Click
        If AllowToSave() Then
            savedata(clicked)
        End If

    End Sub
    Public Sub savedata(ByVal clicked As Boolean)
        Try
            If fndbankcode.Value = "" Then
                myMessages.blankValue(Me, "Bank Code", Me.Text)
                fndbankcode.Focus()
                'ElseIf dtp_PayRecDate.Text = "" Then
                '    myMessages.blankValue("Pay/Rec Date")
                '    dtp_PayRecDate.Focus()
            Else
                If clsCommon.myLen(fndbankcode.Value) > 0 Then
                    Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select RIGHT(BANKACC, 3) from TSPL_BANK_MASTER  Where BANK_CODE='" + fndbankcode.Value + "'")
                    clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Common Services", "Reverse Transaction", LocSegmentCode, dtp_reversaldate.Value, Nothing)
                End If

                ''richa agarwal 3 Aug,2016 to check balance amount of bank
                If CheckNegativeBankBalance() Then
                    If btn_save.Text = "Save" Then
                        savebutton()

                    ElseIf btn_save.Text = "Update" Then

                        If clsCommon.myLen(fndreversecode.Value) > 0 Then
                            Dim strchk As String = "select Post from TSPL_BANK_REVERSE where Reverse_Code='" + fndreversecode.Value + "'"
                            Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                            If chkpost = "P" Then
                                clsCommon.MyMessageBoxShow(Me, "Transaction already posted", Me.Text)
                                Exit Sub
                            End If
                        End If
                        updatebutton()

                    End If
                End If


            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ''richa agarwal create function to check bank balance on save
    Public Function CheckNegativeBankBalance() As Boolean
        If Not isFlag Then
            If clsCommon.CompairString(ddl_SourceApplication.Text, "Account Payable") = CompairStringResult.Equal Then
                Dim strPaymenttype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Payment_Type from TSPL_PAYMENT_HEADER where Payment_No  ='" & fndCheckPaymentNo.Value & "'", Nothing))
                If clsCommon.CompairString(strPaymenttype, "RC") = CompairStringResult.Equal Then
                    Dim Bank_Type_Check As String = "0"
                    Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, Nothing)
                    Dim Bank_Type As String = clsBankMaster.GetBankType(fndbankcode.Value, Nothing)
                    Dim Bank_Balance As Decimal = 0
                    Dim Bank_Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" & fndbankcode.Value & "')", Nothing))
                    If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
                        '' allow for all
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                            Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing)
                            If Bank_Balance < clsCommon.myCdbl(txt_paymentAmount.Text) Then
                                Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_paymentAmount.Text) & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                            End If
                        End If
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                            Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing)
                            If Bank_Balance < clsCommon.myCdbl(txt_paymentAmount.Text) Then
                                Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_paymentAmount.Text) & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                            End If
                        End If
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
                        Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing)
                        If Bank_Balance < clsCommon.myCdbl(txt_paymentAmount.Text) Then
                            Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_paymentAmount.Text) & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                        End If
                    End If
                End If

            Else
                Dim strReceipttype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Receipt_Type  from TSPL_RECEIPT_HEADER where Receipt_No ='" & fndcheckReceiptNo.Value & "'", Nothing))
                If clsCommon.CompairString(strReceipttype, "F") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strReceipttype, "S") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strReceipttype, "A") <> CompairStringResult.Equal Then
                    Dim Bank_Type_Check As String = "0"
                    Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, Nothing)
                    Dim Bank_Type As String = clsBankMaster.GetBankType(fndbankcode.Value, Nothing)
                    Dim Bank_Balance As Decimal = 0
                    Dim Refund_Amount As Decimal = clsCommon.myCdbl(txt_receiptAmount.Text)
                    Dim Bank_Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" & fndbankcode.Value & "')", Nothing))

                    If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
                        '' allow for all
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                            Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing)
                            ''------------
                            If Bank_Balance < Refund_Amount Then
                                Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                            End If
                        End If
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                            Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing)
                            If Bank_Balance < Refund_Amount Then
                                Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                            End If
                        End If
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
                        Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing)
                        If Bank_Balance < Refund_Amount Then
                            Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                        End If
                    End If
                End If

            End If
        End If
        Return True
    End Function


    ''--------------

    ''richa agarwal create function to check bank balance on delete
    Public Function CheckNegativeBankBalanceondelete() As Boolean
        If Not isFlag Then
            If clsCommon.CompairString(ddl_SourceApplication.Text, "Account Payable") = CompairStringResult.Equal Then
                Dim strPaymenttype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Payment_Type from TSPL_PAYMENT_HEADER where Payment_No  ='" & fndCheckPaymentNo.Value & "'", Nothing))
                If clsCommon.CompairString(strPaymenttype, "RC") <> CompairStringResult.Equal Then
                    Dim Bank_Type_Check As String = "0"
                    Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, Nothing)
                    Dim Bank_Type As String = clsBankMaster.GetBankType(fndbankcode.Value, Nothing)
                    Dim Bank_Balance As Decimal = 0
                    Dim Bank_Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" & fndbankcode.Value & "')", Nothing))
                    If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
                        '' allow for all
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                            Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing, True)
                            If Bank_Balance < clsCommon.myCdbl(txt_paymentAmount.Text) Then
                                Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_paymentAmount.Text) & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                            End If
                        End If
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                            Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing, True)
                            If Bank_Balance < clsCommon.myCdbl(txt_paymentAmount.Text) Then
                                Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_paymentAmount.Text) & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                            End If
                        End If
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
                        Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing, True)
                        If Bank_Balance < clsCommon.myCdbl(txt_paymentAmount.Text) Then
                            Throw New Exception("Payment Amount : " & clsCommon.myCdbl(txt_paymentAmount.Text) & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                        End If
                    End If
                End If

            Else
                Dim strReceipttype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Receipt_Type  from TSPL_RECEIPT_HEADER where Receipt_No ='" & fndcheckReceiptNo.Value & "'", Nothing))
                If clsCommon.CompairString(strReceipttype, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(strReceipttype, "S") = CompairStringResult.Equal OrElse clsCommon.CompairString(strReceipttype, "A") = CompairStringResult.Equal Then
                    Dim Bank_Type_Check As String = "0"
                    Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, Nothing)
                    Dim Bank_Type As String = clsBankMaster.GetBankType(fndbankcode.Value, Nothing)
                    Dim Bank_Balance As Decimal = 0
                    Dim Refund_Amount As Decimal = clsCommon.myCdbl(txt_receiptAmount.Text)
                    Dim Bank_Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" & fndbankcode.Value & "')", Nothing))

                    If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
                        '' allow for all
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                            Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing, True)
                            ''------------
                            If Bank_Balance < Refund_Amount Then
                                Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                            End If
                        End If
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                            Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing, True)
                            If Bank_Balance < Refund_Amount Then
                                Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                            End If
                        End If
                    ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
                        Bank_Balance = clsPaymentHeader.GetBankBalance(fndreversecode.Value, dtp_reversaldate.Value, fndbankcode.Value, Bank_Location, Nothing, True)
                        If Bank_Balance < Refund_Amount Then
                            Throw New Exception("Payment Amount : " & Refund_Amount & " Available Bank Balance(" & Bank_Location & ") : " & Bank_Balance & "")
                        End If
                    End If
                End If

            End If
        End If
        Return True
    End Function


    ''--------------

    Private Sub savebutton()
        Try
            isTDSReverse = False
            Dim RvslDate As String = clsCommon.GetPrintDate(dtp_reversaldate.Value, "dd/MMM/yyyy")
            Dim PayRevDate As String = clsCommon.GetPrintDate(dtp_PayRecDate.Value, "dd/MMM/yyyy")
            If ddl_SourceApplication.Text = "Account Payable" Then
                Dim obj As New clsPaymentHeader()
                obj = clsPaymentHeader.GetData(fndCheckPaymentNo.Value, NavigatorType.Current)
                If fndCheckPaymentNo.Value = "" Then
                    myMessages.blankValue(Me, "Payment No", Me.Text)
                    fndCheckPaymentNo.Focus()
                ElseIf txt_paymentAmount.Text = "" Then
                    myMessages.blankValue(Me, "Payment Amount", Me.Text)
                    txt_paymentAmount.Focus()
                ElseIf clsCommon.GetDateWithStartTime(clsCommon.myCDate(obj.Payment_Date)) >= clsCommon.GetDateWithEndTime(dtp_reversaldate.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Reversal Date should be greator than or equal to Payment Date", Me.Text)
                    dtp_reversaldate.Focus()
                Else
                    'Dim tpd As String = Format(dtp_transferpostingdate.Value.ToString(), "dd/MM/yyyy")
                    'Dim STR As String = funautogenerateno()
                    Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select TSPL_GL_ACCOUNTS.Account_Seg_Code7 from TSPL_BANK_MASTER LEFT OUTER JOIN TSPL_GL_ACCOUNTS ON TSPL_GL_ACCOUNTS.Account_Code=TSPL_BANK_MASTER.BANKACC Where BANK_CODE='" + fndbankcode.Value + "'")
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

                    Dim STR As String = clsERPFuncationality.GetNextCode(trans, RvslDate, clsDocType.BankRevseEnty, clsDocTransactionType.Account_Payable, LocSegmentCode, True)
                    trans.Commit()
                    Dim docAmount As Double
                    If objCommonVar.IsDemoERP Then
                        If obj.TDS_Amount > 0 Then
                            If clsCommon.MyMessageBoxShow(Me, "Do you want to reverse TDS Amount also ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.No Then
                                isTDSReverse = False
                                docAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Payment_Amount as DocAmt from TSPL_PAYMENT_HEADER Where Payment_No='" + fndCheckPaymentNo.Value + "'"))
                            Else
                                isTDSReverse = True
                                docAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Payment_Amount+ISNULL(TDS_Amount, 0) as DocAmt from TSPL_PAYMENT_HEADER Where Payment_No='" + fndCheckPaymentNo.Value + "'"))
                            End If
                        Else
                            isTDSReverse = True
                            docAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Payment_Amount+ISNULL(TDS_Amount, 0) as DocAmt from TSPL_PAYMENT_HEADER Where Payment_No='" + fndCheckPaymentNo.Value + "'"))
                        End If
                    Else
                        isTDSReverse = True
                        docAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Payment_Amount+ISNULL(TDS_Amount, 0) as DocAmt from TSPL_PAYMENT_HEADER Where Payment_No='" + fndCheckPaymentNo.Value + "'"))
                    End If

                    Dim dblTDSAmount As Double = 0
                    If isTDSReverse = True Then
                        dblTDSAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(TDS_Amount, 0) as TDS_Amount from TSPL_PAYMENT_HEADER Where Payment_No='" + fndCheckPaymentNo.Value + "'"))
                    End If

                    ''richa agarwal 18 Jan,2017
                    ''richa agarwal 19 Dec,2018 BHA/17/12/18-000756
                    ''If clsDBFuncationality.getSingleValue("select count(*) from TSPL_BANK_REVERSE where 1=1 and Document_No ='" + fndCheckPaymentNo.Value + "' and Reverse_Document ='Payments' ", Nothing) < 1 AndAlso clsDBFuncationality.getSingleValue("select count(*) from tspl_BankReco_Detail where 1=1 and Document_No ='" + fndCheckPaymentNo.Value + "' and isnull(Reconciliation_Status,'') ='C' ", Nothing) < 1 Then
                    If clsDBFuncationality.getSingleValue("select count(*) from TSPL_BANK_REVERSE where 1=1 and Document_No ='" + fndCheckPaymentNo.Value + "' and Reverse_Document ='Payments' ", Nothing) < 1 Then
                        connectSql.RunSp("sp_tspl_bankreverse_insert", New SqlParameter("@Reverse_Code", STR), New SqlParameter("@Reversal_Date", RvslDate), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Back_Acc_No", txt_BankaccountNo.Text), New SqlParameter("@Source_Type", "AP"), New SqlParameter("@Reverse_Document", txt_PaymentReverseDocument.Text), New SqlParameter("@Reason", txt_reason.Text), New SqlParameter("@Vendor_Code", fndVendorNo.Value), New SqlParameter("@Vendor_Name", txt_VendorNo.Text), New SqlParameter("@Cust_Code", ""), New SqlParameter("@Cust_Name", ""), New SqlParameter("@Document_No", fndCheckPaymentNo.Value), New SqlParameter("@Cheque_No", txt_checkpaymentno.Text), New SqlParameter("@Amount", clsCommon.myCstr(docAmount)), New SqlParameter("@Pay_Rec_Date", PayRevDate), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Document already created for Document No " + fndCheckPaymentNo.Value + " or used in Bank Reco. ")
                        fndCheckPaymentNo.Focus()
                        Exit Sub
                    End If
                    ''------------------------

                    'connectSql.RunSp("sp_tspl_bankreverse_insert", New SqlParameter("@Reverse_Code", STR), New SqlParameter("@Reversal_Date", RvslDate), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Back_Acc_No", txt_BankaccountNo.Text), New SqlParameter("@Source_Type", "AP"), New SqlParameter("@Reverse_Document", txt_PaymentReverseDocument.Text), New SqlParameter("@Reason", txt_reason.Text), New SqlParameter("@Vendor_Code", fndVendorNo.Value), New SqlParameter("@Vendor_Name", txt_VendorNo.Text), New SqlParameter("@Cust_Code", ""), New SqlParameter("@Cust_Name", ""), New SqlParameter("@Document_No", fndCheckPaymentNo.Value), New SqlParameter("@Cheque_No", txt_checkpaymentno.Text), New SqlParameter("@Amount", clsCommon.myCstr(docAmount)), New SqlParameter("@Pay_Rec_Date", PayRevDate), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))


                    Dim chkBounce As String = Nothing
                    If chkIsChequeBounce.Checked = True Then
                        chkBounce = "Y"
                    Else
                        chkBounce = "N"
                    End If
                    clsDBFuncationality.ExecuteNonQuery("Update tspl_bank_reverse set isChequeBounce='" & chkBounce & "',Reverse_TDS_Amount=" & dblTDSAmount & "  where Reverse_Code='" & STR & "'")
                    Dim message As String = "Data Saved Successfully" + Environment.NewLine

                    fndreversecode.Value = STR
                    btn_save.Text = "Update"
                    fndreversecode.MyReadOnly = True

                    ''richa 22 Dec,2017
                    funFill4()

                    message += "Do you want to print? "
                    If clicked = False Then
                        If common.clsCommon.MyMessageBoxShow(message, "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                            printData(fndreversecode.Value, fndCheckPaymentNo.Value, fndcheckReceiptNo.Value, fndCheckPaymentNo.Value, fndcheckReceiptNo.Value, "")
                        End If
                    End If
                    btn_delete.Enabled = True
                    btn_post.Enabled = True
                    ddl_SourceApplication.Enabled = False
                    txt_PaymentReverseDocument.Enabled = False
                End If
            Else
                isTDSReverse = True
                Dim receiptdate As String = "select TSPL_RECEIPT_HEADER.Receipt_Date  from TSPL_RECEIPT_HEADER where Receipt_No = '" + fndcheckReceiptNo.Value + "'"
                If fndcheckReceiptNo.Value = "" Then
                    myMessages.blankValue(Me, "Receipt No", Me.Text)
                    fndcheckReceiptNo.Focus()
                ElseIf txt_receiptAmount.Text = "" Then
                    myMessages.blankValue(Me, "Receipt Amount", Me.Text)
                    txt_receiptAmount.Focus()
                ElseIf clsCommon.myCDate(dtp_reversaldate.Value) < clsCommon.myCDate(dtp_PayRecDate.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Reversal Date should be greator than or equal to Receipt Date")
                    dtp_reversaldate.Focus()
                Else
                    'Dim tpd As String = Format(dtp_transferpostingdate.Value.ToString(), "dd/MM/yyyy")
                    'Dim STR As String = funautogenerateno()
                    Dim LocSegmentCode As String = clsDBFuncationality.getSingleValue("Select TSPL_GL_ACCOUNTS.Account_Seg_Code7 from TSPL_BANK_MASTER LEFT OUTER JOIN TSPL_GL_ACCOUNTS ON TSPL_GL_ACCOUNTS.Account_Code=TSPL_BANK_MASTER.BANKACC Where BANK_CODE='" + fndbankcode.Value + "'")
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

                    Dim STR As String = clsERPFuncationality.GetNextCode(trans, RvslDate, clsDocType.BankRevseEnty, clsDocTransactionType.Account_Receivable, LocSegmentCode, True)
                    trans.Commit()

                    ''richa agarwal 18 Jan,2017
                    ''richa agarwal 19 Dec,2018 BHA/17/12/18-000756
                    If clsDBFuncationality.getSingleValue("select count(*) from TSPL_BANK_REVERSE where 1=1 and Document_No ='" + fndcheckReceiptNo.Value + "' and Reverse_Document ='Receipts' ", Nothing) < 1 Then
                        connectSql.RunSp("sp_tspl_bankreverse_insert", New SqlParameter("@Reverse_Code", STR), New SqlParameter("@Reversal_Date", RvslDate), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Back_Acc_No", txt_BankaccountNo.Text), New SqlParameter("@Source_Type", "AR"), New SqlParameter("@Reverse_Document", txt_ReceiptReversedocument.Text), New SqlParameter("@Reason", txt_reason.Text), New SqlParameter("@Vendor_Code", ""), New SqlParameter("@Vendor_Name", ""), New SqlParameter("@Cust_Code", fndcustomerNo.Value), New SqlParameter("@Cust_Name", txt_CustomerNo.Text), New SqlParameter("@Document_No", fndcheckReceiptNo.Value), New SqlParameter("@Cheque_No", txt_checkreceiptno.Text), New SqlParameter("@Amount", txt_receiptAmount.Text), New SqlParameter("@Pay_Rec_Date", PayRevDate), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Document already created for Document No " + fndcheckReceiptNo.Value + " or used in Bank Reco. ")
                        fndcheckReceiptNo.Focus()
                        Exit Sub
                    End If
                    ''------------------------


                    ' connectSql.RunSp("sp_tspl_bankreverse_insert", New SqlParameter("@Reverse_Code", STR), New SqlParameter("@Reversal_Date", RvslDate), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Back_Acc_No", txt_BankaccountNo.Text), New SqlParameter("@Source_Type", "AR"), New SqlParameter("@Reverse_Document", txt_ReceiptReversedocument.Text), New SqlParameter("@Reason", txt_reason.Text), New SqlParameter("@Vendor_Code", ""), New SqlParameter("@Vendor_Name", ""), New SqlParameter("@Cust_Code", fndcustomerNo.Value), New SqlParameter("@Cust_Name", txt_CustomerNo.Text), New SqlParameter("@Document_No", fndcheckReceiptNo.Value), New SqlParameter("@Cheque_No", txt_checkreceiptno.Text), New SqlParameter("@Amount", txt_receiptAmount.Text), New SqlParameter("@Pay_Rec_Date", PayRevDate), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))
                    Dim chkBounce As String = Nothing
                    If chkIsChequeBounce.Checked = True Then
                        chkBounce = "Y"
                    Else
                        chkBounce = "N"
                    End If
                    clsDBFuncationality.ExecuteNonQuery("Update tspl_bank_reverse set isChequeBounce='" & chkBounce & "' where Reverse_Code='" & STR & "'")
                    Dim message As String = "Data Saved Successfully" + Environment.NewLine

                    fndreversecode.Value = STR
                    btn_save.Text = "Update"
                    fndreversecode.MyReadOnly = True
                    ''richa 22 Dec,2017
                    funFill4()
                    message += "Do you want to print? "
                    If clicked = False Then
                        If common.clsCommon.MyMessageBoxShow(message, "", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
                            printData(fndreversecode.Value, fndCheckPaymentNo.Value, fndcheckReceiptNo.Value, fndCheckPaymentNo.Value, fndcheckReceiptNo.Value, "")
                        End If
                    End If
                    btn_delete.Enabled = True
                    btn_post.Enabled = True
                    ddl_SourceApplication.Enabled = False
                    txt_ReceiptReversedocument.Enabled = False
                End If
            End If

            '===============================update by preeti gupta Aginst ticket no[BM00000009001]
            If clsCommon.myLen(fndCheckPaymentNo.Value) > 0 Then
                Dim qrychk As String = "update TSPL_PAYMENT_HEADER set IsChkReverse='Y' where Payment_No='" + fndCheckPaymentNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qrychk)

            End If
            If clsCommon.myLen(fndcheckReceiptNo.Value) > 0 Then
                Dim qrychkRec As String = "update TSPL_RECEIPT_HEADER set IsChkReverse='Y' where Receipt_No='" + fndcheckReceiptNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qrychkRec)

            End If

            '==================================================END==================================

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub updatebutton()
        Try
            isTDSReverse = False
            isTaxReverse = False
            Dim RvslDate As String = clsCommon.GetPrintDate(dtp_reversaldate.Value, "dd/MMM/yyyy")
            Dim PayRevDate As String = clsCommon.GetPrintDate(dtp_PayRecDate.Value, "dd/MMM/yyyy")

            '===============================update by richa agarwal 2 July,2018 ticket no. KDI/02/07/18-000383
            Dim strPaymentOrreciptno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_No from TSPL_BANK_REVERSE where Reverse_Code ='" & fndreversecode.Value & "'"))
            clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set IsChkReverse='N' where Payment_No='" + strPaymentOrreciptno + "'")
            clsDBFuncationality.ExecuteNonQuery("update TSPL_RECEIPT_HEADER set IsChkReverse='N' where Receipt_No='" + strPaymentOrreciptno + "'")
            '======================================END==================================================

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndreversecode.Value, "TSPL_BANK_REVERSE", "Reverse_Code", Nothing)

            If ddl_SourceApplication.Text = "Account Payable" Then
                Dim obj As New clsPaymentHeader()
                obj = clsPaymentHeader.GetData(fndCheckPaymentNo.Value, NavigatorType.Current)
                If fndCheckPaymentNo.Value = "" Then
                    myMessages.blankValue(Me, "Payment No", Me.Text)
                    fndCheckPaymentNo.Focus()
                ElseIf txt_paymentAmount.Text = "" Then
                    myMessages.blankValue(Me, "Payment Amount", Me.Text)
                    txt_paymentAmount.Focus()
                ElseIf clsCommon.GetDateWithStartTime(clsCommon.myCDate(obj.Payment_Date)) > clsCommon.GetDateWithEndTime(dtp_reversaldate.Value.ToString()) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Reversal Date should be greator than Payment Date")
                    dtp_reversaldate.Focus()
                Else
                    Dim docAmount As Double
                    If objCommonVar.IsDemoERP Then
                        If obj.TDS_Amount > 0 Then
                            If clsCommon.MyMessageBoxShow("Do you want to reverse TDS Amount also ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.No Then
                                isTDSReverse = False
                                docAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Payment_Amount as DocAmt from TSPL_PAYMENT_HEADER Where Payment_No='" + fndCheckPaymentNo.Value + "'"))
                            Else
                                isTDSReverse = True
                                docAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Payment_Amount+ISNULL(TDS_Amount, 0) as DocAmt from TSPL_PAYMENT_HEADER Where Payment_No='" + fndCheckPaymentNo.Value + "'"))
                            End If
                        Else
                            isTDSReverse = True
                            docAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Payment_Amount+ISNULL(TDS_Amount, 0) as DocAmt from TSPL_PAYMENT_HEADER Where Payment_No='" + fndCheckPaymentNo.Value + "'"))
                        End If
                    Else
                        isTDSReverse = True
                        docAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Payment_Amount+ISNULL(TDS_Amount, 0) as DocAmt from TSPL_PAYMENT_HEADER Where Payment_No='" + fndCheckPaymentNo.Value + "'"))
                    End If

                    ''richa 23 june,2017

                    If obj.TAX1_Amt > 0 Then
                        If clsCommon.MyMessageBoxShow(Me, "Do you want to reverse Tax Amount also ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.No Then
                            isTaxReverse = False
                        Else
                            isTaxReverse = True
                        End If
                    Else
                        isTaxReverse = True
                    End If

                    Dim dblTDSAmount As Double = 0
                    If isTDSReverse = True Then
                        dblTDSAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select ISNULL(TDS_Amount, 0) as TDS_Amount from TSPL_PAYMENT_HEADER Where Payment_No='" + fndCheckPaymentNo.Value + "'"))
                    End If

                    ''richa agarwal 18 Jan,2017
                    ''richa agarwal 19 Dec,2018 BHA/17/12/18-000756
                    If clsDBFuncationality.getSingleValue("select count(*) from TSPL_BANK_REVERSE where 1=1 and Document_No ='" + fndCheckPaymentNo.Value + "' and Reverse_Document ='Payments' and Reverse_Code<>'" & clsCommon.myCstr(fndreversecode.Value) & "' ", Nothing) < 1 Then
                        connectSql.RunSp("sp_tspl_bankreverse_update", New SqlParameter("@Reverse_Code", fndreversecode.Value), New SqlParameter("@Reversal_Date", RvslDate), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Back_Acc_No", txt_BankaccountNo.Text), New SqlParameter("@Source_Type", "AP"), New SqlParameter("@Reverse_Document", txt_PaymentReverseDocument.Text), New SqlParameter("@Reason", txt_reason.Text), New SqlParameter("@Vendor_Code", fndVendorNo.Value), New SqlParameter("@Vendor_Name", txt_VendorNo.Text), New SqlParameter("@Cust_Code", ""), New SqlParameter("@Cust_Name", ""), New SqlParameter("@Document_No", fndCheckPaymentNo.Value), New SqlParameter("@Cheque_No", txt_checkpaymentno.Text), New SqlParameter("@Amount", clsCommon.myCstr(docAmount)), New SqlParameter("@Pay_Rec_Date", PayRevDate), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Document already created for Document No " + fndCheckPaymentNo.Value + " or used in Bank Reco. ")
                        fndCheckPaymentNo.Focus()
                        Exit Sub
                    End If
                    ''------------------------

                    '  connectSql.RunSp("sp_tspl_bankreverse_update", New SqlParameter("@Reverse_Code", fndreversecode.Value), New SqlParameter("@Reversal_Date", RvslDate), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Back_Acc_No", txt_BankaccountNo.Text), New SqlParameter("@Source_Type", "AP"), New SqlParameter("@Reverse_Document", txt_PaymentReverseDocument.Text), New SqlParameter("@Reason", txt_reason.Text), New SqlParameter("@Vendor_Code", fndVendorNo.Value), New SqlParameter("@Vendor_Name", txt_VendorNo.Text), New SqlParameter("@Cust_Code", ""), New SqlParameter("@Cust_Name", ""), New SqlParameter("@Document_No", fndCheckPaymentNo.Value), New SqlParameter("@Cheque_No", txt_checkpaymentno.Text), New SqlParameter("@Amount", clsCommon.myCstr(docAmount)), New SqlParameter("@Pay_Rec_Date", PayRevDate), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))
                    Dim chkBounce As String = Nothing
                    If chkIsChequeBounce.Checked = True Then
                        chkBounce = "Y"
                    Else
                        chkBounce = "N"
                    End If
                    clsDBFuncationality.ExecuteNonQuery("Update tspl_bank_reverse set isChequeBounce='" & chkBounce & "',Reverse_TDS_Amount=" & dblTDSAmount & " where Reverse_Code='" & fndreversecode.Value.ToString & "'")
                    'clsDBFuncationality.ExecuteNonQuery("Update tspl_bank_reverse set isChequeBounce='" & IIf(chkIsChequeBounce.Checked = True, "Y", "N") & "' where Reverse_Code='" & clsCommon.myCstr(fndreversecode.Value) & "'")
                    If clicked = False Then
                        myMessages.update()
                    End If

                    btn_save.Text = "Update"
                    fndreversecode.MyReadOnly = True
                    btn_delete.Enabled = True
                    btn_post.Enabled = True
                End If

            Else
                isTDSReverse = True
                Dim receiptdate As String = "select TSPL_RECEIPT_HEADER.Receipt_Date  from TSPL_RECEIPT_HEADER where Receipt_No = '" + fndcheckReceiptNo.Value + "'"
                If fndcheckReceiptNo.Value = "" Then
                    myMessages.blankValue(Me, "Payment No", Me.Text)
                    fndcheckReceiptNo.Focus()
                ElseIf txt_receiptAmount.Text = "" Then
                    myMessages.blankValue(Me, "Receipt Amount", Me.Text)
                    txt_receiptAmount.Focus()
                    'ElseIf clsCommon.GetDateWithStartTime(clsCommon.myCDate(clsDBFuncationality.getSingleValue(receiptdate))) > clsCommon.GetDateWithEndTime(dtp_reversaldate.Value.ToString()) Then
                    '    common.clsCommon.MyMessageBoxShow("Reversal Date should be greator than Receipt Date")
                    '    dtp_reversaldate.Focus()
                ElseIf clsCommon.myCDate(dtp_reversaldate.Value) < clsCommon.myCDate(dtp_PayRecDate.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Reversal Date should be greator than or equal to Receipt Date", Me.Text)
                    dtp_reversaldate.Focus()
                Else
                    'Dim tpd As String = Format(dtp_transferpostingdate.Value, "dd/MM/yyyy")

                    ''richa agarwal 19 Jan,2017
                    ''richa agarwal 19 Dec,2018 BHA/17/12/18-000756
                    If clsDBFuncationality.getSingleValue("select count(*) from TSPL_BANK_REVERSE where 1=1 and Document_No ='" + fndcheckReceiptNo.Value + "' and Reverse_Document ='Receipts' and Reverse_Code<>'" & clsCommon.myCstr(fndreversecode.Value) & "' ", Nothing) < 1 Then
                        connectSql.RunSp("sp_tspl_bankreverse_update", New SqlParameter("@Reverse_Code", fndreversecode.Value), New SqlParameter("@Reversal_Date", RvslDate), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Back_Acc_No", txt_BankaccountNo.Text), New SqlParameter("@Source_Type", "AR"), New SqlParameter("@Reverse_Document", txt_ReceiptReversedocument.Text), New SqlParameter("@Reason", txt_reason.Text), New SqlParameter("@Vendor_Code", ""), New SqlParameter("@Vendor_Name", ""), New SqlParameter("@Cust_Code", fndcustomerNo.Value), New SqlParameter("@Cust_Name", txt_CustomerNo.Text), New SqlParameter("@Document_No", fndcheckReceiptNo.Value), New SqlParameter("@Cheque_No", txt_checkreceiptno.Text), New SqlParameter("@Amount", txt_receiptAmount.Text), New SqlParameter("@Pay_Rec_Date", PayRevDate), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Document already created for Document No " + fndcheckReceiptNo.Value + " or used in Bank Reco. ")
                        fndcheckReceiptNo.Focus()
                        Exit Sub
                    End If
                    ''------------------------
                    ''richa 23 june,2017

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select TAX1_Amt from TSPL_RECEIPT_HEADER  where Receipt_No ='" + fndcheckReceiptNo.Value + "'", Nothing)) > 0 Then
                        If clsCommon.MyMessageBoxShow(Me, "Do you want to reverse Tax Amount also ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.No Then
                            isTaxReverse = False
                        Else
                            isTaxReverse = True
                        End If
                    Else
                        isTaxReverse = True
                    End If


                    ' connectSql.RunSp("sp_tspl_bankreverse_update", New SqlParameter("@Reverse_Code", fndreversecode.Value), New SqlParameter("@Reversal_Date", RvslDate), New SqlParameter("@Bank_Code", fndbankcode.Value), New SqlParameter("@Back_Acc_No", txt_BankaccountNo.Text), New SqlParameter("@Source_Type", "AR"), New SqlParameter("@Reverse_Document", txt_ReceiptReversedocument.Text), New SqlParameter("@Reason", txt_reason.Text), New SqlParameter("@Vendor_Code", ""), New SqlParameter("@Vendor_Name", ""), New SqlParameter("@Cust_Code", fndcustomerNo.Value), New SqlParameter("@Cust_Name", txt_CustomerNo.Text), New SqlParameter("@Document_No", fndcheckReceiptNo.Value), New SqlParameter("@Cheque_No", txt_checkreceiptno.Text), New SqlParameter("@Amount", txt_receiptAmount.Text), New SqlParameter("@Pay_Rec_Date", PayRevDate), New SqlParameter("@Post", "n"), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@comp_code", companyCode))
                    Dim chkBounce As String = Nothing
                    If chkIsChequeBounce.Checked = True Then
                        chkBounce = "Y"
                    Else
                        chkBounce = "N"
                    End If
                    clsDBFuncationality.ExecuteNonQuery("Update tspl_bank_reverse set isChequeBounce='" & chkBounce & "' where Reverse_Code='" & fndreversecode.Value.ToString & "'")
                    If clicked = False Then
                        myMessages.update()
                    End If

                    btn_save.Text = "Update"
                    fndreversecode.MyReadOnly = True
                    btn_delete.Enabled = True
                    btn_post.Enabled = True
                End If
            End If
            '===============================update by preeti gupta Aginst ticket no[BM00000009001]


            If clsCommon.myLen(fndCheckPaymentNo.Value) > 0 Then
                Dim qrychk As String = "update TSPL_PAYMENT_HEADER set IsChkReverse='Y' where Payment_No='" + fndCheckPaymentNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qrychk)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndCheckPaymentNo.Value, "TSPL_PAYMENT_HEADER", "Payment_No", Nothing)
            End If
            If clsCommon.myLen(fndcheckReceiptNo.Value) > 0 Then
                Dim qrychkRec As String = "update TSPL_RECEIPT_HEADER set IsChkReverse='Y' where Receipt_No='" + fndcheckReceiptNo.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qrychkRec)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndcheckReceiptNo.Value, "TSPL_RECEIPT_HEADER", "Receipt_No", Nothing)
            End If





            '============================================END============================================
            funFill4()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btn_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_delete.Click, RadButton8.Click
        deletedata()
    End Sub
    Public Sub deletedata()
        Try
            If myMessages.deleteConfirm() Then
                If CheckNegativeBankBalanceondelete() Then
                    fundelete()
                End If

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub fundelete()
        Try
            '===============================update by richa agarwal 2 July,2018 ticket no. KDI/02/07/18-000383
            Dim strqry As String = "select Reverse_Code, Document_No, Reversal_Date,Bank_Code, Back_Acc_No, Post, Source_Type ,amount,Vendor_Code,Vendor_Name,Cust_Code,Cust_Name from TSPL_BANK_REVERSE where Reverse_Code='" & fndreversecode.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Document No. not found to delete")
            End If
            If clsCommon.CompairString("p", clsCommon.myCstr(dt.Rows(0)("Post"))) = CompairStringResult.Equal Then
                Throw New Exception("Already Posted Document")
            End If

            Dim strPaymentOrreciptno As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Document_No from TSPL_BANK_REVERSE where Reverse_Code ='" & fndreversecode.Value & "'"))
            clsDBFuncationality.ExecuteNonQuery("update TSPL_PAYMENT_HEADER set IsChkReverse='N' where Payment_No='" + strPaymentOrreciptno + "'")
            clsDBFuncationality.ExecuteNonQuery("update TSPL_RECEIPT_HEADER set IsChkReverse='N' where Receipt_No='" + strPaymentOrreciptno + "'")
            '======================================END==================================================

            'Ticket No  TEC/10/09/19-001007 Sanjay
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, fndreversecode.Value, "TSPL_BANK_REVERSE", "Reverse_Code", Nothing)

            connectSql.RunSp("sp_tspl_bankreverse_delete", New SqlParameter("@Reverse_Code", fndreversecode.Value))
            myMessages.delete()
            btn_delete.Enabled = False
            btn_post.Enabled = False
            btn_save.Text = "Save"

            fndreversecode.Focus()

            '===============================update by preeti gupta Aginst ticket no[BM00000009001]
            Dim qrychk As String = "update TSPL_PAYMENT_HEADER set IsChkReverse='N' where Payment_No='" + fndCheckPaymentNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qrychk)
            Dim qrychkRec As String = "update TSPL_RECEIPT_HEADER set IsChkReverse='N' where Receipt_No='" + fndcheckReceiptNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qrychkRec)
            '======================================END==================================================

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndreversecode.Value, "TSPL_BANK_REVERSE", "Reverse_Code", Nothing)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndcheckReceiptNo.Value, "TSPL_RECEIPT_HEADER", "Receipt_No", Nothing)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndCheckPaymentNo.Value, "TSPL_PAYMENT_HEADER", "Payment_No", Nothing)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btn_reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        resetdata()
    End Sub
    Public Sub resetdata()
        isTDSReverse = False
        fundata()
        fndreversecode.Value = ""
        fndbankcode.Value = ""
        txt_Bankcode.Text = ""
        txt_BankaccountNo.Text = ""
        txt_reason.Text = ""
        dtp_reversaldate.Value = clsCommon.GETSERVERDATE()
        fndreversecode.MyReadOnly = False
        txt_PaymentReverseDocument.Text = "Payments"
        fndcustomerNo.Value = ""
        fndVendorNo.Value = ""
        txt_CustomerNo.Text = ""
        txt_VendorNo.Text = ""
        fndCheckPaymentNo.Value = ""
        fndcheckReceiptNo.Value = ""
        txt_paymentAmount.Enabled = False
        txt_receiptAmount.Enabled = False
        ddl_SourceApplication.Enabled = True
        txt_checkreceiptno.Text = ""
        txt_checkpaymentno.Text = ""
        txt_receiptAmount.Text = ""
        txt_paymentAmount.Text = ""
        dtp_PayRecDate.Value = clsCommon.GETSERVERDATE()
        btn_save.Text = "Save"
        ddl_SourceApplication.Text = "Account Payable"
        txt_ReceiptReversedocument.Visible = False
        lbl_CustomerNumber.Visible = False
        fndcustomerNo.Visible = False
        txt_CustomerNo.Visible = False
        lbl_CheckReceiptNo.Visible = False
        fndcheckReceiptNo.Visible = False
        txt_checkreceiptno.Visible = True
        lbl_ReceiptAmount.Visible = False
        txt_receiptAmount.Visible = False
        lbl_Receiptdate.Visible = False
        txt_PaymentReverseDocument.Visible = True
        lbl_Vendornumber.Visible = True
        fndVendorNo.Visible = True
        txt_VendorNo.Visible = True
        lbl_checkpaymentno.Visible = True
        fndCheckPaymentNo.Visible = True
        ' txt_checkpaymentno.Visible = True
        lbl_Paymentamount.Visible = True
        txt_paymentAmount.Visible = True
        lbl_Paymentdate.Visible = True
        btn_delete.Enabled = False
        btn_post.Enabled = False
        chkIsChequeBounce.Checked = False
        fndreversecode.Focus()
        fndVendorNo.Enabled = True
        fndcustomerNo.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        '===============================update by preeti gupta Aginst ticket no[BM00000009001]
        btnReverseTransaction.Visible = False
        '=======================================END========================================
    End Sub
    Private Sub TextChanged4(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim s As String
            s = clsDBFuncationality.getSingleValue("select Reverse_Code from TSPL_BANK_REVERSE where [Reverse_Code] ='" + fndreversecode.Value + "'")

            'dr = connectSql.RunSqlReturnDR("select Reverse_Code from TSPL_BANK_REVERSE where [Reverse_Code] ='" + fndreversecode.Value + "'")

            'While dr.Read()

            'End While
            If s <> "" Then
                funFill4()
            Else
                fndbankcode.Value = ""
                txt_Bankcode.Text = ""
                txt_BankaccountNo.Text = ""
                txt_reason.Text = ""
                dtp_reversaldate.Value = clsCommon.GETSERVERDATE()
                fndcustomerNo.Value = ""
                fndVendorNo.Value = ""
                txt_CustomerNo.Text = ""
                txt_VendorNo.Text = ""
                fndCheckPaymentNo.Value = ""
                fndcheckReceiptNo.Value = ""
                txt_receiptAmount.Text = ""
                txt_paymentAmount.Text = ""
                dtp_PayRecDate.Value = clsCommon.GETSERVERDATE()
                btn_save.Enabled = True
                btn_save.Text = "Save"
            End If
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
        ddl_SourceApplication.Enabled = False
    End Sub
    Public Sub funFill4()
        Try
            UsLock1.Status = ERPTransactionStatus.Pending
            If fndreversecode.Value <> "" Then
                UsLock1.Status = ERPTransactionStatus.Pending
                Dim str As String = connectSql.RunScalar("select Reverse_Document  from TSPL_BANK_REVERSE where Reverse_Code = '" + fndreversecode.Value + "'")
                'Dim str123 As String = connectSql.RunScalar("select Reverse_Document  from tspl_payment_header inner join TSPL_BANK_REVERSE on tspl_payment_header.bank_code = TSPL_BANK_REVERSE.bank_code where Reverse_Code = '" + fnd_reversecode.txtValue.Text + "'")
                If str.Trim() = "Payments" Then
                    'If Not String.IsNullOrEmpty(str123) Then
                    Dim post As String = connectSql.RunScalar("select post from TSPL_BANK_REVERSE where Reverse_Code = '" + fndreversecode.Value + "'")
                    If Not String.IsNullOrEmpty(post) Then

                        If post.Trim() = "P" Then
                            UsLock1.Status = ERPTransactionStatus.Approved

                            ds = connectSql.RunSQLReturnDS("select Reverse_Code,Bank_Code,Back_Acc_No,Reverse_Document,Reason,Reversal_Date,Vendor_Code,Vendor_Name,Cheque_No,Amount,Pay_Rec_Date,Document_No,isChequeBounce from TSPL_BANK_REVERSE where Reverse_Code = '" + fndreversecode.Value + "'")
                            Dim dr As DataRow = ds.Tables(0).Rows(0)
                            fndreversecode.Value = dr(0).ToString()
                            fndbankcode.Value = dr(1).ToString()
                            'txt_Bankcode.Text = dr(2).ToString()
                            txt_BankaccountNo.Text = dr(2).ToString()
                            txt_PaymentReverseDocument.Text = dr(3).ToString()
                            txt_reason.Text = dr(4).ToString()
                            dtp_reversaldate.Value = dr(5).ToString()
                            fndVendorNo.Value = dr(6).ToString()
                            txt_VendorNo.Text = dr(7).ToString()
                            txt_checkpaymentno.Text = dr(8).ToString()
                            txt_paymentAmount.Text = dr(9).ToString()
                            dtp_PayRecDate.Value = clsCommon.GetPrintDate(dr(10).ToString(), "dd/MM/yyyy")
                            fndCheckPaymentNo.Value = dr(11).ToString()
                            ddl_SourceApplication.Text = "Account Payable"
                            fndVendorNo.Visible = True
                            fndcustomerNo.Visible = False
                            lbl_Vendornumber.Visible = True
                            lbl_CustomerNumber.Visible = False
                            txt_VendorNo.Visible = True
                            txt_CustomerNo.Visible = False
                            lbl_ReceiptAmount.Visible = False
                            lbl_Paymentamount.Visible = True
                            lbl_Paymentdate.Visible = True
                            lbl_Receiptdate.Visible = False
                            fndCheckPaymentNo.Visible = True
                            fndcheckReceiptNo.Visible = False
                            lbl_checkpaymentno.Visible = True
                            lbl_CheckReceiptNo.Visible = False
                            If clsCommon.CompairString(dr("isChequeBounce").ToString, "Y") = CompairStringResult.Equal Then
                                chkIsChequeBounce.Checked = True
                            Else
                                chkIsChequeBounce.Checked = False
                            End If
                            txt_checkreceiptno.Visible = False
                            txt_checkpaymentno.Visible = True

                            txt_paymentAmount.Visible = True
                            txt_receiptAmount.Visible = False
                            txt_PaymentReverseDocument.Visible = True
                            txt_ReceiptReversedocument.Visible = False
                            btn_save.Text = "Update"
                            fndreversecode.MyReadOnly = True
                            funposteddata()
                            '' Anubhooti 24-Nov-2014 BM00000004671
                            Dim PaymentType As String
                            PaymentType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Payment_Type,'') As Payment_Type From TSPL_PAYMENT_HEADER Where Payment_No='" & clsCommon.myCstr(dr(11)) & "'"))
                            If clsCommon.CompairString(PaymentType, "MI") = CompairStringResult.Equal Then
                                fndVendorNo.Enabled = False
                            Else
                                fndVendorNo.Enabled = True
                            End If
                            ''
                        Else
                            ds = connectSql.RunSQLReturnDS("select Reverse_Code,Bank_Code,Back_Acc_No,Reverse_Document,Reason,Reversal_Date,Vendor_Code,Vendor_Name,Cheque_No,Amount,Pay_Rec_Date,Document_No, IsChequeBounce from TSPL_BANK_REVERSE where Reverse_Code = '" + fndreversecode.Value + "'")
                            Dim dr As DataRow = ds.Tables(0).Rows(0)
                            fndreversecode.Value = dr(0).ToString()
                            fndbankcode.Value = dr(1).ToString()
                            'txt_Bankcode.Text = dr(2).ToString()
                            txt_BankaccountNo.Text = dr(2).ToString()
                            txt_PaymentReverseDocument.Text = dr(3).ToString()
                            txt_reason.Text = dr(4).ToString()
                            dtp_reversaldate.Value = dr(5).ToString()
                            fndVendorNo.Value = dr(6).ToString()
                            txt_VendorNo.Text = dr(7).ToString()
                            txt_checkpaymentno.Text = dr(8).ToString()
                            txt_paymentAmount.Text = dr(9).ToString()
                            dtp_PayRecDate.Value = clsCommon.GetPrintDate(dr(10).ToString().Trim(), "dd/MM/yyyy")
                            fndCheckPaymentNo.Value = dr(11).ToString()
                            If clsCommon.CompairString(dr("IsChequeBounce").ToString, "Y") = CompairStringResult.Equal Then
                                chkIsChequeBounce.Checked = True
                            Else
                                chkIsChequeBounce.Checked = False
                            End If
                            'fndCheckPaymentNo.Value = dr(8).ToString()
                            ddl_SourceApplication.Text = "Account Payable"
                            fndVendorNo.Visible = True
                            fndcustomerNo.Visible = False
                            lbl_Vendornumber.Visible = True
                            lbl_CustomerNumber.Visible = False
                            txt_VendorNo.Visible = True
                            txt_CustomerNo.Visible = False
                            lbl_ReceiptAmount.Visible = False
                            lbl_Paymentamount.Visible = True
                            lbl_Paymentdate.Visible = True
                            lbl_Receiptdate.Visible = False
                            fndCheckPaymentNo.Visible = True
                            fndcheckReceiptNo.Visible = False
                            lbl_checkpaymentno.Visible = True
                            lbl_CheckReceiptNo.Visible = False

                            txt_checkreceiptno.Visible = False
                            txt_checkpaymentno.Visible = True

                            txt_paymentAmount.Visible = True
                            txt_receiptAmount.Visible = False
                            'txt_PaymentReverseDocument.Visible = True
                            'txt_ReceiptReversedocument.Visible = False
                            btn_save.Text = "Update"
                            fndreversecode.MyReadOnly = True
                            fundata()
                            '' Anubhooti 24-Nov-2014 BM00000004671
                            Dim PaymentType As String
                            PaymentType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Payment_Type,'') As Payment_Type From TSPL_PAYMENT_HEADER Where Payment_No='" & clsCommon.myCstr(dr(11)) & "'"))
                            If clsCommon.CompairString(PaymentType, "MI") = CompairStringResult.Equal Then
                                fndVendorNo.Enabled = False
                            Else
                                fndVendorNo.Enabled = True
                            End If
                            ''
                        End If
                    End If
                Else
                    Dim post As String = connectSql.RunScalar("select post from TSPL_BANK_REVERSE where Reverse_Code = '" + fndreversecode.Value + "'")
                    If Not String.IsNullOrEmpty(post) Then
                        If post.Trim() = "P" Then
                            UsLock1.Status = ERPTransactionStatus.Approved
                            ds = connectSql.RunSQLReturnDS("select Reverse_Code,Bank_Code,Back_Acc_No,Reverse_Document,Reason,Reversal_Date,Cust_Code,Cust_Name,Cheque_No,Amount,Pay_Rec_Date,Document_No,IsChequeBounce from TSPL_BANK_REVERSE where Reverse_Code = '" + fndreversecode.Value + "'")
                            Dim dr As DataRow = ds.Tables(0).Rows(0)
                            fndreversecode.Value = dr(0).ToString()
                            fndbankcode.Value = dr(1).ToString()
                            'txt_Bankcode.Text = dr(2).ToString()
                            txt_BankaccountNo.Text = dr(2).ToString()
                            txt_ReceiptReversedocument.Text = dr(3).ToString()
                            txt_reason.Text = dr(4).ToString()
                            dtp_reversaldate.Value = dr(5).ToString()
                            fndcustomerNo.Value = dr(6).ToString()
                            txt_CustomerNo.Text = dr(7).ToString()
                            txt_checkreceiptno.Text = dr(8).ToString()
                            txt_receiptAmount.Text = dr(9).ToString()
                            dtp_PayRecDate.Value = clsCommon.GetPrintDate(dr(10).ToString(), "dd/MM/yyyy")
                            fndcheckReceiptNo.Value = dr(11).ToString()
                            ddl_SourceApplication.Text = "Account Receivable"
                            fndVendorNo.Visible = False
                            fndcustomerNo.Visible = True
                            lbl_Vendornumber.Visible = False
                            lbl_CustomerNumber.Visible = True
                            txt_VendorNo.Visible = False
                            txt_CustomerNo.Visible = True
                            lbl_ReceiptAmount.Visible = True
                            lbl_Paymentamount.Visible = False
                            lbl_Paymentdate.Visible = False
                            lbl_Receiptdate.Visible = True
                            fndCheckPaymentNo.Visible = False
                            fndcheckReceiptNo.Visible = True
                            lbl_checkpaymentno.Visible = False
                            lbl_CheckReceiptNo.Visible = True

                            txt_checkreceiptno.Visible = True
                            txt_checkpaymentno.Visible = False
                            If clsCommon.CompairString(dr("IsChequeBounce").ToString, "Y") = CompairStringResult.Equal Then
                                chkIsChequeBounce.Checked = True
                            Else
                                chkIsChequeBounce.Checked = False
                            End If
                            txt_paymentAmount.Visible = False
                            txt_receiptAmount.Visible = True
                            txt_PaymentReverseDocument.Visible = False
                            txt_ReceiptReversedocument.Visible = True
                            btn_save.Text = "Update"
                            fndreversecode.MyReadOnly = True
                            funposteddata()
                            '' Anubhooti 24-Nov-2014 BM00000004671
                            Dim ReceiptType As String
                            ReceiptType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Receipt_Type ,'') As Receipt_Type From TSPL_RECEIPT_HEADER Where Receipt_No ='" & clsCommon.myCstr(dr(11)) & "'"))
                            If clsCommon.CompairString(ReceiptType, "M") = CompairStringResult.Equal Then
                                fndcustomerNo.Enabled = False
                            Else
                                fndcustomerNo.Enabled = True
                            End If
                            ''
                        Else
                            ds = connectSql.RunSQLReturnDS("select Reverse_Code,Bank_Code,Back_Acc_No,Reverse_Document,Reason,Reversal_Date,Cust_Code,Cust_Name,Cheque_No,Amount,Pay_Rec_Date,Document_No,IsChequeBounce from TSPL_BANK_REVERSE where Reverse_Code = '" + fndreversecode.Value + "'")
                            Dim dr As DataRow = ds.Tables(0).Rows(0)
                            fndreversecode.Value = dr(0).ToString()
                            fndbankcode.Value = dr(1).ToString()
                            'txt_Bankcode.Text = dr(2).ToString()
                            txt_BankaccountNo.Text = dr(2).ToString()
                            txt_ReceiptReversedocument.Text = dr(3).ToString()
                            txt_reason.Text = dr(4).ToString()
                            dtp_reversaldate.Value = dr(5).ToString()
                            fndcustomerNo.Value = dr(6).ToString()
                            txt_CustomerNo.Text = dr(7).ToString()
                            txt_checkreceiptno.Text = dr(8).ToString()
                            txt_receiptAmount.Text = dr(9).ToString()
                            dtp_PayRecDate.Value = clsCommon.GetPrintDate(dr(10).ToString(), "dd/MM/yyyy")
                            fndcheckReceiptNo.Value = dr(11).ToString()
                            ddl_SourceApplication.Text = "Account Receivable"
                            fndVendorNo.Visible = False
                            fndcustomerNo.Visible = True
                            lbl_Vendornumber.Visible = False
                            lbl_CustomerNumber.Visible = True
                            txt_VendorNo.Visible = False
                            txt_CustomerNo.Visible = True
                            lbl_ReceiptAmount.Visible = True
                            lbl_Paymentamount.Visible = False
                            lbl_Paymentdate.Visible = False
                            lbl_Receiptdate.Visible = True
                            fndCheckPaymentNo.Visible = False
                            fndcheckReceiptNo.Visible = True
                            lbl_checkpaymentno.Visible = False
                            lbl_CheckReceiptNo.Visible = True
                            If clsCommon.CompairString(dr("IsChequeBounce").ToString, "Y") = CompairStringResult.Equal Then
                                chkIsChequeBounce.Checked = True
                            Else
                                chkIsChequeBounce.Checked = False
                            End If
                            txt_checkreceiptno.Visible = True
                            txt_checkpaymentno.Visible = False

                            txt_paymentAmount.Visible = False
                            txt_receiptAmount.Visible = True
                            txt_PaymentReverseDocument.Visible = False
                            txt_ReceiptReversedocument.Visible = True
                            btn_save.Text = "Update"
                            fndreversecode.MyReadOnly = True
                            fundata()
                            '' Anubhooti 24-Nov-2014 BM00000004671
                            Dim ReceiptType As String
                            ReceiptType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Receipt_Type ,'') As Receipt_Type From TSPL_RECEIPT_HEADER Where Receipt_No ='" & clsCommon.myCstr(dr(11)) & "'"))
                            If clsCommon.CompairString(ReceiptType, "M") = CompairStringResult.Equal Then
                                fndcustomerNo.Enabled = False
                            Else
                                fndcustomerNo.Enabled = True
                            End If
                            ''
                        End If
                    End If
                End If
            End If
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''To Call The post function 
    Private Sub btn_post_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_post.Click, RadButton6.Click
        Try
            '===============================update by richa agarwal 6 July,2018 ticket no. KDI/03/07/18-000384
            Dim strqry As String = "select Reverse_Code, Document_No, Reversal_Date,Bank_Code, Back_Acc_No, Post, Source_Type ,amount,Vendor_Code,Vendor_Name,Cust_Code,Cust_Name from TSPL_BANK_REVERSE where Reverse_Code='" & fndreversecode.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Document No. not found to post")
            End If
            clicked = True
            postdata()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub UpdateInvoiceBal()
        Try
            Dim qry As String = Nothing
            Dim dt As DataTable = Nothing
            Dim Doc_No As String = Nothing
            Dim AppliedAmt As Decimal = 0
            Dim BalAmt As Decimal = 0
            If ddl_SourceApplication.SelectedIndex = 0 Then
                qry = "select Document_No ,Applied_Amount  from TSPL_PAYMENT_DETAIL where Payment_No ='" + fndCheckPaymentNo.Value + "'"
                dt = clsDBFuncationality.GetDataTable(qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        Doc_No = dr("Document_No").ToString()
                        AppliedAmt = CDec(dr("Applied_Amount").ToString())
                        BalAmt = CDec(connectSql.RunScalar("select Balance_Amt  from TSPL_VENDOR_INVOICE_HEAD where Document_No ='" + Doc_No + "' and Vendor_Code ='" + fndVendorNo.Value + "'")) + AppliedAmt
                        qry = "update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt ='" + BalAmt.ToString() + "' where Document_No ='" + Doc_No + "' and Vendor_Code ='" + fndVendorNo.Value + "'"
                        connectSql.RunSql(qry)
                    Next
                End If
                'Else
                '    qry = "select Document_No ,Applied_Amount from TSPL_RECEIPT_DETAIL where Receipt_No ='" + fndcheckReceiptNo.Value + "'"
                '    dt = clsDBFuncationality.GetDataTable(qry)
                '    For Each dr As DataRow In dt.Rows
                '        Doc_No = dr("Document_No").ToString()
                '        AppliedAmt = CDec(dr("Applied_Amount").ToString())
                '        BalAmt = CDec(connectSql.RunScalar("select Balance_Amt from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No ='" + Doc_No + "'")) + AppliedAmt
                '        qry = "update TSPL_SALE_INVOICE_HEAD set Balance_Amt ='" + BalAmt.ToString() + "' where Sale_Invoice_No ='" + Doc_No + "' "
                '        connectSql.RunSql(qry)
                '    Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub postdata()

        Try
            If myMessages.postConfirm() Then
                isFlag = True
                savedata(clicked)
                If clsBankReverse.PostData(fndreversecode.Value, isTDSReverse, isTaxReverse) Then
                    myMessages.post()
                    funposteddata()
                    funFill4()
                End If
            Else
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
        clicked = False
        'If ddl_SourceApplication.Text = "Account Payable" Then
        '    If myMessages.postConfirm() Then
        '        Dim ch As String
        '        'Dim k As String
        '        'If fnd_CheckPaymentNo.txtValue.Text <> "" Then
        '        '    ch = fnd_CheckPaymentNo.txtValue.Text
        '        '    k = ch.Substring(0, 2)
        '        'End If
        '        If fnd_CheckPaymentNo.txtValue.Text <> "" Then
        '            ch = connectSql.RunScalar("select Payment_Type from TSPL_PAYMENT_HEADER where Payment_No = '" + fnd_CheckPaymentNo.txtValue.Text + "'")
        '        End If
        '        Dim strdocument As String
        '        If ch = "PY" Then
        '            strdocument = connectSql.RunScalar("select Document_No  from TSPL_PAYMENT_DETAIL where Payment_No = '" + fnd_CheckPaymentNo.txtValue.Text + "'")
        '        End If
        '        Dim paymentno As String = "SELECT  TSPL_VENDOR_INVOICE_HEAD.Balance_Amt, TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No FROM  TSPL_PAYMENT_HEADER INNER JOIN TSPL_VENDOR_INVOICE_HEAD ON TSPL_PAYMENT_HEADER.Vendor_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code where TSPL_PAYMENT_HEADER.Payment_No = '" + fnd_CheckPaymentNo.txtValue.Text + "' and TSPL_PAYMENT_HEADER.BANK_CODE = '" + fnd_Bankcode.txtValue.Text + "' and TSPL_VENDOR_INVOICE_HEAD.Document_No = '" + strdocument + "'"
        '        ds = connectSql.RunSQLReturnDS(paymentno)
        '        If ds.Tables(0).Rows.Count > 0 Then
        '            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '                Dim chekexist As String = connectSql.RunScalar("select Applied_Amount  from TSPL_PAYMENT_DETAIL where  Payment_No = '" + fnd_CheckPaymentNo.txtValue.Text + "'  ")
        '                'and Vendor_Invoice_No= '" + ds.Tables(0).Rows(j)(1).ToString() + "'
        '                If Not String.IsNullOrEmpty(chekexist) Then
        '                    Dim value As Decimal = CDec(chekexist) + CDec(ds.Tables(0).Rows(j)(0))
        '                    Dim str2 As String = "update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt = '" + CStr(value) + "' where Vendor_Invoice_No = '" + ds.Tables(0).Rows(j)(1).ToString() + "'"
        '                    connectSql.RunSql(str2)
        '                End If
        '            Next
        '        End If
        '        Dim payment As String = fnd_CheckPaymentNo.txtValue.Text
        '        'Dim checkno As String = CStr(connectSql.RunScalar("select Cheque_No  from TSPL_PAYMENT_HEADER   where Payment_No = '" + fnd_CheckPaymentNo.txtValue.Text + "'"))
        '        Dim qry1 As String = "select m.Voucher_No,m.Source_Doc_No,d.Account_code,d.Amount from TSPL_JOURNAL_MASTER m right outer join " & _
        '                                       "TSPL_JOURNAL_DETAILS d on m.Voucher_No = d.Voucher_No where m.Source_Doc_No in " & _
        '                                       "(select Payment_No from TSPL_PAYMENT_HEADER where Payment_No = '" + fnd_CheckPaymentNo.txtValue.Text + "')"
        '        Dim arrglentry1 As New ArrayList()

        '        ds = connectSql.RunSQLReturnDS(qry1)
        '        If ds.Tables(0).Rows.Count > 0 Then
        '            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '                Dim value As Decimal = CDec(ds.Tables(0).Rows(i)(3))
        '                If value > 0 Then
        '                    Dim account As String = ds.Tables(0).Rows(i)(2).ToString()
        '                    Dim amount As Decimal = -1 * (ds.Tables(0).Rows(i)(3).ToString())
        '                    Dim accdr() As String = {account, amount}
        '                    arrglentry1.Add(accdr)

        '                Else
        '                    Dim account As String = ds.Tables(0).Rows(i)(2).ToString()
        '                    Dim amount As Decimal = -1 * (ds.Tables(0).Rows(i)(3).ToString())
        '                    Dim acccr() As String = {account, amount}
        '                    arrglentry1.Add(acccr)

        '                End If
        '            Next
        '        End If
        '        Dim strdesc1 As String = "REVERSE " + fnd_CheckPaymentNo.txtValue.Text
        '        transportSql.FunGrnlEntry(dtp_reversaldate.Value.Date, strdesc1, "RV-TA", "Bank Reverse", Me.fnd_reversecode.txtValue.Text, txt_BankaccountNo.Text, "O", "", "", userCode, companyCode, arrglentry1)
        '        Dim STR1 As String = "UPDATE TSPL_BANK_REVERSE SET POST = 'P' WHERE Reverse_Code = '" + fnd_reversecode.txtValue.Text + "'"
        '        connectSql.RunSql(STR1)
        '        myMessages.post()
        '        funposteddata()
        '    End If
        'Else
        '    If myMessages.postConfirm() Then
        '        Dim ch As String
        '        'Dim k As String
        '        'If fnd_CheckPaymentNo.txtValue.Text <> "" Then
        '        '    ch = fnd_CheckPaymentNo.txtValue.Text
        '        '    k = ch.Substring(0, 2)
        '        'End If
        '        If fnd_checkReceiptNo.txtValue.Text <> "" Then
        '            ch = connectSql.RunScalar("select Receipt_Type from TSPL_RECEIPT_HEADER where Receipt_No = '" + fnd_checkReceiptNo.txtValue.Text + "'")
        '        End If
        '        Dim strdocument1 As String
        '        If ch = "R" Then
        '            strdocument1 = connectSql.RunScalar("select Document_No  from TSPL_RECEIPT_DETAIL where Receipt_No = '" + fnd_checkReceiptNo.txtValue.Text + "'")
        '        End If
        '        Dim receiptno As String = "SELECT  TSPL_SALE_INVOICE_HEAD.Balance_Amt, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No FROM  TSPL_RECEIPT_HEADER INNER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_RECEIPT_HEADER.Cust_Code = TSPL_SALE_INVOICE_HEAD.Cust_Code where TSPL_RECEIPT_HEADER.Receipt_No = '" + fnd_checkReceiptNo.txtValue.Text + "' and TSPL_RECEIPT_HEADER.Bank_Code = '" + fnd_Bankcode.txtValue.Text + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = '" + strdocument1 + "'"
        '        ds = connectSql.RunSQLReturnDS(receiptno)
        '        If ds.Tables(0).Rows.Count > 0 Then
        '            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '                Dim chekexist As String = connectSql.RunScalar("select Applied_Amount  from TSPL_RECEIPT_DETAIL where  Receipt_No = '" + fnd_checkReceiptNo.txtValue.Text + "'")
        '                If Not String.IsNullOrEmpty(chekexist) Then
        '                    Dim value As Decimal = CDec(chekexist) + CDec(ds.Tables(0).Rows(j)(0))
        '                    Dim str2 As String = "update TSPL_SALE_INVOICE_HEAD set Balance_Amt = '" + CStr(value) + "' where Sale_Invoice_No = '" + ds.Tables(0).Rows(j)(1).ToString() + "'"
        '                    connectSql.RunSql(str2)
        '                End If
        '            Next
        '        End If
        '        Dim receipt As String = fnd_checkReceiptNo.txtValue.Text
        '        'Dim chequeno As String = CStr(connectSql.RunScalar("select Cheque_No  from TSPL_RECEIPT_HEADER   where Receipt_No = '" + fnd_checkReceiptNo.txtValue.Text + "'"))
        '        Dim qry As String = "select m.Voucher_No,m.Source_Doc_No,d.Account_code,d.Amount from TSPL_JOURNAL_MASTER m right outer join " & _
        '                                       "TSPL_JOURNAL_DETAILS d on m.Voucher_No = d.Voucher_No where m.Source_Doc_No in " & _
        '                                       "(select Receipt_No from TSPL_RECEIPT_HEADER where  Receipt_No= '" + fnd_checkReceiptNo.txtValue.Text + "')"
        '        Dim arrglentry As New ArrayList()
        '        ds = connectSql.RunSQLReturnDS(qry)
        '        If ds.Tables(0).Rows.Count > 0 Then
        '            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '                Dim value As Decimal = CDec(ds.Tables(0).Rows(i)(3))
        '                If value > 0 Then
        '                    Dim account As String = ds.Tables(0).Rows(i)(2).ToString()
        '                    Dim amount As Decimal = ds.Tables(0).Rows(i)(3).ToString()
        '                    Dim accdr() As String = {account, amount * -1}
        '                    arrglentry.Add(accdr)

        '                Else
        '                    Dim account As String = ds.Tables(0).Rows(i)(2).ToString()
        '                    Dim amount As Decimal = ds.Tables(0).Rows(i)(3).ToString()
        '                    Dim acccr() As String = {account, amount * -1}
        '                    arrglentry.Add(acccr)
        '                End If
        '            Next
        '        End If
        '        Dim strdesc As String = "REVERSE" + fnd_checkReceiptNo.txtValue.Text
        '        transportSql.FunGrnlEntry(dtp_reversaldate.Value.Date, strdesc, "RV-TA", "Bank Reverse", Me.fnd_reversecode.txtValue.Text, txt_BankaccountNo.Text, "O", "", "", userCode, companyCode, arrglentry)
        '        Dim STR As String = "UPDATE TSPL_BANK_REVERSE SET POST = 'P' WHERE Reverse_Code = '" + fnd_reversecode.txtValue.Text + "'"
        '        connectSql.RunSql(STR)
        '        funposteddata()
        '        myMessages.post()
        '    End If
        'End If

        ''Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        ''Dim Arr As ArrayList = New ArrayList()
        ''If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        ''    For Each dr As DataRow In dt.Rows
        ''        If ddl_SourceApplication.Text = "Account Payable" Then
        ''            If dr(3).ToString().Substring(0, 1) = "-" Then
        ''                Dim Acc1() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(txt_paymentAmount.Text)}
        ''                Arr.Add(Acc1)
        ''            Else
        ''                Dim Acc1() As String = {clsCommon.myCstr(dr("Account_code")), -1 * clsCommon.myCdbl(txt_paymentAmount.Text)}
        ''                Arr.Add(Acc1)
        ''            End If
        ''        Else
        ''            If dr(3).ToString().Substring(0, 1) = "-" Then
        ''                Dim Acc1() As String = {clsCommon.myCstr(dr("Account_code")), clsCommon.myCdbl(txt_receiptAmount.Text)}
        ''                Arr.Add(Acc1)
        ''            Else
        ''                Dim Acc1() As String = {clsCommon.myCstr(dr("Account_code")), -1 * clsCommon.myCdbl(txt_receiptAmount.Text)}
        ''                Arr.Add(Acc1)
        ''            End If
        ''        End If
        ''    Next
        ''End If

    End Sub
    Private Sub funposteddata()
        btn_save.Enabled = False
        btn_delete.Enabled = False
        btn_post.Enabled = False
    End Sub
    Private Sub fundata()
        btn_save.Enabled = True
        btn_delete.Enabled = True
        btn_post.Enabled = True
    End Sub

    Private Sub txt_Amount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_receiptAmount.KeyPress
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 48) _
                Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 57) Then
            e.Handled = True
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
                e.Handled = False
            End If
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 46) Then
                e.Handled = False
            End If
            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 39) Then
                e.Handled = False
            End If
        End If
    End Sub
    Private Function funautogenerateno() As String
        Dim strgeneratecode As String = ""
        Dim strgenerate As String = ""
        Dim total As String = ""
        Dim cutgenerate As String = ""
        Try
            strgenerate = strgeneratecode + "%"
            Dim str1 As String = Nothing
            Dim check As Integer = connectSql.RunScalar("select count(*) from TSPL_BANK_REVERSE")
            If check <> 0 Then
                str1 = connectSql.RunScalar("select MAX(Reverse_Code)  from TSPL_BANK_REVERSE  where Reverse_Code like 'RT%'")
            Else
            End If
            If String.IsNullOrEmpty(str1) Then
                total = "RT" + "000001"
            Else
                cutgenerate = str1.Substring(2, 6)
                Dim i As Integer = Integer.Parse(cutgenerate)
                i = i + 1
                Dim stri As String = CStr(i)
                If stri.Length = 1 Then
                    Dim t As String = Convert.ToString(i)
                    total = Convert.ToString("RT" + "00000" + t)
                ElseIf stri.Length = 2 Then
                    Dim t As String = Convert.ToString(i)
                    total = "RT" + "0000" + t
                ElseIf stri.Length = 3 Then
                    Dim t As String = Convert.ToString(i)
                    total = "RT" + "000" + t
                ElseIf stri.Length = 4 Then
                    Dim t As String = Convert.ToString(i)
                    total = "RT" + "00" + t
                ElseIf stri.Length = 5 Then
                    Dim t As String = Convert.ToString(i)
                    total = "RT" + "0" + t
                ElseIf stri.Length = 6 Then
                    Dim t As String = Convert.ToString(i)
                    total = "RT" + t
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
        Return total
    End Function
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "REVERSE-TRAN"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete & "," & enuUserRights.enuAuthorised
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
    '            btn_save.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btn_delete.Enabled = False
    '        End If
    '        If strTemp(3) = "0" Then 'Grant Authorize access
    '            btn_post.Enabled = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub frmReverseTransaction_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            savedata(clicked)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            postdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            resetdata()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverseTransaction.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    'Edited By Abhishek Kumar===BM00000008261
    Private Sub fndreversecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndreversecode._MYValidating
        Dim Qry As String = "select TSPL_BANK_REVERSE.Reverse_Code as [Code], CONVERT(date,reversal_date,105) as [Date], Case When TSPL_BANK_REVERSE.Source_Type='AR' Then 'Account Receivable' else 'Account Payable' END as [Type], TSPL_BANK_REVERSE.Bank_Code as [Bank Code], Case When TSPL_BANK_REVERSE.Post='P' Then 'Posted' Else 'Unposted' End as [Status],TSPL_BANK_REVERSE.Document_No as [Payment No],TSPL_BANK_REVERSE.Cheque_No as [Cheque No] " & _
        " ,case when TSPL_PAYMENT_HEADER.Payment_No is not null then 'Payment' else 'Receipt' end as [Payment/Receipt]" & _
        " ,coalesce(TSPL_PAYMENT_HEADER.Vendor_Code,TSPL_RECEIPT_HEADER.cust_code) as [Vendor/Customer Code],coalesce(TSPL_PAYMENT_HEADER.Vendor_Name,TSPL_RECEIPT_HEADER.customer_name) as [Vendor/Customer Name],coalesce(TSPL_PAYMENT_HEADER.Payment_Amount,TSPL_RECEIPT_HEADER.receipt_amount) as [Payment/Receipt Amount] " & _
        " ,case when TSPL_PAYMENT_HEADER.Payment_No is not null then (case when TSPL_PAYMENT_HEADER.Payment_Type='PY' then 'Payment' when TSPL_PAYMENT_HEADER.Payment_Type='AV' then 'Advance' when TSPL_PAYMENT_HEADER.Payment_Type='OA' then 'On Account' when TSPL_PAYMENT_HEADER.Payment_Type='MI' then 'Miscellaneous' when TSPL_PAYMENT_HEADER.Payment_Type='RC' then 'Receipt' when TSPL_PAYMENT_HEADER.Payment_Type='AD' then 'Apply Document' end) else " & _
        " (case when TSPL_RECEIPT_HEADER.Receipt_Type='R' then 'Receipt' when TSPL_RECEIPT_HEADER.Receipt_Type='P' then 'Advance' when TSPL_RECEIPT_HEADER.Receipt_Type='A' then 'Apply Document' when TSPL_RECEIPT_HEADER.Receipt_Type='M' then 'Misc Receipt' when TSPL_RECEIPT_HEADER.Receipt_Type='O' then 'On Account' when TSPL_RECEIPT_HEADER.Receipt_Type='U' then 'Unapplied' when TSPL_RECEIPT_HEADER.Receipt_Type='F' then 'Refund' when TSPL_RECEIPT_HEADER.Receipt_Type='S' then 'Misc Refund' end) end as [Payment/Receipt Type]" & _
        "  from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_REVERSE.Bank_Code LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_HEADER.Payment_No left outer join TSPL_RECEIPT_HEADER on TSPL_BANK_REVERSE.Document_No=TSPL_RECEIPT_HEADER.Receipt_No"
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(Nothing)
        Dim strWhrclas As String = "1=1"
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strWhrclas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(Bank_Code) > 0 Then
                strWhrclas += " AND TSPL_BANK_REVERSE.Bank_Code in ( " + Bank_Code + " )"
            End If
        End If
        Try
            fndreversecode.Value = clsCommon.ShowSelectForm("Code", Qry, "Code", strWhrclas, fndreversecode.Value, "Code", isButtonClicked, "TSPL_BANK_REVERSE.reversal_date")
            If clsCommon.myLen(fndreversecode.Value) > 0 Then
                funFill4()
            Else
                fndbankcode.Value = ""
                txt_Bankcode.Text = ""
                txt_BankaccountNo.Text = ""
                txt_reason.Text = ""
                dtp_reversaldate.Value = clsCommon.GETSERVERDATE()
                fndcustomerNo.Value = ""
                fndVendorNo.Value = ""
                txt_CustomerNo.Text = ""
                txt_VendorNo.Text = ""
                fndCheckPaymentNo.Value = ""
                fndcheckReceiptNo.Value = ""
                txt_receiptAmount.Text = ""
                txt_paymentAmount.Text = ""
                chkIsChequeBounce.Checked = False
                dtp_PayRecDate.Value = clsCommon.GETSERVERDATE()
                btn_save.Enabled = True
                btn_save.Text = "Save"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub fndreversecode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndreversecode._MYNavigator
        Dim qry As String = " select reverse_code from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_REVERSE.Bank_Code Where 2=2 "
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(Nothing)
        Dim strWhrclas As String = ""
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strWhrclas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(Bank_Code) > 0 Then
                strWhrclas += " AND TSPL_BANK_REVERSE.Bank_Code in ( " + Bank_Code + " )"
            End If
        End If
        qry += "" + strWhrclas + ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_BANK_REVERSE.reverse_code=(select MIN(reverse_code) from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_REVERSE.Bank_Code Where 1=1 " + strWhrclas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_BANK_REVERSE.reverse_code=(select MAX(reverse_code) from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_REVERSE.Bank_Code Where 1=1 " + strWhrclas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_BANK_REVERSE.reverse_code=(select Min(reverse_code) from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_REVERSE.Bank_Code where reverse_code > '" + fndreversecode.Value + "' " + strWhrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_BANK_REVERSE.reverse_code=(select Max(reverse_code) from TSPL_BANK_REVERSE LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Bank_Code=TSPL_BANK_REVERSE.Bank_Code where reverse_code < '" + fndreversecode.Value + "' " + strWhrclas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_BANK_REVERSE.reverse_code='" + fndreversecode.Value + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            fndreversecode.Value = clsCommon.myCstr(dt.Rows(0)("reverse_code"))
            funFill4()
        End If
    End Sub
    Private Sub fndbankcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndbankcode._MYValidating
        Dim qry As String = "select BANK_CODE as [Code],DESCRIPTION as [Description] ,ADD1 as [Address 1] ,ADD2 as [Address 2],ADD3 as [Address 3],ADD4 as [Address 4],CITY as [City] ,STATE as [State],POSTAL as [Postal],COUNTRY as [Country] from TSPL_BANK_MASTER"
        Dim Bank_Code As String = FrmMainTranScreen.bankPermission(Nothing)
        Dim strWhrclas As String = "INACTIVE='Active'"
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strWhrclas += " AND RIGHT(TSPL_BANK_MASTER.BANKACC,3) in (" + objCommonVar.strCurrUserLocationsSegment + ")"
            End If
        ElseIf clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PermissionSettingForTransactionWithBank, clsFixedParameterType.PermissionSettingForTransactionWithBank, Nothing)) = 1 Then
            If clsCommon.myLen(Bank_Code) > 0 Then
                strWhrclas += " AND TSPL_BANK_MASTER.Bank_Code in ( " + Bank_Code + " )"
            End If
        End If
        fndbankcode.Value = clsCommon.ShowSelectForm("BankCode", qry, "Code", strWhrclas, fndbankcode.Value, "Code", isButtonClicked)
        txt_Bankcode.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_BANK_MASTER where BANK_CODE='" & fndbankcode.Value & "'")
        Try
            Dim s As String
            s = clsDBFuncationality.getSingleValue("select Bank_Code from TSPL_BANK_MASTER where [Bank_Code] ='" + fndbankcode.Value + "'")

            'While dr.Read()
            '    s = dr(0).ToString()
            'End While
            If s <> "" Then
                funFill1()
            Else
                txt_Bankcode.Text = ""
                txt_BankaccountNo.Text = ""
                'btn_save.Enabled = True
                'btn_save.Text = "&Save"
            End If
            fndCheckPaymentNo.Value = ""
            fndcheckReceiptNo.Value = ""
            fndVendorNo.Value = ""
            fndcustomerNo.Value = ""
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub fndVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndVendorNo._MYValidating
        If fndbankcode.Value <> "" Then
            Dim qry As String = "select distinct v.Vendor_Code as [Code],v.Vendor_Name as [Vendor Name] from TSPL_VENDOR_MASTER v join  TSPL_PAYMENT_HEADER  p on v.Vendor_Code = p.Vendor_Code "
            fndVendorNo.Value = clsCommon.ShowSelectForm("VendorCode", qry, "Code", "p.Bank_Code = '" + fndbankcode.Value + "' AND V.Status='N' ", fndVendorNo.Value, "Code", isButtonClicked)
            txt_VendorNo.Text = clsDBFuncationality.getSingleValue("select distinct v.Vendor_Name as [Vendor Name] from TSPL_VENDOR_MASTER v join  TSPL_PAYMENT_HEADER  p on v.Vendor_Code = p.Vendor_Code where p.Bank_Code = '" + fndbankcode.Value + "' AND V.Status='N' ")
            Try
                Dim s As String
                s = clsDBFuncationality.getSingleValue("select Vendor_Code from TSPL_VENDOR_MASTER where [Vendor_Code] ='" + fndVendorNo.Value + "'  AND Status='N' ")

                'While dr.Read()
                '    s = dr(0).ToString()
                'End While
                If s <> "" Then
                    funFill2()
                    '' Anubhooti 23-Nov-2014 BM00000004671
                    fndCheckPaymentNo.Value = ""
                    txt_checkpaymentno.Text = ""
                Else
                    txt_VendorNo.Text = ""
                    '' Anubhooti 23-Nov-2014 BM00000004671
                    fndCheckPaymentNo.Value = ""
                    txt_checkpaymentno.Text = ""
                    'btn_save.Enabled = True
                    '  btn_save.Text = "&Save"
                End If
                ''Dim qry1 As String = "select Payment_No as [Payment Number],Cheque_No as [Cheque Number],Payment_Date as [Payment Date],Payment_Post_Date as [Payment Post Date],Bank_Code as [Bank Code],Payment_Type as [Payment Type] ,Payment_Amount as [Payment Amount] from TSPL_PAYMENT_HEADER where Vendor_Code = '" + fndVendorNo.Value + "' and Bank_code = '" + fndbankcode.Value + "' and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Post = 'p' or Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments')"
                ''Dim dr1 As SqlDataReader
                ''dr1 = connectSql.RunSqlReturnDR(qry1)
                ''If dr1.HasRows Then
                ''    While dr1.Read()
                ''        fndCheckPaymentNo.Value = dr1(0).ToString()
                ''        txt_checkpaymentno.Text = dr1(1).ToString()
                ''        txt_paymentAmount.Text = dr1(6).ToString()
                ''    End While
                ''End If
                'fndCheckPaymentNo.Value = clsDBFuncationality.getSingleValue("select Payment_No as [Payment Number],Cheque_No as [Cheque Number],Payment_Date as [Payment Date],Payment_Post_Date as [Payment Post Date],Bank_Code as [Bank Code],Payment_Type as [Payment Type] ,Payment_Amount as [Payment Amount] from TSPL_PAYMENT_HEADER where Vendor_Code = '" + fndVendorNo.Value + "' and Bank_code = '" + fndbankcode.Value + "' and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Post = 'p' or Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments')")

            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        Else
            common.clsCommon.MyMessageBoxShow(Me, "Select Bank Code !", Me.Text)
        End If
    End Sub

    Private Sub fndcustomerNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcustomerNo._MYValidating
        If fndbankcode.Value <> "" Then
            Dim qry As String = "select distinct c.Cust_Code as [Code],c.Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER c join  TSPL_RECEIPT_HEADER  r on c.Cust_Code = r.Cust_Code "
            fndcustomerNo.Value = clsCommon.ShowSelectForm("CustomerNo", qry, "Code", "r.Bank_Code = '" + fndbankcode.Value + "'", fndcustomerNo.Value, "Code", isButtonClicked)
            txt_CustomerNo.Text = clsDBFuncationality.getSingleValue("select distinct c.Customer_Name as [Customer Name] from TSPL_CUSTOMER_MASTER c join  TSPL_RECEIPT_HEADER  r on c.Cust_Code = r.Cust_Code where r.Bank_Code = '" + fndbankcode.Value + "'")
            Try
                Dim s As String
                s = clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_CUSTOMER_MASTER where [Cust_Code] ='" + fndcustomerNo.Value + "'")

                'While dr.Read()
                '    s = dr(0).ToString()
                'End While
                If s <> "" Then
                    funFill3()
                    '' Anubhooti 23-Nov-2014 BM00000004671
                    fndcheckReceiptNo.Value = ""
                    txt_checkreceiptno.Text = ""
                Else
                    txt_CustomerNo.Text = ""
                    '' Anubhooti 23-Nov-2014 BM00000004671
                    fndcheckReceiptNo.Value = ""
                    txt_checkreceiptno.Text = ""
                    'btn_save.Enabled = True
                    'btn_save.Text = "&Save"
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        Else
            common.clsCommon.MyMessageBoxShow(Me, "Select Bank Code !", Me.Text)
        End If
    End Sub

    Private Sub fndCheckPaymentNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCheckPaymentNo._MYValidating
        If fndbankcode.Value <> "" Then
            ''Dim qry As String = "select Payment_No as [Code],Cheque_No as [Cheque Number],Payment_Date as [Payment Date],Payment_Post_Date as [Payment Post Date],Bank_Code as [Bank Code],Payment_Type as [Payment Type] ,Payment_Amount as [Payment Amount] from TSPL_PAYMENT_HEADER "
            ''fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", "Bank_Code = '" + fndbankcode.Value + "' and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Post = 'p' or Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments')", fndCheckPaymentNo.Value, "Code", isButtonClicked)
            ''txt_checkpaymentno.Text = clsDBFuncationality.getSingleValue("select Cheque_No from TSPL_PAYMENT_HEADER where Bank_Code = '" + fndbankcode.Value + "' and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Post = 'p' or Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments')")
            ''Try
            ''    dr = connectSql.RunSqlReturnDR("select Payment_No from TSPL_PAYMENT_HEADER where [Payment_No] ='" + fndCheckPaymentNo.Value + "'")
            ''    Dim s As String
            ''    While dr.Read()
            ''        s = dr(0).ToString()
            ''    End While
            ''    If s <> "" Then
            ''        funFill5()
            ''    Else
            ''        txt_checkpaymentno.Text = ""
            ''        txt_paymentAmount.Text = ""
            ''        dtp_PayRecDate.Text = ""
            ''        btn_save.Enabled = True
            ''        'btn_save.Text = "&Save"
            ''    End If
            Dim PaymentType As String
            '====================update by preeti gupta Against ticket no [BM00000009021]("In Bank reverse screen those advance /OA doc should not come which has been applied once in applied document.")
            Dim qry As String = "select Payment_No as [Code],Cheque_No as [Cheque],Vendor_Code AS [Vendor Code],Payment_Date as [Payment Date],Payment_Post_Date as [Payment Post Date],Bank_Code as [Bank Code],Payment_Type as [Payment Type] ,sum(Payment_Amount) as [Payment Amount] from TSPL_PAYMENT_HEADER   "

            '' changes done by richa agarwal for not showing on account,advance whose are merged with apply document
            'Comment by balwinder on 22-03-2016 for showing OA,AV Document of UCBank type
            'If fndVendorNo.Value <> "" Then
            '    ' fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", " Posted='1' AND Bank_Code = '" + fndbankcode.Value + "' and IsChkReverse='N' and Vendor_Code='" + fndVendorNo.Value + "'  and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments') and Payment_No not in (select Payment_No  from TSPL_PAYMENT_HEADER where Payment_Type in ('AV','OA')) group by Payment_No,Cheque_No,Payment_Date,Payment_Post_Date,Bank_Code,Payment_Type,Vendor_Code ", fndCheckPaymentNo.Value, "Cheque", isButtonClicked)
            '    fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", " not exists (select 1 from TSPL_REVALUATION_DETAIL where TSPL_REVALUATION_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No) and Posted='1' AND Bank_Code = '" + fndbankcode.Value + "' and IsChkReverse='N' and Vendor_Code='" + fndVendorNo.Value + "'  and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments') AND Payment_No NOT IN (select DISTINCT Applied_Payment from TSPL_PAYMENT_HEADER where Payment_Type ='AD' AND payment_no NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments')) group by Payment_No,Cheque_No,Payment_Date,Payment_Post_Date,Bank_Code,Payment_Type,Vendor_Code ", fndCheckPaymentNo.Value, "Cheque", isButtonClicked)
            'Else
            '    fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", " not exists (select 1 from TSPL_REVALUATION_DETAIL where TSPL_REVALUATION_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No) and Posted='1' AND Bank_Code = '" + fndbankcode.Value + "' and IsChkReverse='N' and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments') AND Payment_No NOT IN (select DISTINCT Applied_Payment from TSPL_PAYMENT_HEADER where Payment_Type ='AD' AND payment_no NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments')) group by Payment_No,Cheque_No,Payment_Date,Payment_Post_Date,Bank_Code,Payment_Type,Vendor_Code ", fndCheckPaymentNo.Value, "Cheque", isButtonClicked)
            '    'fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", " Posted='1' AND Bank_Code = '" + fndbankcode.Value + "' and IsChkReverse='N' and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments') AND Payment_No NOT IN (select DISTINCT Applied_Payment from TSPL_PAYMENT_HEADER where Payment_Type ='AD' AND payment_no NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments')) group by Payment_No,Cheque_No,Payment_Date,Payment_Post_Date,Bank_Code,Payment_Type,Vendor_Code ", fndCheckPaymentNo.Value, "Cheque", isButtonClicked)
            'End If
            If fndVendorNo.Value <> "" Then
                ' fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", " Posted='1' AND Bank_Code = '" + fndbankcode.Value + "' and IsChkReverse='N' and Vendor_Code='" + fndVendorNo.Value + "'  and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments') and Payment_No not in (select Payment_No  from TSPL_PAYMENT_HEADER where Payment_Type in ('AV','OA')) group by Payment_No,Cheque_No,Payment_Date,Payment_Post_Date,Bank_Code,Payment_Type,Vendor_Code ", fndCheckPaymentNo.Value, "Cheque", isButtonClicked)
                fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", " not exists (select 1 from TSPL_REVALUATION_DETAIL where TSPL_REVALUATION_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No) and Posted='1' AND Bank_Code = '" + fndbankcode.Value + "' and IsChkReverse='N' and Vendor_Code='" + fndVendorNo.Value + "'  and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Reverse_Document = 'Receipts' and ISNULL(Post,'') ='P') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments' and ISNULL(Post,'') ='P') AND Payment_No NOT IN (select DISTINCT Applied_Payment from TSPL_PAYMENT_HEADER where Payment_Type ='AD' AND payment_no NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments' and ISNULL(Post,'') ='P') union all sELECT DISTINCT Document_No   FROM TSPL_PAYMENT_DETAIL WHERE Document_No IN ( sELECT PAYMENT_NO FROM TSPL_PAYMENT_HEADER WHERE Payment_Type ='RC') AND payment_no NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments' and ISNULL(Post,'') ='P') AND ISNULL(TSPL_PAYMENT_DETAIL.Document_No,'')<>'') group by Payment_No,Cheque_No,Payment_Date,Payment_Post_Date,Bank_Code,Payment_Type,Vendor_Code ", fndCheckPaymentNo.Value, "Cheque", isButtonClicked)
            Else
                fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", " not exists (select 1 from TSPL_REVALUATION_DETAIL where TSPL_REVALUATION_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No) and Posted='1' AND Bank_Code = '" + fndbankcode.Value + "' and IsChkReverse='N' and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Reverse_Document = 'Receipts'  and ISNULL(Post,'') ='P') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments'  and ISNULL(Post,'') ='P') AND Payment_No NOT IN (select DISTINCT Applied_Payment from TSPL_PAYMENT_HEADER where Payment_Type ='AD' AND payment_no NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments'  and ISNULL(Post,'') ='P') union all sELECT DISTINCT Document_No   FROM TSPL_PAYMENT_DETAIL WHERE Document_No IN ( sELECT PAYMENT_NO FROM TSPL_PAYMENT_HEADER WHERE Payment_Type ='RC') AND payment_no NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments'  and ISNULL(Post,'') ='P') AND ISNULL(TSPL_PAYMENT_DETAIL.Document_No,'')<>'') group by Payment_No,Cheque_No,Payment_Date,Payment_Post_Date,Bank_Code,Payment_Type,Vendor_Code ", fndCheckPaymentNo.Value, "Cheque", isButtonClicked)
                'fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", " Posted='1' AND Bank_Code = '" + fndbankcode.Value + "' and IsChkReverse='N' and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments') AND Payment_No NOT IN (select DISTINCT Applied_Payment from TSPL_PAYMENT_HEADER where Payment_Type ='AD' AND payment_no NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments')) group by Payment_No,Cheque_No,Payment_Date,Payment_Post_Date,Bank_Code,Payment_Type,Vendor_Code ", fndCheckPaymentNo.Value, "Cheque", isButtonClicked)
            End If

            'If fndVendorNo.Value <> "" Then
            '    fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", " Posted='1' AND Bank_Code = '" + fndbankcode.Value + "' and IsChkReverse='N' and Vendor_Code='" + fndVendorNo.Value + "'  and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments') group by Payment_No,Cheque_No,Payment_Date,Payment_Post_Date,Bank_Code,Payment_Type,Vendor_Code ", fndCheckPaymentNo.Value, "Cheque", isButtonClicked)
            'Else
            '    fndCheckPaymentNo.Value = clsCommon.ShowSelectForm("CheckPaymentNo", qry, "Code", " Posted='1' AND Bank_Code = '" + fndbankcode.Value + "' and IsChkReverse='N' and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments') group by Payment_No,Cheque_No,Payment_Date,Payment_Post_Date,Bank_Code,Payment_Type,Vendor_Code ", fndCheckPaymentNo.Value, "Cheque", isButtonClicked)
            'End If
            ''-----------------------------
            txt_checkpaymentno.Text = clsDBFuncationality.getSingleValue("select Cheque_No from TSPL_PAYMENT_HEADER where Bank_Code = '" + fndbankcode.Value + "' and Payment_No not in(select document_no from TSPL_BANK_REVERSE where  Post = 'p' or Reverse_Document = 'Receipts') and Payment_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Payments')")
            '' Anubhooti 24-Nov-2014 BM00000004671
            PaymentType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Payment_Type,'') As Payment_Type From TSPL_PAYMENT_HEADER Where Payment_No='" & clsCommon.myCstr(fndCheckPaymentNo.Value) & "'"))
            If clsCommon.CompairString(PaymentType, "MI") = CompairStringResult.Equal Then
                fndVendorNo.Enabled = False
            Else
                fndVendorNo.Enabled = True
            End If
            ''
            If fndCheckPaymentNo.Value <> "" Then
                funFill5()
                txt_checkpaymentno.Visible = True
                txt_checkreceiptno.Visible = False
            Else
                txt_checkpaymentno.Text = ""
                'txt_checkpaymentno.Visible = False
                txt_paymentAmount.Text = ""
                dtp_PayRecDate.Text = ""
                fndVendorNo.Value = ""
                txt_VendorNo.Text = ""
                'btn_save.Enabled = True
                'btn_save.Text = "&Save"
            End If


            'btn_post.Enabled = True
        Else
            common.clsCommon.MyMessageBoxShow(Me, "Select Bank Code !", Me.Text)
        End If
    End Sub

    Private Sub fndcheckReceiptNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcheckReceiptNo._MYValidating
        If fndbankcode.Value <> "" Then
            'Dim qry As String = "select Receipt_No as [Code],Cheque_No as [Cheque Number],Receipt_Date as [Receipt Date],Receipt_Post_Date as [Receipt Post Date],Cust_Code as [Customer Code],Customer_Name as [Customer Name],Bank_Code as [Bank Code],Receipt_Amount as [Receipt Amount] from TSPL_RECEIPT_HEADER  "
            'fndcheckReceiptNo.Value = clsCommon.ShowSelectForm("CheckRecieptNo", qry, "Code", "Bank_Code = '" + fndbankcode.Value + "' and   Cust_Code='" + fndcustomerNo.Value + "' and Receipt_No not in(select document_no from TSPL_BANK_REVERSE where  Post = 'p' or Reverse_Document = 'Payments') and Receipt_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Receipts')", fndcheckReceiptNo.Value, "Code", isButtonClicked)
            'txt_checkreceiptno.Text = clsDBFuncationality.getSingleValue("select Cheque_No from TSPL_RECEIPT_HEADER where Bank_Code = '" + fndbankcode.Value + "' and Receipt_No not in(select document_no from TSPL_BANK_REVERSE where  Post = 'p' or Reverse_Document = 'Payments') and Receipt_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Receipts')")
            Try
                Dim ReceiptType As String
                ''richa agarwal ERO/18/07/19-000956 hide auto apply documents of recipt type from bank reverse screen
                Dim qry As String = "select Receipt_no as [Code],Cheque_No as [Cheque No],Receipt_Date as [Receipt Date],Receipt_Post_Date as [Receipt Post Date],Cust_Code as [Customer Code],Customer_Name as [Customer Name],Bank_Code as [Bank Code],sum(Receipt_Amount + UnApplied_Balance) as [Receipt Amount] from TSPL_RECEIPT_HEADER    "
                '' changes done by richa agarwal for not showing on account,advance,refund whose are merged with apply document
                '              Dim whrcls As String = " AND Receipt_No NOT IN (select DISTINCT Applied_Receipt from TSPL_RECEIPT_HEADER where Receipt_Type ='A' AND Receipt_No  NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document ='Receipts')) " & _
                '" AND Receipt_No NOT IN (SELECT DISTINCT TSPL_RECEIPT_DETAIL .Document_No FROM TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL .Receipt_No WHERE ISNULL(TSPL_RECEIPT_DETAIL.Receipt_Type,'')  ='F' AND ISNULL(TSPL_RECEIPT_HEADER .Receipt_Type,'')  ='A' AND TSPL_RECEIPT_HEADER.Receipt_No  NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document ='Receipts')) and TSPL_RECEIPT_HEADER.IsAutoApplyDoc_Refund =0 "
                Dim whrcls As String = " AND Receipt_No NOT IN (select DISTINCT Applied_Receipt from TSPL_RECEIPT_HEADER where Receipt_Type ='A' AND Receipt_No  NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document ='Receipts') union select distinct UnappliedEntry.Receipt_No  from TSPL_RECEIPT_HEADER inner join(select UnApplied_No,Receipt_No from TSPL_RECEIPT_HEADER where Receipt_Type ='R' and isnull(UnApplied_No,'') <>'')UnappliedEntry on TSPL_RECEIPT_HEADER .Applied_Receipt =UnappliedEntry.UnApplied_No WHERE TSPL_RECEIPT_HEADER.IsChkReverse='N' ) " & _
" AND Receipt_No NOT IN (SELECT DISTINCT TSPL_RECEIPT_DETAIL .Document_No FROM TSPL_RECEIPT_HEADER LEFT OUTER JOIN TSPL_RECEIPT_DETAIL ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_RECEIPT_DETAIL .Receipt_No WHERE ISNULL(TSPL_RECEIPT_DETAIL.Receipt_Type,'')  ='F' AND ISNULL(TSPL_RECEIPT_HEADER .Receipt_Type,'')  ='A' AND TSPL_RECEIPT_HEADER.Receipt_No  NOT in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document ='Receipts')) and TSPL_RECEIPT_HEADER.IsAutoApplyDoc_Refund =0 "


                If fndcustomerNo.Value <> "" Then
                    fndcheckReceiptNo.Value = clsCommon.ShowSelectForm("CheckRecieptNo", qry, "Code", " not exists (select 1 from TSPL_REVALUATION_DETAIL where TSPL_REVALUATION_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No)  and Posted='Y' AND Bank_Code = '" + fndbankcode.Value + "' and TSPL_RECEIPT_HEADER.receipt_type <>'K' and   Cust_Code='" + fndcustomerNo.Value + "' and Receipt_Type <> 'U' and Receipt_No not in(select document_no from TSPL_BANK_REVERSE where  Post = 'p' or Reverse_Document = 'Payments') and Receipt_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Receipts') and Posted='y' and IsChkReverse='N' " & whrcls & " group by Receipt_No,Cheque_No,Cheque_Date,Receipt_Date,Receipt_Post_Date,Cust_Code,Customer_Name,Bank_Code", fndcheckReceiptNo.Value, "Code", isButtonClicked)
                Else
                    fndcheckReceiptNo.Value = clsCommon.ShowSelectForm("CheckRecieptNo", qry, "Code", " not exists (select 1 from TSPL_REVALUATION_DETAIL where TSPL_REVALUATION_DETAIL.Receipt_No=TSPL_RECEIPT_HEADER.Receipt_No)  and Posted='Y' AND Bank_Code = '" + fndbankcode.Value + "' and TSPL_RECEIPT_HEADER.receipt_type <>'K' and   Receipt_No not in(select document_no from TSPL_BANK_REVERSE where  Post = 'p' or Reverse_Document = 'Payments') and Receipt_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Receipts') and Posted='y' and IsChkReverse='N' " & whrcls & " group by Receipt_no,Cheque_No,Cheque_Date,Receipt_Date,Receipt_Post_Date,Cust_Code,Customer_Name,Bank_Code", fndcheckReceiptNo.Value, "Code", isButtonClicked)
                End If
                'txt_checkreceiptno.Text = clsDBFuncationality.getSingleValue("select Cheque_No from TSPL_RECEIPT_HEADER where Bank_Code = '" + fndbankcode.Value + "' and Receipt_No not in(select document_no from TSPL_BANK_REVERSE where  Post = 'p' or Reverse_Document = 'Payments') and Receipt_No not in (select Document_No from TSPL_BANK_REVERSE where Reverse_Document = 'Receipts')")

                '' Anubhooti 24-Nov-2014 BM00000004671
                ReceiptType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Receipt_Type ,'') As Receipt_Type From TSPL_RECEIPT_HEADER Where Receipt_No ='" & clsCommon.myCstr(fndcheckReceiptNo.Value) & "'"))
                If clsCommon.CompairString(ReceiptType, "M") = CompairStringResult.Equal Then
                    fndcustomerNo.Enabled = False
                Else
                    fndcustomerNo.Enabled = True
                End If
                ''



                If fndcheckReceiptNo.Value <> "" Then
                    funFill6()
                    txt_checkpaymentno.Visible = False
                    txt_checkreceiptno.Visible = True
                Else
                    txt_checkreceiptno.Text = ""
                    'txt_checkreceiptno.Visible = False
                    txt_receiptAmount.Text = ""
                    dtp_PayRecDate.Value = clsCommon.GETSERVERDATE()
                    'btn_save.Enabled = True
                    'btn_save.Text = "&Save"
                End If

                ''dr = connectSql.RunSqlReturnDR("select Receipt_No from TSPL_RECEIPT_HEADER where [Receipt_No] ='" + fndcheckReceiptNo.Value + "'")
                ''Dim s As String
                ''While dr.Read()
                ''    s = dr(0).ToString()
                ''End While
                ''If s <> "" Then
                ''    funFill6()
                ''Else
                ''    txt_checkreceiptno.Text = ""
                ''    txt_receiptAmount.Text = ""
                ''    dtp_PayRecDate.Value = clsCommon.GetPrintDate(Date.Today, "dd/MM/yyyy")
                ''    btn_save.Enabled = True
                ''    'btn_save.Text = "&Save"
                ''End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        Else
            common.clsCommon.MyMessageBoxShow(Me, "Select Bank Code !", Me.Text)
        End If
    End Sub
    'Edited By Abhishek

    Private Sub RadMenu1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenu1.Click

    End Sub

    Private Sub dtp_reversaldate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtp_reversaldate.ValueChanged

    End Sub

    Private Sub fndcustomerNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndcustomerNo.Load

    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click, RadButton10.Click
        printData(fndreversecode.Value, fndCheckPaymentNo.Value, fndcheckReceiptNo.Value, fndCheckPaymentNo.Value, fndcheckReceiptNo.Value, "")
    End Sub
    Public Sub printData(ByVal StrCode As String, ByVal APCheckNo As String, ByVal ARCheckNo As String, ByVal APDocNo As String, ByVal ARDocNo As String, ByVal IsPost As String)
        Dim strQuery As String = ""
        Dim count As Integer = 0
        count = clsDBFuncationality.getSingleValue("select count(*) from tspl_bank_reverse where Post ='n'and Reverse_Code ='" + StrCode + "'")
        Dim paymentNo As String = clsDBFuncationality.getSingleValue("select    Payment_No   from TSPL_PAYMENT_HEADER  where Cheque_No='" + APCheckNo + "' OR Payment_No='" + APDocNo + "'")
        Dim receiptNo As String = clsDBFuncationality.getSingleValue("select   Receipt_No   from TSPL_RECEIPT_HEADER  where Cheque_No='" + ARCheckNo + "' OR Receipt_No='" + ARDocNo + "'")
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "MPD") = CompairStringResult.Equal Then
            Dim strWhrClausePayment As String = ""
            If count = 0 Then
                strWhrClausePayment += " and m.Source_Doc_No ='" + StrCode + "'"
            Else
                strWhrClausePayment += " and m.Source_Doc_No in (select Payment_No from TSPL_PAYMENT_HEADER where Payment_No = '" + paymentNo + "') and R.Reverse_Code='" + StrCode + "'"
            End If

            Dim strWhrClauseReceipt As String = ""
           
            strWhrClauseReceipt += " where 2=2 and m.Source_Doc_No in (select Receipt_No from TSPL_RECEIPT_HEADER where  Receipt_No= '" + receiptNo + "') and R.Reverse_Code='" + StrCode + "'"

            If btn_save.Text = "Save" AndAlso clsCommon.myLen(IsPost) <= 0 Then
                Return
            Else

                If (ddl_SourceApplication.Text = "Account Payable") Then
                    'Dim paymentNo As String = clsDBFuncationality.getSingleValue("select    Payment_No   from TSPL_PAYMENT_HEADER  where Cheque_No='" + fndCheckPaymentNo.Value + "' OR Payment_No='" + fndCheckPaymentNo.Value + "'")

                    strQuery = " select R.Reverse_Code ,R.Reversal_Date,R.Reason,d.Account_code,d.Account_Desc,-1*d.Amount,case when (-1*d.Amount)>=0 then (-1* d.Amount) else 0 end as CrAmt,case when (-1*d.Amount)<0 then( d.Amount) else 0 end as DrAmt ,"
                    strQuery += " R .Created_By ,R .Modify_By ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,CASE WHEN R .Source_Type  = 'AP' THEN R .Vendor_Code  WHEN R .Source_Type  = 'AR' THEN R .Cust_Code  END AS Vendorcode,"
                    strQuery += " (SELECT  CASE WHEN R .Source_Type  = 'AP' THEN R .Vendor_Name   WHEN R .Source_Type  = 'AR' THEN R .Cust_Name   END  AS Expr2) AS VendorName  ,(SELECT  CASE WHEN R .Source_Type  = 'AP' THEN 'Vendor Name'   WHEN R .Source_Type  = 'AR' THEN 'Customer Name'  END  AS Expr2) AS headingName,(SELECT  CASE WHEN R .Source_Type  = 'AP' THEN 'Vendor Code'   WHEN R .Source_Type  = 'AR' THEN 'Customer Code'  END  AS Expr2) AS headingCode"
                    strQuery += " ,R.Reverse_Document,R.Cheque_No   from tspl_bank_reverse R "
                    strQuery += " left  outer join   TSPL_JOURNAL_MASTER  m on (m.CustVend_Code =R.Cust_Code or  m.CustVend_Code =R.Vendor_Code) and m.source_doc_no=r.Reverse_Code  "
                    strQuery += " left   outer join   TSPL_JOURNAL_DETAILS d on m.Voucher_No = d.Voucher_No "
                    strQuery += " left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =r.Comp_Code"
                    strQuery += "  where 2=2 " & strWhrClausePayment & ""


                Else
                    'Dim receiptNo As String = clsDBFuncationality.getSingleValue("select   Receipt_No   from TSPL_RECEIPT_HEADER  where Cheque_No='" + fndcheckReceiptNo.Value + "' OR Receipt_No='" + fndcheckReceiptNo.Value + "'")

                    strQuery = " select R.Reverse_Code ,R.Reversal_Date,R.Reason,d.Account_code,d.Account_Desc,-1*d.Amount,case when (-1*d.Amount)>=0 then (-1* d.Amount) else 0 end as DrAmt,case when (-1*d.Amount)<0 then( d.Amount) else 0 end as CrAmt ,"
                    strQuery += " R .Created_By ,R .Modify_By ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,CASE WHEN R .Source_Type  = 'AP' THEN R .Vendor_Code  WHEN R .Source_Type  = 'AR' THEN R .Cust_Code  END AS Vendorcode,"
                    strQuery += " (SELECT  CASE WHEN R .Source_Type  = 'AP' THEN R .Vendor_Name   WHEN R .Source_Type  = 'AR' THEN R .Cust_Name   END  AS Expr2) AS VendorName  ,(SELECT  CASE WHEN R .Source_Type  = 'AP' THEN 'Vendor Name'   WHEN R .Source_Type  = 'AR' THEN 'Customer Name'  END  AS Expr2) AS headingName,(SELECT  CASE WHEN R .Source_Type  = 'AP' THEN 'Vendor Code'   WHEN R .Source_Type  = 'AR' THEN 'Customer Code'  END  AS Expr2) AS headingCode"
                    strQuery += "  ,R.Reverse_Document,R.Cheque_No   from tspl_bank_reverse R "
                    strQuery += " left  outer join   TSPL_JOURNAL_MASTER  m on (m.CustVend_Code =R.Cust_Code or  m.CustVend_Code =R.Vendor_Code) and m.source_doc_no=r.Reverse_Code  "
                    strQuery += " left   outer join   TSPL_JOURNAL_DETAILS d on m.Voucher_No = d.Voucher_No "
                    strQuery += " left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =r.Comp_Code"
                    strQuery += "  " & strWhrClauseReceipt & ""

                End If

            End If
        Else
            If btn_save.Text = "Save" AndAlso clsCommon.myLen(IsPost) <= 0 Then
                Return
            Else

                If (ddl_SourceApplication.Text = "Account Payable") Then
                    'Dim paymentNo As String = clsDBFuncationality.getSingleValue("select    Payment_No   from TSPL_PAYMENT_HEADER  where Cheque_No='" + fndCheckPaymentNo.Value + "' OR Payment_No='" + fndCheckPaymentNo.Value + "'")

                    strQuery = " select R.Reverse_Code ,R.Reversal_Date,R.Reason,d.Account_code,d.Account_Desc,-1*d.Amount as Amount,case when (-1*d.Amount)>=0 then (-1* d.Amount) else 0 end as DrAmt,case when (-1*d.Amount)<0 then( d.Amount) else 0 end as CrAmt ,"
                    strQuery += " R .Created_By ,R .Modify_By ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,CASE WHEN R .Source_Type  = 'AP' THEN R .Vendor_Code  WHEN R .Source_Type  = 'AR' THEN R .Cust_Code  END AS Vendorcode,"
                    strQuery += " (SELECT  CASE WHEN R .Source_Type  = 'AP' THEN R .Vendor_Name   WHEN R .Source_Type  = 'AR' THEN R .Cust_Name   END  AS Expr2) AS VendorName  ,(SELECT  CASE WHEN R .Source_Type  = 'AP' THEN 'Vendor Name'   WHEN R .Source_Type  = 'AR' THEN 'Customer Name'  END  AS Expr2) AS headingName,(SELECT  CASE WHEN R .Source_Type  = 'AP' THEN 'Vendor Code'   WHEN R .Source_Type  = 'AR' THEN 'Customer Code'  END  AS Expr2) AS headingCode"
                    strQuery += " ,R.Reverse_Document,R.Cheque_No   from tspl_bank_reverse R "
                    strQuery += " left  outer join   TSPL_JOURNAL_MASTER  m on m.CustVend_Code =R.Cust_Code or  m.CustVend_Code =R.Vendor_Code  "
                    strQuery += " left   outer join   TSPL_JOURNAL_DETAILS d on m.Voucher_No = d.Voucher_No "
                    strQuery += " left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =r.Comp_Code"
                    strQuery += "  where m.Source_Doc_No in (select Payment_No from TSPL_PAYMENT_HEADER where Payment_No = '" + paymentNo + "') and R.Reverse_Code='" + StrCode + "'"


                Else
                    'Dim receiptNo As String = clsDBFuncationality.getSingleValue("select   Receipt_No   from TSPL_RECEIPT_HEADER  where Cheque_No='" + fndcheckReceiptNo.Value + "' OR Receipt_No='" + fndcheckReceiptNo.Value + "'")

                    strQuery = " select R.Reverse_Code ,R.Reversal_Date,R.Reason,d.Account_code,d.Account_Desc,-1*d.Amount,case when (-1*d.Amount)>=0 then (-1* d.Amount) else 0 end as DrAmt,case when (-1*d.Amount)<0 then( d.Amount) else 0 end as CrAmt ,"
                    strQuery += " R .Created_By ,R .Modify_By ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,CASE WHEN R .Source_Type  = 'AP' THEN R .Vendor_Code  WHEN R .Source_Type  = 'AR' THEN R .Cust_Code  END AS Vendorcode,"
                    strQuery += " (SELECT  CASE WHEN R .Source_Type  = 'AP' THEN R .Vendor_Name   WHEN R .Source_Type  = 'AR' THEN R .Cust_Name   END  AS Expr2) AS VendorName  ,(SELECT  CASE WHEN R .Source_Type  = 'AP' THEN 'Vendor Name'   WHEN R .Source_Type  = 'AR' THEN 'Customer Name'  END  AS Expr2) AS headingName,(SELECT  CASE WHEN R .Source_Type  = 'AP' THEN 'Vendor Code'   WHEN R .Source_Type  = 'AR' THEN 'Customer Code'  END  AS Expr2) AS headingCode"
                    strQuery += "  ,R.Reverse_Document,R.Cheque_No   from tspl_bank_reverse R "
                    strQuery += " left  outer join   TSPL_JOURNAL_MASTER  m on m.CustVend_Code =R.Cust_Code or  m.CustVend_Code =R.Vendor_Code  "
                    strQuery += " left   outer join   TSPL_JOURNAL_DETAILS d on m.Voucher_No = d.Voucher_No "
                    strQuery += " left outer join  TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code  =r.Comp_Code"
                    strQuery += "  where m.Source_Doc_No in (select Receipt_No from TSPL_RECEIPT_HEADER where  Receipt_No= '" + receiptNo + "') and R.Reverse_Code='" + StrCode + "'"

                End If

            End If
        End If


        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(strQuery)
        ''richa agarwal 21 Dec,2017 For TDS Reverse
        If (ddl_SourceApplication.Text = "Account Payable") Then
            Dim PaymentAmtWithTDS As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select TDS_Amount+Payment_Amount from TSPL_PAYMENT_HEADER where Payment_No ='" + paymentNo + "'"))
            If PaymentAmtWithTDS <> clsCommon.myCdbl(txt_paymentAmount.Text) Then
                Dim dtR As New DataTable()
                dtR = clsBankReverse.ReverseTDSCheck(StrCode, dt, Nothing)
                If dtR IsNot Nothing AndAlso dtR.Rows.Count > 0 Then
                    For i As Integer = 0 To dtR.Rows.Count - 1
                        If clsCommon.myCdbl(dtR.Rows(i)("Amount")) <> clsCommon.myCdbl(dtR.Rows(i)("CRAmt")) Then
                            dtR.Rows(i)("CRAmt") = clsCommon.myCdbl(dtR.Rows(i)("Amount")) * -1
                            Exit For
                        End If
                    Next
                    dt = New DataTable()
                    dt = dtR
                End If
            End If
        End If
        ''---------------------
        Dim frmCRV As New frmCrystalReportViewer()
        frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "rptBankReverseEntry", "Bank Reverse Entry Report")
        frmCRV = Nothing
    End Sub

  
    Private Sub btnReverseTransaction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseTransaction.Click
        If Not UsLock1.Status = ERPTransactionStatus.Approved Then
            If common.clsCommon.MyMessageBoxShow(Me, "Transaction status should be Approved to reverse and unpost", Me.Text) Then
                Exit Sub
            End If
        End If
        If clsCommon.myLen(fndreversecode.Value) > 0 Then
            If common.clsCommon.MyMessageBoxShow(Me, "Do you want to Reverse the current Transaction." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim qry As String = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='RV-TA' and Source_Doc_No='" + fndreversecode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strVoucherNo As String = clsCommon.myCstr(dt.Rows(0)("Voucher_No"))
                If clsCommon.myLen(strVoucherNo) > 0 Then
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strVoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                    qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If
            Dim qty As String = "select Reverse_Code, Document_No, Reversal_Date,Bank_Code, Back_Acc_No, Post, Source_Type ,amount from TSPL_BANK_REVERSE where Reverse_Code='" + fndreversecode.Value + "'"
            dt = clsDBFuncationality.GetDataTable(qty, trans)
            '===============================update by preeti gupta Aginst ticket no[BM00000009001]
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Source_Type")), "AP") = CompairStringResult.Equal Then
                    qry = "update TSPL_PAYMENT_HEADER set IsChkReverse='Y' where Payment_No='" + fndCheckPaymentNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Else
                    qry = "update TSPL_RECEIPT_HEADER set IsChkReverse='Y' where Receipt_No='" + fndcheckReceiptNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Dim ReceiptType As String = clsDBFuncationality.getSingleValue("select Receipt_Type  from  TSPL_RECEIPT_HEADER where Receipt_No in (select innRH.Applied_Receipt from TSPL_RECEIPT_HEADER as innRH where innRH.Receipt_No='" + fndcheckReceiptNo.Value + "')", trans)
                    If clsCommon.CompairString(ReceiptType, "O") = CompairStringResult.Equal Or clsCommon.CompairString(ReceiptType, "P") = CompairStringResult.Equal Or clsCommon.CompairString(ReceiptType, "U") = CompairStringResult.Equal Then
                        clsDBFuncationality.ExecuteNonQuery("update TSPL_RECEIPT_HEADER set Balance_Amt=Balance_Amt -('" + clsCommon.myCstr(txt_receiptAmount.Text) + "') where  Receipt_No in (select innRH.Applied_Receipt from TSPL_RECEIPT_HEADER as innRH where innRH.Receipt_No='" + fndcheckReceiptNo.Value + "')", trans)
                    End If

                    ''to update chkreverse='N' of unappiledentry
                    qry = "update TSPL_RECEIPT_HEADER set IsChkReverse='N'  where Receipt_No in (sELECT UnApplied_No from TSPL_RECEIPT_HEADER WHERE Receipt_No ='" + fndcheckReceiptNo.Value + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    ''----------

                    ''richa agarwal to update balance of sale invoice
                    Dim dt1 As DataTable = Nothing
                    Dim Doc_No As String = Nothing
                    Dim AppliedAmt As Decimal = 0
                    Dim BalAmt As Decimal = 0
                    qry = "select Document_No ,Applied_Amount,Receipt_Type  from TSPL_RECEIPT_DETAIL where Receipt_No ='" + fndcheckReceiptNo.Value + "'"
                    dt1 = clsDBFuncationality.GetDataTable(qry, trans)
                    For Each dr As DataRow In dt1.Rows
                        Doc_No = dr("Document_No").ToString()
                        AppliedAmt = CDec(dr("Applied_Amount").ToString())
                        BalAmt = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Balance_Amt  from TSPL_Customer_Invoice_Head where Document_No ='" + Doc_No + "' ")) - AppliedAmt
                        qry = "update TSPL_Customer_Invoice_Head set Balance_Amt ='" + BalAmt.ToString() + "' where Document_No ='" + Doc_No + "' "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        If clsCommon.CompairString(dr("Receipt_Type").ToString(), "F") = CompairStringResult.Equal Then
                            BalAmt = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Balance_Amt  from TSPL_RECEIPT_HEADER where Receipt_No ='" + Doc_No + "' ")) - AppliedAmt
                            qry = "update TSPL_RECEIPT_HEADER set Balance_Amt ='" + BalAmt.ToString() + "' where Receipt_No ='" + Doc_No + "' "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                    Next


                    '' to update balance amount against credit note which is applied with apply document on header
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Document_Type ,'') from TSPL_Customer_Invoice_Head WHERE DOCUMENT_NO IN (SELECT Applied_Receipt from TSPL_RECEIPT_HEADER WHERE Receipt_No ='" + fndcheckReceiptNo.Value + "' AND Receipt_Type ='A')", trans)), "C") = CompairStringResult.Equal Then
                        Doc_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Applied_Receipt FROM TSPL_RECEIPT_HEADER WHERE Receipt_No ='" + fndcheckReceiptNo.Value + "' AND Receipt_Type ='A'", trans))
                        AppliedAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select AMOUNT from TSPL_BANK_REVERSE WHERE Reverse_Code ='" + fndreversecode.Value + "'", trans))
                        BalAmt = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Balance_Amt  from TSPL_Customer_Invoice_Head where Document_No ='" + Doc_No + "' ")) - AppliedAmt
                        qry = "update TSPL_Customer_Invoice_Head set Balance_Amt ='" + BalAmt.ToString() + "' where Document_No ='" + Doc_No + "' "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                    ''-------------------------
                End If
            End If
            '==============================================END==================================================
            Xtra.UpdateSaleInvoiceBalanceAmt(trans)
            Xtra.UpdateAPInvoiceBalanceAmount("", trans)
            qry = "update TSPL_BANK_REVERSE set Post='N' where Reverse_Code='" + fndreversecode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndreversecode.Value, "TSPL_BANK_REVERSE", "Reverse_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndcheckReceiptNo.Value, "TSPL_RECEIPT_HEADER", "Receipt_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndCheckPaymentNo.Value, "TSPL_PAYMENT_HEADER", "Payment_No", trans)

            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Revese and unposted successfully", Me.Text)
            funFill4()

            'Dim qry As String = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='RV-TA' and Source_Doc_No='" + fndreversecode.Value + "'"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Dim strVoucherNo As String = clsCommon.myCstr(dt.Rows(0)("Voucher_No"))
            '    If clsCommon.myLen(strVoucherNo) > 0 Then
            '        qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'"
            '        clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '        qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
            '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '    End If
            '    Dim qty As String = "select Reverse_Code, Document_No, Reversal_Date,Bank_Code, Back_Acc_No, Post, Source_Type ,amount from TSPL_BANK_REVERSE where Reverse_Code='" + fndreversecode.Value + "'"
            '    dt = clsDBFuncationality.GetDataTable(qty, trans)
            '    '===============================update by preeti gupta Aginst ticket no[BM00000009001]
            '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Source_Type")), "AP") = CompairStringResult.Equal Then
            '            qry = "update TSPL_PAYMENT_HEADER set IsChkReverse='Y' where Payment_No='" + fndCheckPaymentNo.Value + "'"
            '            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '        Else
            '            qry = "update TSPL_RECEIPT_HEADER set IsChkReverse='Y' where Receipt_No='" + fndcheckReceiptNo.Value + "'"
            '            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '        End If
            '    End If
            '    '==============================================END==================================================
            '    Xtra.UpdateSaleInvoiceBalanceAmt(trans)
            '    Xtra.UpdateAPInvoiceBalanceAmount("", trans)
            '    qry = "update TSPL_BANK_REVERSE set Post='N' where Reverse_Code='" + fndreversecode.Value + "'"
            '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '    trans.Commit()
            '    clsCommon.MyMessageBoxShow("Revese and unposted successfully")
            '    funFill4()
            'End If
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndreversecode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndreversecode.Load

    End Sub
    ' Ticket No : TEC/08/05/19-000478 By Prabhakar 
    Private Sub btnOpenBankCashBook_Click(sender As Object, e As EventArgs) Handles btnOpenBankCashBook.Click
        clsOpenBankCashBook.ShowBankCashBookDatails(fndreversecode.Value)
    End Sub
End Class
