'' richa agarwal against ticket no. BM00000006889
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports common
Imports System.IO
Public Class Frmreceiptvoucher2
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub fndreceiptfrom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'fndreceiptfrom.ConnectionString = connectSql.SqlCon()
        'fndreceiptfrom.Query = "select Receipt_No as [Receipt No], Entry_Desc as [Description] from TSPL_RECEIPT_HEADER  "
        'fndreceiptfrom.ValueToSelect = "Receipt No"
        'fndreceiptfrom.Caption = "Receipt Detail"
        'fndreceiptfrom.ValueToSelect1 = "Description"
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.receiptreport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub fndreceiptto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndreceiptto.ConnectionString = connectSql.SqlCon()
        'fndreceiptto.Query = "select Receipt_No as [Receipt No], Entry_Desc as [Description] from TSPL_RECEIPT_HEADER  "
        'fndreceiptto.ValueToSelect = "Receipt No"
        'fndreceiptto.Caption = "Receipt Detail"
        'fndreceiptto.ValueToSelect1 = "Description"
    End Sub

    Private Sub fndcustomerfrom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndcustomerfrom.ConnectionString = connectSql.SqlCon()
        'fndcustomerfrom.Query = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master"
        'fndcustomerfrom.ValueToSelect = "Customer Code"
        'fndcustomerfrom.Caption = "Customer Details"
        'fndcustomerfrom.ValueToSelect1 = "Customer Name"
    End Sub

    Private Sub fndcustomerto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndcustomerto.ConnectionString = connectSql.SqlCon()
        'fndcustomerto.Query = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master"
        'fndcustomerto.ValueToSelect = "Customer Code"
        'fndcustomerto.Caption = "Customer Details"
        'fndcustomerto.ValueToSelect1 = "Customer Name"

    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Sub Print()
        If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select atleast one Location")
            Return
        End If
        Dim fromdate As String = clsCommon.GetPrintDate(dtpfrom.Value, "dd/MM/yyyy")
        Dim todate As String = clsCommon.GetPrintDate(dtpto.Value, "dd/MM/yyyy")
        Dim arrreceipt As ArrayList = Nothing
        If chkReceiptSelect.IsChecked = True Then
            arrreceipt = cbgReceipt.CheckedValue
        ElseIf chkReceiptSelect.IsChecked = False Then
            arrreceipt = cbgReceipt.AllValue
        End If
        Dim arrlocation As ArrayList = Nothing
        If chkLocSelect.IsChecked = True Then
            arrlocation = cbgLocation.CheckedValue
        ElseIf chkLocSelect.IsChecked = False Then
            arrlocation = cbgLocation.AllValue
        End If
        Dim arrcustomer As ArrayList = Nothing
        If chkCustomerSelect.IsChecked = True Then
            arrcustomer = cbgCustomer.CheckedValue
        End If


        PrintData(fromdate, todate, chkReceiptSelect.IsChecked, arrreceipt, chkCustomerSelect.IsChecked, arrcustomer, chkLocSelect.IsChecked, arrlocation)
    End Sub
    
    'Shared Sub PrintDate(ByVal fromdate As String, ByVal todate As String, ByVal chkReceiptSelect As Boolean, ByVal arrreceipt As ArrayList, ByVal chkCustomerSelect As Boolean, ByVal arrcustomer As ArrayList)
    '    Dim dttemp As New DataTable()
    '    Try
    '        If chkReceiptSelect AndAlso arrreceipt.Count <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("Please Select at least one Receipt.")
    '            Return
    '        ElseIf (chkReceiptSelect) Then
    '            arrreceipt = arrreceipt
    '        ElseIf (chkReceiptSelect) = False Then
    '            arrreceipt = arrreceipt
    '        End If
    '        If chkCustomerSelect AndAlso arrcustomer.Count <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("Please Select at least one Customer.")
    '            Return
    '        ElseIf (chkCustomerSelect) Then
    '            arrcustomer = arrcustomer
    '        End If
    '        Dim qry As String = "select Receipt_No  from TSPL_RECEIPT_HEADER where 2=2"
    '        If arrreceipt IsNot Nothing AndAlso arrreceipt.Count > 0 Then
    '            qry += " and Receipt_No in (" + clsCommon.GetMulcallString(arrreceipt) + ")"
    '        End If
    '        If arrcustomer IsNot Nothing AndAlso arrcustomer.Count > 0 Then
    '            qry += "and Cust_Code in (" + clsCommon.GetMulcallString(arrcustomer) + ")"
    '        End If
    '        Dim dtdocument As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        dttemp.Columns.Add("Cheque_No", GetType(System.String))
    '        dttemp.Columns.Add("Cheque_Date", GetType(System.DateTime))
    '        dttemp.Columns.Add("Entry_Desc", GetType(System.String))
    '        dttemp.Columns.Add("Receipt_No", GetType(System.String))
    '        dttemp.Columns.Add("Receipt_Post_Date")
    '        dttemp.Columns.Add("Created_By")
    '        dttemp.Columns.Add("Modify_By")
    '        dttemp.Columns.Add("Detail_Line_No")
    '        dttemp.Columns.Add("Account_code", GetType(System.String))
    '        dttemp.Columns.Add("Account_Desc", GetType(System.String))
    '        dttemp.Columns.Add("Document_No", GetType(System.String))
    '        dttemp.Columns.Add("Remarks", GetType(System.String))
    '        dttemp.Columns.Add("Comment", GetType(System.String))
    '        dttemp.Columns.Add("Amount", GetType(System.Decimal))
    '        dttemp.Columns.Add("Description", GetType(System.String))
    '        dttemp.Columns.Add("Cust_Code", GetType(System.String))
    '        dttemp.Columns.Add("Customer_Name", GetType(System.String))
    '        dttemp.Columns.Add("BankName", GetType(System.String))
    '        dttemp.Columns.Add("CompanyName", GetType(System.String))
    '        Dim logo1 As DataColumn = New DataColumn()
    '        logo1.DataType = System.Type.GetType("System.Byte[]")
    '        Dim logo2 As DataColumn = New DataColumn()
    '        logo2.DataType = System.Type.GetType("System.Byte[]")
    '        dttemp.Columns.Add("logo1")
    '        dttemp.Columns.Add("logo2")
    '        'dttemp.Columns.Add("logo1", GetType(Image))
    '        'dttemp.Columns.Add("logo2", GetType(Image))
    '        dttemp.Columns.Add("SegName", GetType(System.String))
    '        dttemp.Columns.Add("vouchertype", GetType(System.String))
    '        Dim dt As DataTable
    '        Dim recvalue As String
    '        For Each drreceipt As DataRow In dtdocument.Rows
    '            recvalue = drreceipt("Receipt_No").ToString()
    '            Dim strquery As String
    '            Dim strQuerydocno As String = String.Empty
    '            Dim strqueryremarks As String = String.Empty
    '            Dim strquerycomment As String = String.Empty
    '            Dim strdoccomment As String
    '            strquery = "select  TSPL_RECEIPT_DETAIL.Document_No + CASE WHEN len(TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date) < 0 THEN '   ' ELSE ',' + CONVERT(varchar(10), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) END AS Document_No,   TSPL_RECEIPT_DETAIL.Remarks, TSPL_RECEIPT_DETAIL.Comment from TSPL_RECEIPT_DETAIL   LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= TSPL_RECEIPT_DETAIL.Document_No  where TSPL_RECEIPT_DETAIL.Receipt_No ='" + recvalue + " '"
    '            dt = clsDBFuncationality.GetDataTable(strquery)
    '            Dim strdocno As String = String.Empty
    '            Dim strremark As String
    '            Dim strcomment As String
    '            Dim arrDocNo As New List(Of String)()
    '            Dim arrRmks As New List(Of String)()
    '            Dim arrComment As New List(Of String)()
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                For Each dr As DataRow In dt.Rows
    '                    Dim strTDocNo As String = String.Empty
    '                    Dim strTRmks As String = String.Empty
    '                    Dim strTComment As String = String.Empty
    '                    strTDocNo = clsCommon.myCstr(dr("Document_No"))
    '                    strTRmks = clsCommon.myCstr(dr("Remarks"))
    '                    strTComment = clsCommon.myCstr(dr("Comment"))

    '                    If clsCommon.myLen(strTDocNo) > 0 AndAlso Not arrDocNo.Contains(strTDocNo) Then
    '                        If clsCommon.myLen(strdocno) > 0 Then
    '                            strdocno += ", "
    '                        End If
    '                        strdocno += strTDocNo
    '                    Else
    '                        arrDocNo.Add(strTDocNo)
    '                    End If
    '                    If clsCommon.myLen(strTComment) > 0 AndAlso Not arrComment.Contains(strTComment) Then
    '                        If clsCommon.myLen(strcomment) > 0 Then
    '                            strcomment += ", "
    '                        End If
    '                        strcomment += strTComment
    '                    Else
    '                        arrComment.Add(strTComment)
    '                    End If
    '                Next
    '            End If
    '            strquery = "SELECT     Cheque_No, Cheque_Date, Entry_Desc, Receipt_No, Receipt_Post_Date, Created_By, Modify_By, Detail_Line_No, Account_code, Account_Desc, " & _
    '                                   " '" + strdocno + "' as Document_No,'" + strremark + "'as Remarks,'" + strcomment + "'as Comment, Amount, Description, Cust_Code, Customer_Name, BankName, " & _
    '                                   " (SELECT     Comp_Name FROM TSPL_COMPANY_MASTER WHERE      (Comp_Code = 'demo')) AS CompanyName, " & _
    '                                   " (SELECT     Logo_Img FROM          TSPL_COMPANY_MASTER WHERE      (Comp_Code = 'demo')) AS logo1, " & _
    '                                   " (SELECT     Logo_Img2 FROM          TSPL_COMPANY_MASTER WHERE      (Comp_Code = 'demo')) AS logo2, " & _
    '                                   " (SELECT     Description FROM TSPL_GL_SEGMENT_CODE WHERE      (Seg_No = '7') AND (Segment_code = SUBSTRING(xxx.Account_code, LEN(xxx.Account_code) - 2, 3))) AS SegName," & _
    '            "case when  xxx.Payment_Code ='cheque' then 'Bank Receipt Voucher' when xxx.Payment_Code ='check' then 'Bank Receipt Voucher'when xxx.Payment_Code ='SETTLEMENT' then'Cash Receipt Voucher'when xxx.Payment_Code ='SETTLEB'then 'Bank Receipt Voucher' when xxx.Payment_Code ='CASH' then 'Cash Receipt Voucher'  " & _
    '" when xxx.Payment_Code='OTHER' then '" + recvalue + "'+ ' Voucher'    else (select distinct TSPL_PAYMENT_CODE.Payment_Code  from TSPL_PAYMENT_CODE left outer join TSPL_RECEIPT_HEADER on TSPL_PAYMENT_CODE.Payment_Code =TSPL_RECEIPT_HEADER.Payment_Code  where Receipt_No='" + recvalue + "') +'  Voucher' " & _
    '" end   as vouchertype    FROM  (SELECT DISTINCT  TSPL_RECEIPT_HEADER.Cheque_No, TSPL_RECEIPT_HEADER.Cheque_Date, TSPL_RECEIPT_HEADER.Entry_Desc,  " & _
    '                                   " TSPL_RECEIPT_HEADER.Receipt_No, TSPL_RECEIPT_HEADER.Receipt_Post_Date, TSPL_RECEIPT_HEADER.Created_By,  " & _
    '                                   " TSPL_RECEIPT_HEADER.Modify_By, TSPL_JOURNAL_DETAILS.Detail_Line_No, TSPL_JOURNAL_DETAILS.Account_code,  " & _
    '                                    " TSPL_JOURNAL_DETAILS.Account_Desc, TSPL_JOURNAL_DETAILS.Amount,  " & _
    '                                   " TSPL_JOURNAL_DETAILS.Description, TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Customer_Name,  " & _
    '                                   " TSPL_BANK_MASTER.DESCRIPTION AS BankName ,Payment_Code FROM TSPL_JOURNAL_DETAILS "
    '            strquery += " left OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No "
    '            strquery += " left OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_JOURNAL_MASTER.Source_Doc_No "
    '            strquery += " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE =TSPL_RECEIPT_HEADER.Bank_Code  ) AS xxx "
    '            strquery += " WHERE   2=2 "

    '            If clsCommon.myLen(fromdate) > 0 Then
    '                strquery += " and convert(date,Receipt_Post_Date,103) >= convert(date,'" + fromdate + "',103)"
    '            End If

    '            If clsCommon.myLen(todate) > 0 Then
    '                strquery += " and convert(date,Receipt_Post_Date,103) <= convert(date,'" + todate + "',103)"
    '            End If

    '            If arrreceipt IsNot Nothing AndAlso arrreceipt.Count > 0 Then
    '                strquery += "and Receipt_No='" + recvalue + "'"
    '            End If
    '            If arrcustomer IsNot Nothing AndAlso arrcustomer.Count > 0 Then
    '                strquery += "and Cust_Code in (" + clsCommon.GetMulcallString(arrcustomer) + ")"
    '            End If
    '            dt = clsDBFuncationality.GetDataTable(strquery)
    '            For count As Integer = 0 To dt.Rows.Count - 1
    '                Dim newrow As DataRow = dttemp.NewRow()
    '                newrow("Cheque_No") = Convert.ToString(dt.Rows(count)("Cheque_No"))
    '                newrow("Cheque_Date") = dt.Rows(count)("Cheque_Date")
    '                newrow("Entry_Desc") = Convert.ToString(dt.Rows(count)("Entry_Desc"))
    '                newrow("Receipt_No") = Convert.ToString(dt.Rows(count)("Receipt_No"))
    '                newrow("Receipt_Post_Date") = dt.Rows(count)("Receipt_Post_Date")
    '                newrow("Created_By") = Convert.ToString(dt.Rows(count)("Created_By"))
    '                newrow("Modify_By") = Convert.ToString(dt.Rows(count)("Modify_By"))
    '                newrow("Detail_Line_No") = Convert.ToString(dt.Rows(count)("Detail_Line_No"))
    '                newrow("Account_code") = Convert.ToString(dt.Rows(count)("Account_code"))
    '                newrow("Account_Desc") = Convert.ToString(dt.Rows(count)("Account_Desc"))
    '                newrow("Document_No") = Convert.ToString(dt.Rows(count)("Document_No"))
    '                newrow("Remarks") = Convert.ToString(dt.Rows(count)("Remarks"))
    '                newrow("Comment") = Convert.ToString(dt.Rows(count)("Comment"))
    '                newrow("Amount") = Convert.ToString(dt.Rows(count)("Amount"))
    '                newrow("Description") = Convert.ToString(dt.Rows(count)("Description"))
    '                newrow("Cust_Code") = Convert.ToString(dt.Rows(count)("Cust_Code"))
    '                newrow("Customer_Name") = Convert.ToString(dt.Rows(count)("Customer_Name"))
    '                newrow("BankName") = Convert.ToString(dt.Rows(count)("BankName"))
    '                newrow("CompanyName") = Convert.ToString(dt.Rows(count)("CompanyName"))
    '                newrow("logo1") = DirectCast(dt.Rows(0)("logo1"), Byte())
    '                ''newrow("logo2") = DirectCast(dt.Rows(0)("logo2"), Byte())
    '                newrow("SegName") = Convert.ToString(dt.Rows(count)("SegName"))
    '                newrow("vouchertype") = Convert.ToString(dt.Rows(count)("vouchertype"))
    '                dttemp.Rows.Add(newrow)
    '            Next
    '        Next
    '        FrmSalerReport.funreport(dttemp, "receipt", "Receipt Report")
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
    '    End Try
    'End Sub

    'Public Function funreceiptreport(ByVal strReport As String, ByVal fromdate As String, ByVal todate As String, ByVal chkReceiptSelect As Boolean, ByVal arrreceipt As ArrayList, ByVal chkCustomerSelect As Boolean, ByVal arrCustomer As ArrayList)


    '    Try

    '        Dim strquery As String

    '        Dim strQuerydocno As String = String.Empty
    '        Dim strqueryremarks As String = String.Empty
    '        Dim strquerycomment As String = String.Empty
    '        Dim strdoccomment As String
    '        strquery = "select  TSPL_RECEIPT_DETAIL.Document_No + CASE WHEN len(TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date) < 0 THEN '   ' ELSE ',' + CONVERT(varchar(10), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) END AS Document_No,   TSPL_RECEIPT_DETAIL.Remarks, TSPL_RECEIPT_DETAIL.Comment from TSPL_RECEIPT_DETAIL   LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= TSPL_RECEIPT_DETAIL.Document_No  where TSPL_RECEIPT_DETAIL.Receipt_No =" + clsCommon.GetMulcallString(arrreceipt) + " "
    '        Dim dt As DataTable
    '        dt = clsDBFuncationality.GetDataTable(strquery)
    '        Dim strdocno As String
    '        Dim strremark As String
    '        Dim strcomment As String
    '        Dim arrDocNo As New List(Of String)()
    '        Dim arrRmks As New List(Of String)()
    '        Dim arrComment As New List(Of String)()
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                Dim strTDocNo As String = clsCommon.myCstr(dr("Document_No"))
    '                Dim strTRmks As String = clsCommon.myCstr(dr("Remarks"))
    '                Dim strTComment As String = clsCommon.myCstr(dr("Comment"))

    '                If clsCommon.myLen(strTDocNo) > 0 AndAlso Not arrDocNo.Contains(strTDocNo) Then
    '                    If clsCommon.myLen(strdocno) > 0 Then
    '                        strdocno += ", "
    '                    End If
    '                    strdocno += strTDocNo
    '                Else
    '                    arrDocNo.Add(strTDocNo)
    '                End If



    '                If clsCommon.myLen(strTComment) > 0 AndAlso Not arrComment.Contains(strTComment) Then
    '                    If clsCommon.myLen(strcomment) > 0 Then
    '                        strcomment += ", "
    '                    End If
    '                    strcomment += strTComment
    '                Else
    '                    arrComment.Add(strTComment)
    '                End If
    '            Next
    '        End If
    '        strquery = "SELECT     Cheque_No, Cheque_Date, Entry_Desc, Receipt_No, Receipt_Post_Date, Created_By, Modify_By, Detail_Line_No, Account_code, Account_Desc, " & _
    '                               " '" + strdocno + "' as Document_No,'" + strremark + "'as Remarks,'" + strcomment + "'as Comment, Amount, Description, Cust_Code, Customer_Name, BankName, " & _
    '                               " (SELECT     Comp_Name FROM TSPL_COMPANY_MASTER WHERE      (Comp_Code = 'demo')) AS CompanyName, " & _
    '                               " (SELECT     Logo_Img FROM          TSPL_COMPANY_MASTER WHERE      (Comp_Code = 'demo')) AS logo1, " & _
    '                               " (SELECT     Logo_Img2 FROM          TSPL_COMPANY_MASTER WHERE      (Comp_Code = 'demo')) AS logo2, " & _
    '                               " (SELECT     Description FROM TSPL_GL_SEGMENT_CODE WHERE      (Seg_No = '7') AND (Segment_code = SUBSTRING(xxx.Account_code, LEN(xxx.Account_code) - 2, 3))) AS SegName " & _
    '                               " FROM         (SELECT DISTINCT  " & _
    '                               " TSPL_RECEIPT_HEADER.Cheque_No, TSPL_RECEIPT_HEADER.Cheque_Date, TSPL_RECEIPT_HEADER.Entry_Desc,  " & _
    '                               " TSPL_RECEIPT_HEADER.Receipt_No, TSPL_RECEIPT_HEADER.Receipt_Post_Date, TSPL_RECEIPT_HEADER.Created_By,  " & _
    '                               " TSPL_RECEIPT_HEADER.Modify_By, TSPL_JOURNAL_DETAILS.Detail_Line_No, TSPL_JOURNAL_DETAILS.Account_code,  " & _
    '                                " TSPL_JOURNAL_DETAILS.Account_Desc, TSPL_JOURNAL_DETAILS.Amount,  " & _
    '                               " TSPL_JOURNAL_DETAILS.Description, TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Customer_Name,  " & _
    '                               " TSPL_BANK_MASTER.DESCRIPTION AS BankName FROM TSPL_JOURNAL_DETAILS "
    '        strquery += " left OUTER JOIN TSPL_JOURNAL_MASTER ON TSPL_JOURNAL_MASTER.Journal_No = TSPL_JOURNAL_DETAILS.Journal_No "
    '        strquery += " left OUTER JOIN TSPL_RECEIPT_HEADER ON TSPL_RECEIPT_HEADER.Receipt_No =TSPL_JOURNAL_MASTER.Source_Doc_No "
    '        strquery += " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE =TSPL_RECEIPT_HEADER.Bank_Code  ) AS xxx "
    '        strquery += " WHERE     (2 = 2)  "
    '        If arrreceipt.Count > 0 Then
    '            strquery += "and Receipt_No in (" + clsCommon.GetMulcallString(arrreceipt) + ")"
    '        End If
    '        If arrCustomer.Count > 0 Then
    '            strquery += "and Cust_Code in (" + clsCommon.GetMulcallString(arrCustomer) + ")"
    '        End If
    '        dt = clsDBFuncationality.GetDataTable(strquery)

    '        ' ''If Not String.IsNullOrEmpty(customerfrom) And Not String.IsNullOrEmpty(customerto) Then
    '        ' ''    query = query + " and  xxx.Cust_Code between '" + customerfrom + "' and '" + customerto + " '"
    '        ' ''ElseIf Not String.IsNullOrEmpty(customerfrom) And String.IsNullOrEmpty(customerto) Then
    '        ' ''    query = query + " and  xxx.Cust_Code = '" + customerfrom + "'  "

    '        ' ''End If

    '        ' ''If Not String.IsNullOrEmpty(receiptfrom) And Not String.IsNullOrEmpty(receiptto) Then
    '        ' ''    query = query + " and  xxx.Receipt_No between '" + receiptfrom + "' and '" + receiptto + "' "
    '        ' ''ElseIf Not String.IsNullOrEmpty(receiptfrom) And String.IsNullOrEmpty(receiptto) Then
    '        ' ''    query = query + " and  xxx.Receipt_No = '" + receiptfrom + "'  "

    '        ' ''End If

    '        ' ''If String.IsNullOrEmpty(receiptfrom) And String.IsNullOrEmpty(receiptto) And String.IsNullOrEmpty(customerfrom) And String.IsNullOrEmpty(customerto) Then
    '        ' ''    query = query + " and  TSPL_RECEIPT_HEADER.Receipt_Post_Date  between '" + fromdate + "' and '" + todate + "' "

    '        ' ''End If
    '        ' ''  query = query + " order by Detail_Line_No desc"
    '        'Dim dttemp As New DataTable()
    '        'dttemp.Columns.Add("Cheque_No")
    '        'dttemp.Columns.Add("Cheque_Date")
    '        'dttemp.Columns.Add("Entry_Desc")
    '        'dttemp.Columns.Add("Receipt_No")
    '        'dttemp.Columns.Add("Receipt_Post_Date")
    '        'dttemp.Columns.Add("Created_By")
    '        'dttemp.Columns.Add("Modify_By")
    '        'dttemp.Columns.Add("Detail_Line_No")
    '        'dttemp.Columns.Add("Account_code")
    '        'dttemp.Columns.Add("Account_Desc")
    '        'dttemp.Columns.Add("Document_No")
    '        'dttemp.Columns.Add("Remarks")
    '        'dttemp.Columns.Add("Comment")
    '        'dttemp.Columns.Add("Amount")
    '        'dttemp.Columns.Add("Description")
    '        'dttemp.Columns.Add("Customer_Name")
    '        'dttemp.Columns.Add("BankName")
    '        'dttemp.Columns.Add("CompanyName")
    '        'dttemp.Columns.Add("logo1")
    '        'dttemp.Columns.Add("logo2")
    '        'dttemp.Columns.Add("SegName")

    '        '  Dim newrow As DataRow = dttemp.NewRow()
    '        '  newrow("Cheque_No") = dt("Cheque_No")

    '        FrmSalerReport.funreport(strquery, "receipt", "Receipt Report")

    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
    '    End Try
    'End Function

    Shared Sub PrintData(ByVal fromdate As String, ByVal todate As String, ByVal chkReceiptSelect As Boolean, ByVal arrreceipt As ArrayList, ByVal chkCustomerSelect As Boolean, ByVal arrcustomer As ArrayList, ByVal chklocationselect As Boolean, ByVal arrlocation As ArrayList)
        Dim dttemp As New DataTable()
        Dim strTDocNo As String = String.Empty
        Dim arrDocNo1 As New List(Of String)() ''---- '' added By Abhishek kumar as on 11 july 2012  get DocNo For SubReport
        Try
            If chkReceiptSelect AndAlso arrreceipt.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select at least one Receipt.")
                Return
            ElseIf (chkReceiptSelect) = False Then
                arrreceipt = arrreceipt
            End If
            If chklocationselect AndAlso arrlocation.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select at least one Location.")
                Return
            ElseIf (chklocationselect) = False Then
                arrlocation = arrlocation
            End If
            If chkCustomerSelect And (arrcustomer Is Nothing OrElse arrcustomer.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Select at least one Customer.")
                Return
            ElseIf (chkCustomerSelect) Then
                arrcustomer = arrcustomer
            End If

            Dim qry As String = "select Receipt_No  from TSPL_RECEIPT_HEADER where 2=2"
            If arrreceipt IsNot Nothing AndAlso arrreceipt.Count > 0 Then
                qry += " and Receipt_No in (" + clsCommon.GetMulcallString(arrreceipt) + ")"
            End If
            If arrcustomer IsNot Nothing AndAlso arrcustomer.Count > 0 Then
                qry += "and Cust_Code in (" + clsCommon.GetMulcallString(arrcustomer) + ")"
            End If
            Dim dtdocument As DataTable = clsDBFuncationality.GetDataTable(qry)
            dttemp.Columns.Add("Cheque_No", GetType(System.String))
            dttemp.Columns.Add("Cheque_Date", GetType(System.String))
            dttemp.Columns.Add("Cheque_From", GetType(System.String))
            dttemp.Columns.Add("Entry_Desc", GetType(System.String))
            dttemp.Columns.Add("Receipt_No", GetType(System.String))
            dttemp.Columns.Add("Receipt_Post_Date")
            dttemp.Columns.Add("Created_By")
            dttemp.Columns.Add("Modify_By")
            dttemp.Columns.Add("Detail_Line_No")
            dttemp.Columns.Add("Account_code", GetType(System.String))
            dttemp.Columns.Add("Account_Desc", GetType(System.String))
            dttemp.Columns.Add("Document_No", GetType(System.String))
            dttemp.Columns.Add("Remarks", GetType(System.String))
            dttemp.Columns.Add("Comment", GetType(System.String))
            dttemp.Columns.Add("Amount", GetType(System.Decimal))
            dttemp.Columns.Add("Description", GetType(System.String))
            dttemp.Columns.Add("Cust_Code", GetType(System.String))
            dttemp.Columns.Add("Customer_Name", GetType(System.String))
            dttemp.Columns.Add("BankName", GetType(System.String))
            dttemp.Columns.Add("CompanyName", GetType(System.String))
            Dim logo1 As DataColumn = New DataColumn()
            logo1.DataType = System.Type.GetType("System.Byte[]")

            Dim logo2 As DataColumn = New DataColumn()
            logo2.DataType = System.Type.GetType("System.Byte[]")
            logo1.Caption = "logo1"
            logo2.Caption = "logo2"
            dttemp.Columns.Add(logo1)
            dttemp.Columns.Add(logo2)
            'dttemp.Columns.Add("logo1", GetType(Image))
            'dttemp.Columns.Add("logo2", GetType(Image))
            dttemp.Columns.Add("Posted", GetType(System.String))
            dttemp.Columns.Add("SegName", GetType(System.String))
            dttemp.Columns.Add("vouchertype", GetType(System.String))
            dttemp.Columns.Add("SaleInvoice", GetType(System.String))
            dttemp.Columns.Add("DocNo", GetType(System.String)) '' --- '' added By Abhishek kumar as on 11 july 2012  get DocNo For SubReport
            Dim dt As DataTable
            Dim recvalue As String
            For Each drreceipt As DataRow In dtdocument.Rows
                recvalue = drreceipt("Receipt_No").ToString()
                Dim strquery As String
                Dim strQuerydocno As String = String.Empty
                Dim strqueryremarks As String = String.Empty
                Dim strquerycomment As String = String.Empty
                ' Dim strdoccomment As String
                strquery = "select  TSPL_RECEIPT_DETAIL.Document_No as DocNo,TSPL_RECEIPT_DETAIL.Document_No + CASE WHEN len(TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date) < 0 THEN '   ' ELSE ',' + CONVERT(varchar(10), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) END AS Document_No,   TSPL_RECEIPT_DETAIL.Remarks, TSPL_RECEIPT_DETAIL.Comment from TSPL_RECEIPT_DETAIL   LEFT OUTER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= TSPL_RECEIPT_DETAIL.Document_No  where TSPL_RECEIPT_DETAIL.Receipt_No ='" + recvalue + "' "
                dt = clsDBFuncationality.GetDataTable(strquery)
                Dim strdocno As String = String.Empty
                Dim strremark As String = String.Empty   '''''''''''''''' Added by Abhishek kumar as on 26/09/2012 due to remarks repeating
                Dim strcomment As String = ""
                Dim arrDocNo As New List(Of String)()
                Dim arrRmks As New List(Of String)()
                Dim arrComment As New List(Of String)()
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows

                        Dim strTRmks As String = String.Empty
                        Dim strTComment As String = String.Empty
                        Dim DocNo As String = clsCommon.myCstr(dr("DocNo")) '-- '' added By Abhishek kumar as on 11 july 2012  get DocNo For SubReport
                        strTDocNo = clsCommon.myCstr(dr("Document_No"))
                        strTRmks = clsCommon.myCstr(dr("Remarks"))
                        strTComment = clsCommon.myCstr(dr("Comment"))

                        arrDocNo1.Add(DocNo) '' added By Abhishek kumar as on 11 july 2012  get DocNo For SubReport
                        If clsCommon.myLen(strTDocNo) > 0 AndAlso Not arrDocNo.Contains(strTDocNo) Then
                            If clsCommon.myLen(strdocno) > 0 Then
                                strdocno += ", "
                            End If
                            strdocno += strTDocNo
                        Else
                            arrDocNo.Add(strTDocNo)
                        End If
                        If clsCommon.myLen(strTComment) > 0 AndAlso Not arrComment.Contains(strTComment) Then
                            If clsCommon.myLen(strcomment) > 0 Then
                                strcomment += ", "
                            End If
                            strcomment += strTComment
                        Else
                            arrComment.Add(strTComment)
                        End If
                        If clsCommon.myLen(strTRmks) > 0 AndAlso Not arrDocNo.Contains(strTRmks) Then
                            If clsCommon.myLen(strremark) > 0 Then

                                strremark += ","
                            End If
                            strremark += strTRmks
                        Else
                            arrRmks.Add(strremark)
                        End If

                    Next

                End If


                strremark = strremark.Replace("'", "''")
                '' ---- '' added One Column DocNo in this Query By Abhishek kumar as on 11 july 2012  get DocNo For SubReport
                strquery = "SELECT     Cheque_No,  Cheque_Date, Entry_Desc, Receipt_No,convert(varchar, Receipt_Post_Date,103)as Receipt_Post_Date, Created_By, Modify_By, Detail_Line_No, Account_code, Account_Desc, " & _
                                       " '" + strdocno + "' as Document_No,(Substring('" & strdocno & "',0,charindex(',','" & strdocno & "')))as DocNo,''as Remarks, rmk as Comment, Amount, Description, Cust_Code, Customer_Name, BankName, " & _
                                       " (SELECT     Comp_Name FROM TSPL_COMPANY_MASTER WHERE      (Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')) AS CompanyName, " & _
                                       " (SELECT     Logo_Img FROM          TSPL_COMPANY_MASTER WHERE      (Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')) AS logo1, " & _
                                       " (SELECT     Logo_Img2 FROM          TSPL_COMPANY_MASTER WHERE      (Comp_Code = '" + objCommonVar.CurrentCompanyCode + "')) AS logo2, " & _
                                       " (SELECT     Description FROM TSPL_GL_SEGMENT_CODE WHERE      (Seg_No = '7') AND (Segment_code = SUBSTRING(xxx.Account_code, LEN(xxx.Account_code) - 2, 3))) AS SegName," & _
                "case "
                '' Anubhooti 11-Feb-2015 (If Receipt Type =Refund Then VoucherType Heading should be 'Payment Voucher')
                strquery += " WHEN (Select Receipt_Type From TSPL_RECEIPT_HEADER Where Receipt_No ='" + recvalue + "') ='F' THEN 'Payment Voucher'"

                strquery += " when  xxx.Payment_Code ='cheque' then 'Bank Receipt Voucher' when xxx.Payment_Code ='check' then 'Bank Receipt Voucher'when xxx.Payment_Code ='SETTLEMENT' then'Cash Receipt Voucher'when xxx.Payment_Code ='SETTLEB'then 'Bank Receipt Voucher' when xxx.Payment_Code ='CASH' then 'Cash Receipt Voucher'  " & _
    "  when xxx.Payment_Code ='NEFT' THEN 'NEFT Receipt Voucher'when xxx.Payment_Code ='RTGS' THEN 'RTGS Receipt Voucher' when xxx.Payment_Code ='DD' THEN 'DD Receipt Voucher' when xxx.Payment_Code='OTHER' then '" + recvalue + "'+ ' Voucher'    else (select distinct TSPL_PAYMENT_CODE.Payment_Code  from TSPL_PAYMENT_CODE left outer join TSPL_RECEIPT_HEADER on TSPL_PAYMENT_CODE.Payment_Code =TSPL_RECEIPT_HEADER.Payment_Code  where Receipt_No='" + recvalue + "') +'  Voucher' " & _
    " end   as vouchertype ,xxx.IsShowDocumentNo, Cheque_From,case when Posted ='N' Then 'UN-APPROVED' else 'P' end as Posted,SaleInvoice  FROM  ("
                strquery += " select TSPL_RECEIPT_HEADER.Cheque_No,TSPL_RECEIPT_HEADER.Cheque_Date,TSPL_RECEIPT_HEADER.Entry_Desc, xx.Receipt_No,TSPL_RECEIPT_HEADER.Receipt_Post_Date,TSPL_RECEIPT_HEADER.Created_By,TSPL_RECEIPT_HEADER.Modify_By,0 as Detail_Line_No,xx.ACCode as Account_code,TSPL_GL_ACCOUNTS.Description as Account_Desc,xx.Amount,'' as Description,TSPL_RECEIPT_HEADER.Cust_Code, TSPL_RECEIPT_HEADER.Customer_Name,   TSPL_BANK_MASTER.DESCRIPTION AS BankName ,Payment_Code,xx.IsShowDocumentNo,tspl_gl_accounts.Account_Seg_Code7, TSPL_RECEIPT_HEADER.Cheque_From, xx.Entry_Desc as rmk,TSPL_RECEIPT_HEADER.Posted , SaleInvoice " + Environment.NewLine
                strquery += " from(" + Environment.NewLine
                'strquery += "select TSPL_RECEIPT_HEADER.Receipt_No , Dr_Account as ACCode, Receipt_Amount as Amount,1 as IsShowDocumentNo, '' as Entry_Desc , STUFF((select ', '+ (Case When (ISNULL(Against_Sale_Return_No,'')+ISNULL(Against_Sale_No,''))<>'' Then (ISNULL(Against_Sale_Return_No,'')+ISNULL(Against_Sale_No,'')) Else TSPL_RECEIPT_DETAIL.Document_No End) FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No where Receipt_No= TSPL_RECEIPT_HEADER.Receipt_No For XML PATH ('')),1,1,'') as SaleInvoice from TSPL_RECEIPT_HEADER  left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Dr_Account =TSPL_GL_ACCOUNTS.Account_Code where Receipt_Type<>'S' " + Environment.NewLine
                strquery += "select TSPL_RECEIPT_HEADER.Receipt_No , Dr_Account as ACCode,case when isnull(BASE_CURRENCY_CODE,'')=isnull(CURRENCY_CODE,'') then Receipt_Amount-((Bank_Charges_Amt +Foreign_Bank_Charges_Amt )) else RECEIVED_AMOUNT_BASE_CURRENCY-((Bank_Charges_Amt +Foreign_Bank_Charges_Amt*ConvRate)) end as Amount,1 as IsShowDocumentNo, '' as Entry_Desc , STUFF((select ', '+ (Case When (ISNULL(Against_Sale_Return_No,'')+ISNULL(Against_Sale_No,''))<>'' Then (ISNULL(Against_Sale_Return_No,'')+ISNULL(Against_Sale_No,'')) Else TSPL_RECEIPT_DETAIL.Document_No End) FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No where Receipt_No= TSPL_RECEIPT_HEADER.Receipt_No For XML PATH ('')),1,1,'') as SaleInvoice from TSPL_RECEIPT_HEADER  left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Dr_Account =TSPL_GL_ACCOUNTS.Account_Code where Receipt_Type not in ('S','F') " + Environment.NewLine
                '------------------------aDDED by richa agarwal
                strquery += " union all " + Environment.NewLine
                strquery += " select TSPL_RECEIPT_HEADER.Receipt_No , Dr_Account as ACCode,case when isnull(BASE_CURRENCY_CODE,'')=isnull(CURRENCY_CODE,'') then Receipt_Amount else RECEIVED_AMOUNT_BASE_CURRENCY end as Amount,1 as IsShowDocumentNo, '' as Entry_Desc , STUFF((select ', '+ (Case When (ISNULL(Against_Sale_Return_No,'')+ISNULL(Against_Sale_No,''))<>'' Then (ISNULL(Against_Sale_Return_No,'')+ISNULL(Against_Sale_No,'')) Else TSPL_RECEIPT_DETAIL.Document_No End) FROM TSPL_RECEIPT_DETAIL LEFT OUTER JOIN TSPL_Customer_Invoice_Head ON TSPL_Customer_Invoice_Head.Document_No=TSPL_RECEIPT_DETAIL.Document_No where Receipt_No= TSPL_RECEIPT_HEADER.Receipt_No For XML PATH ('')),1,1,'') as SaleInvoice from TSPL_RECEIPT_HEADER  left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Dr_Account =TSPL_GL_ACCOUNTS.Account_Code where Receipt_Type ='F' " + Environment.NewLine
                '------------------------------------------------Added By-Pankaj Kumar
                strquery += " union all " + Environment.NewLine
                'strquery += "select TSPL_RECEIPT_HEADER.Receipt_No , Cr_Account as ACCode,Receipt_Amount*-1 as Amount,1 as IsShowDocumentNo, '' as Entry_Desc ,'' as SaleInvoice from TSPL_RECEIPT_HEADER  left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Cr_Account =TSPL_GL_ACCOUNTS.Account_Code where Receipt_Type='S' " + Environment.NewLine
                strquery += "select TSPL_RECEIPT_HEADER.Receipt_No , Cr_Account as ACCode,case when isnull(BASE_CURRENCY_CODE,'')=isnull(CURRENCY_CODE,'') then Receipt_Amount*-1 else RECEIVED_AMOUNT_BASE_CURRENCY*-1 end  as Amount,1 as IsShowDocumentNo, '' as Entry_Desc ,'' as SaleInvoice from TSPL_RECEIPT_HEADER  left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Cr_Account =TSPL_GL_ACCOUNTS.Account_Code where Receipt_Type='S' " + Environment.NewLine
                '-----------------------------------------------
                strquery += " union all " + Environment.NewLine
                'strquery += " select TSPL_RECEIPT_HEADER.Receipt_No ,Cr_Account as ACCode ,-1*Receipt_Amount as Amount,1 as IsShowDocumentNo, '' as Entry_Desc ,'' as SaleInvoice from TSPL_RECEIPT_HEADER left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Dr_Account =TSPL_GL_ACCOUNTS.Account_Code  where      Receipt_Type Not In ('M', 'S')" + Environment.NewLine
                strquery += " select TSPL_RECEIPT_HEADER.Receipt_No ,Cr_Account as ACCode ,case when isnull(BASE_CURRENCY_CODE,'')=isnull(CURRENCY_CODE,'') then Receipt_Amount*-1 else RECEIVED_AMOUNT_BASE_CURRENCY*-1 end as Amount,1 as IsShowDocumentNo, '' as Entry_Desc ,'' as SaleInvoice from TSPL_RECEIPT_HEADER left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Dr_Account =TSPL_GL_ACCOUNTS.Account_Code  where Receipt_Type Not In ('M', 'S','F')" + Environment.NewLine
                '------------------------aDDED by richa agarwal
                strquery += " union all " + Environment.NewLine
                ' strquery += "  select TSPL_RECEIPT_HEADER.Receipt_No ,Cr_Account as ACCode ,case when isnull(BASE_CURRENCY_CODE,'')=isnull(CURRENCY_CODE,'') then (Receipt_Amount-((Bank_Charges_Amt +Foreign_Bank_Charges_Amt )))*-1 else (RECEIVED_AMOUNT_BASE_CURRENCY-((Bank_Charges_Amt +Foreign_Bank_Charges_Amt*ConvRate)))*-1 end as Amount,1 as IsShowDocumentNo, '' as Entry_Desc ,'' as SaleInvoice from TSPL_RECEIPT_HEADER left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Dr_Account =TSPL_GL_ACCOUNTS.Account_Code  where Receipt_Type='F' " + Environment.NewLine
                strquery += "  select TSPL_RECEIPT_HEADER.Receipt_No ,Cr_Account as ACCode ,case when isnull(BASE_CURRENCY_CODE,'')=isnull(CURRENCY_CODE,'') then (Receipt_Amount + Bank_Charges_Amt -(( Foreign_Bank_Charges_Amt )))*-1 else (RECEIVED_AMOUNT_BASE_CURRENCY + Bank_Charges_Amt -((Foreign_Bank_Charges_Amt*ConvRate)))*-1 end as Amount,1 as IsShowDocumentNo, '' as Entry_Desc ,'' as SaleInvoice from TSPL_RECEIPT_HEADER left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Dr_Account =TSPL_GL_ACCOUNTS.Account_Code  where Receipt_Type='F' " + Environment.NewLine
                ''-----------------------------------------
                strquery += " union all " + Environment.NewLine
                ''richa agarwal 09/06/2015
                strquery += " Select TSPL_RECEIPT_HEADER.Receipt_No,TSPL_CUSTOMER_ACCOUNT_SET.Foreign_Bank_Charges_Account as ACCode, TSPL_RECEIPT_HEADER.Foreign_Bank_Charges_Amt*TSPL_RECEIPT_HEADER.ConvRate * (case when TSPL_RECEIPT_HEADER.Receipt_Type ='F' then -1 else 1 end) as Amount,1 as IsShowDocumentNo, '' as Entry_Desc ,'' as SaleInvoice  from TSPL_RECEIPT_HEADER " & _
                " Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_RECEIPT_HEADER.Cust_Code " & _
                " Left Outer Join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account where TSPL_RECEIPT_HEADER.Foreign_Bank_Charges_Amt<>0"
                strquery += " union all " + Environment.NewLine
                'strquery += "Select TSPL_RECEIPT_HEADER.Receipt_No,  TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account as ACCode,TSPL_RECEIPT_HEADER.Bank_Charges_Amt * (case when TSPL_RECEIPT_HEADER.Receipt_Type ='F' then -1 else 1 end)  as Amount,1 as IsShowDocumentNo, '' as Entry_Desc ,'' as SaleInvoice  from TSPL_RECEIPT_HEADER " & _
                strquery += "Select TSPL_RECEIPT_HEADER.Receipt_No,  TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account as ACCode,TSPL_RECEIPT_HEADER.Bank_Charges_Amt  as Amount,1 as IsShowDocumentNo, '' as Entry_Desc ,'' as SaleInvoice  from TSPL_RECEIPT_HEADER " & _
                "Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_RECEIPT_HEADER.Cust_Code " & _
                "Left Outer Join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account where TSPL_RECEIPT_HEADER.Bank_Charges_Amt <>0 "
                strquery += " union all " + Environment.NewLine
                ''---------------------------
                strquery += " select TSPL_RECEIPT_HEADER.Receipt_No, Account_Code as ACCode ,-1*Applied_Amount as Amount,1 as IsShowDocumentNo, TSPL_RECEIPT_DETAIL.Remarks ,'' as SaleInvoice" + Environment.NewLine
                strquery += " from TSPL_RECEIPT_DETAIL " + Environment.NewLine
                strquery += " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " + Environment.NewLine
                strquery += " where     TSPL_RECEIPT_HEADER.Receipt_Type ='M'" + Environment.NewLine
                strquery += " union all " + Environment.NewLine
                '-------------------------------------------Added By-pankaj kumar
                strquery += " select TSPL_RECEIPT_HEADER.Receipt_No, Account_Code as ACCode ,Applied_Amount as Amount,1 as IsShowDocumentNo, TSPL_RECEIPT_DETAIL.Remarks,'' as SaleInvoice " + Environment.NewLine
                strquery += " from TSPL_RECEIPT_DETAIL " + Environment.NewLine
                strquery += " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=TSPL_RECEIPT_DETAIL.Receipt_No " + Environment.NewLine
                strquery += " where     TSPL_RECEIPT_HEADER.Receipt_Type ='S'" + Environment.NewLine
                '-------------------------------------------
                strquery += "union all " + Environment.NewLine
                strquery += "select TSPL_RECEIPT_HEADER.Receipt_No , Dr_Account as ACCode,UnApplied_Balance as Amount,0 as IsShowDocumentNo, '' as Entry_Desc ,'' as SaleInvoice from TSPL_RECEIPT_HEADER left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Dr_Account =TSPL_GL_ACCOUNTS.Account_Code  " + Environment.NewLine
                strquery += "where LEN(ISNULL(UnApplied_No,''))>0 " + Environment.NewLine
                strquery += "union all " + Environment.NewLine
                strquery += "select TSPL_RECEIPT_HEADER.Receipt_No ,Cr_Account as ACCode ,-1*UnApplied_Balance as Amount,0 as IsShowDocumentNo, '' as Entry_Desc  ,'' as SaleInvoice from TSPL_RECEIPT_HEADER left outer join TSPL_GL_ACCOUNTS  on TSPL_RECEIPT_HEADER.Dr_Account =TSPL_GL_ACCOUNTS.Account_Code " + Environment.NewLine
                strquery += "where LEN(ISNULL(UnApplied_No,''))>0" + Environment.NewLine

                strquery += " )xx " + Environment.NewLine
                strquery += " left outer join TSPL_RECEIPT_HEADER on TSPL_RECEIPT_HEADER.Receipt_No=xx.Receipt_No"
                strquery += " left  outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=xx.ACCode"
                strquery += " LEFT OUTER JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE =TSPL_RECEIPT_HEADER.Bank_Code "

                strquery += " ) AS xxx "
                strquery += " WHERE   2=2 "


                If clsCommon.myLen(fromdate) > 0 Then
                    strquery += " and convert(date,Receipt_Post_Date,103) >= convert(date,'" + fromdate + "',103)"
                End If

                If clsCommon.myLen(todate) > 0 Then
                    strquery += " and convert(date,Receipt_Post_Date,103) <= convert(date,'" + todate + "',103)"
                End If

                If arrreceipt IsNot Nothing AndAlso arrreceipt.Count > 0 Then
                    strquery += "and Receipt_No='" + recvalue + "'"
                End If
                If arrcustomer IsNot Nothing AndAlso arrcustomer.Count > 0 Then
                    strquery += "and Cust_Code in (" + clsCommon.GetMulcallString(arrcustomer) + ")"
                End If
                If arrlocation IsNot Nothing AndAlso arrlocation.Count > 0 Then
                    strquery += "and xxx. Account_Seg_Code7  in (" + clsCommon.GetMulcallString(arrlocation) + ")"
                End If
                dt = clsDBFuncationality.GetDataTable(strquery)
                For count As Integer = 0 To dt.Rows.Count - 1
                    Dim newrow As DataRow = dttemp.NewRow()
                    newrow("Cheque_No") = Convert.ToString(dt.Rows(count)("Cheque_No"))
                    If clsCommon.myLen(dt.Rows(count)("Cheque_No")) <= 0 Then
                        newrow("Cheque_Date") = Nothing
                    Else
                        newrow("Cheque_Date") = dt.Rows(count)("Cheque_Date")
                    End If
                    newrow("Entry_Desc") = Convert.ToString(dt.Rows(count)("Entry_Desc"))
                    newrow("Receipt_No") = Convert.ToString(dt.Rows(count)("Receipt_No"))
                    newrow("Receipt_Post_Date") = dt.Rows(count)("Receipt_Post_Date")
                    newrow("Created_By") = Convert.ToString(dt.Rows(count)("Created_By"))
                    newrow("Modify_By") = Convert.ToString(dt.Rows(count)("Modify_By"))
                    newrow("Detail_Line_No") = Convert.ToString(dt.Rows(count)("Detail_Line_No"))
                    Dim glloccode As String = String.Empty
                    Dim strcusnamacc As String = String.Empty
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Receipt_Type,'') from TSPL_RECEIPT_header where Receipt_No ='" & Convert.ToString(dt.Rows(count)("Receipt_No")) & "'")), "F") = CompairStringResult.Equal Then
                        If Convert.ToDouble(dt.Rows(count)("Amount")) > 0 Then
                            glloccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Location_GL_Code,'') from TSPL_RECEIPT_header where Receipt_No ='" & Convert.ToString(dt.Rows(count)("Receipt_No")) & "'"))
                            If glloccode <> "" AndAlso clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(TSPL_CUSTOMER_ACCOUNT_SET.Bank_Charges_Other_Account,'') from  TSPL_CUSTOMER_MASTER  Left Outer Join TSPL_CUSTOMER_ACCOUNT_SET on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account where TSPL_CUSTOMER_MASTER .Cust_Code ='" & clsCommon.myCstr(dt.Rows(count)("Cust_Code")) & "'")), clsCommon.myCstr(dt.Rows(count)("Account_code"))) <> CompairStringResult.Equal Then
                                strcusnamacc = clsERPFuncationality.ChangeGLAccountLocationSegment(Convert.ToString(dt.Rows(count)("Account_code")), glloccode, True, Nothing)
                                newrow("Account_code") = strcusnamacc
                                newrow("Account_Desc") = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_GL_ACCOUNTS.Description from TSPL_GL_ACCOUNTS where Account_Code ='" & strcusnamacc & "'"))
                            Else
                                newrow("Account_code") = Convert.ToString(dt.Rows(count)("Account_code"))
                                newrow("Account_Desc") = Convert.ToString(dt.Rows(count)("Account_Desc"))
                            End If
                        Else
                            newrow("Account_code") = Convert.ToString(dt.Rows(count)("Account_code"))
                            newrow("Account_Desc") = Convert.ToString(dt.Rows(count)("Account_Desc"))
                        End If
                    Else
                        If Convert.ToDouble(dt.Rows(count)("Amount")) < 0 Then
                            glloccode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(Location_GL_Code,'') from TSPL_RECEIPT_header where Receipt_No ='" & Convert.ToString(dt.Rows(count)("Receipt_No")) & "'"))
                            If glloccode <> "" Then
                                strcusnamacc = clsERPFuncationality.ChangeGLAccountLocationSegment(Convert.ToString(dt.Rows(count)("Account_code")), glloccode, True, Nothing)
                                newrow("Account_code") = strcusnamacc
                                newrow("Account_Desc") = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select TSPL_GL_ACCOUNTS.Description from TSPL_GL_ACCOUNTS where Account_Code ='" & strcusnamacc & "'"))
                            Else
                                newrow("Account_code") = Convert.ToString(dt.Rows(count)("Account_code"))
                                newrow("Account_Desc") = Convert.ToString(dt.Rows(count)("Account_Desc"))
                            End If
                        Else
                            newrow("Account_code") = Convert.ToString(dt.Rows(count)("Account_code"))
                            newrow("Account_Desc") = Convert.ToString(dt.Rows(count)("Account_Desc"))
                        End If
                    End If


                    If clsCommon.myCdbl(dt.Rows(count)("IsShowDocumentNo")) = 1 Then
                        newrow("Document_No") = Convert.ToString(dt.Rows(count)("Document_No"))
                    End If
                    newrow("DocNo") = Convert.ToString(dt.Rows(count)("DocNo")) '' added By Abhishek kumar as on 11 july 2012  get DocNo For SubReport
                    newrow("Remarks") = Convert.ToString(dt.Rows(count)("Remarks"))
                    newrow("Comment") = Convert.ToString(dt.Rows(count)("Comment"))
                    'newrow("Amount") = Convert.ToString(dt.Rows(count)("Amount"))
                    newrow("Amount") = clsCommon.myCdbl(dt.Rows(count)("Amount"))
                    newrow("Description") = Convert.ToString(dt.Rows(count)("Description"))
                    newrow("Cust_Code") = Convert.ToString(dt.Rows(count)("Cust_Code"))
                    newrow("Customer_Name") = Convert.ToString(dt.Rows(count)("Customer_Name"))
                    newrow("BankName") = Convert.ToString(dt.Rows(count)("BankName"))
                    newrow("CompanyName") = Convert.ToString(dt.Rows(count)("CompanyName"))
                    If clsCommon.myLen(dt.Rows(0)("logo1")) > 0 Then
                        newrow("Column1") = DirectCast(dt.Rows(0)("logo1"), Byte())
                    End If
                    If clsCommon.myLen(dt.Rows(0)("logo2")) > 0 Then
                        newrow("Column2") = DirectCast(dt.Rows(0)("logo2"), Byte())
                    End If

                    newrow("Posted") = Convert.ToString(dt.Rows(count)("Posted"))
                    newrow("SegName") = Convert.ToString(dt.Rows(count)("SegName"))
                    newrow("vouchertype") = Convert.ToString(dt.Rows(count)("vouchertype"))
                    newrow("Cheque_From") = Convert.ToString(dt.Rows(count)("Cheque_From"))
                    newrow("SaleInvoice") = Convert.ToString(dt.Rows(count)("SaleInvoice"))
                    dttemp.Rows.Add(newrow)
                Next
            Next
            '' added By Abhishek kumar as on 11 july 2012   For SubReport
            Dim adjustSubReport As String = "select finalQry .*,TSPL_COMPANY_MASTER.Comp_Name as compname, TSPL_COMPANY_MASTER.Logo_Img as Image1, TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select max(ADD1 + case when len(add2)> 0 then ',' else '' end + ADD2 +case when len(add3)> 0 then ','else '' end +ADD3+case when len(add4)> 0 then ',' else '' end +ADD4+case when len(City_Code)> 0 then ',' else '' end +City_Code +case when len(STATE)> 0 then ',' else '' end  +STATE) from tspl_location_master where Location_Code in(select Location_Code from TSPL_LOCATION_MASTER where Loc_Segment_Code =(substring (finalqry.AcctNo ,LEN(finalqry.AcctNo)-2,5)))   )as address from (select xx.Adjustment_No,Convert(varchar,xx.Adjustment_Date,103) as Adjustment_Date ,xx.Customer_No ,xx.Customer_Name ,xx.AcctNo ,xx.AcctDesc ,xx.DbtAmt ,xx.CrAmt ,xx.Comp_Code,xx.Doc_No  from " & _
                  " (SELECT  TSPL_Receipt_Adjustment_Detail.Adjustment_No  ,Adjustment_Date,TSPL_Receipt_Adjustment_Header.Customer_No ,  (select Customer_Name from TSPL_CUSTOMER_MASTER where cust_Code =TSPL_Receipt_Adjustment_Header .Customer_No )as Customer_Name,TSPL_Receipt_Adjustment_Detail.Account_No as AcctNo,Account_Description as AcctDesc,Amount as DbtAmt,0 as CrAmt,TSPL_Receipt_Adjustment_Header .Comp_Code,TSPL_Receipt_Adjustment_Header .Doc_No  FROM TSPL_Receipt_Adjustment_Detail left outer join TSPL_Receipt_Adjustment_Header on  TSPL_Receipt_Adjustment_Detail.Adjustment_No = TSPL_Receipt_Adjustment_Header .Adjustment_No   where TSPL_Receipt_Adjustment_Header .Doc_No  in (" & clsCommon.GetMulcallString(arrDocNo1) & ")" & _
                   " union all " & _
                   " select TSPL_Receipt_Adjustment_Header.Adjustment_No ,TSPL_Receipt_Adjustment_Header .Adjustment_Date  ,TSPL_Receipt_Adjustment_Header .Customer_No,(select Customer_Name from TSPL_CUSTOMER_MASTER where cust_Code =TSPL_Receipt_Adjustment_Header .Customer_No )as Customer_Name,(select TSPL_CUSTOMER_ACCOUNT_SET .Receivable_Control_acct from TSPL_CUSTOMER_ACCOUNT_SET where TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account =TSPL_CUSTOMER_master.Cust_Account) as Acct,(select Description from TSPL_GL_ACCOUNTS where Account_Code =(select TSPL_CUSTOMER_ACCOUNT_SET .Receivable_Control_acct  from TSPL_CUSTOMER_ACCOUNT_SET where TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account =TSPL_CUSTOMER_master.Cust_Account))as AcctDesc,0 as DbtAmt,TSPL_Receipt_Adjustment_Header.Adjustment_Amount as CrAmt ,TSPL_Receipt_Adjustment_Header .Comp_Code,TSPL_Receipt_Adjustment_Header .Doc_No from TSPL_Receipt_Adjustment_Header left outer join TSPL_CUSTOMER_MASTER on TSPL_Receipt_Adjustment_Header .Customer_No =tspl_customer_master.Cust_Code  where TSPL_Receipt_Adjustment_Header .Doc_No  in (" & clsCommon.GetMulcallString(arrDocNo1) & "))as xx )as finalQry left outer join TSPL_COMPANY_MASTER on finalQry .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code "
            '------ Code Ends Here --------------
            frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.SalesReport, dttemp, clsDBFuncationality.GetDataTable(adjustSubReport), "receipt", "Receipt Report", "AdjustmentSubReport.rpt")

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub


    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub Frmreceiptvoucher2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpfrom.Value = clsCommon.GETSERVERDATE().Date
        dtpto.Value = clsCommon.GETSERVERDATE().Date
        chkCustomerAll.IsChecked = True
        chkReceiptAll.IsChecked = True
        LoadReceipt()
        LoadCustomer()
        loadlocation()
        ButtonToolTip.SetToolTip(btnprint, "Press Ctrl+P for Print")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        SetUserMgmtNew()
    End Sub
    Sub loadlocation()
        ' Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Sub LoadReceipt()
        Dim strquery As String = "select Receipt_No as [Receipt No], Receipt_Date as [Receipt Date] from TSPL_RECEIPT_HEADER where (Receipt_Post_Date >= Convert(DATE,'" + dtpfrom.Value + "',103) AND Receipt_Post_Date <= CONVERT(DATE,'" + dtpto.Value + "',103))"
        cbgReceipt.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgReceipt.ValueMember = "Receipt No"
        cbgReceipt.DisplayMember = "Description"
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Private Sub chkReceiptAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkReceiptAll.ToggleStateChanged, chkReceiptSelect.ToggleStateChanged
        cbgReceipt.Enabled = Not chkReceiptAll.IsChecked
        If chkReceiptSelect.IsChecked Then
            chkCustomerAll.IsChecked = chkReceiptSelect.IsChecked
        End If
    End Sub
    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged, chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
        If chkCustomerSelect.IsChecked Then
            chkReceiptAll.IsChecked = chkCustomerSelect.IsChecked
        End If
    End Sub
    Private Sub dtpfrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpfrom.ValueChanged
        LoadReceipt()
    End Sub
    Private Sub dtpto_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpto.ValueChanged
        LoadReceipt()
    End Sub

    Private Sub Frmreceiptvoucher2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
       
        End If
    End Sub


    Private Sub cbgLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbgLocation.Load

    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
End Class
