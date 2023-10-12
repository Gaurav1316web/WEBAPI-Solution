Imports System.Data.SqlClient

Public Class ClsShareMaster

#Region "Variables"
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public IDate As DateTime
    Public CertificateFrom As String = Nothing
    Public CertificateTo As String = Nothing
    Public Shares As Integer = 0
    Public Rate As Double = 0
    Public Amount As Double = 0
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modify_By As String = Nothing
    Public Modify_Date As DateTime
    Public Post_By As String = Nothing
    Public Post_Date As DateTime
    Public Arr As List(Of ClsShareMovement) = Nothing


#End Region

    Public Function SaveData(ByVal obj As ClsShareMaster, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As ClsShareMaster, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim StrQry As String = "delete from TSPL_SHARE_MOVEMENT where Share_Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Range_From", obj.CertificateFrom)
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Shares)
            clsCommon.AddColumnsForChange(coll, "Range_To", obj.CertificateTo)
            clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "IDate", obj.IDate)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.ShareMaster, "", objCommonVar.strCurrUserLocations)
                If clsCommon.myLen(obj.Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHARE_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHARE_MASTER", OMInsertOrUpdate.Update, "TSPL_SHARE_MASTER.Code='" + obj.Code + "'", trans)
            End If

            'IsSaved = IsSaved AndAlso clsNotificationDetails.SaveData(obj.Code, obj.Arr, trans)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsShareMaster
        Dim obj As ClsShareMaster = Nothing

        Try
            Dim strQry As String = "select * from TSPL_SHARE_MASTER where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Code = (select MIN(Code) from TSPL_SHARE_MASTER where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Code = (Select Max(Code) from TSPL_SHARE_MASTER where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Code = (Select Min(Code) from TSPL_SHARE_MASTER where Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Code = (select Max(Code) from TSPL_SHARE_MASTER where Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsShareMaster()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.IDate = clsCommon.myCstr(dt.Rows(0)("IDate"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
                obj.CertificateFrom = clsCommon.myCstr(dt.Rows(0)("Range_From"))
                obj.CertificateTo = clsCommon.myCstr(dt.Rows(0)("Range_To"))
                obj.Shares = clsCommon.myCstr(dt.Rows(0)("Qty"))
                obj.Rate = clsCommon.myCstr(dt.Rows(0)("Rate"))
                obj.Amount = clsCommon.myCstr(dt.Rows(0)("Amount"))
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                ' obj.Arr = clsNotificationDetails.GetData(obj.Code, trans)
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function


    Public Shared Function DeleteData(ByVal StrCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = DeleteData(StrCode, trans)
            trans.Commit()
            Return isSaved
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal StrCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If (clsCommon.myLen(StrCode) <= 0) Then
                Throw New Exception("Code No. not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_SHARE_MOVEMENT where Share_Code='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHARE_MASTER where Code='" + StrCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
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
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As ClsShareMaster = ClsShareMaster.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Code : " + strDocNo + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Post_Date)
            End If


            Dim Arr As New List(Of ClsShareMovement)
            For i As Integer = obj.CertificateFrom To obj.CertificateTo
                Dim objTr As New ClsShareMovement()
                objTr.Source_Code = obj.Code
                objTr.Source_Date = obj.IDate
                objTr.Source_Type = "SH-MA"
                objTr.Share_Code = obj.Code
                'objTr.Certificate_No = obj.CertificateFrom
                objTr.Certificate_No = clsCommon.myCstr(i)
                objTr.Rate = obj.Rate
                objTr.Amount = obj.Rate
                objTr.RI = 1
                objTr.Status = 1
                objTr.Created_By = objCommonVar.CurrentUserCode
                objTr.Created_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")

                If (clsCommon.myLen(objTr.Source_Code) > 0) Then
                    Arr.Add(objTr)
                End If
            Next


            ClsShareMovement.SaveData(Arr, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Post_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Post_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHARE_MASTER", OMInsertOrUpdate.Update, "Code='" + clsCommon.myCstr(obj.Code) + "'", trans)


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


End Class
