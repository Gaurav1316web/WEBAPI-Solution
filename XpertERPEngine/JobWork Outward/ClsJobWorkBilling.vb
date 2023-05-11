''--27/08/2012--Updation By--[Pankaj Kumar]--Applied GL Security While Navigating Document FInder-------Fwd By--Ranjana Mam
' done by priti BHA/26/11/18-000708
Imports common
Imports System.Data.SqlClient
Imports System.Data
Public Class ClsJobWorkBilling
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As String = Nothing
    Public Specification As String = Nothing
    Public From_Date As String = Nothing
    Public To_Date As String = Nothing
    Public Status As String = Nothing
    Public cust_Code As String = Nothing
    Public cust_Name As String = Nothing
    Public Loc_Code As String = Nothing
    Public Loc_Name As String = Nothing
    Public Description As String = Nothing
    Public reff As String = Nothing
    Public Tax_Group As String = Nothing
    Public Tax_Desc As String = Nothing
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Amt As Double = 0 
    Public Amount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public doc_Amt As Double = 0
    Public Comp_Code As String = Nothing 
    Public Terms_Code As String = Nothing  
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Invoice_Type As String = Nothing
    Public Arr As List(Of ClsJobWorkBillingDetail) = Nothing
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public RoundOffAmount As Double = 0
    Public Total_Gross_Weight As Double = 0
    Public Total_Net_Weight As Double = 0
    Public Is_Taxable As Boolean = False
    Public EWayBillNo As String
    Public EWayBillDate As Date? = Nothing
    Public Electronic_Ref_No As String
    Public Billing_Loc_Code As String = String.Empty
    Public Billing_Loc_Name As String = String.Empty

#End Region


    Public Function SaveData(ByVal obj As ClsJobWorkBilling, ByVal strScrapSaleInvoiceNo As String, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim Desc As String = String.Empty
        Dim VatInvoiceType As String = Nothing
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Inward", "JobWork Billing", obj.Loc_Code, obj.Document_Date, trans)
            If Not isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_JOBWORK_BILLING_HEAD", "Document_Code", "TSPL_JOBWORK_BILLING_DETAIL", "Document_Code", trans)
            End If

            Dim qry As String = "delete from TSPL_JOBWORK_BILLING_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.JobWorkBilling, "", obj.Loc_Code)
            End If
            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            '-----------------------------
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt "))
            clsCommon.AddColumnsForChange(coll, "cust_Code", obj.cust_Code)
            clsCommon.AddColumnsForChange(coll, "cust_Name", obj.cust_Name)
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "reff", obj.reff)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Tax_Desc", obj.Tax_Desc)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "doc_Amt", obj.doc_Amt)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)        
            clsCommon.AddColumnsForChange(coll, "Invoice_Type", obj.Invoice_Type)          
            clsCommon.AddColumnsForChange(coll, "Total_Gross_Weight", obj.Total_Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Total_Net_Weight", obj.Total_Net_Weight)
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", IIf(obj.Is_Taxable, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Billing_Loc_Code", obj.Billing_Loc_Code, True)
            clsCommon.AddColumnsForChange(coll, "Billing_Loc_Name", obj.Billing_Loc_Name, True)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOBWORK_BILLING_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOBWORK_BILLING_HEAD", OMInsertOrUpdate.Update, "TSPL_JOBWORK_BILLING_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If

            isSaved = isSaved AndAlso ClsJobWorkBillingDetail.SaveData(obj.Document_Code, Arr, trans)

            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function InvoiceType(ByVal Location As String, ByVal Customer As String, ByVal trans As SqlTransaction) As String
        Dim dt As DataTable
        Dim strloc As String = Nothing
        Dim qry As String = Nothing
        Dim strInvoiceType As String = Nothing
        strloc = Location
        qry = "SELECT TSPL_LOCATION_MASTER.Excisable,TSPL_LOCATION_MASTER.State, " & _
          "TSPL_LOCATION_MASTER.Sales_Tax_Group as LocalTaxGroup,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as Local_Tax_GroupName, " & _
          "TSPL_LOCATION_MASTER.Sales_Tax_GroupIS as InterstateTaxGroup,TSPL_TAX_GROUP_MASTERIS.Tax_Group_Desc as Interstate_Tax_GroupName " & _
          "FROM TSPL_LOCATION_MASTER left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_Group and TSPL_TAX_GROUP_MASTER.Tax_Group_Type='S' left outer join TSPL_TAX_GROUP_MASTER as TSPL_TAX_GROUP_MASTERIS on TSPL_TAX_GROUP_MASTERIS.Tax_Group_Code=TSPL_LOCATION_MASTER.Sales_Tax_GroupIS and TSPL_TAX_GROUP_MASTERIS.Tax_Group_Type='S' " & _
          "WHERE TSPL_LOCATION_MASTER.Location_Code = '" + strloc + "'"


        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strLocState As String = clsCommon.myCstr(dt.Rows(0)("State"))

        qry = "select Price_Code,price_CodeNon,State,Tin_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + Customer + "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim strCustState As String = clsCommon.myCstr(dt.Rows(0)("State"))
        Dim strTinNo As String = clsCommon.myCstr(dt.Rows(0)("Tin_No"))

        If clsCommon.myLen(strTinNo) > 0 AndAlso clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
            strInvoiceType = "T"
        Else
            If clsCommon.CompairString(strLocState, strCustState) = CompairStringResult.Equal Then
                strInvoiceType = "R"
            Else
                strInvoiceType = "I" 'Interstate series
            End If

        End If

        Return strInvoiceType
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strInvoiceCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select status from TSPL_JOBWORK_BILLING_HEAD where Document_Code='" + strInvoiceCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "select distinct Receipt_No  from TSPL_RECEIPT_DETAIL where Document_No in (select Document_No from TSPL_Customer_Invoice_Head where AgainstScrap in ('" + strInvoiceCode + "') ) "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current Document is used in following Receipt -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Receipt_No"))
                Next
                Throw New Exception(Qry)
            End If

            ''--- Joun Entry Delete
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No " &
            " in (select Document_No from TSPL_Customer_Invoice_Head where AgainstScrap='" + strInvoiceCode + "')", trans))
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)

            Qry = "Delete from TSPL_JOURNAL_DETAILS where Voucher_No='" & Voucher_No & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Delete from  TSPL_JOURNAL_MASTER where Voucher_No='" & Voucher_No & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            ''---- End


            Dim strARInvoiceNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_Customer_Invoice_Head where AgainstScrap='" + strInvoiceCode + "'", trans))
            If clsCommon.myLen(strARInvoiceNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strARInvoiceNo, "TSPL_Customer_Invoice_Head", "Document_No", "TSPL_Customer_Invoice_Detail", "Document_No", trans)
                Qry = "Delete from TSPL_Customer_Invoice_Detail where Document_No ='" + strARInvoiceNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "Delete from  TSPL_Customer_Invoice_Head where Document_No='" + strARInvoiceNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            'Qry = "Delete from TSPL_Customer_Invoice_Detail where Document_No in (select Document_No from TSPL_Customer_Invoice_Head where AgainstScrap='" + strInvoiceCode + "')"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            'Qry = "Delete from  TSPL_Customer_Invoice_Head where AgainstScrap='" + strInvoiceCode + "'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_JOBWORK_BILLING_HEAD set status = 0 where Document_Code='" + strInvoiceCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strInvoiceCode, "TSPL_JOBWORK_BILLING_HEAD", "Document_Code", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String, ByVal NavType As NavigatorType) As Boolean
        '' created by Sanjay
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As ClsJobWorkBilling = ClsJobWorkBilling.GetData(Doc_No, NavType, trans)

            If obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            ''richa agarwal 05 Jan,2021
            Dim dtirn As DataTable = clsDBFuncationality.GetDataTable("select Einvoice_type,IRN_No,Is_Taxable,Billing_Loc_Code  from TSPL_JOBWORK_BILLING_HEAD where Document_Code='" & Doc_No & "'", trans)
            If dtirn IsNot Nothing AndAlso dtirn.Rows.Count > 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(dtirn.Rows(0)("Einvoice_type")), "BB") = CompairStringResult.Equal AndAlso obj.Is_Taxable = True AndAlso clsERPFuncationality.GetEInvoiceStatus(obj.Document_Date, trans) = True Then
                    If ClsEInvoiceOFAPIs.EInvoice_Cancellation(Doc_No, clsCommon.myCstr(dtirn.Rows(0)("IRN_No")), clsCommon.myCstr(dtirn.Rows(0)("Billing_Loc_Code")), trans) = True Then
                    Else
                        Throw New Exception("Invalid JSON Value")
                    End If
                End If
            End If
            ''----------

            '' transfer data into cancel table
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_JOBWORK_BILLING_HEAD", "Document_Code", "TSPL_JOBWORK_BILLING_DETAIL", "Document_Code", trans)

            '' cancel customer invoice data
            qry = "select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & Doc_No & "'"
            Dim Document_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Document_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Document_No, "TSPL_Customer_Invoice_Head", "Document_No", "TSPL_Customer_Invoice_Detail", "Document_No", trans)
            End If

            '' cancel journal master data invoice
            qry = "select Voucher_No from TSPL_JOURNAL_MASTER  where Source_Doc_No in (select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & Doc_No & "')"
            Dim Voucher_No As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(Voucher_No) > 0 Then
                clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Voucher_No, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
            End If

            '' cancel custom field data
            clsCommonFunctionality.SaveCancelDataMultKey(objCommonVar.CurrentUserCode, Doc_No, "TSPL_CUSTOM_FIELD_VALUES", "Transaction_Code", "Program_Code", Form_Id, trans)

            ''delete data from multiple tables

            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & Doc_No & "'))"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & Doc_No & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Customer_Invoice_Detail where Document_No in (Select Document_No from TSPL_Customer_Invoice_Head  where AgainstScrap='" & Doc_No & "')"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Customer_Invoice_Head where AgainstScrap='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_CUSTOM_FIELD_VALUES where Transaction_Code ='" & Doc_No & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOBWORK_BILLING_DETAIL where Document_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_JOBWORK_BILLING_HEAD where Document_Code='" & Doc_No & "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
            '' release objects 
            obj = Nothing
            qry = Nothing

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As ClsJobWorkBilling
        Return GetData(strDocumentNo, NavType, Nothing, False)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, ByVal isAssetType As Boolean) As ClsJobWorkBilling
        Return GetData(strPONo, NavType, trans)
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsJobWorkBilling
        Dim obj As ClsJobWorkBilling = Nothing
        Dim qry As String = "SELECT BillingLocation.Location_Desc as Billing_loc_Name ,TSPL_JOBWORK_BILLING_HEAD.Billing_loc_code, TSPL_JOBWORK_BILLING_HEAD.From_Date,TSPL_JOBWORK_BILLING_HEAD.To_Date,TSPL_JOBWORK_BILLING_HEAD.Invoice_Type,TSPL_JOBWORK_BILLING_HEAD.Document_Code,TSPL_JOBWORK_BILLING_HEAD.Document_Date,TSPL_JOBWORK_BILLING_HEAD.cust_Code,TSPL_JOBWORK_BILLING_HEAD.cust_Name, " & _
            "TSPL_JOBWORK_BILLING_HEAD.Status,TSPL_JOBWORK_BILLING_HEAD.Description,TSPL_JOBWORK_BILLING_HEAD.reff,TSPL_JOBWORK_BILLING_HEAD.Tax_Group, " & _
            "TSPL_JOBWORK_BILLING_HEAD.tax_desc, TSPL_JOBWORK_BILLING_HEAD.loc_code, TSPL_JOBWORK_BILLING_HEAD.loc_Name, " & _
            "TSPL_JOBWORK_BILLING_HEAD.TAX1,TSPL_JOBWORK_BILLING_HEAD.TAX1_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX1_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX1_Base_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX2,TSPL_JOBWORK_BILLING_HEAD.TAX2_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX2_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX2_Base_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX3,TSPL_JOBWORK_BILLING_HEAD.TAX3_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX3_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX3_Base_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX4,TSPL_JOBWORK_BILLING_HEAD.TAX4_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX4_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX4_Base_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX5,TSPL_JOBWORK_BILLING_HEAD.TAX5_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX5_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX5_Base_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX6,TSPL_JOBWORK_BILLING_HEAD.TAX6_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX6_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX6_Base_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX7,TSPL_JOBWORK_BILLING_HEAD.TAX7_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX7_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX7_Base_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX8,TSPL_JOBWORK_BILLING_HEAD.TAX8_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX8_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX8_Base_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX9,TSPL_JOBWORK_BILLING_HEAD.TAX9_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX9_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX9_Base_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX10,TSPL_JOBWORK_BILLING_HEAD.TAX10_Rate,TSPL_JOBWORK_BILLING_HEAD.TAX10_Amt,TSPL_JOBWORK_BILLING_HEAD.TAX10_Base_Amt, " & _
            "TSPL_JOBWORK_BILLING_HEAD.Amount,TSPL_JOBWORK_BILLING_HEAD.Total_Tax_Amt,TSPL_JOBWORK_BILLING_HEAD.Doc_Amt,TSPL_JOBWORK_BILLING_HEAD.Comp_Code,TSPL_JOBWORK_BILLING_HEAD.Terms_Code,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_JOBWORK_BILLING_HEAD.Total_Gross_Weight,TSPL_JOBWORK_BILLING_HEAD.Total_Net_Weight,TSPL_JOBWORK_BILLING_HEAD.RoundOffAmount,TSPL_JOBWORK_BILLING_HEAD.Is_Taxable,TSPL_JOBWORK_BILLING_HEAD.EWayBillNo,TSPL_JOBWORK_BILLING_HEAD.EWayBillDate FROM TSPL_JOBWORK_BILLING_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_JOBWORK_BILLING_HEAD.loc_code left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_JOBWORK_BILLING_HEAD.Terms_Code  left outer join TSPL_LOCATION_MASTER as BillingLocation on BillingLocation.Location_Code=TSPL_JOBWORK_BILLING_HEAD.Billing_loc_code where 2=2"
        Dim whrCls As String = ""

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND loc_code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_JOBWORK_BILLING_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_JOBWORK_BILLING_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_JOBWORK_BILLING_HEAD.Document_Code = (select Max(Document_Code) from TSPL_JOBWORK_BILLING_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_JOBWORK_BILLING_HEAD.Document_Code = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_JOBWORK_BILLING_HEAD.Document_Code = (select Min(Document_Code) from TSPL_JOBWORK_BILLING_HEAD where Document_Code>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_JOBWORK_BILLING_HEAD.Document_Code = (select Max(Document_Code) from TSPL_JOBWORK_BILLING_HEAD where Document_Code<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsJobWorkBilling()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.cust_Code = clsCommon.myCstr(dt.Rows(0)("cust_Code"))
            obj.cust_Name = clsCommon.myCstr(dt.Rows(0)("cust_Name"))
            obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Loc_Name = clsCommon.myCstr(dt.Rows(0)("Loc_Name"))
            obj.Billing_Loc_Code = clsCommon.myCstr(dt.Rows(0)("Billing_Loc_Code"))
            obj.Billing_Loc_Name = clsCommon.myCstr(dt.Rows(0)("Billing_Loc_Name"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.reff = clsCommon.myCstr(dt.Rows(0)("reff"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.Tax_Desc = clsCommon.myCstr(dt.Rows(0)("Tax_Desc"))
            obj.Amount = clsCommon.myCstr(dt.Rows(0)("Amount"))
            obj.Total_Tax_Amt = clsCommon.myCstr(dt.Rows(0)("Total_tax_amt"))
            obj.doc_Amt = clsCommon.myCstr(dt.Rows(0)("Doc_Amt"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Base_Amt"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Base_Amt"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Base_Amt"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Base_Amt"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Base_Amt"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.TAX6_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Base_Amt"))
            obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.TAX7_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Base_Amt"))
            obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.TAX8_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Base_Amt"))
            obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
            obj.TAX9_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Base_Amt"))
            obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.TAX10_Base_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Base_Amt"))
            obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))         
            obj.Invoice_Type = clsCommon.myCstr(dt.Rows(0)("Invoice_Type"))         
            obj.Total_Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Total_Gross_Weight"))
            obj.Total_Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Total_Net_Weight"))
            obj.Is_Taxable = If(clsCommon.myCdbl(dt.Rows(0)("Is_Taxable")) > 0, True, False)
            obj.EWayBillNo = clsCommon.myCstr(dt.Rows(0)("EWayBillNo"))
            If dt.Rows(0)("EWayBillDate") IsNot DBNull.Value Then
                obj.EWayBillDate = clsCommon.myCDate(dt.Rows(0)("EWayBillDate"))
            End If
            qry = "SELECT TSPL_JOBWORK_BILLING_DETAIL.ConvKG_Qty,TSPL_JOBWORK_BILLING_DETAIL.ConvKG_Price,TSPL_JOBWORK_BILLING_DETAIL.JW_EstimationNo,TSPL_JOBWORK_BILLING_DETAIL.ItemwiseTaxCode,TSPL_JOBWORK_BILLING_DETAIL.Document_Code,TSPL_JOBWORK_BILLING_DETAIL.Line_No,TSPL_JOBWORK_BILLING_DETAIL.Item_Code,TSPL_JOBWORK_BILLING_DETAIL.Item_Desc, " & _
                "TSPL_JOBWORK_BILLING_DETAIL.unit_code,TSPL_JOBWORK_BILLING_DETAIL.Invoice_Qty,TSPL_JOBWORK_BILLING_DETAIL.price,TSPL_JOBWORK_BILLING_DETAIL.JWI_Price_Code, " & _
                "TSPL_JOBWORK_BILLING_DETAIL.TotalTaxAmt,TSPL_JOBWORK_BILLING_DETAIL.ItemAmt,TSPL_JOBWORK_BILLING_DETAIL.TotalAmt, " & _
                "TSPL_JOBWORK_BILLING_DETAIL.TAX1,TSPL_JOBWORK_BILLING_DETAIL.TAX1_Rate,TSPL_JOBWORK_BILLING_DETAIL.TAX1_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX2,TSPL_JOBWORK_BILLING_DETAIL.TAX2_Rate,TSPL_JOBWORK_BILLING_DETAIL.TAX2_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX3,TSPL_JOBWORK_BILLING_DETAIL.TAX3_Rate,TSPL_JOBWORK_BILLING_DETAIL.TAX3_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX4,TSPL_JOBWORK_BILLING_DETAIL.TAX4_Rate,TSPL_JOBWORK_BILLING_DETAIL.TAX4_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX5,TSPL_JOBWORK_BILLING_DETAIL.TAX5_Rate,TSPL_JOBWORK_BILLING_DETAIL.TAX5_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX6,TSPL_JOBWORK_BILLING_DETAIL.TAX6_Rate,TSPL_JOBWORK_BILLING_DETAIL.TAX6_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX7,TSPL_JOBWORK_BILLING_DETAIL.TAX7_Rate,TSPL_JOBWORK_BILLING_DETAIL.TAX7_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX8,TSPL_JOBWORK_BILLING_DETAIL.TAX8_Rate,TSPL_JOBWORK_BILLING_DETAIL.TAX8_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX9,TSPL_JOBWORK_BILLING_DETAIL.TAX9_Rate,TSPL_JOBWORK_BILLING_DETAIL.TAX9_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX10, " & _
                "TSPL_JOBWORK_BILLING_DETAIL.TAX10_Rate,TSPL_JOBWORK_BILLING_DETAIL.TAX10_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX1_Base_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX2_Base_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX3_Base_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX4_Base_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX5_Base_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX6_Base_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX7_Base_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX8_Base_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX9_Base_Amt,TSPL_JOBWORK_BILLING_DETAIL.TAX10_Base_Amt, " & _
                "TSPL_JOBWORK_BILLING_DETAIL.FATKg,TSPL_JOBWORK_BILLING_DETAIL.SNFKg,TSPL_JOBWORK_BILLING_DETAIL.ConvKG_Qty,TSPL_JOBWORK_BILLING_DETAIL.ConvKG_Qty  FROM TSPL_JOBWORK_BILLING_DETAIL  where TSPL_JOBWORK_BILLING_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_JOBWORK_BILLING_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsJobWorkBillingDetail)
                Dim objTr As ClsJobWorkBillingDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsJobWorkBillingDetail
                    objTr.JW_EstimationNo = clsCommon.myCstr(dr("JW_EstimationNo"))
                    objTr.ItemwiseTaxCode = clsCommon.myCstr(dr("ItemwiseTaxCode"))
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Invoice_Qty = clsCommon.myCdbl(dr("Invoice_Qty"))
                    objTr.ConvKG_Qty = clsCommon.myCdbl(dr("ConvKG_Qty"))
                    objTr.FATKg = clsCommon.myCdbl(dr("FATKg"))
                    objTr.SNFKg = clsCommon.myCdbl(dr("SNFKg"))
                    objTr.price = clsCommon.myCdbl(dr("price"))
                    objTr.JWI_Price_Code = clsCommon.myCstr(dr("JWI_Price_Code"))
                    objTr.ConvKG_Price = clsCommon.myCdbl(dr("ConvKG_Price"))
                    objTr.ItemAmt = clsCommon.myCdbl(dr("ItemAmt"))
                    objTr.TotalTaxAmt = clsCommon.myCdbl(dr("TotalTaxAmt"))
                    objTr.TotalAmt = clsCommon.myCdbl(dr("TotalAmt"))
                    objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objTr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objTr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objTr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objTr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objTr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objTr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objTr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objTr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))
                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    'objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function
    Public Shared Function EInvoice_Implementation(ByVal strDocNo As String, ByVal strLocation As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If

            Dim strtoken As String = ClsEInvoiceOFAPIs.IsGenerateAuthTokenNo_Required(objCommonVar.CurrentCompanyCode, strLocation, trans)
            If clsCommon.myLen(strtoken) > 0 Then
                Dim strQry As String = "select TSPL_Customer_master .Cust_Code ,TSPL_JOBWORK_BILLING_HEAD.Document_Code as DocNo,convert(date,TSPL_JOBWORK_BILLING_HEAD.Document_Date,103) as DocDate,case when TSPL_Customer_Invoice_Head.Document_Type='D' then 'DBN' when TSPL_Customer_Invoice_Head.Document_Type ='C' then 'CRN' else 'INV' end as DocType ,'B2B' as SupTyp, 'N'  as IgstOnIntra,Bill_To_Location.GSTNO as SellerGSTINNo ,Bill_To_Location.location_desc as SellerLglNm,TSPL_COMPANY_MASTER.Comp_Name as SellerTrdNm,Bill_To_Location.Add1 as SellerAdd1,Bill_To_Location.Add2 as SellerAdd2 ,Bill_To_Location.City_Code  as SellerLoc,Bill_To_Location.Pin_Code  as SellerPincode,BillToLocation_State_Master.GST_STATE_Code as SellerStcd,Bill_To_Location.Phone1 as SellerPhone,Bill_To_Location.Email as SellerEmail,TSPL_Customer_master.GSTNo as BuyerGSTINNo ,TSPL_Customer_master.Customer_Name as BuyerLglNm,TSPL_Customer_master.Alies_name as BuyerTrdNm, Customer_State_Master.GST_STATE_Code as BuyerPOS,TSPL_Customer_master.Add1 as BuyerAdd1,TSPL_Customer_master.Add2 as BuyerAdd2 ,tspl_city_master.City_Name as BuyerLoc,cast(TSPL_Customer_master.PIN_NO as int) as BuyerPincode,Customer_State_Master.GST_STATE_Code as BuyerStcd,TSPL_Customer_master.Phone1 as BuyerPhone,TSPL_Customer_master.Email as BuyerEmail,TSPL_JOBWORK_BILLING_DETAIL.Line_No as ItemSlNo, 'N' as ItemIsServc,TSPL_ITEM_MASTER.Item_Desc AS ItemPrdDesc,TSPL_ITEM_MASTER.HSN_Code AS ItemHsnCd,TSPL_JOBWORK_BILLING_DETAIL.Invoice_Qty as ItemQty,TSPL_JOBWORK_BILLING_DETAIL.Unit_code as ItemUnit,TSPL_JOBWORK_BILLING_DETAIL.Price as ItemUnitPrice,TSPL_JOBWORK_BILLING_DETAIL.ItemAmt as ItemTotAmt,0 as ItemDiscount,TSPL_JOBWORK_BILLING_DETAIL.ItemAmt as ItemAssAmt,case when ISNULL(TSPL_JOBWORK_BILLING_DETAIL .tax1,'') ='IGST' THEN TSPL_JOBWORK_BILLING_DETAIL.TAX1_Rate when ISNULL(TSPL_JOBWORK_BILLING_DETAIL .tax1,'') ='CGST' AND   ISNULL(TSPL_JOBWORK_BILLING_DETAIL .tax2,'') ='SGST'  THEN TSPL_JOBWORK_BILLING_DETAIL.TAX1_Rate+TSPL_JOBWORK_BILLING_DETAIL.TAX2_Rate  ELSE 0 end as ItemGstRt, case when TSPL_JOBWORK_BILLING_DETAIL .TAX1 ='SGST' AND TSPL_JOBWORK_BILLING_DETAIL .TAX2  ='CGST' then TSPL_JOBWORK_BILLING_DETAIL.TAX1_Amt when TSPL_JOBWORK_BILLING_DETAIL .TAX1 ='CGST' AND TSPL_JOBWORK_BILLING_DETAIL .TAX2  ='SGST' then TSPL_JOBWORK_BILLING_DETAIL.TAX2_Amt else 0 end ItemSgstAmt,case when TSPL_JOBWORK_BILLING_DETAIL .TAX1 ='IGST' then TSPL_JOBWORK_BILLING_DETAIL.TAX1_Amt else 0 end ItemIgstAmt,case when TSPL_JOBWORK_BILLING_DETAIL .TAX1 ='CGST' AND TSPL_JOBWORK_BILLING_DETAIL .TAX2  ='SGST' then TSPL_JOBWORK_BILLING_DETAIL.TAX1_Amt when TSPL_JOBWORK_BILLING_DETAIL .TAX1 ='SGST' AND TSPL_JOBWORK_BILLING_DETAIL .TAX2  ='CGST' then TSPL_JOBWORK_BILLING_DETAIL.TAX2_Amt else 0 end ItemCgstAmt,0 as ItemOthChrg,TSPL_JOBWORK_BILLING_DETAIL.TotalAmt-case when isnull(TCS1.is_tcs,'')='Y' THEN  TSPL_JOBWORK_BILLING_DETAIL.TAX2_AMT when isnull(TCS2.is_tcs,'')='Y' THEN  TSPL_JOBWORK_BILLING_DETAIL.TAX3_AMT ELSE 0 END as ItemTotItemVal,TSPL_JOBWORK_BILLING_HEAD .Amount as ValDtlsAssVal,case when TSPL_JOBWORK_BILLING_HEAD .TAX1 ='CGST' AND TSPL_JOBWORK_BILLING_HEAD .TAX2  ='SGST' then TSPL_JOBWORK_BILLING_HEAD.TAX1_Amt when TSPL_JOBWORK_BILLING_HEAD .TAX1 ='SGST' AND TSPL_JOBWORK_BILLING_HEAD .TAX2  ='CGST' then TSPL_JOBWORK_BILLING_HEAD.TAX2_Amt else 0 end ValDtlsCgstVal, case when TSPL_JOBWORK_BILLING_HEAD .TAX1 ='SGST' AND TSPL_JOBWORK_BILLING_HEAD .TAX2  ='CGST' then TSPL_JOBWORK_BILLING_HEAD.TAX1_Amt when TSPL_JOBWORK_BILLING_HEAD .TAX1 ='CGST' AND TSPL_JOBWORK_BILLING_HEAD .TAX2  ='SGST' then TSPL_JOBWORK_BILLING_HEAD.TAX2_Amt else 0 end ValDtlsSgstVal,case when TSPL_JOBWORK_BILLING_HEAD .TAX1 ='IGST' then TSPL_JOBWORK_BILLING_HEAD.TAX1_Amt else 0 end ValDtlsIgstVal,0 as ValDtlsDiscount,case when isnull(TCS1.is_tcs,'')='Y' THEN  TSPL_JOBWORK_BILLING_HEAD.TAX2_AMT when isnull(TCS2.is_tcs,'')='Y' THEN  TSPL_JOBWORK_BILLING_HEAD.TAX3_AMT ELSE 0 END as ValDtlsOthChrg,TSPL_JOBWORK_BILLING_HEAD.Doc_Amt as ValDtlsTotInvVal,TSPL_JOBWORK_BILLING_HEAD.RoundOffAmount  as ValDtlsRndOffAmt
from TSPL_JOBWORK_BILLING_HEAD
Left Outer Join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No =TSPL_JOBWORK_BILLING_HEAD.Document_Code
Left Outer Join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code  ='" & objCommonVar.CurrentCompanyCode & "'
Left Outer Join TSPL_Customer_master on TSPL_Customer_master.Cust_Code  =TSPL_JOBWORK_BILLING_HEAD.Cust_Code
left Outer Join TSPL_LOCATION_MASTER as Bill_To_Location on Bill_To_Location.Location_Code =TSPL_JOBWORK_BILLING_HEAD.Billing_Loc_Code 
left outer join TSPL_JOBWORK_BILLING_DETAIL on TSPL_JOBWORK_BILLING_DETAIL.document_code=TSPL_JOBWORK_BILLING_HEAD.document_code
left outer join tspl_item_master on tspl_item_master.Item_code=TSPL_JOBWORK_BILLING_DETAIL.Item_code
left outer join TSPL_STATE_MASTER as BillToLocation_State_Master on BillToLocation_State_Master.STATE_CODE  =Bill_To_Location.State
left outer join TSPL_STATE_MASTER as Customer_State_Master on Customer_State_Master.STATE_CODE  =TSPL_Customer_master.State 
left outer join tspl_city_master on tspl_city_master.city_code=TSPL_Customer_master.City_Code
left outer join tspl_tax_master as TCS1 on TCS1.Tax_Code =TSPL_JOBWORK_BILLING_HEAD.Tax2
left outer join tspl_tax_master as TCS2 on TCS2.Tax_Code =TSPL_JOBWORK_BILLING_HEAD.Tax3
where TSPL_JOBWORK_BILLING_HEAD.Document_Code ='" & strDocNo & "'"


                Dim objResult As Object = ClsEInvoiceOFAPIs.PostAuthTokenNo_withInvoiceData(objCommonVar.CurrentCompanyCode, strtoken, strQry, strLocation, trans, True)
                If objResult IsNot Nothing Then
                    'assign to variable
                    Dim AckNo As String = objResult.SelectToken("AckNo").ToString
                    Dim AckDt As String = objResult.SelectToken("AckDt").ToString
                    Dim Irn As String = objResult.SelectToken("Irn").ToString
                    Dim SignedQRCode As String = objResult.SelectToken("SignedQRCode").ToString
                    clsDBFuncationality.ExecuteNonQuery("update TSPL_JOBWORK_BILLING_HEAD set  IRN_No ='" & Irn & "',qr_code='" & SignedQRCode & "',ack_no='" & AckNo & "',ack_date='" & clsCommon.GetPrintDate(AckDt, "dd/MMM/yyyy hh:mm tt") & "' where TSPL_JOBWORK_BILLING_HEAD.Document_Code ='" & strDocNo & "'", trans)

                    Dim TempByte As Byte() = clsERPFuncationality.GenerateMyQCCode(SignedQRCode)
                    clsDBFuncationality.UpdateImage("BarCode_Img", TempByte, "TSPL_JOBWORK_BILLING_HEAD", "TSPL_JOBWORK_BILLING_HEAD.Document_Code='" & strDocNo & "'", trans)
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim GSTStatus As Boolean = False

        Try
            Dim isSaved As Boolean = True
            Dim isscrap As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
           
            Dim obj As ClsJobWorkBilling = ClsJobWorkBilling.GetData(strDocNo, NavigatorType.Current, trans, False)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Inward", "JobWork Billing", obj.Loc_Code, obj.Document_Date, trans)

            If (obj.Status = 1) Then
                Throw New Exception("Already Post  :")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Shipment No " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If

            'Sanjay 02/July/2018 Check Tax Group
            GSTStatus = clsERPFuncationality.GetGSTStatus(obj.Document_Date)
            If (obj.Is_Taxable = True) AndAlso (clsCommon.myLen(obj.Tax_Group) <= 0) Then
                Throw New Exception("Tax Group not found :" + "Shipment No " + obj.Document_Code)
            End If
            If GSTStatus = True AndAlso obj.Is_Taxable = True Then
                'clsLocationWiseTax.IsValidTaxGroup(obj.Tax_Group, obj.Loc_Code, obj.cust_Code, "S", obj.Document_Date, trans)
                clsLocationWiseTax.IsValidTaxGroup(obj.Tax_Group, obj.Billing_Loc_Code, obj.cust_Code, "S", obj.Document_Date, trans)
            End If

            Dim ECustomerType = clsERPFuncationality.GetCustomerEInvoiceType(obj.cust_Code, trans)

            Dim qry = "Update TSPL_JOBWORK_BILLING_HEAD set Status=1,Modify_By='" + objCommonVar.CurrentUserCode + "',Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',EInvoice_Type='" + ECustomerType + "'"
            qry += " where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_JOBWORK_BILLING_HEAD", "Document_Code", trans)
            '---------------------For AR-Invoice Entry-----------------------------

            Dim Auto_Gen_No As Boolean = True

            Dim objCust As New clsCustomerInvoiceHead()
            ' objCust.Document_No = "'"
           
            objCust.Document_Date = obj.Document_Date
            objCust.Customer_Code = obj.cust_Code
            objCust.Customer_Name = obj.cust_Name
            'objCust.loc_code = obj.Loc_Code
            ' objCust.loc_code = clsLocation.GetSegmentCode(obj.Loc_Code, trans)
            objCust.loc_code = clsLocation.GetSegmentCode(obj.Billing_Loc_Code, trans)
            objCust.Account_Set = clsDBFuncationality.getSingleValue("select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.cust_Code + "'", trans)
            objCust.Document_Type = "I"
            objCust.Order_No = ""
            objCust.On_Hold = "0"
            objCust.Remarks = ""
            objCust.Description = ""
            objCust.Tax_Group = obj.Tax_Group
            objCust.RefDocType = ""
            objCust.RefDocNo = ""

            objCust.TAX1 = obj.TAX1
            objCust.TAX1_Rate = obj.TAX1_Rate
            objCust.Tax1_BAmount = obj.TAX1_Base_Amt
            objCust.TAX1_Amt = obj.TAX1_Amt

            objCust.TAX2 = obj.TAX2
            objCust.TAX2_Rate = obj.TAX2_Rate
            objCust.Tax2_BAmount = obj.TAX2_Base_Amt
            objCust.TAX2_Amt = obj.TAX2_Amt

            objCust.TAX3 = obj.TAX3
            objCust.TAX3_Rate = obj.TAX3_Rate
            objCust.Tax3_BAmount = obj.TAX3_Base_Amt
            objCust.TAX3_Amt = obj.TAX3_Amt

            objCust.TAX4 = obj.TAX4
            objCust.TAX4_Rate = obj.TAX4_Rate
            objCust.Tax4_BAmount = obj.TAX4_Base_Amt
            objCust.TAX4_Amt = obj.TAX4_Amt

            objCust.TAX5 = obj.TAX5
            objCust.TAX5_Rate = obj.TAX5_Rate
            objCust.Tax5_BAmount = obj.TAX5_Base_Amt
            objCust.TAX5_Amt = obj.TAX5_Amt

            objCust.TAX6 = obj.TAX6
            objCust.TAX6_Rate = obj.TAX6_Rate
            objCust.Tax6_BAmount = obj.TAX6_Base_Amt
            objCust.TAX6_Amt = obj.TAX6_Amt

            objCust.TAX7 = obj.TAX7
            objCust.TAX7_Rate = obj.TAX7_Rate
            objCust.Tax7_BAmount = obj.TAX7_Base_Amt
            objCust.TAX7_Amt = obj.TAX7_Amt

            objCust.TAX8 = obj.TAX8
            objCust.TAX8_Rate = obj.TAX8_Rate
            objCust.Tax8_BAmount = obj.TAX8_Base_Amt
            objCust.TAX8_Amt = obj.TAX8_Amt

            objCust.TAX9 = obj.TAX9
            objCust.TAX9_Rate = obj.TAX9_Rate
            objCust.Tax9_BAmount = obj.TAX9_Base_Amt
            objCust.TAX9_Amt = obj.TAX9_Amt

            objCust.TAX10 = obj.TAX10
            objCust.TAX10_Rate = obj.TAX10_Rate
            objCust.Tax10_BAmount = obj.TAX10_Base_Amt
            objCust.TAX10_Amt = obj.TAX10_Amt
            objCust.Terms_Code = obj.Terms_Code
            objCust.Terms_Description = clsDBFuncationality.getSingleValue("select Terms_Desc from TSPL_TERMS_MASTER where Terms_Code='" + obj.Terms_Code + "'", trans)
            objCust.Discount_Percentage = 0
            objCust.Discount_Base = obj.Amount
            objCust.Discount_Amount = 0
            objCust.Amount_Less_Discount = 0
            objCust.Total_Tax = obj.Total_Tax_Amt
            objCust.Document_Total = obj.doc_Amt
            objCust.RoundOffAmount = obj.RoundOffAmount
            objCust.Comp_Code = obj.Comp_Code
            objCust.Balance_Amt = obj.doc_Amt

            ''richa agarwal 25/06/2015 change location of account against BM00000007177
            '  objCust.Customer_Control_AC = clsDBFuncationality.getSingleValue("select Receivable_Control_acct from TSPL_CUSTOMER_ACCOUNT_SET left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account where TSPL_CUSTOMER_MASTER.Cust_Code='" + obj.cust_Code + "'", trans)
            Dim strCustomerCntrlAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receivable_Control_acct from TSPL_CUSTOMER_ACCOUNT_SET left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account=TSPL_CUSTOMER_MASTER.Cust_Account where TSPL_CUSTOMER_MASTER.Cust_Code='" + obj.cust_Code + "'", trans))
            '  objCust.Customer_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(strCustomerCntrlAcc, obj.Loc_Code, trans)
            objCust.Customer_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(strCustomerCntrlAcc, obj.Billing_Loc_Code, trans)
            '--------------------------------
          
            objCust.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
            objCust.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
            objCust.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
            objCust.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
            objCust.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
            objCust.TAX6_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
            objCust.TAX7_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
            objCust.TAX8_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
            objCust.TAX9_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
            objCust.TAX10_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
            
            objCust.Balance_Amt = 0
            objCust.Tax_Calculation_Type = "0"
            objCust.AgainstScrap = obj.Document_Code
            objCust.Arr = New List(Of clsCustomerInvoiceDetail)
            For Each objout As ClsJobWorkBillingDetail In obj.Arr
                Dim objtr As New clsCustomerInvoiceDetail()
                'objtr.invoice_No = invoice
                objtr.SNo = objout.Line_No
                ''richa agarwal 25/06/2015 change location of account against BM00000007177
                'objtr.GL_Account_Code = clsDBFuncationality.getSingleValue("select TSPL_SALES_ACCOUNTS.Sales_Account  from TSPL_SALES_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objout.Item_Code + "'", trans)
                'objtr.GL_Account_Desc = clsDBFuncationality.getSingleValue("select TSPL_SALES_ACCOUNTS.Sales_Class_Desc  from TSPL_SALES_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objout.Item_Code + "'", trans)
                Dim strGLAcc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_SALES_ACCOUNTS.Sales_Account  from TSPL_SALES_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Sale_Class_Code=TSPL_SALES_ACCOUNTS.Sales_Class_Code where TSPL_ITEM_MASTER.Item_Code='" + objout.Item_Code + "'", trans))
                'objtr.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(strGLAcc, obj.Loc_Code, trans)
                objtr.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(strGLAcc, obj.Billing_Loc_Code, trans)
                objtr.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(objtr.GL_Account_Code) + "'", trans))
                objtr.Reco_Control_Account = "S"
                ''-------------------------------------
                objtr.Amount = objout.ItemAmt
                objtr.Discount_Per = 0
                objtr.Discount = 0
                objtr.Amount_less_Discount = objout.ItemAmt

                objtr.TAX1 = objout.TAX1
                objtr.TAX1_Rate = objout.TAX1_Rate
                objtr.TAX1_Amt = objout.TAX1_Amt
                objtr.TAX1_Base_Amt = objout.TAX1_Base_Amt


                objtr.TAX2 = objout.TAX2
                objtr.TAX2_Rate = objout.TAX2_Rate
                objtr.TAX2_Amt = objout.TAX2_Amt
                objtr.TAX2_Base_Amt = objout.TAX2_Base_Amt

                objtr.TAX3 = objout.TAX3
                objtr.TAX3_Rate = objout.TAX3_Rate
                objtr.TAX3_Amt = objout.TAX3_Amt
                objtr.TAX3_Base_Amt = objout.TAX3_Base_Amt

                objtr.TAX4 = objout.TAX4
                objtr.TAX4_Rate = objout.TAX4_Rate
                objtr.TAX4_Amt = objout.TAX4_Amt
                objtr.TAX4_Base_Amt = objout.TAX4_Base_Amt

                objtr.TAX5 = objout.TAX5
                objtr.TAX5_Rate = objout.TAX5_Rate
                objtr.TAX5_Amt = objout.TAX5_Amt
                objtr.TAX5_Base_Amt = objout.TAX4_Base_Amt

                objtr.TAX6 = objout.TAX6
                objtr.TAX6_Rate = objout.TAX6_Rate
                objtr.TAX6_Amt = objout.TAX6_Amt
                objtr.TAX6_Base_Amt = objout.TAX6_Base_Amt

                objtr.TAX7 = objout.TAX7
                objtr.TAX7_Rate = objout.TAX7_Rate
                objtr.TAX7_Amt = objout.TAX7_Amt
                objtr.TAX7_Base_Amt = objout.TAX7_Base_Amt

                objtr.TAX8 = objout.TAX8
                objtr.TAX8_Rate = objout.TAX8_Rate
                objtr.TAX8_Amt = objout.TAX8_Amt
                objtr.TAX8_Base_Amt = objout.TAX8_Base_Amt

                objtr.TAX9 = objout.TAX9
                objtr.TAX9_Rate = objout.TAX9_Rate
                objtr.TAX9_Amt = objout.TAX9_Amt
                objtr.TAX9_Base_Amt = objout.TAX9_Base_Amt

                objtr.TAX10 = objout.TAX10
                objtr.TAX10_Rate = objout.TAX10_Rate
                objtr.TAX10_Amt = objout.TAX10_Amt
                objtr.TAX10_Base_Amt = objout.TAX10_Base_Amt

                objtr.Total_Tax = objout.TotalTaxAmt
                objtr.Total_Amount = objout.TotalAmt
                objtr.Remarks = ""
                objtr.Comments = ""
                objCust.Arr.Add(objtr)
            Next

            objCust.SaveData(objCust, Auto_Gen_No, trans, "", "", "")
            clsCustomerInvoiceHead.PostData("", objCust.Document_No, "", trans)
            ''------------------------------
            ''richa agarwal 31 Dec,2020 check eInvoice Implementation
            If clsCommon.CompairString(ECustomerType, "BB") = CompairStringResult.Equal AndAlso obj.Is_Taxable = True AndAlso clsERPFuncationality.GetEInvoiceStatus(obj.Document_Date, trans) = True Then
                If ClsJobWorkBilling.EInvoice_Implementation(obj.Document_Code, obj.Billing_Loc_Code, trans) = True Then
                Else
                    Throw New Exception("Invalid JSON Value")
                End If
            End If


            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document not found to Delete")
        End If
        Dim obj As ClsJobWorkBilling = ClsJobWorkBilling.GetData(strCode, NavigatorType.Current)
        Dim frm As New FrmFreeTxtBox1
        frm.Text = "Remarks for Delete"
        frm.ShowDialog()
        If clsCommon.myLen(frm.strRmks) <= 0 Then
            Return False
        End If


        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Inward", "JobWork Billing", obj.Loc_Code, obj.Document_Date, trans)

                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :")
                End If
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_JOBWORK_BILLING_HEAD", "Document_Code", "TSPL_JOBWORK_BILLING_DETAIL", "Document_Code", trans)
                Dim qry As String = "delete from TSPL_JOBWORK_BILLING_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "Delete from TSPL_JOBWORK_BILLING_HEAD  where Document_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)

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

    Public Shared Function GetVehicleDesc(ByVal strVehicleId As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "Select Description from TSPL_GL_SEGMENT_CODE where Seg_No=2 AND Segment_code='" + strVehicleId + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    '=====Sanjeet (Round Off Calculation)===

    Public Shared Function Calculate_RoundOffAmt(ByVal DocumentAmt As Decimal, ByVal trans As SqlTransaction) As List(Of Decimal)
        Dim LstDecml As New List(Of Decimal)
        Try
            Dim proundoff As Decimal = 0
            Dim amt As Decimal = 0

            If DocumentAmt > 0 Then
                Dim strPreAmt As Decimal = 0
                Dim strPostAmt As Decimal = 0

                If clsCommon.myCstr(DocumentAmt).Contains(".") Then
                    strPreAmt = clsCommon.myCdbl(clsCommon.myCstr(DocumentAmt).Substring(0, clsCommon.myCstr(DocumentAmt).IndexOf(".")))
                    strPostAmt = clsCommon.myCdbl(DocumentAmt - strPreAmt) * 100
                Else
                    strPreAmt = DocumentAmt
                    strPostAmt = 0
                End If

                If strPostAmt >= 50 Then
                    strPreAmt = strPreAmt + 1
                    ' strPostAmt = System.Math.Round((strPostAmt - 100) / 100, 2)
                    strPostAmt = System.Math.Round((100 - strPostAmt) / 100, 2)

                    LstDecml.Add(strPreAmt)
                    LstDecml.Add(strPostAmt)
                ElseIf strPostAmt < 50 Then
                    strPreAmt = strPreAmt
                    'strPostAmt = System.Math.Round(strPostAmt / 100, 2)
                    strPostAmt = System.Math.Round((strPostAmt * -1) / 100, 2)

                    LstDecml.Add(strPreAmt)
                    LstDecml.Add(strPostAmt)
                End If
            End If

            Return LstDecml
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class ClsJobWorkBillingDetail
#Region "Variables"
    Public JW_EstimationNo As String = Nothing
    Public ItemwiseTaxCode As String = Nothing
    Public Document_Code As String = Nothing
    Public Specification As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Invoice_Qty As Double = 0
    Public ConvKG_Qty As Double = 0
    Public ConvKG_Price As Double = 0
    Public price As Double = 0
    Public JWI_Price_Code As String = Nothing
    Public ItemAmt As String = Nothing
    Public TotalTaxAmt As Double = 0
    Public TotalAmt As Double = 0 '
    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Description As String = Nothing   'Additiona charges description  
    Public Unit_Code As String = ""
    Public FATKg As Double = 0
    Public SNFKg As Double = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsJobWorkBillingDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsJobWorkBillingDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Invoice_Qty", obj.Invoice_Qty)
                clsCommon.AddColumnsForChange(coll, "price", obj.price)
                clsCommon.AddColumnsForChange(coll, "JWI_Price_Code", obj.JWI_Price_Code, True)
                clsCommon.AddColumnsForChange(coll, "ConvKG_Qty", obj.ConvKG_Qty)
                clsCommon.AddColumnsForChange(coll, "ConvKG_Price", obj.ConvKG_Price)
                clsCommon.AddColumnsForChange(coll, "ItemAmt", obj.ItemAmt)
                clsCommon.AddColumnsForChange(coll, "TotalTaxAmt", obj.TotalTaxAmt)
                clsCommon.AddColumnsForChange(coll, "TotalAmt", obj.TotalAmt)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                'clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "FATKg", obj.FATKg)
                clsCommon.AddColumnsForChange(coll, "SNFkg", obj.SNFKg)
                clsCommon.AddColumnsForChange(coll, "ItemwiseTaxCode", obj.ItemwiseTaxCode)
                clsCommon.AddColumnsForChange(coll, "JW_EstimationNo", obj.JW_EstimationNo)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_JOBWORK_BILLING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class








