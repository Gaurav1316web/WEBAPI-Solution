Imports common
Imports System.Data.SqlClient
Imports System.Collections

Public Class ClsMaterialQuotationHead
#Region "Variables"
    Public Code As String = Nothing
    Public QDate As DateTime = Nothing
    Public Location_Code As String = Nothing
    Public Requisition_Id As String = Nothing
    Public Description As String = Nothing
    Public Ref_No As String = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public On_Hold As Boolean = False
    Public Status As ERPTransactionStatus = 0
    Public Posting_Date As DateTime? = Nothing
    Public Total_Amt As Double = 0
    Public ArrTr As List(Of ClsMaterialQuotationDeatil) = Nothing
    Public Form_ID As String = ""
    'Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public arrCustList As ArrayList = Nothing
    Public Is_Taxable As Boolean = False
#End Region

    Public Function SaveData(ByVal obj As ClsMaterialQuotationHead, ByVal isNewEntry As Boolean, Optional ByVal import As Boolean = False, Optional ByVal isMakeAbandomentNo As Boolean = False) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isMakeAbandomentNo = False Then
                If Not isNewEntry Then
                    Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_SCRAP_QUOTATION_HEAD Where Code='" + obj.Code + "'", trans))
                    If Status = 1 Then
                        Throw New Exception("This document is already posted.")
                    End If
                End If
            End If

            If Not import Then
                Dim qry As String = "delete from TSPL_SCRAP_QUOTATION_DETAIL where Code='" + obj.Code + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "Delete from TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL where code='" + obj.Code + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            End If
            If (isNewEntry) Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.QDate, clsDocType.MaterialQuotation, "", obj.Location_Code)
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "QDate", clsCommon.GetPrintDate(obj.QDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Requisition_Id", obj.Requisition_Id)
            'clsCommon.AddColumnsForChange(coll, "RFQ_NO", obj.RFQ_NO)
            'clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            'clsCommon.AddColumnsForChange(coll, "Quotation_No", obj.Quotation_No)
            'clsCommon.AddColumnsForChange(coll, "Quotation_Date", clsCommon.GetPrintDate(obj.Quotation_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", IIf(obj.Is_Taxable, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAP_QUOTATION_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAP_QUOTATION_HEAD", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsMaterialQuotationDeatil.SaveData(obj.Code, obj.ArrTr, trans)
            isSaved = isSaved AndAlso clsMaterialCustomerDetail.SaveData(obj.Code, obj.arrCustList, trans)
            'isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Code, obj.arrCustomFields, trans)
            trans.Commit()

            'If Not isNewEntry Then
            '    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Code), "TSPL_SCRAP_QUOTATION_HEAD", "Code", "TSPL_SCRAP_QUOTATION_DETAIL", "Code", trans)
            'End If

        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

        Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As ClsMaterialQuotationHead
            Return GetData(strCode, NavType, Nothing)
        End Function

        Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As ClsMaterialQuotationHead
            Dim obj As ClsMaterialQuotationHead = Nothing
            Dim qry As String = "SELECT TSPL_SCRAP_QUOTATION_HEAD.* FROM TSPL_SCRAP_QUOTATION_HEAD where  2=2"

            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_SCRAP_QUOTATION_HEAD.Code=(select MIN(Code) from TSPL_SCRAP_QUOTATION_HEAD  )"
                Case NavigatorType.Last
                    qry += " and TSPL_SCRAP_QUOTATION_HEAD.Code=(select Max(Code) from TSPL_SCRAP_QUOTATION_HEAD  )"
                Case NavigatorType.Next
                    qry += " and TSPL_SCRAP_QUOTATION_HEAD.Code=(select Min(Code) from TSPL_SCRAP_QUOTATION_HEAD where Code > '" + strCode + "' )"
                Case NavigatorType.Previous
                    qry += " and TSPL_SCRAP_QUOTATION_HEAD.Code=(select Max(Code) from TSPL_SCRAP_QUOTATION_HEAD where Code < '" + strCode + "' )"
                Case NavigatorType.Current
                    qry += " and TSPL_SCRAP_QUOTATION_HEAD.Code='" + strCode + "'"
            End Select

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsMaterialQuotationHead()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.QDate = clsCommon.myCDate(dt.Rows(0)("QDate"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                'obj.RFQ_NO = clsCommon.myCstr(dt.Rows(0)("RFQ_NO"))
                obj.Requisition_Id = clsCommon.myCstr(dt.Rows(0)("Requisition_Id"))
                'obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                'obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
                'obj.Quotation_No = clsCommon.myCstr(dt.Rows(0)("Quotation_No"))
                'obj.Quotation_Date = clsCommon.myCDate(dt.Rows(0)("Quotation_Date"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.On_Hold = clsCommon.myCdbl(dt.Rows(0)("On_Hold"))
            obj.Is_Taxable = If(clsCommon.myCdbl(dt.Rows(0)("Is_Taxable")) > 0, True, False)
                If (clsCommon.myCdbl(dt.Rows(0)("Status")) = 0) Then
                    obj.Status = ERPTransactionStatus.Pending
                Else
                    obj.Status = ERPTransactionStatus.Approved
                End If

                If dt.Rows(0)("Posting_Date") Is DBNull.Value Then
                    obj.Posting_Date = Nothing
                Else
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))

                qry = "SELECT TSPL_SCRAP_QUOTATION_DETAIL.* FROM TSPL_SCRAP_QUOTATION_DETAIL  where TSPL_SCRAP_QUOTATION_DETAIL.Code='" + obj.Code + "' ORDER BY Line_No"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.ArrTr = New List(Of ClsMaterialQuotationDeatil)
                    Dim objTr As ClsMaterialQuotationDeatil
                    For Each dr As DataRow In dt.Rows
                        objTr = New ClsMaterialQuotationDeatil()
                        objTr.Code = clsCommon.myCstr(dr("Code"))
                        objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                        objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                        objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                        objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                        objTr.Item_Cost = clsCommon.myCstr(dr("Item_Cost"))
                        objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                        objTr.Specification = clsCommon.myCstr(dr("Specification"))
                        objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                        obj.ArrTr.Add(objTr)
                    Next
            End If

            Dim templist As New ArrayList
            qry = "SELECT TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL.* FROM TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL  where TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL.Code='" + obj.Code + "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                'obj.arrCustomerList = New List(Of clsMaterialCustomerDetail)
                'Dim objTr As clsMaterialCustomerDetail
                For Each dr As DataRow In dt.Rows
                    'objTr = New clsMaterialCustomerDetail()
                    'objTr.Code = clsCommon.myCstr(dr("Code"))
                    'objTr.Customer_Code = clsCommon.myCstr(dr("Customer_Code"))
                    templist.Add(clsCommon.myCstr(dr("Customer_Code")))
                Next
            End If
            obj.arrCustList = templist
        End If

            Return obj
        End Function

        Public Shared Function PostData(ByVal strDocNo As String) As Boolean
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Quotation No not found to Post")
                End If
                Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
                Dim obj As ClsMaterialQuotationHead = ClsMaterialQuotationHead.GetData(strDocNo, NavigatorType.Current, trans)
                If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("No Data found to Post")
                End If
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                If (obj.On_Hold = 1) Then
                Throw New Exception("Material Quotation No " + obj.Code + " Is On Hold.Can't Post it")
                End If

                Dim qry As String = "Update TSPL_SCRAP_QUOTATION_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Code='" + strDocNo + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                trans.Commit()

            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
            Return True
        End Function

        Public Shared Function DeleteData(ByVal strCode As String) As Boolean
            Dim isSaved As Boolean = False
            If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Quotation No not found to Delete")
            End If
            Dim obj As ClsMaterialQuotationHead = ClsMaterialQuotationHead.GetData(strCode, NavigatorType.Current)
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                Try
                    If (obj.Status = ERPTransactionStatus.Approved) Then
                        Throw New Exception("Already Post on :" + obj.Posting_Date)
                    End If
                    Dim qry As String = "delete from TSPL_SCRAP_QUOTATION_DETAIL where Code='" + strCode + "'"
                    isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL where Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "delete from TSPL_SCRAP_QUOTATION_HEAD where Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


                'isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Code, trans)
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

        Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
            Dim qry As String = "select TSPL_SCRAP_QUOTATION_HEAD.CODE from TSPL_SCRAP_QUOTATION_HEAD where TSPL_SCRAP_QUOTATION_HEAD.CODE ='" + Code + "'   "
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If

        End Function

    End Class

    Public Class ClsMaterialQuotationDeatil
#Region "Variables"
        Public Code As String = Nothing
        Public Line_No As Integer = 0
        Public Item_Code As String = Nothing
        Public Item_Desc As String = Nothing
        Public Qty As Double = 0
        Public Unit_Code As String = Nothing
        Public Item_Cost As String = Nothing
        Public Item_Net_Amt As Double = 0
        Public Specification As String = Nothing
        Public Remarks As String = Nothing
#End Region

        Public Shared Function SaveData(ByVal strReqNo As String, ByVal Arr As List(Of ClsMaterialQuotationDeatil), ByVal trans As SqlTransaction, Optional ByVal import As Boolean = False) As Boolean
            Dim intLineNo As Integer = 1
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As ClsMaterialQuotationDeatil In Arr
                    Dim coll As New Hashtable()
                    If Not import Then
                        clsCommon.AddColumnsForChange(coll, "Code", strReqNo)
                    Else
                        clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                    End If
                    'clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
                    clsCommon.AddColumnsForChange(coll, "Line_No", intLineNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                    clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                    clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                    clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAP_QUOTATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    intLineNo = intLineNo + 1
                Next
            End If
            Return True
        End Function
End Class
Public Class clsMaterialCustomerDetail
#Region "Variables"
    'Public Code As String = ""
    Public Customer_Code As String = ""
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As ArrayList, ByVal tran As SqlTransaction) As Boolean
        Try

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i As Integer = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Code", DocNo)
                    clsCommon.AddColumnsForChange(coll, "Customer_Code", arr.Item(i))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAP_QUOTATION_CUSTOMER_DETAIL", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class


Public Class ClsMaterialQuotationOrderHead
#Region "Variables"
    Public Code As String = Nothing
    Public QODate As DateTime = Nothing
    Public Location_Code As String = Nothing
    Public Requisition_Id As String = Nothing
    Public Description As String = Nothing
    Public Ref_No As String = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public On_Hold As Boolean = False
    Public Status As ERPTransactionStatus = 0
    Public Posting_Date As DateTime? = Nothing
    Public Total_Amt As Double = 0
    Public ArrTr As List(Of ClsMaterialQuotationOrderDeatil) = Nothing
    Public Form_ID As String = ""
    'Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
    Public Customer_Code As String = Nothing
    Public ScrapQuotation_Code As String = Nothing
    Public Is_Taxable As Boolean = False
#End Region

    'sanjay

    Public Function SaveData(ByVal obj As ClsMaterialQuotationOrderHead, ByVal isNewEntry As Boolean, Optional ByVal import As Boolean = False, Optional ByVal isMakeAbandomentNo As Boolean = False) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isMakeAbandomentNo = False Then
                If Not isNewEntry Then
                    Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_SCRAP_QUOTATION_ORDER_HEAD Where Code='" + obj.Code + "'", trans))
                    If Status = 1 Then
                        Throw New Exception("This document is already posted.")
                    End If
                End If
            End If

            If Not import Then
                Dim qry As String = "delete from TSPL_SCRAP_QUOTATION_ORDER_DETAIL where Code='" + obj.Code + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            End If
            If (isNewEntry) Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.QODate, clsDocType.MaterialQuotationOrder, "", obj.Location_Code)
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "QODate", clsCommon.GetPrintDate(obj.QODate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Requisition_Id", obj.Requisition_Id)
            clsCommon.AddColumnsForChange(coll, "ScrapQuotation_Code", obj.ScrapQuotation_Code)
            clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
            'clsCommon.AddColumnsForChange(coll, "Quotation_No", obj.Quotation_No)
            'clsCommon.AddColumnsForChange(coll, "Quotation_Date", clsCommon.GetPrintDate(obj.Quotation_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", IIf(obj.Is_Taxable, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAP_QUOTATION_ORDER_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAP_QUOTATION_ORDER_HEAD", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsMaterialQuotationOrderDeatil.SaveData(obj.Code, obj.ArrTr, trans)
            'isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Code, obj.arrCustomFields, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As ClsMaterialQuotationOrderHead
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As ClsMaterialQuotationOrderHead
        Dim obj As ClsMaterialQuotationOrderHead = Nothing
        Dim qry As String = "SELECT TSPL_SCRAP_QUOTATION_ORDER_HEAD.* FROM TSPL_SCRAP_QUOTATION_ORDER_HEAD where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SCRAP_QUOTATION_ORDER_HEAD.Code=(select MIN(Code) from TSPL_SCRAP_QUOTATION_ORDER_HEAD  )"
            Case NavigatorType.Last
                qry += " and TSPL_SCRAP_QUOTATION_ORDER_HEAD.Code=(select Max(Code) from TSPL_SCRAP_QUOTATION_ORDER_HEAD  )"
            Case NavigatorType.Next
                qry += " and TSPL_SCRAP_QUOTATION_ORDER_HEAD.Code=(select Min(Code) from TSPL_SCRAP_QUOTATION_ORDER_HEAD where Code > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_SCRAP_QUOTATION_ORDER_HEAD.Code=(select Max(Code) from TSPL_SCRAP_QUOTATION_ORDER_HEAD where Code < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_SCRAP_QUOTATION_ORDER_HEAD.Code='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsMaterialQuotationOrderHead()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.QODate = clsCommon.myCDate(dt.Rows(0)("QODate"))
            obj.ScrapQuotation_Code = clsCommon.myCstr(dt.Rows(0)("ScrapQuotation_Code"))
            obj.Requisition_Id = clsCommon.myCstr(dt.Rows(0)("Requisition_Id"))
            obj.Customer_Code = clsCommon.myCstr(dt.Rows(0)("Customer_Code"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            'obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            'obj.Quotation_No = clsCommon.myCstr(dt.Rows(0)("Quotation_No"))
            'obj.Quotation_Date = clsCommon.myCDate(dt.Rows(0)("Quotation_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.On_Hold = clsCommon.myCdbl(dt.Rows(0)("On_Hold"))
            obj.Is_Taxable = If(clsCommon.myCdbl(dt.Rows(0)("Is_Taxable")) > 0, True, False)
            If (clsCommon.myCdbl(dt.Rows(0)("Status")) = 0) Then
                obj.Status = ERPTransactionStatus.Pending
            Else
                obj.Status = ERPTransactionStatus.Approved
            End If

            If dt.Rows(0)("Posting_Date") Is DBNull.Value Then
                obj.Posting_Date = Nothing
            Else
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If
            obj.Total_Amt = clsCommon.myCdbl(dt.Rows(0)("Total_Amt"))

            qry = "SELECT TSPL_SCRAP_QUOTATION_ORDER_DETAIL.* FROM TSPL_SCRAP_QUOTATION_ORDER_DETAIL  where TSPL_SCRAP_QUOTATION_ORDER_DETAIL.Code='" + obj.Code + "' ORDER BY Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrTr = New List(Of ClsMaterialQuotationOrderDeatil)
                Dim objTr As ClsMaterialQuotationOrderDeatil
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsMaterialQuotationOrderDeatil()
                    objTr.Code = clsCommon.myCstr(dr("Code"))
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Item_Cost = clsCommon.myCstr(dr("Item_Cost"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    obj.ArrTr.Add(objTr)
                Next
            End If

        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Quotation Order No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As ClsMaterialQuotationOrderHead = ClsMaterialQuotationOrderHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold = 1) Then
                Throw New Exception("Material Quotation Order No " + obj.Code + " Is On Hold.Can't Post it")
            End If

            Dim qry As String = "Update TSPL_SCRAP_QUOTATION_ORDER_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Quotation Order No not found to Delete")
        End If
        Dim obj As ClsMaterialQuotationOrderHead = ClsMaterialQuotationOrderHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_SCRAP_QUOTATION_ORDER_DETAIL where Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SCRAP_QUOTATION_ORDER_HEAD where Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


                'isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Code, trans)
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

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_SCRAP_QUOTATION_ORDER_HEAD.CODE from TSPL_SCRAP_QUOTATION_ORDER_HEAD where TSPL_SCRAP_QUOTATION_ORDER_HEAD.CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class

Public Class ClsMaterialQuotationOrderDeatil
#Region "Variables"
    Public Code As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0
    Public Unit_Code As String = Nothing
    Public Item_Cost As String = Nothing
    Public Item_Net_Amt As Double = 0
    Public Specification As String = Nothing
    Public Remarks As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strReqNo As String, ByVal Arr As List(Of ClsMaterialQuotationOrderDeatil), ByVal trans As SqlTransaction, Optional ByVal import As Boolean = False) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsMaterialQuotationOrderDeatil In Arr
                Dim coll As New Hashtable()
                If Not import Then
                    clsCommon.AddColumnsForChange(coll, "Code", strReqNo)
                Else
                    clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                End If
                'clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
                clsCommon.AddColumnsForChange(coll, "Line_No", intLineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAP_QUOTATION_ORDER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                intLineNo = intLineNo + 1
            Next
        End If
        Return True
    End Function
End Class

'Quotatuion Comparison Start 

'FOR LOADING QUOTATION ORDER AGAINST MATERIAL QUOTATION START
Public Class ClsGettingMaterialQuotationOrderData

#Region "Variables"
    
    Public ArrTr As List(Of ClsGettingMaterialQuotationOrderData) = Nothing
    Public Order_Code As String = Nothing
    Public Customer_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0
    Public Unit_Code As String = Nothing
    Public Item_Cost As String = Nothing
    Public Item_Net_Amt As Double = 0
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
    Public Is_Taxable As Boolean = False
#End Region

    Public Shared Function GetOrderData(ByVal strCode As String) As ClsGettingMaterialQuotationOrderData
        Dim obj As ClsGettingMaterialQuotationOrderData = Nothing
        Dim qry As String = "select TSPL_SCRAP_QUOTATION_ORDER_HEAD.Is_Taxable,TSPL_SCRAP_QUOTATION_ORDER_HEAD.Customer_Code,TSPL_SCRAP_QUOTATION_ORDER_HEAD.Location_Code,TSPL_SCRAP_QUOTATION_ORDER_DETAIL.* "
        qry += " from TSPL_SCRAP_QUOTATION_ORDER_HEAD "
        qry += " left outer join TSPL_SCRAP_QUOTATION_ORDER_DETAIL on TSPL_SCRAP_QUOTATION_ORDER_DETAIL.code = TSPL_SCRAP_QUOTATION_ORDER_HEAD.Code"
        qry += " where TSPL_SCRAP_QUOTATION_ORDER_HEAD.ScrapQuotation_Code='" & strCode & "'"
        qry += " order by TSPL_SCRAP_QUOTATION_ORDER_HEAD.code,TSPL_SCRAP_QUOTATION_ORDER_DETAIL.Line_No"
        obj = New ClsGettingMaterialQuotationOrderData
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.ArrTr = New List(Of ClsGettingMaterialQuotationOrderData)
            Dim objTr As ClsGettingMaterialQuotationOrderData
            For Each dr As DataRow In dt.Rows
                objTr = New ClsGettingMaterialQuotationOrderData()
                objTr.Order_Code = clsCommon.myCstr(dr("Code"))
                objTr.Is_Taxable = If(clsCommon.myCdbl(dr("Is_Taxable")) > 0, True, False)
                objTr.Customer_Code = clsCommon.myCstr(dr("Customer_Code"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                objTr.Item_Cost = clsCommon.myCstr(dr("Item_Cost"))
                objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                objTr.Specification = clsCommon.myCstr(dr("Specification"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                obj.ArrTr.Add(objTr)
            Next
        End If

        Return obj
    End Function
End Class
'FOR LOADING QUOTATION ORDER AGAINST MATERIAL QUOTATION END

Public Class ClsMaterialQuotationComparisonHead
#Region "Variables"
    Public Code As String = Nothing
    Public QCDate As DateTime = Nothing
    Public Location_Code As String = Nothing
    Public Requisition_Id As String = Nothing
    Public Description As String = Nothing
    Public Ref_No As String = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public On_Hold As Boolean = False
    Public Status As ERPTransactionStatus = 0
    Public Posting_Date As DateTime? = Nothing
    Public ArrTr As List(Of ClsMaterialQuotationComparisonDeatil) = Nothing
    Public Form_ID As String = ""
    Public ScrapQuotation_Code As String = Nothing
    Public Is_Taxable As Boolean = False
#End Region

    
    Public Function SaveData(ByVal obj As ClsMaterialQuotationComparisonHead, ByVal isNewEntry As Boolean, Optional ByVal import As Boolean = False, Optional ByVal isMakeAbandomentNo As Boolean = False) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isMakeAbandomentNo = False Then
                If Not isNewEntry Then
                    Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD Where Code='" + obj.Code + "'", trans))
                    If Status = 1 Then
                        Throw New Exception("This document is already posted.")
                    End If
                End If
            End If

            If Not import Then
                Dim qry As String = "delete from TSPL_SCRAP_QUOTATION_COMPARISON_DETAIL where Code='" + obj.Code + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            End If
            If (isNewEntry) Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.QCDate, clsDocType.MaterialQuotationComparison, "", obj.Location_Code)
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "QCDate", clsCommon.GetPrintDate(obj.QCDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Requisition_Id", obj.Requisition_Id)
            clsCommon.AddColumnsForChange(coll, "ScrapQuotation_Code", obj.ScrapQuotation_Code)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_Taxable", IIf(obj.Is_Taxable, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAP_QUOTATION_COMPARISON_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAP_QUOTATION_COMPARISON_HEAD", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsMaterialQuotationComparisonDeatil.SaveData(obj.Code, obj.ArrTr, trans)

            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As ClsMaterialQuotationComparisonHead
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As ClsMaterialQuotationComparisonHead
        Dim obj As ClsMaterialQuotationComparisonHead = Nothing
        Dim qry As String = "SELECT TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.* FROM TSPL_SCRAP_QUOTATION_COMPARISON_HEAD where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.Code=(select MIN(Code) from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD  )"
            Case NavigatorType.Last
                qry += " and TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.Code=(select Max(Code) from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD  )"
            Case NavigatorType.Next
                qry += " and TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.Code=(select Min(Code) from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD where Code > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.Code=(select Max(Code) from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD where Code < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.Code='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsMaterialQuotationComparisonHead()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.QCDate = clsCommon.myCDate(dt.Rows(0)("QCDate"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.ScrapQuotation_Code = clsCommon.myCstr(dt.Rows(0)("ScrapQuotation_Code"))
            obj.Requisition_Id = clsCommon.myCstr(dt.Rows(0)("Requisition_Id"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.On_Hold = clsCommon.myCdbl(dt.Rows(0)("On_Hold"))
            obj.Is_Taxable = If(clsCommon.myCdbl(dt.Rows(0)("Is_Taxable")) > 0, True, False)
            If (clsCommon.myCdbl(dt.Rows(0)("Status")) = 0) Then
                obj.Status = ERPTransactionStatus.Pending
            Else
                obj.Status = ERPTransactionStatus.Approved
            End If

            If dt.Rows(0)("Posting_Date") Is DBNull.Value Then
                obj.Posting_Date = Nothing
            Else
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If

            qry = "SELECT TSPL_SCRAP_QUOTATION_COMPARISON_DETAIL.* FROM TSPL_SCRAP_QUOTATION_COMPARISON_DETAIL  where TSPL_SCRAP_QUOTATION_COMPARISON_DETAIL.Code='" + obj.Code + "' ORDER BY Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrTr = New List(Of ClsMaterialQuotationComparisonDeatil)
                Dim objTr As ClsMaterialQuotationComparisonDeatil
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsMaterialQuotationComparisonDeatil()
                    objTr.Code = clsCommon.myCstr(dr("Code"))
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Customer_Code = clsCommon.myCstr(dr("Customer_Code"))
                    objTr.QuotationOrder_Code = clsCommon.myCstr(dr("QuotationOrder_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Desc = clsCommon.myCstr(dr("Item_Desc"))
                    objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Item_Cost = clsCommon.myCstr(dr("Item_Cost"))
                    objTr.Item_Net_Amt = clsCommon.myCdbl(dr("Item_Net_Amt"))
                    objTr.Specification = clsCommon.myCstr(dr("Specification"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    obj.ArrTr.Add(objTr)
                Next
            End If

        End If

        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Quotation Comparison No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As ClsMaterialQuotationComparisonHead = ClsMaterialQuotationComparisonHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold = 1) Then
                Throw New Exception("Material Quotation Comparison No " + obj.Code + " Is On Hold.Can't Post it")
            End If

            Dim qry As String = "Update TSPL_SCRAP_QUOTATION_COMPARISON_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Quotation Comparison No not found to Delete")
        End If
        Dim obj As ClsMaterialQuotationComparisonHead = ClsMaterialQuotationComparisonHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_SCRAP_QUOTATION_COMPARISON_DETAIL where Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD where Code='" + strCode + "'"
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

    Public Shared Function CheckNewEntry(ByVal Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = "select TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.CODE from TSPL_SCRAP_QUOTATION_COMPARISON_HEAD where TSPL_SCRAP_QUOTATION_COMPARISON_HEAD.CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class

Public Class ClsMaterialQuotationComparisonDeatil
#Region "Variables"
    Public Code As String = Nothing
    Public Line_No As Integer = 0
    Public QuotationOrder_Code As String = Nothing
    Public Customer_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0
    Public Unit_Code As String = Nothing
    Public Item_Cost As String = Nothing
    Public Item_Net_Amt As Double = 0
    Public Specification As String = Nothing
    Public Remarks As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strReqNo As String, ByVal Arr As List(Of ClsMaterialQuotationComparisonDeatil), ByVal trans As SqlTransaction, Optional ByVal import As Boolean = False) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsMaterialQuotationComparisonDeatil In Arr
                Dim coll As New Hashtable()
                If Not import Then
                    clsCommon.AddColumnsForChange(coll, "Code", strReqNo)
                Else
                    clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                End If

                clsCommon.AddColumnsForChange(coll, "Line_No", intLineNo)
                clsCommon.AddColumnsForChange(coll, "QuotationOrder_Code", obj.QuotationOrder_Code)
                clsCommon.AddColumnsForChange(coll, "Customer_Code", obj.Customer_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCRAP_QUOTATION_COMPARISON_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                intLineNo = intLineNo + 1
            Next
        End If
        Return True
    End Function
End Class
