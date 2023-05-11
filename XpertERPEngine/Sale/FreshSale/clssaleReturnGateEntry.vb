Imports common
Imports System.Data.SqlClient
Public Class clssaleReturnGateEntryHead
#Region "Variables"

    Public Gate_Entry_No As String = Nothing
    Public Gate_Entry_Date As Date?
    Public Vehicle_Code As String = Nothing
    Public Man_Vehicle_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public Transport As String = Nothing
    Public Man_Transport As String = Nothing
    Public Doc_Type As String = Nothing
    Public Customer_Code As String = Nothing
    Public Remarks As String = Nothing
    Public Comment As String = Nothing
    Public POSTED As Integer = 0
    Public POSTED_BY As String = Nothing
    Public POSTED_DATE As Date?
    Public Vehicle_Desc As String = Nothing
    Public Location_Desc As String = Nothing
    Public Transporter_Name As String = Nothing
    Public Customer_Name As String = Nothing
    Public Arr As List(Of clssaleReturnGateEntryDetail) = Nothing
    Public ArrInvoice As List(Of clssaleReturnGateEntryInvoice) = Nothing
    Public isCancel As Integer = 0
    Public Cancel_Date As Date? = Nothing

#End Region
    Public Function SaveData(ByVal obj As clssaleReturnGateEntryHead, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clssaleReturnGateEntryHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Try
            Dim qry As String = Nothing
            qry = "delete from TSPL_Sale_Return_Gate_Entry_Detail where Gate_Entry_No='" + obj.Gate_Entry_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SALE_RETURN_GATE_ENTRY_INVOICE_WISE where Gate_Entry_No='" + obj.Gate_Entry_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Gate_Entry_No = clsERPFuncationality.GetNextCode(trans, obj.Gate_Entry_Date, clsDocType.frmSaleReturnGateEntry, "", obj.Location_Code, False)
            End If
            If (clsCommon.myLen(obj.Gate_Entry_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_Date", clsCommon.GetPrintDate(obj.Gate_Entry_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Man_Vehicle_Code", obj.Man_Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Transport", obj.Transport)
            clsCommon.AddColumnsForChange(coll, "Man_Transport", obj.Man_Transport)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "POSTED", obj.POSTED)
            clsCommon.AddColumnsForChange(coll, "isCancel", obj.isCancel)
            If obj.isCancel = 1 Then
                clsCommon.AddColumnsForChange(coll, "Cancel_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            End If

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Sale_Return_Gate_Entry_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Sale_Return_Gate_Entry_Head", OMInsertOrUpdate.Update, "TSPL_Sale_Return_Gate_Entry_Head.Gate_Entry_No='" + obj.Gate_Entry_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clssaleReturnGateEntryDetail.SaveData(obj.Gate_Entry_No, Arr, trans)

            isSaved = isSaved AndAlso clssaleReturnGateEntryInvoice.SaveData(obj.Gate_Entry_No, ArrInvoice, trans)



        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clssaleReturnGateEntryHead
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strSRGENo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clssaleReturnGateEntryHead
        Dim obj As clssaleReturnGateEntryHead = Nothing
        Dim qry As String = "select TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Gate_Entry_No ,TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Gate_Entry_Date,TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Vehicle_Code, " & _
                             " TSPL_VEHICLE_MASTER.Description as Vehicle_Desc,TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Man_Vehicle_Code,TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Location_Code " & _
                             " ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Transport ,TSPL_TRANSPORT_MASTER.Transporter_Name,TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Doc_Type ,TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Man_Transport ,TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Customer_Code ,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SALE_RETURN_GATE_ENTRY_HEAD.POSTED," & _
                             " TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Remarks, TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Comment,TSPL_SALE_RETURN_GATE_ENTRY_HEAD.isCancel" & _
                             " from TSPL_SALE_RETURN_GATE_ENTRY_HEAD" & _
                             " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.vehicle_id=TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Vehicle_Code " & _
                             " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Location_Code" & _
                             " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id= TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Transport " & _
                             " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Customer_Code  where 2=2"
        Dim whrCls As String = ""

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Gate_Entry_No = (select MIN(Gate_Entry_No) from TSPL_SALE_RETURN_GATE_ENTRY_HEAD )"
            Case NavigatorType.Last
                qry += " and TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Gate_Entry_No = (select Max(Gate_Entry_No) from TSPL_SALE_RETURN_GATE_ENTRY_HEAD )"
            Case NavigatorType.Current
                qry += " and TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Gate_Entry_No = '" + strSRGENo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Gate_Entry_No = (select Min(Gate_Entry_No) from TSPL_SALE_RETURN_GATE_ENTRY_HEAD where Gate_Entry_No>'" + strSRGENo + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_SALE_RETURN_GATE_ENTRY_HEAD.Gate_Entry_No = (select Max(Gate_Entry_No) from TSPL_SALE_RETURN_GATE_ENTRY_HEAD where Gate_Entry_No<'" + strSRGENo + "'  )"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clssaleReturnGateEntryHead()
            obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
            obj.Gate_Entry_Date = clsCommon.GetPrintDate(dt.Rows(0)("Gate_Entry_Date"), "dd/MMM/yyyy hh:mm tt")
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Vehicle_Desc = clsCommon.myCstr(dt.Rows(0)("Vehicle_Desc"))
            obj.Man_Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Man_Vehicle_Code"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.Transport = clsCommon.myCstr(dt.Rows(0)("Transport"))
            obj.Transporter_Name = clsCommon.myCstr(dt.Rows(0)("Transporter_Name"))
            obj.Man_Transport = clsCommon.myCstr(dt.Rows(0)("Man_Transport"))
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Comment = clsCommon.myCstr(dt.Rows(0)("Comment"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.POSTED = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            obj.isCancel = clsCommon.myCdbl(dt.Rows(0)("isCancel"))
            qry = "select TSPL_SALE_RETURN_GATE_ENTRY_DETAIL.Gate_Entry_No ,TSPL_SALE_RETURN_GATE_ENTRY_DETAIL.LINE_NO ,TSPL_SALE_RETURN_GATE_ENTRY_DETAIL.Item_Code ," & _
            " TSPL_ITEM_MASTER.Item_Desc ,TSPL_ITEM_MASTER.HSN_Code,TSPL_SALE_RETURN_GATE_ENTRY_DETAIL .UOM ,TSPL_SALE_RETURN_GATE_ENTRY_DETAIL.Remarks,TSPL_SALE_RETURN_GATE_ENTRY_DETAIL.Qty,TSPL_SALE_RETURN_GATE_ENTRY_DETAIL.Remarks    from TSPL_SALE_RETURN_GATE_ENTRY_DETAIL " & _
            " left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code =TSPL_SALE_RETURN_GATE_ENTRY_DETAIL.Item_Code   "
            qry += " where TSPL_SALE_RETURN_GATE_ENTRY_DETAIL.Gate_Entry_No='" + obj.Gate_Entry_No + "' ORDER BY TSPL_SALE_RETURN_GATE_ENTRY_DETAIL.Line_No asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clssaleReturnGateEntryDetail)
                Dim objTr As clssaleReturnGateEntryDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clssaleReturnGateEntryDetail

                    objTr.Gate_Entry_No = clsCommon.myCstr(dr("Gate_Entry_No"))
                    objTr.Line_No = clsCommon.myCstr(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.HSN = clsCommon.myCstr(dr("HSN_Code"))
                    objTr.UOM = clsCommon.myCstr(dr("uom"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    obj.Arr.Add(objTr)
                Next
            End If

            qry = "select * from TSPL_Sale_Return_Gate_Entry_Invoice_Wise "
            qry += " where TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Gate_Entry_No='" + obj.Gate_Entry_No + "' ORDER BY TSPL_Sale_Return_Gate_Entry_Invoice_Wise.Gate_Entry_No asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrInvoice = New List(Of clssaleReturnGateEntryInvoice)
                Dim objTrTr As clssaleReturnGateEntryInvoice
                For Each dr As DataRow In dt.Rows
                    objTrTr = New clssaleReturnGateEntryInvoice

                    objTrTr.Gate_Entry_No = clsCommon.myCstr(dr("Gate_Entry_No"))
                    objTrTr.Invoice_No = clsCommon.myCstr(dr("Invoice_No"))
                    obj.ArrInvoice.Add(objTrTr)
                Next
            End If

        End If

        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Sale Return Gate Entry No not found to Delete")
        End If
        Dim obj As clssaleReturnGateEntryHead = clssaleReturnGateEntryHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Gate_Entry_No) > 0) Then
            Try
                If (obj.POSTED = 1) Then
                    Throw New Exception("Already Posted") 'on :" + obj.Posting_Date
                End If

                Dim qry As String = "delete from TSPL_Sale_Return_Gate_Entry_Invoice_Wise where Gate_Entry_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SALE_RETURN_GATE_ENTRY_DETAIL where Gate_Entry_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SALE_RETURN_GATE_ENTRY_HEAD where Gate_Entry_No='" + strCode + "'"
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

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean

        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Sale Return Gate Entry No. not found to Post")
            End If
           
            'Dim obj As clssaleReturnGateEntryHead = clssaleReturnGateEntryHead.GetData(strDocNo, NavigatorType.Current, trans)

            'If (obj Is Nothing OrElse clsCommon.myLen(obj.Gate_Entry_No) <= 0) Then
            '    Throw New Exception("No Data found to Post")
            'End If

            Dim qry As String = "Update TSPL_Sale_Return_Gate_Entry_Head set posted=1 where Gate_Entry_No='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    ' For Cancel Document 
    Public Shared Function CancelData(ByVal FormId As String, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            CancelData(strDocNo, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function CancelData(ByVal strDocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Sale Return Gate Entry No. not found to Cancel")
            End If
            Dim qry As String = " Update TSPL_Sale_Return_Gate_Entry_Head set isCancel=1 , Cancel_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "', Modify_By = '" + objCommonVar.CurrentUserCode + "', Modify_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "'  where Gate_Entry_No='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    ' End
End Class
Public Class clssaleReturnGateEntryDetail
#Region "Variables"
    Public Gate_Entry_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public HSN As String = Nothing
    Public UOM As String = Nothing
    Public Qty As Double = 0
    Public Remarks As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clssaleReturnGateEntryDetail), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clssaleReturnGateEntryDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Sale_Return_Gate_Entry_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class

Public Class clssaleReturnGateEntryInvoice
#Region "Variables"
    Public Gate_Entry_No As String = Nothing
    Public Invoice_No As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clssaleReturnGateEntryInvoice), ByVal trans As SqlTransaction) As Boolean

        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clssaleReturnGateEntryInvoice In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Invoice_No", obj.Invoice_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Sale_Return_Gate_Entry_Invoice_Wise", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
