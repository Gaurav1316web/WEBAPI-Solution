Imports System.Data.SqlClient
Imports System.IO

Public Class clsRMDemandApproval
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As DateTime = Nothing
    Public From_Date As DateTime = Nothing
    Public To_Date As DateTime = Nothing
    Public Remarks As String = Nothing
    Public Comment As String = Nothing

    Public Created_By As String = Nothing
    Public Created_Date As DateTime = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime = Nothing
    Public Posting_Date As DateTime?
    Public Posted_By As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Arr As List(Of clsRMDemandApprovalItemLocation) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsRMDemandApproval, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsRMDemandApproval, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try

            Dim qry As String = "delete from TSPL_RM_DEMAND_APPROVAL_INDENT where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.RMDemandApproval, "", "")
                If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy  hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RM_DEMAND_APPROVAL", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RM_DEMAND_APPROVAL", OMInsertOrUpdate.Update, "TSPL_RM_DEMAND_APPROVAL.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsRMDemandApprovalItemLocation.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_Code), "TSPL_RM_DEMAND_APPROVAL", "Document_Code", "TSPL_RM_DEMAND_APPROVAL_INDENT", "Document_Code", "TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION", "Document_Code", trans)


        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetData(ByVal strDocumentCode As String, ByVal NavType As NavigatorType) As clsRMDemandApproval
        Try
            Return GetData(strDocumentCode, NavType, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocumentCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsRMDemandApproval
        Dim obj As clsRMDemandApproval = Nothing
        Try
            Dim qry As String = "SELECT TSPL_RM_DEMAND_APPROVAL.* from TSPL_RM_DEMAND_APPROVAL where 2=2"
            Dim WhrCls As String = ""
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_RM_DEMAND_APPROVAL.Document_Code = (select MIN(TSPL_RM_DEMAND_APPROVAL.Document_Code) from TSPL_RM_DEMAND_APPROVAL Where 1=1 " + WhrCls + ")"
                Case NavigatorType.Last
                    qry += " and TSPL_RM_DEMAND_APPROVAL.Document_Code = (select Max(TSPL_RM_DEMAND_APPROVAL.Document_Code) from TSPL_RM_DEMAND_APPROVAL Where 1=1 " + WhrCls + ")"
                Case NavigatorType.Next
                    qry += " and TSPL_RM_DEMAND_APPROVAL.Document_Code = (select Min(TSPL_RM_DEMAND_APPROVAL.Document_Code) from TSPL_RM_DEMAND_APPROVAL where Document_Code>'" + strDocumentCode + "' " + WhrCls + ")"
                Case NavigatorType.Previous
                    qry += " and TSPL_RM_DEMAND_APPROVAL.Document_Code = (select Max(TSPL_RM_DEMAND_APPROVAL.Document_Code) from TSPL_RM_DEMAND_APPROVAL where Document_Code<'" + strDocumentCode + "' " + WhrCls + ")"
                Case NavigatorType.Current
                    qry += " and TSPL_RM_DEMAND_APPROVAL.Document_Code = '" + strDocumentCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsRMDemandApproval
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
                obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Comment = clsCommon.myCstr(dt.Rows(0)("Comment"))
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
                obj.Created_Date = clsCommon.myCstr(dt.Rows(0)("Created_Date"))
                obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
                obj.Modified_Date = clsCommon.myCstr(dt.Rows(0)("Modified_Date"))
                If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                    obj.Posting_Date = clsCommon.myCstr(dt.Rows(0)("Posting_Date"))
                    obj.Posted_By = clsCommon.myCstr(dt.Rows(0)("Posted_By"))
                End If
                obj.Arr = clsRMDemandApprovalItemLocation.GetData(obj.Document_Code, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Tender No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy  hh:mm tt")

            Dim obj As clsRMDemandApproval = clsRMDemandApproval.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_RM_DEMAND_APPROVAL set Status=1, Posting_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function



    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Tender not found to Delete")
        End If
        Dim obj As clsRMDemandApproval = clsRMDemandApproval.GetData(strCode, NavigatorType.Current, trans)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                If (obj.Status = ERPTransactionStatus.Approved) Then
                    Throw New Exception("Already Posted on :" + obj.Posting_Date)
                End If

                Dim qry As String = "delete from TSPL_RM_DEMAND_APPROVAL_INDENT where Document_Code='" + obj.Document_Code + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_RM_DEMAND_APPROVAL where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return True
    End Function


    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "select 1 from TSPL_RM_DEMAND_APPROVAL where Document_Code='" + strCode + "' and Status=1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Transaction status should be posted.")
            End If

            qry = "update TSPL_RM_DEMAND_APPROVAL set Status=0,Posting_Date=null where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = "select TSPL_RM_DEMAND_APPROVAL.Document_Code as DocumentNo,convert(varchar(12),TSPL_RM_DEMAND_APPROVAL.Document_Date,103) as DocumentDate,case when isnull(TSPL_RM_DEMAND_APPROVAL.Status,0)=1 then 'Posted' else 'Pending' end as Status from TSPL_RM_DEMAND_APPROVAL"
        Dim str As String = clsCommon.ShowSelectForm("RMDemApp", qry, "DocumentNo", whrcls, curcode, "DocumentNo    ", isButtonClicked)
        Return str
    End Function


End Class

Public Class clsRMDemandApprovalItemLocation
#Region "Variables"
    Public Document_Code As String = Nothing
    Public TRNo As Integer
    Public Location As String = Nothing
    Public Location_Name As String = Nothing ''Not a TAble column
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing ''Not a TAble column
    Public UOM As String = Nothing
    Public Qty_Indent As Decimal = 0
    Public Qty_Stock As Decimal = 0
    Public Qty_Approve As Decimal = 0

    Public ArrIndent As ArrayList = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsRMDemandApprovalItemLocation), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsRMDemandApprovalItemLocation In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Qty_Indent", obj.Qty_Indent)
                clsCommon.AddColumnsForChange(coll, "Qty_Stock", obj.Qty_Stock)
                clsCommon.AddColumnsForChange(coll, "Qty_Approve", obj.Qty_Approve)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION", OMInsertOrUpdate.Insert, "", trans)

                obj.TRNo = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select MAX(TRNo) from TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION where Document_Code='" + strDocNo + "'", trans))
                clsRMDemandApprovalItemIndent.SaveData(strDocNo, obj.TRNo, obj.ArrIndent, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsRMDemandApprovalItemLocation)
        Dim arr As List(Of clsRMDemandApprovalItemLocation) = Nothing
        Dim qry As String = "SELECT TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.*,TSPL_LOCATION_MASTER.Location_Desc
                    , TSPL_ITEM_MASTER.Item_Desc
                    FROM TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Location 
                    left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Item_Code
                    where TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Document_Code='" + strDocNo + "' ORDER BY TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.TRNo"
        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsRMDemandApprovalItemLocation)
            Dim objTr As clsRMDemandApprovalItemLocation
            For Each dr As DataRow In dt.Rows
                objTr = New clsRMDemandApprovalItemLocation
                objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                objTr.TRNo = clsCommon.myCDecimal(dr("TRNo"))
                objTr.Location = clsCommon.myCstr(dr("Location"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.Qty_Indent = clsCommon.myCdbl(dr("Qty_Indent"))
                objTr.Qty_Stock = clsCommon.myCdbl(dr("Qty_Stock"))
                objTr.Qty_Approve = clsCommon.myCdbl(dr("Qty_Approve"))

                objTr.ArrIndent = clsRMDemandApprovalItemIndent.GetData(strDocNo, objTr.TRNo, trans)
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsRMDemandApprovalItemIndent
#Region "Variables"
    Public Document_Code As String = Nothing
    Public TRNo As Integer
    Public Against_TRNo As Integer
    Public Against_Requisition As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal AgainstTRNo As Integer, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each strIndent As String In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Against_TRNo", AgainstTRNo)
                clsCommon.AddColumnsForChange(coll, "Against_Requisition", strIndent)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_RM_DEMAND_APPROVAL_INDENT", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal AgainstTRNo As Integer, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "select TSPL_RM_DEMAND_APPROVAL_INDENT.Against_Requisition from TSPL_RM_DEMAND_APPROVAL_INDENT  where TSPL_RM_DEMAND_APPROVAL_INDENT.Document_Code='" + clsCommon.myCstr(strDocNo) + "' and Against_TRNo=" + clsCommon.myCstr(AgainstTRNo) + " order by TSPL_RM_DEMAND_APPROVAL_INDENT.TRNo"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList()
            For ii As Integer = 0 To dt.Rows.Count - 1
                arr.Add(clsCommon.myCstr(dt.Rows(ii)("Against_Requisition")))
            Next
        End If
        Return arr
    End Function

End Class