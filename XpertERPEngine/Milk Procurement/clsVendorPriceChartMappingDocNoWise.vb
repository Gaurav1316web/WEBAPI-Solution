Imports common
Imports System.Data.SqlClient

Public Class clsVendorPriceChartMappingDocNoWise
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As Date?
    Public Posted As Integer = 0
    Public Arr As List(Of clsVendorPriceChartMappingDocNoWiseDetail) = Nothing
#End Region

    Public Shared Function SaveData(ByVal obj As clsVendorPriceChartMappingDocNoWise, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim isSaved As Boolean = True
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.VendorPriceMapDocwise, "", "")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Vendor_Price_Chart_mapping_DocWise_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Vendor_Price_Chart_mapping_DocWise_Master", OMInsertOrUpdate.Update, "Tspl_Vendor_Price_Chart_mapping_DocWise_Master.Document_No='" + obj.Document_No + "'", trans)
            End If
          
            clsVendorPriceChartMappingDocNoWiseDetail.SaveData(obj.Document_No, obj.Arr, trans)

            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsVendorPriceChartMappingDocNoWise
        Dim obj As clsVendorPriceChartMappingDocNoWise = Nothing
        Dim Arr As List(Of clsVendorPriceChartMappingDocNoWise) = Nothing
        Dim qry As String = "select * from Tspl_Vendor_Price_Chart_mapping_DocWise_Master where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and Tspl_Vendor_Price_Chart_mapping_DocWise_Master.Document_No = (select MIN(Document_No) from Tspl_Vendor_Price_Chart_mapping_DocWise_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and Tspl_Vendor_Price_Chart_mapping_DocWise_Master.Document_No = (select Max(Document_No) from Tspl_Vendor_Price_Chart_mapping_DocWise_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and Tspl_Vendor_Price_Chart_mapping_DocWise_Master.Document_No = (select top 1 Document_No from Tspl_Vendor_Price_Chart_mapping_DocWise_Master WHERE 1=1 " + whrclas + " and Document_No='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and Tspl_Vendor_Price_Chart_mapping_DocWise_Master.Document_No = (select Min(Document_No) from Tspl_Vendor_Price_Chart_mapping_DocWise_Master where Document_No>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and Tspl_Vendor_Price_Chart_mapping_DocWise_Master.Document_No = (select Max(Document_No) from Tspl_Vendor_Price_Chart_mapping_DocWise_Master where Document_No<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsVendorPriceChartMappingDocNoWise
            If clsCommon.myLen(dt.Rows(0)("Document_Date")) > 0 Then
                obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            End If        
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
            'obj.Arr = clsVendorPriceChartMappingDocNoWiseDetail.GetData(obj.Document_No, Nothing)
        End If
        Return obj
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim qry As String = "Update Tspl_Vendor_Price_Chart_mapping_DocWise_Master set Posted=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim qry As String = "delete from Tspl_Vendor_Price_Chart_mapping_DocWise_Detail where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from Tspl_Vendor_Price_Chart_mapping_DocWise_Master where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
End Class
Public Class clsVendorPriceChartMappingDocNoWiseDetail
#Region "Variables"
    Public Document_No As String = Nothing
    Public Pricecode As String = Nothing
    Public VendorCode As String = Nothing
    Public isDefault As Integer = 0
    Public Milk_Grade_Code As String = Nothing
    Public Status As Integer = 0
    Public SequenceNo As Integer = 0
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsVendorPriceChartMappingDocNoWiseDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from Tspl_Vendor_Price_Chart_mapping_DocWise_Detail where Document_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsVendorPriceChartMappingDocNoWiseDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Pricecode", obj.Pricecode)
                clsCommon.AddColumnsForChange(coll, "VendorCode", obj.VendorCode)
                clsCommon.AddColumnsForChange(coll, "isDefault", obj.isDefault)
                clsCommon.AddColumnsForChange(coll, "Milk_Grade_Code", obj.Milk_Grade_Code)
                clsCommon.AddColumnsForChange(coll, "SequenceNo", obj.SequenceNo)
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Vendor_Price_Chart_mapping_DocWise_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsVendorPriceChartMappingDocNoWiseDetail)
        Dim arr As List(Of clsVendorPriceChartMappingDocNoWiseDetail) = Nothing
        Dim qry As String
        qry = "select * from " & _
            "Tspl_Vendor_Price_Chart_mapping_DocWise_Detail where Tspl_Vendor_Price_Chart_mapping_DocWise_Detail.Document_No='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsVendorPriceChartMappingDocNoWiseDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsVendorPriceChartMappingDocNoWiseDetail = New clsVendorPriceChartMappingDocNoWiseDetail()
                obj.Document_No = strCode
                obj.Milk_Grade_code = clsCommon.myCstr(dr("Milk_Grade_code"))
                obj.Pricecode = clsCommon.myCstr(dr("Pricecode"))
                obj.VendorCode = clsCommon.myCstr(dr("VendorCode"))
                obj.isDefault = clsCommon.myCdbl(dr("isDefault"))
                obj.Status = clsCommon.myCdbl(dr("Status"))
                obj.SequenceNo = clsCommon.myCdbl(dr("SequenceNo"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class