
Imports common
Imports System.Data.SqlClient

Public Class clsGazeReading
#Region "variables"
    Public Code As String = Nothing
    Public Description As String = Nothing
    Public Capacity As Integer
    Public Arr As List(Of clsGazeReadingDetail)

#End Region

    Public Shared Function SaveData(obj As clsGazeReading, ByVal IsNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "Delete from TSPL_GAZE_READING_DETAIL where Code='" + obj.Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim CurrDate As DateTime = clsCommon.GETSERVERDATE(trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Capacity", obj.Capacity)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(CurrDate, "dd/MMM/yyyy hh:mm:ss tt"))
            If IsNewEntry Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, CurrDate, clsDocType.GazeReading, "", "")
                If clsCommon.myLen(obj.Code) < 0 Then
                    Throw New Exception("Eroor in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(CurrDate, "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GAZE_READING", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GAZE_READING", OMInsertOrUpdate.Update, "Code = '" + obj.Code + "'", trans)
            End If
            clsGazeReadingDetail.SaveData(obj.Code, obj.Arr, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim obj As clsGazeReading = clsGazeReading.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String = "Delete from TSPL_GAZE_READING_DETAIL where Code='" + obj.Code + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_GAZE_READING where Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsGazeReading
        Dim obj As clsGazeReading = Nothing
        Try
            Dim qry As String = "Select TSPL_GAZE_READING.* from TSPL_GAZE_READING Where 1=1"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and TSPL_GAZE_READING.Code = (select MIN(TSPL_GAZE_READING.Code) from TSPL_GAZE_READING )"
                Case NavigatorType.Last
                    qry += " and TSPL_GAZE_READING.Code = (select MAX(TSPL_GAZE_READING.Code) from TSPL_GAZE_READING )"
                Case NavigatorType.Next
                    qry += " and TSPL_GAZE_READING.Code = (select MIN(TSPL_GAZE_READING.Code) from TSPL_GAZE_READING where TSPL_GAZE_READING.Code > '" + strCode + "' )"
                Case NavigatorType.Previous
                    qry += " and TSPL_GAZE_READING.Code = (select MAX(TSPL_GAZE_READING.Code) from TSPL_GAZE_READING where TSPL_GAZE_READING.Code < '" + strCode + "')"
                Case NavigatorType.Current
                    qry += " and TSPL_GAZE_READING.Code = '" + strCode + "'"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj = New clsGazeReading()
                obj.Code = clsCommon.myCstr(dt.Rows(0)("Code"))
                obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
                obj.Capacity = clsCommon.myCdbl(dt.Rows(0)("Capacity"))
                obj.Arr = clsGazeReadingDetail.GetData(obj.Code)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function
End Class

Public Class clsGazeReadingDetail
#Region "variables"
    Public Code As String = Nothing
    Public MM As Integer
    Public Value As Integer
#End Region

    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsGazeReadingDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                For Each obj As clsGazeReadingDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Code", strCode)
                    clsCommon.AddColumnsForChange(coll, "MM", obj.MM)
                    clsCommon.AddColumnsForChange(coll, "Value", obj.Value)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GAZE_READING_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String) As List(Of clsGazeReadingDetail)
        Dim arr As New List(Of clsGazeReadingDetail)
        Dim qry As String = "Select TSPL_GAZE_READING_DETAIL.* from TSPL_GAZE_READING_DETAIL Where Code='" + strCode + "' order by MM "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim obj As New clsGazeReadingDetail()
                obj.Code = clsCommon.myCstr(dr("Code"))
                obj.MM = clsCommon.myCdbl(dr("MM"))
                obj.Value = clsCommon.myCdbl(dr("Value"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class





