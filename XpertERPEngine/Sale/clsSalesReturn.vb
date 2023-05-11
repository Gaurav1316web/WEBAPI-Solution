Imports common
Imports System.Data.SqlClient
Public Class clsSalesReturnHead
#Region "Varibales"
    Public Sale_Return_No As String
    Public shippmentType As String

    Public Sale_Return_Date As DateTime
    Public Sale_Return_Posting_Date As Date
    Public Actual_Return_Date As Date
    Public Invoice_No As String
    Public Invoice_Date As Date
    Public Cust_Code As String
    Public Cust_Name As String
    Public Cust_PONo As String
    Public Ref_No As String
    Public Description As String
    Public Price_Code As String
    Public Tax_Group As String
    Public Location As String
    Public Cust_Account As String
    Public TAX1 As String
    Public TAX1_Rate As Double
    Public TAX1_Assessable_Amt As Double
    Public TAX1_Amt As Double
    Public TAX2 As String
    Public TAX2_Rate As Double
    Public TAX2_Assessable_Amt As Double
    Public TAX2_Amt As Double
    Public TAX3 As String
    Public TAX3_Rate As Double
    Public TAX3_Assessable_Amt As Double
    Public TAX3_Amt As Double
    Public TAX4 As String
    Public TAX4_Rate As Double
    Public TAX4_Assessable_Amt As Double
    Public TAX4_Amt As Double
    Public TAX5 As String
    Public TAX5_Rate As Double
    Public TAX5_Assessable_Amt As Double
    Public TAX5_Amt As Double
    Public TAX6 As String
    Public TAX6_Rate As Double
    Public TAX6_Assessable_Amt As Double
    Public TAX6_Amt As Double
    Public TAX7 As String
    Public TAX7_Rate As Double
    Public TAX7_Assessable_Amt As Double
    Public TAX7_Amt As Double
    Public TAX8 As String
    Public TAX8_Rate As Double
    Public TAX8_Assessable_Amt As Double
    Public TAX8_Amt As Double
    Public TAX9 As String
    Public TAX9_Rate As Double
    Public TAX9_Assessable_Amt As Double
    Public TAX9_Amt As Double
    Public TAX10 As String
    Public TAX10_Rate As Double
    Public TAX10_Assessable_Amt As Double
    Public TAX10_Amt As Double
    Public Total_Assessable_Amount As Double
    Public Inv_Disc_Percent As Double
    Public Inv_Discount_Amt As Double
    Public Inv_Detail_Disc_Amt As Double
    Public Inv_Detail_Total_Amt As Double
    Public Inv_Tax_Amt As Double
    Public Freight_Amt As Double
    Public Other_Charges As Double
    Public Add_Charges As Double
    Public Total_Invoice_Amt As Double
    Public Balance_Amt As Double
    Public Salesman_Code As String
    Public Mode_Of_Transport As String
    Public Vehicle_Code As String
    Public Vehicle_No As String
    Public KM_Reading As Integer
    Public Route_No As String
    Public Route_Desc As String
    Public Scheme_Sample_Code As String
    Public Terms_Code As String
    Public Comments As String
    Public Level1_User_code As String
    Public Level2_User_code As String
    Public Level3_User_code As String
    Public Level4_User_code As String
    Public Level5_User_code As String
    Public Comp_Code As String
    Public Is_Post As String
    Public TPT As Double
    Public Empty_Value As Double
    Public Created_By As String
    Public Created_Date As Date
    Public Modify_By As String
    Public Modify_Date As Date
    Public Shell_Qty As Double
  

    Public Arr As List(Of clsSalesReturnDetail) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsSalesReturnHead, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Sale Return", obj.Location, obj.Sale_Return_Date, trans)

            Dim qry As String = "delete from TSPL_SALE_RETURN_DETAIL where Sale_Return_No='" + obj.Sale_Return_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Sale_Return_No = clsERPFuncationality.GetNextCode(trans, obj.Sale_Return_Date, clsDocType.SalesReturn, "", obj.Location)
            End If
            If (clsCommon.myLen(obj.Sale_Return_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Actual_Return_Date", clsCommon.GetPrintDate(obj.Sale_Return_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Sale_Return_Date", clsCommon.GetPrintDate(obj.Sale_Return_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.Invoice_No)
            clsCommon.AddColumnsForChange(coll, "Invoice_Date", clsCommon.GetPrintDate(obj.Invoice_Date, "dd/MMM/yyyy"))
            ''clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Shell_Qty", obj.Shell_Qty)
            clsCommon.AddColumnsForChange(coll, "Tax_Group", obj.Tax_Group)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Cust_Name", obj.Cust_Name)
            clsCommon.AddColumnsForChange(coll, "Cust_PONo", obj.Cust_PONo)
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
            clsCommon.AddColumnsForChange(coll, "Cust_Account", obj.Cust_Account)
            clsCommon.AddColumnsForChange(coll, "Shipment_type", obj.shippmentType)
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
            clsCommon.AddColumnsForChange(coll, "Inv_Disc_Percent", obj.Inv_Disc_Percent)
            clsCommon.AddColumnsForChange(coll, "Inv_Discount_Amt", obj.Inv_Discount_Amt)
            clsCommon.AddColumnsForChange(coll, "Inv_Detail_Disc_Amt", obj.Inv_Detail_Disc_Amt)
            clsCommon.AddColumnsForChange(coll, "Inv_Detail_Total_Amt", obj.Inv_Detail_Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Inv_Tax_Amt", obj.Inv_Tax_Amt)
            clsCommon.AddColumnsForChange(coll, "Freight_Amt", obj.Freight_Amt)
            clsCommon.AddColumnsForChange(coll, "Other_Charges", obj.Other_Charges)
            clsCommon.AddColumnsForChange(coll, "Add_Charges", obj.Add_Charges)
            clsCommon.AddColumnsForChange(coll, "Total_Invoice_Amt", obj.Total_Invoice_Amt)
            clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Balance_Amt)
            clsCommon.AddColumnsForChange(coll, "Salesman_Code", obj.Salesman_Code)
            clsCommon.AddColumnsForChange(coll, "Mode_Of_Transport", obj.Mode_Of_Transport)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)
            clsCommon.AddColumnsForChange(coll, "KM_Reading", obj.KM_Reading)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Route_Desc", obj.Route_Desc)
            clsCommon.AddColumnsForChange(coll, "Terms_Code", obj.Terms_Code)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "Level1_User_code", obj.Level1_User_code)
            clsCommon.AddColumnsForChange(coll, "Level2_User_code", obj.Level2_User_code)
            clsCommon.AddColumnsForChange(coll, "Level3_User_code", obj.Level3_User_code)
            clsCommon.AddColumnsForChange(coll, "Level4_User_code", obj.Level4_User_code)
            clsCommon.AddColumnsForChange(coll, "Level5_User_code", obj.Level5_User_code)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "TPT", obj.TPT)
            clsCommon.AddColumnsForChange(coll, "Total_Assessable_Amount", obj.Total_Assessable_Amount)
            clsCommon.AddColumnsForChange(coll, "Scheme_Sample_Code", obj.Scheme_Sample_Code)
            clsCommon.AddColumnsForChange(coll, "Empty_Value", obj.Empty_Value)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Sale_Return_No", obj.Sale_Return_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALE_RETURN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALE_RETURN_HEAD", OMInsertOrUpdate.Update, "TSPL_SALE_RETURN_HEAD.Sale_Return_No='" + obj.Sale_Return_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsSalesReturnDetail.SaveData(obj.Sale_Return_No, obj.Arr, trans)


            '----------for updating status for fully of partial return--------------

            Dim strsaleinvoice As String = "select sum (Qty) as qty from (select  (case when TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  ) as Qty ,Unit_code from TSPL_SALE_INVOICE_HEAD " & _
                                           " left outer join TSPL_SALE_INVOICE_DETAIL on TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
                                           " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_SALE_INVOICE_DETAIL.Item_Code    and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code " & _
                                           " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + obj.Invoice_No + "') final "
            Dim salInqty As Decimal = clsDBFuncationality.getSingleValue(strsaleinvoice, trans)


            Dim strReturn As String = "select sum (Qty) as qty from (select  (case when TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(TSPL_SALE_RETURN_DETAIL.Return_qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(TSPL_SALE_RETURN_DETAIL.Return_qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  ) as Qty ,Unit_code from TSPL_SALE_RETURN_HEAD " & _
                                      " left outer join TSPL_SALE_RETURN_DETAIL on TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No " & _
                                       " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_SALE_RETURN_DETAIL.Item_Code    and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_RETURN_DETAIL.Unit_code " & _
                                       " where TSPL_SALE_RETURN_HEAD.Invoice_No='" + obj.Invoice_No + "') final "

            Dim salRETqty As Decimal = clsDBFuncationality.getSingleValue(strReturn, trans)
            'Dim isfullyReturn As Integer
            'If salInqty = salRETqty Then
            '    isfullyReturn = 1
            'Else
            '    isfullyReturn = 0
            'End If


            ' Dim isfullyReturn As Integer
            Dim strupdate As String
            If salInqty = salRETqty Then
                strupdate = "update TSPL_SALE_RETURN_HEAD set Is_FullyRevrse =1 where Sale_Return_No='" + obj.Sale_Return_No + "'"
            Else
                strupdate = "update TSPL_SALE_RETURN_HEAD set Is_FullyRevrse =0 where Sale_Return_No='" + obj.Sale_Return_No + "'"
            End If


            ' Dim strupdate As String = "update TSPL_SALE_RETURN_HEAD set Is_FullyRevrse ='" + isfullyReturn + "' where Sale_Return_No='" + obj.Sale_Return_No + "'"
            clsDBFuncationality.ExecuteNonQuery(strupdate, trans)

            '--------------------------------------------------------------------------------

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsSalesReturnHead
        Return GetData(strRetCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSalesReturnHead
        Dim obj As clsSalesReturnHead = Nothing

        Dim qry As String = "select TSPL_SALE_RETURN_HEAD.Sale_Return_No,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,TSPL_SALE_RETURN_HEAD.Sale_Return_Posting_Date,TSPL_SALE_RETURN_HEAD.Actual_Return_Date,TSPL_SALE_RETURN_HEAD.Invoice_No,TSPL_SALE_RETURN_HEAD.Invoice_Date,TSPL_SALE_RETURN_HEAD.Cust_Code,TSPL_SALE_RETURN_HEAD.Cust_Name,TSPL_SALE_RETURN_HEAD.Cust_PONo,TSPL_SALE_RETURN_HEAD.Ref_No,TSPL_SALE_RETURN_HEAD.Description,TSPL_SALE_RETURN_HEAD.Price_Code,TSPL_SALE_RETURN_HEAD.Tax_Group,TSPL_SALE_RETURN_HEAD.Location,TSPL_SALE_RETURN_HEAD.Cust_Account,TSPL_SALE_RETURN_HEAD.TAX1,TSPL_SALE_RETURN_HEAD.TAX1_Rate,TSPL_SALE_RETURN_HEAD.TAX1_Assessable_Amt,TSPL_SALE_RETURN_HEAD.TAX1_Amt,TSPL_SALE_RETURN_HEAD.TAX2,TSPL_SALE_RETURN_HEAD.TAX2_Rate,TSPL_SALE_RETURN_HEAD.TAX2_Assessable_Amt,TSPL_SALE_RETURN_HEAD.TAX2_Amt,TSPL_SALE_RETURN_HEAD.TAX3,TSPL_SALE_RETURN_HEAD.TAX3_Rate,TSPL_SALE_RETURN_HEAD.TAX3_Assessable_Amt,TSPL_SALE_RETURN_HEAD.TAX3_Amt,TSPL_SALE_RETURN_HEAD.Shell_Qty," & _
        " TAX4,TSPL_SALE_RETURN_HEAD.TAX4_Rate,TSPL_SALE_RETURN_HEAD.TAX4_Assessable_Amt,TSPL_SALE_RETURN_HEAD.TAX4_Amt,TSPL_SALE_RETURN_HEAD.TAX5,TSPL_SALE_RETURN_HEAD.TAX5_Rate,TSPL_SALE_RETURN_HEAD.TAX5_Assessable_Amt,TSPL_SALE_RETURN_HEAD.TAX5_Amt,TSPL_SALE_RETURN_HEAD.TAX6,TSPL_SALE_RETURN_HEAD.TAX6_Rate,TSPL_SALE_RETURN_HEAD.TAX6_Assessable_Amt,TSPL_SALE_RETURN_HEAD.TAX6_Amt,TSPL_SALE_RETURN_HEAD.TAX7,TSPL_SALE_RETURN_HEAD.TAX7_Rate,TSPL_SALE_RETURN_HEAD.TAX7_Assessable_Amt,TSPL_SALE_RETURN_HEAD.TAX7_Amt,TSPL_SALE_RETURN_HEAD.TAX8,TSPL_SALE_RETURN_HEAD.TAX8_Rate,TSPL_SALE_RETURN_HEAD.TAX8_Assessable_Amt,TSPL_SALE_RETURN_HEAD.TAX8_Amt,TSPL_SALE_RETURN_HEAD.TAX9,TSPL_SALE_RETURN_HEAD.TAX9_Rate,TSPL_SALE_RETURN_HEAD.TAX9_Assessable_Amt,TSPL_SALE_RETURN_HEAD.TAX9_Amt,TSPL_SALE_RETURN_HEAD.TAX10,TSPL_SALE_RETURN_HEAD.TAX10_Rate,TSPL_SALE_RETURN_HEAD.TAX10_Assessable_Amt,TSPL_SALE_RETURN_HEAD.TAX10_Amt,TSPL_SALE_RETURN_HEAD.Total_Assessable_Amount,TSPL_SALE_RETURN_HEAD.Inv_Disc_Percent,TSPL_SALE_RETURN_HEAD.Inv_Discount_Amt,TSPL_SALE_RETURN_HEAD.Inv_Detail_Disc_Amt," & _
        " Inv_Detail_Total_Amt, TSPL_SALE_RETURN_HEAD.Inv_Tax_Amt, TSPL_SALE_RETURN_HEAD.Freight_Amt, TSPL_SALE_RETURN_HEAD.Other_Charges, TSPL_SALE_RETURN_HEAD.Add_Charges, TSPL_SALE_RETURN_HEAD.Total_Invoice_Amt, TSPL_SALE_RETURN_HEAD.Balance_Amt, TSPL_SALE_RETURN_HEAD.Salesman_Code, TSPL_SALE_RETURN_HEAD.Mode_Of_Transport, TSPL_SALE_RETURN_HEAD.Vehicle_Code, TSPL_SALE_RETURN_HEAD.Vehicle_No, TSPL_SALE_RETURN_HEAD.KM_Reading, TSPL_SALE_RETURN_HEAD.Route_No, TSPL_SALE_RETURN_HEAD.Route_Desc, TSPL_SALE_RETURN_HEAD.Scheme_Sample_Code, TSPL_SALE_RETURN_HEAD.Terms_Code, TSPL_SALE_RETURN_HEAD.Comments, TSPL_SALE_RETURN_HEAD.Level1_User_code, TSPL_SALE_RETURN_HEAD.Level2_User_code, TSPL_SALE_RETURN_HEAD.Level3_User_code, TSPL_SALE_RETURN_HEAD.Level4_User_code, TSPL_SALE_RETURN_HEAD.Level5_User_code, TSPL_SALE_RETURN_HEAD.Comp_Code, TSPL_SALE_RETURN_HEAD.Is_Post, TSPL_SALE_RETURN_HEAD.TPT, TSPL_SALE_RETURN_HEAD.Empty_Value" & _
        " from TSPL_SALE_RETURN_HEAD where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SALE_RETURN_HEAD.Sale_Return_No = (select MIN(Sale_Return_No) from TSPL_SALE_RETURN_HEAD)"
            Case NavigatorType.Last
                qry += " and TSPL_SALE_RETURN_HEAD.Sale_Return_No = (select Max(Sale_Return_No) from TSPL_SALE_RETURN_HEAD)"
            Case NavigatorType.Next
                qry += " and TSPL_SALE_RETURN_HEAD.Sale_Return_No = (select Min(Sale_Return_No) from TSPL_SALE_RETURN_HEAD where Sale_Return_No>'" + strRetCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_SALE_RETURN_HEAD.Sale_Return_No = (select Max(Sale_Return_No) from TSPL_SALE_RETURN_HEAD where Sale_Return_No<'" + strRetCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_SALE_RETURN_HEAD.Sale_Return_No = '" + strRetCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsSalesReturnHead()
            obj.Sale_Return_No = clsCommon.myCstr(dt.Rows(0)("Sale_Return_No"))
            obj.Sale_Return_Date = clsCommon.myCDate(dt.Rows(0)("Sale_Return_Date"))
            obj.Actual_Return_Date = clsCommon.myCDate(dt.Rows(0)("Actual_Return_Date"))
            If dt.Rows(0)("Sale_Return_Posting_Date") IsNot DBNull.Value Then
                obj.Sale_Return_Posting_Date = clsCommon.myCDate(dt.Rows(0)("Sale_Return_Posting_Date"))
            End If
            obj.Invoice_No = clsCommon.myCstr(dt.Rows(0)("Invoice_No"))
            obj.Invoice_Date = clsCommon.myCstr(dt.Rows(0)("Invoice_Date"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Cust_Name = clsCommon.myCstr(dt.Rows(0)("Cust_Name"))
            obj.Cust_PONo = clsCommon.myCstr(dt.Rows(0)("Cust_PONo"))
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
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
            obj.Terms_Code = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.Level1_User_code = clsCommon.myCstr(dt.Rows(0)("Level1_User_code"))
            obj.Level2_User_code = clsCommon.myCstr(dt.Rows(0)("Level2_User_code"))
            obj.Level3_User_code = clsCommon.myCstr(dt.Rows(0)("Level3_User_code"))
            obj.Level4_User_code = clsCommon.myCstr(dt.Rows(0)("Level4_User_code"))
            obj.Level5_User_code = clsCommon.myCstr(dt.Rows(0)("Level5_User_code"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Is_Post = clsCommon.myCstr(dt.Rows(0)("Is_Post"))
            obj.TPT = clsCommon.myCdbl(dt.Rows(0)("TPT"))
            obj.Empty_Value = clsCommon.myCdbl(dt.Rows(0)("Empty_Value"))
            obj.Scheme_Sample_Code = clsCommon.myCstr(dt.Rows(0)("Scheme_Sample_Code"))
            obj.Shell_Qty = clsCommon.myCdbl(dt.Rows(0)("Shell_Qty"))
            obj.Arr = clsSalesReturnDetail.GetData(obj.Sale_Return_No, trans)
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Sale Return No not found to Delete")
        End If
        Dim obj As clsSalesReturnHead = clsSalesReturnHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Sale_Return_No) > 0) Then
            Try
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Sale Return", obj.Location, obj.Sale_Return_Date, trans)

                If clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal Then
                    Throw New Exception("Already Posted on :" + obj.Sale_Return_Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_SALE_RETURN_DETAIL where Sale_Return_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SALE_RETURN_HEAD where Sale_Return_No='" + strCode + "'"
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

    Public Shared Function CreateJournalEntry(ByVal strVourcherNoForRecreateOnly As String, ByVal obj As clsSalesReturnHead, ByVal trans As SqlTransaction) As Boolean
        Dim ArryLstGLAC As ArrayList = New ArrayList()
        Dim qry As String = ""
        Dim strItem As String
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
        ''For Shipment Return Variabels
        Dim cogs As Double = 0
        Dim schemeCogs As Double = 0
        Dim promoCogs As Double = 0
        Dim shipmentCogs As Double = 0
        ''End of For Shipment Return Variabels



        For Each objtr As clsSalesReturnDetail In obj.Arr
            strItem = objtr.Item_Code
            Dim convFac As Double = clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_code, trans)
            Dim dblqty As Double = objtr.Return_Qty
            If objtr.Unit_Cogs > 0 Then
                dblShipmentTotCost += objtr.Unit_Cogs * (dblqty) / convFac
                If clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal Then
                    dblSchemeTotAmt += dblqty * objtr.Unit_Cogs / convFac
                ElseIf clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal Then
                    dblSamplingTotAmt += dblqty * objtr.Unit_Cogs / convFac
                End If
            End If

            If (clsCommon.CompairString(objtr.Scheme_Item, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Sampling_Item, "Y") = CompairStringResult.Equal) Then
                dblTotTaxAmtFOC1 += objtr.TOT_TAX1_Amt 'dblqty * objtr.TAX1_Amt
                dblTotTaxAmtFOC2 += objtr.TOT_TAX2_Amt ''dblqty * objtr.TAX2_Amt
                dblTotTaxAmtFOC3 += objtr.TOT_TAX3_Amt ''dblqty * objtr.TAX3_Amt
                dblTotTaxAmtFOC4 += objtr.TOT_TAX4_Amt ''dblqty * objtr.TAX4_Amt
                dblTotTaxAmtFOC5 += objtr.TOT_TAX5_Amt ''dblqty * objtr.TAX5_Amt
                dblTotTaxAmtFOC6 += objtr.TOT_TAX6_Amt ''dblqty * objtr.TAX6_Amt
            End If

            dblTotTaxAmt1 += objtr.TOT_TAX1_Amt ''dblqty * objtr.TAX1_Amt
            dblTotTaxAmt2 += objtr.TOT_TAX2_Amt ''dblqty * objtr.TAX2_Amt
            dblTotTaxAmt3 += objtr.TOT_TAX3_Amt ''dblqty * objtr.TAX3_Amt
            dblTotTaxAmt4 += objtr.TOT_TAX4_Amt ''dblqty * objtr.TAX4_Amt
            dblTotTaxAmt5 += objtr.TOT_TAX5_Amt ''dblqty * objtr.TAX5_Amt
            dblTotTaxAmt6 += objtr.TOT_TAX6_Amt ''dblqty * objtr.TAX6_Amt
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

            ''''Item wise GL Account''''''
            Dim objItemWiseGLAC As clsJournalDetailTemp = New clsJournalDetailTemp()
            qry = "select Sales_Return_Account from TSPL_SALES_ACCOUNTS where Sales_Class_Code in (select Sale_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')"
            objItemWiseGLAC.Account_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            If clsCommon.myLen(objItemWiseGLAC.Account_code) <= 0 Then
                Throw New Exception("Please set Sale Account of Sale Account set for item : " + objtr.Item_Code)
            End If


            If (objtr.Total_net_Amt - dblCurrExcise - dblCurrCess - dblCurrHECess) < 0 Then
                dblCurrExcise = 0
                dblCurrCess = 0
                dblCurrHECess = 0
            End If
            qry = "select  * from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + obj.Invoice_No + "' and Invoice_Type='Excise'"
            Dim dtSI As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim isSaleInvoiceLocation As Boolean = False
            If dtSI IsNot Nothing AndAlso dtSI.Rows.Count > 0 Then
                isSaleInvoiceLocation = True
            End If

            Dim dblSaleAmount As Double = IIf(isSaleInvoiceLocation, objtr.Total_net_Amt, (objtr.Total_net_Amt - dblCurrExcise - dblCurrCess - dblCurrHECess))

            '''' commented by priti on 29/11/2013
            'Dim dblSaleAmount As Double = objtr.Total_net_Amt - dblCurrExcise - dblCurrCess - dblCurrHECess
            '''' coomented ends here

            objItemWiseGLAC.Account_code = clsERPFuncationality.ChangeGLAccountLocationSegment(objItemWiseGLAC.Account_code, obj.Location, trans)
            objItemWiseGLAC.Amount = -1 * dblSaleAmount
            ArrItemWiseGLAC.Add(objItemWiseGLAC)
            ''''End of Item wise GL Account''''''

            dblExcise += dblCurrExcise
            dblCess += dblCurrCess
            dblHECess += dblCurrHECess




            ''Update [Sale_Account_Amount] in sale invoice detail table 
            qry = " Update TSPL_SALE_RETURN_DETAIL set Sale_Account_Amount='" + clsCommon.myCstr(dblSaleAmount) + "' where Sale_Return_No='" + objtr.Sale_Return_No + "' and Sale_Return_Id='" + clsCommon.myCstr(objtr.Sale_Return_Id) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            ''End of Update [GL_Account_Amount] in sale invoice detail table 


            If objtr.Unit_Cogs > 0 Then
                If clsCommon.CompairString(objtr.Scheme_Item, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objtr.Promo_Scheme_Item, "N") = CompairStringResult.Equal AndAlso clsCommon.CompairString(objtr.Sampling_Item, "N") = CompairStringResult.Equal Then
                    cogs += Math.Round((dblqty * objtr.Unit_Cogs / convFac), 2)
                ElseIf objtr.Scheme_Item = "Y" Or objtr.Sampling_Item = "Y" Then
                    schemeCogs += Math.Round(dblqty * objtr.Unit_Cogs / convFac, 2)
                ElseIf objtr.Promo_Scheme_Item = "Y" Then
                    promoCogs += Math.Round(dblqty * objtr.Unit_Cogs, 2)
                End If
            End If
        Next
        ''Update [Tot_Sale_Account_Amount] in sale invoice Header table 
        qry = " Update TSPL_SALE_RETURN_HEAD set Sale_Account_Amount=(select sum(Sale_Account_Amount) from TSPL_SALE_RETURN_DETAIL where TSPL_SALE_RETURN_DETAIL.Sale_Return_No='" + obj.Sale_Return_No + "') where TSPL_SALE_RETURN_HEAD.Sale_Return_No='" + obj.Sale_Return_No + "' "
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        ''End of Update [Tot_Sale_Account_Amount] in sale invoice Header table 

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
        If obj.Total_Invoice_Amt > 0 Then
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


        If obj.Inv_Detail_Total_Amt > 0 Then
            For Each objItemWiseGLAC As clsJournalDetailTemp In ArrItemWiseGLAC
                If objItemWiseGLAC.Amount <> 0 Then
                    Dim Acc1() As String = {objItemWiseGLAC.Account_code, objItemWiseGLAC.Amount}
                    ArryLstGLAC.Add(Acc1)
                End If
            Next
        End If


        Dim objTM As clsTaxMaster

        ''for normal Sale
        'If dblExcise > 0 Then
        '    objTM = clsTaxMaster.GetTaxDetailsForSale("BED", trans)
        '    If objTM IsNot Nothing Then
        '        If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
        '            Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + obj.TAX1)
        '        End If
        '        objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
        '        Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblExcise)}
        '        ArryLstGLAC.Add(Acc1)

        '        If dblShipmentTotCost > 0 Then
        '            If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
        '                Throw New Exception("Please set Tax Liablility Account of Tax Authority " + obj.TAX1)
        '            End If
        '            objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
        '            Dim Acc() As String = {objTM.Tax_Net_Payable, (dblExcise + dblExciseFOC)}
        '            ArryLstGLAC.Add(Acc)
        '        End If
        '    End If
        'End If


        'If dblCess > 0 Then
        '    objTM = clsTaxMaster.GetTaxDetailsForSale("ECESS", trans)
        '    If objTM IsNot Nothing Then
        '        If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
        '            Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + obj.TAX1)
        '        End If
        '        objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
        '        Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblCess)}
        '        ArryLstGLAC.Add(Acc1)

        '        If dblShipmentTotCost > 0 Then
        '            If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
        '                Throw New Exception("Please set Tax Liablility Account of Tax Authority " + obj.TAX1)
        '            End If
        '            objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
        '            Dim Acc() As String = {objTM.Tax_Net_Payable, (dblCess + dblCessFOC)}
        '            ArryLstGLAC.Add(Acc)
        '        End If
        '    End If
        'End If



        'If dblHECess > 0 Then
        '    objTM = clsTaxMaster.GetTaxDetailsForSale("HCESS", trans)
        '    If objTM IsNot Nothing Then
        '        If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
        '            Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + obj.TAX1)
        '        End If
        '        objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
        '        Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblHECess)}
        '        ArryLstGLAC.Add(Acc1)

        '        If dblShipmentTotCost > 0 Then
        '            If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
        '                Throw New Exception("Please set Tax Liablility Account of Tax Authority " + obj.TAX1)
        '            End If
        '            objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
        '            Dim Acc() As String = {objTM.Tax_Net_Payable, (dblHECess + dblHECessFOC)}
        '            ArryLstGLAC.Add(Acc)
        '        End If
        '    End If
        'End If

        ''''addded by priti on 29/11/2013
        Dim objItemTaxCode As clsItemTax = Nothing
        Dim strFOC As String
        For Each objtr As clsSalesReturnDetail In obj.Arr
            strItem = objtr.Item_Code
            strFOC = objtr.Scheme_Item
            'If clsCommon.CompairString(strFOC, "N") = CompairStringResult.Equal Then
            objItemTaxCode = clsItemTax.GetData(strItem, trans)
            'End If
            If Not objItemTaxCode Is Nothing Then
                Exit For
            End If
        Next

        'If Not objItemTaxCode Is Nothing Then


        'Throw New Exception("Please set Tax Code for  " + strItem)

        qry = "select  * from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + obj.Invoice_No + "' and Invoice_Type='Excise'"
        Dim dtSI1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim isSaleInvoiceLocation1 As Boolean = False
        If dtSI1 IsNot Nothing AndAlso dtSI1.Rows.Count > 0 Then
            isSaleInvoiceLocation1 = True
        End If
        '''' priti code ends here

        If dblTotTaxAmt1 > 0 Then
            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX1, trans)
            If objTM IsNot Nothing Then
                If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then

                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + obj.TAX1)
                    End If
                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt1 - dblTotTaxAmtFOC1)}
                    ArryLstGLAC.Add(Acc1)

                    '''' addded by priti on 29/11/2013 for GL entry of non excisable of sale return for manufactured item 
                Else
                    If Not objItemTaxCode Is Nothing Then
                        objTM = clsTaxMaster.GetTaxDetailsForSale(objItemTaxCode.Tax_Code_Excise, trans)
                        If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                            Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + objItemTaxCode.Tax_Code_Excise)
                        End If
                        objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                        Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblExcise)}
                        ArryLstGLAC.Add(Acc1)
                    End If
                    '''' priti codes 29/11/2013 ends here


                    'Commented as asked by amit sir
                    'If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                    '    Throw New Exception("Please set Tax Net Payale Account of Tax Authority " + obj.TAX1)
                    'End If
                    'objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                    'Dim Acc2() As String = {objTM.Tax_Net_Payable, dblTotTaxAmt1}
                    'ArryLstGLAC.Add(Acc2)
                    End If
                    objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX1, trans)
                    If Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                        If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                            Throw New Exception("Please set Tax Liablility Account of Tax Authority " + obj.TAX1)
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
                If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + obj.TAX2)
                    End If
                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt2 - dblTotTaxAmtFOC2)}
                    ArryLstGLAC.Add(Acc1)


                    'If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                    '    Throw New Exception("Please set Tax Net Payale Account of Tax Authority " + obj.TAX2)
                    'End If
                    'objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                    'Dim Acc2() As String = {objTM.Tax_Net_Payable, dblTotTaxAmt2}
                    'ArryLstGLAC.Add(Acc2)
                End If
                If Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Authority " + obj.TAX2)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, obj.Location, trans)
                    Dim Acc() As String = {objTM.Tax_Liability_Account, -1 * (dblTotTaxAmt2)}
                    ArryLstGLAC.Add(Acc)
                End If

            End If

            '''' addded by priti on 29/11/2013 for GL entry of non excisable of sale return
        Else
            'objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX2, trans)
            If Not objItemTaxCode Is Nothing And isSaleInvoiceLocation1 = False Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objItemTaxCode.Tax_Code_Ecess, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + objItemTaxCode.Tax_Code_Ecess)
                    End If

                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)

                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblCess)}
                    ArryLstGLAC.Add(Acc1)
                End If
            End If
            '''' priti codes 29/11/2013 ends here
        End If


        If dblTotTaxAmt3 > 0 Then
            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX3, trans)
            If objTM IsNot Nothing Then
                If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                    Throw New Exception("Please set Tax Liablility Account of Tax Authority " + obj.TAX3)
                End If
                If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then

                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + obj.TAX3)
                    End If
                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt3 - dblTotTaxAmtFOC3)}
                    ArryLstGLAC.Add(Acc1)


                    'If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                    '    Throw New Exception("Please set Tax Net Payale Account of Tax Authority " + obj.TAX3)
                    'End If
                    'objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                    'Dim Acc2() As String = {objTM.Tax_Net_Payable, dblTotTaxAmt3}
                    'ArryLstGLAC.Add(Acc2)
                End If
                If Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Authority " + obj.TAX5)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, obj.Location, trans)
                    Dim Acc() As String = {objTM.Tax_Liability_Account, -1 * (dblTotTaxAmt3)}
                    ArryLstGLAC.Add(Acc)
                End If

            End If
            '''' addded by priti on 29/11/2013 for GL entry of non excisable of sale return
        Else
            'objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX3, trans)
            If Not objItemTaxCode Is Nothing And isSaleInvoiceLocation1 = False Then
                objTM = clsTaxMaster.GetTaxDetailsForSale(objItemTaxCode.Tax_Code_Hcess, trans)
                If objTM IsNot Nothing Then
                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + objItemTaxCode.Tax_Code_Hcess)
                    End If

                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblHECess)}
                    ArryLstGLAC.Add(Acc1)
                End If
            End If
            '''' priti codes 29/11/2013 ends here
        End If

        If dblTotTaxAmt4 > 0 Then
            objTM = clsTaxMaster.GetTaxDetailsForSale(obj.TAX4, trans)
            If objTM IsNot Nothing Then
                If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then

                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + obj.TAX4)
                    End If
                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt4 - dblTotTaxAmtFOC4)}
                    ArryLstGLAC.Add(Acc1)

                    'If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                    '    Throw New Exception("Please set Tax  Net Payale Account of Tax Authority " + obj.TAX4)
                    'End If
                    'objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                    'Dim Acc2() As String = {objTM.Tax_Net_Payable, dblTotTaxAmt4}
                    'ArryLstGLAC.Add(Acc2)
                End If
                If Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Authority " + obj.TAX4)
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
                If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then

                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + obj.TAX5)
                    End If
                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt5 - dblTotTaxAmtFOC5)}
                    ArryLstGLAC.Add(Acc1)

                    'If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                    '    Throw New Exception("Please set Tax  Net Payale Account of Tax Authority " + obj.TAX5)
                    'End If
                    'objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                    'Dim Acc2() As String = {objTM.Tax_Net_Payable, (dblTotTaxAmt5)}
                    'ArryLstGLAC.Add(Acc2)
                End If
                If Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Authority " + obj.TAX5)
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
                If clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then

                    If clsCommon.myLen(objTM.Tax_Recoverable_Account) <= 0 Then
                        Throw New Exception("Please set Tax Recoverable Account of Tax Authority " + obj.TAX6)
                    End If
                    objTM.Tax_Recoverable_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Recoverable_Account, obj.Location, trans)
                    Dim Acc1() As String = {objTM.Tax_Recoverable_Account, -1 * (dblTotTaxAmt6 - dblTotTaxAmtFOC6)}
                    ArryLstGLAC.Add(Acc1)


                    'If clsCommon.myLen(objTM.Tax_Net_Payable) <= 0 Then
                    '    Throw New Exception("Please set Tax Net Payale Account of Tax Authority " + obj.TAX6)
                    'End If
                    'objTM.Tax_Net_Payable = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Net_Payable, obj.Location, trans)
                    'Dim Acc2() As String = {objTM.Tax_Net_Payable, dblTotTaxAmt6}
                    'ArryLstGLAC.Add(Acc2)
                End If
                If Not clsCommon.CompairString(objTM.Type, "E") = CompairStringResult.Equal Then
                    If clsCommon.myLen(objTM.Tax_Liability_Account) <= 0 Then
                        Throw New Exception("Please set Tax Liablility Account of Tax Authority " + obj.TAX6)
                    End If
                    objTM.Tax_Liability_Account = clsERPFuncationality.ChangeGLAccountLocationSegment(objTM.Tax_Liability_Account, obj.Location, trans)
                    Dim Acc() As String = {objTM.Tax_Liability_Account, -1 * (dblTotTaxAmt6)}
                    ArryLstGLAC.Add(Acc)
                End If
            End If
        End If

        'End If


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
                ''Dim Acc1() As String = {strCogsAccount, (dblShipmentTotCost - dblSchemeTotAmt - dblSamplingTotAmt)}
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
            Dim Acc1() As String = {strSchemeCtrlAccount, dblSchemeTotAmt - dblExciseFOC - dblCessFOC - dblHECessFOC} 'comment for wrong journal entry made for scheme item
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
        qry = "select Shipment_Type from TSPL_SALE_INVOICE_HEAD where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + obj.Invoice_No + "'"


        If clsCommon.CompairString(clsDBFuncationality.getSingleValue(qry, trans), "Sale") = CompairStringResult.Equal Then
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
        Else
            qry = "SELECT PA.Reserve_Stock FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
             " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
             " TSPL_GL_ACCOUNTS AS GLA ON PA.Reserve_Stock = GLA.Account_Code WHERE IM.Item_Code='" + obj.Arr(0).Item_Code + "'"
            Dim strReverseStockAccount As String = clsDBFuncationality.getSingleValue(qry, trans)
            If clsCommon.myLen(strReverseStockAccount) <= 0 Then
                Throw New Exception("Please set Reverse Stock Account of Purchase Account set")
            End If

            strReverseStockAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strReverseStockAccount, obj.Location, trans)
            Dim Acc1() As String = {strReverseStockAccount, -1 * shipmentCogs}
            ArryLstGLAC.Add(Acc1)
        End If

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


        transportSql.FunGrnlEntryWithTrans(obj.Location, False, strVourcherNoForRecreateOnly, trans, obj.Sale_Return_Date, "Against Sale Return No" + obj.Sale_Return_No, "SD-SR", "Sale Return", obj.Sale_Return_No, "", "C", obj.Cust_Code, obj.Cust_Name, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstNew)
        ''end of GL Entry
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

            ''Delete the items whose qty is Zero
            qry = "delete from tspl_sale_return_detail where Sale_Return_No='" + strDocNo + "' and Return_Qty='0' "
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Sale_Return_Id,Sale_Return_No from tspl_sale_return_detail where Sale_Return_No='" + strDocNo + "' order by Sale_Return_Id", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim counter As Integer = 1
                For Each dr As DataRow In dt.Rows
                    qry = "update tspl_sale_return_detail set Sale_Return_Id='" + clsCommon.myCstr(counter) + "' where Sale_Return_No='" + strDocNo + "' and Sale_Return_Id='" + clsCommon.myCstr(dr("Sale_Return_Id")) + "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    counter += 1
                Next
            End If
            ''End of Delete the items whose qty is Zero


            Dim obj As clsSalesReturnHead = clsSalesReturnHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Sale_Return_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Sales And Distribution", "Sale Return", obj.Location, obj.Sale_Return_Date, trans)

            If (clsCommon.CompairString(obj.Is_Post, "Y") = CompairStringResult.Equal) Then
                Throw New Exception("Already Post on :" + obj.Sale_Return_Posting_Date)
            End If

            CreateJournalEntry("", obj, trans)

            
            qry = "select  TSPL_SALE_INVOICE_HEAD.Shipment_Type,TSPL_SHIPMENT_MASTER.Transfer_No "
            qry += " from TSPL_SALE_INVOICE_HEAD "
            qry += " left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SALE_INVOICE_HEAD.Shipment_No "
            qry += " where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + obj.Invoice_No + "'"
            Dim dtSINo As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dtSINo Is Nothing OrElse dtSINo.Rows.Count <= 0 Then
                Throw New Exception("Sale invoice no not found")
            End If

            Dim dtInvoiceDate As Date = New Date(obj.Invoice_Date.Year, obj.Invoice_Date.Month, 1)
            dtInvoiceDate = dtInvoiceDate.AddMonths(1)

            For Each objtr As clsSalesReturnDetail In obj.Arr
                If objtr.Return_Qty > 0 Then
                    ''For Update Amount of Target
                    Dim strSchemeCode As String = objtr.From_Scheme_Code
                    If clsCommon.myLen(strSchemeCode) >= 2 Then
                        Dim strTwoCharacher As String = strSchemeCode.Substring(0, 2)
                        If clsCommon.CompairString(strTwoCharacher, "MS") = CompairStringResult.Equal Then
                            If clsCommon.myLen(objtr.Discount_Code) > 0 AndAlso objtr.Target_Discount_Amt > 0 Then
                                qry = "select Cust_Code,Discount_Type,Month_Year,Bal_Amount,Amount from TSPL_TARGET_MASTER where Cust_Code='" + obj.Cust_Code + "' and Discount_Type='" + objtr.Discount_Code + "' and Month_Year<'" + clsCommon.GetPrintDate(dtInvoiceDate, "dd/MMM/yyyy") + "' order by Month_Year desc"
                                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                Dim targetAmt As Decimal = objtr.Target_Discount_Amt
                                For Each dr As DataRow In dtTemp.Rows
                                    Dim dblAmtToUpdate As Double = IIf(clsCommon.myCdbl(dr("Amount")) - clsCommon.myCdbl(dr("Bal_Amount")) < targetAmt, clsCommon.myCdbl(dr("Amount")) - clsCommon.myCdbl(dr("Bal_Amount")), targetAmt)
                                    targetAmt -= dblAmtToUpdate
                                    qry = "Update TSPL_TARGET_MASTER set Bal_Amount=Bal_Amount+'" + clsCommon.myCstr(dblAmtToUpdate) + "' "
                                    qry += " where  Cust_Code='" + obj.Cust_Code + "' and discount_Type='" + objtr.Discount_Code + "' and Month_Year ='" + clsCommon.GetPrintDate(clsCommon.myCDate(dr("Month_Year")), "dd/MMM/yyyy") + "'"
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                    If targetAmt <= 0 Then
                                        Exit For
                                    End If
                                Next

                            End If
                        End If
                    End If
                End If
            Next

            If clsCommon.CompairString(clsCommon.myCstr(dtSINo.Rows(0)("Shipment_Type")), "Sale") = CompairStringResult.Equal Then
                ''Inventory Movement  and itemLocation 
                Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
                Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
                For Each objTR As clsSalesReturnDetail In obj.Arr
                    Dim objInventoryMovemnt As New clsInventoryMovement()
                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = obj.Location
                    objInventoryMovemnt.Item_Code = objTR.Item_Code
                    objInventoryMovemnt.Item_Desc = objTR.Item_Desc
                    objInventoryMovemnt.Qty = objTR.Actual_Return_Qty
                    objInventoryMovemnt.UOM = objTR.Unit_code
                    objInventoryMovemnt.Basic_Cost = IIf(objTR.Unit_Cogs = 0, 0, objTR.Basic_Rate)
                    objInventoryMovemnt.Rec_Cost = IIf(objTR.Unit_Cogs = 0, 0, objTR.Total_Basic_Amt)
                    objInventoryMovemnt.Add_Cost = IIf(objTR.Unit_Cogs = 0, 0, objTR.Total_Tax_Amt)
                    objInventoryMovemnt.Net_Cost = IIf(objTR.Unit_Cogs = 0, 0, objTR.Total_Item_Amt)
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
                    objLocationDetails.Item_Qty = clsCommon.myCdbl(objTR.Actual_Return_Qty) / dblConvFac
                    objLocationDetails.Amount = IIf(objTR.Unit_Cogs = 0, 0, clsCommon.myCdbl(objTR.Total_Item_Amt))
                    objLocationDetails.MRP = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select MRP_Amt from TSPL_SALE_RETURN_DETAIL where Item_Code='" + clsCommon.myCstr(objTR.Item_Code) + "' and Sale_Return_No='" + strDocNo + "' ", trans)) * dblConvFac
                    'dt = clsDBFuncationality.GetDataTable("select top 1 MFG_Date,Expiry_Date,Batch_No from TSPL_ADJUSTMENT_DETAIL where Item_Code='" + objTR.Item_Code + "'", trans)
                    'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    '    objLocationDetails.Batch_No = clsCommon.myCstr(dt.Rows(0)("Batch_No"))
                    '    objLocationDetails.MFG_Date = clsCommon.myCDate(dt.Rows(0)("MFG_Date"))
                    '    objLocationDetails.Expiry_Date = clsCommon.myCDate(dt.Rows(0)("Expiry_Date"))
                    'End If
                    objLocationDetails.ItemType = "FM"
                    ArrLocationDetails.Add(objLocationDetails)



                    ''Handling Leak Qty by Cahnge it's code in Father Code
                    If objTR.Leak_Qty > 0 Then
                        Dim strFatherCode As String = clsItemMaster.GetFatherCode(objTR.Item_Code, trans)
                        If clsCommon.myLen(strFatherCode) > 0 Then
                            Dim strNewUOM As String = ""
                            If clsCommon.CompairString(objTR.Unit_code, "FC") = CompairStringResult.Equal Then
                                strNewUOM = "EC"
                            ElseIf clsCommon.CompairString(objTR.Unit_code, "FB") = CompairStringResult.Equal Then
                                strNewUOM = "EB"
                            End If

                            dblConvFac = clsItemMaster.GetConvertionFactor(strFatherCode, strNewUOM, trans)
                            If dblConvFac = 0 Then
                                Throw New Exception("Conversion Factor found zero for item :" + strFatherCode + " and Uom:'" + strNewUOM)
                            End If
                            Dim dblFatherRate As Double = clsItemPriceMaster.GetMRPOfFinishItem(strFatherCode, objTR.Unit_code, trans)

                            Dim strFatherName = clsItemMaster.GetItemName(strFatherCode, trans)
                            objInventoryMovemnt = New clsInventoryMovement()
                            objInventoryMovemnt.InOut = "I"
                            objInventoryMovemnt.Location_Code = obj.Location

                            objInventoryMovemnt.Cust_Code = obj.Cust_Code
                            objInventoryMovemnt.Cust_Name = obj.Cust_Name

                            objInventoryMovemnt.Item_Code = strFatherCode
                            objInventoryMovemnt.Item_Desc = strFatherName
                            objInventoryMovemnt.Qty = objTR.Leak_Qty
                            objInventoryMovemnt.UOM = strNewUOM
                            objInventoryMovemnt.Basic_Cost = dblFatherRate * objTR.Leak_Qty
                            objInventoryMovemnt.Add_Cost = 0
                            objInventoryMovemnt.Net_Cost = dblFatherRate * objTR.Leak_Qty
                            objInventoryMovemnt.ItemType = "E"
                            objInventoryMovemnt.MRP = objTR.MRP_Amt
                            ArrInventoryMovement.Add(objInventoryMovemnt)


                            objLocationDetails = New clsItemLocationDetails()
                            objLocationDetails.Item_Code = strFatherCode
                            objLocationDetails.Item_Desc = strFatherName
                            objLocationDetails.Location_Code = obj.Location
                            objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                            objLocationDetails.Item_Qty = objTR.Leak_Qty / dblConvFac
                            objLocationDetails.Amount = dblFatherRate * objTR.Leak_Qty
                            objLocationDetails.MRP = dblFatherRate * dblConvFac
                            If dblConvFac <> 1 Then
                                objLocationDetails.MRP += 100 ''100 is Shell Cost
                            End If
                            objLocationDetails.ItemType = "E"
                            ArrLocationDetails.Add(objLocationDetails)
                        End If
                    End If
                Next
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("Sale Return", strDocNo, obj.Sale_Return_Date, clsCommon.GetPrintDate(obj.Sale_Return_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Sale_Return_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)

                '' ''End of  Inventory Movement  
            Else
                ''Update Transfer Qty
                Dim strTransferNo As String = clsCommon.myCstr(dtSINo.Rows(0)("Transfer_No"))
                For Each objTR As clsSalesReturnDetail In obj.Arr
                    Dim convertFact As Decimal = clsItemMaster.GetConvertionFactor(objTR.Item_Code, objTR.Unit_code, trans)

                    qry = "SELECT ISNULL( Pending_Balance_In_Bottle,0) from TSPL_TRANSFER_DETAIL WHERE Transfer_No='" + strTransferNo + "' AND " & _
                    " Item_Code='" + objTR.Item_Code + "'  AND MRP='" + (CDec(objTR.MRP_Amt) * convertFact).ToString() + "' "
                    Dim balanceqtyInBottle As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                    qry = "SELECT pending_Qty from TSPL_TRANSFER_DETAIL WHERE Transfer_No='" + strTransferNo + "' AND " & _
                   " Item_Code='" + objTR.Item_Code + "'  AND MRP='" + (CDec(objTR.MRP_Amt) * convertFact).ToString() + "' "
                    Dim balanceqty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

                    Dim shippedQty As Decimal = 0
                    Dim otherconversion As Decimal
                    Dim shippedQtyInBottle As Decimal = 0
                    If convertFact = 1 Then
                        otherconversion = clsDBFuncationality.getSingleValue("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(objTR.Item_Code) + "' and UOM_Code <> '" + Convert.ToString(objTR.Unit_code) + "' AND UM.Create_Price = 'Y'", trans)
                        shippedQtyInBottle = CDec(objTR.Return_Qty) * otherconversion
                    Else
                        shippedQtyInBottle = CDec(objTR.Return_Qty)
                    End If
                    qry = "UPDATE TSPL_TRANSFER_DETAIL SET Pending_Qty='" + (balanceqty + CDec(objTR.Return_Qty) * 1 / convertFact).ToString() + "', Pending_Balance_In_Bottle ='" + Convert.ToString(balanceqtyInBottle + shippedQtyInBottle) + "' WHERE Transfer_No='" + strTransferNo + "' AND " & _
                    " Item_Code='" + objTR.Item_Code + "' AND MRP='" + (CDec(objTR.MRP_Amt) * convertFact).ToString() + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    ''End of Update Transfer Qty
                Next
            End If




            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegtiveOfSaleInvoiceBlanceAmt, clsFixedParameterCode.AllowNegtiveOfSaleInvoiceBlanceAmt, trans)) = 1 Then
                qry = " update TSPL_SALE_INVOICE_HEAD set Balance_Amt= case when  Balance_Amt-" + clsCommon.myCstr(clsCommon.myCdbl(obj.Empty_Value + obj.Total_Invoice_Amt)) + "<0 then 0 else Balance_Amt-" + clsCommon.myCstr(clsCommon.myCdbl(obj.Empty_Value + obj.Total_Invoice_Amt)) + " end where Sale_Invoice_No='" + obj.Invoice_No + "' "
            Else
                qry = " update TSPL_SALE_INVOICE_HEAD set Balance_Amt=  Balance_Amt-" + clsCommon.myCstr(clsCommon.myCdbl(obj.Empty_Value + obj.Total_Invoice_Amt)) + "  where Sale_Invoice_No='" + obj.Invoice_No + "' "
            End If
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "Update TSPL_SALE_RETURN_HEAD set Is_Post='Y', Sale_Return_Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy") + "' where Sale_Return_No='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            'Throw New Exception("exception")
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
Public Class clsSalesReturnDetail
#Region "Variables"
    Public Sale_Return_Id As Integer
    Public Sale_Return_No As String
    Public Item_Code As String
    Public Item_Desc As String
    Public Price_Date As Date
    Public Invoice_Qty As Double
    Public Return_Qty As Double
    Public Actual_Return_Qty As Double
    Public Balance_Qty As Double
    Public Unit_code As String
    Public Location As String
    Public Price_code As String
    Public Scheme_Applicable As String
    Public Scheme_Code_Qty As String
    Public Scheme_Item As String
    Public Promo_Scheme_Applicable As String
    Public Promo_Scheme_Code As String
    Public Promo_Scheme_Item As String
    Public Scheme_Disc_Applicable As String
    Public Scheme_Code_Cash As String
    Public Sampling_Item As String
    Public MRP_Amt As Double
    Public Basic_Rate As Double
    Public Item_Assessable_Rate As Double
    Public Disc_Amt As Double
    Public Item_Net_Amt As Double
    Public TAX1 As String
    Public TAX1_Rate As Double
    Public TAX1_Assessable_Amt As Double
    Public TAX1_Amt As Double
    Public TAX2 As String
    Public TAX2_Rate As Double
    Public TAX2_Assessable_Amt As Double
    Public TAX2_Amt As Double
    Public TAX3 As String
    Public TAX3_Rate As Double
    Public TAX3_Assessable_Amt As Double
    Public TAX3_Amt As Double
    Public TAX4 As String
    Public TAX4_Rate As Double
    Public TAX4_Assessable_Amt As Double
    Public TAX4_Amt As Double
    Public TAX5 As String
    Public TAX5_Rate As Double
    Public TAX5_Assessable_Amt As Double
    Public TAX5_Amt As Double
    Public TAX6 As String
    Public TAX6_Rate As Double
    Public TAX6_Assessable_Amt As Double
    Public TAX6_Amt As Double
    Public TAX7 As String
    Public TAX7_Rate As Double
    Public TAX7_Assessable_Amt As Double
    Public TAX7_Amt As Double
    Public TAX8 As String
    Public TAX8_Rate As Double
    Public TAX8_Assessable_Amt As Double
    Public TAX8_Amt As Double
    Public TAX9 As String
    Public TAX9_Rate As Double
    Public TAX9_Assessable_Amt As Double
    Public TAX9_Amt As Double
    Public TAX10 As String
    Public TAX10_Rate As Double
    Public TAX10_Assessable_Amt As Double
    Public TAX10_Amt As Double

    Public Item_Tax As Double
    Public Total_Assessable_Amt As Double
    Public Total_MRP_Amt As Double
    Public Total_Basic_Amt As Double
    Public Total_Disc_Amt As Double
    Public Total_net_Amt As Double
    Public Total_Tax_Amt As Double
    Public Total_Item_Amt As Double
    Public Empty_Value As Double
    Public TPT As Double
    Public Total_TPT As Double
    Public Empty_Value_Shell As Double
    Public Empty_Value_Bottle As Double
    Public Cust_Discount As Double
    Public Total_Cust_Discount As Double
    Public Unit_Cogs As Double
    Public Price_Amount1 As Double
    Public Price_Amount2 As Double
    Public Price_Amount3 As Double
    Public Price_Amount4 As Double
    Public Price_Amount5 As Double
    Public Price_Amount6 As Double
    Public Price_Amount7 As Double
    Public Price_Amount8 As Double
    Public Price_Amount9 As Double
    Public Price_Amount10 As Double
    Public Main_item As String
    Public Discount_Code As String
    Public Cust_Item_Discount_NoTax As Double

    Public Target_Discount_Amt As Double
    Public From_Scheme_Code As String
    Public Leak_Qty As Double = 0
    Public Burst_Qty As Double = 0
    Public Short_Qty As Double = 0


    Public TOT_TAX1_Amt As Double = 0
    Public TOT_TAX2_Amt As Double = 0
    Public TOT_TAX3_Amt As Double = 0
    Public TOT_TAX4_Amt As Double = 0
    Public TOT_TAX5_Amt As Double = 0
    Public TOT_TAX6_Amt As Double = 0
    Public TOT_TAX7_Amt As Double = 0
    Public TOT_TAX8_Amt As Double = 0
    Public TOT_TAX9_Amt As Double = 0
    Public TOT_TAX10_Amt As Double = 0

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsSalesReturnDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSalesReturnDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Sale_Return_Id", obj.Sale_Return_Id)
                clsCommon.AddColumnsForChange(coll, "Sale_Return_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Invoice_Qty", obj.Invoice_Qty)
                clsCommon.AddColumnsForChange(coll, "Return_Qty", obj.Return_Qty)
                clsCommon.AddColumnsForChange(coll, "Actual_Return_Qty", obj.Actual_Return_Qty)
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
                clsCommon.AddColumnsForChange(coll, "MRP_Amt", obj.MRP_Amt)
                clsCommon.AddColumnsForChange(coll, "Basic_Rate", obj.Basic_Rate)
                clsCommon.AddColumnsForChange(coll, "Item_Assessable_Rate", obj.Item_Assessable_Rate)
                clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                clsCommon.AddColumnsForChange(coll, "TAX1_Assessable_Amt", obj.TAX1_Assessable_Amt)
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


                clsCommon.AddColumnsForChange(coll, "TOT_TAX1_Amt", obj.TOT_TAX1_Amt)
                clsCommon.AddColumnsForChange(coll, "TOT_TAX2_Amt", obj.TOT_TAX2_Amt)
                clsCommon.AddColumnsForChange(coll, "TOT_TAX3_Amt", obj.TOT_TAX3_Amt)
                clsCommon.AddColumnsForChange(coll, "TOT_TAX4_Amt", obj.TOT_TAX4_Amt)
                clsCommon.AddColumnsForChange(coll, "TOT_TAX5_Amt", obj.TOT_TAX5_Amt)
                clsCommon.AddColumnsForChange(coll, "TOT_TAX6_Amt", obj.TOT_TAX6_Amt)
                clsCommon.AddColumnsForChange(coll, "TOT_TAX7_Amt", obj.TOT_TAX7_Amt)
                clsCommon.AddColumnsForChange(coll, "TOT_TAX8_Amt", obj.TOT_TAX8_Amt)
                clsCommon.AddColumnsForChange(coll, "TOT_TAX9_Amt", obj.TOT_TAX9_Amt)
                clsCommon.AddColumnsForChange(coll, "TOT_TAX10_Amt", obj.TOT_TAX10_Amt)



                clsCommon.AddColumnsForChange(coll, "Item_Tax", obj.Item_Tax)
                clsCommon.AddColumnsForChange(coll, "Total_Assessable_Amt", obj.Total_Assessable_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_MRP_Amt", obj.Total_MRP_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Basic_Amt", obj.Total_Basic_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Disc_Amt", obj.Total_Disc_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_net_Amt", obj.Total_net_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Tax_Amt", obj.Total_Tax_Amt)
                clsCommon.AddColumnsForChange(coll, "Total_Item_Amt", obj.Total_Item_Amt)
                clsCommon.AddColumnsForChange(coll, "Empty_Value", obj.Empty_Value)
                clsCommon.AddColumnsForChange(coll, "TPT", obj.TPT)
                clsCommon.AddColumnsForChange(coll, "Total_TPT", obj.Total_TPT)
                clsCommon.AddColumnsForChange(coll, "Empty_Value_Shell", obj.Empty_Value_Shell)
                clsCommon.AddColumnsForChange(coll, "Empty_Value_Bottle", obj.Empty_Value_Bottle)
                clsCommon.AddColumnsForChange(coll, "Cust_Discount", obj.Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Total_Cust_Discount", obj.Total_Cust_Discount)
                clsCommon.AddColumnsForChange(coll, "Unit_Cogs", obj.Unit_Cogs)
                clsCommon.AddColumnsForChange(coll, "price_amount1", obj.Price_Amount1)
                clsCommon.AddColumnsForChange(coll, "price_amount2", obj.Price_Amount2)
                clsCommon.AddColumnsForChange(coll, "price_amount3", obj.Price_Amount3)
                clsCommon.AddColumnsForChange(coll, "price_amount4", obj.Price_Amount4)
                clsCommon.AddColumnsForChange(coll, "price_amount5", obj.Price_Amount5)
                clsCommon.AddColumnsForChange(coll, "price_amount6", obj.Price_Amount6)
                clsCommon.AddColumnsForChange(coll, "price_amount7", obj.Price_Amount7)
                clsCommon.AddColumnsForChange(coll, "price_amount8", obj.Price_Amount8)
                clsCommon.AddColumnsForChange(coll, "price_amount9", obj.Price_Amount9)
                clsCommon.AddColumnsForChange(coll, "price_amount10", obj.Price_Amount10)
                clsCommon.AddColumnsForChange(coll, "Main_Item", obj.Main_item)
                clsCommon.AddColumnsForChange(coll, "Discount_Code", obj.Discount_Code)
                clsCommon.AddColumnsForChange(coll, "Cust_Item_Discount_NoTax", obj.Cust_Item_Discount_NoTax)

                clsCommon.AddColumnsForChange(coll, "Target_Discount_Amt", obj.Target_Discount_Amt)
                clsCommon.AddColumnsForChange(coll, "From_Scheme_Code", obj.From_Scheme_Code)
                clsCommon.AddColumnsForChange(coll, "Leak_Qty", obj.Leak_Qty)
                clsCommon.AddColumnsForChange(coll, "Burst_Qty", obj.Burst_Qty)
                clsCommon.AddColumnsForChange(coll, "Short_Qty", obj.Short_Qty)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALE_RETURN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsSalesReturnDetail)
        Dim arr As List(Of clsSalesReturnDetail) = Nothing
        Dim qry As String = "select TSPL_SALE_RETURN_DETAIL.Sale_Return_Id,TSPL_SALE_RETURN_DETAIL.Sale_Return_No,TSPL_SALE_RETURN_DETAIL.Item_Code,TSPL_SALE_RETURN_DETAIL.Item_Desc,TSPL_SALE_RETURN_DETAIL.Price_Date,TSPL_SALE_RETURN_DETAIL.Invoice_Qty,TSPL_SALE_RETURN_DETAIL.Return_Qty,TSPL_SALE_RETURN_DETAIL.Actual_Return_Qty ,TSPL_SALE_RETURN_DETAIL.Balance_Qty,TSPL_SALE_RETURN_DETAIL.Unit_code,TSPL_SALE_RETURN_DETAIL.Location,TSPL_SALE_RETURN_DETAIL.Price_code,TSPL_SALE_RETURN_DETAIL.Scheme_Applicable,TSPL_SALE_RETURN_DETAIL.Scheme_Code_Qty,TSPL_SALE_RETURN_DETAIL.Scheme_Item,TSPL_SALE_RETURN_DETAIL.Promo_Scheme_Applicable,TSPL_SALE_RETURN_DETAIL.Promo_Scheme_Code,TSPL_SALE_RETURN_DETAIL.Promo_Scheme_Item,TSPL_SALE_RETURN_DETAIL.Scheme_Disc_Applicable,TSPL_SALE_RETURN_DETAIL.Scheme_Code_Cash,TSPL_SALE_RETURN_DETAIL.Sampling_Item,TSPL_SALE_RETURN_DETAIL.MRP_Amt,TSPL_SALE_RETURN_DETAIL.Basic_Rate,TSPL_SALE_RETURN_DETAIL.Item_Assessable_Rate,TSPL_SALE_RETURN_DETAIL.Disc_Amt,TSPL_SALE_RETURN_DETAIL.Item_Net_Amt ,TSPL_SALE_RETURN_DETAIL.TAX1,TSPL_SALE_RETURN_DETAIL.TAX1_Rate,TSPL_SALE_RETURN_DETAIL.TAX1_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.TAX1_Amt,TSPL_SALE_RETURN_DETAIL.TAX2,TSPL_SALE_RETURN_DETAIL.TAX2_Rate,TSPL_SALE_RETURN_DETAIL.TAX2_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.TAX2_Amt,TSPL_SALE_RETURN_DETAIL.TAX3,TSPL_SALE_RETURN_DETAIL.TAX3_Rate,TSPL_SALE_RETURN_DETAIL.TAX3_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.TAX3_Amt,TSPL_SALE_RETURN_DETAIL.TAX4,TSPL_SALE_RETURN_DETAIL.TAX4_Rate,TSPL_SALE_RETURN_DETAIL.TAX4_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.TAX4_Amt,TSPL_SALE_RETURN_DETAIL.TAX5,TSPL_SALE_RETURN_DETAIL.TAX5_Rate,TSPL_SALE_RETURN_DETAIL.TAX5_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.TAX5_Amt,TSPL_SALE_RETURN_DETAIL.TAX6,TSPL_SALE_RETURN_DETAIL.TAX6_Rate,TSPL_SALE_RETURN_DETAIL.TAX6_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.TAX6_Amt,TSPL_SALE_RETURN_DETAIL.TAX7," & _
        " TSPL_SALE_RETURN_DETAIL.TAX7_Rate,TSPL_SALE_RETURN_DETAIL.TAX7_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.TAX7_Amt,TSPL_SALE_RETURN_DETAIL.TAX8,TSPL_SALE_RETURN_DETAIL.TAX8_Rate,TSPL_SALE_RETURN_DETAIL.TAX8_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.TAX8_Amt,TSPL_SALE_RETURN_DETAIL.TAX9,TSPL_SALE_RETURN_DETAIL.TAX9_Rate,TSPL_SALE_RETURN_DETAIL.TAX9_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.TAX9_Amt,TSPL_SALE_RETURN_DETAIL.TAX10,TSPL_SALE_RETURN_DETAIL.TAX10_Rate,TSPL_SALE_RETURN_DETAIL.TAX10_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.TAX10_Amt,TSPL_SALE_RETURN_DETAIL.Item_Tax,TSPL_SALE_RETURN_DETAIL.Total_Assessable_Amt,TSPL_SALE_RETURN_DETAIL.Total_MRP_Amt,TSPL_SALE_RETURN_DETAIL.Total_Basic_Amt,TSPL_SALE_RETURN_DETAIL.Total_Disc_Amt,TSPL_SALE_RETURN_DETAIL.Total_net_Amt,TSPL_SALE_RETURN_DETAIL.Total_Tax_Amt,TSPL_SALE_RETURN_DETAIL.Total_Item_Amt,TSPL_SALE_RETURN_DETAIL.Empty_Value,TSPL_SALE_RETURN_DETAIL.TPT,TSPL_SALE_RETURN_DETAIL.Total_TPT,TSPL_SALE_RETURN_DETAIL.Empty_Value_Shell,TSPL_SALE_RETURN_DETAIL.Empty_Value_Bottle,TSPL_SALE_RETURN_DETAIL.Cust_Discount,TSPL_SALE_RETURN_DETAIL.Total_Cust_Discount,TSPL_SALE_RETURN_DETAIL.Unit_Cogs,TSPL_SALE_RETURN_DETAIL.price_amount1,TSPL_SALE_RETURN_DETAIL.price_amount2 ,TSPL_SALE_RETURN_DETAIL.price_amount3 ,TSPL_SALE_RETURN_DETAIL.price_amount4 ,TSPL_SALE_RETURN_DETAIL.price_amount5,TSPL_SALE_RETURN_DETAIL.price_amount6,TSPL_SALE_RETURN_DETAIL.price_amount7,TSPL_SALE_RETURN_DETAIL.price_amount8,TSPL_SALE_RETURN_DETAIL.price_amount9,TSPL_SALE_RETURN_DETAIL.price_amount10,TSPL_SALE_RETURN_DETAIL.Main_Item,TSPL_SALE_RETURN_DETAIL.Discount_Code,TSPL_SALE_RETURN_DETAIL.Cust_Item_Discount_NoTax,TSPL_SALE_RETURN_DETAIL.From_Scheme_Code,TSPL_SALE_RETURN_DETAIL.Target_Discount_Amt,TSPL_SALE_RETURN_DETAIL.Leak_Qty,TSPL_SALE_RETURN_DETAIL.Burst_Qty,TSPL_SALE_RETURN_DETAIL.Short_Qty,TSPL_SALE_RETURN_DETAIL.TOT_TAX1_Amt,TSPL_SALE_RETURN_DETAIL.TOT_TAX2_Amt,TSPL_SALE_RETURN_DETAIL.TOT_TAX3_Amt,TSPL_SALE_RETURN_DETAIL.TOT_TAX4_Amt,TSPL_SALE_RETURN_DETAIL.TOT_TAX5_Amt,TSPL_SALE_RETURN_DETAIL.TOT_TAX6_Amt,TSPL_SALE_RETURN_DETAIL.TOT_TAX7_Amt,TSPL_SALE_RETURN_DETAIL.TOT_TAX8_Amt,TSPL_SALE_RETURN_DETAIL.TOT_TAX9_Amt,TSPL_SALE_RETURN_DETAIL.TOT_TAX10_Amt from TSPL_SALE_RETURN_DETAIL where TSPL_SALE_RETURN_DETAIL.Sale_Return_No='" + strCode + "' order by TSPL_SALE_RETURN_DETAIL.Sale_Return_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsSalesReturnDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsSalesReturnDetail = New clsSalesReturnDetail()
                obj.Sale_Return_Id = clsCommon.myCdbl(dr("Sale_Return_Id"))
                obj.Sale_Return_No = clsCommon.myCstr(dr("Sale_Return_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))

                obj.Invoice_Qty = clsCommon.myCdbl(dr("Invoice_Qty"))
                obj.Return_Qty = clsCommon.myCdbl(dr("Return_Qty"))
                obj.Actual_Return_Qty = clsCommon.myCdbl(dr("Actual_Return_Qty"))
                obj.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))

                obj.Unit_Cogs = clsCommon.myCdbl(dr("Unit_Cogs"))

                obj.Price_Date = clsCommon.myCDate(dr("Price_Date"))
                obj.Invoice_Qty = clsCommon.myCdbl(dr("Invoice_Qty"))
                obj.Return_Qty = clsCommon.myCdbl(dr("Return_Qty"))
                obj.Actual_Return_Qty = clsCommon.myCdbl(dr("Actual_Return_Qty"))
                obj.Balance_Qty = clsCommon.myCdbl(dr("Balance_Qty"))
                obj.Unit_code = clsCommon.myCstr(dr("Unit_code"))
                obj.Location = clsCommon.myCstr(dr("Location"))
                obj.Price_code = clsCommon.myCstr(dr("Price_code"))
                obj.Scheme_Applicable = clsCommon.myCstr(dr("Scheme_Applicable"))
                obj.Scheme_Code_Qty = clsCommon.myCdbl(dr("Scheme_Code_Qty"))
                obj.Scheme_Item = clsCommon.myCstr(dr("Scheme_Item"))
                obj.Promo_Scheme_Applicable = clsCommon.myCstr(dr("Promo_Scheme_Applicable"))
                obj.Promo_Scheme_Code = clsCommon.myCstr(dr("Promo_Scheme_Code"))
                obj.Promo_Scheme_Item = clsCommon.myCstr(dr("Promo_Scheme_Item"))
                obj.Scheme_Disc_Applicable = clsCommon.myCstr(dr("Scheme_Disc_Applicable"))
                obj.Scheme_Code_Cash = clsCommon.myCstr(dr("Scheme_Code_Cash"))
                obj.Sampling_Item = clsCommon.myCstr(dr("Sampling_Item"))
                obj.Sale_Return_Id = clsCommon.myCstr(dr("Sale_Return_Id"))
                obj.MRP_Amt = clsCommon.myCdbl(dr("MRP_Amt"))
                obj.Basic_Rate = clsCommon.myCstr(dr("Basic_Rate"))
                obj.Item_Assessable_Rate = clsCommon.myCdbl(dr("Item_Assessable_Rate"))
                obj.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                obj.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                obj.TAX1 = clsCommon.myCstr(dr("TAX1"))
                obj.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                obj.TAX1_Assessable_Amt = clsCommon.myCdbl(dr("TAX1_Assessable_Amt"))
                obj.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                obj.TAX2 = clsCommon.myCstr(dr("TAX2"))
                obj.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                obj.TAX2_Assessable_Amt = clsCommon.myCdbl(dr("TAX2_Assessable_Amt"))
                obj.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                obj.TAX3 = clsCommon.myCstr(dr("TAX3"))
                obj.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                obj.TAX3_Assessable_Amt = clsCommon.myCdbl(dr("TAX3_Assessable_Amt"))
                obj.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                obj.TAX4 = clsCommon.myCstr(dr("TAX4"))
                obj.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                obj.TAX4_Assessable_Amt = clsCommon.myCdbl(dr("TAX4_Assessable_Amt"))
                obj.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                obj.TAX5 = clsCommon.myCstr(dr("TAX5"))
                obj.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                obj.TAX5_Assessable_Amt = clsCommon.myCdbl(dr("TAX5_Assessable_Amt"))
                obj.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                obj.TAX6 = clsCommon.myCstr(dr("TAX6"))
                obj.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                obj.TAX6_Assessable_Amt = clsCommon.myCdbl(dr("TAX6_Assessable_Amt"))
                obj.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                obj.TAX7 = clsCommon.myCstr(dr("TAX7"))
                obj.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                obj.TAX7_Assessable_Amt = clsCommon.myCdbl(dr("TAX7_Assessable_Amt"))
                obj.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                obj.TAX8 = clsCommon.myCstr(dr("TAX8"))
                obj.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                obj.TAX8_Assessable_Amt = clsCommon.myCdbl(dr("TAX8_Assessable_Amt"))
                obj.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                obj.TAX9 = clsCommon.myCstr(dr("TAX9"))
                obj.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                obj.TAX9_Assessable_Amt = clsCommon.myCdbl(dr("TAX9_Assessable_Amt"))
                obj.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                obj.TAX10 = clsCommon.myCstr(dr("TAX10"))
                obj.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                obj.TAX10_Assessable_Amt = clsCommon.myCdbl(dr("TAX10_Assessable_Amt"))
                obj.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))

                obj.TOT_TAX1_Amt = clsCommon.myCdbl(dr("TOT_TAX1_Amt"))
                obj.TOT_TAX2_Amt = clsCommon.myCdbl(dr("TOT_TAX2_Amt"))
                obj.TOT_TAX3_Amt = clsCommon.myCdbl(dr("TOT_TAX3_Amt"))
                obj.TOT_TAX4_Amt = clsCommon.myCdbl(dr("TOT_TAX4_Amt"))
                obj.TOT_TAX5_Amt = clsCommon.myCdbl(dr("TOT_TAX5_Amt"))
                obj.TOT_TAX6_Amt = clsCommon.myCdbl(dr("TOT_TAX6_Amt"))
                obj.TOT_TAX7_Amt = clsCommon.myCdbl(dr("TOT_TAX7_Amt"))
                obj.TOT_TAX8_Amt = clsCommon.myCdbl(dr("TOT_TAX8_Amt"))
                obj.TOT_TAX9_Amt = clsCommon.myCdbl(dr("TOT_TAX9_Amt"))
                obj.TOT_TAX10_Amt = clsCommon.myCdbl(dr("TOT_TAX10_Amt"))


                obj.Item_Tax = clsCommon.myCstr(dr("Item_Tax"))
                obj.Total_Assessable_Amt = clsCommon.myCdbl(dr("Total_Assessable_Amt"))
                obj.Total_MRP_Amt = clsCommon.myCdbl(dr("Total_MRP_Amt"))
                obj.Total_Basic_Amt = clsCommon.myCdbl(dr("Total_Basic_Amt"))
                obj.Total_Disc_Amt = clsCommon.myCdbl(dr("Total_Disc_Amt"))
                obj.Total_net_Amt = clsCommon.myCdbl(dr("Total_net_Amt"))
                obj.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                obj.Total_Item_Amt = clsCommon.myCdbl(dr("Total_Item_Amt"))
                obj.Empty_Value = clsCommon.myCdbl(dr("Empty_Value"))
                obj.TPT = clsCommon.myCdbl(dr("TPT"))
                obj.Total_TPT = clsCommon.myCdbl(dr("Total_TPT"))
                obj.Empty_Value_Shell = clsCommon.myCdbl(dr("Empty_Value_Shell"))
                obj.Empty_Value_Bottle = clsCommon.myCdbl(dr("Empty_Value_Bottle"))
                obj.Cust_Discount = clsCommon.myCdbl(dr("Cust_Discount"))
                obj.Total_Cust_Discount = clsCommon.myCdbl(dr("Total_Cust_Discount"))
                obj.Price_Amount1 = clsCommon.myCdbl(dr("price_amount1"))
                obj.Price_Amount2 = clsCommon.myCdbl(dr("price_amount2"))
                obj.Price_Amount3 = clsCommon.myCdbl(dr("price_amount3"))
                obj.Price_Amount4 = clsCommon.myCdbl(dr("price_amount4"))
                obj.Price_Amount5 = clsCommon.myCdbl(dr("price_amount5"))
                obj.Price_Amount6 = clsCommon.myCdbl(dr("price_amount6"))
                obj.Price_Amount7 = clsCommon.myCdbl(dr("price_amount7"))
                obj.Price_Amount8 = clsCommon.myCdbl(dr("price_amount8"))
                obj.Price_Amount9 = clsCommon.myCdbl(dr("price_amount9"))
                obj.Price_Amount10 = clsCommon.myCdbl(dr("price_amount10"))
                obj.Main_item = clsCommon.myCstr(dr("Main_Item"))
                obj.Discount_Code = clsCommon.myCstr(dr("Discount_Code"))
                obj.Cust_Item_Discount_NoTax = clsCommon.myCdbl(dr("Cust_Item_Discount_NoTax"))

                obj.From_Scheme_Code = clsCommon.myCstr(dr("From_Scheme_Code"))
                obj.Target_Discount_Amt = clsCommon.myCdbl(dr("Target_Discount_Amt"))

                obj.Leak_Qty = clsCommon.myCdbl(dr("Leak_Qty"))
                obj.Burst_Qty = clsCommon.myCdbl(dr("Burst_Qty"))
                obj.Short_Qty = clsCommon.myCdbl(dr("Short_Qty"))

                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
