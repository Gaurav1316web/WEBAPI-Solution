'--------Created By Richa 29/08/2014 Against Ticket No BM00000003640
Imports System.Data.SqlClient
Imports common
Public Class ClsFixedDeposit

#Region "variables"
    Public Document_No As String = Nothing
    Public Document_Date As Date?
    Public Bank_Code As String = Nothing
    Public Duration As Integer = 0
    Public Amount As Double = 0
    Public Rate As Double = 0
    Public MaturityAmount As Double = 0
    Public BankName As String = Nothing
    Public Duration_Desp As String = Nothing
    Public FDRNo As String = Nothing
    Public LCRequestNo As String = Nothing
    Public Due_Date As Date?
    Public GL_Account_Code As String = Nothing
    Public GL_Account_Name As String = Nothing
    Public Posted As Integer = 0
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No as [Code],TSPL_FIXED_DEPOSIT_MASTER_MT.Document_Date as Date,TSPL_FIXED_DEPOSIT_MASTER_MT.Bank_Code as [Bank Code],TSPL_FIXED_DEPOSIT_MASTER_MT.Duration,TSPL_FIXED_DEPOSIT_MASTER_MT.Duration_Desp AS [Duration Desp],TSPL_FIXED_DEPOSIT_MASTER_MT.Amount,TSPL_FIXED_DEPOSIT_MASTER_MT.Created_By as [Created By] ,convert(varchar,TSPL_FIXED_DEPOSIT_MASTER_MT.Created_Date,103) as [Created Date],TSPL_FIXED_DEPOSIT_MASTER_MT.Modified_By as [Modified By],convert(varchar,TSPL_FIXED_DEPOSIT_MASTER_MT.Modified_Date,103) as [Modified Date] ,TSPL_FIXED_DEPOSIT_MASTER_MT.Comp_Code as [Comp Code] from TSPL_FIXED_DEPOSIT_MASTER_MT"
        str = clsCommon.ShowSelectForm("FixedDepositCode", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As ClsFixedDeposit, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.FixedDeposit, "", "")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
            clsCommon.AddColumnsForChange(coll, "Duration", obj.Duration)
            clsCommon.AddColumnsForChange(coll, "Duration_Desp", obj.Duration_Desp)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "MaturityAmount", obj.MaturityAmount)
            clsCommon.AddColumnsForChange(coll, "LCRequestNo", obj.LCRequestNo, True)
            clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
            clsCommon.AddColumnsForChange(coll, "FDRNo", obj.FDRNo)
            clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code)
            clsCommon.AddColumnsForChange(coll, "GL_Account_Name", obj.GL_Account_Name)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FIXED_DEPOSIT_MASTER_MT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FIXED_DEPOSIT_MASTER_MT", OMInsertOrUpdate.Update, "TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No='" + obj.Document_No + "'", trans)
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsFixedDeposit
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsFixedDeposit
        Dim obj As ClsFixedDeposit = Nothing
        Dim Arr As List(Of ClsFixedDeposit) = Nothing
        Dim qry As String = ""
        qry = "Select TSPL_FIXED_DEPOSIT_MASTER_MT.GL_Account_Name,TSPL_FIXED_DEPOSIT_MASTER_MT.Posted,TSPL_FIXED_DEPOSIT_MASTER_MT.GL_Account_Code,TSPL_FIXED_DEPOSIT_MASTER_MT.FDRNo,TSPL_FIXED_DEPOSIT_MASTER_MT.Rate,TSPL_FIXED_DEPOSIT_MASTER_MT.LCRequestNo,TSPL_FIXED_DEPOSIT_MASTER_MT.MaturityAmount,TSPL_FIXED_DEPOSIT_MASTER_MT.Due_Date,TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No,TSPL_FIXED_DEPOSIT_MASTER_MT.Document_Date,TSPL_FIXED_DEPOSIT_MASTER_MT.Bank_Code,TSPL_FIXED_DEPOSIT_MASTER_MT.Duration,TSPL_FIXED_DEPOSIT_MASTER_MT.Duration_Desp,TSPL_FIXED_DEPOSIT_MASTER_MT.Amount,TSPL_BANK_MASTER.DESCRIPTION  from TSPL_FIXED_DEPOSIT_MASTER_MT Left Outer Join TSPL_BANK_MASTER on TSPL_FIXED_DEPOSIT_MASTER_MT.Bank_Code=TSPL_BANK_MASTER.BANK_CODE where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No = (select MIN(Document_No) from TSPL_FIXED_DEPOSIT_MASTER_MT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No = (select Max(Document_No) from TSPL_FIXED_DEPOSIT_MASTER_MT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No = (select Min(Document_No) from TSPL_FIXED_DEPOSIT_MASTER_MT where Document_No>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_FIXED_DEPOSIT_MASTER_MT.Document_No = (select Max(Document_No) from TSPL_FIXED_DEPOSIT_MASTER_MT where Document_No<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsFixedDeposit()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
            obj.Duration = clsCommon.myCdbl(dt.Rows(0)("Duration"))
            obj.BankName = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Duration_Desp = clsCommon.myCstr(dt.Rows(0)("Duration_Desp"))
            obj.MaturityAmount = clsCommon.myCdbl(dt.Rows(0)("MaturityAmount"))
            obj.Rate = clsCommon.myCdbl(dt.Rows(0)("Rate"))
            obj.FDRNo = clsCommon.myCstr(dt.Rows(0)("FDRNo"))
            obj.Due_Date = clsCommon.myCDate(dt.Rows(0)("Due_Date"))
            obj.LCRequestNo = clsCommon.myCstr(dt.Rows(0)("LCRequestNo"))
            obj.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("GL_Account_Code"))
            obj.GL_Account_Name = clsCommon.myCstr(dt.Rows(0)("GL_Account_Name"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = "delete from TSPL_FIXED_DEPOSIT_MASTER_MT where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As ClsFixedDeposit = ClsFixedDeposit.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            ' CreateJournalEntry(obj.Document_No, trans)



            Dim objPH As clsPaymentHeader = ConvertFDtoPaymentEntry(obj, trans)
            objPH.SaveData1(objPH, True, trans)
            clsPaymentHeader.PostData(objPH.Payment_No, "", trans)
            Dim qry = "Update TSPL_FIXED_DEPOSIT_MASTER_MT set Posted=1, " & _
          "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
          " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Update TSPL_FIXED_DEPOSIT_MASTER_MT set AgainstPayment_No='" & objPH.Payment_No & "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function
    Private Shared Function ConvertFDtoPaymentEntry(ByVal objFD As ClsFixedDeposit, ByVal trans As SqlTransaction) As clsPaymentHeader
        Dim obj As New clsPaymentHeader()
        obj = New clsPaymentHeader()
        obj.Payment_Date = objFD.Document_Date
        obj.Bank_Code = objFD.Bank_Code
        obj.Payment_Type = "MI"
        obj.Payment_Code = "TRANSFER"
        obj.Payment_Amount = objFD.Amount
        obj.Total_Applied_Amount = objFD.Amount
        obj.PAYMENT_AMOUNT_BASE_CURRENCY = objFD.Amount
        obj.Payment_Post_Date = objFD.Document_Date
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT CURRENCY_CODE,ConvRate  FROM TSPL_LC_REQUEST_MT WHERE LCRequestNo ='" & objFD.LCRequestNo & "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.BASE_CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            obj.ConvRateOld = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
        End If



        obj.ArrTr = New List(Of clsPaymentDetail)
        Dim objTr As New clsPaymentDetail

        objTr.Payment_Line_No = "1"
        objTr.Payment_Type = "MI"
        objTr.Net_Balance = objFD.Amount
        objTr.Account_Code = objFD.GL_Account_Code
        objTr.Description = objFD.GL_Account_Name
        objTr.ConvRateOld = obj.ConvRate
        obj.ArrTr.Add(objTr)


        Return obj
    End Function
    Public Shared Sub CreateJournalEntry(ByVal strCode As String, ByVal trans As SqlTransaction)
        Try
            Dim obj As New ClsFixedDeposit
            obj = ClsFixedDeposit.GetData(strCode, NavigatorType.Current, trans)
            Dim ArryLstGLAC As ArrayList = New ArrayList()
            Dim strbankAccount As String = ""
            Dim strGLAccount As String = ""
            Dim dblTotalCost As Double = 0
            Dim UseSubAcc As String = ""


            Dim BankTypeOfBank As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Bank_type,'') AS Bank_Type From TSPL_BANK_MASTER Where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))

            UseSubAcc = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToUseSubAccount, clsFixedParameterCode.AllowToUseSubAccount, trans))
            If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                strbankAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(Sub_Account,'')  BANKACC from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))
            Else
                strbankAccount = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANKACC  from TSPL_BANK_MASTER  where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans))
            End If

            If clsCommon.myLen(strbankAccount) <= 0 Then
                If clsCommon.CompairString(UseSubAcc, "1") = CompairStringResult.Equal AndAlso clsCommon.CompairString(BankTypeOfBank, "B") = CompairStringResult.Equal Then
                    Throw New Exception("Please enter sub account for bank " + clsCommon.myCstr(obj.Bank_Code))
                Else
                    Throw New Exception("Please enter bank account for bank " + clsCommon.myCstr(obj.Bank_Code))
                End If

            End If
            Dim BankLocation As String = clsDBFuncationality.getSingleValue("select Right(BANKACC ,3)  from TSPL_BANK_MASTER where BANK_CODE ='" + clsCommon.myCstr(obj.Bank_Code) + "'", trans)
            strbankAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strbankAccount, BankLocation, trans)

            Dim Amount As Double = clsCommon.myCdbl(obj.Amount)
            Dim Acc() As String = {strbankAccount, -1 * Amount}
            ArryLstGLAC.Add(Acc)

            strGLAccount = clsCommon.myCstr(obj.GL_Account_Code)
            strGLAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strGLAccount, BankLocation, trans)


            Dim Acc1() As String = {strGLAccount, 1 * clsCommon.myCdbl(obj.Amount)}
            ArryLstGLAC.Add(Acc1)


            clsJournalMaster.FunGrnlEntryWithTrans(BankLocation, False, trans, obj.Document_Date, "Journal Entry Against Fixed Deposit for Document No " + obj.Document_No + " ", "FD-MT", "Fixed Deposit Merchant Trade", obj.Document_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "", "")


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
End Class
