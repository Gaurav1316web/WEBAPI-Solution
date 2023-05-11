Imports common
Imports System.Data.SqlClient
Public Class clsPurchaseJobWork
#Region "Variables"
    Public Document_No As String = Nothing
    Public SNo As Integer
    Public AP_Invoice_No As String = Nothing
    Public AR_Invoice_No As String = Nothing
    Public Receipt_No As String = Nothing
    Public Payment_No As String = Nothing
    Public Revaluate_Amount As Decimal
    Public Balance_Amount As Decimal
    Public Gain_Amount As Decimal
    Public Loss_Amount As Decimal
    Public Trans_Sub_Type As String
    Public Tran_Conv_Rate As Decimal
    Public Vendor_Customer_Code As String = Nothing ''Not a Table Column
    Public Vendor_Customer_Name As String = Nothing ''Not a Table Column
    Public Invoice_Date As DateTime ''Not a Table Column
    Public Invoice_Amount As Decimal ''Not a Table Column
    Public Company_Currency_Amount As Decimal ''Not a Table Column
    Public Loc_Segment As String ''Not a Table Column
    Public Document_Date As DateTime
    Public Description As String
    Public Trans_Type As String = Nothing
    Public Currency_Code As String = Nothing
    Public Currency_Rate As Decimal
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_By As String = Nothing
    Public Posted_Date As DateTime? = Nothing


#End Region

    Public Shared Function CreateAPInvoice(ByVal strCode As String, Optional ByVal FORMTYPE As String = Nothing)

        Dim objVendInv As New clsVedorInvoiceHead()
        Dim objtr As New clsPurchaseJobWork()
        objtr = clsPurchaseOrderHead.GetData(strCode, NavigatorType.Current, "", IIf(clsCommon.CompairString(FORMTYPE, clsUserMgtCode.FrmPurchaseOrderMT) = CompairStringResult.Equal, "MT", "PO"))
        objVendInv.Document_No = ""
        objVendInv.Invoice_Entry_Date = objtr.Document_Date
        Dim dblAmt As Double = 0
        If objtr.Gain_Amount > 0 Then
            objVendInv.Document_Type = "D"
            dblAmt = objtr.Gain_Amount
        Else
            objVendInv.Document_Type = "C"
            dblAmt = objtr.Loss_Amount
        End If
        objVendInv.Invoice_Type = "AP"
        objVendInv.loc_code = objtr.Loc_Segment
        objVendInv.Document_Total = dblAmt
        objVendInv.Vendor_Code = objtr.Vendor_Customer_Code
        objVendInv.Vendor_Name = objtr.Vendor_Customer_Name
        objVendInv.Posting_Date = objtr.Document_Date
        objVendInv.Vendor_Invoice_Date = objtr.Document_Date
        Dim strDocNo As String = ""

        If clsCommon.CompairString(objtr.Trans_Type, "AP") = CompairStringResult.Equal Then
            objVendInv.Description = "Auto Generated Entry for Revalualtion No - " + objtr.Document_No + " And Source Document No - " + objtr.AP_Invoice_No + " At Line No - " + clsCommon.myCstr(objtr.SNo)
        ElseIf clsCommon.CompairString(objtr.Trans_Type, "PY") = CompairStringResult.Equal Then
            objVendInv.Description = "Auto Generated Entry for Revalualtion No - " + objtr.Document_No + " And Source Document No - " + objtr.Payment_No + " At Line No - " + clsCommon.myCstr(objtr.SNo)
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = " select TSPL_VENDOR_MASTER.Vendor_Account,TSPL_VENDOR_MASTER.Terms_Code,TSPL_TERMS_MASTER.Terms_Desc,TSPL_TERMS_MASTER.No_Days  from TSPL_VENDOR_MASTER left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_VENDOR_MASTER.Terms_Code where  TSPL_VENDOR_MASTER.Vendor_Code='" + objtr.Vendor_Customer_Code + "'"
        Dim dtVendor As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtVendor Is Nothing OrElse dtVendor.Rows.Count <= 0 Then
            Throw New Exception("Please set vendor account of " + objtr.Vendor_Customer_Code)
        End If

        objVendInv.Account_Set = clsCommon.myCstr(dtVendor.Rows(0)("Vendor_Account"))
        objVendInv.Terms_Code = clsCommon.myCstr(dtVendor.Rows(0)("Terms_Code"))
        objVendInv.Terms_Description = clsCommon.myCstr(dtVendor.Rows(0)("Terms_Desc"))
        objVendInv.Due_Date = objtr.Document_Date.AddDays(clsCommon.myCdbl(dtVendor.Rows(0)("No_Days")))

        objVendInv.On_Hold = 0
        objVendInv.Remarks = ""
        'objVendInv.Description = obj.Description
        objVendInv.Balance_Amt = dblAmt
        objVendInv.RefDocType = "Purchase JobWork"
        objVendInv.RefDocNo = objtr.Document_No
        objVendInv.ConvRate = 1
        objVendInv.CURRENCY_CODE = objCommonVar.BaseCurrencyCode
        objVendInv.Discount_Base = dblAmt
        objVendInv.Amount_Less_Discount = dblAmt
        objVendInv.Document_Total = dblAmt
        objVendInv.Arr = New List(Of clsVedorInvoiceDetail)

        '' Detail Saving
        ''----------------------------------------------
        Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()
        Dim dtAC As DataTable = clsDBFuncationality.GetDataTable("SELECT TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_GAIN_ACCOUNT, TSPL_VENDOR_ACCOUNT_SET.EXCHANGE_LOSS_ACCOUNT FROM TSPL_VENDOR_MASTER LEFT OUTER JOIN TSPL_VENDOR_ACCOUNT_SET ON TSPL_VENDOR_ACCOUNT_SET.Acct_Set_Code =TSPL_VENDOR_MASTER.Vendor_Account  WHERE TSPL_VENDOR_MASTER.Vendor_Code ='" + objtr.Vendor_Customer_Code + "' ", trans)
        If dtAC Is Nothing AndAlso dtAC.Rows.Count <= 0 Then
            Throw New Exception("Please set vendor account set")
        End If
        Dim strAccount As String = ""

        If objtr.Gain_Amount > 0 Then
            If clsCommon.myLen(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT")) <= 0 Then
                Throw New Exception("Please set Exchage gain account for vendor" + objtr.Vendor_Customer_Code)
            End If
            strAccount = clsCommon.myCstr(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT"))

        ElseIf objtr.Loss_Amount > 0 Then
            If clsCommon.myLen(dtAC.Rows(0)("EXCHANGE_GAIN_ACCOUNT")) <= 0 Then
                Throw New Exception("Please set Exchage Loss account for vendor" + objtr.Vendor_Customer_Code)
            End If
            strAccount = clsCommon.myCstr(dtAC.Rows(0)("EXCHANGE_LOSS_ACCOUNT"))
        End If
        strAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strAccount, objVendInv.loc_code, True, trans)

        objVendInvTR.Detail_Line_No = 1


        objVendInvTR.Amount = dblAmt
        objVendInvTR.Amount_less_Discount = dblAmt
        objVendInvTR.Total_Amount = dblAmt


        objVendInvTR.GL_Account_Code = strAccount
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendInv.Account_Set + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objVendInv.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
            objVendInv.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInv.Vendor_Control_AC, objVendInv.loc_code, True, trans)
        End If
        If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Please set the vendor payable Account")
        End If

        objVendInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & strAccount & "'", trans))
        objVendInv.Arr.Add(objVendInvTR)

        ''----------------------------------------------

        objVendInv.SaveData(objVendInv, True, trans)
        clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)
        Return True
    End Function

End Class




