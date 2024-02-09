Imports common
Imports System.Data.SqlClient
Public Class clsBulkSaleAcknowledgement
#Region "Variable"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public Bulk_Dispatch_Document As String = Nothing
    Public Qty As Decimal = 0
    Public FAT As Decimal = 0
    Public FAT_KG As Decimal = 0
    Public SNF As Decimal = 0
    Public SNF_KG As Decimal = 0
    Public FAT_Rate As Decimal = 0
    Public SNF_Rate As Decimal = 0
    Public Amount As Decimal = 0
    Public Diff_Amount As Decimal = 0
    Public Created_By As String = Nothing
    Public Created_Date As DateTime = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As DateTime = Nothing
    Public Posted_By As String = Nothing
    Public Posting_Date As DateTime = Nothing
    Public Status As Integer = 0
    Public Remarks As String = Nothing
    Public Dispatch_QTY As Decimal = 0
    Public Dispatch_FAT As Decimal = 0
    Public Dispatch_FATKG As Decimal = 0
    Public Dispatch_SNF As Decimal = 0
    Public Dispatch_SNFKG As Decimal = 0
    Public Dispatch_FATRate As Decimal = 0
    Public Dispatch_SNFRate As Decimal = 0
    Public Dispatch_Amount As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal obj As clsBulkSaleAcknowledgement, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As clsBulkSaleAcknowledgement, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Bulk_Dispatch_Document", obj.Bulk_Dispatch_Document)
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
            clsCommon.AddColumnsForChange(coll, "FAT", obj.FAT)
            clsCommon.AddColumnsForChange(coll, "FAT_KG", obj.FAT_KG)
            clsCommon.AddColumnsForChange(coll, "SNF", obj.SNF)
            clsCommon.AddColumnsForChange(coll, "SNF_KG", obj.SNF_KG)
            clsCommon.AddColumnsForChange(coll, "FAT_Rate", obj.FAT_Rate)
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", obj.SNF_Rate)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "Diff_Amount", obj.Diff_Amount)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCstr(DateTime.Now), clsDocType.BulkSaleAcknowledgement, "", objCommonVar.strCurrUserLocations)
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_SALE_ACKNOWLEDGEMENT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_SALE_ACKNOWLEDGEMENT", OMInsertOrUpdate.Update, "TSPL_BULK_SALE_ACKNOWLEDGEMENT.Document_No='" + obj.Document_No + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        Finally
            obj = Nothing
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = "delete from TSPL_BULK_SALE_ACKNOWLEDGEMENT where Document_No='" + strDocNo + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBulkSaleAcknowledgement
        Dim obj As clsBulkSaleAcknowledgement = Nothing
        Dim qry As String = "Select * from TSPL_BULK_SALE_ACKNOWLEDGEMENT where 1=1"
        Dim whrclas As String = " and Document_No='" + strCode + "'"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Document_No = (select MIN(Document_No) from TSPL_BULK_SALE_ACKNOWLEDGEMENT where 1=1  )"
            Case NavigatorType.Last
                qry += " And Document_No = (Select Max(Document_No) from TSPL_BULK_SALE_ACKNOWLEDGEMENT where 1=1 )"
            Case NavigatorType.Next
                qry += " And Document_No = (Select Min(Document_No) from TSPL_BULK_SALE_ACKNOWLEDGEMENT where TSPL_BULK_SALE_ACKNOWLEDGEMENT.Document_No>'" + clsCommon.myCstr(strCode) + "' )"
            Case NavigatorType.Previous
                qry += " and Document_No = (select Max(Document_No) from TSPL_BULK_SALE_ACKNOWLEDGEMENT where TSPL_BULK_SALE_ACKNOWLEDGEMENT.Document_No<'" + clsCommon.myCstr(strCode) + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_BULK_SALE_ACKNOWLEDGEMENT.Document_No = '" + clsCommon.myCstr(strCode) + "' "
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsBulkSaleAcknowledgement()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Bulk_Dispatch_Document = clsCommon.myCstr(dt.Rows(0)("Bulk_Dispatch_Document"))
            obj.Qty = clsCommon.myCDecimal(dt.Rows(0)("Qty"))
            obj.FAT = clsCommon.myCDecimal(dt.Rows(0)("FAT"))
            obj.FAT_KG = clsCommon.myCDecimal(dt.Rows(0)("FAT_KG"))
            obj.SNF = clsCommon.myCDecimal(dt.Rows(0)("SNF"))
            obj.SNF_KG = clsCommon.myCDecimal(dt.Rows(0)("SNF_KG"))
            obj.FAT_Rate = clsCommon.myCDecimal(dt.Rows(0)("FAT_Rate"))
            obj.SNF_Rate = clsCommon.myCDecimal(dt.Rows(0)("SNF_Rate"))
            obj.Amount = clsCommon.myCDecimal(dt.Rows(0)("Amount"))
            obj.Diff_Amount = clsCommon.myCDecimal(dt.Rows(0)("Diff_Amount"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = clsCommon.myCdbl(dt.Rows(0)("Status"))
        End If
        Return obj
    End Function
    Public Shared Function ReturnDispatchQry() As String
        Dim qry As String = "select TSPL_Dispatch_BulkSale.Document_No,TSPL_Dispatch_Detail_BulkSale.Qty,TSPL_Dispatch_Detail_BulkSale.FatPer,TSPL_Dispatch_Detail_BulkSale.Fat_KG,TSPL_Dispatch_Detail_BulkSale.SNFPer,TSPL_Dispatch_Detail_BulkSale.SNF_KG,TSPL_Dispatch_Detail_BulkSale.FatRate,TSPL_Dispatch_Detail_BulkSale.SNFRate,
                             TSPL_Dispatch_Detail_BulkSale.Amount from TSPL_Dispatch_Detail_BulkSale  
                             Left Outer Join TSPL_Dispatch_BulkSale On TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No  Where 1=1 "
        Return qry
    End Function
    Public Shared Function DispatchGetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBulkSaleAcknowledgement
        Dim obj As clsBulkSaleAcknowledgement = Nothing
        Dim qry As String = ReturnDispatchQry()
        Dim whrclas As String = " and TSPL_Dispatch_BulkSale.Document_No='" + clsCommon.myCstr(strCode) + "'"
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Dispatch_BulkSale.Document_No = (select MIN(TSPL_Dispatch_BulkSale.Document_No) from TSPL_Dispatch_BulkSale where 1=1  )"
            Case NavigatorType.Last
                qry += " And TSPL_Dispatch_BulkSale.Document_No = (Select Max(TSPL_Dispatch_BulkSale.Document_No) from TSPL_Dispatch_BulkSale where 1=1 )"
            Case NavigatorType.Next
                qry += " And TSPL_Dispatch_BulkSale.Document_No = (Select Min(TSPL_Dispatch_BulkSale.Document_No) from TSPL_Dispatch_BulkSale where TSPL_Dispatch_BulkSale.Document_No>'" + clsCommon.myCstr(strCode) + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_Dispatch_BulkSale.Document_No = (select Max(TSPL_Dispatch_BulkSale.Document_No) from TSPL_Dispatch_BulkSale where TSPL_Dispatch_BulkSale.Document_No<'" + clsCommon.myCstr(strCode) + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_Dispatch_BulkSale.Document_No = '" + clsCommon.myCstr(strCode) + "' "
        End Select
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt1.Rows.Count > 0 Then
            obj = New clsBulkSaleAcknowledgement()
            obj.Bulk_Dispatch_Document = clsCommon.myCstr(dt1.Rows(0)("Document_No"))
            obj.Dispatch_QTY = clsCommon.myCDecimal(dt1.Rows(0)("Qty"))
            obj.Dispatch_FAT = clsCommon.myCDecimal(dt1.Rows(0)("FatPer"))
            obj.Dispatch_FATKG = clsCommon.myCDecimal(dt1.Rows(0)("Fat_KG"))
            obj.Dispatch_SNF = clsCommon.myCDecimal(dt1.Rows(0)("SNFPer"))
            obj.Dispatch_SNFKG = clsCommon.myCDecimal(dt1.Rows(0)("SNF_KG"))
            obj.Dispatch_FATRate = clsCommon.myCDecimal(dt1.Rows(0)("FatRate"))
            obj.Dispatch_SNFRate = clsCommon.myCDecimal(dt1.Rows(0)("SNFRate"))
            obj.Dispatch_Amount = clsCommon.myCDecimal(dt1.Rows(0)("Amount"))
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Dispatch No not found to Post")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULK_SALE_ACKNOWLEDGEMENT", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(strDocNo) + "'", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
