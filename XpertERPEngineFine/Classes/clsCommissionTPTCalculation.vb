Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls
'Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports System.Text

Public Class clsCommissionTPTCalculation
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As Date
    Public From_Date As Date
    Public To_Date As Date
    Public Route_No As ArrayList
    Public Item_Code As ArrayList
    Public Distributor_Code As String = Nothing
    Public Commission_TPT_Rate As Decimal = 0
    Public Remarks As String = Nothing
    Public Total_Amount As Decimal = 0
    Public Status As Integer = 0
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime
    Public Posted_By As String = Nothing
    Public Posted_Date As DateTime
    Public Arr As List(Of clsCommissionTPTCalculationDetail)
    Public Arr_Invoice As List(Of clsCommissionTPTCalculationDetailInvoice) = Nothing

#End Region

    Public Function SaveData(ByVal obj As clsCommissionTPTCalculation, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsCommissionTPTCalculation, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.frmCommissionTPTCalculation, Nothing, Nothing)
            End If
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CUSTOMER_COMMISSION_TPT_INVOICE Where Document_Code='" & obj.Document_Code & "' ", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CUSTOMER_COMMISSION_TPT_DETAIL Where Document_Code='" & obj.Document_Code & "' ", trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Distributor_Code)
            clsCommon.AddColumnsForChange(coll, "Commission_TPT_Rate", obj.Commission_TPT_Rate)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Total_Amount", obj.Total_Amount)
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMMISSION_TPT", OMInsertOrUpdate.Insert, Nothing, trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMMISSION_TPT", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_COMMISSION_TPT.Document_Code='" & obj.Document_Code & "'", trans)
            End If
            Dim objDetail As New clsCommissionTPTCalculationDetail()
            objDetail.SaveData(obj.Document_Code, obj.Arr, trans)

            Dim objDetailInvoice As New clsCommissionTPTCalculationDetailInvoice()
            objDetailInvoice.SaveData(obj.Document_Code, obj.Arr_Invoice, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_CUSTOMER_COMMISSION_TPT", "Document_Code", "TSPL_CUSTOMER_COMMISSION_TPT_DETAIL", "Document_Code", "TSPL_CUSTOMER_COMMISSION_TPT_INVOICE", "Document_Code", trans)
            objDetail = Nothing
            objDetailInvoice = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCommissionTPTCalculation
        Dim obj As clsCommissionTPTCalculation = Nothing
        Try
            Dim strQry As String = "Select * from TSPL_CUSTOMER_COMMISSION_TPT Where 2=2 "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_CUSTOMER_COMMISSION_TPT.Document_Code = (select MIN(Document_Code) from TSPL_CUSTOMER_COMMISSION_TPT where 1=1 )"
                Case NavigatorType.Last
                    strQry += " and TSPL_CUSTOMER_COMMISSION_TPT.Document_Code = (select Max(Document_Code) from TSPL_CUSTOMER_COMMISSION_TPT where 1=1 )"
                Case NavigatorType.Next
                    strQry += " and TSPL_CUSTOMER_COMMISSION_TPT.Document_Code = (select Min(Document_Code) from TSPL_CUSTOMER_COMMISSION_TPT where Document_Code>'" & strCode & "' )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_CUSTOMER_COMMISSION_TPT.Document_Code = (select Max(Document_Code) from TSPL_CUSTOMER_COMMISSION_TPT where Document_Code<'" & strCode & "' )"
                Case NavigatorType.Current
                    strQry += " and TSPL_CUSTOMER_COMMISSION_TPT.Document_Code = '" & strCode & "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsCommissionTPTCalculation()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
                obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
                obj.Distributor_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
                obj.Commission_TPT_Rate = clsCommon.myCDecimal(dt.Rows(0)("Commission_TPT_Rate"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Total_Amount = clsCommon.myCstr(dt.Rows(0)("Total_Amount"))
                obj.Status = clsCommon.myCDecimal(dt.Rows(0)("Status"))

                obj.Arr = New List(Of clsCommissionTPTCalculationDetail)
                strQry = Nothing
                strQry = "Select * from TSPL_CUSTOMER_COMMISSION_TPT_DETAIL where Document_Code='" & obj.Document_Code & "'"
                dt = Nothing
                dt = clsDBFuncationality.GetDataTable(strQry)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.Route_No = New ArrayList()
                    For Each row In dt.Rows
                        Dim objTr As New clsCommissionTPTCalculationDetail()
                        objTr.Document_Code = clsCommon.myCstr(row("Document_Code"))
                        objTr.Route_No = clsCommon.myCstr(row("Route_Code"))
                        If Not obj.Route_No.Contains(objTr.Route_No) Then
                            obj.Route_No.Add(objTr.Route_No)
                        End If
                        objTr.Detail_Date = clsCommon.myCstr(row("Date"))
                        objTr.Qty_In_Ltr = clsCommon.myCstr(row("Quantity"))
                        objTr.Amt = clsCommon.myCstr(row("Amount"))
                        If clsCommon.myLen(objTr.Document_Code) > 0 Then
                            obj.Arr.Add(objTr)
                        End If
                    Next
                End If


                obj.Arr_Invoice = New List(Of clsCommissionTPTCalculationDetailInvoice)
                strQry = Nothing
                strQry = "Select * from TSPL_CUSTOMER_COMMISSION_TPT_INVOICE where Document_Code='" & obj.Document_Code & "'"
                dt = Nothing
                dt = clsDBFuncationality.GetDataTable(strQry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    obj.Item_Code = New ArrayList()
                    For Each row In dt.Rows
                        Dim objTrInvoice As New clsCommissionTPTCalculationDetailInvoice()
                        objTrInvoice.Document_Code = clsCommon.myCstr(row("Document_Code"))
                        objTrInvoice.Invoice_No = clsCommon.myCstr(row("Invoice_Code"))
                        objTrInvoice.Item_Code = clsCommon.myCstr(row("Item_Code"))
                        If Not obj.Item_Code.Contains(objTrInvoice.Item_Code) Then
                            obj.Item_Code.Add(objTrInvoice.Item_Code)
                        End If
                        objTrInvoice.Qty_In_Ltr = clsCommon.myCstr(row("Quantity"))
                        If clsCommon.myLen(objTrInvoice.Document_Code) > 0 Then
                            obj.Arr_Invoice.Add(objTrInvoice)
                        End If
                    Next
                End If
            Else
                obj = Nothing
                Throw New Exception("Data not found !")
            End If
        Catch ex As Exception
            obj = Nothing
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CUSTOMER_COMMISSION_TPT_INVOICE Where Document_Code='" & strCode & "' ", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CUSTOMER_COMMISSION_TPT_DETAIL Where Document_Code='" & strCode & "' ", trans)
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_CUSTOMER_COMMISSION_TPT Where Document_Code='" & strCode & "' ", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function PostData(ByVal strDoc As String) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMMISSION_TPT", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_COMMISSION_TPT.Document_Code='" & strDoc & "'", Nothing)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsCommissionTPTCalculationDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Detail_Date As Date
    Public Route_No As String = Nothing
    Public Qty_In_Ltr As Decimal = 0
    Public Amt As Decimal = 0
#End Region

    Public Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsCommissionTPTCalculationDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsCommissionTPTCalculationDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Date", obj.Detail_Date)
                    clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_No)
                    clsCommon.AddColumnsForChange(coll, "Quantity", obj.Qty_In_Ltr)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amt)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMMISSION_TPT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsCommissionTPTCalculationDetailInvoice
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Invoice_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Qty_In_Ltr As Decimal = 0
#End Region

    Public Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsCommissionTPTCalculationDetailInvoice), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsCommissionTPTCalculationDetailInvoice In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Invoice_Code", obj.Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Quantity", obj.Qty_In_Ltr)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMMISSION_TPT_INVOICE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
