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
    Public Arr As List(Of String) = New List(Of String)
    Public Certificate As String = Nothing
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
            IsSaved = True
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DCS_Code", obj.DCS_Code)
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "IDate", obj.IDate)
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
                Dim qrycertificate As String = "Select Certificate_No from TSPL_SHARE_MOVEMENT where Share_Code='" + obj.Share_Code + "'"
                Dim dtcertificate As DataTable = clsDBFuncationality.GetDataTable(qrycertificate, trans)
                If dtcertificate.Rows.Count > 0 Then
                    For i As Integer = 0 To dtcertificate.Rows.Count - 1
                        obj.Arr.Add(clsCommon.myCstr(dtcertificate(i)("Certificate_No")))
                    Next
                    obj.Certificate = clsCommon.GetMulcallStringWithComma(obj.Arr)
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
            qry = "delete from TSPL_SHARE_ALLOTMENT where Share_Code='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHARE_ALLOTMENT where Code='" + StrCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
