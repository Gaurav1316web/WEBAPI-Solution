Imports common
Imports System.Data.SqlClient

Public Class clsVendorPriceChartMapping
#Region "Variables"
    Public Pricecode As String = Nothing
    Public VendorCode As String = Nothing
    Public isDefault As Integer = 0
    Public SequenceNo As Integer = 0
    Public Milk_Grade_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strPriceCode As String, ByVal arr As List(Of clsVendorPriceChartMapping), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim isSaved As Boolean = True
            Dim AllowMultiplePricewithMultipleVendor As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkPriceChartMultiplepriceToMultipleVendor, clsFixedParameterCode.AllowBulkPriceChartMultiplepriceToMultipleVendor, trans))

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                If AllowMultiplePricewithMultipleVendor = 0 Then
                    For Each obj As clsVendorPriceChartMapping In arr
                        qry = "select count(*) from tspl_Vendor_price_chart_mapping where priceCode='" & obj.Pricecode & "' and VendorCode='" & obj.VendorCode & "'"
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
                            If obj.isDefault = 1 Then
                                qry = "  update tspl_Vendor_price_chart_mapping set isDefault=0 where vendorCode='" & obj.VendorCode & "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "PriceCode", obj.Pricecode)
                            clsCommon.AddColumnsForChange(coll, "VendorCode", obj.VendorCode)
                            clsCommon.AddColumnsForChange(coll, "isDefault", obj.isDefault)
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_vendor_price_chart_Mapping", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            If obj.isDefault = 1 Then
                                qry = "  update tspl_Vendor_price_chart_mapping set isDefault=0 where vendorCode='" & obj.VendorCode & "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                qry = "  update tspl_Vendor_price_chart_mapping set isDefault=1 where vendorCode='" & obj.VendorCode & "' and PriceCode='" & obj.Pricecode & "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            Else
                                qry = "  update tspl_Vendor_price_chart_mapping set isDefault=" & obj.isDefault & " where vendorCode='" & obj.VendorCode & "' and pricecode='" & obj.Pricecode & "'"
                                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            End If
                        End If
                    Next
                Else
                    Dim strqry As String = Nothing
                    Dim intPriceType As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsPrice_GradeWise from TSPL_Bulk_Price_MASTER where Price_Code='" & strPriceCode & "'", trans))
                    If intPriceType = 0 Then
                        strqry = "Delete from tspl_Vendor_price_chart_mapping where PriceCode='" & strPriceCode & "' "
                    Else
                        strqry = "Delete from tspl_Vendor_price_chart_mapping where PriceCode='" & strPriceCode & "' and  Milk_Grade_Code='" & arr(0).Milk_Grade_Code & "'"
                    End If
                    clsDBFuncationality.getSingleValue(strqry, trans)

                    If (arr IsNot Nothing AndAlso arr.Count > 0) Then

                        For Each obj As clsVendorPriceChartMapping In arr
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Pricecode", obj.Pricecode)
                            clsCommon.AddColumnsForChange(coll, "VendorCode", obj.VendorCode)
                            clsCommon.AddColumnsForChange(coll, "isDefault", obj.isDefault)
                            clsCommon.AddColumnsForChange(coll, "Milk_Grade_Code", obj.Milk_Grade_Code)
                            clsCommon.AddColumnsForChange(coll, "SequenceNo", obj.SequenceNo)
                            isSaved = clsCommonFunctionality.UpdateDataTable(coll, "tspl_Vendor_price_chart_mapping", OMInsertOrUpdate.Insert, "", trans)
                        Next
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


Public Class clsVendorPriceChartMappingUDLHead
#Region "Variables"
    Public Pricecode As String = Nothing
    Public Milk_Grade_Code As String = Nothing
    Public arrVendor As ArrayList
#End Region

    Public Function SaveData(ByVal arr As List(Of clsVendorPriceChartMappingUDLHead)) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsVendorPriceChartMappingUDLHead In arr
                    qry = "select top 1 PriceCode from TSPL_VENDOR_PRICE_CHART_MAPPING where PriceCode='" + obj.Pricecode + "' and Milk_Grade_Code='" + obj.Milk_Grade_Code + "' and posted=1"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Throw New Exception("Some posted vendor mapping found for Price code :" + obj.Pricecode + " and Milk Grade Code:" + obj.Milk_Grade_Code)
                    End If
                    qry = "delete from Tspl_Vendor_Price_Chart_mapping where PriceCode='" + obj.Pricecode + "' and Milk_Grade_Code='" + obj.Milk_Grade_Code + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                    For Each StrVendor As String In obj.arrVendor
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "PriceCode", obj.Pricecode)
                        clsCommon.AddColumnsForChange(coll, "Milk_Grade_Code", obj.Milk_Grade_Code)
                        clsCommon.AddColumnsForChange(coll, "VendorCode", StrVendor)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_PRICE_CHART_MAPPING", OMInsertOrUpdate.Insert, "", tran)
                    Next
                Next
            End If
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function DeleteData(ByVal arr As List(Of clsVendorPriceChartMappingUDLHead)) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsVendorPriceChartMappingUDLHead In arr
                    qry = "select top 1 PriceCode from TSPL_VENDOR_PRICE_CHART_MAPPING where PriceCode='" + obj.Pricecode + "' and Milk_Grade_Code='" + obj.Milk_Grade_Code + "' and posted=1"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Throw New Exception("Some posted vendor mapping found for Price code :" + obj.Pricecode + " and Milk Grade Code:" + obj.Milk_Grade_Code)
                    End If
                    qry = "delete from Tspl_Vendor_Price_Chart_mapping where PriceCode='" + obj.Pricecode + "' and Milk_Grade_Code='" + obj.Milk_Grade_Code + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                Next
            End If
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function PostData(ByVal arr As List(Of clsVendorPriceChartMappingUDLHead)) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For Each obj As clsVendorPriceChartMappingUDLHead In arr
                    qry = "select top 1 PriceCode from TSPL_VENDOR_PRICE_CHART_MAPPING where PriceCode='" + obj.Pricecode + "' and Milk_Grade_Code='" + obj.Milk_Grade_Code + "' and posted=1"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Throw New Exception("Some posted vendor mapping found for Price code :" + obj.Pricecode + " and Milk Grade Code:" + obj.Milk_Grade_Code)
                    End If
                    qry = "Update Tspl_Vendor_Price_Chart_mapping set posted='1',Posted_By='" + objCommonVar.CurrentUserCode + "',Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt") + "'  where PriceCode='" + obj.Pricecode + "' and Milk_Grade_Code='" + obj.Milk_Grade_Code + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, tran)
                Next
            End If
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function AddMore(ByVal obj As clsVendorPriceChartMappingUDLHead) As Boolean
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            For Each StrVendor As String In obj.arrVendor
                qry = "select top 1 PriceCode from TSPL_VENDOR_PRICE_CHART_MAPPING where PriceCode='" + obj.Pricecode + "' and Milk_Grade_Code='" + obj.Milk_Grade_Code + "' and posted=1 and VendorCode='" + StrVendor + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Vendor mapping already found for Price code :" + obj.Pricecode + " and Milk Grade Code:" + obj.Milk_Grade_Code + " and Vendor:" + StrVendor)
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "PriceCode", obj.Pricecode)
                clsCommon.AddColumnsForChange(coll, "Milk_Grade_Code", obj.Milk_Grade_Code)
                clsCommon.AddColumnsForChange(coll, "VendorCode", StrVendor)
                clsCommon.AddColumnsForChange(coll, "posted", 1)
                clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(tran), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_PRICE_CHART_MAPPING", OMInsertOrUpdate.Insert, "", tran)
            Next
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

 
