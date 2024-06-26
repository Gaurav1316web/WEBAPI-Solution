'=============BM00000003058,updated by Rohit,Create new Setting(Return Without Invoice).=============
'-----------------BM00000003051

Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

<Serializable()> _
Public Class clsPurchaseOrderHead
    Implements ICloneable
#Region "Variables"
    Public Bank_Code As String = Nothing
    Public Payment_Code As String = Nothing
    Public Deliverydays As Integer = 0
    Public Emergency As Integer = Nothing
    Public strScheduleNo As String = Nothing
    Public IsPOSeriesWithoutItemwise As Boolean = False
    Public Delivery_Terms_Code As String = Nothing
    Public Payment_Terms As String = Nothing
    Public Insurance_Terms As String = Nothing
    Public Against_PO As String = Nothing
    Public Renewal_Date As String = Nothing
    Public Is_Open_PO As Integer = Nothing
    Public IsCancel As Integer = 0
    Public Capacity As String = Nothing
    Public Make As String = Nothing
    Public Model As String = Nothing
    Public roadpermit As String = Nothing
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Cform As String = Nothing
    Public Auto_PO As String = Nothing
    Public PurchaseOrder_No As String = Nothing
    Public PurchaseOrder_Date As DateTime = Nothing
    Public Invdate As Date? = Nothing
    Public Delivery_date As String = Nothing
    Public Delivery_Duration As String = ""
    Public PurchaseOrder_Type As String = Nothing
    Public Against_RGP As Integer = 0
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Against_RGP_NO As String = Nothing
    Public On_Hold As Boolean = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Bill_To_Location As String = Nothing
    Public BillToLocationName As String = Nothing
    Public Ship_To_Location As String = Nothing
    Public ShipToLocationName As String = Nothing
    Public Tax_Group As String = Nothing

    Public close_yn As String
    Public close_remarks As String

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
    Public Header_Discount_Amount As Decimal = 0
    Public Discount_Amt As Double = 0
    Public Amount_Less_Discount As Double = 0
    Public PO_Total_Amt As Double = 0
    Public Mode_Of_Transport As String = Nothing
    Public Comments As String = Nothing
    Public Comment1 As String = Nothing
    Public Comment2 As String = Nothing
    Public Comment3 As String = Nothing
    Public Comment4 As String = Nothing
    Public Comment5 As String = Nothing
    Public Comment6 As String = Nothing
    Public Comment7 As String = Nothing
    Public Comment8 As String = Nothing
    Public Comment9 As String = Nothing
    Public Comment10 As String = Nothing
    Public Comment11 As String = Nothing
    Public Comment12 As String = Nothing
    Public Comment13 As String = Nothing
    Public Comment14 As String = Nothing
    Public Comp_Code As String = Nothing
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Terms_Remark As String = Nothing
    Public Due_Date As String = Nothing
    Public Posting_Date As DateTime?
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Abandonment_No As Double = 0
    Public Subject As String = Nothing
    Public Content_Subject As String = Nothing
    Public Kind_Attentation As String = Nothing

    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing

    Public Against_Tender As String = "N"
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
    Public Amt_After_Tax As Double = 0
    Public Add_Charge_Apply_On1 As String = Nothing
    Public Add_Charge_Per1 As Double = 0
    Public Add_Charge_Apply_On2 As String = Nothing
    Public Add_Charge_Per2 As Double = 0
    Public Add_Charge_Apply_On3 As String = Nothing
    Public Add_Charge_Per3 As Double = 0
    Public Add_Charge_Apply_On4 As String = Nothing
    Public Add_Charge_Per4 As Double = 0
    Public Add_Charge_Apply_On5 As String = Nothing
    Public Add_Charge_Per5 As Double = 0
    Public Add_Charge_Apply_On6 As String = Nothing
    Public Add_Charge_Per6 As Double = 0
    Public Add_Charge_Apply_On7 As String = Nothing
    Public Add_Charge_Per7 As Double = 0
    Public Add_Charge_Apply_On8 As String = Nothing
    Public Add_Charge_Per8 As Double = 0
    Public Add_Charge_Apply_On9 As String = Nothing
    Public Add_Charge_Per9 As Double = 0
    Public Add_Charge_Apply_On10 As String = Nothing
    Public Add_Charge_Per10 As Double = 0

    Public Insurance As String = Nothing
    Public Packing_Forward As String = Nothing
    Public Freight As String = ""
    Public Retention As Decimal = 0

    Public Against_Requisition As String = Nothing

    Public Quotation_No As String = Nothing
    Public Quotation_Date As DateTime? = Nothing
    Public is_Excise_On_Qty As Boolean = False
    Public AssessableAmt As Decimal = 0
    Public Against_Vendor_Quotation As String = ""
    Public Arr As List(Of clsPurchaseOrderDetail) = Nothing
    Public Arr_Road As List(Of clsPurchaseOrderRoadDetail) = Nothing
    Public Arr_CFORM As List(Of clsPurchaseOrderCFORMDetail) = Nothing

    Public Arr_FieldCategory As List(Of clsPurchaseOrderRoadDetail) = Nothing
    Public Arr_Terms_C As List(Of clsPurchaseOrderRoadDetail) = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public PROJECT_ID As String = Nothing
    Public IsAbatementPO As Integer = 0
    Public Expiry_Date As Date?
    Public MCC_Purchase As Integer = 0
    Public State_Code As String = ""
    Public PO_Amount As Double
    Public isBlanket As Integer = 0
    '===shivani
    Public IsPO As Integer = 0
    Public IsContent As Integer = 0
    '====
    Public isApproved As Integer = 0
    ''richa agarwal 24/12/2014
    Public MT_Is_Merchant_Trade As Integer = 0
    Public MT_PI_No As String = ""
    Public MT_HS_Classification_No As String = ""
    Public MT_PI_Status As String = ""
    Public MT_PI_Status_Date As Date? = Nothing
    Public MT_Payment_Terms_Group_Code As String = ""
    Public MT_Is_AmountinRs As Integer = 0
    Public MT_LC As Double = 0
    Public MT_CAD As Double = 0
    Public MT_RETAINED As Double = 0
    Public MT_CIF As Double = 0
    Public MT_Balance_Payment As Double = 0
    Public MT_On_Account As Double = 0
    Public MT_Advance As Double = 0
    Public MT_Beneficiary_Code As String = ""
    Public MT_INCOTERMS As String = ""
    Public Auto_Calculate As Integer = 0
    Public MT_Buyer_PO_No As String = Nothing
    Public MT_Buyer_PO_Date As Date? = Nothing
    ''----------------
    ''richa agarwal 08/04/2015 against ticket no BM00000006065
    Public MT_Pre_Carriage_By As String = Nothing
    Public MT_PI_Due_Date As Date? = Nothing
    Public MT_Carrier As String = Nothing
    Public MT_Discharge_Port As String = Nothing
    Public MT_Final_Destination As String = Nothing
    Public MT_Origin_Country As String = Nothing
    Public MT_Final_Destination_Country As String = Nothing
    Public MT_CreditTerms_Code As String = Nothing
    Public MT_Stuffing_Status As String = Nothing
    Public MT_Payment_Terms As String = Nothing
    Public MT_EX_Term_Code As String = Nothing
    Public MT_Accepted_Date As Date? = Nothing
    Public MT_is_Accepted As String = Nothing
    Public MT_is_Partshipment As String = Nothing
    Public MT_is_Transshipment As String = Nothing
    Public MT_CreditTermsName As String = Nothing
    Public MT_PT_Advance_Amount As Double = 0
    Public MT_is_Partpayment As String = Nothing
    Public MT_Advance_Type As String = Nothing
    Public Schedule_Type As String = Nothing   '' only for reading purpose in MRP
    ''----------------------------------
    'stuti
    Public Capex_Code As String = Nothing
    Public Capex_SubCode As String = Nothing
    Public Category As String = Nothing

    '====end here======1
    Public Apply_Receive_Control As Boolean = False
    Public ServiceBill_No As String = Nothing
    Public ServiceBill_Date As String = Nothing
    Public Tot_Empty_Amount As Double = 0
    Public Is_Shortage_Include_In_Landed_Cost As Boolean = False
    Public GSTRegistered As Boolean = False
    Public NumDocAmt As Decimal = 0
    Public Sublocation_Code As String
    Public SubLocationName As String
    Public Total_Taxable_Amount As Decimal
    Public isJobWorkOutward As Boolean = False
    Public WorkOrder_Vendor As String = Nothing
    Public WorkOrder_Vendor_Add As String = Nothing
    Public WorkOrder_Vendor_Phn As String = Nothing

    Public SendMailForAdvancePaymenTerms As Boolean = False
    Public Is_Repair As Boolean = False
    Public ReferencePO As String = Nothing

    Public Confirmatory_PO_SRN_No As String = Nothing
    Public RefTendorNo As String = Nothing
    Public Against_WorkEstimation_Id As String = Nothing

    Public Total_Item_Insurance_Amt As Decimal = 0
    Public Total_Add_Charge_Insurance As Decimal = 0
    Public Arr_ACInsurance As List(Of clsPurchaseOrderAdditionChargeInsurance) = Nothing
    Public ArrSchedule As List(Of clsTenderSchedulePO) = Nothing
    Public objPIRemittance As clsPIRemittance = Nothing

#End Region

    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PURCHASE_ORDER_HEAD", "PurchaseOrder_No", "TSPL_PURCHASE_ORDER_DETAIL", "PurchaseOrder_No", "TSPL_PI_REMITTANCE", "Document_No", trans)
        Return True
    End Function
    Public Shared Function CancleUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PURCHASE_ORDER_HEAD", "PurchaseOrder_No", "TSPL_PURCHASE_ORDER_DETAIL", "PurchaseOrder_No", "TSPL_PI_REMITTANCE", "Document_No", trans)
        Return True
    End Function
    Public Shared Function CancelData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PURCHASE_ORDER_HEAD", "PurchaseOrder_No", "TSPL_PURCHASE_ORDER_DETAIL", "PurchaseOrder_No", "TSPL_PI_REMITTANCE", "Document_No", trans)
        Return True
    End Function

    ''Note Very Important If any change mad in PO Head or PO Detail table allso update it's History table.
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim m As New MemoryStream()
        Dim f As New BinaryFormatter()
        f.Serialize(m, Me)
        m.Seek(0, SeekOrigin.Begin)
        Return f.Deserialize(m)
    End Function
    Public Shared Function CreateMultiplePOs(ByVal ArrPO As List(Of clsPurchaseOrderHead)) As List(Of String)
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim ArrCreatedPO As New List(Of String)()
        Try
            For Each obj As clsPurchaseOrderHead In ArrPO
                obj.SaveData(obj, True, False, trans)
                ArrCreatedPO.Add(obj.PurchaseOrder_No)
            Next
            '' Throw New Exception("Exception")
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return ArrCreatedPO
    End Function

    Public Shared Function GetPurchaseTypeName(ByVal PurchaseType_Code As String) As String
        Dim str As String = ""
        If clsCommon.CompairString(PurchaseType_Code, "") = CompairStringResult.Equal Then
            str = "None"
        ElseIf clsCommon.CompairString(PurchaseType_Code, "L") = CompairStringResult.Equal Then
            str = "Domestic"
        ElseIf clsCommon.CompairString(PurchaseType_Code, "I") = CompairStringResult.Equal Then
            str = "Import"
        ElseIf clsCommon.CompairString(PurchaseType_Code, "J") = CompairStringResult.Equal Then
            str = "Job Work"
        ElseIf clsCommon.CompairString(PurchaseType_Code, "S") = CompairStringResult.Equal Then
            str = "Work Order(Service PO)"
        End If

        Return str
    End Function

    Public Shared Function GetBalanceQty(ByVal PoNo As String, ByVal ItemCode As String) As String
        Dim str As String = ""
        Dim qry As String = ""
        Try
            qry = "select sum(xx.Qty) from (select (max(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty)+(-sum(TSPL_GRN_DETAIL.GRN_Qty)+sum(TSPL_GRN_DETAIL.Tolerence_Qty))) as Qty from TSPL_PURCHASE_ORDER_DETAIL " &
                            " left outer join TSPL_GRN_DETAIL  on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_GRN_DETAIL.PO_Id and TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code " &
                            " where TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No='" + clsCommon.myCstr(PoNo) + "' and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + clsCommon.myCstr(ItemCode) + "'"

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.OpenPOforRejectShortageQty, clsFixedParameterCode.OpenPOforRejectShortageQty, Nothing)) = 1 Then
                qry += " union all " + Environment.NewLine +
                " select (isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)+isnull(TSPL_SRN_DETAIL.Rejected_Qty,0)) as Qty from TSPL_SRN_DETAIL   left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No " + Environment.NewLine +
                " where  TSPL_SRN_DETAIL.Item_Code='" + clsCommon.myCstr(ItemCode) + "' and TSPL_SRN_DETAIL.PO_ID='" + clsCommon.myCstr(PoNo) + "' "
            End If

            qry += " )xx "
            str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.CompairString(str, "") = CompairStringResult.Equal Then
                qry = "select TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty from TSPL_PURCHASE_ORDER_DETAIL where TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No='" + clsCommon.myCstr(PoNo) + "' and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + clsCommon.myCstr(ItemCode) + "'"
                str = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function

    Public Shared Function LoadPurchaseType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "Domestic"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "I"
        dr("Name") = "Import"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "J"
        dr("Name") = "Job Work"
        dt.Rows.Add(dr)

        Dim PurchaseOrderthroughAP As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UDLPurchaseOrderthroughAP, clsFixedParameterCode.UDLPurchaseOrderthroughAP, Nothing)) = 1, True, False)
        If PurchaseOrderthroughAP = True Then
            dr = dt.NewRow()
            dr("Code") = "S"
            dr("Name") = "Services"
            dt.Rows.Add(dr)
        End If

        Return dt
    End Function

    Public Function SaveData(ByVal obj As clsPurchaseOrderHead, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean) As Boolean
        Dim isSaved As Boolean = True
        IsPOSeriesWithoutItemwise = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.POSeriesWithoutItemTypewise, clsFixedParameterCode.POSeriesWithoutItemTypewise, Nothing)) = "1", True, False))

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, isMakeAbandomentNo, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsPurchaseOrderHead, ByVal isNewEntry As Boolean, ByVal isMakeAbandomentNo As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Order", obj.Bill_To_Location, obj.PurchaseOrder_Date, trans)
            '' Anubhooti 22-Aug-2014 (Amandment After Posting,Make cond isMakeAbandomentNo = False )
            If isMakeAbandomentNo = False Then
                If Not isNewEntry Then
                    If Not clsCommon.myCBool(obj.IsCancel) = True Then
                        Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_PURCHASE_ORDER_HEAD Where PurchaseOrder_No='" + obj.PurchaseOrder_No + "'", trans))
                        If Status = 1 Then
                            Throw New Exception("This document is already posted.")
                        End If
                    End If

                End If
            End If
            Dim qry As String = ""
            If isMakeAbandomentNo Then
                clsPurchaseOrderHeadHist.SaveDataForHistory(obj.PurchaseOrder_No, clsCommon.myCdbl(obj.Abandonment_No + 1), trans)
                qry = "select 1 from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL where TRANS_Code='PO-ODR' and Document_Code in ( '" + obj.PurchaseOrder_No + "')"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = "update TSPL_PURCHASE_ORDER_HEAD set Status=0,Posting_Date=null where PurchaseOrder_No='" + obj.PurchaseOrder_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If

            clsPurchaseOrderAdditionChargeInsurance.DeleteData(obj.PurchaseOrder_No, trans)
            '--varsha add
            'clsPurchaseOrderHeadHist.SaveCancleData(obj.PurchaseOrder_No, "", trans)

            '--varsha end

            If Not isNewEntry Then
                HistoryUpdate(obj.PurchaseOrder_No, trans)
            End If
            'If Not isNewEntry Then
            '    CancleUpdate(obj.PurchaseOrder_No, trans)
            'End If

            qry = "delete from TSPL_PI_REMITTANCE where Document_No='" + obj.PurchaseOrder_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + obj.PurchaseOrder_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TENDER_SCHEDULE_PENALTY_PO where DocumentCode='" + obj.PurchaseOrder_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_TENDER_SCHEDULE_PO where DocumentCode='" + obj.PurchaseOrder_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                If obj.isJobWorkOutward = True Then
                    obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.PurchaserOrderOutward, clsDocTransactionType.POJobWorkOutward, obj.Sublocation_Code)
                ElseIf clsCommon.CompairString(clsUserMgtCode.WorkOrderEng, obj.Form_ID) = CompairStringResult.Equal Then
                    obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.WorkOrderEng, clsDocTransactionType.POJobWork, obj.Bill_To_Location)
                Else
                    Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PurchaseCounterOnTransactionType, clsFixedParameterCode.PurchaseCounterOnTransactionType, trans)) = 0, False, True) ''Make Setting Balwinder
                    If isPODocumentTypeWise Then
                        If clsCommon.CompairString(obj.MT_Is_Merchant_Trade, "1") = CompairStringResult.Equal Then
                            obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.MerchantTradePO, "", obj.Bill_To_Location)
                        Else
                            If clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal Then
                                obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.PurchaserOrder, clsDocTransactionType.POJobWork, obj.Bill_To_Location)
                            ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "I") = CompairStringResult.Equal Then
                                obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.PurchaserOrder, clsDocTransactionType.POImport, obj.Bill_To_Location)
                            ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "L") = CompairStringResult.Equal Then
                                obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.PurchaserOrder, clsDocTransactionType.PODomestic, obj.Bill_To_Location)
                            ElseIf clsCommon.CompairString(obj.PurchaseOrder_Type, "S") = CompairStringResult.Equal Then
                                obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.PurchaserOrder, clsDocTransactionType.Other, obj.Bill_To_Location)
                            Else
                                Throw New Exception("Type is Not Correct To Generate the Transaction Code")
                            End If
                        End If
                    ElseIf IsPOSeriesWithoutItemwise Then
                        obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.PurchaserOrder, "", obj.Bill_To_Location)
                    Else
                        ''richa agarwal 07/04/2015 change PO series separately for merchant trade
                        If clsCommon.CompairString(obj.MT_Is_Merchant_Trade, "1") = CompairStringResult.Equal Then
                            obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.MerchantTradePO, "", obj.Bill_To_Location)
                        Else
                            If clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal Then
                                obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.PurchaserOrder, clsDocTransactionType.POJobWork, obj.Bill_To_Location)
                            Else
                                Dim TransType = clsDBFuncationality.getSingleValue("SELECT PREFIX_CODE FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE='" + obj.Item_Type + "'", trans)
                                obj.PurchaseOrder_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PurchaseOrder_Date), clsDocType.PurchaserOrder, TransType, obj.Bill_To_Location)
                                If clsCommon.CompairString(obj.PurchaseOrder_No, String.Empty) = CompairStringResult.Equal Then
                                    Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                                End If
                            End If
                        End If
                    End If
                End If

            End If
            If (clsCommon.myLen(obj.PurchaseOrder_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ReferencePO", obj.ReferencePO)
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Date", clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Delivery_date", clsCommon.GetPrintDate(obj.Delivery_date, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Delivery_Duration", obj.Delivery_Duration)
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Type", obj.PurchaseOrder_Type)
            clsCommon.AddColumnsForChange(coll, "Against_RGP", obj.Against_RGP)
            clsCommon.AddColumnsForChange(coll, "Against_Vendor_Quotation", obj.Against_Vendor_Quotation, True)
            clsCommon.AddColumnsForChange(coll, "Is_Open_PO", obj.Is_Open_PO)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location, True)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Location", obj.Ship_To_Location, True)
            clsCommon.AddColumnsForChange(coll, "Sublocation_Code", obj.Sublocation_Code, True)
            clsCommon.AddColumnsForChange(coll, "Confirmatory_PO_SRN_No", obj.Confirmatory_PO_SRN_No, True)
            clsCommon.AddColumnsForChange(coll, "isJobWorkOutward", IIf(obj.isJobWorkOutward, 1, 0))
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
            clsCommon.AddColumnsForChange(coll, "Total_Taxable_Amount", obj.Total_Taxable_Amount)
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "PO_Total_Amt", obj.PO_Total_Amt)

            clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.Discount_Base)
            clsCommon.AddColumnsForChange(coll, "Header_Discount_Amount", obj.Header_Discount_Amount)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)

            clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Comment1", obj.Comment1)
            clsCommon.AddColumnsForChange(coll, "Comment2", obj.Comment2)
            clsCommon.AddColumnsForChange(coll, "Comment3", obj.Comment3)
            clsCommon.AddColumnsForChange(coll, "Comment4", obj.Comment4)
            clsCommon.AddColumnsForChange(coll, "Comment5", obj.Comment5)
            clsCommon.AddColumnsForChange(coll, "Comment6", obj.Comment6)
            clsCommon.AddColumnsForChange(coll, "Comment7", obj.Comment7)
            clsCommon.AddColumnsForChange(coll, "Comment8", obj.Comment8)
            clsCommon.AddColumnsForChange(coll, "Comment9", obj.Comment9)
            clsCommon.AddColumnsForChange(coll, "Comment10", obj.Comment10)
            clsCommon.AddColumnsForChange(coll, "Comment11", obj.Comment11)
            clsCommon.AddColumnsForChange(coll, "Comment12", obj.Comment12)
            clsCommon.AddColumnsForChange(coll, "Comment13", obj.Comment13)
            'clsCommon.AddColumnsForChange(coll, "Comment14", obj.Comment14)
            clsCommon.AddColumnsForChange(coll, "Against_RGP_NO", obj.Against_RGP_NO, True)

            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Against_Requisition", obj.Against_Requisition, True)
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)

            clsCommon.AddColumnsForChange(coll, "Against_Tender", obj.Against_Tender)
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


            clsCommon.AddColumnsForChange(coll, "Amt_After_Tax", obj.Amt_After_Tax)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Apply_On1", obj.Add_Charge_Apply_On1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Per1", obj.Add_Charge_Per1)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Apply_On2", obj.Add_Charge_Apply_On2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Per2", obj.Add_Charge_Per2)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Apply_On3", obj.Add_Charge_Apply_On3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Per3", obj.Add_Charge_Per3)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Apply_On4", obj.Add_Charge_Apply_On4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Per4", obj.Add_Charge_Per4)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Apply_On5", obj.Add_Charge_Apply_On5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Per5", obj.Add_Charge_Per5)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Apply_On6", obj.Add_Charge_Apply_On6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Per6", obj.Add_Charge_Per6)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Apply_On7", obj.Add_Charge_Apply_On7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Per7", obj.Add_Charge_Per7)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Apply_On8", obj.Add_Charge_Apply_On8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Per8", obj.Add_Charge_Per8)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Apply_On9", obj.Add_Charge_Apply_On9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Per9", obj.Add_Charge_Per9)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Apply_On10", obj.Add_Charge_Apply_On10)
            clsCommon.AddColumnsForChange(coll, "Add_Charge_Per10", obj.Add_Charge_Per10)
            clsCommon.AddColumnsForChange(coll, "Total_Add_Charge", obj.Total_Add_Charge)



            clsCommon.AddColumnsForChange(coll, "Payment_Terms", obj.Payment_Terms)
            clsCommon.AddColumnsForChange(coll, "Insurance_Terms", obj.Insurance_Terms)
            clsCommon.AddColumnsForChange(coll, "Delivery_Terms_Code", obj.Delivery_Terms_Code, True)
            clsCommon.AddColumnsForChange(coll, "Subject", obj.Subject)
            clsCommon.AddColumnsForChange(coll, "Content_Subject", obj.Content_Subject)
            clsCommon.AddColumnsForChange(coll, "Kind_Attentation", obj.Kind_Attentation)
            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Quotation_No", obj.Quotation_No)
            If obj.Quotation_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "Quotation_Date", clsCommon.GetPrintDate(obj.Quotation_Date, "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Quotation_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "is_Excise_On_Qty", IIf(obj.is_Excise_On_Qty, 1, 0))
            clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Terms_Remark", obj.Terms_Remark)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isMakeAbandomentNo Then
                Dim AmendmentCode As String = Nothing
                AmendmentCode = obj.PurchaseOrder_No + "$" + clsCommon.myCstr(obj.Abandonment_No + 1)
                clsCommon.AddColumnsForChange(coll, "Amendment_Code", clsCommon.myCstr(AmendmentCode))
                clsCommon.AddColumnsForChange(coll, "Amendment_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Amendment_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            End If


            clsCommon.AddColumnsForChange(coll, "close_yn", obj.close_yn)
            clsCommon.AddColumnsForChange(coll, "close_remarks", obj.close_remarks)
            clsCommon.AddColumnsForChange(coll, "Against_PO", obj.Against_PO)
            If clsCommon.myLen(obj.Renewal_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Renewal_Date", clsCommon.GetPrintDate(obj.Renewal_Date, "dd/MMM/yyyy"))
            End If

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            If obj.ApplicableFrom Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"), True)
            End If

            'clsCommon.AddColumnsForChange(coll, "ApplicableFrom", obj.ApplicableFrom, True)
            '' End currencyconversion
            '' for abatement PO
            clsCommon.AddColumnsForChange(coll, "IsAbatementPO", obj.IsAbatementPO)
            If clsCommon.myLen(obj.Expiry_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy"), True)
            Else
                clsCommon.AddColumnsForChange(coll, "Expiry_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "MCC_Purchase", obj.MCC_Purchase)
            clsCommon.AddColumnsForChange(coll, "Auto_Purchase", obj.Auto_PO)
            clsCommon.AddColumnsForChange(coll, "State_Code", obj.State_Code, True)
            clsCommon.AddColumnsForChange(coll, "PO_Amount", obj.PO_Amount)
            clsCommon.AddColumnsForChange(coll, "isBlanket", obj.isBlanket)
            clsCommon.AddColumnsForChange(coll, "IsPO", obj.IsPO)
            clsCommon.AddColumnsForChange(coll, "IsContent", obj.IsContent)
            clsCommon.AddColumnsForChange(coll, "Issue_Road_Permit", obj.roadpermit)
            clsCommon.AddColumnsForChange(coll, "Issue_C_Form", obj.Cform)

            ''richa agarwal 24/12/2014
            clsCommon.AddColumnsForChange(coll, "MT_Is_Merchant_Trade", obj.MT_Is_Merchant_Trade)
            clsCommon.AddColumnsForChange(coll, "MT_PI_No", obj.MT_PI_No)
            clsCommon.AddColumnsForChange(coll, "MT_PI_Status", obj.MT_PI_Status, True)
            If obj.MT_PI_Status_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "MT_PI_Status_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "MT_PI_Status_Date", clsCommon.GetPrintDate(obj.MT_PI_Status_Date, "dd/MMM/yyyy"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "MT_HS_Classification_No", obj.MT_HS_Classification_No)
            clsCommon.AddColumnsForChange(coll, "MT_Payment_Terms_Group_Code", obj.MT_Payment_Terms_Group_Code, True)
            clsCommon.AddColumnsForChange(coll, "MT_Is_AmountinRs", obj.MT_Is_AmountinRs)
            clsCommon.AddColumnsForChange(coll, "MT_LC", obj.MT_LC)
            clsCommon.AddColumnsForChange(coll, "MT_CAD", obj.MT_CAD)
            clsCommon.AddColumnsForChange(coll, "MT_RETAINED", obj.MT_RETAINED)
            clsCommon.AddColumnsForChange(coll, "MT_CIF", obj.MT_CIF)
            clsCommon.AddColumnsForChange(coll, "MT_Balance_Payment", obj.MT_Balance_Payment)
            clsCommon.AddColumnsForChange(coll, "MT_On_Account", obj.MT_On_Account)
            clsCommon.AddColumnsForChange(coll, "MT_Advance", obj.MT_Advance)
            clsCommon.AddColumnsForChange(coll, "MT_Beneficiary_Code", obj.MT_Beneficiary_Code)
            clsCommon.AddColumnsForChange(coll, "MT_Buyer_PO_No", obj.MT_Buyer_PO_No)
            If obj.MT_Buyer_PO_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "MT_Buyer_PO_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "MT_Buyer_PO_Date", clsCommon.GetPrintDate(obj.MT_Buyer_PO_Date, "dd/MMM/yyyy"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "MT_INCOTERMS", obj.MT_INCOTERMS, True)
            clsCommon.AddColumnsForChange(coll, "Auto_Calculate", obj.Auto_Calculate)
            ''-----------------------
            ''richa agarwal 08/04/2014
            clsCommon.AddColumnsForChange(coll, "MT_Carrier", obj.MT_Carrier)
            clsCommon.AddColumnsForChange(coll, "MT_Pre_Carriage_By", obj.MT_Pre_Carriage_By)
            clsCommon.AddColumnsForChange(coll, "MT_Discharge_Port", obj.MT_Discharge_Port)
            clsCommon.AddColumnsForChange(coll, "MT_Final_Destination", obj.MT_Final_Destination)
            clsCommon.AddColumnsForChange(coll, "MT_Origin_Country", obj.MT_Origin_Country)
            clsCommon.AddColumnsForChange(coll, "MT_Final_Destination_Country", obj.MT_Final_Destination_Country)
            clsCommon.AddColumnsForChange(coll, "MT_CreditTerms_Code", obj.MT_CreditTerms_Code)
            clsCommon.AddColumnsForChange(coll, "MT_Stuffing_Status", obj.MT_Stuffing_Status)
            clsCommon.AddColumnsForChange(coll, "MT_Payment_Terms", obj.MT_Payment_Terms)
            clsCommon.AddColumnsForChange(coll, "MT_EX_Term_Code", obj.MT_EX_Term_Code)
            clsCommon.AddColumnsForChange(coll, "MT_is_Accepted", obj.MT_is_Accepted)
            clsCommon.AddColumnsForChange(coll, "MT_is_Partshipment", obj.MT_is_Partshipment)
            clsCommon.AddColumnsForChange(coll, "MT_is_Transshipment", obj.MT_is_Transshipment)
            clsCommon.AddColumnsForChange(coll, "MT_CreditTermsName", obj.MT_CreditTermsName)
            clsCommon.AddColumnsForChange(coll, "MT_is_Partpayment", obj.MT_is_Partpayment)
            clsCommon.AddColumnsForChange(coll, "MT_Advance_Type", obj.MT_Advance_Type)
            clsCommon.AddColumnsForChange(coll, "MT_PT_Advance_Amount", obj.MT_PT_Advance_Amount)
            If obj.MT_PI_Due_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "MT_PI_Due_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "MT_PI_Due_Date", clsCommon.GetPrintDate(obj.MT_PI_Due_Date, "dd/MMM/yyyy"), True)
            End If
            If obj.MT_Accepted_Date Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "MT_Accepted_Date", Nothing, True)
            Else
                clsCommon.AddColumnsForChange(coll, "MT_Accepted_Date", clsCommon.GetPrintDate(obj.MT_Accepted_Date, "dd/MMM/yyyy"), True)
            End If
            ''--------------------------
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code, True)
            clsCommon.AddColumnsForChange(coll, "Payment_Code", obj.Payment_Code, True)
            clsCommon.AddColumnsForChange(coll, "Capex_Code", obj.Capex_Code, True)
            clsCommon.AddColumnsForChange(coll, "Capex_SubCode", obj.Capex_SubCode, True)
            clsCommon.AddColumnsForChange(coll, "IsCancel", obj.IsCancel)
            clsCommon.AddColumnsForChange(coll, "Category", clsCommon.myCstr(obj.Category))
            clsCommon.AddColumnsForChange(coll, "Emergency", (obj.Emergency))
            clsCommon.AddColumnsForChange(coll, "Delivery_days", (obj.Deliverydays))
            clsCommon.AddColumnsForChange(coll, "Apply_Receive_Control", IIf(obj.Apply_Receive_Control, 1, 0))

            clsCommon.AddColumnsForChange(coll, "ServiceBill_No", (obj.ServiceBill_No))

            If clsCommon.myLen(obj.ServiceBill_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "ServiceBill_Date", clsCommon.GetPrintDate(obj.ServiceBill_Date, "dd/MMM/yyyy hh:mm tt"))
            End If

            'Dim GstStatus As Boolean = clsERPFuncationality.GetGSTStatus(clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy"))
            'If GstStatus Then
            '    clsCommon.AddColumnsForChange(coll, "GSTRegistered", IIf(clsVendorMaster.IsGSTRegisteredVendor(obj.Vendor_Code, trans), 1, 0))
            'Else
            '    clsCommon.AddColumnsForChange(coll, "GSTRegistered", 1)
            'End If

            clsCommon.AddColumnsForChange(coll, "GSTRegistered", IIf(obj.GSTRegistered, 1, 0))

            clsCommon.AddColumnsForChange(coll, "WorkOrder_Vendor", obj.WorkOrder_Vendor)
            clsCommon.AddColumnsForChange(coll, "WorkOrder_Vendor_Phn", obj.WorkOrder_Vendor_Phn)
            clsCommon.AddColumnsForChange(coll, "WorkOrder_Vendor_Add", obj.WorkOrder_Vendor_Add)

            clsCommon.AddColumnsForChange(coll, "Is_Repair", IIf(obj.Is_Repair, 1, 0))

            '' Work done agaist ticket no.BHA/13/08/18-000419
            clsCommon.AddColumnsForChange(coll, "Freight", obj.Freight)
            clsCommon.AddColumnsForChange(coll, "Packing_Forward", obj.Packing_Forward)
            clsCommon.AddColumnsForChange(coll, "Insurance", obj.Insurance)
            '' End
            clsCommon.AddColumnsForChange(coll, "Against_WorkEstimation_Id", obj.Against_WorkEstimation_Id, True)
            clsCommon.AddColumnsForChange(coll, "RefTendorNo", obj.RefTendorNo, True)
            clsCommon.AddColumnsForChange(coll, "From_Screen_Code", obj.Form_ID)

            clsCommon.AddColumnsForChange(coll, "Total_Add_Charge_Insurance", obj.Total_Add_Charge_Insurance)
            clsCommon.AddColumnsForChange(coll, "Total_Item_Insurance_Amt", obj.Total_Item_Insurance_Amt)
            clsCommon.AddColumnsForChange(coll, "Retention", obj.Retention)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PURCHASE_ORDER_HEAD", OMInsertOrUpdate.Insert, "", trans)
                ' clsCommonFunctionality.SaveCancelData(coll, "TSPL_PURCHASE_ORDER_HEAD_Cancel_Data", OMInsertOrUpdate.Insert, "", trans)

            Else
                If isMakeAbandomentNo Then
                    obj.Abandonment_No = obj.Abandonment_No + 1
                    clsCommon.AddColumnsForChange(coll, "Abandonment_No", obj.Abandonment_No)
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PURCHASE_ORDER_HEAD", OMInsertOrUpdate.Update, "TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" + obj.PurchaseOrder_No + "'", trans)
            End If
            clsPurchaseOrderDetail.SaveData(obj.PurchaseOrder_No, obj.Arr, trans)
            clsPIRemittance.SaveData(obj.objPIRemittance, obj.PurchaseOrder_No, obj.PurchaseOrder_Date, trans)
            clsCustomFieldValues.SaveData(obj.Form_ID, obj.PurchaseOrder_No, obj.arrCustomFields, trans)
            clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "PurchaseOrder_No", obj.PurchaseOrder_No, "TSPL_PURCHASE_ORDER_HEAD", trans)
            'If obj.PurchaseOrder_No >= 0 Then
            'clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.PurchaseOrder_No), "TSPL_PURCHASE_ORDER_HEAD", "PurchaseOrder_No", "TSPL_PURCHASE_ORDER_DETAIL", "PurchaseOrder_No", "TSPL_PI_REMITTANCE", "Document_No", trans)

            If obj.roadpermit = "1" Then
                clsPurchaseOrderRoadDetail.SaveData_RoadPermit(obj.PurchaseOrder_No, obj.Arr_Road, trans)
            End If
            If obj.Cform = "1" Then
                clsPurchaseOrderCFORMDetail.SaveData_CFORM(obj.PurchaseOrder_No, obj.Arr_CFORM, trans)
            End If
            clsPurchaseOrderRoadDetail.SaveData_WorkOrder(obj.PurchaseOrder_No, obj.Arr_FieldCategory, trans)
            clsPurchaseOrderRoadDetail.SaveData_WorkOrder_Terms(obj.PurchaseOrder_No, obj.Arr_Terms_C, trans)
            clsPurchaseOrderAdditionChargeInsurance.SaveData(obj.PurchaseOrder_No, obj.PurchaseOrder_Date, obj.Arr_ACInsurance, trans)
            clsTenderSchedulePO.SaveData(obj.PurchaseOrder_No, obj.ArrSchedule, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    ''richa agarwal 24/12/2014
    Public Shared Function UpdateAfterPosting(ByVal obj As clsPurchaseOrderHead, ByVal trans As SqlTransaction) As Boolean
        Try
            If obj IsNot Nothing And clsCommon.myLen(obj.PurchaseOrder_No) > 0 Then
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "MT_PI_Status", obj.MT_PI_Status)
                If obj.MT_PI_Status_Date Is Nothing Then
                    clsCommon.AddColumnsForChange(coll, "MT_PI_Status_Date", Nothing, True)
                Else
                    clsCommon.AddColumnsForChange(coll, "MT_PI_Status_Date", clsCommon.GetPrintDate(obj.MT_PI_Status_Date, "dd/MMM/yyyy"), True)
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PURCHASE_ORDER_HEAD", OMInsertOrUpdate.Update, "TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" + obj.PurchaseOrder_No + "'", trans)
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    ''---------------------------------
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, Optional ByVal arrLoc As String = "", Optional ByVal IsMerchantTrade As String = "", Optional ByVal FORMTYPE As String = "") As clsPurchaseOrderHead
        Return GetData(strDocumentNo, NavType, arrLoc, Nothing, IsMerchantTrade, FORMTYPE)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal arrLoc As String, ByVal trans As SqlTransaction, Optional ByVal IsMerchantTrade As String = "", Optional ByVal FORMTYPE As String = "") As clsPurchaseOrderHead
        Dim obj As clsPurchaseOrderHead = Nothing
        Dim qry As String = "SELECT TSPL_PURCHASE_ORDER_HEAD.*,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_LOCATION_MASTER_SubLocation.Location_Desc as SubLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName, " &
        " TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName  " &
        " FROM TSPL_PURCHASE_ORDER_HEAD " &
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location " &
        " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_PURCHASE_ORDER_HEAD.Ship_To_Location " &
        " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_SubLocation on TSPL_LOCATION_MASTER_SubLocation.Location_Code=TSPL_PURCHASE_ORDER_HEAD.Sublocation_Code " &
        " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_PURCHASE_ORDER_HEAD.Tax_Group " &
        " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_PURCHASE_ORDER_HEAD.Terms_Code where 2=2"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        If clsCommon.myLen(arrLoc) > 0 Then
            whrClas = " and Bill_To_Location in (" + arrLoc + ") "
        End If
        If clsCommon.CompairString(IsMerchantTrade, "MT") = CompairStringResult.Equal Then
            whrClas += " and TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade=1 "
        ElseIf clsCommon.CompairString(IsMerchantTrade, "PO") = CompairStringResult.Equal Then
            whrClas += " and TSPL_PURCHASE_ORDER_HEAD.MT_Is_Merchant_Trade=0 "
        End If
        If clsCommon.myLen(FORMTYPE) > 0 Then
            whrClas += " and TSPL_PURCHASE_ORDER_HEAD.From_Screen_Code='" + FORMTYPE + "'"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = (select MIN(PurchaseOrder_No) from TSPL_PURCHASE_ORDER_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = (select Max(PurchaseOrder_No) from TSPL_PURCHASE_ORDER_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = (select Min(PurchaseOrder_No) from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No>'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = (select Max(PurchaseOrder_No) from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No<'" + strPONo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPurchaseOrderHead()
            obj.ReferencePO = clsCommon.myCstr(dt.Rows(0)("ReferencePO"))
            obj.roadpermit = clsCommon.myCstr(dt.Rows(0)("Issue_Road_Permit"))
            obj.Cform = clsCommon.myCstr(dt.Rows(0)("Issue_C_Form"))
            obj.Payment_Terms = clsCommon.myCstr(dt.Rows(0)("Payment_Terms"))
            obj.Insurance_Terms = clsCommon.myCstr(dt.Rows(0)("Insurance_Terms"))
            obj.Delivery_Terms_Code = clsCommon.myCstr(dt.Rows(0)("Delivery_Terms_Code"))
            obj.Auto_PO = clsCommon.myCstr(dt.Rows(0)("Auto_Purchase"))
            obj.PurchaseOrder_No = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No"))
            obj.PurchaseOrder_Date = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Date"))
            obj.Delivery_date = clsCommon.myCstr(dt.Rows(0)("Delivery_date"))
            obj.Delivery_Duration = clsCommon.myCstr(dt.Rows(0)("Delivery_Duration"))
            obj.PurchaseOrder_Type = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type"))
            obj.Against_RGP = clsCommon.myCdbl(dt.Rows(0)("Against_RGP"))
            obj.close_yn = clsCommon.myCstr(dt.Rows(0)("close_yn"))
            obj.Is_Open_PO = CInt(clsCommon.myCdbl(dt.Rows(0)("Is_Open_PO")))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1 AndAlso clsCommon.myCdbl(dt.Rows(0)("iscancel")) <> 1, ERPTransactionStatus.Approved, IIf(clsCommon.myCdbl(dt.Rows(0)("iscancel")) = 1, ERPTransactionStatus.Cancel, ERPTransactionStatus.Pending))
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
            obj.Sublocation_Code = clsCommon.myCstr(dt.Rows(0)("Sublocation_Code"))
            obj.Against_RGP_NO = clsCommon.myCstr(dt.Rows(0)("Against_RGP_NO"))
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
            obj.Total_Taxable_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Taxable_Amount"))
            obj.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("Discount_Base"))
            obj.Header_Discount_Amount = clsCommon.myCdbl(dt.Rows(0)("Header_Discount_Amount"))
            obj.Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Discount_Amt"))
            obj.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("Amount_Less_Discount"))
            obj.PO_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("PO_Total_Amt"))
            obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comment1 = clsCommon.myCstr(dt.Rows(0)("Comment1"))
            obj.Comment2 = clsCommon.myCstr(dt.Rows(0)("Comment2"))
            obj.Comment3 = clsCommon.myCstr(dt.Rows(0)("Comment3"))
            obj.Comment4 = clsCommon.myCstr(dt.Rows(0)("Comment4"))
            obj.Comment5 = clsCommon.myCstr(dt.Rows(0)("Comment5"))
            obj.Comment6 = clsCommon.myCstr(dt.Rows(0)("Comment6"))
            obj.Comment7 = clsCommon.myCstr(dt.Rows(0)("Comment7"))
            obj.Comment8 = clsCommon.myCstr(dt.Rows(0)("Comment8"))
            obj.Comment9 = clsCommon.myCstr(dt.Rows(0)("Comment9"))
            obj.Comment10 = clsCommon.myCstr(dt.Rows(0)("Comment10"))
            obj.Comment11 = clsCommon.myCstr(dt.Rows(0)("Comment11"))
            obj.Comment12 = clsCommon.myCstr(dt.Rows(0)("Comment12"))
            obj.Comment13 = clsCommon.myCstr(dt.Rows(0)("Comment13"))
            ' obj.Comment14 = clsCommon.myCstr(dt.Rows(0)("Comment14"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Terms_Remark = clsCommon.myCstr(dt.Rows(0)("Terms_Remark"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName")) 'SubLocationName
            obj.SubLocationName = clsCommon.myCstr(dt.Rows(0)("SubLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))
            obj.Confirmatory_PO_SRN_No = clsCommon.myCstr(dt.Rows(0)("Confirmatory_PO_SRN_No"))

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
            End If
            obj.Quotation_No = clsCommon.myCstr(dt.Rows(0)("Quotation_No"))
            If dt.Rows(0)("Quotation_Date") IsNot DBNull.Value Then
                obj.Quotation_Date = clsCommon.myCDate(dt.Rows(0)("Quotation_Date"))
            End If
            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))
            obj.Against_Requisition = clsCommon.myCstr(dt.Rows(0)("Against_Requisition"))
            obj.Modify_By = clsCommon.myCstr(dt.Rows(0)("Modify_By"))
            obj.Modify_Date = clsCommon.myCstr(dt.Rows(0)("Modify_Date"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
            obj.Abandonment_No = clsCommon.myCdbl(dt.Rows(0)("Abandonment_No"))
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)

            obj.Against_Tender = clsCommon.myCstr(dt.Rows(0)("Against_Tender"))
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

            obj.Amt_After_Tax = clsCommon.myCdbl(dt.Rows(0)("Amt_After_Tax"))
            obj.Add_Charge_Apply_On1 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Apply_On1"))
            obj.Add_Charge_Per1 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Per1"))
            obj.Add_Charge_Apply_On2 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Apply_On2"))
            obj.Add_Charge_Per2 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Per2"))
            obj.Add_Charge_Apply_On3 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Apply_On3"))
            obj.Add_Charge_Per3 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Per3"))
            obj.Add_Charge_Apply_On4 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Apply_On4"))
            obj.Add_Charge_Per4 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Per4"))
            obj.Add_Charge_Apply_On5 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Apply_On5"))
            obj.Add_Charge_Per5 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Per5"))
            obj.Add_Charge_Apply_On6 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Apply_On6"))
            obj.Add_Charge_Per6 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Per6"))
            obj.Add_Charge_Apply_On7 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Apply_On7"))
            obj.Add_Charge_Per7 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Per7"))
            obj.Add_Charge_Apply_On8 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Apply_On8"))
            obj.Add_Charge_Per8 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Per8"))
            obj.Add_Charge_Apply_On9 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Apply_On9"))
            obj.Add_Charge_Per9 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Per9"))
            obj.Add_Charge_Apply_On10 = clsCommon.myCstr(dt.Rows(0)("Add_Charge_Apply_On10"))
            obj.Add_Charge_Per10 = clsCommon.myCdbl(dt.Rows(0)("Add_Charge_Per10"))


            obj.Total_Add_Charge = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge"))
            obj.Total_Add_Charge_Insurance = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge_Insurance"))
            obj.Total_Item_Insurance_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Item_Insurance_Amt"))
            obj.is_Excise_On_Qty = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Excise_On_Qty")) = 1, True, False)
            obj.isJobWorkOutward = IIf(clsCommon.myCdbl(dt.Rows(0)("isJobWorkOutward")) = 1, True, False)
            obj.GSTRegistered = IIf(clsCommon.myCdbl(dt.Rows(0)("GSTRegistered")) = 1, True, False)
            obj.AssessableAmt = clsCommon.myCdbl(dt.Rows(0)("AssessableAmt"))
            obj.Against_Vendor_Quotation = clsCommon.myCstr(dt.Rows(0)("Against_Vendor_Quotation"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))
            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))

            obj.MCC_Purchase = clsCommon.myCdbl(dt.Rows(0)("MCC_Purchase"))
            obj.State_Code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
            obj.PO_Amount = clsCommon.myCdbl(dt.Rows(0)("PO_Amount"))
            obj.isBlanket = clsCommon.myCdbl(dt.Rows(0)("isBlanket"))
            obj.IsPO = clsCommon.myCdbl(dt.Rows(0)("IsPO"))
            obj.IsContent = clsCommon.myCdbl(dt.Rows(0)("IsContent"))
            obj.Against_PO = clsCommon.myCstr(dt.Rows(0)("Against_PO"))
            If IsDBNull(dt.Rows(0)("Renewal_Date")) = True Then
                obj.Renewal_Date = ""
            Else
                obj.Renewal_Date = clsCommon.myCstr(dt.Rows(0)("Renewal_Date"))
            End If

            obj.isApproved = clsCommon.myCdbl(dt.Rows(0)("Is_Approved"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION 
            obj.IsAbatementPO = dt.Rows(0)("IsAbatementPO")
            If clsCommon.myLen(dt.Rows(0)("Expiry_Date")) > 0 Then
                obj.Expiry_Date = dt.Rows(0)("Expiry_Date")
            End If
            ''richa agarwal 24/12/2014
            obj.MT_Is_Merchant_Trade = clsCommon.myCdbl(dt.Rows(0)("MT_Is_Merchant_Trade"))
            obj.MT_PI_No = clsCommon.myCstr(dt.Rows(0)("MT_PI_No"))
            obj.MT_HS_Classification_No = clsCommon.myCstr(dt.Rows(0)("MT_HS_Classification_No"))
            obj.MT_PI_Status = clsCommon.myCstr(dt.Rows(0)("MT_PI_Status"))
            If IsDBNull(dt.Rows(0)("MT_PI_Status_Date")) = True Then
                obj.MT_PI_Status_Date = Nothing
            Else
                obj.MT_PI_Status_Date = clsCommon.GetPrintDate(dt.Rows(0)("MT_PI_Status_Date"), "dd/MMM/yyyy")
            End If
            obj.MT_Payment_Terms_Group_Code = clsCommon.myCstr(dt.Rows(0)("MT_Payment_Terms_Group_Code"))
            obj.MT_Is_AmountinRs = clsCommon.myCdbl(dt.Rows(0)("MT_Is_AmountinRs"))
            obj.MT_LC = clsCommon.myCdbl(dt.Rows(0)("MT_LC"))
            obj.MT_CAD = clsCommon.myCdbl(dt.Rows(0)("MT_CAD"))
            obj.MT_Advance = clsCommon.myCdbl(dt.Rows(0)("MT_Advance"))
            obj.MT_RETAINED = clsCommon.myCdbl(dt.Rows(0)("MT_RETAINED"))
            obj.MT_CIF = clsCommon.myCdbl(dt.Rows(0)("MT_CIF"))
            obj.MT_Balance_Payment = clsCommon.myCdbl(dt.Rows(0)("MT_Balance_Payment"))
            obj.MT_On_Account = clsCommon.myCdbl(dt.Rows(0)("MT_On_Account"))
            obj.MT_Beneficiary_Code = clsCommon.myCstr(dt.Rows(0)("MT_Beneficiary_Code"))
            obj.MT_Buyer_PO_No = clsCommon.myCstr(dt.Rows(0)("MT_Buyer_PO_No"))
            If IsDBNull(dt.Rows(0)("MT_Buyer_PO_Date")) = True Then
                obj.MT_Buyer_PO_Date = Nothing
            Else
                obj.MT_Buyer_PO_Date = clsCommon.GetPrintDate(dt.Rows(0)("MT_Buyer_PO_Date"), "dd/MMM/yyyy")
            End If

            obj.MT_INCOTERMS = clsCommon.myCstr(dt.Rows(0)("MT_INCOTERMS"))
            obj.Auto_Calculate = clsCommon.myCdbl(dt.Rows(0)("Auto_Calculate"))
            obj.Subject = clsCommon.myCstr(dt.Rows(0)("Subject"))
            obj.Content_Subject = clsCommon.myCstr(dt.Rows(0)("Content_Subject"))
            obj.Kind_Attentation = clsCommon.myCstr(dt.Rows(0)("Kind_Attentation"))

            ''-----------------------------
            ''richa agarwal 08/04/2015
            obj.MT_Carrier = clsCommon.myCstr(dt.Rows(0)("MT_Carrier"))
            obj.MT_Pre_Carriage_By = clsCommon.myCstr(dt.Rows(0)("MT_Pre_Carriage_By"))
            obj.MT_Discharge_Port = clsCommon.myCstr(dt.Rows(0)("MT_Discharge_Port"))
            obj.MT_Final_Destination = clsCommon.myCstr(dt.Rows(0)("MT_Final_Destination"))
            obj.MT_Origin_Country = clsCommon.myCstr(dt.Rows(0)("MT_Origin_Country"))
            obj.MT_Final_Destination_Country = clsCommon.myCstr(dt.Rows(0)("MT_Final_Destination_Country"))
            obj.MT_CreditTerms_Code = clsCommon.myCstr(dt.Rows(0)("MT_CreditTerms_Code"))
            obj.MT_Stuffing_Status = clsCommon.myCstr(dt.Rows(0)("MT_Stuffing_Status"))
            obj.MT_Payment_Terms = clsCommon.myCstr(dt.Rows(0)("MT_Payment_Terms"))
            obj.MT_EX_Term_Code = clsCommon.myCstr(dt.Rows(0)("MT_EX_Term_Code"))
            obj.MT_is_Accepted = clsCommon.myCstr(dt.Rows(0)("MT_is_Accepted"))
            obj.MT_is_Partshipment = clsCommon.myCstr(dt.Rows(0)("MT_is_Partshipment"))
            obj.MT_is_Transshipment = clsCommon.myCstr(dt.Rows(0)("MT_is_Transshipment"))
            obj.MT_CreditTermsName = clsCommon.myCstr(dt.Rows(0)("MT_CreditTermsName"))
            obj.MT_is_Partpayment = clsCommon.myCstr(dt.Rows(0)("MT_is_Partpayment"))
            obj.MT_Advance_Type = clsCommon.myCstr(dt.Rows(0)("MT_Advance_Type"))
            obj.MT_PT_Advance_Amount = clsCommon.myCdbl(dt.Rows(0)("MT_PT_Advance_Amount"))
            If IsDBNull(dt.Rows(0)("MT_PI_Due_Date")) = True Then
                obj.MT_PI_Due_Date = Nothing
            Else
                obj.MT_PI_Due_Date = clsCommon.GetPrintDate(dt.Rows(0)("MT_PI_Due_Date"), "dd/MMM/yyyy")
            End If
            If IsDBNull(dt.Rows(0)("MT_Accepted_Date")) = True Then
                obj.MT_Accepted_Date = Nothing
            Else
                obj.MT_Accepted_Date = clsCommon.GetPrintDate(dt.Rows(0)("MT_Accepted_Date"), "dd/MMM/yyyy")
            End If
            'stuti'
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Payment_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Code"))
            obj.Capex_Code = clsCommon.myCstr(dt.Rows(0)("Capex_Code"))
            obj.Capex_SubCode = clsCommon.myCstr(dt.Rows(0)("Capex_SubCode"))
            obj.IsCancel = CInt(dt.Rows(0)("IsCancel"))
            obj.Category = clsCommon.myCstr(dt.Rows(0)("Category"))
            obj.Emergency = CInt(dt.Rows(0)("Emergency"))
            obj.Deliverydays = CInt(dt.Rows(0)("Delivery_days"))
            obj.Apply_Receive_Control = IIf(clsCommon.myCdbl(dt.Rows(0)("Apply_Receive_Control")) = 1, True, False)
            obj.ServiceBill_No = clsCommon.myCstr(dt.Rows(0)("ServiceBill_No"))
            If dt.Rows(0)("ServiceBill_Date") IsNot DBNull.Value Then
                obj.ServiceBill_Date = clsCommon.myCstr(dt.Rows(0)("ServiceBill_Date"))
            End If

            If dt.Rows(0)("PurchaseOrder_Date") IsNot DBNull.Value Then
                obj.Invdate = clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Date"))
            End If

            obj.NumDocAmt = clsCommon.myCdbl(dt.Rows(0)("PO_Total_Amt"))

            '' Update Work Order related function done by parteek 31/10/2017 client UDL

            obj.WorkOrder_Vendor = clsCommon.myCstr(dt.Rows(0)("WorkOrder_Vendor"))
            obj.WorkOrder_Vendor_Add = clsCommon.myCstr(dt.Rows(0)("WorkOrder_Vendor_Add"))
            obj.WorkOrder_Vendor_Phn = clsCommon.myCstr(dt.Rows(0)("WorkOrder_Vendor_Phn"))

            obj.Is_Repair = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Repair")) > 0, True, False)

            obj.Insurance = clsCommon.myCstr(dt.Rows(0)("Insurance"))
            obj.Packing_Forward = clsCommon.myCstr(dt.Rows(0)("Packing_Forward"))
            obj.Retention = clsCommon.myCdbl(dt.Rows(0)("Retention"))
            obj.Freight = clsCommon.myCstr(dt.Rows(0)("Freight"))
            obj.RefTendorNo = clsCommon.myCstr(dt.Rows(0)("RefTendorNo"))
            obj.Against_WorkEstimation_Id = clsCommon.myCstr(dt.Rows(0)("Against_WorkEstimation_Id"))
            obj.Form_ID = clsCommon.myCstr(dt.Rows(0)("From_Screen_Code"))
            '' End function


            '======end here========='
            ''-------------------------
            obj.objPIRemittance = clsPIRemittance.GetData(obj.PurchaseOrder_No, trans)

            qry = "SELECT TSPL_PURCHASE_ORDER_DETAIL.*,TSPL_LOCATION_MASTER.Location_Desc as LocationName,(case when len(isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,''))>0 then (select MAX(Requisition_Qty) from TSPL_REQUISITION_DETAIL where Requisition_Id=TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id and Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code)  else 0 end) as OriginalReqQty,(select sum(1) from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_Id=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No and TSPL_GRN_DETAIL.Item_Code=TSPL_PURCHASE_ORDER_DETAIL.Item_Code) as IsUsedInGRN FROM TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PURCHASE_ORDER_DETAIL.Location where TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No='" + obj.PurchaseOrder_No + "' ORDER BY TSPL_PURCHASE_ORDER_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsPurchaseOrderDetail)
                Dim objTr As clsPurchaseOrderDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsPurchaseOrderDetail

                    objTr.Insurance_Base_Amt = clsCommon.myCdbl(dr("Insurance_Base_Amt"))
                    objTr.Insurance_Per = clsCommon.myCdbl(dr("Insurance_Per"))


                    objTr.Last_Same_Vendor_Rate = clsCommon.myCdbl(dr("Last_Same_Vendor_Rate"))
                    objTr.Last_Other_Vendor_Rate = clsCommon.myCdbl(dr("Last_Other_Vendor_Rate"))
                    objTr.PurchaseOrder_No = clsCommon.myCstr(dr("PurchaseOrder_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Line_No = Convert.ToInt32(clsCommon.myCdbl(dr("Line_No")))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.PurchaseOrder_Qty = clsCommon.myCdbl(dr("PurchaseOrder_Qty"))
                    objTr.Requisition_Id = clsCommon.myCstr(dr("Requisition_Id"))
                    objTr.OriginalReqQty = clsCommon.myCdbl(dr("OriginalReqQty"))
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

                    objTr.Header_Discount_Per = clsCommon.myCdbl(dr("Header_Discount_Per"))
                    objTr.Header_Discount_Amount = clsCommon.myCdbl(dr("Header_Discount_Amount"))
                    objTr.Disc_Per = clsCommon.myCdbl(dr("Disc_Per"))
                    objTr.Detail_Discount_Amount = clsCommon.myCdbl(dr("Detail_Discount_Amount"))

                    objTr.Disc_Per_Unit = clsCommon.myCdbl(dr("Disc_Per_Unit"))
                    objTr.Disc_Amt_Per_Unit = clsCommon.myCdbl(dr("Disc_Amt_Per_Unit"))

                    objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))

                    objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))

                    objTr.Item_Insurance_Base_Amt = clsCommon.myCdbl(dr("Item_Insurance_Base_Amt"))
                    objTr.Item_Insurance_Apply_On = clsCommon.myCstr(dr("Item_Insurance_Apply_On"))
                    objTr.Item_Insurance_Rate = clsCommon.myCdbl(dr("Item_Insurance_Rate"))
                    objTr.Item_Insurance_Amt = clsCommon.myCdbl(dr("Item_Insurance_Amt"))
                    objTr.Item_Amt_After_Insurance = clsCommon.myCdbl(dr("Item_Amt_After_Insurance"))


                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))

                    objTr.Taxable_Amount_Per = clsCommon.myCdbl(dr("Taxable_Amount_Per"))
                    objTr.Taxable_Amount = clsCommon.myCdbl(dr("Taxable_Amount"))


                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    '====Sanjeet(22/12/2016)==========
                    objTr.Capacity = clsCommon.myCstr(dr("Capacity"))
                    objTr.Make = clsCommon.myCstr(dr("Make"))
                    objTr.Model = clsCommon.myCstr(dr("Model"))
                    '=======================
                    objTr.IsUsedInGRN = clsCommon.myCBool(dr("IsUsedInGRN"))
                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Assessable = clsCommon.myCdbl(dr("Assessable"))
                    objTr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))

                    objTr.AbatementRate = clsCommon.myCdbl(dr("AbatementRate"))
                    objTr.AssessableMRP = clsCommon.myCdbl(dr("AssessableMRP"))
                    objTr.TotalAssessableMRP = clsCommon.myCdbl(dr("TotalAssessableMRP"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))

                    objTr.Qty_Desc = clsCommon.myCstr(dr("Qty_Desc"))
                    objTr.Rate_Desc = clsCommon.myCstr(dr("Rate_Desc"))
                    objTr.Amount_Desc = clsCommon.myCdbl(dr("Amount_Desc"))
                    ''richa agarwal 07/04/2015
                    objTr.FatPer_MT = clsCommon.myCdbl(dr("FatPer_MT"))
                    objTr.SNFPer_MT = clsCommon.myCdbl(dr("SNFPer_MT"))
                    objTr.FatKG_MT = clsCommon.myCdbl(dr("FatKG_MT"))
                    objTr.SNFKG_MT = clsCommon.myCdbl(dr("SNFKG_MT"))
                    objTr.Item_Weight_MT = clsCommon.myCdbl(dr("Item_Weight_MT"))
                    objTr.Weight_UOM_MT = clsCommon.myCstr(dr("Weight_UOM_MT"))
                    ''------------------------


                    ''------------------19/10/2016=======================
                    objTr.ItemAdd_Charge_Code1 = clsCommon.myCstr(dr("ItemAdd_Charge_Code1"))
                    objTr.ItemAdd_Charge_Code2 = clsCommon.myCstr(dr("ItemAdd_Charge_Code2"))
                    objTr.ItemAdd_Charge_Code3 = clsCommon.myCstr(dr("ItemAdd_Charge_Code3"))
                    objTr.ItemAdd_Charge_Code4 = clsCommon.myCstr(dr("ItemAdd_Charge_Code4"))
                    objTr.ItemAdd_Charge_Code5 = clsCommon.myCstr(dr("ItemAdd_Charge_Code5"))
                    objTr.ItemAdd_Charge_Code6 = clsCommon.myCstr(dr("ItemAdd_Charge_Code6"))
                    objTr.ItemAdd_Charge_Code7 = clsCommon.myCstr(dr("ItemAdd_Charge_Code7"))
                    objTr.ItemAdd_Charge_Code8 = clsCommon.myCstr(dr("ItemAdd_Charge_Code8"))
                    objTr.ItemAdd_Charge_Code9 = clsCommon.myCstr(dr("ItemAdd_Charge_Code9"))
                    objTr.ItemAdd_Charge_Code10 = clsCommon.myCstr(dr("ItemAdd_Charge_Code10"))
                    objTr.ItemAdd_Org_Charge_Amt1 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt1"))
                    objTr.ItemAdd_Org_Charge_Amt2 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt2"))
                    objTr.ItemAdd_Org_Charge_Amt3 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt3"))
                    objTr.ItemAdd_Org_Charge_Amt4 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt4"))
                    objTr.ItemAdd_Org_Charge_Amt5 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt5"))
                    objTr.ItemAdd_Org_Charge_Amt6 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt6"))
                    objTr.ItemAdd_Org_Charge_Amt7 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt7"))
                    objTr.ItemAdd_Org_Charge_Amt8 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt8"))
                    objTr.ItemAdd_Org_Charge_Amt9 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt9"))
                    objTr.ItemAdd_Org_Charge_Amt10 = clsCommon.myCdbl(dr("ItemAdd_Org_Charge_Amt10"))
                    objTr.ItemAdd_Calc_Charge_Amt1 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt1"))
                    objTr.ItemAdd_Calc_Charge_Amt2 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt2"))
                    objTr.ItemAdd_Calc_Charge_Amt3 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt3"))
                    objTr.ItemAdd_Calc_Charge_Amt4 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt4"))
                    objTr.ItemAdd_Calc_Charge_Amt5 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt5"))
                    objTr.ItemAdd_Calc_Charge_Amt6 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt6"))
                    objTr.ItemAdd_Calc_Charge_Amt7 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt7"))
                    objTr.ItemAdd_Calc_Charge_Amt8 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt8"))
                    objTr.ItemAdd_Calc_Charge_Amt9 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt9"))
                    objTr.ItemAdd_Calc_Charge_Amt10 = clsCommon.myCdbl(dr("ItemAdd_Calc_Charge_Amt10"))
                    objTr.Total_ItemAdd_Charge = clsCommon.myCdbl(dr("Total_ItemAdd_Charge"))
                    ''=======================================================
                    objTr.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(dr("Against_Item_Wise_Tax_Rate"))
                    obj.Arr.Add(objTr)
                Next
            End If

            '----------------------roadpermit detail---------------------------------------------
            obj.Arr_Road = New List(Of clsPurchaseOrderRoadDetail)
            obj.Arr_CFORM = New List(Of clsPurchaseOrderCFORMDetail)
            If obj.roadpermit = "1" Then
                qry = "select TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL.form_code,TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL.issue_no,TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL.srn_no from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL.purchaseorder_NO='" + obj.PurchaseOrder_No + "' and TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL.vendor_code='" + obj.Vendor_Code + "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    Dim objtr1 As New clsPurchaseOrderRoadDetail()
                    For Each dr As DataRow In dt.Rows
                        objtr1 = New clsPurchaseOrderRoadDetail()
                        objtr1.roadpono = clsCommon.myCstr(obj.PurchaseOrder_No)
                        objtr1.roadvendor = clsCommon.myCstr(obj.Vendor_Code)
                        objtr1.roadcode = clsCommon.myCstr(dr("form_code"))
                        objtr1.roadissue_no = clsCommon.myCstr(dr("issue_no"))
                        objtr1.RoadpermitSRNNO = clsCommon.myCstr(dr("srn_no"))

                        obj.Arr_Road.Add(objtr1)
                    Next
                End If
            End If

            If obj.Cform = "1" Then
                qry = "select TSPL_CFORM_ISSUE_RECEIVE_DETAIL.form_code,TSPL_CFORM_ISSUE_RECEIVE_DETAIL.issue_no,TSPL_CFORM_ISSUE_RECEIVE_DETAIL.srn_no from TSPL_CFORM_ISSUE_RECEIVE_DETAIL where TSPL_CFORM_ISSUE_RECEIVE_DETAIL.purchaseorder_no='" + obj.PurchaseOrder_No + "' and TSPL_CFORM_ISSUE_RECEIVE_DETAIL.vendor_code='" + obj.Vendor_Code + "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    Dim objtr1 As New clsPurchaseOrderCFORMDetail()
                    For Each dr As DataRow In dt.Rows
                        objtr1 = New clsPurchaseOrderCFORMDetail()
                        objtr1.cformpono = clsCommon.myCstr(obj.PurchaseOrder_No)
                        objtr1.cformvendor = clsCommon.myCstr(obj.Vendor_Code)
                        objtr1.cformcode = clsCommon.myCstr(dr("form_code"))
                        objtr1.cformissue_no = clsCommon.myCstr(dr("issue_no"))
                        objtr1.cformSRNNO = clsCommon.myCstr(dr("srn_no"))

                        obj.Arr_CFORM.Add(objtr1)
                    Next
                End If
            End If
            '---------------------------------------------------------------------------------------
            ''==== added by Parteek 19/09/2017
            obj.Arr_FieldCategory = New List(Of clsPurchaseOrderRoadDetail)
            obj.Arr_Terms_C = New List(Of clsPurchaseOrderRoadDetail)

            qry = "select TSPL_PURCHASE_ORDER_WORK_ORDER.Field_Name,TSPL_PURCHASE_ORDER_WORK_ORDER.Description from TSPL_PURCHASE_ORDER_WORK_ORDER where TSPL_PURCHASE_ORDER_WORK_ORDER.purchaseorder_NO='" + obj.PurchaseOrder_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim objtr1 As New clsPurchaseOrderRoadDetail()
                For Each dr As DataRow In dt.Rows
                    objtr1 = New clsPurchaseOrderRoadDetail()
                    objtr1.FieldName = clsCommon.myCstr(dr("Field_Name"))
                    objtr1.FieldDesc = clsCommon.myCstr(dr("Description"))

                    obj.Arr_FieldCategory.Add(objtr1)
                Next
            End If

            qry = "select Terms_Condition from TSPL_PURCHASE_ORDER_WORK_ORDER_TERMS where purchaseorder_NO='" + obj.PurchaseOrder_No + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Dim objtr2 As New clsPurchaseOrderRoadDetail()
                For Each dr As DataRow In dt.Rows
                    objtr2 = New clsPurchaseOrderRoadDetail()
                    objtr2.Terms_C = clsCommon.myCstr(dr("Terms_Condition"))

                    obj.Arr_Terms_C.Add(objtr2)
                Next
            End If
            obj.Arr_ACInsurance = clsPurchaseOrderAdditionChargeInsurance.GetData(obj.PurchaseOrder_No, trans)
            obj.ArrSchedule = clsTenderSchedulePO.GetData(obj.PurchaseOrder_No, trans)
        End If

        Return obj
    End Function

    'stuti--------------
    Public Shared Function CheckPOUsedInSRNorGRN(ByVal strPONo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select sum(fin.[cnt]) from (SELECT 1 as [cnt] from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.PO_ID ='" + clsCommon.myCstr(strPONo) + "' union all SELECT 1 as [cnt] from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_ID ='" + clsCommon.myCstr(strPONo) + "')fin"
        Dim count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    '======end here=======

    Public Shared Function IsValidProjectForPO(ByVal strPONo As String, ByVal strProject As String) As String
        Dim strProj As String = clsDBFuncationality.getSingleValue("select PROJECT_ID from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" + strPONo + "'")
        Return strProj
    End Function
    Public Shared Function GetLocationForPO(ByVal strPONo As String) As String
        Dim strLoc As String = clsDBFuncationality.getSingleValue("select Bill_To_Location from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" + strPONo + "'")
        Return strLoc
    End Function

    Public Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, Optional ByVal Schedule_ON As Boolean = False, Optional ByVal arrLoc As String = "", Optional ByVal isamendment As Boolean = False) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin() 'BM00000008148
        Try
            PostData(FormId, strDocNo, arrLoc, isCheckForPosted, Schedule_ON, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function


    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsPurchaseOrderDetail)) As String
        For Each objtr As clsPurchaseOrderDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function
    Public Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal arrLoc As String, ByVal isCheckForPosted As Boolean, Optional ByVal Schedule_ON As Boolean = False, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal strAPInvNo As String = Nothing, Optional ByVal strAPInvVoucherNo As String = Nothing) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Purchase Order No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsPurchaseOrderHead = clsPurchaseOrderHead.GetData(strDocNo, NavigatorType.Current, arrLoc, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.PurchaseOrder_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Order", obj.Bill_To_Location, obj.PurchaseOrder_Date, trans)

            If (isCheckForPosted AndAlso obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (isCheckForPosted AndAlso obj.On_Hold) Then
                Throw New Exception("Purchase order No " + obj.PurchaseOrder_No + " Is On Hold.Can't Post it")
            End If

            If obj.isApproved = 0 Then
                Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_PURCHASE_ORDER_HEAD", "PurchaseOrder_No", strDocNo, trans)
                If isResult = False Then
                    trans.Commit()
                    Return False
                End If
            End If

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                If clsCommon.CompairString(obj.Item_Type, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal Then
                Else
                    For Each objTr As clsPurchaseOrderDetail In obj.Arr
                        If clsCommon.myLen(objTr.Requisition_Id) > 0 Then
                            Dim qry1 As String = "update TSPL_REQUISITION_DETAIL set Balance_Qty=Balance_Qty - " + clsCommon.myCstr(objTr.PurchaseOrder_Qty) + " where Requisition_Id='" + objTr.Requisition_Id + "' and Item_Code='" + objTr.Item_Code + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                        End If
                    Next
                End If
            Else
                For Each objTr As clsPurchaseOrderDetail In obj.Arr
                    If clsCommon.myLen(objTr.Requisition_Id) > 0 Then
                        Dim qry1 As String = "update TSPL_REQUISITION_DETAIL set Balance_Qty=Balance_Qty - " + clsCommon.myCstr(objTr.PurchaseOrder_Qty) + " where Requisition_Id='" + objTr.Requisition_Id + "' and Item_Code='" + objTr.Item_Code + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                    End If
                Next
            End If




            Dim qry As String = "Update TSPL_PURCHASE_ORDER_HEAD set Status=1, Posting_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where PurchaseOrder_No='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_PURCHASE_ORDER_HEAD", "PurchaseOrder_No", trans)

            '' Anubhooti 03-Nov-2014 BM00000003888
            Dim Count As Double = 0
            If clsCommon.myLen(obj.Expiry_Date) > 0 Then
                Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) As Row From TSPL_EXPIRY_DATE WHERE Document_No='" & strDocNo & "' AND Program_Code='PO-ODR'", trans))
                If Count > 0 Then
                    Dim QryExpDate As String = "Update TSPL_EXPIRY_DATE set  Doc_Date='" & clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy hh:mm tt") & "',Expiry_Date='" + clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy") + "',Modified_By='" + objCommonVar.CurrentUserCode + "',Modified_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") & "' where Program_Code='PO-ODR' AND Document_No='" + strDocNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(QryExpDate, trans)
                Else
                    Dim QryNew As String = clsDBFuncationality.ExecuteNonQuery("Insert into TSPL_EXPIRY_DATE (Modified_Date,Modified_By,Screen_Name,Created_By,Document_No,Comp_Code,Doc_Date,Expiry_Date,Program_Code,Created_Date,New_Expiry_Date) Values ('" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") & "','" & objCommonVar.CurrentUserCode & "','Purchase Order','" & objCommonVar.CurrentUserCode & "','" + strDocNo + "','" + objCommonVar.CurrentCompanyCode + "','" & clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy hh:mm tt") & "','" & clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy") & "','PO-ODR','" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy") & "','" & clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy") & "')", trans)
                End If
            End If

            ''
            '''' fOR DEMO start here by prit on 28/08/2014
            Dim strPrincipalVendor = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.Principal_Vendor + "' and code='" + clsFixedParameterCode.Principal_Vendor + "'", trans))
            Dim strDatabase = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.Principal_Vendor_Database + "' and code='" + clsFixedParameterCode.Principal_Vendor_Database + "'", trans))
            If clsCommon.myLen(strPrincipalVendor) > 0 AndAlso clsCommon.myLen(strDatabase) > 0 AndAlso clsCommon.CompairString(strPrincipalVendor, obj.Vendor_Code) = CompairStringResult.Equal Then
                If CreateSaleOrder(False, obj, trans, strDatabase) = False Then
                    Return False
                End If
            End If
            ''''  end here
            ''richa agarwal 20/04/2015 BM00000006251
            If clsCommon.myLen(obj.MT_PI_No) > 0 AndAlso clsCommon.CompairString(obj.MT_Is_Merchant_Trade, "1") = CompairStringResult.Equal Then
                qry = "update  TSPL_EX_PI_HEAD  set MT_Against_PO ='" & obj.PurchaseOrder_No & "',MT_Against_PO_Date ='" & clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy hh:mm tt") & "' where Document_Code ='" & obj.MT_PI_No & "' and Document_Type ='MT'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            ''-------------------------------




            '===================create suto schedule is schedule setting is On=========================
            If Schedule_ON AndAlso clsCommon.CompairString(obj.Item_Type, "N") <> CompairStringResult.Equal Then
                If clsCommon.MyMessageBoxShow("Do you want to create auto purchase schedule?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes Then
a:
                    Dim frm As New FrmCheckBoxGrid()
                    frm.IsForDate = False
                    frm.qry = "select 'Monthly Schedule' as Value union all select 'Weekly Schedule' as Value union all select 'Daily Schedule' as Value"
                    frm.trans = trans
                    frm.ShowDialog()

                    If frm.arrValue IsNot Nothing AndAlso frm.arrValue.Count > 0 Then
                        If frm.arrValue.Count > 1 Then
                            clsCommon.MyMessageBoxShow("Select any one schedule type.")
                            GoTo a
                        End If
                        Dim Sch_Type As String = ""
                        For Each Str As String In frm.arrValue
                            Sch_Type = Str
                        Next

                        If clsCommon.CompairString(Sch_Type, "Monthly Schedule") = CompairStringResult.Equal Then
                            Sch_Type = "M"
                        ElseIf clsCommon.CompairString(Sch_Type, "Weekly Schedule") = CompairStringResult.Equal Then
                            Sch_Type = "W"
                        ElseIf clsCommon.CompairString(Sch_Type, "Daily Schedule") = CompairStringResult.Equal Then
                            Sch_Type = "D"
                        End If

                        Dim frm1 As New FrmCheckBoxGrid()
                        frm1.IsForDate = True
                        frm1.DateType_Daily_Monthly_Weekly = Sch_Type
                        frm1.trans = trans
                        frm1.ShowDialog()

                        Dim sch_date As Date? = Nothing

                        If frm1.arrValue IsNot Nothing AndAlso frm1.arrValue.Count > 0 Then
                            sch_date = clsCommon.myCDate(frm1.arrValue(0))

                            SaveScheduleData(obj, Sch_Type, sch_date, trans)
                            If clsCommon.myLen(obj.strScheduleNo) > 0 Then
                                clsCommon.MyMessageBoxShow("Schedule No " & obj.strScheduleNo & " generated successfully.")
                            End If
                        End If

                    End If
                End If
            End If
            obj.SendMailForAdvancePaymenTerms = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.MailForAdvancePaymentTerm & "'", trans)) = 0, False, True)
            If obj.SendMailForAdvancePaymenTerms = True Then
                Dim qryTerms As String = clsDBFuncationality.getSingleValue("select TSPL_TERMS_MASTER.isAdvance from TSPL_TERMS_MASTER left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.Terms_Code=TSPL_TERMS_MASTER.Terms_Code where TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" & obj.PurchaseOrder_No & "'", trans)
                If clsCommon.CompairString(qryTerms, "1") = CompairStringResult.Equal Then
                    Dim AdvancePayment As Decimal = 0
                    Dim advancePersent As Integer = clsDBFuncationality.getSingleValue("select TSPL_TERMS_MASTER.Advance_per from TSPL_TERMS_MASTER left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.Terms_Code=TSPL_TERMS_MASTER.Terms_Code where TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" & obj.PurchaseOrder_No & "'", trans)
                    AdvancePayment = obj.PO_Total_Amt * advancePersent / 100
                    CreateEmailContent(obj, AdvancePayment, trans)
                End If
            End If
            CreateTransactionEmailContent(obj, trans)
            ' CreateTransactionSMSContent(obj, trans)
            '=========================end here========================================================================

            '-----------------Added Parteek 23/03/2016
            If clsCommon.CompairString(obj.PurchaseOrder_Type, "S") = CompairStringResult.Equal Then

                '----------Ap invoice Entry functionality
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                objVendorInvHead.Document_No = strAPInvNo
                objVendorInvHead.Invoice_Entry_Date = clsCommon.myCDate(obj.PurchaseOrder_Date, "dd/MM/yyyy")
                objVendorInvHead.Vendor_Code = obj.Vendor_Code
                objVendorInvHead.Vendor_Name = obj.Vendor_Name
                objVendorInvHead.Vendor_Invoice_No = obj.PurchaseOrder_No
                objVendorInvHead.Invoice_Type = "AP"
                If obj.Invdate Is Nothing Then
                    objVendorInvHead.Vendor_Invoice_Date = obj.PurchaseOrder_Date
                Else
                    objVendorInvHead.Vendor_Invoice_Date = obj.Invdate
                End If


                objVendorInvHead.loc_code = obj.Bill_To_Location
                objVendorInvHead.PROJECT_ID = obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
                End If

                objVendorInvHead.Document_Type = "P" ''For Purchase Invoice Type


                objVendorInvHead.Total_Tax = obj.Total_Tax_Amt

                objVendorInvHead.On_Hold = False
                Dim srndate As String
                If clsCommon.myLen(obj.Against_Requisition) > 0 Then
                    Dim query As String = "select Requisition_Date  from TSPL_REQUISITION_HEAD where Requisition_id ='" + obj.Against_Requisition + "' "
                    srndate = clsCommon.myCDate(CStr(clsDBFuncationality.getSingleValue(query, trans)), "dd/MM/yyyy")

                Else
                    srndate = obj.PurchaseOrder_Date
                End If


                objVendorInvHead.Description = "Vendor " + obj.Vendor_Code + "/" + obj.Vendor_Name + " .Against PO No " + obj.PurchaseOrder_No + "-" + obj.Against_Requisition + "-" + srndate
                objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
                objVendorInvHead.Tax_Group = obj.Tax_Group
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    objVendorInvHead.TAX1 = obj.TAX1
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX1, trans) Then
                        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
                    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    objVendorInvHead.TAX2 = obj.TAX2
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX2, trans) Then
                        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
                    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    objVendorInvHead.TAX3 = obj.TAX3
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX3, trans) Then
                        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
                    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    objVendorInvHead.TAX4 = obj.TAX4
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX4, trans) Then
                        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
                    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    objVendorInvHead.TAX5 = obj.TAX5
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX5, trans) Then
                        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.Bill_To_Location, trans)

                    End If
                    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
                    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    objVendorInvHead.TAX6 = obj.TAX6
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX6, trans) Then
                        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
                    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    objVendorInvHead.TAX7 = obj.TAX7
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX7, trans) Then
                        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.Bill_To_Location, trans)

                    End If
                    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
                    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    objVendorInvHead.TAX8 = obj.TAX8
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX8, trans) Then
                        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
                    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    objVendorInvHead.TAX9 = obj.TAX9
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX9, trans) Then
                        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
                    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    objVendorInvHead.TAX10 = obj.TAX10
                    If clsTaxMaster.ISTaxRecoverableAC(obj.TAX10, trans) Then
                        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.Bill_To_Location, trans)
                    End If
                    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
                    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
                End If

                objVendorInvHead.Terms_Code = obj.Terms_Code
                objVendorInvHead.Terms_Description = obj.TermsName
                objVendorInvHead.Due_Date = obj.Due_Date
                objVendorInvHead.Discount_Base = obj.Discount_Base
                objVendorInvHead.Discount_Amount = obj.Discount_Amt
                objVendorInvHead.Amount_Less_Discount = obj.Amount_Less_Discount
                objVendorInvHead.Document_Total = obj.PO_Total_Amt
                objVendorInvHead.Balance_Amt = obj.PO_Total_Amt
                objVendorInvHead.Against_POInvoice_No = ""
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, obj.Bill_To_Location, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, obj.Bill_To_Location, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If

                'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge
                ''===============21/12/2016 Parteek Excel Format point=====
                objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
                objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
                objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

                objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
                objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
                objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

                objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
                objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
                objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

                objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
                objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
                objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

                objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
                objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
                objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

                objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
                objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
                objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

                objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
                objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
                objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

                objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
                objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
                objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

                objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
                objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
                objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

                objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
                objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
                objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10

                objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge
                ''===================End=================================

                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                Dim ii As Integer = 0
                Dim isFirstTime As Boolean = True
                Dim strFirstItemCode As String = GetFirstItemCode(obj.Arr)
                'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
                objVendorInvHead.Total_Landed_Amt = 0
                For Each objPIDetail As clsPurchaseOrderDetail In obj.Arr
                    Dim strICode As String = objPIDetail.Item_Code
                    If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        strICode = strFirstItemCode
                    End If

                    ''Fill VendorInvoice details Data
                    qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                    End If

                    Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
                    Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))


                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                        objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.Bill_To_Location, trans)
                    End If



                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                    ii = ii + 1
                    objVendorInvDetail.Detail_Line_No = ii
                    objVendorInvDetail.GL_Account_Code = strPaybleCleanigCtrlAC
                    objVendorInvDetail.GL_Account_Desc = strPaybleCleanigCtrlACName
                    objVendorInvDetail.Amount = objPIDetail.Amount
                    objVendorInvDetail.Discount_Per = objPIDetail.Disc_Per
                    objVendorInvDetail.Discount = objPIDetail.Disc_Amt
                    objVendorInvDetail.Amount_less_Discount = objPIDetail.Amt_Less_Discount
                    objVendorInvDetail.TAX1 = objPIDetail.TAX1
                    objVendorInvDetail.TAX1_Rate = objPIDetail.TAX1_Rate
                    objVendorInvDetail.TAX1_Amt = objPIDetail.TAX1_Amt
                    objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt
                    objVendorInvDetail.TAX2 = objPIDetail.TAX2
                    objVendorInvDetail.TAX2_Rate = objPIDetail.TAX2_Rate
                    objVendorInvDetail.TAX2_Amt = objPIDetail.TAX2_Amt
                    objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt
                    objVendorInvDetail.TAX3 = objPIDetail.TAX3
                    objVendorInvDetail.TAX3_Rate = objPIDetail.TAX3_Rate
                    objVendorInvDetail.TAX3_Amt = objPIDetail.TAX3_Amt
                    objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt
                    objVendorInvDetail.TAX4 = objPIDetail.TAX4
                    objVendorInvDetail.TAX4_Rate = objPIDetail.TAX4_Rate
                    objVendorInvDetail.TAX4_Amt = objPIDetail.TAX4_Amt
                    objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt
                    objVendorInvDetail.TAX5 = objPIDetail.TAX5
                    objVendorInvDetail.TAX5_Rate = objPIDetail.TAX5_Rate
                    objVendorInvDetail.TAX5_Amt = objPIDetail.TAX5_Amt
                    objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt
                    objVendorInvDetail.TAX6 = objPIDetail.TAX6
                    objVendorInvDetail.TAX6_Rate = objPIDetail.TAX6_Rate
                    objVendorInvDetail.TAX6_Amt = objPIDetail.TAX6_Amt
                    objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt
                    objVendorInvDetail.TAX7 = objPIDetail.TAX7
                    objVendorInvDetail.TAX7_Rate = objPIDetail.TAX7_Rate
                    objVendorInvDetail.TAX7_Amt = objPIDetail.TAX7_Amt
                    objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt
                    objVendorInvDetail.TAX8 = objPIDetail.TAX8
                    objVendorInvDetail.TAX8_Rate = objPIDetail.TAX8_Rate
                    objVendorInvDetail.TAX8_Amt = objPIDetail.TAX8_Amt
                    objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt
                    objVendorInvDetail.TAX9 = objPIDetail.TAX9
                    objVendorInvDetail.TAX9_Rate = objPIDetail.TAX9_Rate
                    objVendorInvDetail.TAX9_Amt = objPIDetail.TAX9_Amt
                    objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt
                    objVendorInvDetail.TAX10 = objPIDetail.TAX10
                    objVendorInvDetail.TAX10_Rate = objPIDetail.TAX10_Rate
                    objVendorInvDetail.TAX10_Amt = objPIDetail.TAX10_Amt
                    objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt
                    objVendorInvDetail.Total_Tax = objPIDetail.Total_Tax_Amt
                    objVendorInvDetail.Total_Amount = objPIDetail.Item_Net_Amt
                    objVendorInvDetail.Landed_Amount = objPIDetail.Item_Net_Amt - objPIDetail.Amt_Less_Discount
                    If Not obj.Is_Shortage_Include_In_Landed_Cost Then
                        objVendorInvDetail.Landed_Amount += objPIDetail.Shortage_Amount
                    End If

                    objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

                    If objPIDetail.Disc_Type = 1 Then
                        objVendorInvHead.Discount_Amount -= objPIDetail.Disc_Amt
                        objVendorInvHead.Amount_Less_Discount += objPIDetail.Disc_Amt
                        objVendorInvHead.Document_Total += objPIDetail.Disc_Amt
                    End If


                    objVendorInvDetail.TAX1_Base_Amt = objPIDetail.TAX1_Base_Amt
                    objVendorInvDetail.TAX2_Base_Amt = objPIDetail.TAX2_Base_Amt
                    objVendorInvDetail.TAX3_Base_Amt = objPIDetail.TAX3_Base_Amt
                    objVendorInvDetail.TAX4_Base_Amt = objPIDetail.TAX4_Base_Amt
                    objVendorInvDetail.TAX5_Base_Amt = objPIDetail.TAX5_Base_Amt
                    objVendorInvDetail.TAX6_Base_Amt = objPIDetail.TAX6_Base_Amt
                    objVendorInvDetail.TAX7_Base_Amt = objPIDetail.TAX7_Base_Amt
                    objVendorInvDetail.TAX8_Base_Amt = objPIDetail.TAX8_Base_Amt
                    objVendorInvDetail.TAX9_Base_Amt = objPIDetail.TAX9_Base_Amt
                    objVendorInvDetail.TAX10_Base_Amt = objPIDetail.TAX10_Base_Amt

                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                    End If
                    ''End of Fill Vendor Invoice Detail Data
                Next

                objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If


                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                ''multicurrency
                objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
                objVendorInvHead.ConvRate = obj.ConvRate
                objVendorInvHead.ApplicableFrom = obj.ApplicableFrom
                ''end multicurrency
                Dim Issaved As Boolean = True
                objVendorInvHead.SaveData(objVendorInvHead, True, trans, strAPInvVoucherNo)
                If Issaved = True Then
                    clsCommon.MyMessageBoxShow("AP Invoice is created " + objVendorInvHead.Document_No + " throuth Purchase Order No : " & obj.PurchaseOrder_No & "")
                End If



                '--------- End
            End If
            '--------- End
            CreateNotificationContentEMP(obj.PurchaseOrder_No, trans)

            If objCommonVar.InternalSMSEmailinPurchaseModule = True Then
                CreateInternalEmailSMS(obj, trans)
            End If

            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Sub CreateInternalEmailSMS(ByVal obj As clsPurchaseOrderHead, ByVal trans As SqlTransaction)
        Dim itemName As String = ""
        Dim UOM As String = ""
        Dim qty As String = ""
        Dim ItemDetail As String = ""
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseOrder + "2" + "'", trans)

        Dim qry As String = "select TSPL_USER_MASTER.User_Code from TSPL_USER_MASTER "

        If clsCommon.myLen(obj.Against_Requisition) > 0 Then
            qry += " left join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Created_By=TSPL_USER_MASTER.User_Code left join TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.Against_Requisition=TSPL_REQUISITION_HEAD.Requisition_Id "
        Else
            qry += " left join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.Created_By=TSPL_USER_MASTER.User_Code  "
        End If
        qry += " where TSPL_PURCHASE_ORDER_HEAD.PURCHASEORDER_no='" + obj.PurchaseOrder_No + "'"
        Dim StrUserCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Dim arrMobileNo As New List(Of String)
        Dim arrMailID As List(Of String) = clsERPFuncationality.ReportingMailIdandPhone(StrUserCode, arrMobileNo, trans)

        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 AndAlso ((arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Or (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0)) Then

            'Dim qry1 As String = "select TSPL_PURCHASE_ORDER_DETAIL.Unit_code,CAST(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty AS decimal(18,2)) as Qty,isnull(TSPL_PURCHASE_ORDER_DETAIL.item_desc,'') as item_desc "
            'qry1 += "  from TSPL_PURCHASE_ORDER_DETAIL"
            'qry1 += "  where TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No='" & obj.PurchaseOrder_No & "' ORDER BY TSPL_PURCHASE_ORDER_DETAIL.Line_No"
            'Dim dtDocWise As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)

            'For ii As Integer = 0 To dtDocWise.Rows.Count - 1
            '    itemName = clsCommon.myCstr(dtDocWise.Rows(ii)("item_desc"))
            '    UOM = clsCommon.myCstr(dtDocWise.Rows(ii)("Unit_Code"))
            '    qty = clsCommon.myCstr(dtDocWise.Rows(ii)("Qty"))

            '    ItemDetail += itemName + " " + UOM + "-" + qty + ","
            'Next

            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                For Each objdetail As clsPurchaseOrderDetail In obj.Arr
                    itemName = clsCommon.myCstr(objdetail.Item_Desc)
                    UOM = clsCommon.myCstr(objdetail.Unit_code)
                    qty = clsCommon.myCstr(objdetail.PurchaseOrder_Qty)
                    ItemDetail += itemName + " " + UOM + "-" + qty + Environment.NewLine
                Next
            End If

            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso (arrMailID IsNot Nothing AndAlso arrMailID.Count > 0) Then
                Dim objEmailH As New clsEMailHead()
                objEmailH.arrEMail = New List(Of String)()
                objEmailH.arrEMail = arrMailID

                objEmailH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                objEmailH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))

                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.PurchaseOrder_No)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy"))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.PO_Total_Amt))
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, obj.Vendor_Code)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, obj.Vendor_Name)
                objEmailH.Email_Text = objEmailH.Email_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                objEmailH.SaveData(clsUserMgtCode.mbtnPurchaseOrder, objEmailH, trans)
                objEmailH = Nothing

            End If

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 AndAlso (arrMobileNo IsNot Nothing AndAlso arrMobileNo.Count > 0) Then
                Dim objSMSH As New clsSMSHead()
                objSMSH.arrMobilNo = New List(Of String)()
                objSMSH.arrMobilNo = arrMobileNo

                objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.PurchaseOrder_No)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.PO_Total_Amt))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, obj.Vendor_Code)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, obj.Vendor_Name)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)

                objSMSH.SaveData(clsUserMgtCode.mbtnPurchaseOrder, objSMSH, trans)
                objSMSH = Nothing
            End If
        End If


    End Sub

    Sub CreateEmailContent(ByVal obj As clsPurchaseOrderHead, ByVal AdnavePayment As Decimal, ByVal trans As SqlTransaction)
        obj.SendMailForAdvancePaymenTerms = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.MailForAdvancePaymentTerm & "'", trans)) = 0, False, True)

        Dim Form_ID As String = clsUserMgtCode.mbtnPurchaseOrder
        Dim Emp_NAme As String = ""
        Dim ModifyBy As String = ""
        Dim AdvancePer As Decimal = 0
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "1" + "'", trans)
        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
            Dim qry As String = "select distinct TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_HEAD.PO_Total_Amt,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Name,TSPL_PURCHASE_ORDER_HEAD.Modify_By,TSPL_TERMS_MASTER.Advance_Per "
            qry += " from TSPL_PURCHASE_ORDER_HEAD"
            qry += " left outer join TSPL_TERMS_MASTER on TSPL_PURCHASE_ORDER_HEAD.Terms_Code =TSPL_TERMS_MASTER.Terms_Code "
            qry += " where 2=2  and PurchaseOrder_No='" + obj.PurchaseOrder_No + "' "

            Dim dtMailID As DataTable = clsDBFuncationality.GetDataTable("SELECT distinct TSPL_ES_CONTENT_EMP_DETAIL.Emp_Code,tspl_employee_master.EMail_ID as Email,tspl_employee_master.Emp_Name from TSPL_ES_CONTENT_EMP_DETAIL left outer join tspl_employee_master on tspl_employee_master.Emp_code=TSPL_ES_CONTENT_EMP_DETAIL.Emp_code where Form_ID='" + Form_ID + "1" + "'", trans)

            Dim dtParty As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtParty IsNot Nothing AndAlso dtParty.Rows.Count > 0 Then
                If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 AndAlso clsCommon.myLen(dtMailID.Rows(0)("Email")) > 0 Then
                    Dim objSMSH As New clsEMailHead()
                    objSMSH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                    objSMSH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))
                    AdvancePer = clsCommon.myCstr(dtParty.Rows(0)("Advance_Per"))
                    Emp_NAme = clsCommon.myCstr(dtMailID.Rows(0)("Emp_Name"))
                    ModifyBy = clsCommon.myCstr(dtParty.Rows(0)("Modify_By"))
                    objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.PurchaseOrder_No)
                    objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy"))
                    objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.PO_Total_Amt))
                    objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, obj.Vendor_Code)
                    objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, obj.Vendor_Name)
                    objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.AdvancePayment, AdnavePayment)
                    objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.AdvancePersentage, AdvancePer)
                    objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.MOdifyBY, ModifyBy)
                    objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.EmpName, Emp_NAme)
                    objSMSH.arrEMail = New List(Of String)()
                    objSMSH.arrEMail.Add(clsCommon.myCstr(dtMailID.Rows(0)("Email")))
                    objSMSH.SaveData(Form_ID, objSMSH, trans)
                    objSMSH = Nothing
                End If
            End If
        End If
    End Sub
    Private Shared Function CreateNotificationContentEMP(ByVal StrDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseOrder + "'", trans))
        Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseOrder + "'", trans))
        Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseOrder + "'", trans))
        Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseOrder + "'", trans))

        '' work to be done agiast ticket no. BHA/14/09/18-000550  date 18/09/2018 
        Dim qry As String = "select Delivery_date,convert(varchar,PurchaseOrder_Date,103) as PurchaseOrder_Date,PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.Modify_Date"
        qry += " ,TSPL_PURCHASE_ORDER_HEAD.Modify_By,TSPL_USER_MASTER.User_Code,TSPL_USER_MASTER.IP_Address ,TSPL_PURCHASE_ORDER_HEAD.Ref_No as DeliveryPlace "
        qry += " ,TSPL_VENDOR_MASTER.Vendor_Name from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PURCHASE_ORDER_HEAD.Vendor_Code LEFT OUTER JOIN TSPL_USER_MASTER ON TSPL_USER_MASTER.User_Code=TSPL_PURCHASE_ORDER_HEAD.Modify_By "
        qry += " where 2=2 "
        qry += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" + StrDocNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If clsCommon.myLen(strNotifiContent) > 0 Then
            Dim objNotification As New clsNotificationHead()
            objNotification.Notification_Text = strNotifiContent
            objNotification.Notification_Caption = strNotifiCaption
            objNotification.Notification_On = strNotificationOn
            objNotification.Notification_ConditionDate = clsCommon.myCDate(dt.Rows(0)("Delivery_date"))
            objNotification.Notification_Detail_Text = strNotifi_DetalContent
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_No, clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_No")))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Doc_Date, clsCommon.myCDate(dt.Rows(0)("PurchaseOrder_Date")))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Delivery_date, clsCommon.myCstr(dt.Rows(0)("Delivery_date")))
            objNotification.Notification_Text = objNotification.Notification_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(dt.Rows(0)("Vendor_Name")))
            objNotification.SaveData(clsUserMgtCode.mbtnPurchaseOrder, objNotification, trans)
            objNotification = Nothing
            Return True
        End If
        Return False
    End Function
    Sub CreateTransactionEmailContent(ByVal obj As clsPurchaseOrderHead, ByVal trans As SqlTransaction)
        obj.SendMailForAdvancePaymenTerms = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.MailForAdvancePaymentTerm & "'", trans)) = 0, False, True)
        Dim Form_ID As String = clsUserMgtCode.mbtnPurchaseOrder
        Dim objContent As clsESContent = clsESContent.GetData(Form_ID, trans)
        Dim strVEmailID As String = clsVendorMaster.GetVendorEmailID(obj.Vendor_Code, trans)
        If objContent IsNot Nothing AndAlso clsCommon.myLen(strVEmailID) > 0 Then
            Dim objSMSH As New clsEMailHead()
            objSMSH.Email_Subject = clsCommon.myCstr(objContent.EMail_Subject)
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_No, obj.PurchaseOrder_No)
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy"))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.PO_Total_Amt))
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Vendor_Code, obj.Vendor_Code)
            objSMSH.Email_Subject = objSMSH.Email_Subject.Replace(frmEMailAndSMSSetting.Vendor_Name, obj.Vendor_Name)

            objSMSH.Attachment_1_Path = PrintData(obj, trans, True)

            objSMSH.Email_Text = clsCommon.myCstr(objContent.EMail_Text)
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.PurchaseOrder_No)
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy"))
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.DocAmount, clsCommon.myFormat(obj.PO_Total_Amt))
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, obj.Vendor_Code)
            objSMSH.Email_Text = objSMSH.Email_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, obj.Vendor_Name)

            'Sanjay,for Sending Attachment file by mail
            Dim AttachmentCount As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_ATTACHMENTS where TransactionId='" + obj.PurchaseOrder_No + "'", trans)
            If AttachmentCount > 0 Then
                objSMSH.Against_PO_NO = obj.PurchaseOrder_No
            End If

            objSMSH.arrEMail = New List(Of String)()
            objSMSH.arrEMail.Add(strVEmailID)
            objSMSH.SaveData(Form_ID, objSMSH, trans)
            objSMSH = Nothing
            objContent = Nothing
            strVEmailID = Nothing
        End If
    End Sub

    Private Sub CreateTransactionSMSContent(ByVal obj As clsPurchaseOrderHead, ByVal trans As SqlTransaction)
        Dim Form_ID As String = clsUserMgtCode.mbtnPurchaseOrder
        Dim strContactPerson As String = ""
        Dim strotherno As String = Nothing
        strotherno = clsDBFuncationality.getSingleValue("select Phone1 from TSPL_VENDOR_MASTER where Vendor_Code ='" & obj.Vendor_Code & "' ", trans)
        strContactPerson = clsDBFuncationality.getSingleValue("select upper(substring(Contact_Person_Name,1,1)) + lower(substring(Contact_Person_Name,2,49)) from TSPL_VENDOR_MASTER where Vendor_Code ='" & obj.Vendor_Code & "' ", trans)
        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", trans)
        Dim objSMSH As New clsSMSHead()
        objSMSH.arrMobilNo = New List(Of String)()
        objSMSH.arrMobilNo.Add(clsCommon.myCstr(strotherno))
        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then

            If clsCommon.myLen(dtContent.Rows(0)("SMS_Text")) > 0 Then

                objSMSH.SMS_Text = clsCommon.myCstr(dtContent.Rows(0)("SMS_Text"))

                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_No, obj.PurchaseOrder_No)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Doc_Date, clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MMM/yyyy"))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, obj.Vendor_Code)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, obj.Vendor_Name)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.TotalAmount, clsCommon.myFormat(obj.PO_Total_Amt))
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.Form_Code, Form_ID)
                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(frmEMailAndSMSSetting.ContactPerson, strContactPerson)

                objSMSH.SaveData(Form_ID, objSMSH, trans)
                objSMSH = Nothing
                'If Not isPost Then
                '    clsCommon.MyMessageBoxShow("SMS Send Successfully", Me.Text)
                'End If
            End If
        End If
        'Sanjay
    End Sub


    Public Shared Function SaveScheduleData(ByVal objPur As clsPurchaseOrderHead, ByVal Sch_Type As String, ByVal Sch_Date As Date, ByVal Trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim obj As New clsPurchaseSchedule()
        Dim objtr As New clsPurchaseScheduleDetail()
        Dim objtr1 As New clsPurchaseScheduleVendorDetail()
        Dim stck_qty As Double = Nothing
        Try
            If objPur IsNot Nothing AndAlso clsCommon.myLen(objPur.PurchaseOrder_No) > 0 Then
                obj = New clsPurchaseSchedule()

                Dim TotalNo_Count_For_Sch_Qty As Integer = 0
                '===============================calculation================================================
                Dim days As Integer = 0
                Dim Week As Integer = 0
                Dim Visible_Counter As Integer = 0
                If clsCommon.CompairString(Sch_Type, "M") <> CompairStringResult.Equal Then
                    days = DateTime.DaysInMonth(CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(Sch_Date, "yyyy"))), CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(Sch_Date, "MM"))))
                    Dim date1 As String = clsCommon.myCstr(clsCommon.myCstr(days) + "/" + clsCommon.GetPrintDate(Sch_Date, "MMM") + "/" + clsCommon.GetPrintDate(Sch_Date, "yyyy"))
                    Week = clsPurchaseSchedule.GetNoOfWeekInMonth(Sch_Date, False, Trans)
                End If

                If clsCommon.CompairString(Sch_Type, "W") = CompairStringResult.Equal Then
                    For ii As Integer = 1 To 6
                        TotalNo_Count_For_Sch_Qty += 1
                        If ii > Week Then
                            TotalNo_Count_For_Sch_Qty -= 1
                        End If
                    Next
                End If
                '==========================================
                If clsCommon.CompairString(Sch_Type, "D") = CompairStringResult.Equal Then
                    For ii As Integer = 1 To 31
                        TotalNo_Count_For_Sch_Qty += 1
                        If ii > days Then
                            TotalNo_Count_For_Sch_Qty -= 1
                        End If
                    Next
                End If
                '==================
                If clsCommon.CompairString(Sch_Type, "M") = CompairStringResult.Equal Then
                    For ii As Integer = 1 To 12
                        TotalNo_Count_For_Sch_Qty += 1
                    Next
                End If
                Visible_Counter = TotalNo_Count_For_Sch_Qty
                '=============end here=================================================================

                obj.Document_Date = clsCommon.GETSERVERDATE(Trans)
                obj.Description = objPur.Description
                obj.Vendor_Code = objPur.Vendor_Code
                If clsCommon.myLen(Sch_Date) = 4 Then
                    obj.Schedule_Month = clsCommon.myCDate("01/01/" + clsCommon.myCstr(Sch_Date))
                Else
                    obj.Schedule_Month = clsCommon.myCDate(Sch_Date)
                End If

                obj.Schedule_Type = Sch_Type
                obj.PO_Code = objPur.PurchaseOrder_No
                obj.PO_Type = objPur.PurchaseOrder_Type

                obj.Arr = New List(Of clsPurchaseScheduleDetail)
                obj.Arr_Vendor = New List(Of clsPurchaseScheduleVendorDetail)

                For Each objPurD As clsPurchaseOrderDetail In objPur.Arr
                    objtr = New clsPurchaseScheduleDetail()
                    objtr1 = New clsPurchaseScheduleVendorDetail()

                    objtr.Line_No = objPurD.Line_No
                    objtr.Item_Code = objPurD.Item_Code
                    objtr.Unit_Code = objPurD.Unit_code
                    objtr.PO_Code = objPur.PurchaseOrder_No
                    objtr.PO_Date = objPur.PurchaseOrder_Date
                    objtr.PO_Qty = objPurD.PurchaseOrder_Qty
                    objtr.Schedule_Qty = objPurD.PurchaseOrder_Qty
                    Dim mod_value As Decimal = 0
                    Dim qty As Decimal = 0
                    Dim xBal As Double = 0

                    stck_qty = clsCommon.myCdbl(clsItemLocationDetails.getBalance(objPurD.Item_Code, objPur.Bill_To_Location, objPur.PurchaseOrder_No, objPur.PurchaseOrder_Date, Trans, objPurD.Unit_code, objPurD.MRP))

                    If clsCommon.CompairString(Sch_Type, "W") = CompairStringResult.Equal Then
                        Dim Week_of_sch As Integer = 1 ' clsPurchaseSchedule.GetNoOfWeekInMonth(Sch_Date, False, Trans)
                        TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - Week_of_sch + 1
                        mod_value = clsCommon.myCdbl(objPurD.PurchaseOrder_Qty) Mod TotalNo_Count_For_Sch_Qty
                        qty = (clsCommon.myCdbl(objPurD.PurchaseOrder_Qty) - mod_value) / TotalNo_Count_For_Sch_Qty

                        For ii As Integer = 1 To 6
                            If ii <= Visible_Counter AndAlso ii >= Week_of_sch Then
                                objtr.Week1_Qty = IIf(ii = 1, qty, objtr.Week1_Qty)
                                objtr.Week2_Qty = IIf(ii = 2, qty, objtr.Week2_Qty)
                                objtr.Week3_Qty = IIf(ii = 3, qty, objtr.Week3_Qty)
                                objtr.Week4_Qty = IIf(ii = 4, qty, objtr.Week4_Qty)
                                objtr.Week5_Qty = IIf(ii = 5, qty, objtr.Week5_Qty)
                                objtr.Week6_Qty = IIf(ii = 6, qty, objtr.Week6_Qty)
                            Else
                                objtr.Week1_Qty = IIf(ii = 1, Nothing, objtr.Week1_Qty)
                                objtr.Week2_Qty = IIf(ii = 2, Nothing, objtr.Week2_Qty)
                                objtr.Week3_Qty = IIf(ii = 3, Nothing, objtr.Week3_Qty)
                                objtr.Week4_Qty = IIf(ii = 4, Nothing, objtr.Week4_Qty)
                                objtr.Week5_Qty = IIf(ii = 5, Nothing, objtr.Week5_Qty)
                                objtr.Week6_Qty = IIf(ii = 6, Nothing, objtr.Week6_Qty)
                            End If
                        Next
                        Dim xx As Integer = TotalNo_Count_For_Sch_Qty + Week_of_sch - 1
                        objtr.Week1_Qty = IIf(xx = 1, qty + mod_value, objtr.Week1_Qty)
                        objtr.Week2_Qty = IIf(xx = 2, qty + mod_value, objtr.Week2_Qty)
                        objtr.Week3_Qty = IIf(xx = 3, qty + mod_value, objtr.Week3_Qty)
                        objtr.Week4_Qty = IIf(xx = 4, qty + mod_value, objtr.Week4_Qty)
                        objtr.Week5_Qty = IIf(xx = 5, qty + mod_value, objtr.Week5_Qty)
                        objtr.Week6_Qty = IIf(xx = 6, qty + mod_value, objtr.Week6_Qty)
                        TotalNo_Count_For_Sch_Qty = xx

                        If objtr.Week1_Qty > 0 AndAlso stck_qty > objtr.Week1_Qty Then
                            objtr.Week1_Qty = 0
                            xBal = stck_qty - objtr.Week1_Qty
                        ElseIf objtr.Week1_Qty > 0 AndAlso stck_qty <= objtr.Week1_Qty Then
                            objtr.Week1_Qty = objtr.Week1_Qty - stck_qty
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 1 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 1)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 1)

                            objtr.Week2_Qty += IIf(TotalNo_Count_For_Sch_Qty = 2, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 2, qty, 0))
                            objtr.Week3_Qty += IIf(TotalNo_Count_For_Sch_Qty = 3, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 3, qty, 0))
                            objtr.Week4_Qty += IIf(TotalNo_Count_For_Sch_Qty = 4, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 4, qty, 0))
                            objtr.Week5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Week6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Week2_Qty > 0 AndAlso xBal > objtr.Week2_Qty Then
                            objtr.Week2_Qty = 0
                            xBal = xBal - objtr.Week2_Qty
                        ElseIf objtr.Week2_Qty > 0 AndAlso xBal <= objtr.Week2_Qty Then
                            objtr.Week2_Qty = objtr.Week2_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Week2_Qty += IIf(2 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 2 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 2)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 2)

                            objtr.Week3_Qty += IIf(TotalNo_Count_For_Sch_Qty = 3, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 3, qty, 0))
                            objtr.Week4_Qty += IIf(TotalNo_Count_For_Sch_Qty = 4, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 4, qty, 0))
                            objtr.Week5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Week6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If

                        If objtr.Week3_Qty > 0 AndAlso xBal > objtr.Week3_Qty Then
                            objtr.Week3_Qty = 0
                            xBal = xBal - objtr.Week3_Qty
                        ElseIf objtr.Week3_Qty > 0 AndAlso xBal <= objtr.Week3_Qty Then
                            objtr.Week3_Qty = objtr.Week3_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Week3_Qty += IIf(3 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 3 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 3)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 3)

                            objtr.Week4_Qty += IIf(TotalNo_Count_For_Sch_Qty = 4, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 4, qty, 0))
                            objtr.Week5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Week6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Week4_Qty > 0 AndAlso xBal > objtr.Week4_Qty Then
                            objtr.Week4_Qty = 0
                            xBal = xBal - objtr.Week4_Qty
                        ElseIf objtr.Week4_Qty > 0 AndAlso xBal <= objtr.Week4_Qty Then
                            objtr.Week4_Qty = objtr.Week4_Qty - stck_qty
                            If xBal <= 0 Then
                                objtr.Week4_Qty += IIf(4 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 4 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 4)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 4)

                            objtr.Week5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Week6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Week5_Qty > 0 AndAlso xBal > objtr.Week5_Qty Then
                            objtr.Week5_Qty = 0
                            xBal = xBal - objtr.Week5_Qty
                        ElseIf objtr.Week5_Qty > 0 AndAlso xBal <= objtr.Week5_Qty Then
                            objtr.Week5_Qty = objtr.Week5_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Week5_Qty += IIf(5 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 5 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 5)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 5)

                            objtr.Week6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Week6_Qty > 0 AndAlso xBal > objtr.Week6_Qty Then
                            objtr.Week6_Qty = 0
                            xBal = xBal - objtr.Week6_Qty
                        ElseIf objtr.Week6_Qty > 0 AndAlso xBal <= objtr.Week6_Qty Then
                            objtr.Week6_Qty = objtr.Week6_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Week6_Qty += IIf(6 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                    End If
                    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
                    If clsCommon.CompairString(Sch_Type, "D") = CompairStringResult.Equal Then
                        Dim day_of_sch As Integer = 1 ' CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(Sch_Date, "dd")))
                        TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - day_of_sch + 1
                        mod_value = clsCommon.myCdbl(objPurD.PurchaseOrder_Qty) Mod TotalNo_Count_For_Sch_Qty
                        qty = (clsCommon.myCdbl(objPurD.PurchaseOrder_Qty) - mod_value) / TotalNo_Count_For_Sch_Qty

                        For ii As Integer = 1 To 31
                            If ii <= Visible_Counter AndAlso ii >= day_of_sch Then
                                objtr.Day1_Qty = IIf(ii = 1, qty, objtr.Day1_Qty)
                                objtr.Day2_Qty = IIf(ii = 2, qty, objtr.Day2_Qty)
                                objtr.Day3_Qty = IIf(ii = 3, qty, objtr.Day3_Qty)
                                objtr.Day4_Qty = IIf(ii = 4, qty, objtr.Day4_Qty)
                                objtr.Day5_Qty = IIf(ii = 5, qty, objtr.Day5_Qty)
                                objtr.Day6_Qty = IIf(ii = 6, qty, objtr.Day6_Qty)
                                objtr.Day7_Qty = IIf(ii = 7, qty, objtr.Day7_Qty)
                                objtr.Day8_Qty = IIf(ii = 8, qty, objtr.Day8_Qty)
                                objtr.Day9_Qty = IIf(ii = 9, qty, objtr.Day9_Qty)
                                objtr.Day10_Qty = IIf(ii = 10, qty, objtr.Day10_Qty)
                                objtr.Day11_Qty = IIf(ii = 11, qty, objtr.Day11_Qty)
                                objtr.Day12_Qty = IIf(ii = 12, qty, objtr.Day12_Qty)
                                objtr.Day13_Qty = IIf(ii = 13, qty, objtr.Day13_Qty)
                                objtr.Day14_Qty = IIf(ii = 14, qty, objtr.Day14_Qty)
                                objtr.Day15_Qty = IIf(ii = 15, qty, objtr.Day15_Qty)
                                objtr.Day16_Qty = IIf(ii = 16, qty, objtr.Day16_Qty)
                                objtr.Day17_Qty = IIf(ii = 17, qty, objtr.Day17_Qty)
                                objtr.Day18_Qty = IIf(ii = 18, qty, objtr.Day18_Qty)
                                objtr.Day19_Qty = IIf(ii = 19, qty, objtr.Day19_Qty)
                                objtr.Day20_Qty = IIf(ii = 20, qty, objtr.Day20_Qty)
                                objtr.Day21_Qty = IIf(ii = 21, qty, objtr.Day21_Qty)
                                objtr.Day22_Qty = IIf(ii = 22, qty, objtr.Day22_Qty)
                                objtr.Day23_Qty = IIf(ii = 23, qty, objtr.Day23_Qty)
                                objtr.Day24_Qty = IIf(ii = 24, qty, objtr.Day24_Qty)
                                objtr.Day25_Qty = IIf(ii = 25, qty, objtr.Day25_Qty)
                                objtr.Day26_Qty = IIf(ii = 26, qty, objtr.Day26_Qty)
                                objtr.Day27_Qty = IIf(ii = 27, qty, objtr.Day27_Qty)
                                objtr.Day28_Qty = IIf(ii = 28, qty, objtr.Day28_Qty)
                                objtr.Day29_Qty = IIf(ii = 29, qty, objtr.Day29_Qty)
                                objtr.Day30_Qty = IIf(ii = 30, qty, objtr.Day30_Qty)
                                objtr.Day31_Qty = IIf(ii = 31, qty, objtr.Day31_Qty)
                            Else
                                objtr.Day1_Qty = IIf(ii = 1, Nothing, objtr.Day1_Qty)
                                objtr.Day2_Qty = IIf(ii = 2, Nothing, objtr.Day2_Qty)
                                objtr.Day3_Qty = IIf(ii = 3, Nothing, objtr.Day3_Qty)
                                objtr.Day4_Qty = IIf(ii = 4, Nothing, objtr.Day4_Qty)
                                objtr.Day5_Qty = IIf(ii = 5, Nothing, objtr.Day5_Qty)
                                objtr.Day6_Qty = IIf(ii = 6, Nothing, objtr.Day6_Qty)
                                objtr.Day7_Qty = IIf(ii = 7, Nothing, objtr.Day7_Qty)
                                objtr.Day8_Qty = IIf(ii = 8, Nothing, objtr.Day8_Qty)
                                objtr.Day9_Qty = IIf(ii = 9, Nothing, objtr.Day9_Qty)
                                objtr.Day10_Qty = IIf(ii = 10, Nothing, objtr.Day10_Qty)
                                objtr.Day11_Qty = IIf(ii = 11, Nothing, objtr.Day11_Qty)
                                objtr.Day12_Qty = IIf(ii = 12, Nothing, objtr.Day12_Qty)
                                objtr.Day13_Qty = IIf(ii = 13, Nothing, objtr.Day13_Qty)
                                objtr.Day14_Qty = IIf(ii = 14, Nothing, objtr.Day14_Qty)
                                objtr.Day15_Qty = IIf(ii = 15, Nothing, objtr.Day15_Qty)
                                objtr.Day16_Qty = IIf(ii = 16, Nothing, objtr.Day16_Qty)
                                objtr.Day17_Qty = IIf(ii = 17, Nothing, objtr.Day17_Qty)
                                objtr.Day18_Qty = IIf(ii = 18, Nothing, objtr.Day18_Qty)
                                objtr.Day19_Qty = IIf(ii = 19, Nothing, objtr.Day19_Qty)
                                objtr.Day20_Qty = IIf(ii = 20, Nothing, objtr.Day20_Qty)
                                objtr.Day21_Qty = IIf(ii = 21, Nothing, objtr.Day21_Qty)
                                objtr.Day22_Qty = IIf(ii = 22, Nothing, objtr.Day22_Qty)
                                objtr.Day23_Qty = IIf(ii = 23, Nothing, objtr.Day23_Qty)
                                objtr.Day24_Qty = IIf(ii = 24, Nothing, objtr.Day24_Qty)
                                objtr.Day25_Qty = IIf(ii = 25, Nothing, objtr.Day25_Qty)
                                objtr.Day26_Qty = IIf(ii = 26, Nothing, objtr.Day26_Qty)
                                objtr.Day27_Qty = IIf(ii = 27, Nothing, objtr.Day27_Qty)
                                objtr.Day28_Qty = IIf(ii = 28, Nothing, objtr.Day28_Qty)
                                objtr.Day29_Qty = IIf(ii = 29, Nothing, objtr.Day29_Qty)
                                objtr.Day30_Qty = IIf(ii = 30, Nothing, objtr.Day30_Qty)
                                objtr.Day31_Qty = IIf(ii = 31, Nothing, objtr.Day31_Qty)
                            End If
                        Next
                        Dim xx As Integer = TotalNo_Count_For_Sch_Qty + day_of_sch - 1
                        objtr.Day1_Qty = IIf(xx = 1, qty + mod_value, objtr.Day1_Qty)
                        objtr.Day2_Qty = IIf(xx = 2, qty + mod_value, objtr.Day2_Qty)
                        objtr.Day3_Qty = IIf(xx = 3, qty + mod_value, objtr.Day3_Qty)
                        objtr.Day4_Qty = IIf(xx = 4, qty + mod_value, objtr.Day4_Qty)
                        objtr.Day5_Qty = IIf(xx = 5, qty + mod_value, objtr.Day5_Qty)
                        objtr.Day6_Qty = IIf(xx = 6, qty + mod_value, objtr.Day6_Qty)
                        objtr.Day7_Qty = IIf(xx = 7, qty + mod_value, objtr.Day7_Qty)
                        objtr.Day8_Qty = IIf(xx = 8, qty + mod_value, objtr.Day8_Qty)
                        objtr.Day9_Qty = IIf(xx = 9, qty + mod_value, objtr.Day9_Qty)
                        objtr.Day10_Qty = IIf(xx = 10, qty + mod_value, objtr.Day10_Qty)
                        objtr.Day11_Qty = IIf(xx = 11, qty + mod_value, objtr.Day11_Qty)
                        objtr.Day12_Qty = IIf(xx = 12, qty + mod_value, objtr.Day12_Qty)
                        objtr.Day13_Qty = IIf(xx = 13, qty + mod_value, objtr.Day13_Qty)
                        objtr.Day14_Qty = IIf(xx = 14, qty + mod_value, objtr.Day14_Qty)
                        objtr.Day15_Qty = IIf(xx = 15, qty + mod_value, objtr.Day15_Qty)
                        objtr.Day16_Qty = IIf(xx = 16, qty + mod_value, objtr.Day16_Qty)
                        objtr.Day17_Qty = IIf(xx = 17, qty + mod_value, objtr.Day17_Qty)
                        objtr.Day18_Qty = IIf(xx = 18, qty + mod_value, objtr.Day18_Qty)
                        objtr.Day19_Qty = IIf(xx = 19, qty + mod_value, objtr.Day19_Qty)
                        objtr.Day20_Qty = IIf(xx = 20, qty + mod_value, objtr.Day20_Qty)
                        objtr.Day21_Qty = IIf(xx = 21, qty + mod_value, objtr.Day21_Qty)
                        objtr.Day22_Qty = IIf(xx = 22, qty + mod_value, objtr.Day22_Qty)
                        objtr.Day23_Qty = IIf(xx = 23, qty + mod_value, objtr.Day23_Qty)
                        objtr.Day24_Qty = IIf(xx = 24, qty + mod_value, objtr.Day24_Qty)
                        objtr.Day25_Qty = IIf(xx = 25, qty + mod_value, objtr.Day25_Qty)
                        objtr.Day26_Qty = IIf(xx = 26, qty + mod_value, objtr.Day26_Qty)
                        objtr.Day27_Qty = IIf(xx = 27, qty + mod_value, objtr.Day27_Qty)
                        objtr.Day28_Qty = IIf(xx = 28, qty + mod_value, objtr.Day28_Qty)
                        objtr.Day29_Qty = IIf(xx = 29, qty + mod_value, objtr.Day29_Qty)
                        objtr.Day30_Qty = IIf(xx = 30, qty + mod_value, objtr.Day30_Qty)
                        objtr.Day31_Qty = IIf(xx = 31, qty + mod_value, objtr.Day31_Qty)

                        TotalNo_Count_For_Sch_Qty = xx
                        xBal = 0

                        If objtr.Day1_Qty > 0 AndAlso stck_qty > objtr.Day1_Qty Then
                            objtr.Day1_Qty = 0
                            xBal = stck_qty - objtr.Day1_Qty
                        ElseIf objtr.Day1_Qty > 0 AndAlso stck_qty <= objtr.Day1_Qty Then
                            objtr.Day1_Qty = objtr.Day1_Qty - stck_qty
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 1 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 1)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 1)

                            objtr.Day2_Qty += IIf(TotalNo_Count_For_Sch_Qty = 2, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 2, qty, 0))
                            objtr.Day3_Qty += IIf(TotalNo_Count_For_Sch_Qty = 3, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 3, qty, 0))
                            objtr.Day4_Qty += IIf(TotalNo_Count_For_Sch_Qty = 4, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 4, qty, 0))
                            objtr.Day5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Day6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            objtr.Day7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Day8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Day9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Day10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Day11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day2_Qty > 0 AndAlso xBal > objtr.Day2_Qty Then
                            objtr.Day2_Qty = 0
                            xBal = xBal - objtr.Day2_Qty
                        ElseIf objtr.Day2_Qty > 0 AndAlso xBal <= objtr.Day2_Qty Then
                            objtr.Day2_Qty = objtr.Day2_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day2_Qty += IIf(2 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 2 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 2)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 2)

                            objtr.Day3_Qty += IIf(TotalNo_Count_For_Sch_Qty = 3, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 3, qty, 0))
                            objtr.Day4_Qty += IIf(TotalNo_Count_For_Sch_Qty = 4, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 4, qty, 0))
                            objtr.Day5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Day6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            objtr.Day7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Day8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Day9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Day10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Day11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day3_Qty > 0 AndAlso xBal > objtr.Day3_Qty Then
                            objtr.Day3_Qty = 0
                            xBal = xBal - objtr.Day3_Qty
                        ElseIf objtr.Day3_Qty > 0 AndAlso xBal <= objtr.Day3_Qty Then
                            objtr.Day3_Qty = objtr.Day3_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day3_Qty += IIf(3 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 3 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 3)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 3)

                            objtr.Day4_Qty += IIf(TotalNo_Count_For_Sch_Qty = 4, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 4, qty, 0))
                            objtr.Day5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Day6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            objtr.Day7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Day8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Day9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Day10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Day11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day4_Qty > 0 AndAlso xBal > objtr.Day4_Qty Then
                            objtr.Day4_Qty = 0
                            xBal = xBal - objtr.Day4_Qty
                        ElseIf objtr.Day4_Qty > 0 AndAlso xBal <= objtr.Day4_Qty Then
                            objtr.Day4_Qty = objtr.Day4_Qty - stck_qty
                            If xBal <= 0 Then
                                objtr.Day4_Qty += IIf(4 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 4 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 4)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 4)

                            objtr.Day5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Day6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            objtr.Day7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Day8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Day9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Day10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Day11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day5_Qty > 0 AndAlso xBal > objtr.Day5_Qty Then
                            objtr.Day5_Qty = 0
                            xBal = xBal - objtr.Day5_Qty
                        ElseIf objtr.Day5_Qty > 0 AndAlso xBal <= objtr.Day5_Qty Then
                            objtr.Day5_Qty = objtr.Day5_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day5_Qty += IIf(5 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 5 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 5)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 5)

                            objtr.Day6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            objtr.Day7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Day8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Day9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Day10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Day11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day6_Qty > 0 AndAlso xBal > objtr.Day6_Qty Then
                            objtr.Day6_Qty = 0
                            xBal = xBal - objtr.Day6_Qty
                        ElseIf objtr.Day6_Qty > 0 AndAlso xBal <= objtr.Day6_Qty Then
                            objtr.Day6_Qty = objtr.Day6_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day6_Qty += IIf(6 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                        '======================7======================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 6 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 6)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 6)

                            objtr.Day7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Day8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Day9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Day10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Day11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day7_Qty > 0 AndAlso xBal > objtr.Day7_Qty Then
                            objtr.Day7_Qty = 0
                            xBal = xBal - objtr.Day7_Qty
                        ElseIf objtr.Day7_Qty > 0 AndAlso xBal <= objtr.Day7_Qty Then
                            objtr.Day7_Qty = objtr.Day7_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day7_Qty += IIf(7 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 7 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 7)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 7)

                            objtr.Day8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Day9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Day10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Day11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day8_Qty > 0 AndAlso xBal > objtr.Day8_Qty Then
                            objtr.Day8_Qty = 0
                            xBal = xBal - objtr.Day8_Qty
                        ElseIf objtr.Day8_Qty > 0 AndAlso xBal <= objtr.Day8_Qty Then
                            objtr.Day8_Qty = objtr.Day8_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day8_Qty += IIf(8 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 8 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 8)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 8)

                            objtr.Day9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Day10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Day11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day9_Qty > 0 AndAlso xBal > objtr.Day9_Qty Then
                            objtr.Day9_Qty = 0
                            xBal = xBal - objtr.Day9_Qty
                        ElseIf objtr.Day9_Qty > 0 AndAlso xBal <= objtr.Day9_Qty Then
                            objtr.Day9_Qty = objtr.Day9_Qty - stck_qty
                            If xBal <= 0 Then
                                objtr.Day9_Qty += IIf(9 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 9 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 9)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 9)

                            objtr.Day10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Day11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day10_Qty > 0 AndAlso xBal > objtr.Day10_Qty Then
                            objtr.Day10_Qty = 0
                            xBal = xBal - objtr.Day10_Qty
                        ElseIf objtr.Day10_Qty > 0 AndAlso xBal <= objtr.Day10_Qty Then
                            objtr.Day10_Qty = objtr.Day10_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day10_Qty += IIf(10 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 10 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 10)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 10)

                            objtr.Day11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day11_Qty > 0 AndAlso xBal > objtr.Day11_Qty Then
                            objtr.Day11_Qty = 0
                            xBal = xBal - objtr.Day11_Qty
                        ElseIf objtr.Day11_Qty > 0 AndAlso xBal <= objtr.Day11_Qty Then
                            objtr.Day11_Qty = objtr.Day11_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day11_Qty += IIf(11 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                        '=============================13========================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 11 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 11)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 11)

                            objtr.Day12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day12_Qty > 0 AndAlso xBal > objtr.Day12_Qty Then
                            objtr.Day12_Qty = 0
                            xBal = xBal - objtr.Day12_Qty
                        ElseIf objtr.Day12_Qty > 0 AndAlso xBal <= objtr.Day12_Qty Then
                            objtr.Day12_Qty = objtr.Day12_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day12_Qty += IIf(12 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 12 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 12)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 12)

                            objtr.Day13_Qty += IIf(TotalNo_Count_For_Sch_Qty = 13, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 13, qty, 0))
                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day13_Qty > 0 AndAlso xBal > objtr.Day13_Qty Then
                            objtr.Day13_Qty = 0
                            xBal = xBal - objtr.Day13_Qty
                        ElseIf objtr.Day13_Qty > 0 AndAlso xBal <= objtr.Day13_Qty Then
                            objtr.Day13_Qty = objtr.Day13_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day13_Qty += IIf(13 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 13 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 13)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 13)

                            objtr.Day14_Qty += IIf(TotalNo_Count_For_Sch_Qty = 14, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 14, qty, 0))
                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day14_Qty > 0 AndAlso xBal > objtr.Day14_Qty Then
                            objtr.Day14_Qty = 0
                            xBal = xBal - objtr.Day14_Qty
                        ElseIf objtr.Day14_Qty > 0 AndAlso xBal <= objtr.Day14_Qty Then
                            objtr.Day14_Qty = objtr.Day14_Qty - stck_qty
                            If xBal <= 0 Then
                                objtr.Day14_Qty += IIf(14 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 14 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 14)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 14)

                            objtr.Day15_Qty += IIf(TotalNo_Count_For_Sch_Qty = 15, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 15, qty, 0))
                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day15_Qty > 0 AndAlso xBal > objtr.Day15_Qty Then
                            objtr.Day15_Qty = 0
                            xBal = xBal - objtr.Day15_Qty
                        ElseIf objtr.Day15_Qty > 0 AndAlso xBal <= objtr.Day15_Qty Then
                            objtr.Day15_Qty = objtr.Day15_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day15_Qty += IIf(15 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 15 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 15)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 15)

                            objtr.Day16_Qty += IIf(TotalNo_Count_For_Sch_Qty = 16, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 16, qty, 0))
                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day16_Qty > 0 AndAlso xBal > objtr.Day16_Qty Then
                            objtr.Day16_Qty = 0
                            xBal = xBal - objtr.Day16_Qty
                        ElseIf objtr.Day16_Qty > 0 AndAlso xBal <= objtr.Day16_Qty Then
                            objtr.Day16_Qty = objtr.Day16_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day16_Qty += IIf(16 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                        '============================20==========================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 16 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 16)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 16)

                            objtr.Day17_Qty += IIf(TotalNo_Count_For_Sch_Qty = 17, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 17, qty, 0))
                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day17_Qty > 0 AndAlso xBal > objtr.Day17_Qty Then
                            objtr.Day17_Qty = 0
                            xBal = xBal - objtr.Day17_Qty
                        ElseIf objtr.Day17_Qty > 0 AndAlso xBal <= objtr.Day17_Qty Then
                            objtr.Day17_Qty = objtr.Day17_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day17_Qty += IIf(17 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 17 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 17)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 17)

                            objtr.Day18_Qty += IIf(TotalNo_Count_For_Sch_Qty = 18, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 18, qty, 0))
                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day18_Qty > 0 AndAlso xBal > objtr.Day18_Qty Then
                            objtr.Day18_Qty = 0
                            xBal = xBal - objtr.Day18_Qty
                        ElseIf objtr.Day18_Qty > 0 AndAlso xBal <= objtr.Day18_Qty Then
                            objtr.Day18_Qty = objtr.Day18_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day18_Qty += IIf(18 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 18 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 18)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 18)

                            objtr.Day19_Qty += IIf(TotalNo_Count_For_Sch_Qty = 19, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 19, qty, 0))
                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day19_Qty > 0 AndAlso xBal > objtr.Day19_Qty Then
                            objtr.Day19_Qty = 0
                            xBal = xBal - objtr.Day19_Qty
                        ElseIf objtr.Day19_Qty > 0 AndAlso xBal <= objtr.Day19_Qty Then
                            objtr.Day19_Qty = objtr.Day19_Qty - stck_qty
                            If xBal <= 0 Then
                                objtr.Day19_Qty += IIf(19 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 19 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 19)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 19)

                            objtr.Day20_Qty += IIf(TotalNo_Count_For_Sch_Qty = 20, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 20, qty, 0))
                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day20_Qty > 0 AndAlso xBal > objtr.Day20_Qty Then
                            objtr.Day20_Qty = 0
                            xBal = xBal - objtr.Day20_Qty
                        ElseIf objtr.Day20_Qty > 0 AndAlso xBal <= objtr.Day20_Qty Then
                            objtr.Day20_Qty = objtr.Day20_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day20_Qty += IIf(20 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 20 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 20)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 20)

                            objtr.Day21_Qty += IIf(TotalNo_Count_For_Sch_Qty = 21, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 21, qty, 0))
                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day21_Qty > 0 AndAlso xBal > objtr.Day21_Qty Then
                            objtr.Day21_Qty = 0
                            xBal = xBal - objtr.Day21_Qty
                        ElseIf objtr.Day21_Qty > 0 AndAlso xBal <= objtr.Day21_Qty Then
                            objtr.Day21_Qty = objtr.Day21_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day21_Qty += IIf(21 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                        '=========================25==========================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 21 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 21)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 21)

                            objtr.Day22_Qty += IIf(TotalNo_Count_For_Sch_Qty = 22, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 22, qty, 0))
                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day22_Qty > 0 AndAlso xBal > objtr.Day22_Qty Then
                            objtr.Day22_Qty = 0
                            xBal = xBal - objtr.Day22_Qty
                        ElseIf objtr.Day22_Qty > 0 AndAlso xBal <= objtr.Day22_Qty Then
                            objtr.Day22_Qty = objtr.Day22_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day22_Qty += IIf(22 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 22 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 22)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 22)

                            objtr.Day23_Qty += IIf(TotalNo_Count_For_Sch_Qty = 23, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 23, qty, 0))
                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day23_Qty > 0 AndAlso xBal > objtr.Day23_Qty Then
                            objtr.Day23_Qty = 0
                            xBal = xBal - objtr.Day23_Qty
                        ElseIf objtr.Day23_Qty > 0 AndAlso xBal <= objtr.Day23_Qty Then
                            objtr.Day23_Qty = objtr.Day23_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day23_Qty += IIf(23 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 23 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 23)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 23)

                            objtr.Day24_Qty += IIf(TotalNo_Count_For_Sch_Qty = 24, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 24, qty, 0))
                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day24_Qty > 0 AndAlso xBal > objtr.Day24_Qty Then
                            objtr.Day24_Qty = 0
                            xBal = xBal - objtr.Day24_Qty
                        ElseIf objtr.Day24_Qty > 0 AndAlso xBal <= objtr.Day24_Qty Then
                            objtr.Day24_Qty = objtr.Day24_Qty - stck_qty
                            If xBal <= 0 Then
                                objtr.Day24_Qty += IIf(24 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 24 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 24)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 24)

                            objtr.Day25_Qty += IIf(TotalNo_Count_For_Sch_Qty = 25, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 25, qty, 0))
                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day25_Qty > 0 AndAlso xBal > objtr.Day25_Qty Then
                            objtr.Day25_Qty = 0
                            xBal = xBal - objtr.Day25_Qty
                        ElseIf objtr.Day25_Qty > 0 AndAlso xBal <= objtr.Day25_Qty Then
                            objtr.Day25_Qty = objtr.Day25_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day25_Qty += IIf(25 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 25 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 25)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 25)

                            objtr.Day26_Qty += IIf(TotalNo_Count_For_Sch_Qty = 26, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 26, qty, 0))
                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day26_Qty > 0 AndAlso xBal > objtr.Day26_Qty Then
                            objtr.Day26_Qty = 0
                            xBal = xBal - objtr.Day26_Qty
                        ElseIf objtr.Day26_Qty > 0 AndAlso xBal <= objtr.Day26_Qty Then
                            objtr.Day26_Qty = objtr.Day26_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day26_Qty += IIf(26 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 26 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 26)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 26)

                            objtr.Day27_Qty += IIf(TotalNo_Count_For_Sch_Qty = 27, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 27, qty, 0))
                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day27_Qty > 0 AndAlso xBal > objtr.Day27_Qty Then
                            objtr.Day27_Qty = 0
                            xBal = xBal - objtr.Day27_Qty
                        ElseIf objtr.Day27_Qty > 0 AndAlso xBal <= objtr.Day27_Qty Then
                            objtr.Day27_Qty = objtr.Day27_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day27_Qty += IIf(27 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 27 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 27)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 27)

                            objtr.Day28_Qty += IIf(TotalNo_Count_For_Sch_Qty = 28, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 28, qty, 0))
                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day28_Qty > 0 AndAlso xBal > objtr.Day28_Qty Then
                            objtr.Day28_Qty = 0
                            xBal = xBal - objtr.Day28_Qty
                        ElseIf objtr.Day28_Qty > 0 AndAlso xBal <= objtr.Day28_Qty Then
                            objtr.Day28_Qty = objtr.Day28_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day28_Qty += IIf(28 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 28 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 28)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 28)

                            objtr.Day29_Qty += IIf(TotalNo_Count_For_Sch_Qty = 29, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 29, qty, 0))
                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day29_Qty > 0 AndAlso xBal > objtr.Day29_Qty Then
                            objtr.Day29_Qty = 0
                            xBal = xBal - objtr.Day29_Qty
                        ElseIf objtr.Day29_Qty > 0 AndAlso xBal <= objtr.Day29_Qty Then
                            objtr.Day29_Qty = objtr.Day29_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day29_Qty += IIf(29 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 29 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 29)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 29)

                            objtr.Day30_Qty += IIf(TotalNo_Count_For_Sch_Qty = 30, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 30, qty, 0))
                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day30_Qty > 0 AndAlso xBal > objtr.Day30_Qty Then
                            objtr.Day30_Qty = 0
                            xBal = xBal - objtr.Day30_Qty
                        ElseIf objtr.Day30_Qty > 0 AndAlso xBal <= objtr.Day30_Qty Then
                            objtr.Day30_Qty = objtr.Day30_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day30_Qty += IIf(30 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 30 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 30)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 30)

                            objtr.Day31_Qty += IIf(TotalNo_Count_For_Sch_Qty = 31, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 31, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Day31_Qty > 0 AndAlso xBal > objtr.Day31_Qty Then
                            objtr.Day31_Qty = 0
                            xBal = xBal - objtr.Day31_Qty
                        ElseIf objtr.Day31_Qty > 0 AndAlso xBal <= objtr.Day31_Qty Then
                            objtr.Day31_Qty = objtr.Day31_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Day31_Qty += IIf(31 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If

                    End If ''end D
                    'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
                    If clsCommon.CompairString(Sch_Type, "M") = CompairStringResult.Equal Then
                        TotalNo_Count_For_Sch_Qty = TotalNo_Count_For_Sch_Qty - 1 + 1 'TotalNo_Count_For_Sch_Qty - clsCommon.myCdbl(clsCommon.GetPrintDate(Sch_Date, "MM")) + 1
                        mod_value = clsCommon.myCdbl(objPurD.PurchaseOrder_Qty) Mod TotalNo_Count_For_Sch_Qty
                        qty = (clsCommon.myCdbl(objPurD.PurchaseOrder_Qty) - mod_value) / TotalNo_Count_For_Sch_Qty

                        For ii As Integer = 1 To 12
                            If ii <= Visible_Counter AndAlso ii >= 1 Then
                                objtr.Month1_Qty = IIf(ii = 1, qty, objtr.Month1_Qty)
                                objtr.Month2_Qty = IIf(ii = 2, qty, objtr.Month2_Qty)
                                objtr.Month3_Qty = IIf(ii = 3, qty, objtr.Month3_Qty)
                                objtr.Month4_Qty = IIf(ii = 4, qty, objtr.Month4_Qty)
                                objtr.Month5_Qty = IIf(ii = 5, qty, objtr.Month5_Qty)
                                objtr.Month6_Qty = IIf(ii = 6, qty, objtr.Month6_Qty)
                                objtr.Month7_Qty = IIf(ii = 7, qty, objtr.Month7_Qty)
                                objtr.Month8_Qty = IIf(ii = 8, qty, objtr.Month8_Qty)
                                objtr.Month9_Qty = IIf(ii = 9, qty, objtr.Month9_Qty)
                                objtr.Month10_Qty = IIf(ii = 10, qty, objtr.Month10_Qty)
                                objtr.Month11_Qty = IIf(ii = 11, qty, objtr.Month11_Qty)
                                objtr.Month12_Qty = IIf(ii = 12, qty, objtr.Month12_Qty)
                            Else
                                objtr.Month1_Qty = IIf(ii = 1, Nothing, objtr.Month1_Qty)
                                objtr.Month2_Qty = IIf(ii = 2, Nothing, objtr.Month2_Qty)
                                objtr.Month3_Qty = IIf(ii = 3, Nothing, objtr.Month3_Qty)
                                objtr.Month4_Qty = IIf(ii = 4, Nothing, objtr.Month4_Qty)
                                objtr.Month5_Qty = IIf(ii = 5, Nothing, objtr.Month5_Qty)
                                objtr.Month6_Qty = IIf(ii = 6, Nothing, objtr.Month6_Qty)
                                objtr.Month7_Qty = IIf(ii = 7, Nothing, objtr.Month7_Qty)
                                objtr.Month8_Qty = IIf(ii = 8, Nothing, objtr.Month8_Qty)
                                objtr.Month9_Qty = IIf(ii = 9, Nothing, objtr.Month9_Qty)
                                objtr.Month10_Qty = IIf(ii = 10, Nothing, objtr.Month10_Qty)
                                objtr.Month11_Qty = IIf(ii = 11, Nothing, objtr.Month11_Qty)
                                objtr.Month12_Qty = IIf(ii = 12, Nothing, objtr.Month12_Qty)
                            End If
                        Next
                        Dim xx As Integer = TotalNo_Count_For_Sch_Qty - 1 + 1
                        objtr.Month1_Qty = IIf(xx = 1, qty + mod_value, objtr.Month1_Qty)
                        objtr.Month2_Qty = IIf(xx = 2, qty + mod_value, objtr.Month2_Qty)
                        objtr.Month3_Qty = IIf(xx = 3, qty + mod_value, objtr.Month3_Qty)
                        objtr.Month4_Qty = IIf(xx = 4, qty + mod_value, objtr.Month4_Qty)
                        objtr.Month5_Qty = IIf(xx = 5, qty + mod_value, objtr.Month5_Qty)
                        objtr.Month6_Qty = IIf(xx = 6, qty + mod_value, objtr.Month6_Qty)
                        objtr.Month7_Qty = IIf(xx = 7, qty + mod_value, objtr.Month7_Qty)
                        objtr.Month8_Qty = IIf(xx = 8, qty + mod_value, objtr.Month8_Qty)
                        objtr.Month9_Qty = IIf(xx = 9, qty + mod_value, objtr.Month9_Qty)
                        objtr.Month10_Qty = IIf(xx = 10, qty + mod_value, objtr.Month10_Qty)
                        objtr.Month11_Qty = IIf(xx = 11, qty + mod_value, objtr.Month11_Qty)
                        objtr.Month12_Qty = IIf(xx = 12, qty + mod_value, objtr.Month12_Qty)

                        TotalNo_Count_For_Sch_Qty = xx

                        xBal = 0

                        If objtr.Month1_Qty > 0 AndAlso stck_qty > objtr.Month1_Qty Then
                            objtr.Month1_Qty = 0
                            xBal = stck_qty - objtr.Month1_Qty
                        ElseIf objtr.Month1_Qty > 0 AndAlso stck_qty <= objtr.Month1_Qty Then
                            objtr.Month1_Qty = objtr.Month1_Qty - stck_qty
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 1 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 1)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 1)

                            objtr.Month2_Qty += IIf(TotalNo_Count_For_Sch_Qty = 2, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 2, qty, 0))
                            objtr.Month3_Qty += IIf(TotalNo_Count_For_Sch_Qty = 3, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 3, qty, 0))
                            objtr.Month4_Qty += IIf(TotalNo_Count_For_Sch_Qty = 4, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 4, qty, 0))
                            objtr.Month5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Month6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            objtr.Month7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Month8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Month9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Month10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Month11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month2_Qty > 0 AndAlso xBal > objtr.Month2_Qty Then
                            objtr.Month2_Qty = 0
                            xBal = xBal - objtr.Month2_Qty
                        ElseIf objtr.Month2_Qty > 0 AndAlso xBal <= objtr.Month2_Qty Then
                            objtr.Month2_Qty = objtr.Month2_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Month2_Qty += IIf(2 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 2 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 2)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 2)

                            objtr.Month3_Qty += IIf(TotalNo_Count_For_Sch_Qty = 3, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 3, qty, 0))
                            objtr.Month4_Qty += IIf(TotalNo_Count_For_Sch_Qty = 4, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 4, qty, 0))
                            objtr.Month5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Month6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            objtr.Month7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Month8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Month9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Month10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Month11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month3_Qty > 0 AndAlso xBal > objtr.Month3_Qty Then
                            objtr.Month3_Qty = 0
                            xBal = xBal - objtr.Month3_Qty
                        ElseIf objtr.Month3_Qty > 0 AndAlso xBal <= objtr.Month3_Qty Then
                            objtr.Month3_Qty = objtr.Month3_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Month3_Qty += IIf(3 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 3 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 3)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 3)

                            objtr.Month4_Qty += IIf(TotalNo_Count_For_Sch_Qty = 4, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 4, qty, 0))
                            objtr.Month5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Month6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            objtr.Month7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Month8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Month9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Month10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Month11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month4_Qty > 0 AndAlso xBal > objtr.Month4_Qty Then
                            objtr.Month4_Qty = 0
                            xBal = xBal - objtr.Month4_Qty
                        ElseIf objtr.Month4_Qty > 0 AndAlso xBal <= objtr.Month4_Qty Then
                            objtr.Month4_Qty = objtr.Month4_Qty - stck_qty
                            If xBal <= 0 Then
                                objtr.Month4_Qty += IIf(4 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 4 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 4)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 4)

                            objtr.Month5_Qty += IIf(TotalNo_Count_For_Sch_Qty = 5, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 5, qty, 0))
                            objtr.Month6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            objtr.Month7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Month8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Month9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Month10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Month11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month5_Qty > 0 AndAlso xBal > objtr.Month5_Qty Then
                            objtr.Month5_Qty = 0
                            xBal = xBal - objtr.Month5_Qty
                        ElseIf objtr.Month5_Qty > 0 AndAlso xBal <= objtr.Month5_Qty Then
                            objtr.Month5_Qty = objtr.Month5_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Month5_Qty += IIf(5 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 5 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 5)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 5)

                            objtr.Month6_Qty += IIf(TotalNo_Count_For_Sch_Qty = 6, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 6, qty, 0))
                            objtr.Month7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Month8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Month9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Month10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Month11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month6_Qty > 0 AndAlso xBal > objtr.Month6_Qty Then
                            objtr.Month6_Qty = 0
                            xBal = xBal - objtr.Month6_Qty
                        ElseIf objtr.Month6_Qty > 0 AndAlso xBal <= objtr.Month6_Qty Then
                            objtr.Month6_Qty = objtr.Month6_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Month6_Qty += IIf(6 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                        '======================7======================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 6 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 6)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 6)

                            objtr.Month7_Qty += IIf(TotalNo_Count_For_Sch_Qty = 7, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 7, qty, 0))
                            objtr.Month8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Month9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Month10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Month11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month7_Qty > 0 AndAlso xBal > objtr.Month7_Qty Then
                            objtr.Month7_Qty = 0
                            xBal = xBal - objtr.Month7_Qty
                        ElseIf objtr.Month7_Qty > 0 AndAlso xBal <= objtr.Month7_Qty Then
                            objtr.Month7_Qty = objtr.Month7_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Month7_Qty += IIf(7 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 7 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 7)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 7)

                            objtr.Month8_Qty += IIf(TotalNo_Count_For_Sch_Qty = 8, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 8, qty, 0))
                            objtr.Month9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Month10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Month11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month8_Qty > 0 AndAlso xBal > objtr.Month8_Qty Then
                            objtr.Month8_Qty = 0
                            xBal = xBal - objtr.Month8_Qty
                        ElseIf objtr.Month8_Qty > 0 AndAlso xBal <= objtr.Month8_Qty Then
                            objtr.Month8_Qty = objtr.Month8_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Month8_Qty += IIf(8 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 8 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 8)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 8)

                            objtr.Month9_Qty += IIf(TotalNo_Count_For_Sch_Qty = 9, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 9, qty, 0))
                            objtr.Month10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Month11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month9_Qty > 0 AndAlso xBal > objtr.Month9_Qty Then
                            objtr.Month9_Qty = 0
                            xBal = xBal - objtr.Month9_Qty
                        ElseIf objtr.Month9_Qty > 0 AndAlso xBal <= objtr.Month9_Qty Then
                            objtr.Month9_Qty = objtr.Month9_Qty - stck_qty
                            If xBal <= 0 Then
                                objtr.Month9_Qty += IIf(9 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 9 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 9)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 9)

                            objtr.Month10_Qty += IIf(TotalNo_Count_For_Sch_Qty = 10, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 10, qty, 0))
                            objtr.Month11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month10_Qty > 0 AndAlso xBal > objtr.Month10_Qty Then
                            objtr.Month10_Qty = 0
                            xBal = xBal - objtr.Month10_Qty
                        ElseIf objtr.Month10_Qty > 0 AndAlso xBal <= objtr.Month10_Qty Then
                            objtr.Month10_Qty = objtr.Month10_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Month10_Qty += IIf(10 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                        '========================================================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 10 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 10)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 10)

                            objtr.Month11_Qty += IIf(TotalNo_Count_For_Sch_Qty = 11, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 11, qty, 0))
                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month11_Qty > 0 AndAlso xBal > objtr.Month11_Qty Then
                            objtr.Month11_Qty = 0
                            xBal = xBal - objtr.Month11_Qty
                        ElseIf objtr.Month11_Qty > 0 AndAlso xBal <= objtr.Month11_Qty Then
                            objtr.Month11_Qty = objtr.Month11_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Month11_Qty += IIf(11 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If
                        '=============================13========================================
                        If xBal <= 0 AndAlso TotalNo_Count_For_Sch_Qty > 11 Then
                            mod_value = stck_qty Mod (TotalNo_Count_For_Sch_Qty - 11)
                            qty = (stck_qty - mod_value) / (TotalNo_Count_For_Sch_Qty - 11)

                            objtr.Month12_Qty += IIf(TotalNo_Count_For_Sch_Qty = 12, qty + mod_value, IIf(TotalNo_Count_For_Sch_Qty > 12, qty, 0))
                            mod_value = 0
                            qty = 0
                            stck_qty = 0
                        End If
                        If objtr.Month12_Qty > 0 AndAlso xBal > objtr.Month12_Qty Then
                            objtr.Month12_Qty = 0
                            xBal = xBal - objtr.Month12_Qty
                        ElseIf objtr.Month12_Qty > 0 AndAlso xBal <= objtr.Month12_Qty Then
                            objtr.Month12_Qty = objtr.Month12_Qty - xBal
                            If xBal <= 0 Then
                                objtr.Month12_Qty += IIf(12 = TotalNo_Count_For_Sch_Qty, qty + mod_value, qty)
                            End If
                            xBal = 0
                        End If ''end
                    End If

                    objtr.Remarks = objPurD.Remarks

                    obj.Arr.Add(objtr)

                    '===================vendor grid==========================================================
                    objtr1.Line_No = objPurD.Line_No
                    objtr1.Item_Code = objPurD.Item_Code
                    objtr1.Unit_Code = objPurD.Unit_code
                    objtr1.PO_Code = objPur.PurchaseOrder_No
                    objtr1.PO_Date = objPur.PurchaseOrder_Date
                    objtr1.PO_Qty = objPurD.PurchaseOrder_Qty
                    objtr1.Schedule_Qty = objPurD.PurchaseOrder_Qty
                    objtr1.Week1_Qty = objtr.Week1_Qty
                    objtr1.Week2_Qty = objtr.Week2_Qty
                    objtr1.Week3_Qty = objtr.Week3_Qty
                    objtr1.Week4_Qty = objtr.Week4_Qty
                    objtr1.Week5_Qty = objtr.Week5_Qty
                    objtr1.Week6_Qty = objtr.Week6_Qty
                    objtr1.Day1_Qty = objtr.Day1_Qty
                    objtr1.Day2_Qty = objtr.Day2_Qty
                    objtr1.Day3_Qty = objtr.Day3_Qty
                    objtr1.Day4_Qty = objtr.Day4_Qty
                    objtr1.Day5_Qty = objtr.Day5_Qty
                    objtr1.Day6_Qty = objtr.Day6_Qty
                    objtr1.Day7_Qty = objtr.Day7_Qty
                    objtr1.Day8_Qty = objtr.Day8_Qty
                    objtr1.Day9_Qty = objtr.Day9_Qty
                    objtr1.Day10_Qty = objtr.Day10_Qty
                    objtr1.Day11_Qty = objtr.Day11_Qty
                    objtr1.Day12_Qty = objtr.Day12_Qty
                    objtr1.Day13_Qty = objtr.Day13_Qty
                    objtr1.Day14_Qty = objtr.Day14_Qty
                    objtr1.Day15_Qty = objtr.Day15_Qty
                    objtr1.Day16_Qty = objtr.Day16_Qty
                    objtr1.Day17_Qty = objtr.Day17_Qty
                    objtr1.Day18_Qty = objtr.Day18_Qty
                    objtr1.Day19_Qty = objtr.Day19_Qty
                    objtr1.Day20_Qty = objtr.Day20_Qty
                    objtr1.Day21_Qty = objtr.Day21_Qty
                    objtr1.Day22_Qty = objtr.Day22_Qty
                    objtr1.Day23_Qty = objtr.Day23_Qty
                    objtr1.Day24_Qty = objtr.Day24_Qty
                    objtr1.Day25_Qty = objtr.Day25_Qty
                    objtr1.Day26_Qty = objtr.Day26_Qty
                    objtr1.Day27_Qty = objtr.Day27_Qty
                    objtr1.Day28_Qty = objtr.Day28_Qty
                    objtr1.Day29_Qty = objtr.Day29_Qty
                    objtr1.Day30_Qty = objtr.Day30_Qty
                    objtr1.Day31_Qty = objtr.Day31_Qty
                    objtr1.Month1_Qty = objtr.Month1_Qty
                    objtr1.Month2_Qty = objtr.Month2_Qty
                    objtr1.Month3_Qty = objtr.Month3_Qty
                    objtr1.Month4_Qty = objtr.Month4_Qty
                    objtr1.Month5_Qty = objtr.Month5_Qty
                    objtr1.Month6_Qty = objtr.Month6_Qty
                    objtr1.Month7_Qty = objtr.Month7_Qty
                    objtr1.Month8_Qty = objtr.Month8_Qty
                    objtr1.Month9_Qty = objtr.Month9_Qty
                    objtr1.Month10_Qty = objtr.Month10_Qty
                    objtr1.Month11_Qty = objtr.Month11_Qty
                    objtr1.Month12_Qty = objtr.Month12_Qty
                    objtr1.Remarks = objPurD.Remarks

                    obj.Arr_Vendor.Add(objtr1)
                Next ''end cond.

                isSaved = isSaved AndAlso clsPurchaseSchedule.SaveData(obj, True, Trans)
                objPur.strScheduleNo = obj.Document_Code
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            obj = Nothing
            objtr = Nothing
            objtr1 = Nothing
        End Try

        Return isSaved
    End Function

    Public Shared Function closepodata(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal cls As String, Optional ByVal strRemarks As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            closepodata(trans, strDocNo, isCheckForPosted, cls, strRemarks)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function closepodata(ByVal trans As SqlTransaction, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, ByVal cls As String, Optional ByVal strRemarks As String = "") As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Purchase Order No not found to Close")
            End If
            Dim strClosedDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsPurchaseOrderHead = clsPurchaseOrderHead.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.PurchaseOrder_No) <= 0) Then
                Throw New Exception("No Data found to Close")
            End If
            obj.close_yn = cls
            obj.close_remarks = strRemarks
            Dim qry As String = "Update TSPL_PURCHASE_ORDER_HEAD set close_yn='" + obj.close_yn + "',close_remarks='" + obj.close_remarks + "',Closed_By='" + clsCommon.myCstr(objCommonVar.CurrentUserCode) + "',Closed_Date='" + strClosedDate + "' where PurchaseOrder_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    '''''  only for demo

    Public Shared Function CreateSaleOrder(ByVal isDoAbandomentNo As Boolean, ByVal objPOHead As clsPurchaseOrderHead, ByVal trans As SqlTransaction, ByVal strDatabasename As String) As Boolean
        Try

            Dim obj As New clsSNSalesOrderHead()
            Dim strCustomerCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_fixed_parameter where type='" + clsFixedParameterType.Principal_Customer + "' and code='" + clsFixedParameterCode.Principal_Customer + "'", trans))
            Dim strCustomerName = clsDBFuncationality.getSingleValue("select Customer_Name from " + strDatabasename + ".dbo.TSPL_CUSTOMER_MASTER  where Cust_Code='" + strCustomerCode + "'", trans)
            If clsCommon.myLen(strCustomerCode) = 0 Then
                Throw New Exception("Please enter customer for creating sale order ")
            End If
            obj.Cust_PO_No = objPOHead.PurchaseOrder_No
            obj.HeadDisc_Per = 0
            obj.Price_Group_Code = ""
            obj.Route_No = ""
            obj.Route_Desc = ""
            obj.Price_Code = ""
            obj.Document_Code = ""
            obj.Document_Date = objPOHead.PurchaseOrder_Date
            obj.CloseSO = "N"
            obj.Delivery_date = objPOHead.Delivery_date
            obj.Customer_Code = strCustomerCode
            obj.Customer_Name = strCustomerName
            obj.Ref_No = objPOHead.Ref_No
            obj.Total_Tax_Amt = objPOHead.Total_Tax_Amt
            obj.Remarks = objPOHead.Remarks
            obj.Bill_To_Location = objPOHead.Bill_To_Location
            obj.Ship_To_Location = objPOHead.Ship_To_Location
            obj.Comments = objPOHead.Comments
            obj.Comment1 = objPOHead.Comment1
            obj.Comment2 = objPOHead.Comment2
            obj.Comment3 = objPOHead.Comment3
            obj.Comment4 = objPOHead.Comment4
            obj.Comment5 = objPOHead.Comment5
            obj.Comment6 = objPOHead.Comment6
            obj.Comment7 = objPOHead.Comment7
            obj.Comment8 = objPOHead.Comment8
            obj.Comment9 = objPOHead.Comment9
            obj.Comment10 = objPOHead.Comment10
            obj.Comment11 = objPOHead.Comment11
            obj.Comment12 = objPOHead.Comment12
            obj.Comment13 = objPOHead.Comment13
            'obj.Comment14 = objPOHead.Comments
            'obj.Comment15 = objPOHead.Comments
            obj.On_Hold = objPOHead.On_Hold
            obj.Mode_Of_Transport = objPOHead.Mode_Of_Transport
            obj.Description = objPOHead.Description
            obj.Tax_Group = objPOHead.Tax_Group
            obj.SalesOrder_Type = objPOHead.PurchaseOrder_Type
            obj.Item_Type = objPOHead.Item_Type
            obj.Dept = objPOHead.Dept
            obj.Dept_Desc = objPOHead.Dept_Desc
            obj.PROJECT_ID = objPOHead.PROJECT_ID
            obj.Approvel_Required = 0
            If clsCommon.myLen(objPOHead.TAX1) > 0 Then
                obj.TAX1 = objPOHead.TAX1
                obj.TAX1_Rate = objPOHead.TAX1_Rate
                obj.TAX1_Base_Amt = objPOHead.TAX1_Base_Amt
                obj.TAX1_Amt = objPOHead.TAX1_Amt
            End If
            If clsCommon.myLen(objPOHead.TAX2) > 0 Then
                obj.TAX2 = objPOHead.TAX2
                obj.TAX2_Rate = objPOHead.TAX2_Rate
                obj.TAX2_Base_Amt = objPOHead.TAX2_Base_Amt
                obj.TAX2_Amt = objPOHead.TAX2_Amt
            End If
            If clsCommon.myLen(objPOHead.TAX3) > 0 Then
                obj.TAX3 = objPOHead.TAX3
                obj.TAX3_Rate = objPOHead.TAX3_Rate
                obj.TAX3_Base_Amt = objPOHead.TAX3_Base_Amt
                obj.TAX3_Amt = objPOHead.TAX3_Amt
            End If
            If clsCommon.myLen(objPOHead.TAX4) > 0 Then
                obj.TAX4 = objPOHead.TAX4
                obj.TAX4_Rate = objPOHead.TAX4_Rate
                obj.TAX4_Base_Amt = objPOHead.TAX4_Base_Amt
                obj.TAX4_Amt = objPOHead.TAX4_Amt
            End If
            If clsCommon.myLen(objPOHead.TAX5) > 0 Then
                obj.TAX5 = objPOHead.TAX1
                obj.TAX5_Rate = objPOHead.TAX5_Rate
                obj.TAX5_Base_Amt = objPOHead.TAX5_Base_Amt
                obj.TAX5_Amt = objPOHead.TAX5_Amt
            End If
            If clsCommon.myLen(objPOHead.TAX6) > 0 Then
                obj.TAX6 = objPOHead.TAX6
                obj.TAX6_Rate = objPOHead.TAX6_Rate
                obj.TAX6_Base_Amt = objPOHead.TAX6_Base_Amt
                obj.TAX6_Amt = objPOHead.TAX6_Amt
            End If
            If clsCommon.myLen(objPOHead.TAX7) > 0 Then
                obj.TAX7 = objPOHead.TAX7
                obj.TAX7_Rate = objPOHead.TAX7_Rate
                obj.TAX7_Base_Amt = objPOHead.TAX7_Base_Amt
                obj.TAX7_Amt = objPOHead.TAX7_Amt
            End If
            If clsCommon.myLen(objPOHead.TAX8) > 0 Then
                obj.TAX8 = objPOHead.TAX8
                obj.TAX8_Rate = objPOHead.TAX8_Rate
                obj.TAX8_Base_Amt = objPOHead.TAX8_Base_Amt
                obj.TAX8_Amt = objPOHead.TAX8_Amt
            End If
            If clsCommon.myLen(objPOHead.TAX9) > 0 Then
                obj.TAX9 = objPOHead.TAX9
                obj.TAX9_Rate = objPOHead.TAX9_Rate
                obj.TAX9_Base_Amt = objPOHead.TAX9_Base_Amt
                obj.TAX9_Amt = objPOHead.TAX9_Amt
            End If
            If clsCommon.myLen(objPOHead.TAX1) > 0 Then
                obj.TAX10 = objPOHead.TAX10
                obj.TAX10_Rate = objPOHead.TAX10_Rate
                obj.TAX10_Base_Amt = objPOHead.TAX10_Base_Amt
                obj.TAX10_Amt = objPOHead.TAX10_Amt
            End If

            obj.Terms_Code = objPOHead.Terms_Code
            obj.Due_Date = objPOHead.Due_Date
            obj.Discount_Base = objPOHead.Discount_Base
            obj.Discount_Amt = objPOHead.Discount_Amt
            obj.Amount_Less_Discount = objPOHead.Amount_Less_Discount
            obj.Total_Amt = objPOHead.PO_Total_Amt
            obj.Abandonment_No = objPOHead.Abandonment_No
            obj.Against_Quotation_No = ""




            If clsCommon.myLen(obj.Add_Charge_Code1) > 0 Then
                obj.Add_Charge_Code1 = objPOHead.Add_Charge_Code1
                obj.Add_Charge_Name1 = objPOHead.Add_Charge_Name1
                obj.Add_Charge_Amt1 = objPOHead.Add_Charge_Amt1
            End If
            If clsCommon.myLen(obj.Add_Charge_Code2) > 0 Then
                obj.Add_Charge_Code2 = objPOHead.Add_Charge_Code2
                obj.Add_Charge_Name2 = objPOHead.Add_Charge_Name2
                obj.Add_Charge_Amt2 = objPOHead.Add_Charge_Amt2
            End If
            If clsCommon.myLen(obj.Add_Charge_Code3) > 0 Then
                obj.Add_Charge_Code3 = objPOHead.Add_Charge_Code3
                obj.Add_Charge_Name3 = objPOHead.Add_Charge_Name3
                obj.Add_Charge_Amt3 = objPOHead.Add_Charge_Amt3
            End If
            If clsCommon.myLen(obj.Add_Charge_Code4) > 0 Then
                obj.Add_Charge_Code4 = objPOHead.Add_Charge_Code4
                obj.Add_Charge_Name4 = objPOHead.Add_Charge_Name4
                obj.Add_Charge_Amt4 = objPOHead.Add_Charge_Amt4
            End If
            If clsCommon.myLen(obj.Add_Charge_Code5) > 0 Then
                obj.Add_Charge_Code5 = objPOHead.Add_Charge_Code5
                obj.Add_Charge_Name1 = objPOHead.Add_Charge_Name5
                obj.Add_Charge_Amt5 = objPOHead.Add_Charge_Amt5
            End If
            If clsCommon.myLen(obj.Add_Charge_Code6) > 0 Then
                obj.Add_Charge_Code6 = objPOHead.Add_Charge_Code6
                obj.Add_Charge_Name6 = objPOHead.Add_Charge_Name6
                obj.Add_Charge_Amt6 = objPOHead.Add_Charge_Amt6
            End If
            If clsCommon.myLen(obj.Add_Charge_Code7) > 0 Then
                obj.Add_Charge_Code7 = objPOHead.Add_Charge_Code7
                obj.Add_Charge_Name7 = objPOHead.Add_Charge_Name7
                obj.Add_Charge_Amt7 = objPOHead.Add_Charge_Amt7
            End If
            If clsCommon.myLen(obj.Add_Charge_Code8) > 0 Then
                obj.Add_Charge_Code8 = objPOHead.Add_Charge_Code8
                obj.Add_Charge_Name8 = objPOHead.Add_Charge_Name8
                obj.Add_Charge_Amt8 = objPOHead.Add_Charge_Amt8
            End If
            If clsCommon.myLen(obj.Add_Charge_Code9) > 0 Then
                obj.Add_Charge_Code9 = objPOHead.Add_Charge_Code9
                obj.Add_Charge_Name9 = objPOHead.Add_Charge_Name9
                obj.Add_Charge_Amt9 = objPOHead.Add_Charge_Amt9
            End If
            If clsCommon.myLen(obj.Add_Charge_Code10) > 0 Then
                obj.Add_Charge_Code10 = objPOHead.Add_Charge_Code10
                obj.Add_Charge_Name10 = objPOHead.Add_Charge_Name10
                obj.Add_Charge_Amt10 = objPOHead.Add_Charge_Amt10
            End If
            obj.Total_Add_Charge = objPOHead.Total_Add_Charge

            obj.Salesman_Code = ""
            obj.Salesman_Name = ""

            obj.Arr = New List(Of clsSNSalesOrderDetail)

            For Each objPODetail As clsPurchaseOrderDetail In objPOHead.Arr

                Dim objTr As clsSNSalesOrderDetail = New clsSNSalesOrderDetail()
                objTr.Line_No = objPODetail.Line_No
                objTr.Row_Type = objPODetail.Row_Type
                objTr.Item_Code = objPODetail.Item_Code
                objTr.Item_Desc = objPODetail.Item_Desc
                objTr.Qty = objPODetail.PurchaseOrder_Qty
                objTr.Balance_Qty = objPODetail.PurchaseOrder_Qty
                objTr.Unit_code = objPODetail.Unit_code
                objTr.Quotation_Code = 0
                'objTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
                objTr.Item_Cost = objPODetail.Item_Cost
                objTr.Amount = objPODetail.Amount
                objTr.Disc_Per = objPODetail.Disc_Per
                objTr.Disc_Amt = objPODetail.Disc_Amt
                objTr.Amt_Less_Discount = objPODetail.Amt_Less_Discount
                objTr.TAX1 = objPODetail.TAX1
                objTr.TAX1_Base_Amt = objPODetail.TAX1_Base_Amt
                objTr.TAX1_Rate = objPODetail.TAX1_Rate
                objTr.TAX1_Amt = objPODetail.TAX1_Amt
                objTr.TAX2 = objPODetail.TAX2
                objTr.TAX2_Base_Amt = objPODetail.TAX2_Base_Amt
                objTr.TAX2_Rate = objPODetail.TAX2_Rate
                objTr.TAX2_Amt = objPODetail.TAX2_Amt
                objTr.TAX3 = objPODetail.TAX3
                objTr.TAX3_Base_Amt = objPODetail.TAX3_Base_Amt
                objTr.TAX3_Rate = objPODetail.TAX3_Rate
                objTr.TAX3_Amt = objPODetail.TAX3_Amt
                objTr.TAX4 = objPODetail.TAX4
                objTr.TAX4_Base_Amt = objPODetail.TAX4_Base_Amt
                objTr.TAX4_Rate = objPODetail.TAX4_Rate
                objTr.TAX4_Amt = objPODetail.TAX4_Amt
                objTr.TAX5 = objPODetail.TAX5
                objTr.TAX5_Base_Amt = objPODetail.TAX5_Base_Amt
                objTr.TAX5_Rate = objPODetail.TAX5_Rate
                objTr.TAX5_Amt = objPODetail.TAX5_Amt
                objTr.TAX6 = objPODetail.TAX6
                objTr.TAX6_Base_Amt = objPODetail.TAX6_Base_Amt
                objTr.TAX6_Rate = objPODetail.TAX6_Rate
                objTr.TAX6_Amt = objPODetail.TAX6_Amt
                objTr.TAX7 = objPODetail.TAX7
                objTr.TAX7_Base_Amt = objPODetail.TAX7_Base_Amt
                objTr.TAX7_Rate = objPODetail.TAX7_Rate
                objTr.TAX7_Amt = objPODetail.TAX7_Amt
                objTr.TAX8 = objPODetail.TAX8
                objTr.TAX8_Base_Amt = objPODetail.TAX8_Base_Amt
                objTr.TAX8_Rate = objPODetail.TAX8_Rate
                objTr.TAX8_Amt = objPODetail.TAX8_Amt
                objTr.TAX9 = objPODetail.TAX9
                objTr.TAX9_Base_Amt = objPODetail.TAX9_Base_Amt
                objTr.TAX9_Rate = objPODetail.TAX9_Rate
                objTr.TAX9_Amt = objPODetail.TAX9_Amt
                objTr.TAX10 = objPODetail.TAX10
                objTr.TAX10_Base_Amt = objPODetail.TAX10_Base_Amt
                objTr.TAX10_Rate = objPODetail.TAX10_Rate
                objTr.TAX10_Amt = objPODetail.TAX10_Amt
                objTr.Total_Tax_Amt = objPODetail.Total_Tax_Amt
                objTr.Item_Net_Amt = objPODetail.Item_Net_Amt
                objTr.Specification = objPODetail.Specification
                objTr.Remarks = objPODetail.Remarks
                objTr.Location = objPODetail.Location 'clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                objTr.MRP = objPODetail.MRP
                objTr.Scheme_Applicable = "N"
                objTr.Scheme_Code = ""
                objTr.Scheme_Item = "N"
                objTr.Item_Tax = objPODetail.Total_Tax_Amt
                objTr.Total_MRP_Amt = 0
                objTr.Total_Basic_Amt = 0
                objTr.Total_Disc_Amt = objPODetail.Disc_Amt
                objTr.Cust_Discount = 0
                objTr.Total_Cust_Discount = 0
                objTr.ActualRate = objPODetail.Item_Cost
                objTr.Cust_DiscountQty = 0

                objTr.Price_code = ""
                objTr.Abatement_Per = objPODetail.Assessable
                objTr.Abatement_Amt = objPODetail.AssessableAmt
                objTr.FOC_Item = 0

                objTr.Item_Weight = 0
                objTr.Conv_Factor = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & clsCommon.myCstr(objPODetail.Item_Code) & "' and UOM_Code='" & objPODetail.Unit_code & "' and Stocking_Unit='Y'", trans))
                objTr.TotalItem_Weight = 0
                objTr.Batch_No = ""
                objTr.Bin_No = ""
                objTr.HeadDiscPer = 0
                objTr.HeadDiscPerAmt = 0
                'If clsCommon.myLen(grow.Cells(colExpiry).Value) > 0 Then
                '    objTr.Expiry_Date = clsCommon.myCDate(grow.Cells(colExpiry).Value, "dd-MM-yyyy")
                'End If
                'If clsCommon.myLen(grow.Cells(colManufactureDate).Value) > 0 Then
                '    objTr.MFG_Date = clsCommon.myCDate(grow.Cells(colManufactureDate).Value)
                'End If
                objTr.Markup_On = ""
                objTr.Markup_Percent = 0
                objTr.Landing_Cost = 0
                objTr.CustDiscPer = 0
                objTr.HeadDiscAmt = 0
                objTr.CasdDiscScheme_Code = ""
                objTr.Purchase_Cost = 0
                objTr.OrgRate = objPODetail.Item_Cost
                objTr.PrincipleCode = ""
                objTr.PrincipleDesc = ""
                objTr.vendor_code = ""
                objTr.vendor_desc = ""

                objTr.HeadDiscPer = 0
                objTr.HeadDiscPerAmt = 0
                ''objTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
                ''objTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
                If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next
            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                Return False
            End If
            ''For Custom Fields


            ''End of For Custom Fields

            '' CurrencConversion

            Dim isSaved As Boolean = obj.DemoSaveData(obj, True, isDoAbandomentNo, strDatabasename, trans)
            If isSaved Then
                'clsCommon.MyMessageBoxShow("Sale Order created successfully   " + obj.Document_Code + " ")
            End If
            Return isSaved



        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
    End Function

    ''''' ends here
    ''Create GRN and MRN When its setting is On
    'If Not clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal Then
    '    qry = "select IsAutoCreateGRNAndMRN from TSPL_INV_PARAMETERS"
    '    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) = 1 Then
    '        Dim strGRNNo As String = ""
    '        Dim strMRNNo As String = ""
    '        qry = "select MRN_No,Against_GRN from TSPL_MRN_HEAD where Against_PO='" + obj.PurchaseOrder_No + "'"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            strGRNNo = clsCommon.myCstr(dt.Rows(0)("Against_GRN"))
    '            strMRNNo = clsCommon.myCstr(dt.Rows(0)("MRN_No"))
    '        End If


    '        Dim objGRN As New clsGRNHead()
    '        objGRN.GRN_No = strGRNNo
    '        objGRN.GRN_Date = obj.PurchaseOrder_Date
    '        objGRN.Vendor_Code = obj.Vendor_Code
    '        objGRN.Vendor_Name = obj.Vendor_Name
    '        objGRN.Ref_No = obj.Ref_No
    '        objGRN.Total_Tax_Amt = obj.Total_Tax_Amt
    '        objGRN.Remarks = obj.Remarks
    '        objGRN.Bill_To_Location = obj.Bill_To_Location
    '        objGRN.Ship_To_Location = obj.Ship_To_Location
    '        objGRN.Comments = obj.Comments
    '        objGRN.On_Hold = obj.On_Hold
    '        objGRN.Description = obj.Description
    '        objGRN.Tax_Group = obj.Tax_Group

    '        objGRN.Item_Type = obj.Item_Type
    '        objGRN.Dept = obj.Dept
    '        objGRN.Dept_Desc = obj.Dept_Desc

    '        If clsCommon.myLen(obj.TAX1) > 0 Then
    '            objGRN.TAX1 = obj.TAX1
    '            objGRN.TAX1_Rate = obj.TAX1_Rate
    '            objGRN.TAX1_Base_Amt = obj.TAX1_Base_Amt
    '            objGRN.TAX1_Amt = obj.TAX1_Amt
    '        End If
    '        If clsCommon.myLen(obj.TAX2) > 0 Then
    '            objGRN.TAX2 = obj.TAX2
    '            objGRN.TAX2_Rate = obj.TAX2_Rate
    '            objGRN.TAX2_Base_Amt = obj.TAX2_Base_Amt
    '            objGRN.TAX2_Amt = obj.TAX2_Amt
    '        End If
    '        If clsCommon.myLen(obj.TAX3) > 0 Then
    '            objGRN.TAX3 = obj.TAX3
    '            objGRN.TAX3_Rate = obj.TAX3_Rate
    '            objGRN.TAX3_Base_Amt = obj.TAX3_Base_Amt
    '            objGRN.TAX3_Amt = obj.TAX3_Amt
    '        End If
    '        If clsCommon.myLen(obj.TAX4) > 0 Then
    '            objGRN.TAX4 = obj.TAX4
    '            objGRN.TAX4_Rate = obj.TAX4_Rate
    '            objGRN.TAX4_Base_Amt = obj.TAX4_Base_Amt
    '            objGRN.TAX4_Amt = obj.TAX4_Amt
    '        End If
    '        If clsCommon.myLen(obj.TAX5) > 0 Then
    '            objGRN.TAX5 = obj.TAX5
    '            objGRN.TAX5_Rate = obj.TAX5_Rate
    '            objGRN.TAX5_Base_Amt = obj.TAX5_Base_Amt
    '            objGRN.TAX5_Amt = obj.TAX5_Amt
    '        End If
    '        If clsCommon.myLen(obj.TAX6) > 0 Then
    '            objGRN.TAX6 = obj.TAX6
    '            objGRN.TAX6_Rate = obj.TAX6_Rate
    '            objGRN.TAX6_Base_Amt = obj.TAX6_Base_Amt
    '            objGRN.TAX6_Amt = obj.TAX6_Amt
    '        End If
    '        If clsCommon.myLen(obj.TAX7) > 0 Then
    '            objGRN.TAX7 = obj.TAX7
    '            objGRN.TAX7_Rate = obj.TAX7_Rate
    '            objGRN.TAX7_Base_Amt = obj.TAX7_Base_Amt
    '            objGRN.TAX7_Amt = obj.TAX7_Amt
    '        End If
    '        If clsCommon.myLen(obj.TAX8) > 0 Then
    '            objGRN.TAX8 = obj.TAX8
    '            objGRN.TAX8_Rate = obj.TAX8_Rate
    '            objGRN.TAX8_Base_Amt = obj.TAX8_Base_Amt
    '            objGRN.TAX8_Amt = obj.TAX8_Amt
    '        End If
    '        If clsCommon.myLen(obj.TAX9) > 0 Then
    '            objGRN.TAX9 = obj.TAX9
    '            objGRN.TAX9_Rate = obj.TAX9_Rate
    '            objGRN.TAX9_Base_Amt = obj.TAX9_Base_Amt
    '            objGRN.TAX9_Amt = obj.TAX9_Amt
    '        End If
    '        If clsCommon.myLen(obj.TAX10) > 0 Then
    '            objGRN.TAX10 = obj.TAX10
    '            objGRN.TAX10_Rate = obj.TAX10_Rate
    '            objGRN.TAX10_Base_Amt = obj.TAX10_Base_Amt
    '            objGRN.TAX10_Amt = obj.TAX10_Amt
    '        End If

    '        objGRN.Terms_Code = obj.Terms_Code
    '        objGRN.Due_Date = obj.Due_Date
    '        objGRN.Discount_Base = obj.Discount_Base
    '        objGRN.Discount_Amt = obj.Discount_Amt
    '        objGRN.Amount_Less_Discount = obj.Amount_Less_Discount
    '        objGRN.GRN_Total_Amt = obj.PO_Total_Amt

    '        'objGRN.Carrier = obj.Carrier
    '        'objGRN.VehicleNo = obj.VehicleNo
    '        'objGRN.GRNo = obj.GRNo
    '        'objGRN.GENo = obj.GENo
    '        'If txtGEDate.Checked Then
    '        '    objGRN.GEDate = obj.GEDate
    '        'End If

    '        objGRN.Against_PO = obj.PurchaseOrder_No

    '        objGRN.Against_Requisition = obj.Against_Requisition





    '        If clsCommon.myLen(obj.Add_Charge_Code1) > 0 Then
    '            objGRN.Add_Charge_Code1 = obj.Add_Charge_Code1
    '            objGRN.Add_Charge_Name1 = obj.Add_Charge_Name1
    '            objGRN.Add_Charge_Amt1 = obj.Add_Charge_Amt1
    '        End If


    '        If clsCommon.myLen(obj.Add_Charge_Code2) > 0 Then
    '            objGRN.Add_Charge_Code2 = obj.Add_Charge_Code2
    '            objGRN.Add_Charge_Name2 = obj.Add_Charge_Name2
    '            objGRN.Add_Charge_Amt2 = obj.Add_Charge_Amt2
    '        End If


    '        If clsCommon.myLen(obj.Add_Charge_Code3) > 0 Then
    '            objGRN.Add_Charge_Code3 = obj.Add_Charge_Code3
    '            objGRN.Add_Charge_Name3 = obj.Add_Charge_Name3
    '            objGRN.Add_Charge_Amt3 = obj.Add_Charge_Amt3
    '        End If

    '        If clsCommon.myLen(obj.Add_Charge_Code4) > 0 Then
    '            objGRN.Add_Charge_Code4 = obj.Add_Charge_Code4
    '            objGRN.Add_Charge_Name4 = obj.Add_Charge_Name4
    '            objGRN.Add_Charge_Amt4 = obj.Add_Charge_Amt4
    '        End If

    '        If clsCommon.myLen(obj.Add_Charge_Code5) > 0 Then
    '            objGRN.Add_Charge_Code5 = obj.Add_Charge_Code5
    '            objGRN.Add_Charge_Name5 = obj.Add_Charge_Name5
    '            objGRN.Add_Charge_Amt5 = obj.Add_Charge_Amt5
    '        End If

    '        If clsCommon.myLen(obj.Add_Charge_Code6) > 0 Then
    '            objGRN.Add_Charge_Code6 = obj.Add_Charge_Code6
    '            objGRN.Add_Charge_Name6 = obj.Add_Charge_Name6
    '            objGRN.Add_Charge_Amt6 = obj.Add_Charge_Amt6
    '        End If

    '        If clsCommon.myLen(obj.Add_Charge_Code7) > 0 Then
    '            objGRN.Add_Charge_Code7 = obj.Add_Charge_Code7
    '            objGRN.Add_Charge_Name7 = obj.Add_Charge_Name7
    '            objGRN.Add_Charge_Amt7 = obj.Add_Charge_Amt7
    '        End If

    '        If clsCommon.myLen(obj.Add_Charge_Code8) > 0 Then
    '            objGRN.Add_Charge_Code8 = obj.Add_Charge_Code8
    '            objGRN.Add_Charge_Name8 = obj.Add_Charge_Name8
    '            objGRN.Add_Charge_Amt8 = obj.Add_Charge_Amt8
    '        End If

    '        If clsCommon.myLen(obj.Add_Charge_Code9) > 0 Then
    '            objGRN.Add_Charge_Code9 = obj.Add_Charge_Code9
    '            objGRN.Add_Charge_Name9 = obj.Add_Charge_Name9
    '            objGRN.Add_Charge_Amt9 = obj.Add_Charge_Amt9
    '        End If

    '        If clsCommon.myLen(obj.Add_Charge_Code10) > 0 Then
    '            objGRN.Add_Charge_Code10 = obj.Add_Charge_Code10
    '            objGRN.Add_Charge_Name10 = obj.Add_Charge_Name10
    '            objGRN.Add_Charge_Amt10 = obj.Add_Charge_Amt10
    '        End If

    '        objGRN.Total_Add_Charge = obj.Total_Add_Charge

    '        objGRN.Arr = New List(Of clsGRNDetail)
    '        For Each objTr As clsPurchaseOrderDetail In obj.Arr
    '            Dim objGRNTr As New clsGRNDetail()
    '            objGRNTr.Line_No = objTr.Line_No
    '            objGRNTr.Row_Type = objTr.Row_Type
    '            objGRNTr.Item_Code = objTr.Item_Code
    '            objGRNTr.Item_Desc = objTr.Item_Desc
    '            objGRNTr.GRN_Qty = objTr.PurchaseOrder_Qty

    '            objGRNTr.Unit_code = objTr.Unit_code
    '            objGRNTr.PO_Id = objTr.PurchaseOrder_No
    '            'objGRNTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
    '            objGRNTr.Item_Cost = objTr.Item_Cost
    '            objGRNTr.Amount = objTr.Amount
    '            objGRNTr.Disc_Per = objTr.Disc_Per
    '            objGRNTr.Disc_Amt = objTr.Disc_Amt
    '            objGRNTr.Amt_Less_Discount = objTr.Amt_Less_Discount
    '            objGRNTr.TAX1 = objTr.TAX1
    '            objGRNTr.TAX1_Base_Amt = objTr.TAX1_Base_Amt
    '            objGRNTr.TAX1_Rate = objTr.TAX1_Rate
    '            objGRNTr.TAX1_Amt = objTr.TAX1_Amt
    '            objGRNTr.TAX2 = objTr.TAX2
    '            objGRNTr.TAX2_Base_Amt = objTr.TAX2_Base_Amt
    '            objGRNTr.TAX2_Rate = objTr.TAX2_Rate
    '            objGRNTr.TAX2_Amt = objTr.TAX2_Amt
    '            objGRNTr.TAX3 = objTr.TAX3
    '            objGRNTr.TAX3_Base_Amt = objTr.TAX3_Base_Amt
    '            objGRNTr.TAX3_Rate = objTr.TAX3_Rate
    '            objGRNTr.TAX3_Amt = objTr.TAX3_Amt
    '            objGRNTr.TAX4 = objTr.TAX4
    '            objGRNTr.TAX4_Base_Amt = objTr.TAX4_Base_Amt
    '            objGRNTr.TAX4_Rate = objTr.TAX4_Rate
    '            objGRNTr.TAX4_Amt = objTr.TAX4_Amt
    '            objGRNTr.TAX5 = objTr.TAX5
    '            objGRNTr.TAX5_Base_Amt = objTr.TAX5_Base_Amt
    '            objGRNTr.TAX5_Rate = objTr.TAX5_Rate
    '            objGRNTr.TAX5_Amt = objTr.TAX5_Amt
    '            objGRNTr.TAX6 = objTr.TAX6
    '            objGRNTr.TAX6_Base_Amt = objTr.TAX6_Base_Amt
    '            objGRNTr.TAX6_Rate = objTr.TAX6_Rate
    '            objGRNTr.TAX6_Amt = objTr.TAX6_Amt
    '            objGRNTr.TAX7 = objTr.TAX7
    '            objGRNTr.TAX7_Base_Amt = objTr.TAX7_Base_Amt
    '            objGRNTr.TAX7_Rate = objTr.TAX7_Rate
    '            objGRNTr.TAX7_Amt = objTr.TAX7_Amt
    '            objGRNTr.TAX8 = objTr.TAX8
    '            objGRNTr.TAX8_Base_Amt = objTr.TAX8_Base_Amt
    '            objGRNTr.TAX8_Rate = objTr.TAX8_Rate
    '            objGRNTr.TAX8_Amt = objTr.TAX8_Amt
    '            objGRNTr.TAX9 = objTr.TAX9
    '            objGRNTr.TAX9_Base_Amt = objTr.TAX9_Base_Amt
    '            objGRNTr.TAX9_Rate = objTr.TAX9_Rate
    '            objGRNTr.TAX9_Amt = objTr.TAX9_Amt
    '            objGRNTr.TAX10 = objTr.TAX10
    '            objGRNTr.TAX10_Base_Amt = objTr.TAX10_Base_Amt
    '            objGRNTr.TAX10_Rate = objTr.TAX10_Rate
    '            objGRNTr.TAX10_Amt = objTr.TAX10_Amt
    '            objGRNTr.Total_Tax_Amt = objTr.Total_Tax_Amt
    '            objGRNTr.Item_Net_Amt = objTr.Item_Net_Amt
    '            objGRNTr.Location = objTr.Location

    '            objGRNTr.MRP = objTr.MRP
    '            ''objGRNTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
    '            ''objGRNTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
    '            'objGRNTr.Batch_No = objTr.Batch_No

    '            objGRNTr.Specification = objTr.Specification
    '            objGRNTr.Remarks = objTr.Remarks

    '            'If clsCommon.myLen(grow.Cells(colExpiry).Value) > 0 Then
    '            '    objGRNTr.Expiry_Date = objTr.Expiry_Date
    '            'End If
    '            'If clsCommon.myLen(grow.Cells(colManufactureDate).Value) > 0 Then
    '            '    objGRNTr.MFG_Date = objTr.MFG_Date
    '            'End If
    '            'objGRNTr.Leak_Qty = objTr.Leak_Qty
    '            'objGRNTr.Burst_Qty = objTr.Burst_Qty
    '            'objGRNTr.Short_Qty = objTr.Short_Qty
    '            objGRNTr.Balance_Qty = objTr.Balance_Qty
    '            If (clsCommon.myLen(objGRNTr.Item_Code) > 0) Then
    '                objGRN.Arr.Add(objGRNTr)
    '            End If
    '        Next
    '        objGRN.SaveData(objGRN, IIf(clsCommon.myLen(strGRNNo) > 0, False, True), trans)
    '        clsGRNHead.PostData(objGRN.GRN_No, isCheckForPosted, trans)
    '        objGRN = objGRN.GetData(objGRN.GRN_No, NavigatorType.Current, trans)







    '        Dim objMRN As New clsMRNHead()
    '        objMRN.MRN_No = strMRNNo
    '        objMRN.MRN_Date = objGRN.GRN_Date
    '        objMRN.Vendor_Code = objGRN.Vendor_Code
    '        objMRN.Vendor_Name = objGRN.Vendor_Name
    '        objMRN.Ref_No = objGRN.Ref_No
    '        objMRN.Total_Tax_Amt = objGRN.Total_Tax_Amt
    '        objMRN.Remarks = objGRN.Remarks
    '        objMRN.Bill_To_Location = objGRN.Bill_To_Location
    '        objMRN.Ship_To_Location = objGRN.Ship_To_Location
    '        objMRN.Comments = objGRN.Comments
    '        objMRN.On_Hold = objGRN.On_Hold
    '        objMRN.Description = objGRN.Description
    '        objMRN.Tax_Group = objGRN.Tax_Group


    '        If clsCommon.myLen(objGRN.TAX1) > 0 Then
    '            objMRN.TAX1 = objGRN.TAX1
    '            objMRN.TAX1_Rate = objGRN.TAX1_Rate
    '            objMRN.TAX1_Base_Amt = objGRN.TAX1_Base_Amt
    '            objMRN.TAX1_Amt = objGRN.TAX1_Amt
    '        End If
    '        If clsCommon.myLen(objGRN.TAX2) > 0 Then
    '            objMRN.TAX2 = objGRN.TAX2
    '            objMRN.TAX2_Rate = objGRN.TAX2_Rate
    '            objMRN.TAX2_Base_Amt = objGRN.TAX2_Base_Amt
    '            objMRN.TAX2_Amt = objGRN.TAX2_Amt
    '        End If
    '        If clsCommon.myLen(objGRN.TAX3) > 0 Then
    '            objMRN.TAX3 = objGRN.TAX3
    '            objMRN.TAX3_Rate = objGRN.TAX3_Rate
    '            objMRN.TAX3_Base_Amt = objGRN.TAX3_Base_Amt
    '            objMRN.TAX3_Amt = objGRN.TAX3_Amt
    '        End If
    '        If clsCommon.myLen(objGRN.TAX4) > 0 Then
    '            objMRN.TAX4 = objGRN.TAX4
    '            objMRN.TAX4_Rate = objGRN.TAX4_Rate
    '            objMRN.TAX4_Base_Amt = objGRN.TAX4_Base_Amt
    '            objMRN.TAX4_Amt = objGRN.TAX4_Amt
    '        End If
    '        If clsCommon.myLen(objGRN.TAX5) > 0 Then
    '            objMRN.TAX5 = objGRN.TAX5
    '            objMRN.TAX5_Rate = objGRN.TAX5_Rate
    '            objMRN.TAX5_Base_Amt = objGRN.TAX5_Base_Amt
    '            objMRN.TAX5_Amt = objGRN.TAX5_Amt
    '        End If
    '        If clsCommon.myLen(objGRN.TAX6) > 0 Then
    '            objMRN.TAX6 = objGRN.TAX6
    '            objMRN.TAX6_Rate = objGRN.TAX6_Rate
    '            objMRN.TAX6_Base_Amt = objGRN.TAX6_Base_Amt
    '            objMRN.TAX6_Amt = objGRN.TAX6_Amt
    '        End If
    '        If clsCommon.myLen(objGRN.TAX7) > 0 Then
    '            objMRN.TAX7 = objGRN.TAX7
    '            objMRN.TAX7_Rate = objGRN.TAX7_Rate
    '            objMRN.TAX7_Base_Amt = objGRN.TAX7_Base_Amt
    '            objMRN.TAX7_Amt = objGRN.TAX7_Amt
    '        End If
    '        If clsCommon.myLen(objGRN.TAX8) > 0 Then
    '            objMRN.TAX8 = objGRN.TAX8
    '            objMRN.TAX8_Rate = objGRN.TAX8_Rate
    '            objMRN.TAX8_Base_Amt = objGRN.TAX8_Base_Amt
    '            objMRN.TAX8_Amt = objGRN.TAX8_Amt
    '        End If
    '        If clsCommon.myLen(objGRN.TAX9) > 0 Then
    '            objMRN.TAX9 = objGRN.TAX9
    '            objMRN.TAX9_Rate = objGRN.TAX9_Rate
    '            objMRN.TAX9_Base_Amt = objGRN.TAX9_Base_Amt
    '            objMRN.TAX9_Amt = objGRN.TAX9_Amt
    '        End If
    '        If clsCommon.myLen(objGRN.TAX10) > 0 Then
    '            objMRN.TAX10 = objGRN.TAX10
    '            objMRN.TAX10_Rate = objGRN.TAX10_Rate
    '            objMRN.TAX10_Base_Amt = objGRN.TAX10_Base_Amt
    '            objMRN.TAX10_Amt = objGRN.TAX10_Amt
    '        End If

    '        objMRN.Terms_Code = objGRN.Terms_Code
    '        objMRN.Due_Date = objGRN.Due_Date
    '        objMRN.Discount_Base = objGRN.Discount_Base
    '        objMRN.Discount_Amt = objGRN.Discount_Amt
    '        objMRN.Amount_Less_Discount = objGRN.Amount_Less_Discount
    '        objMRN.MRN_Total_Amt = objGRN.GRN_Total_Amt

    '        objMRN.Carrier = objGRN.Carrier
    '        objMRN.VehicleNo = objGRN.VehicleNo
    '        objMRN.GRNo = objGRN.GRNo
    '        objMRN.GENo = objGRN.GENo

    '        objMRN.GEDate = objGRN.GEDate
    '        objMRN.Item_Type = objGRN.Item_Type
    '        objMRN.Dept = objGRN.Dept
    '        objMRN.Dept_Desc = objGRN.Dept_Desc

    '        objMRN.Against_GRN = objGRN.GRN_No
    '        If clsCommon.myLen(objMRN.Against_GRN) > 0 Then
    '            objMRN.Against_PO = objGRN.Against_PO
    '        End If
    '        If clsCommon.myLen(objMRN.Against_PO) > 0 Then
    '            objMRN.Against_Requisition = objGRN.Against_Requisition
    '        End If
    '        If clsCommon.myLen(objGRN.Add_Charge_Code1) > 0 Then
    '            objMRN.Add_Charge_Code1 = objGRN.Add_Charge_Code1
    '            objMRN.Add_Charge_Name1 = objGRN.Add_Charge_Name1
    '            objMRN.Add_Charge_Amt1 = objGRN.Add_Charge_Amt1
    '        End If
    '        If clsCommon.myLen(objGRN.Add_Charge_Code2) > 0 Then
    '            objMRN.Add_Charge_Code2 = objGRN.Add_Charge_Code2
    '            objMRN.Add_Charge_Name2 = objGRN.Add_Charge_Name2
    '            objMRN.Add_Charge_Amt2 = objGRN.Add_Charge_Amt2
    '        End If
    '        If clsCommon.myLen(objGRN.Add_Charge_Code3) > 0 Then
    '            objMRN.Add_Charge_Code3 = objGRN.Add_Charge_Code3
    '            objMRN.Add_Charge_Name3 = objGRN.Add_Charge_Name3
    '            objMRN.Add_Charge_Amt3 = objGRN.Add_Charge_Amt3
    '        End If
    '        If clsCommon.myLen(objGRN.Add_Charge_Code4) > 0 Then
    '            objMRN.Add_Charge_Code4 = objGRN.Add_Charge_Code4
    '            objMRN.Add_Charge_Name4 = objGRN.Add_Charge_Name4
    '            objMRN.Add_Charge_Amt4 = objGRN.Add_Charge_Amt4
    '        End If
    '        If clsCommon.myLen(objGRN.Add_Charge_Code5) > 0 Then
    '            objMRN.Add_Charge_Code5 = objGRN.Add_Charge_Code5
    '            objMRN.Add_Charge_Name5 = objGRN.Add_Charge_Name5
    '            objMRN.Add_Charge_Amt5 = objGRN.Add_Charge_Amt5
    '        End If
    '        If clsCommon.myLen(objGRN.Add_Charge_Code6) > 0 Then
    '            objMRN.Add_Charge_Code6 = objGRN.Add_Charge_Code6
    '            objMRN.Add_Charge_Name6 = objGRN.Add_Charge_Name6
    '            objMRN.Add_Charge_Amt6 = objGRN.Add_Charge_Amt6
    '        End If
    '        If clsCommon.myLen(objGRN.Add_Charge_Code7) > 0 Then
    '            objMRN.Add_Charge_Code7 = objGRN.Add_Charge_Code7
    '            objMRN.Add_Charge_Name7 = objGRN.Add_Charge_Name7
    '            objMRN.Add_Charge_Amt7 = objGRN.Add_Charge_Amt7
    '        End If
    '        If clsCommon.myLen(objGRN.Add_Charge_Code8) > 0 Then
    '            objMRN.Add_Charge_Code8 = objGRN.Add_Charge_Code8
    '            objMRN.Add_Charge_Name8 = objGRN.Add_Charge_Name8
    '            objMRN.Add_Charge_Amt8 = objGRN.Add_Charge_Amt8
    '        End If
    '        If clsCommon.myLen(objGRN.Add_Charge_Code9) > 0 Then
    '            objMRN.Add_Charge_Code9 = objGRN.Add_Charge_Code9
    '            objMRN.Add_Charge_Name9 = objGRN.Add_Charge_Name9
    '            objMRN.Add_Charge_Amt9 = objGRN.Add_Charge_Amt9
    '        End If
    '        If clsCommon.myLen(objGRN.Add_Charge_Code10) > 0 Then
    '            objMRN.Add_Charge_Code10 = objGRN.Add_Charge_Code10
    '            objMRN.Add_Charge_Name10 = objGRN.Add_Charge_Name10
    '            objMRN.Add_Charge_Amt10 = objGRN.Add_Charge_Amt10
    '        End If
    '        objMRN.Total_Add_Charge = objGRN.Total_Add_Charge
    '        objMRN.Arr = New List(Of clsMRNDetail)
    '        For Each objGRNTr As clsGRNDetail In objGRN.Arr
    '            Dim objMRNTr As New clsMRNDetail()
    '            objMRNTr.Line_No = objGRNTr.Line_No
    '            objMRNTr.Row_Type = objGRNTr.Row_Type
    '            objMRNTr.Item_Code = objGRNTr.Item_Code
    '            objMRNTr.Item_Desc = objGRNTr.Item_Desc
    '            objMRNTr.MRN_Qty = objGRNTr.GRN_Qty
    '            objMRNTr.Leak_Qty = objGRNTr.Leak_Qty
    '            objMRNTr.Burst_Qty = objGRNTr.Burst_Qty
    '            objMRNTr.Short_Qty = objGRNTr.Short_Qty
    '            objMRNTr.Excess_Qty = 0
    '            objMRNTr.Balance_Qty = objGRNTr.Balance_Qty
    '            objMRNTr.Unit_code = objGRNTr.Unit_code
    '            objMRNTr.GRN_Id = objGRNTr.GRN_No
    '            'objMRNTr.Location = clsCommon.myCstr(grow.Cells(colloc).Value)
    '            objMRNTr.Item_Cost = objGRNTr.Item_Cost
    '            objMRNTr.Amount = objGRNTr.Amount
    '            objMRNTr.Disc_Per = objGRNTr.Disc_Per
    '            objMRNTr.Disc_Amt = objGRNTr.Disc_Amt
    '            objMRNTr.Amt_Less_Discount = objGRNTr.Amt_Less_Discount
    '            objMRNTr.TAX1 = objGRNTr.TAX1
    '            objMRNTr.TAX1_Base_Amt = objGRNTr.TAX1_Base_Amt
    '            objMRNTr.TAX1_Rate = objGRNTr.TAX1_Rate
    '            objMRNTr.TAX1_Amt = objGRNTr.TAX1_Amt
    '            objMRNTr.TAX2 = objGRNTr.TAX2
    '            objMRNTr.TAX2_Base_Amt = objGRNTr.TAX2_Base_Amt
    '            objMRNTr.TAX2_Rate = objGRNTr.TAX2_Rate
    '            objMRNTr.TAX2_Amt = objGRNTr.TAX2_Amt
    '            objMRNTr.TAX3 = objGRNTr.TAX3
    '            objMRNTr.TAX3_Base_Amt = objGRNTr.TAX3_Base_Amt
    '            objMRNTr.TAX3_Rate = objGRNTr.TAX3_Rate
    '            objMRNTr.TAX3_Amt = objGRNTr.TAX3_Amt
    '            objMRNTr.TAX4 = objGRNTr.TAX4
    '            objMRNTr.TAX4_Base_Amt = objGRNTr.TAX4_Base_Amt
    '            objMRNTr.TAX4_Rate = objGRNTr.TAX4_Rate
    '            objMRNTr.TAX4_Amt = objGRNTr.TAX4_Amt
    '            objMRNTr.TAX5 = objGRNTr.TAX5
    '            objMRNTr.TAX5_Base_Amt = objGRNTr.TAX5_Base_Amt
    '            objMRNTr.TAX5_Rate = objGRNTr.TAX5_Rate
    '            objMRNTr.TAX5_Amt = objGRNTr.TAX5_Amt
    '            objMRNTr.TAX6 = objGRNTr.TAX6
    '            objMRNTr.TAX6_Base_Amt = objGRNTr.TAX6_Base_Amt
    '            objMRNTr.TAX6_Rate = objGRNTr.TAX6_Rate
    '            objMRNTr.TAX6_Amt = objGRNTr.TAX6_Amt
    '            objMRNTr.TAX7 = objGRNTr.TAX7
    '            objMRNTr.TAX7_Base_Amt = objGRNTr.TAX7_Base_Amt
    '            objMRNTr.TAX7_Rate = objGRNTr.TAX7_Rate
    '            objMRNTr.TAX7_Amt = objGRNTr.TAX7_Amt
    '            objMRNTr.TAX8 = objGRNTr.TAX8
    '            objMRNTr.TAX8_Base_Amt = objGRNTr.TAX8_Base_Amt
    '            objMRNTr.TAX8_Rate = objGRNTr.TAX8_Rate
    '            objMRNTr.TAX8_Amt = objGRNTr.TAX8_Amt
    '            objMRNTr.TAX9 = objGRNTr.TAX9
    '            objMRNTr.TAX9_Base_Amt = objGRNTr.TAX9_Base_Amt
    '            objMRNTr.TAX9_Rate = objGRNTr.TAX9_Rate
    '            objMRNTr.TAX9_Amt = objGRNTr.TAX9_Amt
    '            objMRNTr.TAX10 = objGRNTr.TAX10
    '            objMRNTr.TAX10_Base_Amt = objGRNTr.TAX10_Base_Amt
    '            objMRNTr.TAX10_Rate = objGRNTr.TAX10_Rate
    '            objMRNTr.TAX10_Amt = objGRNTr.TAX10_Amt
    '            objMRNTr.Total_Tax_Amt = objGRNTr.Total_Tax_Amt
    '            objMRNTr.Item_Net_Amt = objGRNTr.Item_Net_Amt
    '            objMRNTr.Location = objGRNTr.Location

    '            objMRNTr.MRP = objGRNTr.MRP
    '            ''objMRNTr.Assessable = clsCommon.myCdbl(grow.Cells(colAssessableRate).Value)
    '            ''objMRNTr.AssessableAmt = clsCommon.myCdbl(grow.Cells(colAssessableAmount).Value)
    '            objMRNTr.Batch_No = objGRNTr.Batch_No
    '            objMRNTr.Specification = objGRNTr.Specification
    '            objMRNTr.Remarks = objGRNTr.Remarks
    '            objMRNTr.Expiry_Date = objGRNTr.Expiry_Date
    '            objMRNTr.MFG_Date = objGRNTr.MFG_Date
    '            If (clsCommon.myLen(objMRNTr.Item_Code) > 0) Then
    '                objMRN.Arr.Add(objMRNTr)
    '            End If
    '        Next
    '        objMRN.SaveData(objMRN, IIf(clsCommon.myLen(strMRNNo) > 0, False, True), trans)
    '        clsMRNHead.PostData(objMRN.MRN_No, isCheckForPosted, trans)
    '    End If
    'End If

    Public Shared Function CancelPOData(ByVal strCode As String, ByVal isMerchantTrade As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            CancelData(strCode, isMerchantTrade, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CancelData(ByVal strCode As String, ByVal isMerchantTrade As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsPurchaseOrderHead = clsPurchaseOrderHead.GetData(strCode, NavigatorType.Current, "", trans, isMerchantTrade)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PurchaseOrder_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Order", obj.Bill_To_Location, obj.PurchaseOrder_Date, trans)
                CancleUpdate(obj.PurchaseOrder_No, trans)
                clsPurchaseOrderAdditionChargeInsurance.DeleteData(obj.PurchaseOrder_No, trans)

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PURCHASE_ORDER_HEAD", "PurchaseOrder_No", "TSPL_PURCHASE_ORDER_DETAIL", "PurchaseOrder_No", "TSPL_PI_REMITTANCE", "Document_No", trans)

                Dim qry As String = "delete from TSPL_PI_REMITTANCE where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                '==========================reset ref. of po no is any renewal exist============================
                clsDBFuncationality.ExecuteNonQuery("Update tspl_purchase_order_head set Renewal_Date=null,Against_PO=null where Against_PO='" + obj.PurchaseOrder_No + "'", trans)
                '==============================================================================================

                qry = "delete from TSPL_PURCHASE_ORDER_WORK_ORDER where PurchaseOrder_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PURCHASE_ORDER_WORK_ORDER_Terms where PurchaseOrder_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = " delete from TSPL_TENDER_SCHEDULE_PENALTY_PO where DocumentCode='" + strCode + " '"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = " delete from TSPL_TENDER_SCHEDULE_PO where DocumentCode='" + strCode + " '"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                'clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)

                qry = "delete from TSPL_CFORM_ISSUE_RECEIVE_DETAIL where purchaseorder_no='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where purchaseorder_no='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal isMerchantTrade As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, isMerchantTrade, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal isMerchantTrade As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsPurchaseOrderHead = clsPurchaseOrderHead.GetData(strCode, NavigatorType.Current, "", trans, isMerchantTrade)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PurchaseOrder_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Order", obj.Bill_To_Location, obj.PurchaseOrder_Date, trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsPurchaseOrderAdditionChargeInsurance.DeleteData(obj.PurchaseOrder_No, trans)

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PURCHASE_ORDER_HEAD", "PurchaseOrder_No", "TSPL_PURCHASE_ORDER_DETAIL", "PurchaseOrder_No", "TSPL_PI_REMITTANCE", "Document_No", trans)

                Dim qry As String = "delete from TSPL_PI_REMITTANCE where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                '==========================reset ref. of po no is any renewal exist============================
                clsDBFuncationality.ExecuteNonQuery("Update tspl_purchase_order_head set Renewal_Date=null,Against_PO=null where Against_PO='" + obj.PurchaseOrder_No + "'", trans)
                '==============================================================================================

                qry = "delete from TSPL_PURCHASE_ORDER_WORK_ORDER where PurchaseOrder_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PURCHASE_ORDER_WORK_ORDER_Terms where PurchaseOrder_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)

                qry = "delete from TSPL_CFORM_ISSUE_RECEIVE_DETAIL where purchaseorder_no='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where purchaseorder_no='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function

    Public Shared Function IsValidVendorForPO(ByVal ArrPONo As List(Of String), ByVal strVendorCode As String) As Boolean
        If ArrPONo IsNot Nothing AndAlso ArrPONo.Count > 0 Then
            Dim qry As String = "select PurchaseOrder_No,Vendor_Code,Vendor_Name from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No  in (" + clsCommon.GetMulcallString(ArrPONo) + ") and Vendor_Code not in ('" + strVendorCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "PO No:" + clsCommon.myCstr(dr("PurchaseOrder_No")) + " Is For Vendor Code: " + clsCommon.myCstr(dr("Vendor_Code")) + " Vendor Name:" + clsCommon.myCstr(dr("Vendor_Name"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function
    Public Shared Function IsOpenPO(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Dim Is_OpenPO As Integer = 0
        Dim qry As String = "select Is_Open_PO from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No ='" & strCode & "'"
        Is_OpenPO = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Return Is_OpenPO
    End Function

    Public Shared Function IsValidTaxGroupForPO(ByVal ArrPONo As List(Of String), ByVal strTaxGroupCode As String) As Boolean
        If ArrPONo IsNot Nothing AndAlso ArrPONo.Count > 0 Then
            Dim qry As String = "select PurchaseOrder_No,Tax_Group from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No  in (" + clsCommon.GetMulcallString(ArrPONo) + ") and Tax_Group not in ('" + strTaxGroupCode + "')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "PO No:" + clsCommon.myCstr(dr("PurchaseOrder_No")) + " .Tax Group is: " + clsCommon.myCstr(dr("Tax_Group"))
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function
    Public Shared Function GetPurchaseSetting() As DataTable
        Dim qry As String

        qry = "select CREATE_ABATEMENT_BASED_PO as IsAbatementPO,CREATE_PO_WITH_REQ as PO_Req,ENABLE_POPUP_REORDERLEVEL as PopupReorder, " &
        " MANDATE_BATCHNO_RM,MANDATE_BATCHNO_FG,MANDATE_BATCHNO_ASSET,MANDATE_BATCHNO_OTHERS,MANDATE_MFG_RM,MANDATE_MFG_FG,MANDATE_MFG_ASSET,MANDATE_MFG_OTHERS, " &
        " MANDATE_EXP_RM,MANDATE_EXP_FG,MANDATE_EXP_ASSET,MANDATE_EXP_OTHERS,Return_without_Invoice from TSPL_PURCHASE_SETTINGS"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count <= 0 Then
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item("IsAbatementPO") = False
            dr.Item("PO_Req") = 0
            dr.Item("PopupReorder") = False

            dr.Item("MANDATE_BATCHNO_RM") = False
            dr.Item("MANDATE_BATCHNO_FG") = False
            dr.Item("MANDATE_BATCHNO_ASSET") = False
            dr.Item("MANDATE_BATCHNO_OTHERS") = False

            dr.Item("MANDATE_MFG_RM") = False
            dr.Item("MANDATE_MFG_FG") = False
            dr.Item("MANDATE_MFG_ASSET") = False
            dr.Item("MANDATE_MFG_OTHERS") = False

            dr.Item("MANDATE_EXP_RM") = False
            dr.Item("MANDATE_EXP_FG") = False
            dr.Item("MANDATE_EXP_ASSET") = False
            dr.Item("MANDATE_EXP_OTHERS") = False

            dt.Rows.Add(dr)
            dt.AcceptChanges()
        End If
        Return dt
    End Function
    Public Shared Function GetInventorySetting() As DataTable
        Dim qry As String

        qry = "select IsTermsEditableOnPurchase,IsTermsEditableOnSales,IsTermsEditableOnInv from TSPL_INV_PARAMETERS"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count <= 0 Then
            Dim dr As DataRow
            dr = dt.NewRow

            dr.Item("IsTermsEditableOnPurchase") = False
            dr.Item("IsTermsEditableOnSales") = False
            dr.Item("IsTermsEditableOnInv") = False

            dt.Rows.Add(dr)
            dt.AcceptChanges()
        End If
        Return dt
    End Function
    Public Shared Function GetTaxDetail(ByVal TaxCode As String, Optional ByVal trans As SqlTransaction = Nothing) As DataTable
        Dim qry As String
        qry = "select Tax_Code,Tax_Code_Desc,Tax_Liability_Account,Tax_Recoverable,Excisable,Type as [Tax Type] from TSPL_TAX_MASTER where Tax_Code='" & TaxCode & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Public Shared Function BlanketPO(ByVal strPOCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select isBlanket from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + strPOCode + "' "
        Return IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0, True, False)
    End Function

    Public Shared Function RepariedPO(ByVal strPOCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select Is_Repair from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + strPOCode + "' "
        Return IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0, True, False)
    End Function
    Public Shared Function IsValidRepairForPO(ByVal ArrPONo As List(Of String), ByVal isRepairType As Boolean) As Boolean
        If ArrPONo IsNot Nothing AndAlso ArrPONo.Count > 0 Then
            Dim qry As String = " select PurchaseOrder_No,Vendor_Code,Vendor_Name from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No  in (" + clsCommon.GetMulcallString(ArrPONo) + ") and isnull(Is_Repair,0) <> " + IIf(isRepairType, "1", "0") + "  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim msg As String = "Error. "
                For Each dr As DataRow In dt.Rows
                    msg += Environment.NewLine + "PO No:" + clsCommon.myCstr(dr("PurchaseOrder_No")) + " is not of " + IIf(isRepairType, "Repairable", "Non-Repairable")
                Next
                Throw New Exception(msg)
            End If
        End If
        Return True
    End Function

    Public Function PrintData(ByVal strCode As String) As String
        Try
            Return PrintData(GetData(strCode, NavigatorType.Current), Nothing, False)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return ""
    End Function

    Public Function PrintData(ByVal obj As clsPurchaseOrderHead, ByVal tran As SqlTransaction, ByVal isPDFPath As Boolean) As String
        Try
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                If clsCommon.CompairString(obj.Item_Type, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.PurchaseOrder_Type, "J") = CompairStringResult.Equal Then
                    PrintWorkOrder(obj.PurchaseOrder_No, tran, isPDFPath)
                Else
                    Return PrintNew(obj.PurchaseOrder_No, Form_ID, IIf(obj.MT_Is_Merchant_Trade = 1, True, False), IIf(obj.IsPO = 1, True, False), tran, isPDFPath)
                End If
            Else
                Return PrintNew(obj.PurchaseOrder_No, Form_ID, IIf(obj.MT_Is_Merchant_Trade = 1, True, False), IIf(obj.IsPO = 1, True, False), tran, isPDFPath)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return ""
    End Function

    Private Sub PrintWorkOrder(ByVal StrDocNo As String, ByVal tran As SqlTransaction, ByVal isPDFPath As Boolean)
        Try
            Dim level1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_USER_MASTER.User_Name from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join TSPL_USER_MASTER  on TSPL_USER_MASTER.User_Code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.User_Code where Document_Code='" & StrDocNo & "' and No_of_Level=1", tran))
            Dim level2 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_USER_MASTER.User_Name from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join TSPL_USER_MASTER  on TSPL_USER_MASTER.User_Code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.User_Code where Document_Code='" & StrDocNo & "' and No_of_Level=2", tran))
            Dim level3 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_USER_MASTER.User_Name from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join TSPL_USER_MASTER  on TSPL_USER_MASTER.User_Code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.User_Code where Document_Code='" & StrDocNo & "' and No_of_Level=3", tran))

            ''richa UDL/27/06/19-000300
            Dim qry As String = " select TSPL_PURCHASE_ORDER_DETAIL.Item_Code,TSPL_PURCHASE_ORDER_DETAIL.item_desc,TSPL_PURCHASE_ORDER_DETAIL.Unit_code" &
 " ,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty,TSPL_PURCHASE_ORDER_DETAIL.Taxable_Amount ,TSPL_PURCHASE_ORDER_DETAIL.Amount ,TSPL_PURCHASE_ORDER_HEAD.Total_Tax_Amt" &
 " ,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost,TSPL_PURCHASE_ORDER_DETAIL.Item_Net_Amt,TSPL_PURCHASE_ORDER_DETAIL.tax1,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX1_Base_Amt " &
 " ,TSPL_PURCHASE_ORDER_DETAIL.tax2,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX2_Base_Amt " &
 " ,TSPL_PURCHASE_ORDER_DETAIL.tax3,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt,TSPL_PURCHASE_ORDER_DETAIL.TAX3_Base_Amt,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,TSPL_PURCHASE_ORDER_HEAD.Vendor_Name,TSPL_VENDOR_MASTER.Add1,TSPL_VENDOR_MASTER.Add2,TSPL_VENDOR_MASTER.Add3,TSPL_VENDOR_MASTER.State_Code as State,TSPL_VENDOR_MASTER.City_Code_Desc as VendorCity,TSPL_VENDOR_MASTER.Country,TSPL_VENDOR_MASTER.GSTFinalNo,TSPL_PURCHASE_ORDER_HEAD.Kind_Attentation,TSPL_PURCHASE_ORDER_HEAD.Subject,TSPL_PURCHASE_ORDER_HEAD.Terms_Remark,TSPL_VENDOR_MASTER.Phone1 as mobile,TSPL_VENDOR_MASTER.Phone2,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,103) as po_date,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,106) as FormatDate,  TSPL_USER_MASTER.User_Name as Created_By ,Loc_Short_Name_001 " &
 " , Comp_Add1,Com_Add2,Com_Add3,City, Comp_State,Comp_Country,GST_STATE_Code_001,GSTNO_001 as Comp_GSTINNO,TSPL_LOCATION_MASTER.Loc_Short_Name,TSPL_LOCATION_MASTER.Add1 AS Loc_Add1, TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_Add3,TSPL_LOCATION_MASTER.GSTNO as Loc_GSTINNO,LOC_STATE_MASTER.GST_STATE_Code AS Loc_GstStateCode,LOC_STATE_MASTER.STATE_NAME as Loc_State  " &
" ,'" + level1 + "' as Level1,'" + level2 + "' as Level2,'" + level3 + "' as Level3 ,TSPL_USER_MASTER.User_Name as UserName, TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.logo_img,TSPL_PURCHASE_ORDER_HEAD.Quotation_No,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.Quotation_Date,103) as Quotation_Date,TSPL_COMPANY_MASTER.CINNo,Vendor_STATE_MASTER.GST_STATE_Code as Vendor_State_Code,TSPL_VENDOR_MASTER.Email,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.Discount_Amt " &
" from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_PURCHASE_ORDER_DETAIL on TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PURCHASE_ORDER_HEAD.Vendor_Code " &
                " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_PURCHASE_ORDER_HEAD.Comp_Code " &
                " left outer join TSPL_STATE_MASTER as Company_State_Master on Company_State_Master.STATE_CODE =TSPL_COMPANY_MASTER.State  " &
                " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location " &
                " LEFT OUTER JOIN TSPL_STATE_MASTER AS LOC_STATE_MASTER ON LOC_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State " &
                " LEFT OUTER JOIN TSPL_STATE_MASTER AS Vendor_STATE_MASTER ON Vendor_STATE_MASTER.STATE_CODE=TSPL_VENDOR_MASTER.State_Code " &
            " left outer join TSPL_USER_MASTER  on TSPL_USER_MASTER.User_Code=TSPL_PURCHASE_ORDER_HEAD.Created_By " &
            " left outer join (select Location_Code,TSPL_LOCATION_MASTER.Loc_Short_Name as Loc_Short_Name_001,case when TSPL_LOCATION_MASTER.Location_Code='001' then TSPL_Location_MASTER.add1 end as Comp_Add1 " &
 ",case when TSPL_LOCATION_MASTER.Location_Code='001' then TSPL_Location_MASTER.add2 end as Com_Add2 " &
 ",case when TSPL_LOCATION_MASTER.Location_Code='001' then TSPL_Location_MASTER.add3 end as Com_Add3 " &
 ",TSPL_LOCATION_MASTER.City_Code as City " &
 ",case when TSPL_LOCATION_MASTER.Location_Code='001' then TSPL_STATE_MASTER.STATE_NAME end as Comp_State " &
 ",TSPL_LOCATION_MASTER.Country as Comp_Country,TSPL_LOCATION_MASTER.GSTNO as GSTNO_001,GST_STATE_Code as GST_STATE_Code_001 " &
  "          from TSPL_LOCATION_MASTER " &
 "left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State " &
 "where Location_Code='001')DefaultLoc on DefaultLoc.Location_Code='001' "
            qry += " where 2=2 and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" & StrDocNo & "'"


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)

            Dim qry1 As String = "select TSPL_PURCHASE_ORDER_WORK_ORDER_TERMS.Terms_Condition,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code from TSPL_PURCHASE_ORDER_WORK_ORDER_TERMS left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_WORK_ORDER_TERMS.PurchaseOrder_No "
            qry1 += " where 2=2 and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" & StrDocNo & "'"

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1, tran)

            Dim qry2 As String = "select TSPL_PURCHASE_ORDER_WORK_ORDER.Field_Name,TSPL_PURCHASE_ORDER_WORK_ORDER.Description as Field_Name_desc from TSPL_PURCHASE_ORDER_WORK_ORDER left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_WORK_ORDER.PurchaseOrder_No "
            qry2 += " where 2=2 and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" & StrDocNo & "'"

            Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(qry2, tran)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to print")
            End If
            Dim frmCRViewer As New frmCrystalReportViewer()
            ''frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "rptWorkOrder", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptTermsCondition.rpt", " Teram Conditions", dt3, "rptWorkOrderValues.rpt", )
            frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "rptWorkOrder", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptTermsCondition.rpt", "rptWorkOrderValues.rpt", dt3)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function PrintNew(ByVal StrDocNo As String, ByVal StrFormID As String, ByVal IsMT As Boolean, ByVal IsPO As Boolean, ByVal tran As SqlTransaction, ByVal isPDFPath As Boolean) As String
        Dim frmCRViewer As New frmCrystalReportViewer()
        Dim level1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_USER_MASTER.User_Name from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join TSPL_USER_MASTER  on TSPL_USER_MASTER.User_Code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.User_Code where Document_Code='" & StrDocNo & "' and No_of_Level=1 and status='Approved'", tran))
        Dim level2 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_USER_MASTER.User_Name from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join TSPL_USER_MASTER  on TSPL_USER_MASTER.User_Code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.User_Code where Document_Code='" & StrDocNo & "' and No_of_Level=2 and status='Approved'", tran))
        Dim level3 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_USER_MASTER.User_Name from TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL left outer join TSPL_USER_MASTER  on TSPL_USER_MASTER.User_Code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.User_Code where Document_Code='" & StrDocNo & "' and No_of_Level=3 and status='Approved'", tran))

        '==shivani
        '' changed by Panch raj against ticket No:BM00000008420
        '===========Update by preeti gupta Against Ticket No[BM00000008420,ERO/25/06/18-000360,ERO/04/04/18-000061,UDL/26/06/18-000194]==================
        Dim qry As String = ("select Transaction_Code,Custom_field_Code,value,Name  from TSPL_CUSTOM_FIELD_VALUES left join TSPL_CUSTOM_FIELD_HEAD on TSPL_CUSTOM_FIELD_HEAD.Code=TSPL_CUSTOM_FIELD_VALUES.Custom_Field_Code left join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_CUSTOM_FIELD_VALUES.Program_Code where TSPL_CUSTOM_FIELD_VALUES.Program_Code='PO-ODR' and Transaction_Code='" & StrDocNo & "' ")
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        Dim customecount As Integer = dt1.Rows.Count
        '========
        '===Sanjeet(gst(28/06/2017)======
        Dim PO_Date As Date?
        PO_Date = Nothing
        PO_Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date  from TSPL_PURCHASE_ORDER_HEAD where TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No ='" + StrDocNo + "'", tran))
        Dim Ho_Address As String = Nothing
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Dim HoQuery As String = "SELECT case when isnull(TSPL_LOCATION_MASTER.Add1,'')<>'' then  TSPL_LOCATION_MASTER.Add1 +', ' else '' end +" &
                    " case when isnull(TSPL_LOCATION_MASTER.Add2,'')<>'' then  TSPL_LOCATION_MASTER.Add2 +', ' else '' end + " &
                    " case when isnull(TSPL_LOCATION_MASTER.Add3,'')<>'' then  TSPL_LOCATION_MASTER.Add3 +', ' else '' end + " &
                    " case when isnull(TSPL_CITY_MASTER.City_Name,'') <>'' then TSPL_CITY_MASTER.City_Name +', '  else '' end " &
                     " + case when isnull( TSPL_STATE_MASTER.STATE_NAME,'')<>'' then  TSPL_STATE_MASTER.STATE_NAME +', ' else '' end " &
                     " + Case when isnull(TSPL_LOCATION_MASTER.Pin_Code,'')<>'' then convert(varchar,TSPL_LOCATION_MASTER.Pin_Code) else ' ' end as HO_Address " &
                     " FROM TSPL_LOCATION_MASTER  left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_LOCATION_MASTER.City_Code " &
                     " left outer join TSPL_STATE_MASTER ON TSPL_STATE_MASTER.STATE_CODE=TSPL_LOCATION_MASTER.State  WHERE Location_Code='001'	"
            Ho_Address = clsCommon.myCstr(clsDBFuncationality.getSingleValue(HoQuery, tran))
        End If


        '========================
        '============update by preeti against ticket no[GKD/22/05/18-000135]=================
        Dim strAuthrozedBy As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 tspl_user_master.user_name as Modified_By  from tspl_approval_level_transaction_detail left join tspl_user_master on tspl_user_master.user_code=tspl_approval_level_transaction_detail.Modified_By where Document_Code='" + StrDocNo + "' and Status ='Approved' and No_Of_Level=1  ", tran))
        '====================================================================================
        Dim strQuery As String
        Dim FooterText As String
        Dim dtBarCode As New DataTable
        dtBarCode.Columns.Add("BarCodeImage", GetType(Byte()))
        Dim bytes() As Byte
        Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(clsCommon.MyBarcodeImage(StrDocNo, 1, False).[GetType]())
        bytes = DirectCast(BitmapConverter.ConvertTo(clsCommon.MyBarcodeImage(StrDocNo, 1, False), GetType(Byte())), Byte())
        'dtBarCode.Rows.Add()

        strQuery = ""
        '' Anubhooti 28-Aug-2014 (Demo Setting For Status)
        Dim QryShowStatus As String = ""
        Dim ShowStatusForPurchase As Double = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='ShowStatusForPurchase'", tran)
        If clsCommon.CompairString(clsCommon.myCstr(ShowStatusForPurchase), "1") = CompairStringResult.Equal Then
            QryShowStatus = " ,(case when TSPL_PURCHASE_ORDER_HEAD.status =1 then 'AUTHORIZED' else 'NOT AUTHORIZED' end) as Status "
        Else
            QryShowStatus = " , '' as Status "
        End If

        '' Anubhooti 22-Aug-2014 (Demo Setting For Abemdment)

        Dim Is_AbemdmentForDemo As Double = clsDBFuncationality.getSingleValue("SELECT Description  FROM TSPL_FIXED_PARAMETER  WHERE Code ='Is_AbemdmentForDemo'", tran)
        '' query changed by Panch raj against Ticket No:BM00000008420
        If Is_AbemdmentForDemo = 1 Then

            strQuery = "select Footer_Text  from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + StrFormID + "' "
            'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Footer_Text  from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + Form_ID + "'"))
            'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strQuery)

            'FooterText = dt1.Rows(0).Item("Footer_Text")
            FooterText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Footer_Text  from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + StrFormID + "'", tran))
            strQuery = "select "

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                strQuery += "'" + level1 + "' as Level1,'" + level2 + "' as Level2,'" + level3 + "' as Level3, "
            End If

            strQuery += "  createdUser.User_Name as UserName,isNull (TSPL_PURCHASE_ORDER_HEAD.Against_Vendor_Quotation, '') as Against_Vendor_Quotation  ,case when len ( isNull (TSPL_PURCHASE_ORDER_HEAD.Against_Vendor_Quotation, '')) > 0 then convert(varchar, TSPL_VENDOR_QUOTATION_HEAD.VQDate ,103) else  '' end as Vendor_Quotation_Date, '" + strAuthrozedBy + "' as AuthrozedBy,'" + Ho_Address + "' as HO_Address, TSPL_VENDOR_MASTER.State_Code AS Vendor_StateCode,tspl_state_master_for_location_state.state_code as Loc_StateCode, ISNULL(TSPL_ITEM_MASTER.HSN_Code,'') AS HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code, TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo , TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER_FOR_VENDOR.GST_STATE_Code AS Vendor_GST_StateCode,TSPL_PURCHASE_ORDER_HEAD.Created_By as Created_By_Name,TSPL_PURCHASE_ORDER_HEAD.Posted_By as Modify_By_Name, TSPL_PURCHASE_ORDER_HEAD.created_by,TSPL_PURCHASE_ORDER_HEAD.Posted_By as Modify_By ,TSPL_CURRENCY_MASTER.CURRENCY_SIGN,TSPL_CURRENCY_MASTER.currency_code ,TSPL_EX_PI_HEAD.Payment_Terms as PI_Payment_terms ,TSPL_EX_PI_HEAD.Terms_Code as Payment_Terms_Code,convert(varchar,TSPL_EX_PI_HEAD.Due_Date,103) as PI_Due_Date ,TSPL_PURCHASE_ORDER_HEAD.MT_PI_No ,convert(varchar,TSPL_EX_PI_HEAD.Document_Date,103) as PI_Date,tspl_state_master_for_location_state.state_name as Location_state_name,tspl_location_master.city_code  as Loca_city_name,case when ISNULL(TSPL_PURCHASE_ORDER_DETAIL.Qty_Desc,'')='' then CAST(CAST(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty As decimal(18,2)) AS varchar) else TSPL_PURCHASE_ORDER_DETAIL.Qty_Desc end as frm_Qty_Desc, case when isnull(TSPL_PURCHASE_ORDER_DETAIL.Rate_Desc,'')='' then TSPL_PURCHASE_ORDER_DETAIL.Item_Cost else TSPL_PURCHASE_ORDER_DETAIL.Rate_Desc end as  frm_Rate_Desc, tspl_purchase_order_head.IsContent,tspl_purchase_order_head.IsPO," + clsCommon.myCstr(customecount) + " as CustomCount,tspl_purchase_order_detail.remarks as detail_remark,tspl_company_master.cinno as Comp_CIN,TSPL_PURCHASE_ORDER_DETAIL.item_cost+case when PurchaseOrder_Qty=0 then 1 else ( Amt_Less_Discount /PurchaseOrder_Qty )end as NetAmount,case when PurchaseOrder_Qty=0 then Item_Cost else ( Amt_Less_Discount /PurchaseOrder_Qty )end NetRate,tspl_purchase_order_head.auto_calculate,tspl_purchase_order_head.item_type, TSPL_PURCHASE_ORDER_HEAD.payment_Terms,tspl_purchase_order_head.subject,tspl_purchase_order_head.content_Subject,tspl_purchase_order_head.Kind_Attentation,tspl_purchase_order_head.Comments,TSPL_PURCHASE_ORDER_DETAIL.Qty_Desc ,TSPL_PURCHASE_ORDER_DETAIL.Rate_Desc ,cast(TSPL_PURCHASE_ORDER_DETAIL.Amount_Desc as float) as Amount_Desc,TSPL_PURCHASE_ORDER_HEAD.insurance_Terms,TSPL_PURCHASE_ORDER_HEAD.Delivery_Terms_Code,tspl_delivery_terms_master.description as Delivery_Terms_Desc,TSPL_VENDOR_MASTER.Add1 as VenAdd1,TSPL_VENDOR_MASTER.add2 as VenAdd2,TSPL_VENDOR_MASTER.Add3 as VenAdd3,TSPL_VENDOR_MASTER.City_Code_Desc as Vendor_City,TSPL_VENDOR_MASTER.State as Vendor_State,TSPL_VENDOR_MASTER.Pin_Code as Vendor_Pin,case when len(isnull(TSPL_VENDOR_MASTER.Add3,'')) > 0 then  TSPL_VENDOR_MASTER.Add3 else '' end + case when len( isnull(TSPL_VENDOR_MASTER.City_Code_Desc,'')) >0 then  case when len(isnull(TSPL_VENDOR_MASTER.Add3,'')) > 0 then ', '+ TSPL_VENDOR_MASTER.City_Code_Desc else TSPL_VENDOR_MASTER.City_Code_Desc  end else ' ' end + case when len(isnull( TSPL_VENDOR_MASTER.State,'')) >0 then  case when len( isnull(TSPL_VENDOR_MASTER.City_Code_Desc,'')) >0 then ', ' +TSPL_VENDOR_MASTER.State  else TSPL_VENDOR_MASTER.State end        else ' ' end + Case when len(isnull(TSPL_VENDOR_MASTER.Pin_Code,'') ) > 0 then ' - '+TSPL_VENDOR_MASTER.Pin_Code else ' ' end as Vend_Add3_City_State_Pin,'' as FromDate,'" + FooterText + "' as FooterText,'' as Todate,'' as DocFilter,'' as VendorCodeFilter,'' as LocCodeFilter,PurchaseOrder_Type,case when PurchaseOrder_Type='J' then 'WORK ORDER' else case when PurchaseOrder_Type='L' or PurchaseOrder_Type='I'  then 'PURCHASE ORDER' else '' end end as OrderWise,case when PurchaseOrder_Type='J' then 'THE ABOVE RATES ARE INCLUSIVE OF TAX' else case when PurchaseOrder_Type='L' then '6 MONTHS FROM THE DATE OF SUPPLY FOR ANY TYPE OF MANUFACTURING DEFECT ONLY.' else '' end end as Note,  TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as address, TSPL_PURCHASE_ORDER_HEAD.Dept_Desc, Case WHen '" + objCommonVar.CurrentCompanyCode + "'='GUNTUR' Then TSPL_PURCHASE_ORDER_HEAD.Delivery_Duration Else TSPL_PURCHASE_ORDER_HEAD.Delivery_date End as [Delivery_date],TSPL_PURCHASE_ORDER_HEAD.Remarks ,TSPL_PURCHASE_ORDER_HEAD.Terms_Code,TSPL_PURCHASE_ORDER_HEAD.Terms_Remark,TSPL_PURCHASE_ORDER_HEAD.Mode_Of_Transport as  ModeofTransport,TSPL_PURCHASE_ORDER_DETAIL .Specification as  specification,TSPL_PURCHASE_ORDER_HEAD.Abandonment_No,(select max(TSPL_PURCHASE_ORDER_HEAD_Hist.Abandonment_Date) from TSPL_PURCHASE_ORDER_HEAD_Hist where TSPL_PURCHASE_ORDER_HEAD_Hist.Abandonment_No=TSPL_PURCHASE_ORDER_HEAD.Abandonment_No and PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No) as Abandonment_Date,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as purchase_no ,convert(varchar,TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date,103) as po_date ,case TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Type when 'L'then 'Local' when 'I' then 'Import' when 'J' then 'Job Work' when 'O' then 'Open' when 'S' then 'Specific'else 'Null' end as po_type ,tspl_purchase_order_head.vendor_name,TSPL_PURCHASE_ORDER_HEAD .Vendor_Code as vendor_type,TSPL_PURCHASE_ORDER_HEAD .Terms_Code as termscode ,TSPL_PURCHASE_ORDER_HEAD .Ref_No as ref_no ,TSPL_PURCHASE_ORDER_HEAD .Comments as comments,TSPL_PURCHASE_ORDER_HEAD .Discount_Amt as dis_amt,TSPL_PURCHASE_ORDER_DETAIL.Disc_Amt as dis_amt1,TSPL_PURCHASE_ORDER_HEAD.Amount_Less_Discount  as aftrdiscount ,TSPL_PURCHASE_ORDER_HEAD .PO_Total_Amt as Total_amount,TSPL_PURCHASE_ORDER_HEAD.Discount_Base as bfrdisc_amount,tax1.Tax_Code_Desc as tax1name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax3_amt,0) as txt3amt,tax4.Tax_Code_Desc as tax4name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax6_amt,0) as txt6amt,tax7.Tax_Code_Desc as tax7name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax8_amt,0) as txt8amt,isnull (TSPL_PURCHASE_ORDER_HEAD.tax9_amt,0) as txt9amt,tax10.Tax_Code_Desc as tax10name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax10_amt,0) as txt10amt,isnull(TSPL_PURCHASE_ORDER_HEAD .Total_Tax_Amt,0) as total_tax_amt, TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount,  " &
           " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" &
         "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" &
            "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" &
          "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" &
             "  + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end" &
            "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end " &
          "  + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end " &
            "  +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " &
           "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " &
            " as Company_Address," &
            " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" &
         "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" &
          "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" &
          "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end " &
          "  + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end " &
            "  +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " &
           "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " &
            " as Comp_Address, TSPL_LOCATION_MASTER.HOAdd1, TSPL_LOCATION_MASTER.HOAdd2," &
            "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name1 as Add1Desc, " &
            " case when len(TSPL_COMPANY_MASTER.Pan_No) >0 then cast (TSPL_COMPANY_MASTER.Pan_No as varchar) else '' end as PAN_NO , " &
            "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt1,0) as Add1, " &
            "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name2 as Add2Desc, " &
             "   isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt2,0) as Add2, " &
             "   TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name3 as Add3Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt3,0) as Add3, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name4 as Add4Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt4,0) as Add4, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name5 as Add5Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt5,0) as Add5, " &
             "   TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name6 as Add6Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt6,0) as Add6, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name7 as Add7Desc, " &
             "   isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt7,0) as Add7, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name8 as Add8Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt8,0) as Add8, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name9 as Add9Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt9,0) as Add9, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name10 as Add10Desc, " &
               "case when TSPL_PURCHASE_ORDER_DETAIL.TAX1='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX2='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX3='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX4='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX5='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX6='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX7='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX8='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX9='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX10='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate END AS Exci_Item_TaxRate ," &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt10,0) as Add10,(case when TSPL_PURCHASE_ORDER_HEAD.Emergency=0 then '-N' else '-E' end) as Emergency,TSPL_PURCHASE_ORDER_DETAIL.Total_Tax_Amt,TSPL_PURCHASE_ORDER_DETAIL.Item_Net_Amt, " &
             " TSPL_PURCHASE_ORDER_HEAD.PO_Total_Amt as DocAmt,TSPL_COMPANY_MASTER.Add1 as Comp_Add1 ,TSPL_COMPANY_MASTER.Add2 as Comp_Add2 ,TSPL_COMPANY_MASTER.Add3 as Comp_Add3, ( case when TSPL_COMPANY_MASTER.Phone2  <> '' then TSPL_COMPANY_MASTER.Phone1 +','+TSPL_COMPANY_MASTER.Phone2 else TSPL_COMPANY_MASTER.Phone1 end) as Comp_Phn,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as Comp_Email ,TSPL_COMPANY_MASTER.Tin_No as Comp_TinNo,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_PURCHASE_ORDER_HEAD.CURRENCY_CODE,(TSPL_VENDOR_MASTER.Lst_No) as ven_lst_no ,(TSPL_VENDOR_MASTER.CST)as ven_cst ,(TSPL_VENDOR_MASTER.Tin_No)as ven_tin_no,TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location , (case when TSPL_PURCHASE_ORDER_HEAD.status =1 then TSPL_PURCHASE_ORDER_HEAD.Modify_By else '' end) as Modify_By,TSPL_SHIP_TO_LOCATION.add1 +case when len(TSPL_SHIP_TO_LOCATION.add2)>0 then ', '+TSPL_SHIP_TO_LOCATION.add2 else '' end +case when LEN(isnull(TSPL_SHIP_TO_LOCATION.Add3,''))>0 then ', '+isnull(TSPL_SHIP_TO_LOCATION.Add3,'') else ' ' end   as Ship_address,TSPL_PURCHASE_ORDER_HEAD.Created_By ,TSPL_PURCHASE_ORDER_HEAD.Modify_By ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_SHIP_TO_LOCATION.Ship_To_Desc  ,TSPL_PURCHASE_ORDER_HEAD.Terms_Code ,TSPL_PURCHASE_ORDER_HEAD.Delivery_date , " &
             " TSPL_PURCHASE_ORDER_HEAD.TAX1,TSPL_PURCHASE_ORDER_HEAD.TAX2,TSPL_PURCHASE_ORDER_HEAD.TAX3,TSPL_PURCHASE_ORDER_HEAD.TAX4,TSPL_PURCHASE_ORDER_HEAD.TAX5 ,TSPL_PURCHASE_ORDER_HEAD. Ship_To_Location, " &
             " TSPL_LOCATION_MASTER.Location_Desc as compname,TSPL_LOCATION_MASTER.Registration_Number,TSPL_LOCATION_MASTER.Registration_Number as VAT_No,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_LOCATION_MASTER.add1 as Loc_ADD1, TSPL_LOCATION_MASTER.add2 +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as address1 ,TSPL_LOCATION_MASTER.TAN_No as Loc_FaxNo, TSPL_VENDOR_MASTER.Email as VenEmail,TSPL_PURCHASE_ORDER_DETAIL.item_code as item_code, TSPL_VENDOR_ITEM_DETAIL.vendor_item_no as VendorItem,((TSPL_PURCHASE_ORDER_DETAIL.item_desc) +(case when TSPL_PURCHASE_ORDER_DETAIL.Specification='' then '' else ' . ' end) +TSPL_PURCHASE_ORDER_DETAIL.Specification+ (case when TSPL_PURCHASE_ORDER_DETAIL.Remarks='' then '' else ' / ' end) + TSPL_PURCHASE_ORDER_DETAIL.Remarks )  as itemdesc,TSPL_TERMS_MASTER.Terms_Desc  as termsdesc,TSPL_PURCHASE_ORDER_DETAIL.Row_Type,TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty as qty,TSPL_PURCHASE_ORDER_DETAIL.unit_code as uom,TSPL_PURCHASE_ORDER_DETAIL.item_cost as itemcost,TSPL_PURCHASE_ORDER_DETAIL.amount as amount,TSPL_PURCHASE_ORDER_DETAIL.MRP ,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost , Case When TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount=0 then 0 else (TSPL_PURCHASE_ORDER_DETAIL.Total_Tax_Amt /TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount) *100 end as Tax,case when TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount=0 then TSPL_PURCHASE_ORDER_DETAIL.Item_Cost else ((((TSPL_PURCHASE_ORDER_DETAIL.Total_Tax_Amt /TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount) *100)*TSPL_PURCHASE_ORDER_DETAIL.Item_Cost/100) +TSPL_PURCHASE_ORDER_DETAIL.Item_Cost) end as landing_Rate,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,TSPL_PURCHASE_ORDER_DETAIL.Item_Net_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX1_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX2_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX3_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX4_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX5_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX6_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX7_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX8_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX9_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX10_Rate ,TSPL_PURCHASE_ORDER_DETAIL.Disc_Per as 'dis_per',TSPL_LOCATION_MASTER.CST_No as CST_LST ,TSPL_LOCATION_MASTER.TIN_No, TSPL_LOCATION_MASTER.Ecc_Number as ExciseNo, TSPL_LOCATION_MASTER.Range_Code as Range, TSPL_LOCATION_MASTER.Commissionerate as DivisionCommission,  " &
             " Case when len(ISNULL(TSPL_VENDOR_MASTER.Phone1,''))>0 and TSPL_VENDOR_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_VENDOR_MASTER.Phone1 end " &
            "  +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_VENDOR_MASTER.Phone2 Else'' End as VenPhone1 , TSPL_VENDOR_MASTER.Fax as VenFax,TSPL_VENDOR_MASTER.Tin_No as Vendor_Tin_No,  Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_LOCATION_MASTER.Phone1 end " &
            "  +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as Phn, TSPL_LOCATION_MASTER.TAN_No as faxno,TSPL_LOCATION_MASTER.Email as EmailId, TSPL_PURCHASE_ORDER_DETAIL.Unit_code,Circle_No as APGST,TSPL_PURCHASE_ORDER_HEAD.Total_Add_Charge as total_add_charges,TSPL_PURCHASE_ORDER_HEAD.Against_Requisition as ReqNo,Convert(varchar(15),TSPL_REQUISITION_HEAD.Requisition_Date,103)  as ReqDt ,TSPL_PURCHASE_ORDER_HEAD.Quotation_No,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.Quotation_Date,103) as Quotation_Date,TSPL_PURCHASE_ORDER_DETAIL .Location, TSPL_PURCHASE_ORDER_HEAD.Expiry_Date,TSPL_PURCHASE_ORDER_HEAD.abandonment_no as AmdNo,  convert (varchar,TSPL_PURCHASE_ORDER_HEAD.amendment_date ,103  ) as Abd_Date,TSPL_PURCHASE_ORDER_HEAD.amendment_by " &
             " " & QryShowStatus & " " &
              " ,taxRate1.Tax_Rate_Desc as Tax_Rate_Desc1,taxRate2.Tax_Rate_Desc as Tax_Rate_Desc2,taxRate3.Tax_Rate_Desc as Tax_Rate_Desc3, " &
             " taxRate4.Tax_Rate_Desc as Tax_Rate_Desc4,taxRate5.Tax_Rate_Desc as Tax_Rate_Desc5,taxRate6.Tax_Rate_Desc as Tax_Rate_Desc6, " &
             " taxRate7.Tax_Rate_Desc as Tax_Rate_Desc7,taxRate8.Tax_Rate_Desc as Tax_Rate_Desc8,taxRate9.Tax_Rate_Desc as Tax_Rate_Desc9, " &
             " taxRate10.Tax_Rate_Desc as Tax_Rate_Desc10 , TSPL_LOCATION_MASTER.add2 as Loc_Add2 ,TSPL_LOCATION_MASTER.Add3 as Loc_Add3,TSPL_LOCATION_MASTER.Add4 as Loc_Add4,TSPL_PURCHASE_ORDER_HEAD.Currency_Code " &
            " ,tax1.type as Tax1Type,tax2.Type AS Tax2Type,tax3.Type AS Tax3Type,tax4.Type AS Tax4Type,tax5.Type AS Tax5Type,tax6.type  AS Tax6Type, tax7.type  AS Tax7Type,tax8.type  AS Tax8Type,tax9.type  AS Tax9Type,tax10.type  AS Tax10Type " &
            " ,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt,0) as DTAX1_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt,0) as DTAX2_AMT," &
            " ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt,0) as DTAX3_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt,0) as DTAX4_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt,0) as DTAX5_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt, 0)as DTAX6_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt,0) as DTAX7_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt,0) as DTAX8_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt,0) as DTAX9_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt,0) as DTAX10_AMT," &
            " ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,0) AS DTAX1_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate,0) AS DTAX2_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,0) AS DTAX3_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate,0) AS DTAX4_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate,0) AS DTAX5_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate,0) AS DTAX6_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate,0) AS DTAX7_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate,0) AS DTAX8_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate,0) AS DTAX9_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate,0) AS DTAX10_Rate " &
            " ,TSPL_PURCHASE_ORDER_HEAD.Insurance,TSPL_PURCHASE_ORDER_HEAD.Freight,TSPL_PURCHASE_ORDER_HEAD.Packing_Forward " &
            " ,(select STUFF((SELECT ','+ QUOTENAME(Terms_Condition) as Alies_Name FROM TSPL_PURCHASE_ORDER_WORK_ORDER_Terms where purchaseorder_no='" + StrDocNo + "' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')) as TandC " &
             " from TSPL_PURCHASE_ORDER_DETAIL " &
            " left outer join TSPL_PURCHASE_ORDER_HEAD  on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No" &
            " Left Outer Join TSPL_VENDOR_ITEM_DETAIL ON TSPL_VENDOR_ITEM_DETAIL.item_no=TSPL_PURCHASE_ORDER_DETAIL.Item_Code AND TSPL_VENDOR_ITEM_DETAIL.vendor_code=TSPL_PURCHASE_ORDER_HEAD.Vendor_Code AND TSPL_VENDOR_ITEM_DETAIL.Location_Code=TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location" &
            " left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_PURCHASE_ORDER_HEAD.Against_Requisition" &
            " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax1" &
            " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_PURCHASE_ORDER_HEAD.tax2" &
            " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_PURCHASE_ORDER_HEAD .TAX3" &
            " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_PURCHASE_ORDER_HEAD .tax4" &
            " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_PURCHASE_ORDER_HEAD .tax5" &
            " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX6" &
            " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX7" &
            " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX8" &
            " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX9" &
            " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX10" &
            " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_PURCHASE_ORDER_HEAD.comp_code" &
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PURCHASE_ORDER_HEAD.Vendor_Code" &
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location" &
            " left outer join TSPL_SHIP_TO_LOCATION on TSPL_PURCHASE_ORDER_HEAD.Ship_To_Location =TSPL_SHIP_TO_LOCATION.Ship_To_Code" &
            " left outer join TSPL_TERMS_MASTER on TSPL_PURCHASE_ORDER_HEAD.Terms_Code =TSPL_TERMS_MASTER.Terms_Code " &
             " LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " &
            " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State " &
               " left outer join TSPL_TAX_rates as taxRate1 on taxRate1.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax1 and taxRate1.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX1_Rate and taxRate1.Tax_Type='P' " &
            " left outer join TSPL_TAX_rates as taxRate2 on taxRate2.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax2 and taxRate2.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX2_Rate and taxRate2.Tax_Type='P'  " &
            " left outer join TSPL_TAX_rates as taxRate3 on taxRate3.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax3 and taxRate3.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX3_Rate and taxRate3.Tax_Type='P'  " &
            " left outer join TSPL_TAX_rates as taxRate4 on taxRate4.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax4 and taxRate4.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX4_Rate and taxRate4.Tax_Type='P' " &
            " left outer join TSPL_TAX_rates as taxRate5 on taxRate5.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax5 and taxRate5.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX5_Rate and taxRate5.Tax_Type='P' " &
            " left outer join TSPL_TAX_rates as taxRate6 on taxRate6.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax6 and taxRate6.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX6_Rate and taxRate6.Tax_Type='P' " &
            " left outer join TSPL_TAX_rates as taxRate7 on taxRate7.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax7 and taxRate7.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX7_Rate and taxRate7.Tax_Type='P'  " &
            " left outer join TSPL_TAX_rates as taxRate8 on taxRate8.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax8 and taxRate8.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX8_Rate and taxRate8.Tax_Type='P' " &
            " left outer join TSPL_TAX_rates as taxRate9 on taxRate9.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax9 and taxRate9.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX9_Rate and taxRate9.Tax_Type='P'  " &
            " left outer join TSPL_TAX_rates as taxRate10 on taxRate10.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax10 and taxRate10.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX10_Rate and taxRate10.Tax_Type='P'    " &
            " left outer join tspl_delivery_terms_master on tspl_delivery_terms_master.code=TSPL_PURCHASE_ORDER_HEAD.Delivery_Terms_Code " &
            " left outer join tspl_state_master as tspl_state_master_for_location_state on   tspl_state_master_for_location_state.state_code=tspl_location_master.state" &
            " left outer join TSPL_EX_PI_HEAD  on TSPL_EX_PI_HEAD.Document_Code =TSPL_PURCHASE_ORDER_HEAD.MT_PI_No" &
            " left outer join TSPL_CURRENCY_MASTER on TSPL_CURRENCY_MASTER.CURRENCY_CODE =TSPL_PURCHASE_ORDER_HEAD.CURRENCY_CODE" &
            " left outer join TSPL_USER_MASTER as createdUser on createdUser.User_Code =TSPL_PURCHASE_ORDER_HEAD.Created_By " &
            " left outer join TSPL_USER_MASTER as ModifyUser on ModifyUser.User_Code =TSPL_PURCHASE_ORDER_HEAD.Modify_By " &
            " left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_FOR_VENDOR ON TSPL_VENDOR_MASTER.State_Code=TSPL_STATE_MASTER_FOR_VENDOR.STATE_CODE " &
            " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
            " left outer join TSPL_VENDOR_QUOTATION_HEAD on TSPL_VENDOR_QUOTATION_HEAD.Code = TSPL_PURCHASE_ORDER_HEAD.Against_Vendor_Quotation " &
            " where 2=2"
            strQuery += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No in ('" + StrDocNo + "') "
            strQuery += " order by TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No ,TSPL_PURCHASE_ORDER_DETAIL .Line_No"
        Else
            strQuery = "select   TSPL_COMPANY_MASTER.add1 as Add01,TSPL_COMPANY_MASTER.add2 as add02,TSPL_COMPANY_MASTER.Add3 as add03,TSPL_COMPANY_MASTER.Pincode as PC,TSPL_COMPANY_MASTER.CINNo AS CNo,TSPL_COMPANY_MASTER.Email as Mail,TSPL_CITY_MASTER_fOR_Comp.City_Name as CityName,TSPL_COMPANY_MASTER.Phone1 as MNo1,TSPL_COMPANY_MASTER.Phone2 as MNo2,
                         TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" &
         "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" &
            "  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end" &
          "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" &
           "  + case when len(TSPL_COMPANY_MASTER.Pan_No      )>0 then ', PAN No - '+ cast(TSPL_COMPANY_MASTER.Pan_No as varchar)  else ' ' end" &
            "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end " &
          "  + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end " &
            "  +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " &
           "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " &
            " as Company_Address," &
            "  TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end" &
         "  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end" &
          "  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end" &
          "  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end " &
          "  + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end " &
            "  +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End " &
           "  + case when len(TSPL_COMPANY_MASTER.Email    )>0 then ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end " &
            " as Comp_Address " &
            "from TSPL_COMPANY_MASTER " +
"LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  " +
"LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State  " +
"where TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim dtComp As DataTable = clsDBFuncationality.GetDataTable(strQuery, tran)



            '=============Added by preeti Gupta Against Ticket No[ADV/01/05/18-000028]======== Ticket No : BHA/22/08/18-000474 by prabhakar work on rpt for Header_Discount_Amount
            strQuery = "select  Footer_Text  from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + StrFormID + "' "
            FooterText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Footer_Text  from TSPL_Crystal_Report_Footer_Setting where Frm_ID ='" + StrFormID + "'", tran))
            strQuery = "select TSPL_LOCATION_MASTER.Add1 as Location_Add1, TSPL_LOCATION_MASTER.Add2 as Location_Add2 , TSPL_LOCATION_MASTER.Add3 as Location_Add3 , TSPL_LOCATION_MASTER.Add4 as Location_Add4 , TSPL_LOCATION_MASTER.Telphone as Location_Telphone ,TSPL_VENDOR_MASTER.Email,TSPL_COMPANY_MASTER.CST_LST,TSPL_LOCATION_MASTER.Email as Location_Email, TSPL_LOCATION_MASTER.IsMainPlant as Location_IsMainPlant,TSPL_COMPANY_MASTER.Ecc_No,TSPL_COMPANY_MASTER.Circle_No, '" + clsCommon.myCstr(dtComp.Rows(0)("Company_Address")) + "' as Company_Address,'" + clsCommon.myCstr(dtComp.Rows(0)("Comp_Address")) + "' as Comp_Address,'" + clsCommon.myCstr(dtComp.Rows(0)("Add01")) + "' as Add01 ,'" + clsCommon.myCstr(dtComp.Rows(0)("add02")) + "' as add02 ,'" + clsCommon.myCstr(dtComp.Rows(0)("add03")) + "' as add03 ,'" + clsCommon.myCstr(dtComp.Rows(0)("PC")) + "' as PC ,'" + clsCommon.myCstr(dtComp.Rows(0)("CNo")) + "' as CNo ,'" + clsCommon.myCstr(dtComp.Rows(0)("Mail")) + "' as Mail ,'" + clsCommon.myCstr(dtComp.Rows(0)("CityName")) + "' as CityName ,'" + clsCommon.myCstr(dtComp.Rows(0)("MNo1")) + "' as MNo1 ,'" + clsCommon.myCstr(dtComp.Rows(0)("MNo2")) + "' as MNo2, isnull(TSPL_PURCHASE_ORDER_DETAIL.Detail_Discount_Amount,0) as Detail_Discount_Amount, isnull(tspl_purchase_order_head.Description,'') as Description,isnull(TSPL_PURCHASE_ORDER_HEAD.Header_Discount_Amount,0) as Header_Discount_Amount, Ship_Location.Add1 as ShipAdd1,Ship_Location.Add2 as ShipAdd2,Ship_Location.Add3 as ShipAdd3,Ship_Location.location_code as Ship_Loc_code,Ship_Location.location_desc as Ship_Loc_Des, isNull (TSPL_PURCHASE_ORDER_HEAD.Against_Vendor_Quotation, '') as Against_Vendor_Quotation  ,case when len ( isNull (TSPL_PURCHASE_ORDER_HEAD.Against_Vendor_Quotation, '')) > 0 then convert(varchar, TSPL_VENDOR_QUOTATION_HEAD.VQDate ,103) else  '' end as Vendor_Quotation_Date, '" + strAuthrozedBy + "' as AuthrozedBy,TSPL_STATE_MASTER_FOR_VENDOR.state_Name as Vend_State_Name,'" + Ho_Address + "' as HO_Address,TSPL_VENDOR_MASTER.State_Code AS Vendor_StateCode,tspl_state_master_for_location_state.state_code as Loc_StateCode, case when TSPL_PURCHASE_ORDER_DETAIL.Row_type='Misc' then isnull (TSPL_Additional_Charges.SAC_Code,'') else ISNULL(TSPL_ITEM_MASTER.HSN_Code,'')  end AS HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code, TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo , TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER_FOR_VENDOR.GST_STATE_Code AS Vendor_GST_StateCode, TSPL_PURCHASE_ORDER_HEAD.Created_By as Created_By_Name,TSPL_PURCHASE_ORDER_HEAD.Posted_By as Modify_By_Name, TSPL_PURCHASE_ORDER_HEAD.created_by,TSPL_PURCHASE_ORDER_HEAD.Posted_By as Modify_By , CurrencyMaster.CURRENCY_SIGN ,TSPL_PURCHASE_ORDER_HEAD.CURRENCY_CODE as CURRENCY_CODE_For_Symbol,createdUser.User_Name as createdUserName ,ModifyUser.User_Name as ModifyUserName ,PostedUser.User_Name as PostedUserName,TSPL_EX_PI_HEAD.Payment_Terms as PI_Payment_terms ,TSPL_EX_PI_HEAD.Terms_Code as Payment_Terms_Code,convert(varchar,TSPL_EX_PI_HEAD.Due_Date,103) as PI_Due_Date ,TSPL_PURCHASE_ORDER_HEAD.MT_PI_No ,convert(varchar,TSPL_EX_PI_HEAD.Document_Date,103) as PI_Date, tspl_state_master_for_location_state.state_name as Location_state_name,tspl_location_master.city_code  as Loca_city_name, case when ISNULL(TSPL_PURCHASE_ORDER_DETAIL.Qty_Desc,'')='' then CAST(CAST(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty As decimal(18,2)) AS varchar) else TSPL_PURCHASE_ORDER_DETAIL.Qty_Desc end as frm_Qty_Desc, case when isnull(TSPL_PURCHASE_ORDER_DETAIL.Rate_Desc,'')='' then TSPL_PURCHASE_ORDER_DETAIL.Item_Cost else TSPL_PURCHASE_ORDER_DETAIL.Rate_Desc end as  frm_Rate_Desc,tspl_purchase_order_head.IsContent,tspl_purchase_order_head.IsPO," + clsCommon.myCstr(customecount) + " as CustomCount,tspl_purchase_order_detail.remarks as detail_remark,tspl_purchase_order_head.auto_calculate,TSPL_PURCHASE_ORDER_DETAIL.item_cost+case when PurchaseOrder_Qty=0 then 1 else ( Amt_Less_Discount /PurchaseOrder_Qty )end as NetAmount,case when PurchaseOrder_Qty=0 then Item_Cost else ( Amt_Less_Discount /PurchaseOrder_Qty )end NetRate,tspl_company_master.cinno as Comp_CIN,tspl_purchase_order_head.item_type,TSPL_PURCHASE_ORDER_HEAD.payment_Terms,tspl_purchase_order_head.subject,tspl_purchase_order_head.content_Subject,tspl_purchase_order_head.Kind_Attentation,tspl_purchase_order_head.Comments,TSPL_PURCHASE_ORDER_DETAIL.Qty_Desc ,TSPL_PURCHASE_ORDER_DETAIL.Rate_Desc ,cast(TSPL_PURCHASE_ORDER_DETAIL.Amount_Desc as Float) as Amount_Desc,TSPL_PURCHASE_ORDER_HEAD.insurance_Terms,TSPL_PURCHASE_ORDER_HEAD.Delivery_Terms_Code,tspl_delivery_terms_master.description as Delivery_Terms_Desc,TSPL_VENDOR_MASTER.Add1 as VenAdd1,TSPL_VENDOR_MASTER.add2 as VenAdd2,TSPL_VENDOR_MASTER.Add3 as VenAdd3,TSPL_VENDOR_MASTER.City_Code_Desc as Vendor_City,TSPL_VENDOR_MASTER.State as Vendor_State,TSPL_VENDOR_MASTER.Pin_Code as Vendor_Pin,case when len(isnull(TSPL_VENDOR_MASTER.Add3,'')) > 0 then  TSPL_VENDOR_MASTER.Add3 else '' end + case when len( isnull(TSPL_VENDOR_MASTER.City_Code_Desc,'')) >0 then  case when len(isnull(TSPL_VENDOR_MASTER.Add3,'')) > 0 then ', '+ TSPL_VENDOR_MASTER.City_Code_Desc else TSPL_VENDOR_MASTER.City_Code_Desc  end else ' ' end + case when len(isnull( TSPL_VENDOR_MASTER.State,'')) >0 then  case when len( isnull(TSPL_VENDOR_MASTER.City_Code_Desc,'')) >0 then ', ' +TSPL_VENDOR_MASTER.State  else TSPL_VENDOR_MASTER.State end        else ' ' end  + Case when len(isnull(TSPL_VENDOR_MASTER.Pin_Code,'') ) > 0 then ' - '+TSPL_VENDOR_MASTER.Pin_Code else ' ' end as Vend_Add3_City_State_Pin,'' as FromDate,'" + FooterText + "' as FooterText,'' as Todate,'' as DocFilter,'' as VendorCodeFilter,'' as LocCodeFilter,PurchaseOrder_Type,case when PurchaseOrder_Type='J' then 'WORK ORDER' else case when PurchaseOrder_Type='L' or PurchaseOrder_Type='I' then 'PURCHASE ORDER'+(case when TSPL_PURCHASE_ORDER_HEAD.Confirmatory_PO_SRN_No is not null then ' [ Confirmatory ]' else '' end ) else '' end end as OrderWise,case when PurchaseOrder_Type='J' then 'THE ABOVE RATES ARE INCLUSIVE OF TAX' else case when PurchaseOrder_Type='L' then '6 MONTHS FROM THE DATE OF SUPPLY FOR ANY TYPE OF MANUFACTURING DEFECT ONLY.' else '' end end as Note,  TSPL_VENDOR_MASTER.add1 +case when len(TSPL_VENDOR_MASTER.add2)>0 then ', '+TSPL_VENDOR_MASTER.add2 else '' end +case when LEN(isnull(TSPL_VENDOR_MASTER.Add3,''))>0 then ', '+isnull(TSPL_VENDOR_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_VENDOR_MASTER.City_Code_Desc)>0 then ', '+TSPL_VENDOR_MASTER.City_Code_Desc else ' ' end + case when len(TSPL_VENDOR_MASTER.State )>0 then TSPL_VENDOR_MASTER.State else '' end  as address, TSPL_PURCHASE_ORDER_HEAD.Dept_Desc, Case WHen '" + objCommonVar.CurrentCompanyCode + "'='GUNTUR' Then TSPL_PURCHASE_ORDER_HEAD.Delivery_Duration Else TSPL_PURCHASE_ORDER_HEAD.Delivery_date End as [Delivery_date],TSPL_PURCHASE_ORDER_HEAD.Remarks ,TSPL_PURCHASE_ORDER_HEAD.Terms_Code,TSPL_PURCHASE_ORDER_HEAD.Terms_Remark,TSPL_PURCHASE_ORDER_HEAD.Mode_Of_Transport as  ModeofTransport,TSPL_PURCHASE_ORDER_DETAIL .Specification as  specification,TSPL_PURCHASE_ORDER_HEAD.Abandonment_No"
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                strQuery += ",getdate() as Abandonment_Date"
            Else
                strQuery += ",(select max(TSPL_PURCHASE_ORDER_HEAD_Hist.Abandonment_Date) from TSPL_PURCHASE_ORDER_HEAD_Hist where TSPL_PURCHASE_ORDER_HEAD_Hist.Abandonment_No=TSPL_PURCHASE_ORDER_HEAD.Abandonment_No and PurchaseOrder_No=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No) as Abandonment_Date"
            End If
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "SKR") = CompairStringResult.Equal Then
                strQuery += " ,TSPL_PURCHASE_ORDER_HEAD.RefTendorNo ,TSPL_TENDER_HEADER.DocumentDate as RALDATE"
            End If
            strQuery += ",TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No as purchase_no ,convert(varchar,TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Date,103) as po_date ,case TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_Type when 'L'then 'Local' when 'I' then 'Import' when 'J' then 'Job Work' when 'O' then 'Open' when 'S' then 'Specific'else 'Null' end as po_type ,tspl_purchase_order_head.vendor_name,TSPL_PURCHASE_ORDER_HEAD .Vendor_Code as vendor_type,TSPL_PURCHASE_ORDER_HEAD .Terms_Code as termscode ,TSPL_PURCHASE_ORDER_HEAD .Ref_No as ref_no ,TSPL_PURCHASE_ORDER_HEAD .Comments as comments ,TSPL_PURCHASE_ORDER_HEAD.Comment1,TSPL_PURCHASE_ORDER_HEAD.Comment2, TSPL_PURCHASE_ORDER_HEAD.Comment3,TSPL_PURCHASE_ORDER_HEAD.Comment4,TSPL_PURCHASE_ORDER_HEAD.Comment5,TSPL_PURCHASE_ORDER_HEAD.Comment6,TSPL_PURCHASE_ORDER_HEAD.Comment7,TSPL_PURCHASE_ORDER_HEAD.Comment8,TSPL_PURCHASE_ORDER_HEAD.Comment9,TSPL_PURCHASE_ORDER_HEAD.Comment10,TSPL_PURCHASE_ORDER_HEAD.Comment11,TSPL_PURCHASE_ORDER_HEAD.Comment12,TSPL_PURCHASE_ORDER_HEAD.Comment13,TSPL_PURCHASE_ORDER_HEAD.Comment14,TSPL_LOCATION_MASTER.Range_Name,TSPL_LOCATION_MASTER.Range_Address,TSPL_PURCHASE_ORDER_HEAD .Discount_Amt as dis_amt,TSPL_PURCHASE_ORDER_DETAIL.Disc_Amt as dis_amt1,TSPL_PURCHASE_ORDER_HEAD.Amount_Less_Discount  as aftrdiscount ,TSPL_PURCHASE_ORDER_HEAD .PO_Total_Amt as Total_amount,TSPL_PURCHASE_ORDER_HEAD.Discount_Base as bfrdisc_amount,tax1.Tax_Code_Desc as tax1name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax1_amt,0) as txt1amt,tax2.Tax_Code_Desc as tax2name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax2_amt,0) as txt2amt,tax3.Tax_Code_Desc as tax3name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax3_amt,0) as txt3amt,tax4.Tax_Code_Desc as tax4name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax4_amt,0) as txt4amt,tax5.Tax_Code_Desc as tax5name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax5_amt,0) as txt5amt,tax6.Tax_Code_Desc as tax6name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax6_amt,0) as txt6amt,tax7.Tax_Code_Desc as tax7name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax7_amt,0) as txt7amt,tax8.Tax_Code_Desc as tax8name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax8_amt,0) as txt8amt,isnull (TSPL_PURCHASE_ORDER_HEAD.tax9_amt,0) as txt9amt,tax10.Tax_Code_Desc as tax10name,isnull (TSPL_PURCHASE_ORDER_HEAD.tax10_amt,0) as txt10amt,isnull(TSPL_PURCHASE_ORDER_HEAD .Total_Tax_Amt,0) as total_tax_amt, TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount,  " &
              " TSPL_LOCATION_MASTER.HOAdd1, TSPL_LOCATION_MASTER.HOAdd2, " &
            " TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name1 as Add1Desc, " &
            " case when len(TSPL_COMPANY_MASTER.Pan_No) >0 then cast (TSPL_COMPANY_MASTER.Pan_No as varchar) else '' end as PAN_NO , " &
            "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt1,0) as Add1, " &
            "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name2 as Add2Desc, " &
             "   isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt2,0) as Add2, " &
             "   TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name3 as Add3Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt3,0) as Add3, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name4 as Add4Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt4,0) as Add4, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name5 as Add5Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt5,0) as Add5, " &
             "   TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name6 as Add6Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt6,0) as Add6, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name7 as Add7Desc, " &
             "   isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt7,0) as Add7, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name8 as Add8Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt8,0) as Add8, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name9 as Add9Desc, " &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt9,0) as Add9, " &
              "  TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Name10 as Add10Desc, " &
              "case when TSPL_PURCHASE_ORDER_DETAIL.TAX1='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX2='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX3='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX4='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX5='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX6='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX7='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX8='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX9='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate  WHEN TSPL_PURCHASE_ORDER_DETAIL.TAX10='EXCISE' THEN TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate END AS Exci_Item_TaxRate ," &
              "  isnull (TSPL_PURCHASE_ORDER_HEAD.Add_Charge_Amt10,0) as Add10,(case when TSPL_PURCHASE_ORDER_HEAD.Emergency=0 then '-N' else '-E' end) as Emergency,TSPL_PURCHASE_ORDER_DETAIL.Total_Tax_Amt,TSPL_PURCHASE_ORDER_DETAIL.Item_Net_Amt,  " &
             " TSPL_PURCHASE_ORDER_HEAD.PO_Total_Amt as DocAmt,TSPL_COMPANY_MASTER.Add1 as Comp_Add1 ,TSPL_COMPANY_MASTER.Add2 as Comp_Add2 ,TSPL_COMPANY_MASTER.Add3 as Comp_Add3, ( case when TSPL_COMPANY_MASTER.Phone2  <> '' then TSPL_COMPANY_MASTER.Phone1 +','+TSPL_COMPANY_MASTER.Phone2 else TSPL_COMPANY_MASTER.Phone1 end) as Comp_Phn,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as Comp_Email ,TSPL_COMPANY_MASTER.Tin_No as Comp_TinNo,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.CURRENCY_CODE,(TSPL_VENDOR_MASTER.Lst_No) as ven_lst_no ,(TSPL_VENDOR_MASTER.CST)as ven_cst ,(TSPL_VENDOR_MASTER.Tin_No)as ven_tin_no,TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location , (case when TSPL_PURCHASE_ORDER_HEAD.status =1 then TSPL_PURCHASE_ORDER_HEAD.Modify_By else '' end) as Modify_By,TSPL_SHIP_TO_LOCATION.add1 +case when len(TSPL_SHIP_TO_LOCATION.add2)>0 then ', '+TSPL_SHIP_TO_LOCATION.add2 else '' end +case when LEN(isnull(TSPL_SHIP_TO_LOCATION.Add3,''))>0 then ', '+isnull(TSPL_SHIP_TO_LOCATION.Add3,'') else ' ' end   as Ship_address,TSPL_PURCHASE_ORDER_HEAD.Created_By ,TSPL_PURCHASE_ORDER_HEAD.Modify_By ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_SHIP_TO_LOCATION.Ship_To_Desc  ,TSPL_PURCHASE_ORDER_HEAD.Terms_Code ,TSPL_PURCHASE_ORDER_HEAD.Delivery_date , " &
             " TSPL_PURCHASE_ORDER_HEAD.TAX1,TSPL_PURCHASE_ORDER_HEAD.TAX2,TSPL_PURCHASE_ORDER_HEAD.TAX3,TSPL_PURCHASE_ORDER_HEAD.TAX4,TSPL_PURCHASE_ORDER_HEAD.TAX5 ,TSPL_PURCHASE_ORDER_HEAD. Ship_To_Location, " &
             " TSPL_LOCATION_MASTER.Location_Desc as compname,TSPL_LOCATION_MASTER.Registration_Number,TSPL_LOCATION_MASTER.Registration_Number as VAT_No,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_LOCATION_MASTER.add1 as Loc_Add1, TSPL_LOCATION_MASTER.add2 +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add4,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add4,'') else ' ' end  as address1 ,TSPL_LOCATION_MASTER.TAN_No as Loc_FaxNo, TSPL_VENDOR_MASTER.Email as VenEmail,TSPL_PURCHASE_ORDER_DETAIL.item_code as item_code, TSPL_VENDOR_ITEM_DETAIL.vendor_item_no as VendorItem, "
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GMD") = CompairStringResult.Equal Then
                strQuery = strQuery + " ((TSPL_PURCHASE_ORDER_DETAIL.item_desc) +(case when TSPL_PURCHASE_ORDER_DETAIL.Specification='' then '' else ' . ' end) +TSPL_PURCHASE_ORDER_DETAIL.Specification+ (case when TSPL_PURCHASE_ORDER_DETAIL.Remarks='' then '' else ' / ' end) + TSPL_PURCHASE_ORDER_DETAIL.Remarks +(case when TSPL_PURCHASE_ORDER_detail.Capacity='' then '' else ' / ' end) + TSPL_PURCHASE_ORDER_detail.Capacity  +(case when TSPL_PURCHASE_ORDER_detail.Make='' then '' else ' / ' end) + TSPL_PURCHASE_ORDER_detail.Make +(case when TSPL_PURCHASE_ORDER_detail.Model='' then '' else ' / ' end) + TSPL_PURCHASE_ORDER_detail.Model )  as itemdesc "
            Else
                strQuery = strQuery + " ((TSPL_PURCHASE_ORDER_DETAIL.item_desc) +(case when TSPL_PURCHASE_ORDER_DETAIL.Specification='' then '' else ' . ' end) +TSPL_PURCHASE_ORDER_DETAIL.Specification+ (case when TSPL_PURCHASE_ORDER_DETAIL.Remarks='' then '' else ' / ' end) + TSPL_PURCHASE_ORDER_DETAIL.Remarks )  as itemdesc "
            End If
            strQuery = strQuery + " ,TSPL_TERMS_MASTER.Terms_Desc  as termsdesc,TSPL_PURCHASE_ORDER_DETAIL.Row_Type,TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_qty as qty,TSPL_PURCHASE_ORDER_DETAIL.unit_code as uom,TSPL_PURCHASE_ORDER_DETAIL.item_cost as itemcost,TSPL_PURCHASE_ORDER_DETAIL.amount as amount,TSPL_PURCHASE_ORDER_DETAIL.MRP ,TSPL_PURCHASE_ORDER_DETAIL.Item_Cost ,case when TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount=0 then 0 else (TSPL_PURCHASE_ORDER_DETAIL.Total_Tax_Amt /TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount) *100 end as Tax,case when TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount=0 then TSPL_PURCHASE_ORDER_DETAIL.Item_Cost else ((((TSPL_PURCHASE_ORDER_DETAIL.Total_Tax_Amt /TSPL_PURCHASE_ORDER_DETAIL.Amt_Less_Discount) *100)*TSPL_PURCHASE_ORDER_DETAIL.Item_Cost/100) +TSPL_PURCHASE_ORDER_DETAIL.Item_Cost) end as landing_Rate,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,TSPL_PURCHASE_ORDER_DETAIL.Item_Net_Amt,TSPL_PURCHASE_ORDER_HEAD.TAX1_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX2_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX3_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX4_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX5_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX6_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX7_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX8_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX9_Rate ,TSPL_PURCHASE_ORDER_HEAD.TAX10_Rate ,TSPL_PURCHASE_ORDER_DETAIL.Disc_Per as 'dis_per',TSPL_LOCATION_MASTER.CST_No as CST_LST ,TSPL_LOCATION_MASTER.TIN_No, TSPL_LOCATION_MASTER.Ecc_Number as ExciseNo, TSPL_LOCATION_MASTER.Range_Code as Range, TSPL_LOCATION_MASTER.Commissionerate as DivisionCommission,  " &
             "Case when len(ISNULL(TSPL_VENDOR_MASTER.Phone1,''))>0 and TSPL_VENDOR_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_VENDOR_MASTER.Phone1 end " &
            "  +  Case When   ISNULL(TSPL_VENDOR_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_VENDOR_MASTER.Phone2 Else'' End  as VenPhone1 , TSPL_VENDOR_MASTER.Fax as VenFax,TSPL_VENDOR_MASTER.Tin_No as Vendor_Tin_No, Case when len(ISNULL(TSPL_LOCATION_MASTER.Phone1,''))>0 and TSPL_LOCATION_MASTER.Phone1='(+__)__________' then '' else ' '+TSPL_LOCATION_MASTER.Phone1 end " &
            "  +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_LOCATION_MASTER.Phone2 Else'' End  as Phn,TSPL_LOCATION_MASTER.TAN_No as faxno,TSPL_LOCATION_MASTER.Email as EmailId, TSPL_PURCHASE_ORDER_DETAIL.Unit_code,Circle_No as APGST,TSPL_PURCHASE_ORDER_HEAD.Total_Add_Charge as total_add_charges,TSPL_PURCHASE_ORDER_HEAD.Against_Requisition as ReqNo,Convert(varchar(15),TSPL_REQUISITION_HEAD.Requisition_Date,103)  as ReqDt ,TSPL_PURCHASE_ORDER_HEAD.Quotation_No,convert(varchar,TSPL_PURCHASE_ORDER_HEAD.Quotation_Date,103) as Quotation_Date,TSPL_PURCHASE_ORDER_DETAIL .Location, TSPL_PURCHASE_ORDER_HEAD.Expiry_Date,0 As AmdNo,Null As Abd_Date " &
             " " & QryShowStatus & " " &
             " ,CurrencyMaster.CURRENCY_SIGN, " &
             " taxRate1.Tax_Rate_Desc as Tax_Rate_Desc1,taxRate2.Tax_Rate_Desc as Tax_Rate_Desc2,taxRate3.Tax_Rate_Desc as Tax_Rate_Desc3, " &
             " taxRate4.Tax_Rate_Desc as Tax_Rate_Desc4,taxRate5.Tax_Rate_Desc as Tax_Rate_Desc5,taxRate6.Tax_Rate_Desc as Tax_Rate_Desc6, " &
             " taxRate7.Tax_Rate_Desc as Tax_Rate_Desc7,taxRate8.Tax_Rate_Desc as Tax_Rate_Desc8,taxRate9.Tax_Rate_Desc as Tax_Rate_Desc9, " &
            " taxRate10.Tax_Rate_Desc as Tax_Rate_Desc10 , TSPL_LOCATION_MASTER.add2 as Loc_Add2 ,TSPL_LOCATION_MASTER.Add3 as Loc_Add3,TSPL_LOCATION_MASTER.Add4 as Loc_Add4,TSPL_PURCHASE_ORDER_HEAD.Currency_Code " &
            " ,tax1.type as Tax1Type,tax2.Type AS Tax2Type,tax3.Type AS Tax3Type,tax4.Type AS Tax4Type,tax5.Type AS Tax5Type,tax6.type  AS Tax6Type, tax7.type  AS Tax7Type,tax8.type  AS Tax8Type,tax9.type  AS Tax9Type,tax10.type  AS Tax10Type " &
            " ,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX1_Amt,0) as DTAX1_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX2_Amt,0) as DTAX2_AMT," &
            " ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX3_Amt,0) as DTAX3_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX4_Amt,0) as DTAX4_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX5_Amt,0) as DTAX5_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX6_Amt, 0)as DTAX6_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX7_Amt,0) as DTAX7_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX8_Amt,0) as DTAX8_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX9_Amt,0) as DTAX9_AMT,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX10_Amt,0) as DTAX10_AMT," &
            " ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX1_Rate,0) AS DTAX1_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX2_Rate,0) AS DTAX2_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX3_Rate,0) AS DTAX3_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX4_Rate,0) AS DTAX4_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX5_Rate,0) AS DTAX5_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX6_Rate,0) AS DTAX6_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX7_Rate,0) AS DTAX7_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX8_Rate,0) AS DTAX8_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX9_Rate,0) AS DTAX9_Rate,ISNULL(TSPL_PURCHASE_ORDER_DETAIL.TAX10_Rate,0) AS DTAX10_Rate " &
            " ,TSPL_PURCHASE_ORDER_HEAD.Insurance,TSPL_PURCHASE_ORDER_HEAD.Freight,TSPL_PURCHASE_ORDER_HEAD.Packing_Forward,isnull(TSPL_PURCHASE_ORDER_HEAD.ReferencePO,'') as ReferencePO " &
            " ,isnull(tspl_Gl_segment_code.Email_Department,'') as Email_Department" &
            " ,tspl_vendor_master.Account_No,tspl_vendor_master.bank_name,tspl_vendor_master.IFSC_CODE,tspl_vendor_master.Branch_Name " &
            " from TSPL_PURCHASE_ORDER_DETAIL " & Environment.NewLine +
            " left outer join TSPL_PURCHASE_ORDER_HEAD  on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No =TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No"
            If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "SKR") = CompairStringResult.Equal Then
                    strQuery += "   left outer join TSPL_TENDER_HEADER  on TSPL_TENDER_HEADER.DocumentCode =TSPL_PURCHASE_ORDER_HEAD.RefTendorNo"
             END If
        strQuery +=
            " left outer join tspl_Gl_segment_code on tspl_Gl_segment_code.Segment_code=TSPL_PURCHASE_ORDER_HEAD.Dept and tspl_Gl_segment_code.Seg_No=3 " & Environment.NewLine +
            " Left Outer Join TSPL_VENDOR_ITEM_DETAIL ON TSPL_VENDOR_ITEM_DETAIL.item_no=TSPL_PURCHASE_ORDER_DETAIL.Item_Code AND TSPL_VENDOR_ITEM_DETAIL.vendor_code=TSPL_PURCHASE_ORDER_HEAD.Vendor_Code AND TSPL_VENDOR_ITEM_DETAIL.Location_Code=TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location" & Environment.NewLine +
            " left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_PURCHASE_ORDER_HEAD.Against_Requisition" & Environment.NewLine +
            " left outer join TSPL_TAX_MASTER as tax1 on tax1.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax1" & Environment.NewLine +
            " left outer join tspl_tax_master as tax2 on tax2.tax_code = TSPL_PURCHASE_ORDER_HEAD.tax2" & Environment.NewLine +
            " left outer join tspl_tax_master as tax3 on tax3.Tax_Code=TSPL_PURCHASE_ORDER_HEAD .TAX3" & Environment.NewLine +
            " left outer join TSPL_TAX_MASTER as tax4 on tax4.Tax_Code= TSPL_PURCHASE_ORDER_HEAD .tax4" & Environment.NewLine +
            " left outer join TSPL_TAX_MASTER as tax5 on tax5.Tax_Code=TSPL_PURCHASE_ORDER_HEAD .tax5" & Environment.NewLine +
            " left outer join TSPL_TAX_MASTER as tax6 on tax6.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX6" & Environment.NewLine +
            " left outer join TSPL_TAX_MASTER as tax7 on tax7.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX7" & Environment.NewLine +
            " left outer join TSPL_TAX_MASTER as tax8 on tax8.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX8" & Environment.NewLine +
            " left outer join TSPL_TAX_MASTER as tax9 on tax9.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX9" & Environment.NewLine +
            " left outer join TSPL_TAX_MASTER as tax10 on tax10.Tax_Code =TSPL_PURCHASE_ORDER_HEAD .TAX10" & Environment.NewLine +
            " left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_PURCHASE_ORDER_HEAD.comp_code" & Environment.NewLine +
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_PURCHASE_ORDER_HEAD.Vendor_Code" & Environment.NewLine +
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code=  TSPL_PURCHASE_ORDER_HEAD.Bill_To_Location" & Environment.NewLine +
            " left outer join TSPL_SHIP_TO_LOCATION on TSPL_PURCHASE_ORDER_HEAD.Ship_To_Location =TSPL_SHIP_TO_LOCATION.Ship_To_Code" & Environment.NewLine +
            " left outer join TSPL_TERMS_MASTER on TSPL_PURCHASE_ORDER_HEAD.Terms_Code =TSPL_TERMS_MASTER.Terms_Code " & Environment.NewLine +
            " left join tspl_currency_master on tspl_currency_master.CURRENCY_CODE =TSPL_PURCHASE_ORDER_HEAD.CURRENCY_CODE" & Environment.NewLine +
            " left outer join TSPL_TAX_rates as taxRate1 on taxRate1.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax1 and taxRate1.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX1_Rate and taxRate1.Tax_Type='P' " & Environment.NewLine +
            " left outer join TSPL_TAX_rates as taxRate2 on taxRate2.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax2 and taxRate2.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX2_Rate and taxRate2.Tax_Type='P'  " & Environment.NewLine +
            " left outer join TSPL_TAX_rates as taxRate3 on taxRate3.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax3 and taxRate3.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX3_Rate and taxRate3.Tax_Type='P'  " & Environment.NewLine +
            " left outer join TSPL_TAX_rates as taxRate4 on taxRate4.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax4 and taxRate4.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX4_Rate and taxRate4.Tax_Type='P' " & Environment.NewLine +
            " left outer join TSPL_TAX_rates as taxRate5 on taxRate5.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax5 and taxRate5.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX5_Rate and taxRate5.Tax_Type='P' " & Environment.NewLine +
            " left outer join TSPL_TAX_rates as taxRate6 on taxRate6.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax6 and taxRate6.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX6_Rate and taxRate6.Tax_Type='P' " & Environment.NewLine +
            " left outer join TSPL_TAX_rates as taxRate7 on taxRate7.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax7 and taxRate7.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX7_Rate and taxRate7.Tax_Type='P'  " & Environment.NewLine +
            " left outer join TSPL_TAX_rates as taxRate8 on taxRate8.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax8 and taxRate8.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX8_Rate and taxRate8.Tax_Type='P' " & Environment.NewLine +
            " left outer join TSPL_TAX_rates as taxRate9 on taxRate9.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax9 and taxRate9.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX9_Rate and taxRate9.Tax_Type='P'  " & Environment.NewLine +
            " left outer join TSPL_TAX_rates as taxRate10 on taxRate10.tax_code =TSPL_PURCHASE_ORDER_HEAD.tax10 and taxRate10.tax_rate=TSPL_PURCHASE_ORDER_HEAD.TAX10_Rate and taxRate10.Tax_Type='P'    " & Environment.NewLine +
             " left outer join tspl_delivery_terms_master on tspl_delivery_terms_master.code=TSPL_PURCHASE_ORDER_HEAD.Delivery_Terms_Code" & Environment.NewLine +
            " left outer join tspl_state_master as tspl_state_master_for_location_state on   tspl_state_master_for_location_state.state_code=tspl_location_master.state" & Environment.NewLine +
             " left outer join TSPL_EX_PI_HEAD  on TSPL_EX_PI_HEAD.Document_Code =TSPL_PURCHASE_ORDER_HEAD.MT_PI_No" & Environment.NewLine +
             " left outer join TSPL_CURRENCY_MASTER as CurrencyMaster on CurrencyMaster.CURRENCY_CODE =TSPL_VENDOR_MASTER.CURRENCY_CODE" & Environment.NewLine +
             " left outer join TSPL_USER_MASTER as createdUser on createdUser.User_Code =TSPL_PURCHASE_ORDER_HEAD.Created_By " & Environment.NewLine +
              " left outer join TSPL_USER_MASTER as ModifyUser on ModifyUser.User_Code =TSPL_PURCHASE_ORDER_HEAD.Modify_By " & Environment.NewLine +
              " left outer join TSPL_USER_MASTER as PostedUser  on   PostedUser.User_Code =   TSPL_PURCHASE_ORDER_HEAD.Posted_By   " & Environment.NewLine +
                "left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_FOR_VENDOR ON TSPL_VENDOR_MASTER.State_Code=TSPL_STATE_MASTER_FOR_VENDOR.STATE_CODE " & Environment.NewLine +
                "LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_PURCHASE_ORDER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & Environment.NewLine +
                " left outer join TSPL_VENDOR_QUOTATION_HEAD on TSPL_VENDOR_QUOTATION_HEAD.Code = TSPL_PURCHASE_ORDER_HEAD.Against_Vendor_Quotation " & Environment.NewLine +
                "left outer join tspl_location_master as Ship_Location on TSPL_PURCHASE_ORDER_HEAD.Ship_To_Location =Ship_Location.location_code  " & Environment.NewLine +
                " left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code = TSPL_PURCHASE_ORDER_DETAIL.Item_Code and TSPL_PURCHASE_ORDER_DETAIL.Row_Type='Misc' " & Environment.NewLine +
            " where 2=2"
        strQuery += " and TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No in ('" + StrDocNo + "') "
            strQuery += " order by TSPL_PURCHASE_ORDER_HEAD .PurchaseOrder_No ,TSPL_PURCHASE_ORDER_DETAIL .Line_No"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery, tran)

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data found to print")
        End If

        dt.Columns.Add("BarCodeImage", GetType(Byte()))
        For Each dr As DataRow In dt.Rows
            dr("BarCodeImage") = bytes
        Next

        '===================detail taxes'''''''''''''''''''''''''
        Dim dt3_qry As String = "select final.* from (select PurchaseOrder_No,TAX1,TAX1_Rate,sum(tax_amt) as tax_amt from ( " &
        " select PurchaseOrder_No,tax1,TAX1_Rate,sum(tax1_amt) as tax_amt from TSPL_PURCHASE_ORDER_DETAIL group by tax1,TAX1_Rate,PurchaseOrder_No " &
        " union all " &
        " select PurchaseOrder_No,tax2,TAX2_Rate,sum(tax2_amt) as tax_amt from TSPL_PURCHASE_ORDER_DETAIL group by tax2,TAX2_Rate,PurchaseOrder_No " &
        " union all " &
        " select PurchaseOrder_No,tax3,TAX3_Rate,sum(tax3_amt) as tax_amt from TSPL_PURCHASE_ORDER_DETAIL group by tax3,TAX3_Rate,PurchaseOrder_No " &
        " union all " &
        " select PurchaseOrder_No,tax4,TAX4_Rate,sum(tax4_amt) as tax_amt from TSPL_PURCHASE_ORDER_DETAIL group by tax4,TAX4_Rate,PurchaseOrder_No " &
        " union all  " &
        " select PurchaseOrder_No,tax5,TAX5_Rate,sum(tax5_amt) as tax_amt from TSPL_PURCHASE_ORDER_DETAIL group by tax5,TAX5_Rate,PurchaseOrder_No " &
        " union all" &
        " select PurchaseOrder_No,tax6,TAX6_Rate,sum(tax6_amt) as tax_amt from TSPL_PURCHASE_ORDER_DETAIL group by tax6,TAX6_Rate,PurchaseOrder_No " &
        " union all " &
        " select PurchaseOrder_No,tax7,TAX7_Rate,sum(tax7_amt) as tax_amt from TSPL_PURCHASE_ORDER_DETAIL group by tax7,TAX7_Rate,PurchaseOrder_No " &
        " union all " &
        " select PurchaseOrder_No,tax8,TAX8_Rate,sum(tax8_amt) as tax_amt from TSPL_PURCHASE_ORDER_DETAIL group by tax8,TAX8_Rate,PurchaseOrder_No " &
        " union all " &
        " select PurchaseOrder_No,tax9,TAX9_Rate,sum(tax9_amt) as tax_amt from TSPL_PURCHASE_ORDER_DETAIL group by tax9,TAX9_Rate,PurchaseOrder_No " &
        " union all " &
        " select PurchaseOrder_No,tax10,TAX10_Rate,sum(tax10_amt) as tax_amt from TSPL_PURCHASE_ORDER_DETAIL group by tax10,TAX10_Rate,PurchaseOrder_No " &
        " )a where len(isnull(tax1,''))>0 and PurchaseOrder_No='" + StrDocNo + "'  group by PurchaseOrder_No,TAX1,TAX1_Rate )final" &
        " left outer join (select * from (select 1 as sno,purchaseorder_no,tax1 from tspl_purchase_order_head where isnull(tax1,'')<>'' union all select 2 as sno,purchaseorder_no,tax2 from tspl_purchase_order_head where isnull(tax2,'')<>'' union all select 3 as sno,purchaseorder_no,tax3 from tspl_purchase_order_head where isnull(tax3,'')<>'' union all select 4 as sno,purchaseorder_no,tax4 from tspl_purchase_order_head where isnull(tax4,'')<>'' union all select 5 as sno,purchaseorder_no,tax5 from tspl_purchase_order_head where isnull(tax5,'')<>'' union all select 6 as sno,purchaseorder_no,tax6 from tspl_purchase_order_head where isnull(tax6,'')<>'' union all select 7 as sno,purchaseorder_no,tax7 from tspl_purchase_order_head where isnull(tax7,'')<>'' union all select 8 as sno,purchaseorder_no,tax8 from tspl_purchase_order_head where isnull(tax8,'')<>'' union all select 9 as sno,purchaseorder_no,tax9 from tspl_purchase_order_head where isnull(tax9,'')<>'' union all select 10 as sno,purchaseorder_no,tax10 from tspl_purchase_order_head where isnull(tax10,'')<>'')s where s.purchaseorder_no='" + StrDocNo + "')ax on ax.purchaseorder_no=final.purchaseorder_no and ax.tax1=final.tax1 where final.Tax_amt > 0  order by ax.sno,final.tax1_rate "
        Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(dt3_qry, tran)
        ''==========================against[BM00000008380] Ticket : BHA/08/06/18-000045 , ERO/19/06/18-000353 For Print 
        If ((clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHBA") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "001") = CompairStringResult.Equal) Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GK") = CompairStringResult.Equal) Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "ADVANTEK") = CompairStringResult.Equal Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
            If IsMT = True Then
                Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "Merchant_PO-G", "Merchant Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "", "", dt3)
            Else

                '' Work done by Parteek on 13/08/2018
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "TSDDCF") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("item_type")), "N") = CompairStringResult.Equal Then
                        SetItemWiseTax(dt, StrDocNo, tran)
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "CustomField.rpt", "MMM.rpt", dt3)
                    End If
                End If
                ' ENd

                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHBA") = CompairStringResult.Equal AndAlso (clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") <> CompairStringResult.Equal) Then
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Vendor_StateCode")), clsCommon.myCstr(dt.Rows(0)("Loc_StateCode"))) = CompairStringResult.Equal Then
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "CustomField.rpt", "MMM.rpt", dt3)
                    Else
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "PO-G-Interstate", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "CustomField.rpt", "MMM.rpt", dt3)
                    End If
                End If

                '==============================Added by preeti Gupta Against Ticket No[ERO/19/04/18-000092]=========================
                If (clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("item_type")), "N") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("auto_calculate")), 0) = CompairStringResult.Equal) AndAlso Not clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "SPMMD") = CompairStringResult.Equal Then
                    If IsPO = True Then
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "CustomField.rpt", "MMM.rpt", dt3)
                    Else
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "CustomField.rpt", "MMM.rpt", dt3)
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal Then
                    If IsPO = True Then
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "CustomField.rpt", "MMM.rpt", dt3)
                    Else
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "WO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "CustomField.rpt", "MMM.rpt", dt3)
                    End If
                ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "L") = CompairStringResult.Equal Then
                    SetItemWiseTax(dt, StrDocNo, tran)
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "CustomField.rpt", "MMM.rpt", dt3)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "I") = CompairStringResult.Equal Then
                    SetItemWiseTax(dt, StrDocNo, tran)
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt1, "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "CustomField.rpt", "MMM.rpt", dt3)
                Else
                    Throw New Exception("Not a valid Po Type")
                End If
            End If
        ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "MPD") = CompairStringResult.Equal Then
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("item_type")), "N") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("auto_calculate")), 0) = CompairStringResult.Equal Then
                If IsPO = True Then
                    Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                Else
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt3, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "MMM.rpt", "rptCompanyAddress.rpt", clsERPFuncationality.CompanyAddresShowinFooter())
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal Then
                If IsPO = True Then
                    Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                Else
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "WO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt")
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "L") = CompairStringResult.Equal Then
                SetItemWiseTax(dt, StrDocNo, tran)
                'frmCRViewer.funreport(CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "PO-G", "Purchase Order")
                Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "I") = CompairStringResult.Equal Then
                SetItemWiseTax(dt, StrDocNo, tran)
                'frmCRViewer.funreport(CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "PO-G", "Purchase Order")
                Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt")
            Else
                Throw New Exception("Not a valid Po Type")
            End If
        ElseIf clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Or clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GHO") = CompairStringResult.Equal Then
            '======================================================================================'
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("item_type")), "N") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("auto_calculate")), 0) = CompairStringResult.Equal Then
                If clsERPFuncationality.GetGSTStatus(PO_Date) Then
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Vendor_StateCode")), clsCommon.myCstr(dt.Rows(0)("Loc_StateCode"))) = CompairStringResult.Equal Then
                        Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                    Else
                        Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G -InterState", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                    End If
                Else
                    If IsPO = True Then
                        Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                    Else
                        Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                    End If
                End If

            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal Then
                If clsERPFuncationality.GetGSTStatus(PO_Date) Then
                    If IsPO = True Then
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Vendor_StateCode")), clsCommon.myCstr(dt.Rows(0)("Loc_StateCode"))) = CompairStringResult.Equal Then
                            Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                        Else
                            Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G -InterState", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                        End If
                    Else
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Vendor_StateCode")), clsCommon.myCstr(dt.Rows(0)("Loc_StateCode"))) = CompairStringResult.Equal Then
                            Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "WO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                        Else
                            Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "WO-G -Interstate", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                        End If
                    End If
                Else
                    If IsPO = True Then
                        Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                    Else
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "WO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt")
                    End If
                End If

            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "L") = CompairStringResult.Equal Then
                If clsERPFuncationality.GetGSTStatus(PO_Date) Then
                    Dim isGSTSkip As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(" select  count (TSPL_PURCHASE_ORDER_DETAIL.Item_Code) as NoOfItem from TSPL_PURCHASE_ORDER_DETAIL inner join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_code = TSPL_PURCHASE_ORDER_DETAIL.Item_Code and TSPL_ITEM_MASTER.Skip_GST = 1 left outer join TSPL_PURCHASE_ORDER_head on TSPL_PURCHASE_ORDER_head.purchaseorder_no = TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_no where  TSPL_PURCHASE_ORDER_DETAIL.purchaseorder_no = '" + clsCommon.myCstr(dt.Rows(0)("purchase_no")) + "' ", tran))
                    If isGSTSkip = False Then
                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Vendor_StateCode")), clsCommon.myCstr(dt.Rows(0)("Loc_StateCode"))) = CompairStringResult.Equal Then
                            Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt3, "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "MMM.rpt")
                        Else
                            Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt3, "PO-G-Interstate", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "MMM.rpt")
                        End If
                    Else
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt3, "PO-G_Skip_GST", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "MMM.rpt")
                    End If

                Else
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt3, "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "MMM.rpt")
                End If

            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "I") = CompairStringResult.Equal Then
                If clsERPFuncationality.GetGSTStatus(PO_Date) Then
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Vendor_StateCode")), clsCommon.myCstr(dt.Rows(0)("Loc_StateCode"))) = CompairStringResult.Equal Then
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt3, "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "MMM.rpt")
                    Else
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, dt3, "PO-G-Interstate", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "MMM.rpt")
                    End If
                Else
                    SetItemWiseTax(dt, StrDocNo, tran)
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt", )
                End If
            Else
                Throw New Exception("Not a valid Po Type")
            End If
        Else
            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("item_type")), "N") = CompairStringResult.Equal And clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("auto_calculate")), 0) = CompairStringResult.Equal Then
                If IsPO = True Then
                    Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                Else
                    Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "J") = CompairStringResult.Equal Then
                If IsPO = True Then
                    Return frmCRViewer.funreport(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, EnumTecxpertPaperSize.NA, "JO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")))
                Else
                    'Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "WO-G", "Work Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt")
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(tran), "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                End If
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "L") = CompairStringResult.Equal Then
                SetItemWiseTax(dt, StrDocNo, tran)
                'Ticket No-MIL/20/08/19-000124,sanjay
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "GMD") = CompairStringResult.Equal Then
                    If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Vendor_StateCode")), clsCommon.myCstr(dt.Rows(0)("Loc_StateCode"))) = CompairStringResult.Equal Then
                        Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "PO_G_Interstate", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt")
                    End If
                End If
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "PLI") = CompairStringResult.Equal Then
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(tran), "PO-G-PLI", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                ElseIf clsCommon.CompairString(objCommonVar.CurrComp_Code1, "BKN") = CompairStringResult.Equal Then
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(tran), "PO-GBKN", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                ElseIf clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(tran), "PO-GALW", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                ElseIf clsCommon.CompairString(objCommonVar.CurrComp_Code1, "SKR") = CompairStringResult.Equal Then
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(tran), "PO-G-SKR", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)

                Else
                    Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(tran), "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
                End If
                'Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(tran), "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt", "MMM.rpt", dt3)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("PurchaseOrder_Type")), "I") = CompairStringResult.Equal Then
                SetItemWiseTax(dt, StrDocNo, tran)
                Return frmCRViewer.funsubreportWithdt(isPDFPath, CrystalReportFolder.PurchaseOrder, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "PO-G", "Purchase Order", clsCommon.myCDate(dt.Rows(0)("po_date")), "rptCompanyAddress.rpt")
            Else
                Throw New Exception("Not a valid Po Type")
            End If
        End If
    End Function

    Private Function SetItemWiseTax(ByVal dtAfterModify As DataTable, ByVal strShipFrm As String, ByVal tran As SqlTransaction) As DataTable
        dtAfterModify.Columns.Add("TAX1_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate4", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Rate5", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt3", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt4", GetType(Double))
        dtAfterModify.Columns.Add("TAX1_Amt5", GetType(Double))

        dtAfterModify.Columns.Add("TAX2_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate4", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Rate5", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt3", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt4", GetType(Double))
        dtAfterModify.Columns.Add("TAX2_Amt5", GetType(Double))

        dtAfterModify.Columns.Add("TAX3_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate4", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Rate5", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt3", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt4", GetType(Double))
        dtAfterModify.Columns.Add("TAX3_Amt5", GetType(Double))

        dtAfterModify.Columns.Add("TAX4_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate4", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Rate5", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt3", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt4", GetType(Double))
        dtAfterModify.Columns.Add("TAX4_Amt5", GetType(Double))

        dtAfterModify.Columns.Add("TAX5_Rate1", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate2", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate3", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate4", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Rate5", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt1", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt2", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt3", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt4", GetType(Double))
        dtAfterModify.Columns.Add("TAX5_Amt5", GetType(Double))



        Dim qry As String = "select Tax,Rate,SUM(Amt) as TaxAmt"
        qry += " from ("
        qry += " select TAX1 as Tax,TAX1_Rate as Rate,TAX1_Amt as Amt"
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "' "
        qry += " union all "
        qry += " select TAX2 as Tax,TAX2_Rate as Rate,TAX2_Amt as Amt "
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX3 as Tax,TAX3_Rate as Rate,TAX3_Amt as Amt "
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX4 as Tax,TAX4_Rate as Rate,TAX4_Amt as Amt "
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "'  "
        qry += " union all "
        qry += " select TAX5 as Tax,TAX5_Rate as Rate,TAX5_Amt as Amt "
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "'   "
        qry += " union all "
        qry += " select TAX6 as Tax,TAX6_Rate as Rate,TAX6_Amt as Amt "
        qry += " from TSPL_PURCHASE_ORDER_DETAIL where PurchaseOrder_No='" + strShipFrm + "'   "
        qry += " )xxx "
        qry += " group by Tax,Rate   having SUM(Amt)>0   "


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                For ii As Integer = 1 To 5
                    Dim strCol As String = "TAX" + clsCommon.myCstr(ii) + ""
                    If clsCommon.CompairString(clsCommon.myCstr(dtAfterModify.Rows(0)(strCol)), clsCommon.myCstr(dr("Tax"))) = CompairStringResult.Equal Then
                        If clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt1")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate1") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt1") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt2")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate2") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt2") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt3")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate3") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt3") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt4")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate4") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt4") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        ElseIf clsCommon.myCdbl(dtAfterModify.Rows(0)("TAX" + clsCommon.myCstr(ii) + "_Amt5")) <= 0 Then
                            For jj As Integer = 0 To dtAfterModify.Rows.Count - 1
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Rate5") = clsCommon.myCdbl(dr("Rate"))
                                dtAfterModify.Rows(jj)("TAX" + clsCommon.myCstr(ii) + "_Amt5") = clsCommon.myCdbl(dr("TaxAmt"))
                            Next
                        Else
                            Throw New Exception("Printing Support only 5 Diffent Rates")
                        End If

                    End If
                Next
            Next
        End If
        Return dtAfterModify
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal strFromID As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, strFromID, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal strFromID As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select 1 from TSPL_PURCHASE_ORDER_HEAD where PurchaseOrder_No='" + strCode + "' and Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Transaction status should be posted.")
            End If


            qry = "select distinct SRN_No from TSPL_SRN_DETAIL where PO_ID='" + strCode + "' union all select distinct GRN_No as SRN_No from TSPL_GRN_DETAIL where PO_Id='" + strCode + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                qry = "Purchase order used in following SRN"
                For Each dr As DataRow In dt.Rows
                    qry += Environment.NewLine + clsCommon.myCstr(dr("SRN_No"))
                Next
                qry += Environment.NewLine + "Can't unpost it"
                Throw New Exception(qry)
            End If

            qry = "update TSPL_PURCHASE_ORDER_HEAD set Status=0,Posting_Date=null where PurchaseOrder_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PURCHASE_ORDER_HEAD", "PurchaseOrder_No", trans)

            qry = "update TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL set is_reverse=1 where document_code='" + strCode + "' and trans_code='" + strFromID + "' and is_reverse=0"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function AgainstTender(ByVal strDocNo As String, ByVal DocTypeGRN1SRN2 As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        If DocTypeGRN1SRN2 = 1 Then ''GRN
            qry = "select 1 from TSPL_GRN_HEAD 
inner join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_GRN_HEAD.Against_PO
inner join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_PURCHASE_ORDER_HEAD.RefTendorNo
where TSPL_GRN_HEAD.GRN_No='" + strDocNo + "' and isnull(TSPL_TENDER_HEADER.Tender_Type,0) in (0,1)"
        ElseIf DocTypeGRN1SRN2 = 2 Then ''SRN
            qry = "select 1 from TSPL_SRN_DETAIL 
inner join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_SRN_DETAIL.PO_ID
inner join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_PURCHASE_ORDER_HEAD.RefTendorNo
where TSPL_SRN_DETAIL.SRN_No='" + strDocNo + "' and isnull(TSPL_TENDER_HEADER.Tender_Type,0) in (0,1)"
        Else
            Throw New Exception("Wrong DocTypeGRN1SRN2")
        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function
End Class

<Serializable()>
Public Class clsPurchaseOrderDetail
#Region "Variables"
    Public Capex_SubCode As String = Nothing
    Public Capex_Code As String = Nothing
    Public Emergency As Boolean = Nothing
    Public Category As String = Nothing
    Public Last_Same_Vendor_Rate As Double = Nothing
    Public Last_Other_Vendor_Rate As Double = Nothing
    Public PurchaseOrder_No As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Status As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public PurchaseOrder_Qty As Double = 0 '
    Public Requisition_Id As String = Nothing
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
    Public Header_Discount_Per As Decimal = 0
    Public Header_Discount_Amount As Decimal = 0
    Public Disc_Per As Double = 0
    Public Detail_Discount_Amount As Decimal = 0
    Public Disc_Per_Unit As Decimal = 0
    Public Disc_Amt_Per_Unit As Decimal = 0
    Public Disc_Amt As Double = 0
    Public Disc_Type As Integer = 0
    Public Shortage_Amount As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public Taxable_Amount As Double = 0
    Public Taxable_Amount_Per As Double = 0
    Public POTax_Group As String = Nothing 'Not a table field
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public MRP As Double = 0
    Public Assessable As Double = 0
    Public AssessableAmt As Double = 0
    Public IsUsedInGRN As Boolean = False
    Public AbatementRate As Decimal = 0
    Public AssessableMRP As Decimal = 0
    Public TotalAssessableMRP As Decimal = 0
    Public Bin_No As String = Nothing
    Public MRN_No As String = Nothing
    Public GRN_No_Temp As String = Nothing
    Public Qty_Desc As String = ""
    Public Rate_Desc As String = ""
    Public Amount_Desc As String = ""
    Public FatPer_MT As Double = 0
    Public SNFPer_MT As Double = 0
    Public FatKG_MT As Double = 0
    Public SNFKG_MT As Double = 0
    Public Item_Weight_MT As Double = 0
    Public Weight_UOM_MT As String = Nothing
    Public ItemAdd_Charge_Code1 As String = Nothing
    Public ItemAdd_Org_Charge_Amt1 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt1 As Double = Nothing
    Public ItemAdd_Charge_Code9 As String = Nothing
    Public ItemAdd_Org_Charge_Amt9 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt9 As Double = Nothing
    Public ItemAdd_Charge_Code8 As String = Nothing
    Public ItemAdd_Org_Charge_Amt8 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt8 As Double = Nothing
    Public ItemAdd_Charge_Code7 As String = Nothing
    Public ItemAdd_Org_Charge_Amt7 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt7 As Double = Nothing
    Public ItemAdd_Charge_Code6 As String = Nothing
    Public ItemAdd_Org_Charge_Amt6 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt6 As Double = Nothing
    Public ItemAdd_Charge_Code5 As String = Nothing
    Public ItemAdd_Org_Charge_Amt5 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt5 As Double = Nothing
    Public ItemAdd_Charge_Code4 As String = Nothing
    Public ItemAdd_Org_Charge_Amt4 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt4 As Double = Nothing
    Public ItemAdd_Charge_Code3 As String = Nothing
    Public ItemAdd_Org_Charge_Amt3 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt3 As Double = Nothing
    Public ItemAdd_Charge_Code2 As String = Nothing
    Public ItemAdd_Org_Charge_Amt2 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt2 As Double = Nothing
    Public ItemAdd_Charge_Code10 As String = Nothing
    Public ItemAdd_Org_Charge_Amt10 As Double = Nothing
    Public ItemAdd_Calc_Charge_Amt10 As Double = Nothing
    Public Total_ItemAdd_Charge As Double = Nothing
    Public Make As String = Nothing
    Public Model As String = Nothing
    Public Capacity As String = Nothing
    Public Against_Item_Wise_Tax_Rate As String = Nothing
    Public Insurance_Base_Amt As Decimal
    Public Insurance_Per As Decimal

    Public Item_Insurance_Base_Amt As Decimal = 0
    Public Item_Insurance_Apply_On As String = Nothing
    Public Item_Insurance_Rate As Decimal = 0
    Public Item_Insurance_Amt As Decimal = 0
    Public Item_Amt_After_Insurance As Decimal = 0
    '=============================
#End Region
    ''Note Very Important If any change mad in PO Head or PO Detail table allso update it's History table.
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsPurchaseOrderDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsPurchaseOrderDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Qty", obj.PurchaseOrder_Qty)
                clsCommon.AddColumnsForChange(coll, "Requisition_Id", obj.Requisition_Id, True)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)

                clsCommon.AddColumnsForChange(coll, "Header_Discount_Per", obj.Header_Discount_Per)
                clsCommon.AddColumnsForChange(coll, "Header_Discount_Amount", obj.Header_Discount_Amount)
                clsCommon.AddColumnsForChange(coll, "Disc_Per", obj.Disc_Per)
                clsCommon.AddColumnsForChange(coll, "Detail_Discount_Amount", obj.Detail_Discount_Amount)

                clsCommon.AddColumnsForChange(coll, "Disc_Per_Unit", obj.Disc_Per_Unit)
                clsCommon.AddColumnsForChange(coll, "Disc_Amt_Per_Unit", obj.Disc_Amt_Per_Unit)

                clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", obj.Amt_Less_Discount)

                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Base_Amt", obj.Item_Insurance_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Apply_On", obj.Item_Insurance_Apply_On)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Rate", obj.Item_Insurance_Rate)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Amt", obj.Item_Insurance_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Amt_After_Insurance", obj.Item_Amt_After_Insurance)

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

                clsCommon.AddColumnsForChange(coll, "Taxable_Amount", obj.Taxable_Amount)
                clsCommon.AddColumnsForChange(coll, "Taxable_Amount_Per", obj.Taxable_Amount_Per)

                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)

                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Assessable", obj.Assessable)
                clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)
                '' for abatement PO
                clsCommon.AddColumnsForChange(coll, "AbatementRate", obj.AbatementRate)
                clsCommon.AddColumnsForChange(coll, "AssessableMRP", obj.AssessableMRP)
                clsCommon.AddColumnsForChange(coll, "TotalAssessableMRP", obj.TotalAssessableMRP)
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.Bin_No)
                clsCommon.AddColumnsForChange(coll, "Last_Other_Vendor_Rate", obj.Last_Other_Vendor_Rate)
                clsCommon.AddColumnsForChange(coll, "Last_Same_Vendor_Rate", obj.Last_Same_Vendor_Rate)

                clsCommon.AddColumnsForChange(coll, "Qty_Desc", obj.Qty_Desc, True)
                clsCommon.AddColumnsForChange(coll, "Rate_Desc", obj.Rate_Desc, True)
                clsCommon.AddColumnsForChange(coll, "Amount_Desc", obj.Amount_Desc, True)
                ''richa agarwal 07/04/2015
                clsCommon.AddColumnsForChange(coll, "FatPer_MT", obj.FatPer_MT)
                clsCommon.AddColumnsForChange(coll, "SNFPer_MT", obj.SNFPer_MT)
                clsCommon.AddColumnsForChange(coll, "FatKG_MT", obj.FatKG_MT)
                clsCommon.AddColumnsForChange(coll, "SNFKG_MT", obj.SNFKG_MT)
                clsCommon.AddColumnsForChange(coll, "Item_Weight_MT", obj.Item_Weight_MT)
                clsCommon.AddColumnsForChange(coll, "Weight_UOM_MT", obj.Weight_UOM_MT)
                ''-----------------


                ''==========================19/10/2016======================
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code1", obj.ItemAdd_Charge_Code1)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt1", obj.ItemAdd_Org_Charge_Amt1)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt1", obj.ItemAdd_Calc_Charge_Amt1)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code9", obj.ItemAdd_Charge_Code9)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt9", obj.ItemAdd_Org_Charge_Amt9)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt9", obj.ItemAdd_Calc_Charge_Amt9)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code8", obj.ItemAdd_Charge_Code8)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt8", obj.ItemAdd_Org_Charge_Amt8)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt8", obj.ItemAdd_Calc_Charge_Amt8)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code7", obj.ItemAdd_Charge_Code7)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt7", obj.ItemAdd_Org_Charge_Amt7)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt7", obj.ItemAdd_Calc_Charge_Amt7)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code6", obj.ItemAdd_Charge_Code6)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt6", obj.ItemAdd_Org_Charge_Amt6)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt6", obj.ItemAdd_Calc_Charge_Amt6)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code5", obj.ItemAdd_Charge_Code5)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt5", obj.ItemAdd_Org_Charge_Amt5)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt5", obj.ItemAdd_Calc_Charge_Amt5)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code4", obj.ItemAdd_Charge_Code4)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt4", obj.ItemAdd_Org_Charge_Amt4)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt4", obj.ItemAdd_Calc_Charge_Amt4)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code3", obj.ItemAdd_Charge_Code3)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt3", obj.ItemAdd_Org_Charge_Amt3)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt3", obj.ItemAdd_Calc_Charge_Amt3)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code2", obj.ItemAdd_Charge_Code2)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt2", obj.ItemAdd_Org_Charge_Amt2)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt2", obj.ItemAdd_Calc_Charge_Amt2)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code10", obj.ItemAdd_Charge_Code10)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt10", obj.ItemAdd_Org_Charge_Amt10)
                clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt10", obj.ItemAdd_Calc_Charge_Amt10)
                clsCommon.AddColumnsForChange(coll, "Total_ItemAdd_Charge", obj.Total_ItemAdd_Charge)
                ''=================================================================
                clsCommon.AddColumnsForChange(coll, "Capacity", obj.Capacity)
                clsCommon.AddColumnsForChange(coll, "Make", obj.Make)
                clsCommon.AddColumnsForChange(coll, "Model", obj.Model)
                '==================================================================
                clsCommon.AddColumnsForChange(coll, "Against_Item_Wise_Tax_Rate", obj.Against_Item_Wise_Tax_Rate, True)

                clsCommon.AddColumnsForChange(coll, "Insurance_Base_Amt", obj.Insurance_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "Insurance_Per", obj.Insurance_Per)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PURCHASE_ORDER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalancePOQtyBySRN(ByVal strPOCode As String, ByVal strICode As String, ByVal strCurrGRNNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " &
            " select TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,1 as RI from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_DETAIL.Status=0 and TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No ='" + strPOCode + "' and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + strICode + "' and  TSPL_PURCHASE_ORDER_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_PURCHASE_ORDER_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'" &
            " union all " &
         " select  TSPL_SRN_DETAIL.Item_Code as ICode,((TSPL_SRN_DETAIL.SRN_Qty)+(TSPL_SRN_DETAIL.Leak_Qty)+(TSPL_SRN_DETAIL.Burst_Qty)+(TSPL_SRN_DETAIL.Short_Qty)) as Qty,-1 as RI from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No where TSPL_SRN_DETAIL.PO_Id='" + strPOCode + "'   and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_SRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_SRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_SRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' and TSPL_SRN_DETAIL.SRN_No not in ('" + strCurrGRNNo + "')  "
        '" union all " & _
        'qry += "   select  TSPL_GRN_DETAIL.Item_Code as ICode,((TSPL_GRN_DETAIL.GRN_Qty)+(TSPL_GRN_DETAIL.Leak_Qty)+(TSPL_GRN_DETAIL.Burst_Qty)+(TSPL_GRN_DETAIL.Short_Qty)) as Qty,-1 as RI from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_DETAIL.PO_Id='" + strPOCode + "'   and TSPL_GRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_GRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_GRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_GRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' and TSPL_GRN_DETAIL.GRN_No not in ('" + strCurrGRNNo + "')   " & _
        qry += " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
    Public Shared Function GetBalancePOQtyByGRN(ByVal strPOCode As String, ByVal strICode As String, ByVal strCurrGRNNo As String, ByVal strUOM As String, ByVal dblMRP As Double, ByVal dblAssessable As Double, Optional ByVal trans As SqlTransaction = Nothing) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " &
            " select TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty"
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoClosePOBasedOnSRNQtyWithTolerance, clsFixedParameterCode.AutoClosePOBasedOnSRNQtyWithTolerance, trans)) = 1 Then
            qry += " +((TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty * ISNULL(TSPL_ITEM_MASTER.tolerence ,0))/100)"
        End If
        qry += " as Qty,1 as RI from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No 
                left outer join tspl_item_master on tspl_item_master.item_code=TSPL_PURCHASE_ORDER_DETAIL.item_code
                where TSPL_PURCHASE_ORDER_DETAIL.Status=0 and TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No ='" + strPOCode + "' and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + strICode + "' and  TSPL_PURCHASE_ORDER_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_PURCHASE_ORDER_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_PURCHASE_ORDER_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "'" &
            " union all " +
         "   select  TSPL_GRN_DETAIL.Item_Code as ICode,((TSPL_GRN_DETAIL.GRN_Qty-TSPL_GRN_DETAIL.Tolerence_Qty)+(TSPL_GRN_DETAIL.Leak_Qty)+(TSPL_GRN_DETAIL.Burst_Qty)+(TSPL_GRN_DETAIL.Short_Qty)) as Qty,-1 as RI from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_DETAIL.PO_Id='" + strPOCode + "' and TSPL_GRN_HEAD.IsCancel=0   and TSPL_GRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_GRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_GRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and isnull(TSPL_GRN_DETAIL.Assessable,0)='" + clsCommon.myCstr(dblAssessable) + "' and TSPL_GRN_DETAIL.GRN_No not in ('" + strCurrGRNNo + "') and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0   " &
               "   union all select TSPL_PO_SCH_DETAIL.Item_Code as ICode,TSPL_PO_SCH_DETAIL.schedule_qty as Qty,-1 as RI from TSPL_PO_SCH_DETAIL left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code where TSPL_PO_SCH_DETAIL.PO_code='" + strPOCode + "'   and TSPL_PO_SCH_DETAIL.Item_Code='" + strICode + "' and  TSPL_PO_SCH_DETAIL.Unit_code='" + strUOM + "' and TSPL_PO_SCH_DETAIL.document_code not in ('" + strCurrGRNNo + "')   " &
               "   union all select TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,(TSPL_RGP_JOB_WORK_DETAIL.rgp_qty) as Qty,-1 as RI from TSPL_RGP_JOB_WORK_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no where TSPL_RGP_JOB_WORK_DETAIL.PO_id='" + strPOCode + "' and TSPL_RGP_JOB_WORK_DETAIL.Item_Code='" + strICode + "' and  TSPL_RGP_JOB_WORK_DETAIL.Unit_code='" + strUOM + "' and TSPL_RGP_JOB_WORK_DETAIL.rgp_no not in ('" + strCurrGRNNo + "') and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.Against_Schedule_Code,''))<=0   "
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.OpenPOforRejectShortageQty, clsFixedParameterCode.OpenPOforRejectShortageQty, trans)) = 1 Then
            qry += "union all " + Environment.NewLine +
            " select TSPL_SRN_DETAIL.Item_Code as ICode,(isnull(TSPL_SRN_DETAIL.Leak_Qty,0)+isnull(TSPL_SRN_DETAIL.Burst_Qty,0) +isnull(TSPL_SRN_DETAIL.Short_Qty,0)+isnull(TSPL_SRN_DETAIL.Rejected_Qty,0)) as Qty,1 as RI from TSPL_SRN_DETAIL   left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.SRN_No=TSPL_SRN_DETAIL.SRN_No " + Environment.NewLine +
            " where  TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and TSPL_SRN_DETAIL.Unit_code='" + strUOM + "' and isnull(TSPL_SRN_DETAIL.MRP,0)='" + clsCommon.myCstr(dblMRP) + "' and TSPL_SRN_DETAIL.PO_ID='" + strPOCode + "'" + Environment.NewLine +
            " and TSPL_SRN_DETAIL.GRN_ID not in ('" + strCurrGRNNo + "') and len(isnull(TSPL_SRN_DETAIL.PO_ID,''))>0 "
        End If
        qry += " union all " + Environment.NewLine +
               " select  TSPL_QC_CHECK_SRN_DETAIL.Item_Code as ICode,(coalesce(TSPL_GRN_DETAIL.GRN_Qty,0)-coalesce(TSPL_QC_CHECK_SRN_DETAIL.Reject_Qty,0)) as Qty,1 as RI from TSPL_QC_CHECK_SRN_DETAIL " &
               " left join TSPL_MRN_DETAIL on TSPL_QC_CHECK_SRN_DETAIL.MRN_No=TSPL_MRN_DETAIL.MRN_No and TSPL_QC_CHECK_SRN_DETAIL.Item_Code=TSPL_MRN_DETAIL.Item_Code " &
               " and TSPL_QC_CHECK_SRN_DETAIL.Unit_Code=TSPL_MRN_DETAIL.Unit_code " &
               " left join TSPL_GRN_DETAIL on TSPL_MRN_DETAIL.GRN_Id=TSPL_GRN_DETAIL.GRN_No and TSPL_MRN_DETAIL.Item_Code=TSPL_GRN_DETAIL.Item_Code " &
               " and TSPL_MRN_DETAIL.Unit_code=TSPL_GRN_DETAIL.Unit_code " &
               " left outer join TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Document_Code=TSPL_QC_CHECK_SRN_DETAIL.Document_Code " &
               " where TSPL_QC_CHECK_SRN_DETAIL.PO_No='" & strPOCode & "' and TSPL_QC_CHECK_HEAD.IsCancel=0 " &
               " and TSPL_QC_CHECK_SRN_DETAIL.Item_Code='" & strICode & "' and TSPL_GRN_DETAIL.GRN_No not in ('" + strCurrGRNNo + "') and  TSPL_QC_CHECK_SRN_DETAIL.Unit_code='" & strUOM & "' and (TSPL_QC_CHECK_SRN_DETAIL.Reject_Qty>0 or TSPL_QC_CHECK_SRN_DETAIL.OK_Qty<=0) and not exists (select TSPL_QC_CHECK_SRN_DETAIL.* from TSPL_QC_CHECK_APPROVAL_ENTRY where TSPL_QC_CHECK_SRN_DETAIL.Document_Code=TSPL_QC_CHECK_APPROVAL_ENTRY.Document_Code)"
        qry += " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function GetBalancePOQtyBySchedule(ByVal strPOCode As String, ByVal strICode As String, ByVal strDocumentNo As String, ByVal strUOM As String) As Double
        Dim qry As String = "select SUM(qty * RI) as Balance from(  " &
            " select TSPL_PURCHASE_ORDER_DETAIL.Item_Code as ICode,TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty as Qty,1 as RI from TSPL_PURCHASE_ORDER_DETAIL left outer join TSPL_PURCHASE_ORDER_HEAD on TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No=TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where TSPL_PURCHASE_ORDER_DETAIL.Status=0 and TSPL_PURCHASE_ORDER_HEAD.Status=1 and TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No ='" + strPOCode + "' and TSPL_PURCHASE_ORDER_DETAIL.Item_Code='" + strICode + "' and  TSPL_PURCHASE_ORDER_DETAIL.Unit_code='" + strUOM + "' " &
            " union all "

        qry += "   select  TSPL_GRN_DETAIL.Item_Code as ICode,((TSPL_GRN_DETAIL.GRN_Qty)+(TSPL_GRN_DETAIL.Leak_Qty)+(TSPL_GRN_DETAIL.Burst_Qty)+(TSPL_GRN_DETAIL.Short_Qty)) as Qty,-1 as RI from TSPL_GRN_DETAIL left outer join TSPL_GRN_HEAD on TSPL_GRN_HEAD.GRN_No=TSPL_GRN_DETAIL.GRN_No where TSPL_GRN_DETAIL.PO_Id='" + strPOCode + "' and TSPL_GRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_GRN_DETAIL.Unit_code='" + strUOM + "' and TSPL_GRN_DETAIL.GRN_No not in ('" + strDocumentNo + "') and len(isnull(TSPL_GRN_DETAIL.Against_RGP_No,''))<=0 and len(isnull(TSPL_GRN_DETAIL.Against_Schedule_Code,''))<=0   " &
               "   union all select TSPL_SRN_DETAIL.Item_Code as ICode,(TSPL_SRN_DETAIL.SRN_Qty+TSPL_SRN_DETAIL.rejected_qty+TSPL_SRN_DETAIL.leak_qty+TSPL_SRN_DETAIL.burst_qty+TSPL_SRN_DETAIL.short_qty) as Qty,-1 as RI from TSPL_SRN_DETAIL left outer join TSPL_SRN_HEAD on TSPL_SRN_HEAD.srn_no=TSPL_SRN_DETAIL.srn_no where TSPL_SRN_DETAIL.PO_id='" + strPOCode + "' and TSPL_SRN_DETAIL.Item_Code='" + strICode + "' and  TSPL_SRN_DETAIL.Unit_code='" + strUOM + "' and TSPL_SRN_DETAIL.srn_no not in ('" + strDocumentNo + "') and len(isnull(TSPL_SRN_DETAIL.Against_Schedule_Code,''))<=0 and len(isnull(TSPL_SRN_DETAIL.RGP_Id,''))<=0 and len(isnull(TSPL_SRN_DETAIL.MRN_Id,''))<=0   " &
               "   union all select TSPL_RGP_JOB_WORK_DETAIL.Item_Code as ICode,(TSPL_RGP_JOB_WORK_DETAIL.rgp_qty) as Qty,-1 as RI from TSPL_RGP_JOB_WORK_DETAIL left outer join TSPL_RGP_HEAD on TSPL_RGP_HEAD.rgp_no=TSPL_RGP_JOB_WORK_DETAIL.rgp_no where TSPL_RGP_JOB_WORK_DETAIL.PO_id='" + strPOCode + "' and TSPL_RGP_JOB_WORK_DETAIL.Item_Code='" + strICode + "' and  TSPL_RGP_JOB_WORK_DETAIL.Unit_code='" + strUOM + "' and TSPL_RGP_JOB_WORK_DETAIL.rgp_no not in ('" + strDocumentNo + "') and len(isnull(TSPL_RGP_JOB_WORK_DETAIL.Against_Schedule_Code,''))<=0   " &
               "   union all select TSPL_PO_SCH_DETAIL.Item_Code as ICode,TSPL_PO_SCH_DETAIL.schedule_qty as Qty,-1 as RI from TSPL_PO_SCH_DETAIL left outer join TSPL_PO_SCH_HEAD on TSPL_PO_SCH_HEAD.document_code=TSPL_PO_SCH_DETAIL.document_code where TSPL_PO_SCH_DETAIL.PO_code='" + strPOCode + "' and TSPL_PO_SCH_DETAIL.Item_Code='" + strICode + "' and  TSPL_PO_SCH_DETAIL.Unit_code='" + strUOM + "' and TSPL_PO_SCH_DETAIL.document_code not in ('" + strDocumentNo + "')   " &
               " )Final "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function

    Public Shared Function CompletePO(ByVal strReqCode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_PURCHASE_ORDER_DETAIL set Status ='1' where PurchaseOrder_No='" + strReqCode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class

<Serializable()>
Public Class clsPurchaseOrderHeadHist
#Region "Variables"
#End Region
    'Public Shared Function SaveCancleData(ByVal strCode As String, ByVal intAmbandentNo As Integer, ByVal trans As SqlTransaction) As Boolean
    '    Dim isSaved As Boolean = True
    '    Try
    '        Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_PURCHASE_ORDER_HEAD", trans)
    '        Dim qry As String = "INSERT INTO TSPL_PURCHASE_ORDER_HEAD_Cancel_Data (" + strInvColumns + ") SELECT " + strInvColumns + " FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + clsCommon.myCstr(strCode) + "'"
    '        clsDBFuncationality.ExecuteNonQuery(qry, trans)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return isSaved
    'End Function
    Public Shared Function SaveDataForHistory(ByVal strCode As String, ByVal intAmbandentNo As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            '' Anubhooti 22-Aug-2014 (TSPL_PURCHASE_ORDER_HEAD_Hist --> TSPL_PURCHASE_ORDER_HEAD_Hist_New)
            Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_PURCHASE_ORDER_HEAD", trans)
            Dim qry As String = "INSERT INTO TSPL_PURCHASE_ORDER_HEAD_Hist_New (" + strInvColumns + ",Abandonment_Date) SELECT " + strInvColumns.Replace("Abandonment_No", "" + clsCommon.myCstr(intAmbandentNo) + "") + ",'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' FROM TSPL_PURCHASE_ORDER_HEAD WHERE PurchaseOrder_No='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            '' Detail
            '' Anubhooti 22-Aug-2014 (TSPL_PURCHASE_ORDER_DETAIL_Hist --> TSPL_PURCHASE_ORDER_DETAIL_Hist_New)
            Dim strDetailColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_PURCHASE_ORDER_DETAIL", trans)
            Dim qry1 As String = "INSERT INTO TSPL_PURCHASE_ORDER_DETAIL_Hist_New(" + strDetailColumns + ",Abandonment_No,Abandonment_Date) SELECT " + strDetailColumns + "," + clsCommon.myCstr(intAmbandentNo) + ",'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' FROM TSPL_PURCHASE_ORDER_DETAIL WHERE PurchaseOrder_No='" + clsCommon.myCstr(strCode) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry1, trans)
            ''
            ''
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal intAmbandentNo As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim obj As clsPurchaseOrderHead = clsPurchaseOrderHead.GetData(strCode, NavigatorType.Current, "", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Date", clsCommon.GetPrintDate(obj.PurchaseOrder_Date, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Delivery_date", clsCommon.GetPrintDate(obj.Delivery_date, "dd/MM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Type", obj.PurchaseOrder_Type)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
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
            clsCommon.AddColumnsForChange(coll, "Header_Discount_Amount", obj.Header_Discount_Amount)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "PO_Total_Amt", obj.PO_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Comment1", obj.Comment1)
            clsCommon.AddColumnsForChange(coll, "Comment2", obj.Comment2)
            clsCommon.AddColumnsForChange(coll, "Comment3", obj.Comment3)
            clsCommon.AddColumnsForChange(coll, "Comment4", obj.Comment4)
            clsCommon.AddColumnsForChange(coll, "Comment5", obj.Comment5)
            clsCommon.AddColumnsForChange(coll, "Comment6", obj.Comment6)
            clsCommon.AddColumnsForChange(coll, "Comment7", obj.Comment7)
            clsCommon.AddColumnsForChange(coll, "Comment8", obj.Comment8)
            clsCommon.AddColumnsForChange(coll, "Comment9", obj.Comment9)
            clsCommon.AddColumnsForChange(coll, "Comment10", obj.Comment10)
            clsCommon.AddColumnsForChange(coll, "Comment11", obj.Comment11)
            clsCommon.AddColumnsForChange(coll, "Comment12", obj.Comment12)
            clsCommon.AddColumnsForChange(coll, "Comment13", obj.Comment13)
            'clsCommon.AddColumnsForChange(coll, "Comment14", obj.Comment14)
            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Against_Requisition", obj.Against_Requisition, True)


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



            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)
            clsCommon.AddColumnsForChange(coll, "Modify_By", obj.Modify_By)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", obj.Modify_Date)
            clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", obj.PurchaseOrder_No)
            clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
            clsCommon.AddColumnsForChange(coll, "Created_Date", obj.Created_Date)
            clsCommon.AddColumnsForChange(coll, "Abandonment_No", intAmbandentNo)
            clsCommon.AddColumnsForChange(coll, "Abandonment_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Abandonment_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Quotation_No", obj.Quotation_No)
            If obj.Quotation_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "Quotation_Date", clsCommon.GetPrintDate(obj.Quotation_Date, "dd/MMM/yyyy hh:mm tt"))
            End If


            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PURCHASE_ORDER_HEAD_Hist_New", OMInsertOrUpdate.Insert, "", trans)

            If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                For Each objTR As clsPurchaseOrderDetail In obj.Arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", objTR.PurchaseOrder_No)
                    clsCommon.AddColumnsForChange(coll, "Line_No", objTR.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Row_Type", objTR.Row_Type)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", objTR.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_Qty", objTR.PurchaseOrder_Qty)
                    clsCommon.AddColumnsForChange(coll, "Requisition_Id", objTR.Requisition_Id, True)
                    clsCommon.AddColumnsForChange(coll, "Balance_Qty", objTR.Balance_Qty)
                    clsCommon.AddColumnsForChange(coll, "Unit_code", objTR.Unit_code)
                    clsCommon.AddColumnsForChange(coll, "Location", objTR.Location)
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", objTR.Item_Cost)
                    clsCommon.AddColumnsForChange(coll, "Amount", objTR.Amount)

                    clsCommon.AddColumnsForChange(coll, "Header_Discount_Per", objTR.Header_Discount_Per)
                    clsCommon.AddColumnsForChange(coll, "Header_Discount_Amount", objTR.Header_Discount_Amount)
                    clsCommon.AddColumnsForChange(coll, "Disc_Per", objTR.Disc_Per)
                    clsCommon.AddColumnsForChange(coll, "Detail_Discount_Amount", objTR.Detail_Discount_Amount)
                    clsCommon.AddColumnsForChange(coll, "Disc_Amt", objTR.Disc_Amt)

                    clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", objTR.Amt_Less_Discount)
                    clsCommon.AddColumnsForChange(coll, "TAX1", objTR.TAX1)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", objTR.TAX1_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", objTR.TAX1_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", objTR.TAX1_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2", objTR.TAX2)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", objTR.TAX2_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", objTR.TAX2_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt", objTR.TAX2_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3", objTR.TAX3)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", objTR.TAX3_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", objTR.TAX3_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt", objTR.TAX3_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4", objTR.TAX4)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", objTR.TAX4_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", objTR.TAX4_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt", objTR.TAX4_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5", objTR.TAX5)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", objTR.TAX5_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", objTR.TAX5_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt", objTR.TAX5_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6", objTR.TAX6)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", objTR.TAX6_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Rate", objTR.TAX6_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Amt", objTR.TAX6_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7", objTR.TAX7)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", objTR.TAX7_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Rate", objTR.TAX7_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Amt", objTR.TAX7_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8", objTR.TAX8)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", objTR.TAX8_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Rate", objTR.TAX8_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Amt", objTR.TAX8_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9", objTR.TAX9)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", objTR.TAX9_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Rate", objTR.TAX9_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Amt", objTR.TAX9_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10", objTR.TAX10)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", objTR.TAX10_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Rate", objTR.TAX10_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Amt", objTR.TAX10_Amt)
                    clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", objTR.Total_Tax_Amt)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", objTR.Item_Net_Amt)
                    clsCommon.AddColumnsForChange(coll, "Specification", objTR.Specification)
                    clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                    clsCommon.AddColumnsForChange(coll, "Abandonment_No", intAmbandentNo)
                    clsCommon.AddColumnsForChange(coll, "Assessable", objTR.Assessable)
                    clsCommon.AddColumnsForChange(coll, "AssessableAmt", objTR.AssessableAmt)
                    '' for abatement PO
                    clsCommon.AddColumnsForChange(coll, "AbatementRate", objTR.AbatementRate)
                    clsCommon.AddColumnsForChange(coll, "AssessableMRP", objTR.AssessableMRP)
                    clsCommon.AddColumnsForChange(coll, "TotalAssessableMRP", objTR.TotalAssessableMRP)


                    ''==========================19/10/2016======================
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code1", objTR.ItemAdd_Charge_Code1)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt1", objTR.ItemAdd_Org_Charge_Amt1)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt1", objTR.ItemAdd_Calc_Charge_Amt1)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code9", objTR.ItemAdd_Charge_Code9)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt9", objTR.ItemAdd_Org_Charge_Amt9)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt9", objTR.ItemAdd_Calc_Charge_Amt9)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code8", objTR.ItemAdd_Charge_Code8)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt8", objTR.ItemAdd_Org_Charge_Amt8)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt8", objTR.ItemAdd_Calc_Charge_Amt8)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code7", objTR.ItemAdd_Charge_Code7)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt7", objTR.ItemAdd_Org_Charge_Amt7)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt7", objTR.ItemAdd_Calc_Charge_Amt7)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code6", objTR.ItemAdd_Charge_Code6)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt6", objTR.ItemAdd_Org_Charge_Amt6)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt6", objTR.ItemAdd_Calc_Charge_Amt6)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code5", objTR.ItemAdd_Charge_Code5)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt5", objTR.ItemAdd_Org_Charge_Amt5)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt5", objTR.ItemAdd_Calc_Charge_Amt5)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code4", objTR.ItemAdd_Charge_Code4)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt4", objTR.ItemAdd_Org_Charge_Amt4)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt4", objTR.ItemAdd_Calc_Charge_Amt4)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code3", objTR.ItemAdd_Charge_Code3)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt3", objTR.ItemAdd_Org_Charge_Amt3)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt3", objTR.ItemAdd_Calc_Charge_Amt3)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code2", objTR.ItemAdd_Charge_Code2)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt2", objTR.ItemAdd_Org_Charge_Amt2)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt2", objTR.ItemAdd_Calc_Charge_Amt2)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Charge_Code10", objTR.ItemAdd_Charge_Code10)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Org_Charge_Amt10", objTR.ItemAdd_Org_Charge_Amt10)
                    clsCommon.AddColumnsForChange(coll, "ItemAdd_Calc_Charge_Amt10", objTR.ItemAdd_Calc_Charge_Amt10)
                    clsCommon.AddColumnsForChange(coll, "Total_ItemAdd_Charge", objTR.Total_ItemAdd_Charge)
                    ''=================================================================

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PURCHASE_ORDER_DETAIL_Hist_New", OMInsertOrUpdate.Insert, "", trans)
                Next

            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

End Class

<Serializable()>
Public Class clsPurchaseOrderRoadDetail
    Public roadvendor As String = Nothing
    Public roadpono As String = Nothing
    Public roadcode As String = Nothing
    Public roadissue_no As String = Nothing
    Public RoadpermitSRNNO As String = Nothing
    Public FieldName As String = Nothing
    Public FieldDesc As String = Nothing
    Public Terms_C As String = Nothing

    Public Shared Function SaveData_RoadPermit(ByVal PurchaseOrderNo As String, ByVal arr As List(Of clsPurchaseOrderRoadDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL where purchaseorder_no='" + PurchaseOrderNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPurchaseOrderRoadDetail In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", PurchaseOrderNo)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", objtr.roadvendor)
                    clsCommon.AddColumnsForChange(coll, "Form_Code", objtr.roadcode)
                    clsCommon.AddColumnsForChange(coll, "Issue_No", objtr.roadissue_no)
                    clsCommon.AddColumnsForChange(coll, "srn_no", objtr.RoadpermitSRNNO)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ROADPERMIT_ISSUE_RECEIVE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveData_WorkOrder(ByVal PurchaseOrderNo As String, ByVal arr As List(Of clsPurchaseOrderRoadDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from tspl_Purchase_Order_work_order where purchaseorder_no='" + PurchaseOrderNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPurchaseOrderRoadDetail In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", PurchaseOrderNo)
                    clsCommon.AddColumnsForChange(coll, "Field_Name", objtr.FieldName)
                    clsCommon.AddColumnsForChange(coll, "Description", objtr.FieldDesc)

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_Purchase_Order_work_order", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function SaveData_WorkOrder_Terms(ByVal PurchaseOrderNo As String, ByVal arr As List(Of clsPurchaseOrderRoadDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_PURCHASE_ORDER_WORK_ORDER_Terms where purchaseorder_no='" + PurchaseOrderNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPurchaseOrderRoadDetail In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", PurchaseOrderNo)
                    clsCommon.AddColumnsForChange(coll, "Terms_Condition", objtr.Terms_C)


                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PURCHASE_ORDER_WORK_ORDER_Terms", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

<Serializable()>
Public Class clsPurchaseOrderCFORMDetail
    Public cformvendor As String = Nothing
    Public cformpono As String = Nothing
    Public cformcode As String = Nothing
    Public cformissue_no As String = Nothing
    Public cformSRNNO As String = Nothing


    Public Shared Function SaveData_CFORM(ByVal PurchaseOrderNo As String, ByVal arr As List(Of clsPurchaseOrderCFORMDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_CFORM_ISSUE_RECEIVE_DETAIL where purchaseorder_no='" + PurchaseOrderNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim isSaved As Boolean = True
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPurchaseOrderCFORMDetail In arr
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "PurchaseOrder_No", PurchaseOrderNo)
                    clsCommon.AddColumnsForChange(coll, "Vendor_Code", objtr.cformvendor)
                    clsCommon.AddColumnsForChange(coll, "Form_Code", objtr.cformcode)
                    clsCommon.AddColumnsForChange(coll, "Issue_No", objtr.cformissue_no)
                    clsCommon.AddColumnsForChange(coll, "srn_no", objtr.cformSRNNO)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CFORM_ISSUE_RECEIVE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

<Serializable()>
Public Class clsPurchaseOrderAdditionChargeInsurance
#Region "Variables"
    Public TR_Code As String = Nothing
    Public PO_No As String = Nothing
    Public AC_Code As String = Nothing
    Public AC_Name As String = Nothing ''Not a table Field
    Public Amount As Decimal
#End Region
    Public Shared Function SaveData(ByVal DocNo As String, ByVal DocDate As DateTime, ByVal arr As List(Of clsPurchaseOrderAdditionChargeInsurance), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPurchaseOrderAdditionChargeInsurance In arr
                    coll = New Hashtable()
                    objtr.TR_Code = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_Code", objtr.TR_Code)
                    clsCommon.AddColumnsForChange(coll, "PO_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "AC_Code", objtr.AC_Code)
                    clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PURCHASE_ORDER_ADDITION_CHARGE_INSURANCE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal DocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "delete from TSPL_PURCHASE_ORDER_ADDITION_CHARGE_INSURANCE where PO_No='" + DocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function
    Public Shared Function GetData(ByVal DocNo As String, ByVal trans As SqlTransaction) As List(Of clsPurchaseOrderAdditionChargeInsurance)
        Dim qry As String = "select TSPL_PURCHASE_ORDER_ADDITION_CHARGE_INSURANCE.*,TSPL_Additional_Charges.Description as AC_Name  from TSPL_PURCHASE_ORDER_ADDITION_CHARGE_INSURANCE left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code=TSPL_PURCHASE_ORDER_ADDITION_CHARGE_INSURANCE.AC_Code where TSPL_PURCHASE_ORDER_ADDITION_CHARGE_INSURANCE.PO_No='" + DocNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim Arr_ACInsurance As List(Of clsPurchaseOrderAdditionChargeInsurance) = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Arr_ACInsurance = New List(Of clsPurchaseOrderAdditionChargeInsurance)
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsPurchaseOrderAdditionChargeInsurance()
                objtr.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                objtr.PO_No = clsCommon.myCstr(dr("PO_No"))
                objtr.AC_Code = clsCommon.myCstr(dr("AC_Code"))
                objtr.AC_Name = clsCommon.myCstr(dr("AC_Name"))
                objtr.Amount = clsCommon.myCstr(dr("Amount"))
                Arr_ACInsurance.Add(objtr)
            Next
        End If
        Return Arr_ACInsurance
    End Function
End Class


Public Class clsTenderSchedulePO
#Region "Variables"
    Public DocumentCode As String
    Public SNo As Integer
    Public PSNo As Integer
    Public Schedule_No As Integer
    Public From_Date As Date
    Public To_Date As Date
    Public Item_Code As String
    Public Schedule_Qty_Per As Decimal
    Public Schedule_Qty As Decimal
    Public Schedule_Short_Per As Decimal
    Public Schedule_Short As Decimal
    Public Late_Days As Integer
    Public Extension_Days As Integer
    Public Item_Name As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Location_Name As String = Nothing
    Public Arr As List(Of clsTenderSchedulePeneltyPO) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTenderSchedulePO), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTenderSchedulePO In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "PSNo", obj.PSNo)
                clsCommon.AddColumnsForChange(coll, "Schedule_No", obj.Schedule_No)
                clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Schedule_Qty_Per", obj.Schedule_Qty_Per)
                clsCommon.AddColumnsForChange(coll, "Schedule_Qty", obj.Schedule_Qty)
                clsCommon.AddColumnsForChange(coll, "Schedule_Short_Per", obj.Schedule_Short_Per)
                clsCommon.AddColumnsForChange(coll, "Schedule_Short", obj.Schedule_Short)
                clsCommon.AddColumnsForChange(coll, "Late_Days", obj.Late_Days)
                clsCommon.AddColumnsForChange(coll, "Extension_Days", obj.Extension_Days)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SCHEDULE_PO", OMInsertOrUpdate.Insert, "", trans)

                Dim PK As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select MAX(PK_ID) from TSPL_TENDER_SCHEDULE_PO where DocumentCode='" + strDocNo + "'", trans))
                clsTenderSchedulePeneltyPO.SaveData(strDocNo, PK, obj.Arr, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsTenderSchedulePO)
        Dim arr As List(Of clsTenderSchedulePO) = Nothing
        Dim qry As String = "select TSPL_TENDER_SCHEDULE_PO.* from TSPL_TENDER_SCHEDULE_PO  where TSPL_TENDER_SCHEDULE_PO.DocumentCode='" + clsCommon.myCstr(strDocNo) + "' order by TSPL_TENDER_SCHEDULE_PO.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsTenderSchedulePO)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsTenderSchedulePO
                obj.SNo = ii + 1
                obj.DocumentCode = clsCommon.myCstr(dt.Rows(ii)("DocumentCode"))
                obj.PSNo = clsCommon.myCDecimal(dt.Rows(ii)("PSNo"))
                obj.Schedule_No = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_No"))
                obj.From_Date = clsCommon.myCDate(dt.Rows(ii)("From_Date"))
                obj.To_Date = clsCommon.myCDate(dt.Rows(ii)("To_Date"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(ii)("Item_Code"))
                obj.Schedule_Qty_Per = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Qty_Per"))
                obj.Schedule_Qty = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Qty"))
                obj.Schedule_Short_Per = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Short_Per"))
                obj.Schedule_Short = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Short"))
                obj.Late_Days = clsCommon.myCDecimal(dt.Rows(ii)("Late_Days"))
                obj.Extension_Days = clsCommon.myCDecimal(dt.Rows(ii)("Extension_Days"))
                obj.Arr = clsTenderSchedulePeneltyPO.GetData(clsCommon.myCDecimal(dt.Rows(ii)("PK_Id")), False, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsTenderSchedulePeneltyPO
#Region "Variables"
    Public PK_Id As Integer
    Public DocumentCode As String
    Public Against_Tender_Schedule_PK_Id As Integer
    Public Penalty_Date As Date
    Public Penalty As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal AgainstSchedulePKId As Integer, ByVal Arr As List(Of clsTenderSchedulePeneltyPO), ByVal trans As SqlTransaction) As Boolean
        For Each obj As clsTenderSchedulePeneltyPO In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
            clsCommon.AddColumnsForChange(coll, "Against_Tender_Schedule_PK_Id", AgainstSchedulePKId)
            clsCommon.AddColumnsForChange(coll, "Penalty_Date", clsCommon.GetPrintDate(obj.Penalty_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Penalty", obj.Penalty)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SCHEDULE_PENALTY_PO", OMInsertOrUpdate.Insert, "", trans)
        Next
        Return True
    End Function

    Public Shared Function GetData(ByVal AgainstSchedulePKId As Integer, ByVal AddExtensionDays As Boolean, ByVal trans As SqlTransaction) As List(Of clsTenderSchedulePeneltyPO)
        Dim arr As List(Of clsTenderSchedulePeneltyPO) = Nothing
        Dim qry As String = "select TSPL_TENDER_SCHEDULE_PENALTY_PO.DocumentCode,TSPL_TENDER_SCHEDULE_PENALTY_PO.PK_Id,TSPL_TENDER_SCHEDULE_PENALTY_PO.Against_Tender_Schedule_PK_Id "
        If AddExtensionDays = True Then
            qry += " ,DATEADD(day,isnull(TSPL_TENDER_SCHEDULE_PO.Extension_Days,0),TSPL_TENDER_SCHEDULE_PENALTY_PO.Penalty_Date) "
        Else
            qry += " ,TSPL_TENDER_SCHEDULE_PENALTY_PO.Penalty_Date "
        End If
        qry += " AS Penalty_Date ,TSPL_TENDER_SCHEDULE_PENALTY_PO.Penalty
         from TSPL_TENDER_SCHEDULE_PENALTY_PO
         left outer join TSPL_TENDER_SCHEDULE_PO on TSPL_TENDER_SCHEDULE_PO.DocumentCode=TSPL_TENDER_SCHEDULE_PENALTY_PO.DocumentCode
         and TSPL_TENDER_SCHEDULE_PO.PK_ID=TSPL_TENDER_SCHEDULE_PENALTY_PO.Against_Tender_Schedule_PK_Id
         where TSPL_TENDER_SCHEDULE_PENALTY_PO.Against_Tender_Schedule_PK_Id='" + clsCommon.myCstr(AgainstSchedulePKId) + "' order by TSPL_TENDER_SCHEDULE_PENALTY_PO.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsTenderSchedulePeneltyPO)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsTenderSchedulePeneltyPO
                obj.PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("PK_Id"))
                obj.DocumentCode = clsCommon.myCstr(dt.Rows(ii)("DocumentCode"))
                obj.Against_Tender_Schedule_PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("Against_Tender_Schedule_PK_Id"))
                obj.Penalty_Date = clsCommon.myCDate(dt.Rows(ii)("Penalty_Date"))
                obj.Penalty = clsCommon.myCDecimal(dt.Rows(ii)("Penalty"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class