Imports System
Imports System.Data.SqlClient
Imports common


Public Class ClsTransferIncompleteRemarks
#Region "veriables"
    Public TransferNo As String = Nothing
    Public QuickSettlementRemarks As String
    Public Invoiceremarks As String
    Public routeNo As String
    Public routdesc As String
    Public transferDate As String
    Public saleCode As String
    Public saledesc As String
    Public Modify_By As String = Nothing
    Public Modify_Date As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public comp_code As String
#End Region

    ''For Save Data in Tspl_TransferIncompleteRemarks
    Public Function SaveData(ByVal obj As ClsTransferIncompleteRemarks, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "QuickSettlementRemarks", obj.QuickSettlementRemarks)
            clsCommon.AddColumnsForChange(coll, "InvoiceRemarks", obj.Invoiceremarks)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "TransferNo ", obj.TransferNo)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_TransferIncompleteRemarks ", OMInsertOrUpdate.Insert, "", trans)

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_TransferIncompleteRemarks ", OMInsertOrUpdate.Update, "TransferNo='" + obj.TransferNo + "'", trans)

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

    Public Shared Function GetData(ByVal code As String, ByVal NavType As common.NavigatorType) As ClsTransferIncompleteRemarks
        Dim obj As ClsTransferIncompleteRemarks = Nothing
        Dim qry As String = "select TransferNo, quicksettlementremarks,invoiceremarks from tspl_transferincompleteRemarks  where  2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Tspl_TransferIncompleteRemarks .TransferNo =(select MIN(TransferNo) from Tspl_TransferIncompleteRemarks )"
            Case NavigatorType.Last
                qry += "  and Tspl_TransferIncompleteRemarks .TransferNo =(select Max(TransferNo) from Tspl_TransferIncompleteRemarks )"
            Case NavigatorType.Next
                qry += " and Tspl_TransferIncompleteRemarks .TransferNo=(select Min(TransferNo) from Tspl_TransferIncompleteRemarks  where TransferNo > '" + code + "')"
            Case NavigatorType.Previous
                qry += " and Tspl_TransferIncompleteRemarks .TransferNo=(select Max(TransferNo) from Tspl_TransferIncompleteRemarks  where TransferNo < '" + code + "')"
            Case NavigatorType.Current
                qry += " and Tspl_TransferIncompleteRemarks .TransferNo='" + code + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsTransferIncompleteRemarks
            obj.TransferNo = clsCommon.myCstr(dt.Rows(0)("TransferNo"))
            obj.QuickSettlementRemarks = clsCommon.myCstr(dt.Rows(0)("QuickSettlementRemarks"))
            obj.Invoiceremarks = clsCommon.myCstr(dt.Rows(0)("InvoiceRemarks"))
            Dim dt1 As DataTable = getrouteNo(obj.TransferNo)

            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj.transferDate = clsCommon.GetPrintDate(dt1.Rows(0)("Transfer_Date"), "dd/MMM/yyyy")
                obj.routeNo = clsCommon.myCstr(dt1.Rows(0)("Route_No"))
                obj.routdesc = clsCommon.myCstr(dt1.Rows(0)("Route_Desc"))
                obj.saleCode = clsCommon.myCstr(dt1.Rows(0)("Salesmancode"))
                obj.saledesc = clsCommon.myCstr(dt1.Rows(0)("SalesManDesc"))
            End If
        End If

        Return obj
    End Function
    Public Shared Function getrouteNo(ByVal transferNo As String) As DataTable
        Dim qry As String = "SELECT  TSPL_TRANSFER_HEAD .Transfer_Date ,TSPL_TRANSFER_HEAD .Route_No ,TSPL_TRANSFER_HEAD .Route_Desc ,TSPL_TRANSFER_HEAD .Salesmancode , (select Emp_Name  from TSPL_EMPLOYEE_MASTER  where TSPL_EMPLOYEE_MASTER .EMP_CODE =TSPL_TRANSFER_HEAD .Salesmancode  and Emp_type ='Salesman'   )as SalesManDesc from TSPL_TRANSFER_HEAD  where Transfer_No ='" & transferNo & "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt1

    End Function


    Public Shared Function DeleteData(ByVal Code As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(Code) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If
        Dim obj As ClsTransferIncompleteRemarks = ClsTransferIncompleteRemarks.GetData(Code, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.TransferNo) > 0) Then
            Try
                Dim qry As String = "delete from Tspl_TransferIncompleteRemarks  where TransferNo ='" + Code + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from Tspl_TransferIncompleteRemarks  where TransferNo ='" + Code + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function
End Class
