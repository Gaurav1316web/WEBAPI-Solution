Imports common
Imports System.Data.SqlClient

Public Class clsShipmentMaster

#Region "Varibales"
    Public Shipment_ID As Integer = Nothing
    Public Order_No As String = Nothing
    Public Order_Date As Date = Nothing
    Public Shipment_No As String = Nothing
    Public Shipment_Date As Date = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Cust_PONo As String = Nothing
    Public Expected_Ship_Date As Date = Nothing
    Public Status As String = Nothing
    Public On_Hold As String = Nothing
    Public Multiple_Orders As String = Nothing
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
    Public Shipment_Disc_Percent As Double = Nothing
    Public Shipment_Discount_Amt As Double = Nothing
    Public Shipment_Detail_Disc_Amt As Double = Nothing
    Public Shipment_Detail_Total_Amt As Double = Nothing
    Public Shipment_Tax_Amt As Double = Nothing
    Public Freight_Amt As Double = Nothing
    Public Other_Charges As Double = Nothing
    Public Add_Charges As Double = Nothing
    Public Total_Order_Amt As Double = Nothing
    Public Salesman_Code As String = Nothing
    Public Mode_Of_Transport As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Vehicle_No As String = Nothing
    Public KM_Reading As Integer = Nothing
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public Trip_No As Integer = Nothing
    Public Scheme_Sample_Code As String = Nothing
    Public Price_Date As Date = Nothing
    Public Terms_Code As String = Nothing
    Public Comments As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modify_By As String = Nothing
    Public Modify_Date As DateTime
    Public Level1_User_code As String = Nothing
    Public Level2_User_code As String = Nothing
    Public Level3_User_code As String = Nothing
    Public Level4_User_code As String = Nothing
    Public Level5_User_code As String = Nothing
    Public Comp_Code As String = Nothing
    Public Shipment_Type As String = Nothing
    Public Is_Post As String = Nothing
    Public Total_TPT As Double = Nothing
    Public Transfer_No As String = Nothing
    Public Transfer_Date As Date = Nothing
    Public Level1_User_Commission As Double = Nothing
    Public Level2_User_Commission As Double = Nothing
    Public Level3_User_Commission As Double = Nothing
    Public Level4_User_Commission As Double = Nothing
    Public Level5_User_Commission As Double = Nothing
    Public Empty_Value As Double = Nothing
    Public Is_Complete As String = Nothing
    Public Shell_Qty As Double = Nothing
    Public Date_Time_Removal As DateTime = Nothing
    Public Customer_Invoice_No As String = Nothing
    Public Employee_Code As String = Nothing
    Public is_Sample As Integer = Nothing
    Public Total_Disc_Percent As Double = Nothing
    Public Tax_Recoverable_Amt As Double = Nothing
    Public Tax_Recoverable_Amt2 As Double = Nothing
    Public Tax_Recoverable_Amt3 As Double = Nothing
    Public Tot_Customer_Dis_Amt As Double = Nothing
    Public is_Route_Jumped As Integer = 0
    Public Is_Printed As Integer = 0
    Public Verify_By As String = Nothing
    Public Ship_To As String = Nothing
    Public Ship_To_Desc As String = Nothing
    Public Is_Scheduled As Integer = 0
    Public Against_C_Form As Boolean = False
    Public Transaction_Type As String = Nothing
    Public Mannual_Invoice_Amt As Double = Nothing
    Public Route_Type_Id As String = Nothing
    Public Mannual_Invoice_Qty As Double = 0
    Public TAX_GROUP_TYPE As String = Nothing
    Public Invoice_No As String = Nothing
    Public Is_Create_Empty As Boolean = False
    Public Discount_On As Integer = 0
    Public Manaual_Invoice_No As String = ""
    Public Arr As List(Of clsShipmentDetail) = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsShipmentMaster, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsShipmentMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Shipment/Sale Invoice", obj.Location, obj.Shipment_Date, trans)

            Dim qry As String = "delete from TSPL_SHIPMENT_DETAILS where Shipment_No='" + obj.Shipment_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Shipment_No = clsERPFuncationality.GetNextCode(trans, obj.Shipment_Date, clsDocType.Shipment, "", obj.Location)
            End If
            If (clsCommon.myLen(obj.Shipment_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Order_No", obj.Order_No)
            clsCommon.AddColumnsForChange(coll, "Order_Date", clsCommon.GetPrintDate(obj.Order_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Shipment_Date", clsCommon.GetPrintDate(obj.Date_Time_Removal, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_Name", obj.Cust_Name)
            clsCommon.AddColumnsForChange(coll, "Cust_PONo", obj.Cust_PONo)
            clsCommon.AddColumnsForChange(coll, "Expected_Ship_Date", clsCommon.GetPrintDate(obj.Expected_Ship_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "On_Hold", obj.On_Hold)
            clsCommon.AddColumnsForChange(coll, "Multiple_Orders", obj.Multiple_Orders)
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Is_Create_Empty", IIf(obj.Is_Create_Empty, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Discount_On", Discount_On)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Cust_Account", obj.Cust_Account)


            clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
            clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX1_Assessable_Amt", obj.TAX1_Assessable_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
            clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX2_Assessable_Amt", obj.TAX2_Assessable_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
            clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX3_Assessable_Amt", obj.TAX3_Assessable_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
            clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX4_Assessable_Amt", obj.TAX4_Assessable_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
            clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX5_Assessable_Amt", obj.TAX5_Assessable_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
            clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX6_Assessable_Amt", obj.TAX6_Assessable_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
            clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX7_Assessable_Amt", obj.TAX7_Assessable_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
            clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX8_Assessable_Amt", obj.TAX8_Assessable_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
            clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX9_Assessable_Amt", obj.TAX9_Assessable_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
            clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
            clsCommon.AddColumnsForChange(coll, "TAX10_Assessable_Amt", obj.TAX10_Assessable_Amt)
            clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
            clsCommon.AddColumnsForChange(coll, "Total_Assessable_Amount", obj.Total_Assessable_Amount)
            clsCommon.AddColumnsForChange(coll, "Shipment_Disc_Percent", obj.Shipment_Disc_Percent)
            clsCommon.AddColumnsForChange(coll, "Shipment_Discount_Amt", obj.Shipment_Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Shipment_Detail_Disc_Amt", obj.Shipment_Detail_Disc_Amt)
            clsCommon.AddColumnsForChange(coll, "Shipment_Detail_Total_Amt", obj.Shipment_Detail_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Shipment_Tax_Amt", obj.Shipment_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Freight_Amt", obj.Freight_Amt)

            clsCommon.AddColumnsForChange(coll, "Other_Charges", obj.Other_Charges)
            clsCommon.AddColumnsForChange(coll, "Add_Charges", obj.Add_Charges)
            clsCommon.AddColumnsForChange(coll, "Total_Order_Amt", obj.Total_Order_Amt)
            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)


            clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
            clsCommon.AddColumnsForChange(coll, "KM_Reading", obj.KM_Reading)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Route_Desc", obj.Route_Desc)
            clsCommon.AddColumnsForChange(coll, "Trip_No", obj.Trip_No)
            clsCommon.AddColumnsForChange(coll, "Scheme_Sample_Code", obj.Scheme_Sample_Code)
            clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Level1_User_code", obj.Level1_User_code)
            clsCommon.AddColumnsForChange(coll, "Level2_User_code", obj.Level2_User_code)
            clsCommon.AddColumnsForChange(coll, "Level3_User_code", obj.Level3_User_code)
            clsCommon.AddColumnsForChange(coll, "Level4_User_code", obj.Level4_User_code)
            clsCommon.AddColumnsForChange(coll, "Level5_User_code", obj.Level5_User_code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Shipment_Type", obj.Shipment_Type)
            clsCommon.AddColumnsForChange(coll, "Is_Post", obj.Is_Post)
            clsCommon.AddColumnsForChange(coll, "Total_TPT", obj.Total_TPT)
            clsCommon.AddColumnsForChange(coll, "Transfer_No", obj.Transfer_No)
            clsCommon.AddColumnsForChange(coll, "Transfer_Date", clsCommon.GetPrintDate(obj.Transfer_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Level1_User_Commission", obj.Level1_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level2_User_Commission", obj.Level2_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level3_User_Commission", obj.Level3_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level4_User_Commission", obj.Level4_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level5_User_Commission", obj.Level5_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Empty_Value", obj.Empty_Value)
            clsCommon.AddColumnsForChange(coll, "Is_Complete", obj.Is_Complete)
            clsCommon.AddColumnsForChange(coll, "Shell_Qty", obj.Shell_Qty)
            clsCommon.AddColumnsForChange(coll, "Date_Time_Removal", clsCommon.GetPrintDate(obj.Date_Time_Removal, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Customer_Invoice_No", obj.Customer_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Employee_Code", obj.Employee_Code)


            clsCommon.AddColumnsForChange(coll, "is_Sample", obj.is_Sample)
            clsCommon.AddColumnsForChange(coll, "Total_Disc_Percent", obj.Total_Disc_Percent)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable_Amt", obj.Tax_Recoverable_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable_Amt2", obj.Tax_Recoverable_Amt2)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable_Amt3", obj.Tax_Recoverable_Amt3)
            clsCommon.AddColumnsForChange(coll, "Tot_Customer_Dis_Amt", obj.Tot_Customer_Dis_Amt)
            clsCommon.AddColumnsForChange(coll, "is_Route_Jumped", obj.is_Route_Jumped)
            clsCommon.AddColumnsForChange(coll, "Is_Printed", obj.Is_Printed)
            clsCommon.AddColumnsForChange(coll, "Verify_By", obj.Verify_By)
            clsCommon.AddColumnsForChange(coll, "Ship_To", obj.Ship_To)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Desc", obj.Ship_To_Desc)
            clsCommon.AddColumnsForChange(coll, "Is_Scheduled", obj.Is_Scheduled)
            clsCommon.AddColumnsForChange(coll, "Against_C_Form", IIf(obj.Against_C_Form, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Transaction_Type", obj.Transaction_Type)
            clsCommon.AddColumnsForChange(coll, "Mannual_Invoice_Amt", obj.Mannual_Invoice_Amt)
            clsCommon.AddColumnsForChange(coll, "Route_Type_Id", obj.Route_Type_Id)
            clsCommon.AddColumnsForChange(coll, "Mannual_Invoice_Qty", obj.Mannual_Invoice_Qty)
            clsCommon.AddColumnsForChange(coll, "TAX_GROUP_TYPE", obj.TAX_GROUP_TYPE)
            clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.Invoice_No)

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Shipment_ID", connectSql.autoNumber("TSPL_SHIPMENT_MASTER", trans))
                clsCommon.AddColumnsForChange(coll, "Shipment_No", obj.Shipment_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIPMENT_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIPMENT_MASTER", OMInsertOrUpdate.Update, "TSPL_SHIPMENT_MASTER.Shipment_No='" + obj.Shipment_No + "'", trans)
            End If


            isSaved = isSaved AndAlso clsShipmentDetail.SaveData(obj.Shipment_No, Arr, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal Trans As SqlTransaction) As clsShipmentMaster
        Return GetData(strCode, NavigatorType.Current, Trans)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal Trans As SqlTransaction) As clsShipmentMaster
        Dim obj As clsShipmentMaster = Nothing
        Dim strLocation As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) Then
            strLocation += " and TSPL_SHIPMENT_MASTER.Location  in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Dim qry As String = "select * from TSPL_SHIPMENT_MASTER where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SHIPMENT_MASTER.Shipment_No = (select MIN(Shipment_No) from TSPL_SHIPMENT_MASTER where 2=2 " + strLocation + ")"
            Case NavigatorType.Last
                qry += " and TSPL_SHIPMENT_MASTER.Shipment_No = (select Max(Shipment_No) from TSPL_SHIPMENT_MASTER where 2=2 " + strLocation + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SHIPMENT_MASTER.Shipment_No = '" + strCode + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SHIPMENT_MASTER.Shipment_No = (select Min(Shipment_No) from TSPL_SHIPMENT_MASTER where Shipment_No>'" + strCode + "' " + strLocation + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SHIPMENT_MASTER.Shipment_No = (select Max(Shipment_No) from TSPL_SHIPMENT_MASTER where Shipment_No<'" + strCode + "' " + strLocation + ")"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsShipmentMaster
            obj.Shipment_ID = clsCommon.myCdbl(dt.Rows(0)("Shipment_ID"))
            obj.Order_No = clsCommon.myCstr(dt.Rows(0)("Order_No"))
            If dt.Rows(0)("Order_Date") IsNot DBNull.Value Then
                obj.Order_Date = clsCommon.myCDate(dt.Rows(0)("Order_Date"))
            End If

            obj.Shipment_No = clsCommon.myCstr(dt.Rows(0)("Shipment_No"))
            obj.Shipment_Date = clsCommon.myCDate(dt.Rows(0)("Shipment_Date"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
            obj.Cust_PONo = clsCommon.myCstr(dt.Rows(0)("Cust_PONo"))
            obj.Expected_Ship_Date = clsCommon.myCDate(dt.Rows(0)("Expected_Ship_Date"))
            obj.Status = clsCommon.myCstr(dt.Rows(0)("Status"))
            obj.On_Hold = clsCommon.myCstr(dt.Rows(0)("On_Hold"))
            obj.Multiple_Orders = clsCommon.myCstr(dt.Rows(0)("Multiple_Orders"))
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
            obj.Shipment_Disc_Percent = clsCommon.myCdbl(dt.Rows(0)("Shipment_Disc_Percent"))
            obj.Shipment_Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Shipment_Discount_Amt"))
            obj.Shipment_Detail_Disc_Amt = clsCommon.myCdbl(dt.Rows(0)("Shipment_Detail_Disc_Amt"))
            obj.Shipment_Detail_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Shipment_Detail_Total_Amt"))
            obj.Shipment_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Shipment_Tax_Amt"))
            obj.Freight_Amt = clsCommon.myCdbl(dt.Rows(0)("Freight_Amt"))
            obj.Other_Charges = clsCommon.myCdbl(dt.Rows(0)("Other_Charges"))
            obj.Add_Charges = clsCommon.myCdbl(dt.Rows(0)("Add_Charges"))
            obj.Total_Order_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Order_Amt"))
            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Mode_Of_Transport = clsCommon.myCstr(dt.Rows(0)("Mode_Of_Transport"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.KM_Reading = clsCommon.myCdbl(dt.Rows(0)("KM_Reading"))
            obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
            obj.Route_Desc = clsCommon.myCstr(dt.Rows(0)("Route_Desc"))
            obj.Trip_No = clsCommon.myCdbl(dt.Rows(0)("Trip_No"))
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
            obj.Shipment_Type = clsCommon.myCstr(dt.Rows(0)("Shipment_Type"))
            obj.Is_Post = clsCommon.myCstr(dt.Rows(0)("Is_Post"))
            obj.Against_C_Form = IIf(clsCommon.myCdbl(dt.Rows(0)("Against_C_Form")) = 1, True, False)
            obj.Total_TPT = clsCommon.myCdbl(dt.Rows(0)("Total_TPT"))
            obj.Transfer_No = clsCommon.myCstr(dt.Rows(0)("Transfer_No"))
            If dt.Rows(0)("Transfer_Date") IsNot DBNull.Value Then
                obj.Transfer_Date = clsCommon.myCDate(dt.Rows(0)("Transfer_Date"))
            End If
            obj.Is_Create_Empty = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Create_Empty")) = 1, True, False)
            obj.Discount_On = clsCommon.myCdbl(dt.Rows(0)("Discount_On"))
            obj.Verify_By = clsCommon.myCstr(dt.Rows(0)("Verify_By"))
            obj.Is_Printed = clsCommon.myCdbl(dt.Rows(0)("Is_Printed"))
            obj.Level1_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level1_User_Commission"))
            obj.Level2_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level2_User_Commission"))
            obj.Level3_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level3_User_Commission"))
            obj.Level4_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level4_User_Commission"))
            obj.Level5_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level5_User_Commission"))
            obj.Empty_Value = clsCommon.myCdbl(dt.Rows(0)("Empty_Value"))
            obj.Is_Complete = clsCommon.myCstr(dt.Rows(0)("Is_Complete"))
            obj.Shell_Qty = clsCommon.myCdbl(dt.Rows(0)("Shell_Qty"))
            obj.Date_Time_Removal = clsCommon.myCDate(dt.Rows(0)("Date_Time_Removal"))
            obj.Customer_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Customer_Invoice_No"))
            obj.Employee_Code = clsCommon.myCstr(dt.Rows(0)("Employee_Code"))
            obj.is_Sample = clsCommon.myCdbl(dt.Rows(0)("is_Sample"))
            obj.Total_Disc_Percent = clsCommon.myCdbl(dt.Rows(0)("Total_Disc_Percent"))
            obj.Tax_Recoverable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt"))
            obj.Tax_Recoverable_Amt2 = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt2"))
            obj.Tax_Recoverable_Amt3 = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt3"))
            obj.Tot_Customer_Dis_Amt = clsCommon.myCdbl(dt.Rows(0)("Tot_Customer_Dis_Amt"))
            obj.is_Route_Jumped = clsCommon.myCdbl(dt.Rows(0)("is_Route_Jumped"))

            obj.Ship_To = clsCommon.myCstr(dt.Rows(0)("Ship_To"))
            obj.Ship_To_Desc = clsCommon.myCstr(dt.Rows(0)("Ship_To_Desc"))
            obj.Is_Scheduled = clsCommon.myCdbl(dt.Rows(0)("Is_Scheduled"))
            obj.Transaction_Type = clsCommon.myCstr(dt.Rows(0)("Transaction_Type"))
            obj.Mannual_Invoice_Amt = clsCommon.myCdbl(dt.Rows(0)("Mannual_Invoice_Amt"))
            obj.Mannual_Invoice_Qty = clsCommon.myCdbl(dt.Rows(0)("Mannual_Invoice_Qty"))
            obj.Route_Type_Id = clsCommon.myCstr(dt.Rows(0)("Route_Type_Id"))

            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"), "dd/MMM/yyyy hh:mm tt")
            obj.Invoice_No = clsCommon.myCstr(dt.Rows(0)("Invoice_No"))
            qry = "select * from TSPL_SHIPMENT_DETAILS where Shipment_No='" + obj.Shipment_No + "' order by Shipment_Id"
            dt = clsDBFuncationality.GetDataTable(qry, Trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Arr = New List(Of clsShipmentDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As clsShipmentDetail = New clsShipmentDetail()
                    objtr.Shipment_Id = clsCommon.myCdbl(dr("Shipment_Id"))
                    objtr.Shipment_No = clsCommon.myCstr(dr("Shipment_No"))
                    objtr.Complete = clsCommon.myCstr(dr("Complete"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objtr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                    objtr.Order_Qty = clsCommon.myCdbl(dr("Order_Qty"))
                    objtr.Shipped_Qty = clsCommon.myCdbl(dr("Shipped_Qty"))
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
                    objtr.Empty_Value = clsCommon.myCdbl(dr("Empty_Value"))
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
                    objtr.TPT = clsCommon.myCdbl(dr("TPT"))
                    objtr.Total_Item_Amt = clsCommon.myCdbl(dr("Total_Item_Amt"))
                    objtr.Total_TPT = clsCommon.myCdbl(dr("Total_TPT"))
                    objtr.Unit_COGS = clsCommon.myCdbl(dr("Unit_COGS"))
                    objtr.From_Scheme_Code = clsCommon.myCstr(dr("From_Scheme_Code"))
                    objtr.Abatement = clsCommon.myCdbl(dr("Abatement"))
                    objtr.Empty_Value_Shell = clsCommon.myCdbl(dr("Empty_Value_Shell"))
                    objtr.Empty_Value_Bottle = clsCommon.myCdbl(dr("Empty_Value_Bottle"))
                    objtr.Transfer_Basic_Amount = clsCommon.myCdbl(dr("Transfer_Basic_Amount"))
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
                    objtr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    objtr.Sample_Scheme_Code = clsCommon.myCstr(dr("Sample_Scheme_Code"))
                    objtr.Main_Item = clsCommon.myCstr(dr("Main_Item"))
                    objtr.Discount_Code = clsCommon.myCstr(dr("Discount_Code"))
                    objtr.Cust_Item_Discount_NoTax = clsCommon.myCdbl(dr("Cust_Item_Discount_NoTax"))
                    objtr.Target_Discount_Amt = clsCommon.myCdbl(dr("Target_Discount_Amt"))
                    objtr.Price_To_Show = clsCommon.myCdbl(dr("Price_To_Show"))

                    objtr.RAW_Qty = clsCommon.myCdbl(dr("RAW_Qty"))
                    objtr.Converted_Qty = clsCommon.myCdbl(dr("Converted_Qty"))
                    objtr.OZ_Qty = clsCommon.myCdbl(dr("OZ_Qty"))
                    objtr.Abatement_rate = clsCommon.myCdbl(dr("Abatement_rate"))


                    If dr("Price_Date_Actual") IsNot DBNull.Value Then
                        objtr.Price_Date_Actual = clsCommon.myCDate(dr("Price_Date_Actual"))
                    End If
                    obj.Arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function postShipment(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Shipment No not found to Post")
            End If



            ''Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            Dim obj As clsShipmentMaster = clsShipmentMaster.GetData(strCode, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Shipment_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Shipment/Sale Invoice", obj.Location, obj.Shipment_Date, trans)

            If clsCommon.CompairString(obj.On_Hold, "Y") = CompairStringResult.Equal Then
                Throw New Exception("Shipment No " + obj.Shipment_No + " Is On Hold.Can't Post it")
            End If
            If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Modify_Date, "dd/MM/yyyy"))
            End If
            If clsCommon.CompairString("Y", clsFixedParameter.GetData(clsFixedParameterType.PrintVerify, clsFixedParameterCode.SalesInvoice, trans)) = CompairStringResult.Equal Then
                If obj.Is_Printed = 0 Then
                    Throw New Exception("Before posting, Please Check and Verify Sale Invoice on print preview. ")
                End If
            End If


            If clsCommon.myLen(obj.Order_No) > 0 Then
                ''Delete the items whose qty is Zero
                Dim qry As String = "delete from TSPL_SHIPMENT_DETAILS where Shipment_No='" + strCode + "' and Shipped_Qty='0' "
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Shipment_Id,Shipment_No from TSPL_SHIPMENT_DETAILS where Shipment_No='" + strCode + "' order by Shipment_Id", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim counter As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        qry = "update TSPL_SHIPMENT_DETAILS set Shipment_Id='" + clsCommon.myCstr(counter) + "' where Shipment_No='" + strCode + "' and Shipment_Id='" + clsCommon.myCstr(dr("Shipment_Id")) + "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        counter += 1
                    Next
                End If


                qry = "delete from TSPL_SALE_INVOICE_DETAIL where Sale_Invoice_No='" + obj.Invoice_No + "' and Invoice_Qty='0' "
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                dt = clsDBFuncationality.GetDataTable("select Sale_Invoice_Id,Sale_Invoice_No from TSPL_SALE_INVOICE_DETAIL where Sale_Invoice_No='" + obj.Invoice_No + "' order by Sale_Invoice_Id", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim counter As Integer = 1
                    For Each dr As DataRow In dt.Rows
                        qry = "update TSPL_SALE_INVOICE_DETAIL set Sale_Invoice_Id='" + clsCommon.myCstr(counter) + "' where Sale_Invoice_No='" + obj.Invoice_No + "' and Sale_Invoice_Id='" + clsCommon.myCstr(dr("Sale_Invoice_Id")) + "'"
                        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        counter += 1
                    Next
                End If
                ''End of Delete the items whose qty is Zero
                obj = New clsShipmentMaster()
                obj = clsShipmentMaster.GetData(strCode, trans)

            End If



            Dim Sql As String = ""

            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim totalnet As Decimal = 0
            For Each objtr As clsShipmentDetail In obj.Arr
                If clsCommon.CompairString(objtr.Scheme_Applicable, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objtr.Promo_Scheme_Applicable, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objtr.Sampling_Item, "N") = CompairStringResult.Equal Then
                    totalnet = totalnet + objtr.Total_net_Amt
                End If
            Next

            Dim assess As Decimal = 0
            Dim totalAssess As Decimal = 0
            Dim PunchinDate As DateTime = New DateTime(obj.Shipment_Date.Year, obj.Shipment_Date.Month, obj.Shipment_Date.Day, obj.Date_Time_Removal.Hour, obj.Date_Time_Removal.Minute, obj.Date_Time_Removal.Second)
            Dim cogs, conversionamt, schemeCogs, promoCogs, count As Decimal
            Dim Currcogs, CurrschemeCogs, CurrpromoCogs As Decimal
            Dim dblItemBasicPrice As Double = 0

            Dim strLocation As String = obj.Location
            If clsCommon.CompairString(obj.Shipment_Type, "Transfer") = CompairStringResult.Equal Then
                strLocation = clsDBFuncationality.getSingleValue("select To_Location from TSPL_TRANSFER_HEAD where Transfer_No='" + obj.Transfer_No + "'", trans)
            End If

            Dim strLocationName As String = clsLocation.GetName(strLocation, trans)
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            For Each objtr As clsShipmentDetail In obj.Arr
                If objtr.Shipped_Qty > 0 Then
                    updateItemLocationDetails(trans, obj, objtr, strCode)
                    'dblAmountFoJE = dblAmountFoJE + (objtr.Price_To_Show * objtr.Shipped_Qty)
                    ''For Update Amount of Target
                    Dim strSchemeCode As String = objtr.From_Scheme_Code
                    If clsCommon.myLen(strSchemeCode) >= 2 Then
                        Dim strTwoCharacher As String = strSchemeCode.Substring(0, 2)
                        If clsCommon.CompairString(strTwoCharacher, "MS") = CompairStringResult.Equal Then
                            If clsCommon.myLen(objtr.Discount_Code) > 0 AndAlso objtr.Target_Discount_Amt > 0 Then
                                Dim qry As String = "select Cust_Code,Discount_Type,Month_Year,Bal_Amount from TSPL_TARGET_MASTER where Cust_Code='" + obj.Cust_Code + "' and Discount_Type='" + objtr.Discount_Code + "' order by Month_Year"
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                Dim targetAmt As Decimal = objtr.Target_Discount_Amt
                                For Each dr As DataRow In dt.Rows
                                    Dim dblAmtToUpdate As Double = IIf(clsCommon.myCdbl(dr("Bal_Amount")) < targetAmt, clsCommon.myCdbl(dr("Bal_Amount")), targetAmt)
                                    targetAmt -= dblAmtToUpdate
                                    qry = "Update TSPL_TARGET_MASTER set Bal_Amount=Bal_Amount-'" + clsCommon.myCstr(dblAmtToUpdate) + "' "
                                    qry += " where  Cust_Code='" + obj.Cust_Code + "' and discount_Type='" + objtr.Discount_Code + "' and Month_Year ='" + clsCommon.GetPrintDate(clsCommon.myCDate(dr("Month_Year")), "dd/MMM/yyyy") + "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    If targetAmt <= 0 Then
                                        Exit For
                                    End If
                                Next

                            End If
                        End If
                    End If
                    ''End of Update Amount of Target

                    count = count + 1

                    Currcogs = 0
                    CurrschemeCogs = 0
                    CurrpromoCogs = 0
                    ''For journal Entry
                    Sql = "SELECT Unit_COGS,Scheme_Item,Promo_Scheme_Item,Sampling_Item from TSPL_SHIPMENT_DETAILS WHERE Shipment_No='" + strCode + "' " & _
                    " AND Shipment_Id='" + clsCommon.myCstr(objtr.Shipment_Id) + "'"
                    Dim postDr As DataTable = clsDBFuncationality.GetDataTable(Sql, trans)
                    For Each dr As DataRow In postDr.Rows
                        If clsCommon.myCdbl(dr(0)) > 0 Then
                            If dr(1).ToString() = "N" AndAlso dr(2).ToString() = "N" AndAlso dr(3).ToString() = "N" Then
                                conversionamt = clsItemMaster.GetConvertionFactor(Convert.ToString(objtr.Item_Code), Convert.ToString(objtr.Unit_code), trans)
                                Currcogs = Math.Round((clsCommon.myCdbl(objtr.Shipped_Qty) * clsCommon.myCdbl(dr(0)) / conversionamt), 2)
                                cogs = cogs + Currcogs
                            ElseIf dr(1).ToString() = "Y" Or dr(3).ToString() = "Y" Then
                                conversionamt = clsItemMaster.GetConvertionFactor(Convert.ToString(objtr.Item_Code), Convert.ToString(objtr.Unit_code), trans)
                                CurrschemeCogs = Math.Round(clsCommon.myCdbl(objtr.Shipped_Qty) * clsCommon.myCdbl(dr(0)) / conversionamt, 2)
                                schemeCogs = schemeCogs + CurrschemeCogs
                            ElseIf dr(2).ToString() = "Y" Then
                                CurrpromoCogs = Math.Round(+clsCommon.myCdbl(objtr.Shipped_Qty) * clsCommon.myCdbl(dr(0)), 2)
                                promoCogs = promoCogs + CurrpromoCogs
                            End If
                        End If
                    Next

                    ''End of Journal Entry

                    If clsCommon.CompairString(obj.Shipment_Type, "Sale") = CompairStringResult.Equal Then






                        ''For Inventory Movement
                        dblItemBasicPrice = Currcogs + CurrschemeCogs + CurrpromoCogs
                        Sql = "SELECT Excisable from TSPL_LOCATION_MASTER WHERE Location_Code='" + obj.Location + "'"
                        Dim excisable As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))

                        Sql = "Select Abatement_Rate from TSPL_ITEM_PRICE_MASTER WHERE Item_Code='" + objtr.Item_Code + "' AND Item_Basic_Net='" + clsCommon.myCstr(objtr.MRP_Amt) + "' AND Start_Date='" + Format(CDate(obj.Price_Date), "MM/dd/yyyy") + "'"
                        assess = Math.Round(CDec(objtr.Total_MRP_Amt) * clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sql, trans)) / 100, 2)
                        ''''''''''Calculate Additional Cost
                        Dim addAmt As Decimal = 0 ''grow.Cells("tax" + (gvTaxDetails.RowCount).ToString() + "Amt").Value * CDec(objtr.Shipped_Qty)
                        Dim addAmtAll As Decimal = 0
                        If clsCommon.myLen(objtr.TAX1) > 0 AndAlso objtr.TAX1_Amt > 0 Then
                            If Not clsTaxMaster.IsTaxExcisable(objtr.TAX1, trans) Then
                                addAmt += objtr.TAX1_Amt * objtr.Shipped_Qty
                            End If
                            addAmtAll += obj.TAX1_Amt
                        End If
                        If clsCommon.myLen(objtr.TAX2) > 0 AndAlso objtr.TAX2_Amt > 0 Then
                            If Not clsTaxMaster.IsTaxExcisable(objtr.TAX2, trans) Then
                                addAmt += objtr.TAX2_Amt * objtr.Shipped_Qty
                            End If
                            addAmtAll += obj.TAX2_Amt
                        End If
                        If clsCommon.myLen(objtr.TAX3) > 0 AndAlso objtr.TAX3_Amt > 0 Then
                            If Not clsTaxMaster.IsTaxExcisable(objtr.TAX3, trans) Then
                                addAmt += objtr.TAX3_Amt * objtr.Shipped_Qty
                            End If
                            addAmtAll += obj.TAX3_Amt
                        End If
                        If clsCommon.myLen(objtr.TAX4) > 0 AndAlso objtr.TAX4_Amt > 0 Then
                            If Not clsTaxMaster.IsTaxExcisable(objtr.TAX4, trans) Then
                                addAmt += objtr.TAX4_Amt * objtr.Shipped_Qty
                            End If
                            addAmtAll += obj.TAX4_Amt
                        End If
                        If clsCommon.myLen(objtr.TAX5) > 0 AndAlso objtr.TAX5_Amt > 0 Then
                            If Not clsTaxMaster.IsTaxExcisable(objtr.TAX5, trans) Then
                                addAmt += objtr.TAX5_Amt * objtr.Shipped_Qty
                            End If
                            addAmtAll += obj.TAX5_Amt
                        End If
                        If clsCommon.myLen(objtr.TAX6) > 0 AndAlso objtr.TAX6_Amt > 0 Then
                            If Not clsTaxMaster.IsTaxExcisable(objtr.TAX6, trans) Then
                                addAmt += objtr.TAX6_Amt * objtr.Shipped_Qty
                            End If
                            addAmtAll += obj.TAX6_Amt
                        End If
                        ''''''''''End of Calculate Additional Cost

                        Dim objInventoryMovemnt As New clsInventoryMovement()
                        objInventoryMovemnt.InOut = "O"
                        objInventoryMovemnt.Location_Code = strLocation

                        objInventoryMovemnt.Cust_Code = obj.Cust_Code
                        objInventoryMovemnt.Cust_Name = obj.Cust_Name


                        objInventoryMovemnt.Item_Code = objtr.Item_Code
                        objInventoryMovemnt.Item_Desc = objtr.Item_Desc
                        objInventoryMovemnt.Qty = objtr.Shipped_Qty
                        objInventoryMovemnt.UOM = objtr.Unit_code
                        objInventoryMovemnt.MRP = objtr.MRP_Amt
                        objInventoryMovemnt.Net_Cost = dblItemBasicPrice
                        objInventoryMovemnt.ItemType = "FM"
                        objInventoryMovemnt.Basic_Cost = dblItemBasicPrice

                        If excisable = "T" Then
                            Dim assessibleamt As Decimal = assess
                            Dim dt As DataTable = clsTaxGroupMaster.GetTaxDetailsscrap(obj.Tax_Group, trans)
                            Dim dblPercentage As Double = 0
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                For ii As Integer = 0 To dt.Rows.Count - 1
                                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("Type")), "E") = CompairStringResult.Equal Then
                                        If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("Surtax")), "N") = CompairStringResult.Equal Then
                                            dblPercentage += clsCommon.myCdbl(dt.Rows(ii)("TaxRate"))
                                        Else
                                            Dim strSurTaxCode As String = clsCommon.myCstr(dt.Rows(ii)("Surtax_Tax_Code"))
                                            For jj As Integer = 0 To ii
                                                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(jj)("Tax_Code")), strSurTaxCode) = CompairStringResult.Equal Then
                                                    dblPercentage += clsCommon.myCdbl(dt.Rows(jj)("TaxRate")) * clsCommon.myCdbl(dt.Rows(ii)("TaxRate")) / 100
                                                    Exit For
                                                End If
                                            Next
                                        End If
                                    End If
                                Next
                            End If
                            dblPercentage = Math.Round(dblPercentage, 2, MidpointRounding.ToEven)
                            Dim recAmt As Decimal = assessibleamt * dblPercentage / 100
                            objInventoryMovemnt.Add_Cost = addAmt
                            objInventoryMovemnt.Rec_Cost = addAmt
                        Else
                            objInventoryMovemnt.Add_Cost = 0
                            objInventoryMovemnt.Rec_Cost = addAmtAll
                        End If
                        ArrInventoryMovement.Add(objInventoryMovemnt)
                        ''End of Inventory Movement
                    End If
                End If
            Next
            ''trans.Rollback()
            clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), ArrLocationDetails, trans)
            clsInventoryMovement.SaveData("Load Out", obj.Shipment_No, PunchinDate, clsCommon.GetPrintDate(PunchinDate, "dd/MM/yyyy"), ArrInventoryMovement, trans)

            Dim shipmentCogs As Decimal = cogs + schemeCogs + promoCogs
            CreateJournalEntry(strCode, obj, trans)

            Sql = "UPDATE TSPL_SHIPMENT_MASTER SET Is_Post='Y', Status='In Progress' WHERE Shipment_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Sql, trans)

            If clsCommon.CompairString(obj.Shipment_Type, "Transfer") = CompairStringResult.Equal Then
                Xtra.UpdateBalanceQtyAndBalanceQtyInBottleOFTransfer(obj.Transfer_No, trans)
            End If
            Return True

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJournalEntry(ByVal strCode As String, ByVal obj As clsShipmentMaster, ByVal trans As SqlTransaction) As Boolean
        Dim IsCreateJE As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CrreateTransferShipmentJE, clsFixedParameterCode.CrreateTransferShipmentJE, trans)) = 1, True, False)

        If IsCreateJE AndAlso clsCommon.CompairString(obj.Shipment_Type, "Transfer") = CompairStringResult.Equal Then
            Dim dblAmountFoJE As Double = 0
            For Each objtr As clsShipmentDetail In obj.Arr
                If objtr.Shipped_Qty > 0 Then
                    updateItemLocationDetails(trans, obj, objtr, strCode)
                    dblAmountFoJE = dblAmountFoJE + (objtr.Price_To_Show * objtr.Shipped_Qty)
                End If
            Next


            Dim lineNo As Integer = 1
            'Dim jrnlObj As New frmJournalEntry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
            Dim StrVoucher As String = transportSql.fnAutoGenerateNo(trans, obj.Shipment_Date, clsDocTransactionType.JournalEntryOther, obj.Location, False)
            Dim Sql As String = "SELECT SourceDescription  FROM TSPL_GL_SOURCECODE WHERE SourceCode = 'SD-LO'"
            Dim strSourceDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))
            Dim strInvoiceNo As String = strCode

            Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER"
            Dim Jrnl As String = CInt(clsDBFuncationality.getSingleValue(strJrnl, trans)) + 1

            Dim dt As String = clsCommon.GetPrintDate(obj.Shipment_Date, "dd/MMM/yyyy")
            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Source_Code", "SD-LO"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", dt), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Voucher_Desc", obj.Description), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", obj.Remarks), New SqlParameter("@Comments", obj.Remarks), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", dt), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", obj.Cust_Code), New SqlParameter("@CustVend_Name", obj.Cust_Name), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", obj.Total_Order_Amt), New SqlParameter("@Total_Credit_Amt", obj.Total_Order_Amt), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", clsCommon.GETSERVERDATE(trans)), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", clsCommon.GETSERVERDATE(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
            Dim stvoucherdesc As String = "Load out " + strCode + " for customer " + obj.Cust_Name + ""
            clsDBFuncationality.ExecuteNonQuery("update TSPL_JOURNAL_MASTER set Voucher_Desc =' " + stvoucherdesc.Trim() + "' where Voucher_No = '" + StrVoucher + "'", trans)

            Dim strInvAcc As String = ""
            Dim strInvAccDesc As String = ""
            Dim strShpClrAcc As String = ""
            Dim strShpClrAccDesc As String = ""

            Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + obj.Location + "'"
            Dim locSegCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))
            Dim strTempVar As String = ""

            Sql = "SELECT PA.Reserve_Stock FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
             " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
             " TSPL_GL_ACCOUNTS AS GLA ON PA.Reserve_Stock = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr(0).Item_Code + "'"
            strTempVar = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))
            strInvAcc = strTempVar.Replace(strTempVar.Substring(strTempVar.Length - 3, 3), locSegCode)
            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strInvAcc + "'"
            strInvAccDesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))

            Sql = "select TSPL_SALES_ACCOUNTS.Suspence_Account from TSPL_ITEM_MASTER left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code  where TSPL_ITEM_MASTER.Item_Code='" + obj.Arr(0).Item_Code + "'"
            strTempVar = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))
            strShpClrAcc = strTempVar.Replace(strTempVar.Substring(strTempVar.Length - 3, 3), locSegCode)
            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strShpClrAcc + "'"
            strShpClrAccDesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))

            Dim objACSeg As Accountsegment = Accountsegment.Getaccountcodedesc(strShpClrAcc, trans)
            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strShpClrAcc), New SqlParameter("@Account_Desc", strShpClrAccDesc), New SqlParameter("@Amount", dblAmountFoJE), New SqlParameter("@Description", obj.Description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objACSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objACSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objACSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objACSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objACSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objACSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objACSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objACSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objACSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objACSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objACSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objACSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objACSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objACSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objACSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objACSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objACSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objACSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objACSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objACSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objACSeg.Account_Seg_Desc10))
            lineNo = lineNo + 1
            objACSeg = Accountsegment.Getaccountcodedesc(strInvAcc, trans)
            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", dt), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strInvAcc), New SqlParameter("@Account_Desc", strInvAccDesc), New SqlParameter("@Amount", dblAmountFoJE * (-1)), New SqlParameter("@Description", obj.Description), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", dt), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", objACSeg.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", objACSeg.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", objACSeg.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", objACSeg.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", objACSeg.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", objACSeg.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", objACSeg.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", objACSeg.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", objACSeg.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", objACSeg.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", objACSeg.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", objACSeg.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", objACSeg.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", objACSeg.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", objACSeg.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", objACSeg.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", objACSeg.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", objACSeg.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", objACSeg.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", objACSeg.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", objACSeg.Account_Seg_Desc10))
            lineNo = lineNo + 1
            Sql = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + StrVoucher + "' "
            clsDBFuncationality.ExecuteNonQuery(Sql, trans)

        End If
        Return True
    End Function


    Private Shared Sub updateItemLocationDetails(ByVal trans As SqlTransaction, ByVal obj As clsShipmentMaster, ByVal objtr As clsShipmentDetail, ByVal shipmentNo As String)
        Dim itemQty As Decimal = 0
        Dim cogs As Decimal = 0
        Dim unitCogs As Decimal = 0
        Dim shipmentCogs As Decimal = 0
        Dim itemlocationqty As Decimal = 0
        Dim amount As Decimal = 0
        Dim shipmentcogs1 As Decimal = 0
        Dim shipmentqty1 As Decimal = 0
        Dim shipmentamt1 As Decimal = 0
        Dim convertFact As Decimal = clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_code, trans)
        Dim sql As String = ""
        If clsCommon.CompairString(obj.Shipment_Type, "Sale") = CompairStringResult.Equal Then
            sql = "SELECT sum(isnull(Amount,0))/sum(isnull(Item_Qty,0))  FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objtr.Item_Code + "' "
            sql += " AND location_code='" + obj.Location + "' and MRP='" + (CDec(objtr.MRP_Amt) * convertFact).ToString() + "'"
            unitCogs = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql, trans)), 2)
        Else
            sql = "SELECT Item_Price FROM TSPL_TRANSFER_DETAIL WHERE Item_Code='" + objtr.Item_Code + "' AND " & _
            " Transfer_No='" + obj.Transfer_No + "' AND MRP='" + (CDec(objtr.MRP_Amt) * convertFact).ToString() + "' "

            unitCogs = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql, trans))
            ''   sql = "UPDATE TSPL_SHIPMENT_DETAILS SET Unit_COGS='" + unitCogs.ToString() + "' WHERE Item_Code='" + objtr.Item_Code + "' AND " & _
            ''" Shipment_No='" + shipmentNo + "' AND Unit_Code='" + objtr.Unit_code + "' AND " & _
            ''" Price_Date='" + Format(CDate(objtr.Price_Date), "MM/dd/yyyy") + "' AND MRP_Amt='" + clsCommon.myCstr(objtr.MRP_Amt) + "' AND Transfer_Basic_Amount='" + clsCommon.myCstr(objtr.Transfer_Basic_Amount) + "'"
            ''   clsDBFuncationality.ExecuteNonQuery(sql, trans)
        End If


        If clsCommon.CompairString(obj.Shipment_Type, "Sale") = CompairStringResult.Equal Then

            '--------------------------------update Item location details---------------------------------
            Dim countloop As Decimal = 0
            Dim shippedqty As Decimal = Math.Round(CDec(objtr.Shipped_Qty / convertFact), 2)

            Dim batchnumber As String = ""
            Dim balanceqty1 As Decimal = 0
            Dim applyqty As Decimal = 0
            Dim b As String = ""
            sql = "SELECT Item_Qty, Amount,Batch_No FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objtr.Item_Code + "' " & _
                " AND location_code='" + obj.Location + "' and MRP='" + CDec(objtr.MRP_Amt * convertFact).ToString() + "' and batch_no <> '' order by Expiry_Date asc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql, trans)
            If dt.Rows.Count > 0 Then
                For k As Integer = 0 To dt.Rows.Count - 1
                    If CDec(dt.Rows(k)("Item_Qty")) <> 0 Then
                        If CDec(dt.Rows(k)("Item_Qty")) <> 0 Or CDec(dt.Rows(k)("amount")) <> 0 Then
                            itemQty = CDec(dt.Rows(k)("Item_Qty"))
                            cogs = CDec(dt.Rows(k)("Amount"))
                            unitCogs = Math.Round(cogs / itemQty, 2)
                            batchnumber = Convert.ToString(dt.Rows(k)("Batch_No"))
                            If shippedqty > itemQty Then
                                applyqty = itemQty
                                shippedqty = shippedqty - itemQty
                                shipmentqty1 = shipmentqty1 + applyqty
                                shipmentamt1 = shipmentamt1 + cogs
                            Else
                                applyqty = shippedqty
                                shippedqty = shippedqty - applyqty
                                shipmentqty1 = shipmentqty1 + applyqty
                                shipmentamt1 = shipmentamt1 + Math.Round(cogs / itemQty * applyqty, 2)
                                shipmentcogs1 = Math.Round(shipmentamt1 / shipmentqty1, 2)
                            End If

                        End If
                        If applyqty > 0 Then
                            itemlocationqty = itemQty - applyqty
                            amount = cogs - (unitCogs * applyqty)
                            itemlocationqty = Math.Round(itemlocationqty, 2)
                            amount = Math.Round(amount, 2)
                            If itemlocationqty < 0.04 Then
                                itemlocationqty = 0
                                amount = 0
                            End If
                            sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(itemlocationqty) + "', " & _
                                "Amount='" + Convert.ToString(amount) + "' where Item_Code='" + objtr.Item_Code + "' " & _
                                " AND location_code='" + obj.Location + "' and MRP='" + (CDec(objtr.MRP_Amt) * convertFact).ToString() + "' and batch_no = '" + batchnumber + "'"
                            clsDBFuncationality.ExecuteNonQuery(sql, trans)
                        End If
                        If shippedqty = 0 Then
                            Exit For
                        End If
                    End If
                Next
                ''sql = "UPDATE TSPL_SHIPMENT_DETAILS SET Unit_COGS='" + shipmentcogs1.ToString() + "' WHERE Item_Code='" + objtr.Item_Code + "' AND " & _
                ''    " Shipment_No='" + shipmentNo + "' AND Unit_Code='" + objtr.Unit_code + "' AND " & _
                ''    " Price_Date='" + Format(CDate(objtr.Price_Date), "MM/dd/yyyy") + "' AND MRP_Amt='" + clsCommon.myCstr(objtr.MRP_Amt) + "' AND Transfer_Basic_Amount='" + clsCommon.myCstr(objtr.Transfer_Basic_Amount) + "'"
                ''clsDBFuncationality.ExecuteNonQuery(sql, trans)
            End If
            ''If clsCommon.myLen(obj.Order_No) > 0 Then
            ''    sql = "SELECT Balance_Qty from TSPL_SALES_ORDER_DETAILS WHERE Order_No='" + obj.Order_No + "' AND " & _
            ''" Item_Code='" + objtr.Item_Code + "' AND Unit_Code='" + objtr.Unit_code + "' AND " & _
            ''" Price_Date='" + Format(CDate(objtr.Price_Date), "MM/dd/yyyy") + "' AND MRP_Amt='" + clsCommon.myCstr(objtr.MRP_Amt) + "' AND OG_Basic_Amount='" + clsCommon.myCstr(objtr.Transfer_Basic_Amount) + "' "
            ''    Dim balanceqty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql, trans))
            ''    sql = "UPDATE TSPL_SALES_ORDER_DETAILS SET Balance_Qty='" + clsCommon.myCstr((balanceqty - CDec(objtr.Shipped_Qty)).ToString()) + "' WHERE Order_No='" + obj.Order_No + "' AND " & _
            ''" Item_Code='" + objtr.Item_Code + "' AND Unit_Code='" + objtr.Unit_code + "' AND " & _
            ''" Price_Date='" + Format(CDate(objtr.Price_Date), "MM/dd/yyyy") + "' AND MRP_Amt='" + clsCommon.myCstr(objtr.MRP_Amt) + "' AND OG_Basic_Amount='" + clsCommon.myCstr(objtr.Transfer_Basic_Amount) + "' "
            ''    clsDBFuncationality.ExecuteNonQuery(sql, trans)
            ''End If
        Else
            'FrmUtility.UpdateBalanceQtyAndBalanceQtyInBottleOFTransfer(obj.Transfer_No, trans)
            ' sql = "SELECT ISNULL( Pending_Balance_In_Bottle,0) from TSPL_TRANSFER_DETAIL WHERE Transfer_No='" + obj.Transfer_No + "' AND " & _
            ' " Item_Code='" + objtr.Item_Code + "'  AND MRP='" + (CDec(objtr.MRP_Amt) * convertFact).ToString() + "' "
            ' Dim balanceqtyInBottle As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql, trans))
            ' sql = "SELECT pending_Qty from TSPL_TRANSFER_DETAIL WHERE Transfer_No='" + obj.Transfer_No + "' AND " & _
            '" Item_Code='" + objtr.Item_Code + "'  AND MRP='" + (CDec(objtr.MRP_Amt) * convertFact).ToString() + "' "
            ' Dim balanceqty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql, trans))

            ' Dim shippedQty As Decimal = 0
            ' Dim otherconversion As Decimal
            ' Dim shippedQtyInBottle As Decimal = 0
            ' If convertFact = 1 Then
            '     otherconversion = clsDBFuncationality.getSingleValue("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(objtr.Item_Code) + "' and UOM_Code <> '" + Convert.ToString(objtr.Unit_code) + "' AND UM.Create_Price = 'Y'", trans)
            '     shippedQtyInBottle = CDec(objtr.Shipped_Qty) * otherconversion
            ' Else
            '     shippedQtyInBottle = CDec(objtr.Shipped_Qty)
            ' End If

            ' sql = "UPDATE TSPL_TRANSFER_DETAIL SET Pending_Qty='" + (balanceqty - CDec(objtr.Shipped_Qty) * 1 / convertFact).ToString() + "', Pending_Balance_In_Bottle ='" + Convert.ToString(balanceqtyInBottle - shippedQtyInBottle) + "' WHERE Transfer_No='" + obj.Transfer_No + "' AND " & _
            ' " Item_Code='" + objtr.Item_Code + "' AND MRP='" + (CDec(objtr.MRP_Amt) * convertFact).ToString() + "' "
            ' clsDBFuncationality.ExecuteNonQuery(sql, trans)
        End If
    End Sub

    Public Shared Sub updateUnitCogs(ByVal trans As SqlTransaction, ByVal obj As clsShipmentMaster, ByVal objtr As clsShipmentDetail, ByVal shipmentNo As String)
        Dim itemQty As Decimal = 0
        Dim cogs As Decimal = 0
        Dim unitCogs As Decimal = 0
        Dim shipmentCogs As Decimal = 0
        Dim itemlocationqty As Decimal = 0
        Dim amount As Decimal = 0
        Dim shipmentcogs1 As Decimal = 0
        Dim shipmentqty1 As Decimal = 0
        Dim shipmentamt1 As Decimal = 0
        Dim convertFact As Decimal = clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_code, trans)
        Dim sql As String = ""
        If clsCommon.CompairString(obj.Shipment_Type, "Sale") = CompairStringResult.Equal Then
            sql = "SELECT case when sum(isnull(Item_Qty,0))=0 then 0 else  sum(isnull(Amount,0))/sum(isnull(Item_Qty,0)) end  FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objtr.Item_Code + "' "
            sql += " AND location_code='" + obj.Location + "' and MRP='" + (CDec(objtr.MRP_Amt) * convertFact).ToString() + "'"
            unitCogs = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql, trans)), 2)


            Dim countloop As Decimal = 0
            Dim shippedqty As Decimal = Math.Round(CDec(objtr.Shipped_Qty / convertFact), 2)

            Dim batchnumber As String = ""
            Dim balanceqty1 As Decimal = 0
            Dim applyqty As Decimal = 0
            Dim b As String = ""
            sql = "SELECT Item_Qty, Amount,Batch_No FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + objtr.Item_Code + "' " & _
                " AND location_code='" + obj.Location + "' and MRP='" + CDec(objtr.MRP_Amt * convertFact).ToString() + "' and batch_no <> '' order by Expiry_Date asc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql, trans)
            If dt.Rows.Count > 0 Then
                For k As Integer = 0 To dt.Rows.Count - 1
                    If CDec(dt.Rows(k)("Item_Qty")) <> 0 Then
                        If CDec(dt.Rows(k)("Item_Qty")) <> 0 Or CDec(dt.Rows(k)("amount")) <> 0 Then
                            itemQty = CDec(dt.Rows(k)("Item_Qty"))
                            cogs = CDec(dt.Rows(k)("Amount"))
                            unitCogs = Math.Round(cogs / itemQty, 2)
                            batchnumber = Convert.ToString(dt.Rows(k)("Batch_No"))
                            If shippedqty > itemQty Then
                                applyqty = itemQty
                                shippedqty = shippedqty - itemQty
                                shipmentqty1 = shipmentqty1 + applyqty
                                shipmentamt1 = shipmentamt1 + cogs
                            Else
                                applyqty = shippedqty
                                shippedqty = shippedqty - applyqty
                                shipmentqty1 = shipmentqty1 + applyqty
                                shipmentamt1 = shipmentamt1 + Math.Round(cogs / itemQty * applyqty, 2)

                                If shipmentqty1 = 0 Then
                                    shipmentcogs1 = 0
                                Else
                                    shipmentcogs1 = Math.Round(shipmentamt1 / shipmentqty1, 2)
                                End If
                            End If
                        End If
                        If applyqty > 0 Then
                            itemlocationqty = itemQty - applyqty
                            amount = cogs - (unitCogs * applyqty)
                            itemlocationqty = Math.Round(itemlocationqty, 2)
                            amount = Math.Round(amount, 2)
                            If itemlocationqty < 0.04 Then
                                itemlocationqty = 0
                                amount = 0
                            End If
                        End If
                        If shippedqty = 0 Then
                            Exit For
                        End If
                    End If
                Next
                sql = "UPDATE TSPL_SHIPMENT_DETAILS SET Unit_COGS='" + shipmentcogs1.ToString() + "' WHERE Item_Code='" + objtr.Item_Code + "' AND " & _
                    " Shipment_No='" + shipmentNo + "' AND Unit_Code='" + objtr.Unit_code + "' AND " & _
                    " Price_Date='" + Format(CDate(objtr.Price_Date), "MM/dd/yyyy") + "' AND MRP_Amt='" + clsCommon.myCstr(objtr.MRP_Amt) + "' AND Transfer_Basic_Amount='" + clsCommon.myCstr(objtr.Transfer_Basic_Amount) + "'"

                clsDBFuncationality.ExecuteNonQuery(sql, trans)
            End If

        Else
            ''For Transfer
            sql = "SELECT Item_Price FROM TSPL_TRANSFER_DETAIL WHERE Item_Code='" + objtr.Item_Code + "' AND " & _
            " Transfer_No='" + obj.Transfer_No + "' AND MRP='" + (CDec(objtr.MRP_Amt) * convertFact).ToString() + "' "

            unitCogs = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql, trans))

            sql = "UPDATE TSPL_SHIPMENT_DETAILS SET Unit_COGS='" + unitCogs.ToString() + "' WHERE Item_Code='" + objtr.Item_Code + "' AND " & _
         " Shipment_No='" + shipmentNo + "' AND Unit_Code='" + objtr.Unit_code + "' AND " & _
         " Price_Date='" + Format(CDate(objtr.Price_Date), "MM/dd/yyyy") + "' AND MRP_Amt='" + clsCommon.myCstr(objtr.MRP_Amt) + "' AND Transfer_Basic_Amount='" + clsCommon.myCstr(objtr.Transfer_Basic_Amount) + "'"
            clsDBFuncationality.ExecuteNonQuery(sql, trans)
        End If
    End Sub

    Public Shared Function GetTranferForCompleteQuery(ByVal arr As ArrayList) As String

        Dim baseQty As String = "select TSPL_TRANSFER_HEAD.Transfer_No,CONVERT(varchar(11), TSPL_TRANSFER_HEAD.Transfer_Date,103) as Transfer_Date,TSPL_TRANSFER_HEAD.Route_No,TSPL_TRANSFER_HEAD.Route_Desc,Item_Code,TSPL_TRANSFER_DETAIL.Price_Date,Item_Qty,TSPL_TRANSFER_DETAIL.Uom,1 as RI ,0  as FOCItem,1 as Chk" + Environment.NewLine
        baseQty += " from TSPL_TRANSFER_DETAIL" + Environment.NewLine
        baseQty += " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_HEAD .Transfer_No=TSPL_TRANSFER_DETAIL.Transfer_No" + Environment.NewLine
        baseQty += " where TSPL_TRANSFER_DETAIL.Uom<> 'SH' and TSPL_TRANSFER_HEAD.Transfer_No in (" + clsCommon.GetMulcallString(arr) + ") " + Environment.NewLine
        baseQty += " union all " + Environment.NewLine
        baseQty += " select TSPL_SHIPMENT_MASTER.Transfer_No as Transfer_No,'' as Transfer_Date,'' as Route_No,'' as Route_Desc,TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.Price_Date,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as Item_Qty,TSPL_SALE_INVOICE_DETAIL.Unit_code ,2 as RI," + Environment.NewLine
        baseQty += " (CASE WHEN TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y' or TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item='Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' or TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 THEN 1 ELSE 0 END) as FOCItem ,0 as Chk" + Environment.NewLine
        baseQty += " from TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
        baseQty += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        baseQty += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No" + Environment.NewLine
        baseQty += " where TSPL_SHIPMENT_MASTER.Transfer_No in (" + clsCommon.GetMulcallString(arr) + ") " + Environment.NewLine
        baseQty += " union all " + Environment.NewLine
        baseQty += " select TSPL_TRANSFER_HEAD.Load_Out_No as Transfer_No,'' as Transfer_Date,'' as Route_No,'' as Route_Desc,TSPL_TRANSFER_DETAIL.Item_Code,TSPL_TRANSFER_DETAIL.Price_Date,(ISNULL( TSPL_TRANSFER_DETAIL.Burst,0)+isnull(TSPL_TRANSFER_DETAIL.Leak,0)+isnull(TSPL_TRANSFER_DETAIL.Shortage,0)+TSPL_TRANSFER_DETAIL.LoadIn_Qty) as Item_Qty,TSPL_TRANSFER_DETAIL.Uom  ,3 as RI,0  as FOCItem,0 as Chk" + Environment.NewLine
        baseQty += " from TSPL_TRANSFER_DETAIL " + Environment.NewLine
        baseQty += " left outer join TSPL_TRANSFER_HEAD on TSPL_TRANSFER_DETAIL.Transfer_No=TSPL_TRANSFER_HEAD.Transfer_No" + Environment.NewLine
        baseQty += " where TSPL_TRANSFER_DETAIL.Uom<> 'SH' and TSPL_TRANSFER_HEAD.Load_Out_No in (" + clsCommon.GetMulcallString(arr) + ") and Transfer_Type='LI'" + Environment.NewLine

        Dim qry As String = "select Transfer_No,MAX(Transfer_Date) as Transfer_Date,MAX(Route_No) as Route_No,max(Route_Desc) as Route_Desc,Item_Code" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =1 then 1 else 0 end  ),2)) as LoadoutQty" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =3 then 1 else 0 end  ),2)) as LoadinQty" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =1 then 1 else case when RI in (3) then -1 else 0 end end),2)) as ProposedSale" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =2 then 1 else 0 end  ),2)) as GrossSaleQty " + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =2 and FOCItem=1 then 1 else 0 end  ),2)) as DiscountQty " + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(sum( Item_Qty * case when RI =2 then 1 else 0 end)-sum(Item_Qty * case when RI =2 and FOCItem=1 then 1 else 0 end),2)) as NetSale" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(sum(Item_Qty * case when RI =1 then 1 else case when RI in (2,3) then -1 else 0 end end),2)) as BalanceQty" + Environment.NewLine
        qry += " from ( " + Environment.NewLine
        qry += " select xxx.Transfer_No,xxx.Transfer_Date,xxx.Route_No, xxx.Route_Desc,xxx.Item_Code,xxx.Price_Date,xxx.Item_Qty as OrgItem_Qty,xxx.Item_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as Item_Qty,'FC' as Uom,xxx.RI,xxx.FOCItem,xxx.Chk from (" + Environment.NewLine
        qry += baseQty
        qry += " ) xxx " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.Uom" + Environment.NewLine
        qry += " )xxxx" + Environment.NewLine
        qry += " group by Transfer_No,Item_Code " + Environment.NewLine
        qry += " having SUM(chk) > 0 " + Environment.NewLine

        Return qry
    End Function

    Public Shared Function GetTranferForComplete(ByVal arr As ArrayList, ByRef ArrCompleteTranfer As List(Of String)) As DataTable
        ArrCompleteTranfer = New List(Of String)

        Dim qry As String = GetTranferForCompleteQuery(arr)

        Dim qry1 = "select distinct Transfer_No from(" + qry + ")xxxxx where BalanceQty<>0"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)

        For Each strTrans As String In arr
            Dim isFound As Boolean = False
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.CompairString(strTrans, clsCommon.myCstr(dr("Transfer_No"))) = CompairStringResult.Equal Then
                        isFound = True
                        Exit For
                    End If
                Next
            End If
            If Not isFound Then
                ArrCompleteTranfer.Add(strTrans)
            End If
        Next
        Return clsDBFuncationality.GetDataTable(qry + " order by Transfer_Date,Transfer_No")
    End Function

    Public Shared Function GetTranferInvoices(ByVal strTransferNo As String, ByVal strItemCode As String) As DataTable
        Dim qry As String = "select xxx.Sale_Invoice_No as [Invoice No],max(xxx.Sale_Invoice_Date) as [Sale Invoie No],max(xxx.Route_No) as [Route Code], max(xxx.Route_Desc) as [Route],max(xxx.Cust_Code) as [Customer Code],max(xxx.Cust_Name) as Customer,xxx.Item_Code as [Item Code] " + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(SUM(Item_Qty * case when Unit_code='FC' and FOCItem=0  then 1 else 0 end),2)) as [Sale FC]" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(SUM(Item_Qty * case when Unit_code='FB' and FOCItem=0 then 1 else 0 end),2)) as [Sale FB]" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(SUM(ItemQtyinFC * case when FOCItem=0 then 1 else 0 end),2)) as [Sale Total]" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(SUM(Item_Qty * case when Unit_code='FC' and FOCItem=1 then 1 else 0 end),2)) as [FOC FC]" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(SUM(Item_Qty * case when Unit_code='FB' and FOCItem=1 then 1 else 0 end),2)) as [FOC FB]" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(SUM(ItemQtyinFC * case when FOCItem=1 then 1 else 0 end),2)) as [FOC Total]" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(SUM(Item_Qty * case when Unit_code='FC' then 1 else 0 end),2)) as [Total FC]" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(SUM(Item_Qty * case when Unit_code='FB' then 1 else 0 end),2)) as [Total FB]" + Environment.NewLine
        qry += " ,CONVERT(decimal(18,2), ROUND(SUM(ItemQtyinFC),2)) as [Total]" + Environment.NewLine
        qry += " from (" + Environment.NewLine
        qry += " select  TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No,CONVERT(varchar(11), TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)  as Sale_Invoice_Date,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Route_Desc,TSPL_SALE_INVOICE_HEAD.Cust_Code, TSPL_SALE_INVOICE_HEAD.Cust_Name,TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty  as Item_Qty,TSPL_SALE_INVOICE_DETAIL.Invoice_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor as ItemQtyinFC,TSPL_SALE_INVOICE_DETAIL.Unit_code,(CASE WHEN TSPL_SALE_INVOICE_DETAIL.Scheme_Item='Y' or TSPL_SALE_INVOICE_DETAIL.Promo_Scheme_Item='Y' or TSPL_SALE_INVOICE_DETAIL.Sampling_Item='Y' or TSPL_SALE_INVOICE_HEAD.Inv_Disc_Percent=100 THEN 1 ELSE 0 END) as FOCItem  " + Environment.NewLine
        qry += " from TSPL_SALE_INVOICE_DETAIL " + Environment.NewLine
        qry += " left outer join TSPL_SALE_INVOICE_HEAD on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" + Environment.NewLine
        qry += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No" + Environment.NewLine
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code" + Environment.NewLine
        qry += " where TSPL_SHIPMENT_MASTER.Transfer_No ='" + strTransferNo + "' and  TSPL_SALE_INVOICE_DETAIL.Item_Code='" + strItemCode + "'" + Environment.NewLine
        qry += " ) xxx group by Sale_Invoice_No,Item_Code order by  Sale_Invoice_No,Item_Code"
        Return clsDBFuncationality.GetDataTable(qry)
    End Function
End Class

Public Class clsShipmentDetail
#Region "Varibales"
    Public Shipment_Id As Integer = Nothing
    Public Shipment_No As String = Nothing
    Public Complete As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Price_Date As Date = Nothing
    Public Order_Qty As Double = Nothing
    Public Shipped_Qty As Double = Nothing
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
    Public Empty_Value As Double = Nothing
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
    Public TPT As Double = Nothing
    Public Total_Item_Amt As Double = Nothing
    Public Total_TPT As Double = Nothing
    Public Unit_COGS As Double = Nothing
    Public From_Scheme_Code As String = Nothing
    Public Abatement As Double = Nothing
    Public Empty_Value_Shell As Double = Nothing
    Public Empty_Value_Bottle As Double = Nothing
    Public Transfer_Basic_Amount As Double = Nothing
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
    Public Batch_No As String = Nothing
    Public Sample_Scheme_Code As String = Nothing
    Public Main_Item As String = Nothing
    Public Discount_Code As String = Nothing
    Public Cust_Item_Discount_NoTax As Double = Nothing
    Public Target_Discount_Amt As Double = 0
    Public Price_Date_Actual As Date? = Nothing
    Public Price_To_Show As Double = 0
    Public RAW_Qty As Double = Nothing
    Public Converted_Qty As Double = Nothing
    Public OZ_Qty As Double = Nothing
    Public Abatement_rate As Double = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsShipmentDetail), ByVal trans As SqlTransaction) As Boolean
        Dim sno As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsShipmentDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Shipment_Id", sno)
                sno += 1
                clsCommon.AddColumnsForChange(coll, "Shipment_No", strDocNo)

                clsCommon.AddColumnsForChange(coll, "Complete", obj.Complete)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))

                clsCommon.AddColumnsForChange(coll, "Order_Qty", obj.Order_Qty)
                clsCommon.AddColumnsForChange(coll, "Shipped_Qty", obj.Shipped_Qty)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Price_code", obj.Price_code)
                clsCommon.AddColumnsForChange(coll, "Scheme_Applicable", obj.Scheme_Applicable)
                clsCommon.AddColumnsForChange(coll, "Scheme_Code_Qty", obj.Scheme_Code_Qty)
                clsCommon.AddColumnsForChange(coll, "Scheme_Item", obj.Scheme_Item)

                clsCommon.AddColumnsForChange(coll, "Promo_Scheme_Applicable", obj.Promo_Scheme_Applicable)
                clsCommon.AddColumnsForChange(coll, "Promo_Scheme_Code", obj.Promo_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Promo_Scheme_Item", obj.Promo_Scheme_Item)
                clsCommon.AddColumnsForChange(coll, "Scheme_Disc_Applicable", obj.Scheme_Disc_Applicable)
                clsCommon.AddColumnsForChange(coll, "Scheme_Code_Cash", obj.Scheme_Code_Cash)
                clsCommon.AddColumnsForChange(coll, "Sampling_Item", obj.Sampling_Item)
                clsCommon.AddColumnsForChange(coll, "Empty_Value", obj.Empty_Value)
                clsCommon.AddColumnsForChange(coll, "MRP_Amt", obj.MRP_Amt)
                clsCommon.AddColumnsForChange(coll, "Basic_Rate", obj.Basic_Rate)
                clsCommon.AddColumnsForChange(coll, "Item_Assessable_Rate", obj.Item_Assessable_Rate)
                clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)


                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Assessable_Amt", obj.TAX1_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                clsCommon.AddColumnsForChange(coll, "TAX2_Assessable_Amt", obj.TAX2_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                clsCommon.AddColumnsForChange(coll, "TAX3_Assessable_Amt", obj.TAX3_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                clsCommon.AddColumnsForChange(coll, "TAX4_Assessable_Amt", obj.TAX4_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                clsCommon.AddColumnsForChange(coll, "TAX5_Assessable_Amt", obj.TAX5_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                clsCommon.AddColumnsForChange(coll, "TAX6_Assessable_Amt", obj.TAX6_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                clsCommon.AddColumnsForChange(coll, "TAX7_Assessable_Amt", obj.TAX7_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                clsCommon.AddColumnsForChange(coll, "TAX8_Assessable_Amt", obj.TAX8_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                clsCommon.AddColumnsForChange(coll, "TAX9_Assessable_Amt", obj.TAX9_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                clsCommon.AddColumnsForChange(coll, "TAX10_Assessable_Amt", obj.TAX10_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Tax", obj.Item_Tax)
                clsCommon.AddColumnsForChange(coll, "Total_Assessable_Amt", obj.Total_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_MRP_Amt", obj.Total_MRP_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Total_Basic_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Disc_Amt", obj.Total_Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_net_Amt", obj.Total_net_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Price_To_Show", obj.Price_To_Show)

                clsCommon.AddColumnsForChange(coll, "TPT", obj.TPT)
                clsCommon.AddColumnsForChange(coll, "Total_Item_Amt", obj.Total_Item_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_TPT", obj.Total_TPT)
                clsCommon.AddColumnsForChange(coll, "Unit_COGS", obj.Unit_COGS)
                clsCommon.AddColumnsForChange(coll, "From_Scheme_Code", obj.From_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Abatement", obj.Abatement)
                clsCommon.AddColumnsForChange(coll, "Empty_Value_Shell", obj.Empty_Value_Shell)
                clsCommon.AddColumnsForChange(coll, "Empty_Value_Bottle", obj.Empty_Value_Bottle)
                clsCommon.AddColumnsForChange(coll, "Transfer_Basic_Amount", obj.Transfer_Basic_Amount)
                clsCommon.AddColumnsForChange(coll, "Cust_Discount", obj.Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Total_Cust_Discount", obj.Total_Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Level1_User_Code", obj.Level1_User_Code)
                clsCommon.AddColumnsForChange(coll, "Level2_User_Code", obj.Level2_User_Code)
                clsCommon.AddColumnsForChange(coll, "Level3_User_Code", obj.Level3_User_Code)
                clsCommon.AddColumnsForChange(coll, "Level4_User_Code", obj.Level4_User_Code)
                clsCommon.AddColumnsForChange(coll, "Level5_User_Code", obj.Level5_User_Code)
                clsCommon.AddColumnsForChange(coll, "Level1_User_Commission", obj.Level1_User_Commission)
                clsCommon.AddColumnsForChange(coll, "Level2_User_Commission", obj.Level2_User_Commission)
                clsCommon.AddColumnsForChange(coll, "Level3_User_Commission", obj.Level3_User_Commission)
                clsCommon.AddColumnsForChange(coll, "Level4_User_Commission", obj.Level4_User_Commission)
                clsCommon.AddColumnsForChange(coll, "Level5_User_Commission", obj.Level5_User_Commission)
                clsCommon.AddColumnsForChange(coll, "Level1_User_Comm_Amount", obj.Level1_User_Comm_Amount)
                clsCommon.AddColumnsForChange(coll, "Level2_User_Comm_Amount", obj.Level2_User_Comm_Amount)
                clsCommon.AddColumnsForChange(coll, "Level3_User_Comm_Amount", obj.Level3_User_Comm_Amount)
                clsCommon.AddColumnsForChange(coll, "Level4_User_Comm_Amount", obj.Level4_User_Comm_Amount)
                clsCommon.AddColumnsForChange(coll, "Level5_User_Comm_Amount", obj.Level5_User_Comm_Amount)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                clsCommon.AddColumnsForChange(coll, "Sample_Scheme_Code", obj.Sample_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Main_Item", obj.Main_Item)
                clsCommon.AddColumnsForChange(coll, "Discount_Code", obj.Discount_Code)
                clsCommon.AddColumnsForChange(coll, "Cust_Item_Discount_NoTax", obj.Cust_Item_Discount_NoTax)
                clsCommon.AddColumnsForChange(coll, "Target_Discount_Amt", obj.Target_Discount_Amt)
                If obj.Price_Date_Actual.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Price_Date_Actual", clsCommon.GetPrintDate(obj.Price_Date_Actual, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "RAW_Qty", obj.RAW_Qty)
                clsCommon.AddColumnsForChange(coll, "Converted_Qty", obj.Converted_Qty)
                clsCommon.AddColumnsForChange(coll, "OZ_Qty", obj.OZ_Qty)
                clsCommon.AddColumnsForChange(coll, "Abatement_rate", obj.Abatement_rate)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIPMENT_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function


End Class
