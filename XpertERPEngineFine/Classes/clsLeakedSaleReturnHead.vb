Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class clsLeakedSaleReturnHead
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime? = Nothing
    Public Customer_Code As String = Nothing
    Public Customer_Name As String = Nothing  'Not a table field
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Route_No As String = Nothing
    Public Route_Desc As String = Nothing
    Public Remarks As String = Nothing
    Public Tax_Group As String = Nothing
    Public TaxGroupName As String = Nothing 'Not a table field
    Public Location_Code As String = Nothing
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
    Public Posting_Date As DateTime? = Nothing
    Public Arr As List(Of clsLeakedSaleReturnDetail) = Nothing
    Public ArrMainItem As List(Of clsLeakedSaleReturnMainItem) = Nothing
#End Region
    Public Shared Function SaveData(ByVal obj As clsLeakedSaleReturnHead, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsLeakedSaleReturnHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            'If Not isNewEntry Then
            '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_LEAKED_SALE_RETURN_HEAD", "Document_Code", "TSPL_LEAKED_SALE_RETURN_DETAIL", "Document_Code", "TSPL_LEAKED_SALE_RETURN_Main_Item", "Document_Code", trans)
            'End If
            Dim qry As String = "delete from TSPL_LEAKED_SALE_RETURN_Main_Item where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_LEAKED_SALE_RETURN_DETAIL where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Route_No", clsCommon.myCstr(obj.Route_No))
            clsCommon.AddColumnsForChange(coll, "Location_Code", clsCommon.myCstr(obj.Location_Code))
            clsCommon.AddColumnsForChange(coll, "Customer_Code", clsCommon.myCstr(obj.Customer_Code))
            clsCommon.AddColumnsForChange(coll, "Remarks", clsCommon.myCstr(obj.Remarks))
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
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmLeakedSaleReturn, "", obj.Location_Code)
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAKED_SALE_RETURN_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAKED_SALE_RETURN_HEAD", OMInsertOrUpdate.Update, "TSPL_LEAKED_SALE_RETURN_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsLeakedSaleReturnDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsLeakedSaleReturnMainItem.SaveData(obj.Document_Code, obj.ArrMainItem, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_LEAKED_SALE_RETURN_HEAD", "Document_Code", "TSPL_LEAKED_SALE_RETURN_DETAIL", "Document_Code", "TSPL_LEAKED_SALE_RETURN_Main_Item", "Document_Code", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLeakedSaleReturnHead
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLeakedSaleReturnHead
        Dim obj As clsLeakedSaleReturnHead = Nothing
        Try
            Dim qry As String = "select * from TSPL_LEAKED_SALE_RETURN_HEAD where 2=2 "
            Dim whrCls As String = ""
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_LEAKED_SALE_RETURN_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_LEAKED_SALE_RETURN_HEAD WHERE 1=1 " + whrCls + ") "
                Case NavigatorType.Last
                    qry += " and TSPL_LEAKED_SALE_RETURN_HEAD.Document_Code = (select Max(Document_Code) from TSPL_LEAKED_SALE_RETURN_HEAD WHERE 1=1 " + whrCls + ")"
                Case NavigatorType.Current
                    qry += " and TSPL_LEAKED_SALE_RETURN_HEAD.Document_Code = '" + strCode + "' "
                Case NavigatorType.Next
                    qry += " and TSPL_LEAKED_SALE_RETURN_HEAD.Document_Code = (select Min(Document_Code) from TSPL_LEAKED_SALE_RETURN_HEAD where Document_Code>'" + strCode + "' " + whrCls + ")"
                Case NavigatorType.Previous
                    qry += " and TSPL_LEAKED_SALE_RETURN_HEAD.Document_Code = (select Max(Document_Code) from TSPL_LEAKED_SALE_RETURN_HEAD where Document_Code<'" + strCode + "' " + whrCls + ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsLeakedSaleReturnHead()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
                obj.Customer_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + obj.Customer_Code + "'", trans))
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                obj.Route_No = clsCommon.myCstr(dt.Rows(0)("Route_No"))
                obj.Route_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + obj.Route_No + "'", trans))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
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
                qry = "select * from tspl_leaked_sale_return_detail where Document_Code='" + obj.Document_Code + "'"
                dt = New DataTable
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.Arr = New List(Of clsLeakedSaleReturnDetail)
                    For Each dr As DataRow In dt.Rows
                        Dim objTr As New clsLeakedSaleReturnDetail
                        objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                        objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objTr.Item_Desc = clsCommon.myCstr(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + objTr.Item_Code + "'", trans)))
                        objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                        objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                        objTr.Price_Item = clsCommon.myCstr(dr("Price_Item"))
                        objTr.Price_Item_Desc = clsCommon.myCstr(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + objTr.Price_Item + "'", trans)))
                        objTr.Price_Item_UOM = clsCommon.myCstr(dr("Price_Item_UOM"))
                        objTr.SNF_Per = clsCommon.myCdbl(dr("SNF_Per"))
                        objTr.FAt_Per = clsCommon.myCdbl(dr("FAt_Per"))
                        objTr.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                        objTr.FAT_KG = clsCommon.myCdbl(dr("FAT_KG"))
                        objTr.Item_Rate = clsCommon.myCdbl(dr("Item_Rate"))
                        objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                        objTr.Location = clsCommon.myCstr(dr("Location"))
                        objTr.TaxGroup = clsCommon.myCstr(dr("TaxGroup"))
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
                        objTr.Disc_Amt = clsCommon.myCdbl(dr("Disc_Amt"))
                        objTr.Amt_Less_Discount = clsCommon.myCdbl(dr("Amt_Less_Discount"))
                        objTr.Total_Tax_Amt = clsCommon.myCdbl(dr("Total_Tax_Amt"))
                        objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                        objTr.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                        obj.Arr.Add(objTr)
                    Next
                End If
                qry = "select * from TSPL_LEAKED_SALE_RETURN_Main_Item where Document_Code='" + obj.Document_Code + "'"
                dt = New DataTable
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.ArrMainItem = New List(Of clsLeakedSaleReturnMainItem)
                    For Each dr As DataRow In dt.Rows
                        Dim objMTr As New clsLeakedSaleReturnMainItem()
                        objMTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                        objMTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objMTr.Item_Desc = clsCommon.myCstr(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from TSPL_ITEM_MASTER where Item_Code='" + objMTr.Item_Code + "'", trans)))
                        objMTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                        objMTr.Qty = clsCommon.myCdbl(dr("Qty"))
                        obj.ArrMainItem.Add(objMTr)
                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = ""
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsLeakedSaleReturnHead = clsLeakedSaleReturnHead.GetData(strCode, NavigatorType.Current, trans)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                If (obj.Status = 1) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ChangeInventroyMovemnet, clsFixedParameterCode.ChangeInventroyMovemnet, trans)) = 0 Then
                    UpdateInventoryMovement(obj, trans, False)
                End If
                qry = "Update TSPL_LEAKED_SALE_RETURN_Head set  Status=1, Posting_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "',Modify_By='" + objCommonVar.CurrentUserCode + "'"
                qry += " where Document_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_LEAKED_SALE_RETURN_HEAD", "Document_Code", "TSPL_LEAKED_SALE_RETURN_DETAIL", "Document_Code", "TSPL_LEAKED_SALE_RETURN_Main_Item", "Document_Code", trans)

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function UpdateInventoryMovement(ByVal obj As clsLeakedSaleReturnHead, ByVal trans As SqlTransaction, Optional ByVal UpdateInventory As Boolean = False) As Boolean
        Dim TransType_Str As String = ""
        Try
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            If UpdateInventory = True Then
                clsDBFuncationality.ExecuteNonQuery("update tspl_batch_item set Against_Inv_Movement_Trans_Id=null where Document_Code='" & obj.Document_Code & "'", trans)
                clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" & obj.Document_Code & "'", trans)
            End If
            Dim strRgpNo As String = Nothing
            Dim intCounter As Integer = 0
            For Each objTr As clsLeakedSaleReturnDetail In obj.Arr
                intCounter = intCounter + 1
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
                End If
                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                objInventoryMovemnt.InOut = "I"
                objInventoryMovemnt.Location_Code = objTr.Location
                objInventoryMovemnt.Cust_Code = obj.Customer_Code
                objInventoryMovemnt.Cust_Name = obj.Customer_Name
                objInventoryMovemnt.Item_Code = objTr.Item_Code
                objInventoryMovemnt.Item_Desc = objTr.Item_Desc
                objInventoryMovemnt.Qty = objTr.Qty
                objInventoryMovemnt.UOM = objTr.Unit_Code
                objInventoryMovemnt.Basic_Cost = objTr.Item_Rate
                objInventoryMovemnt.MRP = objTr.Item_Rate
                objInventoryMovemnt.Add_Cost = objTr.Total_Tax_Amt
                objInventoryMovemnt.Net_Cost = objTr.Amt_Less_Discount
                objInventoryMovemnt.FAT_Per = objTr.FAt_Per
                objInventoryMovemnt.SNF_Per = objTr.SNF_Per
                objInventoryMovemnt.FAT_KG = objTr.FAT_KG
                objInventoryMovemnt.SNF_KG = objTr.SNF_KG
                objInventoryMovemnt.Is_Scheme_Item = "N"
                Dim qry As String = ""
                If clsCommon.myLen(objTr.Price_Code) > 0 Then
                    Dim arr As New clsFatSnfRateCalculator
                    If objCommonVar.PricePlan = 6 OrElse objCommonVar.PricePlan = 7 Then
                        qry = "select * from TSPL_MILK_PRICE_MASTER where Price_Code in (select Price_Chart_Code from TSPL_PRICE_CHART_PLANNING where Planning_Code='" & objTr.Price_Code & "')"
                    Else
                        qry = "select * from TSPL_MILK_PRICE_MASTER where Price_Code=" _
                                      & " (select Distinct Price_Code from tspl_Fat_SNf_Uploader_Master where code='" & objTr.Price_Code & "')"
                    End If

                    Dim dtMilkPrice As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If objCommonVar.ApplyTransFATSNFRateForCalculateFATSNFRate Then
                        arr = clsFatSnfRateCalculator.CalculateFATSNFRatefromTransactionPer(objTr.Qty, (objTr.Amt_Less_Discount), objTr.FAt_Per, objTr.SNF_Per, clsCommon.myCDecimal(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCDecimal(dtMilkPrice.Rows(0).Item("Snf_Ratio")))
                    ElseIf objCommonVar.ApplyStdFATSNFRate Then
                        arr = clsFatSnfRateCalculator.CalculateStdFATSNFRate(objTr.Qty, clsCommon.myCDecimal(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCDecimal(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCDecimal(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCDecimal(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCDecimal(dtMilkPrice.Rows(0).Item("Milk_Rate")), objTr.FAt_Per, objTr.SNF_Per)
                    Else

                        If clsCommon.myCDecimal(dtMilkPrice.Rows(0).Item("Fat_Pers")) = objTr.FAt_Per And clsCommon.myCDecimal(dtMilkPrice.Rows(0).Item("Snf_Pers")) = objTr.SNF_Per Then
                            arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(objTr.Qty, clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Snf_Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")))
                        Else
                            Try
                                arr = clsFatSnfRateCalculator.CalculateIn(objTr.Qty, clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("SNF_Pers")), objTr.FAt_Per, objTr.SNF_Per, clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Milk_Rate")), objTr.Item_Rate)
                            Catch ex As Exception
                                If ex.Message.Contains("Same equation repeated") Then
                                    arr = clsFatSnfRateCalculator.CalculateInonSamePercentage(objTr.Qty, clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Fat_Pers")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("SNF_Pers")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCstr(dtMilkPrice.Rows(0).Item("Snf_Ratio")), objTr.Item_Rate)
                                    If objInventoryMovemnt.FAT_KG <> 0 Then
                                        arr.fatR = arr.FatAmt / objInventoryMovemnt.FAT_KG
                                    End If
                                    If objInventoryMovemnt.SNF_KG <> 0 Then
                                        arr.snfR = arr.snfAmt / objInventoryMovemnt.SNF_KG
                                    End If
                                Else
                                    Throw New Exception(ex.Message)
                                End If
                            End Try
                        End If
                    End If
                    objInventoryMovemnt.Fat_Rate = arr.fatR
                    objInventoryMovemnt.Fat_Amt = arr.FatAmt
                    objInventoryMovemnt.SNF_Rate = arr.snfR
                    objInventoryMovemnt.SNF_Amt = arr.snfAmt
                    dtMilkPrice = Nothing
                    arr = Nothing
                End If
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                ArrInventoryMovement.Add(objInventoryMovemnt)
            Next
            clsInventoryMovementNew.SaveData("FS-SH", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
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
        Try
            Dim isSaved As Boolean = True
            Dim qry As String = ""
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            Dim obj As clsLeakedSaleReturnHead = clsLeakedSaleReturnHead.GetData(strCode, NavigatorType.Current, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_bank_book", "SOURCEDOC_NO", trans)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If
                clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_LEAKED_SALE_RETURN_Head", "Document_Code", "TSPL_LEAKED_SALE_RETURN_Detail", "Document_Code", "TSPL_LEAKED_SALE_RETURN_Main_Item", "Document_Code", trans)
                'qry = "Delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "'"
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_LEAKED_SALE_RETURN_Main_Item where Document_Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_LEAKED_SALE_RETURN_Detail where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_LEAKED_SALE_RETURN_Head where Document_Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If
            Dim Qry As String = "select Status from TSPL_LEAKED_SALE_RETURN_Head where Document_Code='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "Update TSPL_LEAKED_SALE_RETURN_Head set Status = 0,Posting_Date= null where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsLeakedSaleReturnDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Qty As Double = 0
    Public Price_Item As String = Nothing
    Public Price_Item_Desc As String = Nothing
    Public Price_Item_UOM As String = Nothing
    Public FAt_Per As Double = 0
    Public SNF_Per As Double = 0
    Public FAT_KG As Double = 0
    Public SNF_KG As Double = 0
    Public Location As String = Nothing '
    Public TaxGroup As String = Nothing '
    Public Item_Rate As Double = 0
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
    Public Disc_Amt As Double = 0
    Public Amt_Less_Discount As Double = 0
    Public Total_Tax_Amt As Double = 0
    Public Item_Net_Amt As Double = 0
    Public Price_Code As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsLeakedSaleReturnDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsLeakedSaleReturnDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Price_Item", obj.Price_Item)
                    clsCommon.AddColumnsForChange(coll, "Price_Item_UOM", obj.Price_Item_UOM)
                    clsCommon.AddColumnsForChange(coll, "FAt_Per", obj.FAt_Per)
                    clsCommon.AddColumnsForChange(coll, "SNF_Per", obj.SNF_Per)
                    clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
                    clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
                    clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Item_Rate)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                    clsCommon.AddColumnsForChange(coll, "Disc_Amt", obj.Disc_Amt)
                    clsCommon.AddColumnsForChange(coll, "Amt_Less_Discount", obj.Amt_Less_Discount)
                    clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                    clsCommon.AddColumnsForChange(coll, "TaxGroup", obj.TaxGroup)
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
                    clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAKED_SALE_RETURN_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsLeakedSaleReturnMainItem
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_Code As String = Nothing
    Public Qty As Double = 0
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsLeakedSaleReturnMainItem), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsLeakedSaleReturnMainItem In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAKED_SALE_RETURN_Main_Item", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
