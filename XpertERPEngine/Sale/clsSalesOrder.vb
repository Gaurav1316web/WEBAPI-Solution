Imports common
Imports System.Data.SqlClient

Public Class clsSalesOrder
#Region "Varibales"
    Public CSRemarks As String = Nothing
    Public CSComments As String = Nothing
    Public CheckSlipNo As String = Nothing
    Public CheckSlip_Date As DateTime = Nothing
    Public Order_No As String = Nothing
    Public Order_Date As DateTime = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Expected_Ship_Date As DateTime
    Public Ship_To As String
    Public Ship_To_Desc As String
    Public Cust_PONo As String = Nothing
    Public On_Hold As Boolean = False
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
    Public Order_Disc_Percent As Double = Nothing
    Public Order_Discount_Amt As Double = Nothing
    Public Order_Detail_Disc_Amt As Double = Nothing
    Public Order_Detail_Total_Amt As Double = Nothing
    Public Order_Tax_Amt As Double = Nothing
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
    Public Level1_User_code As String = Nothing
    Public Level2_User_code As String = Nothing
    Public Level3_User_code As String = Nothing
    Public Level4_User_code As String = Nothing
    Public Level5_User_code As String = Nothing
    Public Comp_Code As String = Nothing
    Public Is_Post As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_Date As DateTime? = Nothing
    Public Total_TPT As Double = Nothing
    Public Level1_User_Commission As Double = Nothing
    Public Level2_User_Commission As Double = Nothing
    Public Level3_User_Commission As Double = Nothing
    Public Level4_User_Commission As Double = Nothing
    Public Level5_User_Commission As Double = Nothing
    Public Empty_Value As Double = Nothing
    Public Shell_Qty As Double = Nothing
    Public Employee_Code As String = Nothing
    Public is_Sample As Integer = Nothing
    Public Total_Disc_Percent As Double = Nothing
    Public Tax_Recoverable_Amt As Double = Nothing
    Public Tax_Recoverable_Amt2 As Double = Nothing
    Public Tax_Recoverable_Amt3 As Double = Nothing
    Public Tot_Customer_Dis_Amt As Double = Nothing

    Public Total_Tonnage As Double = Nothing
    Public Payment_Amount As Double = 0
    Public Is_Scheduled As Integer = 0
    Public Against_C_Form As Boolean = False
    Public Route_Type_Id As String = Nothing
    Public TAX_GROUP_TYPE As String = Nothing
    Public Payment_Date As Date? = Nothing
    Public Discount_On As Integer = 0
    Public Status As String = Nothing
    Public Arr As List(Of clsSalesOrderDetail) = Nothing
    Public Arr1 As List(Of clsSalesOrder) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsSalesOrder, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsSalesOrder, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_SALES_ORDER_DETAIL where Order_No='" + obj.Order_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Order_No = clsERPFuncationality.GetNextCode(trans, obj.Order_Date, clsDocType.SaleOrder, "", obj.Location)
            End If
            If (clsCommon.myLen(obj.Order_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "Order_Date", clsCommon.GetPrintDate(obj.Order_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_Name", obj.Cust_Name)
            clsCommon.AddColumnsForChange(coll, "Expected_Ship_Date", clsCommon.GetPrintDate(obj.Expected_Ship_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Ship_To", obj.Ship_To)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Desc", obj.Ship_To_Desc)
            clsCommon.AddColumnsForChange(coll, "Cust_PONo", obj.Cust_PONo)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
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
            clsCommon.AddColumnsForChange(coll, "Order_Disc_Percent", obj.Order_Disc_Percent)
            clsCommon.AddColumnsForChange(coll, "Order_Discount_Amt", obj.Order_Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Order_Detail_Disc_Amt", obj.Order_Detail_Disc_Amt)
            clsCommon.AddColumnsForChange(coll, "Order_Detail_Total_Amt", obj.Order_Detail_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Order_Tax_Amt", obj.Order_Tax_Amt)
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
            clsCommon.AddColumnsForChange(coll, "Total_TPT", obj.Total_TPT)
            clsCommon.AddColumnsForChange(coll, "Level1_User_Commission", obj.Level1_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level2_User_Commission", obj.Level2_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level3_User_Commission", obj.Level3_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level4_User_Commission", obj.Level4_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level5_User_Commission", obj.Level5_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Empty_Value", obj.Empty_Value)
            clsCommon.AddColumnsForChange(coll, "Shell_Qty", obj.Shell_Qty)
            clsCommon.AddColumnsForChange(coll, "Employee_Code", obj.Employee_Code)
            clsCommon.AddColumnsForChange(coll, "is_Sample", obj.is_Sample)
            clsCommon.AddColumnsForChange(coll, "Total_Disc_Percent", obj.Total_Disc_Percent)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable_Amt", obj.Tax_Recoverable_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable_Amt2", obj.Tax_Recoverable_Amt2)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable_Amt3", obj.Tax_Recoverable_Amt3)
            clsCommon.AddColumnsForChange(coll, "Tot_Customer_Dis_Amt", obj.Tot_Customer_Dis_Amt)

            clsCommon.AddColumnsForChange(coll, "Is_Scheduled", obj.Is_Scheduled)
            clsCommon.AddColumnsForChange(coll, "Against_C_Form", IIf(obj.Against_C_Form, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Route_Type_Id", obj.Route_Type_Id)
            clsCommon.AddColumnsForChange(coll, "TAX_GROUP_TYPE", obj.TAX_GROUP_TYPE)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If obj.Payment_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Payment_Date", clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Payment_Date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Payment_Amount", obj.Payment_Amount)


            clsCommon.AddColumnsForChange(coll, "Total_Tonnage", obj.Total_Tonnage)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Order_No", obj.Order_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_ORDER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_ORDER_HEAD", OMInsertOrUpdate.Update, "TSPL_SALES_ORDER_HEAD.Order_No='" + obj.Order_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsSalesOrderDetail.SaveData(obj.Order_No, Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetDataCheckSlip(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsSalesOrder
        Return GetDataCheckSlip(strRetCode, NavType, Nothing)
    End Function

    Public Shared Function GetDataCheckSlip(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSalesOrder
        Dim obj As clsSalesOrder = Nothing
        Dim qry As String = "select CheckSlipNo,CheckSlip_Date,Cust_Code,Cust_Name,Salesman_Code,Vehicle_Code,Vehicle_No,CSComments,CSRemarks,Location from TSPL_SALES_ORDER_HEAD where  CheckSlipNo is not null or CheckSlipNo <> ''"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SALES_ORDER_HEAD.CheckSlipNo = (select MIN(CheckSlipNo) from TSPL_SALES_ORDER_HEAD)"
            Case NavigatorType.Last
                qry += " and TSPL_SALES_ORDER_HEAD.CheckSlipNo = (select Max(CheckSlipNo) from TSPL_SALES_ORDER_HEAD)"
            Case NavigatorType.Next
                qry += " and TSPL_SALES_ORDER_HEAD.CheckSlipNo = (select Min(CheckSlipNo) from TSPL_SALES_ORDER_HEAD where GPCode>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_SALES_ORDER_HEAD.CheckSlipNo = (select Max(CheckSlipNo) from TSPL_SALES_ORDER_HEAD where GPCode<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_SALES_ORDER_HEAD.CheckSlipNo = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsSalesOrder()
            obj.CheckSlipNo = clsCommon.myCstr(dt.Rows(0)("CheckSlipNo"))
            obj.CheckSlip_Date = clsCommon.myCDate(dt.Rows(0)("CheckSlip_Date"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
            obj.Salesman_Code = clsCommon.myCstr(dt.Rows(0)("Salesman_Code"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.CSComments = clsCommon.myCstr(dt.Rows(0)("CSComments"))
            obj.CSRemarks = clsCommon.myCstr(dt.Rows(0)("CSRemarks"))
            'obj.Post = clsCommon.myCstr(dt.Rows(0)("Post"))
            obj.Location = clsCommon.myCstr(dt.Rows(0)("Location"))

            obj.Arr1 = clsSalesOrder.GetDataCheckSlipDetail(obj.CheckSlipNo, obj.CheckSlip_Date, trans)

        End If
        Return obj
    End Function
    Public Shared Function GetDataCheckSlipDetail(ByVal strCode As String, ByVal strdate As Date, ByVal trans As SqlTransaction) As List(Of clsSalesOrder)
        Dim arr As List(Of clsSalesOrder) = Nothing

        Dim qry As String = "select 1 as Status,Order_No,Order_Date,Emp_Name,Cust_Name,Route_Desc,Location from TSPL_SALES_ORDER_HEAD left outer join TSPL_EMPLOYEE_MASTER on TSPL_SALES_ORDER_HEAD.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE where CheckSlipNo='" & strCode & "'" & _
        " union all select 0 as Status,Order_No,Order_Date,Emp_Name,Cust_Name,Route_Desc,Location from TSPL_SALES_ORDER_HEAD left outer join TSPL_EMPLOYEE_MASTER on TSPL_SALES_ORDER_HEAD.Salesman_Code=TSPL_EMPLOYEE_MASTER.EMP_CODE where  convert(date,Order_Date,103)='" & clsCommon.GetPrintDate(strdate, "dd/MMM/yyyy") & "' and  CheckSlipNo is null "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsSalesOrder)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsSalesOrder = New clsSalesOrder()
                obj.Order_No = clsCommon.myCstr(dr("Order_No"))
                obj.Order_Date = clsCommon.myCstr(dr("Order_Date"))
                obj.Salesman_Code = clsCommon.myCstr(dr("Emp_Name"))
                obj.Cust_Name = clsCommon.myCstr(dr("Cust_Name"))
                obj.Route_Desc = clsCommon.myCstr(dr("Route_Desc"))
                obj.Location = clsCommon.myCstr(dr("Location"))
                obj.Status = clsCommon.myCstr(dr("Status"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal Trans As SqlTransaction) As clsSalesOrder
        Return GetData(strCode, NavigatorType.Current, Trans)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal Trans As SqlTransaction) As clsSalesOrder
        Dim obj As clsSalesOrder = Nothing
        Dim strLocation As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) Then
            strLocation += " and TSPL_SALES_ORDER_HEAD.Location  in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Dim qry As String = "select * from TSPL_SALES_ORDER_HEAD where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SALES_ORDER_HEAD.Order_No = (select MIN(Order_No) from TSPL_SALES_ORDER_HEAD where 2=2 " + strLocation + ")"
            Case NavigatorType.Last
                qry += " and TSPL_SALES_ORDER_HEAD.Order_No = (select Max(Order_No) from TSPL_SALES_ORDER_HEAD where 2=2 " + strLocation + ")"
            Case NavigatorType.Current
                qry += " and TSPL_SALES_ORDER_HEAD.Order_No = '" + strCode + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SALES_ORDER_HEAD.Order_No = (select Min(Order_No) from TSPL_SALES_ORDER_HEAD where Order_No>'" + strCode + "' " + strLocation + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_SALES_ORDER_HEAD.Order_No = (select Max(Order_No) from TSPL_SALES_ORDER_HEAD where Order_No<'" + strCode + "' " + strLocation + ")"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsSalesOrder
            obj.Order_No = clsCommon.myCstr(dt.Rows(0)("Order_No"))
            If dt.Rows(0)("Order_Date") IsNot DBNull.Value Then
                obj.Order_Date = clsCommon.myCDate(dt.Rows(0)("Order_Date"))
            End If
            obj.Order_No = clsCommon.myCstr(dt.Rows(0)("Order_No"))
            obj.Order_Date = clsCommon.myCDate(dt.Rows(0)("Order_Date"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
            obj.Expected_Ship_Date = clsCommon.myCDate(dt.Rows(0)("Expected_Ship_Date"))
            obj.Ship_To = clsCommon.myCstr(dt.Rows(0)("Ship_To"))
            obj.Ship_To_Desc = clsCommon.myCstr(dt.Rows(0)("Ship_To_Desc"))
            obj.Cust_PONo = clsCommon.myCstr(dt.Rows(0)("Cust_PONo"))

            obj.Is_Post = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Post")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Post_Date") IsNot DBNull.Value Then
                obj.Post_Date = clsCommon.myCDate(dt.Rows(0)("Post_Date"))
            End If
            If dt.Rows(0)("Payment_Date") IsNot DBNull.Value Then
                obj.Payment_Date = clsCommon.myCDate(dt.Rows(0)("Payment_Date"))
            End If
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
            obj.Order_Disc_Percent = clsCommon.myCdbl(dt.Rows(0)("Order_Disc_Percent"))
            obj.Order_Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Order_Discount_Amt"))
            obj.Order_Detail_Disc_Amt = clsCommon.myCdbl(dt.Rows(0)("Order_Detail_Disc_Amt"))
            obj.Order_Detail_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Order_Detail_Total_Amt"))
            obj.Order_Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Order_Tax_Amt"))
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
            obj.Level1_User_code = clsCommon.myCstr(dt.Rows(0)("Level1_User_code"))
            obj.Level2_User_code = clsCommon.myCstr(dt.Rows(0)("Level2_User_code"))
            obj.Level3_User_code = clsCommon.myCstr(dt.Rows(0)("Level3_User_code"))
            obj.Level4_User_code = clsCommon.myCstr(dt.Rows(0)("Level4_User_code"))
            obj.Level5_User_code = clsCommon.myCstr(dt.Rows(0)("Level5_User_code"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Is_Post = clsCommon.myCstr(dt.Rows(0)("Is_Post"))
            obj.Against_C_Form = IIf(clsCommon.myCdbl(dt.Rows(0)("Against_C_Form")) = 1, True, False)
            obj.Total_TPT = clsCommon.myCdbl(dt.Rows(0)("Total_TPT"))
            obj.Discount_On = clsCommon.myCdbl(dt.Rows(0)("Discount_On"))
            obj.Level1_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level1_User_Commission"))
            obj.Level2_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level2_User_Commission"))
            obj.Level3_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level3_User_Commission"))
            obj.Level4_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level4_User_Commission"))
            obj.Level5_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level5_User_Commission"))
            obj.Empty_Value = clsCommon.myCdbl(dt.Rows(0)("Empty_Value"))
            obj.Shell_Qty = clsCommon.myCdbl(dt.Rows(0)("Shell_Qty"))
            obj.Employee_Code = clsCommon.myCstr(dt.Rows(0)("Employee_Code"))
            obj.is_Sample = clsCommon.myCdbl(dt.Rows(0)("is_Sample"))
            obj.Total_Disc_Percent = clsCommon.myCdbl(dt.Rows(0)("Total_Disc_Percent"))
            obj.Tax_Recoverable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt"))
            obj.Tax_Recoverable_Amt2 = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt2"))
            obj.Tax_Recoverable_Amt3 = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt3"))
            obj.Tot_Customer_Dis_Amt = clsCommon.myCdbl(dt.Rows(0)("Tot_Customer_Dis_Amt"))

            obj.Is_Scheduled = clsCommon.myCdbl(dt.Rows(0)("Is_Scheduled"))
            obj.Route_Type_Id = clsCommon.myCstr(dt.Rows(0)("Route_Type_Id"))
            obj.Payment_Amount = clsCommon.myCdbl(dt.Rows(0)("Payment_Amount"))
            obj.Total_Tonnage = clsCommon.myCdbl(dt.Rows(0)("Total_Tonnage"))

            qry = "select * from TSPL_SALES_ORDER_DETAIL where Order_No='" + obj.Order_No + "' order by SNo"
            dt = clsDBFuncationality.GetDataTable(qry, Trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Arr = New List(Of clsSalesOrderDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As clsSalesOrderDetail = New clsSalesOrderDetail()
                    objtr.SNo = clsCommon.myCdbl(dr("SNo"))
                    objtr.Order_No = clsCommon.myCstr(dr("Order_No"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objtr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                    objtr.Order_Qty = clsCommon.myCdbl(dr("Order_Qty"))
                    objtr.Order_Qty = clsCommon.myCdbl(dr("Order_Qty"))
                    objtr.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                    objtr.Unit_code = clsCommon.myCstr(dr("Unit_code"))
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

                    objtr.Tonnage_Per_Unit = clsCommon.myCdbl(dr("Tonnage_Per_Unit"))
                    objtr.Tonnage = clsCommon.myCdbl(dr("Tonnage"))

                    If dr("Price_Date_Actual") IsNot DBNull.Value Then
                        objtr.Price_Date_Actual = clsCommon.myCDate(dr("Price_Date_Actual"))
                    End If
                    obj.Arr.Add(objtr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Sales Order No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsSalesOrder = clsSalesOrder.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Order_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Is_Post = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Post_Date)
            End If
            If (obj.On_Hold) Then
                Throw New Exception("Sales order No " + obj.Order_No + " Is On Hold.Can't Post it")
            End If
            Dim qry As String = "Update TSPL_SALES_ORDER_HEAD set Is_Post=1, Post_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Order_No='" + strDocNo + "'"
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
            Throw New Exception("Purchase Order No not found to Delete")
        End If
        Dim obj As clsSalesOrder = clsSalesOrder.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Order_No) > 0) Then
            Try
                If (obj.Is_Post = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Post_Date, "dd/MM/yyyy"))
                End If
                Dim qry As String = "delete from TSPL_SALES_ORDER_DETAIL where order_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SALES_ORDER_HEAD where order_No='" + strCode + "'"
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
    Public Shared Function CheckCode(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select count(Order_No) from TSPL_SALES_ORDER_HEAD where Order_No='" + strCode + "' "
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
End Class

Public Class clsSalesOrderDetail
#Region "Varibales"
    Public Order_No As String = Nothing
    Public SNo As Integer = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Price_Date As Date = Nothing
    Public Order_Qty As Double = Nothing
    Public Balance_Qty As Double = Nothing
    Public Unit_code As String = Nothing
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
    Public Tonnage_Per_Unit As Double = Nothing
    Public Tonnage As Double = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsSalesOrderDetail), ByVal trans As SqlTransaction) As Boolean
        Dim sno As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSalesOrderDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "sno", sno)
                sno += 1
                clsCommon.AddColumnsForChange(coll, "Order_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))

                clsCommon.AddColumnsForChange(coll, "Order_Qty", obj.Order_Qty)
                clsCommon.AddColumnsForChange(coll, "Balance_Qty", obj.Balance_Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
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

                clsCommon.AddColumnsForChange(coll, "Tonnage_Per_Unit", obj.Tonnage_Per_Unit)
                clsCommon.AddColumnsForChange(coll, "Tonnage", obj.Tonnage)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_ORDER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetBalanceQty(ByVal strOrderNo As String, ByVal strItemCode As String, ByVal strUOMCode As String, ByVal strShipmentNo As String, ByVal strFrom_Scheme_Code As String) As Double
        Dim qry As String = "select SUM(Qty*RI) as BalanceQty from( "
        qry += " select TSPL_SALES_ORDER_DETAIL.Order_Qty as Qty,1 as RI  from TSPL_SALES_ORDER_DETAIL"
        qry += " left outer join TSPL_SALES_ORDER_HEAD on TSPL_SALES_ORDER_HEAD.Order_No=TSPL_SALES_ORDER_DETAIL.Order_No"
        qry += " where TSPL_SALES_ORDER_HEAD.Order_No='" + strOrderNo + "' and Item_Code='" + strItemCode + "' and Unit_code='" + strUOMCode + "' and From_Scheme_Code='" + strFrom_Scheme_Code + "'"
        qry += " union all "
        qry += " select TSPL_SHIPMENT_DETAILS.Shipped_Qty as Qty ,-1 as RI from TSPL_SHIPMENT_DETAILS"
        qry += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No"
        qry += " where TSPL_SHIPMENT_MASTER.Order_No='" + strOrderNo + "' and Item_Code='" + strItemCode + "' and Unit_code='" + strUOMCode + "' and From_Scheme_Code='" + strFrom_Scheme_Code + "' and TSPL_SHIPMENT_MASTER.Shipment_No not in ('" + strShipmentNo + "') and not( TSPL_SHIPMENT_DETAILS.Scheme_Item='Y' or TSPL_SHIPMENT_DETAILS.Promo_Scheme_Item='Y' or TSPL_SHIPMENT_DETAILS.Sampling_Item='Y' )"
        qry += " )xxx"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    End Function
End Class
