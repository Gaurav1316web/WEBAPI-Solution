Imports common
Imports System.Data.SqlClient
Public Class clsTenderPenalty
#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_Date As DateTime
    Public Tender_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public VendorName As String = Nothing
    Public Item_Code As String = Nothing
    Public ItemName As String = Nothing
    Public Location_Code As String = Nothing
    Public LocationName As String = Nothing
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Arr As ArrayList = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsTenderPenalty, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_TENDER_PENALTY_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim strDocNo As String = ""
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.TenderPenalty, "", obj.Location_Code)
            End If
            If (clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim ServerDate As DateTime = clsCommon.GETSERVERDATE(trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Tender_No", obj.Tender_No)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)

            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(ServerDate, "dd/MMM/yyyy hh:mm:ss tt"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)

                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(ServerDate, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_PENALTY", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_PENALTY", OMInsertOrUpdate.Update, "TSPL_TENDER_PENALTY.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_No), "TSPL_TENDER_PENALTY", "Document_No", "TSPL_TENDER_PENALTY_DETAIL", "Document_No", trans)

            clsTenderPenaltyDetail.SaveData(obj.Document_No, obj.Arr, trans)

            'If Not isNewEntry Then
            clsCommonFunctionality.SaveCancelData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_No), "TSPL_TENDER_PENALTY", "Document_No", "TSPL_TENDER_PENALTY_DETAIL", "Document_No", "TSPL_PI_REMITTANCE", "Document_No", trans)
            'End If
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsTenderPenalty
        Dim obj As New clsTenderPenalty
        Dim qry As String = "SELECT TSPL_TENDER_PENALTY.*,TSPL_LOCATION_MASTER.Location_Desc,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_ITEM_MASTER.Item_Desc
FROM TSPL_TENDER_PENALTY 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TENDER_PENALTY.Location_Code 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_TENDER_PENALTY.Vendor_Code 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_TENDER_PENALTY.Item_Code
where 2=2"
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " And Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " And TSPL_TENDER_PENALTY.Document_No = (select MIN(Document_No) from TSPL_TENDER_PENALTY WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " And TSPL_TENDER_PENALTY.Document_No = (select Max(Document_No) from TSPL_TENDER_PENALTY WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " And TSPL_TENDER_PENALTY.Document_No = '" + strPONo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_TENDER_PENALTY.Document_No = (select Min(Document_No) from TSPL_TENDER_PENALTY where Document_No>'" + strPONo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_TENDER_PENALTY.Document_No = (select Max(Document_No) from TSPL_TENDER_PENALTY where Document_No<'" + strPONo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.LocationName = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))

            obj.Tender_No = clsCommon.myCstr(dt.Rows(0)("Tender_No"))
            obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
            obj.VendorName = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.ItemName = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            qry = "SELECT TSPL_TENDER_PENALTY_DETAIL.SRN_No FROM TSPL_TENDER_PENALTY_DETAIL  where TSPL_TENDER_PENALTY_DETAIL.Document_No='" + obj.Document_No + "' ORDER BY TSPL_TENDER_PENALTY_DETAIL.PK_Id"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New ArrayList
                For Each dr As DataRow In dt.Rows
                    obj.Arr.Add(clsCommon.myCstr(dr("SRN_No")))
                Next
            End If
        End If
        Return obj
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim OpenPOforRejectShortageQty As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.OpenPOforRejectShortageQty, clsFixedParameterCode.OpenPOforRejectShortageQty, trans)) = "1", True, False))
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsTenderPenalty = clsTenderPenalty.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase", "Store receipt Note", obj.Location_Code, obj.Document_Date, trans)
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = "Update TSPL_TENDER_PENALTY set Status=1,Post_Date='" + clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt") + "',Post_By='" + objCommonVar.CurrentUserCode + "' where Document_No='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CancelData(ByVal strCode As String) As Boolean

        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Cancel")
        End If
        Dim obj As clsTenderPenalty = clsTenderPenalty.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase", "Store receipt Note", obj.Location_Code, obj.Document_Date, trans)
                'If (obj.Status = 1) Then
                '    Throw New Exception("Already Posted Docuemnt [" + obj.Document_No + "]")
                'End If
                Dim qry As String = ""

                DeleteSRNDeduction(obj.Arr, obj.Item_Code, True, True, True, trans)

                qry = "delete from TSPL_TENDER_PENALTY_DETAIL where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_TENDER_PENALTY where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean

        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsTenderPenalty = clsTenderPenalty.GetData(strCode, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
            Try
                'clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Purchase", "Store receipt Note", obj.Location_Code, obj.Document_Date, trans)
                If (obj.Status = 1) Then
                    Throw New Exception("Already Posted Docuemnt [" + obj.Document_No + "]")
                End If
                Dim qry As String = ""

                DeleteSRNDeduction(obj.Arr, obj.Item_Code, True, True, True, trans)

                qry = "delete from TSPL_TENDER_PENALTY_DETAIL where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_TENDER_PENALTY where Document_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function
    Public Shared Function CheckRALPenaltyUsedPI(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        'Dim qry As String = "select sum(fin.[cnt]) from (SELECT 1 as [cnt] from TSPL_SRN_DETAIL where TSPL_SRN_DETAIL.PO_ID ='" + clsCommon.myCstr(strPONo) + "' union all SELECT 1 as [cnt] from TSPL_GRN_DETAIL where TSPL_GRN_DETAIL.PO_ID ='" + clsCommon.myCstr(strPONo) + "')fin"
        Dim qry As String = " select sum(fin.[cnt]) FROM (select 1 as [cnt],TSPL_PI_DETAIL.PI_No,TSPL_TENDER_PENALTY_DETAIL.SRN_No from TSPL_TENDER_PENALTY_DETAIL 
                              inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_TENDER_PENALTY_DETAIL.SRN_No  
                              where TSPL_TENDER_PENALTY_DETAIL.Document_No='" + strCode + "')fin"
        Dim count As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        If count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select Status from TSPL_TENDER_PENALTY where Document_No='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("Document No [" + strCode + "] not found for reverse and unpost")
        End If

        If Not clsCommon.myCdbl(dt.Rows(0)("Status")) = 1 Then
            Throw New Exception("Transaction status should be posted for reverse and unpost")
        End If

        qry = "select TSPL_PI_DETAIL.PI_No,TSPL_TENDER_PENALTY_DETAIL.SRN_No from TSPL_TENDER_PENALTY_DETAIL 
inner join TSPL_PI_DETAIL on TSPL_PI_DETAIL.SRN_Id=TSPL_TENDER_PENALTY_DETAIL.SRN_No  
where TSPL_TENDER_PENALTY_DETAIL.Document_No='" + strCode + "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("Purchase Invoice No [" + clsCommon.myCstr(dt.Rows(0)("PI_No")) + "] is Generated Against SRN No [" + clsCommon.myCstr(dt.Rows(0)("SRN_No")) + "] ")
        End If

        qry = "select Document_No from TSPL_TENDER_PENALTY 
where exists(select 1 from TSPL_TENDER_PENALTY as TabCurr where TabCurr.Document_No='" + strCode + "' and TabCurr.Location_Code=TSPL_TENDER_PENALTY.Location_Code and TabCurr.Tender_No=TSPL_TENDER_PENALTY.Tender_No and TabCurr.Vendor_Code=TSPL_TENDER_PENALTY.Vendor_Code and TabCurr.Item_Code=TSPL_TENDER_PENALTY.Item_Code and TabCurr.Created_Date< TSPL_TENDER_PENALTY.Created_Date)"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("Please remove Tender Penalty Docuemnt [" + clsCommon.myCstr(dt.Rows(0)("Document_No")) + "] to unpost it")
        End If

        qry = "Update TSPL_TENDER_PENALTY set Status = 0 where Document_No='" + strCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_TENDER_PENALTY", "Document_No", trans)
        trans.Commit()
        Catch ex As Exception
        trans.Rollback()
        Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteSRNDeduction(ByVal ArrSRNNo As ArrayList, ByVal strICode As String, ByVal ApplyQCDed As Boolean, ByVal ApplySecDed As Boolean, ByVal ApplyRALPenalty As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            If ArrSRNNo IsNot Nothing AndAlso ArrSRNNo.Count > 0 Then
                Dim qry As String = ""
                If ApplyQCDed Then
                    qry = "delete From TSPL_SRN_DEDUCTION  Where SRN_No in (" + clsCommon.GetMulcallString(ArrSRNNo) + ") and Item_Code='" + strICode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                If ApplySecDed Then
                    qry = "delete From TSPL_SRN_DEDUCTION_SECURITY  Where SRN_No in (" + clsCommon.GetMulcallString(ArrSRNNo) + ") and Item_Code='" + strICode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                If ApplyRALPenalty Then
                    qry = "delete From TSPL_SRN_TENDER_CALC  Where SRN_No in  (" + clsCommon.GetMulcallString(ArrSRNNo) + ") and Item_Code='" + strICode + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsTenderPenaltyDetail
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each str As String In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "SRN_No", str)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_PENALTY_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
