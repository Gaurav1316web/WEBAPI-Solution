Imports common
Imports System.Data.SqlClient
Public Class clsPurchasReturnHead
#Region "Variables"
    Public Sub_Location_code As String = String.Empty
    Public ShowItemAllStructureWise As Boolean = False
    Public Project_Id As String = Nothing
    Public PR_No As String = Nothing
    Public Vendor_Invoice_No As String = Nothing
    Public PR_Date As DateTime
    Public Vendor_Code As String = Nothing
    Public GSTRegistered As Integer = 0
    Public Vendor_Name As String = Nothing
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
    Public Total_Taxable_Amount As Double = 0
    Public PR_Total_Amt As Double = 0
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Due_Date As String = Nothing
    Public Posting_Date As String = Nothing
    Public Carrier As String = Nothing
    Public VehicleNo As String = Nothing
    Public GRNo As String = Nothing
    Public GENo As String = Nothing
    Public GEDate As Date? = Nothing
    Public Item_Type As String = Nothing

    Public Against_Requisition As String = Nothing
    Public Against_PO As String = Nothing
    Public Against_GRN As String = Nothing
    Public Against_MRN As String = Nothing
    Public Against_SRN As String = Nothing
    Public Against_PI As String = Nothing
    Public Auto_Gen_Againnt_PI_No As String = Nothing
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
    Public Tot_Empty_Amount As Double = 0



    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public is_Excise_On_Qty As Boolean = False
    Public AssessableAmt As Decimal = 0
    Public Arr As List(Of clsPurchasReturnDetail) = Nothing
    Public objPIRemittance As clsPIRemittance = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public TrType As String = Nothing
    Public NoteType As String = Nothing
    Public is_Reject_Item As Boolean = False
    Public isJobWorkOutward As Boolean = False
    Public RoundOffAmt As Decimal

    Public Total_Item_Insurance_Amt As Decimal = 0
    Public Total_Add_Charge_Insurance As Decimal = 0
    Public Arr_ACInsurance As List(Of clsPRAdditionChargeInsurance) = Nothing
#End Region

    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PR_HEAD", "PR_No", "TSPL_PR_DETAIL", "PR_No", "TSPL_PI_REMITTANCE", "Document_No", trans)
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsPurchasReturnHead, ByVal isNewEntry As Boolean) As Boolean
        ShowItemAllStructureWise = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowItemAllStructureWise, clsFixedParameterCode.ShowItemAllStructureWise, Nothing)) = "1", True, False))
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

    Public Function SaveData(ByVal obj As clsPurchasReturnHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Return", obj.Bill_To_Location, obj.PR_Date, trans)
            If Not isNewEntry Then
                HistoryUpdate(obj.PR_No, trans)
            End If
            clsPRAdditionChargeInsurance.DeleteData(obj.PR_No, trans)
            clsSerializeInvenotry.DeleteData("Purchase Return", obj.PR_No, trans)
            Dim qry As String = "delete from TSPL_PR_DETAIL where PR_No='" + obj.PR_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_PI_REMITTANCE where Document_No='" + obj.PR_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                If ShowItemAllStructureWise Then
                    obj.PR_No = clsERPFuncationality.GetNextCode(trans, obj.PR_Date, clsDocType.PurchaseReturn, clsDocTransactionType.All, obj.Bill_To_Location)
                Else
                    If clsCommon.CompairString(obj.Item_Type, "R") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Item_Type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Item_Type, "O") = CompairStringResult.Equal Then
                        obj.PR_No = clsERPFuncationality.GetNextCode(trans, obj.PR_Date, clsDocType.PurchaseReturn, clsDocTransactionType.POOther, obj.Bill_To_Location)
                    Else
                        Dim TransType = clsDBFuncationality.getSingleValue("SELECT PREFIX_CODE FROM TSPL_ITEM_TYPE_MASTER WHERE ITEM_TYPE_CODE='" + obj.Item_Type + "'", trans)
                        obj.PR_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.PR_Date), clsDocType.PurchaseReturn, TransType, obj.Bill_To_Location)
                    End If

                End If

                If clsCommon.CompairString(obj.PR_No, String.Empty) = CompairStringResult.Equal Then
                    Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                End If
            End If
                If (clsCommon.myLen(obj.PR_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PR_Date", clsCommon.GetPrintDate(obj.PR_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Vendor_Invoice_No", obj.Vendor_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Bill_To_Location", obj.Bill_To_Location)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Location", obj.Ship_To_Location)
            clsCommon.AddColumnsForChange(coll, "GSTRegistered", obj.GSTRegistered)
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
            clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Discount_Base", obj.Discount_Base)
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Amount_Less_Discount", obj.Amount_Less_Discount)
            clsCommon.AddColumnsForChange(coll, "Total_Taxable_Amount", obj.Total_Taxable_Amount)
            clsCommon.AddColumnsForChange(coll, "PR_Total_Amt", obj.PR_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "RoundOffAmt", obj.RoundOffAmt)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)

            clsCommon.AddColumnsForChange(coll, "Against_Requisition", obj.Against_Requisition, True)
            clsCommon.AddColumnsForChange(coll, "Against_PO", obj.Against_PO, True)
            clsCommon.AddColumnsForChange(coll, "Against_GRN", obj.Against_GRN, True)
            clsCommon.AddColumnsForChange(coll, "Against_MRN", obj.Against_MRN, True)
            clsCommon.AddColumnsForChange(coll, "Against_SRN", obj.Against_SRN, True)
            clsCommon.AddColumnsForChange(coll, "Against_PI", obj.Against_PI, True)
            clsCommon.AddColumnsForChange(coll, "Project_Id", obj.Project_Id, True)
            clsCommon.AddColumnsForChange(coll, "Sub_Location_code", obj.Sub_Location_code, True)

            clsCommon.AddColumnsForChange(coll, "Auto_Gen_Againnt_PI_No", obj.Auto_Gen_Againnt_PI_No, True)

            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If

            clsCommon.AddColumnsForChange(coll, "is_Excise_On_Qty", IIf(obj.is_Excise_On_Qty, 1, 0))
            clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)

            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Carrier", obj.Carrier)
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
            clsCommon.AddColumnsForChange(coll, "GENo", obj.GENo)

            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            If obj.GEDate.HasValue Then
                clsCommon.AddColumnsForChange(coll, "GEDate", clsCommon.GetPrintDate(obj.GEDate, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "GEDate", Nothing, True)
            End If
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
            clsCommon.AddColumnsForChange(coll, "Tot_Empty_Amount", obj.Tot_Empty_Amount)
            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            If obj.ApplicableFrom IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(obj.ApplicableFrom, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", Nothing, True)
            End If
            '' End currencyconversion
            clsCommon.AddColumnsForChange(coll, "Transaction_type", obj.TrType)
            clsCommon.AddColumnsForChange(coll, "Note_type", obj.NoteType)
            clsCommon.AddColumnsForChange(coll, "is_Reject_Item", IIf(obj.is_Reject_Item, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Total_Add_Charge_Insurance", obj.Total_Add_Charge_Insurance)
            clsCommon.AddColumnsForChange(coll, "Total_Item_Insurance_Amt", obj.Total_Item_Insurance_Amt)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "PR_No", obj.PR_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PR_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PR_HEAD", OMInsertOrUpdate.Update, "TSPL_PR_HEAD.PR_No='" + obj.PR_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsPurchasReturnDetail.SaveData(obj.PR_No, obj.PR_Date, Arr, trans, obj.TrType)
            isSaved = isSaved AndAlso clsPIRemittance.SaveData(obj.objPIRemittance, obj.PR_No, obj.PR_Date, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.PR_No, obj.arrCustomFields, trans)
            isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "PR_No", obj.PR_No, "TSPL_PR_HEAD", trans)
            isSaved = isSaved AndAlso clsPRAdditionChargeInsurance.SaveData(obj.PR_No, obj.PR_Date, obj.Arr_ACInsurance, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsPurchasReturnHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsPurchasReturnHead
        Dim obj As clsPurchasReturnHead = Nothing
        Dim qry As String = "SELECT TSPL_PR_HEAD.*,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName FROM TSPL_PR_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PR_HEAD.Bill_To_Location left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_PR_HEAD.Ship_To_Location left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_PR_HEAD.Tax_Group left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_PR_HEAD.Terms_Code where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PR_HEAD.PR_No = (select MIN(PR_No) from TSPL_PR_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_PR_HEAD.PR_No = (select Max(PR_No) from TSPL_PR_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_PR_HEAD.PR_No = (select Min(PR_No) from TSPL_PR_HEAD where PR_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_PR_HEAD.PR_No = (select Max(PR_No) from TSPL_PR_HEAD where PR_No<'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_PR_HEAD.PR_No = '" + strPONo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsPurchasReturnHead()
            obj.PR_No = clsCommon.myCstr(dt.Rows(0)("PR_No"))
            obj.PR_Date = clsCommon.myCDate(dt.Rows(0)("PR_Date"))
            obj.Vendor_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Vendor_Invoice_No"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.GSTRegistered = clsCommon.myCdbl(dt.Rows(0)("GSTRegistered"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Bill_To_Location = clsCommon.myCstr(dt.Rows(0)("Bill_To_Location"))
            obj.Ship_To_Location = clsCommon.myCstr(dt.Rows(0)("Ship_To_Location"))
            obj.Sub_Location_code = clsCommon.myCstr(dt.Rows(0)("Sub_Location_code"))
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
            obj.Total_Taxable_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Taxable_Amount"))
            obj.RoundOffAmt = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmt"))
            obj.PR_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("PR_Total_Amt"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))
            obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))

            obj.Carrier = clsCommon.myCstr(dt.Rows(0)("Carrier"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))
            obj.GENo = clsCommon.myCstr(dt.Rows(0)("GENo"))
            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)

            obj.Against_Requisition = clsCommon.myCstr(dt.Rows(0)("Against_Requisition"))
            obj.Against_PO = clsCommon.myCstr(dt.Rows(0)("Against_PO"))
            obj.Against_GRN = clsCommon.myCstr(dt.Rows(0)("Against_GRN"))
            obj.Against_MRN = clsCommon.myCstr(dt.Rows(0)("Against_MRN"))
            obj.Against_SRN = clsCommon.myCstr(dt.Rows(0)("Against_SRN"))
            obj.Against_PI = clsCommon.myCstr(dt.Rows(0)("Against_PI"))
            obj.Auto_Gen_Againnt_PI_No = clsCommon.myCstr(dt.Rows(0)("Auto_Gen_Againnt_PI_No"))
            If dt.Rows(0)("GEDate") IsNot DBNull.Value Then
                obj.GEDate = clsCommon.myCDate(dt.Rows(0)("GEDate"))
            End If
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))

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
            obj.Tot_Empty_Amount = clsCommon.myCdbl(dt.Rows(0)("Tot_Empty_Amount"))
            obj.objPIRemittance = clsPIRemittance.GetData(obj.PR_No, trans)
            obj.is_Excise_On_Qty = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Excise_On_Qty")) = 1, True, False)
            obj.isJobWorkOutward = IIf(clsCommon.myCdbl(dt.Rows(0)("isJobWorkOutward")) = 1, True, False)
            obj.AssessableAmt = clsCommon.myCdbl(dt.Rows(0)("AssessableAmt"))
            obj.Project_Id = clsCommon.myCstr(dt.Rows(0)("Project_Id"))
            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            obj.TrType = clsCommon.myCstr(dt.Rows(0)("Transaction_type"))
            obj.NoteType = clsCommon.myCstr(dt.Rows(0)("Note_type"))
            '' END CURRENCYCONVERSION
            obj.is_Reject_Item = IIf(clsCommon.myCdbl(dt.Rows(0)("is_Reject_Item")) = 1, True, False)
            obj.Total_Add_Charge_Insurance = clsCommon.myCdbl(dt.Rows(0)("Total_Add_Charge_Insurance"))
            obj.Total_Item_Insurance_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Item_Insurance_Amt"))

            qry = "SELECT TSPL_PR_DETAIL.*,TSPL_LOCATION_MASTER.Location_Desc as LocationName,(case when len(isnull(TSPL_PR_DETAIL.PI_Id,''))>0 then (select MAX(PI_Qty) from TSPL_PI_DETAIL where TSPL_PI_DETAIL.PI_No=TSPL_PR_DETAIL.PI_Id and TSPL_PI_DETAIL.Item_Code=TSPL_PR_DETAIL.Item_Code)  else 0 end) as OrgPIQty  FROM TSPL_PR_DETAIL left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_PR_DETAIL.Location where TSPL_PR_DETAIL.PR_No='" + obj.PR_No + "' ORDER BY TSPL_PR_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsPurchasReturnDetail)
                Dim objTr As clsPurchasReturnDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsPurchasReturnDetail
                    objTr.PR_No = clsCommon.myCstr(dr("PR_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.PR_Qty = clsCommon.myCdbl(dr("PR_Qty"))
                    objTr.PI_Id = clsCommon.myCstr(dr("PI_Id"))

                    objTr.PO_ID = clsCommon.myCstr(dr("PO_ID"))
                    objTr.GRN_ID = clsCommon.myCstr(dr("GRN_ID"))
                    objTr.MRN_ID = clsCommon.myCstr(dr("MRN_ID"))
                    objTr.SRN_Id = clsCommon.myCstr(dr("SRN_Id"))

                    objTr.OrgPIQty = clsCommon.myCdbl(dr("OrgPIQty"))
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
                    objTr.Taxable_Amount_Per = clsCommon.myCdbl(dr("Taxable_Amount_Per"))
                    objTr.Taxable_Amount = clsCommon.myCdbl(dr("Taxable_Amount"))

                    objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))
                    If dr("MFG_Date") IsNot DBNull.Value Then
                        objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    If dr("Expiry_Date") IsNot DBNull.Value Then
                        objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                    End If
                    objTr.Assessable = clsCommon.myCdbl(dr("Assessable"))
                    objTr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))
                    objTr.Empty_Amount = clsCommon.myCdbl(dr("Empty_Amount"))
                    objTr.Landed_Cost_Rate = clsCommon.myCdbl(dr("Landed_Cost_Rate"))
                    objTr.Landed_Cost_Amount = clsCommon.myCdbl(dr("Landed_Cost_Amount"))

                    objTr.Total_AddtionalCost_PerUnit = clsCommon.myCdbl(dr("Total_AddtionalCost_PerUnit"))
                    objTr.Total_NonRecTax_PerUnit = clsCommon.myCdbl(dr("Total_NonRecTax_PerUnit"))
                    objTr.Total_RecTax_PerUnit = clsCommon.myCdbl(dr("Total_RecTax_PerUnit"))

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
                    objTr.Category = clsCommon.myCstr(dr("Category"))
                    objTr.Emergency = IIf(clsCommon.myCdbl(dr("Emergency")) = 1, True, False)
                    objTr.Capex_Code = clsCommon.myCstr(dr("Capex_Code"))
                    objTr.Capex_SubCode = clsCommon.myCstr(dr("Capex_SubCode"))

                    objTr.Against_Item_Wise_Tax_Rate = clsCommon.myCstr(dr("Against_Item_Wise_Tax_Rate"))

                    objTr.Insurance_Base_Amt = clsCommon.myCdbl(dr("Insurance_Base_Amt"))
                    objTr.Insurance_Per = clsCommon.myCdbl(dr("Insurance_Per"))

                    objTr.Item_Insurance_Base_Amt = clsCommon.myCdbl(dr("Item_Insurance_Base_Amt"))
                    objTr.Item_Insurance_Apply_On = clsCommon.myCstr(dr("Item_Insurance_Apply_On"))
                    objTr.Item_Insurance_Rate = clsCommon.myCdbl(dr("Item_Insurance_Rate"))
                    objTr.Item_Insurance_Amt = clsCommon.myCdbl(dr("Item_Insurance_Amt"))
                    objTr.Item_Amt_After_Insurance = clsCommon.myCdbl(dr("Item_Amt_After_Insurance"))

                    objTr.arrSrItem = clsSerializeInvenotry.GetData("Purchase Return", objTr.PR_No, objTr.Item_Code, objTr.Line_No, trans)
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans, Nothing, Nothing)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional strVoucherNoRecreateOnly As String = Nothing, Optional ByVal strAPVoucherNoRecreateOnly As String = Nothing, Optional ByVal strAPInvNoRecreatedOnly As String = Nothing, Optional isRecreateThroughUtility As Boolean = False) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Purchase Return No not found to Post")
            End If
            Dim obj As clsPurchasReturnHead = clsPurchasReturnHead.GetData(strDocNo, NavigatorType.Current, trans)
            '=======Added by preeti gupta 14/12/2018=============
            Dim isApplyPurchaseAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0, False, True)
            '=========================================
            If (obj Is Nothing OrElse clsCommon.myLen(obj.PR_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Return", obj.Bill_To_Location, obj.PR_Date, trans)
            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Purchase Return No " + obj.PR_No + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = ""
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_PR_HEAD", "PR_No", obj.PR_No, trans)
            If isResult = False Then
                Return False
            End If

            Dim isAllQtyZero As Boolean = False
            qry = "select 1 from TSPL_PR_DETAIL where PR_No='" + obj.PR_No + "' and PR_Qty>0  and Row_Type='Item' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                isAllQtyZero = False
            Else
                isAllQtyZero = True
            End If
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)

            For Each objTr As clsPurchasReturnDetail In obj.Arr
                If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                    If clsCommon.myLen(objTr.PI_Id) > 0 Then
                        Dim qry1 As String = "update TSPL_PI_DETAIL set Balance_Qty=Balance_Qty - " + clsCommon.myCstr(objTr.PR_Qty) + " where PI_No='" + objTr.PI_Id + "' and Item_Code='" + objTr.Item_Code + "' and Unit_code='" + objTr.Unit_code + "' and isnull(MRP,0)='" + clsCommon.myCstr(objTr.MRP) + "' and isnull(Assessable,0)='" + clsCommon.myCstr(objTr.Assessable) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                    End If
                    Dim strItemTypeToSave As String = ""
                    If clsCommon.CompairString(obj.Item_Type, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(obj.Item_Type, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Item_Type, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(obj.Item_Type, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    End If

                    Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                    If ConvFac = 0 Then
                        Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
                    End If

                    Dim objInventoryMovemnt As New clsInventoryMovement()

                    'Sanjay
                    objInventoryMovemnt.CalculateAvgCost = False
                    objInventoryMovemnt.FIFO_Cost = 0
                    objInventoryMovemnt.LIFO_Cost = 0
                    objInventoryMovemnt.Avg_Cost = objTr.Landed_Cost_Amount
                    'Sanjay

                    objInventoryMovemnt.InOut = IIf(obj.NoteType = "C", "I", "O")

                    If obj.is_Reject_Item Then
                        objInventoryMovemnt.Location_Code = clsLocation.GetRejectedLocation(objTr.Location, trans)
                    Else
                        If clsCommon.myLen(obj.Ship_To_Location) > 0 Then
                            objInventoryMovemnt.Location_Code = obj.Ship_To_Location
                        Else
                            objInventoryMovemnt.Location_Code = objTr.Location
                        End If
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0 And obj.isJobWorkOutward = False Then
                        objInventoryMovemnt.Location_Code = obj.Sub_Location_code
                    End If

                    objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                    objInventoryMovemnt.Vendor_Name = obj.Vendor_Name
                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                    objInventoryMovemnt.Qty = objTr.PR_Qty
                    objInventoryMovemnt.UOM = objTr.Unit_code
                    objInventoryMovemnt.Basic_Cost = objTr.Item_Cost * IIf(obj.ConvRate = 0, 1, obj.ConvRate)
                    objInventoryMovemnt.MRP = objTr.MRP
                    objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt * IIf(obj.ConvRate = 0, 1, obj.ConvRate)
                    objInventoryMovemnt.Net_Cost = objTr.Item_Net_Amt * IIf(obj.ConvRate = 0, 1, obj.ConvRate)
                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                End If
            Next
            If obj.TrType <> "P" Then
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("Purchase Return", obj.PR_No, obj.PR_Date, obj.PR_Date, ArrInventoryMovement, trans)
            End If
            If isApplyPurchaseAccounting = True And (clsCommon.CompairString(obj.TrType, "P") = CompairStringResult.Equal) Then
                Dim counterReject As Integer = 1
                Dim objAdjRej As New ClsAdjustments()
                objAdjRej.Arr = New List(Of ClsAdjustmentsDetails)

                objAdjRej.Adjustment_No = ""
                objAdjRej.Adjustment_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
                objAdjRej.Description = "Price Adjustment Against PR -" & clsCommon.myCstr(obj.PR_No) & ""
                objAdjRej.Unit_Code = "ALL"
                objAdjRej.Loc_Code = clsLocation.GetRejectedLocation(obj.Bill_To_Location, trans)
                objAdjRej.Loc_Desc = clsLocation.GetName(objAdjRej.Loc_Code, trans)
                objAdjRej.Trans_Type = IIf(obj.NoteType = "D", clsCommon.myCstr("Out"), clsCommon.myCstr("In"))
                objAdjRej.chklocation = "N"
                objAdjRej.IsMilkType = 0
                objAdjRej.MainLocationCode = ""
                objAdjRej.MainLocationDesc = ""
                objAdjRej.Against_PurchaseReturn_No = obj.PR_No
                For Each objTr As clsPurchasReturnDetail In obj.Arr
                    If clsCommon.myLen(objTr.Item_Code) > 0 AndAlso clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                        Dim objAdTr As New ClsAdjustmentsDetails()
                        objAdTr.Adjustment_Line_No = counterReject
                        counterReject += 1
                        objAdTr.Item_Code = clsCommon.myCstr(objTr.Item_Code)
                        If clsCommon.myLen(objAdTr.Item_Code) > 0 Then
                            objAdTr.Item_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Item_Desc,'') AS Item_Desc From TSPL_ITEM_MASTER Where Item_Code ='" & clsCommon.myCstr(objAdTr.Item_Code) & "'", trans))
                        Else
                            objAdTr.Item_Description = ""
                        End If
                        objAdTr.Adjustment_Type = clsCommon.myCstr("Cost").Substring(0, 1) + IIf(clsCommon.CompairString(objAdjRej.Trans_Type, "In") = CompairStringResult.Equal, "I", "D")
                        objAdTr.Item_Quantity = 0
                        objAdTr.Item_Cost = objTr.Amount
                        objAdTr.Unit_Code = clsCommon.myCstr(objTr.Unit_code)
                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PURCHASE_ACCOUNTS.Adjustment_Account ,TSPL_GL_ACCOUNTS.Description  from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Adjustment_Account where TSPL_ITEM_MASTER.Item_Code='" + objTr.Item_Code + "'", trans)
                        If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                            Throw New Exception("Plese set the Purchase Account set or its Adjustment Writeoff Account for item " + objAdTr.Item_Code)
                        End If
                        objAdTr.Account_Code = clsCommon.myCstr(dt1.Rows(0)("Adjustment_Account"))
                        objAdTr.Account_Description = clsCommon.myCstr(dt1.Rows(0)("Description"))
                        objAdTr.Remarks = ""
                        objAdTr.Comments = ""
                        objAdTr.mrp = clsCommon.myCdbl(0)
                        objAdTr.Batch_No = ""
                        objAdTr.Breakage = clsCommon.myCdbl(0)
                        objAdTr.Breakage_Cost = clsCommon.myCdbl(0)
                        objAdTr.ItemType = ""
                        objAdTr.BreakageType = ""
                        objAdTr.LeakageQty = clsCommon.myCdbl(0)
                        objAdTr.Basic_Price = clsCommon.myCdbl(0)
                        objAdTr.fat_pers = clsCommon.myCdbl(0)
                        objAdTr.fat_kg = clsCommon.myCdbl(0)
                        objAdTr.snf_kg = clsCommon.myCdbl(0)
                        objAdTr.snf_pers = clsCommon.myCdbl(0)
                        objAdTr.ItemType = clsItemMaster.GetStoreAdjustmentItemTypeWithTrans(objTr.Item_Code, trans)
                        objAdTr.Itemstatus = "NEW"
                        If (clsCommon.myLen(objAdTr.Item_Code) > 0) Then
                            objAdjRej.Arr.Add(objAdTr)
                        End If
                    End If
                Next


                Dim isSavedAdj As Boolean = objAdjRej.SaveData(objAdjRej, True, "", trans)
                ClsAdjustments.PostData(objAdjRej.Adjustment_No, "Store Adjustment", trans, False)
            End If

            Dim AP_NOT_Exist As Boolean = True

            Dim isCreateAPDebitNote As Boolean = True
            If obj.is_Reject_Item Then
                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateDbitNoteForRejectPI, clsFixedParameterCode.CreateDbitNoteForRejectPI, trans)) = 1 Then
                    isCreateAPDebitNote = False
                End If
            End If

            If isCreateAPDebitNote Then

                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurReturnEvenIfPaymentDone, clsFixedParameterCode.AllowPurReturnEvenIfPaymentDone, trans)) = 0 Then
                    ''richa 02/08/2017 to check balance of invoice to if paymnet is done te only remaining amount of invoice will be return
                    Dim dblPendingBalance As Double = GetPIBalance(obj.Vendor_Code, obj.PR_No, obj.Against_PI, obj.PR_Date, trans)
                    If isRecreateThroughUtility = False Then
                        If dblPendingBalance = 0 Then
                            Throw New Exception("You can't return this PI becuase whole Payment has been done.")
                        End If
                        If obj.PR_Total_Amt > dblPendingBalance Then
                            Throw New Exception("Document Amount should not be greater than " & dblPendingBalance & ".")
                        End If
                    End If
                    ''---------------------
                End If




                Dim objVendorInvHead As New clsVedorInvoiceHead()
                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                If strAPInvNoRecreatedOnly IsNot Nothing AndAlso clsCommon.myLen(strAPInvNoRecreatedOnly) > 0 Then
                    objVendorInvHead.Document_No = strAPInvNoRecreatedOnly
                    AP_NOT_Exist = False
                End If
                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(obj.PR_Date, "dd/MM/yyyy")
                objVendorInvHead.Vendor_Code = obj.Vendor_Code
                objVendorInvHead.Vendor_Name = obj.Vendor_Name
                objVendorInvHead.Vendor_Invoice_No = obj.Vendor_Invoice_No
                objVendorInvHead.Vendor_Invoice_Date = clsCommon.GetPrintDate(obj.PR_Date, "dd/MM/yyyy")
                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(obj.Bill_To_Location, trans)
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + obj.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + obj.Vendor_Name)
                End If
                objVendorInvHead.Document_Type = obj.NoteType '"D" ''Purchase Return will make Debit Note of PIInvoice
                ''objVendorInvHead.PO_Number = obj.p

                '' ''added by priti
                ''objVendorInvHead.RefDocType = clsCommon.myCstr(cmbRefType.SelectedValue)
                ''objVendorInvHead.RefDocNo = txtRefDocNo.Text
                '' '' priti ends here
                'objVendorInvHead.Order_No = txtOrderNo.Text
                objVendorInvHead.Total_Tax = obj.Total_Tax_Amt
                objVendorInvHead.GSTRegistered = obj.GSTRegistered
                objVendorInvHead.On_Hold = False
                objVendorInvHead.Description = "Against Purchase Return No " + obj.PR_No
                objVendorInvHead.Tax_Calculation_Type = obj.Tax_Calculation_Type
                objVendorInvHead.Tax_Group = obj.Tax_Group
                If (clsCommon.myLen(obj.TAX1) > 0) Then
                    objVendorInvHead.TAX1 = obj.TAX1
                    objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
                    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
                End If
                If (clsCommon.myLen(obj.TAX2) > 0) Then
                    objVendorInvHead.TAX2 = obj.TAX2
                    objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
                    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
                End If
                If (clsCommon.myLen(obj.TAX3) > 0) Then
                    objVendorInvHead.TAX3 = obj.TAX3
                    objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
                    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
                End If
                If (clsCommon.myLen(obj.TAX4) > 0) Then
                    objVendorInvHead.TAX4 = obj.TAX4
                    objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
                    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
                End If
                If (clsCommon.myLen(obj.TAX5) > 0) Then
                    objVendorInvHead.TAX5 = obj.TAX5
                    objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
                    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
                End If
                If (clsCommon.myLen(obj.TAX6) > 0) Then
                    objVendorInvHead.TAX6 = obj.TAX6
                    objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
                    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
                End If
                If (clsCommon.myLen(obj.TAX7) > 0) Then
                    objVendorInvHead.TAX7 = obj.TAX7
                    objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
                    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
                End If
                If (clsCommon.myLen(obj.TAX8) > 0) Then
                    objVendorInvHead.TAX8 = obj.TAX8
                    objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
                    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
                End If
                If (clsCommon.myLen(obj.TAX9) > 0) Then
                    objVendorInvHead.TAX9 = obj.TAX9
                    objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
                    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
                End If
                If (clsCommon.myLen(obj.TAX10) > 0) Then
                    objVendorInvHead.TAX10 = obj.TAX10
                    objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
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
                objVendorInvHead.RoundOffAmount = obj.RoundOffAmt
                objVendorInvHead.Document_Total = obj.PR_Total_Amt
                objVendorInvHead.Balance_Amt = obj.PR_Total_Amt
                objVendorInvHead.Against_PurchaseReturn_No = obj.PR_No
                objVendorInvHead.Invoice_Type = "AP"
                '' currency update
                objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
                objVendorInvHead.ConvRate = obj.ConvRate

                dt = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If

                'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

                'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
                'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
                'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

                'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
                'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
                'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

                'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
                'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
                'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

                'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
                'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
                'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

                'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
                'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
                'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

                'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
                'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
                'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

                'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
                'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
                'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

                'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
                'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
                'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

                'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
                'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
                'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

                'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
                'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
                'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10

                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)

                objVendorInvHead.Empty_Amount = 0
                Dim strFirstItemCode As String = GetFirstItemCode(obj.Arr)
                Dim ii As Integer = 0
                For Each objPIDetail As clsPurchasReturnDetail In obj.Arr
                    Dim strICode As String = objPIDetail.Item_Code
                    If clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                        strICode = strFirstItemCode
                    End If

                    ''Fill VendorInvoice details Data
                    ''qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing where TSPL_ITEM_MASTER.Item_Code='" + objPIDetail.Item_Code + "'"
                    qry = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Other_2,TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing,TSPL_PURCHASE_ACCOUNTS.Other_1,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount,TSPL_PURCHASE_ACCOUNTS.Purchase_JobWork from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing where TSPL_ITEM_MASTER.Item_Code='" + strICode + "'"

                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("Please set Purchase Account set for item " + strICode + "(" + objPIDetail.Item_Desc + ")")
                    End If
                    ''Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                    ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    ''    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
                    ''End If
                    ''Dim strPaybleCleanigCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Payable_Clearing"))
                    Dim strPaybleCleanigCtrlAC As String = Nothing
                    If obj.isJobWorkOutward Then
                        If clsCommon.myLen(clsCommon.myCstr(dt.Rows(0)("Purchase_JobWork"))) = 0 Then
                            Throw New Exception("Please set Purchase Job Work Account for Account set " + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")))
                        End If
                        strPaybleCleanigCtrlAC = clsCommon.myCstr(dt.Rows(0)("Purchase_JobWork"))
                        strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
                    Else
                        If clsCommon.myLen(obj.Auto_Gen_Againnt_PI_No) > 0 Then
                            strPaybleCleanigCtrlAC = clsCommon.myCstr(dt.Rows(0)("Other_2"))
                        ElseIf obj.is_Reject_Item Then
                            strPaybleCleanigCtrlAC = clsCommon.myCstr(dt.Rows(0)("Other_1")) '' rejected account
                        Else
                            strPaybleCleanigCtrlAC = clsCommon.myCstr(dt.Rows(0)("Credit_Debit_Note_Clearing"))
                        End If
                        If clsCommon.myLen(strPaybleCleanigCtrlAC) <= 0 Then
                            Throw New Exception("Purchase set Credit Debit Note Clearing/Short control/Rejected account of purchasee account set for item " + objPIDetail.Item_Desc)
                        End If
                    End If





                    strPaybleCleanigCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleCleanigCtrlAC, obj.Bill_To_Location, trans)
                    Dim strPaybleCleanigCtrlACName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_GL_ACCOUNTS where Account_Code='" + strPaybleCleanigCtrlAC + "'", trans))



                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        objVendorInvHead.Empty_Account = clsCommon.myCstr(dt.Rows(0)("EmptyAccount"))
                        objVendorInvHead.Empty_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Empty_Account, obj.Bill_To_Location, trans)
                    End If

                    ''If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("isEmpty")), "Y") = CompairStringResult.Equal Then
                    ''    Dim dblVal As Double = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DefaultValue, objPIDetail.Unit_code, trans))
                    ''    objVendorInvHead.Empty_Amount += dblVal * objPIDetail.PR_Qty
                    ''End If

                    ''If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    ''    Throw New Exception("Please set Payable Clearing Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + objPIDetail.Item_Code + "(" + objPIDetail.Item_Desc + ") ")
                    ''End If

                    '=========Added by preeti gupta Against ticket no[BHA/27/11/18-000727]11/12/2018===================
                    If isApplyPurchaseAccounting = False Then
                        clsInventoryMovement.UpdateInvControlAccount(objPIDetail.PR_No, "Purchase Return", objPIDetail.Item_Code, "", strPaybleCleanigCtrlAC, "", trans)
                    End If
                    '========================================================================

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
                    If isAllQtyZero Then
                        If Not clsCommon.CompairString(objPIDetail.Row_Type, clsItemRowType.RowTypeMisc) = CompairStringResult.Equal Then
                            objVendorInvDetail.Landed_Amount = objPIDetail.Landed_Cost_Amount - objPIDetail.Amt_Less_Discount
                        End If
                    Else
                        objVendorInvDetail.Landed_Amount = objPIDetail.Landed_Cost_Amount - objPIDetail.Amt_Less_Discount
                    End If

                    objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount

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

                If obj.objPIRemittance IsNot Nothing Then
                    objVendorInvHead.RemittanceObject = New clsRemittance()
                    objVendorInvHead.RemittanceObject.Vendor_Code = obj.objPIRemittance.Vendor_Code
                    objVendorInvHead.RemittanceObject.Vendor_Name = obj.objPIRemittance.Vendor_Name
                    objVendorInvHead.RemittanceObject.Document_No = obj.objPIRemittance.Document_No
                    objVendorInvHead.RemittanceObject.Document_Date = obj.objPIRemittance.Document_Date
                    objVendorInvHead.RemittanceObject.Document_Type = obj.objPIRemittance.Document_Type
                    objVendorInvHead.RemittanceObject.Document_Amount = obj.objPIRemittance.Document_Amount
                    objVendorInvHead.RemittanceObject.Service_Type = obj.objPIRemittance.Service_Type
                    objVendorInvHead.RemittanceObject.Actual_TDS_Base = obj.objPIRemittance.Actual_TDS_Base
                    objVendorInvHead.RemittanceObject.Calculated_TDS_Base = obj.objPIRemittance.Calculated_TDS_Base
                    objVendorInvHead.RemittanceObject.Actual_TDS = obj.objPIRemittance.Actual_TDS
                    objVendorInvHead.RemittanceObject.Calculated_TDS = obj.objPIRemittance.Calculated_TDS
                    objVendorInvHead.RemittanceObject.Actual_Surcharge = obj.objPIRemittance.Actual_Surcharge
                    objVendorInvHead.RemittanceObject.Calculated_Surcharge = obj.objPIRemittance.Calculated_Surcharge
                    objVendorInvHead.RemittanceObject.Actual_Edu_Cess = obj.objPIRemittance.Actual_Edu_Cess
                    objVendorInvHead.RemittanceObject.Calculated_Edu_Cess = obj.objPIRemittance.Calculated_Edu_Cess
                    objVendorInvHead.RemittanceObject.Actual_Sec_Educess = obj.objPIRemittance.Actual_Sec_Educess
                    objVendorInvHead.RemittanceObject.Calculated_Sec_Educess = obj.objPIRemittance.Calculated_Sec_Educess
                    objVendorInvHead.RemittanceObject.Actual_Total_TDS = obj.objPIRemittance.Actual_Total_TDS
                    objVendorInvHead.RemittanceObject.Calculated_Total_TDS = obj.objPIRemittance.Calculated_Total_TDS
                    objVendorInvHead.RemittanceObject.Fiscal_Year = obj.objPIRemittance.Fiscal_Year
                    objVendorInvHead.RemittanceObject.Quarter = obj.objPIRemittance.Quarter
                    objVendorInvHead.RemittanceObject.Section_Code = obj.objPIRemittance.Section_Code
                    objVendorInvHead.RemittanceObject.Section_Description = obj.objPIRemittance.Section_Description
                    objVendorInvHead.RemittanceObject.Branch_Code = obj.objPIRemittance.Branch_Code
                    objVendorInvHead.RemittanceObject.Deduction_Code = obj.objPIRemittance.Deduction_Code
                    objVendorInvHead.RemittanceObject.TDS_Per = obj.objPIRemittance.TDS_Per
                    objVendorInvHead.RemittanceObject.Surcharge_Per = obj.objPIRemittance.Surcharge_Per
                    objVendorInvHead.RemittanceObject.Edu_Cess_Per = obj.objPIRemittance.Edu_Cess_Per
                    objVendorInvHead.RemittanceObject.Sec_Educess_Per = obj.objPIRemittance.Sec_Educess_Per
                    objVendorInvHead.RemittanceObject.Select_By = obj.objPIRemittance.Select_By
                    objVendorInvHead.RemittanceObject.IsTDSOverride = obj.objPIRemittance.IsTDSOverride
                    objVendorInvHead.RemittanceObject.IsApplyTDS = obj.objPIRemittance.IsApplyTDS

                    objVendorInvHead.TDS_Base_Actual_Amount = obj.objPIRemittance.Actual_TDS_Base
                    objVendorInvHead.TDS_Base_Calculated_Amount = obj.objPIRemittance.Calculated_TDS_Base
                    objVendorInvHead.TDS_Percentage = obj.objPIRemittance.TDS_Per
                    objVendorInvHead.TDS_Actual_Amount = obj.objPIRemittance.Actual_Total_TDS
                    objVendorInvHead.TDS_Calculated_Amount = obj.objPIRemittance.Calculated_Total_TDS
                    objVendorInvHead.Nature_of_deduction = obj.objPIRemittance.Deduction_Code
                    objVendorInvHead.Branch_Code = obj.objPIRemittance.Branch_Code
                    objVendorInvHead.Balance_Amt = obj.PR_Total_Amt - obj.objPIRemittance.Actual_Total_TDS
                    objVendorInvHead.Section_Code = obj.objPIRemittance.Section_Code
                End If
                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If


                isSaved = isSaved AndAlso objVendorInvHead.SaveData(objVendorInvHead, AP_NOT_Exist, trans)
                isSaved = isSaved AndAlso clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, strAPVoucherNoRecreateOnly)
            End If
            If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.OpenPOforRejectShortageQty, clsFixedParameterCode.OpenPOforRejectShortageQty, trans)) = 1) Then
                For Each objtr As clsPurchasReturnDetail In obj.Arr
                    If clsCommon.myLen(objtr.PO_ID) > 0 Then
                        Dim qr As String = "Update TSPL_PURCHASE_ORDER_HEAD SET close_yn='N' where TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No='" + objtr.PO_ID + "'"
                        clsDBFuncationality.ExecuteNonQuery(qr, trans)
                    End If
                Next
            End If
            qry = "Update TSPL_PR_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.PR_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where PR_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_PR_HEAD", "PR_No", trans)
            ''Throw New Exception("AASFASFASDF")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetPIBalance(ByVal Vendor_Code As String, ByVal PR_No As String, ByVal Against_PI As String, ByVal PR_Date As DateTime, ByVal trans As SqlTransaction) As Decimal
        Dim strQryForRejectedAmt As String = "- ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(Document_Total,0) as Document_Total,(case when inn.GSTRegistered =0 or inn.RCM=1 then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount " &
             "from TSPL_VENDOR_INVOICE_HEAD as inn where inn.Against_PurchaseReturn_No  in (SELECT PR_No  FROM TSPL_PR_HEAD WHERE Against_PI =TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No and TSPL_PR_HEAD.PR_No <>'" + PR_No + "' ) and inn.Document_Type='D' and inn.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code )z ) else 0 end,0) "
        '' this code is wrritten for that debit note which is created auto through PI and Pr is not created against that PI
        Dim strQryForRejectedAmtforNonPR As String = "- ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(Document_Total,0) as Document_Total,(case when inn.GSTRegistered =0 or inn.RCM=1 then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount " &
      "from TSPL_VENDOR_INVOICE_HEAD as inn where inn.Against_POInvoice_No=TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No and inn.Document_Type='D' and inn.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code )z ) else 0 end,0) "
        Dim strqryforNonPostedPRTypeofInvoiceBAlance As String = "   - ISNULL(case when ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='I' then (Select sum (z.Document_Total)-sum (isnull(z.TaxAmount,0)) from ( select  isnull(PR_Total_Amt ,0) as Document_Total,(case when inn.GSTRegistered =0  then  ( case when len(isnull(inn.Tax1,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax1)='Y'  then inn.TAX1_Amt else 0 end +  case when len(isnull(inn.TAX2,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX2)='Y'  then inn.TAX2_Amt else 0 end +  case when len(isnull(inn.TAX3,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX3)='Y'  then inn.TAX3_Amt else 0 end +  case when len(isnull(inn.TAX4,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX4)='Y'  then inn.TAX4_Amt else 0 end +  case when len(isnull(inn.TAX5,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX5)='Y'  then inn.TAX5_Amt else 0 end +  case when len(isnull(inn.TAX6,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX6)='Y'   then inn.TAX6_Amt else 0 end +  case when len(isnull(inn.TAX7,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX7)='Y'  then inn.TAX7_Amt else 0 end +  case when len(isnull(inn.TAX8 ,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX8)='Y'  then inn.TAX8_Amt else 0 end +  case when len(isnull(inn.TAX9,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.TAX9)='Y'  then inn.TAX9_Amt else 0 end +  case when len(isnull(inn.Tax10,''))> 0 and (Select Tax_Recoverable from TSPL_TAX_MASTER where Tax_Code =inn.Tax10)='Y'  then inn.TAX10_Amt else 0 end ) else 0 end) as TaxAmount from TSPL_PR_HEAD  as inn where inn.Against_PI ='" & Against_PI & "' and inn.PR_No <>'" & PR_No & "' AND INN.transaction_type ='R' AND inn.Note_type ='D' and inn.Status =0 )z ) else 0 end,0)"
        Dim strTaxRecovarableQuery As String = "" 'By balwinder because tax amount should be added for Balance amount as asked by ranjana mam

        Dim qry As String = "Select (isnull(PendingAmt,0)-isnull(TDSAmt,0)) as PendingAmt from ( select Case When TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' Then 'Debit Note' Else case  When TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' Then 'Credit Note' Else 'Invoice' End End As [DocType],  " &
     " TSPL_VENDOR_INVOICE_HEAD.Document_No, Case When ISNULL(Against_POInvoice_No,'')<>'' Then Against_POInvoice_No When ISNULL(Against_PurchaseReturn_No,'')<> '' Then Against_PurchaseReturn_No Else Document_No End as PurchaseInvoice," &
     " Case When ISNULL(Against_POInvoice_No,'')<>'' Then (Select convert(varchar,PI_Date,103)  from TSPL_PI_HEAD where PI_No =Against_POInvoice_No)  When ISNULL(Against_PurchaseReturn_No,'')<> '' Then (Select convert(varchar,PR_Date,103) from TSPL_PR_HEAD where PR_No  =Against_PurchaseReturn_No ) Else TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date End as DocumentDate, " &
     " TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date as [DocDate] ,  " &
     " TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_No as [VendorInvoiceNo], " &
     " TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " as [OriginalAmt]  ," &
     " TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount as [TDSAmt], " &
     " (TSPL_VENDOR_INVOICE_HEAD.Document_Total-TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount) " + strTaxRecovarableQuery + "  as [NetAmount], " &
     " TSPL_VENDOR_INVOICE_HEAD.Document_Total " + strTaxRecovarableQuery + " - ISNULL((Select SUM(Applied_Amount) from TSPL_PAYMENT_DETAIL Where TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No and not exists (select 1 from TSPL_BANK_REVERSE where TSPL_BANK_REVERSE.Reverse_Document='Payments' and TSPL_BANK_REVERSE.Document_No=TSPL_PAYMENT_DETAIL.Payment_No) ),0) " + strQryForRejectedAmt + " " + strQryForRejectedAmtforNonPR + " " + strqryforNonPostedPRTypeofInvoiceBAlance + " " &
     " -ISNULL((Select SUM(isnull(TSPL_PAYMENT_ADJUSTMENT_DETAIL.Amount,0)) from TSPL_PAYMENT_ADJUSTMENT_HEADER LEFT OUTER JOIN TSPL_PAYMENT_ADJUSTMENT_DETAIL on TSPL_PAYMENT_ADJUSTMENT_Header.Adjustment_No=TSPL_PAYMENT_ADJUSTMENT_DETAIL.Adjustment_No Where isnull(TSPL_PAYMENT_ADJUSTMENT_Header.Doc_No,'') = isnull(TSPL_VENDOR_INVOICE_HEAD.Document_No,'') ),0) " &
     " as [PendingAmt],TSPL_VENDOR_INVOICE_HEAD.ConvRate " &
     " ,isnull(( select top 1 TSPL_REVALUATION_HEAD.Currency_Rate from TSPL_REVALUATION_DETAIL left outer join TSPL_REVALUATION_HEAD on TSPL_REVALUATION_HEAD.Document_No=TSPL_REVALUATION_DETAIL.Document_No  where TSPL_REVALUATION_DETAIL.AP_Invoice_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_REVALUATION_HEAD.Status=1 and TSPL_REVALUATION_HEAD.Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(PR_Date), "dd/MMM/yyyy hh:mm tt") + "' order by TSPL_REVALUATION_HEAD.Document_Date desc),0) as ConvRateRevaluation " &
     " from TSPL_VENDOR_INVOICE_HEAD " &
     " Left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  " &
     " WHERE TSPL_VENDOR_INVOICE_HEAD.Vendor_Code ='" + Vendor_Code + "' AND (ISNULL(RefDocNo,'')= '' AND ISNULL(Against_PurchaseReturn_No,'')= '')  and not ( ISNULL( TSPL_VENDOR_INVOICE_HEAD.Against_POInvoice_No,'')<>'' and TSPL_VENDOR_INVOICE_HEAD.Document_Type='D') " &
     " and TSPL_VENDOR_INVOICE_HEAD.RefDocType<>'REVALUATION ENTRY'" &
    " and isnull(TSPL_VENDOR_INVOICE_HEAD.posting_date,'')<>'') FINALQRY WHERE PurchaseInvoice ='" & Against_PI & "' and FINALQRY.PendingAmt>0 ORDER BY Convert(Date,FINALQRY.DocDate,103)  "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsPurchasReturnDetail)) As String
        For Each objtr As clsPurchasReturnDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
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
        Dim obj As clsPurchasReturnHead = clsPurchasReturnHead.GetData(strCode, NavigatorType.Current, trans)
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PR_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase Order", "Purchase Return", obj.Bill_To_Location, obj.PR_Date, trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsPRAdditionChargeInsurance.DeleteData(strCode, trans)
                clsSerializeInvenotry.DeleteData("Purchase Return", strCode, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_PR_HEAD", "PR_No", "TSPL_PR_DETAIL", "PR_No", trans)
                Dim qry As String = "delete from TSPL_PR_DETAIL where PR_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PR_HEAD where PR_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, strCode, trans)
                'If (isSaved) Then
                '    trans.Commit()
                'Else
                '    trans.Rollback()
                'End If
            Catch ex As Exception
                'trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isApplyPurchaseAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0, False, True)
            Dim qry As String = "select TSPL_PR_HEAD.PR_No,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_JOURNAL_MASTER.Voucher_No,PIAPInv.Document_No as PIAPInv_Document_No,PIAPInv.Update_PR_APInvoice_Balance_Amt as PIAPInv_Update_PR_APInvoice_Balance_Amt,TSPL_VENDOR_INVOICE_HEAD.Document_Total ,TSPL_VENDOR_INVOICE_HEAD.TDS_Actual_Amount,TSPL_PR_HEAD.transaction_type  from TSPL_PR_HEAD 
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_PurchaseReturn_No=TSPL_PR_HEAD.PR_No
left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_VENDOR_INVOICE_HEAD.Document_No and TSPL_JOURNAL_MASTER.Source_Code='AP-CN'
left outer join TSPL_PJV_HEAD on TSPL_PJV_HEAD.Invoice_No=TSPL_PR_HEAD.PR_No 
left outer join TSPL_VENDOR_INVOICE_HEAD as PIAPInv on PIAPInv.Against_POInvoice_No=TSPL_PR_HEAD.Against_PI
where PR_No='" + strDocNo + "' and TSPL_PR_HEAD.Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                HistoryUpdate(strDocNo, trans)
                If clsCommon.myCdbl(dt.Rows(0)("PIAPInv_Update_PR_APInvoice_Balance_Amt")) > 0 Then
                    If clsCommon.myLen(dt.Rows(0)("PIAPInv_Document_No")) > 0 Then
                        qry = "update TSPL_VENDOR_INVOICE_HEAD set Balance_Amt=Balance_Amt+ " + clsCommon.myCstr(clsCommon.myCstr(dt.Rows(0)("Document_Total")) - clsCommon.myCstr(dt.Rows(0)("TDS_Actual_Amount"))) + " where Document_No='" + clsCommon.myCstr(dt.Rows(0)("PIAPInv_Document_No")) + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If

                Dim docNo As String = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                qry = "delete from TSPL_JOURNAL_DETAILS where TSPL_JOURNAL_DETAILS.Voucher_No in (select Voucher_No from  TSPL_JOURNAL_MASTER where Source_Doc_No='" & docNo & "')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Source_Doc_No ='" & docNo & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)


                ''Delete AP Invoice
                docNo = clsCommon.myCstr(dt.Rows(0)("Document_No"))

                qry = "select Payment_No from TSPL_PAYMENT_DETAIL  where Document_No='" & docNo & "'"
                Dim dtAP As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtAP IsNot Nothing AndAlso dtAP.Rows.Count > 0 Then
                    qry = "AP-Invoice " + docNo + " is used in following Payment -"
                    For Each dr As DataRow In dtAP.Rows
                        qry += Environment.NewLine + clsCommon.myCstr(dr("Payment_No"))
                    Next
                    Throw New Exception(qry)
                End If
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, docNo, "TSPL_VENDOR_INVOICE_HEAD", "Document_No", "TSPL_VENDOR_INVOICE_DETAIL", "Document_No", trans)
                qry = "delete from TSPL_VENDOR_INVOICE_DETAIL where Document_No='" & docNo & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_VENDOR_INVOICE_HEAD where Document_No='" & docNo & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)


                docNo = clsCommon.myCstr(dt.Rows(0)("PR_No"))


                Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(docNo), "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)
                qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & docNo & "' and Trans_Type='Purchase Return'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)


                Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PO-RT' and Source_Doc_No='" & docNo & "'", trans)
                If clsCommon.myLen(VoucherNo) > 0 Then
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                    qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" & VoucherNo & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" & VoucherNo & "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If

                If isApplyPurchaseAccounting = True And (clsCommon.CompairString(dt.Rows(0)("transaction_type"), "P") = CompairStringResult.Equal) Then
                    qry = "delete from TSPL_Inventory_Movement where Trans_Type='IC-AD' and Source_Doc_No in ( select Adjustment_No  from TSPL_ADJUSTMENT_HEADER where Against_PurchaseReturn_No in ('" + docNo + "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_ADJUSTMENT_DETAIL where  TSPL_ADJUSTMENT_DETAIL.Adjustment_No in ( select Adjustment_No  from TSPL_ADJUSTMENT_HEADER where Against_PurchaseReturn_No in ('" + docNo + "'))"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "delete from TSPL_ADJUSTMENT_HEADER where Against_PurchaseReturn_No in ('" + docNo + "')"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If

                qry = "update TSPL_PR_HEAD set Status=0 where PR_No='" & docNo & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(docNo), "TSPL_PR_HEAD", "PR_No", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function CancelData(ByVal Form_Id As String, ByVal Doc_No As String) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Doc_Type As String = Nothing

            Dim obj As clsPurchasReturnHead = clsPurchasReturnHead.GetData(Doc_No, NavigatorType.Current, trans)
            If obj Is Nothing OrElse clsCommon.myLen(obj.PR_No) <= 0 Then
                Throw New Exception("Document- " & Doc_No & " not found")
            End If

            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, Doc_No, "TSPL_PR_HEAD", "PR_No", "TSPL_PR_DETAIL", "PR_No", trans)

            clsPurchasReturnHead.ReverseAndUnpost(Doc_No, trans)
            clsPurchasReturnHead.DeleteData(Doc_No, trans)

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

End Class

Public Class clsPurchasReturnDetail
#Region "Variables"
    Public PR_No As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Status As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public PR_Qty As Double = 0
    Public PI_Id As String = Nothing
    Public PO_ID As String = Nothing
    Public GRN_ID As String = Nothing
    Public MRN_ID As String = Nothing
    Public SRN_Id As String = Nothing
    Public OrgPIQty As Double = 0 'Not a Table Field
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
    Public Amt_Less_Discount As Double = 0
    Public Taxable_Amount_Per As Decimal = 0
    Public Taxable_Amount As Decimal = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public MRP As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public Assessable As Double = 0
    Public AssessableAmt As Double = 0
    Public Empty_Amount As Double = 0

    Public Landed_Cost_Rate As Double = 0
    Public Landed_Cost_Amount As Double = 0

    Public Total_AddtionalCost_PerUnit As Double = 0
    Public Total_NonRecTax_PerUnit As Double = 0
    Public Total_RecTax_PerUnit As Double = 0
    Public Bin_No As String = Nothing

    ''====================19/10/2016====================================
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

    Public Capex_SubCode As String = Nothing
    Public Capex_Code As String = Nothing
    Public Emergency As Boolean = Nothing
    Public Category As String = Nothing
    ''==================================================================
    Public Against_Item_Wise_Tax_Rate As String = Nothing

    Public Insurance_Base_Amt As Decimal
    Public Insurance_Per As Decimal
    Public Item_Insurance_Base_Amt As Decimal = 0
    Public Item_Insurance_Apply_On As String = Nothing
    Public Item_Insurance_Rate As Decimal = 0
    Public Item_Insurance_Amt As Decimal = 0
    Public Item_Amt_After_Insurance As Decimal = 0
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsPurchasReturnDetail), ByVal trans As SqlTransaction, Optional ByVal Transaction_type As String = "") As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsPurchasReturnDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PR_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "PR_Qty", obj.PR_Qty)
                clsCommon.AddColumnsForChange(coll, "PI_Id", obj.PI_Id, True)

                clsCommon.AddColumnsForChange(coll, "PO_ID", obj.PO_ID, True)
                clsCommon.AddColumnsForChange(coll, "GRN_ID", obj.GRN_ID, True)
                clsCommon.AddColumnsForChange(coll, "MRN_ID", obj.MRN_ID, True)
                clsCommon.AddColumnsForChange(coll, "SRN_Id", obj.SRN_Id, True)

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

                clsCommon.AddColumnsForChange(coll, "Taxable_Amount_Per", obj.Taxable_Amount_Per)
                clsCommon.AddColumnsForChange(coll, "Taxable_Amount", obj.Taxable_Amount)

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
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.Bin_No)
                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Landed_Cost_Rate", obj.Landed_Cost_Rate)
                clsCommon.AddColumnsForChange(coll, "Landed_Cost_Amount", obj.Landed_Cost_Amount)

                clsCommon.AddColumnsForChange(coll, "Total_AddtionalCost_PerUnit", obj.Total_AddtionalCost_PerUnit)
                clsCommon.AddColumnsForChange(coll, "Total_NonRecTax_PerUnit", obj.Total_NonRecTax_PerUnit)
                clsCommon.AddColumnsForChange(coll, "Total_RecTax_PerUnit", obj.Total_RecTax_PerUnit)


                If obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd-MMM-yyyy"))
                End If
                If obj.Expiry_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd-MMM-yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Assessable", obj.Assessable)
                clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)
                clsCommon.AddColumnsForChange(coll, "Empty_Amount", obj.Empty_Amount)

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

                clsCommon.AddColumnsForChange(coll, "Capex_Code", obj.Capex_Code, True)
                clsCommon.AddColumnsForChange(coll, "Capex_SubCode", obj.Capex_SubCode, True)
                clsCommon.AddColumnsForChange(coll, "Category", obj.Category, True)
                clsCommon.AddColumnsForChange(coll, "Emergency", IIf(obj.Emergency, 1, 0))

                clsCommon.AddColumnsForChange(coll, "Against_Item_Wise_Tax_Rate", obj.Against_Item_Wise_Tax_Rate, True)

                clsCommon.AddColumnsForChange(coll, "Insurance_Base_Amt", obj.Insurance_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "Insurance_Per", obj.Insurance_Per)

                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Base_Amt", obj.Item_Insurance_Base_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Apply_On", obj.Item_Insurance_Apply_On)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Rate", obj.Item_Insurance_Rate)
                clsCommon.AddColumnsForChange(coll, "Item_Insurance_Amt", obj.Item_Insurance_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Amt_After_Insurance", obj.Item_Amt_After_Insurance)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PR_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                If Transaction_type <> "P" Then
                    clsSerializeInvenotry.SaveData("Purchase Return", strDocNo, dtDocDate, "O", obj.Item_Code, obj.Location, obj.Line_No, obj.arrSrItem, trans)
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function CompletePI(ByVal strDoccode As String, ByVal strICode As String, ByVal LineNo As Integer) As Boolean
        Dim qry As String = "update TSPL_PR_DETAIL set Status=1 where PR_No='" + strDoccode + "' and Line_No='" + clsCommon.myCstr(LineNo) + "' and Item_Code='" + strICode + "'"
        Return clsDBFuncationality.ExecuteNonQuery(qry)
    End Function
End Class

<Serializable()>
Public Class clsPRAdditionChargeInsurance
#Region "Variables"
    Public TR_Code As String = Nothing
    Public PR_No As String = Nothing
    Public AC_Code As String = Nothing
    Public AC_Name As String = Nothing ''Not a table Field
    Public Amount As Decimal
#End Region
    Public Shared Function SaveData(ByVal DocNo As String, ByVal DocDate As DateTime, ByVal arr As List(Of clsPRAdditionChargeInsurance), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each objtr As clsPRAdditionChargeInsurance In arr
                    coll = New Hashtable()
                    objtr.TR_Code = clsERPFuncationality.GetNextCode(trans, DocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                    clsCommon.AddColumnsForChange(coll, "TR_Code", objtr.TR_Code)
                    clsCommon.AddColumnsForChange(coll, "PR_No", DocNo)
                    clsCommon.AddColumnsForChange(coll, "AC_Code", objtr.AC_Code)
                    clsCommon.AddColumnsForChange(coll, "Amount", objtr.Amount)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PR_ADDITION_CHARGE_INSURANCE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal DocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "delete from TSPL_PR_ADDITION_CHARGE_INSURANCE where PR_No='" + DocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return True
    End Function
    Public Shared Function GetData(ByVal DocNo As String, ByVal trans As SqlTransaction) As List(Of clsPRAdditionChargeInsurance)
        Dim qry As String = "select TSPL_PR_ADDITION_CHARGE_INSURANCE.*,TSPL_Additional_Charges.Description as AC_Name  from TSPL_PR_ADDITION_CHARGE_INSURANCE left outer join TSPL_Additional_Charges on TSPL_Additional_Charges.Code=TSPL_PR_ADDITION_CHARGE_INSURANCE.AC_Code where TSPL_PR_ADDITION_CHARGE_INSURANCE.PR_No='" + DocNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim Arr_ACInsurance As List(Of clsPRAdditionChargeInsurance) = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Arr_ACInsurance = New List(Of clsPRAdditionChargeInsurance)
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsPRAdditionChargeInsurance()
                objtr.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                objtr.PR_No = clsCommon.myCstr(dr("PR_No"))
                objtr.AC_Code = clsCommon.myCstr(dr("AC_Code"))
                objtr.AC_Name = clsCommon.myCstr(dr("AC_Name"))
                objtr.Amount = clsCommon.myCstr(dr("Amount"))
                Arr_ACInsurance.Add(objtr)
            Next
        End If
        Return Arr_ACInsurance
    End Function
End Class


