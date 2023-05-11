Imports System
Imports System.Data.SqlClient
Imports common



Public Class ClsViewPunchingInvoice
#Region "veriables"
    Public TransferNo As String = Nothing
    Public NoOfCashMemo As Double
    Public PunchedInvoice As Double = 0
    Public Balance As Double = 0
    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public comp_code As String

#End Region
    ''For Save Data in tspl_tspl_ViewPunchingInvoice Table
    Public Function SaveData(ByVal obj As ClsViewPunchingInvoice, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "NoOfCashMemo", obj.NoOfCashMemo)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "TransferNo", obj.TransferNo)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_ViewPunchingInvoice", OMInsertOrUpdate.Insert, "", trans)
            
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_ViewPunchingInvoice", OMInsertOrUpdate.Update, "TransferNo='" + obj.TransferNo + "'", trans)

            End If
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved

    End Function
    Public Shared Function GetData(ByVal TransferNo As String, ByVal NavType As common.NavigatorType) As ClsViewPunchingInvoice
        Dim obj As ClsViewPunchingInvoice = Nothing
        Dim qry As String = "SELECT TransferNo, NoOfCashMemo from tspl_ViewPunchingInvoice where  2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and tspl_ViewPunchingInvoice.TransferNo =(select MIN(TransferNo) from tspl_ViewPunchingInvoice)"
            Case NavigatorType.Last
                qry += "  and tspl_ViewPunchingInvoice.TransferNo =(select Max(TransferNo) from tspl_ViewPunchingInvoice)"
            Case NavigatorType.Next
                qry += " and tspl_ViewPunchingInvoice.TransferNo=(select Min(TransferNo) from tspl_ViewPunchingInvoice where TransferNo > '" + TransferNo + "')"
            Case NavigatorType.Previous
                qry += " and tspl_ViewPunchingInvoice.TransferNo=(select Max(TransferNo) from tspl_ViewPunchingInvoice where TransferNo < '" + TransferNo + "')"
            Case NavigatorType.Current
                qry += " and tspl_ViewPunchingInvoice.TransferNo='" + TransferNo + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsViewPunchingInvoice()
            obj.TransferNo = clsCommon.myCstr(dt.Rows(0)("TransferNo"))
            obj.NoOfCashMemo = clsCommon.myCdbl(dt.Rows(0)("NoOfCashMemo"))
            obj.PunchedInvoice = GetNoOfInvoice(obj.TransferNo)
            obj.Balance = clsCommon.myCdbl(obj.NoOfCashMemo) - clsCommon.myCdbl(obj.PunchedInvoice)

        End If

        Return obj
    End Function

    Public Shared Function GetNoOfInvoice(ByVal TransferNo As String) As Integer
        Dim qry As String = "select Count(Sale_Invoice_No)as NoOfInvoice   from TSPL_SALE_INVOICE_HEAD inner join TSPL_SHIPMENT_MASTER on TSPL_SALE_INVOICE_HEAD .Shipment_No =TSPL_SHIPMENT_MASTER .Shipment_No inner join TSPL_TRANSFER_HEAD On TSPL_SHIPMENT_MASTER .Transfer_No  =TSPL_TRANSFER_HEAD .Transfer_No where TSPL_TRANSFER_HEAD.Transfer_No ='" & TransferNo & "' "
        Dim NoOfInvoice As Integer = clsDBFuncationality.getSingleValue(qry)
        Return NoOfInvoice
    End Function
End Class
