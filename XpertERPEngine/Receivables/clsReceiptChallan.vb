Imports common
Imports System.Data.SqlClient
Public Class clsReceiptChallan

#Region "variables"
    Public SALE_INVOICE_NO As String = Nothing    
    Public VEHICLE_IN As String = Nothing
    Public RECEIPT_IN As String = Nothing
    Public REMARKS As String = Nothing
    Public COMMENTS As String = Nothing
    Public TRANSFER_HO As String = Nothing
    Public GRNo As String = Nothing
    Public GRDate As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal ArrVisi As List(Of clsReceiptChallan)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (ArrVisi IsNot Nothing AndAlso ArrVisi.Count > 0) Then
                For Each obj As clsReceiptChallan In ArrVisi
                    Dim coll As New Hashtable()
                    Dim Qry As String = "Delete from TSPL_RECEIPT_CHALLAN where SALE_INVOICE_NO='" & obj.SALE_INVOICE_NO & "' "
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    clsCommon.AddColumnsForChange(coll, "SALE_INVOICE_NO", obj.SALE_INVOICE_NO)
                    clsCommon.AddColumnsForChange(coll, "VEHICLE_IN", obj.VEHICLE_IN)
                    clsCommon.AddColumnsForChange(coll, "RECEIPT_IN", obj.RECEIPT_IN)
                    clsCommon.AddColumnsForChange(coll, "TRANSFER_HO", obj.TRANSFER_HO)
                    clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS, True)
                    clsCommon.AddColumnsForChange(coll, "COMMENTS", obj.COMMENTS, True)
                    '' Anubhooti 30-Sep-2014 BM00000003711
                    clsCommon.AddColumnsForChange(coll, "GRNo", obj.GRNo, True)
                    If clsCommon.myLen(obj.GRDate) > 0 Then
                        clsCommon.AddColumnsForChange(coll, "GRDate", clsCommon.GetPrintDate(obj.GRDate, "dd/MMM/yyyy"), True)
                        'Else
                        '    clsCommon.AddColumnsForChange(coll, "GRDate", "")
                    End If

                    ''
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RECEIPT_CHALLAN", OMInsertOrUpdate.Insert, "", trans)

                    '-------------------------Updates Data In TSPL_SD_SALE_INVOICE_HEAD-------------------------------------------
                    Dim coll1 As New Hashtable()

                    clsCommon.AddColumnsForChange(coll1, "VEHICLE_IN", obj.VEHICLE_IN)
                    clsCommon.AddColumnsForChange(coll1, "RECEIPT_IN", obj.RECEIPT_IN)
                    clsCommon.AddColumnsForChange(coll1, "TRANSFER_HO", obj.TRANSFER_HO)
                    clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_SD_SALE_INVOICE_HEAD", OMInsertOrUpdate.Update, "TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" + obj.SALE_INVOICE_NO + "'", trans)
                    '------------------------------------------------------------------------------------------------
                Next              
            End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

End Class
