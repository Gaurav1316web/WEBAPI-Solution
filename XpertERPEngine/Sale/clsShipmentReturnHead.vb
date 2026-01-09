Imports System.Data.SqlClient

Public Class clsShipmentReturnHead
#Region "Variables"
    Public TotalCAN As Double = 0
    Public ShippedCAN As Double = 0
    Public CrateQty As Integer = 0
    Public Is_Taxable As Integer = 0
    Public Gate_ReturnNo As String = Nothing
    Public Sub_Location_code As String = String.Empty
    Public CrateMan As Double = 0
    Public jaali As Double = 0
    Public Box As Double = 0
    Public Trans_type As String = String.Empty
    Public RoundOffAmount As Double = 0
    Public Return_Type As String = Nothing
    Public Shift_Type As String = Nothing
    Public Damage_Type As String = Nothing
    Public Cust_PO_No As String = Nothing
    Public Price_Group_Code As String = Nothing
    Public Invoice_Type As String = Nothing
    Public PROJECT_ID As String = Nothing
    Public Price_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public HeadDisc_Per As Double = 0
    Public HeadDisc_Amt As Double = 0
    Public HeadDisc_PerAmt As Double = 0
    Public TotCashDiscAmt As Double = 0

    Public Document_Code As String = Nothing
    Public Document_Date As DateTime
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing  'Not a table field
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public On_Hold As Boolean = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public Bill_To_Location As String = Nothing
    Public BillToLocationName As String = Nothing 'Not a table field
    Public Ship_To_Location As String = Nothing
    Public ShipToLocationName As String = Nothing 'Not a table field
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
    Public Total_Amt As Double = 0
    Public Comments As String = Nothing
    Public Comp_Code As String = Nothing
    Public Terms_Code As String = Nothing
    Public TermsName As String = Nothing
    Public Due_Date As DateTime? = Nothing
    Public Posting_Date As DateTime? = Nothing
    Public Carrier As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public VehicleNo As String = Nothing
    Public GRNo As String = Nothing
    Public GENo As String = Nothing
    Public GEDate As Date? = Nothing
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
    Public Dept As String = Nothing
    Public Dept_Desc As String = Nothing
    Public Item_Type As String = Nothing
    Public Challan_No As String = Nothing
    Public Challan_Date As DateTime? = Nothing
    Public Inv_No As String = Nothing
    Public Inv_Date As DateTime? = Nothing
    Public Against_Invoice_No As String = Nothing
    Public Is_Internal As Boolean = False
    Public Tax_Calculation_Type As EnumTaxCalucationType
    Public Salesman_Code As String = Nothing
    Public Salesman_Name As String = Nothing
    Public Arr As List(Of clsShipmentReturnDetail) = Nothing
    'Public Booth_Arr As List(Of clsDSSalesReturnBookingDetail) = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public CURRENCY_CODE As String = ""
    Public ConvRate As Decimal
    Public ApplicableFrom As Date? = Nothing
    Public Is_Cancelled As Integer = 0
    Public Screen_Type As String = ""
    Public IsSampling As Integer = 0
    Public Total_Comm_Amt As Double = 0
    Public Distributor_Commission_TotalAmt As Decimal = 0
    Public Transporter_Commission_TotalAmt As Decimal = 0
    Public Security_TotalAmt As Decimal = 0
#End Region
    Public Function SaveData(ByVal obj As clsShipmentReturnHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsShipmentReturnHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim Trans_type_Str As String = ""
        Dim isSaved As Boolean = True

        Trans_type_Str = clsCommon.myCstr(obj.Trans_type) + "-SR"
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmSaleReturndairy, obj.Bill_To_Location, obj.Document_Date, trans)

            clsSerializeInvenotry.DeleteData("Sale Return", obj.Document_Code, trans)
            clsBatchInventory.DeleteData(Trans_type_Str, obj.Document_Code, trans)
            Dim qry As String = "delete from TSPL_SD_Shipment_RETURN_DETAIL where Document_Code='" & obj.Document_Code & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDairyShipmentReturn, clsDocTransactionType.NA, obj.Bill_To_Location)
            End If
            If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "TotalCAN", obj.TotalCAN)
            clsCommon.AddColumnsForChange(coll, "ShippedCAN", obj.ShippedCAN)
            clsCommon.AddColumnsForChange(coll, "CrateQty", obj.CrateQty)
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", obj.Is_Taxable)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Internal", IIf(obj.Is_Internal, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Inv_No", obj.Inv_No)
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
            clsCommon.AddColumnsForChange(coll, "RoundOffAmount", obj.RoundOffAmount)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "PROJECT_ID", obj.PROJECT_ID, True)
            clsCommon.AddColumnsForChange(coll, "Sub_Location_code", obj.Sub_Location_code, True)

            If clsCommon.myLen(obj.Due_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Due_Date", clsCommon.GetPrintDate(obj.Due_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Due_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Carrier", obj.Carrier)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "VehicleNo", obj.VehicleNo)
            clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo)
            clsCommon.AddColumnsForChange(coll, "GENo", obj.GENo)
            clsCommon.AddColumnsForChange(coll, "Dept", obj.Dept)
            clsCommon.AddColumnsForChange(coll, "Dept_Desc", obj.Dept_Desc)
            clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type)
            clsCommon.AddColumnsForChange(coll, "Against_Invoice_No", obj.Against_Invoice_No, True)
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
            clsCommon.AddColumnsForChange(coll, "Tax_Calculation_Type", IIf(obj.Tax_Calculation_Type = EnumTaxCalucationType.Automatic, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Inv_Date", clsCommon.GetPrintDate(obj.Inv_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code, True)
            clsCommon.AddColumnsForChange(coll, "Salesman_Name", obj.Salesman_Name)

            '' currencyconversion
            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", obj.CURRENCY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ConvRate", obj.ConvRate)
            clsCommon.AddColumnsForChange(coll, "ApplicableFrom", obj.ApplicableFrom, True)
            '' End currencyconversion

            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Route_Desc", obj.Route_Desc)
            clsCommon.AddColumnsForChange(coll, "TotCashDiscAmt", obj.TotCashDiscAmt)
            clsCommon.AddColumnsForChange(coll, "Invoice_Type", obj.Invoice_Type)
            clsCommon.AddColumnsForChange(coll, "Price_Group_Code", obj.Price_Group_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_PO_No", obj.Cust_PO_No)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_Per", obj.HeadDisc_Per)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_PerAmt", obj.HeadDisc_PerAmt)
            clsCommon.AddColumnsForChange(coll, "HeadDisc_Amt", obj.HeadDisc_Amt)
            clsCommon.AddColumnsForChange(coll, "Return_Type", obj.Return_Type)
            clsCommon.AddColumnsForChange(coll, "Shift_Type", obj.Shift_Type, True)
            clsCommon.AddColumnsForChange(coll, "Damage_Type", obj.Damage_Type)
            clsCommon.AddColumnsForChange(coll, "CrateMan", obj.CrateMan)
            clsCommon.AddColumnsForChange(coll, "jaali", obj.jaali)
            clsCommon.AddColumnsForChange(coll, "Box", obj.Box)
            clsCommon.AddColumnsForChange(coll, "Gate_ReturnNo", obj.Gate_ReturnNo)
            clsCommon.AddColumnsForChange(coll, "Is_Cancelled", obj.Is_Cancelled)
            clsCommon.AddColumnsForChange(coll, "IsSampling", obj.IsSampling)
            'clsCommon.AddColumnsForChange(coll, "Total_Comm_Amt", obj.Total_Comm_Amt)
            clsCommon.AddColumnsForChange(coll, "Distributor_Commission_TotalAmt", obj.Distributor_Commission_TotalAmt)
            clsCommon.AddColumnsForChange(coll, "Transporter_Commission_TotalAmt", obj.Transporter_Commission_TotalAmt)
            clsCommon.AddColumnsForChange(coll, "Security_TotalAmt", obj.Security_TotalAmt)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_type)
                clsCommon.AddColumnsForChange(coll, "Screen_Type", obj.Screen_Type)
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_RETURN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_RETURN_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsShipmentReturnDetail.SaveData(obj.Document_Code, obj.Document_Date, obj.Arr, obj.Trans_type, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_SD_SHIPMENT_RETURN_HEAD", "Document_Code", "TSPL_SD_SHIPMENT_RETURN_DETAIL", "Document_Code", trans)


            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Document_Code, obj.arrCustomFields, trans)
            '''' to save item weight unit
            qry = "update TSPL_SD_SALE_RETURN_DETAIL set Weight_UOM= (select Weight_UOM from TSPL_ITEM_MASTER where Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code)  where Document_Code='" + obj.Document_Code + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '''' 


            'isSaved = isSaved AndAlso clsApprovalScreen.SaveApprovalAtTransLevel(obj.Form_ID, "Document_Code", obj.Document_Code, "TSPL_SD_SALE_RETURN_HEAD", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsShipmentReturnHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsShipmentReturnHead
        Dim obj As clsShipmentReturnHead = Nothing
        Dim Trans_type_Str As String = ""
        Dim Trans_type As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Trans_Type from TSPL_SD_SHIPMENT_RETURN_HEAD where document_code='" + strPONo + "'", trans))
        Dim qry As String = "SELECT TSPL_SD_SHIPMENT_RETURN_HEAD.Sub_Location_code,TSPL_SD_SHIPMENT_RETURN_HEAD.RoundOffAmount,TSPL_SD_SHIPMENT_RETURN_HEAD.TotalCAN,TSPL_SD_SHIPMENT_RETURN_HEAD.ShippedCAN, TSPL_SD_SHIPMENT_RETURN_HEAD.CrateQty,TSPL_SD_SHIPMENT_RETURN_HEAD.Is_Taxable,TSPL_SD_SHIPMENT_RETURN_HEAD.Gate_ReturnNo,TSPL_SD_SHIPMENT_RETURN_HEAD.CrateMan,TSPL_SD_SHIPMENT_RETURN_HEAD.Jaali,TSPL_SD_SHIPMENT_RETURN_HEAD.Box , TSPL_SD_SHIPMENT_RETURN_HEAD.Trans_Type,TSPL_SD_SHIPMENT_RETURN_HEAD.Return_Type,TSPL_SD_SHIPMENT_RETURN_HEAD.Shift_Type,TSPL_SD_SHIPMENT_RETURN_HEAD.Damage_Type,TSPL_SD_SHIPMENT_RETURN_HEAD.HeadDisc_PerAmt,TSPL_SD_SHIPMENT_RETURN_HEAD.Cust_PO_No,TSPL_SD_SHIPMENT_RETURN_HEAD.VehicleNo, TSPL_SD_SHIPMENT_RETURN_HEAD.Vehicle_Code,TSPL_SD_SHIPMENT_RETURN_HEAD.price_group_code , " &
        "TSPL_SD_SHIPMENT_RETURN_HEAD.Invoice_Type,TSPL_SD_SHIPMENT_RETURN_HEAD.HeadDisc_Per,TSPL_SD_SHIPMENT_RETURN_HEAD.HeadDisc_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TotCashDiscAmt,TSPL_SD_SHIPMENT_RETURN_HEAD.Route_No,TSPL_SD_SHIPMENT_RETURN_HEAD.Route_Desc,TSPL_SD_SHIPMENT_RETURN_HEAD.Price_Code, TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Code,TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Date,TSPL_SD_SHIPMENT_RETURN_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SHIPMENT_RETURN_HEAD.Status,TSPL_SD_SHIPMENT_RETURN_HEAD.On_Hold,TSPL_SD_SHIPMENT_RETURN_HEAD.Ref_No,TSPL_SD_SHIPMENT_RETURN_HEAD.Description,TSPL_SD_SHIPMENT_RETURN_HEAD.Remarks,TSPL_SD_SHIPMENT_RETURN_HEAD.Tax_Group,TSPL_SD_SHIPMENT_RETURN_HEAD.Bill_To_Location,TSPL_SD_SHIPMENT_RETURN_HEAD.Ship_To_Location,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX1,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX1_Rate,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX1_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX1_Base_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX2,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX2_Rate,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX2_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX2_Base_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX3,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX3_Rate,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX3_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX3_Base_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX4,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX4_Rate,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX4_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX4_Base_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX5,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX5_Rate,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX5_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX5_Base_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX6,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX6_Rate,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX6_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX6_Base_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX7,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX7_Rate,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX7_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX7_Base_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX8,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX8_Rate,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX8_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX8_Base_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX9,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX9_Rate,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX9_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX9_Base_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX10,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX10_Rate,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX10_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.TAX10_Base_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.Discount_Base,TSPL_SD_SHIPMENT_RETURN_HEAD.Discount_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.Amount_Less_Discount,TSPL_SD_SHIPMENT_RETURN_HEAD.Total_Tax_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.Comments,TSPL_SD_SHIPMENT_RETURN_HEAD.Comp_Code,TSPL_SD_SHIPMENT_RETURN_HEAD.Terms_Code,TSPL_SD_SHIPMENT_RETURN_HEAD.Due_Date ,TSPL_LOCATION_MASTER.Location_Desc as BillToLocationName,TSPL_SHIP_TO_LOCATION.Ship_To_Desc as ShipToLocationName,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc as TaxGroupName,TSPL_TERMS_MASTER.Terms_Desc as TermsName,TSPL_SD_SHIPMENT_RETURN_HEAD.Posting_Date,TSPL_SD_SHIPMENT_RETURN_HEAD.Total_Amt,TSPL_SD_SHIPMENT_RETURN_HEAD.Carrier,TSPL_SD_SHIPMENT_RETURN_HEAD.VehicleNo,TSPL_SD_SHIPMENT_RETURN_HEAD.GRNo,TSPL_SD_SHIPMENT_RETURN_HEAD.GENo,TSPL_SD_SHIPMENT_RETURN_HEAD.GEDate, TSPL_SD_SHIPMENT_RETURN_HEAD.Dept,TSPL_SD_SHIPMENT_RETURN_HEAD.Dept_Desc,TSPL_SD_SHIPMENT_RETURN_HEAD.Item_Type,TSPL_SD_SHIPMENT_RETURN_HEAD.Against_Invoice_No ,TSPL_SD_SHIPMENT_RETURN_HEAD.Against_Invoice_No,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Code1,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Name1,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Amt1,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Code2,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Name2,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Amt2,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Code3,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Name3,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Amt3,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Code4,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Name4,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Amt4,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Code5,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Name5,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Amt5,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Code6,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Name6,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Amt6,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Code7,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Name7,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Amt7,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Code8,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Name8,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Amt8,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Code9 ,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Name9,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Amt9 ,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Code10 ,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Name10,TSPL_SD_SHIPMENT_RETURN_HEAD.Add_Charge_Amt10,TSPL_SD_SHIPMENT_RETURN_HEAD.Total_Add_Charge,TSPL_SD_SHIPMENT_RETURN_HEAD.Tax_Calculation_Type,TSPL_SD_SHIPMENT_RETURN_HEAD.Challan_No, TSPL_SD_SHIPMENT_RETURN_HEAD.Challan_Date, TSPL_SD_SHIPMENT_RETURN_HEAD.Inv_Date,TSPL_SD_SHIPMENT_RETURN_HEAD.Inv_No,TSPL_SD_SHIPMENT_RETURN_HEAD.Is_Internal ,TSPL_SD_SHIPMENT_RETURN_HEAD.Salesman_Code ,TSPL_SD_SHIPMENT_RETURN_HEAD.Salesman_Name,"
        qry += " TSPL_SD_SHIPMENT_RETURN_HEAD.CURRENCY_CODE,TSPL_SD_SHIPMENT_RETURN_HEAD.CONVRATE,TSPL_SD_SHIPMENT_RETURN_HEAD.APPLICABLEFROM,TSPL_SD_SHIPMENT_RETURN_HEAD.PROJECT_ID ,TSPL_SD_SHIPMENT_RETURN_HEAD.Is_Cancelled "
        qry += " FROM TSPL_SD_SHIPMENT_RETURN_HEAD"
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_RETURN_HEAD.Bill_To_Location "
        qry += " left outer join TSPL_SHIP_TO_LOCATION on TSPL_SHIP_TO_LOCATION.Ship_To_Code=TSPL_SD_SHIPMENT_RETURN_HEAD.Ship_To_Location "
        qry += " left outer join  TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code= TSPL_SD_SHIPMENT_RETURN_HEAD.Tax_Group "
        qry += " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_SD_SHIPMENT_RETURN_HEAD.Terms_Code "
        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SHIPMENT_RETURN_HEAD.Customer_Code where TSPL_SD_SHIPMENT_RETURN_HEAD.Trans_Type='" + Trans_type + "' and TSPL_SD_SHIPMENT_RETURN_HEAD.Screen_Type='DS' "
        Dim whrCls As String = ""

        '-------richa 12/08/2014 Ticket No. BM00000003242---------
        Dim strwherecls As String = ""
        'If clsCommon.CompairString(clsCommon.myCstr(NavType).ToUpper(), "CURRENT") <> CompairStringResult.Equal Then
        '    strwherecls = FrmMainTranScreen.CustomerPermission()
        'End If


        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 And clsCommon.myLen(strwherecls) > 0 Then
            whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ") and TSPL_SD_SHIPMENT_RETURN_HEAD.Customer_Code in (" + strwherecls + ") "
        ElseIf clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Bill_To_Location in (" + objCommonVar.strCurrUserLocations + ")"
        ElseIf clsCommon.myLen(strwherecls) > 0 Then
            whrCls = " AND TSPL_SD_SHIPMENT_RETURN_HEAD.Customer_Code in (" + strwherecls + ")"
        End If
        '-----------------------------------------------------
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_SD_SHIPMENT_RETURN_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SHIPMENT_RETURN_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Code = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Code = (select Min(Document_Code) from TSPL_SD_SHIPMENT_RETURN_HEAD where Document_Code>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SD_SHIPMENT_RETURN_HEAD.Document_Code = (select Max(Document_Code) from TSPL_SD_SHIPMENT_RETURN_HEAD where Document_Code<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsShipmentReturnHead()
            obj.TotalCAN = clsCommon.myCdbl(dt.Rows(0)("TotalCAN"))
            obj.ShippedCAN = clsCommon.myCdbl(dt.Rows(0)("ShippedCAN"))
            obj.CrateQty = clsCommon.myCdbl(dt.Rows(0)("CrateQty"))
            obj.Is_Cancelled = clsCommon.myCdbl(dt.Rows(0)("Is_Cancelled"))
            obj.Is_Taxable = clsCommon.myCdbl(dt.Rows(0)("Is_Taxable"))
            obj.Gate_ReturnNo = clsCommon.myCstr(dt.Rows(0)("Gate_ReturnNo"))
            obj.CrateMan = clsCommon.myCdbl(dt.Rows(0)("CrateMan"))
            obj.jaali = clsCommon.myCdbl(dt.Rows(0)("jaali"))
            obj.Box = clsCommon.myCdbl(dt.Rows(0)("Box"))

            obj.Trans_type = clsCommon.myCstr(dt.Rows(0)("Trans_type"))
            Trans_type_Str = clsCommon.myCstr(dt.Rows(0)("Trans_type")) + "-SR"
            obj.Invoice_Type = clsCommon.myCstr(dt.Rows(0)("Invoice_Type"))
            obj.Cust_PO_No = clsCommon.myCstr(dt.Rows(0)("Cust_PO_No"))
            obj.Price_Group_Code = clsCommon.myCstr(dt.Rows(0)("Price_Group_Code"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Sub_Location_code = clsCommon.myCstr(dt.Rows(0)("Sub_Location_code"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
            obj.Is_Internal = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Internal")) = 1, True, False)
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
            obj.RoundOffAmount = clsCommon.myCdbl(dt.Rows(0)("RoundOffAmount"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Due_Date = clsCommon.myCstr(dt.Rows(0)("Due_Date"))
            obj.BillToLocationName = clsCommon.myCstr(dt.Rows(0)("BillToLocationName"))
            obj.ShipToLocationName = clsCommon.myCstr(dt.Rows(0)("ShipToLocationName"))
            obj.TaxGroupName = clsCommon.myCstr(dt.Rows(0)("TaxGroupName"))
            obj.TermsName = clsCommon.myCstr(dt.Rows(0)("TermsName"))
            obj.PROJECT_ID = clsCommon.myCstr(dt.Rows(0)("PROJECT_ID"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If


            obj.Challan_No = clsCommon.myCdbl(dt.Rows(0)("Challan_No"))
            obj.Carrier = clsCommon.myCstr(dt.Rows(0)("Carrier"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.VehicleNo = clsCommon.myCstr(dt.Rows(0)("VehicleNo"))
            obj.GRNo = clsCommon.myCstr(dt.Rows(0)("GRNo"))
            obj.GENo = clsCommon.myCstr(dt.Rows(0)("GENo"))
            If dt.Rows(0)("GEDate") IsNot DBNull.Value Then
                obj.GEDate = clsCommon.myCDate(dt.Rows(0)("GEDate"))
            End If




            obj.Dept = clsCommon.myCstr(dt.Rows(0)("Dept"))
            obj.Dept_Desc = clsCommon.myCstr(dt.Rows(0)("Dept_Desc"))
            obj.Item_Type = clsCommon.myCstr(dt.Rows(0)("Item_Type"))

            obj.Against_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Against_Invoice_No"))


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
            obj.Inv_No = clsCommon.myCstr(dt.Rows(0)("Inv_No"))
            If clsCommon.myLen((dt.Rows(0)("Challan_Date"))) <= 0 Then
                obj.Challan_Date = ""
            Else
                obj.Challan_Date = clsCommon.GetPrintDate((dt.Rows(0)("Challan_Date")), "dd/MMM/yyyy")
            End If

            If clsCommon.myLen((dt.Rows(0)("Inv_Date"))) <= 0 Then
                obj.Inv_Date = ""
            Else
                obj.Inv_Date = clsCommon.GetPrintDate((dt.Rows(0)("Inv_Date")), "dd/MMM/yyyy")
            End If

            obj.Tax_Calculation_Type = IIf(clsCommon.myCdbl(dt.Rows(0)("Tax_Calculation_Type")) = 0, EnumTaxCalucationType.Automatic, EnumTaxCalucationType.Mannual)

            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Salesman_Name = clsCommon.myCstr(dt.Rows(0)("Salesman_Name"))
            obj.HeadDisc_Per = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Per"))
            obj.HeadDisc_PerAmt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_PerAmt"))
            obj.HeadDisc_Amt = clsCommon.myCdbl(dt.Rows(0)("HeadDisc_Amt"))
            obj.Return_Type = clsCommon.myCstr(dt.Rows(0)("Return_Type"))
            obj.Shift_Type = clsCommon.myCstr(dt.Rows(0)("Shift_Type"))
            obj.Damage_Type = clsCommon.myCstr(dt.Rows(0)("Damage_Type"))

            '' CURRENCYCONVERSION 
            obj.CURRENCY_CODE = clsCommon.myCstr(dt.Rows(0)("CURRENCY_CODE"))
            obj.ConvRate = clsCommon.myCdbl(dt.Rows(0)("ConvRate"))
            If IsDBNull(dt.Rows(0)("ApplicableFrom")) = True Then
                obj.ApplicableFrom = Nothing
            Else
                obj.ApplicableFrom = clsCommon.GetPrintDate(dt.Rows(0)("ApplicableFrom"), "dd/MMM/yyyy")
            End If
            '' END CURRENCYCONVERSION 

            qry = "SELECT isnull(TSPL_SD_SHIPMENT_RETURN_DETAIL.Delivery_Code,'') as Delivery_Code,TSPL_SD_SHIPMENT_RETURN_DETAIL.CAN,  TSPL_SD_SHIPMENT_RETURN_DETAIL.Crate,TSPL_SD_SHIPMENT_RETURN_DETAIL.InvoiceCashScheme_Code,  TSPL_SD_SHIPMENT_RETURN_DETAIL.ItemwiseTaxCode,TSPL_SD_SHIPMENT_RETURN_DETAIL.Alter_UnitQty,TSPL_SD_SHIPMENT_RETURN_DETAIL.Rate_UnitQty,TSPL_SD_SHIPMENT_RETURN_DETAIL.InvoiceScheme_Code,TSPL_SD_SHIPMENT_RETURN_DETAIL.Cash_Scheme_Amount,TSPL_SD_SHIPMENT_RETURN_DETAIL.Cash_Scheme_Type,TSPL_SD_SHIPMENT_RETURN_DETAIL.Cash_Scheme_Pers,TSPL_SD_SHIPMENT_RETURN_DETAIL.Cash_Scheme_Code,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX_PAID ,TSPL_SD_SHIPMENT_RETURN_DETAIL.Item_Group, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Scheme_Item_UOM,TSPL_SD_SHIPMENT_RETURN_DETAIL.Scheme_Qty,TSPL_SD_SHIPMENT_RETURN_DETAIL.Scheme_Item_Code,TSPL_SD_SHIPMENT_RETURN_DETAIL.Scheme_Type,TSPL_SD_SHIPMENT_RETURN_DETAIL.Alternate_UOM,TSPL_SD_SHIPMENT_RETURN_DETAIL.RATE_UOM , TSPL_SD_SHIPMENT_RETURN_DETAIL.OrgUnit_code,TSPL_SD_SHIPMENT_RETURN_DETAIL.Is_Mannual_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.Document_Code,TSPL_SD_SHIPMENT_RETURN_DETAIL.Line_No, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Status,TSPL_SD_SHIPMENT_RETURN_DETAIL.Row_Type,TSPL_SD_SHIPMENT_RETURN_DETAIL.status,TSPL_SD_SHIPMENT_RETURN_DETAIL.Item_Code, " &
            "TSPL_ITEM_MASTER.Item_Desc,TSPL_SD_SHIPMENT_RETURN_DETAIL.Qty,TSPL_SD_SHIPMENT_RETURN_DETAIL.Free_Qty,TSPL_SD_SHIPMENT_RETURN_DETAIL.SHIPMENT_Code, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Shipment_Code,TSPL_SD_SHIPMENT_RETURN_DETAIL.Balance_Qty,TSPL_SD_SHIPMENT_RETURN_DETAIL.Unit_code, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Location,TSPL_SD_SHIPMENT_RETURN_DETAIL.Item_Cost,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX1,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX1_Rate, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX1_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX2,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX2_Rate,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX2_Amt, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX3,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX3_Rate,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX3_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX4, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX4_Rate,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX4_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX5,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX5_Rate, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX5_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX6,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX6_Rate,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX6_Amt, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX7,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX7_Rate,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX7_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX8, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX8_Rate,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX8_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX9,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX9_Rate, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX9_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX10,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX10_Rate,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX10_Amt, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Amount,TSPL_SD_SHIPMENT_RETURN_DETAIL.Disc_Per,TSPL_SD_SHIPMENT_RETURN_DETAIL.Disc_Amt, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Amt_Less_Discount,TSPL_SD_SHIPMENT_RETURN_DETAIL.Total_Tax_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.Item_Net_Amt, " &
            "TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX1_Base_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX2_Base_Amt, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX3_Base_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX4_Base_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX5_Base_Amt, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX6_Base_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX7_Base_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX8_Base_Amt, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX9_Base_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.TAX10_Base_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.MRP , " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Batch_No,TSPL_SD_SHIPMENT_RETURN_DETAIL.MFG_Date,TSPL_SD_SHIPMENT_RETURN_DETAIL.Expiry_Date, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Specification,TSPL_SD_SHIPMENT_RETURN_DETAIL.Remarks,TSPL_SD_SHIPMENT_RETURN_DETAIL.Assessable, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.AssessableAmt,TSPL_SD_SHIPMENT_RETURN_DETAIL.DamageQty,TSPL_SD_SHIPMENT_RETURN_DETAIL.Return_Amount,TSPL_SD_SHIPMENT_RETURN_DETAIL.Damage_Amount, " &
             "TSPL_SD_SHIPMENT_RETURN_DETAIL.Scheme_Applicable,TSPL_SD_SHIPMENT_RETURN_DETAIL.Scheme_Code, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Scheme_Item,TSPL_SD_SHIPMENT_RETURN_DETAIL.Item_Tax,TSPL_SD_SHIPMENT_RETURN_DETAIL.Total_MRP_Amt, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Total_Basic_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.Total_Disc_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.Cust_Discount, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Total_Cust_Discount,TSPL_SD_SHIPMENT_RETURN_DETAIL.ActualRate,TSPL_SD_SHIPMENT_RETURN_DETAIL.Cust_DiscountQty, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Price_code,TSPL_SD_SHIPMENT_RETURN_DETAIL.Abatement_Per,TSPL_SD_SHIPMENT_RETURN_DETAIL.Abatement_Amt, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.FOC_Item,TSPL_SD_SHIPMENT_RETURN_DETAIL.Item_Weight,TSPL_SD_SHIPMENT_RETURN_DETAIL.Price_Date, " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.Bin_No,TSPL_SD_SHIPMENT_RETURN_DETAIL.vendor_code,TSPL_SD_SHIPMENT_RETURN_DETAIL.vendor_desc,TSPL_SD_SHIPMENT_RETURN_DETAIL.PrincipleCode,TSPL_SD_SHIPMENT_RETURN_DETAIL.PrincipleDesc,TSPL_SD_SHIPMENT_RETURN_DETAIL.TotalItem_Weight,TSPL_SD_SHIPMENT_RETURN_DETAIL.Conv_Factor,TSPL_SD_SHIPMENT_RETURN_DETAIL.Purchase_Cost,TSPL_SD_SHIPMENT_RETURN_DETAIL.OrgRate,  " &
            "TSPL_SD_SHIPMENT_RETURN_DETAIL.HeadDiscPer,TSPL_SD_SHIPMENT_RETURN_DETAIL.HeadDiscPerAmt,TSPL_SD_SHIPMENT_RETURN_DETAIL.Markup_On,TSPL_SD_SHIPMENT_RETURN_DETAIL.Markup_Percent,TSPL_SD_SHIPMENT_RETURN_DETAIL.Landing_Cost,TSPL_SD_SHIPMENT_RETURN_DETAIL.HeadDiscAmt,TSPL_SD_SHIPMENT_RETURN_DETAIL.CustDiscPer,TSPL_SD_SHIPMENT_RETURN_DETAIL.CasdDiscScheme_Code,TSPL_SD_SHIPMENT_RETURN_DETAIL.ActualUOM,TSPL_SD_SHIPMENT_RETURN_DETAIL.ActualQty,TSPL_SD_SHIPMENT_RETURN_DETAIL.ActualConvAmt,TSPL_SD_SHIPMENT_RETURN_DETAIL.Distributor_Commission_PKID,TSPL_SD_SHIPMENT_RETURN_DETAIL.Distributor_Commission_Rate,TSPL_SD_SHIPMENT_RETURN_DETAIL.Distributor_Commission_RateWithTax,TSPL_SD_SHIPMENT_RETURN_DETAIL.Distributor_Commission_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.Transporter_Commission_Rate,TSPL_SD_SHIPMENT_RETURN_DETAIL.Transporter_Commission_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.Security_Rate,TSPL_SD_SHIPMENT_RETURN_DETAIL.Security_Amt,TSPL_SD_SHIPMENT_RETURN_DETAIL.Transporter,TSPL_SD_SHIPMENT_RETURN_DETAIL.Price_with_tax "
            qry += " FROM TSPL_SD_SHIPMENT_RETURN_DETAIL "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SHIPMENT_RETURN_DETAIL.Location "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SD_SHIPMENT_RETURN_DETAIL.Item_Code"
            qry += " where TSPL_SD_SHIPMENT_RETURN_DETAIL.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_SD_SHIPMENT_RETURN_DETAIL.Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsShipmentReturnDetail)
                Dim objTr As clsShipmentReturnDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsShipmentReturnDetail
                    objTr.Delivery_Code = clsCommon.myCstr(dr("Delivery_Code"))
                    objTr.CAN = clsCommon.myCdbl(dr("CAN"))
                    objTr.Crate = clsCommon.myCdbl(dr("Crate"))
                    objTr.ItemwiseTaxCode = clsCommon.myCstr(dr("ItemwiseTaxCode"))
                    objTr.Alter_UnitQty = clsCommon.myCdbl(dr("Alter_UnitQty"))
                    objTr.Rate_UnitQty = clsCommon.myCdbl(dr("Rate_UnitQty"))
                    objTr.InvoiceCashScheme_Code = clsCommon.myCstr(dr("InvoiceCashScheme_Code"))
                    objTr.InvoiceScheme_Code = clsCommon.myCstr(dr("InvoiceScheme_Code"))
                    objTr.Cash_Scheme_Code = clsCommon.myCstr(dr("Cash_Scheme_Code"))
                    objTr.Cash_Scheme_Type = clsCommon.myCstr(dr("Cash_Scheme_Type"))
                    objTr.Cash_Scheme_Pers = clsCommon.myCdbl(dr("Cash_Scheme_Pers"))
                    objTr.Cash_Scheme_Amount = clsCommon.myCdbl(dr("Cash_Scheme_Amount"))
                    objTr.Scheme_Type = clsCommon.myCstr(dr("Scheme_Type"))
                    objTr.Scheme_Qty = clsCommon.myCdbl(dr("Scheme_Qty"))
                    objTr.Scheme_Item_UOM = clsCommon.myCstr(dr("Scheme_Item_UOM"))
                    objTr.Scheme_Item_Code = clsCommon.myCstr(dr("Scheme_Item_Code"))
                    objTr.Alternate_UOM = clsCommon.myCstr(dr("Alternate_UOM"))
                    objTr.RATE_UOM = clsCommon.myCstr(dr("RATE_UOM"))
                    objTr.Item_Group = clsCommon.myCstr(dr("Item_Group"))
                    objTr.TAX_PAID = clsCommon.myCstr(dr("TAX_PAID"))
                    objTr.Price_with_tax = clsCommon.myCdbl(dr("Price_with_tax"))

                    objTr.OrgUnit_code = clsCommon.myCstr(dr("OrgUnit_code"))
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Status = Convert.ToInt32(clsCommon.myCdbl(dr("Status")))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.DamageQty = clsCommon.myCdbl(dr("DamageQty"))
                    objTr.Return_Amount = clsCommon.myCdbl(dr("Return_Amount"))
                    objTr.Damage_Amount = clsCommon.myCdbl(dr("Damage_Amount"))
                    objTr.Free_Qty = clsCommon.myCdbl(dr("Free_Qty"))
                    objTr.Shipment_Code = clsCommon.myCstr(dr("Shipment_Code"))

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


                    objTr.Is_Mannual_Amt = clsCommon.myCdbl(dr("Is_Mannual_Amt"))

                    ' ''objTr.Landed_Cost_Rate = clsCommon.myCdbl(dr("Landed_Cost_Rate"))
                    ' ''objTr.Landed_Cost_Amount = clsCommon.myCdbl(dr("Landed_Cost_Amount"))

                    objTr.MRP = clsCommon.myCdbl(dr("MRP"))
                    objTr.Assessable = clsCommon.myCdbl(dr("Assessable"))
                    objTr.AssessableAmt = clsCommon.myCdbl(dr("AssessableAmt"))
                    objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    If dr("MFG_Date") IsNot DBNull.Value Then
                        objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    If dr("Expiry_Date") IsNot DBNull.Value Then
                        objTr.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                    End If
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))


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
                    If IsDBNull(dr("Price_Date")).ToString() = "" Then
                        objTr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                    End If


                    objTr.Abatement_Per = clsCommon.myCdbl(dr("Abatement_Per"))
                    objTr.Abatement_Amt = clsCommon.myCdbl(dr("Abatement_Amt"))
                    objTr.FOC_Item = clsCommon.myCdbl(dr("FOC_Item"))
                    objTr.Markup_On = clsCommon.myCstr(dr("Markup_On"))
                    objTr.Markup_Percent = clsCommon.myCdbl(dr("Markup_Percent"))
                    objTr.Landing_Cost = clsCommon.myCdbl(dr("Landing_Cost"))
                    objTr.HeadDiscAmt = clsCommon.myCdbl(dr("HeadDiscAmt"))
                    objTr.CustDiscPer = clsCommon.myCdbl(dr("CustDiscPer"))
                    objTr.CasdDiscScheme_Code = clsCommon.myCstr(dr("CasdDiscScheme_Code"))
                    objTr.Item_Weight = clsCommon.myCdbl(dr("Item_Weight"))
                    objTr.TotalItem_Weight = clsCommon.myCdbl(dr("TotalItem_Weight"))
                    objTr.Conv_Factor = clsCommon.myCdbl(dr("Conv_Factor"))
                    objTr.Purchase_Cost = clsCommon.myCdbl(dr("Purchase_Cost"))
                    objTr.OrgRate = clsCommon.myCdbl(dr("OrgRate"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))
                    objTr.PrincipleCode = clsCommon.myCstr(dr("PrincipleCode"))
                    objTr.PrincipleDesc = clsCommon.myCstr(dr("PrincipleDesc"))
                    objTr.vendor_code = clsCommon.myCstr(dr("vendor_code"))
                    objTr.vendor_desc = clsCommon.myCstr(dr("vendor_desc"))
                    objTr.HeadDiscPer = clsCommon.myCdbl(dr("HeadDiscPer"))
                    objTr.HeadDiscPerAmt = clsCommon.myCdbl(dr("HeadDiscPerAmt"))
                    objTr.ActuaQty = clsCommon.myCdbl(dr("ActualQty"))
                    objTr.ActualUOM = clsCommon.myCstr(dr("ActualUOM"))
                    objTr.ActualReturnQty = clsCommon.myCdbl(dr("ActualConvAmt"))
                    objTr.Distributor_Commission_PKID = clsCommon.myCstr(dr("Distributor_Commission_PKID"))
                    objTr.Distributor_Commission_Rate = clsCommon.myCdbl(dr("Distributor_Commission_Rate"))
                    objTr.Distributor_Commission_RateWithTax = clsCommon.myCdbl(dr("Distributor_Commission_RateWithTax"))
                    objTr.Distributor_Commission_Amt = clsCommon.myCdbl(dr("Distributor_Commission_Amt"))
                    objTr.Transporter_Commission_Rate = clsCommon.myCdbl(dr("Transporter_Commission_Rate"))
                    objTr.Transporter_Commission_Amt = clsCommon.myCdbl(dr("Transporter_Commission_Amt"))
                    objTr.Security_Rate = clsCommon.myCdbl(dr("Security_Rate"))
                    objTr.Security_Amt = clsCommon.myCdbl(dr("Security_Amt"))
                    objTr.Transporter = clsCommon.myCdbl(dr("Transporter"))
                    objTr.arrSrItem = clsSerializeInvenotry.GetData("Sale Return", objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData(Trans_type_Str, objTr.Document_Code, objTr.Item_Code, objTr.Line_No, trans)
                    obj.Arr.Add(objTr)
                Next
            End If

        End If

        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim Trans_type_Str As String = ""
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsShipmentReturnHead = clsShipmentReturnHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Trans_type_Str = clsCommon.myCstr(obj.Trans_type) + "-SR"
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                '' Anubhooti 06-Sep-2014 BM00000003735 (Remarks: Locked Transaction)
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmSaleReturndairy, obj.Bill_To_Location, obj.Document_Date, trans)
                ''
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_SD_SHIPMENT_RETURN_HEAD", "Document_Code", "TSPL_SD_SHIPMENT_RETURN_DETAIL", "Document_Code", trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_INVENTORY_MOVEMENT", "Source_Doc_No", trans)

                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsSerializeInvenotry.DeleteData("Sale Return", strCode, trans)
                clsBatchInventory.DeleteData(Trans_type_Str, strCode, trans)
                Dim qry As String = "delete from TSPL_SD_SHIPMENT_RETURN_DETAIL where Document_Code='" & strCode & "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_SD_SHIPMENT_RETURN_HEAD where Document_Code='" & strCode & "'"
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
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction, Optional ByVal strARNoForRecreate As String = Nothing, Optional ByVal strVoucherForRecreate As String = Nothing, Optional ByVal IsSkipEInvoicePosting As Boolean = False) As Boolean
        Try
            Dim isSaved As Boolean = True
            Dim Trans_type_Str As String = ""
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsShipmentReturnHead = clsShipmentReturnHead.GetData(strDocNo, NavigatorType.Current, trans)
            Trans_type_Str = clsCommon.myCstr(obj.Trans_type) + "-SR"
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleSaleDairy, clsUserMgtCode.frmSaleReturndairy, obj.Bill_To_Location, obj.Document_Date, trans)

            If (obj.Status = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Transaction " + obj.Document_Code + " Is On Hold.Can't Post it")
            End If

            'Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(FormId, "TSPL_SD_SALE_RETURN_HEAD", "Document_Code", obj.Document_Code, trans)
            'If isResult = False Then
            '    trans.Commit()
            '    Return False
            'End If
            Dim qry As String = ""
            UpdateInventoryMovement(obj, trans, False)

            'Dim dblcount As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count (*) as rowno from tspl_sd_sale_return_detail where document_Code ='" & obj.Document_Code & "' and scheme_item='N'", trans))
            'If dblcount > 0 Then
            '    createARInvoice(obj, trans, strARNoForRecreate, strVoucherForRecreate)
            'Else
            '    CreateJournalEntry(obj.Document_Code, trans, "")
            'End If



            'Dim ECustomerType = clsERPFuncationality.GetCustomerEInvoiceTypeFromTransationTable("TSPL_SD_SALE_INVOICE_HEAD", "Document_Code", obj.Against_Invoice_No, trans)

            ''richa agarwal 25 Dec,2020 check eInvoice Implementation
            'If IsSkipEInvoicePosting = False Then
            '    If clsCommon.CompairString(ECustomerType, "BB") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(obj.Is_Taxable), "1") = CompairStringResult.Equal AndAlso clsERPFuncationality.GetEInvoiceStatus(obj.Document_Date, trans) = True Then
            '        If clsDSSalesReturnHead.EInvoice_Implementation(obj.Document_Code, obj.Bill_To_Location, trans) = True Then
            '        Else
            '            Throw New Exception("Invalid JSON Value")
            '        End If
            '    End If
            'End If

            qry = "Update TSPL_SD_SHIPMENT_RETURN_HEAD set Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
            qry += " where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_SD_SHIPMENT_RETURN_HEAD", "Document_Code", "TSPL_SD_SHIPMENT_RETURN_DETAIL", "Document_Code", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function UpdateInventoryMovement(ByVal obj As clsShipmentReturnHead, ByVal trans As SqlTransaction, Optional ByVal UpdateInventory As Boolean = False) As Boolean

        Dim isSaved As Boolean = True
        Dim Trans_type_Str As String = ""
        Trans_type_Str = clsCommon.myCstr(obj.Trans_type) + "-SR"
        If UpdateInventory = True Then
            clsDBFuncationality.ExecuteNonQuery("update tspl_batch_item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & obj.Document_Code & "'", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & obj.Document_Code & "'", trans)
        End If
        Dim qry As String = ""

        Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
        Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)

        Dim strFirstItemCodeNonItemRowType As String = GetFirstItemCode(obj.Arr)
        Dim strRgpNo As String = Nothing
        Dim intCounter As Integer = 0
        For Each objTr As clsShipmentReturnDetail In obj.Arr
            intCounter = intCounter + 1
            If clsCommon.CompairString(objTr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Dim strItemType As String = clsItemMaster.GetItemType(objTr.Item_Code, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                Else
                    strItemTypeToSave = strItemType
                    'Throw New Exception("Item Type not found: " + strItemType)
                End If
                Dim objLocationDetails As New clsItemLocationDetails()
                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objTr.Item_Code, objTr.Unit_code, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objTr.Item_Code + " and Uom:'" + objTr.Unit_code)
                End If

                '''' FOR cogs START here

                Dim objInv As clsSNShipmentHead
                Dim arr As New List(Of String)


                Dim dblCogsBasicCost As Double
                objInv = clsSNShipmentHead.GetData(objTr.Shipment_Code, NavigatorType.Current, trans)
                If Not objInv Is Nothing Then
                    For Each objInvDetail As clsSNShipmentDetail In objInv.Arr
                        If objInvDetail.Item_Code = objTr.Item_Code Then
                            dblCogsBasicCost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost " &
                            "when Costing_Method=3 then LIFO_Cost end)/sum(Qty) as COst from TSPL_INVENTORY_MOVEMENT left outer join " &
                            "TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on " &
                            "TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where " &
                            "Source_Doc_No='" & objInvDetail.Document_Code & "' And TSPL_INVENTORY_MOVEMENT.Item_Code='" & objTr.Item_Code & "'  and  TSPL_INVENTORY_MOVEMENT.MRP=" & objTr.MRP & " ", trans))



                        End If
                    Next
                End If
                '''' COGS ENDS HERE


                objLocationDetails.Item_Code = objTr.Item_Code
                objLocationDetails.Item_Desc = objTr.Item_Desc
                objLocationDetails.Location_Code = objTr.Location
                objLocationDetails.Location_Desc = objTr.LocationName
                objLocationDetails.Item_Qty = objTr.Qty
                objLocationDetails.Amount = objTr.Amount
                objLocationDetails.MRP = objTr.MRP * ConvFac
                If objTr.MFG_Date.HasValue Then
                    objLocationDetails.MFG_Date = objTr.MFG_Date
                End If
                objLocationDetails.Batch_No = objTr.Batch_No
                If objTr.Expiry_Date.HasValue Then
                    objLocationDetails.Expiry_Date = objTr.Expiry_Date
                End If
                objLocationDetails.ItemType = strItemTypeToSave
                ArrLocationDetails.Add(objLocationDetails)

                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "I"
                objInventoryMovemnt.Location_Code = IIf(clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0, obj.Sub_Location_code, objTr.Location)

                objInventoryMovemnt.Cust_Code = obj.Customer_Code
                objInventoryMovemnt.Cust_Name = obj.Customer_Name

                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                'objInventoryMovemnt.Qty = objTr.Qty
                objInventoryMovemnt.Qty = objTr.ActuaQty
                objInventoryMovemnt.UOM = IIf(clsCommon.myLen(clsCommon.myCstr(objTr.ActualUOM)) > 0, objTr.ActualUOM, objTr.Unit_code)
                objInventoryMovemnt.Is_Scheme_Item = objTr.Scheme_Item
                ''richa only for scheme item 
                If clsCommon.CompairString(objTr.Scheme_Item, "Y") = CompairStringResult.Equal Then
                    If Not objInv Is Nothing Then
                        For Each objInvDetail As clsSNShipmentDetail In objInv.Arr
                            If objInvDetail.Item_Code = objTr.Item_Code Then
                                If clsCommon.CompairString(objInvDetail.Scheme_Item, "Y") = CompairStringResult.Equal Then
                                    dblCogsBasicCost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(case when Costing_Method=0 then Avg_Cost when Costing_Method=1 then Avg_Cost when Costing_Method=2 then FIFO_Cost " &
                            "when Costing_Method=3 then LIFO_Cost end)/sum(Qty) as COst from TSPL_INVENTORY_MOVEMENT left outer join " &
                            "TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code left outer join TSPL_PURCHASE_ACCOUNTS on " &
                            "TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code where " &
                            "Source_Doc_No='" & objInvDetail.Document_Code & "' And TSPL_INVENTORY_MOVEMENT.Item_Code='" & objTr.Item_Code & "' and TSPL_INVENTORY_MOVEMENT.is_Scheme_item='Y' and  TSPL_INVENTORY_MOVEMENT.MRP='" & objInvDetail.MRP & "' ", trans))



                                End If

                            End If
                        Next
                    End If
                End If

                If clsCommon.CompairString(objTr.Scheme_Item, "Y") = CompairStringResult.Equal Then
                    objInventoryMovemnt.Basic_Cost = dblCogsBasicCost * objTr.ActuaQty
                Else
                    objInventoryMovemnt.Basic_Cost = dblCogsBasicCost * objTr.Qty
                End If

                objInventoryMovemnt.Avg_Cost = objInventoryMovemnt.Basic_Cost
                objInventoryMovemnt.LIFO_Cost = objInventoryMovemnt.Basic_Cost
                objInventoryMovemnt.FIFO_Cost = objInventoryMovemnt.Basic_Cost
                objInventoryMovemnt.CalculateAvgCost = False
                objInventoryMovemnt.MRP = objTr.MRP

                objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
                objInventoryMovemnt.Net_Cost = objTr.Total_Tax_Amt
                'If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                '    objInventoryMovemnt.ItemType = "RM"
                'ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                '    objInventoryMovemnt.ItemType = "OT"
                'ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                '    objInventoryMovemnt.ItemType = "FT"
                'End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                ArrInventoryMovement.Add(objInventoryMovemnt)
            End If
        Next
        isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)
        isSaved = isSaved AndAlso clsInventoryMovement.SaveData(Trans_type_Str, obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

        ' done by priti on BHA/09/05/18-000022
        Dim AllowCrateCanPhysicalStock As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowCratePhysicalStock, clsFixedParameterCode.AllowCratePhysicalStock, trans))
        Dim dblCrateQty As Double = obj.CrateQty
        Dim dblCanQty As Double = obj.ShippedCAN
        If AllowCrateCanPhysicalStock = 1 Then
            If dblCrateQty > 0 Then
                Dim strCrateItem = clsDBFuncationality.getSingleValue("select top 1 Item_Code from TSPL_ITEM_MASTER where isnull(CRATE,0)=1", trans)
                If clsCommon.myLen(strCrateItem) > 0 Then
                    Dim dblRate As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCrateRate, clsFixedParameterCode.ItemCrateRate, trans))
                    Dim strCrateUOM = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & strCrateItem & "' and Default_UOM=1", trans)
                    'Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strCrateItem, obj.Bill_To_Location, obj.Document_Code, obj.Document_Date, trans, strCrateUOM, 0)
                    Dim strItemType As String = clsItemMaster.GetItemType(strCrateItem, trans)
                    Dim strItemTypeToSave As String = ""
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    Else
                        strItemTypeToSave = strItemType
                    End If
                    Dim objInventoryMovemnt As New clsInventoryMovement()
                    Dim ArrInventoryMovementCrate As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = IIf(clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0, obj.Sub_Location_code, obj.Bill_To_Location)

                    objInventoryMovemnt.Cust_Code = obj.Customer_Code
                    objInventoryMovemnt.Cust_Name = obj.Customer_Name

                    objInventoryMovemnt.Item_Code = strCrateItem
                    objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(strCrateItem, trans)
                    objInventoryMovemnt.Qty = obj.CrateQty
                    objInventoryMovemnt.UOM = strCrateUOM
                    objInventoryMovemnt.Basic_Cost = dblRate
                    objInventoryMovemnt.MRP = 0
                    objInventoryMovemnt.Add_Cost = 0
                    objInventoryMovemnt.Net_Cost = 0
                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    ArrInventoryMovementCrate.Add(objInventoryMovemnt)
                    clsInventoryMovement.SaveData(Trans_type_Str, obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementCrate, trans)

                    qry = "Update TSPL_SD_SHIPMENT_RETURN_HEAD set Crate_Item='" & strCrateItem & "',Crate_ItemUnit='" & strCrateUOM & "',Crate_ItemRate='" & dblRate & "' where Document_Code='" & obj.Document_Code & "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Else
                    Throw New Exception("Please Create item as Crate type Item.")
                    isSaved = False
                End If
            End If
            ' For Can

            If dblCanQty > 0 Then
                Dim strCanItem = clsDBFuncationality.getSingleValue("select top 1 Item_Code from TSPL_ITEM_MASTER where isnull(Can,0)=1", trans)
                If clsCommon.myLen(strCanItem) > 0 Then
                    Dim dblRate As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemCanRate, clsFixedParameterCode.ItemCanRate, trans))
                    Dim strCanUOM = clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Item_Code='" & strCanItem & "' and Default_UOM=1", trans)

                    Dim strItemType As String = clsItemMaster.GetItemType(strCanItem, trans)
                    Dim strItemTypeToSave As String = ""
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    Else
                        strItemTypeToSave = strItemType
                    End If
                    Dim objInventoryMovemnt As New clsInventoryMovement()
                    Dim ArrInventoryMovementCan As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = IIf(clsCommon.myLen(clsCommon.myCstr(obj.Sub_Location_code)) > 0, obj.Sub_Location_code, obj.Bill_To_Location)

                    objInventoryMovemnt.Cust_Code = obj.Customer_Code
                    objInventoryMovemnt.Cust_Name = obj.Customer_Name

                    objInventoryMovemnt.Item_Code = strCanItem
                    objInventoryMovemnt.Item_Desc = clsItemMaster.GetItemName(strCanItem, trans)
                    objInventoryMovemnt.Qty = obj.ShippedCAN
                    objInventoryMovemnt.UOM = strCanUOM
                    objInventoryMovemnt.Basic_Cost = dblRate
                    objInventoryMovemnt.MRP = 0
                    objInventoryMovemnt.Add_Cost = 0
                    objInventoryMovemnt.Net_Cost = 0
                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    ArrInventoryMovementCan.Add(objInventoryMovemnt)
                    clsInventoryMovement.SaveData(Trans_type_Str, obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementCan, trans)

                    qry = "Update TSPL_SD_SHIPMENT_RETURN_HEAD set Can_Item='" & strCanItem & "',Can_ItemUnit='" & strCanUOM & "',Can_ItemRate='" & dblRate & "' where Document_Code='" & obj.Document_Code & "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Else
                    Throw New Exception("Please Create item as Can type Item.")
                    isSaved = False
                End If

            End If
        End If
        Return True
    End Function
    Private Shared Function GetFirstItemCode(ByVal Arr As List(Of clsShipmentReturnDetail)) As String
        For Each objtr As clsShipmentReturnDetail In Arr
            If clsCommon.CompairString(objtr.Row_Type, clsItemRowType.RowTypeItem) = CompairStringResult.Equal Then
                Return objtr.Item_Code
            End If
        Next
        Return ""
    End Function


End Class
Public Class clsShipmentReturnDetail
#Region "Variables"
    Public CAN As Double = 0
    Public Crate As Double = 0
    Public ItemwiseTaxCode As String = ""
    Public Alter_UnitQty As Double = 0
    Public Rate_UnitQty As Double = 0
    Public ActuaQty As Double = 0
    Public ActualReturnQty As Double = 0
    Public InvoiceScheme_Code As String = Nothing
    Public InvoiceCashScheme_Code As String = Nothing
    Public Alternate_UOM As String = Nothing
    Public RATE_UOM As String = Nothing
    Public ActualUOM As String = Nothing
    Public Item_Group As String = Nothing
    Public TAX_PAID As String = Nothing
    Public Scheme_Type As String = Nothing
    Public Scheme_Item_Code As String = Nothing
    Public Scheme_Qty As Decimal = Nothing
    Public Scheme_Item_UOM As String = Nothing
    Public Cash_Scheme_Code As String = Nothing
    Public Cash_Scheme_Type As String = Nothing
    Public Cash_Scheme_Pers As Decimal = Nothing
    Public Cash_Scheme_Amount As Decimal = Nothing

    Public OrgUnit_code As String = ""
    Public Document_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing 'Not a Table Field
    Public Qty As Double = 0
    Public Balance_Qty As Double = 0
    Public Free_Qty As Double = 0
    Public Shipment_Code As String = Nothing
    Public Unit_code As String = Nothing '
    Public Location As String = Nothing '
    Public LocationName As String = Nothing 'Not a Table Field
    Public Item_Cost As Double = 0
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
    Public Status As Integer = 0
    Public MRP As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public Assessable As Double = 0
    Public AssessableAmt As Double = 0
    Public Is_Mannual_Amt As Integer = Nothing
    Public SRNTax_Group As String = Nothing 'Not a Table Field

    Public Scheme_Applicable As String = Nothing
    Public Scheme_Code As String = Nothing
    Public Scheme_Item As String = Nothing
    Public Item_Tax As Double = 0
    Public Total_MRP_Amt As Double = 0
    Public Total_Basic_Amt As Double = 0
    Public Total_Disc_Amt As Double = 0
    Public Price_with_tax As Double = 0
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
    Public Price_Date As Date? = Nothing
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
    Public DamageQty As Double = 0
    Public Return_Amount As Double = 0
    Public Damage_Amount As Double = 0
    Public Bin_No As String = Nothing
    Public PrincipleCode As String = Nothing
    Public PrincipleDesc As String = Nothing
    Public vendor_code As String = Nothing
    Public vendor_desc As String = Nothing
    Public HeadDiscPer As Double = 0
    Public HeadDiscPerAmt As Double = 0
    Public Delivery_Code As String = Nothing
    Public Sampling As Integer = 0
    Public Distributor_Commission_PKID As String = ""
    Public Distributor_Commission_Rate As Decimal = 0
    Public Distributor_Commission_RateWithTax As Decimal = 0
    Public Distributor_Commission_Amt As Decimal = 0
    Public Transporter_Commission_Rate As Decimal = 0
    Public Transporter_Commission_Amt As Decimal = 0
    Public Security_Rate As Decimal = 0
    Public Security_Amt As Decimal = 0
    Public Transporter As String = ""
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal dtDocDate As DateTime, ByVal Arr As List(Of clsShipmentReturnDetail), ByVal Trans_Type As String, ByVal trans As SqlTransaction) As Boolean
        Dim Trans_type_Str As String = ""
        Trans_type_Str = clsCommon.myCstr(Trans_Type) + "-SR"
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsShipmentReturnDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Sampling", obj.Sampling)
                clsCommon.AddColumnsForChange(coll, "Delivery_Code", obj.Delivery_Code, True)
                clsCommon.AddColumnsForChange(coll, "CAN", obj.CAN)
                clsCommon.AddColumnsForChange(coll, "Crate", obj.Crate)
                clsCommon.AddColumnsForChange(coll, "ItemwiseTaxCode", obj.ItemwiseTaxCode)
                clsCommon.AddColumnsForChange(coll, "Alter_UnitQty", obj.Alter_UnitQty)
                clsCommon.AddColumnsForChange(coll, "Rate_UnitQty", obj.Rate_UnitQty)
                clsCommon.AddColumnsForChange(coll, "InvoiceScheme_Code", obj.InvoiceScheme_Code)
                clsCommon.AddColumnsForChange(coll, "InvoiceCashScheme_Code", obj.InvoiceCashScheme_Code)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Code", obj.Cash_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Type", obj.Cash_Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Pers", obj.Cash_Scheme_Pers)
                clsCommon.AddColumnsForChange(coll, "Cash_Scheme_Amount", obj.Cash_Scheme_Amount)

                clsCommon.AddColumnsForChange(coll, "Scheme_Item_Code", obj.Scheme_Item_Code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item_UOM", obj.Scheme_Item_UOM)
                clsCommon.AddColumnsForChange(coll, "Scheme_Qty", obj.Scheme_Qty)
                clsCommon.AddColumnsForChange(coll, "Scheme_Type", obj.Scheme_Type)
                clsCommon.AddColumnsForChange(coll, "Alternate_UOM", obj.Alternate_UOM, True)
                clsCommon.AddColumnsForChange(coll, "RATE_UOM", obj.RATE_UOM, True)
                clsCommon.AddColumnsForChange(coll, "Item_Group", obj.Item_Group)
                clsCommon.AddColumnsForChange(coll, "TAX_PAID", obj.TAX_PAID)

                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "OrgUnit_code", obj.OrgUnit_code)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                clsCommon.AddColumnsForChange(coll, "Free_qty", obj.Free_Qty)

                clsCommon.AddColumnsForChange(coll, "Shipment_Code", obj.Shipment_Code, True)

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
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)

                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Is_Mannual_Amt", obj.Is_Mannual_Amt)
                If obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd-MMM-yyyy"))
                End If
                If obj.Expiry_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd-MMM-yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Assessable", obj.Assessable)
                clsCommon.AddColumnsForChange(coll, "AssessableAmt", obj.AssessableAmt)

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

                clsCommon.AddColumnsForChange(coll, "Item_Weight", obj.Item_Weight)
                clsCommon.AddColumnsForChange(coll, "Conv_Factor", obj.Conv_Factor)
                clsCommon.AddColumnsForChange(coll, "TotalItem_Weight", obj.TotalItem_Weight)
                clsCommon.AddColumnsForChange(coll, "Purchase_Cost", obj.Purchase_Cost)
                clsCommon.AddColumnsForChange(coll, "OrgRate", obj.OrgRate)
                clsCommon.AddColumnsForChange(coll, "DamageQty", obj.DamageQty)
                clsCommon.AddColumnsForChange(coll, "Return_Amount", obj.Return_Amount)
                clsCommon.AddColumnsForChange(coll, "Damage_Amount", obj.Damage_Amount)
                clsCommon.AddColumnsForChange(coll, "Bin_No", obj.Bin_No)
                clsCommon.AddColumnsForChange(coll, "PrincipleCode", obj.PrincipleCode)
                clsCommon.AddColumnsForChange(coll, "PrincipleDesc", obj.PrincipleDesc)
                clsCommon.AddColumnsForChange(coll, "vendor_code", obj.vendor_code)
                clsCommon.AddColumnsForChange(coll, "vendor_desc", obj.vendor_desc)
                clsCommon.AddColumnsForChange(coll, "HeadDiscAmt", obj.HeadDiscAmt)
                clsCommon.AddColumnsForChange(coll, "HeadDiscPer", obj.HeadDiscPer)
                clsCommon.AddColumnsForChange(coll, "HeadDiscPerAmt", obj.HeadDiscPerAmt)
                clsCommon.AddColumnsForChange(coll, "ActualUOM", obj.ActualUOM)
                clsCommon.AddColumnsForChange(coll, "ActualQty", obj.ActuaQty)
                clsCommon.AddColumnsForChange(coll, "ActualConvAmt", obj.ActualReturnQty)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_PKID", obj.Distributor_Commission_PKID, True)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_Rate", obj.Distributor_Commission_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_RateWithTax", obj.Distributor_Commission_RateWithTax, True)
                clsCommon.AddColumnsForChange(coll, "Distributor_Commission_Amt", obj.Distributor_Commission_Amt, True)
                clsCommon.AddColumnsForChange(coll, "Transporter_Commission_Rate", obj.Transporter_Commission_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Transporter_Commission_Amt", obj.Transporter_Commission_Amt, True)
                clsCommon.AddColumnsForChange(coll, "Security_Rate", obj.Security_Rate, True)
                clsCommon.AddColumnsForChange(coll, "Security_Amt", obj.Security_Amt, True)
                clsCommon.AddColumnsForChange(coll, "Transporter", obj.Transporter, True)
                clsCommon.AddColumnsForChange(coll, "Price_with_tax", obj.Price_with_tax, True)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SD_SHIPMENT_RETURN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                clsSerializeInvenotry.SaveData("Shipment Return", strDocNo, dtDocDate, "I", obj.Item_Code, obj.Location, obj.Line_No, obj.arrSrItem, trans)
                clsBatchInventory.SaveData(Trans_type_Str, strDocNo, dtDocDate, "I", obj.Item_Code, obj.Location, obj.Line_No, obj.MRP, obj.Unit_code, obj.arrBatchItem, trans)

            Next
        End If
        Return True
    End Function
End Class
