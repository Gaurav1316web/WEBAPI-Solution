'                                            Modified by = Priit (21/09/2012)
Imports common
Imports System.Data.SqlClient
Public Class clsSaleHead
#Region "Variables"
    Public Sale_Invoice_ID As Integer = Nothing
    Public Sale_Invoice_No As String = Nothing
    Public Sale_Invoice_Date As Date = Nothing
    Public Shipment_No As String = Nothing
    Public Shipment_Date As Date = Nothing
    Public Order_No As String = Nothing
    Public Order_Date As Date = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Cust_PONo As String = Nothing
    Public Expected_Ship_Date As Date = Nothing
    Public Status As String = Nothing
    Public On_Hold As String = Nothing
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Price_Code As String = Nothing
    Public Tax_Group As String = Nothing
    Public Location As String = Nothing
    Public Cust_Account As String = Nothing
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = Nothing
    Public TAX1_Assessable_Amt As Double = Nothing
    Public TAX1_Amt As Double = Nothing
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = Nothing
    Public TAX2_Assessable_Amt As Double = Nothing
    Public TAX2_Amt As Double = Nothing
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = Nothing
    Public TAX3_Assessable_Amt As Double = Nothing
    Public TAX3_Amt As Double = Nothing
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = Nothing
    Public TAX4_Assessable_Amt As Double = Nothing
    Public TAX4_Amt As Double = Nothing
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = Nothing
    Public TAX5_Assessable_Amt As Double = Nothing
    Public TAX5_Amt As Double = Nothing
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = Nothing
    Public TAX6_Assessable_Amt As Double = Nothing
    Public TAX6_Amt As Double = Nothing
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = Nothing
    Public TAX7_Assessable_Amt As Double = Nothing
    Public TAX7_Amt As Double = Nothing
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = Nothing
    Public TAX8_Assessable_Amt As Double = Nothing
    Public TAX8_Amt As Double = Nothing
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = Nothing
    Public TAX9_Assessable_Amt As Double = Nothing
    Public TAX9_Amt As Double = Nothing
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = Nothing
    Public TAX10_Assessable_Amt As Double = Nothing
    Public TAX10_Amt As Double = Nothing
    Public Total_Assessable_Amount As Double = Nothing
    Public Inv_Disc_Percent As Double = Nothing
    Public Inv_Discount_Amt As Double = Nothing
    Public Inv_Detail_Disc_Amt As Double = Nothing
    Public Inv_Detail_Total_Amt As Double = Nothing
    Public Inv_Tax_Amt As Double = Nothing
    Public Freight_Amt As Double = Nothing
    Public Other_Charges As Double = Nothing
    Public Add_Charges As Double = Nothing
    Public Total_Invoice_Amt As Double = Nothing
    Public Balance_Amt As Double = Nothing
    Public Salesman_Code As String = Nothing
    Public Mode_Of_Transport As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Vehicle_No As String = Nothing
    Public KM_Reading As Integer = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public Scheme_Sample_Code As String = Nothing
    Public Price_Date As Date = Nothing
    Public Terms_Code As String = Nothing
    Public Comments As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date = Nothing
    Public Level1_User_code As String = Nothing
    Public Level2_User_code As String = Nothing
    Public Level3_User_code As String = Nothing
    Public Level4_User_code As String = Nothing
    Public Level5_User_code As String = Nothing
    Public Comp_Code As String = Nothing
    Public Is_Post As String = Nothing
    Public TPT As Double = Nothing
    Public Empty_Value As Double = Nothing
    Public Trans_Type As String = Nothing
    Public Trans_Type_Code As String = Nothing
    Public Level1_User_Commission As Double = Nothing
    Public Level2_User_Commission As Double = Nothing
    Public Level3_User_Commission As Double = Nothing
    Public Level4_User_Commission As Double = Nothing
    Public Level5_User_Commission As Double = Nothing
    Public Due_Date As Date = Nothing
    Public Shell_Qty As Double = Nothing
    Public Customer_Invoice_No As String = Nothing
    Public Invoice_Type As String = Nothing
    Public Date_Time_Removal As DateTime = Nothing
    Public Credit_Invoice As String = Nothing

    Public Total_Disc_Percent As Double = Nothing
    Public Tax_Recoverable_Amt As Double = Nothing
    Public Tax_Recoverable_Amt2 As Double = Nothing
    Public Tax_Recoverable_Amt3 As Double = Nothing
    Public Is_Create_Empty As Boolean = False
    Public Discount_On As Integer = 0


    Public Arr As List(Of clsSaleDetail) = Nothing
#End Region


    Public Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As clsSaleHead
        Dim obj As clsSaleHead = Nothing
        Dim qry As String = "select * from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsSaleHead()
            obj.Sale_Invoice_ID = clsCommon.myCdbl(dt.Rows(0)("Sale_Invoice_ID"))
            obj.Sale_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Sale_Invoice_No"))
            obj.Sale_Invoice_Date = clsCommon.myCDate(dt.Rows(0)("Sale_Invoice_Date"))
            obj.Shipment_No = clsCommon.myCstr(dt.Rows(0)("Shipment_No"))
            obj.Shipment_Date = clsCommon.myCDate(dt.Rows(0)("Shipment_Date"))
            obj.Order_No = clsCommon.myCstr(dt.Rows(0)("Order_No"))
            obj.Order_Date = clsCommon.myCDate(dt.Rows(0)("Order_Date"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
            obj.Cust_PONo = clsCommon.myCstr(dt.Rows(0)("Cust_PONo"))
            obj.Expected_Ship_Date = clsCommon.myCDate(dt.Rows(0)("Expected_Ship_Date"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.On_Hold = clsCommon.myCstr(dt.Rows(0)("On_Hold"))
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
            obj.Tax_Group = clsCommon.myCstr(dt.Rows(0)("Tax_Group"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))
            obj.Cust_Account = clsCommon.myCstr(dt.Rows(0)("Cust_Account"))
            obj.TAX1 = clsCommon.myCstr(dt.Rows(0)("TAX1"))
            obj.TAX1_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX1_Rate"))
            obj.TAX1_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Assessable_Amt"))
            obj.TAX1_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX1_Amt"))
            obj.TAX2 = clsCommon.myCstr(dt.Rows(0)("TAX2"))
            obj.TAX2_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX2_Rate"))
            obj.TAX2_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Assessable_Amt"))
            obj.TAX2_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX2_Amt"))
            obj.TAX3 = clsCommon.myCstr(dt.Rows(0)("TAX3"))
            obj.TAX3_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX3_Rate"))
            obj.TAX3_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Assessable_Amt"))
            obj.TAX3_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX3_Amt"))
            obj.TAX4 = clsCommon.myCstr(dt.Rows(0)("TAX4"))
            obj.TAX4_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX4_Rate"))
            obj.TAX4_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Assessable_Amt"))
            obj.TAX4_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX4_Amt"))
            obj.TAX5 = clsCommon.myCstr(dt.Rows(0)("TAX5"))
            obj.TAX5_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX5_Rate"))
            obj.TAX5_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Assessable_Amt"))
            obj.TAX5_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX5_Amt"))
            obj.TAX6 = clsCommon.myCstr(dt.Rows(0)("TAX6"))
            obj.TAX6_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX6_Rate"))
            obj.TAX6_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Assessable_Amt"))
            obj.TAX6_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX6_Amt"))
            obj.TAX7 = clsCommon.myCstr(dt.Rows(0)("TAX7"))
            obj.TAX7_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX7_Rate"))
            obj.TAX7_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Assessable_Amt"))
            obj.TAX7_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX7_Amt"))
            obj.TAX8 = clsCommon.myCstr(dt.Rows(0)("TAX8"))
            obj.TAX8_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX8_Rate"))
            obj.TAX8_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Assessable_Amt"))
            obj.TAX8_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX8_Amt"))
            obj.TAX9 = clsCommon.myCstr(dt.Rows(0)("TAX9"))
            obj.TAX9_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX9_Rate"))
            obj.TAX9_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Assessable_Amt"))
            obj.TAX9_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX9_Amt"))
            obj.TAX10 = clsCommon.myCstr(dt.Rows(0)("TAX10"))
            obj.TAX10_Rate = clsCommon.myCdbl(dt.Rows(0)("TAX10_Rate"))
            obj.TAX10_Assessable_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Assessable_Amt"))
            obj.TAX10_Amt = clsCommon.myCdbl(dt.Rows(0)("TAX10_Amt"))
            obj.Total_Assessable_Amount = clsCommon.myCdbl(dt.Rows(0)("Total_Assessable_Amount"))
            obj.Inv_Disc_Percent = clsCommon.myCdbl(dt.Rows(0)("Inv_Disc_Percent"))
            obj.Inv_Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Inv_Discount_Amt"))
            obj.Inv_Detail_Disc_Amt = clsCommon.myCdbl(dt.Rows(0)("Inv_Detail_Disc_Amt"))
            obj.Inv_Detail_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Inv_Detail_Total_Amt"))
            obj.Inv_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Inv_Tax_Amt"))
            obj.Freight_Amt = clsCommon.myCdbl(dt.Rows(0)("Freight_Amt"))
            obj.Other_Charges = clsCommon.myCdbl(dt.Rows(0)("Other_Charges"))
            obj.Add_Charges = clsCommon.myCdbl(dt.Rows(0)("Add_Charges"))
            obj.Total_Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Invoice_Amt"))
            obj.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("Balance_Amt"))
            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.KM_Reading = clsCommon.myCdbl(dt.Rows(0)("KM_Reading"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.Scheme_Sample_Code = clsCommon.myCstr(dt.Rows(0)("Scheme_Sample_Code"))
            obj.Price_Date = clsCommon.myCDate(dt.Rows(0)("Price_Date"))
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            obj.Modify_By = clsCommon.myCstr(dt.Rows(0)("Modify_By"))
            obj.Modify_Date = clsCommon.myCDate(dt.Rows(0)("Modify_Date"))
            obj.Level1_User_code = clsCommon.myCstr(dt.Rows(0)("Level1_User_code"))
            obj.Level2_User_code = clsCommon.myCstr(dt.Rows(0)("Level2_User_code"))
            obj.Level3_User_code = clsCommon.myCstr(dt.Rows(0)("Level3_User_code"))
            obj.Level4_User_code = clsCommon.myCstr(dt.Rows(0)("Level4_User_code"))
            obj.Level5_User_code = clsCommon.myCstr(dt.Rows(0)("Level5_User_code"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Is_Post = clsCommon.myCstr(dt.Rows(0)("Is_Post"))
            obj.TPT = clsCommon.myCdbl(dt.Rows(0)("TPT"))
            obj.Empty_Value = clsCommon.myCdbl(dt.Rows(0)("Empty_Value"))
            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
            obj.Trans_Type_Code = clsCommon.myCstr(dt.Rows(0)("Trans_Type_Code"))
            obj.Level1_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level1_User_Commission"))
            obj.Level2_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level2_User_Commission"))
            obj.Level3_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level3_User_Commission"))
            obj.Level4_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level4_User_Commission"))
            obj.Level5_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level5_User_Commission"))
            obj.Due_Date = clsCommon.myCDate(dt.Rows(0)("Due_Date"))
            obj.Shell_Qty = clsCommon.myCdbl(dt.Rows(0)("Shell_Qty"))
            obj.Customer_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Customer_Invoice_No"))
            obj.Invoice_Type = clsCommon.myCstr(dt.Rows(0)("Invoice_Type"))
            obj.Date_Time_Removal = clsCommon.myCDate(dt.Rows(0)("Date_Time_Removal"))
            obj.Credit_Invoice = clsCommon.myCstr(dt.Rows(0)("Credit_Invoice"))

            obj.Total_Disc_Percent = clsCommon.myCdbl(dt.Rows(0)("Total_Disc_Percent"))
            obj.Tax_Recoverable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt"))
            obj.Tax_Recoverable_Amt2 = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt2"))
            obj.Tax_Recoverable_Amt3 = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt3"))

            obj.Is_Create_Empty = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Create_Empty")) = 1, True, False)
            obj.Discount_On = clsCommon.myCdbl(dt.Rows(0)("Discount_On"))
            obj.Arr = New List(Of clsSaleDetail)
            qry = "select * from TSPL_SALE_INVOICE_DETAIL where Sale_Invoice_No='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim objtr As New clsSaleDetail()
                    objtr.Sale_Invoice_Id = clsCommon.myCdbl(dr("Sale_Invoice_Id"))
                    objtr.Sale_Invoice_No = clsCommon.myCstr(dr("Sale_Invoice_No"))
                    objtr.Complete = clsCommon.myCstr(dr("Complete"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objtr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                    objtr.Order_Qty = clsCommon.myCdbl(dr("Order_Qty"))
                    objtr.Shipped_Qty = clsCommon.myCdbl(dr("Shipped_Qty"))
                    objtr.Invoice_Qty = clsCommon.myCdbl(dr("Invoice_Qty"))
                    objtr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objtr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                    objtr.Location = clsCommon.myCstr(dr("Location"))
                    objtr.Price_code = clsCommon.myCstr(dr("Price_code"))
                    objtr.Scheme_Applicable = clsCommon.myCstr(dr("Scheme_Applicable"))
                    objtr.Scheme_Code_Qty = clsCommon.myCstr(dr("Scheme_Code_Qty"))
                    objtr.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                    objtr.Promo_Scheme_Applicable = clsCommon.myCstr(dr("Promo_Scheme_Applicable"))
                    objtr.Promo_Scheme_Code = clsCommon.myCstr(dr("Promo_Scheme_Code"))
                    objtr.Promo_Scheme_Item = clsCommon.myCstr(dr("Promo_Scheme_Item"))
                    objtr.Scheme_Disc_Applicable = clsCommon.myCstr(dr("Scheme_Disc_Applicable"))
                    objtr.Scheme_Code_Cash = clsCommon.myCstr(dr("Scheme_Code_Cash"))
                    objtr.Sampling_Item = clsCommon.myCstr(dr("Sampling_Item"))
                    objtr.MRP_Amt = clsCommon.myCdbl(dr("MRP_Amt"))
                    objtr.Basic_Rate = clsCommon.myCdbl(dr("Basic_Rate"))
                    objtr.Item_Assessable_Rate = clsCommon.myCdbl(dr("Item_Assessable_Rate"))
                    objtr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                    objtr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                    objtr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objtr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objtr.TAX1_Assessable_Amt = clsCommon.myCdbl(dr("TAX1_Assessable_Amt"))
                    objtr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objtr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objtr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objtr.TAX2_Assessable_Amt = clsCommon.myCdbl(dr("TAX2_Assessable_Amt"))
                    objtr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objtr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objtr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objtr.TAX3_Assessable_Amt = clsCommon.myCdbl(dr("TAX3_Assessable_Amt"))
                    objtr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objtr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objtr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objtr.TAX4_Assessable_Amt = clsCommon.myCdbl(dr("TAX4_Assessable_Amt"))
                    objtr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objtr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objtr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objtr.TAX5_Assessable_Amt = clsCommon.myCdbl(dr("TAX5_Assessable_Amt"))
                    objtr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objtr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objtr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objtr.TAX6_Assessable_Amt = clsCommon.myCdbl(dr("TAX6_Assessable_Amt"))
                    objtr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objtr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objtr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objtr.TAX7_Assessable_Amt = clsCommon.myCdbl(dr("TAX7_Assessable_Amt"))
                    objtr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objtr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objtr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objtr.TAX8_Assessable_Amt = clsCommon.myCdbl(dr("TAX8_Assessable_Amt"))
                    objtr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objtr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objtr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objtr.TAX9_Assessable_Amt = clsCommon.myCdbl(dr("TAX9_Assessable_Amt"))
                    objtr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objtr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objtr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objtr.TAX10_Assessable_Amt = clsCommon.myCdbl(dr("TAX10_Assessable_Amt"))
                    objtr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objtr.Item_Tax = clsCommon.myCdbl(dr("Item_Tax"))
                    objtr.Total_Assessable_Amt = clsCommon.myCdbl(dr("Total_Assessable_Amt"))
                    objtr.Total_MRP_Amt = clsCommon.myCdbl(dr("Total_MRP_Amt"))
                    objtr.Total_Basic_Amt = clsCommon.myCdbl(dr("Total_Basic_Amt"))
                    objtr.Total_Disc_Amt = clsCommon.myCdbl(dr("Total_Disc_Amt"))
                    objtr.Total_net_Amt = clsCommon.myCdbl(dr("Total_net_Amt"))
                    objtr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                    objtr.Total_Item_Amt = clsCommon.myCdbl(dr("Total_Item_Amt"))
                    objtr.Empty_Value = clsCommon.myCdbl(dr("Empty_Value"))
                    objtr.TPT = clsCommon.myCdbl(dr("TPT"))
                    objtr.Total_TPT = clsCommon.myCdbl(dr("Total_TPT"))
                    objtr.Empty_Value_Shell = clsCommon.myCdbl(dr("Empty_Value_Shell"))
                    objtr.Empty_Value_Bottle = clsCommon.myCdbl(dr("Empty_Value_Bottle"))
                    objtr.Cust_Discount = clsCommon.myCdbl(dr("Cust_Discount"))
                    objtr.Total_Cust_Discount = clsCommon.myCdbl(dr("Total_Cust_Discount"))
                    objtr.Level1_User_Code = clsCommon.myCstr(dr("Level1_User_Code"))
                    objtr.Level2_User_Code = clsCommon.myCstr(dr("Level2_User_Code"))
                    objtr.Level3_User_Code = clsCommon.myCstr(dr("Level3_User_Code"))
                    objtr.Level4_User_Code = clsCommon.myCstr(dr("Level4_User_Code"))
                    objtr.Level5_User_Code = clsCommon.myCstr(dr("Level5_User_Code"))
                    objtr.Level1_User_Commission = clsCommon.myCdbl(dr("Level1_User_Commission"))
                    objtr.Level2_User_Commission = clsCommon.myCdbl(dr("Level2_User_Commission"))
                    objtr.Level3_User_Commission = clsCommon.myCdbl(dr("Level3_User_Commission"))
                    objtr.Level4_User_Commission = clsCommon.myCdbl(dr("Level4_User_Commission"))
                    objtr.Level5_User_Commission = clsCommon.myCdbl(dr("Level5_User_Commission"))
                    objtr.Level1_User_Comm_Amount = clsCommon.myCdbl(dr("Level1_User_Comm_Amount"))
                    objtr.Level2_User_Comm_Amount = clsCommon.myCdbl(dr("Level2_User_Comm_Amount"))
                    objtr.Level3_User_Comm_Amount = clsCommon.myCdbl(dr("Level3_User_Comm_Amount"))
                    objtr.Level4_User_Comm_Amount = clsCommon.myCdbl(dr("Level4_User_Comm_Amount"))
                    objtr.Level5_User_Comm_Amount = clsCommon.myCdbl(dr("Level5_User_Comm_Amount"))
                    objtr.Main_Item = clsCommon.myCstr(dr("Main_Item"))
                    objtr.Discount_Code = clsCommon.myCstr(dr("Discount_Code"))
                    objtr.Cust_Item_Discount_NoTax = clsCommon.myCdbl(dr("Cust_Item_Discount_NoTax"))
                    objtr.Unit_Cogs = clsCommon.myCdbl(dr("Unit_Cogs"))
                    objtr.Abatement_rate = clsCommon.myCdbl(dr("Abatement_rate"))
                    objtr.Target_Discount_Amt = clsCommon.myCdbl(dr("Target_Discount_Amt"))
                    obj.Arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function createInvoice(ByVal strInvoiceCode As String, ByVal strShipCode As String, ByVal trans As SqlTransaction) As String
        Dim strInvType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Type from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + strInvoiceCode + "'", trans))

        Dim qry As String = "delete from TSPL_SALE_INVOICE_DETAIL where  Sale_Invoice_No='" + strInvoiceCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + strInvoiceCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim obj As clsShipmentMaster = clsShipmentMaster.GetData(strShipCode, trans)
        If obj Is Nothing OrElse clsCommon.myLen(obj.Shipment_No) <= 0 Then
            Throw New Exception("Shipment No not found to Create Invoice")
        End If
        Dim l1User, l2User, l3User, l4User, l5User, Invoiceno As String
        l1User = String.Empty
        l2User = String.Empty
        l3User = String.Empty
        l4User = String.Empty
        l5User = String.Empty
        Invoiceno = String.Empty
        Dim Sql As String = ""
        Dim mrp As Decimal = 0 '' = totalMRP()
        Dim basic As Decimal = 0 ''totalBasicAmt()
        Dim assessible As Decimal = 0 ''totalAssessibleAmt()
        Dim detailDiscount As Decimal = 0 '' totalDiscount()
        Dim shipmentTaxAmt As Decimal = obj.TAX1_Amt + obj.TAX2_Amt + obj.TAX3_Amt + obj.TAX4_Amt + obj.TAX5_Amt + obj.TAX6_Amt + obj.TAX7_Amt + obj.TAX8_Amt + obj.TAX9_Amt + obj.TAX10_Amt
        Dim netAmount As Decimal = 0 ''totalNetAmount()
        Dim shipmentDiscPer As Decimal = 0.0
        Dim shipmentDiscAmt As Decimal = 0.0
        Dim additionalCharges As Decimal = 0.0
        Dim OtherCharges As Decimal = 0.0
        Dim freightCharges As Decimal = 0.0
        Dim emptyValue As Decimal = 0 '' totalEmptyAmount()
        Dim totalTPT As Decimal = 0 '' totalTransport()
        If obj.Shipment_Disc_Percent > 0 Then
            totalTPT = obj.Total_TPT
        End If


        additionalCharges = obj.Add_Charges
        OtherCharges = obj.Other_Charges
        freightCharges = obj.Freight_Amt


        If obj.Shipment_Disc_Percent > 0 Then
            shipmentDiscPer = obj.Shipment_Disc_Percent
            shipmentDiscAmt = obj.Shipment_Discount_Amt
        Else
            shipmentDiscAmt = obj.Tot_Customer_Dis_Amt
        End If
        Dim priceDate As Date = obj.Price_Date

        For Each objtr As clsShipmentDetail In obj.Arr
            If clsCommon.CompairString(clsCommon.myCstr(objtr.Scheme_Item), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(objtr.Promo_Scheme_Item), "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(objtr.Sampling_Item), "N") = CompairStringResult.Equal Then
                mrp += objtr.Total_MRP_Amt
                netAmount += objtr.Total_net_Amt
            End If
            basic += objtr.Total_Basic_Amt
            Sql = "Select Abatement_Rate from TSPL_ITEM_PRICE_MASTER WHERE Item_Code='" + objtr.Item_Code + "' AND Item_Basic_Net='" + clsCommon.myCstr(objtr.MRP_Amt) + "' AND Start_Date='" + Format(CDate(objtr.Price_Date), "MM/dd/yyyy") + "'"
            Dim abatement As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sql, trans))
            assessible += Math.Round(objtr.Total_MRP_Amt * abatement / 100, 2)
            detailDiscount += objtr.Total_Disc_Amt
            emptyValue += Math.Round(objtr.Empty_Value, 2)
            If Not obj.Shipment_Disc_Percent > 0 Then
                totalTPT = objtr.Total_TPT
            End If
        Next


        Sql = "Select Excisable from TSPL_LOCATION_MASTER WHERE Location_Code='" + obj.Location + "' "
        Dim LType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))
        Dim transTypeCode As String = ""
        Dim transType As String = clsDocTransactionType.SaleInvoiceExcise
        Dim dt As DataTable
        If LType = "Y" Or LType = "T" Then
            transType = clsDocTransactionType.SaleInvoiceExcise
        Else
            Sql = "Select State from TSPL_LOCATION_MASTER WHERE Location_Code='" + obj.Location + "'"
            Dim locationState As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))
            Sql = "Select State,Tin_No,Transaction_Type from TSPL_CUSTOMER_MASTER WHERE Cust_Code='" + obj.Cust_Code + "'"
            dt = clsDBFuncationality.GetDataTable(Sql, trans)
            Dim custState As String = dt.Rows(0)(0).ToString()
            Dim tinNo As String = dt.Rows(0)(1).ToString()

            Dim custTransType As String = ""
            If clsCommon.myLen(obj.Transfer_No) > 0 Then
                custTransType = obj.Transaction_Type
            Else
                custTransType = clsCommon.myCstr(dt.Rows(0)("Transaction_Type"))
                If clsCommon.myLen(custTransType) <= 0 Then
                    Throw New Exception("Please set Customer's Transaction Type")
                End If
            End If


            If clsCommon.myLen(obj.Customer_Invoice_No) > 0 Then
                If clsCommon.myLen(tinNo) > 0 Then
                    transType = clsDocTransactionType.RouteSeriesTax
                Else
                    transType = clsDocTransactionType.RouteSeriesRetail
                End If
                ''If clsCommon.CompairString(custTransType, "R") = CompairStringResult.Equal Then
                ''    transType = clsDocTransactionType.RouteSeriesRetail
                ''ElseIf clsCommon.CompairString(custTransType, "T") = CompairStringResult.Equal Then
                ''    transType = clsDocTransactionType.RouteSeriesTax
                ''End If
            Else
                If clsCommon.myLen(tinNo) > 0 Then
                    transType = clsDocTransactionType.SaleInvoiceTax
                Else
                    transType = clsDocTransactionType.SaleInvoiceRetail
                End If
                ''If clsCommon.CompairString(custTransType, "R") = CompairStringResult.Equal Then
                ''    transType = clsDocTransactionType.SaleInvoiceRetail
                ''ElseIf clsCommon.CompairString(custTransType, "T") = CompairStringResult.Equal Then
                ''    transType = clsDocTransactionType.SaleInvoiceTax
                ''End If
            End If






            ''If clsCommon.CompairString(custState, locationState) = CompairStringResult.Equal AndAlso clsCommon.myLen(tinNo) > 0 Then
            ''    transType = clsDocTransactionType.SaleInvoiceTax
            ''Else
            ''    transType = clsDocTransactionType.SaleInvoiceRetail
            ''End If
        End If
        Sql = "SELECT Level1_User_code,Level2_User_code,Level3_User_code,Level4_User_code,Level5_User_code FROM TSPL_SHIPMENT_MASTER  where Shipment_No = '" + strShipCode + "'"
        dt = clsDBFuncationality.GetDataTable(Sql, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            l1User = Convert.ToString(dt.Rows(0)(0))
            l2User = Convert.ToString(dt.Rows(0)(1))
            l3User = Convert.ToString(dt.Rows(0)(2))
            l4User = Convert.ToString(dt.Rows(0)(3))
            l5User = Convert.ToString(dt.Rows(0)(4))
        End If
        Dim invNo As String = ""
        If clsCommon.myLen(strInvoiceCode) > 0 Then
            invNo = strInvoiceCode
        Else
            invNo = clsERPFuncationality.GetNextCode(trans, obj.Shipment_Date, clsDocType.SaleInvoice, transType, obj.Location)
            If clsCommon.CompairString(obj.Shipment_Type, "Transfer") = CompairStringResult.Equal Then
                invNo = invNo + "/" + obj.Customer_Invoice_No
            End If
        End If
        Invoiceno = invNo


        Dim due As Date = obj.Shipment_Date.AddDays(10)
        Dim duedate As String = Format(due, "yyyy-MM-dd")

        ''Dim strcreditinvoice As String = String.Empty
        ''If obj.chlcreditinvoice.Checked = True Then
        ''    strcreditinvoice = "Y"
        ''Else
        ''    strcreditinvoice = "N"
        ''End If
        Dim dblTaxRecAmt As Double = 0
        Dim dblTaxRecAmt2 As Double = 0
        Dim dblTaxRecAmt3 As Double = 0
        Dim dblTotDisPer As Double = GetRecoverableAmt(obj, dblTaxRecAmt, dblTaxRecAmt2, dblTaxRecAmt3)
        Dim dtRemoTime As DateTime = New DateTime(obj.Shipment_Date.Year, obj.Shipment_Date.Month, obj.Shipment_Date.Day, obj.Date_Time_Removal.Hour, obj.Date_Time_Removal.Minute, obj.Date_Time_Removal.Second)

        clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_SALE_INVOICE_HEAD_INSERT", New SqlParameter("@Sale_Invoice_No", invNo), New SqlParameter("@shellqty", clsCommon.myCdbl(obj.Shell_Qty)), New SqlParameter("@removaltime", dtRemoTime), New SqlParameter("@creditinvoice", "N"), New SqlParameter("@Sale_Invoice_Date", obj.Shipment_Date), New SqlParameter("@Shipment_No", obj.Shipment_No), New SqlParameter("@Shipment_Date", obj.Shipment_Date), New SqlParameter("@Order_No", obj.Order_No), New SqlParameter("@Order_Date", obj.Order_Date), New SqlParameter("@Expected_Ship_Date", obj.Expected_Ship_Date), New SqlParameter("@Cust_Code", obj.Cust_Code), New SqlParameter("@Cust_Name", obj.Cust_Name), New SqlParameter("@Cust_PONo", obj.Cust_PONo), New SqlParameter("@Status", "Open"), New SqlParameter("@On_Hold", obj.On_Hold), New SqlParameter("@Ref_No", obj.Ref_No), New SqlParameter("@Description", obj.Description), New SqlParameter("@Remarks", obj.Remarks), New SqlParameter("@Price_Code", obj.Price_Code), New SqlParameter("@Tax_Group", obj.Tax_Group), New SqlParameter("@Location", obj.Location), New SqlParameter("@Cust_Account", obj.Cust_Account), New SqlParameter("@Total_Assessable_Amount", assessible), New SqlParameter("@Inv_Disc_Percent", shipmentDiscPer), New SqlParameter("@Inv_Discount_Amt", shipmentDiscAmt), New SqlParameter("@Inv_Detail_Disc_Amt", detailDiscount), New SqlParameter("@Inv_Detail_Total_Amt", clsCommon.myCdbl(obj.Shipment_Detail_Total_Amt)), New SqlParameter("@Inv_Tax_Amt", shipmentTaxAmt), New SqlParameter("@Freight_Amt", freightCharges), New SqlParameter("@Other_Charges", OtherCharges), New SqlParameter("@Add_Charges", additionalCharges), New SqlParameter("@Total_Invoice_Amt", obj.Total_Order_Amt), New SqlParameter("@Salesman_Code", obj.Salesman_Code), New SqlParameter("@Mode_Of_Transport", obj.Mode_Of_Transport), New SqlParameter("@Vehicle_Code", obj.Vehicle_Code), New SqlParameter("@Vehicle_No", obj.Vehicle_No), New SqlParameter("@KM_Reading", obj.KM_Reading), New SqlParameter("@Route_No", obj.Route_No), New SqlParameter("@Route_Desc", obj.Route_Desc), New SqlParameter("@Scheme_Sample_Code", obj.Scheme_Sample_Code), New SqlParameter("@Price_Date", priceDate), New SqlParameter("@Terms_Code", obj.Terms_Code), New SqlParameter("@Comments", obj.Remarks), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", clsCommon.GETSERVERDATE(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", clsCommon.GETSERVERDATE(trans)), New SqlParameter("@Level1_User_code", l1User), New SqlParameter("@Level2_User_code", l2User), New SqlParameter("@Level3_User_code", l3User), New SqlParameter("@Level4_User_code", l4User), New SqlParameter("@Level5_User_code", l5User), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode), New SqlParameter("@Is_Post", "N"), New SqlParameter("@TPT", obj.Total_TPT), New SqlParameter("@Empty_Value", obj.Empty_Value), New SqlParameter("@Invoice_Type", IIf(clsCommon.myLen(strInvType) > 0, strInvType, transType)), New SqlParameter("@Total_Disc_Percent", dblTotDisPer), New SqlParameter("@Tax_Recoverable_Amt", dblTaxRecAmt), New SqlParameter("@Tot_Customer_Dis_Amt", clsCommon.myCdbl(obj.Tot_Customer_Dis_Amt)), New SqlParameter("@Tax_Recoverable_Amt2", dblTaxRecAmt2), New SqlParameter("@Tax_Recoverable_Amt3", dblTaxRecAmt3), New SqlParameter("@Balance_Amt", obj.Total_Order_Amt + obj.Empty_Value))
        clsDBFuncationality.ExecuteNonQuery("update TSPL_SALE_INVOICE_HEAD set Due_Date = '" + duedate.ToString() + "' where Sale_Invoice_No = '" + invNo + "'", trans)

        Dim strIsCreditInv As String = clsDBFuncationality.getSingleValue("select Credit_Customer from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Cust_Code + "'", trans)

        Sql = "UPDATE TSPL_SALE_INVOICE_HEAD SET "
        Sql += " Tax1 ='" + obj.TAX1 + "',Tax1_Rate='" + clsCommon.myCstr(obj.TAX1_Rate) + "',Tax1_Amt='" + clsCommon.myCstr(obj.TAX1_Amt) + "' "
        Sql += " ,Tax2 ='" + obj.TAX2 + "',Tax2_Rate='" + clsCommon.myCstr(obj.TAX2_Rate) + "',Tax2_Amt='" + clsCommon.myCstr(obj.TAX2_Amt) + "' "
        Sql += " ,Tax3 ='" + obj.TAX3 + "',Tax3_Rate='" + clsCommon.myCstr(obj.TAX3_Rate) + "',Tax3_Amt='" + clsCommon.myCstr(obj.TAX3_Amt) + "' "
        Sql += " ,Tax4 ='" + obj.TAX4 + "',Tax4_Rate='" + clsCommon.myCstr(obj.TAX4_Rate) + "',Tax4_Amt='" + clsCommon.myCstr(obj.TAX4_Amt) + "' "
        Sql += " ,Tax5 ='" + obj.TAX5 + "',Tax5_Rate='" + clsCommon.myCstr(obj.TAX5_Rate) + "',Tax5_Amt='" + clsCommon.myCstr(obj.TAX5_Amt) + "' "
        Sql += " ,Tax6 ='" + obj.TAX6 + "',Tax6_Rate='" + clsCommon.myCstr(obj.TAX6_Rate) + "',Tax6_Amt='" + clsCommon.myCstr(obj.TAX6_Amt) + "' "
        Sql += " ,is_Route_Jumped='" + clsCommon.myCstr(obj.is_Route_Jumped) + "'"
        Sql += " ,TAX1_Assessable_Amt='" + clsCommon.myCstr(obj.TAX1_Assessable_Amt) + "'"
        Sql += " ,TAX2_Assessable_Amt='" + clsCommon.myCstr(obj.TAX2_Assessable_Amt) + "'"
        Sql += " ,TAX3_Assessable_Amt='" + clsCommon.myCstr(obj.TAX3_Assessable_Amt) + "'"
        Sql += " ,TAX4_Assessable_Amt='" + clsCommon.myCstr(obj.TAX4_Assessable_Amt) + "'"
        Sql += " ,TAX5_Assessable_Amt='" + clsCommon.myCstr(obj.TAX5_Assessable_Amt) + "'"
        Sql += " ,TAX6_Assessable_Amt='" + clsCommon.myCstr(obj.TAX6_Assessable_Amt) + "'"
        Sql += " ,TAX7_Assessable_Amt='" + clsCommon.myCstr(obj.TAX7_Assessable_Amt) + "'"
        Sql += " ,TAX8_Assessable_Amt='" + clsCommon.myCstr(obj.TAX8_Assessable_Amt) + "'"
        Sql += " ,TAX9_Assessable_Amt='" + clsCommon.myCstr(obj.TAX9_Assessable_Amt) + "'"
        Sql += " ,TAX10_Assessable_Amt='" + clsCommon.myCstr(obj.TAX10_Assessable_Amt) + "'"
        Sql += " ,Credit_Invoice='" + strIsCreditInv + "',Verify_By='" + obj.Verify_By + "',is_Printed='" + clsCommon.myCstr(obj.Is_Printed) + "',Ship_To='" + obj.Ship_To + "',Ship_To_Desc='" + obj.Ship_To_Desc + "',Is_Scheduled='" + clsCommon.myCstr(obj.Is_Scheduled) + "',Against_C_Form='" + IIf(obj.Against_C_Form, "1", "0") + "',Mannual_Invoice_Amt='" + clsCommon.myCstr(obj.Mannual_Invoice_Amt) + "',Mannual_Invoice_Qty='" + clsCommon.myCstr(obj.Mannual_Invoice_Qty) + "',Customer_Invoice_No='" + obj.Customer_Invoice_No + "',Route_Type_Id='" + obj.Route_Type_Id + "',Created_By='" + obj.Created_By + "',Created_Date='" + clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy hh:mm tt") + "',Is_Create_Empty='" + IIf(obj.Is_Create_Empty, "1", "0") + "',Discount_On='" + clsCommon.myCstr(obj.Discount_On) + "'"
        Sql += " WHERE Sale_Invoice_No='" + invNo + "'"
        clsDBFuncationality.ExecuteNonQuery(Sql, trans)

        Dim invoiceId As Integer = 1
        For Each objtr As clsShipmentDetail In obj.Arr
            If objtr.Shipped_Qty > 0 Then
                Dim schemeItemCode As String = objtr.Scheme_Code_Qty
                If String.IsNullOrEmpty(schemeItemCode) Then
                    schemeItemCode = ""
                End If
                priceDate = objtr.Price_Date
                clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_SALE_INVOICE_DETAIL_INSERT", New SqlParameter("@Sale_Invoice_Id", invoiceId), New SqlParameter("@mainitem", objtr.Main_Item), New SqlParameter("@Sale_Invoice_No", invNo), New SqlParameter("@Complete", objtr.Complete), New SqlParameter("@Item_Code", objtr.Item_Code), New SqlParameter("@Item_Desc", objtr.Item_Desc), New SqlParameter("@Price_Date", priceDate), New SqlParameter("@Order_Qty", objtr.Order_Qty), New SqlParameter("@Shipped_Qty", objtr.Shipped_Qty), New SqlParameter("@Invoice_Qty", objtr.Shipped_Qty), New SqlParameter("@Balance_Qty", objtr.Shipped_Qty), New SqlParameter("@Unit_code", objtr.Unit_code), New SqlParameter("@Location", objtr.Location), New SqlParameter("@Price_code", obj.Price_Code), New SqlParameter("@Scheme_Applicable", objtr.Scheme_Applicable), New SqlParameter("@Scheme_Code_Qty", schemeItemCode), New SqlParameter("@Scheme_Item", objtr.Scheme_Item), New SqlParameter("@Scheme_Disc_Applicable ", objtr.Scheme_Disc_Applicable), New SqlParameter("@Scheme_Code_Cash ", objtr.Scheme_Code_Cash), New SqlParameter("@Sampling_Item ", objtr.Sampling_Item), New SqlParameter("@Empty_Value ", objtr.Empty_Value), New SqlParameter("@Empty_Value_Shell ", objtr.Empty_Value_Shell), New SqlParameter("@Empty_Value_Bottle ", objtr.Empty_Value_Bottle), New SqlParameter("@MRP_Amt ", objtr.MRP_Amt), New SqlParameter("@Basic_Rate ", objtr.Basic_Rate), New SqlParameter("@Item_Assessable_Rate", objtr.Item_Assessable_Rate), New SqlParameter("@Disc_Amt", objtr.Disc_Amt), New SqlParameter("@Item_Net_Amt", objtr.Item_Net_Amt), New SqlParameter("@Item_Tax", objtr.Item_Tax), New SqlParameter("@Total_Assessable_Amt", objtr.Total_Assessable_Amt), New SqlParameter("@Total_MRP_Amt", objtr.Total_MRP_Amt), New SqlParameter("@Total_Basic_Amt", objtr.Total_Basic_Amt), New SqlParameter("@Total_Disc_Amt", objtr.Total_Disc_Amt), New SqlParameter("@Total_net_Amt", objtr.Total_net_Amt), New SqlParameter("@Total_Tax_Amt", objtr.Total_Tax_Amt), New SqlParameter("@TPT", objtr.TPT), New SqlParameter("@Total_Item_Amt", objtr.Total_Item_Amt), New SqlParameter("@total_TPT", objtr.Total_TPT), New SqlParameter("@Promo_Scheme_Applicable", objtr.Promo_Scheme_Applicable), New SqlParameter("@Promo_Scheme_Code", objtr.Promo_Scheme_Code), New SqlParameter("@Promo_Scheme_Item", objtr.Promo_Scheme_Item), New SqlParameter("@Cust_Discount", objtr.Cust_Discount), New SqlParameter("@Total_Cust_Discount", objtr.Total_Cust_Discount), New SqlParameter("@Discount_Code", objtr.Discount_Code), New SqlParameter("@Cust_Item_Discount_NoTax", objtr.Cust_Item_Discount_NoTax))

                qry = "select TSPL_ITEM_PRICE_MASTER.Price_Amount1 ,TSPL_ITEM_PRICE_MASTER.Price_Amount2 ,TSPL_ITEM_PRICE_MASTER.Price_Amount3 ,TSPL_ITEM_PRICE_MASTER.Price_Amount4 ,TSPL_ITEM_PRICE_MASTER.Price_Amount5 ,TSPL_ITEM_PRICE_MASTER.Price_Amount6 ,TSPL_ITEM_PRICE_MASTER.Price_Amount7 ,TSPL_ITEM_PRICE_MASTER.Price_Amount8 ,TSPL_ITEM_PRICE_MASTER.Price_Amount9 ,TSPL_ITEM_PRICE_MASTER.Price_Amount10 from TSPL_ITEM_PRICE_MASTER"
                qry += " where  TSPL_ITEM_PRICE_MASTER.Price_Code='" + objtr.Price_code + "' and  TSPL_ITEM_PRICE_MASTER.Item_Code='" + objtr.Item_Code + "' and TSPL_ITEM_PRICE_MASTER.Item_Basic_Net='" + clsCommon.myCstr(clsCommon.myCdbl(objtr.MRP_Amt * clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_code, trans))) + "' and TSPL_ITEM_PRICE_MASTER.UOM='FC' "
                If objtr.Price_Date_Actual IsNot Nothing Then
                    qry += " and  TSPL_ITEM_PRICE_MASTER.Start_Date= '" + clsCommon.GetPrintDate(objtr.Price_Date_Actual, "dd/MMM/yyyy") + "'"
                Else
                    qry += " and  TSPL_ITEM_PRICE_MASTER.Start_Date= '" + clsCommon.GetPrintDate(objtr.Price_Date, "dd/MMM/yyyy") + "'"
                End If
                Dim dtPriceComponent As DataTable = clsDBFuncationality.GetDataTable(qry, trans)


                Sql = "UPDATE TSPL_SHIPMENT_DETAILS SET Balance_Qty='0' WHERE Shipment_Id='" + invoiceId.ToString() + "' and Shipment_No='" + obj.Shipment_No + "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                ''If clsCommon.CompairString(obj.Shipment_Type, "Transfer") = CompairStringResult.Equal Then
                Sql = "UPDATE TSPL_SALE_INVOICE_DETAIL SET "
                Sql += " Tax1 ='" + objtr.TAX1 + "',Tax1_Rate='" + clsCommon.myCstr(objtr.TAX1_Rate) + "',Tax1_Amt='" + clsCommon.myCstr(objtr.TAX1_Amt) + "',Tax1_Assessable_Amt='" + clsCommon.myCstr(objtr.TAX1_Assessable_Amt) + "'"
                Sql += " ,Tax2 ='" + objtr.TAX2 + "',Tax2_Rate='" + clsCommon.myCstr(objtr.TAX2_Rate) + "',Tax2_Amt='" + clsCommon.myCstr(objtr.TAX2_Amt) + "',Tax2_Assessable_Amt='" + clsCommon.myCstr(objtr.TAX2_Assessable_Amt) + "'"
                Sql += " ,Tax3 ='" + objtr.TAX3 + "',Tax3_Rate='" + clsCommon.myCstr(objtr.TAX3_Rate) + "',Tax3_Amt='" + clsCommon.myCstr(objtr.TAX3_Amt) + "',Tax3_Assessable_Amt='" + clsCommon.myCstr(objtr.TAX3_Assessable_Amt) + "'"
                Sql += " ,Tax4 ='" + objtr.TAX4 + "',Tax4_Rate='" + clsCommon.myCstr(objtr.TAX4_Rate) + "',Tax4_Amt='" + clsCommon.myCstr(objtr.TAX4_Amt) + "',Tax4_Assessable_Amt='" + clsCommon.myCstr(objtr.TAX4_Assessable_Amt) + "'"
                Sql += " ,Tax5 ='" + objtr.TAX5 + "',Tax5_Rate='" + clsCommon.myCstr(objtr.TAX5_Rate) + "',Tax5_Amt='" + clsCommon.myCstr(objtr.TAX5_Amt) + "',Tax5_Assessable_Amt='" + clsCommon.myCstr(objtr.TAX5_Assessable_Amt) + "'"
                Sql += " ,Tax6 ='" + objtr.TAX6 + "',Tax6_Rate='" + clsCommon.myCstr(objtr.TAX6_Rate) + "',Tax6_Amt='" + clsCommon.myCstr(objtr.TAX6_Amt) + "',Tax6_Assessable_Amt='" + clsCommon.myCstr(objtr.TAX6_Assessable_Amt) + "',Target_Discount_Amt='" + clsCommon.myCstr(objtr.Target_Discount_Amt) + "'"
                If objtr.Price_Date_Actual IsNot Nothing Then
                    Sql += " ,Price_Date_Actual ='" + clsCommon.GetPrintDate(objtr.Price_Date_Actual, "dd/MMM/yyyy") + "'"
                End If
                If dtPriceComponent IsNot Nothing AndAlso dtPriceComponent.Rows.Count > 0 Then
                    Sql += " ,TSPL_SALE_INVOICE_DETAIL.Price_Amount1 =" + clsCommon.myCstr(clsCommon.myCdbl(dtPriceComponent.Rows(0)("Price_Amount1"))) + ", TSPL_SALE_INVOICE_DETAIL.Price_Amount2 =" + clsCommon.myCstr(clsCommon.myCdbl(dtPriceComponent.Rows(0)("Price_Amount2"))) + ",TSPL_SALE_INVOICE_DETAIL.Price_Amount3 =" + clsCommon.myCstr(clsCommon.myCdbl(dtPriceComponent.Rows(0)("Price_Amount3"))) + ", TSPL_SALE_INVOICE_DETAIL.Price_Amount4 =" + clsCommon.myCstr(clsCommon.myCdbl(dtPriceComponent.Rows(0)("Price_Amount4"))) + " , TSPL_SALE_INVOICE_DETAIL.Price_Amount5 =" + clsCommon.myCstr(clsCommon.myCdbl(dtPriceComponent.Rows(0)("Price_Amount5"))) + ", TSPL_SALE_INVOICE_DETAIL.Price_Amount6 =" + clsCommon.myCstr(clsCommon.myCdbl(dtPriceComponent.Rows(0)("Price_Amount6"))) + ",TSPL_SALE_INVOICE_DETAIL.Price_Amount7 =" + clsCommon.myCstr(clsCommon.myCdbl(dtPriceComponent.Rows(0)("Price_Amount7"))) + ",  TSPL_SALE_INVOICE_DETAIL.Price_Amount8 =" + clsCommon.myCstr(clsCommon.myCdbl(dtPriceComponent.Rows(0)("Price_Amount8"))) + ",TSPL_SALE_INVOICE_DETAIL.Price_Amount9 =" + clsCommon.myCstr(clsCommon.myCdbl(dtPriceComponent.Rows(0)("Price_Amount9"))) + ", TSPL_SALE_INVOICE_DETAIL.Price_Amount10=" + clsCommon.myCstr(clsCommon.myCdbl(dtPriceComponent.Rows(0)("Price_Amount10"))) + ""
                Else
                    Sql += " ,TSPL_SALE_INVOICE_DETAIL.Price_Amount1 =0, TSPL_SALE_INVOICE_DETAIL.Price_Amount2 =0,TSPL_SALE_INVOICE_DETAIL.Price_Amount3 =0, TSPL_SALE_INVOICE_DETAIL.Price_Amount4 =0 , TSPL_SALE_INVOICE_DETAIL.Price_Amount5 =0, TSPL_SALE_INVOICE_DETAIL.Price_Amount6 =0,TSPL_SALE_INVOICE_DETAIL.Price_Amount7 =0,  TSPL_SALE_INVOICE_DETAIL.Price_Amount8 =0,TSPL_SALE_INVOICE_DETAIL.Price_Amount9 =0, TSPL_SALE_INVOICE_DETAIL.Price_Amount10=0 "
                End If
                Sql += " ,TSPL_SALE_INVOICE_DETAIL.Price_To_Show='" + clsCommon.myCstr(objtr.Price_To_Show) + "'"

                Sql += ", TSPL_SALE_INVOICE_DETAIL.RAW_Qty=" + clsCommon.myCstr(objtr.RAW_Qty) + ", TSPL_SALE_INVOICE_DETAIL.Converted_Qty=" + clsCommon.myCstr(objtr.Converted_Qty) + ", TSPL_SALE_INVOICE_DETAIL.Abatement_rate=" + clsCommon.myCstr(objtr.Abatement_rate) + ""
                Sql += ",TSPL_SALE_INVOICE_DETAIL.From_Scheme_Code='" + clsCommon.myCstr(objtr.From_Scheme_Code) + "' "
                Sql += " WHERE Sale_Invoice_No='" + invNo.ToString() + "' AND Sale_Invoice_Id='" + invoiceId.ToString() + "'"
                clsDBFuncationality.ExecuteNonQuery(Sql, trans)
                ''End If
                invoiceId = invoiceId + 1
            End If
        Next
        qry = "update TSPL_SALE_INVOICE_DETAIL set TSPL_SALE_INVOICE_DETAIL.Unit_Cogs=TSPL_SHIPMENT_DETAILS.Unit_COGS"
        qry += " from TSPL_SALE_INVOICE_DETAIL "
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No"
        qry += " left outer join TSPL_SHIPMENT_DETAILS on TSPL_SHIPMENT_DETAILS.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No "
        qry += " and TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_Id=TSPL_SHIPMENT_DETAILS.Shipment_Id"
        qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + Invoiceno + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)

        ''qry = "select 1 from TSPL_SALE_INVOICE_HEAD where Shipment_No='" + strShipCode + "'"
        ''Dim dtSaleInvoice As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        ''If dtSaleInvoice IsNot Nothing AndAlso dtSaleInvoice.Rows.Count > 1 Then
        ''    Throw New Exception("There exist " + clsCommon.myCstr(dtSaleInvoice.Rows.Count) + " Sale Invoice Against Shipment No. " + strShipCode)
        ''End If

        qry = "select 1 from TSPL_SHIPMENT_MASTER where LEN(ISNULL( TAX1,''))<=0  and Shipment_No='" + strShipCode + "'"
        Dim dtSaleInvoice As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtSaleInvoice IsNot Nothing AndAlso dtSaleInvoice.Rows.Count > 1 Then
            Throw New Exception("Tax Authority can'e be left blank.")
        End If

        qry = "update TSPL_SHIPMENT_MASTER set Invoice_No='" + Invoiceno + "' where Shipment_No='" + strShipCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Return Invoiceno
    End Function

    Public Shared Function Postdata(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Invoice No not found to post")
            End If


            CreateJournalEntry(strCode, trans)

            Dim qry As String = "UPDATE TSPL_SALE_INVOICE_HEAD SET Is_Post='Y' WHERE Sale_invoice_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strAdjustmentNo As String = ClsAdjustments.GetEmptyAdjustmentCode(strCode, trans)

            If clsCommon.myLen(strAdjustmentNo) > 0 Then
                ClsAdjustments.PostData(strAdjustmentNo, "Empty Transactions", trans) 'Updated by--Pankaj on 23/07/2012
            End If


            '''''''''''''''''''''''''''''''''''''For Making AR Invoice
            ''Dim objCustInv As New clsCustomerInvoiceHead()
            '' ''objCustInv.Document_No
            ''objCustInv.Document_Date = obj.Sale_Invoice_Date
            ''objCustInv.Document_Type = "I"
            ''objCustInv.Document_Total = obj.Total_Invoice_Amt
            ''objCustInv.Customer_Code = obj.Cust_Code
            ''objCustInv.Customer_Name = obj.Cust_Name
            ''objCustInv.Posting_Date = obj.Sale_Invoice_Date
            ''qry = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Cust_Code + "'"
            ''objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, trans)
            '' ''objCustInv.Order_No
            ''objCustInv.On_Hold = 0
            ''objCustInv.Remarks = obj.Remarks
            ''objCustInv.Description = obj.Description
            ''objCustInv.Tax_Group = obj.Tax_Group
            ''objCustInv.TAX1 = obj.TAX1
            ''objCustInv.TAX1_Rate = obj.TAX1_Rate
            ''objCustInv.TAX1_Amt = obj.TAX1_Amt
            ''objCustInv.TAX2 = obj.TAX2_Amt
            ''objCustInv.TAX2_Rate = obj.TAX2_Rate
            ''objCustInv.TAX2_Amt = obj.TAX2_Amt
            ''objCustInv.TAX3 = obj.TAX3
            ''objCustInv.TAX3_Rate = obj.TAX3_Rate
            ''objCustInv.TAX3_Amt = obj.TAX3_Amt
            ''objCustInv.TAX4 = obj.TAX4
            ''objCustInv.TAX4_Rate = obj.TAX4_Rate
            ''objCustInv.TAX4_Amt = obj.TAX4_Amt
            ''objCustInv.TAX5 = obj.TAX5
            ''objCustInv.TAX5_Rate = obj.TAX5_Rate
            ''objCustInv.TAX5_Amt = obj.TAX5_Amt
            ''objCustInv.TAX6 = obj.TAX6
            ''objCustInv.TAX6_Rate = obj.TAX6_Rate
            ''objCustInv.TAX6_Amt = obj.TAX6_Amt
            ''objCustInv.TAX7 = obj.TAX7
            ''objCustInv.TAX7_Rate = obj.TAX7_Rate
            ''objCustInv.TAX7_Amt = obj.TAX7_Amt
            ''objCustInv.TAX8 = obj.TAX8
            ''objCustInv.TAX8_Rate = obj.TAX8_Rate
            ''objCustInv.TAX8_Amt = obj.TAX8_Amt
            ''objCustInv.TAX9 = obj.TAX9
            ''objCustInv.TAX9_Rate = obj.TAX9_Rate
            ''objCustInv.TAX9_Amt = obj.TAX9_Amt
            ''objCustInv.TAX10 = obj.TAX10
            ''objCustInv.TAX10_Rate = obj.TAX10_Rate
            ''objCustInv.TAX10_Amt = obj.TAX10_Amt
            ''objCustInv.Total_Tax = obj.Inv_Tax_Amt
            ''objCustInv.Tax1_BAmount = obj.TAX1_Assessable_Amt
            ''objCustInv.Tax2_BAmount = obj.TAX2_Assessable_Amt
            ''objCustInv.Tax3_BAmount = obj.TAX3_Assessable_Amt
            ''objCustInv.Tax4_BAmount = obj.TAX4_Assessable_Amt
            ''objCustInv.Tax5_BAmount = obj.TAX5_Assessable_Amt
            ''objCustInv.Tax6_BAmount = obj.TAX6_Assessable_Amt
            ''objCustInv.Tax7_BAmount = obj.TAX7_Assessable_Amt
            ''objCustInv.Tax8_BAmount = obj.TAX8_Assessable_Amt
            ''objCustInv.Tax9_BAmount = obj.TAX9_Assessable_Amt
            ''objCustInv.Tax10_BAmount = obj.TAX10_Assessable_Amt
            ''objCustInv.Balance_Amt = obj.Total_Invoice_Amt
            ''objCustInv.Terms_Code = obj.Terms_Code
            ''qry = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + obj.Terms_Code + "'"
            ''dt = clsDBFuncationality.GetDataTable(qry, trans)
            ''If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            ''    objCustInv.Terms_Description = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
            ''    objCustInv.Due_Date = obj.Sale_Invoice_Date.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
            ''End If
            ''objCustInv.Discount_Percentage = obj.Inv_Disc_Percent
            ''objCustInv.Discount_Base = obj.Inv_Discount_Amt * (100 / obj.Inv_Disc_Percent)
            ''objCustInv.Discount_Amount = obj.Inv_Discount_Amt
            '' ''objCustInv.Amount_Less_Discount = 
            ''dt = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", trans)
            ''If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            ''    objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
            ''    If clsCommon.myCdbl(obj.Inv_Discount_Amt) > 0 Then
            ''        objCustInv.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Receipts_Discount_acct"))
            ''    End If
            ''End If

            ''If obj.TAX1_Amt > 0 AndAlso clsCommon.myLen(obj.TAX1) > 0 AndAlso clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            ''    objCustInv.TAX1_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX1, trans)
            ''End If
            ''If obj.TAX2_Amt > 0 AndAlso clsCommon.myLen(obj.TAX2) > 0 AndAlso clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            ''    objCustInv.TAX2_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX2, trans)
            ''End If
            ''If obj.TAX3_Amt > 0 AndAlso clsCommon.myLen(obj.TAX3) > 0 AndAlso clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            ''    objCustInv.TAX3_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX3, trans)
            ''End If
            ''If obj.TAX4_Amt > 0 AndAlso clsCommon.myLen(obj.TAX4) > 0 AndAlso clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            ''    objCustInv.TAX4_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX4, trans)
            ''End If
            ''If obj.TAX5_Amt > 0 AndAlso clsCommon.myLen(obj.TAX5) > 0 AndAlso clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            ''    objCustInv.TAX5_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX5, trans)
            ''End If
            ''If obj.TAX6_Amt > 0 AndAlso clsCommon.myLen(obj.TAX6) > 0 AndAlso clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            ''    objCustInv.TAX6_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX6, trans)
            ''End If
            ''If obj.TAX7_Amt > 0 AndAlso clsCommon.myLen(obj.TAX7) > 0 AndAlso clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            ''    objCustInv.TAX7_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX7, trans)
            ''End If
            ''If obj.TAX8_Amt > 0 AndAlso clsCommon.myLen(obj.TAX8) > 0 AndAlso clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            ''    objCustInv.TAX8_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX8, trans)
            ''End If
            ''If obj.TAX9_Amt > 0 AndAlso clsCommon.myLen(obj.TAX9) > 0 AndAlso clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            ''    objCustInv.TAX9_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX9, trans)
            ''End If
            ''If obj.TAX10_Amt > 0 AndAlso clsCommon.myLen(obj.TAX10) > 0 AndAlso clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            ''    objCustInv.TAX10_GLAC = clsTaxMaster.GetTaxPayAC(obj.TAX10, trans)
            ''End If

            '' ''objCustInv.RefDocType
            '' ''objCustInv.RefDocNo
            '' ''objCustInv.Add_Charge_Code1
            '' ''objCustInv.Add_Charge_Name1
            '' ''objCustInv.Add_Charge_Amt1()
            '' ''objCustInv.Add_Charge_Code2
            '' ''objCustInv.Add_Charge_Name2
            '' ''objCustInv.Add_Charge_Amt2()
            '' ''objCustInv.Add_Charge_Code3
            '' ''objCustInv.Add_Charge_Name3
            '' ''objCustInv.Add_Charge_Amt3()
            '' ''objCustInv.Add_Charge_Code4
            '' ''objCustInv.Add_Charge_Name4
            '' ''objCustInv.Add_Charge_Amt4()
            '' ''objCustInv.Add_Charge_Code5
            '' ''objCustInv.Add_Charge_Name5
            '' ''objCustInv.Add_Charge_Amt5()
            '' ''objCustInv.Add_Charge_Code6
            '' ''objCustInv.Add_Charge_Name6
            '' ''objCustInv.Add_Charge_Amt6()
            '' ''objCustInv.Add_Charge_Code7
            '' ''objCustInv.Add_Charge_Name7
            '' ''objCustInv.Add_Charge_Amt7()
            '' ''objCustInv.Add_Charge_Code8
            '' ''objCustInv.Add_Charge_Name8
            '' ''objCustInv.Add_Charge_Amt8()
            '' ''objCustInv.Add_Charge_Code9
            '' ''objCustInv.Add_Charge_Name9
            '' ''objCustInv.Add_Charge_Amt9()
            '' ''objCustInv.Add_Charge_Code10
            '' ''objCustInv.Add_Charge_Name10
            '' ''objCustInv.Add_Charge_Amt10()
            '' ''objCustInv.Total_Add_Charge()
            ''objCustInv.Tax_Calculation_Type = EnumTaxCalucationType.Automatic
            '' ''objCustInv.Status
            '' ''objCustInv.AgainstScrap
            ''objCustInv.Against_Sale_No = obj.Sale_Invoice_No
            ''Dim counter As Integer = 1
            ''objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)
            ''For Each objTr As clsSaleDetail In obj.Arr
            ''    Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()
            ''    objCustInvTR.SNo = counter
            ''    dt = clsItemMaster.GetSaleAccGLAC(objTr.Item_Code, trans)
            ''    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            ''        objCustInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Sales_Account"))
            ''        objCustInvTR.GL_Account_Desc = clsCommon.myCstr(dt.Rows(0)("Sales_Class_Desc"))
            ''        objCustInvTR.Amount = objTr.Total_Basic_Amt
            ''        ''objCustInvTR.Discount_Per=
            ''        objCustInvTR.Discount = objTr.Total_Disc_Amt
            ''        objCustInvTR.Amount_less_Discount = objTr.Total_Basic_Amt - objTr.Total_Disc_Amt
            ''        objCustInvTR.TAX1 = objTr.TAX1
            ''        objCustInvTR.TAX1_Rate = objTr.TAX1_Rate
            ''        objCustInvTR.TAX1_Amt = objTr.TAX1_Amt
            ''        objCustInvTR.TAX2 = objTr.TAX2
            ''        objCustInvTR.TAX2_Rate = objTr.TAX2_Rate
            ''        objCustInvTR.TAX2_Amt = objTr.TAX2_Amt
            ''        objCustInvTR.TAX3 = objTr.TAX3
            ''        objCustInvTR.TAX3_Rate = objTr.TAX3_Rate
            ''        objCustInvTR.TAX3_Amt = objTr.TAX3_Amt
            ''        objCustInvTR.TAX4 = objTr.TAX4
            ''        objCustInvTR.TAX4_Rate = objTr.TAX4_Rate
            ''        objCustInvTR.TAX4_Amt = objTr.TAX4_Amt
            ''        objCustInvTR.TAX5 = objTr.TAX5
            ''        objCustInvTR.TAX5_Rate = objTr.TAX5_Rate
            ''        objCustInvTR.TAX5_Amt = objTr.TAX5_Amt
            ''        objCustInvTR.TAX6 = objTr.TAX6
            ''        objCustInvTR.TAX6_Rate = objTr.TAX6_Rate
            ''        objCustInvTR.TAX6_Amt = objTr.TAX6_Amt
            ''        objCustInvTR.TAX7 = objTr.TAX7
            ''        objCustInvTR.TAX7_Rate = objTr.TAX7_Rate
            ''        objCustInvTR.TAX7_Amt = objTr.TAX7_Amt
            ''        objCustInvTR.TAX8 = objTr.TAX8
            ''        objCustInvTR.TAX8_Rate = objTr.TAX8_Rate
            ''        objCustInvTR.TAX8_Amt = objTr.TAX8_Amt
            ''        objCustInvTR.TAX9 = objTr.TAX9
            ''        objCustInvTR.TAX9_Rate = objTr.TAX9_Rate
            ''        objCustInvTR.TAX9_Amt = objTr.TAX9_Amt
            ''        objCustInvTR.TAX10 = objTr.TAX10
            ''        objCustInvTR.TAX10_Rate = objTr.TAX10_Rate
            ''        objCustInvTR.TAX10_Amt = objTr.TAX10_Amt
            ''        objCustInvTR.Total_Tax = objTr.Total_Tax_Amt
            ''        objCustInvTR.Total_Amount = objTr.Total_net_Amt
            ''        ''objCustInvTR.Remarks=
            ''        ''objCustInvTR.Comments=
            ''        objCustInv.Arr.Add(objCustInvTR)
            ''        counter += 1
            ''    End If
            ''Next
            ''objCustInv.SaveData(objCustInv, True, trans)
            ''qry = "update TSPL_Customer_Invoice_Head set Status=1,Posting_Date='" + clsCommon.GetPrintDate(objCustInv.Document_Date, "dd/MMM/yyyy") + "' where Document_No='" + objCustInv.Document_No + "'"
            ''clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJournalEntry(ByVal strCode As String, ByVal trans As SqlTransaction)
        Dim obj As New clsSaleHead
        obj = obj.GetData(strCode, trans)
        Dim ArryLstGLAC As ArrayList = New ArrayList()

        Dim strShipmentClearingAC As String = ""
        Dim strCogsAccount As String = ""
        Dim qry As String = "select Receivable_Control_acct,Container_Deposit from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + obj.Cust_Account + "'"
        Dim dtCustACSet As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtCustACSet Is Nothing OrElse dtCustACSet.Rows.Count <= 0 Then
            Throw New Exception("Customer Account Set Not found")
        End If

        qry = "select Sale_Class_Code,Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + obj.Arr(0).Item_Code + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Please set Sale_Class_Code and Purchase_Class_Code for first item")
        End If
        Dim strFirstItemSaleAccountSet As String = clsCommon.myCstr(dt.Rows(0)("Sale_Class_Code"))
        Dim strFirstItemPurchaseAccountSet As String = clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code"))


        qry = "select Sales_Account,Cost_Of_Goods_Sold,Returnable_Container,Schemes,Promotional from TSPL_SALES_ACCOUNTS where Sales_Class_Code='" + strFirstItemSaleAccountSet + "'"
        Dim dtSaleACSet As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtSaleACSet Is Nothing OrElse dtSaleACSet.Rows.Count <= 0 Then
            Throw New Exception("Customer Account Set Not found")
        End If
        Dim isLocationExcisable As Boolean = clsLocation.isLocatinExcisable(obj.Location, trans)
        Dim dblTotTPT As Double = obj.TPT
        Dim dblShipmentTotCost As Double = 0
        Dim dblSchemeTotAmt As Double = 0
        Dim dblSamplingTotAmt As Double = 0

        Dim dblTotTaxAmt1 As Double = 0
        Dim dblTotTaxAmt2 As Double = 0
        Dim dblTotTaxAmt3 As Double = 0
        Dim dblTotTaxAmt4 As Double = 0
        Dim dblTotTaxAmt5 As Double = 0
        Dim dblTotTaxAmt6 As Double = 0

        Dim dblTotTaxAmtFOC1 As Double = 0
        Dim dblTotTaxAmtFOC2 As Double = 0
        Dim dblTotTaxAmtFOC3 As Double = 0
        Dim dblTotTaxAmtFOC4 As Double = 0
        Dim dblTotTaxAmtFOC5 As Double = 0
        Dim dblTotTaxAmtFOC6 As Double = 0


        Dim dblExcise As Double = 0
        Dim dblCess As Double = 0
        Dim dblHECess As Double = 0
        Dim dblExciseFOC As Double = 0
        Dim dblCessFOC As Double = 0
        Dim dblHECessFOC As Double = 0

        Dim ArrItemWiseGLAC As List(Of clsJournalDetailTemp) = New List(Of clsJournalDetailTemp)()

        For Each objtr As clsSaleDetail In obj.Arr

            Dim convFac As Double = clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_code, trans)
            If objtr.Unit_Cogs > 0 Then
                dblShipmentTotCost += objtr.Unit_Cogs * objtr.Invoice_Qty / convFac
                If clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal Then
                    dblSchemeTotAmt += objtr.Invoice_Qty * objtr.Unit_Cogs / convFac
                ElseIf clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal Then
                    dblSamplingTotAmt += objtr.Invoice_Qty * objtr.Unit_Cogs / convFac
                End If
            End If



            ''If (clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Promo_Scheme_Applicable, "Y") = CompairStringResult.Equal) Then
            If (clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal) Then
                dblTotTaxAmtFOC1 += objtr.Invoice_Qty * objtr.TAX1_Amt
                dblTotTaxAmtFOC2 += objtr.Invoice_Qty * objtr.TAX2_Amt
                dblTotTaxAmtFOC3 += objtr.Invoice_Qty * objtr.TAX3_Amt
                dblTotTaxAmtFOC4 += objtr.Invoice_Qty * objtr.TAX4_Amt
                dblTotTaxAmtFOC5 += objtr.Invoice_Qty * objtr.TAX5_Amt
                dblTotTaxAmtFOC6 += objtr.Invoice_Qty * objtr.TAX6_Amt
            End If

            dblTotTaxAmt1 += objtr.Invoice_Qty * objtr.TAX1_Amt
            dblTotTaxAmt2 += objtr.Invoice_Qty * objtr.TAX2_Amt
            dblTotTaxAmt3 += objtr.Invoice_Qty * objtr.TAX3_Amt
            dblTotTaxAmt4 += objtr.Invoice_Qty * objtr.TAX4_Amt
            dblTotTaxAmt5 += objtr.Invoice_Qty * objtr.TAX5_Amt
            dblTotTaxAmt6 += objtr.Invoice_Qty * objtr.TAX6_Amt
            Dim dblCurrExcise As Double = 0
            Dim dblCurrCess As Double = 0
            Dim dblCurrHECess As Double = 0
            If Not isLocationExcisable Then
                Dim strItemForExcise As String = objtr.Item_Code
                If (clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal) Then
                    strItemForExcise = objtr.Main_Item
                End If
                Dim objItemTax As clsItemTax = clsItemTax.GetData(strItemForExcise, trans)
                If objItemTax IsNot Nothing Then
                    ''Dim subStr As String = objtr.Item_Code.Substring(0, 2)

                    ''Dim dblAbdRate As Double = IIf(clsCommon.CompairString("SM", subStr) = CompairStringResult.Equal, 0.65, 0.6)
                    Dim dblAbdRate As Double = objtr.Abatement_rate / 100
                    dblCurrExcise = dblAbdRate * objtr.Total_MRP_Amt * objItemTax.Excise / 100
                    ''If (clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Promo_Scheme_Applicable, "Y") = CompairStringResult.Equal) Then
                    If (clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal) Then
                        dblExciseFOC += dblCurrExcise
                        dblCessFOC += dblCurrExcise * objItemTax.Ecess / 100
                        dblHECessFOC += dblCurrExcise * objItemTax.Hcess / 100
                        dblCurrExcise = 0 ''Becuase it is Added for current item GL Account code.
                    Else
                        ''dblExcise += dblCurrExcise
                        dblCurrCess = dblCurrExcise * objItemTax.Ecess / 100
                        dblCurrHECess = dblCurrExcise * objItemTax.Hcess / 100
                    End If
                End If
            End If
            dblCurrExcise = Math.Round(dblCurrExcise, 2, MidpointRounding.ToEven)
            dblCurrCess = Math.Round(dblCurrCess, 2, MidpointRounding.ToEven)
            dblCurrHECess = Math.Round(dblCurrHECess, 2, MidpointRounding.ToEven)



            ''''Item wise GL Account''''''
            Dim objItemWiseGLAC As clsJournalDetailTemp = New clsJournalDetailTemp()
            qry = "select Sales_Account from TSPL_SALES_ACCOUNTS where Sales_Class_Code in (select Sale_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')"
            objItemWiseGLAC.Account_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(objItemWiseGLAC.Account_code) <= 0 Then
                Throw New Exception("Please set Sale Account of Sale Account set for item : " + objtr.Item_Code)
            End If
            Dim dblTotExciseAmt As Double = 0
            If Not isLocationExcisable AndAlso (objtr.Total_net_Amt - dblCurrExcise - dblCurrCess - dblCurrHECess) < 0 Then
                dblCurrExcise = 0
                dblCurrCess = 0
                dblCurrHECess = 0
            Else
                dblTotExciseAmt = dblCurrExcise + dblCurrCess + dblCurrHECess
            End If

            Dim dblSaleAmount As Double = IIf(isLocationExcisable, objtr.Total_net_Amt, (objtr.Total_net_Amt - dblTotExciseAmt))
            objItemWiseGLAC.Account_code = clsERPFuncationality.ChangeGLAccountLocationSegment(objItemWiseGLAC.Account_code, obj.Location, trans)
            objItemWiseGLAC.Amount = -1 * dblSaleAmount
            ArrItemWiseGLAC.Add(objItemWiseGLAC)
            ''''End of Item wise GL Account''''''


            dblExcise += dblCurrExcise
            dblCess += dblCurrCess
            dblHECess += dblCurrHECess


            ''Update [Sale_Account_Amount] in sale invoice detail table 
            qry = " Update TSPL_SALE_INVOICE_DETAIL set Sale_Account_Amount='" + clsCommon.myCstr(dblSaleAmount) + "' where Sale_Invoice_No='" + objtr.Sale_Invoice_No + "' and Sale_Invoice_Id='" + clsCommon.myCstr(objtr.Sale_Invoice_Id) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''End of Update [GL_Account_Amount] in sale invoice detail table 
        Next
        ''Update [Tot_Sale_Account_Amount] in sale invoice Header table 
        qry = " Update TSPL_SALE_INVOICE_HEAD set Tot_Sale_Account_Amount=(select sum(Sale_Account_Amount) from TSPL_SALE_INVOICE_DETAIL where TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No='" + obj.Sale_Invoice_No + "') where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + obj.Sale_Invoice_No + "' "
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        ''End of Update [Tot_Sale_Account_Amount] in sale invoice Header table 

        dblExcise = Math.Round(dblExcise, 2, MidpointRounding.ToEven)
        dblCess = Math.Round(dblCess, 2, MidpointRounding.ToEven)
        dblHECess = Math.Round(dblHECess, 2, MidpointRounding.ToEven)

        dblExciseFOC = Math.Round(dblExciseFOC, 2, MidpointRounding.ToEven)
        dblCessFOC = Math.Round(dblCessFOC, 2, MidpointRounding.ToEven)
        dblHECessFOC = Math.Round(dblHECessFOC, 2, MidpointRounding.ToEven)
        ''''''''''''''''''''''''''End of Calucaltion Part



        '''''''''''''''''''''''''''''Jouranal Entery Begins Now
        If obj.Total_Invoice_Amt > 0 AndAlso obj.Total_Disc_Percent <> 100 Then
            Dim strDebtorCtrlAc As String = clsCommon.myCstr(dtCustACSet.Rows(0)("Receivable_Control_acct"))
            If clsCommon.myLen(strDebtorCtrlAc) <= 0 Then
                Throw New Exception("Please set Debtor Control Account of Customer Account set")
            End If
            strDebtorCtrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strDebtorCtrlAc, obj.Location, trans)
            Dim Acc() As String = {strDebtorCtrlAc, obj.Total_Invoice_Amt}
            ArryLstGLAC.Add(Acc)
        End If

        If obj.Empty_Value > 0 Then
            Dim strContainerDeposit As String = clsCommon.myCstr(dtCustACSet.Rows(0)("Container_Deposit"))
            If clsCommon.myLen(strContainerDeposit) <= 0 Then
                Throw New Exception("Please set Debtor Control Account of Customer Account set")
            End If
            strContainerDeposit = clsERPFuncationality.ChangeGLAccountLocationSegment(strContainerDeposit, obj.Location, trans)
            Dim Acc() As String = {strContainerDeposit, (obj.Empty_Value)}
            ArryLstGLAC.Add(Acc)
        End If


        If obj.Inv_Detail_Total_Amt > 0 And obj.Total_Disc_Percent <> 100 Then
            For Each objItemWiseGLAC As clsJournalDetailTemp In ArrItemWiseGLAC
                If objItemWiseGLAC.Amount <> 0 Then
                    Dim Acc1() As String = {objItemWiseGLAC.Account_code, objItemWiseGLAC.Amount}
                    ArryLstGLAC.Add(Acc1)
                End If
            Next
            ''Dim strSaleAccountSet As String = clsCommon.myCstr(dtSaleACSet.Rows(0)("Sales_Account"))
            ''If clsCommon.myLen(strSaleAccountSet) <= 0 Then
            ''    Throw New Exception("Please set Sale Account of Sale Account set")
            ''End If
            ''strSaleAccountSet = clsERPFuncationality.ChangeGLAccountLocationSegment(strSaleAccountSet, obj.Location, trans)
            ''Dim Acc1() As String = {strSaleAccountSet, -1 * (IIf(isLocationExcisable, obj.Inv_Detail_Total_Amt, (obj.Inv_Detail_Total_Amt - dblExcise - dblCess - dblHECess)))}
            ''ArryLstGLAC.Add(Acc1)
        End If


        Dim objTM As clsTaxMaster
        If Not isLocationExcisable Then
            ''for normal Sale
            If dblExcise > 0 And obj.Total_Disc_Percent <> 100 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale("BED", trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX1)
                    End If
                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblExcise)}
                    ArryLstGLAC.Add(Acc1)

                    If dblShipmentTotCost > 0 Then
                        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                            Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX1)
                        End If
                        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                        Dim Acc() As String = {objTM.Tax_Net_Payable, (dblExcise + dblExciseFOC)}
                        ArryLstGLAC.Add(Acc)
                    End If
                End If
            End If

            'If obj.Total_Disc_Percent = 100 Then
            '    If dblExciseFOC > 0 Then
            '        objTM = clsTaxMaster.GetTaxDetailsForSale("BED", trans)
            '        If objTM IsNot Nothing Then
            '            If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
            '                Throw New Exception("Please set Tax Net Payable Account of Tax Autherity " + obj.TAX1)
            '            End If

            '            objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
            '            Dim Acc2() As String = {objTM.Tax_Net_Payable, (dblExciseFOC)}
            '            ArryLstGLAC.Add(Acc2)
            '        End If
            '    End If
            'End If

            If dblCess > 0 And obj.Total_Disc_Percent <> 100 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale("ECESS", trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX1)
                    End If
                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblCess)}
                    ArryLstGLAC.Add(Acc1)
                    If dblShipmentTotCost > 0 Then
                        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                            Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX1)
                        End If
                        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                        Dim Acc() As String = {objTM.Tax_Net_Payable, (dblCess + dblCessFOC)}
                        ArryLstGLAC.Add(Acc)
                    End If
                End If
            End If

            'If obj.Total_Disc_Percent = 100 Then
            '    objTM = clsTaxMaster.GetTaxDetailsForSale("ECESS", trans)
            '    If objTM IsNot Nothing Then
            '        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
            '            Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX1)
            '        End If
            '        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
            '        Dim Acc3() As String = {objTM.Tax_Net_Payable, (dblCessFOC)}
            '        ArryLstGLAC.Add(Acc3)
            '    End If
            'End If

            If dblHECess > 0 And obj.Total_Disc_Percent <> 100 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale("HCESS", trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX1)
                    End If
                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblHECess)}
                    ArryLstGLAC.Add(Acc1)
                    If dblShipmentTotCost > 0 Then
                        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                            Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX1)
                        End If
                        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                        Dim Acc() As String = {objTM.Tax_Net_Payable, (dblHECess + dblHECessFOC)}
                        ArryLstGLAC.Add(Acc)
                    End If
                End If
            End If

            'If obj.Total_Disc_Percent = 100 Then
            '    objTM = clsTaxMaster.GetTaxDetailsForSale("HCESS", trans)
            '    If objTM IsNot Nothing Then
            '        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
            '            Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX1)
            '        End If
            '        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
            '        Dim Acc4() As String = {objTM.Tax_Net_Payable, (dblHECessFOC)}
            '        ArryLstGLAC.Add(Acc4)
            '    End If
            'End If
        End If

        If dblTotTaxAmt1 > 0 Then
            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX1, trans)
            If objTM IsNot Nothing Then
                If Not (obj.Total_Disc_Percent = 100 AndAlso Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal) Then
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                        If obj.Total_Disc_Percent <> 100 Then
                            If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                                Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX1)
                            End If
                            objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                            Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt1 - dblTotTaxAmtFOC1)}
                            ArryLstGLAC.Add(Acc1)
                        End If

                        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                            Throw New Exception("Please set Tax Net Payale Account of Tax Autherity " + obj.TAX1)
                        End If
                        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                        Dim Acc2() As String = {objTM.Tax_Net_Payable, dblTotTaxAmt1}
                        ArryLstGLAC.Add(Acc2)
                    End If

                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX1)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, obj.Location, trans)
                    Dim Acc() As String = {objTM.Tax_Liability_Account, -1 * (dblTotTaxAmt1)}
                    ArryLstGLAC.Add(Acc)
                End If
            End If
        End If

        If dblTotTaxAmt2 > 0 Then
            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX2, trans)
            If objTM IsNot Nothing Then
                If Not (obj.Total_Disc_Percent = 100 AndAlso Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal) Then
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                        If obj.Total_Disc_Percent <> 100 Then
                            If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                                Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX2)
                            End If
                            objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                            Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt2 - dblTotTaxAmtFOC2)}
                            ArryLstGLAC.Add(Acc1)
                        End If

                        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                            Throw New Exception("Please set Tax Net Payale Account of Tax Autherity " + obj.TAX2)
                        End If
                        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                        Dim Acc2() As String = {objTM.Tax_Net_Payable, dblTotTaxAmt2}
                        ArryLstGLAC.Add(Acc2)
                    End If

                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX2)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, obj.Location, trans)
                    Dim Acc() As String = {objTM.Tax_Liability_Account, -1 * (dblTotTaxAmt2)}
                    ArryLstGLAC.Add(Acc)
                End If
            End If
        End If
        If dblTotTaxAmt3 > 0 Then
            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX3, trans)
            If objTM IsNot Nothing Then
                If Not (obj.Total_Disc_Percent = 100 AndAlso Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal) Then
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX3)
                    End If
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                        If obj.Total_Disc_Percent <> 100 Then
                            If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                                Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX3)
                            End If
                            objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                            Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt3 - dblTotTaxAmtFOC3)}
                            ArryLstGLAC.Add(Acc1)
                        End If

                        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                            Throw New Exception("Please set Tax Net Payale Account of Tax Autherity " + obj.TAX3)
                        End If
                        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                        Dim Acc2() As String = {objTM.Tax_Net_Payable, dblTotTaxAmt3}
                        ArryLstGLAC.Add(Acc2)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, obj.Location, trans)
                    Dim Acc() As String = {objTM.Tax_Liability_Account, -1 * (dblTotTaxAmt3)}
                    ArryLstGLAC.Add(Acc)
                End If
            End If
        End If

        If dblTotTaxAmt4 > 0 Then
            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX4, trans)
            If objTM IsNot Nothing Then
                If Not (obj.Total_Disc_Percent = 100 AndAlso Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal) Then
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                        If obj.Total_Disc_Percent <> 100 Then
                            If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                                Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX4)
                            End If
                            objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                            Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt4 - dblTotTaxAmtFOC4)}
                            ArryLstGLAC.Add(Acc1)
                        End If
                        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                            Throw New Exception("Please set Tax  Net Payale Account of Tax Autherity " + obj.TAX4)
                        End If
                        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                        Dim Acc2() As String = {objTM.Tax_Net_Payable, dblTotTaxAmt4}
                        ArryLstGLAC.Add(Acc2)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX4)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, obj.Location, trans)
                    Dim Acc() As String = {objTM.Tax_Liability_Account, -1 * (dblTotTaxAmt4)}
                    ArryLstGLAC.Add(Acc)
                End If
            End If
        End If
        If dblTotTaxAmt5 > 0 Then
            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX5, trans)
            If objTM IsNot Nothing Then
                If Not (obj.Total_Disc_Percent = 100 AndAlso Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal) Then
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                        If obj.Total_Disc_Percent <> 100 Then
                            If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                                Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX5)
                            End If
                            objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                            Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt5 - dblTotTaxAmtFOC5)}
                            ArryLstGLAC.Add(Acc1)
                        End If
                        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                            Throw New Exception("Please set Tax  Net Payale Account of Tax Autherity " + obj.TAX5)
                        End If
                        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                        Dim Acc2() As String = {objTM.Tax_Net_Payable, (dblTotTaxAmt5)}
                        ArryLstGLAC.Add(Acc2)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX5)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, obj.Location, trans)
                    Dim Acc() As String = {objTM.Tax_Liability_Account, -1 * (dblTotTaxAmt5)}
                    ArryLstGLAC.Add(Acc)
                End If
            End If
        End If
        If dblTotTaxAmt6 > 0 Then
            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX6, trans)
            If objTM IsNot Nothing Then
                If Not (obj.Total_Disc_Percent = 100 AndAlso Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal) Then
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                        If obj.Total_Disc_Percent <> 100 Then
                            If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                                Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX6)
                            End If
                            objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                            Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt6 - dblTotTaxAmtFOC6)}
                            ArryLstGLAC.Add(Acc1)
                        End If

                        If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                            Throw New Exception("Please set Tax Net Payale Account of Tax Autherity " + obj.TAX6)
                        End If
                        objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                        Dim Acc2() As String = {objTM.Tax_Net_Payable, dblTotTaxAmt6}
                        ArryLstGLAC.Add(Acc2)
                    End If
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX6)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, obj.Location, trans)
                    Dim Acc() As String = {objTM.Tax_Liability_Account, -1 * (dblTotTaxAmt6)}
                    ArryLstGLAC.Add(Acc)
                End If
            End If
        End If




        If dblTotTPT > 0 And obj.Total_Disc_Percent <> 100 Then
            qry = "select Price_Comp_Account_Code from TSPL_PRICE_COMPONENT_MASTER where TPT_Type='Y'"
            Dim strPriceCompAccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strPriceCompAccount) <= 0 Then
                Throw New Exception("Please set Price Component Account ")
            End If
            strPriceCompAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strPriceCompAccount, obj.Location, trans)
            Dim Acc() As String = {strPriceCompAccount, -1 * dblTotTPT}
            ArryLstGLAC.Add(Acc)
        End If

        ''qry = "select SUM(Shipped_Qty*Unit_COGS) from TSPL_SHIPMENT_DETAILS where Shipment_No='" + obj.Shipment_No + "'"
        ''Dim dblShipmentTotCost As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        If dblShipmentTotCost > 0 Then
            qry = "select Shipment_Clearing from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code='" + strFirstItemPurchaseAccountSet + "'"
            strShipmentClearingAC = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(strShipmentClearingAC) <= 0 Then
                Throw New Exception("Please set Shipment Clearing account of Purchase set " + strFirstItemPurchaseAccountSet)
            End If
            strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Location, trans)
            Dim Acc() As String = {strShipmentClearingAC, -1 * dblShipmentTotCost}
            ArryLstGLAC.Add(Acc)


            If dblShipmentTotCost - dblSchemeTotAmt - dblSamplingTotAmt > 0 Then
                strCogsAccount = clsCommon.myCstr(dtSaleACSet.Rows(0)("Cost_Of_Goods_Sold"))
                If clsCommon.myLen(strCogsAccount) <= 0 Then
                    Throw New Exception("Please set cost of Goods Sole account of Sale Account set")
                End If
                strCogsAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strCogsAccount, obj.Location, trans)
                Dim Acc1() As String = {strCogsAccount, IIf(isLocationExcisable, (dblShipmentTotCost - dblSchemeTotAmt - dblSamplingTotAmt), dblShipmentTotCost - dblSchemeTotAmt - dblSamplingTotAmt - dblExcise - dblCess - dblHECess)}
                ArryLstGLAC.Add(Acc1)
            End If
        End If

        If obj.Empty_Value > 0 Then
            Dim strContainerDepositSaleAccount As String = clsCommon.myCstr(dtSaleACSet.Rows(0)("Returnable_Container"))
            If clsCommon.myLen(strContainerDepositSaleAccount) <= 0 Then
                Throw New Exception("Please set Debtor Control Account of Customer Account set")
            End If
            strContainerDepositSaleAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strContainerDepositSaleAccount, obj.Location, trans)
            Dim Acc1() As String = {strContainerDepositSaleAccount, -1 * (obj.Empty_Value)}
            ArryLstGLAC.Add(Acc1)
        End If
        If dblSchemeTotAmt > 0 Then
            Dim strSchemeCtrlAccount As String = clsCommon.myCstr(dtSaleACSet.Rows(0)("Schemes"))
            If clsCommon.myLen(strSchemeCtrlAccount) <= 0 Then
                Throw New Exception("Please set Debtor Control Account of Customer Account set")
            End If
            strSchemeCtrlAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strSchemeCtrlAccount, obj.Location, trans)
            Dim Acc1() As String = {strSchemeCtrlAccount, dblSchemeTotAmt - dblExciseFOC - dblCessFOC - dblHECessFOC}
            ''Dim Acc1() As String = {strSchemeCtrlAccount, dblSchemeTotAmt}
            ArryLstGLAC.Add(Acc1)
        End If
        If dblSamplingTotAmt > 0 Then
            qry = "select Account_Code from TSPL_Sampling_Master where Sampling_Code='" + obj.Scheme_Sample_Code + "'"
            Dim strSamplingCtrlAccount As String = clsDBFuncationality.getSingleValue(qry, trans)
            If clsCommon.myLen(strSamplingCtrlAccount) <= 0 Then
                Throw New Exception("Please set Scheme Sample codr for Sample" + obj.Scheme_Sample_Code)
            End If
            strSamplingCtrlAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strSamplingCtrlAccount, obj.Location, trans)
            Dim Acc1() As String = {strSamplingCtrlAccount, dblSamplingTotAmt - dblCessFOC - dblExciseFOC - dblHECessFOC}
            ArryLstGLAC.Add(Acc1)
        End If

        ''Adjust  Shipment Clearing AC and Cost of Goods Sold if Out of Balance upto RS 1
        If clsCommon.myLen(strShipmentClearingAC) > 0 AndAlso clsCommon.myLen(strCogsAccount) > 0 Then
            ''Dim adsf() As String = {"100001-LWR", 0.7}
            ''ArryLstGLAC.Add(adsf)

            Dim dblTotDrAmt As Decimal = 0
            Dim dblTotCrAmt As Decimal = 0
            For ii As Integer = 0 To ArryLstGLAC.Count - 1
                If ArryLstGLAC(ii)(1) > 0 Then
                    dblTotDrAmt += Math.Round(clsCommon.myCdbl(ArryLstGLAC(ii)(1)), 2, MidpointRounding.AwayFromZero)
                Else
                    dblTotCrAmt += -1 * Math.Round(clsCommon.myCdbl(ArryLstGLAC(ii)(1)), 2, MidpointRounding.AwayFromZero)
                End If
            Next
            Dim dblDiffence As Decimal = Math.Round(clsCommon.myCdbl(dblTotDrAmt - dblTotCrAmt), 2, MidpointRounding.AwayFromZero)

            If Not Math.Abs(dblDiffence) = 0.0 Then
                If dblTotCrAmt > dblTotDrAmt Then
                    ''Adjust in Debit Amount and account cost of goods sold is Increased
                    For ii As Integer = 0 To ArryLstGLAC.Count - 1
                        If clsCommon.CompairString(ArryLstGLAC(ii)(0), strCogsAccount) = CompairStringResult.Equal Then
                            ArryLstGLAC(ii)(1) += (-1 * dblDiffence)
                        End If
                    Next
                Else
                    ''Adjust in Credit Amount and account  Shipment Clearing Is Increased
                    For ii As Integer = 0 To ArryLstGLAC.Count - 1
                        If clsCommon.CompairString(ArryLstGLAC(ii)(0), strShipmentClearingAC) = CompairStringResult.Equal Then
                            ArryLstGLAC(ii)(1) += (-1 * dblDiffence)
                        End If
                    Next
                End If
            End If
        End If
        ''End of Adjust  Shipment Clearing AC and Cost of Goods Sold if Out of Balance upto RS 1 

        transportSql.FunGrnlEntryWithTrans(obj.Location, False, trans, obj.Sale_Invoice_Date, "Sale against Invoice No   " + obj.Sale_Invoice_No, "SD-IN", "INVOICE", obj.Sale_Invoice_No, obj.Description, "C", obj.Cust_Code, obj.Cust_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, "", "Shipment No " + obj.Shipment_No + " for customer " + obj.Cust_Name + "")
        Return True
    End Function

    Private Shared Function GetRecoverableAmt(ByVal obj As clsShipmentMaster, ByRef dblTaxRecAmt As Double, ByRef dblTaxRecAmt2 As Double, ByRef dblTaxRecAmt3 As Double) As Double
        Dim dblTotDisPer As Double = 0
        If isAllTargetItem(obj) Then
            dblTotDisPer = 100
        Else
            Dim temp As Double = 0
            If obj.Shipment_Disc_Percent < 100 Then
                temp = obj.Shipment_Discount_Amt + obj.Shipment_Detail_Total_Amt
            ElseIf obj.Shipment_Disc_Percent = 100 Then
                temp = obj.Shipment_Discount_Amt
            End If

            If temp = 0 Then
                dblTotDisPer = 0
            Else
                dblTotDisPer = (clsCommon.myCdbl(obj.Tot_Customer_Dis_Amt) + clsCommon.myCdbl(obj.Shipment_Discount_Amt)) * 100 / temp
            End If
        End If

        dblTotDisPer = Math.Round(dblTotDisPer, 2, MidpointRounding.ToEven)
        Dim dblTax1Amt As Double = 0
        Dim dblTax2Amt As Double = 0
        Dim dblTax3Amt As Double = 0
        For Each objtr As clsShipmentDetail In obj.Arr
            dblTax1Amt += clsCommon.myCdbl(objtr.TAX1_Amt) * clsCommon.myCdbl(objtr.Shipped_Qty)
            dblTax2Amt += clsCommon.myCdbl(objtr.TAX2_Amt) * clsCommon.myCdbl(objtr.Shipped_Qty)
            dblTax3Amt += clsCommon.myCdbl(objtr.TAX3_Amt) * clsCommon.myCdbl(objtr.Shipped_Qty)
        Next
        dblTaxRecAmt = dblTax1Amt - ((dblTotDisPer * dblTax1Amt) / 100)
        dblTaxRecAmt = Math.Round(dblTaxRecAmt, 2, MidpointRounding.ToEven)

        dblTaxRecAmt2 = dblTax2Amt - ((dblTotDisPer * dblTax2Amt) / 100)
        dblTaxRecAmt2 = Math.Round(dblTaxRecAmt2, 2, MidpointRounding.ToEven)

        dblTaxRecAmt3 = dblTax3Amt - ((dblTotDisPer * dblTax3Amt) / 100)
        dblTaxRecAmt3 = Math.Round(dblTaxRecAmt3, 2, MidpointRounding.ToEven)

        Return dblTotDisPer
    End Function

    Private Shared Function isAllTargetItem(ByVal obj As clsShipmentMaster) As Boolean
        For Each objtr As clsShipmentDetail In obj.Arr
            If objtr.Shipped_Qty > 0 Then
                If clsCommon.myLen(objtr.Discount_Code) <= 0 Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    'Public Shared Function SetTempProvisionSale() As Boolean
    '    clsCommon.ProgressBarShow()
    '    Dim isSaved As Boolean = True
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        Dim arrDB As List(Of String) = New List(Of String)
    '        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                Dim strDB As String = clsCommon.myCstr(dr("DataBase_Name"))
    '                arrDB.Add(strDB)
    '            Next
    '        End If
    '        ''Step1
    '        qry = "delete from TEMP_PROVISIONAL_SALES"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

    '        ''Step 2  query to insert load out details
    '        qry = "Insert into TEMP_PROVISIONAL_SALES(Comp_Code,CE,ADC,TDM,HOS,LoadIn_EmptyValue,LoadOut_EmptyValue,Loadout_Amount,RouteNo,Transfer_No,Transfer_Date,Vehicle_Code,LoadOut_Location,LoadIn_Location,Salesmancode,Emp_Name,Item_Code,Item_Desc,LoadOutQty,LoadInQty,Conversion_Factor,LoadIn_No,Breakage,Leak,MRP,Unit_Code,Vehicle_No,Pack_Code,Flavour_Code,Post) select a.Comp_code,CE,ADC,TDM,HOS,0,b.Empty_Value * b.Item_Qty,b.Item_Qty * (BasicPrice_WithTax + TPT_Value + Empty_Value),Route_No,a.Transfer_No,a.Transfer_Date,Vehicle_Code,From_Location,'',Salesmancode,c.Emp_Name,b.Item_Code,Item_Desc,Item_Qty,0,Conversion_Factor,'',Breakage,Leak,MRP,UOM_Code,Vehicle_No,pack.Class_Code,fl.Class_Code,case when Post='Y' then 'Y' else 'N' end as Post from TSPL_TRANSFER_HEAD a inner join TSPL_TRANSFER_DETAIL b on a.Transfer_No=b.Transfer_No inner join TSPL_LOCATION_MASTER on a.To_Location=TSPL_LOCATION_MASTER.Location_Code inner join TSPL_EMPLOYEE_MASTER c on a.Salesmancode=c.EMP_CODE inner join TSPL_ITEM_UOM_DETAIL d on b.Item_Code=d.Item_Code and b.Uom=d.UOM_Code inner join TSPL_ITEM_DETAILS fl on b.Item_Code=fl.Item_Code inner join TSPL_ITEM_DETAILS pack on b.Item_Code=pack.Item_Code   where Transfer_Type='LO' and Location_Type='Logical' and fl.Class_Name='Flavour' and pack.Class_Name='size' and Uom <> 'sh' and a.Transfer_No not in (select transfer_no from TEMP_PROVISIONAL_SALES)"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)


    '        ''3rd query to insert loadin details
    '        qry = "update TEMP_PROVISIONAL_SALES set LoadInQty  = ( select isnull(SUM(loadIn ),0) from (select (case when Uom='FB' then  LoadIn_Qty/(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom   ) when Uom='SH' then 0 else LoadIn_Qty end) as loadIn ,H.Load_Out_No,L.Item_Code     from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No   ) as xxx where xxx.Load_Out_No =TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=TEMP_PROVISIONAL_SALES.Item_Code)  , Leak = ( select isnull(SUM(loadIn ),0) from (select (case when Uom='FB' then  Leak /(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom  ) when Uom='SH' then 0 else Leak  end) as loadIn ,H.Load_Out_No,L.Item_Code     from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No   ) as xxx where xxx.Load_Out_No =TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=TEMP_PROVISIONAL_SALES.Item_Code) , Shortage=( select isnull(SUM(loadIn ),0) from (select (case when Uom='FB' then  Shortage /(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom  ) when Uom='SH' then 0 else Shortage  end) as loadIn ,H.Load_Out_No,L.Item_Code   from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No   ) as xxx where xxx.Load_Out_No =TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=TEMP_PROVISIONAL_SALES.Item_Code) ,Breakage =( select isnull(SUM(loadIn ),0) from (select (case when Uom='FB' then  Burst /(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom  ) when Uom='SH' then 0 else Burst  end) as loadIn ,H.Load_Out_No,L.Item_Code     from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No  ) as xxx where xxx.Load_Out_No =TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=TEMP_PROVISIONAL_SALES.Item_Code) ,  Amount =( select isnull(SUM(amt),0) from (select  ((LoadIn_Qty +Leak+Burst+Shortage )*(BasicPrice_WithTax+TPT_Value+Empty_Value )) as amt ,L.Item_Code ,H.Load_Out_No    from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No  ) as xxx where xxx.Load_Out_No =TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=TEMP_PROVISIONAL_SALES.Item_Code )"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)


    '        ''4th query Update LoadIn Empty Value
    '        qry = "update TEMP_PROVISIONAL_SALES set LoadIn_EmptyValue = ( select isnull(SUM(amt),0) from (select  ((LoadIn_Qty +Leak+Burst+Shortage )*(Empty_Value )) as amt ,L.Item_Code ,H.Load_Out_No    from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No  ) as xxx where xxx.Load_Out_No =TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=TEMP_PROVISIONAL_SALES.Item_Code )"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

    '        ''5th query shell entry
    '        qry = "with  CTE(Load_Out_No,Transfer_No)as (select distinct H.Load_Out_No,H.Transfer_No    from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No =H.Transfer_No where  len(H.Load_Out_No)>0 and  H.Transfer_Type='LI' and L.Uom ='SH' and h.Load_Out_No   not in (select Transfer_No    from TEMP_PROVISIONAL_SALES where Unit_Code ='SH'))insert into TEMP_PROVISIONAL_SALES(Transfer_No, Transfer_Date, Vehicle_Code, LoadOut_Location, LoadIn_Location, Salesmancode, Emp_Name, Item_Code, Item_Desc, LoadOutQty, LoadInQty,  Conversion_Factor, LoadIn_No, Breakage, Leak, MRP, Unit_Code, Vehicle_No, Pack_Code, Flavour_Code, Amount, Shortage,RouteNo) select   H.Load_Out_No,H.Transfer_Date,H.Vehicle_Code,H.To_Location,'',H.Salesmancode,H.FromLoc_Desc,l.Item_Code,L.Item_Desc,0,0,0,'',0,0,L.MRP,L.Uom,H.Vehicle_No ,(select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= L.Item_Code and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')),(select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= L.Item_Code and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')),(L.LoadIn_Qty *(L.BasicPrice_WithTax+L.TPT_Value+L.Empty_Value )),0,Route_No  from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No inner join CTE  on H.Transfer_No =CTE .Transfer_No  where L.Uom  ='SH'"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)


    '        '6th query shell entry
    '        qry = "with CTE(Load_Out_No,Transfer_No) as ( select distinct H.Load_Out_No,H.Transfer_No from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No =H.Transfer_No where  len(H.Load_Out_No)>0 and  H.Transfer_Type='LI' and L.Uom ='SH' and h.Load_Out_No   not in (select Transfer_No    from TEMP_PROVISIONAL_SALES where Unit_Code ='SH'))insert into TEMP_PROVISIONAL_SALES(Transfer_No, Transfer_Date, Vehicle_Code, LoadOut_Location, LoadIn_Location, Salesmancode, Emp_Name, Item_Code, Item_Desc, LoadOutQty, LoadInQty, Conversion_Factor, LoadIn_No, Breakage, Leak, MRP, Unit_Code, Vehicle_No, Pack_Code, Flavour_Code, Amount, Shortage) select   H.Load_Out_No,H.Transfer_Date,H.Vehicle_Code,H.To_Location,'',H.Salesmancode,H.FromLoc_Desc,l.Item_Code,L.Item_Desc,0,0,0,'',0,0,L.MRP,L.Uom,H.Vehicle_No ,(select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= L.Item_Code and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')),(select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= L.Item_Code and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')),(L.LoadIn_Qty *(L.BasicPrice_WithTax+L.TPT_Value+L.Empty_Value )),0  from TSPL_TRANSFER_DETAIL L inner join TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No inner join CTE  on H.Transfer_No =CTE .Transfer_No  where L.Uom  ='SH'"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

    '        '7th 
    '        qry = "update TEMP_PROVISIONAL_SALES set RouteNo=(select Route_No from tspl_transfer_head where tspl_transfer_head.Transfer_No =TEMP_PROVISIONAL_SALES.Transfer_No ) where RouteNo is null"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

    '        '8th
    '        qry = "update TEMP_PROVISIONAL_SALES set Route_Type_Id=(select Route_Type_Id  from TSPL_ROUTE_MASTER a inner join TSPL_ROUTE_TYPE b on a.Type=b.Route_Type_Id where a.Route_No=TEMP_PROVISIONAL_SALES.RouteNo)"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

    '        '9th
    '        qry = "update tspl_sale_invoice_head set Route_Type_Id=(select Route_Type_Id  from TSPL_ROUTE_MASTER a inner join TSPL_ROUTE_TYPE b on a.Type=b.Route_Type_Id where a.Route_No=tspl_sale_invoice_head.Route_No)"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)


    '        If isSaved Then
    '            trans.Commit()
    '            clsCommon.ProgressBarHide()

    '        End If

    '    Catch ex As Exception
    '        clsCommon.ProgressBarHide()
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    Finally
    '        clsCommon.ProgressBarHide()
    '    End Try
    'End Function


    'Public Shared Function SetUpdateRouteTypeID() As Boolean

    '    'clsCommon.ProgressBarShow()
    '    Dim isSaved As Boolean = True
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        Dim arrDB As List(Of String) = New List(Of String)
    '        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                Dim strDB As String = clsCommon.myCstr(dr("DataBase_Name"))
    '                arrDB.Add(strDB)
    '            Next
    '        End If


    '        '7th 
    '        qry = "update TEMP_PROVISIONAL_SALES set RouteNo=(select Route_No from tspl_transfer_head where tspl_transfer_head.Transfer_No =TEMP_PROVISIONAL_SALES.Transfer_No ) where RouteNo is null"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

    '        '8th
    '        qry = "update TEMP_PROVISIONAL_SALES set Route_Type_Id=(select Route_Type_Id  from TSPL_ROUTE_MASTER a inner join TSPL_ROUTE_TYPE b on a.Type=b.Route_Type_Id where a.Route_No=TEMP_PROVISIONAL_SALES.RouteNo) where Route_Type_Id is null"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

    '        '9th
    '        qry = "update tspl_sale_invoice_head set Route_Type_Id=(select Route_Type_Id  from TSPL_ROUTE_MASTER a inner join TSPL_ROUTE_TYPE b on a.Type=b.Route_Type_Id where a.Route_No=tspl_sale_invoice_head.Route_No) where Route_Type_Id is null"
    '        isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)


    '        If isSaved Then
    '            trans.Commit()
    '            clsCommon.ProgressBarHide()

    '        End If

    '    Catch ex As Exception
    '        clsCommon.ProgressBarHide()
    '        trans.Rollback()

    '    Finally
    '        clsCommon.ProgressBarHide()
    '    End Try
    'End Function


    Public Shared Function SetTempProvisionSale() As Boolean

        clsCommon.ProgressBarShow()
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim arrDB As List(Of String) = New List(Of String)
            Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim strDB As String = clsCommon.myCstr(dr("DataBase_Name"))
                    arrDB.Add(strDB)
                Next
            End If
            ''Step1
            'qry = "delete from " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES"
            'isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

            ''Step 2  query to insert load out details
            'qry = "Insert into " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES(Comp_Code,CE,ADC,TDM,HOS,LoadIn_EmptyValue,LoadOut_EmptyValue,Loadout_Amount,RouteNo,Transfer_No,Transfer_Date,Vehicle_Code,LoadOut_Location,LoadIn_Location,Salesmancode,Emp_Name,Item_Code,Item_Desc,LoadOutQty,LoadInQty,Conversion_Factor,LoadIn_No,Breakage,Leak,MRP,Unit_Code,Vehicle_No,Pack_Code,Flavour_Code,Post) select a.Comp_code,CE,ADC,TDM,HOS,0,b.Empty_Value * b.Item_Qty,b.Item_Qty * (BasicPrice_WithTax + TPT_Value + Empty_Value),Route_No,a.Transfer_No,a.Transfer_Date,Vehicle_Code,From_Location,'',Salesmancode,c.Emp_Name,b.Item_Code,Item_Desc,Item_Qty,0,Conversion_Factor,'',Breakage,Leak,MRP,UOM_Code,Vehicle_No,pack.Class_Code,fl.Class_Code,case when Post='Y' then 'Y' else 'N' end as Post from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD a inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL b on a.Transfer_No=b.Transfer_No inner join " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on a.To_Location=" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code inner join " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER c on a.Salesmancode=c.EMP_CODE inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL d on b.Item_Code=d.Item_Code and b.Uom=d.UOM_Code inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS fl on b.Item_Code=fl.Item_Code inner join " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS pack on b.Item_Code=pack.Item_Code   where Transfer_Type='LO' and Location_Type='Logical' and fl.Class_Name='Flavour' and pack.Class_Name='size' and Uom <> 'sh' and a.Transfer_No not in (select transfer_no from " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES)"
            'isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)


            ''3rd query to insert loadin details
            qry = "update " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES set LoadInQty  = ( select isnull(SUM(loadIn ),0) from (select (case when Uom='FB' then  LoadIn_Qty/(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom   ) when Uom='SH' then 0 else LoadIn_Qty end) as loadIn ,H.Load_Out_No,L.Item_Code,L.MRP * (select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom ) as mrp      from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL L inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No   ) as xxx where xxx.Load_Out_No =" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code and xxx.MRP=TEMP_PROVISIONAL_SALES.MRP)  , " & _
            "Leak = ( select isnull(SUM(loadIn ),0) from (select (case when Uom='FB' then  Leak /(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom  ) when Uom='SH' then 0 else Leak  end) as loadIn ,H.Load_Out_No,L.Item_Code,L.MRP * (select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom ) as mrp      from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL L inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No   ) as xxx where xxx.Load_Out_No =" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code and xxx.MRP=TEMP_PROVISIONAL_SALES.MRP)  , " & _
            "Shortage=( select isnull(SUM(loadIn ),0) from (select (case when Uom='FB' then  Shortage /(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom  ) when Uom='SH' then 0 else Shortage  end) as loadIn ,H.Load_Out_No,L.Item_Code,L.MRP * (select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom ) as mrp      from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL L inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No   ) as xxx where xxx.Load_Out_No =" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code and xxx.MRP=TEMP_PROVISIONAL_SALES.MRP)  , " & _
            "Breakage =( select isnull(SUM(loadIn ),0) from (select (case when Uom='FB' then  Burst /(select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom  ) when Uom='SH' then 0 else Burst  end) as loadIn ,H.Load_Out_No,L.Item_Code,L.MRP * (select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom ) as mrp      from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL L inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No   ) as xxx where xxx.Load_Out_No =" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code and xxx.MRP=TEMP_PROVISIONAL_SALES.MRP)  , " & _
            "Amount =( select isnull(SUM(amt),0) from (select  ((LoadIn_Qty +Leak+Burst+Shortage )*(BasicPrice_WithTax+TPT_Value+Empty_Value )) as amt ,L.Item_Code ,H.Load_Out_No,L.MRP * (select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom ) as mrp      from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL L inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No   ) as xxx where xxx.Load_Out_No =" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code and xxx.MRP=TEMP_PROVISIONAL_SALES.MRP)  "
            isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)


            ''4th query Update LoadIn Empty Value
            'qry = "update " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES set LoadIn_EmptyValue = ( select isnull(SUM(amt),0) from (select  ((LoadIn_Qty +Leak+Burst+Shortage )*(Empty_Value )) as amt ,L.Item_Code ,H.Load_Out_No,L.MRP * (select Conversion_Factor  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code=L.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code =Uom ) as mrp      from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL L inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No   ) as xxx where xxx.Load_Out_No =" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No and xxx.Item_Code=" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Item_Code and xxx.MRP=TEMP_PROVISIONAL_SALES.MRP)   "
            'isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

            ''5th query shell entry
            'qry = "with  CTE(Load_Out_No,Transfer_No)as (select distinct H.Load_Out_No,H.Transfer_No    from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL L inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H on L.Transfer_No =H.Transfer_No where  len(H.Load_Out_No)>0 and  H.Transfer_Type='LI' and L.Uom ='SH' and h.Load_Out_No   not in (select Transfer_No    from " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES where Unit_Code ='SH'))insert into " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES(Transfer_No, Transfer_Date, Vehicle_Code, LoadOut_Location, LoadIn_Location, Salesmancode, Emp_Name, Item_Code, Item_Desc, LoadOutQty, LoadInQty,  Conversion_Factor, LoadIn_No, Breakage, Leak, MRP, Unit_Code, Vehicle_No, Pack_Code, Flavour_Code, Amount, Shortage,RouteNo) select   H.Load_Out_No,H.Transfer_Date,H.Vehicle_Code,H.To_Location,'',H.Salesmancode,H.FromLoc_Desc,l.Item_Code,L.Item_Desc,0,0,0,'',0,0,L.MRP,L.Uom,H.Vehicle_No ,(select Class_Code  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS where Item_Code= L.Item_Code and Class_Name = (select inv_class_name from " + clsCommon.ReplicateDBString + "TSPL_INV_CLASS where Class_Type = 'Size Type')),(select Class_Code  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS where Item_Code= L.Item_Code and Class_Name = (select inv_class_name from " + clsCommon.ReplicateDBString + "TSPL_INV_CLASS where Class_Type = 'Flavour Type')),(L.LoadIn_Qty *(L.BasicPrice_WithTax+L.TPT_Value+L.Empty_Value )),0,Route_No  from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL L inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No inner join CTE  on H.Transfer_No =CTE .Transfer_No  where L.Uom  ='SH'"
            'isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)


            '6th query shell entry
            'qry = "with CTE(Load_Out_No,Transfer_No) as ( select distinct H.Load_Out_No,H.Transfer_No from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL L inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H on L.Transfer_No =H.Transfer_No where  len(H.Load_Out_No)>0 and  H.Transfer_Type='LI' and L.Uom ='SH' and h.Load_Out_No   not in (select Transfer_No    from " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES where Unit_Code ='SH'))insert into " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES(Transfer_No, Transfer_Date, Vehicle_Code, LoadOut_Location, LoadIn_Location, Salesmancode, Emp_Name, Item_Code, Item_Desc, LoadOutQty, LoadInQty, Conversion_Factor, LoadIn_No, Breakage, Leak, MRP, Unit_Code, Vehicle_No, Pack_Code, Flavour_Code, Amount, Shortage) select   H.Load_Out_No,H.Transfer_Date,H.Vehicle_Code,H.To_Location,'',H.Salesmancode,H.FromLoc_Desc,l.Item_Code,L.Item_Desc,0,0,0,'',0,0,L.MRP,L.Uom,H.Vehicle_No ,(select Class_Code  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS where Item_Code= L.Item_Code and Class_Name = (select inv_class_name from " + clsCommon.ReplicateDBString + "TSPL_INV_CLASS where Class_Type = 'Size Type')),(select Class_Code  from " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS where Item_Code= L.Item_Code and Class_Name = (select inv_class_name from " + clsCommon.ReplicateDBString + "TSPL_INV_CLASS where Class_Type = 'Flavour Type')),(L.LoadIn_Qty *(L.BasicPrice_WithTax+L.TPT_Value+L.Empty_Value )),0  from " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL L inner join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD H on L.Transfer_No=H.Transfer_No inner join CTE  on H.Transfer_No =CTE .Transfer_No  where L.Uom  ='SH'"
            'isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

            '7th 
            'qry = "update " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES set RouteNo=(select Route_No from " + clsCommon.ReplicateDBString + "tspl_transfer_head where " + clsCommon.ReplicateDBString + "tspl_transfer_head.Transfer_No =" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No ) where RouteNo is null"
            'isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

            '8th
            'qry = "update " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES set Route_Type_Id=(select Route_Type_Id  from " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER a inner join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE b on a.Type=b.Route_Type_Id where a.Route_No=" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.RouteNo)"
            'isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

            ''9th
            'qry = "update " + clsCommon.ReplicateDBString + "tspl_sale_invoice_head set Route_Type_Id=(select Route_Type_Id  from " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER a inner join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE b on a.Type=b.Route_Type_Id where a.Route_No=" + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Route_No)"
            'isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)


            If isSaved Then
                trans.Commit()
                clsCommon.ProgressBarHide()

            End If

        Catch ex As Exception
            clsCommon.ProgressBarHide()
            trans.Rollback()
            Throw New Exception(ex.Message)
        Finally
            clsCommon.ProgressBarHide()
        End Try
        Return isSaved
    End Function


    Public Shared Function SetUpdateRouteTypeID() As Boolean

        'clsCommon.ProgressBarShow()
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim arrDB As List(Of String) = New List(Of String)
            Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim strDB As String = clsCommon.myCstr(dr("DataBase_Name"))
                    arrDB.Add(strDB)
                Next
            End If


            '7th 
            qry = "update " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES set RouteNo=(select Route_No from " + clsCommon.ReplicateDBString + "tspl_transfer_head where " + clsCommon.ReplicateDBString + "tspl_transfer_head.Transfer_No =" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.Transfer_No ) where RouteNo is null"
            isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

            '8th
            qry = "update " + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES set Route_Type_Id=(select Route_Type_Id  from " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER a inner join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE b on a.Type=b.Route_Type_Id where a.Route_No=" + clsCommon.ReplicateDBString + "TEMP_PROVISIONAL_SALES.RouteNo) where Route_Type_Id is null"
            isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

            '9th
            qry = "update " + clsCommon.ReplicateDBString + "tspl_sale_invoice_head set Route_Type_Id=(select Route_Type_Id  from " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER a inner join " + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE b on a.Type=b.Route_Type_Id where a.Route_No=" + clsCommon.ReplicateDBString + "tspl_sale_invoice_head.Route_No) where Route_Type_Id is null"
            isSaved = clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, arrDB, trans)

            If isSaved Then
                trans.Commit()
                'clsCommon.ProgressBarHide()
            End If
        Catch ex As Exception
            'clsCommon.ProgressBarHide()
            trans.Rollback()

        Finally
            'clsCommon.ProgressBarHide()
        End Try
        Return isSaved
    End Function


    Public Shared Function ReverseTargetAmt(ByVal strShipmentNo As String, ByVal dtShipedDate As DateTime, ByVal trans As SqlTransaction) As Boolean
        ''For Target Reverse
        Dim qry As String = " select TSPL_SHIPMENT_MASTER.Cust_Code,TSPL_SHIPMENT_DETAILS.Discount_Code,TSPL_SHIPMENT_DETAILS.Target_Discount_Amt from TSPL_SHIPMENT_DETAILS "
        qry += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No"
        qry += " where TSPL_SHIPMENT_DETAILS.Shipment_No='" + strShipmentNo + "' and TSPL_SHIPMENT_DETAILS.From_Scheme_Code like 'MS%' and Target_Discount_Amt>0 and len(isnull(Discount_Code,''))>0 "
        Dim dtForCheck As Date = New Date(dtShipedDate.Year, dtShipedDate.Month, 1)
        dtForCheck = dtForCheck.AddMonths(1)
        dtForCheck = dtForCheck.AddDays(-1) ''For Taking last day
        Dim dtTarget As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        For Each dr As DataRow In dtTarget.Rows
            Dim dblTargetAmt As Double = clsCommon.myCdbl(dr("Target_Discount_Amt"))
            qry = "select Month_Year,Amount,Bal_Amount,Amount-Bal_Amount as ApplicableBalanceAmt from TSPL_TARGET_MASTER where Cust_Code='" + clsCommon.myCstr(dr("Cust_Code")) + "' and Discount_Type='" + clsCommon.myCstr(dr("Discount_Code")) + "' and Month_Year<='" + clsCommon.GetPrintDate(dtForCheck, "dd/MMM/yyyy") + "' and Amount-Bal_Amount>0 order by Month_Year desc"
            Dim dtInner As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            For Each drinner As DataRow In dtInner.Rows
                Dim dblApplicableBalanceAmt As Double = clsCommon.myCdbl(drinner("ApplicableBalanceAmt"))
                Dim dblAmtToUpdate As Double = 0
                If dblTargetAmt > dblApplicableBalanceAmt Then
                    dblAmtToUpdate = dblApplicableBalanceAmt
                Else
                    dblAmtToUpdate = dblTargetAmt
                End If
                qry = "update TSPL_TARGET_MASTER set TSPL_TARGET_MASTER.Bal_Amount=TSPL_TARGET_MASTER.Bal_Amount+" + clsCommon.myCstr(dblAmtToUpdate) + " where TSPL_TARGET_MASTER.Cust_Code='" + clsCommon.myCstr(dr("Cust_Code")) + "' and TSPL_TARGET_MASTER.Discount_Type='" + clsCommon.myCstr(dr("Discount_Code")) + "'  and TSPL_TARGET_MASTER.Month_Year='" + clsCommon.GetPrintDate(clsCommon.myCDate(drinner("Month_Year")), "dd/MMM/yyyy") + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                dblTargetAmt -= dblAmtToUpdate

                If dblTargetAmt = 0 Then
                    Exit For
                End If
            Next
            If dblTargetAmt > 0 Then
                Throw New Exception("Target amount not reversed")
            End If

        Next
        ''end For Target Reverse
        Return True
    End Function

    Public Shared Function UpdateWithCustomerNewPrice(ByVal isForReverse As Boolean, ByVal strShipmentNo As String, ByVal strLocation As String, ByVal dtShipedDate As Date, ByVal isUpdateCustomerNewPrice As Boolean, ByVal isForTransfer As Boolean)
        Return UpdateWithCustomerNewPrice(isForReverse, strShipmentNo, strLocation, dtShipedDate, isUpdateCustomerNewPrice, isForTransfer, False)
    End Function
    Public Shared Function UpdateWithCustomerNewPrice(ByVal isForReverse As Boolean, ByVal strShipmentNo As String, ByVal strLocation As String, ByVal dtShipedDate As Date, ByVal isUpdateCustomerNewPrice As Boolean, ByVal isForTransfer As Boolean, ByVal isDeleteShipment As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If UpdateWithCustomerNewPrice(isForReverse, strShipmentNo, strLocation, dtShipedDate, isUpdateCustomerNewPrice, isForTransfer, isDeleteShipment, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function UpdateWithCustomerNewPrice(ByVal isForReverse As Boolean, ByVal strShipmentNo As String, ByVal strLocation As String, ByVal dtShipedDate As Date, ByVal isUpdateCustomerNewPrice As Boolean, ByVal isForTransfer As Boolean, ByVal isDeleteShipment As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(strShipmentNo) > 0 Then
                Dim strBaseQuery As String = ""
                Dim qry As String = ""
                Try
                    ReverseTargetAmt(strShipmentNo, dtShipedDate, trans)



                    If isForReverse Then
                        qry = "select Sale_Invoice_No,Is_Post  from TSPL_SALE_INVOICE_HEAD where Shipment_No='" + strShipmentNo + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                        Dim arr As List(Of String) = New List(Of String)
                        For Each dr As DataRow In dt.Rows
                            Dim strSaleInvNo As String = clsCommon.myCstr(dr("Sale_Invoice_No"))
                            arr.Add(strSaleInvNo)


                            If isForTransfer Then
                                qry = " insert into TEMP_Delete (ShipmentNo,InvoiceNo) values('" + strShipmentNo + "','" + strSaleInvNo + "')"
                            Else
                                qry = " insert into TEMP_Delete_sale (ShipmentNo,InvoiceNo) values('" + strShipmentNo + "','" + strSaleInvNo + "')"
                            End If
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        Next
                        clsSaleHead.ReverseSaleAndShipment(strShipmentNo, arr, isDeleteShipment, trans, isForTransfer)
                    End If

                    If isUpdateCustomerNewPrice Then
                        qry = "update TSPL_SHIPMENT_MASTER set TSPL_SHIPMENT_MASTER.Price_Code=(select "
                        If clsLocation.isLocatinExcisable(strLocation, trans) Then
                            qry += " Price_Code "
                        Else
                            qry += " price_CodeNon "
                        End If
                        qry += " from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SHIPMENT_MASTER.Cust_Code ) where TSPL_SHIPMENT_MASTER.Shipment_No='" + strShipmentNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)


                        qry = " update TSPL_SHIPMENT_DETAILS set TSPL_SHIPMENT_DETAILS.Price_Code=(select Price_Code from TSPL_SHIPMENT_MASTER where TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No) where TSPL_SHIPMENT_DETAILS.Shipment_No='" + strShipmentNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        strBaseQuery = " select TSPL_SHIPMENT_DETAILS.Shipment_No,TSPL_SHIPMENT_DETAILS.Shipment_Id, TSPL_ITEM_PRICE_MASTER.Item_Basic_Price,TSPL_ITEM_PRICE_MASTER.Price_Rate10 as TPT, TSPL_ITEM_PRICE_MASTER.Empty_Value_Bottle,TSPL_ITEM_PRICE_MASTER.Empty_Value_Shell ,TSPL_ITEM_PRICE_MASTER.Abatement_Rate,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,TSPL_ITEM_PRICE_MASTER.Start_Date "
                        strBaseQuery += " from TSPL_SHIPMENT_DETAILS "
                        strBaseQuery += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No"
                        strBaseQuery += " inner join TSPL_ITEM_PRICE_MASTER on  TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_SHIPMENT_DETAILS.Price_code and TSPL_ITEM_PRICE_MASTER.Start_Date= ( select MAX(Start_Date) from TSPL_ITEM_PRICE_MASTER as P where 2=2 and P.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code and P.UOM=TSPL_ITEM_PRICE_MASTER.UOM and P.Item_Basic_Net=TSPL_ITEM_PRICE_MASTER.Item_Basic_Net and TSPL_ITEM_PRICE_MASTER.Price_Code=P.Price_Code and P.Tax_group=TSPL_ITEM_PRICE_MASTER.Tax_group and Start_Date<='" + clsCommon.GetPrintDate(dtShipedDate, "dd/MMM/yyyy") + "' )  and TSPL_ITEM_PRICE_MASTER.Item_Basic_Net=TSPL_SHIPMENT_DETAILS.MRP_Amt  and TSPL_ITEM_PRICE_MASTER.Tax_group= TSPL_SHIPMENT_MASTER.Tax_Group  and TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code and TSPL_ITEM_PRICE_MASTER.UOM=TSPL_SHIPMENT_DETAILS.Unit_code  "
                        strBaseQuery += " where TSPL_SHIPMENT_DETAILS.Shipment_No='" + strShipmentNo + "'"


                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strBaseQuery, trans)
                        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable("Select 1 from TSPL_SHIPMENT_DETAILS where Shipment_No='" + strShipmentNo + "'", trans)

                        If dt1 Is Nothing OrElse dt2 Is Nothing Then
                            Throw New Exception("No item found to change")
                        End If

                        If Not dt1.Rows.Count = dt2.Rows.Count Then
                            Throw New Exception("New Item Price Price Details not found for all items")
                        End If

                        qry = "update TSPL_SHIPMENT_DETAILS set TSPL_SHIPMENT_DETAILS.Basic_Rate=xxx.Item_Basic_Price,TSPL_SHIPMENT_DETAILS.TPT=xxx.TPT,"
                        qry += " TSPL_SHIPMENT_DETAILS.Empty_Value_Bottle=xxx.Empty_Value_Bottle,TSPL_SHIPMENT_DETAILS.Empty_Value_Shell=xxx.Empty_Value_Shell,TSPL_SHIPMENT_DETAILS.Empty_Value=xxx.Empty_Value_Bottle+xxx.Empty_Value_Shell,TSPL_SHIPMENT_DETAILS.Abatement_Rate=xxx.Abatement_Rate,TSPL_SHIPMENT_DETAILS.TAX1_Rate=xxx.TAX1_Rate,TSPL_SHIPMENT_DETAILS.Price_Date=xxx.Start_Date "
                        qry += " From ("
                        qry += strBaseQuery
                        qry += " )xxx"
                        qry += " inner join TSPL_SHIPMENT_DETAILS on  TSPL_SHIPMENT_DETAILS.Shipment_No=xxx.Shipment_No and TSPL_SHIPMENT_DETAILS.Shipment_Id=xxx.Shipment_Id"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        qry = "update TSPL_SHIPMENT_MASTER set TSPL_SHIPMENT_MASTER.Price_Date=(select top 1 TSPL_SHIPMENT_DETAILS.Price_Date from TSPL_SHIPMENT_DETAILS where TSPL_SHIPMENT_DETAILS.Shipment_No=TSPL_SHIPMENT_MASTER.Shipment_No ) where TSPL_SHIPMENT_MASTER.Shipment_No='" + strShipmentNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseSaleAndShipment(ByVal strShipmentNo As String, ByVal ArrSaleInv As List(Of String), ByVal isDeleteShipment As Boolean, ByVal trans As SqlTransaction, ByVal isDeleteEmpty As Boolean) As Boolean
        Try
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
            Dim qry As String = "select Item_Code,Item_Desc,Location_Code,Qty,UOM,MRP,ItemType,Basic_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strShipmentNo + "' and Trans_Type='Load Out'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            For Each objtr As DataRow In dt.Rows
                Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objtr("Item_Code")), clsCommon.myCstr(objtr("UOM")), trans)
                Dim objLocationDetails As New clsItemLocationDetails()
                objLocationDetails.Item_Code = clsCommon.myCstr(objtr("Item_Code"))
                objLocationDetails.Item_Desc = clsCommon.myCstr(objtr("Item_Desc"))
                objLocationDetails.Location_Code = clsCommon.myCstr(objtr("Location_Code"))
                objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                objLocationDetails.Item_Qty = clsCommon.myCdbl(objtr("Qty")) / dblConvFac
                objLocationDetails.Amount = clsCommon.myCdbl(objtr("Basic_Cost"))
                objLocationDetails.MRP = clsCommon.myCdbl(objtr("MRP")) * dblConvFac
                'dt = clsDBFuncationality.GetDataTable("select top 1 MFG_Date,Expiry_Date,Batch_No from TSPL_ADJUSTMENT_DETAIL where Item_Code='" + objLocationDetails.Item_Code + "'", trans)
                'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '    objLocationDetails.Batch_No = clsCommon.myCstr(dt.Rows(0)("Batch_No"))
                '    objLocationDetails.MFG_Date = clsCommon.myCDate(dt.Rows(0)("MFG_Date"))
                '    objLocationDetails.Expiry_Date = clsCommon.myCDate(dt.Rows(0)("Expiry_Date"))
                'End If
                objLocationDetails.ItemType = clsCommon.myCstr(objtr("ItemType"))
                ArrLocationDetails.Add(objLocationDetails)
            Next

            clsItemLocationDetails.SaveData(strPostDate, ArrLocationDetails, trans)

            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strShipmentNo + "' and Trans_Type='Load Out'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            Dim objShipment As clsShipmentMaster = clsShipmentMaster.GetData(strShipmentNo, trans)

            If clsCommon.CompairString(objShipment.Shipment_Type, "Transfer") = CompairStringResult.Equal Then
                For Each objTR As clsShipmentDetail In objShipment.Arr
                    Dim convertFact As Decimal = clsItemMaster.GetConvertionFactor(objTR.Item_Code, objTR.Unit_code, trans)

                    qry = "SELECT ISNULL( Pending_Balance_In_Bottle,0) from TSPL_TRANSFER_DETAIL WHERE Transfer_No='" + objShipment.Transfer_No + "' AND " & _
                    " Item_Code='" + objTR.Item_Code + "'  AND MRP='" + (CDec(objTR.MRP_Amt) * convertFact).ToString() + "' "
                    Dim balanceqtyInBottle As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                    qry = "SELECT pending_Qty from TSPL_TRANSFER_DETAIL WHERE Transfer_No='" + objShipment.Transfer_No + "' AND " & _
                   " Item_Code='" + objTR.Item_Code + "'  AND MRP='" + (CDec(objTR.MRP_Amt) * convertFact).ToString() + "' "
                    Dim balanceqty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

                    Dim shippedQty As Decimal = 0
                    Dim otherconversion As Decimal
                    Dim shippedQtyInBottle As Decimal = 0
                    If convertFact = 1 Then
                        otherconversion = clsDBFuncationality.getSingleValue("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(objTR.Item_Code) + "' and UOM_Code <> '" + Convert.ToString(objTR.Unit_code) + "' AND UM.Create_Price = 'Y'", trans)
                        shippedQtyInBottle = CDec(objTR.Shipped_Qty) * otherconversion
                    Else
                        shippedQtyInBottle = CDec(objTR.Shipped_Qty)
                    End If
                    qry = "UPDATE TSPL_TRANSFER_DETAIL SET Pending_Qty='" + (balanceqty + CDec(objTR.Shipped_Qty) * 1 / convertFact).ToString() + "', Pending_Balance_In_Bottle ='" + Convert.ToString(balanceqtyInBottle + shippedQtyInBottle) + "' WHERE Transfer_No='" + objShipment.Transfer_No + "' AND " & _
                    " Item_Code='" + objTR.Item_Code + "' AND MRP='" + (CDec(objTR.MRP_Amt) * convertFact).ToString() + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    ''End of Update Transfer Qty
                Next
            End If


            Dim strVoucherNo As String = ""
            For Each strInvNo As String In ArrSaleInv
                Dim strSaleInvoiceNo As String = clsCommon.myCstr(strInvNo)
                If clsCommon.myLen(strSaleInvoiceNo) > 0 Then
                    If isDeleteShipment Then
                        qry = "delete from TSPL_SALE_INVOICE_DETAIL where Sale_Invoice_No='" + strSaleInvoiceNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)

                        qry = "delete from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + strSaleInvoiceNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Else
                        qry = "Update TSPL_SALE_INVOICE_HEAD Set Status='Open' where Sale_Invoice_No='" + strSaleInvoiceNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                    'Throw New Exception("Balwinder")

                    qry = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SD-IN' and Source_Doc_No='" + strSaleInvoiceNo + "'"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    For Each dr As DataRow In dt.Rows
                        strVoucherNo = clsCommon.myCstr(dr("Voucher_No"))
                        If clsCommon.myLen(strVoucherNo) > 0 Then
                            qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        End If
                    Next


                    If isDeleteEmpty Then
                        '''''''''''''''''For Empty
                        qry = "select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Document_No='" + strSaleInvoiceNo + "' and Reference_Document='Sale Invoice' and ItemType='E'"
                        Dim dtAdj As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For Each dr As DataRow In dtAdj.Rows
                            Dim strAdjustmentNo As String = clsCommon.myCstr(dr("Adjustment_No"))

                            qry = "select Item_Code,Item_Desc,Location_Code,Qty,UOM,MRP,ItemType,Basic_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strAdjustmentNo + "' and Trans_Type='Adjustment'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            For Each objtr As DataRow In dt.Rows
                                Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objtr("Item_Code")), clsCommon.myCstr(objtr("UOM")), trans)
                                Dim objLocationDetails As New clsItemLocationDetails()
                                objLocationDetails.Item_Code = clsCommon.myCstr(objtr("Item_Code"))
                                objLocationDetails.Item_Desc = clsCommon.myCstr(objtr("Item_Desc"))
                                objLocationDetails.Location_Code = clsCommon.myCstr(objtr("Location_Code"))
                                objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                                objLocationDetails.Item_Qty = -1 * clsCommon.myCdbl(objtr("Qty")) / dblConvFac
                                objLocationDetails.Amount = -1 * clsCommon.myCdbl(objtr("Basic_Cost"))
                                objLocationDetails.MRP = clsCommon.myCdbl(objtr("MRP")) * dblConvFac
                                dt = clsDBFuncationality.GetDataTable("select top 1 MFG_Date,Expiry_Date,Batch_No from TSPL_ADJUSTMENT_DETAIL where Item_Code='" + objLocationDetails.Item_Code + "' and MFG_Date is not null and [Expiry_Date] is not null", trans)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    objLocationDetails.Batch_No = clsCommon.myCstr(dt.Rows(0)("Batch_No"))
                                    objLocationDetails.MFG_Date = clsCommon.myCDate(dt.Rows(0)("MFG_Date"))
                                    objLocationDetails.Expiry_Date = clsCommon.myCDate(dt.Rows(0)("Expiry_Date"))
                                End If
                                objLocationDetails.ItemType = clsCommon.myCstr(objtr("ItemType"))
                                ArrLocationDetails.Add(objLocationDetails)
                            Next

                            clsItemLocationDetails.SaveData(strPostDate, ArrLocationDetails, trans)

                            qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strAdjustmentNo + "' and Trans_Type='Adjustment'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No='" + strAdjustmentNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "delete from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" + strAdjustmentNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No='" + strAdjustmentNo + "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            For Each dr1 As DataRow In dt.Rows
                                strVoucherNo = clsCommon.myCstr(dr1("Voucher_No"))
                                If clsCommon.myLen(strVoucherNo) > 0 Then
                                    qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                    qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If
                            Next
                        Next
                        '''''''''''''''''End of Empty


                        '''''''''For Receipt
                        qry = "select Receipt_No from TSPL_RECEIPT_DETAIL  where Document_No='" + strSaleInvoiceNo + "'"
                        Dim dtReceipt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For Each dr As DataRow In dtReceipt.Rows
                            Dim strReceiptNo As String = clsCommon.myCstr(dr("Receipt_No"))

                            qry = "delete from TSPL_RECEIPT_DETAIL where Receipt_No='" + strReceiptNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "delete from TSPL_RECEIPT_HEADER where Receipt_No='" + strReceiptNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AR-PY' and Source_Doc_No='" + strReceiptNo + "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            For Each dr1 As DataRow In dt.Rows
                                strVoucherNo = clsCommon.myCstr(dr1("Voucher_No"))
                                If clsCommon.myLen(strVoucherNo) > 0 Then
                                    qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                                    qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If
                            Next
                        Next
                        '''''''''End of For Receipt
                    End If
                End If
            Next

            qry = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + strShipmentNo + "' and Source_Code='SD-LO' "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            For Each dr As DataRow In dt.Rows
                strVoucherNo = clsCommon.myCstr(dr("Voucher_No"))
                If clsCommon.myLen(strVoucherNo) > 0 Then
                    qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            Next

            If isDeleteShipment Then
                qry = "delete from TSPL_SHIPMENT_DETAILS where Shipment_No='" + strShipmentNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SHIPMENT_MASTER where Shipment_No='" + strShipmentNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                qry = "update TSPL_SHIPMENT_MASTER set Is_Post='N',[Status]= 'Open' where Shipment_No='" + strShipmentNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class


Public Class clsSaleDetail
#Region "Variables"
    Public Sale_Invoice_Id As Integer = Nothing
    Public Sale_Invoice_No As String = Nothing
    Public Complete As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Price_Date As Date = Nothing
    Public Order_Qty As Double = Nothing
    Public Shipped_Qty As Double = Nothing
    Public Invoice_Qty As Double = Nothing
    Public Balance_Qty As Double = Nothing
    Public Unit_code As String = Nothing
    Public Location As String = Nothing
    Public Price_code As String = Nothing
    Public Scheme_Applicable As String = Nothing
    Public Scheme_Code_Qty As String = Nothing
    Public Scheme_Item As String = Nothing
    Public Promo_Scheme_Applicable As String = Nothing
    Public Promo_Scheme_Code As String = Nothing
    Public Promo_Scheme_Item As String = Nothing
    Public Scheme_Disc_Applicable As String = Nothing
    Public Scheme_Code_Cash As String = Nothing
    Public Sampling_Item As String = Nothing
    Public MRP_Amt As Double = Nothing
    Public Basic_Rate As Double = Nothing
    Public Item_Assessable_Rate As Double = Nothing
    Public Disc_Amt As Double = Nothing
    Public Item_Net_Amt As Double = Nothing
    Public TAX1 As String = Nothing
    Public TAX1_Rate As Double = Nothing
    Public TAX1_Assessable_Amt As Double = Nothing
    Public TAX1_Amt As Double = Nothing
    Public TAX2 As String = Nothing
    Public TAX2_Rate As Double = Nothing
    Public TAX2_Assessable_Amt As Double = Nothing
    Public TAX2_Amt As Double = Nothing
    Public TAX3 As String = Nothing
    Public TAX3_Rate As Double = Nothing
    Public TAX3_Assessable_Amt As Double = Nothing
    Public TAX3_Amt As Double = Nothing
    Public TAX4 As String = Nothing
    Public TAX4_Rate As Double = Nothing
    Public TAX4_Assessable_Amt As Double = Nothing
    Public TAX4_Amt As Double = Nothing
    Public TAX5 As String = Nothing
    Public TAX5_Rate As Double = Nothing
    Public TAX5_Assessable_Amt As Double = Nothing
    Public TAX5_Amt As Double = Nothing
    Public TAX6 As String = Nothing
    Public TAX6_Rate As Double = Nothing
    Public TAX6_Assessable_Amt As Double = Nothing
    Public TAX6_Amt As Double = Nothing
    Public TAX7 As String = Nothing
    Public TAX7_Rate As Double = Nothing
    Public TAX7_Assessable_Amt As Double = Nothing
    Public TAX7_Amt As Double = Nothing
    Public TAX8 As String = Nothing
    Public TAX8_Rate As Double = Nothing
    Public TAX8_Assessable_Amt As Double = Nothing
    Public TAX8_Amt As Double = Nothing
    Public TAX9 As String = Nothing
    Public TAX9_Rate As Double = Nothing
    Public TAX9_Assessable_Amt As Double = Nothing
    Public TAX9_Amt As Double = Nothing
    Public TAX10 As String = Nothing
    Public TAX10_Rate As Double = Nothing
    Public TAX10_Assessable_Amt As Double = Nothing
    Public TAX10_Amt As Double = Nothing
    Public Item_Tax As Double = Nothing
    Public Total_Assessable_Amt As Double = Nothing
    Public Total_MRP_Amt As Double = Nothing
    Public Total_Basic_Amt As Double = Nothing
    Public Total_Disc_Amt As Double = Nothing
    Public Total_net_Amt As Double = Nothing
    Public Total_Tax_Amt As Double = Nothing
    Public Total_Item_Amt As Double = Nothing
    Public Empty_Value As Double = Nothing
    Public TPT As Double = Nothing
    Public Unit_Cogs As Double = Nothing
    Public Total_TPT As Double = Nothing
    Public Empty_Value_Shell As Double = Nothing
    Public Empty_Value_Bottle As Double = Nothing
    Public Cust_Discount As Double = Nothing
    Public Total_Cust_Discount As Double = Nothing
    Public Level1_User_Code As String = Nothing
    Public Level2_User_Code As String = Nothing
    Public Level3_User_Code As String = Nothing
    Public Level4_User_Code As String = Nothing
    Public Level5_User_Code As String = Nothing
    Public Level1_User_Commission As Double = Nothing
    Public Level2_User_Commission As Double = Nothing
    Public Level3_User_Commission As Double = Nothing
    Public Level4_User_Commission As Double = Nothing
    Public Level5_User_Commission As Double = Nothing
    Public Level1_User_Comm_Amount As Double = Nothing
    Public Level2_User_Comm_Amount As Double = Nothing
    Public Level3_User_Comm_Amount As Double = Nothing
    Public Level4_User_Comm_Amount As Double = Nothing
    Public Level5_User_Comm_Amount As Double = Nothing
    Public Main_Item As String = Nothing
    Public Discount_Code As String = Nothing
    Public Cust_Item_Discount_NoTax As Double = Nothing
    Public Abatement_rate As Double = Nothing
    Public Target_Discount_Amt As Double = 0
#End Region

End Class
