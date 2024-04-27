Imports System.Data.SqlClient
Public Class ClsCMUGrouping


    Public Code As String = ""
    Public Remarks As String = ""
    Public Name As String = ""
    Public Doc_Date As Date
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Post_By As String = Nothing
    Public Post_Date As DateTime
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime
    Public Arr As List(Of ClsCMUGroupingDetail) = Nothing



    Public Shared Function SaveData(ByVal obj As ClsCMUGrouping, ByVal isNewEntry As Boolean) As Boolean
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

    Public Shared Function SaveData(ByVal obj As ClsCMUGrouping, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim IsSaved As Boolean = True

        Try
            IsSaved = True

            Dim StrQry As String = ""
            StrQry = "delete from TSPL_BULL_CMU_GROUPING_Detail where Document_No='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            'clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmBullCMUGrouping, "", objCommonVar.strCurrUserLocations)
                If clsCommon.myLen(obj.Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CMU_GROUPING", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CMU_GROUPING", OMInsertOrUpdate.Update, "TSPL_BULL_CMU_GROUPING.Document_No='" + obj.Code + "'", trans)
            End If

            IsSaved = IsSaved AndAlso ClsCMUGroupingDetail.SaveData(clsCommon.myCstr(obj.Code), obj.Arr, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isSaved As Boolean = True
            isSaved = DeleteData(strCode, trans)
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
                Throw New Exception("Document_No not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_BULL_CMU_GROUPING_Detail where Document_No='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BULL_CMU_GROUPING where Document_No='" + StrCode + "'"
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
                Throw New Exception("Document_No not found to Post")
            End If
            Dim obj As ClsCMUGrouping = ClsCMUGrouping.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("Document_No : " + strDocNo + " not found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Post_Date)
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CMU_GROUPING", OMInsertOrUpdate.Update, "Document_No='" + clsCommon.myCstr(obj.Code) + "'", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsCMUGrouping
        Try
            Dim obj As ClsCMUGrouping = Nothing
            Dim qry As String = "SELECT Document_No,Name,Remarks,Status,* FROM TSPL_BULL_CMU_GROUPING WHERE 2=2 "
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Document_No = (select MIN(Document_No) from TSPL_BULL_CMU_GROUPING where 1=1  )"
                Case NavigatorType.Last
                    qry += " And Document_No = (Select Max(Document_No) from TSPL_BULL_CMU_GROUPING where 1=1 )"
                Case NavigatorType.Next
                    qry += " And Document_No = (Select Min(Document_No) from TSPL_BULL_CMU_GROUPING where Document_No>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    qry += " and Document_No = (select Max(Document_No) from TSPL_BULL_CMU_GROUPING where Document_No<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    qry += " and Document_No = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsCMUGrouping()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Document_No"))
                obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))
                'clsCommon.GetPrintDate(dt.Rows(0)("Start_Date"), "dd/MMM/yyyy")
                obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                obj.Arr = ClsCMUGroupingDetail.GetData(obj.Code, trans)
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class ClsCMUGroupingDetail
    Public GroupCode As String = Nothing
    Public GroupName As String = Nothing
    Public ParameterCode As String = Nothing
    Public Document_No As String = Nothing
    Public Parameter_Type As String = Nothing
    Public Parameter_Name As String = Nothing
    Public Pk_Id As Integer = 0



    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsCMUGroupingDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As ClsCMUGroupingDetail In Arr
                    Dim colm As New Hashtable()
                    clsCommon.AddColumnsForChange(colm, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(colm, "Against_Parameter_Group_Code", obj.Pk_Id)
                    ' clsCommon.AddColumnsForChange(colm, "Parameter_Code", obj.ParameterCode)
                    'clsCommon.AddColumnsForChange(colm, "Amount", obj.ParameterCode)
                    clsCommonFunctionality.UpdateDataTable(colm, "TSPL_BULL_CMU_GROUPING_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return True
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of ClsCMUGroupingDetail)
        Dim arr As List(Of ClsCMUGroupingDetail) = Nothing
        Try
            Dim dt As New DataTable
            Dim strQry As String = " select TSPL_BULL_CMU_GROUPING_Detail.Document_No,TSPL_BULL_CMU_GROUPING.Name,TSPL_BULL_CMU_GROUPING.Remarks,TSPL_BULL_SHED_PARAMETER_DETAIL.PK_Id,
	                                 TSPL_BULL_SHED_PARAMETER_DETAIL.Code as GroupCode,TSPL_BULL_SHED_PARAMETER_MASTER.Name as GroupName,TSPL_BULL_SHED_PARAMETER_DETAIL.PCode,
                                     TSPL_BULL_SHED_PARAMETER.Name as ParameterName,TSPL_BULL_SHED_PARAMETER.Type as ParameterType  from TSPL_BULL_CMU_GROUPING_Detail
                                     left outer join TSPL_BULL_CMU_GROUPING on TSPL_BULL_CMU_GROUPING.Document_No = TSPL_BULL_CMU_GROUPING_Detail.Document_No
                                     left outer join TSPL_BULL_SHED_PARAMETER_DETAIL on TSPL_BULL_SHED_PARAMETER_DETAIL.PK_Id = TSPL_BULL_CMU_GROUPING_Detail.Against_Parameter_Group_Code
                                     left outer join TSPL_BULL_SHED_PARAMETER_MASTER on TSPL_BULL_SHED_PARAMETER_MASTER.Code = TSPL_BULL_SHED_PARAMETER_DETAIL.Code
                                     left outer join TSPL_BULL_SHED_PARAMETER on TSPL_BULL_SHED_PARAMETER.Code = TSPL_BULL_SHED_PARAMETER_DETAIL.PCode 
                                     where TSPL_BULL_CMU_GROUPING_Detail.Document_No= '" & strDocNo & "' "

            dt = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                arr = New List(Of ClsCMUGroupingDetail)
                Dim objTr As ClsCMUGroupingDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsCMUGroupingDetail
                    objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                    objTr.Parameter_Type = clsCommon.myCstr(dr("ParameterType"))
                    objTr.Parameter_Name = clsCommon.myCstr(dr("ParameterName"))
                    objTr.ParameterCode = clsCommon.myCstr(dr("PCode"))
                    objTr.GroupCode = clsCommon.myCstr(dr("GroupCode"))
                    objTr.GroupName = clsCommon.myCstr(dr("GroupName"))
                    objTr.Pk_Id = clsCommon.myCstr(dr("PK_Id"))

                    arr.Add(objTr)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function
End Class
