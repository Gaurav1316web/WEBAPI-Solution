Imports System.Data.SqlClient
Imports common
Public Class ClsTDSPayment
#Region "variables"
    Public Document_No As String = String.Empty
    Public Document_Date As Date = Nothing
    Public Bank_Code As String = String.Empty
    Public BankName As String = String.Empty
    Public TDS_Section_Code As String = String.Empty
    Public TDS_Section_Name As String = String.Empty
    Public TDS_Deduction_Code As String = String.Empty
    Public BSR_Code As String = String.Empty
    Public From_Date As Date = Nothing
    Public To_Date As Date = Nothing
    Public Challan_No As String = String.Empty
    Public Challan_Date As Date = Nothing
    Public Posted As Double = 0
    Public Location_Code As String = String.Empty
    Public Location_Name As String = String.Empty
    Public TotalPayment As Double = 0

    Public Payment_Code As String = String.Empty
    Public Cheque_No As String = Nothing
    Public Cheque_Date As Date?
    Public PDC_Cheque As Char = "N"
    Public CHECK_PRINT As Integer = 0

    Public arrTDSPaymentDetail As List(Of ClsTDSPaymentDetail) = Nothing
    Public arrTDSPaymentDetail_TDS_ND As List(Of ClsTDSPaymentDetailTDS_ND) = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As ClsTDSPayment, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal obj As ClsTDSPayment, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "TDS", "TDS Payment", obj.Location_Code, obj.Document_Date, trans)
            qry = "delete from TSPL_TDS_PAYMENT_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_TDS_PAYMENT_TDS_ND_DETAIL where Document_No='" & obj.Document_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.TDSPayment, "", obj.Location_Code, True)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)

            clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No)
            If clsCommon.myLen(obj.Cheque_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "PDC_Cheque", obj.PDC_Cheque)
            clsCommon.AddColumnsForChange(coll, "CHECK_PRINT", obj.CHECK_PRINT)

            clsCommon.AddColumnsForChange(coll, "TDS_Section_Code", obj.TDS_Section_Code)
            clsCommon.AddColumnsForChange(coll, "TDS_Deduction_Code", obj.TDS_Deduction_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "BSR_Code", obj.BSR_Code)
            clsCommon.AddColumnsForChange(coll, "TotalPayment", obj.TotalPayment)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))



            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TDS_PAYMENT_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TDS_PAYMENT_HEADER", OMInsertOrUpdate.Update, "TSPL_TDS_PAYMENT_HEADER.Document_No='" + obj.Document_No + "'", trans)
            End If
            ClsTDSPaymentDetail.saveData(obj.arrTDSPaymentDetail, obj.Document_No, trans)
            ClsTDSPaymentDetailTDS_ND.saveData(obj.arrTDSPaymentDetail_TDS_ND, obj.Document_No, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            qry = Nothing
            obj = Nothing
        End Try
        Return True
    End Function
    Private Shared Function ConvertTDSPaymentToPaymentEntry(ByVal objTDSPayment As ClsTDSPayment, ByVal trans As SqlTransaction) As clsPaymentHeader
        Dim obj As New clsPaymentHeader()
        obj = New clsPaymentHeader()
        ' obj.Payment_No = clsCommon.myCstr(txtPaymentNo.Value)
        obj.Entry_Desc = " Payment entry against TDS Payment Document No " + clsCommon.myCstr(objTDSPayment.Document_No)
        obj.Payment_Date = clsCommon.myCDate(objTDSPayment.Document_Date)
        obj.Payment_Post_Date = clsCommon.myCDate(objTDSPayment.Document_Date)
        obj.Bank_Code = clsCommon.myCstr(objTDSPayment.Bank_Code)
        obj.Payment_Type = "MI"

        obj.Payment_Code = clsCommon.myCstr(objTDSPayment.Payment_Code)
        obj.CHECK_PRINT = objTDSPayment.CHECK_PRINT
        obj.Cheque_No = objTDSPayment.Cheque_No
        obj.Cheque_Date = objTDSPayment.Cheque_Date
        obj.PDC_Cheque = objTDSPayment.PDC_Cheque

        obj.Account_Payee = 0
        obj.Against_TDS_PAYMENT_No = clsCommon.myCstr(objTDSPayment.Document_No)
        obj.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code in (select BANKACC from TSPL_BANK_MASTER where BANK_CODE='" + clsCommon.myCstr(objTDSPayment.Bank_Code) + "')", trans))

        obj.Payment_Amount = clsCommon.myCdbl(objTDSPayment.TotalPayment)
        obj.Total_Applied_Amount = clsCommon.myCdbl(objTDSPayment.TotalPayment)
        obj.IsChkReverse = "N"
        obj.Form_ID = "PYMT-NEW"

        obj.CURRENCY_CODE = "INR"
        obj.ConvRate = 1
        obj.ConvRateOld = 1
        obj.ApplicableFrom = Nothing
        obj.PAYMENT_AMOUNT_BASE_CURRENCY = clsCommon.myCdbl(objTDSPayment.TotalPayment)

        obj.EXCHANGE_LOSS_AMT = 0
        obj.EXCHANGE_GAIN_AMT = 0
        obj.EXCHANGE_GAIN_ACCOUNT = Nothing
        obj.EXCHANGE_LOSS_ACCOUNT = Nothing


        Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select TSPL_TDS_PAYMENT_DETAIL.Document_No ,TSPL_TDS_PAYMENT_DETAIL.GL_Account ,sum(TSPL_TDS_PAYMENT_DETAIL.totaltdsamount) as amount,max(TSPL_GL_ACCOUNTS.Description) as GL_AccountDescription from TSPL_TDS_PAYMENT_DETAIL Left Outer Join TSPL_GL_ACCOUNTS on TSPL_TDS_PAYMENT_DETAIL.GL_Account =TSPL_GL_ACCOUNTS.Account_Code where TSPL_TDS_PAYMENT_DETAIL.Document_No='" & clsCommon.myCstr(objTDSPayment.Document_No) & "' group by TSPL_TDS_PAYMENT_DETAIL.GL_Account ,TSPL_TDS_PAYMENT_DETAIL.Document_No ", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim objTr As New clsPaymentDetail
            obj.ArrTr = New List(Of clsPaymentDetail)
            For I As Integer = 0 To dt.Rows.Count - 1
                objTr = New clsPaymentDetail()
                objTr.Payment_Type = "MI"
                objTr.Account_Code = clsCommon.myCstr(dt.Rows(I)("GL_Account"))
                objTr.Description = clsCommon.myCstr(dt.Rows(I)("GL_AccountDescription"))
                objTr.Net_Balance = clsCommon.myCdbl(dt.Rows(I)("amount"))
                objTr.ESI_WCT_Percentage = (-objTr.Net_Balance / objTr.Net_Balance) * 100
                obj.ArrTr.Add(objTr)
            Next
        End If


        Return obj
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As ClsTDSPayment
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsTDSPayment
        Dim obj As ClsTDSPayment = Nothing
        Dim Arr As List(Of ClsTDSPayment) = Nothing
        'Dim qry As String = "Select TSPL_TDS_PAYMENT_HEADER.Cheque_No,TSPL_TDS_PAYMENT_HEADER.Cheque_Date,TSPL_TDS_PAYMENT_HEADER.PDC_Cheque,TSPL_TDS_PAYMENT_HEADER.CHECK_PRINT,TSPL_TDS_PAYMENT_HEADER.Payment_Code,TSPL_TDS_PAYMENT_HEADER.TotalPayment,TSPL_TDS_PAYMENT_HEADER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_TDS_PAYMENT_HEADER.Posted,TSPL_TDS_PAYMENT_HEADER.Document_No,TSPL_TDS_PAYMENT_HEADER.Document_Date,TSPL_TDS_PAYMENT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as Bank_Name,TSPL_TDS_PAYMENT_HEADER.TDS_Section_Code,TSPL_TDS_SECTION_MASTER.Description as TDS_Section_Name,TSPL_TDS_PAYMENT_HEADER.TDS_Deduction_Code,TSPL_TDS_PAYMENT_HEADER.From_Date,TSPL_TDS_PAYMENT_HEADER.To_Date,TSPL_TDS_PAYMENT_HEADER.BSR_Code,TSPL_TDS_PAYMENT_HEADER.Challan_No,TSPL_TDS_PAYMENT_HEADER.Challan_Date from TSPL_TDS_PAYMENT_HEADER Left Outer join TSPL_BANK_MASTER on TSPL_TDS_PAYMENT_HEADER.Bank_Code=TSPL_BANK_MASTER.BANK_CODE left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_PAYMENT_HEADER.TDS_Section_Code=TSPL_TDS_SECTION_MASTER.TDS_Group Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_TDS_PAYMENT_HEADER.Location_Code  where 2=2  "
        Dim qry As String = "Select TSPL_TDS_PAYMENT_HEADER.Cheque_No,TSPL_TDS_PAYMENT_HEADER.Cheque_Date,TSPL_TDS_PAYMENT_HEADER.PDC_Cheque,TSPL_TDS_PAYMENT_HEADER.CHECK_PRINT,TSPL_TDS_PAYMENT_HEADER.Payment_Code,TSPL_TDS_PAYMENT_HEADER.TotalPayment,TSPL_TDS_PAYMENT_HEADER.Location_Code,TSPL_GL_SEGMENT_CODE.Description as Location_Desc ,TSPL_TDS_PAYMENT_HEADER.Posted,TSPL_TDS_PAYMENT_HEADER.Document_No,TSPL_TDS_PAYMENT_HEADER.Document_Date,TSPL_TDS_PAYMENT_HEADER.Bank_Code,TSPL_BANK_MASTER.DESCRIPTION as Bank_Name,TSPL_TDS_PAYMENT_HEADER.TDS_Section_Code,TSPL_TDS_SECTION_MASTER.Description as TDS_Section_Name,TSPL_TDS_PAYMENT_HEADER.TDS_Deduction_Code,TSPL_TDS_PAYMENT_HEADER.From_Date,TSPL_TDS_PAYMENT_HEADER.To_Date,TSPL_TDS_PAYMENT_HEADER.BSR_Code,TSPL_TDS_PAYMENT_HEADER.Challan_No,TSPL_TDS_PAYMENT_HEADER.Challan_Date from TSPL_TDS_PAYMENT_HEADER Left Outer join TSPL_BANK_MASTER on TSPL_TDS_PAYMENT_HEADER.Bank_Code=TSPL_BANK_MASTER.BANK_CODE left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_PAYMENT_HEADER.TDS_Section_Code=TSPL_TDS_SECTION_MASTER.TDS_Group Left Outer Join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_TDS_PAYMENT_HEADER.Location_Code  where 2=2  "
        'If clsCommon.myLen(arrLoc) > 0 Then
        '    qry += " and TSPL_SECONDARY_SETTING_QC_HEAD.Location_Code in (" + arrLoc + ") "
        'End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_TDS_PAYMENT_HEADER.Document_No = (select MIN(Document_No) from TSPL_TDS_PAYMENT_HEADER WHERE 1=1 " + whrclas + "  )"
            Case NavigatorType.Last
                qry += " and TSPL_TDS_PAYMENT_HEADER.Document_No = (select Max(Document_No) from TSPL_TDS_PAYMENT_HEADER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_TDS_PAYMENT_HEADER.Document_No = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_TDS_PAYMENT_HEADER.Document_No = (select Min(Document_No) from TSPL_TDS_PAYMENT_HEADER where Document_No>'" + strCode + "' " + whrclas + "  )"
            Case NavigatorType.Previous
                qry += " and TSPL_TDS_PAYMENT_HEADER.Document_No = (select Max(Document_No) from TSPL_TDS_PAYMENT_HEADER where Document_No<'" + strCode + "' " + whrclas + "  )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj = New ClsTDSPayment()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))

            obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            obj.CHECK_PRINT = clsCommon.myCdbl(dt.Rows(0)("CHECK_PRINT"))
            obj.Cheque_No = clsCommon.myCstr(dt.Rows(0)("Cheque_No"))
            If clsCommon.myLen(dt.Rows(0)("Cheque_Date")) > 0 Then
                obj.Cheque_Date = clsCommon.myCstr(dt.Rows(0)("Cheque_Date"))
            End If
            obj.PDC_Cheque = clsCommon.myCstr(dt.Rows(0)("PDC_Cheque"))


            obj.BankName = clsCommon.myCstr(dt.Rows(0)("Bank_Name"))
            obj.TDS_Section_Code = clsCommon.myCstr(dt.Rows(0)("TDS_Section_Code"))
            obj.TDS_Section_Name = clsCommon.myCstr(dt.Rows(0)("TDS_Section_Name"))
            obj.TDS_Deduction_Code = clsCommon.myCstr(dt.Rows(0)("TDS_Deduction_Code"))
            obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.BSR_Code = clsCommon.myCstr(dt.Rows(0)("BSR_Code"))
            obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
            obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))

            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.TotalPayment = clsCommon.myCdbl(dt.Rows(0)("TotalPayment"))
            obj.arrTDSPaymentDetail = ClsTDSPaymentDetail.getData(obj.Document_No, trans)
            obj.arrTDSPaymentDetail_TDS_ND = ClsTDSPaymentDetailTDS_ND.getData(obj.Document_No, trans)

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
            Dim qry As String = ""

            qry = "delete from TSPL_TDS_PAYMENT_DETAIL where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_TDS_PAYMENT_TDS_ND_DETAIL where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_TDS_PAYMENT_HEADER where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, arrLoc, strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal arrLoc As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("QC No not found to Post")
            End If
            Dim obj As ClsTDSPayment = ClsTDSPayment.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim objPY As clsPaymentHeader = ConvertTDSPaymentToPaymentEntry(obj, trans)
            objPY.SaveData1(objPY, True, trans)
            clsPaymentHeader.PostData(objPY.Payment_No, "MPayable", trans)

            Dim qry = "Update TSPL_TDS_PAYMENT_HEADER set Posted=1, " & _
            " Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' " & _
            " where Document_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class ClsTDSPaymentDetail
    Public Document_No As String = String.Empty
    Public Document_type As String = String.Empty
    Public TDS_Deduction_Code As String = String.Empty
    Public Vendor_Code As String = String.Empty
    Public Against_Document_No As String = String.Empty
    Public Against_Document_Date As Date = Nothing
    Public PAN As String = String.Empty
    Public Vendor_Name As String = String.Empty
    Public TDS_Deduction_Name As String = String.Empty
    Public Taxable_Amount As Double = 0
    Public Document_amount As Double = 0
    Public Income_tax As Double = 0
    Public TotalTdsAmount As Double = 0
    Public RateForTdsDeduction As Double = 0
    Public Location_Code As String = String.Empty
    Public Location_Name As String = String.Empty
    Public Deduct_Code As String = String.Empty
    Public Description As String = String.Empty
    Public GL_Account As String = String.Empty

    Public Shared Function saveData(ByVal arrObj As List(Of ClsTDSPaymentDetail), ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As ClsTDSPaymentDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Document_type", obj.Document_type)
                    clsCommon.AddColumnsForChange(coll, "TDS_Deduction_Code", obj.TDS_Deduction_Code)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                    clsCommon.AddColumnsForChange(coll, "Against_Document_No", obj.Against_Document_No)
                    clsCommon.AddColumnsForChange(coll, "Against_Document_Date", clsCommon.GetPrintDate(obj.Against_Document_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "PAN", obj.PAN)
                    clsCommon.AddColumnsForChange(coll, "Taxable_Amount", obj.Taxable_Amount)
                    clsCommon.AddColumnsForChange(coll, "Document_amount", obj.Document_amount)
                    clsCommon.AddColumnsForChange(coll, "Income_tax", obj.Income_tax)
                    clsCommon.AddColumnsForChange(coll, "TotalTdsAmount", obj.TotalTdsAmount)
                    clsCommon.AddColumnsForChange(coll, "RateForTdsDeduction", obj.RateForTdsDeduction)
                    clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                    clsCommon.AddColumnsForChange(coll, "Deduct_Code", obj.Deduct_Code)
                    clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                    clsCommon.AddColumnsForChange(coll, "GL_Account", obj.GL_Account)
                   
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TDS_PAYMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
            arrObj = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal strDocument_No As String, ByVal trans As SqlTransaction) As List(Of ClsTDSPaymentDetail)
        Try
            Dim arrObj As List(Of ClsTDSPaymentDetail) = Nothing
            Dim obj As ClsTDSPaymentDetail = Nothing
            Dim qry As String = "Select TSPL_TDS_PAYMENT_DETAIL.Document_No,TSPL_TDS_PAYMENT_DETAIL.Document_type,TSPL_TDS_PAYMENT_DETAIL.TDS_Deduction_Code,TSPL_TDS_DEDUCTION_HEAD.Description as TDS_Deduction_Name ,TSPL_TDS_PAYMENT_DETAIL.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name ,TSPL_TDS_PAYMENT_DETAIL.Against_Document_No,TSPL_TDS_PAYMENT_DETAIL.Against_Document_Date,TSPL_TDS_PAYMENT_DETAIL.PAN,TSPL_TDS_PAYMENT_DETAIL.Taxable_Amount,TSPL_TDS_PAYMENT_DETAIL.Document_amount,TSPL_TDS_PAYMENT_DETAIL.Income_tax,TSPL_TDS_PAYMENT_DETAIL.TotalTdsAmount,TSPL_TDS_PAYMENT_DETAIL.RateForTdsDeduction,TSPL_TDS_PAYMENT_DETAIL.Location_Code,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_TDS_PAYMENT_DETAIL.Deduct_Code,TSPL_TDS_PAYMENT_DETAIL.Description,TSPL_TDS_PAYMENT_DETAIL.GL_Account from TSPL_TDS_PAYMENT_DETAIL Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_TDS_PAYMENT_DETAIL.Location_Code Left outer Join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_TDS_PAYMENT_DETAIL.Vendor_Code left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_PAYMENT_DETAIL.TDS_Deduction_Code= TSPL_TDS_DEDUCTION_HEAD.Deduction_Code  where TSPL_TDS_PAYMENT_DETAIL.Document_No='" & strDocument_No & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsTDSPaymentDetail)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsTDSPaymentDetail()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))

                    obj.Document_type = clsCommon.myCstr(dt.Rows(i)("Document_type"))
                    obj.TDS_Deduction_Code = clsCommon.myCstr(dt.Rows(i)("TDS_Deduction_Code"))
                    obj.TDS_Deduction_Name = clsCommon.myCstr(dt.Rows(i)("TDS_Deduction_Name"))
                    obj.Vendor_Code = clsCommon.myCstr(dt.Rows(i)("Vendor_Code"))
                    obj.Vendor_Name = clsCommon.myCstr(dt.Rows(i)("Vendor_Name"))
                    obj.Against_Document_No = clsCommon.myCstr(dt.Rows(i)("Against_Document_No"))
                    obj.Against_Document_Date = clsCommon.myCDate(dt.Rows(i)("Against_Document_Date"))
                    obj.PAN = clsCommon.myCstr(dt.Rows(i)("PAN"))
                    obj.Location_Code = clsCommon.myCstr(dt.Rows(i)("Location_Code"))
                    obj.Location_Name = clsCommon.myCstr(dt.Rows(i)("Location_Desc"))
                    obj.Description = clsCommon.myCstr(dt.Rows(i)("Description"))
                    obj.GL_Account = clsCommon.myCstr(dt.Rows(i)("GL_Account"))
                    obj.Document_amount = clsCommon.myCdbl(dt.Rows(i)("Document_amount"))
                    obj.Taxable_Amount = clsCommon.myCdbl(dt.Rows(i)("Taxable_Amount"))
                    obj.Income_tax = clsCommon.myCdbl(dt.Rows(i)("Income_tax"))
                    obj.TotalTdsAmount = clsCommon.myCdbl(dt.Rows(i)("TotalTdsAmount"))
                    obj.RateForTdsDeduction = clsCommon.myCdbl(dt.Rows(i)("RateForTdsDeduction"))
                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class


Public Class ClsTDSPaymentDetailTDS_ND
    Public Document_No As String = String.Empty
    Public TDS_Deduction_Code As String = String.Empty

    Public Shared Function saveData(ByVal arrObj As List(Of ClsTDSPaymentDetailTDS_ND), ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then

                For Each obj As ClsTDSPaymentDetailTDS_ND In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "TDS_Deduction_Code", obj.TDS_Deduction_Code)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TDS_Payment_TDS_ND_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
            arrObj = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal strDocument_No As String, ByVal trans As SqlTransaction) As List(Of ClsTDSPaymentDetailTDS_ND)
        Try
            Dim arrObj As List(Of ClsTDSPaymentDetailTDS_ND) = Nothing
            Dim obj As ClsTDSPaymentDetailTDS_ND = Nothing
            Dim qry As String = "Select TSPL_TDS_Payment_TDS_ND_Detail.Document_No,TSPL_TDS_Payment_TDS_ND_Detail.TDS_Deduction_Code from TSPL_TDS_Payment_TDS_ND_Detail  where TSPL_TDS_Payment_TDS_ND_Detail.Document_No='" & strDocument_No & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of ClsTDSPaymentDetailTDS_ND)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New ClsTDSPaymentDetailTDS_ND()
                    obj.Document_No = clsCommon.myCstr(dt.Rows(i)("Document_No"))
                    obj.TDS_Deduction_Code = clsCommon.myCstr(dt.Rows(i)("TDS_Deduction_Code"))

                    arrObj.Add(obj)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class