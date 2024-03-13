Imports common
Imports System.Data.SqlClient
'----------by vipin for price code 1 to 10 update on 27/11/2012
'-28/11/2012-12:47PM--Updation By--Pankaj Kumar--Added New Column [] in TSPL_SALE_RETURN_INTER_HEAD And TSPL_SALE_RETURN_INTER_DETAIL Ans LineNo In Detail----
Public Class clsSaleReturnInterCompany
#Region "Varibales"
    Public Document_No As String = Nothing
    Public Document_Date As Date = Nothing
    Public Document_Type As Integer = 0
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public On_Hold As Boolean = False
    Public Ref_No As String = Nothing
    Public Description As String = Nothing
    Public Remarks As String = Nothing
    Public Price_Code As String = Nothing
    Public Location As String = Nothing
    Public Cust_Account As String = Nothing
    Public Tax_Group As String = Nothing
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
    Public Discount_Amt As Double = Nothing
    Public Detail_Disc_Amt As Double = Nothing
    Public Detail_Total_Amt As Double = Nothing
    Public Tax_Amt As Double = Nothing
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
    Public Is_Post As String = Nothing
    Public Post_Date As DateTime? = Nothing
    Public Total_TPT As Double = Nothing
    Public Level1_User_Commission As Double = Nothing
    Public Level2_User_Commission As Double = Nothing
    Public Level3_User_Commission As Double = Nothing
    Public Level4_User_Commission As Double = Nothing
    Public Level5_User_Commission As Double = Nothing
    Public Empty_Value As Double = Nothing
    Public Shell_Qty As Double = Nothing
    Public Customer_Invoice_No As String = Nothing
    Public Employee_Code As String = Nothing
    Public Total_Disc_Percent As Double = Nothing
    Public Tax_Recoverable_Amt As Double = Nothing
    Public Tax_Recoverable_Amt2 As Double = Nothing
    Public Tax_Recoverable_Amt3 As Double = Nothing
    Public Tot_Customer_Dis_Amt As Double = Nothing
    Public Ship_To As String = Nothing
    Public Ship_To_Desc As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As Date = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date = Nothing
    Public Sale_Account_Amount As Double = Nothing

    Public Arr As List(Of clsSaleReturnInterCompanyDetail) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsSaleReturnInterCompany, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Sale Return (Inter Company)", obj.Location, obj.Document_Date, trans)

            Dim qry As String = "delete from TSPL_SALE_RETURN_INTER_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.SalesReturnInterCompany, "", obj.Location)
            End If
            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_Name", obj.Cust_Name)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Cust_Account", obj.Cust_Account)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
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
            clsCommon.AddColumnsForChange(coll, "Discount_Amt", obj.Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Detail_Disc_Amt", obj.Detail_Disc_Amt)
            clsCommon.AddColumnsForChange(coll, "Detail_Total_Amt", obj.Detail_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax_Amt", obj.Tax_Amt)
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
            'clsCommon.AddColumnsForChange(coll, "Is_Post", obj.Is_Post)
            'clsCommon.AddColumnsForChange(coll, "Post_Date", obj.Post_Date)
            clsCommon.AddColumnsForChange(coll, "Total_TPT", obj.Total_TPT)
            clsCommon.AddColumnsForChange(coll, "Level1_User_Commission", obj.Level1_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level2_User_Commission", obj.Level2_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level3_User_Commission", obj.Level3_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level4_User_Commission", obj.Level4_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Level5_User_Commission", obj.Level5_User_Commission)
            clsCommon.AddColumnsForChange(coll, "Empty_Value", obj.Empty_Value)
            clsCommon.AddColumnsForChange(coll, "Shell_Qty", obj.Shell_Qty)
            clsCommon.AddColumnsForChange(coll, "Customer_Invoice_No", obj.Customer_Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Employee_Code", obj.Employee_Code)
            clsCommon.AddColumnsForChange(coll, "Total_Disc_Percent", obj.Total_Disc_Percent)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable_Amt", obj.Tax_Recoverable_Amt)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable_Amt2", obj.Tax_Recoverable_Amt2)
            clsCommon.AddColumnsForChange(coll, "Tax_Recoverable_Amt3", obj.Tax_Recoverable_Amt3)
            clsCommon.AddColumnsForChange(coll, "Tot_Customer_Dis_Amt", obj.Tot_Customer_Dis_Amt)

            clsCommon.AddColumnsForChange(coll, "Ship_To", obj.Ship_To)
            clsCommon.AddColumnsForChange(coll, "Ship_To_Desc", obj.Ship_To_Desc)

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALE_RETURN_INTER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALE_RETURN_INTER_HEAD", OMInsertOrUpdate.Update, "TSPL_SALE_RETURN_INTER_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If


            isSaved = isSaved AndAlso clsSaleReturnInterCompanyDetail.SaveData(obj.Document_No, Arr, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal Trans As SqlTransaction) As clsSaleReturnInterCompany
        Dim obj As clsSaleReturnInterCompany = Nothing


        Dim qry As String = "select * from TSPL_SALE_RETURN_INTER_HEAD where 2=2  "
        Select Case NavType
            Case NavigatorType.Current
                qry += " and Document_No = '" + strCode + "'"
            Case NavigatorType.First
                qry += " and Document_No = (select MIN(Document_No) from TSPL_SALE_RETURN_INTER_HEAD)"
            Case NavigatorType.Last
                qry += " and Document_No = (select Max(Document_No) from TSPL_SALE_RETURN_INTER_HEAD)"
            Case NavigatorType.Next
                qry += " and Document_No = (select Min(Document_No) from TSPL_SALE_RETURN_INTER_HEAD where Document_No>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Document_No = (select Max(Document_No) from TSPL_SALE_RETURN_INTER_HEAD where Document_No<'" + strCode + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, Trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsSaleReturnInterCompany
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
            obj.On_Hold = IIf(clsCommon.myCdbl(dt.Rows(0)("On_Hold")) = 1, True, False)
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
            obj.Discount_Amt = clsCommon.myCdbl(dt.Rows(0)("Discount_Amt"))
            obj.Detail_Disc_Amt = clsCommon.myCdbl(dt.Rows(0)("Detail_Disc_Amt"))
            obj.Detail_Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Detail_Total_Amt"))
            obj.Tax_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax_Amt"))
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
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.Is_Post = clsCommon.myCstr(dt.Rows(0)("Is_Post"))
            obj.Total_TPT = clsCommon.myCdbl(dt.Rows(0)("Total_TPT"))
            obj.Level1_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level1_User_Commission"))
            obj.Level2_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level2_User_Commission"))
            obj.Level3_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level3_User_Commission"))
            obj.Level4_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level4_User_Commission"))
            obj.Level5_User_Commission = clsCommon.myCdbl(dt.Rows(0)("Level5_User_Commission"))
            obj.Empty_Value = clsCommon.myCdbl(dt.Rows(0)("Empty_Value"))
            obj.Shell_Qty = clsCommon.myCdbl(dt.Rows(0)("Shell_Qty"))
            obj.Customer_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Customer_Invoice_No"))
            obj.Employee_Code = clsCommon.myCstr(dt.Rows(0)("Employee_Code"))
            obj.Total_Disc_Percent = clsCommon.myCdbl(dt.Rows(0)("Total_Disc_Percent"))
            obj.Tax_Recoverable_Amt = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt"))
            obj.Tax_Recoverable_Amt2 = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt2"))
            obj.Tax_Recoverable_Amt3 = clsCommon.myCdbl(dt.Rows(0)("Tax_Recoverable_Amt3"))
            obj.Tot_Customer_Dis_Amt = clsCommon.myCdbl(dt.Rows(0)("Tot_Customer_Dis_Amt"))
            obj.Ship_To = clsCommon.myCstr(dt.Rows(0)("Ship_To"))
            obj.Ship_To_Desc = clsCommon.myCstr(dt.Rows(0)("Ship_To_Desc"))

            qry = "select * from TSPL_SALE_RETURN_INTER_DETAIL where Document_No='" + obj.Document_No + "'"
            dt = clsDBFuncationality.GetDataTable(qry, Trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Arr = New List(Of clsSaleReturnInterCompanyDetail)
                For Each dr As DataRow In dt.Rows
                    Dim objtr As clsSaleReturnInterCompanyDetail = New clsSaleReturnInterCompanyDetail()
                    objtr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objtr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objtr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objtr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objtr.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                    objtr.Qty = clsCommon.myCdbl(dr("Qty"))

                    objtr.Actual_Return_Qty = clsCommon.myCdbl(dr("Actual_Return_Qty"))
                    objtr.Leak_Qty = clsCommon.myCdbl(dr("Leak_Qty"))
                    objtr.Burst_Qty = clsCommon.myCdbl(dr("Burst_Qty"))
                    objtr.Short_Qty = clsCommon.myCdbl(dr("Short_Qty"))


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

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsSaleReturnInterCompany = clsSaleReturnInterCompany.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Delete")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Sale Return (Inter Company)", obj.Location, obj.Document_Date, trans)

            If obj.Is_Post = 1 Then
                Throw New Exception("Already Posted Transaction.Can not delete ")
            End If
            Dim qry As String = "delete from TSPL_SALE_RETURN_INTER_DETAIL where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_SALE_RETURN_INTER_HEAD where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True

    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String = ""
        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Sale Return No not found to Post")
            End If
            ''Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")


            Dim obj As clsSaleReturnInterCompany = clsSaleReturnInterCompany.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Sale Return (Inter Company)", obj.Location, obj.Document_Date, trans)

            If (clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal) Then
                Throw New Exception("Already Post on :" + obj.Post_Date)
            End If


            Dim ArryLstGLAC As ArrayList = New ArrayList()

            qry = "select Receivable_Control_acct,Container_Deposit from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + obj.Cust_Account + "'"
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
            Dim isLocationExcisable As Boolean = IIf(obj.Document_Type = 0, True, False)
            Dim dblTotTPT As Double = obj.Total_TPT
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
            ''For Shipment Return Variabels
            Dim cogs As Double = 0
            Dim schemeCogs As Double = 0
            Dim promoCogs As Double = 0
            Dim shipmentCogs As Double = 0
            ''End of For Shipment Return Variabels



            For Each objtr As clsSaleReturnInterCompanyDetail In obj.Arr
                Dim convFac As Double = clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_code, trans)
                If objtr.Unit_COGS > 0 Then
                    dblShipmentTotCost += objtr.Unit_COGS * objtr.Qty / convFac
                    If clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal Then
                        dblSchemeTotAmt += objtr.Qty * objtr.Unit_COGS / convFac
                    ElseIf clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal Then
                        dblSamplingTotAmt += objtr.Qty * objtr.Unit_COGS / convFac
                    End If
                End If
                If (clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal) Then
                    dblTotTaxAmtFOC1 += objtr.Qty * objtr.TAX1_Amt
                    dblTotTaxAmtFOC2 += objtr.Qty * objtr.TAX2_Amt
                    dblTotTaxAmtFOC3 += objtr.Qty * objtr.TAX3_Amt
                    dblTotTaxAmtFOC4 += objtr.Qty * objtr.TAX4_Amt
                    dblTotTaxAmtFOC5 += objtr.Qty * objtr.TAX5_Amt
                    dblTotTaxAmtFOC6 += objtr.Qty * objtr.TAX6_Amt
                End If

                dblTotTaxAmt1 += objtr.Qty * objtr.TAX1_Amt
                dblTotTaxAmt2 += objtr.Qty * objtr.TAX2_Amt
                dblTotTaxAmt3 += objtr.Qty * objtr.TAX3_Amt
                dblTotTaxAmt4 += objtr.Qty * objtr.TAX4_Amt
                dblTotTaxAmt5 += objtr.Qty * objtr.TAX5_Amt
                dblTotTaxAmt6 += objtr.Qty * objtr.TAX6_Amt
                Dim dblCurrExcise As Double = 0
                Dim dblCurrCess As Double = 0
                Dim dblCurrHECess As Double = 0


                If Not isLocationExcisable Then
                    Dim objItemTax As clsItemTax = clsItemTax.GetData(objtr.Item_Code, trans)
                    If objItemTax IsNot Nothing Then
                        Dim subStr As String = objtr.Item_Code.Substring(0, 2)
                        Dim dblAbdRate As Double = IIf(clsCommon.CompairString("SM", subStr) = CompairStringResult.Equal, 0.65, 0.6)
                        dblCurrExcise = dblAbdRate * objtr.Total_MRP_Amt * objItemTax.Excise / 100
                        If (clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal) Then
                            dblExciseFOC += dblCurrExcise
                            dblCessFOC += dblCurrExcise * objItemTax.Ecess / 100
                            dblHECessFOC += dblCurrExcise * objItemTax.Hcess / 100
                            dblCurrExcise = 0 ''Becuase it is Added for current item GL Account code.
                        Else
                            dblCurrCess = dblCurrExcise * objItemTax.Ecess / 100
                            dblCurrHECess = dblCurrExcise * objItemTax.Hcess / 100
                        End If
                    End If
                End If
                dblCurrExcise = Math.Round(dblCurrExcise, 2, MidpointRounding.ToEven)
                dblCurrCess = Math.Round(dblCurrCess, 2, MidpointRounding.ToEven)
                dblCurrHECess = Math.Round(dblCurrHECess, 2, MidpointRounding.ToEven)

                dblExcise += dblCurrExcise
                dblCess += dblCurrCess
                dblHECess += dblCurrHECess


                ''''Item wise GL Account''''''
                Dim objItemWiseGLAC As clsJournalDetailTemp = New clsJournalDetailTemp()
                qry = "select Sales_Return_Account from TSPL_SALES_ACCOUNTS where Sales_Class_Code in (select Sale_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')"
                objItemWiseGLAC.Account_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(objItemWiseGLAC.Account_code) <= 0 Then
                    Throw New Exception("Please set Sale Account of Sale Account set for item : " + objtr.Item_Code)
                End If
                Dim dblSaleAmount As Double = IIf(isLocationExcisable, (objtr.Total_net_Amt - dblCurrExcise - dblCurrCess - dblCurrHECess), objtr.Total_net_Amt)
                ''Dim dblSaleAmount As Double = objtr.Total_net_Amt
                ''Dim dblSaleAmount As Double = objtr.Total_net_Amt - dblCurrExcise - dblCurrCess - dblCurrHECess
                objItemWiseGLAC.Account_code = clsERPFuncationality.ChangeGLAccountLocationSegment(objItemWiseGLAC.Account_code, obj.Location, trans)
                objItemWiseGLAC.Amount = -1 * dblSaleAmount
                ArrItemWiseGLAC.Add(objItemWiseGLAC)
                ''''End of Item wise GL Account''''''

                ''Update [Sale_Account_Amount] in TSPL_SALE_RETURN_INTER_DETAIL table -----------Pankaj(28/11/2012)
                qry = " Update TSPL_SALE_RETURN_INTER_DETAIL set Sale_Account_Amount='" + clsCommon.myCstr(dblSaleAmount) + "' where Document_No='" + objtr.Document_No + "' and Line_No='" + clsCommon.myCstr(objtr.Line_No) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                ''End of Update [Sale_Account_Amount] in TSPL_SALE_RETURN_INTER_DETAIL table----- 


                If objtr.Unit_COGS > 0 Then
                    If clsCommon.CompairString(objtr.Scheme_Item, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objtr.Promo_Scheme_Item, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objtr.Sampling_Item, "N") = CompairStringResult.Equal Then
                        cogs += Math.Round((objtr.Qty * objtr.Unit_COGS / convFac), 2)
                    ElseIf objtr.Scheme_Item = "Y" Or objtr.Sampling_Item = "Y" Then
                        schemeCogs += Math.Round(objtr.Qty * objtr.Unit_COGS / convFac, 2)
                    ElseIf objtr.Promo_Scheme_Item = "Y" Then
                        promoCogs += Math.Round(objtr.Qty * objtr.Unit_COGS, 2)
                    End If
                End If
            Next
            ''Update [Sale_Account_Amount] in TSPL_SALE_RETURN_INTER_HEAD table ------------Pankaj(28/11/2012)
            Dim qryHead As String = " Update TSPL_SALE_RETURN_INTER_HEAD set Sale_Account_Amount=(select sum(ISNULL(Sale_Account_Amount,0)) from TSPL_SALE_RETURN_INTER_DETAIL where TSPL_SALE_RETURN_INTER_DETAIL.Document_No='" + obj.Document_No + "') where TSPL_SALE_RETURN_INTER_HEAD.Document_No='" + obj.Document_No + "' "
            clsDBFuncationality.ExecuteNonQuery(qryHead, trans)
            ''End of Update [Sale_Account_Amount] in TSPL_SALE_RETURN_INTER_HEAD table------ 

            dblExcise = Math.Round(dblExcise, 2, MidpointRounding.ToEven)
            dblCess = Math.Round(dblCess, 2, MidpointRounding.ToEven)
            dblHECess = Math.Round(dblHECess, 2, MidpointRounding.ToEven)

            dblExciseFOC = Math.Round(dblExciseFOC, 2, MidpointRounding.ToEven)
            dblCessFOC = Math.Round(dblCessFOC, 2, MidpointRounding.ToEven)
            dblHECessFOC = Math.Round(dblHECessFOC, 2, MidpointRounding.ToEven)

            shipmentCogs = cogs + schemeCogs + promoCogs
            shipmentCogs = Math.Round(shipmentCogs, 2, MidpointRounding.ToEven)

            dblTotTaxAmt1 = Math.Round(dblTotTaxAmt1, 2, MidpointRounding.ToEven)
            dblTotTaxAmt2 = Math.Round(dblTotTaxAmt2, 2, MidpointRounding.ToEven)
            dblTotTaxAmt3 = Math.Round(dblTotTaxAmt3, 2, MidpointRounding.ToEven)
            dblTotTaxAmt4 = Math.Round(dblTotTaxAmt4, 2, MidpointRounding.ToEven)
            dblTotTaxAmt5 = Math.Round(dblTotTaxAmt5, 2, MidpointRounding.ToEven)
            dblTotTaxAmt6 = Math.Round(dblTotTaxAmt6, 2, MidpointRounding.ToEven)

            ''''''''''''''''''''''''''End of Calucaltion Part



            '''''''''''''''''''''''''''''Jouranal Entery Begins Now
            If obj.Total_Order_Amt > 0 Then
                Dim strDebtorCtrlAc As String = clsCommon.myCstr(dtCustACSet.Rows(0)("Receivable_Control_acct"))
                If clsCommon.myLen(strDebtorCtrlAc) <= 0 Then
                    Throw New Exception("Please set Debtor Control Account of Customer Account set")
                End If
                strDebtorCtrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strDebtorCtrlAc, obj.Location, trans)
                Dim Acc() As String = {strDebtorCtrlAc, obj.Total_Order_Amt}
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


            If obj.Detail_Total_Amt > 0 Then
                For Each objItemWiseGLAC As clsJournalDetailTemp In ArrItemWiseGLAC
                    If objItemWiseGLAC.Amount <> 0 Then
                        Dim Acc1() As String = {objItemWiseGLAC.Account_code, objItemWiseGLAC.Amount}
                        ArryLstGLAC.Add(Acc1)
                    End If
                Next
            End If


            Dim objTM As clsTaxMaster
            ''If Not isLocationExcisable Then
            ''    ''for normal Sale
            ''    If dblExcise > 0 Then
            ''        objTM = clsTaxMaster.GetTaxDetailsForSale("BED", trans)
            ''        If objTM IsNot Nothing Then
            ''            If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
            ''                Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX1)
            ''            End If
            ''            objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
            ''            Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblExcise)}
            ''            ArryLstGLAC.Add(Acc1)

            ''            If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
            ''                Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX1)
            ''            End If
            ''            objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
            ''            Dim Acc() As String = {objTM.Tax_Net_Payable, (dblExcise + dblExciseFOC)}
            ''            ArryLstGLAC.Add(Acc)
            ''        End If
            ''    End If


            ''    If dblCess > 0 Then
            ''        objTM = clsTaxMaster.GetTaxDetailsForSale("ECESS", trans)
            ''        If objTM IsNot Nothing Then
            ''            If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
            ''                Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX1)
            ''            End If
            ''            objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
            ''            Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblCess)}
            ''            ArryLstGLAC.Add(Acc1)

            ''            If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
            ''                Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX1)
            ''            End If
            ''            objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
            ''            Dim Acc() As String = {objTM.Tax_Net_Payable, (dblCess + dblCessFOC)}
            ''            ArryLstGLAC.Add(Acc)
            ''        End If
            ''    End If



            ''    If dblHECess > 0 Then
            ''        objTM = clsTaxMaster.GetTaxDetailsForSale("HCESS", trans)
            ''        If objTM IsNot Nothing Then
            ''            If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
            ''                Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX1)
            ''            End If
            ''            objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
            ''            Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblHECess)}
            ''            ArryLstGLAC.Add(Acc1)

            ''            If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
            ''                Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX1)
            ''            End If
            ''            objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
            ''            Dim Acc() As String = {objTM.Tax_Net_Payable, (dblHECess + dblHECessFOC)}
            ''            ArryLstGLAC.Add(Acc)
            ''        End If
            ''    End If


            ''End If

            If dblTotTaxAmt1 > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX1, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then

                        If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                            Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX1)
                        End If
                        objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                        Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt1 - dblTotTaxAmtFOC1)}
                        ArryLstGLAC.Add(Acc1)


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

            If dblTotTaxAmt2 > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX2, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                        If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                            Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX2)
                        End If
                        objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                        Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt2 - dblTotTaxAmtFOC2)}
                        ArryLstGLAC.Add(Acc1)


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
            If dblTotTaxAmt3 > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX3, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Autherity " + obj.TAX3)
                    End If
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then

                        If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                            Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX3)
                        End If
                        objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                        Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt3 - dblTotTaxAmtFOC3)}
                        ArryLstGLAC.Add(Acc1)


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

            If dblTotTaxAmt4 > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX4, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then

                        If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                            Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX4)
                        End If
                        objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                        Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt4 - dblTotTaxAmtFOC4)}
                        ArryLstGLAC.Add(Acc1)

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
            If dblTotTaxAmt5 > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX5, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then

                        If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                            Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX5)
                        End If
                        objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                        Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt5 - dblTotTaxAmtFOC5)}
                        ArryLstGLAC.Add(Acc1)

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
            If dblTotTaxAmt6 > 0 Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX6, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then

                        If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                            Throw New Exception("Please set Tax Recoverable Account of Tax Autherity " + obj.TAX6)
                        End If
                        objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                        Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt6 - dblTotTaxAmtFOC6)}
                        ArryLstGLAC.Add(Acc1)


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




            If dblTotTPT > 0 Then
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
                Dim strShipmentClearingAC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                If clsCommon.myLen(strShipmentClearingAC) <= 0 Then
                    Throw New Exception("Please set Shipment Clearing account of Purchase set " + strFirstItemPurchaseAccountSet)
                End If
                strShipmentClearingAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentClearingAC, obj.Location, trans)
                Dim Acc() As String = {strShipmentClearingAC, -1 * dblShipmentTotCost}
                ArryLstGLAC.Add(Acc)


                If dblShipmentTotCost - dblSchemeTotAmt - dblSamplingTotAmt > 0 Then
                    Dim strCogsAccount As String = clsCommon.myCstr(dtSaleACSet.Rows(0)("Cost_Of_Goods_Sold"))
                    If clsCommon.myLen(strCogsAccount) <= 0 Then
                        Throw New Exception("Please set cost of Goods Sole account of Sale Account set")
                    End If
                    strCogsAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strCogsAccount, obj.Location, trans)
                    ''Dim Acc1() As String = {strCogsAccount, IIf(isLocationExcisable, (dblShipmentTotCost - dblSchemeTotAmt - dblSamplingTotAmt), dblShipmentTotCost - dblSchemeTotAmt - dblSamplingTotAmt - dblExcise - dblCess - dblHECess)}
                    Dim Acc1() As String = {strCogsAccount, (dblShipmentTotCost - dblSchemeTotAmt - dblSamplingTotAmt)}
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


            ''Adding Shipment Accounts
            ''qry = "select Shipment_Type from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + obj.Invoice_No + "'"
            ''If clsCommon.CompairString(clsDBFuncationality.getSingleValue(qry, trans), "Sale") = CompairStringResult.Equal Then
            If True Then
                qry = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
             " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
             " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr(0).Item_Code + "'"
                Dim strShipmentInventoryCtrlAccount As String = clsDBFuncationality.getSingleValue(qry, trans)
                If clsCommon.myLen(strShipmentInventoryCtrlAccount) <= 0 Then
                    Throw New Exception("Please set Inventroy Control Account of Purchase Account set")
                End If

                strShipmentInventoryCtrlAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strShipmentInventoryCtrlAccount, obj.Location, trans)
                Dim Acc1() As String = {strShipmentInventoryCtrlAccount, -1 * shipmentCogs}
                ArryLstGLAC.Add(Acc1)
            End If

            ''Else
            ''    qry = "SELECT PA.Reserve_Stock FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
            ''     " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
            ''     " TSPL_GL_ACCOUNTS AS GLA ON PA.Reserve_Stock = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr(0).Item_Code + "'"
            ''    Dim strReverseStockAccount As String = clsDBFuncationality.getSingleValue(qry, trans)
            ''    If clsCommon.myLen(strReverseStockAccount) <= 0 Then
            ''        Throw New Exception("Please set Reverse Stock Account of Purchase Account set")
            ''    End If

            ''    strReverseStockAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strReverseStockAccount, obj.Location, trans)
            ''    Dim Acc1() As String = {strReverseStockAccount, -1 * shipmentCogs}
            ''    ArryLstGLAC.Add(Acc1)
            ''End If


            qry = "SELECT PA.Shipment_Clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                  " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                  " TSPL_GL_ACCOUNTS AS GLA ON PA.Shipment_Clearing = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr(0).Item_Code + "'"
            Dim strShpClrAcc As String = clsDBFuncationality.getSingleValue(qry, trans)
            If clsCommon.myLen(strShpClrAcc) <= 0 Then
                Throw New Exception("Please set Shipment Clearing Account of Purchase Account set")
            End If

            strShpClrAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strShpClrAcc, obj.Location, trans)
            Dim Acct() As String = {strShpClrAcc, shipmentCogs}
            ArryLstGLAC.Add(Acct)
            ''End of Adding Shipment Accounts


            ''Reverse the All GL Entry Becuse it is sales Return (changing the Dr to Cr and Cr To Dr)
            Dim ArryLstNew As ArrayList = New ArrayList()
            For Each Str() As String In ArryLstGLAC
                Dim strNew() As String = {Str(0), -1 * Str(1)}
                ArryLstNew.Add(strNew)
            Next

            ''End of Reverse the All GL Entry Becuse it is sales Return (changing the Dr to Cr and Cr To Dr)


            clsJournalMaster.FunGrnlEntryWithTrans(obj.Location, False, trans, obj.Document_Date, "Against Sale Return No" + strDocNo, "SD-SR", "Sale Return", strDocNo, "", "C", obj.Cust_Code, obj.Cust_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstNew)
            ''end of GL Entry



            ''Inventory Movement  and itemLocation 
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
            For Each objTR As clsSaleReturnInterCompanyDetail In obj.Arr
                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "I"
                objInventoryMovemnt.Location_Code = obj.Location

                objInventoryMovemnt.Cust_Code = obj.Cust_Code
                objInventoryMovemnt.Cust_Name = obj.Cust_Name


                objInventoryMovemnt.Item_Code = objTR.Item_Code
                objInventoryMovemnt.Item_Desc = objTR.Item_Desc
                objInventoryMovemnt.Qty = objTR.Qty
                objInventoryMovemnt.UOM = objTR.Unit_code
                objInventoryMovemnt.Basic_Cost = objTR.Basic_Rate
                objInventoryMovemnt.Rec_Cost = objTR.Total_Basic_Amt
                objInventoryMovemnt.Add_Cost = objTR.Total_Tax_Amt
                objInventoryMovemnt.Net_Cost = objTR.Total_Item_Amt
                objInventoryMovemnt.ItemType = "FM"
                objInventoryMovemnt.InOut = "I"
                objInventoryMovemnt.MRP = objTR.MRP_Amt
                ArrInventoryMovement.Add(objInventoryMovemnt)

                Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(objTR.Item_Code, objTR.Unit_code, trans)
                Dim objLocationDetails As New clsItemLocationDetails()
                objLocationDetails.Item_Code = objTR.Item_Code
                objLocationDetails.Item_Desc = objTR.Item_Desc
                objLocationDetails.Location_Code = obj.Location
                objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                objLocationDetails.Item_Qty = clsCommon.myCdbl(objTR.Qty) / dblConvFac
                objLocationDetails.Amount = clsCommon.myCdbl(objTR.Total_Item_Amt)

                objLocationDetails.MRP = objTR.MRP_Amt * dblConvFac 'clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select MRP_Amt from TSPL_SALE_RETURN_DETAIL where Item_Code='" + clsCommon.myCstr(objTR.Item_Code) + "' and Sale_Return_No='" + strDocNo + "' ", trans)) * dblConvFac
                dt = clsDBFuncationality.GetDataTable("select top 1 MFG_Date,Expiry_Date,Batch_No from TSPL_ADJUSTMENT_DETAIL where Item_Code='" + objTR.Item_Code + "'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objLocationDetails.Batch_No = clsCommon.myCstr(dt.Rows(0)("Batch_No"))
                    objLocationDetails.MFG_Date = clsCommon.myCDate(dt.Rows(0)("MFG_Date"))
                    objLocationDetails.Expiry_Date = clsCommon.myCDate(dt.Rows(0)("Expiry_Date"))
                End If
                objLocationDetails.ItemType = "FM"
                ArrLocationDetails.Add(objLocationDetails)
            Next
            isSaved = isSaved AndAlso clsInventoryMovement.SaveData("Sale Return", strDocNo, obj.Document_Date, clsCommon.myCDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.myCDate(obj.Document_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)

            '' ''End of  Inventory Movement  

            qry = "Update TSPL_SALE_RETURN_INTER_HEAD set Is_Post=1, Post_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "'"
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
        Return True
    End Function
End Class

Public Class clsSaleReturnInterCompanyDetail
#Region "Varibales"
    Public Line_No As Integer = 0
    Public Document_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Price_code As String = Nothing
    Public Price_Date As Date = Nothing
    Public Qty As Double = Nothing

    Public Actual_Return_Qty As Double = Nothing
    Public Leak_Qty As Double = Nothing
    Public Burst_Qty As Double = Nothing
    Public Short_Qty As Double = Nothing

    Public Unit_code As String = Nothing

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
    Public Price_Date_Actual As Date? = Nothing
    Public Price_To_Show As Double = 0
    Public RAW_Qty As Double = Nothing
    Public Converted_Qty As Double = Nothing
    Public OZ_Qty As Double = Nothing
    Public Abatement_rate As Double = Nothing

    Public price_amount1 As Double = Nothing
    Public price_amount2 As Double = Nothing
    Public price_amount3 As Double = Nothing
    Public price_amount4 As Double = Nothing
    Public price_amount5 As Double = Nothing
    Public price_amount6 As Double = Nothing
    Public price_amount7 As Double = Nothing
    Public price_amount8 As Double = Nothing
    Public price_amount9 As Double = Nothing
    Public price_amount10 As Double = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsSaleReturnInterCompanyDetail), ByVal trans As SqlTransaction) As Boolean
        Dim LineNo As Integer = 0
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSaleReturnInterCompanyDetail In Arr
                LineNo = LineNo + 1
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Line_No", LineNo)
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Price_code", obj.Price_code)
                clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)

                clsCommon.AddColumnsForChange(coll, "Actual_Return_Qty", obj.Actual_Return_Qty)
                clsCommon.AddColumnsForChange(coll, "Leak_Qty", obj.Leak_Qty)
                clsCommon.AddColumnsForChange(coll, "Burst_Qty", obj.Burst_Qty)
                clsCommon.AddColumnsForChange(coll, "Short_Qty", obj.Short_Qty)

                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
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
                clsCommon.AddColumnsForChange(coll, "TAX5_Assessable_Amt", obj.TAX4_Assessable_Amt)
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
                clsCommon.AddColumnsForChange(coll, "Price_Date_Actual", obj.Price_Date_Actual)
                clsCommon.AddColumnsForChange(coll, "Price_To_Show", obj.Price_To_Show)
                clsCommon.AddColumnsForChange(coll, "RAW_Qty", obj.RAW_Qty)
                clsCommon.AddColumnsForChange(coll, "Converted_Qty", obj.Converted_Qty)
                clsCommon.AddColumnsForChange(coll, "OZ_Qty", obj.OZ_Qty)
                clsCommon.AddColumnsForChange(coll, "Abatement_rate", obj.Abatement_rate)

                '-----------for price code update---------------------------------------------

                Dim qry As String = "select TSPL_ITEM_PRICE_MASTER.Price_Amount1 ,TSPL_ITEM_PRICE_MASTER.Price_Amount2 ,TSPL_ITEM_PRICE_MASTER.Price_Amount3 ,TSPL_ITEM_PRICE_MASTER.Price_Amount4 ,TSPL_ITEM_PRICE_MASTER.Price_Amount5 ,TSPL_ITEM_PRICE_MASTER.Price_Amount6 ,TSPL_ITEM_PRICE_MASTER.Price_Amount7 ,TSPL_ITEM_PRICE_MASTER.Price_Amount8 ,TSPL_ITEM_PRICE_MASTER.Price_Amount9 ,TSPL_ITEM_PRICE_MASTER.Price_Amount10 from TSPL_ITEM_PRICE_MASTER"
                qry += " where  TSPL_ITEM_PRICE_MASTER.Price_Code='" + obj.Price_code + "' and  TSPL_ITEM_PRICE_MASTER.Item_Code='" + obj.Item_Code + "' and TSPL_ITEM_PRICE_MASTER.Item_Basic_Net='" + clsCommon.myCstr(clsCommon.myCdbl(obj.MRP_Amt * clsItemMaster.GetConvertionFactor(obj.Item_Code, obj.Unit_code, trans))) + "' and TSPL_ITEM_PRICE_MASTER.UOM='FC' "
                'If obj.Price_Date_Actual IsNot Nothing Then
                '    qry += " and  TSPL_ITEM_PRICE_MASTER.Start_Date= '" + clsCommon.GetPrintDate(obj.Price_Date_Actual, "dd/MMM/yyyy") + "'"
                'Else
                qry += " and  TSPL_ITEM_PRICE_MASTER.Start_Date= '" + clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy") + "'"
                'End If
                Dim dtPriceComponent As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                '------------------------------------------------------------------------------

                If dtPriceComponent IsNot Nothing AndAlso dtPriceComponent.Rows.Count > 0 Then

                    clsCommon.AddColumnsForChange(coll, "price_amount1", dtPriceComponent.Rows(0)("price_amount1"))
                    clsCommon.AddColumnsForChange(coll, "price_amount2", dtPriceComponent.Rows(0)("price_amount2"))
                    clsCommon.AddColumnsForChange(coll, "price_amount3", dtPriceComponent.Rows(0)("price_amount3"))
                    clsCommon.AddColumnsForChange(coll, "price_amount4", dtPriceComponent.Rows(0)("price_amount4"))
                    clsCommon.AddColumnsForChange(coll, "price_amount5", dtPriceComponent.Rows(0)("price_amount5"))
                    clsCommon.AddColumnsForChange(coll, "price_amount6", dtPriceComponent.Rows(0)("price_amount6"))
                    clsCommon.AddColumnsForChange(coll, "price_amount7", dtPriceComponent.Rows(0)("price_amount7"))
                    clsCommon.AddColumnsForChange(coll, "price_amount8", dtPriceComponent.Rows(0)("price_amount8"))
                    clsCommon.AddColumnsForChange(coll, "price_amount9", dtPriceComponent.Rows(0)("price_amount9"))
                    clsCommon.AddColumnsForChange(coll, "price_amount10", dtPriceComponent.Rows(0)("price_amount10"))

                Else
                    clsCommon.AddColumnsForChange(coll, "price_amount1", 0)
                    clsCommon.AddColumnsForChange(coll, "price_amount2", 0)
                    clsCommon.AddColumnsForChange(coll, "price_amount3", 0)
                    clsCommon.AddColumnsForChange(coll, "price_amount4", 0)
                    clsCommon.AddColumnsForChange(coll, "price_amount5", 0)
                    clsCommon.AddColumnsForChange(coll, "price_amount6", 0)
                    clsCommon.AddColumnsForChange(coll, "price_amount7", 0)
                    clsCommon.AddColumnsForChange(coll, "price_amount8", 0)
                    clsCommon.AddColumnsForChange(coll, "price_amount9", 0)
                    clsCommon.AddColumnsForChange(coll, "price_amount10", 0)
                End If


                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALE_RETURN_INTER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
