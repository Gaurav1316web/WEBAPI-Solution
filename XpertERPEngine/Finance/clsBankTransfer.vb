Imports common
Imports System.Data.SqlClient
Public Class clsBankTransfer
    Public Transfer_No As String = ""
    Public Transfer_Date As Date
    Public Description As String
    Public Check_Print As Integer
    Public Check_Code As String = ""
    Public From_Bank_Code As String
    Public From_Bank_Name As String
    Public From_Bank_Acc_No As String
    Public Cheque_No As String
    Public Transfer_Amount As Decimal = 0

    Public Shared Function PostData(ByVal BnkTrnsfrNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(BnkTrnsfrNo) <= 0) Then
                Throw New Exception("Bank Transfer No not found to Post")
            Else
                Dim obj As clsBankTransfer = clsBankTransfer.GetData(BnkTrnsfrNo, NavigatorType.Current, trans)
                CheckNegativeBankBalance(obj, trans)
                Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                Dim qry As String = "UPDATE TSPL_BANK_TRANSFER SET POST = 'P' WHERE TRANSFER_NO = '" + +"'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CheckNegativeBankBalance(ByVal obj As clsBankTransfer, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        '' Validation check: by Panch Raj against ticket No:BM00000008437,BM00000008719

        Dim Bank_Type_Check As String = "0"
        Bank_Type_Check = clsFixedParameter.GetData(clsFixedParameterType.StopNegativeBankBalance, clsFixedParameterCode.StopNegativeBankBalance, trans)
        Dim Bank_Type As String = clsBankMaster.GetBankType(obj.From_Bank_Code, trans)
        Dim Bank_Balance As Decimal = 0
        If clsCommon.CompairString(Bank_Type_Check, "0") = CompairStringResult.Equal Then
            '' allow for all
        ElseIf clsCommon.CompairString(Bank_Type_Check, "1") = CompairStringResult.Equal Then
            If clsCommon.CompairString(Bank_Type, "C") = CompairStringResult.Equal OrElse clsCommon.CompairString(Bank_Type, "P") = CompairStringResult.Equal Then
                ''richa agarwal 03 Aug,2016
                'Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_Date, obj.From_Bank_Code, clsERPFuncationality.GetLocationSegment(obj.From_Bank_Acc_No, trans), trans)
                Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_No, obj.Transfer_Date, obj.From_Bank_Code, clsERPFuncationality.GetLocationSegment(obj.From_Bank_Acc_No, trans), trans)
                If Bank_Balance < obj.Transfer_Amount Then
                    Throw New Exception("Payment Amount : " & obj.Transfer_Amount & " Available Bank Balance(" & obj.From_Bank_Acc_No & ") : " & Bank_Balance & "")
                End If
            End If
        ElseIf clsCommon.CompairString(Bank_Type_Check, "2") = CompairStringResult.Equal Then
            If clsCommon.CompairString(Bank_Type, "B") = CompairStringResult.Equal Then
                ''richa agarwal 03 Aug,2016
                'Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_Date, obj.From_Bank_Code, clsERPFuncationality.GetLocationSegment(obj.From_Bank_Acc_No, trans), trans)
                Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_No, obj.Transfer_Date, obj.From_Bank_Code, clsERPFuncationality.GetLocationSegment(obj.From_Bank_Acc_No, trans), trans)
                If Bank_Balance < obj.Transfer_Amount Then
                    Throw New Exception("Payment Amount : " & obj.Transfer_Amount & " Available Bank Balance(" & obj.From_Bank_Acc_No & ") : " & Bank_Balance & "")
                End If
            End If
        ElseIf clsCommon.CompairString(Bank_Type_Check, "3") = CompairStringResult.Equal Then
            ''richa agarwal 03 Aug,2016
            'Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_Date, obj.From_Bank_Code, clsERPFuncationality.GetLocationSegment(obj.From_Bank_Acc_No, trans), trans)
            Bank_Balance = clsPaymentHeader.GetBankBalance(obj.Transfer_No, obj.Transfer_Date, obj.From_Bank_Code, clsERPFuncationality.GetLocationSegment(obj.From_Bank_Acc_No, trans), trans)
            If Bank_Balance < obj.Transfer_Amount Then
                Throw New Exception("Payment Amount : " & obj.Transfer_Amount & " Available Bank Balance(" & obj.From_Bank_Acc_No & ") : " & Bank_Balance & "")
            End If
        End If

        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsBankTransfer
        Dim obj As New clsBankTransfer
        Dim qry As String = "select * from TSPL_BANK_TRANSFER where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TRANSFER_NO = (select MIN(TRANSFER_NO) from TSPL_BANK_TRANSFER)"
            Case NavigatorType.Last
                qry += " and TRANSFER_NO = (select Max(TRANSFER_NO) from TSPL_BANK_TRANSFER)"
            Case NavigatorType.Next
                qry += " and TRANSFER_NO = (select Min(TRANSFER_NO) from TSPL_BANK_TRANSFER where  TRANSFER_NO>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TRANSFER_NO = (select Max(TRANSFER_NO) from TSPL_BANK_TRANSFER where TRANSFER_NO<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TRANSFER_NO = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj.SALARY_STRUCTURE_NAME = dt.Rows(0)("SALARY_STRUCTURE_NAME")
            obj.Transfer_No = clsCommon.myCstr(dt.Rows(0)("Transfer_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Check_Print = clsCommon.myCstr(dt.Rows(0)("Check_Print"))
            obj.Check_Code = clsCommon.myCstr(dt.Rows(0)("Check_Code"))
            obj.From_Bank_Code = clsCommon.myCstr(dt.Rows(0)("From_Bank_Code"))
            obj.From_Bank_Name = clsCommon.myCstr(dt.Rows(0)("From_Bank_Name"))
            obj.From_Bank_Acc_No = clsCommon.myCstr(dt.Rows(0)("From_Bank_Acc_No"))
            obj.Cheque_No = clsCommon.myCstr(dt.Rows(0)("Cheque_No"))
            obj.Transfer_Date = clsCommon.myCDate(dt.Rows(0)("Transfer_Date"))
            obj.Transfer_Amount = clsCommon.myCdbl(dt.Rows(0)("Transfer_Amount"))
        End If
        Return obj
    End Function
End Class
