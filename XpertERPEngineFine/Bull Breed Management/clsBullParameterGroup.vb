Imports System.Data.SqlClient

Public Class clsBullParameterGroup
    Public Code As String = ""
    Public Name As String = ""
    ' Public PK_ID As String = " "
    Public Arr As List(Of clsBullParameterGroupDetail) = Nothing
    Public arrSelectionRange As List(Of clsBullParameterGroupDeatilRange) = Nothing


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBullParameterGroup
        Try
            Dim obj As clsBullParameterGroup = Nothing
            Dim qry As String = "SELECT Code,Name FROM TSPL_BULL_PARAMETER_GROUP_MASTER WHERE TSPL_BULL_PARAMETER_GROUP_MASTER.Code='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsBullParameterGroup()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Name = clsCommon.myCstr(dt.Rows(0)("Name"))

            End If
            qry = "select TSPL_BULL_PARAMETER_GROUP_Detail.*,TSPL_BULL_TEST_PARAMETER.Name,TSPL_BULL_TEST_PARAMETER.Type from TSPL_BULL_PARAMETER_GROUP_Detail 
                    left outer join TSPL_BULL_TEST_PARAMETER On TSPL_BULL_TEST_PARAMETER.Code=TSPL_BULL_PARAMETER_GROUP_Detail.TPCode
                    WHERE TSPL_BULL_PARAMETER_GROUP_Detail.Code='" + strCode + "' "

            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsBullParameterGroupDetail)
                Dim objTr As clsBullParameterGroupDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsBullParameterGroupDetail
                    objTr.Code = clsCommon.myCstr(dr("Code"))
                    objTr.TPCode = clsCommon.myCstr(dr("TPCode"))
                    objTr.Name = clsCommon.myCstr(dr("Name"))
                    objTr.Type = clsCommon.myCstr(dr("Type"))
                    objTr.Required_For_Result = clsCommon.myCstr(dr("Required_For_Result"))
                    objTr.From_Range = clsCommon.myCDecimal(dr("From_Range"))
                    objTr.To_Range = clsCommon.myCDecimal(dr("To_Range"))
                    objTr.R_Boolean = clsCommon.myCstr(dr("To_Range"))
                    objTr.Alpha_Numeric = clsCommon.myCstr(dr("Alpha_Numeric"))
                    objTr.Range_Selection = clsCommon.myCstr(dr("Range_Selection"))
                    obj.Arr.Add(objTr)
                Next
            End If
            Return obj
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
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
                Throw New Exception("Code No. not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_BULL_PARAMETER_GROUP_DETAIL_RANGE where Against_Group_Code='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BULL_PARAMETER_GROUP_Detail where Code='" + StrCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BULL_PARAMETER_GROUP_MASTER where Code='" + StrCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function SaveData(ByVal obj As clsBullParameterGroup, ByVal isNewEntry As Boolean) As Boolean
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
    Public Shared Function SaveData(ByVal obj As clsBullParameterGroup, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean

        Dim IsSaved As Boolean = True
        Try
            IsSaved = True

            Dim StrQry As String = "delete from TSPL_BULL_PARAMETER_GROUP_DETAIL_RANGE where Against_Group_Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)

            StrQry = Nothing
            StrQry = "delete from TSPL_BULL_PARAMETER_GROUP_Detail where Code='" + obj.Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)


            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                'obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.frmBullParameterGroup, "", objCommonVar.strCurrUserLocations)
                'If clsCommon.myLen(obj.Code) <= 0 Then
                '    Throw New Exception("Error in Code Generation")
                'End If
                'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_PARAMETER_GROUP_master", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_PARAMETER_GROUP_master", OMInsertOrUpdate.Update, "TSPL_BULL_PARAMETER_GROUP_master.Code='" + obj.Code + "'", trans)
            End If

            IsSaved = IsSaved AndAlso clsBullParameterGroupDetail.SaveData(clsCommon.myCstr(obj.Code), obj.Arr, trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        'whrcls = "  IsDistributor='Y' "
        Dim qry As String = "select Code , Name  From TSPL_BULL_PARAMETER_GROUP_Master "
        str = clsCommon.ShowSelectForm("SECCUSTFIND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
End Class

Public Class clsBullParameterGroupDetail
    Public Code As String = ""
    'Public BULLCODE As String = ""
    Public PK_Id As String = ""
    Public TPCode As String = ""
    Public Name As String = ""
    Public Type As String = ""
    Public Required_For_Result As String = ""
    Public From_Range As Decimal
    Public To_Range As Decimal
    Public R_Boolean As String = ""
    Public Alpha_Numeric As String = ""
    Public Range_Selection As Decimal
    Public RangeArr As Dictionary(Of String, String)


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBullParameterGroupDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBullParameterGroupDetail In Arr
                Dim colm As New Hashtable()
                clsCommon.AddColumnsForChange(colm, "Code", strDocNo)
                clsCommon.AddColumnsForChange(colm, "TPCode", obj.Code)
                clsCommon.AddColumnsForChange(colm, "Required_For_Result", obj.Required_For_Result)
                clsCommon.AddColumnsForChange(colm, "From_Range", obj.From_Range)
                clsCommon.AddColumnsForChange(colm, "To_Range", obj.To_Range)
                clsCommon.AddColumnsForChange(colm, "R_Boolean", obj.R_Boolean)
                clsCommon.AddColumnsForChange(colm, "Alpha_Numeric", obj.Alpha_Numeric)
                clsCommon.AddColumnsForChange(colm, "Range_Selection", obj.Range_Selection)
                clsCommonFunctionality.UpdateDataTable(colm, "TSPL_BULL_PARAMETER_GROUP_Detail", OMInsertOrUpdate.Insert, "", trans)
                If clsCommon.myCstr(obj.Range_Selection) IsNot Nothing AndAlso clsCommon.myCdbl(obj.Range_Selection) > 0 Then
                    Dim pkID As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("Select PK_ID from TSPL_BULL_PARAMETER_GROUP_DETAIL Where Code='" + strDocNo + "' And IsNull(Range_Selection,0)>0 ", trans))
                    clsBullParameterGroupDeatilRange.SaveData(strDocNo, obj.RangeArr, pkID, trans)
                End If
            Next
        End If
        Return True
    End Function
End Class

Public Class clsBullParameterGroupDeatilRange
    Public Against_Group_Code As String
    Public Against_Detail_PK_Id As Decimal
    Public Range_Selection As String
    Public arrSelectionRange As List(Of clsBullParameterGroupDeatilRange) = Nothing
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As Dictionary(Of String, String), ByVal pkID As Decimal, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For ii As Integer = 0 To Arr.Count - 1
                Dim index As Integer = 1
                Dim colm As New Hashtable()
                clsCommon.AddColumnsForChange(colm, "Against_Group_Code", strDocNo)
                'clsCommon.AddColumnsForChange(colm, "Against_Detail_PK_Id", obj.Against_Detail_PK_Id)
                clsCommon.AddColumnsForChange(colm, "Against_Detail_PK_Id", pkID)
                clsCommon.AddColumnsForChange(colm, "Range_Selection", Arr(Arr.Keys(ii)))
                clsCommonFunctionality.UpdateDataTable(colm, "TSPL_BULL_PARAMETER_GROUP_DETAIL_RANGE", OMInsertOrUpdate.Insert, "", trans)
                index += 1
            Next
        End If
        Return True
    End Function
End Class