Imports System.Data.SqlClient
Imports common
Public Class clsDBTNEFTReject
#Region "variables"
    Public Document_Code As String = Nothing
    Public Document_Date As Date
    Public Against_DBT_NEFT As String
    Public Remarks As String = ""
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As Date? = Nothing
    Public arr As List(Of clsDBTNEFTRejectDetail) = Nothing
    Public arrSuccess As List(Of clsDBTNEFTRejectSucess) = Nothing

    'Public ArrDt As DataTable
#End Region
    Public Shared Function SaveData(ByVal obj As clsDBTNEFTReject, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from TSPL_DBT_NEFT_REJECT_SUCESS where Document_Code='" & obj.Document_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_DBT_NEFT_REJECT_DETAIL where Document_Code='" & obj.Document_Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Against_DBT_NEFT", obj.Against_DBT_NEFT)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DBTNEFTReject, "", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DBT_NEFT_REJECT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DBT_NEFT_REJECT", OMInsertOrUpdate.Update, "TSPL_DBT_NEFT_REJECT.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsDBTNEFTRejectDetail.saveData(obj.arr, obj.Document_Code, trans)
            clsDBTNEFTRejectSucess.saveData(obj.arrSuccess, obj.Document_Code, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_DBT_NEFT_REJECT", "Document_Code", "TSPL_DBT_NEFT_REJECT_DETAIL", "Document_Code", "TSPL_DBT_NEFT_REJECT_SUCESS", "Document_Code", trans)

            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsDBTNEFTReject
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDBTNEFTReject
        Dim obj As clsDBTNEFTReject = Nothing
        Dim Arr As List(Of clsDBTNEFTReject) = Nothing
        Dim qry As String = "Select TSPL_DBT_NEFT_REJECT.* from TSPL_DBT_NEFT_REJECT where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DBT_NEFT_REJECT.Document_Code = (select MIN(Document_Code) from TSPL_DBT_NEFT_REJECT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_DBT_NEFT_REJECT.Document_Code = (select Max(Document_Code) from TSPL_DBT_NEFT_REJECT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_DBT_NEFT_REJECT.Document_Code = '" + strCode + "' "
            Case NavigatorType.Next
                qry += " and TSPL_DBT_NEFT_REJECT.Document_Code = (select Min(Document_Code) from TSPL_DBT_NEFT_REJECT where Document_Code>'" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DBT_NEFT_REJECT.Document_Code = (select Max(Document_Code) from TSPL_DBT_NEFT_REJECT where Document_Code<'" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDBTNEFTReject()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Against_DBT_NEFT = clsCommon.myCstr(dt.Rows(0)("Against_DBT_NEFT"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If

            ''obj.arr = clsDBTNEFTRejectDetail.getData(obj.Document_Code, trans)
        End If
        Return obj
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Try
            Dim qry As String = ""
            qry = "delete from TSPL_DBT_NEFT_REJECT_DETAIL where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DBT_NEFT_REJECT_SUCESS where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DBT_NEFT_REJECT where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = "Select TSPL_DBT_NEFT_REJECT.Document_Code as Code,Convert(varchar,TSPL_DBT_NEFT_REJECT.Document_Date,103) as Date
          ,TSPL_DBT_NEFT_REJECT.Remarks as [Remarks],TSPL_DBT_NEFT_REJECT.Against_DBT_NEFT as [DBT NEFT No], Convert(varchar,TSPL_DBT_NEFT.From_Date,103) as [From Date],Convert(varchar,TSPL_DBT_NEFT.To_Date,103) as [To Date]
          ,case when isnull(TSPL_DBT_NEFT_REJECT.Status,0)=0 then 'Pending' else 'Approved' end as Status 
          from TSPL_DBT_NEFT_REJECT left outer join TSPL_DBT_NEFT on TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_REJECT.Against_DBT_NEFT"
        str = clsCommon.ShowSelectForm("DPTRNft#F", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

            Dim obj As clsDBTNEFTReject = clsDBTNEFTReject.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + obj.Posted_Date)
            End If
            Dim qry As String = "Update TSPL_DBT_NEFT_REJECT set Status=1, Posted_Date='" + strPostDate + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_Code='" + strDocNo + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsDBTNEFTRejectDetail
#Region "Variable"
    Public PK_Id As Integer
    Public Document_Code As String = Nothing
    Public Against_DBT_NEFT_TR As Integer
    Public Remarks As String


    ''Not  table columns
    Public Rem_Account_No As String = Nothing
    Public Rem_Name As String = Nothing
    Public VLC_Uploader_Code As String = Nothing
    Public MP_Uploader_Code As String = Nothing
    Public Amount As Decimal = 0
    Public MP_IFSC_No As String = Nothing
    Public MP_Account_No As String = Nothing
    Public MP_Name As String = Nothing
    Public Transaction As String = Nothing
    ''End of Not table columns
#End Region
    Public Shared Function saveData(ByVal arrObj As List(Of clsDBTNEFTRejectDetail), ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsDBTNEFTRejectDetail In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Against_DBT_NEFT_TR", obj.Against_DBT_NEFT_TR)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DBT_NEFT_REJECT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    '    Public Shared Function getData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsDBTNEFTRejectDetail)
    '        Try
    '            Dim arrObj As List(Of clsDBTNEFTRejectDetail) = Nothing
    '            Dim obj As clsDBTNEFTRejectDetail = Nothing
    '            Dim qry As String = "Select TSPL_DBT_NEFT_REJECT_DETAIL.*,TSPL_DBT_NEFT_DETAIL.Rem_Account_No,TSPL_DBT_NEFT_DETAIL.Rem_Name,TSPL_DBT_NEFT_DETAIL.VLC_Uploader_Code,TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code,TSPL_DBT_NEFT_DETAIL.Amount,TSPL_DBT_NEFT_DETAIL.MP_IFSC_No,TSPL_DBT_NEFT_DETAIL.MP_Account_No,TSPL_DBT_NEFT_DETAIL.MP_Name,TSPL_DBT_NEFT_DETAIL.[Transaction]
    'from TSPL_DBT_NEFT_REJECT_DETAIL 
    'right join TSPL_DBT_NEFT_DETAIL on TSPL_DBT_NEFT_DETAIL.PK_Id=TSPL_DBT_NEFT_REJECT_DETAIL.Against_DBT_NEFT_TR
    'where TSPL_DBT_NEFT_REJECT_DETAIL.Document_Code='" & strDocNo & "'"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                arrObj = New List(Of clsDBTNEFTRejectDetail)
    '                For i As Integer = 0 To dt.Rows.Count - 1
    '                    obj = New clsDBTNEFTRejectDetail()
    '                    obj.PK_Id = clsCommon.myCdbl(dt.Rows(i)("PK_Id"))
    '                    obj.Document_Code = clsCommon.myCstr(dt.Rows(i)("Document_Code"))
    '                    obj.Against_DBT_NEFT_TR = clsCommon.myCdbl(dt.Rows(i)("Against_DBT_NEFT_TR"))
    '                    obj.Remarks = clsCommon.myCstr(dt.Rows(i)("Remarks"))


    '                    obj.Rem_Account_No = clsCommon.myCstr(dt.Rows(i)("Rem_Account_No"))
    '                    obj.Rem_Name = clsCommon.myCstr(dt.Rows(i)("Rem_Name"))
    '                    obj.VLC_Uploader_Code = clsCommon.myCstr(dt.Rows(i)("VLC_Uploader_Code"))
    '                    obj.MP_Uploader_Code = clsCommon.myCstr(dt.Rows(i)("MP_Uploader_Code"))
    '                    obj.MP_IFSC_No = clsCommon.myCstr(dt.Rows(i)("MP_IFSC_No"))
    '                    obj.MP_Account_No = clsCommon.myCstr(dt.Rows(i)("MP_Account_No"))
    '                    obj.MP_Name = clsCommon.myCstr(dt.Rows(i)("MP_Name"))
    '                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
    '                    obj.Transaction = clsCommon.myCstr(dt.Rows(i)("Transaction"))
    '                    arrObj.Add(obj)
    '                Next
    '            End If
    '            Return arrObj
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Function
End Class

Public Class clsDBTNEFTRejectSucess
#Region "Variable"
    Public PK_Id As Integer
    Public Document_Code As String = Nothing
    Public Against_DBT_NEFT_TR As Integer
    Public Remarks As String


    ''Not  table columns
    Public Rem_Account_No As String = Nothing
    Public Rem_Name As String = Nothing
    Public VLC_Uploader_Code As String = Nothing
    Public MP_Uploader_Code As String = Nothing
    Public Amount As Decimal = 0
    Public MP_IFSC_No As String = Nothing
    Public MP_Account_No As String = Nothing
    Public MP_Name As String = Nothing
    Public Transaction As String = Nothing
    ''End of Not table columns
#End Region
    Public Shared Function saveData(ByVal arrObj As List(Of clsDBTNEFTRejectSucess), ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As Hashtable
            If arrObj IsNot Nothing Then
                For Each obj As clsDBTNEFTRejectSucess In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Against_DBT_NEFT_TR", obj.Against_DBT_NEFT_TR)
                    clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DBT_NEFT_REJECT_SUCESS", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    '    Public Shared Function getData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsDBTNEFTRejectSucess)
    '        Try
    '            Dim arrObj As List(Of clsDBTNEFTRejectSucess) = Nothing
    '            Dim obj As clsDBTNEFTRejectSucess = Nothing
    '            Dim qry As String = "Select TSPL_DBT_NEFT_REJECT_SUCESS.*,TSPL_DBT_NEFT_DETAIL.Rem_Account_No,TSPL_DBT_NEFT_DETAIL.Rem_Name,TSPL_DBT_NEFT_DETAIL.VLC_Uploader_Code,TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code,TSPL_DBT_NEFT_DETAIL.Amount,TSPL_DBT_NEFT_DETAIL.MP_IFSC_No,TSPL_DBT_NEFT_DETAIL.MP_Account_No,TSPL_DBT_NEFT_DETAIL.MP_Name,TSPL_DBT_NEFT_DETAIL.[Transaction]
    'from TSPL_DBT_NEFT_REJECT_SUCESS 
    'right join TSPL_DBT_NEFT_DETAIL on TSPL_DBT_NEFT_DETAIL.PK_Id=TSPL_DBT_NEFT_REJECT_SUCESS.Against_DBT_NEFT_TR
    'where TSPL_DBT_NEFT_REJECT_SUCESS.Document_Code='" & strDocNo & "'"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                arrObj = New List(Of clsDBTNEFTRejectSucess)
    '                For i As Integer = 0 To dt.Rows.Count - 1
    '                    obj = New clsDBTNEFTRejectSucess()
    '                    obj.PK_Id = clsCommon.myCdbl(dt.Rows(i)("PK_Id"))
    '                    obj.Document_Code = clsCommon.myCstr(dt.Rows(i)("Document_Code"))
    '                    obj.Against_DBT_NEFT_TR = clsCommon.myCdbl(dt.Rows(i)("Against_DBT_NEFT_TR"))
    '                    obj.Remarks = clsCommon.myCstr(dt.Rows(i)("Remarks"))


    '                    obj.Rem_Account_No = clsCommon.myCstr(dt.Rows(i)("Rem_Account_No"))
    '                    obj.Rem_Name = clsCommon.myCstr(dt.Rows(i)("Rem_Name"))
    '                    obj.VLC_Uploader_Code = clsCommon.myCstr(dt.Rows(i)("VLC_Uploader_Code"))
    '                    obj.MP_Uploader_Code = clsCommon.myCstr(dt.Rows(i)("MP_Uploader_Code"))
    '                    obj.MP_IFSC_No = clsCommon.myCstr(dt.Rows(i)("MP_IFSC_No"))
    '                    obj.MP_Account_No = clsCommon.myCstr(dt.Rows(i)("MP_Account_No"))
    '                    obj.MP_Name = clsCommon.myCstr(dt.Rows(i)("MP_Name"))
    '                    obj.Amount = clsCommon.myCdbl(dt.Rows(i)("Amount"))
    '                    obj.Transaction = clsCommon.myCstr(dt.Rows(i)("Transaction"))
    '                    arrObj.Add(obj)
    '                Next
    '            End If
    '            Return arrObj
    '        Catch ex As Exception
    '            Throw New Exception(ex.Message)
    '        End Try
    '    End Function
End Class