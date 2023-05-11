Imports common
Imports System.Data.SqlClient
Public Class ClsVendorQuotationHead
#Region "Variables"
    Public Code As String = Nothing
    Public VQDate As DateTime = Nothing
    Public RFQ_NO As String = Nothing
    Public Requisition_Id As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Quotation_No As String = Nothing
    Public Quotation_Date As DateTime = Nothing
    Public Description As String = Nothing
    Public Ref_No As String = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public On_Hold As Boolean = False
    Public Status As ERPTransactionStatus = 0
    Public Posting_Date As DateTime? = Nothing
    Public Total_Amt As Double = 0
    Public ArrTr As List(Of ClsVendorQuotationDeatil) = Nothing

    Public Form_ID As String = ""
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region

    Public Function SaveData(ByVal obj As ClsVendorQuotationHead, ByVal isNewEntry As Boolean, Optional ByVal import As Boolean = False, Optional ByVal isMakeAbandomentNo As Boolean = False) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isMakeAbandomentNo = False Then
                If Not isNewEntry Then
                    Dim Status As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Status from TSPL_VENDOR_QUOTATION_HEAD Where Code='" + obj.Code + "'", trans))
                    If Status = 1 Then
                        Throw New Exception("This document is already posted.")
                    End If
                End If
            End If

            If Not import Then
                Dim qry As String = "delete from TSPL_Vendor_Quotation_DETAIL where Code='" + obj.Code + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If (isNewEntry) Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.VQDate, clsDocType.VendorQuotation, "", "")
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "VQDate", clsCommon.GetPrintDate(obj.VQDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Requisition_Id", obj.Requisition_Id)
            clsCommon.AddColumnsForChange(coll, "RFQ_NO", obj.RFQ_NO)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Quotation_No", obj.Quotation_No)
            clsCommon.AddColumnsForChange(coll, "Quotation_Date", clsCommon.GetPrintDate(obj.Quotation_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Ref_No", obj.Ref_No)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments)
            clsCommon.AddColumnsForChange(coll, "On_Hold", IIf(obj.On_Hold, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Total_Amt", obj.Total_Amt)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_QUOTATION_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_QUOTATION_HEAD", OMInsertOrUpdate.Update, "Code='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsVendorQuotationDeatil.SaveData(obj.Code, obj.ArrTr, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.Code, obj.arrCustomFields, trans)

            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType) As ClsVendorQuotationHead
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As common.NavigatorType, ByVal trans As SqlTransaction) As ClsVendorQuotationHead
        Dim obj As ClsVendorQuotationHead = Nothing
        Dim qry As String = "SELECT TSPL_Vendor_Quotation_HEAD.*,TSPL_VENDOR_MASTER.Vendor_Name FROM TSPL_Vendor_Quotation_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Vendor_Quotation_HEAD.Vendor_Code where  2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Vendor_Quotation_HEAD.Code=(select MIN(Code) from TSPL_Vendor_Quotation_HEAD  )"
            Case NavigatorType.Last
                qry += " and TSPL_Vendor_Quotation_HEAD.Code=(select Max(Code) from TSPL_Vendor_Quotation_HEAD  )"
            Case NavigatorType.Next
                qry += " and TSPL_Vendor_Quotation_HEAD.Code=(select Min(Code) from TSPL_Vendor_Quotation_HEAD where Code > '" + strCode + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_Vendor_Quotation_HEAD.Code=(select Max(Code) from TSPL_Vendor_Quotation_HEAD where Code < '" + strCode + "' )"
            Case NavigatorType.Current
                qry += " and TSPL_Vendor_Quotation_HEAD.Code='" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsVendorQuotationHead()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
            obj.VQDate = clsCommon.myCDate(dt.Rows(0)("VQDate"))
            obj.RFQ_NO = clsCommon.myCstr(dt.Rows(0)("RFQ_NO"))
            obj.Requisition_Id = clsCommon.myCstr(dt.Rows(0)("Requisition_Id"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Quotation_No = clsCommon.myCstr(dt.Rows(0)("Quotation_No"))
            obj.Quotation_Date = clsCommon.myCDate(dt.Rows(0)("Quotation_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Ref_No = clsCommon.myCstr(dt.Rows(0)("Ref_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Comments = clsCommon.myCstr(dt.Rows(0)("Comments"))
            obj.On_Hold = clsCommon.myCdbl(dt.Rows(0)("On_Hold"))
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

            qry = "SELECT TSPL_Vendor_Quotation_DETAIL.* FROM TSPL_Vendor_Quotation_DETAIL  where TSPL_Vendor_Quotation_DETAIL.Code='" + obj.Code + "' ORDER BY Line_No"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.ArrTr = New List(Of ClsVendorQuotationDeatil)
                Dim objTr As ClsVendorQuotationDeatil
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsVendorQuotationDeatil()
                    objTr.Code = clsCommon.myCstr(dr("Code"))
                    objTr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                    objTr.Row_Type = clsCommon.myCstr(dr("Row_Type"))
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
                Throw New Exception("Requistion No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As ClsVendorQuotationHead = ClsVendorQuotationHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            If (obj.On_Hold = 1) Then
                Throw New Exception("Vendor Quotation No " + obj.Code + " Is On Hold.Can't Post it")
            End If

            Dim qry As String = "Update TSPL_Vendor_Quotation_HEAD set Status=1, Posting_Date='" + strPostDate + "',Modify_By='" + objCommonVar.CurrentUserCode + "' where Code='" + strDocNo + "'"
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
            Throw New Exception("Requisition No not found to Delete")
        End If
        Dim obj As ClsVendorQuotationHead = ClsVendorQuotationHead.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Post on :" + obj.Posting_Date)
                End If
                Dim qry As String = "delete from TSPL_Vendor_Quotation_DETAIL where Code='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_Vendor_Quotation_HEAD where Code='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                isSaved = isSaved AndAlso clsCustomFieldValues.DeleteData(obj.Form_ID, obj.Code, trans)
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
        Dim qry As String = "select TSPL_VENDOR_QUOTATION_HEAD.CODE from TSPL_VENDOR_QUOTATION_HEAD where TSPL_VENDOR_QUOTATION_HEAD.CODE ='" + Code + "'   "
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If

    End Function

End Class


Public Class ClsVendorQuotationDeatil
#Region "Variables"
    Public Code As String = Nothing
    Public Line_No As Integer = 0
    Public Row_Type As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0
    Public Unit_Code As String = Nothing
    Public Item_Cost As String = Nothing
    Public Item_Net_Amt As Double = 0
    Public Specification As String = Nothing
    Public Remarks As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strReqNo As String, ByVal Arr As List(Of ClsVendorQuotationDeatil), ByVal trans As SqlTransaction, Optional ByVal import As Boolean = False) As Boolean
        Dim intLineNo As Integer = 1
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsVendorQuotationDeatil In Arr
                Dim coll As New Hashtable()
                If Not import Then
                    clsCommon.AddColumnsForChange(coll, "Code", strReqNo)
                Else
                    clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                End If
                clsCommon.AddColumnsForChange(coll, "Row_Type", obj.Row_Type)
                clsCommon.AddColumnsForChange(coll, "Line_No", intLineNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Item_Net_Amt", obj.Item_Net_Amt)
                clsCommon.AddColumnsForChange(coll, "Specification", obj.Specification)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_VENDOR_QUOTATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                intLineNo = intLineNo + 1
            Next
        End If
        Return True
    End Function
End Class