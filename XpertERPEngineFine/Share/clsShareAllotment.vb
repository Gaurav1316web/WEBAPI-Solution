Imports System.Data.SqlClient
Public Class clsShareAllotment
#Region "Variables"
    Public Code As String = Nothing
    Public DCS_Code As String = Nothing
    Public Name As String = Nothing
    Public IDate As DateTime
    Public Share_Code As String = Nothing
    Public Qty As Integer = 0
    Public Rate As Decimal = 0
    Public Amount As Decimal = 0
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_By As String = Nothing
    Public Post_Date As DateTime
    Public Remarks As String = Nothing
    'Public ArrCertificateNo As List(Of String) = New List(Of String)
    Public Arr As List(Of ClsShareMovement) = Nothing
    Public Certificate_No As List(Of String) = New List(Of String)
    Public Source_Code As String = Nothing
    Public Source_Type As String = Nothing
    Public RI As Integer = 0
    Public Source_Date As DateTime
#End Region
    Public Function SaveData(ByVal obj As clsShareAllotment, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As clsShareAllotment, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DCS_Code", obj.DCS_Code)
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "IDate", clsCommon.GetPrintDate(obj.IDate, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Share_Code", obj.Share_Code)
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
            clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.ShareAllotment, "", objCommonVar.strCurrUserLocations)
                If clsCommon.myLen(obj.Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHARE_ALLOTMENT", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHARE_ALLOTMENT", OMInsertOrUpdate.Update, "TSPL_SHARE_ALLOTMENT.Code='" + obj.Code + "'", trans)
            End If

            Dim Arr As New List(Of ClsShareMovement)
            If obj.Certificate_No.Count > 0 Then
                For i As Integer = 0 To obj.Certificate_No.Count - 1
                    Dim objTr As New ClsShareMovement()
                    objTr.Source_Code = obj.Code
                    objTr.Source_Date = clsCommon.GetPrintDate(obj.IDate, "dd/MMM/yyyy hh:mm tt")
                    objTr.Source_Type = "SH-AL"
                    objTr.Share_Code = obj.Share_Code
                    objTr.Certificate_No = clsCommon.myCstr(obj.Certificate_No(i))
                    objTr.Rate = obj.Rate
                    objTr.Amount = obj.Rate
                    objTr.RI = -1
                    objTr.Status = 0
                    objTr.Created_By = objCommonVar.CurrentUserCode
                    objTr.Created_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
                    If (clsCommon.myLen(objTr.Source_Code) > 0) Then
                        Arr.Add(objTr)
                    End If
                Next
                IsSaved = IsSaved AndAlso ClsShareMovement.SaveData(Arr, trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsShareAllotment
        Dim obj As clsShareAllotment = Nothing
        Try
            Dim strQry As String = "Select TSPL_SHARE_ALLOTMENT.*,TSPL_SHARE_MOVEMENT.Certificate_No,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.RegistrationNo,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader from TSPL_SHARE_ALLOTMENT
                                    Left Outer Join TSPL_SHARE_MOVEMENT ON TSPL_SHARE_MOVEMENT.Share_Code=TSPL_SHARE_ALLOTMENT.Share_Code
                                    Left Outer Join TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_SHARE_ALLOTMENT.DCS_Code
                                    Left Outer Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and Code = (select MIN(Code) from TSPL_SHARE_ALLOTMENT where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And Code = (Select Max(Code) from TSPL_SHARE_ALLOTMENT where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And Code = (Select Min(Code) from TSPL_SHARE_ALLOTMENT where TSPL_SHARE_ALLOTMENT.Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and Code = (select Max(Code) from TSPL_SHARE_ALLOTMENT where TSPL_SHARE_ALLOTMENT.Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and TSPL_SHARE_ALLOTMENT.Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsShareAllotment()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.IDate = clsCommon.myCstr(dt.Rows(0)("IDate"))
                obj.DCS_Code = clsCommon.myCstr(dt.Rows(0)("DCS_Code"))
                obj.Share_Code = clsCommon.myCstr(dt.Rows(0)("Share_Code"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
                obj.Qty = clsCommon.myCDecimal(dt.Rows(0)("Qty"))
                obj.Rate = clsCommon.myCDecimal(dt.Rows(0)("Rate"))
                obj.Amount = clsCommon.myCDecimal(dt.Rows(0)("Amount"))
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))

                Dim qrycertificate As String = "Select Certificate_No from TSPL_SHARE_MOVEMENT where Source_Code='" + obj.Code + "' and  Share_Code='" + obj.Share_Code + "'"
                Dim dtcertificate As DataTable = clsDBFuncationality.GetDataTable(qrycertificate, trans)
                If dtcertificate.Rows.Count > 0 Then
                    For i As Integer = 0 To dtcertificate.Rows.Count - 1
                        obj.Certificate_No.Add(clsCommon.myCstr(dtcertificate(i)("Certificate_No")))
                    Next
                End If
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
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal StrCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(StrCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_SHARE_ALLOTMENT where Code='" + StrCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = ""
            qry = "delete from TSPL_SHARE_MOVEMENT where Source_Code='" + StrCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
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
            Dim obj As clsShareAllotment = GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Code : " + strDocNo + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Post_Date)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHARE_ALLOTMENT", OMInsertOrUpdate.Update, "TSPL_SHARE_ALLOTMENT.Code='" + obj.Code + "'", trans)

            Dim coll1 As New Hashtable()
            clsCommon.AddColumnsForChange(coll1, "Status", 1)
            clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_SHARE_MOVEMENT", OMInsertOrUpdate.Update, "Source_Code='" + clsCommon.myCstr(obj.Code) + "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ReturnQry(ByVal strDocNo As String, ByVal NoOfShare As String) As String
        Dim qry As String = "select Max(RowNum)[S.No.],Certificate_No As [Certificate No],max(case when Source_Type='SH-MA' then Rate else 0 end) as Rate    
                             from (Select Row_Number() Over (Order By PK_ID) As RowNum,PK_ID,Share_Code,Certificate_No,Rate,Amount,RI,Source_Type from TSPL_SHARE_MOVEMENT 
                             Where Share_Code='" + strDocNo + "' and Certificate_No not in (select Certificate_No from TSPL_SHARE_MOVEMENT group by Certificate_No 
                             having count(Convert(Varchar(10),Certificate_No))>1  ))xxx where Certificate_No not in (select Certificate_No from TSPL_SHARE_MOVEMENT 
                             group by Certificate_No having count(Convert(Varchar(10),Certificate_No))>1 ) and RowNum<=" + NoOfShare + "
                             Group By Share_Code,Certificate_No Having sum(RI)>0 "
        Return qry
    End Function
End Class
