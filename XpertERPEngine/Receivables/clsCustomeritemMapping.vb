Imports common
Imports System.Data.SqlClient

Public Class clsCustomeritemMapping
#Region "Variables"
    Public Customer_Code As String = Nothing
    Public Customer_Desc As String = Nothing
    Public item_code As String = Nothing
    Public item_desc As String = Nothing
    Public UOM As String = Nothing
    Public customer_item_no As String = Nothing
    Public Item_Part_No As String = Nothing
    Public Customer_Part_No As String = Nothing
    Public comp_code As String = Nothing

#End Region
    Public Function SaveData(ByVal isApplyOnGroup As Boolean, ByVal CustomerCode As String, ByVal CustomerDesc As String, ByVal company As String, ByVal Arr As List(Of clsCustomeritemMapping)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Dim isSaved As Boolean = True
        Try
            Dim qry As String = ""
            Dim ArrCustomer As New Dictionary(Of String, String)

            ArrCustomer.Add(CustomerCode, CustomerDesc)



            Dim pair As KeyValuePair(Of String, String)
            For Each pair In ArrCustomer

                'qry = "delete from TSPL_CUSTOMER_ITEM_MAPPING where Customer_Code in ('" + pair.Key + "')"
                'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Dim strDocNo As String = ""
                For Each obj As clsCustomeritemMapping In Arr
                    Dim coll As New Hashtable()
                    qry = "delete from TSPL_CUSTOMER_ITEM_MAPPING where Customer_Code in ('" + pair.Key + "') and item_no='" & obj.item_code & "'"
                    isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    clsCommon.AddColumnsForChange(coll, "Customer_Code", pair.Key)
                    clsCommon.AddColumnsForChange(coll, "Customer_Desc", pair.Value)
                    clsCommon.AddColumnsForChange(coll, "item_no", obj.item_code)
                    clsCommon.AddColumnsForChange(coll, "item_desc", obj.item_desc)
                    clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                    clsCommon.AddColumnsForChange(coll, "Customer_item_no", obj.customer_item_no)
                    clsCommon.AddColumnsForChange(coll, "Item_Part_No", obj.Item_Part_No)
                    clsCommon.AddColumnsForChange(coll, "Customer_Part_No", obj.Customer_Part_No)
                    clsCommon.AddColumnsForChange(coll, "comp_code", company)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_ITEM_MAPPING", OMInsertOrUpdate.Insert, "", trans)
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


    Public Shared Function GetData(ByVal strCode As String) As List(Of clsCustomeritemMapping)
        Dim obj As clsCustomeritemMapping = Nothing
        Dim qry As String = "select item_no ,TSPL_ITEM_MASTER.item_desc ,TSPL_ITEM_MASTER.Unit_Code as uom ,Customer_item_no,Item_Part_No,Customer_Part_No from TSPL_CUSTOMER_ITEM_MAPPING left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code  =TSPL_CUSTOMER_ITEM_MAPPING.item_no WHERE TSPL_CUSTOMER_ITEM_MAPPING.Customer_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'dt = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As New List(Of clsCustomeritemMapping)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As clsCustomeritemMapping
            For Each dr As DataRow In dt.Rows
                objTr = New clsCustomeritemMapping()
                objTr.item_code = clsCommon.myCstr(dr("item_no"))
                objTr.item_desc = clsCommon.myCstr(dr("item_desc"))
                objTr.UOM = clsCommon.myCstr(dr("uom"))
                objTr.customer_item_no = clsCommon.myCstr(dr("Customer_item_no"))
                objTr.Item_Part_No = clsCommon.myCstr(dr("Item_Part_No"))
                objTr.Customer_Part_No = clsCommon.myCstr(dr("Customer_Part_No"))
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
                    qry = "Update TSPL_CUSTOMER_ITEM_MAPPING SET Discount_Per_Level2 = 0 Where Customer_Code='" + strCustCode + "'"
                Else
                    qry = "Update TSPL_CUSTOMER_ITEM_MAPPING SET Discount_Per = 0 Where Customer_Code='" + strCustCode + "'"
                End If
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


End Class














