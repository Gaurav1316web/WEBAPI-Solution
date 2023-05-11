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

Public Class frmPrintCheckMultiple
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Public Shared BankCode As String
    Public Shared CheckCode As String
    Public Shared DocumentType As String
    Public Shared DocumentCode As String
    '' Anubhooti 22-July-2014
    Public Shared Account_Payee As Integer

    '' wreckage and flashing
    Const colDocSelect As String = "colDocSelect"
    Const colDocPrintingId As String = "colDocPrintingId"
    Const colBank_Code As String = "colBank_Code"
    Const colCheck_Code As String = "colCheck_Code"
    Const colCheck_Number As String = "colCheck_Number"
    Const colPrint_Date As String = "colPrint_Date"
    Const colPrinted_By As String = "colPrinted_By"
    Const colPrint_Status As String = "colPrint_Status"
    Const colDocument_Type As String = "colDocument_Type"
    Const colDocument_No As String = "colDocument_No"
    Const colDocument_Date As String = "colDocument_Date"
    Const col_Account_Payee As String = "col_Account_Payee"
    Const colComp_Code As String = "colComp_Code"
    Const colNot_For_High_Value As String = "colNot_For_High_Value"
    Const colPayTo As String = "colPayTo"
    Const colAmount As String = "colAmount"

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCheckPrinting)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnPrint.Visible = MyBase.isModifyFlag

    End Sub
    Sub LoadData(ByVal BankCode As String, ByVal CheckCode As String, ByVal DocumentType As String, Optional ByVal Document_Code As String = "", Optional ByVal trans As SqlTransaction = Nothing)
        Dim objList As List(Of clsPrintCheck) = clsPrintCheck.GetDataMultiple(BankCode, CheckCode, DocumentType, dtpFromDate.Value, dtpToDate.Value, Document_Code, trans)
        If Not objList Is Nothing AndAlso objList.Count > 0 Then
            gvDocs.Rows.Clear()
            For Each obj As clsPrintCheck In objList
                gvDocs.Rows.AddNew()
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colDocSelect).Value = True
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colDocument_No).Value = obj.DOCUMENT_NO
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colDocument_Date).Value = obj.DOCUMENT_Date
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colDocPrintingId).Value = obj.PRINTING_ID
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colBank_Code).Value = obj.BANK_CODE
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colCheck_Code).Value = obj.CHECK_CODE
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colCheck_Number).Value = obj.CHECK_NUMBER
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colPrint_Date).Value = obj.PRINT_DATE
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colPrinted_By).Value = obj.PINTED_BY
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colPrint_Status).Value = obj.PRINT_STATUS
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colDocument_Type).Value = obj.DOCUMENT_TYPE
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(col_Account_Payee).Value = obj.Account_Payee
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colNot_For_High_Value).Value = obj.Not_For_High_Value
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colPayTo).Value = obj.Pay_To
                gvDocs.Rows(gvDocs.Rows.Count - 1).Cells(colAmount).Value = obj.Amount

            Next
        Else
            gvDocs.Rows.Clear()
        End If

    End Sub
    Sub SaveData(ByVal showPrint As Boolean)
        Dim trans As SqlTransaction
        Try
            Dim obj As New clsPrintCheck
            Dim objList As New List(Of clsPrintCheck)
            If AllowToSave() Then
                For Each grow As GridViewRowInfo In gvDocs.Rows
                    If grow.Cells(colDocSelect).Value = False Then
                        Continue For
                    End If
                    obj = New clsPrintCheck()                    
                    obj.DOCUMENT_NO = grow.Cells(colDocument_No).Value
                    obj.DOCUMENT_Date = grow.Cells(colDocument_Date).Value
                    obj.PRINTING_ID = grow.Cells(colDocPrintingId).Value
                    obj.BANK_CODE = grow.Cells(colBank_Code).Value
                    obj.CHECK_CODE = fndCheckCode.Value
                    If clsCommon.myLen(grow.Cells(colCheck_Number).Value) <= 0 Or clsCommon.myCdbl(grow.Cells(colCheck_Number).Value) = 0 Then
                        obj.CHECK_NUMBER = clsPrintCheck.GetNextCheckNumber(obj.BANK_CODE, obj.CHECK_CODE)
                    Else
                        obj.CHECK_NUMBER = grow.Cells(colCheck_Number).Value
                    End If

                    obj.PRINT_DATE = clsCommon.GETSERVERDATE 'grow.Cells(colPrint_Date).Value
                    obj.PINTED_BY = objCommonVar.CurrentUserCode 'grow.Cells(colPrinted_By).Value
                    obj.PRINT_STATUS = IIf(clsCommon.myLen(grow.Cells(colPrint_Status).Value) > 0, grow.Cells(colPrint_Status).Value, "Pending")
                    obj.DOCUMENT_TYPE = ddlPaymentType.SelectedValue 'grow.Cells(colDocument_Type).Value
                    obj.Account_Payee = grow.Cells(col_Account_Payee).Value
                    obj.Not_For_High_Value = grow.Cells(colNot_For_High_Value).Value
                    obj.Pay_To = clsCommon.myCstr(grow.Cells(colPayTo).Value)
                    obj.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                    trans = clsDBFuncationality.GetTransactin
                    If (clsPrintCheck.SaveData(obj, trans)) Then
                        trans.Commit()
                        objList.Add(obj)                    
                    End If

                Next
                funPrint(objList)
                'If (clsPrintCheck.SaveData(objList)) Then

                'If showPrint Then
                'For Each grow As GridViewRowInfo In gvDocs.Rows
                '    If grow.Cells(colDocSelect).Value = True Then
                '        funPrint(grow.Cells(colDocument_Type).Value, grow.Cells(colDocument_No).Value, grow.Cells(col_Account_Payee).Value, grow.Cells(colNot_For_High_Value).Value)
                '    End If
                'Next

                'End If
                LoadData(obj.BANK_CODE, obj.CHECK_CODE, obj.DOCUMENT_TYPE, DocumentCode)
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function funPrint(ByVal arr As List(Of clsPrintCheck)) As Boolean
        Try
            'Qry = "select TSPL_PAYMENT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as bank_name,Cheque_No as check_no, " & _
            '                   " (case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV') then TSPL_VENDOR_MASTER.Vendor_Name  else TSPL_PAYMENT_HEADER.Entry_Desc  end) as Pay_To,(case when memorandum_amt>0 and Payment_Amount>0 then Payment_Amount when memorandum_amt>0 and  Payment_Amount=0 then null else Payment_Amount end ) as Check_Amount,'' as Amount_In_Words," & _
            '                   " TSPL_BANK_MASTER.BANKACCNUMBER as Account_No,TSPL_BANK_MASTER.ADD1 as Address1,TSPL_PAYMENT_HEADER.Comp_Code, " & _
            '                   " TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, convert(varchar,Cheque_Date,105) as check_Date ,(case when memorandum_amt>0 then '(not exceeding for Rs.' + cast(cast(memorandum_amt as decimal) as varchar) + '/-)' else '' end) as Memorandum_Lim_Desc,memorandum_amt,(case when memorandum_amt>0 then Entry_Desc else '' end) as Memorandum_Entry_Desc " & _
            '                   " , (case when TSPL_PAYMENT_HEADER.Account_Payee = 1 then 'A/C Payee' else '' end) as Account_Payee " & _
            '                   " from TSPL_PAYMENT_HEADER " & _
            '                   " left join TSPL_BANK_MASTER on TSPL_PAYMENT_HEADER.Bank_Code=TSPL_BANK_MASTER.Bank_Code " & _
            '                   " left join TSPL_COMPANY_MASTER on TSPL_PAYMENT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
            '                   " left join TSPL_VENDOR_MASTER on TSPL_PAYMENT_HEADER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
            '                   " where TSPL_PAYMENT_HEADER.Payment_No='" & doc_Code & "' "
            'ElseIf clsCommon.CompairString(Document_Type, "Receipt Entry") = CompairStringResult.Equal Then
            'Qry = " select TSPL_RECEIPT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as bank_name,Cheque_No as check_no,  (case when TSPL_RECEIPT_HEADER.Receipt_Type='PY' then TSPL_CUSTOMER_MASTER.Customer_Name else TSPL_RECEIPT_HEADER.Entry_Desc end) as Pay_To,Receipt_Amount as Check_Amount,'' as Amount_In_Words, TSPL_BANK_MASTER.BANKACCNUMBER as Account_No,TSPL_BANK_MASTER.ADD1 as Address1,TSPL_RECEIPT_HEADER.Comp_Code,  TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, convert(varchar,Cheque_Date,105) as check_Date from TSPL_RECEIPT_HEADER  left join TSPL_BANK_MASTER on TSPL_RECEIPT_HEADER.Bank_Code=TSPL_BANK_MASTER.Bank_Code  left join TSPL_COMPANY_MASTER on TSPL_RECEIPT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  left join TSPL_CUSTOMER_MASTER on TSPL_RECEIPT_HEADER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where TSPL_RECEIPT_HEADER.Receipt_No='" & doc_Code & "' "
            'End If

            '    For Each dr As DataRow In dsSetting.Rows
            '        Dim fld As ChequeSettingVar = DirectCast(CInt(clsCommon.myCdbl(dr("CodeValue"))), ChequeSettingVar)
            '        Dim txt As CrystalDecisions.CrystalReports.Engine.TextObject = Nothing

            '        Dim isVisible As Boolean = clsCommon.myCBool(dr("isVisible"))

            '        If fld = ChequeSettingVar.AccountPay Then
            '            isVisible = isAcPayee
            '        End If
            '        If fld = ChequeSettingVar.NotForHighValue Then
            '            isVisible = isNotForHighValue
            '        End If

            '        If isVisible Then
            '            Dim PayTo As String = clsCommon.myCstr(dsData.Rows(0)("Pay_To"))
            '            Dim charPerline As Integer = CInt(clsCommon.myCdbl(dr("CharPerLine")))
            '            Dim amt As Double = clsCommon.myCdbl(dsData.Rows(0)("Check_Amount"))
            '            Dim amtInWords As String = clsCommon.changeNumericToWords(amt) + " Only."

            '            Select Case fld
            '                Case ChequeSettingVar.NameLine1
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNameLine1"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    txt.Text = getLineText(PayTo, charPerline)
            '                    Exit Select
            '                Case ChequeSettingVar.NameLine2
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNameLine2"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    Dim printStr As String = getLineText(PayTo, charPerline)
            '                    txt.Text = PayTo.Substring(clsCommon.myLen(printStr))
            '                    Exit Select
            '                Case ChequeSettingVar.Amount
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAmt"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    txt.Text = clsCommon.myFormat(clsCommon.myCdbl(dsData.Rows(0)("Check_Amount")))
            '                    Exit Select
            '                Case ChequeSettingVar.[Date]
            '                    Dim strSpace As String = clsCommon.myCstr(CInt(clsCommon.myCdbl(dr("CharSpace"))))
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtDate" + strSpace), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    Dim DateStyle As String = clsCommon.myCstr(dr("DateStyle"))
            '                    txt.Text = clsCommon.GetPrintDate(clsCommon.myCDate(dsData.Rows(0)("check_Date")), DateStyle)
            '                    Exit Select
            '                Case ChequeSettingVar.Amtword1
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAmtinWrd1"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    txt.Text = getLineText(amtInWords, charPerline)
            '                    Exit Select
            '                Case ChequeSettingVar.Amtword2
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAmtinWrd2"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    Dim printamt As String = getLineText(amtInWords, charPerline)
            '                    txt.Text = clsCommon.myCstr(amtInWords).Substring(CInt(clsCommon.myLen(printamt)))
            '                    Exit Select
            '                Case ChequeSettingVar.ForCompany
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtForCmp"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    txt.Text = clsCommon.myCstr(dr("Name"))
            '                    Exit Select
            '                Case ChequeSettingVar.Sign
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAuthSign"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    txt.Text = clsCommon.myCstr(dr("Name"))
            '                    Exit Select
            '                Case ChequeSettingVar.NotOver
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNotOver"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    Dim caption As String = clsCommon.myCstr(dr("Name")).Trim()
            '                    If clsCommon.myLen(caption) > 0 Then
            '                        caption = (caption & Convert.ToString(": ")) + clsCommon.myFormat(amt + 1)
            '                    End If
            '                    txt.Text = caption
            '                    Exit Select
            '                Case ChequeSettingVar.AccountPay
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAcPay"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    txt.Text = clsCommon.myCstr(dr("Name"))
            '                    Exit Select
            '                Case ChequeSettingVar.Line1
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtLine1"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    'txt.Text = clsCommon.mycstr(dr["Name"]);
            '                    txt.Text = ""
            '                    Exit Select
            '                Case ChequeSettingVar.Line2
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtLine2"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    'txt.Text = clsCommon.mycstr(dr["Name"]);
            '                    txt.Text = ""
            '                    Exit Select
            '                Case ChequeSettingVar.AccountNo
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtAcNo"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    txt.Text = clsCommon.myCstr(dr("Name"))
            '                    Exit Select
            '                Case ChequeSettingVar.NotForHighValue
            '                    txt = DirectCast(objrptCheque.Section3.ReportObjects("txtNotForHighValue"), CrystalDecisions.CrystalReports.Engine.TextObject)
            '                    txt.Text = clsCommon.myCstr(dr("Name"))
            '                    Exit Select
            '            End Select

            '            If clsCommon.myLen(txt.Text) > 0 Then
            '                If clsCommon.myCdbl(dr("isxxxxxxBefore")) = 1 Then
            '                    txt.Text = "xxxx" + txt.Text
            '                End If
            '                If clsCommon.myCdbl(dr("isxxxxxxAfter")) = 1 Then
            '                    txt.Text = txt.Text + "xxxx"
            '                End If
            '            End If


            '            txt.ObjectFormat.EnableSuppress = False

            '            Dim fontName As String = clsCommon.myCstr(dr("FontName"))
            '            Dim fontSize As Single = CSng(clsCommon.myCdbl(dr("FontSize")))
            '            Dim fontStyle As System.Drawing.FontStyle = System.Drawing.FontStyle.Regular
            '            If CInt(clsCommon.myCdbl(dr("isBold"))) = 1 Then
            '                fontStyle = System.Drawing.FontStyle.Bold
            '            End If
            '            If clsCommon.myLen(fontName) = 0 Then
            '                fontName = "Arial"
            '            End If
            '            If fontSize = 0 Then
            '                fontSize = 9.75F
            '            End If

            '            Dim wwidth As Integer = 0

            '            txt.Left = CInt(clsCommon.myCdbl(dr("FrmLeft")) * 1440)
            '            txt.Top = CInt(clsCommon.myCdbl(dr("FrmTop")) * 1440)
            '            'txt.Height = (int)clsCommon.myCdbl(dr["FrmHeight"]) * 1440;

            '            wwidth = CInt(clsCommon.myCdbl(dr("FrmWidth")) * 1440)
            '            Dim txtwidth As Integer = CInt(fontSize * CInt(clsCommon.myLen(txt.Text))) * 11

            '            If wwidth < txtwidth AndAlso ((txt.Left + txtwidth) < (8 * 1440)) Then
            '                txt.Width = txtwidth
            '            Else
            '                txt.Width = wwidth
            '            End If

            '            txt.ApplyFont(New System.Drawing.Font(fontName, fontSize, fontStyle))

            '        End If
            '    Next
            'End If
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                Dim objrpt As New clsrptChequePrintMultiple()
                objrpt.PrintCheque(arr)
            End If
            Return False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Private Function funPrintMultiple(ByVal Document_Type As String, ByVal doc_Code As String, ByVal Acc_Payee As Boolean, ByVal NotForHighValue As Boolean) As Boolean
        Try
            Dim Qry As String = ""
            If clsCommon.CompairString(Document_Type, "Payment Entry") = CompairStringResult.Equal Then
                Qry = "select TSPL_PAYMENT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as bank_name,Cheque_No as check_no, " & _
                                "   coalesce((CASE WHEN TSPL_VENDOR_MASTER.Form_Type in ('VSP','PTM','TTM') then coalesce(Cheque_In_Favour_Of,'') else (TSPL_VENDOR_MASTER.Vendor_Name) end),Remit_To) as Pay_To,(case when memorandum_amt>0 and Payment_Amount>0 then Payment_Amount when memorandum_amt>0 and  Payment_Amount=0 then null else Payment_Amount end ) as Check_Amount,'' as Amount_In_Words," & _
                                " TSPL_BANK_MASTER.BANKACCNUMBER as Account_No,TSPL_BANK_MASTER.ADD1 as Address1,TSPL_PAYMENT_HEADER.Comp_Code, " & _
                                " TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, convert(varchar,Cheque_Date,105) as check_Date ,(case when memorandum_amt>0 then '(not exceeding for Rs.' + cast(cast(memorandum_amt as decimal) as varchar) + '/-)' else '' end) as Memorandum_Lim_Desc,memorandum_amt,(case when memorandum_amt>0 then Entry_Desc else '' end) as Memorandum_Entry_Desc " & _
                                " , (case when TSPL_PAYMENT_HEADER.Account_Payee = 1 then 'A/C Payee' else '' end) as Account_Payee " & _
                                " from TSPL_PAYMENT_HEADER " & _
                                " left join TSPL_BANK_MASTER on TSPL_PAYMENT_HEADER.Bank_Code=TSPL_BANK_MASTER.Bank_Code " & _
                                " left join TSPL_COMPANY_MASTER on TSPL_PAYMENT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
                                " left join TSPL_VENDOR_MASTER on TSPL_PAYMENT_HEADER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
                                " where TSPL_PAYMENT_HEADER.Payment_No='" & doc_Code & "' "
            ElseIf clsCommon.CompairString(Document_Type, "Receipt Entry") = CompairStringResult.Equal Then
                Qry = " select TSPL_RECEIPT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as bank_name,Cheque_No as check_no,  (case when TSPL_RECEIPT_HEADER.Receipt_Type='PY' then TSPL_CUSTOMER_MASTER.Customer_Name else TSPL_RECEIPT_HEADER.Entry_Desc end) as Pay_To,Receipt_Amount as Check_Amount,'' as Amount_In_Words, TSPL_BANK_MASTER.BANKACCNUMBER as Account_No,TSPL_BANK_MASTER.ADD1 as Address1,TSPL_RECEIPT_HEADER.Comp_Code,  TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, convert(varchar,Cheque_Date,105) as check_Date from TSPL_RECEIPT_HEADER  left join TSPL_BANK_MASTER on TSPL_RECEIPT_HEADER.Bank_Code=TSPL_BANK_MASTER.Bank_Code  left join TSPL_COMPANY_MASTER on TSPL_RECEIPT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  left join TSPL_CUSTOMER_MASTER on TSPL_RECEIPT_HEADER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where TSPL_RECEIPT_HEADER.Receipt_No='" & doc_Code & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim objrpt As New clsrptChequePrint()
                objrpt.PrintCheque(dt, Acc_Payee, NotForHighValue)
            End If



            Return False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function

    'Private Function funPrintOld() As Boolean
    '    Try
    '        Dim Qry As String = ""
    '        If clsCommon.CompairString(DocumentType, "Payment Entry") = CompairStringResult.Equal Then
    '            Qry = "select TSPL_PAYMENT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as bank_name,Cheque_No as check_no, " & _
    '                            " (case when TSPL_PAYMENT_HEADER.Payment_Type in ('PY','AV') then TSPL_VENDOR_MASTER.Vendor_Name + '  x x x x' else TSPL_PAYMENT_HEADER.Entry_Desc + '  x x x x' end) as Pay_To,(case when memorandum_amt>0 and Payment_Amount>0 then Payment_Amount when memorandum_amt>0 and  Payment_Amount=0 then null else Payment_Amount end ) as Check_Amount,'' as Amount_In_Words," & _
    '                            " TSPL_BANK_MASTER.BANKACCNUMBER as Account_No,TSPL_BANK_MASTER.ADD1 as Address1,TSPL_PAYMENT_HEADER.Comp_Code, " & _
    '                            " TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, convert(varchar,Cheque_Date,105) as check_Date,'' as datePart1,'' as datePart2,'' as datePart3,'' as datePart4," & _
    '                            " '' as datePart5,'' as datePart6,'' as datePart7,'' as datePart8 ,(case when memorandum_amt>0 then '(not exceeding for Rs.' + cast(cast(memorandum_amt as decimal) as varchar) + '/-)' else '' end) as Memorandum_Lim_Desc,memorandum_amt,(case when memorandum_amt>0 then Entry_Desc else '' end) as Memorandum_Entry_Desc " & _
    '                            " , (case when TSPL_PAYMENT_HEADER.Account_Payee = 1 then 'A/C Payee' else '' end) as Account_Payee " & _
    '                            " from TSPL_PAYMENT_HEADER " & _
    '                            " left join TSPL_BANK_MASTER on TSPL_PAYMENT_HEADER.Bank_Code=TSPL_BANK_MASTER.Bank_Code " & _
    '                            " left join TSPL_COMPANY_MASTER on TSPL_PAYMENT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " & _
    '                            " left join TSPL_VENDOR_MASTER on TSPL_PAYMENT_HEADER.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " & _
    '                            " where TSPL_PAYMENT_HEADER.Payment_No='" & Me.txtDocCode.Text & "' "
    '        ElseIf clsCommon.CompairString(DocumentType, "Receipt Entry") = CompairStringResult.Equal Then
    '            Qry = " select TSPL_RECEIPT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as bank_name,Cheque_No as check_no,  (case when TSPL_RECEIPT_HEADER.Receipt_Type='PY' then TSPL_CUSTOMER_MASTER.Customer_Name else TSPL_RECEIPT_HEADER.Entry_Desc end) as Pay_To,Receipt_Amount as Check_Amount,'' as Amount_In_Words, TSPL_BANK_MASTER.BANKACCNUMBER as Account_No,TSPL_BANK_MASTER.ADD1 as Address1,TSPL_RECEIPT_HEADER.Comp_Code,  TSPL_COMPANY_MASTER.Comp_Name as Comp_Name, convert(varchar,Cheque_Date,105) as check_Date from TSPL_RECEIPT_HEADER  left join TSPL_BANK_MASTER on TSPL_RECEIPT_HEADER.Bank_Code=TSPL_BANK_MASTER.Bank_Code  left join TSPL_COMPANY_MASTER on TSPL_RECEIPT_HEADER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code  left join TSPL_CUSTOMER_MASTER on TSPL_RECEIPT_HEADER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where TSPL_RECEIPT_HEADER.Receipt_No='" & Me.txtDocCode.Text & "' "
    '        End If
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '        For Each dr As DataRow In dt.Rows
    '            If IsDBNull(dt.Rows(dt.Rows.IndexOf(dr)).Item("Check_Amount")) Then
    '                dt.Rows(dt.Rows.IndexOf(dr)).Item("Amount_In_Words") = ""
    '            Else
    '                dt.Rows(dt.Rows.IndexOf(dr)).Item("Amount_In_Words") = clsCommon.changeNumericToWords(dt.Rows(dt.Rows.IndexOf(dr)).Item("Check_Amount")) + "only x x x x"
    '            End If

    '            Dim arr As String()
    '            arr = dt.Rows(dt.Rows.IndexOf(dr)).Item("check_Date").ToString.Split("-")
    '            If arr.Length = 3 Then
    '                dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart1") = arr(0).Substring(0, 1)
    '                dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart2") = arr(0).Substring(1, 1)
    '                dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart3") = arr(1).Substring(0, 1)
    '                dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart4") = arr(1).Substring(1, 1)
    '                dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart5") = arr(2).Substring(0, 1)
    '                dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart6") = arr(2).Substring(1, 1)
    '                dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart7") = arr(2).Substring(2, 1)
    '                dt.Rows(dt.Rows.IndexOf(dr)).Item("datePart8") = arr(2).Substring(3, 1)
    '            End If
    '        Next
    '        dt.AcceptChanges()
    '        If dt.Rows.Count > 0 Then
    '            '' changed by Panch Raj as per discussion with Amit Sir.
    '            'NewSalesReportViewer.funreport(dt, Me.txtBankCode.Text, "Print Check")
    '            NewSalesReportViewer.funreport(dt, "CheckPrintDemo", "Print Check")
    '        End If
    '        'If MsgBox("Have you done Check Printing ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '        '    Me.txtPrintStatus.Text = "Printed"
    '        '    Return True
    '        'Else
    '        '    Return False
    '        'End If
    '        Return False
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '        Return False
    '    End Try
    'End Function

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(clsCommon.myCstr(fndBankCode)) <= 0 Then
            fndBankCode.Focus()
            clsCommon.MyMessageBoxShow("Bank Code not found")
            Return False
        End If
        'If clsCommon.myLen(clsCommon.myCstr(fndCheckCode.Value)) <= 0 Then
        '    fndCheckCode.Focus()
        '    clsCommon.MyMessageBoxShow("Please select Check Code")
        '    Return False
        'End If
        'If clsCommon.myLen(clsCommon.myCdbl(txtNextCheckNumber.Text)) <= 0 Then
        '    txtNextCheckNumber.Focus()
        '    clsCommon.MyMessageBoxShow("Check No is blank")
        '    Return False
        'End If


        Return True
    End Function

#End Region
#Region "Events"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        SaveData(True)
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        If clsCommon.MyMessageBoxShow("Have you done Check Printing ?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            For Each grow As GridViewRowInfo In gvDocs.Rows
                If grow.Cells(colDocSelect).Value = True Then
                    grow.Cells(colPrint_Status).Value = "Printed"
                End If

            Next

            SaveData(False)
        End If
        Me.Close()
    End Sub
    Private Sub frmPrintCheck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P for Print")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        LoadDocType()
        LoadBlankGrid()
        dtpPrintDate.Value = clsCommon.GETSERVERDATE
        If clsCommon.myLen(DocumentType) > 0 AndAlso clsCommon.myLen(DocumentCode) > 0 AndAlso clsCommon.myLen(fndBankCode.Value) > 0 AndAlso clsCommon.myLen(fndCheckCode.Value) > 0 Then
            fndBankCode.Enabled = False
            dtpFromDate.Enabled = False
            dtpToDate.Enabled = False
            ddlPaymentType.Enabled = False
            LoadData(fndBankCode.Value, fndCheckCode.Value, DocumentType, DocumentCode)
        Else
            dtpToDate.Value = clsCommon.GETSERVERDATE
            dtpFromDate.Value = dtpToDate.Value
        End If
       
        'LoadData(BankCode, CheckCode, DocumentType, DocumentCode)
    End Sub

    Private Sub frmPrintCheck_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isModifyFlag Then
            SaveData(True)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
#End Region


    Private Sub LoadBlankGrid()

        gvDocs.Rows.Clear()
        gvDocs.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.Name = colDocSelect
        repoSelect.Width = 60
        repoSelect.HeaderText = "Select"
        gvDocs.MasterTemplate.Columns.Add(repoSelect)

        Dim repoDocumentNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocumentNo.FormatString = ""
        repoDocumentNo.Name = colDocument_No
        repoDocumentNo.Width = 100
        repoDocumentNo.HeaderText = "Document No"
        repoDocumentNo.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoDocumentNo)

        Dim repoDocumentDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDocumentDate.CustomFormat = "dd/MM/yyyy"
        repoDocumentDate.FormatString = "{0:d}"
        repoDocumentDate.Name = colDocument_Date
        repoDocumentDate.Width = 100
        repoDocumentDate.HeaderText = "Date"
        repoDocumentDate.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoDocumentDate)

        Dim repoDocumentType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDocumentType.FormatString = ""
        repoDocumentType.Name = colDocument_Type
        repoDocumentType.Width = 100
        repoDocumentType.HeaderText = "Document Type"
        repoDocumentType.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoDocumentType)


        Dim repoPrintingId As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrintingId.FormatString = ""
        repoPrintingId.Name = colDocPrintingId
        repoPrintingId.Width = 100
        repoPrintingId.HeaderText = "Printing Id"
        'repoPrintingId.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoPrintingId.TextImageRelation = TextImageRelation.TextBeforeImage
        repoPrintingId.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoPrintingId)

        Dim repoBankCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBankCode.FormatString = ""
        repoBankCode.Name = colBank_Code
        repoBankCode.Width = 100
        repoBankCode.HeaderText = "Bank Code"
        repoBankCode.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoBankCode)

        Dim repoCheckCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCheckCode.FormatString = ""
        repoCheckCode.Name = colCheck_Code
        repoCheckCode.Width = 100
        repoCheckCode.HeaderText = "Check Code"
        repoCheckCode.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoCheckCode)

        Dim repoCheckNumber As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCheckNumber.FormatString = ""
        repoCheckNumber.Name = colCheck_Number
        repoCheckNumber.Width = 100
        repoCheckNumber.HeaderText = "Check Number"
        repoCheckNumber.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoCheckNumber)

        Dim repoPrintStatus As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrintStatus.FormatString = ""
        repoPrintStatus.Name = colPrint_Status
        repoPrintStatus.Width = 100
        repoPrintStatus.HeaderText = "Print Status"
        repoPrintStatus.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoPrintStatus)

        Dim repoPrintDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrintDate.FormatString = ""
        repoPrintDate.Name = colPrint_Date
        repoPrintDate.Width = 100
        repoPrintDate.HeaderText = "Print Date"
        repoPrintDate.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoPrintDate)

        Dim repoPrintedBy As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrintedBy.FormatString = ""
        repoPrintedBy.Name = colPrinted_By
        repoPrintedBy.Width = 100
        repoPrintedBy.HeaderText = "Printed By"
        repoPrintedBy.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoPrintedBy)

        Dim repoAccountPayee As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAccountPayee.FormatString = ""
        repoAccountPayee.Name = col_Account_Payee
        repoAccountPayee.Width = 100
        repoAccountPayee.HeaderText = "Account Payee"
        repoAccountPayee.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoAccountPayee)

        Dim repoNotForHighValue As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNotForHighValue.FormatString = ""
        repoNotForHighValue.Name = colNot_For_High_Value
        repoNotForHighValue.Width = 100
        repoNotForHighValue.HeaderText = "Not for High Value"
        repoNotForHighValue.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoNotForHighValue)

        Dim repoPayto As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPayto.FormatString = ""
        repoPayto.Name = colPayTo
        repoPayto.Width = 100
        repoPayto.HeaderText = "Pay to"
        repoPayto.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoPayto)


        repoPayto = New GridViewTextBoxColumn()
        repoPayto.FormatString = ""
        repoPayto.Name = colAmount
        repoPayto.Width = 100
        repoPayto.HeaderText = "Amount"
        repoPayto.ReadOnly = True
        gvDocs.MasterTemplate.Columns.Add(repoPayto)
        '-------------------------------------------------

        gvDocs.AllowDeleteRow = True
        gvDocs.AllowAddNewRow = False
        gvDocs.ShowGroupPanel = False
        gvDocs.AllowColumnReorder = False
        gvDocs.AllowRowReorder = False
        gvDocs.EnableSorting = False
        gvDocs.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvDocs.MasterTemplate.ShowRowHeaderColumn = False
        gvDocs.Rows.AddNew()
    End Sub
    Sub LoadDocType()
        ddlPaymentType.DataSource = GetDocType()
        ddlPaymentType.DisplayMember = "Name"
        ddlPaymentType.ValueMember = "Code"
        If clsCommon.myLen(DocumentType) > 0 Then
            ddlPaymentType.SelectedValue = DocumentType
        End If
    End Sub
    Public Shared Function GetDocType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Payment Entry"
        dr("Name") = "Payment Entry"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Receipt Entry"
        dr("Name") = "Receipt Entry"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub fndBankCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndBankCode._MYValidating
        fndBankCode.Value = clsBankMaster.getFinder("", fndBankCode.Value, isButtonClicked)
    End Sub
    Private Sub fndCheckCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCheckCode._MYValidating
        Dim strWhrclas As String = ""
        Dim qry As String = "select CHECK_CODE as Code,BANK_CODE as [Bank Code],DESCRIPTION as Description,NEXT_CHECK_NUMBER as [Start Check Number],LAST_CHECK_NUMBER as [Last Check Number] from TSPL_BANK_CHECK_PRINTING"
        strWhrclas = " BANK_CODE='" & fndBankCode.Value & "'"
        fndCheckCode.Value = clsCommon.ShowSelectForm("Check", qry, "Code", strWhrclas, fndCheckCode.Value, "Code", isButtonClicked)
        lblCheckDescription.Text = connectSql.RunScalar("select description from TSPL_BANK_CHECK_PRINTING where CHECK_CODE = '" + fndCheckCode.Value + "'")
        'Me.txtNextCheckNumber.Text = clsPrintCheck.GetNextCheckNumber(txtBankCode.Text, fndCheckCode.Value)
    End Sub

    Public Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        If AllowToSave() Then
            LoadData(fndBankCode.Value, fndCheckCode.Value, ddlPaymentType.SelectedValue, DocumentCode)
        End If
    End Sub
    
End Class
