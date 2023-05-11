'--Updations By [Pankaj Kumar Chaudhary]-Against Ticket No -[BM00000001707]
Imports common
Imports System.Data.SqlClient
Public Class clsRecurringPayableInvoice
#Region "Variables"
    Shared sQuery As String = String.Empty
    Public PROJECT_ID As String = Nothing
    Public Document_No As String = Nothing
    Public Invoice_Entry_Date As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Vendor_Invoice_No As String = Nothing
    Public Vendor_Invoice_Date As String = Nothing
    Public Posting_Date As String = Nothing
    Public Is_cancelled As String = String.Empty
    Public Account_Set As String = Nothing
    Public Document_Type As String = Nothing
    Public PO_Number As String = Nothing
    Public Order_No As String = Nothing
    Public Document_Total As Double = 0
    Public On_Hold As Boolean = Nothing
    Public Remarks As String = Nothing
    Public Description As String = Nothing
    Public Tax_Group As String = Nothing
    ''''added by priti
    Public RefDocType As String = Nothing
    Public RefDocNo As String = Nothing
    Public Against_MillkPurchaseInvoice_No As String = Nothing
    Public Against_VSPItemIssue_No As String = Nothing
    Public Against_BulkMillkPurchaseInvoice_No As String = Nothing
    '''' ends here
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public Tax1_BAmount As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public Tax2_BAmount As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public Tax3_BAmount As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public Tax4_BAmount As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public Tax5_BAmount As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public Tax6_BAmount As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public Tax7_BAmount As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public Tax8_BAmount As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public Tax9_BAmount As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public Tax10_BAmount As Double = 0
    Public TAX10_Amt As Double = 0
    Public Total_Tax As Double = 0
    Public Terms_Code As String = Nothing
    Public Terms_Description As String = Nothing
    Public Due_Date As String = Nothing
    Public Discount_Percentage As Double = 0
    Public Discount_Base As Double = 0
    Public Discount_Amount As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public TDS_Base_Actual_Amount As Double = 0
    Public TDS_Base_Calculated_Amount As Double = 0
    Public TDS_Percentage As Double = 0
    Public TDS_Actual_Amount As Double = 0
    Public TDS_Calculated_Amount As Double = 0
    Public Nature_of_deduction As String = Nothing
    Public Branch_Code As String = Nothing
    Public Section_Code As String = Nothing
    'Public Created_By As String = Nothing
    'Public Created_Date As String = Nothing
    'Public Modify_By As String = Nothing
    'Public Modify_Date As String = Nothing
    Public Comp_Code As String = Nothing
    Public Balance_Amt As Double = 0
    Public Arr As List(Of clsRecurringPayableInvoiceDetail) = Nothing
    Public RemittanceObject As clsRemittance = Nothing

    Public Vendor_Control_AC As String = Nothing
    Public Discount_GL_AC As String = Nothing
    Public TAX1_GLAC As String = Nothing
    Public TAX2_GLAC As String = Nothing
    Public TAX3_GLAC As String = Nothing
    Public TAX4_GLAC As String = Nothing
    Public TAX5_GLAC As String = Nothing
    Public TAX6_GLAC As String = Nothing
    Public TAX7_GLAC As String = Nothing
    Public TAX8_GLAC As String = Nothing
    Public TAX9_GLAC As String = Nothing
    Public TAX10_GLAC As String = Nothing
    Public Against_POInvoice_No As String = Nothing
    Public Against_PurchaseReturn_No As String = Nothing

    Public Against_Acquisition As String = Nothing

    Public Add_Charge_Code1 As String = Nothing
    Public Add_Charge_Name1 As String = Nothing
    Public Add_Charge_Amt1 As Double = 0
    Public Add_Charge_Code2 As String = Nothing
    Public Add_Charge_Name2 As String = Nothing
    Public Add_Charge_Amt2 As Double = 0
    Public Add_Charge_Code3 As String = Nothing
    Public Add_Charge_Name3 As String = Nothing
    Public Add_Charge_Amt3 As Double = 0
    Public Add_Charge_Code4 As String = Nothing
    Public Add_Charge_Name4 As String = Nothing
    Public Add_Charge_Amt4 As Double = 0
    Public Add_Charge_Code5 As String = Nothing
    Public Add_Charge_Name5 As String = Nothing
    Public Add_Charge_Amt5 As Double = 0
    Public Add_Charge_Code6 As String = Nothing
    Public Add_Charge_Name6 As String = Nothing
    Public Add_Charge_Amt6 As Double = 0
    Public Add_Charge_Code7 As String = Nothing
    Public Add_Charge_Name7 As String = Nothing
    Public Add_Charge_Amt7 As Double = 0
    Public Add_Charge_Code8 As String = Nothing
    Public Add_Charge_Name8 As String = Nothing
    Public Add_Charge_Amt8 As Double = 0
    Public Add_Charge_Code9 As String = Nothing
    Public Add_Charge_Name9 As String = Nothing
    Public Add_Charge_Amt9 As Double = 0
    Public Add_Charge_Code10 As String = Nothing
    Public Add_Charge_Name10 As String = Nothing
    Public Add_Charge_Amt10 As Double = 0
    Public Total_Add_Charge As Double = 0
    Public loc_code As String = Nothing
    Public Irregular_loc_code As String = Nothing
    Public Tax_Calculation_Type As EnumTaxCalucationType

    Public TAX1_GLAC_Amt As Double = 0
    Public TAX1_GLAC2 As String = Nothing
    Public TAX1_GLAC2_Amt As Double = 0
    Public TAX1_GLAC3 As String = Nothing
    Public TAX1_GLAC3_Amt As Double = 0
    Public TAX1_GLAC4 As String = Nothing
    Public TAX1_GLAC4_Amt As Double = 0
    Public TAX1_GLAC5 As String = Nothing
    Public TAX1_GLAC5_Amt As Double = 0

    Public TAX2_GLAC_Amt As Double = 0
    Public TAX2_GLAC2 As String = Nothing
    Public TAX2_GLAC2_Amt As Double = 0
    Public TAX2_GLAC3 As String = Nothing
    Public TAX2_GLAC3_Amt As Double = 0
    Public TAX2_GLAC4 As String = Nothing
    Public TAX2_GLAC4_Amt As Double = 0
    Public TAX2_GLAC5 As String = Nothing
    Public TAX2_GLAC5_Amt As Double = 0

    Public TAX3_GLAC_Amt As Double = 0
    Public TAX3_GLAC2 As String = Nothing
    Public TAX3_GLAC2_Amt As Double = 0
    Public TAX3_GLAC3 As String = Nothing
    Public TAX3_GLAC3_Amt As Double = 0
    Public TAX3_GLAC4 As String = Nothing
    Public TAX3_GLAC4_Amt As Double = 0
    Public TAX3_GLAC5 As String = Nothing
    Public TAX3_GLAC5_Amt As Double = 0

    Public TAX4_GLAC_Amt As Double = 0
    Public TAX4_GLAC2 As String = Nothing
    Public TAX4_GLAC2_Amt As Double = 0
    Public TAX4_GLAC3 As String = Nothing
    Public TAX4_GLAC3_Amt As Double = 0
    Public TAX4_GLAC4 As String = Nothing
    Public TAX4_GLAC4_Amt As Double = 0
    Public TAX4_GLAC5 As String = Nothing
    Public TAX4_GLAC5_Amt As Double = 0

    Public TAX5_GLAC_Amt As Double = 0
    Public TAX5_GLAC2 As String = Nothing
    Public TAX5_GLAC2_Amt As Double = 0
    Public TAX5_GLAC3 As String = Nothing
    Public TAX5_GLAC3_Amt As Double = 0
    Public TAX5_GLAC4 As String = Nothing
    Public TAX5_GLAC4_Amt As Double = 0
    Public TAX5_GLAC5 As String = Nothing
    Public TAX5_GLAC5_Amt As Double = 0

    Public TAX6_GLAC_Amt As Double = 0
    Public TAX6_GLAC2 As String = Nothing
    Public TAX6_GLAC2_Amt As Double = 0
    Public TAX6_GLAC3 As String = Nothing
    Public TAX6_GLAC3_Amt As Double = 0
    Public TAX6_GLAC4 As String = Nothing
    Public TAX6_GLAC4_Amt As Double = 0
    Public TAX6_GLAC5 As String = Nothing
    Public TAX6_GLAC5_Amt As Double = 0

    Public TAX7_GLAC_Amt As Double = 0
    Public TAX7_GLAC2 As String = Nothing
    Public TAX7_GLAC2_Amt As Double = 0
    Public TAX7_GLAC3 As String = Nothing
    Public TAX7_GLAC3_Amt As Double = 0
    Public TAX7_GLAC4 As String = Nothing
    Public TAX7_GLAC4_Amt As Double = 0
    Public TAX7_GLAC5 As String = Nothing
    Public TAX7_GLAC5_Amt As Double = 0

    Public TAX8_GLAC_Amt As Double = 0
    Public TAX8_GLAC2 As String = Nothing
    Public TAX8_GLAC2_Amt As Double = 0
    Public TAX8_GLAC3 As String = Nothing
    Public TAX8_GLAC3_Amt As Double = 0
    Public TAX8_GLAC4 As String = Nothing
    Public TAX8_GLAC4_Amt As Double = 0
    Public TAX8_GLAC5 As String = Nothing
    Public TAX8_GLAC5_Amt As Double = 0

    Public TAX9_GLAC_Amt As Double = 0
    Public TAX9_GLAC2 As String = Nothing
    Public TAX9_GLAC2_Amt As Double = 0
    Public TAX9_GLAC3 As String = Nothing
    Public TAX9_GLAC3_Amt As Double = 0
    Public TAX9_GLAC4 As String = Nothing
    Public TAX9_GLAC4_Amt As Double = 0
    Public TAX9_GLAC5 As String = Nothing
    Public TAX9_GLAC5_Amt As Double = 0

    Public TAX10_GLAC_Amt As Double = 0
    Public TAX10_GLAC2 As String = Nothing
    Public TAX10_GLAC2_Amt As Double = 0
    Public TAX10_GLAC3 As String = Nothing
    Public TAX10_GLAC3_Amt As Double = 0
    Public TAX10_GLAC4 As String = Nothing
    Public TAX10_GLAC4_Amt As Double = 0
    Public TAX10_GLAC5 As String = Nothing
    Public TAX10_GLAC5_Amt As Double = 0
    Public Empty_Amount As Double = 0
    Public Empty_Account As String = Nothing
    Public Total_Landed_Amt As Double = 0
    Public is_For_TDS As Integer = 0

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public Is_ProRated As Char = "N"
    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public Invoice_Type As String = Nothing

    Public RoundOffAmount As Double = 0

    Public is_For_Provision As Integer = 0
    Public Prov_From_Date As Date
    Public Prov_To_Date As Date
    Public Prov_Amt As Double = 0
    Public arrProvDocNo As List(Of String) = Nothing
    Public Security As Integer
    Public isDeduction As Integer
    Public Scheduler_Code As String = Nothing
    Public Expiration_date As String = Nothing
    Public Expiration_Type As String = Nothing
    Public Expiration_Amount As Double = 0

#End Region

    Public Function SaveData(ByVal obj As clsRecurringPayableInvoice, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = obj.SaveData(obj, isNewEntry, trans)
            If isSaved AndAlso obj.is_For_Provision = 1 Then
                isSaved = isSaved AndAlso clsProvisionEntry.SaveData(obj.Document_No, arrProvDocNo, trans)
            End If
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

    Public Shared Function Change_Last_Invoice_date(ByVal Doc_Code As String, ByVal Last_Invoice_date As Date, ByVal trans As SqlTransaction) As Boolean
        Try
            sQuery = "update TSPL_Recurring_Payable_INVOICE_HEAD set Last_Invoice_Date='" & Last_Invoice_date & "' where document_No='" & Doc_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
            Return False
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsRecurringPayableInvoice, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        If clsCommon.myLen(obj.loc_code) <= 0 Then
            Throw New Exception("Please first select Location")
        End If

        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement MCC", "Milk Recurring Payable Invoice", obj.loc_code, clsCommon.myCDate(obj.Invoice_Entry_Date), trans)
        Dim qry As String = "delete from tspl_Recurring_payable_invoice_detail where Document_No='" + obj.Document_No + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        'qry = "delete from TSPL_REMITTANCE where Document_No='" + obj.Document_No + "'"
        'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        If obj.Arr.Count <= 0 Then
            Throw New Exception("Please fill at list one Account")
        End If
        Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Arr(0).GL_Account_Code + "'", trans))
        If clsCommon.myLen(strLocation) <= 0 Then
            Throw New Exception("Please enter account with location segment")
        End If


        Dim strDocNo As String = ""
        If (isNewEntry) Then
            ' If (clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal) Then
            obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Invoice_Entry_Date), clsDocType.MilkTransportorInvoice, "", strLocation, True)
            'ElseIf (clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal) Then
            '    obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Invoice_Entry_Date), clsDocType.MilkTransportorInvoice, "", strLocation, True)
            'ElseIf (clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal) Then
            '    obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Invoice_Entry_Date), clsDocType.MilkTransportorInvoice, "", strLocation, True)
            'End If
        End If
        If (clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Invoice_Entry_Date", clsCommon.GetPrintDate(obj.Invoice_Entry_Date, "dd/MM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
        clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
        clsCommon.AddColumnsForChange(coll, "Vendor_Invoice_No", obj.Vendor_Invoice_No)
        clsCommon.AddColumnsForChange(coll, "Vendor_Invoice_Date", clsCommon.GetPrintDate(obj.Vendor_Invoice_Date, "dd/MM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Account_Set", obj.Account_Set)
        clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
        clsCommon.AddColumnsForChange(coll, "PO_Number", obj.PO_Number)
        '------addesd by usha
        clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.loc_code)
        clsCommon.AddColumnsForChange(coll, "IRregular_loc_code", obj.Irregular_loc_code)
        '------------
        '''''added by priit 
        clsCommon.AddColumnsForChange(coll, "RefDocType", obj.RefDocType)
        clsCommon.AddColumnsForChange(coll, "RefDocNo", obj.RefDocNo)
        clsCommon.AddColumnsForChange(coll, "Against_MillkPurchaseInvoice_No", obj.Against_MillkPurchaseInvoice_No, True)
        clsCommon.AddColumnsForChange(coll, "Against_VSPItemIssue_No", obj.Against_VSPItemIssue_No, True)
        clsCommon.AddColumnsForChange(coll, "Against_BulkMillkPurchaseInvoice_No", obj.Against_BulkMillkPurchaseInvoice_No, True)
        '''' ends here
        clsCommon.AddColumnsForChange(coll, "Order_No", obj.Order_No)
        clsCommon.AddColumnsForChange(coll, "Document_Total", obj.Document_Total)
        clsCommon.AddColumnsForChange(coll, "Prov_Amt", 0)
        clsCommon.AddColumnsForChange(coll, "RoundOffAmount", clsCommon.myCdbl(obj.RoundOffAmount))
        Dim strOnHold As String = "N"
        If (obj.On_Hold) Then
            strOnHold = "Y"
        End If
        clsCommon.AddColumnsForChange(coll, "is_For_TDS", obj.is_For_TDS)
        clsCommon.AddColumnsForChange(coll, "On_Hold", strOnHold)
        clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
        clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
        clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
        clsCommon.AddColumnsForChange(coll, "is_For_Provision", obj.is_For_Provision)
        clsCommon.AddColumnsForChange(coll, "isDeduction", obj.isDeduction)
        clsCommon.AddColumnsForChange(coll, "Scheduler_Code", obj.Scheduler_Code)
        clsCommon.AddColumnsForChange(coll, "Expiration_Type", obj.Expiration_Type)
        clsCommon.AddColumnsForChange(coll, "Expiration_date", obj.Expiration_date)
        clsCommon.AddColumnsForChange(coll, "Expiration_Amount", obj.Expiration_Amount)
        'If obj.isDeduction = 1 Then
        '    clsCommon.AddColumnsForChange(coll, "Deduction_Code", clsCommon.myCstr(obj.Deduction_Code))
        '    clsCommon.AddColumnsForChange(coll, "Deduction_Desc", clsCommon.myCstr(obj.Deduction_Desc))
        'End If
        clsCommon.AddColumnsForChange(coll, "Prov_From_Date", clsCommon.GetPrintDate(obj.Prov_From_Date, "dd/MMM/yyyy"))
        If obj.is_For_Provision = 1 Then

            clsCommon.AddColumnsForChange(coll, "Prov_To_Date", clsCommon.GetPrintDate(obj.Prov_To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Prov_Amt", clsCommon.myCdbl(obj.Prov_Amt))
        End If

        clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
        clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax1_BAmount", obj.Tax1_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
        clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax2_BAmount", obj.Tax2_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
        clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax3_BAmount", obj.Tax3_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
        clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax4_BAmount", obj.Tax4_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
        clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax5_BAmount", obj.Tax5_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
        clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax6_BAmount", obj.Tax6_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
        clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax7_BAmount", obj.Tax7_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
        clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax8_BAmount", obj.Tax8_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
        clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax9_BAmount", obj.Tax9_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
        clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
        clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
        clsCommon.AddColumnsForChange(coll, "Tax10_BAmount", obj.Tax10_BAmount)
        clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
        clsCommon.AddColumnsForChange(coll, "Total_Tax", obj.Total_Tax)
        If clsCommon.myLen(obj.Against_MillkPurchaseInvoice_No) <= 0 Then
            obj.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Vendor_Control_AC, strLocation, True, trans)
        End If
        clsCommon.AddColumnsForChange(coll, "Vendor_Control_AC", obj.Vendor_Control_AC, True)
        obj.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.Discount_GL_AC, strLocation, True, trans)
        clsCommon.AddColumnsForChange(coll, "Discount_GL_AC", obj.Discount_GL_AC, True)
        clsCommon.AddColumnsForChange(coll, "Empty_Amount", obj.Empty_Amount)
        clsCommon.AddColumnsForChange(coll, "Empty_Account", obj.Empty_Account, True)
        clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
        clsCommon.AddColumnsForChange(coll, "Invoice_Type", obj.Invoice_Type)
        clsCommon.AddColumnsForChange(coll, "Is_Security", obj.Security)

        UpdateAllTax(obj.Document_No, trans)    '---This Function Updates All TaxGLAccount='' And TaxGLAmt=0

        Dim objTM As clsTaxMaster = clsTaxMaster.GetData(obj.TAX1, trans)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX1_GLAC", objTM.Tax_Recoverable_Account, True)
                clsCommon.AddColumnsForChange(coll, "TAX1_GLAC_Amt", Math.Round(obj.TAX1_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                objTM.Tax_Recoverable_Account2 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX1_GLAC2", objTM.Tax_Recoverable_Account2, True)
                clsCommon.AddColumnsForChange(coll, "TAX1_GLAC2_Amt", Math.Round(obj.TAX1_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                objTM.Tax_Recoverable_Account3 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX1_GLAC3", objTM.Tax_Recoverable_Account3, True)
                clsCommon.AddColumnsForChange(coll, "TAX1_GLAC3_Amt", Math.Round(obj.TAX1_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                objTM.Tax_Recoverable_Account4 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX1_GLAC4", objTM.Tax_Recoverable_Account4, True)
                clsCommon.AddColumnsForChange(coll, "TAX1_GLAC4_Amt", Math.Round(obj.TAX1_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                objTM.Tax_Recoverable_Account5 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX1_GLAC5", objTM.Tax_Recoverable_Account5, True)
                clsCommon.AddColumnsForChange(coll, "TAX1_GLAC5_Amt", Math.Round(obj.TAX1_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven))
            End If

        End If

        objTM = clsTaxMaster.GetData(obj.TAX2, trans)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX2_GLAC", objTM.Tax_Recoverable_Account, True)
                clsCommon.AddColumnsForChange(coll, "TAX2_GLAC_Amt", Math.Round(obj.TAX2_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                objTM.Tax_Recoverable_Account2 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX2_GLAC2", objTM.Tax_Recoverable_Account2, True)
                clsCommon.AddColumnsForChange(coll, "TAX2_GLAC2_Amt", Math.Round(obj.TAX2_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                objTM.Tax_Recoverable_Account3 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX2_GLAC3", objTM.Tax_Recoverable_Account3, True)
                clsCommon.AddColumnsForChange(coll, "TAX2_GLAC3_Amt", Math.Round(obj.TAX2_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                objTM.Tax_Recoverable_Account4 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX2_GLAC4", objTM.Tax_Recoverable_Account4, True)
                clsCommon.AddColumnsForChange(coll, "TAX2_GLAC4_Amt", Math.Round(obj.TAX2_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                objTM.Tax_Recoverable_Account5 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX2_GLAC5", objTM.Tax_Recoverable_Account5, True)
                clsCommon.AddColumnsForChange(coll, "TAX2_GLAC5_Amt", Math.Round(obj.TAX2_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven))
            End If
        End If
        objTM = clsTaxMaster.GetData(obj.TAX3, trans)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX3_GLAC", objTM.Tax_Recoverable_Account, True)
                clsCommon.AddColumnsForChange(coll, "TAX3_GLAC_Amt", Math.Round(obj.TAX3_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                objTM.Tax_Recoverable_Account2 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX3_GLAC2", objTM.Tax_Recoverable_Account2, True)
                clsCommon.AddColumnsForChange(coll, "TAX3_GLAC2_Amt", Math.Round(obj.TAX3_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                objTM.Tax_Recoverable_Account3 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX3_GLAC3", objTM.Tax_Recoverable_Account3, True)
                clsCommon.AddColumnsForChange(coll, "TAX3_GLAC3_Amt", Math.Round(obj.TAX3_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                objTM.Tax_Recoverable_Account4 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX3_GLAC4", objTM.Tax_Recoverable_Account4, True)
                clsCommon.AddColumnsForChange(coll, "TAX3_GLAC4_Amt", Math.Round(obj.TAX3_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                objTM.Tax_Recoverable_Account5 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX3_GLAC5", objTM.Tax_Recoverable_Account5, True)
                clsCommon.AddColumnsForChange(coll, "TAX3_GLAC5_Amt", Math.Round(obj.TAX3_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven))
            End If
        End If
        objTM = clsTaxMaster.GetData(obj.TAX4, trans)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX4_GLAC", objTM.Tax_Recoverable_Account, True)
                clsCommon.AddColumnsForChange(coll, "TAX4_GLAC_Amt", Math.Round(obj.TAX4_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                objTM.Tax_Recoverable_Account2 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX4_GLAC2", objTM.Tax_Recoverable_Account2, True)
                clsCommon.AddColumnsForChange(coll, "TAX4_GLAC2_Amt", Math.Round(obj.TAX4_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                objTM.Tax_Recoverable_Account3 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX4_GLAC3", objTM.Tax_Recoverable_Account3, True)
                clsCommon.AddColumnsForChange(coll, "TAX4_GLAC3_Amt", Math.Round(obj.TAX4_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                objTM.Tax_Recoverable_Account4 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX4_GLAC4", objTM.Tax_Recoverable_Account4, True)
                clsCommon.AddColumnsForChange(coll, "TAX4_GLAC4_Amt", Math.Round(obj.TAX4_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                objTM.Tax_Recoverable_Account5 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX4_GLAC5", objTM.Tax_Recoverable_Account5, True)
                clsCommon.AddColumnsForChange(coll, "TAX4_GLAC5_Amt", Math.Round(obj.TAX4_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven))
            End If
        End If
        objTM = clsTaxMaster.GetData(obj.TAX5, trans)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX5_GLAC", objTM.Tax_Recoverable_Account, True)
                clsCommon.AddColumnsForChange(coll, "TAX5_GLAC_Amt", Math.Round(obj.TAX5_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                objTM.Tax_Recoverable_Account2 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX5_GLAC2", objTM.Tax_Recoverable_Account2, True)
                clsCommon.AddColumnsForChange(coll, "TAX5_GLAC2_Amt", Math.Round(obj.TAX5_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                objTM.Tax_Recoverable_Account3 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX5_GLAC3", objTM.Tax_Recoverable_Account3, True)
                clsCommon.AddColumnsForChange(coll, "TAX5_GLAC3_Amt", Math.Round(obj.TAX5_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                objTM.Tax_Recoverable_Account4 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX5_GLAC4", objTM.Tax_Recoverable_Account4, True)
                clsCommon.AddColumnsForChange(coll, "TAX5_GLAC4_Amt", Math.Round(obj.TAX5_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                objTM.Tax_Recoverable_Account5 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX5_GLAC5", objTM.Tax_Recoverable_Account5, True)
                clsCommon.AddColumnsForChange(coll, "TAX5_GLAC5_Amt", Math.Round(obj.TAX5_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven))
            End If
        End If
        objTM = clsTaxMaster.GetData(obj.TAX6, trans)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX6_GLAC", objTM.Tax_Recoverable_Account, True)
                clsCommon.AddColumnsForChange(coll, "TAX6_GLAC_Amt", Math.Round(obj.TAX6_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                objTM.Tax_Recoverable_Account2 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX6_GLAC2", objTM.Tax_Recoverable_Account2, True)
                clsCommon.AddColumnsForChange(coll, "TAX6_GLAC2_Amt", Math.Round(obj.TAX6_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                objTM.Tax_Recoverable_Account3 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX6_GLAC3", objTM.Tax_Recoverable_Account3, True)
                clsCommon.AddColumnsForChange(coll, "TAX6_GLAC3_Amt", Math.Round(obj.TAX6_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                objTM.Tax_Recoverable_Account4 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX6_GLAC4", objTM.Tax_Recoverable_Account4, True)
                clsCommon.AddColumnsForChange(coll, "TAX6_GLAC4_Amt", Math.Round(obj.TAX6_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                objTM.Tax_Recoverable_Account5 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX6_GLAC5", objTM.Tax_Recoverable_Account5, True)
                clsCommon.AddColumnsForChange(coll, "TAX6_GLAC5_Amt", Math.Round(obj.TAX6_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven))
            End If
        End If
        objTM = clsTaxMaster.GetData(obj.TAX7, trans)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX7_GLAC", objTM.Tax_Recoverable_Account, True)
                clsCommon.AddColumnsForChange(coll, "TAX7_GLAC_Amt", Math.Round(obj.TAX7_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                objTM.Tax_Recoverable_Account2 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX7_GLAC2", objTM.Tax_Recoverable_Account2, True)
                clsCommon.AddColumnsForChange(coll, "TAX7_GLAC2_Amt", Math.Round(obj.TAX7_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                objTM.Tax_Recoverable_Account3 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX7_GLAC3", objTM.Tax_Recoverable_Account3, True)
                clsCommon.AddColumnsForChange(coll, "TAX7_GLAC3_Amt", Math.Round(obj.TAX7_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                objTM.Tax_Recoverable_Account4 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX7_GLAC4", objTM.Tax_Recoverable_Account4, True)
                clsCommon.AddColumnsForChange(coll, "TAX7_GLAC4_Amt", Math.Round(obj.TAX7_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                objTM.Tax_Recoverable_Account5 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX7_GLAC5", objTM.Tax_Recoverable_Account5, True)
                clsCommon.AddColumnsForChange(coll, "TAX7_GLAC5_Amt", Math.Round(obj.TAX7_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven))
            End If
        End If
        objTM = clsTaxMaster.GetData(obj.TAX8, trans)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX8_GLAC", objTM.Tax_Recoverable_Account, True)
                clsCommon.AddColumnsForChange(coll, "TAX8_GLAC_Amt", Math.Round(obj.TAX8_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                objTM.Tax_Recoverable_Account2 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX8_GLAC2", objTM.Tax_Recoverable_Account2, True)
                clsCommon.AddColumnsForChange(coll, "TAX8_GLAC2_Amt", Math.Round(obj.TAX8_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                objTM.Tax_Recoverable_Account3 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX8_GLAC3", objTM.Tax_Recoverable_Account3, True)
                clsCommon.AddColumnsForChange(coll, "TAX8_GLAC3_Amt", Math.Round(obj.TAX8_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                objTM.Tax_Recoverable_Account4 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX8_GLAC4", objTM.Tax_Recoverable_Account4, True)
                clsCommon.AddColumnsForChange(coll, "TAX8_GLAC4_Amt", Math.Round(obj.TAX8_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                objTM.Tax_Recoverable_Account5 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX8_GLAC5", objTM.Tax_Recoverable_Account5, True)
                clsCommon.AddColumnsForChange(coll, "TAX8_GLAC5_Amt", Math.Round(obj.TAX8_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven))
            End If
        End If
        objTM = clsTaxMaster.GetData(obj.TAX9, trans)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX9_GLAC", objTM.Tax_Recoverable_Account, True)
                clsCommon.AddColumnsForChange(coll, "TAX9_GLAC_Amt", Math.Round(obj.TAX9_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                objTM.Tax_Recoverable_Account2 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX9_GLAC2", objTM.Tax_Recoverable_Account2, True)
                clsCommon.AddColumnsForChange(coll, "TAX9_GLAC2_Amt", Math.Round(obj.TAX9_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                objTM.Tax_Recoverable_Account3 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, strLocation, True, trans)

                clsCommon.AddColumnsForChange(coll, "TAX9_GLAC3", objTM.Tax_Recoverable_Account3, True)
                clsCommon.AddColumnsForChange(coll, "TAX9_GLAC3_Amt", Math.Round(obj.TAX9_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                objTM.Tax_Recoverable_Account4 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX9_GLAC4", objTM.Tax_Recoverable_Account4, True)
                clsCommon.AddColumnsForChange(coll, "TAX9_GLAC4_Amt", Math.Round(obj.TAX9_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                objTM.Tax_Recoverable_Account5 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX9_GLAC5", objTM.Tax_Recoverable_Account5, True)
                clsCommon.AddColumnsForChange(coll, "TAX9_GLAC5_Amt", Math.Round(obj.TAX9_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven))
            End If
        End If
        objTM = clsTaxMaster.GetData(obj.TAX10, trans)
        If objTM IsNot Nothing AndAlso objTM.Tax_Recoverable Then
            If clsCommon.myLen(objTM.Tax_Recoverable_Account) > 0 Then
                objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX10_GLAC", objTM.Tax_Recoverable_Account, True)
                clsCommon.AddColumnsForChange(coll, "TAX10_GLAC_Amt", Math.Round(obj.TAX10_Amt * objTM.Tax_Recover_Rate / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account2) > 0 Then
                objTM.Tax_Recoverable_Account2 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account2, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX10_GLAC2", objTM.Tax_Recoverable_Account2, True)
                clsCommon.AddColumnsForChange(coll, "TAX10_GLAC2_Amt", Math.Round(obj.TAX10_Amt * objTM.Tax_Recover_Rate2 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account3) > 0 Then
                objTM.Tax_Recoverable_Account3 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account3, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX10_GLAC3", objTM.Tax_Recoverable_Account3, True)
                clsCommon.AddColumnsForChange(coll, "TAX10_GLAC3_Amt", Math.Round(obj.TAX10_Amt * objTM.Tax_Recover_Rate3 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account4) > 0 Then
                objTM.Tax_Recoverable_Account4 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account4, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX10_GLAC4", objTM.Tax_Recoverable_Account4, True)
                clsCommon.AddColumnsForChange(coll, "TAX10_GLAC4_Amt", Math.Round(obj.TAX10_Amt * objTM.Tax_Recover_Rate4 / 100, 2, MidpointRounding.ToEven))
            End If
            If clsCommon.myLen(objTM.Tax_Recoverable_Account5) > 0 Then
                objTM.Tax_Recoverable_Account5 = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account5, strLocation, True, trans)
                clsCommon.AddColumnsForChange(coll, "TAX10_GLAC5", objTM.Tax_Recoverable_Account5, True)
                clsCommon.AddColumnsForChange(coll, "TAX10_GLAC5_Amt", Math.Round(obj.TAX10_Amt * objTM.Tax_Recover_Rate5 / 100, 2, MidpointRounding.ToEven))
            End If
        End If



        clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
        clsCommon.AddColumnsForChange(coll, "Terms_Description", obj.Terms_Description)

        If obj.Due_Date IsNot Nothing Then
            clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MMM/yyyy"))
        End If


        clsCommon.AddColumnsForChange(coll, "Discount_Percentage", obj.Discount_Percentage)
        clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.Discount_Base)
        clsCommon.AddColumnsForChange(coll, "Discount_Amount", obj.Discount_Amount)
        clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
        clsCommon.AddColumnsForChange(coll, "TDS_Base_Actual_Amount", obj.TDS_Base_Actual_Amount)
        clsCommon.AddColumnsForChange(coll, "TDS_Base_Calculated_Amount", obj.TDS_Base_Calculated_Amount)
        clsCommon.AddColumnsForChange(coll, "TDS_Percentage", obj.TDS_Percentage)
        clsCommon.AddColumnsForChange(coll, "TDS_Actual_Amount", Math.Round(obj.TDS_Actual_Amount, 0, MidpointRounding.AwayFromZero))
        clsCommon.AddColumnsForChange(coll, "TDS_Calculated_Amount", obj.TDS_Calculated_Amount)
        clsCommon.AddColumnsForChange(coll, "Nature_of_deduction", obj.Nature_of_deduction)
        clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code)
        clsCommon.AddColumnsForChange(coll, "Section_Code", obj.Section_Code)
        clsCommon.AddColumnsForChange(coll, "Balance_Amt", IIf(obj.is_For_TDS = 0, (obj.Document_Total - Math.Round(obj.TDS_Actual_Amount, 0, MidpointRounding.AwayFromZero)), obj.Document_Total))
        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Against_POInvoice_No", obj.Against_POInvoice_No, True)
        clsCommon.AddColumnsForChange(coll, "Against_PurchaseReturn_No", obj.Against_PurchaseReturn_No, True)
        clsCommon.AddColumnsForChange(coll, "Against_Acquisition", obj.Against_Acquisition)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Code1", obj.Add_Charge_Code1)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Name1", obj.Add_Charge_Name1)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt1", obj.Add_Charge_Amt1)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Code2", obj.Add_Charge_Code2)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Name2", obj.Add_Charge_Name2)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt2", obj.Add_Charge_Amt2)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Code3", obj.Add_Charge_Code3)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Name3", obj.Add_Charge_Name3)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt3", obj.Add_Charge_Amt3)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Code4", obj.Add_Charge_Code4)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Name4", obj.Add_Charge_Name4)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt4", obj.Add_Charge_Amt4)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Code5", obj.Add_Charge_Code5)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Name5", obj.Add_Charge_Name5)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt5", obj.Add_Charge_Amt5)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Code6", obj.Add_Charge_Code6)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Name6", obj.Add_Charge_Name6)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt6", obj.Add_Charge_Amt6)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Code7", obj.Add_Charge_Code7)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Name7", obj.Add_Charge_Name7)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt7", obj.Add_Charge_Amt7)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Code8", obj.Add_Charge_Code8)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Name8", obj.Add_Charge_Name8)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt8", obj.Add_Charge_Amt8)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Code9", obj.Add_Charge_Code9)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Name9", obj.Add_Charge_Name9)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt9", obj.Add_Charge_Amt9)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Code10", obj.Add_Charge_Code10)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Name10", obj.Add_Charge_Name10)
        clsCommon.AddColumnsForChange(coll, "Add_Charge_Amt10", obj.Add_Charge_Amt10)
        clsCommon.AddColumnsForChange(coll, "Total_Add_Charge", obj.Total_Add_Charge)
        clsCommon.AddColumnsForChange(coll, "Total_Landed_Amt", obj.Total_Landed_Amt)
        clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
        clsCommon.AddColumnsForChange(coll, "Is_ProRated", obj.Is_ProRated)
        '' currencyconversion
        clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
        clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
        ' clsCommon.AddColumnsForChange(coll, "ApplicableFrom", obj.ApplicableFrom, True)
        If Not obj.ApplicableFrom Is Nothing Then
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd-MMM-yyyy"), True)
        End If

        '' End currencyconversion

        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Recurring_Payable_INVOICE_Head", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Recurring_Payable_INVOICE_Head", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
        End If


        isSaved = isSaved AndAlso clsRecurringPayableInvoiceDetail.SaveData(obj.Document_No, Arr, trans)
        isSaved = isSaved AndAlso clsRemittance.SaveData(obj.RemittanceObject, obj.Document_No, strLocation, trans)
        isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_No, obj.arrCustomFields, trans)

        isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_No", obj.Document_No, "TSPL_Recurring_Payable_INVOICE_Head", trans)

        Return isSaved
    End Function

    '-29/06/2013-------------Created by--Pankaj kumar
    Public Shared Function UpdateAllTax(ByVal DocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "Update TSPL_Recurring_Payable_INVOICE_Head Set "

            Qry += " TAX1_GLAC= NULL, TAX1_GLAC_Amt=0, TAX1_GLAC2= NULL, TAX1_GLAC2_Amt=0, TAX1_GLAC3= NULL, TAX1_GLAC3_Amt=0, TAX1_GLAC4= NULL, TAX1_GLAC4_Amt=0, TAX1_GLAC5= NULL, TAX1_GLAC5_Amt=0,"
            Qry += " TAX2_GLAC= NULL, TAX2_GLAC_Amt=0, TAX2_GLAC2= NULL, TAX2_GLAC2_Amt=0, TAX2_GLAC3= NULL, TAX2_GLAC3_Amt=0, TAX2_GLAC4= NULL, TAX2_GLAC4_Amt=0, TAX2_GLAC5= NULL, TAX2_GLAC5_Amt=0,"
            Qry += " TAX3_GLAC= NULL, TAX3_GLAC_Amt=0, TAX3_GLAC2= NULL, TAX3_GLAC2_Amt=0, TAX3_GLAC3= NULL, TAX3_GLAC3_Amt=0, TAX3_GLAC4= NULL, TAX3_GLAC4_Amt=0, TAX3_GLAC5= NULL, TAX3_GLAC5_Amt=0,"
            Qry += " TAX4_GLAC= NULL, TAX4_GLAC_Amt=0, TAX4_GLAC2= NULL, TAX4_GLAC2_Amt=0, TAX4_GLAC3= NULL, TAX4_GLAC3_Amt=0, TAX4_GLAC4= NULL, TAX4_GLAC4_Amt=0, TAX4_GLAC5= NULL, TAX4_GLAC5_Amt=0,"
            Qry += " TAX5_GLAC= NULL, TAX5_GLAC_Amt=0, TAX5_GLAC2= NULL, TAX5_GLAC2_Amt=0, TAX5_GLAC3= NULL, TAX5_GLAC3_Amt=0, TAX5_GLAC4= NULL, TAX5_GLAC4_Amt=0, TAX5_GLAC5= NULL, TAX5_GLAC5_Amt=0,"
            Qry += " TAX6_GLAC= NULL, TAX6_GLAC_Amt=0, TAX6_GLAC2= NULL, TAX6_GLAC2_Amt=0, TAX6_GLAC3= NULL, TAX6_GLAC3_Amt=0, TAX6_GLAC4= NULL, TAX6_GLAC4_Amt=0, TAX6_GLAC5= NULL, TAX6_GLAC5_Amt=0,"
            Qry += " TAX7_GLAC= NULL, TAX7_GLAC_Amt=0, TAX7_GLAC2= NULL, TAX7_GLAC2_Amt=0, TAX7_GLAC3= NULL, TAX7_GLAC3_Amt=0, TAX7_GLAC4= NULL, TAX7_GLAC4_Amt=0, TAX7_GLAC5= NULL, TAX7_GLAC5_Amt=0,"
            Qry += " TAX8_GLAC= NULL, TAX8_GLAC_Amt=0, TAX8_GLAC2= NULL, TAX8_GLAC2_Amt=0, TAX8_GLAC3= NULL, TAX8_GLAC3_Amt=0, TAX8_GLAC4= NULL, TAX8_GLAC4_Amt=0, TAX8_GLAC5= NULL, TAX8_GLAC5_Amt=0,"
            Qry += " TAX9_GLAC= NULL, TAX9_GLAC_Amt=0, TAX9_GLAC2= NULL, TAX9_GLAC2_Amt=0, TAX9_GLAC3= NULL, TAX9_GLAC3_Amt=0, TAX9_GLAC4= NULL, TAX9_GLAC4_Amt=0, TAX9_GLAC5= NULL, TAX9_GLAC5_Amt=0,"
            Qry += " TAX10_GLAC= NULL, TAX10_GLAC_Amt=0, TAX10_GLAC2= NULL, TAX10_GLAC2_Amt=0, TAX10_GLAC3= NULL, TAX10_GLAC3_Amt=0, TAX10_GLAC4= NULL, TAX10_GLAC4_Amt=0, TAX10_GLAC5= NULL, TAX10_GLAC5_Amt=0 "
            Qry += " WHERE TSPL_Recurring_Payable_INVOICE_Head.Document_No='" + DocNo + "'"

            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal strInvoiceType As String) As clsRecurringPayableInvoice
        Return GetData(strDocumentNo, strInvoiceType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal strInvoiceType As String, ByVal trans As SqlTransaction) As clsRecurringPayableInvoice
        Dim obj As clsRecurringPayableInvoice = Nothing
        Dim WhrClause As String = ""
        If clsCommon.myLen(strInvoiceType) > 0 Then
            WhrClause = " and TSPL_Recurring_Payable_INVOICE_Head.Invoice_Type in ('" & strInvoiceType & "')"
        End If
        Dim qry As String = "Select * from TSPL_Recurring_Payable_INVOICE_Head where Document_No='" + strDocumentNo + "' " & WhrClause & "  "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsRecurringPayableInvoice()
            obj.RemittanceObject = clsRemittance.GetData(strDocumentNo, trans)
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Invoice_Entry_Date = clsCommon.myCstr(dt.Rows(0)("Invoice_Entry_Date"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Vendor_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Vendor_Invoice_No"))
            obj.Vendor_Invoice_Date = clsCommon.myCstr(dt.Rows(0)("Vendor_Invoice_Date"))
            obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            obj.Is_cancelled = clsCommon.myCstr(dt.Rows(0)("Is_cancelled"))
            obj.is_For_TDS = clsCommon.myCdbl(dt.Rows(0)("is_For_TDS"))
            obj.Account_Set = clsCommon.myCstr(dt.Rows(0)("Account_Set"))
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.PO_Number = clsCommon.myCstr(dt.Rows(0)("PO_Number"))
            '---------added by usha
            obj.loc_code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Irregular_loc_code = clsCommon.myCstr(dt.Rows(0)("Irregular_Loc_Code"))
            '--------------
            '' priti starts here
            obj.RefDocType = clsCommon.myCstr(dt.Rows(0)("RefDocType"))
            obj.RefDocNo = clsCommon.myCstr(dt.Rows(0)("RefDocNo"))
            obj.Against_MillkPurchaseInvoice_No = clsCommon.myCstr(dt.Rows(0)("Against_MillkPurchaseInvoice_No"))
            obj.Against_VSPItemIssue_No = clsCommon.myCstr(dt.Rows(0)("Against_VSPItemIssue_No"))
            '' priti ends here
            obj.Order_No = clsCommon.myCstr(dt.Rows(0)("Order_No"))
            obj.Document_Total = clsCommon.myCdbl(dt.Rows(0)("Document_Total"))
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.On_Hold = (clsCommon.CompairString("Y", clsCommon.myCstr(dt.Rows(0)("On_Hold"))) = CompairStringResult.Equal)
            obj.is_For_Provision = clsCommon.myCdbl(dt.Rows(0)("is_For_Provision"))
            obj.isDeduction = clsCommon.myCdbl(dt.Rows(0)("isDeduction"))
            obj.Scheduler_Code = clsCommon.myCstr(dt.Rows(0)("Scheduler_Code"))
            obj.Expiration_Type = clsCommon.myCstr(dt.Rows(0)("Expiration_Type"))
            obj.Expiration_date = clsCommon.myCstr(dt.Rows(0)("Expiration_date"))
            obj.Expiration_Amount = clsCommon.myCdbl(dt.Rows(0)("Expiration_Amount"))

            If clsCommon.myLen(dt.Rows(0)("Prov_From_Date")) > 0 Then
                obj.Prov_From_Date = clsCommon.myCDate(dt.Rows(0)("Prov_From_Date"))
            End If
            If obj.is_For_Provision = 1 Then
                obj.Prov_Amt = clsCommon.myCdbl(dt.Rows(0)("Prov_Amt"))

                obj.Prov_To_Date = clsCommon.myCDate(dt.Rows(0)("Prov_To_Date"))
                obj.arrProvDocNo = clsProvisionEntry.getProvisionDocNo(obj.Document_No, trans)
            End If

            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.Tax1_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax1_BAmount"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.Tax2_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax2_BAmount"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.Tax3_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax3_BAmount"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.Tax4_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax4_BAmount"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.Tax5_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax5_BAmount"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.Tax6_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax6_BAmount"))
            obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.Tax7_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax7_BAmount"))
            obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.Tax8_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax8_BAmount"))
            obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
            obj.Tax9_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax9_BAmount"))
            obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.Tax10_BAmount = clsCommon.myCdbl(dt.Rows(0)("Tax10_BAmount"))
            obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
            obj.Total_Tax = clsCommon.myCdbl(dt.Rows(0)("Total_Tax"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Terms_Description = clsCommon.myCstr(dt.Rows(0)("Terms_Description"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.Discount_Percentage = clsCommon.myCdbl(dt.Rows(0)("Discount_Percentage"))
            obj.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amount = clsCommon.myCdbl(dt.Rows(0)("Discount_Amount"))
            obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
            obj.TDS_Base_Actual_Amount = clsCommon.myCdbl(dt.Rows(0)("TDS_Base_Actual_Amount"))
            obj.TDS_Base_Calculated_Amount = clsCommon.myCdbl(dt.Rows(0)("TDS_Base_Calculated_Amount"))
            obj.TDS_Percentage = clsCommon.myCdbl(dt.Rows(0)("TDS_Percentage"))
            obj.TDS_Actual_Amount = clsCommon.myCdbl(dt.Rows(0)("TDS_Actual_Amount"))
            obj.TDS_Calculated_Amount = clsCommon.myCdbl(dt.Rows(0)("TDS_Calculated_Amount"))
            obj.Nature_of_deduction = clsCommon.myCstr(dt.Rows(0)("Nature_of_deduction"))
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.Section_Code = clsCommon.myCstr(dt.Rows(0)("Section_Code"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("Balance_Amt"))
            obj.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Vendor_Control_AC"))
            obj.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_GL_AC"))

            obj.TAX1_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX1_GLAC"))
            obj.TAX1_GLAC_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_GLAC_Amt"))
            obj.TAX1_GLAC2 = clsCommon.myCstr(dt.Rows(0)("TAX1_GLAC2"))
            obj.TAX1_GLAC2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_GLAC2_Amt"))
            obj.TAX1_GLAC3 = clsCommon.myCstr(dt.Rows(0)("TAX1_GLAC3"))
            obj.TAX1_GLAC3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_GLAC3_Amt"))
            obj.TAX1_GLAC4 = clsCommon.myCstr(dt.Rows(0)("TAX1_GLAC4"))
            obj.TAX1_GLAC4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_GLAC4_Amt"))
            obj.TAX1_GLAC5 = clsCommon.myCstr(dt.Rows(0)("TAX1_GLAC5"))
            obj.TAX1_GLAC5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_GLAC5_Amt"))


            obj.TAX2_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX2_GLAC"))
            obj.TAX2_GLAC_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_GLAC_Amt"))
            obj.TAX2_GLAC2 = clsCommon.myCstr(dt.Rows(0)("TAX2_GLAC2"))
            obj.TAX2_GLAC2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_GLAC2_Amt"))
            obj.TAX2_GLAC3 = clsCommon.myCstr(dt.Rows(0)("TAX2_GLAC3"))
            obj.TAX2_GLAC3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_GLAC3_Amt"))
            obj.TAX2_GLAC4 = clsCommon.myCstr(dt.Rows(0)("TAX2_GLAC4"))
            obj.TAX2_GLAC4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_GLAC4_Amt"))
            obj.TAX2_GLAC5 = clsCommon.myCstr(dt.Rows(0)("TAX2_GLAC5"))
            obj.TAX2_GLAC5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_GLAC5_Amt"))

            obj.TAX3_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX3_GLAC"))
            obj.TAX3_GLAC_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_GLAC_Amt"))
            obj.TAX3_GLAC2 = clsCommon.myCstr(dt.Rows(0)("TAX3_GLAC2"))
            obj.TAX3_GLAC2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_GLAC2_Amt"))
            obj.TAX3_GLAC3 = clsCommon.myCstr(dt.Rows(0)("TAX3_GLAC3"))
            obj.TAX3_GLAC3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_GLAC3_Amt"))
            obj.TAX3_GLAC4 = clsCommon.myCstr(dt.Rows(0)("TAX3_GLAC4"))
            obj.TAX3_GLAC4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_GLAC4_Amt"))
            obj.TAX3_GLAC5 = clsCommon.myCstr(dt.Rows(0)("TAX3_GLAC5"))
            obj.TAX3_GLAC5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_GLAC5_Amt"))

            obj.TAX4_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX4_GLAC"))
            obj.TAX4_GLAC_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_GLAC_Amt"))
            obj.TAX4_GLAC2 = clsCommon.myCstr(dt.Rows(0)("TAX4_GLAC2"))
            obj.TAX4_GLAC2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_GLAC2_Amt"))
            obj.TAX4_GLAC3 = clsCommon.myCstr(dt.Rows(0)("TAX4_GLAC3"))
            obj.TAX4_GLAC3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_GLAC3_Amt"))
            obj.TAX4_GLAC4 = clsCommon.myCstr(dt.Rows(0)("TAX4_GLAC4"))
            obj.TAX4_GLAC4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_GLAC4_Amt"))
            obj.TAX4_GLAC5 = clsCommon.myCstr(dt.Rows(0)("TAX4_GLAC5"))
            obj.TAX4_GLAC5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_GLAC5_Amt"))

            obj.TAX5_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX5_GLAC"))
            obj.TAX5_GLAC_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_GLAC_Amt"))
            obj.TAX5_GLAC2 = clsCommon.myCstr(dt.Rows(0)("TAX5_GLAC2"))
            obj.TAX5_GLAC2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_GLAC2_Amt"))
            obj.TAX5_GLAC3 = clsCommon.myCstr(dt.Rows(0)("TAX5_GLAC3"))
            obj.TAX5_GLAC3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_GLAC3_Amt"))
            obj.TAX5_GLAC4 = clsCommon.myCstr(dt.Rows(0)("TAX5_GLAC4"))
            obj.TAX5_GLAC4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_GLAC4_Amt"))
            obj.TAX5_GLAC5 = clsCommon.myCstr(dt.Rows(0)("TAX5_GLAC5"))
            obj.TAX5_GLAC5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_GLAC5_Amt"))

            obj.TAX6_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX6_GLAC"))
            obj.TAX6_GLAC_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_GLAC_Amt"))
            obj.TAX6_GLAC2 = clsCommon.myCstr(dt.Rows(0)("TAX6_GLAC2"))
            obj.TAX6_GLAC2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_GLAC2_Amt"))
            obj.TAX6_GLAC3 = clsCommon.myCstr(dt.Rows(0)("TAX6_GLAC3"))
            obj.TAX6_GLAC3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_GLAC3_Amt"))
            obj.TAX6_GLAC4 = clsCommon.myCstr(dt.Rows(0)("TAX6_GLAC4"))
            obj.TAX6_GLAC4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_GLAC4_Amt"))
            obj.TAX6_GLAC5 = clsCommon.myCstr(dt.Rows(0)("TAX6_GLAC5"))
            obj.TAX6_GLAC5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_GLAC5_Amt"))

            obj.TAX7_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX7_GLAC"))
            obj.TAX7_GLAC_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_GLAC_Amt"))
            obj.TAX7_GLAC2 = clsCommon.myCstr(dt.Rows(0)("TAX7_GLAC2"))
            obj.TAX7_GLAC2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_GLAC2_Amt"))
            obj.TAX7_GLAC3 = clsCommon.myCstr(dt.Rows(0)("TAX7_GLAC3"))
            obj.TAX7_GLAC3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_GLAC3_Amt"))
            obj.TAX7_GLAC4 = clsCommon.myCstr(dt.Rows(0)("TAX7_GLAC4"))
            obj.TAX7_GLAC4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_GLAC4_Amt"))
            obj.TAX7_GLAC5 = clsCommon.myCstr(dt.Rows(0)("TAX7_GLAC5"))
            obj.TAX7_GLAC5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_GLAC5_Amt"))

            obj.TAX8_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX8_GLAC"))
            obj.TAX8_GLAC_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_GLAC_Amt"))
            obj.TAX8_GLAC2 = clsCommon.myCstr(dt.Rows(0)("TAX8_GLAC2"))
            obj.TAX8_GLAC2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_GLAC2_Amt"))
            obj.TAX8_GLAC3 = clsCommon.myCstr(dt.Rows(0)("TAX8_GLAC3"))
            obj.TAX8_GLAC3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_GLAC3_Amt"))
            obj.TAX8_GLAC4 = clsCommon.myCstr(dt.Rows(0)("TAX8_GLAC4"))
            obj.TAX8_GLAC4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_GLAC4_Amt"))
            obj.TAX8_GLAC5 = clsCommon.myCstr(dt.Rows(0)("TAX8_GLAC5"))
            obj.TAX8_GLAC5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_GLAC5_Amt"))

            obj.TAX9_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX9_GLAC"))
            obj.TAX9_GLAC_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_GLAC_Amt"))
            obj.TAX9_GLAC2 = clsCommon.myCstr(dt.Rows(0)("TAX9_GLAC2"))
            obj.TAX9_GLAC2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_GLAC2_Amt"))
            obj.TAX9_GLAC3 = clsCommon.myCstr(dt.Rows(0)("TAX9_GLAC3"))
            obj.TAX9_GLAC3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_GLAC3_Amt"))
            obj.TAX9_GLAC4 = clsCommon.myCstr(dt.Rows(0)("TAX9_GLAC4"))
            obj.TAX9_GLAC4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_GLAC4_Amt"))
            obj.TAX9_GLAC5 = clsCommon.myCstr(dt.Rows(0)("TAX9_GLAC5"))
            obj.TAX9_GLAC5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_GLAC5_Amt"))

            obj.TAX10_GLAC = clsCommon.myCstr(dt.Rows(0)("TAX10_GLAC"))
            obj.TAX10_GLAC_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_GLAC_Amt"))
            obj.TAX10_GLAC2 = clsCommon.myCstr(dt.Rows(0)("TAX10_GLAC2"))
            obj.TAX10_GLAC2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_GLAC2_Amt"))
            obj.TAX10_GLAC3 = clsCommon.myCstr(dt.Rows(0)("TAX10_GLAC3"))
            obj.TAX10_GLAC3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_GLAC3_Amt"))
            obj.TAX10_GLAC4 = clsCommon.myCstr(dt.Rows(0)("TAX10_GLAC4"))
            obj.TAX10_GLAC4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_GLAC4_Amt"))
            obj.TAX10_GLAC5 = clsCommon.myCstr(dt.Rows(0)("TAX10_GLAC5"))
            obj.TAX10_GLAC5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_GLAC5_Amt"))

            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))
            obj.Against_POInvoice_No = clsCommon.myCstr(dt.Rows(0)("Against_POInvoice_No"))
            obj.Against_PurchaseReturn_No = clsCommon.myCstr(dt.Rows(0)("Against_PurchaseReturn_No"))
            obj.Against_BulkMillkPurchaseInvoice_No = clsCommon.myCstr(dt.Rows(0)("Against_BulkMillkPurchaseInvoice_No"))
            obj.Against_Acquisition = clsCommon.myCstr(dt.Rows(0)("Against_Acquisition"))
            obj.Add_Charge_Code1 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code1"))
            obj.Add_Charge_Name1 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name1"))
            obj.Add_Charge_Amt1 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt1"))

            obj.Add_Charge_Code2 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code2"))
            obj.Add_Charge_Name2 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name2"))
            obj.Add_Charge_Amt2 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt2"))

            obj.Add_Charge_Code3 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code3"))
            obj.Add_Charge_Name3 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name3"))
            obj.Add_Charge_Amt3 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt3"))

            obj.Add_Charge_Code4 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code4"))
            obj.Add_Charge_Name4 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name4"))
            obj.Add_Charge_Amt4 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt4"))

            obj.Add_Charge_Code5 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code5"))
            obj.Add_Charge_Name5 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name5"))
            obj.Add_Charge_Amt5 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt5"))

            obj.Add_Charge_Code6 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code6"))
            obj.Add_Charge_Name6 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name6"))
            obj.Add_Charge_Amt6 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt6"))

            obj.Add_Charge_Code7 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code7"))
            obj.Add_Charge_Name7 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name7"))
            obj.Add_Charge_Amt7 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt7"))

            obj.Add_Charge_Code8 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code8"))
            obj.Add_Charge_Name8 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name8"))
            obj.Add_Charge_Amt8 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt8"))

            obj.Add_Charge_Code9 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code9"))
            obj.Add_Charge_Name9 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name9"))
            obj.Add_Charge_Amt9 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt9"))

            obj.Add_Charge_Code10 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Code10"))
            obj.Add_Charge_Name10 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Name10"))
            obj.Add_Charge_Amt10 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Amt10"))

            obj.Empty_Amount = clsCommon.myCdbl(dt.Rows(0)("Empty_Amount"))
            obj.Empty_Account = clsCommon.myCstr(dt.Rows(0)("Empty_Account"))
            obj.Total_Landed_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Landed_Amt"))

            obj.Total_Add_Charge = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge"))
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.Is_ProRated = clsCommon.myCstr(dt.Rows(0)("Is_ProRated"))
            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            obj.Security = clsCommon.myCdbl(dt.Rows(0)("Is_Security"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION

            qry = "Select tspl_Recurring_payable_invoice_detail.*,TSPL_TDS_DEDUCTION_HEAD.Description as Deduction_Name ,TSPL_TDS_DEDUCTION_HEAD.TDS_Section as Deduction_Section  from tspl_Recurring_payable_invoice_detail left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=tspl_Recurring_payable_invoice_detail.Deduction_Code where Document_No='" + strDocumentNo + "' ORDER BY Detail_Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsRecurringPayableInvoiceDetail)
                Dim objTr As clsRecurringPayableInvoiceDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsRecurringPayableInvoiceDetail

                    '**********************************************************************
                    objTr.chrgcatcode = clsCommon.myCstr(dr("charge_cat_code"))
                    objTr.chrgcatdesc = clsCommon.myCstr(dr("charge_cat_desc"))
                    objTr.chritemcode = clsCommon.myCstr(dr("item_code"))
                    objTr.chritemdesc = clsCommon.myCstr(dr("item_desc"))
                    objTr.chrgcatvalue = clsCommon.myCstr(dr("charge_cat_charges"))
                    '*****************************************************************
                    objTr.DeductionCode = clsCommon.myCstr(dr("DeductionCode"))
                    objTr.DeductionDesc = clsCommon.myCstr(dr("Deduction_Desc"))


                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Detail_Line_No = clsCommon.myCstr(dr("Detail_Line_No"))

                    objTr.Deduction_Code = clsCommon.myCstr(dr("Deduction_Code"))
                    objTr.Deduction_Name = clsCommon.myCstr(dr("Deduction_Name"))
                    objTr.Deduction_Section = clsCommon.myCstr(dr("Deduction_Section"))

                    objTr.GL_Account_Code = clsCommon.myCstr(dr("GL_Account_Code"))
                    objTr.GL_Account_Desc = clsCommon.myCstr(dr("GL_Account_Desc"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Discount_Per = clsCommon.myCdbl(dr("Discount_Per"))
                    objTr.Discount = clsCommon.myCdbl(dr("Discount"))
                    objTr.Amount_less_Discount = clsCommon.myCdbl(dr("Amount_less_Discount"))
                    objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objTr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objTr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objTr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objTr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objTr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objTr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objTr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objTr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objTr.Total_Tax = clsCommon.myCdbl(dr("Total_Tax"))
                    objTr.Total_Amount = clsCommon.myCdbl(dr("Total_Amount"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Comments = clsCommon.myCstr(dr("Comments"))
                    objTr.AddChargeCode = clsCommon.myCstr(dr("AddChargeCode"))
                    objTr.AddChargeDesc = clsCommon.myCstr(dr("AddChargeDesc"))
                    objTr.is_Unclaimed_Tax = IIf(clsCommon.myCdbl(dr("is_Unclaimed_Tax") = 1), True, False)
                    objTr.Invoice_No = clsCommon.myCstr(dr("Invoice_No"))
                    objTr.Invoice_Type = clsCommon.myCstr(dr("Invoice_Type"))
                    objTr.Landed_Amount = clsCommon.myCdbl(dr("Landed_Amount"))
                    objTr.Item_Rate = clsCommon.myCdbl(dr("Item_Rate"))
                    objTr.Abatement_Per = clsCommon.myCdbl(dr("Abatement_Per"))
                    objTr.Abatement_Amt = clsCommon.myCdbl(dr("Abatement_Amt"))

                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))

                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal strRefDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = clsRecurringPayableInvoice.PostData(FormId, strDocNo, strRefDocNo, trans)
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

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal strRefDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim strPostDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Invoice_Entry_Date from TSPL_Recurring_Payable_INVOICE_Head where Document_No='" + strDocNo + "'", trans))
        Return PostData(FormId, strDocNo, strRefDocNo, trans, strPostDate)
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal strRefDocNo As String, ByVal trans As SqlTransaction, ByVal strPostDate As Date) As Boolean
        Dim qry As String = ""
        Dim isTaxRecoverable As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Post")
        End If

        Dim obj As clsRecurringPayableInvoice = clsRecurringPayableInvoice.GetData(strDocNo, "", trans)
        If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If
        clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "AP Invoice Entry", obj.loc_code, clsCommon.myCDate(obj.Invoice_Entry_Date), trans)
        If (clsCommon.myLen(obj.Posting_Date) > 0) Then
            Throw New Exception("Already Post on :" + obj.Posting_Date)
        End If
        If (obj.On_Hold) Then
            Throw New Exception("Document No " + obj.Document_No + " Is currently On Hold.Can't Post it")
        End If
        If clsCommon.myLen(obj.Vendor_Control_AC) <= 0 Then
            Throw New Exception("Vendor's Control A/C Not found")
        End If

        Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_Recurring_Payable_INVOICE_Head", "Document_No", strDocNo, trans)
        If isResult = False Then
            trans.Commit()
            Return False
        End If
        'Dim objCostAdj As ClsAdjustments = Nothing
        'Dim ArryLst As ArrayList = New ArrayList()

        ' ''Dim strLocSeg As String = ""
        ' ''If obj.Arr(0).GL_Account_Code.Length > 3 Then
        ' ''    strLocSeg = obj.Arr(0).GL_Account_Code.Substring(clsCommon.myLen(obj.Arr(0).GL_Account_Code) - 2, 3)
        ' ''    If (IsNumeric(strLocSeg)) Then
        ' ''        Throw New Exception("First Account should be with segment")
        ' ''    End If
        ' ''Else
        ' ''    Throw New Exception("First Account should be with segment")
        ' ''End If


        'Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Account_Seg_Code7 from TSPL_GL_ACCOUNTS where Account_Code='" + obj.Arr(0).GL_Account_Code + "'", trans))


        'If obj.TAX1_Amt <> 0 Then
        '    If clsCommon.myLen(obj.TAX1_GLAC) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX1_GLAC, obj.TAX1_GLAC_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX1_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX1_GLAC_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX1_GLAC2) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX1_GLAC2, obj.TAX1_GLAC2_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX1_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX1_GLAC2_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX1_GLAC3) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX1_GLAC3, obj.TAX1_GLAC3_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX1_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX1_GLAC3_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX1_GLAC4) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX1_GLAC4, obj.TAX1_GLAC4_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX1_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX1_GLAC4_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX1_GLAC5) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX1_GLAC5, obj.TAX1_GLAC5_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX1_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX1_GLAC5_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        'End If

        'If obj.TAX2_Amt <> 0 Then
        '    If clsCommon.myLen(obj.TAX2_GLAC) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX2_GLAC, obj.TAX2_GLAC_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX2_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX2_GLAC_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX2_GLAC2) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX2_GLAC2, obj.TAX2_GLAC2_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX2_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX2_GLAC2_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX2_GLAC3) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX2_GLAC3, obj.TAX2_GLAC3_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX2_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX2_GLAC3_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX2_GLAC4) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX2_GLAC4, obj.TAX2_GLAC4_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX2_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX2_GLAC4_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX2_GLAC5) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX2_GLAC5, obj.TAX2_GLAC5_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX2_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX2_GLAC5_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        'End If
        'If obj.TAX3_Amt <> 0 Then
        '    If clsCommon.myLen(obj.TAX3_GLAC) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX3_GLAC, obj.TAX3_GLAC_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX3_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX3_GLAC_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX3_GLAC2) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX3_GLAC2, obj.TAX3_GLAC2_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX3_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX3_GLAC2_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX3_GLAC3) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX3_GLAC3, obj.TAX3_GLAC3_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX3_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX3_GLAC3_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX3_GLAC4) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX3_GLAC4, obj.TAX3_GLAC4_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX3_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX3_GLAC4_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX3_GLAC5) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX3_GLAC5, obj.TAX3_GLAC5_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX3_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX3_GLAC5_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        'End If
        'If obj.TAX4_Amt <> 0 Then
        '    If clsCommon.myLen(obj.TAX4_GLAC) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX4_GLAC, obj.TAX4_GLAC_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX4_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX4_GLAC_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX4_GLAC2) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX4_GLAC2, obj.TAX4_GLAC2_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX4_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX4_GLAC2_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX4_GLAC3) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX4_GLAC3, obj.TAX4_GLAC3_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX4_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX4_GLAC3_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX4_GLAC4) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX4_GLAC4, obj.TAX4_GLAC4_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX4_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX4_GLAC4_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX4_GLAC5) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX4_GLAC5, obj.TAX4_GLAC5_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX4_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX4_GLAC5_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        'End If
        'If obj.TAX5_Amt <> 0 Then
        '    If clsCommon.myLen(obj.TAX5_GLAC) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX5_GLAC, obj.TAX5_GLAC_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX5_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX5_GLAC_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX5_GLAC2) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX5_GLAC2, obj.TAX5_GLAC2_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX5_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX5_GLAC2_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX5_GLAC3) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX5_GLAC3, obj.TAX5_GLAC3_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX5_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX5_GLAC3_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX5_GLAC4) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX5_GLAC4, obj.TAX5_GLAC4_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX5_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX5_GLAC4_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX5_GLAC5) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX5_GLAC5, obj.TAX5_GLAC5_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX5_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX5_GLAC5_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        'End If
        'If obj.TAX6_Amt <> 0 Then
        '    If clsCommon.myLen(obj.TAX6_GLAC) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX6_GLAC, obj.TAX6_GLAC_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX6_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX6_GLAC_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX6_GLAC2) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX6_GLAC2, obj.TAX6_GLAC2_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX6_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX6_GLAC2_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX6_GLAC3) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX6_GLAC3, obj.TAX6_GLAC3_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX6_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX6_GLAC3_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX6_GLAC4) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX6_GLAC4, obj.TAX6_GLAC4_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX6_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX6_GLAC4_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX6_GLAC5) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX6_GLAC5, obj.TAX6_GLAC5_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX6_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX6_GLAC5_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        'End If
        'If obj.TAX7_Amt <> 0 Then
        '    If clsCommon.myLen(obj.TAX7_GLAC) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX7_GLAC, obj.TAX7_GLAC_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX7_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX7_GLAC_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX7_GLAC2) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX7_GLAC2, obj.TAX7_GLAC2_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX7_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX7_GLAC2_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX7_GLAC3) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX7_GLAC3, obj.TAX7_GLAC3_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX7_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX7_GLAC3_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX7_GLAC4) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX7_GLAC4, obj.TAX7_GLAC4_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX7_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX7_GLAC4_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX7_GLAC5) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX7_GLAC5, obj.TAX7_GLAC5_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX7_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX7_GLAC5_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        'End If

        'If obj.TAX8_Amt <> 0 Then
        '    If clsCommon.myLen(obj.TAX8_GLAC) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX8_GLAC, obj.TAX8_GLAC_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX8_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX8_GLAC_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX8_GLAC2) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX8_GLAC2, obj.TAX8_GLAC2_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX8_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX8_GLAC2_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX8_GLAC3) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX8_GLAC3, obj.TAX8_GLAC3_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX8_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX8_GLAC3_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX8_GLAC4) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX8_GLAC4, obj.TAX8_GLAC4_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX8_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX8_GLAC4_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX8_GLAC5) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX8_GLAC5, obj.TAX8_GLAC5_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX8_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX8_GLAC5_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        'End If

        'If obj.TAX9_Amt <> 0 Then
        '    If clsCommon.myLen(obj.TAX9_GLAC) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX9_GLAC, obj.TAX9_GLAC_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX9_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX9_GLAC_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX9_GLAC2) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX9_GLAC2, obj.TAX9_GLAC2_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX9_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX9_GLAC2_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX9_GLAC3) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX9_GLAC3, obj.TAX9_GLAC3_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX9_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX9_GLAC3_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX9_GLAC4) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX9_GLAC4, obj.TAX9_GLAC4_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX9_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX9_GLAC4_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX9_GLAC5) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX9_GLAC5, obj.TAX9_GLAC5_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX9_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX9_GLAC5_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        'End If

        'If obj.TAX10_Amt <> 0 Then
        '    If clsCommon.myLen(obj.TAX10_GLAC) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX10_GLAC, obj.TAX10_GLAC_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX10_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX10_GLAC_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX10_GLAC2) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX10_GLAC2, obj.TAX10_GLAC2_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX10_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX10_GLAC2_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX10_GLAC3) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX10_GLAC3, obj.TAX10_GLAC3_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX10_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX10_GLAC3_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX10_GLAC4) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX10_GLAC4, obj.TAX10_GLAC4_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX10_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX10_GLAC4_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        '    If clsCommon.myLen(obj.TAX10_GLAC5) > 0 Then
        '        Dim AccInvDR() As String = {obj.TAX10_GLAC5, obj.TAX10_GLAC5_Amt}
        '        ArryLst.Add(AccInvDR)
        '        If obj.TAX10_Amt < 0 Then
        '            Dim AccInvDR1() As String = {obj.Vendor_Control_AC, -1 * obj.TAX10_GLAC5_Amt}
        '            ArryLst.Add(AccInvDR1)
        '        End If
        '    End If
        'End If

        'Dim strShipTolocation As String = ""
        'Dim strBrachAC As String = ""
        'Dim strBrachACofShiptoLoc As String = ""
        'If clsCommon.myLen(obj.Against_POInvoice_No) > 0 Then
        '    strShipTolocation = clsDBFuncationality.getSingleValue("select Ship_To_Location from TSPL_PI_HEAD where PI_No='" + obj.Against_POInvoice_No + "'", trans)
        '    If clsCommon.myLen(strShipTolocation) > 0 Then
        '        strShipTolocation = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Code from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + strShipTolocation + "'", trans))
        '        If clsCommon.myLen(strShipTolocation) <= 0 Then
        '            Throw New Exception("Ship to location's Location not found")
        '        End If

        '        qry = "select Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING where From_Location='" + strShipTolocation + "' and To_Location='" + obj.loc_code + "'"
        '        strBrachAC = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        '        If clsCommon.myLen(strBrachAC) <= 0 Then
        '            Throw New Exception("Please set Brach account with From Location=" + strShipTolocation + " and To Location=" + obj.loc_code + "")
        '        End If
        '    End If

        'End If
        'If clsCommon.myLen(obj.Against_MillkPurchaseInvoice_No) > 0 And obj.RefDocType = "MI-PI" Then
        '    strShipTolocation = clsDBFuncationality.getSingleValue("select coalesce(Irregular_Mcc_Code,'') from TSPL_MILK_PURCHASE_INVOICE_HEAD where Doc_Code='" + obj.Against_MillkPurchaseInvoice_No + "'", trans)
        '    If clsCommon.myLen(strShipTolocation) > 0 Then
        '        If clsCommon.CompairString(obj.loc_code, strShipTolocation) <> CompairStringResult.Equal Then
        '            'strShipTolocation = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Code from TSPL_SHIP_TO_LOCATION where Ship_To_Code='" + strShipTolocation + "'", trans))
        '            'If clsCommon.myLen(strShipTolocation) <= 0 Then
        '            '    Throw New Exception("Ship to location's Location not found")
        '            'End If
        '            'obj.loc_code = clsDBFuncationality.getSingleValue("select Irregular_Mcc_Code from TSPL_MILK_PURCHASE_INVOICE_HEAD where Doc_Code='" + obj.Against_MillkPurchaseInvoice_No + "'", trans)
        '            Dim StrShipment As String = clsERPFuncationality.GetLocationSegment(strShipTolocation, trans)
        '            Dim strMainLoc As String = clsERPFuncationality.GetLocationSegment(obj.loc_code, trans)
        '            qry = "select Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING where From_Location='" + StrShipment + "' and To_Location='" + strMainLoc + "'"
        '            strBrachACofShiptoLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        '            If clsCommon.myLen(strBrachACofShiptoLoc) <= 0 Then
        '                Throw New Exception("Please set Brach account with From Location=" + StrShipment + " and To Location=" + strMainLoc + "")
        '            End If
        '            qry = "select Branch_Account from TSPL_BRANCH_ACCOUNT_MAPPING where To_Location='" + StrShipment + "' and From_Location='" + strMainLoc + "'"
        '            strBrachAC = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        '            If clsCommon.myLen(strBrachAC) <= 0 Then
        '                Throw New Exception("Please set Brach account with  From Location=" + strMainLoc + " and To Location=" + StrShipment + " ")
        '            End If
        '        End If
        '    Else
        '        strShipTolocation = ""
        '    End If

        'End If

        ' '' Dim isFirstTime As Boolean = True
        'If Not clsCommon.CompairString(obj.Is_ProRated, "Y") = CompairStringResult.Equal Then
        '    For Each objTR As clsRecurringPayableInvoiceDetail In obj.Arr
        '        Dim intCount As Integer = obj.Arr.Count
        '        Dim dblLedgeerNonRecoverableAmt As Double = clsRecurringPayableInvoice.GetTaxAmt(objTR, trans)
        '        Dim dblAddionalCost As Double = Math.Round((obj.Total_Add_Charge / intCount), 6)
        '        Dim dtbTempAmt As Double = objTR.Amount_less_Discount + dblAddionalCost + IIf(clsCommon.myLen(obj.Against_POInvoice_No) > 0 OrElse clsCommon.myLen(obj.Against_PurchaseReturn_No) > 0, objTR.Landed_Amount, dblLedgeerNonRecoverableAmt)
        '        Dim AccInvDR() As String = {objTR.GL_Account_Code, dtbTempAmt}
        '        ArryLst.Add(AccInvDR)

        '        If clsCommon.myLen(obj.Against_MillkPurchaseInvoice_No) <= 0 Or obj.RefDocType <> "MI-PI" Then
        '            If clsCommon.myLen(strShipTolocation) > 0 Then
        '                Dim strPaybleCleanigCtrlAC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(objTR.GL_Account_Code, strShipTolocation, trans)
        '                Dim AccCr2() As String = {strPaybleCleanigCtrlAC, dtbTempAmt}
        '                ArryLst.Add(AccCr2)

        '                Dim AccCr3() As String = {strBrachAC, -1 * dtbTempAmt}
        '                ArryLst.Add(AccCr3)
        '            End If
        '        Else
        '            If clsCommon.myLen(strShipTolocation) > 0 Then
        '                Dim strPaybleCleanigCtrlAC As String = clsERPFuncationality.ChangeGLAccountLocationSegment(objTR.GL_Account_Code, strShipTolocation, trans)
        '                'Dim AccCr2() As String = {strPaybleCleanigCtrlAC, dtbTempAmt}
        '                'ArryLst.Add(AccCr2)

        '                Dim AccCr3() As String = {strBrachACofShiptoLoc, -1 * dtbTempAmt}
        '                ArryLst.Add(AccCr3)

        '                'Dim AccCr4() As String = {strBrachAC, dtbTempAmt}
        '                'ArryLst.Add(AccCr4)

        '                'Dim AccCr5() As String = {strPaybleCleanigCtrlAC, -1 * dtbTempAmt}
        '                'ArryLst.Add(AccCr5)
        '                strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objTR.GL_Account_Code, obj.loc_code, trans)
        '                Dim AccCr6() As String = {strPaybleCleanigCtrlAC, dtbTempAmt}
        '                ArryLst.Add(AccCr6)
        '            End If
        '        End If
        '        ''isFirstTime = False
        '    Next
        'Else
        '    Dim Purchase_Control_Account As String = ""
        '    Dim ObjSRN As clsSRNHead = clsSRNHead.GetData(obj.RefDocNo, NavigatorType.Current, trans)
        '    Dim Index As Integer = 1
        '    Dim PurchaseControlAmount As Decimal = 0.0
        '    If ObjSRN IsNot Nothing And clsCommon.myLen(ObjSRN.SRN_No) > 0 Then
        '        objCostAdj = New ClsAdjustments()
        '        objCostAdj.Arr = New List(Of ClsAdjustmentsDetails)
        '        For Each objSRNDetail As clsSRNDetail In ObjSRN.Arr
        '            Purchase_Control_Account = "Select Purchase_Control_Account from TSPL_PURCHASE_ACCOUNTS WHERE Purchase_Class_Code= (Select Purchase_Class_Code from TSPL_ITEM_MASTER WHERE Item_Code='" & objSRNDetail.Item_Code & "')"
        '            Purchase_Control_Account = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Purchase_Control_Account, trans))
        '            PurchaseControlAmount = Math.Round((objSRNDetail.Item_Net_Amt / (ObjSRN.SRN_Total_Amt - ObjSRN.Total_Add_Charge)) * obj.Amount_Less_Discount, 2)
        '            If clsCommon.myLen(Purchase_Control_Account) <= 0 Then
        '                Throw New Exception("Purchase Control Account Not Found For Item : " & objSRNDetail.Item_Code & "")
        '            End If
        '            Purchase_Control_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(Purchase_Control_Account, obj.loc_code, trans)
        '            Dim AccInvDR() As String = {Purchase_Control_Account, PurchaseControlAmount}
        '            ArryLst.Add(AccInvDR)
        '            '-------------Cost Adjustment Entry----------------
        '            objCostAdj.Reference_Document = ObjSRN.SRN_No
        '            objCostAdj.Adjustment_Date = obj.Invoice_Entry_Date
        '            objCostAdj.Reference = "Cost Adjustment Against AP Invoice : " + obj.Document_No + " & SRN : " + ObjSRN.SRN_No + "."
        '            objCostAdj.Description = "Cost Adjustment Against AP Invoice : " + obj.Document_No + " & SRN : " + ObjSRN.SRN_No + "."

        '            objCostAdj.Unit_Code = "ALL"
        '            objCostAdj.ItemType = IIf(clsCommon.CompairString(ObjSRN.Item_Type, "F") = CompairStringResult.Equal, "FT", "OT")
        '            objCostAdj.Loc_Code = obj.loc_code
        '            objCostAdj.Loc_Desc = clsLocation.GetName(obj.loc_code, trans)
        '            objCostAdj.Trans_Type = "In"
        '            If clsCommon.CompairString(ObjSRN.Item_Type, "F") = CompairStringResult.Equal Then
        '                objCostAdj.Stock_Type = ""
        '            End If

        '            Dim objTr As New ClsAdjustmentsDetails()
        '            objTr.Adjustment_Line_No = Index
        '            objTr.Item_Code = objSRNDetail.Item_Code
        '            objTr.Item_Description = objSRNDetail.Item_Desc
        '            objTr.Adjustment_Type = "CI"
        '            objTr.Item_Quantity = 0
        '            objTr.Item_Cost = PurchaseControlAmount
        '            objTr.Unit_Code = objSRNDetail.Unit_code
        '            objTr.Remarks = ""
        '            objTr.Comments = ""
        '            objTr.mrp = clsCommon.myCdbl(objSRNDetail.MRP)

        '            objTr.BreakageType = ""
        '            objTr.Breakage = 0.0
        '            objTr.Breakage_Cost = 0.0
        '            objTr.LeakageQty = 0.0
        '            objTr.ItemType = ObjSRN.Item_Type
        '            objTr.Basic_Price = clsItemBasicPrice.GetBasicPrice(objTr.Item_Code, objTr.mrp, trans)
        '            'objTr.arrBatchItem = TryCast(grow.Cells(colICode).Tag, List(Of clsBatchInventory))
        '            objCostAdj.Arr.Add(objTr)
        '        Next
        '        '---------------------------------------------------------
        '    End If
        'End If

        'Dim AccInvCR() As String = {obj.Vendor_Control_AC, -1 * (obj.Document_Total + IIf(obj.TAX1_Amt < 0, -1 * obj.TAX1_Amt, 0) + IIf(obj.TAX2_Amt < 0, -1 * obj.TAX2_Amt, 0) + IIf(obj.TAX3_Amt < 0, -1 * obj.TAX3_Amt, 0) + IIf(obj.TAX4_Amt < 0, -1 * obj.TAX4_Amt, 0) + IIf(obj.TAX5_Amt < 0, -1 * obj.TAX5_Amt, 0) + IIf(obj.TAX6_Amt < 0, -1 * obj.TAX6_Amt, 0) + IIf(obj.TAX7_Amt < 0, -1 * obj.TAX7_Amt, 0) + IIf(obj.TAX8_Amt < 0, -1 * obj.TAX8_Amt, 0) + IIf(obj.TAX9_Amt < 0, -1 * obj.TAX9_Amt, 0) + IIf(obj.TAX10_Amt < 0, -1 * obj.TAX10_Amt, 0))}
        'ArryLst.Add(AccInvCR)


        'If obj.RoundOffAmount <> 0 Then
        '    Dim strACRoundInvCr As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DefaultRoundOffGLAccount, clsFixedParameterCode.DefaultRoundOffGLAccount, trans))

        '    If clsCommon.myLen(strACRoundInvCr) <= 0 Then
        '        Throw New Exception("Please set the Roundoff account")
        '    End If

        '    strACRoundInvCr = clsERPFuncationality.ChangeGLAccountLocationSegment(strACRoundInvCr, strLocation, True, trans)
        '    Dim AccRoundInvCR() As String = {strACRoundInvCr, obj.RoundOffAmount}
        '    ArryLst.Add(AccRoundInvCR)
        'End If



        'If obj.Empty_Amount > 0 Then
        '    If clsCommon.myLen(obj.Empty_Account) < 0 Then
        '        Throw New Exception("Plese set items Container Debosit account")
        '    End If

        '    Dim AccInvDR() As String = {obj.Empty_Account, obj.Empty_Amount}
        '    ArryLst.Add(AccInvDR)
        'End If


        ' ''If obj.Discount_Amount > 0 Then
        ' ''    If clsCommon.myLen(obj.Discount_GL_AC) <= 0 Then
        ' ''        Throw New Exception("Discount GL Account Not found")
        ' ''    End If
        ' ''    Dim AccDisCR() As String = {obj.Discount_GL_AC, -1 * obj.Discount_Amount}
        ' ''    ArryLst.Add(AccDisCR)
        ' ''End If

        'If (obj.RemittanceObject IsNot Nothing AndAlso obj.is_For_TDS = 0) Then ''is_For_TDS Entry made by ap invoice is come in this section
        '    If obj.TDS_Actual_Amount <> 0 Then
        '        Dim AccToInsertDR() As String = {obj.Vendor_Control_AC, obj.TDS_Actual_Amount}
        '        ArryLst.Add(AccToInsertDR)
        '        obj.RemittanceObject.Branch_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(obj.RemittanceObject.Branch_GL_AC, strLocation, True, trans)
        '        Dim AccToInsertCR() As String = {obj.RemittanceObject.Branch_GL_AC, -1 * obj.TDS_Actual_Amount}
        '        ArryLst.Add(AccToInsertCR)
        '    End If
        'End If
        ''----------added by usha-------
        ''-------(start here)
        'Dim pjvNOVochdesc As String
        'Dim strVoucher_Desc As String
        'If clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
        '    strVoucher_Desc = "AP Debit Note Against " + obj.Document_No
        'ElseIf clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal Then
        '    strVoucher_Desc = "AP Credit Note Against " + obj.Document_No
        'ElseIf clsCommon.CompairString(FormId, "CSA-SALE") = CompairStringResult.Equal Then
        '    strVoucher_Desc = "AP Invoice Against CSA Sale Patti No. " + obj.Vendor_Invoice_No
        'ElseIf clsCommon.myLen(obj.Against_MillkPurchaseInvoice_No) > 0 And clsCommon.CompairString(obj.RefDocType, "MI-PI") = CompairStringResult.Equal Then
        '    strVoucher_Desc = "Milk Purchase Invoice Against AP Invoice - " + obj.Document_No
        '    obj.Description = ""
        '    'ElseIf clsCommon.myLen(obj.Against_VSPItemIssue_No) > 0 And clsCommon.CompairString(obj.RefDocType, "VSP_I") = CompairStringResult.Equal Then
        '    '    strVoucher_Desc = "VSP Item Issue Against AP Invoice - " + obj.Document_No
        '    '    obj.Description = ""
        'ElseIf clsCommon.myLen(obj.Against_MillkPurchaseInvoice_No) > 0 And clsCommon.CompairString(obj.RefDocType, "MI-CO") = CompairStringResult.Equal Then
        '    strVoucher_Desc = "EMP Against AP Invoice - " + obj.Document_No
        '    obj.Description = ""
        'ElseIf clsCommon.myLen(obj.Against_MillkPurchaseInvoice_No) > 0 And clsCommon.CompairString(obj.RefDocType, "MI-IN") = CompairStringResult.Equal Then
        '    strVoucher_Desc = "Incentive Against AP Invoice - " + obj.Document_No
        '    obj.Description = ""
        'ElseIf clsCommon.myLen(obj.Against_BulkMillkPurchaseInvoice_No) > 0 And clsCommon.CompairString(obj.RefDocType, "BM-PI") = CompairStringResult.Equal Then
        '    strVoucher_Desc = "Bulk Milk Purchase Invoice No. " & obj.Against_BulkMillkPurchaseInvoice_No & ", Against AP Invoice - " + obj.Document_No
        '    obj.Description = ""
        'Else
        '    strVoucher_Desc = "AP Invoice Against " + obj.Document_No
        'End If
        'If Len(obj.Against_POInvoice_No) > 0 Then
        '    Dim qry1 As String = " select pjv_no from TSPL_PJV_HEAD where Invoice_No ='" + obj.Against_POInvoice_No + "'"

        '    Dim pjvno As String = clsDBFuncationality.getSingleValue(qry1, trans)
        '    pjvNOVochdesc = "PJVNO-" + pjvno + "-" + strVoucher_Desc
        'Else
        '    pjvNOVochdesc = strVoucher_Desc

        'End If
        ''-------------ends here--------
        ''clsCommon.MyMessageBoxShow(obj.Document_No & "    " & obj.Description & "    " & obj.Vendor_Code & "    " & obj.Vendor_Name)
        'If Not clsCommon.CompairString(obj.Order_No, "C") = CompairStringResult.Equal Then  '' for closing (Import opeining balance) balance not making the journal entry. 
        '    If (clsCommon.CompairString(obj.Document_Type, "I") = CompairStringResult.Equal) Then
        '        transportSql.FunGrnlEntryWithTrans(obj.loc_code, True, trans, strPostDate, pjvNOVochdesc, "AP-IN", "AP Invoice", obj.Document_No, obj.Description, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
        '    ElseIf (clsCommon.CompairString(obj.Document_Type, "C") = CompairStringResult.Equal) Then
        '        transportSql.FunGrnlEntryWithTrans(obj.loc_code, True, trans, strPostDate, pjvNOVochdesc, "AP-CN", "AP Invoice", obj.Document_No, obj.Description, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst)
        '    ElseIf (clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal) Then
        '        Dim ArryLstNew As ArrayList = New ArrayList()
        '        For Each Str() As String In ArryLst
        '            Dim strNew() As String = {Str(0), -1 * Str(1)}
        '            ArryLstNew.Add(strNew)
        '        Next
        '        transportSql.FunGrnlEntryWithTrans(obj.loc_code, True, trans, strPostDate, pjvNOVochdesc, "AP-DN", "AP Invoice", obj.Document_No, obj.Description, "V", obj.Vendor_Code, obj.Vendor_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstNew)
        '    Else
        '        Throw New Exception("Invoice Type not found to Post")
        '    End If
        'End If


        'qry = "update TSPL_REMITTANCE set Remit_TDS='N' where Document_No='" + strDocNo + "'"
        'clsDBFuncationality.ExecuteNonQuery(qry, trans)
        'Dim pono As String = obj.Against_POInvoice_No
        'Dim amt As Decimal = obj.Document_Total

        'If clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.Against_PurchaseReturn_No) > 0 Then
        '    qry = "update TSPL_Recurring_Payable_INVOICE_Head set Balance_Amt=Balance_Amt- " + clsCommon.myCstr(obj.Document_Total - obj.TDS_Actual_Amount) + " where Against_POInvoice_No  in ( select Against_PI from TSPL_PR_HEAD where PR_No='" + obj.Against_PurchaseReturn_No + "')"
        '    clsDBFuncationality.ExecuteNonQuery(qry, trans)
        'End If

        If clsCommon.CompairString(obj.RefDocType, "AP") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.RefDocNo) > 0 Then
            Dim strOpearateor = "+"
            If clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                strOpearateor = "-"
            End If
            Dim dblRemAmt As Double = 0
            If (obj.RemittanceObject IsNot Nothing) Then
                dblRemAmt = obj.RemittanceObject.Actual_Total_TDS
            End If

            qry = "update TSPL_Recurring_Payable_INVOICE_Head set Balance_Amt=Balance_Amt" + strOpearateor + " " + clsCommon.myCstr(obj.Document_Total - dblRemAmt) + " where Document_No='" + obj.RefDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "select Balance_Amt  from TSPL_Recurring_Payable_INVOICE_Head where  Document_No='" + obj.RefDocNo + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) < 0 Then
                Throw New Exception("Balance is Going to Negative For AP Invoice No : " + obj.RefDocNo)
            End If
        End If

        qry = "Update TSPL_Recurring_Payable_INVOICE_Head set Posting_Date='" + clsCommon.GetPrintDate(clsCommon.myCDate(strPostDate), "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        'If clsCommon.CompairString(obj.Is_ProRated, "Y") = CompairStringResult.Equal Then
        '    objCostAdj.SaveData(objCostAdj, True, "", trans)
        'End If
        clsRecurringPayableInvoice.CreateAdditionalCostEntry(strRefDocNo, obj, trans)
        Return True
    End Function

    Private Shared Function CreateAdditionalCostEntry(ByVal strRefDocNo As String, ByVal obj As clsRecurringPayableInvoice, ByVal trans As SqlTransaction) As Boolean
        Try
            ''''''''' priti starts here when REFerence Document No is not blank
            If strRefDocNo <> "" Then
                Dim strQry As String
                Dim strRecoverable, strInvAcc, strAdjAcc As String
                Dim dt As New DataTable
                Dim decTaxAmt As Double = 0
                Dim Totalamt As Double
                Dim totalunit_cost_tax As Double = 0
                Dim AddCost As Double = 0
                Dim TotalAddCost As Double = 0
                Dim AddCostOld As Double = 0
                Dim TotalAddCostOld As Double = 0
                Dim Landed_Cost As Double = 0
                Dim TotLanded_Cost As Double = 0
                Dim Landed_CostOld As Double = 0
                Dim TotLanded_CostOld As Double = 0
                Dim intLineNo As Integer = 0
                Dim ArryLst1 As ArrayList = New ArrayList()

                For Each objTR As clsRecurringPayableInvoiceDetail In obj.Arr
                    decTaxAmt = 0
                    If objTR.TAX1 <> "" Then
                        strQry = "select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code='" & objTR.TAX1 & "'"
                        dt = clsDBFuncationality.GetDataTable(strQry, trans)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            'obj = New clsRecurringPayableInvoice()
                            strRecoverable = clsCommon.myCstr(dt.Rows(0)("Tax_Recoverable"))
                            If strRecoverable = "N" Then
                                decTaxAmt = decTaxAmt + obj.TAX1_Amt
                                decTaxAmt = objTR.Amount_less_Discount + decTaxAmt
                            Else
                                decTaxAmt = objTR.Amount_less_Discount + decTaxAmt
                            End If
                        End If
                    Else
                        decTaxAmt = objTR.Amount_less_Discount + decTaxAmt
                    End If
                    If objTR.TAX2 <> "" Then
                        strQry = "select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code='" & objTR.TAX2 & "'"
                        dt = clsDBFuncationality.GetDataTable(strQry, trans)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            'obj = New clsRecurringPayableInvoice()
                            strRecoverable = clsCommon.myCstr(dt.Rows(0)("Tax_Recoverable"))
                            If strRecoverable = "N" Then
                                decTaxAmt = decTaxAmt + objTR.TAX2_Amt
                            End If
                        End If
                    End If
                    If objTR.TAX3 <> "" Then
                        strQry = "select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code='" & objTR.TAX3 & "'"
                        dt = clsDBFuncationality.GetDataTable(strQry, trans)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            'objTR = New clsRecurringPayableInvoice()
                            strRecoverable = clsCommon.myCstr(dt.Rows(0)("Tax_Recoverable"))
                            If strRecoverable = "N" Then
                                decTaxAmt = decTaxAmt + objTR.TAX3_Amt
                            End If
                        End If
                    End If
                    If objTR.TAX4 <> "" Then
                        strQry = "select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code='" & objTR.TAX4 & "'"
                        dt = clsDBFuncationality.GetDataTable(strQry, trans)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            'objTR = New clsRecurringPayableInvoice()
                            strRecoverable = clsCommon.myCstr(dt.Rows(0)("Tax_Recoverable"))
                            If strRecoverable = "N" Then
                                decTaxAmt = decTaxAmt + objTR.TAX4_Amt
                            End If
                        End If
                    End If
                    If objTR.TAX5 <> "" Then
                        strQry = "select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code='" & objTR.TAX5 & "'"
                        dt = clsDBFuncationality.GetDataTable(strQry, trans)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            'objTR = New clsRecurringPayableInvoice()
                            strRecoverable = clsCommon.myCstr(dt.Rows(0)("Tax_Recoverable"))
                            If strRecoverable = "N" Then
                                decTaxAmt = decTaxAmt + objTR.TAX5_Amt
                            End If
                        End If
                    End If
                    If objTR.TAX6 <> "" Then
                        strQry = "select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code='" & objTR.TAX6 & "'"
                        dt = clsDBFuncationality.GetDataTable(strQry, trans)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            strRecoverable = clsCommon.myCstr(dt.Rows(0)("Tax_Recoverable"))
                            If strRecoverable = "N" Then
                                decTaxAmt = decTaxAmt + objTR.TAX6_Amt
                            End If
                        End If
                    End If
                    If objTR.TAX7 <> "" Then
                        strQry = "select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code='" & objTR.TAX7 & "'"
                        dt = clsDBFuncationality.GetDataTable(strQry, trans)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            'objTR = New clsRecurringPayableInvoice()
                            strRecoverable = clsCommon.myCstr(dt.Rows(0)("Tax_Recoverable"))
                            If strRecoverable = "N" Then
                                decTaxAmt = decTaxAmt + objTR.TAX7_Amt
                            End If
                        End If
                    End If
                    If objTR.TAX8 <> "" Then
                        strQry = "select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code='" & objTR.TAX8 & "'"
                        dt = clsDBFuncationality.GetDataTable(strQry, trans)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            'objTR = New clsRecurringPayableInvoice()
                            strRecoverable = clsCommon.myCstr(dt.Rows(0)("Tax_Recoverable"))
                            If strRecoverable = "N" Then
                                decTaxAmt = decTaxAmt + objTR.TAX8_Amt
                            End If
                        End If
                    End If
                    If objTR.TAX9 <> "" Then
                        strQry = "select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code='" & objTR.TAX9 & "'"
                        dt = clsDBFuncationality.GetDataTable(strQry, trans)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            'objTR = New clsRecurringPayableInvoice()
                            strRecoverable = clsCommon.myCstr(dt.Rows(0)("Tax_Recoverable"))
                            If strRecoverable = "N" Then
                                decTaxAmt = decTaxAmt + objTR.TAX9_Amt
                            End If
                        End If
                    End If
                    If objTR.TAX10 <> "" Then
                        strQry = "select Tax_Recoverable from TSPL_TAX_MASTER  where Tax_Code='" & objTR.TAX10 & "'"
                        dt = clsDBFuncationality.GetDataTable(strQry, trans)
                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            'objTR = New clsRecurringPayableInvoice()
                            strRecoverable = clsCommon.myCstr(dt.Rows(0)("Tax_Recoverable"))
                            If strRecoverable = "N" Then
                                decTaxAmt = decTaxAmt + objTR.TAX10_Amt
                            End If
                        End If
                    End If

                    Totalamt = decTaxAmt
                    'Dim strGlAcct As String = obj.Arr(0).GL_Account_Code

                    Dim strSql As String = "select Item_Code,Item_Desc,Location from TSPL_SRN_DETAIL where SRN_No='" & strRefDocNo & "' and Line_No=1"
                    dt = clsDBFuncationality.GetDataTable(strSql, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        obj.Arr = New List(Of clsRecurringPayableInvoiceDetail)
                        Dim objTr1 As clsRecurringPayableInvoiceDetail
                        For Each dr As DataRow In dt.Rows
                            intLineNo = intLineNo + 1
                            objTr1 = New clsRecurringPayableInvoiceDetail
                            objTr1.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                            objTr1.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                            objTr1.Location = clsCommon.myCstr(dr("Location"))



                            Dim strsegment As String = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + objTr1.Location + "'", trans)
                            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable("select  replace (Inv_Control_Account,RIGHT(Inv_Control_Account,3),'" + strsegment + "')AS Inv_Control_Account ,'" & objTR.GL_Account_Code & "' as Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in " & _
                               "(select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objTr1.Item_Code + "')", trans)
                            If dt2 Is Nothing AndAlso dt2.Rows.Count <= 0 Then
                                Throw New Exception("Error")
                            End If

                            For Each DAtarow As DataRow In dt2.Rows
                                strInvAcc = DAtarow(0).ToString()
                                strAdjAcc = DAtarow(1).ToString()
                                Dim Acc1() As String = {strInvAcc, Totalamt}
                                Dim Acc2() As String = {strAdjAcc, Totalamt * -1}
                                ArryLst1.Add(Acc1)
                                ArryLst1.Add(Acc2)
                            Next
                        Next
                    End If
                Next


                If ArryLst1 IsNot Nothing AndAlso ArryLst1.Count > 0 Then
                    transportSql.FunGrnlEntryWithTrans(obj.loc_code, False, trans, clsCommon.GETSERVERDATE(trans), "Additional Cost against SRN No '" & strRefDocNo & "' for Vendor '" & obj.Vendor_Name & "'", "AP-IN", "I/C Adjustments", "", "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst1, strRefDocNo)
                End If
            End If
            ''''''''' priti ends here
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function GetTaxAmt(ByVal objPIDetail As clsRecurringPayableInvoiceDetail, ByVal tans As SqlTransaction) As Double
        Dim dblTotalTax As Double = 0
        Dim isTaxRecoverable As Boolean = False
        If objPIDetail.TAX1_Amt <> 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX1, tans) Then
            dblTotalTax += objPIDetail.TAX1_Amt
        End If
        If objPIDetail.TAX2_Amt <> 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX2, tans) Then
            dblTotalTax += objPIDetail.TAX2_Amt
        End If
        If objPIDetail.TAX3_Amt <> 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX3, tans) Then
            dblTotalTax += objPIDetail.TAX3_Amt
        End If
        If objPIDetail.TAX4_Amt <> 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX4, tans) Then
            dblTotalTax += objPIDetail.TAX4_Amt
        End If
        If objPIDetail.TAX5_Amt <> 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX5, tans) Then
            dblTotalTax += objPIDetail.TAX5_Amt
        End If
        If objPIDetail.TAX6_Amt <> 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX6, tans) Then
            dblTotalTax += objPIDetail.TAX6_Amt
        End If
        If objPIDetail.TAX7_Amt <> 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX7, tans) Then
            dblTotalTax += objPIDetail.TAX7_Amt
        End If
        If objPIDetail.TAX8_Amt <> 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX8, tans) Then
            dblTotalTax += objPIDetail.TAX8_Amt
        End If
        If objPIDetail.TAX9_Amt <> 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX9, tans) Then
            dblTotalTax += objPIDetail.TAX9_Amt
        End If
        If objPIDetail.TAX10_Amt <> 0 AndAlso Not clsTaxMaster.IsTaxRecoverableAC(objPIDetail.TAX10, tans) Then
            dblTotalTax += objPIDetail.TAX10_Amt
        End If
        Return dblTotalTax
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsRecurringPayableInvoice = clsRecurringPayableInvoice.GetData(strDocNo, "")
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationSegment(objCommonVar.CurrentCompanyCode, "Payables", "AP Invoice Entry", obj.loc_code, clsCommon.myCDate(obj.Invoice_Entry_Date), trans)

                If (clsCommon.myLen(obj.Posting_Date) > 0) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from tspl_Recurring_payable_invoice_detail where Document_No='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_REMITTANCE where Document_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_Recurring_Payable_INVOICE_Head where Document_No='" + strDocNo + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strDocNo, trans)
                If obj.is_For_Provision = 1 Then
                    isSaved = isSaved AndAlso clsProvisionEntry.SaveData(obj.Document_No, Nothing, trans)
                End If
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

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "Select Posting_Date from TSPL_Recurring_Payable_INVOICE_Head WHERE Document_No='" + strDocNo + "'"
            If clsCommon.myLen(clsDBFuncationality.getSingleValue(Qry, trans)) <= 0 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Dim obj As clsRecurringPayableInvoice = clsRecurringPayableInvoice.GetData(strDocNo, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Reverse And UnPost")
            End If

            '-----Delete Main Journal ENtry----
            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code like 'AP%' and Source_Doc_No='" + obj.Document_No + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            '-----Delete Additional Cost ENtry----
            VoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code like 'AP%' and Source_Doc_No='" + obj.RefDocNo + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "update TSPL_REMITTANCE set Remit_TDS=NULL where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            If clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.Against_PurchaseReturn_No) > 0 Then
                Qry = "update TSPL_Recurring_Payable_INVOICE_Head set Balance_Amt=Balance_Amt+ " + clsCommon.myCstr(obj.Document_Total - obj.TDS_Actual_Amount) + " where Against_POInvoice_No  in ( select Against_PI from TSPL_PR_HEAD where PR_No='" + obj.Against_PurchaseReturn_No + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            If clsCommon.CompairString(obj.RefDocType, "AP") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.RefDocNo) > 0 Then
                Dim strOpearateor = "-"
                If clsCommon.CompairString(obj.Document_Type, "D") = CompairStringResult.Equal Then
                    strOpearateor = "+"
                End If
                Dim dblRemAmt As Double = 0
                If (obj.RemittanceObject IsNot Nothing) Then
                    dblRemAmt = obj.RemittanceObject.Actual_Total_TDS
                End If

                Qry = "update TSPL_Recurring_Payable_INVOICE_Head set Balance_Amt=Balance_Amt" + strOpearateor + " " + clsCommon.myCstr(obj.Document_Total - dblRemAmt) + " where Document_No='" + obj.RefDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "select Balance_Amt  from TSPL_Recurring_Payable_INVOICE_Head where  Document_No='" + obj.RefDocNo + "'"
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) < 0 Then
                    Throw New Exception("Balance is Going to Negative For AP Invoice No : " + obj.RefDocNo)
                End If

            End If

            Qry = "Update TSPL_Recurring_Payable_INVOICE_Head set Posting_Date=NULL, Modify_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

Public Class clsRecurringPayableInvoiceDetail
#Region "Variables"
    Public chrgcatcode As String = Nothing
    Public chrgcatdesc As String = Nothing
    Public chrgcatvalue As String = Nothing
    Public chritemcode As String = Nothing
    Public chritemdesc As String = Nothing


    '' priti starts here
    Public Document_No As String = Nothing
    Public Unit_code As String = Nothing
    Public Location As String = Nothing
    Public Detail_Line_No As Integer = 0
    Public unit_cost_tax As Double = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public MRP As Double = 0
    Public Batch_No As String = Nothing
    Public MFG_Date As String = Nothing
    Public Expiry_Date As String = Nothing
    Public Adjustment_Account As String = Nothing
    Public Description As String = Nothing


    '' priti ends here
    Public Deduction_Code As String = Nothing
    Public Deduction_Name As String = Nothing ''Not a table column
    Public Deduction_Section As String = Nothing ''Not a table column


    Public GL_Account_Code As String = Nothing
    Public GL_Account_Desc As String = Nothing
    Public Amount As Double = 0
    Public Discount_Per As Double = 0
    Public Discount As Double = 0
    Public Amount_less_Discount As String = Nothing
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
    Public Total_Tax As Double = 0
    Public Total_Amount As Double = 0
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public AddChargeCode As String = Nothing
    Public AddChargeDesc As String = Nothing
    Public is_Unclaimed_Tax As Boolean = False


    Public Invoice_No As String = Nothing
    Public Invoice_Type As String = Nothing
    Public Landed_Amount As Decimal = 0
    Public Item_Rate As Decimal = 0
    Public Abatement_Per As Double = 0
    Public Abatement_Amt As Double = 0

    Public TAX1_Base_Amt As Double = 0
    Public TAX2_Base_Amt As Double = 0
    Public TAX3_Base_Amt As Double = 0
    Public TAX4_Base_Amt As Double = 0
    Public TAX5_Base_Amt As Double = 0
    Public TAX6_Base_Amt As Double = 0
    Public TAX7_Base_Amt As Double = 0
    Public TAX8_Base_Amt As Double = 0
    Public TAX9_Base_Amt As Double = 0
    Public TAX10_Base_Amt As Double = 0
    Public DeductionCode As String = ""
    Public DeductionDesc As String = ""
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsRecurringPayableInvoiceDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsRecurringPayableInvoiceDetail In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "item_code", obj.chritemcode)
                clsCommon.AddColumnsForChange(coll, "item_desc", obj.chritemdesc)
                clsCommon.AddColumnsForChange(coll, "charge_cat_code", obj.chrgcatcode)
                clsCommon.AddColumnsForChange(coll, "charge_cat_desc", obj.chrgcatdesc)
                clsCommon.AddColumnsForChange(coll, "charge_cat_charges", obj.chrgcatvalue)

                ' clsCommon.AddColumnsForChange(coll, "charge_cat_desc", obj.chrgcatdesc)
                'clsCommon.AddColumnsForChange(coll, "charge_cat_charges", obj.chrgcatvalue)
                clsCommon.AddColumnsForChange(coll, "DeductionCode", clsCommon.myCstr(obj.DeductionCode))
                clsCommon.AddColumnsForChange(coll, "Deduction_Desc", clsCommon.myCstr(obj.DeductionDesc))

                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Detail_Line_No", obj.Detail_Line_No)
                clsCommon.AddColumnsForChange(coll, "Deduction_Code", obj.Deduction_Code, True)
                clsCommon.AddColumnsForChange(coll, "GL_Account_Code", obj.GL_Account_Code)
                clsCommon.AddColumnsForChange(coll, "GL_Account_Desc", obj.GL_Account_Desc)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Discount_Per", obj.Discount_Per)
                clsCommon.AddColumnsForChange(coll, "Discount", obj.Discount)
                clsCommon.AddColumnsForChange(coll, "Amount_less_Discount", obj.Amount_less_Discount)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Tax", obj.Total_Tax)
                clsCommon.AddColumnsForChange(coll, "Total_Amount", obj.Total_Amount)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
                clsCommon.AddColumnsForChange(coll, "AddChargeCode", obj.AddChargeCode)
                clsCommon.AddColumnsForChange(coll, "AddChargeDesc", obj.AddChargeDesc)
                clsCommon.AddColumnsForChange(coll, "is_Unclaimed_Tax", IIf(obj.is_Unclaimed_Tax, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.Invoice_No)
                clsCommon.AddColumnsForChange(coll, "Invoice_Type", obj.Invoice_Type)
                clsCommon.AddColumnsForChange(coll, "Landed_Amount", obj.Landed_Amount)
                clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Item_Rate)
                clsCommon.AddColumnsForChange(coll, "Abatement_Per", obj.Abatement_Per)
                clsCommon.AddColumnsForChange(coll, "Abatement_Amt", obj.Abatement_Amt)

                clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_Recurring_payable_invoice_detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

End Class