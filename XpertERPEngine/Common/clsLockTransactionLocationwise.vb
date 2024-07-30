Imports common
Imports System.Data.SqlClient
Public Class clsLockTransactionLocationwise
#Region "Variables"
    Public Location_Code As String = Nothing
    Public Module_Name As String = Nothing
    Public Trans_Name As String = Nothing
    Public Is_Locked As Boolean
    Public Start_Date As Date?
    Public End_Date As Date?
#End Region
    Public Shared Function SaveData(ByVal strLocation As String, ByVal strTransName As String, ByVal arr As List(Of clsLockTransactionLocationwise), ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim isSaved As Boolean = True
            For Each obj As clsLockTransactionLocationwise In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Module_Name", obj.Module_Name)
                clsCommon.AddColumnsForChange(coll, "Trans_Name", obj.Trans_Name)
                clsCommon.AddColumnsForChange(coll, "Is_Locked", IIf(obj.Is_Locked, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCK_LOCATION", OMInsertOrUpdate.Insert, "", Trans)
            Next
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


End Class
Public Class clsLockTransactionLocationSegmentwise
#Region "Variables"
    Public Location_Segment_Code As String = Nothing
    Public Module_Name As String = Nothing
    Public Trans_Name As String = Nothing
    Public Is_Locked As Boolean
    Public Start_Date As Date?
    Public End_Date As Date?
    Public Program_Code As String = Nothing

    Public Shared Function SaveData(ByVal strLocation As String, ByVal strTransName As String, ByVal arr As List(Of clsLockTransactionLocationSegmentwise), ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim isSaved As Boolean = True
            For Each obj As clsLockTransactionLocationSegmentwise In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Location_Segment_Code", obj.Location_Segment_Code)
                clsCommon.AddColumnsForChange(coll, "Module_Name", obj.Module_Name)
                clsCommon.AddColumnsForChange(coll, "Trans_Name", obj.Trans_Name)
                clsCommon.AddColumnsForChange(coll, "Is_Locked", IIf(obj.Is_Locked, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCK_LOCATION_SEGMENT", OMInsertOrUpdate.Insert, "", Trans)
            Next
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region
End Class
Public Class clsLockTransactionLocationUserwise
#Region "Variables"
    Public Location_Code As String = Nothing
    Public Module_Name As String = Nothing
    Public Trans_Name As String = Nothing
    Public User_Code As String = Nothing
    Public User_Name As String = Nothing
    Public ToDate As Date?
    Public Status As String = Nothing
    Public ModuleCode As String = Nothing
    Public TransCode As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strLocation As String, ByVal strTransName As String, ByVal arr As List(Of clsLockTransactionLocationUserwise), ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim isSaved As Boolean = True
            For Each obj As clsLockTransactionLocationUserwise In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Module_Name", obj.Module_Name)
                clsCommon.AddColumnsForChange(coll, "Trans_Name", obj.Trans_Name)
                clsCommon.AddColumnsForChange(coll, "User_Code", obj.User_Code)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "ToDate", clsCommon.GetPrintDate(obj.ToDate, "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCK_LOCATION_USER", OMInsertOrUpdate.Insert, "", Trans)
            Next
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal Location As String, ByVal strModule As String, ByVal strTransaction As String) As List(Of clsLockTransactionLocationUserwise)
        Dim qry As String = ""
        If clsCommon.myLen(Location) > 0 Then
            qry = " select '0' as Status, TSPL_LOCK_LOCATION_USER.User_Code,User_Name,ToDate as Date from TSPL_LOCK_LOCATION_USER " & _
           "left outer join tspL_USER_MASTER on TSPL_LOCK_LOCATION_USER.User_Code=tspL_USER_MASTER.User_Code where Location_Code  ='" & Location & "'   and Module_Name='" & strModule & "' " & _
          "and Trans_Name='" & strTransaction & "' " & _
           "union all " & _
           " select '1' as Status, User_Code,USER_NAME,'' as Date from tspL_USER_MASTER " & _
           "where  user_code not in (select  TSPL_LOCK_LOCATION_USER.User_Code from TSPL_LOCK_LOCATION_USER where Location_Code  ='" & Location & "'   and Module_Name='" & strModule & "' " & _
          "and Trans_Name='" & strTransaction & "' ) "
        Else
            qry = " select '0' as Status, TSPL_LOCK_LOCATION_USER.User_Code,User_Name,ToDate as Date from TSPL_LOCK_LOCATION_USER " & _
           "left outer join tspL_USER_MASTER on TSPL_LOCK_LOCATION_USER.User_Code=tspL_USER_MASTER.User_Code where Location_Code  ='" & Location & "'   and Module_Name='" & strModule & "' " & _
          "and Trans_Name='" & strTransaction & "' " & _
           "union all " & _
           " select '1' as Status, User_Code,USER_NAME,'' as Date from tspL_USER_MASTER " & _
           "where  user_code not in (select  TSPL_LOCK_LOCATION_USER.User_Code from TSPL_LOCK_LOCATION_USER where Location_Code  ='" & Location & "'   and Module_Name='" & strModule & "' " & _
          "and Trans_Name='" & strTransaction & "' ) "
        End If
       
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As List(Of clsLockTransactionLocationUserwise) = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsLockTransactionLocationUserwise)
            For Each dr As DataRow In dt.Rows
                Dim objTr As clsLockTransactionLocationUserwise = New clsLockTransactionLocationUserwise()
                objTr.Status = clsCommon.myCstr(dr("Status"))
                objTr.User_Code = clsCommon.myCstr(dr("User_Code"))
                objTr.User_Name = clsCommon.myCstr(dr("User_Name"))
                If dr("Date") IsNot DBNull.Value Then
                    objTr.ToDate = clsCommon.myCDate(dr("Date"))
                End If
                'objTr.ToDate = clsCommon.myCDate(dr("Date"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class
Public Class clsLockTransactionLocationSegmentUserwise
#Region "Variables"
    Public Location_Segment_Code As String = Nothing
    Public Module_Name As String = Nothing
    Public Trans_Name As String = Nothing
    Public User_Code As String = Nothing
    Public User_Name As String = Nothing
    Public ToDate As Date?
    Public Status As String = Nothing
    Public TransactionName As String = Nothing

#End Region
    Public Shared Function SaveData(ByVal strLocation As String, ByVal strTransName As String, ByVal arr As List(Of clsLockTransactionLocationSegmentUserwise), ByVal Trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim isSaved As Boolean = True
            For Each obj As clsLockTransactionLocationSegmentUserwise In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Location_Segment_Code", obj.Location_Segment_Code)
                clsCommon.AddColumnsForChange(coll, "Module_Name", obj.Module_Name)
                clsCommon.AddColumnsForChange(coll, "Trans_Name", obj.Trans_Name)
                clsCommon.AddColumnsForChange(coll, "User_Code", obj.User_Code)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "ToDate", clsCommon.GetPrintDate(obj.ToDate, "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOCK_LOCATION_SEGMENT_USER", OMInsertOrUpdate.Insert, "", Trans)
            Next
            Return isSaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal Location As String, ByVal strModule As String, ByVal strTransaction As String) As List(Of clsLockTransactionLocationSegmentUserwise)
        Dim qry As String = ""
        If clsCommon.myLen(Location) > 0 Then
            qry = "select '0' as Status, TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code,User_Name,ToDate as Date from TSPL_LOCK_LOCATION_SEGMENT_USER " & _
          "left outer join tspL_USER_MASTER on TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code=tspL_USER_MASTER.User_Code where Location_Segment_Code  ='" & Location & "'   and Module_Name='" & strModule & "' " & _
          "and Trans_Name='" & strTransaction & "'   " & _
          "union all " & _
          "select '1' as Status, User_Code,USER_NAME,'' as Date from tspL_USER_MASTER " & _
          "where  user_code not in (select  TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code from TSPL_LOCK_LOCATION_SEGMENT_USER where Location_Segment_Code  ='" & Location & "'   and Module_Name='" & strModule & "' " & _
          "and Trans_Name='" & strTransaction & "'  ) "
        Else
            qry = "select '0' as Status, TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code,User_Name,ToDate as Date from TSPL_LOCK_LOCATION_SEGMENT_USER " & _
          "left outer join tspL_USER_MASTER on TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code=tspL_USER_MASTER.User_Code where Location_Segment_Code  ='" & Location & "'   and Module_Name='" & strModule & "' " & _
          "and Trans_Name='" & strTransaction & "'   " & _
          "union all " & _
          "select '1' as Status, User_Code,USER_NAME,'' as Date from tspL_USER_MASTER " & _
          "where  user_code not in (select  TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code from TSPL_LOCK_LOCATION_SEGMENT_USER where Location_Segment_Code  ='" & Location & "'   and Module_Name='" & strModule & "' " & _
          "and Trans_Name='" & strTransaction & "'  ) "
        End If
      
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim Arr As List(Of clsLockTransactionLocationSegmentUserwise) = Nothing
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Arr = New List(Of clsLockTransactionLocationSegmentUserwise)
            For Each dr As DataRow In dt.Rows
                Dim objTr As clsLockTransactionLocationSegmentUserwise = New clsLockTransactionLocationSegmentUserwise()
                objTr.Status = clsCommon.myCstr(dr("Status"))
                objTr.User_Code = clsCommon.myCstr(dr("User_Code"))
                objTr.User_Name = clsCommon.myCstr(dr("User_Name"))
                objTr.ToDate = clsCommon.myCDate(dr("Date"))
                Arr.Add(objTr)
            Next
        End If
        Return Arr
    End Function
End Class



