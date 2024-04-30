Imports System.Data.SqlClient
Imports common

Public Class clsInsuranceTagAllocation

#Region "Variables"

    Public Document_Code As String = Nothing
    Public Document_date As DateTime? = Nothing
    Public Status As Integer = 0
    Public Bull_Code As String = Nothing
    Public Tag_No As String = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsInsuranceTagAllocation, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal AutoSave As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsInsuranceTagAllocation, ByVal isNewEntry As Boolean, ByVal strTransType As String, ByVal trans As SqlTransaction, ByVal AutoSave As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_date", clsCommon.GetPrintDate(obj.Document_date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Bull_Code", clsCommon.myCstr(obj.Bull_Code))
            clsCommon.AddColumnsForChange(coll, "Tag_No", clsCommon.myCstr(obj.Tag_No))

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                If clsCommon.CompairString(AutoSave, False) = CompairStringResult.Equal Then
                    obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_date, clsDocType.InsuranceTagAllocation, "", "")
                End If
                If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INS_TAG_ALLOCATION", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INS_TAG_ALLOCATION", OMInsertOrUpdate.Update, "TSPL_INS_TAG_ALLOCATION.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_INS_TAG_ALLOCATION", "Document_Code", trans)

        Catch err As Exception

            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strRetCode As String, ByVal NavType As NavigatorType, ByVal TransType As String) As clsInsuranceTagAllocation
        Return GetData(strRetCode, NavType, TransType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal TransType As String, ByVal trans As SqlTransaction) As clsInsuranceTagAllocation
        Dim obj As clsInsuranceTagAllocation = Nothing
        Dim qry As String = "select TSPL_INS_TAG_ALLOCATION.Document_Code,Bull_Code,TSPL_INS_TAG_ALLOCATION.Document_date,TSPL_BULL_INSURANCE_TAG.Tag_No,ISNULL( Status,0) as Status 
        from TSPL_INS_TAG_ALLOCATION left outer join TSPL_BULL_INSURANCE_TAG on TSPL_BULL_INSURANCE_TAG.PK_Id = TSPL_INS_TAG_ALLOCATION.Tag_No   where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_INS_TAG_ALLOCATION.Document_Code = (select MIN(Document_Code) from TSPL_INS_TAG_ALLOCATION)"
            Case NavigatorType.Last
                qry += " and TSPL_INS_TAG_ALLOCATION.Document_Code = (select Max(Document_Code) from TSPL_INS_TAG_ALLOCATION)"
            Case NavigatorType.Next
                qry += " and TSPL_INS_TAG_ALLOCATION.Document_Code = (select Min(Document_Code) from TSPL_INS_TAG_ALLOCATION where Document_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_INS_TAG_ALLOCATION.Document_Code = (select Max(Document_Code) from TSPL_INS_TAG_ALLOCATION where Document_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_INS_TAG_ALLOCATION.Document_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            obj = New clsInsuranceTagAllocation()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Bull_Code = clsCommon.myCstr(dt.Rows(0)("Bull_Code"))
            obj.Document_date = clsCommon.myCDate(dt.Rows(0)("Document_date"))
            obj.Tag_No = clsCommon.myCdbl(dt.Rows(0)("Tag_No"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj
    End Function

    Public Shared Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select TSPL_INS_TAG_ALLOCATION.Document_Code as DocumentCode,Bull_Code as [Bull Code],convert(varchar(12),TSPL_INS_TAG_ALLOCATION.Document_date,103) as [Document Date],TSPL_BULL_INSURANCE_TAG.Tag_No as [Tag No],case when Status = 1 then 'Posted' else 'Unposted' end as Posted from TSPL_INS_TAG_ALLOCATION
        left outer join TSPL_BULL_INSURANCE_TAG on TSPL_BULL_INSURANCE_TAG.PK_Id = TSPL_INS_TAG_ALLOCATION.Tag_No "
        str = clsCommon.ShowSelectForm("InsuranceTagAllocation", sql, "DocumentCode", "", strCode, "DocumentCode", isButtonClicked)
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
                Throw New Exception("Document No not found to Post")
            End If
            Dim obj As clsInsuranceTagAllocation = clsInsuranceTagAllocation.GetData(strDocNo, NavigatorType.Current, "", trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = 1) Then
                Throw New Exception("Already Posted")
            End If

            clsDBFuncationality.ExecuteNonQuery("Update TSPL_INS_TAG_ALLOCATION set Status= 1, Posted_By = '" + objCommonVar.CurrentUserCode + "',Posted_Date = '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt") + "'  where Document_Code='" & obj.Document_Code & "'", trans)

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

            Dim obj As clsInsuranceTagAllocation = clsInsuranceTagAllocation.GetData(strCode, NavigatorType.Current, Nothing, trans)
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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INS_TAG_ALLOCATION", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_Code + "'", trans)

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
        Dim obj As clsInsuranceTagAllocation = clsInsuranceTagAllocation.GetData(strCode, NavigatorType.Current, "", trans)
        Try
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            If clsCommon.CompairString(obj.Status, "1") = CompairStringResult.Equal Then
                Throw New Exception("Already Posted")
            End If
            Dim qry As String = Nothing
            qry = "delete from TSPL_INS_TAG_ALLOCATION where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class