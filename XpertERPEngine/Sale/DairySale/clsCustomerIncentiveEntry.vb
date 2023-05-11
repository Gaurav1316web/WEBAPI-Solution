Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

Public Class clsCustomerIncentiveEntryHead
#Region "Variables"
    Public Doc_Code As String = Nothing
    Public Doc_Date As DateTime
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public Filter_Month As Date
    Public Filter_Month_From As Date? = Nothing
    Public Filter_Month_To As Date? = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending

    Public Additional_Security As Decimal
    Public Security_Part As Decimal

    Public arrCustomer As ArrayList = Nothing
    Public arr As List(Of clsCustomerIncentiveEntryDetail) = Nothing
    Public arrCustIncentive As List(Of clsCustomerIncentiveEntryCustomerIncentiveWise) = Nothing
    Public arrCustStru As List(Of clsCustomerIncentiveEntryStrucreCustomerWise) = Nothing
    Public arrCustItem As List(Of clsCustomerIncentiveEntryItemCustomerWise) = Nothing
    Public arrInvoice As List(Of clsCustomerIncentiveEntryInvoiceWise) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsCustomerIncentiveEntryHead, ByVal isNewEntry As Boolean) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, tran, "")
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsCustomerIncentiveEntryHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal strVoucherNoRecreateOnly As String) As Boolean
        If clsCommon.myLen(obj.Location_Code) <= 0 Then
            Throw New Exception("Please first select Location")
        End If
        Dim qry As String = "delete from TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_CUSTOMER_INCENTIVE_DETAIL where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE where Doc_Code='" + obj.Doc_Code + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        If obj.arr.Count <= 0 Then
            Throw New Exception("Please fill at one invoice details")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
        clsCommon.AddColumnsForChange(coll, "Filter_Month", clsCommon.GetPrintDate(obj.Filter_Month, "dd/MMM/yyyy"))

        clsCommon.AddColumnsForChange(coll, "Additional_Security", obj.Additional_Security)
        clsCommon.AddColumnsForChange(coll, "Security_Part", obj.Security_Part)

        If obj.Filter_Month_From Is Nothing Then
            clsCommon.AddColumnsForChange(coll, "Filter_Month_From", Nothing, True)
        Else
            clsCommon.AddColumnsForChange(coll, "Filter_Month_From", clsCommon.GetPrintDate(obj.Filter_Month_From, "dd/MMM/yyyy"))
        End If
        If obj.Filter_Month_To Is Nothing Then
            clsCommon.AddColumnsForChange(coll, "Filter_Month_To", Nothing, True)
        Else
            clsCommon.AddColumnsForChange(coll, "Filter_Month_To", clsCommon.GetPrintDate(obj.Filter_Month_To, "dd/MMM/yyyy"))
        End If
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        If isNewEntry Then
            obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.CustomerIncentiveEntry, clsDocTransactionType.Transaction, obj.Location_Code)
            If (clsCommon.myLen(obj.Doc_Date) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            clsCommon.AddColumnsForChange(coll, "Doc_Code", obj.Doc_Code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD", OMInsertOrUpdate.Insert, "", trans)
        Else
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD", OMInsertOrUpdate.Update, "Doc_Code='" + obj.Doc_Code + "'", trans)
        End If
        clsCustomerIncentiveEntryDetail.SaveData(obj.Doc_Code, obj.Doc_Date, obj.arr, trans)
        clsCustomerIncentiveEntryInvoiceWise.SaveData(obj.Doc_Code, obj.Doc_Date, obj.arrInvoice, trans)
        clsCustomerIncentiveEntryCustomerIncentiveWise.SaveData(obj.Doc_Code, obj.Doc_Date, obj.arrCustIncentive, trans)
        clsCustomerIncentiveEntryStrucreCustomerWise.SaveData(obj.Doc_Code, obj.Doc_Date, obj.arrCustStru, trans)
        clsCustomerIncentiveEntryItemCustomerWise.SaveData(obj.Doc_Code, obj.Doc_Date, obj.arrCustItem, trans)
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomerIncentiveEntryHead
        Dim obj As clsCustomerIncentiveEntryHead = Nothing
        Dim qry As String = "SELECT TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.*,TSPL_LOCATION_MASTER.Location_Desc as Location_Name from TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Location_Code  where 2=2"
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = (select MIN(Doc_Code) from TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = (select Max(Doc_Code) from TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = (select Min(Doc_Code) from TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD where Doc_Code>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = (select Max(Doc_Code) from TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD where Doc_Code<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD.Doc_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCustomerIncentiveEntryHead()
            obj.Doc_Code = clsCommon.myCstr(dt.Rows(0)("Doc_Code"))
            obj.Doc_Date = clsCommon.myCDate(dt.Rows(0)("Doc_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            obj.Filter_Month = clsCommon.myCDate(dt.Rows(0)("Filter_Month"))

            obj.Additional_Security = clsCommon.myCdbl(dt.Rows(0)("Additional_Security"))
            obj.Security_Part = clsCommon.myCdbl(dt.Rows(0)("Security_Part"))

            If dt.Rows(0)("Filter_Month_From") IsNot DBNull.Value Then
                obj.Filter_Month_From = clsCommon.myCDate(dt.Rows(0)("Filter_Month_From"))
            End If
            If dt.Rows(0)("Filter_Month_To") IsNot DBNull.Value Then
                obj.Filter_Month_To = clsCommon.myCDate(dt.Rows(0)("Filter_Month_To"))
            End If
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Posted, ERPTransactionStatus.Pending)
            obj.arr = clsCustomerIncentiveEntryDetail.GetData(obj.Doc_Code, trans, obj.arrCustomer)
            obj.arrInvoice = clsCustomerIncentiveEntryInvoiceWise.GetData(obj.Doc_Code, trans)
            obj.arrCustItem = clsCustomerIncentiveEntryItemCustomerWise.GetData(obj.Doc_Code, trans)
            obj.arrCustStru = clsCustomerIncentiveEntryStrucreCustomerWise.GetData(obj.Doc_Code, trans)
            obj.arrCustIncentive = clsCustomerIncentiveEntryCustomerIncentiveWise.GetData(obj.Doc_Code, trans)
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        
        If clsCommon.myLen(strDocNo) <= 0 Then
            Throw New Exception("Please provide document no to post")
        End If

        Dim obj As clsCustomerIncentiveEntryHead = clsCustomerIncentiveEntryHead.GetData(strDocNo, NavigatorType.Current, trans)
        If obj.Posted = ERPTransactionStatus.Posted Then
            Throw New Exception("Already posted transaction - " + obj.Doc_Code)
        End If
        If Not (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DayWiseCustomerIncentiveCalculation, clsFixedParameterCode.DayWiseCustomerIncentiveCalculation, trans)) = 1) Then
            For Each objtr As clsCustomerIncentiveEntryDetail In obj.arr
                CreateCreditNote(obj, objtr, trans)
            Next
        End If

        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CustomerIncetiveAutoSecuity, clsFixedParameterCode.CustomerIncetiveAutoSecuity, trans)) = 1) Then
            For Each objtr As clsCustomerIncentiveEntryDetail In obj.arr
                CreateReceiptEntry(obj, objtr, trans)
            Next
        End If
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Posted", 1)
        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD", OMInsertOrUpdate.Update, "Doc_Code='" + strDocNo + "'", trans)
        Return True
    End Function

    Shared Function CreateReceiptEntry(ByVal obj As clsCustomerIncentiveEntryHead, ByVal objtr As clsCustomerIncentiveEntryDetail, ByVal trans As SqlTransaction) As Boolean
        If objtr.Security_To_Be_Taken > 0 Then
            Dim objREC As New clsRcptEntryHeader()
            objREC.memorndmamt = "0"
            objREC.Entry_Desc = "AD Security deposit of Vendor Marging [" + obj.Doc_Code + "] "
            objREC.Receipt_Date = obj.Doc_Date
            objREC.Receipt_Post_Date = obj.Doc_Date
            objREC.SecurityDeposit = "Y"
            objREC.SecurityDepositType = "S"
            objREC.Bank_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CustomerIncetiveBankForSecuity, clsFixedParameterCode.CustomerIncetiveBankForSecuity, trans))
            objREC.Reference = objtr.TR_Code
            objREC.Narration = clsUserMgtCode.CustomerIncentiveEntry
            If clsCommon.myLen(objREC.Bank_Code) <= 0 Then
                Throw New Exception("Please set [" + clsFixedParameterType.CustomerIncetiveBankForSecuity + "] in fixed parameter")
            End If
            objREC.Bank_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bank_Code from TSPL_Bank_Master  where Bank_Code='" & objREC.Bank_Code & "'", trans))
            If clsCommon.myLen(objREC.Bank_Code) <= 0 Then
                Throw New Exception("Fixed Parameter [" + clsFixedParameterType.CustomerIncetiveBankForSecuity + "] Invalid Bank")
            End If

            objREC.Payment_Code = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CustomerIncetivePaymentModeForSecuity, clsFixedParameterCode.CustomerIncetivePaymentModeForSecuity, trans))
            If clsCommon.myLen(objREC.Payment_Code) <= 0 Then
                Throw New Exception("Please set [" + clsFixedParameterType.CustomerIncetivePaymentModeForSecuity + "] in fixed parameter")
            End If
            objREC.Payment_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payment_Code from TSPL_PAYMENT_CODE where Payment_Code='" & objREC.Payment_Code & "'", trans))
            If clsCommon.myLen(objREC.Payment_Code) <= 0 Then
                Throw New Exception("Fixed Parameter [" + clsFixedParameterType.CustomerIncetivePaymentModeForSecuity + "] Invalid Payment Mode")
            End If

            objREC.Cust_Code = objtr.Cust_Code
            objREC.Customer_Name = objtr.Cust_Name
            ' objREC.Location_GL_Code = clsCommon.myCstr(dr("Location_Code"))
            objREC.Location_GL_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select distinct(Segment_code) as Code  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code where Seg_No = '7' AND GIT='N' and  TSPL_LOCATION_MASTER.Location_Code='" & obj.Location_Code & "' ", trans))
            objREC.CURRENCY_CODE = "INR"
            'objREC.BASE_CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("BASE_CURRENCY_CODE"))
            ''objREC.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            'objREC.ConvRate = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select case when isnull(ConvRateRevaluation,0)<>0 then ConvRateRevaluation else ConvRate end as ConvRate from ( Select isnull(TSPL_RECEIPT_HEADER.ConvRate,1) as ConvRate,isnull( ( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate  from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.receipt_no=TSPL_RECEIPT_HEADER.Receipt_No and TSPL_REVALUATION_HEAD.Status=1 and  TSPL_REVALUATION_HEAD.Document_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(objREC.Receipt_Date), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation from TSPL_RECEIPT_HEADER where Receipt_No ='" & txtDocumentNo.Value & "' )xx", Nothing))
            'objREC.ConvRateOld = clsCommon.myCdbl(dt.Rows(0)("ConvRateOld"))

            objREC.ApplicableFrom = Nothing
            objREC.Receipt_Type = "P"
            objREC.IsSalesmanType = "N"
            objREC.CFormRecd = "N"
            objREC.CHECK_PRINT = 0
            objREC.IsApplyDocAuto = 1
            objREC.Receipt_Amount = objtr.Security_To_Be_Taken
            objREC.UnApply_Amt = objtr.Security_To_Be_Taken
            objREC.Balance_Amt = objtr.Security_To_Be_Taken
            objREC.RECEIVED_AMOUNT_BASE_CURRENCY = objREC.Receipt_Amount

            objREC.SaveData(objREC, True, trans)
            clsRcptEntryHeader.funRcptPost(objREC.Receipt_No, trans)
        End If
        Return True
    End Function
    Shared Function CreateCreditNote(ByVal obj As clsCustomerIncentiveEntryHead, ByVal objtr As clsCustomerIncentiveEntryDetail, ByVal trans As SqlTransaction) As Boolean
        Dim objCustInv As New clsCustomerInvoiceHead()
        ''objCustInv.Document_No ''Will be Generateed
        objCustInv.Document_Date = obj.Doc_Date
        objCustInv.Document_Type = "C"
        objCustInv.RoundOffAmount = Math.Round(Math.Round(clsCommon.myCdbl(objtr.Incentive_Amount), 0) - clsCommon.myCdbl(objtr.Incentive_Amount), 2) ''ERO/07/11/19-001091 by balwinder on 21/11/2019
        objCustInv.Document_Total = Math.Round(clsCommon.myCdbl(objtr.Incentive_Amount), 0)
        objCustInv.Customer_Code = objtr.Cust_Code
        objCustInv.Customer_Name = objtr.Cust_Name
        objCustInv.Posting_Date = obj.Doc_Date
        Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + objtr.Cust_Code + "'"
        objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
        ''objCustInv.Order_No
        objCustInv.loc_code = clsLocation.GetSegmentCode(obj.Location_Code, trans)
        objCustInv.On_Hold = 0
        objCustInv.Remarks = obj.Doc_Code
        objCustInv.Description = "Auto Customer Incentive Entry Code " + obj.Doc_Code
        'objCustInv.Tax_Group = ""
        'objCustInv.TAX1 = ""
        'objCustInv.TAX1_Rate = 0
        'objCustInv.TAX1_Amt = 0
        'objCustInv.TAX2 = 0
        'objCustInv.TAX2_Rate = 0
        'objCustInv.TAX2_Amt = 0
        'objCustInv.TAX3 = ""
        'objCustInv.TAX3_Rate = 0
        'objCustInv.TAX3_Amt = 0
        'objCustInv.TAX4 = ""
        'objCustInv.TAX4_Rate = 0
        'objCustInv.TAX4_Amt = 0
        'objCustInv.TAX5 = ""
        'objCustInv.TAX5_Rate = 0
        'objCustInv.TAX5_Amt = 0
        'objCustInv.TAX6 = ""
        'objCustInv.TAX6_Rate = 0
        'objCustInv.TAX6_Amt = 0
        'objCustInv.TAX7 = ""
        'objCustInv.TAX7_Rate = ""
        'objCustInv.TAX7_Amt = 0
        'objCustInv.TAX8 = ""
        'objCustInv.TAX8_Rate = 0
        'objCustInv.TAX8_Amt = 0
        'objCustInv.TAX9 = ""
        'objCustInv.TAX9_Rate = 0
        'objCustInv.TAX9_Amt = 0
        'objCustInv.TAX10 = ""
        'objCustInv.TAX10_Rate = 0
        'objCustInv.TAX10_Amt = 0
        'objCustInv.Total_Tax = 0
        'objCustInv.Tax1_BAmount = 0
        'objCustInv.Tax2_BAmount = 0
        'objCustInv.Tax3_BAmount = 0
        'objCustInv.Tax4_BAmount = 0
        'objCustInv.Tax5_BAmount = 0
        'objCustInv.Tax6_BAmount = 0
        'objCustInv.Tax7_BAmount = 0
        'objCustInv.Tax8_BAmount = 0
        'objCustInv.Tax9_BAmount = 0
        'objCustInv.Tax10_BAmount = 0
        'objCustInv.Balance_Amt = objtr.Incentive_Amount
        objCustInv.Terms_Code = ""
        objCustInv.PROJECT_ID = ""

        '' currency details
        objCustInv.CURRENCY_CODE = ""
        objCustInv.ConvRate = 1
        'objCustInv.ApplicableFrom = obj.ApplicableFrom
        objCustInv.RefDocType = "CUS_INC_ENT"
        'objCustInv.Trans_Type = "MT"
        objCustInv.RefDocNo = objtr.TR_Code
        ''------------------------

        'qry = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + obj.Terms_Code + "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
        '    objCustInv.Terms_Description = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
        '    objCustInv.Due_Date = obj.Document_Date.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
        'End If

        objCustInv.Discount_Percentage = 0
        objCustInv.Discount_Base = objtr.Incentive_Amount
        objCustInv.Discount_Amount = 0
        objCustInv.Amount_Less_Discount = objtr.Incentive_Amount
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
        End If

        'If obj.TAX1_Amt > 0 AndAlso clsCommon.myLen(obj.TAX1) > 0 Then
        '    objCustInv.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
        '    objCustInv.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX1_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX2_Amt > 0 AndAlso clsCommon.myLen(obj.TAX2) > 0 Then
        '    objCustInv.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
        '    objCustInv.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX2_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX3_Amt > 0 AndAlso clsCommon.myLen(obj.TAX3) > 0 Then
        '    objCustInv.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
        '    objCustInv.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX3_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX4_Amt > 0 AndAlso clsCommon.myLen(obj.TAX4) > 0 Then
        '    objCustInv.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
        '    objCustInv.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX4_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX5_Amt > 0 AndAlso clsCommon.myLen(obj.TAX5) > 0 Then
        '    objCustInv.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
        '    objCustInv.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX5_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX6_Amt > 0 AndAlso clsCommon.myLen(obj.TAX6) > 0 Then
        '    objCustInv.TAX6_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
        '    objCustInv.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX6_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX7_Amt > 0 AndAlso clsCommon.myLen(obj.TAX7) > 0 Then
        '    objCustInv.TAX7_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
        '    objCustInv.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX7_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX8_Amt > 0 AndAlso clsCommon.myLen(obj.TAX8) > 0 Then
        '    objCustInv.TAX8_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
        '    objCustInv.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX8_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX9_Amt > 0 AndAlso clsCommon.myLen(obj.TAX9) > 0 Then
        '    objCustInv.TAX9_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
        '    objCustInv.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX9_GLAC, obj.Bill_To_Location, trans)
        'End If
        'If obj.TAX10_Amt > 0 AndAlso clsCommon.myLen(obj.TAX10) > 0 Then
        '    objCustInv.TAX10_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
        '    objCustInv.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.TAX10_GLAC, obj.Bill_To_Location, trans)
        'End If

        ''objCustInv.RefDocType=
        ''objCustInv.RefDocNo
        'objCustInv.Add_Charge_Code1 = obj.Add_Charge_Code1
        'objCustInv.Add_Charge_Name1 = obj.Add_Charge_Name1
        'objCustInv.Add_Charge_Amt1 = obj.Add_Charge_Amt1
        'objCustInv.Add_Charge_Code2 = obj.Add_Charge_Code2
        'objCustInv.Add_Charge_Name2 = obj.Add_Charge_Name2
        'objCustInv.Add_Charge_Amt2 = obj.Add_Charge_Amt2
        'objCustInv.Add_Charge_Code3 = obj.Add_Charge_Code3
        'objCustInv.Add_Charge_Name3 = obj.Add_Charge_Name3
        'objCustInv.Add_Charge_Amt3 = obj.Add_Charge_Amt3
        'objCustInv.Add_Charge_Code4 = obj.Add_Charge_Code4
        'objCustInv.Add_Charge_Name4 = obj.Add_Charge_Name4
        'objCustInv.Add_Charge_Amt4 = obj.Add_Charge_Amt4
        'objCustInv.Add_Charge_Code5 = obj.Add_Charge_Code5
        'objCustInv.Add_Charge_Name5 = obj.Add_Charge_Name5
        'objCustInv.Add_Charge_Amt5 = obj.Add_Charge_Amt5
        'objCustInv.Add_Charge_Code6 = obj.Add_Charge_Code6
        'objCustInv.Add_Charge_Name6 = obj.Add_Charge_Name6
        'objCustInv.Add_Charge_Amt6 = obj.Add_Charge_Amt6
        'objCustInv.Add_Charge_Code7 = obj.Add_Charge_Code7
        'objCustInv.Add_Charge_Name7 = obj.Add_Charge_Name7
        'objCustInv.Add_Charge_Amt7 = obj.Add_Charge_Amt7
        'objCustInv.Add_Charge_Code8 = obj.Add_Charge_Code8
        'objCustInv.Add_Charge_Name8 = obj.Add_Charge_Name8
        'objCustInv.Add_Charge_Amt8 = obj.Add_Charge_Amt8
        'objCustInv.Add_Charge_Code9 = obj.Add_Charge_Code9
        'objCustInv.Add_Charge_Name9 = obj.Add_Charge_Name9
        'objCustInv.Add_Charge_Amt9 = obj.Add_Charge_Amt9
        'objCustInv.Add_Charge_Code10 = obj.Add_Charge_Code10
        'objCustInv.Add_Charge_Name10 = obj.Add_Charge_Name10
        'objCustInv.Add_Charge_Amt10 = obj.Add_Charge_Amt10
        'objCustInv.Total_Add_Charge = obj.Total_Add_Charge
        objCustInv.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
        ''objCustInv.Status
        ''objCustInv.AgainstScrap
        'objCustInv.Against_Sale_No = obj.Document_Code

        Dim counter As Integer = 1
        objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)
        For ii As Integer = 0 To obj.arrCustIncentive.Count - 1
            If clsCommon.CompairString(obj.arrCustIncentive(ii).Cust_Code, objtr.Cust_Code) = CompairStringResult.Equal Then
                Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
                objCustInvTR.SNo = counter
                objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GL_Code from TSPL_SALES_INCENTIVE_HEADER where INCENTIVE_CODE='" + obj.arrCustIncentive(ii).Incentive_Code + "'", trans))
                If clsCommon.myLen(objCustInvTR.GL_Account_Code) <= 0 Then
                    Throw New Exception("Please set GL Account For Incetive Code[" + obj.arrCustIncentive(ii).Incentive_Code + "]")
                End If

                objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Location_Code, trans)
                objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)

                objCustInvTR.Amount = obj.arrCustIncentive(ii).Incentive_Amount
                objCustInvTR.Discount_Per = 0
                objCustInvTR.Discount = 0
                objCustInvTR.Amount_less_Discount = obj.arrCustIncentive(ii).Incentive_Amount
                'objCustInvTR.TAX1 = objTr.TAX1
                'objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
                'objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
                'objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
                'objCustInvTR.TAX2 = objTr.TAX2
                'objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
                'objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
                'objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
                'objCustInvTR.TAX3 = objTr.TAX3
                'objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
                'objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
                'objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
                'objCustInvTR.TAX4 = objTr.TAX4
                'objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
                'objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
                'objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
                'objCustInvTR.TAX5 = objTr.TAX5
                'objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
                'objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
                'objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
                'objCustInvTR.TAX6 = objTr.TAX6
                'objCustInvTR.TAX6_Rate = objTr.TAX6_Rate
                'objCustInvTR.TAX6_Amt = objTr.TAX6_Amt
                'objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
                'objCustInvTR.TAX7 = objTr.TAX7
                'objCustInvTR.TAX7_Rate = objTr.TAX7_Rate
                'objCustInvTR.TAX7_Amt = objTr.TAX7_Amt
                'objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
                'objCustInvTR.TAX8 = objTr.TAX8
                'objCustInvTR.TAX8_Rate = objTr.TAX8_Rate
                'objCustInvTR.TAX8_Amt = objTr.TAX8_Amt
                'objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
                'objCustInvTR.TAX9 = objTr.TAX9
                'objCustInvTR.TAX9_Rate = objTr.TAX9_Rate
                'objCustInvTR.TAX9_Amt = objTr.TAX9_Amt
                'objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
                'objCustInvTR.TAX10 = objTr.TAX10
                'objCustInvTR.TAX10_Rate = objTr.TAX10_Rate
                'objCustInvTR.TAX10_Amt = objTr.TAX10_Amt
                'objCustInvTR.TAX10_Base_Amt = objTr.TAX10_Base_Amt
                'objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
                objCustInvTR.Total_Amount = obj.arrCustIncentive(ii).Incentive_Amount
                'objCustInvTR.Remarks = objTr.Remarks
                'objCustInvTR.TAX1_Base_Amt = objTr.TAX1_Base_Amt
                'objCustInvTR.TAX2_Base_Amt = objTr.TAX2_Base_Amt
                'objCustInvTR.TAX3_Base_Amt = objTr.TAX3_Base_Amt
                'objCustInvTR.TAX4_Base_Amt = objTr.TAX4_Base_Amt
                'objCustInvTR.TAX5_Base_Amt = objTr.TAX5_Base_Amt
                'objCustInvTR.TAX6_Base_Amt = objTr.TAX6_Base_Amt
                'objCustInvTR.TAX7_Base_Amt = objTr.TAX7_Base_Amt
                'objCustInvTR.TAX8_Base_Amt = objTr.TAX8_Base_Amt
                'objCustInvTR.TAX9_Base_Amt = objTr.TAX9_Base_Amt
                'objCustInvTR.TAX10_Base_Amt = objTr.TAX10_Base_Amt
                'objCustInvTR.Comments=objTr.Comments
                objCustInv.Arr.Add(objCustInvTR)
                counter += 1
            End If
        Next


        

        objCustInv.SaveData(objCustInv, True, trans, "")
        clsCustomerInvoiceHead.PostData("", objCustInv.Document_No, "", trans)

        Return True
    End Function
    Public Shared Function DeleteData(ByVal StrDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(StrDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsCustomerIncentiveEntryHead = clsCustomerIncentiveEntryHead.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
            Try
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Order", obj.Bill_To_Location, obj.PurchaseOrder_Date, trans)
                If (obj.Posted = ERPTransactionStatus.Posted) Then
                    Throw New Exception("Already Posted")
                End If
                Dim qry As String = "delete from TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE where Doc_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE where Doc_Code='" + obj.Doc_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_CUSTOMER_INCENTIVE_DETAIL where Doc_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE where Doc_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD where Doc_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                'trans.Commit()
            Catch ex As Exception
                'trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function ReverseAndUnpostData(ByVal StrDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpostData(StrDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpostData(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception("Doc No not found to Post")
            End If
            Dim obj As clsCustomerIncentiveEntryHead = clsCustomerIncentiveEntryHead.GetData(StrDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Posted = ERPTransactionStatus.Pending) Then
                Throw New Exception("Transacation should be posted for reverse and unposted")
            End If

            Dim qry As String = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AR-CR' and Source_Doc_No in  ( select TSPL_Customer_Invoice_Head.Document_No from TSPL_Customer_Invoice_Head where RefDocType = 'CUS_INC_ENT' and RefDocNo in (select TR_Code from TSPL_CUSTOMER_INCENTIVE_DETAIL where Doc_Code='" + StrDocNo + "')))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='AR-CR' and Source_Doc_No in  ( select TSPL_Customer_Invoice_Head.Document_No from TSPL_Customer_Invoice_Head where RefDocType = 'CUS_INC_ENT' and RefDocNo in (select TR_Code from TSPL_CUSTOMER_INCENTIVE_DETAIL where Doc_Code='" + StrDocNo + "'))  "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Customer_Invoice_Detail where Document_No in ( select TSPL_Customer_Invoice_Head.Document_No from TSPL_Customer_Invoice_Head where RefDocType = 'CUS_INC_ENT' and RefDocNo in (select TR_Code from TSPL_CUSTOMER_INCENTIVE_DETAIL where Doc_Code='" + StrDocNo + "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Customer_Invoice_Head where RefDocType = 'CUS_INC_ENT' and RefDocNo in (select TR_Code from TSPL_CUSTOMER_INCENTIVE_DETAIL where Doc_Code='" + StrDocNo + "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each objtr As clsCustomerIncentiveEntryDetail In obj.arr
                qry = "select Receipt_No  from TSPL_RECEIPT_HEADER where Reference='" + objtr.TR_Code + "'	and Narration='" + clsUserMgtCode.CustomerIncentiveEntry + "'"
                qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(qry) > 0 Then
                    clsRcptEntryHeader.ReverseAndUnpost(qry, trans)
                    clsRcptEntryHeader.fundelete(qry, trans)
                End If
            Next
            qry = "update TSPL_CUSTOMER_INCENTIVE_ENTRY_HEAD set Posted=0,Posted_Date=null,Posted_By=null where Doc_Code='" + StrDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsCustomerIncentiveEntryDetail
#Region "Variables"
    Public TR_Code As String
    Public Doc_Code As String
    Public Cust_Code As String
    Public Cust_Name As String
    Public Incentive_Amount As Decimal
    Public Deduction_Code As String
    Public Deduction_TR_Code As String
    Public Deduction_Amount As Decimal
    Public Amount As Decimal

    Public Avg_Qty As Decimal
    Public Secuity As Decimal
    Public Dues As Decimal
    Public Additional_Security_Deposit_Amt As Decimal
    Public Security_To_Be_Taken As Decimal
    Public Net_Margin_Payable As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsCustomerIncentiveEntryDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerIncentiveEntryDetail In Arr
                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Incentive_Amount", obj.Incentive_Amount)
                clsCommon.AddColumnsForChange(coll, "Deduction_Code", obj.Deduction_Code, True)
                clsCommon.AddColumnsForChange(coll, "Deduction_TR_Code", obj.Deduction_TR_Code, True)
                clsCommon.AddColumnsForChange(coll, "Deduction_Amount", obj.Deduction_Amount)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)

                clsCommon.AddColumnsForChange(coll, "Avg_Qty", obj.Avg_Qty)
                clsCommon.AddColumnsForChange(coll, "Secuity", obj.Secuity)
                clsCommon.AddColumnsForChange(coll, "Dues", obj.Dues)
                clsCommon.AddColumnsForChange(coll, "Additional_Security_Deposit_Amt", obj.Additional_Security_Deposit_Amt)
                clsCommon.AddColumnsForChange(coll, "Security_To_Be_Taken", obj.Security_To_Be_Taken)
                clsCommon.AddColumnsForChange(coll, "Net_Margin_Payable", obj.Net_Margin_Payable)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_INCENTIVE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction, ByRef arrCust As ArrayList) As List(Of clsCustomerIncentiveEntryDetail)
        arrCust = New ArrayList
        Dim arr As List(Of clsCustomerIncentiveEntryDetail) = Nothing
        Dim qry As String = "select TSPL_CUSTOMER_INCENTIVE_DETAIL.*,TSPL_CUSTOMER_MASTER.Customer_Name as Cust_Name  from TSPL_CUSTOMER_INCENTIVE_DETAIL left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_INCENTIVE_DETAIL.Cust_Code  where Doc_Code='" + strDocNo + "' order by TR_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCustomerIncentiveEntryDetail)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsCustomerIncentiveEntryDetail
                obj.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                obj.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
                obj.Incentive_Amount = clsCommon.myCdbl(dr("Incentive_Amount"))
                obj.Deduction_Code = clsCommon.myCstr(dr("Deduction_Code"))
                obj.Deduction_TR_Code = clsCommon.myCstr(dr("Deduction_TR_Code"))
                obj.Deduction_Amount = clsCommon.myCdbl(dr("Deduction_Amount"))
                obj.Amount = clsCommon.myCdbl(dr("Amount"))

                obj.Avg_Qty = clsCommon.myCdbl(dr("Avg_Qty"))
                obj.Secuity = clsCommon.myCdbl(dr("Secuity"))
                obj.Dues = clsCommon.myCdbl(dr("Dues"))
                obj.Additional_Security_Deposit_Amt = clsCommon.myCdbl(dr("Additional_Security_Deposit_Amt"))
                obj.Security_To_Be_Taken = clsCommon.myCdbl(dr("Security_To_Be_Taken"))
                obj.Net_Margin_Payable = clsCommon.myCdbl(dr("Net_Margin_Payable"))

                arr.Add(obj)
                arrCust.Add(obj.Cust_Code)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsCustomerIncentiveEntryCustomerIncentiveWise 'ERO/14/03/19-000513 by balwinder on 15/Mar/2018
#Region "Variables"
    Public TR_Code As String
    Public Doc_Code As String
    Public Date_Wise As Date?
    Public Cust_Code As String
    Public Cust_Name As String
    Public Range_Qty As Decimal
    Public Range_Avg_Qty As Decimal
    Public Range_UOM As String
    Public Incentive_Qty As Decimal
    Public Incentive_UOM As String
    Public Incentive_TR_Code As String
    Public Incentive_Code As String
    Public Incentive_Rate As Decimal
    Public Incentive_Amount As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsCustomerIncentiveEntryCustomerIncentiveWise), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerIncentiveEntryCustomerIncentiveWise In Arr
                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                If obj.Date_Wise IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Date_Wise", clsCommon.GetPrintDate(obj.Date_Wise, "dd/MMM/yyyy"))
                End If

                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Range_Qty", obj.Range_Qty)
                clsCommon.AddColumnsForChange(coll, "Range_Avg_Qty", obj.Range_Avg_Qty)
                clsCommon.AddColumnsForChange(coll, "Range_UOM", obj.Range_UOM)
                clsCommon.AddColumnsForChange(coll, "Incentive_Qty", obj.Incentive_Qty)
                clsCommon.AddColumnsForChange(coll, "Incentive_UOM", obj.Incentive_UOM)
                clsCommon.AddColumnsForChange(coll, "Incentive_TR_Code", obj.Incentive_TR_Code)
                clsCommon.AddColumnsForChange(coll, "Incentive_Code", obj.Incentive_Code)
                clsCommon.AddColumnsForChange(coll, "Incentive_Rate", obj.Incentive_Rate)
                clsCommon.AddColumnsForChange(coll, "Incentive_Amount", obj.Incentive_Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsCustomerIncentiveEntryCustomerIncentiveWise)
        Dim arr As List(Of clsCustomerIncentiveEntryCustomerIncentiveWise) = Nothing
        Dim qry As String = "select TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.*,TSPL_CUSTOMER_MASTER.Customer_Name as Cust_Name  from TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_INCENTIVE_CUSTOMER_INCENTIVE_WISE.Cust_Code  where Doc_Code='" + strDocNo + "' order by TR_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCustomerIncentiveEntryCustomerIncentiveWise)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsCustomerIncentiveEntryCustomerIncentiveWise
                obj.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                If dr("Date_Wise") IsNot DBNull.Value Then
                    obj.Date_Wise = clsCommon.myCDate(dr("Date_Wise"))
                End If
                obj.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                obj.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
                obj.Range_Qty = clsCommon.myCdbl(dr("Range_Qty"))
                obj.Range_Avg_Qty = clsCommon.myCdbl(dr("Range_Avg_Qty"))
                obj.Range_UOM = clsCommon.myCstr(dr("Range_UOM"))
                obj.Incentive_Qty = clsCommon.myCdbl(dr("Incentive_Qty"))
                obj.Incentive_UOM = clsCommon.myCstr(dr("Incentive_UOM"))
                obj.Incentive_TR_Code = clsCommon.myCstr(dr("Incentive_TR_Code"))
                obj.Incentive_Code = clsCommon.myCstr(dr("Incentive_Code"))
                obj.Incentive_Rate = clsCommon.myCdbl(dr("Incentive_Rate"))
                obj.Incentive_Amount = clsCommon.myCdbl(dr("Incentive_Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsCustomerIncentiveEntryStrucreCustomerWise
#Region "Variables"
    Public TR_Code As String
    Public Doc_Code As String
    Public Date_Wise As Date?
    Public Cust_Code As String
    Public Cust_Name As String
    Public Structure_Code As String
    Public Stock_Qty As Decimal
    Public Stock_UOM As String
    Public Range_Qty As Decimal
    Public Range_UOM As String
    Public Incentive_Qty As Decimal
    Public Incentive_UOM As String
    Public Incentive_Code As String
    
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsCustomerIncentiveEntryStrucreCustomerWise), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerIncentiveEntryStrucreCustomerWise In Arr
                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                If obj.Date_Wise IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Date_Wise", clsCommon.GetPrintDate(obj.Date_Wise, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.Structure_Code)
                clsCommon.AddColumnsForChange(coll, "Stock_Qty", obj.Stock_Qty)
                clsCommon.AddColumnsForChange(coll, "Stock_UOM", obj.Stock_UOM)
                clsCommon.AddColumnsForChange(coll, "Range_Qty", obj.Range_Qty)
                clsCommon.AddColumnsForChange(coll, "Range_UOM", obj.Range_UOM)
                clsCommon.AddColumnsForChange(coll, "Incentive_Qty", obj.Incentive_Qty)
                clsCommon.AddColumnsForChange(coll, "Incentive_UOM", obj.Incentive_UOM)
                clsCommon.AddColumnsForChange(coll, "Incentive_Code", obj.Incentive_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsCustomerIncentiveEntryStrucreCustomerWise)
        Dim arr As List(Of clsCustomerIncentiveEntryStrucreCustomerWise) = Nothing
        Dim qry As String = "select TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE.*,TSPL_CUSTOMER_MASTER.Customer_Name as Cust_Name  from TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_INCENTIVE_STRUCTURE_CUSTOMER_WISE.Cust_Code  where Doc_Code='" + strDocNo + "' order by TR_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCustomerIncentiveEntryStrucreCustomerWise)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsCustomerIncentiveEntryStrucreCustomerWise
                obj.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                If dr("Date_Wise") IsNot DBNull.Value Then
                    obj.Date_Wise = clsCommon.myCstr(dr("Date_Wise"))
                End If
                obj.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                obj.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
                obj.Structure_Code = clsCommon.myCstr(dr("Structure_Code"))
                obj.Stock_Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                obj.Stock_UOM = clsCommon.myCstr(dr("Stock_UOM"))
                obj.Range_Qty = clsCommon.myCdbl(dr("Range_Qty"))
                obj.Range_UOM = clsCommon.myCstr(dr("Range_UOM"))
                obj.Incentive_Qty = clsCommon.myCdbl(dr("Incentive_Qty"))
                obj.Incentive_UOM = clsCommon.myCstr(dr("Incentive_UOM"))
                obj.Incentive_Code = clsCommon.myCstr(dr("Incentive_Code"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsCustomerIncentiveEntryItemCustomerWise
#Region "Variables"
    Public TR_Code As String
    Public Doc_Code As String
    Public Date_Wise As Date?
    Public Cust_Code As String
    Public Cust_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public Structure_Code As String
    Public Stock_Qty As Decimal
    Public Stock_UOM As String
    Public Range_Qty As Decimal
    Public Range_UOM As String
    Public Incentive_Qty As Decimal
    Public Incentive_UOM As String
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsCustomerIncentiveEntryItemCustomerWise), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerIncentiveEntryItemCustomerWise In Arr
                Dim coll As New Hashtable()
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                If obj.Date_Wise IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Date_Wise", clsCommon.GetPrintDate(obj.Date_Wise, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.Structure_Code)
                clsCommon.AddColumnsForChange(coll, "Stock_Qty", obj.Stock_Qty)
                clsCommon.AddColumnsForChange(coll, "Stock_UOM", obj.Stock_UOM)
                clsCommon.AddColumnsForChange(coll, "Range_Qty", obj.Range_Qty)
                clsCommon.AddColumnsForChange(coll, "Range_UOM", obj.Range_UOM)
                clsCommon.AddColumnsForChange(coll, "Incentive_Qty", obj.Incentive_Qty)
                clsCommon.AddColumnsForChange(coll, "Incentive_UOM", obj.Incentive_UOM)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsCustomerIncentiveEntryItemCustomerWise)
        Dim arr As List(Of clsCustomerIncentiveEntryItemCustomerWise) = Nothing
        Dim qry As String = "select TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.*,TSPL_CUSTOMER_MASTER.Customer_Name as Cust_Name,TSPL_ITEM_MASTER.Item_Desc as Item_Name  from TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Cust_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Item_Code  where TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.Doc_Code='" + strDocNo + "' order by TSPL_CUSTOMER_INCENTIVE_ITEM_CUSTOMER_WISE.TR_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCustomerIncentiveEntryItemCustomerWise)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsCustomerIncentiveEntryItemCustomerWise
                obj.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                If dr("Date_Wise") IsNot DBNull.Value Then
                    obj.Date_Wise = clsCommon.myCstr(dr("Date_Wise"))
                End If
                obj.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                obj.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Item_Name = clsCommon.myCstr(dr("Item_Name"))
                obj.Structure_Code = clsCommon.myCstr(dr("Structure_Code"))
                obj.Stock_Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                obj.Stock_UOM = clsCommon.myCstr(dr("Stock_UOM"))
                obj.Range_Qty = clsCommon.myCdbl(dr("Range_Qty"))
                obj.Range_UOM = clsCommon.myCstr(dr("Range_UOM"))
                obj.Incentive_Qty = clsCommon.myCdbl(dr("Incentive_Qty"))
                obj.Incentive_UOM = clsCommon.myCstr(dr("Incentive_UOM"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsCustomerIncentiveEntryInvoiceWise
#Region "Variables"
    Public TR_Code As String = Nothing
    Public Doc_Code As String = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public Invoice_Code As String = Nothing
    Public Return_Code As String = Nothing
    Public Doc_Date As Date?
    Public Structure_Code As String
    Public Qty As Decimal
    Public UOM As String
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsCustomerIncentiveEntryInvoiceWise), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim ii As Integer = 1
            For Each obj As clsCustomerIncentiveEntryInvoiceWise In Arr
                obj.TR_Code = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "TR_Code", obj.TR_Code)
                clsCommon.AddColumnsForChange(coll, "Doc_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Invoice_Code", obj.Invoice_Code, True)
                clsCommon.AddColumnsForChange(coll, "Return_Code", obj.Return_Code, True)
                If obj.Doc_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Structure_Code", obj.Structure_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE", OMInsertOrUpdate.Insert, "", trans)
                ii += 1
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsCustomerIncentiveEntryInvoiceWise)
        Dim arr As List(Of clsCustomerIncentiveEntryInvoiceWise) = Nothing
        Dim qry As String = "select TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.*,TSPL_CUSTOMER_MASTER.Customer_Name as Cust_Name,TSPL_ITEM_MASTER.Item_Desc as Item_Name  from TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Cust_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Item_Code  where TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.Doc_Code='" + strDocNo + "' order by TSPL_CUSTOMER_INCENTIVE_INVOICE_WISE.TR_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCustomerIncentiveEntryInvoiceWise)
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsCustomerIncentiveEntryInvoiceWise
                obj.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                obj.Doc_Code = clsCommon.myCstr(dr("Doc_Code"))
                obj.Invoice_Code = clsCommon.myCstr(dr("Invoice_Code"))
                obj.Return_Code = clsCommon.myCstr(dr("Return_Code"))
                If dr("Doc_Date") IsNot DBNull.Value Then
                    obj.Doc_Date = clsCommon.myCDate(dr("Doc_Date"))
                End If


                obj.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                obj.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Item_Name = clsCommon.myCstr(dr("Item_Name"))
                obj.Structure_Code = clsCommon.myCstr(dr("Structure_Code"))
                obj.Qty = clsCommon.myCdbl(dr("Qty"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class