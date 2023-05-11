Imports common
Imports System.Data.SqlClient
Public Class clsSNSalesQuotationHead
#Region "Variables"
    Public Cust_PO_No As String = Nothing
    Public Price_Group_Code As String = Nothing
    Public Approvel_Required As Double = 0
    Public Is_Approved As Double = 0
    Public Approved_Date As Date = Nothing
    Public Approval_Level As String = String.Empty
    Public Is_Dis_Type_Rate As Double = 0
    Public Approved_By As String = Nothing
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Delivery_date As DateTime = Nothing
    Public SalesOrder_Type As String = Nothing
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Bill_To_Location As String = Nothing
    Public BillToLocationName As String = Nothing
    Public Ship_To_Location As String = Nothing
    Public ShipToLocationName As String = Nothing
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
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
    Public Total_Tax_Amt As Double = 0
    Public Discount_Base As Double = 0
    Public Discount_Amt As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public Total_Amt As Double = 0
    Public Mode_Of_Transport As String = Nothing
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Due_Date As String = Nothing
    Public Posting_Date As DateTime? = Nothing
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Abandonment_No As Double = 0
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

    Public Modify_By As String = Nothing
    Public Modify_Date As DateTime = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As DateTime = Nothing
    Public Against_Quotation_No As String = Nothing
    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Arr As List(Of clsSNSalesQuotationDetail) = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public PROJECT_ID As String = Nothing
    Public Price_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public HeadDisc_Per As Double = 0
    Public HeadDisc_Amt As Double = 0
    Public TotCashDiscAmt As Double = 0
    Public is_taxable As Integer = 0

#End Region

    ''Note Very Important If any change mad in SO Head or SO Detail table allso update it's History table.

    Public Function SaveData(ByVal obj As clsSNSalesQuotationHead, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            checkSaveNotification(obj.Document_Date, obj.Customer_Code, trans)
            If isMakeAbandomentNo Then
                isSaved = isSaved AndAlso clsPurchaseOrderHeadHist.SaveData(obj.Document_Code, clsCommon.myCdbl(obj.Abandonment_No + 1), trans)
            End If

            Dim qry As String = "delete from TSPL_SD_SALES_Quotation_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNSalesQuotation, clsDocTransactionType.SNQuotationFinishedGoods, obj.Bill_To_Location)

                'If clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.SNSalesOrder, clsDocTransactionType.SNQuotationFinishedGoods, obj.Bill_To_Location)
                'Else
                '    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.SNSalesOrder, clsDocTransactionType.SNQuotationOther, obj.Bill_To_Location)
                'End If
            End If
            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Delivery_date", clsCommon.GetPrintDate(obj.Delivery_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "SalesOrder_Type", obj.SalesOrder_Type)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            'clsCommon.AddColumnsForChange(coll, "Customer_Name", obj.Customer_Name)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Location", obj.Ship_To_Location)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
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
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.Discount_Base)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)

            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Against_Quotation_No", obj.Against_Quotation_No, True)
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
            clsCommon.AddColumnsForChange(coll, "Approvel_Required", obj.Approvel_Required)
            clsCommon.AddColumnsForChange(coll, "Approval_Level", obj.Approval_Level)
            clsCommon.AddColumnsForChange(coll, "Is_Dis_Type_Rate", obj.Is_Dis_Type_Rate)
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

            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code, True)
            clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)
            clsCommon.AddColumnsForChange(coll, "is_taxable", obj.is_taxable)

            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", obj.ApplicableFrom, True)
            '' End currencyconversion

            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Route_Desc", obj.Route_Desc)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_Per", obj.HeadDisc_Per)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_Amt", obj.HeadDisc_Amt)
            clsCommon.AddColumnsForChange(coll, "TotCashDiscAmt", obj.TotCashDiscAmt)
            clsCommon.AddColumnsForChange(coll, "Price_Group_Code", obj.Price_Group_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_PO_No", obj.Cust_PO_No)
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
              
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALES_Quotation_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                If isMakeAbandomentNo Then
                    obj.Abandonment_No = obj.Abandonment_No + 1
                    clsCommon.AddColumnsForChange(coll, "Abandonment_No", obj.Abandonment_No)
                End If
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALES_Quotation_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SALES_Quotation_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsSNSalesQuotationDetail.SaveData(obj.Document_Code, Arr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)

            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_SD_SALES_Quotation_HEAD", trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function checkSaveNotification(ByVal strDate As String, ByVal strCustCode As String, ByVal trans As SqlTransaction)
        Try
            Dim CreditLimit As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Credit_Limit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + strCustCode + "'", trans))
            Dim qry As String
            Dim dt As DataTable = clsScreenNotificationSchedule.GetScreenNotificationInfo(clsUserMgtCode.frmSNSalesOrder, trans)
            For Each dr As DataRow In dt.Rows
                'Criteria, Notification, Validation
                If clsCommon.CompairString(dr("Criteria"), "Credit days") = CompairStringResult.Equal Then
                    qry = "Select COUNT(*) from TSPL_SD_SALES_Quotation_HEAD WHERE Status<>1 AND Customer_Code='" + strCustCode + "' AND CONVERT(Date, Due_Date,103)<'" + clsCommon.GetPrintDate(strDate, "dd/MMM/yyyy") + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                        If clsCommon.CompairString(dr("Validation"), "Warning") = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow(dr("Notification"))
                        ElseIf clsCommon.CompairString(dr("Validation"), "Full Stop") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        End If
                    End If
                ElseIf clsCommon.CompairString(dr("Criteria"), "Credit Amount") = CompairStringResult.Equal Then
                    qry = "Select SUM(Total_Amt) from TSPL_SD_SALES_Quotation_HEAD WHERE Status<>1 AND Customer_Code='" + strCustCode + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > CreditLimit Then
                        If clsCommon.CompairString(dr("Validation"), "Warning") = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow(dr("Notification"))
                        ElseIf clsCommon.CompairString(dr("Validation"), "Full Stop") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        End If
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function checkApprovalNotification(ByVal strDate As String, ByVal strCustCode As String, ByVal trans As SqlTransaction)
        Try
            Dim CreditLimit As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Credit_Limit from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + strCustCode + "'", trans))
            Dim qry As String
            Dim dt As DataTable = clsScreenNotificationSchedule.GetScreenNotificationInfo(clsUserMgtCode.frmSNSalesOrder, trans)
            For Each dr As DataRow In dt.Rows
                'Criteria, Notification, Validation
                If clsCommon.CompairString(dr("Criteria"), "Credit days") = CompairStringResult.Equal Then
                    qry = "Select COUNT(*) from TSPL_SD_SALES_Quotation_HEAD WHERE Status<>1 AND Customer_Code='" + strCustCode + "' AND CONVERT(Date, Due_Date,103)<'" + clsCommon.GetPrintDate(strDate, "dd/MMM/yyyy") + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
                        If clsCommon.CompairString(dr("Validation"), "Stop Approval") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        ElseIf clsCommon.CompairString(dr("Validation"), "Full Stop") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        End If
                    End If
                ElseIf clsCommon.CompairString(dr("Criteria"), "Credit Amount") = CompairStringResult.Equal Then
                    qry = "Select SUM(Total_Amt) from TSPL_SD_SALES_Quotation_HEAD WHERE Status<>1 AND Customer_Code='" + strCustCode + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > CreditLimit Then
                        If clsCommon.CompairString(dr("Validation"), "Stop Approval") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        ElseIf clsCommon.CompairString(dr("Validation"), "Full Stop") = CompairStringResult.Equal Then
                            Throw New Exception(dr("Notification"))
                        End If
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsSNSalesQuotationHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSNSalesQuotationHead
        Dim obj As clsSNSalesQuotationHead = Nothing
        Dim qry As String = "SELECT TSPL_SD_SALES_Quotation_HEAD.is_taxable, TSPL_SD_SALES_Quotation_HEAD.Is_Dis_Type_Rate,TSPL_SD_SALES_Quotation_HEAD.Approval_Level,TSPL_SD_SALES_Quotation_HEAD.Cust_PO_No,TSPL_SD_SALES_Quotation_HEAD.price_group_code, TSPL_SD_SALES_Quotation_HEAD.Route_No,TSPL_SD_SALES_Quotation_HEAD.Route_Desc,TSPL_SD_SALES_Quotation_HEAD.Price_Code,TSPL_SD_SALES_Quotation_HEAD.Document_Code,TSPL_SD_SALES_Quotation_HEAD.SalesOrder_Type ,TSPL_SD_SALES_Quotation_HEAD.Document_Date, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_SD_SALES_Quotation_HEAD.Status, " & _
        " TSPL_SD_SALES_Quotation_HEAD.On_Hold,TSPL_SD_SALES_Quotation_HEAD.Ref_No,TSPL_SD_SALES_Quotation_HEAD.Description,TSPL_SD_SALES_Quotation_HEAD.Remarks, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Tax_Group,TSPL_SD_SALES_Quotation_HEAD.Bill_To_Location,TSPL_SD_SALES_Quotation_HEAD.Ship_To_Location, " & _
        " TSPL_SD_SALES_Quotation_HEAD.TAX1,TSPL_SD_SALES_Quotation_HEAD.TAX1_Rate,TSPL_SD_SALES_Quotation_HEAD.TAX1_Amt,TSPL_SD_SALES_Quotation_HEAD.TAX1_Base_Amt, " & _
        " TSPL_SD_SALES_Quotation_HEAD.TAX2,TSPL_SD_SALES_Quotation_HEAD.TAX2_Rate,TSPL_SD_SALES_Quotation_HEAD.TAX2_Amt,TSPL_SD_SALES_Quotation_HEAD.TAX2_Base_Amt, " & _
        " TSPL_SD_SALES_Quotation_HEAD.TAX3,TSPL_SD_SALES_Quotation_HEAD.TAX3_Rate,TSPL_SD_SALES_Quotation_HEAD.TAX3_Amt,TSPL_SD_SALES_Quotation_HEAD.TAX3_Base_Amt, " & _
        " TSPL_SD_SALES_Quotation_HEAD.TAX4,TSPL_SD_SALES_Quotation_HEAD.TAX4_Rate,TSPL_SD_SALES_Quotation_HEAD.TAX4_Amt,TSPL_SD_SALES_Quotation_HEAD.TAX4_Base_Amt, " & _
        " TSPL_SD_SALES_Quotation_HEAD.TAX5,TSPL_SD_SALES_Quotation_HEAD.TAX5_Rate,TSPL_SD_SALES_Quotation_HEAD.TAX5_Amt,TSPL_SD_SALES_Quotation_HEAD.TAX5_Base_Amt, " & _
        " TSPL_SD_SALES_Quotation_HEAD.TAX6,TSPL_SD_SALES_Quotation_HEAD.TAX6_Rate,TSPL_SD_SALES_Quotation_HEAD.TAX6_Amt,TSPL_SD_SALES_Quotation_HEAD.TAX6_Base_Amt, " & _
        " TSPL_SD_SALES_Quotation_HEAD.TAX7,TSPL_SD_SALES_Quotation_HEAD.TAX7_Rate,TSPL_SD_SALES_Quotation_HEAD.TAX7_Amt,TSPL_SD_SALES_Quotation_HEAD.TAX7_Base_Amt, " & _
        " TSPL_SD_SALES_Quotation_HEAD.TAX8,TSPL_SD_SALES_Quotation_HEAD.TAX8_Rate,TSPL_SD_SALES_Quotation_HEAD.TAX8_Amt,TSPL_SD_SALES_Quotation_HEAD.TAX8_Base_Amt, " & _
        " TSPL_SD_SALES_Quotation_HEAD.TAX9,TSPL_SD_SALES_Quotation_HEAD.TAX9_Rate,TSPL_SD_SALES_Quotation_HEAD.TAX9_Amt,TSPL_SD_SALES_Quotation_HEAD.TAX9_Base_Amt, " & _
        " TSPL_SD_SALES_Quotation_HEAD.TAX10,TSPL_SD_SALES_Quotation_HEAD.TAX10_Rate,TSPL_SD_SALES_Quotation_HEAD.TAX10_Amt,TSPL_SD_SALES_Quotation_HEAD.TAX10_Base_Amt, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Discount_Base,TSPL_SD_SALES_Quotation_HEAD.Discount_Amt,TSPL_SD_SALES_Quotation_HEAD.Amount_Less_Discount, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Total_Tax_Amt,TSPL_SD_SALES_Quotation_HEAD.Total_Amt,TSPL_SD_SALES_Quotation_HEAD.Mode_Of_Transport," & _
        " TSPL_SD_SALES_Quotation_HEAD.Comments,TSPL_SD_SALES_Quotation_HEAD.Comp_Code,TSPL_SD_SALES_Quotation_HEAD.Terms_Code,TSPL_SD_SALES_Quotation_HEAD.Due_Date , " & _
        " TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName, " & _
        " TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_SD_SALES_Quotation_HEAD.Posting_Date, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Delivery_date,TSPL_SD_SALES_Quotation_HEAD.Dept,TSPL_SD_SALES_Quotation_HEAD.Dept_Desc,TSPL_SD_SALES_Quotation_HEAD.Item_Type, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Modify_By,TSPL_SD_SALES_Quotation_HEAD.Modify_Date,TSPL_SD_SALES_Quotation_HEAD.Created_By," & _
        " TSPL_SD_SALES_Quotation_HEAD.Created_Date,TSPL_SD_SALES_Quotation_HEAD.Abandonment_No,TSPL_SD_SALES_Quotation_HEAD.Against_Quotation_No, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Code1,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Name1,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Amt1, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Code2,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Name2,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Amt2, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Code3,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Name3,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Amt3, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Code4,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Name4,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Amt4, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Code5,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Name5,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Amt5, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Code6,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Name6,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Amt6, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Code7,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Name7,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Amt7, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Code8,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Name8,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Amt8, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Code9 ,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Name9,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Amt9 ," & _
        " TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Code10 ,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Name10,TSPL_SD_SALES_Quotation_HEAD.Add_Charge_Amt10, " & _
        " TSPL_SD_SALES_Quotation_HEAD.Total_Add_Charge,TSPL_SD_SALES_Quotation_HEAD.Salesman_Code,TSPL_SD_SALES_Quotation_HEAD.Salesman_Name, " & _
        " TSPL_SD_SALES_Quotation_HEAD.CURRENCY_CODE,TSPL_SD_SALES_Quotation_HEAD.CONVRATE,TSPL_SD_SALES_Quotation_HEAD.APPLICABLEFROM ,TSPL_SD_SALES_Quotation_HEAD.PROJECT_ID,TSPL_SD_SALES_Quotation_HEAD.Approvel_Required,TSPL_SD_SALES_Quotation_HEAD.Is_Approved" & _
        " ,TSPL_SD_SALES_Quotation_HEAD.HeadDisc_Per,TSPL_SD_SALES_Quotation_HEAD.HeadDisc_Amt,TSPL_SD_SALES_Quotation_HEAD.TotCashDiscAmt FROM TSPL_SD_SALES_Quotation_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALES_Quotation_HEAD.Bill_To_Location " & _
        " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SD_SALES_Quotation_HEAD.Ship_To_Location left outer join  " & _
        " TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_SD_SALES_Quotation_HEAD.Tax_Group left " & _
        " outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_SD_SALES_Quotation_HEAD.Terms_Code left " & _
        " outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALES_Quotation_HEAD.Customer_Code  where 2=2"
        Dim whrClas As String = ""
       
        '-------richa 30/07/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
            strwherecls = FrmMainTranScreen.CustomerPermission()
        End If

        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    whrClas = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        'End If

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SALES_Quotation_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrClas = " AND TSPL_SD_SALES_Quotation_HEAD.Customer_Code in (" + strwherecls + ")"
        End If
        '-----------------------------------------------------
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SD_SALES_Quotation_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_SD_SALES_Quotation_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_SD_SALES_Quotation_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SALES_Quotation_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_SD_SALES_Quotation_HEAD.Document_Code = (select Min(Document_Code) from TSPL_SD_SALES_Quotation_HEAD where Document_Code>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SD_SALES_Quotation_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SALES_Quotation_HEAD where Document_Code<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SD_SALES_Quotation_HEAD.Document_Code = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSNSalesQuotationHead()
            obj.Approval_Level = clsCommon.myCstr(dt.Rows(0)("Approval_Level"))
            obj.Is_Dis_Type_Rate = clsCommon.myCdbl(dt.Rows(0)("Is_Dis_Type_Rate"))
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Delivery_date = clsCommon.myCstr(dt.Rows(0)("Delivery_date"))
            obj.SalesOrder_Type = clsCommon.myCstr(dt.Rows(0)("SalesOrder_Type"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
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
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Discount_Amt"))
            obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))

            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))
            obj.Approvel_Required = clsCommon.myCdbl(dt.Rows(0)("Approvel_Required"))
            obj.Is_Approved = clsCommon.myCdbl(dt.Rows(0)("Is_Approved"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Against_Quotation_No = clsCommon.myCstr(dt.Rows(0)("Against_Quotation_No"))

            obj.HeadDisc_Per = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Per"))
            obj.HeadDisc_Amt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Amt"))
            obj.TotCashDiscAmt = clsCommon.myCdbl(dt.Rows(0)("TotCashDiscAmt"))
            obj.Price_Group_Code = clsCommon.myCstr(dt.Rows(0)("Price_Group_Code"))
            obj.Cust_PO_No = clsCommon.myCstr(dt.Rows(0)("Cust_PO_No"))
            obj.Abandonment_No = clsCommon.myCdbl(dt.Rows(0)("Abandonment_No"))


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

            obj.Total_Add_Charge = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge"))

            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Salesman_Name = clsCommon.myCstr(dt.Rows(0)("Salesman_Name"))
            obj.is_taxable = clsCommon.myCdbl(dt.Rows(0)("is_taxable"))

            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION 
            qry = "SELECT TSPL_SD_SALES_Quotation_DETAIL.ItemwiseTaxCode, TSPL_SD_SALES_Quotation_DETAIL.Document_Code,TSPL_SD_SALES_Quotation_DETAIL.Line_No,TSPL_SD_SALES_Quotation_DETAIL.Status, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Row_Type,TSPL_SD_SALES_Quotation_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SALES_Quotation_DETAIL.Qty, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Balance_Qty,TSPL_SD_SALES_Quotation_DETAIL.Unit_code, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Location,TSPL_SD_SALES_Quotation_DETAIL.Item_Cost,TSPL_SD_SALES_Quotation_DETAIL.TAX1, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX1_Rate,TSPL_SD_SALES_Quotation_DETAIL.TAX1_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX2, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX2_Rate,TSPL_SD_SALES_Quotation_DETAIL.TAX2_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX3, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX3_Rate,TSPL_SD_SALES_Quotation_DETAIL.TAX3_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX4, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX4_Rate,TSPL_SD_SALES_Quotation_DETAIL.TAX4_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX5, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX5_Rate,TSPL_SD_SALES_Quotation_DETAIL.TAX5_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX6, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX6_Rate,TSPL_SD_SALES_Quotation_DETAIL.TAX6_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX7, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX7_Rate,TSPL_SD_SALES_Quotation_DETAIL.TAX7_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX8, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX8_Rate,TSPL_SD_SALES_Quotation_DETAIL.TAX8_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX9, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX9_Rate,TSPL_SD_SALES_Quotation_DETAIL.TAX9_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX10, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX10_Rate,TSPL_SD_SALES_Quotation_DETAIL.TAX10_Amt,TSPL_SD_SALES_Quotation_DETAIL.Amount, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Disc_Per,TSPL_SD_SALES_Quotation_DETAIL.Disc_Amt,TSPL_SD_SALES_Quotation_DETAIL.Amt_Less_Discount, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Total_Tax_Amt,TSPL_SD_SALES_Quotation_DETAIL.Item_Net_Amt,TSPL_LOCATION_MASTER.Location_Desc as LocationName, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX1_Base_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX2_Base_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX3_Base_Amt, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX4_Base_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX5_Base_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX6_Base_Amt, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX7_Base_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX8_Base_Amt,TSPL_SD_SALES_Quotation_DETAIL.TAX9_Base_Amt, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TAX10_Base_Amt,TSPL_SD_SALES_Quotation_DETAIL.Specification, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Remarks,TSPL_SD_SALES_Quotation_DETAIL.MRP,TSPL_SD_SALES_Quotation_DETAIL.Assessable, " & _
            " TSPL_SD_SALES_Quotation_DETAIL.MRP,TSPL_SD_SALES_Quotation_DETAIL.Scheme_Applicable,TSPL_SD_SALES_Quotation_DETAIL.Scheme_Code, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Scheme_Item,TSPL_SD_SALES_Quotation_DETAIL.Item_Tax,TSPL_SD_SALES_Quotation_DETAIL.Total_MRP_Amt, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Total_Basic_Amt,TSPL_SD_SALES_Quotation_DETAIL.Total_Disc_Amt,TSPL_SD_SALES_Quotation_DETAIL.Cust_Discount, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Total_Cust_Discount,TSPL_SD_SALES_Quotation_DETAIL.ActualRate,TSPL_SD_SALES_Quotation_DETAIL.Cust_DiscountQty, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Price_code,TSPL_SD_SALES_Quotation_DETAIL.Abatement_Per,TSPL_SD_SALES_Quotation_DETAIL.Abatement_Amt, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.FOC_Item,TSPL_SD_SALES_Quotation_DETAIL.Item_Weight,TSPL_SD_SALES_Quotation_DETAIL.Price_Date,TSPL_SD_SALES_Quotation_DETAIL.Batch_No,TSPL_SD_SALES_Quotation_DETAIL.MFG_Date,TSPL_SD_SALES_Quotation_DETAIL.Expiry_Date, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.TotalItem_Weight,TSPL_SD_SALES_Quotation_DETAIL.Conv_Factor,TSPL_SD_SALES_Quotation_DETAIL.Purchase_Cost,TSPL_SD_SALES_Quotation_DETAIL.OrgRate, " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Bin_No,TSPL_SD_SALES_Quotation_DETAIL.vendor_code,TSPL_SD_SALES_Quotation_DETAIL.vendor_desc,TSPL_SD_SALES_Quotation_DETAIL.PrincipleCode,TSPL_SD_SALES_Quotation_DETAIL.PrincipleDesc,TSPL_SD_SALES_Quotation_DETAIL.Markup_On,TSPL_SD_SALES_Quotation_DETAIL.Markup_Percent,TSPL_SD_SALES_Quotation_DETAIL.Landing_Cost,TSPL_SD_SALES_Quotation_DETAIL.HeadDiscAmt,TSPL_SD_SALES_Quotation_DETAIL.CustDiscPer,TSPL_SD_SALES_Quotation_DETAIL.CasdDiscScheme_Code " & _
            "FROM TSPL_SD_SALES_Quotation_DETAIL left outer join TSPL_LOCATION_MASTER on " & _
            "TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALES_Quotation_DETAIL.Location left outer join TSPL_ITEM_MASTER on " & _
            "TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SALES_Quotation_DETAIL.Item_Code where " & _
            "TSPL_SD_SALES_Quotation_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_SD_SALES_Quotation_DETAIL.Line_No asc"

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsSNSalesQuotationDetail)
                Dim objTr As clsSNSalesQuotationDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSNSalesQuotationDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Line_No = Convert.ToInt32(clsCommon.myCdbl(dr("Line_No")))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))


                    objTr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Location = clsCommon.myCstr(dr("Location"))
                    objTr.LocationName = clsCommon.myCstr(dr("LocationName"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
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
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
                    objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))

                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    'objTr.IsUsedInGRN = clsCommon.myCBool(dr("IsUsedInGRN"))
                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Scheme_Applicable = clsCommon.myCstr(dr("Scheme_Applicable"))
                    objTr.Scheme_Code = clsCommon.myCstr(dr("Scheme_Code"))
                    objTr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                    objTr.Item_Tax = clsCommon.myCdbl(dr("Item_Tax"))
                    objTr.Total_MRP_Amt = clsCommon.myCdbl(dr("Total_MRP_Amt"))
                    objTr.Total_Basic_Amt = clsCommon.myCdbl(dr("Total_Basic_Amt"))
                    objTr.Total_Disc_Amt = clsCommon.myCdbl(dr("Total_Disc_Amt"))
                    objTr.Cust_Discount = clsCommon.myCdbl(dr("Cust_Discount"))
                    objTr.Total_Cust_Discount = clsCommon.myCdbl(dr("Total_Cust_Discount"))
                    objTr.ActualRate = clsCommon.myCdbl(dr("ActualRate"))
                    objTr.Cust_DiscountQty = clsCommon.myCdbl(dr("Cust_DiscountQty"))
                    objTr.Price_code = clsCommon.myCstr(dr("Price_code"))
                    objTr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                    objTr.Abatement_Per = clsCommon.myCdbl(dr("Abatement_Per"))
                    objTr.Abatement_Amt = clsCommon.myCdbl(dr("Abatement_Amt"))
                    objTr.FOC_Item = clsCommon.myCdbl(dr("FOC_Item"))
                    objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    If dr("MFG_Date") IsNot DBNull.Value Then
                        objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    If dr("Expiry_Date") IsNot DBNull.Value Then
                        objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                    End If
                    objTr.Item_Weight = clsCommon.myCdbl(dr("Item_Weight"))
                    objTr.TotalItem_Weight = clsCommon.myCdbl(dr("TotalItem_Weight"))
                    objTr.Conv_Factor = clsCommon.myCdbl(dr("Conv_Factor"))

                    objTr.Markup_On = clsCommon.myCstr(dr("Markup_On"))
                    objTr.Markup_Percent = clsCommon.myCdbl(dr("Markup_Percent"))
                    objTr.Landing_Cost = clsCommon.myCdbl(dr("Landing_Cost"))
                    objTr.HeadDiscAmt = clsCommon.myCdbl(dr("HeadDiscAmt"))
                    objTr.CustDiscPer = clsCommon.myCdbl(dr("CustDiscPer"))
                    objTr.CasdDiscScheme_Code = clsCommon.myCstr(dr("CasdDiscScheme_Code"))
                    objTr.Purchase_Cost = clsCommon.myCdbl(dr("Purchase_Cost"))
                    objTr.OrgRate = clsCommon.myCdbl(dr("OrgRate"))
                    objTr.PrincipleCode = clsCommon.myCstr(dr("PrincipleCode"))
                    objTr.PrincipleDesc = clsCommon.myCstr(dr("PrincipleDesc"))
                    objTr.vendor_code = clsCommon.myCstr(dr("vendor_code"))
                    objTr.vendor_desc = clsCommon.myCstr(dr("vendor_desc"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))
                    'objTr.Assessable = clsCommon.myCdbl(dr("Assessable"))
                    'objTr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))
                    objTr.ItemwiseTaxCode = clsCommon.myCstr(dr("ItemwiseTaxCode"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Sale Quotation No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsSNSalesQuotationHead = clsSNSalesQuotationHead.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            checkApprovalNotification(obj.Document_Date, obj.Customer_Code, trans)
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Sale Quotation No " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If

            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_SD_SALES_Quotation_HEAD", "Document_Code", obj.Document_Code, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If
            
            Dim qry As String = "Update TSPL_SD_SALES_Quotation_HEAD set Status=1, Posting_Date='" + strPostDate + "', Modify_Date='" + strPostDate + "' where Document_Code='" + strDocNo + "' " ''server date not converted into proper format in modify date,so do change 19/10/2015
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
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
            Throw New Exception("Sale Order No not found to Delete")
        End If
        Dim obj As clsSNSalesQuotationHead = clsSNSalesQuotationHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_SD_SALES_Quotation_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_SALES_Quotation_HEAD where Document_Code='" + strCode + "'"
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

    Public Shared Function IsValidCustomer(ByVal ArrPONo As List(Of String), ByVal strVendorCode As String) As Boolean
        If ArrPONo IsNot Nothing AndAlso ArrPONo.Count > 0 Then
            Dim qry As String = "select TSPL_SD_SALES_Quotation_HEAD.Document_Code,TSPL_SD_SALES_Quotation_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_SALES_Quotation_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALES_Quotation_HEAD.Customer_Code where Document_Code  in (" + clsCommon.GetMulcallString(ArrPONo) + ") and Customer_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "Order No:" + clsCommon.myCstr(dr("Document_Code")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Customer_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Customer_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Shared Function IsValidTaxGroupForPO(ByVal ArrPONo As List(Of String), ByVal strTaxGroupCode As String) As Boolean
        If ArrPONo IsNot Nothing AndAlso ArrPONo.Count > 0 Then
            Dim qry As String = "select Document_Code,Tax_Group from TSPL_SD_SALES_Quotation_HEAD where Document_Code  in (" + clsCommon.GetMulcallString(ArrPONo) + ") and Tax_Group not in ('" + strTaxGroupCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "PO No:" + clsCommon.myCstr(dr("Document_Code")) + " .Tax Group is: " + clsCommon.myCstr(dr("Tax_Group"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function


    Public Shared Function IsValidProjectForSO(ByVal strPONo As String, ByVal strProject As String) As String
        Dim strProj As String = clsDBFuncationality.getSingleValue("select PROJECT_ID from TSPL_SD_SALES_Quotation_HEAD where Document_Code ='" + strPONo + "'")
        Return strProj
    End Function
End Class

Public Class clsSNSalesQuotationDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Status As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0 '
    Public Quotation_Code As String = Nothing
    Public OriginalReqQty As Double = 0 ''Not a tale field
    Public Balance_Qty As Double = 0 '
    Public Unit_code As String = Nothing '
    Public Location As String = Nothing '
    Public LocationName As String = Nothing 'Not a Table Field
    Public Item_Cost As Double = 0 '
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
    Public Amount As Double = 0
    Public Disc_Per As Double = 0
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public SOTax_Group As String = Nothing 'Not a table field
    Public Assessable As Double = 0
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public MRP As Double = 0
    Public is_taxable As Double = 0
    Public OrderTax_Group As String 'Not a table field

    Public Scheme_Applicable As String = Nothing
    Public Scheme_Code As String = Nothing
    Public Scheme_Item As String = Nothing
    Public Item_Tax As Double = 0
    Public Total_MRP_Amt As Double = 0
    Public Total_Basic_Amt As Double = 0
    Public Total_Disc_Amt As Double = 0
    Public Cust_Discount As Double = 0
    Public Total_Cust_Discount As Double = 0
    Public Price_Amount1 As Double = 0
    Public Price_Amount2 As Double = 0
    Public Price_Amount3 As Double = 0
    Public Price_Amount4 As Double = 0
    Public Price_Amount5 As Double = 0
    Public Price_Amount6 As Double = 0
    Public Price_Amount7 As Double = 0
    Public Price_Amount8 As Double = 0
    Public Price_Amount9 As Double = 0
    Public Price_Amount10 As Double = 0
    Public ActualRate As Double = 0
    Public Cust_DiscountQty As Double = 0
    Public Price_code As String = Nothing
    Public Abatement_Per As Double = 0
    Public Abatement_Amt As Double = 0
    Public FOC_Item As Double = 0
    Public Price_Date As String = Nothing
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Item_Weight As Double = 0
    Public Conv_Factor As Double = 0
    Public TotalItem_Weight As Double = 0
    Public Markup_On As String = Nothing
    Public Markup_Percent As Double = 0
    Public Landing_Cost As Double = 0
    Public HeadDiscAmt As Double = 0
    Public CustDiscPer As Double = 0
    Public CasdDiscScheme_Code As String = Nothing
    Public Purchase_Cost As Double = 0
    Public OrgRate As Double = 0
    Public PrincipleCode As String = Nothing
    Public PrincipleDesc As String = Nothing
    Public vendor_code As String = Nothing
    Public vendor_desc As String = Nothing
    Public Bin_No As String = Nothing
    Public ItemwiseTaxCode As String = ""
#End Region

    ''Note Very Important If any change mad in PO Head or PO Detail table allso update it's History table.
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsSNSalesQuotationDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSNSalesQuotationDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Per", obj.Disc_Per)
                clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", obj.Amt_Less_Discount)
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
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)

                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "ItemwiseTaxCode", obj.ItemwiseTaxCode)


                Dim strSql As String = "select top 1 TSPL_ITEM_PRICE_MASTER.Price_Amount1 ,TSPL_ITEM_PRICE_MASTER.Price_Amount2 , " & _
                "TSPL_ITEM_PRICE_MASTER.Price_Amount3 ,TSPL_ITEM_PRICE_MASTER.Price_Amount4 ,TSPL_ITEM_PRICE_MASTER.Price_Amount5 , " & _
                "TSPL_ITEM_PRICE_MASTER.Price_Amount6 ,TSPL_ITEM_PRICE_MASTER.Price_Amount7 ,TSPL_ITEM_PRICE_MASTER.Price_Amount8 , " & _
                "TSPL_ITEM_PRICE_MASTER.Price_Amount9 ,TSPL_ITEM_PRICE_MASTER.Price_Amount10 from TSPL_ITEM_PRICE_MASTER " & _
               "where  TSPL_ITEM_PRICE_MASTER.Price_Code='" & obj.Price_code & "' and  TSPL_ITEM_PRICE_MASTER.Item_Code='" & obj.Item_Code & "' and  " & _
               "TSPL_ITEM_PRICE_MASTER.Item_Basic_Net=" & obj.MRP & " and TSPL_ITEM_PRICE_MASTER.UOM='" & obj.Unit_code & "' "
                Dim dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(strSql, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Price_Amount1 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount1"))
                    obj.Price_Amount2 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount2"))
                    obj.Price_Amount3 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount3"))
                    obj.Price_Amount4 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount4"))
                    obj.Price_Amount5 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount5"))
                    obj.Price_Amount6 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount6"))
                    obj.Price_Amount7 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount7"))
                    obj.Price_Amount8 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount8"))
                    obj.Price_Amount9 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount9"))
                    obj.Price_Amount10 = clsCommon.myCdbl(dt.Rows(0).Item("Price_Amount10"))
                End If

                clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", IIf(obj.Scheme_Applicable = "Yes", "Y", "N"))
                clsCommon.AddColumnsForChange(coll, "Scheme_Code", obj.Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", IIf(obj.Scheme_Item = "Yes", "Y", "N"))
                clsCommon.AddColumnsForChange(coll, "Item_Tax", obj.Item_Tax)
                clsCommon.AddColumnsForChange(coll, "Total_MRP_Amt", obj.Total_MRP_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Total_Basic_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Disc_Amt", obj.Total_Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Cust_Discount", obj.Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Total_Cust_Discount", obj.Total_Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Price_Amount1", obj.Price_Amount1)
                clsCommon.AddColumnsForChange(coll, "Price_Amount2", obj.Price_Amount2)
                clsCommon.AddColumnsForChange(coll, "Price_Amount3", obj.Price_Amount3)
                clsCommon.AddColumnsForChange(coll, "Price_Amount4", obj.Price_Amount4)
                clsCommon.AddColumnsForChange(coll, "Price_Amount5", obj.Price_Amount5)
                clsCommon.AddColumnsForChange(coll, "Price_Amount6", obj.Price_Amount6)
                clsCommon.AddColumnsForChange(coll, "Price_Amount7", obj.Price_Amount7)
                clsCommon.AddColumnsForChange(coll, "Price_Amount8", obj.Price_Amount8)
                clsCommon.AddColumnsForChange(coll, "Price_Amount9", obj.Price_Amount9)
                clsCommon.AddColumnsForChange(coll, "Price_Amount10", obj.Price_Amount10)
                clsCommon.AddColumnsForChange(coll, "ActualRate", obj.ActualRate)
                clsCommon.AddColumnsForChange(coll, "Cust_DiscountQty", obj.Cust_DiscountQty)
                clsCommon.AddColumnsForChange(coll, "Price_code", obj.Price_code)
                clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Abatement_Per", obj.Abatement_Per)
                clsCommon.AddColumnsForChange(coll, "Abatement_Amt", obj.Abatement_Amt)
                clsCommon.AddColumnsForChange(coll, "FOC_Item", obj.FOC_Item)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                If obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd-MMM-yyyy"))
                End If
                If obj.Expiry_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd-MMM-yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Item_Weight", obj.Item_Weight)
                clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                clsCommon.AddColumnsForChange(coll, "TotalItem_Weight", obj.TotalItem_Weight)
                clsCommon.AddColumnsForChange(coll, "Markup_On", obj.Markup_On)
                clsCommon.AddColumnsForChange(coll, "Markup_Percent", obj.Markup_Percent)
                clsCommon.AddColumnsForChange(coll, "Landing_Cost", obj.Landing_Cost)
                clsCommon.AddColumnsForChange(coll, "HeadDiscAmt", obj.HeadDiscAmt)
                clsCommon.AddColumnsForChange(coll, "CustDiscPer", obj.CustDiscPer)
                clsCommon.AddColumnsForChange(coll, "CasdDiscScheme_Code", obj.CasdDiscScheme_Code)
                clsCommon.AddColumnsForChange(coll, "Purchase_Cost", obj.Purchase_Cost)
                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)
                clsCommon.AddColumnsForChange(coll, "PrincipleCode", obj.PrincipleCode)
                clsCommon.AddColumnsForChange(coll, "PrincipleDesc", obj.PrincipleDesc)
                clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendor_code)
                clsCommon.AddColumnsForChange(coll, "vendor_desc", obj.vendor_desc)
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.Bin_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SALES_Quotation_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalancePOQty(ByVal strPOCode As String, ByVal strICode As String, ByVal strCurrGRNNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_SD_SALES_Quotation_DETAIL.Item_Code as ICode,TSPL_SD_SALES_Quotation_DETAIL.Qty as Qty,1 as RI from TSPL_SD_SALES_Quotation_DETAIL left outer join TSPL_SD_SALES_Quotation_HEAD on TSPL_SD_SALES_Quotation_HEAD.Document_Code=TSPL_SD_SALES_Quotation_DETAIL.Document_Code where TSPL_SD_SALES_Quotation_DETAIL.Status=0 and TSPL_SD_SALES_Quotation_HEAD.Status=1 and TSPL_SD_SALES_Quotation_DETAIL.Document_Code ='" + strPOCode + "' and TSPL_SD_SALES_Quotation_DETAIL.Item_Code='" + strICode + "' and  TSPL_SD_SALES_Quotation_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_SD_SALES_Quotation_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_SD_SALES_Quotation_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'" & _
            " union all " & _
            " select TSPL_GRN_DETAIL.Item_Code as ICode,((TSPL_GRN_DETAIL.GRN_Qty)+(TSPL_GRN_DETAIL.Leak_Qty)+(TSPL_GRN_DETAIL.Burst_Qty)+(TSPL_GRN_DETAIL.Short_Qty)) as Qty,-1 as RI from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_DETAIL.PO_Id='" + strPOCode + "'   and TSPL_GRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_GRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_GRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_GRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' and TSPL_GRN_DETAIL.GRN_No not in ('" + strCurrGRNNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompletePO(ByVal strReqCode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_SD_SALES_Quotation_DETAIL set Status ='1' where Document_Code='" + strReqCode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
    Public Shared Function GetBalanceRequitionQty(ByVal strReqCode As String, ByVal strICode As String, ByVal strCurrPONo As String) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from( " & _
            " select TSPL_SD_SALES_Quotation_DETAIL.Item_Code as ICode,TSPL_SD_SALES_Quotation_DETAIL.Qty as Qty,1 as RI from TSPL_SD_SALES_Quotation_DETAIL left outer join TSPL_SD_SALES_Quotation_HEAD on TSPL_SD_SALES_Quotation_HEAD.Document_Code=TSPL_SD_SALES_Quotation_DETAIL.Document_Code where  TSPL_SD_SALES_Quotation_HEAD.Status=1 and TSPL_SD_SALES_Quotation_DETAIL.Document_Code='" + strReqCode + "' and TSPL_SD_SALES_Quotation_DETAIL.Item_Code='" + strICode + "'" & _
            " union all " & _
            "select TSPL_SD_SALES_ORDER_DETAIL.Item_Code as ICode,TSPL_SD_SALES_ORDER_DETAIL.Qty as Qty,-1 as RI from TSPL_SD_SALES_ORDER_DETAIL left outer join TSPL_SD_SALES_ORDER_HEAD on TSPL_SD_SALES_ORDER_HEAD.Document_Code=TSPL_SD_SALES_ORDER_DETAIL.Document_Code where TSPL_SD_SALES_ORDER_DETAIL.Document_Code='" + strReqCode + "'   and TSPL_SD_SALES_ORDER_DETAIL.Item_Code='" + strICode + "' and TSPL_SD_SALES_ORDER_DETAIL.Document_Code not in ('" + strCurrPONo + "')  " & _
            ")Final"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
End Class



