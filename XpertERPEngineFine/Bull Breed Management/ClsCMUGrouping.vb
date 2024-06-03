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
    Public arrSelectionRange As List(Of clsBullCMUGroupingDeatilRange) = Nothing
    Public arrPKID As List(Of clsBullCMUGroupingDeatilRange) = Nothing



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

            StrQry = "delete from TSPL_BULL_CMU_GROUPING_DETAIL_RANGE where Against_Group_Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)

            StrQry = "delete from TSPL_BULL_CMU_GROUPING_Detail where Document_No='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Code)
            'clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                'obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmBullCMUGrouping, "", objCommonVar.strCurrUserLocations)
                'If clsCommon.myLen(obj.Code) <= 0 Then
                '    Throw New Exception("Error in Code Generation")
                'End If
                ' clsCommon.AddColumnsForChange(coll, "Document_No", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CMU_GROUPING", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CMU_GROUPING", OMInsertOrUpdate.Update, "TSPL_BULL_CMU_GROUPING.Document_No='" + obj.Code + "'", trans)
            End If

            IsSaved = IsSaved AndAlso ClsCMUGroupingDetail.SaveData(clsCommon.myCstr(obj.Code), obj.Arr, trans)



            'Dim Qry As String = ""
            'Dim dt As New DataTable
            'Qry = "select TSPL_BULL_CMU_GROUPING_detail.PK_Id from TSPL_BULL_CMU_GROUPING_detail where Document_No='" + obj.Code + "'"
            'dt = clsDBFuncationality.GetDataTable(Qry, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    obj.arrPKID = New List(Of clsBullCMUGroupingDeatilRange)
            '    Dim objTr As clsBullCMUGroupingDeatilRange
            '    For Each dr As DataRow In dt.Rows
            '        objTr = New clsBullCMUGroupingDeatilRange
            '        objTr.Against_Detail_PK_Id = clsCommon.myCstr(dr("PK_Id"))
            '        obj.arrPKID.Add(objTr)
            '    Next
            'End If
            ''Return obj
            'IsSaved = IsSaved AndAlso clsBullCMUGroupingDeatilRange.SaveData(clsCommon.myCstr(obj.Code), obj.Arr, trans)
            'IsSaved = IsSaved AndAlso clsBullCMUGroupingDeatilRange.SaveData(clsCommon.myCstr(obj.Code), obj.arrSelectionRange, trans)
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
            qry = "delete from TSPL_BULL_CMU_GROUPING_DETAIL_RANGE where Against_Group_Code='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
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
    Public Required_For_Result As String = ""
    Public From_Range As Decimal
    Public To_Range As Decimal
    Public R_Boolean As String = ""
    Public Alpha_Numeric As String = ""
    Public Range_Selection As Decimal
    Public RangeArr As Dictionary(Of String, String)



    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsCMUGroupingDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As ClsCMUGroupingDetail In Arr
                    Dim colm As New Hashtable()
                    clsCommon.AddColumnsForChange(colm, "Document_No", strDocNo)
                    clsCommon.AddColumnsForChange(colm, "Against_Parameter_Group_Code", obj.Pk_Id)
                    'clsCommon.AddColumnsForChange(colm, "Against_Parameter_Code", obj.GroupCode)
                    clsCommon.AddColumnsForChange(colm, "Required_For_Result", obj.Required_For_Result)
                    clsCommon.AddColumnsForChange(colm, "From_Range", obj.From_Range)
                    clsCommon.AddColumnsForChange(colm, "To_Range", obj.To_Range)
                    clsCommon.AddColumnsForChange(colm, "R_Boolean", obj.R_Boolean)
                    clsCommon.AddColumnsForChange(colm, "Alpha_Numeric", obj.Alpha_Numeric)
                    clsCommon.AddColumnsForChange(colm, "Range_Selection", obj.Range_Selection)
                    'clsBullCMUGroupingDeatilRange.SaveData(strDocNo, obj.RangeArr, trans)
                    ' clsCommon.AddColumnsForChange(colm, "Parameter_Code", obj.ParameterCode)
                    'clsCommon.AddColumnsForChange(colm, "Amount", obj.ParameterCode)
                    clsCommonFunctionality.UpdateDataTable(colm, "TSPL_BULL_CMU_GROUPING_Detail", OMInsertOrUpdate.Insert, "", trans)
                    If clsCommon.myCstr(obj.Range_Selection) IsNot Nothing AndAlso clsCommon.myCdbl(obj.Range_Selection) > 0 Then
                        Dim pkID As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("Select PK_ID from TSPL_BULL_CMU_GROUPING_Detail Where Document_No ='" + strDocNo + "' And IsNull(Range_Selection,0)>0 ", trans))
                        clsBullCMUGroupingDeatilRange.SaveData(strDocNo, obj.RangeArr, pkID, trans)
                        'clsBullCMUGroupingDeatilRange.SaveData(strDocNo, obj.RangeArr, trans)
                    End If

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
            Dim strQry As String = " select TSPL_BULL_CMU_GROUPING_Detail.*,TSPL_BULL_CMU_GROUPING.Name,TSPL_BULL_CMU_GROUPING.Remarks,TSPL_BULL_SHED_PARAMETER_DETAIL.PK_Id as AgainstPK_Id,
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
                    objTr.Pk_Id = clsCommon.myCstr(dr("AgainstPK_Id"))
                    objTr.Required_For_Result = clsCommon.myCstr(dr("Required_For_Result"))
                    objTr.From_Range = clsCommon.myCDecimal(dr("From_Range"))
                    objTr.To_Range = clsCommon.myCDecimal(dr("To_Range"))
                    objTr.R_Boolean = clsCommon.myCstr(dr("To_Range"))
                    objTr.Alpha_Numeric = clsCommon.myCstr(dr("Alpha_Numeric"))
                    objTr.Range_Selection = clsCommon.myCstr(dr("Range_Selection"))
                    objTr.RangeArr = clsBullCMUGroupingDeatilRange.GetData(objTr.Document_No)

                    arr.Add(objTr)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function
End Class

Public Class clsBullCMUGroupingDeatilRange
    Public Against_Group_Code As String
    Public Against_Detail_PK_Id As Decimal
    Public Range_Selection As String

    Public Shared Function GetData(ByVal strCode As String) As Dictionary(Of String, String)
        Return GetData(strCode, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal tran As SqlTransaction) As Dictionary(Of String, String)
        'Dim arr As New Dictionary(Of String, String)
        'Dim qry As String = "Select TSPL_BULL_CMU_GROUPING_DETAIL_RANGE.* from TSPL_BULL_CMU_GROUPING_DETAIL_RANGE Where Against_Group_Code='" + strCode + "'"
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '    For Each dr As DataRow In dt.Rows
        '        arr.Add(clsCommon.myCDecimal(dr("SNF_Per")), clsCommon.myCDecimal(dr("Range_Selection")))
        '    Next
        'End If
        'Return arr
    End Function
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As Dictionary(Of String, String), ByVal PkID As String, ByVal trans As SqlTransaction) As Boolean
        'Public Shared Function SaveData(ByVal strDocNo As String, ByVal arrSelection As List(Of clsBullCMUGroupingDeatilRange), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For ii As Integer = 0 To Arr.Count - 1
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Against_Group_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Against_Detail_PK_Id", PkID)
                clsCommon.AddColumnsForChange(coll, "Range_Selection", Arr(Arr.Keys(ii)))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_CMU_GROUPING_DETAIL_RANGE", OMInsertOrUpdate.Insert, "", trans)
            Next
            'For Each obj As clsBullCMUGroupingDeatilRange In arrSelection
            '    Dim colm As New Hashtable()
            '    clsCommon.AddColumnsForChange(colm, "Against_Group_Code", strDocNo)
            '    'clsCommon.AddColumnsForChange(colm, "Against_Detail_PK_Id", obj.Against_Detail_PK_Id)
            '    clsCommon.AddColumnsForChange(colm, "Against_Detail_PK_Id", arrPKID.Item(0).Against_Detail_PK_Id)
            '    clsCommon.AddColumnsForChange(colm, "Range_Selection", Arr.Keys(ii))
            '    clsCommonFunctionality.UpdateDataTable(colm, "TSPL_BULL_CMU_GROUPING_DETAIL_RANGE", OMInsertOrUpdate.Insert, "", trans)
            'Next
        End If
        Return True
    End Function


End Class
