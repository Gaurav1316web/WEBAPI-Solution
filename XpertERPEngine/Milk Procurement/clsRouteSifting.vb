Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI


Public Class clsRouteSifting
#Region "veriables"
    Public route_no As String = Nothing
    Public new_route_id As String
    Public desc As String
    Public customer_id As String
    Public Customer_name As String
    Public existing_route_id As String
    Public fromdate As String
    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public comp_code As String
    Public Route_group As String

#End Region
    '' This Code For Retriving Data from Tspl_Route_master

    Public Shared Function GetDatafromRoute_master(ByVal route_no As String) As DataTable
        Dim obj As clsRouteSifting = Nothing
        Dim qry As String = "select Distinct tspl_customer_master.cust_code as CustCode,tspl_customer_master.customer_name as Cust_Name,tspl_customer_master.route_no as Route_No ,CONVERT(varchar(12),GETDATE(),103) as Fromdate, (Select COUNT(*) from TSPL_VISI_MASTER Where Customer_Id=TSPL_CUSTOMER_MASTER.Cust_Code ) as Visi_Id, Route_Group from TSPL_CUSTOMER_MASTER join  TSPL_ROUTE_MASTER on TSPL_CUSTOMER_MASTER .Route_No =TSPL_ROUTE_MASTER .Route_No Left Outer Join TSPL_ROUTE_GROUP_MASTER on TSPL_ROUTE_MASTER.Route_No=TSPL_ROUTE_GROUP_MASTER.Route_Code LEFT OUTER JOIN TSPL_VISI_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_VISI_MASTER.Customer_Id where  tspl_customer_master.Route_No ='" & route_no & "' Order By Cust_Code "
        Return clsDBFuncationality.GetDataTable(qry)



    End Function
    '' For Save Data in Route Shifting Table
    Public Function SaveData(ByVal Arr As List(Of clsRouteSifting), ByVal route_no As String, ByVal ddl As RadDropDownList) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            'Dim qry As String = "delete from tspl_route_shifting where New_route_id='" & new_route_id & "' and Existing_Route_Id='" & route_no & "'    "
            'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            For Each obj As clsRouteSifting In Arr
                Dim qry As String = "delete from tspl_route_shifting where New_route_id='" & new_route_id & "' and Existing_Route_Id='" & route_no & "'  and Customer_Id='" + obj.customer_id + "'  "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "From_date", clsCommon.GetPrintDate(obj.fromdate, "dd/MM/yyyy"))
                'clsCommon.AddColumnsForChange(coll, "From_date", Cdate(obj.fromdate, "dd/MM/yyyy")
                clsCommon.AddColumnsForChange(coll, "Existing_Route_Id", obj.route_no)
                clsCommon.AddColumnsForChange(coll, "New_Route_Id", obj.new_route_id)
                clsCommon.AddColumnsForChange(coll, "Description", obj.desc)
                clsCommon.AddColumnsForChange(coll, "Customer_Id", obj.customer_id)
                clsCommon.AddColumnsForChange(coll, "Name", obj.Customer_name)
                clsCommon.AddColumnsForChange(coll, "Route_group", obj.Route_group)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                If ddl.SelectedIndex = 2 Then
                    clsDBFuncationality.ExecuteNonQuery("update tspl_customer_master set Status = 'Y' where Cust_Code = '" + Convert.ToString(obj.customer_id) + "'", trans)

                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_route_shifting", OMInsertOrUpdate.Insert, "", trans)
                    Dim PriceCode As String = ""
                    Dim PriceCodeNon As String = ""
                    Dim SalePersonCode As String = ""
                    Dim SalePersonCodeDesc As String = ""
                    Dim dr As DataTable = Nothing
                    Dim qryPrcCode As String = "select Price_Code,NonPrice_Code,Employee_Code,Employee_Name  from TSPL_ROUTE_MASTER where Route_No ='" & obj.new_route_id & "'  "
                    dr = clsDBFuncationality.GetDataTable(qryPrcCode, trans)

                    If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                        PriceCode = dr.Rows(0)(0).ToString()
                        PriceCodeNon = dr.Rows(0)(1).ToString()
                        SalePersonCode = dr.Rows(0)(2).ToString()
                        SalePersonCodeDesc = dr.Rows(0)(3).ToString()
                    End If


                    Dim coll1 As New Hashtable()
                    clsCommon.AddColumnsForChange(coll1, "Customer_name", obj.Customer_name)
                    clsCommon.AddColumnsForChange(coll1, "Route_No", obj.new_route_id)
                    clsCommon.AddColumnsForChange(coll1, "Route_Desc", obj.desc)
                    clsCommon.AddColumnsForChange(coll1, "Price_Code", PriceCode)
                    clsCommon.AddColumnsForChange(coll1, "price_CodeNon", PriceCodeNon)
                    clsCommon.AddColumnsForChange(coll1, "Salesman_Code", SalePersonCode)
                    clsCommon.AddColumnsForChange(coll1, "Salesman_Desc", SalePersonCodeDesc)
                    clsCommon.AddColumnsForChange(coll1, "Route_group", obj.Route_group)
                    'Dim qry1 As String = "update tspl_customer_master set Customer_name='" & obj.Customer_name & "',Route_No ='" & obj.new_route_id & "',Route_Desc ='" & obj.desc & "',Price_Code='" & PriceCode & "',price_CodeNon='" & PriceCodeNon & "', Salesman_Code='" & SalePersonCode & "', Salesman_Desc='" & SalePersonCodeDesc & "', Route_Group='" & obj.Route_group & "' where  cust_Code='" & obj.customer_id & "'"
                    clsCommonFunctionality.UpdateDataTable(coll1, "tspl_customer_master", OMInsertOrUpdate.Update, "cust_Code='" + obj.customer_id + "'", trans)
                    'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)
                End If


                ' 'Dim qry1 As String = ""
                ' 'If ddl.SelectedIndex = 2 Then
                ' '    qry1 = "update tspl_customer_master set Customer_name='" & obj.Customer_name & "',Route_No ='" & obj.new_route_id & "',Route_Desc ='" & obj.desc & "',Price_Code='" & PriceCode & "',price_CodeNon='" & PriceCodeNon & "', Salesman_Code='" & SalePersonCode & "', Salesman_Desc='" & SalePersonCodeDesc & "' where  cust_Code='" & obj.customer_id & "'"
                ' 'Else
                ' 'qry1 = "update tspl_customer_master set Customer_name='" & obj.Customer_name & "',Route_No ='" & obj.new_route_id & "',Route_Desc ='" & obj.desc & "',Price_Code='" & PriceCode & "',price_CodeNon='" & PriceCodeNon & "', Salesman_Code='" & SalePersonCode & "', Salesman_Desc='" & SalePersonCodeDesc & "', Route_Group='" & obj.Route_group & "' where  cust_Code='" & obj.customer_id & "'"
                ' ' End If
                ' 'isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)

            Next
            If isSaved Then
                trans.Commit()
            End If
        Catch Err As Exception
            trans.Rollback()
            Throw New Exception(Err.Message)
        End Try
        Return isSaved
    End Function


End Class
