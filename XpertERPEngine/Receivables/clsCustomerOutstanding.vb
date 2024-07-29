Imports common
Imports System.Data.SqlClient
Public Class clsCustomerOutstanding

#Region "Variables"
    Public Customer_Outsanding_No As String = Nothing
    Public Document_Date As DateTime
    Public Cust_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public Location_Code As String = Nothing
    Public Location_Name As String = Nothing
    Public Posted As Integer = 0
    Public Posting_Date As DateTime?
    Dim qry As String = ""

    Public Arr As List(Of clsCustomerOutstandingDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsCustomerOutstanding, ByVal isNewEntry As Boolean, ByVal FormId As String) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = obj.SaveData(obj, isNewEntry, trans, FormId)
            If (isSaved) Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As clsCustomerOutstanding, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, ByVal FormId As String) As Boolean
        Dim isSaved As Boolean = True

        clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleReceivable, clsUserMgtCode.FrmCustomersOutstanding, obj.Location_Code, obj.Document_Date, trans)

        qry = "delete from TSPL_CUSTOMER_OUTSTANDING_DETAIL where Customer_Outsanding_No='" + obj.Customer_Outsanding_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim strDocNo As String = ""
           If isNewEntry Then
            obj.Customer_Outsanding_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.CustomerOutstanding, "", obj.Location_Code)
        End If
     
        If (clsCommon.myLen(obj.Customer_Outsanding_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
        Dim coll As New Hashtable()
        If DateTime = "1" Then
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        Else
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
        End If
        clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
        clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Customer_Outsanding_No", obj.Customer_Outsanding_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_OUTSTANDING_HEADER", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_OUTSTANDING_HEADER", OMInsertOrUpdate.Update, "Customer_Outsanding_No='" + obj.Customer_Outsanding_No + "'", trans)
        End If
        isSaved = isSaved AndAlso clsCustomerOutstandingDetail.SaveData(obj.Customer_Outsanding_No, Arr, trans)
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType) As clsCustomerOutstanding
        Return GetData(strCode, arrLoc, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal arrLoc As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomerOutstanding
        Dim obj As clsCustomerOutstanding = Nothing
        Dim Arr As List(Of clsCustomerOutstanding) = Nothing
        Dim qry As String = "Select TSPL_CUSTOMER_OUTSTANDING_HEADER.*,tspl_Location_master.Location_Desc as Location_Name,tspl_customer_master.Customer_Name from TSPL_CUSTOMER_OUTSTANDING_HEADER left outer join tspl_customer_master on tspl_customer_master.Cust_code=TSPL_CUSTOMER_OUTSTANDING_HEADER.Cust_Code left outer join tspl_Location_master on tspl_Location_master.Location_Code=TSPL_CUSTOMER_OUTSTANDING_HEADER.Location_Code  where 2=2 "
        If clsCommon.myLen(arrLoc) > 0 Then
            qry += "  and TSPL_CUSTOMER_OUTSTANDING_HEADER.Location_Code in (" + arrLoc + ") "
        End If
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_CUSTOMER_OUTSTANDING_HEADER.Customer_Outsanding_No = (select MIN(TSPL_CUSTOMER_OUTSTANDING_HEADER.Customer_Outsanding_No) from TSPL_CUSTOMER_OUTSTANDING_HEADER WHERE 1=1 " + whrclas + " and TSPL_CUSTOMER_OUTSTANDING_HEADER.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Last
                qry += " and TSPL_CUSTOMER_OUTSTANDING_HEADER.Customer_Outsanding_No = (select Max(TSPL_CUSTOMER_OUTSTANDING_HEADER.Customer_Outsanding_No) from TSPL_CUSTOMER_OUTSTANDING_HEADER WHERE 1=1 " + whrclas + " and TSPL_CUSTOMER_OUTSTANDING_HEADER.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Current
                qry += " and TSPL_CUSTOMER_OUTSTANDING_HEADER.Customer_Outsanding_No='" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_CUSTOMER_OUTSTANDING_HEADER.Customer_Outsanding_No = (select Min(TSPL_CUSTOMER_OUTSTANDING_HEADER.Customer_Outsanding_No) from TSPL_CUSTOMER_OUTSTANDING_HEADER where Customer_Outsanding_No>'" + strCode + "' " + whrclas + " and TSPL_CUSTOMER_OUTSTANDING_HEADER.Location_Code in (" + arrLoc + ") )"
            Case NavigatorType.Previous
                qry += " and TSPL_CUSTOMER_OUTSTANDING_HEADER.Customer_Outsanding_No = (select Max(TSPL_CUSTOMER_OUTSTANDING_HEADER.Customer_Outsanding_No) from TSPL_CUSTOMER_OUTSTANDING_HEADER where Customer_Outsanding_No<'" + strCode + "' " + whrclas + " and TSPL_CUSTOMER_OUTSTANDING_HEADER.Location_Code in (" + arrLoc + ") )"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCustomerOutstanding()
            obj.Customer_Outsanding_No = clsCommon.myCstr(dt.Rows(0)("Customer_Outsanding_No"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.Arr = clsCustomerOutstandingDetail.getData(obj.Customer_Outsanding_No, trans)
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal arrLoc As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsCustomerOutstanding = clsCustomerOutstanding.GetData(strDocNo, arrLoc, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Customer_Outsanding_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleReceivable, clsUserMgtCode.FrmCustomersOutstanding, obj.Location_Code, obj.Document_Date, trans)

                If obj.Posted = 1 Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If

                Dim qry As String = "delete from TSPL_CUSTOMER_OUTSTANDING_DETAIL where Customer_Outsanding_No='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


                qry = "delete from TSPL_CUSTOMER_OUTSTANDING_HEADER where Customer_Outsanding_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal arrLoc As String) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = clsCustomerOutstanding.PostData(FormId, strDocNo, arrLoc, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal arrLoc As String, ByVal trans As SqlTransaction, Optional ByVal strVoucherNoForRecreatedOnly As String = Nothing) As Boolean
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Post")
        End If

        Dim obj As clsCustomerOutstanding = clsCustomerOutstanding.GetData(strDocNo, arrLoc, NavigatorType.Current, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.Customer_Outsanding_No) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If

        clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleReceivable, clsUserMgtCode.FrmCustomersOutstanding, obj.Location_Code, obj.Document_Date, trans)
        If (obj.Posted = 1) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If

        createARInvoice(obj, trans, FormId)

        Dim qr As String = "Update TSPL_CUSTOMER_OUTSTANDING_HEADER set Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Modified_By='" + objCommonVar.CurrentUserCode + "' , Posted=1 where Customer_Outsanding_No='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qr, trans)
        Return True
    End Function
    Public Shared Function createARInvoice(ByVal obj As clsCustomerOutstanding, ByVal trans As SqlTransaction, ByVal FormId As String) As Boolean
        ''''''''''''''''''''''''''''''''''For Making AR Invoice



        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
            For Each objTr As clsCustomerOutstandingDetail In obj.Arr
                Dim objCustInv As New clsCustomerInvoiceHead()
                objCustInv.Document_Date = obj.Document_Date
                'If clsCommon.CompairString(obj.InvoiceAgainst, "Against Dispatch") = CompairStringResult.Equal Then
                '    objCustInv.Trans_Type = "BS"
                'Else
                '    objCustInv.Trans_Type = "BST"
                'End If

                objCustInv.Document_Type = "D"
                objCustInv.Document_Total = objTr.PenaltyAmount
                objCustInv.Customer_Code = obj.Cust_Code
                'objCustInv.RoundOffAmount = obj.RoundOffAmount
                objCustInv.RoundOffAmount = 0
                objCustInv.Customer_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Cust_Code + "'", trans))
                objCustInv.Posting_Date = obj.Document_Date
                Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Cust_Code + "'"
                objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)

                objCustInv.loc_code = clsLocation.GetSegmentCode(obj.Location_Code, trans)
                objCustInv.On_Hold = 0
                objCustInv.Remarks = ""
                objCustInv.Description = ""
                objCustInv.Tax_Group = ""
                objCustInv.TAX1 = ""
                objCustInv.TAX1_Rate = 0
                objCustInv.TAX1_Amt = 0
                objCustInv.TAX2 = ""
                objCustInv.TAX2_Rate = 0
                objCustInv.TAX2_Amt = 0
                objCustInv.TAX3 = ""
                objCustInv.TAX3_Rate = 0
                objCustInv.TAX3_Amt = 0
                objCustInv.TAX4 = ""
                objCustInv.TAX4_Rate = 0
                objCustInv.TAX4_Amt = 0
                objCustInv.TAX5 = ""
                objCustInv.TAX5_Rate = 0
                objCustInv.TAX5_Amt = 0
                objCustInv.TAX6 = ""
                objCustInv.TAX6_Rate = 0
                objCustInv.TAX6_Amt = 0
                objCustInv.TAX7 = ""
                objCustInv.TAX7_Rate = 0
                objCustInv.TAX7_Amt = 0
                objCustInv.TAX8 = ""
                objCustInv.TAX8_Rate = 0
                objCustInv.TAX8_Amt = 0
                objCustInv.TAX9 = ""
                objCustInv.TAX9_Rate = 0
                objCustInv.TAX9_Amt = 0
                objCustInv.TAX10 = ""
                objCustInv.TAX10_Rate = 0
                objCustInv.TAX10_Amt = 0
                objCustInv.Total_Tax = 0
                objCustInv.Tax1_BAmount = 0
                objCustInv.Tax2_BAmount = 0
                objCustInv.Tax3_BAmount = 0
                objCustInv.Tax4_BAmount = 0
                objCustInv.Tax5_BAmount = 0
                objCustInv.Tax6_BAmount = 0
                objCustInv.Tax7_BAmount = 0
                objCustInv.Tax8_BAmount = 0
                objCustInv.Tax9_BAmount = 0
                objCustInv.Tax10_BAmount = 0
                objCustInv.Balance_Amt = objTr.PenaltyAmount
                objCustInv.Terms_Code = ""
                objCustInv.PROJECT_ID = ""
                objCustInv.CURRENCY_CODE = objTr.CURRENCY_CODE
                objCustInv.ConvRate = objTr.ConvRateOld
                objCustInv.ApplicableFrom = Nothing


                objCustInv.Discount_Percentage = 0
                ' objCustInv.Discount_Base = objTr.PenaltyAmount - obj.RoundOffAmount
                objCustInv.Discount_Base = objTr.PenaltyAmount
                objCustInv.Discount_Amount = 0

                ' objCustInv.Amount_Less_Discount = objTr.PenaltyAmount - obj.RoundOffAmount
                objCustInv.Amount_Less_Discount = objTr.PenaltyAmount

                Dim dt As DataTable
                dt = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
                End If

                objCustInv.Add_Charge_Code1 = ""
                objCustInv.Add_Charge_Name1 = ""
                objCustInv.Add_Charge_Amt1 = 0
                objCustInv.Add_Charge_Code2 = ""
                objCustInv.Add_Charge_Name2 = ""
                objCustInv.Add_Charge_Amt2 = 0
                objCustInv.Add_Charge_Code3 = ""
                objCustInv.Add_Charge_Name3 = ""
                objCustInv.Add_Charge_Amt3 = 0
                objCustInv.Add_Charge_Code4 = ""
                objCustInv.Add_Charge_Name4 = ""
                objCustInv.Add_Charge_Amt4 = 0
                objCustInv.Add_Charge_Code5 = ""
                objCustInv.Add_Charge_Name5 = ""
                objCustInv.Add_Charge_Amt5 = 0
                objCustInv.Add_Charge_Code6 = ""
                objCustInv.Add_Charge_Name6 = ""
                objCustInv.Add_Charge_Amt6 = 0
                objCustInv.Add_Charge_Code7 = ""
                objCustInv.Add_Charge_Name7 = ""
                objCustInv.Add_Charge_Amt7 = 0
                objCustInv.Add_Charge_Code8 = ""
                objCustInv.Add_Charge_Name8 = ""
                objCustInv.Add_Charge_Amt8 = 0
                objCustInv.Add_Charge_Code9 = ""
                objCustInv.Add_Charge_Name9 = ""
                objCustInv.Add_Charge_Amt9 = 0
                objCustInv.Add_Charge_Code10 = ""
                objCustInv.Add_Charge_Name10 = ""
                objCustInv.Add_Charge_Amt10 = 0
                objCustInv.Total_Add_Charge = 0
                objCustInv.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
                ' objCustInv.Against_Sale_No = obj.Document_No
                objCustInv.RefDocNo = objTr.Document_No
                objCustInv.Remarks = "Auto Debit Note Against Doc No " & objTr.Document_No & " for penalty Charges "
                objCustInv.Description = "Auto Debit Note Against Doc No " & objTr.Document_No & " for penalty Charges "

                Dim counter As Integer = 1
                objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)
                Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()

                objCustInvTR.SNo = counter
                dt = clsDBFuncationality.GetDataTable("select Penalty_Charges_Account from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Penalty Charges account for customer " + obj.Cust_Code)
                End If

                objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Penalty_Charges_Account"))
                objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, obj.Location_Code, trans)
                objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, trans)
                objCustInvTR.Reco_Control_Account = "S"


                objCustInvTR.Amount = objTr.PenaltyAmount
                objCustInvTR.Discount_Per = 0
                objCustInvTR.Discount = 0

                objCustInvTR.Amount_less_Discount = objTr.PenaltyAmount

                objCustInvTR.TAX1 = ""
                objCustInvTR.TAX1_Rate = 0
                objCustInvTR.TAX1_Amt = 0
                objCustInvTR.TAX2 = ""
                objCustInvTR.TAX2_Rate = 0
                objCustInvTR.TAX2_Amt = 0
                objCustInvTR.TAX3 = ""
                objCustInvTR.TAX3_Rate = 0
                objCustInvTR.TAX3_Amt = 0
                objCustInvTR.TAX4 = ""
                objCustInvTR.TAX4_Rate = 0
                objCustInvTR.TAX4_Amt = 0
                objCustInvTR.TAX5 = ""
                objCustInvTR.TAX5_Rate = 0
                objCustInvTR.TAX5_Amt = 0
                objCustInvTR.TAX6 = ""
                objCustInvTR.TAX6_Rate = 0
                objCustInvTR.TAX6_Amt = 0
                objCustInvTR.TAX7 = ""
                objCustInvTR.TAX7_Rate = 0
                objCustInvTR.TAX7_Amt = 0
                objCustInvTR.TAX8 = ""
                objCustInvTR.TAX8_Rate = 0
                objCustInvTR.TAX8_Amt = 0
                objCustInvTR.TAX9 = ""
                objCustInvTR.TAX9_Rate = 0
                objCustInvTR.TAX9_Amt = 0
                objCustInvTR.TAX10 = ""
                objCustInvTR.TAX10_Rate = 0
                objCustInvTR.TAX10_Amt = 0
                objCustInvTR.Total_Tax = 0

                objCustInvTR.Total_Amount = objTr.PenaltyAmount
                objCustInvTR.Remarks = ""
                objCustInvTR.TAX1_Base_Amt = 0
                objCustInvTR.TAX2_Base_Amt = 0
                objCustInvTR.TAX3_Base_Amt = 0
                objCustInvTR.TAX4_Base_Amt = 0
                objCustInvTR.TAX5_Base_Amt = 0
                objCustInvTR.TAX6_Base_Amt = 0
                objCustInvTR.TAX7_Base_Amt = 0
                objCustInvTR.TAX8_Base_Amt = 0
                objCustInvTR.TAX9_Base_Amt = 0
                objCustInvTR.TAX10_Base_Amt = 0
                objCustInv.Arr.Add(objCustInvTR)
                counter += 1


                objCustInv.SaveData(objCustInv, True, trans, "CustomerOutstanding")
                clsCustomerInvoiceHead.PostData("CustomerOutstanding", objCustInv.Document_No, "", trans)

            Next
        End If


        Return True
    End Function
End Class

Public Class clsCustomerOutstandingDetail
#Region "Variables"
    Public Customer_Outsanding_No As String = String.Empty
    Public Customer_Outsanding_Date As DateTime
    Public Line_No As Integer = 0
    Public Apply As String = String.Empty
    Public Document_Type As String = String.Empty
    Public Document_No As String = String.Empty
    Public Document_Date As Date?
    Public Due_Date As Date?
    Public AgeingDays As Double = 0
    Public Pending_Balance As Double = 0
    Public PenaltyPer As Double = 0
    Public PenaltyAmount As Double = 0
    Public CURRENCY_CODE As String = String.Empty
    Public ConvRateOld As Double = 0
    
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsCustomerOutstandingDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsCustomerOutstandingDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Customer_Outsanding_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Customer_Outsanding_Date", clsCommon.GetPrintDate(obj.Customer_Outsanding_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Apply", obj.Apply)
                clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "AgeingDays", obj.AgeingDays)
                clsCommon.AddColumnsForChange(coll, "Pending_Balance", obj.Pending_Balance)
                clsCommon.AddColumnsForChange(coll, "PenaltyPer", obj.PenaltyPer)
                clsCommon.AddColumnsForChange(coll, "PenaltyAmount", obj.PenaltyAmount)
                clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE)
                clsCommon.AddColumnsForChange(coll, "ConvRateOld", obj.ConvRateOld)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_OUTSTANDING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function getData(ByVal strDocumentNo As String, ByVal trans As SqlTransaction) As List(Of clsCustomerOutstandingDetail)
        Try
            Dim arrObj As List(Of clsCustomerOutstandingDetail) = Nothing
            Dim obj As clsCustomerOutstandingDetail = Nothing
            Dim qry As String = "Select * from TSPL_CUSTOMER_OUTSTANDING_DETAIL where Customer_Outsanding_No='" + strDocumentNo + "' ORDER BY Line_No"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arrObj = New List(Of clsCustomerOutstandingDetail)
                Dim objTr As clsCustomerOutstandingDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsCustomerOutstandingDetail
                    objTr.Customer_Outsanding_No = clsCommon.myCstr(dr("Customer_Outsanding_No"))
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Apply = clsCommon.myCstr(dr("Apply"))
                    objTr.Document_Type = clsCommon.myCstr(dr("Document_Type"))
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Customer_Outsanding_Date = clsCommon.myCDate(dr("Customer_Outsanding_Date"))
                    objTr.Document_Date = clsCommon.myCDate(dr("Document_Date"))
                    objTr.Due_Date = clsCommon.myCDate(dr("Due_Date"))
                    objTr.AgeingDays = clsCommon.myCdbl(dr("AgeingDays"))
                    objTr.Pending_Balance = clsCommon.myCdbl(dr("Pending_Balance"))
                    objTr.PenaltyAmount = clsCommon.myCdbl(dr("PenaltyAmount"))
                    objTr.PenaltyPer = clsCommon.myCdbl(dr("PenaltyPer"))
                    objTr.ConvRateOld = clsCommon.myCdbl(dr("ConvRateOld"))
                    objTr.CURRENCY_CODE = clsCommon.myCstr(dr("CURRENCY_CODE"))
                    arrObj.Add(objTr)
                Next
            End If
            Return arrObj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class