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
    Public Total_Qty As Decimal = 0
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
            clsCommon.AddColumnsForChange(coll, "Total_Qty", obj.Total_Qty)
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

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_CUSTOMER_COMMISSION_TPT", "Document_Code", "TSPL_CUSTOMER_COMMISSION_TPT_DETAIL", "Document_Code", "TSPL_CUSTOMER_COMMISSION_TPT_INVOICE", "Document_Code", trans)
            objDetail = Nothing
            'objDetailInvoice = Nothing
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
                obj.Total_Qty = clsCommon.myCDecimal(dt.Rows(0)("Total_Qty"))
                obj.Total_Amount = clsCommon.myCDecimal(dt.Rows(0)("Total_Amount"))
                obj.Status = clsCommon.myCDecimal(dt.Rows(0)("Status"))

                'obj.Arr = New List(Of clsCommissionTPTCalculationDetail)
                Dim objDetail As New clsCommissionTPTCalculationDetail()
                obj.Arr = objDetail.GetData(obj.Document_Code, 0, False)
                objDetail = Nothing
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    obj.Route_No = New ArrayList()
                    For Each routeNo In obj.Arr
                        If Not obj.Route_No.Contains(routeNo.Route_No) Then
                            obj.Route_No.Add(routeNo.Route_No)
                        End If
                    Next
                End If
                obj.Arr_Invoice = New List(Of clsCommissionTPTCalculationDetailInvoice)
                Dim objInvoiceDetail As New clsCommissionTPTCalculationDetailInvoice()
                obj.Arr_Invoice = objInvoiceDetail.GetData(obj.Document_Code, 0, True)
                objInvoiceDetail = Nothing
                If obj.Arr_Invoice IsNot Nothing AndAlso obj.Arr_Invoice.Count > 0 Then
                    obj.Item_Code = New ArrayList()
                    For Each itemCode In obj.Arr_Invoice
                        If Not obj.Item_Code.Contains(itemCode.Item_Code) Then
                            obj.Item_Code.Add(itemCode.Item_Code)
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

    Public Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Try
            Dim obj As clsCommissionTPTCalculation = New clsCommissionTPTCalculation
            obj = obj.GetData(strCode, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
                Return False
            End If
            If Not obj.Status = 1 Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
                Return False
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMMISSION_TPT", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_COMMISSION_TPT.Document_Code='" & strCode & "'", Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsCommissionTPTCalculationDetail
#Region "Variables"
    Public PK_ID As Integer
    Public Document_Code As String = Nothing
    Public Detail_Date As Date
    Public Route_No As String = Nothing
    Public Qty_In_Ltr As Decimal = 0
    Public Amt As Decimal = 0
    Public Arr_Invoice As List(Of clsCommissionTPTCalculationDetailInvoice) = Nothing
#End Region

    Public Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsCommissionTPTCalculationDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsCommissionTPTCalculationDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Date", clsCommon.GetPrintDate(obj.Detail_Date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_No)
                    clsCommon.AddColumnsForChange(coll, "Quantity", obj.Qty_In_Ltr)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amt)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMMISSION_TPT_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                    Dim PK_ID As Integer = clsERPFuncationality.GetScopeIdentityValue(trans)
                    Dim objInvoiceDetail As New clsCommissionTPTCalculationDetailInvoice()
                    objInvoiceDetail.SaveData(strDocNo, PK_ID, obj.Arr_Invoice, trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal PK_ID As Integer, ByVal LoadAllData As Boolean) As List(Of clsCommissionTPTCalculationDetail)
        Dim arr As List(Of clsCommissionTPTCalculationDetail) = Nothing
        Dim qry As String = "select * from TSPL_CUSTOMER_COMMISSION_TPT_DETAIL Where Document_Code = '" & strCode & "' order by Document_Code,PK_ID "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsCommissionTPTCalculationDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsCommissionTPTCalculationDetail = New clsCommissionTPTCalculationDetail()
                obj.PK_ID = clsCommon.myCDecimal(dr("PK_ID"))
                obj.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                obj.Detail_Date = clsCommon.myCDate(dr("Date"))
                obj.Route_No = clsCommon.myCstr(dr("Route_Code"))
                obj.Qty_In_Ltr = clsCommon.myCDecimal(dr("Quantity"))
                obj.Amt = clsCommon.myCDecimal(dr("Amount"))

                'Dim objInvoice As New clsCommissionTPTCalculationDetailInvoice()
                'obj.Arr_Invoice = objInvoice.GetData(strCode, PK_ID, LoadAllData)
                'objInvoice = Nothing

                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class

Public Class clsCommissionTPTCalculationDetailInvoice
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Invoice_No As String = Nothing
    Public Invoice_Date As Date = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Ref_PK_ID As Integer = 0
    Public Qty_In_Ltr As Decimal = 0
#End Region

    Public Function SaveData(ByVal strDocNo As String, ByVal PK_ID As Integer, ByVal Arr As List(Of clsCommissionTPTCalculationDetailInvoice), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsCommissionTPTCalculationDetailInvoice In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Invoice_Code", obj.Invoice_No)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Quantity", obj.Qty_In_Ltr)
                    clsCommon.AddColumnsForChange(coll, "Ref_PK_ID", PK_ID)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_COMMISSION_TPT_INVOICE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function GetData(ByVal strCode As String, ByVal Ref_PK_ID As Integer, ByVal LoadAllData As Boolean) As List(Of clsCommissionTPTCalculationDetailInvoice)
        Dim Arr_Invoice As List(Of clsCommissionTPTCalculationDetailInvoice) = Nothing
        Try
            Dim qry As String = "Select Convert(Varchar(10),TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)Document_Date,TSPL_CUSTOMER_COMMISSION_TPT_INVOICE.*,TSPL_ITEM_MASTER.Item_Desc 
from TSPL_CUSTOMER_COMMISSION_TPT_INVOICE 
Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_CUSTOMER_COMMISSION_TPT_INVOICE.Invoice_Code
Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code=TSPL_CUSTOMER_COMMISSION_TPT_INVOICE.Item_Code Where TSPL_CUSTOMER_COMMISSION_TPT_INVOICE.Document_Code =  '" & strCode & "' "

            If Not LoadAllData Then
                qry += " and TSPL_CUSTOMER_COMMISSION_TPT_INVOICE.Ref_PK_ID = " & clsCommon.myCstr(Ref_PK_ID) & " "
            End If
            qry += " order by TSPL_CUSTOMER_COMMISSION_TPT_INVOICE.Document_Code, TSPL_CUSTOMER_COMMISSION_TPT_INVOICE.PK_ID "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Arr_Invoice = New List(Of clsCommissionTPTCalculationDetailInvoice)()
                For Each dr As DataRow In dt.Rows
                    Dim objTR As clsCommissionTPTCalculationDetailInvoice = New clsCommissionTPTCalculationDetailInvoice()
                    objTR.Document_Code = strCode
                    objTR.Ref_PK_ID = clsCommon.myCdbl(dr("Ref_PK_ID"))
                    objTR.Invoice_No = clsCommon.myCstr(dr("Invoice_Code"))
                    objTR.Invoice_Date = clsCommon.myCstr(dr("Document_Date"))
                    objTR.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTR.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTR.Qty_In_Ltr = clsCommon.myCDecimal(dr("Quantity"))
                    Arr_Invoice.Add(objTR)
                    objTR = Nothing
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Arr_Invoice
    End Function
End Class
