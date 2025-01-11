Imports System.Data.SqlClient
Imports common

Public Class clsMultipleShareAllotment

#Region "Variables"
    Public Document_No As String = Nothing
    Public Document_date As Date? = Nothing
    Public Rate_Of_One_Share As Decimal
    Public From_Date As Date? = Nothing
    Public To_Date As Date? = Nothing
    Public Status As Integer = 0
    Public Arr As List(Of clsMultipleShareAllotmentDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsMultipleShareAllotment, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, Nothing, trans, AutoSave)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsMultipleShareAllotment, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "delete from TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL where Document_No='" + obj.Document_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Rate_Of_One_Share", obj.Rate_Of_One_Share)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.MultipleShareAllotment, "", "")
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No='" + obj.Document_No + "'", trans)
            End If
            isSaved = isSaved AndAlso clsMultipleShareAllotmentDetail.SaveData(obj.Document_No, obj.Arr, trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType) As clsMultipleShareAllotment
        Return GetData(strRetCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMultipleShareAllotment
        Dim obj As clsMultipleShareAllotment = Nothing
        Dim qry As String = "select Document_No , Document_date,Rate_Of_One_Share  ,ISNULL( Status,0) as Status,From_Date,To_Date from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No = (select MIN(Document_No) from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD)"
            Case NavigatorType.Last
                qry += " and TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No = (select Max(Document_No) from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD)"
            Case NavigatorType.Next
                qry += " and TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No = (select Min(Document_No) from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD where Document_No >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No = (select Max(Document_No) from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD where Document_No <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD.Document_No = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsMultipleShareAllotment()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Rate_Of_One_Share = clsCommon.myCdbl(dt.Rows(0)("Rate_Of_One_Share"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.From_Date = clsCommon.myCDate(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCDate(dt.Rows(0)("To_Date"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.Arr = clsMultipleShareAllotmentDetail.GetData(obj.Document_No, trans)
        End If
        Return obj
    End Function

    Public Shared Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select Document_No as DocumentNo ,convert(varchar(12),Document_date,103) as DocumentDate,From_Date as [From Date],To_Date as [To Date],Rate_Of_One_Share as [Rate of One Share],case when Status = 1 then 'posted' else 'Unposted' end as Posted from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD"
        str = clsCommon.ShowSelectForm("HeadLoad", sql, "DocumentNo", "", strCode, "DocumentNo", isButtonClicked)
        Return str
    End Function


    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = isSaved AndAlso PostData(FormId, strDocNo, trans)

            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Docume nt No not found to Post")
            End If
            Dim obj As clsMultipleShareAllotment = clsMultipleShareAllotment.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD set Status= 1, Posted_By = '" + objCommonVar.CurrentUserCode + "',Posted_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "'  where Document_No='" & obj.Document_No & "'", trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim isResponse As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ReverseAndUnpost(strCode, trans) Then
                isResponse = True
            Else
                isResponse = False
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isResponse As Boolean = True
        Try

            Dim obj As clsMultipleShareAllotment = clsMultipleShareAllotment.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Status) <= 0) Then
                clsCommon.MyMessageBoxShow("No Data found to Reverse And UnPost")
                isResponse = False
            End If

            If Not obj.Status = 1 Then
                clsCommon.MyMessageBoxShow("Transaction status should be posted for reverse and unpost")
                isResponse = False
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 0)
            clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", Nothing, True)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isResponse
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
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As clsMultipleShareAllotment = clsMultipleShareAllotment.GetData(strCode, NavigatorType.Current, trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing
            qry = "delete from TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_MULTIPLE_SHARE_ALLOTMENT_HEAD where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsMultipleShareAllotmentDetail

#Region "Variables"
    Public Document_No As String = Nothing
    Public Vendor_Code As String = Nothing
    Public VLC_Code_VLC_Uploader As String = Nothing
    Public VLC_Name As String = Nothing
    Public Share_Opening_Amt As Decimal
    Public Share_Deducted_Amt As Decimal
    Public Balance_Amt As Decimal
    Public Rate_Per_Share As Decimal
    Public Allocated_Share As Decimal
    Public Share_Certificate_From As Decimal
    Public Share_Certificate_To As Decimal

#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsMultipleShareAllotmentDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsMultipleShareAllotmentDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", strCode)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Share_Opening_Amt", obj.Share_Opening_Amt)
                clsCommon.AddColumnsForChange(coll, "Share_Deducted_Amt", obj.Share_Deducted_Amt)
                clsCommon.AddColumnsForChange(coll, "Balance_Amt", obj.Balance_Amt)
                clsCommon.AddColumnsForChange(coll, "Rate_Per_Share", obj.Rate_Per_Share)
                clsCommon.AddColumnsForChange(coll, "Allocated_Share", obj.Allocated_Share)
                clsCommon.AddColumnsForChange(coll, "Share_Certificate_From", obj.Share_Certificate_From)
                clsCommon.AddColumnsForChange(coll, "Share_Certificate_To", obj.Share_Certificate_To)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsMultipleShareAllotmentDetail)
        Dim arr As List(Of clsMultipleShareAllotmentDetail) = Nothing
        Dim qry As String = "select TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.*, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader , TSPL_VLC_MASTER_HEAD.VLC_Name  
         from TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Vendor_Code
         left  join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC where  TSPL_MULTIPLE_SHARE_ALLOTMENT_DETAIL.Document_No = '" + strCode + "' order by Document_No "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMultipleShareAllotmentDetail)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsMultipleShareAllotmentDetail = New clsMultipleShareAllotmentDetail()
                obj.Document_No = clsCommon.myCstr(dr("Document_No"))
                obj.VLC_Code_VLC_Uploader = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                obj.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                obj.VLC_Name = clsCommon.myCstr(dr("VLC_Name"))
                obj.Share_Opening_Amt = clsCommon.myCdbl(dr("Share_Opening_Amt"))
                obj.Share_Deducted_Amt = clsCommon.myCdbl(dr("Share_Deducted_Amt"))
                obj.Balance_Amt = clsCommon.myCdbl(dr("Balance_Amt"))
                obj.Rate_Per_Share = clsCommon.myCdbl(dr("Rate_Per_Share"))
                obj.Allocated_Share = clsCommon.myCdbl(dr("Allocated_Share"))
                obj.Share_Certificate_From = clsCommon.myCdbl(dr("Share_Certificate_From"))
                obj.Share_Certificate_To = clsCommon.myCdbl(dr("Share_Certificate_To"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

End Class




