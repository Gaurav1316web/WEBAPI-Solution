Imports common
Imports System.Data.SqlClient

Public Class clsCustomeritemDetails
#Region "Variables"
    Public Customer_Code As String = Nothing
    Public Customer_Desc As String = Nothing
    Public item_code As String = Nothing
    Public item_desc As String = Nothing
    Public UOM As String = Nothing
    Public item_rate As Double = Nothing
    Public Item_MRP As Double = Nothing
    Public customer_item_no As String = Nothing
    Public comp_code As String = Nothing
    Public Start_Date As Date? = Nothing
    Public End_Date As Date? = Nothing
    Public version As Integer
    Public Discount_Per As Double = 0
    Public Discount_Per_Level2 As Double = 0
    Public Min_Rate As Double = 0
    Public Approval_Item_Rate As Double = 0
#End Region
    Public Function SaveData(ByVal isApplyOnGroup As Boolean, ByVal CustomerCode As String, ByVal CustomerDesc As String, ByVal company As String, ByVal Arr As List(Of clsCustomeritemDetails)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Dim isSaved As Boolean = True
        Try
            Dim qry As String = ""
            Dim ArrCustomer As New Dictionary(Of String, String)
            If isApplyOnGroup Then
                qry = "select Cust_Code,Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Group_Code in (select Cust_Group_Code from TSPL_CUSTOMER_MASTER as  Inn where Inn.Cust_Code='" + CustomerCode + "')"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        ArrCustomer.Add(clsCommon.myCstr(dr("Cust_Code")), clsCommon.myCstr(dr("Customer_Name")))
                    Next
                End If
            Else
                ArrCustomer.Add(CustomerCode, CustomerDesc)
            End If


            Dim pair As KeyValuePair(Of String, String)
            For Each pair In ArrCustomer
                clsCustomerItemDetailHistory.SaveDataHistory(CustomerCode, Arr, trans)
                qry = "delete from TSPL_CUSTOMER_ITEM_DETAIL where Customer_Code='" + pair.Key + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Dim strDocNo As String = ""
                For Each obj As clsCustomeritemDetails In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Customer_Code", pair.Key)
                    clsCommon.AddColumnsForChange(coll, "Customer_Desc", pair.Value)
                    clsCommon.AddColumnsForChange(coll, "item_no", obj.item_code)
                    clsCommon.AddColumnsForChange(coll, "item_desc", obj.item_desc)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Discount_Per", obj.Discount_Per)
                    clsCommon.AddColumnsForChange(coll, "Discount_Per_Level2", obj.Discount_Per_Level2)
                    clsCommon.AddColumnsForChange(coll, "item_rate", obj.item_rate)
                    clsCommon.AddColumnsForChange(coll, "Min_Rate", obj.Min_Rate)
                    clsCommon.AddColumnsForChange(coll, "Approval_Item_Rate", obj.Approval_Item_Rate)
                    clsCommon.AddColumnsForChange(coll, "Customer_item_no", obj.customer_item_no)
                    clsCommon.AddColumnsForChange(coll, "comp_code", company)
                    clsCommon.AddColumnsForChange(coll, "Item_MRP", obj.Item_MRP)

                    If clsCommon.myLen(obj.Start_Date) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd-MMM-yyyy"))
                    End If

                    If clsCommon.myLen(obj.End_Date) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd-MMM-yyyy"))
                    End If

                    clsCommon.AddColumnsForChange(coll, "Version", version)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ITEM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            Next



            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String) As List(Of clsCustomeritemDetails)
        Dim obj As clsCustomeritemDetails = Nothing
        Dim qry As String = "select item_no ,item_desc ,uom ,Item_Mrp, item_rate ,Customer_item_no, Start_Date, End_Date,Discount_Per,Approval_Item_Rate,Min_Rate, Discount_Per_Level2  from TSPL_CUSTOMER_ITEM_DETAIL WHERE Customer_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'dt = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of clsCustomeritemDetails)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As clsCustomeritemDetails
            For Each dr As DataRow In dt.Rows
                objTr = New clsCustomeritemDetails()
                objTr.item_code = clsCommon.myCstr(dr("item_no"))
                objTr.item_desc = clsCommon.myCstr(dr("item_desc"))

                objTr.UOM = clsCommon.myCstr(dr("uom"))
                objTr.Discount_Per = clsCommon.myCdbl(dr("Discount_Per"))
                objTr.Discount_Per_Level2 = clsCommon.myCdbl(dr("Discount_Per_Level2"))

                objTr.Item_MRP = clsCommon.myCdbl(dr("Item_MRP"))
                objTr.item_rate = clsCommon.myCdbl(dr("Item_rate"))
                objTr.Approval_Item_Rate = clsCommon.myCdbl(dr("Approval_Item_Rate"))
                objTr.Min_Rate = clsCommon.myCdbl(dr("Min_Rate"))
                objTr.customer_item_no = clsCommon.myCstr(dr("Customer_item_no"))
                If clsCommon.myLen(dr("Start_Date")) <= 0 Then
                    objTr.Start_Date = Nothing
                Else
                    objTr.Start_Date = clsCommon.GetPrintDate(dr("Start_Date"))
                End If

                If clsCommon.myLen(dr("End_Date")) <= 0 Then
                    objTr.End_Date = Nothing
                Else
                    objTr.End_Date = clsCommon.GetPrintDate(dr("End_Date"))
                End If
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function

    Public Shared Function DeleteData(ByVal strCustCode As String, ByVal IsFromApproval As Boolean) As Boolean
        Try
            If clsCommon.myLen(strCustCode) > 0 Then
                Dim qry As String
                If IsFromApproval Then
                    qry = "Update TSPL_CUSTOMER_ITEM_DETAIL SET Discount_Per_Level2 = 0 Where Customer_Code='" + strCustCode + "'"
                Else
                    qry = "Update TSPL_CUSTOMER_ITEM_DETAIL SET Discount_Per = 0 Where Customer_Code='" + strCustCode + "'"
                End If
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetItemRateAndDiscount(ByVal strCustomerCode As String, ByVal strICode As String, ByVal strUOM As String, ByVal TransDate As Date) As clsCustomeritemDetails
        Dim obj As clsCustomeritemDetails = Nothing

        Dim qry As String = "select  Approval_Item_Rate,Discount_Per,Min_Rate from TSPL_CUSTOMER_ITEM_DETAIL where Customer_Code='" + strCustomerCode + "' and item_no='" + strICode + "' and uom='" + strUOM + "' "
        qry += " and isnull( Start_Date,'" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "')<='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' and isnull(End_Date,'" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "')>='" + clsCommon.GetPrintDate(TransDate, "dd/MMM/yyyy") + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsCustomeritemDetails()
            obj.Approval_Item_Rate = clsCommon.myCdbl(dt.Rows(0)("Approval_Item_Rate"))
            obj.Discount_Per = clsCommon.myCdbl(dt.Rows(0)("Discount_Per"))
            obj.Min_Rate = clsCommon.myCdbl(dt.Rows(0)("Min_Rate"))
        End If
        Return obj
    End Function
End Class

Public Class clsCustomerItemDetailHistory
    Public Shared Function SaveDataHistory(ByVal VendorCode As String, ByVal Arr As List(Of clsCustomeritemDetails), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qryInsert As String = "select Customer_Code, Customer_Desc, item_no, Item_Mrp,item_desc, uom, item_rate, Customer_item_no, Comp_Code, Start_Date, End_Date, Version  from TSPL_CUSTOMER_ITEM_DETAIL where Customer_Code='" + VendorCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qryInsert, trans)
            If dt.Rows.Count <= 0 Then
                Return False
            Else
                For Each obj As clsCustomeritemDetails In Arr
                    For Each dr As DataRow In dt.Rows
                        If dr("item_no") = obj.item_code And (dr("item_rate") <> obj.item_rate Or dr("Start_Date") <> obj.Start_Date) Then
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Customer_Code", dr("Customer_Code"))
                            clsCommon.AddColumnsForChange(coll, "Customer_Desc", dr("Customer_Desc"))
                            clsCommon.AddColumnsForChange(coll, "item_no", dr("item_no"))
                            clsCommon.AddColumnsForChange(coll, "item_desc", dr("item_desc"))
                            clsCommon.AddColumnsForChange(coll, "UOM", dr("UOM"))
                            'clsCommon.AddColumnsForChange(coll, "MRP", dr("MRP"))
                            clsCommon.AddColumnsForChange(coll, "item_rate", dr("item_rate"))
                            clsCommon.AddColumnsForChange(coll, "Item_Mrp", dr("Item_Mrp"))
                            clsCommon.AddColumnsForChange(coll, "Customer_item_no", dr("Customer_item_no"))
                            clsCommon.AddColumnsForChange(coll, "comp_code", dr("comp_code"))

                            If dr("Start_Date") IsNot DBNull.Value Then
                                clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(dr("Start_Date"), "dd-MMM-yyyy"), True)
                                'Dim obj As New clsVendorItemDetail
                            End If


                            Dim Enddate As Date = clsCommon.myCDate(obj.Start_Date).AddDays(-1)
                            clsCommon.AddColumnsForChange(coll, "end_Date", clsCommon.GetPrintDate(Enddate, "dd-MMM-yyyy"), True)



                            clsCommon.AddColumnsForChange(coll, "History_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd-MMM-yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Version", dr("Version"))
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ITEM_DETAIL_Hist", OMInsertOrUpdate.Insert, "", trans)
                        End If

                    Next
                Next
            End If

            If isSaved Then
                'trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function



End Class













