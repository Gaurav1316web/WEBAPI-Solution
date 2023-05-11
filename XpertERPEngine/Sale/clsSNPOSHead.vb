Imports common
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.IO

Public Class clsSNPOSHead
#Region "Variables"
    Public Form_Id As String = ""
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing  'Not a table field
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Delivery_Type As String = Nothing
    Public Delivery_Date As DateTime = Nothing
    Public Against_Order_No As String = Nothing
    Public Bill_To_Location As String = Nothing
    Public BillToLocationName As String = Nothing 'Not a table field
    Public Remarks As String = Nothing
    Public Message As String = Nothing
    Public Payment_Mode As String = Nothing
    Public Cheque_No As String = Nothing
    Public Cheque_Date As String = Nothing
    Public Credit_Card_No As String = Nothing
    Public Credit_Card_Bank As String = Nothing
    Public Credit_Card_Batch_No As String = Nothing
    Public Credit_Card_Approval_code As String = Nothing
    Public Debit_Card_No As String = Nothing
    Public Debit_Card_Type As String = Nothing
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
    Public Discount_Base As Double = 0
    Public Discount_Amt As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Total_Amt_After_Tax As Double = 0
    Public Total_Freight As Double = 0
    Public Total_Other_Charges As Double = 0
    Public Total_Amt As Double = 0
    Public Advance_Paid As Double = 0
    Public Balance_Payment As Double = 0
    Public Amount_Paid As Double = 0
    Public Balance As Double = 0
    Public Posting_Date As DateTime? = Nothing
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Ship_To_Code As String
    Public Arr As List(Of clsSNPOSDetail) = Nothing
#End Region


    Public Function SaveData(ByVal obj As clsSNPOSHead, ByVal isNewEntry As Boolean) As Boolean

        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_SD_POS_DETAIL where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.SNPOS, "", obj.Bill_To_Location)
            End If
            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Delivery_Type", obj.Delivery_Type)
            clsCommon.AddColumnsForChange(coll, "Delivery_Date", clsCommon.GetPrintDate(obj.Delivery_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Against_Order_No", obj.Against_Order_No, True)
            clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Message", obj.Message)
            clsCommon.AddColumnsForChange(coll, "Payment_Mode", obj.Payment_Mode)
            clsCommon.AddColumnsForChange(coll, "Cheque_No", obj.Cheque_No)
            If obj.Cheque_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Cheque_Date", clsCommon.GetPrintDate(obj.Cheque_Date, "dd/MMM/yyyy"))
            End If


            clsCommon.AddColumnsForChange(coll, "Ship_To_Code", obj.Ship_To_Code)
            clsCommon.AddColumnsForChange(coll, "Credit_Card_No", obj.Credit_Card_No)
            clsCommon.AddColumnsForChange(coll, "Credit_Card_Bank", obj.Credit_Card_Bank)
            clsCommon.AddColumnsForChange(coll, "Credit_Card_Batch_No", obj.Credit_Card_Batch_No)
            clsCommon.AddColumnsForChange(coll, "Credit_Card_Approval_code", obj.Credit_Card_Approval_code)
            clsCommon.AddColumnsForChange(coll, "Debit_Card_No", obj.Debit_Card_No)
            clsCommon.AddColumnsForChange(coll, "Debit_Card_Type", obj.Debit_Card_Type)
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
            clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.Discount_Base)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Amt_After_Tax", obj.Total_Amt_After_Tax)
            clsCommon.AddColumnsForChange(coll, "Total_Freight", obj.Total_Freight)
            clsCommon.AddColumnsForChange(coll, "Total_Other_Charges", obj.Total_Other_Charges)
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Advance_Paid", obj.Advance_Paid)
            clsCommon.AddColumnsForChange(coll, "Balance_Payment", obj.Balance_Payment)
            clsCommon.AddColumnsForChange(coll, "Amount_Paid", obj.Amount_Paid)
            clsCommon.AddColumnsForChange(coll, "Balance", obj.Balance)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_POS_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_POS_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_POS_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsSNPOSDetail.SaveData(obj.Document_Code, Arr, trans)

            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_SD_POS_HEAD", trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsSNPOSHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSNPOSHead
        Dim obj As clsSNPOSHead = Nothing
        Dim qry As String = "SELECT TSPL_SD_POS_HEAD.*,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName"
        qry += "  FROM TSPL_SD_POS_HEAD"
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_POS_HEAD.Bill_To_Location "
        qry += " left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_SD_POS_HEAD.Tax_Group "
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_POS_HEAD.Customer_Code where 2=2"
        Dim whrCls As String = ""
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        'End If
        '-------richa 12/08/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
            strwherecls = FrmMainTranScreen.CustomerPermission()
        End If


        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_POS_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrCls = " AND TSPL_SD_POS_HEAD.Customer_Code in (" + strwherecls + ")"
        End If
        '-----------------------------------------------------
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SD_POS_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_SD_POS_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_SD_POS_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_POS_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SD_POS_HEAD.Document_Code = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SD_POS_HEAD.Document_Code = (select Min(Document_Code) from TSPL_SD_POS_HEAD where Document_Code>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SD_POS_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_POS_HEAD where Document_Code<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSNPOSHead()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Delivery_Type = clsCommon.myCdbl(dt.Rows(0)("Delivery_Type"))
            obj.Delivery_Date = clsCommon.GetPrintDate((dt.Rows(0)("Delivery_Date")), "dd/MMM/yyyy")
            obj.Against_Order_No = clsCommon.myCstr(dt.Rows(0)("Against_Order_No"))
            obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Message = clsCommon.myCstr(dt.Rows(0)("Message"))
            obj.Payment_Mode = clsCommon.myCstr(dt.Rows(0)("Payment_Mode"))
            obj.Cheque_No = clsCommon.myCstr(dt.Rows(0)("Cheque_No"))
            obj.Cheque_Date = clsCommon.myCstr(dt.Rows(0)("Cheque_Date"))
            obj.Credit_Card_No = clsCommon.myCstr(dt.Rows(0)("Credit_Card_No"))
            obj.Credit_Card_Bank = clsCommon.myCstr(dt.Rows(0)("Credit_Card_Bank"))
            obj.Credit_Card_Batch_No = clsCommon.myCstr(dt.Rows(0)("Credit_Card_Batch_No"))
            obj.Credit_Card_Approval_code = clsCommon.myCstr(dt.Rows(0)("Credit_Card_Approval_code"))
            obj.Debit_Card_No = clsCommon.myCstr(dt.Rows(0)("Debit_Card_No"))
            obj.Debit_Card_Type = clsCommon.myCstr(dt.Rows(0)("Debit_Card_Type"))
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
            obj.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("Discount_Base"))
            obj.Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Discount_Amt"))
            obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
            obj.Total_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Tax_Amt"))
            obj.Total_Amt_After_Tax = clsCommon.myCdbl(dt.Rows(0)("Total_Amt_After_Tax"))
            obj.Total_Other_Charges = clsCommon.myCdbl(dt.Rows(0)("Total_Other_Charges"))
            obj.Total_Freight = clsCommon.myCdbl(dt.Rows(0)("Total_Freight"))
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))
            obj.Advance_Paid = clsCommon.myCdbl(dt.Rows(0)("Advance_Paid"))
            obj.Balance_Payment = clsCommon.myCdbl(dt.Rows(0)("Balance_Payment"))
            obj.Amount_Paid = clsCommon.myCdbl(dt.Rows(0)("Amount_Paid"))
            obj.Balance = clsCommon.myCdbl(dt.Rows(0)("Balance"))
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.Ship_To_Code = clsCommon.myCstr(dt.Rows(0)("Ship_To_Code"))

            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If

            qry = "SELECT  TSPL_SD_POS_DETAIL.*,TSPL_ITEM_MASTER.Item_Desc "
            qry += " FROM TSPL_SD_POS_DETAIL "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_POS_DETAIL.Item_Code"
            qry += " where TSPL_SD_POS_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_SD_POS_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsSNPOSDetail)
                Dim objTr As clsSNPOSDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsSNPOSDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.ItemwiseTaxCode = clsCommon.myCstr(dr("ItemwiseTaxCode"))
                    objTr.Bar_code = clsCommon.myCstr(dr("Bar_code"))
                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))

                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    objTr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
                    objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))
                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Amt_After_Tax = clsCommon.myCdbl(dr("Amt_After_Tax"))
                    objTr.Freight = clsCommon.myCdbl(dr("Freight"))
                    objTr.Other_Charges = clsCommon.myCdbl(dr("Other_Charges"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
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
                    objTr.Order_Code = clsCommon.myCstr(dr("Order_Code"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsSNPOSHead = clsSNPOSHead.GetData(strDocNo, NavigatorType.Current, trans)
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("SRN No not found to Post")
            End If

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Invoice No " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If

            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_SD_POS_HEAD", "Document_Code", obj.Document_Code, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If

            Dim qry As String = ""

            Dim objShipemnt As clsSNShipmentHead = ConvertPOSToShipment(obj, trans)
            objShipemnt.Against_POS = obj.Document_Code
            objShipemnt.Form_ID = ""
            objShipemnt.SaveData(objShipemnt, True, trans)
            clsSNShipmentHead.PostData(objShipemnt.Form_ID, objShipemnt.Document_Code, trans)

            qry = "Update TSPL_SD_POS_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'  "
            qry += " where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If


        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Try
            SendSMSAndEMail(obj, strDocNo)
        Catch ex As Exception
            Throw New Exception("Data Posted Successfully." + Environment.NewLine + " But Follwing Error in Sending SMS Or Email" + Environment.NewLine + ex.Message)
        End Try
        Return True
    End Function
    ''richa Ticket No. 13/08/2014 Against Ticket No. BM00000003502
    Private Shared Sub SendEmail(ByVal lstUsers As List(Of String), ByVal isSendForApproval As Boolean, ByVal strDocNo As String)
        Try
            'Dim objEmail As clsEmailSMSSettingNew = clsEmailSMSSettingNew.GetData(clsUserMgtCode.frmSNPOS)

            'If objEmail Is Nothing Then
            '    Throw New Exception("First do email and sms setting")
            '    Return
            'End If
            'If clsCommon.myLen(objEmail.mailsubjct) <= 0 Then
            '    Throw New Exception("First do email and sms setting")
            '    Return
            'End If

            'Dim strContactPerson As String = ""
            'Dim objPos As clsSNPOSHead = clsSNPOSHead.GetData(strDocNo, NavigatorType.Current)
            'Dim strSubject As String = objEmail.mailsubjct.Replace(clsEmailSMSConstants.DeliveryNo, objPos.Document_Code)
            'strSubject = strSubject.Replace(clsEmailSMSConstants.DeliveryDate, clsCommon.GetPrintDate(objPos.Document_Date, "dd/MMM/yyyy"))

            'Dim strbody As String = objEmail.mailbody.Replace(clsEmailSMSConstants.DeliveryNo, objPos.Document_Code)
            'strbody = strbody.Replace(clsEmailSMSConstants.DeliveryDate, clsCommon.GetPrintDate(objPos.Document_Date, "dd/MMM/yyyy"))
            'strbody = strbody.Replace(clsEmailSMSConstants.CustomerNo, objPos.Customer_Code)
            'strbody = strbody.Replace(clsEmailSMSConstants.CustomerName, objPos.Customer_Name)
            'strbody = strbody.Replace(clsEmailSMSConstants.LocationCode, objPos.Bill_To_Location)
            'strbody = strbody.Replace(clsEmailSMSConstants.LocationName, objPos.BillToLocationName)
            'strbody = strbody.Replace(clsEmailSMSConstants.TotalAmount, objPos.Total_Amt)
            'strbody = strbody.Replace(clsEmailSMSConstants.CompanyName, objCommonVar.CurrentCompanyName)

            'Dim strRptPath As String = ""


            'Dim strPath As String = ""
            'For Each strUser As String In lstUsers
            '    Dim lstReceiptents As New List(Of String)
            '    Dim CCReceiptents As New List(Of String)
            '    Dim qry As String = ""
            '    Dim emailId As String = ""


            '    Dim ArrRecipients As List(Of clsEmailSMSRecipients) = clsEmailSMSRecipients.GetData(clsEmailAndSMSRecipients.strTransTrype)
            '    For Each objRece As clsEmailSMSRecipients In ArrRecipients
            '        If objRece.Is_Send_Email AndAlso clsCommon.myLen(objRece.EMail_ID) > 0 Then
            '            If clsCommon.CompairString(objRece.To_Or_CC, "CC") = CompairStringResult.Equal Then
            '                CCReceiptents.Add(objRece.EMail_ID)
            '            Else
            '                lstReceiptents.Add(objRece.EMail_ID)
            '            End If

            '        End If
            '    Next

            '    strbody = strbody.Replace(clsEmailSMSConstants.ContactPerson, strContactPerson)

            '    Dim body As String = strbody.Replace(clsEmailSMSConstants.UserCode, strUser)

            '    clsMailViaOutlook.SendEmail(strSubject, body, lstReceiptents, CCReceiptents, strPath)
            'Next

            '----------SANJAY Task No-TEC/23/07/19-000951
            Dim objPos As clsSNPOSHead = clsSNPOSHead.GetData(strDocNo, NavigatorType.Current)
            Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmSNPOS + "'", Nothing)
            If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.DeliveryNo, objPos.Document_Code)
                objEmailH.Email_Subject = objEmailH.Email_Subject.Replace(frmEMailAndSMSSetting.DeliveryDate, clsCommon.GetPrintDate(objPos.Document_Date, "dd/MMM/yyyy"))

                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.DeliveryNo, objPos.Document_Code)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.DeliveryDate, clsCommon.GetPrintDate(objPos.Document_Date, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerNo, objPos.Customer_Code)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.CustomerName, objPos.Customer_Name)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.LocationCode, objPos.Bill_To_Location)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.LocationName, objPos.BillToLocationName)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.TotalAmount, objPos.Total_Amt)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Company_Name, objCommonVar.CurrentCompanyName)


                For Each strUser As String In lstUsers
                    Dim qry As String = ""
                    Dim emailId As String = ""
                    If isSendForApproval Then
                        qry = "select E_Mail from TSPL_USER_MASTER where User_Code in ('" + strUser + "') "
                        emailId = clsDBFuncationality.getSingleValue(qry)
                    Else
                        emailId = clsDBFuncationality.getSingleValue("select Email from tspl_customer_master where CUST_CODE ='" & strUser & "' ")
                    End If

                    If clsCommon.myLen(emailId) > 0 Then
                        objEmailH.arrEMail.Add(clsCommon.myCstr(emailId))
                    End If

                Next

                objEmailH.SaveData(clsUserMgtCode.frmSNPOS, objEmailH, Nothing)
                objEmailH = Nothing
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Shared Function SendSMSAndEMail(ByVal obj As clsSNPOSHead, ByVal strDocNo As String) As Boolean
        Try
            If Not clsCommon.myInternetWork Then
                Throw New Exception("Internet is Not Working properly")
            End If
            Dim lstUsers As New List(Of String)
            lstUsers.Add(obj.Customer_Code)
            SendEmail(lstUsers, False, strDocNo)
        Catch ex As Exception
            Throw New Exception(ex.Message())
        End Try
        Return True
    End Function
    '------------------------------------Richa Code Ends Here-------------------------

    'Private Shared Function SendSMSAndEMail(ByVal obj As clsSNPOSHead) As Boolean

    '    If Not clsCommon.myInternetWork Then
    '        Throw New Exception("Internet is Not Working properly")
    '    End If

    '    Dim ObjConfig As clsEmailAndSMSConfig = clsEmailAndSMSConfig.GetData()
    '    If ObjConfig IsNot Nothing Then
    '        ''Sending Mail
    '        Dim ArrRecipients As List(Of clsEmailSMSRecipients) = clsEmailSMSRecipients.GetData(FrmEmailAndSMSRecipients.strTransTrype)
    '        Dim objCustomer As clsCustomerMasterNew = clsCustomerMasterNew.GetData(obj.Customer_Code, common.NavigatorType.Current, Nothing)
    '        Dim ArrPhonNo As New List(Of String)

    '        Dim MailMsg As New MailMessage()
    '        MailMsg.Subject = ObjConfig.EMail_Subject
    '        MailMsg.From = New MailAddress(ObjConfig.EMail_ID)


    '        If clsCommon.myLen(objCustomer.Contact_Person_Email) > 0 Then
    '            MailMsg.To.Add(objCustomer.Contact_Person_Email)
    '        End If
    '        If clsCommon.myLen(objCustomer.Phone1) > 0 Then
    '            ArrPhonNo.Add(objCustomer.Phone1)
    '        End If
    '        If clsCommon.myLen(objCustomer.Phone2) > 0 Then
    '            ArrPhonNo.Add(objCustomer.Phone2)
    '        End If

    '        If ArrRecipients IsNot Nothing Then
    '            For Each objRece As clsEmailSMSRecipients In ArrRecipients
    '                If objRece.Is_Send_Email AndAlso clsCommon.myLen(objRece.EMail_ID) > 0 Then
    '                    If clsCommon.CompairString(objRece.To_Or_CC, "CC") = CompairStringResult.Equal Then
    '                        MailMsg.CC.Add(objRece.EMail_ID)
    '                    Else
    '                        MailMsg.To.Add(objRece.EMail_ID)
    '                    End If
    '                End If
    '                If objRece.Is_Send_SMS AndAlso clsCommon.myLen(objRece.Phone) > 0 Then
    '                    ArrPhonNo.Add(objRece.Phone)
    '                End If
    '            Next
    '        End If



    '        ObjConfig.EMail_Text = ObjConfig.EMail_Text.Replace(frmEMailAndSMSSetting.CustomerName, obj.Customer_Name)
    '        ObjConfig.EMail_Text = ObjConfig.EMail_Text.Replace(frmEMailAndSMSSetting.InvoiceNo, obj.Document_Code)
    '        ObjConfig.EMail_Text = ObjConfig.EMail_Text.Replace(frmEMailAndSMSSetting.InvoiceDate, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"))
    '        ObjConfig.EMail_Text = ObjConfig.EMail_Text.Replace(frmEMailAndSMSSetting.DocumentAmount, clsCommon.myFormat(obj.Total_Amt))
    '        ObjConfig.EMail_Text = ObjConfig.EMail_Text.Replace(frmEMailAndSMSSetting.Remarks, obj.Remarks)
    '        ObjConfig.EMail_Text = ObjConfig.EMail_Text.Replace(frmEMailAndSMSSetting.Branch, obj.BillToLocationName)
    '        ObjConfig.EMail_Text = ObjConfig.EMail_Text.Replace(frmEMailAndSMSSetting.CompanyName, objCommonVar.CurrentCompanyName)
    '        MailMsg.Body = ObjConfig.EMail_Text

    '        MailMsg.Priority = MailPriority.High
    '        MailMsg.IsBodyHtml = False

    '        'Dim MsgAttach As New Attachment(Application.StartupPath() + "\Tecxpert\transportmaster.pdf")
    '        'MailMsg.Attachments.Add(MsgAttach)
    '        Dim SmtpMail As New SmtpClient(ObjConfig.EMail_SMTP_Client)
    '        SmtpMail.Port = clsCommon.myCdbl(ObjConfig.EMail_Port)
    '        SmtpMail.Credentials = New System.Net.NetworkCredential(ObjConfig.EMail_ID, ObjConfig.EMail_Pwd)
    '        SmtpMail.EnableSsl = ObjConfig.EMail_Enabel_SSL

    '        If clsCommon.myLen(ObjConfig.EMail_Text) > 0 AndAlso (MailMsg.To.Count > 0 OrElse MailMsg.CC.Count > 0) Then
    '            SmtpMail.Send(MailMsg)
    '        End If


    '        ''Sending SMS
    '        If ArrPhonNo IsNot Nothing AndAlso ArrPhonNo.Count > 0 AndAlso clsCommon.myLen(ObjConfig.SMS_Text) > 0 Then
    '            ObjConfig.SMS_Text = ObjConfig.SMS_Text.Replace(frmEMailAndSMSSetting.CustomerName, obj.Customer_Name)
    '            ObjConfig.SMS_Text = ObjConfig.SMS_Text.Replace(frmEMailAndSMSSetting.InvoiceNo, obj.Document_Code)
    '            ObjConfig.SMS_Text = ObjConfig.SMS_Text.Replace(frmEMailAndSMSSetting.InvoiceDate, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"))
    '            ObjConfig.SMS_Text = ObjConfig.SMS_Text.Replace(frmEMailAndSMSSetting.DocumentAmount, clsCommon.myFormat(obj.Total_Amt))
    '            ObjConfig.SMS_Text = ObjConfig.SMS_Text.Replace(frmEMailAndSMSSetting.Remarks, obj.Remarks)
    '            ObjConfig.SMS_Text = ObjConfig.SMS_Text.Replace(frmEMailAndSMSSetting.Branch, obj.BillToLocationName)
    '            ObjConfig.SMS_Text = ObjConfig.SMS_Text.Replace(frmEMailAndSMSSetting.CompanyName, objCommonVar.CurrentCompanyName)

    '            For Each strPhoneNo As String In ArrPhonNo
    '                Dim client As New System.Net.WebClient()
    '                Dim baseurl As String = ObjConfig.SMS_String + "?username=" + ObjConfig.SMS_User_Name + "&password=" + ObjConfig.SMS_Pwd + "&sendername=" + ObjConfig.SMS_Sender_Name + "&mobileno=91" + strPhoneNo + "&message=" + ObjConfig.SMS_Text
    '                Dim data As Stream = client.OpenRead(baseurl)
    '                Dim reader As StreamReader = New StreamReader(data)
    '                Dim s As String = reader.ReadToEnd()
    '                data.Close()
    '                reader.Close()
    '            Next
    '        End If
    '    End If


    'End Function

    Private Shared Function ConvertPOSToShipment(ByVal objPOS As clsSNPOSHead, ByVal trans As SqlTransaction) As clsSNShipmentHead
        Dim obj As New clsSNShipmentHead()
        obj = New clsSNShipmentHead()
        obj.Document_Code = objPOS.Document_Code
        obj.Document_Date = objPOS.Document_Date
        obj.Customer_Code = objPOS.Customer_Code
        obj.Customer_Name = objPOS.Customer_Name
        obj.Status = ERPTransactionStatus.Pending
        obj.On_Hold = IIf(objPOS.On_Hold = 1, True, False)
        'obj.Is_Internal = IIf(objPOS.Is_Internal = 1, True, False)
        'obj.Ref_No = objPOS.Ref_No
        obj.Description = objPOS.Message
        obj.Remarks = objPOS.Remarks
        obj.Bill_To_Location = objPOS.Bill_To_Location
        obj.Ship_To_Location = objPOS.Ship_To_Code
        obj.Tax_Group = objPOS.Tax_Group
        obj.TAX1 = objPOS.TAX1
        obj.TAX1_Rate = objPOS.TAX1_Rate
        obj.TAX1_Base_Amt = objPOS.TAX1_Base_Amt
        obj.TAX1_Amt = objPOS.TAX1_Amt
        obj.TAX2 = objPOS.TAX2
        obj.TAX2_Rate = objPOS.TAX2_Rate
        obj.TAX2_Base_Amt = objPOS.TAX2_Base_Amt
        obj.TAX2_Amt = objPOS.TAX2_Amt
        obj.TAX3 = objPOS.TAX3
        obj.TAX3_Base_Amt = objPOS.TAX3_Base_Amt
        obj.TAX3_Rate = objPOS.TAX3_Rate
        obj.TAX3_Amt = objPOS.TAX3_Amt
        obj.TAX4 = objPOS.TAX4
        obj.TAX4_Rate = objPOS.TAX4_Rate
        obj.TAX4_Base_Amt = objPOS.TAX4_Base_Amt
        obj.TAX4_Amt = objPOS.TAX4_Amt
        obj.TAX5 = objPOS.TAX5
        obj.TAX5_Rate = objPOS.TAX5_Rate
        obj.TAX5_Base_Amt = objPOS.TAX5_Base_Amt
        obj.TAX5_Amt = objPOS.TAX5_Amt
        obj.TAX6 = objPOS.TAX6
        obj.TAX6_Rate = objPOS.TAX6_Rate
        obj.TAX6_Base_Amt = objPOS.TAX6_Base_Amt
        obj.TAX6_Amt = objPOS.TAX6_Amt
        obj.TAX7 = objPOS.TAX7
        obj.TAX7_Rate = objPOS.TAX7_Rate
        obj.TAX7_Base_Amt = objPOS.TAX7_Base_Amt
        obj.TAX7_Amt = objPOS.TAX7_Amt
        obj.TAX8 = objPOS.TAX8
        obj.TAX8_Rate = objPOS.TAX8_Rate
        obj.TAX8_Base_Amt = objPOS.TAX8_Base_Amt
        obj.TAX8_Amt = objPOS.TAX8_Amt
        obj.TAX9 = objPOS.TAX9
        obj.TAX9_Rate = objPOS.TAX9_Rate
        obj.TAX9_Base_Amt = objPOS.TAX9_Base_Amt
        obj.TAX9_Amt = objPOS.TAX9_Amt
        obj.TAX10 = objPOS.TAX10
        obj.TAX10_Rate = objPOS.TAX10_Rate
        obj.TAX10_Base_Amt = objPOS.TAX10_Base_Amt
        obj.TAX10_Amt = objPOS.TAX10_Amt
        obj.Total_Tax_Amt = objPOS.Total_Tax_Amt
        obj.Discount_Base = objPOS.Discount_Base
        obj.Discount_Amt = objPOS.Discount_Amt
        obj.Amount_Less_Discount = objPOS.Amount_Less_Discount
        obj.Total_Amt = objPOS.Total_Amt
        'obj.Comments = objPOS.Comments
        'obj.Comp_Code = objPOS.Comp_Code
        'obj.Terms_Code = objPOS.Terms_Code
        'obj.Due_Date = objPOS.Due_Date
        obj.BillToLocationName = objPOS.BillToLocationName
        'obj.ShipToLocationName = objPOS.ShipToLocationName
        obj.TaxGroupName = objPOS.TaxGroupName
        'obj.TermsName = objPOS.TermsName

        If objPOS.Posting_Date IsNot Nothing Then
            obj.Posting_Date = objPOS.Posting_Date
        End If


        'obj.Challan_No = objPOS.Challan_No
        'obj.Carrier = objPOS.Carrier
        'obj.VehicleNo = objPOS.VehicleNo
        'obj.GRNo = objPOS.GRNo
        'obj.GENo = objPOS.GENo
        'If objPOS.GEDate IsNot Nothing Then
        '    obj.GEDate = objPOS.GEDate
        'End If




        'obj.Dept = objPOS.Dept
        'obj.Dept_Desc = objPOS.Dept_Desc
        Dim qry As String = "select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + objPOS.Arr(0).Item_Code + "'"
        obj.Item_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        obj.Against_POS = objPOS.Document_Code


        obj.Add_Charge_Code1 = "Freight"
        obj.Add_Charge_Name1 = "Freight"
        obj.Add_Charge_Amt1 = objPOS.Total_Freight

        obj.Add_Charge_Code2 = "Other Charges"
        obj.Add_Charge_Name2 = "Other Charges"
        obj.Add_Charge_Amt2 = objPOS.Total_Other_Charges

        'obj.Add_Charge_Code3 = objPOS.Add_Charge_Code3
        'obj.Add_Charge_Name3 = objPOS.Add_Charge_Name3
        'obj.Add_Charge_Amt3 = objPOS.Add_Charge_Amt3

        'obj.Add_Charge_Code4 = objPOS.Add_Charge_Code4
        'obj.Add_Charge_Name4 = objPOS.Add_Charge_Name4
        'obj.Add_Charge_Amt4 = objPOS.Add_Charge_Amt4

        'obj.Add_Charge_Code5 = objPOS.Add_Charge_Code5
        'obj.Add_Charge_Name5 = objPOS.Add_Charge_Name5
        'obj.Add_Charge_Amt5 = objPOS.Add_Charge_Amt5

        'obj.Add_Charge_Code6 = objPOS.Add_Charge_Code6
        'obj.Add_Charge_Name6 = objPOS.Add_Charge_Name6
        'obj.Add_Charge_Amt6 = objPOS.Add_Charge_Amt6

        'obj.Add_Charge_Code7 = objPOS.Add_Charge_Code7
        'obj.Add_Charge_Name7 = objPOS.Add_Charge_Name7
        'obj.Add_Charge_Amt7 = objPOS.Add_Charge_Amt7

        'obj.Add_Charge_Code8 = objPOS.Add_Charge_Code8
        'obj.Add_Charge_Name8 = objPOS.Add_Charge_Name8
        'obj.Add_Charge_Amt8 = objPOS.Add_Charge_Amt8

        'obj.Add_Charge_Code9 = objPOS.Add_Charge_Code9
        'obj.Add_Charge_Name9 = objPOS.Add_Charge_Name9
        'obj.Add_Charge_Amt9 = objPOS.Add_Charge_Amt9

        'obj.Add_Charge_Code10 = objPOS.Add_Charge_Code10
        'obj.Add_Charge_Name10 = objPOS.Add_Charge_Name10
        'obj.Add_Charge_Amt10 = objPOS.Add_Charge_Amt10

        obj.Total_Add_Charge = objPOS.Total_Freight + objPOS.Total_Other_Charges
        'obj.Inv_No = objPOS.Inv_No
        'If clsCommon.myLen(objPOS.Challan_Date) <= 0 Then
        '    obj.Challan_Date = ""
        'Else
        '    obj.Challan_Date = clsCommon.GetPrintDate(objPOS.Challan_Date, "dd/MMM/yyyy")
        'End If

        'If clsCommon.myLen(objPOS.Inv_Date) <= 0 Then
        '    obj.Inv_Date = ""
        'Else
        '    obj.Inv_Date = clsCommon.GetPrintDate(objPOS.Inv_Date, "dd/MMM/yyyy")
        'End If

        obj.Tax_Calculation_Type = IIf(objPOS.Tax_Calculation_Type = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
        obj.Is_Create_Auto_Receipt = True

        If (objPOS.Arr IsNot Nothing AndAlso objPOS.Arr.Count > 0) Then
            obj.Arr = New List(Of clsSNShipmentDetail)
            Dim objTr As clsSNShipmentDetail
            For Each objShipmentDetail As clsSNPOSDetail In objPOS.Arr
                objTr = New clsSNShipmentDetail
                objTr.Document_Code = objShipmentDetail.Document_Code
                objTr.Row_Type = "Item"
                objTr.Line_No = objShipmentDetail.Line_No
                'objTr.Status = Convert.ToInt32(objShipmentDetail.Status)
                objTr.Item_Code = objShipmentDetail.Item_Code
                objTr.Item_Desc = objShipmentDetail.Item_Desc
                objTr.Qty = objShipmentDetail.Qty
                'objTr.Free_Qty = objShipmentDetail.Free_Qty
                'objTr.Shipment_Code = objPOS.Document_Code
                'objTr.Balance_Qty = objShipmentDetail.Balance_Qty
                objTr.Unit_code = objShipmentDetail.Unit_code
                objTr.Location = objPOS.Bill_To_Location
                objTr.ItemwiseTaxCode = objShipmentDetail.ItemwiseTaxCode
                objTr.LocationName = objPOS.BillToLocationName
                objTr.Item_Cost = objShipmentDetail.Item_Cost
                objTr.TAX1 = objShipmentDetail.TAX1
                objTr.TAX1_Base_Amt = objShipmentDetail.TAX1_Base_Amt
                objTr.TAX1_Rate = objShipmentDetail.TAX1_Rate
                objTr.TAX1_Amt = objShipmentDetail.TAX1_Amt
                objTr.TAX2 = objShipmentDetail.TAX2
                objTr.TAX2_Base_Amt = objShipmentDetail.TAX2_Base_Amt
                objTr.TAX2_Rate = objShipmentDetail.TAX2_Rate
                objTr.TAX2_Amt = objShipmentDetail.TAX2_Amt
                objTr.TAX3 = objShipmentDetail.TAX3
                objTr.TAX3_Base_Amt = objShipmentDetail.TAX3_Base_Amt
                objTr.TAX3_Rate = objShipmentDetail.TAX3_Rate
                objTr.TAX3_Amt = objShipmentDetail.TAX3_Amt
                objTr.TAX4 = objShipmentDetail.TAX4
                objTr.TAX4_Base_Amt = objShipmentDetail.TAX4_Base_Amt
                objTr.TAX4_Rate = objShipmentDetail.TAX4_Rate
                objTr.TAX4_Amt = objShipmentDetail.TAX4_Amt
                objTr.TAX5 = objShipmentDetail.TAX5
                objTr.TAX5_Base_Amt = objShipmentDetail.TAX5_Base_Amt
                objTr.TAX5_Rate = objShipmentDetail.TAX5_Rate
                objTr.TAX5_Amt = objShipmentDetail.TAX5_Amt
                objTr.TAX6 = objShipmentDetail.TAX6
                objTr.TAX6_Base_Amt = objShipmentDetail.TAX6_Base_Amt
                objTr.TAX6_Rate = objShipmentDetail.TAX6_Rate
                objTr.TAX6_Amt = objShipmentDetail.TAX6_Amt
                objTr.TAX7 = objShipmentDetail.TAX7
                objTr.TAX7_Base_Amt = objShipmentDetail.TAX7_Base_Amt
                objTr.TAX7_Rate = objShipmentDetail.TAX7_Rate
                objTr.TAX7_Amt = objShipmentDetail.TAX7_Amt
                objTr.TAX8 = objShipmentDetail.TAX8
                objTr.TAX8_Base_Amt = objShipmentDetail.TAX8_Base_Amt
                objTr.TAX8_Rate = objShipmentDetail.TAX8_Rate
                objTr.TAX8_Amt = objShipmentDetail.TAX8_Amt
                objTr.TAX9 = objShipmentDetail.TAX9
                objTr.TAX9_Base_Amt = objShipmentDetail.TAX9_Base_Amt
                objTr.TAX9_Rate = objShipmentDetail.TAX9_Rate
                objTr.TAX9_Amt = objShipmentDetail.TAX9_Amt
                objTr.TAX10 = objShipmentDetail.TAX10
                objTr.TAX10_Base_Amt = objShipmentDetail.TAX10_Base_Amt
                objTr.TAX10_Rate = objShipmentDetail.TAX10_Rate
                objTr.TAX10_Amt = objShipmentDetail.TAX10_Amt
                objTr.Amount = objShipmentDetail.Amount
                objTr.Disc_Per = objShipmentDetail.Disc_Per
                objTr.Disc_Amt = objShipmentDetail.Disc_Amt
                objTr.Amt_Less_Discount = objShipmentDetail.Amt_Less_Discount
                objTr.Total_Tax_Amt = objShipmentDetail.Total_Tax_Amt
                objTr.Item_Net_Amt = objShipmentDetail.Item_Net_Amt


                'objTr.Is_Mannual_Amt = objShipmentDetail.Is_Mannual_Amt

                objTr.MRP = objShipmentDetail.MRP
                'objTr.Assessable = objShipmentDetail.Assessable
                'objTr.AssessableAmt = objShipmentDetail.AssessableAmt
                'objTr.Batch_No = objShipmentDetail.Batch_No
                'If objShipmentDetail.MFG_Date IsNot Nothing Then
                '    objTr.MFG_Date = objShipmentDetail.MFG_Date
                'End If
                'If objShipmentDetail.Expiry_Date IsNot Nothing Then
                '    objTr.Expiry_Date = objShipmentDetail.Expiry_Date
                'End If
                'objTr.Specification = objShipmentDetail.Specification
                'objTr.Remarks = objShipmentDetail.Remarks
                obj.Arr.Add(objTr)
            Next
        End If
        Return obj
    End Function

    'Private Shared Function ConvertShipmentToSaleInvoice(ByVal objShipment As clsSNPOSHead) As clsSNInvoiceHead
    '    Dim obj As New clsSNInvoiceHead()
    '    obj = New clsSNInvoiceHead()
    '    obj.Document_Code = objShipment.Document_Code
    '    obj.Document_Date = objShipment.Document_Date
    '    obj.Customer_Code = objShipment.Customer_Code
    '    obj.Customer_Name = objShipment.Customer_Name
    '    obj.Status = IIf(objShipment.Status = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
    '    obj.On_Hold = IIf(objShipment.On_Hold = 1, True, False)
    '    obj.Is_Internal = IIf(objShipment.Is_Internal = 1, True, False)
    '    obj.Payment_Mode = objShipment.Payment_Mode
    '    obj.Message = objShipment.Message
    '    obj.Remarks = objShipment.Remarks
    '    obj.Bill_To_Location = objShipment.Bill_To_Location
    '    obj.Cheque_No = objShipment.Cheque_No
    '    obj.Tax_Group = objShipment.Tax_Group
    '    obj.TAX1 = objShipment.TAX1
    '    obj.TAX1_Rate = objShipment.TAX1_Rate
    '    obj.TAX1_Base_Amt = objShipment.TAX1_Base_Amt
    '    obj.TAX1_Amt = objShipment.TAX1_Amt
    '    obj.TAX2 = objShipment.TAX2
    '    obj.TAX2_Rate = objShipment.TAX2_Rate
    '    obj.TAX2_Base_Amt = objShipment.TAX2_Base_Amt
    '    obj.TAX2_Amt = objShipment.TAX2_Amt
    '    obj.TAX3 = objShipment.TAX3
    '    obj.TAX3_Base_Amt = objShipment.TAX3_Base_Amt
    '    obj.TAX3_Rate = objShipment.TAX3_Rate
    '    obj.TAX3_Amt = objShipment.TAX3_Amt
    '    obj.TAX4 = objShipment.TAX4
    '    obj.TAX4_Rate = objShipment.TAX4_Rate
    '    obj.TAX4_Base_Amt = objShipment.TAX4_Base_Amt
    '    obj.TAX4_Amt = objShipment.TAX4_Amt
    '    obj.TAX5 = objShipment.TAX5
    '    obj.TAX5_Rate = objShipment.TAX5_Rate
    '    obj.TAX5_Base_Amt = objShipment.TAX5_Base_Amt
    '    obj.TAX5_Amt = objShipment.TAX5_Amt
    '    obj.TAX6 = objShipment.TAX6
    '    obj.TAX6_Rate = objShipment.TAX6_Rate
    '    obj.TAX6_Base_Amt = objShipment.TAX6_Base_Amt
    '    obj.TAX6_Amt = objShipment.TAX6_Amt
    '    obj.TAX7 = objShipment.TAX7
    '    obj.TAX7_Rate = objShipment.TAX7_Rate
    '    obj.TAX7_Base_Amt = objShipment.TAX7_Base_Amt
    '    obj.TAX7_Amt = objShipment.TAX7_Amt
    '    obj.TAX8 = objShipment.TAX8
    '    obj.TAX8_Rate = objShipment.TAX8_Rate
    '    obj.TAX8_Base_Amt = objShipment.TAX8_Base_Amt
    '    obj.TAX8_Amt = objShipment.TAX8_Amt
    '    obj.TAX9 = objShipment.TAX9
    '    obj.TAX9_Rate = objShipment.TAX9_Rate
    '    obj.TAX9_Base_Amt = objShipment.TAX9_Base_Amt
    '    obj.TAX9_Amt = objShipment.TAX9_Amt
    '    obj.TAX10 = objShipment.TAX10
    '    obj.TAX10_Rate = objShipment.TAX10_Rate
    '    obj.TAX10_Base_Amt = objShipment.TAX10_Base_Amt
    '    obj.TAX10_Amt = objShipment.TAX10_Amt
    '    obj.Total_Tax_Amt = objShipment.Total_Tax_Amt
    '    obj.Discount_Base = objShipment.Discount_Base
    '    obj.Discount_Amt = objShipment.Discount_Amt
    '    obj.Amount_Less_Discount = objShipment.Amount_Less_Discount
    '    obj.Total_Amt = objShipment.Total_Amt
    '    obj.Comments = objShipment.Comments
    '    obj.Comp_Code = objShipment.Comp_Code
    '    obj.Terms_Code = objShipment.Terms_Code
    '    obj.Due_Date = objShipment.Due_Date
    '    obj.BillToLocationName = objShipment.BillToLocationName
    '    obj.ShipToLocationName = objShipment.ShipToLocationName
    '    obj.TaxGroupName = objShipment.TaxGroupName
    '    obj.TermsName = objShipment.TermsName

    '    If objShipment.Posting_Date IsNot Nothing Then
    '        obj.Posting_Date = objShipment.Posting_Date
    '    End If


    '    obj.Delivery_Type = objShipment.Delivery_Type
    '    obj.Carrier = objShipment.Carrier
    '    obj.VehicleNo = objShipment.VehicleNo
    '    obj.GRNo = objShipment.GRNo
    '    obj.GENo = objShipment.GENo
    '    If objShipment.GEDate IsNot Nothing Then
    '        obj.GEDate = objShipment.GEDate
    '    End If




    '    obj.Dept = objShipment.Dept
    '    obj.Dept_Desc = objShipment.Dept_Desc
    '    obj.Item_Type = objShipment.Item_Type

    '    obj.Against_Shipment_No = objShipment.Document_Code


    '    obj.Cheque_Date = objShipment.Cheque_Date
    '    obj.Add_Charge_Name1 = objShipment.Add_Charge_Name1
    '    obj.Balance_Payment = objShipment.Balance_Payment

    '    obj.Add_Charge_Code2 = objShipment.Add_Charge_Code2
    '    obj.Add_Charge_Name2 = objShipment.Add_Charge_Name2
    '    obj.Amount_Paid = objShipment.Amount_Paid

    '    obj.Add_Charge_Code3 = objShipment.Add_Charge_Code3
    '    obj.Add_Charge_Name3 = objShipment.Add_Charge_Name3
    '    obj.Balance = objShipment.Balance

    '    obj.Add_Charge_Code4 = objShipment.Add_Charge_Code4
    '    obj.Add_Charge_Name4 = objShipment.Add_Charge_Name4
    '    obj.Advance_Paid = objShipment.Advance_Paid

    '    obj.Add_Charge_Code5 = objShipment.Add_Charge_Code5
    '    obj.Add_Charge_Name5 = objShipment.Add_Charge_Name5
    '    obj.Total_Other_Charges = objShipment.Total_Other_Charges

    '    obj.Add_Charge_Code6 = objShipment.Add_Charge_Code6
    '    obj.Add_Charge_Name6 = objShipment.Add_Charge_Name6
    '    obj.Total_Freight = objShipment.Total_Freight

    '    obj.Add_Charge_Code7 = objShipment.Add_Charge_Code7
    '    obj.Add_Charge_Name7 = objShipment.Add_Charge_Name7
    '    obj.Total_Amt_After_Tax = objShipment.Total_Amt_After_Tax

    '    obj.Credit_Card_No = objShipment.Credit_Card_No
    '    obj.Credit_Card_Bank = objShipment.Credit_Card_Bank
    '    obj.Credit_Card_Batch_No = objShipment.Credit_Card_Batch_No

    '    obj.Add_Charge_Code9 = objShipment.Add_Charge_Code9
    '    obj.Credit_Card_Approval_code = objShipment.Credit_Card_Approval_code
    '    obj.Add_Charge_Amt9 = objShipment.Add_Charge_Amt9

    '    obj.Debit_Card_No = objShipment.Debit_Card_No
    '    obj.Debit_Card_Type = objShipment.Debit_Card_Type
    '    obj.Add_Charge_Amt10 = objShipment.Add_Charge_Amt10

    '    obj.Total_Add_Charge = objShipment.Total_Add_Charge
    '    obj.Inv_No = objShipment.Inv_No
    '    If clsCommon.myLen(objShipment.Delivery_Date) <= 0 Then
    '        obj.Delivery_Date = ""
    '    Else
    '        obj.Delivery_Date = clsCommon.GetPrintDate(objShipment.Delivery_Date, "dd/MMM/yyyy")
    '    End If

    '    If clsCommon.myLen(objShipment.Inv_Date) <= 0 Then
    '        obj.Inv_Date = ""
    '    Else
    '        obj.Inv_Date = clsCommon.GetPrintDate(objShipment.Inv_Date, "dd/MMM/yyyy")
    '    End If

    '    obj.Tax_Calculation_Type = IIf(objShipment.Tax_Calculation_Type = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)
    '    obj.Is_Create_Auto_Receipt = objShipment.Is_Create_Auto_Receipt


    '    If (objShipment.Arr IsNot Nothing AndAlso objShipment.Arr.Count > 0) Then
    '        obj.Arr = New List(Of clsSNInvoiceDetail)
    '        Dim objTr As clsSNInvoiceDetail
    '        For Each objShipmentDetail As clsSNPOSDetail In objShipment.Arr
    '            objTr = New clsSNInvoiceDetail
    '            objTr.Document_Code = objShipmentDetail.Document_Code
    '            objTr.Row_Type = objShipmentDetail.Row_Type
    '            objTr.Line_No = objShipmentDetail.Line_No
    '            objTr.Status = Convert.ToInt32(objShipmentDetail.Status)
    '            objTr.Item_Code = objShipmentDetail.Item_Code
    '            objTr.Item_Desc = objShipmentDetail.Item_Desc
    '            objTr.Qty = objShipmentDetail.Qty
    '            objTr.Other_Charges = objShipmentDetail.Other_Charges
    '            objTr.Shipment_Code = objShipment.Document_Code
    '            objTr.Freight = objShipmentDetail.Freight
    '            objTr.Unit_code = objShipmentDetail.Unit_code
    '            objTr.Location = objShipmentDetail.Location
    '            objTr.LocationName = objShipmentDetail.LocationName
    '            objTr.Item_Cost = objShipmentDetail.Item_Cost
    '            objTr.TAX1 = objShipmentDetail.TAX1
    '            objTr.TAX1_Base_Amt = objShipmentDetail.TAX1_Base_Amt
    '            objTr.TAX1_Rate = objShipmentDetail.TAX1_Rate
    '            objTr.TAX1_Amt = objShipmentDetail.TAX1_Amt
    '            objTr.TAX2 = objShipmentDetail.TAX2
    '            objTr.TAX2_Base_Amt = objShipmentDetail.TAX2_Base_Amt
    '            objTr.TAX2_Rate = objShipmentDetail.TAX2_Rate
    '            objTr.TAX2_Amt = objShipmentDetail.TAX2_Amt
    '            objTr.TAX3 = objShipmentDetail.TAX3
    '            objTr.TAX3_Base_Amt = objShipmentDetail.TAX3_Base_Amt
    '            objTr.TAX3_Rate = objShipmentDetail.TAX3_Rate
    '            objTr.TAX3_Amt = objShipmentDetail.TAX3_Amt
    '            objTr.TAX4 = objShipmentDetail.TAX4
    '            objTr.TAX4_Base_Amt = objShipmentDetail.TAX4_Base_Amt
    '            objTr.TAX4_Rate = objShipmentDetail.TAX4_Rate
    '            objTr.TAX4_Amt = objShipmentDetail.TAX4_Amt
    '            objTr.TAX5 = objShipmentDetail.TAX5
    '            objTr.TAX5_Base_Amt = objShipmentDetail.TAX5_Base_Amt
    '            objTr.TAX5_Rate = objShipmentDetail.TAX5_Rate
    '            objTr.TAX5_Amt = objShipmentDetail.TAX5_Amt
    '            objTr.TAX6 = objShipmentDetail.TAX6
    '            objTr.TAX6_Base_Amt = objShipmentDetail.TAX6_Base_Amt
    '            objTr.TAX6_Rate = objShipmentDetail.TAX6_Rate
    '            objTr.TAX6_Amt = objShipmentDetail.TAX6_Amt
    '            objTr.TAX7 = objShipmentDetail.TAX7
    '            objTr.TAX7_Base_Amt = objShipmentDetail.TAX7_Base_Amt
    '            objTr.TAX7_Rate = objShipmentDetail.TAX7_Rate
    '            objTr.TAX7_Amt = objShipmentDetail.TAX7_Amt
    '            objTr.TAX8 = objShipmentDetail.TAX8
    '            objTr.TAX8_Base_Amt = objShipmentDetail.TAX8_Base_Amt
    '            objTr.TAX8_Rate = objShipmentDetail.TAX8_Rate
    '            objTr.TAX8_Amt = objShipmentDetail.TAX8_Amt
    '            objTr.TAX9 = objShipmentDetail.TAX9
    '            objTr.TAX9_Base_Amt = objShipmentDetail.TAX9_Base_Amt
    '            objTr.TAX9_Rate = objShipmentDetail.TAX9_Rate
    '            objTr.TAX9_Amt = objShipmentDetail.TAX9_Amt
    '            objTr.TAX10 = objShipmentDetail.TAX10
    '            objTr.TAX10_Base_Amt = objShipmentDetail.TAX10_Base_Amt
    '            objTr.TAX10_Rate = objShipmentDetail.TAX10_Rate
    '            objTr.TAX10_Amt = objShipmentDetail.TAX10_Amt
    '            objTr.Amount = objShipmentDetail.Amount
    '            objTr.Disc_Per = objShipmentDetail.Disc_Per
    '            objTr.Disc_Amt = objShipmentDetail.Disc_Amt
    '            objTr.Amt_Less_Discount = objShipmentDetail.Amt_Less_Discount
    '            objTr.Total_Tax_Amt = objShipmentDetail.Total_Tax_Amt
    '            objTr.Item_Net_Amt = objShipmentDetail.Item_Net_Amt


    '            objTr.Is_Mannual_Amt = objShipmentDetail.Is_Mannual_Amt

    '            objTr.Amt_After_Tax = objShipmentDetail.Amt_After_Tax
    '            objTr.Assessable = objShipmentDetail.Assessable
    '            objTr.AssessableAmt = objShipmentDetail.AssessableAmt
    '            objTr.Batch_No = objShipmentDetail.Batch_No
    '            If objShipmentDetail.MFG_Date IsNot Nothing Then
    '                objTr.MFG_Date = objShipmentDetail.MFG_Date
    '            End If
    '            If objShipmentDetail.Expiry_Date IsNot Nothing Then
    '                objTr.Expiry_Date = objShipmentDetail.Expiry_Date
    '            End If
    '            objTr.Specification = objShipmentDetail.Specification
    '            objTr.Remarks = objShipmentDetail.Remarks
    '            obj.Arr.Add(objTr)
    '        Next
    '    End If
    '    Return obj
    'End Function


    'Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsSNPOSDetail)) As String


    '    For Each objtr As clsSNPOSDetail In Arr
    '        If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
    '            Return objtr.Item_Code
    '        End If
    '    Next
    '    Return ""
    'End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsSNPOSHead = clsSNPOSHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_SD_POS_DETAIL where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SD_POS_HEAD where Document_Code='" + strCode + "'"
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

    Public Shared Function IsValidCustomer(ByVal Arr As List(Of String), ByVal strCustomerCode As String) As Boolean
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            Dim qry As String = "select TSPL_SD_POS_HEAD.Document_Code,TSPL_SD_POS_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name from TSPL_SD_POS_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_POS_HEAD.Customer_Code where Document_Code in (" + clsCommon.GetMulcallString(Arr) + ") and Customer_Code not in ('" + strCustomerCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "SRN No:" + clsCommon.myCstr(dr("Document_Code")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Customer_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Customer_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function
    ''To be Uncomment
    '    Public Sub SRNPrintOut(ByVal FromDate As Date?, ByVal ToDate As Date?, ByVal IsDocTypeFinsihGoods As Boolean, ByVal ArrSrnNo As ArrayList, ByVal ArrVendor As ArrayList, ByVal ArrLocation As ArrayList)
    '        Dim qry As String

    '        Try
    '            If IsDocTypeFinsihGoods Then
    '                qry = "select Document_Code,MAX(ItemType )as ItemType,MAX(MRN_Date) as Document_Date,MAX(Customer_Name) as Customer_Name,MAX(GRNo) as GRNo,MAX(GENo) as GENo,MAX(GEDate) as GEDate,Item_Code,MAX(Item_Desc) as Item_Desc,MAX(VehicleNo) as VehicleNo, SUM(ISNULL( FCS,0)) as FCS, SUM(isnull(FBS,0))as FBS, SUM(ISNULL( FSH,0)) as FSH, SUM(ISNULL( ECS,0)) as ECS, SUM(ISNULL( EBS,0)) as EBS, SUM(Leak_Qty) as HF,SUM(Burst_Qty) as Burst,SUM(Short_Qty) as Short,MAX(Remarks) as Remarks,max(Payment_Mode)as Payment_Mode from( " & _
    '         "select TSPL_SD_POS_HEAD.Document_Code,TSPL_SD_POS_HEAD .Item_Type as ItemType," & _
    '         "(replace( CONVERT(varchar(11), TSPL_SD_POS_HEAD.Document_Date,104),'.','/')+' '+CONVERT(varchar(100),TSPL_SD_POS_HEAD.Document_Date,108) )as MRN_Date,TSPL_SD_POS_HEAD.Customer_Name,TSPL_SD_POS_HEAD.GRNo,TSPL_SD_POS_HEAD.GENo," & _
    '         "(case when LEN(TSPL_SD_POS_HEAD.GEDate)>0  then REPLACE( CONVERT(varchar(11), TSPL_SD_POS_HEAD.GEDate,104),'.','/') else '' end) as GEDate,TSPL_SD_POS_HEAD.VehicleNo,TSPL_SD_POS_HEAD.Remarks ,TSPL_SD_POS_HEAD.Payment_Mode,TSPL_SD_POS_DETAIL.Item_Code,TSPL_SD_POS_DETAIL.Item_Desc,TSPL_SD_POS_DETAIL.Unit_code," & _
    '         "case when Unit_code='FC' then Qty + ISNULL( Other_Charges,0) end as FCS, " & _
    '         "case when Unit_code='FB' then Qty + ISNULL( Other_Charges,0) end as FBS, " & _
    '         "case when Unit_code='SH' then Qty + ISNULL( Other_Charges,0) end as FSH, " & _
    '         "case when Unit_code='EC' then Qty + ISNULL( Other_Charges,0) end as ECS," & _
    '         "case when Unit_code='EB' then Qty + ISNULL( Other_Charges,0) end as EBS, " & _
    '         "TSPL_SD_POS_DETAIL.Leak_Qty,TSPL_SD_POS_DETAIL.Burst_Qty,TSPL_SD_POS_DETAIL.Short_Qty from TSPL_SD_POS_DETAIL left outer join TSPL_SD_POS_HEAD on TSPL_SD_POS_HEAD.Document_Code= TSPL_SD_POS_DETAIL.Document_Code " & _
    '         " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SD_POS_HEAD.Bill_To_Location   where Item_Type ='F'"
    '                If FromDate.HasValue AndAlso ToDate.HasValue Then
    '                    qry += " and Convert(date,TSPL_SD_POS_HEAD.Document_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_SD_POS_HEAD.Document_Date,103)<=Convert(date,'" + ToDate + "',103) "
    '                End If

    '                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
    '                    qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
    '                End If
    '                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
    '                    qry += " and TSPL_SD_POS_HEAD.Document_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
    '                End If
    '                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
    '                    qry += " and TSPL_SD_POS_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")" 'ADDED BY ABHISHEK AS ON 30 AUG 2012
    '                End If
    '                qry += " )xxx group by Document_Code,Item_Code order by Item_Desc"
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    common.clsCommon.MyMessageBoxShow("No Record Found")
    '                Else
    '                    PurchaseOrderViewer.funreport(dt, EnumTecxpertPaperSize.PaperSize10x6, "rptSRNCustomReport", "SRN Report")

    '                End If
    '            Else ''For RM Other Print out
    '                Dim strquery As String = "SELECT TSPL_SD_POS_HEAD.Document_Code, TSPL_SD_POS_HEAD.Document_Date,TSPL_SD_POS_HEAD.Customer_Name,(case when len(against_mrn)>0 then (select MRN_Date  from tspl_mrn_head where tspl_mrn_head.MRN_No =against_mrn) else Document_Date end ) as Delivery_Date, TSPL_SD_POS_HEAD.Payment_Mode  " & _
    '                      "as Delivery_Type, TSPL_SD_POS_HEAD.Inv_No, TSPL_SD_POS_HEAD.Inv_Date, TSPL_SD_POS_HEAD.GRNo,TSPL_SD_POS_HEAD.Amount_Less_Discount ,TSPL_SD_POS_HEAD.GENo,TSPL_SD_POS_HEAD.Total_Amt, " & _
    '                      "TSPL_SD_POS_HEAD.GEDate, TSPL_SD_POS_HEAD.VehicleNo, TSPL_SD_POS_HEAD.Carrier,TSPL_SD_POS_HEAD.Remarks,TSPL_SD_POS_DETAIL.Landed_Cost_Rate,TSPL_SD_POS_DETAIL.Landed_Cost_Amount , TSPL_SD_POS_DETAIL.Item_Code,TSPL_SD_POS_DETAIL.Row_Type,TSPL_SD_POS_DETAIL.Amt_Less_Discount," & _
    '"TSPL_SD_POS_DETAIL.Item_Cost as basicRate,TSPL_SD_POS_DETAIL.Item_Net_Amt as BasicTotal,TSPL_SD_POS_DETAIL.Unit_Cost_Tax_Rate as UCTR," & _
    '"TSPL_SD_POS_DETAIL.Unit_Cost_Tax as uctax,TSPL_SD_POS_DETAIL.Item_Desc,TSPL_SD_POS_DETAIL.Unit_code,TSPL_SD_POS_DETAIL.Qty,TSPL_SD_POS_DETAIL.Rejected_Qty,TSPL_SD_POS_HEAD.Customer_Code,TSPL_SD_POS_HEAD.Total_Amt,TSPL_SD_POS_DETAIL.ITEM_COST," & _
    ' "TSPL_VENDOR_MASTER.Add1 as venAdd1, TSPL_VENDOR_MASTER.Add2 as vanadd2, TSPL_VENDOR_MASTER.Add3 as venadd3, " & _
    '"tax1.Tax_Code_Desc as tax1name,isnull (TSPL_SD_POS_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name," & _
    '"isnull (TSPL_SD_POS_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_SD_POS_HEAD.tax3_amt,0) as txt3amt," & _
    '"tax4.Tax_Code_Desc as tax4name,isnull (TSPL_SD_POS_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name," & _
    '"isnull (TSPL_SD_POS_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_SD_POS_HEAD.tax6_amt,0) as txt6amt " & _
    '",tax7.Tax_Code_Desc as tax7name,isnull (TSPL_SD_POS_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name," & _
    '"isnull (TSPL_SD_POS_HEAD.tax8_amt,0) as txt8amt, tax9.Tax_Code_Desc as tax9name,isnull (TSPL_SD_POS_HEAD.tax9_amt,0) as txt9amt," & _
    '"tax10.Tax_Code_Desc as tax10name,isnull (TSPL_SD_POS_HEAD.tax10_amt,0) as txt10amt, TSPL_COMPANY_MASTER.Comp_Name as compname, " & _
    '"TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_SD_POS_DETAIL.Qty," & _
    '"case when tax1.Tax_Recoverable='Y' then TSPL_SD_POS_HEAD.tax1_amt else null end as Tax1Recoverable," & _
    '"case when tax2.Tax_Recoverable='Y' then TSPL_SD_POS_HEAD.TAX2_Amt else null end as Tax2Recoverable, " & _
    '"case when tax3.Tax_Recoverable='Y' then TSPL_SD_POS_HEAD.tax3_amt else null end as Tax3Recoverable, " & _
    '"case when tax4.Tax_Recoverable='Y' then TSPL_SD_POS_HEAD.tax4_amt else null end as Tax4Recoverable, " & _
    '"case when tax5.Tax_Recoverable='Y' then TSPL_SD_POS_HEAD.tax5_amt else null end as Tax5Recoverable, " & _
    '"case when tax6.Tax_Recoverable='Y' then TSPL_SD_POS_HEAD.tax6_amt else null end as Tax6Recoverable," & _
    '"case when tax7.Tax_Recoverable='Y' then TSPL_SD_POS_HEAD.tax7_amt else null end as Tax7Recoverable, " & _
    '"case when tax8.Tax_Recoverable='Y' then TSPL_SD_POS_HEAD.tax8_amt else null end as Tax8Recoverable, " & _
    '"case when tax9.Tax_Recoverable='Y' then TSPL_SD_POS_HEAD.tax9_amt else null end as Tax9Recoverable," & _
    '"case when tax10.Tax_Recoverable='Y' then TSPL_SD_POS_HEAD.tax10_amt else null end as Tax10Recoverable, " & _
    '"convert(varchar,isnull (TSPL_SD_POS_HEAD.TAX1_Rate ,0),103)+'%' as txt1Rate," & _
    '"convert(varchar,isnull (TSPL_SD_POS_HEAD.TAX2_Rate   ,0),103)+'%' as txt2Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_POS_HEAD.TAX3_Rate  ,0),103)+'%' as txt3Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_POS_HEAD.TAX4_Rate  ,0),103)+'%' as txt4Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_POS_HEAD.TAX5_Rate  ,0),103)+'%' as txt5Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_POS_HEAD.TAX6_Rate  ,0),103)+'%' as txt6Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_POS_HEAD.TAX7_Rate  ,0),103)+'%' as txt7Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_POS_HEAD.TAX8_Rate  ,0),103)+'%' as txt8Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_POS_HEAD.TAX9_Rate  ,0),103)+'%' as txt9Rate, " & _
    '"convert(varchar,isnull (TSPL_SD_POS_HEAD.TAX10_Rate  ,0),103)+'%' as txt10Rate," & _
    '"TSPL_SD_POS_DETAIL.Amt_Less_Discount as Value,(select SUM(rejected_qty) from TSPL_SD_POS_DETAIL where Document_Code=TSPL_SD_POS_HEAD.Document_Code) as Rej_qty, (select SUM(TSPL_MRN_DETAIL.MRN_Qty) from TSPL_SD_POS_DETAIL left outer join TSPL_MRN_DETAIL on TSPL_MRN_DETAIL .MRN_No=TSPL_SD_POS_DETAIL.Order_Code and TSPL_MRN_DETAIL.Item_Code=TSPL_SD_POS_DETAIL.Item_Code where Document_Code =TSPL_SD_POS_HEAD.Document_Code)as MrnTotQty, (select SUM(Qty) from TSPL_SD_POS_DETAIL where Document_Code=TSPL_SD_POS_HEAD.Document_Code) as SRNQtyTotal, (select case when COUNT(xxx.PI_No)>1 then Min(xxx.PI_No)+ ' *' else Min(xxx.PI_No)end as PINO from" & _
    '" ( select TSPL_PI_DETAIL.PI_No from TSPL_PI_DETAIL  where  TSPL_PI_DETAIL.SRN_Id= TSPL_SD_POS_HEAD.Document_Code " & _
    '" GROUP by TSPL_PI_DETAIL.PI_No)xxx) as PInvNo  ,    " & _
    '       " TSPL_SD_POS_HEAD.Add_Charge_Name1 as Add1Name, " & _
    '     " TSPL_SD_POS_HEAD.Balance_Payment as Add1 , " & _
    '     "     TSPL_SD_POS_HEAD.Add_Charge_Name2 as Add2Name, " & _
    '     "   TSPL_SD_POS_HEAD.Amount_Paid as Add2 , " & _
    '     "    TSPL_SD_POS_HEAD.Add_Charge_Name3 as Add3Name, " & _
    '     "   TSPL_SD_POS_HEAD.Balance as Add3 , " & _
    '     "    TSPL_SD_POS_HEAD.Add_Charge_Name4 as Add4Name, " & _
    '     "    TSPL_SD_POS_HEAD.Advance_Paid as Add4 , " & _
    '     "     TSPL_SD_POS_HEAD.Add_Charge_Name5 as Add5Name, " & _
    '      "     TSPL_SD_POS_HEAD.Total_Other_Charges as Add5 , " & _
    '      "     TSPL_SD_POS_HEAD.Add_Charge_Name6 as Add6Name, " & _
    '      "    TSPL_SD_POS_HEAD.Total_Freight as Add6 , " & _
    '      "    TSPL_SD_POS_HEAD.Add_Charge_Name7 as Add7Name, " & _
    '      "     TSPL_SD_POS_HEAD.Total_Amt_After_Tax as Add7 , " & _
    '      "       TSPL_SD_POS_HEAD.Credit_Card_Bank as Add8Name, " & _
    '      "      TSPL_SD_POS_HEAD.Credit_Card_Batch_No as Add8 , " & _
    '       "      TSPL_SD_POS_HEAD.Credit_Card_Approval_code as Add9Name, " & _
    '       "      TSPL_SD_POS_HEAD.Add_Charge_Amt9 as Add9 , " & _
    '       "      TSPL_SD_POS_HEAD.Debit_Card_Type as Add10Name, " & _
    '       "     TSPL_SD_POS_HEAD.Add_Charge_Amt10 as Add10,TSPL_SD_POS_HEAD.Against_RGP,TSPL_SD_POS_DETAIL .Specification   " & _
    ' " FROM  TSPL_SD_POS_DETAIL INNER JOIN TSPL_SD_POS_HEAD ON TSPL_SD_POS_DETAIL.Document_Code = TSPL_SD_POS_HEAD.Document_Code " & _
    ' "INNER JOIN TSPL_COMPANY_MASTER ON TSPL_SD_POS_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  " & _
    ' "INNER JOIN TSPL_VENDOR_MASTER ON TSPL_SD_POS_HEAD.Customer_Code = TSPL_VENDOR_MASTER.Customer_Code " & _
    ' "left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_SD_POS_HEAD.tax1  " & _
    ' "left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_SD_POS_HEAD.tax2 " & _
    ' "left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_SD_POS_HEAD .TAX3 " & _
    ' "left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_SD_POS_HEAD .tax4 " & _
    ' "left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_SD_POS_HEAD .tax5 " & _
    ' "left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_SD_POS_HEAD .TAX6  " & _
    ' "left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_SD_POS_HEAD .TAX7  " & _
    ' "left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_SD_POS_HEAD .TAX8 " & _
    ' "left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_SD_POS_HEAD .TAX9 " & _
    ' " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_SD_POS_HEAD .TAX10  " & _
    ' "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=TSPL_SD_POS_HEAD.Bill_To_Location  " & _
    ' " where TSPL_SD_POS_HEAD .Item_Type not in('F')"

    '                If FromDate.HasValue AndAlso ToDate.HasValue Then
    '                    strquery += " and Convert(date,TSPL_SD_POS_HEAD.Document_Date,103)>=Convert(date,'" + FromDate + "',103)and Convert(date,TSPL_SD_POS_HEAD.Document_Date,103)<=Convert(date,'" + ToDate + "',103) "

    '                End If
    '                If ArrLocation IsNot Nothing AndAlso ArrLocation.Count > 0 Then
    '                    strquery += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(ArrLocation) + ") "
    '                End If
    '                If ArrSrnNo IsNot Nothing AndAlso ArrSrnNo.Count > 0 Then
    '                    strquery += " and TSPL_SD_POS_HEAD.Document_Code in (" + clsCommon.GetMulcallString(ArrSrnNo) + ")  "
    '                End If
    '                If ArrVendor IsNot Nothing AndAlso ArrVendor.Count > 0 Then
    '                    strquery += " and TSPL_SD_POS_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(ArrVendor) + ")  "

    '                End If
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strquery)
    '                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '                    common.clsCommon.MyMessageBoxShow("No Record Found")
    '                Else
    '                    PurchaseOrderViewer.funreport(dt, "SRNReportThroughReport", "Store Receipt Report")
    '                End If
    '            End If

    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Sub
End Class

Public Class clsSNPOSDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Line_No As Integer = 0

    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing 'Not a Table Field
    Public Qty As Double = 0
    Public Unit_code As String = Nothing '
    Public Item_Cost As Double = 0
    Public Amount As Double = 0
    Public Disc_Per As Double = 0
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Amt_After_Tax As Double = 0
    Public Freight As Double = 0
    Public Other_Charges As Double = 0
    Public Item_Net_Amt As Double = 0
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
    Public Order_Code As String = Nothing

    Public Bar_code As String = Nothing
    Public MRP As Double = 0
    Public Header_Tax_Group As String = Nothing 'Not a Table Field
    Public ItemwiseTaxCode As String
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsSNPOSDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSNPOSDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)

                clsCommon.AddColumnsForChange(coll, "Bar_code", obj.Bar_code, True)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)

                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Per", obj.Disc_Per)
                clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", obj.Amt_Less_Discount)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_After_Tax", obj.Amt_After_Tax)

                clsCommon.AddColumnsForChange(coll, "Freight", obj.Freight)
                clsCommon.AddColumnsForChange(coll, "Other_Charges", obj.Other_Charges)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)

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
                clsCommon.AddColumnsForChange(coll, "Order_Code", obj.Order_Code, True)
                clsCommon.AddColumnsForChange(coll, "ItemwiseTaxCode", obj.ItemwiseTaxCode, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_POS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceSRNQty(ByVal strSRNCode As String, ByVal strICode As String, ByVal strCurrPINNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " & _
            " select TSPL_SD_POS_DETAIL.Item_Code as ICode,TSPL_SD_POS_DETAIL.Qty as Qty,1 as RI from TSPL_SD_POS_DETAIL left outer join TSPL_SD_POS_HEAD on TSPL_SD_POS_HEAD.Document_Code=TSPL_SD_POS_DETAIL.Document_Code where TSPL_SD_POS_DETAIL.Status=0 and TSPL_SD_POS_HEAD.Status=1 and TSPL_SD_POS_DETAIL.Document_Code ='" + strSRNCode + "' and TSPL_SD_POS_DETAIL.Item_Code='" + strICode + "' and  TSPL_SD_POS_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_SD_POS_DETAIL.Amt_After_Tax,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_SD_POS_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' " & _
            " union all " & _
            " select TSPL_PI_DETAIL.Item_Code as ICode,TSPL_PI_DETAIL.PI_Qty as Qty,-1 as RI from TSPL_PI_DETAIL left outer join TSPL_PI_HEAD on TSPL_PI_HEAD.PI_No=TSPL_PI_DETAIL.PI_No where TSPL_PI_DETAIL.SRN_Id='" + strSRNCode + "'   and TSPL_PI_DETAIL.Item_Code='" + strICode + "'  and  TSPL_PI_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_PI_DETAIL.Amt_After_Tax,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PI_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'  and TSPL_PI_DETAIL.PI_No not in ('" + strCurrPINNo + "')  " & _
            " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompleteSRN(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_SD_POS_DETAIL set Status=1 where Document_Code='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class
